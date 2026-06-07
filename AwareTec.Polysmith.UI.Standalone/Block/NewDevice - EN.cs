using AwareTec.Polysmith.DataBaseCom;
using AwareTec.Polysmith.UI.DataModel;
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

namespace AwareTec.Polysmith.UI.Block
{
    public partial class NewDevice : SkinForm
    {
        #region 全局变量

        Configuration m_appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
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
        private bool bak_localScan = false;
        #endregion

        #region 事件与委托的定义
        public delegate bool StartMonitorDelegate(IDevice client, DataTable channelconfig);
        /// <summary>
        /// 确认开始监控时发生
        /// </summary>
        public event StartMonitorDelegate StartMonitorHandler;
        #endregion

        #region 初始化相关
        /// <summary>
        /// 构造函数
        /// </summary>
        public NewDevice()
        {
            InitializeComponent();

            #region 为防止designer随意更改 解绑事件，因此事件绑定一律放置于逻辑代码的构造函数中
            this.Load += NewDevice_Load;
            this.Disposed += NewDevice_Disposed;

            this.LocalScanRadioButton.CheckedChanged += this.LocalScanRadioButton_CheckedChanged;
            this.RemoteScanRadioButton.CheckedChanged += RemoteScanRadioButton_CheckedChanged;
            this.DeviceNameComboBox.DropDown += this.DeviceNameComboBox_DropDown;
            this.DeviceNameComboBox.SelectedValueChanged += this.DeviceNameComboBox_SelectedValueChanged;
            this.EditLabel.Click += this.EditLabel_Click;
            this.MontageThemeComboBox.DropDown += this.MontageThemeComboBox_DropDown;
            this.MontageThemeComboBox.SelectedValueChanged += this.MontageThemeComboBox_SelectedValueChanged;
            this.MoreInfoLabel.Click += this.MoreInfoLabel_Click;
            this.PreviousStepButton.Click += this.PreviousStepButton_Click;
            this.EnterMonitoringModeButton.Click += this.EnterMonitoringModeButton_Click;
            this.CancelButton.Click += this.CancelButton_Click;
            #endregion
        }

        /// <summary>
        /// 初始化信息
        /// </summary>
        /// <param name="info"></param>
        public void Init(Doc_PatientInfo info, bool showBefore = false)
        {
            m_patientinfo = info;
            m_showBefore = showBefore;
        }


        /// <summary>
        /// 加载窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewDevice_Load(object sender, EventArgs e)
        {
            #region 加载患者信息
            PatientNoTextBox.Text = m_patientinfo.PatientNo;
            PatientNameTextBox.Text = m_patientinfo.PatientName;
            PatientAgeTextBox.Text = m_patientinfo.Age.ToString();
            genderMaleRadioButton.Checked = m_patientinfo.Gender.Equals(Program.Language == "EN" ? "Male" : "男");
            genderFemaleRadioButton.Checked = m_patientinfo.Gender.Equals(Program.Language == "EN" ? "Female" : "女");

            #endregion

            #region 加载导联信息
            string filename = ConfigurationManager.AppSettings["MontageName"];
            string path = Path.Combine(ChannelManage.Default.ConfigruationBasePath, filename);
            if (File.Exists(path))
            {
                MontageThemeComboBox.Text = filename;
                MontageThemeComboBox.Items.Add(filename);
                MontageThemeComboBox.SelectedItem = filename;
                m_ChannelConfigPath = path;
            }
            #endregion

            #region 加载硬件设备信息
            string devName = ConfigurationManager.AppSettings["DeviceName"];
            LocalScanRadioButton.Checked = Program.LocationScan;
            RemoteScanRadioButton.Checked = !Program.LocationScan;
            List<IDevice> devs = Program.LocationScan ? DeviceOnLine.Default.GetDevices() : DeviceOnWLan.Default.getDevices();
            IDevice find = devs.Find(t => t.DeviceName == devName);
            if (find != null)
            {
                DeviceNameComboBox.Items.Add(find.DeviceName);
                DeviceNameComboBox.Text = find.DeviceName;
            }
            #endregion

            PreviousStepButton.Visible = m_showBefore;
            bak_localScan = Program.LocationScan;
            m_init = true;
        }
        #endregion

