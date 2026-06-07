using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.Predefined.total
{
    [XmlRoot(ElementName = "PredefinedChannels")]
    public class PredefinedChannels
    {
        [XmlElement(Type = typeof(PredefinedChannel))]
        public ArrayList PredefinedChannel;
    }
}
