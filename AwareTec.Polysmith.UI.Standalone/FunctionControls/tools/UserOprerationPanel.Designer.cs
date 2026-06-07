namespace AwareTec.Polysmith.UI.FunctionControls.tools
{
    partial class UserOprerationPanel
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
            this.btnDown = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnAuto = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(75, 72);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(60, 24);
            this.btnDown.TabIndex = 51;
            this.btnDown.Tag = "1";
            this.btnDown.Text = "↓";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLeft_MouseDown);
            this.btnDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnLeft_MouseUp);
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(142, 41);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(52, 27);
            this.btnRight.TabIndex = 50;
            this.btnRight.Tag = "3";
            this.btnRight.Text = "→";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLeft_MouseDown);
            this.btnRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnLeft_MouseUp);
            // 
            // btnAuto
            // 
            this.btnAuto.Location = new System.Drawing.Point(75, 40);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(60, 27);
            this.btnAuto.TabIndex = 49;
            this.btnAuto.Text = "O";
            this.btnAuto.UseVisualStyleBackColor = true;
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(8, 40);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(60, 27);
            this.btnLeft.TabIndex = 48;
            this.btnLeft.Tag = "2";
            this.btnLeft.Text = "←";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLeft_MouseDown);
            this.btnLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnLeft_MouseUp);
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(75, 8);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(60, 26);
            this.btnUp.TabIndex = 47;
            this.btnUp.Tag = "0";
            this.btnUp.Text = "↑";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLeft_MouseDown);
            this.btnUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnLeft_MouseUp);
            // 
            // UserOprerationPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnAuto);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.btnUp);
            this.Name = "UserOprerationPanel";
            this.Size = new System.Drawing.Size(202, 118);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnAuto;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnUp;
    }
}
