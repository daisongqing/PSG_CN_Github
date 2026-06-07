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
    public partial class ExportSelection : CloudSkinForm
    {
        #region 私有变量

        private bool m_DoctorDetailischeck = false;
        private bool m_PatientResultischeck = false;
        private bool m_Videoischeck = false;

        #endregion

        #region 公有变量

        /// <summary>
        /// 医生信息是否需要导出
        /// </summary>
        public bool DoctorDetailischeck
        {
            get => m_DoctorDetailischeck;
            set => m_DoctorDetailischeck = value;
        }

        /// <summary>
        /// 病例评分结果是否需要导出
        /// </summary>
        public bool PatientResultischeck
        {
            get => m_PatientResultischeck;
            set => m_PatientResultischeck = value;
        }

        /// <summary>
        /// 视频文件是否需要导出
        /// </summary>
        public bool Videoischeck
        {
            get => m_Videoischeck;
            set => m_Videoischeck = value;
        }

        #endregion

        #region 初始化 load/dispose/init

        /// <summary>
        /// 初始化
        /// </summary>
        public ExportSelection(bool hasvideo)
        {
            InitializeComponent();
            this.Load += ExportSelection_Load;

            PatientEDF.Checked = true;
            PatientEDF.Enabled = false;
            PatientDetail.Checked = true;
            PatientDetail.Enabled = false;
            if (hasvideo)
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
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportSelection_Load(object sender, EventArgs e)
        {
            this.ConfirmButton.Click += ConfirmButton_Click;
            this.CancelButton.Click += CancelButton_Click;
            this.AllSelectCheckBox.CheckedChanged += AllSelectCheckBox_CheckedChanged;
        }

        #endregion

        #region 私有方法

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

        #endregion

        #region 按钮方法

        /// <summary>
        /// 导出文件 取消按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            //DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("用户点击 导出数据-取消按钮");
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 导出文件 确定按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            m_DoctorDetailischeck = DoctorDetail.Checked;
            m_PatientResultischeck = PatientResult.Checked;
            m_Videoischeck = Video.Checked;
            base.DialogResult = DialogResult.OK;
            //DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("用户点击 导出数据-确定按钮，导出数据可选项，医生信息{0}，病例评分结果{1}，视频文件{2}", DoctorDetail.Checked ? "已选择" : "未选择", PatientResult.Checked ? "已选择" : "未选择", Video.Checked ? "已选择" : "未选择"));
            base.Close();
        }

        #endregion

        #region 公有方法

        #endregion

    }
}
