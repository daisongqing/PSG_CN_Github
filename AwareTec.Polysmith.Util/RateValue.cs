using System;

namespace AwareTec.Polysmith.Util
{
    /// <summary>
    /// 滤波器类型
    /// </summary>
    public enum FilterType
    {
        /// <summary>
        /// 未定义
        /// </summary>
        None = 255,
        /// <summary>
        ///   陷波器
        /// </summary>
        SingleNotch = 0,
        /// <summary>
        /// 低通
        /// </summary>
        LowPass = 1,
        /// <summary>
        /// 高通
        /// </summary>
        HighPass = 2
    }

    /// <summary>
    /// 系数结构
    /// </summary>
    public class RateValue
    {
        /// <summary>
        /// 滤波器类型
        /// </summary>
        public FilterType FilterTyp = FilterType.None;
        /// <summary>
        /// 数据采样频率
        /// </summary>
        public int SampleSpan = 0;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 系数A
        /// </summary>
        public double[] RateA = new double[0];
        /// <summary>
        /// 系数B
        /// </summary>
        public double[] RateB = new double[0];
        /// <summary>
        /// 上一次存留的转换值数组
        /// </summary>
        public float[] bak_Y = new float[0];
        /// <summary>
        /// 上一次存留的原始值数组
        /// </summary>
        public float[] bak_X = new float[0];
        /// <summary>
        /// 起始索引
        /// </summary>
        public int StartIdx = 0;
        private bool m_checked = false;
        /// <summary>
        /// 是否被采用
        /// </summary>
        public bool Checked
        {
            set
            {
                m_checked = value;
                if (!value)
                {
                    bak_X = new float[0];
                    bak_Y = new float[0];
                }
            }
            get
            {
                return m_checked;
            }
        }
        /// <summary>
        /// 克隆
        /// </summary>
        public RateValue Clone()
        {
            RateValue rv = new RateValue();
            rv.RateA = Copy(this.RateA);
            rv.RateB = Copy(this.RateB);
            rv.Checked = this.Checked;
            rv.SampleSpan = this.SampleSpan;
            rv.Name = this.Name;
            rv.FilterTyp = this.FilterTyp;
            return rv;
        }
        /// <summary>
        /// 深复制
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private double[] Copy(double[] source)
        {
            double[] ret = new double[source.Length];
            Array.Copy(source, 0, ret, 0, ret.Length);
            return ret;
        }
    }
}
