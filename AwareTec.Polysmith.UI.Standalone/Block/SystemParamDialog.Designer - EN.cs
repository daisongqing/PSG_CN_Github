using Microsoft.VisualBasic.PowerPacks;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace AwareTec.Polysmith.UI.Block
{
    partial class SystemParamDialog
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
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.BrowseEDFButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.BrowseReportButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.BrowseVideoButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.SaveEDFTextBox = new pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox();
            this.SaveReportTextBox = new pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox();
            this.SaveVideoTextBox = new pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox();
            this.ReportFormCombBox = new pSystem.UI.ReaLTaiizor.Controls.AloneComboBox();
            this.AllSleepCombBox = new pSystem.UI.ReaLTaiizor.Controls.AloneComboBox();
            this.BreathFormCombBox = new pSystem.UI.ReaLTaiizor.Controls.AloneComboBox();
            this.CbSO2Drop = new pSystem.UI.ReaLTaiizor.Controls.AloneComboBox();
            this.TbServerAddress = new pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox();
            this.SaveButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.CancelButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.YesToSfCheckBox = new pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox();
            this.NoToSfCheckBox = new pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox();
            this.YesToOpenCheckBox = new pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox();
            this.NoToOpenCheckBox = new pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox();
            this.AgeCheckBox = new pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox();
            this.BirthdayCheckBox = new pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox();
            this.UserAnaTableCheckBox = new pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox();
            this.CarmeAddressTextBox = new pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.minSpo2TextBox = new pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.FullSaveribbonCheckBox = new pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox();
            this.SplitSaveribbonCheckBox = new pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.MaxSpiltVideodungeonTextBox = new pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.enLocalVideoCheckBox = new pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox();
            this.label20 = new System.Windows.Forms.Label();
            this.hospitalNameTextBox = new pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox();
            this.label21 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(3, 35);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Size = new System.Drawing.Size(843, 494);
            this.shapeContainer1.TabIndex = 0;
            this.shapeContainer1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(21, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 27);
            this.label1.TabIndex = 8;
            this.label1.Text = "EDF data storage path";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(19, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 27);
            this.label2.TabIndex = 8;
            this.label2.Text = "Report output path";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(249, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(229, 27);
            this.label3.TabIndex = 8;
            this.label3.Text = "Sleep Report Template";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(19, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(217, 27);
            this.label4.TabIndex = 8;
            this.label4.Text = "Default report format";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(382, 226);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(359, 27);
            this.label5.TabIndex = 8;
            this.label5.Text = "Open the report directly after saving";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(21, 226);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(320, 27);
            this.label6.TabIndex = 8;
            this.label6.Text = "Automatic screen switching time";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(280, 226);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(77, 31);
            this.numericUpDown1.TabIndex = 12;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(196, 228);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(18, 24);
            this.label7.TabIndex = 8;
            this.label7.Text = "s";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(394, 521);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(163, 27);
            this.label8.TabIndex = 8;
            this.label8.Text = "Service Address";
            this.label8.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(21, 324);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(189, 27);
            this.label9.TabIndex = 8;
            this.label9.Text = "Camera IP address";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(19, 130);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(216, 27);
            this.label10.TabIndex = 8;
            this.label10.Text = "Video recording path";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(24, 518);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(232, 27);
            this.label11.TabIndex = 8;
            this.label11.Text = "生成报告前关联算法分析";
            this.label11.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(384, 269);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(260, 27);
            this.label12.TabIndex = 8;
            this.label12.Text = "Patient\'s birth information";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(383, 318);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(435, 27);
            this.label14.TabIndex = 8;
            this.label14.Text = "Oxygen event judgment threshold  Decrease";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(544, 175);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(214, 27);
            this.label15.TabIndex = 8;
            this.label15.Text = "Resp report template";
            // 
            // BrowseEDFButton
            // 
            this.BrowseEDFButton.BackColor = System.Drawing.Color.Transparent;
            this.BrowseEDFButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.BrowseEDFButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BrowseEDFButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.BrowseEDFButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.BrowseEDFButton.Image = null;
            this.BrowseEDFButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BrowseEDFButton.InactiveColorA = System.Drawing.Color.White;
            this.BrowseEDFButton.InactiveColorB = System.Drawing.Color.White;
            this.BrowseEDFButton.Location = new System.Drawing.Point(747, 45);
            this.BrowseEDFButton.Margin = new System.Windows.Forms.Padding(2);
            this.BrowseEDFButton.Name = "BrowseEDFButton";
            this.BrowseEDFButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.BrowseEDFButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.BrowseEDFButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.BrowseEDFButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.BrowseEDFButton.Size = new System.Drawing.Size(87, 28);
            this.BrowseEDFButton.TabIndex = 62;
            this.BrowseEDFButton.Text = "Browse";
            this.BrowseEDFButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.BrowseEDFButton.Click += new System.EventHandler(this.BrowseEDFButton_Click);
            // 
            // BrowseReportButton
            // 
            this.BrowseReportButton.BackColor = System.Drawing.Color.Transparent;
            this.BrowseReportButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.BrowseReportButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BrowseReportButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.BrowseReportButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.BrowseReportButton.Image = null;
            this.BrowseReportButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BrowseReportButton.InactiveColorA = System.Drawing.Color.White;
            this.BrowseReportButton.InactiveColorB = System.Drawing.Color.White;
            this.BrowseReportButton.Location = new System.Drawing.Point(747, 85);
            this.BrowseReportButton.Margin = new System.Windows.Forms.Padding(2);
            this.BrowseReportButton.Name = "BrowseReportButton";
            this.BrowseReportButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.BrowseReportButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.BrowseReportButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.BrowseReportButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.BrowseReportButton.Size = new System.Drawing.Size(87, 28);
            this.BrowseReportButton.TabIndex = 63;
            this.BrowseReportButton.Text = "Browse";
            this.BrowseReportButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.BrowseReportButton.Click += new System.EventHandler(this.BrowseReportButton_Click);
            // 
            // BrowseVideoButton
            // 
            this.BrowseVideoButton.BackColor = System.Drawing.Color.Transparent;
            this.BrowseVideoButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.BrowseVideoButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BrowseVideoButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.BrowseVideoButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.BrowseVideoButton.Image = null;
            this.BrowseVideoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BrowseVideoButton.InactiveColorA = System.Drawing.Color.White;
            this.BrowseVideoButton.InactiveColorB = System.Drawing.Color.White;
            this.BrowseVideoButton.Location = new System.Drawing.Point(747, 125);
            this.BrowseVideoButton.Margin = new System.Windows.Forms.Padding(2);
            this.BrowseVideoButton.Name = "BrowseVideoButton";
            this.BrowseVideoButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.BrowseVideoButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.BrowseVideoButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.BrowseVideoButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.BrowseVideoButton.Size = new System.Drawing.Size(87, 28);
            this.BrowseVideoButton.TabIndex = 64;
            this.BrowseVideoButton.Text = "Browse";
            this.BrowseVideoButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.BrowseVideoButton.Click += new System.EventHandler(this.BrowseVideoButton_Click);
            // 
            // SaveEDFTextBox
            // 
            this.SaveEDFTextBox.BackColor = System.Drawing.Color.Transparent;
            this.SaveEDFTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.SaveEDFTextBox.EdgeColor = System.Drawing.Color.White;
            this.SaveEDFTextBox.Font = new System.Drawing.Font("Tahoma", 11F);
            this.SaveEDFTextBox.ForeColor = System.Drawing.Color.DimGray;
            this.SaveEDFTextBox.Location = new System.Drawing.Point(180, 44);
            this.SaveEDFTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.SaveEDFTextBox.MaxLength = 32767;
            this.SaveEDFTextBox.Multiline = false;
            this.SaveEDFTextBox.Name = "SaveEDFTextBox";
            this.SaveEDFTextBox.ReadOnly = true;
            this.SaveEDFTextBox.Size = new System.Drawing.Size(558, 37);
            this.SaveEDFTextBox.TabIndex = 65;
            this.SaveEDFTextBox.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.SaveEDFTextBox.UseSystemPasswordChar = false;
            // 
            // SaveReportTextBox
            // 
            this.SaveReportTextBox.BackColor = System.Drawing.Color.Transparent;
            this.SaveReportTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.SaveReportTextBox.EdgeColor = System.Drawing.Color.White;
            this.SaveReportTextBox.Font = new System.Drawing.Font("Tahoma", 11F);
            this.SaveReportTextBox.ForeColor = System.Drawing.Color.DimGray;
            this.SaveReportTextBox.Location = new System.Drawing.Point(180, 85);
            this.SaveReportTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.SaveReportTextBox.MaxLength = 32767;
            this.SaveReportTextBox.Multiline = false;
            this.SaveReportTextBox.Name = "SaveReportTextBox";
            this.SaveReportTextBox.ReadOnly = true;
            this.SaveReportTextBox.Size = new System.Drawing.Size(558, 37);
            this.SaveReportTextBox.TabIndex = 66;
            this.SaveReportTextBox.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.SaveReportTextBox.UseSystemPasswordChar = false;
            // 
            // SaveVideoTextBox
            // 
            this.SaveVideoTextBox.BackColor = System.Drawing.Color.Transparent;
            this.SaveVideoTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.SaveVideoTextBox.EdgeColor = System.Drawing.Color.White;
            this.SaveVideoTextBox.Font = new System.Drawing.Font("Tahoma", 11F);
            this.SaveVideoTextBox.ForeColor = System.Drawing.Color.DimGray;
            this.SaveVideoTextBox.Location = new System.Drawing.Point(180, 126);
            this.SaveVideoTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.SaveVideoTextBox.MaxLength = 32767;
            this.SaveVideoTextBox.Multiline = false;
            this.SaveVideoTextBox.Name = "SaveVideoTextBox";
            this.SaveVideoTextBox.ReadOnly = true;
            this.SaveVideoTextBox.Size = new System.Drawing.Size(558, 37);
            this.SaveVideoTextBox.TabIndex = 67;
            this.SaveVideoTextBox.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.SaveVideoTextBox.UseSystemPasswordChar = false;
            // 
            // ReportFormCombBox
            // 
            this.ReportFormCombBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ReportFormCombBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ReportFormCombBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ReportFormCombBox.EnabledCalc = true;
            this.ReportFormCombBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReportFormCombBox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ReportFormCombBox.FormattingEnabled = true;
            this.ReportFormCombBox.ItemHeight = 20;
            this.ReportFormCombBox.Items.AddRange(new object[] {
            "Pdf",
            "Word",
            "Excel"});
            this.ReportFormCombBox.Location = new System.Drawing.Point(170, 175);
            this.ReportFormCombBox.Name = "ReportFormCombBox";
            this.ReportFormCombBox.Size = new System.Drawing.Size(80, 26);
            this.ReportFormCombBox.TabIndex = 68;
            this.ReportFormCombBox.SelectedIndexChanged += new System.EventHandler(this.ReportFormCombBox_SelectedIndexChanged);
            // 
            // AllSleepCombBox
            // 
            this.AllSleepCombBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AllSleepCombBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.AllSleepCombBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AllSleepCombBox.EnabledCalc = true;
            this.AllSleepCombBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AllSleepCombBox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AllSleepCombBox.FormattingEnabled = true;
            this.AllSleepCombBox.ItemHeight = 20;
            this.AllSleepCombBox.Location = new System.Drawing.Point(410, 175);
            this.AllSleepCombBox.Name = "AllSleepCombBox";
            this.AllSleepCombBox.Size = new System.Drawing.Size(135, 26);
            this.AllSleepCombBox.TabIndex = 69;
            // 
            // BreathFormCombBox
            // 
            this.BreathFormCombBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BreathFormCombBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.BreathFormCombBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BreathFormCombBox.EnabledCalc = true;
            this.BreathFormCombBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BreathFormCombBox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BreathFormCombBox.FormattingEnabled = true;
            this.BreathFormCombBox.ItemHeight = 20;
            this.BreathFormCombBox.Location = new System.Drawing.Point(691, 174);
            this.BreathFormCombBox.Name = "BreathFormCombBox";
            this.BreathFormCombBox.Size = new System.Drawing.Size(155, 26);
            this.BreathFormCombBox.TabIndex = 70;
            // 
            // CbSO2Drop
            // 
            this.CbSO2Drop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CbSO2Drop.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CbSO2Drop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbSO2Drop.EnabledCalc = true;
            this.CbSO2Drop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CbSO2Drop.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CbSO2Drop.FormattingEnabled = true;
            this.CbSO2Drop.ItemHeight = 20;
            this.CbSO2Drop.Items.AddRange(new object[] {
            "2 %",
            "3 %",
            "4 %",
            "5 %"});
            this.CbSO2Drop.Location = new System.Drawing.Point(690, 316);
            this.CbSO2Drop.Name = "CbSO2Drop";
            this.CbSO2Drop.Size = new System.Drawing.Size(155, 26);
            this.CbSO2Drop.TabIndex = 71;
            // 
            // TbServerAddress
            // 
            this.TbServerAddress.BackColor = System.Drawing.Color.Transparent;
            this.TbServerAddress.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.TbServerAddress.EdgeColor = System.Drawing.Color.Empty;
            this.TbServerAddress.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TbServerAddress.ForeColor = System.Drawing.Color.DimGray;
            this.TbServerAddress.Location = new System.Drawing.Point(627, 521);
            this.TbServerAddress.MaxLength = 32767;
            this.TbServerAddress.Multiline = false;
            this.TbServerAddress.Name = "TbServerAddress";
            this.TbServerAddress.ReadOnly = false;
            this.TbServerAddress.Size = new System.Drawing.Size(155, 37);
            this.TbServerAddress.TabIndex = 72;
            this.TbServerAddress.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.TbServerAddress.UseSystemPasswordChar = false;
            this.TbServerAddress.Visible = false;
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.BackColor = System.Drawing.Color.Transparent;
            this.SaveButton.BorderColor = System.Drawing.Color.Transparent;
            this.SaveButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SaveButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.SaveButton.ForeColor = System.Drawing.Color.White;
            this.SaveButton.Image = null;
            this.SaveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SaveButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.SaveButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.SaveButton.Location = new System.Drawing.Point(740, 472);
            this.SaveButton.Margin = new System.Windows.Forms.Padding(2);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.SaveButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.SaveButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.SaveButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.SaveButton.Size = new System.Drawing.Size(84, 28);
            this.SaveButton.TabIndex = 73;
            this.SaveButton.Text = "Save";
            this.SaveButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.BackColor = System.Drawing.Color.Transparent;
            this.CancelButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.CancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CancelButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.CancelButton.Image = null;
            this.CancelButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CancelButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.CancelButton.InactiveColorB = System.Drawing.Color.White;
            this.CancelButton.Location = new System.Drawing.Point(606, 472);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.PressedColorA = System.Drawing.Color.White;
            this.CancelButton.PressedColorB = System.Drawing.Color.White;
            this.CancelButton.PressedContourColorA = System.Drawing.Color.White;
            this.CancelButton.PressedContourColorB = System.Drawing.Color.White;
            this.CancelButton.Size = new System.Drawing.Size(84, 28);
            this.CancelButton.TabIndex = 74;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // YesToSfCheckBox
            // 
            this.YesToSfCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.YesToSfCheckBox.BaseColor = System.Drawing.Color.Transparent;
            this.YesToSfCheckBox.BorderColor = System.Drawing.Color.Transparent;
            this.YesToSfCheckBox.CheckBackColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.YesToSfCheckBox.CheckBackColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.YesToSfCheckBox.CheckBorderColorA = System.Drawing.Color.Transparent;
            this.YesToSfCheckBox.CheckBorderColorB = System.Drawing.Color.Transparent;
            this.YesToSfCheckBox.Checked = true;
            this.YesToSfCheckBox.CheckedColor = System.Drawing.Color.White;
            this.YesToSfCheckBox.CompositingQualityType = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            this.YesToSfCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.YesToSfCheckBox.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.YesToSfCheckBox.ForeColor = System.Drawing.Color.Black;
            this.YesToSfCheckBox.Location = new System.Drawing.Point(231, 521);
            this.YesToSfCheckBox.Name = "YesToSfCheckBox";
            this.YesToSfCheckBox.Size = new System.Drawing.Size(48, 21);
            this.YesToSfCheckBox.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.YesToSfCheckBox.TabIndex = 76;
            this.YesToSfCheckBox.Text = "Yes";
            this.YesToSfCheckBox.TextRenderingType = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            this.YesToSfCheckBox.Visible = false;
            this.YesToSfCheckBox.Click += new System.EventHandler(this.YesToSfCheckBox_Click);
            // 
            // NoToSfCheckBox
            // 
            this.NoToSfCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.NoToSfCheckBox.BaseColor = System.Drawing.Color.Transparent;
            this.NoToSfCheckBox.BorderColor = System.Drawing.Color.Transparent;
            this.NoToSfCheckBox.CheckBackColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.NoToSfCheckBox.CheckBackColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.NoToSfCheckBox.CheckBorderColorA = System.Drawing.Color.Transparent;
            this.NoToSfCheckBox.CheckBorderColorB = System.Drawing.Color.Transparent;
            this.NoToSfCheckBox.Checked = false;
            this.NoToSfCheckBox.CheckedColor = System.Drawing.Color.White;
            this.NoToSfCheckBox.CompositingQualityType = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            this.NoToSfCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.NoToSfCheckBox.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.NoToSfCheckBox.ForeColor = System.Drawing.Color.Black;
            this.NoToSfCheckBox.Location = new System.Drawing.Point(291, 518);
            this.NoToSfCheckBox.Name = "NoToSfCheckBox";
            this.NoToSfCheckBox.Size = new System.Drawing.Size(48, 21);
            this.NoToSfCheckBox.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.NoToSfCheckBox.TabIndex = 78;
            this.NoToSfCheckBox.Text = "No";
            this.NoToSfCheckBox.TextRenderingType = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            this.NoToSfCheckBox.Visible = false;
            this.NoToSfCheckBox.Click += new System.EventHandler(this.NoToSfCheckBox_Click);
            // 
            // YesToOpenCheckBox
            // 
            this.YesToOpenCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.YesToOpenCheckBox.BaseColor = System.Drawing.Color.Transparent;
            this.YesToOpenCheckBox.BorderColor = System.Drawing.Color.Transparent;
            this.YesToOpenCheckBox.CheckBackColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.YesToOpenCheckBox.CheckBackColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.YesToOpenCheckBox.CheckBorderColorA = System.Drawing.Color.Transparent;
            this.YesToOpenCheckBox.CheckBorderColorB = System.Drawing.Color.Transparent;
            this.YesToOpenCheckBox.Checked = true;
            this.YesToOpenCheckBox.CheckedColor = System.Drawing.Color.White;
            this.YesToOpenCheckBox.CompositingQualityType = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            this.YesToOpenCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.YesToOpenCheckBox.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.YesToOpenCheckBox.ForeColor = System.Drawing.Color.Black;
            this.YesToOpenCheckBox.Location = new System.Drawing.Point(677, 226);
            this.YesToOpenCheckBox.Name = "YesToOpenCheckBox";
            this.YesToOpenCheckBox.Size = new System.Drawing.Size(48, 21);
            this.YesToOpenCheckBox.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.YesToOpenCheckBox.TabIndex = 80;
            this.YesToOpenCheckBox.Text = "Yes";
            this.YesToOpenCheckBox.TextRenderingType = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            this.YesToOpenCheckBox.Click += new System.EventHandler(this.YesToOpenCheckBox_Click);
            // 
            // NoToOpenCheckBox
            // 
            this.NoToOpenCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.NoToOpenCheckBox.BaseColor = System.Drawing.Color.Transparent;
            this.NoToOpenCheckBox.BorderColor = System.Drawing.Color.Transparent;
            this.NoToOpenCheckBox.CheckBackColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.NoToOpenCheckBox.CheckBackColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.NoToOpenCheckBox.CheckBorderColorA = System.Drawing.Color.Transparent;
            this.NoToOpenCheckBox.CheckBorderColorB = System.Drawing.Color.Transparent;
            this.NoToOpenCheckBox.Checked = false;
            this.NoToOpenCheckBox.CheckedColor = System.Drawing.Color.White;
            this.NoToOpenCheckBox.CompositingQualityType = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            this.NoToOpenCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.NoToOpenCheckBox.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.NoToOpenCheckBox.ForeColor = System.Drawing.Color.Black;
            this.NoToOpenCheckBox.Location = new System.Drawing.Point(749, 226);
            this.NoToOpenCheckBox.Name = "NoToOpenCheckBox";
            this.NoToOpenCheckBox.Size = new System.Drawing.Size(48, 21);
            this.NoToOpenCheckBox.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.NoToOpenCheckBox.TabIndex = 81;
            this.NoToOpenCheckBox.Text = "No";
            this.NoToOpenCheckBox.TextRenderingType = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            this.NoToOpenCheckBox.Click += new System.EventHandler(this.NoToOpenCheckBox_Click);
            // 
            // AgeCheckBox
            // 
            this.AgeCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.AgeCheckBox.BaseColor = System.Drawing.Color.Transparent;
            this.AgeCheckBox.BorderColor = System.Drawing.Color.Transparent;
            this.AgeCheckBox.CheckBackColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.AgeCheckBox.CheckBackColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.AgeCheckBox.CheckBorderColorA = System.Drawing.Color.Transparent;
            this.AgeCheckBox.CheckBorderColorB = System.Drawing.Color.Transparent;
            this.AgeCheckBox.Checked = false;
            this.AgeCheckBox.CheckedColor = System.Drawing.Color.White;
            this.AgeCheckBox.CompositingQualityType = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            this.AgeCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AgeCheckBox.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.AgeCheckBox.ForeColor = System.Drawing.Color.Black;
            this.AgeCheckBox.Location = new System.Drawing.Point(637, 269);
            this.AgeCheckBox.Name = "AgeCheckBox";
            this.AgeCheckBox.Size = new System.Drawing.Size(58, 21);
            this.AgeCheckBox.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.AgeCheckBox.TabIndex = 82;
            this.AgeCheckBox.Text = "Age";
            this.AgeCheckBox.TextRenderingType = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            this.AgeCheckBox.Click += new System.EventHandler(this.AgeCheckBox_Click);
            // 
            // BirthdayCheckBox
            // 
            this.BirthdayCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.BirthdayCheckBox.BaseColor = System.Drawing.Color.Transparent;
            this.BirthdayCheckBox.BorderColor = System.Drawing.Color.Transparent;
            this.BirthdayCheckBox.CheckBackColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.BirthdayCheckBox.CheckBackColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.BirthdayCheckBox.CheckBorderColorA = System.Drawing.Color.Transparent;
            this.BirthdayCheckBox.CheckBorderColorB = System.Drawing.Color.Transparent;
            this.BirthdayCheckBox.Checked = false;
            this.BirthdayCheckBox.CheckedColor = System.Drawing.Color.White;
            this.BirthdayCheckBox.CompositingQualityType = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            this.BirthdayCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BirthdayCheckBox.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.BirthdayCheckBox.ForeColor = System.Drawing.Color.Black;
            this.BirthdayCheckBox.Location = new System.Drawing.Point(704, 271);
            this.BirthdayCheckBox.Name = "BirthdayCheckBox";
            this.BirthdayCheckBox.Size = new System.Drawing.Size(87, 21);
            this.BirthdayCheckBox.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.BirthdayCheckBox.TabIndex = 83;
            this.BirthdayCheckBox.Text = "Birthday";
            this.BirthdayCheckBox.TextRenderingType = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            this.BirthdayCheckBox.Click += new System.EventHandler(this.BirthdayCheckBox_Click);
            // 
            // UserAnaTableCheckBox
            // 
            this.UserAnaTableCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.UserAnaTableCheckBox.BaseColor = System.Drawing.Color.Transparent;
            this.UserAnaTableCheckBox.BorderColor = System.Drawing.Color.Transparent;
            this.UserAnaTableCheckBox.CheckBackColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.UserAnaTableCheckBox.CheckBackColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.UserAnaTableCheckBox.CheckBorderColorA = System.Drawing.Color.Transparent;
            this.UserAnaTableCheckBox.CheckBorderColorB = System.Drawing.Color.Transparent;
            this.UserAnaTableCheckBox.Checked = false;
            this.UserAnaTableCheckBox.CheckedColor = System.Drawing.Color.White;
            this.UserAnaTableCheckBox.CompositingQualityType = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            this.UserAnaTableCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UserAnaTableCheckBox.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.UserAnaTableCheckBox.ForeColor = System.Drawing.Color.Black;
            this.UserAnaTableCheckBox.Location = new System.Drawing.Point(640, 462);
            this.UserAnaTableCheckBox.Name = "UserAnaTableCheckBox";
            this.UserAnaTableCheckBox.Size = new System.Drawing.Size(141, 21);
            this.UserAnaTableCheckBox.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.UserAnaTableCheckBox.TabIndex = 84;
            this.UserAnaTableCheckBox.Text = "使用远程分析平台";
            this.UserAnaTableCheckBox.TextRenderingType = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            this.UserAnaTableCheckBox.Visible = false;
            this.UserAnaTableCheckBox.CheckedChanged += new pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox.CheckedChangedEventHandler(this.UserAnaTableCheckBox_CheckedChanged);
            // 
            // CarmeAddressTextBox
            // 
            this.CarmeAddressTextBox.BackColor = System.Drawing.Color.Transparent;
            this.CarmeAddressTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.CarmeAddressTextBox.EdgeColor = System.Drawing.Color.Empty;
            this.CarmeAddressTextBox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CarmeAddressTextBox.ForeColor = System.Drawing.Color.DimGray;
            this.CarmeAddressTextBox.Location = new System.Drawing.Point(158, 324);
            this.CarmeAddressTextBox.MaxLength = 32767;
            this.CarmeAddressTextBox.Multiline = false;
            this.CarmeAddressTextBox.Name = "CarmeAddressTextBox";
            this.CarmeAddressTextBox.ReadOnly = false;
            this.CarmeAddressTextBox.Size = new System.Drawing.Size(199, 37);
            this.CarmeAddressTextBox.TabIndex = 85;
            this.CarmeAddressTextBox.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.CarmeAddressTextBox.UseSystemPasswordChar = false;
            this.CarmeAddressTextBox.TextChanged += new System.EventHandler(this.CarmeAddressTextBox_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(21, 275);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(336, 27);
            this.label13.TabIndex = 8;
            this.label13.Text = "Minimum oxygen alarm threshold";
            // 
            // minSpo2TextBox
            // 
            this.minSpo2TextBox.BackColor = System.Drawing.Color.Transparent;
            this.minSpo2TextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.minSpo2TextBox.EdgeColor = System.Drawing.Color.Empty;
            this.minSpo2TextBox.Font = new System.Drawing.Font("Tahoma", 11F);
            this.minSpo2TextBox.ForeColor = System.Drawing.Color.DimGray;
            this.minSpo2TextBox.Location = new System.Drawing.Point(277, 271);
            this.minSpo2TextBox.Margin = new System.Windows.Forms.Padding(2);
            this.minSpo2TextBox.MaxLength = 5;
            this.minSpo2TextBox.Multiline = false;
            this.minSpo2TextBox.Name = "minSpo2TextBox";
            this.minSpo2TextBox.ReadOnly = false;
            this.minSpo2TextBox.Size = new System.Drawing.Size(80, 37);
            this.minSpo2TextBox.TabIndex = 86;
            this.minSpo2TextBox.Text = "30";
            this.minSpo2TextBox.TextAlignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.minSpo2TextBox.UseSystemPasswordChar = false;
            this.minSpo2TextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HeightTextBox_KeyPress);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Location = new System.Drawing.Point(227, 278);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(26, 24);
            this.label16.TabIndex = 8;
            this.label16.Text = "%";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.Location = new System.Drawing.Point(19, 369);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(227, 27);
            this.label17.TabIndex = 87;
            this.label17.Text = "Video storage method";
            // 
            // FullSaveribbonCheckBox
            // 
            this.FullSaveribbonCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.FullSaveribbonCheckBox.BaseColor = System.Drawing.Color.Transparent;
            this.FullSaveribbonCheckBox.BorderColor = System.Drawing.Color.Transparent;
            this.FullSaveribbonCheckBox.CheckBackColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.FullSaveribbonCheckBox.CheckBackColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.FullSaveribbonCheckBox.CheckBorderColorA = System.Drawing.Color.Transparent;
            this.FullSaveribbonCheckBox.CheckBorderColorB = System.Drawing.Color.Transparent;
            this.FullSaveribbonCheckBox.Checked = true;
            this.FullSaveribbonCheckBox.CheckedColor = System.Drawing.Color.White;
            this.FullSaveribbonCheckBox.CompositingQualityType = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            this.FullSaveribbonCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FullSaveribbonCheckBox.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.FullSaveribbonCheckBox.ForeColor = System.Drawing.Color.Black;
            this.FullSaveribbonCheckBox.Location = new System.Drawing.Point(194, 369);
            this.FullSaveribbonCheckBox.Name = "FullSaveribbonCheckBox";
            this.FullSaveribbonCheckBox.Size = new System.Drawing.Size(88, 21);
            this.FullSaveribbonCheckBox.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.FullSaveribbonCheckBox.TabIndex = 88;
            this.FullSaveribbonCheckBox.Text = "Complete";
            this.FullSaveribbonCheckBox.TextRenderingType = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            this.FullSaveribbonCheckBox.Click += new System.EventHandler(this.FullSaveribbonCheckBox_Click);
            // 
            // SplitSaveribbonCheckBox
            // 
            this.SplitSaveribbonCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.SplitSaveribbonCheckBox.BaseColor = System.Drawing.Color.Transparent;
            this.SplitSaveribbonCheckBox.BorderColor = System.Drawing.Color.Transparent;
            this.SplitSaveribbonCheckBox.CheckBackColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.SplitSaveribbonCheckBox.CheckBackColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.SplitSaveribbonCheckBox.CheckBorderColorA = System.Drawing.Color.Transparent;
            this.SplitSaveribbonCheckBox.CheckBorderColorB = System.Drawing.Color.Transparent;
            this.SplitSaveribbonCheckBox.Checked = false;
            this.SplitSaveribbonCheckBox.CheckedColor = System.Drawing.Color.White;
            this.SplitSaveribbonCheckBox.CompositingQualityType = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            this.SplitSaveribbonCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SplitSaveribbonCheckBox.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.SplitSaveribbonCheckBox.ForeColor = System.Drawing.Color.Black;
            this.SplitSaveribbonCheckBox.Location = new System.Drawing.Point(292, 370);
            this.SplitSaveribbonCheckBox.Name = "SplitSaveribbonCheckBox";
            this.SplitSaveribbonCheckBox.Size = new System.Drawing.Size(84, 21);
            this.SplitSaveribbonCheckBox.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.SplitSaveribbonCheckBox.TabIndex = 89;
            this.SplitSaveribbonCheckBox.Text = "Slice";
            this.SplitSaveribbonCheckBox.TextRenderingType = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            this.SplitSaveribbonCheckBox.Click += new System.EventHandler(this.SplitSaveribbonCheckBox_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.Location = new System.Drawing.Point(21, 417);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(224, 27);
            this.label18.TabIndex = 90;
            this.label18.Text = "Slice storage Each size";
            // 
            // MaxSpiltVideodungeonTextBox
            // 
            this.MaxSpiltVideodungeonTextBox.BackColor = System.Drawing.Color.Transparent;
            this.MaxSpiltVideodungeonTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.MaxSpiltVideodungeonTextBox.EdgeColor = System.Drawing.Color.Empty;
            this.MaxSpiltVideodungeonTextBox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaxSpiltVideodungeonTextBox.ForeColor = System.Drawing.Color.DimGray;
            this.MaxSpiltVideodungeonTextBox.Location = new System.Drawing.Point(200, 414);
            this.MaxSpiltVideodungeonTextBox.MaxLength = 32767;
            this.MaxSpiltVideodungeonTextBox.Multiline = false;
            this.MaxSpiltVideodungeonTextBox.Name = "MaxSpiltVideodungeonTextBox";
            this.MaxSpiltVideodungeonTextBox.ReadOnly = false;
            this.MaxSpiltVideodungeonTextBox.Size = new System.Drawing.Size(88, 37);
            this.MaxSpiltVideodungeonTextBox.TabIndex = 91;
            this.MaxSpiltVideodungeonTextBox.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.MaxSpiltVideodungeonTextBox.UseSystemPasswordChar = false;
            this.MaxSpiltVideodungeonTextBox.TextChanged += new System.EventHandler(this.MaxSpiltVideodungeonTextBox_TextChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.Location = new System.Drawing.Point(294, 417);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(45, 27);
            this.label19.TabIndex = 92;
            this.label19.Text = "MB";
            // 
            // enLocalVideoCheckBox
            // 
            this.enLocalVideoCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.enLocalVideoCheckBox.BaseColor = System.Drawing.Color.Transparent;
            this.enLocalVideoCheckBox.BorderColor = System.Drawing.Color.Transparent;
            this.enLocalVideoCheckBox.CheckBackColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.enLocalVideoCheckBox.CheckBackColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.enLocalVideoCheckBox.CheckBorderColorA = System.Drawing.Color.Transparent;
            this.enLocalVideoCheckBox.CheckBorderColorB = System.Drawing.Color.Transparent;
            this.enLocalVideoCheckBox.Checked = false;
            this.enLocalVideoCheckBox.CheckedColor = System.Drawing.Color.White;
            this.enLocalVideoCheckBox.CompositingQualityType = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            this.enLocalVideoCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.enLocalVideoCheckBox.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.enLocalVideoCheckBox.ForeColor = System.Drawing.Color.Black;
            this.enLocalVideoCheckBox.Location = new System.Drawing.Point(690, 367);
            this.enLocalVideoCheckBox.Name = "enLocalVideoCheckBox";
            this.enLocalVideoCheckBox.Size = new System.Drawing.Size(77, 21);
            this.enLocalVideoCheckBox.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.enLocalVideoCheckBox.TabIndex = 94;
            this.enLocalVideoCheckBox.Text = "Allow";
            this.enLocalVideoCheckBox.TextRenderingType = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.Location = new System.Drawing.Point(385, 365);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(389, 27);
            this.label20.TabIndex = 93;
            this.label20.Text = "Allow local connection to record videos";
            // 
            // hospitalNameTextBox
            // 
            this.hospitalNameTextBox.BackColor = System.Drawing.Color.Transparent;
            this.hospitalNameTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.hospitalNameTextBox.EdgeColor = System.Drawing.Color.Empty;
            this.hospitalNameTextBox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hospitalNameTextBox.ForeColor = System.Drawing.Color.DimGray;
            this.hospitalNameTextBox.Location = new System.Drawing.Point(490, 414);
            this.hospitalNameTextBox.MaxLength = 32767;
            this.hospitalNameTextBox.Multiline = false;
            this.hospitalNameTextBox.Name = "hospitalNameTextBox";
            this.hospitalNameTextBox.ReadOnly = false;
            this.hospitalNameTextBox.Size = new System.Drawing.Size(334, 37);
            this.hospitalNameTextBox.TabIndex = 95;
            this.hospitalNameTextBox.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.hospitalNameTextBox.UseSystemPasswordChar = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label21.Location = new System.Drawing.Point(375, 418);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(153, 27);
            this.label21.TabIndex = 96;
            this.label21.Text = "Hospital Name";
            // 
            // SystemParamDialog
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CanResize = false;
            this.ClientSize = new System.Drawing.Size(849, 532);
            this.ControlBox = false;
            this.Controls.Add(this.label21);
            this.Controls.Add(this.hospitalNameTextBox);
            this.Controls.Add(this.enLocalVideoCheckBox);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.MaxSpiltVideodungeonTextBox);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.SplitSaveribbonCheckBox);
            this.Controls.Add(this.FullSaveribbonCheckBox);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.minSpo2TextBox);
            this.Controls.Add(this.CarmeAddressTextBox);
            this.Controls.Add(this.UserAnaTableCheckBox);
            this.Controls.Add(this.BirthdayCheckBox);
            this.Controls.Add(this.AgeCheckBox);
            this.Controls.Add(this.NoToOpenCheckBox);
            this.Controls.Add(this.YesToOpenCheckBox);
            this.Controls.Add(this.NoToSfCheckBox);
            this.Controls.Add(this.YesToSfCheckBox);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.TbServerAddress);
            this.Controls.Add(this.CbSO2Drop);
            this.Controls.Add(this.BreathFormCombBox);
            this.Controls.Add(this.AllSleepCombBox);
            this.Controls.Add(this.ReportFormCombBox);
            this.Controls.Add(this.SaveVideoTextBox);
            this.Controls.Add(this.SaveReportTextBox);
            this.Controls.Add(this.SaveEDFTextBox);
            this.Controls.Add(this.BrowseVideoButton);
            this.Controls.Add(this.BrowseReportButton);
            this.Controls.Add(this.BrowseEDFButton);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.shapeContainer1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SystemParamDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "System configuration";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft BrowseEDFButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft BrowseReportButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft BrowseVideoButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox SaveEDFTextBox;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox SaveReportTextBox;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox SaveVideoTextBox;
        private pSystem.UI.ReaLTaiizor.Controls.AloneComboBox ReportFormCombBox;
        private pSystem.UI.ReaLTaiizor.Controls.AloneComboBox AllSleepCombBox;
        private pSystem.UI.ReaLTaiizor.Controls.AloneComboBox BreathFormCombBox;
        private pSystem.UI.ReaLTaiizor.Controls.AloneComboBox CbSO2Drop;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox TbServerAddress;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft SaveButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft CancelButton;
        private pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox YesToSfCheckBox;
        private pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox NoToSfCheckBox;
        private pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox YesToOpenCheckBox;
        private pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox NoToOpenCheckBox;
        private pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox AgeCheckBox;
        private pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox BirthdayCheckBox;
        private pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox UserAnaTableCheckBox;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox CarmeAddressTextBox;
        private Label label13;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox minSpo2TextBox;
        private Label label16;
        private Label label17;
        private pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox FullSaveribbonCheckBox;
        private pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox SplitSaveribbonCheckBox;
        private Label label18;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox MaxSpiltVideodungeonTextBox;
        private Label label19;
        private pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox enLocalVideoCheckBox;
        private Label label20;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonTextBox hospitalNameTextBox;
        private Label label21;
    }
}