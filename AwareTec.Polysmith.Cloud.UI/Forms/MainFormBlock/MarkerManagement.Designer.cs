
namespace AwareTec.Polysmith.Cloud.UI.Forms.MainFormBlock
{
    partial class MarkerManagement
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.abSelect = new System.Windows.Forms.DataGridViewEx();
            this.strName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColorSelect = new pSystem.UI.ReaLTaiizor.CustomCtrl.Picker.ColorSelectPickerColumn();
            this.Typ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HotKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinLimit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.EditButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.DeleteButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.AddButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.abSelect)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.abSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.abSelect.BackgroundColor = System.Drawing.Color.White;
            this.abSelect.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.abSelect.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.abSelect.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.abSelect.ColumnHeadersHeight = 50;
            this.abSelect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.abSelect.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.strName,
            this.Description,
            this.ColorSelect,
            this.Typ,
            this.HotKey,
            this.MinLimit});
            this.abSelect.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.abSelect.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(250)))), ((int)(((byte)(245)))));
            this.abSelect.HeaderColLinearBegin = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(250)))), ((int)(((byte)(245)))));
            this.abSelect.HeaderColLinearEnd = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(250)))), ((int)(((byte)(245)))));
            this.abSelect.HeaderRowLinearBegin = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(250)))), ((int)(((byte)(245)))));
            this.abSelect.HeaderRowLinearEnd = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(250)))), ((int)(((byte)(245)))));
            this.abSelect.Location = new System.Drawing.Point(0, 0);
            this.abSelect.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.abSelect.MultiSelect = false;
            this.abSelect.Name = "abSelect";
            this.abSelect.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.abSelect.RowIndexOffSet = 0;
            this.abSelect.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.abSelect.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.abSelect.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.abSelect.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.abSelect.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(246)))), ((int)(((byte)(235)))));
            this.abSelect.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.abSelect.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.abSelect.RowTemplate.Height = 40;
            this.abSelect.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.abSelect.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.abSelect.Size = new System.Drawing.Size(1220, 773);
            this.abSelect.TabIndex = 36;
            // 
            // strName
            // 
            this.strName.DataPropertyName = "Name";
            this.strName.FillWeight = 12F;
            this.strName.HeaderText = "事件名称";
            this.strName.MinimumWidth = 130;
            this.strName.Name = "strName";
            this.strName.ReadOnly = true;
            this.strName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.strName.Width = 130;
            // 
            // Description
            // 
            this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Description.DataPropertyName = "Description";
            this.Description.FillWeight = 30F;
            this.Description.HeaderText = "事件描述";
            this.Description.MinimumWidth = 450;
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColorSelect
            // 
            this.ColorSelect.DataPropertyName = "ColorSelect";
            this.ColorSelect.FillWeight = 30F;
            this.ColorSelect.HeaderText = "颜色";
            this.ColorSelect.MinimumWidth = 200;
            this.ColorSelect.Name = "ColorSelect";
            this.ColorSelect.ReadOnly = true;
            this.ColorSelect.Width = 200;
            // 
            // Typ
            // 
            this.Typ.FillWeight = 10F;
            this.Typ.HeaderText = "分类";
            this.Typ.MinimumWidth = 100;
            this.Typ.Name = "Typ";
            this.Typ.ReadOnly = true;
            // 
            // HotKey
            // 
            this.HotKey.FillWeight = 10F;
            this.HotKey.HeaderText = "热键";
            this.HotKey.MinimumWidth = 100;
            this.HotKey.Name = "HotKey";
            this.HotKey.ReadOnly = true;
            // 
            // MinLimit
            // 
            this.MinLimit.FillWeight = 10F;
            this.MinLimit.HeaderText = "最小时域(s)";
            this.MinLimit.MinimumWidth = 120;
            this.MinLimit.Name = "MinLimit";
            this.MinLimit.ReadOnly = true;
            this.MinLimit.Width = 120;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.EditButton);
            this.panel2.Controls.Add(this.DeleteButton);
            this.panel2.Controls.Add(this.AddButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1220, 62);
            this.panel2.TabIndex = 38;
            // 
            // EditButton
            // 
            this.EditButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.EditButton.BackColor = System.Drawing.Color.Transparent;
            this.EditButton.BorderColor = System.Drawing.Color.Transparent;
            this.EditButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EditButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.EditButton.ForeColor = System.Drawing.Color.White;
            this.EditButton.Image = null;
            this.EditButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.EditButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.EditButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.EditButton.Location = new System.Drawing.Point(1149, 20);
            this.EditButton.Name = "EditButton";
            this.EditButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.EditButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.EditButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.EditButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.EditButton.Size = new System.Drawing.Size(50, 32);
            this.EditButton.TabIndex = 57;
            this.EditButton.Text = "编辑";
            this.EditButton.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // DeleteButton
            // 
            this.DeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteButton.BackColor = System.Drawing.Color.Transparent;
            this.DeleteButton.BorderColor = System.Drawing.Color.Transparent;
            this.DeleteButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DeleteButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DeleteButton.ForeColor = System.Drawing.Color.White;
            this.DeleteButton.Image = null;
            this.DeleteButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DeleteButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.DeleteButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.DeleteButton.Location = new System.Drawing.Point(1088, 20);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.DeleteButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.DeleteButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.DeleteButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.DeleteButton.Size = new System.Drawing.Size(50, 32);
            this.DeleteButton.TabIndex = 56;
            this.DeleteButton.Text = "删除";
            this.DeleteButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.DeleteButton.Visible = false;
            // 
            // AddButton
            // 
            this.AddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AddButton.BackColor = System.Drawing.Color.Transparent;
            this.AddButton.BorderColor = System.Drawing.Color.Transparent;
            this.AddButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AddButton.ForeColor = System.Drawing.Color.White;
            this.AddButton.Image = null;
            this.AddButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AddButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.AddButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.AddButton.Location = new System.Drawing.Point(1027, 20);
            this.AddButton.Name = "AddButton";
            this.AddButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.AddButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.AddButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.AddButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.AddButton.Size = new System.Drawing.Size(50, 32);
            this.AddButton.TabIndex = 55;
            this.AddButton.Text = "新增";
            this.AddButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.AddButton.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.abSelect);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 97);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1220, 773);
            this.panel1.TabIndex = 39;
            // 
            // MarkerManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CanResize = false;
            this.ClientSize = new System.Drawing.Size(1226, 873);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MarkerManagement";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "事件管理";
            ((System.ComponentModel.ISupportInitialize)(this.abSelect)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridViewEx abSelect;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft AddButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft DeleteButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft EditButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn strName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private pSystem.UI.ReaLTaiizor.CustomCtrl.Picker.ColorSelectPickerColumn ColorSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn Typ;
        private System.Windows.Forms.DataGridViewTextBoxColumn HotKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinLimit;
    }
}