using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace AwareTec.Polysmith.pChart
{
    /// <summary>
    /// X轴
    /// </summary>
    public class XAxis : Axis
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public XAxis()
            : base()
        {
            Name = "X Axis";
        }
        /// <summary>
        ///  绘制X坐标轴
        /// </summary>
        /// <param name="g"></param>
        /// <param name="MarginDistance">偏移量</param>
        public void DrawXAxis(Graphics g, int MarginDistance, int width, int height)
        {
            Axis axis = this;
            if (axis.AxisVisible)
            {
                Pen pen = new Pen(axis.BackColor, 1);
                Pen bak_pen = new Pen(Color.FromArgb(85, axis.ForeColor));
                g.DrawLine(pen, new PointF(MarginDistance, height - MarginDistance), new PointF(width - MarginDistance, height - MarginDistance));
                if (axis.CalibrationsVisible)
                {
                    int xseconds = (int)axis.MaxValue / 10000;
                    bool legend = (axis.LegendLables.Count == axis.Calibrations.Count);
                    List<float> Calibrations = axis.Calibrations.Select(t => ((t - MinValue) * axis.ValueRate) + MarginDistance).ToList();
                    for (int i = 1; i < axis.Calibrations.Count; i++)
                    {
                        bak_pen.DashStyle = DashStyle.Dash;
                        bak_pen.DashPattern = new float[] { 5, 5 };
                        float distance = Calibrations[i] - Calibrations[i - 1];
                        int subCount = 5;
                        if (xseconds >= 1 && xseconds < 10)
                        {
                            subCount = xseconds;
                        }
                        else if (xseconds == 0 || xseconds > 30)
                        {
                            subCount = 1;
                        }
                        float span = distance / subCount;

                        for (int j = 1; j < subCount; j++)
                        {
                            float x = Calibrations[i - 1] + span * j;
                            g.DrawLine(bak_pen, new PointF(x, height - MarginDistance), new PointF(x, MarginDistance));
                        }
                        bak_pen.DashStyle = DashStyle.Solid;
                        g.DrawLine(bak_pen, new PointF(Calibrations[i], height - MarginDistance), new PointF(Calibrations[i], MarginDistance));
                        if (legend)
                        {
                            int cnt = 0;
                            do
                            {
                                if (i == 1 && cnt == 0)
                                {
                                    i = 0;
                                    cnt++;
                                }
                                SizeF waterSize = g.MeasureString(LegendLables[i], Font);
                                StringFormat sf = new StringFormat(waterSize.Width > distance ? StringFormatFlags.DirectionVertical : StringFormatFlags.DisplayFormatControl) { Alignment = StringAlignment.Center };
                                float x = 0;
                                if (waterSize.Width > distance)
                                {
                                    sf = new StringFormat(StringFormatFlags.DirectionVertical) { Alignment = StringAlignment.Center };
                                    x = Calibrations[i] - waterSize.Height / 2;
                                }
                                else
                                {
                                    sf = new StringFormat(StringFormatFlags.DisplayFormatControl) { Alignment = StringAlignment.Center };
                                    x = Calibrations[i] - waterSize.Width / 2;
                                }
                                PointF p = new PointF(x, height - MarginDistance + 2);
                                g.DrawString(LegendLables[i], Font, Brushes.Blue, p, sf);
                                if (i == 0)
                                {
                                    i = 1;
                                }
                                cnt++;
                            } while (i == 1 && cnt == 2);
                        }
                    }
                }
                pen.Dispose();
                bak_pen.Dispose();
            }
        }
    }
}
