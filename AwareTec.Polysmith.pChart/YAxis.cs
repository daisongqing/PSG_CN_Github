using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace AwareTec.Polysmith.pChart
{
    /// <summary>
    /// Y轴
    /// </summary>
    public class YAxis : Axis
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public YAxis()
            : base()
        {
            Name = "Y Axis";
        }
        /// <summary>
        ///  绘制Y坐标轴
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="MarginDistance">偏移量</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        public void DrawYAxis(Graphics g, int MarginDistance, int width, int height)
        {
            Axis axis = this;
            if (axis.AxisVisible)
            {
                Pen pen = new Pen(axis.BackColor, 1);
                Pen bak_pen = new Pen(Color.FromArgb(85, axis.ForeColor));
                g.DrawLine(pen, new PointF(MarginDistance, height - MarginDistance), new PointF(MarginDistance, MarginDistance));
                if (axis.CalibrationsVisible)
                {
                    List<float> Calibrations = axis.Calibrations.Select(t => ((t - MinValue) * axis.ValueRate) + MarginDistance).ToList();
                    for (int i = 1; i < axis.Calibrations.Count; i++)
                    {
                        bak_pen.DashStyle = DashStyle.Dash;
                        bak_pen.DashPattern = new float[] { 5, 5 };
                        float span = (Calibrations[i] - Calibrations[i - 1]) / 5;
                        for (int j = 1; j < 5; j++)
                        {
                            float y = Calibrations[i - 1] + span * j;
                            g.DrawLine(bak_pen, new PointF(MarginDistance, height - y), new PointF(width - MarginDistance, height - y));
                        }
                        bak_pen.DashStyle = DashStyle.Solid;
                        g.DrawLine(bak_pen, new PointF(MarginDistance, height - Calibrations[i]), new PointF(width - MarginDistance, height - Calibrations[i]));
                    }
                }
                pen.Dispose();
                bak_pen.Dispose();
            }
        }
        /// <summary>
        /// 绘制辅助刻度尺
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="MarginDistance">偏移量</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        public void drawyAxis(Graphics g, int MarginDistance, int width, int height)
        {

            if (this.CalibrationsVisible)
            {
                this.Interval = 1;
                Pen bak_pen = new Pen(Color.FromArgb(85, this.ForeColor), 0.5f);
                float Bottomoffset = MarginDistance + offSetDistance;
                ///绘制刻度线
                List<float> Calibrations = this.Calibrations.Select(t => ((t - MinValue) * this.ValueRate) + Bottomoffset).ToList();
                bak_pen.DashStyle = DashStyle.Solid;
                g.DrawLine(bak_pen, new PointF(MarginDistance, Calibrations[0]), new PointF(width - MarginDistance, Calibrations[0]));
                for (int s = 1; s < this.Calibrations.Count; s++)
                {
                    bak_pen.DashStyle = DashStyle.Dash;
                    bak_pen.DashPattern = new float[] { 3, 3 };
                    ///没个刻度，再分成5个子刻度，以虚线绘制
                    float span = (Calibrations[s] - Calibrations[s - 1]) / 5;
                    for (int j = 1; j < 5; j++)
                    {
                        float y = Calibrations[s - 1] + span * j;
                        g.DrawLine(bak_pen, new PointF(MarginDistance, y), new PointF(width - MarginDistance, y));
                    }
                    bak_pen.DashStyle = DashStyle.Solid;
                    g.DrawLine(bak_pen, new PointF(MarginDistance, Calibrations[s]), new PointF(width - MarginDistance, Calibrations[s]));
                }
                bak_pen.Dispose();
            }
        }
    }
}
