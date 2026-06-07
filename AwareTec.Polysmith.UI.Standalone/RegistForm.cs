using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using pSystem.UI.Register;
using System.Threading;
using DotNetCtl;
using AwareTec.Polysmith.UI.FunctionControls.tools;

namespace AwareTec.Polysmith.UI
{
    public partial class RegistForm : SkinForm
    {
        ErrorProviderHelper errorProvider_Reg;
        private Reg m_regist = null;
        public RegistForm()
        {
            InitializeComponent();
            this.Load += RegistForm_Load;
            m_regist = new Reg("physio.cn", true) { VerifyMode = emVerifyMode.Both };
            m_regist.SetRegistKey("CNTenLater");
            string password = m_regist.VerifyReg().ToUpper();
            if (password == "20HL" || password == "20WL")
            {
                isOk = true;
            }
            errorProvider_Reg = new ErrorProviderHelper(this);
        }
        /// <summary>
        /// 是否已经注册成功
        /// </summary>
        public bool isOk = false;
        private void RegistForm_Load(object sender, EventArgs e)
        {
            MachineTextBox.Text = m_regist.getMachineID();
            //hospitalTextBox.Text = Channel.Default.SystemSetting.Reserve3;
            this.Text = Program.Language=="EN"? "Register V1.1.0.15" : "注册  V1.1.0.15";
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (this.hospitalTextBox.Text.Length>0)
                Channel.Default.SystemSetting.Reserve3 = this.hospitalTextBox.Text;

            Task.Factory.StartNew(() =>
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    string errMessage = "";
                    errorProvider_Reg.Clear();
                    string regcode = this.MachineNoTextBox.Text.Trim();
                    if (!String.IsNullOrEmpty(regcode))
                    {
                        int cs = 0;
                        bool _reg = false;

                    _begin:
                        try
                        {
                            _reg = m_regist.WriteReg(regcode);
                        }
                        catch (Exception ex)
                        {
                            errMessage = ex.Message;
                            Thread.Sleep(500);
                            if (cs < 20)
                            {
                                cs++;
                                goto _begin;
                            }
                        }

                        if (_reg)
                        {
                            DialogResult = System.Windows.Forms.DialogResult.OK;
                            isOk = true;
                            MessageForm.Show(Program.Language == "EN" ? "Registered Successfully" : "注册成功，请点击“确定”进入睡眠分析系统！", Program.Language == "EN" ? "Message" : "注册提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                            this.Dispose();
                        }
                        else
                            errorProvider_Reg.ShowError(MachineNoTextBox, Program.Language == "EN" ? "Invalid Registration Code\r\r\n" : "无效注册码\r\n\r\n" + errMessage);
                    }
                    else
                        errorProvider_Reg.ShowError(MachineNoTextBox, Program.Language == "EN" ? "The registration code cannot be empty!" : "注册码不能为空!");
                }));
            });
        }

        private void ZhuCeButton_Click(object sender, EventArgs e)
        {
           MachineNoTextBox.Text= m_regist.CreateRegInfo(MachineTextBox.Text, 1, 120);
        }

        private void _CompanyLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://www.physiomedtec.com/");
            }
            catch (Exception ee)
            {
                AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "Please set any browser other than IE as the default browser before opening the website" : "请将除IE浏览器之外的其他浏览器设为默认浏览器，再打开网址");
            }
        }
    }
}
