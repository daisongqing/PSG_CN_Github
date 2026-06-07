//*************************************************************************************
//
//文件名(File Name):         $safeitemrootname$
//
//功能描述(Description)：    
//
//数据表(Tables):            
//
//作者(Author):              力力
//
//日期(Create Date):         $time$
//
//参考文档(Reference)(可选)： 该档所对应的分析文档，设计文档。
//
//引用(Using)(可选)﹕         开发的系统中引用其它系统的Dll、对象时，要列出其对应的出处，是否与系统有关(不清楚的可以不写)，以方便制作安装档。
//
//修改记录(Revicion History):
//      R1:
//          修改作者:
//          修改日期:
//          修改理由:
//
//      R2:
//
//*************************************************************************************

using pSystem.Communication.Com;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System.Threading;
using System.IO;

namespace AwareTec.Polysmith.UI.DataModel
{
    /// <summary>
    /// 扫描设备和检查在线状态服务
    /// </summary>
    public class DeviceScan
    {
        #region 私有变量
        private static DeviceScan m_Default = null;
        private bool m_Start = false;
        private BluetoothClient m_BlueToothClient = null;
        private bool m_LoadReady = false;
        private object m_Obj = new object();
        private System.Threading.Thread m_MainThread = null;
        private bool m_KillTask = true;
        private List<BthCommans> m_DeviceList = null;
        private int m_Count = 1;
        private int m_TotalCount = 1;
        private List<string> m_RemovePortList = new List<string>();
        private List<ScanEntly> m_AllThread = new List<ScanEntly>();
        private object m_ObjLock = new object();
        private string m_SuccessPortName = "";
        private List<byte> m_PortReceiveDataList = new List<byte>();
        #endregion

        #region 公有变量
        /// <summary>
        /// 开启服务
        /// </summary>
        public bool Start
        {
            set
            {
                m_Start = value;
            }
            get
            {
                return m_Start;
            }
        }
        /// <summary>
        /// 完成登录标志
        /// </summary>
        public bool LoadReady
        {
            set
            {
                lock (m_Obj)
                    m_LoadReady = value;
            }
            get
            {
                return m_LoadReady;
            }
        }
        /// <summary>
        /// 是否是win7系统
        /// </summary>
        public static bool IsWin7 = Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 1;
        /// <summary>
        /// 事件 用来标记 离线时发生
        /// </summary>
        public event Action OffLineHandler;
        #endregion

