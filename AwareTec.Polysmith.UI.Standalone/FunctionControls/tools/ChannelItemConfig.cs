using AwareTec.Polysmith.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.UI.FunctionControls.tools
{
    public partial class ChannelItemConfig : SkinForm
    {
        
        public ChannelItemConfig()
        {
            InitializeComponent();
            ISRealView = false;
            this.Load += ChannelItemConfig_Load;
        }
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
                            m_Table.Name = value;
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
        private void ChannelItemConfig_Load(object sender, EventArgs e)
        {
            CbSensi.SelectedIndexChanged += CbSensi_SelectedIndexChanged;
            HighPassComboBox.SelectedIndexChanged += HighPassComboBox_SelectedIndexChanged;
            LowPassComboBox.SelectedIndexChanged += LowPassComboBox_SelectedIndexChanged;
            XianBoComboBox.SelectedIndexChanged += XianBoComboBox_SelectedIndexChanged;
            colorPickerControl1.ColorChanged += colorPickerControl1_ColorChanged;
            Jx75uvCheckBox.Visible = !ISRealView;
        }

        private void colorPickerControl1_ColorChanged(object sender, EventArgs e)
        {
            m_Table.PenColor = colorPickerControl1.Color;
            change();
        }

        void XianBoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_Table.SingleNotch = XianBoComboBox.SelectedItem.ToString();
            change();
        }

        void LowPassComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_Table.LowPass = LowPassComboBox.SelectedItem.ToString();
            change();
        }

        void HighPassComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_Table.HighPass = HighPassComboBox.SelectedItem.ToString();
            change();
        }

        private void CbSensi_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_Table.PixelRate = ChannelTable.getSensitivity(CbSensi.SelectedItem);
            change();
        }
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
        private bool m_init = false;
        private ChannelTable m_Table = null;
        public void Init(ChannelTable item)
        {
            m_Table = item;
            ChannelName.Text = item.Name;
            MaxValueTextBox.Text = item.MaxValue.ToString();
            MinValueTextBox.Text = item.MinValue.ToString();
            string[] rangs = new string[] { "Off" };
            string ID = m_Table.ID.Replace("Clone_", "").Replace("Append_", "").Split(':')[0];
            DataBaseCom.Doc_Channel find = Channel.Default.ChannelProperties.Find(t => t.Name == ID);
            string unit = "";
            if (find != null)
            {
                rangs = find.strPixelRangArray;
                unit = find.Unit;
            }
            CbSensi.Items.AddRange(rangs);
            HighPassComboBox.Items.Add("Off");
            HighPassComboBox.Items.AddRange(Channel.Default.channel_FilterDropDownHandle(ID, "HighPass"));
            LowPassComboBox.Items.Add("Off");
            LowPassComboBox.Items.AddRange(Channel.Default.channel_FilterDropDownHandle(ID, "LowPass"));
            XianBoComboBox.Items.Add("Off");
            XianBoComboBox.Items.AddRange(Channel.Default.channel_FilterDropDownHandle(ID, "SingleNotch"));
            CbSensi.Text = ChannelTable.setSensitivity(item.PixelRate, unit == "Pa");
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
            if (m_Table.TimeSpan < 500)
                Jx75uvCheckBox.Enabled = false;
            else
                Jx75uvCheckBox.Checked = m_Table.DBaseLineVisible;
            colorPickerControl1.Color = item.PenColor;
            HighPassComboBox.Text = item.HighPass;
            LowPassComboBox.Text = item.LowPass;
            XianBoComboBox.Text = item.SingleNotch;
            m_init = true;
        }
        public delegate void UpdateItemDelegate(ChannelTable channel);
        /// <summary>
        /// 通道属性更改时发生
        /// </summary>
        public event UpdateItemDelegate UpdateItemHandle;
        private void change()
        {
            if (UpdateItemHandle != null)
                UpdateItemHandle.Invoke(m_Table);
        }

        private void MaxValueTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
           DataModel.InputTextCheck.floatvalue_KeyPress(sender,e);
        }

        private void MirrorCheckBox_CheckedChanged(object sender)
        {
            if (m_init)
            {
                m_Table.Antipole = MirrorCheckBox.Checked;
                change();
            }
        }

        private void Jx75uvCheckBox_CheckedChanged(object sender)
        {
            m_Table.DBaseLineVisible = Jx75uvCheckBox.Checked;
            change();
        }
        /// <summary>
        /// 是否为实时
        /// </summary>
        public bool ISRealView { set; get; }
    }
}
