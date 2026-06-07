using AwareTec.Polysmith.Cloud.UI.DataModels;
using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.SpecificXmlHelper;
using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.User;
using AwareTec.Polysmith.Cloud.UI.DataModels.EnumModels;
using AwareTec.Polysmith.Cloud.UI.DataModels.Global;
using AwareTec.Polysmith.Cloud.UI.ViewHelpers;
using AwareTec.Polysmith.DataBaseCom;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.Cloud.UI.Forms.MainFormBlock
{
    public partial class NewDevice : CloudSkinForm
    {
        #region 私有变量
        private Views.ViewsEventArgs.SwitchPageEventArgs m_dataMoudle = null;
        private UserOperationConfig m_userOperationConfig = null;
        private DataTable m_ChannelData = null;
        private Doc_PatientInfo m_patientinfo = null;
        private bool m_showBefore = false;
        /// <summary>
        /// 窗体是否已加载
        /// </summary>
        private bool m_init = false;
        /// <summary>
        /// 下拉后是否自动改变过
        /// </summary>
        private bool m_autochange = false;
        /// <summary>
        /// 选择通道配置文件路径
        /// </summary>
        private string m_ChannelConfigPath = "";
        /// <summary>
        /// 选择模式
        /// </summary>
        private ScanMethod bak_localScan =ScanMethod.Empty;

        #endregion

        #region 公有变量

        #endregion

        #region 事件委托

        public delegate bool StartMonitorDelegate(IDevice client, DataTable channelconfig, Views.ViewsEventArgs.SwitchPageEventArgs e);
        /// <summary>
        /// 确认开始监控时发生
        /// </summary>
        public event StartMonitorDelegate StartMonitorHandler;

        #endregion

        #region 初始化 load/dispose/init

        /// <summary>
        /// 初始化
        /// </summary>
        public NewDevice()
        {
            InitializeComponent();
            this.Load += NewDevice_Load;
            this.Disposed += NewDevice_Disposed;
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewDevice_Load(object sender, EventArgs e)
        {
            tipinfolabel.Text = m_dataMoudle.OrderItem.examType == RestfulWebRequest.EnumModels.EnumModels4Table.ExamType.MultiLead ? "*只显示多导设备" : "";
            this.LocalScanRadioButton.CheckedChanged += this.LocalScanRadioButton_CheckedChanged;
            this.RemoteScanRadioButton.CheckedChanged += RemoteScanRadioButton_CheckedChanged;
            this.DeviceNameComboBox.DropDown += this.DeviceNameComboBox_DropDown;
            this.DeviceNameComboBox.SelectedValueChanged += this.DeviceNameComboBox_SelectedValueChanged;
            this.MontageThemeComboBox.DropDown += this.MontageThemeComboBox_DropDown;
            this.MontageThemeComboBox.SelectedValueChanged += this.MontageThemeComboBox_SelectedValueChanged;
            this.EnterMonitoringModeButton.Click += this.EnterMonitoringModeButton_Click;
            this.CancelButton.Click += this.CancelButton_Click;
            m_userOperationConfig = GlobalSingleton.Instance.User.UserConfig;
            #region 加载患者信息
            PatientNoTextBox.Text = m_patientinfo.PatientNo;
            PatientNameTextBox.Text = m_patientinfo.PatientName;
            PatientAgeTextBox.Text = m_patientinfo.Age.ToString();
            genderMaleRadioButton.Checked = m_patientinfo.Gender.Equals("男");
            genderFemaleRadioButton.Checked = m_patientinfo.Gender.Equals("女");

            #endregion

            #region 加载导联信息
            string filename = m_userOperationConfig.LastSelectedScheme;
            string path = Path.Combine(ChannelManageCloud.Default.ConfigruationBasePath, filename);
            if (File.Exists(path))
            {
                MontageThemeComboBox.Text = filename;
                MontageThemeComboBox.Items.Add(filename);
                MontageThemeComboBox.SelectedItem = filename;
                m_ChannelConfigPath = path;
            }
            #endregion

            #region 加载硬件设备信息
            string devName = m_userOperationConfig.LastBoundDevice;
            LocalScanRadioButton.Checked = m_userOperationConfig.TypeOfLastScan == ScanMethod.Local;
            RemoteScanRadioButton.Checked = !LocalScanRadioButton.Checked;
            List<IDevice> devs = LocalScanRadioButton.Checked ? DeviceOnLine.Default.GetDevices() : DeviceOnWLan.Default.getDevices();
            IDevice find = devs.Find(t => t.DeviceName == devName);
            if (find != null)
            {
                DeviceNameComboBox.Items.Add(find.DeviceName);
                DeviceNameComboBox.Text = find.DeviceName;
            }
            #endregion
            bak_localScan = m_userOperationConfig.TypeOfLastScan;
            m_init = true;
        }

        /// <summary>
        /// 销毁此窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewDevice_Disposed(object sender, EventArgs e)
        {
            if (bak_localScan != m_userOperationConfig.TypeOfLastScan)
            {
                if (m_userOperationConfig.TypeOfLastScan==ScanMethod.Local)
                {
                    DeviceOnWLan.Default.Start = false;
                    DeviceOnLine.Default.Start = true;
                }
                else
                {
                    DeviceOnLine.Default.Start = false;
                    DeviceOnWLan.Default.Start = true;
                }
            }
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 添加端口号的打印信息
        /// </summary>
        /// <param name="obj"></param>
        private void port_AddLog(pSystem.LogManagement.LogItem obj)
        {
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(obj.Description,pSystem.LogManagement.LogLevel.DEBUG);
        }
        /// <summary>
        /// 本地单选框选中状态改变时的绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LocalScanRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            ScanWay_CheckedChanged();
        }

        /// <summary>
        /// 远程单选框选中状态改变时的绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoteScanRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            ScanWay_CheckedChanged();
        }

        /// <summary>
        /// 单选框选中状态改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScanWay_CheckedChanged()
        {
            DeviceNameComboBox.Text = string.Empty;
            DeviceNameComboBox.Items.Clear();
            if (m_init)
            {
                if (LocalScanRadioButton.Checked)
                {
                    DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("用户选择 设备连接为 本地");
                    DeviceOnWLan.Default.Start = false;
                    DeviceOnLine.Default.Start = true;
                    bak_localScan = ScanMethod.Local;
                }
                else
                {
                    DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("用户选择 设备连接为 远程");
                    DeviceOnLine.Default.Start = false;
                    DeviceOnWLan.Default.Start = true;
                    bak_localScan = ScanMethod.Remote;
                }
            }
        }

        /// <summary>
        /// 设备名下拉框的下拉事件绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeviceNameComboBox_DropDown(object sender, EventArgs e)
        {
            DeviceNameComboBox.Items.Clear();
            List<IDevice> finddevices = new List<IDevice>();
            if (LocalScanRadioButton.Checked)
            {
                finddevices.AddRange(DeviceOnLine.Default.GetDevices());
            }
            else
            {
                finddevices.AddRange(DeviceOnWLan.Default.getDevices());
            }
            for (int i = 0; i < finddevices.Count; i++)
            {
                string devicename = finddevices[i].Description;
                if (m_dataMoudle.OrderItem.examType == RestfulWebRequest.EnumModels.EnumModels4Table.ExamType.MultiLead && devicename.Contains("iRem-A"))
                {
                    DeviceNameComboBox.Items.Add(devicename);
                }
                if (m_dataMoudle.OrderItem.examType == RestfulWebRequest.EnumModels.EnumModels4Table.ExamType.PrimaryScreeningTest && devicename.Contains("iRem-"))
                {
                    DeviceNameComboBox.Items.Add(devicename);
                }
                //DeviceNameComboBox.Items.Add(finddevices[i].Description);
            }
            DeviceNameComboBox.Tag = finddevices;
            m_autochange = false;
        }

        /// <summary>
        /// 设备名值改变时事件的绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeviceNameComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (m_init)
            {
                if (!m_autochange && DeviceNameComboBox.SelectedIndex >= 0)
                {
                    string str = DeviceNameComboBox.SelectedItem.ToString();
                    string[] ss = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string txt = str.Replace(string.Format("{0} ", ss[0]), "");
                    m_autochange = true;
                    DeviceNameComboBox.Items[DeviceNameComboBox.SelectedIndex] = txt;
                    DeviceNameComboBox.SelectedItem = txt;
                    DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("设备下拉框 用户选择设备名称{0}", txt), pSystem.LogManagement.LogLevel.WARN);
                }
            }
        }

        /// <summary>
        /// 导联方案下拉框的下拉事件绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MontageThemeComboBox_DropDown(object sender, EventArgs e)
        {
            if (Directory.Exists(ChannelManageCloud.Default.ConfigruationBasePath))
            {
                MontageThemeComboBox.Items.Clear();
                string path = ChannelManageCloud.Default.ConfigruationBasePath;
                int len = path.Length + 1;
                string[] names = Directory.GetFiles(path, "*.cfg");
                if (names.Length > 0)
                {
                    MontageThemeComboBox.Items.AddRange(names.Select(t => t.Remove(0, len)).ToArray());
                }
            }
        }

        /// <summary>
        /// 导联方案 值改变时事件的绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MontageThemeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (m_init)
                m_ChannelConfigPath = Path.Combine(ChannelManageCloud.Default.ConfigruationBasePath, MontageThemeComboBox.SelectedItem.ToString());
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("用户选择导联方案为 {0}", MontageThemeComboBox.SelectedItem.ToString()), pSystem.LogManagement.LogLevel.INFO);
        }

        #endregion

        #region 按钮方法

        /// <summary>
        /// 进入PSG监测模式按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterMonitoringModeButton_Click(object sender, EventArgs e)
        {
            #region 用户输入检查
            if (string.IsNullOrWhiteSpace(MontageThemeComboBox.Text) ||
                MontageThemeComboBox.SelectedItem == null)
            //if (m_ChannelConfigPath == "")
            {
                AhDung.MessageTip.ShowWarning("请选择通道配置方案!");
                return;
            }
            string devName = DeviceNameComboBox.Text.Trim();
            if (devName == "")
            {
                AhDung.MessageTip.ShowWarning("当前没有选用有效设备!");
                return;
            }
            #endregion
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("点击 进入PSG监测模式 按钮");
            Channel.Default.Patient = m_patientinfo;
            m_ChannelData = ChannelManageCloud.Default.ReadChannelConfig(m_ChannelConfigPath);
            Channel.Default.CurrentChannelPath = m_ChannelConfigPath;
            if (DeviceNameComboBox.SelectedItem != null && MontageThemeComboBox.SelectedItem != null)
            {
                m_userOperationConfig.LastBoundDevice = DeviceNameComboBox.SelectedItem.ToString();
                m_userOperationConfig.LastSelectedScheme = MontageThemeComboBox.SelectedItem.ToString();
                m_userOperationConfig.TypeOfLastScan = LocalScanRadioButton.Checked ? ScanMethod.Local : ScanMethod.Remote;
                bak_localScan = m_userOperationConfig.TypeOfLastScan;
                (new UserOperationConfigXmlHelper(GlobalSingleton.Instance.User.ConfigPath)).Modify(m_userOperationConfig);

            }
            if (m_ChannelData.Rows.Count > 0)
            {
                IDevice m_selectDev = (DeviceNameComboBox.Tag as List<IDevice>).Find(t => t.DeviceName.Trim() == devName.Trim());
                if (m_selectDev == null)
                {
                    AhDung.MessageTip.ShowWarning("无效设备名，请重新选择");
                    return;
                }
                if (m_selectDev.MatchKey == "")
                {
                    AhDung.MessageTip.ShowWarning("等待驱动安装完成...");
                    return;
                }
                if (m_selectDev.MatchKey.Contains(":"))
                {
                    DeviceOnWLan.Default.CurrentDevice = m_selectDev;
                }
                else
                {
                    if (!m_selectDev.DeviceName.Contains("iRem"))//如果下拉框选择的设备此时已被扫描到，选择扫描的com口
                    {
                        BthCommans device = m_selectDev as BthCommans;
                        IOClient port = new MySerialPort(m_selectDev.MatchKey)
                        {
                            BaudRate = 460800
                        };
                        port.Delay = 10;
                        port.AddLog += port_AddLog;
                        m_selectDev.PortClient = port;
                        m_selectDev.DeviceAddr = device.ID;
                    }
                    else//选择原来设备的com口
                    {
                        DeviceOnLine.Default.UpdateDevices(m_selectDev.MatchKey, DeviceState.Running);
                        System.Threading.Thread.Sleep(100);
                        IOClient port = new MySerialPort(m_selectDev.MatchKey) { BaudRate=115200};
                        port.Delay = 10;
                        port.AddLog += port_AddLog;
                        m_selectDev.PortClient = port;
                    }
                }
                if (StartMonitorHandler != null)
                {
                     m_patientinfo.RecordTime = DateTime.Now;
                    if (StartMonitorHandler.Invoke(m_selectDev, m_ChannelData,m_dataMoudle))
                    {
                        this.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 取消按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 公有方法
        /// <summary>
        /// 初始化信息
        /// </summary>
        /// <param name="info"></param>
        public void Init(Views.ViewsEventArgs.SwitchPageEventArgs e)
        {
            m_dataMoudle = e;
            m_patientinfo = GlobalSingleton.Instance.convetToPatient(e.OrderItem.patient);
        }
        #endregion
    }
}