        #region 按钮 标签的点击事件绑定
        /// <summary>
        /// 取消按钮点击的绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            DataModel.LogInstance.Default.AddLog("点击 监测配置-取消 按钮");
            this.Close();
        }

        /// <summary>
        /// 更多点击的绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoreInfoLabel_Click(object sender, EventArgs e)
        {
            Block.PatientEdit patient = new Block.PatientEdit();
            patient.Initialize(m_patientinfo, false);
            //原来的属性没有赋值成功 owner为null
            patient.Owner = this.FindForm();
            patient.StartPosition = FormStartPosition.CenterScreen;
            patient.ShowDialog();
        }

        /// <summary>
        /// 上一步按钮点击的绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviousStepButton_Click(object sender, EventArgs e)
        {
            DataModel.LogInstance.Default.AddLog("点击 监测配置-上一步 按钮");
            object[] ags = new object[] { m_patientinfo, this.Owner };
            this.Close();
            Task.Factory.StartNew((t) =>
            {
                object[] args = ags as object[];
                MainForm mf = args[1] as MainForm;
                mf.Invoke(new MethodInvoker(() =>
                {
                    PatientEdit patient = new PatientEdit();
                    patient.Initialize(m_patientinfo);
                    patient.Owner = this.Owner;
                    patient.StartPosition = FormStartPosition.CenterParent;
                    patient.SaveInfoHandle += (patient.Owner as MainForm).patient_SaveInfoHandle;
                    patient.ShowDialog();
                }));
            }, ags);
        }

