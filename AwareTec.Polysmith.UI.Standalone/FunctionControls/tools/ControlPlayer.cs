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
using System.Drawing.Drawing2D;
using System.Windows.Forms.Ctrl;
using System.IO;
using System.Runtime.InteropServices;

namespace AwareTec.Polysmith.UI.FunctionControls.tools
{
    public partial class ControlPlayer : UserControl
    {
        private Popup m_operatePanel = null;
        private bool m_reset = false;
        private string m_RecordViewPath = AppDomain.CurrentDomain.BaseDirectory + "Media\\2021-07-21.mp4";
        private string m_SaveNewViewPath = AppDomain.CurrentDomain.BaseDirectory + "Media\\2020-09-01\\NewRecord22040747.mp4";
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
            RecPlayFastBut.Enabled = RecPlayFrontBut.Enabled = RecPlayNextBut.Enabled = RecPlaySlowBut.Enabled = RecPlayStopBut.Enabled = RecPlayToEndBut.Enabled = RecPlayToStartBut.Enabled = false;
            wTrackBar1.Value = 50;
            this.Load += ControlPlayer_Load;
        }

        private void ControlPlayer_Load(object sender, EventArgs e)
        {
            if (m_IsRealView)
            {
                label1.Visible = false;
                RecPlayPauseBut.Visible = false;
                wTrackBar1.Visible = RecPlaySoundBut.Visible = false;
            }
            else
            {
                button6.Visible = button7.Visible = false;
                int offset = Width - wTrackBar1.Location.X - wTrackBar1.Width;
                wTrackBar1.Location = new Point(wTrackBar1.Location.X + offset,wTrackBar1.Location.Y);
                RecPlaySoundBut.Location = new Point(RecPlaySoundBut.Location.X + offset, RecPlaySoundBut.Location.Y);
            }
            m_operatePanel = new Popup(new FunctionControls.tools.UserOprerationPanel());
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
        }
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
                    System.Threading.Thread.Sleep(40);
                    if (!m_MouseDown && m_allowRead)
                    {
                        VedioManagement.Default.RefreshProgress();
                    }
                    Console.WriteLine(m_MouseDown);
                }
            });
            th.IsBackground = true;
            th.SetApartmentState(System.Threading.ApartmentState.MTA);
            th.Start();
        }
        private void ControlPlayer_Move(object sender, EventArgs e)
        {
            if (m_operatePanel.IsOpen)
                m_operatePanel.ReLocation(button7);
        }

        private void ControlPlayer_Resize(object sender, EventArgs e)
        {
            m_AddValue = (float)(panel3.Width * 1.0 / m_TotalTimes);
        }

        private void Default_ReviewRunningHandler(int currentTimes, uint TotalsTime)
        {
            TotalTimes = Convert.ToInt32(TotalsTime);
            TimeSpan span = new TimeSpan(0, 0, 0, 0, currentTimes);
            if (m_CurrentTime != span.TotalMilliseconds)
            {
                m_CurrentTime = (int)span.TotalMilliseconds;
                if (this.IsDisposed)
                    return;
                try
                {
                    this.Invoke(new MethodInvoker(() =>
                    {
                        panel3.Invalidate();
                        if (m_CurrentTime == m_TotalTimes)
                        {
                            m_reset = true;
                            openVol = false;
                            m_Pause = true;
                            RecPlayPauseBut.MouseUpImage = Properties.Resources.play;
                            RecPlayPauseBut.MouseDownImage = RecPlayPauseBut.MouseOnImage = Properties.Resources.play_2;
                            this.toolTip1.SetToolTip(this.RecPlayPauseBut, "播放");
                            RecPlayFastBut.Enabled = RecPlayFrontBut.Enabled = RecPlayNextBut.Enabled = RecPlaySlowBut.Enabled = RecPlayStopBut.Enabled = RecPlayToEndBut.Enabled = RecPlayToStartBut.Enabled = false;
                        }
                    }));
                }
                catch { }
                if (TimeLineChangeHandler != null)
                    TimeLineChangeHandler.Invoke(m_CurrentTime);
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
        #region 按钮事件
        private void ViewPlay(int playTyp)
        {
            if (ViewPlayHandle != null)
                ViewPlayHandle.Invoke(playTyp);
        }
        private bool m_Pause = true;
        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!m_operatePanel.IsOpen)
                m_operatePanel.Show(button7);
            else
                m_operatePanel.Close();
        }
        bool openVol = false;
        private void button6_Click(object sender, EventArgs e)
        {
            if (openVol)
            {
                openVol = false;
                button6.BackgroundImage = Properties.Resources.volStop;
            }
            else
            {
                openVol = true;
                button6.BackgroundImage = Properties.Resources.volStart;
            }
            VedioManagement.Default.VioceTalk();
        }

        private void btnLeft_MouseDown(object sender, MouseEventArgs e)
        {
            VedioManagement.Default.DeviceOperate(Convert.ToInt16((sender as Button).Tag));
        }

        private void btnLeft_MouseUp(object sender, MouseEventArgs e)
        {
            VedioManagement.Default.DeviceOperate(Convert.ToInt16((sender as Button).Tag), false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VedioManagement.Default.setReViewCurrentTimes(--m_CurrentTime);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            VedioManagement.Default.setReViewCurrentTimes(++m_CurrentTime);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            FileInfo sr = new FileInfo(m_RecordViewPath);///m_RecordViewPath：待分析的视频文件路径
            int buffLength = 32 * 1024;
            byte[] pReadBuf = new byte[buffLength];
            using (BinaryWriter bw = new BinaryWriter(new FileStream(m_SaveNewViewPath, FileMode.Append)))///m_SaveNewViewPath；分析后数据保存路径
            {
                using (FileStream fsRead = new FileStream(m_RecordViewPath, FileMode.Open))
                {
                    byte[] head = new byte[40];
                    fsRead.Read(head, 0, 40);
                    IntPtr m_hNewHandle = Vedio.VedioStreamSDK.HIKANA_CreateStreamEx(1024 * 1024, head);///创建分析句柄
                    int offset = 0;
                    while (true)
                    {
                        int readCount = fsRead.Read(pReadBuf, 0, pReadBuf.Length);///读取原加密视频数据
                        if (readCount == 0)
                        {
                            byte[] pReadRemainBuf = new byte[pReadBuf.Length];
                            uint nRemain = 0;
                            Vedio.VedioStreamSDK.HIKANA_GetRemainData(m_hNewHandle, pReadRemainBuf, ref nRemain);
                            break;
                        }
                        else
                        {
                            offset += readCount;
                            fsRead.Seek(offset, SeekOrigin.Begin);
                        }
                        Vedio.VedioStreamSDK.HIKANA_InputData(m_hNewHandle, pReadBuf, (uint)readCount);///原数据推送到分析模块
                        Vedio.VedioStreamSDK.HIKANA_SetOutputPacketType(m_hNewHandle, 0);// 设置输出帧为裸数据(1:不带封装；0:为默认,带封装)
                        int rettt = 0;
                        Vedio.VedioStreamSDK.PACKET_INFO_EX stPacket = new VedioStreamSDK.PACKET_INFO_EX();///创建帧信息体用于接收解析的数据
                        while (rettt == 0)
                        {
                            rettt = Vedio.VedioStreamSDK.HIKANA_GetOnePacketEx(m_hNewHandle, ref stPacket);///获取解析数据
                            if (rettt == 0)
                            {
                                byte[] data = new byte[stPacket.dwPacketSize];
                                Marshal.Copy(stPacket.pPacketBuffer, data, 0, data.Length);
                                bw.Write(data, 0, (int)stPacket.dwPacketSize);///存储不加密的解析数据
                                string str = (string.Format("当前帧序号：{0}*****当前时间：{1}-{2}-{3} {4}:{5}:{6} {7}", stPacket.dwFrameNum, stPacket.nYear,
                                               stPacket.nMonth, stPacket.nDay, stPacket.nHour, stPacket.nMinute, stPacket.nSecond, stPacket.nMillisecond));
                                Console.WriteLine(str);
                            }
                        }
                    }
                    bw.Flush();
                    bw.Close();
                    Vedio.VedioStreamSDK.HIKANA_ClearBuffer(m_hNewHandle);
                    Vedio.VedioStreamSDK.HIKANA_Destroy(m_hNewHandle);
                }
            }
        }

        private void pictrueBoxEx1_Click(object sender, EventArgs e)
        {
            if (m_reset)
            {
                ViewPlay(1);
                m_reset = false;
                m_Pause = false;
                return;
            }
            if (!m_Pause)
            {
                if (VedioManagement.Default.ReviewPause(true).IsOK)
                {
                    RecPlayPauseBut.MouseUpImage = Properties.Resources.play;
                    RecPlayPauseBut.MouseDownImage = RecPlayPauseBut.MouseOnImage = Properties.Resources.play_2;
                    this.toolTip1.SetToolTip(this.RecPlayPauseBut, "播放");
                    m_Pause = true;
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
                if (VedioManagement.Default.ReviewPause(false).IsOK)
                {
                    RecPlayPauseBut.MouseUpImage = Properties.Resources.pause;
                    RecPlayPauseBut.MouseDownImage = RecPlayPauseBut.MouseOnImage = Properties.Resources.pause_2;
                    this.toolTip1.SetToolTip(this.RecPlayPauseBut, "暂停");
                    m_Pause = false;
                }
                else
                {
                    RecPlayPauseBut.MouseUpImage = Properties.Resources.play;
                    RecPlayPauseBut.MouseDownImage = RecPlayPauseBut.MouseOnImage = Properties.Resources.play_2;
                    this.toolTip1.SetToolTip(this.RecPlayPauseBut, "播放");
                }
            }
        }

        private void pictrueBoxEx3_Click(object sender, EventArgs e)
        {
            if (m_CurrentTime != m_TotalTimes)
            {
                m_CurrentTime = m_TotalTimes;
                VedioManagement.Default.setReViewCurrentTimes(m_CurrentTime);
            }
        }

        private void pictrueBoxEx5_Click(object sender, EventArgs e)
        {
            if (m_CurrentTime != 0)
            {
                m_CurrentTime = 0;
                VedioManagement.Default.setReViewCurrentTimes(m_CurrentTime);
            }
        }

        private void pictrueBoxEx4_Click(object sender, EventArgs e)
        {
            if (VedioManagement.Default.ReviewPlayFast(true).IsOK)
            {
                ///加速
            }
        }

        private void pictrueBoxEx2_Click(object sender, EventArgs e)
        {
            if (VedioManagement.Default.ReviewStop().IsOK)
            {
                RecPlayPauseBut.MouseUpImage = Properties.Resources.play;
                RecPlayPauseBut.MouseDownImage = RecPlayPauseBut.MouseOnImage = Properties.Resources.play_2;
                this.toolTip1.SetToolTip(this.RecPlayPauseBut, "播放");
                RecPlayFastBut.Enabled = RecPlayFrontBut.Enabled = RecPlayNextBut.Enabled = RecPlaySlowBut.Enabled = RecPlayStopBut.Enabled = RecPlayToEndBut.Enabled = RecPlayToStartBut.Enabled = false;
                AhDung.MessageTip.ShowOk("成功");
            }
        }
        private bool m_playSoundEnable = true;
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
                VedioManagement.Default.ReviewPlaySound(false);
                RecPlaySoundBut.MouseUpImage = Properties.Resources.sound_3;
                RecPlaySoundBut.MouseOnImage = RecPlaySoundBut.MouseDownImage = Properties.Resources.sound_4;
                this.toolTip1.SetToolTip(this.RecPlaySoundBut, "禁音：关");
            }
        }

        private void RecPlayFrontBut_Click(object sender, EventArgs e)
        {

        }

        private void RecPlaySlowBut_Click(object sender, EventArgs e)
        {
            if (VedioManagement.Default.ReviewPlayFast(false).IsOK)
            {
                ///减速
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
                if (!m_Pause)
                    VedioManagement.Default.ReviewPause(false);
                VedioManagement.Default.setReViewCurrentTimes(m_CurrentTime);
                System.Threading.Thread.Sleep(200);
                m_MouseDown = false;
            }
        }

        void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_MouseDown = true;
                RectangleF rang = new RectangleF(m_HeadRect.X - 3, m_HeadRect.Y, m_HeadRect.Width + 6, m_HeadRect.Height);
                if (!m_Pause)
                    VedioManagement.Default.ReviewPause(true);
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
                    label1.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss fff}", m_baseTime.AddMilliseconds(m_CurrentTime)); //string.Format("{0:hh\\:mm\\:ss} / {1}", span, str_TotalTimes);
                }
                m_refresh = false;
            }
        }       
        #endregion
        #region
        private bool m_tTime_ini = false;
        /// <summary>
        /// 总播放时长
        /// </summary>
        public int TotalTimes
        {
            set
            {
                if (!m_tTime_ini && value>0)
                {
                    TimeSpan span = new TimeSpan(0, 0, 0, 0, value);
                    str_TotalTimes = span.ToString();
                    m_TotalTimes = (int)span.TotalMilliseconds;
                    m_tTime_ini = true;
                    m_AddValue = (float)(panel3.Width * 1.0 / m_TotalTimes);
                    RePlayButt();
                }
            }
        }
        public void RePlayButt()
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
                this.Invoke(new MethodInvoker(() =>
                {
                    RecPlayPauseBut.MouseUpImage = Properties.Resources.pause;
                    RecPlayPauseBut.MouseDownImage = RecPlayPauseBut.MouseOnImage = Properties.Resources.pause_2;
                    this.toolTip1.SetToolTip(this.RecPlayPauseBut, "暂停");
                    RecPlayFastBut.Enabled = RecPlayFrontBut.Enabled = RecPlayNextBut.Enabled = RecPlaySlowBut.Enabled = RecPlayStopBut.Enabled = RecPlayToEndBut.Enabled = RecPlayToStartBut.Enabled = true;
                    m_Pause = false;
                }));
            }
        }
        /// <summary>
        /// 视频播放时发生
        /// </summary>
        public event Action<int> ViewPlayHandle;
        /// <summary>
        /// 播放进度发生变化时触发
        /// </summary>
        public event Action<int> TimeLineChangeHandler;
        private DateTime m_baseTime=default(DateTime);
        /// <summary>
        /// 设置当前时间基数
        /// </summary>
        public DateTime StartViewTime
        {
            set
            {
                m_baseTime = value;
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
        private int setmillsSeconds = 0;
        private bool m_allowRead = true;
        public void SetCurrentTime(int millsSeconds)

        {
            setmillsSeconds = millsSeconds;
            m_allowRead = false;
            VedioManagement.Default.setReViewCurrentTimes(millsSeconds);
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
            m_CurrentTime = 0;
            bool ret = VedioManagement.Default.StartReviewRecord(RecordViewPath, winHand, millsSeconds).IsOK;
            return ret;
        }
        public void ClearAll()
        {
            try
            {
                if (!m_IsRealView)
                {
                    this.Invoke(new MethodInvoker(() =>
                    {
                        label1.Text = "00:00:00 / 00:00:00";
                        m_CurrentTime = 0;
                        panel3.Invalidate();
                    }));
                }
                m_KillTask = true;
                th.Abort();
            }
            catch
            {
                m_KillTask = true;
            }
        }
        #endregion
    }
}
