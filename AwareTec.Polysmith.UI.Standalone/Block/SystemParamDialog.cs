using AwareTec.Polysmith.DataBaseCom;
using AwareTec.Polysmith.UI.DataModel;
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
using AwareTec.Polysmith.UI.FunctionControls.tools;
using AwareTec.Polysmith.Util.PathUtils;
using AwareTec.Polysmith.Vedio;
using ICSharpCode.SharpZipLib.Zip;

namespace AwareTec.Polysmith.UI.Block
{
    public partial class SystemParamDialog : SkinForm
    {
        private string templatePath = AppDomain.CurrentDomain.BaseDirectory + "ReportTemplate\\Sleep\\";
        private bool ischangevideosetting = false;
        /// <summary>
        /// 存放记忆的内容
        /// </summary>
        private string m_MemoryPath = AppDomain.CurrentDomain.BaseDirectory + "Memory.dat";
        private string templatePath2 = AppDomain.CurrentDomain.BaseDirectory + "ReportTemplate\\Breath\\";
        /// <summary>
        /// 存放的视频地址记录
        /// </summary>
        private List<string> bak_VedioPathList = new List<string>();
        public SystemParamDialog()
        {
            this.InitializeComponent();
            base.Load += this.SystemParamDialog_Load;
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Return)
            {
                this.SaveButton_Click(null, null);
                return false;
            }
            if (keyData == Keys.Escape)
            {
                this.CancelButton_Click(null, null);
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
        void BrowseVideoButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new System.Windows.Forms.FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                SaveVideoTextBox.Text = folder.SelectedPath;
            }
        }
        void BrowseReportButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new System.Windows.Forms.FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                SaveReportTextBox.Text = folder.SelectedPath;
            }
        }

        void BrowseEDFButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new System.Windows.Forms.FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                SaveEDFTextBox.Text = folder.SelectedPath;
            }
        }

        void NoToOpenCheckBox_Click(object sender, EventArgs e)
        {
            NoToOpenCheckBox.Checked = true;
            YesToOpenCheckBox.Checked = false;
        }

        void YesToOpenCheckBox_Click(object sender, EventArgs e)
        {
            YesToOpenCheckBox.Checked = true;
            NoToOpenCheckBox.Checked = false;
        }
        void YesToSfCheckBox_Click(object sender, EventArgs e)
        {
            YesToSfCheckBox.Checked = true;
            NoToSfCheckBox.Checked = false;
        }

        void NoToSfCheckBox_Click(object sender, EventArgs e)
        {
            NoToSfCheckBox.Checked = true;
            YesToSfCheckBox.Checked = false;
        }
        void AgeCheckBox_Click(object sender, EventArgs e)
        {
            AgeCheckBox.Checked = true;
            BirthdayCheckBox.Checked = false;
        }

        void BirthdayCheckBox_Click(object sender, EventArgs e)
        {
            BirthdayCheckBox.Checked = true;
            AgeCheckBox.Checked = false;
        }
        private void FullSaveribbonCheckBox_Click(object sender, EventArgs e)
        {
            ischangevideosetting = true;
            FullSaveribbonCheckBox.Checked = true;
            SplitSaveribbonCheckBox.Checked = false;
            label18.Visible = label19.Visible = MaxSpiltVideodungeonTextBox.Visible = false;
        }

        private void SplitSaveribbonCheckBox_Click(object sender, EventArgs e)
        {
            ischangevideosetting = true;
            SplitSaveribbonCheckBox.Checked = true;
            FullSaveribbonCheckBox.Checked = false;
            label18.Visible = label19.Visible = MaxSpiltVideodungeonTextBox.Visible = true;
        }

        #region 记忆内容
        private List<string> bak_RemoteAnalysisList = new List<string>();
        private void readMemory()
        {
            if (File.Exists(m_MemoryPath))
            {
                using (FileStream fs = new FileStream(m_MemoryPath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        StringBuilder sb = new StringBuilder();
                        string line = sr.ReadLine();
                        while (!string.IsNullOrEmpty(line))
                        {
                            sb.AppendFormat("{0}\r\n", line);
                            line = sr.ReadLine();
                        }
                        string[] str = sb.ToString().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        if (str.Length > 0)
                        {
                            for (int i = 0; i < str.Length; i++)
                            {
                                string[] ss = str[i].Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                                if (ss.Length == 2)
                                {
                                    if (ss[0] == "VedioPath")
                                    {
                                        bak_VedioPathList = ss[1].Split(';').ToList();
                                        CreatMemoryComm(CarmeAddressTextBox.DungeonTB, bak_VedioPathList);
                                    }
                                    else if (ss[0] == "RemoteAnalysisPath")
                                    {
                                        bak_RemoteAnalysisList = ss[1].Split(';').ToList();
                                        CreatMemoryComm(TbServerAddress.DungeonTB, bak_RemoteAnalysisList);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void CreatMemoryComm(TextBox c, List<string> context)
        {
            if (context.Count > 0)
            {
                var source = new AutoCompleteStringCollection();
                source.AddRange(context.ToArray());
                c.AutoCompleteCustomSource = source;
                c.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                c.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
        }
        private void writeMemory()
        {
            bool f = false, f2 = false;
            string context = CarmeAddressTextBox.Text.Trim();
            if (context != "")
            {
                if (!bak_VedioPathList.Contains(context))
                {
                    bak_VedioPathList.Insert(0, context);
                    f = true;
                }
            }
            context = TbServerAddress.Text.Trim();
            if (context != "")
            {
                if (!bak_RemoteAnalysisList.Contains(context))
                {
                    bak_RemoteAnalysisList.Insert(0, context);
                    f2 = true;
                }
            }
            if (f || f2)
            {
                using (FileStream fs = new FileStream(m_MemoryPath, FileMode.Create))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("[VedioPath]{0}\r\n", string.Join(";", bak_VedioPathList));
                        sb.AppendFormat("[RemoteAnalysisPath]{0}\r\n", string.Join(";", bak_RemoteAnalysisList));
                        sw.Write(sb.ToString());
                        sw.AutoFlush = true;
                        sw.Close();
                    }
                    fs.Close();
                }
            }
        }
        #endregion
        private void SystemParamDialog_Load(object sender, EventArgs e)
        {
            readMemory();
            bool flag = Channel.Default.SystemSetting.ReportType.ToLower() == "excel";
            string[] files = Directory.GetFiles(this.templatePath, flag ? "*.xlsx" : "*.doc*");
            for (int i = 0; i < files.Length; i++)
            {
                this.AllSleepCombBox.Items.Add(Path.GetFileName(files[i]));
                if (this.AllSleepCombBox.Items.Count > 0)
                {
                    this.AllSleepCombBox.SelectedIndex = 0;
                }
            }
            files = Directory.GetFiles(this.templatePath2, flag ? "*.xlsx" : "*.doc*");
            for (int j = 0; j < files.Length; j++)
            {
                this.BreathFormCombBox.Items.Add(Path.GetFileName(files[j]));
                if (this.BreathFormCombBox.Items.Count > 0)
                {
                    this.BreathFormCombBox.SelectedIndex = 0;
                }
            }
            this.load();
        }
        private void load()
        {
            for (int i = 0; i < this.ReportFormCombBox.Items.Count; i++)
            {
                if (this.ReportFormCombBox.Items[i].ToString().ToLower() == Channel.Default.SystemSetting.ReportType.ToLower())
                {
                    this.ReportFormCombBox.SelectedIndex = i;
                    break;
                }
            }
            this.SaveEDFTextBox.Text = Channel.Default.SystemSetting.SaveEdfPath;
            this.SaveReportTextBox.Text = Channel.Default.SystemSetting.ReportPath;
            this.SaveVideoTextBox.Text = Channel.Default.SystemSetting.VedioSavePath;
            this.AllSleepCombBox.Text = Path.GetFileName(Channel.Default.SystemSetting.ReportTemplate);
            this.BreathFormCombBox.Text = Path.GetFileName(Channel.Default.SystemSetting.ReportTemplate2);
            this.CbSO2Drop.Text = string.Format("{0} %", Channel.Default.SystemSetting.OxygenReduceRange);
            this.numericUpDown1.Value = Channel.Default.SystemSetting.PlaySpanTime;
            this.YesToOpenCheckBox.Checked = Channel.Default.SystemSetting.AutoRunReport;
            this.NoToOpenCheckBox.Checked = !this.YesToOpenCheckBox.Checked;
            this.YesToSfCheckBox.Checked = Channel.Default.SystemSetting.AnalysisBeforeReport;
            this.NoToSfCheckBox.Checked = !this.YesToSfCheckBox.Checked;
            this.BirthdayCheckBox.Checked = Channel.Default.SystemSetting.UseBirthdayEnable;
            this.AgeCheckBox.Checked = !this.BirthdayCheckBox.Checked;
            this.FullSaveribbonCheckBox.Checked = Channel.Default.SystemSetting.NotSplitRecordFile;
            this.SplitSaveribbonCheckBox.Checked = !this.FullSaveribbonCheckBox.Checked;
            label18.Visible = label19.Visible = MaxSpiltVideodungeonTextBox.Visible = !this.FullSaveribbonCheckBox.Checked;
            //this.UserAnaTableCheckBox.Checked = Channel.Default.SystemSetting.AllowRemoteAnalysis;
            this.TbServerAddress.Text = Channel.Default.SystemSetting.RemoteServerAddr;
            this.CarmeAddressTextBox.Text = Channel.Default.SystemSetting.VedioSourceUrl;
            this.minSpo2TextBox.Text = Channel.Default.SystemSetting.Reserve1;
            this.enLocalVideoCheckBox.Checked = Channel.Default.SystemSetting.Reserve2 == "1" ? true : false;
            this.hospitalNameTextBox.Text = Channel.Default.SystemSetting.Reserve3;
            this.MaxSpiltVideodungeonTextBox.Text = Channel.Default.SystemSetting.MaxRecodFileLenght.ToString();
            ischangevideosetting = false;
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if(SplitSaveribbonCheckBox.Checked&&(Convert.ToInt32(MaxSpiltVideodungeonTextBox.Text)<100|| Convert.ToInt32(MaxSpiltVideodungeonTextBox.Text) > 1024))
            {
                AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "Video slice storage should be between 100 and 1024 MB in size per slice" : "视频切片存储 每片大小应在 100~1024 MB之间");
                return;
            }
            Doc_SystemSetting doc_SystemSetting = new Doc_SystemSetting();
            doc_SystemSetting = new Doc_SystemSetting
            {
                SaveEdfPath = this.SaveEDFTextBox.Text,
                ReportPath = this.SaveReportTextBox.Text,
                ReportTemplate = this.templatePath + this.AllSleepCombBox.Text,
                ReportTemplate2 = this.templatePath2 + this.BreathFormCombBox.Text,
                OxygenReduceRange = this.CbSO2Drop.Text.Replace(" %", ""),
                ReportType = this.ReportFormCombBox.Text,
                PlaySpanTime = (int)this.numericUpDown1.Value,
                // AllowRemoteAnalysis = this.UserAnaTableCheckBox.Checked,
                AutoRunReport = this.YesToOpenCheckBox.Checked,
                RemoteServerAddr = this.TbServerAddress.Text,
                VedioSavePath = this.SaveVideoTextBox.Text,
                VedioSourceUrl = this.CarmeAddressTextBox.Text,
                AnalysisBeforeReport = this.YesToSfCheckBox.Checked,
                UseBirthdayEnable = this.BirthdayCheckBox.Checked,
                ModeType = Channel.Default.SystemSetting.ModeType,
                Reserve1 = this.minSpo2TextBox.Text.Trim(),
                Reserve2 = this.enLocalVideoCheckBox.Checked ? "1" : "0",
                Reserve3 = this.hospitalNameTextBox.Text,
                NotSplitRecordFile = SplitSaveribbonCheckBox.Checked ? false : true,
                MaxRecodFileLenght = SplitSaveribbonCheckBox.Checked ? Convert.ToInt32(this.MaxSpiltVideodungeonTextBox.Text) : Channel.Default.SystemSetting.MaxRecodFileLenght
            };
            writeMemory();
            if (!DataBaseHelper.Default.Update(new Doc_SystemSetting
            {
                ID = Channel.Default.SystemSetting.ID
            }, doc_SystemSetting))
            {
                AhDung.MessageTip.ShowError(Program.Language == "EN" ? "Modification failed, database exception!" : "修改失败，数据库异常！", -1, null, null, false);
                DataModel.LogInstance.Default.AddLog("用户点击 系统设置-保存按钮 修改失败，数据库异常！", pSystem.LogManagement.LogLevel.ERROR);
                return;
            }
            if (doc_SystemSetting.OxygenReduceRange != Channel.Default.SystemSetting.OxygenReduceRange)
            {
                pChart.IMarker marker = MarkerManage.Default.DefineMarkers.Find((pChart.IMarker t) => t.MarkTyp == pChart.IMarker.MarkType.OxygenReduce);
                if (marker != null)
                {
                    marker.Description = marker.Description.Replace(string.Format("{0}%", Channel.Default.SystemSetting.OxygenReduceRange), string.Format("{0}%", doc_SystemSetting.OxygenReduceRange));
                    DataBaseHelper.Default.Update(new Doc_EventsDefine
                    {
                        Name = marker.Name,
                        ModeType = Channel.Default.SystemSetting.ModeType
                    },
                                                new Doc_EventsDefine
                                                {
                                                    Description = marker.Description
                                                });
                }
            }
            if (doc_SystemSetting.AllowRemoteAnalysis != Channel.Default.SystemSetting.AllowRemoteAnalysis)
            {
                if (MessageForm.Show(Program.Language == "EN" ? "Special configuration has been modified and needs to be logged in again to take effect. Do you want to restart immediately?" : "特殊配置被修改，需要重新登录生效，是否立即重启？", Program.Language == "EN" ? "Information" : "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataModel.LogInstance.Default.AddLog("用户点击 系统设置-保存按钮 特殊配置被修改，确认立即重启，重新登录");
                    Program.IsLogin = true;
                    base.Owner.Close();
                }
            }
            else if (doc_SystemSetting.AllowRemoteAnalysis && doc_SystemSetting.RemoteServerAddr != Channel.Default.SystemSetting.RemoteServerAddr)
            {
                if (MessageForm.Show(Program.Language == "EN" ? "Special configuration has been modified and needs to be logged in again to take effect. Do you want to restart immediately?" : "特殊配置被修改，需要重新登录生效，是否立即重启？", Program.Language == "EN" ? "Information" : "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataModel.LogInstance.Default.AddLog("用户点击 系统设置-保存按钮 特殊配置被修改，确认立即重启，重新登录");
                    Program.IsLogin = true;
                    base.Owner.Close();
                }
            }
            else
            {
                doc_SystemSetting.ID = Channel.Default.SystemSetting.ID;
                Channel.Default.SystemSetting = doc_SystemSetting;
                VedioManagement.Default.NotSplitRecordFile = this.FullSaveribbonCheckBox.Checked;
                VedioManagement.Default.MaxRecodFileLenght = Convert.ToInt32(this.MaxSpiltVideodungeonTextBox.Text);
                if (ischangevideosetting)
                {
                    AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "The video configuration has been modified and needs to be logged in again to take effect" : "视频配置被修改，需要重新登录生效");
                }
                else
                {
                    AhDung.MessageTip.ShowOk(Program.Language=="EN"? "The configuration has taken effect!" : "配置已生效！", -1, null, null, false);
                }
                DataModel.LogInstance.Default.AddLog(string.Format("用户点击 系统设置-保存按钮 配置已生效，edf数据存储路径为 {0}，诊断报告输出路径为 {1}，视频录像存储路径为 {2}，摄像头资源地址为 {3} ",
                                                        StringPath.ConvertLogPath(SaveEDFTextBox.Text),
                                                        StringPath.ConvertLogPath(SaveReportTextBox.Text),
                                                        StringPath.ConvertLogPath(SaveVideoTextBox.Text),
                                                        CarmeAddressTextBox.Text), pSystem.LogManagement.LogLevel.INFO);
            }
            base.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DataModel.LogInstance.Default.AddLog("用户点击 系统设置-取消按钮");
            this.Close();
        }
        private int bak_selectIndex = 0;
        private void ReportFormCombBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bak_selectIndex != ReportFormCombBox.SelectedIndex)
            {
                int idx = bak_selectIndex;
                bak_selectIndex = ReportFormCombBox.SelectedIndex;
                bool flag = ReportFormCombBox.SelectedIndex == 3;
                if (idx < 3 && !flag)
                    return;
                AllSleepCombBox.Items.Clear();
                BreathFormCombBox.Items.Clear();
                string[] files = Directory.GetFiles(this.templatePath, flag ? "*.xlsx" : "*.doc*");
                for (int i = 0; i < files.Length; i++)
                {
                    this.AllSleepCombBox.Items.Add(Path.GetFileName(files[i]));
                }
                files = Directory.GetFiles(this.templatePath2, flag ? "*.xlsx" : "*.doc*");
                for (int j = 0; j < files.Length; j++)
                {
                    this.BreathFormCombBox.Items.Add(Path.GetFileName(files[j]));
                }
            }
        }

        private void UserAnaTableCheckBox_CheckedChanged(object sender)
        {
            label8.Visible = UserAnaTableCheckBox.Checked;
            TbServerAddress.Visible = UserAnaTableCheckBox.Checked;
        }

        private void AgeCheckBox_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void HeightTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataModel.InputTextCheck.floatvalue_KeyPress(sender, e);
        }

        private void MaxSpiltVideodungeonTextBox_TextChanged(object sender, EventArgs e)
        {
            ischangevideosetting = true;
        }

        private void CarmeAddressTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
