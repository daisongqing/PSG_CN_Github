using AwareTec.Polysmith.Cloud.UI.DataModels;
using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.Predefined;
using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.User;
using AwareTec.Polysmith.Cloud.UI.DataModels.Global;
using AwareTec.Polysmith.Cloud.UI.Forms.MainFormBlock;
using AwareTec.Polysmith.Cloud.UI.Forms.Tips;
using AwareTec.Polysmith.DataBaseCom;
using AwareTec.Polysmith.DataCenter;
using AwareTec.Polysmith.pChart;
using AwareTec.Polysmith.Protocol;
using AwareTec.Polysmith.Util;
using pSystem.Communication.Com;
using RestfulWebRequest.ApiCall;
using RestfulWebRequest.RestfulModel;
using RestfulWebRequest.RestfulTable.RestfulRequestTable;
using RestfulWebRequest.RestfulTable.RestfulResponseTable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.Cloud.UI.Forms
{
    public partial class RealDataView : UserControl
    {
        #region 私有变量
        private Views.ViewsEventArgs.SwitchPageEventArgs m_dataMoudle = null;
        private UserOperationConfig m_userOperationConfig = null;
        private DataTable m_CurrentTable = null;
        private Doc_PatientInfo m_patient = null;
        
        private Protocol.ProtocolServer m_Protocol = null;
        private List<PredefinedCalibration> m_CalibrationDefine = null;
        private bool m_isKillTask = false;
        private bool m_hasVedio = false;
        private string devName = "";
        private ViewResult m_viewResult = new ViewResult();
        private ChannelFiliter xfChannel1 = null;
        private ChannelFiliter xfChannel2 = null;
        private ChannelFiliter xfChannel3 = null;
        private ChannelFiliter xfChannel4 = null;
        private bool temporaryClear = false;
        private bool m_stop = true;
        private bool m_DeviceRun = false;
        private DateTime startruntime = new DateTime();
        private bool m_WarnningSound = false;
        private object m_calibrationLock = new object();
        private string PortName = "";
        private string matchKey = "";
        private bool _initButtonReady = false;
        //设备中是否存放了定时信息
        private bool _hassettime = false;
        #endregion

        #region 公有变量

        public Protocol.ProtocolServer Protocol = null;
        public bool isreconnection = false;
        public bool hasvideo = false;

        #endregion

        #region 事件委托

        private delegate void Default_CalibrationChangedViewDelegate(Doc_CalibrationRecord record, int typ);

        #endregion


        #region 初始化 load/dispose/init

        /// <summary>
        /// 初始化
        /// </summary>
        public RealDataView()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);//解决闪烁
            this.SetStyle(ControlStyles.Opaque, true);//解决背景重绘问题(设置不绘制窗口背景，因为重绘窗口背景会导致性能底下)
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);//解决闪烁 
            this.UpdateStyles();
            this.Load += RealDataView_Load;
            this.Resize += RealDataView_Resize;
            this.MouseWheel += RealDataView_MouseWheel;
            this.Disposed += RealDataView_Disposed;
            m_userOperationConfig = GlobalSingleton.Instance.User.UserConfig;
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RealDataView_Load(object sender, EventArgs e)
        {
            this.DingBiaoButton.Click += DingBiaoButton_Click;
            this.PasueButton.Click += PasueButton_Click;
            this.QuitButton.Click += QuitButton_Click;
            this.SetTimeButton.Click += SetTimeButton_Click;
            this.StartRunButton.Click += StartRunButton_Click;
            this.ZkTestButton.Click += ZkTestButton_Click;
            ChartArea.DrawImageBeforeHandle += ChartArea_DrawImageBeforeHandle;
            ChartArea.ChannelViewPopupHandler += ChartArea_ChannelViewPopupHandler;
            ChartArea.ChannelHeadPopupHandler += ChartArea_ChannelHeadPopupHandler;
            ChartArea.ChannelSortedHandle += ChartArea_ChannelSortedHandle;
            ChartArea.ShowOrHideChannelHandler += ChartArea_ShowOrHideChannelHandler;
            Channel.Default.CalibrationChangedViewHandle += Default_CalibrationChangedViewHandle;
            Channel.Default.ChannelChangedHandle += ChangedView;
            Channel.Default.BaseTimeLineSpan = 30;
            ChartArea.BaseTimeLineSpan = Channel.Default.BaseTimeLineSpan;
            timer1.Enabled = true;
            timer1.Interval = 1000;
            Channel.Default.AllowShowDoctor = false;
            if (!_initButtonReady)
            {
                Channel.Default.RefreshHomeMenu();
            }
            ShowInformationLabel.Text = string.Format("{1}({0}) {3} {2}岁 {4}", m_patient.PatientNo, m_patient.PatientName, m_patient.Age, m_patient.Gender, devName);
            if (isreconnection)
                ShowInformationLabel.Text = string.Format("{1}({0}) {3} {2}岁 {5}  @开始：{4}", m_patient.PatientNo, m_patient.PatientName, m_patient.Age, m_patient.Gender, m_dataMoudle.OrderItem.ActualExamTime, devName);
            m_CalibrationDefine = GlobalSingleton.Instance.PredefinedData.PredefinedCalibrations;
            Channel.Default.IsRealTimeView = true;
            //如果是初筛  不需要做定标和阻抗
            if (m_dataMoudle.OrderItem.examType == RestfulWebRequest.EnumModels.EnumModels4Table.ExamType.PrimaryScreeningTest)
            {
                DingBiaoButton.Visible = false;
                ZkTestButton.Visible = false;
                SetTimeButton.Location = ZkTestButton.Location;
            }
            ///todo 加载视频 暂时不做
            m_hasVedio = false; //Channel.Default.SystemSetting.VedioSourceUrl != "" && !Program.LocationScan;
            hasvideo = m_hasVedio;
            if (m_hasVedio)
            {
                ZkTestButton.Visible = false;
                SetTimeButton.Location = DingBiaoButton.Location;
                SetTimeButton.Visible = false;
                DingBiaoButton.Location = ZkTestButton.Location;
            }
        }

        /// <summary>
        /// 窗口调整大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RealDataView_Resize(object sender, EventArgs e)
        {
            ShowInformationLabel.Location = new Point((DingBiaoButton.Location.X - 50) / 2 - (int)ShowInformationLabel.CreateGraphics().MeasureString(ShowInformationLabel.Text, ShowInformationLabel.Font).Width / 2, ShowInformationLabel.Location.Y);
        }

        /// <summary>
        /// 鼠标滚轮调整曲线的灵敏度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RealDataView_MouseWheel(object sender, MouseEventArgs e)
        {
            CurveItem selected = ChartArea.ItemMouseOnHead();
            if (selected != null)
            {
                if (selected.IsShowValue)
                    return;
                float max = selected.PixelRate;
                DataTable dt = GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentDataTable;
                DataRow[] dr = dt.Select(string.Format("ChannelID='{0}'", selected.ID));
                if (dr.Length > 0)
                {
                    ChannelTable channel = GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelTable.Find(t => t.ChannelID == selected.ID);
                    int[] data = channel.intPixelRangArray;
                    for (int s = 0; s < data.Length; s++)
                    {
                        if (max == (data[s]))
                        {
                            int sindx = s + (e.Delta > 0 ? 0 - 1 : 1);
                            if (sindx < 0)
                                sindx = 0;
                            else if (sindx == data.Length)
                                sindx = data.Length - 1;
                            selected.PixelRate = data[sindx];
                            dr[0]["Sensitivity"] = channel.strPixelRangArray[sindx];
                            channel.Sensitivity = selected.PixelRate;
                            break;
                        }
                    }
                    ChartArea.HeadMouseWheel(selected, selected.PixelRate);
                }
            }
        }

        /// <summary>
        /// 窗口释放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RealDataView_Disposed(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            Channel.Default.ChannelChangedHandle -= ChangedView;
            Channel.Default.CalibrationChangedViewHandle -= Default_CalibrationChangedViewHandle;
            if (!m_isKillTask)
            {
                Task.Factory.StartNew(() =>
                {
                    DeviceOnLine.Default.UpdateDevices(PortName, DeviceState.OffLine);
                    EDF.Default.Reset();
                    m_Protocol.FnStop = true;
                    m_Protocol.Dispose();
                    Channel.Default.EndTime = DateTime.Now;
                    m_isKillTask = true;
                });
            }
            ChartArea.Dispose();
            Channel.Default.IsRealTimeView = false;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 通道装换成数据通讯对象
        /// </summary>
        /// <param name="channels"></param>
        /// <returns></returns>
        private List<DataDefine> ConvertToChannel(List<ChannelTable> channels)
        {
            List<DataDefine> define = new List<DataDefine>();
            for (int i = 0; i < channels.Count; i++)
            {
                ChannelTable channel = channels[i];
                if (!channel.Enable || channel.IsClone)
                    continue;
                DataDefine data = new DataDefine(channel.strName);
                data.ByteCountInData = channel.ByteLenghtOfValue;
                data.DataLength = channel.LenghtInGroup;
                data.GroupID = channel.GroupID;
                data.GroupIndex = channel.IndexInGroup;
                data.ShouldConverted = channel.NeedConvert;
                data.unSignData = channel.UnSignData;
                data.ID = channel.ID;
                data.MaxADValue = (int)channel.ADMaxValue;
                data.MinADValue = (int)channel.ADMinValue;
                m_Protocol_RecChannelEventHandle(data);
                define.Add(data);
            }
            return define;
        }

        /// <summary>
        /// 设置定时 handle
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
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
                    //定时需要改变记录的状态
                    bool isSuccess = ApiRequest.Instance.UpdateScheduledTaskFlag(new UpdateScheduledTaskFlagRequestModel()
                    {
                        id = m_dataMoudle.OrderItem.id,
                        isTimedTask = true
                    }, out ResponseModel responseModel);
                    AhDung.MessageTip.ShowOk("定时生效");
                    DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("用户定时设置成功 用户设置的定时时间为 {0}  至 {1}", start.ToString("G"), end.ToString("G")), pSystem.LogManagement.LogLevel.WARN);
                }
                else
                {
                    AhDung.MessageTip.ShowError("设定失败");
                    DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("用户定时设置失败", pSystem.LogManagement.LogLevel.ERROR);
                }
            }
            catch { }
            return ret;
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
        /// 计时器每秒刷新底部信息的状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (m_UserDataFresh != null)
                m_UserDataFresh.Invoke(m_viewResult);
        }

        #endregion

        #region 定标相关处理

        /// <summary>
        /// 生物定标时发生
        /// </summary>
        /// <param name="record"></param>
        /// <param name="typ"></param>
        private void Default_CalibrationChangedViewHandle(Doc_CalibrationRecord record, int typ)
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
                                    PredefinedCalibration find = m_CalibrationDefine.Find(t => t.Id == int.Parse(ss[0]));
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
                                                ChartArea.CalibrationMarks.RemoveAll(t => t.Name == find.Name);
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
        /// 定标更新时
        /// </summary>
        /// <param name="Name"></param>
        private void UpdateCalibration(string Name)
        {
            List<IMarker> find = ChartArea.CalibrationMarks.FindAll(t => t.Name == Name);
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

        #endregion

        #region pChart触发事件实现方法
        /// <summary>
        /// 通道顺序被调整时发生
        /// </summary>
        /// <param name="SortMap"></param>
        /// <returns></returns>
        private bool ChartArea_ChannelSortedHandle(Dictionary<string, string> SortMap)
        {
            DataTable dt = GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentDataTable.Clone();
            DataTable dat = GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentDataTable.Rows.Count > 0 ? GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentDataTable : GlobalSingleton.Instance.User.DefaultChannelConfig.CurrentDataTable;
            foreach (KeyValuePair<string, string> p in SortMap)
            {
                DataRow[] dr = dat.Select(string.Format("ChannelID='{0}'", p.Key));
                if (dr.Length > 0)
                {
                    string[] ss = p.Value.Split(';');
                    dr[0]["Index"] = int.Parse(ss[0]);
                    dr[0]["Reserve"] = ss[1];
                    dt.ImportRow(dr[0]);
                }
            }
            if (GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentDataTable.Rows.Count > 0)
                GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentDataTable = dt;
            else
                GlobalSingleton.Instance.User.DefaultChannelConfig.CurrentDataTable = dt;
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
                var item = new MainFormBlock.ChannelItemConfig();
                item.Text = Channel.Default.FindChannelTable(channel.ID).strName;
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
            ChannelTable find = GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelTable.Find(t => t.ChannelID == channel.ChannelID);
            CurveItem item = ChartArea.FindCurve(channel.ID);
            if (find != null)
            {
                find = channel;
            }
            else
            {
                //Channel.Default.CurrentSaveTable.Add(channel);
                item.TemporaryControl = true;
            }
            item.Name = channel.strName;
            item.PixelRate = channel.PixelEnable ? channel.Sensitivity : 0;
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
            item.PenColor = channel.ColorSelect;
            ChartArea.TemporaryInvalidate(m_stop);
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
            }));
        }

        /// <summary>
        /// 在曲线刷新前需要实现的内容
        /// </summary>
        /// <returns></returns>
        private bool ChartArea_DrawImageBeforeHandle()
        {
            if (Channel.Default.ShouldRefresh)
            {
                List<ChannelTable> channelTables = GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelTable;
                //DataTable dt = GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelTable;
                //DataTable dt = Channel.Default.CurrentChannelTable;
                ChartArea.BaseTimeLineSpan = Channel.Default.BaseTimeLineSpan;
                List<string> strIDs = ChartArea.getItemIDs();
                //Channel.Default.CurrentSaveTable.Clear();
                for (int i = 0; i < channelTables.Count; i++)
                {
                    ChannelTable table = channelTables[i];
                    //Channel.Default.CurrentSaveTable.Add(table);
                    pChart.CurveItem find = ChartArea.FindCurve(table.ID,table.IsClone);
                    if (find == null)
                    {
                        find = Channel.Default.CreatChannel(channelTables[i]);
                        if (table.IsClone)
                        {
                            pChart.CurveItem clonechannel = ChartArea.FindCurve(table.ID);
                            if (clonechannel != null)
                            {
                                find.CloneDataSource(clonechannel);
                                clonechannel.PushDataToCloneItemHanlde += find.RecivePushData;
                            }
                        }
                        else if (table.ID.ToString().Contains("Append_"))
                        {

                        }
                        ChartArea.AddCurve(find);
                    }
                    else
                    {
                        find.Name = table.strName;
                        find.Visible = table.State;
                        find.ChannelNo = table.Index;
                        find.yAxis.SetMaxMinValue(table.MaxValue, table.MinValue);
                        find.yAxis.LegendLables.Clear();
                        find.yAxis.LegendLables.Add(find.yAxis.MinValue.ToString());
                        find.yAxis.LegendLables.Add(find.yAxis.MaxValue.ToString());
                        find.PixelRate = table.Sensitivity;
                        find.ValueZoomRate = 1;// table.ValueZoomRate;暂时去掉倍数
                        find.TimeSpan = 1000 / table.SpanTime;
                        find.PenColor = table.ColorSelect;
                        find.SingleNotch = table.SingleNotch;
                        find.HighPass = table.HighPass;
                        find.LowPass = table.LowPass;
                        find.belong = Convert.ToInt32(table.Reserve.Split(':')[0]);
                        find.Antipole = table.Antipole;
                        if (temporaryClear)
                            find.TemporaryControl = false;
                        (find.Tag as ChannelFiliter).Reset();
                        strIDs.Remove(table.ChannelID);
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

        /// <summary>
        /// 显示或者隐藏信道/曲线 handle
        /// </summary>
        /// <param name="curveItem"></param>
        private void ChartArea_ShowOrHideChannelHandler(CurveItem curveItem)
        {
            ChannelTable channelTable = GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelTable.First(t => t.ID == curveItem.ChannelNum && t.strName == curveItem.Name);
            DataRow[] dr = GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentDataTable.Select(string.Format("ID='{0}' AND strName='{1}'", curveItem.ChannelNum, curveItem.Name));
            //DataRow[] dr = Channel.Default.CurrentChannelTable.Select(string.Format("ID='{0}'", curveItem.ID));
            if (dr.Length != 1 || channelTable == null)
                throw new Exception("数据似乎有些错误");
            bool isVisible = Convert.ToBoolean(dr[0]["State"]);
            dr[0]["State"] = !isVisible;
            channelTable.State = !isVisible;
            //GlobalSingleton.Instance.User.CurrentChannelConfig.ChannelProperties.
            Channel.Default.ChannelChanged();
        }

        #endregion

        #region 与设备建立通讯过程

        /// <summary>
        /// 开始监听
        /// </summary>
        /// <param name="client"></param>
        /// <param name="channelconfig"></param>
        public bool Start(IDevice client, Control containControl)
        {
            ProgressTipForm.Defalut.Text = "进入PSG监听模式";
            ProgressTipForm.ResultTag = null;
            ProgressTipForm.Defalut.Argument = new object[] { client, containControl };
            ProgressTipForm.Defalut.DoWork += Defalut_DoWork;
            bool ret = ProgressTipForm.Defalut.ShowDialog() == DialogResult.OK;
            return ret;
        }

        #region 异步线程对话框置顶写法
        private bool ShowMessage(string msg, Control containControl)
        {
            object ss = containControl.Invoke(new MessageBoxShow(MessageBoxShow_F), new object[] { msg });
            return Convert.ToBoolean(ss);
        }
        delegate bool MessageBoxShow(string msg);
        bool MessageBoxShow_F(string msg)
        {
            return MessageForm.Show(msg, "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes;
        }
        #endregion
        private void Defalut_DoWork(ProgressTipForm sender, DoWorkEventArgs e)
        {
            object[] args = e.Argument as object[];
            IDevice client = args[0] as IDevice;
            Control containControl = args[1] as Control;
            ///下列动作需在调用ConvertToChannel函数前执行完毕，因为ConvertToChannel用到了EDF类中的Channels
            DataTable dat = GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentDataTable;
            if (client is BthWLans)
            {
                (client as BthWLans).ReconnectHandle += RealDataView_ReconnectHandle;
            }
            for (int i = 0; i < dat.Rows.Count; i++)
            {
                ChannelTable table = ChannelTable.ConvertToChannel(dat.Rows[i]);
                EDF.Default.ConvertToChannelItemEx(table.ChannelID, table.SpanTime);
            }
            try
            {
                m_Protocol = new Protocol.ProtocolServer(client.PortClient, ConvertToChannel(GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelTable));
                Protocol = m_Protocol;
                PortName = client.MatchKey;
            }
            catch (Exception ee)
            {
                sender.SetError(string.Format("缺少vc_redist.x64 驱动包：{0}", ee.Message));
                goto getOut;
            }
            if (sender.CancellationPending)
            {
                e.Cancel = true;
                goto getOut;
            }
            List<IDevice> findalldevs = new List<IDevice>();
            if (m_userOperationConfig.TypeOfLastScan ==DataModels.EnumModels.ScanMethod.Local)
            {
                findalldevs.AddRange(DeviceOnLine.Default.GetDevices());
            }
            else
            {
                findalldevs.AddRange(DeviceOnWLan.Default.getDevices());
            }
            IDevice finddevs = findalldevs.Find(t => t.DeviceName == client.DeviceName);
            if (finddevs == null)
            {
                sender.SetError("当前设备处理离线模式，请稍后重试！");
                goto getOut;
            }
            devName = finddevs.DeviceName;
            sender.SetProgress(5, string.Format("连接设备{0}...", devName));
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
            if (client.DeviceAddr != null)
            {
                if (client.PortClient.DataSend(ASCIIEncoding.ASCII.GetBytes(string.Format("AT+LECCONN={0}\r\n", client.DeviceAddr.Trim()))))
                {
                    //break;
                }
            }
            m_Protocol.PatientNo = m_patient.PatientNo;
            m_Protocol.RecUserDataHandle += m_Protocol_RecUserDataHandle;
            m_Protocol.FnStop = false;
            m_isKillTask = false;
            if (sender.CancellationPending)
            {
                e.Cancel = true;
                goto getOut;
            }
            sender.SetProgress(20, "设备初始化...");
            System.Threading.Thread.Sleep(2000);
            if (!m_Protocol.InitDeviceInfo())
            {
                sender.SetError(string.Format("{0}初始化失败", devName));
                goto getOut;
            }
            if (!m_Protocol.GetDeviceSNCode())
            {
                sender.SetError(string.Format("{0}获取SN号失败", devName));
                goto getOut;
            }
            ///为了兼容不带定时功能的设备，暂时不判断
            m_Protocol.GetAutoRunTime();
            System.Threading.Thread.Sleep(500);
            if (!string.IsNullOrEmpty(m_Protocol.UserData.AutoRunTime))
                _hassettime = true;
            bool isSuccess = ApiRequest.Instance.HasEquipment(new RestfulWebRequest.RestfulTable.RestfulRequestTable.HasEquipmentRequestModel()
            {
                sn = m_Protocol.UserData.DeviceSNCode
            }, out ResponseModel responseModel);
            if (!isSuccess)
            {
                sender.SetError("服务端请求判断设备存在性失败");
                goto getOut;
            }
            var model = (responseModel as ResponseSuccessModel<HasEquipmentResponseModel>).RestfulTable;
            if (!model.exist)
            {
                sender.SetError(string.Format("{0}无授权！", devName));
                goto getOut;
            }
            if (m_Protocol.UserData.DeviceBusy || _hassettime)
            {
                sender.SetProgress(60, string.Format("获取与{0}匹配的病例信息...", devName));
                if (!m_Protocol.GetPatientIDRequest())
                {
                    sender.SetError(string.Format("{0}的病例信息获取失败", devName));
                    goto getOut;
                }
                else
                {
                    string key = m_Protocol.UserData.MatchKey;
                    if (key == m_dataMoudle.OrderItem.id)
                    {
                        if (ShowMessage(string.Format("已找到与当前设备信息匹配的历史病例({0} {1} {2})，是否要继续接入监测？", m_patient.PatientNo, m_patient.PatientName, m_dataMoudle.OrderItem.ActualExamTime), containControl))
                        {
                            sender.SetProgress(75, "启动继续监听...");
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
                                Channel.Default.RefreshHomeMenu();
                                containControl.Invoke(new MethodInvoker(() =>
                                {
                                    DeviceOnLine.Default.UpdateDevices(PortName, DeviceState.Running);
                                    //DingBiaoButton.Enabled = false;
                                    PasueButton.Enabled = true;
                                    StartRunButton.Enabled = !string.IsNullOrEmpty(m_Protocol.UserData.AutoRunTime) && !m_Protocol.UserData.DeviceBusy;
                                    SetTimeButton.Enabled = false;
                                    //ZkTestButton.Enabled = false;
                                    StartRunButton.Text = !string.IsNullOrEmpty(m_Protocol.UserData.AutoRunTime) ? "开始采集" : "停止采集";
                                    m_stop = false;
                                    ChartArea.FnStop = false;
                                    m_hasVedio = false;// Channel.Default.SystemSetting.VedioSourceUrl != "" && m_userOperationConfig.TypeOfLastScan == DataModels.EnumModels.ScanMethod.Remote;
                                    hasvideo = m_hasVedio;
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
                        if (_hassettime)
                        {
                            sender.SetError(string.Format("该设备已经定时(病例号 {0})，不可监测其他病例！",m_Protocol.UserData.PatientInfo.Split(' ')[0]));
                            goto getOut;
                        }
                        else
                        {
                            sender.SetError(string.Format("设备处于其他病例(病例号 {0})PSG睡眠监测中！", m_Protocol.UserData.PatientInfo.Split(' ')[0]));
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
            sender.SetProgress(60, "写入病例信息...");
            matchKey = m_dataMoudle.OrderItem.id;///订单号
            //为了字符总长度不超过80，这里对patientno和patientname的长度进行限制
            string patientnoinmatchkey = m_patient.PatientNo;
            string patientnameinmatchkey = m_patient.PatientName;
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
                if (!m_Protocol.SetPatientIDRequest(string.Format("{0} {1} {2:dd-MM-yyyy} {3} {4}", patientnoinmatchkey, m_patient.Gender, m_patient.BirthDate, patientnameinmatchkey, matchKey)))
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
            } while (m_Protocol.UserData.MatchKey != matchKey && trycount < 5);
            if (trycount >= 5)
            {
                sender.SetError("病例信息写入失败：匹配码不一致！");
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("匹配码不一致,下发匹配码{0}，收到匹配码{1}", matchKey, m_Protocol.UserData.MatchKey), pSystem.LogManagement.LogLevel.ERROR);
                goto getOut;
            }
            if (sender.CancellationPending)
            {
                e.Cancel = true;
                goto getOut;
            }
            sender.SetProgress(75, "写入设备信息...");
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
        last:
            Channel.Default.StartTime = DateTime.Now;
            sender.SetProgress(90, "加载界面...");
            System.Threading.Thread.Sleep(500);
            if (sender.CancellationPending)
            {
                e.Cancel = true;
                goto getOut;
            }
            sender.SetProgress(100, "完成");
            Channel.Default.HomePageRefresh = true;
            EDF.Default.DeviceSNCode = m_Protocol.UserData.DeviceSNCode;
            goto doupdate;
        getOut:
            DeviceOnLine.Default.UpdateDevices(client.MatchKey, DeviceState.OffLine);
        goreturn:
            m_Protocol.Dispose();
        doupdate:
            Task.Factory.StartNew(() =>
            {
                if (ProgressTipForm.ResultTag != null)
                    Channel.Default.UpdateRecord(Convert.ToInt32(ProgressTipForm.ResultTag));
            });
        }

        private void RealDataView_ReconnectHandle(IOClient newportClient)
        {
            System.Threading.Thread.Sleep(1000);
            m_Protocol.UpdatePortClient(newportClient);
            m_Protocol.SetContinueSampleRequest();
        }
        #endregion

        #region 实时数据接收时处理
        private void m_Protocol_RecUserDataHandle(UserDataDefine UserData)
        {
            m_viewResult.BatteryCapacity = UserData.BatteryCapacity;
        }
        /// <summary>
        /// 注册数据接收事件
        /// </summary>
        /// <param name="Channel"></param>
        private void m_Protocol_RecChannelEventHandle(Protocol.DataDefine Channel)
        {
            pChart.CurveItem find = ChartArea.FindCurve(Channel.ID,false);
            if (find != null)
            {
                List<string> ids = ChartArea.getItemIDs();
                ids.Remove(find.ID);///去掉本体通道
                List<string> subids = ids.FindAll(t => t.Replace("Clone_", "") == find.ID);
                if (subids != null)
                {
                    ///克隆的通道绑定其本体通道数据来源
                    for (int i = 0; i < subids.Count; i++)
                    {
                        pChart.CurveItem item = ChartArea.FindCurve(subids[i]);
                        if (item != null)
                            Channel.RecOneDataEventHandle += item.AddDatavalue;
                    }
                }
                Channel.RecOneDataEventHandle += find.AddDatavalue;
                //17 血氧 BloodOxygen 18 体位 BodyState 19环境光 AmbientLight 20 脉率 PulseRate
                //11 胸呼吸 ChestBreathing 12 腹呼吸 AbdominalBreathing 15 鼻压力 PressureMuzzleFlow 16 热敏 ThermalMuzzleFlow
                //22 电量
                ///实时存储暂时去掉
                //ChannelItem one = EDF.Default.Channels.Find(t => t.ID == Channel.ID);
                //if (one != null)
                //    Channel.RecOneEdfDataEventHandle += one.AddData;
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

        private void ThermalMuzzleFlow_RecOneDataEventHandle(float data, bool Invaild, string msg)
        {
            float value = xfChannel4.getHeartRate((int)data);
            m_viewResult.ThermalBreathingRate = string.Format("热敏呼吸率：{0}次/min", xfChannel4.getBreathChannelState(data) ? value.ToString() : "--");
        }
        private void PressureMuzzleFlow_RecOneDataEventHandle(float data, bool Invaild, string msg)
        {
            float value = xfChannel3.getHeartRate((int)data);
            m_viewResult.Pressure = string.Format("压力值：{0}cmH2O", Math.Round(data / 98.067, 3));//pa转厘米水柱
            m_viewResult.PressureBreathingRate = string.Format("压力呼吸率：{0}次/min", xfChannel3.getBreathChannelState(data) ? value.ToString() : "--");
        }
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


        #region 按钮方法

        /// <summary>
        /// 阻抗按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZkTestButton_Click(object sender, EventArgs e)
        {
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("点击 阻抗测试 按钮");
            RealDataViewBlock.ImpedanceView view = new RealDataViewBlock.ImpedanceView();
            view.Init(m_Protocol);
            ChartArea.FnStop = true;
            view.ShowDialog();
            ChartArea.FnStop = false;
            m_Protocol.FnStop = m_stop;
        }

        /// <summary>
        /// 开始采集按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartRunButton_Click(object sender, EventArgs e)
        {
            string des = StartRunButton.Text;
            ///todo 加载视频 暂时不做
            m_hasVedio = false;//// Channel.Default.SystemSetting.VedioSourceUrl != "" && !Program.LocationScan;
            hasvideo = m_hasVedio;
            if (des == "停止采集")
            {
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("点击 停止采集 按钮");
                if (MessageForm.Show(string.Format("数据采集设备正常工作中，是否执行停止操作{0}？", m_hasVedio ? "(视频监控同时停止)" : ""), "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("用户确认停止采集");
                    Task.Factory.StartNew(() =>
                    {
                        if (m_Protocol.StopSampleRequest())
                        {
                            ChartArea.FnStop = true;
                            EDF.Default.Reset();
                            m_Protocol.FnStop = true;
                            Channel.Default.EndTime = DateTime.Now;
                            Channel.Default.RefreshHomeMenu();
                            this.Invoke(new MethodInvoker(() =>
                            {
                                DingBiaoButton.Enabled = false;
                                PasueButton.Enabled = false;
                                SetTimeButton.Enabled = true;
                                m_stop = false;
                                m_DeviceRun = false;
                                PasueButton.Text = "暂停";
                                StartRunButton.Text = "开始采集";
                                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("数据停止采集执行成功!", pSystem.LogManagement.LogLevel.WARN);
                                AhDung.MessageTip.ShowOk("数据停止采集执行成功!");
                            }));
                        }
                        else
                        {
                            AhDung.MessageTip.ShowError("数据停止采集执行失败!");
                            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("数据停止采集执行失败!", pSystem.LogManagement.LogLevel.ERROR);
                        }
                    });
                }
                else
                {
                    DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("用户取消停止采集");
                }
            }
            else
            {
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("点击 开始采集 按钮");
                Task.Factory.StartNew(() =>
                {
                    //开始采集接口运行一次就会更改状态，再次执行就会失败，所以定时任务 在开始采集前不需要执行接口
                    if (!_hassettime)
                    {
                        bool isSuccess = ApiRequest.Instance.StartMonitoring(new StartMonitoringRequestModel()
                        {
                            id = m_dataMoudle.OrderItem.id,
                            sn = m_Protocol.UserData.DeviceSNCode,
                        }, out ResponseModel responseModel);
                        if (!isSuccess)
                        {
                            if (responseModel.HttpStatusCode == System.Net.HttpStatusCode.Forbidden)
                            {
                                AhDung.MessageTip.ShowWarning("订单已过预约时间，无法开始采集！");
                                return;
                            }
                        }
                    }
                    m_Protocol.FnStop = false;

                    if (m_Protocol.StartSampleRequest())
                    {
                        ChartArea.FnStop = false;
                        _initButtonReady = true;
                        Channel.Default.RefreshHomeMenu();
                        //EDF.Default.Init(matchKey);
                        this.Invoke(new MethodInvoker(() =>
                        {
                            DataModels.DeviceOnLine.Default.UpdateDevices(PortName, DeviceState.Running);
                            DingBiaoButton.Enabled = true;
                            PasueButton.Enabled = true;
                            SetTimeButton.Enabled = false;
                            m_stop = false;
                            PasueButton.Text = "暂停";
                            StartRunButton.Text = "停止采集";
                            m_DeviceRun = true;
                            AhDung.MessageTip.ShowOk("数据开始采集执行成功!");
                            startruntime = DateTime.Now;
                            ShowInformationLabel.Text = string.Format("{1}({0}) {3} {2}岁 {5}  @开始：{4}", m_patient.PatientNo, m_patient.PatientName, m_patient.Age, m_patient.Gender, startruntime, devName);
                            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("数据开始采集执行成功!", pSystem.LogManagement.LogLevel.WARN);

                        }));
                    }
                    else
                    {
                        AhDung.MessageTip.ShowError("数据开始采集执行失败!");
                        DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("数据开始采集执行失败!", pSystem.LogManagement.LogLevel.ERROR);
                    }
                });
            }
        }

        /// <summary>
        /// 定时按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetTimeButton_Click(object sender, EventArgs e)
        {
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("点击 定时设置 按钮");
            RealDataViewBlock.SetTime setTime = new RealDataViewBlock.SetTime();
            setTime.SetTimeHandler += setTime_SetTimeHandler;
            setTime.StartPosition = FormStartPosition.Manual;
            Point ss = PointToScreen(SetTimeButton.Location);
            setTime.Location = new Point(ss.X + SetTimeButton.Width - setTime.Width, ss.Y + SetTimeButton.Height + 5);
            setTime.AutoRunTime = m_Protocol.UserData.AutoRunTime;
            setTime.ShowDialog();
            //如果定时 不经过开始采集 需要改变 首页记录状态
            if(setTime.DialogResult== DialogResult.OK)
            {
                if (m_dataMoudle.OrderItem.status == RestfulWebRequest.EnumModels.EnumModels4Table.OrderStatus.ToBeMonitored)
                {
                    bool isSuccess = ApiRequest.Instance.StartMonitoring(new StartMonitoringRequestModel()
                    {
                        id = m_dataMoudle.OrderItem.id,
                        sn = m_Protocol.UserData.DeviceSNCode
                    }, out ResponseModel responseModel);
                    _hassettime = true;
                }
            }
        }

        /// <summary>
        /// 退出按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuitButton_Click(object sender, EventArgs e)
        {
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("点击 红色的× 退出按钮");
            if (MessageForm.Show(string.Format("是否退出实时监测页面(数据采集{0}不会中断)？", m_hasVedio ? "和视频监控均" : ""), "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("用户确定退出实时监测页面");
                ChartArea.FnStop = true;
                EDF.Default.Reset();
                m_Protocol.FnStop = true;
                m_stop = false;
                m_DeviceRun = false;
                this.Dispose();
            }
            else
            {
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("用户取消退出实时监测页面");
            }
        }

        /// <summary>
        /// 暂停按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasueButton_Click(object sender, EventArgs e)
        {
            if (!m_stop)
            {
                m_stop = true;
                m_Protocol.FnStop = true;
                PasueButton.Text = "继续";
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("点击 实时监测-暂停 按钮");
            }
            else
            {
                m_stop = false;
                m_Protocol.FnStop = false;
                PasueButton.Text = "暂停";
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("点击 实时监测-继续 按钮");
            }
        }

        /// <summary>
        /// 定标按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DingBiaoButton_Click(object sender, EventArgs e)
        {
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("点击 定标 按钮");
            RealDataViewBlock.Calibration cail = new RealDataViewBlock.Calibration();
            cail.Protocol = m_Protocol;
            cail.StartPosition = FormStartPosition.CenterParent;
            cail.Tag = m_dataMoudle.OrderItem.id;
            cail.ShowDialog();
        }

        #endregion

        #region 公有方法
        #region  实时数据更新时触发事件定义
        public delegate void UserDataFreshDelegate(ViewResult data);
        private event UserDataFreshDelegate m_UserDataFresh;
        /// <summary>
        /// 实时数据更新触发
        /// </summary>
        public event UserDataFreshDelegate UserDataFreshHandler
        {
            add
            {
                if (m_UserDataFresh != null)
                {
                    m_UserDataFresh = null;
                }
                m_UserDataFresh += value;
            }
            remove
            {
                m_UserDataFresh = null;
            }
        }

        #endregion
        /// <summary>
        /// 传入通道配置表
        /// </summary>
        /// <param name="table"></param>
        public void InitChannel(DataTable table, Views.ViewsEventArgs.SwitchPageEventArgs e)
        {
            m_dataMoudle = e;
            m_CurrentTable = table;
            m_patient = GlobalSingleton.Instance.convetToPatient(e.OrderItem.patient);
            for (int i = 0; i < m_CurrentTable.Rows.Count; i++)
            {
                CurveItem item = Channel.Default.CreatChannel(m_CurrentTable.Rows[i]);
                ChartArea.AddCurve(item);
            }
        }
        /// <summary>
        /// 保留最后一次使用的montage名称
        /// </summary>
        /// <param name="name"></param>
        public void UpdateMontageName(string name)
        {
            //todo 暂时不做
            //if (m_MainViewRecord != null)
            //    DataBaseHelper.Default.Update(new Doc_MainViewRecord()
            //    {
            //        GUID = m_MainViewRecord.GUID
            //    },
            //    new Doc_MainViewRecord()
            //    {
            //        ReviewMontageName = name
            //    });
        }
        /// <summary>
        /// 键盘按下时触发
        /// </summary>
        /// <param name="key"></param>
        public bool mainKeyDown(KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F && e.Shift)
            {
                RealTimeDataView_KeyDown(null);
                return true;
            }
            else if (e.Control && e.KeyCode == Keys.F && !e.Shift)
            {
                RealTimeDataView_KeyDown();
                return true;
            }
            return false;
        }
        /// <summary>
        /// 全通道自适应
        /// </summary>
        /// <param name="sender"></param>
        private void RealTimeDataView_KeyDown(object sender)
        {
            List<ChannelTable>  tables= GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelTable;
            for (int i = 0; i < tables.Count; i++)
            {
                ChannelTable table = tables[i];
                if (table.Sensitivity == 0 || !table.State)
                    continue;
                pChart.CurveItem find = ChartArea.FindCurve(table.ChannelID);
                AutoScale(find,true);
            }
            Channel.Default.ShouldRefresh = true;
            ChartArea.ChartInvalidate();
        }
        /// <summary>
        /// 单通道自适应
        /// </summary>
        private void RealTimeDataView_KeyDown()
        {
            pChart.CurveItem find = ChartArea.GetCurrentItem();
            AutoScale(find, true);
        }
        /// <summary>
        /// 自适应灵敏度
        /// </summary>
        /// <param name="find"></param>
        /// <param name="needFresh"></param>
        private void AutoScale(CurveItem find, bool needFresh = false)
        {
            if (find == null)
                return;
            if (find.PixelRate == 0)
                return;
            int max = find.getMaxValue();
            if (max == 0)
                return;
            else
            {
                ChannelTable channel = GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelTable.Find(t => t.ChannelID == find.ID);

                int[] data = channel.intPixelRangArray;
                float rr = ChartArea.pRate;
                bool ok = false;
                DataTable dt = GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentDataTable;

                DataRow[] dr = dt.Select(string.Format("ChannelID='{0}'", find.ID));
                if (dr.Length > 0)
                {
                    for (int s = 0; s < data.Length; s++)
                    {
                        if (max < (data[s] * rr))
                        {
                            find.PixelRate = data[s];
                            dr[0]["Sensitivity"] = channel.strPixelRangArray[s];
                            channel.Sensitivity = find.PixelRate;
                            ok = true;
                            break;
                        }
                    }
                    if (!ok)
                    {
                        find.PixelRate = data[data.Length - 1];
                        dr[0]["Sensitivity"] = channel.strPixelRangArray[data.Length - 1];
                        channel.Sensitivity = find.PixelRate;
                    }
                    find.TemporaryControl = true;
                }
            }
            if (needFresh)
            {
                Channel.Default.ShouldRefresh = true;
                ChartArea.ChartInvalidate();
            }
        }
        #endregion
    }
}
