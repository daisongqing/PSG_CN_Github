using AwareTec.Polysmith.Cloud.UI.DataModels.Global;
using AwareTec.Polysmith.Cloud.UI.ViewHelpers;
using AwareTec.Polysmith.Util.EnumUtils;
using MinimalistUI.EnumModels;
using RestfulWebRequest.EnumModels.EnumModels4Table;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.Cloud.UI.Views
{
    public partial class LoginInfoView : UserControl
    {
        private ModeType _modeType = ModeType.Adult;
        private bool _passwordischange = false;
        public LoginInfoView()
        {
            InitializeComponent();
            this.AccountTextbox.Focus();
            this.AccountTextbox.TextChanged += AccountTextbox_TextChanged;
            this.PasswordTextbox.KeyDown += PasswordTextbox_KeyDown; 
        }


        public string AccountName => AccountTextbox.Text.Replace(" ","");
        public string Password => PasswordTextbox.Text;
        public bool RememberPassword => RememberPasswordCheckBox.Checked;
        public bool PassWordIsChange
        {
            set => _passwordischange = value;
            get => _passwordischange;
        }
        public ModeType Mode => _modeType;

        /// <summary>
        /// 儿童版 成人版切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModeTypeSwitch_SwitchChanged(object sender, MinimalistUI.CustomEventArgs.SwitchControls.SwitchChangedEventArgs e)
        {
            foreach (ModeType item in Enum.GetValues(typeof(ModeType)))
            {
                if (EnumHelper.GetDescription(item).Equals(e.SwitchText))
                {
                    _modeType = item;
                    return;
                }
            }
        }



        private void PasswordTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                return;
            }
            else
            {
                _passwordischange = true;
            }
        }

        /// <summary>
        /// 用户名输入值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccountTextbox_TextChanged(object sender, EventArgs e)
        {
            UserDomain userDomain;
            if ((userDomain = LoginHelper.ReadUser(AccountName)) == null)
            {
                PasswordTextbox.Text = "";
                return;
            }
            PasswordTextbox.Text = userDomain.PasswordMd5;
            RememberPasswordCheckBox.Checked = userDomain.RememberPassword;
        }

    }
}