        #region 初始化和释放
        /// <summary>
        /// 单例模式
        /// </summary>
        public static DeviceScan Default
        {
            get
            {
                return m_Default ?? (m_Default = new DeviceScan());
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public DeviceScan()
        {
            m_DeviceList = new List<BthCommans>();
            try
            {
                m_BlueToothClient = new BluetoothClient();
            }
            catch
            {
                AhDung.MessageTip.ShowError("未检测到本地蓝牙适配器，请手动开启！", 1500);
            }
            if (IsWin7)
            {
                m_TotalCount = m_Count = 20;
            }
            else
            {
                m_TotalCount = m_Count = 1;
            }
            m_KillTask = false;
            TaskStart();
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            m_KillTask = true;
            if (m_MainThread != null)
            {
                m_MainThread.Abort();
                m_MainThread.DisableComObjectEagerCleanup();
            }
        }
        #endregion

        #region 私有类
        /// <summary>
        /// 存放自动扫描的线程和Client口
        /// </summary>
        private class ScanEntly
        {
            /// <summary>
            /// 线程
            /// </summary>
            public Thread Thread { set; get; }
            /// <summary>
            /// client口
            /// </summary>
            public IOClient Client { set; get; }
            /// <summary>
            /// 是否接受到数据
            /// </summary>
            public bool IsReceiveData = false;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 任务执行器
        /// </summary>
        private void TaskStart()
        {
            //m_MainThread = new System.Threading.Thread(() =>
            //{
            //    ReadMemoryScanDevice();
            //    string[] comPortsNamesArr = System.IO.Ports.SerialPort.GetPortNames();
            //    for (int i = comPortsNamesArr.Length - 1; i >= 0; i--)
            //    {
            //        System.Threading.Thread thread = new Thread(new ParameterizedThreadStart(AddBthDevice));
            //        thread.Name = comPortsNamesArr[i];
            //        thread.IsBackground = true;
            //        thread.Start(comPortsNamesArr[i].Trim());
            //        m_AllThread.Add(new ScanEntly() { Thread = thread });
            //    }
            //    Task.Factory.StartNew(() =>
            //    {
            //        int connectionCount = 0;
            //        while (connectionCount < 5)
            //        {
            //            Thread.Sleep(1000);
            //            for (int i = 0; i < m_AllThread.Count; i++)
            //            {
            //                if (m_AllThread[i].IsReceiveData)
            //                    break;
            //            }
            //            connectionCount++;
            //        }
            //        for (int i = 0; i < m_AllThread.Count; i++)
            //        {
            //            //if (!m_AllThread[i].IsReceiveData && m_AllThread[i].Client != null)
            //            //{
            //            //    m_AllThread[i].Client.Close();
            //            //    m_AllThread[i].Thread.Abort();
            //            //}
            //            if (m_AllThread[i].Client != null)
            //            {
            //                m_AllThread[i].Client.Close();
            //            }
            //            m_AllThread[i].Thread.Abort();
            //        }
            //    });
            //    while (!this.m_KillTask)
            //    {
            //        try
            //        {
            //            if ((!this.m_LoadReady || !Channel.Default.IsRealTimeView) && this.m_Start)
            //            {
            //                for (int i = 0; i < m_AllThread.Count; i++)
            //                {
            //                    if (m_AllThread[i].IsReceiveData)
            //                    {
            //                        AddBthDevice(m_AllThread[i].Client.MatchKey);
            //                        //System.Threading.Thread thread = new Thread(new ParameterizedThreadStart(AddBthDevice));
            //                        //thread.Name = m_AllThread[i].Client.MatchKey;
            //                        //thread.IsBackground = true;
            //                        //thread.Start(m_AllThread[i].Client.MatchKey);
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                for (int i = 0; i < m_AllThread.Count; i++)
            //                {
            //                    if (m_AllThread[i].Client != null)
            //                    {
            //                        m_AllThread[i].Client.Close();
            //                    }
            //                    m_AllThread[i].Thread.Abort();
            //                }
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            DataModel.LogInstance.Default.AddLog(string.Format("扫描出错 错误信息为{0}", ex.Message));
            //        }
            //        System.Threading.Thread.Sleep(1000);
            //    }
            //});
            //m_MainThread.IsBackground = true;
            //m_MainThread.SetApartmentState(System.Threading.ApartmentState.MTA);
            //m_MainThread.Start();
        }

        /// <summary>
        /// 从记忆文件中读取HST扫描过得设备
        /// </summary>
        private void ReadMemoryScanDevice()
        {
            if (File.Exists(Channel.Default.MemoryDeviceAddress))
            {
                using (FileStream fs = new FileStream(Channel.Default.MemoryDeviceAddress, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        StringBuilder sb = new StringBuilder();
                        string line = sr.ReadLine();
                        while (!string.IsNullOrEmpty(line))
                        {
                            sb.AppendFormat("{0}\r\n", line);
                            line = sr.ReadLine();
                        }
                        string[] devicenumber = sb.ToString().Split(new string[] { "********************************\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        if (devicenumber.Length > 0)
                        {
                            for (int i = 0; i < devicenumber.Length; i++)
                            {
                                BthCommans MemoryDevice = new BthCommans();
                                string[] onedevicedetails = devicenumber[i].Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                                for (int k = 0; k < onedevicedetails.Length; k++)
                                {
                                    string[] ss = onedevicedetails[k].Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                                    if (ss.Length == 2)
                                    {
                                        if (ss[0] == "DeviceName")
                                        {
                                            MemoryDevice.DeviceName = ss[1];
                                        }
                                        else if (ss[0] == "BlueToothAddress")
                                        {
                                            MemoryDevice.ID = ss[1];
                                        }
                                        else if (ss[0] == "PortName")
                                        {
                                            MemoryDevice.PortName = ss[1];
                                        }
                                    }
                                }
                                m_DeviceList.Add(MemoryDevice);
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 线程发指令，将设备添加到列表
        /// </summary>
        /// <param name="getportname"></param>
        private void AddBthDevice(object getportname)
        {
            string myportname = (string)getportname;
            IOClient myioclient = null;
            myioclient = new MySerialPort(myportname)
            {
                BaudRate = 460800,  //波特率
            };
            myioclient.DataReceiveHandle += MyIOClient_DataReceiveHandle;
            if (!myioclient.IsConnected)
                myioclient.Open();
            ScanEntly scanfindently = null;
            lock (new object())
            {
                scanfindently = m_AllThread.Find(t => t.Thread.Name == myportname);
                if (scanfindently != null)
                {
                    scanfindently.Client = myioclient;
                }
            }
            if (myioclient.DataSend(ASCIIEncoding.ASCII.GetBytes("AT+SCAN=1\r\n")))
            {
                System.Threading.Thread.Sleep(2000);
                Task.Factory.StartNew(() =>
                {
                    string receivedata = System.Text.Encoding.Default.GetString(m_PortReceiveDataList.ToArray());
                    string[] devicealldetails = receivedata.Split(new string[] { "+SCAN" }, StringSplitOptions.None);
                    if (devicealldetails.Length > 1)
                    {
                        for (int j = 0; j < devicealldetails.Length; j++)
                        {
                            string[] devicedetails = devicealldetails[j].Split(',');
                            if (devicedetails.Length >= 5)
                            {
                                //去重(按蓝牙地址）
                                if (!m_DeviceList.Exists(t => t.ID == devicedetails[1]))
                                {
                                    if (devicedetails[3].Contains("HST"))
                                    {
                                        BthCommans newdevice = new BthCommans();
                                        newdevice.PortName = m_SuccessPortName;
                                        newdevice.Status = DataModel.DeviceState.Enable;
                                        newdevice.ID = devicedetails[1];
                                        newdevice.DeviceName = devicedetails[3];
                                        m_DeviceList.Add(newdevice);
                                        WriteMemoryDevice(newdevice);
                                    }
                                }
                                //是否在线还是离线
                                else
                                {
                                    m_DeviceList.Find(t => t.ID == devicedetails[1]).Status = DeviceState.OnLine;
                                }
                            }
                        }
                    }
                    if (m_PortReceiveDataList.Count > 1000)
                        m_PortReceiveDataList.Clear();
                });
            }
        }
        /// <summary>
        /// 将HST扫描过的设备写入到记忆文件里面
        /// </summary>
        private void WriteMemoryDevice(BthCommans newdevice)
        {
            if (!File.Exists(Channel.Default.MemoryDeviceAddress))
                File.Create(Channel.Default.MemoryDeviceAddress);
            using (FileStream fs = new FileStream(Channel.Default.MemoryDeviceAddress, FileMode.Append))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("[DeviceName]{0}\r\n", string.Join(";", newdevice.DeviceName));
                    sb.AppendFormat("[BlueToothAddress]{0}\r\n", string.Join(";", newdevice.ID));
                    sb.AppendFormat("[PortName]{0}\r\n", string.Join(";", newdevice.PortName));
                    sb.Append("********************************\r\n");
                    sw.Write(sb.ToString());
                    sw.AutoFlush = true;
                    sw.Close();
                }
                fs.Close();
            }
        }
        /// <summary>
        /// 端口接受到数据
        /// </summary>
        /// <param name="receivedata"></param>
        /// <param name="ioclient"></param>
        private void MyIOClient_DataReceiveHandle(byte[] receivedata, IOClient ioclient)
        {
            m_PortReceiveDataList.AddRange(receivedata);
            if (receivedata != null)
            {
                m_SuccessPortName = ioclient.MatchKey;
                lock (new object())
                {
                    ScanEntly find = m_AllThread.Find(t => t.Thread.Name == ioclient.MatchKey);
                    if (find != null)
                    {
                        find.IsReceiveData = true;
                    }
                }
            }
        }
        /// <summary>
        /// 获取所有蓝牙设备
        /// </summary>
        /// <returns></returns>
        private IEnumerable<BthCommans> GetBthDevices()
        {
            return m_DeviceList.Where(t => t.Status == DeviceState.Enable);
        }
        #endregion

        #region 公有方法
        /// <summary>
        /// 更新设备状态
        /// </summary>
        /// <param name="portname"></param>
        /// <param name="status"></param>
        public void UpDateDevices(string portname, DeviceState status)
        {
            lock (m_ObjLock)
            {
                BthCommans findDevice = m_DeviceList.Find(t => t.PortName == portname);
                if (findDevice != null) findDevice.Status = status;
            }
        }

        /// <summary>
        /// 获取最新设备信息列表
        /// </summary>
        /// <returns></returns>
        public List<IDevice> GetDevices()
        {
            List<IDevice> ret = new List<IDevice>();
            List<IDevice> ret2 = new List<IDevice>();
            lock (m_ObjLock)
            {
                for (int i = 0; i < m_DeviceList.Count; i++)
                {
                    if (m_DeviceList[i].Status == DeviceState.OnLine)
                        ret.Add(m_DeviceList[i].Clone());
                    else
                        ret2.Add(m_DeviceList[i].Clone());
                }
            }
            ret.AddRange(ret2);
            return ret;
        }
        #endregion
    }
}
