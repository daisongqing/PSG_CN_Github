using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.pChart
{
    public partial class Chart : UserControl
    {
        #region 属性
        private Color m_channelColor = Color.FromArgb(243, 245, 255);
        private List<HotKey> m_HotKeys = new List<HotKey>();
        private System.Timers.Timer m_Timer1 = null;
        /// <summary>
        /// 绘图区域高
        /// </summary>
        private int m_Height = 800;
        /// <summary>
        /// 绘图区域宽
        /// </summary>
        private int m_Width = 600;
        /// <summary>
        /// 当前帧序号
        /// </summary>
        private int m_FrameCnt = 1;
        private int m_TotalFrameCnt = 0;
        /// <summary>
        /// 一帧表示的时间间隔
        /// </summary>
        private int m_OneFrameSpanTime = 30;

        /// <summary>
        /// 通道上部的空白区域（针对所有通道设置的右键快捷菜单）
        /// </summary>
        public Rectangle ChannelUpperEmptyArea
        {
            get
            {
                Size size = new Size(panel1.Width,
                                     panel3.Height);
                return new Rectangle(panel3.Location, size);
            }
        }
        #endregion
        /// <summary>
        /// 构造函数(默认主题: 单机版主题)
        /// </summary>
        public Chart()
        {
            InitializeComponent();
            Channel_InRightClick += Panel1_RightClick;
            HideChannel.DropDown.Closing += DropDown_Closing;
            HideWave.DropDown.Closing += DropDown_Closing;
            HideWave.DropDownOpening += HideWave_DropDownOpening;
            HideChannel.DropDownOpening += HideChannel_DropDownOpening;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer
                 | ControlStyles.ResizeRedraw
                 | ControlStyles.Selectable
                 | ControlStyles.AllPaintingInWmPaint
                 | ControlStyles.UserPaint
                 | ControlStyles.SupportsTransparentBackColor
                 | ControlStyles.Opaque,
            true);
            ////采用双缓冲技术的控件必需的设置
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();
            this.Load += ChartCaptains_Load;
        }

        /// <summary>
        /// 构造函数(带颜色主题)
        /// </summary>
        public Chart(Color channelRegionColor,
                     Color topBarColor)
        {
            InitializeComponent();
            SkinControl(channelRegionColor, topBarColor);
            Channel_InRightClick += Panel1_RightClick;
            HideChannel.DropDown.Closing += DropDown_Closing;
            HideWave.DropDown.Closing += DropDown_Closing;
            HideWave.DropDownOpening += HideWave_DropDownOpening;
            HideChannel.DropDownOpening += HideChannel_DropDownOpening;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer
                 | ControlStyles.ResizeRedraw
                 | ControlStyles.Selectable
                 | ControlStyles.AllPaintingInWmPaint
                 | ControlStyles.UserPaint
                 | ControlStyles.SupportsTransparentBackColor
                 | ControlStyles.Opaque,
            true);
            ////采用双缓冲技术的控件必需的设置
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();
            this.Load += ChartCaptains_Load;
        }

        private void SkinControl(Color channelRegionColor,
                                 Color topBarColor)
        {
            panel1.BackColor = m_channelColor = channelRegionColor;
            panel3.BackColor = topBarColor;
        }

        private bool draw = true;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0112 && m.WParam == (IntPtr)0xF012) //响应到窗体移动事件
                draw = false;
            else if (m.Msg == 0x000F && (!draw))  //绘图事件
            {
                return;
            }
            else if (m.Msg == 0x0014) // 禁掉清除背景消息
            {
                return;
            }
            base.WndProc(ref m);
        }
        /// <summary>
        /// 防止拖拽的时候出现卡顿
        /// </summary>
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle = 0x02000000;////用双缓冲绘制窗口的所有子控件
        //        return cp;
        //    }
        //}
        /// <summary>
        /// 窗体加载时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChartCaptains_Load(object sender, EventArgs e)
        {
            this.Resize += Chart_Resize;
            this.panel3.Paint += panel3_Paint;
            this.panel1.Paint += panel1_Paint;
            //this.panel1.MouseLeave += panel1_MouseLeave;
            this.panel1.MouseDown += panel1_MouseDown;
            this.panel1.MouseDoubleClick += Panel1_MouseDoubleClick;
            this.panel1.MouseMove += panel1_MouseMove;
            this.panel1.MouseUp += panel1_MouseUp;
            master_CurveArea.UserSelected = true;
            master_CurveArea.getCalibrationMarksHandle += bak_CurveArea_getCalibrationMarksHandle;
            master_CurveArea.getGlobalMarkersHandle += bak_CurveArea_getGlobalMarkersHandle;
            master_CurveArea.getAllItemsHandle += bak_CurveArea_getAllItemsHandle;
            master_CurveArea.getSleepPoseHandle += bak_CurveArea_getSleepPoseHandle;
            master_CurveArea.getSleepStageHandle += bak_CurveArea_getSleepStageHandle;
            master_CurveArea.ChannelViewPopupHandler += bak_CurveArea_ChannelViewPopupHandler;
            master_CurveArea.SelectRectangleKeyDownHandler += master_CurveArea_SelectRectangleKeyDownHandler;
            master_CurveArea.CaptainValueChangeHandler += bak_CurveArea_CaptainValueChangeHandler;
            master_CurveArea.MarkerMouseDoubleClick += bak_CurveArea_MarkerMouseDoubleClick;
            master_CurveArea.MarkerDeleteHandler += RemoveMarks;
            master_CurveArea.SelectedHappenHandler += master_CurveArea_SelectedHappenHandler;
            master_CurveArea.getHotKeysHandler += master_CurveArea_getHotKeysHandler;
            master_CurveArea.MarkTypeChanged += master_CurveArea_MarkTypeChanged;
            master_CurveArea.VideoStatusChanged += Master_CurveArea_VideoStatusChanged;
            master_CurveArea.Belong = 0;
            master_CurveArea.isSplitscreen = false;
            master_CurveArea.getCurveItemHandle += master_CurveArea_getCurveItemHandle;
            master_CurveArea.ReadChannelDataHandler += master_CurveArea_ReadChannelDataHandler;
            master_CurveArea.ReadChannelDataExHandler += master_CurveArea_ReadChannelDataExHandler;
            master_CurveArea.getMulSleepHandler += Master_CurveArea_getMulSleepHandler;
            master_CurveArea.JumpHappenHandler += Bak_curveArea_JumpHappenHandler;
            master_CurveArea.MouseUpChangeFrameHandle += Master_CurveArea_MouseMoveUpHandler;
            bak_curveArea.SelectRectangleKeyDownHandler += master_CurveArea_SelectRectangleKeyDownHandler;
            bak_curveArea.getCalibrationMarksHandle += bak_CurveArea_getCalibrationMarksHandle;
            bak_curveArea.getGlobalMarkersHandle += bak_CurveArea_getGlobalMarkersHandle;
            bak_curveArea.getAllItemsHandle += bak_CurveArea_getAllItemsHandle;
            bak_curveArea.getSleepPoseHandle += bak_CurveArea_getSleepPoseHandle;
            bak_curveArea.getSleepStageHandle += bak_CurveArea_getSleepStageHandle;
            bak_curveArea.ChannelViewPopupHandler += bak_CurveArea_ChannelViewPopupHandler;
            bak_curveArea.CaptainValueChangeHandler += bak_CurveArea_CaptainValueChangeHandler;
            bak_curveArea.MarkerMouseDoubleClick += bak_CurveArea_MarkerMouseDoubleClick;
            bak_curveArea.MarkerDeleteHandler += RemoveMarks;
            bak_curveArea.SelectedHappenHandler += master_CurveArea_SelectedHappenHandler;
            bak_curveArea.VideoStatusChanged += Master_CurveArea_VideoStatusChanged;
            bak_curveArea.Belong = 1;
            bak_curveArea.isSplitscreen = false;
            bak_curveArea.getCurveItemHandle += master_CurveArea_getCurveItemHandle;
            bak_curveArea.getHotKeysHandler += master_CurveArea_getHotKeysHandler;
            bak_curveArea.MarkTypeChanged += master_CurveArea_MarkTypeChanged;
            bak_curveArea.ReadChannelDataHandler += master_CurveArea_ReadChannelDataHandler;
            bak_curveArea.ReadChannelDataExHandler += master_CurveArea_ReadChannelDataExHandler;
            bak_curveArea.getMulSleepHandler += Master_CurveArea_getMulSleepHandler;
            bak_curveArea.JumpHappenHandler += Bak_curveArea_JumpHappenHandler;
            bak_curveArea.MouseUpChangeFrameHandle += Master_CurveArea_MouseMoveUpHandler;
            if (m_isRealDataCurve)
            {
                m_Timer1 = new System.Timers.Timer()
                {
                    Interval = 100,
                    Enabled = true,
                    AutoReset = false
                };
                m_Timer1.Elapsed += m_Timer1_Elapsed;
            }
            CurveItemChanged(panel1);
        }

        private void Master_CurveArea_VideoStatusChanged(bool play, DateTime currentTime)
        {
            videoStatusChange(play, currentTime);
        }

        private List<IMarker> Master_CurveArea_getMulSleepHandler()
        {
            return m_mulSleepMarks;
        }

        private bool master_CurveArea_ReadChannelDataHandler(DateTime start, DateTime end, List<int> channelIDs)
        {
            return ReadChannelData(start, end, channelIDs);
        }
        private bool master_CurveArea_ReadChannelDataExHandler(DateTime start, DateTime end, List<CurveItem> curveItems)
        {
            return ReadChannelData(start, end, curveItems);
        }
        private curveArea.CaptainInfoUion m_Captaininfo = null;
        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            if (m_Captaininfo == null)
                return;
            using (Graphics g = e.Graphics)
            {
                int right = panel1.Right;
                //DrawTitile(g, right, panel3.Width, panel3.Height, m_Captaininfo.ViewStartTime, m_Captaininfo.ViewEndTime, m_Captaininfo.CurrentFrameNo);
                DrawTitile(g, m_Captaininfo);
            }
        }
        #region 数据交互实现
        private bool master_CurveArea_MarkTypeChanged(IMarker[] marks, IMarker.MarkType NewMarkTyp, string NewMarkName)
        {
            if (MarkTypeChanged != null)
            {
                return MarkTypeChanged.Invoke(marks, NewMarkTyp, NewMarkName);
            }
            return false;
        }
        private List<HotKey> master_CurveArea_getHotKeysHandler()
        {
            return m_HotKeys;
        }

        private CurveItem master_CurveArea_getCurveItemHandle(int ID)
        {
            return FindCurve(ID);
        }
        public delegate void SelectedHappenDelegate(int belong, int TimeSpanLine);
        /// <summary>
        /// 选中事件发生时
        /// </summary>
        public event SelectedHappenDelegate SelectedHappenHandler;

        public delegate void ShowOrHideChannelDelegate(CurveItem curveItem);

        /// <summary>
        /// 右键点击通道 修改通道可见性时发生
        /// </summary>
        public event ShowOrHideChannelDelegate ShowOrHideChannelHandler;

        private void master_CurveArea_SelectedHappenHandler(int belong)
        {
            int timespan = 0;
            if (belong == 0)
            {
                bak_curveArea.UserSelected = false;
                timespan = master_CurveArea.BaseTimeLineSpan;
            }
            else
            {
                master_CurveArea.UserSelected = false;
                timespan = bak_curveArea.BaseTimeLineSpan;
            }
            isPartyRectFresh = false;
            panel1.Invalidate();
            if (SelectedHappenHandler != null)
                SelectedHappenHandler.Invoke(belong, timespan);
        }

        void bak_CurveArea_CaptainValueChangeHandler(curveArea.CaptainInfoUion info)
        {
            m_Captaininfo = info;
            //int right = panel1.Right;
            //panel3.Invalidate(new Region(new Rectangle(right, 0, panel3.Width - right, panel3.Height)));
            DateTime dt = DateTime.Now;
            using (Graphics g = panel3.CreateGraphics())
            {
                int right = panel1.Right;
                g.Clip = new Region(new Rectangle(right, 0, panel3.Width - right, panel3.Height));
                g.Clear(panel3.BackColor);
                if (info.showTyp == 1)
                {
                    DrawTitile(g, right, panel3.Width, panel3.Height, info.CurrentTime.ToString("HH:mm:ss.f"), info.NowLocationX + panel1.Right, info.ViewStartTime, info.ViewEndTime, info.CurrentFrameNo);
                }
                else if (info.showTyp == 2)
                {
                    TimeSpan ts = (info.RectEndTime - info.RectStartTime);
                    DrawTitile(g, panel3.Width, panel3.Height, string.Format("{0} <  {1:f1}s  > {2}{3}", info.RectStartTime.ToString("HH:mm:ss.f"), Math.Abs(ts.TotalMilliseconds) / 1000.0, info.RectEndTime.ToString("HH:mm:ss.f"), info.Comments));
                }
                else
                {
                    DrawTitile(g, info);
                    //DrawTitile(g, right, panel3.Width, panel3.Height, info.ViewStartTime, info.ViewEndTime, info.CurrentFrameNo);
                }
                Console.WriteLine(string.Format("*******标题区域发生一次重绘，耗时[{0}ms]*********", (DateTime.Now - dt).TotalMilliseconds));
            }
        }
        bool bak_CurveArea_MarkerMouseDoubleClick(string channelID, IMarker mark, MouseEventArgs e)
        {
            if (MarkerMouseDoubleClick != null)
            {
                return MarkerMouseDoubleClick.Invoke(channelID, mark, e);
            }
            return false;
        }
        void bak_CurveArea_ChannelViewPopupHandler(CurveItem channel, MouseButtons buttons)
        {
            if (ChannelViewPopupHandler != null)
                ChannelViewPopupHandler.Invoke(channel, buttons);
        }
        bool master_CurveArea_SelectRectangleKeyDownHandler(string ChannelID, CurveItem mark)
        {
            if (SelectRectangleKeyDownHandler != null)
            {
                return SelectRectangleKeyDownHandler.Invoke(ChannelID, mark);
            }
            return false;
        }
        string[] bak_CurveArea_getSleepStageHandle()
        {
            return SleepStage;
        }

        int[] bak_CurveArea_getSleepPoseHandle()
        {
            return SleepPos;
        }
        private object m_lockobj = new object();
        System.Collections.Generic.List<CurveItem> bak_CurveArea_getAllItemsHandle(int belong)
        {
            lock (m_lockobj)
                return m_curveItems.Where(t => t.belong == belong).ToList();
        }

        System.Collections.Generic.List<IMarker> bak_CurveArea_getGlobalMarkersHandle()
        {
            return pChartMarks;
        }

        System.Collections.Generic.List<IMarker> bak_CurveArea_getCalibrationMarksHandle()
        {
            return CalibrationMarks;
        }
        /// <summary>
        /// 画出框选区域的范围值
        /// </summary>
        /// <param name="g"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="value"></param>
        private void DrawTitile(Graphics g, int width, int height, string value)
        {
            using (Font strFont = new System.Drawing.Font("华文中宋", 12F, FontStyle.Bold))
            {
                SizeF cwaterSize = g.MeasureString(value, strFont);
                PointF valueP = new PointF(width / 2 - cwaterSize.Width / 2, height - cwaterSize.Height - 1);
                g.DrawString(value, strFont, new SolidBrush(Color.FromArgb(27, 211, 171)), valueP);
            }
        }
        /// <summary>
        /// 绘出光标移动时的显示时间轴
        /// </summary>
        /// <param name="g"></param>
        /// <param name="offset"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="value"></param>
        /// <param name="x"></param>
        private void DrawTitile(Graphics g, int offset, int width, int height, string value, float x, DateTime st, DateTime et, int framecnt)
        {
            int framespan = m_OneFrameSpanTime;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            using (Font strFont = new System.Drawing.Font("华文中宋", 12F, FontStyle.Bold))
            {
                SizeF cwaterSize = g.MeasureString(value, strFont);
                PointF valueP = new PointF(x - cwaterSize.Width / 2, height - cwaterSize.Height - 1);
                if (valueP.X + cwaterSize.Width > width)
                    valueP.X = width - cwaterSize.Width;
                else if (valueP.X - offset < 0)
                {
                    valueP.X = offset;
                }
                g.DrawString(value, strFont, new SolidBrush(Color.FromArgb(27, 211, 171)), valueP);
                string strStart = string.Format("{0} ({1} / {2})", st.ToString("HH:mm:ss"), framecnt, m_TotalFrameCnt);
                string strEnd = et.ToString("HH:mm:ss");
                SizeF swaterSize = g.MeasureString(strStart, strFont);
                PointF startP = new PointF(offset, height - swaterSize.Height - 1);
                if (valueP.X > startP.X + swaterSize.Width)
                {
                    g.DrawString(strStart, strFont, new SolidBrush(Color.FromArgb(27, 211, 171)), startP);
                }
                SizeF ewaterSize = g.MeasureString(strEnd, strFont);
                PointF endP = new PointF(width - ewaterSize.Width - 1, height - ewaterSize.Height - 1);
                if (valueP.X + cwaterSize.Width < endP.X)
                {
                    g.DrawString(strEnd, strFont, new SolidBrush(Color.FromArgb(27, 211, 171)), endP);
                }
            }
        }
        /// <summary>
        /// 绘出开始结束时间刻度
        /// </summary>
        /// <param name="g"></param>
        /// <param name="offset"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private void DrawTitile(Graphics g, int offset, int width, int height, DateTime st, DateTime et, int framecnt)
        {
            using (Font strFont = new System.Drawing.Font("华文中宋", 12F, FontStyle.Bold))
            {
                string strStart = string.Format("{0} ({1} / {2})", st.ToString("HH:mm:ss"), framecnt, m_TotalFrameCnt);
                string strEnd = et.ToString("HH:mm:ss");
                SizeF swaterSize = g.MeasureString(strStart, strFont);
                PointF startP = new PointF(offset, height - swaterSize.Height - 1);
                g.DrawString(strStart, strFont, new SolidBrush(Color.FromArgb(27, 211, 171)), startP);
                SizeF ewaterSize = g.MeasureString(strEnd, strFont);
                PointF endP = new PointF(width - ewaterSize.Width - 1, height - ewaterSize.Height - 1);
                g.DrawString(strEnd, strFont, new SolidBrush(Color.FromArgb(27, 211, 171)), endP);
            }
        }

        private void DrawTitile(Graphics g, curveArea.CaptainInfoUion info)
        {
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            using (Font timeFont = new Font("华文中宋", 12F, FontStyle.Bold))
            {
                string strStart = string.Format("{0} ({1} / {2})", info.ViewStartTime.ToString("HH:mm:ss"), info.CurrentFrameNo, m_TotalFrameCnt);
                string strEnd = info.ViewEndTime.ToString("HH:mm:ss");
                SizeF startSize = g.MeasureString(strStart, timeFont);
                SizeF endSize = g.MeasureString(strEnd, timeFont);
                PointF startP = new PointF(panel1.Right, panel3.Height - startSize.Height - 1);
                PointF endP = new PointF(panel3.Width - endSize.Width - 1, panel3.Height - endSize.Height - 1);

                DrawingText startTime = new DrawingText(g, timeFont, new SolidBrush(Color.FromArgb(27, 211, 171)), startP, strStart);
                DrawingText endTime = new DrawingText(g, timeFont, new SolidBrush(Color.FromArgb(27, 211, 171)), endP, strEnd);

                const float FONT_SIZE = 9;
                if (info != null)
                {
                    #region 时基小于等于30
                    if (BaseTimeLineSpan <= 30)
                    {
                        float fontSize = FONT_SIZE;
                        getFontSize(panel3.Width, panel3.Height, ref fontSize);
                        DrawingText.TextRegion tr = PaintSleepStage(g, info, info.CurrentFrameNo, fontSize);
                        if (tr != null)
                        {
                            if (!tr.isCoincidedWith(startTime.Size))
                                startTime.Draw();

                            if (!tr.isCoincidedWith(endTime.Size))
                                endTime.Draw();
                        }
                        else
                        {
                            startTime.Draw();
                            endTime.Draw();
                        }
                    }
                    #endregion

                    #region 时基大于30
                    else
                    {
                        int len = BaseTimeLineSpan / 30;
                        int frameCnt = info.CurrentFrameNo;
                        int fontWidth = panel3.Width / len;
                        float fontSize = FONT_SIZE;
                        getFontSize(fontWidth, panel3.Height, ref fontSize);

                        bool startIsCoincideWith = false;
                        bool endIsCoincideWith = false;
                        for (int i = 0; i < len; i++)
                        {
                            if (frameCnt > SleepStage.Length)
                                break;
                            DrawingText.TextRegion tr = PaintSleepStage(g, info, frameCnt, fontSize);
                            if (tr != null)
                            {
                                if (tr.isCoincidedWith(startTime.Size) && !startIsCoincideWith)
                                    startIsCoincideWith = true;
                                if (tr.isCoincidedWith(endTime.Size) && !endIsCoincideWith)
                                    endIsCoincideWith = true;
                            }
                            frameCnt++;
                        }
                        if (!startIsCoincideWith)
                            startTime.Draw();
                        if (!endIsCoincideWith)
                            endTime.Draw();
                    }
                    #endregion
                }
            }
        }

        /// <summary>
        /// 绘制轴显示睡眠分期 文字印记
        /// </summary>
        /// <param name="g"></param>
        /// <param name="frameCnt"></param>
        /// <returns></returns>
        private DrawingText.TextRegion PaintSleepStage(Graphics g, curveArea.CaptainInfoUion info, int frameCnt, float fontSize)
        {
            DrawingText rightDt = null;
            DrawingText leftDt = null;

            DateTime next = master_CurveArea.StartTime.AddMilliseconds(frameCnt * 30000);
            if (next > info.ViewStartTime && next < info.ViewEndTime)
            {
                string leftStr = frameCnt - 1 >= SleepStage.Length ? null : SleepStage[frameCnt - 1];
                string rightStr = frameCnt >= SleepStage.Length ? null : SleepStage[frameCnt];
                int x = (int)Math.Round((next - info.ViewStartTime).TotalMilliseconds * master_CurveArea.xAxis.ValueRate, 0);
                float lineX = x + panel1.Width;
                using (Pen pen = new Pen(new SolidBrush(Color.FromArgb(200, Color.Blue)), 1) { DashStyle = DashStyle.Dash })
                {
                    g.DrawLine(pen, new PointF(lineX, 0), new PointF(lineX, panel3.Height));
                }

                #region 睡眠分期在轴线左右标注
                using (Font labelFont = new Font("微软雅黑", fontSize))
                {
                    if ((!string.IsNullOrWhiteSpace(rightStr)) || (!string.IsNullOrWhiteSpace(leftStr)))
                    {
                        float standardWidth = rightStr == String.Empty ?
                                              g.MeasureString(leftStr, labelFont).Width :
                                              g.MeasureString(rightStr, labelFont).Width;
                        if (rightStr != String.Empty && leftStr != String.Empty)
                            standardWidth = g.MeasureString(rightStr, labelFont).Width > g.MeasureString(leftStr, labelFont).Width ?
                                            g.MeasureString(leftStr, labelFont).Width : g.MeasureString(rightStr, labelFont).Width;
                        float marginBetweenTextAndLine = standardWidth / 3;

                        if (!string.IsNullOrWhiteSpace(rightStr))
                        {
                            SizeF rightSf = g.MeasureString(rightStr, labelFont);
                            float rightX = lineX + marginBetweenTextAndLine;
                            float rightY = panel3.Height - rightSf.Height - 1;

                            rightDt = new DrawingText(g,
                                                    labelFont,
                                                    new SolidBrush(Color.Blue),
                                                    new PointF(rightX, rightY),
                                                    rightStr);
                            rightDt.Draw();
                        }
                        if (!string.IsNullOrWhiteSpace(leftStr))
                        {
                            SizeF leftSf = g.MeasureString(leftStr, labelFont);
                            float leftX = lineX - leftSf.Width - marginBetweenTextAndLine;
                            float leftY = panel3.Height - leftSf.Height - 1;

                            leftDt = new DrawingText(g,
                                                    labelFont,
                                                    new SolidBrush(Color.Blue),
                                                    new PointF(leftX, leftY),
                                                    leftStr);
                            leftDt.Draw();
                        }
                    }
                }
                #endregion
            }
            var strLabel = rightDt?.Size + leftDt?.Size;
            return strLabel;
        }

        /// <summary>
        /// 获取适宜的字体大小
        /// </summary>
        /// <param name="with"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private void getFontSize(int width, int height, ref float refontSize)
        {
            float fontSize = (height * 3 / 4);
            float fontSize2 = (float)((width * 3 / 4) * 0.5);
            if (fontSize > fontSize2)
                fontSize = fontSize2;
            refontSize = fontSize > refontSize ? refontSize : fontSize / 2.5f;
        }
        #endregion
        /// <summary>
        /// paint绘制是否完成
        /// </summary>
        private bool m_freshOk = true;
        /// <summary>
        /// 定时器任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_Timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            m_Timer1.Enabled = false;
            if (m_freshOk)
            {
                m_freshOk = false;
                m_Timer1.Interval = 100;
                if (!m_fnstop)
                    ChartAreaInvalidate(true);
                m_freshOk = true;
            }
            else
            {
                m_Timer1.Interval = 50;
            }
            m_Timer1.Enabled = true;
        }

        /// <summary>
        /// 窗体大小改变时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chart_Resize(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            m_Width = panel3.Width - panel1.Width;
            m_Height = panel1.Height;
            //m_DistanceBaseLine = m_Height;
            m_rectangle = new Rectangle(new Point(0, 0), new Size(m_Width, m_Height));
            if (panel1.Height > 0)
            {
                CurveItemChanged(panel1);
            }
            chartResize();
            isPartyRectFresh = false;
            panel1.Invalidate();
            Console.WriteLine(string.Format("*******Chart容器发生大小变化，耗时[{0}ms]*********", (DateTime.Now - dt).TotalMilliseconds));
        }
        #region 曲线部分效果
        /// <summary>
        /// 开始时间
        /// </summary>
        private DateTime m_StartTime = DateTime.Now;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mark"></param>
        /// <param name="e"></param>
        /// <returns>是否删除该项</returns>
        public delegate bool MouseDoubleClickDelegate(string channelID, IMarker mark, MouseEventArgs e);
        /// <summary>
        /// 事件的光标双击事件
        /// </summary>
        public event MouseDoubleClickDelegate MarkerMouseDoubleClick;
        /// <summary>
        ///  删除事件标记
        /// </summary>
        public void RemoveMarks(IMarker mark)
        {
            if (mark.Delete)
            {
                int channelID = int.Parse(mark.ID.Split(':')[0]);
                if (channelID > 100)
                    pChartMarks.Remove(mark);
                else
                {
                    int[] channelidx = (mark as RectangleMarkers).OnChannelIndexs;
                    IEnumerable<CurveItem> list = m_curveItems.FindAll(t => channelidx.Contains(t.ChannelNum));
                    foreach (CurveItem item in list)
                    {
                        string eventid = string.Format("{0}:{1}", item.ChannelNum, mark.ID.Split(':')[1]);
                        item.CurrentMarks.RemoveAll((t) => t.ID == eventid);
                    }
                }
                ChartAreaInvalidate(false);
            }
            else
            {
                ///添加改变同步
            }
        }
        /// <summary>
        ///  删除事件标记
        /// </summary>
        public void RemoveMarks(List<IMarker> marks)
        {
            for (int s = 0; s < marks.Count; s++)
            {
                IMarker mark = marks[s];
                int channelID = int.Parse(mark.ID.Split(':')[0]);
                if (channelID > 100)
                    pChartMarks.Remove(mark);
                else
                {
                    int[] channelidx = (mark as RectangleMarkers).OnChannelIndexs;
                    IEnumerable<CurveItem> list = m_curveItems.FindAll(t => channelidx.Contains(t.ChannelNum));
                    foreach (CurveItem item in list)
                    {
                        string eventid = string.Format("{0}:{1}", item.ChannelNum, mark.ID.Split(':')[1]);
                        item.CurrentMarks.RemoveAll((t) => t.ID == eventid);
                    }
                }
            }
            ChartAreaInvalidate(false);
        }
        private CurveItem m_currentItem = null;
        #endregion
        #region 头部效果
        private bool m_HeadTaskCompelet = true;
        private bool m_headReady = true;
        /// <summary>
        /// 重绘事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (m_headReady)
            {
                m_headReady = false;
                using (Graphics g = e.Graphics)
                {
                    try
                    {
                        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                        DateTime dt = DateTime.Now;
                        g.Clear(m_channelColor);
                        if (m_DistanceBaseLine > 0 && m_DistanceBaseLine < m_Height)
                        {
                            using (Pen pen = new Pen(Color.LightGray, 2))
                            {
                                if (master_CurveArea.UserSelected)
                                {
                                    g.FillRectangle(new SolidBrush(Color.FromArgb(150,225, 233, 255)), new RectangleF(0, 0, panel1.Width, m_DistanceBaseLine));
                                }
                                else
                                {
                                    g.FillRectangle(new SolidBrush(Color.FromArgb(150, 225, 233, 255)), new RectangleF(0, m_DistanceBaseLine, panel1.Width,panel1.Height- m_DistanceBaseLine));
                                }
                                int y = (int)(master_CurveArea.UserSelected ? 0 : m_DistanceBaseLine);
                                int y1 = (int)(master_CurveArea.UserSelected ? m_DistanceBaseLine : panel1.Height);
                                g.DrawLine(pen, new PointF(panel1.Width - 2, y), new PointF(panel1.Width - 2, y1));
                                pen.Color = Color.FromArgb( 123, 152, 186);
                                g.DrawLine(pen, new PointF(0, m_DistanceBaseLine), new PointF(panel1.Width, m_DistanceBaseLine));
                            }
                        }
                        else
                        {
                            using (Pen pen = new Pen(Color.LightGray, 1))
                            {
                                g.DrawLine(pen, new PointF(panel1.Width - 1, 0), new PointF(panel1.Width - 1, panel1.Height));
                            }
                        }
                        for (int i = 0; i < m_curveItems.Count; i++)
                        {
                            if (!m_curveItems[i].Visible)
                            {
                                continue;
                            }
                            float fontSize = (m_curveItems[i].yAxis.Displacement * 3 / 4);
                            fontSize = fontSize > 8 ? 8 : fontSize - 6;
                            float ynameLocation = m_curveItems[i].headBaseDistance - m_curveItems[i].Font.Height / 2.0f;
                            using (Font f = new Font("微软雅黑", fontSize))
                            {
                                if (m_curveItems[i].yAxis.CalibrationsVisible)
                                {
                                    int len = m_curveItems[i].yAxis.LegendLables.Count;
                                    if (len > 1)
                                    {
                                        string strMax = m_curveItems[i].yAxis.LegendLables[len - 1];
                                        string strMin = m_curveItems[i].yAxis.LegendLables[0];
                                        float y0 = m_curveItems[i].headBaseDistance - m_curveItems[i].yAxis.Displacement / 2;
                                        float y1 = m_curveItems[i].headBaseDistance + m_curveItems[i].yAxis.Displacement / 2 - f.Height;
                                        float x0 = panel1.Width - g.MeasureString(strMax, f).Width - 2;
                                        float x1 = panel1.Width - g.MeasureString(strMin, f).Width - 2;
                                        g.DrawString(strMax, f, new SolidBrush(Color.Green), new PointF(x0, y0));
                                        g.DrawString(strMin, f, new SolidBrush(Color.Green), new PointF(x1, y1));
                                    }
                                }
                            }
                            g.FillRectangle(new SolidBrush(m_curveItems[i].PenColor), 0, ynameLocation+3, 30, m_curveItems[i].Font.Height - 10);
                            g.DrawString(string.Format("{0}{1}", m_curveItems[i].TemporaryControl ? "* " : "", m_curveItems[i].Name), m_curveItems[i].Font, new SolidBrush(Color.FromArgb(102, 102, 102)), new PointF(32, ynameLocation));
                        }
                        Console.WriteLine(string.Format("*******头部区域发生一次重绘({0}ms)*********", (DateTime.Now - dt).TotalMilliseconds));
                    }
                    catch (Exception ee)
                    {
                        Console.WriteLine(ee.Message);
                    }
                    isPartyRectFresh = false;
                }
                m_headReady = true;
            }
        }
        private CurveItem headSelectItem = null;
        /// <summary>
        /// 重新绘制效果
        /// </summary>
        /// <param name="g"></param>
        /// <param name="color"></param>
        /// <param name="p"></param>
        private void reDrawHead(Graphics g, Color color, Point p, CurveItem find = null)
        {
            if (find != null)
            {
                float ynameLocation = find.headBaseDistance - find.Font.Height / 2.0f;
                g.Clip = new System.Drawing.Region(find.HeadRectangle);
                Color FColor = color;
                Color TColor = color;// Color.FromArgb(225, 233, 255);
                Brush b = new LinearGradientBrush(find.HeadRectangle, FColor, TColor, LinearGradientMode.Vertical);
                g.FillRectangle(b, find.HeadRectangle);
                g.FillRectangle(new SolidBrush(find.PenColor), 0, ynameLocation + 3, 30, find.Font.Height - 10);
                g.DrawString(string.Format("{0}{1}", find.TemporaryControl ? "* " : "", find.Name), find.Font, new SolidBrush(Color.WhiteSmoke), new PointF(32, ynameLocation));
                g.Dispose();
            }
        }
        /// <summary>
        ///  还原
        /// </summary>
        /// <param name="g"></param>
        /// <param name="find"></param>
        private void ResetHead(Graphics g, CurveItem find)
        {
            if (find != null)
            {
                isPartyRectFresh = true;
                m_PartyRectFreshID = find.ID;
                panel1.Invalidate(new System.Drawing.Region(find.HeadRectangle));
            }
        }
        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            if (currentItem != null && m_allowLeaveFresh)
            {
                isPartyRectFresh = true;
                m_PartyRectFreshID = currentItem.ID;
                panel1.Invalidate(new System.Drawing.Region(currentItem.HeadRectangle));
                currentItem = null;
                m_allowLeaveFresh = true;
            }
            bak_currentItem = null;
            if (headSelectItem != null)
            {
                headSelectItem = null;
            }
            m_dragLine = new RectangleF();
        }

        private Action<object, MouseEventArgs> Channel_InRightClick;
        private bool isPartyRectFresh = false;
        private string m_PartyRectFreshID = "";
        private bool head_MouseIsDown = false;
        private int bak_channelNo = -1;
        private float m_DistanceBaseLine = 0;
        private bool m_allowLeaveFresh = true;
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (headSelectItem == null)
                return;
            if (head_MouseIsDown)
            {
                bool hasfresh = false;
                if (e.Location.Y > m_DistanceBaseLine && headSelectItem.belong == 0)
                {
                    headSelectItem.belong = 1;
                    if (currentItem == null)
                    {
                        currentItem = m_curveItems.Find(t => t.IsLastViewChannel && Visible);
                        bak_channelNo = 1;
                    }
                }
                else if (e.Location.Y < m_DistanceBaseLine && headSelectItem.belong == 1)
                {
                    headSelectItem.belong = 0;
                    if (currentItem == null)
                    {
                        currentItem = m_curveItems.Find(t => t.yTop == 0 && Visible);
                        bak_channelNo = 0;
                    }
                }
                if (currentItem != null && bak_channelNo != -1)
                {
                    if (headSelectItem.ChannelNo < currentItem.ChannelNo)
                    {
                        int selectchannelno = headSelectItem.ChannelNo;
                        for (int i = selectchannelno + 1; i < currentItem.ChannelNo + bak_channelNo; i++)
                        {
                            m_curveItems[i].ChannelNo -= 1;
                        }
                    }
                    else
                    {
                        //将分屏的通道拖动回去时，会出现headselectitem.ChannelNo跟着变动的情况，所以for循环前赋值
                        int selectchannelno = headSelectItem.ChannelNo;
                        for (int i = currentItem.ChannelNo + bak_channelNo; i < selectchannelno; i++)
                        {
                            m_curveItems[i].ChannelNo += 1;
                        }
                    }
                    headSelectItem.ChannelNo = currentItem.ChannelNo + (bak_channelNo == 0 ? -1 : 1);
                    m_curveItems.Sort((student1, student2) => (student1.ChannelNo - student2.ChannelNo));
                    if (ChannelSortedHandle != null)
                    {
                        Dictionary<string, string> map = new Dictionary<string, string>();
                        for (int s = 0; s < m_curveItems.Count; s++)
                        {
                            map.Add(m_curveItems[s].ID, string.Format("{0};{1}:{2}", m_curveItems[s].ChannelNo, m_curveItems[s].belong, m_curveItems[s].belong == 0 ? master_CurveArea.BaseTimeLineSpan : bak_curveArea.BaseTimeLineSpan));
                        }
                        ChannelSortedHandle.BeginInvoke(map, null, null);
                    }
                    CurveItemChanged(panel1);
                    bak_channelNo = -1;
                    isPartyRectFresh = false;
                    m_allowLeaveFresh = false;
                    panel1.Invalidate();
                }
                else
                {
                    if (currentItem == null)
                    {
                        isPartyRectFresh = false;
                        panel1.Invalidate();
                    }
                    else
                    {
                        isPartyRectFresh = true;
                        m_PartyRectFreshID = currentItem.ID;
                        panel1.Invalidate();
                        //panel1.Invalidate(new System.Drawing.Region(currentItem.HeadRectangle));
                    }
                }
                if (headSelectItem != null)
                {
                    headSelectItem.isSelected = false;
                    if (!m_isRealDataCurve && !hasfresh)
                    {
                        ChartAreaInvalidate(true, headSelectItem.belong == 0 ? 1 : 2);
                        hasfresh = true;
                    }
                    headSelectItem = null;
                }
                head_MouseIsDown = false;
            }
            //else
            //{
            //    if (e.Button == MouseButtons.Right)
            //    {
            //        for (int i = 0; i < m_curveItems.Count; i++)
            //        {
            //            if (m_curveItems[i].HeadRectangle.Contains(e.Location) && m_curveItems[i].Visible)
            //            {
            //                if (ChannelHeadPopupHandler != null)
            //                {
            //                    ChannelHeadPopupHandler.BeginInvoke(m_curveItems[i].Clone(false), e.Button, null, null);
            //                }
            //                break;
            //            }
            //        }
            //    }
            //}
            m_HeadTaskCompelet = true;
        }
        private RectangleF m_dragLine = new RectangleF();
        private CurveItem currentItem = null;
        private CurveItem bak_currentItem = null;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            using (Graphics g = panel1.CreateGraphics())
            {
                currentItem = m_curveItems.Find(t => (t.Visible && t.HeadRectangle.Contains(e.Location)));
                if (currentItem == null)
                    return;
                if (head_MouseIsDown)
                {
                    if (headSelectItem == null)
                        return;
                    if (currentItem.ChannelNo == headSelectItem.ChannelNo)
                        return;
                    bak_channelNo = -1;
                    if (m_dragLine.Width != 0)
                    {
                        g.Clip = new Region(m_dragLine);
                        g.Clear(panel1.BackColor);
                    }
                    ///当前选中项的上边距区域
                    bool istopRec = new RectangleF(currentItem.HeadRectangle.X, currentItem.HeadRectangle.Y, currentItem.HeadRectangle.Width, currentItem.HeadRectangle.Height / 2).Contains(e.Location);
                    bool isbootomRec = new RectangleF(currentItem.HeadRectangle.X, currentItem.HeadRectangle.Y + currentItem.HeadRectangle.Height / 2, currentItem.HeadRectangle.Width, currentItem.HeadRectangle.Height / 2).Contains(e.Location);
                    if (!istopRec && !isbootomRec)
                    {
                        return;
                    }
                    bak_channelNo = istopRec ? 0 : 1;///表示要移动的通道是在当前位置序号之前还是之后，0表示在其上，1表示在其下
                                                     ///绘制拖拽效果的那根线
                    Color FColor = Color.Goldenrod;
                    Color TColor = Color.WhiteSmoke;
                    m_dragLine = new RectangleF(currentItem.HeadRectangle.X, istopRec ? currentItem.HeadRectangle.Y : currentItem.HeadRectangle.Y + currentItem.HeadRectangle.Height - 3, currentItem.HeadRectangle.Width, 3);
                    using (Brush b = new LinearGradientBrush(m_dragLine, FColor, TColor, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(b, m_dragLine);
                    }
                }
                #region 暂时去掉移动效果
                //else
                //{
                //    if (bak_currentItem == null)
                //    {
                //        bak_currentItem = currentItem;
                //    }
                //    else
                //    {
                //        if (currentItem.ChannelNo == bak_currentItem.ChannelNo)
                //            return;
                //        else
                //        {
                //            ResetHead(g, bak_currentItem);
                //            bak_currentItem = currentItem;
                //        }
                //    }
                //    reDrawHead(g, Color.DimGray, e.Location, currentItem);
                //}
                #endregion
            }
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                head_MouseIsDown = true;
                headSelectItem = m_curveItems.Find(t => t.HeadRectangle.Contains(e.Location) && t.Visible);
                if (headSelectItem == null)
                    return;
                headSelectItem.isSelected = true;
                if (!m_isRealDataCurve)
                {
                    ChartAreaInvalidate(true, headSelectItem.belong == 0 ? 1 : 2);

                }
                reDrawHead(panel1.CreateGraphics(), Color.FromArgb(116, 149, 243), e.Location, headSelectItem);
            }
            else if (e.Button == MouseButtons.Right)
            {
                Channel_InRightClick(sender, e);
            }
            m_HeadTaskCompelet = false;
        }

        /// <summary>
        /// 通道窗口的双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (headSelectItem == null)
                return;

            if (e.Button == MouseButtons.Left)
            {
                headSelectItem = m_curveItems.Find(t => t.HeadRectangle.Contains(e.Location) && t.Visible);
                if (ChannelHeadPopupHandler != null)
                    ChannelHeadPopupHandler.BeginInvoke(headSelectItem.Clone(false), e.Button, null, null);
            }
        }

        #region 通道右键快捷菜单处理
        /// <summary>
        /// 通道窗口的右键单击事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel1_RightClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            FillChannelHideData();
            FillWaveHideData();

            Point mousePosition = new Point(e.X + 5, e.Y - 5);
            mousePosition = panel1.PointToScreen(mousePosition);
            ChannelSetting.Show(mousePosition);
        }

        private void HideChannel_DropDownOpening(object sender, EventArgs e)
        {
            FillChannelHideData();
        }

        private void HideWave_DropDownOpening(object sender, EventArgs e)
        {
            FillWaveHideData();
        }

        /// <summary>
        /// 防止点触关闭下拉框菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DropDown_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            e.Cancel = e.CloseReason == ToolStripDropDownCloseReason.ItemClicked;
        }

        /// <summary>
        /// 填充通道可见性设置的数据
        /// </summary>
        private void FillChannelHideData()
        {
            //填充通道不可见的快捷菜单项
            HideChannel.DropDownItems.Clear();
            foreach (var item in m_curveItems)
            {
                ToolStripMenuItem meanuItem = new ToolStripMenuItem(item.Name);
                meanuItem.Tag = item;
                meanuItem.Checked = item.Visible;
                meanuItem.CheckOnClick = true;
                meanuItem.CheckedChanged += HideChannel_CheckedChanged;
                HideChannel.DropDownItems.Add(meanuItem);
            }
        }

        /// <summary>
        /// 填充波形可见性设置的数据
        /// </summary>
        private void FillWaveHideData()
        {
            //填充波形不可见的快捷菜单项
            HideWave.DropDownItems.Clear();
            var visibleCurveItems = m_curveItems.Where(t => t.Visible).ToList();
            foreach (var item in visibleCurveItems)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem(item.Name);
                menuItem.Tag = item;
                menuItem.Checked = item.IsDraw;
                menuItem.CheckOnClick = true;
                menuItem.CheckedChanged += HideWave_CheckedChanged;
                HideWave.DropDownItems.Add(menuItem);
            }
        }

        /// <summary>
        /// 波形可见性选中状态改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HideWave_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender is ToolStripMenuItem))
                return;
            var menuItem = sender as ToolStripMenuItem;
            if (!(menuItem.Tag is CurveItem))
                return;

            var curveItem = menuItem.Tag as CurveItem;
            curveItem.IsDraw = !curveItem.IsDraw;
            if (m_isRealDataCurve)
                return;
            ChartAreaInvalidate(true);
        }

        /// <summary>
        /// 通道可见性选中状态改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HideChannel_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender is ToolStripMenuItem))
                return;
            var menuItem = sender as ToolStripMenuItem;
            if (!(menuItem.Tag is CurveItem))
                return;

            var curveItem = menuItem.Tag as CurveItem;

            if (ShowOrHideChannelHandler != null)
                ShowOrHideChannelHandler.BeginInvoke(curveItem, null, null);
        }
        #endregion

        #endregion
        #region 私有方法
        /// <summary>
        /// 曲线集合
        /// </summary>
        private List<CurveItem> m_curveItems = new List<CurveItem>();
        /// <summary>
        /// 调整两个面板的大小
        /// </summary>
        private void chartResize()
        {
            if (m_DistanceBaseLine == 0)
            {
                master_CurveArea.Height = 0;
                master_CurveArea.Visible = false;
                bak_curveArea.Visible = true;
                if (!bak_curveArea.UserSelected)
                    bak_curveArea.UserSelected = true;
            }
            else if (m_DistanceBaseLine == m_Height)
            {
                bak_curveArea.Visible = false;
                master_CurveArea.Height = m_Height;
                master_CurveArea.Visible = true;
                if (!master_CurveArea.UserSelected)
                    master_CurveArea.UserSelected = true;
            }
            else
            {
                master_CurveArea.Height = (int)m_DistanceBaseLine;
                master_CurveArea.Visible = true;
                bak_curveArea.Visible = true;
            }
            //判断是否 是 分屏
            if (master_CurveArea.Visible && bak_curveArea.Visible)
            {
                master_CurveArea.isSplitscreen = true;
                bak_curveArea.isSplitscreen = true;
            }
            else
            {
                master_CurveArea.isSplitscreen = false;
                bak_curveArea.isSplitscreen = false;
            }
        }
        /// <summary>
        /// 通道变化处理
        /// </summary>
        private void CurveItemChanged(Control ctr)
        {
            float curveheight = ctr.Height;
            bool baseline = false;
            int cnt = m_curveItems.Where(t => t.Visible == true).Count();
            if (cnt != 0)
            {
                using (Graphics g = ctr.CreateGraphics())
                {
                    ///根据通道个数，计算平均通道高度
                    float m = curveheight / cnt;
                    m_pRate = (float)(m * 25.4 / g.DpiY);
                    int ChannelNo = 0;
                    float fontSize = (m * 3 / 4);
                    fontSize = fontSize > 13 ? 13 : fontSize - 2;
                    int lastIdx = 0;
                    m_curveItems.Sort((student1, student2) => (student1.ChannelNo - student2.ChannelNo));
                    int basetimespan0 = master_CurveArea.BaseTimeLineSpan * 1000, basetimespan1 = bak_curveArea.BaseTimeLineSpan * 1000;
                    for (int i = 0; i < m_curveItems.Count; i++)
                    {
                        m_curveItems[i].IsLastViewChannel = false;
                        if (!m_curveItems[i].Visible)
                            continue;
                        m_curveItems[i].Font = new Font("微软雅黑", fontSize);
                        m_curveItems[i].yTop = m * ChannelNo;
                        if (!baseline && m_curveItems[i].belong == 1)
                        {
                            m_DistanceBaseLine = m_curveItems[i].yTop;
                            baseline = true;
                        }
                        m_curveItems[i].headBaseDistance = m_curveItems[i].yTop + m / 2;
                        m_curveItems[i].yBottom = m_curveItems[i].belong == 0 ? m_curveItems[i].yTop + m : m_curveItems[i].yTop - m_DistanceBaseLine + m;
                        m_curveItems[i].yBaseDistance = m_curveItems[i].belong == 0 ? m_curveItems[i].headBaseDistance : m_curveItems[i].headBaseDistance - m_DistanceBaseLine;
                        m_curveItems[i].yAxis.Displacement = m;
                        m_curveItems[i].yAxis.offSetDistance = m_curveItems[i].belong == 0 ? m_curveItems[i].yTop : m_curveItems[i].yTop - m_DistanceBaseLine;
                        m_curveItems[i].xAxis.MaxValue = m_curveItems[i].belong == 0 ? basetimespan0 : basetimespan1;
                        m_curveItems[i].xAxis.offSetDistance = 0;
                        m_curveItems[i].PixelRateCoefficient = m_pRate;
                        if (m_curveItems[i].PixelRate != 0)
                        {
                            m_curveItems[i].yAxis.MaxValue = m_pRate * m_curveItems[i].PixelRate;
                            m_curveItems[i].PixelConstants = m_curveItems[i].PixelRate;
                        }
                        else
                        {
                            m_curveItems[i].PixelConstants = m_curveItems[i].yAxis.MaxValue / m_pRate;
                        }
                        m_curveItems[i].yLimitMaxDistance = curveheight;
                        m_curveItems[i].yLimitMinDistance = 0;
                        m_curveItems[i].xAxis.Displacement = m_Width;
                        m_curveItems[i].ClientRectangle = new RectangleF(0, m_curveItems[i].yAxis.offSetDistance, m_Width, m);
                        m_curveItems[i].HeadRectangle = new RectangleF(0, m_curveItems[i].yTop, panel1.Width, m);
                        ChannelNo++;
                        lastIdx = i;
                    }
                    if (!baseline)
                    {
                        m_DistanceBaseLine = m_Height;
                    }
                    m_curveItems[lastIdx].IsLastViewChannel = true;
                }
            }
        }
        #endregion
        #region 公有成员
        public void CurveItemChanged()
        {
            CurveItemChanged(panel1);
        }
        private float m_pRate = 0;
        /// <summary>
        /// 获取通道位移常数
        /// </summary>
        public float pRate
        {
            get
            {
                return m_pRate;
            }
        }
        /// <summary>
        /// 事件类型发生变化时
        /// </summary>
        /// <param name="marks"></param>
        public delegate bool MarkTypeChangedDelegate(IMarker[] marks, IMarker.MarkType NewMarkTyp, string NewMarkName);
        /// <summary>
        /// 通过快捷方式使事件类型发生变化时触发
        /// </summary>
        public event MarkTypeChangedDelegate MarkTypeChanged;
        /// <summary>
        /// 绘制趋势图前发生
        /// </summary>
        public delegate bool DrawImageBefore();
        /// <summary>
        /// 绘制趋势图前发生
        /// </summary>
        public event DrawImageBefore DrawImageBeforeHandle;
        /// <summary>
        /// 通道顺序调整后发生
        /// </summary>
        public delegate bool ChannelSorted(Dictionary<string, string> SortMap);
        /// <summary>
        /// 通道顺序调整后发生
        /// </summary>
        public event ChannelSorted ChannelSortedHandle;
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
        public delegate void JumpHappenDelegate(int StartFrameIndex, DateTime MarkStartTime, int belong);
        public event JumpHappenDelegate JumpHappenHandler;
        /// <summary>
        /// 分屏时点击事件可以跳转
        /// </summary>
        /// <param name="StartFrameNo"></param>
        /// <param name="StartTime"></param>
        private void Bak_curveArea_JumpHappenHandler(int StartFrameIndex, DateTime MarkStartTime, int belong)
        {
            if (JumpHappenHandler != null)
            {
                JumpHappenHandler.Invoke(StartFrameIndex, MarkStartTime, belong);
            }
        }
        #endregion
        #region 拖拉标记事件松开鼠标后，进度条帧同步
        /// <summary>
        /// 拖拉标记事件松开鼠标后，进度条帧同步
        /// </summary>
        /// <param name="endFrameNo"></param>
        /// <param name="moveOffTime"></param>
        private void Master_CurveArea_MouseMoveUpHandler(int endFrameNo, int moveOffTime)
        {
            if (MouseUpChangeFrameHandle != null)
                MouseUpChangeFrameHandle.Invoke(endFrameNo, moveOffTime);
        }

        public delegate void MouseUpChangeFrameDelegate(int endFrameNo, int moveOffTime);
        /// <summary>
        /// 拖拉标记事件松开鼠标后，进度条帧同步
        /// </summary>
        public event MouseUpChangeFrameDelegate MouseUpChangeFrameHandle;
        #endregion
        /// <summary>
        /// 设置热键使能
        /// </summary>
        public bool HotKeyEnable
        {
            set
            {
                master_CurveArea.HotKeyEnable = bak_curveArea.HotKeyEnable = value;
            }
        }
        private bool m_fnstop = true;
        /// <summary>
        /// 实时采集时的开关
        /// </summary>
        public bool FnStop
        {
            set
            {
                m_fnstop = value;
            }
            get
            {
                return m_fnstop;
            }
        }
        /// <summary>
        /// 移动增量s
        /// </summary>
        private int m_moveTimeSpan = 1;
        private int m_BaseTimeLineSpan = 30;
        /// <summary>
        /// 时基s
        /// </summary>
        public int BaseTimeLineSpan
        {
            set
            {
                m_BaseTimeLineSpan = value;
                int belong = 0;
                if (master_CurveArea.UserSelected)
                {
                    master_CurveArea.BaseTimeLineSpan = value;
                }
                else
                {
                    belong = 1;
                    bak_curveArea.BaseTimeLineSpan = value;
                }
                int max = value * 1000;
                for (int i = 0; i < m_curveItems.Count; i++)
                {
                    if (m_curveItems[i].belong == belong)
                        m_curveItems[i].xAxis.MaxValue = max;
                }
            }
            get
            {
                if (master_CurveArea.UserSelected)
                {
                    m_BaseTimeLineSpan = master_CurveArea.BaseTimeLineSpan;
                }
                else
                {
                    m_BaseTimeLineSpan = bak_curveArea.BaseTimeLineSpan;
                }
                return m_BaseTimeLineSpan;
            }
        }
        private int m_OtherBaseTimeLineSpan = 30;
        /// <summary>
        /// 另一个屏的时基s
        /// </summary>
        public int OtherBaseTimeLineSpan
        {
            set
            {
                m_OtherBaseTimeLineSpan = value;
            }
            get
            {
                if (master_CurveArea.UserSelected)
                {
                    m_OtherBaseTimeLineSpan = bak_curveArea.BaseTimeLineSpan;
                }
                else
                {
                    m_OtherBaseTimeLineSpan = master_CurveArea.BaseTimeLineSpan;
                }
                return m_OtherBaseTimeLineSpan;
            }
        }

        /// <summary>
        /// 是否需要显示时间轴刻度
        /// </summary>
        private bool m_TimeLineVisible = false;
        /// <summary>
        /// 是否显示时间轴线
        /// </summary>
        public bool TimeLineVisible
        {
            set
            {
                ///实时曲线时是不会显示轴线的
                if (m_isRealDataCurve)
                    return;
                m_TimeLineVisible = value;
                master_CurveArea.TimeLineVisible = value;
                bak_curveArea.TimeLineVisible = value;
            }
            get
            {
                return m_TimeLineVisible;
            }
        }

        private bool m_isRealDataCurve = false;
        /// <summary>
        /// 是否是实时曲线
        /// </summary>
        public bool isRealDataCurve
        {
            set
            {
                m_isRealDataCurve = value;
                master_CurveArea.isRealDataCurve = value;
                bak_curveArea.isRealDataCurve = value;
            }
            get
            {
                return m_isRealDataCurve;
            }
        }

        private Rectangle m_rectangle = new Rectangle();
        /// <summary>
        /// 绘制区域
        /// </summary>
        public Rectangle CurveClientRectangle
        {
            get
            {
                return m_rectangle;
            }
        }
        /// <summary>
        /// 全局标记
        /// </summary>
        public List<IMarker> pChartMarks = new System.Collections.Generic.List<IMarker>();
        /// <summary>
        /// 默认背景色为黑
        /// </summary>
        private Color m_backcolor = System.Drawing.SystemColors.Window;
        /// <summary>
        /// 获取或设置背景色
        /// </summary>
        public new Color BackColor { set { m_backcolor = value; } get { return m_backcolor; } }
        /// <summary>
        /// 睡眠分期数组
        /// </summary>
        public string[] SleepStage = new string[0];
        /// <summary>
        /// 睡眠体位数组
        /// </summary>
        public int[] SleepPos = new int[0];
        /// <summary>
        /// 获取时间偏移量
        /// </summary>
        public int MoveTimeSpan
        {
            get
            {
                if (master_CurveArea.UserSelected)
                {
                    return master_CurveArea.MoveTimeSpan;
                }
                else
                    return bak_curveArea.MoveTimeSpan;
            }
        }
        /// <summary>
        /// 定标的数据
        /// </summary>
        public List<IMarker> CalibrationMarks = new System.Collections.Generic.List<IMarker>();
        /// <summary>
        /// 数据清空
        /// </summary>
        public void Clear()
        {
            Task[] tt = new Task[m_curveItems.Count];
            for (int i = 0; i < tt.Length; i++)
            {
                tt[i] = new Task((obj) =>
                {
                    (obj as CurveItem).Clear();
                }, m_curveItems[i]);
                tt[i].Start();
            }
            Task.WaitAll(tt);///等待所有异步线程完成
            for (int i = 0; i < tt.Length; i++)
            {
                tt[i].Dispose();///释放线程资源
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
        /// <summary>
        /// 情况标签集合
        /// </summary>
        public void ClearMarkers(bool isrecover = false)
        {
            if(isrecover)
            {
                for (int i = 0; i < m_curveItems.Count; i++)
                {
                    m_curveItems[i].CurrentMarks.Clear();
                }
                pChartMarks.Clear();
            }
            for (int i = 0; i < m_curveItems.Count; i++)
            {
                m_curveItems[i].CurrentMarks.RemoveAll((IMarker t) => (int)t.MarkTyp > (int)IMarker.MarkType.None || (int)t.MarkTyp < (int)IMarker.MarkType.AnlysisEnable);
            }
            pChartMarks.RemoveAll((IMarker t) => t.MarkTyp != IMarker.MarkType.None);
        }
        /// <summary>
        /// 添加克隆的通道
        /// </summary>
        /// <param name="item"></param>
        public void AddCloneCurve(CurveItem item)
        {
            item.IsCloneItem = true;
            m_curveItems.Insert(item.ChannelNo, item);
            for (int i = item.ChannelNo + 1; i < m_curveItems.Count; i++)
            {
                m_curveItems[i].ChannelNo += 1;
            }
        }
        /// <summary>
        /// 绑定通道数据
        /// </summary>
        public void BindXYDataValues(string ID, List<float> xyvalues)
        {
            CurveItem find = FindCurve(ID);
            if (find != null)
            {
                find.yAxis.MaxValue = xyvalues.Max();
                find.xAxis.MaxValue = 30000;
                find.BindYDataValues(xyvalues);
            }
        }
        /// <summary>
        /// 获取所有通道ID列表
        /// </summary>
        /// <returns></returns>
        public List<string> getItemIDs()
        {
            return m_curveItems.Select(t => t.ID).ToList();
        }
        /// <summary>
        /// 查找曲线
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public CurveItem FindCurve(string ID)
        {
            return m_curveItems.Find(t => t.ID == ID);
        }
        /// <summary>
        /// 查找曲线
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public CurveItem FindCurve(int channelNum)
        {
            return m_curveItems.Find(t => t.ChannelNum == channelNum && !t.IsCloneItem);
        }
        /// <summary>
        /// 查找曲线
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public CurveItem FindCurve(int channelNum, bool isclone)
        {
            return m_curveItems.Find(t => t.ChannelNum == channelNum && t.IsCloneItem == isclone);
        }
        /// <summary>
        /// 加入曲线
        /// </summary>
        /// <param name="item"></param>
        public CurveItem AddCurve(CurveItem item)
        {
            CurveItem find = FindCurve(item.ID);
            if (find == null)
            {
                item.xAxis.MaxValue = m_BaseTimeLineSpan * 1000;
                item.TimeSpan = item.TimeSpan;
                m_curveItems.Add(item);
                return item;
            }
            return find;
        }
        /// <summary>
        /// 移除指定通道
        /// </summary>
        /// <param name="item"></param>
        public void RemoveCurve(CurveItem item)
        {
            if (item != null)
            {
                if (item.IsCloneItem)
                {
                    try
                    {
                        FindCurve(item.ID.Replace("Clone_", "")).CheckPushDataDelegateList(item.ID);
                    }
                    catch { }
                }
                item.Clear();
                m_curveItems.Remove(item);
            }
        }
        /// <summary>
        /// 移除指定通道
        /// </summary>
        /// <param name="item"></param>
        public void RemoveCurve(string ID)
        {
            RemoveCurve(FindCurve(ID));
        }
        /// <summary>
        /// 移除指定通道
        /// </summary>
        /// <param name="item"></param>
        public void RemoveAllCurve()
        {
            Clear();
            m_curveItems.Clear();
        }
        /// <summary>
        /// 获取当前选择的通道
        /// </summary>
        /// <returns></returns>
        public CurveItem GetCurrentItem()
        {
            return ItemMouseOnHead();
        }
        /// <summary>
        /// 主动刷新趋势图
        /// </summary>
        /// <param name="FrameCnt">当前帧序号</param>
        /// <param name="offsetTime">当前帧的偏移量</param>
        /// <returns>返回下一帧的帧序号</returns>
        public void Invalidate(DateTime startTime, int FrameCnt, int offsetTime, int TotalFrameCnt, int witchcurvearea = 2)
        {
            m_TotalFrameCnt = TotalFrameCnt;
            m_FrameCnt = FrameCnt;
            if (witchcurvearea == 1)
            {
                master_CurveArea.Invalidate(startTime, FrameCnt, offsetTime, m_TotalFrameCnt);
            }
            else if (witchcurvearea == 0)
            {
                bak_curveArea.Invalidate(startTime, FrameCnt, offsetTime, m_TotalFrameCnt);
            }
            else
            {
                master_CurveArea.Invalidate(startTime, FrameCnt, offsetTime, m_TotalFrameCnt);
                bak_curveArea.Invalidate(startTime, FrameCnt, offsetTime, m_TotalFrameCnt);
            }
            if (SleepStage.Length == 0)
            {
                SleepStage = new string[TotalFrameCnt];
                SleepPos = new int[TotalFrameCnt];

            }
            //panel1.Invalidate();
        }
        /// <summary>
        ///  获取当前帧序号
        /// </summary>
        public int CurrentFrameNo
        {
            get
            {
                return m_FrameCnt;
            }
        }
        /// <summary>
        /// 主动刷新界面
        /// </summary>
        public void ChartVedioLineInvalidate()
        {
            if (master_CurveArea.Visible)
            {
                if (master_CurveArea.PaintReady)
                    master_CurveArea.AreaInvalidate();
            }
            if (bak_curveArea.Visible)
            {
                if (bak_curveArea.PaintReady)
                    bak_curveArea.AreaInvalidate();
            }
        }
        /// <summary>
        /// 主动刷新界面
        /// </summary>
        public void ChartInvalidate()
        {
            ChartAreaInvalidate(true);
        }
        /// <summary>
        /// 临时变更时的刷新
        /// </summary>
        public void TemporaryInvalidate(bool chartFresh = true)
        {
            if (chartFresh)
            {
                ChartAreaInvalidate(true);
            }
            isPartyRectFresh = false;
            panel1.Invalidate();
        }
        /// <summary>
        /// 刷新一次趋势图
        /// </summary>
        public bool ChartAreaInvalidate(bool userRefresh = false, int all = 0)
        {
            bool headfresh = false;
            if (DrawImageBeforeHandle != null && userRefresh)
            {
                if (DrawImageBeforeHandle.Invoke())
                {
                    CurveItemChanged(panel1);
                    isPartyRectFresh = false;
                    ///更新趋势头部
                    panel1.Invalidate();
                    headfresh = true;
                }
            }
            if (userRefresh)
            {
                try
                {
                    if (!this.IsDisposed)
                    {
                        this.Invoke(new MethodInvoker(() =>
                        {
                            chartResize();
                        }));
                    }
                }
                catch { }
            }
            if (master_CurveArea.Visible && all != 2)
            {
                if (master_CurveArea.PaintReady)
                    master_CurveArea.AreaInvalidate(userRefresh);
            }
            if (bak_curveArea.Visible && all != 1)
            {
                if (bak_curveArea.PaintReady)
                    bak_curveArea.AreaInvalidate(userRefresh);
            }
            return headfresh;
        }
        /// <summary>
        /// 事件使能
        /// </summary>
        public bool MarkEnable
        {
            set
            {
                master_CurveArea.MarkEnable = bak_curveArea.MarkEnable = value;
            }
        }
        /// <summary>
        /// 返回当前光标所在的通道头部名
        /// </summary>
        /// <returns></returns>
        public CurveItem ItemMouseOnHead()
        {
            return m_curveItems.Find(t => t.HeadRectangle.Contains(panel1.PointToClient(Cursor.Position)) && t.Visible);
        }
        /// <summary>
        /// 头部相应滚轮事件
        /// </summary>
        /// <param name="pixelvalue"></param>
        /// <returns></returns>
        public bool HeadMouseWheel(CurveItem find, float pixelvalue)
        {
            if (find != null)
            {
                find.PixelRate = pixelvalue;
                find.PixelConstants = find.PixelRate;
                if (pixelvalue > 0)
                {
                    find.yAxis.MaxValue = m_pRate * find.PixelRate;
                }
                if (m_isRealDataCurve && !m_fnstop)
                    return true;
                ChartAreaInvalidate(true, find.belong == 0 ? 1 : 2);
            }
            return false;
        }
        /// <summary>
        /// 设置热键清单
        /// </summary>
        /// <param name="Hotskeys"></param>
        public void SetHotKeys(List<HotKey> Hotskeys)
        {
            m_HotKeys = Hotskeys;
        }
        /// <summary>
        /// 触发按钮事件
        /// </summary>
        /// <param name="e"></param>
        public void setKeyDown(KeyEventArgs e)
        {
            if (bak_curveArea.UserSelected)
            {
                bak_curveArea.setKeyDown(e);
            }
            else
            {
                master_CurveArea.setKeyDown(e);
            }
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public new void Dispose()
        {
            //Clear();//用了多线程还是会增加200ms左右的耗时，暂时去掉
            m_curveItems.Clear();
            master_CurveArea.Dispose();
            bak_curveArea.Dispose();
            this.Dispose(true);
            GC.Collect();
        }
        public delegate bool SelectRectangleKeyDownDelegate(string ChannelID, CurveItem mark);
        /// <summary>
        /// 快捷键标记事件
        /// </summary>
        public event SelectRectangleKeyDownDelegate SelectRectangleKeyDownHandler;

        public DateTime VedioTimeLine
        {
            set
            {
                bak_curveArea.VedioTimeLine = master_CurveArea.VedioTimeLine = value;
            }
        }
        #endregion
        #region 获取当前可见时间段数据时触发
        public delegate bool ReadChannelDataDelegate(DateTime start, DateTime end, List<int> channelIDs);
        /// <summary>
        /// 获取当前可见时间段数据时触发
        /// </summary>
        public event ReadChannelDataDelegate ReadChannelDataHandler;
        /// <summary>
        /// 读取通道数据
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        private bool ReadChannelData(DateTime start, DateTime end, List<int> channelIDs)
        {
            if (ReadChannelDataHandler != null)
            {
                return ReadChannelDataHandler.Invoke(start, end, channelIDs);
            }
            return false;
        }
        public delegate bool ReadChannelDataExDelegate(DateTime start, DateTime end, List<CurveItem> curveItems);
        /// <summary>
        /// 获取当前可见时间段数据时触发
        /// </summary>
        public event ReadChannelDataExDelegate ReadChannelDataExHandler;

        /// <summary>
        /// 读取通道数据
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        private bool ReadChannelData(DateTime start, DateTime end, List<CurveItem> curveItems)
        {
            if (ReadChannelDataExHandler != null)
            {
                return ReadChannelDataExHandler.Invoke(start, end, curveItems);
            }
            return false;
        }
        #endregion
        private List<IMarker> m_mulSleepMarks = new List<IMarker>();
        /// <summary>
        /// 将存放多次小睡事件的list传值过来
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="startLine"></param>
        /// <param name="endLine"></param>
        public void DrawcurveAreaMultSleepUpdate(List<pChart.IMarker> MultipleSleepMarks)
        {
            m_mulSleepMarks = new List<IMarker>(MultipleSleepMarks);

        }

        private void ChannelSetting_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            e.Cancel = e.CloseReason == ToolStripDropDownCloseReason.ItemClicked;
        }
        #region 视频联动
        /// <summary>
        /// 播放状态更改时发生
        /// </summary>
        /// <param name="play"></param>
        /// <param name="currentTime"></param>
        public delegate void VideoStatusChangedDelegate(bool play, DateTime currentTime);
        /// <summary>
        /// 播放状态更改时发生
        /// </summary>
        public event VideoStatusChangedDelegate VideoStatusChanged;

        private void videoStatusChange(bool play, DateTime currentTime)
        {
            if (VideoStatusChanged != null)
            {
                VideoStatusChanged.BeginInvoke(play, currentTime, null, null);
            }
        }
        #endregion
    }
}
