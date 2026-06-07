using AwareTec.Polysmith.Cloud.UI.DataModels.Global;
using AwareTec.Polysmith.Cloud.UI.Forms.ConfigurationBlock;
using AwareTec.Polysmith.Cloud.UI.Forms.Tips;
using AwareTec.Polysmith.Cloud.UI.ViewHelpers;
using AwareTec.Polysmith.Util.EncryptUtils;
using RestfulWebRequest.ApiCall;
using RestfulWebRequest.EnumModels.EnumModels4Table;
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
    /// <summary>
    /// 登录窗体
    /// </summary>
    public partial class LoginForm : CloudSkinForm
    {
        public LoginForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyPress += LoginForm_KeyPress;
        }
        /// <summary>
        /// 回车登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                LoginButton_Click(null, null);
            }
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            LoginButton.Focus();
            if (string.IsNullOrWhiteSpace(LoginInfoView.AccountName))
            {
                MessageForm.Show("用户名不得为空");
                return;
            }

            UserDomain oldUserDomain = null;
            string passwordMd5 = null;
            if (LoginInfoView.RememberPassword && !LoginInfoView.PassWordIsChange)
            {
                if ((oldUserDomain = LoginHelper.ReadUser(LoginInfoView.AccountName)) != null)
                    passwordMd5 = string.IsNullOrWhiteSpace(oldUserDomain.PasswordMd5) ? null : oldUserDomain.PasswordMd5;
            }

            LoginInfoView.PassWordIsChange = false;

            if (string.IsNullOrWhiteSpace(passwordMd5) &&
               string.IsNullOrWhiteSpace(LoginInfoView.Password))
            {
                MessageForm.Show("密码不得为空");
                return;
            }

            bool isSuccess = ApiRequest.Instance.Login(new UserLoginRequestModel()
            {
                account = LoginInfoView.AccountName,
                password = string.IsNullOrWhiteSpace(passwordMd5) ?
                           Md5Helper.EncryptString(LoginInfoView.Password) :
                           passwordMd5
            }, out ResponseModel responseModel);

            if (!isSuccess)
            {
                var model = responseModel as ResponseFailModel<UserLoginRequestModel>;
                MessageForm.Show("用户名或密码不正确，请检查！");
                return;
            }

            var userModel = (responseModel as ResponseSuccessModel<UserLoginResponseModel>).RestfulTable;
            if (userModel.type == UserType.ChannelBusinessUser)
            {
                MessageForm.Show("当前渠道商暂时不予以登录");
                return;
            }

            string password = LoginInfoView.RememberPassword ?
                              string.IsNullOrWhiteSpace(passwordMd5) ?
                              Md5Helper.EncryptString(LoginInfoView.Password) :
                              passwordMd5 : string.Empty;
            UserDomain newUserDomain = new UserDomain(LoginInfoView.AccountName,
                                                      password,
                                                      LoginInfoView.RememberPassword)
            {
                ModeType = LoginInfoView.Mode
            };
            bool newSuccessful = LoginHelper.NewUser(newUserDomain, out string errMessage);
            if (!newSuccessful)
            {
                MessageForm.Show("检索用户文件夹失败" + errMessage);
                return;
            }
                

            GlobalSingleton.Instance.User = newUserDomain;

            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("用户点击登陆按钮，登陆的用户名为 {0}，登陆模式为 {1}", LoginInfoView.AccountName, LoginInfoView.Mode == ModeType.Child ? "儿童版" : "成人版", pSystem.LogManagement.LogLevel.INFO));

            DialogResult = DialogResult.OK;
        }

        private void FullUserConfigButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("用户在登陆页面点击全用户配置图标", pSystem.LogManagement.LogLevel.INFO);
            var form = new FullUserConfiguration();
            form.ShowDialog();
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Size iconSize = new Size(32, 32);
            Rectangle R = new Rectangle(this.Location, iconSize);
            if (R.Contains(Cursor.Position) && e.CloseReason == CloseReason.UserClosing)
                e.Cancel = true;
        }
    } 
}
 