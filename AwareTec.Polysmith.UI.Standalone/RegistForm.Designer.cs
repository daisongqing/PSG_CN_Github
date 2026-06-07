namespace AwareTec.Polysmith.UI
{
    partial class RegistForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SaveButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.MachineTextBox = new pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox();
            this.MachineNoTextBox = new pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox();
            this._CompanyLink = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.hospitalTextBox = new pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(48, 111);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 27);
            this.label1.TabIndex = 2;
            this.label1.Text = "机器码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(48, 209);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 27);
            this.label2.TabIndex = 2;
            this.label2.Text = "序列号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label3.Location = new System.Drawing.Point(121, 365);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(392, 27);
            this.label3.TabIndex = 5;
            this.label3.Text = "请联系软件所有权负责人获取注册序列号！";
            // 
            // SaveButton
            // 
            this.SaveButton.BackColor = System.Drawing.Color.Transparent;
            this.SaveButton.BorderColor = System.Drawing.Color.Transparent;
            this.SaveButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SaveButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.SaveButton.ForeColor = System.Drawing.Color.White;
            this.SaveButton.Image = null;
            this.SaveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SaveButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.SaveButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.SaveButton.Location = new System.Drawing.Point(250, 294);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.SaveButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.SaveButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.SaveButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.SaveButton.Size = new System.Drawing.Size(126, 42);
            this.SaveButton.TabIndex = 74;
            this.SaveButton.Text = "注 册";
            this.SaveButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // MachineTextBox
            // 
            this.MachineTextBox.BackColor = System.Drawing.Color.Transparent;
            this.MachineTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.MachineTextBox.EdgeColor = System.Drawing.Color.Empty;
            this.MachineTextBox.Font = new System.Drawing.Font("Tahoma", 11F);
            this.MachineTextBox.ForeColor = System.Drawing.Color.DimGray;
            this.MachineTextBox.Location = new System.Drawing.Point(132, 107);
            this.MachineTextBox.MaxLength = 32767;
            this.MachineTextBox.Multiline = false;
            this.MachineTextBox.Name = "MachineTextBox";
            this.MachineTextBox.ReadOnly = false;
            this.MachineTextBox.Size = new System.Drawing.Size(381, 37);
            this.MachineTextBox.TabIndex = 76;
            this.MachineTextBox.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.MachineTextBox.UseSystemPasswordChar = false;
            // 
            // MachineNoTextBox
            // 
            this.MachineNoTextBox.BackColor = System.Drawing.Color.Transparent;
            this.MachineNoTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.MachineNoTextBox.EdgeColor = System.Drawing.Color.Empty;
            this.MachineNoTextBox.Font = new System.Drawing.Font("Tahoma", 11F);
            this.MachineNoTextBox.ForeColor = System.Drawing.Color.DimGray;
            this.MachineNoTextBox.Location = new System.Drawing.Point(132, 209);
            this.MachineNoTextBox.MaxLength = 32767;
            this.MachineNoTextBox.Multiline = false;
            this.MachineNoTextBox.Name = "MachineNoTextBox";
            this.MachineNoTextBox.ReadOnly = false;
            this.MachineNoTextBox.Size = new System.Drawing.Size(381, 37);
            this.MachineNoTextBox.TabIndex = 77;
            this.MachineNoTextBox.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.MachineNoTextBox.UseSystemPasswordChar = false;
            // 
            // _CompanyLink
            // 
            this._CompanyLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this._CompanyLink.AutoSize = true;
            this._CompanyLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this._CompanyLink.Font = new System.Drawing.Font("微软雅黑", 10F);
            this._CompanyLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this._CompanyLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this._CompanyLink.Location = new System.Drawing.Point(533, 209);
            this._CompanyLink.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._CompanyLink.Name = "_CompanyLink";
            this._CompanyLink.Size = new System.Drawing.Size(92, 27);
            this._CompanyLink.TabIndex = 78;
            this._CompanyLink.TabStop = true;
            this._CompanyLink.Text = "前往申请";
            this._CompanyLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._CompanyLink_LinkClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(33, 242);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 27);
            this.label4.TabIndex = 79;
            this.label4.Text = "医院名称";
            this.label4.Visible = false;
            // 
            // hospitalTextBox
            // 
            this.hospitalTextBox.BackColor = System.Drawing.Color.Transparent;
            this.hospitalTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.hospitalTextBox.EdgeColor = System.Drawing.Color.Empty;
            this.hospitalTextBox.Font = new System.Drawing.Font("Tahoma", 11F);
            this.hospitalTextBox.ForeColor = System.Drawing.Color.DimGray;
            this.hospitalTextBox.Location = new System.Drawing.Point(132, 242);
            this.hospitalTextBox.MaxLength = 32767;
            this.hospitalTextBox.Multiline = false;
            this.hospitalTextBox.Name = "hospitalTextBox";
            this.hospitalTextBox.ReadOnly = false;
            this.hospitalTextBox.Size = new System.Drawing.Size(381, 37);
            this.hospitalTextBox.TabIndex = 80;
            this.hospitalTextBox.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.hospitalTextBox.UseSystemPasswordChar = false;
            this.hospitalTextBox.Visible = false;
            // 
            // RegistForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CanResize = false;
            this.ClientSize = new System.Drawing.Size(654, 406);
            this.Controls.Add(this.hospitalTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._CompanyLink);
            this.Controls.Add(this.MachineNoTextBox);
            this.Controls.Add(this.MachineTextBox);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "RegistForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "注册";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft SaveButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox MachineTextBox;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox MachineNoTextBox;
        private System.Windows.Forms.LinkLabel _CompanyLink;
        private System.Windows.Forms.Label label4;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox hospitalTextBox;
    }
}