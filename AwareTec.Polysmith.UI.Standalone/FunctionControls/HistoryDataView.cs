using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AhDung;
using AwareTec.Polysmith.DataBaseCom;
using AwareTec.Polysmith.pChart;
using AwareTec.Polysmith.UI.DataModel;
using AwareTec.Polysmith.Util;
using AwareTec.Polysmith.Vedio;
using pSystem.Interface.Util;

namespace AwareTec.Polysmith.UI.FunctionControls
{
    public partial class HistoryDataView : UserControl
    {
        #region 私有变量定义
        private MyConfiguration m_myConfiguration = null;
        internal DataModel.DataManger dataManager = null;
        private tools.HScrollBarEx m_HScrollBarEx1 = null;
        private EDFInstance m_EdfInstance = null;
        private List<IMarker> m_MultSleepList = new List<IMarker>();
        private List<float> Spo2Values = new List<float>();
        private List<float> HeartValues = new List<float>();
        private List<float> posValues = new List<float>();
        private List<float> mtValues = new List<float>();
        private List<float> breathValues = new List<float>();
        private List<float> CalibrationValues = new List<float>();
        private List<float> PressureValues = new List<float>();
        private object m_lockObj = new object();
        private RoundAngleButton bak_butt = null;
        private bool temporaryClear = false;
        private object m_lockPage = new object();
        private bool m_vedioRun = true;
        private string eventpath = "";
        /// <summary>
        /// 是否已经加载界面
        /// </summary>
        private bool m_isLoad = false;
        private MainForm m_parent = null;
        private string m_VedioViewPath = "";
        /// <summary>
        /// 记录文件的开始时间
        /// </summary>
        private DateTime m_Start = DateTime.Now.AddHours(-8);
        /// <summary>
        /// 记录文件的结束时间
        /// </summary>
        private DateTime m_end = DateTime.Now;
        private int m_TotalFrameCnt = 960;
        private string KeyInfo = "";

        private bool m_auotAnalysis = false;
        /// <summary>
        /// 本次的操作记录次数
        /// </summary>
        private int m_operatCnt = 0;

        /// <summary>
        /// 是否是联动变化
        /// </summary>
        private bool m_AutoValueChange = false;
        private bool m_Compelet = false;
        /// <summary>
        /// 是否是视频带动帧跳转
        /// </summary>
        private bool m_vedioAutoJump = false;
        #endregion

        #region 构造函数\Load\Dispose
        public HistoryDataView()
        {
            InitializeComponent();
            base.Name = "HistoryDataView";
            m_HScrollBarEx1 = new tools.HScrollBarEx
            {
                Dock = DockStyle.Fill
            };
            m_HScrollBarEx1.ValueChanged += m_HScrollBarEx1_ValueChanged;
            panel2.Controls.Add(m_HScrollBarEx1);
            Dock = DockStyle.Fill;
            base.Load += HistoryDataView_Load;
            reportChart1.CurrentFrameChangedHandler += reportChart1_CurrentFrameChangedHandler;
            reportChart1.SleepMarkChangedHandler += ReportChart1_SleepMarkChangedHandler;
            reportChart1.HSrollBarExUpdateHandele += ReportChart1_HSrollBarExUpdateHandele;
            reportChart1.curveAreaMultSleepUpdateHandele += ReportChart1_curveAreaMultSleepUpdateHandele;
            reportChart1.QueryScoreLockStatusHappend += ReportChart1_QueryScoreLockStatusHappend;
            reportChart1.createmultsleephandle += ReportChart1_createmultsleephandle;
            reportChart1.editmultsleephandle += ReportChart1_editmultsleephandle;
            ChartArea.DrawImageBeforeHandle += ChartArea_DrawImageBeforeHandle;
            ChartArea.ChannelHeadPopupHandler += ChartArea_ChannelHeadPopupHandler;
            ChartArea.ChannelSortedHandle += ChartArea_ChannelSortedHandle;
            ChartArea.SelectedHappenHandler += ChartArea_SelectedHappenHandler;
            ChartArea.ReadChannelDataExHandler += ChartArea_ReadChannelDataExHandler;
            ChartArea.JumpHappenHandler += ChartArea_JumpHappenHandler;
            ChartArea.MouseUpChangeFrameHandle += ChartArea_MouseMoveUpHandler;
            Channel.Default.BaseTimeLineSpan = 30;
            base.Disposed += HistoryDataView_Disposed;
            ViewClientMenu.Closed += ViewClientMenu_Closed;
            initEventTable();
            dataManager = new DataModel.DataManger();
        }
        #region 剪切edf文件 进度条数据回传
        public delegate void SearchProgressBarValueDelegate(long barvalue);
        public event SearchProgressBarValueDelegate SearchProgressBarValueEvenetHandle;
        private void M_EdfInstance_SearchProgressBarValueEventHandle(long BarValue)
        {
            if (SearchProgressBarValueEvenetHandle != null)
                SearchProgressBarValueEvenetHandle.Invoke(BarValue);
        }
        #endregion
        private void ReportChart1_editmultsleephandle(List<IMarker> multsleeplist, RectangleMarkers sleepmark, MouseEventArgs e)
        {
            tools.AddMultSleep addsleep = new tools.AddMultSleep();
            addsleep.IsEdit = true;
            addsleep.MultipleSleepMarks = multsleeplist;
            addsleep.CreatingSleepHandler += ReportChart1_SleepMarkChangedHandler;
            addsleep.SleepMark = multsleeplist.Find(t => (t as RectangleMarkers).ID == sleepmark.ID && (t as RectangleMarkers).StartTime.Year != 1 && (t as RectangleMarkers).EndTime.Year != 1) as RectangleMarkers;
            addsleep.SetMaxMinDate(m_end, m_Start);
            addsleep.Dock = DockStyle.Fill;
            Point reportchartx = this.reportChart1.Location;
            Size panelsize = this.panel3.Size;
            Point startpoint = new Point(reportchartx.X + e.X, panelsize.Height + e.Y);
            Block.Moudle mm = new Block.Moudle();
            mm.Text = "修改小睡";
            mm.Size = new Size(addsleep.Width, addsleep.Height + mm.CaptionHeight);
            mm.Controls.Add(addsleep);
            mm.StartPosition = FormStartPosition.Manual;
            mm.Location = startpoint;
            mm.ShowDialog();
            reportChart1.Refresh();
        }

        private void ReportChart1_createmultsleephandle(List<IMarker> multsleeplist, RectangleMarkers sleepmark, MouseEventArgs e)
        {
            tools.AddMultSleep addsleep = new tools.AddMultSleep();
            addsleep.IsEdit = false;
            addsleep.MultipleSleepMarks = multsleeplist;
            addsleep.CreatingSleepHandler += ReportChart1_SleepMarkChangedHandler;
            addsleep.SleepMark = multsleeplist.Find(t => (t as RectangleMarkers).ID == sleepmark.ID && (t as RectangleMarkers).StartTime.Year != 1 && (t as RectangleMarkers).EndTime.Year != 1) as RectangleMarkers;
            addsleep.SetMaxMinDate(m_end, m_Start);
            addsleep.Dock = DockStyle.Fill;
            Point reportchartx = this.reportChart1.Location;
            Size panelsize = this.panel3.Size;
            Point startpoint = new Point(reportchartx.X + e.X, panelsize.Height + e.Y);
            Block.Moudle mm = new Block.Moudle();
            mm.Text = "添加小睡";
            mm.Size = new Size(addsleep.Width, addsleep.Height + mm.CaptionHeight);
            mm.Controls.Add(addsleep);
            mm.StartPosition = FormStartPosition.Manual;
            mm.Location = startpoint;
            mm.ShowDialog();
            reportChart1.Refresh();
        }
        private void HistoryDataView_Load(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            Channel.Default.AllowShowDoctor = true;
            m_parent = (base.ParentForm as MainForm);
            m_parent.EditEdfByOnOffTimeEventHandler -= M_parent_EditEdfByOnOffTimeEventHandler;
            m_parent.EditEdfByOnOffTimeEventHandler += M_parent_EditEdfByOnOffTimeEventHandler;
            ViewClientMenu.KeyDown += ViewClientMenu_KeyDown;
            ChartArea.TimeLineVisible = true;
            //RemoteDataInteraction.Default.ExtraTaskRunningHandler += Default_ExtraTaskRunningHandler;
            ChartArea.ChannelViewPopupHandler += chart1_ChannelViewPopupHandler;
            ChartArea.MarkerMouseDoubleClick += ChartArea_MarkerMouseRightClick;
            ChartArea.SelectRectangleKeyDownHandler += ChartArea_SelectRectangleKeyDownHandler;
            ChartArea.MarkTypeChanged += ChartArea_MarkTypeChanged;
            ChartArea.ShowOrHideChannelHandler += ChartArea_ShowOrHideChannelHandler;
            ChartArea.VideoStatusChanged += ChartArea_VideoStatusChanged;
            Channel.Default.ChannelChangedHandle += ChangedView;
            Channel.Default.MarkColorChangedHandle += Default_MarkColorChangedHandle;
            Channel.Default.DefineMarksChangeHandle += Default_DefineMarksChangeHandle;
            base.MouseWheel += HistoryDataView_MouseWheel;
            m_Start = ((Channel.Default.StartTime.Year == 1) ? m_Start : Channel.Default.StartTime);
            timer1.Interval = 1000;
            timer1.Enabled = false;
            subMenubutton_Click(button2, null);
            SleepStateChange(1);
            if (CalibrationValues.Exists(t => t > 0))
                AddCalibration();
            else
                addCalibration();
            ChartArea.Invalidate(m_Start, 1, 0, m_TotalFrameCnt);
            if (ResultDomain.Default.SleepStagPoints != null)
            {
                for (int i = 0; i < m_TotalFrameCnt && i < ResultDomain.Default.SleepStagPoints.Length; i++)
                {
                    if (ResultDomain.Default.SleepStagPoints[i] != null)
                    {
                        float yMaxValue = ResultDomain.Default.SleepStagPoints[i].YMaxValue;
                        ChartArea.SleepStage[i] = ((yMaxValue == 1f) ? "N3" : ((yMaxValue == 2f) ? "N2" : ((yMaxValue == 3f) ? "N1" : ((yMaxValue == 4f) ? "R" : ((yMaxValue == 5f) ? "W" : "")))));
                    }
                    if (ResultDomain.Default.BodyStatePoints[i] != null)
                    {
                        ChartArea.SleepPos[i] = (int)ResultDomain.Default.BodyStatePoints[i].YMaxValue;
                    }
                }
            }
            m_HScrollBarEx1.BaseTimeLine = ChartArea.BaseTimeLineSpan;
            m_HScrollBarEx1.MoveOffValue = ChartArea.MoveTimeSpan;
            m_isLoad = true;
            SetInfo();
            DateTime lightonTime = ChartArea.pChartMarks.Find(t => t.ID == string.Format("255:{0}", pChart.IMarker.MarkType.LightOn.ToString())) != null ? Channel.Default.AnalysisReult.Sleep.LightOnTime : default(DateTime);
            DateTime lightoffTime = ChartArea.pChartMarks.Find(t => t.ID == string.Format("255:{0}", pChart.IMarker.MarkType.LightOff.ToString())) != null ? Channel.Default.AnalysisReult.Sleep.LightOffTime : default(DateTime);
            reportChart1.SetLightONOFF(lightoffTime, lightonTime);
            initLoad();
            if (Channel.Default.AnalysisReult.Epochs.Count == 0)
            {
                for (int j = 0; j < m_TotalFrameCnt; j++)
                {
                    Channel.Default.AnalysisReult.Epochs.Add(new Doc_Epochs
                    {
                        EpochIndex = j
                    });
                }
            }
            (this.ParentForm as MainForm).ControlTimer(true);
            ChartArea.SetHotKeys(MarkerManage.Default.getHotKeys());
            try
            {
                Doc_MainViewRecord record = DataBaseHelper.Default.Select(new Doc_MainViewRecord() { GUID = m_guid });
                if (record != null)
                {
                    eventpath = record.Reserve5;
                    m_VedioViewPath = record.Reserve3;
                    if (Path.GetFileName(m_VedioViewPath) == record.MatchKey)
                    {
                        m_VedioViewPath = Path.GetDirectoryName(m_VedioViewPath);
                    }
                }
                else
                {
                    m_VedioViewPath = Channel.Default.SystemSetting.VedioSavePath;
                }
                //m_VedioViewPath = @"F:\菲诗奥软件工程\PSG项目源码\5.0版本PSG软件源码\AwareTec.Polysmith\AwareTec.Polysmith.UI\bin\Release\Media\2021-06-05";
            }
            catch (Exception ee)
            {
                DataModel.LogInstance.Default.AddLog(string.Format("进入回放页面 根据Guid查找记录出错 {0}", ee), pSystem.LogManagement.LogLevel.ERROR);
            }

            m_myConfiguration = new MyConfiguration()
            {
                SaveDirectory = m_VedioViewPath,
                UrlPath = Channel.Default.SystemSetting.VedioSourceUrl,
                MatchKey = MatchKey,
                EDFTotalTimes = (int)(m_end - m_Start).TotalMilliseconds
            };
            m_myConfiguration.Init();
            Console.WriteLine(string.Format("历史回放加载总时间：{0}ms", (DateTime.Now - dt).TotalMilliseconds));
        }


        private void HistoryDataView_Disposed(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            timer1.Enabled = false;
            Channel.Default.ChannelChangedHandle -= ChangedView;
            Channel.Default.MarkColorChangedHandle -= Default_MarkColorChangedHandle;
            Channel.Default.ProgressStauts = DataModel.ProgressState.None;
            //if (Channel.Default.CurrentChannelTable.Rows.Count > 0)
            //{
            //    ChannelManage.Default.SaveChannelConfig(Channel.Default.CurrentChannelTable, Channel.Default.CurrentChannelPath);
            //}
            //else
            //    ChannelManage.Default.SaveChannelConfig(Channel.Default.DefultChannelTable, Channel.Default.DefaultChannelPath);
            timer1.Dispose();
            ChartArea.Dispose();
            Channel.Default.AnlysisFinish = false;
            if (eventpath != Channel.Default.SystemSetting.SaveEdfPath)
            {
                //更新resere5字段
                DataBaseCom.Doc_MainViewRecord oldrec = new Doc_MainViewRecord()
                {
                    GUID = m_guid
                };
                DataBaseCom.Doc_MainViewRecord newrec = new Doc_MainViewRecord()
                {
                    Reserve5 = Channel.Default.SystemSetting.SaveEdfPath
                };
                DataBaseHelper.Default.Update(oldrec, newrec);
                Channel.Default.UpdateRecord(Convert.ToInt32(Channel.Default.AnalysisReult.Tag));
            }
            Channel.Default.AnalysisReult.Clear();///清楚所有解析数据结果
            Channel.Default.AllowShowDoctor = false;
            //DataModel.RemoteDataInteraction.Default.DeleteEdfSource();///删除edf文件
            Channel.Default.IsBreathOnly = false;
            SetInfo(false);
            if (play != null)
            {
                m_vPlayer.CloseAll();
                play.Close();
                play = null;
            }
            SourceDispose();
            Console.WriteLine(string.Format("历史回放关闭耗时：{0}ms", (DateTime.Now - dt).TotalMilliseconds));
        }
        #endregion

