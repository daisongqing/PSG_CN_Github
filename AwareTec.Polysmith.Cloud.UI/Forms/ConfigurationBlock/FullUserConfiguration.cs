using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.FullUser;
using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.SpecificXmlHelper;
using AwareTec.Polysmith.Cloud.UI.DataModels.EnumModels;
using AwareTec.Polysmith.Cloud.UI.DataModels.Global;
using AwareTec.Polysmith.Cloud.UI.Forms.Tips;
using AwareTec.Polysmith.Cloud.UI.ViewHelpers;
using AwareTec.Polysmith.Util.PathUtils;
using AwareTec.Polysmith.Util.XmlUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace AwareTec.Polysmith.Cloud.UI.Forms.ConfigurationBlock
{
    public partial class FullUserConfiguration : CloudSkinForm
    {
        private FullUserConfigurationModel _originalModel = null;

        public FullUserConfiguration()
        {
            InitializeComponent();
            Init();
            this.FormClosed += FullUserConfiguration_FormClosed;
        }

        private void FullUserConfiguration_FormClosed(object sender, FormClosedEventArgs e)
        {
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("用户点击全用户配置-退出图标", pSystem.LogManagement.LogLevel.INFO);
        }

        private void Init()
        {
            var xmlHelper = new FullUserConfigurationXmlHelper(GlobalReadonlyString.PredefinedPath.FULL_USER_CONFIG_FILE);
            _originalModel = xmlHelper.Read();
            RootPathTextBox.Text = _originalModel.RootPath;
            OrderPathLabel.Text = _originalModel.OrderPath;
            ReportPathLabel.Text = _originalModel.ReportPath;
            RemoteUrlTxt.Text = _originalModel.RemoteUrl;
        }

        private void SaveButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            if (string.IsNullOrWhiteSpace(RootPathTextBox.Text) ||
                string.IsNullOrWhiteSpace(OrderPathLabel.Text) ||
                string.IsNullOrWhiteSpace(ReportPathLabel.Text))
            {
                AhDung.MessageTip.ShowWarning("路径不得为空");
                return;
            }
                

            if(RootPathTextBox.Text.Equals(_originalModel.RootPath) &&
               OrderPathLabel.Text.Equals(_originalModel.OrderPath) &&
               ReportPathLabel.Text.Equals(_originalModel.ReportPath) &&
               RemoteUrlTxt.Text.Equals(_originalModel.RemoteUrl))
            {
                AhDung.MessageTip.ShowWarning("当前无修改");
                return;
            }

            var model = new FullUserConfigurationModel()
            {
                RootPath = RootPathTextBox.Text,
                OrderPath = OrderPathLabel.Text,
                ReportPath = ReportPathLabel.Text,
                RemoteUrl = RemoteUrlTxt.Text.TrimEnd('/')
            };
            var xmlHelper = new FullUserConfigurationXmlHelper(GlobalReadonlyString.PredefinedPath.FULL_USER_CONFIG_FILE);
            if (xmlHelper.Modify(model))
            {
                RestfulWebRequest.ApiCall.ApiRequest.RemoteUrl = model.RemoteUrl;
                AhDung.MessageTip.ShowOk("修改成功");
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("用户点击全用户配置-保存按钮,软件存储根目录路径为 {0}," +
                    "首页表单数据路径为 {1},导出报告存储路径为 {2},云服务地址设置为 {3},保存成功",
                    RootPathTextBox.Text, OrderPathLabel.Text, ReportPathLabel.Text, RemoteUrlTxt.Text.TrimEnd('/')), pSystem.LogManagement.LogLevel.WARN);
            }
            else
            {
                AhDung.MessageTip.ShowError("修改失败");
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("用户点击全用户配置-保存按钮,软件存储根目录路径为 {0}," +
                    "首页表单数据路径为 {1},导出报告存储路径为 {2},云服务地址设置为 {3},保存失败",
                    RootPathTextBox.Text, OrderPathLabel.Text, ReportPathLabel.Text, RemoteUrlTxt.Text.TrimEnd('/')), pSystem.LogManagement.LogLevel.ERROR);
                return;
            }
            this.Close();
        }

        private void CancelButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            FullUserConfigurationModel model = GetSuggestionAboutPathHelper.GetAndGeneratePath();
            if (model.RootPath.Equals(RootPathTextBox.Text))
            {
                MessageForm.Show("已经为推荐配置");
                return;
            }

            RootPathTextBox.Text = model.RootPath;
            OrderPathLabel.Text = model.OrderPath;
            ReportPathLabel.Text= model.ReportPath;
            RemoteUrlTxt.Text = model.RemoteUrl;
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("用户点击全用户配置-重置到推荐配置按钮,重置后软件存储根目录路径为 {0}," +
                "首页表单数据路径为 {1},导出报告存储路径为 {2},云服务地址设置为 {3}",
                RootPathTextBox.Text, OrderPathLabel.Text, ReportPathLabel.Text, RemoteUrlTxt.Text), pSystem.LogManagement.LogLevel.INFO);

        }

        private void RootPathTextBox_TextChanged(object sender, EventArgs e)
        {
            SaveButton.Enabled = !RootPathTextBox.Text.Equals(_originalModel.RootPath)|| !RemoteUrlTxt.Text.Equals(_originalModel.RemoteUrl);
        }

        private void BrowseEDFButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            var open = new FolderBrowserDialog();
            open.ShowNewFolderButton = false;
            open.RootFolder = Environment.SpecialFolder.MyComputer;
            if (open.ShowDialog() == DialogResult.OK)
            {
                string path = open.SelectedPath;
                RootPathTextBox.Text = path;
                OrderPathLabel.Text = path + GlobalReadonlyString.PredefinedPath.ORDER_PATH;
                ReportPathLabel.Text = path + GlobalReadonlyString.PredefinedPath.REPORT_PATH;
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("用户点击全用户配置-浏览按钮,重新选择后,软件存储根目录路径为 {0}," +
                        "首页表单数据路径为 {1},导出报告存储路径为 {2}",
                        RootPathTextBox.Text, OrderPathLabel.Text, ReportPathLabel.Text), pSystem.LogManagement.LogLevel.INFO);

            }
        }
    }
}
