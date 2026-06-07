using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Drawing.Imaging;
namespace AwareTec.Polysmith.pChart
{
    public partial class Chart : UserControl
    {
        #region 属性
        private System.Timers.Timer m_Timer1 = null;
        /// <summary>
        /// 绘图区域高
        /// </summary>
        private int m_Height = 900;
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
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        public Chart()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer
                 | ControlStyles.ResizeRedraw
                 | ControlStyles.Selectable
                 | ControlStyles.AllPaintingInWmPaint
                 | ControlStyles.UserPaint
                 | ControlStyles.SupportsTransparentBackColor
                 | ControlStyles.Opaque,
            true);
            this.UpdateStyles();
            this.Load += ChartCaptains_Load;
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0014) // 禁掉清除背景消息
            {
                return;
            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// 窗体加载时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChartCaptains_Load(object sender, EventArgs e)
        {
            this.Resize += Chart_Resize;
            this.panel1.Paint += panel1_Paint;
            //this.panel1.MouseLeave += panel1_MouseLeave;
            this.panel1.MouseDown += panel1_MouseDown;
            this.panel1.MouseMove += panel1_MouseMove;
            this.panel1.MouseUp += panel1_MouseUp;
            master_CurveArea.UserSelected = true;
            master_CurveArea.getCalibrationMarksHandle += bak_CurveArea_getCalibrationMarksHandle;
            master_CurveArea.getGlobalMarkersHandle += bak_CurveArea_getGlobalMarkersHandle;
            master_CurveArea.getAllItemsHandle += bak_CurveArea_getAllItemsHandle;
            master_CurveArea.getSleepPoseHandle += bak_CurveArea_getSleepPoseHandle;
            master_CurveArea.getSleepStageHandle += bak_CurveArea_getSleepStageHandle;
            master_CurveArea.ChannelViewPopupHandler += bak_CurveArea_ChannelViewPopupHandler;
            master_CurveArea.CaptainValueChangeHandler += bak_CurveArea_CaptainValueChangeHandler;
            master_CurveArea.MarkerMouseDoubleClick += bak_CurveArea_MarkerMouseDoubleClick;
            master_CurveArea.MarkerDeleteHandler += RemoveMarks;
            master_CurveArea.SelectedHappenHandler += master_CurveArea_SelectedHappenHandler;
            master_CurveArea.Belong = 0;
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
            bak_curveArea.Belong = 1;
            if (m_isRealDataCurve)
            {
                m_Timer1 = new System.Timers.Timer()
                {
                    Interval = 1000,
                    Enabled = true
                };
                m_Timer1.Elapsed += m_Timer1_Elapsed;
            }
            CurveItemChanged(panel1);
        }
        #region 数据交互实现
        public delegate void SelectedHappenDelegate(int belong,int TimeSpanLine);
        /// <summary>
        /// 选中事件发生时
        /// </summary>
        public event SelectedHappenDelegate SelectedHappenHandler;
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
            if (SelectedHappenHandler != null)
                SelectedHappenHandler.Invoke(belong,timespan);
        }

        void bak_CurveArea_CaptainValueChangeHandler(curveArea.CaptainInfoUion info)
        {
            using (Graphics g = panel3.CreateGraphics())
            {
                int right = panel1.Right;
                g.Clip = new Region(new Rectangle(right, 0, panel3.Width, panel3.Height));
                g.Clear(panel3.BackColor);
                if (info.showTyp == 1)
                {
                    DrawTitile(g, right, panel3.Width, panel3.Height, info.CurrentTime.ToString("HH:mm:ss.f"), info.NowLocationX + panel1.Right, info.ViewStartTime, info.ViewEndTime, info.CurrentFrameNo);
                }
                else if (info.showTyp == 2)
                {
                    TimeSpan ts = (info.RectEndTime - info.RectStartTime);
                    DrawTitile(g, panel3.Width, panel3.Height, string.Format("{0} <  {1:f1}s  > {2}", info.RectStartTime.ToString("HH:mm:ss.f"), Math.Abs(ts.TotalMilliseconds) / 1000.0, info.RectEndTime.ToString("HH:mm:ss.f")));
                }
                else
                {
                    DrawTitile(g, right, panel3.Width, panel3.Height, info.ViewStartTime, info.ViewEndTime, info.CurrentFrameNo);
                }
                Console.WriteLine("*******标题区域发生一次重绘*********");
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
            Font strFont = new System.Drawing.Font("华文中宋", 12F, FontStyle.Bold);
            SizeF cwaterSize = g.MeasureString(value, strFont);
            PointF valueP = new PointF(width / 2 - cwaterSize.Width / 2, height - cwaterSize.Height - 1);
            g.DrawString(value, strFont, Brushes.Blue, valueP);
            Console.WriteLine(" 画出框选区域的范围值");
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
            Font strFont = new System.Drawing.Font("华文中宋", 12F, FontStyle.Bold);
            SizeF cwaterSize = g.MeasureString(value, strFont);
            PointF valueP = new PointF(x - cwaterSize.Width / 2, height - cwaterSize.Height - 1);
            if (valueP.X + cwaterSize.Width > width)
                valueP.X = width - cwaterSize.Width;
            else if (valueP.X - offset < 0)
            {
                valueP.X = offset;
            }
            g.DrawString(value, strFont, Brushes.Blue, valueP);
            string strStart = string.Format("{0}({1} / {2})", st.ToString("HH:mm:ss"), framecnt, m_TotalFrameCnt);
            string strEnd = et.ToString("HH:mm:ss");
            SizeF swaterSize = g.MeasureString(strStart, strFont);
            PointF startP = new PointF(offset, height - swaterSize.Height - 1);
            if (valueP.X > startP.X + swaterSize.Width)
            {
                g.DrawString(strStart, strFont, Brushes.Blue, startP);
            }
            SizeF ewaterSize = g.MeasureString(strEnd, strFont);
            PointF endP = new PointF(width - ewaterSize.Width - 1, height - ewaterSize.Height - 1);
            if (valueP.X + cwaterSize.Width < endP.X)
            {
                g.DrawString(strEnd, strFont, Brushes.Blue, endP);
            }
            Console.WriteLine("绘出光标移动时的显示时间轴");
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
            Font strFont = new System.Drawing.Font("华文中宋", 12F, FontStyle.Bold);
            string strStart = string.Format("{0} ({1} / {2})", st.ToString("HH:mm:ss"), framecnt, m_TotalFrameCnt);
            string strEnd = et.ToString("HH:mm:ss");
            SizeF swaterSize = g.MeasureString(strStart, strFont);
            PointF startP = new PointF(offset, height - swaterSize.Height - 1);
            g.DrawString(strStart, strFont, Brushes.Blue, startP);
            SizeF ewaterSize = g.MeasureString(strEnd, strFont);
            PointF endP = new PointF(width - ewaterSize.Width - 1, height - ewaterSize.Height - 1);
            g.DrawString(strStart, strFont, Brushes.Blue, endP);
            Console.WriteLine("绘出开始结束时间刻度");
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
                m_Timer1.Interval = 100;
                if (!m_fnstop)
                    ChartAreaInvalidate(true);
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
            m_Width = panel3.Width - panel1.Width;
            m_Height = panel1.Height;
            m_DistanceBaseLine = m_Height;
            m_rectangle = new Rectangle(new Point(0, 0), new Size(m_Width, m_Height));
            if (panel1.Height > 0)
            {
                CurveItemChanged(panel1);
            }
            chartResize();
            panel1.Invalidate();
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
                string channelID = mark.ID.Split(':')[0];
                if (channelID == "All")
                    pChartMarks.Remove(mark);
                else
                {
                    int[] channelidx = (mark as RectangleMarkers).OnChannelIndexs;
                    IEnumerable<CurveItem> list = m_curveItems.FindAll(t => channelidx.Contains(t.ChannelNum));
                    foreach (CurveItem item in list)
                    {
                        string eventid = string.Format("{0}:{1}", item.ID, mark.ID.Split(':')[1]);
                        item.m_Marks.RemoveAll((t) => t.ID == eventid);
                    }
                }
                ChartAreaInvalidate(false);
            }
            else
            {
                ///添加改变同步
            }
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
                    DateTime dt = DateTime.Now;
                    for (int i = 0; i < m_curveItems.Count; i++)
                    {
                        if (!m_curveItems[i].Visible)
                        {
                            continue;
                        }
                        if (isPartyRectFresh)
                        {
                            if (m_curveItems[i].ID != m_PartyRectFreshID)
                            {
                                continue;
                            }
                        }
                        float fontSize = (m_curveItems[i].yAxis.Displacement * 3 / 4);
                        fontSize = fontSize > 8 ? 8 : fontSize - 2;
                        float ynameLocation = m_curveItems[i].headBaseDistance - m_curveItems[i].Font.Height / 2;
                        using (Font f = new Font("宋体", fontSize))
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
                        g.FillRectangle(new SolidBrush(m_curveItems[i].PenColor), 0, ynameLocation + 3, 30, m_curveItems[i].Font.Height - 10);
                        g.DrawString(string.Format("{0}{1}", m_curveItems[i].TemporaryControl ? "* " : "", m_curveItems[i].Name), m_curveItems[i].Font, new SolidBrush(Color.WhiteSmoke), new PointF(32, ynameLocation));
                    }
                    Console.WriteLine(string.Format("*******头部区域发生一次重绘({0}ms)*********", (DateTime.Now - dt).TotalMilliseconds));
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
                float ynameLocation =  find.headBaseDistance - find.Font.Height / 2;
                g.Clip = new System.Drawing.Region(find.HeadRectangle);
                Color FColor = color;
                Color TColor = Color.WhiteSmoke;
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
            if (currentItem != null&&m_allowLeaveFresh)
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
        private bool isPartyRectFresh = false;
        private string m_PartyRectFreshID = "";
        private bool head_MouseIsDown = false;
        private int bak_channelNo = -1;
        private float m_DistanceBaseLine = 0;
        private bool m_allowLeaveFresh = true;
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
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
                        for (int i = headSelectItem.ChannelNo + 1; i < currentItem.ChannelNo + bak_channelNo; i++)
                        {
                            m_curveItems[i].ChannelNo -= 1;
                        }
                    }
                    else
                    {
                        for (int i = currentItem.ChannelNo + bak_channelNo; i < headSelectItem.ChannelNo; i++)
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
                        panel1.Invalidate(new System.Drawing.Region(currentItem.HeadRectangle));
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
            else
            {
                if (e.Button == MouseButtons.Right)
                {
                    for (int i = 0; i < m_curveItems.Count; i++)
                    {
                        if (m_curveItems[i].HeadRectangle.Contains(e.Location) && m_curveItems[i].Visible)
                        {
                            if (ChannelHeadPopupHandler != null)
                            {
                                ChannelHeadPopupHandler.BeginInvoke(m_curveItems[i].Clone(false), e.Button, null, null);
                            }
                            break;
                        }
                    }
                }
            }
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
                if (headSelectItem != null)
                    headSelectItem.isSelected = true;
                if (!m_isRealDataCurve)
                {
                    ChartAreaInvalidate(true, headSelectItem.belong == 0 ? 1 : 2);

                }
                reDrawHead(panel1.CreateGraphics(), Color.SkyBlue, e.Location, headSelectItem);
            }
            m_HeadTaskCompelet = false;
        }
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
            }
            else if (m_DistanceBaseLine == m_Height)
            {
                bak_curveArea.Visible = false;
                master_CurveArea.Height = m_Height;
                master_CurveArea.Visible = true;
            }
            else
            {
                master_CurveArea.Height = (int)m_DistanceBaseLine;
                master_CurveArea.Visible = true;
                bak_curveArea.Visible = true;
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
                    fontSize = fontSize > 16 ? 16 : fontSize - 2;
                    int lastIdx = 0;
                    m_curveItems.Sort((student1, student2) => (student1.ChannelNo - student2.ChannelNo));
                    int basetimespan0 = master_CurveArea.BaseTimeLineSpan * 1000, basetimespan1 = bak_curveArea.BaseTimeLineSpan * 1000;
                    for (int i = 0; i < m_curveItems.Count; i++)
                    {
                        m_curveItems[i].IsLastViewChannel = false;
                        if (!m_curveItems[i].Visible)
                            continue;
                        m_curveItems[i].Font = new Font("宋体", fontSize);
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
        #endregion
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
                return m_BaseTimeLineSpan;
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
                return m_moveTimeSpan;
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
            for (int i = 0; i < m_curveItems.Count; i++)
            {
                m_curveItems[i].Clear();
            }
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
                find.BindYDataValues(xyvalues.ToArray());
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
            return m_curveItems.Find(t => t.ChannelNum == channelNum);
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
                item.xAxis.MaxValue = m_BaseTimeLineSpan*1000;
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
                m_curveItems.Remove(item);
        }
        /// <summary>
        /// 移除指定通道
        /// </summary>
        /// <param name="item"></param>
        public void RemoveCurve(string ID)
        {
            m_curveItems.Remove(FindCurve(ID));
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
            return headSelectItem;
        }
        /// <summary>
        /// 主动刷新趋势图
        /// </summary>
        /// <param name="FrameCnt">当前帧序号</param>
        /// <param name="offsetTime">当前帧的偏移量</param>
        /// <returns>返回下一帧的帧序号</returns>
        public void Invalidate(DateTime startTime, int FrameCnt,int offsetTime, int TotalFrameCnt)
        {
            m_TotalFrameCnt = TotalFrameCnt;
            m_FrameCnt = FrameCnt;
            master_CurveArea.Invalidate(startTime, FrameCnt, offsetTime, m_TotalFrameCnt);
            bak_curveArea.Invalidate(startTime, FrameCnt, offsetTime, m_TotalFrameCnt);
            if (SleepStage.Length == 0)
            {
                SleepStage = new string[TotalFrameCnt];
                SleepPos=new int[TotalFrameCnt];

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
                this.Invoke(new MethodInvoker(() =>
                {
                    chartResize();
                }));
            }
            if (master_CurveArea.Visible && all != 2)
                master_CurveArea.AreaInvalidate(userRefresh);
            if (bak_curveArea.Visible && all != 1)
                bak_curveArea.AreaInvalidate(userRefresh);
            return headfresh;
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
        public bool HeadMouseWheel( CurveItem find , float pixelvalue)
        {
            if (find != null)
            {
                find.PixelRate = pixelvalue;
                find.yAxis.MaxValue = m_pRate * find.PixelRate;
                find.PixelConstants = find.PixelRate;
                if (m_isRealDataCurve && !m_fnstop)
                    return true;
                ChartAreaInvalidate(true, find.belong == 0 ? 1 : 2);
            }
            return false;
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public new void Dispose()
        {
            Clear();
            m_curveItems.Clear();
            master_CurveArea.Dispose();
            bak_curveArea.Dispose();
            GC.Collect();
            this.Dispose(true);
        }
        #endregion
    }
}
