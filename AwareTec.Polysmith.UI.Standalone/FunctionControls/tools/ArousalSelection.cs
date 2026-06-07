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
    public partial class ArousalSelection : UserControl
    {
        public ArousalSelection()
        {
            InitializeComponent();
            this.Load += ArousalSelection_Load;
        }

        private void ArousalSelection_Load(object sender, EventArgs e)
        {
            string[] value = m_strValue.Split(';');
            foreach (Control c in this.Controls)
            {
                if (c is pSystem.UI.ReaLTaiizor.Controls.AloneComboBox)
                    {
                    string[] val = c.Tag.ToString().Split('\\');
                    pSystem.UI.ReaLTaiizor.Controls.AloneComboBox cb = c as pSystem.UI.ReaLTaiizor.Controls.AloneComboBox;
                    foreach (string s in val)
                    {
                        ChannelTable find = Channel.Default.CurrentSaveTable.Find(t => t.ChannelNum.ToString() == s);
                        if (find == null)
                            continue;
                        else
                        {
                            cb.Items.Add(find.Name);
                        }
                        if (value.Contains(s))
                        {
                            cb.Text = find.Name;
                        }
                    }
                }
            }

        }

        private string m_strValue = "1";
        public string Value
        {
            set
            {
                m_strValue = value;
            }
            get
            {
                return m_strValue;
            }
        }

        private void CbBrain_SelectedIndexChanged(object sender, EventArgs e)
        {
            pSystem.UI.ReaLTaiizor.Controls.AloneComboBox bb =sender as pSystem.UI.ReaLTaiizor.Controls.AloneComboBox;
            string[] ss = m_strValue.Split(';');
            ss[0]=Channel.Default.CurrentSaveTable.Find(t=>t.Name==bb.Text).ChannelNum.ToString();
            m_strValue = string.Join(";",ss);
        }


    }
}
