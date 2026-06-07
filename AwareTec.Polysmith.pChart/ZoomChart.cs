using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace AwareTec.Polysmith.pChart
{
    public partial class ZoomChart : UserControl
    {
        /// <summary>
        /// 超级坐标结构
        /// </summary>
        private class SupPointF
        {
            /// <summary>
            /// 实际坐标X
            /// </summary>
            public float X = 0;
            /// <summary>
            /// 实际坐标X
            /// </summary>
            public float Y = 0;
            /// <summary>
            /// 实际Y值
            /// </summary>
            public float YValue = 0;
            /// <summary>
            /// 对应的屏幕坐标
            /// </summary>
            public Point ScreenPoint = new Point(0, 0);
        }
        /// <summary>
        /// X轴坐标队列
        /// </summary>
        public XAxis xAxis = null;
        /// <summary>
        /// Y轴坐标队列
        /// </summary>
        public YAxis yAxis = null;
        /// <summary>
        /// 绘图区域高
        /// </summary>
        private int m_Height = 800;
        /// <summary>
        /// 绘图区域宽
        /// </summary>
        private int m_Width = 600;
        /// <summary>
        /// 边距间隔
        /// </summary>
        private int m_docksize = 53;
        /// <summary>
        /// 存放源数据的队列
        /// </summary>
        private List<float> m_DataSource = new List<float>(0);
        /// <summary>
        /// 每秒所占位移量
        /// </summary>
        private int m_SecondSpanDistance = 40;
        private bool m_valueShowFresh = true;
        /// <summary>
        /// 绘制好的原图
        /// </summary>
        private Image m_SourceImage = null;
        public ZoomChart()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            base.UpdateStyles();
            this.xAxis = new XAxis
            {
                BackColor = Color.Black,
                ForeColor = Color.LightGray,
                AxisVisible = true,
                CalibrationsVisible = true,
                Displacement = (float)this.m_Width,
                MaxValue = 10000f
            };
            this.yAxis = new YAxis
            {
                BackColor = Color.Black,
                ForeColor = Color.LightGray,
                AxisVisible = true,
                CalibrationsVisible = true,
                Displacement = (float)this.m_Height
            };
            base.Load += this.ZoomChart_Load;
            this.MouseWheel += ZoomChart_MouseWheel;
        }
        private bool m_startpointFresh = false;
        private void ZoomChart_MouseWheel(object sender, MouseEventArgs e)
        {
            if (MouseIsDown)
                return;
            int value = e.Delta < 0 ? hScrollBar1.Value + hScrollBar1.SmallChange : hScrollBar1.Value - hScrollBar1.SmallChange;
            int max = hScrollBar1.Maximum - 9;
            if (value > max)
                value = max;
            else if (value < hScrollBar1.Minimum)
                value = hScrollBar1.Minimum;
            if (value != hScrollBar1.Value)
            {
                hScrollBar1.Value = value;
                this.m_xoffSet = value;
                this.m_valueShowFresh = true;
                m_startpointFresh = true;
                this.panel1.Invalidate();
            }
        }
        #region 移动效果
        bool MouseIsDown = false;
        private void frmMain_MouseUp(object sender, MouseEventArgs e)
        {
            this.panel1.Capture = false;
            Cursor.Clip = Rectangle.Empty;
            this.MouseIsDown = false;
            this.m_EndSubPoint = null;
            this.m_StartSubPiont = null;
            this.panel1.Invalidate();
        }
        /// <summary>
        /// 提示文本是否已显示
        /// </summary>
        private bool m_tipIsShow = false;
        private SupPointF m_StartSubPiont = null, m_EndSubPoint = null;
        /// <summary>
        /// 
        /// </summary>
        private bool m_HasLines = false;
        int cnt = 0;
        private void frmMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.MouseIsDown)
            {
                this.panel1.Capture = true;
                if (this.ClientPoints != null)
                {
                    ZoomChart.SupPointF supPointF = this.ClientPoints.FindLast((ZoomChart.SupPointF t) => t.X <= (float)(e.X + 2));
                    if (supPointF != null)
                    {
                        this.m_EndSubPoint = new ZoomChart.SupPointF
                        {
                            X = supPointF.X,
                            Y = supPointF.Y,
                            YValue = supPointF.YValue
                        };
                        this.panel1.Invalidate();
                        return;
                    }
                }
            }
            else if (this.ClientPoints != null)
            {
                ZoomChart.SupPointF supPointF2 = this.ClientPoints.Find((ZoomChart.SupPointF t) => t.X >= (float)(e.X - 1) && t.X <= (float)(e.X + 1));
                if (supPointF2 != null)
                {
                    this.m_StartSubPiont = new ZoomChart.SupPointF
                    {
                        X = supPointF2.X,
                        Y = supPointF2.Y,
                        YValue = supPointF2.YValue
                    };
                }
                else
                {
                    this.m_StartSubPiont = null;
                }
                this.panel1.Invalidate();
            }
        }
        void frmMain_MouseDown(object sender, MouseEventArgs e)
        {
            this.MouseIsDown = true;
            if (this.ClientPoints != null)
            {
                ZoomChart.SupPointF supPointF = this.ClientPoints.Find((ZoomChart.SupPointF t) => t.X >= (float)(e.X - 2));
                if (supPointF != null && this.m_StartSubPiont == null)
                {
                    this.m_StartSubPiont = new ZoomChart.SupPointF
                    {
                        X = supPointF.X,
                        Y = supPointF.Y,
                        YValue = supPointF.YValue
                    };
                }
            }
        }
        #endregion
        /// <summary>
        /// 防止拖拽的时候出现卡顿
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = 0x02000000;////用双缓冲绘制窗口的所有子控件
                return cp;
            }
        }
        private float m_yMaxValue = 0;
        private void ZoomChart_Load(object sender, EventArgs e)
        {
            this.panel1.Paint += this.panel1_Paint;
            this.panel1.Resize += this.panel1_Resize;
            this.panel1.MouseDown += this.frmMain_MouseDown;
            this.panel1.MouseMove += this.frmMain_MouseMove;
            this.panel1.MouseUp += this.frmMain_MouseUp;
            if (this.panel1.Width != 0 && this.panel1.Height != 0)
            {
                this.m_Height = this.panel1.Height;
                this.m_Width = this.panel1.Width;
                this.m_SourceImage = this.getMap();
                this.m_xoffSet = ((this.hScrollBar1.Value == this.hScrollBar1.Maximum) ? (this.hScrollBar1.Value - 9) : this.hScrollBar1.Value);
            }
            this.hScrollBar1.Focus();
        }
        /// <summary>
        /// 当前显示区域的数据点
        /// </summary>
        private List<SupPointF> ClientPoints { set; get; }
        /// <summary>
        /// 所有坐标点
        /// </summary>
        private List<PointF> m_DataSourcePoints { set; get; }
        private void panel1_Resize(object sender, EventArgs e)
        {
            if (this.panel1.Width != 0 && this.panel1.Height != 0)
            {
                this.m_Height = this.panel1.Height;
                this.m_Width = this.panel1.Width;
                this.m_SourceImage = this.getMap();
                this.m_valueShowFresh = true;
                if (this.hScrollBar1.Visible)
                {
                    this.m_xoffSet = ((this.hScrollBar1.Value == this.hScrollBar1.Maximum) ? (this.hScrollBar1.Value - 9) : this.hScrollBar1.Value);
                    return;
                }
                this.m_xoffSet = 0;
            }
        }
        private int m_xoffSet = 0;
        private bool m_ShowPeekValue = false;
        private Region m_PeekValueRegion = null;
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            using (Graphics graphics = e.Graphics)
            {
                this.m_yMaxValue = (float)((double)this.m_Height * 25.4 / (double)graphics.DpiY);
                if (this.m_SourceImage != null)
                {
                    int num = this.m_xoffSet * this.m_SecondSpanDistance;
                    graphics.DrawImage(this.m_SourceImage, new RectangleF(0f, 0f, (float)this.m_Width, (float)this.m_Height), new RectangleF((float)num, 0f, (float)this.m_Width, (float)this.m_Height), GraphicsUnit.Pixel);
                    if (this.m_valueShowFresh)
                    {
                        this.ClientPoints = new List<ZoomChart.SupPointF>();
                        for (int i = 0; i < this.m_DataSourcePoints.Count; i++)
                        {
                            if (this.m_DataSourcePoints[i].X >= (float)num && this.m_DataSourcePoints[i].X <= (float)(num + this.m_Width))
                            {
                                SupPointF one = new ZoomChart.SupPointF
                                {
                                    X = this.m_DataSourcePoints[i].X - (float)num,
                                    Y = this.m_DataSourcePoints[i].Y,
                                    YValue = this.m_DataSource[i]
                                };
                                this.ClientPoints.Add(one);
                                if (m_startpointFresh && m_StartSubPiont != null)
                                {
                                    if (one.X >= (float)(m_StartSubPiont.X - 1) && one.X <= (float)(m_StartSubPiont.X + 1))
                                    {
                                        m_StartSubPiont = one;
                                        m_startpointFresh = false;
                                    }
                                }
                            }
                        }
                        this.m_valueShowFresh = false;
                    }
                }
                if (this.MouseIsDown)
                {
                    if (this.m_StartSubPiont == null)
                    {
                        goto IL_8E6;
                    }
                    graphics.FillEllipse(Brushes.Red, new RectangleF(this.m_StartSubPiont.X - 3f, this.m_StartSubPiont.Y - 3f, 6f, 6f));
                    if (m_EndSubPoint == null)
                    {
                        goto IL_8E6;
                    }
                    if (this.m_StartSubPiont.X == this.m_EndSubPoint.X)
                    {
                        goto IL_8E6;
                    }
                    graphics.FillEllipse(Brushes.Red, new RectangleF(this.m_EndSubPoint.X - 3f, this.m_EndSubPoint.Y - 3f, 6f, 6f));
                    using (Pen pen = new Pen(Color.Red, 1f))
                    {
                        pen.DashStyle = DashStyle.Dash;
                        pen.DashPattern = new float[]
                        {
                            3f,
                            2f
                        };
                        graphics.DrawLines(pen, new PointF[]
                        {
                            new PointF(this.m_StartSubPiont.X, this.m_StartSubPiont.Y),
                            new PointF(this.m_EndSubPoint.X, this.m_StartSubPiont.Y),
                            new PointF(this.m_EndSubPoint.X, this.m_EndSubPoint.Y)
                        });
                        using (Font font = new Font("宋体", 10f))
                        {
                            float num2 = this.m_EndSubPoint.X - this.m_StartSubPiont.X;
                            float num3 = Math.Abs(num2);
                            string text = string.Format("{0:f3} s", num3 / this.xAxis.ValueRate / 1000f);
                            float width = graphics.MeasureString(text, font).Width;
                            float num4 = Math.Abs(this.m_EndSubPoint.YValue - this.m_StartSubPiont.YValue);
                            float num5 = this.m_EndSubPoint.Y - this.m_StartSubPiont.Y;
                            float num6 = Math.Abs(num5);
                            string text2;
                            if (num4 > 1000000f)
                            {
                                text2 = string.Format("{0:f2}{1}", num4 / 1000000f, "V");
                            }
                            else if (num4 > 1000f)
                            {
                                text2 = string.Format("{0:f2}{1}", num4 / 1000f, "mV");
                            }
                            else
                            {
                                text2 = string.Format("{0:f2}{1}", num4, "μV");
                            }
                            float m_RealTimeFrequency = -1;
                            if (TimeSpan < 100)
                            {
                                int startIdx = ClientPoints.FindIndex(t => t.X == m_StartSubPiont.X);
                                int endIdx = ClientPoints.FindIndex(t => t.X == m_EndSubPoint.X);
                                bool flag = startIdx > endIdx;
                                int i = flag ? endIdx : startIdx;
                                int cnt = 0;
                                int len = (!flag ? endIdx : startIdx) + 1;
                                int totalCnt = len - i;
                                List<float> yvalues = new List<float>(totalCnt);
                                for (; i < len; i++)
                                {
                                    yvalues.Add(ClientPoints[i].YValue);
                                }
                                float avg = yvalues.Average();
                                float basevalue = (float)Math.Round(yvalues.Sum(x => Math.Pow(x - avg, 2)) / yvalues.Count, 2);
                                for (int s = 0; s < yvalues.Count; s++)
                                {
                                    if (s + 1 < yvalues.Count)
                                    {
                                        float y = (yvalues[s] - avg) / basevalue;
                                        float y1 = (yvalues[s + 1] - avg) / basevalue;
                                        ///找极致点并累计计数
                                        if (y * y1 < 0)
                                        {
                                            cnt++;
                                        }
                                    }
                                }
                                m_RealTimeFrequency = cnt * 500.0f / (totalCnt * TimeSpan);
                            }
                            text2 = string.Format("({0}{1:f2}%){2}{3}", (this.m_EndSubPoint.YValue - this.m_StartSubPiont.YValue > 0f) ? "+" : "-", num4 * 100f / Math.Abs(this.m_StartSubPiont.YValue), text2, m_RealTimeFrequency > -1 ? string.Format("\r\n{0:f2} Hz", m_RealTimeFrequency) : "");
                            float width2 = graphics.MeasureString(text2, font).Width;
                            if (num3 < width || num6 < (float)font.Height)
                            {
                                PointF point = new PointF(this.m_EndSubPoint.X + 2f, this.m_EndSubPoint.Y - (float)(font.Height / 2));
                                if (point.Y < 0f)
                                {
                                    point.Y = 0f;
                                }
                                else if (point.Y > (float)this.m_Height)
                                {
                                    point.Y = (float)(this.m_Height - font.Height);
                                }
                                string text3 = string.Format("({0},{1})", text, text2);
                                float width3 = graphics.MeasureString(text3, font).Width;
                                if (this.m_EndSubPoint.X < this.m_StartSubPiont.X)
                                {
                                    point.X = this.m_EndSubPoint.X - width3 - 2f;
                                    if (point.X < 0f)
                                    {
                                        point.X = 0f;
                                        point.Y = this.m_EndSubPoint.Y + 2f;
                                        if (point.Y + (float)font.Height > (float)this.m_Height)
                                        {
                                            point.Y = this.m_EndSubPoint.Y - (float)font.Height - 2f;
                                        }
                                    }
                                }
                                else if (point.X + width3 > (float)this.m_Width)
                                {
                                    point.X = (float)this.m_Width - width3;
                                    point.Y = this.m_EndSubPoint.Y + 2f;
                                    if (point.Y + (float)font.Height > (float)this.m_Height)
                                    {
                                        point.Y = this.m_EndSubPoint.Y - (float)font.Height - 2f;
                                    }
                                }
                                graphics.DrawString(text3, font, Brushes.DarkBlue, point);
                            }
                            else
                            {
                                PointF point2 = new PointF(this.m_StartSubPiont.X + num2 / 2f - width / 2f, this.m_StartSubPiont.Y - (float)font.Height - 2f);
                                if (point2.Y < 0f)
                                {
                                    point2.Y = this.m_StartSubPiont.Y + 2f;
                                }
                                graphics.DrawString(text, font, Brushes.DarkBlue, point2);
                                point2 = new PointF(this.m_EndSubPoint.X + 2f, this.m_EndSubPoint.Y - num5 / 2f - (float)(font.Height / 2));
                                if (point2.X + width2 > (float)this.m_Width)
                                {
                                    point2.X = this.m_EndSubPoint.X - width2 - 2f;
                                }
                                graphics.DrawString(text2, font, Brushes.DarkBlue, point2);
                            }
                        }
                        goto IL_8E6;
                    }
                }
                if (this.m_StartSubPiont != null)
                {
                    graphics.FillEllipse(Brushes.Red, new RectangleF(this.m_StartSubPiont.X - 3f, this.m_StartSubPiont.Y - 3f, 6f, 6f));
                    float num7 = Math.Abs(this.m_StartSubPiont.YValue);
                    string text4;
                    if (num7 > 1000000f)
                    {
                        text4 = string.Format("({0:f2}{1})", this.m_StartSubPiont.YValue / 1000000f, "V");
                    }
                    else if (num7 > 1000f)
                    {
                        text4 = string.Format("({0:f2}{1})", this.m_StartSubPiont.YValue / 1000f, "mV");
                    }
                    else
                    {
                        text4 = string.Format("({0:f2}{1})", this.m_StartSubPiont.YValue, "μV");
                    }
                    PointF point3 = new PointF(this.m_StartSubPiont.X + 4f, this.m_StartSubPiont.Y + 4f);
                    using (Font font2 = new Font("宋体", 10f))
                    {
                        float width4 = graphics.MeasureString(text4, font2).Width;
                        if (point3.X + width4 > (float)this.m_Width)
                        {
                            point3.X = (float)this.m_Width - width4;
                        }
                        if (point3.Y + (float)font2.Height > (float)this.m_Height)
                        {
                            point3.Y = this.m_StartSubPiont.Y - 4f - (float)font2.Height;
                        }
                        graphics.DrawString(text4, font2, Brushes.Black, point3);
                    }
                }
            IL_8E6:;
            }
        }
        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            this.m_xoffSet = e.NewValue;
            this.m_StartSubPiont = null;
            this.m_valueShowFresh = true;
            this.panel1.Invalidate();
        }
        /// <summary>
        ///   绘制曲线
        /// </summary>
        /// <returns></returns>
        private Image getMap()
        {
            int num = this.xAxis.Interval * this.xDistanceProportion + 2 * this.m_docksize;
            if (num < this.m_Width)
            {
                num = this.m_Width;
                this.hScrollBar1.Visible = false;
            }
            else
            {
                this.hScrollBar1.Visible = true;
                int num2 = (num - this.m_Width) / this.m_SecondSpanDistance;
                this.hScrollBar1.Maximum = num2 + 9;
            }
            int height = this.m_Height;
            Bitmap bitmap = new Bitmap(num, height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clip = new Region(new Rectangle(0, 0, num, height));
                graphics.Clear(Color.WhiteSmoke);
                graphics.SmoothingMode = SmoothingMode.None;
                this.xAxis.Displacement = (float)(this.xAxis.Interval * this.xDistanceProportion);
                this.yAxis.Displacement = (float)(height - 2 * this.m_docksize);
                this.xAxis.DrawXAxis(graphics, this.m_docksize, num, height);
                this.yAxis.DrawYAxis(graphics, this.m_docksize, num, height);
                using (Pen pen = new Pen(Brushes.LightSkyBlue, 2f))
                {
                    PointF[] array = this.ConvertToPoints(this.m_DataSource);
                    if (array.Length > 0)
                    {
                        graphics.DrawLines(pen, array);
                    }
                    this.m_DataSourcePoints = new List<PointF>(array);
                }
            }
            return bitmap;
        }
        /// <summary>
        /// 数据转换
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private PointF[] ConvertToPoints(List<float> data)
        {
            PointF[] array = new PointF[data.Count];
            for (int i = 0; i < data.Count; i++)
            {
                array[i] = new PointF(this.xAxis.ValueRate * (float)this.TimeSpan * (float)i + (float)this.m_docksize, (float)this.m_Height - (this.yAxis.ValueRate * (data[i] - this.yAxis.MinValue) + (float)this.m_docksize));
            }
            return array;
        }
        #region  公有成员
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="data">被选中区域数据点</param>
        /// <param name="startTime">选中区域的开始时间</param>
        /// <param name="timespan">数据点的采集频率</param>
        public void InitSmithData(List<float> data, DateTime startTime, int timespan)
        {
            if (data.Count > 1)
            {
                this.TimeSpan = timespan;
                int num = timespan * data.Count;
                this.xAxis.Interval = ((num % 1000 == 0) ? (num / 1000) : (num / 1000 + 1));
                this.xAxis.MaxValue = (float)(this.xAxis.Interval * 1000);
                this.xAxis.LegendLables.Add(startTime.ToString());
                float num2 = data[0];
                float num3 = data[0];
                for (int i = 1; i < data.Count; i++)
                {
                    if (num2 < data[i])
                    {
                        num2 = data[i];
                    }
                    else if (num3 > data[i])
                    {
                        num3 = data[i];
                    }
                }
                this.yAxis.SetMaxMinValue(num2 + 5f, num3 - 5f);
                for (int j = 0; j < this.xAxis.Interval; j++)
                {
                    this.xAxis.LegendLables.Add(string.Format("+{0}s", j + 1));
                }
                this.m_DataSource = new List<float>(data);
                this.m_SourceImage = this.getMap();
            }
        }
        /// <summary>
        /// x轴一个刻度所占像素个数
        /// </summary>
        public int xDistanceProportion = 200;
        /// <summary>
        /// y轴一个刻度所占像素个数
        /// </summary>
        public int yDistanceProportion = 50;
        /// <summary>
        /// 采样周期
        /// </summary>
        public int TimeSpan = 10;
        #endregion
    }
}
