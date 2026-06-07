using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.UI.DataModel
{
    /// <summary>
    /// 批量修改的信息集合
    /// </summary>
    public class BatchUnit
    {
        public bool MaxValueEnable = false;
        /// <summary>
        /// 最大值
        /// </summary>
        public string MaxValue { set; get; }
        public bool MinValueEnable = false;
        /// <summary>
        /// 最小值
        /// </summary>
        public string MinValue { set; get; }
        public bool PixelRateEnable = false;
        /// <summary>
        /// 灵敏度
        /// </summary>
        public string PixelRate { set; get; }
        /// <summary>
        /// 灵敏度选项
        /// </summary>
        public List<string> PixelRateItems { set; get; }
        public bool SingleNotchEnable = false;
        /// <summary>
        /// 陷波器
        /// </summary>
        public string SingleNotch { set; get; }
        /// <summary>
        /// 陷波器选项
        /// </summary>
        public List<string> SingleNotchItems { set; get; }
        public bool HighPassEnable = false;
        /// <summary>
        /// 高通滤波器
        /// </summary>
        public string HighPass { set; get; }
        /// <summary>
        /// 高通选项
        /// </summary>
        public List<string> HighPassItems { set; get; }
        public bool LowPassEnable = false;
        /// <summary>
        /// 低通滤波器
        /// </summary>
        public string LowPass { set; get; }
        /// <summary>
        /// 低通选项
        /// </summary>
        public List<string> LowPassItems { set; get; }
        public bool SampleSpanEnable = false;
        /// <summary>
        /// 采样频率
        /// </summary>
        public string SampleSpan { set; get; }
        public bool PenColorEnable = false;
        /// <summary>
        /// 画笔色彩
        /// </summary>
        public Color PenColor { set; get; }
    }
}
