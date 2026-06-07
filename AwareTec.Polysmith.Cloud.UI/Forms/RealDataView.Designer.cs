
using System.Drawing;

namespace AwareTec.Polysmith.Cloud.UI.Forms
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ShowInformationLabel = new System.Windows.Forms.Label();
            this.ChartArea = new pChart.Chart(Color.FromArgb(239, 255, 253), Color.FromArgb(239, 255, 253));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SetTimeButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.DingBiaoButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.ZkTestButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.PasueButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.StartRunButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.QuitButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.HeadClientContent = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // ShowInformationLabel
            // 
            this.ShowInformationLabel.AutoSize = true;
            this.ShowInformationLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.ShowInformationLabel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowInformationLabel.ForeColor = System.Drawing.Color.DarkSeaGreen;
            this.ShowInformationLabel.Location = new System.Drawing.Point(80, 3);
            this.ShowInformationLabel.Name = "ShowInformationLabel";
            this.ShowInformationLabel.Size = new System.Drawing.Size(0, 20);
            this.ShowInformationLabel.TabIndex = 6;
            // 
            // ChartArea
            // 
            this.ChartArea.BaseTimeLineSpan = 30;
            this.ChartArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChartArea.FnStop = true;
            this.ChartArea.isRealDataCurve = true;
            this.ChartArea.Location = new System.Drawing.Point(0, 0);
            this.ChartArea.Margin = new System.Windows.Forms.Padding(4);
            this.ChartArea.Name = "ChartArea";
            this.ChartArea.OtherBaseTimeLineSpan = 30;
            this.ChartArea.Size = new System.Drawing.Size(775, 540);
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
            this.SetTimeButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SetTimeButton.ForeColor = System.Drawing.Color.White;
            this.SetTimeButton.Image = null;
            this.SetTimeButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SetTimeButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.SetTimeButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.SetTimeButton.Location = new System.Drawing.Point(238, 0);
            this.SetTimeButton.Margin = new System.Windows.Forms.Padding(2);
            this.SetTimeButton.Name = "SetTimeButton";
            this.SetTimeButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.SetTimeButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.SetTimeButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.SetTimeButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.SetTimeButton.Size = new System.Drawing.Size(74, 28);
            this.SetTimeButton.TabIndex = 74;
            this.SetTimeButton.Text = "定时设置";
            this.SetTimeButton.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // DingBiaoButton
            // 
            this.DingBiaoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DingBiaoButton.BackColor = System.Drawing.Color.Transparent;
            this.DingBiaoButton.BorderColor = System.Drawing.Color.Transparent;
            this.DingBiaoButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DingBiaoButton.Enabled = false;
            this.DingBiaoButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DingBiaoButton.ForeColor = System.Drawing.Color.White;
            this.DingBiaoButton.Image = null;
            this.DingBiaoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DingBiaoButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.DingBiaoButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.DingBiaoButton.Location = new System.Drawing.Point(339, 0);
            this.DingBiaoButton.Margin = new System.Windows.Forms.Padding(2);
            this.DingBiaoButton.Name = "DingBiaoButton";
            this.DingBiaoButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.DingBiaoButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.DingBiaoButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.DingBiaoButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.DingBiaoButton.Size = new System.Drawing.Size(74, 28);
            this.DingBiaoButton.TabIndex = 75;
            this.DingBiaoButton.Text = "定 标";
            this.DingBiaoButton.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // ZkTestButton
            // 
            this.ZkTestButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ZkTestButton.BackColor = System.Drawing.Color.Transparent;
            this.ZkTestButton.BorderColor = System.Drawing.Color.Transparent;
            this.ZkTestButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ZkTestButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ZkTestButton.ForeColor = System.Drawing.Color.White;
            this.ZkTestButton.Image = null;
            this.ZkTestButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ZkTestButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.ZkTestButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.ZkTestButton.Location = new System.Drawing.Point(434, 0);
            this.ZkTestButton.Margin = new System.Windows.Forms.Padding(2);
            this.ZkTestButton.Name = "ZkTestButton";
            this.ZkTestButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.ZkTestButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.ZkTestButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.ZkTestButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.ZkTestButton.Size = new System.Drawing.Size(74, 28);
            this.ZkTestButton.TabIndex = 76;
            this.ZkTestButton.Text = "阻抗测试";
            this.ZkTestButton.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // PasueButton
            // 
            this.PasueButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PasueButton.BackColor = System.Drawing.Color.Transparent;
            this.PasueButton.BorderColor = System.Drawing.Color.Transparent;
            this.PasueButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PasueButton.Enabled = false;
            this.PasueButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PasueButton.ForeColor = System.Drawing.Color.White;
            this.PasueButton.Image = null;
            this.PasueButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PasueButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.PasueButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.PasueButton.Location = new System.Drawing.Point(540, 0);
            this.PasueButton.Margin = new System.Windows.Forms.Padding(2);
            this.PasueButton.Name = "PasueButton";
            this.PasueButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.PasueButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.PasueButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.PasueButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.PasueButton.Size = new System.Drawing.Size(74, 28);
            this.PasueButton.TabIndex = 77;
            this.PasueButton.Text = "暂 停";
            this.PasueButton.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // StartRunButton
            // 
            this.StartRunButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StartRunButton.BackColor = System.Drawing.Color.Transparent;
            this.StartRunButton.BorderColor = System.Drawing.Color.Transparent;
            this.StartRunButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StartRunButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StartRunButton.ForeColor = System.Drawing.Color.White;
            this.StartRunButton.Image = null;
            this.StartRunButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StartRunButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.StartRunButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.StartRunButton.Location = new System.Drawing.Point(629, 0);
            this.StartRunButton.Margin = new System.Windows.Forms.Padding(2);
            this.StartRunButton.Name = "StartRunButton";
            this.StartRunButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.StartRunButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.StartRunButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.StartRunButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.StartRunButton.Size = new System.Drawing.Size(74, 28);
            this.StartRunButton.TabIndex = 78;
            this.StartRunButton.Text = "开始采集";
            this.StartRunButton.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // QuitButton
            // 
            this.QuitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.QuitButton.BackColor = System.Drawing.Color.Transparent;
            this.QuitButton.BorderColor = System.Drawing.Color.Transparent;
            this.QuitButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.QuitButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.QuitButton.ForeColor = System.Drawing.Color.White;
            this.QuitButton.Image = null;
            this.QuitButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.QuitButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.QuitButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.QuitButton.Location = new System.Drawing.Point(730, 0);
            this.QuitButton.Margin = new System.Windows.Forms.Padding(2);
            this.QuitButton.Name = "QuitButton";
            this.QuitButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.QuitButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.QuitButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.QuitButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.QuitButton.Size = new System.Drawing.Size(43, 28);
            this.QuitButton.TabIndex = 79;
            this.QuitButton.Text = "X";
            this.QuitButton.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // HeadClientContent
            // 
            this.HeadClientContent.Name = "HeadClientContent";
            this.HeadClientContent.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // RealDataView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.QuitButton);
            this.Controls.Add(this.StartRunButton);
            this.Controls.Add(this.PasueButton);
            this.Controls.Add(this.ZkTestButton);
            this.Controls.Add(this.DingBiaoButton);
            this.Controls.Add(this.SetTimeButton);
            this.Controls.Add(this.ShowInformationLabel);
            this.Controls.Add(this.ChartArea);
            this.Name = "RealDataView";
            this.Size = new System.Drawing.Size(775, 540);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public pChart.Chart ChartArea;
        private System.Windows.Forms.ContextMenuStrip HeadClientContent;
        private System.Windows.Forms.Label ShowInformationLabel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Timer timer1;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft SetTimeButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft DingBiaoButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft ZkTestButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft PasueButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft StartRunButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft QuitButton;

    }
}
