using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.DataModels
{
    /// <summary>
    /// 预定义事件表
    /// </summary>
    public class PreDefinedEvent
    {
        /// <summary>
        /// 预定义事件id
        /// </summary>
        public int ID { set; get; }
        /// <summary>
        /// 事件名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 是否区域标签
        /// </summary>
        public int Flag { set; get; }
        /// <summary>
        /// 事件类型
        /// </summary>
        public int MarkTyp { set; get; }
        /// <summary>
        /// 可选通道
        /// </summary>
        public string AllowChannel { set; get; }
    }
}
