namespace AwareTec.Polysmith.UI
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.PasswordTextBox = new pSystem.UI.ReaLTaiizor.Controls.MaterialSingleTextBox();
            this.UserNameTextBox = new pSystem.UI.ReaLTaiizor.Controls.MaterialSingleTextBox();
            this.LoginButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.RegisterButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.RemberMeCheckBox = new pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox();
            this.SelectModeComboBox = new pSystem.UI.ReaLTaiizor.Controls.MaterialComboBox();
            this.SuspendLayout();
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Depth = 0;
            this.PasswordTextBox.ForeColor = System.Drawing.Color.DimGray;
            this.PasswordTextBox.Hint = "输入密码";
            this.PasswordTextBox.Location = new System.Drawing.Point(102, 140);
            this.PasswordTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PasswordTextBox.MaxLength = 32767;
            this.PasswordTextBox.MouseState = pSystem.UI.ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '\0';
            this.PasswordTextBox.SelectedText = "";
            this.PasswordTextBox.SelectionLength = 0;
            this.PasswordTextBox.SelectionStart = 0;
            this.PasswordTextBox.Size = new System.Drawing.Size(335, 22);
            this.PasswordTextBox.TabIndex = 20;
            this.PasswordTextBox.TabStop = false;
            this.PasswordTextBox.UseAccentColor = false;
            this.PasswordTextBox.UseSystemPasswordChar = true;
            this.PasswordTextBox.TextChanged += new System.EventHandler(this.PasswordTextBox_TextChanged);
            // 
            // UserNameTextBox
            // 
            this.UserNameTextBox.Depth = 0;
            this.UserNameTextBox.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.UserNameTextBox.ForeColor = System.Drawing.Color.DimGray;
            this.UserNameTextBox.Hint = "输入账号";
            this.UserNameTextBox.Location = new System.Drawing.Point(102, 91);
            this.UserNameTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UserNameTextBox.MaxLength = 32767;
            this.UserNameTextBox.MouseState = pSystem.UI.ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.UserNameTextBox.Name = "UserNameTextBox";
            this.UserNameTextBox.PasswordChar = '\0';
            this.UserNameTextBox.SelectedText = "";
            this.UserNameTextBox.SelectionLength = 0;
            this.UserNameTextBox.SelectionStart = 0;
            this.UserNameTextBox.Size = new System.Drawing.Size(335, 22);
            this.UserNameTextBox.TabIndex = 19;
            this.UserNameTextBox.TabStop = false;
            this.UserNameTextBox.UseAccentColor = false;
            this.UserNameTextBox.UseSystemPasswordChar = false;
            this.UserNameTextBox.Enter += new System.EventHandler(this.UserNameTextBox_Enter);
            this.UserNameTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UserNameTextBox_KeyPress);
            this.UserNameTextBox.Leave += new System.EventHandler(this.UserNameTextBox_Leave);
            // 
            // LoginButton
            // 
            this.LoginButton.BackColor = System.Drawing.Color.Transparent;
            this.LoginButton.BorderColor = System.Drawing.Color.Transparent;
            this.LoginButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LoginButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.LoginButton.ForeColor = System.Drawing.Color.White;
            this.LoginButton.Image = null;
            this.LoginButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LoginButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.LoginButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.LoginButton.Location = new System.Drawing.Point(102, 302);
            this.LoginButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.LoginButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.LoginButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.LoginButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.LoginButton.Size = new System.Drawing.Size(269, 38);
            this.LoginButton.TabIndex = 42;
            this.LoginButton.Text = "登 录";
            this.LoginButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // RegisterButton
            // 
            this.RegisterButton.BackColor = System.Drawing.Color.Transparent;
            this.RegisterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.RegisterButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RegisterButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.RegisterButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.RegisterButton.Image = null;
            this.RegisterButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.RegisterButton.InactiveColorA = System.Drawing.Color.White;
            this.RegisterButton.InactiveColorB = System.Drawing.Color.White;
            this.RegisterButton.Location = new System.Drawing.Point(102, 352);
            this.RegisterButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RegisterButton.Name = "RegisterButton";
            this.RegisterButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.RegisterButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.RegisterButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.RegisterButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.RegisterButton.Size = new System.Drawing.Size(269, 38);
            this.RegisterButton.TabIndex = 43;
            this.RegisterButton.Text = "注 册";
            this.RegisterButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.RegisterButton.Click += new System.EventHandler(this.RegisterButton_Click);
            // 
            // RemberMeCheckBox
            // 
            this.RemberMeCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.RemberMeCheckBox.BaseColor = System.Drawing.Color.Transparent;
            this.RemberMeCheckBox.BorderColor = System.Drawing.Color.Transparent;
            this.RemberMeCheckBox.CheckBackColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.RemberMeCheckBox.CheckBackColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.RemberMeCheckBox.CheckBorderColorA = System.Drawing.Color.Transparent;
            this.RemberMeCheckBox.CheckBorderColorB = System.Drawing.Color.Transparent;
            this.RemberMeCheckBox.Checked = true;
            this.RemberMeCheckBox.CheckedColor = System.Drawing.Color.White;
            this.RemberMeCheckBox.CompositingQualityType = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            this.RemberMeCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RemberMeCheckBox.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.RemberMeCheckBox.ForeColor = System.Drawing.Color.Black;
            this.RemberMeCheckBox.Location = new System.Drawing.Point(274, 232);
            this.RemberMeCheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RemberMeCheckBox.Name = "RemberMeCheckBox";
            this.RemberMeCheckBox.Size = new System.Drawing.Size(112, 25);
            this.RemberMeCheckBox.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.RemberMeCheckBox.TabIndex = 49;
            this.RemberMeCheckBox.Text = "记住密码";
            this.RemberMeCheckBox.TextRenderingType = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            // 
            // SelectModeComboBox
            // 
            this.SelectModeComboBox.AutoResize = false;
            this.SelectModeComboBox.BackColor = System.Drawing.Color.White;
            this.SelectModeComboBox.Depth = 0;
            this.SelectModeComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.SelectModeComboBox.DropDownHeight = 60;
            this.SelectModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectModeComboBox.DropDownWidth = 121;
            this.SelectModeComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.SelectModeComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SelectModeComboBox.FormattingEnabled = true;
            this.SelectModeComboBox.Hint = "选择模式";
            this.SelectModeComboBox.IntegralHeight = false;
            this.SelectModeComboBox.ItemHeight = 29;
            this.SelectModeComboBox.Location = new System.Drawing.Point(102, 182);
            this.SelectModeComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SelectModeComboBox.MaxDropDownItems = 2;
            this.SelectModeComboBox.MouseState = pSystem.UI.ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            this.SelectModeComboBox.Name = "SelectModeComboBox";
            this.SelectModeComboBox.Size = new System.Drawing.Size(256, 35);
            this.SelectModeComboBox.StartIndex = 0;
            this.SelectModeComboBox.TabIndex = 50;
            this.SelectModeComboBox.UseAccent = false;
            this.SelectModeComboBox.UseTallSize = false;
            // 
            // Login
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CanResize = false;
            this.ClientSize = new System.Drawing.Size(456, 422);
            this.CloseBoxSize = new System.Drawing.Size(32, 32);
            this.Controls.Add(this.SelectModeComboBox);
            this.Controls.Add(this.RemberMeCheckBox);
            this.Controls.Add(this.RegisterButton);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.UserNameTextBox);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximizeBoxSize = new System.Drawing.Size(32, 32);
            this.MinimizeBoxSize = new System.Drawing.Size(32, 32);
            this.Name = "Login";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "多导睡眠呼吸监测数据分析软件 V1.1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Login_FormClosed);
            this.Load += new System.EventHandler(this.Login_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Login_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion
        private pSystem.UI.ReaLTaiizor.Controls.MaterialSingleTextBox UserNameTextBox;
        private pSystem.UI.ReaLTaiizor.Controls.MaterialSingleTextBox PasswordTextBox;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft LoginButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft RegisterButton;
        private pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox RemberMeCheckBox;
        private pSystem.UI.ReaLTaiizor.Controls.MaterialComboBox SelectModeComboBox;
    }
}