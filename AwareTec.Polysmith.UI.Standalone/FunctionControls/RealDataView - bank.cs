using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using pSystem.Communication.Com;
using AwareTec.Polysmith.Util;
using AwareTec.Polysmith.Protocol;
using AwareTec.Polysmith.pChart;
using AwareTec.Polysmith.UI.DataModel;
using System.Diagnostics;
using AwareTec.Polysmith.DataBaseCom;
using AwareTec.Polysmith.DataCenter;
using AwareTec.Polysmith.UI.FunctionControls.tools;
using AwareTec.Polysmith.Vedio;
using System.Media;
using iTextSharp.text.pdf;

namespace AwareTec.Polysmith.UI.FunctionControls
{
    public partial class RealDataView : UserControl
    {
        #region 私有成员
        private Protocol.ProtocolServer m_Protocol = null;
        private MainForm m_parent = null;
        private List<Doc_CalibrationDefine> m_CalibrationDefine = null;
        private bool m_isKillTask = false;
        private bool m_hasVedio = false;
        private SoundPlayer soundplayer = new SoundPlayer();
        private bool issettime = false;
        /// <summary>
        /// 如果是新建病例下一步选择设备直接开始检测，在连接过程中失败，不会删除首页记录中该记录信息
        /// </summary>
        private bool isnewpatient = false;
        private string devName = "";
        #endregion
        #region 公有成员
        public Protocol.ProtocolServer Protocol = null;
        public bool isreconnection = false;
        public bool hasvideo = false;
        #endregion
        #region 构造函数
        public RealDataView()
        {
            InitializeComponent();
            this.Load += RealDataView_Load;
            this.Disposed += RealDataView_Disposed;
            m_viewResult = new ViewResult();
            this.Resize += RealDataView_Resize;
            Channel.Default.BaseTimeLineSpan = 30;
        }
        #endregion

        #region 窗体触发事件实现方法

        private void RealDataView_Resize(object sender, EventArgs e)
        {
            label3.Location = new Point((DingBiaoButton.Location.X - 50) / 2 - (int)label3.CreateGraphics().MeasureString(label3.Text, label3.Font).Width / 2, label3.Location.Y);
        }

        private void RealDataView_Disposed(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            Channel.Default.ChannelChangedHandle -= ChangedView;
            Channel.Default.CalibrationChangedViewHandle -= Default_CalibrationChangedViewHandle;
            SetInfo(false);
            if (!m_isKillTask)
            {
                Task.Factory.StartNew(() =>
                {
                    DataModel.DeviceOnLine.Default.UpdateDevices(PortName, DeviceState.OffLine);
                    EDF.Default.Reset();
                    m_Protocol.FnStop = true;
                    m_Protocol.Dispose();
                    Channel.Default.ProgressStauts = ProgressState.None;
                    Channel.Default.EndTime = DateTime.Now;
                    m_isKillTask = true;
                });
            }
            ChartArea.Dispose();
            Channel.Default.IsRealTimeView = false;
            //if (m_vPlayer != null)
            //{
            //    m_vPlayer.CloseAll();
            //    play.Close();
            //}
        }

        private void RealDataView_Load(object sender, EventArgs e)
        {
            ChartArea.DrawImageBeforeHandle += ChartArea_DrawImageBeforeHandle;
            ChartArea.ChannelViewPopupHandler += ChartArea_ChannelViewPopupHandler;
            ChartArea.ChannelHeadPopupHandler += ChartArea_ChannelHeadPopupHandler;
            ChartArea.ChannelSortedHandle += ChartArea_ChannelSortedHandle;
            ChartArea.BaseTimeLineSpan = Channel.Default.BaseTimeLineSpan;
            ChartArea.SelectedHappenHandler += ChartArea_SelectedHappenHandler;
            ChartArea.ShowOrHideChannelHandler += ChartArea_ShowOrHideChannelHandler;
            Channel.Default.CalibrationChangedViewHandle += Default_CalibrationChangedViewHandle;
            this.MouseWheel += RealDataView_MouseWheel;
            m_parent = this.ParentForm as MainForm;
            Channel.Default.ChannelChangedHandle += ChangedView;
            timer1.Enabled = true;
            timer1.Interval = 1000;
            Channel.Default.AllowShowDoctor = false;
            if (!_initButtonReady)
            {
                Channel.Default.ProgressStauts = ProgressState.None;
                Channel.Default.RefreshHomeMenu();
            }
            SetInfo();
            label3.Text = string.Format(Program.Language=="EN"? "{1}({0}) {3} {2} years old {4}" : "{1}({0}) {3} {2}岁 {4}", Channel.Default.Patient.PatientNo, Channel.Default.Patient.PatientName, Channel.Default.Patient.Age, Channel.Default.Patient.Gender, devName);
            if(isreconnection)
                label3.Text = string.Format(Program.Language == "EN" ? "{1}({0}) {3} {2}Years Old  {5}  @Start：{4}" : "{1}({0}) {3} {2}岁 {5}  @开始：{4}", Channel.Default.Patient.PatientNo, Channel.Default.Patient.PatientName, Channel.Default.Patient.Age, Channel.Default.Patient.Gender, m_MainViewRecord.RecordTime, devName);
            m_CalibrationDefine = DataModel.DataBaseHelper.Default.SelectAll(new Doc_CalibrationDefine());
            Channel.Default.IsRealTimeView = true;

            if (Channel.Default.SystemSetting.Reserve2 != "1")
                m_hasVedio = Channel.Default.SystemSetting.VedioSourceUrl != "" && !Program.LocationScan;
            else
                m_hasVedio = Channel.Default.SystemSetting.VedioSourceUrl != "";

            hasvideo = m_hasVedio;
            if(m_hasVedio)
            {
                ZkTestButton.Visible = false;
                SetTimeButton.Location = DingBiaoButton.Location;
                SetTimeButton.Visible = false;
                DingBiaoButton.Location = ZkTestButton.Location;
            }
        }

        private void ChartArea_ShowOrHideChannelHandler(CurveItem curveItem)
        {
            DataRow[] dr = Channel.Default.CurrentChannelTable.Select(string.Format("ID='{0}'", curveItem.ID));
            if (dr.Length != 1)
                throw new Exception("数据似乎有些错误");

            bool isVisible = Convert.ToBoolean(dr[0]["State"]);
            dr[0]["State"] = !isVisible;
            Channel.Default.ChannelChanged();
        }

        private void RealDataView_MouseWheel(object sender, MouseEventArgs e)
        {
            CurveItem selected = ChartArea.ItemMouseOnHead();
            if (selected != null)
            {
                if (selected.IsShowValue)
                    return;
                float max = selected.PixelRate;
                DataTable dt = Channel.Default.CurrentChannelTable;
                DataRow[] dr = dt.Select(string.Format("ID='{0}'", selected.ID));
                if (dr.Length > 0)
                {
                    string ID = selected.ID.Replace("Clone_", "").Replace("Append_", "").Split(':')[0];
                    Doc_Channel channel= Channel.Default.ChannelProperties.Find(t => t.Name == ID);
                    int[] data =channel.intPixelRangArray;
                    for (int s = 0; s < data.Length; s++)
                    {
                        if (max == (data[s]))
                        {
                            int sindx = s + (e.Delta > 0 ? 0 - 1 : 1);
                            if (sindx < 0)
                                sindx =  0;
                            else if (sindx == data.Length)
                                sindx = data.Length - 1;
                            selected.PixelRate = data[sindx];
                            dr[0]["Sensitivity"] = channel.strPixelRangArray[sindx];
                            break;
                        }
                    }
                    ChartArea.HeadMouseWheel(selected, selected.PixelRate);
                }
            }
        }
        #endregion

