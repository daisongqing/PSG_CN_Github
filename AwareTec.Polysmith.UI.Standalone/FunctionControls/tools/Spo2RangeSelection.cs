using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.UI.FunctionControls.tools
{
    public partial class Spo2RangeSelection : UserControl
    {
        private string m_strValue = "3";
        public Spo2RangeSelection()
        {
            this.InitializeComponent();
            base.Load += this.Spo2RangeSelection_Load;
        }

        private void Spo2RangeSelection_Load(object sender, EventArgs e)
        {
            this.Spo2DropCombBox.SelectedItem = string.Format("{0} %", this.m_strValue);
            this.Spo2DropCombBox.SelectedIndexChanged += this.Spo2DropCombBox_SelectedIndexChanged;
        }


        private void Spo2DropCombBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_strValue = this.Spo2DropCombBox.SelectedItem.ToString().Replace(" %", "");
        }


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
    }
}
