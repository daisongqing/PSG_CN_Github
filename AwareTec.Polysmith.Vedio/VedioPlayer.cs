using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AwareTec.Polysmith.Vedio;
using System.IO;

namespace AwareTec.Polysmith.Vedio
{
    public partial class VedioPlayer : UserControl
    {
        #region 私有成员
        private bool m_init = false;
        private string m_RecordViewPath = "E:\\菲诗奥数据\\Media\\2021-07-20.mp4";
        /// <summary>
        /// 播放器控制面板
        /// </summary>
        private ControlPlayer m_player = null;
        /// <summary>
        /// 视频播放参数实例
        /// </summary>
        private MyConfiguration m_Configuration = null;
        /// <summary>
        /// 是否需要跳转视频
        /// </summary>
        private bool isjump = false;
        int zoomOffSet_W = 30;
        int zoomOffSet_H = 30;
        int zoomCount = 0;
        /// <summary>
        /// 视频画面的宽度
        /// </summary>
        private int m_viewWidth = 0;
        /// <summary>
        /// 视频界面的高度
        /// </summary>
        private int m_viewHeight = 0;
        /// <summary>
        /// 视频画面与实际展示的宽度比值
        /// </summary>
        private float m_WidthRate = 1;
        /// <summary>
        /// 视频画面与实际展示的高度比值
        /// </summary>
        private float m_HeightRate = 1;
        /// <summary>
        /// 历史回放是否已开启
        /// </summary>
        private bool m_RecordViewOpen = false;
        private int m_ZoomTotalCnt = 20;
        private Point m_ZoomCenterPoint = Point.Empty;
        private int bak_fastRate = 0;
        /// <summary>
        /// 获取进度的线程
        /// </summary>
        private System.Threading.Thread th = null;
        /// <summary>
        /// 中断任务标志
        /// </summary>
        private bool m_KillTask = true;
        /// <summary>
        /// 任务执行器
        /// </summary>
        private void TaskStart()
        {
            th = new System.Threading.Thread(() =>
            {
                while (!m_KillTask)
                {
                    System.Threading.Thread.Sleep(50);
                }
            });
            th.IsBackground = true;
            th.SetApartmentState(System.Threading.ApartmentState.MTA);
            th.Start();
        }
        /// <summary>
        /// 定时器任务（刷新播放帧率）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            int rate = VedioManagement.Default.FastRate;
            if (rate == 0)
            {
                if (label1.Visible)
                    label1.Visible = false;
            }
            else
            {
                if (!label1.Visible && rate != 1)
                {
                    label1.Visible = true;
                }
                if (rate != bak_fastRate)
                {
                    bak_fastRate = rate;

                    label1.Text = string.Format("速度 *{0}", rate < 0 ? string.Format("1/{0}", Math.Abs(rate)) : rate.ToString());
                }
            }
            timer1.Enabled = true;
        }
        #endregion

        #region 构造函数/Load/Dispose/Resize
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="myConfiguration"></param>
        public VedioPlayer(MyConfiguration myConfiguration)
        {
            m_Configuration = myConfiguration;
            InitializeComponent();
            this.Load += VedioPlayCtrl_Load;
            this.SizeChanged += VedioPlayCtrl_SizeChanged;
            this.Disposed += VedioPlayer_Disposed;
            this.panel1.MouseWheel += panel1_MouseWheel;
            panel1.Dock = DockStyle.Fill;
            m_player = new ControlPlayer();
            m_player.Dock = DockStyle.Bottom;
            m_player.ViewPlayHandle += player_ViewPlayHandle;
            m_player.TimeLineChangeHandler += player_TimeLineChangeHandler;
            m_player.ProcessValueChanged += Player_ProcessValueChanged;
            m_player.TotalTimes = (myConfiguration.EDFTotalTimes);
            m_player.MinimumSize = new Size(this.MinimumSize.Width, m_player.Height);
            this.Controls.Add(m_player);
        }

        private void VedioPlayer_Disposed(object sender, EventArgs e)
        {
            m_init = false;
        }

        private void VedioPlayCtrl_SizeChanged(object sender, EventArgs e)
        {
            if (!m_IsRealView)
            {
                if (m_RecordViewOpen)
                {
                    m_WidthRate = m_viewWidth * 1.0f / panel1.Width;
                    m_HeightRate = m_viewHeight * 1.0f / panel1.Height;
                }
            }
        }

