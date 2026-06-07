using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Ctrl;

namespace AwareTec.Polysmith.UI.FunctionControls.tools
{
    public partial class ExportSelection : SkinForm
    {
        public ExportSelection()
        {
            InitializeComponent();
            PatientEDF.Checked = true;
            PatientEDF.Enabled = false;
            PatientDetail.Checked = true;
            PatientDetail.Enabled = false;
        }
        public ExportSelection(bool hasvideo)
        {
            InitializeComponent();
            PatientEDF.Checked = true;
            PatientEDF.Enabled = false;
            PatientDetail.Checked = true;
            PatientDetail.Enabled = false;
            if(hasvideo)
            {
                Video.Checked = false;
                Video.Visible = true;
            }
            else
            {
                Video.Checked = false;
                Video.Visible = false;
            }
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            DataModel.LogInstance.Default.AddLog("用户点击 导出数据-取消按钮");
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
        public bool DoctorDetailischeck = false;
        public bool PatientResultischeck = false;
        public bool Videoischeck = false;
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            DoctorDetailischeck = DoctorDetail.Checked;
            PatientResultischeck = PatientResult.Checked;
            Videoischeck = Video.Checked;
            base.DialogResult = DialogResult.OK;
            DataModel.LogInstance.Default.AddLog(string.Format("用户点击 导出数据-确定按钮，导出数据可选项，医生信息{0}，病例评分结果{1}，视频文件{2}",  DoctorDetail.Checked? "已选择" : "未选择", PatientResult.Checked? "已选择" : "未选择", Video.Checked? "已选择" : "未选择"));
            base.Close();
        }
        /// <summary>
        /// 全选/不选
        /// </summary>
        /// <param name="sender"></param>
        private void AllSelectCheckBox_CheckedChanged(object sender)
        {
            bool state = AllSelectCheckBox.Checked;
            foreach (Control c in panel1.Controls)
            {
                if (c is pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox)
                {
                    if (c.Name != AllSelectCheckBox.Name && c.Enabled && c.Visible)
                    {
                        (c as pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox).Checked = state;
                    }
                }
            }
        }
    }
}
