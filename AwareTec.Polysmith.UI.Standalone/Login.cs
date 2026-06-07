using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwareTec.Polysmith.Util;
using System.Windows.Forms;
using AwareTec.Polysmith.Util.EnumUtils;
using AwareTec.Polysmith.DataBaseCom;
using AwareTec.Polysmith.UI.EnumModel;
using AwareTec.Polysmith.UI.CompatibleDbManager.SystemSettingTable;
using AwareTec.Polysmith.UI.CompatibleDbManager;
using AwareTec.Polysmith.Util.PathUtils;

namespace AwareTec.Polysmith.UI
{
    public partial class Login : SkinForm
    {
        #region 全局变量
        DotNetCtl.ErrorProviderHelper m_errorProvider;
        Configuration m_appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        string m_psw = "";
        bool m_userNull = false;
        const int USERNAME_PADDING = 22;
        const int PASSWORD_PADDING = -40;
        #endregion

        #region 私有方法

        /// <summary>
        /// 初始化下拉框的选项数据并设置默认选中项
        /// </summary>
        /// <param name="mode">默认选中的模式, 默认成人</param>
        private void InitComboBox(ModeType mode = ModeType.Adult)
        {
            SelectModeComboBox.DataSource = EnumMappingObject<ModeType>.ParseEnum();
            SelectModeComboBox.DisplayMember = "Description";

            SelectModeComboBox.SelectedItem = mode;
        }

        #endregion

        #region 初始化相关及窗体绑定事件

        /// <summary>
        /// 构造函数
        /// </summary>
        public Login()
        {
            m_errorProvider = new DotNetCtl.ErrorProviderHelper(this);
            InitializeComponent();

            if (Program.Language == "EN") {

                UserNameTextBox.Hint = "Input Name";// ML.GetText("LOGIN_NAME_HINT", "输入账号");
                PasswordTextBox.Hint = "Input Password";// ML.GetText("LOGIN_PASSWORD_HINT", "输入密码");
                SelectModeComboBox.Hint = "Mode Select";// ML.GetText("LOGIN_SLECTMODE_HINT", "选择模式");
                RemberMeCheckBox.Text = "Rember Me";// ML.GetText("LOGIN_REMBERME", "记住密码");
                LoginButton.Text = "Login";// ML.GetText("LOGIN_LOGIN", "登录");
                RegisterButton.Text = "Register";// ML.GetText("LOGIN_REGISTER", "注册");
                this.Text = "AwareSleep PSG Analysis Software V1.0";
            }

            // ML.LoadFormLanguage(this);
        }

        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_Load(object sender, EventArgs e)
        {
            if (Program.newUser)
            {
                UserNameTextBox.Text = Program.userName;
                PasswordTextBox.Text = "";
                return;
            }
            RemberMeCheckBox.Checked = bool.Parse(ConfigurationManager.AppSettings["Remember"]);
            UserNameTextBox.Text = ConfigurationManager.AppSettings["UserName"];
            Program.LocationScan = int.Parse(ConfigurationManager.AppSettings["DeviceScan"]) == 0;
            if (RemberMeCheckBox.Checked)
            {
                PasswordTextBox.Text = ConfigurationManager.AppSettings["PassWord"];
                m_errorProvider.ShowSuccess(PasswordTextBox, Program.Language=="EN"? "Password Correct" : "密码正确", PASSWORD_PADDING);
            }
            else
            {
                PasswordTextBox.Text = "";
            }

            InitComboBox();
           
        }

        /// <summary>
        /// 窗体已关闭时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.newUser = false;
        }

