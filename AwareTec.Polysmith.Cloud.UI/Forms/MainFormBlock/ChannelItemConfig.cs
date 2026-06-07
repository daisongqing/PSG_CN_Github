using AwareTec.Polysmith.Cloud.UI.DataModels;
using AwareTec.Polysmith.Cloud.UI.DataModels.Global;
using AwareTec.Polysmith.Cloud.UI.ViewHelpers;
using AwareTec.Polysmith.Util;
using pSystem.UI.ReaLTaiizor.CustomCtrl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.Cloud.UI.Forms.MainFormBlock
{
    public partial class ChannelItemConfig : CloudSkinForm
    {
        #region 私有变量

        private bool m_init = false;
        private ChannelTable m_Table = null;

        #endregion

        #region 公有变量

        /// <summary>
        /// 是否为实时
        /// </summary>
        public bool ISRealView { set; get; }

        #endregion

        #region 事件委托

        public delegate void UpdateItemDelegate(ChannelTable channel);
        /// <summary>
        /// 通道属性更改时发生
        /// </summary>
        public event UpdateItemDelegate UpdateItemHandle;

        #endregion

        #region 初始化 load/dispose/init

        /// <summary>
        /// 初始化
        /// </summary>
        public ChannelItemConfig()
        {
            InitializeComponent();
            this.Load += ChannelItemConfig_Load;
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChannelItemConfig_Load(object sender, EventArgs e)
        {
            this.CbSensi.SelectedIndexChanged += CbSensi_SelectedIndexChanged;
            this.HighPassComboBox.SelectedIndexChanged += HighPassComboBox_SelectedIndexChanged;
            this.LowPassComboBox.SelectedIndexChanged += LowPassComboBox_SelectedIndexChanged;
            this.XianBoComboBox.SelectedIndexChanged += XianBoComboBox_SelectedIndexChanged;
            this.colorSelectComboBox.DropDownOnClick += ColorSelectComboBox_DropDownOnClick;
            this.SensiCheckBox.Click += SensiCheckBox_Click;
            this.MaxMinCheckBox.Click += MaxMinCheckBox_Click;
            this.MaxValueTextBox.KeyPress += MaxValueTextBox_KeyPress;
            this.MirrorCheckBox.CheckedChanged += MirrorCheckBox_CheckedChanged;
            this.Jx75uvCheckBox.CheckedChanged += Jx75uvCheckBox_CheckedChanged;
            this.FormClosing += ChannelItemConfigCopy_FormClosing;

            ISRealView = false;
            Jx75uvCheckBox.Visible = !ISRealView;
        }

        /// <summary>
        /// 单个通道属性窗口 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChannelItemConfigCopy_FormClosing(object sender, FormClosingEventArgs e)
        {
            colorSelectComboBox.DropDownOnClick -= ColorSelectComboBox_DropDownOnClick;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 陷波器下拉框选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XianBoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_Table.SingleNotch = XianBoComboBox.SelectedItem.ToString();
            change();
        }

        /// <summary>
        /// 低通下拉框选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LowPassComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_Table.LowPass = LowPassComboBox.SelectedItem.ToString();
            change();
        }

        /// <summary>
        /// 高通下拉框选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HighPassComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_Table.HighPass = HighPassComboBox.SelectedItem.ToString();
            change();
        }

        /// <summary>
        /// 灵敏度下拉框选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbSensi_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_Table.Sensitivity = ChannelTable.getSensitivity(CbSensi.SelectedItem);
            change();
        }

        /// <summary>
        /// 灵敏度勾选框点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SensiCheckBox_Click(object sender, EventArgs e)
        {
            MaxValueTextBox.Enabled = false;
            MinValueTextBox.Enabled = false;
            MaxMinCheckBox.Checked = false;
            SensiCheckBox.Checked = true;
            CbSensi.Enabled = true;
            m_Table.MaxMinValueEnable = false;
            change();
        }

        /// <summary>
        /// 最大/小值限定 勾选框点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaxMinCheckBox_Click(object sender, EventArgs e)
        {
            MaxValueTextBox.Enabled = true;
            MinValueTextBox.Enabled = true;
            MaxMinCheckBox.Checked = true;
            SensiCheckBox.Checked = false;
            CbSensi.Enabled = false;
            m_Table.MaxMinValueEnable = true;
            m_Table.MaxValue = float.Parse(MaxValueTextBox.Text);
            m_Table.MinValue = float.Parse(MinValueTextBox.Text);
            change();
        }

        /// <summary>
        /// 颜色下拉框点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorSelectComboBox_DropDownOnClick(object sender, pSystem.UI.ReaLTaiizor.CustomCtrl.CustomCtrlEventArgs.DropDownOnClickEventArgs e)
        {
            ColorSettings colorSettings = new ColorSettings((ColorComboBox)sender, e.Color);
            colorSettings.StartPosition = FormStartPosition.Manual;
            colorSettings.Location = Cursor.Position;
            colorSettings.Location = EnsureLocationHelper.CalculateSituableLocation(colorSettings);
            colorSettings.ColorChanged += ColorSettings_ColorChanged;
            colorSettings.ShowDialog();
        }

        /// <summary>
        /// 颜色改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorSettings_ColorChanged(object sender, pSystem.UI.ReaLTaiizor.CustomCtrl.CustomCtrlEventArgs.ColorChangedEventArgs e)
        {
            if (!(sender is ColorSettings))
                return;

            ColorSettings colorSettings = sender as ColorSettings;

            colorSettings.Sender.Color = m_Table.ColorSelect = e.Color;
            change();
            colorSettings.ColorChanged -= ColorSettings_ColorChanged;
            colorSettings.Close();
        }

        /// <summary>
        /// 文本框输入限制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaxValueTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputTextCheck.floatvalue_KeyPress(sender, e);
        }

        /// <summary>
        /// 镜像 勾选框点击
        /// </summary>
        /// <param name="sender"></param>
        private void MirrorCheckBox_CheckedChanged(object sender)
        {
            if (m_init)
            {
                m_Table.Antipole = MirrorCheckBox.Checked;
                change();
            }
        }

        /// <summary>
        /// 75微伏基线 勾选框点击
        /// </summary>
        /// <param name="sender"></param>
        private void Jx75uvCheckBox_CheckedChanged(object sender)
        {
            m_Table.DBaseLineVisible = Jx75uvCheckBox.Checked;
            change();
        }

        /// <summary>
        /// 键盘快捷键
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter:
                    if (ChannelName.Focused)
                    {
                        string value = ChannelName.Text.Trim();
                        if (value != "")
                        {
                            m_Table.strName = value;
                            change();
                        }
                    }
                    else if (MaxValueTextBox.Focused)
                    {
                        float value = m_Table.MaxValue;
                        if (float.TryParse(MaxValueTextBox.Text.Trim(), out value))
                        {
                            if (value != m_Table.MaxValue)
                            {
                                if (value > m_Table.MinValue)
                                {
                                    m_Table.MaxValue = value;
                                    change();
                                }
                                else
                                {
                                    AhDung.MessageTip.ShowWarning("最大值小于最小值，请及时更改最小值方能生效");
                                }
                            }
                        }
                        else
                        {
                            AhDung.MessageTip.ShowWarning("数据格式不正确");
                        }
                    }
                    else if (MinValueTextBox.Focused)
                    {
                        float value = m_Table.MinValue;
                        if (float.TryParse(MinValueTextBox.Text.Trim(), out value))
                        {
                            if (value != m_Table.MinValue)
                            {
                                if (m_Table.MaxValue > value)
                                {
                                    m_Table.MinValue = value;
                                    change();
                                }
                                else
                                {
                                    AhDung.MessageTip.ShowWarning("最小值大于最大值，请及时更改最大值方能生效");
                                }
                            }
                        }
                        else
                        {
                            AhDung.MessageTip.ShowWarning("数据格式不正确");
                        }
                    }
                    change();
                    return true;//不继续处理
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// 单个通道属性值 发生改变
        /// </summary>
        private void change()
        {
            if (UpdateItemHandle != null)
                UpdateItemHandle.Invoke(m_Table);
        }

        #endregion

        #region 按钮方法

        #endregion

        #region 公有方法

        /// <summary>
        /// 外界调用初始化
        /// </summary>
        /// <param name="item"></param>
        public void Init(ChannelTable item)
        {
            m_Table = item;
            ChannelName.Text = item.strName;
            MaxValueTextBox.Text = item.MaxValue.ToString();
            MinValueTextBox.Text = item.MinValue.ToString();
            string[] rangs = new string[] { "Off" };
            ChannelTable find = GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelTable.Find(t => t.ChannelID == m_Table.ChannelID);
            string unit = "";
            if (find != null)
            {
                rangs = find.strPixelRangArray;
                unit = find.Unit;
            }
            CbSensi.Items.AddRange(rangs);
            HighPassComboBox.Items.Add("Off");
            HighPassComboBox.Items.AddRange(Channel.Default.channel_FilterDropDownHandle(m_Table.ID, "HighPass"));
            LowPassComboBox.Items.Add("Off");
            LowPassComboBox.Items.AddRange(Channel.Default.channel_FilterDropDownHandle(m_Table.ID, "LowPass"));
            XianBoComboBox.Items.Add("Off");
            XianBoComboBox.Items.AddRange(Channel.Default.channel_FilterDropDownHandle(m_Table.ID, "SingleNotch"));
            CbSensi.Text = ChannelTable.setSensitivity(item.Sensitivity, unit == "Pa");
            if (item.PixelEnable)
            {
                if (item.MaxMinValueEnable)
                {
                    SensiCheckBox.Checked = false;
                    CbSensi.Enabled = false;
                    MaxMinCheckBox.Checked = true;
                    MaxValueTextBox.Enabled = true;
                    MinValueTextBox.Enabled = true;
                    MirrorCheckBox.Enabled = false;
                }
                else
                {
                    SensiCheckBox.Checked = true;
                    MaxMinCheckBox.Checked = false;
                    MirrorCheckBox.Enabled = true;
                }
            }
            else
            {
                SensiCheckBox.Checked = false;
                SensiCheckBox.Enabled = false;
                CbSensi.Enabled = false;
                MaxMinCheckBox.Checked = true;
                MaxValueTextBox.Enabled = true;
                MinValueTextBox.Enabled = true;
                MirrorCheckBox.Enabled = false;
            }
            MirrorCheckBox.Checked = m_Table.Antipole;
            if (m_Table.SpanTime < 500)
                Jx75uvCheckBox.Enabled = false;
            else
                Jx75uvCheckBox.Checked = m_Table.DBaseLineVisible;
            colorSelectComboBox.Color = item.ColorSelect;
            HighPassComboBox.Text = item.HighPass;
            LowPassComboBox.Text = item.LowPass;
            XianBoComboBox.Text = item.SingleNotch;
            m_init = true;
        }

        #endregion

    }
}
