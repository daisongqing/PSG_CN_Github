using System;
using System.Collections.Generic;
using System.Drawing;

namespace AwareTec.Polysmith.pChart
{
    /// <summary>
    /// 轴
    /// </summary>
    public class Axis
    {
        private float m_Displacement = 400;
        /// <summary>
        /// 实际位移值
        /// </summary>
        public float Displacement
        {
            set
            {
                if (m_Displacement != value)
                {
                    m_Displacement = value;
                    m_ValueRate = (float)(m_Displacement * 1.00) / (m_MaxValue - m_MinValue);
                }
            }
            get
            {
                return m_Displacement;
            }
        }
        /// <summary>
        /// 位移偏移量
        /// </summary>
        public float offSetDistance = 0;
        /// <summary>
        /// 值转换系数
        /// </summary>
        private float m_ValueRate = 0.01f;
        /// <summary>
        /// 值转换系数
        /// </summary>
        public float ValueRate
        {
            get
            {
                return m_ValueRate;
            }
        }
        public Axis()
        {
            MaxValue = 10;
        }
        /// <summary>
        /// 刻度
        /// </summary>
        public List<float> Calibrations = new List<float>();
        /// <summary>
        /// 与刻度对应的颜色
        /// </summary>
        public List<Color> CalibrationsColors = new List<Color>();
        /// <summary>
        /// 与刻度对应的颜色的描述
        /// </summary>
        public List<string> CalibrationsColorsName = new List<string>();
        /// <summary>
        /// 与刻度值对应的描述
        /// </summary>
        public List<string> LegendLables = new List<string>();
        /// <summary>
        /// 轴名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 单位 
        /// </summary>
        public string Unit = "";
        /// <summary>
        /// 单位是否显示
        /// </summary>
        public bool UnitIsVisianble = false;
        /// <summary>
        /// 放大系数
        /// </summary>
        private float m_ZoomRate = 1.0f;
        /// <summary>
        /// 设置放大系数
        /// </summary>
        public float ZoomRate
        {
            set
            {
                m_ZoomRate = value;
                m_MinValue = m_MinValue * value;
                m_MaxValue = m_MaxValue * value;
            }
            get
            {
                return m_ZoomRate;
            }
        }

        private Font m_Font = new Font("宋体", 9.0f);
        /// <summary>
        /// 字体样式
        /// </summary>
        public Font Font
        {
            set
            {
                m_Font = value;
            }
            get
            {
                return m_Font;
            }
        }

        /// <summary>
        /// 默认背景色为黑
        /// </summary>
        private Color m_BackColor = Color.Black;
        /// <summary>
        /// 获取或设置背景色
        /// </summary>
        public Color BackColor { set { m_BackColor = value; } get { return m_BackColor; } }
        /// <summary>
        /// 默认前景色为黑
        /// </summary>
        private Color m_ForeColor = Color.LawnGreen;
        /// <summary>
        /// 获取或设置前景色
        /// </summary>
        public Color ForeColor { set { m_ForeColor = value; } get { return m_ForeColor; } }
        /// <summary>
        /// 是否需要绘制刻度
        /// </summary>
        public bool CalibrationsVisible = false;
        /// <summary>
        /// 是否需要绘制轴线
        /// </summary>
        public bool AxisVisible = false;
        private int m_Interval = 10;
        /// <summary>
        /// 刻度个数
        /// </summary>
        public int Interval
        {
            set
            {
                if (m_Interval == value)
                    return;
                m_Interval = value;
                Calibrations.Clear();
                float span = (float)Math.Round((m_MaxValue - m_MinValue) / m_Interval, 0);
                for (int i = 0; i < m_Interval; i++)
                {
                    Calibrations.Add(m_MinValue + span * i);
                }
                Calibrations.Add(m_MaxValue);
            }
            get
            {
                return m_Interval;
            }
        }
        private float m_MaxValue = 1;
        /// <summary>
        /// 最大值
        /// </summary>
        public float MaxValue
        {
            set
            {
                if (m_MaxValue == value)
                    return;
                if (value - m_MinValue < 1)
                    value = m_MinValue + 1;
                m_MaxValue = value;
                Calibrations.Clear();
                m_ValueRate = (float)Displacement / (m_MaxValue - m_MinValue);
                float span = (float)Math.Round((m_MaxValue - m_MinValue) / m_Interval, 0);
                for (int i = 0; i < m_Interval; i++)
                {
                    Calibrations.Add(m_MinValue + span * i);
                }
                Calibrations.Add(m_MaxValue);
            }
            get
            {
                return m_MaxValue;
            }
        }

        private float m_MinValue = 0;
        /// <summary>
        /// 最小值
        /// </summary>
        public float MinValue
        {
            set
            {
                if (m_MinValue == value)
                    return;
                if (m_MaxValue - value < 1)
                    value = m_MaxValue - 1;
                m_MinValue = value;
                Calibrations.Clear();
                m_ValueRate = (float)Displacement / (m_MaxValue - m_MinValue);
                float span = (float)Math.Round((m_MaxValue - m_MinValue) / m_Interval, 0);
                for (int i = 0; i < m_Interval; i++)
                {
                    Calibrations.Add(m_MinValue + span * i);
                }
                Calibrations.Add(m_MaxValue);
            }
            get
            {
                return m_MinValue;
            }
        }
        /// <summary>
        /// 设置最大最小值
        /// </summary>
        /// <param name="maxvalue"></param>
        /// <param name="minvalue"></param>
        public void SetMaxMinValue(float maxvalue, float minvalue)
        {
            if (maxvalue < minvalue)
                return;
            m_MinValue = minvalue;
            m_MaxValue = maxvalue;
            Calibrations.Clear();
            m_ValueRate = (float)Displacement / (m_MaxValue - m_MinValue);
            float span = (float)Math.Round((m_MaxValue - m_MinValue) / m_Interval, 0);
            for (int i = 0; i < m_Interval; i++)
            {
                Calibrations.Add(m_MinValue + span * i);
            }
            Calibrations.Add(m_MaxValue);
        }
    }
}
