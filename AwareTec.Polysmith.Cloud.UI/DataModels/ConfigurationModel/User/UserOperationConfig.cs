using AwareTec.Polysmith.Cloud.UI.DataModels.EnumModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.User
{
    /// <summary>
    /// 用户操作配置
    /// </summary>
    [XmlRoot(ElementName = "UserOperationConfig")]
    public class UserOperationConfig
    {
        [XmlElement(ElementName = "TypeOfLastScan")]
        public ScanMethod TypeOfLastScan { get; set; }
        [XmlElement(ElementName = "LastBoundDevice", IsNullable = true)]
        public string LastBoundDevice { get; set; }
        [XmlElement(ElementName = "LastConfiguredAnalysisParameters")]
        public int? LastConfiguredAnalysisParameters { get; set; }
        [XmlElement(ElementName = "LastExportedReportFormat")]
        public string LastExportedReportFormat { get; set; }
        [XmlElement(ElementName = "LastExportedSleepReportName")]
        public string LastExportedSleepReportName { get; set; }
        [XmlElement(ElementName = "LastExportedBreathReportName")]
        public string LastExportedBreathReportName { get; set; }
        [XmlElement(ElementName = "LastSelectedScheme")]
        public string LastSelectedScheme { get; set; }
        [XmlElement(ElementName = "AutomaticScrollingInterval")]
        public int AutomaticScrollingInterval { get; set; }
        [XmlElement(ElementName = "AutoOpenReport")]
        public bool AutoOpenReport { get; set; }
    }
}
