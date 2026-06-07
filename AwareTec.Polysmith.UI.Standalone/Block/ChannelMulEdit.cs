using AwareTec.Polysmith.UI.DataModel;
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
    public partial class ChannelMulEdit : SkinForm
    {
        private BatchUnit m_batchUnit = new BatchUnit();
        public ChannelMulEdit()
        {
            InitializeComponent();
            this.Load += ChannelMulEdit_Load;
        }

        void ChannelMulEdit_Load(object sender, EventArgs e)
        {
            comboBoxEx1.SelectedIndex = 0;
            comboBoxEx2.SelectedIndex = 0;
            comboBoxEx3.SelectedIndex = 0;
            comboBoxEx4.SelectedIndex = 0;
        }

        private void RoundAngleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RoundAngleButton1_Click(object sender, EventArgs e)
        {
            m_batchUnit.PixelRateEnable = checkBoxEx2.Checked;
            if (m_batchUnit.PixelRateEnable)
            {
                m_batchUnit.PixelRate = comboBoxEx1.Text.Trim();
                if (m_batchUnit.PixelRate == "")
                {
                    AhDung.MessageTip.ShowError("请选择灵敏度");
                    return;
                }
            }
            m_batchUnit.HighPassEnable = checkBoxEx6.Checked;
            if (m_batchUnit.HighPassEnable)
            {
                m_batchUnit.HighPass = comboBoxEx2.Text.Trim();
                if (m_batchUnit.HighPass == "")
                {
                    AhDung.MessageTip.ShowError("请选择高通滤波器");
                    return;
                }
            }
            m_batchUnit.LowPassEnable = checkBoxEx7.Checked;
            if (m_batchUnit.LowPassEnable)
            {
                m_batchUnit.LowPass = comboBoxEx3.Text.Trim();
                if (m_batchUnit.LowPass == "")
                {
                    AhDung.MessageTip.ShowError("请选择低通滤波器");
                    return;
                }
            }
            m_batchUnit.SingleNotchEnable = checkBoxEx5.Checked;
            if (m_batchUnit.SingleNotchEnable)
            {
                m_batchUnit.SingleNotch = comboBoxEx4.Text.Trim();
                if (m_batchUnit.LowPass == "")
                {
                    AhDung.MessageTip.ShowError("请选择陷波器");
                    return;
                }
            }
            m_batchUnit.MaxValueEnable = checkBoxEx3.Checked;
            if (m_batchUnit.MaxValueEnable)
            {
                m_batchUnit.MaxValue = textBoxEx3.Text.Trim();
                if (m_batchUnit.MaxValue == "")
                {
                    AhDung.MessageTip.ShowError("请输入最大值");
                    return;
                }
            }
            m_batchUnit.MinValueEnable = checkBoxEx4.Checked;
            if (m_batchUnit.MinValueEnable)
            {
                m_batchUnit.MinValue = textBoxEx4.Text.Trim();
                if (m_batchUnit.MinValue == "")
                {
                    AhDung.MessageTip.ShowError("请输入最小值");
                    return;
                }
            }
            m_batchUnit.SampleSpanEnable = checkBoxEx1.Checked;
            if (m_batchUnit.SampleSpanEnable)
            {
                m_batchUnit.SampleSpan = textBoxEx1.Text.Trim();
                if (m_batchUnit.SampleSpan == "")
                {
                    AhDung.MessageTip.ShowError("请输入采样频率");
                    return;
                }
            }
            m_batchUnit.PenColorEnable = checkBoxEx8.Checked;
            m_batchUnit.PenColor = colorPickerControl1.Color;
            if (CommitBatchUnitHandle != null)
            {
                CommitBatchUnitHandle.BeginInvoke(m_batchUnit,null,null);
            }
            this.Close();
        }
        public delegate void CommitBatchUnitDelegate(BatchUnit unit);
        /// <summary>
        /// 提交批量修改完成时发生事件
        /// </summary>
        public event CommitBatchUnitDelegate CommitBatchUnitHandle;
        /// <summary>
        /// 界面参数初始化
        /// </summary>
        /// <param name="data"></param>
        public void Init(BatchUnit data)
        {
            m_batchUnit = data;
            if (m_batchUnit.PixelRateItems.Count == 0)
            {
                comboBoxEx1.Items.Add("Off");
            }
            else
                comboBoxEx1.Items.AddRange(m_batchUnit.PixelRateItems.ToArray());
            comboBoxEx2.Items.AddRange(m_batchUnit.HighPassItems.ToArray());
            comboBoxEx3.Items.AddRange(m_batchUnit.LowPassItems.ToArray());
            comboBoxEx4.Items.AddRange(m_batchUnit.SingleNotchItems.ToArray());
        }
    }
}
