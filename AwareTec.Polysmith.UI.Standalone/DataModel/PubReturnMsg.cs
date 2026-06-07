using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AwareTec.Polysmith.UI.DataModel
{
    [XmlRoot("Response")]
    public class PubReturnMsg
    {
        /// <summary>
        /// 鼻压力数据
        /// </summary>
        public List<float> PressureMuzzleFlowData { get; set; }

        /// <summary>
        /// 热敏数据
        /// </summary>
        public List<float> ThermalMuzzleFlowData { get; set; }

        public string ServiceName { get; set; }

        public string rtnCode { get; set; }

        public string rtnMsg { get; set; }


        //返回参数
        public string result { get; set; }
    }
}
