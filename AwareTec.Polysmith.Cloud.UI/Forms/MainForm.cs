using AwareTec.Polysmith.Cloud.UI.DataModels;
using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.User;
using AwareTec.Polysmith.Cloud.UI.DataModels.EnumModels;
using AwareTec.Polysmith.Cloud.UI.DataModels.Global;
using AwareTec.Polysmith.Cloud.UI.Forms.ConfigurationBlock;
using AwareTec.Polysmith.Cloud.UI.Forms.MainFormBlock;
using AwareTec.Polysmith.Cloud.UI.Forms.RealDataViewBlock;
using AwareTec.Polysmith.Cloud.UI.Forms.Tips;
using AwareTec.Polysmith.Cloud.UI.ViewHelpers;
using AwareTec.Polysmith.Cloud.UI.Views;
using AwareTec.Polysmith.DataBaseCom;
using AwareTec.Polysmith.DataCenter;
using AwareTec.Polysmith.Util;
using AwareTec.Polysmith.Util.EnumUtils;
using AwareTec.Polysmith.Util.ExcelUtils;
using AwareTec.Polysmith.Util.PathUtils;
using NPOI.SS.UserModel;
using RestfulWebRequest.ApiCall;
using RestfulWebRequest.EnumModels.EnumModels4Table;
using RestfulWebRequest.RestfulModel;
using RestfulWebRequest.RestfulTable.RestfulRequestTable;
using RestfulWebRequest.RestfulTable.RestfulResponseTable;
using RestfulWebRequest.RestfulTable.RestfulResponseTable.BuiltInType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.Cloud.UI.Forms
{
    /// <summary>
    /// 主窗体
    /// </summary>
    public partial class MainForm : CloudSkinForm
    {
        private bool _networkIsAvailable = true;
        public bool NetworkIsAvailable
        {
            set
            {
                if (_networkIsAvailable == value)
                    return;
                _networkIsAvailable = value;
                toolStripStatusLabel3.Text = _networkIsAvailable ? "准备" : "离线";
            }
        }

        private MainFormCloseMethod _mainFormCloseMethod = MainFormCloseMethod.Empty;
        public MainFormCloseMethod MainFormCloseMethod => _mainFormCloseMethod;

        #region 私有成员
        /// <summary>
        /// 算法通讯接口类
        /// </summary>
        private TargetCenter m_TargetInstance = null;
        private ICommunication m_comm = null;
        private Protocol.ProtocolServer realdataview_protocol;
        private bool addrealdataview = false;
        private bool reconnection = false;
        private bool hasvideo = false;
        private PdfViewDialog m_PdfViewDialog = null;
        private int m_CurrentFrameNo = 1;
        private string[] m_SleepState = new string[10];
        private object m_lockSleep = new object();
        private bool m_SleepStateChange = false;
        private RibbonButton bak_butt;
        #endregion
        public MainForm()
        {
            InitializeComponent();
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            base.UpdateStyles();
            this.UsernameLabel.Text = GlobalSingleton.Instance.User.Username;
            ModeTypeLabel.Text = EnumHelper.GetDescription(GlobalSingleton.Instance.User.ModeType);
            this.timer1.Interval = 100;
            this.ribbon1.Expanded = true;
            this.ribbon1.ThemeColor = RibbonTheme.Black;
            base.TopLevel = true;
            this.bak_butt = ribbonButton12;
            this.bak_butt.Checked = true;
            m_comm = new RemoteDataInteration();
            this.m_TargetInstance = new TargetCenter(m_comm);
            this.Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            m_comm.Init();
            List<string> cfgNames = ViewHelpers.ChannelManageCloud.Default.GetCfgNames();
            foreach (string text in cfgNames)
            {
                this.ribbonComboBox1.DropDownItems.Add(new RibbonLabel
                {
                    Text = text
                });
                //导联方案下拉框名字的显示
                if (ribbonComboBox1.DropDownItems.Count > 0)
                    ribbonComboBox1.SelectedItem = new RibbonLabel
                    {
                        Text = GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelPath.Split('\\').Last()
                    };
            }
            this.panel1.Paint += panel1_Paint;
            this.BatteryCapacity.Paint += this.BatteryCapacity_Paint;
            base.FormClosing += this.MainForm_FormClosing;
            base.FormClosed += this.MainForm_FormClosed;
            this.ribbonComboBox1.DropDownItemClicked += this.ribbonComboBox1_DropDownItemClicked;
            this.ribbonComboBox1.MouseLeave += this.ribbonComboBox1_MouseLeave;
            this.ribbonComboBox1.DropDownShowing += this.ribbonComboBox1_DropDownShowing;
            LoadHomePage();
        }


        #region 公有成员
        #region 属性
        /// <summary>
        /// 电池剩余容量
        /// </summary>
        public static float BatteryCapacityValue = 100;
        /// <summary>
        /// 电池剩余可监测时间
        /// </summary>
        public static int BatteryLastTime = 0;
        #endregion
        #endregion

        #region 内部方法

        #region 重绘相关
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
                string tips = string.Format("可监测的时间剩余{0}时{1}分", BatteryLastTime / 60, BatteryLastTime % 60);
                if (BatteryCapacity.ToolTipText != tips)
                    BatteryCapacity.ToolTipText = tips;
            }
        }

        /// <summary>
        /// 主菜单与内容分割线显示
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
        #endregion
        #region  主窗体资源释放时触发
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_TargetInstance.Dispose();
            if (_mainFormCloseMethod == MainFormCloseMethod.Empty)
                _mainFormCloseMethod = MainFormCloseMethod.Exit;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_mainFormCloseMethod==MainFormCloseMethod.SignOut)
            {
                Channel.Default.ScoreLock = false;
                return;
            }
            if (MessageForm.Show("是否确认退出系统？", "信息确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
            else
            {
                Channel.Default.ScoreLock = false;
            }
        }
        #endregion
        #region 快捷键处理
        private delegate bool MainFormKeyDownDelegate(KeyEventArgs e);
        private event MainFormKeyDownDelegate m_MainFormKeyDown;
        /// <summary>
        /// 主页面按键触发
        /// </summary>
        private event MainFormKeyDownDelegate MainFormKeyDown
        {
            add
            {
                if (m_MainFormKeyDown != null)
                {
                    m_MainFormKeyDown = null;
                }
                m_MainFormKeyDown += value;
            }
            remove
            {
                m_MainFormKeyDown = null;
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Control block = getControl();
            if (block != null)
            {
                if (block is HistoryDataView)
                {
                    switch (keyData)
                    {
                        case Keys.Right:
                        case Keys.Left:
                            (block as HistoryDataView).PageMove(keyData == Keys.Up ? 0 : keyData == Keys.Down ? 3 : keyData == Keys.Left ? 1 : 2, true);
                            return true;
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    //if (m_MainFormKeyDown != null)
        //    if (m_MainFormKeyDown != null && keyData != Keys.F5)
        //    {
        //        KeyEventArgs e = new KeyEventArgs(keyData);
        //        if (m_MainFormKeyDown.Invoke(e))
        //            return true;
        //    }
        //    return base.ProcessCmdKey(ref msg, keyData);
        //}
        protected override void OnKeyDown(KeyEventArgs e)
        {
            e.Handled = true;
            if (e.KeyCode == Keys.F5)
            {
                var olf = new OperationLogForm();
                olf.StartPosition = FormStartPosition.CenterParent;
                olf.ShowDialog();
            }
            else
            {
                if (m_MainFormKeyDown != null)
                {
                    m_MainFormKeyDown.Invoke(e);
                }
            }
            base.OnKeyDown(e);
        }

        #endregion
        #region  页面的加入、移除、获取
        #region 加载首页
        public delegate void RecordViewChangedDelegate();
        /// <summary>
        /// 需要更新首页表单时触发
        /// </summary>
        public event RecordViewChangedDelegate RecordViewChanged;
        private bool m_TaskRun = false;
        /// <summary>
        /// 加载首页
        /// </summary>
        /// <returns></returns>
        private HomePageOrderRecordView LoadHomePage()
        {
            if (bak_butt != null)
                bak_butt.Checked = false;
            bak_butt = ribbonButton12;
            bak_butt.Checked = true;
            ribbonPanel4.Visible = false;
            ReturnHomePageRibbonBtn.Visible = StartAnalysisRibbonBtn.Visible = SaveAnalysisRibbonBtn.Visible = false;
            MultiTrendChartRibbonBtn.Visible = ExportReportRibbonBtn.Visible = DownLoadReportRibbonBtn.Visible = UploadReportRibbonBtn.Visible = false;
            sumitAuditBtn.Visible = AuditBtn.Visible = false;
            HomePageOrderRecordView page = new HomePageOrderRecordView();
            page.Tag = true;
            page.StartMonitoringBeClicked += Page_StartMonitoringBeClicked;
            page.EnterPlayBeClicked += Page_EnterPlayBeClicked;
            if (AddControls(page))
            {
                RecordViewChanged += page.RefeshViewData;
            }
            return page;
        }
        /// <summary>
        /// 进入回放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_EnterPlayBeClicked(object sender, Views.ViewsEventArgs.SwitchPageEventArgs e)
        {
            if (m_TaskRun)
            {
                AhDung.MessageTip.ShowWarning("执行未结束，请勿重复操作！");
                return;
            }
            m_TaskRun = true;
            Channel.Default.Patient = GlobalSingleton.Instance.convetToPatient(e.OrderItem.patient);
            Channel.Default.Doctor = new Doc_Doctor() { Name = e.OrderItem.doctor };
            Task.Factory.StartNew(() =>
            {
                HistoryDataView view = LoadHistoryPage(e);
                m_TaskRun = false;
            });
        }
        /// <summary>
        /// 进入实时监测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_StartMonitoringBeClicked(object sender, Views.ViewsEventArgs.SwitchPageEventArgs e)
        {
            NewMonitor(e);
        }
        #endregion
        #region 加载回放界面
        /// <summary>
        /// 加载回放界面
        /// </summary>
        /// <returns></returns>
        private HistoryDataView LoadHistoryPage(Views.ViewsEventArgs.SwitchPageEventArgs e)
        {
            HistoryDataView page = new HistoryDataView();
            MainFormKeyDown += page.mainKeyDown;
            MarkSleepHandler += page.MarkSleep;
            FramePlayHandler += page.Execute;
            page.Disposed += historyControlDispose;
            page.Tag = false;
            page.SleepStateChangedHandle += Page_SleepStateChangedHandle;
            page.ChartArea.SelectedHappenHandler += ChartArea_SelectedHappenHandler;
            page.GUID = e.OrderItem.id;
            SetInfo(e.EdfPath);
            AddDataSource(page, e);
            AddControls(page);
            this.Invoke(new MethodInvoker(() =>
            {
                ribbonPanel4.Visible = true;
                ribbonItemGroup2.Visible = true;
                ExamineStatus examine = ExamineStatus.Empty;
                if (e.OrderItem.examineStatus != null)
                    examine = (ExamineStatus)e.OrderItem.examineStatus;
                changeHistoryViewMenu(e.OrderItem.status, examine);
                ribbon1.ActiveTab = ribbonTab1;
                timer1.Interval = 100;
                timer1.Enabled = true;
            }));
            page.ChartArea.MarkEnable = !Channel.Default.ScoreLock;
            Channel.Default.IsAutoMark = false;
            Channel.Default.Doctor.Name = (bool)GlobalSingleton.Instance.User.UserTokenInfo.IsAdmin ? "管理员" : e.OrderItem.doctor;
            Channel.Default.IsBreathOnly = e.OrderItem.examType == ExamType.PrimaryScreeningTest;
            AddLog(string.Format("加载回放页面,记录订单号为 {0},类别为 {1},病例号为 {2},病人姓名为 {3},edf文件路径为 {4},订单guid 为 {5}",
                e.OrderItem.no, e.OrderItem.examType == ExamType.PrimaryScreeningTest ? "初筛" : "多导", e.OrderItem.medicalNo,
                e.OrderItem.patientName, e.EdfPath, e.OrderItem.id), pSystem.LogManagement.LogLevel.WARN);
            return page;
        }
        /// <summary>
        /// 改变按钮状态
        /// </summary>
        /// <param name="status"></param>
        private void changeHistoryViewMenu(OrderStatus status,ExamineStatus auditStatus=ExamineStatus.NotReviewed)
        {
            ReturnHomePageRibbonBtn.Visible = true;
            MultiTrendChartRibbonBtn.Visible = true;
            ///待分析状态（开始分析、多图谱、保存分析、导出报告，提交审核）
            ///待审核状态（开始分析、多图谱、导出报告，审核（审核成功后出现上传报告））
            ///已完成状态（多图谱、下载报告）
            switch (status)
            {
                case OrderStatus.ToBeAnalysis:
                    SaveAnalysisRibbonBtn.Visible = StartAnalysisRibbonBtn.Visible = true;
                    ExportReportRibbonBtn.Visible = true;
                    DownLoadReportRibbonBtn.Visible = UploadReportRibbonBtn.Visible = false;
                    sumitAuditBtn.Visible = true;
                    AuditBtn.Visible = false;
                    break;
                case OrderStatus.ToBeAudit:
                    ExportReportRibbonBtn.Visible = true;
                    DownLoadReportRibbonBtn.Visible = false;
                    sumitAuditBtn.Visible = false;
                    if ((bool)GlobalSingleton.Instance.User.UserTokenInfo.IsAdmin)
                    {
                        StartAnalysisRibbonBtn.Visible = SaveAnalysisRibbonBtn.Visible = AuditBtn.Visible = auditStatus != ExamineStatus.Passed;
                        UploadReportRibbonBtn.Visible = auditStatus == ExamineStatus.Passed;
                        Channel.Default.ScoreLock = auditStatus == ExamineStatus.Passed;
                    }
                    else
                    {
                        StartAnalysisRibbonBtn.Visible = SaveAnalysisRibbonBtn.Visible = false;
                        AuditBtn.Visible = UploadReportRibbonBtn.Visible = false;
                        Channel.Default.ScoreLock = true;
                    }
                    break;
                case OrderStatus.Completed:
                    Channel.Default.ScoreLock = true;
                    ExportReportRibbonBtn.Visible = StartAnalysisRibbonBtn.Visible = false;
                    UploadReportRibbonBtn.Visible = SaveAnalysisRibbonBtn.Visible = false;
                    DownLoadReportRibbonBtn.Visible = true;
                    sumitAuditBtn.Visible = AuditBtn.Visible = false;
                    break;
            }
           // ribbon1.Refresh();//该方法会强制控件重绘其自身及其所有子级。 这等效于将 Invalidate 方法设置为 true 并将该方法与 Update 结合使用
        }
        private void AddDataSource(HistoryDataView view, Views.ViewsEventArgs.SwitchPageEventArgs e)
        {
            this.ProcessRunning(2, "加载edf文件...");
            string path = e.EdfPath;
            if (Path.GetExtension(path).ToLower() != ".edf")
            {
                List<string> files = new List<string>();
                FolderHelper.GetFile(path,".edf",ref files);
                for(int i = 0; i < files.Count; i++)
                {
                    EDF one = EDF.Default.ConvertToEDF(EDF.Default.ReadFile(files[i], false));
                    if (e.OrderItem.id == one.getMatchKey())
                    {
                        path = files[i];
                        break;
                    }
                }
            }
            EDF edf = view.Bind(path);
            this.ProcessRunning(40, "读取用户评分数据...");
            AnalysisResult ret= new AnalysisResult();
            var orderPaths = GlobalSingleton.Instance.User.OrderPath;
            var find = orderPaths.Find(t => t.OrderId == e.OrderItem.id);
            ///查询版本号
            bool isSuccess = ApiRequest.Instance.QueryMonitoringVersion(new RestfulWebRequest.RestfulTable.RestfulRequestTable.QueryMonitoringDataRequestModel
            {
                orderId = e.OrderItem.id
            }, out ResponseModel responseModel);
            if (isSuccess)
            {
                var data = responseModel as ResponseSuccessModel<QueryMonitoringVersionResponseModel>;
                if (find != null)
                    if (data.RestfulTable.version >= find.Version)
                    {
                        ///查询数据
                        isSuccess = ApiRequest.Instance.QueryMonitoringData(new RestfulWebRequest.RestfulTable.RestfulRequestTable.QueryMonitoringDataRequestModel
                        {
                            orderId = e.OrderItem.id
                        }, out ResponseModel responseModel2);
                        if (isSuccess)
                        {
                            find.Version++;
                            GlobalSingleton.Instance.User.OrderPathXmlHelper.Modify(orderPaths);
                            var data2 = responseModel2 as ResponseSuccessModel<QueryMonitoringDataResponseModel>;
                            {
                                ret.Epochs = view.dataManager.dataFactory.EpochsInstance.ToValue(data2.RestfulTable.frameInfo);
                                ret.EventRecords = view.dataManager.dataFactory.EventsInstance.ToValue(data2.RestfulTable.eventMarkers);
                            }
                        }
                    }
            }
            view.OrderPath = find.Clone();
            ////如果云端未读取到数据，再从本地读取，已读取到需要跳过
            if (ret.Epochs.Count == 0)
                ret = view.dataManager.ReadResult();
            this.ProcessRunning(60, "导入用户评分数据...");
            view.LoadAnalysisData(ret);
            this.ProcessRunning(100, "数据导入中...");
        }
        private void Page_SleepStateChangedHandle(string[] state, int frameNo = 0)
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
        /// 回放窗口释放时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void historyControlDispose(object sender, EventArgs e)
        {
            if (RecordViewChanged != null)
                RecordViewChanged.BeginInvoke(null,null);
            Channel.Default.ScoreLock = false;
            ribbonButtonFrameRun.SmallImage = Properties.Resources.play;
            if (!Channel.Default.SleepLock)
            {
                Channel.Default.SleepLock = true;
                LockButton.SmallImage = Properties.Resources.lock_1;
                LockButton.ToolTip = "解锁";
            }
            m_comm.DeleteDataSource();
            MainFormKeyDown -= null;
            MarkSleepHandler -= null;
            FramePlayHandler -= null;
            SetInfo("", false);
            timer1.Enabled = false;
        }
        /// <summary>
        /// 基本信息
        /// </summary>
        /// <param name="visible"></param>
        private void SetInfo(string edfPath, bool visible = true)
        {
            this.Invoke(new MethodInvoker(()=>
            {
                this.OneLable.Visible = visible;
                this.OneLable.Text = "    ";
                this.TwoLable.Visible = visible;
                this.TwoLable.Text = "    ";
                this.TwoLable.ForeColor = Color.Black;//预防可能的变色
                this.ThreeLable.Visible = visible;
                this.ThreeLable.Text = "    ";
                this.ThreeLable.ForeColor = Color.Black;//预防可能的变色
                this.FourLable.Visible = visible;
                this.FourLable.Text = "    ";
                this.FiveLabel.Visible = visible;
                this.FiveLabel.Text = "    ";
                this.SixLabel.Visible = visible;
                this.SixLabel.Text = "    ";
                this.SevenLable.Visible = visible;
                this.SevenLable.Text = "    ";
                this.EightLable.Visible = false;
                this.BatteryCapacity.Visible = false;
                if (visible)
                {
                    this.OneLable.Text = string.Format("病例号:{0}", Channel.Default.Patient.PatientNo);
                    this.TwoLable.Text = string.Format("姓名:{0}", Channel.Default.Patient.PatientName);
                    this.ThreeLable.Text = string.Format("性别:{0}", Channel.Default.Patient.Gender);
                    this.FourLable.Text = string.Format("年龄:{0}岁", Channel.Default.Patient.Age);
                    this.FiveLabel.Text = string.Format("诊断时间:{0}", Channel.Default.Patient.RecordTime);
                    this.SixLabel.Text = string.Format("评分医师:{0}", Channel.Default.Doctor.Name);
                    this.SevenLable.Text = string.Format("数据源:{0}", Path.GetFileName(edfPath));
                }
            }));
        }
        #endregion
        #region 加载实时界面

        private void NewMonitor(Views.ViewsEventArgs.SwitchPageEventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                NewDevice dev = new NewDevice();
                dev.Owner = this;
                dev.Init(e);
                dev.StartMonitorHandler += LoadRealDataPage;
                dev.ShowDialog();
                if (addrealdataview && !reconnection && hasvideo)
                {
                    AddLog("进行阻抗测试", pSystem.LogManagement.LogLevel.DEBUG);
                    ImpedanceView view = new ImpedanceView();
                    view.Init(realdataview_protocol);
                    view.ShowDialog();

                }
                addrealdataview = false;
            }));
        }
        /// <summary>
        /// 加载实时界面
        /// </summary>
        /// <returns></returns>
        private bool LoadRealDataPage(IDevice client, DataTable channelconfig, Views.ViewsEventArgs.SwitchPageEventArgs e)
        {
            client.PortClient.IOConnectHandle += this.client_IOConnectHandle;
            RealDataView page = new RealDataView();
            page.InitChannel(channelconfig, e);
            if (page.Start(client, this.panel1))
            {
                realdataview_protocol = page.Protocol;
                addrealdataview = true;
                reconnection = page.isreconnection;
                hasvideo = page.hasvideo;
                page.Tag = false;
                page.UserDataFreshHandler += Page_UserDataFreshHandler;
                page.Disposed += realControlDispose;
                page.ChartArea.SelectedHappenHandler += ChartArea_SelectedHappenHandler;
                MainFormKeyDown += page.mainKeyDown;
                ribbonPanel4.Visible = true;
                ribbonItemGroup2.Visible = false;
                ReturnHomePageRibbonBtn.Visible = StartAnalysisRibbonBtn.Visible = SaveAnalysisRibbonBtn.Visible = false;
                MultiTrendChartRibbonBtn.Visible = ExportReportRibbonBtn.Visible = DownLoadReportRibbonBtn.Visible = UploadReportRibbonBtn.Visible = false;
                SetRealInfo();
                AddControls(page);
                AddLog(string.Format("加载实时页面,记录订单号为 {0},类别为 {1},病例号为 {2},病人姓名为 {3},edf文件路径为 {4},订单guid 为 {5}",
                         e.OrderItem.no, e.OrderItem.examType == ExamType.PrimaryScreeningTest ? "初筛" : "多导", e.OrderItem.medicalNo,
                         e.OrderItem.patientName, e.EdfPath, e.OrderItem.id), pSystem.LogManagement.LogLevel.WARN);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 时基改变
        /// </summary>
        /// <param name="belong"></param>
        /// <param name="TimeSpanLine"></param>
        private void ChartArea_SelectedHappenHandler(int belong, int TimeSpanLine)
        {
            ChangeTimeSpan(TimeSpanLine);
        }

        private void Page_UserDataFreshHandler(ViewResult data)
        {
            if (this.toolStripStatusLabel3.Text != "离线")
            {
                this.FourLable.Text = data.ThermalBreathingRate;
                this.FiveLabel.Text = data.PressureBreathingRate;
                this.SixLabel.Text = data.Pressure;
                this.EightLable.Text = data.AbdominalBreathingRate;
                this.SevenLable.Text = data.ChestBreathingRate;
                if (data.PulseRate != "")
                {
                    if (!data.PulseRate.Contains("min"))
                    {
                        if (this.ThreeLable.ForeColor == Color.Black)
                            this.ThreeLable.ForeColor = Color.Red;
                        else
                        {
                            this.ThreeLable.ForeColor = Color.Black;
                        }
                    }
                    else
                    {
                        if (this.ThreeLable.ForeColor != Color.Black)
                            this.ThreeLable.ForeColor = Color.Black;
                    }
                }
                this.ThreeLable.Text = data.PulseRate;
                this.toolStripStatusLabel3.Text = data.AmbientLight;
                this.OneLable.Text = data.BodySate;
                if (data.BloodOxygen != "")
                {
                    if (!data.BloodOxygen.Contains("%"))
                    {
                        if (this.TwoLable.ForeColor == Color.Black)
                        {
                            this.TwoLable.ForeColor = Color.Red;
                        }
                        else
                        {
                            this.TwoLable.ForeColor = Color.Black;
                        }
                    }
                    else
                    {
                        if (this.TwoLable.ForeColor != Color.Black)
                            this.TwoLable.ForeColor = Color.Black;
                    }
                }
                this.TwoLable.Text = data.BloodOxygen;
                ///电池剩余容量
                if (!this.BatteryCapacity.Visible)
                    this.BatteryCapacity.Visible = true;
                if (BatteryCapacityValue != data.BatteryCapacity)
                {
                    BatteryCapacityValue = data.BatteryCapacity > 100 ? 100 : data.BatteryCapacity < 0 ? 0 : data.BatteryCapacity;
                    BatteryLastTime = (int)(16.191 * data.BatteryCapacity - 161.41);
                    this.BatteryCapacity.Invalidate();
                }
            }
        }

        /// <summary>
        /// 开始监测 work
        /// </summary>
        /// <param name="client"></param>
        /// <param name="channelconfig"></param>
        private void realControlDispose(object sender, EventArgs e)
        {
            if (RecordViewChanged != null)
                RecordViewChanged.Invoke();
            MainFormKeyDown -= null;
            SetRealInfo(false);
            this.LoadHomePage();
        }
        /// <summary>
        /// 基本信息
        /// </summary>
        /// <param name="visible"></param>
        private void SetRealInfo(bool visible = true)
        {
            this.OneLable.Visible = visible;
            this.OneLable.Text = "    ";
            this.TwoLable.Visible = visible;
            this.TwoLable.Text = "    ";
            this.ThreeLable.Visible = visible;
            this.ThreeLable.Text = "    ";
            this.FourLable.Visible = visible;
            this.FourLable.Text = "    ";
            this.FiveLabel.Visible = visible;
            this.FiveLabel.Text = "    ";
            this.SixLabel.Visible = visible;
            this.SixLabel.Text = "    ";
            this.SevenLable.Visible = visible;
            this.SevenLable.Text = "    ";
            this.EightLable.Visible = visible;
            this.EightLable.Text = "    ";
            this.BatteryCapacity.Visible = visible;
            if (!visible)
                this.toolStripStatusLabel3.Text = "准备";
        }
        /// <summary>
        /// 离线/在线状态
        /// </summary>
        /// <param name="result"></param>
        private void client_IOConnectHandle(bool result)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                toolStripStatusLabel3.Text = result ? "在线" : "离线";
            }));
        }
        #endregion
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
        private bool AddControls(UserControl uc)
        {
            bool ret = true;
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
                    if (!Convert.ToBoolean(c.Tag))
                    {
                        remove.Add(c);
                    }
                }
                if (!find)
                {
                    t.BringToFront();
                    panel1.Controls.Add(t);
                }
                else
                {
                    ret = false;
                }
                int len = remove.Count;
                while (len > 0)
                {
                    remove[0].Dispose();
                    len--;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            });
            return ret;
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

        #region 主菜单click事件

        /// <summary>
        /// 主菜单上所有按钮的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_Click(object sender, EventArgs e)
        {
            EDF.Default.Interrupt = false;
            RibbonButton ribbonButton = (RibbonButton)sender;
            switch (ribbonButton.Name)
            {
                case "StartAnalysisRibbonBtn":
                    AddLog("点击 系统-开始分析 按钮");
                    if (ribbonButton.Text == "开始分析")
                    {
                        Control control = this.getControl();
                        if (control == null || !(control is HistoryDataView))
                            break;
                        HistoryDataView historyDataView = control as HistoryDataView;
                        if (!historyDataView.AnalysisEnable)
                        {
                            AhDung.MessageTip.ShowWarning("无法分析：数据未满足自动分析的条件（采集时间>10分钟）！", -1, new bool?(), new System.Drawing.Point?(), false);
                            AddLog("无法分析：数据未满足自动分析的条件（采集时间>10分钟）！", pSystem.LogManagement.LogLevel.ERROR);
                            break;
                        }
                        if (this.ShowAnalysisSelection() == DialogResult.Cancel)
                            break;
                        ribbonButton.Text = "停止分析";
                        ProgressTipForm.Defalut.Text = "数据分析进度";
                        ProgressTipForm.Defalut.Argument = (object)historyDataView;
                        ProgressTipForm.Defalut.DoWork += new ProgressTipForm.DoWorkEventHandler(this.Analysis_DoWork);
                        ProgressTipForm.Defalut.CanceledHandle += Defalut_CanceledHandle;
                        int num2 = (int)ProgressTipForm.Defalut.ShowDialog();
                        ribbonButton.Text = "开始分析";
                        break;
                    }
                    break;
                case "SaveAnalysisRibbonBtn":
                    if (MessageForm.Show("是否保存所有评分结果？", "信息确认", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                        break;
                    AddLog("点击 系统-保存分析 按钮");
                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        if (SaveData())
                        {
                            AhDung.MessageTip.ShowOk("保存成功!", -1, new bool?(), new Point(this.Location.X + this.Width / 2, this.Location.Y + this.Height / 2));
                            AddLog("保存成功!", pSystem.LogManagement.LogLevel.WARN);
                        }
                    }));
                    break;
                case "EventDefineBtn":
                    AddLog("点击 设置-事件管理 按钮");
                    new MarkerManagement().ShowDialog();
                    break;
                case "MontageBtn":
                    AddLog("点击 设置-通道管理 按钮");
                    var montage = new Montage();
                    montage.Owner = this.ParentForm;
                    montage.StartPosition = FormStartPosition.CenterParent;
                    montage.FilterColumnLoadHandle += Channel.Default.Channel_FilterLoad;
                    montage.Init(GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentDataTable.Rows.Count != 0 ? GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelPath : GlobalSingleton.Instance.User.DefaultChannelConfig.CurrentChannelPath);
                    montage.FilterDropDownHandle += Channel.Default.channel_FilterDropDownHandle;
                    int num4 = (int)montage.ShowDialog();
                    break;
                case "MultiTrendChartRibbonBtn":
                    AddLog("点击 系统-多趋势图 按钮");
                    Moudle moudle = new Moudle();
                    moudle.ControlBox = true;
                    moudle.MaximizeBox = true;
                    moudle.CanResize = true;
                    moudle.StartPosition = FormStartPosition.CenterParent;
                    moudle.Text = "多谱图叠加";
                    moudle.Size = new System.Drawing.Size(900, 1000);
                    moudle.KeyPreview = true;
                    MulReportChart mulReportChart = new MulReportChart();
                    mulReportChart.CurrentFrameChangedHandler += new MulReportChart.CurrentFrameChangedDelegate(this.pan_CurrentFrameChangedHandler);
                    mulReportChart.Dock = DockStyle.Fill;
                    mulReportChart.CurrentFrameNo = this.m_CurrentFrameNo;
                    mulReportChart.LoadData(ResultDomain.Default, Channel.Default.IsBreathOnly);
                    moudle.Controls.Add((Control)mulReportChart);
                    int num66 = (int)moudle.ShowDialog();
                    break;
                case "SystemSettingBtn":
                    AddLog("点击 设置-系统设置 按钮");
                    if (new UserOperationConfiguration().ShowDialog() == DialogResult.OK)
                    {
                        //todo  参数更新 暂时不做
                    }
                    break;
                case "ExportReportRibbonBtn":
                    AddLog("点击 系统-导出报告 按钮");
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
                        saveFileDialog2.Filter = "Xps|*.xps|Pdf|*.pdf|Word|*.doc;*.docx";
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
                    HistoryDataView historyDataView1 = (HistoryDataView)null;
                    if (!Channel.Default.ScoreLock)
                        historyDataView1 = this.getControl() as HistoryDataView;
                    ProgressTipForm.Defalut.Text = "生成睡眠分析报告";
                    ProgressTipForm.Defalut.Argument = (object)new object[4] { str3, num7, historyDataView1, outReportSelection.ReportTempalteName };
                    ProgressTipForm.Defalut.DoWork += new ProgressTipForm.DoWorkEventHandler(this.ImportReport_DoWork);
                    ProgressTipForm.Defalut.CanceledHandle += Defalut_CanceledHandle;
                    int num8 = (int)ProgressTipForm.Defalut.ShowDialog();
                    AddLog("进度条窗口展示状态:" + (DialogResult)num8, pSystem.LogManagement.LogLevel.DEBUG);
                    AddLog(string.Format("生成睡眠分析报告成功！生成的报告名称为 {0}", StringPath.ConvertLogPath(str3)), pSystem.LogManagement.LogLevel.WARN);
                    break;
                case "ReturnHomePageRibbonBtn":
                    AddLog("点击 系统-首页 按钮");
                    this.LoadHomePage();
                    break;
                case "UploadReportRibbonBtn":
                    AddLog("点击 系统-上传报告 按钮");
                    OpenFileDialog upLoad = new OpenFileDialog();
                    upLoad.Filter = "Excel|*.xls;*.xlsx|Pdf|*.pdf|Word|*.doc;*.docx";
                    upLoad.RestoreDirectory = true;
                    if (upLoad.ShowDialog() == DialogResult.OK)
                    {
                        string reportPath = upLoad.FileName;
                        HistoryDataView historyDataView = getControl() as HistoryDataView;
                        bool isSuccess = UploadReportHelper.Upload(reportPath, historyDataView.GUID);
                        if (isSuccess)
                        {
                            changeHistoryViewMenu(OrderStatus.Completed);
                            AhDung.MessageTip.ShowOk("报告上传成功!");
                            AddLog(string.Format("报告上传成功 文件路径为 {0}", reportPath), pSystem.LogManagement.LogLevel.WARN);
                        }
                        else
                        {
                            AhDung.MessageTip.ShowError("报告上传失败");
                            AddLog(string.Format("报告上传失败 文件路径为 {0}", reportPath), pSystem.LogManagement.LogLevel.ERROR);
                        }
                    }
                    break;
                case "DownLoadReportRibbonBtn":
                    AddLog("点击 系统-下载报告 按钮");
                    SaveFileDialog downReport = new SaveFileDialog();
                    //打开的文件选择对话框上的标题
                    downReport.Title = "请选择文件";
                    downReport.Filter = "Excel|*.xls;*.xlsx|Pdf|*.pdf|Word|*.doc;*.docx";
                    downReport.FileName = string.Format("{1}({2}){4}报告{0}--{3}", (object)Channel.Default.StartTime.ToString("yyyyMMddHHmmss"), (object)Channel.Default.Patient.PatientName, (object)Channel.Default.Patient.PatientNo, (object)Channel.Default.Doctor.Name, Channel.Default.IsBreathOnly ? "初筛" : "睡眠分析");
                    downReport.RestoreDirectory = true;
                    downReport.FilterIndex = 1;
                    if (downReport.ShowDialog() == DialogResult.OK)
                    {
                        //获得文件路径
                        string localFilePath = downReport.FileName.ToString();
                        //获取文件路径，不带文件名
                        string FilePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));
                        //获取文件名，带后缀名，不带路径
                        string fileNameWithSuffix = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);
                        //去除文件后缀名
                        string fileNameWithoutSuffix = fileNameWithSuffix.Substring(0, fileNameWithSuffix.LastIndexOf("."));

                        HistoryDataView historyDataView = getControl() as HistoryDataView;
                        /// 从云服务具体下载报告
                        string reportPath = DownloadFileHelper.Download(FilePath,
                                                    fileNameWithoutSuffix,
                                                    historyDataView != null ? historyDataView.GUID : "",
                                                    FileType.Report);
                        if (String.IsNullOrEmpty(reportPath))
                        {
                            AhDung.MessageTip.ShowWarning("请等待其他下载任务完成！");
                            AddLog(string.Format("下载报告失败，有其他下载任务", pSystem.LogManagement.LogLevel.ERROR));
                            break;
                        }
                        else
                        {
                            AhDung.MessageTip.ShowOk("下载成功！");
                            AddLog(string.Format("下载报告成功！ 下载的文件名为 {0},保存路径为 {1}", fileNameWithSuffix, FilePath), pSystem.LogManagement.LogLevel.WARN);
                            System.Diagnostics.Process.Start(reportPath);
                        }
                    }
                    break;
                case "ribbonButton7":
                    AddLog("点击 帮助-操作手册 按钮");
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
                    AhDung.MessageTip.ShowError("未找到操作手册说明书", -1, new bool?(), new System.Drawing.Point?(), false);
                    AddLog("未找到操作手册说明书", pSystem.LogManagement.LogLevel.ERROR);
                    break;
                case "ribbonButton8":
                    AddLog("点击 帮助-关于 按钮");
                    new About().ShowDialog();
                    break;
                case "AutoScore":
                    if (Channel.Default.ScoreLock)
                        return;
                    if (Channel.Default.IsAutoMark)
                    {
                        Channel.Default.IsAutoMark = false;
                        AutoScore.SmallImage = Properties.Resources.auto_2;
                        AutoScore.ToolTip = "自动事件判断：关";
                        AddLog("用户 点击自动事件判断按钮 点击之后当前 自动事件判断：关");
                    }
                    else
                    {
                        Channel.Default.IsAutoMark = true;
                        AutoScore.SmallImage = Properties.Resources.auto_1;
                        AutoScore.ToolTip = "自动事件判断：开";
                        AddLog("用户 点击自动事件判断按钮 点击之后当前 自动事件判断：开");
                    }
                    break;

                case "sumitAuditBtn":
                    if (true)
                    {
                        AddLog("点击 系统-提交审核 按钮");
                        if (SaveData())
                        {
                            HistoryDataView historyDataView = getControl() as HistoryDataView;
                            //更改记录状态是否成功
                            bool isSuccess = ApiRequest.Instance.SaveAnalysis(new RestfulWebRequest.RestfulTable.RestfulRequestTable.SaveAnalysisRequestModel()
                            {
                                id = historyDataView.GUID
                            }, out ResponseModel responseModel);
                            if (isSuccess)
                            {
                                Channel.Default.ScoreLock = !(bool)GlobalSingleton.Instance.User.UserTokenInfo.IsAdmin;
                                historyDataView.ChartArea.MarkEnable = !Channel.Default.ScoreLock;
                                sumitAuditBtn.Visible = false;
                                StartAnalysisRibbonBtn.Visible = false;
                                SaveAnalysisRibbonBtn.Visible = false;
                                if ((bool)GlobalSingleton.Instance.User.UserTokenInfo.IsAdmin)
                                {
                                    AuditBtn.Visible = true;
                                }
                                AhDung.MessageTip.ShowOk("提交审核成功！");
                                AddLog("审核提交成功", pSystem.LogManagement.LogLevel.WARN);
                                return;
                            }
                        }
                        AddLog("审核提交失败", pSystem.LogManagement.LogLevel.ERROR);
                        AhDung.MessageTip.ShowError("提交审核失败，请重新提交！");
                    }
                    break;

                case "AuditBtn":
                    if (true)
                    {
                        AddLog("点击 系统-审核 按钮");
                        DialogResult auditResult = MessageForm.Show("评分结果经诊断，是否允许通过?", "审核确认", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        HistoryDataView historyDataView = getControl() as HistoryDataView;
                        if (auditResult == DialogResult.Yes)
                        {
                            //更改记录状态是否成功
                            bool isSuccess = ApiRequest.Instance.SaveAnalysis(new RestfulWebRequest.RestfulTable.RestfulRequestTable.SaveAnalysisRequestModel()
                            {
                                id = historyDataView.GUID
                            }, out ResponseModel responseModel);
                            //审核
                            isSuccess = ApiRequest.Instance.Audit(new RestfulWebRequest.RestfulTable.RestfulRequestTable.AuditRequestModel()
                            {
                                id = historyDataView.GUID,
                                status = ExamineStatus.Passed,
                                remark = "分析结果予以通过"
                            }, out ResponseModel responseModel2);
                            if (!isSuccess)
                            {
                                AhDung.MessageTip.ShowError("操作失败，请重新审核！");
                                AddLog("审核操作失败", pSystem.LogManagement.LogLevel.ERROR);
                                return;
                            }
                            changeHistoryViewMenu(OrderStatus.ToBeAudit, ExamineStatus.Passed);
                            Channel.Default.ScoreLock = true;
                            historyDataView.ChartArea.MarkEnable = !Channel.Default.ScoreLock;
                            AhDung.MessageTip.ShowOk("操作成功，请上传报告！");
                            AddLog("管理员判断 审核通过",pSystem.LogManagement.LogLevel.WARN);
                        }
                        else if (auditResult == DialogResult.No)
                        {
                            //审核
                            bool isSuccess = ApiRequest.Instance.Audit(new RestfulWebRequest.RestfulTable.RestfulRequestTable.AuditRequestModel()
                            {
                                id = historyDataView.GUID,
                                status = ExamineStatus.NoPassed,
                                remark = "分析结果有误!"
                            }, out ResponseModel responseModel);
                            if (!isSuccess)
                            {
                                AhDung.MessageTip.ShowError("操作失败，请重新审核！");
                                AddLog("审核操作失败", pSystem.LogManagement.LogLevel.ERROR);
                                return;
                            }
                            changeHistoryViewMenu(OrderStatus.ToBeAudit, ExamineStatus.NoPassed);
                            Channel.Default.ScoreLock = false;
                            historyDataView.ChartArea.MarkEnable = !Channel.Default.ScoreLock;
                            AhDung.MessageTip.ShowOk("操作成功：订单审核不予通过！");
                            AddLog("管理员判断 审核不予通过", pSystem.LogManagement.LogLevel.WARN);
                        }
                    }
                    break;
                case "SignOutRibbonBtn":
                    AddLog("点击 系统-注销 按钮");
                    if (MessageForm.Show("是否注销当前用户?", "信息确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        AddLog("用户取消 注销");
                        return;
                    }
                    else
                    {
                        AddLog("用户确定 注销");
                    }
                    _mainFormCloseMethod = MainFormCloseMethod.SignOut;
                    this.Close();
                    break;
                case "ExitRibbonBtn":
                    AddLog("点击 系统-退出 按钮");
                    //if (MessageForm.Show("是否退出当前用户?", "信息确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    //{
                    //    AddLog("用户取消 退出");
                    //    return;
                    //}
                    //else
                    //{
                    //    AddLog("用户确定 退出");
                    //}
                    _mainFormCloseMethod = MainFormCloseMethod.Exit;
                    this.Close();
                    break;
            }
        }

        private void FreshMainMenu()
        {
            this.Invoke(new MethodInvoker(() =>
            {
                bool visibly = !Channel.Default.ScoreLock;
                ///睡眠分期按钮不可见
                ribbonButton20.Visible = ribbonButton21.Visible = ribbonButton22.Visible = ribbonButton23.Visible = ribbonButton24.Visible = LockButton.Visible = visibly;
                ///自动事件标记
                AutoScore.Visible = visibly;

                StartAnalysisRibbonBtn.Enabled = SaveAnalysisRibbonBtn.Enabled = visibly;
            }));
        }
        /// <summary>
        /// 开始分析 work
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Analysis_DoWork(ProgressTipForm sender, DoWorkEventArgs e)
        {
            DateTime onetime = DateTime.Now;
            UserOperationConfig m_userOperationConfig = GlobalSingleton.Instance.User.UserConfig;
            HistoryDataView historyDataView = sender.Argument as HistoryDataView;
            sender.SetProgress(2, "数据初始化...");
            AnalysisResult analysisResult = historyDataView.getAnalysisResult(true);
            AddLog(string.Format("初始化信息 [GUID：{0} startTime：{1} lightoff：{2} lighton：{3}] type：{4}", analysisResult.GUID, analysisResult.StartTime, analysisResult.LightOffTime, analysisResult.LightOnTime, Channel.Default.IsBreathOnly ? "初筛" : "多导睡眠"), pSystem.LogManagement.LogLevel.DEBUG);
            if (!this.m_TargetInstance.Init(analysisResult.GUID, analysisResult.EdfPath, analysisResult.StartTime, analysisResult.LightOffTime, analysisResult.LightOnTime, Channel.Default.IsBreathOnly))
            {
                e.Cancel = true;
                sender.SetError("尝试分析的开关灯时间范围失效，请重新标定后重试！");
            }
            else
            {
                bool autoAnalysis = m_userOperationConfig.LastConfiguredAnalysisParameters != 0;
                if (!autoAnalysis)
                {
                    AddLog("不进入AI分析：未选择待分析的事件类别", pSystem.LogManagement.LogLevel.WARN);
                    return;
                }
                //防止取消分析后直接再次分析，所以等待2秒，让后台算法进程释放
                System.Threading.Thread.Sleep(2000);
                sender.SetProgress(30, "导入用户标记事件...");
                DateTime now1 = DateTime.Now;
                if (!this.m_TargetInstance.SubmitAnalysisConditions(new InConditions()
                {
                    Epochs = analysisResult.Epochs,
                    EventRecords = analysisResult.EventRecords,
                    AnalysisStateWord = (int)m_userOperationConfig.LastConfiguredAnalysisParameters
                }))
                {
                    e.Cancel = true;
                    sender.SetError("导入用户标记事件失败！");
                }
                else
                {
                    Console.WriteLine(string.Format("上传数据耗时：{0}ms", (object)(DateTime.Now - now1).TotalMilliseconds).ToString());
                    DateTime now2 = DateTime.Now;
                    sender.SetProgress(60, "数据分析...");
                    if (sender.CancellationPending)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        AddLog("自动分析                开始获取分析结果", pSystem.LogManagement.LogLevel.DEBUG);
                        AnalysisResult result = AnalysisResult.Convert(this.m_TargetInstance.getResult());
                        if (!sender.CancellationPending)
                            AddLog("自动分析                获取分析结果完成", pSystem.LogManagement.LogLevel.WARN);
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
                                sender.SetError("分析失败！");
                            }
                            if (!e.Cancel)
                            {
                                Channel.Default.AnalysisReult = result;
                                sender.SetProgress(90, "加载分析后的数据...");
                                AddLog("自动分析                开始加载分析后的数据", pSystem.LogManagement.LogLevel.DEBUG);
                                historyDataView.LoadAnalysisData(Channel.Default.AnalysisReult, true);
                                if (!sender.CancellationPending)
                                    AddLog("自动分析                加载分析后的数据完成", pSystem.LogManagement.LogLevel.WARN);
                                if (sender.CancellationPending)
                                {
                                    e.Cancel = true;
                                }
                                else
                                {
                                    sender.SetProgress(100, "分析完成");
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
            AddLog(string.Format("分析完成总耗时：{0} min", (object)(DateTime.Now - onetime).TotalMinutes).ToString(),pSystem.LogManagement.LogLevel.DEBUG);
        }

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
                    AhDung.MessageTip.ShowWarning("尝试分析的开关灯时间范围失效，请重新标定后重试！", -1, null, null, false);
                    AddLog("尝试分析的开关灯时间范围失效，请重新标定后重试", pSystem.LogManagement.LogLevel.ERROR);
                    return;
                }
                sender.SetProgress(15, "导入用户标记事件...");
                if (!this.m_TargetInstance.SubmitAnalysisConditions(new InConditions()
                {
                    Epochs = analysisResult.Epochs,
                    EventRecords = analysisResult.EventRecords,
                    AnalysisStateWord = 0
                }))
                {
                    sender.SetError("导入用户标记事件失败！");
                    AddLog("导入用户标记事件失败！", pSystem.LogManagement.LogLevel.ERROR);
                    return;
                }
                sender.SetProgress(30, "分析数据...");
                AnalysisResult analysisResult2 = AnalysisResult.Convert(this.m_TargetInstance.getResult());
                analysisResult2.StartTime = Channel.Default.AnalysisReult.StartTime;
                analysisResult2.EndTime = Channel.Default.AnalysisReult.EndTime;
                analysisResult2.Tag = Channel.Default.AnalysisReult.Tag;
                analysisResult2.BreathEvent.AveragePressureInTIB = analysisResult.BreathEvent.AveragePressureInTIB;
                Channel.Default.AnalysisReult = analysisResult2;
                if (sender.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                sender.SetProgress(40, "提取数据...");
                num2 = 40;
            }
            sender.SetProgress(2 + num2, "生成报告基础信息...");
            if (Channel.Default.Patient == null)
            {
                Channel.Default.Patient = new Doc_PatientInfo();
            }
            Channel.Default.Patient.DoctorName = Channel.Default.Doctor.Name;
            Channel.Default.Patient.CurrentDateTime = DateTime.Now;
            Channel.Default.Patient.RecordTime = Channel.Default.StartTime;
            Assembly assembly = Assembly.Load("AwareTec.Polysmith.DataBaseCom");
            Type[] types = assembly.GetTypes();
            List<string> des = new List<string>();
            List<string> values = new List<string>();
            PropertyInfo[] properties = typeof(AnalysisResult).GetProperties();
            sender.SetProgress(10 + num2, "生成报告数据信息...");
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
            sender.SetProgress(20 + num2, "生成报告图样信息...");
            System.Drawing.Image image = Channel.Default.CreatMap(null, Channel.Default.IsBreathOnly);
            image.Save(Channel.Default.Patient.ResultPhoto);
            sender.SetProgress(70, "正在保存报告文件...");
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
                    sender.SetError("导入Excel失败，请查看日志");
                    AddLog("[导入Excel失败]:" + ex.Message, pSystem.LogManagement.LogLevel.ERROR);
                    return;
                }
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
                    sender.SetProgress(80, "报告保存成功！");
                }
                else if (per == 90)
                {
                    sender.SetProgress(90, "正在打开报告...");
                }
                else
                {
                    if (per == 100)
                    {
                        sender.SetProgress(100, "完成");
                        return;
                    }
                    if (per == -1)
                    {
                        sender.SetProgress(100, "");
                        sender.SetError("保存失败！");
                        AddLog("保存失败！", pSystem.LogManagement.LogLevel.ERROR);
                        return;
                    }
                    sender.SetProgress(per);
                }
                System.Threading.Thread.Sleep(50);
            }
        }
        /// <summary>
        /// 取消分析时触发
        /// </summary>
        private void Defalut_CanceledHandle()
        {
            m_comm.AnalysisStop();
        }
        #endregion

        #region 导联下拉框
        /// <summary>
        /// 导联方案下拉框下拉时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ribbonComboBox1_DropDownShowing(object sender, EventArgs e)
        {
            List<string> items = ChannelManageCloud.Default.GetCfgNames();
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
            string path = string.Format("{0}\\{1}", ChannelManageCloud.Default.ConfigruationBasePath, selectText);
            if (selectText != "" && path != GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelPath)
            {
                ChannelConfig channelConfig = new ChannelConfig(path);
                GlobalSingleton.Instance.User.CurrentChannelConfig = channelConfig;

                Channel.Default.ChannelChanged();
            }
            AddLog(string.Format("用户 选择导联方案为{0}", selectText));
        }
        #endregion
        /// <summary>
        /// 添加日志信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="level"></param>
        private void AddLog(string msg, pSystem.LogManagement.LogLevel level= pSystem.LogManagement.LogLevel.INFO)
        {
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(msg,level);
        }
        #region 回放相关操作
        /// <summary>
        /// 保存一次数据
        /// </summary>
        /// <returns></returns>
        private bool SaveData()
        {
            HistoryDataView historyDataView = getControl() as HistoryDataView;
            AnalysisResult analysisResult = historyDataView.getAnalysisResult();
            if (!this.m_TargetInstance.Init(analysisResult.GUID, analysisResult.EdfPath, analysisResult.StartTime, analysisResult.LightOffTime, analysisResult.LightOnTime, Channel.Default.IsBreathOnly))
            {
                AhDung.MessageTip.ShowWarning("尝试分析的开关灯时间范围失效，请重新标定后重试！", -1, null, null, false);
                AddLog("尝试分析的开关灯时间范围失效，请重新标定后重试！", pSystem.LogManagement.LogLevel.ERROR);
                return false;
            }
            if (!this.m_TargetInstance.SubmitAnalysisConditions(analysisResult.Epochs, analysisResult.EventRecords, false))
            {
                AhDung.MessageTip.ShowError("保存失败!", -1, new bool?(), new Point(this.Location.X + this.Width / 2, this.Location.Y + this.Height / 2));
                AddLog("保存失败! 提交分析条件失败", pSystem.LogManagement.LogLevel.ERROR);
                return false;
            }
            AnalysisResult analysisResult2 = AnalysisResult.Convert(this.m_TargetInstance.getResult());
            analysisResult2.StartTime = Channel.Default.AnalysisReult.StartTime;
            analysisResult2.EndTime = Channel.Default.AnalysisReult.EndTime;
            analysisResult2.Tag = Channel.Default.AnalysisReult.Tag;
            Channel.Default.AnalysisReult = analysisResult2;
            var result = historyDataView.dataManager.SaveDataResult(analysisResult2);
            Channel.Default.CreatMap((ResultDomain)null, Channel.Default.IsBreathOnly).Save(Channel.Default.Patient.ResultPhoto);
            ///查询版本号
            bool isSuccess = ApiRequest.Instance.QueryMonitoringVersion(new RestfulWebRequest.RestfulTable.RestfulRequestTable.QueryMonitoringDataRequestModel
            {
                orderId = historyDataView.GUID
            }, out ResponseModel responseModel);
            bool notFind = responseModel.HttpStatusCode == System.Net.HttpStatusCode.Forbidden;
            if (isSuccess || notFind)
            {
                if (!notFind)
                {
                    //var data = responseModel as ResponseSuccessModel<QueryMonitoringVersionResponseModel>;
                    //if (data.RestfulTable.version > 0)
                    {
                        ///修改用户数据
                        isSuccess = ApiRequest.Instance.EditMonitoringData(historyDataView.getRemoteAnalysisResult(result), out ResponseModel responseModel2);
                    }
                }
                else
                {
                    ///保存用户数据
                    isSuccess = ApiRequest.Instance.AddMonitoringData(historyDataView.getRemoteAnalysisResult(result), out ResponseModel responseModel2);
                }
                if (isSuccess)
                {
                    var orderPaths = GlobalSingleton.Instance.User.OrderPath;
                    var find = orderPaths.Find(t => t.OrderId == historyDataView.GUID);
                    if (find != null)
                    {
                        find.Version++;
                        GlobalSingleton.Instance.User.OrderPathXmlHelper.Modify(orderPaths);
                        historyDataView.OrderPath = find.Clone();
                    }
                    return true;
                }
            }
            AhDung.MessageTip.ShowError("保存失败!", -1, new bool?(), new Point(this.Location.X + this.Width / 2, this.Location.Y + this.Height / 2));
            AddLog(string.Format("保存失败! 失败原因为更改记录状态成功 guid为 {0} 事件地址为{1}", analysisResult.GUID, historyDataView.dataManager.dataFactory.BasePath), pSystem.LogManagement.LogLevel.ERROR);
            return false;
        }
        /// <summary>
        /// 定时器任务（刷新睡眠分期状态）
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
                ribbonTextBox1.TextBoxText = state[0];
                ribbonTextBox2.TextBoxText = state[1];
                ribbonTextBox3.TextBoxText = state[2];
                ribbonTextBox4.TextBoxText = state[3];
                ribbonTextBox5.TextBoxText = state[4];
                ribbonTextBox6.TextBoxText = state[5];
                ribbonTextBox7.TextBoxText = state[6];
                ribbonTextBox8.TextBoxText = state[7];
                ribbonTextBox9.TextBoxText = state[8];
                ribbonTextBox10.TextBoxText = state[9];
            }
        }
        private delegate void MarkSleepDelegate(int typ, bool iskeyDown = false);
        private event MarkSleepDelegate m_MarkSleepHandler;
        /// <summary>
        /// 睡眠标记按键按下时触发
        /// </summary>
        private event MarkSleepDelegate MarkSleepHandler
        {
            add
            {
                if (m_MarkSleepHandler != null)
                    m_MarkSleepHandler = null;
                m_MarkSleepHandler += value;

            }
            remove
            {
                m_MarkSleepHandler = null;
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
            int value = 5;
            switch (rbt.Name)
            {
                case "ribbonButton20"://w
                    value = 5;
                    break;
                case "ribbonButton21"://n1
                    value = 3;
                    break;
                case "ribbonButton22"://n2
                    value = 2;
                    break;
                case "ribbonButton23"://n3
                    value = 1;
                    break;
                case "ribbonButton24"://r
                    value = 4;
                    break;
                case "LockButton":
                    if (Channel.Default.ScoreLock)
                    {
                        AhDung.MessageTip.ShowWarning("当前订单评分已完成！");
                        return;
                    }
                    //解锁
                    const string LOCK_TEXT = "锁定";
                    const string UNLOCK_TEXT = "解锁";
                    const string SUCCESS = "成功";
                    if (Channel.Default.SleepLock)
                    {
                        Channel.Default.SleepLock = false;
                        LockButton.SmallImage = Properties.Resources.lock_2;
                        LockButton.ToolTip = LOCK_TEXT;
                        AddLog("用户点击 解锁按钮，解锁成功");
                        AhDung.MessageTip.ShowOk(UNLOCK_TEXT + SUCCESS);
                    }
                    //锁定
                    else
                    {
                        Channel.Default.SleepLock = true;
                        LockButton.SmallImage = Properties.Resources.lock_1;
                        LockButton.ToolTip = UNLOCK_TEXT;
                        AddLog("用户点击 锁定按钮，锁定成功");
                        AhDung.MessageTip.ShowOk(LOCK_TEXT + SUCCESS);
                    }
                    return;
            }
            if (!Channel.Default.SleepLock && !Channel.Default.ScoreLock)
            {
                if (m_MarkSleepHandler != null)
                {
                    m_MarkSleepHandler.Invoke(value);
                }
            }
            else
            {
                AhDung.MessageTip.ShowWarning(Channel.Default.SleepLock ? "请先解开睡眠分期使能锁！" : Channel.Default.ScoreLock ? "评分已结束，如需重新评分，请先解锁评分状态！" : "无权限");
            }
        }
        private delegate void FramePlayDelegate(int typ, bool pageAll = false);
        private event FramePlayDelegate m_FramePlayHandler;
        /// <summary>
        /// 控制数据翻页时触发
        /// </summary>
        private event FramePlayDelegate FramePlayHandler
        {
            add
            {
                if (m_FramePlayHandler != null)
                    m_FramePlayHandler = null;
                m_FramePlayHandler += value;

            }
            remove
            {
                m_FramePlayHandler = null;
            }
        }
        private void PlayFrame_MouseDown(object sender, EventArgs e)
        {
            RibbonButton rbt = (RibbonButton)sender;
            int typ = 9;
            switch (rbt.Name)
            {
                case "ribbonButton28":
                    typ = 0;
                    break;
                case "ribbonButton29":
                    typ = 1;
                    break;
                case "ribbonButton31":
                    typ = 2;
                    break;
                case "ribbonButton32":
                    typ = 3;
                    break;
                case "ribbonButtonFrameRun":
                    if (ribbonButtonFrameRun.Value == "4")
                    {
                        ribbonButtonFrameRun.SmallImage = Properties.Resources.pause;
                        typ = 4;
                        ribbonButtonFrameRun.Value = "5";
                    }
                    else
                    {
                        ribbonButtonFrameRun.SmallImage = Properties.Resources.play;
                        typ = 5;
                        ribbonButtonFrameRun.Value = "4";
                    }
                    break;
            }
            if (m_FramePlayHandler != null)
                m_FramePlayHandler.Invoke(typ,true);
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
                if (block is HistoryDataView)
                {
                    (block as HistoryDataView).ChangeFrameNo(frameCnt);
                }
            }
        }
        #endregion

        #region  时基切换
        /// <summary>
        /// 改变时基显示状态
        /// </summary>
        /// <param name="sTimeSpan"></param>
        private void ChangeTimeSpan(int sTimeSpan)
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
                    ChangeTimeSpan(sTimeSpan);
                }));
            }
            else
            {
                foreach (RibbonItem c in ribbonItemGroup1.Items)
                {
                    if (c is RibbonButton)
                    {
                        if (c.Text == null)
                            return;
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
        }
        private delegate void BaseTimeChangedDelegate(int baseTime);
        private event BaseTimeChangedDelegate m_BaseTimeChangedHandler;
        /// <summary>
        /// 时基切换时触发
        /// </summary>
        private event BaseTimeChangedDelegate BaseTimeChanged
        {
            add
            {
                if (m_BaseTimeChangedHandler != null)
                    m_BaseTimeChangedHandler = null;
                m_BaseTimeChangedHandler += value;

            }
            remove
            {
                m_BaseTimeChangedHandler = null;
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
                string strValue = rbt.Text.ToUpper();
                int value = 30;
                if (strValue.Contains("S"))
                {
                    value = int.Parse(strValue.Replace("S", ""));
                }
                else if (strValue.Contains("MIN"))
                {
                    value = int.Parse(strValue.Replace("MIN", "")) * 60;
                }
                else if (strValue.Contains("H"))
                {
                    value = int.Parse(strValue.Replace("H", "")) * 3600;
                }
                rbt.Checked = true;
                bak_butt = rbt;
                Channel.Default.BaseTimeLineSpan = value;
                Channel.Default.ChannelChanged();
            }
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
            return MessageForm.Show(msg, "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes;
        }
        private DialogResult ShowMessageYNC(string msg)
        {
            object ss = this.Invoke(new MessageBoxShowYNC(MessageBoxShow_YNC), new object[] { msg });
            return (DialogResult)ss;
        }
        delegate DialogResult MessageBoxShowYNC(string msg);
        DialogResult MessageBoxShow_YNC(string msg)
        {
            return MessageForm.Show(msg, "信息提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
        }
        private DialogResult ShowAnalysisSelection()
        {
            object ss = this.Invoke(new _MessageBoxShowAnalysisSelection(MessageBoxShow_AnalysisSelection));
            return (DialogResult)ss;
        }
        delegate DialogResult _MessageBoxShowAnalysisSelection();
        DialogResult MessageBoxShow_AnalysisSelection()
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
                    toolStripStatusLabel3.Text = "准备";
                }
                else
                {
                    toolStripProgressBar1.Value = percent;
                    this.toolStripProgressBar1.Visible = true;
                    toolStripStatusLabel3.Text = des;
                    //DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("双击进入回放：完成进度>>{0}%", percent), pSystem.LogManagement.LogLevel.DEBUG);
                }
            }));
        }
        #endregion

        #endregion
    }
}
