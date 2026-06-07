using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.User
{
    [XmlRoot(ElementName = "OrderPath")]
    public class OrderPath
    {
        [XmlElement(ElementName = "OrderId")]
        public string OrderId { get; set; }
        [XmlElement(ElementName = "Path")]
        public string Path { get; set; }
        [XmlElement(ElementName = "Version")]
        public int Version { get; set; }
        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public OrderPath Clone()
        {
            OrderPath ret = new OrderPath();
            ret.OrderId = this.OrderId;
            ret.Version = this.Version;
            ret.Path = this.Path;
            return ret;
        }
    }
}
