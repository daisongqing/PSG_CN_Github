
namespace AwareTec.Polysmith.Cloud.UI.Views
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
        /// 设计器支持所需的方法 - 不要修改
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
            this.PrintButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ShowContantComBox = new System.Windows.Forms.Ctrl.CheckBoxComboBox();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 470);
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
            this.SelectedComboBox.Location = new System.Drawing.Point(278, 8);
            this.SelectedComboBox.Margin = new System.Windows.Forms.Padding(2);
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
            this.maxValueText.Location = new System.Drawing.Point(499, 8);
            this.maxValueText.MaxLength = 32767;
            this.maxValueText.Multiline = false;
            this.maxValueText.Name = "maxValueText";
            this.maxValueText.ReadOnly = false;
            this.maxValueText.Size = new System.Drawing.Size(60, 28);
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
            this.minValueText.Location = new System.Drawing.Point(614, 8);
            this.minValueText.MaxLength = 32767;
            this.minValueText.Multiline = false;
            this.minValueText.Name = "minValueText";
            this.minValueText.ReadOnly = false;
            this.minValueText.Size = new System.Drawing.Size(60, 28);
            this.minValueText.TabIndex = 53;
            this.minValueText.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.minValueText.UseSystemPasswordChar = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(250)))), ((int)(((byte)(245)))));
            this.panel2.Controls.Add(this.PrintButton);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.minValueText);
            this.panel2.Controls.Add(this.maxValueText);
            this.panel2.Controls.Add(this.ShowContantComBox);
            this.panel2.Controls.Add(this.SelectedComboBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(784, 45);
            this.panel2.TabIndex = 1;
            // 
            // PrintButton
            // 
            this.PrintButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PrintButton.BackColor = System.Drawing.Color.Transparent;
            this.PrintButton.BorderColor = System.Drawing.Color.Transparent;
            this.PrintButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PrintButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PrintButton.ForeColor = System.Drawing.Color.White;
            this.PrintButton.Image = null;
            this.PrintButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PrintButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.PrintButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.PrintButton.Location = new System.Drawing.Point(694, 7);
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.PrintButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.PrintButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.PrintButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.PrintButton.Size = new System.Drawing.Size(59, 30);
            this.PrintButton.TabIndex = 48;
            this.PrintButton.Text = "打印";
            this.PrintButton.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(565, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 55;
            this.label4.Text = "最小值";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(451, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 55;
            this.label3.Text = "最大值";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(229, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 55;
            this.label1.Text = "选择项";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(8, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 55;
            this.label2.Text = "显示项";
            // 
            // ShowContantComBox
            // 
            checkBoxProperties1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ShowContantComBox.CheckBoxProperties = checkBoxProperties1;
            this.ShowContantComBox.DisplayMemberSingleItem = "";
            this.ShowContantComBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ShowContantComBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShowContantComBox.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ShowContantComBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.ShowContantComBox.FormattingEnabled = true;
            this.ShowContantComBox.Location = new System.Drawing.Point(57, 9);
            this.ShowContantComBox.Name = "ShowContantComBox";
            this.ShowContantComBox.Size = new System.Drawing.Size(162, 27);
            this.ShowContantComBox.TabIndex = 2;
            // 
            // MulReportChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "MulReportChart";
            this.Size = new System.Drawing.Size(784, 515);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private pSystem.UI.ReaLTaiizor.Controls.PoisonToolTip poisonToolTip1;
        private System.Windows.Forms.Ctrl.CheckBoxComboBox ShowContantComBox;
        private pSystem.UI.ReaLTaiizor.Controls.AloneComboBox SelectedComboBox;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox minValueText;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox maxValueText;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft PrintButton;
    }
}
