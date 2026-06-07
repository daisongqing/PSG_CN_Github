using System.Windows.Forms;

namespace AwareTec.Polysmith.Cloud.UI.Views
{
    partial class OrderRecordView
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DataGridView = new System.Windows.Forms.DataGridViewEx();
            this.RightClickMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.StartMonitoring = new System.Windows.Forms.ToolStripMenuItem();
            this.ContinueMonitoring = new System.Windows.Forms.ToolStripMenuItem();
            this.UploadEdf = new System.Windows.Forms.ToolStripMenuItem();
            this.EnterPlayBack = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowData = new System.Windows.Forms.ToolStripMenuItem();
            this.DownloadEdf = new System.Windows.Forms.ToolStripMenuItem();
            this.OrderRecordPaginationView = new AwareTec.Polysmith.Cloud.UI.Views.OrderRecordPaginationView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.OrderNumberCol = new System.Windows.Forms.DataGridViewLinkAndImageColumn();
            this.CategoryCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MedicalRecordNumberCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatientNameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatientGenderCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatientAgeCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatientBMICol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AppointmentMonitoringTimeCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UploadAndDownloadProgressCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProgressRateCol = new System.Windows.Forms.DataGridViewLinkAndImageColumn();
            this.ApprovalStatusCol = new System.Windows.Forms.DataGridViewLinkAndImageColumn();
            this.MapInterpnetationDoctorNameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AtlasCol = new System.Windows.Forms.DataGridViewLinkColumn();
            this.ReportCol = new System.Windows.Forms.DataGridViewLinkColumn();
            this.OrderCreationTimeCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).BeginInit();
            this.RightClickMenuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataGridView
            // 
            this.DataGridView.AllowUserToAddRows = false;
            this.DataGridView.AllowUserToDeleteRows = false;
            this.DataGridView.AllowUserToOrderColumns = true;
            this.DataGridView.AllowUserToResizeColumns = false;
            this.DataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 11F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridView.BackgroundColor = System.Drawing.Color.White;
            this.DataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 11F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridView.ColumnHeadersHeight = 40;
            this.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OrderNumberCol,
            this.CategoryCol,
            this.MedicalRecordNumberCol,
            this.PatientNameCol,
            this.PatientGenderCol,
            this.PatientAgeCol,
            this.PatientBMICol,
            this.AppointmentMonitoringTimeCol,
            this.UploadAndDownloadProgressCol,
            this.ProgressRateCol,
            this.ApprovalStatusCol,
            this.MapInterpnetationDoctorNameCol,
            this.AtlasCol,
            this.ReportCol,
            this.OrderCreationTimeCol});
            this.DataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridView.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.DataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.DataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(250)))), ((int)(((byte)(245)))));
            this.DataGridView.HeaderColLinearBegin = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(250)))), ((int)(((byte)(245)))));
            this.DataGridView.HeaderColLinearEnd = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(250)))), ((int)(((byte)(245)))));
            this.DataGridView.HeaderRowLinearBegin = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(250)))), ((int)(((byte)(245)))));
            this.DataGridView.HeaderRowLinearEnd = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(250)))), ((int)(((byte)(245)))));
            this.DataGridView.Location = new System.Drawing.Point(0, 0);
            this.DataGridView.MultiSelect = false;
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.RowHeadersWidth = 42;
            this.DataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DataGridView.RowIndexOffSet = 0;
            this.DataGridView.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DataGridView.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.DataGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.DataGridView.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.DataGridView.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.DataGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(246)))), ((int)(((byte)(235)))));
            this.DataGridView.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.DataGridView.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView.RowTemplate.Height = 35;
            this.DataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView.Size = new System.Drawing.Size(1800, 536);
            this.DataGridView.TabIndex = 0;
            this.DataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellDoubleClick);
            this.DataGridView.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView_CellMouseClick);
            this.DataGridView.SizeChanged += new System.EventHandler(this.DataGridView_SizeChanged);
            // 
            // RightClickMenuStrip
            // 
            this.RightClickMenuStrip.BackColor = System.Drawing.Color.White;
            this.RightClickMenuStrip.Font = new System.Drawing.Font("微软雅黑", 9.5F);
            this.RightClickMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.RightClickMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartMonitoring,
            this.ContinueMonitoring,
            this.UploadEdf,
            this.EnterPlayBack,
            this.ShowData,
            this.DownloadEdf});
            this.RightClickMenuStrip.Name = "RightClickMenuStrip";
            this.RightClickMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.RightClickMenuStrip.ShowImageMargin = false;
            this.RightClickMenuStrip.Size = new System.Drawing.Size(120, 160);
            // 
            // StartMonitoring
            // 
            this.StartMonitoring.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StartMonitoring.Font = new System.Drawing.Font("微软雅黑", 9.5F);
            this.StartMonitoring.Name = "StartMonitoring";
            this.StartMonitoring.Size = new System.Drawing.Size(119, 26);
            this.StartMonitoring.Text = "开始监测";
            this.StartMonitoring.Click += new System.EventHandler(this.StartMonitoring_Click);
            // 
            // ContinueMonitoring
            // 
            this.ContinueMonitoring.Name = "ContinueMonitoring";
            this.ContinueMonitoring.Size = new System.Drawing.Size(119, 26);
            this.ContinueMonitoring.Text = "继续监听";
            this.ContinueMonitoring.Click += new System.EventHandler(this.ContinueMonitoring_Click);
            // 
            // UploadEdf
            // 
            this.UploadEdf.Name = "UploadEdf";
            this.UploadEdf.Size = new System.Drawing.Size(119, 26);
            this.UploadEdf.Text = "上传edf";
            this.UploadEdf.Click += new System.EventHandler(this.UploadEdf_Click);
            // 
            // EnterPlayBack
            // 
            this.EnterPlayBack.Name = "EnterPlayBack";
            this.EnterPlayBack.Size = new System.Drawing.Size(119, 26);
            this.EnterPlayBack.Text = "进入回放";
            this.EnterPlayBack.Click += new System.EventHandler(this.EnterPlayBack_Click);
            // 
            // ShowData
            // 
            this.ShowData.Name = "ShowData";
            this.ShowData.Size = new System.Drawing.Size(119, 26);
            this.ShowData.Text = "查看数据";
            this.ShowData.Click += new System.EventHandler(this.ShowData_Click);
            // 
            // DownloadEdf
            // 
            this.DownloadEdf.Name = "DownloadEdf";
            this.DownloadEdf.Size = new System.Drawing.Size(119, 26);
            this.DownloadEdf.Text = "下载edf";
            this.DownloadEdf.Click += new System.EventHandler(this.DownloadEdf_Click);
            // 
            // OrderRecordPaginationView
            // 
            this.OrderRecordPaginationView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(252)))), ((int)(((byte)(251)))));
            this.OrderRecordPaginationView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OrderRecordPaginationView.Location = new System.Drawing.Point(0, 0);
            this.OrderRecordPaginationView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OrderRecordPaginationView.Name = "OrderRecordPaginationView";
            this.OrderRecordPaginationView.OrderRecordPaginationViewModel = null;
            this.OrderRecordPaginationView.Size = new System.Drawing.Size(1800, 69);
            this.OrderRecordPaginationView.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.DataGridView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1800, 536);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.OrderRecordPaginationView);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 467);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1800, 60);
            this.panel2.TabIndex = 3;
            // 
            // OrderNumberCol
            // 
            this.OrderNumberCol.ActiveLinkColor = System.Drawing.Color.Black;
            this.OrderNumberCol.FillWeight = 10F;
            this.OrderNumberCol.HeaderText = "订单号";
            this.OrderNumberCol.Image = null;
            this.OrderNumberCol.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.OrderNumberCol.LinkColor = System.Drawing.Color.Black;
            this.OrderNumberCol.MinimumWidth = 120;
            this.OrderNumberCol.Name = "OrderNumberCol";
            this.OrderNumberCol.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.OrderNumberCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.OrderNumberCol.VisitedLinkColor = System.Drawing.Color.Black;
            this.OrderNumberCol.ReadOnly = true;
            // 
            // CategoryCol
            // 
            this.CategoryCol.FillWeight = 4F;
            this.CategoryCol.HeaderText = "类别";
            this.CategoryCol.MinimumWidth = 55;
            this.CategoryCol.Name = "CategoryCol";
            this.CategoryCol.ReadOnly = true;
            // 
            // MedicalRecordNumberCol
            // 
            this.MedicalRecordNumberCol.FillWeight = 8F;
            this.MedicalRecordNumberCol.HeaderText = "病例号";
            this.MedicalRecordNumberCol.MinimumWidth = 100;
            this.MedicalRecordNumberCol.Name = "MedicalRecordNumberCol";
            this.MedicalRecordNumberCol.ReadOnly = true;
            // 
            // PatientNameCol
            // 
            this.PatientNameCol.FillWeight = 5F;
            this.PatientNameCol.HeaderText = "病人姓名";
            this.PatientNameCol.MinimumWidth = 100;
            this.PatientNameCol.Name = "PatientNameCol";
            this.PatientNameCol.ReadOnly = true;
            // 
            // PatientGenderCol
            // 
            this.PatientGenderCol.FillWeight = 4F;
            this.PatientGenderCol.HeaderText = "性别";
            this.PatientGenderCol.MinimumWidth = 45;
            this.PatientGenderCol.Name = "PatientGenderCol";
            this.PatientGenderCol.ReadOnly = true;
            // 
            // PatientAgeCol
            // 
            this.PatientAgeCol.FillWeight = 3F;
            this.PatientAgeCol.HeaderText = "年龄";
            this.PatientAgeCol.MinimumWidth = 50;
            this.PatientAgeCol.Name = "PatientAgeCol";
            this.PatientAgeCol.ReadOnly = true;
            // 
            // PatientBMICol
            // 
            this.PatientBMICol.FillWeight = 4F;
            this.PatientBMICol.HeaderText = "BMI";
            this.PatientBMICol.MinimumWidth = 50;
            this.PatientBMICol.Name = "PatientBMICol";
            this.PatientBMICol.ReadOnly = true;
            // 
            // AppointmentMonitoringTimeCol
            // 
            this.AppointmentMonitoringTimeCol.FillWeight = 15F;
            this.AppointmentMonitoringTimeCol.HeaderText = "预约监测时间";
            this.AppointmentMonitoringTimeCol.MinimumWidth = 150;
            this.AppointmentMonitoringTimeCol.Name = "AppointmentMonitoringTimeCol";
            this.AppointmentMonitoringTimeCol.ReadOnly = true;
            // 
            // UploadAndDownloadProgressCol
            // 
            this.UploadAndDownloadProgressCol.FillWeight = 10F;
            this.UploadAndDownloadProgressCol.HeaderText = "文件传输进度";
            this.UploadAndDownloadProgressCol.MinimumWidth = 120;
            this.UploadAndDownloadProgressCol.Name = "UploadAndDownloadProgressCol";
            this.UploadAndDownloadProgressCol.ReadOnly = true;
            // 
            // ProgressRateCol
            // 
            this.ProgressRateCol.ActiveLinkColor = System.Drawing.Color.Black;
            this.ProgressRateCol.FillWeight = 10F;
            this.ProgressRateCol.HeaderText = "进度";
            this.ProgressRateCol.Image = null;
            this.ProgressRateCol.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.ProgressRateCol.LinkColor = System.Drawing.Color.Black;
            this.ProgressRateCol.MinimumWidth = 120;
            this.ProgressRateCol.Name = "ProgressRateCol";
            this.ProgressRateCol.ReadOnly = true;
            this.ProgressRateCol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ProgressRateCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ProgressRateCol.VisitedLinkColor = System.Drawing.Color.Black;
            // 
            // ApprovalStatusCol
            // 
            this.ApprovalStatusCol.ActiveLinkColor = System.Drawing.Color.Black;
            this.ApprovalStatusCol.FillWeight = 10F;
            this.ApprovalStatusCol.HeaderText = "审核状态";
            this.ApprovalStatusCol.Image = null;
            this.ApprovalStatusCol.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.ApprovalStatusCol.LinkColor = System.Drawing.Color.Black;
            this.ApprovalStatusCol.MinimumWidth = 100;
            this.ApprovalStatusCol.Name = "ApprovalStatusCol";
            this.ApprovalStatusCol.ReadOnly = true;
            this.ApprovalStatusCol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ApprovalStatusCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ApprovalStatusCol.VisitedLinkColor = System.Drawing.Color.Black;
            // 
            // MapInterpnetationDoctorNameCol
            // 
            this.MapInterpnetationDoctorNameCol.FillWeight = 5F;
            this.MapInterpnetationDoctorNameCol.HeaderText = "判图医师";
            this.MapInterpnetationDoctorNameCol.MinimumWidth = 120;
            this.MapInterpnetationDoctorNameCol.Name = "MapInterpnetationDoctorNameCol";
            this.MapInterpnetationDoctorNameCol.ReadOnly = true;
            // 
            // AtlasCol
            // 
            this.AtlasCol.ActiveLinkColor = System.Drawing.Color.Black;
            this.AtlasCol.FillWeight = 5F;
            this.AtlasCol.HeaderText = "图谱";
            this.AtlasCol.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.AtlasCol.LinkColor = System.Drawing.Color.Black;
            this.AtlasCol.MinimumWidth = 60;
            this.AtlasCol.Name = "AtlasCol";
            this.AtlasCol.ReadOnly = true;
            this.AtlasCol.VisitedLinkColor = System.Drawing.Color.Black;
            // 
            // ReportCol
            // 
            this.ReportCol.ActiveLinkColor = System.Drawing.Color.Black;
            this.ReportCol.FillWeight = 5F;
            this.ReportCol.HeaderText = "报告";
            this.ReportCol.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.ReportCol.LinkColor = System.Drawing.Color.Black;
            this.ReportCol.MinimumWidth = 60;
            this.ReportCol.Name = "ReportCol";
            this.ReportCol.ReadOnly = true;
            this.ReportCol.VisitedLinkColor = System.Drawing.Color.Black;
            // 
            // OrderCreationTimeCol
            // 
            this.OrderCreationTimeCol.FillWeight = 15F;
            this.OrderCreationTimeCol.HeaderText = "订单创建时间";
            this.OrderCreationTimeCol.MinimumWidth = 150;
            this.OrderCreationTimeCol.Name = "OrderCreationTimeCol";
            // 
            // OrderRecordView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "OrderRecordView";
            this.Size = new System.Drawing.Size(1800, 536);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).EndInit();
            this.RightClickMenuStrip.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewEx DataGridView;
        private ContextMenuStrip RightClickMenuStrip;
        private ToolStripMenuItem StartMonitoring;
        private ToolStripMenuItem UploadEdf;
        private ToolStripMenuItem EnterPlayBack;
        private ToolStripMenuItem ShowData;
        private ToolStripMenuItem DownloadEdf;
        private ToolStripMenuItem ContinueMonitoring;
        private OrderRecordPaginationView OrderRecordPaginationView;
        private Panel panel1;
        private Panel panel2;
        private DataGridViewLinkAndImageColumn OrderNumberCol;
        private DataGridViewTextBoxColumn CategoryCol;
        private DataGridViewTextBoxColumn MedicalRecordNumberCol;
        private DataGridViewTextBoxColumn PatientNameCol;
        private DataGridViewTextBoxColumn PatientGenderCol;
        private DataGridViewTextBoxColumn PatientAgeCol;
        private DataGridViewTextBoxColumn PatientBMICol;
        private DataGridViewTextBoxColumn AppointmentMonitoringTimeCol;
        private DataGridViewTextBoxColumn UploadAndDownloadProgressCol;
        private DataGridViewLinkAndImageColumn ProgressRateCol;
        private DataGridViewLinkAndImageColumn ApprovalStatusCol;
        private DataGridViewTextBoxColumn MapInterpnetationDoctorNameCol;
        private DataGridViewLinkColumn AtlasCol;
        private DataGridViewLinkColumn ReportCol;
        private DataGridViewTextBoxColumn OrderCreationTimeCol;
    }
}
