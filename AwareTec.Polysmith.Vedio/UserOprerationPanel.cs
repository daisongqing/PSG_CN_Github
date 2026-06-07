using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AwareTec.Polysmith.Vedio;

namespace AwareTec.Polysmith.Vedio
{
    public partial class UserOprerationPanel : UserControl
    {
        public UserOprerationPanel()
        {
            InitializeComponent();
        }
        private void btnLeft_MouseDown(object sender, MouseEventArgs e)
        {
            VedioManagement.Default.DeviceOperate(Convert.ToInt16((sender as Control).Tag));
        }

        private void btnLeft_MouseUp(object sender, MouseEventArgs e)
        {
            VedioManagement.Default.DeviceOperate(Convert.ToInt16((sender as Control).Tag), false);
        }

    }
}
