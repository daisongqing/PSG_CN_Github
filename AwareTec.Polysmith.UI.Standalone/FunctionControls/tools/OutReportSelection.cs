using AwareTec.Polysmith.DataBaseCom;
using AwareTec.Polysmith.UI.DataModel;
using AwareTec.Polysmith.Util.PathUtils;
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

namespace AwareTec.Polysmith.UI.FunctionControls.tools
{
    public partial class OutReportSelection : SkinForm
    {
        private static OutReportSelection m_default;
        private string m_BreathReportPath = AppDomain.CurrentDomain.BaseDirectory + "ReportTemplate\\Breath";
        private string m_ReportPath = AppDomain.CurrentDomain.BaseDirectory + "ReportTemplate\\Sleep";
        private string m_BreathReportName = "";
        private string m_ReportName = "";
        private string bak_BreathReportName = "";
        private string bak_ReportName = "";
        private bool m_init;
        private bool userClick = true;
        private string m_SavePath = "";
        private string m_ReportType = "";
        private string m_ReportTempalteName = "";
        private bool m_IsBreathOnly;
        public static OutReportSelection Defalut
        {
            get
            {
                OutReportSelection result;
                if ((result = OutReportSelection.m_default) == null)
                {
                    result = (OutReportSelection.m_default = new OutReportSelection());
                }
                return result;
            }
        }
        public OutReportSelection()
        {
            this.InitializeComponent();
            base.Load += this.ProgressTipForm_Load;
            base.FormClosed += this.ProgressTipForm_FormClosed;
            ReportModelCombBox.ItemHeight = ReportFormCombBox.ItemHeight = 20;
            ReportModelCombBox.DrawMode = DrawMode.OwnerDrawVariable;
            ReportFormCombBox.DrawMode = DrawMode.OwnerDrawVariable;
            ReportModelCombBox.DrawItem += ComboBox1_DrawItem;
            ReportFormCombBox.DrawItem += ComboBox1_DrawItem;
            ReportModelCombBox.SelectedIndexChanged += ReportModelCombBox_SelectedIndexChanged;
            ReportFormCombBox.SelectedIndexChanged += ReportFormCombBox_SelectedIndexChanged;
            RaBAllSleep.CheckedChanged += RaBAllSleep_CheckedChanged;
            BreathRadioButton.CheckedChanged += RaBAllSleep_CheckedChanged;
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Return)
            {
                this.SubmitButton_Click(null, null);
                return true;
            }
            if (keyData == Keys.Escape)
            {
                this.CancelButton_Click(null, null);
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
        private void ProgressTipForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void ProgressTipForm_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Channel.Default.strRecordReportConfig))
            {
                string[] array = Channel.Default.strRecordReportConfig.Split(new char[]
				{
					';'
				});
                if (array.Length > 3)
                {
                    string[] array2 = array[3].Split(new char[]
					{
						'/'
					});
                    this.m_ReportName = (this.bak_ReportName = array2[0]);
                    if (array2.Length > 1)
                    {
                        this.m_BreathReportName = (this.bak_BreathReportName = array2[1]);
                    }
                    this.userClick = false;
                    this.BreathRadioButton.Checked = bool.Parse(array[1]);
                    this.RaBAllSleep.Checked = !this.BreathRadioButton.Checked;
                    this.m_SavePath = array[0];
                    this.PathTextBox.Text = string.Format("{0}\\{1:yyyy-MM-dd}", this.m_SavePath, Channel.Default.StartTime);
                    this.m_init = true;
                    this.ReportFormCombBox.SelectedItem = array[2];
                    this.ReportModelCombBox.SelectedItem = (this.RaBAllSleep.Checked ? this.m_ReportName : this.m_BreathReportName);
                    this.userClick = true;
                }
                return;
            }
            if (!string.IsNullOrEmpty(Channel.Default.SystemSetting.ReportPath))
            {
                this.m_SavePath = Channel.Default.SystemSetting.ReportPath;
                this.PathTextBox.Text = string.Format("{0}\\{1:yyyy-MM-dd}", this.m_SavePath, Channel.Default.StartTime);
            }
            else
            {
                this.m_SavePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                this.PathTextBox.Text = string.Format("{0}\\{1:yyyy-MM-dd}", this.m_SavePath, Channel.Default.StartTime);
            }
            this.BreathRadioButton.Checked = Channel.Default.IsBreathOnly;
            this.RaBAllSleep.Checked = !this.BreathRadioButton.Checked;
            this.m_init = true;
            for (int i = 0; i < this.ReportFormCombBox.Items.Count; i++)
            {
                string b = this.ReportFormCombBox.Items[i].ToString().ToLower();
                if (Channel.Default.SystemSetting.ReportType.ToLower() == b)
                {
                    this.ReportFormCombBox.SelectedItem = this.ReportFormCombBox.Items[i];
                    break;
                }
            }
            this.m_ReportName = Path.GetFileName(Channel.Default.SystemSetting.ReportTemplate);
            this.m_BreathReportName = Path.GetFileName(Channel.Default.SystemSetting.ReportTemplate2);
            this.ReportModelCombBox.SelectedItem = (this.RaBAllSleep.Checked ? this.m_ReportName : this.m_BreathReportName);
        }

        private void ComboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox com = (ComboBox)sender;
            if (e.Index < 0)
            {

                return;

            }

            e.DrawBackground();

            e.DrawFocusRectangle();

            e.Graphics.DrawString(com.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds.X, e.Bounds.Y + 3);

        }
        private void ReportFormCombBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CheckReportTyp();
            this.ReportModelCombBox.Text = (this.RaBAllSleep.Checked ? this.m_ReportName : this.m_BreathReportName);
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            DataModel.LogInstance.Default.AddLog("用户点击 导出报告-取消按钮");
            base.DialogResult = DialogResult.Cancel;
            base.Close();
        }
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
            if (this.m_SavePath == "")
            {
                AhDung.MessageTip.ShowWarning("未选择报告导出路径！", -1, null, null, false);
                return;
            }
            this.m_ReportTempalteName = this.ReportModelCombBox.Text;
            if (this.m_ReportTempalteName == "")
            {
                AhDung.MessageTip.ShowWarning("未选择报告模板！", -1, null, null, false);
                return;
            }
            this.m_IsBreathOnly = this.BreathRadioButton.Checked;
            this.m_ReportType = this.ReportFormCombBox.Text;
            Channel.Default.strRecordReportConfig = string.Format("{0};{1};{2};{3}/{4}", new object[]
			{
				this.m_SavePath,
				this.m_IsBreathOnly,
				this.ReportFormCombBox.Text,
				this.m_IsBreathOnly ? this.bak_ReportName : this.m_ReportTempalteName,
				this.m_IsBreathOnly ? this.m_BreathReportName : this.bak_BreathReportName
			});
            DataBaseHelper.Default.Update(new Doc_MainViewRecord
            {
                ID = Convert.ToInt32(Channel.Default.AnalysisReult.Tag)
            }, new Doc_MainViewRecord
            {
                Reserve1 = Channel.Default.strRecordReportConfig
            });
            this.m_ReportTempalteName = string.Format("{0}\\{1}", this.m_IsBreathOnly ? this.m_BreathReportPath : this.m_ReportPath, this.m_ReportTempalteName);
            DataModel.LogInstance.Default.AddLog(string.Format("用户点击 导出报告-提交按钮， 输出路径为 {0}， 报告类型为 {1}， 报告格式为 {2}， 报告模版为 {3}", StringPath.ConvertLogPath(PathTextBox.Text), RaBAllSleep.Checked?"全睡眠":"呼吸筛查",this.ReportFormCombBox.Text, ReportModelCombBox.Text));
            base.Close();
        }
        /// <summary>
        /// 浏览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                this.m_SavePath = folderBrowserDialog.SelectedPath;
                this.PathTextBox.Text = string.Format("{0}\\{1:yyyy-MM-dd}", this.m_SavePath, Channel.Default.StartTime);
                DataModel.LogInstance.Default.AddLog(string.Format("用户点击 导出报告-浏览按钮，选择的输出路径为 {0}", StringPath.ConvertLogPath(this.PathTextBox.Text)));
            }
        }

        private void ReportModelCombBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.RaBAllSleep.Checked)
            {
                this.m_ReportName = this.ReportModelCombBox.SelectedItem.ToString();
                return;
            }
            this.m_BreathReportName = this.ReportModelCombBox.SelectedItem.ToString();
        }
        private void RaBAllSleep_CheckedChanged(object sender, EventArgs e)
        {
            this.BreathRadioButton.Checked = !this.RaBAllSleep.Checked;
            this.CheckReportTyp();
            if (this.userClick)
            {
                this.ReportModelCombBox.Text = (this.RaBAllSleep.Checked ? this.m_ReportName : this.m_BreathReportName);
            }
            this.userClick = true;
        }
        private void CheckReportTyp()
        {
            if (this.m_init)
            {
                if (this.ReportFormCombBox.SelectedItem == null)
                    return;
                string[] files;
                if (this.ReportFormCombBox.SelectedItem.ToString().ToLower() == "excel")
                {
                    if (this.RaBAllSleep.Checked)
                    {
                        files = Directory.GetFiles(this.m_ReportPath, "*.xlsx");
                    }
                    else
                    {
                        files = Directory.GetFiles(this.m_BreathReportPath, "*.xlsx");
                    }
                }
                else if (this.RaBAllSleep.Checked)
                {
                    string[] search = new string[] { ".doc", ".docx" };
                    files = Directory.GetFiles(this.m_ReportPath, "*.*").Where(t => search.Contains(Path.GetExtension(t).ToLower())).ToArray();
                }
                else
                {
                    string[] search = new string[] { ".doc", ".docx" };
                    files = Directory.GetFiles(this.m_BreathReportPath, "*.*").Where(t => search.Contains(Path.GetExtension(t).ToLower())).ToArray();
                }
                this.ReportModelCombBox.Items.Clear();
                string[] items = (from t in files
                                  select Path.GetFileName(t)).ToArray<string>();
                this.ReportModelCombBox.Items.AddRange(items);
                if (items.Length == 1)
                {
                    ReportModelCombBox.SelectedIndex = 0;
                }
            }
        }

        public string SavePath
        {
            get
            {
                return this.m_SavePath;
            }
        }

        public string ReportType
        {
            get
            {
                return this.m_ReportType;
            }
        }

        public string ReportTempalteName
        {
            get
            {
                return this.m_ReportTempalteName;
            }
        }

        public bool IsBreathOnly
        {
            get
            {
                return this.m_IsBreathOnly;
            }
        }
    }
}
