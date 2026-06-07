using AwareTec.Polysmith.Cloud.UI.DataModels.Global;
using pSystem.UI.Register;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.Cloud.UI.Forms
{
    /// <summary>
    /// 序列号激活窗体
    /// </summary>
    public partial class LicenseActivationForm : CloudSkinForm
    {
        public LicenseActivationForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            MachineCodeTextBox.Text = GlobalSingleton.Instance.Reg.getMachineID();
        }

        private void ActivateButton_Click(object sender, EventArgs e)
        {
            SerialNumberTextBox.ErrorMessage = null;
            Task.Factory.StartNew(() =>
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    string errMessage = "";
                    
                    string regcode = this.SerialNumberTextBox.Text.Trim();

                    if (string.IsNullOrEmpty(regcode))
                    {
                        SerialNumberTextBox.ErrorMessage = "激活码不能为空";
                        return;
                    }

                    int cs = 0;
                    bool _reg = false;
                _begin:
                    try
                    {
                        _reg = GlobalSingleton.Instance.Reg.WriteReg(regcode);
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
                        DialogResult = DialogResult.OK;
                        MessageBox.Show("激活成功，请点击“确定”进入睡眠分析系统(云服务版)！", "激活提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                        this.Dispose();
                    }
                    else
                        SerialNumberTextBox.ErrorMessage = "无效激活码";
                }));
            });
        }

        private void _CompanyLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://sleep.physiomedtec.com:5301/index.html#/login");
            }
            catch (Exception ee)
            {
                AhDung.MessageTip.ShowWarning("请将除IE浏览器之外的其他浏览器设为默认浏览器，再打开网址");
            }
        }
    }
}
