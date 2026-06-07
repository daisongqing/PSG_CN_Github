using pSystem.Communication.Com;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AwareTec.Polysmith.DataBaseCom;
using AwareTec.Polysmith.Protocol;
using AwareTec.Polysmith.pChart;
using System.Drawing;
using AwareTec.Polysmith.Util;
using System.Reflection;
using System.Diagnostics;
using AwareTec.Polysmith.Cloud.UI.ViewHelpers;
using AwareTec.Polysmith.Cloud.UI.DataModels.EnumModels;
using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.Predefined;


namespace AwareTec.Polysmith.Cloud.UI.DataModels
{
    public class Channel:IDisposable
    {
        #region 私有成员
        private static Channel m_Default = null;
        private DataTable m_DefaultChannel = null;
        private string m_defaultChanelPath = DataModels.Global.GlobalReadonlyString.PredefinedPath.USER_CHANNEL_FILE;
        private ChannelFiliter m_ChannelFiliter = null;
       /// <summary>
       /// 通道配置表单
       /// </summary>
        private DataTable m_DefineData = null;
        #region 内存回收
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        private static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        /// <summary>
        /// 释放内存
        /// </summary>
        private void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
            GC.Collect();
        }
        #endregion
        /// <summary>
        /// 定时清理系统垃圾的线程
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
                    System.Threading.Thread.Sleep(10000);
                    ClearMemory();
                }
            });
            th.IsBackground = true;
            th.SetApartmentState(System.Threading.ApartmentState.MTA);
            th.Start();
        }
        /// <summary>
        /// 重命名通道对象定义
        /// </summary>
        private class ReNameUnion
        {
            /// <summary>
            /// 通道ID
            /// </summary>
            public int ID { set; get; }
            /// <summary>
            /// 原名称
            /// </summary>
            public string OldName { set; get; }
            /// <summary>
            /// 修改后的名称
            /// </summary>
            public string NewName { set; get; }
        }
        private List<ReNameUnion> m_reNameList = new List<ReNameUnion>();
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        public Channel()
        {
            string defaultChannePath = string.Format("{0}{1}", ChannelManageCloud.Default.ConfigruationBasePath, m_defaultChanelPath);

            m_ChannelFiliter = new ChannelFiliter();
            SharpZipLibHelper.UnZip(string.Format("{0}Filiter\\Filiter.zip", AppDomain.CurrentDomain.BaseDirectory), string.Format("{0}Filiter", AppDomain.CurrentDomain.BaseDirectory), "pps");
            m_ChannelFiliter.InitRate(true);

            AnalysisReult = new AnalysisResult();
            BaseTimeLineSpan = 30;
            IsRealTimeView = false;
            SleepLock = true;
            ScoreLock = false;
            AllowShowDoctor = false;

            ReportStructurals = Global.GlobalSingleton.Instance.PredefinedData.PredefinedReportStructure;
            for (int i = 0; i < ReportStructurals.Count; i++)
            {
                ReportStructurals[i].SavePath = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, ReportStructurals[i].SavePath);
            }
            m_KillTask = false;
            TaskStart();
            FnStop = false;
        }
        /// <summary>
        /// 对象实例
        /// </summary>
        public static Channel Default
        {
            get
            {
                return m_Default ?? (m_Default = new Channel());
            }
        }
        /// <summary>
        /// 加载视频
        /// </summary>
        public void LoadVedio(string exeName,string MatchKey,string UrlPath,string savedirectory,bool NotSplitRecordFile,int MaxRecodFileLenght, bool newVedio = false)
        {
            //启动程序
            string app = AppDomain.CurrentDomain.BaseDirectory + exeName;
            if (File.Exists(app))
            {
                Process[] foundProcess = Process.GetProcessesByName(exeName.Replace(".exe",""));
                if (foundProcess.Length > 0)
                {
                    if (newVedio)
                    {
                        try
                        {
                            foundProcess[0].Kill();
                            foundProcess[0].Close();
                            foundProcess[0].Dispose();
                        }
                        catch { }
                    }
                    else
                        return;
                }

                Process pro = new Process();
                pro.StartInfo.FileName = app;
                pro.StartInfo.Arguments = string.Format("{0} {1} {2} {3} {4}", UrlPath, savedirectory, MatchKey, NotSplitRecordFile, MaxRecodFileLenght);
                Debug.Print(pro.StartInfo.Arguments);
                if(pro.Start()) //启动
                {
                   Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("视频程序加载成功"), pSystem.LogManagement.LogLevel.WARN);
                }
                else
                {
                    Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("视频程序加载失败"), pSystem.LogManagement.LogLevel.ERROR);
                }
            }
            else
            {
                Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("视频程序地址错误 查询地址为{0}", app), pSystem.LogManagement.LogLevel.ERROR);
            }
        }
        /// <summary>
        /// 资源释放
        /// </summary>
        public void Dispose()
        {
            m_KillTask = true;
            th.Abort();
            ClearMemory();
        }
        /// <summary>
        /// 报告模板路径
        /// </summary>
        public string ReportTemplatePath
        {
            get
            {
                if (!File.Exists(_SystemSetting.ReportTemplate))
                {
                    return AppDomain.CurrentDomain.BaseDirectory + "DefaultReportTemplate.doc";
                }
                return _SystemSetting.ReportTemplate;
            }
        }
        private string m_MemoryDeviceAddress = AppDomain.CurrentDomain.BaseDirectory + "Device.dat";
        /// <summary>
        /// HST扫描设备存放地址
        /// </summary>
        public string MemoryDeviceAddress
        {
            get
            {
                return m_MemoryDeviceAddress;
            }
        }
        /// <summary>
        /// 是否开启视频
        /// </summary>
        public bool VedioEnable = false;
        /// <summary>
        /// 是否为呼吸筛查
        /// </summary>
        public bool IsBreathOnly = false;
        /// <summary>
        /// 自动分析状态字
        /// </summary>
        public int AnalysisStateWord = 0;

        /// <summary>
        /// 临时通道参数存储
        /// </summary>
        public List<ChannelTable> CurrentSaveTable = new List<ChannelTable>();
        /// <summary>
        /// 获取通道的临时属性
        /// </summary>
        /// <param name="ID">通道信号名</param>
        /// <returns></returns>
        public ChannelTable FindChannelTable(string ID)
        {
            ChannelTable ret = Global.GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelTable.Find(t => t.ChannelID.Trim() == ID);
            return ret;
        }
        private bool m_IsRealTimeView = false;
        private object m_lockRealView = new object();     
        /// <summary>
        /// 是否加载的是实时监测界面
        /// </summary>
        public bool IsRealTimeView
        {
            set
            {
                lock (m_lockRealView)
                    m_IsRealTimeView = value;
            }
            get
            {
                return m_IsRealTimeView;
            }
        }
        private void m_ProtocolClient_RecChannelEventHandle(DataDefine Channel)
        {
            ///接收到通道
        }
        public DataTable CurrentChannelTable
        {
            set
            {
                m_DefineData = value;
            }
            get
            {
                return m_DefineData;
            }
        }
        public delegate void ConfigNameChangedDelegate(string name);
        /// <summary>
        /// 导联方案发生改变
        /// </summary>
        public event ConfigNameChangedDelegate ConfigNameChanged;
        private string m_CurrentChannelPath = "";

        public delegate void ModeTypeChangedDelegate();
        public event ModeTypeChangedDelegate ModeTypeChangedHandler;

        /// <summary>
        /// 获取或设置当前配置文件路径
        /// </summary>
        public string CurrentChannelPath
        {
            set
            {
                if (value != m_CurrentChannelPath)
                {
                    if (ConfigNameChanged != null)
                        ConfigNameChanged.Invoke(Path.GetFileName(value));
                }
                m_CurrentChannelPath = value;
            }
            get
            {
                return m_CurrentChannelPath;
            }
        }
        /// <summary>
        /// 默认通道参数
        /// </summary>
        public DataTable DefultChannelTable
        {
            get
            {
                return m_DefaultChannel;
            }
            set
            {
                m_DefaultChannel = value;
            }
        }
        /// <summary>
        /// 获取默认配置文件路径
        /// </summary>
        public string DefaultChannelPath
        {
            get
            {
                return Path.Combine( ChannelManageCloud.Default.ConfigruationBasePath ,m_defaultChanelPath);
            }
        }
        /// <summary>
        /// 自动识别事件开关
        /// </summary>
        public bool IsAutoMark { set; get; }
        /// <summary>
        ///  加载的记录是否已分析
        /// </summary>
        public bool AnlysisFinish = false;
        /// <summary>
        /// 时间基准s
        /// </summary>
        public int BaseTimeLineSpan { set; get; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { set; get; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { set; get; }
        /// <summary>
        /// 是否需要更新
        /// </summary>
        public bool ShouldRefresh { set; get; }
        /// <summary>
        /// 病人信息
        /// </summary>
        public Doc_PatientInfo Patient { set; get; }
        /// <summary>
        /// 允许展示医生信息
        /// </summary>
        public bool AllowShowDoctor { set; get; }
        /// <summary>
        /// 评分者信息
        /// </summary>
        public Doc_Doctor Doctor { set; get; }
        private Doc_ParameterSettings _ParameterSetting;
        /// <summary>
        /// 算法参数设置
        /// </summary>
        public Doc_ParameterSettings ParameterSetting
        {
            get => _ParameterSetting;
            set
            {
                _ParameterSetting = value;
            }
        }
        private Doc_SystemSetting _SystemSetting=new Doc_SystemSetting(true);
        /// <summary>
        /// 全局设置参数
        /// </summary>
        public Doc_SystemSetting SystemSetting 
        {
            get => _SystemSetting;
            set
            {
                var originalSystemSetting = _SystemSetting;
                _SystemSetting = value;
                if (originalSystemSetting != null &&
                    originalSystemSetting.ModeType != value.ModeType)
                {
                    if (ModeTypeChangedHandler != null)
                        ModeTypeChangedHandler.Invoke();
                }
            }
        }

        /// <summary>
        /// 微觉醒数据源选择字符串：其中是对应通道编码。
        /// </summary>
        public string ArousalDataSourcePlan = "1";
        /// <summary>
        /// 睡眠分期数据源选择字符串：其中是对应通道编码。
        /// </summary>
        public string SleepStageSourcePlan = "4;23;6";
        /// <summary>
        /// 氧减范围
        /// </summary>
        public string OxygenReduceRange = "3";
        /// <summary>
        /// 
        /// </summary>
        public string strRecordReportConfig = "";
        /// <summary>
        /// 获取报告结构图可变元素集合
        /// </summary>
        public List<PredefinedReportStructure> ReportStructurals { get; private set; }
        /// <summary>
        /// 获取通道配置属性列表
        /// </summary>
        public List<ChannelTable> ChannelProperties { get; private set; }
        /// <summary>
        /// 定标用的关键字
        /// </summary>
        public string MatchKey { set; get; }
        /// <summary>
        ///  解析状态字
        /// </summary>
        public AnalysisResult AnalysisReult { set; get; }
        /// <summary>
        /// 主页面表格是否需要更新
        /// </summary>
        public bool HomePageRefresh = false;
        /// <summary>
        /// 是否终止任务
        /// </summary>
        public bool FnStop { set; get; }
        /// <summary>
        /// 睡眠分期锁定使能
        /// </summary>
        public bool SleepLock { set; get; }
        /// <summary>
        /// 评分锁定
        /// </summary>
        public bool ScoreLock { set; get; }

        /// <summary>
        /// 登录账户信息
        /// </summary>
        public Doc_UsersInfo Loginer { set; get; }
        public delegate void NextStepDelegate(Doc_PatientInfo info, bool showBefore);
        private event NextStepDelegate m_NextStepHandle;
        /// <summary>
        /// 下一步时触发
        /// </summary>
        public event NextStepDelegate NextStepHandle
        {
            add
            {
                if (m_NextStepHandle != null)
                    m_NextStepHandle = null;
                m_NextStepHandle = value;
            }
            remove
            {
                if (m_NextStepHandle != null)
                    m_NextStepHandle = null;
            }
        }
        /// <summary>
        ///   新监测
        /// </summary>
        /// <param name="info"></param>
        public void NextStep(Doc_PatientInfo info,bool showBefore=false)
        {
            if(m_NextStepHandle!=null)
                m_NextStepHandle.BeginInvoke(info, showBefore,null,null);
        }
        public delegate void UpdateRecordDelegate(int id);
        private event UpdateRecordDelegate m_UpdateRecordHandle;
        /// <summary>
        /// 首页表单记录更新时触发
        /// </summary>
        public event UpdateRecordDelegate UpdateRecordHandle
        {
            add
            {
                if (m_UpdateRecordHandle != null)
                    m_UpdateRecordHandle = null;
                m_UpdateRecordHandle = value;
            }
            remove
            {
                if (m_UpdateRecordHandle != null)
                    m_UpdateRecordHandle = null;
            }
        }
        /// <summary>
        /// 首页表单记录更新时触发
        /// </summary>
        /// <param name="guid"></param>
        public void UpdateRecord(int id)
        {
            if (m_UpdateRecordHandle != null)
                m_UpdateRecordHandle.Invoke(id);
        }
        /// <summary>
        /// 开始历史数据分析委托
        /// </summary>
        /// <param name="ID">病历ID</param>
        /// <param name="date">监测日期</param>
        public delegate void StartHistroyAnalysisByEDFDelegate(string path,bool isAuto=false,string guid="");
        private event StartHistroyAnalysisByEDFDelegate m_StartHistroyAnalysisByEDFHandle;
        /// <summary>
        /// 开始历史数据分析事件
        /// </summary>
        public event StartHistroyAnalysisByEDFDelegate StartHistroyAnalysisByEDFHandle
        {
            add
            {
                if (m_StartHistroyAnalysisByEDFHandle != null)
                    m_StartHistroyAnalysisByEDFHandle = null;
                m_StartHistroyAnalysisByEDFHandle = value;
            }
            remove
            {
                if (m_StartHistroyAnalysisByEDFHandle != null)
                    m_StartHistroyAnalysisByEDFHandle = null;
            }
        }
        /// <summary>
        ///   新监测
        /// </summary>
        /// <param name="info"></param>
        public void StartHistroyAnalysisByEDF(string path,bool isauto=false,string guid="")
        {
            if (m_StartHistroyAnalysisByEDFHandle != null)
                m_StartHistroyAnalysisByEDFHandle.Invoke(path, isauto,guid);
        }
        public delegate void ChannelCreatingDelegate(CurveItem item);
        /// <summary>
        /// 通道创建时发生
        /// </summary>
        public event ChannelCreatingDelegate ChannelCreatingHandle;
        public delegate ChannelTable ChannelCloneDelegate(ChannelTable table);
        private event ChannelCloneDelegate m_ChannelCloneHandle;
        /// <summary>
        /// 通道克隆时发生
        /// </summary>
        public event ChannelCloneDelegate ChannelCloneHandle
        {
            add
            {
                if (m_ChannelCloneHandle != null)
                    m_ChannelCloneHandle = null;
                m_ChannelCloneHandle += value;
            }
            remove
            {
                m_ChannelCloneHandle = null;
            }
        }
        public delegate void ChannelChangedDelegate(bool TemporaryInvalidate=false);
        private event ChannelChangedDelegate m_ChannelChangedHandle;
        /// <summary>
        /// 通道配置改变时发生
        /// </summary>
        public event ChannelChangedDelegate ChannelChangedHandle
        {
            add
            {
                if (m_ChannelChangedHandle != null)
                {
                    Delegate[] find = m_ChannelChangedHandle.GetInvocationList();
                    foreach (Delegate gate in find)
                    {
                        if (gate.Target == value.Target)
                        {
                            return;
                        }
                    }
                    m_ChannelChangedHandle += value;
                }
                else
                {
                    m_ChannelChangedHandle += value;
                }
            }
            remove
            {
                if (m_ChannelChangedHandle != null)
                {
                    Delegate[] find = m_ChannelChangedHandle.GetInvocationList();
                    foreach (Delegate gate in find)
                    {
                        if (gate.Target == value.Target)
                        {
                            m_ChannelChangedHandle -= value;
                        }
                        break;
                    }
                }
            }
        }
        public delegate void ChangeMenuDelegate();
        private event ChangeMenuDelegate m_ChangeMenuHandle;
        /// <summary>
        /// 触发主菜单更新事件
        /// </summary>
        public event ChangeMenuDelegate ChangeMenuHandle
        {
            add
            {
                if (m_ChangeMenuHandle != null)
                    m_ChangeMenuHandle = null;
                m_ChangeMenuHandle = value;
            }
            remove
            {
                if (m_ChangeMenuHandle != null)
                    m_ChangeMenuHandle = null;
            }
        }
        /// <summary>
        /// 刷新主菜单
        /// </summary>
        public void RefreshHomeMenu()
        {
            if (m_ChangeMenuHandle != null)
            {
                m_ChangeMenuHandle.BeginInvoke(null, null);
            }
        }
        /// <summary>
        /// 標籤的顏色發生改變
        /// </summary>
        public delegate void MarkColorChangedDelegate(IMarker marker);
        private event MarkColorChangedDelegate m_MarkColorChangedHandle;
        public event MarkColorChangedDelegate MarkColorChangedHandle
        {
            add
            {
                if (m_MarkColorChangedHandle != null)
                    m_MarkColorChangedHandle = null;
                m_MarkColorChangedHandle = value;
            }
            remove
            {
                if (m_MarkColorChangedHandle != null)
                    m_MarkColorChangedHandle = null;
            }
        }
        /// <summary>
        /// 颜色发生该变
        /// </summary>
        /// <param name="marker"></param>
        public void MarkColorChange(IMarker marker)
        {
            if (m_MarkColorChangedHandle != null)
                m_MarkColorChangedHandle.Invoke(marker);
        }
        /// <summary>
        /// 标记信息更新
        /// </summary>
        public delegate void DefineMarksChangeDelegate();
        private event DefineMarksChangeDelegate m_DefineMarksChangeHandle;
        public event DefineMarksChangeDelegate DefineMarksChangeHandle
        {
            add
            {
                if (m_DefineMarksChangeHandle != null)
                    m_DefineMarksChangeHandle = null;
                m_DefineMarksChangeHandle = value;
            }
            remove
            {
                if (m_DefineMarksChangeHandle != null)
                    m_DefineMarksChangeHandle = null;
            }
        }
        /// <summary>
        /// 颜色发生该变
        /// </summary>
        /// <param name="marker"></param>
        public void DefineMarksChange()
        {
            if (m_DefineMarksChangeHandle != null)
                m_DefineMarksChangeHandle.Invoke();
        }
        /// <summary>
        /// 通道配置修改时发生
        /// </summary>
        public void ChannelChanged(bool TemporaryInvalidate=false)
        {
            ShouldRefresh = !TemporaryInvalidate;
            if (m_ChannelChangedHandle != null)
                m_ChannelChangedHandle.BeginInvoke(TemporaryInvalidate, null, null);
        }
        /// <summary>
        /// 0-定标开始 1-定标成功 2-定标失败
        /// </summary>
        /// <param name="record"></param>
        /// <param name="typ"></param>
        public delegate void CalibrationChangedView(Doc_CalibrationRecord record, int typ);
        public event CalibrationChangedView CalibrationChangedViewHandle;

        public void calibrationChangedView(Doc_CalibrationRecord record, int typ)
        {
            if (CalibrationChangedViewHandle != null)
            {
                CalibrationChangedViewHandle.Invoke(record, typ);
            }
        }
        private int _sn = 1;
        /// <summary>
        /// 创建订单号
        /// </summary>
        /// <returns></returns>
        public string CreatGUID(int PatientID, int DoctorID)
        {
            if (_sn == 1000)
                _sn = 1;
            else
                _sn++;
            return string.Format("PS{0}{1}{2}{3:yyyMMddHHmmss}{4}", Loginer.ID, DoctorID, PatientID, DateTime.Now, _sn.ToString().PadLeft(3, '0'));
        }
          
        /// <summary>
        /// 只更新界面
        /// </summary>
        public void reFreshView()
        {
            if (m_ChannelChangedHandle != null)
                m_ChannelChangedHandle.BeginInvoke(false, null, null);
        }

        /// <summary>
        /// 根据配置文件创建通道
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataTable CreatChannels(List<ChannelTable> channels)
        {
            DataTable ret = ChannelManageCloud.Default.CreatEmptyTable();
            int m_channelCnt = 0;
            foreach (ChannelTable channel in channels)
            {
                if (!channel.Enable)
                    continue;
                ChannelTable table = new ChannelTable()
                {
                    MaxValue = channel.ADMaxValue,
                    MinValue = channel.ADMinValue,
                    State = true,
                    SpanTime = channel.SpanTime,
                    Index = m_channelCnt,
                    Sensitivity = channel.PixelEnable ? 1 : 0,
                    HighPass = "Off",
                    LowPass = "Off",
                    SingleNotch = "Off",
                    ColorSelect = channel.ColorSelect,
                    strName = channel.strName,
                    ID = channel.ID,
                    PixelEnable = channel.PixelEnable,
                    CalibrationsVisible = channel.CalibrationsVisible,
                    IsShowValue = channel.IsShowValue,
                    Antipole = channel.Antipole,
                };
                ret.Rows.Add(ChannelTable.ConvertToDataRow(table, ret));
                if (ChannelCreatingHandle != null)
                    ChannelCreatingHandle.Invoke( CreatChannel(table));
                 m_channelCnt++;
            }
            return ret;
        }
        /// <summary>
        /// 创建一个模板导联
        /// </summary>
        /// <param name="channels"></param>
        /// <returns></returns>
        public DataTable ConvertModleTable(List<Doc_Channel> channels)
        {
            DataTable dt = ChannelManageCloud.Default.CreatEmptyTable();
            channels = channels.OrderBy(t => t.IndexInGroup).ToList();
            for (int i = 0; i < channels.Count; i++)
            {
                Doc_Channel channel = channels[i];
                dt.Rows.Add(channel.Enable, channel.Description, channel.SpanTime, channel.ADMaxValue, channel.ADMinValue, i, channel.Name, "Off", 1, "Off", "Off", "Off", !channel.IsShowValue, channel.IsShowValue, channel.CalibrationsVisible, channel.AntipoleEnable, channel.Color, "0:30");
            }
            return dt;
        }
        private bool m_shouldFilterReset = false;
        private string m_gobalSingleNotch = "OFF";
        /// <summary>
        ///  全局陷波器类型
        /// </summary>
        public string GlobalSingleNotch
        {
            set
            {
                if (value != "OFF" && value != m_gobalSingleNotch)
                    m_shouldFilterReset = true;
                m_gobalSingleNotch = value;
            }
            get
            {
                return m_gobalSingleNotch;
            }
        }
        private string m_gobalHighPass = "OFF";
        /// <summary>
        ///  全局高通滤波器类型
        /// </summary>
        public string GlobalHighPass
        {
            set
            {
                if (value != "OFF" && value != m_gobalHighPass)
                    m_shouldFilterReset = true;
                m_gobalHighPass = value;
            }
            get
            {
                return m_gobalHighPass;
            }
        }
        private string m_gobalLowPass = "OFF";
        /// <summary>
        ///  全局低通滤波器类型
        /// </summary>
        public string GlobalLowPass
        {
            set
            {
                if (value != "OFF" && value != m_gobalLowPass)
                    m_shouldFilterReset = true;
                m_gobalLowPass = value;
            }
            get
            {
                return m_gobalLowPass;
            }
        }
        /// <summary>
        /// 是否全局相应虑波
        /// </summary>
        public bool IsGlobalFiliterAction
        {
            get
            {
                if (m_gobalSingleNotch != "OFF"||m_gobalHighPass!="OFF"||m_gobalLowPass!="OFF")
                    return true;
                else
                    return false;
            }
        }
        private object m_lockObj = new object();
        /// <summary>
        /// 过滤器处理
        /// </summary>
        /// <param name="item"></param>
        /// <param name="datasource"></param>
        /// <param name="filiter"></param>
        /// <returns></returns>
        private float[] item_FiliterDataEvent(pChart.CurveItem item, float[] datasource, int firstDataIndex,object filiter)
        {
            ChannelFiliter tag = (ChannelFiliter)filiter;
            bool _IsGlobalFiliterAction = false;
            lock (m_lockObj)
            {
                if (item.SubsectionzFlite || m_shouldFilterReset)
                {
                    m_shouldFilterReset = false;
                    tag.Reset();
                }
                _IsGlobalFiliterAction = IsGlobalFiliterAction;
            }
            int span = getSampleSpan(item.ChannelNum);
            if (!_IsGlobalFiliterAction)
            {
                RateValue find = findRV(item.HighPass, span, FilterType.HighPass, tag);
                if (find != null)
                {
                    datasource = tag.Filiter(datasource,firstDataIndex, find);
                }
                find = findRV(item.LowPass, span, FilterType.LowPass, tag);
                if (find != null)
                {
                    datasource = tag.Filiter(datasource, firstDataIndex, find);
                }
                find = findRV(item.SingleNotch, span, FilterType.SingleNotch, tag);
                if (find != null)
                {
                    datasource = tag.Filiter(datasource, firstDataIndex, find);
                }
                return datasource;
            }
            else
            {
                RateValue find = findRV(m_gobalHighPass, span, FilterType.HighPass, tag);
                if (find != null)
                {
                    datasource = tag.Filiter(datasource, firstDataIndex, find);
                }
                find = findRV(m_gobalLowPass, span, FilterType.LowPass, tag);
                if (find != null)
                {
                    datasource = tag.Filiter(datasource, firstDataIndex, find);
                }
                find = findRV(m_gobalSingleNotch, span, FilterType.SingleNotch, tag);
                if (find != null)
                {
                    datasource = tag.Filiter(datasource, firstDataIndex, find);
                }
                return datasource;
            }
        }
        private object m_lockFilter = new object();
        private RateValue findRV(string name, int span, FilterType typ, ChannelFiliter channel)
        {
            return channel.ChannelFiltersList.Find(t => t.Name.ToLower() == name.ToLower() && t.FilterTyp == typ && t.SampleSpan == span);
        }

        /// <summary>
        /// 创建一个通道单元
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public CurveItem CreatChannel(DataRow dr)
        {
            return CreatChannel(ChannelTable.ConvertToChannel(dr));
        }
        /// <summary>
        /// 创建一个通道单元
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public CurveItem CreatChannel(ChannelTable table)
        {
            if (table.Enable)
            {
                CurveItem item = new CurveItem();
                item.AutoCalibrationsEnable = false;
                item.Visible = table.State;
                item.yAxis.MaxValue = table.MaxValue;
                item.yAxis.MinValue = table.MinValue;
                item.TimeSpan = 1000 / table.SpanTime;
                item.ChannelNo = table.Index;
                item.PenColor = table.ColorSelect;
                item.Name = table.strName;
                item.SingleNotch = table.SingleNotch;
                item.HighPass = table.HighPass;
                item.LowPass = table.LowPass;
                item.ID = table.ChannelID;
                item.Antipole = table.Antipole;
                item.ChannelNum = table.ID;
                item.PixelRate = table.Sensitivity;
                item.belong = Convert.ToInt32(table.Reserve.Split(':')[0]);
                item.IsShowValue = table.IsShowValue;
                item.IsCloneItem = table.IsClone;
                if (table.CalibrationsVisible)
                {
                    item.yAxis.LegendLables.Clear();
                    item.yAxis.LegendLables.Add(item.yAxis.MinValue.ToString());
                    item.yAxis.LegendLables.Add(item.yAxis.MaxValue.ToString());
                    item.yAxis.CalibrationsVisible = true;
                }
                item.ValueZoomRate = 1;
                item.FiliterDataEvent += item_FiliterDataEvent;
                item.Tag = m_ChannelFiliter.CreatInstance(table.SpanTime);
                return item;

            }
            return null;
        }
        /// <summary>
        /// 根据通道ID与滤波器类型枚举出滤波器分类
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="typ"></param>
        /// <returns></returns>
        public string[] channel_FilterDropDownHandle(int ID, string typ)
        {
            string[] ret = new string[0];
            FilterType ft = typ == "SingleNotch" ? FilterType.SingleNotch : typ == "HighPass" ? FilterType.HighPass : typ == "LowPass" ? FilterType.LowPass : FilterType.None;
            List<RateValue> find = m_ChannelFiliter.ChannelFiltersList.FindAll(t => t.FilterTyp == ft && t.SampleSpan == getSampleSpan(ID));
            if (find != null)
            {
                var ss = find.Select(t => t.Name);
                List<float> str1 = new List<float>();
                List<float> str2 = new List<float>();
                ret=new string[ss.Count()];
                foreach (string s in ss)
                {
                    string st1 = s.ToLower();
                    if (st1.Contains("s"))
                    {
                        str1.Add(float.Parse(st1.Replace("s", "")));
                    }
                    else
                    {
                        str2.Add(float.Parse(st1.Replace("hz", "")));
                    }
                }
                if (str1.Count > 0)
                {
                    str1.Sort();
                    for (int s = 0; s < str1.Count; s++)
                    {
                        ret[s] = string.Format("{0}S", str1[s]);
                    }
                }
                if (str2.Count > 0)
                {
                    str2.Sort();
                    for (int s = 0; s < str2.Count; s++)
                    {
                        ret[s+str1.Count] = string.Format("{0}Hz", str2[s]);
                    }
                }
            }
            return ret;
        }

        public string[] Channel_FilterLoad(string typ)
        {
            string[] ret = new string[0];
            FilterType ft = typ == "SingleNotch" ? FilterType.SingleNotch : typ == "HighPass" ? FilterType.HighPass : typ == "LowPass" ? FilterType.LowPass : FilterType.None;
            List<RateValue> find = m_ChannelFiliter.ChannelFiltersList.FindAll(t => t.FilterTyp == ft);
            if (find != null)
            {
                var ss = find.Select(t => t.Name);
                List<float> str1 = new List<float>();
                List<float> str2 = new List<float>();
                foreach (string s in ss)
                {
                    string st1 = s.ToLower();
                    if (st1.Contains("s"))
                    {
                        float value = float.Parse(st1.Replace("s", ""));
                        if (!str1.Contains(value))
                            str1.Add(value);
                    }
                    else
                    {
                        float value = float.Parse(st1.Replace("hz", ""));
                        if (!str2.Contains(value))
                            str2.Add(value);
                    }
                }
                ret = new string[str1.Count + str2.Count];
                if (str1.Count > 0)
                {
                    str1.Sort();
                    for (int s = 0; s < str1.Count; s++)
                    {
                        ret[s] = string.Format("{0}S", str1[s]);
                    }
                }
                if (str2.Count > 0)
                {
                    str2.Sort();
                    for (int s = 0; s < str2.Count; s++)
                    {
                        ret[s + str1.Count] = string.Format("{0}Hz", str2[s]);
                    }
                }
            }
            return ret;
        }
        /// <summary>
        /// 获取滤波器的采样频率
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private int getSampleSpan(int ID)
        {
            int span = 0;
            ChannelTable find = Global.GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelTable.Find(t => t.ID == ID);
            if (find != null)
                span = find.SpanTime;
            return span;
        }
        /// <summary>
        /// 生成谱图
        /// </summary>
        /// <returns></returns>
        public Image CreatMap(ResultDomain result = null, bool breathOnly = false)
        {
            if (result == null)
                result = Channel.Default.AnalysisReult.ConvertResultDomain(0);
            ChartMap map = new ChartMap();
            map.Width = 670;
            map.StartTime = result.StartTime;
            TimeSpan span = result.EndTime - result.StartTime;
            int hours = (int)span.TotalHours;
            hours = hours < span.TotalHours ? hours + 1 : hours;
            map.xAxis.MaxValue = (int)(span.TotalHours * 60 * 2);
            map.xAxis.Interval = hours;
            map.xAxis.Calibrations.Clear();
            for (int i = 0; i < hours; i++)
            {
                map.xAxis.Calibrations.Add(120 * i);
            }
            map.xAxis.Calibrations.Add(map.xAxis.MaxValue);
            int yoffset = map.Docksize;
            #region 新方式
            var reportmaps = ReportStructurals.Where(t=>t.Visible);
            if (reportmaps.Count() > 0)
            {
                reportmaps = reportmaps.OrderBy(t => t.Index);
                PropertyInfo[] ms = result.GetType().GetProperties();
                foreach (PredefinedReportStructure report in reportmaps)
                {
                    if (report.Id == 9 && breathOnly)///ID为9是睡眠分期结构图
                    {
                        continue;
                    }
                    Area area = new Area();
                    area.ID = report.Name;
                    area.Name = report.Name;
                    area.Description = report.Description;
                    area.Location = new Point(map.Docksize, yoffset);
                    area.Hight = (int)report.Height;
                    area.PenColor = Color.FromName(report.PenColor);
                    PropertyInfo ss = ms.First(t => t.Name == report.DataSourceName);
                    if (ss != null)
                    {
                        try
                        {
                            if (ss.PropertyType == typeof(SuperPointF[]))
                            {
                                area.Points = ((SuperPointF[])Convert.ChangeType(ss.GetValue(result), ss.PropertyType)).ToList();
                            }
                        }
                        catch
                        {

                        }
                    }
                    area.yAxis.Interval = report.Interval;
                    area.yAxis.SetMaxMinValue(report.MaxValue, report.MinValue);
                    if (report.LegendLables != "")
                    {
                        area.yAxis.LegendLables = report.LegendLables.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToList();
                        area.yAxis.CalibrationsColors = report.CalibrationsColors.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(t => Color.FromName(t.Trim())).ToList();
                    }
                    else
                        area.yAxis.LegendLables = area.yAxis.Calibrations.Select(t => t.ToString()).ToList();
                    area.SavePath = report.SavePath;
                    area.ShowTimeLables = report.ShowTimeLables;
                    yoffset += (int)area.Hight;
                    map.ItemAreas.Add(area);
                }
            }
            #endregion
            map.Hight = yoffset + map.Docksize;
            Image ret = map.GetMap();
            for (int i = 0; i < map.ItemAreas.Count; i++)
            {
                if (!Directory.Exists(map.ItemAreas[i].SavePath))
                    Directory.CreateDirectory(Path.GetDirectoryName(map.ItemAreas[i].SavePath));
                map.ItemAreas[i].Width = map.Width;
                map.ItemAreas[i].Location = new Point(30, 0);
                map.ItemAreas[i].GetMap().Save(map.ItemAreas[i].SavePath);
            }
            string[] sleepMapList = new string[] { Patient.FirstSleepPhotoPath , Patient.SecondSleepPhotoPath , Patient.ThirdSleepPhotoPath , Patient.FourthSleepPhotoPath , Patient.FifthSleepPhotoPath };
            for(int i = 0; i < sleepMapList.Length; i++)
            {
                if (File.Exists(sleepMapList[i]))
                    File.Delete(sleepMapList[i]);
            }
            for (int i = 0; i < result.MultSleepRecords.Count; i++)
            {
                if (i < sleepMapList.Length)
                {
                    result.MultSleepRecords = result.MultSleepRecords.OrderBy(t => t.StartIndex).ToList();
                    int startidx = (result.MultSleepRecords[i].StartIndex - result.OffSetFrameCnt + 1);
                    int endidx = (result.MultSleepRecords[i].EndIndex - result.OffSetFrameCnt + 1);
                    ///SleepStagPoints中的xvalue是当前睡眠帧索引减去了偏移索引即（偏移帧数-1），所以这里需要在判断的时候算上偏移
                    try
                    {
                        List<SuperPointF> points = result.SleepStagPoints.Where(t => (t.XValue >= startidx) && (t.XValue < endidx)).ToList();
                        if (points.Count < 1)
                            continue;
                        Area area = new Area();
                        area.Location = new Point(20, 0);
                        area.Top = 10;
                        area.Name = "";
                        area.Hight = 90;
                        area.Width = 200;
                        area.xAxis.Displacement = area.Width - area.Location.X;
                        area.xAxis.Interval = 3;
                        area.xAxis.SetMaxMinValue(endidx, area.xAxis.MinValue = startidx);
                        area.PenColor = Color.SlateGray;
                        area.Points = points;
                        area.yAxis.Interval = 4;
                        area.yAxis.MaxValue = 5;
                        area.yAxis.MinValue = 1;
                        area.ShowTimeLables = true;
                        area.yAxis.LegendLables = new List<string>() { "N3", "N2", "N1", "R", "W" };
                        area.yAxis.CalibrationsColors = new List<Color>() { Color.DarkCyan, Color.LightGreen, Color.GreenYellow, Color.Red, Color.DimGray };
                        string[] times = new string[]
                        {
                    (StartTime.AddMinutes(result.MultSleepRecords[i].StartIndex*0.5f)).ToString("HH:mm"),
                    (StartTime.AddMinutes((result.MultSleepRecords[i].EndIndex / 2 + result.MultSleepRecords[i].StartIndex / 2)*0.5f)).ToString("HH:mm"),
                    (StartTime.AddMinutes(result.MultSleepRecords[i].EndIndex*0.5f)).ToString("HH:mm")
                        };
                        PointF[] locations = new PointF[]
                        {
                    new PointF(area.Location.X,20),
                    new PointF(area.Width/2,20),
                    new PointF(area.Width-20,20),
                        };
                        area.SetTimeLabels(times, locations);
                        area.GetMap().Save(sleepMapList[i]);
                    }
                    catch (Exception ee)
                    {
                        Global.GlobalSingleton.Instance.LogInstance.AddLog(ee.StackTrace, pSystem.LogManagement.LogLevel.ERROR);
                    }
                }
            }
            return ret;
        }

        /// <summary> 
        /// 大文件移动
        /// </summary>
        /// <param name="source">Source file path</param> 
        /// <param name="destination">Destination file path</param> 
        public bool FMove(string source, string destination)
        {
            FnStop = false;
            int array_length = (int)Math.Pow(2, 19);
            byte[] dataArray = new byte[array_length];
            using (FileStream fsread = new FileStream
            (source, FileMode.Open, FileAccess.Read, FileShare.None, array_length))
            {
                using (BinaryReader bwread = new BinaryReader(fsread))
                {
                    using (FileStream fswrite = new FileStream
                    (destination, FileMode.Create, FileAccess.Write, FileShare.None, array_length))
                    {
                        using (BinaryWriter bwwrite = new BinaryWriter(fswrite))
                        {
                            while (!FnStop)
                            {
                                int read = bwread.Read(dataArray, 0, array_length);
                                if (0 == read)
                                    break;
                                bwwrite.Write(dataArray, 0, read);
                            }
                        }
                    }
                }
            }
            if (FnStop)
            {
                File.Delete(destination);
                return false;
            }
            else
                File.Delete(source);
            return true;
        }
    }
}
