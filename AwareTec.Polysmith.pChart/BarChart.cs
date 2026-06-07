using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Imaging;

namespace AwareTec.Polysmith.pChart
{
    /// <summary>
    /// 条形图
    /// </summary>
    public class BarChart
    {
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
        private int m_docksize = 40;
        private int m_topDock = 15;

        private ImageAttributes imageAtt = null;
        /// <summary>
        /// X轴坐标队列
        /// </summary>
        public XAxis xAxis = null;
        /// <summary>
        /// Y轴坐标队列
        /// </summary>
        public YAxis yAxis = null;

        public BarChart()
        {
            IsRange = false;
            xAxis = new XAxis() { BackColor = Color.DarkGray, ForeColor = Color.LimeGreen, AxisVisible = true, CalibrationsVisible = true, Displacement = m_Width - 2 * m_docksize, MaxValue = 100, Interval = 8 };
            yAxis = new YAxis() { BackColor = Color.DarkGray, ForeColor = Color.LimeGreen, AxisVisible = true, CalibrationsVisible = false, Displacement = m_Hight - 2 * m_docksize };
            //imageAtt = GetAlphaImgAttr(40);
        }

        /// <summary>
        /// 获取一个带有透明度的ImageAttributes
        /// </summary>
        /// <param name="opcity"></param>
        /// <returns></returns>
        public ImageAttributes GetAlphaImgAttr(int opcity)
        {
            if (opcity < 0 || opcity > 100)
            {
                throw new ArgumentOutOfRangeException("opcity 值为 0~100");
            }
            //颜色矩阵
            float[][] matrixItems =
     {
          new float[]{1,0,0,0,0},
          new float[]{0,1,0,0,0},
          new float[]{0,0,1,0,0},
          new float[]{0,0,0,(float)opcity / 100,0},
          new float[]{0,0,0,0,1}
     };
            ColorMatrix colorMatrix = new ColorMatrix(matrixItems);
            ImageAttributes imageAtt = new ImageAttributes();
            imageAtt.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            return imageAtt;
        }
        /// <summary>
        /// 以逆时针为方向对图像进行旋转
        /// </summary>
        /// <param name="b">位图流</param>
        /// <param name="angle">旋转角度[0,360](前台给的)</param>
        /// <returns></returns>
        private Image RotateImg(Image b, float angle)
        {
            angle = angle % 360;
            //弧度转换
            double radian = angle * Math.PI / 180.0;
            double cos = Math.Cos(radian);
            double sin = Math.Sin(radian);

            //原图的宽和高
            int w = b.Width;
            int h = b.Height;
            int W = (int)(Math.Max(Math.Abs(w * cos - h * sin), Math.Abs(w * cos + h * sin)));
            int H = (int)(Math.Max(Math.Abs(w * sin - h * cos), Math.Abs(w * sin + h * cos)));

            //目标位图
            Bitmap dsImage = new Bitmap(W, H);
            dsImage.MakeTransparent();
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dsImage);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //计算偏移量
            Point Offset = new Point((W - w) / 2, (H - h) / 2);

            //构造图像显示区域：让图像的中心与窗口的中心点一致
            Rectangle rect = new Rectangle(Offset.X, Offset.Y, w, h);
            Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);

            g.TranslateTransform(center.X, center.Y);
            g.RotateTransform(360 - angle);

            //恢复图像在水平和垂直方向的平移
            g.TranslateTransform(-center.X, -center.Y);
            g.DrawImage(b, rect);

            //重至绘图的所有变换
            g.ResetTransform();
            g.Save();