        private void VedioPlayCtrl_Load(object sender, EventArgs e)
        {
            if (m_IsRealView)
            {
                string[] ss = m_Configuration.UrlPath.Split(':');
                if (ss.Length == 2)
                {
                    string ip = ss[0];
                    try
                    {
                        int port = int.Parse(ss[1]);
                        IntPtr hwd = panel1.Handle;
                        Task.Factory.StartNew(() =>
                        {
                            VedioManagement.Default.Login(ip, port, m_Configuration.UserName, m_Configuration.PassWord);
                            VedioManagement.Default.StartRealViewPlay(hwd);
                            VedioManagement.Default.StartRecord(m_Configuration.FileName, true);
                        });
                    }
                    catch { }
                }
            }
            else
            {
                m_RecordViewPath = m_Configuration.FileName;
                m_player.PlayStatusChanged += Player_PlayStatusChanged;
                timer1.Interval = 500;
                timer1.Start();
            }
            m_init = true;
            //if (m_KillTask)
            //{
            //    m_KillTask = false;
            //    TaskStart();
            //}
        }
        /// <summary>
        /// 画面放大缩小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!m_IsRealView)
            {
                if (zoomCount == 0)
                {
                    VedioManagement.Default.getPictrueSize(ref m_viewWidth, ref m_viewHeight);
                    zoomOffSet_W = m_viewWidth / m_ZoomTotalCnt;
                    zoomOffSet_H = m_viewHeight / m_ZoomTotalCnt;
                    m_WidthRate = m_viewWidth * 1.0f / panel1.Width;
                    m_HeightRate = m_viewHeight * 1.0f / panel1.Height;
                    m_ZoomCenterPoint = new Point((int)(e.X * m_WidthRate), (int)(e.Y * m_HeightRate));
                }
                if (e.Delta > 0)
                {
                    if (zoomCount < m_ZoomTotalCnt - 1)
                    {
                        zoomCount++;
                    }
                }
                else
                {
                    if (zoomCount > 0)
                    {
                        if (zoomCount == m_ZoomTotalCnt)
                            zoomCount = m_ZoomTotalCnt - 2;
                        else
                        {
                            zoomCount--;
                        }
                    }
                }
                int w2 = ((m_ZoomTotalCnt - zoomCount) * zoomOffSet_W);
                int h2 = ((m_ZoomTotalCnt - zoomCount) * zoomOffSet_H);
                int x = m_ZoomCenterPoint.X - w2 / 2;
                if (x < 0)
                {
                    x = 0;
                }
                else if (x + w2 > m_viewWidth)
                {
                    x = m_viewWidth - w2;
                }
                int y = m_ZoomCenterPoint.Y - h2 / 2;
                if (y < 0)
                    y = 0;
                else if (y + h2 > m_viewHeight)
                    y = m_viewHeight - h2;
                Rectangle rang = new Rectangle(x, y, w2 + x, h2 + y);
                VedioManagement.Default.ReviewZoom(ref rang, panel1.Handle);
            }
            else
            {
                VedioManagement.Default.ReviewZoom(e.Delta > 0);
            }
        }
        #endregion

        #region controlPlay事件触发时具体实现
        /// <summary>
        /// 视频进度被拖动时发生
        /// </summary>
        /// <param name="currentTime"></param>
        /// <param name="Pause"></param>
        private void Player_ProcessValueChanged(int currentTime, bool Pause)
        {
            isjump = true;
            m_currentPlayTimes = currentTime;
        }
        /// <summary>
        /// 视频时间线变化时发生
        /// </summary>
        /// <param name="offsetTime"></param>
        private void player_TimeLineChangeHandler(int offsetTime)
        {
            if (isjump)
                return;
            if (TimeLineChangeHandler != null)
            {
                TimeLineChangeHandler.Invoke(offsetTime);
            }
            m_currentPlayTimes = offsetTime;
            if (offsetTime >= m_Configuration.EDFTotalTimes)///关闭屏幕
            {
                this.Invoke(new MethodInvoker(()=>{
                    if (panel1.Visible)
                        panel1.Visible = false;
                }));
                m_Configuration.Reset();
            }
        }
        /// <summary>
        /// 重放时发生
        /// </summary>
        /// <param name="millsecs"></param>
        private void player_ViewPlayHandle(int millsecs)
        {
            SetCurrentTime(millsecs);
        }
        /// <summary>
        /// 监听到播放状态改变时执行
        /// </summary>
        /// <param name="playstatus"></param>
        private void Player_PlayStatusChanged(bool playstatus)
        {
            if (PlayStatusChanged != null)
            {
                PlayStatusChanged.Invoke(playstatus);
            }
        }
        #endregion

        #region 公有成员
        private int m_StartPlayTime = 0;
        /// <summary>
        /// 起始播放的时间节点
        /// </summary>
        public int StartPlayTime
        {
            set
            {
                m_currentPlayTimes = m_StartPlayTime = value;
            }
        }
        /// <summary>
        /// 设置当前时间基数
        /// </summary>
        public DateTime StartViewTime
        {
            set
            {
                m_player.StartViewTime = value;
            }
        }
        private bool m_IsRealView = false;
        /// <summary>
        /// 是否为视频实时预览
        /// </summary>
        public bool IsRealView
        {
            set
            {
                m_IsRealView = value;
                m_player.IsRealView = value;
            }
            get
            {
                return m_IsRealView;
            }
        }
        private int m_currentPlayTimes = 0;
        /// <summary>
        /// 获取当前的播放进度
        /// </summary>
        public int CurrentPlayTimes
        {
            get
            {
                return m_currentPlayTimes;
            }
        }
        /// <summary>
        /// 获取文件存储目录
        /// </summary>
        public string VedioSavePath
        {
            get
            {
                return m_Configuration.SaveDirectory;
            }
        }
        /// <summary>
        /// 键触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void KeyDown(object sender, KeyEventArgs e)
        {
            m_player.ControlPlayer_KeyDown(sender, e);
        }

        private bool m_backClear = false;
        /// <summary>
        /// 指定时间播放
        /// </summary>
        /// <param name="millsSeconds"></param>
        /// <param name="jump"></param>
        public void SetCurrentTime(int millsSeconds, bool jump = false)
        {
            DateTime dt = m_player.StartViewTime.AddMilliseconds(millsSeconds);
            VedioUion find = m_Configuration.getVedioPlay(dt);
            if (find != null)
            {
                if (!find.Running)
                {
                    IntPtr hwd = IntPtr.Zero;
                    this.Invoke(new MethodInvoker(() =>
                    {
                        if (!panel1.Visible)
                            panel1.Visible = true;
                        hwd = panel1.Handle;
                    }));
                    m_player.OffSetMillsSeconds = (int)(find.StartTime - m_player.StartViewTime).TotalMilliseconds;
                    m_RecordViewPath = find.Path;
                    DateTime dt2 = DateTime.Now;
                    if (m_player.StartRecordView(find.Path, hwd, millsSeconds))
                    {
                        find.Running = true;
                        m_backClear = false;
                    }
                    System.Diagnostics.Trace.WriteLine(string.Format("打开视频耗时：{0}ms", (DateTime.Now - dt2).TotalMilliseconds));
                    return;
                }
                if (jump || isjump)
                    m_player.SetCurrentTime(millsSeconds);
            }
            else
            {
                if (!m_backClear)
                {
                    m_backClear = true;
                    this.Invoke(new MethodInvoker(() =>
                    {
                        panel1.Visible = false;
                    }));
                }
                if (millsSeconds >= m_Configuration.EDFTotalTimes)
                {
                    m_player.SetCurrentTime(millsSeconds);
                }
                else
                {
                    m_player.UpdateTimeLine(millsSeconds);
                }
            }
            m_currentPlayTimes = millsSeconds;
            isjump = false;
        }
        /// <summary>
        /// 方向控制前后帧播放 typ-1 left  typ-2 right
        /// </summary>
        /// <param name="typ"></param>
        public void LeftRightKeyControl(int typ)
        {
            m_player.LeftRightKeyControl(typ);
        }
        /// <summary>
        /// 清除资源占用
        /// </summary>
        public void CloseAll()
        {
            m_player.ClearAll();
            m_Configuration.Reset();
            VedioManagement.Default.Dispose();
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void myDispose()
        {
            try
            {
                CloseAll();
            }
            catch { }
            this.Dispose();
        }
        #endregion

        #region 事件定义
        /// <summary>
        /// 播放状态发生变化时 触发
        /// </summary>
        public event Action<bool> PlayStatusChanged;
        /// <summary>
        /// 播放进度发生变化时触发
        /// </summary>
        public event Action<int> TimeLineChangeHandler;
        #endregion
    }
}
