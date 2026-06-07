namespace AwareTec.Polysmith.Cloud.UI.Forms.Tips
{
    partial class NetworkIsUnavailableForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetworkIsUnavailableForm));
            this.UnavailableNetworkPanel = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.NoNetworkPictureBox = new System.Windows.Forms.PictureBox();
            this.UnavailableNetworkPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NoNetworkPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // UnavailableNetworkPanel
            // 
            this.UnavailableNetworkPanel.Controls.Add(this.label7);
            this.UnavailableNetworkPanel.Controls.Add(this.label6);
            this.UnavailableNetworkPanel.Controls.Add(this.NoNetworkPictureBox);
            this.UnavailableNetworkPanel.Location = new System.Drawing.Point(31, 47);
            this.UnavailableNetworkPanel.Name = "UnavailableNetworkPanel";
            this.UnavailableNetworkPanel.Size = new System.Drawing.Size(557, 349);
            this.UnavailableNetworkPanel.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(233, 257);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 24);
            this.label7.TabIndex = 2;
            this.label7.Text = "请联网重试";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(153, 233);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(262, 24);
            this.label6.TabIndex = 1;
            this.label6.Text = "当前无网络，暂未能连接服务器";
            // 
            // NoNetworkPictureBox
            // 
            this.NoNetworkPictureBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("NoNetworkPictureBox.BackgroundImage")));
            this.NoNetworkPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.NoNetworkPictureBox.Location = new System.Drawing.Point(144, 53);
            this.NoNetworkPictureBox.Name = "NoNetworkPictureBox";
            this.NoNetworkPictureBox.Size = new System.Drawing.Size(289, 174);
            this.NoNetworkPictureBox.TabIndex = 0;
            this.NoNetworkPictureBox.TabStop = false;
            // 
            // NetworkIsUnavailableForm
            // 
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 411);
            this.Controls.Add(this.UnavailableNetworkPanel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NetworkIsUnavailableForm";
            this.ShowIcon = false;
            this.Text = "当前无网络";
            this.UnavailableNetworkPanel.ResumeLayout(false);
            this.UnavailableNetworkPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NoNetworkPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel UnavailableNetworkPanel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox NoNetworkPictureBox;
    }
}