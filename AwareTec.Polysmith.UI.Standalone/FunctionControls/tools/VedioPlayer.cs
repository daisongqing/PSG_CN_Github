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

namespace AwareTec.Polysmith.UI.FunctionControls.tools
{
    public partial class VedioPlayer : UserControl
    {
        private bool m_init = false;
        private string m_savePath = AppDomain.CurrentDomain.BaseDirectory + "Media";
        private bool m_reset = false;
        private string m_RecordViewPath ="E:\\菲诗奥数据\\Media\\2021-07-20.mp4";
        private string m_SaveNewViewPath = AppDomain.CurrentDomain.BaseDirectory + "Media\\c775cf12-da03-4c45-95f4-4e5af96c89b4\\NewRecord22040747.mp4";
        private FunctionControls.tools.ControlPlayer player = null;
        public VedioPlayer()
        {
            InitializeComponent();
            this.Load += VedioPlayCtrl_Load;
            this.SizeChanged += VedioPlayCtrl_SizeChanged;
            this.Disposed+=VedioPlayer_Disposed;
            this.panel1.MouseWheel += panel1_MouseWheel;
            panel1.Dock = DockStyle.Fill;
            player = new FunctionControls.tools.ControlPlayer();
            player.Dock = DockStyle.Bottom;
            player.ViewPlayHandle += player_ViewPlayHandle;
            player.TimeLineChangeHandler += player_TimeLineChangeHandler;
            this.Controls.Add(player);
        }

        private void VedioPlayer_Disposed(object sender, EventArgs e)
        {

            m_init = false;
        }

        private void player_TimeLineChangeHandler(int offsetTime)
        {
            if (TimeLineChangeHandler != null)
            {
                TimeLineChangeHandler.Invoke(offsetTime);
            }
        }

        private void player_ViewPlayHandle(int controlTyp)
        {
            switch (controlTyp)
            {
                case 1:
                    StartRecordView(m_RecordViewPath);
                    break;
            }
        }

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
                string[] ss=Channel.Default.SystemSetting.VedioSourceUrl.Split(':');
                if (ss.Length == 2)
                {
                    string ip = ss[0];
                    try
                    {
                        int port = int.Parse(ss[1]);
                        VedioManagement.Default.Login(ip, port, "admin", "physio123.");
                        VedioManagement.Default.StartRealViewPlay(panel1.Handle);
                        VedioManagement.Default.StartRecord(VedioSavePath,true);
                    }
                    catch { }
                }
            }
            else
            {
                Task.Factory.StartNew(()=>
                {
                    StartRecordView(m_RecordViewPath, m_StartPlayTime);
                });
            }
            m_init = true;
            //if (m_KillTask)
            //{
            //    m_KillTask = false;
            //    TaskStart();
            //}
        }
        /// <summary>
        /// 获取进度的线程
        /// </summary>
        private System.Threading.Thread th = null;
        /// <summary>
        /// 中断任务标志
        /// </summary>
        private bool m_KillTask = true;
        private List<RunUion> m_ListVedioFrame = new List<RunUion>();
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
                    int len = 0;
                    RunUion one = null;
                    lock (m_lock)
                    {
                        len = m_ListVedioFrame.Count;
                        if (len > 0)
                            one = m_ListVedioFrame[len - 1];
                    }
                    try
                    {
                        if (one != null)
                        {
                            player.StartViewTime = one.Now;
                            StartRecordView(one.Path, one.OffsetTimes);
                            lock (m_lock)
                            {

                                m_ListVedioFrame.RemoveRange(0, len);

                            }
                        }
                    }
                    catch { }
                }
            });
            th.IsBackground = true;
            th.SetApartmentState(System.Threading.ApartmentState.MTA);
            th.Start();
        }

        private void StartRecordView(string RecordViewPath, int millsSeconds=0)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                if (player.StartRecordView(RecordViewPath, panel1.Handle, millsSeconds))
                {
                    m_viewWidth = panel1.Width;
                    m_viewHeight = panel1.Height;
                    VedioManagement.Default.getPictrueSize(ref m_viewWidth, ref m_viewHeight);
                    zoomOffSet_W = m_viewWidth / m_ZoomTotalCnt;
                    zoomOffSet_H = m_viewHeight / m_ZoomTotalCnt;
                    m_WidthRate = m_viewWidth * 1.0f / panel1.Width;
                    m_HeightRate = m_viewHeight * 1.0f / panel1.Height;
                    m_RecordViewOpen = true;
                }
            }));

        }
        private int m_StartPlayTime = 0;
        public int StartPlayTime
        {
            set
            {
                m_StartPlayTime = value;
            }
        }
        /// <summary>
        /// 设置当前时间基数
        /// </summary>
        public DateTime StartViewTime
        {
            set
            {
                player.StartViewTime = value;
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
                player.IsRealView = value;
            }
            get
            {
                return m_IsRealView;
            }
        }
        private string m_guid = "2020-09-03";
        /// <summary>
        /// 关键字
        /// </summary>
        public string GUID
        {
            set
            {
                m_guid = value;
                if (Channel.Default.SystemSetting.VedioSavePath != "")
                    m_savePath = Channel.Default.SystemSetting.VedioSavePath;
                VedioSavePath = string.Format("{0}\\{1}", m_savePath, m_guid);
                m_RecordViewPath =string.Format("{0}\\{1}.mp4", VedioSavePath,value);
            }
            get
            {
                return m_guid;
            }
        }
        /// <summary>
        /// 获取文件存储目录
        /// </summary>
        public string VedioSavePath
        {
            private set;
            get;
        }
        private bool m_ClearReady = false;
        /// <summary>
        /// 播放进度发生变化时触发
        /// </summary>
        public event Action<int> TimeLineChangeHandler;
        private object m_lock = new object();
        private class RunUion
        {
            public string Path = "";

            public DateTime Now=default(DateTime);

            public int OffsetTimes = 0;
        }
        /// <summary>
        /// 播放视频
        /// </summary>
        /// <param name="FrameNo"></param>
        public void SetViewPlayCase(string path, int FrameNo, DateTime startViewTime, int StartmillsSeconds=0)
        {
            m_RecordViewPath = Path.Combine(path, string.Format("{0}.mp4", FrameNo));
            if (m_init && File.Exists(m_RecordViewPath))
            {
                lock (m_lock)
                    m_ListVedioFrame.Add(new RunUion() { Now = startViewTime, Path = m_RecordViewPath, OffsetTimes = StartmillsSeconds });
            }
            else
            {
                CloseAll();
            }
        }
        public void SetCurrentTime(int millsSeconds)
        {
            player.SetCurrentTime(millsSeconds);
        }
        public void CloseAll()
        {
            lock (m_lock)
                m_ListVedioFrame.Clear();
            player.ClearAll();
            VedioManagement.Default.Dispose();
        }
    }
}
