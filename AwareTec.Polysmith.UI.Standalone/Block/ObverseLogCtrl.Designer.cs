
namespace AwareTec.Polysmith.UI.Block
{
    partial class ObverseLogCtrl
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
            this.LogWebBrowser = new System.Windows.Forms.WebBrowser();
            this.LogBasicLogStatusBar = new pSystem.UI.ReaLTaiizor.Controls.CrownStatusStrip();
            this.CurrentPageNoLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.LogPerRunInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.CrossDaysLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.FatalCheckBox = new pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox();
            this.ErrorCheckBox = new pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox();
            this.WarnCheckBox = new pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox();
            this.InfoCheckBox = new pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox();
            this.DebugCheckBox = new pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox();
            this.PrePageButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.NextPageButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.RadioButtonPanel = new System.Windows.Forms.Panel();
            this.LogBasicLogStatusBar.SuspendLayout();
            this.RadioButtonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LogWebBrowser
            // 
            this.LogWebBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogWebBrowser.Location = new System.Drawing.Point(0, 42);
            this.LogWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.LogWebBrowser.Name = "LogWebBrowser";
            this.LogWebBrowser.Size = new System.Drawing.Size(1026, 511);
            this.LogWebBrowser.TabIndex = 0;
            // 
            // LogBasicLogStatusBar
            // 
            this.LogBasicLogStatusBar.AutoSize = false;
            this.LogBasicLogStatusBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(235)))), ((int)(((byte)(255)))));
            this.LogBasicLogStatusBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.LogBasicLogStatusBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.LogBasicLogStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CurrentPageNoLabel,
            this.LogPerRunInfo,
            this.CrossDaysLabel});
            this.LogBasicLogStatusBar.Location = new System.Drawing.Point(0, 556);
            this.LogBasicLogStatusBar.Name = "LogBasicLogStatusBar";
            this.LogBasicLogStatusBar.Padding = new System.Windows.Forms.Padding(0, 5, 0, 3);
            this.LogBasicLogStatusBar.Size = new System.Drawing.Size(1026, 38);
            this.LogBasicLogStatusBar.SizingGrip = false;
            this.LogBasicLogStatusBar.TabIndex = 1;
            this.LogBasicLogStatusBar.Text = "crownStatusStrip1";
            // 
            // CurrentPageNoLabel
            // 
            this.CurrentPageNoLabel.BackColor = System.Drawing.Color.Transparent;
            this.CurrentPageNoLabel.ForeColor = System.Drawing.Color.DimGray;
            this.CurrentPageNoLabel.Margin = new System.Windows.Forms.Padding(40, 4, 0, 2);
            this.CurrentPageNoLabel.Name = "CurrentPageNoLabel";
            this.CurrentPageNoLabel.Size = new System.Drawing.Size(54, 24);
            this.CurrentPageNoLabel.Text = "当前：";
            // 
            // LogPerRunInfo
            // 
            this.LogPerRunInfo.ForeColor = System.Drawing.Color.DimGray;
            this.LogPerRunInfo.Margin = new System.Windows.Forms.Padding(165, 4, 0, 2);
            this.LogPerRunInfo.Name = "LogPerRunInfo";
            this.LogPerRunInfo.Size = new System.Drawing.Size(73, 24);
            this.LogPerRunInfo.Text = "查看日志:";
            // 
            // CrossDaysLabel
            // 
            this.CrossDaysLabel.ForeColor = System.Drawing.Color.DimGray;
            this.CrossDaysLabel.Margin = new System.Windows.Forms.Padding(5, 4, 0, 2);
            this.CrossDaysLabel.Name = "CrossDaysLabel";
            this.CrossDaysLabel.Size = new System.Drawing.Size(43, 24);
            this.CrossDaysLabel.Text = "运行:";
            // 
            // FatalCheckBox
            // 
            this.FatalCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.FatalCheckBox.BaseColor = System.Drawing.Color.Transparent;
            this.FatalCheckBox.BorderColor = System.Drawing.Color.Transparent;
            this.FatalCheckBox.CheckBackColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.FatalCheckBox.CheckBackColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.FatalCheckBox.CheckBorderColorA = System.Drawing.Color.Transparent;
            this.FatalCheckBox.CheckBorderColorB = System.Drawing.Color.Transparent;
            this.FatalCheckBox.Checked = true;
            this.FatalCheckBox.CheckedColor = System.Drawing.Color.White;
            this.FatalCheckBox.CompositingQualityType = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            this.FatalCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FatalCheckBox.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.FatalCheckBox.ForeColor = System.Drawing.Color.Black;
            this.FatalCheckBox.Location = new System.Drawing.Point(3, 25);
            this.FatalCheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FatalCheckBox.Name = "FatalCheckBox";
            this.FatalCheckBox.Size = new System.Drawing.Size(154, 25);
            this.FatalCheckBox.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.FatalCheckBox.TabIndex = 73;
            this.FatalCheckBox.Text = "严重错误(fatal)";
            this.FatalCheckBox.TextRenderingType = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            // 
            // ErrorCheckBox
            // 
            this.ErrorCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.ErrorCheckBox.BaseColor = System.Drawing.Color.Transparent;
            this.ErrorCheckBox.BorderColor = System.Drawing.Color.Transparent;
            this.ErrorCheckBox.CheckBackColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.ErrorCheckBox.CheckBackColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.ErrorCheckBox.CheckBorderColorA = System.Drawing.Color.Transparent;
            this.ErrorCheckBox.CheckBorderColorB = System.Drawing.Color.Transparent;
            this.ErrorCheckBox.Checked = true;
            this.ErrorCheckBox.CheckedColor = System.Drawing.Color.White;
            this.ErrorCheckBox.CompositingQualityType = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            this.ErrorCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ErrorCheckBox.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ErrorCheckBox.ForeColor = System.Drawing.Color.Black;
            this.ErrorCheckBox.Location = new System.Drawing.Point(219, 25);
            this.ErrorCheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ErrorCheckBox.Name = "ErrorCheckBox";
            this.ErrorCheckBox.Size = new System.Drawing.Size(154, 25);
            this.ErrorCheckBox.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.ErrorCheckBox.TabIndex = 74;
            this.ErrorCheckBox.Text = "错误(error)";
            this.ErrorCheckBox.TextRenderingType = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            // 
            // WarnCheckBox
            // 
            this.WarnCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.WarnCheckBox.BaseColor = System.Drawing.Color.Transparent;
            this.WarnCheckBox.BorderColor = System.Drawing.Color.Transparent;
            this.WarnCheckBox.CheckBackColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.WarnCheckBox.CheckBackColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.WarnCheckBox.CheckBorderColorA = System.Drawing.Color.Transparent;
            this.WarnCheckBox.CheckBorderColorB = System.Drawing.Color.Transparent;
            this.WarnCheckBox.Checked = true;
            this.WarnCheckBox.CheckedColor = System.Drawing.Color.White;
            this.WarnCheckBox.CompositingQualityType = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            this.WarnCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.WarnCheckBox.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.WarnCheckBox.ForeColor = System.Drawing.Color.Black;
            this.WarnCheckBox.Location = new System.Drawing.Point(419, 25);
            this.WarnCheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.WarnCheckBox.Name = "WarnCheckBox";
            this.WarnCheckBox.Size = new System.Drawing.Size(154, 25);
            this.WarnCheckBox.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.WarnCheckBox.TabIndex = 75;
            this.WarnCheckBox.Text = "警告(warn)";
            this.WarnCheckBox.TextRenderingType = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            // 
            // InfoCheckBox
            // 
            this.InfoCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.InfoCheckBox.BaseColor = System.Drawing.Color.Transparent;
            this.InfoCheckBox.BorderColor = System.Drawing.Color.Transparent;
            this.InfoCheckBox.CheckBackColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.InfoCheckBox.CheckBackColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.InfoCheckBox.CheckBorderColorA = System.Drawing.Color.Transparent;
            this.InfoCheckBox.CheckBorderColorB = System.Drawing.Color.Transparent;
            this.InfoCheckBox.Checked = true;
            this.InfoCheckBox.CheckedColor = System.Drawing.Color.White;
            this.InfoCheckBox.CompositingQualityType = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            this.InfoCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.InfoCheckBox.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.InfoCheckBox.ForeColor = System.Drawing.Color.Black;
            this.InfoCheckBox.Location = new System.Drawing.Point(619, 25);
            this.InfoCheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.InfoCheckBox.Name = "InfoCheckBox";
            this.InfoCheckBox.Size = new System.Drawing.Size(154, 25);
            this.InfoCheckBox.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.InfoCheckBox.TabIndex = 76;
            this.InfoCheckBox.Text = "信息(info)";
            this.InfoCheckBox.TextRenderingType = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            // 
            // DebugCheckBox
            // 
            this.DebugCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.DebugCheckBox.BaseColor = System.Drawing.Color.Transparent;
            this.DebugCheckBox.BorderColor = System.Drawing.Color.Transparent;
            this.DebugCheckBox.CheckBackColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.DebugCheckBox.CheckBackColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.DebugCheckBox.CheckBorderColorA = System.Drawing.Color.Transparent;
            this.DebugCheckBox.CheckBorderColorB = System.Drawing.Color.Transparent;
            this.DebugCheckBox.Checked = true;
            this.DebugCheckBox.CheckedColor = System.Drawing.Color.White;
            this.DebugCheckBox.CompositingQualityType = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            this.DebugCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DebugCheckBox.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.DebugCheckBox.ForeColor = System.Drawing.Color.Black;
            this.DebugCheckBox.Location = new System.Drawing.Point(802, 25);
            this.DebugCheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DebugCheckBox.Name = "DebugCheckBox";
            this.DebugCheckBox.Size = new System.Drawing.Size(154, 25);
            this.DebugCheckBox.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.DebugCheckBox.TabIndex = 77;
            this.DebugCheckBox.Text = "调试(debug)";
            this.DebugCheckBox.TextRenderingType = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            // 
            // PrePageButton
            // 
            this.PrePageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PrePageButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(235)))), ((int)(((byte)(255)))));
            this.PrePageButton.BorderColor = System.Drawing.Color.Transparent;
            this.PrePageButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PrePageButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.PrePageButton.ForeColor = System.Drawing.Color.White;
            this.PrePageButton.Image = null;
            this.PrePageButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PrePageButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.PrePageButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.PrePageButton.Location = new System.Drawing.Point(3, 568);
            this.PrePageButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PrePageButton.Name = "PrePageButton";
            this.PrePageButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.PrePageButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.PrePageButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.PrePageButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.PrePageButton.Size = new System.Drawing.Size(30, 17);
            this.PrePageButton.TabIndex = 78;
            this.PrePageButton.Text = "<";
            this.PrePageButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.PrePageButton.Click += new System.EventHandler(this.PrePageButton_Click);
            // 
            // NextPageButton
            // 
            this.NextPageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NextPageButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(235)))), ((int)(((byte)(255)))));
            this.NextPageButton.BorderColor = System.Drawing.Color.Transparent;
            this.NextPageButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.NextPageButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.NextPageButton.ForeColor = System.Drawing.Color.White;
            this.NextPageButton.Image = null;
            this.NextPageButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.NextPageButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.NextPageButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.NextPageButton.Location = new System.Drawing.Point(215, 568);
            this.NextPageButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NextPageButton.Name = "NextPageButton";
            this.NextPageButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.NextPageButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.NextPageButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.NextPageButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.NextPageButton.Size = new System.Drawing.Size(30, 17);
            this.NextPageButton.TabIndex = 79;
            this.NextPageButton.Text = ">";
            this.NextPageButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.NextPageButton.Click += new System.EventHandler(this.NextPageButton_Click);
            // 
            // RadioButtonPanel
            // 
            this.RadioButtonPanel.Controls.Add(this.FatalCheckBox);
            this.RadioButtonPanel.Controls.Add(this.ErrorCheckBox);
            this.RadioButtonPanel.Controls.Add(this.WarnCheckBox);
            this.RadioButtonPanel.Controls.Add(this.InfoCheckBox);
            this.RadioButtonPanel.Controls.Add(this.DebugCheckBox);
            this.RadioButtonPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.RadioButtonPanel.Location = new System.Drawing.Point(0, 0);
            this.RadioButtonPanel.Name = "RadioButtonPanel";
            this.RadioButtonPanel.Size = new System.Drawing.Size(1026, 87);
            this.RadioButtonPanel.TabIndex = 80;
            // 
            // ObverseLogCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.RadioButtonPanel);
            this.Controls.Add(this.NextPageButton);
            this.Controls.Add(this.PrePageButton);
            this.Controls.Add(this.LogBasicLogStatusBar);
            this.Controls.Add(this.LogWebBrowser);
            this.Name = "ObverseLogCtrl";
            this.Size = new System.Drawing.Size(1026, 594);
            this.LogBasicLogStatusBar.ResumeLayout(false);
            this.LogBasicLogStatusBar.PerformLayout();
            this.RadioButtonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser LogWebBrowser;
        private pSystem.UI.ReaLTaiizor.Controls.CrownStatusStrip LogBasicLogStatusBar;
        private System.Windows.Forms.ToolStripStatusLabel CurrentPageNoLabel;
        private System.Windows.Forms.ToolStripStatusLabel LogPerRunInfo;
        private System.Windows.Forms.ToolStripStatusLabel CrossDaysLabel;
        private pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox FatalCheckBox;
        private pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox ErrorCheckBox;
        private pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox WarnCheckBox;
        private pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox InfoCheckBox;
        private pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox DebugCheckBox;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft PrePageButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft NextPageButton;
        private System.Windows.Forms.Panel RadioButtonPanel;
    }
}