        #region pChart触发事件实现方法
        private void ChartArea_SelectedHappenHandler(int belong, int TimeSpanLine)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                m_parent.ChangeTimeSpan(TimeSpanLine);
            }));
        }
        private delegate void Default_CalibrationChangedViewDelegate(Doc_CalibrationRecord record, int typ);
        /// <summary>
        /// 生物定标时发生
        /// </summary>
        /// <param name="record"></param>
        /// <param name="typ"></param>
        private  void Default_CalibrationChangedViewHandle(Doc_CalibrationRecord record, int typ)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Default_CalibrationChangedViewDelegate(Default_CalibrationChangedViewHandle));
            }
            else
            {
                switch (typ)
                {
                    case 0:
                        if (record != null)
                            record.MatchKey = addCalibration(record);///借用matchkey，删除用
                        break;
                    case 1:
                        if (record != null)
                            UpdateCalibration(record.MatchKey);
                        break;
                    case 2:
                        if (record != null)
                            RemoveCalibration(record.MatchKey);
                        break;
                    case 3:
                        RemoveAllCalibration();
                        break;
                }
            }
        }
        /// <summary>
        /// 通道顺序被调整时发生
        /// </summary>
        /// <param name="SortMap"></param>
        /// <returns></returns>
        private bool ChartArea_ChannelSortedHandle(Dictionary<string, string> SortMap)
        {
            DataTable dt = Channel.Default.CurrentChannelTable.Clone();
            DataTable dat = Channel.Default.CurrentChannelTable.Rows.Count > 0 ? Channel.Default.CurrentChannelTable : Channel.Default.DefultChannelTable;
            foreach (KeyValuePair<string, string> p in SortMap)
            {
                DataRow[] dr = dat.Select(string.Format("ID='{0}'", p.Key));
                if (dr.Length > 0)
                {
                    string[] ss = p.Value.Split(';');
                    dr[0]["Index"] = int.Parse(ss[0]);
                    dr[0]["Reserve"] = ss[1];
                    dt.ImportRow(dr[0]);
                }
            }
            if (Channel.Default.CurrentChannelTable.Rows.Count > 0)
                Channel.Default.CurrentChannelTable = dt;
            else
                Channel.Default.DefultChannelTable = dt;
            return true;
        }
        /// <summary>
        /// 通道头部区域右键触发
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="buttons"></param>
        private void ChartArea_ChannelHeadPopupHandler(pChart.CurveItem channel, MouseButtons buttons)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                HeadClientContent.Items.Clear();
                var item = new tools.ChannelItemConfigCopy();
                item.Text = Channel.Default.FindChannelTable(channel.ID).Name;
                item.Init(Channel.Default.FindChannelTable(channel.ID));
                item.UpdateItemHandle += item_UpdateItemHandle;
                item.StartPosition = FormStartPosition.Manual;
                item.Location = Cursor.Position;
                item.Location = EnsureLocationHelper.CalculateSituableLocation(item);
                item.ShowDialog();
            }));
        }
        /// <summary>
        /// 单通道属性更改触发
        /// </summary>
        /// <param name="channel"></param>
        private void item_UpdateItemHandle(ChannelTable channel)
        {
            ChannelTable find = Channel.Default.CurrentSaveTable.Find(t => t.ID == channel.ID);
            CurveItem item = ChartArea.FindCurve(channel.ID);
            if (find != null)
            {
                find = channel;
            }
            else
            {
                Channel.Default.CurrentSaveTable.Add(channel);
                item.TemporaryControl = true;
            }
            item.Name = channel.Name;
            item.PixelRate = channel.PixelEnable ? channel.PixelRate : 0;
            if (channel.PixelEnable)
            {
                if (!channel.MaxMinValueEnable)
                {
                    item.yAxis.SetMaxMinValue(item.PixelRateCoefficient * item.PixelRate, 0);
                    item.PixelConstants = item.PixelRate;
                }
                else
                {
                    item.PixelConstants = item.yAxis.MaxValue / item.PixelRateCoefficient;
                    item.yAxis.SetMaxMinValue(channel.MaxValue, channel.MinValue);
                    if (item.yAxis.LegendLables.Count == 2)
                    {
                        item.yAxis.LegendLables[0] = channel.MinValue.ToString();
                        item.yAxis.LegendLables[1] = channel.MaxValue.ToString();
                    }
                }
            }
            else
            {
                item.PixelConstants = item.yAxis.MaxValue / item.PixelRateCoefficient;
                item.yAxis.SetMaxMinValue(channel.MaxValue, channel.MinValue);
                if (item.yAxis.LegendLables.Count == 2)
                {
                    item.yAxis.LegendLables[0] = channel.MinValue.ToString();
                    item.yAxis.LegendLables[1] = channel.MaxValue.ToString();
                }
            }
            item.DBaseLineVisible = channel.DBaseLineVisible;
            item.Antipole = channel.Antipole;
            item.HighPass = channel.HighPass;
            item.LowPass = channel.LowPass;
            item.SingleNotch = channel.SingleNotch;
            item.PenColor = channel.PenColor;
            //if (m_stop)
                ChartArea.TemporaryInvalidate(m_stop);
        }
        private void ts_Click(object sender, EventArgs e)
        {
            Clipboard.SetText((sender as ToolStripMenuItem).Tag.ToString());
        }
        private void ts2_Click(object sender, EventArgs e)
        {
            DeviceInfo dev = new DeviceInfo() { PortClient = m_Protocol };
            dev.ShowDialog();
        }
        /// <summary>
        /// 趋势图弹出菜单现实方法
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="buttons"></param>
        private void ChartArea_ChannelViewPopupHandler(pChart.CurveItem channel, MouseButtons buttons)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                if (buttons == MouseButtons.Left)
                {
                    Info inf = new Info();
                    inf.PixelRate = channel.PixelConstants;
                    RectangleMarkers rect = channel.CurrentMarks[0] as RectangleMarkers;
                    if (rect.RectanglePoints.Count > 0)
                    {
                        List<float> values = rect.RectanglePoints.Select(t => t).ToList();
                        float max = values.Max();
                        int idxs = values.IndexOf(max);
                        float fx = (float)(max * 0.37);
                        float fx1 = (float)(fx + fx * 0.01), fx2 = (float)(fx - fx * 0.01);
                        float time = 0;
                        for (int i = idxs; i < values.Count; i++)
                        {
                            if (values[i] >= fx2 && values[i] <= fx1)
                            {
                                time = channel.TimeSpan * (i - idxs);
                                break;
                            }
                        }
                        { inf.MaxValue = max; inf.MinValue = values.Min(); inf.ValueCount = values.Count; inf.TimeValue = time; }
                    }
                    inf.Text += string.Format("({0})", channel.Name);
                    inf.TopMost = true;
                    inf.ShowDialog();
                }
                else
                {
                    HeadClientContent.Items.Clear();
                    ToolStripMenuItem ts = new ToolStripMenuItem();
                    ts.Name = "device";
                    ts.Text = "设备信息";
                    ts.Click += ts2_Click;
                    HeadClientContent.Items.Add(ts);
                    HeadClientContent.Show(Cursor.Position);
                }

            }));
        }
        private bool temporaryClear = false;
        /// <summary>
        /// 在曲线刷新前需要实现的内容
        /// </summary>
        /// <returns></returns>
        private bool ChartArea_DrawImageBeforeHandle()
        {
            if (Channel.Default.ShouldRefresh)
            {
                DataTable dt = Channel.Default.CurrentChannelTable;
                ChartArea.BaseTimeLineSpan = Channel.Default.BaseTimeLineSpan;
                List<string> strIDs = ChartArea.getItemIDs();
                Channel.Default.CurrentSaveTable.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ChannelTable table = ChannelTable.ConvertToChannel(dt.Rows[i]);
                    Channel.Default.CurrentSaveTable.Add(table);
                    pChart.CurveItem find = ChartArea.FindCurve(table.ID);
                    if (find == null)
                    {
                        find = Channel.Default.CreatChannel(dt.Rows[i]);
                        if (table.ID.Contains("Clone_"))
                        {
                            pChart.CurveItem clonechannel = ChartArea.FindCurve(table.ID.Replace("Clone_", ""));
                            if (clonechannel != null)
                            {
                                find.CloneDataSource(clonechannel);
                                clonechannel.PushDataToCloneItemHanlde += find.RecivePushData;
                            }
                        }
                        else if (table.ID.Contains("Append_"))
                        {
                            //string[] ids2 = table.ID.Replace("Append_", "").Split(':');
                            //if (ids2.Length == 2)
                            //{
                            //    CurveItem a1 = ChartArea.FindCurve(ids2[0]);
                            //    CurveItem a2 = ChartArea.FindCurve(ids2[1]);
                            //    find.CloneDataSource(a1, a2);
                            //    a1.PushDataAToCloneItemHanlde += find.RecivePushDataA;
                            //    a2.PushDataBToCloneItemHanlde += find.RecivePushDataB;
                            //}
                        }
                        ChartArea.AddCurve(find);
                    }
                    else
                    {
                        find.Name = table.Name;
                        find.Visible = table.Visible;
                        find.ChannelNo = table.ChannelNo;
                        find.yAxis.SetMaxMinValue(table.MaxValue, table.MinValue);
                        find.yAxis.LegendLables.Clear();
                        find.yAxis.LegendLables.Add(find.yAxis.MinValue.ToString());
                        find.yAxis.LegendLables.Add(find.yAxis.MaxValue.ToString());
                        find.PixelRate = table.PixelRate;
                        find.ValueZoomRate = 1;// table.ValueZoomRate;暂时去掉倍数
                        find.TimeSpan = 1000 / table.TimeSpan;
                        find.PenColor = table.PenColor;
                        find.SingleNotch = table.SingleNotch;
                        find.HighPass = table.HighPass;
                        find.LowPass = table.LowPass;
                        find.belong = table.Belong;
                        find.Antipole = table.Antipole;
                        if (temporaryClear)
                            find.TemporaryControl = false;
                        (find.Tag as ChannelFiliter).Reset();
                        strIDs.Remove(table.ID);
                    }
                }
                for (int i = 0; i < strIDs.Count; i++)
                {
                    ChartArea.RemoveCurve(strIDs[i]);
                }
                temporaryClear = false;
                Channel.Default.ShouldRefresh = false;
                return true;
            }
            return false;
        }
        #endregion

        #region 主窗体调用方法
        public void RealTimeDataView_KeyDown(object sender)
        {
            DataTable dt = Channel.Default.CurrentChannelTable;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ChannelTable table = ChannelTable.ConvertToChannel(dt.Rows[i]);
                if (table.PixelRate == 0 || !table.Visible)
                    continue;
                pChart.CurveItem find = ChartArea.FindCurve(table.ID);
                int max = find.getMaxValue();
                if (max == 0)
                    continue;
                else
                {
                    string ID = table.ID.Replace("Clone_", "").Replace("Append_", "").Split(':')[0];
                    Doc_Channel channel = Channel.Default.ChannelProperties.Find(t => t.Name == ID);
                    int[] data = channel.intPixelRangArray;
                    float rr = ChartArea.pRate;
                    bool ok = false;
                    for (int s = 0; s < data.Length; s++)
                    {
                        if (max < (data[s] * rr))
                        {
                            find.PixelRate = data[s];
                            dt.Rows[i]["Sensitivity"] = channel.strPixelRangArray[s];
                            ok = true;
                            break;
                        }
                    }
                    if (!ok)
                    {
                        find.PixelRate = data[data.Length - 1];
                        dt.Rows[i]["Sensitivity"] = channel.strPixelRangArray[data.Length - 1];
                    }
                }
            }
            //ChannelManage.Default.SaveChannelConfig(dt, Channel.Default.CurrentChannelPath);
            Channel.Default.ShouldRefresh = true;
            temporaryClear = true;
            ChartArea.ChartInvalidate();
        }
        public void RealTimeDataView_KeyDown()
        {
            pChart.CurveItem find = ChartArea.GetCurrentItem();
            if (find == null)
                return;
            if (find.PixelRate == 0)
                return;
            int max = find.getMaxValue();
            if (max == 0)
                return;
            else
            {
                string ID = find.ID.Replace("Clone_", "").Replace("Append_", "").Split(':')[0];
                Doc_Channel channel = Channel.Default.ChannelProperties.Find(t => t.Name == ID);
                int[] data = channel.intPixelRangArray;
                float rr = ChartArea.pRate;
                bool ok = false;
                DataTable dt = Channel.Default.CurrentChannelTable;
                DataRow[] dr = dt.Select(string.Format("ID='{0}'", find.ID));
                if (dr.Length > 0)
                {
                    for (int s = 0; s < data.Length; s++)
                    {
                        if (max < (data[s] * rr))
                        {
                            find.PixelRate = data[s];
                            dr[0]["Sensitivity"] = channel.strPixelRangArray[s];
                            ok = true;
                            break;
                        }
                    }
                    if (!ok)
                    {
                        find.PixelRate = data[data.Length - 1];
                        dr[0]["Sensitivity"] = channel.strPixelRangArray[data.Length - 1];
                    }
                    // ChannelManage.Default.SaveChannelConfig(dt, Channel.Default.CurrentChannelPath);
                    Channel.Default.ShouldRefresh = true;
                    find.TemporaryControl = false;
                    ChartArea.ChartInvalidate();
                }
            }
        }
        /// <summary>
        /// 界面更新
        /// </summary>
        private void ChangedView(bool TemporaryInvalidate = false)
        {
            if (TemporaryInvalidate)
            {
                ChartArea.BaseTimeLineSpan = Channel.Default.BaseTimeLineSpan;
                if (m_stop)
                    ChartArea.TemporaryInvalidate(true);
            }
            else
            {
                temporaryClear = true;
                if (m_stop)
                    ChartArea.ChartInvalidate();
            }
        }

        /// <summary>
        /// 保留最后一次使用的montage名称
        /// </summary>
        /// <param name="name"></param>
        public void UpdateMontageName(string name)
        {
            if (m_MainViewRecord != null)
                DataBaseHelper.Default.Update(new Doc_MainViewRecord()
                {
                    GUID = m_MainViewRecord.GUID
                },
                new Doc_MainViewRecord()
                {
                    ReviewMontageName = name
                });
        }
        #endregion

        #region 与设备建立通讯过程
        private string PortName = "";
        private DataBaseCom.Doc_MainViewRecord m_MainViewRecord = null;
        /// <summary>
        /// 开始监听
        /// </summary>
        /// <param name="client"></param>
        /// <param name="channelconfig"></param>
        public bool Start(IDevice client, Control containControl)
        {
            FunctionControls.tools.ProgressTipForm.Defalut.Text = Program.Language=="EN"? "Enter PSG monitoring mode" : "进入PSG监听模式";
            FunctionControls.tools.ProgressTipForm.ResultTag = null;
            FunctionControls.tools.ProgressTipForm.Defalut.Argument = new object[] { client, containControl };
            FunctionControls.tools.ProgressTipForm.Defalut.DoWork += Defalut_DoWork;
            bool ret = FunctionControls.tools.ProgressTipForm.Defalut.ShowDialog() == DialogResult.OK;
            return ret;
        }
        #region 异步线程对话框置顶写法
        private bool ShowMessage(string msg, Control containControl)
        {
            object ss = containControl.Invoke(new MessageBoxShow(MessageBoxShow_F), new object[] { msg });
            return Convert.ToBoolean(ss);
            //return MessageForm.Show(msg, "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes;
        }
        delegate bool MessageBoxShow(string msg);
        bool MessageBoxShow_F(string msg)
        {
            return MessageForm.Show(msg,Program.Language=="EN"? "Message Prompt" : "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes;
        }
        #endregion
        private string matchKey = "";
        private void Defalut_DoWork(tools.ProgressTipForm sender, DoWorkEventArgs e)
        {
            object[] args = e.Argument as object[];
            IDevice client = args[0] as IDevice;
            Control containControl = args[1] as Control;
            ///下列动作需在调用ConvertToChannel函数前执行完毕，因为ConvertToChannel用到了EDF类中的Channels
            DataTable dat = Channel.Default.CurrentChannelTable;
            if (client is BthWLans)
            {
                (client as BthWLans).ReconnectHandle += RealDataView_ReconnectHandle;
            }
            for (int i = 0; i < dat.Rows.Count; i++)
            {
                ChannelTable table = ChannelTable.ConvertToChannel(dat.Rows[i]);
                EDF.Default.ConvertToChannelItemEx(table.ID, table.TimeSpan);
            }
            try
            {
                m_Protocol = new Protocol.ProtocolServer(client.PortClient, ConvertToChannel(Channel.Default.ChannelProperties));
                Protocol = m_Protocol;
                PortName = client.MatchKey;
            }
            catch (Exception ee)
            {
                sender.SetError(string.Format("缺少vc_redist.x64 驱动包：{0}", ee.Message));
                goto getOut;
            }
            DataBaseCom.Doc_MainViewRecord mvr = new DataBaseCom.Doc_MainViewRecord()
            {
                PatientID = Channel.Default.Patient.PatientNo,
                Progress = (int)DataModel.ProgressState.None,
                ModeType = Channel.Default.SystemSetting.ModeType
            };
            m_MainViewRecord = null;
            ///数据库内容变更
            #region 新增病例后再新增表单
            if (!DataModel.DataBaseHelper.Default.Exsit(new DataBaseCom.Doc_PatientInfo() { PatientNo = Channel.Default.Patient.PatientNo }))
            {
                sender.SetProgress(0, Program.Language=="EN"? "Create new case information..." : "创建新病例档案信息...");
                if (DataModel.DataBaseHelper.Default.Insert(Channel.Default.Patient))
                {
                    mvr.RecordTime = DateTime.Now;
                    mvr.Progress = (int)DataModel.ProgressState.None;
                    mvr.ReportReady = false;
                    mvr.CreatTime = DateTime.Now;//用新病例选择远程直接下一步开始检测 新增出来的记录赋值
                    mvr.DifferentVersion = 1;
                    mvr.VideoHave = false;
                    mvr.DoctorID = "1";
                    //病例信息需要记录版本 预防成人版中插入病例后，插入首页记录失败，导致病例信息版本取默认值0
                    mvr.ModeType = Channel.Default.SystemSetting.ModeType;
                    mvr.LoginID = Channel.Default.Loginer.ID;
                    sender.SetProgress(3, Program.Language == "EN" ? "Create medical record information..." : "创建就诊记录信息...");
                    if (DataModel.DataBaseHelper.Default.Insert(mvr))
                    {
                        isnewpatient = true;
                        m_MainViewRecord = DataModel.DataBaseHelper.Default.Select(mvr);
                        FunctionControls.tools.ProgressTipForm.ResultTag = m_MainViewRecord.ID;
                    }
                }
            }
            #endregion
            else
            {
                Doc_MainViewRecord find = DataModel.DataBaseHelper.Default.Select(mvr);
                if (find != null)
                    mvr = find;
                else
                {
                    mvr.CreatTime = DateTime.Now;//点击新病例病例已存在 点击下一步 然后 开始检测 新增的记录进行赋值 或者新监测出现新病例赋值
                    mvr.VideoHave = false;
                    mvr.DifferentVersion = 1;
                    mvr.RecordTime = DateTime.Now;
                    mvr.Progress = (int)DataModel.ProgressState.None;
                    mvr.ReportReady = false;
                    mvr.DoctorID = "1";
                    mvr.LoginID = Channel.Default.Loginer.ID;
                }

            }
            if (sender.CancellationPending)
            {
                e.Cancel = true;
                goto getOut;
            }
            IDevice finddevs = (Program.LocationScan ? DeviceOnLine.Default.GetDevices() : DeviceOnWLan.Default.getDevices()).Find(t => t.MatchKey == client.MatchKey);
            if (finddevs == null)
            {
                sender.SetError("当前设备处理离线模式，请稍后重试！");
                goto getOut;
            }
            devName = finddevs.DeviceName;
            sender.SetProgress(5, string.Format(Program.Language == "EN" ? "Connected Device{0}..." : "连接设备{0}...", devName));
            DateTime dt = DateTime.Now;
            while (!sender.CancellationPending)
            {
                if (client.PortClient.Open())
                {
                    break;
                }
                if ((DateTime.Now - dt).TotalSeconds > 15)
                {
                    break;
                }
                if (!sender.CancellationPending)
                    System.Threading.Thread.Sleep(1000);
            }
            if (sender.CancellationPending)
            {
                e.Cancel = true;
                goto getOut;
            }
            if (!client.PortClient.IsConnected)
            {
                sender.SetError(string.Format("连接设备{0}失败", devName));
                goto getOut;
            }
            m_Protocol.PatientNo = Channel.Default.Patient.PatientNo;
            m_Protocol.RecUserDataHandle += m_Protocol_RecUserDataHandle;
            m_Protocol.FnStop = false;
            m_isKillTask = false;
            if (sender.CancellationPending)
            {
                e.Cancel = true;
                goto getOut;
            }
            sender.SetProgress(20, Program.Language == "EN" ? "Device initialization..." : "设备初始化...");
            System.Threading.Thread.Sleep(1800);
            if (!m_Protocol.InitDeviceInfo())
            {
                sender.SetError(string.Format("{0}初始化失败", devName));
                goto getOut;
            }
            ///为了兼容不带定时功能的设备，暂时不判断
            m_Protocol.GetAutoRunTime();
            m_Protocol.GetDeviceSNCode();
            m_Protocol.GetDeviceHS();
            m_Protocol.getBlueToothName();
            //m_Protocol.GetDeviceType();
            //if (!m_Protocol.GetAutoRunTime())
            //{
            //    sender.SetError(string.Format("{0}获取定时失败", devName));
            //    goto getOut;
            //}
            if (m_Protocol.GetDeviceType())
            {
                if (m_Protocol.UserData.DeviceType >10)
                    {
                        MessageBox.Show("该设备未正式授权,请联系设备提供方" ,"提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
            }


            if (m_Protocol.UserData.DeviceBusy)
            {
                sender.SetProgress(60, string.Format(string.Format(Program.Language == "EN" ? "Retrieve case information that matches {0}..." : "获取与{0}匹配的病例信息...", devName)));
                if (!m_Protocol.GetPatientIDRequest())
                {
                    sender.SetError(string.Format("{0}的病例信息获取失败", devName));
                    goto getOut;
                }
                else
                {
                    if (m_MainViewRecord == null)
                    {
                        string key = m_Protocol.UserData.MatchKey;
                        m_MainViewRecord = DataModel.DataBaseHelper.Default.Select(new DataBaseCom.Doc_MainViewRecord() { MatchKey = key });
                        if (m_MainViewRecord != null)
                        {
                            DataBaseCom.Doc_PatientInfo info = DataModel.DataBaseHelper.Default.Select(new DataBaseCom.Doc_PatientInfo() { PatientNo = m_MainViewRecord.PatientID });
                            if (ShowMessage(string.Format(Program.Language=="EN"? "Found cases that match the current device information({0} {1} {2})，Do you want to continue accessing monitoring？" : "已找到与当前设备信息匹配的历史病例({0} {1} {2})，是否要继续接入监测？", info.PatientNo, info.PatientName, m_MainViewRecord.RecordTime), containControl))
                            {
                                Channel.Default.Patient = info;
                                sender.SetProgress(75, Program.Language == "EN" ? "Start to continue monitoring {0}..." : "启动继续监听...");
                                if (!m_Protocol.SetContinueSampleRequest())
                                {
                                    sender.SetError("启动监听失败!");
                                    goto goreturn;
                                }
                                if (sender.CancellationPending)
                                {
                                    e.Cancel = true;
                                    goto goreturn;
                                }
                                isreconnection = true;
                                if (!_initButtonReady)
                                {
                                    Channel.Default.ProgressStauts = ProgressState.Monitoring;
                                    Channel.Default.RefreshHomeMenu();
                                    containControl.Invoke(new MethodInvoker(() =>
                                    {
                                        // DataModel.DeviceOnLine.Default.updateDevices(PortName, DeviceState.Running);
                                        //DingBiaoButton.Enabled = false;
                                        PasueButton.Enabled = true;
                                        StartRunButton.Enabled = false;
                                        SetTimeButton.Enabled = false;
                                        //ZkTestButton.Enabled = false;
                                        //StartRunButton.Text = "停止采集";
                                        m_stop = false;
                                        ChartArea.FnStop = false;
                                        if (Channel.Default.SystemSetting.Reserve2 != "1")
                                            m_hasVedio = Channel.Default.SystemSetting.VedioSourceUrl != "" && !Program.LocationScan;
                                        else
                                            m_hasVedio = Channel.Default.SystemSetting.VedioSourceUrl != "";
                                        hasvideo = m_hasVedio;
                                        if (m_hasVedio)
                                        {
                                            ///防止断电重连时，重新打开视频监控
                                            Channel.Default.LoadVedio("myVedioPlayer.exe", m_MainViewRecord.MatchKey, Channel.Default.SystemSetting.VedioSourceUrl, Channel.Default.SystemSetting.VedioSavePath, Channel.Default.SystemSetting.NotSplitRecordFile, Channel.Default.SystemSetting.MaxRecodFileLenght);
                                            DataBaseHelper.Default.Update(new Doc_MainViewRecord()
                                            {
                                                GUID = m_MainViewRecord.GUID
                                            }, new Doc_MainViewRecord()
                                            {
                                                Reserve3 = Channel.Default.SystemSetting.VedioSavePath,
                                                VideoHave = true
                                            });
                                            FunctionControls.tools.ProgressTipForm.ResultTag = m_MainViewRecord.ID;
                                        }
                                    }));
                                    _initButtonReady = true;
                                }
                                ///直接进入监测画面，不需要下发病人信息，设备ID
                                goto last;
                            }
                            else
                            {
                                sender.SetError("请按确定键结束本次操作!");
                                goto goreturn;
                            }
                        }
                        else
                        {
                            sender.SetError(Program.Language=="EN"? "The device is already in monitoring mode!" : "设备处于PSG监测中，无法由当前系统接入监听!");
                            goto getOut;
                        }
                    }
                }
            }
            if (sender.CancellationPending)
            {
                e.Cancel = true;
                goto getOut;
            }
            sender.SetProgress(60, Program.Language == "EN" ? "Write case information..." : "写入病例信息...");
            matchKey = Guid.NewGuid().ToString();
            //为了字符总长度不超过80，这里对patientno和patientname的长度进行限制
            string patientnoinmatchkey = Channel.Default.Patient.PatientNo;
            string patientnameinmatchkey = Channel.Default.Patient.PatientName;
            if (patientnoinmatchkey.Length > 15)
            {
                patientnoinmatchkey = patientnoinmatchkey.Substring(0, 15);
                int ss = patientnoinmatchkey.Length;
            }
            if (patientnameinmatchkey.Length > 6)
            {
                patientnameinmatchkey = patientnameinmatchkey.Substring(0, 6);
            }
            int trycount = 0;
            do
            {
                if (!m_Protocol.SetPatientIDRequest(string.Format("{0} {1} {2:dd-MM-yyyy} {3} {4}", patientnoinmatchkey, Channel.Default.Patient.Gender, Channel.Default.Patient.BirthDate, patientnameinmatchkey, matchKey)))
                {
                    sender.SetError("病例信息写入失败：设备无响应或者PSG睡眠监测中！");
                    goto getOut;
                }
                else
                {
                    System.Threading.Thread.Sleep(200);
                    trycount++;
                }
                if (sender.CancellationPending)
                {
                    e.Cancel = true;
                    goto getOut;
                }
            } while (m_Protocol.UserData.MatchKey != matchKey&&trycount<5);
            if(trycount>=5)
            {
                sender.SetError("病例信息写入失败：匹配码不一致！");
                DataModel.LogInstance.Default.AddLog(string.Format("匹配码不一致,下发匹配码{0}，收到匹配码{1}", matchKey, m_Protocol.UserData.MatchKey), pSystem.LogManagement.LogLevel.ERROR);
                goto getOut;
            }
            if (sender.CancellationPending)
            {
                e.Cancel = true;
                goto getOut;
            }
            sender.SetProgress(75, Program.Language == "EN" ? "Write device information..." : "写入设备信息...");
            if (!m_Protocol.SetDeviceInfoRequest(""))
            {
                sender.SetError("设备信息写入失败!");
                goto getOut;
            }
            if (sender.CancellationPending)
            {
                e.Cancel = true;
                goto getOut;
            }
            //if (ShowMessage("是否进入实时监测界面？", containControl))
            //{
            //    goto last;
            //}
            //else
            //{
            //    sender.SetProgress(100, "完成");
            //    e.Cancel = true;
            //    sender.DialogResult = DialogResult.Cancel;
            //    goto getOut;
            //}
         last:
            soundplayer.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "LowSpo2.wav";//警告声音
            soundplayer.Load(); //同步加载声音
            Channel.Default.StartTime = DateTime.Now;
            sender.SetProgress(90, Program.Language == "EN" ? "Loading interface..." : "加载界面...");
            System.Threading.Thread.Sleep(500);
            if (sender.CancellationPending)
            {
                e.Cancel = true;
                goto getOut;
            }
            sender.SetProgress(100, Program.Language == "EN" ? "Complete" : "完成");
            //进度条加载完成之后 再插入记录 避免进度条取消时 记录已经插入数据库中
            if (m_MainViewRecord == null)
            {
                if (mvr.ID == 0)
                {
                    if (DataModel.DataBaseHelper.Default.Insert(mvr))
                    {
                        m_MainViewRecord = DataModel.DataBaseHelper.Default.Select(mvr);
                        FunctionControls.tools.ProgressTipForm.ResultTag = m_MainViewRecord.ID;
                    }
                }
                else
                    m_MainViewRecord = mvr;
            }
            //继续监听 不应该修改数据库中对应的记录
            if (!isreconnection)
            {
                m_MainViewRecord.Progress = (int)DataModel.ProgressState.None;
                m_MainViewRecord.MatchKey = matchKey;
                m_MainViewRecord.Reserve2 = m_Protocol.UserData.DeviceSNCode;
                m_MainViewRecord.RecordTime = DateTime.Now;
                ///把当前记录的标识变更成准备分析状态
                DataModel.DataBaseHelper.Default.Update(new Doc_MainViewRecord() { ID = m_MainViewRecord.ID }, m_MainViewRecord);
            }
            Channel.Default.HomePageRefresh = true;
            EDF.Default.DeviceSNCode = m_Protocol.UserData.DeviceSNCode;
            goto doupdate;
         getOut:
            if (m_MainViewRecord != null && !isnewpatient)
            {
                DataBaseHelper.Default.Delete(new Doc_MainViewRecord
                {
                    ID = m_MainViewRecord.ID
                });
            }
            DeviceOnLine.Default.UpdateDevices(client.MatchKey, DeviceState.OffLine);
        goreturn:
            m_Protocol.Dispose();
        doupdate:
            Task.Factory.StartNew(() =>
            {
                if (FunctionControls.tools.ProgressTipForm.ResultTag != null)
                    Channel.Default.UpdateRecord(Convert.ToInt32(FunctionControls.tools.ProgressTipForm.ResultTag));
            });
        }

        private void RealDataView_ReconnectHandle(IOClient newportClient)
        {
            //newportClient.DataReceiveHandle += ((IOClient.DataRecHandleDelegate)DeviceOnWLan.Default.CurrentDevice.PortClient.GetDataReceiveInvorker());
            //oldportClient = newportClient;
            System.Threading.Thread.Sleep(1000);
            m_Protocol.UpdatePortClient(newportClient);
            m_Protocol.SetContinueSampleRequest();
        }
        #endregion

        #region 实时数据接收时处理
        private void m_Protocol_RecUserDataHandle(UserDataDefine UserData)
        {
            if (this.IsHandleCreated)
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    if (!m_parent.BatteryCapacity.Visible)
                        m_parent.BatteryCapacity.Visible = true;
                    if (MainForm.BatteryCapacityValue != UserData.BatteryCapacity*1.05)
                    {
                        MainForm.BatteryCapacityValue = UserData.BatteryCapacity*1.05 > 100 ? 100 : UserData.BatteryCapacity < 0 ? 0 : (int)Math.Round(UserData.BatteryCapacity * 1.05);
                        MainForm.BatteryLastTime = (int)(16.191 * UserData.BatteryCapacity - 161.41);
                        m_parent.BatteryCapacity.Invalidate();
                    }
                }));
            }
            else
            {
                MainForm.BatteryCapacityValue = UserData.BatteryCapacity * 1.05 > 100 ? 100 : UserData.BatteryCapacity < 0 ? 0 : (int)Math.Round(UserData.BatteryCapacity * 1.05);
                //MainForm.BatteryCapacityValue = UserData.BatteryCapacity > 100 ? 100 : UserData.BatteryCapacity < 0 ? 0 : UserData.BatteryCapacity;
                MainForm.BatteryLastTime = (int)(16.191 * UserData.BatteryCapacity - 161.41);
            }
        }
        /// <summary>
        /// 注册数据接收事件
        /// </summary>
        /// <param name="Channel"></param>
        private void m_Protocol_RecChannelEventHandle(Protocol.DataDefine Channel)
        {
            pChart.CurveItem find = ChartArea.FindCurve(Channel.Name);
            if (find != null)
            {
                List<string> ids = ChartArea.getItemIDs();
                ids.Remove(Channel.Name);///去掉本体通道
                List<string> subids = ids.FindAll(t => t.Replace("Clone_","")==(Channel.Name));
                if (subids != null)
                {
                    ///克隆的通道绑定其本体通道数据来源
                    for (int i = 0; i < subids.Count; i++)
                    {
                        pChart.CurveItem item = ChartArea.FindCurve(subids[i]);
                        Channel.RecOneDataEventHandle += item.AddDatavalue;
                    }
                }
                Channel.RecOneDataEventHandle += find.AddDatavalue;
                //17 血氧 BloodOxygen 18 体位 BodyState 19环境光 AmbientLight 20 脉率 PulseRate
                //11 胸呼吸 ChestBreathing 12 腹呼吸 AbdominalBreathing 15 鼻压力 PressureMuzzleFlow 16 热敏 ThermalMuzzleFlow
                //22 电量
                ///实时存储暂时去掉
                ChannelItem one = EDF.Default.Channels.Find(t => t.ID == Channel.ID);
                if (one != null)
                    Channel.RecOneEdfDataEventHandle += one.AddData;
                if (Channel.ID == 17)
                {
                    Channel.RecOneDataEventHandle += BloodOxygen_RecOneDataEventHandle;
                }
                else if (Channel.ID == 18)
                {
                    Channel.RecOneDataEventHandle += BodyState_RecOneDataEventHandle;
                }
                else if (Channel.ID == 19)
                {
                    Channel.RecOneDataEventHandle += AmbientLight_RecOneDataEventHandle;
                }
                else if (Channel.ID == 20)
                {
                    Channel.RecOneDataEventHandle += PulseRate_RecOneDataEventHandle;
                }
                else if (Channel.ID == 11)
                {
                    Channel.RecOneDataEventHandle += ChestBreathing_RecOneDataEventHandle;
                    xfChannel1 = new ChannelFiliter(100);
                    xfChannel1.breathChannelType = 4;
                }
                else if (Channel.ID == 12)
                {
                    Channel.RecOneDataEventHandle += AbdominalBreathing_RecOneDataEventHandle;
                    xfChannel2 = new ChannelFiliter(100);
                    xfChannel2.breathChannelType = 3;
                }
                else if (Channel.ID == 15)
                {
                    Channel.RecOneDataEventHandle += PressureMuzzleFlow_RecOneDataEventHandle;
                    xfChannel3 = new ChannelFiliter(100);
                    xfChannel3.breathChannelType = 2;
                }
                else if (Channel.ID == 16)
                {
                    Channel.RecOneDataEventHandle += ThermalMuzzleFlow_RecOneDataEventHandle;
                    xfChannel4 = new ChannelFiliter(100);
                    xfChannel4.breathChannelType = 1;
                }
            }
        }
        private ViewResult m_viewResult = null;
        private ChannelFiliter xfChannel1 = null;
        private ChannelFiliter xfChannel2 = null;
        private ChannelFiliter xfChannel3 = null;
        private ChannelFiliter xfChannel4 = null;
        private void ThermalMuzzleFlow_RecOneDataEventHandle(float data, bool Invaild, string msg)
        {
            float value = xfChannel4.getHeartRate((int)data);
            m_viewResult.ThermalBreathingRate = string.Format("热敏呼吸率：{0}次/min", xfChannel4.getBreathChannelState(data) ? value.ToString() : "--");
        }

        private void PressureMuzzleFlow_RecOneDataEventHandle(float data, bool Invaild, string msg)
        {
            float value = xfChannel3.getHeartRate((int)data);
            m_viewResult.Pressure = string.Format("压力值：{0:f2}cmH2O", data / 98.067);//pa转厘米水柱
            m_viewResult.PressureBreathingRate = string.Format("压力呼吸率：{0}次/min", xfChannel3.getBreathChannelState(data) ? value.ToString() : "--");
        }
        private bool _initButtonReady = false;
        private void AbdominalBreathing_RecOneDataEventHandle(float data, bool Invaild, string msg)
        {
            float value = xfChannel2.getHeartRate((int)data);
            m_viewResult.AbdominalBreathingRate = string.Format("腹部呼吸率：{0}次/min", xfChannel2.getBreathChannelState(data) ? value.ToString() : "--");
        }
        private void ChestBreathing_RecOneDataEventHandle(float data, bool Invaild, string msg)
        {
            float value = xfChannel1.getHeartRate((int)data);
            m_viewResult.ChestBreathingRate = string.Format("胸部呼吸率：{0}次/min", xfChannel1.getBreathChannelState(data) ? value.ToString() : "--");
        }

        private void PulseRate_RecOneDataEventHandle(float data, bool Invaild, string msg)
        {
            m_viewResult.PulseRate = string.Format("脉率:{0}", Invaild ? msg : string.Format("{0}次/min", data));
        }

        private void AmbientLight_RecOneDataEventHandle(float data, bool Invaild, string msg)
        {
            //m_viewResult.AmbientLight = string.Format("{0}", data > 10 ? "开灯" : "关灯");
            m_viewResult.AmbientLight = string.Format("{0}", data == 0x01 ? "开灯" : "关灯");
        }

        private void BodyState_RecOneDataEventHandle(float data, bool Invaild, string msg)
        {
            /// 仰：0x01  左：0x02  右：0x03  趴：0x04  坐：0x05   倒立：0x06
            m_viewResult.BodySate = string.Format("体位：{0}", data == 0x01 ? "仰卧" : data == 0x02 ? "左侧卧" : data == 0x03 ? "右侧卧" : data == 0x04 ? "俯卧" : data == 0x05 ? "站立" : "倒立");
        }

        private void BloodOxygen_RecOneDataEventHandle(float data, bool Invaild, string msg)
        {
            m_viewResult.BloodOxygen = string.Format("血氧：{0}", Invaild ? msg : string.Format("{0}%", data));
        }
        #endregion

        #region 按钮单击实现
        private void QuitButton_Click(object sender, EventArgs e)
        {
            DataModel.LogInstance.Default.AddLog("点击 红色的× 退出按钮");
            if (MessageForm.Show(string.Format(ML.GetText("REALDATAVIEW_SLOSE_S1", "是否退出实时监测页面(数据采集") +"{0}"+ ML.GetText("REALDATAVIEW_SLOSE_S2", "不会中断)?"), m_hasVedio ? ML.GetText("REALDATAVIEW_SLOSE_S3", "和视频监控均") : ""), ML.GetText("REALDATAVIEW_MESSAGE", "信息提示"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
               // if (MessageForm.Show(string.Format("是否退出实时监测页面(数据采集{0}不会中断)？", m_hasVedio ? "和视频监控均" : ""), "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataModel.LogInstance.Default.AddLog("用户确定退出实时监测页面");
                ChartArea.FnStop = true;
                EDF.Default.Reset();
                m_Protocol.FnStop = true;
                m_stop = false;
                m_DeviceRun = false;
                this.Dispose();
                m_parent.LoadHomePage();
            }
            else
            {
                DataModel.LogInstance.Default.AddLog("用户取消退出实时监测页面");
            }
        }
        private bool m_stop = true;
        private void PasueButton_Click(object sender, EventArgs e)
        {
            if (!m_stop)
            {
                m_stop = true;
                m_Protocol.FnStop = true;
                PasueButton.Text = ML.GetText("REALDATAVIEW_CONTINUE", "继续");// "继续"; 
                DataModel.LogInstance.Default.AddLog("点击 实时监测-暂停 按钮");
            }
            else
            {
                m_stop = false;
                m_Protocol.FnStop = false;
                PasueButton.Text = ML.GetText("REALDATAVIEW_PAUSE", "暂停");// "暂停";
                DataModel.LogInstance.Default.AddLog("点击 实时监测-继续 按钮");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (m_parent.toolStripStatusLabel3.Text != "离线")
            {
                m_parent.FourLable.Text = m_viewResult.ThermalBreathingRate;
                m_parent.FiveLabel.Text = m_viewResult.PressureBreathingRate;
                m_parent.SixLabel.Text = m_viewResult.Pressure;
                m_parent.EightLable.Text = m_viewResult.AbdominalBreathingRate;
                m_parent.SevenLable.Text = m_viewResult.ChestBreathingRate;
                if (m_viewResult.PulseRate != "")
                {
                    if (!m_viewResult.PulseRate.Contains("min"))
                    {
                        if (m_parent.ThreeLable.ForeColor == Color.Black)
                            m_parent.ThreeLable.ForeColor = Color.Red;
                        else
                        {
                            m_parent.ThreeLable.ForeColor = Color.Black;
                        }
                    }
                    else
                    {
                        if (m_parent.ThreeLable.ForeColor != Color.Black)
                            m_parent.ThreeLable.ForeColor = Color.Black;
                    }
                }
                m_parent.ThreeLable.Text = m_viewResult.PulseRate;
                m_parent.toolStripStatusLabel3.Text = m_viewResult.AmbientLight;
                m_parent.OneLable.Text = m_viewResult.BodySate;
                if (m_viewResult.BloodOxygen != "")
                {
                    if (!m_viewResult.BloodOxygen.Contains("%"))
                    {
                        if (m_parent.TwoLable.ForeColor == Color.Black)
                        {
                            m_parent.TwoLable.ForeColor = Color.Red;
                        }
                        else
                        {
                            m_parent.TwoLable.ForeColor = Color.Black;
                        }
                    }
                    else
                    {
                        if (m_parent.TwoLable.ForeColor != Color.Black)
                            m_parent.TwoLable.ForeColor = Color.Black;
                        string strSpo2 = m_viewResult.BloodOxygen;
                        float value = float.Parse(strSpo2.Replace("%", "").Replace("血氧：", ""));
                        if (value < float.Parse(Channel.Default.SystemSetting.Reserve1) && value > 0)
                            WarnningSound = true;
                    }
                }
                m_parent.TwoLable.Text = m_viewResult.BloodOxygen;
            }
        }

        private void ZkTestButton_Click(object sender, EventArgs e)
        {
            DataModel.LogInstance.Default.AddLog("点击 阻抗测试 按钮");
            Block.ImpedanceView view = new Block.ImpedanceView();
            view.Init(m_Protocol);
            ChartArea.FnStop = true; 
            view.ShowDialog();
            ChartArea.FnStop = false;
            m_Protocol.FnStop = m_stop;
        }
        bool m_DeviceRun = false;
        private DateTime startruntime = new DateTime();
        private void StartRunButton_Click(object sender, EventArgs e)
        {
            string des = StartRunButton.Text;
            if (Channel.Default.SystemSetting.Reserve2 != "1")
                m_hasVedio = Channel.Default.SystemSetting.VedioSourceUrl != "" && !Program.LocationScan;
            else
                m_hasVedio = Channel.Default.SystemSetting.VedioSourceUrl != "";
            hasvideo = m_hasVedio;
           // if (des == "停止采集")
           if (des == ML.GetText("REALDATAVIEW_STOP", "停止采集"))
            {
                DataModel.LogInstance.Default.AddLog("点击 停止采集 按钮");
                //string s1 = "Is the data collection device performing a stop operation during normal operation";
                //string s2 = "Video stops simultaneously";
                //string s3 = "Message";
                if (MessageForm.Show(string.Format(ML.GetText("REALDATAVIEW_STOP_S1", "数据采集设备正常工作中，是否执行停止操作") +"{0}？", m_hasVedio ? "("+ ML.GetText("REALDATAVIEW_STOP_S2", "视频监控同时停止") +")" : ""), ML.GetText("REALDATAVIEW_STOP", "信息提示"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                   // if (MessageForm.Show(string.Format("数据采集设备正常工作中，是否执行停止操作{0}？", m_hasVedio ? "(视频监控同时停止)" : ""), "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DataModel.LogInstance.Default.AddLog("用户确认停止采集");
                    Task.Factory.StartNew(() =>
                    {
                        if (m_Protocol.StopSampleRequest())
                        //m_Protocol.StopSampleRequest();
                        {
                            ChartArea.FnStop = true;
                            EDF.Default.Reset();
                            m_Protocol.FnStop = true;
                            Channel.Default.EndTime = DateTime.Now;
                            Channel.Default.ProgressStauts = ProgressState.MonitorFinish;
                            Channel.Default.RefreshHomeMenu();
                            //DataModel.DataBaseHelper.Default.Update(new DataBaseCom.Doc_MainViewRecord() { ID = Convert.ToInt32(Channel.Default.AnalysisReult.Tag) }, new DataBaseCom.Doc_MainViewRecord() { DoctorID = Channel.Default.Doctor.ID.ToString(), EdfPath = EDF.Default.EdfPath, GUID = string.Format("{0}_{1}_{2}_88", DataModel.DataBaseHelper.Default.ComputeSHA256(EDF.Default.EdfPath), Channel.Default.Patient.PatientNo, Channel.Default.Doctor.ID), ReportReady = false, Progress = (int)DataModel.ProgressState.Temporary });
                            this.Invoke(new MethodInvoker(() =>
                            {
                                DingBiaoButton.Enabled = false;
                                PasueButton.Enabled = false;
                                m_stop = false;
                                m_DeviceRun = false;
                                PasueButton.Text = ML.GetText("REALDATAVIEW_PAUSE", "暂停");// "暂停";
                                StartRunButton.Text = ML.GetText("REALDATAVIEW_START", "开始采集");// "开始采集";
                                if (m_hasVedio)
                                    WinMessageUtil.SendMessage("myVedioPlayer", 0, "SampleStop");
                                DataModel.LogInstance.Default.AddLog("数据停止采集执行成功!", pSystem.LogManagement.LogLevel.WARN);
                                AhDung.MessageTip.ShowOk("数据停止采集执行成功!");
                            }));
                        }
                        else
                        {
                            AhDung.MessageTip.ShowError("数据停止采集执行失败!");
                            DataModel.LogInstance.Default.AddLog("数据停止采集执行失败!", pSystem.LogManagement.LogLevel.ERROR);
                        }
                    });
                }
                else
                {
                    DataModel.LogInstance.Default.AddLog("用户取消停止采集");
                }
            }
            else
            {
                DataModel.LogInstance.Default.AddLog("点击 开始采集 按钮");
                Task.Factory.StartNew(() =>
                {
                    m_Protocol.FnStop = false;
                    if (m_Protocol.StartSampleRequest())
                    // m_Protocol.StartSampleRequest();
                    {
                        ChartArea.FnStop = false;
                        _initButtonReady = true;
                        Channel.Default.ProgressStauts = DataModel.ProgressState.Monitoring;
                        Channel.Default.RefreshHomeMenu();
                        EDF.Default.Init(m_MainViewRecord.MatchKey);
                        this.Invoke(new MethodInvoker(() =>
                        {
                            //DataModel.DeviceOnLine.Default.updateDevices(PortName, DeviceState.Running);
                            DingBiaoButton.Enabled = true;
                            PasueButton.Enabled = true;
                            m_stop = false;
                            PasueButton.Text = ML.GetText("REALDATAVIEW_PAUSE", "暂停");// "暂停";
                            StartRunButton.Text = ML.GetText("REALDATAVIEW_STOP", "停止采集");// "停止采集";
                            m_DeviceRun = true;
                            AhDung.MessageTip.ShowOk("数据开始采集执行成功!");
                            startruntime = DateTime.Now;
                            label3.Text = string.Format("{1}({0}) {3} {2}岁 {5}  @开始：{4}", Channel.Default.Patient.PatientNo, Channel.Default.Patient.PatientName, Channel.Default.Patient.Age, Channel.Default.Patient.Gender, startruntime, devName);
                            DataModel.LogInstance.Default.AddLog("数据开始采集执行成功!", pSystem.LogManagement.LogLevel.WARN);
                            if (m_hasVedio)
                            {
                                Channel.Default.LoadVedio("myVedioPlayer.exe", m_MainViewRecord.MatchKey, Channel.Default.SystemSetting.VedioSourceUrl, Channel.Default.SystemSetting.VedioSavePath, Channel.Default.SystemSetting.NotSplitRecordFile, Channel.Default.SystemSetting.MaxRecodFileLenght, true);
                                //        m_vPlayer = new VedioPlayer(new MyConfiguration()
                                //        {
                                //            SaveDirectory = Channel.Default.SystemSetting.VedioSavePath,
                                //            UrlPath = Channel.Default.SystemSetting.VedioSourceUrl,
                                //            MatchKey= m_MainViewRecord.MatchKey
                                //});
                                //        m_vPlayer.Dock = DockStyle.Fill;
                                //        m_vPlayer.IsRealView = true;
                                //        play = new SkinForm();
                                //        play.ShowIcon = false;
                                //        play.Text = "视频监控";
                                //        play.Size = new System.Drawing.Size(500, 400);
                                //        play.Controls.Add(m_vPlayer);
                                //        play.Left = Screen.AllScreens[0].WorkingArea.Right - play.Width;
                                //        play.Top = Screen.AllScreens[0].WorkingArea.Bottom - play.Height;
                                //        play.StartPosition = FormStartPosition.Manual;
                                //        play.ControlBox = false;
                                //        play.Owner = this.ParentForm;
                                //        play.Show();
                                DataBaseHelper.Default.Update(new Doc_MainViewRecord()
                                {
                                    GUID = m_MainViewRecord.GUID
                                },
                                new Doc_MainViewRecord()
                                {
                                    Reserve3 = Channel.Default.SystemSetting.VedioSavePath,
                                    VideoHave = true,
                                    RecordTime = startruntime
                                });
                            }
                            //点击开始采集之后，再改变首页记录状态
                            DataBaseHelper.Default.Update(new Doc_MainViewRecord()
                            {
                                GUID = m_MainViewRecord.GUID
                            },
                            new Doc_MainViewRecord()
                            {
                                Progress = (int)ProgressState.Ready
                            });
                        }));
                    }
                    else
                    {
                        AhDung.MessageTip.ShowError("数据开始采集执行失败!");
                        DataModel.LogInstance.Default.AddLog("数据开始采集执行失败!", pSystem.LogManagement.LogLevel.ERROR);
                    }
                });
                //this.BeginInvoke(new MethodInvoker(() =>
                //{
                //    Block.VedioPlayCtrl vedio = new Block.VedioPlayCtrl() { IsRealView = true, GUID = m_MainViewRecord.MatchKey };
                //    vedio.StartPosition = FormStartPosition.Manual;
                //    vedio.Location = new Point(Screen.PrimaryScreen.Bounds.Width - vedio.Width, Screen.PrimaryScreen.Bounds.Height - vedio.Height);
                //    vedio.Show();
                //}));
            }
        }

        private void DingBiaoButton_Click(object sender, EventArgs e)
        {
            DataModel.LogInstance.Default.AddLog("点击 定标 按钮");
            Block.Calibration cail = new Block.Calibration();
            cail.Protocol = m_Protocol;
            cail.StartPosition = FormStartPosition.CenterParent;
            cail.Tag = m_MainViewRecord;
            cail.ShowDialog();
        }
        #endregion

        #region 私有成员函数
        private bool m_WarnningSound = false;
        private bool WarnningSound
        {
            set
            {
                if (value != m_WarnningSound)
                {
                    if (value)
                    {
                        soundplayer.PlayLooping();
                        pictrueBoxEx1.Visible = value;
                    }
                    else
                    {
                        soundplayer.Stop();
                        pictrueBoxEx1.Visible = false;
                    }
                    m_WarnningSound = value;
                }
            }
        }
        /// <summary>
        /// 基本信息
        /// </summary>
        /// <param name="visible"></param>
        private void SetInfo(bool visible = true)
        {
            m_parent.OneLable.Visible = visible;
            m_parent.OneLable.Text = "    ";
            m_parent.TwoLable.Visible = visible;
            m_parent.TwoLable.Text = "    ";
            m_parent.ThreeLable.Visible = visible;
            m_parent.ThreeLable.Text = "    ";
            m_parent.FourLable.Visible = visible;
            m_parent.FourLable.Text = "    ";
            m_parent.FiveLabel.Visible = visible;
            m_parent.FiveLabel.Text = "    ";
            m_parent.SixLabel.Visible = visible;
            m_parent.SixLabel.Text = "    ";
            m_parent.SevenLable.Visible = visible;
            m_parent.SevenLable.Text = "    ";
            m_parent.EightLable.Visible = visible;
            m_parent.EightLable.Text = "    ";
            m_parent.BatteryCapacity.Visible = visible;
            if (!visible)
                m_parent.toolStripStatusLabel3.Text = Program.Language == "EN" ? "Ready" : "准备";
        }
        private object m_calibrationLock = new object();
        /// <summary>
        /// 定标成功时
        /// </summary>
        private string addCalibration(Doc_CalibrationRecord record)
        {

            if (record != null)
            {
                if (record.Comments != "")
                {
                    List<int> keys = new List<int>();
                    string[] items = record.Comments.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (items.Length > 0)
                    {
                        for (int i = 0; i < items.Length; i++)
                        {
                            string[] ss = items[i].Split(':');
                            if (ss.Length == 2)
                            {
                                string[] tt = ss[1].Split('-');
                                if (tt.Length == 2)
                                {
                                    Doc_CalibrationDefine find = m_CalibrationDefine.Find(t => t.ID == int.Parse(ss[0]));
                                    DateTime st = record.StartTime.AddMilliseconds(float.Parse(tt[0]));
                                    if (find != null)
                                    {
                                        string[] ids = find.ChannelID.Split('/');
                                        for (int s = 0; s < ids.Length; s++)
                                        {
                                            RectangleMarkers mark = new RectangleMarkers();
                                            mark.ID = EDF.Default.ConvertToChannelIDEx(int.Parse(ids[s]));
                                            mark.StartTime = st;
                                            mark.Name = find.Name;
                                            mark.MarkCreatTime = default(DateTime);
                                            lock (m_calibrationLock)
                                            {
                                                ChartArea.CalibrationMarks.RemoveAll(t=>t.Name==find.Name);
                                                ChartArea.CalibrationMarks.Add(mark);
                                            }
                                        }
                                        return find.Name;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return "";
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="Name"></param>
        private void UpdateCalibration(string Name)
        {
           List<IMarker> find= ChartArea.CalibrationMarks.FindAll(t=>t.Name==Name);
           if (find != null)
           {
               DateTime dt = DateTime.Now;
               foreach (IMarker mark in find)
               {
                   RectangleMarkers mk = (mark as RectangleMarkers);
                   mk.EndTime = dt;
                   mk.MarkCreatTime = mk.StartTime;
               }
           }
        }
        /// <summary>
        /// 定标失败时
        /// </summary>
        /// <param name="Name"></param>
        private void RemoveCalibration(string Name)
        {
            lock (m_calibrationLock)
                ChartArea.CalibrationMarks.RemoveAll(t => t.Name == Name);
        }
        /// <summary>
        /// 定标失败时
        /// </summary>
        /// <param name="Name"></param>
        private void RemoveAllCalibration()
        {
            lock (m_calibrationLock)
                ChartArea.CalibrationMarks.Clear();
        }
        /// <summary>
        /// 通道装换成数据通讯对象
        /// </summary>
        /// <param name="channels"></param>
        /// <returns></returns>
        private List<DataDefine> ConvertToChannel(List<Doc_Channel> channels)
        {
            List<DataDefine> define = new List<DataDefine>();
            for (int i = 0; i < channels.Count; i++)
            {
                Doc_Channel channel = channels[i];
                if (!channel.Enable)
                    continue;
                DataDefine data = new DataDefine(channel.Name);
                data.ByteCountInData = channel.ByteLenghtOfValue;
                data.DataLength = channel.LenghtInGroup;
                data.GroupID = channel.GroupID;
                data.GroupIndex = channel.IndexInGroup;
                data.MaxADValue = channel.pADMaxValue;
                data.MaxViewValue = channel.pViewMaxValue;
                data.MinADValue = channel.pADMinValue;
                data.MinViewValue = channel.pViewMinValue;
                data.ShouldConverted = channel.NeedConvert;
                data.unSignData = channel.UnSignData;
                data.ID = channel.ID;
                m_Protocol_RecChannelEventHandle(data);
                define.Add(data);
            }
            return define;
        }
        #endregion

        private void SetTimeButton_Click(object sender, EventArgs e)
        {
            DataModel.LogInstance.Default.AddLog("点击 定时设置 按钮");
            tools.SetTime setTime = new tools.SetTime();
            setTime.SetTimeHandler += setTime_SetTimeHandler;
            setTime.StartPosition = FormStartPosition.Manual;
            Point ss = PointToScreen(SetTimeButton.Location);
            setTime.Location = new Point(ss.X + SetTimeButton.Width - setTime.Width, ss.Y + SetTimeButton.Height + 5);
            setTime.AutoRunTime = m_Protocol.UserData.AutoRunTime;
            setTime.ShowDialog();

        }
        
        private bool setTime_SetTimeHandler(DateTime start, DateTime end)
        {
            bool ret = false;
            try
            {
                if (m_DeviceRun)
                {
                    AhDung.MessageTip.ShowWarning("请先停止采集");
                    return false;
                }
                ret = m_Protocol.SetAutoRunTime(start, end);
                if (ret)
                {
                    AhDung.MessageTip.ShowOk("定时生效");
                    DataModel.LogInstance.Default.AddLog(string.Format("用户定时设置成功 用户设置的定时时间为 {0}  至 {1}", start.ToString("G"), end.ToString("G")), pSystem.LogManagement.LogLevel.WARN);
                    issettime = true;
                    DataBaseHelper.Default.Update(new Doc_MainViewRecord() { ID =m_MainViewRecord.ID  },
                        new Doc_MainViewRecord()
                        {
                            //定时也需要改变记录的状态
                            Progress=(int)ProgressState.Ready,
                            Reserve4 = issettime.ToString()
                        });
                }
                else
                {
                    AhDung.MessageTip.ShowError("设定失败");
                    DataModel.LogInstance.Default.AddLog("用户定时设置失败", pSystem.LogManagement.LogLevel.ERROR);
                }
            }
            catch { }
            return ret;
        }

        private void pictrueBoxEx1_Click(object sender, EventArgs e)
        {
            WarnningSound = false;
        }


    }
}
