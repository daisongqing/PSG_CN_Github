using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.EnumModels
{
    public enum ScanMethod
    {
        [XmlEnum(Name = "Empty"), Description("空")]
        Empty = -1,
        [XmlEnum(Name = "Local"), Description("本地")]
        Local,
        [XmlEnum(Name = "Remote"), Description("远程")]
        Remote
    }
}
