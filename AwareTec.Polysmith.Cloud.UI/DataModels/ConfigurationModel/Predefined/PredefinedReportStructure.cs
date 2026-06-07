using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.Predefined
{
    /// <summary>
    /// 多趋势图
    /// </summary>
    [XmlRoot(ElementName = "ReportStructure")]
    public class PredefinedReportStructure
    {
        [XmlElement(ElementName = "Id")]
        public int Id { set; get; }
        [XmlElement(ElementName = "Name")]
        public string Name { set; get; }
        [XmlElement(ElementName = "Description")]
        public string Description { set; get; }
        [XmlElement(ElementName = "Interval")]
        public int Interval { set; get; }
        [XmlElement(ElementName = "MaxValue")]
        public float MaxValue { set; get; }
        [XmlElement(ElementName = "MinValue")]
        public float MinValue { set; get; }
        [XmlElement(ElementName = "Height")]
        public float Height { set; get; }
        [XmlElement(ElementName = "Width")]
        public float Width { set; get; }
        [XmlElement(ElementName = "PenColor")]
        public string PenColor { set; get; }
        [XmlElement(ElementName = "LegendLables", IsNullable = true)]
        public string LegendLables { set; get; }
        [XmlElement(ElementName = "CalibrationsColors", IsNullable = true)]
        public string CalibrationsColors { set; get; }
        [XmlElement(ElementName = "RangeCanChanged")]
        public bool RangeCanChanged { set; get; }
        [XmlElement(ElementName = "ShowTimeLables")]
        public bool ShowTimeLables { set; get; }
        [XmlElement(ElementName = "ChartStyle", IsNullable = true)]
        public int? ChartStyle { set; get; }
        [XmlElement(ElementName = "SavePath")]
        public string SavePath { set; get; }
        [XmlElement(ElementName = "DataSourceName")]
        public string DataSourceName { set; get; }
        [XmlElement(ElementName = "Visible")]
        public bool Visible { set; get; }
        [XmlElement(ElementName = "Index")]
        public int Index { set; get; }
    }
}