        #region 覆盖默认的系统键处理方式，遇到方向键，则直接返回，系统不处理，这样键值就会被传递到窗体，触发KeyDown事件
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down ||
                         keyData == Keys.Left || keyData == Keys.Right)
                return false;
            else
            {
                if (keyData == Keys.Space || keyData == Keys.Enter)
                {
                    if (m_vPlayer != null)
                    {
                        m_vPlayer.KeyDown(null, new KeyEventArgs(keyData));
                        return true;
                    }
                }
                return base.ProcessDialogKey(keyData);
            }
        }
        #endregion

        #region 内部使用
        private void Default_MarkColorChangedHandle(IMarker marker)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                if (marker is StringMarkers)
                {
                    List<IMarker> list = ChartArea.pChartMarks.FindAll((IMarker t) => t.MarkTyp == marker.MarkTyp);
                    if (list != null)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            IMarker marker2 = list[i];
                            marker2.ForeColor = marker.BackColor;
                            marker2.Description = marker.Description;
                        }
                    }
                }
                else
                {
                    RectangleMarkers mark = marker as RectangleMarkers;
                    for (int j = 0; j < mark.OnChannelIndexs.Length; j++)
                    {
                        CurveItem curveItem = ChartArea.FindCurve(mark.OnChannelIndexs[j]);
                        if (curveItem != null)
                        {
                            List<IMarker> list2 = curveItem.CurrentMarks.FindAll((IMarker t) => t.MarkTyp == mark.MarkTyp);
                            if (list2 != null)
                            {
                                for (int k = 0; k < list2.Count; k++)
                                {
                                    IMarker marker3 = list2[k];
                                    marker3.ForeColor = marker.BackColor;
                                    marker3.Description = marker.Description;
                                }
                            }
                        }
                    }
                }
                ChartArea.ChartInvalidate();
            }));
        }
        private bool M_parent_EditEdfByOnOffTimeEventHandler(DateTime starttime, DateTime stoptime, bool byLightOnOff)
        {
            List<DateTime> startTimes = new List<DateTime>();
            List<DateTime> stopTimes = new List<DateTime>();
            string[] offTimes = new string[2] { "0", "0" };
            if (byLightOnOff)
            {
                if (starttime.Year == 1 || stoptime.Year == 1)
                {
                    AhDung.MessageTip.ShowError(Program.Language == "EN" ? "Unmarked on/off light event" : "未标记开关灯");
                    DataModel.LogInstance.Default.AddLog("用户未标记开关灯事件", pSystem.LogManagement.LogLevel.ERROR);
                    return false;
                }
                startTimes.Add(starttime);
                stopTimes.Add(stoptime);
                if (!m_EdfInstance.readEdfDataByMultSleep(startTimes, stopTimes, m_EdfInstance.DataSource))
                {
                    AhDung.MessageTip.ShowError(Program.Language == "EN" ? "Please return to the homepage, re-enter playback, and try again" : "请返回首页,重新进入回放,重试");
                    DataModel.LogInstance.Default.AddLog("裁剪EDF文件失败", pSystem.LogManagement.LogLevel.ERROR);
                    return false;
                }
                return true;
            }
            else
            {
                for (int i = 0; i < m_MultSleepList.Count; i++)
                {
                    RectangleMarkers MultSleepMarker = m_MultSleepList[i] as RectangleMarkers;
                    if (MultSleepMarker != null && m_Start.Year != 1)
                    {
                        offTimes = MultSleepMarker.Comments2.Split('-');
                        if (offTimes.Length == 2 && m_Start.Year != 1)
                        {
                            startTimes.Add(m_Start.AddSeconds(MultSleepMarker.StartIndex * 30 + Convert.ToInt32(offTimes[0])));
                            stopTimes.Add(m_Start.AddSeconds(MultSleepMarker.EndIndex * 30 + Convert.ToInt32(offTimes[1])));
                        }
                    }
                    DataModel.LogInstance.Default.AddLog(string.Format("用户标记的第{0}个小睡事件 开始时间为{1} 结束时间为{2}", i + 1, startTimes[i], stopTimes[i]), pSystem.LogManagement.LogLevel.DEBUG);
                }
                if (stopTimes.Count == startTimes.Count && m_MultSleepList.Count > 0)
                {
                    if (!m_EdfInstance.readEdfDataByMultSleep(startTimes, stopTimes, m_EdfInstance.DataSource))
                    {
                        AhDung.MessageTip.ShowError(Program.Language == "EN" ? "Please return to the homepage, re-enter playback, and try again" : "请返回首页,重新进入回放,重试");
                        DataModel.LogInstance.Default.AddLog("裁剪EDF文件失败", pSystem.LogManagement.LogLevel.ERROR);
                        return false;
                    }
                    else
                        return true;
                    //return m_EdfInstance.readEdfDataByMultSleep(startTimes, stopTimes, m_EdfInstance.DataSource);
                }
                else
                {
                    AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "Unmarked nap events" : "未标记小睡事件");
                    DataModel.LogInstance.Default.AddLog("用户未标记小睡事件", pSystem.LogManagement.LogLevel.ERROR);
                    return false;
                }
            }
        }
        private void ViewClientMenu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            this.Refresh();///刷新状态，当右键菜单弹出而不进行任何操作时，用于去掉颜色标记的选择范围
        }
        /// <summary>
        /// 界面更新
        /// </summary>
        private void ChangedView(bool TemporaryInvalidate = false)
        {
            if (TemporaryInvalidate)
            {
                m_HScrollBarEx1.BaseTimeLine = ChartArea.BaseTimeLineSpan = Channel.Default.BaseTimeLineSpan;
                m_HScrollBarEx1.MoveOffValue = ChartArea.MoveTimeSpan;
                ChartArea.TemporaryInvalidate(true);
            }
            else
            {
                temporaryClear = true;
                ChartArea.ChartInvalidate();
            }
        }
        /// <summary>
        /// 一帧数据被加载时执行
        /// </summary>
        /// <param name="frameCnt"></param>
        private void SleepStateChange(int frameCnt)
        {
            string[] array = new string[10];
            for (int i = 0; i < array.Length; i++)
            {
                int num = frameCnt + i - 4;
                if (num < 1 || num > m_TotalFrameCnt)
                {
                    array[i] = "";
                }
                else if (ResultDomain.Default.SleepStagPoints != null && num - 1 < ResultDomain.Default.SleepStagPoints.Length && ResultDomain.Default.SleepStagPoints[num - 1] != null)
                {
                    float yMaxValue = ResultDomain.Default.SleepStagPoints[num - 1].YMaxValue;
                    array[i] = ((yMaxValue == 5f) ? "W" : ((yMaxValue == 4f) ? "R" : ((yMaxValue == 3f) ? "1" : ((yMaxValue == 2f) ? "2" : ((yMaxValue == 1f) ? "3" : "")))));
                }
            }
            this.SleepStateChangedHandle.BeginInvoke(array, frameCnt, null, null);
        }
        private void Default_DefineMarksChangeHandle()
        {
            ChartArea.SetHotKeys(MarkerManage.Default.getHotKeys());
        }
        private void HistoryDataView_MouseWheel(object sender, MouseEventArgs e)
        {
            CurveItem curveItem = ChartArea.ItemMouseOnHead();
            if (curveItem == null)
            {
                if (e.Delta > 0)
                {
                    m_HScrollBarEx1.UserKeyDown(1, pageAll: true);
                }
                else
                {
                    m_HScrollBarEx1.UserKeyDown(2, pageAll: true);
                }
            }
            else
            {
                if (curveItem.IsShowValue)
                {
                    return;
                }
                float pixelRate = curveItem.PixelRate;
                DataTable currentChannelTable = Channel.Default.CurrentChannelTable;
                DataRow[] array = currentChannelTable.Select(string.Format("ID='{0}'", curveItem.ID));
                if (array.Length <= 0)
                {
                    return;
                }
                string ID = curveItem.ID.Replace("Clone_", "").Replace("Append_", "").Split(':')[0];
                Doc_Channel doc_Channel = Channel.Default.ChannelProperties.Find((Doc_Channel t) => t.Name == ID);
                int[] intPixelRangArray = doc_Channel.intPixelRangArray;
                for (int i = 0; i < intPixelRangArray.Length; i++)
                {
                    if (pixelRate == (float)intPixelRangArray[i])
                    {
                        int num = i + ((e.Delta <= 0) ? 1 : (-1));
                        if (num < 0)
                        {
                            num = 0;
                        }
                        else if (num == intPixelRangArray.Length)
                        {
                            num = intPixelRangArray.Length - 1;
                        }
                        curveItem.PixelRate = intPixelRangArray[num];
                        array[0]["Sensitivity"] = doc_Channel.strPixelRangArray[num];
                        ChannelTable ctt = Channel.Default.CurrentSaveTable.Find((ChannelTable t) => t.ID == ID);
                        if (ctt != null)
                            ctt.PixelRate = curveItem.PixelRate;
                        break;
                    }
                }
                ChartArea.HeadMouseWheel(curveItem, curveItem.PixelRate);
            }
        }
        /// <summary>
        /// 基本信息
        /// </summary>
        /// <param name="visible"></param>
        private void SetInfo(bool visible = true)
        {
            m_parent.OneLable.Visible = visible;
            m_parent.OneLable.Text = visible ? string.Format(Program.Language == "EN" ? "No.: {0}" : "病例号:{0}", Channel.Default.Patient.PatientNo) : "    ";
            m_parent.TwoLable.Visible = visible;
            m_parent.TwoLable.Text = visible ? string.Format(Program.Language == "EN" ? "Name: {0}" : "姓名:{0}", Channel.Default.Patient.PatientName) : "    ";
            m_parent.TwoLable.ForeColor = Color.Black;//预防可能的变色
            m_parent.ThreeLable.Visible = visible;
            m_parent.ThreeLable.Text = visible ? string.Format(Program.Language == "EN" ? "Gender: {0}" : "性别:{0}", Channel.Default.Patient.Gender) : "    ";
            m_parent.ThreeLable.ForeColor = Color.Black;//预防可能的变色
            m_parent.FourLable.Visible = visible;
            m_parent.FourLable.Text = visible ? string.Format(Program.Language == "EN" ? "Age: {0}" : "年龄:{0}岁", Channel.Default.Patient.Age) : "    ";
            m_parent.FiveLabel.Visible = visible;
            m_parent.FiveLabel.Text = visible ? string.Format(Program.Language == "EN" ? "RecordTime: {0}" : "诊断时间:{0}", Channel.Default.Patient.RecordTime) : "    ";
            m_parent.SixLabel.Visible = visible;
            m_parent.SixLabel.Text = visible ? string.Format(Program.Language == "EN" ? "Doctor: {0}" : "评分医师:{0}", Channel.Default.Doctor.Name) : "    ";
            m_parent.SevenLable.Visible = visible;
            m_parent.SevenLable.Text = visible ? string.Format(Program.Language == "EN" ? "Data Source: {0}" : "数据源:{0}", Path.GetFileName(EdfPath)) : "    ";
            m_parent.EightLable.Visible = false;
            m_parent.BatteryCapacity.Visible = false;
        }
        /// <summary>
        /// 从数据库加载定标数据
        /// </summary>
        private void addCalibration()
        {
            List<Doc_CalibrationDefine> list = DataBaseHelper.Default.SelectAll(new Doc_CalibrationDefine());
            Doc_CalibrationRecord doc_CalibrationRecord = DataBaseHelper.Default.Select(new Doc_CalibrationRecord
            {
                MatchKey = MatchKey
            });
            if (doc_CalibrationRecord == null || !(doc_CalibrationRecord.Comments != ""))
            {
                return;
            }
            new List<int>();
            string[] array = doc_CalibrationRecord.Comments.Split(new char[1]
            {
                ';'
            }, StringSplitOptions.RemoveEmptyEntries);
            if (array.Length <= 0)
            {
                return;
            }
            ChartArea.CalibrationMarks.Clear();
            for (int i = 0; i < array.Length; i++)
            {
                string[] ss = array[i].Split(':');
                if (ss.Length != 2)
                {
                    continue;
                }
                string[] array2 = ss[1].Split('-');
                if (array2.Length != 2)
                {
                    continue;
                }
                Doc_CalibrationDefine doc_CalibrationDefine = list.Find((Doc_CalibrationDefine t) => t.ID == int.Parse(ss[0]));
                DateTime startTime = doc_CalibrationRecord.StartTime.AddMilliseconds(float.Parse(array2[0]));
                DateTime endTime = doc_CalibrationRecord.StartTime.AddMilliseconds(float.Parse(array2[1]));
                if (doc_CalibrationDefine != null)
                {
                    string[] array3 = doc_CalibrationDefine.ChannelID.Split('/');
                    for (int j = 0; j < array3.Length; j++)
                    {
                        RectangleMarkers rectangleMarkers = new RectangleMarkers();
                        rectangleMarkers.ID = EDF.Default.ConvertToChannelIDEx(int.Parse(array3[j]));
                        rectangleMarkers.StartTime = startTime;
                        rectangleMarkers.EndTime = endTime;
                        rectangleMarkers.Name = doc_CalibrationDefine.Name;
                        ChartArea.CalibrationMarks.Add(rectangleMarkers);
                    }
                }
            }
        }
        /// <summary>
        /// 从本地文件加载数据
        /// </summary>
        private void AddCalibration()
        {
            List<Doc_CalibrationDefine> CalibrationDefinelist = DataBaseHelper.Default.SelectAll(new Doc_CalibrationDefine());
            Dictionary<int, string> dic_cailbrationvalue = new Dictionary<int, string>();
            float excessivevalue = 0;
            float endindex = 0;
            float startindex = 0;
            int realvalue = 0;
            bool findend = false;
            ChartArea.CalibrationMarks.Clear();
            for (int index = CalibrationValues.Count - 1; index >= 0; index--)
            {
                if (dic_cailbrationvalue.Count < CalibrationDefinelist.Count)
                {
                    if (CalibrationValues[index] == excessivevalue)
                    {
                        continue;
                    }
                    if (!findend)
                    {
                        if (dic_cailbrationvalue.Keys.Contains((int)CalibrationValues[index]))
                        {
                            continue;
                        }
                        realvalue = (int)CalibrationValues[index];
                        endindex = index;
                        findend = true;
                        excessivevalue = CalibrationValues[index];
                    }
                    else
                    {
                        startindex = index + 1;
                        dic_cailbrationvalue.Add(realvalue, string.Format("{0}-{1}", startindex, endindex));
                        Doc_CalibrationDefine doc_CalibrationDefine = CalibrationDefinelist.Find((Doc_CalibrationDefine t) => t.ID == realvalue);
                        if (doc_CalibrationDefine != null)
                        {
                            string[] array3 = doc_CalibrationDefine.ChannelID.Split('/');
                            for (int j = 0; j < array3.Length; j++)
                            {
                                RectangleMarkers rectangleMarkers = new RectangleMarkers();
                                rectangleMarkers.ID = EDF.Default.ConvertToChannelIDEx(int.Parse(array3[j]));
                                rectangleMarkers.StartTime = m_Start.AddSeconds(startindex);
                                rectangleMarkers.EndTime = m_Start.AddSeconds(endindex + 1);
                                rectangleMarkers.Name = doc_CalibrationDefine.Name;
                                ChartArea.CalibrationMarks.Add(rectangleMarkers);
                            }
                        }
                        findend = false;
                        excessivevalue = 0;
                        if (CalibrationValues[index] != 0)
                            index++;
                    }
                }
            }
        }
        private void subMenubutton_Click(object sender, EventArgs e)
        {
            RoundAngleButton button = (RoundAngleButton)sender;
            if (this.bak_butt != null)
            {
                if (this.bak_butt.Name == button.Name)
                {
                    return;
                }
                this.bak_butt.NormalColor = Color.WhiteSmoke;
            }
            button.NormalColor = Color.FromArgb(160, 185, 255);
            this.reportChart1.ShowLightOnAndOff = true;
            this.reportChart1.ChangeVLineWith = true;
            string name;
            this.reportChart1.yAxis.CalibrationsColors.Clear();
            switch (name = button.Name)
            {
                case "button1":
                    this.reportChart1.Initialize(ResultDomain.Default.HypopneaPoints, this.m_TotalFrameCnt, this.m_Start);
                    this.reportChart1.yAxis.Interval = 2;
                    this.reportChart1.yAxis.MinValue = 0f;
                    this.reportChart1.yAxis.MaxValue = 30f;
                    this.reportChart1.yAxis.LegendLables = (from t in this.reportChart1.yAxis.Calibrations
                                                            select t.ToString()).ToList<string>();
                    break;
                case "button2":
                    this.reportChart1.Initialize(ResultDomain.Default.SleepStagPoints, this.m_TotalFrameCnt, this.m_Start);
                    this.reportChart1.YLegends = new List<string>
                {
                    " ",
                    "N3",
                    "N2",
                    "N1",
                    "R",
                    "W"
                };
                    this.reportChart1.yAxis.CalibrationsColors = new List<Color>() { Color.DarkCyan, Color.LightGreen, Color.GreenYellow, Color.Red, Color.DimGray };
                    break;
                case "button3":
                    this.reportChart1.Initialize(ResultDomain.Default.HeartRatePoints, this.m_TotalFrameCnt, this.m_Start);
                    this.reportChart1.yAxis.Interval = 5;
                    this.reportChart1.yAxis.MaxValue = 190f;
                    this.reportChart1.yAxis.MinValue = 30f;
                    this.reportChart1.yAxis.LegendLables = (from t in this.reportChart1.yAxis.Calibrations
                                                            select t.ToString()).ToList<string>();
                    break;
                case "button4":
                    this.reportChart1.Initialize(ResultDomain.Default.BloodOxygenPoints, this.m_TotalFrameCnt, this.m_Start);
                    this.reportChart1.yAxis.Interval = 5;
                    this.reportChart1.yAxis.MaxValue = 100f;
                    this.reportChart1.yAxis.MinValue = 50f;
                    this.reportChart1.yAxis.LegendLables = (from t in this.reportChart1.yAxis.Calibrations
                                                            select t.ToString()).ToList<string>();
                    break;
                case "button5":
                    this.reportChart1.Initialize(ResultDomain.Default.CAPoints, this.m_TotalFrameCnt, this.m_Start);
                    this.reportChart1.yAxis.Interval = 2;
                    this.reportChart1.yAxis.MinValue = 0f;
                    this.reportChart1.yAxis.MaxValue = 30f;
                    this.reportChart1.yAxis.LegendLables = (from t in this.reportChart1.yAxis.Calibrations
                                                            select t.ToString()).ToList<string>();
                    break;
                case "button6":
                    this.reportChart1.Initialize(ResultDomain.Default.OAPoints, this.m_TotalFrameCnt, this.m_Start);
                    this.reportChart1.yAxis.Interval = 2;
                    this.reportChart1.yAxis.MinValue = 0f;
                    this.reportChart1.yAxis.MaxValue = 30f;
                    this.reportChart1.yAxis.LegendLables = (from t in this.reportChart1.yAxis.Calibrations
                                                            select t.ToString()).ToList<string>();
                    break;
                case "button7":
                    this.reportChart1.Initialize(ResultDomain.Default.MAPoints, this.m_TotalFrameCnt, this.m_Start);
                    this.reportChart1.yAxis.Interval = 2;
                    this.reportChart1.yAxis.MinValue = 0f;
                    this.reportChart1.yAxis.MaxValue = 30f;
                    this.reportChart1.yAxis.LegendLables = (from t in this.reportChart1.yAxis.Calibrations
                                                            select t.ToString()).ToList<string>();
                    break;
                case "button8":
                    this.reportChart1.Initialize(ResultDomain.Default.BodyStatePoints, this.m_TotalFrameCnt, this.m_Start);
                    this.reportChart1.YLegends = new List<string>
                {
                    " ",
                    "S",
                    "L",
                    "R",
                    "P",
                    "UP"
                };
                    reportChart1.yAxis.CalibrationsColors = new List<Color>() { Color.Olive, Color.LightGreen, Color.DarkCyan, Color.Gold, Color.DimGray };
                    break;
                case "button9":
                    this.reportChart1.ChangeVLineWith = false;
                    this.reportChart1.Initialize(ResultDomain.Default.MTPoints, this.m_TotalFrameCnt, this.m_Start);
                    this.reportChart1.yAxis.Interval = 5;
                    this.reportChart1.yAxis.MinValue = 0f;
                    this.reportChart1.yAxis.MaxValue = 500f;
                    this.reportChart1.yAxis.LegendLables = (from t in this.reportChart1.yAxis.Calibrations
                                                            select t.ToString()).ToList<string>();
                    break;
                case "button14":
                    this.reportChart1.Initialize(ResultDomain.Default.MicArousalPoints, this.m_TotalFrameCnt, this.m_Start);
                    this.reportChart1.yAxis.Interval = 2;
                    this.reportChart1.yAxis.MinValue = 0f;
                    this.reportChart1.yAxis.MaxValue = 30f;
                    this.reportChart1.yAxis.LegendLables = (from t in this.reportChart1.yAxis.Calibrations
                                                            select t.ToString()).ToList<string>();
                    break;
                case "button12":
                    this.reportChart1.Initialize(ResultDomain.Default.PLMPoints, this.m_TotalFrameCnt, this.m_Start);
                    this.reportChart1.yAxis.Interval = 2;
                    this.reportChart1.yAxis.MinValue = 0f;
                    this.reportChart1.yAxis.MaxValue = 30f;
                    this.reportChart1.yAxis.LegendLables = (from t in this.reportChart1.yAxis.Calibrations
                                                            select t.ToString()).ToList<string>();
                    break;
                case "button13":
                    this.reportChart1.Initialize(ResultDomain.Default.PLMsPoints, this.m_TotalFrameCnt, this.m_Start);
                    this.reportChart1.yAxis.Interval = 2;
                    this.reportChart1.yAxis.MinValue = 0f;
                    this.reportChart1.yAxis.MaxValue = 30f;
                    this.reportChart1.yAxis.LegendLables = (from t in this.reportChart1.yAxis.Calibrations
                                                            select t.ToString()).ToList<string>();
                    break;

            }
            this.reportChart1.Refresh();
            this.bak_butt = button;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = Channel.Default.SystemSetting.PlaySpanTime * 1000;
            int currentNo = m_HScrollBarEx1.CurrentFrameCnt;
            if (currentNo == m_TotalFrameCnt)
            {
                ///播放到最后一帧停止
                //timer1.Enabled = false;
                //(this.ParentForm as MainForm).ribbonButtonFrameRun.SmallImage = Properties.Resources.fRun;
                //(this.ParentForm as MainForm).ribbonButtonFrameRun.Value = "4";
                //return;
                currentNo = 0;
            }
            m_AutoValueChange = false;
            m_HScrollBarEx1.CurrentFrameCnt = currentNo + 1;
        }
        private void reportChart1_CurrentFrameChangedHandler(int frameCnt)
        {
            m_HScrollBarEx1.CurrentFrameCnt = frameCnt;
            m_AutoValueChange = true;
        }
        private void ReportChart1_HSrollBarExUpdateHandele(List<IMarker> MultipleSleepMarks)
        {
            m_HScrollBarEx1.DrawMultipleSleepMarks(MultipleSleepMarks);
        }
        private void ReportChart1_curveAreaMultSleepUpdateHandele(List<IMarker> MultipleSleepMarks)
        {
            ChartArea.DrawcurveAreaMultSleepUpdate(MultipleSleepMarks);

        }
        private bool ReportChart1_QueryScoreLockStatusHappend()
        {
            return Channel.Default.ScoreLock;
        }
        private void m_HScrollBarEx1_ValueChanged(int frameCnt, bool antoJump)
        {
            DateTime dt = DateTime.Now;
            CurrentFrameNo = frameCnt;
            m_startViewFrameNo = frameCnt;
            if (m_vPlayer != null && !antoJump)
            {
                m_runpage = true;
                m_vPlayer.SetCurrentTime((frameCnt - 1) * 30000 + 100, true);///翻页多加1s
                m_runpage = false;
            }
            //if (frameCnt != ChartArea.CurrentFrameNo)
            {
                lock (m_lockPage)
                {
                    m_vedioRun = false;
                    ChartArea.Invalidate(m_Start, frameCnt, m_HScrollBarEx1.FrameOffsetValue, m_TotalFrameCnt);
                    m_vedioRun = true;
                }
                if (!m_AutoValueChange)
                {
                    reportChart1.CurrentFrameNo = frameCnt;
                }
                m_AutoValueChange = false;
                SleepStateChange(frameCnt);
            }
            m_vedioAutoJump = false;
            Console.WriteLine(string.Format("睡眠分期翻页耗时：{0}ms", (DateTime.Now - dt).TotalMilliseconds));
        }
        private void freshreportChart()
        {
            if (bak_butt.Tag != null)
            {
                SuperPointF[] data = null;
                switch (Convert.ToInt32(bak_butt.Tag))
                {
                    case 0:
                        data = DataModel.ResultDomain.Default.CAPoints;
                        break;
                    case 1:
                        data = DataModel.ResultDomain.Default.OAPoints;
                        break;
                    case 2:
                        data = DataModel.ResultDomain.Default.MAPoints;
                        break;
                    case 3:
                        data = DataModel.ResultDomain.Default.HypopneaPoints;
                        break;
                    case 4:
                        data = DataModel.ResultDomain.Default.MTPoints;
                        break;
                    case 5:
                        data = DataModel.ResultDomain.Default.SleepStagPoints;
                        break;
                    case 6:
                        data = DataModel.ResultDomain.Default.BloodOxygenPoints;
                        break;
                    case 8:
                        data = DataModel.ResultDomain.Default.PLMPoints;
                        break;
                    case 11:
                        data = DataModel.ResultDomain.Default.PLMsPoints;
                        break;
                    case 7:
                    case 12:
                    case 13:
                    case 14:
                        data = DataModel.ResultDomain.Default.MicArousalPoints;
                        break;
                }
                this.Invoke(new MethodInvoker(() =>
                {
                    reportChart1.Invalidate(data);
                }));
            }
        }
        /// <summary>
        /// 多次小睡标记发生更改时触发
        /// </summary>
        /// <param name="currents"></param>
        private void ReportChart1_SleepMarkChangedHandler(List<IMarker> currents)
        {
            m_MultSleepList = currents;
            dataManager.dataFactory.EventsInstance.Delete((int)IMarker.MarkType.MultipleSleep, false);
            List<DataMangement.EventRecordsUit.EventRecord> records = new List<DataMangement.EventRecordsUit.EventRecord>();
            for (int i = 0; i < currents.Count; i++)
            {
                RectangleMarkers rectMark = currents[i] as RectangleMarkers;
                DataMangement.EventRecordsUit.EventRecord record = new DataMangement.EventRecordsUit.EventRecord()
                {
                    ByHand = true,
                    StartIndex = rectMark.StartIndex,
                    EndIndex = rectMark.EndIndex,
                    MarkType = (int)rectMark.MarkTyp,
                    Comments = rectMark.Comments2,
                    Condition = dataManager.ConvertEventCondition(DataMangement.UpdateMode.Insert)
                };
                records.Add(record);
            }
            dataManager.dataFactory.EventsInstance.AddRange(records);
            Channel.Default.CanRecover = true;
        }
        #endregion

        #region Chart事件触发实现
        private void ChartArea_VideoStatusChanged(bool play, DateTime currentTime)
        {
            if (m_vPlayer != null)
            {
                if (!play)
                {
                    m_runpage = true;
                    // m_vPlayer.KeyDown(null, new KeyEventArgs(Keys.Space));
                }
                else
                {
                    m_runpage = false;
                    m_vPlayer.SetCurrentTime((int)(currentTime - m_Start).TotalMilliseconds, true);
                    //m_vPlayer.KeyDown(null, new KeyEventArgs(Keys.Space));
                }
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
        private void ChartArea_SelectedHappenHandler(int belong, int TimeSpanLine)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                m_HScrollBarEx1.MoveOffValue = ChartArea.MoveTimeSpan;
                m_HScrollBarEx1.BaseTimeLine = TimeSpanLine;
                (this.ParentForm as MainForm).ChangeTimeSpan(TimeSpanLine);
            }));
        }
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
        private bool ChartArea_DrawImageBeforeHandle()
        {
            if (Channel.Default.ShouldRefresh)
            {
                DataTable currentChannelTable = Channel.Default.CurrentChannelTable;
                currentChannelTable = ((currentChannelTable.Rows.Count == 0) ? Channel.Default.DefultChannelTable : currentChannelTable);
                m_HScrollBarEx1.BaseTimeLine = ChartArea.BaseTimeLineSpan = Channel.Default.BaseTimeLineSpan;
                m_HScrollBarEx1.MoveOffValue = ChartArea.MoveTimeSpan;
                List<string> itemIDs = ChartArea.getItemIDs();
                Channel.Default.CurrentSaveTable.Clear();
                for (int i = 0; i < currentChannelTable.Rows.Count; i++)
                {
                    ChannelTable channelTable = ChannelTable.ConvertToChannel(currentChannelTable.Rows[i]);
                    Channel.Default.CurrentSaveTable.Add(channelTable);
                    CurveItem curveItem = ChartArea.FindCurve(channelTable.ID);
                    if (curveItem == null)
                    {
                        curveItem = Channel.Default.CreatChannel(currentChannelTable.Rows[i]);
                        if (channelTable.ID.Contains("Clone_"))
                        {
                            CurveItem curveItem2 = ChartArea.FindCurve(channelTable.ID.Replace("Clone_", ""));
                            if (curveItem2 != null)
                            {
                                curveItem.CloneDataSource(curveItem2);
                            }
                        }
                        else if (channelTable.ID.Contains("Append_"))
                        {
                            string[] ids2 = channelTable.ID.Replace("Append_", "").Split(':');
                            if (ids2.Length == 2)
                            {
                                curveItem.CloneDataSource(ChartArea.FindCurve(ids2[0]), ChartArea.FindCurve(ids2[1]));
                            }
                        }
                        ChartArea.AddCurve(curveItem);
                        continue;
                    }
                    curveItem.Name = channelTable.Name;
                    curveItem.Visible = channelTable.Visible;
                    curveItem.ChannelNo = channelTable.ChannelNo;
                    curveItem.yAxis.SetMaxMinValue(channelTable.MaxValue, channelTable.MinValue);
                    curveItem.yAxis.LegendLables.Clear();
                    curveItem.yAxis.LegendLables.Add(curveItem.yAxis.MinValue.ToString());
                    curveItem.yAxis.LegendLables.Add(curveItem.yAxis.MaxValue.ToString());
                    curveItem.PixelRate = channelTable.PixelRate;
                    curveItem.TimeSpan = 1000 / channelTable.TimeSpan;
                    curveItem.PenColor = channelTable.PenColor;
                    curveItem.SingleNotch = channelTable.SingleNotch;
                    curveItem.HighPass = channelTable.HighPass;
                    curveItem.LowPass = channelTable.LowPass;
                    curveItem.belong = channelTable.Belong;
                    curveItem.Antipole = channelTable.Antipole;
                    curveItem.DBaseLineVisible = channelTable.DBaseLineVisible;
                    if (temporaryClear)
                    {
                        curveItem.TemporaryControl = false;
                    }
                    (curveItem.Tag as ChannelFiliter).Reset();
                    itemIDs.Remove(channelTable.ID);
                }
                for (int j = 0; j < itemIDs.Count; j++)
                {
                    ChartArea.RemoveCurve(itemIDs[j]);
                }
                Channel.Default.ShouldRefresh = false;
                temporaryClear = false;
                return true;
            }
            return false;
        }
        #region 事件部分操作
        /// <summary>
        /// 主动读取edf数据
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="curveItems"></param>
        /// <returns></returns>
        private bool ChartArea_ReadChannelDataExHandler(DateTime start, DateTime end, List<CurveItem> curveItems)
        {
            if (start < m_Start)
                start = m_Start;
            if (end > m_end)
                end = m_end;
            bool ret = m_EdfInstance.readData(start, end, curveItems);
            ///每次获取通道数据要记得给克隆通道和合成通道进行数据重赋值
            for (int i = 0; i < curveItems.Count; i++)
            {
                CurveItem item = curveItems[i];

                if (item.ID.Contains("Clone_"))
                {
                    CurveItem curveItem2 = ChartArea.FindCurve(item.ID.Replace("Clone_", ""));
                    if (curveItem2 != null)
                    {
                        item.CloneDataSource(curveItem2);
                    }
                }
                else if (item.ID.Contains("Append_"))
                {
                    string[] ids2 = item.ID.Replace("Append_", "").Split(':');
                    if (ids2.Length == 2)
                    {
                        item.CloneDataSource(ChartArea.FindCurve(ids2[0]), ChartArea.FindCurve(ids2[1]));
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 曲线区域框选后左键菜单
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="buttons"></param>
        private void chart1_ChannelViewPopupHandler(pChart.CurveItem channel, MouseButtons buttons)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                if (channel.IsMarkerChanged)
                {
                    #region 事件框选区域被更改时具体业务
                    if (channel.CurrentMarks.Count > 0)
                    {
                        changeMarkRange(channel.CurrentMarks[0], channel.removeMarks);
                        this.freshEvents();
                    }
                    return;
                    #endregion
                }
                m_Compelet = false;
                ViewClientMenu.Items.Clear();
                if (buttons == MouseButtons.Left)
                {
                    List<IMarker> all = MarkerManage.Default.SearchMarkers(channel.ID);
                    if (all.Count > 0 && !Channel.Default.ScoreLock)
                    {
                        ///如果允许自动标记，即当该通道有且仅有一种事件类型时，框选后即事件标记生效，免去选择过程
                        if (Channel.Default.IsAutoMark)
                        {
                            IMarker mark = channel.CurrentMarks[0];
                            float druation = (float)((mark as RectangleMarkers).EndTime - mark.MarkCreatTime).TotalSeconds;
                            if (mark is RectangleMarkers)
                            {
                                IMarker marker = null;
                                if (all.Count == 1)
                                {
                                    marker = all[0];
                                }
                                else
                                {
                                    IMarker find = null;
                                    if ((find = all.Find(t => t.MarkTyp == IMarker.MarkType.OxygenReduce)) != null)
                                    {
                                        marker = find;
                                    }
                                    else if ((find = all.Find(t => (druation > 17 ? t.MarkTyp == IMarker.MarkType.PeriodicalBodyMove : t.MarkTyp == IMarker.MarkType.LegMove))) != null)
                                    {
                                        marker = find;
                                    }
                                    else if ((find = all.Find(t => t.MarkTyp == IMarker.MarkType.Arousal)) != null)
                                    {
                                        if (SearchConectMarkerType(mark.MarkCreatTime, IMarker.MarkType.RespArousal))
                                        {
                                            marker = all.Find(t => t.MarkTyp == IMarker.MarkType.RespArousal);
                                        }
                                        else if (SearchConectMarkerType(mark.MarkCreatTime, IMarker.MarkType.PlmArousal))
                                        {
                                            marker = all.Find(t => t.MarkTyp == IMarker.MarkType.PlmArousal);
                                        }
                                        else if (SearchConectMarkerType(mark.MarkCreatTime, IMarker.MarkType.LmArousal))
                                        {
                                            marker = all.Find(t => t.MarkTyp == IMarker.MarkType.LmArousal);
                                        }
                                        else
                                        {
                                            marker = find;
                                        }
                                    }
                                    else if ((find = all.Find(t => t.MarkTyp == IMarker.MarkType.Hypopnea)) != null)
                                    {
                                        if (SearchConectMarkerType((mark as RectangleMarkers).EndTime.AddSeconds(5), IMarker.MarkType.Hypopnea))
                                        {
                                            marker = all.Find(t => t.MarkTyp == IMarker.MarkType.Hypopnea);
                                        }
                                        else
                                        {
                                            //                          ThermalMuzzleFlow  PressureMuzzleFlow  ChestBreathing     AbdominalBreathing
                                            //软件自动时间判断功能修改。热敏画线中枢性 ，鼻压力画线阻塞型 ，胸部运动画线低通气 ，腹部运动画线混合型。
                                            //处理配置文件
                                            if (ConfigurationManager.AppSettings["AutoMarkConfig"] == "1")
                                            {

                                                if (channel.ID== "PressureMuzzleFlow")
                                                {
                                                    marker = all.Find(t => t.MarkTyp == IMarker.MarkType.OA);
                                                }
                                                else if (channel.ID == "ThermalMuzzleFlow")
                                                {
                                                    marker = all.Find(t => t.MarkTyp == IMarker.MarkType.CA);
                                                }
                                                else if (channel.ID == "ChestBreathing")
                                                {
                                                    marker = all.Find(t => t.MarkTyp == IMarker.MarkType.Hypopnea);
                                                }
                                                else if (channel.ID == "AbdominalBreathing")
                                                {
                                                    marker = all.Find(t => t.MarkTyp == IMarker.MarkType.MA);
                                                }
                                               
                                            } else
                                            { 
                                                marker = all.Find(t => t.MarkTyp == IMarker.MarkType.OA); 
                                            }
                                        }
                                    }
                                }
                                if (marker != null)
                                {
                                    (mark as pChart.RectangleMarkers).MinLimitValue = (marker as pChart.RectangleMarkers).MinLimitValue;
                                    mark.Name = marker.Name;
                                    mark.Comments = marker.Comments;
                                    mark.Description = marker.Description;
                                    mark.ForeColor = marker.BackColor;
                                    mark.MarkTyp = marker.MarkTyp;
                                    if (marks_CreatingMarksHandler(channel.ID, mark, true))
                                    {
                                        channel.removeMarks = FilitRemoveMarks(channel.removeMarks, mark.MarkTyp);
                                        if (channel.removeMarks.Count > 0)
                                        {
                                            deleteMarks(channel.ID, channel.removeMarks);
                                            ChartArea.RemoveMarks(channel.removeMarks);
                                        }
                                    }
                                    return;
                                }
                            }
                        }
                        ToolStripMenuItem ts2 = new ToolStripMenuItem();
                        ts2.Name = "sEvent";
                        ts2.Text = Program.Language == "EN" ? "Add Event" : "添加事件";
                        ts2.Click += LeftMarkerCreating_Click;
                        ts2.Tag = channel;
                        ViewClientMenu.Items.Add(ts2);

                        ToolStripSeparator sep = new ToolStripSeparator();
                        ViewClientMenu.Items.Add(sep);

                        #region 直接显示事件类别
                        for (int i = 0; i < all.Count; i++)
                        {
                            ToolStripMenuItem markTSButton = new ToolStripMenuItem();
                            markTSButton.Name = all[i].Name;
                            markTSButton.Text = all[i].Name;
                            markTSButton.Click += Marker_Click;
                            markTSButton.Tag = channel;
                            markTSButton.ShowShortcutKeys = true;
                            markTSButton.ShortcutKeyDisplayString = all[i].HotKey;
                            ViewClientMenu.Items.Add(markTSButton);
                        }
                        #endregion
                        ToolStripSeparator sep2 = new ToolStripSeparator();
                        ViewClientMenu.Items.Add(sep2);
                    }

                    ToolStripMenuItem ts = new ToolStripMenuItem();
                    ts.Name = "Zoom";
                    ts.Text = Program.Language == "EN" ? "Zoom" : "放大";
                    ts.Click += ZoomView_Click;
                    ts.Tag = channel;
                    ViewClientMenu.Items.Add(ts);
                    ToolStripMenuItem ts3 = new ToolStripMenuItem();
                    ts3.Name = "measurement";
                    ts3.Text = Program.Language == "EN" ? "Measure" : "测量";
                    ts3.Click += Measure_Click;
                    ts3.Tag = channel;
                    ViewClientMenu.Items.Add(ts3);

                    ViewClientMenu.Show(Cursor.Position);
                }
                else if (buttons == MouseButtons.Right)
                {
                    if (!Channel.Default.ScoreLock)
                    {
                        ToolStripMenuItem ts2 = new ToolStripMenuItem();
                        ts2.Name = "sEvent";
                        ts2.Text = Program.Language == "EN" ? "Add Event" : "添加事件";
                        ts2.Click += RightMarkerCreating_Click;
                        ts2.Tag = channel;
                        ViewClientMenu.Items.Add(ts2);
                        ViewClientMenu.Show(Cursor.Position);
                        List<IMarker> all = MarkerManage.Default.DefineMarkers.FindAll(t => (t is pChart.StringMarkers));
                        if (all.Count > 0)
                        {
                            ToolStripSeparator sep = new ToolStripSeparator();
                            ViewClientMenu.Items.Add(sep);
                            for (int i = 0; i < all.Count; i++)
                            {
                                ToolStripMenuItem markTSButton = new ToolStripMenuItem();
                                markTSButton.Name = all[i].Name;
                                markTSButton.Text = all[i].Name;
                                markTSButton.Click += AllMaker_Click;
                                markTSButton.Tag = channel;
                                ViewClientMenu.Items.Add(markTSButton);
                            }
                            ToolStripSeparator sep2 = new ToolStripSeparator();
                            ViewClientMenu.Items.Add(sep2);
                        }
                    }

                    ToolStripMenuItem ts4 = new ToolStripMenuItem();
                    ts4.Name = "measurement";
                    ts4.Text = Program.Language == "EN" ? "Print" : "打印";
                    ts4.Click += ImgSave_Click;
                    ts4.Tag = channel;
                    ViewClientMenu.Items.Add(ts4);
                    ViewClientMenu.Show(Cursor.Position);
                }
            }));
        }
        /// <summary>
        /// 事件类型被更改
        /// </summary>
        /// <param name="marks"></param>
        /// <param name="NewMarkTyp"></param>
        /// <returns></returns>
        private bool ChartArea_MarkTypeChanged(IMarker[] marks, pChart.IMarker.MarkType NewMarkTyp, string NewMarkName)
        {
            if (Channel.Default.ScoreLock)
            {
                MessageTip.ShowWarning(Program.Language == "EN" ? "The rating has ended. If you need to re rate, please unlock the rating status first" : "评分已结束，如需重新评分，请先解锁评分状态");
                return false;
            }
            if (marks.Length == 0)
                return false;
            if (marks[0].MarkTyp == IMarker.MarkType.None || NewMarkTyp == IMarker.MarkType.None)
            {
                AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "Custom events and system events are not allowed to be converted to each other!" : "自定义事件与系统事件不允许相互转换！");
                return false;
            }
            IMarker find = MarkerManage.Default.DefineMarkers.Find(t => t.MarkTyp == NewMarkTyp && t.Name == NewMarkName);
            if (find != null)
            {
                if (find is RectangleMarkers)
                {
                    Array.Sort((find as RectangleMarkers).OnChannelIndexs);
                    Array.Sort((marks[0] as RectangleMarkers).OnChannelIndexs);
                    if (string.Join("/", (find as RectangleMarkers).OnChannelIndexs) != string.Join("/", (marks[0] as RectangleMarkers).OnChannelIndexs))
                    {
                        this.Invoke(new MethodInvoker(() =>
                        {
                            AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "The changed event is not applicable to the selected channel!" : "将更改的事件不适用所选通道！");
                        }));

                        return false;
                    }
                    if ((find as RectangleMarkers).MinLimitValue > (marks[0] as RectangleMarkers).MinLimitValue)
                    {
                        this.Invoke(new MethodInvoker(() =>
                        {
                            AhDung.MessageTip.ShowWarning(string.Format(Program.Language == "EN" ? "Change failed: Current event duration< {0}s ！" : "更改失败：当前事件持续时间<{0}s！", (find as RectangleMarkers).MinLimitValue));
                        }));

                        return false;
                    }
                }
                RectangleMarkers oneMark = marks[0] as RectangleMarkers;
                IMarker.MarkType oldTyp = oneMark.MarkTyp;
                for (int i = 0; i < marks.Length; i++)
                {
                    IMarker mark = marks[i];
                    mark.MarkTyp = NewMarkTyp;
                    mark.Description = find.Description;
                    mark.ForeColor = find.BackColor;
                    mark.isSelected = false;
                    mark.Name = find.Name;
                    mark.HotKey = find.HotKey;
                }
                ///oneMark已经被更新到最新的事件类型了
                ///加入事件存储中心
                dataManager.dataFactory.EventsInstance.Add(new DataMangement.EventRecordsUit.EventRecord() { StartIndex = oneMark.StartIndex, EndIndex = oneMark.EndIndex, MarkType = (int)NewMarkTyp, Condition = dataManager.ConvertEventCondition(DataMangement.UpdateMode.Update, oldTyp, oneMark.StartIndex, oneMark.EndIndex), Comments = oneMark.Name });
                DataModel.LogInstance.Default.AddLog(string.Format("更改区域标签事件,事件类型为 {0}，事件名称为 {1}，事件开始时间为 {2}，事件结束时间为 {3} ", oneMark.MarkTyp, oneMark.Name, oneMark.StartTime, oneMark.EndTime), pSystem.LogManagement.LogLevel.DEBUG);
                Task.Factory.StartNew(() =>
                {
                    freshEvents();
                });
                MarkEvents(oneMark, new RectangleMarkers()
                {
                    StartTime = oneMark.StartTime,
                    EndTime = oneMark.EndTime,
                    StartFrameNo = oneMark.StartFrameNo,
                    EndFrameNo = oneMark.EndFrameNo,
                    MarkTyp = oldTyp,
                    Name = oneMark.Name
                });
                return true;
            }
            return false;
        }
        /// <summary>
        /// 快捷方式标记事件
        /// </summary>
        /// <param name="ChannelID"></param>
        /// <param name="mark"></param>
        /// <returns></returns>
        private bool ChartArea_SelectRectangleKeyDownHandler(string ChannelID, CurveItem item)
        {
            if (Channel.Default.ScoreLock)
            {
                MessageTip.ShowWarning(Program.Language == "EN" ? "The rating has ended. If you need to re rate, please unlock the rating status first" : "评分已结束，如需重新评分，请先解锁评分状态");
                return false;
            }
            IMarker mark = item.CurrentMarks[0];
            IMarker marker = MarkerManage.Default.DefineMarkers.Find((IMarker t) => t.Name == mark.Name);
            if (marker != null)
            {
                if (marker is RectangleMarkers)
                {
                    if ((marker as pChart.RectangleMarkers).OnChannelIndexs.Contains(Channel.Default.ChannelProperties.Find(t => t.Name == ChannelID).ID))
                    {
                        (mark as pChart.RectangleMarkers).MinLimitValue = (marker as pChart.RectangleMarkers).MinLimitValue;
                    }
                    else
                    {
                        AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "Event type mismatch!" : "事件类型不匹配！");
                        return false;
                    }

                    mark.Name = marker.Name;
                    mark.Comments = marker.Comments;
                    mark.Description = marker.Description;
                    mark.ForeColor = marker.BackColor;
                    if (marks_CreatingMarksHandler(ChannelID, mark, true))
                    {
                        item.removeMarks = FilitRemoveMarks(item.removeMarks, mark.MarkTyp);
                        if (item.removeMarks.Count > 0)
                        {
                            deleteMarks(item.ID, item.removeMarks);
                            ChartArea.RemoveMarks(item.removeMarks);
                        }
                    }
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 事件鼠标右键单击事件触发
        /// </summary>
        /// <param name="channelID"></param>
        /// <param name="mark"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool ChartArea_MarkerMouseRightClick(string channelID, IMarker mark, MouseEventArgs e)
        {
            if (Channel.Default.ScoreLock)
            {
                MessageTip.ShowWarning(Program.Language == "EN" ? "The rating has ended. If you need to re rate, please unlock the rating status first" : "评分已结束，如需重新评分，请先解锁评分状态");
                return false;
            }
            if (mark == null)
                return false;
            if (!mark.Delete)
            {
                tools.AddMarks addMarks = new tools.AddMarks();
                addMarks.isRectangle = true;
                addMarks.IsEdit = true;
                addMarks.CreatingMarksHandler += marks_CreatingMarksHandler;
                addMarks.Dock = DockStyle.Fill;
                addMarks.ChannelID = channelID;
                if (mark is RectangleMarkers)
                {
                    ///标签会被克隆
                    addMarks.perMarker = (IMarker)mark.Clone(); ;
                    addMarks.StartTime = (mark as RectangleMarkers).StartTime;
                    addMarks.EndTime = (mark as RectangleMarkers).EndTime;
                }
                else
                {
                    ///因为是值引用，点位标签需要克隆
                    addMarks.perMarker = (IMarker)mark.Clone();
                    addMarks.isRectangle = false;
                    addMarks.StartTime = mark.MarkCreatTime;
                }
                addMarks.SetMaxMinDate(m_end, m_Start);
                Block.Moudle moudle = new Block.Moudle();
                moudle.Text = Program.Language == "EN" ? "Event Information" : "事件信息";
                moudle.Size = new Size(addMarks.Width, addMarks.Height + moudle.CaptionHeight);
                moudle.Controls.Add(addMarks);
                moudle.StartPosition = FormStartPosition.Manual;
                moudle.Location = e.Location;
                moudle.ShowDialog();
                mark.Delete = addMarks.Delete;
            }
            if (mark.Delete)
            {
                Task.Factory.StartNew(delegate
                {
                    if (mark.MarkTyp == IMarker.MarkType.LightOff || mark.MarkTyp == IMarker.MarkType.LightOn)
                    {
                        this.Invoke(new MethodInvoker(() =>
                        {
                            DateTime lightonTime = ChartArea.pChartMarks.Find(t => t.ID == string.Format("255:{0}", pChart.IMarker.MarkType.LightOn.ToString())) != null ? Channel.Default.AnalysisReult.Sleep.LightOnTime : default(DateTime);
                            DateTime lightoffTime = ChartArea.pChartMarks.Find(t => t.ID == string.Format("255:{0}", pChart.IMarker.MarkType.LightOff.ToString())) != null ? Channel.Default.AnalysisReult.Sleep.LightOffTime : default(DateTime);
                            reportChart1.SetLightONOFF(lightoffTime, lightonTime);

                        }));
                        int sindex = (int)(mark.MarkCreatTime - m_Start).TotalSeconds;
                        dataManager.dataFactory.EventsInstance.Add(new DataMangement.EventRecordsUit.EventRecord()
                        {
                            StartIndex = sindex,
                            EndIndex = sindex,
                            MarkType = (int)mark.MarkTyp,
                            Condition = dataManager.ConvertEventCondition(DataMangement.UpdateMode.Delete)
                        });
                        DataModel.LogInstance.Default.AddLog(string.Format("删除开关灯事件，删除的事件类型{0}，事件时间为{1}", mark.MarkTyp.ToString(), mark.MarkCreatTime), pSystem.LogManagement.LogLevel.DEBUG);
                        string id = mark.ID.Split(':')[1];
                        bak_EventRecordList.RemoveAll((IMarker t) => t.MarkTyp == mark.MarkTyp && t.ID.Split(':')[1] == id);
                        if (mark.MarkTyp == IMarker.MarkType.LightOff)
                            Channel.Default.AnalysisReult.Sleep.LightOffTime = new DateTime(1, 1, 1);
                        else
                            Channel.Default.AnalysisReult.Sleep.LightOnTime = new DateTime(1, 1, 1);
                        freshEvents();
                    }
                    else
                    {
                        if (mark is RectangleMarkers)
                        {
                            (mark as RectangleMarkers).DeleteByHand = true;
                            CheckPLMS(mark as RectangleMarkers, 1000 / ChartArea.FindCurve(channelID).TimeSpan, false);
                        }
                        if (mark is RectangleMarkers && !mark.MarkTyp.ToString().Contains("Arousal"))
                            AutoArousalScore((mark as RectangleMarkers).EndTime, IMarker.MarkType.Arousal, mark.MarkTyp);
                        deleteMarks(channelID, new List<IMarker>() { mark });
                    }
                });
            }
            return true;
        }
        /// <summary>
        /// 标记一个事件
        /// </summary>
        /// <param name="channelID"></param>
        /// <param name="mark"></param>
        /// <param name="reTimeRange"></param>
        private bool marks_CreatingMarksHandler(string channelID, IMarker mark, bool reTimeRange)
        {
            if (channelID.Contains("_Clone"))
            {
                MessageTip.ShowWarning(Program.Language == "EN" ? "Derivative channels cannot be marked" : "派生通道不能做标记", -1, null, null, false);
                return false;
            }
            int channelID2 = EDF.Default.ConvertToChannelNumEx(channelID);
            if (channelID == ChannelID.All.ToString())
            {
                //新标记的ID 点位标签（开关灯）
                string markID = "";
                IMarker.MarkType oldTyp = IMarker.MarkType.None;
                bool issystemevent = true; //将自定义点位标签与开关灯相区分
                bool currentIDChanged = false;///事件类型是否被改变
                bool lighteventFresh = false;
                int oldIndex = -1;
                if (mark.MarkTyp == IMarker.MarkType.LightOn)
                {
                    ///点位标签StringMarkers的ID是由All通道和MarkCreatTime.ToOADate()组成，所以要更换ID
                    markID = string.Format("{1}:{0}", mark.MarkTyp.ToString(), (int)ChannelID.All);
                    ///查找是否存在关灯事件
                    IMarker marker = this.ChartArea.pChartMarks.Find((IMarker t) => t.ID == string.Format("{1}:{0}", IMarker.MarkType.LightOff.ToString(), (int)ChannelID.All));
                    if (marker != null)
                    {
                        if (marker.ID != mark.ID)///开灯是新创建或者由老的开灯变时
                        {
                            if (marker.MarkCreatTime > mark.MarkCreatTime)
                            {
                                MessageTip.ShowWarning(Program.Language == "EN" ? "The light on event cannot be marked before the light off time" : "标记开灯的时间不能早于关灯时间", -1, null, null, false);
                                return false;
                            }
                            else if ((mark.MarkCreatTime - marker.MarkCreatTime).TotalMinutes < 1)
                            {
                                MessageTip.ShowWarning(Program.Language == "EN" ? "The time interval between light on and off cannot be less than 1 minute" : "开关灯时间间隔不能小于1min", -1, null, null, false);
                                return false;
                            }
                        }
                        else if (this.ChartArea.pChartMarks.Find((IMarker t) => t.ID == markID) == null)///如果开灯由关灯变换而来，那么查找之前是否存在开灯事件
                        {
                            ///不存在就把关灯事件的ID先变更掉,但是类型未作变更
                            mark.ID = markID;
                            currentIDChanged = true;
                        }
                        oldIndex = (int)(marker.MarkCreatTime - m_Start).TotalSeconds;
                        oldTyp = marker.MarkTyp;
                    }
                    Channel.Default.AnalysisReult.Sleep.LightOnTime = mark.MarkCreatTime;
                    lighteventFresh = true;
                }
                else if (mark.MarkTyp == IMarker.MarkType.LightOff)
                {
                    markID = string.Format("{1}:{0}", mark.MarkTyp.ToString(), (int)ChannelID.All);
                    IMarker marker2 = this.ChartArea.pChartMarks.Find((IMarker t) => t.ID == string.Format("{1}:{0}", IMarker.MarkType.LightOn.ToString(), (int)ChannelID.All));
                    if (marker2 != null)
                    {
                        if (marker2.ID != mark.ID)
                        {
                            if (marker2.MarkCreatTime < mark.MarkCreatTime)
                            {
                                MessageTip.ShowWarning(Program.Language == "EN" ? "The light on event cannot be marked before the light off time" : "标记关灯的时间不能迟于开灯时间", -1, null, null, false);
                                return false;
                            }
                            else if ((marker2.MarkCreatTime - mark.MarkCreatTime).TotalMinutes < 1)
                            {
                                MessageTip.ShowWarning(Program.Language == "EN" ? "The time interval between light on and off cannot be less than 1 minute" : "开关灯时间间隔不能小于1min", -1, null, null, false);
                                return false;
                            }
                        }
                        else if (this.ChartArea.pChartMarks.Find((IMarker t) => t.ID == markID) == null)
                        {
                            mark.ID = markID;
                            currentIDChanged = true;
                        }
                        oldIndex = (int)(marker2.MarkCreatTime - m_Start).TotalSeconds;
                        oldTyp = marker2.MarkTyp;
                    }
                    Channel.Default.AnalysisReult.Sleep.LightOffTime = mark.MarkCreatTime;
                    lighteventFresh = true;
                }
                else
                {
                    markID = string.Format("{1}:{0}", mark.MarkTyp.ToString(), (int)ChannelID.All);
                    issystemevent = false;
                }
                int sindex = (int)(mark.MarkCreatTime - m_Start).TotalSeconds;
                IMarker find = this.ChartArea.pChartMarks.Find((IMarker t) => t.Name == mark.Name);///查找是否存在同一种类型事件
                if (find == null || issystemevent == false)
                {
                    if (currentIDChanged)
                    {
                        IMarker oldimark = this.bak_EventRecordList.Find((IMarker t) => t.MarkTyp == oldTyp);
                        this.bak_EventRecordList.RemoveAll((IMarker t) => t.MarkTyp == oldTyp);
                        this.ChartArea.pChartMarks.RemoveAll(t => t.MarkTyp == oldTyp);
                        {
                            ///说明是由另外事件直接变更而来，同时存在同类事件，要先删除掉被变更的事件
                            dataManager.dataFactory.EventsInstance.Add(new DataMangement.EventRecordsUit.EventRecord()
                            {
                                StartIndex = oldIndex,
                                EndIndex = oldIndex,
                                MarkType = (int)oldTyp,
                                Condition = dataManager.ConvertEventCondition(DataMangement.UpdateMode.Delete)
                            });
                            DataModel.LogInstance.Default.AddLog(string.Format("删除开关灯事件,事件类型为 {0}，事件时间为{1}", oldTyp.ToString(), oldimark.MarkCreatTime), pSystem.LogManagement.LogLevel.DEBUG);
                        }
                    }
                    mark.ID = markID;
                    this.ChartArea.pChartMarks.Add(mark);
                    this.bak_EventRecordList.Add(mark);
                    dataManager.dataFactory.EventsInstance.Add(new DataMangement.EventRecordsUit.EventRecord() { StartIndex = sindex, EndIndex = sindex, MarkType = (int)mark.MarkTyp, Comments = mark.Name, Condition = ((int)DataMangement.UpdateMode.Insert).ToString() });
                    DataModel.LogInstance.Default.AddLog(string.Format("新增点位标签事件,事件名称为 {0}，事件时间为{1}", mark.Name, mark.MarkCreatTime), pSystem.LogManagement.LogLevel.DEBUG);
                }
                else
                {
                    IMarker marker3 = this.bak_EventRecordList.Find((IMarker t) => t.MarkTyp == find.MarkTyp && t.Name == find.Name);
                    string oldimarkertime = marker3.MarkCreatTime.ToString("G");
                    int delIndex = (int)(find.MarkCreatTime - m_Start).TotalSeconds;///记录老事件变更前的开始索引值
                                                                                    ///把老的同类事件的属性更改成最新的
                    find.Name = mark.Name;
                    find.Description = mark.Description;
                    find.ForeColor = mark.ForeColor;
                    find.MarkTyp = mark.MarkTyp;
                    find.MarkCreatTime = mark.MarkCreatTime;
                    int num2 = sindex / 30;
                    find.StartFrameNo = ((sindex % 30 == 0) ? num2 : (num2 + 1));
                    if (marker3 != null)
                    {
                        marker3 = find;
                    }
                    if (!currentIDChanged)///如果之前存在同类型事件
                    {

                        if (oldTyp != mark.MarkTyp)///判断mark更改前的事件类型是否与更改后的事件类型一致，不一致则需要删除mark
                        {
                            this.bak_EventRecordList.RemoveAll((IMarker t) => t.MarkTyp == oldTyp && t.ID == mark.ID);
                            ///把ID为
                            this.ChartArea.pChartMarks.RemoveAll(t => t.MarkTyp == oldTyp && t.ID == mark.ID);
                            {
                                ///说明是由另外事件直接变更而来，同时存在同类事件，要先删除掉被变更的事件
                                dataManager.dataFactory.EventsInstance.Add(new DataMangement.EventRecordsUit.EventRecord()
                                {
                                    StartIndex = sindex,
                                    EndIndex = sindex,
                                    MarkType = (int)oldTyp,
                                    Condition = dataManager.ConvertEventCondition(DataMangement.UpdateMode.Delete)
                                });
                                DataModel.LogInstance.Default.AddLog(string.Format("删除开关灯事件,事件类型为 {0}，事件时间为{1}", oldTyp.ToString(), oldimarkertime), pSystem.LogManagement.LogLevel.DEBUG);
                            }
                        }
                        dataManager.dataFactory.EventsInstance.Add(new DataMangement.EventRecordsUit.EventRecord()
                        {
                            StartIndex = sindex,
                            EndIndex = sindex,
                            MarkType = (int)mark.MarkTyp,
                            ByHand = true,
                            Condition = dataManager.ConvertEventCondition(DataMangement.UpdateMode.Update, mark.MarkTyp, delIndex, delIndex)
                        });
                        DataModel.LogInstance.Default.AddLog(string.Format("更新开关灯事件,事件类型为 {0}，事件时间为{1}", mark.MarkTyp.ToString(), mark.MarkCreatTime), pSystem.LogManagement.LogLevel.DEBUG);
                    }
                    else
                    {
                        dataManager.dataFactory.EventsInstance.Add(new DataMangement.EventRecordsUit.EventRecord()
                        {
                            StartIndex = sindex,
                            EndIndex = sindex,
                            MarkType = (int)mark.MarkTyp,
                            ByHand = true,
                            Condition = dataManager.ConvertEventCondition(DataMangement.UpdateMode.Update, oldTyp, sindex, sindex)
                        });
                        DataModel.LogInstance.Default.AddLog(string.Format("更新开关灯事件,事件类型为 {0}，事件时间为{1}", mark.MarkTyp.ToString(), mark.MarkCreatTime), pSystem.LogManagement.LogLevel.DEBUG);
                    }
                }
                if (lighteventFresh)
                {
                    DateTime end = (this.ChartArea.pChartMarks.Find((IMarker t) => t.ID == string.Format("255:{0}", IMarker.MarkType.LightOn.ToString())) != null) ? Channel.Default.AnalysisReult.Sleep.LightOnTime : default(DateTime);
                    DateTime start = (this.ChartArea.pChartMarks.Find((IMarker t) => t.ID == string.Format("255:{0}", IMarker.MarkType.LightOff.ToString())) != null) ? Channel.Default.AnalysisReult.Sleep.LightOffTime : default(DateTime);
                    this.reportChart1.SetLightONOFF(start, end);
                }
            }
            else
            {
                RectangleMarkers rectangleMarkers = mark.Clone() as RectangleMarkers;
                bool isadd = false;
                bool isedit = false;
                float limitRecTime = rectangleMarkers.MinLimitValue;
                IMarker.MarkType _newMarkTyp = IMarker.MarkType.None;
                if (mark.MarkTyp == pChart.IMarker.MarkType.Hypopnea || mark.MarkTyp == pChart.IMarker.MarkType.CA || mark.MarkTyp == pChart.IMarker.MarkType.OA || mark.MarkTyp == pChart.IMarker.MarkType.MA)
                {
                    _newMarkTyp = IMarker.MarkType.RespArousal;
                    if (rectangleMarkers.MinLimitValue == 0)
                        limitRecTime = 5;
                }
                else if (mark.MarkTyp == pChart.IMarker.MarkType.OxygenReduce)
                {
                    if (rectangleMarkers.MinLimitValue == 0)
                        limitRecTime = 2;
                }
                else if (mark.MarkTyp == pChart.IMarker.MarkType.LegMove)
                {
                    _newMarkTyp = IMarker.MarkType.LmArousal;
                    if (rectangleMarkers.MinLimitValue == 0)
                        limitRecTime = 0.5f;
                }
                else if (mark.MarkTyp == pChart.IMarker.MarkType.PeriodicalBodyMove)
                {
                    _newMarkTyp = IMarker.MarkType.PlmArousal;
                    if (rectangleMarkers.MinLimitValue == 0)
                        limitRecTime = 17f;
                }
                else
                {
                    if (rectangleMarkers.MinLimitValue == 0)
                        limitRecTime = 1;
                }
                if ((rectangleMarkers.EndTime - rectangleMarkers.StartTime).TotalSeconds < limitRecTime)
                {
                    this.ChartArea.ChartAreaInvalidate(false, 0);
                    AhDung.MessageTip.ShowWarning(string.Format("事件标记时间不能少于{0}s", limitRecTime));
                    return false;
                }
                IMarker oldMarker = null;
                bool eventsIsExist = false;
                int StartTimeSpan = (int)(rectangleMarkers.StartTime - this.m_Start).TotalMilliseconds;
                int EndTimeSpan = (int)(rectangleMarkers.EndTime - this.m_Start).TotalMilliseconds;
                string arg = rectangleMarkers.ID.Split(new char[]
                {
                    ':'
                })[1];
                bool isfirst = true;
                for (int i = 0; i < rectangleMarkers.OnChannelIndexs.Length; i++)
                {
                    CurveItem curveItem = this.ChartArea.FindCurve(rectangleMarkers.OnChannelIndexs[i]);
                    if (curveItem != null)
                    {
                        string text = string.Format("{0}:{1}", curveItem.ChannelNum, arg);
                        string eventid = text;
                        RectangleMarkers mm = curveItem.CurrentMarks.Find((IMarker t) => t.ID == eventid) as RectangleMarkers;
                        if (reTimeRange)
                        {
                            rectangleMarkers.StartIndex = StartTimeSpan / curveItem.TimeSpan;
                            rectangleMarkers.EndIndex = EndTimeSpan / curveItem.TimeSpan;
                            rectangleMarkers.StartIndex = ((StartTimeSpan % curveItem.TimeSpan == 0) ? rectangleMarkers.StartIndex : (rectangleMarkers.StartIndex + 1));
                            rectangleMarkers.EndIndex = ((EndTimeSpan % curveItem.TimeSpan == 0) ? rectangleMarkers.EndIndex : (rectangleMarkers.EndIndex + 1));
                            int startframeXH = StartTimeSpan / 30000;
                            startframeXH = ((StartTimeSpan % 30000 == 0) ? startframeXH : (startframeXH + 1));
                            rectangleMarkers.StartFrameNo = startframeXH;
                            int endframeXH = EndTimeSpan / 30000;
                            endframeXH = ((EndTimeSpan % 30000 == 0) ? endframeXH : (endframeXH + 1));
                            rectangleMarkers.EndFrameNo = endframeXH;
                            eventid = string.Format("{0}:{1}-{2}", curveItem.ChannelNum, rectangleMarkers.StartIndex, rectangleMarkers.EndIndex);
                        }
                        if (mm == null)
                        {
                            if (!isadd)
                            {
                                isadd = true;
                            }
                            IMarker marker4 = (IMarker)rectangleMarkers.Clone();
                            marker4.ID = eventid;
                            lock (this.m_objLock)
                            {
                                curveItem.CurrentMarks.Add(marker4);
                            }
                            if (!eventsIsExist)
                            {
                                this.bak_EventRecordList.Add(marker4);
                                eventsIsExist = true;
                            }
                        }
                        else
                        {
                            if (!isedit)
                            {
                                isedit = true;
                                oldMarker = new RectangleMarkers() { StartTime = mm.StartTime, EndTime = mm.EndTime, MarkTyp = mm.MarkTyp, StartFrameNo = mm.StartFrameNo, EndFrameNo = mm.EndFrameNo, Name = mm.Name };
                            }
                            mm.EndFrameNo = rectangleMarkers.EndFrameNo;
                            mm.StartFrameNo = rectangleMarkers.StartFrameNo;
                            mm.ID = eventid;
                            mm.StartTime = rectangleMarkers.StartTime;
                            mm.EndTime = rectangleMarkers.EndTime;
                            mm.StartIndex = rectangleMarkers.StartIndex;
                            mm.EndIndex = rectangleMarkers.EndIndex;
                            if (mm.MarkTyp != mark.MarkTyp)
                            {
                                string id = mark.ID.Split(new char[] { ':' })[1];
                                mm.Name = rectangleMarkers.Name;
                                mm.ForeColor = rectangleMarkers.ForeColor;
                                mm.Description = rectangleMarkers.Description;
                                mm.Comments = rectangleMarkers.Comments;
                                if (isfirst)
                                {
                                    dataManager.dataFactory.EventsInstance.Add(new DataMangement.EventRecordsUit.EventRecord()
                                    {
                                        StartIndex = mm.StartIndex,
                                        EndIndex = mm.EndIndex,
                                        MarkType = (int)mm.MarkTyp,
                                        Condition = dataManager.ConvertEventCondition(DataMangement.UpdateMode.Delete)
                                    });
                                    dataManager.dataFactory.EventsInstance.Add(new DataMangement.EventRecordsUit.EventRecord()
                                    {
                                        StartIndex = (mark as RectangleMarkers).StartIndex,
                                        EndIndex = (mark as RectangleMarkers).EndIndex,
                                        MarkType = (int)mark.MarkTyp,
                                        ByHand = true,
                                        Condition = ((int)DataMangement.UpdateMode.Insert).ToString(),
                                        Comments = mark.Name
                                    });
                                    DataModel.LogInstance.Default.AddLog(string.Format("更新区域标签事件,旧事件类型为 {0}，新事件类型为 {3},新事件开始时间为 {1}，新事件结束时间为 {2} ", mm.MarkTyp.ToString(), (mark as RectangleMarkers).StartTime, (mark as RectangleMarkers).EndTime, mark.MarkTyp.ToString()), pSystem.LogManagement.LogLevel.DEBUG);
                                }
                                mm.MarkTyp = rectangleMarkers.MarkTyp;
                                isfirst = false;
                            }
                        }
                    }
                }
                changeMarkRange(rectangleMarkers, null, false);
                if (isadd)
                {
                    dataManager.dataFactory.EventsInstance.Add(new DataMangement.EventRecordsUit.EventRecord() { StartIndex = rectangleMarkers.StartIndex, EndIndex = rectangleMarkers.EndIndex, ByHand = true, MarkType = (int)rectangleMarkers.MarkTyp, Condition = ((int)DataMangement.UpdateMode.Insert).ToString(), Comments = rectangleMarkers.Name });
                    DataModel.LogInstance.Default.AddLog(string.Format("新增区域标签事件,事件所在通道为 {0}，事件名称为 {1}，事件开始时间为 {2}，事件结束时间为 {3} ", channelID, rectangleMarkers.Name, rectangleMarkers.StartTime, rectangleMarkers.EndTime), pSystem.LogManagement.LogLevel.DEBUG);
                    this.MarkEvents(mark, null, true);
                }
                /*
                //else if (isedit)
                //{
                changeMarkRange(rectangleMarkers, null, false);
                //dataManager.dataFactory.EventsInstance.Add(new DataMangement.EventRecordsUit.EventRecord() { StartIndex = rectangleMarkers.StartIndex, EndIndex = rectangleMarkers.EndIndex, ByHand = true, MarkType = (int)mark.MarkTyp, Condition = string.Format("{0}|{1}", (int)DataMangement.UpdateMode.Update, rectangleMarkers.ID.Split(':')[1]) });
                //this.MarkEvents(mark, oldMarker);
                //}
                */
            }
            this.ChartArea.ChartAreaInvalidate(false, 0);
            this.freshEvents();
            return true;
        }
        /// <summary>
        /// 事件的款选范围发生改变时触发
        /// </summary>
        /// <param name="bechangedmark"></param>
        private void changeMarkRange(IMarker bechangedmark, List<IMarker> removeMarks, bool needFresh = true)
        {
            if (bechangedmark is RectangleMarkers)
            {
                RectangleMarkers mark = bechangedmark as RectangleMarkers;///mark中的ID为变化前的ID
                if (mark.OnChannelIndexs.Length < 1)
                    return;
                CurveItem channel = ChartArea.FindCurve(mark.OnChannelIndexs[0]);
                CheckPLMS(mark, 1000 / channel.TimeSpan);
                if (removeMarks == null)
                {
                    removeMarks = new List<IMarker>(channel.CurrentMarks.FindAll(delegate (IMarker t)
                     {
                         RectangleMarkers rec = t as RectangleMarkers;
                         if (rec.ReadOnly)
                             return false;
                         else if (mark.StartIndex == rec.StartIndex && mark.EndIndex == rec.EndIndex && mark.MarkTyp == rec.MarkTyp)
                         {
                             return false;
                         }
                         return mark.StartIndex <= rec.EndIndex && mark.EndIndex >= rec.StartIndex;
                     }));
                }
                removeMarks = FilitRemoveMarks(removeMarks, mark.MarkTyp);
                if (removeMarks.Count > 0)
                {
                    deleteMarks(channel.ID, removeMarks, false);//避免重复性更新
                    ChartArea.RemoveMarks(removeMarks);
                }
                else
                {
                    if (needFresh)
                        ChartArea.ChartAreaInvalidate();
                }
                Dictionary<ITable, ITable> m_updateList = new Dictionary<ITable, ITable>();
                string matchNewMarkID = "", matchOldMarkID = "";
                string eventID = string.Format("{0}:{1}-{2}", mark.ID.Split(':')[0], mark.StartIndex, mark.EndIndex);
                if (eventID != mark.ID)
                {
                    if (matchNewMarkID == "")
                    {
                        matchNewMarkID = string.Format(":{0}", eventID.Split(':')[1]);
                        matchOldMarkID = string.Format(":{0}", mark.ID.Split(':')[1]);
                    }
                }
                if (matchOldMarkID != "")
                {
                    int[] indexs = matchOldMarkID.Split(new char[] { ':', '-' }, StringSplitOptions.RemoveEmptyEntries).Select(t => int.Parse(t)).ToArray();
                    if (indexs.Length < 1)
                    {
                        if (needFresh)
                            this.freshEvents();
                        return;
                    }
                    ///加入事件存储中心
                    dataManager.dataFactory.EventsInstance.Add(new DataMangement.EventRecordsUit.EventRecord()
                    {
                        StartIndex = mark.StartIndex,
                        EndIndex = mark.EndIndex,
                        MarkType = (int)mark.MarkTyp,
                        ByHand = true,
                        Condition = dataManager.ConvertEventCondition(DataMangement.UpdateMode.Update, mark.MarkTyp, matchOldMarkID),
                        Comments = mark.Name
                    });
                    DataModel.LogInstance.Default.AddLog(string.Format("更新区域标签事件,事件类型为 {0}，事件开始时间为 {1}，事件结束时间为 {2} ", mark.MarkTyp.ToString(), mark.StartTime, mark.EndTime), pSystem.LogManagement.LogLevel.DEBUG);
                    int StartTimeSpan = indexs[0] * channel.TimeSpan;
                    int startframeXH = StartTimeSpan / 30000;
                    startframeXH = ((StartTimeSpan % 30000 == 0) ? startframeXH : (startframeXH + 1));
                    int endTimeSpan = indexs[1] * channel.TimeSpan;
                    int endframeXH = endTimeSpan / 30000;
                    endframeXH = ((endTimeSpan % 30000 == 0) ? endframeXH : (endframeXH + 1));
                    RectangleMarkers _oldRectMarker = new RectangleMarkers()
                    {
                        StartTime = m_Start.AddMilliseconds(StartTimeSpan),
                        EndTime = m_Start.AddMilliseconds(indexs[1] * channel.TimeSpan),
                        MarkTyp = mark.MarkTyp,
                        StartFrameNo = startframeXH,
                        EndFrameNo = endframeXH
                    };
                    if (mark.MarkTyp == IMarker.MarkType.None)
                    {

                    }
                    else if (IsArousalMark(mark.MarkTyp))
                    {
                        mark.ID = eventID;///id要更新过来

                        IMarker newmark = AutoConvertArousalMark(mark);
                        if (newmark.MarkTyp != mark.MarkTyp)
                        {
                            UpdateArousalScore(mark, newmark.MarkTyp);
                        }
                    }
                    else
                    {
                        IMarker _arousal = SearchArousalMarker(_oldRectMarker.EndTime, _oldRectMarker.MarkTyp);
                        ///_arousal为空则说明_oldRectMarker.MarkTyp事件老的范围未自动关联微觉醒事件
                        if (_arousal == null)
                        {
                            AutoArousalScore(mark.EndTime, getArousalMarkType(mark.MarkTyp));
                        }
                        else
                        {
                            IMarker new_arousal = SearchArousalMarker(mark.EndTime, mark.MarkTyp);
                            ///_arousal为空则说明mark.MarkTyp事件新的范围未自动关联微觉醒事件，需要恢复成自发性微觉醒，否则不需要恢复
                            if (new_arousal == null)
                            {
                                new_arousal = SearchArousalMarker(mark.EndTime, IMarker.MarkType.Arousal);
                                if (new_arousal == null)
                                {
                                    UpdateArousalScore(_arousal, IMarker.MarkType.Arousal);
                                }
                                else if (new_arousal.ID != _arousal.ID)//如果新关联的微觉醒事件不与原来关联的相同，则需要恢复老的，变更新的关联
                                {
                                    UpdateArousalScore(_arousal, IMarker.MarkType.Arousal);
                                    UpdateArousalScore(new_arousal, getArousalMarkType(mark.MarkTyp));
                                }
                            }
                        }
                    }
                    MarkEvents(mark, _oldRectMarker);
                }
                else
                {
                    //只有对可能影响自定义微觉醒的事件标记才调用
                    if (getArousalMarkType(mark.MarkTyp) != IMarker.MarkType.None)
                        AutoArousalScore(mark.EndTime, getArousalMarkType(mark.MarkTyp));
                }

            }
        }
        /// <summary>
        /// 手动标记事件
        /// </summary>
        /// <param name="typ">事件类型</param>
        private void MarkEvents(IMarker mark, IMarker oldmark, bool add = true)
        {
            SuperPointF[] data = null;
            if (mark is RectangleMarkers)
            {
                bool bakadd = add;
                add = true;
                IMarker[] markers = new IMarker[] { oldmark, mark };
                for (int s = 0; s < markers.Length; s++)
                {
                    if (markers[s] == null)
                    {
                        add = !bakadd;//先反置位，s=1时才能是原值
                        continue;
                    }
                    else
                    {
                        add = !add;///如果s=1时要先减
                    }
                    RectangleMarkers rectMark = markers[s] as RectangleMarkers;


                    //如果是腿动，需要计算周期性循环腿动，则需要遍历所有周期性腿动相关联的帧
                    if (rectMark.MarkTyp == IMarker.MarkType.LegMove)
                    {
                        //先全部置零再把周期性腿动相关的帧全部重新赋值
                        Array.ForEach(ResultDomain.Default.PLMsPoints, t => t.YMaxValue = 0);
                        List<IMarker> PLMsList = bak_EventRecordList.FindAll(t => t.MarkTyp == IMarker.MarkType.PeriodicalBodyMove);
                        data = ResultDomain.Default.PLMsPoints;
                        for (int index = 0; index < PLMsList.Count; index++)
                        {
                            RectangleMarkers PLMmark = PLMsList[index] as RectangleMarkers;
                            //只需要算头和尾的偏移量，中间全部是30
                            for (int FrameIndex = PLMmark.StartFrameNo - 1; FrameIndex < PLMmark.EndFrameNo; FrameIndex++)
                            {
                                float druationTimeInFrame = 0;
                                //如果一帧内发生
                                if (PLMmark.StartFrameNo == PLMmark.EndFrameNo)
                                {
                                    druationTimeInFrame = (float)Math.Round((PLMmark.EndTime - PLMmark.StartTime).TotalSeconds, 2);
                                }
                                else if (FrameIndex == PLMmark.StartFrameNo - 1)
                                {
                                    druationTimeInFrame = (float)Math.Round(30 - ((PLMmark.StartTime - m_Start).TotalSeconds - FrameIndex * 30), 2);
                                }
                                else if (FrameIndex == PLMmark.EndFrameNo - 1)
                                {
                                    druationTimeInFrame = (float)Math.Round((PLMmark.EndTime - m_Start).TotalSeconds - FrameIndex * 30, 2);
                                }
                                else
                                {
                                    druationTimeInFrame = 30;
                                }
                                data[FrameIndex].YMaxValue = druationTimeInFrame;
                                Channel.Default.AnalysisReult.Epochs[FrameIndex].PLMs = druationTimeInFrame;
                                dataManager.dataFactory.EpochsInstance.Add(new DataMangement.EpochsUnit.EpochInfo() { Index = FrameIndex, ByHand = true, Epoch = Channel.Default.AnalysisReult.Epochs[FrameIndex] });
                                Channel.Default.CanRecover = true;
                            }
                        }
                        //每次周期性循环腿动的数据全部更新
                        ResultDomain.Default.PLMsPoints = data;
                    }

                    //其余事件按照开始帧和结束帧就可以确定帧的内容 
                    int startTimeSpan = (int)(rectMark.StartTime - this.m_Start).TotalSeconds;
                    int endTimeSpan = (int)(rectMark.EndTime - this.m_Start).TotalSeconds;
                    int currentFrameTimeSpan = rectMark.StartFrameNo * 30 - startTimeSpan;
                    for (int i = rectMark.StartFrameNo - 1; i < rectMark.EndFrameNo; i++)
                    {
                        switch (rectMark.MarkTyp)
                        {
                            case IMarker.MarkType.CA:
                                data = ResultDomain.Default.CAPoints;
                                break;
                            case IMarker.MarkType.OA:
                                data = ResultDomain.Default.OAPoints;
                                break;
                            case IMarker.MarkType.MA:
                                data = ResultDomain.Default.MAPoints;
                                break;
                            case IMarker.MarkType.Hypopnea:
                                data = ResultDomain.Default.HypopneaPoints;
                                break;
                            case IMarker.MarkType.MT:
                                data = ResultDomain.Default.MTPoints;
                                break;
                            case IMarker.MarkType.Arousal:
                            case IMarker.MarkType.LmArousal:
                            case IMarker.MarkType.PlmArousal:
                            case IMarker.MarkType.RespArousal:
                                data = ResultDomain.Default.MicArousalPoints;
                                break;
                            case IMarker.MarkType.PeriodicalBodyMove:
                                data = ResultDomain.Default.PLMsPoints;
                                break;
                            case IMarker.MarkType.LegMove:
                                data = ResultDomain.Default.PLMPoints;
                                break;
                            default:
                                return;
                        }
                        int lastTimespan = endTimeSpan - (i + 1) * 30;
                        float druationInEpoch;
                        if (lastTimespan <= 0)
                        {
                            druationInEpoch = (float)(currentFrameTimeSpan + lastTimespan);
                        }
                        else
                        {
                            druationInEpoch = (float)currentFrameTimeSpan;
                            currentFrameTimeSpan = 30;
                        }
                        Doc_Epochs doc_Epochs = new Doc_Epochs
                        {
                            EpochIndex = i
                        };
                        if (i < data.Length && i >= 0)
                        {
                            bool isinsert = false;
                            if (data[i] == null)
                            {
                                data[i] = new SuperPointF
                                {
                                    YMaxValue = 0f
                                };
                            }
                            if (add)
                            {
                                data[i].YMaxValue += druationInEpoch;
                                if (data[i].YMaxValue > 30f)
                                {
                                    data[i].YMaxValue = 30f;
                                }
                            }
                            else
                            {
                                data[i].YMaxValue -= druationInEpoch;
                                if (data[i].YMaxValue < 0f)
                                {
                                    data[i].YMaxValue = 0f;
                                }
                            }
                            data[i].YMinValue = 0f;
                            data[i].XValue = (float)i;
                            druationInEpoch = data[i].YMaxValue;
                            if (!Channel.Default.AnalysisReult.Epochs[i].EpochExist)
                            {
                                Channel.Default.AnalysisReult.Epochs[i].EpochExist = true;
                                isinsert = true;
                            }
                            switch (rectMark.MarkTyp)
                            {
                                case IMarker.MarkType.CA:
                                    Channel.Default.AnalysisReult.Epochs[i].CA = (doc_Epochs.CA = druationInEpoch);
                                    break;
                                case IMarker.MarkType.OA:
                                    Channel.Default.AnalysisReult.Epochs[i].OA = (doc_Epochs.OA = druationInEpoch);
                                    break;
                                case IMarker.MarkType.MA:
                                    Channel.Default.AnalysisReult.Epochs[i].MA = (doc_Epochs.MA = druationInEpoch);
                                    break;
                                case IMarker.MarkType.Hypopnea:
                                    Channel.Default.AnalysisReult.Epochs[i].Hypopnea = (doc_Epochs.Hypopnea = druationInEpoch);
                                    break;
                                case IMarker.MarkType.MT:
                                    Channel.Default.AnalysisReult.Epochs[i].MT = (doc_Epochs.MT = druationInEpoch);
                                    break;
                                case IMarker.MarkType.LegMove:
                                    Channel.Default.AnalysisReult.Epochs[i].PLM = (doc_Epochs.PLM = druationInEpoch);
                                    break;
                                case IMarker.MarkType.PeriodicalBodyMove:
                                    Channel.Default.AnalysisReult.Epochs[i].PLMs = (doc_Epochs.PLMs = druationInEpoch);
                                    break;
                                case IMarker.MarkType.Arousal:
                                case IMarker.MarkType.LmArousal:
                                case IMarker.MarkType.PlmArousal:
                                case IMarker.MarkType.RespArousal:
                                    Channel.Default.AnalysisReult.Epochs[i].MicArousal = (doc_Epochs.MicArousal = druationInEpoch);
                                    break;
                                default:
                                    return;
                            }
                            if (isinsert)
                            {
                                dataManager.dataFactory.EpochsInstance.Add(new DataMangement.EpochsUnit.EpochInfo() { Index = i, ByHand = true, Epoch = doc_Epochs });
                            }
                            else
                            {
                                dataManager.dataFactory.EpochsInstance.Add(new DataMangement.EpochsUnit.EpochInfo() { Index = i, ByHand = true, Epoch = Channel.Default.AnalysisReult.Epochs[i] });
                            }
                            Channel.Default.CanRecover = true;
                        }
                    }
                    base.Invoke(new MethodInvoker(delegate ()
                    {
                        if (this.bak_butt.Tag != null)
                        {
                            string a = this.bak_butt.Tag.ToString();
                            int markTyp = (int)rectMark.MarkTyp;
                            //觉醒包含多个种类
                            if (a == ((int)IMarker.MarkType.Arousal).ToString())
                            {
                                if (rectMark.MarkTyp == IMarker.MarkType.Arousal || rectMark.MarkTyp == IMarker.MarkType.LmArousal || rectMark.MarkTyp == IMarker.MarkType.PlmArousal || rectMark.MarkTyp == IMarker.MarkType.RespArousal)
                                {
                                    this.reportChart1.Invalidate(ResultDomain.Default.MicArousalPoints);
                                }
                            }
                            //腿动会影响周期性循环腿动的值
                            else if (a == ((int)RectangleMarkers.MarkType.PeriodicalBodyMove).ToString() && rectMark.MarkTyp == IMarker.MarkType.LegMove)
                            {
                                this.reportChart1.Invalidate(ResultDomain.Default.PLMsPoints);
                            }
                            else if (a == markTyp.ToString())
                            {
                                this.reportChart1.Invalidate(data);
                            }
                        }
                    }));
                }
                Channel.Default.AnalysisReult.HasDataChange = true;
            }
        }
        /// <summary>
        /// 删除索引
        /// </summary>
        /// <param name="channelID"></param>
        /// <param name="marks"></param>
        private void deleteMarks(string channelID, List<IMarker> marks, bool refrshEventList = true, bool realdeleteevent = true)
        {
            for (int i = 0; i < marks.Count; i++)
            {
                RectangleMarkers mark = marks[i] as RectangleMarkers;
                StringMarkers mark2 = marks[i] as StringMarkers;
                if (mark != null)
                {
                    dataManager.dataFactory.EventsInstance.Add(new DataMangement.EventRecordsUit.EventRecord()
                    {
                        StartIndex = mark.StartIndex,
                        EndIndex = mark.EndIndex,
                        MarkType = (int)mark.MarkTyp,
                        ByHand = true,
                        DeleteByHand = (int)mark.MarkTyp == 99 ? false : mark.DeleteByHand,
                        Condition = dataManager.ConvertEventCondition(DataMangement.UpdateMode.Delete)
                    });
                    DataModel.LogInstance.Default.AddLog(string.Format("删除区域标签事件，事件类型为{0}，事件开始时间{1}，事件结束时间{2}", mark.MarkTyp.ToString(), mark.StartTime, mark.EndTime), pSystem.LogManagement.LogLevel.DEBUG);
                    MarkEvents(mark, null, false);
                    string id = mark.ID.Split(':')[1];
                    bak_EventRecordList.RemoveAll((IMarker t) => t.MarkTyp == mark.MarkTyp && t.ID.Split(':')[1] == id);
                }
                if (mark2 != null)
                {
                    int sindex = (int)(mark2.MarkCreatTime - m_Start).TotalSeconds;
                    dataManager.dataFactory.EventsInstance.Add(new DataMangement.EventRecordsUit.EventRecord()
                    {
                        StartIndex = sindex,
                        EndIndex = sindex,
                        MarkType = (int)mark2.MarkTyp,
                        Condition = dataManager.ConvertEventCondition(DataMangement.UpdateMode.Delete)
                    });
                    DataModel.LogInstance.Default.AddLog(string.Format("删除点位标签事件，删除的事件名称为{0}，事件时间为{1}", mark2.Name, mark2.MarkCreatTime), pSystem.LogManagement.LogLevel.DEBUG);
                    bak_EventRecordList.Remove(mark2);
                }
            }
            if (marks.Count > 0)
            {
                freshreportChart();
                if (refrshEventList)
                    freshEvents();
            }
        }
        /// <summary>
        /// 过滤掉不符合吞并规则的待移除事件
        /// </summary>
        /// <param name="marks"></param>
        /// <param name="markTyp"></param>
        /// <returns></returns>
        private List<IMarker> FilitRemoveMarks(List<IMarker> marks, IMarker.MarkType markTyp)
        {
            List<IMarker> newmarks = marks;
            for (int i = 0; i < marks.Count; i++)
            {
                if (markTyp == IMarker.MarkType.None && marks[i].MarkTyp != IMarker.MarkType.None)
                {
                    newmarks.Remove(marks[i]);
                    i--;
                    continue;
                }
                if (markTyp != IMarker.MarkType.None && marks[i].MarkTyp == IMarker.MarkType.None)
                {
                    newmarks.Remove(marks[i]);
                    i--;
                    continue;
                }
            }
            IMarker.MarkType exceputTyp = IMarker.MarkType.None;
            if (markTyp == IMarker.MarkType.LegMove)
                exceputTyp = IMarker.MarkType.PeriodicalBodyMove;
            else if (markTyp == IMarker.MarkType.PeriodicalBodyMove)
                exceputTyp = IMarker.MarkType.LegMove;
            else
            {
                return newmarks;
            }
            newmarks.RemoveAll(t => t.MarkTyp == exceputTyp);
            return newmarks;
        }
        /// <summary>
        /// 自动检测腿动群是否可以组成一个PLM
        /// </summary>
        /// <param name="mark"></param>
        /// <param name="timespan"></param>
        private void CheckPLMS(RectangleMarkers mark, int timespan, bool add = true)
        {
            if (mark.MarkTyp == pChart.IMarker.MarkType.LegMove)
            {
                List<IMarker> find = bak_EventRecordList.FindAll(delegate (IMarker t)
                {
                    return t.MarkTyp == pChart.IMarker.MarkType.LegMove;
                });
                if (find.Count > 1)
                {
                    List<Point> list = find.Select(delegate (IMarker t)
                    {
                        RectangleMarkers rect = t as RectangleMarkers;
                        return new Point(rect.StartIndex, rect.EndIndex);
                    }).ToList();
                    Point addone = new Point(mark.StartIndex, mark.EndIndex);
                    if (add)
                    {
                        if (list.Find(t => t == addone) == null)
                            list.Add(addone);
                    }
                    else
                    {
                        list.Remove(addone);
                    }
                    list.Sort((t1, t2) => t1.X.CompareTo(t2.X));
                    int cnt = 0;
                    int startidx = 0, endidx = 0;
                    List<Point> plms = new List<Point>();
                    for (int i = 1; i < list.Count; i++)
                    {
                        int offsetx = list[i].X - list[i - 1].Y;
                        if (offsetx / timespan < 90)
                        {
                            if (cnt == 0)
                            {
                                startidx = list[i - 1].X;
                            }
                            endidx = list[i].Y;
                            cnt++;
                        }
                        else
                        {
                            if (cnt >= 3)
                            {
                                endidx = list[i - 1].Y;
                                plms.Add(new Point(startidx, endidx));
                            }
                            cnt = 0;
                        }
                    }
                    if (cnt >= 3)
                    {
                        plms.Add(new Point(startidx, endidx));
                    }
                    bak_EventRecordList.RemoveAll(t => t.MarkTyp == pChart.IMarker.MarkType.PeriodicalBodyMove);
                    RectangleMarkers modle = DataModel.MarkerManage.Default.DefineMarkers.Find(t => t.MarkTyp == pChart.IMarker.MarkType.PeriodicalBodyMove) as RectangleMarkers;
                    CurveItem[] allcurve = new CurveItem[modle.OnChannelIndexs.Length];
                    for (int i = 0; i < modle.OnChannelIndexs.Length; i++)
                    {
                        CurveItem item = ChartArea.FindCurve(modle.OnChannelIndexs[i]);
                        if (item != null)
                        {
                            item.CurrentMarks.RemoveAll(t => t.MarkTyp == pChart.IMarker.MarkType.PeriodicalBodyMove);
                        }
                        allcurve[i] = item;
                    }
                    ///删除所有的PLMs
                    dataManager.dataFactory.EventsInstance.Delete((int)pChart.IMarker.MarkType.PeriodicalBodyMove);
                    List<DataMangement.EventRecordsUit.EventRecord> records = new List<DataMangement.EventRecordsUit.EventRecord>();
                    for (int i = 0; i < plms.Count; i++)
                    {
                        int startOff = 1000 / timespan * plms[i].X;
                        int endOff = 1000 / timespan * plms[i].Y;
                        DateTime markStartTime = this.m_Start.AddMilliseconds((double)startOff);
                        DateTime markEndTime = this.m_Start.AddMilliseconds((double)endOff);
                        int framestartNo = startOff / 30000;
                        framestartNo = ((startOff % 30000 == 0) ? framestartNo : (framestartNo + 1));
                        int frameendNo = endOff / 30000;
                        frameendNo = ((endOff % 30000 == 0) ? frameendNo : (frameendNo + 1));
                        for (int s = 0; s < modle.OnChannelIndexs.Length; s++)
                        {
                            IMarker rect = new RectangleMarkers()
                            {
                                ID = string.Format("{0}:{1}-{2}", modle.OnChannelIndexs[s], plms[i].X, plms[i].Y),
                                Name = modle.Name,
                                Description = modle.Description,
                                StartTime = markStartTime,
                                EndTime = markEndTime,
                                StartIndex = plms[i].X,
                                EndIndex = plms[i].Y,
                                StartFrameNo = framestartNo,
                                EndFrameNo = frameendNo,
                                MarkCreatTime = markStartTime,
                                MarkTyp = pChart.IMarker.MarkType.PeriodicalBodyMove,
                                Comments = modle.Comments,
                                ForeColor = modle.BackColor,
                                MinLimitValue = modle.MinLimitValue,
                            };
                            records.Add(new DataMangement.EventRecordsUit.EventRecord()
                            {
                                StartIndex = plms[i].X,
                                EndIndex = plms[i].Y,
                                ByHand = true,
                                MarkType = (int)pChart.IMarker.MarkType.PeriodicalBodyMove,
                                Condition = ((int)DataMangement.UpdateMode.Insert).ToString()
                            });
                            allcurve[s].CurrentMarks.Add(rect);
                            if (s == 0)
                                bak_EventRecordList.Add(rect);
                        }
                    }
                    dataManager.dataFactory.EventsInstance.AddRange(records);
                }
            }
        }
        /// <summary>
        /// 自动判别微觉醒的类型
        /// </summary>
        /// <param name="endTime"></param>
        /// <param name="newMarkTyp"></param>
        private void AutoArousalScore(DateTime endTime, IMarker.MarkType newMarkTyp)
        {
            AutoArousalScore(endTime, newMarkTyp, IMarker.MarkType.Arousal);
        }
        /// <summary>
        /// 自动判别微觉醒的类型
        /// </summary>
        /// <param name="endTime"></param>
        /// <param name="newMarkTyp"></param>
        private void AutoArousalScore(DateTime endTime, IMarker.MarkType newMarkTyp, IMarker.MarkType oldMarkTyp)
        {
            IMarker Arousal = SearchArousalMarker(endTime, oldMarkTyp);
            if (Arousal != null)
            {
                UpdateArousalScore(Arousal, newMarkTyp);
            }
        }
        /// <summary>
        /// 更新觉醒事件到指定新类型
        /// </summary>
        /// <param name="oldMarker"></param>
        /// <param name="newMarkTyp"></param>
        private void UpdateArousalScore(IMarker oldMarker, IMarker.MarkType newMarkTyp)
        {
            if (oldMarker.MarkTyp == IMarker.MarkType.None)
                return;
            IMarker respArousal = MarkerManage.Default.DefineMarkers.Find(t => t.MarkTyp == newMarkTyp);
            if (respArousal != null)
            {
                IMarker.MarkType _oldMarkTyp = oldMarker.MarkTyp;
                ///更改掉事件列表中的事件属性
                oldMarker.MarkTyp = respArousal.MarkTyp;
                oldMarker.Name = respArousal.Name;
                oldMarker.ForeColor = respArousal.BackColor;
                oldMarker.Description = respArousal.Description;
                oldMarker.Comments = respArousal.Comments;
                int[] oldChannels = (oldMarker as RectangleMarkers).OnChannelIndexs;
                string[] ss = oldMarker.ID.Split(':');
                oldMarker.ID = string.Format("{0}:{1}", oldChannels[0], ss[1]);
                //RectangleMarkers addone = null;
                ///先删除老的微觉醒，删除是防止新的类别微觉醒跟微觉醒的作用通道不一致
                for (int i = 0; i < oldChannels.Length; i++)
                {
                    CurveItem curveItem = this.ChartArea.FindCurve(oldChannels[i]);
                    if (curveItem != null)
                    {
                        string id = string.Format("{0}:{1}", oldChannels[i], ss[1]);
                        curveItem.CurrentMarks.RemoveAll(t => t.ID == id);
                        if (i == 0)
                        {
                            ///CurrentMarks有删除事件的话，bak_EventRecordList一定要同步
                            bak_EventRecordList.RemoveAll(t => t.ID == id);
                        }
                    }
                }
                ///把事件属性更改后重新加入到对应通道
                int[] newChannels = (respArousal as RectangleMarkers).OnChannelIndexs;
                for (int i = 0; i < newChannels.Length; i++)
                {
                    CurveItem curveItem = this.ChartArea.FindCurve(newChannels[i]);
                    if (curveItem != null)
                    {
                        string id = string.Format("{0}:{1}", newChannels[i], ss[1]);
                        RectangleMarkers newone = (RectangleMarkers)oldMarker.Clone();
                        newone.ID = id;
                        newone.MinLimitValue = (respArousal as RectangleMarkers).MinLimitValue;
                        curveItem.CurrentMarks.Add(newone);
                        if (i == 0)
                        {
                            ///CurrentMarks有添加事件的话，bak_EventRecordList一定要同步
                            bak_EventRecordList.Add(newone);
                        }
                    }
                }
                int oldstartIndex = (oldMarker as RectangleMarkers).StartIndex;
                int oldendIndex = (oldMarker as RectangleMarkers).EndIndex;
                dataManager.dataFactory.EventsInstance.Add(new DataMangement.EventRecordsUit.EventRecord()
                {
                    StartIndex = oldstartIndex,
                    EndIndex = oldendIndex,
                    MarkType = (int)respArousal.MarkTyp,
                    ByHand = true,
                    Condition = dataManager.ConvertEventCondition(DataMangement.UpdateMode.Update, _oldMarkTyp, oldstartIndex, oldendIndex)
                });
                DataModel.LogInstance.Default.AddLog(string.Format("更新区域标签事件,事件类型为 {0}，事件开始时间为 {1}，事件结束时间为 {2} ", newMarkTyp.ToString(), (oldMarker as RectangleMarkers).StartTime, (oldMarker as RectangleMarkers).EndTime), pSystem.LogManagement.LogLevel.DEBUG);
            }
        }
        /// <summary>
        /// 根据事件类型找关联微觉醒标记
        /// </summary>
        /// <param name="endTime"></param>
        /// <param name="markTyp"></param>
        /// <returns></returns>
        private IMarker SearchArousalMarker(DateTime endTime, IMarker.MarkType markTyp)
        {
            markTyp = getArousalMarkType(markTyp);
            if (markTyp == IMarker.MarkType.None)
            {
                return null;
            }
            return bak_EventRecordList.Find(delegate (IMarker t)
            {
                if (t.MarkTyp == markTyp)
                {
                    RectangleMarkers rectmark = t as RectangleMarkers;
                    if (rectmark.StartTime <= endTime && rectmark.EndTime >= endTime)
                    {
                        return true;
                    }
                }
                return false;
            });
        }
        /// <summary>
        /// 自动变换微觉醒类型
        /// </summary>
        /// <param name="ArousalMark">一个微觉醒类型的事件</param>
        /// <returns></returns>
        private IMarker AutoConvertArousalMark(IMarker ArousalMark)
        {
            IMarker ret = (RectangleMarkers)ArousalMark.Clone();
            DateTime startTime = ArousalMark.MarkCreatTime;
            IMarker.MarkType markTyp = ArousalMark.MarkTyp;
            IMarker find = bak_EventRecordList.Find(delegate (IMarker t)
            {
                if (t.MarkTyp == IMarker.MarkType.LegMove)
                {
                    markTyp = IMarker.MarkType.LmArousal;
                }
                else if ((t.MarkTyp == IMarker.MarkType.Hypopnea || t.MarkTyp == IMarker.MarkType.OA || t.MarkTyp == IMarker.MarkType.CA || t.MarkTyp == IMarker.MarkType.MA))
                {
                    markTyp = IMarker.MarkType.RespArousal;
                }
                else if (t.MarkTyp == IMarker.MarkType.PeriodicalBodyMove)
                {
                    markTyp = IMarker.MarkType.PlmArousal;
                }
                else
                    return false;
                RectangleMarkers rectmark = t as RectangleMarkers;
                if (rectmark.StartTime <= startTime && rectmark.EndTime >= startTime)
                {
                    return true;
                }
                return false;
            });
            if (find == null)
                ret.MarkTyp = IMarker.MarkType.Arousal;
            else
            {
                ret.MarkTyp = markTyp;
            }
            return ret;
        }
        /// <summary>
        /// 判断是否存在关联事件
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="markTyp"></param>
        /// <returns></returns>
        private bool SearchConectMarkerType(DateTime startTime, IMarker.MarkType markTyp)
        {
            IMarker find = bak_EventRecordList.Find(delegate (IMarker t)
             {
                 if (markTyp == IMarker.MarkType.LmArousal)
                 {
                     if (t.MarkTyp != IMarker.MarkType.LegMove)
                         return false;
                 }
                 else if (markTyp == IMarker.MarkType.RespArousal)
                 {
                     if (!(t.MarkTyp == IMarker.MarkType.Hypopnea || t.MarkTyp == IMarker.MarkType.OA || t.MarkTyp == IMarker.MarkType.CA || t.MarkTyp == IMarker.MarkType.MA))
                         return false;
                 }
                 else if (markTyp == IMarker.MarkType.PlmArousal)
                 {
                     if (t.MarkTyp != IMarker.MarkType.PeriodicalBodyMove)
                         return false;
                 }
                 else if (markTyp == IMarker.MarkType.Hypopnea)
                 {
                     if (t.MarkTyp != IMarker.MarkType.OxygenReduce)
                         return false;
                 }
                 else
                 {
                     return false;
                 }
                 RectangleMarkers rectmark = t as RectangleMarkers;
                 if (rectmark.StartTime <= startTime && rectmark.EndTime >= startTime)
                 {
                     return true;
                 }
                 return false;
             });
            return (find != null);
        }
        /// <summary>
        /// 判断是否是微觉醒相关事件
        /// </summary>
        /// <param name="markTyp"></param>
        /// <returns></returns>
        private bool IsArousalMark(IMarker.MarkType markTyp)
        {
            return (markTyp == IMarker.MarkType.Arousal || markTyp == IMarker.MarkType.RespArousal || markTyp == IMarker.MarkType.PlmArousal || markTyp == IMarker.MarkType.LmArousal);
        }
        /// <summary>
        /// 获取微觉醒类型
        /// </summary>
        /// <param name="markTyp"></param>
        /// <returns></returns>
        private IMarker.MarkType getArousalMarkType(IMarker.MarkType markTyp)
        {
            if (!IsArousalMark(markTyp))
            {
                if (markTyp == pChart.IMarker.MarkType.Hypopnea || markTyp == pChart.IMarker.MarkType.CA || markTyp == pChart.IMarker.MarkType.OA || markTyp == pChart.IMarker.MarkType.MA)
                {
                    markTyp = IMarker.MarkType.RespArousal;
                }
                else if (markTyp == pChart.IMarker.MarkType.LegMove)
                {
                    markTyp = IMarker.MarkType.LmArousal;
                }
                else if (markTyp == pChart.IMarker.MarkType.PeriodicalBodyMove)
                {
                    markTyp = IMarker.MarkType.PlmArousal;
                }
                else
                    return IMarker.MarkType.None;
            }
            return markTyp;
        }
        /// <summary>
        /// 拖拉标记事件松开鼠标后，进度条帧同步
        /// </summary>
        /// <param name="endFrameNo"></param>
        /// <param name="moveOffTime"></param>
        private void ChartArea_MouseMoveUpHandler(int endFrameNo, int moveOffTime)
        {
            m_HScrollBarEx1.FrameOffsetValue = moveOffTime;
            m_HScrollBarEx1.CurrentFrameCnt = endFrameNo;
        }
        #endregion
        #region 事件辅助功能方法
        private void ImgSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "png|*.png|jpeg|*.jpeg|bmp|*.bmp";
            sf.RestoreDirectory = true;
            sf.FilterIndex = 1;
            sf.FileName = string.Format("{2}{1:yyMMddhhmmss}Frame{0}", m_currentFrameNo, DateTime.Now, KeyInfo);
            if (sf.ShowDialog() == DialogResult.OK)
            {
                Task.Factory.StartNew(() =>
                {
                    System.Threading.Thread.Sleep(500);
                    Bitmap bit = new Bitmap(this.ChartArea.Width, this.ChartArea.Height);//实例化一个和窗体一样大的bitmap
                    Graphics g = Graphics.FromImage(bit);
                    g.CompositingQuality = CompositingQuality.HighQuality;//质量设为最高
                    g.CopyFromScreen(this.Parent.Left, this.Parent.Top, 0, 0, new Size(ChartArea.Width, ChartArea.Height));//保存整个窗体为图片
                    bit.Save(sf.FileName);//默认保存格式为PNG，保存成jpg格式质量不是很好
                    AhDung.MessageTip.ShowOk("打印成功");
                });
            }
        }
        private void Marker_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;
            pChart.CurveItem item = (tsm.Tag as pChart.CurveItem);
            IMarker marker = MarkerManage.Default.DefineMarkers.Find((IMarker t) => t.Name == tsm.Text);
            item.CurrentMarks[0].Comments = marker.Comments;
            item.CurrentMarks[0].Name = tsm.Text;
            item.CurrentMarks[0].Description = marker.Description;
            item.CurrentMarks[0].ForeColor = marker.BackColor;
            item.CurrentMarks[0].MarkTyp = marker.MarkTyp;
            (item.CurrentMarks[0] as pChart.RectangleMarkers).MinLimitValue = (marker as pChart.RectangleMarkers).MinLimitValue;
            if (marks_CreatingMarksHandler(item.ID, item.CurrentMarks[0], false))
            {
                item.removeMarks = FilitRemoveMarks(item.removeMarks, item.CurrentMarks[0].MarkTyp);
                if (item.removeMarks.Count > 0)
                {
                    deleteMarks(item.ID, item.removeMarks);
                    ChartArea.RemoveMarks(item.removeMarks);
                }
            }
            ChartArea.ChartVedioLineInvalidate();
        }
        private void ViewClientMenu_KeyDown(object sender, KeyEventArgs e)
        {
            ChartArea.setKeyDown(e);
            ViewClientMenu.Close();
        }
        private void Measure_Click(object sender, EventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                ToolStripMenuItem tsm = (ToolStripMenuItem)sender;
                pChart.CurveItem channel = (tsm.Tag as pChart.CurveItem);
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
                m_Compelet = true;
                ChartArea.ChartVedioLineInvalidate();
            }));
        }

        private void LeftMarkerCreating_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;
            tools.AddMarks marks = new tools.AddMarks();
            marks.isRectangle = true;
            //拖拉然后点击弹窗出来的添加事件 时间没有赋值 
            marks.SetMaxMinDate(m_end, m_Start);
            marks.CreatingMarksHandler += marks_CreatingMarksHandler;
            marks.Dock = DockStyle.Fill;
            pChart.CurveItem item = (tsm.Tag as pChart.CurveItem);
            marks.ChannelID = item.ID;
            marks.perMarker = item.CurrentMarks[0];
            marks.StartTime = (item.CurrentMarks[0] as RectangleMarkers).StartTime;
            marks.EndTime = (item.CurrentMarks[0] as RectangleMarkers).EndTime;
            Block.Moudle mm = new Block.Moudle();
            mm.Text = Program.Language == "EN" ? "Add Event" : "添加事件";
            mm.Size = new Size(marks.Width, marks.Height + mm.CaptionHeight);
            mm.Controls.Add(marks);
            if (mm.ShowDialog() != DialogResult.OK)
                ChartArea.ChartVedioLineInvalidate();
            m_Compelet = true;
        }
        private void RightMarkerCreating_Click(object sender, EventArgs e)
        {
            //右键 添加事件
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;
            tools.AddMarks marks = new tools.AddMarks();
            marks.isRectangle = false;
            marks.SetMaxMinDate(m_end, m_Start);
            marks.CreatingMarksHandler += marks_CreatingMarksHandler;
            marks.Dock = DockStyle.Fill;
            pChart.CurveItem item = (tsm.Tag as pChart.CurveItem);
            marks.ChannelID = ChannelID.All.ToString();///通道ID
            marks.perMarker = item.CurrentMarks[0];
            marks.StartTime = (item.CurrentMarks[0] as StringMarkers).MarkCreatTime;
            Block.Moudle mm = new Block.Moudle();
            mm.Text = Program.Language == "EN" ? "Add Event" : "添加事件";
            mm.Size = new Size(marks.Width, marks.Height + mm.CaptionHeight);
            mm.StartPosition = FormStartPosition.Manual;
            mm.Location = Cursor.Position;
            mm.Controls.Add(marks);
            if (mm.ShowDialog() != DialogResult.OK)
                ChartArea.ChartVedioLineInvalidate();
            m_Compelet = true;
        }
        private void AllMaker_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;
            pChart.CurveItem item = (tsm.Tag as pChart.CurveItem);
            IMarker marker = MarkerManage.Default.DefineMarkers.Find((IMarker t) => t.Name == tsm.Text);
            item.CurrentMarks[0].Name = tsm.Text;
            item.CurrentMarks[0].Description = marker.Description;
            item.CurrentMarks[0].ForeColor = marker.BackColor;
            item.CurrentMarks[0].MarkTyp = marker.MarkTyp;
            marks_CreatingMarksHandler(ChannelID.All.ToString(), item.CurrentMarks[0], false);
            ChartArea.ChartVedioLineInvalidate();
            m_Compelet = true;
        }
        private object m_objLock = new object();
        private void ZoomView_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)sender;
            ZoomChart zoomChart = new ZoomChart();
            RectangleMarkers rectangleMarkers = (toolStripMenuItem.Tag as CurveItem).CurrentMarks[0] as RectangleMarkers;
            zoomChart.InitSmithData(rectangleMarkers.RectanglePoints, rectangleMarkers.StartTime, (toolStripMenuItem.Tag as CurveItem).TimeSpan);
            SkinForm skinForm = new SkinForm();
            skinForm.Text = Program.Language == "EN" ? "Localized enlargement" : "局部放大";
            skinForm.ShowIcon = false;
            skinForm.ShowInTaskbar = false;
            skinForm.Owner = base.ParentForm;
            skinForm.Size = new Size(800, 600);
            skinForm.StartPosition = FormStartPosition.CenterParent;
            skinForm.Controls.Add(zoomChart);
            zoomChart.Dock = DockStyle.Fill;
            skinForm.ShowDialog();
            ChartArea.ChartVedioLineInvalidate();
            this.m_Compelet = true;
        }
        #endregion
        #region 通道属性临时更改
        private void ChartArea_ChannelHeadPopupHandler(pChart.CurveItem channel, MouseButtons buttons)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                HeadClientContent.Items.Clear();

                var item = new tools.ChannelItemConfigCopy();
                ChannelTable find = Channel.Default.FindChannelTable(channel.ID);
                item.Text = find.Name;
                item.Init(find);
                item.UpdateItemHandle += item_UpdateItemHandle;
                item.StartPosition = FormStartPosition.Manual;
                item.Location = Cursor.Position;
                item.Location = EnsureLocationHelper.CalculateSituableLocation(item);
                item.ShowDialog();
            }));
        }
        /// <summary>
        /// 临时属性被更改时触发
        /// </summary>
        /// <param name="channel"></param>
        private void item_UpdateItemHandle(ChannelTable channel)
        {
            ChannelTable channelTable = Channel.Default.CurrentSaveTable.Find((ChannelTable t) => t.ID == channel.ID);
            CurveItem curveItem = ChartArea.FindCurve(channel.ID);
            if (channelTable != null)
            {
                channelTable = channel;
            }
            else
            {
                Channel.Default.CurrentSaveTable.Add(channel);
            }
            curveItem.TemporaryControl = true;
            curveItem.Name = channel.Name;
            curveItem.PixelRate = (channel.PixelEnable ? channel.PixelRate : 0f);
            if (channel.PixelEnable)
            {
                if (!channel.MaxMinValueEnable)
                {
                    curveItem.yAxis.SetMaxMinValue(curveItem.PixelRateCoefficient * curveItem.PixelRate, 0f);
                    curveItem.PixelConstants = curveItem.PixelRate;
                }
                else
                {
                    curveItem.PixelConstants = curveItem.yAxis.MaxValue / curveItem.PixelRateCoefficient;
                    curveItem.yAxis.SetMaxMinValue(channel.MaxValue, channel.MinValue);
                    if (curveItem.yAxis.LegendLables.Count == 2)
                    {
                        curveItem.yAxis.LegendLables[0] = channel.MinValue.ToString();
                        curveItem.yAxis.LegendLables[1] = channel.MaxValue.ToString();
                    }
                }
            }
            else
            {
                curveItem.PixelConstants = curveItem.yAxis.MaxValue / curveItem.PixelRateCoefficient;
                curveItem.yAxis.SetMaxMinValue(channel.MaxValue, channel.MinValue);
                if (curveItem.yAxis.LegendLables.Count == 2)
                {
                    curveItem.yAxis.LegendLables[0] = channel.MinValue.ToString();
                    curveItem.yAxis.LegendLables[1] = channel.MaxValue.ToString();
                }
            }
            curveItem.DBaseLineVisible = channel.DBaseLineVisible;
            curveItem.Antipole = channel.Antipole;
            curveItem.HighPass = channel.HighPass;
            curveItem.LowPass = channel.LowPass;
            curveItem.SingleNotch = channel.SingleNotch;
            curveItem.PenColor = channel.PenColor;
            ChartArea.TemporaryInvalidate();
        }
        #endregion
        #endregion

        #region 公有成员
        private string m_guid = "";
        /// <summary>
        /// 数据库关联ID
        /// </summary>
        public string GUID
        {
            set
            {
                m_guid = value;
            }
            get { return m_guid; }
        }
        /// <summary>
        /// 定标的关键字
        /// </summary>
        public string MatchKey { set; get; }
        /// <summary>
        /// 监测日期
        /// </summary>
        public DateTime RecordTime { set; get; }
        public delegate void SleepStateChangedDelegate(string[] state, int frameNo = 0);
        /// <summary>
        /// 睡眠分期发生变化
        /// </summary>
        public event SleepStateChangedDelegate SleepStateChangedHandle;
        /// <summary>
        /// 能否进行算法分析
        /// </summary>
        public bool AnalysisEnable = false;
        /// <summary>
        /// 获取总帧数
        /// </summary>
        public int TotalFrameCnt
        {
            get
            {
                return m_TotalFrameCnt;
            }
        }
        /// <summary>
        /// 加载的edf完整路径
        /// </summary>
        private string m_edfpath = "";
        /// <summary>
        /// 加载的edf完整路径
        /// </summary>
        public string EdfPath
        {
            get
            {
                return m_edfpath;
            }
        }
        /// <summary>
        /// 获取hashcode
        /// </summary>
        public string HashCode
        {
            get
            {
                return m_EdfInstance.DataSource.HashCode;
            }
        }
        /// <summary>
        /// 撤销
        /// </summary>
        public void HistoryDataView_Return()
        {
            #region 暂时不需要
            //if (m_addMarksList.Count <= 0)
            //{
            //    return;
            //}
            //KeyValuePair<string, string> value = m_addMarksList.ElementAt(m_addMarksList.Count - 1);
            //if (value.Value == ChannelID.All.ToString())
            //{
            //    IMarker mark = ChartArea.pChartMarks.Find((IMarker t) => t.ID == value.Key);
            //    if (mark != null)
            //    {
            //        ChartArea.pChartMarks.Remove(mark);
            //        string id2 = mark.ID.Split(':')[1];
            //        bak_EventRecordList.RemoveAll((IMarker t) => t.MarkTyp == mark.MarkTyp && t.ID.Split(':')[1] == id2);
            //        lock (m_removeObjLock)
            //        {
            //            m_removeList.Add(string.Format("{0};{1};{2}", EDF.Default.ConvertToChannelNumEx(value.Value), value.Key, (int)mark.MarkTyp));
            //        }
            //        m_addMarksList.Remove(value.Key);
            //        Thread.Sleep(10);
            //        ChartArea.ChartAreaInvalidate();
            //    }
            //}
            //else
            //{
            //    CurveItem curveItem = ChartArea.FindCurve(value.Value);
            //    if (curveItem != null)
            //    {
            //        IMarker mark = curveItem.m_Marks.Find((IMarker t) => t.ID == value.Key);
            //        if (mark != null)
            //        {
            //            int markTyp = (int)mark.MarkTyp;
            //            MarkEvents(mark, add: false);
            //            curveItem.m_Marks.Remove(mark);
            //            m_addMarksList.Remove(value.Key);
            //            lock (m_removeObjLock)
            //            {
            //                m_removeList.Add(string.Format("{0};{1};{2}", EDF.Default.ConvertToChannelNumEx(value.Value), value.Key, markTyp));
            //            }
            //            Thread.Sleep(10);
            //            mark.Delete = true;
            //            ChartArea.RemoveMarks(mark);
            //            string id = mark.ID.Split(':')[1];
            //            bak_EventRecordList.RemoveAll((IMarker t) => t.MarkTyp == mark.MarkTyp && t.ID.Split(':')[1] == id);
            //        }
            //    }
            //}
            //freshEvents();
            #endregion
        }
        /// <summary>
        /// 滚屏
        /// </summary>
        /// <param name="value"></param>
        public void PageMove(int value, bool allpage)
        {
            if (m_vPlayer != null)
            {
                m_vPlayer.LeftRightKeyControl(value);
            }
            else
                m_HScrollBarEx1.UserKeyDown(value, allpage);
        }

        public void HistoryDataView_KeyDown(object sender)
        {
            DataTable currentChannelTable = Channel.Default.CurrentChannelTable;
            for (int i = 0; i < currentChannelTable.Rows.Count; i++)
            {
                ChannelTable channelTable = ChannelTable.ConvertToChannel(currentChannelTable.Rows[i]);
                if (channelTable.PixelRate == 0f || !channelTable.Visible)
                {
                    continue;
                }
                CurveItem curveItem = ChartArea.FindCurve(channelTable.ID);
                int num = (int)((double)curveItem.getMaxValue() * 0.5);
                if (num == 0)
                {
                    continue;
                }
                string ID = channelTable.ID.Replace("Clone_", "").Replace("Append_", "").Split(':')[0];
                Doc_Channel doc_Channel = Channel.Default.ChannelProperties.Find((Doc_Channel t) => t.Name == ID);
                int[] intPixelRangArray = doc_Channel.intPixelRangArray;
                float pRate = ChartArea.pRate;
                bool flag = false;
                for (int j = 0; j < intPixelRangArray.Length; j++)
                {
                    if ((float)num < (float)intPixelRangArray[j] * pRate)
                    {
                        curveItem.PixelRate = intPixelRangArray[j];
                        currentChannelTable.Rows[i]["Sensitivity"] = doc_Channel.strPixelRangArray[j];
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    curveItem.PixelRate = intPixelRangArray[intPixelRangArray.Length - 1];
                    currentChannelTable.Rows[i]["Sensitivity"] = doc_Channel.strPixelRangArray[intPixelRangArray.Length - 1];
                }
            }
            Channel.Default.ShouldRefresh = true;
            temporaryClear = true;
            ChartArea.ChartInvalidate();
        }

        public void HistoryDataView_KeyDown()
        {
            CurveItem currentItem = ChartArea.GetCurrentItem();
            if (currentItem == null || currentItem.PixelRate == 0f)
            {
                return;
            }
            int num = (int)((double)currentItem.getMaxValue() * 0.5);
            if (num == 0)
            {
                return;
            }
            string ID = currentItem.ID.Replace("Clone_", "").Replace("Append_", "").Split(':')[0];
            Doc_Channel doc_Channel = Channel.Default.ChannelProperties.Find((Doc_Channel t) => t.Name == ID);
            int[] intPixelRangArray = doc_Channel.intPixelRangArray;
            float pRate = ChartArea.pRate;
            bool flag = false;
            DataTable currentChannelTable = Channel.Default.CurrentChannelTable;
            DataRow[] array = currentChannelTable.Select(string.Format("ID='{0}'", currentItem.ID));
            if (array.Length <= 0)
            {
                return;
            }
            for (int i = 0; i < intPixelRangArray.Length; i++)
            {
                if ((float)num < (float)intPixelRangArray[i] * pRate)
                {
                    currentItem.PixelRate = intPixelRangArray[i];
                    array[0]["Sensitivity"] = doc_Channel.strPixelRangArray[i];
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                currentItem.PixelRate = intPixelRangArray[intPixelRangArray.Length - 1];
                array[0]["Sensitivity"] = doc_Channel.strPixelRangArray[intPixelRangArray.Length - 1];
            }
            Channel.Default.ShouldRefresh = true;
            currentItem.TemporaryControl = false;
            ChartArea.ChartInvalidate();
        }
        /// <summary>
        /// 保留最后一次使用的montage名称
        /// </summary>
        /// <param name="name"></param>
        public void UpdateMontageName(string name)
        {
            DataBaseHelper.Default.Update(new Doc_MainViewRecord() { GUID = m_guid }, new Doc_MainViewRecord() { ReviewMontageName = name });
        }
        /// <summary>
        /// 是否中断分析过程
        /// </summary>
        public bool Interrupt = false;
        /// <summary>
        /// 设置中断
        /// </summary>
        /// <param name="interrupt"></param>
        public void setInterrupt(bool interrupt)
        {
            lock (m_lockObj)
                Interrupt = interrupt;
        }

        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <returns></returns>
        public byte[] getDataSource()
        {
            return DataModel.EDF.Default.ReadFile(m_edfpath);
        }

        /// <summary>
        /// 绑定文件
        /// </summary>
        /// <param name="fileName"></param>
        public EDF Bind(string fileName, bool isfirst = false)
        {
            m_EdfInstance = new EDFInstance(fileName);
            m_EdfInstance.SearchProgressBarValueEventHandle += M_EdfInstance_SearchProgressBarValueEventHandle;
            if (m_EdfInstance.DataSource.IsCorrect)
            {
                m_edfpath = fileName;
                m_Start = m_EdfInstance.DataSource.StartTime;
                m_end = m_EdfInstance.DataSource.EndTime;
                TimeSpan span = m_end - m_Start;
                int totalSec = (int)span.TotalSeconds;
                m_TotalFrameCnt = ((totalSec % 30 == 0) ? (totalSec / 30) : (totalSec / 30 + 1));
                dataManager.Initialize(Channel.Default.SystemSetting.SaveEdfPath, GUID, TotalFrameCnt);
                KeyInfo = string.Format("{0}({1})", Channel.Default.Patient.PatientName, Channel.Default.Patient.PatientNo);
                DataTable currentChannelTable = Channel.Default.CurrentChannelTable;
                currentChannelTable = ((currentChannelTable.Rows.Count == 0) ? Channel.Default.DefultChannelTable : currentChannelTable);
                if (Channel.Default.CurrentChannelTable.Rows.Count == 0)
                {
                    Channel.Default.CurrentChannelTable = currentChannelTable.Copy();
                    Channel.Default.CurrentChannelPath = Channel.Default.DefaultChannelPath;
                }
                object lockObj = new object();
                DateTime time = DateTime.Now;
                Channel.Default.CurrentSaveTable.Clear();
                for (int i = 0; i < currentChannelTable.Rows.Count; i++)
                {
                    DataRow dr = currentChannelTable.Rows[i];
                    ChannelTable channelTable = ChannelTable.ConvertToChannel(dr);
                    Channel.Default.CurrentSaveTable.Add(channelTable);
                    CurveItem curveItem = Channel.Default.CreatChannel(channelTable);
                    curveItem.ValueZoomRate = 1f;
                    curveItem.SignName = EDF.Default.ConvertToChannelNameEx(curveItem.ChannelNum);
                    curveItem.ReadDataHandler += m_EdfInstance.readChanelData;
                    lock (lockObj)
                    {
                        curveItem = ChartArea.AddCurve(curveItem);
                    }
                }
                Channel.Default.PressureMuzzleFlowChannel = m_EdfInstance.readChanelData(0, m_TotalFrameCnt * 30 * 100, 15);
                //Channel.Default.ThermalMuzzleFlowChannel = m_EdfInstance.readChanelData(0, m_TotalFrameCnt * 30 * 100, 16);
                m_HScrollBarEx1.SetStartEndTime(m_Start, m_end);
                ResultDomain.Default.InitData(m_TotalFrameCnt);
                AnalysisEnable = span.TotalMinutes >= 10.0;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            return m_EdfInstance.DataSource;
        }
        /// <summary>
        /// 执行播放功能
        /// </summary>
        /// <param name="typ">0-跳转到第一帧 1-后退一帧 2-前进一帧 3-跳转到最后一帧 4-开始播放  5-停止播放</param>
        public void Execute(int typ, bool pageAll = false)
        {
            int m_framecnt = m_HScrollBarEx1.CurrentFrameCnt;
            switch (typ)
            {
                default:
                    m_HScrollBarEx1.UserKeyDown(typ, pageAll);
                    break;
                case 4:
                    timer1.Interval = 200;
                    timer1.Enabled = true;
                    break;
                case 5:
                    timer1.Enabled = false;
                    break;
            }
        }
        /// <summary>
        /// 改变当前显示帧
        /// </summary>
        /// <param name="frameCnt"></param>
        public void ChangeFrameNo(int frameCnt)
        {
            m_HScrollBarEx1.CurrentFrameCnt = frameCnt;
        }
        /// <summary>
        /// 手动标记睡眠分期
        /// </summary>
        /// <param name="typ"></param>
        public void MarkSleep(int typ, bool iskeyDown = false)
        {
            DateTime dt = DateTime.Now;
            int num = this.m_HScrollBarEx1.CurrentFrameCnt - 1;
            if (num < ResultDomain.Default.SleepStagPoints.Length && num >= 0)
            {
                bool isadd = false;
                if (ResultDomain.Default.SleepStagPoints[num] == null)
                {
                    ResultDomain.Default.SleepStagPoints[num] = new SuperPointF
                    {
                        XValue = (float)num,
                        YMaxValue = (float)typ
                    };
                    Channel.Default.AnalysisReult.Epochs[num].EpochExist = true;
                    isadd = true;
                }
                else
                {
                    if (!Channel.Default.AnalysisReult.Epochs[num].EpochExist)
                    {
                        isadd = true;
                        Channel.Default.AnalysisReult.Epochs[num].EpochExist = true;
                    }
                    Channel.Default.AnalysisReult.Epochs[num].Stage = typ;
                }
                Channel.Default.AnalysisReult.Epochs[num].ByHand = true;
                ResultDomain.Default.SleepStagPoints[num].YMaxValue = (float)typ;
                dataManager.dataFactory.EpochsInstance.Add(new DataMangement.EpochsUnit.EpochInfo()
                {
                    Index = num,
                    ByHand = true,
                    Epoch = isadd ? new Doc_Epochs
                    {
                        EpochIndex = num,
                        Stage = typ,
                        ByHand = true
                    } : Channel.Default.AnalysisReult.Epochs[num]
                });
                Channel.Default.CanRecover = true;
                this.ChartArea.SleepStage[num] = ((typ == 1) ? "N3" : ((typ == 2) ? "N2" : ((typ == 3) ? "N1" : ((typ == 4) ? "R" : ((typ == 5) ? "W" : "")))));
                if (this.bak_butt.Name == "button2")
                {
                    this.reportChart1.Invalidate(ResultDomain.Default.SleepStagPoints, true);
                }
                else
                {
                    this.reportChart1.CurrentFrameNo = this.m_HScrollBarEx1.CurrentFrameCnt + 1;
                }
                if (!iskeyDown)
                {
                    this.m_AutoValueChange = true;
                    this.m_HScrollBarEx1.NextPage();
                }
                Channel.Default.AnalysisReult.HasDataChange = true;
                DataModel.LogInstance.Default.AddLog(string.Format("用户手动标记睡眠分期，当前帧序号为{0}，标记的睡眠分期为{1}", this.m_HScrollBarEx1.CurrentFrameCnt - 1, this.ChartArea.SleepStage[num]), pSystem.LogManagement.LogLevel.DEBUG);
            }
            Console.WriteLine(string.Format("【{1}】睡眠分期总耗时：{0}ms", (DateTime.Now - dt).TotalMilliseconds, DateTime.Now));
        }
        /// <summary>
        /// 事件标记转换，并显示到相应通道趋势图上去
        /// </summary>
        /// <param name="_marker"></param>
        /// <param name="group"></param>
        /// <param name="grouping"></param>
        private void EventToCureItem(IMarker _marker, IGrouping<string, Doc_EventRecords> group, IGrouping<int, Doc_EventRecords> grouping)
        {
            if (_marker.MarkTyp == pChart.IMarker.MarkType.CheyneStokes)///陈施氏呼吸事件暂时不显示，由医生手动标记
                return;
            bool hasAddList = false;
            RectangleMarkers marker = _marker as RectangleMarkers;
            if (marker != null)
            {
                for (int i = 0; i < marker.OnChannelIndexs.Length; i++)
                {
                    CurveItem curveItem = this.ChartArea.FindCurve(marker.OnChannelIndexs[i]);
                    if (curveItem != null)
                    {
                        List<Doc_EventRecords> list2 = null;
                        if (group != null)
                            list2 = group.ToList<Doc_EventRecords>();
                        else
                            list2 = grouping.ToList<Doc_EventRecords>();
                        for (int j = 0; j < list2.Count; j++)
                        {
                            int startTimespan = curveItem.TimeSpan * list2[j].StartIndex;
                            int endTimeSpan = curveItem.TimeSpan * list2[j].EndIndex;
                            list2[j].StartTime = this.m_Start.AddMilliseconds((double)startTimespan);
                            list2[j].EndTime = this.m_Start.AddMilliseconds((double)endTimeSpan);
                            int startFrameXH = startTimespan / 30000;
                            startFrameXH = ((startTimespan % 30000 == 0) ? startFrameXH : (startFrameXH + 1));
                            int endFrameXH = endTimeSpan / 30000;
                            endFrameXH = ((endTimeSpan % 30000 == 0) ? endFrameXH : (endFrameXH + 1));
                            IMarker item2 = new RectangleMarkers((marker as RectangleMarkers).ReadOnly)
                            {
                                ID = ((list2[j].EventID == "") ? string.Format("{0}:{1}-{2}", curveItem.ChannelNum, list2[j].StartIndex, list2[j].EndIndex) : list2[j].EventID),
                                Name = marker.Name,
                                Description = marker.Description,
                                StartTime = list2[j].StartTime,
                                EndTime = list2[j].EndTime,
                                StartIndex = list2[j].StartIndex,
                                EndIndex = list2[j].EndIndex,
                                StartFrameNo = startFrameXH,
                                EndFrameNo = endFrameXH,
                                MarkCreatTime = list2[j].StartTime,
                                MarkTyp = marker.MarkTyp,
                                Comments = marker.Comments,
                                ForeColor = marker.BackColor,
                                MinLimitValue = marker.MinLimitValue
                            };
                            curveItem.CurrentMarks.Add(item2);
                            if (!hasAddList)
                                this.bak_EventRecordList.Add(item2);
                        }
                    }
                    hasAddList = true;
                }
            }
            else
            {
                List<Doc_EventRecords> list = null;
                if (group != null)
                    list = group.ToList<Doc_EventRecords>();
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].StartTime.Year == 1)
                        list[i].StartTime = this.m_Start.AddSeconds(list[i].StartIndex);

                    int frameXH = list[i].StartIndex / 30;
                    frameXH = (list[i].StartIndex % 30 == 0) ? frameXH : (frameXH + 1);

                    IMarker item = new StringMarkers
                    {
                        ID = string.Format("{0}:{1}", (int)ChannelID.All, _marker.MarkTyp.ToString()),
                        Name = _marker.Name,
                        Description = _marker.Description,
                        MarkCreatTime = list[i].StartTime,
                        StartFrameNo = frameXH,
                        EndFrameNo = frameXH,
                        MarkTyp = _marker.MarkTyp,
                        ForeColor = _marker.BackColor
                    };
                    this.ChartArea.pChartMarks.Add(item);
                    this.bak_EventRecordList.Add(item);
                }
            }
        }
        /// <summary>
        /// 自定义事件加载完成
        /// </summary>
        private bool zidingyievenrok = false;
        /// <summary>
        /// 加载之前分析的轨迹
        /// </summary>
        public void LoadAnalysisData(AnalysisResult data, bool isanalysis = false, bool isrecover = false)
        {
            if (data != null)
            {
                dataManager.StartServer();
                if (data.Epochs.Count == 0)
                {
                    m_EdfInstance.ReadBaseInfo();
                    dataManager.SaveSpo2Values(m_EdfInstance.Spo2Values);
                    dataManager.SavePressureValues(m_EdfInstance.PressurePeekValues);
                    Spo2Values = m_EdfInstance.Spo2Values;
                    HeartValues = m_EdfInstance.HeartValues;
                    mtValues = m_EdfInstance.MTValues;
                    dataManager.SaveCalobrationValues(m_EdfInstance.CalibrationValues);
                    CalibrationValues = m_EdfInstance.CalibrationValues;
                    posValues = m_EdfInstance.PosValues;
                    PressureValues = m_EdfInstance.PressurePeekValues;
                    AnalysisResult ret = getBaseInfoer(data.GUID);
                    ret.StartTime = m_Start;
                    ret.EndTime = m_end;
                    ret.Tag = Channel.Default.AnalysisReult.Tag;
                    ret.EventRecords = data.EventRecords;
                    Channel.Default.AnalysisReult = ret;
                    data = ret;
                }
                else
                {
                    if (data.OldVision)///为了兼容1.0版本做了一个特殊处理
                    {
                        m_EdfInstance.ReadBaseInfo();
                        dataManager.SaveSpo2Values(m_EdfInstance.Spo2Values);
                        dataManager.SavePressureValues(m_EdfInstance.PressurePeekValues);
                        Spo2Values = m_EdfInstance.Spo2Values;
                        HeartValues = m_EdfInstance.HeartValues;
                        mtValues = m_EdfInstance.MTValues;
                        dataManager.SaveCalobrationValues(m_EdfInstance.CalibrationValues);
                        CalibrationValues = m_EdfInstance.CalibrationValues;
                        posValues = m_EdfInstance.PosValues;
                        PressureValues = m_EdfInstance.PressurePeekValues;
                    }
                    else if (!isrecover)
                    {
                        //恢复时候这些基础数据不需要再读一遍
                        Spo2Values = dataManager.dataFactory.Spo2Instance.ReadResult();
                        CalibrationValues = dataManager.dataFactory.CalibrationInstance.ReadResult();
                        PressureValues = dataManager.dataFactory.PressureInstance.ReadResult();
                        //存血氧数据的文件 有时会没有生成 所以这里重新读取一遍
                        if (Spo2Values.Count == 0 || CalibrationValues.Count == 0 || PressureValues.Count == 0)
                        {
                            m_EdfInstance.ReadBaseInfo();
                            dataManager.SaveSpo2Values(m_EdfInstance.Spo2Values);
                            dataManager.SavePressureValues(m_EdfInstance.PressurePeekValues);
                            Spo2Values = m_EdfInstance.Spo2Values;
                            HeartValues = m_EdfInstance.HeartValues;
                            mtValues = m_EdfInstance.MTValues;
                            dataManager.SaveCalobrationValues(m_EdfInstance.CalibrationValues);
                            CalibrationValues = m_EdfInstance.CalibrationValues;
                            posValues = m_EdfInstance.PosValues;
                            PressureValues = m_EdfInstance.PressurePeekValues;
                        }
                    }
                }
                #region 事件标记转换，并显示到相应通道趋势图上去
                IEnumerable<IGrouping<int, Doc_EventRecords>> recordsGroup = from t in data.EventRecords
                                                                             group t by t.EventType;
                if (this.bak_EventRecordList == null)
                {
                    this.bak_EventRecordList = new List<IMarker>(data.EventRecords.Count);
                }
                else
                {
                    this.bak_EventRecordList.RemoveAll((IMarker t) => t.MarkTyp != IMarker.MarkType.None);
                }
                //恢复时候 所有事件都应该删除
                if (isrecover)
                {
                    reportChart1.MultipleSleepMarks.Clear();
                    this.bak_EventRecordList.Clear();
                    m_HScrollBarEx1.Invalidate();
                }
                ChartArea.ClearMarkers(isrecover);
                bool lighton = false;
                bool lightoff = false;
                foreach (IGrouping<int, Doc_EventRecords> grouping in recordsGroup)
                {
                    zidingyievenrok = false;
                    //if (grouping.Key == (int)IMarker.MarkType.MultipleSleep && !this.m_isLoad)
                    if (grouping.Key == (int)IMarker.MarkType.MultipleSleep)
                    {
                        List<Doc_EventRecords> sleepmarks = grouping.ToList<Doc_EventRecords>();
                        if (sleepmarks.Count > 0)
                        {
                            reportChart1.MultipleSleepMarks.Clear();
                            m_MultSleepList.AddRange(sleepmarks.Select(t => new RectangleMarkers() { StartIndex = t.StartIndex, EndIndex = t.EndIndex, MarkTyp = IMarker.MarkType.MultipleSleep, Comments2 = t.Tag }));
                            reportChart1.MultipleSleepMarks.AddRange(sleepmarks.Select(t => new RectangleMarkers() { StartIndex = t.StartIndex, EndIndex = t.EndIndex, MarkTyp = IMarker.MarkType.MultipleSleep, Comments2 = t.Tag }));
                        }
                        continue;
                    }
                    IMarker _marker = null;
                    if (grouping.Key == (int)IMarker.MarkType.None)
                    {
                        //自定义事件额外按照名字进行分类
                        IEnumerable<IGrouping<string, Doc_EventRecords>> recordsGroup2 = from t in grouping.ToList()
                                                                                         group t by t.Tag;
                        foreach (IGrouping<string, Doc_EventRecords> group in recordsGroup2)
                        {
                            _marker = MarkerManage.Default.DefineMarkers.Find((IMarker t) => (int)t.MarkTyp == grouping.Key && t.Name == group.Key);
                            if (_marker != null)
                                EventToCureItem(_marker, group, null);
                        }
                        zidingyievenrok = true;
                    }
                    else
                    {
                        _marker = MarkerManage.Default.DefineMarkers.Find((IMarker t) => (int)t.MarkTyp == grouping.Key);
                    }
                    //开关灯
                    if (grouping.Key > (int)IMarker.MarkType.None)
                    {
                        List<Doc_EventRecords> list = grouping.ToList<Doc_EventRecords>();
                        for (int i = 0; i < list.Count; i++)
                        {
                            IMarker.MarkType mktyp = _marker.MarkTyp;
                            if (list[i].StartTime.Year == 1)
                                list[i].StartTime = this.m_Start.AddSeconds(list[i].StartIndex);
                            else
                            {
                                list[i].StartIndex = (int)(list[i].StartTime - this.m_Start).TotalSeconds;
                            }
                            if (mktyp == IMarker.MarkType.LightOn)
                            {
                                lighton = true;
                                Channel.Default.AnalysisReult.Sleep.LightOnTime = list[i].StartTime;
                            }
                            else if (mktyp == IMarker.MarkType.LightOff)
                            {
                                lightoff = true;
                                Channel.Default.AnalysisReult.Sleep.LightOffTime = list[i].StartTime;
                            }
                            int frameXH = list[i].StartIndex / 30;
                            frameXH = ((list[i].StartIndex % 30 == 0) ? frameXH : (frameXH + 1));
                            IMarker item = new StringMarkers
                            {
                                ID = string.Format("{0}:{1}", (int)ChannelID.All, mktyp.ToString()),
                                Name = _marker.Name,
                                Description = _marker.Description,
                                MarkCreatTime = list[i].StartTime,
                                StartFrameNo = frameXH,
                                EndFrameNo = frameXH,
                                MarkTyp = mktyp,
                                ForeColor = _marker.BackColor
                            };
                            this.ChartArea.pChartMarks.Add(item);
                            this.bak_EventRecordList.Add(item);
                        }
                    }
                    else
                    {
                        if (_marker != null && zidingyievenrok == false)
                        {
                            EventToCureItem(_marker, null, grouping);
                        }
                    }
                }
                if (this.m_isLoad)
                {
                    base.Invoke(new MethodInvoker(delegate ()
                    {
                        this.reportChart1.SetLightONOFF(lightoff ? data.Sleep.LightOffTime : default(DateTime), lighton ? data.Sleep.LightOnTime : default(DateTime));
                    }));
                }
                #endregion
                ///绑定数据
                dataManager.BindData(Channel.Default.AnalysisReult, isanalysis);
                ResultDomain resultDomain = data.ConvertResultDomain(this.m_TotalFrameCnt, true);
                ResultDomain.Default.StartTime = data.StartTime;
                ResultDomain.Default.EndTime = data.EndTime;
                if (resultDomain.SleepStagPoints.Length > 0)
                {
                    ResultDomain.Default.SleepStagPoints = resultDomain.SleepStagPoints;
                    if (this.m_isLoad)
                    {
                        for (int l = 0; l < resultDomain.SleepStagPoints.Length; l++)
                        {
                            float ymaxValue = resultDomain.SleepStagPoints[l].YMaxValue;
                            this.ChartArea.SleepStage[l] = ((ymaxValue == 1f) ? "N3" : ((ymaxValue == 2f) ? "N2" : ((ymaxValue == 3f) ? "N1" : ((ymaxValue == 4f) ? "R" : ((ymaxValue == 5f) ? "W" : "")))));
                        }
                        this.SleepStateChange(1);
                    }
                }
                if (resultDomain.BloodOxygenPoints.Length > 0)
                {
                    ResultDomain.Default.BloodOxygenPoints = resultDomain.BloodOxygenPoints;
                }
                if (resultDomain.BodyStatePoints.Length > 0)
                {
                    ResultDomain.Default.BodyStatePoints = resultDomain.BodyStatePoints;
                    if (this.m_isLoad)
                    {
                        for (int m = 0; m < resultDomain.BodyStatePoints.Length; m++)
                        {
                            this.ChartArea.SleepPos[m] = (int)resultDomain.BodyStatePoints[m].YMaxValue;
                        }
                    }
                }
                if (resultDomain.CAPoints.Length > 0)
                {
                    ResultDomain.Default.CAPoints = resultDomain.CAPoints;
                }
                if (resultDomain.HeartRatePoints.Length > 0)
                {
                    ResultDomain.Default.HeartRatePoints = resultDomain.HeartRatePoints;
                }
                if (resultDomain.HypopneaPoints.Length > 0)
                {
                    ResultDomain.Default.HypopneaPoints = resultDomain.HypopneaPoints;
                }
                if (resultDomain.MAPoints.Length > 0)
                {
                    ResultDomain.Default.MAPoints = resultDomain.MAPoints;
                }
                if (resultDomain.OAPoints.Length > 0)
                {
                    ResultDomain.Default.OAPoints = resultDomain.OAPoints;
                }
                if (resultDomain.MTPoints.Length > 0)
                {
                    ResultDomain.Default.MTPoints = resultDomain.MTPoints;
                }
                if (resultDomain.PLMPoints.Length > 0)
                {
                    ResultDomain.Default.PLMPoints = resultDomain.PLMPoints;
                }
                if (resultDomain.PLMsPoints.Length > 0)
                {
                    ResultDomain.Default.PLMsPoints = resultDomain.PLMsPoints;
                }
                if (resultDomain.MicArousalPoints.Length > 0)
                {
                    ResultDomain.Default.MicArousalPoints = resultDomain.MicArousalPoints;
                }
                Channel.Default.CanRecover = dataManager.HasSaveAsFile();
                //默认进回放 并且没有备份文件保存一次
                if (!isrecover && Channel.Default.CanRecover == false)
                {
                    EpochsEventsSaveAs();
                }
                if (this.m_isLoad)
                {
                    this.freshreportChart();
                    this.freshEvents(true);
                    this.ChartArea.ChartInvalidate();
                }
                //如果是已完成状态的记录不允许恢复备份
                if (Channel.Default.ScoreLock)
                    Channel.Default.CanRecover = false;
            }
        }

        /// <summary>
        /// 获取当前汇总的事件标记
        /// </summary>
        /// <returns></returns>
        public AnalysisResult getAnalysisResult(bool AIAnalysis = false)
        {
            AnalysisResult analysisResult = new AnalysisResult();
            analysisResult.StartTime = this.m_Start;
            analysisResult.EndTime = this.m_end;
            analysisResult.EdfPath = this.m_edfpath;
            analysisResult.GUID = this.m_guid;
            List<string> itemIDs = this.ChartArea.getItemIDs();
            List<IMarker> list = this.ChartArea.pChartMarks.FindAll((IMarker t) => t.MarkTyp == IMarker.MarkType.LightOff || t.MarkTyp == IMarker.MarkType.LightOn);
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    IMarker marker = list[i];
                    if (marker.MarkTyp == IMarker.MarkType.LightOff)
                    {
                        analysisResult.LightOffTime = marker.MarkCreatTime;
                    }
                    else
                    {
                        analysisResult.LightOnTime = marker.MarkCreatTime;
                    }
                    Doc_EventRecords item = new Doc_EventRecords
                    {
                        EventID = marker.ID,
                        EventType = (int)marker.MarkTyp,
                        ChannelID = 255,
                        StartTime = marker.MarkCreatTime,
                        EndTime = marker.MarkCreatTime,
                        GUID = this.m_guid,
                        Description = marker.Description,
                        EventName = marker.Name
                    };
                    analysisResult.EventRecords.Add(item);
                }
            }
            if (analysisResult.LightOffTime.Year == 1)
            {
                analysisResult.LightOffTime = this.m_Start;
            }
            if (analysisResult.LightOnTime.Year == 1)
            {
                analysisResult.LightOnTime = this.m_end;
            }
            List<float> spo2values = new List<float>(Spo2Values);
            bool spo2Reset = false;
            for (int j = 0; j < itemIDs.Count; j++)
            {
                CurveItem curveItem = this.ChartArea.FindCurve(itemIDs[j]);
                if (curveItem != null && !curveItem.ID.Contains("_Clone"))
                {
                    List<IMarker> marks = curveItem.CurrentMarks;
                    if (curveItem.ChannelNum == 17)
                    {
                        List<IMarker> more = marks.FindAll(t => t.MarkTyp == pChart.IMarker.MarkType.Spo2Artifact);
                        if (more.Count > 0)
                        {
                            for (int s = 0; s < more.Count; s++)
                            {
                                RectangleMarkers mk = more[s] as RectangleMarkers;
                                for (int m = mk.StartIndex; m <= mk.EndIndex; m++)
                                {
                                    if (m < spo2values.Count)
                                        spo2values[m] = 0;
                                }
                            }
                            spo2Reset = true;
                        }
                    }
                    for (int k = 0; k < marks.Count; k++)
                    {
                        if (marks[k] is RectangleMarkers && marks[k].MarkTyp != IMarker.MarkType.None)
                        {
                            RectangleMarkers rect = marks[k] as RectangleMarkers;
                            Doc_EventRecords doc_EventRecords = new Doc_EventRecords
                            {
                                EventID = rect.ID,
                                EventType = (int)rect.MarkTyp,
                                ChannelID = curveItem.ChannelNum,
                                StartIndex = rect.StartIndex,
                                EndIndex = rect.EndIndex,
                                StartTime = rect.StartTime,
                                EndTime = rect.EndTime,
                                GUID = this.m_guid,
                                Description = rect.Description,
                                EventName = rect.Name,
                                TimeSpan = curveItem.TimeSpan
                            };
                            if (curveItem.ChannelNum == 17)
                            {
                                IEnumerable<float> source = this.Spo2Values.Where((float num, int index) => index >= rect.StartIndex && index <= rect.EndIndex && num > 0f);
                                doc_EventRecords.Tag = ((source.Count<float>() > 0) ? source.Min() : 0f).ToString("f2");
                                doc_EventRecords.Comments = ((source.Count<float>() > 0) ? source.Max() - source.Min() : 0f).ToString("f2");
                            }
                            analysisResult.EventRecords.Add(doc_EventRecords);
                        }
                    }
                }
            }
            for (int i = 0; i < reportChart1.MultipleSleepMarks.Count; i++)
            {
                RectangleMarkers sleepMark = reportChart1.MultipleSleepMarks[i] as RectangleMarkers;
                analysisResult.EventRecords.Add(new Doc_EventRecords()
                {
                    StartIndex = sleepMark.StartIndex,
                    EndIndex = sleepMark.EndIndex,
                    EventType = (int)sleepMark.MarkTyp
                });

            }
            if (AIAnalysis)
                ///记录中附加上被删除的事件列表，算法需要做甄别
                analysisResult.EventRecords.AddRange(dataManager.dataFactory.EventsInstance.getDeleteList());
            List<Doc_Epochs> list2 = new List<Doc_Epochs>();
            if (ResultDomain.Default.SleepStagPoints != null)
            {
                for (int i = 0; i < m_TotalFrameCnt; i++)
                {
                    float num2 = -1f;
                    Doc_Epochs item2 = new Doc_Epochs();
                    if (ResultDomain.Default.SleepStagPoints[i] != null && i < Channel.Default.AnalysisReult.Epochs.Count)
                    {
                        num2 = ResultDomain.Default.SleepStagPoints[i].YMaxValue;
                        {
                            Channel.Default.AnalysisReult.Epochs[i].Stage = (int)num2;
                            string[] ss = Channel.Default.AnalysisReult.Epochs[i].SpO2.Split(',');
                            if (ss.Length < 4 || spo2Reset)
                            {
                                float max = 0, min = 0, ave = 0;
                                int vaildCnt = 0;
                                int oneframevaluesCnt = 30;
                                int offset = 30;
                                int startIndex = i * oneframevaluesCnt;
                                if (startIndex + offset > spo2values.Count)
                                    offset = spo2values.Count - startIndex;
                                int[] secs = new int[8];
                                ///（最小，最大，均值，90-100持续时间,85-90，80-90,70-80,60-70,50-60,40-50,40以下）
                                for (int s = startIndex; s < startIndex + offset; s++)
                                {
                                    float value = spo2values[s];
                                    if (value > 0)
                                    {
                                        if (max == 0)
                                        {
                                            max = value;
                                        }
                                        else if (max < value)
                                        {
                                            max = value;
                                        }
                                        if (min == 0)
                                        {
                                            min = value;
                                        }
                                        else if (min > value)
                                        {
                                            min = value;
                                        }
                                        ave += value;
                                        vaildCnt++;
                                        if (value >= 90)
                                        {
                                            secs[0]++;
                                        }
                                        else if (value >= 80)
                                        {
                                            secs[2]++;
                                            if (value >= 85)
                                            {
                                                secs[1]++;
                                            }
                                        }
                                        else if (value >= 70)
                                        {
                                            secs[3]++;
                                        }
                                        else if (value >= 60)
                                        {
                                            secs[4]++;
                                        }
                                        else if (value >= 50)
                                        {
                                            secs[5]++;
                                        }
                                        else if (value >= 40)
                                        {
                                            secs[6]++;
                                        }
                                        else
                                        {
                                            secs[7]++;
                                        }
                                    }
                                }
                                Channel.Default.AnalysisReult.Epochs[i].SpO2 = string.Format("{0},{1},{2:f2},{3}", min, max, vaildCnt > 0 ? ave / vaildCnt : 0, string.Join(",", secs));
                            }
                            item2 = Channel.Default.AnalysisReult.Epochs[i];
                        }
                    }
                    else
                    {
                        item2 = new Doc_Epochs
                        {
                            EpochIndex = i,
                            Stage = (int)num2,
                            //SpO2 = string.Format("0,0,{0}", this.m_spo2_Ave[i]),
                            //HeartRate = string.Format("0,0,{0}", this.m_heart_Ave[i])
                        };
                    }
                    list2.Add(item2);
                }
            }
            Channel.Default.AnalysisReult.Epochs = list2;
            analysisResult.Epochs = new List<Doc_Epochs>(Channel.Default.AnalysisReult.Epochs);
            try
            {
                float pres = PressureValues.Average();
                analysisResult.BreathEvent.AveragePressureInTIB = (float)Math.Round(pres, 2);
            }
            catch { }
            return analysisResult;
        }
        /// <summary>
        /// 空的待分析
        /// </summary>
        /// <returns></returns>
        public AnalysisResult getEmptyAnalysisResult(int framecnt)
        {
            AnalysisResult ret = new AnalysisResult();
            ret.StartTime = m_Start;
            for (int i = 0; i < framecnt; i++)
            {
                Doc_EventRecords record = new Doc_EventRecords()
                {
                    EventType = (int)pChart.IMarker.MarkType.Stage,
                    StartIndex = i,
                    ChannelID = (int)ChannelID.All,
                    EndIndex = i,
                    GUID = m_guid,
                    Description = "0"
                };
                ret.EventRecords.Add(record);
            }
            return ret;
        }
        /// <summary>
        /// 主动释放资源
        /// </summary>
        public void SourceDispose()
        {
            m_EdfInstance.Dispose();
            dataManager.dataFactory.Dispose();///释放资源
        }
        /// <summary>
        /// 在点击 开始分析之后 将帧信息和事件信息 另存一份到本地
        /// </summary>
        public bool EpochsEventsSaveAs()
        {
            return dataManager.SaveCopyAs();
        }
        /// <summary>
        /// 在点击恢复图标之后 将帧信息和事件信息 恢复成之前 另存到本地的文件
        /// </summary>
        public bool EpochsEventsRecover()
        {
            if (dataManager.RecoverCopyAs())
            {
                dataManager.dataFactory.EpochsInstance.Initialize(TotalFrameCnt);
                dataManager.dataFactory.EpochsInstance.Start();
                dataManager.dataFactory.EventsInstance.Start();
                return true;
            }
            return false;
        }
        #endregion

        #region 事件列表
        private DataTable m_EventRecordList = null;
        private List<IMarker> bak_EventRecordList = new List<IMarker>();
        /// <summary>
        /// 初始化
        /// </summary>
        private void initLoad()
        {

            EventRecordCase.SelectedIndexChanged += EventRecordCase_SelectedIndexChanged;
            EventRecordCase.DropDown += EventRecordCase_DropDown;
            initSelectText();
            EventRecordCase.SelectedIndex = 0;
            toolStripComboBox1.SelectedIndexChanged += toolStripComboBox1_SelectedIndexChanged;
        }
        private void EventRecordCase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EventRecordCase.SelectedItem != null)
            {
                string selectevent = EventRecordCase.SelectedItem.ToString().Split(' ')[0];
                if (!string.IsNullOrEmpty(selectevent))
                {
                    if (selectevent == (Program.Language == "EN" ? "SpO2_Decrease" : "氧减"))
                    {
                        toolStripComboBox1.Items.Clear();
                        toolStripComboBox1.Visible = true;
                        toolStripComboBox1.Items.Add(" ");
                        toolStripComboBox1.Items.Add(Program.Language == "EN" ? "Minimum blood oxygen value" : "最小血氧值");
                        toolStripComboBox1.Items.Add(Program.Language == "EN" ? "Longest oxygen reduction" : "最长氧减");
                    }
                    else if (selectevent == (Program.Language == "EN" ? "Hypopnea" : "低通气"))
                    {
                        toolStripComboBox1.Items.Clear();
                        toolStripComboBox1.Visible = true;
                        toolStripComboBox1.Items.Add(" ");
                        toolStripComboBox1.Items.Add(Program.Language == "EN" ? "Longest hypoventilation" : "最长低通气");
                    }
                    else if (selectevent == (Program.Language == "EN" ? "Mixed_Apnea" : "混合性呼吸暂停"))
                    {
                        toolStripComboBox1.Items.Clear();
                        toolStripComboBox1.Visible = true;
                        toolStripComboBox1.Items.Add(" ");
                        toolStripComboBox1.Items.Add(Program.Language == "EN" ? "Longest mixed apnea" : "最长混合性呼吸暂停");
                    }
                    else if (selectevent == (Program.Language == "EN" ? "Obstructive_Apnea" : "阻塞性呼吸暂停"))
                    {
                        toolStripComboBox1.Items.Clear();
                        toolStripComboBox1.Visible = true;
                        toolStripComboBox1.Items.Add(" ");
                        toolStripComboBox1.Items.Add(Program.Language == "EN" ? "Longest obstructive sleep apnea" : "最长阻塞性呼吸暂停");
                    }
                    else if (selectevent == (Program.Language == "EN" ? "Central_Apnea" : "中枢性呼吸暂停"))
                    {
                        toolStripComboBox1.Items.Clear();
                        toolStripComboBox1.Visible = true;
                        toolStripComboBox1.Items.Add(" ");
                        toolStripComboBox1.Items.Add(Program.Language == "EN" ? "Longest central apnea" : "最长中枢性呼吸暂停");
                    }
                    else if (selectevent == (Program.Language == "EN" ? "PLMS" : "周期性循环腿动"))
                    {
                        toolStripComboBox1.Items.Clear();
                        toolStripComboBox1.Visible = true;
                        toolStripComboBox1.Items.Add(" ");
                        toolStripComboBox1.Items.Add(Program.Language == "EN" ? "Related leg movements" : "相关联腿动");
                    }
                    else
                    {
                        toolStripComboBox1.Visible = false;
                    }
                }
                else
                {
                    toolStripComboBox1.Visible = false;
                }
            }
            freshEvents(true);
            isfirstselect = true;
        }
        private void freshEvents(bool isSelectedIndexChange = false)
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                if (!isSelectedIndexChange)
                    Channel.Default.CanRecover = true;
                if (EventRecordCase.SelectedIndex == 0)
                {
                    FillEventData();
                }
                else if (EventRecordCase.SelectedIndex == 1)
                {
                    FillEventData((int)IMarker.MarkType.None);
                }
                else
                {
                    string[] name = EventRecordCase.Text.Split(' ');
                    IMarker rect = MarkerManage.Default.DefineMarkers.Find(t => t.Name == name[0]);
                    //IMarker rect = MarkerManage.Default.DefineMarkers.Find(t => t.Name == EventRecordCase.Text);
                    if (rect != null)
                    {
                        FillEventData((int)rect.MarkTyp);
                    }
                }
            }));
        }

        /// <summary>
        /// 填充事件
        /// </summary>
        /// <param name="markTyp"></param>
        private void FillEventData(int markTyp = -1)
        {
            List<IMarker> select = new List<IMarker>();
            List<IMarker> spo2Events = new List<IMarker>();
            List<IMarker> LegsMoveEvents = new List<IMarker>();
            List<IMarker> PLMsEvents = new List<IMarker>();
            if (bak_EventRecordList.Count != 0)
            {
                if (markTyp == -1)
                {
                    select = bak_EventRecordList.OrderBy(t => t.MarkCreatTime).ToList();
                }
                else
                    select = bak_EventRecordList.Where(t => t.MarkTyp == (IMarker.MarkType)markTyp).OrderBy(t => t.MarkCreatTime).ToList();
                spo2Events = bak_EventRecordList.FindAll(t => t.MarkTyp == IMarker.MarkType.OxygenReduce);
                LegsMoveEvents = bak_EventRecordList.FindAll(t => t.MarkTyp == IMarker.MarkType.LegMove);
                var list = bak_EventRecordList.FindAll(t => t.MarkTyp == IMarker.MarkType.PeriodicalBodyMove);
                PLMsEvents = list.Where((x, i) => list.FindIndex(n => n.ID == x.ID) == i).ToList();
                try
                {
                    foreach (IMarker OnePLMs in PLMsEvents)
                    {
                        int legsinPLMscount = 0;
                        int s = (OnePLMs as RectangleMarkers).StartFrameNo;
                        int e = (OnePLMs as RectangleMarkers).EndFrameNo;
                        for (int i = 0; i < LegsMoveEvents.Count; i++)
                        {
                            if (LegsMoveEvents[i].StartFrameNo >= s && LegsMoveEvents[i].EndFrameNo <= e)
                            {
                                legsinPLMscount++;
                            }
                        }
                        OnePLMs.Tag = legsinPLMscount;
                    }
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee.Message);
                }

                foreach (IMarker m in spo2Events)
                {
                    int s = (m as RectangleMarkers).StartIndex;
                    int e = (m as RectangleMarkers).EndIndex;
                    if (s >= Spo2Values.Count || e >= Spo2Values.Count)
                    {
                        continue;
                    }
                    float spo2Min = 100;
                    for (int i = s; i <= e; i++)
                    {
                        if (Spo2Values[i] == 0)
                            continue;
                        if (spo2Min > Spo2Values[i])
                        {
                            spo2Min = Spo2Values[i];
                        }
                    }
                    if (spo2Min != 100)
                        m.Tag = spo2Min;
                }
            }
            DataTable source = m_EventRecordList.Clone();
            List<string> PLMsEventsIdList = new List<string>();
            for (int i = 0; i < select.Count; i++)
            {
                IMarker mark = select[i];
                if (mark is StringMarkers)
                {
                    source.Rows.Add(mark.MarkCreatTime, 1, mark.Name, mark.Description, (Program.Language == "EN" ? "Point Label" : "点位标签"), mark.StartFrameNo, mark.StartFrameNo, mark.ID);
                }
                else
                {
                    RectangleMarkers mark2 = mark as RectangleMarkers;
                    if (mark2.ReadOnly)
                        continue;
                    string des = "";
                    if (mark2.MarkTyp == IMarker.MarkType.OxygenReduce)
                    {
                        if (mark2.Tag != null)
                            if (Program.Language == "EN")
                                des = string.Format("Minimum oxygen saturation：{0:f1}%", mark2.Tag);
                            else
                                des = string.Format("最低氧饱和度：{0:f1}%", mark2.Tag);
                    }
                    else if (mark2.MarkTyp == IMarker.MarkType.Hypopnea)
                    {
                        DateTime endt = mark2.EndTime.AddSeconds(30);
                        IMarker spo2find = spo2Events.Find(t => t.MarkCreatTime > mark2.StartTime && t.MarkCreatTime < endt);
                        if (spo2find != null)
                        {
                            if (Program.Language == "EN")
                                des = string.Format("Accompanied by oxygen depletion lasting for{1}s{0}", spo2find.Tag != null ? string.Format(" ，Minimum blood oxygen：{0:f1}%", spo2find.Tag) : "", Math.Round(((spo2find as RectangleMarkers).EndTime - spo2find.MarkCreatTime).TotalSeconds, 2));
                            else
                                des = string.Format("伴随氧减持续{1}s{0}", spo2find.Tag != null ? string.Format(" ，最低血氧：{0:f1}%", spo2find.Tag) : "", Math.Round(((spo2find as RectangleMarkers).EndTime - spo2find.MarkCreatTime).TotalSeconds, 2));
                        }
                    }
                    else if (mark2.MarkTyp == IMarker.MarkType.OA)
                    {
                        DateTime endt = mark2.EndTime.AddSeconds(30);
                        IMarker spo2find = spo2Events.Find(t => t.MarkCreatTime > mark2.StartTime && t.MarkCreatTime < endt);
                        if (spo2find != null)
                        {
                            if (Program.Language == "EN")
                                des = string.Format("Accompanied by oxygen depletion lasting for{1}s{0}", spo2find.Tag != null ? string.Format(" ，Minimum blood oxygen：{0:f1}%", spo2find.Tag) : "", Math.Round(((spo2find as RectangleMarkers).EndTime - spo2find.MarkCreatTime).TotalSeconds, 2));
                            else
                                des = string.Format("伴随氧减持续{1}s{0}", spo2find.Tag != null ? string.Format(" ，最低血氧：{0:f1}%", spo2find.Tag) : "", Math.Round(((spo2find as RectangleMarkers).EndTime - spo2find.MarkCreatTime).TotalSeconds, 2));
                        }
                    }
                    else if (mark2.MarkTyp == IMarker.MarkType.CA)
                    {
                        DateTime endt = mark2.EndTime.AddSeconds(30);
                        IMarker spo2find = spo2Events.Find(t => t.MarkCreatTime > mark2.StartTime && t.MarkCreatTime < endt);
                        if (spo2find != null)
                        {
                            if (Program.Language == "EN")
                                des = string.Format("Accompanied by oxygen depletion lasting for{1}s{0}", spo2find.Tag != null ? string.Format(" ，Minimum blood oxygen：{0:f1}%", spo2find.Tag) : "", Math.Round(((spo2find as RectangleMarkers).EndTime - spo2find.MarkCreatTime).TotalSeconds, 2));
                            else
                                des = string.Format("伴随氧减持续{1}s{0}", spo2find.Tag != null ? string.Format(" ，最低血氧：{0:f1}%", spo2find.Tag) : "", Math.Round(((spo2find as RectangleMarkers).EndTime - spo2find.MarkCreatTime).TotalSeconds, 2));
                        }
                    }
                    else if (mark2.MarkTyp == IMarker.MarkType.MA)
                    {
                        DateTime endt = mark2.EndTime.AddSeconds(30);
                        IMarker spo2find = spo2Events.Find(t => t.MarkCreatTime > mark2.StartTime && t.MarkCreatTime < endt);
                        if (spo2find != null)
                        {
                            if (Program.Language == "EN")
                                des = string.Format("Accompanied by oxygen depletion lasting for{1}s{0}", spo2find.Tag != null ? string.Format(" ，Minimum blood oxygen：{0:f1}%", spo2find.Tag) : "", Math.Round(((spo2find as RectangleMarkers).EndTime - spo2find.MarkCreatTime).TotalSeconds, 2));
                            else
                                des = string.Format("伴随氧减持续{1}s{0}", spo2find.Tag != null ? string.Format(" ，最低血氧：{0:f1}%", spo2find.Tag) : "", Math.Round(((spo2find as RectangleMarkers).EndTime - spo2find.MarkCreatTime).TotalSeconds, 2));
                        }
                    }
                    else if (mark2.MarkTyp == IMarker.MarkType.PeriodicalBodyMove)
                    {
                        if (Program.Language == "EN")
                            des = string.Format("During this period, the legs moved {0} times", mark2.Tag);
                        else
                            des = string.Format("此期间腿动{0}次", mark2.Tag);
                        if (PLMsEvents.Contains(mark) && !PLMsEventsIdList.Contains(mark.ID))
                        {
                            PLMsEventsIdList.Add(mark.ID);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    source.Rows.Add(mark.MarkCreatTime, Math.Round((mark2.EndTime - mark2.StartTime).TotalSeconds, 2), mark.Name, des, Program.Language == "EN" ? "Area Label" : "区域标签", mark.StartFrameNo, mark.EndFrameNo, mark.ID);
                }
            }
            selectgridview.SuspendLayout();
            selectgridview.Enabled = true;
            selectgridview.DataSource = source;
            selectgridview.ResumeLayout();
        }
        private void abSelect_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                int framecnt = Convert.ToInt32(selectgridview.Rows[e.RowIndex].Cells["StartFrameNo"].Value);
                DateTime st = Convert.ToDateTime(selectgridview.Rows[e.RowIndex].Cells["CreatTime"].Value);
                if (framecnt * 30 <= (st - m_Start).TotalSeconds)
                {
                    framecnt++;
                }
                ChangeFrameNo(framecnt);
            }
        }
        /// <summary>
        /// 初始化表单
        /// </summary>
        private void initEventTable()
        {
            m_EventRecordList = new DataTable();
            m_EventRecordList.Columns.Add("CreatTime", typeof(DateTime));
            m_EventRecordList.Columns.Add("LastTime", typeof(float));
            m_EventRecordList.Columns.Add("MarkerName");
            m_EventRecordList.Columns.Add("MarkerDescription");
            m_EventRecordList.Columns.Add("MarkerType");
            m_EventRecordList.Columns.Add("StartFrameNo");
            m_EventRecordList.Columns.Add("EndFrameNo");
            m_EventRecordList.Columns.Add("MarkerID");
            //selectgridview.DataSource = m_EventRecordList;
            selectgridview.RowPostPaint += selectgridview_RowPostPaint;
            selectgridview.CellDoubleClick += abSelect_CellDoubleClick;
        }

        private void selectgridview_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, selectgridview.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), selectgridview.RowHeadersDefaultCellStyle.Font, rectangle, selectgridview.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }
        /// <summary>
        /// 初始化选择下拉框
        /// </summary>
        private void initSelectText()
        {
            List<IMarker> markEvents = new List<IMarker>();
            List<string> name = new List<string>();
            for (int i = 0; i < DataModel.MarkerManage.Default.DefineMarkers.Count; i++)
            {
                IMarker mark = DataModel.MarkerManage.Default.DefineMarkers[i];
                if (mark.MarkTyp != IMarker.MarkType.None)
                {
                    if (mark is RectangleMarkers)
                    {
                        if ((mark as RectangleMarkers).ReadOnly)
                            continue;
                        markEvents = bak_EventRecordList.FindAll(t => t.MarkTyp == mark.MarkTyp);
                    }
                    else
                    {
                        markEvents = bak_EventRecordList.FindAll(t => t.MarkTyp == mark.MarkTyp);
                    }

                    if (mark.MarkTyp == IMarker.MarkType.PeriodicalBodyMove)
                    {
                        var PLMsEvents = markEvents.Where((x, y) => markEvents.FindIndex(n => n.ID == x.ID) == y).ToList();
                        name.Add(string.Format("{0}  ({1})", mark.Name, PLMsEvents.Count));
                        continue;
                    }

                    name.Add(string.Format("{0}  ({1})", mark.Name, markEvents.Count));
                }
            }
            EventRecordCase.Items.Clear();
            EventRecordCase.Items.Add(" ");
            EventRecordCase.Items.Add(Program.Language == "EN" ? "Custom" : "自定义");
            EventRecordCase.Items.AddRange(name.ToArray());
        }

        private void initSelectTextEx()
        {
            toolStripComboBoxTreeView1.TreeView.Nodes.Add("  ");
            toolStripComboBoxTreeView1.TreeView.Nodes.Add(Program.Language == "EN" ? "Custom" : "自定义");

            ///添加开关灯
            TreeNode light = new TreeNode("开关灯");
            light.Tag = string.Format("{0}/{1}", (int)IMarker.MarkType.LightOff, (int)IMarker.MarkType.LightOn);
            TreeNode lightoff = new TreeNode(Program.Language == "EN" ? "Light_Off" : "关灯");
            lightoff.Tag = string.Format("{0}", (int)IMarker.MarkType.LightOff);
            light.Nodes.Add(lightoff);
            TreeNode lighton = new TreeNode(Program.Language == "EN" ? "Light_On" : "开灯");
            lighton.Tag = string.Format("{0}", (int)IMarker.MarkType.LightOn);
            light.Nodes.Add(lighton);
            toolStripComboBoxTreeView1.TreeView.Nodes.Add(light);

            ///添加呼吸事件
            TreeNode breath = new TreeNode(Program.Language == "EN" ? " Respiratory_Events" : "呼吸事件");
            breath.Tag = string.Format("{0}/{1}/{2}/{3}", (int)IMarker.MarkType.OA, (int)IMarker.MarkType.CA, (int)IMarker.MarkType.MA, (int)IMarker.MarkType.Hypopnea);
            TreeNode apNode = new TreeNode(Program.Language == "EN" ? "Apnea" : "呼吸暂停");
            apNode.Tag = string.Format("{0}/{1}/{2}", (int)IMarker.MarkType.OA, (int)IMarker.MarkType.CA, (int)IMarker.MarkType.MA);
            TreeNode oa = new TreeNode(Program.Language == "EN" ? "Obstructive_Apnea" : "阻塞性呼吸暂停");
            oa.Tag = string.Format("{0}", (int)IMarker.MarkType.OA);
            apNode.Nodes.Add(oa);
            TreeNode ca = new TreeNode(Program.Language == "EN" ? "Central_Apnea" : "中枢性呼吸暂停");
            ca.Tag = string.Format("{0}", (int)IMarker.MarkType.CA);
            apNode.Nodes.Add(ca);
            TreeNode ma = new TreeNode(Program.Language == "EN" ? "Mixed_Apnea" : "混合性呼吸暂停");
            ma.Tag = string.Format("{0}", (int)IMarker.MarkType.MA);
            apNode.Nodes.Add(ma);
            breath.Nodes.Add(apNode);
            TreeNode hyp = new TreeNode(Program.Language == "EN" ? "Hypopnea" : "低通气");
            hyp.Tag = string.Format("{0}", (int)IMarker.MarkType.Hypopnea);
            breath.Nodes.Add(hyp);
            toolStripComboBoxTreeView1.TreeView.Nodes.Add(breath);

            ///添加觉醒事件
            TreeNode arual = new TreeNode("微觉醒");
            arual.Tag = string.Format("{0}/{1}/{2}/{3}", (int)IMarker.MarkType.Arousal, (int)IMarker.MarkType.LmArousal, (int)IMarker.MarkType.PlmArousal, (int)IMarker.MarkType.RespArousal);
            TreeNode Autoarual = new TreeNode("自发微觉醒");
            Autoarual.Tag = string.Format("{0}", (int)IMarker.MarkType.Arousal);
            arual.Nodes.Add(Autoarual);
            TreeNode resp = new TreeNode("呼吸相关微觉醒");
            resp.Tag = string.Format("{0}", (int)IMarker.MarkType.RespArousal);
            arual.Nodes.Add(resp);
            TreeNode PlmArousal = new TreeNode("PLMS相关微觉醒");
            PlmArousal.Tag = string.Format("{0}", (int)IMarker.MarkType.PlmArousal);
            arual.Nodes.Add(PlmArousal);
            TreeNode LmArousal = new TreeNode("腿动相关微觉醒");
            LmArousal.Tag = string.Format("{0}", (int)IMarker.MarkType.LmArousal);
            arual.Nodes.Add(LmArousal);
            toolStripComboBoxTreeView1.TreeView.Nodes.Add(arual);
            toolStripComboBoxTreeView1.TreeView.ExpandAll();
            toolStripComboBoxTreeView1.DropDownWidth = toolStripComboBoxTreeView1.Width;
            toolStripComboBoxTreeView1.DropDownHeight = toolStripComboBoxTreeView1.TreeView.ItemHeight * 9 + 15;
            toolStripComboBoxTreeView1.ComboBox.Height = toolStripComboBoxTreeView1.Owner.Height;
        }
        /// <summary>
        /// 分屏时 点击事件可以跳转(点击的屏不动 另一屏跳转)
        /// </summary>
        /// <param name="StartFrameNo"></param>
        /// <param name="StartTime"></param>
        private void ChartArea_JumpHappenHandler(int StartFrameIndex, DateTime MarkStartTime, int belong)
        {
            if (StartFrameIndex * ChartArea.OtherBaseTimeLineSpan <= (int)(MarkStartTime - m_Start).TotalSeconds)
            {
                StartFrameIndex++;
            }
            ChartArea.Invalidate(m_Start, StartFrameIndex, 0, m_TotalFrameCnt, belong);
        }
        #endregion

        #region 加载视频
        private Form play = null;
        private VedioPlayer m_vPlayer = null;
        private int m_currentFrameNo = 1;
        private int m_startViewFrameNo = 1;
        private bool m_runpage = false;
        private long m_totalMillTimes = 0;
        public int CurrentFrameNo
        {
            set
            {
                m_currentFrameNo = value;
            }
        }
        private int bak_offmoveFrame = 0;
        private int m_frameadd = 0;
        private int m_PlaySpan = 40;//ms
        private void playRecordViewTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                playRecordViewTimer.Interval = m_PlaySpan;
                if (m_vPlayer == null || m_runpage)
                    return;
                int currentPlayTimes = m_vPlayer.CurrentPlayTimes + m_PlaySpan;
                if (currentPlayTimes <= m_totalMillTimes)
                {
                    play_TimeLineChangeHandler(m_vPlayer.CurrentPlayTimes);
                    m_vPlayer.SetCurrentTime(currentPlayTimes);
                }
                else
                {
                    currentPlayTimes = (int)m_totalMillTimes;
                    m_vPlayer.SetCurrentTime(currentPlayTimes);
                    playRecordViewTimer.Stop();
                }
            }
            catch (Exception ee)
            {

            }
        }
        private void play_TimeLineChangeHandler(int obj)
        {
            if (m_vedioRun)
            {
                if (m_runpage)
                    return;
                DateTime dt = m_Start.AddMilliseconds(obj);
                ChartArea.VedioTimeLine = dt;
                int fc = obj / 30000;///该视频已播放多少帧
                if (obj % 30000 != 0)
                {
                    fc += 1;
                }
                int offfc = obj / (Channel.Default.BaseTimeLineSpan * 1000);///该视频已播放多少个数据屏，
                int ViewCntInframe = 30 / Channel.Default.BaseTimeLineSpan;////一帧有多少数据屏

                int offmoveFrame = 0;
                if (ViewCntInframe != 0)
                {
                    offmoveFrame = offfc % ViewCntInframe;///帧的内部偏移
                }
                if (m_currentFrameNo != fc && fc <= TotalFrameCnt && fc >= 1)
                {
                    if (ViewCntInframe < 1)
                    {
                        m_frameadd = (Channel.Default.BaseTimeLineSpan / 30);
                        bool cansee = fc < m_startViewFrameNo;
                        if (fc - m_startViewFrameNo >= m_frameadd || cansee)
                        {
                            int jumpframeno = cansee ? m_startViewFrameNo - m_frameadd : m_startViewFrameNo + m_frameadd;
                            if (jumpframeno < 1)
                            {
                                jumpframeno = 1;
                            }
                            else if (jumpframeno > m_TotalFrameCnt)
                            {
                                jumpframeno = m_TotalFrameCnt;
                            }
                            m_vedioAutoJump = true;
                            m_HScrollBarEx1.CurrentFrameCnt = jumpframeno;
                        }
                        else
                        {
                            ChartArea.ChartVedioLineInvalidate();
                        }
                    }
                    else
                    {
                        m_vedioAutoJump = true;
                        m_HScrollBarEx1.CurrentFrameCnt = fc;
                    }
                }
                else
                {
                    if (bak_offmoveFrame != offmoveFrame)
                    {
                        bak_offmoveFrame = offmoveFrame;
                        ChartArea.Invalidate(m_Start, m_currentFrameNo, offmoveFrame * Channel.Default.BaseTimeLineSpan, m_TotalFrameCnt);
                    }
                    else
                    {
                        ChartArea.ChartVedioLineInvalidate();
                    }
                }
            }
        }
        private int m_timeLine = 30;
        /// <summary>
        /// 获取视频所在路径
        /// </summary>
        public string VedioViewPath
        {
            get
            {
                if (string.IsNullOrEmpty(MatchKey))
                    return "";
                return new MyConfiguration() { SaveDirectory = m_VedioViewPath, MatchKey = MatchKey }.FileName;
            }
        }
        /// <summary>
        /// 视频播放 开始/结束
        /// </summary>
        /// <param name="on"></param>
        public void PlayVedioRecord(bool on)
        {
            if (on)
            {
                if (play == null)
                {
                    m_vPlayer = new VedioPlayer(m_myConfiguration);
                    m_vPlayer.PlayStatusChanged += M_vPlayer_PlayStatusChanged;
                    //m_vPlayer.TimeLineChangeHandler += play_TimeLineChangeHandler;
                    m_totalMillTimes = (long)(m_end - m_Start).TotalMilliseconds;
                    m_vPlayer.Dock = DockStyle.Fill;
                    m_vPlayer.IsRealView = false;
                    m_vPlayer.StartViewTime = m_Start;
                    m_vPlayer.StartPlayTime = (m_currentFrameNo - 1) * 30000 + 1000;///偏移1s
                    playRecordViewTimer.Start();
                    play = new SkinForm();
                    play.ShowIcon = false;
                    play.Text = "视频回放";
                    play.Size = new System.Drawing.Size(430, 300);
                    play.MinimumSize = new Size(m_vPlayer.MinimumSize.Width, m_vPlayer.MinimumSize.Height + 100);
                    play.Controls.Add(m_vPlayer);
                    play.Left = Screen.AllScreens[0].WorkingArea.Right - play.Width;
                    play.Top = Screen.AllScreens[0].WorkingArea.Bottom - play.Height;
                    play.StartPosition = FormStartPosition.Manual;
                    play.ControlBox = false;
                    play.Owner = this.ParentForm;
                    play.Show();
                }
            }
            else
            {
                if (play != null)
                {
                    playRecordViewTimer.Stop();
                    m_vPlayer.CloseAll();
                    play.Close();
                    play = null;
                    m_vPlayer = null;
                    ChartArea.VedioTimeLine = m_Start;
                    ChartArea.ChartVedioLineInvalidate();
                }
            }
            m_timeLine = Channel.Default.BaseTimeLineSpan;
        }
        /// <summary>
        /// 监听到播放状态发生变化
        /// </summary>
        /// <param name="playStatus"></param>
        private void M_vPlayer_PlayStatusChanged(bool playStatus)
        {
            if (m_vPlayer != null)
                if (playStatus)
                {
                    playRecordViewTimer.Start();
                    m_vPlayer.TimeLineChangeHandler -= play_TimeLineChangeHandler;
                }
                else
                {
                    playRecordViewTimer.Stop();
                    m_vPlayer.TimeLineChangeHandler += play_TimeLineChangeHandler;
                }
        }
        #endregion

        #region 计算基本信息
        public AnalysisResult getBaseInfoer(string guid)
        {
            AnalysisResult result = new AnalysisResult();
            result.Epochs = new List<Doc_Epochs>(m_TotalFrameCnt);
            for (int i = 0; i < m_TotalFrameCnt; i++)
            {
                Doc_Epochs one = new Doc_Epochs();
                one.EpochIndex = i;
                one.GUID = guid;
                int oneframevaluesCnt = 30;
                int offset = 30;
                int startIndex = i * oneframevaluesCnt;
                if (startIndex + offset > Spo2Values.Count)
                    offset = Spo2Values.Count - startIndex;
                float[] values = new float[oneframevaluesCnt];
                Spo2Values.CopyTo(startIndex, values, 0, offset);
                var list = values.Where(t => t > 0);
                if (list.Count() > 0)
                {
                    one.SpO2 = string.Format("{0},{1},{2:f2}", list.Min(), list.Max(), list.Average());
                }
                else
                    one.SpO2 = "0,0,0";
                HeartValues.CopyTo(startIndex, values, 0, offset);
                list = values.Where(t => t > 0);
                int maxvalueindex = 0;
                int minvalueindex = 0;
                float maxvalue = 0;
                float minvalue = 101;
                if (list.Count() > 0)
                {
                    for (int k = 0; k < values.Length; k++)
                    {
                        if (values[k] > maxvalue)
                        {
                            maxvalueindex = k;
                            maxvalue = values[k];
                        }
                        if (values[k] < minvalue)
                        {
                            minvalueindex = k;
                            minvalue = values[k];
                        }
                    }
                    one.HeartRate = string.Format("{0},{1},{2:f2},{3},{4}", list.Min(), list.Max(), list.Average(), minvalueindex, maxvalueindex);
                }
                else
                    one.HeartRate = "0,0,0,0,0";
                posValues.CopyTo(startIndex, values, 0, offset);
                var ss = values.GroupBy(t => t);
                int max = 0;
                one.Pos = -1;
                foreach (var item in ss)
                {
                    int len = ss.ToList().Count;
                    if (max < len)
                    {
                        max = len;
                        one.Pos = (int)item.Key;
                    }
                }
                try
                {
                    mtValues.CopyTo(startIndex, values, 0, offset);
                }
                catch { }
                float avg = values.Average();
                one.MT = (float)Math.Round(values.Sum(x => Math.Pow(x - avg, 2)) / values.Count(), 2);
                result.Epochs.Add(one);
            }
            return result;
        }
        #endregion

        private void EventRecordCase_DropDown(object sender, EventArgs e)
        {
            string s = EventRecordCase.Text;
            List<IMarker> markEvents = new List<IMarker>();
            List<string> name = new List<string>();
            for (int i = 0; i < DataModel.MarkerManage.Default.DefineMarkers.Count; i++)
            {
                IMarker mark = DataModel.MarkerManage.Default.DefineMarkers[i];
                if (mark.MarkTyp != IMarker.MarkType.None)
                {
                    if (mark is RectangleMarkers)
                    {
                        if ((mark as RectangleMarkers).ReadOnly)
                            continue;
                        markEvents = bak_EventRecordList.FindAll(t => t.MarkTyp == mark.MarkTyp);
                    }
                    else
                    {
                        markEvents = bak_EventRecordList.FindAll(t => t.MarkTyp == mark.MarkTyp);
                    }

                    if (mark.MarkTyp == IMarker.MarkType.PeriodicalBodyMove)
                    {
                        var PLMsEvents = markEvents.Where((x, y) => markEvents.FindIndex(n => n.ID == x.ID) == y).ToList();
                        name.Add(string.Format("{0}  ({1})", mark.Name, PLMsEvents.Count));
                        continue;
                    }

                    name.Add(string.Format("{0}  ({1})", mark.Name, markEvents.Count));
                }
            }
            EventRecordCase.Items.Clear();
            EventRecordCase.Items.Add(" ");
            EventRecordCase.Items.Add(Program.Language == "EN" ? "Custom" : "自定义");
            EventRecordCase.Items.AddRange(name.ToArray());
            EventRecordCase.Text = s;
        }
        private bool isfirstselect = true;
        private DataTable baksource = new DataTable();
        /// <summary>
        /// 事件列表二级菜单下拉框选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectgridview.Rows.Count > 0)
            {
                DataTable source = (DataTable)selectgridview.DataSource;
                DataTable newsource = m_EventRecordList.Clone();
                if (isfirstselect)
                {
                    baksource = source;
                    isfirstselect = false;
                }
                if (toolStripComboBox1.SelectedItem.ToString() == " " && isfirstselect == false)
                {
                    newsource = baksource;
                }
                else if (toolStripComboBox1.SelectedItem.ToString() == (Program.Language == "EN" ? "Minimum blood oxygen value" : "最小血氧值") && isfirstselect == false)
                {
                    double minspo2 = 101;
                    int minspo2index = 0;
                    for (int i = 0; i < source.Rows.Count; i++)
                    {
                        string[] ss = baksource.Rows[i].ItemArray[3].ToString().Split(new char[] { '：', '%' });
                        double spo2value = Convert.ToDouble(ss[1]);
                        if (spo2value < minspo2)
                        {
                            minspo2 = spo2value;
                            minspo2index = i;
                        }
                    }
                    newsource.Rows.Add(baksource.Rows[minspo2index].ItemArray[0], baksource.Rows[minspo2index].ItemArray[1], baksource.Rows[minspo2index].ItemArray[2], baksource.Rows[minspo2index].ItemArray[3], baksource.Rows[minspo2index].ItemArray[4], baksource.Rows[minspo2index].ItemArray[5], baksource.Rows[minspo2index].ItemArray[6], baksource.Rows[minspo2index].ItemArray[7]);
                }
                else if (toolStripComboBox1.SelectedItem.ToString() == (Program.Language == "EN" ? "Related leg movements" : "相关联腿动") && isfirstselect == false)
                {
                    int PLMsLegsCount = 0;
                    RectangleMarkers rectangleMarkers = new RectangleMarkers();
                    List<IMarker> bak_legsevents = bak_EventRecordList.FindAll(t => t.MarkTyp == IMarker.MarkType.LegMove);
                    //获得所有的周期性循环腿动列表
                    for (int i = 0; i < baksource.Rows.Count; i++)
                    {
                        newsource.Rows.Add(baksource.Rows[i].ItemArray[0], baksource.Rows[i].ItemArray[1], baksource.Rows[i].ItemArray[2], baksource.Rows[i].ItemArray[3], baksource.Rows[i].ItemArray[4], baksource.Rows[i].ItemArray[5], baksource.Rows[i].ItemArray[6], baksource.Rows[i].ItemArray[7]);
                    }
                    for (int i = 0; i < bak_legsevents.Count; i++)
                    {
                        for (int k = 0; k < baksource.Rows.Count; k++)
                        {
                            try
                            {
                                //第5列是开始帧，第6列是结束帧
                                if (bak_legsevents[i].StartFrameNo >= Convert.ToInt32(baksource.Rows[k].ItemArray[5].ToString()) && bak_legsevents[i].EndFrameNo <= Convert.ToInt32(baksource.Rows[k].ItemArray[6].ToString()))
                                {
                                    //为了计算腿动持续时间
                                    rectangleMarkers = bak_legsevents[i] as RectangleMarkers;
                                    if (rectangleMarkers != null)
                                    {
                                        newsource.Rows.Add(rectangleMarkers.MarkCreatTime, Math.Round((rectangleMarkers.EndTime - rectangleMarkers.StartTime).TotalSeconds, 2), rectangleMarkers.Name, "", "区域标签", rectangleMarkers.StartFrameNo, rectangleMarkers.EndFrameNo, rectangleMarkers.ID);
                                        PLMsLegsCount++;
                                        break;
                                    }
                                }
                            }
                            catch (Exception ee)
                            {
                                DataModel.LogInstance.Default.AddLog(string.Format("点击相关联腿动出错，错误信息为：{0}", ee.Message), pSystem.LogManagement.LogLevel.ERROR);
                            }
                        }
                    }
                    //在第一行增加总次数，以记录开始时间为创建时间，帧固定为第一帧，确保排序后一定在第一行
                    newsource.Rows.Add(m_Start, 0, "PLMs总次数", string.Format("{0}次", PLMsLegsCount), Program.Language == "EN" ? "Area Label" : "区域标签", 1, 1, "");
                    //按照事件创建时间排序
                    newsource.DefaultView.Sort = "CreatTime";
                }
                else
                {
                    newsource = selectmaxvalue(newsource);
                }
                selectgridview.SuspendLayout();
                selectgridview.Enabled = true;
                selectgridview.DataSource = newsource;
                selectgridview.ResumeLayout();
            }
        }
        /// <summary>
        /// 查找表中最大值
        /// </summary>
        /// <param name="newsource"></param>
        /// <returns></returns>
        private DataTable selectmaxvalue(DataTable newsource)
        {
            double maxvalue = 0;
            int maxvalueindex = 0;
            for (int i = 0; i < baksource.Rows.Count; i++)
            {
                double ss = Convert.ToDouble(baksource.Rows[i].ItemArray[1].ToString());
                if (ss > maxvalue)
                {
                    maxvalue = ss;
                    maxvalueindex = i;
                }
            }
            newsource.Rows.Add(baksource.Rows[maxvalueindex].ItemArray[0], baksource.Rows[maxvalueindex].ItemArray[1], baksource.Rows[maxvalueindex].ItemArray[2], baksource.Rows[maxvalueindex].ItemArray[3], baksource.Rows[maxvalueindex].ItemArray[4], baksource.Rows[maxvalueindex].ItemArray[5], baksource.Rows[maxvalueindex].ItemArray[6], baksource.Rows[maxvalueindex].ItemArray[7]);
            return newsource;
        }
    }
}
