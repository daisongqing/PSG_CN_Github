
namespace AwareTec.Polysmith.Cloud.UI.Forms.MainFormBlock
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
            this.abSelect = new System.Windows.Forms.DataGridViewEx();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AllCheckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NoCheckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colorPickerColumn1 = new System.Windows.Forms.ColorPickerColumn();
            this.panel1 = new pSystem.UI.ReaLTaiizor.Controls.Panel();
            this.SaveButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.DeriveButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.ResetButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.SelectChannelComBox = new pSystem.UI.ReaLTaiizor.Controls.AloneComboBox();
            this.panel2 = new pSystem.UI.ReaLTaiizor.Controls.Panel();
            this.State = new System.Windows.Forms.DataGridViewCheckBoxColumnEx();
            this.strName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sensitivity = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.SingleNotch = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.HighPass = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.LowPass = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.PixelEnable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColorSelect = new pSystem.UI.ReaLTaiizor.CustomCtrl.Picker.ColorSelectPickerColumn();
            this.Reserve = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsShowValue = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CalibrationsVisible = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Antipole = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AntipolePic = new System.Windows.Forms.DataGridViewImageColumn();
            this.ChannelID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DBaseLineVisible = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsClone = new System.Windows.Forms.DataGridViewCheckBoxColumn();
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
            this.abSelect.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.abSelect.BackgroundColor = System.Drawing.Color.White;
            this.abSelect.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.abSelect.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 11F);
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
            this.MaxValue,
            this.MinValue,
            this.Index,
            this.ID,
            this.Sensitivity,
            this.SingleNotch,
            this.HighPass,
            this.LowPass,
            this.PixelEnable,
            this.ColorSelect,
            this.Reserve,
            this.IsShowValue,
            this.CalibrationsVisible,
            this.Antipole,
            this.AntipolePic,
            this.ChannelID,
            this.DBaseLineVisible,
            this.IsClone});
            this.abSelect.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.abSelect.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(250)))), ((int)(((byte)(245)))));
            this.abSelect.HeaderColLinearBegin = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(250)))), ((int)(((byte)(245)))));
            this.abSelect.HeaderColLinearEnd = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(250)))), ((int)(((byte)(245)))));
            this.abSelect.HeaderRowLinearBegin = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(250)))), ((int)(((byte)(245)))));
            this.abSelect.HeaderRowLinearEnd = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(250)))), ((int)(((byte)(245)))));
            this.abSelect.Location = new System.Drawing.Point(20, 0);
            this.abSelect.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.abSelect.MultiSelect = false;
            this.abSelect.Name = "abSelect";
            this.abSelect.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
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
            this.abSelect.Size = new System.Drawing.Size(1186, 750);
            this.abSelect.TabIndex = 17;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AllCheckToolStripMenuItem,
            this.NoCheckToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 48);
            // 
            // AllCheckToolStripMenuItem
            // 
            this.AllCheckToolStripMenuItem.Name = "AllCheckToolStripMenuItem";
            this.AllCheckToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.AllCheckToolStripMenuItem.Tag = "1";
            this.AllCheckToolStripMenuItem.Text = "全勾";
            // 
            // NoCheckToolStripMenuItem
            // 
            this.NoCheckToolStripMenuItem.Name = "NoCheckToolStripMenuItem";
            this.NoCheckToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.NoCheckToolStripMenuItem.Tag = "0";
            this.NoCheckToolStripMenuItem.Text = "不勾";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "strName";
            this.dataGridViewTextBoxColumn1.FillWeight = 20F;
            this.dataGridViewTextBoxColumn1.HeaderText = "通道名称";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 150;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "TimeSpan";
            this.dataGridViewTextBoxColumn2.HeaderText = "采样周期(ms)";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 40;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
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
            this.panel1.Controls.Add(this.DeriveButton);
            this.panel1.Controls.Add(this.ResetButton);
            this.panel1.Controls.Add(this.SelectChannelComBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.EdgeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(3, 35);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(1220, 62);
            this.panel1.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.panel1.TabIndex = 19;
            this.panel1.Text = "panel1";
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.BackColor = System.Drawing.Color.Transparent;
            this.SaveButton.BorderColor = System.Drawing.Color.Transparent;
            this.SaveButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SaveButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SaveButton.ForeColor = System.Drawing.Color.White;
            this.SaveButton.Image = null;
            this.SaveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SaveButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.SaveButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.SaveButton.Location = new System.Drawing.Point(1156, 20);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.SaveButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.SaveButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.SaveButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.SaveButton.Size = new System.Drawing.Size(50, 32);
            this.SaveButton.TabIndex = 58;
            this.SaveButton.Text = "保存";
            this.SaveButton.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // DeriveButton
            // 
            this.DeriveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DeriveButton.BackColor = System.Drawing.Color.Transparent;
            this.DeriveButton.BorderColor = System.Drawing.Color.Transparent;
            this.DeriveButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DeriveButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DeriveButton.ForeColor = System.Drawing.Color.White;
            this.DeriveButton.Image = null;
            this.DeriveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DeriveButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.DeriveButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.DeriveButton.Location = new System.Drawing.Point(1098, 20);
            this.DeriveButton.Name = "DeriveButton";
            this.DeriveButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.DeriveButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.DeriveButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.DeriveButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.DeriveButton.Size = new System.Drawing.Size(50, 32);
            this.DeriveButton.TabIndex = 56;
            this.DeriveButton.Text = "派生";
            this.DeriveButton.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // ResetButton
            // 
            this.ResetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ResetButton.BackColor = System.Drawing.Color.Transparent;
            this.ResetButton.BorderColor = System.Drawing.Color.Transparent;
            this.ResetButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ResetButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ResetButton.ForeColor = System.Drawing.Color.White;
            this.ResetButton.Image = null;
            this.ResetButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ResetButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.ResetButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.ResetButton.Location = new System.Drawing.Point(1040, 20);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.ResetButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.ResetButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.ResetButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.ResetButton.Size = new System.Drawing.Size(50, 32);
            this.ResetButton.TabIndex = 55;
            this.ResetButton.Text = "重置";
            this.ResetButton.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // SelectChannelComBox
            // 
            this.SelectChannelComBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SelectChannelComBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.SelectChannelComBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectChannelComBox.EnabledCalc = true;
            this.SelectChannelComBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.SelectChannelComBox.FormattingEnabled = true;
            this.SelectChannelComBox.ItemHeight = 20;
            this.SelectChannelComBox.Location = new System.Drawing.Point(20, 20);
            this.SelectChannelComBox.Name = "SelectChannelComBox";
            this.SelectChannelComBox.Size = new System.Drawing.Size(200, 26);
            this.SelectChannelComBox.TabIndex = 0;
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
            this.panel2.Size = new System.Drawing.Size(1220, 773);
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
            // 
            // strName
            // 
            this.strName.DataPropertyName = "strName";
            this.strName.FillWeight = 10F;
            this.strName.HeaderText = "通道名称";
            this.strName.MinimumWidth = 120;
            this.strName.Name = "strName";
            this.strName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // MaxValue
            // 
            this.MaxValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MaxValue.DataPropertyName = "MaxValue";
            this.MaxValue.FillWeight = 5F;
            this.MaxValue.HeaderText = "最大值";
            this.MaxValue.MinimumWidth = 100;
            this.MaxValue.Name = "MaxValue";
            this.MaxValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // MinValue
            // 
            this.MinValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MinValue.DataPropertyName = "MinValue";
            this.MinValue.FillWeight = 5F;
            this.MinValue.HeaderText = "最小值";
            this.MinValue.MinimumWidth = 100;
            this.MinValue.Name = "MinValue";
            this.MinValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Index
            // 
            this.Index.DataPropertyName = "Index";
            this.Index.HeaderText = "通道序号";
            this.Index.MinimumWidth = 55;
            this.Index.Name = "Index";
            this.Index.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Index.Visible = false;
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
            this.Sensitivity.MinimumWidth = 128;
            this.Sensitivity.Name = "Sensitivity";
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
            this.ColorSelect.MinimumWidth = 128;
            this.ColorSelect.Name = "ColorSelect";
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
            // 
            // AntipolePic
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = "null";
            this.AntipolePic.DefaultCellStyle = dataGridViewCellStyle3;
            this.AntipolePic.FillWeight = 10F;
            this.AntipolePic.HeaderText = "镜像";
            this.AntipolePic.MinimumWidth = 80;
            this.AntipolePic.Name = "AntipolePic";
            // 
            // ChannelID
            // 
            this.ChannelID.DataPropertyName = "ChannelID";
            this.ChannelID.HeaderText = "唯一标识";
            this.ChannelID.Name = "ChannelID";
            this.ChannelID.Visible = false;
            // 
            // DBaseLineVisible
            // 
            this.DBaseLineVisible.DataPropertyName = "DBaseLineVisible";
            this.DBaseLineVisible.HeaderText = "75基线";
            this.DBaseLineVisible.Name = "DBaseLineVisible";
            this.DBaseLineVisible.Visible = false;
            // 
            // IsClone
            // 
            this.IsClone.DataPropertyName = "IsClone";
            this.IsClone.HeaderText = "克隆";
            this.IsClone.Name = "IsClone";
            this.IsClone.Visible = false;
            // 
            // Montage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CanResize = false;
            this.ClientSize = new System.Drawing.Size(1226, 873);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 10F);
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
        private System.Windows.Forms.ToolStripMenuItem AllCheckToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NoCheckToolStripMenuItem;
        private pSystem.UI.ReaLTaiizor.Controls.Panel panel1;
        private pSystem.UI.ReaLTaiizor.Controls.AloneComboBox SelectChannelComBox;
        private pSystem.UI.ReaLTaiizor.Controls.Panel panel2;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft ResetButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft SaveButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft DeriveButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumnEx State;
        private System.Windows.Forms.DataGridViewTextBoxColumn strName;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewComboBoxColumn Sensitivity;
        private System.Windows.Forms.DataGridViewComboBoxColumn SingleNotch;
        private System.Windows.Forms.DataGridViewComboBoxColumn HighPass;
        private System.Windows.Forms.DataGridViewComboBoxColumn LowPass;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PixelEnable;
        private pSystem.UI.ReaLTaiizor.CustomCtrl.Picker.ColorSelectPickerColumn ColorSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reserve;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsShowValue;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CalibrationsVisible;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Antipole;
        private System.Windows.Forms.DataGridViewImageColumn AntipolePic;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChannelID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DBaseLineVisible;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsClone;
    }
}