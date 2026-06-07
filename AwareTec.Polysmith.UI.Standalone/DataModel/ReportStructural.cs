using AwareTec.Polysmith.pChart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.UI.DataModel
{
    public class ReportStructural
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { set; get; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { set; get; }
        /// <summary>
        /// 显示范围最大值
        /// </summary>
        public float MaxValue { set; get; }
        /// <summary>
        /// 显示范围最小值
        /// </summary>
        public float MinValue { set; get; }
        public int Index { set; get; }
        /// <summary>
        /// 绘制点
        /// </summary>
        public List<SuperPointF> Pionts { set; get; }


    }
}
