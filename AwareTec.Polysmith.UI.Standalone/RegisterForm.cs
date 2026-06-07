using AwareTec.Polysmith.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.UI
{
    public partial class RegisterForm : SkinForm
    {
        #region 全局变量
        DotNetCtl.ErrorProviderHelper m_errorProvider;
        Configuration m_appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        bool m_userExist = false;
        const int errorPadding = -2;
        #endregion

        #region 初始化相关及窗体绑定事件
        public RegisterForm()
        {
            m_errorProvider = new DotNetCtl.ErrorProviderHelper(this);
            InitializeComponent();
        }

        /// <summary>
        /// 窗体已关闭时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.IsLogin = true;
        }

        private void RegisterForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                RegisterButton_Click(null, null);
            }
        }

        #endregion

        #region 所有控件的绑定事件

        /// <summary>
        /// 离开用户名文本框时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UsernameTextBox_Leave(object sender, EventArgs e)
        {
            DataBaseCom.Doc_UsersInfo user = DataModel.DataBaseHelper.Default.Select(new DataBaseCom.Doc_UsersInfo() { Name = UsernameTextBox.Text });
            if (user != null)
            {
                m_errorProvider.ShowError(UsernameTextBox, "用户名已存在", errorPadding);
                m_userExist = true;
            }
        }

        /// <summary>
        /// 进入用户名文本框时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UsernameTextBox_Enter(object sender, EventArgs e)
        {
            if (m_userExist)
            {
                m_errorProvider.Clear();
                m_userExist = false;
            }
        }

        /// <summary>
        /// 返回按钮点击时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 重置按钮点击时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetButton_Click(object sender, EventArgs e)
        {
            foreach(var item in this.Controls)
            {
                if (item is pSystem.UI.ReaLTaiizor.Controls.MaterialSingleTextBox)
                    (item as pSystem.UI.ReaLTaiizor.Controls.MaterialSingleTextBox).Text = string.Empty;
            }
        }

        /// <summary>
        /// 注册按钮点击时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterButton_Click(object sender, EventArgs e)
        {
            #region 检查用户名
            if (string.IsNullOrEmpty(UsernameTextBox.Text))
            {
                m_errorProvider.ShowError(UsernameTextBox, "用户名不能为空", errorPadding);
                return;
            }
            if (m_userExist)
            {
                return;
            }
            if (UsernameTextBox.Text.Length < 4)
            {
                m_errorProvider.ShowError(UsernameTextBox, "用户名长度限制4-20", errorPadding);
                return;
            }
            else
            {
                m_errorProvider.Clear(UsernameTextBox);
            }
            #endregion

            #region 检查密码
            if (string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                m_errorProvider.ShowError(PasswordTextBox, "密码不能为空", errorPadding);
                return;
            }
            if (PasswordTextBox.Text.Length < 4)
            {
                m_errorProvider.ShowError(PasswordTextBox, "密码长度限制4-20", errorPadding);
                return;
            }
            else
            {
                m_errorProvider.Clear(PasswordTextBox);
            }
            #endregion

            #region 检查密码一致性
            if (PasswordConfirmTextBox.Text != PasswordTextBox.Text)
            {
                m_errorProvider.ShowError(PasswordConfirmTextBox, "两次输入密码不一致", errorPadding);
                return;
            }
            #endregion
            DataBaseCom.Doc_UsersInfo info = new DataBaseCom.Doc_UsersInfo() { Name = UsernameTextBox.Text, PassWord = PasswordTextBox.Text };
            //if (!DataModel.DataBaseHelper.Default.Exsit(info))
            {
                this.DialogResult = DialogResult.OK;
                bool ok = DataModel.DataBaseHelper.Default.Insert(info);
                if (ok)
                {
                    string dirctoryPath = HardDiskMation.Default.getAutoDirectory();
                    DataBaseCom.Doc_SystemSetting doc_SystemSetting = new DataBaseCom.Doc_SystemSetting(true)
                    {
                        UserID = DataModel.DataBaseHelper.Default.Select(info).ID,
                        SaveEdfPath = string.Format("{0}Data", dirctoryPath),
                        VedioSavePath = string.Format("{0}Media", dirctoryPath),
                        ReportPath = string.Format("{0}pSleepReport", dirctoryPath),
                    };
                    DataModel.DataBaseHelper.Default.Insert(doc_SystemSetting);///插入相应的配置表数据
                    if (!System.IO.Directory.Exists(doc_SystemSetting.SaveEdfPath))
                        System.IO.Directory.CreateDirectory(doc_SystemSetting.SaveEdfPath);
                    if (!System.IO.Directory.Exists(doc_SystemSetting.VedioSavePath))
                        System.IO.Directory.CreateDirectory(doc_SystemSetting.VedioSavePath);
                    if (!System.IO.Directory.Exists(doc_SystemSetting.ReportPath))
                        System.IO.Directory.CreateDirectory(doc_SystemSetting.ReportPath);
                    AhDung.MessageTip.ShowOk("用户注册成功！");
                    Program.newUser = true;
                    Program.userName = info.Name;
                    this.Close();
                }
                else
                {
                    AhDung.MessageTip.ShowError("用户注册失败！", errorPadding);
                }
            }
        }

        /// <summary>
        /// 密码文本框的文本改变时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            if (PasswordTextBox.Text == PasswordConfirmTextBox.Text)
            {
                m_errorProvider.Clear(PasswordTextBox);
                m_errorProvider.Clear(PasswordConfirmTextBox);
            }
        }

        /// <summary>
        /// 用户名文本框发生键盘按下时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UsernameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = DataModel.InputTextCheck.CheckChar(e.KeyChar.ToString());
        }

        /// <summary>
        /// 显示或隐藏密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewOrHidePasswordPictureBox_Click(object sender, EventArgs e)
        {
            PasswordTextBox.UseSystemPasswordChar = !PasswordTextBox.UseSystemPasswordChar;
            this.ViewOrHidePasswordPictureBox.BackgroundImage = !PasswordTextBox.UseSystemPasswordChar ?
                                                                Properties.Resources.viewPassword :
                                                                Properties.Resources.hidePassword;
        }

        #endregion
    }
}
