namespace AwareTec.Polysmith.UI.Block
{
    partial class ChannelCloneDialog
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
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ChannelName = new pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox();
            this.CombBoxDataSource = new pSystem.UI.ReaLTaiizor.Controls.AloneComboBox();
            this.CoBtInsert = new pSystem.UI.ReaLTaiizor.Controls.AloneComboBox();
            this.RaBtFront = new pSystem.UI.ReaLTaiizor.Controls.FoxRadioButton();
            this.RaBtFallow = new pSystem.UI.ReaLTaiizor.Controls.FoxRadioButton();
            this.SaveButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.CancelButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.SuspendLayout();
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(3, 35);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Size = new System.Drawing.Size(492, 234);
            this.shapeContainer1.TabIndex = 31;
            this.shapeContainer1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(40, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 32;
            this.label1.Text = "通道名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(46, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 20);
            this.label2.TabIndex = 32;
            this.label2.Text = "数 据 源";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(40, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 20);
            this.label3.TabIndex = 32;
            this.label3.Text = "插入位置";
            // 
            // ChannelName
            // 
            this.ChannelName.BackColor = System.Drawing.Color.Transparent;
            this.ChannelName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.ChannelName.EdgeColor = System.Drawing.Color.Empty;
            this.ChannelName.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChannelName.ForeColor = System.Drawing.Color.DimGray;
            this.ChannelName.Location = new System.Drawing.Point(117, 61);
            this.ChannelName.MaxLength = 32767;
            this.ChannelName.Multiline = false;
            this.ChannelName.Name = "ChannelName";
            this.ChannelName.ReadOnly = false;
            this.ChannelName.Size = new System.Drawing.Size(335, 28);
            this.ChannelName.TabIndex = 36;
            this.ChannelName.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.ChannelName.UseSystemPasswordChar = false;
            // 
            // CombBoxDataSource
            // 
            this.CombBoxDataSource.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CombBoxDataSource.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CombBoxDataSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CombBoxDataSource.EnabledCalc = true;
            this.CombBoxDataSource.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CombBoxDataSource.FormattingEnabled = true;
            this.CombBoxDataSource.ItemHeight = 20;
            this.CombBoxDataSource.Location = new System.Drawing.Point(117, 106);
            this.CombBoxDataSource.Name = "CombBoxDataSource";
            this.CombBoxDataSource.Size = new System.Drawing.Size(335, 26);
            this.CombBoxDataSource.TabIndex = 37;
            this.CombBoxDataSource.SelectedValueChanged += new System.EventHandler(this.CombBoxDataSource_SelectedValueChanged);
            // 
            // CoBtInsert
            // 
            this.CoBtInsert.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CoBtInsert.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CoBtInsert.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CoBtInsert.EnabledCalc = true;
            this.CoBtInsert.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CoBtInsert.FormattingEnabled = true;
            this.CoBtInsert.ItemHeight = 20;
            this.CoBtInsert.Location = new System.Drawing.Point(176, 154);
            this.CoBtInsert.Name = "CoBtInsert";
            this.CoBtInsert.Size = new System.Drawing.Size(188, 26);
            this.CoBtInsert.TabIndex = 38;
            this.CoBtInsert.SelectedValueChanged += new System.EventHandler(this.CoBtInsert_SelectedValueChanged);
            // 
            // RaBtFront
            // 
            this.RaBtFront.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.RaBtFront.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.RaBtFront.Checked = false;
            this.RaBtFront.CheckedColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.RaBtFront.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RaBtFront.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.RaBtFront.DisabledCheckedColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.RaBtFront.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(178)))), ((int)(((byte)(190)))));
            this.RaBtFront.EnabledCalc = true;
            this.RaBtFront.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.RaBtFront.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(78)))), ((int)(((byte)(90)))));
            this.RaBtFront.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.RaBtFront.Location = new System.Drawing.Point(117, 157);
            this.RaBtFront.Name = "RaBtFront";
            this.RaBtFront.Size = new System.Drawing.Size(53, 23);
            this.RaBtFront.TabIndex = 39;
            this.RaBtFront.Text = "前";
            // 
            // RaBtFallow
            // 
            this.RaBtFallow.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.RaBtFallow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.RaBtFallow.Checked = true;
            this.RaBtFallow.CheckedColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.RaBtFallow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RaBtFallow.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.RaBtFallow.DisabledCheckedColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.RaBtFallow.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(178)))), ((int)(((byte)(190)))));
            this.RaBtFallow.EnabledCalc = true;
            this.RaBtFallow.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.RaBtFallow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(78)))), ((int)(((byte)(90)))));
            this.RaBtFallow.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.RaBtFallow.Location = new System.Drawing.Point(399, 157);
            this.RaBtFallow.Name = "RaBtFallow";
            this.RaBtFallow.Size = new System.Drawing.Size(53, 23);
            this.RaBtFallow.TabIndex = 40;
            this.RaBtFallow.Text = "后";
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
            this.SaveButton.Location = new System.Drawing.Point(363, 210);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.SaveButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.SaveButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.SaveButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.SaveButton.Size = new System.Drawing.Size(89, 32);
            this.SaveButton.TabIndex = 41;
            this.SaveButton.Text = "提 交";
            this.SaveButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
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
            this.CancelButton.Location = new System.Drawing.Point(238, 210);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.PressedColorA = System.Drawing.Color.White;
            this.CancelButton.PressedColorB = System.Drawing.Color.White;
            this.CancelButton.PressedContourColorA = System.Drawing.Color.White;
            this.CancelButton.PressedContourColorB = System.Drawing.Color.White;
            this.CancelButton.Size = new System.Drawing.Size(89, 32);
            this.CancelButton.TabIndex = 42;
            this.CancelButton.Text = "取 消";
            this.CancelButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ChannelCloneDialog2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CanResize = false;
            this.ClientSize = new System.Drawing.Size(498, 272);
            this.ControlBox = false;
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.RaBtFallow);
            this.Controls.Add(this.RaBtFront);
            this.Controls.Add(this.CoBtInsert);
            this.Controls.Add(this.CombBoxDataSource);
            this.Controls.Add(this.ChannelName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.shapeContainer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChannelCloneDialog2";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "派生";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox ChannelName;
        private pSystem.UI.ReaLTaiizor.Controls.AloneComboBox CombBoxDataSource;
        private pSystem.UI.ReaLTaiizor.Controls.AloneComboBox CoBtInsert;
        private pSystem.UI.ReaLTaiizor.Controls.FoxRadioButton RaBtFront;
        private pSystem.UI.ReaLTaiizor.Controls.FoxRadioButton RaBtFallow;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft SaveButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft CancelButton;
    }
}