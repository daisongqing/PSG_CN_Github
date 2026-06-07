using AwareTec.Polysmith.pChart;
using AwareTec.Polysmith.UI.FunctionControls.tools;
using AwareTec.Polysmith.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using AwareTec.Polysmith.DataCenter;
using AwareTec.Polysmith.UI.DataModel;
using AwareTec.Polysmith.DataBaseCom;
using AwareTec.Polysmith.UI.FunctionControls;
using AwareTec.Polysmith.UI.Block;
using AwareTec.Polysmith.UI.EnumModel;
using AwareTec.Polysmith.Util.EnumUtils;
using AwareTec.Polysmith.Util.ExcelUtils;
using NPOI.SS.UserModel;
using AwareTec.Polysmith.Util.PathUtils;
using System.Configuration;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Runtime.InteropServices;
using NPOI.POIFS.Crypt.Dsig;

namespace AwareTec.Polysmith.UI
{
    public partial class MainForm : SkinForm
    {
        #region 私有变量

        /// <summary>
        /// 算法通讯接口类
        /// </summary>
        private DataCenter.TargetCenter m_TargetInstance = null;
        private ICommunication m_comm = null;
        private Protocol.ProtocolServer realdataview_protocol;
        private bool addrealdataview = false;
        private bool reconnection = false;
        private bool hasvideo = false;
        private Dictionary<string, string> m_TargetInstance_NewFileCreating()
        {
            return Channel.Default.getReNameMap();
        }
        private string m_TargetInstance_FileHashCodeCreating(string filePath, byte[] data = null)
        {
            if (data == null)
            {
                Control block = getControl();
                if (block != null)
                {
                    if (block is FunctionControls.HistoryDataView)
                    {
                        return (block as FunctionControls.HistoryDataView).HashCode;
                    }
                }
                return "";
            }
            else
            {
                return DataModel.DataBaseHelper.Default.ComputeSHA256(data);
            }
        }
        private PdfViewDialog m_PdfViewDialog = null;
        private int m_CurrentFrameNo = 1;
        private string[] m_SleepState = new string[10];
        private object m_lockSleep = new object();
        private bool m_SleepStateChange = false;
        private RibbonButton bak_butt;

        #endregion

        #region 公有变量

        /// <summary>
        /// 电池剩余容量
        /// </summary>
        public static float BatteryCapacityValue = 100;
        /// <summary>
        /// 电池剩余可监测时间
        /// </summary>
        public static int BatteryLastTime = 0;

        #endregion

        #region 事件委托
        /// <summary>
        /// 中断任务
        /// </summary>
        private event Action<bool> m_InterruptHandle;
        /// <summary>
        /// 中断任务
        /// </summary>
        private event Action<bool> InterruptHandle
        {
            add
            {
                if (m_InterruptHandle != null)
                    m_InterruptHandle = null;
                m_InterruptHandle += value;
            }
            remove
            {
                if (m_InterruptHandle != null)
                    m_InterruptHandle = null;
            }
        }
        public delegate bool EditEdfByOnOffTimeDelegate(DateTime starttime, DateTime stoptime, bool byLightOnOff);
        public event EditEdfByOnOffTimeDelegate EditEdfByOnOffTimeEventHandler;
        #endregion

        #region 初始化 load/close/dispose

        public MainForm()
        {
            this.InitializeComponent();

           

            var systemSetting = Channel.Default.SystemSetting;
            var modeString = EnumHelper.GetDescription((ModeType)Channel.Default.SystemSetting.ModeType);
            this.Text = this.Text + modeString;
            this.BatteryCapacity.Paint += this.BatteryCapacity_Paint;
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            base.UpdateStyles();
            this.UserHandleContextMenuStrip.Size = new System.Drawing.Size(76, 80);
            Channel.Default.ChannelCreatingHandle += this.m_channel_Management_ChannelCreatingHandle;
            Channel.Default.ChangeMenuHandle += this.FreshHomeMenu;
            Channel.Default.NextStepHandle += this.patient_NextStepHandle;
            Channel.Default.RecoverStateChangedHandler += Default_RecoverStateChangedHandler;
            Channel.Default.StartHistroyAnalysisByEDFHandle += this.ihd_StartHistroyAnalysisByEDFHandle;
            DeviceOnLine.Default.OffLineHandler += this.Default_OffLineHandler;
            ResultDomain.Default.Start = true;
            MarkerManage.Default.Start = true;
            this.bak_butt = this.ribbonButton36;
            this.bak_butt.Checked = true;
            base.Load += this.MainForm_Load;
            this.ribbon1.Expanded = true;
            base.TopLevel = true;
            // this.m_TargetInstance = new TargetCenter(RemoteDataInteraction.Default);
            m_comm = new RemoteDataInteration();
            this.m_TargetInstance = new TargetCenter(m_comm);
            this.m_TargetInstance.FileHashCodeCreating += this.m_TargetInstance_FileHashCodeCreating;
            this.m_TargetInstance.NewFileCreating += this.m_TargetInstance_NewFileCreating;
            panel1.Paint += panel1_Paint;
           
            if (Channel.Default.SystemSetting.Reserve3.Length > 0)
                this.Text += " - " + Channel.Default.SystemSetting.Reserve3;

            //this.Text = "AwareSleep PSG analysis software for philips test 20241220";

           

            if (Program.Language == "EN"){
                this.Text = "AwareSleep PSG analysis software V1.01225 Beta";

                ribbonTab1.Text = ML.GetText("MAINFORM_SYSTEM", "系统");
                ribbonButton49.Text = ML.GetText("MAINFORM_HOME", "首页");
                ribbonButton1.Text = ML.GetText("MAINFORM_NEWCASE", "新病例");
                ribbonButton6.Text = ML.GetText("MAINFORM_START_ANALYZING", "开始分析");
                ribbonButton7.Text = ML.GetText("MAINFORM_SAVE_ANALYZING", "保存分析");
                ribbonButton12.Text = ML.GetText("MAINFORM_TREND_CHART", "多趋势图");
                ribbonButton32.Text = ML.GetText("MAINFORM_SLEEP_REPORT", "睡眠分析报告");

                ribbonLabel2.Text = ML.GetText("MAINFORM_TIME_BASE", "      时基：");
                ribbonLabel1.Text = ML.GetText("MAINFORM_SLEEP_STAGE", "睡眠分期：");
                ribbonLabel3.Text = ML.GetText("MAINFORM_CHANNEL_1", "导联方案:");
                SingleNotchRibbonButton.Text = ML.GetText("MAINFORM_NOTCH", "陷波器");
                HighPassRibbonButton.Text = ML.GetText("MAINFORM_HIGH_PASS", "高通");
                LowPassRibbonButton.Text = ML.GetText("MAINFORM_LOW_PASS", "低通");

                ribbonTab3.Text = ML.GetText("MAINFORM_CONFIG", "设置");
                ribbonButton5.Text = ML.GetText("MAINFORM_RATER", "评分员");
                ribbonButton10.Text = ML.GetText("MAINFORM_EVENT_DEF", "定义事件");
                ribbonButton11.Text = ML.GetText("MAINFORM_CHANNEL", "通道管理");
                ribbonButton13.Text = ML.GetText("MAINFORM_SETUP", "系统设置");

                ribbonTab5.Text = ML.GetText("MAINFORM_HELP", "帮助");
                ribbonButton30.Text = ML.GetText("MAINFORM_MANUAL", "操作手册");
                ribbonButton31.Text = ML.GetText("MAINFORM_ABOUT", "关于");


                LogoutToolStripMenuItem.Text = "Logout";
                ExitToolStripMenuItem.Text = "Close";

                AutoScore.ToolTip = "Automatic Score：OFF ";// 自动事件判断：关";
                EditEDF.Text = "Crop EDF file";
                EditEDF.ToolTip = "Crop EDF file";
                RecoverSaveFile.Text = "Restore to local backup files";
                RecoverSaveFile.ToolTip = "Restore to local backup files";
                SingleNotchRibbonButton.Text = "SingleNotch";
                HighPassRibbonButton.Text = "HighPass";
                LowPassRibbonButton.Text = "LowPass";

                LockButton.ToolTip = "UnLock";
                ribbonButton2.ToolTip = "View Video";


            }



            toolStripStatusLabel3.Text = Program.Language=="EN"?"Ready": "准备";

            //HighPassRibbonButton.Text = Program.Language == "EN" ? "HighPass" : "高通";


            //ML.LoadFormLanguage(this);

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string[] array = Channel.Default.Channel_FilterLoad("SingleNotch");
            this.SingleNotchRibbonButton.DropDownItems.Add(new RibbonButton("OFF"));
            for (int i = 0; i < array.Length; i++)
            {
                this.SingleNotchRibbonButton.DropDownItems.Add(new RibbonButton(array[i]));
            }
            this.SingleNotchRibbonButton.DropDownItems[0].Checked = true;
            array = Channel.Default.Channel_FilterLoad("HighPass");
            this.HighPassRibbonButton.DropDownItems.Add(new RibbonButton("OFF"));
            for (int j = 0; j < array.Length; j++)
            {
                this.HighPassRibbonButton.DropDownItems.Add(new RibbonButton(array[j]));
            }
            this.HighPassRibbonButton.DropDownItems[0].Checked = true;
            array = Channel.Default.Channel_FilterLoad("LowPass");
            this.LowPassRibbonButton.DropDownItems.Add(new RibbonButton("OFF"));
            for (int k = 0; k < array.Length; k++)
            {
                this.LowPassRibbonButton.DropDownItems.Add(new RibbonButton(array[k]));
            }
            this.LowPassRibbonButton.DropDownItems[0].Checked = true;
            this.LowPassRibbonButton.DropDownItemClicked += this.FliterRibbonButton_DropDownItemClicked;
            this.SingleNotchRibbonButton.DropDownItemClicked += this.FliterRibbonButton_DropDownItemClicked;
            this.HighPassRibbonButton.DropDownItemClicked += this.FliterRibbonButton_DropDownItemClicked;
            this.LoadHomePage();
            if (Channel.Default.SleepLock)
            {
                this.LockButton.SmallImage = Properties.Resources.Lockyes;
            }
            List<string> cfgNames = ChannelManage.Default.GetCfgNames();
            foreach (string text in cfgNames)
            {
                this.ribbonComboBox1.DropDownItems.Add(new RibbonLabel
                {
                    Text = text
                });
            }
            this.ribbonComboBox1.DropDownItemClicked += this.ribbonComboBox1_DropDownItemClicked;
            this.ribbonComboBox1.MouseLeave += this.ribbonComboBox1_MouseLeave;
            this.ribbonComboBox1.DropDownShowing += this.ribbonComboBox1_DropDownShowing;
            base.FormClosing += this.MainForm_FormClosing;
            base.FormClosed += this.MainForm_FormClosed;
            Channel.Default.ConfigNameChanged += this.Default_ConfigNameChanged;
            timer1.Interval = 500;
            m_comm.Init();
            UsernameLabel.Text = Channel.Default.Loginer.Name;
            //AdjustPosition();
        }

        /// <summary>
        /// 加载主页
        /// </summary>
        internal void LoadHomePage()
        {
            ribbonPanel2.Visible = ribbonPanel12.Visible = ribbonPanel13.Visible = ribbonPanel14.Visible = false;
            ribbonButton49.Visible = false;
            ribbonButton1.Visible = true;
            if (bak_butt != null)
                bak_butt.Checked = false;
            bak_butt = ribbonButton36;
            bak_butt.Checked = true;
            ribbon1.Refresh();
            FunctionControls.AnalysisRecordView arv = new FunctionControls.AnalysisRecordView();
            arv.LoadRecordHandle += arv_LoadRecordHandle;
            ribbonButton2.SmallImage = Properties.Resources.Videono;
            Channel.Default.VedioEnable = false;
            ribbonButton2.ToolTip = Program.Language == "EN" ? "View Video" : "查看视频";
            Channel.Default.IsAutoMark = false;
            AutoScore.SmallImage = Properties.Resources.AutoScore2;
            AutoScore.ToolTip = Program.Language == "EN" ? "Automatic Score: Off" : "自动事件判断：关";
            AddControls(arv);
        }

