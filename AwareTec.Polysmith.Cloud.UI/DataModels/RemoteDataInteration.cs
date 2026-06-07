using AwareTec.Polysmith.Cloud.UI.DataModels.Global;
using AwareTec.Polysmith.Cloud.UI.Forms.Tips;
using AwareTec.Polysmith.DataBaseCom;
using AwareTec.Polysmith.DataCenter;
using Newtonsoft.Json;
using pSystem.Communication.Com;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.DataModels
{
    public class RemoteDataInteration : ICommunication
    {
        #region 私有成员
        /// <summary>
        /// 物理通讯对象
        /// </summary>
        private IOClient m_Client = null;
        /// <summary>
        /// 通讯端口
        /// </summary>
        private int m_PortID = 5000;
        /// <summary>
        /// 帧集合
        /// </summary>
        private List<Doc_Epochs> m_EpochsList = new List<Doc_Epochs>();
        private Doc_Epochs[] m_EpochsArray = new Doc_Epochs[0];
        /// <summary>
        /// 帧信息读取完成标志
        /// </summary>
        private bool m_epochReadComplete = false;
        /// <summary>
        /// 事件信息读取完成标志
        /// </summary>
        private bool m_eventsReadComplete = false;
        private bool m_epochBaseReadCompplete = false;
        /// <summary>
        /// 事件集合
        /// </summary>
        private List<Doc_EventRecords> m_EventReocrds = new List<Doc_EventRecords>();
        private bool m_AnalysisComplete = false;
        private string m_guid = "";
        /// <summary>
        /// edf的开始记录时间
        /// </summary>
        private DateTime m_startTime = default(DateTime);
        /// <summary>
        /// 频谱存放路径
        /// </summary>
        private string m_spectrumPath = "";
        /// <summary>
        /// 是否是初筛
        /// </summary>
        private bool m_isBreath = false;
        /// <summary>
        /// 输入内容
        /// </summary>
        private class JsonObj
        {
            public string edf = "";
            public bool base_info_only = false;
            public string resp_config = "* 300/*/*";//最小持续时间 最大持续时间/氧减阈值/分析数据源通道ID 如需默认可置*
            public string leg_config = "0.5/*";
            public string action_config = "10/*";
            public string snore_config = "*";
            public string arousal_config = "*/*";
            public string light_time = "";
            public string stage = "1/*/";
            public string events = "";
            public string autoanalysis = "";
            public bool close = false;
            public string deleted_events = "";
            /// <summary>
            /// 是否启用频谱分析
            /// </summary>
            public bool spectrumenable = false;
        }
        /// <summary>
        /// 输出内容
        /// </summary>
        private class SendObj
        {
            /// <summary>
            /// 数据类型
            /// </summary>
            public string type = "";
            /// <summary>
            /// 数据长度
            /// </summary>
            public string length = "";
            /// <summary>
            /// 当前包序号/总包数量
            /// </summary>
            public string segment = "";
            /// <summary>
            /// 数据段
            /// </summary>
            public string data = "";
        }

        /// <summary>
        /// 信息交互的数据类型
        /// </summary>
        private enum DataType
        {
            /// <summary>
            /// 信息交互
            /// </summary>
            Comfirm = 0x00,
            /// <summary>
            /// 事件回传
            /// </summary>
            EventInfo = 0x01,
            /// <summary>
            /// 基础信息回传
            /// </summary>
            BaseInfo = 0x02,
            //帧信息
            EpochsInfo = 0x03,
            /// <summary>
            /// 应答
            /// </summary>
            Respone = 0x80,
            /// <summary>
            /// 停止分析
            /// </summary>
            AnalysisStop = 0x81,
            /// <summary>
            /// 清除资源数据
            /// </summary>
            ClearDatasource = 0x82,
            /// <summary>
            /// 频谱分析完成消息互传
            /// </summary>
            SpectrumReady = 0x83

        }
        private string m_recDataString = "";
        private bool m_responeComplete = false;
        /// <summary>
        /// 获取帧信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private void getEpochsInfo(string value, string guid)
        {
            bool add = m_EpochsArray.Length == 0;
            string[] info = value.Split(';');
            for (int i = 0; i < info.Length; i++)
            {
                string[] values = info[i].Split(',');
                if (values.Length >= 7)
                {
                    string pos = values[3];
                    int epIdx = int.Parse(values[0]);
                    Doc_Epochs epoch = add ? new Doc_Epochs() : m_EpochsArray[epIdx];
                    epoch.EpochIndex = epIdx;
                    epoch.HeartRate = values[1].Replace(' ', ',');
                    epoch.SpO2 = values[2].Replace(' ', ',');
                    epoch.Pos = pos == "S" ? 1 : pos == "P" ? 4 : pos == "UP" ? 5 : pos == "R" ? 3 : pos == "L" ? 2 : -1;
                    epoch.MT = float.Parse(values[4]);
                    epoch.LightState = values[5] == "ON" ? 1 : 0;
                    epoch.SornCount = float.Parse(values[6]);
                    epoch.GUID = guid;
                    if (add)
                        m_EpochsList.Add(epoch);

                }
            }
            if (add)
                m_EpochsArray = m_EpochsList.ToArray();
        }
        /// <summary>
        /// 获取帧信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private void getAnalysisEpochInfo(string value, string guid)
        {
            bool add = m_EpochsArray.Length == 0;
            string[] info = value.Split(';');
            for (int i = 0; i < info.Length; i++)
            {
                string[] values = info[i].Split(',');
                if (values.Length >= 7)
                {
                    ///新增睡眠分期AI自动判定还是手动标记 2021.08.26
                    string[] stage = values[1].Split(new char[] { '%' }, StringSplitOptions.RemoveEmptyEntries);///算法里的值为0、1、2、3、4（w\n1\n2\n3\r），软件中对应是5,3,2,1,4
                    int epIdx = int.Parse(values[0]);
                    Doc_Epochs epoch = add ? new Doc_Epochs() : m_EpochsArray[epIdx];
                    epoch.EpochIndex = epIdx;
                    epoch.Stage = m_isBreath ? 4 : (stage[0] == "W" ? 5 : stage[0] == "R" ? 4 : stage[0] == "N3" ? 1 : stage[0] == "N1" ? 3 : stage[0] == "N2" ? 2 : -1);
                    epoch.CA = float.Parse(values[2]);
                    epoch.OA = float.Parse(values[3]);
                    epoch.MA = float.Parse(values[4]);
                    epoch.Hypopnea = float.Parse(values[5]);
                    epoch.Chen = float.Parse(values[6]);
                    epoch.SornCount = float.Parse(values[7]);
                    if (values.Length >= 9)
                    {
                        epoch.PLM = float.Parse(values[8]);
                        epoch.PLMs = float.Parse(values[9]);
                    }
                    if (values.Length >= 14)
                        epoch.MicArousal = float.Parse(values[10]) + float.Parse(values[11]) + float.Parse(values[12]) + float.Parse(values[13]);
                    epoch.GUID = guid;
                    if (stage.Length > 1)
                        epoch.ByHand = stage[1] == "1";
                    if (add)
                        m_EpochsList.Add(epoch);
                }
            }
            if (add)
                m_EpochsArray = m_EpochsList.ToArray();
        }
        /// <summary>
        /// 获取事件
        /// </summary>
        /// <param name="value"></param>
        /// <param name="guid"></param>
        public void getEvents(string value, string guid)
        {
            string[] strRec = value.Split(new char[] { '%' }, StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < strRec.Length; j++)
            {
                string[] ss = strRec[j].Split(new char[] { ':', '/' }, StringSplitOptions.RemoveEmptyEntries);
                if (ss.Length == 3)
                {
                    int[] ss2 = ss[0].Split(',').Select(t => int.Parse(t)).ToArray();
                    string[] records = ss[2].Split(';');
                    int typ = int.Parse(ss[1]);
                    if (typ == (int)pChart.IMarker.MarkType.Kcomplex)
                        continue;
                    for (int i = 0; i < records.Length; i++)
                    {
                        string[] stridx = records[i].Split('-');
                        int sindex = int.Parse(stridx[0]);
                        int eindex = int.Parse(stridx[1]);
                        m_EventReocrds.Add(new Doc_EventRecords()
                        {
                            StartIndex = sindex,
                            EndIndex = eindex,
                            EventType = typ,
                            Tag = stridx.Length == 3 ? stridx[2] : "",
                            GUID = guid
                        });
                    }
                }
            }
        }
        /// <summary>
        /// 写入条件
        /// </summary>
        /// <returns></returns>
        private bool ComitCondition(JsonObj data)
        {
            System.Threading.Thread.Sleep(1000);
            return SendData(JsonConvert.SerializeObject(data));
        }
        #region 链路数据交互

        /// <summary>
        /// 数据发送
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool SendData(string data)
        {
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("发送到AI：{0}", data), pSystem.LogManagement.LogLevel.FATAL);
            if (m_Client.IsConnected)
            {
                int maxlenght = 1000;
                int len = data.Length;
                int totalCnt = len % maxlenght == 0 ? len / maxlenght : len / maxlenght + 1;
                int TmpIndex = 1;
                while (len > 0)///尝试分包发送
                {
                    if (len > maxlenght)
                    {
                        len = maxlenght;
                    }
                    string value = data.Substring(0, len);
                    int cnt = 0;
                    m_responeComplete = false;
                    ///组件发送内容
                    SendObj send = new SendObj();
                    send.type = ((int)(DataType.Respone)).ToString();
                    send.length = len.ToString();
                    send.segment = string.Format("{0}/{1}", TmpIndex++, totalCnt);
                    send.data = value;
                    value = JsonConvert.SerializeObject(send);
                    byte[] databytes = System.Text.Encoding.UTF8.GetBytes(value);
                    do
                    {
                        cnt++;
                        m_Client.DataSend(databytes);
                        for (int i = 0; i < 100; i++)
                        {
                            System.Threading.Thread.Sleep(10);
                            if (m_responeComplete)
                                break;
                        }
                    } while (cnt < 4 && !m_responeComplete);
                    if (!m_responeComplete)
                    {
                        return false;
                    }
                    data = data.Remove(0, len);
                    len = data.Length;
                }
                return true;
            }
            return false;
        }

        private bool SendDataNoResponse(DataType typ, string data = "")
        {
            if (m_Client.IsConnected)
            {
                int len = data.Length;
                ///组件发送内容
                SendObj send = new SendObj();
                send.type = ((int)typ).ToString();
                send.length = len.ToString();
                send.segment = string.Format("{0}/{1}", 1, 1);
                send.data = data;
                string value = JsonConvert.SerializeObject(send);
                byte[] databytes = System.Text.Encoding.UTF8.GetBytes(value);

                m_Client.DataSend(databytes);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 数据接收
        /// </summary>
        /// <param name="data"></param>
        /// <param name="portClient"></param>
        private void m_Clinet_DataReceiveHandle(byte[] data, IOClient portClient)
        {
            string value = System.Text.Encoding.UTF8.GetString(data);
            try
            {
                Console.WriteLine(string.Format("{0:yyyy-MM-dd HH:mm:ss ff} [接收]:{1}", DateTime.Now, value));
                if (data.Length < 10)
                    return;
                SendObj recdata = JsonConvert.DeserializeObject<SendObj>(value);
                DataType typ = (DataType)Enum.Parse(typeof(DataType), recdata.type);
                if (typ == DataType.Respone)
                {
                    m_responeComplete = true;
                    return;
                }
                string[] idxs = recdata.segment.Split('/');
                m_recDataString = string.Format("{0}{1}", m_recDataString, recdata.data);
                if (idxs[0] != idxs[1])
                {
                    recdata.data = "";
                    m_Client.DataSend(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(recdata)));
                    return;
                }
                switch (typ)
                {
                    case DataType.BaseInfo:
                        DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("数据接受 {0}", m_recDataString), pSystem.LogManagement.LogLevel.FATAL);
                        getEpochsInfo(m_recDataString, m_guid);
                        m_epochBaseReadCompplete = true;
                        break;
                    case DataType.EventInfo:
                        DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("数据接受 {0}", m_recDataString), pSystem.LogManagement.LogLevel.FATAL);
                        getEvents(m_recDataString, m_guid);
                        m_eventsReadComplete = true;
                        break;
                    case DataType.EpochsInfo:
                        DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("数据接受 {0}", m_recDataString), pSystem.LogManagement.LogLevel.FATAL);
                        getAnalysisEpochInfo(m_recDataString, m_guid);
                        m_epochReadComplete = true;
                        break;

                    case DataType.Comfirm:
                        if (recdata.data == "1")
                        {
                            m_AnalysisComplete = true;
                        }
                        m_recDataString = "";
                        recdata.data = "";
                        return;
                    case DataType.SpectrumReady:
                        if (m_SpectrumReadyHandle != null)
                        {
                            m_SpectrumReadyHandle.BeginInvoke(m_spectrumPath, null, null);
                        }
                        break;
                }
                m_recDataString = "";
                recdata.data = "";
                m_Client.DataSend(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(recdata)));
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
        }
        #endregion
        #endregion
        #region 公有成员
        public RemoteDataInteration()
        {
            m_AnalysisExePath = string.Format("{0}AnalysisSource\\{1}.exe", AppDomain.CurrentDomain.BaseDirectory, m_analysisName);
            m_spectrumPath = string.Format("{0}Spectrum\\map", AppDomain.CurrentDomain.BaseDirectory, m_analysisName);
        }
        #endregion

        #region 实现ICommunication接口
        private string HashCode = "";
        public string ID
        {
            set
            {
                HashCode = value;
            }
            get
            {
                return HashCode;
            }
        }
        /// <summary>
        /// 分析终止
        /// </summary>
        /// <returns></returns>
        public bool AnalysisStop()
        {
            return SendDataNoResponse(DataType.AnalysisStop); ;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public bool Init()
        {
            try
            {
                //string m_ip = "10.1.12.212";// ;
                string m_ip = "127.0.0.1";
                if (m_ip == "")
                {
                    IPAddress[] addres = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName());
                    for (int i = 0; i < addres.Length; i++)
                    {
                        if (addres[i].AddressFamily.ToString() == "InterNetwork")
                        {
                            m_ip = addres[i].ToString();
                            break;
                        }
                    }
                }
                m_Client = new MyTCPClient(m_ip, m_PortID);
                m_Client.DataReceiveHandle += m_Clinet_DataReceiveHandle;
                Task.Factory.StartNew(() =>
                {
                    RunAnalysisExe();
                    if (m_Client.IsConnected)
                        m_Client.Close();
                    m_Client.Open();
                    m_KillTask = false;
                    TaskStart();
                });
                return true;
            }
            catch { }
            return false;
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        /// <returns></returns>
        public bool Dispose()
        {
            m_KillTask = true;
            try
            {
                if (th == null)
                    return true;
                th.Abort();
                th.DisableComObjectEagerCleanup();
            }
            catch { }
            try
            {
                if (m_Client.IsConnected)
                    m_Client.Close();
            }
            catch { }
            try
            {
                Process[] p = Process.GetProcessesByName(m_analysisName);
                if (p.Length > 0)
                    p[0].Kill();
            }
            catch { }
            return true;
        }
        /// <summary>
        /// 连接是否正常
        /// </summary>
        public bool IsConnected { get { return m_Client.IsConnected; } }
        /// <summary>
        /// 执行附加任务
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public object GoExtraTask(object[] args)
        {
            if (this.m_ExtraTaskRunningHandler != null)
                return this.m_ExtraTaskRunningHandler(args);
            return (object)"";
        }
        /// <summary>
        /// 准备分析前的必要数据
        /// </summary>
        /// <returns></returns>
        public bool SubmitAnalysisConditions(InConditions Conditions)
        {
            int analysisStateWord = Conditions.AnalysisStateWord;
            m_epochReadComplete = false;
            m_epochBaseReadCompplete = false;
            m_eventsReadComplete = (1 << (int)pChart.IMarker.MarkType.Stage) == analysisStateWord;
            m_EventReocrds.Clear();
            m_EpochsList.Clear();
            m_EpochsArray = new Doc_Epochs[0];
            m_startTime = Conditions.StartTime;
            m_isBreath = Conditions.IsBreathOnly;
            string strDelete = "";
            StringBuilder sb = new StringBuilder();
            var g = Conditions.EventRecords.GroupBy(t => t.EventType);
            foreach (var item in g)
            {
                if (item.Key < 50 && item.Key != (int)pChart.IMarker.MarkType.MultipleSleep)
                {
                    List<Doc_EventRecords> records = item.ToList();
                    var vaildList = records.Where(t => !t.ByHandDelete);
                    int cnt = vaildList.Count();
                    if (cnt > 0)
                        sb.AppendFormat("{0}:{1}%", item.Key, string.Join(";", vaildList.Select(t => string.Format("{0}-{1}", t.StartIndex, t.EndIndex))));
                    if (cnt != records.Count)///说明有删除事件
                    {
                        vaildList = records.Where(t => t.ByHandDelete);
                        strDelete = string.Format("{2}{0}:{1}%", item.Key, string.Join(";", vaildList.Select(t => string.Format("{0}-{1}", t.StartIndex, t.EndIndex))), strDelete);
                    }
                }
            }
            strDelete = strDelete.TrimEnd('%');
            string strEvents = sb.ToString().TrimEnd('%');
            sb.Clear();
            ///各事件的分析使能设定
            var names = Enum.GetValues(typeof(pChart.IMarker.MarkType));
            int stage = 0;
            foreach (int value in names)
            {
                if (value < 50)
                {
                    int on = (analysisStateWord >> value) & 1;
                    if (value == (int)pChart.IMarker.MarkType.Stage)
                        stage = on;
                    else
                        sb.AppendFormat("{0}:{1};", value, on);
                }
            }
            string strAutoword = sb.ToString().TrimEnd(';');
            sb.Clear();
            ///by psz 睡眠分期区分是手动标记还是自动标记，手动标记为1，睡眠分期与标记类型之间用%分割
            sb.AppendFormat("{0}/{1}/{2}", stage, Channel.Default.SleepStageSourcePlan.Replace(';', ','), string.Join(",", Conditions.Epochs.Select(t => string.Format("{0}%{1}", t.Stage == 5 ? "W" : t.Stage == 4 ? "R" : t.Stage == 1 ? "N3" : t.Stage == 3 ? "N1" : t.Stage == 2 ? "N2" : "-1", t.ByHand ? 1 : 0))));
            string strStage = sb.ToString().TrimEnd(';');
            float respLimitvalue = 0;
            try
            {
                respLimitvalue = ((MarkerManage.Default.DefineMarkers.Find(t => t.MarkTyp == pChart.IMarker.MarkType.Hypopnea)) as pChart.RectangleMarkers).MinLimitValue;
            }
            catch { }
            float legLimitvalue = 0;
            try
            {
                legLimitvalue = ((MarkerManage.Default.DefineMarkers.Find(t => t.MarkTyp == pChart.IMarker.MarkType.LegMove)) as pChart.RectangleMarkers).MinLimitValue;
            }
            catch { }

            float BodyActionLimitvalue = 0;
            try
            {
                BodyActionLimitvalue = ((MarkerManage.Default.DefineMarkers.Find(t => t.MarkTyp == pChart.IMarker.MarkType.MT)) as pChart.RectangleMarkers).MinLimitValue;
            }
            catch { }

            float ArousalLimitvalue = 0;
            try
            {
                ArousalLimitvalue = ((MarkerManage.Default.DefineMarkers.Find(t => t.MarkTyp == pChart.IMarker.MarkType.Arousal)) as pChart.RectangleMarkers).MinLimitValue;
            }
            catch { }
            JsonObj senddata = new JsonObj()
            {
                //edf = string.Format("D:\\PSG\\load_data\\edfdata\\{0}", Path.GetFileName(Conditions.EdfPath)),
                edf = Conditions.EdfPath,
                base_info_only = true,
                light_time = string.Format("{0:yyyy-MM-dd HH:mm:ss}/{1:yyyy-MM-dd HH:mm:ss}", Conditions.LightOffTime, Conditions.LightOnTime),
                events = strEvents,
                stage = strStage,
                resp_config = string.Format("{0} */{1}/{2}", respLimitvalue == 0 ? 10 : respLimitvalue, Channel.Default.OxygenReduceRange, "*"),
                leg_config = string.Format("{0}/{1}", legLimitvalue == 0 ? 0.5f : legLimitvalue, "*"),
                action_config = string.Format("{0}/{1}", BodyActionLimitvalue == 0 ? 10 : BodyActionLimitvalue, "*"),//作用通道暂时不加
                arousal_config = string.Format("{0}/{1}", ArousalLimitvalue == 0 ? 5 : ArousalLimitvalue, "*"),//作用通道暂时不加
                autoanalysis = strAutoword,
                deleted_events = strDelete
            };
            return ComitCondition(senddata);
        }
        /// <summary>
        /// 获取最终的分析结果
        /// </summary>
        /// <param name="ID">结果保存的唯一标识符</param>
        /// <returns></returns>
        public OutResult ReadAnalysiResult(string ResultID)
        {
            AnalysisResult ret = new AnalysisResult();
            ret.Sleep.LightOnTime = Channel.Default.AnalysisReult.Sleep.LightOnTime;
            ret.Sleep.LightOffTime = Channel.Default.AnalysisReult.Sleep.LightOffTime;
            m_guid = ResultID;
            ret.GUID = ResultID;
            DateTime dt = DateTime.Now;
            m_AnalysisComplete = false;
            while (!m_AnalysisComplete)
            {
                if (!m_Client.IsConnected)
                {
                    EDF.Default.Interrupt = true;
                }
                ///睡眠两秒发送心跳，需要拆分成100份来睡，防止EDF.Default.Interrupt置true后，又迅速被置成false，无法退出循环
                for (int i = 0; i < 100; i++)
                {
                    System.Threading.Thread.Sleep(20);
                    if (EDF.Default.Interrupt)
                    {
                        Trace.WriteLine(string.Format("[{0:yyyy-MM-dd HH:mm:ss}] 停止心跳通讯", DateTime.Now));
                        return ret;
                    }
                }
                SendDataNoResponse(DataType.Comfirm);
            }
            SendDataNoResponse(DataType.Comfirm, "1");
            while (!m_eventsReadComplete || !m_epochReadComplete || !m_epochBaseReadCompplete)
            {
                if (EDF.Default.Interrupt)
                    break;
                System.Threading.Thread.Sleep(100);
            }
            if (!EDF.Default.Interrupt)
            {
                ret.Epochs = m_EpochsArray.ToList();
                ret.EventRecords = m_EventReocrds;
                if (ret.Sleep.LightOnTime.Year != 1)
                {
                    int lightonSpan = (int)(ret.Sleep.LightOnTime - m_startTime).TotalSeconds;
                    ret.EventRecords.Add(new Doc_EventRecords()
                    {
                        StartIndex = lightonSpan,
                        EndIndex = lightonSpan,
                        GUID = ResultID,
                        EventType = (int)pChart.IMarker.MarkType.LightOn
                    });
                }
                if (ret.Sleep.LightOffTime.Year != 1)
                {
                    int lightoffSpan = (int)(ret.Sleep.LightOffTime - m_startTime).TotalSeconds;
                    ret.EventRecords.Add(new Doc_EventRecords()
                    {
                        StartIndex = lightoffSpan,
                        EndIndex = lightoffSpan,
                        GUID = ResultID,
                        EventType = (int)pChart.IMarker.MarkType.LightOff
                    });
                }
            }
            else
            {
                ret.Interrupt = true;
                EDF.Default.Interrupt = false;
            }
            Trace.WriteLine(string.Format("获取指标数据耗时：{0}ms", (DateTime.Now - dt).TotalMilliseconds));
            return ret;
        }
        /// <summary>
        /// 判断数据源是否存在
        /// </summary>
        /// <returns></returns>
        public bool ExistDataSource()
        {
            return true;
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        public bool UpLoadDataSouce(byte[] data)
        {

            return true;
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <returns></returns>
        public bool DeleteDataSource()
        {
            return SendDataNoResponse(DataType.ClearDatasource);
        }

        private event ExtraTaskRunningDelegate m_ExtraTaskRunningHandler;

        public event ExtraTaskRunningDelegate ExtraTaskRunningHandler
        {
            add
            {
                if (this.m_ExtraTaskRunningHandler != null)
                    this.m_ExtraTaskRunningHandler = (ExtraTaskRunningDelegate)null;
                this.m_ExtraTaskRunningHandler = value;
            }
            remove
            {
                if (this.m_ExtraTaskRunningHandler == null)
                    return;
                this.m_ExtraTaskRunningHandler = (ExtraTaskRunningDelegate)null;
            }
        }
        #endregion
        #region 频谱分析
        public delegate void SpectrumReadyDelegate(string path);
        private event SpectrumReadyDelegate m_SpectrumReadyHandle;
        /// <summary>
        /// 图谱绘制触发
        /// </summary>
        public event SpectrumReadyDelegate SpectrumReadyHandle
        {
            add
            {
                if (m_SpectrumReadyHandle != null)
                    m_SpectrumReadyHandle = null;
                m_SpectrumReadyHandle += value;
            }
            remove
            {
                m_SpectrumReadyHandle = null;
            }
        }
        #endregion
        #region 尝试连接任务
        /// <summary>
        /// 定时清理系统垃圾的线程
        /// </summary>
        private System.Threading.Thread th = null;
        /// <summary>
        /// 中断任务标志
        /// </summary>
        private bool m_KillTask = true;
        /// <summary>
        /// 任务执行器
        /// </summary>
        private void TaskStart()
        {
            th = new System.Threading.Thread(() =>
            {
                while (!m_KillTask)
                {
                    System.Threading.Thread.Sleep(1500);
                    if (!m_Client.IsConnected)
                    {
                        try
                        {
                            m_Client.Close();
                        }
                        catch { }
                        try
                        {
                            if (!m_Client.Open())
                            {
                                RunAnalysisExe();
                            }
                        }
                        catch
                        {

                        }
                    }
                }
            });
            th.IsBackground = true;
            th.SetApartmentState(System.Threading.ApartmentState.MTA);
            th.Start();
        }
        private string m_analysisName = "socket_tcp";
        private string m_AnalysisExePath = "";
        /// <summary>
        /// 开启线程
        /// </summary>
        /// <returns></returns>
        private bool RunAnalysisExe()
        {
            try
            {
                //return true;///给算法调试用
                if (!runProcess())
                {
                    MessageForm.Show("请检查算法模块运行是否正常！");
                    return false;
                }
            }
            catch (ArgumentException ex)
            {
                return false;
            }
            return true;
        }
        private bool runProcess()
        {
            if (File.Exists(m_AnalysisExePath))
            {
                Process[] pro = Process.GetProcessesByName(m_analysisName);
                for (int i = 0; i < pro.Length; i++)
                {
                    pro[i].Kill();
                }
                Process proc = new Process();
                proc.StartInfo.FileName = m_AnalysisExePath;
                //设定程式执行参数 
                proc.StartInfo.Arguments = string.Format("--path=\"{0}\"", Path.GetDirectoryName(m_AnalysisExePath));
                proc.Start();
                if (proc != null)
                {
                    //监视进程退出
                    proc.EnableRaisingEvents = true;
                    //指定退出事件方法
                    proc.Exited += new EventHandler(proc_Exited);
                }
                return true;
            }
            else
                return false;
        }
        /// <summary>
        ///启动外部程序退出事件
        /// </summary>
        private void proc_Exited(object sender, EventArgs e)
        {
            try
            {
                m_Client.Close();
                if (!m_KillTask)
                {
                    if (!runProcess())
                    {
                        MessageForm.Show("请检查算法模块运行是否正常！");
                    }
                }
            }
            catch { }
        }
        #endregion
    }
}
