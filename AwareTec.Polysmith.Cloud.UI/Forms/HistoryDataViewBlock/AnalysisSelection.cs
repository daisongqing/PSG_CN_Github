using AwareTec.Polysmith.Cloud.UI.DataModels;
using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.SpecificXmlHelper;
using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.User;
using AwareTec.Polysmith.Cloud.UI.DataModels.Global;
using AwareTec.Polysmith.Cloud.UI.Views;
using System;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;

namespace AwareTec.Polysmith.Cloud.UI.Forms.MainFormBlock
{
    public partial class AnalysisSelection : CloudSkinForm
    {
        #region 私有变量
        private UserOperationConfig m_userOperationConfig = null;
        private static AnalysisSelection m_default = null;
        private bool isload = false;
        private Views.ArousalSelection aSelection = null;
        private Views.SleepStageSelection sSelection = null;
        private Views.Spo2RangeSelection spo2Selection = null;

        #endregion

        #region 公有变量

        public static AnalysisSelection Defalut
        {
            get
            {
                return m_default ?? (m_default = new AnalysisSelection());
            }
        }

        #endregion

        #region 初始化 load/dispose/init

        /// <summary>
        /// 初始化
        /// </summary>
        public AnalysisSelection()
        {
            InitializeComponent();
            this.Load += AnalysisSelection_Load;
            this.FormClosed += AnalysisSelection_FormClosed;
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnalysisSelection_Load(object sender, EventArgs e)
        {
            this.CancelButton.Click += CancelButton_Click;
            this.SubmitButton.Click += SubmitButton_Click;
            this.SectleSleepModeRadBut.CheckedChanged += SectleSleepModeRadBut_CheckedChanged;
            this.radioButton2.CheckedChanged += SectleSleepModeRadBut_CheckedChanged;
            this.AllSelectCheckBox.CheckedChanged += AllSelectCheckBox_CheckedChanged;
            this.LowHCheckBox.CheckedChanged += LowHCheckBox_CheckedChanged;
            this.O2CheckBox.CheckedChanged += O2CheckBox_CheckedChanged;
            this.SmallWakeCheckBox.CheckedChanged += SmallWakeCheckBox_CheckedChanged;
            this.SleepDevCheckBox.CheckedChanged += SleepDevCheckBox_CheckedChanged;
            m_userOperationConfig = GlobalSingleton.Instance.User.UserConfig;
            int word = (int)m_userOperationConfig.LastConfiguredAnalysisParameters;
            string[] allowshows = Channel.Default.IsBreathOnly ? radioButton2.Tag.ToString().Split('\\') : SectleSleepModeRadBut.Tag.ToString().Split('\\');
            foreach (Control c in panel1.Controls)
            {
                if (c is pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox)
                {
                    if (c.Name != AllSelectCheckBox.Name && c.Enabled)
                    {
                        if (allowshows.Contains(c.Tag))
                        {
                            (c as pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox).Checked = ((word >> (Convert.ToByte(c.Tag))) & 0x01) == 1;
                        }
                        else
                        {
                            (c as pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox).Checked = false;
                            //c.Enabled = false;
                        }
                    }
                }
            }
            if (word != 0x3F)
            {
                AllSelectCheckBox.Checked = false;
            }
            this.aSelection = new ArousalSelection
            {
                Value =  Channel.Default.ArousalDataSourcePlan
            };
            this.sSelection = new SleepStageSelection
            {
                Value = Channel.Default.SleepStageSourcePlan
            };
            this.spo2Selection = new Spo2RangeSelection
            {
                Value = Channel.Default.OxygenReduceRange
            };
            this.pictrueBoxEx1.SetPopupControl(this.aSelection);
            this.pictrueBoxEx2.SetPopupControl(this.sSelection);
            this.pictrueBoxEx3.SetPopupControl(this.spo2Selection);
            this.radioButton2.Checked = Channel.Default.IsBreathOnly;
            base.Move += this.AnalysisSelection_Move;
            this.isload = true;
        }

        /// <summary>
        /// 窗口移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnalysisSelection_Move(object sender, EventArgs e)
        {
            this.pictrueBoxEx1.ReLocation();
            this.pictrueBoxEx2.ReLocation();
            this.pictrueBoxEx3.ReLocation();
        }

        /// <summary>
        /// 窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnalysisSelection_FormClosed(object sender, FormClosedEventArgs e)
        {
            (new UserOperationConfigXmlHelper(GlobalSingleton.Instance.User.ConfigPath)).Modify(m_userOperationConfig);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 全睡眠还是初筛模式 单选按钮 的改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectleSleepModeRadBut_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (!radioButton.Checked)
                return;
            string text = radioButton.Tag as string;
            string[] source = text.Split(new char[]
            {
                '\\'
            });
            foreach (object obj in this.panel1.Controls)
            {
                Control control = (Control)obj;
                if (control is pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox && control.Name != this.AllSelectCheckBox.Name)
                {
                    if (source.Contains(control.Tag))
                    {
                        control.Visible = true;
                        if (this.AllSelectCheckBox.Checked)
                        {
                            (control as pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox).Checked = true;
                        }
                    }
                    else if (radioButton == this.radioButton2)
                    {
                        control.Visible = false;
                        (control as pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox).Checked = false;
                    }
                    else
                    {
                        control.Visible = true;
                    }
                    if (control.Name == this.SleepDevCheckBox.Name)
                    {
                        Channel.Default.IsBreathOnly = !control.Visible;
                        if (!Channel.Default.IsBreathOnly)
                        {
                            control.Enabled = true;
                        }
                        this.label2.Visible = control.Visible;
                    }
                }
            }
        }

        /// <summary>
        /// 全选 勾选框状态改变
        /// </summary>
        /// <param name="sender"></param>
        private void AllSelectCheckBox_CheckedChanged(object sender)
        {
            if (!isload)
                return;
            bool state = AllSelectCheckBox.Checked;
            foreach (Control c in panel1.Controls)
            {
                if (c is pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox)
                {
                    if (c.Name != AllSelectCheckBox.Name && c.Enabled && c.Visible)
                    {
                        (c as pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox).Checked = state;
                    }
                }
            }
        }

        /// <summary>
        /// 低通气 特殊处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LowHCheckBox_CheckedChanged(object sender)
        {
            CACheckBox.Checked = OACheckBox.Checked = MACheckBox.Checked = LowHCheckBox.Checked;
        }

        /// <summary>
        /// 氧减 勾选框状态改变
        /// </summary>
        /// <param name="sender"></param>
        private void O2CheckBox_CheckedChanged(object sender)
        {
            this.pictrueBoxEx3.Visible = this.O2CheckBox.Checked;
        }

        /// <summary>
        /// 觉醒 勾选框状态改变
        /// </summary>
        /// <param name="sender"></param>
        private void SmallWakeCheckBox_CheckedChanged(object sender)
        {
            pictrueBoxEx1.Visible = SmallWakeCheckBox.Checked;
        }

        /// <summary>
        /// 睡眠分期 勾选框状态改变
        /// </summary>
        /// <param name="sender"></param>
        private void SleepDevCheckBox_CheckedChanged(object sender)
        {
            pictrueBoxEx2.Visible = SleepDevCheckBox.Checked;
        }

        #endregion

        #region 按钮方法

        /// <summary>
        /// 开始分析 提交按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
            int num = 0;
            foreach (object obj in this.panel1.Controls)
            {
                Control control = (Control)obj;
                if (control is pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox && control.Name != this.AllSelectCheckBox.Name && (control as pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox).Checked)
                {
                    num += 1 << (int)Convert.ToByte(control.Tag);
                    if (control.Name == this.SmallWakeCheckBox.Name)
                    {
                        Channel.Default.ArousalDataSourcePlan = this.aSelection.Value;
                    }
                    else if (control.Name == this.SleepDevCheckBox.Name)
                    {
                        Channel.Default.SleepStageSourcePlan = this.sSelection.Value;
                    }
                    else if (control.Name == this.O2CheckBox.Name)
                    {
                        Channel.Default.OxygenReduceRange = this.spo2Selection.Value;
                    }
                }
            }
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("用户点击 自动分析-提交按钮，当前选择的模式是{0}，事件分析可选项，低通气{1}，中枢性呼吸暂停{2}，阻塞性呼吸暂停{3}，混合性呼吸暂停{4}，氧减{5}，打鼾{6}，腿动{7}，MT{8}，微觉醒{9}，睡眠分期{10}", SectleSleepModeRadBut.Checked ? "全睡眠模式" : "呼吸筛查模式", LowHCheckBox.Checked ? "已选择" : "未选择", CACheckBox.Checked ? "已选择" : "未选择", OACheckBox.Checked ? "已选择" : "未选择", MACheckBox.Checked ? "已选择" : "未选择", O2CheckBox.Checked ? "已选择" : "未选择", SennerCheckBox.Checked ? "已选择" : "未选择", LegCheckBox.Checked ? "已选择" : "未选择", MTCheckBox.Checked ? "已选择" : "未选择", SmallWakeCheckBox.Checked ? "已选择" : "未选择", SleepDevCheckBox.Checked ? "已选择" : "未选择"));
            m_userOperationConfig.LastConfiguredAnalysisParameters = num;
            base.Close();
        }

        /// <summary>
        /// 开始分析 取消按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("用户点击 自动分析-取消按钮");
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion

        #region 公有方法

        #endregion

    }
}
