using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AwareTec.Polysmith.pChart
{
    /// <summary>
    /// 趋势叠加图谱
    /// </summary>
    public class ChartMap
    {
        /// <summary>
        /// 显示单元
        /// </summary>
        public List<Area> ItemAreas = new List<Area>();
        /// <summary>
        /// 宽度
        /// </summary>
        private int m_Width = 500;
        /// <summary>
        /// 高度
        /// </summary>
        private int m_Hight = 800;
        /// <summary>
        /// 边距大小
        /// </summary>
        private int m_docksize = 30;
        /// <summary>
        /// X轴坐标队列
        /// </summary>
        public XAxis xAxis = null;
        /// <summary>
        /// Y轴坐标队列
        /// </summary>
        public YAxis yAxis = null;
        /// <summary>
        /// 开始记录时间
        /// </summary>
        public DateTime StartTime { set; get; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public ChartMap()
        {
            xAxis = new XAxis() { BackColor = Color.DarkGray, ForeColor = Color.LimeGreen, AxisVisible = true, CalibrationsVisible = true, Displacement = m_Width - 2 * m_docksize, MaxValue = 960, Interval = 8 };
            yAxis = new YAxis() { BackColor = Color.DarkGray, ForeColor = Color.LimeGreen, AxisVisible = true, Displacement = m_Hight - 2 * m_docksize };
        }
        /// <summary>
        /// 获取位图信息（整个趋势图）
        /// </summary>
        /// <returns></returns>
        public Image GetMap()
        {
            Bitmap srcImg = new Bitmap(m_Width, m_Hight);
            using (Graphics g = Graphics.FromImage(srcImg))
            {
                int w = this.m_Width;
                int h = this.m_Hight;
                g.Clip = new Region(new Rectangle(0, 0, w, h));
                g.Clear(this.m_backcolor);
                g.SmoothingMode = SmoothingMode.None;
                ///绘制左右两y轴
                using (Pen pen = new Pen(yAxis.BackColor, 1))
                {
                    g.DrawLine(pen, new PointF(m_docksize, m_Hight - m_docksize), new PointF(m_docksize, m_docksize));
                    g.DrawLine(pen, new PointF(m_Width - m_docksize, m_Hight - m_docksize), new PointF(m_Width - m_docksize, m_docksize));
                    //for(int i = 0; i < ItemAreas.Count; i++)
                    //{
                    //    if (ItemAreas[i].Edit)
                    //    {

                    //    }
                    //}
                }
                List<string> times = new List<string>();
                List<PointF> loacations = new List<PointF>();
                ///绘制上下两x轴
                using (Pen pen = new Pen(xAxis.BackColor, 1))
                {
                    g.DrawLine(pen, new PointF(m_docksize, m_Hight - m_docksize), new PointF(m_Width - m_docksize, m_Hight - m_docksize));
                    g.DrawLine(pen, new PointF(m_docksize, m_docksize), new PointF(m_Width - m_docksize, m_docksize));
                    int lastIdx = xAxis.Calibrations.Count - 1;
                    float fontx = 0;
                    for (int i = 0; i < xAxis.Calibrations.Count; i++)
                    {
                        string word = StartTime.AddHours(i).ToString(i == 0 ? "HH:mm:ss" : "HH:mm");
                        float wordw = g.MeasureString(word, xAxis.Font).Width;
                        float calibrationX = xAxis.Calibrations[i] * xAxis.ValueRate + m_docksize;
                        if (i > 0)
                        {
                            if (i == lastIdx)
                            {
                                if (m_Width - m_docksize - wordw - 4 - fontx <= 0)
                                {
                                    break;
                                }
                                word = StartTime.AddSeconds(xAxis.Calibrations[i] * 30).ToString("HH:mm");
                            }
                            else
                            {
                                g.DrawLine(pen, new PointF(calibrationX, m_Hight - m_docksize), new PointF(calibrationX, m_Hight - m_docksize - 2));
                            }
                        }
                        PointF loc = new PointF(calibrationX - wordw / 2, m_Hight - m_docksize + 4);
                        g.DrawString(word, xAxis.Font, new SolidBrush(xAxis.BackColor), loc);
                        times.Add(word);
                        loacations.Add(loc);
                        fontx = calibrationX;
                    }
                }
                for (int s = 0; s < ItemAreas.Count; s++)
                {
                    if (ItemAreas[s].Checked)
                    {
                        ItemAreas[s].xAxis.MaxValue = xAxis.MaxValue;
                        ItemAreas[s].xAxis.MinValue = xAxis.MinValue;
                        ItemAreas[s].xAxis.Interval = xAxis.Interval;
                        ItemAreas[s].xAxis.Displacement = xAxis.Displacement;
                        ItemAreas[s].DrawYCalibrations(g,false);
                        ItemAreas[s].DrawCurve(g);
                        ItemAreas[s].SetTimeLabels(times.ToArray(), loacations.ToArray());
                    }
                }
            }
            return srcImg;
        }
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width
        {
            set
            {
                m_Width = value;
                xAxis.Displacement = value - 2 * m_docksize;
            }
            get
            {
                return m_Width;
            }
        }
        /// <summary>
        /// 高度
        /// </summary>
        public int Hight
        {
            set
            {
                m_Hight = value;
                yAxis.Displacement = value - 2 * m_docksize;
            }
            get
            {
                return m_Hight;
            }
        }
        /// <summary>
        /// 边距大小
        /// </summary>
        public int Docksize
        {
            set
            {
                m_docksize = value;
                xAxis.Displacement = m_Width - 2 * value;
                yAxis.Displacement = m_Hight - 2 * value;
            }
            get
            {
                return m_docksize;
            }
        }
        /// <summary>
        /// 默认背景色为系统色
        /// </summary>
        private Color m_backcolor = System.Drawing.SystemColors.Window;
        /// <summary>
        /// 获取或设置背景色
        /// </summary>
        public Color BackColor { set { m_backcolor = value; } get { return m_backcolor; } }
    }
}
