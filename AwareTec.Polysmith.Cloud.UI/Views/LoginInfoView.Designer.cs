using MinimalistUI.EnumModels;

namespace AwareTec.Polysmith.Cloud.UI.Views
{
    partial class LoginInfoView
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
            this.RememberPasswordCheckBox = new pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox();
            this.AccountTextbox = new MinimalistUI.Components.TextboxControls.MinimalistTextbox();
            this.PasswordTextbox = new MinimalistUI.Components.TextboxControls.MinimalistTextbox();
            this.ModeTypeSwitch = new MinimalistUI.Components.SwitchControls.SwitchControl();
            this.SuspendLayout();
            // 
            // RememberPasswordCheckBox
            // 
            this.RememberPasswordCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.RememberPasswordCheckBox.BaseColor = System.Drawing.Color.Transparent;
            this.RememberPasswordCheckBox.BorderColor = System.Drawing.Color.Transparent;
            this.RememberPasswordCheckBox.CheckBackColorA = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.RememberPasswordCheckBox.CheckBackColorB = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.RememberPasswordCheckBox.CheckBorderColorA = System.Drawing.Color.Transparent;
            this.RememberPasswordCheckBox.CheckBorderColorB = System.Drawing.Color.Transparent;
            this.RememberPasswordCheckBox.Checked = true;
            this.RememberPasswordCheckBox.CheckedColor = System.Drawing.Color.White;
            this.RememberPasswordCheckBox.CompositingQualityType = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            this.RememberPasswordCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RememberPasswordCheckBox.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RememberPasswordCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.RememberPasswordCheckBox.Location = new System.Drawing.Point(233, 380);
            this.RememberPasswordCheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RememberPasswordCheckBox.Name = "RememberPasswordCheckBox";
            this.RememberPasswordCheckBox.Size = new System.Drawing.Size(111, 28);
            this.RememberPasswordCheckBox.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.RememberPasswordCheckBox.TabIndex = 3;
            this.RememberPasswordCheckBox.Text = "记住密码";
            this.RememberPasswordCheckBox.TextRenderingType = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            // 
            // AccountTextbox
            // 
            this.AccountTextbox.CueText = "";
            this.AccountTextbox.CueTextColor = System.Drawing.Color.Black;
            this.AccountTextbox.CueTextFont = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AccountTextbox.ErrorMessage = "";
            this.AccountTextbox.ErrorMessageTextColorLoseFocus = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            this.AccountTextbox.ErrorMessageTextFont = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AccountTextbox.InputTextColor = System.Drawing.Color.Black;
            this.AccountTextbox.InputTextFont = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AccountTextbox.LeftIcon = global::AwareTec.Polysmith.Cloud.UI.Properties.Resources.userIcon;
            this.AccountTextbox.Location = new System.Drawing.Point(13, 95);
            this.AccountTextbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AccountTextbox.Name = "AccountTextbox";
            this.AccountTextbox.Radius = 18F;
            this.AccountTextbox.ReadOnly = false;
            this.AccountTextbox.RightIcon = null;
            this.AccountTextbox.Size = new System.Drawing.Size(331, 141);
            this.AccountTextbox.TabIndex = 1;
            this.AccountTextbox.TextBoxChar = '\0';
            this.AccountTextbox.TextBoxDisplayName = "用户名";
            this.AccountTextbox.TextBoxDisplayNameColorLoseFocus = System.Drawing.Color.Black;
            this.AccountTextbox.TextBoxDisplayNameColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.AccountTextbox.TextBoxDisplayNameFont = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AccountTextbox.WireframeColorLoseFocus = System.Drawing.Color.Black;
            this.AccountTextbox.WireframeColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            //this.AccountTextbox.TextBoxLoseFocus += new System.EventHandler<MinimalistUI.CustomEventArgs.TextboxControls.TextBoxLoseFocusEventArgs>(this.AccountTextbox_TextBoxLoseFocus);
            // 
            // PasswordTextbox
            // 
            this.PasswordTextbox.CueText = "";
            this.PasswordTextbox.CueTextColor = System.Drawing.Color.Black;
            this.PasswordTextbox.CueTextFont = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PasswordTextbox.ErrorMessage = "";
            this.PasswordTextbox.ErrorMessageTextColorLoseFocus = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            this.PasswordTextbox.ErrorMessageTextFont = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PasswordTextbox.InputTextColor = System.Drawing.Color.Black;
            this.PasswordTextbox.InputTextFont = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PasswordTextbox.LeftIcon = global::AwareTec.Polysmith.Cloud.UI.Properties.Resources.passwordIcon;
            this.PasswordTextbox.Location = new System.Drawing.Point(13, 240);
            this.PasswordTextbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PasswordTextbox.Name = "PasswordTextbox";
            this.PasswordTextbox.Radius = 18F;
            this.PasswordTextbox.ReadOnly = false;
            this.PasswordTextbox.RightIcon = null;
            this.PasswordTextbox.Size = new System.Drawing.Size(331, 135);
            this.PasswordTextbox.TabIndex = 2;
            this.PasswordTextbox.TextBoxChar = '⬤';
            this.PasswordTextbox.TextBoxDisplayName = "密码";
            this.PasswordTextbox.TextBoxDisplayNameColorLoseFocus = System.Drawing.Color.Black;
            this.PasswordTextbox.TextBoxDisplayNameColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.PasswordTextbox.TextBoxDisplayNameFont = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PasswordTextbox.WireframeColorLoseFocus = System.Drawing.Color.Black;
            this.PasswordTextbox.WireframeColorOnFocus = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            // 
            // ModeTypeSwitch
            // 
            this.ModeTypeSwitch.CloseColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(241)))), ((int)(((byte)(238)))));
            this.ModeTypeSwitch.CloseTextColor = System.Drawing.SystemColors.Desktop;
            this.ModeTypeSwitch.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ModeTypeSwitch.LeftStr = "成人版";
            this.ModeTypeSwitch.Location = new System.Drawing.Point(13, 22);
            this.ModeTypeSwitch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ModeTypeSwitch.Name = "ModeTypeSwitch";
            this.ModeTypeSwitch.OpenColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.ModeTypeSwitch.OpenTextColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ModeTypeSwitch.Radius = 18F;
            this.ModeTypeSwitch.RightStr = "儿童版";
            this.ModeTypeSwitch.Size = new System.Drawing.Size(331, 56);
            this.ModeTypeSwitch.TabIndex = 4;
            this.ModeTypeSwitch.Text = "switchControl1";
            this.ModeTypeSwitch.SwitchChanged += new System.EventHandler<MinimalistUI.CustomEventArgs.SwitchControls.SwitchChangedEventArgs>(this.ModeTypeSwitch_SwitchChanged);
            // 
            // LoginInfoView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.RememberPasswordCheckBox);
            this.Controls.Add(this.AccountTextbox);
            this.Controls.Add(this.PasswordTextbox);
            this.Controls.Add(this.ModeTypeSwitch);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "LoginInfoView";
            this.Size = new System.Drawing.Size(363, 418);
            this.ResumeLayout(false);

        }

        #endregion
        private MinimalistUI.Components.SwitchControls.SwitchControl ModeTypeSwitch;
        private MinimalistUI.Components.TextboxControls.MinimalistTextbox PasswordTextbox;
        private MinimalistUI.Components.TextboxControls.MinimalistTextbox AccountTextbox;
        private pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox RememberPasswordCheckBox;
    }
}
