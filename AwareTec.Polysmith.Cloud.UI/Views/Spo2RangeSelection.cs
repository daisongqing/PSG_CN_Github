using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.Cloud.UI.Views
{
    public partial class Spo2RangeSelection : UserControl
    {
        #region 私有变量

        private string m_strValue = "3";

        #endregion

        #region 公有变量

        /// <summary>
        /// 血氧值
        /// </summary>
        public string Value
        {
            get
            {
                return this.m_strValue;
            }
            set
            {
                this.m_strValue = value;
            }
        }

        #endregion

        #region 初始化 load/dispose/init

        /// <summary>
        /// 初始化
        /// </summary>
        public Spo2RangeSelection()
        {
            InitializeComponent();
            this.Load += Spo2RangeSelection_Load;
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Spo2RangeSelection_Load(object sender, EventArgs e)
        {
            this.Spo2DropCombBox.SelectedItem = string.Format("{0} %", this.m_strValue);
            this.Spo2DropCombBox.SelectedIndexChanged += Spo2DropCombBox_SelectedIndexChanged;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 下拉框选择改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Spo2DropCombBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_strValue = this.Spo2DropCombBox.SelectedItem.ToString().Replace(" %", "");
        }
        #endregion

        #region 按钮方法

        #endregion

        #region 公有方法

        #endregion

    }
}
