using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.DataCenter
{
    /// <summary>
    /// 数据文件基类定义
    /// </summary>
    public class BaseFile
    {
        /// <summary>
        /// edf的加载路径
        /// </summary>
        public string EdfPath { set; get; }
        /// <summary>
        /// 文件开头 8个字节
        /// </summary>
        public string StartFlag { set; get; }
        /// <summary>
        /// 患者编号 80个字节
        /// </summary>
        public string PatientNO { set; get; }
        /// <summary>
        /// 记录设备编号 80个字节
        /// </summary>
        public string DeviceID { set; get; }
        /// <summary>
        /// 记录开始日期 8个字节（日:月:年）
        /// </summary>
        public string RecordDate { set; get; }
        /// <summary>
        /// 记录开始时间 8个字节（ss:mm:hh）
        /// </summary>
        public string RecordTime { set; get; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { protected set; get; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { protected set; get; }
        /// <summary>
        /// 标题长度 8个字节 目前为(4864)
        /// </summary>
        public int CaptainLength { set; get; }
        private string m_Reserve = "";
        /// <summary>
        /// 保留 44个字节 
        /// </summary>
        public string Reserve
        {
            set
            {
                DeviceDataType = 0;
                string[] ss = value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (ss.Length > 1)
                {
                    DeviceSNCode = ss[1];
                    if (ss.Length > 2)
                        DeviceDataType = int.Parse(ss[2]);
                }
                m_Reserve = value;
            }
            get
            {
                return m_Reserve;
            }
        }
        /// <summary>
        /// 设备的数据类型 1-由PSG产生 2-由云平台客户端产生
        /// </summary>
        public int DeviceDataType { private set; get; }
        /// <summary>
        /// 数据记录次数 8个字节 
        /// </summary>
        public int RecordCnt { set; get; }
        /// <summary>
        /// 每次数据记录时间间隔min 8个字节 
        /// </summary>
        public int RecordTimeSpan { set; get; }
        /// <summary>
        /// 信号个数 4个字节 （目前是18个）
        /// </summary>
        public int DataSignCnt { set; get; }
        /// <summary>
        /// 通道
        /// </summary>
        public List<ChannelItem> Channels { set; get; }

        /// <summary>
        /// 设备的SN号
        /// </summary>
        public string DeviceSNCode = "";

        /// <summary>
        /// 是否为正确的edf文件
        /// </summary>
        public bool IsCorrect = false;
        /// <summary>
        /// edf的hash值
        /// </summary>
        public string HashCode
        {
            set;
            get;
        }

        /// <summary>
        /// 1秒时间所有通道的数据长度总和
        /// </summary>
        public int OneSecondDataCounts = 0;
    }
    /// <summary>
    /// 通道
    /// </summary>
    public class ChannelItem
    {
        /// <summary>
        /// 通道ID
        /// </summary>
        public int ID { set; get; }
        /// <summary>
        /// 偏移量
        /// </summary>
        public float OffSetValue = 0;
        /// <summary>
        /// 数据是否要二次处理
        /// </summary>
        public bool DataConvet = false;
        /// <summary>
        /// 信号名称 16个字节
        /// </summary>
        public string DataSignName { set; get; }
        /// <summary>
        /// 传感器类型 80个字节
        /// </summary>
        public string SensorType { set; get; }
        /// <summary>
        /// 单位符号 8个字节
        /// </summary>
        public string Unit { set; get; }
        /// <summary>
        /// 物理信号最小值 8个字节
        /// </summary>
        public float ViewMinValue { set; get; }
        /// <summary>
        /// 物理信号最大值 8个字节
        /// </summary>
        public float ViewMaxValue { set; get; }
        /// <summary>
        /// 数字信号最小值 8个字节
        /// </summary>
        public float ADMinValue { set; get; }
        /// <summary>
        /// 数字信号最大值 8个字节
        /// </summary>
        public float ADMaxValue { set; get; }
        /// <summary>
        /// 存储计算系数A
        /// </summary>
        public float ConstRateA { set; get; }
        /// <summary>
        /// 是否为无符号
        /// </summary>
        public bool UnSignData { set; get; }
        /// <summary>
        /// 前置滤波信息 80个字节
        /// </summary>
        public string FilterInfo { set; get; }
        private int m_dataLength = 0;
        /// <summary>
        /// 数据长度 8个字节
        /// </summary>
        public int DataLength
        {
            set
            {
                m_dataLength = value;
                OneFrameDataCount = 2 * value;
            }
            get
            {
                return m_dataLength;
            }
        }
        /// <summary>
        /// 保留 32个字节
        /// </summary>
        public string Reserve { set; get; }
        /// <summary>
        /// 数据
        /// </summary>
        public List<float> Data { set; get; }
        /// <summary>
        /// 字节数据源
        /// </summary>
        public byte[] DataBytes { set; get; }

        /// <summary>
        /// 字节数据源的总长度
        /// </summary>
        public int DataBytesLenght { set; get; }
        public int TmpIndex = 0;
        public int OneFrameDataCount
        {
            private set;
            get;
        }
        /// <summary>
        /// 通道序号
        /// </summary>
        public int ChannelIndex = 0;
        /// <summary>
        /// 放大倍数
        /// </summary>
        public int ZoomRate = 1;
        /// <summary>
        /// 返回通道数据记录总秒数
        /// </summary>
        public int RecordSeconds
        {
            get
            {
                if (Data != null && DataLength != 0)
                {
                    return Data.Count / DataLength;
                }
                return 0;
            }
        }

        /// <summary>
        /// 没有数据了
        /// </summary>
        public bool DataNull = true;
        private float[] x_eeg = new float[2];
        private float[] y_eeg = new float[2];
        /// <summary>
        /// 构造函数
        /// </summary>
        public ChannelItem()
        {
            Data = new List<float>();
            bytesData.Clear();
            m_currentIdx = 0;
            m_LastIdx = 0;
            m_killTask = true;
        }
        #region convertData
        private List<byte> bytesData = new List<byte>();
        private int m_currentIdx = 0;
        private int m_LastIdx = 0;
        /// <summary>
        /// 添加一个高字节，一个低字节
        /// </summary>
        /// <param name="dataL"></param>
        /// <param name="dataH"></param>
        public void AddBytes(byte dataL, byte dataH)
        {
            bytesData.Add(dataL);
            bytesData.Add(dataH);
            m_LastIdx++;
            if (m_killTask)
            {
                m_killTask = false;
                RunTask();
            }
        }
        private bool m_killTask = true;
        private void RunTask()
        {
            Task.Factory.StartNew(() =>
            {
                while (!m_killTask)
                {
                    System.Threading.Thread.Sleep(100);
                    int len = 0;
                    lock (m_obj)
                        len = bytesData.Count;
                    if (len % 2 != 0)
                        len = len - 1;
                    if (len > 1)
                    {
                        for (int i = 0; i < len;)
                        {
                            float value = BitConverter.ToInt16(new byte[] { bytesData[i], bytesData[i + 1] }, 0);
                            value = (float)(ViewMinValue + ConstRateA * (value - ADMinValue));
                            Data.Add(value);
                            i += 2;
                            m_currentIdx++;
                        }
                        lock (m_obj)
                        {
                            bytesData.RemoveRange(0, len);
                        }
                    }
                }
            });
        }

        /// <summary>
        /// 获取是否转换结束
        /// </summary>
        /// <returns></returns>
        public bool getIsEnd()
        {
            bool ret = m_currentIdx == m_LastIdx;
            if (ret)
            {
                m_killTask = true;
            }
            return ret;
        }
        #endregion
        private object m_obj = new object();
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="data"></param>
        public void AddData(float data, bool Invaild)
        {
            if (Invaild)
                data = 0;
            lock (m_obj)
            {
                Data.Add(data);
            }
        }
        /// <summary>
        /// 数据是否已经准备好
        /// </summary>
        public bool IsReady
        {
            get
            {
                int len = Data.Count;
                DataNull = len == 0;
                return (m_dataLength <= len);
            }
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public byte[] getData()
        {
            byte[] ret = new byte[m_dataLength * 2];
            int len = Data.Count;
            if (m_dataLength > len)
                return new byte[0];
            for (int i = 0; i < m_dataLength; i++)
            {
                float value = Data[i];
                if (DataConvet)///电生理数据需要经过去基线乘系数处理
                {
                    value = filterloop(Data[i]) * 0.089407f;
                }
                byte[] ss = UnSignData ? BitConverter.GetBytes((UInt16)value) : BitConverter.GetBytes((Int16)value);
                ret[2 * i] = ss[0];
                ret[2 * i + 1] = ss[1];
            }
            lock (m_obj)
            {
                Data.RemoveRange(0, m_dataLength);
            }
            return ret;
        }
        /// <summary>
        /// 基线滤波
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        private float filterloop(float val)
        {
            x_eeg[0] = x_eeg[1];
            y_eeg[0] = y_eeg[1];
            x_eeg[1] = val;
            y_eeg[1] = (float)((0.999248 *( x_eeg[1] - x_eeg[0])) + 0.998497 * y_eeg[0]);//0.1Hz
            return y_eeg[1];
        }
    }
}
