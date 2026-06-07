namespace AwareTec.Polysmith.Cloud.UI.Views
{
    partial class OrderRecordSearchView
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
            this.OrderNumberLabel = new System.Windows.Forms.Label();
            this.PatientNameLabel = new System.Windows.Forms.Label();
            this.OrderNumberTextBox = new pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox();
            this.PatientNameTextBox = new pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox();
            this.FromTimeLabel = new System.Windows.Forms.Label();
            this.StartDateTimePicker = new pSystem.UI.ReaLTaiizor.Controls.PoisonDateTime();
            this.EndDateTimePicker = new pSystem.UI.ReaLTaiizor.Controls.PoisonDateTime();
            this.ToTimeLabel = new System.Windows.Forms.Label();
            this.SearchByTimeCheckBox = new pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox();
            this.SearchButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.VisiblePanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.ProgressStatusComboBox = new pSystem.UI.ReaLTaiizor.Controls.AloneComboBox();
            this.ResetButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.VisiblePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // OrderNumberLabel
            // 
            this.OrderNumberLabel.AutoSize = true;
            this.OrderNumberLabel.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.OrderNumberLabel.Location = new System.Drawing.Point(11, 14);
            this.OrderNumberLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.OrderNumberLabel.Name = "OrderNumberLabel";
            this.OrderNumberLabel.Size = new System.Drawing.Size(54, 20);
            this.OrderNumberLabel.TabIndex = 0;
            this.OrderNumberLabel.Text = "订单号";
            // 
            // PatientNameLabel
            // 
            this.PatientNameLabel.AutoSize = true;
            this.PatientNameLabel.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PatientNameLabel.Location = new System.Drawing.Point(178, 14);
            this.PatientNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PatientNameLabel.Name = "PatientNameLabel";
            this.PatientNameLabel.Size = new System.Drawing.Size(69, 20);
            this.PatientNameLabel.TabIndex = 1;
            this.PatientNameLabel.Text = "病人姓名";
            // 
            // OrderNumberTextBox
            // 
            this.OrderNumberTextBox.BackColor = System.Drawing.Color.White;
            this.OrderNumberTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.OrderNumberTextBox.EdgeColor = System.Drawing.Color.Empty;
            this.OrderNumberTextBox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OrderNumberTextBox.ForeColor = System.Drawing.Color.DimGray;
            this.OrderNumberTextBox.Location = new System.Drawing.Point(64, 10);
            this.OrderNumberTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.OrderNumberTextBox.MaxLength = 32767;
            this.OrderNumberTextBox.Multiline = false;
            this.OrderNumberTextBox.Name = "OrderNumberTextBox";
            this.OrderNumberTextBox.ReadOnly = false;
            this.OrderNumberTextBox.Size = new System.Drawing.Size(98, 28);
            this.OrderNumberTextBox.TabIndex = 38;
            this.OrderNumberTextBox.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.OrderNumberTextBox.UseSystemPasswordChar = false;
            this.OrderNumberTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Return_KeyPress);
            // 
            // PatientNameTextBox
            // 
            this.PatientNameTextBox.BackColor = System.Drawing.Color.White;
            this.PatientNameTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.PatientNameTextBox.EdgeColor = System.Drawing.Color.Empty;
            this.PatientNameTextBox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PatientNameTextBox.ForeColor = System.Drawing.Color.DimGray;
            this.PatientNameTextBox.Location = new System.Drawing.Point(244, 10);
            this.PatientNameTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PatientNameTextBox.MaxLength = 32767;
            this.PatientNameTextBox.Multiline = false;
            this.PatientNameTextBox.Name = "PatientNameTextBox";
            this.PatientNameTextBox.ReadOnly = false;
            this.PatientNameTextBox.Size = new System.Drawing.Size(98, 28);
            this.PatientNameTextBox.TabIndex = 39;
            this.PatientNameTextBox.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.PatientNameTextBox.UseSystemPasswordChar = false;
            this.PatientNameTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Return_KeyPress);
            // 
            // FromTimeLabel
            // 
            this.FromTimeLabel.AutoSize = true;
            this.FromTimeLabel.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FromTimeLabel.Location = new System.Drawing.Point(14, 12);
            this.FromTimeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.FromTimeLabel.Name = "FromTimeLabel";
            this.FromTimeLabel.Size = new System.Drawing.Size(24, 20);
            this.FromTimeLabel.TabIndex = 80;
            this.FromTimeLabel.Text = "从";
            // 
            // StartDateTimePicker
            // 
            this.StartDateTimePicker.Location = new System.Drawing.Point(46, 8);
            this.StartDateTimePicker.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.StartDateTimePicker.MinimumSize = new System.Drawing.Size(0, 29);
            this.StartDateTimePicker.Name = "StartDateTimePicker";
            this.StartDateTimePicker.Size = new System.Drawing.Size(150, 30);
            this.StartDateTimePicker.TabIndex = 42;
            this.StartDateTimePicker.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Return_KeyPress);
            // 
            // EndDateTimePicker
            // 
            this.EndDateTimePicker.Location = new System.Drawing.Point(239, 8);
            this.EndDateTimePicker.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.EndDateTimePicker.MinimumSize = new System.Drawing.Size(0, 29);
            this.EndDateTimePicker.Name = "EndDateTimePicker";
            this.EndDateTimePicker.Size = new System.Drawing.Size(150, 30);
            this.EndDateTimePicker.TabIndex = 43;
            this.EndDateTimePicker.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Return_KeyPress);
            // 
            // ToTimeLabel
            // 
            this.ToTimeLabel.AutoSize = true;
            this.ToTimeLabel.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ToTimeLabel.Location = new System.Drawing.Point(206, 12);
            this.ToTimeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ToTimeLabel.Name = "ToTimeLabel";
            this.ToTimeLabel.Size = new System.Drawing.Size(24, 20);
            this.ToTimeLabel.TabIndex = 84;
            this.ToTimeLabel.Text = "至";
            // 
            // SearchByTimeCheckBox
            // 
            this.SearchByTimeCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.SearchByTimeCheckBox.BaseColor = System.Drawing.Color.Transparent;
            this.SearchByTimeCheckBox.BorderColor = System.Drawing.Color.Transparent;
            this.SearchByTimeCheckBox.CheckBackColorA = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.SearchByTimeCheckBox.CheckBackColorB = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.SearchByTimeCheckBox.CheckBorderColorA = System.Drawing.Color.Transparent;
            this.SearchByTimeCheckBox.CheckBorderColorB = System.Drawing.Color.Transparent;
            this.SearchByTimeCheckBox.Checked = false;
            this.SearchByTimeCheckBox.CheckedColor = System.Drawing.Color.White;
            this.SearchByTimeCheckBox.CompositingQualityType = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            this.SearchByTimeCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SearchByTimeCheckBox.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SearchByTimeCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.SearchByTimeCheckBox.Location = new System.Drawing.Point(609, 13);
            this.SearchByTimeCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SearchByTimeCheckBox.Name = "SearchByTimeCheckBox";
            this.SearchByTimeCheckBox.Size = new System.Drawing.Size(116, 22);
            this.SearchByTimeCheckBox.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.SearchByTimeCheckBox.TabIndex = 41;
            this.SearchByTimeCheckBox.Text = "预约监测时间";
            this.SearchByTimeCheckBox.TextRenderingType = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            this.SearchByTimeCheckBox.CheckedChanged += new pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox.CheckedChangedEventHandler(this.SearchByTimeCheckBox_CheckedChanged);
            this.SearchByTimeCheckBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Return_KeyPress);
            // 
            // SearchButton
            // 
            this.SearchButton.BackColor = System.Drawing.Color.Transparent;
            this.SearchButton.BorderColor = System.Drawing.Color.Transparent;
            this.SearchButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SearchButton.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchButton.ForeColor = System.Drawing.Color.White;
            this.SearchButton.Image = null;
            this.SearchButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SearchButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.SearchButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.SearchButton.Location = new System.Drawing.Point(1234, 14);
            this.SearchButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.SearchButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.SearchButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.SearchButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.SearchButton.Size = new System.Drawing.Size(70, 24);
            this.SearchButton.TabIndex = 85;
            this.SearchButton.Text = "查询";
            this.SearchButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.SearchButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SearchButton_MouseClick);
            // 
            // VisiblePanel
            // 
            this.VisiblePanel.Controls.Add(this.FromTimeLabel);
            this.VisiblePanel.Controls.Add(this.StartDateTimePicker);
            this.VisiblePanel.Controls.Add(this.EndDateTimePicker);
            this.VisiblePanel.Controls.Add(this.ToTimeLabel);
            this.VisiblePanel.Location = new System.Drawing.Point(741, 2);
            this.VisiblePanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.VisiblePanel.Name = "VisiblePanel";
            this.VisiblePanel.Size = new System.Drawing.Size(409, 45);
            this.VisiblePanel.TabIndex = 87;
            this.VisiblePanel.VisibleChanged += new System.EventHandler(this.panel1_VisibleChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(370, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 89;
            this.label1.Text = "进度状态";
            // 
            // ProgressStatusComboBox
            // 
            this.ProgressStatusComboBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ProgressStatusComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ProgressStatusComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ProgressStatusComboBox.EnabledCalc = true;
            this.ProgressStatusComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ProgressStatusComboBox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ProgressStatusComboBox.FormattingEnabled = true;
            this.ProgressStatusComboBox.ItemHeight = 20;
            this.ProgressStatusComboBox.Location = new System.Drawing.Point(451, 11);
            this.ProgressStatusComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ProgressStatusComboBox.Name = "ProgressStatusComboBox";
            this.ProgressStatusComboBox.Size = new System.Drawing.Size(155, 26);
            this.ProgressStatusComboBox.TabIndex = 40;
            this.ProgressStatusComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Return_KeyPress);
            // 
            // ResetButton
            // 
            this.ResetButton.BackColor = System.Drawing.Color.Transparent;
            this.ResetButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.ResetButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ResetButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ResetButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.ResetButton.Image = null;
            this.ResetButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ResetButton.InactiveColorA = System.Drawing.Color.White;
            this.ResetButton.InactiveColorB = System.Drawing.Color.White;
            this.ResetButton.Location = new System.Drawing.Point(1343, 14);
            this.ResetButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.ResetButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.ResetButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.ResetButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.ResetButton.Size = new System.Drawing.Size(70, 24);
            this.ResetButton.TabIndex = 90;
            this.ResetButton.Text = "重置";
            this.ResetButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.ResetButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ResetButton_MouseClick);
            // 
            // OrderRecordSearchView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(255)))), ((int)(((byte)(253)))));
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.ProgressStatusComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.VisiblePanel);
            this.Controls.Add(this.SearchByTimeCheckBox);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.PatientNameTextBox);
            this.Controls.Add(this.OrderNumberTextBox);
            this.Controls.Add(this.PatientNameLabel);
            this.Controls.Add(this.OrderNumberLabel);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "OrderRecordSearchView";
            this.Size = new System.Drawing.Size(1443, 53);
            this.VisiblePanel.ResumeLayout(false);
            this.VisiblePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label OrderNumberLabel;
        private System.Windows.Forms.Label PatientNameLabel;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox OrderNumberTextBox;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox PatientNameTextBox;
        private System.Windows.Forms.Label FromTimeLabel;
        private pSystem.UI.ReaLTaiizor.Controls.PoisonDateTime StartDateTimePicker;
        private pSystem.UI.ReaLTaiizor.Controls.PoisonDateTime EndDateTimePicker;
        private System.Windows.Forms.Label ToTimeLabel;
        private pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox SearchByTimeCheckBox;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft SearchButton;
        private System.Windows.Forms.Panel VisiblePanel;
        private System.Windows.Forms.Label label1;
        private pSystem.UI.ReaLTaiizor.Controls.AloneComboBox ProgressStatusComboBox;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft ResetButton;
    }
}