            g.Dispose();
            //保存旋转后的图片
            b.Dispose();
            return dsImage;
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
                SolidBrush[] solid = new SolidBrush[yAxis.CalibrationsColors.Count];
                for (int i = 0; i < solid.Length; i++)
                {
                    solid[i] = new SolidBrush(yAxis.CalibrationsColors[i]);
                }
                bool userCalibrationsColor = solid.Length > 0;
                int xoffset = 0;
                if (yAxis.Name != "")
                    xoffset = xAxis.Font.Height;
                if (yAxis.CalibrationsColorsName.Count > 0)
                {
                    m_topDock = yAxis.Font.Height + 10;
                }
                ///曲线展示区域
                Rectangle chartClientRectangle = new Rectangle(m_docksize + xoffset, m_topDock, w - m_docksize - xoffset - 5, h - m_docksize - m_topDock);
                xAxis.Displacement = chartClientRectangle.Width;
                yAxis.Displacement = chartClientRectangle.Height;
                using (Pen p = new Pen(Brushes.Black, 1))
                {
                    g.DrawRectangle(p, chartClientRectangle);
                    p.DashStyle = DashStyle.Dash;
                    p.DashPattern = new float[] { 3, 3 };
                    if (xAxis.LegendLables.Count > 0)
                    {
                        int lent = xAxis.LegendLables.Count;
                        for (int i = 0; i < lent; i++)
                        {
                            float dw = g.MeasureString(xAxis.LegendLables[i], xAxis.Font).Width;
                            float x = chartClientRectangle.Left + xAxis.Calibrations[i] * xAxis.ValueRate - 5;
                            if (xAxis.CalibrationsVisible && i > 0 && i < lent - 1)
                            {
                                g.DrawLine(p, new Point((int)x, chartClientRectangle.Top), new Point((int)x, chartClientRectangle.Bottom));
                            }
                            if (i < lent - 1)
                                g.DrawString(xAxis.LegendLables[i], xAxis.Font, Brushes.Black, new Point((int)(x - dw / 2), chartClientRectangle.Bottom + 1));
                        }
                        float wordlen = g.MeasureString(xAxis.Name, Font).Width;
                        g.DrawString(xAxis.Name, Font, Brushes.Black, new Point((int)(chartClientRectangle.Left + chartClientRectangle.Width / 2 - wordlen / 2), h - Font.Height));
                    }
                    if (yAxis.LegendLables.Count > 0)
                    {
                        int lent = yAxis.LegendLables.Count;
                        for (int i = 0; i < lent; i++)
                        {
                            float dw = g.MeasureString(yAxis.LegendLables[i], yAxis.Font).Width;
                            float x = chartClientRectangle.Left - dw - 2;
                            float y = chartClientRectangle.Bottom - yAxis.Calibrations[i] * yAxis.ValueRate;
                            if (yAxis.CalibrationsVisible && i > 0 && i < lent - 1)
                            {
                                g.DrawLine(p, new Point(chartClientRectangle.Left, (int)y), new Point(chartClientRectangle.Right, (int)y));
                            }
                            g.DrawString(yAxis.LegendLables[i], yAxis.Font, Brushes.Black, new Point((int)x, (int)y-yAxis.Font.Height/2));
                        }
                        float wordlen = g.MeasureString(yAxis.Name, Font).Width;
                        Bitmap ret = new Bitmap(h, Font.Height);
                        ret.MakeTransparent();
                        using (Graphics gg = Graphics.FromImage(ret))
                        {
                            gg.DrawString(yAxis.Name, Font, Brushes.Black, new Point((int)(ret.Width/2- wordlen/2), 0));
                        }
                        Image scr = RotateImg(ret, 90);
                        g.DrawImage(scr, new Rectangle(0, 0, Font.Height ,h), 0, 0, scr.Width, scr.Height, GraphicsUnit.Pixel);
                    }
                }
                if (IsRange)
                {
                    using (Pen p = new Pen(Brushes.Black, 2))
                    {
                        for (int i = 0; i < Points.Count;)
                        {
                            SuperPointF sp1 = Points[i];
                            SuperPointF sp2 = Points[i + 1];
                            SolidBrush brush = new SolidBrush(m_forecolor);
                            if (userCalibrationsColor)
                            {
                                brush = solid[(int)sp1.YMinValue];
                            }
                            int y = (int)(sp1.YMaxValue * yAxis.ValueRate);
                            int x1 = (int)(sp1.XValue * xAxis.ValueRate);
                            int x2 = (int)(sp2.XValue * xAxis.ValueRate);
                            if (y > chartClientRectangle.Height)
                            {
                                y = chartClientRectangle.Height;
                            }
                            Rectangle rect = new Rectangle(chartClientRectangle.Left + x1+1, chartClientRectangle.Bottom - y, x2 - x1, y);
                            if (rect.Right >= chartClientRectangle.Right)
                            {
                                rect.Width = chartClientRectangle.Right - 1 - rect.Left;
                            }
                            g.FillRectangle(brush, rect);
                            g.DrawRectangle(p, rect);
                            string strword = Math.Round(sp1.YMaxValue, 2).ToString();
                            float dwLen = g.MeasureString(strword, xAxis.Font).Width;
                            g.DrawString(strword, xAxis.Font, Brushes.Black, new Point((int)(rect.Left + rect.Width / 2 - dwLen / 2), rect.Top - xAxis.Font.Height));
                            i += 2;
                        }
                    }
                    int cnCnt = yAxis.CalibrationsColorsName.Count;
                    if (cnCnt > 0)
                    {
                        int offs = 5;
                        for (int i = yAxis.CalibrationsColors.Count - 1; i >= 0; i--)
                        {
                            if (cnCnt > i)
                            {
                                int wordLen = (int)g.MeasureString(yAxis.CalibrationsColorsName[i], yAxis.Font).Width;
                                offs += wordLen;
                                g.DrawString(yAxis.CalibrationsColorsName[i], yAxis.Font, Brushes.Black, new Point(m_Width - offs, 2));
                            }
                            offs += 8;
                            g.FillRectangle(new SolidBrush(yAxis.CalibrationsColors[i]), new Rectangle(m_Width - offs, 2, 5, yAxis.Font.Height));
                            offs += 5;
                        }
                    }
                }
                else
                {
                    if (Points.Count > 0)
                    {
                        int len = Points.Count + 2;
                        Point[] pointFs = new Point[len];
                        for (int i = 0; i < Points.Count; i++)
                        {
                            SuperPointF sp = Points[i];
                            pointFs[i + 1] = new Point(chartClientRectangle.Left + 1 + (int)(sp.XValue * xAxis.ValueRate), chartClientRectangle.Bottom - (int)(sp.YMaxValue * yAxis.ValueRate));

                        }
                        pointFs[0] = new Point(pointFs[1].X, chartClientRectangle.Bottom);
                        pointFs[len - 1] = new Point(pointFs[Points.Count].X, chartClientRectangle.Bottom);
                        g.FillPolygon(new SolidBrush(m_forecolor), pointFs);
                    }
                }
            }
            return srcImg;
        }
        /// <summary>
        /// 是否是范围
        /// </summary>
        public bool IsRange { set; get; }
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width
        {
            set
            {
                m_Width = value;
                xAxis.Displacement = value -  m_docksize;
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
                yAxis.Displacement = value -  m_docksize- m_topDock;
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
                xAxis.Displacement = m_Width - value;
                yAxis.Displacement = m_Hight - m_docksize - m_topDock;
            }
            get
            {
                return m_docksize;
            }
        }
        private Font m_Font = new Font("微软雅黑", 12.0f);
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
        /// 默认背景色为系统色
        /// </summary>
        private Color m_backcolor = System.Drawing.SystemColors.Window;
        /// <summary>
        /// 获取或设置背景色
        /// </summary>
        public Color BackColor { set { m_backcolor = value; } get { return m_backcolor; } }
        /// <summary>
        /// 默认背景色为系统色
        /// </summary>
        private Color m_forecolor = Color.LightPink;
        /// <summary>
        /// 获取或设置前色
        /// </summary>
        public Color ForeColor { set { m_forecolor = value; } get { return m_forecolor; } }
        /// <summary>
        /// 存放点位信息
        /// </summary>
        public List<SuperPointF> Points = new List<SuperPointF>();
    }
}
