namespace AwareTec.Polysmith.UI.FunctionControls.tools
{
    partial class LockDialog
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.SubmitButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.TbPassword = new pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(21, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "解锁密码";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.SubmitButton);
            this.panel1.Controls.Add(this.TbPassword);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(3, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(425, 66);
            this.panel1.TabIndex = 9;
            // 
            // SubmitButton
            // 
            this.SubmitButton.BackColor = System.Drawing.Color.Transparent;
            this.SubmitButton.BorderColor = System.Drawing.Color.Transparent;
            this.SubmitButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SubmitButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.SubmitButton.ForeColor = System.Drawing.Color.White;
            this.SubmitButton.Image = null;
            this.SubmitButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SubmitButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.SubmitButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.SubmitButton.Location = new System.Drawing.Point(327, 21);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.SubmitButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.SubmitButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.SubmitButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.SubmitButton.Size = new System.Drawing.Size(75, 28);
            this.SubmitButton.TabIndex = 50;
            this.SubmitButton.Text = "提 交";
            this.SubmitButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // TbPassword
            // 
            this.TbPassword.BackColor = System.Drawing.Color.White;
            this.TbPassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.TbPassword.EdgeColor = System.Drawing.Color.Empty;
            this.TbPassword.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.TbPassword.ForeColor = System.Drawing.Color.DimGray;
            this.TbPassword.Location = new System.Drawing.Point(92, 21);
            this.TbPassword.MaxLength = 20;
            this.TbPassword.Multiline = false;
            this.TbPassword.Name = "TbPassword";
            this.TbPassword.ReadOnly = false;
            this.TbPassword.Size = new System.Drawing.Size(190, 28);
            this.TbPassword.TabIndex = 49;
            this.TbPassword.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.TbPassword.UseSystemPasswordChar = true;
            this.TbPassword.TextChanged += new System.EventHandler(this.TbPassword_TextChanged);
            this.TbPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbPassword_KeyPress);
            // 
            // LockDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.CanResize = false;
            this.ClientSize = new System.Drawing.Size(431, 104);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LockDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "解锁";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox TbPassword;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft SubmitButton;
    }
}