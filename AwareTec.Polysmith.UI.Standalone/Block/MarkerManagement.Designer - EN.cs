namespace AwareTec.Polysmith.UI.Block
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
            this.AddButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.DeleteButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.EditButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
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
            this.abSelect.BackgroundColor = System.Drawing.Color.WhiteSmoke;
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
            this.abSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.abSelect.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.abSelect.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.abSelect.HeaderColLinearBegin = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.abSelect.HeaderColLinearEnd = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.abSelect.HeaderRowLinearBegin = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(235)))), ((int)(((byte)(255)))));
            this.abSelect.HeaderRowLinearEnd = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(235)))), ((int)(((byte)(255)))));
            this.abSelect.Location = new System.Drawing.Point(0, 0);
            this.abSelect.Margin = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.abSelect.MultiSelect = false;
            this.abSelect.Name = "abSelect";
            this.abSelect.RowHeadersWidth = 62;
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
            this.abSelect.Size = new System.Drawing.Size(1914, 1187);
            this.abSelect.TabIndex = 36;
            this.abSelect.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.abSelect_CellDoubleClick);
            // 
            // strName
            // 
            this.strName.DataPropertyName = "Name";
            this.strName.FillWeight = 10F;
            this.strName.HeaderText = "Event Name";
            this.strName.MinimumWidth = 120;
            this.strName.Name = "strName";
            this.strName.ReadOnly = true;
            this.strName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.strName.Width = 120;
            // 
            // Description
            // 
            this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Description.DataPropertyName = "Description";
            this.Description.FillWeight = 30F;
            this.Description.HeaderText = "Description";
            this.Description.MinimumWidth = 450;
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColorSelect
            // 
            this.ColorSelect.DataPropertyName = "ColorSelect";
            this.ColorSelect.FillWeight = 30F;
            this.ColorSelect.HeaderText = "Color";
            this.ColorSelect.MinimumWidth = 200;
            this.ColorSelect.Name = "ColorSelect";
            this.ColorSelect.ReadOnly = true;
            this.ColorSelect.Width = 200;
            // 
            // Typ
            // 
            this.Typ.FillWeight = 10F;
            this.Typ.HeaderText = "Type";
            this.Typ.MinimumWidth = 100;
            this.Typ.Name = "Typ";
            this.Typ.ReadOnly = true;
            this.Typ.Width = 150;
            // 
            // HotKey
            // 
            this.HotKey.FillWeight = 10F;
            this.HotKey.HeaderText = "HotKey";
            this.HotKey.MinimumWidth = 100;
            this.HotKey.Name = "HotKey";
            this.HotKey.ReadOnly = true;
            this.HotKey.Width = 150;
            // 
            // MinLimit
            // 
            this.MinLimit.FillWeight = 5F;
            this.MinLimit.HeaderText = "Min Limit(s)";
            this.MinLimit.MinimumWidth = 100;
            this.MinLimit.Name = "MinLimit";
            this.MinLimit.ReadOnly = true;
            this.MinLimit.Width = 150;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.AddButton);
            this.panel2.Controls.Add(this.DeleteButton);
            this.panel2.Controls.Add(this.EditButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 35);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1914, 85);
            this.panel2.TabIndex = 38;
            // 
            // AddButton
            // 
            this.AddButton.BackColor = System.Drawing.Color.Transparent;
            this.AddButton.BorderColor = System.Drawing.Color.Transparent;
            this.AddButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.AddButton.ForeColor = System.Drawing.Color.White;
            this.AddButton.Image = null;
            this.AddButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AddButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.AddButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.AddButton.Location = new System.Drawing.Point(1521, 18);
            this.AddButton.Margin = new System.Windows.Forms.Padding(4);
            this.AddButton.Name = "AddButton";
            this.AddButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.AddButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.AddButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.AddButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.AddButton.Size = new System.Drawing.Size(75, 48);
            this.AddButton.TabIndex = 37;
            this.AddButton.Text = "Add";
            this.AddButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.BackColor = System.Drawing.Color.Transparent;
            this.DeleteButton.BorderColor = System.Drawing.Color.Transparent;
            this.DeleteButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DeleteButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.DeleteButton.ForeColor = System.Drawing.Color.White;
            this.DeleteButton.Image = null;
            this.DeleteButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DeleteButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.DeleteButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.DeleteButton.Location = new System.Drawing.Point(1621, 18);
            this.DeleteButton.Margin = new System.Windows.Forms.Padding(4);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.DeleteButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.DeleteButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.DeleteButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.DeleteButton.Size = new System.Drawing.Size(75, 48);
            this.DeleteButton.TabIndex = 38;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // EditButton
            // 
            this.EditButton.BackColor = System.Drawing.Color.Transparent;
            this.EditButton.BorderColor = System.Drawing.Color.Transparent;
            this.EditButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EditButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.EditButton.ForeColor = System.Drawing.Color.White;
            this.EditButton.Image = null;
            this.EditButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.EditButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.EditButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.EditButton.Location = new System.Drawing.Point(1721, 18);
            this.EditButton.Margin = new System.Windows.Forms.Padding(4);
            this.EditButton.Name = "EditButton";
            this.EditButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.EditButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.EditButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.EditButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.EditButton.Size = new System.Drawing.Size(75, 48);
            this.EditButton.TabIndex = 37;
            this.EditButton.Text = "Edit";
            this.EditButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.abSelect);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 120);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1914, 1187);
            this.panel1.TabIndex = 39;
            // 
            // MarkerManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CanResize = false;
            this.ClientSize = new System.Drawing.Size(1920, 1310);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MarkerManagement";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Event management";
            ((System.ComponentModel.ISupportInitialize)(this.abSelect)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridViewEx abSelect;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft EditButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft DeleteButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft AddButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn strName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private pSystem.UI.ReaLTaiizor.CustomCtrl.Picker.ColorSelectPickerColumn ColorSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn Typ;
        private System.Windows.Forms.DataGridViewTextBoxColumn HotKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinLimit;
    }
}