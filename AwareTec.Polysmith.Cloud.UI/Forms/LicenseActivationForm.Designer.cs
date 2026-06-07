namespace AwareTec.Polysmith.Cloud.UI.Forms
{
    partial class LicenseActivationForm
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
            this.ActivateButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.SerialNumberTextBox = new MinimalistUI.Components.TextboxControls.MinimalistTextbox();
            this.MachineCodeTextBox = new MinimalistUI.Components.TextboxControls.MinimalistTextbox();
            this._CompanyLink = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // ActivateButton
            // 
            this.ActivateButton.BackColor = System.Drawing.Color.Transparent;
            this.ActivateButton.BorderColor = System.Drawing.Color.Transparent;
            this.ActivateButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ActivateButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActivateButton.ForeColor = System.Drawing.Color.White;
            this.ActivateButton.Image = null;
            this.ActivateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ActivateButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.ActivateButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.ActivateButton.Location = new System.Drawing.Point(184, 270);
            this.ActivateButton.Margin = new System.Windows.Forms.Padding(2);
            this.ActivateButton.Name = "ActivateButton";
            this.ActivateButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.ActivateButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.ActivateButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.ActivateButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.ActivateButton.Size = new System.Drawing.Size(98, 33);
            this.ActivateButton.TabIndex = 7;
            this.ActivateButton.Text = "激活";
            this.ActivateButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.ActivateButton.Click += new System.EventHandler(this.ActivateButton_Click);
            // 
            // SerialNumberTextBox
            // 
            this.SerialNumberTextBox.CueText = "";
            this.SerialNumberTextBox.CueTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(147)))), ((int)(((byte)(153)))));
            this.SerialNumberTextBox.CueTextFont = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SerialNumberTextBox.ErrorMessage = "";
            this.SerialNumberTextBox.ErrorMessageTextColorLoseFocus = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            this.SerialNumberTextBox.ErrorMessageTextFont = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SerialNumberTextBox.InputTextColor = System.Drawing.Color.Black;
            this.SerialNumberTextBox.InputTextFont = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SerialNumberTextBox.LeftIcon = global::AwareTec.Polysmith.Cloud.UI.Properties.Resources.serialNumber;
            this.SerialNumberTextBox.Location = new System.Drawing.Point(21, 141);
            this.SerialNumberTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.SerialNumberTextBox.Name = "SerialNumberTextBox";
            this.SerialNumberTextBox.Radius = 18F;
            this.SerialNumberTextBox.ReadOnly = false;
            this.SerialNumberTextBox.RightIcon = null;
            this.SerialNumberTextBox.Size = new System.Drawing.Size(400, 125);
            this.SerialNumberTextBox.TabIndex = 1;
            this.SerialNumberTextBox.TextBoxChar = '\0';
            this.SerialNumberTextBox.TextBoxDisplayName = "序列号";
            this.SerialNumberTextBox.TextBoxDisplayNameColorLoseFocus = System.Drawing.Color.Gray;
            this.SerialNumberTextBox.TextBoxDisplayNameColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.SerialNumberTextBox.TextBoxDisplayNameFont = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SerialNumberTextBox.WireframeColorLoseFocus = System.Drawing.Color.Black;
            this.SerialNumberTextBox.WireframeColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            // 
            // MachineCodeTextBox
            // 
            this.MachineCodeTextBox.CueText = "";
            this.MachineCodeTextBox.CueTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(147)))), ((int)(((byte)(153)))));
            this.MachineCodeTextBox.CueTextFont = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MachineCodeTextBox.ErrorMessage = "";
            this.MachineCodeTextBox.ErrorMessageTextColorLoseFocus = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            this.MachineCodeTextBox.ErrorMessageTextFont = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MachineCodeTextBox.InputTextColor = System.Drawing.Color.Black;
            this.MachineCodeTextBox.InputTextFont = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MachineCodeTextBox.LeftIcon = global::AwareTec.Polysmith.Cloud.UI.Properties.Resources.machineCode;
            this.MachineCodeTextBox.Location = new System.Drawing.Point(21, 41);
            this.MachineCodeTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.MachineCodeTextBox.Name = "MachineCodeTextBox";
            this.MachineCodeTextBox.Radius = 18F;
            this.MachineCodeTextBox.ReadOnly = true;
            this.MachineCodeTextBox.RightIcon = null;
            this.MachineCodeTextBox.Size = new System.Drawing.Size(400, 112);
            this.MachineCodeTextBox.TabIndex = 0;
            this.MachineCodeTextBox.TextBoxChar = '\0';
            this.MachineCodeTextBox.TextBoxDisplayName = "机器码";
            this.MachineCodeTextBox.TextBoxDisplayNameColorLoseFocus = System.Drawing.Color.Gray;
            this.MachineCodeTextBox.TextBoxDisplayNameColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.MachineCodeTextBox.TextBoxDisplayNameFont = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MachineCodeTextBox.WireframeColorLoseFocus = System.Drawing.Color.Black;
            this.MachineCodeTextBox.WireframeColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            // 
            // _CompanyLink
            // 
            this._CompanyLink.AutoSize = true;
            this._CompanyLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this._CompanyLink.Font = new System.Drawing.Font("微软雅黑", 10F);
            this._CompanyLink.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this._CompanyLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this._CompanyLink.Location = new System.Drawing.Point(339, 157);
            this._CompanyLink.Name = "_CompanyLink";
            this._CompanyLink.Size = new System.Drawing.Size(65, 20);
            this._CompanyLink.TabIndex = 13;
            this._CompanyLink.TabStop = true;
            this._CompanyLink.Text = "前往申请";
            this._CompanyLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._CompanyLink_LinkClicked);
            // 
            // LicenseActivationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 318);
            this.Controls.Add(this._CompanyLink);
            this.Controls.Add(this.ActivateButton);
            this.Controls.Add(this.SerialNumberTextBox);
            this.Controls.Add(this.MachineCodeTextBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "LicenseActivationForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "激活序列号";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MinimalistUI.Components.TextboxControls.MinimalistTextbox MachineCodeTextBox;
        private MinimalistUI.Components.TextboxControls.MinimalistTextbox SerialNumberTextBox;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft ActivateButton;
        private System.Windows.Forms.LinkLabel _CompanyLink;
    }
}