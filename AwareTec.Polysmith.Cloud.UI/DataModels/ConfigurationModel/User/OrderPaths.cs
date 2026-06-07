using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.User
{
    [XmlRoot(ElementName = "OrderPaths")]
    public class OrderPaths
    {
        [XmlElement(Type = typeof(OrderPath))]
        public ArrayList OrderPath;
    }
}
