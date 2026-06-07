using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.UI.FunctionControls
{
    public partial class About : SkinForm
    {
        public About()
        {
            InitializeComponent();
        }

        private void _CompanyLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(_CompanyLink.Text);
            }
            catch(Exception ee)
            {
                AhDung.MessageTip.ShowWarning("请将除IE浏览器之外的其他浏览器设为默认浏览器，再打开网址");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:office@hxgroup.co?Subject=[Feedback] Feedback");
        }
    }
}
