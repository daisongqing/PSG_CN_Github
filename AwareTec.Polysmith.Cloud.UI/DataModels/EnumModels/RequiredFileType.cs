using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.EnumModels
{
    public enum RequiredFileType
    {
        [XmlEnum(Name = "Empty")]
        Empty = -1,
        [XmlEnum(Name = "AnalysisSource")]
        AnalysisSource = 1,
        [XmlEnum(Name = "Coeffs")]
        Coeffs = 2,
        [XmlEnum(Name = "DefaultFullUserConfig")]
        DefaultFullUserConfig = 3,
        [XmlEnum(Name = "DefaultChannel")]
        DefaultChannel = 4,
        [XmlEnum(Name = "DefaultUserConfig")]
        DefaultUserConfig = 5,
        [XmlEnum(Name = "DefaultEvent")]
        DefaultEvent = 6,
        [XmlEnum(Name = "DefaultOrderPath")]
        DefaultOrderPath = 7,
        [XmlEnum(Name = "Feature")]
        Feature = 8,
        [XmlEnum(Name = "Filter")]
        Filter = 9,
        [XmlEnum(Name = "PredefinedCalibration")]
        PredefinedCalibration = 10,
        [XmlEnum(Name = "PredefinedChannel")]
        PredefinedChannel = 11,
        [XmlEnum(Name = "PredefinedEvent")]
        PredefinedEvent = 12,
        [XmlEnum(Name = "PredefinedReportStructure")]
        PredefinedReportStructure = 13,
        [XmlEnum(Name = "Pvedio")]
        Pvedio = 14
    }
}
