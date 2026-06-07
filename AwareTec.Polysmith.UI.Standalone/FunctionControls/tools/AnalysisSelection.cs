using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Ctrl;

namespace AwareTec.Polysmith.UI.FunctionControls.tools
{
    public partial class AnalysisSelection : SkinForm
    {
        private static AnalysisSelection m_default = null;
        Configuration m_appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        public static AnalysisSelection Defalut
        {
            get
            {
                return m_default ?? (m_default = new AnalysisSelection());
            }
        }
        public AnalysisSelection()
        {
            InitializeComponent();
            this.Load += ProgressTipForm_Load;
            this.FormClosed += ProgressTipForm_FormClosed;

        }

        private void ProgressTipForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_appConfig.AppSettings.Settings["AnalysisStateWord"].Value =Channel.Default.AnalysisStateWord.ToString();
            m_appConfig.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }
        bool isload = false;
        tools.ArousalSelection aSelection = null;
        tools.SleepStageSelection sSelection = null;
        private Spo2RangeSelection spo2Selection=null;
        private void ProgressTipForm_Load(object sender, EventArgs e)
        {
            int word = Convert.ToInt32(ConfigurationManager.AppSettings["AnalysisStateWord"]);
            string[] allowshows = Channel.Default.IsBreathOnly ? radioButton2.Tag.ToString().Split('\\') : radioButton1.Tag.ToString().Split('\\');
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
                Value = Channel.Default.ArousalDataSourcePlan 
            };
            this.sSelection = new SleepStageSelection
            {
                Value = Channel.Default.SleepStageSourcePlan
            };
            this.spo2Selection = new Spo2RangeSelection
            {
                Value = Channel.Default.SystemSetting.OxygenReduceRange
            };
            this.pictrueBoxEx1.SetPopupControl(this.aSelection);
            this.pictrueBoxEx2.SetPopupControl(this.sSelection);
            this.pictrueBoxEx3.SetPopupControl(this.spo2Selection);
            this.radioButton2.Checked = Channel.Default.IsBreathOnly;
            base.Move += this.AnalysisSelection_Move;
            this.isload = true;
        }

        private void AnalysisSelection_Move(object sender, EventArgs e)
        {
            this.pictrueBoxEx1.ReLocation();
            this.pictrueBoxEx2.ReLocation();
            this.pictrueBoxEx3.ReLocation();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DataModel.LogInstance.Default.AddLog("用户点击 自动分析-取消按钮");
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
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
            DataModel.LogInstance.Default.AddLog(string.Format("用户点击 自动分析-提交按钮，当前选择的模式是{0}，事件分析可选项，低通气{1}，中枢性呼吸暂停{2}，阻塞性呼吸暂停{3}，混合性呼吸暂停{4}，氧减{5}，打鼾{6}，腿动{7}，MT{8}，微觉醒{9}，睡眠分期{10}", radioButton1.Checked?"全睡眠模式":"呼吸筛查模式", LowHCheckBox.Checked?"已选择":"未选择", CACheckBox.Checked? "已选择" : "未选择", OACheckBox.Checked? "已选择" : "未选择", MACheckBox.Checked? "已选择" : "未选择", O2CheckBox.Checked ? "已选择" : "未选择", SennerCheckBox.Checked ? "已选择" : "未选择", LegCheckBox.Checked? "已选择" : "未选择", MTCheckBox.Checked? "已选择" : "未选择", SmallWakeCheckBox.Checked? "已选择" : "未选择", SleepDevCheckBox .Checked? "已选择" : "未选择"));
            Channel.Default.AnalysisStateWord = num;
            base.Close();
        }

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
        /// 临时特殊处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LowHCheckBox_CheckedChanged(object sender)
        {
            CACheckBox.Checked = OACheckBox.Checked = MACheckBox.Checked = LowHCheckBox.Checked;
        }

        private void SmallWakeCheckBox_CheckedChanged(object sender)
        {
            pictrueBoxEx1.Visible = SmallWakeCheckBox.Checked;
        }

        private void SleepDevCheckBox_CheckedChanged(object sender)
        {
            pictrueBoxEx2.Visible = SleepDevCheckBox.Checked;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
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

        private void O2CheckBox_CheckedChanged(object sender)
        {
            this.pictrueBoxEx3.Visible = this.O2CheckBox.Checked;
        }
    }
}
