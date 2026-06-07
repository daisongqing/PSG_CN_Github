using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.FullUser;
using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.User;
using AwareTec.Polysmith.Cloud.UI.DataModels.Global;
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

namespace AwareTec.Polysmith.Cloud.UI.Forms.ConfigurationBlock
{
    public partial class UserOperationConfiguration : CloudSkinForm
    {
        private UserOperationConfig userOperationConfig = null;
        public UserOperationConfiguration()
        {
            InitializeComponent();
            userOperationConfig = GlobalSingleton.Instance.User.UserConfig;

            var fullUserConfig = GlobalSingleton.Instance.getSystemSetting();
            this.Load += UserOperationConfiguration_Load;
            this.saveButton.Click += SaveButton_Click;
        }

        private void UserOperationConfiguration_Load(object sender, EventArgs e)
        {
            binddevTextBox.Text = userOperationConfig.LastBoundDevice;
            foxRadioButton2.Checked = userOperationConfig.TypeOfLastScan == DataModels.EnumModels.ScanMethod.Local;
            statusWordText.Text = userOperationConfig.LastConfiguredAnalysisParameters.ToString();
            watermarkComboBox1.Text = userOperationConfig.LastExportedReportFormat;
            sleepReportTemplateTxt.Text = Path.GetFileName(userOperationConfig.LastExportedSleepReportName);
            breathReportTemplateTxt.Text = Path.GetFileName(userOperationConfig.LastExportedBreathReportName);
            montageSelectTxt.Text = Path.GetFileName(userOperationConfig.LastSelectedScheme);
            numericUpDown1.Value = userOperationConfig.AutomaticScrollingInterval;
            autoRadioButton1.Checked = userOperationConfig.AutoOpenReport;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            AhDung.MessageTip.ShowOk("保存成功");
            this.Close();
        }
    }
}
