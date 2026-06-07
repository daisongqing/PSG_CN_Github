using System;

namespace AwareTec.Polysmith.Protocol
{
    /// <summary>
    /// 用户数据
    /// </summary>
    public class UserDataDefine
    {
        /// <summary>
        /// 固件版本号
        /// </summary>
        public string FirmwareVesion { set; get; }
        /// <summary>
        /// 软件版本号
        /// </summary>
        public string SoftWareVesion { set; get; }
        /// <summary>
        /// 最近一次预校时时间值
        /// </summary>
        internal string bakLastCastTime { set; get; }
        /// <summary>
        /// 最近一次校时时间值
        /// </summary>
        public string LastCastTime { set; get; }
        private string m_AutoRunTime = "";
        /// <summary>
        /// 设置的定时开关灯时间字符
        /// </summary>
        public string AutoRunTime
        {
            set
            {
                m_AutoRunTime = value;
            }
            get
            {
                return m_AutoRunTime;
            }
        }
        /// <summary>
        /// 是否初始化完成
        /// </summary>
        public bool InitReady = false;
        /// <summary>
        /// 电池剩余容量
        /// </summary>
        public float BatteryCapacity { set; get; }
        /// <summary>
        /// 设备忙标识
        /// </summary>
        public bool DeviceBusy { set; get; }
        /// <summary>
        /// 设备的SN号
        /// </summary>
        public string DeviceSNCode { set; get; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public int DeviceType { set; get; }
        /// <summary>
        /// 蓝牙设备名称
        /// </summary>
        public string BlueToothName { set; get; }
        /// <summary>
        /// 获取关键字
        /// </summary>
        public string MatchKey { private set; get; }
        private string m_PatientInfo = "";
        /// <summary>
        /// 病例信息
        /// </summary>
        public string PatientInfo
        {
            set
            {
                m_PatientInfo = value;
                string[] ss = value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (ss.Length > 4)
                {
                    MatchKey = ss[4];
                }
                else
                {
                    MatchKey = "";
                }
            }
            get
            {
                return m_PatientInfo;
            }
        }
        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public UserDataDefine Clone()
        {
            UserDataDefine dd = new UserDataDefine();
            dd.FirmwareVesion = this.FirmwareVesion;
            dd.SoftWareVesion = this.SoftWareVesion;
            dd.BatteryCapacity = this.BatteryCapacity;
            dd.DeviceBusy = this.DeviceBusy;
            return dd;
        }
    }
}
