namespace AwareTec.Polysmith.UI.FunctionControls
{
    partial class RealDataView
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
            this.components = new System.ComponentModel.Container();
            this.label3 = new System.Windows.Forms.Label();
            this.ChartArea = new AwareTec.Polysmith.pChart.Chart();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SetTimeButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.DingBiaoButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.ZkTestButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.PasueButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.StartRunButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.QuitButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.HeadClientContent = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pictrueBoxEx1 = new DotNetCtl.CSharpWin.PictrueBoxEx();
            this.ts2 = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            ((System.ComponentModel.ISupportInitialize)(this.pictrueBoxEx1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label3.ForeColor = System.Drawing.Color.DarkSeaGreen;
            this.label3.Location = new System.Drawing.Point(120, 4);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 32);
            this.label3.TabIndex = 6;
            // 
            // ChartArea
            // 
            this.ChartArea.BaseTimeLineSpan = 30;
            this.ChartArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChartArea.FnStop = true;
            this.ChartArea.isRealDataCurve = true;
            this.ChartArea.Location = new System.Drawing.Point(0, 0);
            this.ChartArea.Margin = new System.Windows.Forms.Padding(6);
            this.ChartArea.Name = "ChartArea";
            this.ChartArea.OtherBaseTimeLineSpan = 30;
            this.ChartArea.Size = new System.Drawing.Size(1162, 810);
            this.ChartArea.TabIndex = 0;
            this.ChartArea.TimeLineVisible = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // SetTimeButton
            // 
            this.SetTimeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SetTimeButton.BackColor = System.Drawing.Color.Transparent;
            this.SetTimeButton.BorderColor = System.Drawing.Color.Transparent;
            this.SetTimeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SetTimeButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.SetTimeButton.ForeColor = System.Drawing.Color.White;
            this.SetTimeButton.Image = null;
            this.SetTimeButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SetTimeButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.SetTimeButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.SetTimeButton.Location = new System.Drawing.Point(357, 0);
            this.SetTimeButton.Name = "SetTimeButton";
            this.SetTimeButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.SetTimeButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.SetTimeButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.SetTimeButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.SetTimeButton.Size = new System.Drawing.Size(111, 42);
            this.SetTimeButton.TabIndex = 74;
            this.SetTimeButton.Text = "定时设置";
            this.SetTimeButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.SetTimeButton.Click += new System.EventHandler(this.SetTimeButton_Click);
            // 
            // DingBiaoButton
            // 
            this.DingBiaoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DingBiaoButton.BackColor = System.Drawing.Color.Transparent;
            this.DingBiaoButton.BorderColor = System.Drawing.Color.Transparent;
            this.DingBiaoButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DingBiaoButton.Enabled = false;
            this.DingBiaoButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.DingBiaoButton.ForeColor = System.Drawing.Color.White;
            this.DingBiaoButton.Image = null;
            this.DingBiaoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DingBiaoButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.DingBiaoButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.DingBiaoButton.Location = new System.Drawing.Point(508, 0);
            this.DingBiaoButton.Name = "DingBiaoButton";
            this.DingBiaoButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.DingBiaoButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.DingBiaoButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.DingBiaoButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.DingBiaoButton.Size = new System.Drawing.Size(111, 42);
            this.DingBiaoButton.TabIndex = 75;
            this.DingBiaoButton.Text = "定 标";
            this.DingBiaoButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.DingBiaoButton.Click += new System.EventHandler(this.DingBiaoButton_Click);
            // 
            // ZkTestButton
            // 
            this.ZkTestButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ZkTestButton.BackColor = System.Drawing.Color.Transparent;
            this.ZkTestButton.BorderColor = System.Drawing.Color.Transparent;
            this.ZkTestButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ZkTestButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ZkTestButton.ForeColor = System.Drawing.Color.White;
            this.ZkTestButton.Image = null;
            this.ZkTestButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ZkTestButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.ZkTestButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.ZkTestButton.Location = new System.Drawing.Point(651, 0);
            this.ZkTestButton.Name = "ZkTestButton";
            this.ZkTestButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.ZkTestButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.ZkTestButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.ZkTestButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.ZkTestButton.Size = new System.Drawing.Size(111, 42);
            this.ZkTestButton.TabIndex = 76;
            this.ZkTestButton.Text = "阻抗测试";
            this.ZkTestButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.ZkTestButton.Click += new System.EventHandler(this.ZkTestButton_Click);
            // 
            // PasueButton
            // 
            this.PasueButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PasueButton.BackColor = System.Drawing.Color.Transparent;
            this.PasueButton.BorderColor = System.Drawing.Color.Transparent;
            this.PasueButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PasueButton.Enabled = false;
            this.PasueButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.PasueButton.ForeColor = System.Drawing.Color.White;
            this.PasueButton.Image = null;
            this.PasueButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PasueButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.PasueButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.PasueButton.Location = new System.Drawing.Point(810, 0);
            this.PasueButton.Name = "PasueButton";
            this.PasueButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.PasueButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.PasueButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.PasueButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.PasueButton.Size = new System.Drawing.Size(111, 42);
            this.PasueButton.TabIndex = 77;
            this.PasueButton.Text = "暂 停";
            this.PasueButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.PasueButton.Click += new System.EventHandler(this.PasueButton_Click);
            // 
            // StartRunButton
            // 
            this.StartRunButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StartRunButton.BackColor = System.Drawing.Color.Transparent;
            this.StartRunButton.BorderColor = System.Drawing.Color.Transparent;
            this.StartRunButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StartRunButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.StartRunButton.ForeColor = System.Drawing.Color.White;
            this.StartRunButton.Image = null;
            this.StartRunButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StartRunButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.StartRunButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.StartRunButton.Location = new System.Drawing.Point(944, 0);
            this.StartRunButton.Name = "StartRunButton";
            this.StartRunButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.StartRunButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.StartRunButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.StartRunButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.StartRunButton.Size = new System.Drawing.Size(111, 42);
            this.StartRunButton.TabIndex = 78;
            this.StartRunButton.Text = "开始采集";
            this.StartRunButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.StartRunButton.Click += new System.EventHandler(this.StartRunButton_Click);
            // 
            // QuitButton
            // 
            this.QuitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.QuitButton.BackColor = System.Drawing.Color.Transparent;
            this.QuitButton.BorderColor = System.Drawing.Color.Transparent;
            this.QuitButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.QuitButton.Font = new System.Drawing.Font("宋体", 15F);
            this.QuitButton.ForeColor = System.Drawing.Color.Red;
            this.QuitButton.Image = null;
            this.QuitButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.QuitButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.QuitButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.QuitButton.Location = new System.Drawing.Point(1095, 0);
            this.QuitButton.Name = "QuitButton";
            this.QuitButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.QuitButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.QuitButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.QuitButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.QuitButton.Size = new System.Drawing.Size(64, 42);
            this.QuitButton.TabIndex = 79;
            this.QuitButton.Text = "X";
            this.QuitButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.QuitButton.Click += new System.EventHandler(this.QuitButton_Click);
            // 
            // HeadClientContent
            // 
            this.HeadClientContent.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.HeadClientContent.Name = "HeadClientContent";
            this.HeadClientContent.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // pictrueBoxEx1
            // 
            this.pictrueBoxEx1.BackgroundImage = global::AwareTec.Polysmith.UI.Properties.Resources.warningsound1;
            this.pictrueBoxEx1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictrueBoxEx1.ClickedMouseDownImage = null;
            this.pictrueBoxEx1.ClickedMouseOnImage = null;
            this.pictrueBoxEx1.ClickedMouseUpImage = null;
            this.pictrueBoxEx1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictrueBoxEx1.Location = new System.Drawing.Point(3, 0);
            this.pictrueBoxEx1.Margin = new System.Windows.Forms.Padding(4);
            this.pictrueBoxEx1.MouseDownImage = global::AwareTec.Polysmith.UI.Properties.Resources.warningsound2;
            this.pictrueBoxEx1.MouseOnImage = global::AwareTec.Polysmith.UI.Properties.Resources.warningsound2;
            this.pictrueBoxEx1.MouseUpImage = global::AwareTec.Polysmith.UI.Properties.Resources.warningsound1;
            this.pictrueBoxEx1.Name = "pictrueBoxEx1";
            this.pictrueBoxEx1.Size = new System.Drawing.Size(45, 45);
            this.pictrueBoxEx1.TabIndex = 80;
            this.pictrueBoxEx1.TabStop = false;
            this.pictrueBoxEx1.Visible = false;
            this.pictrueBoxEx1.Click += new System.EventHandler(this.pictrueBoxEx1_Click);
            // 
            // ts2
            // 
            this.ts2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ts2.BackColor = System.Drawing.Color.Transparent;
            this.ts2.BorderColor = System.Drawing.Color.Transparent;
            this.ts2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ts2.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ts2.ForeColor = System.Drawing.Color.White;
            this.ts2.Image = null;
            this.ts2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ts2.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.ts2.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.ts2.Location = new System.Drawing.Point(248, 0);
            this.ts2.Name = "ts2";
            this.ts2.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.ts2.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.ts2.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.ts2.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.ts2.Size = new System.Drawing.Size(74, 42);
            this.ts2.TabIndex = 81;
            this.ts2.Text = " 设备";
            this.ts2.TextAlignment = System.Drawing.StringAlignment.Center;
            this.ts2.Click += new System.EventHandler(this.ts2_Click);
            // 
            // RealDataView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ts2);
            this.Controls.Add(this.pictrueBoxEx1);
            this.Controls.Add(this.QuitButton);
            this.Controls.Add(this.StartRunButton);
            this.Controls.Add(this.PasueButton);
            this.Controls.Add(this.ZkTestButton);
            this.Controls.Add(this.DingBiaoButton);
            this.Controls.Add(this.SetTimeButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ChartArea);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "RealDataView";
            this.Size = new System.Drawing.Size(1162, 810);
            ((System.ComponentModel.ISupportInitialize)(this.pictrueBoxEx1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public pChart.Chart ChartArea;
        private System.Windows.Forms.ContextMenuStrip HeadClientContent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Timer timer1;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft SetTimeButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft DingBiaoButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft ZkTestButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft PasueButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft StartRunButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft QuitButton;
        private DotNetCtl.CSharpWin.PictrueBoxEx pictrueBoxEx1;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft ts2;
    }
}
