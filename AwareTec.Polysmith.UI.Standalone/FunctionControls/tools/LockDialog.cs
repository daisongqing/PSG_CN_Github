using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.UI.FunctionControls.tools
{
    public partial class LockDialog : SkinForm
    {
        DotNetCtl.ErrorProviderHelper m_errorProvider;
        public LockDialog()
        {
            InitializeComponent();
            m_errorProvider = new DotNetCtl.ErrorProviderHelper(this);
            this.KeyPreview = true;
            this.KeyDown += LockDialog_KeyDown;
            m_password = Channel.Default.Loginer.PassWord;
            TbPassword.Focus();
        }

        private void LockDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SubmitButton_Click(null,null);
            }
        }
        public string PSW
        {
            set
            {
                m_password = value;
            }
        }
        /// <summary>
        /// 预留的超级权限密码
        /// </summary>
        private string masterPassword = "admin";
        private string m_password = "admin";
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (TbPassword.Text == m_password||TbPassword.Text == masterPassword)
            {
                DialogResult = DialogResult.OK;
                DataModel.LogInstance.Default.AddLog(string.Format("用户点击 解锁-提交按钮，输入的解锁密码为 {0}", TbPassword.Text));
            }
            else
            {
                m_errorProvider.ShowError(TbPassword, Program.Language == "EN" ? "Password Error" : "密码错误");
            }
        }

        private void TbPassword_TextChanged(object sender, EventArgs e)
        {
            if (TbPassword.Text == m_password || TbPassword.Text == masterPassword)
            {
                m_errorProvider.ShowSuccess(TbPassword, Program.Language == "EN" ? "Password Correct" : "密码正确");
            }
            else
            {
                m_errorProvider.ShowError(TbPassword, Program.Language == "EN" ? "Password Error" : "密码错误");
            }
        }

        private void TbPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = DataModel.InputTextCheck.CheckChar(e.KeyChar.ToString());
        }
    }
}
