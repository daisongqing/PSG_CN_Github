using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.Default
{
    [XmlRoot(ElementName = "DefaultEvent")]
    public class DefaultEvent
    {
        [XmlElement(ElementName = "Id")]
        public int Id { set; get; }
        [XmlElement(ElementName = "Description")]
        public string Description { set; get; }
        [XmlElement(ElementName = "MarkerColor")]
        public string MarkerColor { set; get; }
        [XmlElement(ElementName = "SelectedChannel")]
        public string SelectedChannel { set; get; }
        [XmlElement(ElementName = "HotKey", IsNullable = true)]
        public string HotKey { set; get; }
        [XmlElement(ElementName = "MinTimeDomain", IsNullable = true)]
        public string MinTimeDomain { set; get; }
    }
}
