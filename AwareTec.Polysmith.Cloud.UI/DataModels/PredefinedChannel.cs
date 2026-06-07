using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AwareTec.Polysmith.Cloud.UI.DataModels
{
    /// <summary>
    /// 预定义通道配置表
    /// </summary>
    public class PredefinedChannel
    {
        /// <summary>
        /// 通道id
        /// </summary>
        [XmlElement(ElementName = "Id")]
        public int ID { set; get; }
        /// <summary>
        /// edf标签名称
        /// </summary>
        [XmlElement(ElementName = "SignName")]
        public string SignName { set; get; }
        /// <summary>
        /// 传感器类型
        /// </summary>
        [XmlElement(ElementName = "SensorType")]
        public string SensorType { set; get; }
        /// <summary>
        /// 页面最小值
        /// </summary>
        [XmlElement(ElementName = "ViewMinValue")]
        public float ViewMinValue { set; get; }
        /// <summary>
        /// 页面最大值
        /// </summary>
        [XmlElement(ElementName = "ViewMaxValue")]
        public float ViewMaxValue { set; get; }
        /// <summary>
        /// AD最小值
        /// </summary>
        [XmlElement(ElementName = "ADMinValue")]
        public float ADMinValue { set; get; }
        /// <summary>
        /// AD最大值
        /// </summary>
        [XmlElement(ElementName = "ADMaxValue")]
        public float ADMaxValue { set; get; }
        /// <summary>
        /// 单位
        /// </summary>
        [XmlElement(ElementName = "Unit")]
        public string Unit { set; get; }
        /// <summary>
        /// 是否需要转换
        /// </summary>
        [XmlElement(ElementName = "NeedConvert")]
        public bool NeedConvert { set; get; }
        /// <summary>
        /// 是否启用该通道
        /// </summary>
        [XmlElement(ElementName = "Enable")]
        public bool Enable { set; get; }
        /// <summary>
        /// 采样率
        /// </summary>
        [XmlElement(ElementName = "SpanTime")]
        public int SpanTime { set; get; }
        /// <summary>
        /// 按照通讯协议不同类型的数据放在不同的组的id
        /// </summary>
        [XmlElement(ElementName = "GroupID")]
        public int GroupID { set; get; }
        /// <summary>
        /// 该类型在组中的序号
        /// </summary>
        [XmlElement(ElementName = "IndexInGroup")]
        public int IndexInGroup { set; get; }
        /// <summary>
        /// 该类型在组中的长度
        /// </summary>
        [XmlElement(ElementName = "LenghtInGroup")]
        public int LenghtInGroup { set; get; }
        /// <summary>
        /// 数据值的字节长度
        /// </summary>
        [XmlElement(ElementName = "ByteLenghtOfValue")]
        public int ByteLenghtOfValue { set; get; }
        /// <summary>
        /// 是否无符号
        /// </summary>
        [XmlElement(ElementName = "UnSignData")]
        public bool UnSignData { set; get; }
        /// <summary>
        /// 刻度是否可见
        /// </summary>
        [XmlElement(ElementName = "CalibrationsVisible")]
        public bool CalibrationsVisible { set; get; }
        /// <summary>
        /// 显示值标志
        /// </summary>
        [XmlElement(ElementName = "IsShowValue")]
        public bool IsShowValue { set; get; }
        /// <summary>
        /// 是否开启灵敏度
        /// </summary>
        [XmlElement(ElementName = "PixelEnable")]
        public bool PixelEnable { set; get; }
        /// <summary>
        /// 灵敏度全部范围
        /// </summary>
        [XmlElement(ElementName = "strPixelRange")]
        public string strPixelRange { set; get; }





        /// <summary>
        /// 是否开启最大最小值
        /// </summary>
        public bool MaxMinValueEnable { set; get; }
    }
}
