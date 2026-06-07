namespace AwareTec.Polysmith.UI.Block
{
    partial class Montage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Montage));
            this.abSelect = new System.Windows.Forms.DataGridViewEx();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.全勾ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.不勾ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colorPickerColumn1 = new System.Windows.Forms.ColorPickerColumn();
            this.panel1 = new pSystem.UI.ReaLTaiizor.Controls.Panel();
            this.SaveButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.SyntheticButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.DeriveButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.ResetButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.cbselectchannel = new pSystem.UI.ReaLTaiizor.Controls.AloneComboBox();
            this.panel2 = new pSystem.UI.ReaLTaiizor.Controls.Panel();
            this.State = new System.Windows.Forms.DataGridViewCheckBoxColumnEx();
            this.strName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeSpan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sensitivity = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ZoomRate = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.SingleNotch = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.HighPass = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.LowPass = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.PixelEnable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColorSelect = new System.Windows.Forms.ColorPickerColumn();
            this.Reserve = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsShowValue = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CalibrationsVisible = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Antipole = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AntipolePic = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.abSelect)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // abSelect
            // 
            this.abSelect.AllowUserToAddRows = false;
            this.abSelect.AllowUserToDeleteRows = false;
            this.abSelect.AllowUserToOrderColumns = true;
            this.abSelect.AllowUserToResizeColumns = false;
            this.abSelect.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            this.abSelect.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.abSelect.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.abSelect.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.abSelect.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.abSelect.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.abSelect.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.abSelect.ColumnHeadersHeight = 50;
            this.abSelect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.abSelect.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.State,
            this.strName,
            this.TimeSpan,
            this.MaxValue,
            this.MinValue,
            this.Index,
            this.ID,
            this.Sensitivity,
            this.ZoomRate,
            this.SingleNotch,
            this.HighPass,
            this.LowPass,
            this.PixelEnable,
            this.ColorSelect,
            this.Reserve,
            this.IsShowValue,
            this.CalibrationsVisible,
            this.Antipole,
            this.AntipolePic});
            this.abSelect.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.abSelect.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.abSelect.Location = new System.Drawing.Point(20, 0);
            this.abSelect.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.abSelect.MultiSelect = false;
            this.abSelect.Name = "abSelect";
            this.abSelect.RowIndexOffSet = 0;
            this.abSelect.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.abSelect.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.abSelect.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.abSelect.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.abSelect.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.abSelect.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(246)))), ((int)(((byte)(235)))));
            this.abSelect.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.abSelect.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.abSelect.RowTemplate.Height = 35;
            this.abSelect.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.abSelect.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.abSelect.Size = new System.Drawing.Size(1213, 763);
            this.abSelect.TabIndex = 17;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.全勾ToolStripMenuItem,
            this.不勾ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 48);
            // 
            // 全勾ToolStripMenuItem
            // 
            this.全勾ToolStripMenuItem.Name = "全勾ToolStripMenuItem";
            this.全勾ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.全勾ToolStripMenuItem.Tag = "1";
            this.全勾ToolStripMenuItem.Text = "全勾";
            this.全勾ToolStripMenuItem.Click += new System.EventHandler(this.全勾ToolStripMenuItem_Click);
            // 
            // 不勾ToolStripMenuItem
            // 
            this.不勾ToolStripMenuItem.Name = "不勾ToolStripMenuItem";
            this.不勾ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.不勾ToolStripMenuItem.Tag = "0";
            this.不勾ToolStripMenuItem.Text = "不勾";
            this.不勾ToolStripMenuItem.Click += new System.EventHandler(this.全勾ToolStripMenuItem_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "strName";
            this.dataGridViewTextBoxColumn1.FillWeight = 20F;
            this.dataGridViewTextBoxColumn1.HeaderText = "通道名称";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 150;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = false;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "TimeSpan";
            this.dataGridViewTextBoxColumn2.HeaderText = "采样周期(ms)";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 40;
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "MaxValue";
            this.dataGridViewTextBoxColumn3.FillWeight = 20F;
            this.dataGridViewTextBoxColumn3.HeaderText = "最大值";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 80;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "MinValue";
            this.dataGridViewTextBoxColumn4.FillWeight = 5F;
            this.dataGridViewTextBoxColumn4.HeaderText = "最小值";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 66;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Index";
            this.dataGridViewTextBoxColumn5.HeaderText = "通道序号";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 50;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Visible = false;
            this.dataGridViewTextBoxColumn5.Width = 50;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "ID";
            this.dataGridViewTextBoxColumn6.HeaderText = "标识";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Visible = false;
            // 
            // colorPickerColumn1
            // 
            this.colorPickerColumn1.DataPropertyName = "ColorSelect";
            this.colorPickerColumn1.HeaderText = "颜色";
            this.colorPickerColumn1.Name = "colorPickerColumn1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.SaveButton);
            this.panel1.Controls.Add(this.SyntheticButton);
            this.panel1.Controls.Add(this.DeriveButton);
            this.panel1.Controls.Add(this.ResetButton);
            this.panel1.Controls.Add(this.cbselectchannel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.EdgeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(3, 35);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(1243, 62);
            this.panel1.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.panel1.TabIndex = 19;
            this.panel1.Text = "panel1";
            // 
            // SaveButton
            // 
            this.SaveButton.BackColor = System.Drawing.Color.Transparent;
            this.SaveButton.BorderColor = System.Drawing.Color.Transparent;
            this.SaveButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SaveButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.SaveButton.ForeColor = System.Drawing.Color.White;
            this.SaveButton.Image = null;
            this.SaveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SaveButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.SaveButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.SaveButton.Location = new System.Drawing.Point(1158, 20);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.SaveButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.SaveButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.SaveButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.SaveButton.Size = new System.Drawing.Size(50, 32);
            this.SaveButton.TabIndex = 4;
            this.SaveButton.Text = "保存";
            this.SaveButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // SyntheticButton
            // 
            this.SyntheticButton.BackColor = System.Drawing.Color.Transparent;
            this.SyntheticButton.BorderColor = System.Drawing.Color.Transparent;
            this.SyntheticButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SyntheticButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.SyntheticButton.ForeColor = System.Drawing.Color.White;
            this.SyntheticButton.Image = null;
            this.SyntheticButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SyntheticButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.SyntheticButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.SyntheticButton.Location = new System.Drawing.Point(1102, 20);
            this.SyntheticButton.Name = "SyntheticButton";
            this.SyntheticButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.SyntheticButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.SyntheticButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.SyntheticButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.SyntheticButton.Size = new System.Drawing.Size(50, 32);
            this.SyntheticButton.TabIndex = 3;
            this.SyntheticButton.Text = "合成";
            this.SyntheticButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.SyntheticButton.Click += new System.EventHandler(this.SyntheticButton_Click);
            // 
            // DeriveButton
            // 
            this.DeriveButton.BackColor = System.Drawing.Color.Transparent;
            this.DeriveButton.BorderColor = System.Drawing.Color.Transparent;
            this.DeriveButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DeriveButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.DeriveButton.ForeColor = System.Drawing.Color.White;
            this.DeriveButton.Image = null;
            this.DeriveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DeriveButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.DeriveButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.DeriveButton.Location = new System.Drawing.Point(1046, 20);
            this.DeriveButton.Name = "DeriveButton";
            this.DeriveButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.DeriveButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.DeriveButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.DeriveButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.DeriveButton.Size = new System.Drawing.Size(50, 32);
            this.DeriveButton.TabIndex = 2;
            this.DeriveButton.Text = "派生";
            this.DeriveButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.DeriveButton.Click += new System.EventHandler(this.DeriveButton_Click);
            // 
            // ResetButton
            // 
            this.ResetButton.BackColor = System.Drawing.Color.Transparent;
            this.ResetButton.BorderColor = System.Drawing.Color.Transparent;
            this.ResetButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ResetButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ResetButton.ForeColor = System.Drawing.Color.White;
            this.ResetButton.Image = null;
            this.ResetButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ResetButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.ResetButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.ResetButton.Location = new System.Drawing.Point(990, 20);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.ResetButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.ResetButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.ResetButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.ResetButton.Size = new System.Drawing.Size(50, 32);
            this.ResetButton.TabIndex = 1;
            this.ResetButton.Text = "重置";
            this.ResetButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // cbselectchannel
            // 
            this.cbselectchannel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbselectchannel.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbselectchannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbselectchannel.EnabledCalc = true;
            this.cbselectchannel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbselectchannel.FormattingEnabled = true;
            this.cbselectchannel.ItemHeight = 20;
            this.cbselectchannel.Location = new System.Drawing.Point(20, 20);
            this.cbselectchannel.Name = "cbselectchannel";
            this.cbselectchannel.Size = new System.Drawing.Size(200, 26);
            this.cbselectchannel.TabIndex = 0;
            this.cbselectchannel.SelectedIndexChanged += new System.EventHandler(this.cbselectchannel_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.abSelect);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.EdgeColor = System.Drawing.Color.White;
            this.panel2.ForeColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(3, 97);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5);
            this.panel2.Size = new System.Drawing.Size(1243, 773);
            this.panel2.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.panel2.TabIndex = 20;
            this.panel2.Text = "panel2";
            // 
            // State
            // 
            this.State.DataPropertyName = "State";
            this.State.FillWeight = 5F;
            this.State.HeaderText = "";
            this.State.MinimumWidth = 55;
            this.State.Name = "State";
            this.State.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.State.Width = 55;
            // 
            // strName
            // 
            this.strName.DataPropertyName = "strName";
            this.strName.FillWeight = 10F;
            this.strName.HeaderText = "通道名称";
            this.strName.MinimumWidth = 120;
            this.strName.Name = "strName";
            this.strName.ReadOnly = false;
            this.strName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.strName.Width = 120;
            // 
            // TimeSpan
            // 
            this.TimeSpan.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TimeSpan.DataPropertyName = "TimeSpan";
            this.TimeSpan.FillWeight = 8F;
            this.TimeSpan.HeaderText = "采样频率(Hz)";
            this.TimeSpan.MinimumWidth = 100;
            this.TimeSpan.Name = "TimeSpan";
            this.TimeSpan.ReadOnly = true;
            this.TimeSpan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // MaxValue
            // 
            this.MaxValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MaxValue.DataPropertyName = "MaxValue";
            this.MaxValue.FillWeight = 5F;
            this.MaxValue.HeaderText = "最大值";
            this.MaxValue.MinimumWidth = 90;
            this.MaxValue.Name = "MaxValue";
            this.MaxValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MaxValue.Width = 90;
            // 
            // MinValue
            // 
            this.MinValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MinValue.DataPropertyName = "MinValue";
            this.MinValue.FillWeight = 5F;
            this.MinValue.HeaderText = "最小值";
            this.MinValue.MinimumWidth = 90;
            this.MinValue.Name = "MinValue";
            this.MinValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MinValue.Width = 90;
            // 
            // Index
            // 
            this.Index.DataPropertyName = "Index";
            this.Index.FillWeight = 5F;
            this.Index.HeaderText = "通道序号";
            this.Index.MinimumWidth = 55;
            this.Index.Name = "Index";
            this.Index.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Index.Visible = false;
            this.Index.Width = 55;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "标识";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            // 
            // Sensitivity
            // 
            this.Sensitivity.DataPropertyName = "Sensitivity";
            this.Sensitivity.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.Sensitivity.FillWeight = 10F;
            this.Sensitivity.HeaderText = "灵敏度";
            this.Sensitivity.MinimumWidth = 118;
            this.Sensitivity.Name = "Sensitivity";
            this.Sensitivity.Width = 118;
            // 
            // ZoomRate
            // 
            this.ZoomRate.DataPropertyName = "ZoomRate";
            this.ZoomRate.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.ZoomRate.HeaderText = "放大系数";
            this.ZoomRate.Items.AddRange(new object[] {
            "1",
            "2",
            "4",
            "6",
            "8",
            "12",
            "24"});
            this.ZoomRate.Name = "ZoomRate";
            this.ZoomRate.Visible = false;
            this.ZoomRate.Width = 10;
            // 
            // SingleNotch
            // 
            this.SingleNotch.DataPropertyName = "SingleNotch";
            this.SingleNotch.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.SingleNotch.FillWeight = 10F;
            this.SingleNotch.HeaderText = "陷波器";
            this.SingleNotch.MinimumWidth = 100;
            this.SingleNotch.Name = "SingleNotch";
            // 
            // HighPass
            // 
            this.HighPass.DataPropertyName = "HighPass";
            this.HighPass.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.HighPass.FillWeight = 10F;
            this.HighPass.HeaderText = "高通";
            this.HighPass.MinimumWidth = 100;
            this.HighPass.Name = "HighPass";
            // 
            // LowPass
            // 
            this.LowPass.DataPropertyName = "LowPass";
            this.LowPass.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.LowPass.FillWeight = 10F;
            this.LowPass.HeaderText = "低通";
            this.LowPass.MinimumWidth = 100;
            this.LowPass.Name = "LowPass";
            // 
            // PixelEnable
            // 
            this.PixelEnable.DataPropertyName = "PixelEnable";
            this.PixelEnable.HeaderText = "灵敏度使能";
            this.PixelEnable.Name = "PixelEnable";
            this.PixelEnable.Visible = false;
            // 
            // ColorSelect
            // 
            this.ColorSelect.DataPropertyName = "ColorSelect";
            this.ColorSelect.FillWeight = 40F;
            this.ColorSelect.HeaderText = "颜色";
            this.ColorSelect.MinimumWidth = 160;
            this.ColorSelect.Name = "ColorSelect";
            this.ColorSelect.Width = 160;
            // 
            // Reserve
            // 
            this.Reserve.DataPropertyName = "Reserve";
            this.Reserve.HeaderText = "备注";
            this.Reserve.Name = "Reserve";
            this.Reserve.Visible = false;
            // 
            // IsShowValue
            // 
            this.IsShowValue.DataPropertyName = "IsShowValue";
            this.IsShowValue.HeaderText = "是否显示值";
            this.IsShowValue.Name = "IsShowValue";
            this.IsShowValue.Visible = false;
            // 
            // CalibrationsVisible
            // 
            this.CalibrationsVisible.DataPropertyName = "CalibrationsVisible";
            this.CalibrationsVisible.HeaderText = "是否显示刻度";
            this.CalibrationsVisible.Name = "CalibrationsVisible";
            this.CalibrationsVisible.Visible = false;
            // 
            // Antipole
            // 
            this.Antipole.DataPropertyName = "Antipole";
            this.Antipole.HeaderText = "镜像";
            this.Antipole.Name = "Antipole";
            this.Antipole.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Antipole.Visible = false;
            this.Antipole.Width = 80;
            // 
            // AntipolePic
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = "null";
            this.AntipolePic.DefaultCellStyle = dataGridViewCellStyle3;
            this.AntipolePic.FillWeight = 5F;
            this.AntipolePic.HeaderText = "镜像";
            this.AntipolePic.MinimumWidth = 80;
            this.AntipolePic.Name = "AntipolePic";
            this.AntipolePic.Width = 80;
            // 
            // Montage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CanResize = false;
            this.ClientSize = new System.Drawing.Size(1249, 873);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Montage";
            this.ShowIcon = false;
            this.Text = "通道管理";
            ((System.ComponentModel.ISupportInitialize)(this.abSelect)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.DataGridViewEx abSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.ColorPickerColumn colorPickerColumn1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 全勾ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 不勾ToolStripMenuItem;
        private pSystem.UI.ReaLTaiizor.Controls.Panel panel1;
        private pSystem.UI.ReaLTaiizor.Controls.AloneComboBox cbselectchannel;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft ResetButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft SaveButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft SyntheticButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft DeriveButton;
        private pSystem.UI.ReaLTaiizor.Controls.Panel panel2;
        private System.Windows.Forms.DataGridViewCheckBoxColumnEx State;
        private System.Windows.Forms.DataGridViewTextBoxColumn strName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeSpan;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewComboBoxColumn Sensitivity;
        private System.Windows.Forms.DataGridViewComboBoxColumn ZoomRate;
        private System.Windows.Forms.DataGridViewComboBoxColumn SingleNotch;
        private System.Windows.Forms.DataGridViewComboBoxColumn HighPass;
        private System.Windows.Forms.DataGridViewComboBoxColumn LowPass;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PixelEnable;
        private System.Windows.Forms.ColorPickerColumn ColorSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reserve;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsShowValue;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CalibrationsVisible;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Antipole;
        private System.Windows.Forms.DataGridViewImageColumn AntipolePic;
    }
}