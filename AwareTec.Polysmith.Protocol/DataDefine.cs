using System;
namespace AwareTec.Polysmith.Protocol
{
    /// <summary>
    /// 数据自定义结构
    /// </summary>
    public class DataDefine
    {
        /// <summary>
        /// 控制字定义
        /// </summary>
        public enum CommandID
        {
            /// <summary>
            /// 无指令动作
            /// </summary>
            None = 0,
            /// <summary>
            /// 主送上送uint16数据, 200个胸部呼吸数据+200个腹部呼吸数据+2个右腿动数据+2个左腿动数据
            /// </summary>
            PushInt16_404 = 0x02,
            /// <summary>
            /// 主动上送气流数据（int16 150个热敏数据+ 150个压力数据）
            /// </summary>
            PushMuzzleFlow = 0x03,
            /// <summary>
            /// 主动上送Int16数据,20个心电数据+20个下颌肌电数据+2*20个眼电数据+6*20个脑电数据+ 20个鼾声数据
            ///  更正为Int16数据类型 2018.08.23
            /// </summary>
            PushInt32_220 = 0x04,
            /// <summary>
            /// 主动上送Int16数据,主要是2个血氧数据+ 1个环境光数据+1体位数据（仰：0x01  左：0x02  右：0x03  趴：0x04  坐：0x05）
            /// 更正为Int16数据类型 2018.08.23
            /// </summary>
            PushInt16_4 = 0x01,
            /// <summary>
            /// 握手（上位机发送“link_FSA”请求连接，下位机在1S内回复“consent_FSA”）
            /// </summary>
            IdentifyRequest = 0x20,
            /// <summary>
            /// 查询固件版本（上位机下发“F_H_version”，下位机在1S内回复软硬件版本“x.xx.xx”“x.xx.xx”）
            /// </summary>
            GetFirmwareVesionRequest = 0x21,
            /// <summary>
            /// 校时
            /// </summary>
            SetCastTimeRequest = 0x22,
            /// <summary>
            /// 继续监听
            /// </summary>
            SetContinueSampleRequest = 0x23,
            /// <summary>
            /// 开始测试
            /// </summary>
            SetStartSampleRequest = 0x24,
            /// <summary>
            /// 停止测试
            /// </summary>
            SetStopSampleRequest = 0x25,
            /// <summary>
            /// 设置采样频率
            /// </summary>
            SetSampleFrequencyRequest = 0x26,
            /// <summary>
            /// 设置或读取病人ID
            /// </summary>
            GetPatientIDRequest = 0x27,
            /// <summary>
            /// 设置或读取设备ID
            /// </summary>
            GetDeviceRequest = 0x28,
            /// <summary>
            /// 读取或设置阻抗测试数据
            /// </summary>
            GetImpedanceDataRequest = 0x29,
            /// <summary>
            /// 读取电池剩余容量
            /// </summary>
            GetBatteryCapacityRequest = 0x2A,
            /// <summary>
            /// 设置蓝牙名称
            /// </summary>
            SetBlueToothName = 0x2B,
            /// <summary>
            /// 气压校准
            /// </summary>
            SetPressureCalibration = 0x2C,
            /// <summary>
            /// 设置生物定标状态标识
            /// </summary>
            SetCalibrationFlag = 0x2D,
            /// <summary>
            /// 读设备的日志
            /// </summary>
            GetDeviceHSCode = 0x2E,
            /// <summary>
            /// 读或写设备的SN号
            /// </summary>
            SetDeviceSNCode = 0x2F,
            /// <summary>
            /// 定时开停采集
            /// </summary>
            SetAutoRunTime = 0x30,
            /// <summary>
            /// 设备类型
            /// </summary>
            SetDeviceTypeRequest = 0x31
        }
        /// <summary>
        /// 错误代码
        /// </summary>
        public enum eumErrCode : int
        {
            /// <summary>
            /// 成功
            /// </summary>
            OK = 0,
            /// <summary>
            /// 出现异常
            /// </summary>
            ERROR_EXCEPTION = 1,
            /// <summary>
            /// 端口被占用
            /// </summary>
            ERROR_OPEN = 2,
            /// <summary>
            /// 端口已关闭
            /// </summary>
            ERROR_CLOSE = 3,
            /// <summary>
            /// 字节读取超时
            /// </summary>
            ERROR_TIMEOUT_BYTE = 4,
            /// <summary>
            /// 帧超时
            /// </summary>
            ERROR_TIMEOUT_FRAME = 5,
            ERROR_BCC = 6,
            /// <summary>
            /// 无效值
            /// </summary>
            ERROR_NAK = 7,
            /// <summary>
            /// 无应答
            /// </summary>
            ERROR_NO_ACK = 8,
            /// <summary>
            /// 握手失败
            /// </summary>
            ERROR_IDENTIFY = 9,
            /// <summary>
            /// 帧格式错误
            /// </summary>
            ERROR_FRAME_FORMAT = 10,//帧格式错
            /// <summary>
            /// 中断
            /// </summary>
            ERROR_Interrupt = 11,
            /// <summary>
            /// 不支持
            /// </summary>
            ERROR_NOTSUPPORT = 12,
            /// <summary>
            /// 设备执行失败
            /// </summary>
            ERROR_Faild = 255
        }
        /// <summary>
        /// 帧工厂
        /// </summary>
        public class FrameFactory
        {
            public FrameFactory()
            {
                StartByte = System.Text.Encoding.UTF8.GetBytes("FS");
                Endbyte = System.Text.Encoding.UTF8.GetBytes("PS");
            }
            /// <summary>
            /// 开始字符
            /// </summary>
            public byte[] StartByte { get; set; }
            /// <summary>
            /// 帧长度
            /// </summary>
            public int FrameLength { get; set; }
            /// <summary>
            /// 可变限定字
            /// </summary>
            public byte AccessSelections { set; get; }
            /// <summary>
            /// 设备类型
            /// </summary>
            public char DeviceTyp { set; get; }
            /// <summary>
            /// 地址域
            /// </summary>
            public string address { get; set; }
            /// <summary>
            /// 控制域
            /// </summary>
            public byte Fun { set; get; }
            /// <summary>
            /// 数据域
            /// </summary>
            public byte[] UserData { set; get; }
            /// <summary>
            /// 校验码H
            /// </summary>
            public short Crc_Data { set; get; }
            /// <summary>
            /// 结束字符
            /// </summary>
            public byte[] Endbyte { set; get; }
        }
        /// <summary>
        /// 数据接收委托
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="data"></param>
        public delegate void RecOneDataDelegate(float data, bool Invaild, string msg);
        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event RecOneDataDelegate RecOneDataEventHandle;

