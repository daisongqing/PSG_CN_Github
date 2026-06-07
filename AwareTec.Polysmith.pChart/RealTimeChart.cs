using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AwareTec.Polysmith.pChart
{
    public partial class RealTimeChart : UserControl
    {
        /// <summary>
        /// 曲线集合
        /// </summary>
        private List<CurveItem> m_curveItems = new List<CurveItem>();
        private System.Timers.Timer m_Timer1 = null;
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
        private Image m_SourceImage = null;
        public RealTimeChart()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint |
         ControlStyles.AllPaintingInWmPaint |
         ControlStyles.OptimizedDoubleBuffer |
         ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();
            xAxis = new XAxis() { BackColor = Color.WhiteSmoke, ForeColor = Color.LimeGreen, AxisVisible = true, CalibrationsVisible = true, Displacement = m_Width, MaxValue = 30000 };
            yAxis = new YAxis() { BackColor = Color.WhiteSmoke, ForeColor = Color.LimeGreen, AxisVisible = true, Displacement = m_Height };
            this.Load += RealTimeChart_Load;
            for (int i = 0; i < 20; i++)
            {
                CurveItem item = new CurveItem();
                item.TimeSpan = 10;
                item.ChannelNo = i;
                m_curveItems.Add(item);
                random[i] = new Random();
            }
        }
        /// <summary>
        /// 防止拖拽的时候出现卡顿
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {///在当前控件内会导致panel1的效果出不来
             ///
                CreateParams cp = base.CreateParams;
                cp.ExStyle = 0x02000000;////用双缓冲绘制窗口的所有子控件
                return cp;
            }
        }
        Random[] random = new Random[20];
        private void RealTimeChart_Load(object sender, EventArgs e)
        {
            this.Paint += RealTimeChart_Paint;
            this.Resize += RealTimeChart_Resize;
            m_Timer1 = new System.Timers.Timer();
            m_Timer1.Interval = 10;
            m_Timer1.Enabled = true;
            m_Timer1.Elapsed += m_Timer1_Elapsed;
            RealTimeChart_Resize(null, null);
        }

        void m_Timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                for (int s = 0; s < 10; s++)
                {
                    m_curveItems[i].AddDatavalue(random[i].Next(0, 100), false, "");
                }
            }
            this.Invalidate();
        }

        private void RealTimeChart_Resize(object sender, EventArgs e)
        {
            if (this.Width != 0 || this.Height != 0)
            {
                m_Height = this.Height;
                m_Width = this.Width;
                int ydistance = m_Height / 20;
                for (int i = 0; i < 20; i++)
                {
                    m_curveItems[i].xAxis.Displacement = m_Width;
                    m_curveItems[i].xAxis.MaxValue = xAxis.MaxValue;
                    m_curveItems[i].xAxis.MinValue = xAxis.MinValue;
                    m_curveItems[i].yAxis.Displacement = ydistance;
                    m_curveItems[i].yAxis.MaxValue = 100;
                    m_curveItems[i].yAxis.MinValue = 0;
                    m_curveItems[i].yBaseDistance = ydistance * i + ydistance / 2;
                    m_curveItems[i].yLimitMaxDistance = m_Height;
                    m_curveItems[i].yLimitMinDistance = 0;
                }
            }
        }

        private void RealTimeChart_Paint(object sender, PaintEventArgs e)
        {
            Image map = drawImage();
            e.Graphics.DrawImage(map, 0, 0);
            e.Dispose();
            GC.Collect();
        }
        private struct RectangleEx
        {
            public int X;
            public int Y;
            public int Offset;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public Point[] points;
        }
        private RectangleEx m_rect = new RectangleEx();
        private Image drawImage()
        {
            if (m_SourceImage == null)
            {
                m_SourceImage = new Bitmap(m_Width, m_Height);
                using (Graphics g = Graphics.FromImage(m_SourceImage))
                {
                    int w = this.m_Width;
                    int h = this.m_Height;
                    g.Clip = new Region(new Rectangle(0, 0, w, h));
                    int x_count = xAxis.Calibrations.Count;
                    int y_count = yAxis.Calibrations.Count;
                    int docksize = 0;
                    g.Clear(this.m_backcolor);
                    g.SmoothingMode = SmoothingMode.None;
                    ///绘制x主轴坐标
                    xAxis.DrawXAxis(g, docksize, w, h);
                    ///绘制y主轴坐标
                    yAxis.DrawYAxis(g, docksize, w, h);
                    for (int i = 0; i < m_curveItems.Count; i++)
                    {
                        if (!m_curveItems[i].Visible)
                            continue;
                        ///绘制通道的刻度尺
                        m_curveItems[i].yAxis.drawyAxis(g, docksize, w, h);
                    }
                }
            }
            else
            {
                using (Graphics g = Graphics.FromImage(m_SourceImage))
                {
                    Image newImg = new Bitmap(40, m_Height);
                    using (Graphics g2 = Graphics.FromImage(newImg))
                    {
                        int len = 0;
                        for (int i = 0; i < m_curveItems.Count; i++)
                        {
                            if (!m_curveItems[i].Visible)
                                continue;
                            ///绘制通道的刻度尺
                            m_curveItems[i].yAxis.drawyAxis(g2, 0, 40, m_Height);
                            len = (int)(38 / (m_curveItems[i].xAxis.ValueRate * m_curveItems[i].TimeSpan));
                            PointF[] points = m_curveItems[i].GetxyDataValues(m_rect.Offset, ref len).Points;
                            if (points.Length > 0)
                                using (Pen pointpen = new Pen(m_curveItems[i].PenColor, m_curveItems[i].PenWidth))
                                {
                                    g2.DrawLines(pointpen, points);
                                }
                            else
                                return m_SourceImage;
                        }
                        g.DrawImage(newImg, new Rectangle(m_rect.X, 0, 40, m_Height), new Rectangle(0, 0, 40, m_Height), GraphicsUnit.Pixel);
                        m_rect.Offset += len;
                        m_rect.X += 40;
                        if (m_rect.X == m_Width)
                        {
                            m_rect.X = 0;
                        }
                    }
                }
            }
            return m_SourceImage;
        }
        /// <summary>
        /// 默认背景色为黑
        /// </summary>
        private Color m_backcolor = System.Drawing.SystemColors.Window;
        /// <summary>
        /// 获取或设置背景色
        /// </summary>
        public new Color BackColor { set { m_backcolor = value; } get { return m_backcolor; } }
    }
}
