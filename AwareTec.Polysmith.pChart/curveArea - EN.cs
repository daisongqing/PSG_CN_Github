using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.pChart
{
    public partial class curveArea : UserControl
    {
        #region 属性
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
        /// 当前帧序号
        /// </summary>
        private int m_FrameCnt = 1;
        private int m_TotalFrameCnt = 0;
        /// <summary>
        /// 一帧表示的时间间隔
        /// </summary>
        private int m_OneFrameSpanTime = 30;
        /// <summary>
        /// 移动偏移量
        /// </summary>
        private int m_FrameOffTime = 0;
        /// <summary>
        /// 备用移动偏移量
        /// </summary>
        private int bak_FrameOffTime = 0;
        private DateTime m_ViewStartTime, m_ViewEndTime;
        /// <summary>
        /// 是否是需要主动刷新
        /// </summary>
        private bool m_userRefresh = false;
        /// <summary>
        /// 存放源图
        /// </summary>
        private Image m_SourceImage = null;
        /// <summary>
        /// 移动增量s
        /// </summary>
        private int m_moveTimeSpan = 1;
        /// <summary>
        /// 选中区域开始的时间
        /// </summary>
        private DateTime m_RectStartTime = DateTime.Now;
        /// <summary>
        /// 选中区域结束的时间
        /// </summary>
        private DateTime m_RectEndTime = DateTime.Now;
        /// <summary>
        /// paint绘制是否完成
        /// </summary>
        private bool m_freshOk = true;
        /// <summary>
        /// 绘制区域
        /// </summary>
        private Rectangle m_rectangle = new Rectangle();
        /// <summary>
        /// 当前一瓶代表的时基字符描述
        /// </summary>
        private string m_strViewBaseTime = "";
        private ImageAttributes imageAtt = null;
        #endregion
        public curveArea()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);//解决闪烁
            this.SetStyle(ControlStyles.Opaque, true);//解决背景重绘问题(设置不绘制窗口背景，因为重绘窗口背景会导致性能底下)
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);//解决闪烁 
            this.UpdateStyles();
            xAxis = new XAxis() { BackColor = Color.WhiteSmoke, ForeColor = Color.Gray, AxisVisible = true, CalibrationsVisible = true, Displacement = m_Width, MaxValue = 30000 };
            yAxis = new YAxis() { BackColor = Color.WhiteSmoke, ForeColor = Color.Gray, AxisVisible = true, Displacement = m_Height };
            this.Load += curveArea_Load;
            BaseTimeLineSpan = 30;
            imageAtt = GetAlphaImgAttr(20);
        }

        #region 快捷键处理
        private bool m_HotKeyEnable = true;
        /// <summary>
        /// 热键使能
        /// </summary>
        public bool HotKeyEnable
        {
            set
            {
                m_HotKeyEnable = value;
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (m_HotKeyEnable)
            {
                if (DeleteOne(keyData))///删除
                {
                    return true;
                }
                string[] strkey = keyData.ToString().Split(',');
                HotKey matKey = new HotKey();
                for (int i = 0; i < strkey.Length; i++)
                {
                    string value = strkey[i].Trim();
                    if (value == "Shift")
                        matKey.ShiftEnable = true;
                    else if (value == "Control" || value == "Ctrl")
                        matKey.ControlEnable = true;
                    else if (value == "Alt")
                        matKey.AltEnable = true;
                    else
                        matKey.KeyCode = value;
                }
                if (m_IsSelectRectange && m_SelectRectangeView.Width != 0)///标记快捷功能实现
                {
                    if (doKeyDwon(matKey))
                        return true;
                }
                else if (m_ConnectMarkers.Length > 0)
                {
                    List<HotKey> hotKeys = getHotKeys();
                    HotKey hotkey = hotKeys.Find(t => t.ShiftEnable == matKey.ShiftEnable && t.AltEnable == matKey.AltEnable && t.ControlEnable == matKey.ControlEnable && t.KeyCode == matKey.KeyCode);
                    if (hotkey != null)
                    {
                        IMarker.MarkType markTyp = hotkey.MarkTyp;
                        {
                            if (m_ConnectMarkers[0].MarkTyp != markTyp)
                            {
                                if (MarkTypeChanged != null)
                                {
                                    if (MarkTypeChanged.Invoke(m_ConnectMarkers, markTyp, hotkey.NewMarkName))
                                    {
                                        AreaInvalidate(false);
                                        m_ConnectMarkers = new RectangleMarkers[0];
                                    }
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private bool DeleteOne(Keys key)
        {
            if (m_ConnectMarkers.Length > 0 && key == Keys.Delete)
            {
                if (MarkerMouseDoubleClick != null)
                {
                    try
                    {
                        m_SelectMark = m_ConnectMarkers[0];
                        if (m_SelectMark != null)
                        {
                            m_SelectMark.Delete = true;
                            string channelID = m_curveItems.Find(t => t.ChannelNum.ToString() == m_SelectMark.ID.Split(':')[0]).ID;
                            if (MarkerMouseDoubleClick.Invoke(channelID, m_SelectMark, null))
                            {
                                MarkFreshIAsyncResult(m_SelectMark);
                            }
                        }
                    }
                    catch
                    {
                    }
                    toolTip1.Hide(this);
                    toolTip1.RemoveAll();
                    m_SelectMarker = null;
                    m_SelectMark = null;
                    m_ConnectMarkers = new RectangleMarkers[0];
                    MouseIsDown = false;
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 执行快捷触发
        /// </summary>
        /// <param name="hotkey"></param>
        private bool doKeyDwon(HotKey matKey)
        {
            if (!m_IsSelectRectange)
                return false;
            List<HotKey> hotKeys = getHotKeys();
            HotKey hotkey = hotKeys.Find(t => t.ShiftEnable == matKey.ShiftEnable && t.AltEnable == matKey.AltEnable && t.ControlEnable == matKey.ControlEnable && t.KeyCode == matKey.KeyCode);
            if (hotkey != null)
            {
                IMarker.MarkType markTyp = hotkey.MarkTyp;
                if (SelectRectangleKeyDownHandler != null)
                {
                    CurveItem selectitem = m_curveItems.Find(t => t.ChannelNum == m_SelectRangchannelID && !t.IsCloneItem);
                    if (selectitem != null)
                    {
                        CurveItem find = selectitem.Clone(false);
                        RectangleMarkers rmk;
                        double edspan = (m_RectEndTime - m_StartTime).TotalMilliseconds;
                        int endidx = (int)(edspan / find.TimeSpan);
                        double stspan = (m_RectStartTime - m_StartTime).TotalMilliseconds;
                        int stidx = (int)(stspan / find.TimeSpan);
                        rmk = new RectangleMarkers() { StartIndex = stspan % find.TimeSpan == 0 ? stidx : stidx + 1, EndIndex = edspan % find.TimeSpan == 0 ? endidx : endidx + 1 };
                        if (rmk.EndIndex < rmk.StartIndex)
                        {
                            int sk = rmk.EndIndex;
                            rmk.EndIndex = rmk.StartIndex;
                            rmk.StartIndex = sk;
                        }
                        rmk.StartTime = m_StartTime.AddMilliseconds(rmk.StartIndex * find.TimeSpan);
                        rmk.EndTime = m_StartTime.AddMilliseconds(rmk.EndIndex * find.TimeSpan);
                        rmk.ID = string.Format("{0}:{1}-{2}", find.ChannelNum, rmk.StartIndex, rmk.EndIndex);
                        rmk.MarkCreatTime = rmk.StartTime;
                        rmk.MarkTyp = markTyp;
                        rmk.Name = hotkey.NewMarkName;
                        int ts = (int)(rmk.StartTime - m_StartTime).TotalMilliseconds;
                        int fcnt = ts / 30000;
                        fcnt = ts % 30000 == 0 ? fcnt : fcnt + 1;
                        rmk.StartFrameNo = fcnt;
                        ts = (int)(rmk.EndTime - m_StartTime).TotalMilliseconds;
                        int fcnt2 = ts / 30000;
                        fcnt2 = ts % 30000 == 0 ? fcnt2 : fcnt2 + 1;
                        rmk.EndFrameNo = fcnt2;
                        rmk.RectanglePoints = selectitem.GetRectangeleValues(rmk.StartIndex, rmk.EndIndex).yDataValues.ToList();
                        find.removeMarks.AddRange(m_currentItem.CurrentMarks.FindAll(delegate (IMarker t)
                        {
                            RectangleMarkers rec = t as RectangleMarkers;

                            return rmk.StartIndex <= rec.EndIndex && rmk.EndIndex >= rec.StartIndex && !rec.ReadOnly && t.ID != rmk.ID;
                        }));
                        find.CurrentMarks.Add(rmk);
                        if (SelectRectangleKeyDownHandler.Invoke(selectitem.ID, find))
                        {
                            MouseIsDown = false;
                        }
                    }
                }
                return true;
            }
            return false;
        }
        #endregion
        private void curveArea_Resize(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            m_Width = this.Width;
            m_Height = this.Height;
            ///xy坐标需要重新界定
            xAxis.Displacement = m_Width;
            yAxis.Displacement = m_Height;
            m_rectangle = new Rectangle(new Point(0, 0), new Size(m_Width, m_Height));
            bak_mousePoint = new Point(-10, 0);
            xScreenRate = ScaleX;
            yScreenRate = ScaleY;
            AreaInvalidate(true);
            Console.WriteLine(string.Format("*******趋势图{0}容器发生一次大小变化，耗时[{1}ms]*********", Belong == 0 ? "主" : "副", (DateTime.Now - dt).TotalMilliseconds));
        }

        private void curveArea_Load(object sender, EventArgs e)
        {
            this.Resize += curveArea_Resize;
            this.panel2.Paint += view_Paint;
            this.panel2.MouseLeave += view_MouseLeave;
            this.panel2.MouseDown += view_MouseDown;
            this.panel2.MouseMove += view_MouseMove;
            this.panel2.MouseUp += view_MouseUp;
            if (!m_isRealDataCurve)
            {
                m_MoveTimer = new System.Timers.Timer();
                m_MoveTimer.Elapsed += m_MoveTimer_Elapsed;
            }
        }
        #region 曲线部分效果
        private System.Timers.Timer m_MoveTimer = null;
        private void m_MoveTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (m_freshOk)
            {
                m_MoveTimer.Interval = 200;
                int with2 = panel2.Width;
                if ((m_FrameRightMove && m_FrameCnt < m_TotalFrameCnt) && !(m_FrameCnt == m_TotalFrameCnt - 1 && m_FrameOffTime == 30) || (m_FrameLeftMove && m_FrameCnt > 0) && !(m_FrameCnt == 1 && m_FrameOffTime == 0))
                {
                    int add = m_FrameRightMove ? m_moveTimeSpan : m_FrameLeftMove ? -m_moveTimeSpan : 0;
                    m_FrameOffTime += add;
                    if (m_FrameOffTime > m_OneFrameSpanTime)
                    {
                        m_FrameCnt++;
                        m_FrameOffTime = m_FrameOffTime - m_OneFrameSpanTime;
                    }
                    else if (m_FrameOffTime < 0)
                    {
                        m_FrameCnt--;
                        m_FrameOffTime += m_OneFrameSpanTime;
                    }
                    if (Cursor == Cursors.SizeWE)
                    {
                        m_MoveOffset += (int)(add * xAxis.ValueRate * 1000);
                        float millsec = (m_MoveOffset + ((m_FrameRightMove ? m_Width : 0) - m_DragStartPoint.X)) / xAxis.ValueRate;
                        int offIndex = (int)(millsec / m_currentItem.TimeSpan);
                        DateTime TimeNow = m_DragStartTime.AddMilliseconds(millsec);
                        int IndexNow = m_DragStartIndex + offIndex;
                        Console.WriteLine(string.Format("总共偏移距离-【{0}】 偏移豪秒-【{1}】 偏移索引量-【{2}】", m_MoveOffset, millsec, offIndex));
                        if (m_HandSelect == 1)
                        {
                            int stidx = m_SelectMarker.EndIndex - 1000 / m_currentItem.TimeSpan;
                            if (IndexNow > stidx)
                            {
                                IndexNow = stidx;
                                TimeNow = m_SelectMarker.EndTime.AddMilliseconds(-1000);
                            }
                            else if (IndexNow == stidx && m_SelectMarker.StartIndex == IndexNow)
                            {
                                m_MoveTimer.Stop();
                                return;
                            }
                        }
                        else if (m_HandSelect == 2)
                        {
                            int edidx = m_SelectMarker.StartIndex + 1000 / m_currentItem.TimeSpan;
                            if (IndexNow < edidx)
                            {
                                IndexNow = edidx;
                                TimeNow = m_SelectMarker.StartTime.AddMilliseconds(1000);
                            }
                            else if (IndexNow == edidx && m_SelectMarker.EndIndex == IndexNow)
                            {
                                m_MoveTimer.Stop();
                                return;
                            }
                        }
                        for (int i = 0; i < m_ConnectMarkers.Length; i++)
                        {
                            RectangleMarkers rmark = m_ConnectMarkers[i];
                            if (rmark != null)
                            {
                                if (m_HandSelect == 1)
                                {
                                    rmark.MarkCreatTime = rmark.StartTime = TimeNow;
                                    rmark.StartIndex = IndexNow;
                                }
                                else if (m_HandSelect == 2)
                                {
                                    rmark.EndTime = TimeNow;
                                    rmark.EndIndex = IndexNow;
                                }
                            }
                        }
                    }
                    else
                    {
                        m_RectEndTime = m_RectEndTime.AddSeconds(add);
                        captainChange(new CaptainInfoUion() { ViewStartTime = m_ViewStartTime, ViewEndTime = m_ViewEndTime, RectStartTime = m_RectStartTime, RectEndTime = m_RectEndTime, showTyp = 2, CurrentFrameNo = m_FrameCnt });
                        int moveDistance = (int)Math.Abs((add * 1000 * xAxis.ValueRate));//移动位移
                        if (m_FrameRightMove)
                        {
                            if (m_RectStartTime >= m_ViewEndTime)
                            {
                                m_NewMouseRect.X = with2;
                                m_NewMouseRect.Width = 0;
                            }
                            else
                            {
                                if (m_FrameCnt == m_TotalFrameCnt)
                                {
                                    m_NewMouseRect.X = m_SatrtMouseRect.X;
                                    m_NewMouseRect.Width = m_SatrtMouseRect.X;
                                }
                                else
                                {
                                    m_NewMouseRect.X = m_NewMouseRect.X - moveDistance;
                                    m_NewMouseRect.Width += moveDistance;
                                    if (m_NewMouseRect.X < 0)
                                    {
                                        m_NewMouseRect.X = 0;
                                        m_NewMouseRect.Width = with2;
                                    }
                                }
                            }
                        }
                        else if (m_FrameLeftMove)
                        {
                            if (m_RectStartTime <= m_ViewStartTime)
                            {
                                m_NewMouseRect.X = 0;
                                m_NewMouseRect.Width = 1;
                            }
                            else
                            {
                                if (m_FrameCnt == 1)
                                {
                                    m_NewMouseRect.X = m_SatrtMouseRect.X;
                                    m_NewMouseRect.Width = 0 - m_SatrtMouseRect.X;
                                }
                                else
                                {
                                    m_NewMouseRect.X = m_NewMouseRect.X + moveDistance;
                                    m_NewMouseRect.Width = 0 - m_NewMouseRect.X;
                                    if (m_NewMouseRect.X > with2)
                                    {
                                        m_NewMouseRect.X = with2;
                                        m_NewMouseRect.Width = 0 - with2;
                                    }
                                }
                            }
                        }
                        while (!m_freshOk)
                        {
                            System.Threading.Thread.Sleep(5);
                        }
                        DrawRectangle(m_NewMouseRect, false);
                    }
                    m_userRefresh = true;
                    panel2.Invalidate();
                    Console.WriteLine(string.Format("{0},{1}({2}*{3})", m_NewMouseRect.X, m_NewMouseRect.Y, m_NewMouseRect.Width, m_NewMouseRect.Height));
                    #region 整帧翻屏写法（注释掉）
                    //int add = m_FrameRightMove ? 1 : m_FrameLeftMove ? 0 - 1 : 0;
                    //m_FrameCnt += add;
                    //this.panel2.Invalidate();
                    //m_RectEndTime = m_RectEndTime.AddSeconds(add * 30);
                    //panel3.Invalidate();
                    //if (m_FrameCnt != bak_frameCnt)
                    //{
                    //    m_NewMouseRect.X = m_FrameCnt - bak_frameCnt > 0 ? 0 : m_Width - 1;
                    //    m_NewMouseRect.Width = 0;
                    //    ResizeToRectangle(new Point(0, 0), ref m_NewMouseRect);
                    //}
                    //else
                    //{
                    //    MouseRect = m_SatrtMouseRect;
                    //}
                    #endregion
                }
            }
            else
            {
                m_MoveTimer.Interval = 50;
            }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        private DateTime m_StartTime = DateTime.Now;

        public DateTime StartTime
        {
            get
            {
                return m_StartTime;
            }
        }

        private bool m_NewMouseRectVisible = false;
        ///// <summary>
        ///// 存放定标数据的图片
        ///// </summary>
        //private Image m_CalibrationImage = null;
        ///// <summary>
        ///// 存放分期和体位数据的图片
        ///// </summary>
        //private Image m_StageImage = null;
        ///// <summary>
        ///// 存放定标数据的图片
        ///// </summary>
        //private Image m_CalibrationImage = null;
        //private Image m_RectMarksImage = null;
        //private Image m_StrMarksImage = null;
        private Image m_AllIamge = null;
        private int m_m_LockFreshCnt = 0;
        private bool m_IsSelectRectange = false;
        /// <summary>
        /// 重绘时发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void view_Paint(object sender, PaintEventArgs e)
        {
            if (m_isRealDataCurve && m_LockFresh)
                m_userRefresh = false;

            DateTime ddt = DateTime.Now;
            m_freshOk = false;
            //if (e.ClipRectangle.Height <= panel2.Height - 10 && m_hasLine && !m_userRefresh && m_SourceImage != null)
            //{
            //    if (m_isRealDataCurve)
            //        return;
            //    AreaInvalidate(false);
            //    return;
            //}
            try
            {
                m_curveItems = getAllItems();
                if (m_userRefresh)
                {
                    ///主动向用户获取数据
                    if (!m_isRealDataCurve)
                    {
                        int viewStartTime = ((m_FrameCnt - 1) * m_OneFrameSpanTime + m_FrameOffTime);
                        m_ViewStartTime = m_StartTime.AddSeconds(viewStartTime);///显示区域开始时间
                        m_ViewEndTime = m_ViewStartTime.AddSeconds(m_BaseTimeLineSpan);///显示区域结束时间
                        DateTime st = m_ViewStartTime.AddSeconds(-5);
                        if (st < m_StartTime)
                            st = m_StartTime;
                        //ReadChannelData(st, m_ViewEndTime, m_curveItems.Select(t=>t.ChannelNum).ToList());
                        ReadChannelData(st, m_ViewEndTime, m_curveItems);
                    }
                    m_SourceImage = GetMap();
                }
                if (m_SourceImage != null)
                {
                    ///drawimage
                    e.Graphics.DrawImage(m_SourceImage, 0, 0);
                    if (m_isRealDataCurve)
                        e.Graphics.DrawImage(RealTimeDrawCalibrationMarks(), 0, 0);
                    else
                    {
                        e.Graphics.DrawImage(DrawCalibrationMarks(), 0, 0);
                        e.Graphics.DrawImage(DrawStage(), 0, 0);
                        e.Graphics.DrawImage(DrawMarkers(m_SourceImage), 0, 0);
                        e.Graphics.DrawImage(DrawFlags(), 0, 0);
                        if (m_VedioTimeLineX > 0)
                        {
                            using (Pen p = new Pen(Color.Red, 2))
                            {
                                p.DashStyle = DashStyle.Dash;
                                p.DashPattern = new float[] { 5, 5 };
                                e.Graphics.DrawLine(p, new PointF(m_VedioTimeLineX, 0), new PointF(m_VedioTimeLineX, Height));
                            }
                        }
                    }
                    if (Belong == 1)
                    {
                        using (Pen pen = new Pen(Color.DarkBlue, 3))
                            e.Graphics.DrawLine(pen, new Point(0, 0), new Point(this.Width, 0));
                    }
                    xScreenRate = ScaleX;
                    //xScreenRate = (xScreenRate == 125 ? 120 : xScreenRate == 150 ? 144 : xScreenRate == 200 ? 192 : 96) / 96f;
                    yScreenRate = ScaleY;
                    //yScreenRate = (yScreenRate == 125 ? 120 : yScreenRate == 150 ? 144 : yScreenRate == 200 ? 192 : 96) / 96f;
                    ///bitblt
                    //DrawImage(m_SourceImage, 0, 0, e.Graphics, 0, 0);
                    //if (m_isRealDataCurve)
                    //   DrawImage(RealTimeDrawCalibrationMarks(), 0, 0, e.Graphics, 0, 0);
                    //else
                    //{
                    //    DrawImage(DrawCalibrationMarks(), 0, 0, e.Graphics, 0, 0);
                    //    DrawImage(DrawStage(), 0, 0, e.Graphics, 0, 0);
                    //    DrawImage(DrawMarkers(m_SourceImage), 0, 0, e.Graphics, 0, 0);
                    //    DrawImage(DrawFlags(), 0, 0, e.Graphics, 0, 0);
                    //}
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
            m_freshOk = true;
            System.Diagnostics.Trace.WriteLine(string.Format("趋势图{1}容器执行一次曲线绘制，耗时[{0}ms]", (DateTime.Now - ddt).TotalMilliseconds, Belong == 0 ? "主" : "副"));
            m_userRefresh = false;
        }
        #region Win32 API
        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr ptr);
        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(
       IntPtr hdc, // handle to DC  
       int nIndex // index of capability  
       );
        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);
        #endregion
        #region DeviceCaps常量
        const int HORZRES = 8;
        const int VERTRES = 10;
        const int LOGPIXELSX = 88;
        const int LOGPIXELSY = 90;
        const int DESKTOPVERTRES = 117;
        const int DESKTOPHORZRES = 118;
        #endregion  
        /// <summary>  
        /// 获取宽度缩放百分比  
        /// </summary>  
        private float ScaleX
        {
            get
            {
                IntPtr hdc = GetDC(IntPtr.Zero);
                float ScaleX = (float)GetDeviceCaps(hdc, DESKTOPHORZRES) / (float)GetDeviceCaps(hdc, HORZRES);
                ReleaseDC(IntPtr.Zero, hdc);
                return ScaleX;
            }
        }
        /// <summary>  
        /// 获取高度缩放百分比  
        /// </summary>  
        private static float ScaleY
        {
            get
            {
                IntPtr hdc = GetDC(IntPtr.Zero);
                float ScaleY = (float)(float)GetDeviceCaps(hdc, DESKTOPVERTRES) / (float)GetDeviceCaps(hdc, VERTRES);
                ReleaseDC(IntPtr.Zero, hdc);
                return ScaleY;
            }
        }
        bool MouseIsDown = false;
        /// <summary>
        /// 当前帧用于绘制选中区域的对象
        /// </summary>
        //   Rectangle MouseRect = Rectangle.Empty;
        /// <summary>
        /// 翻页后用于绘制选中区域的对象
        /// </summary>
        private Rectangle m_NewMouseRect = new Rectangle(0, 0, 0, 0);
        /// <summary>
        /// 记录初始位置
        /// </summary>
        private Rectangle m_SatrtMouseRect = Rectangle.Empty;
        /// <summary>
        /// 当前被选中的标记
        /// </summary>
        private IMarker m_SelectMark = null;
        private bool m_TimeLineEnable = true;
        private bool m_hasLine = false;

        public delegate void MouseUpChangeFrameDelegate(int endFrameNo,int moveOffTime);
        /// <summary>
        /// 拖拉标记事件松开鼠标后，进度条帧同步
        /// </summary>
        public event MouseUpChangeFrameDelegate MouseUpChangeFrameHandle;

        private void view_MouseUp(object sender, MouseEventArgs e)
        {
            panel2.Capture = false;
            Cursor.Clip = Rectangle.Empty;
            bool selectMark = false;
            if (!m_isRealDataCurve)
            {
                m_MoveTimer.Stop();
                m_MoveOffset = 0;
                videoStatusChange(true, m_RectEndTime);
                if (m_SelectMarker != null && MouseIsDown)
                {
                    MouseIsDown = false;
                    m_hasLine = false;
                    m_MoveOffset = 0;
                    m_DragStartPoint = Point.Empty;
                    try
                    {

                        CurveItem find = m_currentItem.Clone(false);
                        find.IsMarkerChanged = true;
                        find.ChannelNum = m_currentItem.ChannelNum;
                        for (int i = 0; i < m_ConnectMarkers.Length; i++)
                        {
                            if (m_ConnectMarkers[i] != null)
                            {
                                RectangleMarkers rmk = new RectangleMarkers();
                                rmk.MarkCreatTime = m_ConnectMarkers[i].MarkCreatTime;
                                rmk.StartTime = m_ConnectMarkers[i].StartTime;
                                rmk.EndTime = m_ConnectMarkers[i].EndTime;
                                rmk.StartIndex = m_ConnectMarkers[i].StartIndex;
                                rmk.EndIndex = m_ConnectMarkers[i].EndIndex;
                                rmk.ID = m_ConnectMarkers[i].ID;
                                rmk.Name = m_ConnectMarkers[i].Name;
                                rmk.MarkTyp = m_ConnectMarkers[i].MarkTyp;
                                int ts = (int)(rmk.StartTime - m_StartTime).TotalMilliseconds;
                                int fcnt = ts / 30000;
                                fcnt = ts % 30000 == 0 ? fcnt : fcnt + 1;
                                rmk.StartFrameNo = fcnt;
                                ts = (int)(rmk.EndTime - m_StartTime).TotalMilliseconds;
                                int fcnt2 = ts / 30000;
                                fcnt2 = ts % 30000 == 0 ? fcnt2 : fcnt2 + 1;
                                rmk.EndFrameNo = fcnt2;
                                rmk.Comments = m_ConnectMarkers[i].Comments;
                                find.CurrentMarks.Add(rmk);
                                m_ConnectMarkers[i].ID = string.Format("{0}:{1}-{2}", m_ConnectMarkers[i].ID.Split(':')[0], m_ConnectMarkers[i].StartIndex, m_ConnectMarkers[i].EndIndex);
                                m_ConnectMarkers[i].StartFrameNo = rmk.StartFrameNo;
                                m_ConnectMarkers[i].EndFrameNo = rmk.EndFrameNo;
                            }
                        }
                        find.removeMarks.AddRange(m_currentItem.CurrentMarks.FindAll(delegate (IMarker t)
                        {
                            RectangleMarkers rec = t as RectangleMarkers;
                            return m_SelectMarker.StartIndex <= rec.EndIndex && m_SelectMarker.EndIndex >= rec.StartIndex && !rec.ReadOnly && t.ID != m_SelectMarker.ID;
                        }));
                        ChannelViewPopupHandler.BeginInvoke(find, e.Button, null, null);
                    }
                    catch { }
                    if (MouseUpChangeFrameHandle != null)
                        MouseUpChangeFrameHandle.Invoke(m_FrameCnt, m_FrameOffTime);
                    m_ConnectMarkers = new RectangleMarkers[0];
                    //m_currentItem = null;
                    m_SelectMarker = null;
                    return;
                }
                if (m_SelectMark != null)
                {
                    toolTip1.Hide(this);
                    toolTip1.RemoveAll();
                    ///此处本是要主动更新panel2的 但是在debug模式下tooltips在消失的时候会自动更新
#if !Debug
                    AreaInvalidate(false);
#endif
                    if (MouseIsDown)
                    {
                        MouseIsDown = false;
                        m_NewMouseRect = Rectangle.Empty;
                    }
                    if (m_SelectMark is StringMarkers)
                        m_SelectMark.isSelected = false;
                    m_SelectMark = null;
                    selectMark = true;
                    return;
                }
                else
                {
                    if (m_hasLine)
                    {
                        if (!m_isRealDataCurve)
                            captainChange(new CaptainInfoUion() { ViewStartTime = m_ViewStartTime, ViewEndTime = m_ViewEndTime, RectStartTime = m_RectStartTime, RectEndTime = m_RectEndTime, showTyp = 0, CurrentFrameNo = m_FrameCnt });
                        bak_mousePoint = new Point(-10, 0);
                        m_hasLine = false;
                        panel2.Invalidate();
                    }
                }
            }
            else
            {
                m_freshOk = true;
                m_LockFresh = false;
            }
            CurveItem item = m_curveItems.Find(t => t.ClientRectangle.Contains(MouseIsDown ? m_SatrtMouseRect.Location : e.Location) && t.Visible);
            if (item != null)
            {
                CurveItem find = item.Clone(false);
                if (e.Button == MouseButtons.Left && m_NewMouseRect.Width != 0 && MouseIsDown)
                {
                    if (ChannelViewPopupHandler != null)
                    {
                        RectangleMarkers rmk;
                        if (m_isRealDataCurve)
                        {
                            List<float> datas = new List<float>();
                            ///获取裁剪区域的坐标点
                            for (int i = 0; i < find.ClientRectanglePoints.Count; i++)
                            {
                                if (m_NewMouseRect.X <= find.ClientRectanglePoints[i].X && m_NewMouseRect.Right >= find.ClientRectanglePoints[i].X)
                                {
                                    datas.Add(find.ClientRectanglePoints[i].Y);
                                }
                            }
                            rmk = new RectangleMarkers() { RectanglePoints = datas };
                        }
                        else
                        {
                            //如果是初筛的数据 查看回放时 选择全呼吸 在脑电通道操作会出错 这里做限制
                            int totalPointlen = find.ClientRectanglePoints.Count;
                            if (totalPointlen == 0)
                                return;
                            ///框选区域的最边界超出数据显示范围时，不予响应操作   由于正反方向都可以拖动 所以left和right都限制
                            //if (find.ClientRectanglePoints[totalPointlen - 1].X < m_NewMouseRect.Right || find.ClientRectanglePoints[totalPointlen - 1].X < m_NewMouseRect.Left)
                            //    return;
                            double edspan = (m_RectEndTime - m_StartTime).TotalMilliseconds;
                            int endidx = (int)(edspan / find.TimeSpan);
                            double stspan = (m_RectStartTime - m_StartTime).TotalMilliseconds;
                            int stidx = (int)(stspan / find.TimeSpan);
                            rmk = new RectangleMarkers() { StartIndex = stspan % find.TimeSpan == 0 ? stidx : stidx + 1, EndIndex = edspan % find.TimeSpan == 0 ? endidx : endidx + 1 };
                            if (rmk.EndIndex < rmk.StartIndex)
                            {
                                int sk = rmk.EndIndex;
                                rmk.EndIndex = rmk.StartIndex;
                                rmk.StartIndex = sk;
                            }
                            rmk.StartTime = m_StartTime.AddMilliseconds(rmk.StartIndex * find.TimeSpan);
                            rmk.EndTime = m_StartTime.AddMilliseconds(rmk.EndIndex * find.TimeSpan);
                            rmk.ID = string.Format("{0}:{1}-{2}", find.ChannelNum, rmk.StartIndex, rmk.EndIndex);
                            rmk.MarkCreatTime = rmk.StartTime;
                            int ts = (int)(rmk.StartTime - m_StartTime).TotalMilliseconds;
                            int fcnt = ts / 30000;
                            fcnt = ts % 30000 == 0 ? fcnt : fcnt + 1;
                            rmk.StartFrameNo = fcnt;
                            ts = (int)(rmk.EndTime - m_StartTime).TotalMilliseconds;
                            int fcnt2 = ts / 30000;
                            fcnt2 = ts % 30000 == 0 ? fcnt2 : fcnt2 + 1;
                            rmk.EndFrameNo = fcnt2;
                            rmk.RectanglePoints = item.GetRectangeleValues(rmk.StartIndex, rmk.EndIndex).yDataValues.ToList();
                            find.removeMarks.AddRange(item.CurrentMarks.FindAll(delegate (IMarker t)
                            {
                                RectangleMarkers rec = t as RectangleMarkers;
                                return rmk.StartIndex <= rec.EndIndex && rmk.EndIndex >= rec.StartIndex && !rec.ReadOnly;
                            }));
                            //todo 还有问题
                            //bak_frameCnt = m_FrameCnt;
                            bak_FrameOffTime = m_FrameOffTime;
                            if (MouseUpChangeFrameHandle != null)
                                MouseUpChangeFrameHandle.Invoke(m_FrameCnt, m_FrameOffTime);
                        }
                        find.CurrentMarks.Add(rmk);
                        m_TimeLineEnable = false;
                        ChannelViewPopupHandler.Invoke(find, e.Button);

                    }
                }
                else if (e.Button == MouseButtons.Right && m_NewMouseRect.Width == 0 && !m_isRealDataCurve && !selectMark)
                {
                    int totalPointlen = find.ClientRectanglePoints.Count;
                    if (totalPointlen == 0)
                        return;
                    ///框选区域的最边界超出数据显示范围时，不予响应操作 
                    if (find.ClientRectanglePoints[totalPointlen - 1].X < e.X)
                        return;
                    StringMarkers sm = new StringMarkers();
                    float add = e.X / xAxis.ValueRate;
                    sm.MarkCreatTime = m_ViewStartTime.AddMilliseconds(add);
                    sm.ID = string.Format("255:{0}", sm.MarkCreatTime.ToOADate());
                    sm.Location = e.Location;
                    int ts = (int)(sm.MarkCreatTime - m_StartTime).TotalMilliseconds;
                    int fcnt = ts / 30000;
                    fcnt = ts % 30000 == 0 ? fcnt : fcnt + 1;
                    sm.StartFrameNo = fcnt;
                    sm.EndFrameNo = fcnt;
                    find.CurrentMarks.Add(sm);
                    m_TimeLineEnable = false;
                    ChannelViewPopupHandler.Invoke(find, e.Button);
                }
                UpdateViewIAsyncResult();
            }
            if (e.Button == MouseButtons.Left)
            {
                MouseIsDown = false;
                m_NewMouseRect = Rectangle.Empty;
            }
            //m_currentItem = null;
        }
        /// <summary>
        /// 是否需要显示时间轴刻度
        /// </summary>
        private bool m_TimeLineVisible = false;
        private Point bak_mousePoint = new Point(-10, 0);
        /// <summary>
        /// 备份当前帧
        /// </summary>
        private int bak_frameCnt = 1;
        private bool m_FrameRightMove = false;
        private bool m_FrameLeftMove = false;
        private RectangleMarkers m_SelectMarker = null;
        private RectangleMarkers[] m_ConnectMarkers = new RectangleMarkers[0];
        private Point m_DragStartPoint = Point.Empty;
        /// <summary>
        /// 选择拖拽的边界线是？ 1-左边界 2-右边界
        /// </summary>
        private int m_HandSelect = 0;
        private DateTime m_DragStartTime = default(DateTime);
        private int m_DragStartIndex = 0;
        private int m_MoveOffset = 0;
        private int m_nowLoctionX = 0;
        /// <summary>
        /// 光标在panel2移动的累积位移量
        /// </summary>
        private int m_MoveAdd = 0;
        private void view_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_hasDialog)
                return;
            bool isContains = this.ClientRectangle.Contains(e.X, e.Y);
            if (MouseIsDown && e.Button == MouseButtons.Left)
            {
                if (m_currentItem == null)
                {
                    return;
                }
                int cursorLocationx = e.X > m_currentItem.EndViewX ? m_currentItem.EndViewX : e.X;
                #region 框选范围的拖拉效果
                if (Cursor == Cursors.SizeWE && m_SelectMarker != null)
                {
                    m_FrameLeftMove = e.X <= 2;
                    m_FrameRightMove = e.X >= panel2.Width - 2;
                    if (m_FrameLeftMove || m_FrameRightMove)
                    {
                        if (!m_MoveTimer.Enabled && (m_SelectMarker.EndTime - m_SelectMarker.StartTime).TotalSeconds != 1)
                        {
                            Console.WriteLine(string.Format("{0}-{1}({2},{3})", m_FrameLeftMove, m_FrameRightMove, e.X, e.Y));
                            m_MoveTimer.Interval = 10;
                            m_MoveTimer.Start();//开始滚动
                            bak_frameCnt = m_FrameCnt;
                            m_FrameRightMove = e.X >= panel2.Width - 2 ? true : false;
                        }
                        return;
                    }
                    if (m_MoveTimer != null)
                    {
                        if (m_MoveTimer.Enabled && m_FrameCnt == bak_frameCnt)
                        {
                            m_MoveTimer.Stop();
                        }
                    }
                    int offsetx = (cursorLocationx - m_nowLoctionX);
                    m_nowLoctionX = cursorLocationx;
                    if (m_MoveOffset != 0 && cursorLocationx > 0 && cursorLocationx < panel2.Width)
                    {
                        int bak_value = m_MoveOffset;
                        m_MoveOffset += offsetx;
                        if (m_MoveOffset * bak_value < 0)
                        {
                            m_MoveOffset = 0;
                        }
                        ///m_nowLoctionX代表的是当前光标的位置，当 m_MoveOffset在不清零的时候，m_nowLoctionX在增减的同时，m_MoveOffset也在增减，那么在millsec的计算就时就叠加了
                        if (m_MoveOffset != 0)
                        {
                            m_MoveAdd += offsetx;
                            m_nowLoctionX += m_MoveAdd;
                        }
                    }
                    if (m_nowLoctionX < 0)
                    {
                        m_nowLoctionX = 0;
                    }
                    else if (m_nowLoctionX > panel2.Width)
                    {
                        m_nowLoctionX = panel2.Width;
                    }
                    float millsec = (int)(m_nowLoctionX + m_MoveOffset - m_DragStartPoint.X) / xAxis.ValueRate;
                    int offIndex = (int)(millsec / m_currentItem.TimeSpan);
                    DateTime TimeNow = m_DragStartTime.AddMilliseconds(millsec);
                    int IndexNow = m_DragStartIndex + offIndex;
                    float limitvalue = 1000;///单位ms
                    HotKey hotKey = getHotKeys().Find(t => t.MarkTyp == m_SelectMarker.MarkTyp);
                    if (hotKey != null)
                    {
                        limitvalue = hotKey.MinLimitRange * 1000;///时间阈值是s所以需要转换成ms
                    }
                    if (m_HandSelect == 1)
                    {
                        int stidx = m_SelectMarker.EndIndex - (int)(limitvalue / m_currentItem.TimeSpan);
                        if (IndexNow > stidx)
                        {
                            IndexNow = stidx;
                            TimeNow = m_SelectMarker.EndTime.AddMilliseconds(-limitvalue);
                        }
                        else if (IndexNow == stidx && m_SelectMarker.StartIndex == IndexNow)
                        {
                            m_MoveTimer.Stop();
                            return;
                        }
                    }
                    else if (m_HandSelect == 2)
                    {
                        int edidx = m_SelectMarker.StartIndex + (int)(limitvalue / m_currentItem.TimeSpan);
                        if (IndexNow < edidx)
                        {
                            IndexNow = edidx;
                            TimeNow = m_SelectMarker.StartTime.AddMilliseconds(limitvalue);
                        }
                        else if (IndexNow == edidx && m_SelectMarker.EndIndex == IndexNow)
                        {
                            m_MoveTimer.Stop();
                            return;
                        }
                    }
                    for (int i = 0; i < m_ConnectMarkers.Length; i++)
                    {
                        RectangleMarkers rmark = m_ConnectMarkers[i];
                        if (rmark == null)
                            continue;
                        if (m_HandSelect == 1)
                        {
                            rmark.MarkCreatTime = rmark.StartTime = TimeNow;
                            rmark.StartIndex = IndexNow;
                        }
                        else if (m_HandSelect == 2)
                        {
                            rmark.EndTime = TimeNow;
                            rmark.EndIndex = IndexNow;
                        }
                    }
                    panel2.Invalidate();
                    return;
                }
                #endregion
                if (m_currentItem == null)
                    return;
                if (!m_currentItem.IsDraw)
                    return;
                ResizeToRectangle(e.Location, ref m_NewMouseRect);
                m_NewMouseRectVisible = true;
                if (!m_isRealDataCurve)
                {
                    ///区域框当前时间
                    m_RectEndTime = m_ViewStartTime.AddMilliseconds(cursorLocationx / xAxis.ValueRate);
                    if (m_RectEndTime <= m_RectMouseDownViewEndTime && bak_frameCnt < m_FrameCnt)
                    {
                        m_FrameCnt = bak_frameCnt;
                        m_FrameOffTime = bak_FrameOffTime;
                        m_NewMouseRect.X = m_SatrtMouseRect.X;
                        m_NewMouseRect.Width = 0 - m_SatrtMouseRect.X;
                        AreaInvalidate(true);
                    }
                    float m_RealTimeFrequency = -1;
                    if (m_currentItem != null)
                    {
                        try
                        {
                            if (m_currentItem.TimeSpan < 100)
                            {
                                var selectPoints = m_currentItem.ClientRectanglePoints.Where(t => t.X >= m_SelectRectangeView.X && t.X <= m_SelectRectangeView.Right);
                                if (selectPoints.Count() > 0)
                                {
                                    int cnt = 0;
                                    List<float> yvalues = selectPoints.Select(t => t.Y).ToList();
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
                                    m_RealTimeFrequency = cnt * 500.0f / (yvalues.Count * m_currentItem.TimeSpan);
                                }
                            }
                        }
                        catch { }
                    }
                    captainChange(new CaptainInfoUion() { ViewStartTime = m_ViewStartTime, ViewEndTime = m_ViewEndTime, RectStartTime = m_RectStartTime, RectEndTime = m_RectEndTime, showTyp = 2, CurrentFrameNo = m_FrameCnt, Comments = m_RealTimeFrequency != -1 ? string.Format(" [{0:f2}Hz]", m_RealTimeFrequency) : "" });
                    m_FrameLeftMove = e.X <= 2;
                    m_FrameRightMove = e.X >= panel2.Width - 2;
                    if (m_FrameLeftMove || m_FrameRightMove)
                    {
                        if (!m_MoveTimer.Enabled)
                        {
                            Console.WriteLine(string.Format("{0}-{1}({2},{3})", m_FrameLeftMove, m_FrameRightMove, e.X, e.Y));
                            m_MoveTimer.Interval = 10;
                            m_MoveTimer.Start();//开始滚动
                            bak_frameCnt = m_FrameCnt;
                            m_FrameRightMove = e.X >= panel2.Width - 2 ? true : false;
                        }
                    }
                    else
                    {
                        if (m_MoveTimer != null)
                        {
                            if (m_MoveTimer.Enabled && m_FrameCnt == bak_frameCnt)
                            {
                                m_MoveTimer.Stop();
                            }
                        }
                    }
                }
            }
            if (isContains)
            {
                if (!MouseIsDown && m_markEnable)
                {
                    m_currentItem = m_curveItems.Find(t => t.Visible && t.ClientRectangle.Contains(e.Location));
                    if (!m_isRealDataCurve)
                    {
                        ///区域框开始时间
                        float add = e.X / xAxis.ValueRate;
                        if (m_currentItem != null)
                        {
                            ///把要显示的标记按照起始索引排序
                            IMarker[] marks = m_currentItem.CurrentMarks.Where(t => t.CurrentHasMark).OrderBy(t => t.CurrentStartIndex).ToArray();
                            int len = marks.Length;
                            for (int i = 0; i < len; i++)
                            {
                                if (new RectangleF(marks[i].HeadRectangle.Left - 6, marks[i].HeadRectangle.Top, marks[i].HeadRectangle.Width + 12, marks[i].HeadRectangle.Height).Contains(e.Location))
                                {
                                    m_HandSelect = new RectangleF(marks[i].HeadRectangle.Left - 6, marks[i].HeadRectangle.Top, 12, marks[i].HeadRectangle.Height).Contains(e.Location) ? 1 :
                                        new RectangleF(marks[i].HeadRectangle.Right - 6, marks[i].HeadRectangle.Top, 12, marks[i].HeadRectangle.Height).Contains(e.Location) ? 2 : 0;
                                    if (m_HandSelect > 0 && m_HotKeyEnable && !(marks[i] as RectangleMarkers).ReadOnly)
                                    {
                                        Cursor = Cursors.SizeWE;
                                        m_SelectMarker = marks[i] as RectangleMarkers;
                                        return;
                                    }
                                    Cursor = Cursors.Default;
                                    Console.WriteLine(string.Format("光标的新坐标：{0} 右边界：{1}", e.Location.X, panel2.Width));
                                    return;
                                }
                            }
                            m_SelectMarker = null;
                        }
                    }
                }
                Cursor = Cursors.Cross;
            }
            else
            {
                Cursor = Cursors.Default;
            }
        }
        private void view_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }
        private DateTime m_RectMouseDownViewEndTime;

        private void view_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                this.Focus();
                m_currentItem = m_curveItems.Find(t => t.Visible && t.ClientRectangle.Contains(e.Location));

                if (m_currentItem == null)
                    return;

                if (!m_isRealDataCurve)
                {
                    for (int i = 0; i < m_ConnectMarkers.Length; i++)
                    {
                        if (m_ConnectMarkers[i] != null)
                            m_ConnectMarkers[i].isSelected = false;
                    }
                    ///区域框开始时间
                    float add2 = (e.X > m_currentItem.EndViewX ? m_currentItem.EndViewX : e.X) / xAxis.ValueRate;
                    m_RectMouseDownViewEndTime = m_ViewEndTime;
                    m_RectEndTime = m_RectStartTime = m_ViewStartTime.AddMilliseconds(add2);
                    if (e.Button == MouseButtons.Left)
                    {
                        if (!UserSelected)
                            UserSelected = true;
                        videoStatusChange(false, m_RectStartTime);
                    }
                    if (Cursor == Cursors.SizeWE && e.Button == MouseButtons.Left && m_SelectMarker != null)
                    {
                        m_DragStartTime = (m_HandSelect == 1 ? m_SelectMarker.MarkCreatTime : m_SelectMarker.EndTime);
                        m_DragStartIndex = m_HandSelect == 1 ? m_SelectMarker.StartIndex : m_SelectMarker.EndIndex;
                        m_DragStartPoint = new Point((int)Math.Round((m_DragStartTime - m_ViewStartTime).TotalMilliseconds * xAxis.ValueRate, 0), e.Y);
                        m_ConnectMarkers = new RectangleMarkers[m_SelectMarker.OnChannelIndexs.Length];
                        int tempIdx = 0;
                        m_ConnectMarkers[tempIdx++] = m_SelectMarker;
                        for (int i = 0; i < m_ConnectMarkers.Length; i++)
                        {
                            if (m_SelectMarker.OnChannelIndexs[i] != m_currentItem.ChannelNum)
                            {
                                CurveItem item = getCurveItem(m_SelectMarker.OnChannelIndexs[i]);
                                if (item != null)
                                {
                                    string strID = string.Format(":{0}", m_SelectMarker.ID.Split(':')[1]);
                                    m_ConnectMarkers[tempIdx] = item.CurrentMarks.Find(t => t.ID.Contains(strID) && t.MarkTyp == m_SelectMarker.MarkTyp) as RectangleMarkers;
                                    m_ConnectMarkers[tempIdx++].ID = string.Format("{0}{1}", item.ChannelNum, strID);
                                }
                            }
                        }
                        MouseIsDown = true;
                        return;
                    }
                    if (m_currentItem != null)
                    {
                        List<IMarker> items = getGlobalMarkers().FindAll(t => t.MarkCreatTime >= m_ViewStartTime && t.MarkCreatTime <= m_ViewEndTime);
                        for (int i = 0; i < items.Count; i++)
                        {
                            if (showTips(panel2, "All", items[i], e))
                            {
                                return;
                            }
                        }
                        ///把要显示的标记按照起始索引排序
                        IMarker[] marks = m_currentItem.CurrentMarks.Where(t => t.CurrentHasMark).OrderBy(t => t.CurrentStartIndex).ToArray();
                        int len = marks.Length;
                        for (int i = 0; i < len; i++)
                        {
                            ///当选中的是事件时PLMS时，要优先去判断下光标点击处是否存在腿动
                            if (marks[i].MarkTyp == IMarker.MarkType.PeriodicalBodyMove)
                            {
                                if (marks[i].HeadRectangle.Contains(e.Location))
                                {
                                    var find = marks.Where(t => t.MarkTyp == IMarker.MarkType.LegMove && t.HeadRectangle.Contains(e.Location)).ToList();
                                    if (find.Count > 0)
                                    {
                                        if (showTips(panel2, m_currentItem.ID, find[0], e))
                                        {
                                            return;
                                        }
                                    }
                                }
                            }
                            if (showTips(panel2, m_currentItem.ID, marks[i], e))
                            {
                                return;
                            }
                        }
                    }
                }
                else
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        if (!UserSelected)
                            UserSelected = true;
                        m_m_LockFreshCnt = 0;
                        m_LockFresh = true;
                    }
                }
                if (e.Button == MouseButtons.Right)
                {
                    //m_freshOk = false;
                }
                else
                {
                    MouseIsDown = true;
                    m_IsSelectRectange = false;
                    if (!m_isRealDataCurve)
                    {
                        m_hasLine = true;
                        bak_mousePoint = new Point((e.X > m_currentItem.EndViewX ? m_currentItem.EndViewX : e.X), e.Y);
                        Point current = panel2.PointToClient(Cursor.Position);
                        float add = current.X < 0 ? 0 : current.X / xAxis.ValueRate;
                        DateTime dt = m_ViewStartTime.AddMilliseconds(add);
                        captainChange(new CaptainInfoUion() { ViewStartTime = m_ViewStartTime, ViewEndTime = m_ViewEndTime, CurrentTime = dt, NowLocationX = current.X, showTyp = 1, CurrentFrameNo = m_FrameCnt });
                        panel2.Invalidate();
                    }
                }
                DrawStart(m_isRealDataCurve ? e.Location : bak_mousePoint);
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
        }
        private bool m_hasDialog = false;
        public delegate void JumpHappenDelegate(int StartFrameNo, DateTime StartTime, int belong);
        public event JumpHappenDelegate JumpHappenHandler;
        private bool showTips(Panel selectpanel, string channelID, IMarker mark, MouseEventArgs e)
        {
            if (mark.HeadRectangle.Contains(e.Location))
            {
                if (e.Button == MouseButtons.Right && !m_hasDialog)
                {
                    m_hasDialog = true;
                    if (MarkerMouseDoubleClick != null)
                    {
                        MarkerMouseDoubleClick.Invoke(channelID, mark, new MouseEventArgs(e.Button, e.Clicks, e.X, e.Y + Top, e.Delta));
                        MarkFreshIAsyncResult(mark);
                    }
                    return true;
                }
                mark.isSelected = true;
                m_SelectMark = mark;
                toolTip1 = new ToolTip();
                if (mark is RectangleMarkers)
                {
                    RectangleMarkers rectMark = mark as RectangleMarkers;
                    m_ConnectMarkers = new RectangleMarkers[rectMark.OnChannelIndexs.Length];
                    int tempIdx = 0;
                    m_ConnectMarkers[tempIdx++] = rectMark;
                    for (int i = 0; i < m_ConnectMarkers.Length; i++)
                    {
                        if (rectMark.OnChannelIndexs[i] != m_currentItem.ChannelNum)
                        {
                            CurveItem item = getCurveItem(rectMark.OnChannelIndexs[i]);
                            if (item != null)
                            {
                                string strID = string.Format(":{0}", rectMark.ID.Split(':')[1]);
                                m_ConnectMarkers[tempIdx] = item.CurrentMarks.Find(t => t.ID.Contains(strID) && t.MarkTyp == rectMark.MarkTyp) as RectangleMarkers;
                                m_ConnectMarkers[tempIdx].ID = string.Format("{0}{1}", item.ChannelNum, strID);
                                m_ConnectMarkers[tempIdx++].isSelected = true;
                            }
                        }
                    }
                    toolTip1.Show(string.Format("Start Time： {0}\r\nEnd  Time： {1}\r\nEvent Name： {2}\r\nEvent Description： {3}\r\nDuration(s)： {4:f2}", rectMark.StartTime, rectMark.EndTime, rectMark.Name, rectMark.Description, (rectMark.EndTime - rectMark.StartTime).TotalSeconds), selectpanel, new Point(e.X, e.Y));
                   // toolTip1.Show(string.Format("起始时间：{0}\r\n结束时间：{1}\r\n事件名称：{2}\r\n事件描述：{3}\r\n持续时长(s)：{4:f2}", rectMark.StartTime, rectMark.EndTime, rectMark.Name, rectMark.Description, (rectMark.EndTime - rectMark.StartTime).TotalSeconds), selectpanel, new Point(e.X, e.Y));
                    if (JumpHappenHandler != null && isSplitscreen)
                        JumpHappenHandler.Invoke(rectMark.StartFrameNo, rectMark.StartTime, Belong);
                }
                else
                {
                    toolTip1.Show(string.Format("Start Time： {0}\r\nEnd Time： {1}\r\nEvent Name： {2}", mark.MarkCreatTime, mark.Name, mark.Description), selectpanel, new Point(e.X, e.Y));
                   // toolTip1.Show(string.Format("发生时间：{0}\r\n事件名称：{1}\r\n事件描述：{2}", mark.MarkCreatTime, mark.Name, mark.Description), selectpanel, new Point(e.X, e.Y));
                    if (JumpHappenHandler != null && isSplitscreen)
                        JumpHappenHandler.Invoke(mark.StartFrameNo, mark.MarkCreatTime, Belong);
                }
                AreaInvalidate(false);
                return true;
            }
            return false;
        }
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
        /// 双击标记区域信息弹框后要处理的部分
        /// </summary>
        /// <param name="result"></param>
        private void MarkFreshIAsyncResult(IAsyncResult result)
        {
            IMarker mark = (result.AsyncState as IMarker);
            if (MarkerDeleteHandler != null)
                MarkerDeleteHandler.Invoke(mark);
            m_hasDialog = false;
            //m_currentItem = null;
        }
        /// <summary>
        /// 双击标记区域信息弹框后要处理的部分
        /// </summary>
        /// <param name="result"></param>
        private void MarkFreshIAsyncResult(IMarker mark)
        {
            if (MarkerDeleteHandler != null)
                MarkerDeleteHandler.Invoke(mark);
            m_hasDialog = false;
            //m_currentItem = null;
        }
        private CurveItem m_currentItem = null;
        private void ResizeToRectangle(Point p, ref Rectangle rect)
        {
            try
            {
                if (m_currentItem == null)
                    return;
                if (m_NewMouseRectVisible)
                {
                    DrawRectangle(rect);
                    m_NewMouseRectVisible = false;
                }
                Point start = new Point(rect.X, rect.Y);
                p = CheckEndPoint(ref start, p);
                rect.Y = start.Y;
                rect.Width = p.X - rect.Left;
                rect.Height = p.Y - rect.Top - (m_currentItem.IsLastViewChannel ? 4 : 0);
                DrawRectangle(rect);
                m_NewMouseRectVisible = true;
            }
            catch { }
        }
        /// <summary>
        /// 绘制框选区域
        /// </summary>
        private Rectangle m_SelectRectangeView = Rectangle.Empty;
        private int m_SelectRangchannelID = 0;
        private float xScreenRate = 1;
        private float yScreenRate = 1;
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
            m_IsSelectRectange = true;
            m_SelectRangchannelID = m_currentItem.ChannelNum;
            m_hasLine = false;
            if (Invalidate)
                panel2.Invalidate();
            //this.Invoke(new MethodInvoker(() =>
            //{
            //    Rectangle rect = panel2.RectangleToScreen(rect2);
            //    rect.X = (int)(rect.X * xScreenRate);
            //    rect.Y = (int)(rect.Y * yScreenRate);
            //    rect.Width = (int)(rect.Width * xScreenRate);
            //    rect.Height = (int)(rect.Height * yScreenRate);
            //    ControlPaint.FillReversibleRectangle(rect, Color.Blue);
            //}));
        }
        private void DrawStart(Point StartPoint)
        {
            panel2.Capture = true;
            Cursor.Clip = panel2.RectangleToScreen(new Rectangle(0, 0, ClientSize.Width, ClientSize.Height));
            m_SatrtMouseRect = new Rectangle(StartPoint.X, StartPoint.Y, 0, 0);
            m_NewMouseRect = new Rectangle(StartPoint.X, StartPoint.Y, 0, 0);
        }
        /// <summary>
        /// 判断结束点所在通道位置，y坐标超限是需要进行二次校准，保证其跟起始点在一个通道内
        /// </summary>
        /// <returns></returns>
        public Point CheckEndPoint(ref Point StartPoint, Point EndPoint)
        {
            try
            {
                if (m_currentItem != null)
                {
                    StartPoint = new Point(StartPoint.X, (int)m_currentItem.ClientRectangle.Top);
                    if (EndPoint.X > m_currentItem.EndViewX && !m_isRealDataCurve)
                        EndPoint.X = m_currentItem.EndViewX;
                    return new Point(EndPoint.X > m_currentItem.ClientRectangle.Right ? (int)m_currentItem.ClientRectangle.Right : EndPoint.X < m_currentItem.ClientRectangle.Left ? (int)m_currentItem.ClientRectangle.Left : EndPoint.X, (int)m_currentItem.ClientRectangle.Bottom);
                }
            }
            catch { }
            return EndPoint;
        }
        #endregion
        #region 私有方法
        //声明一个API函数 
        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]

        private static extern bool BitBlt(
            IntPtr hdcDest, // 目标 DC的句柄 
            int nXDest,
            int nYDest,
            int nWidth,
            int nHeight,
            IntPtr hdcSrc, // 源DC的句柄 
            int nXSrc,
            int nYSrc,
            System.Int32 dwRop // 光栅的处理数值 
        );
        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hbitmap);

        [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
        public static extern IntPtr DeleteDC(IntPtr hDc);

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        public static extern IntPtr DeleteObject(IntPtr bmp);
        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="source"></param>
        /// <param name="sx"></param>
        /// <param name="sy"></param>
        /// <param name="destg"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public void DrawImage(Image source, float sx, float sy, Graphics destg, float dx, float dy)
        {
            IntPtr hdc = destg.GetHdc();
            IntPtr memdc = CreateCompatibleDC(hdc);
            SelectObject(memdc, ((Bitmap)source).GetHbitmap());
            BitBlt(hdc, (int)dx, (int)dy, source.Width, source.Height, memdc, (int)sx, (int)sy, 13369376);
        }
        /// <summary>
        /// 获取适宜的字体大小
        /// </summary>
        /// <param name="with"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private void getFontSize(int with, int height, ref float refontSize)
        {
            float fontSize = (height * 3 / 4);
            float fontSize2 = (float)((with * 3 / 4) * 0.5);
            if (fontSize > fontSize2)
                fontSize = fontSize2;
            refontSize = fontSize > refontSize ? refontSize : fontSize - 2;
        }
        /// <summary>
        /// 触发标题改变事件
        /// </summary>
        /// <param name="st"></param>
        /// <param name="et"></param>
        /// <param name="current"></param>
        /// <param name="rectTimes"></param>
        private void captainChange(CaptainInfoUion info)
        {
            if (CaptainValueChangeHandler != null)
            {
                CaptainValueChangeHandler.Invoke(info);
            }
        }
        /// <summary>
        /// 处理绘制区域弹出事件处理完毕后的数据
        /// </summary>
        /// <param name="result"></param>
        private void UpdateViewIAsyncResult()
        {
            if (!m_isRealDataCurve)
            {
                //CurveItem item = (CurveItem)result.AsyncState;
                this.Invoke(new MethodInvoker(() =>
                {
                    // DrawRectangle(m_NewMouseRect);
                    m_FrameCnt = bak_frameCnt;
                    m_FrameOffTime = bak_FrameOffTime;
                    if (m_TimeLineVisible)
                        Cursor = Cursors.Default;
                }));
                m_NewMouseRect = Rectangle.Empty;
                m_TimeLineEnable = true;
            }
        }
        /// <summary>
        /// 曲线集合
        /// </summary>
        private List<CurveItem> m_curveItems = new List<CurveItem>();
        /// <summary>
        /// 获取趋势图
        /// </summary>
        /// <returns></returns>
        private Image GetMap()
        {
            Bitmap srcImg = new Bitmap(m_Width, m_Height);
            using (Graphics g = Graphics.FromImage(srcImg))
            {
                int w = this.m_Width;
                int h = this.m_Height;
                g.Clip = new Region(new Rectangle(0, 0, w, h));
                int x_count = xAxis.Calibrations.Count;
                int y_count = yAxis.Calibrations.Count;
                int docksize = 0;
                g.Clear(this.m_backcolor);
                g.SmoothingMode = SmoothingMode.None;
                float fontSize = 50;
                getFontSize(m_Width, m_Height, ref fontSize);
                ///绘制出时基字符
                using (Font f = new Font("微软雅黑", fontSize, FontStyle.Bold))
                {
                    int wh = (int)g.MeasureString(m_strViewBaseTime, f).Width;
                    using (Bitmap waterImg = new Bitmap(wh, f.Height))
                    {
                        Rectangle rect = new Rectangle(m_Width - wh, h - f.Height, wh, f.Height);
                        using (Graphics ggt = Graphics.FromImage(waterImg))
                        {
                            ggt.DrawString(m_strViewBaseTime, f, Brushes.Gray, new PointF(0, 0));
                        }
                        g.DrawImage(waterImg, rect, 0, 0, rect.Width, rect.Height, GraphicsUnit.Pixel, imageAtt);
                    }
                }
                ///绘制x主轴坐标
                xAxis.DrawXAxis(g, docksize, w, h);
                ///绘制y主轴坐标
                yAxis.DrawYAxis(g, docksize, w, h);
                if (!m_isRealDataCurve)
                {
                    //int viewStartTime = ((m_FrameCnt - 1) * m_OneFrameSpanTime + m_FrameOffTime);
                    //m_ViewStartTime = m_StartTime.AddSeconds(viewStartTime);///显示区域开始时间
                    //m_ViewEndTime = m_ViewStartTime.AddSeconds(m_BaseTimeLineSpan);///显示区域结束时间
                    //if (UserSelected&&!MouseIsDown)       //为了不让轴线的标题延长线提前绘制 因此注释
                    //   captainChange(new CaptainInfoUion() { ViewStartTime = m_ViewStartTime, ViewEndTime = m_ViewEndTime, showTyp = 0, CurrentFrameNo = m_FrameCnt });
                }
                else
                {
                    m_ViewEndTime = DateTime.Now;
                    m_ViewStartTime = m_ViewEndTime.AddSeconds(0 - m_BaseTimeLineSpan);
                }
                SuperPoints[] itemPointUion = new SuperPoints[m_curveItems.Count];
                //List<Bitmap> channelmap = new System.Collections.Generic.List<Bitmap>();
                object objLock = new object();
                Task[] tt = new Task[itemPointUion.Length];
                Bitmap[] maps = new Bitmap[tt.Length];
                DateTime dt = DateTime.Now;
                for (int i = 0; i < m_curveItems.Count; i++)
                {///异步线程加载要绘制的点
                    tt[i] = new Task((obj) =>
                    {
                        int idx = (int)obj;
                        if (m_curveItems[idx].Visible)
                        {
                            Bitmap ret = new Bitmap(w, h);
                            ret.MakeTransparent();

                            if (!m_curveItems[idx].IsDraw)
                                return;
                            using (Graphics gg = Graphics.FromImage(ret))
                            {
                                itemPointUion[idx] = m_isRealDataCurve ? m_curveItems[idx].GetxyDataValues() : m_curveItems[idx].GetOneFramePointsEx(m_FrameCnt, BaseTimeLineSpan, m_FrameOffTime);
                                ///绘制通道的刻度尺
                                m_curveItems[idx].yAxis.drawyAxis(gg, docksize, w, h);
                                using (Pen pointpen = new Pen(m_curveItems[idx].isSelected ? Color.LightSkyBlue : m_curveItems[idx].PenColor, m_curveItems[idx].PenWidth))
                                {
                                    if (m_curveItems[idx].IsShowValue && itemPointUion[idx] != null)
                                    {
                                        PointF[] source = itemPointUion[idx].SourcePoints;
                                        //绘制曲线点
                                        PointF[] datapoints = itemPointUion[idx].Points;
                                        //来回翻页时候会出现黑屏情况 加入try catch 预防
                                        try
                                        {
                                            #region 时基大于60s，小于30min，通过选择，显示血氧带值曲线(实时监测界面不论什么时基都采取抽点，显示带值曲线)
                                            if (m_BaseTimeLineSpan > 60 && m_BaseTimeLineSpan < 1800 && !m_isRealDataCurve)
                                            {
                                                int[] isneeddrawvalue = new int[datapoints.Length];
                                                int pointinterval = itemPointUion[idx].Points.Length / 60 / 2;
                                                float yvalue = 0;
                                                bool isup = false;
                                                bool isdown = false;
                                                int upcount = 0;
                                                int downcount = 0;
                                                float upstartvalue = 0;
                                                float downstartvalue = 0;
                                                if (source[1].Y >= source[0].Y)
                                                {
                                                    isup = true;
                                                    upstartvalue = source[1].Y;
                                                    upcount++;
                                                }
                                                else
                                                {
                                                    isdown = true;
                                                    downstartvalue = source[0].Y;
                                                    downcount++;
                                                }
                                                #region 先确定有价值的拐点，拐点需要画数值 1表示画 0不画
                                                for (int spo2index = 0; spo2index < datapoints.Length - 1; spo2index++)
                                                {
                                                    yvalue = source[spo2index].Y;
                                                    if (isup)
                                                    {
                                                        if (source[spo2index + 1].Y >= yvalue)
                                                        {
                                                            upcount++;
                                                            continue;
                                                        }
                                                        else
                                                        {
                                                            isup = false;
                                                            isdown = true;
                                                            downstartvalue = source[spo2index].Y;
                                                            upcount = 0;
                                                            if (Math.Abs(source[spo2index].Y - upstartvalue) > 2 || upcount > pointinterval)
                                                            {
                                                                isneeddrawvalue[spo2index] = 1;
                                                            }
                                                            continue;
                                                        }
                                                    }
                                                    if (isdown)
                                                    {
                                                        if (source[spo2index + 1].Y <= yvalue)
                                                        {
                                                            downcount++;
                                                            continue;
                                                        }
                                                        else
                                                        {
                                                            isdown = false;
                                                            isup = true;
                                                            upstartvalue = source[spo2index].Y;
                                                            downcount = 0;
                                                            if (downcount > pointinterval || Math.Abs(downstartvalue - source[spo2index].Y) > 2)
                                                            {
                                                                isneeddrawvalue[spo2index] = 1;
                                                            }
                                                            continue;
                                                        }
                                                    }
                                                }
                                                #endregion
                                                #region 拐点数量太少，在不影响拐点的情况下，等间距插入一些值
                                                for (int arrayindex = 0; arrayindex < isneeddrawvalue.Length; arrayindex++)
                                                {
                                                    if (arrayindex > 0 && arrayindex % (itemPointUion[idx].Points.Length / 60) == 0)
                                                    {
                                                        bool isneedshow = true;
                                                        ///防止尾和头相连
                                                        for (int index = arrayindex - pointinterval * 2; index < arrayindex + pointinterval * 2; index++)
                                                        {
                                                            if (isneeddrawvalue[index] == 1)
                                                            {
                                                                isneedshow = false;
                                                                if (index == arrayindex)
                                                                {
                                                                    isneedshow = true;
                                                                }
                                                            }
                                                        }
                                                        isneeddrawvalue[arrayindex] = isneedshow ? 1 : 0;
                                                    }
                                                }
                                                #endregion
                                                using (Font font = new Font("宋体", 9))
                                                {
                                                    #region 按照存好的数组去绘画血氧
                                                    for (int spo2index = 0; spo2index < datapoints.Length - 1; spo2index++)
                                                    {
                                                        yvalue = source[spo2index].Y;
                                                        if (isneeddrawvalue[spo2index] == 1)
                                                        {
                                                            float strValueY = datapoints[spo2index].Y - font.Height - 2;
                                                            float strValueY2 = datapoints[spo2index].Y + font.Height + 2;
                                                            if (strValueY < 0 && strValueY2 < h)
                                                            {
                                                                strValueY = datapoints[spo2index].Y + 2;
                                                            }
                                                            pointpen.StartCap = LineCap.RoundAnchor;
                                                            gg.DrawLine(pointpen, datapoints[spo2index], datapoints[spo2index + 1]);
                                                            gg.DrawString(yvalue.ToString(), font, new SolidBrush(Color.Black), new PointF(datapoints[spo2index].X - 2, strValueY));
                                                        }
                                                        else
                                                        {
                                                            pointpen.StartCap = LineCap.Round;
                                                            gg.DrawLine(pointpen, datapoints[spo2index], datapoints[spo2index + 1]);
                                                        }
                                                    }
                                                    #endregion
                                                }
                                            }
                                            #endregion

                                            #region 时基小于等于60 或者 大于等于30min 通过抽点，显示带值得曲线
                                            else
                                            {
                                                int datalength = datapoints.Length;
                                                using (Font font = new Font("宋体", 9))
                                                {
                                                    pointpen.EndCap = LineCap.RoundAnchor;
                                                    float yvalue = 0;
                                                    if (datalength > 1)
                                                    {
                                                        #region 抽点
                                                        int dcnt = w / ((int)gg.MeasureString("9999", font).Width + 2);///最多显示的点数量
                                                        int dcnt2 = 2; ///抽点范围                                                      
                                                        if (dcnt < datalength)
                                                        {
                                                            dcnt2 = datalength * 2 / dcnt;
                                                        }
                                                        if (dcnt2 > 2)
                                                        {
                                                            int mid = dcnt2 / 2;
                                                            List<PointF> bak_datapoints = new List<PointF>();
                                                            List<PointF> bak_source = new System.Collections.Generic.List<PointF>();
                                                            int cntidx = 0;
                                                            float maxy = 0, miny = 0;
                                                            int maxyidx = 0, minyidx = 0;
                                                            float locx = 0;
                                                            for (int a = 0; a < datalength; a++)
                                                            {
                                                                cntidx++;
                                                                if (cntidx == 1)
                                                                {
                                                                    int addidx = a + mid;
                                                                    if (addidx + 1 > datalength)
                                                                        addidx = a;
                                                                    locx = datapoints[a == 0 ? 0 : addidx].X;
                                                                    miny = maxy = source[a].Y;
                                                                    minyidx = a;
                                                                    maxyidx = a;
                                                                }
                                                                else
                                                                {
                                                                    if (maxy < source[a].Y)
                                                                    {
                                                                        maxy = source[a].Y;
                                                                        maxyidx = a;
                                                                    }
                                                                    else if (miny > source[a].Y)
                                                                    {
                                                                        miny = source[a].Y;
                                                                        minyidx = a;
                                                                    }
                                                                    if (cntidx == dcnt2 || a + 1 == datalength)
                                                                    {
                                                                        if (minyidx == maxyidx && a + 1 < datalength)
                                                                        {
                                                                            maxyidx = a;
                                                                        }
                                                                        if (minyidx < maxyidx)
                                                                        {
                                                                            bak_datapoints.Add(new PointF(locx, datapoints[minyidx].Y));
                                                                            bak_datapoints.Add(new PointF(datapoints[a].X, datapoints[maxyidx].Y));
                                                                            bak_source.Add(new PointF(0, miny));
                                                                            bak_source.Add(new PointF(0, maxy));
                                                                        }
                                                                        else if (minyidx > maxyidx)
                                                                        {
                                                                            bak_datapoints.Add(new PointF(locx, datapoints[maxyidx].Y));
                                                                            bak_datapoints.Add(new PointF(datapoints[a].X, datapoints[minyidx].Y));
                                                                            bak_source.Add(new PointF(0, maxy));
                                                                            bak_source.Add(new PointF(0, miny));
                                                                        }
                                                                        else
                                                                        {
                                                                            bak_datapoints.Add(new PointF(datapoints[a].X, datapoints[minyidx].Y));
                                                                            bak_source.Add(new PointF(0, miny));
                                                                        }
                                                                        cntidx = 0;
                                                                    }
                                                                }
                                                            }
                                                            int llen = bak_datapoints.Count - 1;
                                                            for (int a = 0; a < llen; a++)
                                                            {
                                                                yvalue = bak_source[a].Y;
                                                                float strValueY = bak_datapoints[a].Y - font.Height - 2;
                                                                float strValueY2 = bak_datapoints[a].Y + font.Height + 2;
                                                                if (strValueY < 0 && strValueY2 < h)
                                                                {
                                                                    strValueY = bak_datapoints[a].Y + 2;
                                                                }
                                                                gg.DrawString(yvalue.ToString(), font, new SolidBrush(Color.Black), new PointF(bak_datapoints[a].X - 2, strValueY));
                                                                gg.DrawLine(pointpen, bak_datapoints[a], bak_datapoints[a + 1]);
                                                            }
                                                            yvalue = bak_source[llen].Y;
                                                            float y = bak_datapoints[llen].Y - font.Height - 2;
                                                            if (y < 0 && bak_datapoints[llen].Y + font.Height + 2 < h)
                                                            {
                                                                y = bak_datapoints[llen].Y + 2;
                                                            }
                                                            gg.DrawString(yvalue.ToString(), font, new SolidBrush(Color.Black), new PointF(bak_datapoints[llen].X - 2, y));
                                                        }
                                                        else
                                                        {
                                                            int llen = datalength - 1;
                                                            for (int a = 0; a < llen; a++)
                                                            {
                                                                yvalue = source[a].Y;
                                                                float strValueY = datapoints[a].Y - font.Height - 2;
                                                                float strValueY2 = datapoints[a].Y + font.Height + 2;
                                                                if (strValueY < 0 && strValueY2 < h)
                                                                {
                                                                    strValueY = datapoints[a].Y + 2;
                                                                }
                                                                gg.DrawString(yvalue.ToString(), font, new SolidBrush(Color.Black), new PointF(datapoints[a].X - 2, strValueY));
                                                                gg.DrawLine(pointpen, datapoints[a], datapoints[a + 1]);
                                                            }
                                                            yvalue = source[llen].Y;
                                                            float y = datapoints[llen].Y - font.Height - 2;
                                                            if (y < 0 && datapoints[llen].Y + font.Height + 2 < h)
                                                            {
                                                                y = datapoints[llen].Y + 2;
                                                            }
                                                            gg.DrawString(yvalue.ToString(), font, new SolidBrush(Color.Black), new PointF(datapoints[llen].X - 2, y));
                                                        }
                                                        #endregion
                                                    }
                                                    else if (datalength == 1)
                                                    {
                                                        yvalue = source[0].Y;
                                                        float y = datapoints[0].Y - font.Height - 2;
                                                        float x = (w + docksize) / 2;
                                                        gg.DrawString(yvalue.ToString(), font, new SolidBrush(Color.Black), new PointF(x - 2, y));
                                                        gg.DrawLine(pointpen, new PointF(docksize, datapoints[0].Y), new PointF(x, datapoints[0].Y));
                                                        gg.DrawLine(pointpen, new PointF(x, datapoints[0].Y), new PointF(w, datapoints[0].Y));
                                                    }
                                                }
                                                #endregion
                                            }
                                        }
                                        catch (Exception ee)
                                        {
                                            Console.WriteLine(ee.Message);
                                        }
                                        #region 注释掉不抽点显示带值得曲线
                                        //if (datapoints.Length > 1)
                                        //    using (Font font = new Font("宋体", 9))
                                        //    {
                                        //        pointpen.EndCap = LineCap.RoundAnchor;
                                        //        float yvalue = 0;
                                        //        for (int a = 0; a < datapoints.Length - 1; a++)
                                        //        {
                                        //            yvalue = source[a].Y;
                                        //            float strValueY = datapoints[a].Y - font.Height - 2;
                                        //            float strValueY2 = datapoints[a].Y + font.Height + 2;
                                        //            if (strValueY < 0 && strValueY2 < h)
                                        //            {
                                        //                strValueY = datapoints[a].Y + 2;
                                        //            }
                                        //            gg.DrawString(yvalue.ToString(), font, new SolidBrush(Color.Black), new PointF(datapoints[a].X - 2, strValueY));
                                        //            gg.DrawLine(pointpen, datapoints[a], datapoints[a + 1]);
                                        //        }
                                        //        yvalue = source[source.Length - 1].Y;
                                        //        float y = datapoints[source.Length - 1].Y - font.Height - 2;
                                        //        if (y < 0 && datapoints[source.Length - 1].Y + font.Height + 2 < h)
                                        //        {
                                        //            y = datapoints[source.Length - 1].Y + 2;
                                        //        }
                                        //        gg.DrawString(yvalue.ToString(), font, new SolidBrush(Color.Black), new PointF(datapoints[source.Length - 1].X - 2, y));
                                        //    }
                                        #endregion
                                    }
                                    else
                                    {
                                        try
                                        {
                                            if (itemPointUion[idx] != null)
                                            {
                                                if (itemPointUion[idx].Points.Length > 1)
                                                    ///先绘制出当前区域的曲线
                                                    gg.DrawLines(pointpen, itemPointUion[idx].Points);
                                                else if (itemPointUion[idx].Points.Length == 1)
                                                {
                                                    gg.DrawLine(pointpen, new PointF(docksize, itemPointUion[idx].Points[0].Y), new PointF(w, itemPointUion[idx].Points[0].Y));
                                                }
                                                //绘制75微伏基线
                                                if (m_curveItems[idx].DBaseLineVisible)
                                                {
                                                    using (Pen basepen = new Pen(m_curveItems[idx].isSelected ? Color.LightSkyBlue : Color.LightSlateGray, 1))
                                                    {
                                                        float hy = m_curveItems[idx].yBaseDistance - 75 * m_curveItems[idx].yAxis.ValueRate / 2;
                                                        gg.DrawLine(basepen, new PointF(docksize, hy), new PointF(w - docksize, hy));
                                                        float ly = m_curveItems[idx].yBaseDistance + 75 * m_curveItems[idx].yAxis.ValueRate / 2;
                                                        gg.DrawLine(basepen, new PointF(docksize, ly), new PointF(w - docksize, ly));
                                                    }
                                                }
                                            }
                                        }
                                        catch(Exception ee)
                                        {
                                            Console.WriteLine(ee.Message);
                                        }
                                    }
                                    if (m_curveItems[idx].TimeSpan == 1000)///1s一个点的通道需要多绘制一段
                                    {
                                        if (itemPointUion[idx] != null)
                                        {
                                            if (itemPointUion[idx].Points.Length > 1)
                                            {
                                                int endpointidx = itemPointUion[idx].Points.Length - 1;
                                                gg.DrawLine(pointpen, itemPointUion[idx].Points[endpointidx], new PointF(itemPointUion[idx].Points[endpointidx].X + m_curveItems[idx].xAxis.ValueRate * m_curveItems[idx].TimeSpan, itemPointUion[idx].Points[endpointidx].Y));
                                            }
                                        }
                                    }
                                }
                            }
                            maps[idx] = ret;
                        }
                    }, i);
                    tt[i].Start();
                }
                Task.WaitAll(tt);///等待所有异步线程完成
                Console.WriteLine(string.Format("多线程绘制耗时：{0}ms", (DateTime.Now - dt).TotalMilliseconds));
                for (int i = 0; i < tt.Length; i++)
                {
                    if (maps[i] != null)
                    {
                        g.DrawImage(maps[i], 0, 0);
                        maps[i].Dispose();
                    }
                    tt[i].Dispose();///释放线程资源
                }
                g.Clip.Dispose();
                srcImg.Tag = itemPointUion;
            }
            return (srcImg);
        }

        /// <summary>
        /// 绘制多次小睡的标记线
        /// </summary>
        /// <param name="g"></param>
        /// <param name="mark"></param>
        /// <param name="index"></param>
        private void DrawMulSleepMark(Graphics g, RectangleMarkers mark, int index)
        {
            int[] offs = new int[] { 0, 0 };
            if (!string.IsNullOrEmpty(mark.Comments2))
            {
                try
                {
                    string[] s = mark.Comments2.Split('-');
                    if (s.Length == 2)
                    {
                        offs[0] = Convert.ToInt32(s[0]);
                        offs[1] = Convert.ToInt32(s[1]);
                    }
                }
                catch (Exception ee)
                {
                    return;
                }
            }
            int[] idxs = new int[] { mark.StartIndex, mark.EndIndex };
            for (int i = 0; i < idxs.Length; i++)
            {
                DateTime sleepstarttime = m_StartTime.AddSeconds(idxs[i] * 30 + offs[i]);
                PointF strLocation = PointF.Empty;
                double ts = (sleepstarttime - m_ViewStartTime).TotalMilliseconds + offs[i];
                double offtime = ts * xAxis.ValueRate;
                float angle = 45;
                if (sleepstarttime >= m_ViewStartTime && sleepstarttime < m_ViewEndTime)
                {
                    strLocation = new PointF((int)offtime + 20, -5);
                }
                else if (sleepstarttime == m_ViewEndTime)
                {
                    angle = 315;
                    strLocation = new PointF(g.VisibleClipBounds.Width - 120, 0);
                }
                else
                {
                    continue;
                }
                g.DrawLine(new Pen(Color.Red, 2), (int)offtime, 0, (int)offtime, g.VisibleClipBounds.Height);
                using (Font f = new Font("宋体", 12))
                {
                    //位置
                    g.TranslateTransform((int)offtime, 0);
                    //旋转角度
                    g.RotateTransform(angle);
                    //恢复图像在水平和垂直方向的平移
                    g.TranslateTransform(-(int)offtime, -0);
                    string sleepdescription = string.Format("第{0}次小睡{1}", index + 1, i > 0 ? "结束" : "开始");
                    g.DrawString(sleepdescription, f, new SolidBrush(Color.Blue), strLocation);
                    //重至绘图的所有变换
                    g.ResetTransform();
                    g.Save();
                }
            }
        }
        /// <summary>
        /// 绘制标记
        /// </summary>
        /// <param name="img"></param>
        private Image DrawMarkers(Image img)
        {
            Bitmap ret = new Bitmap(img.Width, img.Height);// img.Clone() as Image;
            ret.MakeTransparent();
            if (!m_isRealDataCurve)///标记单独绘制
            {
                using (Graphics g = Graphics.FromImage(ret))
                {
                    MultipleSleepMarks = getMulSleepMarks();
                    //标记多次小睡的联动
                    int markCnt = MultipleSleepMarks.Count;
                    if (markCnt > 0)
                    {
                        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
                        for (int s = 0; s < markCnt; s++)
                        {
                            DrawMulSleepMark(g, MultipleSleepMarks[s] as RectangleMarkers, s);
                        }
                    }
                    SuperPoints[] itemPointUion = img.Tag as SuperPoints[];
                    for (int i = 0; i < m_curveItems.Count; i++)
                    {
                        if (!m_curveItems[i].Visible)
                            continue;
                        using (Pen pointpen = new Pen(m_curveItems[i].isSelected ? Color.LightSkyBlue : m_curveItems[i].PenColor, m_curveItems[i].PenWidth))
                        {
                            DrawMarkers(m_curveItems[i], g, pointpen, itemPointUion[i]);
                        }
                    }
                }
            }
            return ret;
        }
        /// <summary>
        /// 绘制标记
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="g"></param>
        private void DrawMarkers(CurveItem channel, Graphics g, Pen pointpen, SuperPoints sup)
        {
            if (sup != null)
            {
                ///找出当前所有标记中将要在当前显示区域绘制出来的标记
                for (int s = 0; s < channel.CurrentMarks.Count; s++)
                {
                    IMarker maker = channel.CurrentMarks[s];
                    maker.CurrentHasMark = false;
                    if (maker is RectangleMarkers)
                    {
                        RectangleMarkers mk = maker as RectangleMarkers;
                        mk.CurrentStartIndex = -1;
                        mk.CurrentEndIndex = -1;
                        ///凡是： 标记的起始索引大于当前区域的最大索引，标记的终止索引小于当前区域的起始索引，均不属于要显示的标记。
                        if (!(sup.StartIndex >= mk.EndIndex || sup.EndIndex <= mk.StartIndex))
                        {
                            mk.CurrentStartIndex = sup.StartIndex > mk.StartIndex ? 0 : mk.StartIndex - sup.StartIndex;
                            mk.CurrentEndIndex = sup.EndIndex > mk.EndIndex ? mk.EndIndex - sup.StartIndex - 1 : sup.SourcePoints.Length - 1;
                            mk.CurrentHasMark = true;
                        }
                    }
                }
                ///把要显示的标记按照起始索引排序
                IMarker[] marks = channel.CurrentMarks.Where(t => t.CurrentHasMark).OrderBy(t => t.CurrentStartIndex).ToArray();
                int len = marks.Length;
                for (int s = 0; s < len; s++)
                {
                    if (marks[s] is RectangleMarkers)
                    {
                        RectangleMarkers mk = marks[s] as RectangleMarkers;
                        int stidx = mk.CurrentStartIndex, edidx = mk.CurrentEndIndex;

                        /////超出界线时要强制把索引置位
                        if (edidx > sup.SourcePoints.Length - 1)
                        {
                            edidx = sup.SourcePoints.Length - 1;
                        }
                        if (stidx > edidx)
                        {
                            float limitvalue = 0;
                            HotKey hotKey = getHotKeys().Find(t => t.MarkTyp == mk.MarkTyp);
                            if (hotKey != null)
                            {
                                limitvalue = hotKey.MinLimitRange * 1000;///时间阈值是s所以需要转换成ms
                            }
                            stidx = edidx - (int)(limitvalue / channel.TimeSpan);
                        }
                        float rectw = sup.SourcePoints[edidx].X - sup.SourcePoints[stidx].X;
                        if (sup.EndIndex < mk.EndIndex)
                        {
                            rectw += channel.xAxis.ValueRate * channel.TimeSpan;
                        }
                        mk.HeadRectangle = new RectangleF(sup.SourcePoints[stidx].X, channel.yAxis.offSetDistance, rectw, channel.yAxis.Displacement);
                        g.FillRectangle(new SolidBrush(Color.FromArgb(mk.isSelected ? 205 : 105, mk.isSelected ? Color.LightGray : mk.ForeColor)), mk.HeadRectangle);
                    }
                }
            }
        }
        /// <summary>
        /// 绘制标签
        /// </summary>
        /// <returns></returns>
        private Image DrawFlags()
        {
            Bitmap ret = new Bitmap(m_Width, m_Height);
            ret.MakeTransparent();
            if (!m_isRealDataCurve)///标记单独绘制
            {
                using (Graphics g = Graphics.FromImage(ret))
                {
                    List<IMarker> items = getGlobalMarkers().FindAll(t => t.MarkCreatTime >= m_ViewStartTime && t.MarkCreatTime <= m_ViewEndTime && (t is StringMarkers));
                    for (int i = 0; i < items.Count; i++)
                    {
                        StringMarkers smk = (items[i] as StringMarkers);
                        using (Pen p = new Pen(Color.FromArgb(smk.isSelected ? 255 : 125, Color.Red), smk.isSelected ? 2 : 1))
                        {
                            int x = (int)((smk.MarkCreatTime - m_ViewStartTime).TotalMilliseconds * xAxis.ValueRate);
                            g.DrawLine(p, new Point(x, 0), new Point(x, m_Height));
                            SizeF sf = g.MeasureString(smk.Name, smk.Font);
                            float waterWith = sf.Width + 4;
                            float waterHeight = sf.Height + 4;
                            RectangleF rect = new RectangleF(x + 1, m_Height - waterHeight - 4, waterWith, waterHeight);
                            smk.HeadRectangle = rect;
                            g.FillRectangle(new SolidBrush(Color.FromArgb(smk.isSelected ? 205 : 105, smk.ForeColor)), rect);
                            g.DrawString(smk.Name, smk.Font, new SolidBrush(Color.FromArgb(smk.isSelected ? 255 : 155, Color.Black)), new PointF(rect.Left + 2, rect.Top + 2));
                        }
                    }
                    if (m_IsSelectRectange)
                    {
                        g.FillRectangle(new SolidBrush(Color.FromArgb(50, Color.Yellow)), m_SelectRectangeView);
                        g.DrawRectangle(new Pen(Color.Black), m_SelectRectangeView);


                    }
                    else
                    {
                        m_SelectRangchannelID = -1;
                    }
                    if (m_hasLine)
                    {
                        using (Pen p = new Pen(Color.Gold, 2))
                        {
                            p.DashStyle = DashStyle.Dash;
                            p.DashPattern = new float[] { 5, 5 };
                            g.DrawLine(p, new Point(bak_mousePoint.X, 0), new Point(bak_mousePoint.X, m_Height));
                        }
                    }
                }
            }
            return ret;
        }
        /// <summary>
        /// 绘制睡眠分期
        /// </summary>
        /// <returns></returns>
        private Image DrawStage()
        {
            Bitmap ret = new Bitmap(m_Width, m_Height);
            //ret.MakeTransparent();
            if (!m_isRealDataCurve)///标记单独绘制
            {
                using (Graphics g = Graphics.FromImage(ret))
                {
                    string[] SleepStage = getSleepStage();
                    int[] SleepPos = getSleepPose();
                    if (SleepPos.Length <= 0 || SleepStage.Length <= 0)
                    {
                        return ret;
                    }
                    if (BaseTimeLineSpan <= 30)
                    {
                        float fontSize = 80;
                        getFontSize(m_Width, m_Height, ref fontSize);
                        using (Font f = new Font("微软雅黑", fontSize, FontStyle.Bold))
                        {
                            DateTime next = m_StartTime.AddMilliseconds(m_FrameCnt * 30000);
                            if (next > m_ViewStartTime && next < m_ViewEndTime)
                            {
                                using (Pen pp = new Pen(new SolidBrush(Color.FromArgb(200, Color.Blue)), 1) { DashStyle = DashStyle.Dash })
                                {
                                    float x = (float)((next - m_ViewStartTime).TotalMilliseconds * xAxis.ValueRate);
                                    g.DrawLine(pp, new PointF(x, 0), new PointF(x, m_Height));
                                    for (int i = 0; i < 2; i++)
                                    {
                                        string strStage = SleepStage[m_FrameCnt - 1 + i];
                                        SizeF sf = g.MeasureString(strStage, f);
                                        int waterWith = (int)(sf.Width + 4);
                                        int waterHeight = (int)(f.Height + 4);
                                        int w = (int)(i == 0 ? x : m_Width - x);
                                        Rectangle rect = new Rectangle((int)(x * i + w / 2 - waterWith / 2), m_Height / 2 - (waterHeight + 100) / 2, waterWith, waterHeight);
                                        if (rect.Y < 0)
                                            rect.Y = 0;
                                        Rectangle rect2 = new Rectangle((int)x * i, rect.Bottom, w, m_Height - rect.Bottom);
                                        if (rect2.Width < 0 || rect2.Height < 0)
                                            continue;
                                        g.DrawImage(DrawPos(SleepPos[m_FrameCnt - 1 + i], rect2.Width, rect2.Height), rect2, 0, 0, rect2.Width, rect2.Height, GraphicsUnit.Pixel, imageAtt);
                                        if (w < waterWith)
                                            continue;
                                        using (Bitmap waterImg = new Bitmap(waterWith, waterHeight))
                                        {
                                            using (Graphics gg = Graphics.FromImage(waterImg))
                                            {
                                                gg.DrawString(strStage, f, Brushes.Gray, new PointF(0, 0));
                                            }
                                            g.DrawImage(waterImg, rect, 0, 0, rect.Width, rect.Height, GraphicsUnit.Pixel, imageAtt);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                SizeF sf = g.MeasureString(SleepStage[m_FrameCnt - 1], f);
                                int waterWith = (int)(sf.Width + 4);
                                int waterHeight = (int)(f.Height + 4);
                                Rectangle rect = new Rectangle(m_Width / 2 - waterWith / 2, m_Height / 2 - (waterHeight + 100) / 2, waterWith, waterHeight);
                                if (rect.Y < 0)
                                    rect.Y = 0;
                                Rectangle rect2 = new Rectangle(0, rect.Bottom, m_Width, m_Height - rect.Bottom);
                                if (rect2.Width > 0 && rect2.Height > 0)
                                {
                                    g.DrawImage(DrawPos(SleepPos[m_FrameCnt - 1], rect2.Width, rect2.Height), rect2, 0, 0, rect2.Width, rect2.Height, GraphicsUnit.Pixel, imageAtt);
                                    if (waterWith != 4)
                                    {
                                        using (Bitmap waterImg = new Bitmap(waterWith, waterHeight))
                                        {
                                            using (Graphics gg = Graphics.FromImage(waterImg))
                                            {
                                                gg.DrawString(SleepStage[m_FrameCnt - 1], f, Brushes.Gray, new PointF(0, 0));
                                            }
                                            g.DrawImage(waterImg, rect, 0, 0, rect.Width, rect.Height, GraphicsUnit.Pixel, imageAtt);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        int len = BaseTimeLineSpan / 30;
                        int framecnt = m_FrameCnt;
                        int w = m_Width / len;
                        float fontSize = 80;
                        getFontSize(w, m_Height, ref fontSize);
                        using (Font f = new Font("微软雅黑", fontSize, FontStyle.Bold))
                        {
                            using (Pen pp = new Pen(new SolidBrush(Color.FromArgb(200, Color.Blue)), 1) { DashStyle = DashStyle.Dash })
                            {
                                int add = 0;
                                for (int i = 0; i <= len; i++)
                                {
                                    SizeF sf = g.MeasureString(SleepStage[framecnt - 1], f);
                                    int waterWith = (int)(sf.Width + 4);
                                    int waterHeight = (int)(f.Height + 4);
                                    DateTime next = m_StartTime.AddMilliseconds(framecnt * 30000);
                                    if ((next >= m_ViewStartTime && next <= m_ViewEndTime) || i == len)
                                    {
                                        float x = i == len ? m_Width : (float)((next - m_ViewStartTime).TotalMilliseconds * xAxis.ValueRate);
                                        if (i != len)
                                            g.DrawLine(pp, new PointF(x, 0), new PointF(x, m_Height));
                                        Rectangle rect = new Rectangle((int)(x + add) / 2 - waterWith / 2, m_Height / 2 - (waterHeight + 100) / 2, waterWith, waterHeight);//(waterHeight+100)其中100是体位需要的显示区域高度
                                        if (rect.Y < 0)
                                            rect.Y = 0;
                                        Rectangle rect2 = new Rectangle(add, rect.Bottom, w, m_Height - rect.Bottom);
                                        if (rect2.Width < 0 || rect2.Height < 0)
                                            continue;
                                        int www = (int)(x - add);
                                        if (www <= 0)
                                            break;
                                        g.DrawImage(DrawPos(SleepPos[framecnt - 1], www, rect2.Height), rect2, 0, 0, rect2.Width, rect2.Height, GraphicsUnit.Pixel, imageAtt);
                                        if (x - add < waterWith || waterWith == 4)
                                        {
                                            add = (int)x;
                                            goto Next;
                                        }
                                        if (rect.Left + rect.Width <= m_Width)
                                        {
                                            using (Bitmap waterImg = new Bitmap(waterWith, waterHeight))
                                            {
                                                using (Graphics gg = Graphics.FromImage(waterImg))
                                                {
                                                    gg.DrawString(SleepStage[framecnt - 1], f, Brushes.Gray, new PointF(0, 0));
                                                }
                                                g.DrawImage(waterImg, rect, 0, 0, rect.Width, rect.Height, GraphicsUnit.Pixel, imageAtt);
                                            }
                                        }
                                        add = (int)x;
                                    }
                                    Next: if ((++framecnt) > SleepStage.Length)
                                        break;
                                }
                            }
                        }
                    }
                }
                if (UserSelected && !MouseIsDown)
                    captainChange(new CaptainInfoUion() { ViewStartTime = m_ViewStartTime, ViewEndTime = m_ViewEndTime, showTyp = 0, CurrentFrameNo = m_FrameCnt });
            }
            return ret;
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
        /// 绘制体位图标
        /// </summary>
        /// <param name="typ"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private Image DrawPos(int typ, int width, int height)
        {
            string strValue = "";
            int angle = 0;
            ///"UP","L" , "R","P" ,"S"  
            switch (typ)
            {
                case 2:
                    strValue = "左侧";
                    angle = 90;
                    break;
                case 4:
                    strValue = "俯卧";
                    angle = 180;
                    break;
                case 3:
                    strValue = "右侧";
                    angle = 270;
                    break;
                case 5:
                    strValue = "坐立";
                    break;
                case 1:
                    strValue = "仰卧";
                    break;
            }
            Bitmap map = new Bitmap(width, height);
            map.MakeTransparent();
            if (typ == 0)
                return map;
            Rectangle rect;
            Bitmap body = new Bitmap(120, 120);
            using (Graphics g = Graphics.FromImage(body))
            {
                using (Pen p = new Pen(Brushes.DimGray, 2))
                {
                    int w = 80, h = 100;
                    if (w < map.Width - 20 && h < map.Height - 20)
                    {
                        rect = new Rectangle(body.Width / 2 - w / 2, body.Height / 2 - h / 2, w, h);
                        g.DrawEllipse(p, rect);
                        g.DrawEllipse(p, rect.X - 8, rect.Y + h / 2, 8, 20);
                        g.DrawEllipse(p, rect.X + w, rect.Y + h / 2, 8, 20);
                        if (typ != 5)
                        {
                            Point o = new Point(rect.X + w / 2, rect.Y - 10);
                            g.DrawCurve(p, new Point[] { o, new Point(o.X - 7, o.Y + 10) });
                            g.DrawCurve(p, new Point[] { o, new Point(o.X + 7, o.Y + 10) });
                        }
                        else
                        {
                            g.DrawEllipse(p, rect.X + w / 2 - 4, rect.Bottom - 6, 8, 6);
                        }
                    }
                    else
                    {
                        rect = new Rectangle(0, 0, body.Width, body.Height);
                    }
                }
            }
            Image ret = RotateImg(body, angle);
            using (Graphics g = Graphics.FromImage(ret))
            {
                using (Pen p = new Pen(Brushes.DimGray, 2))
                {
                    using (Font f = new Font("宋体", 18))
                    {
                        float waterWide = g.MeasureString(strValue, f).Width;
                        g.DrawString(strValue, f, p.Brush, new PointF(rect.X + rect.Width / 2 - waterWide / 2, rect.Y + rect.Height / 2 - f.Height / 2));
                    }
                }
            }
            using (Graphics g = Graphics.FromImage(map))
            {
                g.DrawImage(ret, map.Width / 2 - ret.Width / 2, 0);
                //DrawImage(ret, map.Width / 2 - ret.Width / 2, 0, g, 0, 0);
            }
            return map;
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
        /// 绘制定标的标记
        /// </summary>
        /// <param name="img"></param>
        private Image DrawCalibrationMarks()
        {
            Bitmap ret = new Bitmap(m_Width, m_Height);
            ret.MakeTransparent();
            if (!m_isRealDataCurve)///标记单独绘制
            {
                using (Graphics g = Graphics.FromImage(ret))
                {
                    for (int i = 0; i < m_curveItems.Count; i++)
                    {
                        if (!m_curveItems[i].Visible)
                            continue;
                        IEnumerable<IMarker> alls = getCalibrationMarks().Where(t => t.ID == m_curveItems[i].ID && (t is RectangleMarkers));
                        ///找出当前所有标记中将要在当前显示区域绘制出来的标记
                        foreach (IMarker maker in alls)
                        {
                            maker.CurrentHasMark = false;
                            RectangleMarkers mk = maker as RectangleMarkers;
                            ///凡是： 标记的起始索引大于当前区域的最大索引，标记的终止索引小于当前区域的起始索引，均不属于要显示的标记。
                            if (!(m_ViewStartTime >= mk.EndTime || m_ViewEndTime <= mk.StartTime))
                            {
                                double xoff = (m_ViewStartTime - mk.StartTime).TotalMilliseconds;
                                int x = (int)(xoff > 0 ? m_curveItems[i].xAxis.offSetDistance : Math.Abs(xoff) * m_curveItems[i].xAxis.ValueRate + m_curveItems[i].xAxis.offSetDistance);
                                int w = (int)(((m_ViewEndTime > mk.EndTime ? mk.EndTime : m_ViewEndTime) - (xoff > 0 ? m_ViewStartTime : mk.StartTime)).TotalMilliseconds * m_curveItems[i].xAxis.ValueRate + m_curveItems[i].xAxis.offSetDistance);
                                RectangleF rect = new RectangleF(x, m_curveItems[i].yAxis.offSetDistance, w, m_curveItems[i].yAxis.Displacement);
                                g.FillRectangle(new SolidBrush(Color.FromArgb(80, m_curveItems[i].PenColor)), rect);
                                float fontSize = (m_curveItems[i].yAxis.Displacement * 3 / 4);
                                fontSize = fontSize > 10 ? 10 : fontSize - 2;
                                using (Font f = new Font("宋体", fontSize))
                                {
                                    float strWidth = g.MeasureString(mk.Name, f).Width;
                                    g.DrawString(mk.Name, f, Brushes.DimGray, new PointF(rect.X + rect.Width / 2 - strWidth / 2, rect.Y + rect.Height / 2 - f.Height / 2));
                                }
                            }
                        }
                    }
                }
            }
            return ret;
        }
        /// <summary>
        /// 绘制定标的标记
        /// </summary>
        /// <param name="img"></param>
        private Image RealTimeDrawCalibrationMarks()
        {
            Bitmap ret = new Bitmap(m_Width, m_Height);
            ret.MakeTransparent();
            if (m_isRealDataCurve)///标记单独绘制
            {
                using (Graphics g = Graphics.FromImage(ret))
                {
                    for (int i = 0; i < m_curveItems.Count; i++)
                    {
                        if (!m_curveItems[i].Visible)
                            continue;
                        IEnumerable<IMarker> alls = getCalibrationMarks().Where(t => t.ID == m_curveItems[i].ID && (t is RectangleMarkers));
                        ///找出当前所有标记中将要在当前显示区域绘制出来的标记
                        foreach (IMarker maker in alls)
                        {
                            maker.CurrentHasMark = false;
                            RectangleMarkers mk = maker as RectangleMarkers;
                            if (mk.MarkCreatTime.Year == 1)
                                mk.EndTime = DateTime.Now;
                            ///凡是： 标记的起始索引大于当前区域的最大索引，标记的终止索引小于当前区域的起始索引，均不属于要显示的标记。
                            if (!(m_ViewStartTime >= mk.EndTime || m_ViewEndTime <= mk.StartTime))
                            {
                                double xoff = (m_ViewStartTime - mk.StartTime).TotalMilliseconds;
                                int x = (int)(xoff > 0 ? m_curveItems[i].xAxis.offSetDistance : Math.Abs(xoff) * m_curveItems[i].xAxis.ValueRate + m_curveItems[i].xAxis.offSetDistance);
                                int w = (int)(((m_ViewEndTime > mk.EndTime ? mk.EndTime : m_ViewEndTime) - (xoff > 0 ? m_ViewStartTime : mk.StartTime)).TotalMilliseconds * m_curveItems[i].xAxis.ValueRate + m_curveItems[i].xAxis.offSetDistance);
                                RectangleF rect = new RectangleF(x, m_curveItems[i].yAxis.offSetDistance, w, m_curveItems[i].yAxis.Displacement);
                                g.FillRectangle(new SolidBrush(Color.FromArgb(70, m_curveItems[i].PenColor)), rect);
                                float fontSize = (m_curveItems[i].yAxis.Displacement * 3 / 4);
                                fontSize = fontSize > 10 ? 10 : fontSize - 2;
                                using (Font f = new Font("宋体", fontSize))
                                {
                                    float strWidth = g.MeasureString(mk.Name, f).Width;
                                    if (strWidth < w)
                                        g.DrawString(string.Format("{0}({1:f2}s)", mk.Name, (mk.EndTime - mk.StartTime).TotalSeconds), f, Brushes.DimGray, new PointF(rect.X + rect.Width / 2 - strWidth / 2, rect.Y + rect.Height / 2 - f.Height / 2));
                                }
                            }
                        }
                    }
                    if (m_IsSelectRectange)
                    {
                        g.FillRectangle(new SolidBrush(Color.FromArgb(50, Color.Yellow)), m_SelectRectangeView);
                    }
                    else
                    {
                        m_SelectRangchannelID = -1;
                    }
                }
            }
            return ret;
        }
        #endregion
        #region 公有成员
        public delegate bool SelectRectangleKeyDownDelegate(string ChannelID, CurveItem mark);
        /// <summary>
        /// 快捷键标记事件
        /// </summary>
        public event SelectRectangleKeyDownDelegate SelectRectangleKeyDownHandler;
        /// <summary>
        /// 重绘完成
        /// </summary>
        public bool PaintReady
        {
            get
            {
                bool ret = m_freshOk;
                return ret;
            }
        }
        /// <summary>
        /// 标题信息体
        /// </summary>
        public class CaptainInfoUion
        {
            /// <summary>
            /// 当前屏幕的开始时间
            /// </summary>
            public DateTime ViewStartTime { set; get; }
            /// <summary>
            /// 当前屏幕的结束时间
            /// </summary>
            public DateTime ViewEndTime { set; get; }
            /// <summary>
            /// 当前时间显示中心位置
            /// </summary>
            public float NowLocationX = 0;
            /// <summary>
            /// 当前时间
            /// </summary>
            public DateTime CurrentTime { set; get; }
            /// <summary>
            /// 区域选择的开始时间
            /// </summary>
            public DateTime RectStartTime { set; get; }
            /// <summary>
            /// 区域选择的结束时间
            /// </summary>
            public DateTime RectEndTime { set; get; }
            /// <summary>
            /// 0-仅显示开始屏信息 1-显示当前时间 2-显示区域范围
            /// </summary>
            public int showTyp = 0;
            /// <summary>
            /// 当前帧序号
            /// </summary>
            public int CurrentFrameNo = 0;
            /// <summary>
            /// 附加文本
            /// </summary>
            public string Comments = "";
        }
        public delegate void SelectedHappenDelegate(int belong);
        /// <summary>
        /// 选中事件发生时
        /// </summary>
        public event SelectedHappenDelegate SelectedHappenHandler;
        private bool m_UserSelected = false;
        /// <summary>
        /// 是否为用户选择的当前面板
        /// </summary>
        public bool UserSelected
        {
            set
            {
                m_UserSelected = value;
                if (value)
                {
                    if (SelectedHappenHandler != null)
                    {
                        SelectedHappenHandler.Invoke(Belong);
                    }
                    //panel2.BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    //panel2.BorderStyle = BorderStyle.None;
                }
            }
            get
            {
                return m_UserSelected;
            }
        }
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
        private int m_BaseTimeLineSpan = 30;
        /// <summary>
        /// 时基s
        /// </summary>
        public int BaseTimeLineSpan
        {
            set
            {
                m_BaseTimeLineSpan = value;
                if (value < 30)
                {
                    m_moveTimeSpan = 1;
                }
                else if (value <= 120 && value >= 30)
                {
                    m_moveTimeSpan = 3;
                }
                else if (value > 120 && value < 360)
                {
                    m_moveTimeSpan = 10;
                }
                else
                {
                    m_moveTimeSpan = 30;
                }
                string strWater = "";
                if (value / 60 > 1 && value / 60 <= 60)
                    strWater = string.Format("{0} Min", value / 60);
                else if (value / 60 > 60)
                    strWater = string.Format("{0} H", value / 3600);
                else
                    strWater = string.Format("{0} S", value);
                m_strViewBaseTime = strWater;
                this.xAxis.MaxValue = value * 1000;
            }
            get
            {
                return m_BaseTimeLineSpan;
            }
        }
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
            }
            get
            {
                return m_TimeLineVisible;
            }
        }
        private bool m_markEnable = true;
        /// <summary>
        /// 事件使能
        /// </summary>
        public bool MarkEnable
        {
            set
            {
                m_markEnable = value;
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
            }
            get
            {
                return m_isRealDataCurve;
            }
        }
        public delegate void MarkerDeleteDelegate(IMarker mark);
        /// <summary>
        /// 标题绘图更新执行事件
        /// </summary>
        public event MarkerDeleteDelegate MarkerDeleteHandler;
        /// <summary>
        /// 归属等级 主/次 -0/1 
        /// </summary>
        public int Belong = 0;
        /// <summary>
        /// 默认背景色为黑
        /// </summary>
        private Color m_backcolor = System.Drawing.SystemColors.Window;
        /// <summary>
        /// 获取或设置背景色
        /// </summary>
        public new Color BackColor { set { m_backcolor = value; } get { return m_backcolor; } }
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

        public delegate void CaptainValueChangeDelegate(CaptainInfoUion info);
        /// <summary>
        /// 标题绘图更新执行事件
        /// </summary>
        public event CaptainValueChangeDelegate CaptainValueChangeHandler;

        public delegate List<CurveItem> getAllItemsDelegate(int belong);
        /// <summary>
        /// 获取当前曲线对象集合
        /// </summary>
        /// <returns></returns>
        public event getAllItemsDelegate getAllItemsHandle;
        /// <summary>
        /// 获取当前曲线对象集合
        /// </summary>
        /// <returns></returns>
        private List<CurveItem> getAllItems()
        {
            if (getAllItemsHandle != null)
                return getAllItemsHandle.Invoke(Belong);
            return new List<CurveItem>();
        }
        public delegate CurveItem getCurveItemDelegate(int ID);
        /// <summary>
        /// 获取当前曲线对象集合
        /// </summary>
        /// <returns></returns>
        public event getCurveItemDelegate getCurveItemHandle;

        /// <summary>
        /// 获取当前曲线对象
        /// </summary>
        /// <returns></returns>
        private CurveItem getCurveItem(int ID)
        {
            if (getCurveItemHandle != null)
                return getCurveItemHandle.Invoke(ID);
            return null;
        }
        public delegate List<IMarker> getGlobalMarkersDelegate();
        /// <summary>
        /// 获取当前全局标记对象集合
        /// </summary>
        /// <returns></returns>
        public event getGlobalMarkersDelegate getGlobalMarkersHandle;
        /// <summary>
        /// 获取当前全局标记对象集合
        /// </summary>
        /// <returns></returns>
        private List<IMarker> getGlobalMarkers()
        {
            if (getGlobalMarkersHandle != null)
                return getGlobalMarkersHandle.Invoke();
            return new List<IMarker>();
        }

        public delegate int[] getSleepPoseDelegate();
        /// <summary>
        /// 获取体位信息集合
        /// </summary>
        /// <returns></returns>
        public event getSleepPoseDelegate getSleepPoseHandle;
        /// <summary>
        /// 获取体位信息集合
        /// </summary>
        /// <returns></returns>
        private int[] getSleepPose()
        {
            if (getSleepPoseHandle != null)
                return getSleepPoseHandle.Invoke();
            return new int[0];
        }

        public delegate List<IMarker> getCalibrationMarksDelegate();
        /// <summary>
        /// 获取定标对象集合
        /// </summary>
        /// <returns></returns>
        public event getCalibrationMarksDelegate getCalibrationMarksHandle;
        /// <summary>
        /// 获取定标对象集合
        /// </summary>
        /// <returns></returns>
        private List<IMarker> getCalibrationMarks()
        {
            if (getCalibrationMarksHandle != null)
                return getCalibrationMarksHandle.Invoke();
            return new List<IMarker>();
        }

        public delegate string[] getSleepStageDelegate();
        /// <summary>
        /// 获取睡眠分期信息集合
        /// </summary>
        /// <returns></returns>
        public event getSleepStageDelegate getSleepStageHandle;
        /// <summary>
        /// 获取睡眠分期信息集合
        /// </summary>
        /// <returns></returns>
        private string[] getSleepStage()
        {
            if (getSleepStageHandle != null)
                return getSleepStageHandle.Invoke();
            return new string[0];
        }
        public delegate List<HotKey> getHotKeysDelegate();
        public event getHotKeysDelegate getHotKeysHandler;
        /// <summary>
        /// 获取事件热键清单
        /// </summary>
        /// <returns></returns>
        private List<HotKey> getHotKeys()
        {
            if (getHotKeysHandler != null)
            {
                return getHotKeysHandler.Invoke();
            }
            return new List<HotKey>();
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
        /// 主动刷新趋势图
        /// </summary>
        /// <param name="FrameCnt">当前帧序号</param>
        /// <param name="offsetTime">当前帧的偏移量</param>
        /// <returns>返回下一帧的帧序号</returns>
        public void Invalidate(DateTime startTime, int FrameCnt, int offsetTime, int TotalFrameCnt)
        {
            bak_frameCnt = m_FrameCnt = FrameCnt;
            bak_FrameOffTime = m_FrameOffTime = offsetTime;
            m_TotalFrameCnt = TotalFrameCnt;
            m_StartTime = startTime;
            if (this.Visible)
                AreaInvalidate(true);
        }
        private bool m_LockFresh = false;
        internal void AreaInvalidate(bool userRefresh)
        {
            if (!m_LockFresh)
            {
                m_userRefresh = userRefresh;
                if (m_IsSelectRectange)
                    m_IsSelectRectange = false;
                m_hasLine = false;
                m_currentItem = null;
                panel2.Invalidate();
            }
        }

        internal void AreaInvalidate()
        {
            if (!m_LockFresh)
            {
                m_IsSelectRectange = false;
                m_hasLine = false;
                m_currentItem = null;
                panel2.Invalidate();
            }
        }
        /// <summary>
        /// 触发按钮事件
        /// </summary>
        /// <param name="e"></param>
        internal void setKeyDown(KeyEventArgs e)
        {
            HotKey matKey = new HotKey();
            matKey.AltEnable = e.Alt;
            matKey.ControlEnable = e.Control;
            matKey.ShiftEnable = e.Shift;
            matKey.KeyCode = e.KeyCode.ToString();
            if (DeleteOne(matKey.KeyData))
            {
                return;
            }
            doKeyDwon(matKey);
        }

        public float m_VedioTimeLineX = 0;
        /// <summary>
        /// 视频播放的时间
        /// </summary>
        public DateTime VedioTimeLine
        {
            set
            {
                m_VedioTimeLineX = (float)((value - m_ViewStartTime).TotalMilliseconds * xAxis.ValueRate);
                if (m_VedioTimeLineX < 0 && m_VedioTimeLineX > m_Width)
                    m_VedioTimeLineX = 0;
            }
        }
        #endregion

        #region 获取当前可见时间段数据时触发
        internal delegate bool ReadChannelDataDelegate(DateTime start, DateTime end, List<int> channelIDs);
        /// <summary>
        /// 获取当前可见时间段数据时触发
        /// </summary>
        internal event ReadChannelDataDelegate ReadChannelDataHandler;
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
        internal delegate bool ReadChannelDataExDelegate(DateTime start, DateTime end, List<CurveItem> curveItems);
        /// <summary>
        /// 获取当前可见时间段数据时触发
        /// </summary>
        internal event ReadChannelDataExDelegate ReadChannelDataExHandler;

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

        /// <summary>
        /// 存放多次小睡事件
        /// </summary>
        private List<pChart.IMarker> MultipleSleepMarks = new List<pChart.IMarker>();
        public delegate List<IMarker> getMulSleepDelegate();

        public event getMulSleepDelegate getMulSleepHandler;

        /// <summary>
        /// 获取多次小睡的列表
        /// </summary>
        /// <returns></returns>
        private List<IMarker> getMulSleepMarks()
        {
            if (getMulSleepHandler != null)
                return getMulSleepHandler.Invoke();
            return new List<IMarker>();
        }
        /// <summary>
        /// 判断是否是分屏
        /// </summary>
        public bool isSplitscreen = false;
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
