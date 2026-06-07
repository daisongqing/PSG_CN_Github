namespace AwareTec.Polysmith.UI.FunctionControls.tools
{
    partial class ProgressTipForm
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
            this.worker = new System.ComponentModel.BackgroundWorker();
            this.TipMessageLabel = new System.Windows.Forms.Label();
            this.ActionLabel = new System.Windows.Forms.Label();
            this.CancelButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.ActionProgressBar = new pSystem.UI.ReaLTaiizor.Controls.DungeonProgressBar();
            this.SuspendLayout();
            // 
            // TipMessageLabel
            // 
            this.TipMessageLabel.BackColor = System.Drawing.Color.Transparent;
            this.TipMessageLabel.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.TipMessageLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.TipMessageLabel.Location = new System.Drawing.Point(44, 91);
            this.TipMessageLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TipMessageLabel.Name = "TipMessageLabel";
            this.TipMessageLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TipMessageLabel.Size = new System.Drawing.Size(476, 43);
            this.TipMessageLabel.TabIndex = 37;
            this.TipMessageLabel.Text = "设备连接中";
            this.TipMessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ActionLabel
            // 
            this.ActionLabel.AutoSize = true;
            this.ActionLabel.BackColor = System.Drawing.Color.Transparent;
            this.ActionLabel.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.ActionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.ActionLabel.Location = new System.Drawing.Point(237, 66);
            this.ActionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ActionLabel.Name = "ActionLabel";
            this.ActionLabel.Size = new System.Drawing.Size(105, 24);
            this.ActionLabel.TabIndex = 38;
            this.ActionLabel.Text = "In Progress";
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
            this.CancelButton.Location = new System.Drawing.Point(483, 220);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.PressedColorA = System.Drawing.Color.White;
            this.CancelButton.PressedColorB = System.Drawing.Color.White;
            this.CancelButton.PressedContourColorA = System.Drawing.Color.White;
            this.CancelButton.PressedContourColorB = System.Drawing.Color.White;
            this.CancelButton.Size = new System.Drawing.Size(93, 46);
            this.CancelButton.TabIndex = 57;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ActionProgressBar
            // 
            this.ActionProgressBar.BackColor = System.Drawing.Color.Transparent;
            this.ActionProgressBar.BackColorA = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.ActionProgressBar.BackColorB = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.ActionProgressBar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.ActionProgressBar.DrawHatch = true;
            this.ActionProgressBar.ForeColor = System.Drawing.Color.DimGray;
            this.ActionProgressBar.Location = new System.Drawing.Point(48, 150);
            this.ActionProgressBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ActionProgressBar.Maximum = 100;
            this.ActionProgressBar.Minimum = 0;
            this.ActionProgressBar.MinimumSize = new System.Drawing.Size(58, 25);
            this.ActionProgressBar.Name = "ActionProgressBar";
            this.ActionProgressBar.ProgressColorA = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(233)))), ((int)(((byte)(39)))));
            this.ActionProgressBar.ProgressColorB = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(233)))), ((int)(((byte)(120)))));
            this.ActionProgressBar.ProgressHatchColor = System.Drawing.Color.Transparent;
            this.ActionProgressBar.ShowPercentage = true;
            this.ActionProgressBar.Size = new System.Drawing.Size(458, 30);
            this.ActionProgressBar.TabIndex = 58;
            this.ActionProgressBar.Text = "ActionProgressBar";
            this.ActionProgressBar.Value = 50;
            this.ActionProgressBar.ValueAlignment = pSystem.UI.ReaLTaiizor.Controls.DungeonProgressBar.Alignment.Left;
            // 
            // ProgressTipForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 290);
            this.ControlBox = false;
            this.Controls.Add(this.ActionProgressBar);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.TipMessageLabel);
            this.Controls.Add(this.ActionLabel);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressTipForm";
            this.Opacity = 0.9D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "进入PSG监听模式";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker worker;
        private System.Windows.Forms.Label TipMessageLabel;
        private System.Windows.Forms.Label ActionLabel;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft CancelButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonProgressBar ActionProgressBar;
    }
}