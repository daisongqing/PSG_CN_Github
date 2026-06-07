using pSystem.Communication.Com;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.UI.DataModel
{
   public class DeviceOnWLan
    {
       private List<IDevice> m_portClientList = new List<IDevice>();
       private MyTCPServer m_server = null;
       private int m_inPort = 8888;
       private string m_inIP = "";
       private List<string> m_devNames = new List<string>();
       private static DeviceOnWLan m_Default = null;
        private object m_lockObj = new object();
       /// <summary>
       /// 蓝牙网关对象实例
       /// </summary>
       public static DeviceOnWLan Default
       {
           get
           {
               return m_Default ?? (m_Default = new DeviceOnWLan());
           }
       }
       public DeviceOnWLan()
       {
           m_server = new MyTCPServer(m_inPort, m_inIP);
           m_server.RecConnetHandle += m_server_RecConnetHandle;
           m_server.ClientDown += m_server_ClientDown;
       }

        private void m_server_ClientDown(IOClient client)
        {
            StringBuilder showlog = new StringBuilder();
            showlog.AppendLine(client.MatchKey);
            IDevice find = m_portClientList.Find(t => t.MatchKey == client.MatchKey);
            if (find != null)
            {
                try
                {
                    showlog.AppendLine(find.DeviceName);
                    lock (m_lockObj)
                        m_portClientList.Remove(find);
                }
                catch { }
            }
            DataModel.LogInstance.Default.AddLog(string.Format("远程连接掉线 {0}",showlog),pSystem.LogManagement.LogLevel.ERROR);
        }

        private void m_server_RecConnetHandle(IOClient client)
        {
            DataModel.LogInstance.Default.AddLog(string.Format("接收到客户端连接(matchkey:{0})", client.MatchKey), pSystem.LogManagement.LogLevel.DEBUG);
            IDevice find = null;
            if (m_portClientList.Count != 0)
                find = m_portClientList.Find(t => t.MatchKey == client.MatchKey);
            if (find == null)
            {
                lock (m_lockObj)
                    m_portClientList.Add(new BthWLans() { PortClient = client });
                Protocol.ProtocolServer server = new Protocol.ProtocolServer(client, null);
                server.RecDeviceNameEventHandle += server_RecDeviceNameEventHandle;
                System.Threading.Thread.Sleep(2000);
                if (server.getBlueToothName())
                    DataModel.LogInstance.Default.AddLog(string.Format("网关和设备通讯成功 (matchkey:{0})", client.MatchKey), pSystem.LogManagement.LogLevel.DEBUG);
                else
                    DataModel.LogInstance.Default.AddLog(string.Format("网关和设备通讯失败 (matchkey:{0})", client.MatchKey), pSystem.LogManagement.LogLevel.ERROR);
            }
            else
            {
                find.PortClient = client;
            }
        }

        private bool server_RecDeviceNameEventHandle(string matchKey, string devName)
        {
            DataModel.LogInstance.Default.AddLog(string.Format("成功识别客户端连接(matchkey:{0})，其对应设备名称为{1}", matchKey, devName), pSystem.LogManagement.LogLevel.WARN);
            IDevice find = m_portClientList.Find(t => t.MatchKey == matchKey);
            if (find != null)
            {
                find.DeviceName = devName.Trim();
                if (CurrentDevice != null)
                    if (find.DeviceName == CurrentDevice.DeviceName)///触发重连机制
                    {
                        (CurrentDevice as BthWLans).Reconnect((find as BthWLans).PortClient);
                        DataModel.LogInstance.Default.AddLog(string.Format("设备重连 重连的设备名称为{0}", find.DeviceName), pSystem.LogManagement.LogLevel.WARN);
                    }
            }
            return true;///需要清除协议对象资源返回true
        }

        /// <summary>
        /// 开关
        /// </summary>
        public bool Start
       {
           set
           {
               if (value)
               {
                   m_server.StartListener();
               }
               else
               {
                   m_server.StopListener();
                    lock (m_lockObj)
                        m_portClientList.Clear();
               }
           }
       }
       /// <summary>
       /// 获取设备列表
       /// </summary>
       /// <returns></returns>
       public List<IDevice> getDevices()
       {
           return m_portClientList.FindAll(t => t.Description != "");
       }
        /// <summary>
        /// 当前设备
        /// </summary>
        public IDevice CurrentDevice { set; get; }
    }

    public class BthWLans : IDevice
    {
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceAddr { get; set; }
        /// <summary>
        /// 通讯实例
        /// </summary>
        public IOClient PortClient { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string MatchKey
        {
            get
            {
                return PortClient == null ? "" : PortClient.MatchKey;
            }
        }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }
        private DeviceState m_Status = DeviceState.OnLine;
        /// <summary>
        /// 设备状态
        /// </summary>
        public DeviceState Status
        {
            set
            {
                m_Status = value;
            }
            get
            {
                return m_Status;
            }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get
            {
                if (string.IsNullOrEmpty(DeviceName))
                    return "";
                return string.Format("√  {0}", DeviceName);
            }
        }
        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public IDevice Clone()
        {
            BthWLans ret = new BthWLans();
            ret.PortClient = this.PortClient;
            ret.DeviceName = this.DeviceName;
            ret.Status = this.Status;
            return ret;
        }

        /// <summary>
        /// 网络重连处理委托方法
        /// </summary>
        /// <param name="data"></param>
        public delegate void ReconnectDelegate(IOClient newportClient);

        public event ReconnectDelegate ReconnectHandle;
        /// <summary>
        /// 重连时触发
        /// </summary>
        /// <param name="client"></param>
        public void Reconnect(IOClient newportClient)
        {
            if (ReconnectHandle != null)
                ReconnectHandle.Invoke(newportClient);
        }
    }
}
