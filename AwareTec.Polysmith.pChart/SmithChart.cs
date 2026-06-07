using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AwareTec.Polysmith.pChart
{
    public partial class SmithChart : UserControl
    {
        private System.Timers.Timer m_Timer1 = null;
        /// <summary>
        /// 绘制趋势实体对象
        /// </summary>
        public DrawCurve SerilArea = null;
        public SmithChart()
        {
            InitializeComponent();
            SerilArea = new DrawCurve();
            this.Load += SmithChart_Load;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer
                 | ControlStyles.ResizeRedraw
                 | ControlStyles.Selectable
                 | ControlStyles.AllPaintingInWmPaint
                 | ControlStyles.UserPaint
                 | ControlStyles.SupportsTransparentBackColor,
            true);
            this.UpdateStyles();
            this.MouseDown += new MouseEventHandler(frmMain_MouseDown);
            this.MouseMove += new MouseEventHandler(frmMain_MouseMove);
            this.MouseUp += new MouseEventHandler(frmMain_MouseUp);
        }
        #region 绘制移动虚线框
        bool MouseIsDown = false;
        Rectangle MouseRect = Rectangle.Empty;
        private void frmMain_MouseUp(object sender, MouseEventArgs e)
        {
            this.Capture = false;
            Cursor.Clip = Rectangle.Empty;
            MouseIsDown = false;
            DrawRectangle();
            CurveItem find = SerilArea.Find(MouseRect.Width == 0 ? e.Location : new Point(MouseRect.X, MouseRect.Y));
            if (find != null)
            {
                if (!(e.Button == MouseButtons.Left && MouseRect.Width == 0) && find.ClientRectangleChecked)
                {
                    if (ChannelViewPopupHandler != null)
                    {
                        if (MouseRect.Width != 0)
                        {
                            List<PointF> datas = new List<PointF>();
                            ///获取裁剪区域的坐标点
                            for (int i = 0; i < find.ClientRectanglePoints.Count; i++)
                            {
                                if (MouseRect.X <= find.ClientRectanglePoints[i].X && MouseRect.Right >= find.ClientRectanglePoints[i].X)
                                {
                                    datas.Add(find.ClientRectanglePoints[i]);
                                }
                            }
                            find.ClientRectanglePoints = datas;
                        }
                        ChannelViewPopupHandler.Invoke(find, e.Button);
                    }
                }
                else if (find.HeadRectangleChecked)
                {
                    if (ChannelHeadPopupHandler != null)
                    {
                        ChannelHeadPopupHandler.BeginInvoke(find, e.Button, new AsyncCallback(UpdateHeadIAsyncResult), find);
                    }
                }
            }
            MouseRect = Rectangle.Empty;
            m_freshOk = true;
        }
        private Point bak_point = new Point(0, 0);
        private void frmMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseIsDown)
                ResizeToRectangle(e.Location);
            if (SerilArea.ClientRectangle.Contains(e.X, e.Y))
            {
                Cursor = Cursors.Cross;
                //Point p = this.PointToClient(e.Location);
                //if (bak_point.X != p.X && bak_point.Y != p.Y)
                //{

                //    ControlPaint.DrawReversibleLine(this.PointToScreen(new Point(bak_point.X, SerilArea.ClientRectangle.Y)), this.PointToScreen(new Point(bak_point.X, SerilArea.ClientRectangle.Height + SerilArea.ClientRectangle.Y)), Color.Blue);
                //    ControlPaint.DrawReversibleLine(this.PointToScreen(new Point(p.X, SerilArea.ClientRectangle.Y)), this.PointToScreen(new Point(p.X, SerilArea.ClientRectangle.Height + SerilArea.ClientRectangle.Y)), Color.Blue);
                //    bak_point = p;
                //}
            }
            else if (SerilArea.HeadRectangle.Contains(e.X, e.Y))
            {
                Cursor = Cursors.Hand;
            }
            else
            {
                Cursor = Cursors.Default;
            }
        }
        void frmMain_MouseDown(object sender, MouseEventArgs e)
        {
            m_freshOk = false;
            MouseIsDown = true;
            DrawStart(e.Location);
        }
        private void ResizeToRectangle(Point p)
        {
            DrawRectangle();
            Point start = new Point(MouseRect.X, MouseRect.Y);
            p = SerilArea.CheckEndPoint(ref start, p);
            MouseRect.Y = start.Y;
            MouseRect.Width = p.X - MouseRect.Left;
            MouseRect.Height = p.Y - MouseRect.Top;
            DrawRectangle();
        }
        private void DrawRectangle()
        {
            Rectangle rect = this.RectangleToScreen(MouseRect);
            ControlPaint.FillReversibleRectangle(rect, Color.Blue);
        }
        private void DrawStart(Point StartPoint)
        {
            this.Capture = true;
            Cursor.Clip = this.RectangleToScreen(new Rectangle(0, 0, ClientSize.Width, ClientSize.Height));
            MouseRect = new Rectangle(StartPoint.X, StartPoint.Y, 0, 0);
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
        private bool start = true;
        private void SmithChart_Load(object sender, EventArgs e)
        {
            this.SizeChanged += SmithChart_SizeChanged;
            this.Paint += SmithChart_Paint;
            m_Timer1 = new System.Timers.Timer();
            m_Timer1.Interval = 1000;
            m_Timer1.Elapsed += m_Timer1_Elapsed;
            m_Timer1.Enabled = true;
            OnSizeChanged(null);
        }
        #region 趋势图绘制
        /// <summary>
        /// paint绘制是否完成
        /// </summary>
        private bool m_freshOk = true;
        private void SmithChart_Paint(object sender, PaintEventArgs e)
        {
            m_freshOk = false;
            Image map = SerilArea.GetMap();
            e.Graphics.DrawImage(map, 0, 0);
            map.Dispose();
            start = true;
            e.Dispose();
            m_freshOk = true;

        }

        private void SmithChart_SizeChanged(object sender, EventArgs e)
        {
            start = false;
            if (this.Height != 0 && this.Width != 0)
            {
                SerilArea.Height = this.Height;
                SerilArea.Width = this.Width;
            }
            this.Invalidate(new Rectangle(0, 0, this.Width, this.Height));
        }

        private void m_Timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (m_IsRealTimeCurve)
            {
                if (m_freshOk)
                {
                    m_Timer1.Interval = 1000;
                    if (start)
                    {
                        if (this.Height != 0 && this.Width != 0)
                        {
                            SerilArea.Height = this.Height;
                            SerilArea.Width = this.Width;
                        }
                        if (!m_IsStop)
                        {
                            this.Invalidate(new Rectangle(0, 0, this.Width, this.Height));
                            Application.DoEvents();
                        }
                    }
                }
                else
                {
                    m_Timer1.Interval = 50;
                }
            }
        }
        #endregion
        #region 鼠标左右键弹出事件
        /// <summary>
        /// 鼠标键点击绘制区域触发委托
        /// </summary>
        /// <param name="channel">通道</param>
        /// <param name="buttons">左键？右键</param>
        public delegate void ChannelViewPopupDelegate(CurveItem channel, MouseButtons buttons);
        /// <summary>
        /// 鼠标键点击绘制区域触发事件
        /// </summary>
        public event ChannelViewPopupDelegate ChannelViewPopupHandler;
        /// <summary>
        /// 处理绘制区域弹出事件处理完毕后的数据
        /// </summary>
        /// <param name="result"></param>
        private void UpdateViewIAsyncResult(IAsyncResult result)
        {
            CurveItem item = (CurveItem)result.AsyncState;
        }
        /// <summary>
        /// 鼠标键点击头部触发委托
        /// </summary>
        /// <param name="channel">通道</param>
        /// <param name="buttons">左键？右键</param>
        public delegate void ChannelHeadPopupDelegate(CurveItem channel, MouseButtons buttons);
        /// <summary>
        /// 鼠标键点击头部触发事件
        /// </summary>
        public event ChannelHeadPopupDelegate ChannelHeadPopupHandler;
        /// <summary>
        /// 处理头部弹出事件处理完毕后的数据
        /// </summary>
        /// <param name="result"></param>
        private void UpdateHeadIAsyncResult(IAsyncResult result)
        {
            CurveItem item = (CurveItem)result.AsyncState;
        }
        #endregion
        private bool m_IsRealTimeCurve = true;
        /// <summary>
        /// 是否是实时趋势图
        /// </summary>
        public bool IsRealTimeCurve
        {
            set
            {
                m_IsRealTimeCurve = value;
                if (value)
                {
                    IsTimeLineVisible = false;
                }
            }
            get
            {
                return m_IsRealTimeCurve;
            }
        }
        private bool m_IsStop = false;
        /// <summary>
        /// 停止绘制
        /// </summary>
        public bool IsStop
        {
            set
            {
                m_IsStop = value;
            }
            get
            {
                return m_IsStop;
            }
        }
        /// <summary>
        /// 时间轴线是否显示
        /// </summary>
        public bool IsTimeLineVisible = false;
        /// <summary>
        /// 释放资源
        /// </summary>
        public new void Disposed()
        {
            start = false;
            m_Timer1.Enabled = false;
            this.Dispose();
        }
    }
}
