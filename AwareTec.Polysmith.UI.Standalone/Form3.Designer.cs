namespace AwareTec.Polysmith.UI
{
    partial class Form3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            this.buttonEx1 = new System.Windows.Forms.ButtonEx();
            this.buttonEx2 = new System.Windows.Forms.ButtonEx();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxEx1 = new System.Windows.Forms.TextBoxEx();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxEx2 = new System.Windows.Forms.TextBoxEx();
            this.textBoxEx3 = new System.Windows.Forms.TextBoxEx();
            this.textBoxEx4 = new System.Windows.Forms.TextBoxEx();
            this.SuspendLayout();
            // 
            // buttonEx1
            // 
            this.buttonEx1.Location = new System.Drawing.Point(55, 57);
            this.buttonEx1.Name = "buttonEx1";
            this.buttonEx1.Size = new System.Drawing.Size(75, 23);
            this.buttonEx1.TabIndex = 4;
            this.buttonEx1.Text = "readLine";
            this.buttonEx1.UseVisualStyleBackColor = true;
            this.buttonEx1.Click += new System.EventHandler(this.buttonEx1_Click);
            // 
            // buttonEx2
            // 
            this.buttonEx2.Location = new System.Drawing.Point(55, 268);
            this.buttonEx2.Name = "buttonEx2";
            this.buttonEx2.Size = new System.Drawing.Size(75, 23);
            this.buttonEx2.TabIndex = 4;
            this.buttonEx2.Text = "readBytes";
            this.buttonEx2.UseVisualStyleBackColor = true;
            this.buttonEx2.Click += new System.EventHandler(this.buttonEx2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(208, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "耗时";
            // 
            // textBoxEx1
            // 
            this.textBoxEx1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(208)))), ((int)(((byte)(232)))));
            this.textBoxEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(208)))), ((int)(((byte)(232)))));
            this.textBoxEx1.Location = new System.Drawing.Point(260, 59);
            this.textBoxEx1.Name = "textBoxEx1";
            this.textBoxEx1.Size = new System.Drawing.Size(100, 21);
            this.textBoxEx1.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(208, 274);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "耗时";
            // 
            // textBoxEx2
            // 
            this.textBoxEx2.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(208)))), ((int)(((byte)(232)))));
            this.textBoxEx2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(208)))), ((int)(((byte)(232)))));
            this.textBoxEx2.Location = new System.Drawing.Point(260, 271);
            this.textBoxEx2.Name = "textBoxEx2";
            this.textBoxEx2.Size = new System.Drawing.Size(100, 21);
            this.textBoxEx2.TabIndex = 6;
            // 
            // textBoxEx3
            // 
            this.textBoxEx3.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(208)))), ((int)(((byte)(232)))));
            this.textBoxEx3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(208)))), ((int)(((byte)(232)))));
            this.textBoxEx3.Location = new System.Drawing.Point(64, 96);
            this.textBoxEx3.Multiline = true;
            this.textBoxEx3.Name = "textBoxEx3";
            this.textBoxEx3.Size = new System.Drawing.Size(715, 156);
            this.textBoxEx3.TabIndex = 6;
            // 
            // textBoxEx4
            // 
            this.textBoxEx4.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(208)))), ((int)(((byte)(232)))));
            this.textBoxEx4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(208)))), ((int)(((byte)(232)))));
            this.textBoxEx4.Location = new System.Drawing.Point(64, 311);
            this.textBoxEx4.Multiline = true;
            this.textBoxEx4.Name = "textBoxEx4";
            this.textBoxEx4.Size = new System.Drawing.Size(715, 156);
            this.textBoxEx4.TabIndex = 6;
            // 
            // Form3
            // 
            this.AllowEndUserDocking = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 513);
            this.Controls.Add(this.textBoxEx2);
            this.Controls.Add(this.textBoxEx4);
            this.Controls.Add(this.textBoxEx3);
            this.Controls.Add(this.textBoxEx1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonEx2);
            this.Controls.Add(this.buttonEx1);
            this.DockAreas = WinFormsUI.Docking.DockAreas.Document;
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form3";
            this.Text = "Form3";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ButtonEx buttonEx1;
        private System.Windows.Forms.ButtonEx buttonEx2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBoxEx textBoxEx1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBoxEx textBoxEx2;
        private System.Windows.Forms.TextBoxEx textBoxEx3;
        private System.Windows.Forms.TextBoxEx textBoxEx4;
    }
}