        /// <summary>
        /// 数据接收委托
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="data"></param>
        public delegate void RecOneEdfDataDelegate(float data, bool Invaild);
        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event RecOneEdfDataDelegate RecOneEdfDataEventHandle;
        /// <summary>
        ///  转换系数
        /// </summary>
        private double Rate { set; get; }
        /// <summary>
        /// 值
        /// </summary>
        public float Value
        {
            set
            {
                try
                {
                    if (!m_fnstop)
                    {
                        float data = value;
                        bool invaild = false;
                        string msg = "有效值";
                        if (!m_ShouldConverted)
                        {
                            if (value == MaxADValue + 1)
                            {
                                invaild = true;
                                msg = "模块故障";
                                value = 0;
                            }
                            else if (value == MaxADValue + 2)
                            {
                                invaild = true;
                                msg = "手指脱落";
                                value = 0;
                            }
                            if (RecOneEdfDataEventHandle != null)
                            {
                                RecOneEdfDataEventHandle.Invoke(data, invaild);
                            }
                        }
                        else
                        {
                            if (RecOneEdfDataEventHandle != null)
                            {
                                RecOneEdfDataEventHandle.Invoke(data, invaild);
                            }
                            // value = ((MaxViewValue - MinViewValue) * (value - MinADValue)) / (MaxADValue - MinADValue);
                            if (ID == 9 || ID ==16)
                            { 
                            value = ((MaxViewValue - MinViewValue) * (value - MinADValue)) / (MaxADValue - MinADValue);
                            }
                            else if (ID == 11 || ID == 12)
                            { value = 0.089407f * ((MaxViewValue - MinViewValue) * (value - MinADValue)) / (MaxADValue - MinADValue); }
                            else
                            { value = (value * 0.089407f); }
                        }
                        if (IsShowCurve && RecOneDataEventHandle != null)
                        {
                            RecOneDataEventHandle.Invoke(value, invaild, msg);
                        }
                    }
                }
                catch (Exception ee) { System.Console.WriteLine("数据添加：异常"); }
            }
        }
        private bool m_ShouldConverted = false;
        /// <summary>
        /// 是否需要进行值转换
        /// </summary>
        public bool ShouldConverted
        {
            set
            {
                m_ShouldConverted = value;
            }
        }
        /// <summary>
        /// AD最高限值
        /// </summary>
        public int MaxADValue = 0;
        /// <summary>
        /// AD最低限值
        /// </summary>
        public int MinADValue = 0;
        /// <summary>
        /// 与AD对应的最高限值
        /// </summary>
        public int MaxViewValue = 0;
        /// <summary>
        /// 与AD对应的最低限值
        /// </summary>
        public int MinViewValue = 0;
        private int m_DataLength = 0;
        /// <summary>
        /// 一包数据的长度
        /// </summary>
        public int DataLength
        {
            set
            {
                m_DataLength = value;
                DataBytesLength = value * ByteCountInData;
            }
            get
            {
                return m_DataLength;
            }
        }
        /// <summary>
        /// 一包数据的字节数量
        /// </summary>
        public int DataBytesLength
        {
            private set;
            get;
        }
        private int m_ByteCountInData = 1;
        /// <summary>
        /// 一个数据由多少字节组成
        /// </summary>
        public int ByteCountInData
        {
            set
            {
                m_ByteCountInData = value;
                DataBytesLength = value * m_DataLength;
            }
            get
            {
                return m_ByteCountInData;
            }
        }
        /// <summary>
        /// 组ID
        /// </summary>
        public int GroupID = 0;
        /// <summary>
        /// 组内序号
        /// </summary>
        public int GroupIndex = 0;
        /// <summary>
        /// 通道编码
        /// </summary>
        public int ID { set; get; }
        /// <summary>
        /// 无符号值标志
        /// </summary>
        public bool unSignData { set; get; }
        /// <summary>
        /// 值的名称
        /// </summary>
        public string Name
        {
            get { return m_Name; }
        }
        private string m_Name = "";
        private bool m_filiterEnable = false;
        private DataDefine()
        {

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Name"></param>
        public DataDefine(string Name)
        {
            m_Name = Name;
        }
        private object m_obj = new object();
        /// <summary>
        /// 是否显示到视图
        /// </summary>
        public bool IsShowCurve = true;
        /// <summary>
        /// 资源释放
        /// </summary>
        public void Dispose()
        {
            ///
        }
        private bool m_fnstop = false;
        /// <summary>
        /// 停止数据记录
        /// </summary>
        public bool fnStop
        {
            set
            {
                lock (m_obj)
                    m_fnstop = value;
                Clear();
            }
            get
            {
                return m_fnstop;
            }
        }
        /// <summary>
        /// 清空数据
        /// </summary>
        private void Clear()
        {
            ///
        }
    }
}
