using AwareTec.Polysmith.Cloud.UI.ViewHelpers;
using AwareTec.Polysmith.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.Cloud.UI.Forms.MainFormBlock
{
    public partial class MontageDialog : CloudSkinForm
    {
        #region 私有变量

        #endregion

        #region 公有变量

        /// <summary>
        /// 当前方案名称
        /// </summary>
        public string CurrentName { set; get; }

        #endregion

        #region 事件委托

        /// <summary>
        /// 保存方案时触发
        /// </summary>
        public delegate void SaveMontageDelegate(params object[] args);
        /// <summary>
        /// 保存方案时触发
        /// </summary>
        public event SaveMontageDelegate SaveMontageHandle;

        #endregion

        #region 初始化 load/dispose/init

        /// <summary>
        /// 初始化
        /// </summary>
        public MontageDialog()
        {
            InitializeComponent();
            this.Load += MontageDialog_Load;
        }
        
        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MontageDialog_Load(object sender, EventArgs e)
        {
            this.CancelButton.Click += CancelButton_Click;
            this.SubmitButton.Click += SubmitButton_Click;
            this.SaveAsRadioBut.CheckedChanged += SaveAsRadioBut_CheckedChanged;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 另存为新的方案 按钮状态改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAsRadioBut_CheckedChanged(object sender, EventArgs e)
        {
            if (ChannelNameTxtBox.Text == "" && SaveAsRadioBut.Checked)
            {
                ChannelNameTxtBox.Text = string.Format("bak_{0}", CurrentName == null ? "channel" : CurrentName.Replace(".cfg", ""));
            }
        }

        #endregion

        #region 按钮方法

        /// <summary>
        /// 通道管理 保存按钮 点击后 提交按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            object[] args = new object[0];
            if (UpadteRadioBut.Checked)
            {
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("用户选择 更改并保存");
                args = new object[] { true };
            }
            else if (SaveAsRadioBut.Checked)
            {
                if (string.IsNullOrWhiteSpace(ChannelNameTxtBox.Text))
                {
                    AhDung.MessageTip.ShowError("请输入方案名称!");
                    return;
                }
                else
                {
                    if (Directory.Exists(ChannelManageCloud.Default.ConfigruationBasePath))
                    {
                        string path = ChannelManageCloud.Default.ConfigruationBasePath;
                        int len = path.Length + 1;
                        string[] names = Directory.GetFiles(path, "*.cfg");
                        if (names.Length > 0)
                        {
                            if (names.Where(t => Path.GetFileNameWithoutExtension(t) == ChannelNameTxtBox.Text).Count() > 0)
                            {
                                AhDung.MessageTip.ShowError("方案名称已存在，请重新输入!");
                                return;
                            }
                        }
                    }
                    args = new object[2];
                    args[1] = ImmediateUseCheckBox.Checked;
                    args[0] = string.Format("{0}.cfg", ChannelNameTxtBox.Text);
                    DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("用户选择 另存为新的导联方案 新导联方案名称为 {0}.cfg", ChannelNameTxtBox.Text), pSystem.LogManagement.LogLevel.INFO);
                }
            }
            else
            {
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("用户选择 临时更改，适用本次");
            }
            if (SaveMontageHandle != null)
                SaveMontageHandle.Invoke(args);
            DialogResult = DialogResult.Yes;
        }

        /// <summary>
        /// 通道管理 保存按钮 点击后 取消按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("用户点击 保存-取消按钮");
            this.Close();
        }

        #endregion

        #region 公有方法

        #endregion

    }
}
