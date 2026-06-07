using System.Drawing;
namespace AwareTec.Polysmith.UI.FunctionControls.tools
{
    partial class MessageForm
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
            this.AttentionIconPictureBox = new System.Windows.Forms.PictureBox();
            this.AttentionTipsLabel = new System.Windows.Forms.Label();
            this.CancelButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.ConfirmButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            ((System.ComponentModel.ISupportInitialize)(this.AttentionIconPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // AttentionIconPictureBox
            // 
            this.AttentionIconPictureBox.BackgroundImage = global::AwareTec.Polysmith.UI.Properties.Resources.attention;
            this.AttentionIconPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AttentionIconPictureBox.Location = new System.Drawing.Point(44, 44);
            this.AttentionIconPictureBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.AttentionIconPictureBox.Name = "AttentionIconPictureBox";
            this.AttentionIconPictureBox.Size = new System.Drawing.Size(30, 32);
            this.AttentionIconPictureBox.TabIndex = 0;
            this.AttentionIconPictureBox.TabStop = false;
            // 
            // AttentionTipsLabel
            // 
            this.AttentionTipsLabel.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.AttentionTipsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(49)))), ((int)(((byte)(51)))));
            this.AttentionTipsLabel.Location = new System.Drawing.Point(91, 50);
            this.AttentionTipsLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.AttentionTipsLabel.Name = "AttentionTipsLabel";
            this.AttentionTipsLabel.Size = new System.Drawing.Size(87, 26);
            this.AttentionTipsLabel.TabIndex = 1;
            this.AttentionTipsLabel.Text = "这里是提示";
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
            this.CancelButton.Location = new System.Drawing.Point(31, 105);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.PressedColorA = System.Drawing.Color.White;
            this.CancelButton.PressedColorB = System.Drawing.Color.White;
            this.CancelButton.PressedContourColorA = System.Drawing.Color.White;
            this.CancelButton.PressedContourColorB = System.Drawing.Color.White;
            this.CancelButton.Size = new System.Drawing.Size(62, 30);
            this.CancelButton.TabIndex = 33;
            this.CancelButton.Text = "取 消";
            this.CancelButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ConfirmButton
            // 
            this.ConfirmButton.BackColor = System.Drawing.Color.Transparent;
            this.ConfirmButton.BorderColor = System.Drawing.Color.Transparent;
            this.ConfirmButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ConfirmButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ConfirmButton.ForeColor = System.Drawing.Color.White;
            this.ConfirmButton.Image = null;
            this.ConfirmButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ConfirmButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.ConfirmButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.ConfirmButton.Location = new System.Drawing.Point(141, 105);
            this.ConfirmButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ConfirmButton.Name = "ConfirmButton";
            this.ConfirmButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.ConfirmButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.ConfirmButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.ConfirmButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.ConfirmButton.Size = new System.Drawing.Size(62, 30);
            this.ConfirmButton.TabIndex = 42;
            this.ConfirmButton.Text = "确 定";
            this.ConfirmButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.ConfirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // MessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(212, 202);
            this.ControlBox = false;
            this.Controls.Add(this.ConfirmButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.AttentionTipsLabel);
            this.Controls.Add(this.AttentionIconPictureBox);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MessageForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MessageForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.AttentionIconPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox AttentionIconPictureBox;
        private System.Windows.Forms.Label AttentionTipsLabel;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft CancelButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft ConfirmButton;
    }
}