namespace AwareTec.Polysmith.UI.FunctionControls.tools
{
    partial class MulReportChart
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Ctrl.CheckBoxProperties checkBoxProperties1 = new System.Windows.Forms.Ctrl.CheckBoxProperties();
            this.panel1 = new System.Windows.Forms.Panel();
            this.poisonToolTip1 = new pSystem.UI.ReaLTaiizor.Controls.PoisonToolTip();
            this.SelectedComboBox = new pSystem.UI.ReaLTaiizor.Controls.AloneComboBox();
            this.maxValueText = new pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox();
            this.minValueText = new pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.CommitButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxComboBox1 = new System.Windows.Forms.Ctrl.CheckBoxComboBox();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 68);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1176, 704);
            this.panel1.TabIndex = 0;
            // 
            // poisonToolTip1
            // 
            this.poisonToolTip1.Style = pSystem.UI.ReaLTaiizor.Enum.Poison.ColorStyle.Blue;
            this.poisonToolTip1.StyleManager = null;
            this.poisonToolTip1.Theme = pSystem.UI.ReaLTaiizor.Enum.Poison.ThemeStyle.Light;
            // 
            // SelectedComboBox
            // 
            this.SelectedComboBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SelectedComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.SelectedComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectedComboBox.EnabledCalc = true;
            this.SelectedComboBox.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.SelectedComboBox.ForeColor = System.Drawing.Color.DimGray;
            this.SelectedComboBox.FormattingEnabled = true;
            this.SelectedComboBox.ItemHeight = 23;
            this.SelectedComboBox.Location = new System.Drawing.Point(511, 12);
            this.SelectedComboBox.Name = "SelectedComboBox";
            this.SelectedComboBox.Size = new System.Drawing.Size(159, 29);
            this.SelectedComboBox.TabIndex = 51;
            // 
            // maxValueText
            // 
            this.maxValueText.BackColor = System.Drawing.Color.Transparent;
            this.maxValueText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.maxValueText.EdgeColor = System.Drawing.Color.Empty;
            this.maxValueText.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.maxValueText.ForeColor = System.Drawing.Color.DimGray;
            this.maxValueText.Location = new System.Drawing.Point(821, 12);
            this.maxValueText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.maxValueText.MaxLength = 32767;
            this.maxValueText.Multiline = false;
            this.maxValueText.Name = "maxValueText";
            this.maxValueText.ReadOnly = false;
            this.maxValueText.Size = new System.Drawing.Size(90, 37);
            this.maxValueText.TabIndex = 52;
            this.maxValueText.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.maxValueText.UseSystemPasswordChar = false;
            // 
            // minValueText
            // 
            this.minValueText.BackColor = System.Drawing.Color.Transparent;
            this.minValueText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.minValueText.EdgeColor = System.Drawing.Color.Empty;
            this.minValueText.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minValueText.ForeColor = System.Drawing.Color.DimGray;
            this.minValueText.Location = new System.Drawing.Point(1020, 12);
            this.minValueText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.minValueText.MaxLength = 32767;
            this.minValueText.Multiline = false;
            this.minValueText.Name = "minValueText";
            this.minValueText.ReadOnly = false;
            this.minValueText.Size = new System.Drawing.Size(90, 37);
            this.minValueText.TabIndex = 53;
            this.minValueText.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.minValueText.UseSystemPasswordChar = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.panel2.Controls.Add(this.CommitButton);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.minValueText);
            this.panel2.Controls.Add(this.maxValueText);
            this.panel2.Controls.Add(this.checkBoxComboBox1);
            this.panel2.Controls.Add(this.SelectedComboBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1176, 68);
            this.panel2.TabIndex = 1;
            // 
            // CommitButton
            // 
            this.CommitButton.BackColor = System.Drawing.Color.Transparent;
            this.CommitButton.BorderColor = System.Drawing.Color.Transparent;
            this.CommitButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CommitButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.CommitButton.ForeColor = System.Drawing.Color.White;
            this.CommitButton.Image = null;
            this.CommitButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CommitButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(116)))), ((int)(((byte)(238)))));
            this.CommitButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(116)))), ((int)(((byte)(238)))));
            this.CommitButton.Location = new System.Drawing.Point(1136, 10);
            this.CommitButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CommitButton.Name = "CommitButton";
            this.CommitButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(116)))), ((int)(((byte)(238)))));
            this.CommitButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(116)))), ((int)(((byte)(238)))));
            this.CommitButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(116)))), ((int)(((byte)(238)))));
            this.CommitButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(116)))), ((int)(((byte)(238)))));
            this.CommitButton.Size = new System.Drawing.Size(88, 45);
            this.CommitButton.TabIndex = 58;
            this.CommitButton.Text = "Print";
            this.CommitButton.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(917, 21);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 24);
            this.label4.TabIndex = 55;
            this.label4.Text = "Min Value";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(715, 21);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 24);
            this.label3.TabIndex = 55;
            this.label3.Text = "Max Value";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(424, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 24);
            this.label1.TabIndex = 55;
            this.label1.Text = "Selection";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(4, 21);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 24);
            this.label2.TabIndex = 55;
            this.label2.Text = "Display Items";
            // 
            // checkBoxComboBox1
            // 
            checkBoxProperties1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxComboBox1.CheckBoxProperties = checkBoxProperties1;
            this.checkBoxComboBox1.DisplayMemberSingleItem = "";
            this.checkBoxComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.checkBoxComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxComboBox1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.checkBoxComboBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.checkBoxComboBox1.FormattingEnabled = true;
            this.checkBoxComboBox1.Location = new System.Drawing.Point(145, 14);
            this.checkBoxComboBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxComboBox1.Name = "checkBoxComboBox1";
            this.checkBoxComboBox1.Size = new System.Drawing.Size(255, 35);
            this.checkBoxComboBox1.TabIndex = 2;
            // 
            // MulReportChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MulReportChart";
            this.Size = new System.Drawing.Size(1176, 772);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private pSystem.UI.ReaLTaiizor.Controls.PoisonToolTip poisonToolTip1;
        private System.Windows.Forms.Ctrl.CheckBoxComboBox checkBoxComboBox1;
        private pSystem.UI.ReaLTaiizor.Controls.AloneComboBox SelectedComboBox;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox minValueText;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox maxValueText;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft CommitButton;
    }
}
