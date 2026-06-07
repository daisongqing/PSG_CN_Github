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

namespace AwareTec.Polysmith.UI.Block
{
    public partial class MontageDialog : SkinForm
    {
        public MontageDialog()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 当前方案名称
        /// </summary>
        public string CurrentName { set; get; }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            object[] args = new object[0];
            if (RaBtUpadte.Checked)
            {
                DataModel.LogInstance.Default.AddLog("用户选择 更改并保存");
                args = new object[] { true };
            }
            else if (RaBtSave.Checked)
            {
                if (string.IsNullOrWhiteSpace(ChannelName.Text))
                {
                    AhDung.MessageTip.ShowError("请输入方案名称!");
                    return;
                }
                else
                {
                    if (Directory.Exists(ChannelManage.Default.ConfigruationBasePath))
                    {
                        string path = ChannelManage.Default.ConfigruationBasePath;
                        int len = path.Length + 1;
                        string[] names = Directory.GetFiles(path, "*.cfg");
                        if (names.Length > 0)
                        {
                            if (names.Where(t => Path.GetFileNameWithoutExtension(t) == ChannelName.Text).Count() > 0)
                            {
                                AhDung.MessageTip.ShowError("方案名称已存在，请重新输入!");
                                return;
                            }
                        }
                    }
                    args = new object[2];
                    args[1] = ribbonCheckBox1.Checked;
                    args[0] = string.Format("{0}.cfg", ChannelName.Text);
                    DataModel.LogInstance.Default.AddLog(string.Format("用户选择 另存为新的导联方案 新导联方案名称为 {0}.cfg", ChannelName.Text),pSystem.LogManagement.LogLevel.INFO);
                }
            }
            else
            {
                DataModel.LogInstance.Default.AddLog("用户选择 临时更改，适用本次");
            }
            if (SaveMontageHandle != null)
                SaveMontageHandle.Invoke(args);
            DialogResult = DialogResult.Yes;
        }
        /// <summary>
        /// 保存方案时触发
        /// </summary>
        public delegate void SaveMontageDelegate(params object[] args);
        /// <summary>
        /// 保存方案时触发
        /// </summary>
        public event SaveMontageDelegate SaveMontageHandle;

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            DataModel.LogInstance.Default.AddLog("用户点击 保存-取消按钮");
            this.Close();
        }

        private void RaBtSave_CheckedChanged(object sender, EventArgs e)
        {
            if (ChannelName.Text == "" && RaBtSave.Checked)
            {
                ChannelName.Text = string.Format("bak_{0}", CurrentName == null ? "channel" : CurrentName.Replace(".cfg", ""));
            }
        }
    }
}
