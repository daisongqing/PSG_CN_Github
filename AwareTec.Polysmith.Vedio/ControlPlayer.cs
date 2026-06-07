using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Windows.Forms.Ctrl;
using System.IO;
using System.Runtime.InteropServices;
using DotNetCtl.CSharpWin;
using System.Diagnostics;

namespace AwareTec.Polysmith.Vedio
{
    public partial class ControlPlayer : UserControl
    {
        /// <summary>
        /// 时间单元
        /// </summary>
        private class TimeUion
        {
            public int hour { set; get; }

            public int min { set; get; }

            public int seconds { set; get; }

            public void Init(int times)
            {
                hour = (int)(times / 3600);
                float last = (int)(times % 3600);
                min = (int)(last / 60);
                seconds = (int)(last % 60);
            }
        }

        #region 私有成员
        /// <summary>
        /// 暂停标识位
        /// </summary>
        private bool m_Pause = true;
        private Popup m_operatePanel = null;
        /// <summary>
        /// 重播标识位
        /// </summary>
        private bool m_reset = false;
        private bool m_LoadIni = false;
        /// <summary>
        /// 允许获取播放进度标识位
        /// </summary>
        private bool m_allowRead = true;
        /// <summary>
        /// 获取进度的线程
        /// </summary>
        private System.Threading.Thread th = null;
        /// <summary>
        /// 中断任务标志
        /// </summary>
        private bool m_KillTask = true;
        private bool m_IsEnd = false;
        /// <summary>
        /// 任务执行器
        /// </summary>
        private void TaskStart()
        {
            th = new System.Threading.Thread(() =>
            {
                DateTime dt = DateTime.Now;
                while (!m_KillTask)
                {
                    if (!m_IsRealView)
                    {
                        System.Threading.Thread.Sleep(40);
                        if (!m_MouseDown && m_allowRead && !m_IsEnd)
                        {
                            VedioManagement.Default.RefreshProgress();
                        }
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(1000);
                        if (m_LoadIni)
                        {
                            this.Invoke(new MethodInvoker(() =>
                            {
                                float times = (float)(DateTime.Now - dt).TotalSeconds;
                                int hour = (int)(times / 3600);
                                float last = (int)(times % 3600);
                                int mint = (int)(last / 60);
                                int sec = (int)(last % 60);
                                label1.Text = string.Format("视频监测计时：{0}:{1}:{2}", hour.ToString().PadLeft(2, '0'), mint.ToString().PadLeft(2, '0'), sec.ToString().PadLeft(2, '0'));
                            }));
                        }
                    }
                }
            });
            th.IsBackground = true;
            th.SetApartmentState(System.Threading.ApartmentState.MTA);
            th.Start();
        }
        private void ControlPlayer_Move(object sender, EventArgs e)
        {
            if (m_operatePanel.IsOpen)
                m_operatePanel.ReLocation(pictrueBoxEx2);
        }
        /// <summary>
        /// 重置各按钮、标识位状态
        /// </summary>
        private void RePlayButt()
        {
            if (m_LoadIni)
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
                        RePlayButt();
                    }));
                }
                else
                {
                    RecPlayPauseBut.MouseUpImage = Properties.Resources.pause;
                    RecPlayPauseBut.MouseDownImage = RecPlayPauseBut.MouseOnImage = Properties.Resources.pause_2;
                    this.toolTip1.SetToolTip(this.RecPlayPauseBut, "暂停");
                    RecPlayFastBut.Enabled = RecPlayFrontBut.Enabled = RecPlayNextBut.Enabled = RecPlaySlowBut.Enabled = true;
                    m_Pause = false;
                    m_reset = false;
                }
            }
        }
        /// <summary>
        /// 停止状态
        /// </summary>
        public void StopPlayButt()
        {
            if (m_LoadIni)
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
                        StopPlayButt();
                    }));
                }
                else
                {
                    openVol = false;
                    m_Pause = true;
                    m_reset = true;
                    RecPlayPauseBut.MouseUpImage = Properties.Resources.play;
                    RecPlayPauseBut.MouseDownImage = RecPlayPauseBut.MouseOnImage = Properties.Resources.play_2;
                    this.toolTip1.SetToolTip(this.RecPlayPauseBut, "播放");
                    RecPlayFastBut.Enabled = RecPlayFrontBut.Enabled = RecPlayNextBut.Enabled = RecPlaySlowBut.Enabled = false;
                }
            }
        }
        /// <summary>
        /// 获取到播放进度
        /// </summary>
        /// <param name="currentTimes"></param>
        /// <param name="TotalsTime"></param>
        private void Default_ReviewRunningHandler(int currentTimes, uint TotalsTime)
        {
            if (currentTimes > 0)
            {
                TimeSpan span = new TimeSpan(0, 0, 0, 0, currentTimes);
                int _currenttimes = (int)span.TotalMilliseconds + m_offsetMills;
                if (_currenttimes >= m_TotalTimes)
                {
                    m_IsEnd = true;
                    if (this.IsDisposed)
                        return;
                    StopPlayButt();
                }
                if (m_CurrentTime != _currenttimes)
                {
                    m_CurrentTime = _currenttimes;
                    if (m_LoadIni)
                    {
                        if (this.IsDisposed)
                            return;
                        this.Invoke(new MethodInvoker(() =>
                        {
                            panel3.Invalidate();
                        }));
                    }
                    if (TimeLineChangeHandler != null)
                        TimeLineChangeHandler.Invoke(m_CurrentTime);

                }
            }
        }
        #endregion

        #region 构造函数/OnLoad/Resize
        public ControlPlayer()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer
                 | ControlStyles.ResizeRedraw
                 | ControlStyles.Selectable
                 | ControlStyles.AllPaintingInWmPaint
                 | ControlStyles.UserPaint
                 | ControlStyles.SupportsTransparentBackColor
                 | ControlStyles.Opaque,
            true);
            RecPlayFastBut.Enabled = RecPlayFrontBut.Enabled = RecPlayNextBut.Enabled = RecPlaySlowBut.Enabled = false;
            this.Load += ControlPlayer_Load;
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
        /// 控件加载时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlPlayer_Load(object sender, EventArgs e)
        {
            if (m_IsRealView)
            {
                label1.Text = "视频监测计时：00:00:00";
                RecPlayPauseBut.Visible = false;
                wTrackBar1.Visible = RecPlaySoundBut.Visible = false;
            }
            else
            {
                pictrueBoxEx1.Visible = pictrueBoxEx2.Visible = false;
                RecPlayFastBut.Visible = RecPlayFrontBut.Visible = RecPlayNextBut.Visible = RecPlaySlowBut.Visible = true;
                int offset = Width - wTrackBar1.Location.X - wTrackBar1.Width;
                wTrackBar1.Location = new Point(wTrackBar1.Location.X + offset, wTrackBar1.Location.Y);
                RecPlaySoundBut.Location = new Point(RecPlaySoundBut.Location.X + offset, RecPlaySoundBut.Location.Y);
                wTrackBar1.ValueChanged += WTrackBar1_ValueChanged;
            }
            m_operatePanel = new Popup(new UserOprerationPanel());
            m_operatePanel.DefaultDropDownDirection = ToolStripDropDownDirection.BelowRight;
            this.Move += ControlPlayer_Move;
            this.Resize += ControlPlayer_Resize;
            panel3.Paint += panel3_Paint;
            panel3.MouseEnter += panel3_MouseEnter;
            panel3.MouseLeave += panel3_MouseLeave;
            panel3.MouseDown += panel3_MouseDown;
            panel3.MouseUp += panel3_MouseUp;
            panel3.MouseMove += panel3_MouseMove;
            VedioManagement.Default.ReviewRunningHandler += Default_ReviewRunningHandler;
            if (m_KillTask)
            {
                m_KillTask = false;
                TaskStart();
            }
            m_AddValue = (float)(panel3.Width * 1.0 / m_TotalTimes);
            m_LoadIni = true;
        }

        private void ControlPlayer_Resize(object sender, EventArgs e)
        {
            m_AddValue = (float)(panel3.Width * 1.0 / m_TotalTimes);
        }
        #endregion

        #region 键盘事件
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (ControlPlayer_KeyDown(null, new KeyEventArgs(keyData)))
                return true;
            return base.ProcessDialogKey(keyData);
        }
        /// <summary>
        /// 键盘触发实现
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool ControlPlayer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                pictrueBoxEx1_Click(null, null);
                return true;
            }
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                LeftRightKeyControl(e.KeyCode == Keys.Left ? 1 : 2);
                return true;
            }
            return false;
        }
        #endregion

        #region 按钮事件
        /// <summary>
        /// 音量变化时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WTrackBar1_ValueChanged(object sender, EventArgs e)
        {
            ushort volume = (ushort)((0xffff / wTrackBar1.Maximum) * wTrackBar1.Value);
            VedioManagement.Default.ReviewSetVolume(volume);
        }
        /// <summary>
        /// 弹出云台方向操控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            if (!m_operatePanel.IsOpen)
                m_operatePanel.Show(sender as Control);
            else
                m_operatePanel.Close();
        }
        bool openVol = false;
        /// <summary>
        /// 对讲 开/关
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            if (openVol)
            {
                openVol = false;
                pictrueBoxEx1.MouseDownImage = Properties.Resources.sound1;
                pictrueBoxEx1.MouseUpImage = Properties.Resources.sound2;
                pictrueBoxEx1.MouseOnImage = Properties.Resources.sound1;
            }
            else
            {
                openVol = true;
                pictrueBoxEx1.MouseDownImage = Properties.Resources.sound3;
                pictrueBoxEx1.MouseUpImage = Properties.Resources.sound4;
                pictrueBoxEx1.MouseOnImage = Properties.Resources.sound3;
            }
            VedioManagement.Default.VioceTalk();
        }
        /// <summary>
        /// 播放/暂停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictrueBoxEx1_Click(object sender, EventArgs e)
        {
            if (m_reset)
            {
                if (m_CurrentTime >= m_TotalTimes)
                    m_CurrentTime = 0;
                ///重新播放
                if (ViewPlayHandle != null)
                    ViewPlayHandle.Invoke(m_CurrentTime);
                RePlayButt();
                if (PlayStatusChanged != null)
                    PlayStatusChanged.Invoke(!m_Pause);

                this.Invoke(new MethodInvoker(() =>
                {
                    panel3.Invalidate();
                }));
                return;
            }
            if (!m_Pause)
            {
                ///执行暂停
                if (VedioManagement.Default.ReviewPause(true).IsOK)
                {
                    RecPlayPauseBut.MouseUpImage = Properties.Resources.play;
                    RecPlayPauseBut.MouseDownImage = RecPlayPauseBut.MouseOnImage = Properties.Resources.play_2;
                    this.toolTip1.SetToolTip(this.RecPlayPauseBut, "播放");
                    m_Pause = true;
                    if (PlayStatusChanged != null)
                        PlayStatusChanged.Invoke(!m_Pause);
                }
                else
                {
                    RecPlayPauseBut.MouseUpImage = Properties.Resources.pause;
                    RecPlayPauseBut.MouseDownImage = RecPlayPauseBut.MouseOnImage = Properties.Resources.pause_2;
                    this.toolTip1.SetToolTip(this.RecPlayPauseBut, "暂停");
                    m_Pause = false;
                }
            }
            else
            {
                ///取消暂停
                if (VedioManagement.Default.ReviewPause(false).IsOK)
                {
                    RecPlayPauseBut.MouseUpImage = Properties.Resources.pause;
                    RecPlayPauseBut.MouseDownImage = RecPlayPauseBut.MouseOnImage = Properties.Resources.pause_2;
                    this.toolTip1.SetToolTip(this.RecPlayPauseBut, "暂停");
                    m_Pause = false;
                    if (PlayStatusChanged != null)
                        PlayStatusChanged.Invoke(!m_Pause);
                }
                else
                {
                    RecPlayPauseBut.MouseUpImage = Properties.Resources.play;
                    RecPlayPauseBut.MouseDownImage = RecPlayPauseBut.MouseOnImage = Properties.Resources.play_2;
                    this.toolTip1.SetToolTip(this.RecPlayPauseBut, "播放");
                }
            }
        }
        /// <summary>
        /// 快/慢进播放 最大倍率±16倍
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RecPlaySlowBut_Click(object sender, EventArgs e)
        {
            PictrueBoxEx pic = (PictrueBoxEx)sender;
            if (VedioManagement.Default.ReviewPlayFast(pic.Name != "RecPlaySlowBut").IsOK)
            {
            }
        }
        private bool m_playSoundEnable = true;
        /// <summary>
        /// 禁音 开/关
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictrueBoxEx7_Click(object sender, EventArgs e)
        {
            if (m_playSoundEnable)
            {
                m_playSoundEnable = false;
                VedioManagement.Default.ReviewPlaySound(false);
                RecPlaySoundBut.MouseUpImage = Properties.Resources.soundClose;
                RecPlaySoundBut.MouseOnImage = RecPlaySoundBut.MouseDownImage = Properties.Resources.soundClose_2;
                this.toolTip1.SetToolTip(this.RecPlaySoundBut, "禁音：开");
            }
            else
            {
                m_playSoundEnable = true;
                VedioManagement.Default.ReviewPlaySound(true);
                RecPlaySoundBut.MouseUpImage = Properties.Resources.sound_3;
                RecPlaySoundBut.MouseOnImage = RecPlaySoundBut.MouseDownImage = Properties.Resources.sound_4;
                this.toolTip1.SetToolTip(this.RecPlaySoundBut, "禁音：关");
            }
        }
        /// <summary>
        /// 前/后一帧播放（视频进入暂停）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RecPlayNextOrFrontBut_Click(object sender, EventArgs e)
        {
            PictrueBoxEx pic = (PictrueBoxEx)sender;
            if (VedioManagement.Default.ReviewPlayOneByOne(pic.Name == "RecPlayFrontBut" ? false : true).IsOK)
                if (!m_Pause)
                {
                    RecPlayPauseBut.MouseUpImage = Properties.Resources.play;
                    RecPlayPauseBut.MouseDownImage = RecPlayPauseBut.MouseOnImage = Properties.Resources.play_2;
                    this.toolTip1.SetToolTip(this.RecPlayPauseBut, "播放");
                    m_Pause = true;
                    if (PlayStatusChanged != null)
                        PlayStatusChanged.Invoke(!m_Pause);
                }
        }

        #endregion

        #region 播放进度效果
        /// <summary>
        /// 当前播放时间点
        /// </summary>
        private int m_CurrentTime = 0;
        private string str_TotalTimes = "";
        private int m_TotalTimes = 0;
        private float m_AddValue = 0;
        private bool m_MouseDown = false;
        private bool m_moveon = false;
        private bool m_refresh = false;
        private RectangleF m_HeadRect = RectangleF.Empty;

        void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_MouseDown && e.Location.X >= 0 && e.Location.X <= panel3.Width)
            {
                if (e.Location.X == panel3.Width)
                {
                    m_CurrentTime = (int)(e.Location.X / m_AddValue);
                }
                m_CurrentTime = (int)(e.Location.X / m_AddValue);
                panel3.Invalidate();
            }
        }

        void panel3_MouseUp(object sender, MouseEventArgs e)
        {
            if (m_MouseDown)
            {
                if (ProcessValueChanged != null)
                {
                    if (m_CurrentTime >= m_TotalTimes)
                    {
                        m_CurrentTime = m_TotalTimes - 1000;
                    }
                    if (m_reset)
                    {
                        pictrueBoxEx1_Click(null, null);
                    }
                    else
                        ProcessValueChanged.BeginInvoke(m_CurrentTime, m_Pause, null, null);
                }
                m_MouseDown = false;
            }
        }

        void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_MouseDown = true;
                RectangleF rang = new RectangleF(m_HeadRect.X - 3, m_HeadRect.Y, m_HeadRect.Width + 6, m_HeadRect.Height);
                if (!rang.Contains(e.Location))
                {
                    if (e.Location.X == panel3.Width)
                    {
                        m_CurrentTime = (int)(e.Location.X / m_AddValue);
                    }
                    m_CurrentTime = (int)(e.Location.X / m_AddValue);
                    panel3.Invalidate();
                }
                Console.WriteLine("按下");
            }
        }

        void panel3_MouseLeave(object sender, EventArgs e)
        {
            m_moveon = false;
            panel3.Invalidate();
        }

        private void panel3_MouseEnter(object sender, EventArgs e)
        {
            m_moveon = true;
            panel3.Invalidate();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            if (!m_refresh)
            {
                m_refresh = true;
                if (Width != 0 && Height != 0 && m_TotalTimes != 0)
                {
                    using (Graphics g = e.Graphics)
                    {
                        float ProcessRectHeight = panel3.Height - 4;
                        float y = 2;
                        RectangleF backProcessRect = new RectangleF(0, y, panel3.Width, ProcessRectHeight);
                        using (Brush b = new LinearGradientBrush(backProcessRect, Color.WhiteSmoke, Color.LightGray, LinearGradientMode.Vertical))
                        {
                            g.FillRectangle(b, backProcessRect);
                        }
                        float ProcessRectWidth = m_CurrentTime < m_TotalTimes ? m_AddValue * m_CurrentTime : panel3.Width;
                        if (ProcessRectWidth > 0)
                        {
                            RectangleF processRect = new RectangleF(0, y, ProcessRectWidth, ProcessRectHeight);
                            using (Brush b = new LinearGradientBrush(processRect, Color.Yellow, Color.LightGray, LinearGradientMode.Vertical))
                            {
                                g.FillRectangle(b, processRect);
                            }
                        }
                        float s = ProcessRectWidth - panel3.Height;
                        m_HeadRect = new RectangleF(s > 0 ? (s >= Width ? Width - panel3.Height : s) : 0, 0, panel3.Height, panel3.Height);
                        if (m_moveon)
                        {
                            using (Brush b = new LinearGradientBrush(m_HeadRect, Color.Red, Color.Salmon, LinearGradientMode.Vertical))
                            {
                                g.FillRectangle(b, m_HeadRect);
                            }
                        }
                    }
                    TimeSpan span = new TimeSpan(0, 0, 0, 0, m_CurrentTime);
                    if (m_TotalTimeUion != null)
                        label1.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss fff}\r\n{1}", m_baseTime.AddMilliseconds(m_CurrentTime),
                             string.Format("总时长：{0}:{1}:{2}", m_TotalTimeUion.hour.ToString().PadLeft(2, '0'), m_TotalTimeUion.min.ToString().PadLeft(2, '0'), m_TotalTimeUion.seconds.ToString().PadLeft(2, '0'))); //string.Format("{0:hh\\:mm\\:ss} / {1}", span, str_TotalTimes);
                }
                m_refresh = false;
            }
        }
        #endregion

        #region 公共成员
        private bool m_tTime_ini = false;
        private TimeUion m_TotalTimeUion = null;
        /// <summary>
        /// 总播放时长
        /// </summary>
        public int TotalTimes
        {
            set
            {
                if (!m_tTime_ini && value > 0)
                {
                    TimeSpan span = new TimeSpan(0, 0, 0, 0, value);
                    str_TotalTimes = span.ToString();
                    m_TotalTimes = (int)span.TotalMilliseconds;
                    m_TotalTimeUion = new TimeUion();
                    m_TotalTimeUion.Init((int)span.TotalSeconds);
                    m_tTime_ini = true;
                    RePlayButt();
                }
            }
            get
            {
                return m_TotalTimes;
            }
        }

        private DateTime m_baseTime = default(DateTime);
        /// <summary>
        /// 设置当前时间基数
        /// </summary>
        public DateTime StartViewTime
        {
            set
            {
                m_baseTime = value;
            }
            internal get
            {
                return m_baseTime;
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
            }
            get
            {
                return m_IsRealView;
            }
        }
        /// <summary>
        /// 方向控制前后帧播放 typ-1 left  typ-2 right
        /// </summary>
        /// <param name="typ"></param>
        public void LeftRightKeyControl(int typ)
        {
            RecPlayNextOrFrontBut_Click(typ == 1 ? RecPlayFrontBut : RecPlayNextBut, null);
        }
        /// <summary>
        /// 主动更新时间
        /// </summary>
        /// <param name="milSeconds"></param>
        public void UpdateTimeLine(int milSeconds)
        {
            if (m_MouseDown)
                return;
            m_CurrentTime = milSeconds;
            this.Invoke(new MethodInvoker(() =>
            {
                panel3.Invalidate();
            }));
        }
        /// <summary>
        /// 指定播放到当前进度
        /// </summary>
        /// <param name="millsSeconds"></param>
        public void SetCurrentTime(int millsSeconds)
        {
            if (millsSeconds >= m_TotalTimes)
            {
                StopPlayButt();
                return;
            }
            m_allowRead = false;
            VedioManagement.Default.setReViewCurrentTimes(millsSeconds - m_offsetMills);
            m_allowRead = true;
        }
        /// <summary>
        /// 开始回放
        /// </summary>
        /// <param name="RecordViewPath"></param>
        /// <param name="winHand"></param>
        /// <param name="millsSeconds"></param>
        /// <returns></returns>
        public bool StartRecordView(string RecordViewPath, IntPtr winHand, int millsSeconds = 0)
        {
            DateTime dt = DateTime.Now;
            m_CurrentTime = 0;
            m_allowRead = false;
            bool ret = VedioManagement.Default.StartReviewRecord(RecordViewPath, winHand, millsSeconds - m_offsetMills).IsOK;
            if (ret && m_Pause)
            {
                RePlayButt();
            }
            m_IsEnd = false;
            wTrackBar1.Value = 50;
            m_allowRead = true;
            Trace.WriteLine(string.Format("播放视频耗时：{0}ms", (DateTime.Now - dt).TotalMilliseconds));
            return ret;
        }
        private int m_offsetMills = 0;
        /// <summary>
        /// 设置当前的偏移量
        /// </summary>
        public int OffSetMillsSeconds
        {
            set
            {
                m_offsetMills = value;
            }
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void ClearAll()
        {
            try
            {
                m_KillTask = true;
                if (!m_IsRealView)
                {
                    this.Invoke(new MethodInvoker(() =>
                    {
                        label1.Text = "00:00:00 / 00:00:00";
                        m_CurrentTime = 0;
                        panel3.Invalidate();
                    }));
                }
                th.Abort();
            }
            catch
            {
                m_KillTask = true;
            }
        }
        #region 事件定义
        public delegate void ChangedProcessValueDelegate(int currentTime, bool Pause);
        /// <summary>
        /// 进度条被拖动时发生
        /// </summary>
        public event ChangedProcessValueDelegate ProcessValueChanged;

        /// <summary>
        /// 播放状态发生变化时 触发 true-播放状态 false-不播放状态
        /// </summary>
        public event Action<bool> PlayStatusChanged;

        /// <summary>
        /// 视频播放时发生
        /// </summary>
        public event Action<int> ViewPlayHandle;
        /// <summary>
        /// 播放进度发生变化时触发
        /// </summary>
        public event Action<int> TimeLineChangeHandler;
        #endregion
        #endregion

    }
}
