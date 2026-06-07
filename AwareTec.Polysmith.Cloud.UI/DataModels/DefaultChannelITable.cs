using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.DataModels
{
    /// <summary>
    /// 与通道配置表匹配的基类
    /// </summary>
    public class DefaultChannelITable
    {
        /// <summary>
        /// 是否启用该通道
        /// </summary>
        public bool Enable { set; get; }
        /// <summary>
        /// 通道id
        /// </summary>
        public int ID { set; get; }
        /// <summary>
        /// edf标签名称
        /// </summary>
        public string SignName { set; get; }
        /// <summary>
        /// 传感器类型
        /// </summary>
        public string SensorType { set; get; }
        /// <summary>
        /// 页面最小值
        /// </summary>
        public float ViewMinValue { set; get; }
        /// <summary>
        /// 页面最大值
        /// </summary>
        public float ViewMaxValue { set; get; }
        /// <summary>
        /// 是否需要转换
        /// </summary>
        public bool NeedConvert { set; get; }
        /// <summary>
        /// d单位
        /// </summary>
        public string Unit { set; get; }
        /// <summary>
        /// AD最大值
        /// </summary>
        public float ADMaxValue { set; get; }
        /// <summary>
        /// AD最小值
        /// </summary>
        public float ADMinValue { set; get; }
        /// <summary>
        /// 采样率
        /// </summary>
        public int SpanTime { set; get; }
        /// <summary>
        /// 按照通讯协议不同类型的数据放在不同的组的id
        /// </summary>
        public int GroupID { set; get; }
        /// <summary>
        /// 该类型在组中的序号
        /// </summary>
        public int IndexInGroup { set; get; }
        /// <summary>
        /// 该类型在组中的长度
        /// </summary>
        public int LenghtInGroup { set; get; }
        /// <summary>
        /// 数据值的字节长度
        /// </summary>
        public int ByteLenghtOfValue { set; get; }
        /// <summary>
        /// 是否无符号
        /// </summary>
        public bool UnSignData { set; get; }
        /// <summary>
        /// 显示值标志
        /// </summary>
        public bool IsShowValue { set; get; }
        /// <summary>
        /// 刻度是否可见
        /// </summary>
        public bool CalibrationsVisible { set; get; }
        /// <summary>
        /// 是否开启灵敏度
        /// </summary>
        public bool PixelEnable { set; get; }
        /// <summary>
        /// 灵敏度全部范围
        /// </summary>
        public string strPixelRange { set; get; }
        /// <summary>
        /// 是否开启最大最小值
        /// </summary>
        public bool MaxMinValueEnable { set; get; }
    }
}
