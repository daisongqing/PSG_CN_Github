using AwareTec.Polysmith.DataBaseCom;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AwareTec.Polysmith.DataCenter
{
    /// <summary>
    /// 指标信息中心
    /// </summary>
    public class TargetCenter
    {
        #region 私有成员
        private ICommunication m_comm = null;
        private IDataBase m_DataSource = null;
        /// <summary>
        /// 终止标识
        /// </summary>
        private bool m_Interrupt = false;
        /// <summary>
        /// 当前数据标识ID
        /// </summary>
        private FileDataInfo m_CurrentInfo = null;
        /// <summary>
        /// 文件映射表
        /// </summary>
        private List<FileDataInfo> m_FileMaps = new List<FileDataInfo>();
        /// <summary>
        /// 临时存储
        /// </summary>
        private List<Doc_Epochs> bak_epochs = null;
        /// <summary>
        /// 临时存储
        /// </summary>
        private List<Doc_EventRecords> bak_eventRecords = null;
        /// <summary>
        /// 检查文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool CheckFile(string path)
        {
            return true;
            using (FileStream fsRead = new FileStream(path, FileMode.Open)) //打开文件，不能创建新的
            {
                if (fsRead.Length > MaxFileSize)
                {
                    ///根据开关灯来裁剪原始edf文件
                    ///
                    return false;
                }
                return true;
            }
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="fileName"></param>
        private byte[] ReadFile(string fileName, bool All = true)
        {
            byte[] rangeBytes;
            if (File.Exists(fileName))
            {
                int offset = 0;
                try
                {
                    //构造读取文件流对象
                    using (FileStream fsRead = new FileStream(fileName, FileMode.Open)) //打开文件，不能创建新的
                    {
                        rangeBytes = new byte[All ? fsRead.Length : 200];
                        //开辟临时缓存内存
                        byte[] byteArrayRead = new byte[All ? 1024 * 1024 : 200]; //  1字节*1024 = 1k 1k*1024 = 1M内存
                        using (BufferedStream bufferedStream = new BufferedStream(fsRead))
                        {
                            //通过死缓存去读文本中的内容
                            while (!m_Interrupt)
                            {
                                //readCount  这个是保存真正读取到的字节数
                                int readCount = bufferedStream.Read(byteArrayRead, 0, byteArrayRead.Length);
                                if (readCount > 0)
                                {
                                    Array.Copy(byteArrayRead, 0, rangeBytes, offset, readCount);
                                    offset += readCount;
                                }
                                else
                                    break;
                                if (!All)
                                {
                                    break;
                                }
                            }
                            bufferedStream.Close();
                        }
                        fsRead.Close();
                    }
                    return rangeBytes;
                }
                catch (Exception ee)
                {
                    return new byte[0];
                }
            }
            return new byte[0];
        }
        /// <summary>
        /// 数据裁剪
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        private byte[] CreatNewSource(byte[] datas)
        {
            int num = 0;
            int num2 = datas.Length - 1;
            int num3 = num + 8;
            if (num3 > num2)
            {
                return datas;
            }
            num = num3;
            num3 = num + 80;
            if (num3 > num2)
            {
                return datas;
            }
            num = num3;
            num3 = num + 80;
            if (num3 > num2)
            {
                return datas;
            }
            num = num3;
            num3 = num + 8;
            if (num3 > num2)
            {
                return datas;
            }
            int destinationIndex = num;
            string strDateTime = Encoding.ASCII.GetString(datas, num, 8);
            num = num3;
            num3 = num + 8;
            if (num3 > num2)
            {
                return datas;
            }
            string strTime = Encoding.ASCII.GetString(datas, num, 8);
            num = num3;
            num3 = num + 8;
            if (num3 > num2)
            {
                return datas;
            }
            string[] date = strDateTime.Split('.');
            int[] rts = strTime.Split(new char[] { '.', ':' }).Select(t => int.Parse(t == "00" ? "0" : t)).ToArray();
            DateTime d = DateTime.Parse(string.Format("{0} {1}:{2}:{3}", string.Format("{0}/{1}/{2}", date[2], date[1], date[0]), rts[0] > 59 ? 59 : rts[0], rts[1] > 59 ? 59 : rts[1], rts[2] > 59 ? 59 : rts[2]));
            int num4 = int.Parse(Encoding.ASCII.GetString(datas, num, 8).Trim(new char[]
            {
                ' '
            }));
            num = num3;
            num3 = num + 44;
            if (num3 > num2)
            {
                return datas;
            }
            num = num3;
            num3 = num + 8;
            if (num3 > num2)
            {
                return datas;
            }
            int destinationIndex2 = num;
            string string3 = Encoding.ASCII.GetString(datas, num, 8);
            int.Parse(string3.Trim(new char[]
            {
                ' '
            }));
            num = num3;
            num3 = num + 8;
            if (num3 > num2)
            {
                return datas;
            }
            int num5 = int.Parse(Encoding.ASCII.GetString(datas, num, 8).Trim(new char[]
            {
                ' '
            }));
            num = num3;
            num3 = num + 4;
            if (num3 > num2)
            {
                return datas;
            }
            int num6 = int.Parse(Encoding.ASCII.GetString(datas, num, 4).Trim(new char[]
            {
                ' '
            }));
            num = num3;
            int num7 = 0;
            for (int i = 0; i < num6; i++)
            {
                int num8 = 0;
                int num9 = num + 16 * i;
                string text = Encoding.ASCII.GetString(datas, num9, 16).Trim(new char[]
                {
                    ' '
                });
                if (this.m_ReplaceMap.Keys.Contains(text))
                {
                    byte[] bytes = Encoding.ASCII.GetBytes(this.m_ReplaceMap[text].PadRight(16, ' '));
                    Array.Copy(bytes, 0, datas, num9, bytes.Length);
                }
                num8 += 16 * num6;
                num8 += 80 * num6;
                num8 += 8 * num6;
                num8 += 8 * num6;
                num8 += 8 * num6;
                num8 += 8 * num6;
                num8 += 8 * num6;
                num8 += 80 * num6;
                num7 += int.Parse(Encoding.ASCII.GetString(datas, num + num8 + i * 8, 8).Trim(new char[]
                {
                    ' '
                }));
                num8 += 8 * num6;
                num8 += 32 * num6;
            }
            if (this.m_CurrentInfo.IsReEditFile)
            {
                DateTime dateTime = this.m_DataSource.StartRecordTime.AddSeconds((double)((this.m_DataSource.StartFrameNo - 1) * 30));
                DateTime d2 = this.m_DataSource.StartRecordTime.AddSeconds((double)(this.m_DataSource.EndFrameNo * 30));
                string s = string.Format("{0}{1}", dateTime.ToString("dd.MM.yy"), dateTime.ToString("HH.mm.ss"));
                byte[] bytes2 = Encoding.ASCII.GetBytes(s);
                Array.Copy(bytes2, 0, datas, destinationIndex, 16);
                int num10 = (int)(dateTime - d).TotalSeconds;
                int num11 = (int)(d2 - dateTime).TotalSeconds;
                int num12 = num11 / num5;
                byte[] bytes3 = Encoding.ASCII.GetBytes(num12.ToString().PadRight(8, ' '));
                Array.Copy(bytes3, 0, datas, destinationIndex2, 8);
                int num13 = num4 + num10 * num7 * 2;
                int num14 = num7 * 2 * num11;
                while (num13 + num14 > datas.Length)
                {
                    num14 -= num7 * 2;
                }
                byte[] array2 = new byte[num14];
                Array.Copy(datas, num13, array2, 0, array2.Length);
                byte[] array3 = new byte[num4 + array2.Length];
                Array.Copy(datas, 0, array3, 0, num4);
                Array.Copy(array2, 0, array3, num4, array2.Length);
                datas = array3;
            }
            return datas;
        }
        /// <summary>
        /// 需要更改通道名称的映射表
        /// </summary>
        private Dictionary<string, string> m_ReplaceMap = new Dictionary<string, string>();
        /// <summary>
        /// 比较器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private class DistinctBy<T> : IEqualityComparer<T> where T : Doc_EventRecords
        {

            public bool Equals(T x, T y)
            {
                return x.StartIndex == y.StartIndex && x.EndIndex == y.EndIndex && x.EventType == y.EventType;
            }

            public int GetHashCode(T obj)
            {
                return string.Format("{0}:{1}-{2}", obj.EventType, obj.StartIndex, obj.EndIndex).GetHashCode();
            }
        }
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Comm"></param>
        public TargetCenter(ICommunication Comm)
        {
            m_comm = Comm;
        }
        #region 公有成员
        /// <summary>
        /// 设置文件可处理的最大容量
        /// 默认800M
        /// </summary>
        public float MaxFileSize = 1024 * 1024 * 800;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="filePath">edf路径</param>
        /// <param name="recordTime">记录时间</param>
        /// <param name="LightOffTime">关灯时间</param>
        /// <param name="LightOnTime">开灯时间</param>
        public bool Init(string guid, string filePath, DateTime recordTime, DateTime LightOffTime, DateTime LightOnTime, bool isBreathOnly = false)
        {
            if ((LightOnTime - LightOffTime).TotalHours > 24.0)
            {
                return false;
            }
            m_DataSource = new IDataBase();
            this.m_DataSource.StartRecordTime = recordTime;
            this.m_DataSource.ValidStartTime = LightOffTime;
            this.m_DataSource.ValidEndTime = LightOnTime;
            this.m_DataSource.IsBreathOnly = isBreathOnly;
            this.m_CurrentInfo = this.m_FileMaps.Find((FileDataInfo t) => t.ID == guid);
            if (this.m_CurrentInfo == null)
            {
                FileDataInfo fileDataInfo = new FileDataInfo();
                fileDataInfo.ID = guid;
                fileDataInfo.FileName = filePath;
                fileDataInfo.IsFirstLoad = true;
                this.m_FileMaps.Add(fileDataInfo);
                this.m_CurrentInfo = fileDataInfo;
            }
            if (string.IsNullOrEmpty(this.m_CurrentInfo.CommunicationID) && this.FileHashCodeCreating != null)
            {
                this.m_CurrentInfo.CommunicationID = this.FileHashCodeCreating(this.m_CurrentInfo.FileName, null);
            }
            if (this.m_NewFileCreating != null)
            {
                this.m_ReplaceMap = this.m_NewFileCreating();
            }
            this.m_comm.ID = this.m_CurrentInfo.CommunicationID;
            return true;
        }
        /// <summary>
        /// 判断原始数据在算法中心是否已加载
        /// </summary>
        /// <returns>返回当前指令是否成功执行</returns>
        public bool upLoadDataReady(out bool IsReady)
        {
            if (!this.CheckFile(this.m_CurrentInfo.FileName))
            {
                this.m_CurrentInfo.IsReEditFile = true;
                if ((this.m_DataSource.StartFrameNo != this.m_CurrentInfo.ValidStartIndex && this.m_CurrentInfo.ValidStartIndex != 0) || (this.m_CurrentInfo.ValidEndIndex != this.m_DataSource.EndFrameNo && this.m_CurrentInfo.ValidEndIndex != 0))
                {
                    this.m_CurrentInfo.ValidStartIndex = this.m_DataSource.StartFrameNo;
                    this.m_CurrentInfo.ValidEndIndex = this.m_DataSource.EndFrameNo;
                    if (!this.m_CurrentInfo.IsFirstLoad)
                    {
                        this.m_comm.DeleteDataSource();
                        IsReady = false;
                        return true;
                    }
                }
            }
            IsReady = this.m_comm.ExistDataSource();
            return this.m_comm.IsConnected;
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        public bool upLoadData(byte[] data)
        {
            if (this.m_comm.IsConnected)
            {
                if (this.m_CurrentInfo.IsFirstLoad)
                {
                    this.m_CurrentInfo.IsFirstLoad = false;
                }
                if (this.m_ReplaceMap.Count > 0 || this.m_CurrentInfo.IsReEditFile)
                {
                    data = this.CreatNewSource(data);
                    if (this.FileHashCodeCreating != null)
                    {
                        this.m_CurrentInfo.CommunicationID = this.FileHashCodeCreating("", data);
                    }
                    this.m_comm.ID = this.m_CurrentInfo.CommunicationID;
                }
                return this.m_comm.UpLoadDataSouce(data);
            }
            return false;
        }
        /// <summary>
        /// 提交分析条件
        /// </summary>
        public bool SubmitAnalysisConditions(InConditions Conditions)
        {
            this.m_CurrentInfo.IsAutoAnalysis = Conditions.AnalysisStateWord!=0;
            this.bak_epochs = Conditions.Epochs;
            List<Doc_EventRecords> eventRecords2 = Conditions.EventRecords.Distinct(new Doc_EventRecords.Distinct<Doc_EventRecords>()).ToList<Doc_EventRecords>();
            if (this.m_CurrentInfo.IsAutoAnalysis)
            {
                if (!m_comm.IsConnected)
                    return false;
                InConditions inConditions = new InConditions
                {
                    LightOffTime = this.m_DataSource.ValidStartTime,
                    LightOnTime = this.m_DataSource.ValidEndTime,
                    StartTime = this.m_DataSource.StartRecordTime,
                    EdfPath = m_CurrentInfo.FileName,
                    GUID = m_CurrentInfo.ID,
                    AnalysisStateWord=Conditions.AnalysisStateWord,
                    IsBreathOnly= this.m_DataSource.IsBreathOnly
                };
                inConditions.EventRecords = eventRecords2;
                if (Conditions.Epochs.Count > 0)
                {
                    for (int i = 0; i < Conditions.Epochs.Count; i++)
                    {
                        inConditions.Epochs.Add(this.m_DataSource.IsBreathOnly ? new Doc_Epochs
                        {
                            GUID = Conditions.Epochs[i].GUID,
                            HeartRate = Conditions.Epochs[i].HeartRate,
                            SpO2 = Conditions.Epochs[i].SpO2,
                            EpochIndex = Conditions.Epochs[i].EpochIndex,
                            Pos = Conditions.Epochs[i].Pos,
                            Stage = 4
                        } : Conditions.Epochs[i]);
                    }
                }
                return this.m_comm.SubmitAnalysisConditions(inConditions);
            }
            this.bak_eventRecords = eventRecords2.Where(t => !t.ByHandDelete).ToList(); ///是被删除的事件不应该进入指标计算部分
            return this.m_DataSource.Init(this.bak_epochs, bak_eventRecords);
        }
        /// <summary>
        /// 提交分析条件
        /// </summary>
        public bool SubmitAnalysisConditions(List<Doc_Epochs> epochs, List<Doc_EventRecords> eventRecords, bool autoAnalysis = true)
        {
            this.m_CurrentInfo.IsAutoAnalysis = autoAnalysis;
            this.bak_epochs = epochs;
            List<Doc_EventRecords> eventRecords2 = eventRecords.Distinct(new Doc_EventRecords.Distinct<Doc_EventRecords>()).ToList<Doc_EventRecords>();
            if (autoAnalysis)
            {
                if (!m_comm.IsConnected)
                    return false;
                InConditions inConditions = new InConditions
                {
                    LightOffTime = this.m_DataSource.ValidStartTime,
                    LightOnTime = this.m_DataSource.ValidEndTime,
                    StartTime = this.m_DataSource.StartRecordTime,
                    EdfPath = m_CurrentInfo.FileName,
                    GUID = m_CurrentInfo.ID
                };
                if (this.m_CurrentInfo.IsReEditFile)
                {
                    inConditions.Epochs = new List<Doc_Epochs>(this.m_DataSource.EndFrameNo - this.m_DataSource.StartFrameNo + 1);
                    if (epochs.Count > 0)
                    {
                        for (int i = this.m_DataSource.StartFrameNo - 1; i < this.m_DataSource.EndFrameNo; i++)
                        {
                            inConditions.Epochs.Add(this.m_DataSource.IsBreathOnly ? new Doc_Epochs
                            {
                                GUID = epochs[i].GUID,
                                HeartRate = epochs[i].HeartRate,
                                SpO2 = epochs[i].SpO2,
                                EpochIndex = epochs[i].EpochIndex,
                                Pos = epochs[i].Pos,
                                Stage = 4
                            } : epochs[i]);
                        }
                    }
                    inConditions.EventRecords = new List<Doc_EventRecords>();
                    IEnumerable<IGrouping<int, Doc_EventRecords>> enumerable = from t in eventRecords
                                                                               group t by t.EventType;
                    if (enumerable.Count<IGrouping<int, Doc_EventRecords>>() <= 0)
                    {
                        goto IL_2F1;
                    }
                    using (IEnumerator<IGrouping<int, Doc_EventRecords>> enumerator = enumerable.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            IGrouping<int, Doc_EventRecords> grouping = enumerator.Current;
                            List<Doc_EventRecords> list = grouping.ToList<Doc_EventRecords>();
                            try
                            {
                                Enum.Parse(typeof(EventTypeEnum), grouping.Key.ToString(), true);
                                int count = list.Count;
                                if (count > 0)
                                {
                                    int tspan = 1000 / list[0].TimeSpan;
                                    if (tspan > 0)
                                    {
                                        int num = (this.m_DataSource.StartFrameNo - 1) * 30 * tspan;
                                        int num2 = this.m_DataSource.EndFrameNo * 30 * tspan;
                                        for (int j = 0; j < list.Count; j++)
                                        {
                                            Doc_EventRecords doc_EventRecords = list[j];
                                            if (doc_EventRecords.StartIndex <= num2)
                                            {
                                                if (doc_EventRecords.EndIndex > num2)
                                                {
                                                    doc_EventRecords.EndIndex = num2;
                                                }
                                                doc_EventRecords.StartIndex -= num;
                                                doc_EventRecords.EndIndex -= num;
                                                if (doc_EventRecords.EndIndex >= 0)
                                                {
                                                    if (doc_EventRecords.StartIndex < 0)
                                                    {
                                                        doc_EventRecords.StartIndex = 0;
                                                    }
                                                    inConditions.EventRecords.Add(doc_EventRecords);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                        goto IL_2F1;
                    }
                }
                inConditions.EventRecords = eventRecords2;
                if (epochs.Count > 0)
                {
                    for (int i = 0; i < epochs.Count; i++)
                    {
                        inConditions.Epochs.Add(this.m_DataSource.IsBreathOnly ? new Doc_Epochs
                        {
                            GUID = epochs[i].GUID,
                            HeartRate = epochs[i].HeartRate,
                            SpO2 = epochs[i].SpO2,
                            EpochIndex = epochs[i].EpochIndex,
                            Pos = epochs[i].Pos,
                            Stage = 4
                        } : epochs[i]);
                    }
                }
            IL_2F1:
                return this.m_comm.SubmitAnalysisConditions(inConditions);
            }
            this.bak_eventRecords = eventRecords2.Where(t => !t.ByHandDelete).ToList(); ///是被删除的事件不应该进入指标计算部分
            return this.m_DataSource.Init(this.bak_epochs, bak_eventRecords);
        }

        /// <summary>
        /// 获取分析结果
        /// </summary>
        public OutResult getResult(Doc_SystemSetting setting)
        {
            OutResult outResult = new OutResult();          
            if (this.m_CurrentInfo.IsAutoAnalysis)
            {
                outResult = this.m_comm.ReadAnalysiResult(this.m_CurrentInfo.ID);
                if (outResult.Interrupt)
                {
                    return outResult;
                }
                if (outResult.Epochs.Count == 0)
                {
                    outResult.Epochs = bak_epochs;
                }
                else
                {
                    string[] ss = outResult.Epochs[0].SpO2.Split(',');
                    if (ss.Length < 4)
                    {
                        if (bak_epochs[0].SpO2.Split(',').Length > 3)
                        {
                            for (int i = 0; i < outResult.Epochs.Count; i++)
                            {
                                outResult.Epochs[i].SpO2 = bak_epochs[outResult.Epochs[i].EpochIndex].SpO2;
                            }
                        }
                    }
                }
                outResult.EventRecords = outResult.EventRecords.Distinct(new Doc_EventRecords.Distinct<Doc_EventRecords>()).ToList<Doc_EventRecords>();
                if (this.m_CurrentInfo.IsReEditFile)
                {
                    int count = this.bak_epochs.Count;
                    List<Doc_Epochs> list = new List<Doc_Epochs>(count);
                    int num = 0;
                    int num2 = this.m_DataSource.StartFrameNo - 1;
                    for (int i = 0; i < count; i++)
                    {
                        Doc_Epochs doc_Epochs;
                        if (i < num2)
                        {
                            doc_Epochs = this.bak_epochs[i];
                            if (doc_Epochs.SpO2 == "")
                            {
                                doc_Epochs.HeartRate = (doc_Epochs.SpO2 = "0,0,0,0,0");
                            }
                        }
                        else if (i >= this.m_DataSource.EndFrameNo)
                        {
                            doc_Epochs = this.bak_epochs[i];
                            if (doc_Epochs.SpO2 == "")
                            {
                                doc_Epochs.HeartRate = (doc_Epochs.SpO2 = "0,0,0,0,0");
                            }
                        }
                        else
                        {
                            doc_Epochs = outResult.Epochs[num++];
                            if (this.m_DataSource.IsBreathOnly)
                            {
                                doc_Epochs.Stage = this.bak_epochs[i].Stage;
                            }
                        }
                        doc_Epochs.GUID = this.m_CurrentInfo.ID;
                        doc_Epochs.EpochIndex = i;
                        list.Add(doc_Epochs);
                    }
                    outResult.Epochs = list;
                    List<Doc_EventRecords> list2 = new List<Doc_EventRecords>();
                    IEnumerable<IGrouping<int, Doc_EventRecords>> enumerable = from t in outResult.EventRecords
                                                                               group t by t.EventType;
                    if (enumerable.Count<IGrouping<int, Doc_EventRecords>>() > 0)
                    {
                        foreach (IGrouping<int, Doc_EventRecords> grouping in enumerable)
                        {
                            List<Doc_EventRecords> list3 = grouping.ToList<Doc_EventRecords>();
                            try
                            {
                                Enum.Parse(typeof(EventTypeEnum), grouping.Key.ToString(), true);
                                int count2 = list3.Count;
                                if (count2 > 0)
                                {
                                    int num3 = (this.m_DataSource.StartFrameNo - 1) * 30 * (1000 / list3[0].TimeSpan);
                                    int num4 = list3[0].TimeSpan;
                                    bool flag = grouping.Key == 6;
                                    for (int j = 0; j < list3.Count; j++)
                                    {
                                        Doc_EventRecords doc_EventRecords = list3[j];
                                        doc_EventRecords.StartIndex += num3;
                                        doc_EventRecords.EndIndex += num3;
                                        doc_EventRecords.StartTime = this.m_DataSource.StartRecordTime.AddMilliseconds((double)(doc_EventRecords.StartIndex * num4));
                                        doc_EventRecords.EndTime = this.m_DataSource.StartRecordTime.AddMilliseconds((double)(doc_EventRecords.EndIndex * num4));
                                        if (flag)
                                        {
                                            doc_EventRecords.Tag = this.m_comm.GoExtraTask(new object[]
                                            {
                                                17,
                                                doc_EventRecords.StartIndex,
                                                doc_EventRecords.EndIndex
                                            }).ToString();
                                        }
                                        list2.Add(doc_EventRecords);
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                this.m_DataSource.Init(outResult.Epochs, outResult.EventRecords.Distinct(new Doc_EventRecords.Distinct<Doc_EventRecords>()).ToList<Doc_EventRecords>());
                this.bak_eventRecords = outResult.EventRecords;
                this.bak_epochs = outResult.Epochs;
            }
            outResult.Sleep = new SleepResult(this.m_DataSource).getReuslt();
            outResult.Sleep.LightOffFrameNo = m_DataSource.StartFrameNo;
            outResult.Sleep.LightOnFrameNo = m_DataSource.EndFrameNo;
            outResult.Sleep.GUID = this.m_CurrentInfo.ID;
            outResult.BloodOxygen = new Spo2Result(this.m_DataSource).getResult(setting);
            outResult.BloodOxygen.GUID = this.m_CurrentInfo.ID;
            outResult.BodyMovement = new LegsMoveResult(this.m_DataSource).getResult();
            outResult.BodyMovement.GUID = this.m_CurrentInfo.ID;
            outResult.BodyState = new BodyPosResult(this.m_DataSource).getResult();
            outResult.BodyState.GUID = this.m_CurrentInfo.ID;
            BreathResult breathResult = new BreathResult(this.m_DataSource);
            outResult.BreathEvent = breathResult.getResult(setting);
            outResult.BreathEvent.GUID = this.m_CurrentInfo.ID;
            outResult.BloodOxygen.MinApneaBloodOxygen = breathResult.minSpO2ValueByOA;
            outResult.BloodOxygen.MinAllApneaBloodOxygen = breathResult.minSpO2ValueByApnea;
            outResult.BloodOxygen.MinHypopneaBloodOxygen = breathResult.minSpO2ValueByHypopnea;
            outResult.HeartRate = new HeartResult(this.m_DataSource).getResult();
            outResult.HeartRate.GUID = this.m_CurrentInfo.ID;
            outResult.Molar= new MolarResult(this.m_DataSource).getResult();
            outResult.Molar.GUID = this.m_CurrentInfo.ID;
            outResult.LightOffTime = this.m_DataSource.ValidStartTime;
            outResult.LightOnTime = this.m_DataSource.ValidEndTime;
            outResult.Epochs = new List<Doc_Epochs>(this.bak_epochs);
            outResult.EventRecords = new List<Doc_EventRecords>(this.bak_eventRecords);
            outResult.GUID = this.m_CurrentInfo.ID;
            this.bak_epochs.Clear();
            this.bak_eventRecords.Clear();
            m_DataSource.Dispose();
            return outResult;
        }     

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            this.FileHashCodeCreating = null;
            this.m_FileMaps.Clear();
            this.m_ReplaceMap.Clear();
            if (this.bak_epochs != null)
            {
                this.bak_epochs.Clear();
            }
            if (this.bak_eventRecords != null)
            {
                this.bak_eventRecords.Clear();
            }
            m_comm.Dispose();
        }
        #endregion
        #region 定义事件
        public delegate string FileHashCodeCreatingDelegate(string filePath, byte[] data = null);
        /// <summary>
        /// 创建文件哈希值时触发
        /// 以filePath为主，data为辅，当data不为null时，由data生成哈希值
        /// </summary>
        public event FileHashCodeCreatingDelegate FileHashCodeCreating;

        public delegate Dictionary<string, string> NewFileCreatingDelegate();
        private event NewFileCreatingDelegate m_NewFileCreating;
        /// <summary>
        /// 
        /// </summary>
        public event NewFileCreatingDelegate NewFileCreating
        {
            add
            {
                if (m_NewFileCreating != null)
                    m_NewFileCreating = null;
                m_NewFileCreating = value;
            }
            remove
            {
                m_NewFileCreating = null;
            }
        }
        #endregion

        #region 计算指标

        #endregion

    }  
}
