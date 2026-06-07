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
    public partial class Info : CloudSkinForm
    {
        #region 私有变量

        private float m_MaxValue = 0;
        private float m_MinValue = 0;
        private int m_ValueCount = 0;
        private float m_PixelRate = 1;
        private float m_TimeValue = 0;

        #endregion

        #region 公有变量

        /// <summary>
        /// 最大值
        /// </summary>
        public float MaxValue
        {
            set => m_MaxValue = value;
            get => m_MaxValue;
        }

        /// <summary>
        /// 最小值
        /// </summary>
        public float MinValue
        {
            set => m_MinValue = value;
            get => m_MinValue;
        }

        /// <summary>
        /// 点数
        /// </summary>
        public int ValueCount
        {
            set => m_ValueCount = value;
            get => m_ValueCount;
        }

        /// <summary>
        /// 灵敏度
        /// </summary>
        public float PixelRate
        {
            set => m_PixelRate = value;
            get => m_PixelRate;
        }

        /// <summary>
        /// 时间常数
        /// </summary>
        public float TimeValue
        {
            set => m_TimeValue = value;
            get => m_TimeValue;
        }

        #endregion

        #region 初始化 load/dispose/init

        /// <summary>
        /// 初始化
        /// </summary>
        public Info()
        {
            InitializeComponent();
            this.Load += Info_Load;
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Info_Load(object sender, EventArgs e)
        {
            label5.Text = m_ValueCount.ToString();
            MaxuvTextBox.Text = m_MaxValue.ToString("f2");
            MinuvTextBox.Text = m_MinValue.ToString("f2");
            FFvalueuvTextBox.Text = (m_MaxValue - m_MinValue).ToString("f2");
            MaxmmTextBox.Text = (m_MaxValue / m_PixelRate).ToString("f2");
            MinmmTextBox.Text = (m_MinValue / m_PixelRate).ToString("f2");
            FFvaluemmTextBox.Text = ((m_MaxValue - m_MinValue) / m_PixelRate).ToString("f2");
            label12.Text = string.Format("{0}{1}μV/mm", label12.Text, m_PixelRate);
            if (m_TimeValue != 0)
            {
                label13.Visible = true;
                TimeTextBox.Visible = true;
                TimeTextBox.Text = m_TimeValue.ToString("f2");
            }
        }

        #endregion

        #region 私有方法

        #endregion

        #region 按钮方法

        #endregion

        #region 公有方法

        #endregion

    }
}
