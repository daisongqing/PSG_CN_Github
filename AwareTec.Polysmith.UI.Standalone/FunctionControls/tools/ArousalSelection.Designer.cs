namespace AwareTec.Polysmith.UI.FunctionControls.tools
{
    partial class ArousalSelection
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CbBrain = new pSystem.UI.ReaLTaiizor.Controls.AloneComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Location = new System.Drawing.Point(61, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "导联信号源选择";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label2.Location = new System.Drawing.Point(10, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "脑电";
            // 
            // CbBrain
            // 
            this.CbBrain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CbBrain.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CbBrain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbBrain.EnabledCalc = true;
            this.CbBrain.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CbBrain.FormattingEnabled = true;
            this.CbBrain.ItemHeight = 20;
            this.CbBrain.Location = new System.Drawing.Point(85, 45);
            this.CbBrain.Name = "CbBrain";
            this.CbBrain.Size = new System.Drawing.Size(134, 26);
            this.CbBrain.TabIndex = 39;
            this.CbBrain.Tag = "1\\4\\0\\3\\2\\5";
            this.CbBrain.SelectedIndexChanged += new System.EventHandler(this.CbBrain_SelectedIndexChanged);
            // 
            // ArousalSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.CbBrain);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ArousalSelection";
            this.Size = new System.Drawing.Size(231, 93);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private pSystem.UI.ReaLTaiizor.Controls.AloneComboBox CbBrain;
    }
}