        /// <summary>
        /// 加载数据项
        /// </summary>
        /// <param name="result"></param>
        /// <param name="Complete"></param>
        private void arv_LoadRecordHandle(AnalysisResult result, DataModel.ProgressState Complete)
        {
            this.ProcessRunning(2, Program.Language=="EN"? "Data import in progress..." : "数据导入中...");
            HistoryDataView hdv = new HistoryDataView();
            //将edf路径赋值   
            EDF.Default.EdfPath = result.EdfPath;
            Channel.Default.AnlysisFinish = (Complete == ProgressState.Temporary || Complete == ProgressState.Compelet || (Complete == ProgressState.Ready && !string.IsNullOrEmpty(result.EdfPath)));
            if (!Channel.Default.AnlysisFinish)
            {
                Channel.Default.ProgressStauts = ProgressState.Ready;
                Channel.Default.RefreshHomeMenu();
                DataModel.LogInstance.Default.AddLog("无法进入回放页面:记录状态不对或者edf缺失", pSystem.LogManagement.LogLevel.ERROR);
                return;
            }
            this.ProcessRunning(10, Program.Language == "EN" ? "Data import in progress..." : "数据导入中...");
            Doc_MainViewRecord doc_MainViewRecord = DataBaseHelper.Default.Select<Doc_MainViewRecord>(new Doc_MainViewRecord
            {
                ID = Convert.ToInt32(result.Tag)
            });
            if (doc_MainViewRecord != null)
            {

                DataModel.LogInstance.Default.AddLog(string.Format("查询记录最后一次的记忆信息 导联方案名称为：{0}", ribbonComboBox1.TextBoxText), pSystem.LogManagement.LogLevel.DEBUG);
                //避免有时导联方案为空白的情况，这里额外赋值
                if (string.IsNullOrEmpty(ribbonComboBox1.TextBoxText))
                {
                    string[] montagepath = Channel.Default.CurrentChannelPath.Split('\\');
                    ribbonComboBox1.TextBoxText = montagepath[montagepath.Length - 1];
                }
                Channel.Default.strRecordReportConfig = doc_MainViewRecord.Reserve1.Trim();
                if (!string.IsNullOrEmpty(Channel.Default.strRecordReportConfig))
                {
                    string[] array = Channel.Default.strRecordReportConfig.Split(new char[]
                    {
                        ';'
                    });
                    if (array.Length > 3)
                    {
                        Channel.Default.IsBreathOnly = bool.Parse(array[1]);
                    }
                }
                hdv.GUID = doc_MainViewRecord.GUID;
                //if (doc_MainViewRecord.VideoHave)
                //{
                //    string dirpath = string.Format("{0}\\{1}", doc_MainViewRecord.Reserve3, doc_MainViewRecord.MatchKey);
                //    if (Directory.Exists(dirpath))
                //    {
                //        string[] files = Directory.GetFiles(dirpath, "*.mp4");
                //        foreach (string f in files)
                //        {
                //            DateTime st = default(DateTime);
                //            DateTime end = default(DateTime);
                //            Vedio.VedioManagement.Default.getStartAndEndTime(f, ref st, ref end);
                //            Console.WriteLine(string.Format("{0}的开始时间{1}，结束时间{2}", Path.GetFileName(f), st, end));
                //        }
                //    }
                //}
            }
            List<Doc_EventRecords> list = result.EventRecords.FindAll((Doc_EventRecords t) => t.EventType == (int)pChart.IMarker.MarkType.LightOff || t.EventType == (int)pChart.IMarker.MarkType.LightOn);
            bool flag = false;
            bool flag2 = false;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].EventType == (int)pChart.IMarker.MarkType.LightOff)
                {
                    Channel.Default.AnalysisReult.Sleep.LightOffTime = list[i].StartTime;
                    flag2 = true;
                }
                else if (list[i].EventType == (int)pChart.IMarker.MarkType.LightOn)
                {
                    Channel.Default.AnalysisReult.Sleep.LightOnTime = list[i].StartTime;
                    flag = true;
                }
            }
            Channel.Default.ScoreLock = (Complete == ProgressState.Compelet);
            hdv.ChartArea.MarkEnable = !Channel.Default.ScoreLock;
            DataModel.LogInstance.Default.AddLog(string.Format("判断评分状态：{0}", Channel.Default.ScoreLock ? "允许" : "锁定"), pSystem.LogManagement.LogLevel.DEBUG);
            EDF.Default.Interrupt = false;
            DateTime now = DateTime.Now;
            Channel.Default.AnalysisReult = result;
            this.ProcessRunning(20, Program.Language == "EN" ? "Data import in progress..." : "数据导入中...");
            if (hdv.GUID == "")
            {
                result.GUID = Channel.Default.CreatGUID(Channel.Default.Patient.ID, Channel.Default.Doctor.ID);
                hdv.GUID = result.GUID;
                DataModel.LogInstance.Default.AddLog(string.Format("记录原GUID为空，系统自动为其生成GUID：{0}", result.GUID), pSystem.LogManagement.LogLevel.DEBUG);
            }
            EDF edf = hdv.Bind(result.EdfPath);
            DataModel.LogInstance.Default.AddLog("完成数据绑定", pSystem.LogManagement.LogLevel.WARN);
            if (!edf.IsCorrect)
            {
                AhDung.MessageTip.ShowError(Program.Language == "EN" ? "The specified path file is not valid EDF data" : "指定的路径文件不是有效的edf数据");
                this.ProcessRunning(100, "");
                return;
            }
            Console.WriteLine(string.Format("读取EDF文件耗时【{0}ms】", (DateTime.Now - now).TotalMilliseconds));
            this.ProcessRunning(40, Program.Language == "EN" ? "Data import in progress..." : "数据导入中...");
            if (!EDF.Default.Interrupt)// && array2.Length != 0)
            {
                if (Complete == ProgressState.Ready && !string.IsNullOrEmpty(result.EdfPath))
                {
                    Complete = ProgressState.Temporary;
                    DataBaseHelper.Default.Update(new Doc_MainViewRecord
                    {
                        ID = Convert.ToInt32(result.Tag)
                    }, new Doc_MainViewRecord
                    {
                        DoctorID = Channel.Default.Doctor.ID.ToString(),
                        EdfPath = edf.EdfPath,
                        GUID = hdv.GUID == null ? Channel.Default.CreatGUID(Channel.Default.Patient.ID, Channel.Default.Doctor.ID) : hdv.GUID,//右键自动搜索result.GUID一直为null导致新增的记录guid为null  但是全局的自动搜索没有问题会正常赋值
                        ReportReady = false,
                        RecordTime = edf.StartTime,
                        //CreatTime=DateTime.Now,//全局自动搜索匹配到的记录 ，该时间不需要更改
                        //VideoHave=false,
                        DifferentVersion = 1,
                        Progress = 88,
                        FrameCount = hdv.TotalFrameCnt,
                        StartRecordTime = edf.StartTime,
                        EndRecordTime = edf.EndTime
                    });
                    Channel.Default.UpdateRecord(Convert.ToInt32(Channel.Default.AnalysisReult.Tag));
                    DataModel.LogInstance.Default.AddLog(string.Format("自动搜索匹配的记录更新数据状态和明细 {0}", hdv.GUID), pSystem.LogManagement.LogLevel.DEBUG);
                }
                result.StartTime = edf.StartTime;
                result.EndTime = edf.EndTime;
                if (!flag2)
                {
                    Channel.Default.AnalysisReult.Sleep.LightOffTime = result.StartTime;
                }
                if (!flag)
                {
                    Channel.Default.AnalysisReult.Sleep.LightOnTime = result.EndTime;
                }
                this.ProcessRunning(60, Program.Language == "EN" ? "Data import in progress..." : "数据导入中...");
                edf.EdfPath = result.EdfPath;
                if (!EDF.Default.Interrupt)
                {
                    if (Channel.Default.Patient == null)
                    {
                        Channel.Default.Patient = new Doc_PatientInfo();
                    }
                    Channel.Default.Patient.RecordTime = edf.StartTime;
                    Channel.Default.StartTime = edf.StartTime;
                    Channel.Default.EndTime = edf.EndTime;
                    Channel.Default.AnlysisFinish = true;
                    hdv.SleepStateChangedHandle += this.hdv_SleepStateChangedHandle;
                    this.m_CurrentFrameNo = 1;
                    //hdv.BindData(edf);
                    this.ProcessRunning(70, Program.Language == "EN" ? "Data import in progress..." : "数据导入中...");
                    if (!EDF.Default.Interrupt)
                    {
                        if (Channel.Default.AnalysisReult != null)
                        {
                            if (Channel.Default.AnalysisReult.GUID == result.GUID)
                            {
                                result.HasDataChange = Channel.Default.AnalysisReult.HasDataChange;
                            }
                            else
                            {
                                result.HasDataChange = true;
                            }
                        }
                        if (hdv.GUID != "")
                        {
                            AnalysisResult ret = doc_MainViewRecord.DifferentVersion > 0 ? hdv.dataManager.ReadResult() : DataBaseHelper.Default.ReadResultEx(hdv.GUID);
                            result.Epochs = ret.Epochs;
                            result.EventRecords = ret.EventRecords;
                            result.BloodOxygen = ret.BloodOxygen;
                            result.BodyMovement = ret.BodyMovement;
                            result.BodyState = ret.BodyState;
                            result.BreathEvent = ret.BreathEvent;
                            result.HeartRate = ret.HeartRate;
                            result.Sleep = ret.Sleep;
                            result.OldVision = doc_MainViewRecord.DifferentVersion <= 0;
                            DataModel.LogInstance.Default.AddLog(string.Format("读取记录的分析结果：从 {0}读取", doc_MainViewRecord.DifferentVersion > 0 ? "本地文件" : "数据库"), pSystem.LogManagement.LogLevel.DEBUG);
                        }
                        Channel.Default.AnalysisReult = result;
                        hdv.LoadAnalysisData(result, false);
                        DataModel.LogInstance.Default.AddLog("系统基础帧信息分析完成， 记录数据加载完成！", pSystem.LogManagement.LogLevel.WARN);
                        this.ProcessRunning(95, Program.Language == "EN" ? "Data import in progress..." : "数据导入中...");
                        base.Invoke(new MethodInvoker(delegate ()
                        {
                            hdv.MatchKey = edf.getMatchKey();
                            this.AddControls(hdv);
                        }));
                        Channel.Default.ProgressStauts = Complete;
                        Channel.Default.RefreshHomeMenu();
                        this.ProcessRunning(100, Program.Language == "EN" ? "Data import in progress..." : "数据导入中...");
                        if (doc_MainViewRecord.DifferentVersion < 1)
                        {///数据加载完成需要把老的记录版本号变更成最新的
                            DataBaseHelper.Default.Update(new Doc_MainViewRecord() { ID = doc_MainViewRecord.ID }, new Doc_MainViewRecord() { DifferentVersion = 1 });
                        }
                    }
                }
                Console.WriteLine(string.Format("整个数据加载耗时【{0}ms】", (DateTime.Now - now).TotalMilliseconds));
                //Console.WriteLine(string.Format("整个数据加载耗时【{0}ms】", (DateTime.Now - now).TotalMilliseconds));
                DataModel.LogInstance.Default.AddLog(string.Format("整个数据加载耗时【{0}ms】", (DateTime.Now - now).TotalMilliseconds), pSystem.LogManagement.LogLevel.DEBUG);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_TargetInstance.Dispose();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Program.IsLogin)
            {
                Channel.Default.ScoreLock = false;
                return;
            }
            if (MessageForm.Show(Program.Language=="EN"? "Are you sure to log out of the system?":"是否确认退出系统？", Program.Language == "EN" ? "CONFIRM" : "信息确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            //if (MessageForm.Show("是否确认退出系统？", "信息确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
            else
            {
                Channel.Default.ScoreLock = false;
            }
        }

        #endregion

        #region 私有方法（不重要）

        /// <summary>
        /// 根据屏幕尺寸调整各个空间的位置
        /// </summary>
        private void AdjustPosition()
        {
            #region 根据屏幕尺寸调整用户名称模块的位置
            const int USER_NAME_ANCHOR_SCREEN_RIGHT_MARGIN = 125;
            const int USER_NAME_ANCHOR_RIBBON_CAPTION_TOP_MARGIN = 5;
            const int MARGIN_PER_ELEMENT = 3;

            UsernameLabel.Text = Channel.Default.Loginer.Name;

            int centerY = ribbon1.Location.Y + USER_NAME_ANCHOR_RIBBON_CAPTION_TOP_MARGIN + ribbon1.PanelCaptionHeight / 2;
            int x = Screen.PrimaryScreen.WorkingArea.Width - USER_NAME_ANCHOR_SCREEN_RIGHT_MARGIN -
                    DropDownPictureBox.Width;
            DropDownPictureBox.Location = new Point(x, centerY - DropDownPictureBox.Height / 2);

            x = DropDownPictureBox.Location.X - MARGIN_PER_ELEMENT - UsernameLabel.Width;
            UsernameLabel.Location = new Point(x, centerY - UsernameLabel.Height / 2);

            x = UsernameLabel.Location.X - MARGIN_PER_ELEMENT - UserIconPictureBox.Width;
            UserIconPictureBox.Location = new Point(x, centerY - UserIconPictureBox.Height / 2);
            #endregion
        }

        /// <summary>
        /// 导联方案改变
        /// </summary>
        /// <param name="name"></param>
        private void Default_ConfigNameChanged(string name)
        {
            if (this.IsHandleCreated)
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    try
                    {
                        ribbonComboBox1.TextBoxText = name;
                        Control block = getControl();
                        if (block != null)
                        {
                            if (block is FunctionControls.HistoryDataView)
                            {
                                (block as FunctionControls.HistoryDataView).UpdateMontageName(name);
                            }
                            else if (block is FunctionControls.RealDataView)
                            {
                                (block as FunctionControls.RealDataView).UpdateMontageName(name);
                            }
                        }
                    }
                    catch (Exception ee)
                    {
                        DataModel.LogInstance.Default.AddLog(string.Format("没有成功显示导联方案 {0}", ee.Message), pSystem.LogManagement.LogLevel.ERROR);
                    }
                }));
            }
        }

        private void Defalut_CanceledHandle()
        {
            m_comm.AnalysisStop();
        }

        /// <summary>
        /// 导联方案下拉框下拉时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ribbonComboBox1_DropDownShowing(object sender, EventArgs e)
        {
            List<string> items = ChannelManage.Default.GetCfgNames();
            if (items.Count != ribbonComboBox1.DropDownItems.Count)
            {
                this.ribbonComboBox1.DropDownItems.Clear();
                foreach (string s in items)
                    this.ribbonComboBox1.DropDownItems.Add(new RibbonLabel() { Text = s });
            }
        }

        /// <summary>
        /// 导联方案下拉框鼠标移开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ribbonComboBox1_MouseLeave(object sender, MouseEventArgs e)
        {
            if (ribbonComboBox1.DropDownVisible)
                ribbonComboBox1.DropDownClose();
        }

        /// <summary>
        /// 导联方案选中下拉框中的选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ribbonComboBox1_DropDownItemClicked(object sender, RibbonItemEventArgs e)
        {
            string selectText = ribbonComboBox1.TextBoxText;
            string path = string.Format("{0}\\{1}", ChannelManage.Default.ConfigruationBasePath, selectText);
            if (selectText != "" && path != Channel.Default.CurrentChannelPath)
            {
                Channel.Default.CurrentChannelPath = path;
                Channel.Default.CurrentChannelTable = ChannelManage.Default.ReadChannelConfig(Channel.Default.CurrentChannelPath);
                Channel.Default.ChannelChanged();
            }
            DataModel.LogInstance.Default.AddLog(string.Format("用户 选择导联方案为{0}", selectText), pSystem.LogManagement.LogLevel.INFO);
        }

        /// <summary>
        /// 全局滤波器选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FliterRibbonButton_DropDownItemClicked(object sender, RibbonItemEventArgs e)
        {
            RibbonButton rbt = sender as RibbonButton;
            switch (rbt.Name)
            {
                case "SingleNotchRibbonButton":
                    Channel.Default.GlobalSingleNotch = rbt.SelectedItem.Text;
                    break;
                case "HighPassRibbonButton":
                    Channel.Default.GlobalHighPass = rbt.SelectedItem.Text;
                    break;
                case "LowPassRibbonButton":
                    Channel.Default.GlobalLowPass = rbt.SelectedItem.Text;
                    break;
            }
            rbt.SelectedItem.Checked = true;
            if (rbt.SelectedItem.Text == "OFF")
                rbt.Checked = false;
            else
            {
                rbt.Checked = true;
            }
            Channel.Default.reFreshView();
        }

        /// <summary>
        /// 绘制电池容量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BatteryCapacity_Paint(object sender, PaintEventArgs e)
        {
            using (Graphics g = e.Graphics)
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                int h = BatteryCapacity.Height;
                float wordSize = h * 3 / 4 - 1;
                using (Font f = new Font("宋体", 9))
                {
                    int y = h / 2 - f.Height / 2;
                    string strValue = string.Format("{0}%", BatteryCapacityValue);
                    g.DrawString(strValue, f, new SolidBrush(Color.Black), 0, y);
                    int rX = (int)(g.MeasureString(strValue, f).Width + 2);
                    using (Pen p = new Pen(Color.Black, 1))
                    {
                        Rectangle rect = new Rectangle(rX, y, 28, h - 8);
                        g.DrawRectangle(p, rect);
                        g.FillRectangle(BatteryCapacityValue <= 35 && BatteryCapacityValue > 15 ?
                                        Brushes.Yellow :
                                        BatteryCapacityValue <= 15 && BatteryCapacityValue > 0 ?
                                        Brushes.Red :
                                        Brushes.LightGreen, rect.X + 2, rect.Y + 2, (int)((rect.Width - 3) * (BatteryCapacityValue / 100.0)), rect.Height - 3);
                        g.DrawLine(p, new Point(rect.Right + 2, rect.Top + rect.Height / 2 - 3), new Point(rect.Right + 2, rect.Top + rect.Height / 2 + 3));
                    }
                }
                string tips = string.Format(Program.Language=="EN"? "The remaining monitored time is {0} hours and {1} minutes" : "可监测的时间剩余{0}时{1}分", BatteryLastTime / 60, BatteryLastTime % 60);
                if (BatteryCapacity.ToolTipText != tips)
                    BatteryCapacity.ToolTipText = tips;
            }
        }

        /// <summary>
        /// 离线状态
        /// </summary>
        private void Default_OffLineHandler()
        {
            this.Invoke(new MethodInvoker(() =>
            {
                toolStripStatusLabel3.Text =Program.Language=="EN"?"Offline":"离线";
            }));
        }

        /// <summary>
        /// 离线/在线状态
        /// </summary>
        /// <param name="result"></param>
        private void client_IOConnectHandle(bool result)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                toolStripStatusLabel3.Text = result ? (Program.Language == "EN" ? "Online" : "在线") : (Program.Language == "EN" ? "Offline" : "离线");
            }));
        }

        /// <summary>
        /// 裁剪edf文件通过系统开关灯事件或者小睡事件
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="stoptime"></param>
        /// <param name="byLightOnOff"></param>
        /// <returns></returns>
        private bool EditEDF_OnOffTimesEventHandler(DateTime starttime, DateTime stoptime, bool byLightOnOff)
        {
            if (EditEdfByOnOffTimeEventHandler != null)
                return EditEdfByOnOffTimeEventHandler.Invoke(starttime, stoptime, byLightOnOff);
            return false;
        }

        /// <summary>
        /// 委托-=没用  额外写 移除委托的方法
        /// </summary>
        private void ReduceToOnlyOneEvent()
        {
            if (EditEdfByOnOffTimeEventHandler == null) return;
            Delegate[] dels = EditEdfByOnOffTimeEventHandler.GetInvocationList();
            for (int i = 0; i < dels.Length - 1; i++)
            {
                object delObj = dels[i].GetType().GetProperty("Method").GetValue(dels[i], null);
                string funcName = (string)delObj.GetType().GetProperty("Name").GetValue(delObj, null);
                Console.WriteLine(funcName);
                EditEdfByOnOffTimeEventHandler -= dels[i] as EditEdfByOnOffTimeDelegate;
            }
        }

        /// <summary>
        /// 帧位置改变
        /// </summary>
        /// <param name="frameCnt"></param>
        private void pan_CurrentFrameChangedHandler(int frameCnt)
        {
            Control block = getControl();
            if (block != null)
            {
                if (block is FunctionControls.HistoryDataView)
                {
                    (block as FunctionControls.HistoryDataView).ChangeFrameNo(frameCnt);
                }
            }
        }

        /// <summary>
        /// 每秒刷新睡眠分期的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (m_SleepStateChange)
            {
                m_SleepStateChange = false;
                string[] state = new string[m_SleepState.Length];
                Array.Copy(m_SleepState, 0, state, 0, state.Length);
                ribbonTextBox6.TextBoxText = state[0];
                ribbonTextBox7.TextBoxText = state[1];
                ribbonTextBox8.TextBoxText = state[2];
                ribbonTextBox9.TextBoxText = state[3];
                ribbonTextBox10.TextBoxText = state[4];
                ribbonTextBox11.TextBoxText = state[5];
                ribbonTextBox12.TextBoxText = state[6];
                ribbonTextBox13.TextBoxText = state[7];
                ribbonTextBox14.TextBoxText = state[8];
                ribbonTextBox15.TextBoxText = state[9];
            }
        }

        /// <summary>
        /// 点击不同时基图标 改变时基
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseTimeChange_Click(object sender, EventArgs e)
        {
            RibbonButton rbt = sender as RibbonButton;
            if (rbt.Text != bak_butt.Text)
            {
                bak_butt.Checked = false;
                //string strValue = rbt.Text.ToUpper();
                //if (strValue.Contains("S"))
                //{
                //    Channel.Default.BaseTimeLineSpan = int.Parse(strValue.Replace("S", ""));
                //}
                //else if (strValue.Contains("MIN"))
                //{
                //    Channel.Default.BaseTimeLineSpan = int.Parse(strValue.Replace("MIN", "")) * 60;
                //}
                Channel.Default.BaseTimeLineSpan = Convert.ToInt32(rbt.Tag);
                rbt.Checked = true;
                bak_butt = rbt;
                Channel.Default.ChannelChanged(true);
                //if (Channel.Default.VedioEnable)
                //{
                //    Control block = getControl();
                //    if (block != null)
                //    {
                //        if (block is FunctionControls.HistoryDataView)
                //        {
                //            (block as FunctionControls.HistoryDataView).BaseTimeChanged(true);
                //        }
                //    }
                //}
            }
        }
        /// <summary>
        /// 点击不同的分期图标 标注分期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SleepMark_Click(object sender, EventArgs e)
        {
            RibbonButton rbt = (RibbonButton)sender;
            string strTyp = "";
            string strvalue = "";
            int value = 5;
            switch (rbt.Name)
            {
                case "ribbonButton42"://w
                    strvalue = strTyp = "W";
                    value = 5;
                    break;
                case "ribbonButton45"://n1
                    strvalue = "1";
                    strTyp = "N1";
                    value = 3;
                    break;
                case "ribbonButton46"://n2
                    strTyp = "N2";
                    strvalue = "2";
                    value = 2;
                    break;
                case "ribbonButton47"://n3
                    strTyp = "N3";
                    strvalue = "3";
                    value = 1;
                    break;
                case "ribbonButton48"://r
                    strvalue = strTyp = "R";
                    value = 4;
                    break;
                case "LockButton":
                    if (Channel.Default.ScoreLock)
                    {
                        AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "Need to return to the homepage to unlock the playback record progress status as pending completion" : "需要回到首页解锁回放记录进度状态为待完成");
                        return;
                    }
                    //解锁
                    string LOCK_TEXT = Program.Language == "EN" ? "Lock " : "锁定";
                    string UNLOCK_TEXT = Program.Language=="EN"?"UnLock ": "解锁";
                    string SUCCESS = Program.Language == "EN" ? "Success " : "成功";
                    if (Channel.Default.SleepLock)
                    {
                        Channel.Default.SleepLock = false;
                        LockButton.SmallImage = Properties.Resources.Lockno;
                        LockButton.ToolTip = LOCK_TEXT;
                        DataModel.LogInstance.Default.AddLog("用户点击 解锁按钮，解锁成功", pSystem.LogManagement.LogLevel.INFO);
                        AhDung.MessageTip.ShowOk(UNLOCK_TEXT + SUCCESS);
                    }
                    //锁定
                    else
                    {
                        Channel.Default.SleepLock = true;
                        LockButton.SmallImage = Properties.Resources.Lockyes;
                        LockButton.ToolTip = UNLOCK_TEXT;
                        DataModel.LogInstance.Default.AddLog("用户点击 锁定按钮，锁定成功", pSystem.LogManagement.LogLevel.INFO);
                        AhDung.MessageTip.ShowOk(LOCK_TEXT + SUCCESS);
                    }
                    return;
            }
            if (!Channel.Default.SleepLock && !Channel.Default.ScoreLock)
            {
                Control block = getControl();
                if (block != null)
                {
                    if (block is FunctionControls.HistoryDataView)
                    {
                        (block as FunctionControls.HistoryDataView).MarkSleep(value);
                    }
                }
            }
            else
            {
                AhDung.MessageTip.ShowWarning(Channel.Default.SleepLock ? Program.Language == "EN" ? "Please unlock the sleep staging enable lock first!" : "请先解开睡眠分期使能锁！" : Channel.Default.ScoreLock ? Program.Language == "EN" ? "The rating has ended. If you need to re rate, please unlock the rating status first" : "评分已结束，如需重新评分，请先解锁评分状态！" : Program.Language == "EN" ? "No permission" : "无权限");

                //AhDung.MessageTip.ShowWarning(Channel.Default.SleepLock ? "请先解开睡眠分期使能锁！" : Channel.Default.ScoreLock ? "评分已结束，如需重新评分，请先解锁评分状态！" : "无权限");
            }
        }

        /// <summary>
        /// 体位的重绘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,
                                        this.panel1.ClientRectangle,
                                        Color.FromArgb(221, 221, 221),//左
                                        1,
                                        ButtonBorderStyle.None,
                                        Color.FromArgb(221, 221, 221),//上
                                        1,
                                        ButtonBorderStyle.Solid,
                                       Color.FromArgb(221, 221, 221),//右
                                        1,
                                        ButtonBorderStyle.None,
                                        Color.FromArgb(221, 221, 221),//底
                                        1,
                                        ButtonBorderStyle.None);
        }

        private void DropDownPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            if (Convert.ToBoolean(UserHandleContextMenuStrip.Tag))
                UserHandleContextMenuStrip.Close();
            else
            {
                int x = DropDownPictureBox.Location.X + DropDownPictureBox.Width - UserHandleContextMenuStrip.Width;
                int y = UserIconPictureBox.Location.Y + UserIconPictureBox.Height;
                UserHandleContextMenuStrip.Show(new Point(x, y));
            }
        }

        private void UserHandleContextMenuStrip_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            UserHandleContextMenuStrip.Tag = false;
        }

        private void UserHandleContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            UserHandleContextMenuStrip.Tag = true;
        }

        #endregion



        #region 私有方法（重要）

        /// <summary>
        /// 恢复按钮状态改变
        /// </summary>
        /// <param name="recoverstate"></param>
        private void Default_RecoverStateChangedHandler(bool recoverstate)
        {
            RecoverSaveFile.SmallImage = recoverstate ? Properties.Resources.Recover : Properties.Resources.NoRecover;
        }

        /// <summary>
        /// 刷新主菜单
        /// </summary>
        private void FreshHomeMenu()
        {
            this.Invoke(new MethodInvoker(() =>
            {
                switch (Channel.Default.ProgressStauts)
                {
                    case DataModel.ProgressState.MonitorFinish:
                        ribbonPanel12.Enabled = ribbonPanel2.Enabled = true;
                        ribbonButton6.Enabled = true;
                        ribbonButton7.Enabled = ribbonButton12.Enabled = ribbonButton32.Enabled = false;
                        ribbonButton49.Visible = true;
                        ribbonButton49.Enabled = true;
                        ribbonButton1.Visible = false;
                        break;
                    case DataModel.ProgressState.Monitoring:
                        ribbonPanel12.Enabled = ribbonPanel2.Enabled = false;
                        ribbonPanel13.Visible = true;
                        //ribbonPanel14.Visible = true;
                        ribbonButton49.Visible = true;
                        ribbonButton49.Enabled = false;
                        ribbonButton1.Visible = false;
                        ribbonPanel13.Visible = true;
                        ribbonItemGroup2.Visible = true;
                        // ribbonItemGroup1.Visible = true;
                        ribbonItemGroup5.Visible = true;
                        ribbonItemGroup7.Visible = false;
                        ribbonItemGroup4.Visible = false;
                        break;
                    case DataModel.ProgressState.Running:
                        ribbonPanel12.Enabled = ribbonPanel2.Enabled = false;
                        ribbonPanel13.Visible = ribbonPanel14.Visible = false;
                        ribbonButton49.Visible = true;
                        ribbonButton49.Enabled = false;
                        ribbonButton1.Visible = false;
                        break;
                    case DataModel.ProgressState.Replay:
                        ribbonPanel2.Visible = ribbonPanel12.Visible = true;
                        ribbonPanel2.Enabled = true;
                        ribbonPanel12.Enabled = false;
                        ribbonButton7.Enabled = ribbonButton6.Enabled = true;
                        ribbonButton12.Enabled = ribbonButton32.Enabled = false;
                        ribbonPanel13.Visible = ribbonPanel14.Visible = true;
                        ribbonItemGroup2.Visible = true;
                        // ribbonItemGroup1.Visible = true;
                        ribbonItemGroup5.Visible = true;
                        ribbonItemGroup7.Visible = true;
                        ribbonItemGroup4.Visible = true;
                        //ribbonButton42.Visible = ribbonButton45.Visible = ribbonButton46.Visible = ribbonButton47.Visible = ribbonButton48.Visible = true;
                        ribbonButton49.Visible = true;
                        ribbonButton49.Enabled = true;
                        ribbonButton1.Visible = false;
                        break;
                    case DataModel.ProgressState.Temporary:
                        ribbonPanel13.Visible = true;
                        ribbonPanel14.Visible = true;
                        ribbonItemGroup5.Visible = true;
                        ribbonItemGroup7.Visible = true;
                        ribbonItemGroup4.Visible = true;
                        ribbonPanel2.Visible = ribbonPanel12.Visible = true;
                        ribbonPanel2.Enabled = ribbonPanel12.Enabled = true;
                        ribbonButton6.Enabled = ribbonButton7.Enabled = ribbonButton12.Enabled = ribbonButton32.Enabled = true;
                        ribbonItemGroup2.Visible = true;
                        //  ribbonItemGroup1.Visible = true;
                        //ribbonButton42.Visible = ribbonButton45.Visible = ribbonButton46.Visible = ribbonButton47.Visible = ribbonButton48.Visible = true;
                        ribbonButton49.Visible = true;
                        ribbonButton49.Enabled = true;
                        ribbonButton1.Visible = false;
                        break;
                    case DataModel.ProgressState.Compelet:
                        ribbonPanel2.Visible = ribbonPanel12.Visible = true;
                        ribbonPanel2.Enabled = ribbonPanel12.Enabled = true;
                        ribbonButton7.Enabled = ribbonButton6.Enabled = !Channel.Default.ScoreLock;
                        ribbonButton12.Enabled = ribbonButton32.Enabled = true;
                        ribbonPanel13.Visible = ribbonPanel14.Visible = true;
                        ribbonItemGroup2.Visible = true;
                        //  ribbonItemGroup1.Visible = true;
                        ribbonItemGroup5.Visible = true;
                        ribbonItemGroup7.Visible = true;
                        ribbonItemGroup4.Visible = true;
                        //ribbonButton42.Visible = ribbonButton45.Visible = ribbonButton46.Visible = ribbonButton47.Visible = ribbonButton48.Visible = false;
                        ribbonButton49.Visible = true;
                        ribbonButton49.Enabled = true;
                        ribbonButton1.Visible = false;
                        break;
                    case DataModel.ProgressState.Ready:
                        ribbonPanel2.Visible = ribbonPanel12.Visible = true;
                        ribbonPanel12.Enabled = false;
                        ribbonPanel2.Enabled = true;
                        ribbonButton7.Enabled = false;
                        ribbonButton6.Enabled = true;
                        ribbonButton12.Enabled = ribbonButton32.Enabled = true;
                        ribbonPanel13.Visible = ribbonPanel14.Visible = false;
                        ribbonButton49.Visible = true;
                        ribbonButton49.Enabled = true;
                        ribbonButton1.Visible = false;
                        break;
                    case DataModel.ProgressState.None:
                        ribbonPanel12.Enabled = false;
                        ribbonPanel2.Enabled = true;
                        ribbonButton6.Enabled = true;
                        ribbonPanel13.Visible = ribbonPanel14.Visible = false;
                        ribbonButton7.Enabled = ribbonButton12.Enabled = ribbonButton32.Enabled = false;
                        ribbonButton49.Visible = true;
                        ribbonButton49.Enabled = true;
                        ribbonButton1.Visible = false;
                        break;
                }
                if (ribbonItemGroup2.Visible)
                {
                    ribbon1.ActiveTab = ribbonTab1;
                    foreach (RibbonItem c in ribbonItemGroup2.Items)
                    {
                        if (c is RibbonButton)
                        {
                            if (!string.IsNullOrEmpty(c.Tag.ToString()))
                            {
                                if (Convert.ToInt32(c.Tag) == Channel.Default.BaseTimeLineSpan)
                                {
                                    c.Checked = true;
                                }
                                else
                                {
                                    c.Checked = false;
                                }
                            }
                        }
                    }
                }
                //ribbonButton6.Visible = false;///送检版本去掉自动分析
                ribbon1.Refresh();//该方法会强制控件重绘其自身及其所有子级。 这等效于将 Invalidate 方法设置为 true 并将该方法与 Update 结合使用
            }));
        }

        /// <summary>
        /// 创建通道时触发
        /// </summary>
        /// <param name="item"></param>
        private void m_channel_Management_ChannelCreatingHandle(pChart.CurveItem item)
        {

        }

        /// <summary>
        /// 开始检测 handle
        /// </summary>
        /// <param name="info"></param>
        /// <param name="showBefore"></param>
        private void patient_NextStepHandle(DataBaseCom.Doc_PatientInfo info, bool showBefore = false)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                Block.NewDevice dev = new Block.NewDevice();
                dev.Owner = this;
                dev.Init(info, showBefore);
                dev.StartMonitorHandler += dev_StartMonitorHandler;
                dev.ShowDialog();
                if (addrealdataview && !reconnection && hasvideo)
                {
                    DataModel.LogInstance.Default.AddLog("进行阻抗测试", pSystem.LogManagement.LogLevel.WARN);
                    Block.ImpedanceView view = new Block.ImpedanceView();
                    view.Init(realdataview_protocol);
                    view.ShowDialog();

                }
                addrealdataview = false;
            }));

        }

        /// <summary>
        /// 开始监测 work
        /// </summary>
        /// <param name="client"></param>
        /// <param name="channelconfig"></param>
        private bool dev_StartMonitorHandler(IDevice client, DataTable channelconfig)
        {
            RealDataView realDataView = new RealDataView();
            client.PortClient.IOConnectHandle += this.client_IOConnectHandle;
            Channel.Default.CurrentChannelTable = channelconfig;
            for (int i = 0; i < channelconfig.Rows.Count; i++)
            {
                CurveItem item = Channel.Default.CreatChannel(channelconfig.Rows[i]);
                realDataView.ChartArea.AddCurve(item);
            }
            if (realDataView.Start(client, this.panel1))
            {
                this.AddControls(realDataView);
                realdataview_protocol = realDataView.Protocol;
                addrealdataview = true;
                reconnection = realDataView.isreconnection;
                hasvideo = realDataView.hasvideo;
                return true;
            }
            realDataView.ChartArea.Dispose();
            return false;
        }

        /// <summary>
        /// 开始分析 handle
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isauto"></param>
        private void ihd_StartHistroyAnalysisByEDFHandle(string path, bool isauto = false, string guid = "")
        {
            //ribbonButton6.Text = "停止分析";
            ribbonButton6.Text = ML.GetText("MAINFORM_STOP_ANALYZING", "停止分析");
            FunctionControls.tools.ProgressTipForm.Defalut.Text = Program.Language=="EN"?"": "Data Analysis";
            FunctionControls.tools.ProgressTipForm.Defalut.Argument = new object[] { path, isauto, guid };
            FunctionControls.tools.ProgressTipForm.Defalut.DoWork += Defalut_DoWork;
            //ProgressTipForm.Defalut.CanceledHandle += Defalut_CanceledHandle;
            if (FunctionControls.tools.ProgressTipForm.Defalut.ShowDialog() != DialogResult.OK)
            {

            }
            //ribbonButton6.Text = "开始分析";
            ribbonButton6.Text = ML.GetText("MAINFORM_START_ANALYZING", "开始分析");
        }

        /// <summary>
        /// 开始分析 work
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Defalut_DoWork(ProgressTipForm sender, DoWorkEventArgs e)
        {
            if (sender.CancellationPending)
            {
                goto Last;
            }
            object[] array = sender.Argument as object[];
            if (array.Length != 3)
            {
                return;
            }
            string path = array[0] as string;
            string dataGuid = array[2] as string;
            bool isaddSocre = Convert.ToBoolean(array[1]);  //是否自动分析
            string fileName = Path.GetFileName(path);
            sender.SetProgress(20, string.Format(Program.Language=="EN"? "Load Data Source":"加载数据源:{0} ...", fileName));
            if (sender.CancellationPending)
            {
                goto Last;
            }
            if (!EDF.Default.Interrupt)
            {
                bool flag = false;
                string movePath = path;
                if (!isaddSocre)
                {
                    DateTime recordTime = EDF.Default.getRecordTime(EDF.Default.ReadFile(fileName, false));
                    movePath = string.Format("{2}\\{0:yyyy-MM-dd}\\{1}", recordTime, fileName, EDF.Default.DefaultEdfSavePath);
                    flag = this.Docopy(path, movePath);
                    if (sender.CancellationPending)
                    {
                        goto Last;
                    }
                }
                HistoryDataView hdv = new HistoryDataView();
                if (dataGuid == "")
                {
                    dataGuid = Channel.Default.CreatGUID(Channel.Default.Patient.ID, Channel.Default.Doctor.ID);
                }
                else
                {
                    hdv.GUID = dataGuid;
                }
                sender.SetProgress(30, string.Format(Program.Language == "EN" ? "Load Data Source:{0} ..." : "加载数据源:{0} ...", fileName));
                EDF edf = hdv.Bind((flag ? movePath : path), true);
                //将edf文件路径赋值
                EDF.Default.EdfPath = edf.EdfPath;
                sender.SetProgress(50, string.Format(Program.Language == "EN" ? "Load Data Source:{0} ..." : "加载数据源:{0} ...", fileName));
                if (!EDF.Default.Interrupt)
                {
                    if (Channel.Default.Patient == null)
                    {
                        Channel.Default.Patient = new Doc_PatientInfo();
                    }
                    Channel.Default.Patient.RecordTime = edf.StartTime;
                    Channel.Default.StartTime = edf.StartTime;
                    Channel.Default.EndTime = edf.EndTime;
                    hdv.SleepStateChangedHandle += this.hdv_SleepStateChangedHandle;
                    this.m_CurrentFrameNo = 1;
                    bool flag2 = true;
                    sender.SetProgress(60, Program.Language == "EN" ? "Loading Interface..." : "加载界面...");
                    if (sender.CancellationPending)
                    {
                        goto Last;
                    }
                    base.Invoke(new MethodInvoker(delegate ()
                    {
                        hdv.MatchKey = edf.getMatchKey();
                        this.RemoveControls(hdv);
                    }));
                    this.m_TargetInstance.Init(dataGuid, edf.EdfPath, edf.StartTime, edf.StartTime, edf.EndTime, Channel.Default.IsBreathOnly);
                    if (flag2)
                    {
                        sender.SetProgress(70, Program.Language == "EN" ? "Loading Interface..." : "加载界面...");
                        if (hdv.GUID != "" && isaddSocre)
                        {
                            AnalysisResult ret = hdv.dataManager.ReadResult();
                            ret.Tag = Channel.Default.AnalysisReult.Tag;
                            ret.GUID = hdv.GUID;
                            Channel.Default.AnalysisReult = ret;
                            if (Channel.Default.AnalysisReult.StartTime.Year == 1)
                                Channel.Default.AnalysisReult.StartTime = edf.StartTime;
                            if (Channel.Default.AnalysisReult.EndTime.Year == 1)
                                Channel.Default.AnalysisReult.EndTime = edf.EndTime;
                        }
                        DataModel.LogInstance.Default.AddLog(string.Format("查询记录最后一次的记忆信息 导联方案名称为：{0}", ribbonComboBox1.TextBoxText), pSystem.LogManagement.LogLevel.DEBUG);
                        //避免有时导联方案为空白的情况，这里额外赋值
                        if (string.IsNullOrEmpty(ribbonComboBox1.TextBoxText))
                        {
                            string[] montagepath = Channel.Default.CurrentChannelPath.Split('\\');
                            ribbonComboBox1.TextBoxText = montagepath[montagepath.Length - 1];
                        }
                        hdv.LoadAnalysisData(Channel.Default.AnalysisReult, false);
                        if (sender.CancellationPending)
                        {
                            hdv.SourceDispose();
                            goto Last;
                        }
                        Channel.Default.ProgressStauts = ProgressState.Temporary;
                    }
                    else
                    {
                        Channel.Default.ProgressStauts = ProgressState.Replay;
                    }
                    Channel.Default.AnalysisReult.HasDataChange = false;
                    sender.SetProgress(80, Program.Language == "EN" ? "Update Record" : "更新记录");
                    DataBaseHelper.Default.Update(new Doc_MainViewRecord
                    {
                        ID = Convert.ToInt32(Channel.Default.AnalysisReult.Tag)
                    }, new Doc_MainViewRecord
                    {
                        DoctorID = Channel.Default.Doctor.ID.ToString(),
                        EdfPath = edf.EdfPath,
                        //GUID = hdv.GUID,
                        ReportReady = false,
                        RecordTime = edf.StartTime,
                        //CreatTime=DateTime.Now,
                        //VideoHave=false,//新建病例 不采集直接导入edf 新诊断
                        Progress = 88,
                        FrameCount = hdv.TotalFrameCnt,
                        StartRecordTime = edf.StartTime,
                        EndRecordTime = edf.EndTime

                    });
                    if (sender.CancellationPending)
                    {
                        hdv.SourceDispose();
                        goto Last;
                    }
                    sender.SetProgress(100, Program.Language == "EN" ? "Complete" : "完成");
                    if (!EDF.Default.Interrupt)
                    {
                        base.Invoke(new MethodInvoker(delegate ()
                        {
                            this.AddControls(hdv);
                        }));
                    }
                    Channel.Default.RefreshHomeMenu();
                    Channel.Default.UpdateRecord(Convert.ToInt32(Channel.Default.AnalysisReult.Tag));
                }
            }
            return;
            Last:
            e.Cancel = true;
            ///删除记录
            DataBaseHelper.Default.Delete(new Doc_MainViewRecord
            {
                ID = Convert.ToInt32(Channel.Default.AnalysisReult.Tag)
            });
            Channel.Default.UpdateRecord(-1);
            LoadHomePage();
        }

        /// <summary>
        /// 开始分析 work
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Analysis_DoWork(ProgressTipForm sender, DoWorkEventArgs e)
        {
            DateTime onetime = DateTime.Now;
            HistoryDataView historyDataView = sender.Argument as HistoryDataView;
            sender.SetProgress(2,Program.Language=="EN"? "Data Initialization..." : "数据初始化...");
            AnalysisResult analysisResult = historyDataView.getAnalysisResult(true);
            DataModel.LogInstance.Default.AddLog(string.Format("初始化信息 [GUID：{0} startTime：{1} lightoff：{2} lighton：{3}] type：{4}", analysisResult.GUID, analysisResult.StartTime, analysisResult.LightOffTime, analysisResult.LightOnTime, Channel.Default.IsBreathOnly ? "初筛" : "多导睡眠"), pSystem.LogManagement.LogLevel.DEBUG);
            if (!this.m_TargetInstance.Init(analysisResult.GUID, analysisResult.EdfPath, analysisResult.StartTime, analysisResult.LightOffTime, analysisResult.LightOnTime, Channel.Default.IsBreathOnly))
            {
                e.Cancel = true;
                sender.SetError(Program.Language == "EN" ? "The time range for analyzing the on/off light is invalid. Please recalibrate and try again" : "尝试分析的开关灯时间范围失效，请重新标定后重试！");
                Channel.Default.ProgressStauts = ProgressState.Replay;
            }
            else
            {
                bool autoAnalysis = Channel.Default.AnalysisStateWord != 0;
                if (autoAnalysis)
                {
                    sender.SetProgress(10, Program.Language == "EN" ? "Start Analyzing Services..." : "开启分析服务...");
                    string fileName = Path.GetFileName(analysisResult.EdfPath);
                    bool IsReady = false;
                    if (!this.m_TargetInstance.upLoadDataReady(out IsReady))
                    {
                        e.Cancel = true;
                        sender.SetError(Program.Language == "EN" ? "Unable to open analysis service" : "无法开启分析服务");
                        Channel.Default.ProgressStauts = ProgressState.Replay;
                        return;
                    }
                    if (sender.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                    if (!IsReady)
                    {
                        sender.SetProgress(10, string.Format(Program.Language == "EN" ? "Starting to import {0} into the Analysis Center..." : "开始导入{0}到分析中心...", (object)fileName));
                        if (sender.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                        if (!this.m_TargetInstance.upLoadData(historyDataView.getDataSource()))
                        {
                            e.Cancel = true;
                            sender.SetError(Program.Language=="EN"? "File import failed!" : "文件导入失败！");
                            return;
                        }
                        byte[] numArray = new byte[0];
                    }
                    if (sender.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                else
                {
                    DataModel.LogInstance.Default.AddLog("不进入AI分析：未选择待分析的事件类别", pSystem.LogManagement.LogLevel.ERROR);
                }
                //防止取消分析后直接再次分析，所以等待2秒，让后台算法进程释放
                System.Threading.Thread.Sleep(2000);
                sender.SetProgress(30,Program.Language=="EN"? "Import User Tag Events..." : "导入用户标记事件...");
                DateTime now1 = DateTime.Now;
                if (!this.m_TargetInstance.SubmitAnalysisConditions(analysisResult.Epochs, analysisResult.EventRecords, autoAnalysis))
                {
                    e.Cancel = true;
                    sender.SetError(Program.Language == "EN" ? "Import user tag event failed!" : "导入用户标记事件失败！");
                }
                else
                {
                    Console.WriteLine(string.Format("上传数据耗时：{0}ms", (object)(DateTime.Now - now1).TotalMilliseconds).ToString());
                    DateTime now2 = DateTime.Now;
                    sender.SetProgress(60, Program.Language == "EN" ? "Data Analysis..." : "数据分析...");

                    //StartDelegate += new DataAnalysisDelegate(StartFunctionDelegate);
                    //StartDelegate.Invoke(sender);
                    /*var rtnMsg = new PubReturnMsg();
                    rtnMsg.PressureMuzzleFlowData = Channel.Default.PressureMuzzleFlowChannel;
                    SendDelegate += new DataSendDelegate(SendFunctionDelegate);
                    SendDelegate.Invoke(rtnMsg.ToXmlString(), sender);*/
                    //sender.SetProgress(65, "鼻压力数据分析...");

                    if (sender.CancellationPending)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        DataModel.LogInstance.Default.AddLog("自动分析                开始获取分析结果", pSystem.LogManagement.LogLevel.DEBUG);
                        AnalysisResult result = AnalysisResult.Convert(this.m_TargetInstance.getResult(Channel.Default.SystemSetting));

                        if (!sender.CancellationPending)
                            DataModel.LogInstance.Default.AddLog("自动分析                获取分析结果完成", pSystem.LogManagement.LogLevel.WARN);
                        result.StartTime = analysisResult.StartTime;
                        result.EndTime = analysisResult.EndTime;
                        result.Tag = Channel.Default.AnalysisReult.Tag;
                        int cnt = result.Epochs.Where(t => t.Stage != (int)SleepStageEnum.W).Count();
                        analysisResult.EventRecords.RemoveAll(t => t.ByHandDelete);///移除掉手动删除的事件
                        if (sender.CancellationPending)
                        {
                            e.Cancel = true;
                        }
                        else
                        {
                            if (result.Epochs.Count == 0 || (analysisResult.EventRecords.Count > 0 && result.EventRecords.Count == 0 && cnt > 0))
                            {
                                e.Cancel = true;
                                sender.SetError(Program.Language == "EN" ? "Analysis Failed!" : "分析失败！");
                            }
                            if (!e.Cancel)
                            {  
                                Channel.Default.AnalysisReult = result;
                                sender.SetProgress(90, Program.Language=="EN"? "Load The Analyzed Data..." : "加载分析后的数据...");
                                DataModel.LogInstance.Default.AddLog("自动分析                开始加载分析后的数据", pSystem.LogManagement.LogLevel.DEBUG);
                                historyDataView.LoadAnalysisData(Channel.Default.AnalysisReult, true);
                                if (!sender.CancellationPending)
                                    DataModel.LogInstance.Default.AddLog("自动分析                加载分析后的数据完成", pSystem.LogManagement.LogLevel.WARN);
                                if (sender.CancellationPending)
                                {
                                    e.Cancel = true;
                                }
                                else
                                {
                                    Channel.Default.ProgressStauts = ProgressState.Temporary;
                                    sender.SetProgress(100, Program.Language == "EN" ? "Analysis Completed" : "分析完成");
                                    GC.Collect();
                                    Channel.Default.AnalysisReult.HasDataChange = false;
                                    Channel.Default.RefreshHomeMenu();
                                    Console.WriteLine(string.Format("取数据到加载耗时：{0}ms", (object)(DateTime.Now - now2).TotalMilliseconds).ToString());
                                }
                            }
                            else
                            {
                                GC.Collect();
                                Channel.Default.RefreshHomeMenu();
                            }
                        }
                    }
                }
            }
            Console.WriteLine(string.Format("分析完成总耗时：{0}ms", (object)(DateTime.Now - onetime).TotalMilliseconds).ToString());
        }

        /*[DllImport("DataAnalysis.dll")]
        public static extern void DataAnalysis();

        public delegate void DataAnalysisDelegate(ProgressTipForm sender);
        public delegate void DataSendDelegate(string result, ProgressTipForm sender);
        public delegate void DataReceiveDelegate(ProgressTipForm sender);

        public DataAnalysisDelegate StartDelegate;
        private void StartFunctionDelegate(ProgressTipForm sender)
        {
            sender.SetProgress(61, "鼻压力数据分析DLL开启...");
            DataAnalysis();
        }

        public DataSendDelegate SendDelegate;       
        private void SendFunctionDelegate(string result, ProgressTipForm sender)
        {
            sender.SetProgress(62, "发送鼻压力数据...");
            resCode = result;
            HttpResStart(sender);
        }

        public DataReceiveDelegate ReceiveDelegate;
        private void ReceiveFunctionDelegate(ProgressTipForm sender)
        {
            sender.SetProgress(61, "鼻压力数据分析结果接收...");
            
        }

        public string resCode;
        private void HttpResStart(ProgressTipForm sender)
        {
            try
            {
                byte[] sendData = null;//要发送的字节数组
                TcpClient client = null;//TcpClient实例
                NetworkStream stream = null;//网络流
                IPAddress remoteIP = IPAddress.Parse("127.0.0.1");//远程主机IP
                int remotePort = 8087;//远程主机端口
                sendData = Encoding.Default.GetBytes(resCode);//获取要发送的字节数组
                sender.SetProgress(30, "导入用户标记事件...");
                client = new TcpClient();//实例化TcpClient
                try
                {
                    client.Connect(remoteIP, remotePort);//连接远程主机
                    stream = client.GetStream();//获取网络流
                    stream.Write(sendData, 0, sendData.Length);//将数据写入网络流
                    stream.Close();//关闭网络流
                    client.Close();//关闭客户端
                }
                catch (System.Exception ex)
                {

                }

            }
            catch (Exception ex)
            {

            }
        }*/

        private ChannelFiliter xfChannel3 = null;      

        /// <summary>
        /// 生成睡眠报告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportReport_DoWork(ProgressTipForm sender, DoWorkEventArgs e)
        {
            object[] array = sender.Argument as object[];
            int num = Convert.ToInt32(array[1]);
            string rpath = Convert.ToString(array[0]);
            int num2 = 0;
            if (array[2] != null)
            {
                HistoryDataView historyDataView = array[2] as HistoryDataView;
                AnalysisResult analysisResult = historyDataView.getAnalysisResult();
                if (!this.m_TargetInstance.Init(analysisResult.GUID, analysisResult.EdfPath, analysisResult.StartTime, analysisResult.LightOffTime, analysisResult.LightOnTime, Channel.Default.IsBreathOnly))
                {
                    AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "The time range for analyzing the on/off light is invalid. Please recalibrate and try again" : "尝试分析的开关灯时间范围失效，请重新标定后重试！", -1, null, null, false);
                    DataModel.LogInstance.Default.AddLog("尝试分析的开关灯时间范围失效，请重新标定后重试", pSystem.LogManagement.LogLevel.ERROR);
                    return;
                }
                sender.SetProgress(15,Program.Language=="EN"? "Import User Tag Events..." : "导入用户标记事件...");
                if (!this.m_TargetInstance.SubmitAnalysisConditions(analysisResult.Epochs, analysisResult.EventRecords, false))
                {
                    sender.SetError(Program.Language == "EN" ? "Import user tag event failed" : "导入用户标记事件失败！");
                    DataModel.LogInstance.Default.AddLog("导入用户标记事件失败！", pSystem.LogManagement.LogLevel.ERROR);
                    return;
                }
                sender.SetProgress(30, Program.Language == "EN" ? "Analyze Data..." : "分析数据...");
                AnalysisResult analysisResult2 = AnalysisResult.Convert(this.m_TargetInstance.getResult(Channel.Default.SystemSetting));
                analysisResult2.StartTime = Channel.Default.AnalysisReult.StartTime;
                analysisResult2.EndTime = Channel.Default.AnalysisReult.EndTime;
                analysisResult2.Tag = Channel.Default.AnalysisReult.Tag;
                analysisResult2.BreathEvent.AveragePressureInTIB = analysisResult.BreathEvent.AveragePressureInTIB;                
                Channel.Default.AnalysisReult = analysisResult2;

                if (ConfigurationManager.AppSettings["PressureMuzzleFlowChannelShow"] =="1" && array[3].ToString().Contains("呼吸率"))
                {
                    if (Channel.Default.PressureMuzzleFlowChannel.Count() == 0)
                    {
                        AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "Please return to the homepage and re-enter before trying again！" : "请先返回首页重新进入后重试！", -1, null, null, false);
                        DataModel.LogInstance.Default.AddLog("请先返回首页重新进入后重试", pSystem.LogManagement.LogLevel.ERROR);
                        return;
                    }

                    xfChannel3 = new ChannelFiliter(100);
                    xfChannel3.breathChannelType = 2;
                    Channel.Default.AnalysisReult.BreathEvent.strPressureMuzzleFlow = "";
                    var PressureMuzzleFlowChannel = Channel.Default.PressureMuzzleFlowChannel.ToList();

                    for (int i = 0; i < Channel.Default.AnalysisReult.Epochs.Count; i++)
                    {
                        Channel.Default.AnalysisReult.BreathEvent.strPressureMuzzleFlow += string.Format("{0}/{1}\t\t", i + 1, Channel.Default.AnalysisReult.Epochs.Count);
                        var relist = new List<float>();
                        foreach (var item in PressureMuzzleFlowChannel.Take(100 * 30))
                        {
                            float value = xfChannel3.getBreathChannelState((int)item) ? xfChannel3.getHeartRate((int)item) : 0;
                            relist.Add(value);
                        }
                        relist = relist.Distinct().ToList();
                        Channel.Default.AnalysisReult.BreathEvent.strPressureMuzzleFlow += string.Join("\t", relist);
                        Channel.Default.AnalysisReult.BreathEvent.strPressureMuzzleFlow += "\n";
                        PressureMuzzleFlowChannel.RemoveRange(0, 100 * 30);
                    }
                }

                if (ConfigurationManager.AppSettings["PressureMuzzleFlowDataAnalysis"] == "1")
                {
                    #region 鼻压力数据分析
                    var data = Channel.Default.PressureMuzzleFlowChannel;
                    int mbCount = 0;
                    List<int> mbList = new List<int>();
                    bool IsMouthBreath = false;
                    List<float> BreathValueList = new List<float>();

                    xfChannel3 = new ChannelFiliter(100);
                    xfChannel3.breathChannelType = 2;

                    var avg = data.Average();
                    for (int i = 0; i < data.Count - 1; i++)
                    {
                        if (data[i] >= avg - 2 * Channel.Default.AnalysisReult.BreathEvent.MouthBreathValue && data[i] <= avg + 2 * Channel.Default.AnalysisReult.BreathEvent.MouthBreathValue)
                        {
                            mbList.Add(i + 1);
                        }
                        BreathValueList.Add(xfChannel3.getBreathChannelState((int)data[i]) ? xfChannel3.getHeartRate((int)data[i]) : 0);
                    }
                    var GroupConsecutiveList = GroupConsecutive(mbList).ToList();
                    //var alist = new List<List<int>>();
                    for (int i = 0; i < GroupConsecutiveList.Count() - 1; i++)
                    {                       
                        if (GroupConsecutiveList[i].Count >= Channel.Default.AnalysisReult.BreathEvent.UnMouthBreathValue)
                        {
                            //alist.Add(GroupConsecutiveList[i]);
                            mbCount += GroupConsecutiveList[i].Count;
                            IsMouthBreath = true;
                            continue;
                        }

                        if (IsMouthBreath)
                        {
                            if (GroupConsecutiveList[i][0] - GroupConsecutiveList[i - 1][GroupConsecutiveList[i-1].Count - 1] < Channel.Default.AnalysisReult.BreathEvent.UnMouthBreathValue)
                            {
                                mbCount += GroupConsecutiveList[i].Count + (GroupConsecutiveList[i][0] - GroupConsecutiveList[i - 1][GroupConsecutiveList[i - 1].Count - 1]) - 1;
                            }
                            else
                            {
                                IsMouthBreath = false;
                            }
                        }
                    }



                    #region 测试使用
                    /*var blist = new List<int>();
                    foreach (var item in alist)
                    {
                        blist.Add(item.FirstOrDefault());
                        blist.Add(item.LastOrDefault());
                    }
                    var count = 0;
                    Channel.Default.AnalysisReult.BreathEvent.strPressureMuzzleFlow = "";
                    var PressureMuzzleFlowChannel = Channel.Default.PressureMuzzleFlowChannel.ToList();

                    for (int i = 0; i < Channel.Default.AnalysisReult.Epochs.Count; i++)
                    {
                        Channel.Default.AnalysisReult.BreathEvent.strPressureMuzzleFlow += string.Format("{0}/{1}\t\t\n", i + 1, Channel.Default.AnalysisReult.Epochs.Count);
                        for (int j = 1; j <= 30; j++)
                        {                            
                            Channel.Default.AnalysisReult.BreathEvent.strPressureMuzzleFlow += string.Format("第{0}秒:\n",i * 30 + j);
                            var relist = PressureMuzzleFlowChannel.Take(100);
                            foreach (var item in relist)
                            {
                                count++;
                                if (blist.Contains(count))
                                {
                                    Channel.Default.AnalysisReult.BreathEvent.strPressureMuzzleFlow += string.Format("\t#{0}", item);
                                }
                                else
                                {
                                    Channel.Default.AnalysisReult.BreathEvent.strPressureMuzzleFlow += string.Format("\t{0}", item);
                                }
                            
                            }                    
                            Channel.Default.AnalysisReult.BreathEvent.strPressureMuzzleFlow += "\n";
                            PressureMuzzleFlowChannel.RemoveRange(0, 100);
                        }                     
                    }*/ 
                    #endregion

                    Channel.Default.AnalysisReult.BreathEvent.AvgPressureMuzzleFlow = avg;
                    Channel.Default.AnalysisReult.BreathEvent.MouthBreathRatio = (float)((double)mbCount / data.Count());
                    Channel.Default.AnalysisReult.BreathEvent.MouthBreathTimes = (float)((double)mbCount / 100);
                    Channel.Default.AnalysisReult.BreathEvent.MouthBreathMinutes = (float)((double)Channel.Default.AnalysisReult.BreathEvent.MouthBreathTimes / 60);
                    Channel.Default.AnalysisReult.BreathEvent.PressureMuzzleFlowRespiratoryRate = BreathValueList.Where(x => x != 0).LastOrDefault();
                    #endregion
                }


                if (sender.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                sender.SetProgress(40, Program.Language=="EN"? "Extract Data..." : "提取数据...");
                num2 = 40;
            }
            sender.SetProgress(2 + num2, Program.Language == "EN" ? "Generate Report Basic Information..." : "生成报告基础信息...");
            if (Channel.Default.Patient == null)
            {
                Channel.Default.Patient = new Doc_PatientInfo();
            }
            else
            {
                //预防 直接附加进来的记录这里会取值压缩文件中ResultPhoto的地址，应该取本机中的地址
                Channel.Default.Patient = DataModel.DataBaseHelper.Default.Select<Doc_PatientInfo>(new Doc_PatientInfo() { PatientNo = Channel.Default.Patient.PatientNo });
            }
            Channel.Default.Patient.DoctorName = Channel.Default.Doctor.Name;
            Channel.Default.Patient.CurrentDateTime = DateTime.Now;
            Channel.Default.Patient.RecordTime = Channel.Default.StartTime;
            Assembly assembly = Assembly.Load("AwareTec.Polysmith.DataBaseCom");
            Type[] types = assembly.GetTypes();
            List<string> des = new List<string>();
            List<string> values = new List<string>();
            PropertyInfo[] properties = typeof(AnalysisResult).GetProperties();
            sender.SetProgress(10 + num2, Program.Language == "EN" ? "Generate Report Data Information..." : "生成报告数据信息...");
            for (int i = 0; i < types.Length; i++)
            {
                Type instance = types[i];
                object obj = null;
                if (instance == typeof(Doc_PatientInfo))
                {
                    obj = Channel.Default.Patient;
                }
                else if (instance == typeof(Doc_SleepResult))
                {
                    Channel.Default.AnalysisReult.Sleep.StartRecordTime = Channel.Default.StartTime;
                    Channel.Default.AnalysisReult.Sleep.EndRecordTime = Channel.Default.EndTime;
                    Channel.Default.AnalysisReult.Sleep.TotalRecordTime = (float)Math.Round((Channel.Default.EndTime - Channel.Default.StartTime).TotalMinutes, 2);
                    if (Channel.Default.AnalysisReult.Sleep.TotalRecordTime < Channel.Default.AnalysisReult.Sleep.TimeInBed)
                        Channel.Default.AnalysisReult.Sleep.TotalRecordTime = Channel.Default.AnalysisReult.Sleep.TimeInBed;
                    obj = Channel.Default.AnalysisReult.Sleep;
                }
                else if (!instance.IsAbstract && !instance.ContainsGenericParameters && instance.FullName.Contains("Doc_"))
                {
                    try
                    {
                        PropertyInfo propertyInfo = properties.First((PropertyInfo t) => t.PropertyType.FullName == instance.FullName);
                        if (propertyInfo != null)
                        {
                            obj = propertyInfo.GetValue(Channel.Default.AnalysisReult);
                        }
                    }
                    catch (Exception ee)
                    {
                        Console.WriteLine(ee.Message);
                    }
                }
                if (sender.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                if (obj != null)
                {
                    PropertyInfo[] properties2 = instance.GetProperties();
                    for (int j = 0; j < properties2.Length; j++)
                    {
                        des.Add(properties2[j].Name);
                        object value = properties2[j].GetValue(obj, null);
                        values.Add((value != null) ? value.ToString() : "");
                    }
                }
            }
            sender.SetProgress(20 + num2, Program.Language=="EN"? "Generate Report Graphic Information..." : "生成报告图样信息...");
            System.Drawing.Image image = Channel.Default.CreatMap(null, Channel.Default.IsBreathOnly);
            image.Save(Channel.Default.Patient.ResultPhoto);
            sender.SetProgress(70, Program.Language == "EN" ? "Saving Report File..." : "正在保存报告文件...");
            bool flag3 = false;
            int per = 70;
            string templatePath = array[3].ToString();
            if (num > 3)
            {
                try
                {
                    IWorkbook workbook = ExcelHelper.SetValues(templatePath, des, values);
                    per = 75;
                    ExcelHelper.SaveExcel(rpath, workbook);
                    per = 85;
                    if (Channel.Default.SystemSetting.AutoRunReport)
                    {
                        per = 90;
                        System.Diagnostics.Process.Start(rpath);
                        System.Threading.Thread.Sleep(500);
                    }
                    per = 100;
                }
                catch (Exception ex)
                {
                    sender.SetError(Program.Language == "EN" ? "Import Excel failed, please check the log" : "导入Excel失败，请查看日志");
                    DataModel.LogInstance.Default.AddLog("[导入Excel失败]:" + ex.Message, pSystem.LogManagement.LogLevel.ERROR);
                    return;
                }

                //Task.Factory.StartNew(delegate ()
                //{
                //    try
                //    {
                //        ExcelTemplate excelTemplate = new ExcelTemplate();
                //        excelTemplate.Open(templatePath);
                //        per = 72;
                //        excelTemplate.SetValues(des, values);
                //        per = 78;
                //        excelTemplate.SaveAs(rpath, null);
                //        per = 80;
                //        excelTemplate.Close();
                //        if (Channel.Default.SystemSetting.AutoRunReport)
                //        {
                //            per = 90;
                //            System.Diagnostics.Process.Start(rpath);
                //            System.Threading.Thread.Sleep(500);
                //        }
                //        per = 100;
                //    }
                //    catch (Exception ee)
                //    {
                //        sender.SetError("未安装Office 2007！");
                //        return;
                //    }
                //});
            }
            else
            {
                ReportHelper reportHelper = new ReportHelper();
                reportHelper.CreateNewWordDocument(templatePath);
                ReportHelper.DocumentFormat sformat = ReportHelper.DocumentFormat.Pdf;
                switch (num)
                {
                    case 1:
                        sformat = ReportHelper.DocumentFormat.Xps;
                        break;
                    case 2:
                        sformat = ReportHelper.DocumentFormat.Pdf;
                        break;
                    case 3:
                        sformat = ReportHelper.DocumentFormat.Doc;
                        break;
                }
                reportHelper.SaveAs(des, values, rpath, sformat, delegate (bool t)
                {
                    if (t)
                    {
                        per = 80;
                        if (Channel.Default.SystemSetting.AutoRunReport)
                        {
                            per = 90;
                            System.Diagnostics.Process.Start(rpath);
                            System.Threading.Thread.Sleep(500);
                        }
                        per = 100;
                        return;
                    }
                    per = -1;
                });
            }
            while (!flag3)
            {
                if (per == 80)
                {
                    sender.SetProgress(80,Program.Language=="EN"? "Report Saved Successfully！" : "报告保存成功！");
                }
                else if (per == 90)
                {
                    sender.SetProgress(90, Program.Language == "EN" ? "Opening Report..." : "正在打开报告...");
                }
                else
                {
                    if (per == 100)
                    {
                        sender.SetProgress(100, Program.Language == "EN" ? "Finish" : "完成");
                        return;
                    }
                    if (per == -1)
                    {
                        sender.SetProgress(100, "");
                        sender.SetError(Program.Language == "EN" ? "Save failed!" : "保存失败！");
                        DataModel.LogInstance.Default.AddLog("保存失败！", pSystem.LogManagement.LogLevel.ERROR);
                        return;
                    }
                    sender.SetProgress(per);
                }
                System.Threading.Thread.Sleep(50);
            }
        }

        public IEnumerable<List<int>> GroupConsecutive(List<int> list)
        {
            var group = new List<int>();
            foreach (var i in list)
            {
                if (group.Count == 0 || i - group[group.Count - 1] <= 1)
                    group.Add(i);
                else
                {
                    yield return group;
                    group = new List<int> { i };
                }
            }
            yield return group;
        }

        /// <summary>
        /// 更新睡眠分期
        /// </summary>
        /// <param name="state"></param>
        private void hdv_SleepStateChangedHandle(string[] state, int frameNo)
        {
            m_CurrentFrameNo = frameNo;
            if (string.Join("/", state) != string.Join("/", m_SleepState))
            {
                lock (m_lockSleep)
                {
                    m_SleepState = state;
                }
                m_SleepStateChange = true;
            }
        }

        /// <summary>
        /// 保存病例信息
        /// </summary>
        /// <param name="info"></param>
        internal bool patient_SaveInfoHandle(DataBaseCom.Doc_PatientInfo info)
        {
            #region 新增
            //同一医院底下，无论是儿童版还是成人版，病例号都不能相同
            if (!DataModel.DataBaseHelper.Default.Exsit(new DataBaseCom.Doc_PatientInfo() { PatientNo = info.PatientNo }))
            {
                //if (ShowMessage("档案中不存在该病例信息，是否需要创建？"))
                {
                    DataModel.DataBaseHelper.Default.Insert(info);
                    DataBaseCom.Doc_MainViewRecord mvr = new DataBaseCom.Doc_MainViewRecord()
                    {
                        PatientID = info.PatientNo,
                        Progress = (int)DataModel.ProgressState.None,
                        CreatTime = DateTime.Now,
                        VideoHave = false,
                        Reserve3 = "",
                        DifferentVersion = 1,
                        ModeType = Channel.Default.SystemSetting.ModeType
                    };
                    if (!DataModel.DataBaseHelper.Default.Exsit(mvr))
                    {
                        mvr.RecordTime = DateTime.Now;
                        mvr.CreatTime = DateTime.Now;//创建新病例 病例号不存在
                        mvr.VideoHave = false;//默认为false
                        mvr.DoctorID = "1";
                        mvr.DifferentVersion = 1;
                        mvr.Reserve3 = "";
                        mvr.LoginID = Channel.Default.Loginer.ID;
                        DataModel.DataBaseHelper.Default.Insert(mvr);
                    }
                    this.Invoke(new MethodInvoker(() =>
                    {
                        Control block = getControl();
                        if (block != null)
                        {
                            if (block is FunctionControls.AnalysisRecordView)
                            {
                                (block as FunctionControls.AnalysisRecordView).RefreshData();
                            }
                        }
                    }));
                    return true;
                }
            }
            #endregion

            #region 更新
            else
            {
                if (DataModel.DataBaseHelper.Default.Update(new DataBaseCom.Doc_PatientInfo() { PatientNo = info.PatientNo }, info))
                {
                    AhDung.MessageTip.ShowOk(Program.Language == "EN" ? "Case information modified successfully!" : "病例信息修改成功!");
                    this.Invoke(new MethodInvoker(() =>
                    {
                        Control block = getControl();
                        if (block != null)
                        {
                            if (block is FunctionControls.AnalysisRecordView)
                            {
                                (block as FunctionControls.AnalysisRecordView).ChangePatient(info);
                            }
                        }
                    }));
                    return true;
                }
            }
            #endregion
            return false;
        }

        /// <summary>
        /// 查看视频按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ribbonButton2_Click(object sender, EventArgs e)
        {
            Control block = getControl();
            if (block != null)
            {
                if (block is FunctionControls.HistoryDataView)
                {
                    string _path = (block as FunctionControls.HistoryDataView).VedioViewPath;
                    if (_path != "")
                    {
                        if (!File.Exists(_path))
                        {
                            DataModel.LogInstance.Default.AddLog(string.Format("用户点击视频按钮，当前edf文件无匹配视频文件 查找的路径为{0}", _path), pSystem.LogManagement.LogLevel.ERROR);
                            AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "No matching video file!" : "无匹配视频文件！");
                            return;
                        }
                    }
                    else
                    {
                        AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "No video files available!" : "无视频文件！");
                        return;
                    }
                }
                else
                    return;
            }
            else
                return;
            bool on = true;
            if (Channel.Default.VedioEnable)
            {
                ribbonButton2.SmallImage = Properties.Resources.Videono;
                on = false;
                Channel.Default.VedioEnable = false;
                DataModel.LogInstance.Default.AddLog("用户点击视频按钮 视频关闭");
                ribbonButton2.ToolTip = Program.Language == "EN" ? "View Video":"查看视频";
            }
            else
            {
                ribbonButton2.SmallImage = Properties.Resources.Videoyes;
                on = true;
                Channel.Default.VedioEnable = true;
                DataModel.LogInstance.Default.AddLog("用户点击视频按钮 视频开启");
                ribbonButton2.ToolTip = Program.Language == "EN" ? "Close Video":"关闭视频";
            }
            {
                (block as FunctionControls.HistoryDataView).PlayVedioRecord(on);
            }
        }

        /// <summary>
        /// 点击系统设置按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ribbonButton13_Click(object sender, EventArgs e)
        {
            DataModel.LogInstance.Default.AddLog("点击 系统-设置 按钮");
            SystemParamDialog newSystemParamDialog = new SystemParamDialog();
            newSystemParamDialog.Owner = this.ParentForm;
            newSystemParamDialog.StartPosition = FormStartPosition.CenterParent;
            newSystemParamDialog.ShowDialog();
        }

        /// <summary>
        /// 注销快捷菜单点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataModel.LogInstance.Default.AddLog("点击 注销 按钮");
            if (MessageForm.Show(Program.Language == "EN" ? "Do you want to log out of the current user" : "是否注销当前用户？", Program.Language == "EN" ? "CONFIRM" : "信息确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                DataModel.LogInstance.Default.AddLog("用户取消 注销", pSystem.LogManagement.LogLevel.INFO);
                return;
            }
            DataModel.LogInstance.Default.AddLog("用户确定 注销", pSystem.LogManagement.LogLevel.INFO);
            Program.IsLogin = true;
            this.Close();
        }

        /// <summary>
        /// 点击 退出 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string m_strAlert = Program.Language == "EN" ? "Message" : "信息提示";
        public string m_strResetAlert = Program.Language == "EN"? "Do you need to restore to local backup files" : "是否需要恢复成本地的备份文件?";
        private object ribbonButton9;

        /// <summary>
        /// 主菜单上所有按钮的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ribbonButton6_Click(object sender, EventArgs e)
        {
            EDF.Default.Interrupt = false;
            RibbonButton ribbonButton = (RibbonButton)sender;
            switch (ribbonButton.Name)
            {
                case "AutoScore":
                    if (Channel.Default.IsAutoMark)
                    {
                        Channel.Default.IsAutoMark = false;
                        AutoScore.SmallImage = Properties.Resources.AutoScore2;
                        AutoScore.ToolTip = Program.Language == "EN" ? "Automatic Score: Off" : "自动事件判断：关";
                        DataModel.LogInstance.Default.AddLog("用户 点击自动事件判断按钮 点击之后当前 自动事件判断：关");
                    }
                    else
                    {
                        Channel.Default.IsAutoMark = true;
                        AutoScore.SmallImage = Properties.Resources.AutoScore1;
                        AutoScore.ToolTip = Program.Language == "EN" ? "Automatic Score: On" : "自动事件判断：开";
                        DataModel.LogInstance.Default.AddLog("用户 点击自动事件判断按钮 点击之后当前 自动事件判断：开");
                    }
                    break;
                case "EditEDF":
                    Control control2 = this.getControl();
                    if (control2 == null || !(control2 is HistoryDataView))
                        break;
                    HistoryDataView historyDataView2 = control2 as HistoryDataView;
                    DataModel.LogInstance.Default.AddLog("用户 点击 剪切EDF文件 按钮");
                    ReduceToOnlyOneEvent();
                    UI.Block.EditEDF editEDF = new EditEDF(historyDataView2);
                    editEDF.OnOffTimesEventHandler -= EditEDF_OnOffTimesEventHandler;
                    editEDF.OnOffTimesEventHandler += EditEDF_OnOffTimesEventHandler;
                    editEDF.ShowDialog();
                    break;
                case "RecoverSaveFile":
                    if (Channel.Default.CanRecover)
                    {
                        if (MessageForm.Show(m_strResetAlert, m_strAlert, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Control control3 = this.getControl();
                            if (control3 == null || !(control3 is HistoryDataView))
                                break;
                            HistoryDataView historyDataView3 = control3 as HistoryDataView;
                            DataModel.LogInstance.Default.AddLog("用户 点击 恢复本地保存文件 按钮");
                            if (historyDataView3.dataManager.dataFactory != null)
                            {
                                historyDataView3.dataManager.dataFactory.EventsInstance.Dispose();
                                historyDataView3.dataManager.dataFactory.EpochsInstance.Dispose();
                            }
                            if (!historyDataView3.EpochsEventsRecover())
                            {
                                AhDung.MessageTip.ShowError(Program.Language == "EN" ? "Failed to restore locally saved files, please try again" : "恢复本地保存文件失败，请重试");
                            }
                            else
                            {
                                AnalysisResult ret = historyDataView3.dataManager.ReadResult();
                                ret.Tag = Channel.Default.AnalysisReult.Tag;
                                ret.GUID = historyDataView3.GUID;
                                Channel.Default.AnalysisReult = ret;
                                historyDataView3.LoadAnalysisData(Channel.Default.AnalysisReult, false, true);
                                AhDung.MessageTip.ShowOk(Program.Language == "EN" ? "Successfully restored locally saved files" : "恢复本地保存文件成功");
                            }
                            Channel.Default.CanRecover = false;
                        }                       
                    }
                    break;                  
                case "ribbonButton2":
                case "ribbonButton1":
                    DataModel.LogInstance.Default.AddLog("点击 系统-新病例 按钮");
                    PatientEdit patientEdit = new PatientEdit();
                    patientEdit.StartPosition = FormStartPosition.CenterParent;
                    patientEdit.SaveInfoHandle += new PatientEdit.SaveInfoDelegate(this.patient_SaveInfoHandle);
                    int num1 = (int)patientEdit.ShowDialog();
                    break;
                case "ribbonButton6":
                    DataModel.LogInstance.Default.AddLog("点击 系统-开始分析 按钮");
                    //
                    //if (ribbonButton.Text == "开始分析")
                    if (ribbonButton.Text ==  ML.GetText("MAINFORM_START_ANALYZING", "开始分析"))
                    {
                        if (Channel.Default.ProgressStauts == ProgressState.Monitoring && MessageForm.Show(Program.Language == "EN" ? "The current operation will cause the ongoing task to terminate. Should it be executed?" : "当前操作会导致正在进行的任务终止，是否执行？", Program.Language == "EN" ? "CONFIRM" : "信息确认", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                            break;
                        if (Channel.Default.ProgressStauts > ProgressState.MonitorFinish)
                        {
                            Control control = this.getControl();
                            if (control == null || !(control is HistoryDataView))
                                break;
                            HistoryDataView historyDataView = control as HistoryDataView;
                            if (!historyDataView.AnalysisEnable)
                            {
                                AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "Unable to analyze: The data does not meet the conditions for automatic analysis (collection time>10 minutes)" : "无法分析：数据未满足自动分析的条件（采集时间>10分钟）！", -1, new bool?(), new System.Drawing.Point?(), false);
                                DataModel.LogInstance.Default.AddLog("无法分析：数据未满足自动分析的条件（采集时间>10分钟）！", pSystem.LogManagement.LogLevel.ERROR);
                                break;
                            }
                            //在用户确定开始分析之前，另存一份保存本地
                            if (!historyDataView.EpochsEventsSaveAs())
                            {
                                AhDung.MessageTip.ShowError(Program.Language == "EN" ? "Backup of current file failed" : "备份当前文件失败");
                            }
                            else
                            {
                                Channel.Default.CanRecover = true;
                                //AhDung.MessageTip.ShowOk("备份当前文件成功");
                            }
                            if (this.ShowMessageYNC() == DialogResult.Cancel)
                                break;
                            this.ribbonButton6.Text = ML.GetText("MAINFORM_STOP_ANALYZING", "停止分析");
                            ProgressTipForm.Defalut.Text = Program.Language=="EN"? "Data Analysis Progress" : "数据分析进度";
                            ProgressTipForm.Defalut.Argument = (object)historyDataView;
                            ProgressTipForm.Defalut.DoWork += new ProgressTipForm.DoWorkEventHandler(this.Analysis_DoWork);
                            ProgressTipForm.Defalut.CanceledHandle += Defalut_CanceledHandle;
                            int num2 = (int)ProgressTipForm.Defalut.ShowDialog();
                            //this.ribbonButton6.Text = "开始分析";
                            ribbonButton6.Text = ML.GetText("MAINFORM_START_ANALYZING", "开始分析");
                            break;
                        }
                        ImportHistroyDialog importHistroyDialog = new ImportHistroyDialog();
                        if (Channel.Default.Patient != null)
                        {
                            importHistroyDialog.ID = Channel.Default.Patient.PatientNo;
                            importHistroyDialog.RecordTime = Channel.Default.StartTime;
                        }
                        if (importHistroyDialog.ShowDialog() != DialogResult.OK)
                            break;
                        Channel.Default.StartHistroyAnalysisByEDF(importHistroyDialog.EdfPath, false, "");
                        break;
                    }
                    if (MessageForm.Show(Program.Language == "EN" ? "Do you want to stop the sleep analysis process?" : "是否要停止睡眠分析过程？", Program.Language == "EN" ? "CONFIRM" : "信息确认", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                        break;
                    if (this.m_InterruptHandle != null)
                        this.m_InterruptHandle(true);
                    EDF.Default.Interrupt = true;
                    ribbonButton.Text = ML.GetText("MAINFORM_START_ANALYZING", "开始分析");
                    break;
                case "ribbonButton7":
                    //if (!Channel.Default.AnalysisReult.HasDataChange)
                    {
                        if (MessageForm.Show(Program.Language=="EN"? "Do you want to save all rating results?":"是否保存所有评分结果？", Program.Language == "EN" ? "Information Confirmation" : "信息确认", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                            break;
                        DataModel.LogInstance.Default.AddLog("点击 系统-保存分析 按钮");
                        this.BeginInvoke(new MethodInvoker(() =>
                        {
                            HistoryDataView historyDataView = getControl() as HistoryDataView;
                            AnalysisResult analysisResult = historyDataView.getAnalysisResult();
                            if (!this.m_TargetInstance.Init(analysisResult.GUID, analysisResult.EdfPath, analysisResult.StartTime, analysisResult.LightOffTime, analysisResult.LightOnTime, Channel.Default.IsBreathOnly))
                            {
                                AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "The time range for analyzing the on/off light is invalid. Please recalibrate and try again!" : "尝试分析的开关灯时间范围失效，请重新标定后重试！", -1, null, null, false);
                                DataModel.LogInstance.Default.AddLog("尝试分析的开关灯时间范围失效，请重新标定后重试！", pSystem.LogManagement.LogLevel.ERROR);
                                return;
                            }
                            if (!this.m_TargetInstance.SubmitAnalysisConditions(analysisResult.Epochs, analysisResult.EventRecords, false))
                            {
                                AhDung.MessageTip.ShowOk(Program.Language == "EN" ? "Save failed!" : "保存失败!", -1, new bool?(), new Point(this.Location.X + this.Width / 2, this.Location.Y + this.Height / 2));
                                DataModel.LogInstance.Default.AddLog("保存失败! 提交分析条件失败", pSystem.LogManagement.LogLevel.ERROR);
                                return;
                            }
                            AnalysisResult analysisResult2 = AnalysisResult.Convert(this.m_TargetInstance.getResult(Channel.Default.SystemSetting));
                            analysisResult2.StartTime = Channel.Default.AnalysisReult.StartTime;
                            analysisResult2.EndTime = Channel.Default.AnalysisReult.EndTime;
                            analysisResult2.Tag = Channel.Default.AnalysisReult.Tag;
                            Channel.Default.AnalysisReult = analysisResult2;
                            historyDataView.dataManager.SaveDataResult(analysisResult2);
                            Channel.Default.CreatMap((ResultDomain)null, Channel.Default.IsBreathOnly).Save(Channel.Default.Patient.ResultPhoto);
                            //更改记录状态是否成功
                            if (!DataBaseHelper.Default.SaveCompeletResultEx(analysisResult.GUID, historyDataView.dataManager.dataFactory.BasePath))
                            {
                                AhDung.MessageTip.ShowOk(Program.Language == "EN" ? "Save failed!" : "保存失败!", -1, new bool?(), new Point(this.Location.X + this.Width / 2, this.Location.Y + this.Height / 2));
                                DataModel.LogInstance.Default.AddLog(string.Format("保存失败! 失败原因为更改记录状态成功 guid为 {0} 事件地址为{1}", analysisResult.GUID, historyDataView.dataManager.dataFactory.BasePath), pSystem.LogManagement.LogLevel.ERROR);
                                return;
                            }
                            AhDung.MessageTip.ShowOk(Program.Language == "EN" ? "Successfully saved!" : "保存成功!", -1, new bool?(), new Point(this.Location.X + this.Width / 2, this.Location.Y + this.Height / 2));
                            DataModel.LogInstance.Default.AddLog("保存成功!", pSystem.LogManagement.LogLevel.WARN);
                            Channel.Default.ProgressStauts = ProgressState.Compelet;
                            Channel.Default.ScoreLock = true;
                            Channel.Default.CanRecover = false;
                            historyDataView.ChartArea.MarkEnable = !Channel.Default.ScoreLock;
                            Channel.Default.UpdateRecord(Convert.ToInt32(Channel.Default.AnalysisReult.Tag));
                            this.FreshHomeMenu();
                        }));
                        if (!Channel.Default.SleepLock)
                        {
                            Channel.Default.SleepLock = true;
                            LockButton.SmallImage = Properties.Resources.Lockyes;
                            LockButton.ToolTip = Program.Language == "EN" ? "UnLock " : "解锁";
                        }
                        break;
                    }
                    break;
                case "ribbonButton8":
                    if (MessageForm.Show(Program.Language == "EN" ? "Do you want to log out of the current user?" : "是否注销当前用户？", Program.Language == "EN" ? "CONFIRM" : "信息确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        break;
                    Program.IsLogin = true;
                    this.Close();
                    break;
                case "ribbonButton9":
                    this.Close();
                    break;
                case "ribbonButton10":
                    DataModel.LogInstance.Default.AddLog("点击 设置-定义事件 按钮");
                    int num3 = (int)new MarkerManagement().ShowDialog();
                    break;
                case "ribbonButton11":
                    DataModel.LogInstance.Default.AddLog("点击 设置-通道管理 按钮");
                    var montage = new MontageCopy();
                    montage.Owner = this.ParentForm;
                    montage.StartPosition = FormStartPosition.CenterParent;
                    montage.FilterColumnLoadHandle += Channel.Default.Channel_FilterLoad;
                    montage.Init(Channel.Default.CurrentChannelTable.Rows.Count != 0 ? Channel.Default.CurrentChannelPath : Channel.Default.DefaultChannelPath);
                    montage.FilterDropDownHandle += Channel.Default.channel_FilterDropDownHandle;
                    int num4 = (int)montage.ShowDialog();
                    break;
                case "ribbonButton12":
                    DataModel.LogInstance.Default.AddLog("点击 系统-多趋势图 按钮");
                    Moudle moudle = new Moudle();
                    moudle.ControlBox = true;
                    moudle.MaximizeBox = true;
                    moudle.CanResize = true;
                    moudle.Text = Program.Language=="EN"? "Multi Trend Chart Overlay" : "多谱图叠加";
                    moudle.Size = new System.Drawing.Size(900, 1000);
                    moudle.KeyPreview = true;
                    MulReportChart mulReportChart = new MulReportChart();
                    mulReportChart.CurrentFrameChangedHandler += new MulReportChart.CurrentFrameChangedDelegate(this.pan_CurrentFrameChangedHandler);
                    mulReportChart.Dock = DockStyle.Fill;
                    mulReportChart.LoadData(ResultDomain.Default, Channel.Default.IsBreathOnly);
                    mulReportChart.CurrentFrameNo = this.m_CurrentFrameNo;
                    moudle.Controls.Add((Control)mulReportChart);
                    int num66 = (int)moudle.ShowDialog();
                    break;
                case "ribbonButton5":
                    DataModel.LogInstance.Default.AddLog("点击 设置-评分员 按钮");
                    if (new DoctorEdit().ShowDialog() != DialogResult.OK)
                        break;
                    Control control1 = this.getControl();
                    if (control1 == null || !(control1 is AnalysisRecordView))
                        break;
                    (control1 as AnalysisRecordView).RefreshData();
                    break;
                case "ribbonButton3":
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (Program.Language=="EN")
                        openFileDialog.Filter = "Text Files|*.txt|Data Files|*.dat|All Files|*.*";
                    else
                        openFileDialog.Filter = "文本文件|*.txt|数据文件|*.dat|所有文件|*.*";
                    openFileDialog.RestoreDirectory = true;
                    openFileDialog.FilterIndex = 1;
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                        break;
                    break;
                case "ribbonButton4":
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    if (Program.Language == "EN")
                        saveFileDialog1.Filter = "Text Files|*.txt|C# Files|*.cs|All Files|*.*";
                    else
                        saveFileDialog1.Filter = "文本文件|*.txt|C#文件|*.cs|所有文件|*.*";
                    saveFileDialog1.RestoreDirectory = true;
                    saveFileDialog1.FilterIndex = 1;
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        break;
                    break;
                case "ribbonButton30":
                    DataModel.LogInstance.Default.AddLog("点击 帮助-操作手册 按钮");
                    if (File.Exists(Application.StartupPath + "\\help.pdf"))
                    {
                        //操作手册只能打开一个，重复打开会把之前打开的最大化
                        if (m_PdfViewDialog != null && m_PdfViewDialog.Created)
                        {
                            this.Invoke(new MethodInvoker(() =>
                            {
                                m_PdfViewDialog.Activate();
                                m_PdfViewDialog.WindowState = FormWindowState.Maximized;
                            }));
                            break;
                        }
                        else
                        {
                            m_PdfViewDialog = new PdfViewDialog();
                            m_PdfViewDialog.Show();
                            break;
                        }
                    }
                    if (File.Exists(Application.StartupPath + "\\help.chm"))
                    {
                        System.Diagnostics.Process.Start(Application.StartupPath + "\\help.chm");
                        break;
                    }
                    if (File.Exists(Application.StartupPath + "\\help.doc"))
                    {
                        System.Diagnostics.Process.Start(Application.StartupPath + "\\help.doc");
                        break;
                    }
                    if (File.Exists(Application.StartupPath + "\\help.docx"))
                    {
                        System.Diagnostics.Process.Start(Application.StartupPath + "\\help.docx");
                        break;
                    }
                    AhDung.MessageTip.ShowError(Program.Language == "EN"? "Operation manual not found" : "未找到操作手册说明书", -1, new bool?(), new System.Drawing.Point?(), false);
                    DataModel.LogInstance.Default.AddLog("未找到操作手册说明书", pSystem.LogManagement.LogLevel.ERROR);
                    break;
                case "ribbonButton31":
                    DataModel.LogInstance.Default.AddLog("点击 帮助-关于 按钮");
                    int num6 = (int)new About().ShowDialog();
                    break;
                case "ribbonButton32":
                    DataModel.LogInstance.Default.AddLog("点击 系统-睡眠分析报告 按钮");
                    OutReportSelection outReportSelection = new OutReportSelection();
                    if (outReportSelection.ShowDialog() != DialogResult.OK)
                        break;
                    string savePath = outReportSelection.SavePath;
                    string str1 = string.Format("{1}({2}){4}报告{0}--{3}", (object)Channel.Default.StartTime.ToString("yyyyMMddHHmmss"), (object)Channel.Default.Patient.PatientName, (object)Channel.Default.Patient.PatientNo, (object)Channel.Default.Doctor.Name, Channel.Default.IsBreathOnly ? "初筛" : "睡眠分析");
                    string str2 = outReportSelection.ReportType.ToLower();
                    int num7 = str2 == "xps" ? 1 : (str2 == "pdf" ? 2 : (str2 == "excel" ? 4 : 3));
                    if (!Directory.Exists(savePath))
                        Directory.CreateDirectory(savePath);
                    string str3;
                    if (savePath == "")
                    {
                        SaveFileDialog saveFileDialog2 = new SaveFileDialog();
                        saveFileDialog2.Filter = "Xps|*.xps|Pdf|*.pdf|Word|*.doc";
                        saveFileDialog2.RestoreDirectory = true;
                        saveFileDialog2.FilterIndex = num7;
                        saveFileDialog2.FileName = str1;
                        if (saveFileDialog2.ShowDialog() != DialogResult.OK)
                            break;
                        str3 = saveFileDialog2.FileName;
                        num7 = saveFileDialog2.FilterIndex;
                    }
                    else
                        str3 = string.Format("{0}\\{3:yyyy-MM-dd}\\{1}.{2}", (object)savePath, (object)str1, str2 == "word" ? (object)"doc" : (str2 == "excel" ? (object)"xlsx" : (object)str2), (object)Channel.Default.StartTime);
                    Channel.Default.IsBreathOnly = outReportSelection.IsBreathOnly;
                    HistoryDataView historyDataView1 = this.getControl() as HistoryDataView;
                    ProgressTipForm.Defalut.Text = Program.Language=="EN"? "Generate sleep analysis report" : "生成睡眠分析报告";
                    ProgressTipForm.Defalut.Argument = (object)new object[4] { str3, num7, historyDataView1, outReportSelection.ReportTempalteName };
                    ProgressTipForm.Defalut.DoWork += new ProgressTipForm.DoWorkEventHandler(this.ImportReport_DoWork);
                    ProgressTipForm.Defalut.CanceledHandle += Defalut_CanceledHandle;
                    int num8 = (int)ProgressTipForm.Defalut.ShowDialog();
                    DataModel.LogInstance.Default.AddLog("进度条窗口展示状态:" + (DialogResult)num8, pSystem.LogManagement.LogLevel.DEBUG);
                    DataModel.LogInstance.Default.AddLog(string.Format("生成睡眠分析报告成功！生成的报告名称为 {0}", StringPath.ConvertLogPath(str3)), pSystem.LogManagement.LogLevel.WARN);
                    break;
                case "ribbonButton49":
                    this.LoadHomePage();
                    DataModel.LogInstance.Default.AddLog("点击 系统-首页 按钮");
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="c"></param>
        /// <param name="action"></param>
        private void method<T>(T c, Action<T> action) where T : Control
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => action(c)));
            }
            else
                action(c);
        }

        /// <summary>
        /// 添加指定的模块
        /// </summary>
        /// <param name="uc"></param>
        private void AddControls(UserControl uc)
        {
            method(uc, t =>
            {
                bool find = false;
                t.Dock = DockStyle.Fill;
                List<Control> remove = new List<Control>();
                foreach (Control c in this.panel1.Controls)
                {
                    if (c.GetType() == t.GetType())
                    {
                        c.Visible = true;
                        find = true;
                        continue;
                    }
                    c.Visible = false;
                    if (c.GetType() != typeof(FunctionControls.AnalysisRecordView))
                    {
                        remove.Add(c);
                    }
                }
                if (!find)
                {
                    t.BringToFront();
                    panel1.Controls.Add(t);
                }
                int len = remove.Count;
                while (len > 0)
                {
                    if (remove[0] is FunctionControls.HistoryDataView)
                    {
                        Channel.Default.ScoreLock = false;
                        ribbonButtonFrameRun.SmallImage = Properties.Resources.Start1;
                        ControlTimer(false);
                        if (!Channel.Default.SleepLock)
                        {
                            LockButton.OnClick(null);
                        }
                        m_comm.DeleteDataSource();
                    }
                    remove[0].Dispose();
                    len--;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            });
        }

        /// <summary>
        /// 移除指定的模块
        /// </summary>
        /// <param name="uc"></param>
        private void RemoveControls(UserControl uc)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                foreach (Control c in this.panel1.Controls)
                {
                    if (c.GetType() == uc.GetType())
                    {
                        panel1.Controls.Remove(c);
                        c.Dispose();
                    }
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }));
        }

        /// <summary>
        /// 清空控件
        /// </summary>
        private void ClearControls()
        {
            this.Invoke(new MethodInvoker(() =>
            {
                while (panel1.Controls.Count > 0)
                {
                    panel1.Controls[0].Dispose();
                }
            }));
        }

        /// <summary>
        /// 获取控件
        /// </summary>
        /// <returns></returns>
        private Control getControl()
        {
            Control ret = null;
            this.Invoke(new MethodInvoker(() =>
            {
                foreach (Control c in this.panel1.Controls)
                {
                    if (c.Visible)
                    {
                        ret = c;
                    }
                }
            }));
            return ret;
        }

        #endregion

        #region 公有方法

        public void ControlTimer(bool on = true)
        {
            timer1.Enabled = on;
        }

        /// <summary>
        /// 打开加密的pdf文件
        /// </summary>
        /// <param name="filePath"></param>
        public void copyPDF(string filePath)
        {
            iTextSharp.text.pdf.RandomAccessFileOrArray ra = new iTextSharp.text.pdf.RandomAccessFileOrArray(filePath);
            if (ra != null)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                byte[] password = System.Text.ASCIIEncoding.ASCII.GetBytes("pps");
                iTextSharp.text.pdf.PdfReader thepdfReader = new iTextSharp.text.pdf.PdfReader(ra, password);
                iTextSharp.text.pdf.PdfReader.unethicalreading = true;
                int pages = thepdfReader.NumberOfPages;
                iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document();
                iTextSharp.text.pdf.PdfCopy pdfCopy = new iTextSharp.text.pdf.PdfCopy(pdfDoc, ms);
                pdfDoc.Open();
                int i = 0;
                while (i < pages)
                {
                    pdfCopy.AddPage(pdfCopy.GetImportedPage(thepdfReader, i + 1));
                    i += 1;
                }
                pdfDoc.Close();

            }
        }

        /// <summary>
        /// 改变时基显示状态
        /// </summary>
        /// <param name="sTimeSpan"></param>
        public void ChangeTimeSpan(int sTimeSpan)
        {
            foreach (RibbonItem c in ribbonItemGroup2.Items)
            {
                if (c is RibbonButton)
                {
                    string strValue = c.Text.ToUpper();
                    int times = 0;
                    if (strValue.Contains("S"))
                    {
                        times = int.Parse(strValue.Replace("S", ""));
                    }
                    else if (strValue.Contains("MIN"))
                    {
                        times = int.Parse(strValue.Replace("MIN", "")) * 60;
                    }
                    if (times == sTimeSpan)
                    {
                        if (bak_butt.Name != c.Name)
                        {
                            bak_butt.Checked = false;
                            c.Checked = true;
                            bak_butt = c as RibbonButton;
                        }
                        break;
                    }
                }
            }
        }

        #endregion

        #region 快捷键处理

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Control block = getControl();
            if (block != null)
            {
                if (block is FunctionControls.HistoryDataView)
                {
                    switch (keyData)
                    {
                        case Keys.Right:
                        case Keys.Left:
                        case Keys.PageUp:
                        case Keys.PageDown:
                            (block as FunctionControls.HistoryDataView).PageMove(keyData == Keys.Up ? 0 : keyData == Keys.Down ? 3 : keyData == Keys.Left || keyData == Keys.PageUp ? 1 : 2, true);
                            return true;
                    }
                }
                else if (block is FunctionControls.AnalysisRecordView)
                {
                    if ((block as FunctionControls.AnalysisRecordView).mainKeyDown(keyData))
                        return true;
                    else
                        return base.ProcessCmdKey(ref msg, keyData);
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            Console.WriteLine(string.Format("【{0}】触发睡眠分期按键事件", DateTime.Now));
            e.Handled = true;
            Control block = getControl();
            if (e.Shift && e.Control && e.KeyCode == Keys.F)
            {
                if (block != null)
                {
                    if (block is FunctionControls.HistoryDataView)
                    {
                        (block as FunctionControls.HistoryDataView).HistoryDataView_KeyDown(null);
                    }
                    else if (block is FunctionControls.RealDataView)
                    {
                        (block as FunctionControls.RealDataView).RealTimeDataView_KeyDown(null);
                    }
                }
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                if (block != null)
                {
                    if (block is FunctionControls.HistoryDataView)
                    {
                        (block as FunctionControls.HistoryDataView).HistoryDataView_KeyDown();
                    }
                    else if (block is FunctionControls.RealDataView)
                    {
                        (block as FunctionControls.RealDataView).RealTimeDataView_KeyDown();
                    }
                }
            }
            else if (e.Control && e.Alt && e.KeyCode == Keys.Z)
            {
                if (block != null)
                {
                    if (block is FunctionControls.HistoryDataView)
                    {
                        (block as FunctionControls.HistoryDataView).HistoryDataView_Return();
                    }
                }
            }
            else if (e.KeyCode == Keys.F5)
            {
                var olf = new OperationLogForm();
                olf.StartPosition = FormStartPosition.CenterParent;
                olf.ShowDialog();
            }
            else if (e.KeyCode == Keys.D1 || e.KeyCode == Keys.D2 || e.KeyCode == Keys.D3 || e.KeyCode == Keys.W || e.KeyCode == Keys.R || e.KeyCode == Keys.NumPad0
                || e.KeyCode == Keys.NumPad1 || e.KeyCode == Keys.NumPad2 || e.KeyCode == Keys.NumPad3 || e.KeyCode == Keys.NumPad5)
            {
                if (block != null)
                {
                    if (block is FunctionControls.HistoryDataView)
                    {
                        if (!Channel.Default.SleepLock && !Channel.Default.ScoreLock)
                        {

                            (block as FunctionControls.HistoryDataView).MarkSleep((e.KeyCode == Keys.W || e.KeyCode == Keys.NumPad0) ? 5 : (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1) ? 3 : (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2) ? 2 : (e.KeyCode == Keys.D3 || e.KeyCode == Keys.NumPad3) ? 1 : 4);
                        }
                        else
                        {
                            AhDung.MessageTip.ShowWarning(Channel.Default.SleepLock ? Program.Language=="EN"? "Please unlock the sleep staging enable lock first!" : "请先解开睡眠分期使能锁！" : Channel.Default.ScoreLock ? Program.Language == "EN" ? "The rating has ended. If you need to re rate, please unlock the rating status first" : "评分已结束，如需重新评分，请先解锁评分状态！" : Program.Language == "EN" ? "No permission" : "无权限");
                        }
                    }
                }
            }
            else if ((e.KeyCode == Keys.Left || e.KeyCode == Keys.Right) && e.Alt)
            {
                if (block != null)
                {
                    if (block is FunctionControls.HistoryDataView)
                    {
                        (block as FunctionControls.HistoryDataView).PageMove(e.KeyCode == Keys.Up ? 0 : e.KeyCode == Keys.Down ? 3 : e.KeyCode == Keys.Left ? 1 : 2, false);
                    }
                }
            }
            base.OnKeyDown(e);
        }

        #endregion

        #region 异步线程对话框置顶写法
        private bool ShowMessage(string msg)
        {
            object ss = this.Invoke(new MessageBoxShow(MessageBoxShow_F), new object[] { msg });
            return Convert.ToBoolean(ss);
        }
        delegate bool MessageBoxShow(string msg);
        bool MessageBoxShow_F(string msg)
        {
            return MessageForm.Show(msg, Program.Language == "EN" ? "Message" : "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes;
        }
        private DialogResult ShowMessageYNC(string msg)
        {
            object ss = this.Invoke(new MessageBoxShowYNC(MessageBoxShow_YNC), new object[] { msg });
            return (DialogResult)ss;
        }
        delegate DialogResult MessageBoxShowYNC(string msg);
        DialogResult MessageBoxShow_YNC(string msg)
        {
            return MessageForm.Show(msg, Program.Language == "EN" ? "Message" : "信息提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
        }
        private DialogResult ShowMessageYNC()
        {
            object ss = this.Invoke(new _MessageBoxShowYNC(MessageBoxShow_YNC));
            return (DialogResult)ss;
        }
        delegate DialogResult _MessageBoxShowYNC();
        DialogResult MessageBoxShow_YNC()
        {
            return new AnalysisSelection().ShowDialog();
        }
        delegate bool docopy(string strSource, string strTarget);
        private bool _docopy(string strSource, string strTarget)
        {
            return ApiCopyFile.DoCopy(strSource, strTarget, this.Handle);
        }
        private bool Docopy(string strSource, string strTarget)
        {
            object ss = this.Invoke(new docopy(_docopy), new object[] { strSource, strTarget });
            return Convert.ToBoolean(ss);
        }
        #endregion

        #region 进度
        /// <summary>
        /// 开始进度
        /// </summary>
        /// <param name="des"></param>
        private void ProcessRunning(int percent, string des)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                if (percent >= toolStripProgressBar1.Maximum)
                {
                    toolStripProgressBar1.Value = toolStripProgressBar1.Maximum;
                    System.Threading.Thread.Sleep(50);
                    this.toolStripProgressBar1.Visible = false;
                    toolStripProgressBar1.Value = toolStripProgressBar1.Minimum;
                    toolStripStatusLabel3.Text =Program.Language=="EN"?"Ready": "准备";
                }
                else
                {
                    toolStripProgressBar1.Value = percent;
                    this.toolStripProgressBar1.Visible = true;
                    toolStripStatusLabel3.Text = des;
                    DataModel.LogInstance.Default.AddLog(string.Format("双击进入回放：完成进度>>{0}%", percent), pSystem.LogManagement.LogLevel.INFO);
                }
            }));
        }
        #endregion

        #region 帧播放操作

        private void ribbonButton41_MouseLeave(object sender, MouseEventArgs e)
        {
            SingleNotchRibbonButton.CloseDropDown();
        }

        private void ribbonButton40_MouseLeave(object sender, MouseEventArgs e)
        {
            HighPassRibbonButton.CloseDropDown();
        }

        private void ribbonButton45_MouseLeave(object sender, MouseEventArgs e)
        {
            LowPassRibbonButton.CloseDropDown();
        }
        private void ribbonButton40_MouseDown(object sender, MouseEventArgs e)
        {
            RibbonButton rbt = (RibbonButton)sender;
            int typ = 9;
            switch (rbt.Name)
            {
                case "ribbonButton40":
                    typ = 0;
                    break;
                case "ribbonButton41":
                    typ = 1;
                    break;
                case "ribbonButton43":
                    typ = 2;
                    break;
                case "ribbonButton44":
                    typ = 3;
                    break;
            }
            Control block = getControl();
            if (block != null)
            {
                if (block is FunctionControls.HistoryDataView)
                {
                    (block as FunctionControls.HistoryDataView).Execute(typ, true);
                }
            }
        }
        private void ribbonButtonFrameRun_Click(object sender, EventArgs e)
        {
            int typ = 9;
            if (ribbonButtonFrameRun.Value == "4")
            {
                ribbonButtonFrameRun.SmallImage = Properties.Resources.Stop5;
                typ = 4;
                ribbonButtonFrameRun.Value = "5";
            }
            else
            {
                ribbonButtonFrameRun.SmallImage = Properties.Resources.Start1;
                typ = 5;
                ribbonButtonFrameRun.Value = "4";
            }
            Control block = getControl();
            if (block != null)
            {
                if (block is FunctionControls.HistoryDataView)
                {
                    (block as FunctionControls.HistoryDataView).Execute(typ);
                }
            }
        }
        #endregion


    }
}