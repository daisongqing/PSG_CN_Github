using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.Predefined
{
    /// <summary>
    /// 预定义定标配置
    /// </summary>
    [XmlRoot(ElementName = "PredefinedCalibration")]
    public class PredefinedCalibration
    {
        /// <summary>
        /// 定标id
        /// </summary>
        [XmlElement(ElementName = "Id")]
        public int Id { set; get; }
        /// <summary>
        /// 定标名称
        /// </summary>
        [XmlElement(ElementName = "Name")]
        public string Name { set; get; }
        /// <summary>
        /// 定标描述
        /// </summary>
        [XmlElement(ElementName = "Description")]
        public string Description { set; get; }
        /// <summary>
        /// 定标序号
        /// </summary>
        [XmlElement(ElementName = "SortId")]
        public int XH { set; get; }
        /// <summary>
        /// 定标适用通道
        /// </summary>
        [XmlElement(ElementName = "ApplicableChannels")]
        public string ChannelID { set; get; }

    }
}