        /// <summary>
        /// 进入PSG监测模式按钮点击的绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterMonitoringModeButton_Click(object sender, EventArgs e)
        {
            #region 用户输入检查
            if (m_ChannelConfigPath == "")
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
            DataModel.LogInstance.Default.AddLog("点击 进入PSG监测模式 按钮");
            Channel.Default.Patient = m_patientinfo;
            m_ChannelData = ChannelManage.Default.ReadChannelConfig(m_ChannelConfigPath);
            Channel.Default.CurrentChannelPath = m_ChannelConfigPath;
            Channel.Default.ChannelChanged();
            if (DeviceNameComboBox.SelectedItem != null && MontageThemeComboBox.SelectedItem != null)
            {
                m_appConfig.AppSettings.Settings["DeviceName"].Value = DeviceNameComboBox.SelectedItem.ToString();
                m_appConfig.AppSettings.Settings["MontageName"].Value = MontageThemeComboBox.SelectedItem.ToString();
                m_appConfig.AppSettings.Settings["DeviceScan"].Value = LocalScanRadioButton.Checked ? "0" : "1";
                bak_localScan = Program.LocationScan = LocalScanRadioButton.Checked;
                m_appConfig.Save();
                ConfigurationManager.RefreshSection("appSettings");
            }
            if (m_ChannelData.Rows.Count > 0)
            {
                //IDevice m_selectDev;
                //IDevice onlinedevie = (DeviceNameComboBox.Tag as List<IDevice>).Find(t => t.DeviceName.Trim() == devName.Trim());
                //IDevice scandevice = (DeviceNameComboBox.Tag as List<IDevice>).Find(t => t.DeviceName == devName);
                IDevice m_selectDev = (DeviceNameComboBox.Tag as List<IDevice>).Find(t => t.DeviceName.Trim() == devName.Trim());
                if(m_selectDev==null)
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
                    DataModel.DeviceOnLine.Default.UpdateDevices(m_selectDev.MatchKey, DeviceState.Running);
                    System.Threading.Thread.Sleep(100);
                    IOClient port = new MySerialPort(m_selectDev.MatchKey);
                    port.Delay = 10;
                    port.AddLog += port_AddLog;
                    m_selectDev.PortClient = port;
                }
                //IOClient port = new MySerialPort("COM26");
                //port.Delay = 10;
                //port.AddLog += port_AddLog;
                //m_selectDev.PortClient = port;

                if (StartMonitorHandler != null)
                {
                    Channel.Default.Patient.RecordTime = DateTime.Now;
                    if (StartMonitorHandler.Invoke(m_selectDev, m_ChannelData))
                    {
                        this.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 编辑按钮点击的绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditLabel_Click(object sender, EventArgs e)
        {
            if (MontageThemeComboBox.SelectedItem != null)
                m_ChannelConfigPath = Path.Combine(ChannelManage.Default.ConfigruationBasePath, MontageThemeComboBox.SelectedItem.ToString());
            else
                m_ChannelConfigPath = "";
            var channel = new Block.MontageCopy();
            channel.Owner = this.ParentForm;
            channel.FilterColumnLoadHandle += Channel.Default.Channel_FilterLoad;//需写在init之前
            channel.Init(ChannelManage.Default.ReadChannelConfig(m_ChannelConfigPath), m_ChannelConfigPath);
            channel.StartPosition = FormStartPosition.CenterParent;
            channel.FilterDropDownHandle += Channel.Default.channel_FilterDropDownHandle;
            channel.ShowDialog();
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

        private void ScanWay_CheckedChanged()
        {
            DeviceNameComboBox.Text = string.Empty;
            DeviceNameComboBox.Items.Clear();
            if (m_init)
            {
                if (LocalScanRadioButton.Checked)
                {
                    DataModel.LogInstance.Default.AddLog("用户选择 设备连接为 本地");
                    DataModel.DeviceOnWLan.Default.Start = false;
                    DataModel.DeviceOnLine.Default.Start = true;
                    bak_localScan = true;
                }
                else
                {
                    DataModel.LogInstance.Default.AddLog("用户选择 设备连接为 远程");
                    DataModel.DeviceOnLine.Default.Start = false;
                    DataModel.DeviceOnWLan.Default.Start = true;
                    bak_localScan = false;
                }
            }
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

        #endregion

        #region 下拉框的相关事件绑定
        /// <summary>
        /// 设备名下拉框的下拉事件绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeviceNameComboBox_DropDown(object sender, EventArgs e)
        {
            DeviceNameComboBox.Items.Clear();
            List<IDevice> devs = LocalScanRadioButton.Checked ?
                                DeviceOnLine.Default.GetDevices() :
                                DeviceOnWLan.Default.getDevices();
            for (int i = 0; i < devs.Count; i++)
                DeviceNameComboBox.Items.Add(devs[i].Description);
            DeviceNameComboBox.Tag = devs;
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
                    DataModel.LogInstance.Default.AddLog(string.Format("设备下拉框 用户选择设备名称{0}", txt),pSystem.LogManagement.LogLevel.WARN);
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
            if (Directory.Exists(ChannelManage.Default.ConfigruationBasePath))
            {
                MontageThemeComboBox.Items.Clear();
                string path = ChannelManage.Default.ConfigruationBasePath;
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
                m_ChannelConfigPath = Path.Combine(ChannelManage.Default.ConfigruationBasePath, MontageThemeComboBox.SelectedItem.ToString());
            DataModel.LogInstance.Default.AddLog(string.Format("用户选择导联方案为 {0}", MontageThemeComboBox.SelectedItem.ToString()),pSystem.LogManagement.LogLevel.INFO);
        }

        #endregion

        #region 其他

        /// <summary>
        /// 添加端口号的打印信息
        /// </summary>
        /// <param name="obj"></param>
        void port_AddLog(pSystem.LogManagement.LogItem obj)
        {
            Console.WriteLine(obj.Description);
        }

        /// <summary>
        /// 销毁此窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewDevice_Disposed(object sender, EventArgs e)
        {
            if (bak_localScan != Program.LocationScan)
            {
                if (Program.LocationScan)
                {
                    DataModel.DeviceOnWLan.Default.Start = false;
                    DataModel.DeviceOnLine.Default.Start = true;
                }
                else
                {
                    DataModel.DeviceOnLine.Default.Start = false;
                    DataModel.DeviceOnWLan.Default.Start = true;
                }
            }
        }
        #endregion
    }
}
