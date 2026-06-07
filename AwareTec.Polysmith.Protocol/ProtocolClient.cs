using pSystem.Communication.Com;
using System;
using System.Collections.Generic;
using System.Linq;
namespace AwareTec.Polysmith.Protocol
{
    /// <summary>
    /// 链路层基类
    /// </summary>
    internal class ProtocolClient : IDisposable
    {
        #region 私有成员
        private Saver m_reportSaver = null;
        private IOClient m_Client = null;
        private Dictionary<int, DataDefine[]> m_dataDefineList = null;
        private ProtocolAnalysis m_analysis = null;
        private List<DataDefine> m_UserDataList = null;
        private UserDataDefine m_userData = null;
        /// <summary>
        /// 任务终止
        /// </summary>
        private bool m_KillTask = true;
        /// <summary>
        /// 线程th
        /// </summary>
        private System.Threading.Thread th = null;
        private int m_CastTimeCounter = 0;
        private bool m_CastTimeCompelt = true;
        private Saver m_IndexSaver = null;
        /// <summary>
        /// 任务执行器
        /// </summary>
        private void TaskStart()
        {
            th = new System.Threading.Thread(() =>
            {
                while (!m_KillTask)
                {
                    if (!m_CastTimeCompelt)
                    {
                        if (m_CastTimeCounter == 60)
                        {
                            SetCastTimeRequest();
                            m_CastTimeCounter = 0;
                        }
                        m_CastTimeCounter++;
                    }
                    System.Threading.Thread.Sleep(1000);
                }
            });
            th.IsBackground = true;
            th.SetApartmentState(System.Threading.ApartmentState.MTA);
            th.Start();
        }
        /// <summary>
        /// 用户数据解析
        /// </summary>
        /// <param name="DataPiker"></param>
        private void m_analysis_ReciveDataPikerHandle(DataDefine.FrameFactory DataPiker)
        {
            DateTime dt = DateTime.Now;
            try
            {
                switch (DataPiker.Fun)
                {
                    case (byte)DataDefine.CommandID.PushInt32_220:
                    case (byte)DataDefine.CommandID.PushInt16_404:
                    case (byte)DataDefine.CommandID.PushMuzzleFlow:
                    case (byte)DataDefine.CommandID.PushInt16_4:
                        DateTime dst2 = DateTime.Now;
                        if (m_dataDefineList.Count < 1)
                            return;
                        int channelIndex = 0;
                        int totalChannelCount = m_dataDefineList[DataPiker.Fun].Length;
                        int startIndex = 0;
                        while (channelIndex < totalChannelCount)
                        {
                            DataDefine find = m_dataDefineList[DataPiker.Fun][channelIndex];
                            int dCount = find.DataBytesLength + startIndex;
                            for (; startIndex < dCount;)
                            {
                                switch (find.ByteCountInData)
                                {
                                    case 1:
                                        if (find.unSignData)
                                            find.Value = DataPiker.UserData[startIndex];
                                        else
                                            find.Value = Convert.ToSByte(DataPiker.UserData[startIndex].ToString("X2"), 16);
                                        break;
                                    case 2:
                                        if (find.unSignData)
                                        {
                                            find.Value = BitConverter.ToUInt16(DataPiker.UserData, startIndex);
                                        }
                                        else
                                        {
                                            find.Value = BitConverter.ToInt16(DataPiker.UserData, startIndex);
                                        }
                                        break;
                                    case 3:
                                        int value = ((DataPiker.UserData[startIndex] << 16) | (DataPiker.UserData[startIndex + 1] << 8) | DataPiker.UserData[startIndex + 2]);
                                        if (!find.unSignData)
                                            value = value > 0x7FFFFF ? -(0xFFFFFF - value) : value;
                                        find.Value = value;
                                        break;
                                    case 4:
                                        if (find.unSignData)
                                            find.Value = BitConverter.ToUInt32(DataPiker.UserData, startIndex);
                                        else
                                            find.Value = BitConverter.ToInt32(DataPiker.UserData, startIndex);
                                        break;
                                }
                                startIndex += find.ByteCountInData;
                            }
                            channelIndex++;
                        }
                        Console.WriteLine(string.Format(" 接收到[GroupID=0x{1:X2}]数据耗时：{0}ms", (DateTime.Now - dst2).TotalMilliseconds, DataPiker.Fun));
                        break;
                    case (byte)DataDefine.CommandID.GetDeviceRequest:
                        if (DataPiker.UserData.Length > 0 && RecDeviceNameEventHandle != null)
                        {
                            RecDeviceNameEventHandle.BeginInvoke(PortClient.MatchKey, System.Text.Encoding.Default.GetString(DataPiker.UserData), null, null);
                        }
                        Commplet = DataDefine.eumErrCode.OK;
                        m_Step = DataDefine.CommandID.None;
                        break;
                    case (byte)DataDefine.CommandID.GetFirmwareVesionRequest:
                        if (DataPiker.UserData.Length > 0 && (byte)m_Step == DataPiker.Fun)
                        {
                            Commplet = DataDefine.eumErrCode.OK;
                            m_Step = DataDefine.CommandID.None;
                            ///todo 版本号赋值。。。。   硬件那边设计问题                      
                            m_userData.FirmwareVesion = System.Text.Encoding.Default.GetString(DataPiker.UserData).Trim();
                            m_userData.SoftWareVesion = "";
                            //m_userData.FirmwareVesion = string.Format("{0}.{1}.{2}", DataPiker.UserData[3], DataPiker.UserData[4], DataPiker.UserData[5]);
                            //m_userData.SoftWareVesion = string.Format("{0}.{1}.{2}", DataPiker.UserData[0], DataPiker.UserData[1], DataPiker.UserData[2]);
                            Refresh();
                        }
                        else
                        {
                            Commplet = DataDefine.eumErrCode.ERROR_FRAME_FORMAT;
                        }                  
                        break;
                    case (byte)DataDefine.CommandID.GetDeviceHSCode:
                        if (DataPiker.UserData.Length > 0)
                        {
                            Commplet = DataDefine.eumErrCode.OK;
                            m_Step = DataDefine.CommandID.None;

                            
                            m_userData.SoftWareVesion += System.Text.Encoding.Default.GetString(DataPiker.UserData);
                            Refresh();
                        }
                        else
                        {
                            Commplet = DataDefine.eumErrCode.ERROR_FRAME_FORMAT;
                        }
                        break;
                    case (byte)DataDefine.CommandID.GetPatientIDRequest:
                        if (DataPiker.UserData.Length > 0)
                        {
                            m_userData.PatientInfo = System.Text.Encoding.Default.GetString(DataPiker.UserData);
                            Refresh();
                            Commplet = DataDefine.eumErrCode.OK;
                            m_Step = DataDefine.CommandID.None;
                        }
                        else
                        {
                            Commplet = DataDefine.eumErrCode.ERROR_FRAME_FORMAT;
                        }
                        break;
                    case (byte)DataDefine.CommandID.SetDeviceSNCode:
                        if (DataPiker.UserData.Length > 0)
                        {
                            m_userData.DeviceSNCode = System.Text.Encoding.Default.GetString(DataPiker.UserData);
                            Refresh();
                            Commplet = DataDefine.eumErrCode.OK;
                            m_Step = DataDefine.CommandID.None;
                        }
                        else
                        {
                            Commplet = DataDefine.eumErrCode.ERROR_FRAME_FORMAT;
                        }
                        break;
                    case (byte)DataDefine.CommandID.SetDeviceTypeRequest:
                        if (DataPiker.UserData.Length > 0)
                        {
                            m_userData.DeviceType = DataPiker.UserData[0];
                            Refresh();
                            Commplet = DataDefine.eumErrCode.OK;
                            m_Step = DataDefine.CommandID.None;
                        }
                        else
                        {
                            Commplet = DataDefine.eumErrCode.ERROR_FRAME_FORMAT;
                        }
                        break;
                    case (byte)DataDefine.CommandID.SetBlueToothName:
                        if (DataPiker.UserData.Length > 0)
                        {
                            m_userData.BlueToothName = System.Text.Encoding.Default.GetString(DataPiker.UserData);
                            Refresh();
                            if (RecDeviceNameEventHandle != null)
                            {
                                RecDeviceNameEventHandle.BeginInvoke(PortClient.MatchKey, m_userData.BlueToothName, null, null);
                            }
                            Commplet = DataDefine.eumErrCode.OK;
                            m_Step = DataDefine.CommandID.None;
                        }
                        else
                        {
                            Commplet = DataDefine.eumErrCode.ERROR_FRAME_FORMAT;
                        }
                        break;
                    case (byte)DataDefine.CommandID.IdentifyRequest:
                        if (DataPiker.UserData.Length > 16)
                        {
                            if (System.Text.Encoding.Default.GetString(DataPiker.UserData, 0, 11) == "consent_FSA" && (byte)m_Step == DataPiker.Fun)
                            {
                                Commplet = DataDefine.eumErrCode.OK;
                                m_Step = DataDefine.CommandID.None;
                                m_userData.DeviceBusy = DataPiker.UserData[15] == 2;
                                Refresh();
                            }
                            else
                            {
                                Commplet = DataDefine.eumErrCode.ERROR_FRAME_FORMAT;
                            }
                        }
                        else
                        {
                            Commplet = DataDefine.eumErrCode.ERROR_FRAME_FORMAT;
                        }
                        break;
                    case (byte)DataDefine.CommandID.SetCastTimeRequest:
                        //if (m_userData.bakLastCastTime != "")
                        string strTime = System.Text.Encoding.Default.GetString(DataPiker.UserData);
                        if (strTime == DateTime.Parse(m_userData.bakLastCastTime).ToString("yy MM dd HH mm ss"))
                        {
                            m_CastTimeCompelt = true;
                            Commplet = DataDefine.eumErrCode.OK;
                            m_userData.LastCastTime = m_userData.bakLastCastTime;
                            Refresh();
                            m_Step = DataDefine.CommandID.None;
                        }
                        else
                        {
                            Commplet = DataDefine.eumErrCode.ERROR_NAK;
                        }
                        m_userData.bakLastCastTime = "";

                        break;
                    case (byte)DataDefine.CommandID.SetAutoRunTime:
                        string strTime2 = System.Text.Encoding.Default.GetString(DataPiker.UserData);
                        m_userData.AutoRunTime = "";
                        if (m_userData.bakLastCastTime != "")
                        {
                            if (strTime2.Trim('\0') == m_userData.bakLastCastTime)
                            {
                                Commplet = DataDefine.eumErrCode.OK;
                                int year = DateTime.Now.Year / 100;
                                string[] times = strTime2.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                m_userData.AutoRunTime = string.Format("{0}{1}-{2}-{3} {4}:{5}:{6},{0}{7}-{8}-{9} {10}:{11}:{12}",
                                                         year, times[0], times[1], times[2], times[3],
                                                         times[4], times[5], times[6], times[7], times[8], times[9], times[10], times[11]);
                                m_Step = DataDefine.CommandID.None;
                            }
                            else
                            {
                                Commplet = DataDefine.eumErrCode.ERROR_NAK;
                            }
                        }
                        else
                        {
                            string[] times = strTime2.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (times.Length == 12)
                            {
                                Commplet = DataDefine.eumErrCode.OK;
                                m_Step = DataDefine.CommandID.None;
                                int year = DateTime.Now.Year / 100;
                                if (times[1] != "00" && times[7] != "00")
                                    m_userData.AutoRunTime = string.Format("{0}{1}-{2}-{3} {4}:{5}:{6},{0}{7}-{8}-{9} {10}:{11}:{12}",
                                                             year, times[0], times[1], times[2], times[3], times[4], times[5],
                                                                   times[6], times[7], times[8], times[9], times[10], times[11]);
                            }
                            else
                            {
                                Commplet = DataDefine.eumErrCode.ERROR_NAK;
                            }
                        }
                        m_userData.bakLastCastTime = "";
                        break;
                    case (byte)DataDefine.CommandID.SetCalibrationFlag:
                        Commplet = DataDefine.eumErrCode.OK;
                        m_Step = DataDefine.CommandID.None;
                        break;
                    case (byte)DataDefine.CommandID.SetSampleFrequencyRequest:
                        Commplet = DataDefine.eumErrCode.OK;
                        m_Step = DataDefine.CommandID.None;
                        break;
                    case (byte)DataDefine.CommandID.GetBatteryCapacityRequest:
                        if (DataPiker.UserData.Length > 0)
                        {
                            Commplet = DataDefine.eumErrCode.OK;
                            m_userData.BatteryCapacity = DataPiker.UserData[0];
                            m_userData.InitReady = true;
                            Refresh();
                            m_Step = DataDefine.CommandID.None;
                        }
                        else
                        {
                            Commplet = DataDefine.eumErrCode.ERROR_FRAME_FORMAT;
                        }
                        break;
                    case (byte)DataDefine.CommandID.SetStartSampleRequest:
                        if (DataPiker.UserData.Length > 0)
                        {
                            Commplet = DataDefine.eumErrCode.OK;
                            if (DataPiker.UserData[0] == 0)
                            {
                                Commplet = DataDefine.eumErrCode.ERROR_NO_ACK;///采集失败，因为发送判断逻辑不严谨暂时把状态强制成无响应
                            }
                            m_Step = DataDefine.CommandID.None;
                        }
                        else
                        {
                            Commplet = DataDefine.eumErrCode.ERROR_FRAME_FORMAT;
                        }
                        break;
                    case (byte)DataDefine.CommandID.SetContinueSampleRequest:
                        if (DataPiker.UserData.Length > 0)
                        {
                            Commplet = DataDefine.eumErrCode.OK;
                            //if (DataPiker.UserData[0] == 0)
                            //{
                            //    Commplet = DataDefine.eumErrCode.ERROR_NO_ACK;///继续监听失败，因为发送判断逻辑不严谨暂时把状态强制成无响应
                            //}
                            m_Step = DataDefine.CommandID.None;
                        }
                        else
                        {
                            Commplet = DataDefine.eumErrCode.ERROR_FRAME_FORMAT;
                        }
                        break;
                    case (byte)DataDefine.CommandID.SetStopSampleRequest:
                        if (DataPiker.UserData.Length > 0)
                        {
                            Commplet = DataDefine.eumErrCode.OK;
                            //if (DataPiker.UserData[0] == 0)
                            //{
                            //    Commplet = DataDefine.eumErrCode.ERROR_NO_ACK;///停止采集失败，因为发送判断逻辑不严谨暂时把状态强制成无响应
                            //}
                            m_Step = DataDefine.CommandID.None;
                        }
                        else
                        {
                            Commplet = DataDefine.eumErrCode.ERROR_FRAME_FORMAT;
                        }
                        break;
                    case (byte)DataDefine.CommandID.GetImpedanceDataRequest:
                        if (DataPiker.UserData.Length >= 16 * 4)
                        {
                            Commplet = DataDefine.eumErrCode.OK;
                            m_Step = DataDefine.CommandID.None;
                            uint[] imdata = new uint[16];
                            int index = 0;
                            for (int i = 0; i < DataPiker.UserData.Length;)
                            {
                                imdata[index++] = BitConverter.ToUInt32(DataPiker.UserData, i);
                                i += 4;
                                if (index == 16)
                                    break;
                            }
                            if (RecImpedanceDataHandle != null)
                                RecImpedanceDataHandle.BeginInvoke(imdata, null, null);
                        }
                        else if (DataPiker.UserData.Length > 20)
                        {
                            string strvalue = System.Text.Encoding.Default.GetString(DataPiker.UserData);
                            if (strvalue.ToUpper().Contains("OPEN"))
                            {
                                Console.WriteLine("下位机开始阻抗测试");
                            }
                            m_Step = DataDefine.CommandID.None;
                            Commplet = DataDefine.eumErrCode.OK;
                        }
                        else
                        {
                            Commplet = DataDefine.eumErrCode.ERROR_FRAME_FORMAT;
                        }
                        break;
                }
            }
            catch(Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
            Console.WriteLine(string.Format(" 接收到一帧数据耗时：{0}ms", (DateTime.Now - dt).TotalMilliseconds));
        
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="userData"></param>
        private void Refresh()
        {
            if (RefreshUserDataHandle != null)
                RefreshUserDataHandle.Invoke(m_userData);
        }
        /// <summary>
        /// 接收到原始数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="portClient"></param>
        private void value_DataReceiveHandle(byte[] data, IOClient portClient)
        {
            if (!m_stop)
            {
                //if (IsSaver > 0)
                //{
                //string des = string.Join(" ", data.Select(t => t.ToString("x2")));
                ///打印原始数据
                // m_Client.PrintFrame(des, "DataSource", 2);
                m_analysis.RxBytes(data);
                //}
                //if (IsSaver == 0 || IsSaver == 2)
                //{
                m_reportSaver.Save(string.Format("[{0:hh:mm:ss fff}] 接收：{1} bytes", DateTime.Now, data.Length));
                //m_reportSaver.Save(string.Format("{0} \r\n", des));
                //}
            }
        }
        /// <summary>
        /// 组帧
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        private byte[] BulidFrame(DataDefine.FrameFactory frame)
        {
            List<byte> send = new List<byte>();
            send.Add((byte)('A'));
            send.Add(frame.Fun);
            send.Add(frame.AccessSelections);
            int datalen = 0;
            if (frame.UserData != null)
                datalen = frame.UserData.Length;
            int len = datalen + 7;
            send.Add((byte)(len % 256));
            send.Add((byte)(len / 256));
            if (datalen > 0)
                send.AddRange(frame.UserData);
            ProtocolAnalysis.CRC16 crc16 = m_analysis.s_Crc16Bit(send);
            send.InsertRange(0, frame.StartByte);
            send.Add(crc16.CRC_L);
            send.Add(crc16.CRC_H);
            send.AddRange(frame.Endbyte);
            return send.ToArray();
        }
        /// <summary>
        /// 当前指令ID
        /// </summary>
        private DataDefine.CommandID m_Step = DataDefine.CommandID.None;
        private DataDefine.eumErrCode m_compelet = DataDefine.eumErrCode.ERROR_NO_ACK;
        object m_lockObj = new object();
        /// <summary>
        /// 执行结果标识
        /// </summary>
        private DataDefine.eumErrCode Commplet
        {
            set
            {
                lock (m_lockObj)
                    m_compelet = value;
            }
            get
            {
                return m_compelet;
            }
        }
        /// <summary>
        /// 数据发送
        /// </summary>
        /// <param name="data"></param>
        private DataDefine.eumErrCode sendData(byte[] data, DataDefine.CommandID commandID, bool BeResponse=true)
        {
            DataDefine.eumErrCode ret = DataDefine.eumErrCode.OK;
            fnStop = false;
            int sendCnt = 0;
            m_Step = commandID;
            Commplet = BeResponse? DataDefine.eumErrCode.ERROR_NO_ACK: DataDefine.eumErrCode.OK;
            do
            {
                sendCnt++;
                if (m_Client.IsConnected)
                {
                    ///打印发送数据
                    m_Client.PrintFrame(string.Join(" ", data.Select(t => t.ToString("X2"))), m_Step.ToString());
                    if (m_Client.DataSend(data))
                    {
                        System.Threading.Thread.Sleep(m_Client.Delay);
                        int readCnt = 0;
                        int sTatol = 0;
                        //while (sTatol < m_Client.TimeOut && Commplet == DataDefine.eumErrCode.ERROR_NO_ACK)
                        while (sTatol < m_Client.TimeOut && Commplet != DataDefine.eumErrCode.OK)
                        {
                            readCnt++;
                            System.Threading.Thread.Sleep(20);
                            sTatol = readCnt * 20;
                            if (fnStop)
                            {
                                ret = DataDefine.eumErrCode.ERROR_Interrupt;
                                break;
                            }
                        }
                        if(Commplet != DataDefine.eumErrCode.OK)
                        {
                            ret = sTatol >= m_Client.TimeOut ?
                                            DataDefine.eumErrCode.ERROR_TIMEOUT_BYTE :
                                            Commplet;
                        }

                        //if (sTatol >= m_Client.TimeOut && Commplet != DataDefine.eumErrCode.OK)
                        //{
                        //    ret = DataDefine.eumErrCode.ERROR_TIMEOUT_BYTE;
                        //}
                    }
                    else
                    {
                        ret = DataDefine.eumErrCode.ERROR_OPEN;
                    }
                }
                else
                {
                    ret = DataDefine.eumErrCode.ERROR_CLOSE;
                }
                if (fnStop)
                {
                    ret = DataDefine.eumErrCode.ERROR_Interrupt;
                    break;
                }
            } while (sendCnt <= 3 && ret != DataDefine.eumErrCode.OK && !m_stop);
            m_Step = DataDefine.CommandID.None;
            return ret;
        }
        /// <summary>
        /// 握手成功标识
        /// </summary>
        private bool m_IdentifyRaedy = false;
        /// <summary>
        /// 建立会话
        /// </summary>
        private DataDefine.eumErrCode Identify()
        {
            if (m_Step != DataDefine.CommandID.None)
                return DataDefine.eumErrCode.ERROR_NO_ACK;
            DataDefine.FrameFactory frame = new DataDefine.FrameFactory();
            frame.Fun = (byte)DataDefine.CommandID.IdentifyRequest;
            frame.AccessSelections = 0;
            frame.UserData = System.Text.Encoding.Default.GetBytes("link_FSA");
            return sendData(BulidFrame(frame), DataDefine.CommandID.IdentifyRequest);
        }

        /// <summary>
        /// 获取固件版本号
        /// </summary>
        private DataDefine.eumErrCode GetFirmwareVesionRequest()
        {
            if (m_Step != DataDefine.CommandID.None)
                return DataDefine.eumErrCode.ERROR_NO_ACK;
            DataDefine.FrameFactory frame = new DataDefine.FrameFactory();
            frame.Fun = (byte)DataDefine.CommandID.GetFirmwareVesionRequest;
            frame.AccessSelections = 0;
            frame.UserData = System.Text.Encoding.Default.GetBytes("F_H_version");
            return sendData(BulidFrame(frame), DataDefine.CommandID.GetFirmwareVesionRequest);
        }
        /// <summary>
        /// 校时
        /// </summary>
        private DataDefine.eumErrCode SetCastTimeRequest()
        {
            if (m_Step != DataDefine.CommandID.None)
                return DataDefine.eumErrCode.ERROR_NO_ACK;
            DataDefine.FrameFactory frame = new DataDefine.FrameFactory();
            frame.Fun = (byte)DataDefine.CommandID.SetCastTimeRequest;
            frame.AccessSelections = 1;
            DateTime dt = DateTime.Now;
            string strtime = string.Format("{0} {1:MM dd HH mm ss}", dt.Year % 100, dt);
            frame.UserData = System.Text.Encoding.ASCII.GetBytes(strtime);
            m_userData.bakLastCastTime = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return sendData(BulidFrame(frame), DataDefine.CommandID.SetCastTimeRequest);
        }
        #endregion
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Channels"></param>
        public ProtocolClient(List<DataDefine> Channels)
        {
            m_userData = new UserDataDefine();
            m_UserDataList = new List<DataDefine>();
            m_analysis = new ProtocolAnalysis();
            m_IndexSaver = new Saver() { Name = "FrameN0", TotalCnt = 100 };
            m_reportSaver = new Saver() { Name = "端口源信息记录" };
            m_analysis.ReciveDataPikerHandle += m_analysis_ReciveDataPikerHandle;
            m_KillTask = false;
            TaskStart();
            m_dataDefineList = new Dictionary<int, DataDefine[]>();
            if (Channels != null)
            {
                var g = Channels.GroupBy(t => t.GroupID);
                foreach (var item in g)
                {
                    m_dataDefineList.Add(item.Key, item.ToList().OrderBy(t => t.GroupIndex).ToArray());
                }
            }
        }
        #endregion
        #region 事件定义
        /// <summary>
        /// 刷新基础用户数据
        /// </summary>
        /// <param name="userData"></param>
        public delegate void RefreshUserDataDelegate(UserDataDefine userData);
        /// <summary>
        /// 刷新基础用户数据时发生
        /// </summary>
        public event RefreshUserDataDelegate RefreshUserDataHandle;
        /// <summary>
        /// 阻抗数据更新时触发委托
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="data"></param>
        public delegate void RecImpedanceDataDelegate(uint[] UserData);
        /// <summary>
        /// 阻抗数据更新时触发事件
        /// </summary>
        public event RecImpedanceDataDelegate RecImpedanceDataHandle;
        /// <summary>
        /// 查询到设备名时触发委托
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="data"></param>
        public delegate void RecDeviceNameDelegate(string matchKey, string devName);
        /// <summary>
        ///  查询到设备名时触发事件
        /// </summary>
        public event RecDeviceNameDelegate RecDeviceNameEventHandle;
        #endregion
        #region 共有成员
        /// <summary>
        /// ID
        /// </summary>
        public string ID
        {
            set
            {
                m_IndexSaver.ID = value;
            }
            get
            {
                return m_IndexSaver.ID;
            }
        }
        private bool m_stop = false;
        /// <summary>
        /// 停止
        /// </summary>
        public bool fnStop
        {
            set
            {
                m_stop = value;
                foreach (KeyValuePair<int, DataDefine[]> p in m_dataDefineList)
                {
                    for (int i = 0; i < p.Value.Length; i++)
                    {
                        p.Value[i].fnStop = value;
                    }
                }
                if (value)
                {
                    m_IndexSaver.Reset();
                    m_analysis.Reset();
                    m_reportSaver.Reset();
                }
            }
            get
            {
                return m_stop;
            }
        }
        /// <summary>
        /// 设置通讯对象
        /// </summary>
        public IOClient PortClient
        {
            set
            {
                m_Client = value;
                m_Client.DataReceiveHandle += value_DataReceiveHandle;
            }
            get
            {
                return m_Client;
            }
        }
        /// <summary>
        /// 握手
        /// </summary>
        /// <returns></returns>
        private DataDefine.eumErrCode ShakeHands()
        {
            if (m_Step != DataDefine.CommandID.None)
                return DataDefine.eumErrCode.ERROR_NO_ACK;
            DataDefine.eumErrCode ret = DataDefine.eumErrCode.OK;
            if (!m_IdentifyRaedy)
            {
                ret = Identify();
                if (ret != DataDefine.eumErrCode.OK || fnStop)
                    return ret;
                ret = GetFirmwareVesionRequest();
                ///获取固件版本
                if (ret != DataDefine.eumErrCode.OK || fnStop)
                    return ret;
                else
                {
                    m_IdentifyRaedy = true;
                }
                ///校时
                ret = SetCastTimeRequest();
                if (ret != DataDefine.eumErrCode.OK)
                    m_CastTimeCompelt = false;
            }
            return ret;
        }
        /// <summary>
        /// 开始采样
        /// </summary>
        public DataDefine.eumErrCode SetStartSampleRequest()
        {
            DataDefine.eumErrCode ret = ShakeHands();
            if (ret == DataDefine.eumErrCode.OK)
            {
                DataDefine.FrameFactory frame = new DataDefine.FrameFactory();
                frame.Fun = (byte)DataDefine.CommandID.SetStartSampleRequest;
                frame.AccessSelections = 1;
                return sendData(BulidFrame(frame), DataDefine.CommandID.SetStartSampleRequest);
            }
            return ret;
        }
        /// <summary>
        /// 停止采样
        /// </summary>
        public DataDefine.eumErrCode SetStopSampleRequest()
        {
            DataDefine.eumErrCode ret = ShakeHands();
            if (ret == DataDefine.eumErrCode.OK)
            {
                DataDefine.FrameFactory frame = new DataDefine.FrameFactory();
                frame.Fun = (byte)DataDefine.CommandID.SetStopSampleRequest;
                frame.AccessSelections = 1;
                return sendData(BulidFrame(frame), DataDefine.CommandID.SetStopSampleRequest);
            }
            return ret;
        }
        /// <summary>
        ///  设置采样频率
        /// </summary>
        public DataDefine.eumErrCode SetSampleFrequencyRequest(DataDefine.CommandID ID, byte Frequency)
        {
            DataDefine.eumErrCode ret = ShakeHands();
            if (ret == DataDefine.eumErrCode.OK)
            {
                DataDefine.FrameFactory frame = new DataDefine.FrameFactory();
                frame.Fun = (byte)DataDefine.CommandID.SetSampleFrequencyRequest;
                frame.AccessSelections = 1;
                frame.UserData = new byte[] { (byte)ID, Frequency };
                return sendData(BulidFrame(frame), DataDefine.CommandID.SetSampleFrequencyRequest);
            }
            return ret;
        }
        /// <summary>
        ///  读取阻抗值
        /// </summary>
        public DataDefine.eumErrCode GetImpedanceDataRequest(bool on)
        {
            DataDefine.eumErrCode ret = ShakeHands();
            if (ret == DataDefine.eumErrCode.OK)
            {
                DataDefine.FrameFactory frame = new DataDefine.FrameFactory();
                frame.Fun = (byte)DataDefine.CommandID.GetImpedanceDataRequest;
                frame.AccessSelections = (byte)(on ? 1 : 0);
                ret = sendData(BulidFrame(frame), DataDefine.CommandID.GetImpedanceDataRequest);
            }
            return ret;
        }
        /// <summary>
        ///  读取电池剩余容量值
        /// </summary>
        public DataDefine.eumErrCode GetBatteryCapacityRequest()
        {
            DataDefine.eumErrCode ret = ShakeHands();
            if (ret == DataDefine.eumErrCode.OK)
            {
                DataDefine.FrameFactory frame = new DataDefine.FrameFactory();
                frame.Fun = (byte)DataDefine.CommandID.GetBatteryCapacityRequest;
                frame.AccessSelections = 0;
                return sendData(BulidFrame(frame), DataDefine.CommandID.GetBatteryCapacityRequest);
            }
            return ret;
        }
        /// <summary>
        ///  设置病人信息
        /// </summary>
        public DataDefine.eumErrCode SetPatientIDRequest(string ID)
        {
            DataDefine.eumErrCode ret = ShakeHands();
            if (ret == DataDefine.eumErrCode.OK)
            {
                DataDefine.FrameFactory frame = new DataDefine.FrameFactory();
                frame.Fun = (byte)DataDefine.CommandID.GetPatientIDRequest;
                frame.AccessSelections = 1;
                frame.UserData = System.Text.Encoding.Default.GetBytes(ID);
                return sendData(BulidFrame(frame), DataDefine.CommandID.GetPatientIDRequest);
            }
            return ret;
        }
        /// <summary>
        ///  读取病人信息
        /// </summary>
        public DataDefine.eumErrCode GetPatientIDRequest()
        {
            DataDefine.eumErrCode ret = ShakeHands();
            if (ret == DataDefine.eumErrCode.OK)
            {
                DataDefine.FrameFactory frame = new DataDefine.FrameFactory();
                frame.Fun = (byte)DataDefine.CommandID.GetPatientIDRequest;
                frame.AccessSelections = 0;
                return sendData(BulidFrame(frame), DataDefine.CommandID.GetPatientIDRequest);
            }
            return ret;
        }
        /// <summary>
        ///  设置设备信息
        /// </summary>
        public DataDefine.eumErrCode SetDeviceInfoRequest(string deviceInfo)
        {
            DataDefine.eumErrCode ret = ShakeHands();
            if (ret == DataDefine.eumErrCode.OK)
            {
                DataDefine.FrameFactory frame = new DataDefine.FrameFactory();
                frame.Fun = (byte)DataDefine.CommandID.GetDeviceRequest;
                frame.AccessSelections = 1;
                frame.UserData = System.Text.Encoding.Default.GetBytes(deviceInfo);
                return sendData(BulidFrame(frame), DataDefine.CommandID.GetDeviceRequest);
            }
            return ret;
        }
        /// <summary>
        ///  读取设备信息
        /// </summary>
        public DataDefine.eumErrCode GetDeviceInfoRequest()
        {
            DataDefine.eumErrCode ret = ShakeHands();
            if (ret == DataDefine.eumErrCode.OK)
            {
                DataDefine.FrameFactory frame = new DataDefine.FrameFactory();
                frame.Fun = (byte)DataDefine.CommandID.GetDeviceRequest;
                frame.AccessSelections = 0;
                return sendData(BulidFrame(frame), DataDefine.CommandID.GetDeviceRequest);
            }
            return ret;
        }
        /// <summary>
        ///  继续监听
        /// </summary>
        public DataDefine.eumErrCode SetContinueSampleRequest()
        {
            DataDefine.eumErrCode ret = ShakeHands();
            if (ret == DataDefine.eumErrCode.OK)
            {
                DataDefine.FrameFactory frame = new DataDefine.FrameFactory();
                frame.Fun = (byte)DataDefine.CommandID.SetContinueSampleRequest;
                frame.AccessSelections = 1;
                return sendData(BulidFrame(frame), DataDefine.CommandID.SetContinueSampleRequest);
            }
            return ret;
        }
        /// <summary>
        ///  设置SN号
        /// </summary>
        public DataDefine.eumErrCode SetDeviceSNCode(string SNCode)
        {
            DataDefine.eumErrCode ret = ShakeHands();
            if (ret == DataDefine.eumErrCode.OK)
            {
                DataDefine.FrameFactory frame = new DataDefine.FrameFactory();
                frame.Fun = (byte)DataDefine.CommandID.SetDeviceSNCode;
                frame.AccessSelections = 1;
                frame.UserData = System.Text.Encoding.Default.GetBytes(SNCode);
                return sendData(BulidFrame(frame), DataDefine.CommandID.SetDeviceSNCode);
            }
            return ret;
        }
        /// <summary>
        ///  读取SN号
        /// </summary>
        public DataDefine.eumErrCode GetDeviceSNCode()
        {
            DataDefine.eumErrCode ret = ShakeHands();
            if (ret == DataDefine.eumErrCode.OK)
            {
                DataDefine.FrameFactory frame = new DataDefine.FrameFactory();
                frame.Fun = (byte)DataDefine.CommandID.SetDeviceSNCode;
                frame.AccessSelections = 0;
                return sendData(BulidFrame(frame), DataDefine.CommandID.SetDeviceSNCode);
            }
            return ret;
        }
        /// <summary>
        ///  读取历史日志
        /// </summary>
        public DataDefine.eumErrCode GetDeviceHS()
        {
            DataDefine.eumErrCode ret = ShakeHands();
            if (ret == DataDefine.eumErrCode.OK)
            {
                DataDefine.FrameFactory frame = new DataDefine.FrameFactory();
                frame.Fun = (byte)DataDefine.CommandID.GetDeviceHSCode;
                frame.AccessSelections = 0;
                return sendData(BulidFrame(frame), DataDefine.CommandID.GetDeviceHSCode);
            }
            return ret;
        }
        
        /// <summary>
        ///  读取固件号
        /// </summary>
        // public DataDefine.eumErrCode GetDeviceFWCode()
        //{
        //    DataDefine.eumErrCode ret = ShakeHands();
        //    if (ret == DataDefine.eumErrCode.OK)
        //    {
        //        DataDefine.FrameFactory frame = new DataDefine.FrameFactory();
        //        frame.Fun = (byte)DataDefine.CommandID.GetFirmwareVesionRequest;
        //       frame.AccessSelections = 0;
        //        frame.UserData = System.Text.Encoding.Default.GetBytes("F_H_version");
        //       return sendData(BulidFrame(frame), DataDefine.CommandID.GetFirmwareVesionRequest);
        //   }
        //   return ret;
        // }
        /// <summary>
        ///  设置蓝牙名称
        /// </summary>
        public DataDefine.eumErrCode SetBlueToothName(string name)
        {
            DataDefine.eumErrCode ret = ShakeHands();
            if (ret == DataDefine.eumErrCode.OK)
            {
                DataDefine.FrameFactory frame = new DataDefine.FrameFactory();
                frame.Fun = (byte)DataDefine.CommandID.SetBlueToothName;
                frame.AccessSelections = 1;
                frame.UserData = System.Text.Encoding.Default.GetBytes(name);
                return sendData(BulidFrame(frame), DataDefine.CommandID.SetBlueToothName);
            }
            return ret;
        }
        /// <summary>
        ///  设置蓝牙名称
        /// </summary>
        public DataDefine.eumErrCode GetBlueToothName()
        {
            DataDefine.eumErrCode ret = ShakeHands();
            if (ret == DataDefine.eumErrCode.OK)
            {
                DataDefine.FrameFactory frame = new DataDefine.FrameFactory();
                frame.Fun = (byte)DataDefine.CommandID.SetBlueToothName;
                frame.AccessSelections = 0;
                return sendData(BulidFrame(frame), DataDefine.CommandID.SetBlueToothName);
            }
            return ret;
        }
        /// <summary>
        ///  设置设备类型
        /// </summary>
        public DataDefine.eumErrCode SetDeviceType(int DataType = 1)
        {
            DataDefine.eumErrCode ret = ShakeHands();
            if (ret == DataDefine.eumErrCode.OK)
            {
                DataDefine.FrameFactory frame = new DataDefine.FrameFactory();
                frame.Fun = (byte)DataDefine.CommandID.SetDeviceTypeRequest;
                frame.AccessSelections = 1;
                frame.UserData = new byte[] { (byte)DataType };
                return sendData(BulidFrame(frame), DataDefine.CommandID.SetDeviceTypeRequest);
            }
            return ret;
        }
        /// <summary>
        ///  读取设备类型
        /// </summary>
        public DataDefine.eumErrCode GetDeviceType()
        {
            DataDefine.eumErrCode ret = ShakeHands();
            if (ret == DataDefine.eumErrCode.OK)
            {
                DataDefine.FrameFactory frame = new DataDefine.FrameFactory();
                frame.Fun = (byte)DataDefine.CommandID.SetDeviceTypeRequest;
                frame.AccessSelections = 0;
                return sendData(BulidFrame(frame), DataDefine.CommandID.SetDeviceTypeRequest);
            }
            return ret;
        }
        /// <summary>
        ///  气压值校准
        /// </summary>
        public DataDefine.eumErrCode SetPressureCalibration()
        {
            DataDefine.eumErrCode ret = ShakeHands();
            if (ret == DataDefine.eumErrCode.OK)
            {
                DataDefine.FrameFactory frame = new DataDefine.FrameFactory();
                frame.Fun = (byte)DataDefine.CommandID.SetPressureCalibration;
                frame.AccessSelections = 1;
                return sendData(BulidFrame(frame), DataDefine.CommandID.SetPressureCalibration);
            }
            return ret;
        }
        /// <summary>
        ///  生物定标
        /// </summary>
        public DataDefine.eumErrCode SetCalibrationFlag(int CalibrationTyp)
        {
            DataDefine.eumErrCode ret = ShakeHands();
            if (ret == DataDefine.eumErrCode.OK)
            {
                DataDefine.FrameFactory frame = new DataDefine.FrameFactory();
                frame.Fun = (byte)DataDefine.CommandID.SetCalibrationFlag;
                frame.AccessSelections = 1;
                frame.UserData = new byte[] { (byte)CalibrationTyp };
                return sendData(BulidFrame(frame), DataDefine.CommandID.SetCalibrationFlag, false);
            }
            return ret;
        }
        /// <summary>
        ///  设置自动开停采集的时间
        /// </summary>
        public DataDefine.eumErrCode SetAutoRunTime(DateTime startTime, DateTime edTime)
        {
            DataDefine.eumErrCode ret = ShakeHands();
            if (ret == DataDefine.eumErrCode.OK)
            {
                DataDefine.FrameFactory frame = new DataDefine.FrameFactory();
                frame.Fun = (byte)DataDefine.CommandID.SetAutoRunTime;
                frame.AccessSelections = 1;
                string strtime = string.Format("{2} {0:MM dd HH mm} 00 {3} {1:MM dd HH mm} 00", startTime, edTime, startTime.Year % 100, edTime.Year % 100);
                frame.UserData = System.Text.Encoding.ASCII.GetBytes(strtime);
                m_userData.bakLastCastTime = strtime;
                return sendData(BulidFrame(frame), DataDefine.CommandID.SetAutoRunTime);
            }
            return ret;
        }
        /// <summary>
        ///  获取自动开停采集的时间
        /// </summary>
        public DataDefine.eumErrCode GetAutoRunTime()
        {
            DataDefine.eumErrCode ret = ShakeHands();
            if (ret == DataDefine.eumErrCode.OK)
            {
                DataDefine.FrameFactory frame = new DataDefine.FrameFactory();
                frame.Fun = (byte)DataDefine.CommandID.SetAutoRunTime;
                frame.AccessSelections = 0;
                return sendData(BulidFrame(frame), DataDefine.CommandID.SetAutoRunTime);
            }
            return ret;
        }
        /// <summary>
        ///  校时
        /// </summary>
        public DataDefine.eumErrCode BoradCastTime()
        {
            DataDefine.eumErrCode ret = ShakeHands();
            if (ret == DataDefine.eumErrCode.OK)
            {
                return SetCastTimeRequest();
            }
            return ret;
        }
        /// <summary>
        /// 是否存储端口数据
        /// </summary>
        public int IsSaver = 0;
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            try
            {
                m_KillTask = true;
                th.Abort();
            }
            catch { }
            finally
            {
                fnStop = true;
                m_IdentifyRaedy = false;
                m_CastTimeCounter = 0;
                m_CastTimeCompelt = true;
                GC.Collect();
            }
        }
        private string m_basePath = "";
        /// <summary>
        /// 设置存储数据的基础目录
        /// </summary>
        public string BaseSavePath
        {
            set
            {
                m_basePath = value;
                if (m_IndexSaver != null)
                {
                    m_IndexSaver.BaseSavePath = value;
                }
                if (m_reportSaver != null)
                    m_reportSaver.BaseSavePath = value;
            }
        }
        /// <summary>
        /// 显示到视图
        /// </summary>
        public bool isShowToView
        {
            set
            {
                foreach (KeyValuePair<int, DataDefine[]> p in m_dataDefineList)
                {
                    for (int i = 0; i < p.Value.Length; i++)
                    {
                        p.Value[i].IsShowCurve = value;
                    }
                }
            }
        }
        #endregion
    }
}
