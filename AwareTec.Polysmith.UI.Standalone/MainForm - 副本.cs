using AwareTec.Polysmith.pChart;
using AwareTec.Polysmith.UI.FunctionControls.tools;
using AwareTec.Polysmith.Util;
using pSystem.Communication.Com;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace AwareTec.Polysmith.UI
{
    public partial class MainForm : SkinForm
    {
        public MainForm()
        {
            InitializeComponent();
            // CheckForIllegalCrossThreadCalls = false;
            BatteryCapacity.Paint += BatteryCapacity_Paint;
            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);
            UpdateStyles();
            Channel.Default.ChannelCreatingHandle += m_channel_Management_ChannelCreatingHandle;
            Channel.Default.ChangeMenuHandle += FreshHomeMenu;
            Channel.Default.NextStepHandle += patient_NextStepHandle;
            Channel.Default.StartHistroyAnalysisByEDFHandle += ihd_StartHistroyAnalysisByEDFHandle;
            DataModel.DeviceOnLine.Default.OffLineHandler += Default_OffLineHandler;
            DataModel.ResultDomain.Default.Start = true;
            DataModel.MarkerManage.Default.Start = true;
            bak_butt = ribbonButton36;
            bak_butt.Checked = true;
            this.Load += MainForm_Load;
            this.KeyDown += MainForm_KeyDown;
            ribbon1.Expanded = false;
            this.TopLevel = true;
        }

        private void Default_ConfigNameChanged(string name)
        {
            if (this.IsHandleCreated)
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    ribbonComboBox1.TextBoxText = name;
                    if (panel1.Controls.Count > 0)
                    {
                        if (panel1.Controls[0] is FunctionControls.HistoryDataView)
                        {
                            (panel1.Controls[0] as FunctionControls.HistoryDataView).UpdateMontageName(name);
                        }
                        else if (panel1.Controls[0] is FunctionControls.RealDataView)
                        {
                            (panel1.Controls[0] as FunctionControls.RealDataView).UpdateMontageName(name);
                        }
                    }
                }));
            }
        }
        #region 快捷键处理

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (panel1.Controls.Count > 0)
            {
                if (panel1.Controls[0] is FunctionControls.HistoryDataView)
                {
                    switch (keyData)
                    {
                        case Keys.Right:
                        case Keys.Left:
                            (panel1.Controls[0] as FunctionControls.HistoryDataView).PageMove(keyData == Keys.Up ? 0 : keyData == Keys.Down ? 3 : keyData == Keys.Left ? 1 : 2, true);
                            return true;
                    }
                }
                else if (panel1.Controls[0] is FunctionControls.AnalysisRecordView)
                {
                    if ((panel1.Controls[0] as FunctionControls.AnalysisRecordView).mainKeyDown(keyData))
                        return true;
                    else
                        return base.ProcessCmdKey(ref msg, keyData);
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            if (e.Shift && e.Control && e.KeyCode == Keys.F)
            {
                if (panel1.Controls.Count > 0)
                {
                    if (panel1.Controls[0] is FunctionControls.HistoryDataView)
                    {
                        (panel1.Controls[0] as FunctionControls.HistoryDataView).HistoryDataView_KeyDown(sender);
                    }
                    else if (panel1.Controls[0] is FunctionControls.RealDataView)
                    {
                        (panel1.Controls[0] as FunctionControls.RealDataView).RealTimeDataView_KeyDown(sender);
                    }
                }
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                if (panel1.Controls.Count > 0)
                {
                    if (panel1.Controls[0] is FunctionControls.HistoryDataView)
                    {
                        (panel1.Controls[0] as FunctionControls.HistoryDataView).HistoryDataView_KeyDown();
                    }
                    else if (panel1.Controls[0] is FunctionControls.RealDataView)
                    {
                        (panel1.Controls[0] as FunctionControls.RealDataView).RealTimeDataView_KeyDown();
                    }
                }
            }
            else if (e.Control && e.Alt && e.KeyCode == Keys.Z)
            {
                if (panel1.Controls.Count > 0)
                {
                    if (panel1.Controls[0] is FunctionControls.HistoryDataView)
                    {
                        (panel1.Controls[0] as FunctionControls.HistoryDataView).HistoryDataView_Return();
                    }
                }
            }
            else if (e.KeyCode == Keys.F10)
            {
                LockDialog dl = new LockDialog();
                dl.StartPosition = FormStartPosition.CenterParent;
                dl.Text = "权限解锁";
                if (dl.ShowDialog() == DialogResult.OK)
                {
                    Block.SystemParamDialog mm = new Block.SystemParamDialog();
                    mm.Owner = this;
                    mm.StartPosition = FormStartPosition.CenterParent;
                    mm.ShowDialog();
                }
            }
            else if (e.KeyCode == Keys.D1 || e.KeyCode == Keys.D2 || e.KeyCode == Keys.D3 || e.KeyCode == Keys.W || e.KeyCode == Keys.R||e.KeyCode==Keys.NumPad0
                || e.KeyCode == Keys.NumPad1 || e.KeyCode == Keys.NumPad2 || e.KeyCode == Keys.NumPad3 || e.KeyCode == Keys.NumPad5)
            {
                if (panel1.Controls.Count > 0)
                {
                    if (panel1.Controls[0] is FunctionControls.HistoryDataView)
                    {
                        if (!Channel.Default.SleepLock && !Channel.Default.ScoreLock)
                        {

                            (panel1.Controls[0] as FunctionControls.HistoryDataView).MarkSleep((e.KeyCode == Keys.W || e.KeyCode == Keys.NumPad0) ? 5 : (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1) ? 3 : (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2) ? 2 : (e.KeyCode == Keys.D3 || e.KeyCode == Keys.NumPad3) ? 1 : 4);
                            //(panel1.Controls[0] as FunctionControls.HistoryDataView).Execute(2);
                        }
                        else
                        {
                            AhDung.MessageTip.ShowWarning(Channel.Default.SleepLock ? "请先解开睡眠分期使能锁！" : Channel.Default.ScoreLock ? "评分已结束，如需重新评分，请先解锁评分状态！" : "无权限");
                        }
                    }
                }
            }
            else if ((e.KeyCode == Keys.Left || e.KeyCode == Keys.Right) && e.Alt)
            {
                if (panel1.Controls.Count > 0)
                {
                    if (panel1.Controls[0] is FunctionControls.HistoryDataView)
                    {
                        (panel1.Controls[0] as FunctionControls.HistoryDataView).PageMove(e.KeyCode == Keys.Up ? 0 : e.KeyCode == Keys.Down ? 3 : e.KeyCode == Keys.Left ? 1 : 2, false);
                    }
                }
            }
        }

        #endregion
        private void MainForm_Load(object sender, EventArgs e)
        {
            string[] strvalue = Channel.Default.Channel_FilterLoad("SingleNotch");
            SingleNotchRibbonButton.DropDownItems.Add(new RibbonButton("OFF"));
            for (int i = 0; i < strvalue.Length; i++)
                SingleNotchRibbonButton.DropDownItems.Add(new RibbonButton(strvalue[i]));
            SingleNotchRibbonButton.DropDownItems[0].Checked = true;
            strvalue = Channel.Default.Channel_FilterLoad("HighPass");
            HighPassRibbonButton.DropDownItems.Add(new RibbonButton("OFF"));
            for (int i = 0; i < strvalue.Length; i++)
                HighPassRibbonButton.DropDownItems.Add(new RibbonButton(strvalue[i]));
            HighPassRibbonButton.DropDownItems[0].Checked = true;
            strvalue = Channel.Default.Channel_FilterLoad("LowPass");
            LowPassRibbonButton.DropDownItems.Add(new RibbonButton("OFF"));
            for (int i = 0; i < strvalue.Length; i++)
                LowPassRibbonButton.DropDownItems.Add(new RibbonButton(strvalue[i]));
            LowPassRibbonButton.DropDownItems[0].Checked = true;
            LowPassRibbonButton.DropDownItemClicked += FliterRibbonButton_DropDownItemClicked;
            SingleNotchRibbonButton.DropDownItemClicked += FliterRibbonButton_DropDownItemClicked;
            HighPassRibbonButton.DropDownItemClicked += FliterRibbonButton_DropDownItemClicked;
            LoadHomePage();
            if (Channel.Default.SleepLock)
            {
                ribbonButton50.SmallImage = Properties.Resources.plock;
            }
            List<string> items = ChannelManage.Default.GetCfgNames();
            foreach (string s in items)
                this.ribbonComboBox1.DropDownItems.Add(new RibbonLabel() { Text = s });
            this.ribbonComboBox1.DropDownItemClicked += ribbonComboBox1_DropDownItemClicked;
            this.ribbonComboBox1.MouseLeave += ribbonComboBox1_MouseLeave;
            this.FormClosing += MainForm_FormClosing;
            Channel.Default.ConfigNameChanged += Default_ConfigNameChanged;
        }

        void ribbonComboBox1_MouseLeave(object sender, MouseEventArgs e)
        {
            if (ribbonComboBox1.DropDownVisible)
                ribbonComboBox1.DropDownClose();
        }

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
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Program.IsLogin)
                return;
            if (MessageBox.Show("是否确认退出系统？", "信息确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
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
            AddControls(arv);
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
                        ribbonPanel13.Visible =true;// ribbonPanel14.Visible = true;
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
                        ribbonPanel2.Visible = ribbonPanel12.Visible = true;
                        ribbonPanel2.Enabled = ribbonPanel12.Enabled = true;
                        ribbonButton6.Enabled = ribbonButton7.Enabled = ribbonButton12.Enabled = ribbonButton32.Enabled = true;
                        ribbonPanel13.Visible = true;
                        //ribbonPanel14.Visible = true;
                        ribbonItemGroup2.Visible = true;
                        //  ribbonItemGroup1.Visible = true;
                        ribbonItemGroup5.Visible = true;
                        ribbonItemGroup7.Visible = true;
                        ribbonItemGroup4.Visible = true;
                        //ribbonButton42.Visible = ribbonButton45.Visible = ribbonButton46.Visible = ribbonButton47.Visible = ribbonButton48.Visible = true;
                        ribbonButton49.Visible = true;
                        ribbonButton49.Enabled = true;
                        ribbonButton1.Visible = false;
                        break;
                    case DataModel.ProgressState.Compelet:
                        ribbonPanel2.Visible = ribbonPanel12.Visible = true;
                        ribbonPanel2.Enabled = ribbonPanel12.Enabled = true;
                        ribbonButton6.Enabled = !Channel.Default.ScoreLock;
                        ribbonButton7.Enabled = ribbonButton12.Enabled = ribbonButton32.Enabled = true;
                        ribbonPanel13.Visible =true; //ribbonPanel14.Visible = true;
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
                        ribbonButton12.Enabled = ribbonButton32.Enabled = false;
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
                ribbon1.Refresh();//该方法会强制控件重绘其自身及其所有子级。 这等效于将 Invalidate 方法设置为 true 并将该方法与 Update 结合使用
            }));
        }
        /// <summary>
        /// 加载数据项
        /// </summary>
        /// <param name="result"></param>
        /// <param name="Complete"></param>
        private void arv_LoadRecordHandle(DataModel.AnalysisResult result, DataModel.ProgressState Complete)
        {
            Channel.Default.AnlysisFinish = (Complete == DataModel.ProgressState.Temporary || Complete == DataModel.ProgressState.Compelet);
            Channel.Default.AnalysisReult = result;
            if (!Channel.Default.AnlysisFinish)
            {
                Channel.Default.ProgressStauts = DataModel.ProgressState.Ready;
                Channel.Default.RefreshHomeMenu();
            }
            else
            {
                ProcessRunning(5, "数据导入中...");
                {
                    List<DataBaseCom.Doc_EventRecords> reclist = result.EventRecords.FindAll(t => t.EventType == (int)pChart.IMarker.MarkType.LightOff || t.EventType == (int)pChart.IMarker.MarkType.LightOn);
                    bool lighton = false, lightoff = false;
                    for (int i = 0; i < reclist.Count; i++)
                    {
                        if (reclist[i].EventType == (int)pChart.IMarker.MarkType.LightOff)
                        {
                            Channel.Default.AnalysisReult.Sleep.LightOffTime = reclist[i].StartTime;
                            lightoff = true;
                        }
                        else if (reclist[i].EventType == (int)pChart.IMarker.MarkType.LightOn)
                        {
                            Channel.Default.AnalysisReult.Sleep.LightOnTime = reclist[i].StartTime;
                            lighton = true;
                        }
                    }
                    if (!lightoff)
                        Channel.Default.AnalysisReult.Sleep.LightOffTime = result.StartTime;
                    if (!lighton)
                        Channel.Default.AnalysisReult.Sleep.LightOnTime = result.EndTime;
                    Channel.Default.ScoreLock = Complete == DataModel.ProgressState.Compelet;
                    DataModel.EDF.Default.Interrupt = false;
                    DateTime dt = DateTime.Now;
                    byte[] dat = DataModel.EDF.Default.ReadFile(result.EdfPath);
                    Console.WriteLine(string.Format("读取EDF文件耗时【{0}ms】", (DateTime.Now - dt).TotalMilliseconds));
                    ProcessRunning(40, "数据导入中...");
                    if (!DataModel.EDF.Default.Interrupt && dat.Length != 0)
                    {
                        DataModel.EDF edf = DataModel.EDF.Default.ConvertToEDF(dat);
                        result.StartTime = edf.StartTime;
                        result.EndTime = edf.EndTime;
                        ProcessRunning(60, "数据导入中...");
                        //if (edf.PatientNO != Channel.Default.Patient.PatientNo)
                        //{
                        //    ///如果文件中的edf不匹配就退出本次导入，并修改记录状态
                        //    DataModel.DataBaseHelper.Default.Update(new DataBaseCom.Doc_MainViewRecord() { ID = Convert.ToInt32(Channel.Default.AnalysisReult.Tag) }, new DataBaseCom.Doc_MainViewRecord() { EdfPath = "", GUID = "", Progress = (int)DataModel.ProgressState.Ready });
                        //    AhDung.MessageTip.ShowWarning("加载的edf文件与要查看的病人信息不符合！");
                        //    return;
                        //}
                        edf.EdfPath = result.EdfPath;
                        if (!DataModel.EDF.Default.Interrupt)
                        {
                            if (Channel.Default.Patient == null)
                            {
                                Channel.Default.Patient = new DataBaseCom.Doc_PatientInfo();
                            }
                            Channel.Default.Patient.RecordTime = edf.StartTime;
                            Channel.Default.StartTime = edf.StartTime;
                            Channel.Default.AnlysisFinish = true;
                            FunctionControls.HistoryDataView hdv = new FunctionControls.HistoryDataView();
                            hdv.SleepStateChangedHandle += hdv_SleepStateChangedHandle;
                            hdv.BindData(edf);
                            ProcessRunning(70, "数据导入中...");
                            if (!DataModel.EDF.Default.Interrupt)
                            {
                                if (Channel.Default.AnalysisReult != null)
                                {
                                    if (Channel.Default.AnalysisReult.GUID == result.GUID)
                                    {
                                        result.HasDataChange = Channel.Default.AnalysisReult.HasDataChange;
                                    }
                                    else
                                        result.HasDataChange = true;
                                }
                                Channel.Default.AnalysisReult = result;
                                hdv.LoadAnalysisData(result);
                                ProcessRunning(95, "数据导入中...");
                                this.Invoke(new MethodInvoker(() =>
                                {
                                    hdv.GUID = result.GUID;
                                    hdv.MatchKey = edf.getMatchKey();
                                    AddControls(hdv);
                                }));
                                Channel.Default.ProgressStauts = Complete;
                                Channel.Default.RefreshHomeMenu();
                                ProcessRunning(100, "数据导入中...");
                            }
                        }
                        Console.WriteLine(string.Format("整个数据加载耗时【{0}ms】", (DateTime.Now - dt).TotalMilliseconds));
                    }
                }
            }

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
        /// 电池剩余容量
        /// </summary>
        public static float BatteryCapacityValue = 100;
        /// <summary>
        /// 电池剩余可监测时间
        /// </summary>
        public static int BatteryLastTime = 0;
        /// <summary>
        /// 绘制电池容量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BatteryCapacity_Paint(object sender, PaintEventArgs e)
        {
            using (Graphics g = e.Graphics)
            {
                int h = BatteryCapacity.Height;
                float wordSize = h * 3 / 4 - 1;
                using (Font f = new Font("宋体", 9))
                {
                    int y = h / 2 - f.Height / 2;
                    string strValue = string.Format("{0}%", BatteryCapacityValue);
                    g.DrawString(strValue, f, new SolidBrush(Color.White), 0, y);
                    int rX = (int)(g.MeasureString(strValue, f).Width + 2);
                    using (Pen p = new Pen(Color.White, 1))
                    {
                        Rectangle rect = new Rectangle(rX, y, 28, h - 8);
                        g.DrawRectangle(p, rect);
                        g.FillRectangle(BatteryCapacityValue <= 35 && BatteryCapacityValue > 15 ? Brushes.Yellow : BatteryCapacityValue <= 15 && BatteryCapacityValue > 0 ? Brushes.Red : Brushes.White, rect.X + 2, rect.Y + 2, (int)((rect.Width - 3) * (BatteryCapacityValue / 100.0)), rect.Height - 3);
                        g.DrawLine(p, new Point(rect.Right + 2, rect.Top + rect.Height / 2 - 3), new Point(rect.Right + 2, rect.Top + rect.Height / 2 + 3));
                    }
                }
                string tips = string.Format("可监测的时间剩余{0}时{1}分", BatteryLastTime / 60, BatteryLastTime % 60);
                if (BatteryCapacity.ToolTipText != tips)
                    BatteryCapacity.ToolTipText = tips;
            }
        }
        /// <summary>
        /// 防止拖拽的时候出现卡顿
        /// </summary>
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {///在当前控件内会导致panel1的效果出不来
        //        ///
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle = 0x02000000;////用双缓冲绘制窗口的所有子控件
        //        return cp;
        //    }
        //}
        /// <summary>
        /// 创建通道时触发
        /// </summary>
        /// <param name="item"></param>
        private void m_channel_Management_ChannelCreatingHandle(pChart.CurveItem item)
        {

        }
        /// <summary>
        /// 开始监测
        /// </summary>
        /// <param name="client"></param>
        /// <param name="channelconfig"></param>
        private void dev_StartMonitorHandler(IOClient client, DataTable channelconfig)
        {
            FunctionControls.RealDataView rdv = new FunctionControls.RealDataView();
            client.IOConnectHandle += client_IOConnectHandle;
            Channel.Default.CurrentChannelTable = channelconfig;
            for (int i = 0; i < channelconfig.Rows.Count; i++)
            {
                pChart.CurveItem item = Channel.Default.CreatChannel(channelconfig.Rows[i]);
                rdv.ChartArea.AddCurve(item);
            }
            if (rdv.Start(client, this.panel1))
            {
                AddControls(rdv);
            }
            else
            {
                rdv.ChartArea.Dispose();
            }
        }
        private void Default_OffLineHandler()
        {
            this.Invoke(new MethodInvoker(() =>
            {
                toolStripStatusLabel3.Text = "离线";
            }));
        }
        private void client_IOConnectHandle(bool result)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                toolStripStatusLabel3.Text = result ? "在线" : "离线";
            }));
        }
        private void Analysis_DoWork(ProgressTipForm sender, DoWorkEventArgs e)
        {
            FunctionControls.HistoryDataView hdv = sender.Argument as FunctionControls.HistoryDataView;
            string gguid = string.Format("{0}_{1}_{2}_{3}", DataModel.DataBaseHelper.Default.ComputeSHA256(hdv.EdfPath), Channel.Default.Patient.PatientNo, Channel.Default.Doctor.ID, (int)DataModel.ProgressState.Temporary);
            DataModel.RemoteDataInteraction.Default.GUID = gguid == hdv.GUID ? hdv.GUID : gguid;
            sender.SetProgress(2, "开启分析服务...");
            string filename = Path.GetFileName(hdv.EdfPath);
            bool exist = DataModel.RemoteDataInteraction.Default.upLoadDataReady();
            if (!DataModel.RemoteDataInteraction.Default.IsConected)
            {
                sender.SetError("无法开启分析服务");
                Channel.Default.ProgressStauts = DataModel.ProgressState.Replay;
                return;
            }
            if (sender.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            if (!exist)
            {
                sender.SetProgress(10, string.Format("开始导入{0}到分析中心...", filename));
                byte[] readbytes = hdv.getDataSource();
                if (sender.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                if (!DataModel.RemoteDataInteraction.Default.upLoadData(readbytes))
                {
                    sender.SetError("文件导入失败！");
                    return;
                }
                readbytes = new byte[0];
            }
            if (sender.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            sender.SetProgress(30, "导入用户标记事件...");
            /// DialogResult dr = ShowMessageYNC("是否需要启动自动判定？(是)表示先算法自动分析，(否)跳过算法自动分析，直接计算指标数据!");
            DateTime dt = DateTime.Now;
            //DataModel.RemoteDataInteraction.Default.AutoAnalysisEnable = dr == DialogResult.Yes;
            if (!DataModel.RemoteDataInteraction.Default.pushAnalysisResult(hdv.getAnalysisResult()))
            {
                sender.SetError("导入用户标记事件失败！");
                return;
            }
            string des2 = string.Format("上传数据耗时：{0}ms", (DateTime.Now - dt).TotalMilliseconds).ToString();
            Console.WriteLine(des2);
            DateTime dt2 = DateTime.Now;
            sender.SetProgress(60, "数据分析...");
            DataModel.AnalysisResult result = DataModel.RemoteDataInteraction.Default.getAnalysisResult(hdv.GUID);
            result.StartTime = Channel.Default.AnalysisReult.StartTime;
            result.EndTime = Channel.Default.AnalysisReult.EndTime;
            Channel.Default.AnalysisReult = result;
            if (sender.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            DataModel.DataBaseHelper.Default.SaveTemporaryResult(result);///临时分析结果保存到数据库
            sender.SetProgress(90, "加载分析后的数据...");
            hdv.LoadAnalysisData(Channel.Default.AnalysisReult, true);
            if (sender.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            Channel.Default.ProgressStauts = DataModel.ProgressState.Temporary;
            //sender.SetProgress(100, "分析完成");
            GC.Collect();
            Channel.Default.AnalysisReult.HasDataChange = false;
            Channel.Default.RefreshHomeMenu();
            string des = string.Format("取数据到加载耗时：{0}ms", (DateTime.Now - dt2).TotalMilliseconds).ToString();
            Console.WriteLine(des);
        }
        private void ribbonButton6_Click(object sender, EventArgs e)
        {
            RibbonButton rbt = (RibbonButton)sender;
            switch (rbt.Name)
            {
                case "ribbonButton2":
                case "ribbonButton1":
                    Block.PatientEdit patient = new Block.PatientEdit();
                    patient.StartPosition = FormStartPosition.CenterParent;
                    patient.SaveInfoHandle += patient_SaveInfoHandle;
                    patient.ShowDialog();
                    break;
                case "ribbonButton6":
                    if (rbt.Text == "开始分析")
                    {
                        if (Channel.Default.ProgressStauts == DataModel.ProgressState.Monitoring)
                        {
                            if (MessageBox.Show("当前操作会导致正在进行的任务终止，是否执行？", "信息确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                            {
                                return;
                            }
                        }
                        if (((int)Channel.Default.ProgressStauts) > 4)
                        {
                            ///直接上传平台进行分析
                            //AhDung.MessageTip.ShowWarning("无法连接到平台服务器！");
                            //return;
                            if (panel1.Controls.Count > 0)
                            {
                                if (panel1.Controls[0] is FunctionControls.HistoryDataView)
                                {
                                    FunctionControls.HistoryDataView hdv = (panel1.Controls[0] as FunctionControls.HistoryDataView);
                                    bool analysisenable = hdv.AnalysisEnable;
                                    if (!analysisenable)
                                    {
                                        AhDung.MessageTip.ShowWarning("无法分析：数据未满足自动分析的条件（采集时间>10分钟）！");
                                        return;
                                    }
                                    DialogResult dr = ShowMessageYNC();
                                    if (dr != DialogResult.Cancel)
                                    {
                                        ribbonButton6.Text = "停止分析";
                                        FunctionControls.tools.ProgressTipForm.Defalut.Text = "数据分析进度";
                                        FunctionControls.tools.ProgressTipForm.Defalut.Argument = hdv;
                                        FunctionControls.tools.ProgressTipForm.Defalut.DoWork += Analysis_DoWork;
                                        FunctionControls.tools.ProgressTipForm.Defalut.ShowDialog();
                                        ribbonButton6.Text = "开始分析";
                                    }
                                }
                            }
                        }
                        else
                        {
                            Block.ImportHistroyDialog ihd = new Block.ImportHistroyDialog();
                            if (Channel.Default.Patient != null)
                            {
                                ihd.ID = Channel.Default.Patient.PatientNo;
                                ihd.RecordTime = Channel.Default.StartTime;
                            }
                            if (ihd.ShowDialog() == DialogResult.OK)
                            {
                                Channel.Default.StartHistroyAnalysisByEDF(ihd.EdfPath);
                            }
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("是否要停止睡眠分析过程？", "信息确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            if (m_InterruptHandle != null)
                                m_InterruptHandle.Invoke(true);
                            DataModel.EDF.Default.Interrupt = true;
                            rbt.Text = "开始分析";
                        }
                    }
                    break;
                case "ribbonButton7":
                    if (!Channel.Default.AnalysisReult.HasDataChange)
                    {
                        if (MessageBox.Show("是否保存所有评分结果？", "信息确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            this.BeginInvoke(new MethodInvoker(() =>
                            {
                                Image img = Channel.Default.CreatMap();
                                img.Save(Channel.Default.Patient.ResultPhoto);
                                //img.Save(save2.FileName);
                                DataModel.DataBaseHelper.Default.SaveCompeletResult(Channel.Default.AnalysisReult.GUID);
                                AhDung.MessageTip.ShowOk("保存成功!");
                                Channel.Default.ProgressStauts = DataModel.ProgressState.Compelet;
                                Channel.Default.ScoreLock = true;
                                Channel.Default.SleepLock = true;
                                FreshHomeMenu();
                            }));
                        }
                    }
                    else
                    {
                        AhDung.MessageTip.ShowError("评分已被修改，保存前需重新“开始分析”!");
                    }
                    break;
                case "ribbonButton8":
                    if (MessageBox.Show("是否注销当前用户？", "信息确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Program.IsLogin = true;
                        this.Close();
                    }
                    break;
                case "ribbonButton9":
                    this.Close();
                    break;
                case "ribbonButton10":
                    Block.MarkerManagement mark = new Block.MarkerManagement();
                    mark.ShowDialog();
                    break;
                case "ribbonButton11":
                    Block.Montage channel = new Block.Montage();
                    channel.Owner = this.ParentForm;
                    channel.StartPosition = FormStartPosition.CenterParent;
                    channel.FilterColumnLoadHandle += Channel.Default.Channel_FilterLoad;//需写在init之前
                    channel.Init(Channel.Default.CurrentChannelTable.Rows.Count != 0 ? Channel.Default.CurrentChannelPath : Channel.Default.DefaultChannelPath);
                    channel.FilterDropDownHandle += Channel.Default.channel_FilterDropDownHandle;
                    channel.ShowDialog();
                    break;
                case "ribbonButton12":
                    Block.Moudle mutlMap = new Block.Moudle();
                    mutlMap.ControlBox = true;
                    mutlMap.MaximizeBox = true;
                    mutlMap.CanResize = true;
                    mutlMap.Text = "多谱图叠加";
                    mutlMap.Size = new Size(900, 1000);
                    MulReportChart pan = new MulReportChart();
                    pan.CurrentFrameChangedHandler += pan_CurrentFrameChangedHandler;
                    pan.Dock = DockStyle.Fill;
                    pan.LoadData();
                    mutlMap.Controls.Add(pan);
                    mutlMap.ShowDialog();
                    break;
                case "ribbonButton5":
                    Block.DoctorEdit doctor = new Block.DoctorEdit();
                    if (doctor.ShowDialog() == DialogResult.OK)
                    {
                        if (panel1.Controls.Count > 0)
                        {
                            if (panel1.Controls[0] is FunctionControls.AnalysisRecordView)
                            {
                                (panel1.Controls[0] as FunctionControls.AnalysisRecordView).RefreshData();
                            }
                        }
                    }
                    break;
                case "ribbonButton3":
                    OpenFileDialog open = new OpenFileDialog();
                    open.Filter = "文本文件|*.txt|数据文件|*.dat|所有文件|*.*";
                    open.RestoreDirectory = true;
                    open.FilterIndex = 1;
                    if (open.ShowDialog() == DialogResult.OK)
                    {

                    }
                    break;
                case "ribbonButton4":
                    SaveFileDialog save = new SaveFileDialog();
                    save.Filter = "文本文件|*.txt|C#文件|*.cs|所有文件|*.*";
                    save.RestoreDirectory = true;
                    save.FilterIndex = 1;
                    if (save.ShowDialog() == DialogResult.OK)
                    {

                    }
                    break;
                case "ribbonButton30":
                    if (System.IO.File.Exists(Application.StartupPath + "\\help.pdf"))
                    {
                        PdfViewDialog pdfview = new PdfViewDialog();
                        pdfview.Show();
                    }
                    else if (System.IO.File.Exists(Application.StartupPath + "\\help.chm"))
                        System.Diagnostics.Process.Start(Application.StartupPath + "\\help.chm");
                    else if (System.IO.File.Exists(Application.StartupPath + "\\help.doc"))
                        System.Diagnostics.Process.Start(Application.StartupPath + "\\help.doc");
                    else if (System.IO.File.Exists(Application.StartupPath + "\\help.docx"))
                        System.Diagnostics.Process.Start(Application.StartupPath + "\\help.docx");
                    else
                        AhDung.MessageTip.ShowError("未找到操作手册说明书");
                    break;
                case "ribbonButton31":
                    FunctionControls.About about = new FunctionControls.About();
                    about.ShowDialog();
                    break;
                case "ribbonButton32":
                    string rpath = Channel.Default.SystemSetting.ReportPath; ;
                    string fname = string.Format("{1}({2})的睡眠分析报告{0}--{3}", Channel.Default.StartTime.ToString("yyyyMMddHHmmss"), Channel.Default.Patient.PatientName, Channel.Default.Patient.PatientNo, Channel.Default.Doctor.Name);
                    string rpt = Channel.Default.SystemSetting.ReportType.ToLower();
                    int findex = rpt == "xps" ? 1 : rpt == "pdf" ? 2 : 3;
                    if (rpath == "" || !Directory.Exists(rpath))
                    {
                        SaveFileDialog report = new SaveFileDialog();
                        report.Filter = "Xps|*.xps|Pdf|*.pdf|Word|*.doc";
                        report.RestoreDirectory = true;
                        report.FilterIndex = findex;
                        report.FileName = fname;
                        if (report.ShowDialog() == DialogResult.OK)
                        {
                            rpath = report.FileName;
                            {
                                findex = report.FilterIndex;
                            }
                        }
                        else
                            return;
                    }
                    else
                    {
                        rpath = string.Format("{0}\\{3:yyyy-MM-dd}\\{1}.{2}", rpath, fname, Channel.Default.SystemSetting.ReportType.ToLower(), Channel.Default.StartTime);
                    }
                    FunctionControls.HistoryDataView hvd = null;
                    if (Channel.Default.SystemSetting.AnalysisBeforeReport)
                    {
                        DialogResult dr2 = ShowMessageYNC();
                        if (dr2 == DialogResult.Cancel)
                        {
                            return;
                        }
                        hvd = (panel1.Controls[0] as FunctionControls.HistoryDataView);
                    }
                    FunctionControls.tools.ProgressTipForm.Defalut.Text = "生成睡眠分析报告";
                    FunctionControls.tools.ProgressTipForm.Defalut.Argument = new object[] { rpath, findex, hvd };
                    FunctionControls.tools.ProgressTipForm.Defalut.DoWork += ImportReport_DoWork;
                    FunctionControls.tools.ProgressTipForm.Defalut.ShowDialog();
                    break;
                case "ribbonButton49":
                    LoadHomePage();
                    break;
            }
        }
        private void ImportReport_DoWork(ProgressTipForm sender, DoWorkEventArgs e)
        {
            object[] args = sender.Argument as object[];
            int findex = Convert.ToInt32(args[1]);
            string rpath = Convert.ToString(args[0]);
            int offset = 0;
            if (args[2] != null)
            {
                FunctionControls.HistoryDataView hdv = args[2] as FunctionControls.HistoryDataView;
                string gguid = string.Format("{0}_{1}_{2}_{3}", DataModel.DataBaseHelper.Default.ComputeSHA256(hdv.EdfPath), Channel.Default.Patient.PatientNo, Channel.Default.Doctor.ID, (int)DataModel.ProgressState.Temporary);
                DataModel.RemoteDataInteraction.Default.GUID = gguid == hdv.GUID ? hdv.GUID : gguid;
                sender.SetProgress(1, "开启分析服务...");
                string filename = Path.GetFileName(hdv.EdfPath);
                bool exist = DataModel.RemoteDataInteraction.Default.upLoadDataReady();
                if (!DataModel.RemoteDataInteraction.Default.IsConected)
                {
                    sender.SetError("无法开启分析服务");
                    Channel.Default.ProgressStauts = DataModel.ProgressState.Replay;
                    return;
                }
                if (sender.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                if (!exist)
                {
                    sender.SetProgress(5, string.Format("开始导入{0}到分析中心...", filename));
                    byte[] readbytes = hdv.getDataSource();
                    if (sender.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                    if (!DataModel.RemoteDataInteraction.Default.upLoadData(readbytes))
                    {
                        sender.SetError("文件导入失败！");
                        return;
                    }
                    readbytes = new byte[0];
                }
                if (sender.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                sender.SetProgress(15, "导入用户标记事件...");
                if (!DataModel.RemoteDataInteraction.Default.pushAnalysisResult(hdv.getAnalysisResult()))
                {
                    sender.SetError("导入用户标记事件失败！");
                    return;
                }
                sender.SetProgress(30, "分析数据...");
                DataModel.AnalysisResult result = DataModel.RemoteDataInteraction.Default.getAnalysisResult(hdv.GUID);
                result.StartTime = Channel.Default.AnalysisReult.StartTime;
                result.EndTime = Channel.Default.AnalysisReult.EndTime;
                Channel.Default.AnalysisReult = result;
                if (sender.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                sender.SetProgress(40, "提取数据...");
                DataModel.DataBaseHelper.Default.SaveTemporaryResult(result);///临时分析结果保存到数据库
                offset = 40;
            }
            sender.SetProgress(2 + offset, "生成报告基础信息...");
            if (Channel.Default.Patient == null)
            {
                Channel.Default.Patient = new DataBaseCom.Doc_PatientInfo();
            }
            Channel.Default.Patient.DoctorName = Channel.Default.Doctor.Name;
            Channel.Default.Patient.CurrentDateTime = DateTime.Now;
            ReportHelper aph = new ReportHelper();
            aph.CreateNewWordDocument(Channel.Default.ReportTemplatePath);
            ReportHelper.DocumentFormat format = ReportHelper.DocumentFormat.Pdf;
            switch (findex)
            {
                case 1:
                    format = ReportHelper.DocumentFormat.Xps;
                    break;
                case 2:
                    format = ReportHelper.DocumentFormat.Pdf;
                    break;
                case 3:
                    format = ReportHelper.DocumentFormat.Doc;
                    break;
            }
            Assembly asse = Assembly.Load("AwareTec.Polysmith.DataBaseCom");
            var classs = asse.GetTypes();
            List<string> des = new List<string>();
            List<string> values = new List<string>();
            PropertyInfo[] funs = typeof(DataModel.AnalysisResult).GetProperties();
            sender.SetProgress(10 + offset, "生成报告数据信息...");
            for (int i = 0; i < classs.Length; i++)
            {
                Type instance = classs[i];
                object o = null;
                if (instance == typeof(DataBaseCom.Doc_PatientInfo))
                {
                    o = Channel.Default.Patient;
                }
                else if (instance == typeof(DataBaseCom.Doc_SleepResult))
                {
                    Channel.Default.AnalysisReult.Sleep.StartRecordTime = Channel.Default.StartTime;
                    Channel.Default.AnalysisReult.Sleep.EndRecordTime = Channel.Default.EndTime;
                    o = Channel.Default.AnalysisReult.Sleep;
                }
                else
                {
                    if (!instance.IsAbstract && !instance.ContainsGenericParameters && instance.FullName.Contains("Doc_"))
                    {
                        try
                        {
                            PropertyInfo ss = funs.First(t => t.PropertyType.FullName == instance.FullName);
                            if (ss != null)
                            {
                                o = ss.GetValue(Channel.Default.AnalysisReult);
                            }
                        }
                        catch { }
                    }
                }
                if (sender.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                if (o != null)
                {
                    PropertyInfo[] mi = instance.GetProperties();
                    for (int j = 0; j < mi.Length; j++)
                    {
                        des.Add(mi[j].Name);
                        object value = mi[j].GetValue(o, null);
                        values.Add(value != null ? value.ToString() : "");
                    }
                }
            }
            sender.SetProgress(20 + offset, "生成报告图样信息...");
            Image img = Channel.Default.CreatMap();
            img.Save(Channel.Default.Patient.ResultPhoto);
            sender.SetProgress(70, "正在保存报告文件...");
            bool complete = false;
            int per = 70;
            aph.SaveAs(des, values, rpath, format, new Action<bool>((t) =>
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
                }
                else
                {
                    per = -1;
           
                }
            }));
            while (!complete)
            {
                if (per == 80)
                {
                    sender.SetProgress(80, "报告保存成功！");
                }
                else if (per == 90)
                {
                    sender.SetProgress(90, "正在打开报告...");
                }
                else if (per == 100)
                {
                    sender.SetProgress(100, "完成");
                    break;
                }
                else if (per == -1)
                {
                    sender.SetProgress(100, "");
                    sender.SetError("保存失败！");
                    break;
                }
                System.Threading.Thread.Sleep(50);
            }
        }
        private void pan_CurrentFrameChangedHandler(int frameCnt)
        {
            if (panel1.Controls.Count > 0)
            {
                if (panel1.Controls[0] is FunctionControls.HistoryDataView)
                {
                    (panel1.Controls[0] as FunctionControls.HistoryDataView).ChangeFrameNo(frameCnt);
                }
            }
        }

        private void patient_NextStepHandle(DataBaseCom.Doc_PatientInfo info, bool showBefore = false)
        {
            Block.NewDevice dev = new Block.NewDevice();
            dev.Owner = this;
            dev.Init(info, showBefore);
            dev.StartMonitorHandler += dev_StartMonitorHandler;
            dev.PatientEditingHandle += dev_PatientEditingHandle;
            dev.ShowDialog();
        }
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
        /// <summary>
        /// 开始分析
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isauto"></param>
        private void ihd_StartHistroyAnalysisByEDFHandle(string path, bool isauto = false, string guid = "")
        {
            ribbonButton6.Text = "停止分析";
            FunctionControls.tools.ProgressTipForm.Defalut.Text = "数据分析进度";
            FunctionControls.tools.ProgressTipForm.Defalut.Argument = new object[] { path, isauto, guid };
            FunctionControls.tools.ProgressTipForm.Defalut.DoWork += Defalut_DoWork;
            if (FunctionControls.tools.ProgressTipForm.Defalut.ShowDialog() != DialogResult.OK)
            {

            }
            ribbonButton6.Text = "开始分析";
        }
        #region 异步线程对话框置顶写法
        private bool ShowMessage(string msg)
        {
            object ss = this.Invoke(new MessageBoxShow(MessageBoxShow_F), new object[] { msg });
            return Convert.ToBoolean(ss);
        }
        delegate bool MessageBoxShow(string msg);
        bool MessageBoxShow_F(string msg)
        {
            return MessageBox.Show(msg, "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes;
        }
        private DialogResult ShowMessageYNC(string msg)
        {
            object ss = this.Invoke(new MessageBoxShowYNC(MessageBoxShow_YNC), new object[] { msg });
            return (DialogResult)ss;
        }
        delegate DialogResult MessageBoxShowYNC(string msg);
        DialogResult MessageBoxShow_YNC(string msg)
        {
            return MessageBox.Show(msg, "信息提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
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
        #endregion
        private void Defalut_DoWork(ProgressTipForm sender, DoWorkEventArgs e)
        {
            object[] param = sender.Argument as object[];
            if (param.Length != 3)
                return;
            string path = param[0] as string;
            string lguid = param[2] as string;
            bool isauto = Convert.ToBoolean(param[1]);
            {
                DataModel.EDF.Default.Interrupt = false;
                string filename = Path.GetFileName(path);
                sender.SetProgress(5, string.Format("加载数据源:{0} ...", filename));
                byte[] dat = DataModel.EDF.Default.ReadFile(path);
                if (sender.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                if (!DataModel.EDF.Default.Interrupt && dat.Length != 0)
                {
                    DateTime recordTime_Edf = DataModel.EDF.Default.getRecordTime(dat);
                    string fpath = string.Format("{2}\\{0:yyyy-MM-dd}\\{1}", recordTime_Edf, filename, DataModel.EDF.Default.DefaultEdfSavePath);
                    bool ready = DataModel.ApiCopyFile.DoCopy(path, fpath);
                    DataModel.EDF edf = DataModel.EDF.Default.ConvertToEDF(dat);
                    if (sender.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                    bool analysisenable = (edf.EndTime-edf.StartTime).TotalMinutes>=10;
                    edf.EdfPath = ready ? fpath : path;
                    //if (edf.getMatchKey()==)
                    //{
                    //    DataModel.DataBaseHelper.Default.Update(new DataBaseCom.Doc_MainViewRecord() { ID = Convert.ToInt32(Channel.Default.AnalysisReult.Tag) }, new DataBaseCom.Doc_MainViewRecord() { EdfPath = "", GUID = "", Progress = (int)DataModel.ProgressState.Ready });
                    //    AhDung.MessageTip.ShowWarning("加载的edf文件与要查看的病人信息不符合！");
                    //}
                    //else
                    {
                        if (!DataModel.EDF.Default.Interrupt)
                        {
                            if (!isauto) ClearControls();
                            if (Channel.Default.Patient == null)
                            {
                                Channel.Default.Patient = new DataBaseCom.Doc_PatientInfo();
                            }
                            Channel.Default.Patient.RecordTime = edf.StartTime;
                            Channel.Default.StartTime = edf.StartTime;
                            FunctionControls.HistoryDataView hdv = new FunctionControls.HistoryDataView();
                            hdv.SleepStateChangedHandle += hdv_SleepStateChangedHandle;
                            this.Invoke(new MethodInvoker(() =>
                            {
                                hdv.GUID = string.Format("{0}_{1}_{2}_{3}", DataModel.DataBaseHelper.Default.ComputeSHA256(path), Channel.Default.Patient.PatientNo, Channel.Default.Doctor.ID, (int)DataModel.ProgressState.Temporary);///实时监控时点击开始分析，医生信息是为空，待修正
                                hdv.MatchKey = edf.getMatchKey();
                                RemoveControls(hdv);
                            }));
                            DialogResult dr = DialogResult.Yes;
                            if (analysisenable)
                            {
                                dr = ShowMessageYNC("是否启动平台自动分析？");
                                if (dr == DialogResult.Cancel)
                                {
                                    if (hdv.GUID != lguid)///导入edf的guid与正常记录的有所不同，其guid不是由哈希值组成
                                        DataModel.DataBaseHelper.Default.Delete(new DataBaseCom.Doc_MainViewRecord() { ID = Convert.ToInt32(Channel.Default.AnalysisReult.Tag) });
                                    sender.SetError("取消本次操作...");
                                    e.Cancel = true;
                                    return;
                                }
                            }
                            if (isauto ? true : dr == DialogResult.Yes)
                            {
                                if (analysisenable)
                                {
                                    DataModel.RemoteDataInteraction.Default.GUID = hdv.GUID;
                                    sender.SetProgress(20, "开启分析服务...");
                                    bool exist = DataModel.RemoteDataInteraction.Default.upLoadDataReady();
                                    if (!DataModel.RemoteDataInteraction.Default.IsConected)
                                    {
                                        sender.SetError("无法开启分析服务");
                                        return;
                                    }
                                    if (sender.CancellationPending)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }
                                    if (!exist)
                                    {
                                        sender.SetProgress(30, string.Format("开始导入{0}到分析中心...", filename));
                                        if (!DataModel.RemoteDataInteraction.Default.upLoadData(dat))
                                        {
                                            sender.SetError("文件导入失败！");
                                            return;
                                        }
                                    }
                                    if (sender.CancellationPending)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }
                                    sender.SetProgress(50, "数据分析...");
                                    if (lguid != "")
                                        hdv.GUID = lguid;
                                    object tag = Channel.Default.AnalysisReult.Tag;
                                    Channel.Default.AnalysisReult = DataModel.RemoteDataInteraction.Default.getAnalysisResult(hdv.GUID);
                                    Channel.Default.AnalysisReult.Tag = tag;
                                    Channel.Default.AnalysisReult.EndTime = edf.EndTime;
                                    Channel.Default.AnalysisReult.StartTime = edf.StartTime;
                                    if (sender.CancellationPending)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }
                                    DataModel.DataBaseHelper.Default.SaveTemporaryResult(Channel.Default.AnalysisReult);///临时分析结果保存到数据库
                                }
                                Channel.Default.AnlysisFinish = true;
                                sender.SetProgress(80, "加载界面...");
                                hdv.BindData(edf);
                                if (sender.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }
                                if (DataModel.RemoteDataInteraction.Default.IsConected && analysisenable)
                                {
                                    sender.SetProgress(90, "加载界面...");
                                    hdv.LoadAnalysisData(Channel.Default.AnalysisReult, true);
                                    if (sender.CancellationPending)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }
                                    Channel.Default.ProgressStauts = DataModel.ProgressState.Temporary;
                                }
                                else
                                {
                                    Channel.Default.ProgressStauts = DataModel.ProgressState.Replay;
                                }
                                Channel.Default.AnalysisReult.HasDataChange = false;
                                if (!DataModel.EDF.Default.Interrupt)
                                {
                                    this.Invoke(new MethodInvoker(() =>
                                    {
                                        AddControls(hdv);
                                    }));
                                }
                            }
                            else
                            {
                                if (analysisenable)
                                {
                                    DataModel.RemoteDataInteraction.Default.GUID = hdv.GUID;
                                    sender.SetProgress(20, string.Format("加载数据源:{0} ...", filename));
                                    bool exist = DataModel.RemoteDataInteraction.Default.upLoadDataReady();
                                    if (!DataModel.RemoteDataInteraction.Default.IsConected)
                                    {
                                        sender.SetError("文件加载失败！");
                                        return;
                                    }
                                    if (sender.CancellationPending)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }
                                    if (!exist)
                                    {
                                        sender.SetProgress(30, string.Format("加载数据源:{0} ...", filename));
                                        if (!DataModel.RemoteDataInteraction.Default.upLoadData(dat))
                                        {
                                            sender.SetError("文件加载失败！");
                                            return;
                                        }
                                    }
                                    if (sender.CancellationPending)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }
                                    sender.SetProgress(50, string.Format("加载数据源:{0} ...", filename));
                                    if (lguid != "")
                                        hdv.GUID = lguid;
                                    object tag = Channel.Default.AnalysisReult.Tag;
                                    DataModel.RemoteDataInteraction.Default.AutoAnalysisEnable = false;
                                    int totals = (int)(edf.EndTime - edf.StartTime).TotalSeconds;
                                    int frcn = totals % 30 == 0 ? totals / 30 : (totals / 30) + 1;
                                    if (!DataModel.RemoteDataInteraction.Default.pushAnalysisResult(hdv.getEmptyAnalysisResult(frcn)))
                                    {
                                        sender.SetError("导入用户标记事件失败！");
                                        return;
                                    }
                                    System.Threading.Thread.Sleep(1000);
                                    Channel.Default.AnalysisReult = DataModel.RemoteDataInteraction.Default.getAnalysisResult(hdv.GUID, false);
                                    Channel.Default.AnalysisReult.Tag = tag;
                                    Channel.Default.AnalysisReult.Sleep.LightOffTime = edf.StartTime;
                                    Channel.Default.AnalysisReult.Sleep.LightOnTime = edf.EndTime;
                                    Channel.Default.AnalysisReult.EndTime = edf.EndTime;
                                    Channel.Default.AnalysisReult.StartTime = edf.StartTime;
                                    if (sender.CancellationPending)
                                    {
                                        e.Cancel = true;
                                        return;
                                    }
                                    DataModel.DataBaseHelper.Default.SaveTemporaryResult(Channel.Default.AnalysisReult);///临时分析结果保存到数据库
                                }
                                Channel.Default.AnlysisFinish = true;
                                sender.SetProgress(70, "加载界面...");
                                hdv.BindData(edf);
                                if (sender.CancellationPending)
                                {
                                    e.Cancel = true;
                                    DataModel.EDF.Default.Interrupt = true;
                                    return;
                                }
                                sender.SetProgress(80, "加载界面...");
                                hdv.LoadAnalysisData(Channel.Default.AnalysisReult, true);
                                if (sender.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }
                                sender.SetProgress(90, "加载界面...");
                                if (!DataModel.EDF.Default.Interrupt)
                                {
                                    this.Invoke(new MethodInvoker(() =>
                                    {
                                        AddControls(hdv);
                                    }));
                                }
                                Channel.Default.ProgressStauts = DataModel.ProgressState.Temporary;
                            }
                            DataModel.DataBaseHelper.Default.Update(new DataBaseCom.Doc_MainViewRecord() { ID = Convert.ToInt32(Channel.Default.AnalysisReult.Tag) }, new DataBaseCom.Doc_MainViewRecord() { DoctorID = Channel.Default.Doctor.ID.ToString(), EdfPath = edf.EdfPath, GUID = hdv.GUID, ReportReady = false, RecordTime = edf.StartTime, Progress = (int)DataModel.ProgressState.Temporary, FrameCount = hdv.TotalFrameCnt });
                            Channel.Default.RefreshHomeMenu();
                            sender.SetProgress(100, "完成");

                        }
                    }
                }
                dat = new byte[0];
            }
        }

        /// <summary>
        /// 更新睡眠分期
        /// </summary>
        /// <param name="state"></param>
        private void hdv_SleepStateChangedHandle(string[] state)
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
                    hdv_SleepStateChangedHandle(state);
                }));
            }
            else
            {
                this.Invoke(new MethodInvoker(() =>
                {
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
                }));
            }
        }
        private void ProcessRunning()
        {

        }
        /// <summary>
        /// 病人信息详细编辑界面
        /// </summary>
        /// <param name="info"></param>
        private void dev_PatientEditingHandle(DataBaseCom.Doc_PatientInfo info)
        {
            Block.PatientEdit patient = new Block.PatientEdit();
            patient.Initialize(info, false);
            patient.Owner = this.ParentForm;
            patient.StartPosition = FormStartPosition.CenterParent;
            //patient.SaveInfoHandle += patient_SaveInfoHandle;
            patient.ShowDialog();
        }
        /// <summary>
        /// 保存病例信息
        /// </summary>
        /// <param name="info"></param>
        internal bool patient_SaveInfoHandle(DataBaseCom.Doc_PatientInfo info)
        {
            if (!DataModel.DataBaseHelper.Default.Exsit(new DataBaseCom.Doc_PatientInfo() { PatientNo = info.PatientNo }))
            {
                if (ShowMessage("档案中不存在该病例信息，是否需要创建？"))
                {
                    DataModel.DataBaseHelper.Default.Insert(info);
                    DataBaseCom.Doc_MainViewRecord mvr = new DataBaseCom.Doc_MainViewRecord() { PatientID = info.PatientNo, Progress = (int)DataModel.ProgressState.None };
                    if (!DataModel.DataBaseHelper.Default.Exsit(mvr))
                    {
                        mvr.RecordTime = DateTime.Now;
                        mvr.DoctorID = "1";
                        mvr.LoginID = Channel.Default.Loginer.ID;
                        DataModel.DataBaseHelper.Default.Insert(mvr);
                    }
                    this.Invoke(new MethodInvoker(() =>
                    {
                        if (panel1.Controls.Count > 0)
                        {
                            if (panel1.Controls[0] is FunctionControls.AnalysisRecordView)
                            {
                                (panel1.Controls[0] as FunctionControls.AnalysisRecordView).RefreshData();
                            }
                        }
                    }));
                    return true;
                }
            }
            else
            {
                if (DataModel.DataBaseHelper.Default.Update(new DataBaseCom.Doc_PatientInfo() { PatientNo = info.PatientNo }, info))
                {
                    AhDung.MessageTip.ShowOk("病例信息修改成功!");
                    this.Invoke(new MethodInvoker(() =>
                    {
                        if (panel1.Controls.Count > 0)
                        {
                            if (panel1.Controls[0] is FunctionControls.AnalysisRecordView)
                            {
                                (panel1.Controls[0] as FunctionControls.AnalysisRecordView).ChangePatient(info);
                            }
                        }
                    }));
                    return true;
                }
            }
            return false;
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
                foreach (Control c in this.panel1.Controls)
                {
                    if (c.GetType() == t.GetType())
                        return;
                    c.Dispose();
                }
                t.Dock = DockStyle.Fill;
                t.BringToFront();
                panel1.Controls.Add(t);
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
            if (panel1.Controls.Count > 0)
            {
                if (panel1.Controls[0] is FunctionControls.HistoryDataView)
                {
                    (panel1.Controls[0] as FunctionControls.HistoryDataView).Execute(typ, true);
                }
                //else if (panel1.Controls[0] is FunctionControls.AnalysisRecordView)
                //{
                //    (panel1.Controls[0] as FunctionControls.AnalysisRecordView).pKeyDown(typ);
                //}
            }
        }
        private void ribbonButtonFrameRun_Click(object sender, EventArgs e)
        {
            int typ = 9;
            if (ribbonButtonFrameRun.Value == "4")
            {
                ribbonButtonFrameRun.SmallImage = Properties.Resources.fStop;
                typ = 4;
                ribbonButtonFrameRun.Value = "5";
            }
            else
            {
                ribbonButtonFrameRun.SmallImage = Properties.Resources.fRun;
                typ = 5;
                ribbonButtonFrameRun.Value = "4";
            }
            if (panel1.Controls.Count > 0)
            {
                if (panel1.Controls[0] is FunctionControls.HistoryDataView)
                {
                    (panel1.Controls[0] as FunctionControls.HistoryDataView).Execute(typ);
                }
            }
        }
        #endregion
        private RibbonButton bak_butt;
        private void BaseTimeChange_Click(object sender, EventArgs e)
        {
            RibbonButton rbt = sender as RibbonButton;
            if (rbt.Text != bak_butt.Text)
            {
                bak_butt.Checked = false;
                string strValue = rbt.Text.ToUpper();
                if (strValue.Contains("S"))
                {
                    Channel.Default.BaseTimeLineSpan = int.Parse(strValue.Replace("S", ""));
                }
                else if (strValue.Contains("MIN"))
                {
                    Channel.Default.BaseTimeLineSpan = int.Parse(strValue.Replace("MIN", "")) * 60;
                }
                rbt.Checked = true;
                bak_butt = rbt;
                Channel.Default.ChannelChanged();
            }
        }

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
                case "ribbonButton50":
                    if (Channel.Default.ScoreLock)
                    {
                        AhDung.MessageTip.ShowWarning("需要回到首页解锁回放记录进度状态为待完成");
                        return;
                    }
                    if (Channel.Default.SleepLock)
                    {
                        Channel.Default.SleepLock = false;
                        ribbonButton50.SmallImage = Properties.Resources.unplock;
                    }
                    else
                    {
                        Channel.Default.SleepLock = true;
                        ribbonButton50.SmallImage = Properties.Resources.plock;
                    }
                    return;
            }
            if (!Channel.Default.SleepLock && !Channel.Default.ScoreLock)
            {
                if (panel1.Controls.Count > 0)
                {
                    if (panel1.Controls[0] is FunctionControls.HistoryDataView)
                    {
                        (panel1.Controls[0] as FunctionControls.HistoryDataView).MarkSleep(value);
                    }
                }
            }
            else
            {
                AhDung.MessageTip.ShowWarning(Channel.Default.SleepLock ? "请先解开睡眠分期使能锁！" : Channel.Default.ScoreLock ? "评分已结束，如需重新评分，请先解锁评分状态！" : "无权限");
            }
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
                    int times=0;
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
}
