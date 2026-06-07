using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.UI
{
    public partial class Info : SkinForm
    {
        public Info()
        {
            InitializeComponent();
            this.Load += Info_Load;
            TimeValue = 0;
        }

        void Info_Load(object sender, EventArgs e)
        {
            label5.Text = ValueCount.ToString();
            MaxuvTextBox.Text = MaxValue.ToString("f2");
            MinuvTextBox.Text = MinValue.ToString("f2");
            FFvalueuvTextBox.Text = (MaxValue - MinValue).ToString("f2");
            MaxmmTextBox.Text = (MaxValue / PixelRate).ToString("f2");
            MinmmTextBox.Text = (MinValue / PixelRate).ToString("f2");
            FFvaluemmTextBox.Text = ((MaxValue - MinValue) / PixelRate).ToString("f2");
            label12.Text = string.Format("{0}{1}μV/mm", label12.Text, PixelRate);
            if (TimeValue != 0)
            {
                label13.Visible = true;
                TimeTextBox.Visible = true;
                TimeTextBox.Text = TimeValue.ToString("f2");
            }
        }
        /// <summary>
        /// 最大值
        /// </summary>
        public float MaxValue = 0;

        /// <summary>
        /// 最小值
        /// </summary>
        public float MinValue = 0;
        /// <summary>
        /// 点数
        /// </summary>
        public int ValueCount = 0;
        /// <summary>
        /// 灵敏度
        /// </summary>
        public float PixelRate { set; get; }
        /// <summary>
        /// 时间常数
        /// </summary>
        public float TimeValue { set; get; }
    }
}
