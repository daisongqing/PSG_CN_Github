using RestfulWebRequest.EnumModels.EnumModels4Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.Predefined
{
    /// <summary>
    /// 预定义事件表
    /// </summary>
    [XmlRoot(ElementName = "PredefinedEvent")]
    public class PredefinedEvent
    {
        /// <summary>
        /// 预定义事件id
        /// </summary>
        [XmlElement(ElementName = "Id")]
        public int Id { set; get; }
        /// <summary>
        /// 事件名称
        /// </summary>
        [XmlElement(ElementName = "Name")]
        public string Name { set; get; }
        /// <summary>
        /// 是否区域标签
        /// </summary>
        [XmlElement(ElementName = "IsAreaLabel")]
        public bool isAreaLabel { set; get; }
        /// <summary>
        /// 事件类型
        /// </summary>
        [XmlElement(ElementName = "EventType")]
        public EventType eventType { set; get; }
        /// <summary>
        /// 可选通道
        /// </summary>
        [XmlElement(ElementName = "AllowChannel", IsNullable = true)]
        public string AllowChannel { set; get; }
        /// <summary>
        /// 标记是否只读
        /// </summary>
        [XmlElement(ElementName = "IsReadOnly")]
        public bool isReadOnly { get; set; }
    }
}
