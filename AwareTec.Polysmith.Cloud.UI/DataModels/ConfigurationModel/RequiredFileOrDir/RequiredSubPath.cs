using AwareTec.Polysmith.Cloud.UI.DataModels.EnumModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.RequiredFileOrDir
{
    public class RequiredSubPath
    {
        [XmlElement(ElementName = "Path", DataType = "string")]
        public string Path { get; set; }
        [XmlElement(ElementName = "PathType")]
        public RequiredFileType PathType { get; set; }
    }
}
