using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.Default
{
    [XmlRoot(ElementName = "DefaultEvents")]
    public class DefaultEvents
    {
        [XmlElement(Type = typeof(DefaultEvent))]
        public ArrayList DefaultEvent;
    }
}