        private void Login_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                LoginButton_Click(null, null);
            }
        }
        #endregion

        #region 所有控件的绑定事件

        #region 用户名

        /// <summary>
        /// 离开用户名文本框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserNameTextBox_Leave(object sender, EventArgs e)
        {
            var current = sender as Control;

            m_psw = "";
            DataBaseCom.Doc_UsersInfo user = DataModel.DataBaseHelper.Default.Select(new DataBaseCom.Doc_UsersInfo() { Name = UserNameTextBox.Text });
            if (user != null)
            {
                m_psw = user.PassWord;
                m_userNull = false;
            }
            else
            {
                m_errorProvider.ShowError(current, Program.Language == "EN" ? "User Name does not exist" : "用户名不存在", USERNAME_PADDING);
                m_userNull = true;
            }
        }

        /// <summary>
        /// 进入用户名文本框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserNameTextBox_Enter(object sender, EventArgs e)
        {
            if (m_userNull)
                m_errorProvider.Clear();
        }

        private void UserNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = DataModel.InputTextCheck.CheckChar(e.KeyChar.ToString());
            if (!e.Handled)
            {
                m_errorProvider.Clear(PasswordTextBox);
            }
        }
        #endregion

        #region 密码
        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            if (m_psw != "")
            {
                string value = PasswordTextBox.Text;
                if (value == m_psw)
                {
                    m_errorProvider.ShowSuccess(PasswordTextBox, Program.Language == "EN" ? "Password Correct" : "密码正确", PASSWORD_PADDING);
                }
                else
                {
                    m_errorProvider.Clear();
                }
            }
        }

        #endregion

        #region 按钮
        /// <summary>
        /// 登录按钮点击时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginButton_Click(object sender, EventArgs e)
        {
            var user = CheckInput();
            if (user == null)
                return;

            m_errorProvider.Clear(PasswordTextBox);
            #region 模式更新
            
            var mode = (SelectModeComboBox.SelectedItem as EnumMappingObject<ModeType>).Value;
            string log = "选中模式为:{0},正在更新数据库的数据...";
            DataModel.LogInstance.Default.AddLog(string.Format(log, EnumHelper.GetDescription(mode)), pSystem.LogManagement.LogLevel.DEBUG);
            if (!ModeTypeCompatibleDbManager.UpdateModeTypeInDb(mode, user))
                throw new CompatibleDbManagerException(typeof(ModeTypeCompatibleDbManager),
                                                       typeof(Doc_SystemSetting),
                                                       mode.GetType());
            #endregion

            //此登录用户所对应的系统设置表的数据
            var currentSystemSetting = new Doc_SystemSetting() { UserID = user.ID, ModeType = (int)mode };

            Channel.Default.SystemSetting = DataModel.DataBaseHelper.Default.Select(currentSystemSetting);
            this.DialogResult = DialogResult.OK;
            #region 保存此次的登录信息予以下次使用
            DataModel.LogInstance.Default.AddLog(string.Format("进入系统，用户名[{0}] 密码{1}记住", user.Name, RemberMeCheckBox.Checked ? "" : "不"), pSystem.LogManagement.LogLevel.DEBUG);
            m_appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            m_appConfig.AppSettings.Settings["UserName"].Value = UserNameTextBox.Text;
            if (RemberMeCheckBox.Checked)
            {
                m_appConfig.AppSettings.Settings["Password"].Value = PasswordTextBox.Text;
                m_appConfig.AppSettings.Settings["Remember"].Value = "true";
            }
            else
            {
                m_appConfig.AppSettings.Settings["Password"].Value = "";
                m_appConfig.AppSettings.Settings["Remember"].Value = "false";
            }
            m_appConfig.Save();
            ConfigurationManager.RefreshSection("appSettings");
            #endregion

            string path = string.Empty;
            if((path = ConfigureSavePath(Channel.Default.SystemSetting)) == null)
                DataModel.LogInstance.Default.AddLog("自动配置数据存储路径失败", pSystem.LogManagement.LogLevel.ERROR);
            else
                DataModel.LogInstance.Default.AddLog(string.Format("当前数据存储路径[{0}]:", StringPath.ConvertLogPath(path)), pSystem.LogManagement.LogLevel.DEBUG);

            Channel.Default.Loginer = user;
            this.Close();
        }

        /// <summary>
        /// 检查存储路径是否为空，为空则自动进行配置并返回路径
        /// </summary>
        /// <param name="systemSetting"></param>
        /// <returns>
        /// 路径为空并配置成功，返回路径；
        /// 路径不为空也返回路径；
        /// 路径为空并配置失败，返回null；
        /// </returns>
        private string ConfigureSavePath(Doc_SystemSetting systemSetting)
        {
            DataModel.LogInstance.Default.AddLog("系统正在检查数据存储路径...", pSystem.LogManagement.LogLevel.DEBUG);
            if (systemSetting == null)
                throw new Exception("系统配置对象为空指针");

            if (!(string.IsNullOrWhiteSpace(systemSetting.SaveEdfPath))|| !(string.IsNullOrWhiteSpace(systemSetting.ReportPath))|| !(string.IsNullOrWhiteSpace(systemSetting.VedioSavePath)))
            {
                DataModel.LogInstance.Default.AddLog("数据存储路径不为空，跳过配置数据存储路径...", pSystem.LogManagement.LogLevel.DEBUG);
                return systemSetting.SaveEdfPath;
            }

            try
            {
                DataModel.LogInstance.Default.AddLog("数据存储路径为空，系统即将自动配置数据存储路径...", pSystem.LogManagement.LogLevel.DEBUG);
                string dirctoryPath = HardDiskMation.Default.getAutoDirectory();
                DataBaseCom.Doc_SystemSetting doc_SystemSetting = new DataBaseCom.Doc_SystemSetting()
                {
                    SaveEdfPath = string.Format("{0}Data", dirctoryPath),
                    VedioSavePath = string.Format("{0}Media", dirctoryPath),
                    ReportPath = string.Format("{0}pSleepReport", dirctoryPath),
                };
                if (!System.IO.Directory.Exists(doc_SystemSetting.SaveEdfPath))
                    System.IO.Directory.CreateDirectory(doc_SystemSetting.SaveEdfPath);
                if (!System.IO.Directory.Exists(doc_SystemSetting.VedioSavePath))
                    System.IO.Directory.CreateDirectory(doc_SystemSetting.VedioSavePath);
                if (!System.IO.Directory.Exists(doc_SystemSetting.ReportPath))
                    System.IO.Directory.CreateDirectory(doc_SystemSetting.ReportPath);

                DataModel.DataBaseHelper.Default.Update(new DataBaseCom.Doc_SystemSetting()
                {
                    ID = systemSetting.ID
                },
                doc_SystemSetting);
                Channel.Default.SystemSetting.SaveEdfPath = doc_SystemSetting.SaveEdfPath;
                Channel.Default.SystemSetting.VedioSavePath = doc_SystemSetting.VedioSavePath;
                Channel.Default.SystemSetting.ReportPath = doc_SystemSetting.ReportPath;
                Channel.Default.SystemSetting.MaxRecodFileLenght = doc_SystemSetting.MaxRecodFileLenght;
                DataModel.LogInstance.Default.AddLog("数据存储路径自动化配置完成", pSystem.LogManagement.LogLevel.WARN);
                return dirctoryPath;
                
            }
            catch(Exception e)
            {
                return null;
            }
        }


        private Doc_UsersInfo CheckInput()
        {
            #region  检查用户名
            if (m_userNull)
                return null;

            if (string.IsNullOrEmpty(UserNameTextBox.Text.Trim()))
            {
                m_errorProvider.ShowError(UserNameTextBox, Program.Language == "EN" ? "The username cannot be empty" : "用户名不能为空", USERNAME_PADDING);
                return null;
            }

            if (UserNameTextBox.Text.Length < 4)
            {
                m_errorProvider.ShowError(UserNameTextBox, Program.Language == "EN" ? "User name length limit 4-20" : "用户名长度限制4-20", USERNAME_PADDING);
                return null;
            }
            else
            {
                m_errorProvider.Clear(UserNameTextBox);
            }
            #endregion

            #region 检查密码输入
            if (string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                m_errorProvider.ShowError(PasswordTextBox, "密码不能为空", PASSWORD_PADDING);
                return null;
            }
            if (PasswordTextBox.Text.Length < 4)
            {
                m_errorProvider.ShowError(PasswordTextBox, "密码长度限制4-20", PASSWORD_PADDING);
                return null;
            }

            #endregion

            #region 检查账号密码在数据库中的匹配
            DataBaseCom.Doc_UsersInfo info = DataModel.DataBaseHelper.Default.Select(new DataBaseCom.Doc_UsersInfo() { Name = UserNameTextBox.Text, PassWord = PasswordTextBox.Text });
            if (info == null)
            {
                m_errorProvider.ShowError(PasswordTextBox, "密码错误", PASSWORD_PADDING);
                DataModel.LogInstance.Default.AddLog(string.Format("尝试使用用户名[{0}]登录，密码错误！", UserNameTextBox.Text), pSystem.LogManagement.LogLevel.ERROR);
                return null;
            }
            #endregion

            return info;
        }

        /// <summary>
        /// 注册按钮点击时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterForm register = new RegisterForm();
            register.ShowDialog();
            this.Close();
        }
        #endregion

        #endregion
    }
}
