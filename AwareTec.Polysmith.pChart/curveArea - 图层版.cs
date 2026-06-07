using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

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
            xAxis = new XAxis() { BackColor = Color.WhiteSmoke, ForeColor = Color.LimeGreen, AxisVisible = true, CalibrationsVisible = true, Displacement = m_Width, MaxValue = 30000 };
            yAxis = new YAxis() { BackColor = Color.WhiteSmoke, ForeColor = Color.LimeGreen, AxisVisible = true, Displacement = m_Height };
            this.Load += curveArea_Load;
            BaseTimeLineSpan = 30;
            imageAtt = GetAlphaImgAttr(40);
        }

        private void curveArea_Resize(object sender, EventArgs e)
        {
            m_Width = this.Width;
            m_Height = this.Height;
            ///xy坐标需要重新界定
            xAxis.Displacement = m_Width;
            yAxis.Displacement = m_Height;
            m_rectangle = new Rectangle(new Point(0, 0), new Size(m_Width, m_Height));
            bak_mousePoint = new Point(-10, 0);
            AreaInvalidate(true);
        }

        private void curveArea_Load(object sender, EventArgs e)
        {
            this.Resize += curveArea_Resize;
            this.panel2.Paint += view_Paint;
            this.panel2.MouseLeave += view_MouseLeave;
            this.panel2.MouseDown += view_MouseDown;
            this.panel2.MouseMove += view_MouseMove;
            this.panel2.MouseUp += view_MouseUp;
            //this.panel2.MouseEnter += panel2_MouseEnter;
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
                m_MoveTimer.Interval = 1000;
                int with2 = panel2.Width;
                if (m_FrameRightMove && m_FrameCnt > 0 || (m_FrameLeftMove && m_FrameCnt > 0) && !(m_FrameCnt == 1 && m_FrameOffTime == 0))
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
                    AreaInvalidate(true);
                    m_RectEndTime = m_RectEndTime.AddSeconds(add);
                    captainChange(new CaptainInfoUion() { ViewStartTime = m_ViewStartTime, ViewEndTime = m_ViewEndTime, RectStartTime = m_RectStartTime, RectEndTime = m_RectEndTime, showTyp = 2 });
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
                    System.Threading.Thread.Sleep(500);
                    DrawRectangle(m_NewMouseRect);
                    //m_NewMouseRectVisible = true;
                    Console.WriteLine(string.Format("{0},{1}({2}*{3})", m_NewMouseRect.X, m_NewMouseRect.Y, m_NewMouseRect.Width, m_NewMouseRect.Height));
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
                //Application.DoEvents();
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
        private Bitmap m_AllIamge = null;
        private bool m_timelineMove = false;
        private bool hasAllImage = false;
        /// <summary>
        /// 重绘时发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void view_Paint(object sender, PaintEventArgs e)
        {
            DateTime ddt = DateTime.Now;
            m_freshOk = false;
            try
            {
                m_curveItems = getAllItems();
                if (!m_timelineMove || !hasAllImage)
                {
                    m_AllIamge = new Bitmap(m_Width, m_Height);
                    m_AllIamge.MakeTransparent();
                    using (Graphics gl = Graphics.FromImage(m_AllIamge))
                    {
                        if (m_userRefresh)
                        {
                            m_SourceImage = GetMap();
                        }
                        if (m_SourceImage == null)
                            m_SourceImage = GetMap();
                        gl.DrawImage(m_SourceImage, 0, 0);
                        if (m_isRealDataCurve)
                            gl.DrawImage(RealTimeDrawCalibrationMarks(), 0, 0);
                        else
                        {
                            gl.DrawImage(DrawCalibrationMarks(), 0, 0);
                            gl.DrawImage(DrawStage(), 0, 0);
                            gl.DrawImage(DrawMarkers(m_SourceImage), 0, 0);
                            gl.DrawImage(DrawFlags(), 0, 0);
                        }
                    }
                    e.Graphics.DrawImage(m_AllIamge, 0, 0);
                    hasAllImage = true;
                }
                else
                {
                    //if (e.ClipRectangle.Width==m_Width)
                    e.Graphics.DrawImage(m_AllIamge, 0, 0);
                    //else
                    //    e.Graphics.DrawImage(m_AllIamge, e.ClipRectangle, e.ClipRectangle, GraphicsUnit.Pixel);
                    using (Pen p = new Pen(Brushes.Red, 1))
                    {
                        p.DashStyle = DashStyle.Dash;
                        p.DashPattern = new float[] { 3, 3 };
                        e.Graphics.DrawLine(p, new Point(Current_mousePoint.X, ClientRectangle.Top), new Point(Current_mousePoint.X, ClientRectangle.Bottom));
                        bak_mousePoint = Current_mousePoint;
                    }
                    m_timelineMove = false;
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
            e.Dispose();
            m_freshOk = true;
            Console.WriteLine(string.Format("耗时{0}:执行一次曲线绘制", (DateTime.Now - ddt).TotalMilliseconds));
            m_userRefresh = false;
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
        private void view_MouseUp(object sender, MouseEventArgs e)
        {
            bool selectMark = false;
            if (!m_isRealDataCurve)
            {
                m_MoveTimer.Stop();
                if (m_SelectMark != null)
                {
                    m_SelectMark.isSelected = false;
                    toolTip1.Hide(this);
                    toolTip1.RemoveAll();
                    m_SelectMark = null;
                    selectMark = true;
                    return;
                }
            }
            CurveItem item = m_curveItems.Find(t => t.ClientRectangle.Contains(MouseIsDown ? m_SatrtMouseRect.Location : e.Location) && t.Visible);
            if (item != null)
            {
                CurveItem find = item.Clone(false);
                if (e.Button == MouseButtons.Left && m_NewMouseRect.Width != 0)
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
                            rmk = new RectangleMarkers() { StartIndex = (int)((m_RectStartTime - m_StartTime).TotalMilliseconds / find.TimeSpan), EndIndex = (int)((m_RectEndTime - m_StartTime).TotalMilliseconds / find.TimeSpan) };
                            if (rmk.EndIndex < rmk.StartIndex)
                            {
                                int sk = rmk.EndIndex;
                                rmk.EndIndex = rmk.StartIndex;
                                rmk.StartIndex = sk;
                            }
                            int cp = DateTime.Compare(m_RectStartTime, m_RectEndTime);
                            rmk.StartTime = cp > 0 ? m_RectEndTime : m_RectStartTime;
                            rmk.EndTime = cp > 0 ? m_RectStartTime : m_RectEndTime;
                            rmk.ID = string.Format("{0}:{1}-{2}", find.ID, rmk.StartIndex, rmk.EndIndex);
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
                        }
                        find.m_Marks.Add(rmk);
                        m_TimeLineEnable = false;
                        ChannelViewPopupHandler.Invoke(find, e.Button);
                    }
                }
                else if (e.Button == MouseButtons.Right && m_NewMouseRect.Width == 0 && !m_isRealDataCurve && !selectMark)
                {
                    StringMarkers sm = new StringMarkers();
                    float add = e.X / xAxis.ValueRate;
                    sm.MarkCreatTime = m_ViewStartTime.AddMilliseconds(add);
                    sm.ID = string.Format("All:{0}", sm.MarkCreatTime.ToOADate());
                    sm.Location = e.Location;
                    int ts = (int)(sm.MarkCreatTime - m_StartTime).TotalMilliseconds;
                    int fcnt = ts / 30000;
                    fcnt = ts % 30000 == 0 ? fcnt : fcnt + 1;
                    sm.StartFrameNo = fcnt;
                    sm.EndFrameNo = fcnt;
                    find.m_Marks.Add(sm);
                    m_TimeLineEnable = false;
                    //ChannelViewPopupHandler.BeginInvoke(find, e.Button, new AsyncCallback(UpdateViewIAsyncResult), find);
                    ChannelViewPopupHandler.Invoke(find, e.Button);
                }
                UpdateViewIAsyncResult();
            }
            if (m_isRealDataCurve)
            {
                m_freshOk = true;
            }
            if (e.Button == MouseButtons.Left)
            {
                MouseIsDown = false;
                panel2.Capture = false;
                Cursor.Clip = Rectangle.Empty;
            }
            m_currentItem = null;
        }
        /// <summary>
        /// 是否需要显示时间轴刻度
        /// </summary>
        private bool m_TimeLineVisible = false;
        private Point bak_mousePoint = new Point(-10, 0);
        private Point Current_mousePoint = new Point(-10, 0);
        /// <summary>
        /// 备份当前帧
        /// </summary>
        private int bak_frameCnt = 1;
        private bool m_FrameRightMove = false;
        private bool m_FrameLeftMove = false;
        private void view_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_hasDialog)
                return;
            bool isContains = this.ClientRectangle.Contains(e.X, e.Y);
            if (MouseIsDown && e.Button == MouseButtons.Left)
            {
                ResizeToRectangle(e.Location, ref  m_NewMouseRect);
                m_NewMouseRectVisible = true;
                if (!m_isRealDataCurve)
                {
                    ///区域框当前时间
                    m_RectEndTime = m_ViewStartTime.AddMilliseconds(e.X / xAxis.ValueRate);
                    if (m_RectEndTime <= m_RectMouseDownViewEndTime && bak_frameCnt < m_FrameCnt)
                    {
                        m_FrameCnt = bak_frameCnt;
                        m_FrameOffTime = bak_FrameOffTime;
                        m_NewMouseRect.X = m_SatrtMouseRect.X;
                        m_NewMouseRect.Width = 0 - m_SatrtMouseRect.X;
                        AreaInvalidate(true);
                    }
                    captainChange(new CaptainInfoUion() { ViewStartTime = m_ViewStartTime, ViewEndTime = m_ViewEndTime, RectStartTime = m_RectStartTime, RectEndTime = m_RectEndTime, showTyp = 2 });
                    m_FrameLeftMove = e.X <= 2;
                    m_FrameRightMove = e.X >= panel2.Width - 2;
                    if (m_FrameLeftMove || m_FrameRightMove)
                    {
                        if (!m_MoveTimer.Enabled)
                        {
                            Console.WriteLine(string.Format("{0}-{1}({2},{3})", m_FrameLeftMove, m_FrameRightMove, e.X, e.Y));
                            m_MoveTimer.Interval = 1000;
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
                if (m_TimeLineVisible && !MouseIsDown && m_TimeLineEnable)
                {
                    if (!m_timelineMove)
                    {
                        Current_mousePoint = e.Location;
                        m_timelineMove = true;
                        //AreaInvalidate(false);
                        {
                            Point current = panel2.PointToClient(Cursor.Position);
                            float add = current.X < 0 ? 0 : current.X / xAxis.ValueRate;
                            DateTime dt = m_ViewStartTime.AddMilliseconds(add);
                            captainChange(new CaptainInfoUion() { ViewStartTime = m_ViewStartTime, ViewEndTime = m_ViewEndTime, CurrentTime = dt, NowLocationX = current.X, showTyp = 1, CurrentFrameNo = m_FrameCnt });

                        }
                    }
                }
                else
                {
                    Cursor = Cursors.Cross;
                }
            }
            else
            {
                Cursor = Cursors.Default;
            }
        }
        private void view_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            m_timelineMove = false;
            bak_mousePoint.X = -10;
            AreaInvalidate(false);
            captainChange(new CaptainInfoUion() { ViewStartTime = m_ViewStartTime, ViewEndTime = m_ViewEndTime, showTyp = 0, CurrentFrameNo = m_FrameCnt });
        }

        private DateTime m_RectMouseDownViewEndTime;

        private void view_MouseDown(object sender, MouseEventArgs e)
        {
            m_timelineMove = false;
            {
                m_currentItem = m_curveItems.Find(t => t.Visible && t.ClientRectangle.Contains(e.Location));
                if (!m_isRealDataCurve)
                {
                    ///区域框开始时间
                    float add = e.X / xAxis.ValueRate;
                    m_RectMouseDownViewEndTime = m_ViewEndTime;
                    m_RectEndTime = m_RectStartTime = m_ViewStartTime.AddMilliseconds(add);
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
                        IMarker[] marks = m_currentItem.m_Marks.Where(t => t.CurrentHasMark).OrderBy(t => t.CurrentStartIndex).ToArray();
                        int len = marks.Length;
                        for (int i = 0; i < len; i++)
                        {
                            if (showTips(panel2, m_currentItem.ID, marks[i], e))
                            {
                                return;
                            }
                        }
                    }
                }
                if (e.Button == MouseButtons.Right)
                {
                    m_freshOk = false;
                }
                else
                {
                    if (!UserSelected)
                        UserSelected = true;
                    else
                        MouseIsDown = true;
                }
                if (m_TimeLineVisible)
                {
                    AreaInvalidate(false);
                }
                DrawStart(e.Location);
            }
        }
        private bool m_hasDialog = false;
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
                    RectangleMarkers rmk = mark as RectangleMarkers;
                    toolTip1.Show(string.Format("起始时间：{0}\r\n结束时间：{1}\r\n事件名称：{2}\r\n事件描述：{3}", rmk.StartTime, rmk.EndTime, rmk.Name, rmk.Description), selectpanel, new Point(e.X, e.Y));
                }
                else
                    toolTip1.Show(string.Format("发生时间：{0}\r\n事件名称：{1}\r\n事件描述：{2}", mark.MarkCreatTime, mark.Name, mark.Description), selectpanel, new Point(e.X, e.Y));
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
            m_currentItem = null;
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
            m_currentItem = null;
        }
        private CurveItem m_currentItem = null;
        private void ResizeToRectangle(Point p, ref Rectangle rect)
        {
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
        private void DrawRectangle(Rectangle rect2)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                Rectangle rect = panel2.RectangleToScreen(rect2);
                ControlPaint.FillReversibleRectangle(rect, Color.Blue);
            }));
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
            if (m_currentItem != null)
            {
                StartPoint = new Point(StartPoint.X, (int)m_currentItem.ClientRectangle.Top);
                return new Point(EndPoint.X > m_currentItem.ClientRectangle.Right ? (int)m_currentItem.ClientRectangle.Right : EndPoint.X < m_currentItem.ClientRectangle.Left ? (int)m_currentItem.ClientRectangle.Left : EndPoint.X, (int)m_currentItem.ClientRectangle.Bottom);
            }
            return EndPoint;
        }
        #endregion
        #region 私有方法
        /// <summary>
        /// 获取适宜的字体大小
        /// </summary>
        /// <param name="with"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private void getFontSize(int with,int height,ref float refontSize)
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
                    int viewStartTime = ((m_FrameCnt - 1) * m_OneFrameSpanTime + m_FrameOffTime);
                    m_ViewStartTime = m_StartTime.AddSeconds(viewStartTime);///显示区域开始时间
                    m_ViewEndTime = m_ViewStartTime.AddSeconds(m_BaseTimeLineSpan);///显示区域结束时间
                    if (UserSelected)
                        captainChange(new CaptainInfoUion() { ViewStartTime = m_ViewStartTime, ViewEndTime = m_ViewEndTime, showTyp = 0, CurrentFrameNo = m_FrameCnt });
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

                            using (Graphics gg = Graphics.FromImage(ret))
                            {
                                itemPointUion[idx] = m_isRealDataCurve ? m_curveItems[idx].GetxyDataValues() : m_curveItems[idx].GetOneFramePoints(m_FrameCnt, BaseTimeLineSpan, m_FrameOffTime);
                                ///绘制通道的刻度尺
                                m_curveItems[idx].yAxis.drawyAxis(gg, docksize, w, h);
                                using (Pen pointpen = new Pen(m_curveItems[idx].isSelected ? Color.LightSkyBlue : m_curveItems[idx].PenColor, m_curveItems[idx].PenWidth))
                                {
                                    if (m_curveItems[idx].IsShowValue && itemPointUion[idx] != null)
                                    {
                                        PointF[] source = itemPointUion[idx].SourcePoints;
                                        ///绘制曲线点
                                        PointF[] datapoints = itemPointUion[idx].Points;
                                        #region 抽点显示带值得曲线
                                        int datalength = datapoints.Length;
                                        if (datalength > 1)
                                            using (Font font = new Font("宋体", 9))
                                            {
                                                pointpen.EndCap = LineCap.RoundAnchor;
                                                float yvalue = 0;
                                                int dcnt = w / ((int)gg.MeasureString("99", font).Width + 1);///最多显示的点数量
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
                                            }
                                        #endregion
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
                                        if (m_isRealDataCurve)
                                        {
                                            PointF[] datapoints = itemPointUion[idx].Points;
                                            if (datapoints.Length > 1)
                                            {
                                                gg.DrawLines(pointpen, datapoints);
                                                // g.DrawLines(pointpen, datapoints);
                                            }
                                        }
                                        else
                                        {
                                            if (itemPointUion[idx] != null)
                                            {
                                                if (itemPointUion[idx].Points.Length > 0)
                                                    ///先绘制出当前区域的曲线
                                                    gg.DrawLines(pointpen, itemPointUion[idx].Points);
                                            }
                                        }

                                    }
                                }
                            }
                            lock (objLock)
                            {
                                // channelmap.Add(ret);
                                //if (m_curveItems[idx].Antipole)
                                //    g.DrawImage(RotateImg(ret, 180), 0, 0);
                                //else
                                g.DrawImage(ret, 0, 0);
                                ret.Dispose();
                            }
                        }
                    }, i);
                    tt[i].Start();
                }
                Task.WaitAll(tt);///等待所有异步线程完成
                Console.WriteLine(string.Format("多线程绘制耗时：{0}ms", (DateTime.Now - dt).TotalMilliseconds));
                for (int i = 0; i < tt.Length; i++)
                {
                    tt[i].Dispose();///释放线程资源
                }
                g.Clip.Dispose();
                srcImg.Tag = itemPointUion;
            }
            return (srcImg);
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
                for (int s = 0; s < channel.m_Marks.Count; s++)
                {
                    IMarker maker = channel.m_Marks[s];
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
                            mk.CurrentEndIndex = sup.EndIndex > mk.EndIndex ? mk.EndIndex - sup.StartIndex - 1 : sup.Points.Length - 1;
                            mk.CurrentHasMark = true;
                        }
                    }
                }
                ///把要显示的标记按照起始索引排序
                IMarker[] marks = channel.m_Marks.Where(t => t.CurrentHasMark).OrderBy(t => t.CurrentStartIndex).ToArray();
                int len = marks.Length;
                for (int s = 0; s < len; s++)
                {
                    if (marks[s] is RectangleMarkers)
                    {
                        RectangleMarkers mk = marks[s] as RectangleMarkers;
                        mk.HeadRectangle = new RectangleF(sup.Points[mk.CurrentStartIndex].X, channel.yAxis.offSetDistance, sup.Points[mk.CurrentEndIndex].X - sup.Points[mk.CurrentStartIndex].X, channel.yAxis.Displacement);
                        g.FillRectangle(new SolidBrush(Color.FromArgb(mk.isSelected ? 205 : 105, mk.ForeColor)), mk.HeadRectangle);
                        //PointF[] lines2 = new PointF[marks[s].CurrentEndIndex - marks[s].CurrentStartIndex + 1];
                        //if (lines2.Length > 1)
                        //{
                        //    Array.Copy(sup.Points, mk.CurrentStartIndex, lines2, 0, lines2.Length);
                        //    using (Pen cp = new Pen(mk.ForeColor, 1))
                        //    {
                        //        ///绘制标记部分
                        //        g.DrawLines(cp, lines2);
                        //        ///看看当前区域是否适合绘制标记描述
                        //        float distance = (sup.Points[mk.CurrentEndIndex].X - sup.Points[mk.CurrentStartIndex].X);
                        //        SizeF sf = g.MeasureString(mk.Name, mk.Font);
                        //        float waterWith = sf.Width + 4;
                        //        float waterHeight = sf.Height + 4;
                        //        using (Pen sp = new Pen(Color.FromArgb(mk.isSelected ? 255 : 125, mk.BackColor), mk.isSelected ? 3 : 1))
                        //        {
                        //            if (waterWith < distance)
                        //            {
                        //                RectangleF rect = new RectangleF(sup.Points[mk.CurrentStartIndex].X + distance / 2 - waterWith / 2, channel.yBaseDistance - waterHeight / 2, waterWith, waterHeight);
                        //                mk.HeadRectangle = rect;
                        //                g.FillRectangle(new SolidBrush(Color.FromArgb(mk.isSelected ? 205 : 105, mk.ForeColor)), rect);
                        //                g.DrawString(mk.Name, mk.Font, new SolidBrush(Color.FromArgb(mk.isSelected ? 255 : 155, Color.Black)), new PointF(rect.Left + 2, rect.Top + 2));
                        //                g.DrawLine(sp, new PointF(sup.Points[mk.CurrentStartIndex].X, channel.yBaseDistance + 3), new PointF(sup.Points[mk.CurrentStartIndex].X, channel.yBaseDistance - 3));
                        //                g.DrawLine(sp, new PointF(sup.Points[mk.CurrentStartIndex].X, channel.yBaseDistance), new PointF(rect.Left, channel.yBaseDistance));
                        //                g.DrawLine(sp, new PointF(sup.Points[mk.CurrentEndIndex].X, channel.yBaseDistance), new PointF(rect.Right, channel.yBaseDistance));
                        //                g.DrawLine(sp, new PointF(sup.Points[mk.CurrentEndIndex].X, channel.yBaseDistance + 3), new PointF(sup.Points[mk.CurrentEndIndex].X, channel.yBaseDistance - 3));
                        //            }
                        //            else
                        //            {
                        //                mk.HeadRectangle = new RectangleF(sup.Points[mk.CurrentStartIndex].X, channel.yTop, sup.Points[mk.CurrentEndIndex].X - sup.Points[mk.CurrentStartIndex].X, channel.yAxis.Displacement);
                        //                g.FillRectangle(new SolidBrush(Color.FromArgb(mk.isSelected ? 205 : 105, mk.ForeColor)), mk.HeadRectangle);
                        //                //g.DrawLine(sp, new PointF(sup.Points[mk.CurrentStartIndex].X, channel.yBaseDistance), new PointF(sup.Points[mk.CurrentEndIndex].X, channel.yBaseDistance));
                        //            }
                        //        }
                        //    }
                        //}
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
                                    if ((next > m_ViewStartTime && next < m_ViewEndTime) || i == len)
                                    {
                                        float x = i == len ? m_Width : (float)((next - m_ViewStartTime).TotalMilliseconds * xAxis.ValueRate);
                                        if (i != len)
                                            g.DrawLine(pp, new PointF(x, 0), new PointF(x, m_Height));
                                        Rectangle rect = new Rectangle((int)(x + add) / 2 - waterWith / 2, m_Height / 2 - (waterHeight + 100) / 2, waterWith, waterHeight);//(waterHeight+100)其中100是体位需要的显示区域高度
                                        if (rect.Y < 0)
                                            rect.Y = 0;
                                        Rectangle rect2 = new Rectangle(add, rect.Bottom, w, m_Height - rect.Bottom);
                                        g.DrawImage(DrawPos(SleepPos[framecnt - 1], (int)(x - add), rect2.Height), rect2, 0, 0, rect2.Width, rect2.Height, GraphicsUnit.Pixel, imageAtt);
                                        if (x - add < waterWith || waterWith == 4)
                                        {
                                            add = (int)x;
                                            goto Next;
                                        }
                                        using (Bitmap waterImg = new Bitmap(waterWith, waterHeight))
                                        {
                                            using (Graphics gg = Graphics.FromImage(waterImg))
                                            {
                                                gg.DrawString(SleepStage[framecnt - 1], f, Brushes.Gray, new PointF(0, 0));
                                            }
                                            g.DrawImage(waterImg, rect, 0, 0, rect.Width, rect.Height, GraphicsUnit.Pixel, imageAtt);
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
                }
            }
            return ret;
        }
        #endregion
        #region 公有成员
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
                    panel2.BorderStyle = BorderStyle.Fixed3D;
                }
                else
                {
                    panel2.BorderStyle = BorderStyle.None;
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
            AreaInvalidate(true);

        }
        internal void AreaInvalidate(bool userRefresh)
        {
            m_userRefresh = userRefresh;
            panel2.Invalidate();
        }
        #endregion
    }
}
