using AwareTec.Polysmith.Util.PathUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.FullUser
{
    [XmlRoot(ElementName = "Path")]
    public class FullUserConfigurationModel
    {
        [XmlElement(ElementName = "RootPath", IsNullable =true)]
        public string RootPath { get; set; }
        [XmlElement(ElementName = "OrderPath", IsNullable = true)]
        public string OrderPath { get; set; }
        [XmlElement(ElementName = "ReportPath", IsNullable = true)]
        public string ReportPath { get; set; }

        [XmlElement(ElementName = "RemoteUrl", IsNullable = true)]
        public string RemoteUrl { get; set; }
    }
}
