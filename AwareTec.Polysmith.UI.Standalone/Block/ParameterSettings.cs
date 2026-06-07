//*************************************************************************************
//
//文件名(File Name):         ParameterSettings.cs
//
//功能描述(Description)：    可以通过页面设置算法的相关参数，个人设置保存到数据库中，提供默认参数，输入检测。在运行算法时，如果不进行额外设置，
//                           以该页面设置的参数为准。
//
//数据表(Tables):            对应于本地数据库中 ParameterSettings 数据表
//
//作者(Author):              
//
//日期(Create Date):         2022.03.18
//
//参考文档(Reference)(可选)： 该档所对应的分析文档，设计文档。
//
//引用(Using)(可选)﹕         开发的系统中引用其它系统的Dll、对象时，要列出其对应的出处，是否与系统有关(不清楚的可以不写)，以方便制作安装档。
//
//修改记录(Revicion History):
//      R1:
//          修改作者:
//          修改日期:
//          修改理由:
//
//      R2:
//
//*************************************************************************************

using AwareTec.Polysmith.DataBaseCom;
using AwareTec.Polysmith.UI.DataModel;
using AwareTec.Polysmith.UI.FunctionControls.tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.UI.Block
{
    public partial class ParameterSettings : SkinForm
    {
        #region 私有变量
        /// <summary>
        /// 输入信息提示标签
        /// </summary>
        DotNetCtl.ErrorProviderHelper m_errorProvider;
        const int InputValue_PADDING =-20;

        private string m_EventThresholdValueByPress = "50;90;*;*";
        private string m_EventThresholdValueByFlow = "*;*;85;100";
        private string m_EventThresholdValueByPressFlow = "50;85;75;100";
        private Point m_PressStartPoint = new Point();
        private Point m_FlowStartPoint = new Point();
        private Point m_LastTimeStartPoint = new Point();
        private int m_Interval = 50;
        private string[] m_TagValues = new string[2];
        private float m_TagMinValue = 0;
        private float m_TagMaxValue = 200;
        #endregion

        #region 初始化 load/close/dispose

        public ParameterSettings()
        {
            m_errorProvider = new DotNetCtl.ErrorProviderHelper(this);
            InitializeComponent();
            this.Load += this.ParameterSettings_Load;
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParameterSettings_Load(object sender, EventArgs e)
        {
            //呼吸事件的分析通道会影响不同的阈值,控件颜色，位置都需要改变
            this.BreathAnalysisChannelComBox.SelectedIndexChanged += BreathAnalysisChannelComBox_SelectedIndexChanged;
            m_PressStartPoint = this.label50.Location;
            m_FlowStartPoint = this.label52.Location;
            m_LastTimeStartPoint = this.label54.Location;
            m_Interval = (m_FlowStartPoint.Y - m_PressStartPoint.Y) / 2;

            //检查文本框输入时是否是纯数字
            this.Spo2BufferTimeTxtBox.KeyPress += this.CheckTxtBox_KeyPress;
            this.Spo2LastTimeTexBox.KeyPress += this.CheckTxtBox_KeyPress;
            this.MinBreathLastTimeTxtBox.KeyPress += this.CheckTxtBox_KeyPress;
            this.MaxBreathLastTimeTxtBox.KeyPress += this.CheckTxtBox_KeyPress;
            this.MinLMThresholdValueTxtBox.KeyPress += this.CheckTxtBox_KeyPress;
            this.MaxLMThresholdValueTxtBox.KeyPress += this.CheckTxtBox_KeyPress;
            this.MinPLMsThresholdValueTxtBox.KeyPress += this.CheckTxtBox_KeyPress;
            this.MaxPLMsThresholdValueTxtBox.KeyPress += this.CheckTxtBox_KeyPress;
            this.MTLastTimeValueTxtBox.KeyPress += MTLastTimeValueTxtBox_KeyPress; ;
            this.PressureBreathReduceThresholdValueTxtBox.KeyPress += this.CheckTxtBox_KeyPress;
            this.PressureApneaThresholdValueTxtBox.KeyPress += this.CheckTxtBox_KeyPress;
            this.FlowBreathReduceThresholdValueTxtBox.KeyPress += this.CheckTxtBox_KeyPress;
            this.FlowApneaThresholdValueTxtBox.KeyPress += this.CheckTxtBox_KeyPress;

            //检查离开文本框时 输入的值太大或者太小
            this.Spo2BufferTimeTxtBox.Leave += CheckTexBox_Leave;
            this.Spo2LastTimeTexBox.Leave += CheckTexBox_Leave;
            this.MinBreathLastTimeTxtBox.Leave += CheckTexBox_Leave;
            this.MaxBreathLastTimeTxtBox.Leave += CheckTexBox_Leave;
            this.MinLMThresholdValueTxtBox.Leave += CheckTexBox_Leave;
            this.MaxLMThresholdValueTxtBox.Leave += CheckTexBox_Leave;
            this.MinPLMsThresholdValueTxtBox.Leave += CheckTexBox_Leave;
            this.MaxPLMsThresholdValueTxtBox.Leave += CheckTexBox_Leave;
            this.MTLastTimeValueTxtBox.Leave += CheckTexBox_Leave;
            this.PressureBreathReduceThresholdValueTxtBox.Leave += CheckTexBox_Leave;
            this.PressureApneaThresholdValueTxtBox.Leave += CheckTexBox_Leave;
            this.FlowBreathReduceThresholdValueTxtBox.Leave += CheckTexBox_Leave;
            this.FlowApneaThresholdValueTxtBox.Leave += CheckTexBox_Leave;

            InitData();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            this.Spo2BufferTimeTxtBox.Text = Channel.Default.ParameterSetting.Spo2BufferTime.ToString();
            this.Spo2LastTimeTexBox.Text = Channel.Default.ParameterSetting.Spo2LastTime.ToString();
            this.Spo2ReduceComBox.Text = string.Format("{0} %", Channel.Default.ParameterSetting.Spo2Reduce.ToString());
            this.BreathAnalysisChannelComBox.SelectedIndex = Channel.Default.ParameterSetting.BreathAnalysisChannel;
            this.MinBreathLastTimeTxtBox.Text = Channel.Default.ParameterSetting.MinBreathLastTime.ToString();
            this.MaxBreathLastTimeTxtBox.Text = Channel.Default.ParameterSetting.MaxBreathLastTime.ToString();
            this.MinLMThresholdValueTxtBox.Text = Channel.Default.ParameterSetting.MinLMThresholdValue.ToString();
            this.MaxLMThresholdValueTxtBox.Text = Channel.Default.ParameterSetting.MaxLMThresholdValue.ToString();
            this.MinPLMsThresholdValueTxtBox.Text = Channel.Default.ParameterSetting.MinPLMsThresholdValue.ToString();
            this.MaxPLMsThresholdValueTxtBox.Text = Channel.Default.ParameterSetting.MaxPLMsThresholdValue.ToString();
            if (Channel.Default.ParameterSetting.SleepStageModeChoose == 1)
                this.SleepStageModeChooseComBox.SelectedIndex = 0;
            else
                this.SleepStageModeChooseComBox.SelectedIndex = 1;
            this.MTLastTimeValueTxtBox.Text = Channel.Default.ParameterSetting.MTLastTime.ToString();
            string[] val = SpindlesAlphaWavesAnalysisChannelComBox.Tag.ToString().Split('\\');
            foreach (string s in val)
            {
                Doc_Channel find = Channel.Default.ChannelProperties.Find(t => t.ID.ToString() == s);
                if (find == null)
                    continue;
                else
                {
                    SpindlesAlphaWavesAnalysisChannelComBox.Items.Add(find.SignName);
                    ArousalAnalysisChannelComBox.Items.Add(find.SignName);
                }
                if (find.ID == Channel.Default.ParameterSetting.SpindlesAlphaWavesAnalysisChannel)
                {
                    SpindlesAlphaWavesAnalysisChannelComBox.Text = find.SignName;
                }
                if (find.ID == Channel.Default.ParameterSetting.ArousalAnalysisChannel)
                {
                    ArousalAnalysisChannelComBox.Text = find.SignName;
                }
            }
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 呼吸事件分析通道值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BreathAnalysisChannelComBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_EventThresholdValueByPress = Channel.Default.ParameterSetting.EventThresholdValueByPress;
            m_EventThresholdValueByFlow = Channel.Default.ParameterSetting.EventThresholdValueByFlow;
            m_EventThresholdValueByPressFlow = Channel.Default.ParameterSetting.EventThresholdValueByFlowPress;
            string[] values;
            if (BreathAnalysisChannelComBox.SelectedIndex == 0)
            {
                //控件的隐藏和调整位置
                this.label50.Visible = this.label51.Visible = this.label60.Visible = this.label61.Visible = this.PressureBreathReduceThresholdValueTxtBox.Visible = this.PressureApneaThresholdValueTxtBox.Visible = true;
                this.label52.Visible = this.label53.Visible = this.label62.Visible = this.label63.Visible = this.FlowBreathReduceThresholdValueTxtBox.Visible = this.FlowApneaThresholdValueTxtBox.Visible = false;
                this.label54.Location = new Point(this.label54.Location.X, m_FlowStartPoint.Y);
                this.label64.Location = new Point(this.label64.Location.X, m_FlowStartPoint.Y);
                this.MinBreathLastTimeTxtBox.Location = new Point(this.MinBreathLastTimeTxtBox.Location.X, m_FlowStartPoint.Y);
                this.MaxBreathLastTimeTxtBox.Location = new Point(this.MaxBreathLastTimeTxtBox.Location.X, m_FlowStartPoint.Y);
                this.label65.Location = new Point(this.label65.Location.X, m_FlowStartPoint.Y);

                this.PressureBreathReduceThresholdValueTxtBox.Enabled = true;
                this.PressureApneaThresholdValueTxtBox.Enabled = true;
                this.FlowBreathReduceThresholdValueTxtBox.Enabled = false;
                this.FlowApneaThresholdValueTxtBox.Enabled = false;
                this.FlowBreathReduceThresholdValueTxtBox.Text = "*";
                this.FlowApneaThresholdValueTxtBox.Text = "*";
                values = m_EventThresholdValueByPress.Split(';');
                if (values.Length==4)
                {
                    this.PressureBreathReduceThresholdValueTxtBox.Text = values[0];
                    this.PressureApneaThresholdValueTxtBox.Text = values[1];
                }
            }
            else if(BreathAnalysisChannelComBox.SelectedIndex==1)
            {
                //控件的隐藏和调整位置
                this.label50.Visible = this.label51.Visible = this.label60.Visible = this.label61.Visible = this.PressureBreathReduceThresholdValueTxtBox.Visible = this.PressureApneaThresholdValueTxtBox.Visible = false;
                this.label52.Visible = this.label53.Visible = this.label62.Visible = this.label63.Visible = this.FlowBreathReduceThresholdValueTxtBox.Visible = this.FlowApneaThresholdValueTxtBox.Visible = true;
                this.label52.Location = new Point(this.label52.Location.X, m_PressStartPoint.Y);
                this.label53.Location = new Point(this.label53.Location.X, m_PressStartPoint.Y + m_Interval);
                this.label62.Location = new Point(this.label62.Location.X, m_PressStartPoint.Y);
                this.label63.Location = new Point(this.label63.Location.X, m_PressStartPoint.Y + m_Interval);
                this.FlowBreathReduceThresholdValueTxtBox.Location = new Point(this.FlowBreathReduceThresholdValueTxtBox.Location.X, m_PressStartPoint.Y);
                this.FlowApneaThresholdValueTxtBox.Location = new Point(this.FlowApneaThresholdValueTxtBox.Location.X, m_PressStartPoint.Y + m_Interval);
                this.label54.Location = new Point(this.label54.Location.X, m_FlowStartPoint.Y);
                this.label64.Location = new Point(this.label64.Location.X, m_FlowStartPoint.Y);
                this.MinBreathLastTimeTxtBox.Location = new Point(this.MinBreathLastTimeTxtBox.Location.X, m_FlowStartPoint.Y);
                this.MaxBreathLastTimeTxtBox.Location = new Point(this.MaxBreathLastTimeTxtBox.Location.X, m_FlowStartPoint.Y);
                this.label65.Location = new Point(this.label65.Location.X, m_FlowStartPoint.Y);

                this.PressureBreathReduceThresholdValueTxtBox.Enabled = false;
                this.PressureApneaThresholdValueTxtBox.Enabled = false;
                this.PressureBreathReduceThresholdValueTxtBox.Text = "*";
                this.PressureApneaThresholdValueTxtBox.Text = "*";
                this.FlowBreathReduceThresholdValueTxtBox.Enabled = true;
                this.FlowApneaThresholdValueTxtBox.Enabled = true;
                values = m_EventThresholdValueByFlow.Split(';');
                if (values.Length == 4)
                {
                    this.FlowBreathReduceThresholdValueTxtBox.Text = values[2];
                    this.FlowApneaThresholdValueTxtBox.Text = values[3];
                }
            }
            else
            {
                //控件的隐藏和调整位置
                this.label50.Visible = this.label51.Visible = this.label60.Visible = this.label61.Visible = this.PressureBreathReduceThresholdValueTxtBox.Visible = this.PressureApneaThresholdValueTxtBox.Visible = true;
                this.label52.Visible = this.label53.Visible = this.label62.Visible = this.label63.Visible = this.FlowBreathReduceThresholdValueTxtBox.Visible = this.FlowApneaThresholdValueTxtBox.Visible = true;
                this.label52.Location = new Point(this.label52.Location.X, m_FlowStartPoint.Y);
                this.label53.Location = new Point(this.label53.Location.X, m_FlowStartPoint.Y + m_Interval);
                this.label62.Location = new Point(this.label62.Location.X, m_FlowStartPoint.Y);
                this.label63.Location = new Point(this.label63.Location.X, m_FlowStartPoint.Y + m_Interval);
                this.FlowBreathReduceThresholdValueTxtBox.Location = new Point(this.FlowBreathReduceThresholdValueTxtBox.Location.X, m_FlowStartPoint.Y);
                this.FlowApneaThresholdValueTxtBox.Location = new Point(this.FlowApneaThresholdValueTxtBox.Location.X, m_FlowStartPoint.Y + m_Interval);
                this.label54.Location = new Point(this.label54.Location.X, m_LastTimeStartPoint.Y);
                this.label64.Location = new Point(this.label64.Location.X, m_LastTimeStartPoint.Y);
                this.MinBreathLastTimeTxtBox.Location = new Point(this.MinBreathLastTimeTxtBox.Location.X, m_LastTimeStartPoint.Y);
                this.MaxBreathLastTimeTxtBox.Location = new Point(this.MaxBreathLastTimeTxtBox.Location.X, m_LastTimeStartPoint.Y);
                this.label65.Location = new Point(this.label65.Location.X, m_LastTimeStartPoint.Y);

                this.PressureBreathReduceThresholdValueTxtBox.Enabled = true;
                this.PressureApneaThresholdValueTxtBox.Enabled = true;
                this.FlowBreathReduceThresholdValueTxtBox.Enabled = true;
                this.FlowApneaThresholdValueTxtBox.Enabled = true;
                values = m_EventThresholdValueByPressFlow.Split(';');
                if (values.Length == 4)
                {
                    this.PressureBreathReduceThresholdValueTxtBox.Text = values[0];
                    this.PressureApneaThresholdValueTxtBox.Text = values[1];
                    this.FlowBreathReduceThresholdValueTxtBox.Text = values[2];
                    this.FlowApneaThresholdValueTxtBox.Text = values[3];
                }
            }
        }

        /// <summary>
        /// 检查数据输入 (0 表示数据正常 1 表示最大值大于最小值 2 表示文本框输入值过大或者过小 3表示文本框输入为空)
        /// </summary>
        /// <returns></returns>
        private int CheckData()
        {
            if (Convert.ToSingle(MaxPLMsThresholdValueTxtBox.Text) <= Convert.ToSingle(MinPLMsThresholdValueTxtBox.Text) ||
                Convert.ToSingle(MaxLMThresholdValueTxtBox.Text) <= Convert.ToSingle(MinLMThresholdValueTxtBox.Text) ||
                Convert.ToSingle(MaxBreathLastTimeTxtBox.Text) <= Convert.ToSingle(MinBreathLastTimeTxtBox.Text))
                return 1;
            foreach (object obj in this.ParameterSettingsTabPage.Controls)
            {
                Control control = (Control)obj;
                if (control is System.Windows.Forms.TabPage)
                {
                    foreach (object obj2 in control.Controls)
                    {
                        Control control2 = (Control)obj2;
                        if (control2 is pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox)
                        {
                            m_TagValues = control2.Tag.ToString().Split(';');
                            if (m_TagValues.Length == 2)
                            {
                                m_TagMinValue = Convert.ToSingle(m_TagValues[0]);
                                m_TagMaxValue = Convert.ToSingle(m_TagValues[1]);
                                if (control2.Text == "*")
                                    continue;
                                if (string.IsNullOrEmpty(control2.Text))
                                    return 3;
                                if (Convert.ToSingle(control2.Text) < 0 || Convert.ToSingle(control2.Text) > m_TagMaxValue || Convert.ToSingle(control2.Text) < m_TagMinValue)
                                {
                                    return 2;
                                }
                            }
                        }
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// 算法中 该参数只允许 整数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MTLastTimeValueTxtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Control control = sender as Control;
            if (control.ForeColor == Color.Red)
            {
                m_errorProvider.Clear(control);
                control.ForeColor = Color.DimGray;
            }
            DataModel.InputTextCheck.phone_KeyPress(sender, e);
        }

        /// <summary>
        /// 文本框输入限制（只允许数字 小数点）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckTxtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Control control = sender as Control;
            if (control.ForeColor == Color.Red)
            {
                m_errorProvider.Clear(control);
                control.ForeColor = Color.DimGray;
            }
            DataModel.InputTextCheck.floatvalue_KeyPress(sender, e);
        }

        /// <summary>
        /// 离开时检查输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckTexBox_Leave(object sender, EventArgs e)
        {
            Control control = sender as Control;
            m_TagValues = control.Tag.ToString().Split(';');
            if (m_TagValues.Length == 2)
            {
                m_TagMinValue = Convert.ToSingle(m_TagValues[0]);
                m_TagMaxValue = Convert.ToSingle(m_TagValues[1]);
                if(!string.IsNullOrEmpty(control.Text))
                {
                    if (Convert.ToSingle(control.Text) < 0 || Convert.ToSingle(control.Text) > m_TagMaxValue || Convert.ToSingle(control.Text) < m_TagMinValue)
                    {
                        m_errorProvider.ShowError(control, string.Format("时间值过大或者过小! 推荐范围为{0}-{1}", m_TagMinValue, m_TagMaxValue), InputValue_PADDING);
                        //AhDung.MessageTip.ShowWarning(string.Format("时间值过大或者过小! 推荐范围为{0}-{1}", m_TagMinValue, m_TagMaxValue));
                        control.ForeColor = Color.Red;
                    }
                }
                else
                {
                    m_errorProvider.ShowError(control, "输入值不允许为空！", InputValue_PADDING);
                }
            }
        }

        #endregion

        #region 按钮方法

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            string savevalues = string.Format("{0};{1};{2};{3}", PressureBreathReduceThresholdValueTxtBox.Text, PressureApneaThresholdValueTxtBox.Text, FlowBreathReduceThresholdValueTxtBox.Text, FlowApneaThresholdValueTxtBox.Text);
            m_EventThresholdValueByPress = this.BreathAnalysisChannelComBox.SelectedIndex == 0 ? savevalues : m_EventThresholdValueByPress;
            m_EventThresholdValueByFlow = this.BreathAnalysisChannelComBox.SelectedIndex == 1 ? savevalues : m_EventThresholdValueByFlow;
            m_EventThresholdValueByPressFlow = this.BreathAnalysisChannelComBox.SelectedIndex == 2 ? savevalues : m_EventThresholdValueByPressFlow;
            int isok = CheckData();
            if (isok!=0)
            {
                if (isok == 1)
                {
                    AhDung.MessageTip.ShowWarning("时间范围的最大值应大于最小值！");
                    return;
                }
                else if(isok==2)
                {
                    AhDung.MessageTip.ShowWarning("时间值过大或者过小！");
                    return;
                }
                else
                {
                    AhDung.MessageTip.ShowWarning("不允许输入空值！");
                    return;
                }
            }
            Doc_ParameterSettings doc_ParameterSettings = new Doc_ParameterSettings();
            doc_ParameterSettings = new Doc_ParameterSettings
            {
                Spo2BufferTime = Convert.ToSingle(this.Spo2BufferTimeTxtBox.Text),
                Spo2LastTime = Convert.ToSingle(this.Spo2LastTimeTexBox.Text),
                Spo2Reduce = Convert.ToInt32(this.Spo2ReduceComBox.Text.Replace(" %", "")),
                BreathAnalysisChannel = this.BreathAnalysisChannelComBox.SelectedIndex,
                EventThresholdValueByPress = m_EventThresholdValueByPress,
                EventThresholdValueByFlow = m_EventThresholdValueByFlow,
                EventThresholdValueByFlowPress = m_EventThresholdValueByPressFlow,
                MinBreathLastTime = Convert.ToSingle(this.MinBreathLastTimeTxtBox.Text),
                MaxBreathLastTime = Convert.ToSingle(this.MaxBreathLastTimeTxtBox.Text),
                MinLMThresholdValue = Convert.ToSingle(this.MinLMThresholdValueTxtBox.Text),
                MaxLMThresholdValue = Convert.ToSingle(this.MaxLMThresholdValueTxtBox.Text),
                MinPLMsThresholdValue = Convert.ToSingle(this.MinPLMsThresholdValueTxtBox.Text),
                MaxPLMsThresholdValue = Convert.ToSingle(this.MaxPLMsThresholdValueTxtBox.Text),
                SleepStageModeChoose = this.SleepStageModeChooseComBox.Text.Length > 15 ? 3 : 1,
                SpindlesAlphaWavesAnalysisChannel = Channel.Default.ChannelProperties.Find(t => t.SignName == SpindlesAlphaWavesAnalysisChannelComBox.Text).ID,
                MTLastTime = Convert.ToSingle(this.MTLastTimeValueTxtBox.Text),
                ArousalAnalysisChannel = Channel.Default.ChannelProperties.Find(t => t.SignName == ArousalAnalysisChannelComBox.Text).ID,
            };
            if (!DataBaseHelper.Default.Update(new Doc_ParameterSettings
            {
                ID = Channel.Default.ParameterSetting.ID
            }, doc_ParameterSettings))
            {
                AhDung.MessageTip.ShowError("修改失败，数据库异常！", -1, null, null, false);
                DataModel.LogInstance.Default.AddLog("用户点击 参数设置-保存按钮 修改失败，数据库异常！", pSystem.LogManagement.LogLevel.ERROR);
                return;
            }
            else
            {
                doc_ParameterSettings.ID = Channel.Default.ParameterSetting.ID;
                doc_ParameterSettings.UserID = Channel.Default.ParameterSetting.UserID;
                Channel.Default.ParameterSetting = doc_ParameterSettings;
                AhDung.MessageTip.ShowOk("保存成功！");
                this.Close();
            }
        }

        /// <summary>
        /// 恢复默认设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        private void RecoverButton_Click(object sender, EventArgs e)
        {
            if (MessageForm.Show("确认将算法参数信息恢复默认设置吗？", "信息确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Doc_ParameterSettings doc_ParameterSettings = DataModel.DataBaseHelper.Default.Select(new Doc_ParameterSettings() { UserID = 0, ModeType = Channel.Default.SystemSetting.ModeType });
                doc_ParameterSettings.ID = Channel.Default.ParameterSetting.ID;
                doc_ParameterSettings.UserID = Channel.Default.ParameterSetting.UserID;
                if (!DataBaseHelper.Default.Update(new Doc_ParameterSettings
                {
                    ID = Channel.Default.ParameterSetting.ID
                }, doc_ParameterSettings))
                {
                    AhDung.MessageTip.ShowError("修改失败，数据库异常！", -1, null, null, false);
                    DataModel.LogInstance.Default.AddLog("用户点击 参数设置-恢复默认设置 数据库修改失败，数据库异常！", pSystem.LogManagement.LogLevel.ERROR);
                    return;
                }
                else
                {
                    Channel.Default.ParameterSetting = doc_ParameterSettings;
                    InitData();
                    AhDung.MessageTip.ShowOk("参数信息恢复成功！");
                }
            }
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (MessageForm.Show("算法参数信息未完成，确定退出？", "信息确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        #endregion
    }
}
