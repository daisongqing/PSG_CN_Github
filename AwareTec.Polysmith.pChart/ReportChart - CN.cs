using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace AwareTec.Polysmith.pChart
{
    public partial class ReportChart : UserControl
    {
        private readonly Color _reportChartColor = Color.FromArgb(243, 245, 255);
        #region 私有字段
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
        private int m_docksize = 30;
        /// <summary>
        /// 存放源数据的队列
        /// </summary>
        private SuperPointF[] m_DataSource = new SuperPointF[0];
        /// <summary>
        /// 滚动条滚动小格占位移量
        /// </summary>
        private int m_OneFrameDistance = 2;
        /// <summary>
        /// 绘制好的原图
        /// </summary>
        private Image m_SourceImage = null;
        /// <summary>
        /// 绘制图像的总宽度
        /// </summary>
        private int ImageWidth = 0;
        private bool hasPaint = false;
        #endregion
        public ReportChart()
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
                Displacement = (float)this.m_Width
            };
            this.yAxis = new YAxis
            {
                BackColor = Color.Black,
                ForeColor = Color.LightGray,
                AxisVisible = true,
                CalibrationsVisible = true,
                Displacement = (float)this.m_Height
            };
            base.Load += this.ReportChart_Load;

        }

        public ReportChart(Color reportChartColor)
        {
            this.InitializeComponent();
            _reportChartColor = reportChartColor;
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            base.UpdateStyles();
            this.xAxis = new XAxis
            {
                BackColor = Color.Black,
                ForeColor = Color.LightGray,
                AxisVisible = true,
                CalibrationsVisible = true,
                Displacement = (float)this.m_Width
            };
            this.yAxis = new YAxis
            {
                BackColor = Color.Black,
                ForeColor = Color.LightGray,
                AxisVisible = true,
                CalibrationsVisible = true,
                Displacement = (float)this.m_Height
            };
            base.Load += this.ReportChart_Load;

        }

        void ReportChart_Load(object sender, EventArgs e)
        {
            this.panel1.Paint += this.panel1_Paint;
            this.panel1.MouseDown += this.panel1_MouseDown;
            this.panel1.MouseMove += this.panel1_MouseMove;
            this.panel1.MouseUp += this.panel1_MouseUp;
            this.panel1.Resize += this.panel1_Resize;
        }
        #region 鼠标动作效果
        private bool MouseIsDown = false;
        private SuperPointF m_StartSubPiont = null;
        private SuperPointF m_EndSubPiont = null;
        /// <summary>
        /// 翻页后用于绘制选中区域的对象
        /// </summary>
        private Rectangle m_NewMouseRect = new Rectangle(0, 0, 0, 0);
        /// <summary>
        /// 记录初始位置
        /// </summary>
        private Rectangle m_SatrtMouseRect = Rectangle.Empty;
        RectangleMarkers m_NewRectMark = null;
        /// <summary>
        /// 多次小睡创建委托
        /// </summary>
        public delegate void createmultsleep(List<IMarker> multsleeplist, RectangleMarkers sleepmark, MouseEventArgs e);
        /// <summary>
        /// 多次小睡创建事件
        /// </summary>
        public event createmultsleep createmultsleephandle;
        /// <summary>
        /// 多次小睡创建委托
        /// </summary>
        public delegate void editmultsleep(List<IMarker> multsleeplist, RectangleMarkers sleepmark, MouseEventArgs e);
        /// <summary>
        /// 判断是否删除成功
        /// </summary>
        private bool isdeleteok = false;
        /// <summary>
        /// 多次小睡创建事件
        /// </summary>
        public event editmultsleep editmultsleephandle;
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            this.m_EndSubPiont = this.CheckPoint(e.Location);
            if (this.bak_FrameCnt != this.m_CurrentFrameNo)
            {
                this.bak_FrameCnt = this.m_CurrentFrameNo;
                if (this.CurrentFrameChangedHandler != null)
                {
                    this.CurrentFrameChangedHandler.BeginInvoke(this.m_CurrentFrameNo, null, null);
                }
            }
            if (e.Button == MouseButtons.Right && Cursor != Cursors.SizeWE && !ScoreLockStatus)
            {
                if (m_selectMarker != null)
                {
                    if (m_selectMarker.EndIndex < this.m_EndSubPiont.XValue || m_selectMarker.StartIndex > this.m_EndSubPiont.XValue)
                    {
                        m_selectMarker.isSelected = false;
                    }
                    else
                    {
                        MultipleSleepMarks.Remove(m_selectMarker);
                        isdeleteok = true;
                        m_selectMarker = null;
                    }
                }
                else
                {
                    m_SelectRectangeView = Rectangle.Empty;
                    if (m_NewRectMark == null)
                        return;
                    if (m_NewRectMark.EndIndex < 0)
                        return;
                    if (m_NewRectMark.EndIndex < m_NewRectMark.StartIndex)
                    {
                        ///交换帧序后，以前的开始帧序号需要+1，结束帧序号需要-1
                        int bak_startIndex = m_NewRectMark.StartIndex;
                        m_NewRectMark.StartIndex = m_NewRectMark.EndIndex - 1;
                        m_NewRectMark.EndIndex = bak_startIndex + 1;
                    }
                    if (m_NewRectMark.EndIndex - m_NewRectMark.StartIndex < 20)
                    {
                        return;
                    }
                    bool add = false;
                    MultipleSleepMarks.RemoveAll(t =>
                    {
                        RectangleMarkers rec = t as RectangleMarkers;
                        return m_NewRectMark.StartIndex <= rec.EndIndex && m_NewRectMark.EndIndex >= rec.StartIndex;
                    });
                    m_NewRectMark.ID = string.Format("22/1 {0}-{1}", m_NewRectMark.StartIndex, m_NewRectMark.EndIndex);
                    for (int i = 0; i < MultipleSleepMarks.Count; i++)
                    {
                        RectangleMarkers rec = MultipleSleepMarks[i] as RectangleMarkers;
                        if (m_NewRectMark.StartIndex < rec.StartIndex)
                        {
                            MultipleSleepMarks.Insert(i, m_NewRectMark);
                            add = true;
                            break;
                        }
                    }
                    if (!add)
                    {
                        MultipleSleepMarks.Add(m_NewRectMark);
                    }
                    if (createmultsleephandle != null)
                        createmultsleephandle.Invoke(MultipleSleepMarks, m_NewRectMark, e);
                }
                this.m_SourceImage = this.getMap();
            }
            else if (Cursor == Cursors.SizeWE && m_selectMarker != null && !ScoreLockStatus)
            {
                if (MultipleSleepMarks.RemoveAll(t =>
                 {
                     RectangleMarkers rec = t as RectangleMarkers;
                     return m_selectMarker.StartIndex <= rec.EndIndex && m_selectMarker.EndIndex >= rec.StartIndex && t != m_selectMarker;
                 }) > 0)
                {
                    this.m_SourceImage = this.getMap();
                }
                if (editmultsleephandle != null)
                    editmultsleephandle.Invoke(MultipleSleepMarks, m_selectMarker, e);
                m_selectMarker = null;
            }
            if (isdeleteok)
            {
                SleepMarkChanged(MultipleSleepMarks);
                isdeleteok = false;
            }
            panel1.Invalidate();
            this.MouseIsDown = false;
        }
        private int bak_FrameCnt = 1;
        private RectangleMarkers m_selectMarker = null;
        private int m_HandSelect = 0;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.MouseIsDown)
            {
                if (this.m_StartSubPiont != null)
                {
                    this.m_StartSubPiont = this.CheckPoint(e.Location);
                }
                if (!ScoreLockStatus)
                {
                    if (e.Button == MouseButtons.Right && Cursor != Cursors.SizeWE)
                    {
                        if (m_selectMarker != null)
                        {
                            return;
                        }
                        DrawRectangle(new Rectangle(m_SatrtMouseRect.X, m_SatrtMouseRect.Y, e.X - m_SatrtMouseRect.X, m_Height - m_SatrtMouseRect.Y - m_docksize - 2), false);
                        m_NewRectMark.EndIndex = (int)m_StartSubPiont.XValue;///标记范围的结束时间为当前帧的结束时间，也就是下一帧的开始时间，所以此处索引号不需要减一
                    }
                    else if (e.Button == MouseButtons.Left && Cursor == Cursors.SizeWE)
                    {
                        if (m_HandSelect == 2)
                        {
                            if (m_StartSubPiont.XValue - m_selectMarker.StartIndex < 20)
                            {
                                return;
                            }
                            m_selectMarker.EndIndex = (int)m_StartSubPiont.XValue;
                        }
                        else if (m_HandSelect == 1)
                        {
                            if (m_selectMarker.EndIndex - m_StartSubPiont.XValue < 20)
                            {
                                return;
                            }
                            m_selectMarker.StartIndex = (int)m_StartSubPiont.XValue - 1;
                        }
                        m_SourceImage = getMap();
                    }
                }
                this.m_CurrentFrameNo = (int)m_StartSubPiont.XValue;
                this.panel1.Invalidate();
            }
            else
            {
                if (!ScoreLockStatus)
                {
                    float off = this.m_xoffSet * this.m_OneFrameDistance;
                    PointF cursorLocation = e.Location;
                    IMarker find = MultipleSleepMarks.Find(t => new RectangleF(t.HeadRectangle.X - 5 - off, t.HeadRectangle.Y, t.HeadRectangle.Width + 10, t.HeadRectangle.Height).Contains(cursorLocation));
                    if (find != null)
                    {
                        m_selectMarker = find as RectangleMarkers;
                        m_HandSelect = new RectangleF(find.HeadRectangle.X - 5 - off, find.HeadRectangle.Y, 10, find.HeadRectangle.Height).Contains(cursorLocation) ? 1 :
                            new RectangleF(find.HeadRectangle.Right - 5 - off, find.HeadRectangle.Y, 10, find.HeadRectangle.Height).Contains(cursorLocation) ? 2 : 0;
                        if (m_HandSelect > 0)
                        {
                            Cursor = Cursors.SizeWE;
                            return;
                        }
                    }
                    else
                    {
                        m_selectMarker = null;
                    }
                    Cursor = Cursors.Default;
                }
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            this.MouseIsDown = true;
            this.m_StartSubPiont = this.CheckPoint(e.Location);
            this.m_CurrentFrameNo = (int)m_StartSubPiont.XValue;
            if (e.Button == MouseButtons.Right && Cursor != Cursors.SizeWE && !ScoreLockStatus)
            {
                if (m_selectMarker != null)
                {
                    m_selectMarker.isSelected = true;
                    m_SourceImage = getMap();
                }
                else
                {
                    //在其他区域右键点击过后，在右键点击小睡事件不可以直接删除，这里增加查找
                    float off = this.m_xoffSet * this.m_OneFrameDistance;
                    PointF cursorLocation = e.Location;
                    IMarker find = MultipleSleepMarks.Find(t => new RectangleF(t.HeadRectangle.X - 5 - off, t.HeadRectangle.Y, t.HeadRectangle.Width + 10, t.HeadRectangle.Height).Contains(cursorLocation));
                    if (find != null)
                    {
                        m_selectMarker = find as RectangleMarkers;
                    }
                    m_SatrtMouseRect = new Rectangle((int)m_StartSubPiont.X, m_docksize - 2, 0, 0);
                    m_NewRectMark = new RectangleMarkers() { StartIndex = (int)m_StartSubPiont.XValue - 1, EndIndex = -1, MarkTyp = IMarker.MarkType.MultipleSleep };
                }
            }
            this.panel1.Invalidate();
        }
        /// <summary>
        /// 绘制框选区域
        /// </summary>
        private Rectangle m_SelectRectangeView = Rectangle.Empty;

        private void DrawRectangle(Rectangle rect2, bool Invalidate = true)
        {
            if (rect2.Width == 0)
                return;
            m_SelectRectangeView = new Rectangle(rect2.X, rect2.Y, rect2.Width, rect2.Height);
            if (m_SelectRectangeView.Width < 0)
            {
                m_SelectRectangeView.X += m_SelectRectangeView.Width;
                m_SelectRectangeView.Width = 0 - m_SelectRectangeView.Width;
            }
            if (Invalidate)
                panel1.Invalidate();

        }
        /// <summary>
        /// 检查轴线点
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private SuperPointF CheckPoint(Point p)
        {
            SuperPointF superPointF = new SuperPointF();
            int num = this.m_xoffSet * this.m_OneFrameDistance;
            int num2 = (this.m_docksize < num) ? 0 : (this.m_docksize - num);
            float num3 = (float)(this.m_TotalFrameCnt - 1) * this.xAxis.ValueRate + (float)this.m_docksize;
            if (p.X < num2)
            {
                superPointF.X = (float)num2;
            }
            else if ((float)(p.X + num) > num3)
            {
                superPointF.X = (float)((int)num3 - num);
            }
            else
            {
                superPointF.X = (float)p.X;
            }
            superPointF.XValue = (superPointF.X - (float)this.m_docksize + (float)num) / this.xAxis.ValueRate + 1;///superPointF.X从0开始所以帧的偏移数为1
            superPointF.YMax = p.Y < m_docksize ? m_docksize : p.Y > m_Height ? m_Height : p.Y;
            return superPointF;
        }
        #endregion
        //protected override void WndProc(ref Message m)
        //{
        //    switch (m.Msg)
        //    {
        //        case 0x0f:

        //            break;
        //    }
        //    Console.WriteLine(string.Format("消息描述：{0}  消息代码：{1}", m.Msg, m.ToString()));
        //    base.WndProc(ref m);
        //}
        /// <summary>
        /// 是否局部刷新
        /// </summary>
        private bool m_RefreshRect = false;
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            DateTime dt = DateTime.Now;
            using (Graphics graphics = e.Graphics)
            {
                graphics.Clear(BackColor);
                if (this.m_SourceImage != null)
                {
                    int num = this.m_xoffSet * this.m_OneFrameDistance;
                    graphics.DrawImage(this.m_SourceImage, new RectangleF(0f, 0f, (float)this.m_Width, (float)this.m_Height), new RectangleF((float)num, 0f, (float)this.m_Width, (float)this.m_Height), GraphicsUnit.Pixel);
                    this.ClientPoints = new List<SuperPointF>();
                    if (m_SelectRectangeView.Width > 0)
                    {
                        graphics.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.Yellow)), m_SelectRectangeView);
                        graphics.DrawRectangle(new Pen(Color.LightYellow), m_SelectRectangeView);
                    }
                    else
                    {
                        if (this.m_StartSubPiont == null)
                        {
                            this.m_StartSubPiont = new SuperPointF();
                            this.m_StartSubPiont.X = (float)this.m_docksize;
                            this.m_StartSubPiont.XValue = 1f;
                        }
                        else if (0f <= this.m_StartSubPiont.X && this.m_StartSubPiont.X <= (float)this.m_Width)
                        {
                            graphics.DrawImage(this.DrawTimeLine(this.m_StartSubPiont.X), new Point(0, 0));
                        }
                    }
                    if (this.m_DataSource != null)
                    {
                        for (int i = 0; i < this.m_DataSource.Length; i++)
                        {
                            if (this.m_DataSource[i] != null && this.m_DataSource[i].X >= (float)num && this.m_DataSource[i].X <= (float)(num + this.m_Width))
                            {
                                SuperPointF item = new SuperPointF
                                {
                                    X = this.m_DataSource[i].X - (float)num,
                                    YMax = this.m_DataSource[i].YMax,
                                    YMin = this.m_DataSource[i].YMin,
                                    YMaxValue = this.m_DataSource[i].YMaxValue,
                                    YMinValue = this.m_DataSource[i].YMinValue
                                };
                                this.ClientPoints.Add(item);
                            }
                        }
                    }
                }
                this.DrawValue(graphics);
                this.m_RefreshRect = false;
                Console.WriteLine(string.Format("【{1}】报告图谱绘制耗时：{0}ms", (DateTime.Now - dt).TotalMilliseconds, DateTime.Now));
            }
        }
        Rectangle rect = Rectangle.Empty;
        /// <summary>
        /// 绘制参数显示
        /// </summary>
        private void DrawValue(Graphics g)
        {
            //g.Clip = new Region(this.rect);
            //g.Clear(Color.WhiteSmoke);
            using (Font font = new Font("宋体", 9f, FontStyle.Bold))
            {
                if (m_SelectRectangeView.Width > 0)
                {
                    m_NewRectMark.StartTime = m_startTime.AddSeconds(m_NewRectMark.StartIndex * 30);
                    m_NewRectMark.EndTime = m_startTime.AddSeconds(m_NewRectMark.EndIndex * 30);
                    g.DrawString(string.Format("小睡标记开始：{0} <  {1:f1}min  > {2}", m_NewRectMark.StartTime.ToString("HH:mm:ss"), Math.Abs((m_NewRectMark.EndIndex - m_NewRectMark.StartIndex) * 0.5f), m_NewRectMark.EndTime.ToString("HH:mm:ss")), font, new SolidBrush(Color.FromArgb(27, 211, 171)), (float)this.rect.Left, (float)this.rect.Top);
                }
                else if (m_selectMarker != null)
                {
                    RectangleMarkers sleepmarkdetail = MultipleSleepMarks.Find(t => (t as RectangleMarkers).EndIndex == m_selectMarker.EndIndex && (t as RectangleMarkers).StartIndex == m_selectMarker.StartIndex) as RectangleMarkers;
                    int[] offs = new int[2] { 0, 0 };
                    if (!string.IsNullOrEmpty(sleepmarkdetail.Comments2))
                    {
                        string[] offtimes = sleepmarkdetail.Comments2.Split('-');
                        if (offtimes.Length == 2)
                        {
                            offs[0] = Convert.ToInt32(offtimes[0]);
                            offs[1] = Convert.ToInt32(offtimes[1]);
                        }
                    }
                    m_selectMarker.StartTime = m_startTime.AddSeconds(m_selectMarker.StartIndex * 30);
                    m_selectMarker.EndTime = m_startTime.AddSeconds(m_selectMarker.EndIndex * 30);
                    g.DrawString(string.Format("小睡标记调整：{0} <  {1:f1}min  > {2}", m_selectMarker.StartTime.AddSeconds(offs[0]).ToString("HH:mm:ss"), Math.Abs((m_selectMarker.EndIndex - m_selectMarker.StartIndex) * 0.5f), m_selectMarker.EndTime.AddSeconds(offs[1]).ToString("HH:mm:ss")), font, new SolidBrush(Color.FromArgb(27, 211, 171)), (float)this.rect.Left, (float)this.rect.Top);
                }
                else
                    g.DrawString(string.Format("当前帧序号：{0}", this.m_CurrentFrameNo), font, Brushes.Black, (float)this.rect.Left, (float)this.rect.Top);
            }
        }
        /// <summary>
        /// 绘制当前显示的时间轴线
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        private Image DrawTimeLine(float xLocation)
        {
            Bitmap bitmap = new Bitmap(this.m_Width, this.m_Height);
            bitmap.MakeTransparent();
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (Pen pen = new Pen(Brushes.DarkRed, 1f))
                {
                    pen.DashStyle = DashStyle.Dash;
                    pen.DashPattern = new float[]
                    {
                        3f,
                        2f
                    };
                    using (Font font = new Font("宋体", 9f))
                    {
                        string text = this.m_startTime.AddSeconds((double)((this.m_CurrentFrameNo - 1) * 30)).ToString("HH:mm:ss");
                        SizeF sizeF = graphics.MeasureString(text, font);
                        float num = xLocation - sizeF.Width / 2f;
                        if (num < 0f)
                        {
                            num = 0f;
                        }
                        else if (num + sizeF.Width > (float)this.m_Width)
                        {
                            num = (float)this.m_Width - sizeF.Width;
                        }
                        float y = (float)this.m_docksize;
                        graphics.DrawString(text, font, Brushes.DarkRed, new PointF(num, y));
                    }
                    graphics.DrawLine(pen, new Point((int)xLocation, this.m_Height - this.m_docksize), new Point((int)xLocation, this.m_docksize));
                }
            }
            return bitmap;
        }
        private void panel1_Resize(object sender, EventArgs e)
        {
            if (this.panel1.Width != 0 && this.panel1.Height != 0)
            {
                this.m_Height = this.panel1.Height;
                this.m_Width = this.panel1.Width;
                this.rect = new Rectangle(40, this.m_Height - 15, 200, 20);
                this.m_SourceImage = this.getMap();
                if (this.hScrollBar1.Visible)
                {
                    this.m_xoffSet = ((this.hScrollBar1.Value == this.hScrollBar1.Maximum) ? (this.hScrollBar1.Value - 9) : this.hScrollBar1.Value);
                    return;
                }
                this.m_xoffSet = 0;
            }
        }
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
        /// <summary>
        /// 当前显示区域的数据点
        /// </summary>
        private List<SuperPointF> ClientPoints { set; get; }
        /// <summary>
        /// 所有坐标点
        /// </summary>
        private List<PointF> m_DataSourcePoints { set; get; }
        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            setValue(e.NewValue);
        }
        private void setValue(int value)
        {
            int xoffSet = this.m_xoffSet;
            this.m_xoffSet = value;
            this.m_StartSubPiont.X -= (float)((this.m_xoffSet - xoffSet) * this.m_OneFrameDistance);
            this.panel1.Invalidate();
        }
        /// <summary>
        /// 更新軸線的當前坐標
        /// </summary>
        /// <returns></returns>
        private void ReSetStartPoint()
        {
            if (this.m_StartSubPiont != null)
            {
                this.m_StartSubPiont.X = (float)(this.m_CurrentFrameNo - 1) * this.xAxis.ValueRate + (float)this.m_docksize - (float)(this.m_xoffSet * this.m_OneFrameDistance);
                int num = this.m_Width / (2 * this.m_OneFrameDistance);
                if (this.m_StartSubPiont.X < 0f && this.m_xoffSet > 0)
                {
                    num = ((num > this.m_xoffSet) ? (this.m_xoffSet - 1) : num);
                    this.hScrollBar1.Value -= num;
                    this.setValue(this.hScrollBar1.Value);
                    return;
                }
                if (this.m_StartSubPiont.X > (float)this.m_Width && this.m_xoffSet < this.hScrollBar1.Maximum - 9)
                {
                    if (num < this.hScrollBar1.Maximum - 9 - this.hScrollBar1.Value)
                    {
                        this.hScrollBar1.Value += num;
                    }
                    else
                    {
                        this.hScrollBar1.Value = this.hScrollBar1.Maximum - 9;
                    }
                    this.setValue(this.hScrollBar1.Value);
                }
            }
        }
        public delegate void HSrollBarExUpdate(List<IMarker> MultipleSleepMarks);
        public event HSrollBarExUpdate HSrollBarExUpdateHandele;
        public delegate void curveAreaMultSleepUpdate(List<IMarker> MultipleSleepMarks);
        public event curveAreaMultSleepUpdate curveAreaMultSleepUpdateHandele;
        /// <summary>
        ///   绘制曲线
        /// </summary>
        /// <returns></returns>
        private Image getMap()
        {
            int num = this.ImageWidth = this.xAxis.Interval * this.xDistanceProportion + 2 * this.m_docksize;
            if (num < this.m_Width)
            {
                num = this.m_Width;
                this.hScrollBar1.Visible = false;
            }
            else
            {
                this.hScrollBar1.Visible = true;
                int num2 = (num - this.m_Width) / this.m_OneFrameDistance;
                this.hScrollBar1.Maximum = num2 + 9;
            }
            int height = this.m_Height;
            Bitmap bitmap = new Bitmap(num, height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clip = new Region(new Rectangle(0, 0, num, height));
                graphics.Clear(_reportChartColor);
                graphics.SmoothingMode = SmoothingMode.None;
                this.xAxis.Displacement = (float)(this.xAxis.Interval * this.xDistanceProportion);
                this.yAxis.Displacement = (float)(height - 2 * this.m_docksize);
                this.DrawXAxis(graphics, this.m_docksize, num, height, this.m_Datetimelegends);
                this.DrawYAxis(graphics, this.m_docksize, num, height);
                int markCnt = MultipleSleepMarks.Count;
                if (markCnt > 0)
                {
                    float firstStartLocation = m_docksize;
                    for (int s = 0; s < markCnt; s++)
                    {
                        RectangleMarkers rect = MultipleSleepMarks[s] as RectangleMarkers;
                        ///虚线的限制线右边界有1个帧的偏移
                        float startLocation = rect.StartIndex * this.xAxis.ValueRate + m_docksize;
                        float endLocation = (rect.EndIndex - 1) * this.xAxis.ValueRate + m_docksize;
                        rect.HeadRectangle = new RectangleF(startLocation, m_docksize, endLocation - startLocation, yAxis.Displacement - 2);
                        if (rect.isSelected)
                        {
                            graphics.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.SkyBlue)), rect.HeadRectangle);
                        }
                        DrawUnVaildLine(graphics, firstStartLocation, s != 0, startLocation, true);
                        firstStartLocation = endLocation;
                    }
                    DrawUnVaildLine(graphics, firstStartLocation, true, num - m_docksize, false);
                    if (HSrollBarExUpdateHandele != null)
                    {
                        HSrollBarExUpdateHandele.Invoke(MultipleSleepMarks);
                    }
                    if (curveAreaMultSleepUpdateHandele != null)
                    {
                        curveAreaMultSleepUpdateHandele.Invoke(MultipleSleepMarks);
                    }

                }
                else if (this.ShowLightOnAndOff)
                {
                    if (this.m_lightOffLocation != -1f)
                    {
                        DrawUnVaildLine(graphics, m_docksize, false, m_lightOffLocation, true);
                    }
                    if (this.m_lightOnLocation != -1f)
                    {
                        DrawUnVaildLine(graphics, m_lightOnLocation, true, this.ImageWidth - m_docksize, false);
                    }
                }
                using (Pen pen4 = new Pen(Brushes.LightSkyBlue, 1f))
                {
                    if (this.m_DataSource != null)
                    {
                        int num14 = this.m_DataSource.Length;
                        num14 = ((num14 > this.m_TotalFrameCnt) ? this.m_TotalFrameCnt : num14);
                        float num15 = (float)(this.m_Height - this.m_docksize);
                        Pen[] pens = new Pen[yAxis.CalibrationsColors.Count];
                        for (int i = 0; i < pens.Length; i++)
                        {
                            pens[i] = new Pen(yAxis.CalibrationsColors[i], 3f);
                        }
                        bool userCalibrationsColor = pens.Length > 0;
                        for (int k = 0; k < num14; k++)
                        {
                            if (this.m_DataSource[k] != null)
                            {
                                if (this.m_DataSource[k].YMinValue != -1f)
                                {
                                    this.m_DataSource[k].X = this.m_DataSource[k].XValue * this.xAxis.ValueRate + (float)this.m_docksize;
                                    this.m_DataSource[k].YMin = num15 - (this.m_DataSource[k].YMinValue - this.yAxis.MinValue) * this.yAxis.ValueRate;
                                    this.m_DataSource[k].YMax = num15 - (this.m_DataSource[k].YMaxValue - this.yAxis.MinValue) * this.yAxis.ValueRate;
                                    this.m_DataSource[k].YMin = ((this.m_DataSource[k].YMin > num15) ? num15 : ((this.m_DataSource[k].YMin < (float)this.m_docksize) ? ((float)this.m_docksize) : this.m_DataSource[k].YMin));
                                    this.m_DataSource[k].YMax = ((this.m_DataSource[k].YMax > num15) ? num15 : ((this.m_DataSource[k].YMax < (float)this.m_docksize) ? ((float)this.m_docksize) : this.m_DataSource[k].YMax));
                                    if (this.m_DataSource[k].YMinValue == this.m_DataSource[k].YMaxValue && this.m_DataSource[k].YMaxValue > 0f)
                                    {
                                        this.m_DataSource[k].YMax += 0.1f;
                                    }
                                    graphics.DrawLine(pen4, new PointF(this.m_DataSource[k].X, this.m_DataSource[k].YMin), new PointF(this.m_DataSource[k].X, this.m_DataSource[k].YMax));
                                }
                                else if (this.m_DataSource[k].YMinValue == -1f && this.m_DataSource[k].YMaxValue >= 0f)
                                {
                                    SuperPointF superPointF = this.m_DataSource[(k + 1 == num14) ? k : (k + 1)];
                                    if (superPointF == null)
                                    {
                                        superPointF = this.m_DataSource[k];
                                    }
                                    float num16 = superPointF.XValue * this.xAxis.ValueRate + (float)this.m_docksize;
                                    this.m_DataSource[k].X = this.m_DataSource[k].XValue * this.xAxis.ValueRate + (float)this.m_docksize;
                                    this.m_DataSource[k].YMin = num15 - (superPointF.YMaxValue - this.yAxis.MinValue) * this.yAxis.ValueRate;
                                    this.m_DataSource[k].YMax = num15 - (this.m_DataSource[k].YMaxValue - this.yAxis.MinValue) * this.yAxis.ValueRate;
                                    this.m_DataSource[k].YMin = ((this.m_DataSource[k].YMin > num15) ? num15 : ((this.m_DataSource[k].YMin < (float)this.m_docksize) ? ((float)this.m_docksize) : this.m_DataSource[k].YMin));
                                    this.m_DataSource[k].YMax = ((this.m_DataSource[k].YMax > num15) ? num15 : ((this.m_DataSource[k].YMax < (float)this.m_docksize) ? ((float)this.m_docksize) : this.m_DataSource[k].YMax));
                                    if (this.m_DataSource[k].YMin != this.m_DataSource[k].YMax)
                                    {
                                        pen4.Width = 1f;
                                        pen4.Color = Color.LightGray;
                                        graphics.DrawLine(pen4, new PointF(this.m_DataSource[k].X, this.m_DataSource[k].YMin), new PointF(this.m_DataSource[k].X, this.m_DataSource[k].YMax));
                                        pen4.Color = Color.LightSkyBlue;
                                    }
                                    if (this.ChangeVLineWith)
                                    {
                                        pen4.Width = 3f;
                                    }
                                    if (num16 == this.m_DataSource[k].X)
                                    {
                                        graphics.FillRectangle(Brushes.LightSkyBlue, num16, this.m_DataSource[k].YMin - 1f, 1f, 3f);
                                    }
                                    else
                                    {
                                        int index = (int)(m_DataSource[k].YMaxValue - yAxis.MinValue - 1);
                                        Pen pp = pen4;
                                        if (index >= 0 && userCalibrationsColor)
                                            pp = pens[index];
                                        graphics.DrawLine(pp, new PointF(num16, this.m_DataSource[k].YMin), new PointF(this.m_DataSource[k].X, this.m_DataSource[k].YMin));
                                    }
                                    this.m_DataSource[k].YMin = -1f;
                                }
                            }
                        }
                    }
                }
            }
            return bitmap;
        }
        /// <summary>
        /// 绘制无效去
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="startLine"></param>
        /// <param name="endLine"></param>
        private void DrawUnVaildLine(Graphics graphics, float startLine, bool sLineVisible, float endLine, bool eLineVisible)
        {
            using (Pen pen = new Pen(Brushes.Blue, 1f))
            {
                pen.DashStyle = DashStyle.Dash;
                pen.DashPattern = new float[]
                            {
                                5f,
                                5f
                            };
                if (sLineVisible)
                    graphics.DrawLine(pen, new PointF(startLine, (float)(m_Height - this.m_docksize)), new PointF(startLine, (float)this.m_docksize));
                using (Pen pen2 = new Pen(Brushes.Gray, 1f))
                {
                    pen2.DashStyle = DashStyle.Dash;
                    pen2.DashPattern = new float[]
                                {
                                    3f,
                                    3f
                                };
                    int num3 = 10;
                    int addSpan = 5;
                    int num5 = (int)(endLine - startLine - (float)num3) / addSpan + 1;
                    float bottomX = startLine;
                    float topX = (float)(num3 + startLine);
                    for (int i = 0; i < num5; i++)
                    {
                        graphics.DrawLine(pen2, new PointF(bottomX, (float)(m_Height - this.m_docksize)), new PointF(topX, (float)this.m_docksize));
                        bottomX += (float)addSpan;
                        topX += (float)addSpan;
                        if (topX > endLine)
                        {
                            graphics.DrawLine(pen2, new PointF(bottomX, (float)(m_Height - this.m_docksize)), new PointF(endLine, (float)this.m_docksize));
                            break;
                        }
                    }
                }
                if (eLineVisible)
                    graphics.DrawLine(pen, new PointF(endLine, (float)(m_Height - this.m_docksize)), new PointF(endLine, (float)this.m_docksize));
            }
        }
        /// <summary>
        ///  绘制X坐标轴
        /// </summary>
        /// <param name="g"></param>
        /// <param name="MarginDistance">偏移量</param>
        public void DrawXAxis(Graphics g, int MarginDistance, int width, int height, List<string> DateTimeLegends)
        {
            Axis axis = xAxis;
            if (axis.AxisVisible)
            {
                Pen pen = new Pen(axis.BackColor, 1);
                Pen bak_pen = new Pen(axis.ForeColor);
                g.DrawLine(pen, new PointF(MarginDistance, height - MarginDistance), new PointF(width - MarginDistance, height - MarginDistance));
                if (axis.CalibrationsVisible)
                {
                    bool legend = (axis.LegendLables.Count == axis.Calibrations.Count);
                    List<float> Calibrations = axis.Calibrations.Select(t => ((t - axis.MinValue) * axis.ValueRate) + MarginDistance).ToList();
                    for (int i = 1; i < axis.Calibrations.Count; i++)
                    {
                        bak_pen.DashStyle = DashStyle.Dash;
                        bak_pen.DashPattern = new float[] { 5, 5 };
                        float distance = Calibrations[i] - Calibrations[i - 1];
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
                                SizeF waterSize = g.MeasureString(axis.LegendLables[i], Font);
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
                                    x = Calibrations[i];
                                }
                                PointF p = new PointF(x, height - MarginDistance + 2);
                                PointF p2 = new PointF(x, MarginDistance - waterSize.Height - 2);
                                if (i == axis.Calibrations.Count - 1)
                                {
                                    ///最后刻度标注出实际轴线
                                    bak_pen.Color = Color.Red;
                                    float xx = m_TotalFrameCnt * xAxis.ValueRate + MarginDistance;
                                    p2.X = xx + waterSize.Width;
                                    g.DrawLine(bak_pen, new PointF(xx, height - MarginDistance), new PointF(xx, MarginDistance));
                                    //g.DrawString(DateTimeLegends[i], Font, Brushes.Blue, p2, sf);
                                }
                                else
                                {
                                    g.DrawString(DateTimeLegends[i], Font, Brushes.Black, p2, sf);
                                }
                                g.DrawString(axis.LegendLables[i], Font, Brushes.Black, p, sf);
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
        /// <summary>
        ///  绘制Y坐标轴
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <param name="MarginDistance">偏移量</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        public void DrawYAxis(Graphics g, int MarginDistance, int width, int height)
        {
            Axis axis = yAxis;
            if (axis.AxisVisible)
            {
                Pen pen = new Pen(axis.BackColor, 1);
                Pen bak_pen = new Pen(axis.ForeColor);
                g.DrawLine(pen, new PointF(MarginDistance, height - MarginDistance), new PointF(MarginDistance, MarginDistance));
                if (axis.CalibrationsVisible)
                {
                    bool legend = (axis.LegendLables.Count == axis.Calibrations.Count);
                    List<float> Calibrations = axis.Calibrations.Select(t => (height - (t - axis.MinValue) * axis.ValueRate) - MarginDistance).ToList();
                    for (int i = 1; i < axis.Calibrations.Count; i++)
                    {
                        bak_pen.DashStyle = DashStyle.Dash;
                        bak_pen.DashPattern = new float[] { 5, 5 };
                        g.DrawLine(bak_pen, new PointF(MarginDistance, Calibrations[i]), new PointF(width - MarginDistance, Calibrations[i]));
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
                                SizeF waterSize = g.MeasureString(axis.LegendLables[i], Font);
                                StringFormat sf = new StringFormat(StringFormatFlags.DisplayFormatControl) { Alignment = StringAlignment.Center };
                                int x = 12; //(int)(MarginDistance - waterSize.Width);
                                float y = Calibrations[i] - waterSize.Height / 2;
                                PointF p = new PointF(x, y);
                                g.DrawString(axis.LegendLables[i], Font, Brushes.Black, p, sf);
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
        private int m_xoffSet = 0;
        /// <summary>
        /// 总帧数
        /// </summary>
        private int m_TotalFrameCnt = 0;
        private List<string> m_Datetimelegends = new List<string>();
        /// <summary>
        /// 记录开始时间
        /// </summary>
        private DateTime m_startTime = DateTime.Now;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="TotalFrameCnt"></param>
        public void Initialize(SuperPointF[] dataSource, int TotalFrameCnt, DateTime startTime)
        {
            this.m_TotalFrameCnt = TotalFrameCnt;
            this.m_DataSource = dataSource;
            int num = TotalFrameCnt * 30 / 3600;
            this.xAxis.Interval = ((TotalFrameCnt * 30 % 3600 == 0) ? num : (num + 1));
            this.yAxis.Interval = 5;
            this.xAxis.MaxValue = (float)(this.xAxis.Interval * 60 * 2);
            this.xAxis.Displacement = (float)(this.xAxis.Interval * this.xDistanceProportion);
            this.xAxis.LegendLables.Clear();
            this.m_Datetimelegends.Clear();
            this.xAxis.LegendLables.Add("Hours");
            for (int i = 1; i <= this.xAxis.Interval; i++)
            {
                this.xAxis.LegendLables.Add(i.ToString());
            }
            this.m_Datetimelegends.Add(startTime.ToString("HH:mm"));
            for (int j = 1; j < this.xAxis.Interval; j++)
            {
                this.m_Datetimelegends.Add(startTime.AddHours((double)j).ToString("HH:mm"));
            }
            this.m_Datetimelegends.Add(startTime.AddSeconds((double)(this.m_TotalFrameCnt * 30)).ToString("HH:mm"));
            this.m_startTime = startTime;
        }
        /// <summary>
        /// 是否显示开关灯标签
        /// </summary>
        public bool ShowLightOnAndOff = false;
        /// <summary>
        /// 开灯的标记位置
        /// </summary>
        private float m_lightOnLocation = -1;
        /// <summary>
        /// 关灯的标记位置
        /// </summary>
        private float m_lightOffLocation = -1;
        /// <summary>
        /// 设置开始结束时间
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void SetLightONOFF(DateTime start, DateTime end)
        {
            if (start.Year != 1)
            {
                this.m_lightOffLocation = (float)((start - this.m_startTime).TotalSeconds / 30.0 * (double)this.xAxis.ValueRate) + (float)this.m_docksize;
            }
            else
            {
                this.m_lightOffLocation = -1f;
            }
            if (end.Year != 1)
            {
                this.m_lightOnLocation = (float)((end - this.m_startTime).TotalSeconds / 30.0 * (double)this.xAxis.ValueRate) + (float)this.m_docksize;
            }
            else
            {
                this.m_lightOnLocation = -1f;
            }
            if (this.ShowLightOnAndOff)
            {
                this.Refresh();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Refresh()
        {
            this.m_RefreshRect = false;
            if (this.m_StartSubPiont != null)
            {
                this.m_StartSubPiont.XValue = (float)this.m_CurrentFrameNo;
            }
            this.m_SourceImage = this.getMap();

            this.panel1.Invalidate();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSource"></param>
        public void Invalidate(SuperPointF[] dataSource, bool next = false)
        {
            DateTime dt = DateTime.Now;
            this.m_RefreshRect = false;
            this.m_DataSource = dataSource;
            this.m_SourceImage = this.getMap();
            this.m_StartSubPiont.XValue = next ? (float)this.m_CurrentFrameNo++ : (float)this.m_CurrentFrameNo;
            this.ReSetStartPoint();
            Console.WriteLine(string.Format("【{1}】图谱生成耗时：{0}ms", (DateTime.Now - dt).TotalMilliseconds, DateTime.Now));
            this.panel1.Invalidate();
        }
        /// <summary>
        /// 设置y轴显示标签
        /// </summary>
        public List<string> YLegends
        {
            set
            {
                yAxis.MinValue = 0;
                yAxis.MaxValue = value.Count - 1;
                yAxis.Interval = value.Count - 1;
                yAxis.LegendLables = value;
            }
        }
        /// <summary>
        /// 多次小睡标记
        /// </summary>
        public List<IMarker> MultipleSleepMarks = new List<IMarker>();
        public bool ChangeVLineWith = true;
        /// <summary>
        /// x轴一个刻度所占像素个数（30s一帧，一帧一个像素点，那么一个小时就是120个像素点）
        /// </summary>
        public int xDistanceProportion = 120;
        /// <summary>
        /// y轴一个刻度所占像素个数
        /// </summary>
        public int yDistanceProportion = 50;
        /// <summary>
        /// 采样周期
        /// </summary>
        public int TimeSpan = 30;
        /// <summary>
        /// 当前帧序号
        /// </summary>
        private int m_CurrentFrameNo = 1;
        /// <summary>
        /// 当前帧序号
        /// </summary>
        public int CurrentFrameNo
        {
            set
            {
                setHsValue(value);
            }
        }
        private void setHsValue(int value)
        {
            if (this.InvokeRequired)
            {
                while (!this.IsHandleCreated)
                {
                    if (this.Disposing || this.IsDisposed)
                    {
                        return;
                    }
                }
                this.Invoke(new MethodInvoker(() =>
                {
                    setHsValue(value);
                }));
            }
            else
            {
                if (value <= 0)
                {
                    value = 1;
                }
                else if (value > this.m_TotalFrameCnt)
                {
                    value = this.m_TotalFrameCnt;
                }
                if (this.m_CurrentFrameNo != value)
                {
                    this.m_CurrentFrameNo = value;
                    if (this.Width == 0 || this.Height == 0)
                    {
                        return;
                    }
                    this.ReSetStartPoint();
                    Console.WriteLine("帧序号变化更新");
                    this.panel1.Invalidate();
                }
            }
        }
        /// <summary>
        /// 当前帧序号发生改变事件
        /// </summary>
        public delegate void CurrentFrameChangedDelegate(int frameCnt);
        /// <summary>
        /// 当前帧序号发生改变事件
        /// </summary>
        public event CurrentFrameChangedDelegate CurrentFrameChangedHandler;

        /// <summary>
        /// 小睡标记发生变更时触发
        /// </summary>
        public delegate void SleepMarkChangedDelegate(List<IMarker> currents);
        /// <summary>
        /// 小睡标记发生变更时触发
        /// </summary>
        public event SleepMarkChangedDelegate SleepMarkChangedHandler;

        private void SleepMarkChanged(List<IMarker> currentMarks)
        {
            if (SleepMarkChangedHandler != null)
                SleepMarkChangedHandler.Invoke(currentMarks);
        }

        public delegate bool QueryScoreLockStatusDelegage();
        /// <summary>
        /// 获取评分状态时触发
        /// </summary>
        public event QueryScoreLockStatusDelegage QueryScoreLockStatusHappend;
        /// <summary>
        /// 锁定状态
        /// </summary>
        private bool ScoreLockStatus
        {
            get
            {
                if (QueryScoreLockStatusHappend != null)
                    return QueryScoreLockStatusHappend.Invoke();
                return false;
            }
        }
    }
}
