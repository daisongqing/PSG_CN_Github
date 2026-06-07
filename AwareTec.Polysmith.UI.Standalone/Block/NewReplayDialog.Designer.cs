namespace AwareTec.Polysmith.UI.Block
{
    partial class NewReplayDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.RaterComboBox = new pSystem.UI.ReaLTaiizor.Controls.AloneComboBox();
            this.DataSourceTextBox = new pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox();
            this.BrowseButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.AutoSearchButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.CancelButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.ConfirmButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(34, 74);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 23);
            this.label1.TabIndex = 38;
            this.label1.Text = "选择评分人";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(34, 151);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 23);
            this.label2.TabIndex = 39;
            this.label2.Text = "选择数据源";
            // 
            // RaterComboBox
            // 
            this.RaterComboBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RaterComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.RaterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RaterComboBox.EnabledCalc = true;
            this.RaterComboBox.FormattingEnabled = true;
            this.RaterComboBox.ItemHeight = 20;
            this.RaterComboBox.Location = new System.Drawing.Point(144, 74);
            this.RaterComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RaterComboBox.Name = "RaterComboBox";
            this.RaterComboBox.Size = new System.Drawing.Size(211, 26);
            this.RaterComboBox.TabIndex = 51;
            // 
            // DataSourceTextBox
            // 
            this.DataSourceTextBox.BackColor = System.Drawing.Color.Transparent;
            this.DataSourceTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.DataSourceTextBox.EdgeColor = System.Drawing.Color.Empty;
            this.DataSourceTextBox.Font = new System.Drawing.Font("Tahoma", 11F);
            this.DataSourceTextBox.ForeColor = System.Drawing.Color.DimGray;
            this.DataSourceTextBox.Location = new System.Drawing.Point(144, 148);
            this.DataSourceTextBox.MaxLength = 32767;
            this.DataSourceTextBox.Multiline = false;
            this.DataSourceTextBox.Name = "DataSourceTextBox";
            this.DataSourceTextBox.ReadOnly = false;
            this.DataSourceTextBox.Size = new System.Drawing.Size(370, 33);
            this.DataSourceTextBox.TabIndex = 60;
            this.DataSourceTextBox.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.DataSourceTextBox.UseSystemPasswordChar = false;
            // 
            // BrowseButton
            // 
            this.BrowseButton.BackColor = System.Drawing.Color.Transparent;
            this.BrowseButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.BrowseButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BrowseButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.BrowseButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.BrowseButton.Image = null;
            this.BrowseButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BrowseButton.InactiveColorA = System.Drawing.Color.White;
            this.BrowseButton.InactiveColorB = System.Drawing.Color.White;
            this.BrowseButton.Location = new System.Drawing.Point(542, 148);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.BrowseButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.BrowseButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.BrowseButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.BrowseButton.Size = new System.Drawing.Size(69, 33);
            this.BrowseButton.TabIndex = 61;
            this.BrowseButton.Text = "浏览";
            this.BrowseButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // AutoSearchButton
            // 
            this.AutoSearchButton.BackColor = System.Drawing.Color.Transparent;
            this.AutoSearchButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.AutoSearchButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AutoSearchButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.AutoSearchButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.AutoSearchButton.Image = null;
            this.AutoSearchButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AutoSearchButton.InactiveColorA = System.Drawing.Color.White;
            this.AutoSearchButton.InactiveColorB = System.Drawing.Color.White;
            this.AutoSearchButton.Location = new System.Drawing.Point(22, 250);
            this.AutoSearchButton.Name = "AutoSearchButton";
            this.AutoSearchButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.AutoSearchButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.AutoSearchButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.AutoSearchButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.AutoSearchButton.Size = new System.Drawing.Size(81, 38);
            this.AutoSearchButton.TabIndex = 62;
            this.AutoSearchButton.Text = "自动搜索";
            this.AutoSearchButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.AutoSearchButton.Click += new System.EventHandler(this.AutoSearchButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.BackColor = System.Drawing.Color.Transparent;
            this.CancelButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.CancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CancelButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.CancelButton.Image = null;
            this.CancelButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CancelButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.CancelButton.InactiveColorB = System.Drawing.Color.White;
            this.CancelButton.Location = new System.Drawing.Point(431, 250);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.PressedColorA = System.Drawing.Color.White;
            this.CancelButton.PressedColorB = System.Drawing.Color.White;
            this.CancelButton.PressedContourColorA = System.Drawing.Color.White;
            this.CancelButton.PressedContourColorB = System.Drawing.Color.White;
            this.CancelButton.Size = new System.Drawing.Size(83, 38);
            this.CancelButton.TabIndex = 63;
            this.CancelButton.Text = "取 消";
            this.CancelButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ConfirmButton
            // 
            this.ConfirmButton.BackColor = System.Drawing.Color.Transparent;
            this.ConfirmButton.BorderColor = System.Drawing.Color.Transparent;
            this.ConfirmButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ConfirmButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ConfirmButton.ForeColor = System.Drawing.Color.White;
            this.ConfirmButton.Image = null;
            this.ConfirmButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ConfirmButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.ConfirmButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.ConfirmButton.Location = new System.Drawing.Point(540, 250);
            this.ConfirmButton.Name = "ConfirmButton";
            this.ConfirmButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.ConfirmButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.ConfirmButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.ConfirmButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.ConfirmButton.Size = new System.Drawing.Size(83, 38);
            this.ConfirmButton.TabIndex = 64;
            this.ConfirmButton.Text = "确 定";
            this.ConfirmButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.ConfirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // NewReplayDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(629, 306);
            this.ControlBox = false;
            this.Controls.Add(this.ConfirmButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.AutoSearchButton);
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.DataSourceTextBox);
            this.Controls.Add(this.RaterComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.ControlBox = true;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "NewReplayDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "新建诊断";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private pSystem.UI.ReaLTaiizor.Controls.AloneComboBox RaterComboBox;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox DataSourceTextBox;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft BrowseButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft AutoSearchButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft CancelButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft ConfirmButton;
    }
}