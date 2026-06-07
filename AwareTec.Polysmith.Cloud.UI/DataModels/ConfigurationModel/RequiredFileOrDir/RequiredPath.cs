using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.RequiredFileOrDir
{
    [XmlRoot(ElementName = "RequiredPath")]
    public class RequiredPath
    {
        [XmlElement(Type = typeof(RequiredSubPath))]
        public ArrayList RequiredSubPath;
    }
}
