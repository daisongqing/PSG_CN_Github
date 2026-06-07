using AwareTec.Polysmith.DataBaseCom;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AwareTec.Polysmith.DataMangement
{
    /// <summary>
    /// 帧信息处理类
    /// </summary>
    public class EpochsUnit : IUnit
    {
        /// <summary>
        /// 帧信息
        /// </summary>
        public class EpochInfo
        {
            private bool m_byHand = false;
            /// <summary>
            /// 是否为手动标记
            /// </summary>
            public bool ByHand { set { m_byHand = value; } get { return m_byHand; } }
            /// <summary>
            /// 索引
            /// </summary>
            public int Index { set; get; }
            /// <summary>
            /// 预留
            /// </summary>
            public string Comments { set; get; }
            /// <summary>
            /// 帧
            /// </summary>
            public Doc_Epochs Epoch
            {
                set
                {
                    Comments = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14};", value.Stage, value.SpO2, value.HeartRate, value.CA, value.OA, value.MA, value.Hypopnea, value.Chen, value.MT, value.Pos, value.SornCount, value.LightState,value.PLM,value.PLMs,value.MicArousal);
                }
            }
            /// <summary>
            /// 获取存储字符
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return string.Format("{0}/{1}:{2}", Index, m_byHand ? 1 : 0, Comments);
            }
        }
        #region 私有成员
        /// <summary>
        /// 存储事件队列表
        /// </summary>
        private List<EpochInfo> m_records = new List<EpochInfo>();
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
        /// 总帧数
        /// </summary>
        private int m_totalFrameCnt = 0;
        /// <summary>
        /// 队列锁
        /// </summary>
        private object m_lockObj = new object();
        /// <summary>
        /// 初始化完成标志
        /// </summary>
        private bool m_init = false;
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
        internal EpochsUnit()
        {
            FileName = "epochs.epc";
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="TotalFrameCnt"></param>
        public void Initialize(int TotalFrameCnt)
        {
            m_totalFrameCnt = TotalFrameCnt;
            m_records = new List<EpochInfo>(m_totalFrameCnt);
            for (int i = 0; i < m_totalFrameCnt; i++)
            {
                m_records.Add(new EpochInfo()
                {
                    Index = i,
                    ByHand = false,
                    Comments = ""
                });
            }
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
        /// 恢复文件(0 表示成功 1表示文件缺少 2表示文件操作失败)
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
        /// 帧信息集合
        /// </summary>
        public List<EpochInfo> Records
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
        public void Add(EpochInfo record)
        {
            //lock (m_lockObj)
            {
                m_records[record.Index].ByHand = record.ByHand;
                m_records[record.Index].Comments = record.Comments;
            }
            m_fresh = true;
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="records"></param>
        public void BindData(IEnumerable<EpochInfo> records)
        {
            //lock (m_lockObj)
            {
                foreach (EpochInfo record in records)
                {
                    if (record.Index < m_records.Count)
                    {
                        m_records[record.Index].ByHand = record.ByHand;
                        m_records[record.Index].Comments = record.Comments;
                    }
                }
            }
            m_fresh = true;
        }
        /// <summary>
        /// 读取存储结果
        /// </summary>
        /// <returns></returns>
        public List<Doc_Epochs> ReadResult()
        {
            return ToValue(base.ReadData());
        }

        /// <summary>
        /// 转换成值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public List<Doc_Epochs> ToValue(string value)
        {
            List<Doc_Epochs> ret = new List<Doc_Epochs>();
            string[] ss = value.Split('#');
            if (ss.Length > 1)
            {
                if (GUID == ss[0])
                {
                    string[] epochs = ss[1].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    ret = new List<Doc_Epochs>(epochs.Length);
                    for (int i = 0; i < epochs.Length; i++)
                    {
                        string[] strEpoch = epochs[i].Split(':');
                        if (strEpoch.Length == 2)
                        {
                            string[] head = strEpoch[0].Split('/');
                            string[] values = strEpoch[1].Split('|');
                            Doc_Epochs one = new Doc_Epochs();
                            try
                            {
                                one.EpochIndex = int.Parse(head[0]);
                                one.ByHand = head[1] == "0" ? false : true;
                                one.Stage = int.Parse(values[0]);
                                one.SpO2 = values[1];
                                one.HeartRate = values[2];
                                one.CA = float.Parse(values[3]);
                                one.OA = float.Parse(values[4]);
                                one.MA = float.Parse(values[5]);
                                one.Hypopnea = float.Parse(values[6]);
                                one.Chen = float.Parse(values[7]);
                                one.MT = float.Parse(values[8]);
                                one.Pos = int.Parse(values[9]);
                                one.SornCount = float.Parse(values[10]);
                                one.LightState = int.Parse(values[11]);
                                one.PLM = float.Parse(values[12]);
                                one.PLMs = float.Parse(values[13]);
                                one.MicArousal = float.Parse(values[14]);
                            }
                            catch (Exception ee)
                            {
                                Console.WriteLine(string.Format("帧内容读取转换失败：{0}", ee.Message));
                            }
                            ret.Add(one);
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
            string value = "";
            lock (m_lockObj)
            {
                values = m_records.Select(t => t.ToString());
                value = string.Join("", values);
            }
            return string.Format("{0}#{1}", GUID, value);
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
