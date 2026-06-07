using AwareTec.Polysmith.Cloud.UI.DataModels;
using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.SpecificXmlHelper;
using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.User;
using AwareTec.Polysmith.Cloud.UI.DataModels.Global;
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

namespace AwareTec.Polysmith.Cloud.UI.Forms.MainFormBlock
{
    public partial class OutReportSelection : CloudSkinForm
    {
        #region 私有变量
        private UserOperationConfig userOperationConfig = null;
        private static OutReportSelection m_default;
        private string m_BreathReportPath = AppDomain.CurrentDomain.BaseDirectory + "ReportTemplate\\Breath";
        private string m_ReportPath = AppDomain.CurrentDomain.BaseDirectory + "ReportTemplate\\Sleep";
        private string m_BreathReportName = "";
        private string m_ReportName = "";
        private string bak_BreathReportName = "";
        private string bak_ReportName = "";
        private bool m_init;
        private bool m_userClick = true;
        private string m_SavePath = "";
        private string m_ReportType = "";
        private string m_ReportTempalteName = "";
        private bool m_IsBreathOnly;

        #endregion

        #region 公有变量

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

        public string SavePath
        {
            get => m_SavePath;
        }

        public string ReportType
        {
            get => m_ReportType;
        }

        public string ReportTempalteName
        {
            get => m_ReportTempalteName;
        }

        public bool IsBreathOnly
        {
            get => m_IsBreathOnly;
        }

        #endregion

        #region 初始化 load/dispose/init

        /// <summary>
        /// 初始化
        /// </summary>
        public OutReportSelection()
        {
            InitializeComponent();
            this.Load += OutReportSelection_Load;
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OutReportSelection_Load(object sender, EventArgs e)
        {
            this.BrowseButton.Click += BrowseButton_Click;
            this.CancelButton.Click += CancelButton_Click;
            this.SubmitButton.Click += SubmitButton_Click;
            ReportModelCombBox.DrawItem += ComboBox_DrawItem;
            ReportFormCombBox.DrawItem += ComboBox_DrawItem;
            ReportModelCombBox.SelectedIndexChanged += ReportModelCombBox_SelectedIndexChanged;
            ReportFormCombBox.SelectedIndexChanged += ReportFormCombBox_SelectedIndexChanged;
            RaBAllSleep.CheckedChanged += RaBAllSleep_CheckedChanged;
            BreathRadioButton.CheckedChanged += RaBAllSleep_CheckedChanged;

            ReportModelCombBox.ItemHeight = ReportFormCombBox.ItemHeight = 20;
            ReportModelCombBox.DrawMode = DrawMode.OwnerDrawVariable;
            ReportFormCombBox.DrawMode = DrawMode.OwnerDrawVariable;

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
                    this.m_userClick = false;
                    this.BreathRadioButton.Checked = bool.Parse(array[1]);
                    this.RaBAllSleep.Checked = !this.BreathRadioButton.Checked;
                    this.m_SavePath = array[0];
                    this.PathTextBox.Text = string.Format("{0}\\{1:yyyy-MM-dd}", this.m_SavePath, Channel.Default.StartTime);
                    this.m_init = true;
                    this.ReportFormCombBox.SelectedItem = array[2];
                    this.ReportModelCombBox.SelectedItem = (this.RaBAllSleep.Checked ? this.m_ReportName : this.m_BreathReportName);
                    this.m_userClick = true;
                }
                return;
            }
            userOperationConfig = GlobalSingleton.Instance.User.UserConfig;

            var fullUserConfig = GlobalSingleton.Instance.getSystemSetting();

            if (!string.IsNullOrEmpty(fullUserConfig.ReportPath))
            {
                this.m_SavePath = fullUserConfig.ReportPath;
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
                if (userOperationConfig.LastExportedReportFormat.ToLower() == b)
                {
                    this.ReportFormCombBox.SelectedIndex = i;
                    //this.ReportFormCombBox.SelectedItem = this.ReportFormCombBox.Items[i];
                    break;
                }
            }
            this.m_ReportName = Path.GetFileName(userOperationConfig.LastExportedSleepReportName);
            this.m_BreathReportName = Path.GetFileName(userOperationConfig.LastExportedBreathReportName);
            this.ReportModelCombBox.SelectedItem = (this.RaBAllSleep.Checked ? this.m_ReportName : this.m_BreathReportName);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 键盘快捷键
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 报告类型 全睡眠还是呼吸筛查 选中状态改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RaBAllSleep_CheckedChanged(object sender, EventArgs e)
        {
            this.BreathRadioButton.Checked = !this.RaBAllSleep.Checked;
            this.CheckReportTyp();
            if (this.m_userClick)
            {
                this.ReportModelCombBox.Text = (this.RaBAllSleep.Checked ? this.m_ReportName : this.m_BreathReportName);
            }
            this.m_userClick = true;
        }

        /// <summary>
        /// 检查报告类型
        /// </summary>
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

        /// <summary>
        /// 下拉框文本内容的描绘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
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

        /// <summary>
        /// 报告格式下拉框 选择项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportFormCombBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CheckReportTyp();
            this.ReportModelCombBox.Text = (this.RaBAllSleep.Checked ? this.m_ReportName : this.m_BreathReportName);
        }

        /// <summary>
        /// 报告模版下拉框 选择项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportModelCombBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.RaBAllSleep.Checked)
            {
                this.m_ReportName = this.ReportModelCombBox.SelectedItem.ToString();
                return;
            }
            this.m_BreathReportName = this.ReportModelCombBox.SelectedItem.ToString();
        }

        #endregion

        #region 按钮方法

        /// <summary>
        /// 导出报告 提交按钮 点击
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
            ///todo 保存最后一次报告导出的相关参数配置 应该好了
            var xmlHelper = new UserOperationConfigXmlHelper(GlobalSingleton.Instance.User.ConfigPath);
            var model = xmlHelper.Read();
            model.LastExportedReportFormat = ReportFormCombBox.Text;
            model.LastExportedSleepReportName = RaBAllSleep.Checked ? ReportModelCombBox.Text : "";
            model.LastExportedBreathReportName = RaBAllSleep.Checked ? "" : ReportModelCombBox.Text;
            xmlHelper.Modify(model);
            //DataBaseHelper.Default.Update(new Doc_MainViewRecord
            //{
            //    ID = Convert.ToInt32(Channel.Default.AnalysisReult.Tag)
            //}, new Doc_MainViewRecord
            //{
            //    Reserve1 = Channel.Default.strRecordReportConfig
            //});
            this.m_ReportTempalteName = string.Format("{0}\\{1}", this.m_IsBreathOnly ? this.m_BreathReportPath : this.m_ReportPath, this.m_ReportTempalteName);
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("用户点击 导出报告-提交按钮， 输出路径为 {0}， 报告类型为 {1}， 报告格式为 {2}， 报告模版为 {3}", StringPath.ConvertLogPath(PathTextBox.Text), RaBAllSleep.Checked ? "全睡眠" : "呼吸筛查", this.ReportFormCombBox.Text, ReportModelCombBox.Text));
            base.Close();
        }

        /// <summary>
        /// 导出报告 取消按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("用户点击 导出报告-取消按钮");
            base.DialogResult = DialogResult.Cancel;
            base.Close();
        }

        /// <summary>
        /// 导出报告 浏览按钮 点击
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
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("用户点击 导出报告-浏览按钮，选择的输出路径为 {0}", StringPath.ConvertLogPath(this.PathTextBox.Text)));
            }
        }

        #endregion

        #region 公有方法

        #endregion

    }
}
