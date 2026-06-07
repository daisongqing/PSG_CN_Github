namespace AwareTec.Polysmith.Cloud.UI.Forms
{
    partial class LoginForm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.label1 = new System.Windows.Forms.Label();
            this.LoginButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.FullUserConfigButton = new System.Windows.Forms.PictureBox();
            this.LoginInfoView = new AwareTec.Polysmith.Cloud.UI.Views.LoginInfoView();
            ((System.ComponentModel.ISupportInitialize)(this.FullUserConfigButton)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label1.Location = new System.Drawing.Point(82, 576);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(380, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Copyright © 2018-2020 Physio, All rights reserved.";
            // 
            // LoginButton
            // 
            this.LoginButton.BackColor = System.Drawing.Color.Transparent;
            this.LoginButton.BorderColor = System.Drawing.Color.Transparent;
            this.LoginButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LoginButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.LoginButton.ForeColor = System.Drawing.Color.White;
            this.LoginButton.Image = null;
            this.LoginButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LoginButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.LoginButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.LoginButton.Location = new System.Drawing.Point(104, 488);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.LoginButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.LoginButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.LoginButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.LoginButton.Size = new System.Drawing.Size(330, 41);
            this.LoginButton.TabIndex = 6;
            this.LoginButton.Text = "登录";
            this.LoginButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // FullUserConfigButton
            // 
            this.FullUserConfigButton.BackColor = System.Drawing.Color.Transparent;
            this.FullUserConfigButton.BackgroundImage = global::AwareTec.Polysmith.Cloud.UI.Properties.Resources.globalSettings;
            this.FullUserConfigButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FullUserConfigButton.Location = new System.Drawing.Point(406, 10);
            this.FullUserConfigButton.Name = "FullUserConfigButton";
            this.FullUserConfigButton.Size = new System.Drawing.Size(24, 24);
            this.FullUserConfigButton.TabIndex = 7;
            this.FullUserConfigButton.TabStop = false;
            this.FullUserConfigButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FullUserConfigButton_MouseClick);
            // 
            // LoginInfoView
            // 
            this.LoginInfoView.BackColor = System.Drawing.Color.White;
            this.LoginInfoView.Location = new System.Drawing.Point(81, 55);
            this.LoginInfoView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LoginInfoView.Name = "LoginInfoView";
            this.LoginInfoView.Size = new System.Drawing.Size(363, 418);
            this.LoginInfoView.TabIndex = 1;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderWidth = 1;
            this.CanResize = false;
            this.ClientSize = new System.Drawing.Size(534, 603);
            this.Controls.Add(this.LoginInfoView);
            this.Controls.Add(this.FullUserConfigButton);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.FullUserConfigButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft LoginButton;
        private System.Windows.Forms.PictureBox FullUserConfigButton;
        private Views.LoginInfoView LoginInfoView;
    }
}

