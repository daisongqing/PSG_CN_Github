using AwareTec.Polysmith.DataBaseCom;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AwareTec.Polysmith.DataMangement
{
    /// <summary>
    /// 事件管理单元
    /// </summary>
    public class EventRecordsUit : IUnit
    {
        /// <summary>
        /// 定义事件类
        /// </summary>
        public class EventRecord
        {
            /// <summary>
            /// 获取事件ID
            /// </summary>
            public string ID
            {
                get
                {
                    return string.Format("{0}:{1}-{2}", MarkType, StartIndex, EndIndex);
                }
            }
            private bool m_byHand = false;
            /// <summary>
            /// 是否为手动标记
            /// </summary>
            public bool ByHand { set { m_byHand = value; } get { return m_byHand; } }
            private bool m_delete = false;
            /// <summary>
            /// 是否被删除标志
            /// </summary>
            public bool DeleteByHand { set { m_delete = value; } get { return m_delete; } }
            /// <summary>
            /// 开始索引
            /// </summary>
            public int StartIndex { set; get; }
            /// <summary>
            /// 结束索引
            /// </summary>
            public int EndIndex { set; get; }
            /// <summary>
            /// 事件类型
            /// </summary>
            public int MarkType { set; get; }
            /// <summary>
            /// 预留字段
            /// </summary>
            public string Comments { set; get; }

            private UpdateMode m_mode = UpdateMode.None;
            /// <summary>
            /// 获取更改类型
            /// </summary>
            internal UpdateMode Mode
            {
                private set
                {
                    m_mode = value;
                }
                get
                {
                    return m_mode;
                }
            }
            /// <summary>
            /// Key
            /// </summary>
            internal string Key
            {
                private set;
                get;
            }
            /// <summary>
            /// 传入条件 格式为： 更改类型|ID
            /// </summary>
            public string Condition
            {
                set
                {
                    if (string.IsNullOrEmpty(value))
                        return;
                    string[] ss = value.Split('|');
                    m_mode = (UpdateMode)int.Parse(ss[0]);
                    if (ss.Length > 1)
                    {
                        Key = ss[1];
                    }
                }
            }
            /// <summary>
            /// ToString()
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return string.Format("{3}/{4}{5}:{0}-{1}{2};", StartIndex, EndIndex, Comments == "" ? "" : string.Format("-{0}", Comments), MarkType, m_byHand ? 1 : 0, string.Format("{0}", m_delete ? "/1" : ""));
            }
            /// <summary>
            /// 比较器
            /// </summary>
            public class RecordComparer : IEqualityComparer<EventRecord>
            {
                bool IEqualityComparer<EventRecord>.Equals(EventRecord x, EventRecord y)
                {
                    return (x.ID.Equals(y.ID));
                }

                int IEqualityComparer<EventRecord>.GetHashCode(EventRecord obj)
                {
                    if (Object.ReferenceEquals(obj, null))
                        return 0;
                    return obj.ID.GetHashCode();
                }
            }
        }
        #region 私有成员
        /// <summary>
        /// 存储事件队列表
        /// </summary>
        private List<EventRecord> m_records = new List<EventRecord>();
        /// <summary>
        /// 定时任务时间间隔
        /// </summary>
        private int m_sleepTime = 3000;
        /// <summary>
        /// 自动保存的任务线程
        /// </summary>
        private System.Threading.Thread th = null;
        /// <summary>
        /// 终结任务标志位
        /// </summary>
        private bool m_KillTask = false;
        /// <summary>
        /// 是否需要更新
        /// </summary>
        private bool m_fresh = false;
        /// <summary>
        /// 队列锁
        /// </summary>
        private object m_lockObj = new object();
        /// <summary>
        /// 开启任务
        /// </summary>
        private void TaskRun()
        {
            th = new System.Threading.Thread(() =>
            {
                while (!m_KillTask)
                {
                    System.Threading.Thread.Sleep(m_sleepTime);
                    SaveData();
                }
            });
            th.IsBackground = true;
            th.SetApartmentState(System.Threading.ApartmentState.MTA);
            th.Start();
        }
        /// <summary>
        /// 数据实时存储
        /// </summary>
        private void SaveData()
        {
            if (m_fresh)
            {
                byte[] data = System.Text.Encoding.Default.GetBytes(ToString());
                int cnt = 0;
            write:
                try
                {
                    if (m_writer == null)
                        m_writer = CreatWriter(m_savePath);
                    m_writer.BaseStream.SetLength(0);
                    m_writer.Write(data);
                    m_writer.Flush();
                }
                catch
                {
                    cnt++;
                    if (cnt < 4)
                    {
                        System.Threading.Thread.Sleep(100);
                        ///重新再写一遍
                        goto write;
                    }
                }
                m_fresh = false;
            }
        }
        #endregion
        #region 公有成员
        /// <summary>
        /// 构造函数
        /// </summary>
        public EventRecordsUit()
        {

            FileName = "events.evs";

        }
        /// <summary>
        /// 开启服务
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            m_KillTask = false;
            TaskRun();
            return true;
        }
        /// <summary>
        /// 另存文件
        /// </summary>
        public bool SaveCopyAs()
        {
            try
            {
                if (File.Exists(m_saveasPath))
                    File.Delete(m_saveasPath);
                if (!File.Exists(m_savePath))
                {
                    m_fresh = true;
                    SaveData();
                }
                File.Copy(m_savePath, m_saveasPath);
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
                return false;
            }
            return true;
        }
        /// <summary>
        /// 恢复文件 (0 表示成功 1表示文件缺少 2表示文件操作失败)
        /// </summary>
        public int RecoverCopyAs()
        {
            try
            {
                if (!File.Exists(m_saveasPath))
                    return 1;
                FileInfo fileInfo = new FileInfo(m_saveasPath);
                fileInfo.CopyTo(m_savePath, true);
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
                return 2;
            }
            return 0;
        }
        /// <summary>
        /// 获取事件列表
        /// </summary>
        public List<EventRecord> Records
        {
            get
            {
                return m_records;
            }
        }
        /// <summary>
        /// 添加单个事件
        /// </summary>
        /// <param name="record"></param>
        public void Add(EventRecord record)
        {
            switch (record.Mode)
            {
                case UpdateMode.Insert:
                    if (m_records.Find(t => t.ID == record.ID) == null)
                    {
                        lock (m_lockObj)
                            m_records.Add(record);
                    }
                    break;
                case UpdateMode.Update:
                    EventRecord find = m_records.Find(t => t.ID == record.Key);
                    if (find == null)
                    {
                        lock (m_lockObj)
                            m_records.Add(record);
                    }
                    else
                    {
                        find.StartIndex = record.StartIndex;
                        find.EndIndex = record.EndIndex;
                        find.MarkType = record.MarkType;
                        find.ByHand = record.ByHand;
                        find.Comments = record.Comments;
                    }
                    break;
                case UpdateMode.Delete:
                    if (!record.DeleteByHand)
                        lock (m_lockObj)
                            m_records.RemoveAll(t => t.ID.Contains(record.ID));
                    else
                    {
                        EventRecord delete = m_records.Find(t => t.ID.Contains(record.ID));
                        if (delete != null)
                        {
                            delete.DeleteByHand = record.DeleteByHand;
                            delete.ByHand = record.ByHand;
                        }
                    }
                    break;
            }
            m_fresh = true;
        }
        /// <summary>
        /// 添加事件集合
        /// </summary>
        /// <param name="records"></param>
        public void AddRange(IEnumerable<EventRecord> records)
        {
            var g = records.GroupBy(t => t.Mode);
            foreach (var one in g)
            {
                List<EventRecord> items = one.ToList();
                switch (one.Key)
                {
                    case UpdateMode.Insert:
                        lock (m_lockObj)
                            m_records.AddRange(items);
                        break;
                    case UpdateMode.Update:
                        for (int i = 0; i < items.Count; i++)
                        {
                            EventRecord record = items[i];
                            EventRecord find = m_records.Find(t => t.ID == record.Key);
                            if (find == null)
                            {
                                lock (m_lockObj)
                                    m_records.Add(record);
                            }
                            else
                            {
                                find.StartIndex = record.StartIndex;
                                find.EndIndex = record.EndIndex;
                                find.MarkType = record.MarkType;
                                find.ByHand = record.ByHand;
                                find.Comments = record.Comments;
                            }
                        }
                        break;
                    case UpdateMode.Delete:
                        lock (m_lockObj)
                        {
                            var ids = items.Select(t => t.ID);
                            m_records.RemoveAll(t => ids.Contains(t.ID));
                        }
                        break;
                }
            }
            m_fresh = true;
        }
        /// <summary>
        /// 删除某一类型事件
        /// </summary>
        /// <param name="markType"></param>
        public void Delete(int markType, bool deepChange = true)
        {
            lock (m_lockObj)
                m_records.RemoveAll(t => t.MarkType == markType);
            m_fresh = deepChange;
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="records"></param>
        public void BindData(IEnumerable<EventRecord> records, int reserveTyp = -1, bool isanalysis = false)
        {
            lock (m_lockObj)
            {
                if (isanalysis)
                {
                    if (reserveTyp > 0)
                    {
                        m_records.RemoveAll(t => t.MarkType != reserveTyp && (t.MarkType < 80 || t.MarkType > 99) && !t.DeleteByHand);///这些先写死。99代表的是事件类型为None的自定义事件
                    }
                    else
                        m_records.RemoveAll(t => !t.DeleteByHand);
                    records = records.Distinct(new EventRecord.RecordComparer());
                }
                else
                {
                    m_records.Clear();
                }
                m_records.AddRange(records);
            }
            m_fresh = true;
        }
        /// <summary>
        /// 读取存储结果
        /// </summary>
        /// <returns></returns>
        public List<Doc_EventRecords> ReadResult()
        {
            return ToValue(base.ReadData());
        }
        /// <summary>
        /// 获取已删除的事件标记
        /// </summary>
        /// <returns></returns>
        public List<Doc_EventRecords> getDeleteList()
        {
            List<Doc_EventRecords> ret = new List<Doc_EventRecords>();
            foreach (EventRecord record in m_records)
            {
                if (record.DeleteByHand)
                {
                    ret.Add(new Doc_EventRecords
                    {
                        EventType = record.MarkType,
                        EndIndex = record.EndIndex,
                        StartIndex = record.StartIndex,
                        ByHandDelete = true
                    });
                }
            }
            return ret;
        }
        /// <summary>
        /// 转换成值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public List<Doc_EventRecords> ToValue(string value)
        {
            List<Doc_EventRecords> ret = new List<Doc_EventRecords>();
            string[] ss = value.Split('#');
            if (ss.Length > 1)
            {
                if (GUID == ss[0])
                {
                    string[] events = ss[1].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    ret = new List<Doc_EventRecords>(events.Length);
                    for (int i = 0; i < events.Length; i++)
                    {
                        string[] strEpoch = events[i].Split(':');
                        if (strEpoch.Length == 2)
                        {
                            string[] head = strEpoch[0].Split('/');
                            string[] values = strEpoch[1].Split('-');
                            Doc_EventRecords one = new Doc_EventRecords();
                            try
                            {
                                one.EventType = int.Parse(head[0]);
                                one.ByHand = head[1] == "0" ? false : true;
                                one.StartIndex = int.Parse(values[0]);
                                one.EndIndex = int.Parse(values[1]);
                                if (int.Parse(head[0]) == 22 && values.Length > 3)
                                {
                                    one.Tag = string.Format("{0}-{1}", values[2], values[3]);
                                }
                                else if (values.Length > 2)
                                {
                                    one.Tag = values[2];
                                }
                                if (head.Length > 2)
                                {
                                    ///删除项
                                    one.ByHandDelete = head[2] == "1";
                                }
                            }
                            catch (Exception ee)
                            {
                                Console.WriteLine(string.Format("事件内容读取转换失败：{0}", ee.Message));
                            }
                            if (!one.ByHandDelete)
                            {
                                ret.Add(one);
                            }
                            m_records.Add(new EventRecord() { StartIndex = one.StartIndex, EndIndex = one.EndIndex, MarkType = one.EventType, ByHand = one.ByHand, Comments = one.Tag });
                        }
                    }
                }
            }
            return ret;
        }
        /// <summary>
        /// 转成字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            IEnumerable<string> values = null;
            string des = "";
            lock (m_lockObj)
            {
                values = m_records.Select(t => t.ToString());
                des = string.Format("{0}#{1}", GUID, string.Join("", values));
            }
            return des;
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public override void Dispose()
        {
            if (th != null)
            {
                m_KillTask = true;
                System.Threading.Thread.Sleep(50);
                try
                {
                    th.Abort();
                    th.DisableComObjectEagerCleanup();
                }
                catch { }
            }
            SaveData();
            base.Dispose();
            m_records.Clear();
        }
        #endregion
    }
}
