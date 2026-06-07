namespace AwareTec.Polysmith.UI.FunctionControls.tools
{
    partial class Spo2RangeSelection
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
            this.Spo2DropCombBox = new pSystem.UI.ReaLTaiizor.Controls.AloneComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Location = new System.Drawing.Point(64, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "下降阈值设定";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(8, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "下降百分比";
            // 
            // Spo2DropCombBox
            // 
            this.Spo2DropCombBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Spo2DropCombBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Spo2DropCombBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Spo2DropCombBox.EnabledCalc = true;
            this.Spo2DropCombBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Spo2DropCombBox.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Spo2DropCombBox.FormattingEnabled = true;
            this.Spo2DropCombBox.ItemHeight = 20;
            this.Spo2DropCombBox.Items.AddRange(new object[] {
            "2 %",
            "3 %",
            "4 %",
            "5 %"});
            this.Spo2DropCombBox.Location = new System.Drawing.Point(110, 49);
            this.Spo2DropCombBox.Name = "Spo2DropCombBox";
            this.Spo2DropCombBox.Size = new System.Drawing.Size(71, 26);
            this.Spo2DropCombBox.TabIndex = 70;
            // 
            // Spo2RangeSelection
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.Spo2DropCombBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Spo2RangeSelection";
            this.Size = new System.Drawing.Size(211, 101);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private pSystem.UI.ReaLTaiizor.Controls.AloneComboBox Spo2DropCombBox;
    }
}
