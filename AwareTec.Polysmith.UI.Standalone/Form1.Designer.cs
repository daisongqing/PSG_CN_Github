using System.Windows.Forms;
namespace AwareTec.Polysmith.UI
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.停止ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlEx1 = new System.Windows.Forms.TabControlEx();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panelEx2 = new DotNetCtl.UserControls.PanelEx();
            this.buttonEx4 = new System.Windows.Forms.ButtonEx();
            this.buttonEx2 = new System.Windows.Forms.ButtonEx();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxEx4 = new System.Windows.Forms.ComboBoxEx();
            this.comboBoxEx3 = new System.Windows.Forms.ComboBoxEx();
            this.comboBoxEx2 = new System.Windows.Forms.ComboBoxEx();
            this.smithChart1 = new AwareTec.Polysmith.pChart.SmithChart();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.abSelect = new System.Windows.Forms.DataGridViewEx();
            this.strName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FrameCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MissCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FrameIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonEx3 = new System.Windows.Forms.ButtonEx();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.高通滤波ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.低通滤波ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stoolStripMenuItem_30 = new System.Windows.Forms.ToolStripMenuItem();
            this.hzToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.陷波器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hzToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.panelEx1 = new DotNetCtl.UserControls.PanelEx();
            this.buttonEx5 = new System.Windows.Forms.ButtonEx();
            this.buttonEx1 = new System.Windows.Forms.ButtonEx();
            this.comboBoxEx1 = new System.Windows.Forms.ComboBoxEx();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxEx5 = new System.Windows.Forms.ComboBoxEx();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.userDisplay = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.msgDisplay = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel8 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel9 = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.tabControlEx1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.abSelect)).BeginInit();
            this.panel1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.MaxLength = 1000000000;
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(690, 420);
            this.textBox1.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.停止ToolStripMenuItem,
            this.清空ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 48);
            // 
            // 停止ToolStripMenuItem
            // 
            this.停止ToolStripMenuItem.Name = "停止ToolStripMenuItem";
            this.停止ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.停止ToolStripMenuItem.Text = "停止";
            this.停止ToolStripMenuItem.Click += new System.EventHandler(this.停止ToolStripMenuItem_Click);
            // 
            // 清空ToolStripMenuItem
            // 
            this.清空ToolStripMenuItem.Name = "清空ToolStripMenuItem";
            this.清空ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.清空ToolStripMenuItem.Text = "清空";
            this.清空ToolStripMenuItem.Click += new System.EventHandler(this.清空ToolStripMenuItem_Click);
            // 
            // tabControlEx1
            // 
            this.tabControlEx1.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(79)))), ((int)(((byte)(125)))));
            this.tabControlEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(234)))), ((int)(((byte)(246)))));
            this.tabControlEx1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(208)))), ((int)(((byte)(232)))));
            this.tabControlEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(208)))), ((int)(((byte)(232)))));
            this.tabControlEx1.Controls.Add(this.tabPage1);
            this.tabControlEx1.Controls.Add(this.tabPage2);
            this.tabControlEx1.Controls.Add(this.tabPage3);
            this.tabControlEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlEx1.Location = new System.Drawing.Point(3, 78);
            this.tabControlEx1.Name = "tabControlEx1";
            this.tabControlEx1.SelectedIndex = 0;
            this.tabControlEx1.Size = new System.Drawing.Size(698, 429);
            this.tabControlEx1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panelEx2);
            this.tabPage1.Controls.Add(this.smithChart1);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(690, 399);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "趋势图";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panelEx2
            // 
            this.panelEx2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx2.Controls.Add(this.buttonEx4);
            this.panelEx2.Controls.Add(this.buttonEx2);
            this.panelEx2.Controls.Add(this.label3);
            this.panelEx2.Controls.Add(this.label4);
            this.panelEx2.Controls.Add(this.label2);
            this.panelEx2.Controls.Add(this.comboBoxEx4);
            this.panelEx2.Controls.Add(this.comboBoxEx3);
            this.panelEx2.Controls.Add(this.comboBoxEx2);
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.panelEx2.Size = new System.Drawing.Size(690, 51);
            this.panelEx2.TabIndex = 4;
            this.panelEx2.Title = null;
            // 
            // buttonEx4
            // 
            this.buttonEx4.Location = new System.Drawing.Point(460, 9);
            this.buttonEx4.Name = "buttonEx4";
            this.buttonEx4.Size = new System.Drawing.Size(44, 23);
            this.buttonEx4.TabIndex = 5;
            this.buttonEx4.Text = "暂停";
            this.buttonEx4.UseVisualStyleBackColor = true;
            this.buttonEx4.Click += new System.EventHandler(this.buttonEx4_Click);
            // 
            // buttonEx2
            // 
            this.buttonEx2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEx2.Location = new System.Drawing.Point(594, 9);
            this.buttonEx2.Name = "buttonEx2";
            this.buttonEx2.Size = new System.Drawing.Size(75, 23);
            this.buttonEx2.TabIndex = 4;
            this.buttonEx2.Text = "高级设置";
            this.buttonEx2.UseVisualStyleBackColor = true;
            this.buttonEx2.Click += new System.EventHandler(this.buttonEx2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(138, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "灵敏度";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(302, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "放大倍数";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "时间轴";
            // 
            // comboBoxEx4
            // 
            this.comboBoxEx4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEx4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxEx4.FormattingEnabled = true;
            this.comboBoxEx4.Items.AddRange(new object[] {
            "1 μV/mm",
            "2 μV/mm",
            "3 μV/mm",
            "5 μV/mm",
            "7 μV/mm",
            "10 μV/mm",
            "20 μV/mm",
            "30 μV/mm",
            "50 μV/mm",
            "70 μV/mm",
            "100 μV/mm",
            "200 μV/mm",
            "300 μV/mm",
            "500 μV/mm",
            "700 μV/mm",
            "1 mV/mm",
            "2 mV/mm",
            "3 mV/mm",
            "5 mV/mm",
            "7 mV/mm",
            "10 mV/mm",
            "20 mV/mm",
            "30 mV/mm",
            "50 mV/mm",
            "70 mV/mm",
            "100 mV/mm",
            "200 mV/mm",
            "300 mV/mm"});
            this.comboBoxEx4.Location = new System.Drawing.Point(185, 11);
            this.comboBoxEx4.Name = "comboBoxEx4";
            this.comboBoxEx4.Size = new System.Drawing.Size(96, 20);
            this.comboBoxEx4.TabIndex = 2;
            this.comboBoxEx4.SelectedIndexChanged += new System.EventHandler(this.comboBoxEx2_SelectedIndexChanged);
            // 
            // comboBoxEx3
            // 
            this.comboBoxEx3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEx3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxEx3.FormattingEnabled = true;
            this.comboBoxEx3.Items.AddRange(new object[] {
            "1",
            "2",
            "4",
            "6",
            "8",
            "12",
            "24"});
            this.comboBoxEx3.Location = new System.Drawing.Point(361, 11);
            this.comboBoxEx3.Name = "comboBoxEx3";
            this.comboBoxEx3.Size = new System.Drawing.Size(63, 20);
            this.comboBoxEx3.TabIndex = 2;
            this.comboBoxEx3.SelectedIndexChanged += new System.EventHandler(this.comboBoxEx2_SelectedIndexChanged);
            // 
            // comboBoxEx2
            // 
            this.comboBoxEx2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEx2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxEx2.FormattingEnabled = true;
            this.comboBoxEx2.Items.AddRange(new object[] {
            "1(s)",
            "5(s)",
            "10(s)",
            "30(s)",
            "60(s)",
            "5(min)",
            "10(min)",
            "30(min)",
            "60(min)",
            "2(h)",
            "8(h)"});
            this.comboBoxEx2.Location = new System.Drawing.Point(56, 11);
            this.comboBoxEx2.Name = "comboBoxEx2";
            this.comboBoxEx2.Size = new System.Drawing.Size(69, 20);
            this.comboBoxEx2.TabIndex = 2;
            this.comboBoxEx2.SelectedIndexChanged += new System.EventHandler(this.comboBoxEx2_SelectedIndexChanged);
            // 
            // smithChart1
            // 
            this.smithChart1.BackColor = System.Drawing.SystemColors.Window;
            this.smithChart1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.smithChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smithChart1.IsRealTimeCurve = true;
            this.smithChart1.IsStop = false;
            this.smithChart1.Location = new System.Drawing.Point(3, 3);
            this.smithChart1.Name = "smithChart1";
            this.smithChart1.Size = new System.Drawing.Size(684, 393);
            this.smithChart1.TabIndex = 3;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(696, 426);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "日志信息";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.abSelect);
            this.tabPage3.Controls.Add(this.panel1);
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(690, 399);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "分析";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // abSelect
            // 
            this.abSelect.AllowDrop = true;
            this.abSelect.AllowUserToAddRows = false;
            this.abSelect.AllowUserToResizeColumns = false;
            this.abSelect.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.abSelect.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.abSelect.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.abSelect.BackgroundColor = System.Drawing.Color.White;
            this.abSelect.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.abSelect.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.abSelect.ColumnHeadersHeight = 25;
            this.abSelect.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.strName,
            this.FrameCount,
            this.MissCount,
            this.ID,
            this.FrameIndex});
            this.abSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.abSelect.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.abSelect.Location = new System.Drawing.Point(0, 28);
            this.abSelect.MultiSelect = false;
            this.abSelect.Name = "abSelect";
            this.abSelect.RowTemplate.Height = 23;
            this.abSelect.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.abSelect.Size = new System.Drawing.Size(690, 371);
            this.abSelect.TabIndex = 18;
            // 
            // strName
            // 
            this.strName.DataPropertyName = "strName";
            this.strName.FillWeight = 20F;
            this.strName.HeaderText = "通道名称";
            this.strName.MinimumWidth = 150;
            this.strName.Name = "strName";
            this.strName.ReadOnly = true;
            this.strName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FrameCount
            // 
            this.FrameCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.FrameCount.DataPropertyName = "FrameCount";
            this.FrameCount.HeaderText = "采样帧总数";
            this.FrameCount.MinimumWidth = 40;
            this.FrameCount.Name = "FrameCount";
            this.FrameCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FrameCount.Width = 71;
            // 
            // MissCount
            // 
            this.MissCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MissCount.DataPropertyName = "MissCount";
            this.MissCount.FillWeight = 20F;
            this.MissCount.HeaderText = "缺失帧数量";
            this.MissCount.MinimumWidth = 80;
            this.MissCount.Name = "MissCount";
            this.MissCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MissCount.Width = 80;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            // 
            // FrameIndex
            // 
            this.FrameIndex.DataPropertyName = "FrameIndex";
            this.FrameIndex.HeaderText = "FrameIndex";
            this.FrameIndex.Name = "FrameIndex";
            this.FrameIndex.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonEx3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(690, 28);
            this.panel1.TabIndex = 20;
            // 
            // buttonEx3
            // 
            this.buttonEx3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEx3.Location = new System.Drawing.Point(610, 2);
            this.buttonEx3.Name = "buttonEx3";
            this.buttonEx3.Size = new System.Drawing.Size(75, 23);
            this.buttonEx3.TabIndex = 19;
            this.buttonEx3.Text = "分析";
            this.buttonEx3.UseVisualStyleBackColor = true;
            this.buttonEx3.Click += new System.EventHandler(this.buttonEx3_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.高通滤波ToolStripMenuItem,
            this.低通滤波ToolStripMenuItem,
            this.陷波器ToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(125, 70);
            // 
            // 高通滤波ToolStripMenuItem
            // 
            this.高通滤波ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sToolStripMenuItem1,
            this.sToolStripMenuItem});
            this.高通滤波ToolStripMenuItem.Name = "高通滤波ToolStripMenuItem";
            this.高通滤波ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.高通滤波ToolStripMenuItem.Text = "高通滤波";
            // 
            // sToolStripMenuItem1
            // 
            this.sToolStripMenuItem1.Name = "sToolStripMenuItem1";
            this.sToolStripMenuItem1.Size = new System.Drawing.Size(99, 22);
            this.sToolStripMenuItem1.Text = "0.3s";
            this.sToolStripMenuItem1.Click += new System.EventHandler(this.sToolStripMenuItem_Click);
            // 
            // sToolStripMenuItem
            // 
            this.sToolStripMenuItem.Name = "sToolStripMenuItem";
            this.sToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.sToolStripMenuItem.Text = "1s";
            this.sToolStripMenuItem.Click += new System.EventHandler(this.sToolStripMenuItem_Click);
            // 
            // 低通滤波ToolStripMenuItem
            // 
            this.低通滤波ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stoolStripMenuItem_30,
            this.hzToolStripMenuItem});
            this.低通滤波ToolStripMenuItem.Name = "低通滤波ToolStripMenuItem";
            this.低通滤波ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.低通滤波ToolStripMenuItem.Text = "低通滤波";
            // 
            // stoolStripMenuItem_30
            // 
            this.stoolStripMenuItem_30.Name = "stoolStripMenuItem_30";
            this.stoolStripMenuItem_30.Size = new System.Drawing.Size(105, 22);
            this.stoolStripMenuItem_30.Text = "30Hz";
            this.stoolStripMenuItem_30.Click += new System.EventHandler(this.sToolStripMenuItem_Click);
            // 
            // hzToolStripMenuItem
            // 
            this.hzToolStripMenuItem.Name = "hzToolStripMenuItem";
            this.hzToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.hzToolStripMenuItem.Text = "60Hz";
            this.hzToolStripMenuItem.Click += new System.EventHandler(this.sToolStripMenuItem_Click);
            // 
            // 陷波器ToolStripMenuItem
            // 
            this.陷波器ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hzToolStripMenuItem1});
            this.陷波器ToolStripMenuItem.Name = "陷波器ToolStripMenuItem";
            this.陷波器ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.陷波器ToolStripMenuItem.Text = "陷波器";
            // 
            // hzToolStripMenuItem1
            // 
            this.hzToolStripMenuItem1.Name = "hzToolStripMenuItem1";
            this.hzToolStripMenuItem1.Size = new System.Drawing.Size(105, 22);
            this.hzToolStripMenuItem1.Text = "50Hz";
            this.hzToolStripMenuItem1.Click += new System.EventHandler(this.sToolStripMenuItem_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(234)))), ((int)(((byte)(246)))));
            this.panelEx1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelEx1.Controls.Add(this.buttonEx5);
            this.panelEx1.Controls.Add(this.buttonEx1);
            this.panelEx1.Controls.Add(this.comboBoxEx1);
            this.panelEx1.Controls.Add(this.label5);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Controls.Add(this.comboBoxEx5);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(3, 24);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.panelEx1.Size = new System.Drawing.Size(698, 54);
            this.panelEx1.TabIndex = 5;
            this.panelEx1.Title = null;
            // 
            // buttonEx5
            // 
            this.buttonEx5.Location = new System.Drawing.Point(596, 19);
            this.buttonEx5.Name = "buttonEx5";
            this.buttonEx5.Size = new System.Drawing.Size(75, 23);
            this.buttonEx5.TabIndex = 4;
            this.buttonEx5.Text = "开始采集";
            this.buttonEx5.UseVisualStyleBackColor = true;
            this.buttonEx5.Click += new System.EventHandler(this.buttonEx5_Click);
            // 
            // buttonEx1
            // 
            this.buttonEx1.Location = new System.Drawing.Point(482, 20);
            this.buttonEx1.Name = "buttonEx1";
            this.buttonEx1.Size = new System.Drawing.Size(75, 23);
            this.buttonEx1.TabIndex = 3;
            this.buttonEx1.Text = "启动";
            this.buttonEx1.UseVisualStyleBackColor = true;
            this.buttonEx1.Click += new System.EventHandler(this.buttonEx1_Click);
            // 
            // comboBoxEx1
            // 
            this.comboBoxEx1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEx1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxEx1.FormattingEnabled = true;
            this.comboBoxEx1.Location = new System.Drawing.Point(113, 22);
            this.comboBoxEx1.Name = "comboBoxEx1";
            this.comboBoxEx1.Size = new System.Drawing.Size(121, 20);
            this.comboBoxEx1.TabIndex = 2;
            this.comboBoxEx1.DropDown += new System.EventHandler(this.comboBoxEx1_DropDown);
            this.comboBoxEx1.SelectedValueChanged += new System.EventHandler(this.comboBoxEx1_SelectedValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(249, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "数据处理";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "选择通讯端口";
            // 
            // comboBoxEx5
            // 
            this.comboBoxEx5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEx5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxEx5.FormattingEnabled = true;
            this.comboBoxEx5.Items.AddRange(new object[] {
            "仅存储源报文",
            "仅存储解析后数据",
            "存储并显示",
            "全操作"});
            this.comboBoxEx5.Location = new System.Drawing.Point(319, 21);
            this.comboBoxEx5.Name = "comboBoxEx5";
            this.comboBoxEx5.Size = new System.Drawing.Size(107, 20);
            this.comboBoxEx5.TabIndex = 2;
            this.comboBoxEx5.SelectedIndexChanged += new System.EventHandler(this.comboBoxEx5_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userDisplay,
            this.toolStripStatusLabel3,
            this.toolStripProgressBar1,
            this.msgDisplay,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabel5,
            this.toolStripStatusLabel6,
            this.toolStripStatusLabel7,
            this.toolStripStatusLabel8,
            this.toolStripStatusLabel9});
            this.statusStrip1.Location = new System.Drawing.Point(3, 507);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusStrip1.Size = new System.Drawing.Size(698, 26);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // userDisplay
            // 
            this.userDisplay.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.userDisplay.Name = "userDisplay";
            this.userDisplay.Size = new System.Drawing.Size(0, 21);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(36, 21);
            this.toolStripStatusLabel3.Text = "准备";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(150, 20);
            this.toolStripProgressBar1.Visible = false;
            // 
            // msgDisplay
            // 
            this.msgDisplay.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.msgDisplay.Name = "msgDisplay";
            this.msgDisplay.Size = new System.Drawing.Size(167, 21);
            this.msgDisplay.Spring = true;
            this.msgDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 21);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(48, 21);
            this.toolStripStatusLabel1.Text = "体位：";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(48, 21);
            this.toolStripStatusLabel4.Text = "血氧：";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(48, 21);
            this.toolStripStatusLabel5.Text = "脉率：";
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(84, 21);
            this.toolStripStatusLabel6.Text = "热敏呼吸率：";
            // 
            // toolStripStatusLabel7
            // 
            this.toolStripStatusLabel7.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabel7.Name = "toolStripStatusLabel7";
            this.toolStripStatusLabel7.Size = new System.Drawing.Size(84, 21);
            this.toolStripStatusLabel7.Text = "压力呼吸率：";
            // 
            // toolStripStatusLabel8
            // 
            this.toolStripStatusLabel8.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabel8.Name = "toolStripStatusLabel8";
            this.toolStripStatusLabel8.Size = new System.Drawing.Size(84, 21);
            this.toolStripStatusLabel8.Text = "胸部呼吸率：";
            // 
            // toolStripStatusLabel9
            // 
            this.toolStripStatusLabel9.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabel9.Name = "toolStripStatusLabel9";
            this.toolStripStatusLabel9.Size = new System.Drawing.Size(84, 21);
            this.toolStripStatusLabel9.Text = "腹部呼吸率：";
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(61, 4);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 536);
            this.Controls.Add(this.tabControlEx1);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "睡眠检测仪调试软件 v1.0.0.8";
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabControlEx1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.abSelect)).EndInit();
            this.panel1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private pChart.SmithChart smithChart1;
        private System.Windows.Forms.TabControlEx tabControlEx1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private DotNetCtl.UserControls.PanelEx panelEx1;
        private System.Windows.Forms.ComboBoxEx comboBoxEx1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ButtonEx buttonEx1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 停止ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空ToolStripMenuItem;
        private DotNetCtl.UserControls.PanelEx panelEx2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBoxEx comboBoxEx2;
        private System.Windows.Forms.ButtonEx buttonEx2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridViewEx abSelect;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ButtonEx buttonEx3;
        private System.Windows.Forms.DataGridViewTextBoxColumn strName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FrameCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn MissCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FrameIndex;
        private ButtonEx buttonEx4;
        private Label label3;
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem 高通滤波ToolStripMenuItem;
        private ToolStripMenuItem sToolStripMenuItem;
        private ToolStripMenuItem 低通滤波ToolStripMenuItem;
        private ToolStripMenuItem hzToolStripMenuItem;
        private ToolStripMenuItem 陷波器ToolStripMenuItem;
        private ToolStripMenuItem hzToolStripMenuItem1;
        private ToolStripMenuItem stoolStripMenuItem_30;
        private Label label4;
        private ComboBoxEx comboBoxEx4;
        private ComboBoxEx comboBoxEx3;
        private ButtonEx buttonEx5;
        private Label label5;
        private ComboBoxEx comboBoxEx5;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel userDisplay;
        private ToolStripStatusLabel toolStripStatusLabel3;
        private ToolStripProgressBar toolStripProgressBar1;
        private ToolStripStatusLabel msgDisplay;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel4;
        private ToolStripStatusLabel toolStripStatusLabel5;
        private ToolStripStatusLabel toolStripStatusLabel6;
        private ToolStripStatusLabel toolStripStatusLabel7;
        private ToolStripMenuItem sToolStripMenuItem1;
        private ToolStripStatusLabel toolStripStatusLabel8;
        private ToolStripStatusLabel toolStripStatusLabel9;
        private ContextMenuStrip contextMenuStrip3;
    }
}

