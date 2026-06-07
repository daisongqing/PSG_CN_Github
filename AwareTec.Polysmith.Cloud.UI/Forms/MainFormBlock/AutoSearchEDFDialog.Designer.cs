
namespace AwareTec.Polysmith.Cloud.UI.Forms.MainFormBlock
{
    partial class AutoSearchEDFDialog
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
            this.CancelButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.FindFromLocalPathTextBox = new pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox();
            this.SearchProgressBar = new System.Windows.Forms.ProgressBar();
            this.BrowseButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.SubmitButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.labelEx1 = new pSystem.UI.UserControls.LabelEx();
            this.autoSearchLink = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.SuspendLayout();
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.BackColor = System.Drawing.Color.Transparent;
            this.CancelButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.CancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CancelButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.CancelButton.Image = null;
            this.CancelButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CancelButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.CancelButton.InactiveColorB = System.Drawing.Color.White;
            this.CancelButton.Location = new System.Drawing.Point(340, 112);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.PressedColorA = System.Drawing.Color.White;
            this.CancelButton.PressedColorB = System.Drawing.Color.White;
            this.CancelButton.PressedContourColorA = System.Drawing.Color.White;
            this.CancelButton.PressedContourColorB = System.Drawing.Color.White;
            this.CancelButton.Size = new System.Drawing.Size(65, 32);
            this.CancelButton.TabIndex = 56;
            this.CancelButton.Text = "取 消";
            this.CancelButton.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // FindFromLocalPathTextBox
            // 
            this.FindFromLocalPathTextBox.BackColor = System.Drawing.Color.Transparent;
            this.FindFromLocalPathTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.FindFromLocalPathTextBox.EdgeColor = System.Drawing.Color.Empty;
            this.FindFromLocalPathTextBox.Font = new System.Drawing.Font("Tahoma", 11F);
            this.FindFromLocalPathTextBox.ForeColor = System.Drawing.Color.DimGray;
            this.FindFromLocalPathTextBox.Location = new System.Drawing.Point(113, 60);
            this.FindFromLocalPathTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.FindFromLocalPathTextBox.MaxLength = 32767;
            this.FindFromLocalPathTextBox.Multiline = false;
            this.FindFromLocalPathTextBox.Name = "FindFromLocalPathTextBox";
            this.FindFromLocalPathTextBox.ReadOnly = false;
            this.FindFromLocalPathTextBox.Size = new System.Drawing.Size(209, 28);
            this.FindFromLocalPathTextBox.TabIndex = 59;
            this.FindFromLocalPathTextBox.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.FindFromLocalPathTextBox.UseSystemPasswordChar = false;
            // 
            // SearchProgressBar
            // 
            this.SearchProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.SearchProgressBar.Location = new System.Drawing.Point(3, 161);
            this.SearchProgressBar.Name = "SearchProgressBar";
            this.SearchProgressBar.Size = new System.Drawing.Size(494, 10);
            this.SearchProgressBar.TabIndex = 60;
            this.SearchProgressBar.Visible = false;
            // 
            // BrowseButton
            // 
            this.BrowseButton.BackColor = System.Drawing.Color.Transparent;
            this.BrowseButton.BorderColor = System.Drawing.Color.Transparent;
            this.BrowseButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BrowseButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.BrowseButton.ForeColor = System.Drawing.Color.White;
            this.BrowseButton.Image = null;
            this.BrowseButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BrowseButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.BrowseButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.BrowseButton.Location = new System.Drawing.Point(332, 60);
            this.BrowseButton.Margin = new System.Windows.Forms.Padding(2);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.BrowseButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.BrowseButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.BrowseButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.BrowseButton.Size = new System.Drawing.Size(50, 32);
            this.BrowseButton.TabIndex = 62;
            this.BrowseButton.Text = "浏览";
            this.BrowseButton.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // SubmitButton
            // 
            this.SubmitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SubmitButton.BackColor = System.Drawing.Color.Transparent;
            this.SubmitButton.BorderColor = System.Drawing.Color.Transparent;
            this.SubmitButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SubmitButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SubmitButton.ForeColor = System.Drawing.Color.White;
            this.SubmitButton.Image = null;
            this.SubmitButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SubmitButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.SubmitButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.SubmitButton.Location = new System.Drawing.Point(415, 112);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.SubmitButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.SubmitButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.SubmitButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.SubmitButton.Size = new System.Drawing.Size(65, 32);
            this.SubmitButton.TabIndex = 65;
            this.SubmitButton.Text = "上 传";
            this.SubmitButton.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // labelEx1
            // 
            this.labelEx1.Font = new System.Drawing.Font("宋体", 10F);
            this.labelEx1.Location = new System.Drawing.Point(20, 67);
            this.labelEx1.Name = "labelEx1";
            this.labelEx1.Size = new System.Drawing.Size(86, 23);
            this.labelEx1.TabIndex = 67;
            this.labelEx1.Text = "EDF文件路径";
            // 
            // autoSearchLink
            // 
            this.autoSearchLink.BackColor = System.Drawing.Color.Transparent;
            this.autoSearchLink.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.autoSearchLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this.autoSearchLink.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.autoSearchLink.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.autoSearchLink.Image = null;
            this.autoSearchLink.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.autoSearchLink.InactiveColorA = System.Drawing.Color.White;
            this.autoSearchLink.InactiveColorB = System.Drawing.Color.White;
            this.autoSearchLink.Location = new System.Drawing.Point(392, 60);
            this.autoSearchLink.Margin = new System.Windows.Forms.Padding(2);
            this.autoSearchLink.Name = "autoSearchLink";
            this.autoSearchLink.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.autoSearchLink.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.autoSearchLink.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.autoSearchLink.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.autoSearchLink.Size = new System.Drawing.Size(88, 32);
            this.autoSearchLink.TabIndex = 68;
            this.autoSearchLink.Text = "自动搜索";
            this.autoSearchLink.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // AutoSearchEDFDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CanResize = false;
            this.ClientSize = new System.Drawing.Size(500, 174);
            this.Controls.Add(this.autoSearchLink);
            this.Controls.Add(this.labelEx1);
            this.Controls.Add(this.SubmitButton);
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.SearchProgressBar);
            this.Controls.Add(this.FindFromLocalPathTextBox);
            this.Controls.Add(this.CancelButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AutoSearchEDFDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "查找数据文件";
            this.ResumeLayout(false);

        }


        #endregion
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft CancelButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox FindFromLocalPathTextBox;
        private System.Windows.Forms.ProgressBar SearchProgressBar;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft BrowseButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft SubmitButton;
        private pSystem.UI.UserControls.LabelEx labelEx1;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft autoSearchLink;
    }
}