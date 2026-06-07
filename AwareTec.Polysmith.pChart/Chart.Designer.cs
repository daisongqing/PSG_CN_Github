namespace AwareTec.Polysmith.pChart
{
    partial class Chart
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel6 = new System.Windows.Forms.Panel();
            this.bak_curveArea = new AwareTec.Polysmith.pChart.curveArea();
            this.master_CurveArea = new AwareTec.Polysmith.pChart.curveArea();
            this.ChannelSetting = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.HideChannel = new System.Windows.Forms.ToolStripMenuItem();
            this.HideWave = new System.Windows.Forms.ToolStripMenuItem();
            this.panel6.SuspendLayout();
            this.ChannelSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(169, 606);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1024, 29);
            this.panel3.TabIndex = 2;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.bak_curveArea);
            this.panel6.Controls.Add(this.master_CurveArea);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(169, 29);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(855, 606);
            this.panel6.TabIndex = 3;
            // 
            // bak_curveArea
            // 
            this.bak_curveArea.BaseTimeLineSpan = 30;
            this.bak_curveArea.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.bak_curveArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bak_curveArea.isRealDataCurve = false;
            this.bak_curveArea.Location = new System.Drawing.Point(0, 455);
            this.bak_curveArea.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bak_curveArea.Name = "bak_curveArea";
            this.bak_curveArea.Size = new System.Drawing.Size(855, 151);
            this.bak_curveArea.TabIndex = 3;
            this.bak_curveArea.TimeLineVisible = false;
            this.bak_curveArea.UserSelected = false;
            this.bak_curveArea.Visible = false;
            // 
            // master_CurveArea
            // 
            this.master_CurveArea.BaseTimeLineSpan = 30;
            this.master_CurveArea.Dock = System.Windows.Forms.DockStyle.Top;
            this.master_CurveArea.isRealDataCurve = false;
            this.master_CurveArea.Location = new System.Drawing.Point(0, 0);
            this.master_CurveArea.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.master_CurveArea.Name = "master_CurveArea";
            this.master_CurveArea.Size = new System.Drawing.Size(855, 455);
            this.master_CurveArea.TabIndex = 2;
            this.master_CurveArea.TimeLineVisible = false;
            this.master_CurveArea.UserSelected = false;
            // 
            // ChannelSetting
            // 
            this.ChannelSetting.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ChannelSetting.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HideChannel,
            this.HideWave});
            this.ChannelSetting.Name = "ChannelSetting";
            this.ChannelSetting.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.ChannelSetting.ShowImageMargin = false;
            this.ChannelSetting.Size = new System.Drawing.Size(112, 48);
            // 
            // HideChannel
            // 
            this.HideChannel.Name = "HideChannel";
            this.HideChannel.Size = new System.Drawing.Size(111, 22);
            this.HideChannel.Text = "通道不可见";
            // 
            // HideWave
            // 
            this.HideWave.Name = "HideWave";
            this.HideWave.Size = new System.Drawing.Size(111, 22);
            this.HideWave.Text = "波形不可见";
            // 
            // Chart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.DoubleBuffered = true;
            this.Name = "Chart";
            this.Size = new System.Drawing.Size(1024, 635);
            this.panel6.ResumeLayout(false);
            this.ChannelSetting.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel6;
        private curveArea master_CurveArea;
        private curveArea bak_curveArea;
        private System.Windows.Forms.ContextMenuStrip ChannelSetting;
        private System.Windows.Forms.ToolStripMenuItem HideChannel;
        private System.Windows.Forms.ToolStripMenuItem HideWave;
    }
}
