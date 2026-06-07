namespace AwareTec.Polysmith.UI.Block
{
    partial class ImportHistroyDialog
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
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.AutoSearchRadioButton = new pSystem.UI.ReaLTaiizor.Controls.FoxRadioButton();
            this.HandSearchRadioButton = new pSystem.UI.ReaLTaiizor.Controls.FoxRadioButton();
            this.HandSearchTextBox = new pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox();
            this.BrowseButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.SearchButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.CancelButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.SuspendLayout();

            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(3, 35);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";

            this.shapeContainer1.Size = new System.Drawing.Size(525, 188);
            this.shapeContainer1.TabIndex = 31;
            this.shapeContainer1.TabStop = false;
            // 
            // AutoSearchRadioButton
            //
            this.AutoSearchRadioButton.BackColor = System.Drawing.Color.White;
            this.AutoSearchRadioButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.AutoSearchRadioButton.Checked = true;
            this.AutoSearchRadioButton.CheckedColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.AutoSearchRadioButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AutoSearchRadioButton.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.AutoSearchRadioButton.DisabledCheckedColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.AutoSearchRadioButton.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(178)))), ((int)(((byte)(190)))));
            this.AutoSearchRadioButton.EnabledCalc = true;
            this.AutoSearchRadioButton.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.AutoSearchRadioButton.ForeColor = System.Drawing.Color.Black;
            this.AutoSearchRadioButton.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(156)))), ((int)(((byte)(218)))));
            this.AutoSearchRadioButton.Location = new System.Drawing.Point(21, 77);
            this.AutoSearchRadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.AutoSearchRadioButton.Name = "AutoSearchRadioButton";
            this.AutoSearchRadioButton.Size = new System.Drawing.Size(173, 33);
            this.AutoSearchRadioButton.TabIndex = 45;
            this.AutoSearchRadioButton.Text = "自动搜索匹配数据";
            // 
            // HandSearchRadioButton
            // 
            this.HandSearchRadioButton.BackColor = System.Drawing.Color.White;
            this.HandSearchRadioButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.HandSearchRadioButton.Checked = false;
            this.HandSearchRadioButton.CheckedColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.HandSearchRadioButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HandSearchRadioButton.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.HandSearchRadioButton.DisabledCheckedColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.HandSearchRadioButton.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(178)))), ((int)(((byte)(190)))));
            this.HandSearchRadioButton.EnabledCalc = true;
            this.HandSearchRadioButton.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.HandSearchRadioButton.ForeColor = System.Drawing.Color.Black;
            this.HandSearchRadioButton.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(156)))), ((int)(((byte)(218)))));
            this.HandSearchRadioButton.Location = new System.Drawing.Point(21, 128);
            this.HandSearchRadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.HandSearchRadioButton.Name = "HandSearchRadioButton";
            this.HandSearchRadioButton.Size = new System.Drawing.Size(164, 33);
            this.HandSearchRadioButton.TabIndex = 46;
            this.HandSearchRadioButton.Text = "手动查找EDF数据文件";
            // 
            // HandSearchTextBox
            // 
            this.HandSearchTextBox.BackColor = System.Drawing.Color.Transparent;
            this.HandSearchTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.HandSearchTextBox.EdgeColor = System.Drawing.Color.Empty;
            this.HandSearchTextBox.Font = new System.Drawing.Font("Tahoma", 11F);
            this.HandSearchTextBox.ForeColor = System.Drawing.Color.DimGray;
            this.HandSearchTextBox.Location = new System.Drawing.Point(176, 121);
            this.HandSearchTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.HandSearchTextBox.MaxLength = 32767;
            this.HandSearchTextBox.Multiline = false;
            this.HandSearchTextBox.Name = "HandSearchTextBox";
            this.HandSearchTextBox.ReadOnly = false;
            this.HandSearchTextBox.Size = new System.Drawing.Size(278, 28);
            this.HandSearchTextBox.TabIndex = 60;
            this.HandSearchTextBox.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.HandSearchTextBox.UseSystemPasswordChar = false;
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
            this.BrowseButton.Location = new System.Drawing.Point(458, 123);
            this.BrowseButton.Margin = new System.Windows.Forms.Padding(2);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.BrowseButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.BrowseButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.BrowseButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.BrowseButton.Size = new System.Drawing.Size(52, 26);
            this.BrowseButton.TabIndex = 61;
            this.BrowseButton.Text = "浏览";
            this.BrowseButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // SearchButton
            // 
            this.SearchButton.BackColor = System.Drawing.Color.Transparent;
            this.SearchButton.BorderColor = System.Drawing.Color.Transparent;
            this.SearchButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SearchButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.SearchButton.ForeColor = System.Drawing.Color.White;
            this.SearchButton.Image = null;
            this.SearchButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SearchButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.SearchButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.SearchButton.Location = new System.Drawing.Point(448, 175);
            this.SearchButton.Margin = new System.Windows.Forms.Padding(2);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.SearchButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.SearchButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.SearchButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.SearchButton.Size = new System.Drawing.Size(62, 30);
            this.SearchButton.TabIndex = 62;
            this.SearchButton.Text = "提 交";
            this.SearchButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
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
            this.CancelButton.Location = new System.Drawing.Point(364, 175);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.PressedColorA = System.Drawing.Color.White;
            this.CancelButton.PressedColorB = System.Drawing.Color.White;
            this.CancelButton.PressedContourColorA = System.Drawing.Color.White;
            this.CancelButton.PressedContourColorB = System.Drawing.Color.White;
            this.CancelButton.Size = new System.Drawing.Size(62, 30);
            this.CancelButton.TabIndex = 63;
            this.CancelButton.Text = "取 消";
            this.CancelButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ImportHistroyDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CanResize = false;
            this.ClientSize = new System.Drawing.Size(531, 226);
            this.ControlBox = false;
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.HandSearchTextBox);
            this.Controls.Add(this.HandSearchRadioButton);
            this.Controls.Add(this.AutoSearchRadioButton);
            this.Controls.Add(this.shapeContainer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportHistroyDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择待分析数据源";
            this.ResumeLayout(false);

        }

        #endregion
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private pSystem.UI.ReaLTaiizor.Controls.FoxRadioButton AutoSearchRadioButton;
        private pSystem.UI.ReaLTaiizor.Controls.FoxRadioButton HandSearchRadioButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox HandSearchTextBox;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft BrowseButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft SearchButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft CancelButton;
    }
}