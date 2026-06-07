using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.DataModels
{
    /// <summary>
    /// 预定义定标配置
    /// </summary>
    public class PreDefinedCalibration
    {
        /// <summary>
        /// 定标id
        /// </summary>
        public int ID { set; get; }
        /// <summary>
        /// 定标名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 定标描述
        /// </summary>
        public string Description { set; get; }
        /// <summary>
        /// 定标序号
        /// </summary>
        public int XH { set; get; }
        /// <summary>
        /// 定标适用通道
        /// </summary>
        public string ChannelID { set; get; }

    }
}
