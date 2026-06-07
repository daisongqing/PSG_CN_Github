//*************************************************************************************
//
//文件名(File Name):         DeviceOnLine.cs
//
//功能描述(Description)：    通过查找电脑的注册表,查找相关设备并且检查设备的在线状态，供外部调用。
//
//数据表(Tables):            nothing
//
//作者(Author):              
//
//日期(Create Date):         2021.11.25
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

namespace AwareTec.Polysmith.UI.DataModel
{
    /// <summary>
    /// 通过查找电脑的注册表,查找相关设备并且检查设备的在线状态，供外部调用。
    /// </summary>
    public class DeviceOnLine
    {
        #region 私有变量
        private static DeviceOnLine m_Default = null;
        private BluetoothClient m_BthCleint = null;
        private bool m_Start = false;
        private bool m_LoadReady = false;
        private object m_Obj = new object();
        private System.Threading.Thread m_MainThread = null;
        private bool m_KillTask = true;
        private List<BthCommans> m_DeviceList = null;
        private int m_Count = 1;
        private int m_TotalCount = 1;
        private List<string> m_RemovePortList = new List<string>();
        private object m_ObjLock = new object();
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
        /// 离线时发生
        /// </summary>
        public event Action OffLineHandler;
        #endregion

        #region 初始化和释放资源
        /// <summary>
        /// 单例模式
        /// </summary>
        public static DeviceOnLine Default
        {
            get
            {
                return m_Default ?? (m_Default = new DeviceOnLine());
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public DeviceOnLine()
        {
            m_DeviceList = new List<BthCommans>();
            try
            {
                m_BthCleint = new BluetoothClient();
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
            m_MainThread.Abort();
            m_MainThread.DisableComObjectEagerCleanup();
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 任务执行器
        /// </summary>
        private void TaskStart()
        {
            m_MainThread = new System.Threading.Thread(() =>
            {
                while (!this.m_KillTask)
                {
                    try
                    {
                        //todo deviceonline暂时不稳定
                        /*
                        if (Start)
                        {
                            DateTime ff = DateTime.Now;
                            //获取电脑的蓝牙是否开启
                            BluetoothRadio bluetoothRadio = BluetoothRadio.PrimaryRadio;
                            if (bluetoothRadio == null)
                            {
                                AhDung.MessageTip.ShowError(string.Format("本地蓝牙未开启"));
                                System.Threading.Thread.Sleep(10000);
                                continue;
                            }
                            m_DeviceList = GetAllBthDevices();
                            Console.WriteLine(string.Format("扫描一遍蓝牙设备用时{0}s", (DateTime.Now - ff).TotalSeconds));
                            System.Threading.Thread.Sleep(5000);
                        }
                        */
                        #region 彭工方法
                        
                        if ((!this.m_LoadReady || !Channel.Default.IsRealTimeView) && this.m_Start)
                        {
                            IEnumerable<BthCommans> bthDevices = this.GetAllBthDevices();
                            IEnumerable<string> source = from t in bthDevices
                                                         select t.ID;
                            IEnumerable<string> enumerable = from t in this.m_DeviceList
                                                             select t.ID;
                            new List<BthCommans>(bthDevices.Count<BthCommans>());
                            List<string> removeids = new List<string>();
                            foreach (string text in enumerable)
                            {
                                if (!source.Contains(text))
                                {
                                    removeids.Add(text);
                                }
                            }
                            using (IEnumerator<BthCommans> enumerator2 = bthDevices.GetEnumerator())
                            {
                                while (enumerator2.MoveNext())
                                {
                                    BthCommans bt = enumerator2.Current;
                                    if (!enumerable.Contains(bt.ID))
                                    {
                                        if (bt.PortName == null)
                                        {
                                            this.m_RemovePortList.Add(bt.ID);
                                        }
                                        this.m_DeviceList.Add(bt);
                                    }
                                    else if (this.m_RemovePortList.Contains(bt.ID) && bt.PortName != null)
                                    {
                                        this.m_RemovePortList.Remove(bt.ID);
                                        this.m_DeviceList.Find((BthCommans t) => t.ID == bt.ID).PortName = bt.PortName;
                                    }
                                }
                            }
                            if (removeids.Count > 0)
                            {
                                this.m_DeviceList.RemoveAll((BthCommans t) => removeids.Contains(t.ID));
                            }
                            if (this.m_Count == this.m_TotalCount)
                            {
                                this.m_Count = 1;
                                if (DeviceOnLine.IsWin7)
                                {
                                    Task[] array = new Task[this.m_DeviceList.Count];
                                    for (int i = 0; i < this.m_DeviceList.Count; i++)
                                    {
                                        array[i] = new Task(delegate (object obj)
                                        {
                                            int index = (int)obj;
                                            if (this.m_DeviceList[index].Status != DeviceState.Running && !string.IsNullOrEmpty(this.m_DeviceList[index].PortName))
                                            {
                                                MySerialPort mySerialPort = new MySerialPort(this.m_DeviceList[index].PortName);
                                                Console.WriteLine(string.Format("后台线程创建一个{0}的串口实例", this.m_DeviceList[index].PortName));
                                                try
                                                {
                                                    bool flag2 = mySerialPort.Open(new object[0]);
                                                    if (flag2)
                                                    {
                                                        System.Threading.Thread.Sleep(10);
                                                        mySerialPort.Close();
                                                    }
                                                    if (this.m_DeviceList[index].Status != DeviceState.Running)
                                                    {
                                                        this.m_DeviceList[index].Status = (flag2 ? DeviceState.OnLine : DeviceState.OffLine);
                                                    }
                                                }
                                                catch
                                                {
                                                    if (this.m_DeviceList[index].Status != DeviceState.Running)
                                                    {
                                                        this.m_DeviceList[index].Status = DeviceState.OffLine;
                                                    }
                                                }
                                            }
                                        }, i);
                                        array[i].Start();
                                        System.Threading.Thread.Sleep(2);
                                    }
                                    Task.WaitAll(array);
                                    for (int j = 0; j < array.Length; j++)
                                    {
                                        array[j].Dispose();
                                    }
                                    goto IL_420;
                                }
                                DateTime d = DateTime.Now.AddSeconds(-10.0);
                                if (this.m_BthCleint == null)
                                {
                                    goto IL_420;
                                }
                                try
                                {
                                    BluetoothDeviceInfo[] array2 = this.m_BthCleint.DiscoverDevices();
                                    BluetoothDeviceInfo[] array3 = array2;
                                    for (int k = 0; k < array3.Length; k++)
                                    {
                                        BluetoothDeviceInfo item = array3[k];
                                        BthCommans bthCommans = this.m_DeviceList.Find((BthCommans t) => t.DeviceName == item.DeviceName);
                                        if (bthCommans != null)
                                        {
                                            TimeSpan timeSpan = item.LastSeen - d;
                                            bool flag = timeSpan.TotalMilliseconds > 0.0;
                                            if (!flag)
                                            {
                                                double num = timeSpan.TotalHours + 8.0;
                                                if (num < 1.0 && num > 0.0)
                                                {
                                                    flag = ((item.LastSeen.AddHours(8.0) - d).TotalMilliseconds > 0.0);
                                                }
                                            }
                                            if (bthCommans.Status != DeviceState.Running)
                                            {
                                                bthCommans.Status = (flag ? DeviceState.OnLine : DeviceState.OffLine);
                                            }
                                        }
                                    }
                                    goto IL_420;
                                }
                                catch (Exception)
                                {
                                    goto IL_420;
                                }
                            }
                            this.m_Count++;
                        }
                        IL_420:;
                        
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                    }
                    System.Threading.Thread.Sleep(10000);
                }
            });
            m_MainThread.IsBackground = true;
            m_MainThread.SetApartmentState(System.Threading.ApartmentState.MTA);
            m_MainThread.Start();
        }
        /// <summary>
        /// 获取所有蓝牙设备
        /// </summary>
        /// <returns></returns>
        private IEnumerable<BthCommans> GetAllBthDevices()
        {
            //todo 暂时不稳定
            /*
            List<BthCommans> oldblueToothDevices = m_DeviceList;
            List<BthCommans> blueToothDevices = new List<BthCommans>();
            if (m_BthCleint == null)
                return oldblueToothDevices;
            //第三方搜索到的蓝牙设备信息
            BluetoothDeviceInfo[] BluetoothDeviceInfos = this.m_BthCleint.DiscoverDevices();
            foreach (BluetoothDeviceInfo bluetoothDeviceInfo in BluetoothDeviceInfos)
            {
                if (bluetoothDeviceInfo.ClassOfDevice.Device != DeviceClass.AudioVideoHeadset || bluetoothDeviceInfo.Remembered == false)
                    continue;
                BthCommans blueToothDevice = new BthCommans();
                blueToothDevice.DeviceAddr = bluetoothDeviceInfo.DeviceAddress.ToString();
                blueToothDevice.DeviceName = bluetoothDeviceInfo.DeviceName;
                blueToothDevice.Status = DeviceState.None;
                if ((DateTime.Now - bluetoothDeviceInfo.LastSeen).TotalSeconds <= 20)
                {
                    blueToothDevice.Status = DeviceState.OnLine;
                }
                if (bluetoothDeviceInfo.InstalledServices.Length > 0)
                    blueToothDevice.ID = bluetoothDeviceInfo.InstalledServices[0].ToString();
                blueToothDevices.Add(blueToothDevice);
            }
            if (blueToothDevices.Count > 0)
            {
                //通过注册表找到添加的设备对应的COM口
                Microsoft.Win32.RegistryKey reKeyLoMa = Microsoft.Win32.Registry.LocalMachine;
                Microsoft.Win32.RegistryKey reKeySystem = reKeyLoMa.OpenSubKey("SYSTEM");
                string[] controlKeys = new string[] { "ControlSet001", "ControlSet002", "ControlSet003" };
                foreach (string controlsetkey in controlKeys)
                {
                    Microsoft.Win32.RegistryKey reKeyCoSet = reKeySystem.OpenSubKey(controlsetkey);
                    if (reKeyCoSet == null)
                        continue;
                    Microsoft.Win32.RegistryKey reKeyEnum = reKeyCoSet.OpenSubKey("Enum");
                    if (reKeyEnum == null)
                        continue;
                    Microsoft.Win32.RegistryKey reKeyBTHEunm = reKeyEnum.OpenSubKey("BTHENUM");
                    if (reKeyBTHEunm == null)
                        continue;
                    string[] BTHEunmames = reKeyBTHEunm.GetSubKeyNames();
                    foreach (string btheunmname in BTHEunmames)
                    {
                        string[] subkeys = reKeyBTHEunm.OpenSubKey(btheunmname).GetSubKeyNames();
                        foreach (string subkey in subkeys)
                        {
                            try
                            {
                                string[] ss = subkey.Split('&');
                                if (ss.Length > 0)
                                {
                                    string id = ss[ss.Length - 1].Split('_')[0];
                                    BthCommans find = blueToothDevices.Find(t => t.DeviceAddr == id.ToUpper());
                                    if (find != null)
                                    {
                                        var value = reKeyBTHEunm.OpenSubKey(btheunmname).OpenSubKey(subkey).GetValue("FriendlyName");
                                        if (value != null)
                                        {
                                            string[] strLL = value.ToString().Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                                            if (strLL.Length > 1)
                                                find.PortName = strLL[1];
                                        }
                                    }
                                }
                            }
                            catch (Exception ee)
                            {
                                Console.Write(ee.Message);
                            }
                        }
                    }
                }
            }
            else
            {
                //todo 开启软件之后添加新设备会报错
                return oldblueToothDevices;
            }
            return blueToothDevices;
            */
            #region 彭工方法
            
            Microsoft.Win32.RegistryKey hklm = Microsoft.Win32.Registry.LocalMachine;
            Microsoft.Win32.RegistryKey systemnode = hklm.OpenSubKey("SYSTEM");
            string[] controlKey = new string[] { "ControlSet001", "ControlSet002", "ControlSet003" };
            List<BthCommans> bthDevices = new List<BthCommans>();
            for (int m = 0; m < controlKey.Length; m++)
            {
                bthDevices.Clear();
                //打开"SYSTEM"子健
                Microsoft.Win32.RegistryKey ControlSet001 = systemnode.OpenSubKey(controlKey[m]);
                Microsoft.Win32.RegistryKey devEnum = ControlSet001.OpenSubKey("Enum");

                Microsoft.Win32.RegistryKey Parameters = ControlSet001.OpenSubKey("services").OpenSubKey("BTHPORT").OpenSubKey("Parameters");
                Microsoft.Win32.RegistryKey devices = Parameters.OpenSubKey("Devices");
                if (devices == null)
                    continue;
                string[] devs = devices.GetSubKeyNames();
                foreach (string dev in devs)
                {
                    try
                    {
                        BthCommans bth = new BthCommans();
                        bth.ID = dev.ToUpper();
                        bth.Status = DeviceState.None;
                        Microsoft.Win32.RegistryKey devkey = devices.OpenSubKey(dev);
                        byte[] name = devkey.GetValue("Name") as byte[];
                        if (name == null)
                            continue;
                        bth.DeviceName = System.Text.Encoding.Default.GetString(name).Replace("\0", "");
                        bthDevices.Add(bth);
                    }
                    catch (Exception ee)
                    {
                        Console.Write(ee.Message);
                    }
                }
                Microsoft.Win32.RegistryKey bthdevEnum = devEnum.OpenSubKey("BTHENUM");
                if (bthdevEnum != null)
                {
                    string[] keyvalues = bthdevEnum.GetSubKeyNames();
                    foreach (string key in keyvalues)
                    {
                        Microsoft.Win32.RegistryKey subKeys = bthdevEnum.OpenSubKey(key);
                        string[] vals = key.Split(new char[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] subkeysList = subKeys.GetSubKeyNames();
                        foreach (string subkeysvalue in subkeysList)
                        {
                            try
                            {
                                string[] ss = subkeysvalue.Split('&');
                                if (ss.Length > 0)
                                {
                                    string id = ss[ss.Length - 1].Split('_')[0];
                                    BthCommans find = bthDevices.Find(t => t.ID == id.ToUpper());
                                    if (find != null)
                                    {
                                        find.ParentID = vals[0];
                                        var value = subKeys.OpenSubKey(subkeysvalue).GetValue("FriendlyName");
                                        if (value != null)
                                        {
                                            string[] strLL = value.ToString().Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                                            if (strLL.Length > 1)
                                                find.PortName = strLL[1];
                                        }
                                    }
                                }
                            }
                            catch (Exception ee)
                            {
                                Console.Write(ee.Message);
                            }
                        }
                    }
                }
                Microsoft.Win32.RegistryKey LocalServices = Parameters.OpenSubKey("LocalServices");
                #region 老的判断方法，暂时不用
                //for (int i = 0; i < bthDevices.Count; i++)
                //{
                //    try
                //    {
                //        if (bthDevices[i].ParentID == null)
                //            continue;
                //        Microsoft.Win32.RegistryKey loc = LocalServices.OpenSubKey("{" + bthDevices[i].ParentID + "}");
                //        if (loc == null)
                //            continue;
                //        string[] locKeys = loc.GetSubKeyNames();
                //        foreach (string lockey in locKeys)
                //        {
                //            try
                //            {
                //                Microsoft.Win32.RegistryKey sKey = loc.OpenSubKey(lockey);
                //                byte[] locID = sKey.GetValue("AssocBdAddr") as byte[];
                //                var ids = locID.Reverse().Select(t => t.ToString("X2"));
                //                string strlocID = "";
                //                foreach (string id in ids)
                //                {
                //                    strlocID = string.Format("{0}{1}", strlocID, id);
                //                }
                //                if (strlocID.Contains(bthDevices[i].ID))
                //                {
                //                    if (sKey.GetValue("Enabled").ToString() == "1")
                //                    {
                //                        bthDevices[i].Status = DeviceState.Enable;
                //                    }
                //                    break;
                //                }
                //            }
                //            catch (Exception ee)
                //            {
                //                Console.Write(ee.Message);
                //            }
                //        }
                //    }
                //    catch (Exception ee)
                //    {
                //        Console.Write(ee.Message);
                //    }
                //}
                #endregion
                #region 新的测试
                var group = bthDevices.GroupBy(t => t.ParentID);
                List<string> parents = group.Select(t => t.Key).ToList();
                List<string> enables = new List<string>();
                for (int i = 0; i < parents.Count; i++)
                {
                    if (!string.IsNullOrEmpty(parents[i]))
                    {
                        Microsoft.Win32.RegistryKey loc = LocalServices.OpenSubKey("{" + parents[i] + "}");
                        if (loc == null)
                            continue;
                        string[] locKeys = loc.GetSubKeyNames();
                        foreach (string lockey in locKeys)
                        {
                            try
                            {
                                Microsoft.Win32.RegistryKey sKey = loc.OpenSubKey(lockey);
                                byte[] locID = sKey.GetValue("AssocBdAddr") as byte[];
                                var ids = locID.Reverse().Select(t => t.ToString("X2"));
                                string strlocID = "";
                                foreach (string id in ids)
                                {
                                    strlocID = string.Format("{0}{1}", strlocID, id);
                                }
                                if (sKey.GetValue("Enabled").ToString() == "1")
                                {
                                    enables.Add(strlocID);
                                }
                            }
                            catch (Exception ee)
                            {
                                Console.Write(ee.Message);
                            }
                        }
                    }
                }
                enables = enables.Distinct().ToList();
                for (int i = 0; i < bthDevices.Count; i++)
                {
                    var find = enables.Find(t => t.Contains(bthDevices[i].ID));
                    if (find != null)
                    {
                        bthDevices[i].Status = DeviceState.Enable;
                    }
                }
                #endregion
                break;
            }
            return bthDevices.Where(t => t.Status == DeviceState.Enable);
            
            #endregion
        }
        #endregion

        #region 公有方法
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
        /// <summary>
        /// 更新设备状态
        /// </summary>
        /// <param name="portname"></param>
        /// <param name="status"></param>
        public void UpdateDevices(string portname, DeviceState status)
        {
            lock (m_ObjLock)
            {
                BthCommans find = m_DeviceList.Find(t => t.PortName == portname);
                if (find != null) find.Status = status;
            }
        }
        #endregion

    }
    /// <summary>
    /// 蓝牙设备类
    /// </summary>
    public class BthCommans:IDevice
    {
        #region 公有属性
        public string DeviceAddr { get; set; }
        /// <summary>
        /// 通讯实例
        /// </summary>
        public IOClient PortClient { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public string ParentID { set; get; }
        /// <summary>
        /// 蓝牙设备的唯一ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 通讯端口号
        /// </summary>
        public string PortName { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string MatchKey
        {
            get
            {
                return PortName == null ? "" : PortName;
            }
        }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }
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
                return string.Format("{1}  {0}", DeviceName, Status == DeviceState.OnLine ? "√" : "×");
            }
        }
        #endregion
        #region 私有属性
        private DeviceState m_Status = DeviceState.OffLine;
        #endregion
        #region 公有方法
        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public IDevice Clone()
        {
            BthCommans ret = new BthCommans();
            ret.ID = this.ID;
            ret.DeviceName = this.DeviceName;
            ret.PortName = this.PortName;
            ret.ParentID = this.ParentID;
            ret.Status = this.Status;
            return ret;
        }
        #endregion
    }
}
