
namespace AwareTec.Polysmith.Cloud.UI.Forms.RealDataViewBlock
{
    partial class Calibration
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.abSelect = new System.Windows.Forms.DataGridViewEx();
            this.index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewLinkAndImageColumn();
            this.EndButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.StartButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.FailButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.PassButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.CancelButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.SkipButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.abSelect)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.abSelect);
            this.panel1.Location = new System.Drawing.Point(3, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(517, 561);
            this.panel1.TabIndex = 1;
            // 
            // abSelect
            // 
            this.abSelect.AllowUserToAddRows = false;
            this.abSelect.AllowUserToDeleteRows = false;
            this.abSelect.AllowUserToOrderColumns = true;
            this.abSelect.AllowUserToResizeColumns = false;
            this.abSelect.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            this.abSelect.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.abSelect.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(255)))), ((int)(((byte)(253)))));
            this.abSelect.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.abSelect.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.abSelect.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.abSelect.ColumnHeadersHeight = 40;
            this.abSelect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.abSelect.ColumnHeadersVisible = false;
            this.abSelect.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.index,
            this.Description});
            this.abSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.abSelect.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.abSelect.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.abSelect.HeaderColLinearBegin = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.abSelect.HeaderColLinearEnd = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.abSelect.HeaderRowLinearBegin = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(235)))), ((int)(((byte)(255)))));
            this.abSelect.HeaderRowLinearEnd = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(235)))), ((int)(((byte)(255)))));
            this.abSelect.Location = new System.Drawing.Point(0, 0);
            this.abSelect.MultiSelect = false;
            this.abSelect.Name = "abSelect";
            this.abSelect.RowHeadersVisible = false;
            this.abSelect.RowIndexOffSet = 0;
            this.abSelect.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.abSelect.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.abSelect.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.abSelect.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.abSelect.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.abSelect.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(246)))), ((int)(((byte)(235)))));
            this.abSelect.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.abSelect.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.abSelect.RowTemplate.Height = 35;
            this.abSelect.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.abSelect.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.abSelect.Size = new System.Drawing.Size(517, 561);
            this.abSelect.TabIndex = 3;
            // 
            // index
            // 
            this.index.DataPropertyName = "XH";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.index.DefaultCellStyle = dataGridViewCellStyle3;
            this.index.FillWeight = 20F;
            this.index.HeaderText = "序号";
            this.index.MinimumWidth = 70;
            this.index.Name = "index";
            this.index.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.index.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.index.Width = 70;
            // 
            // Description
            // 
            this.Description.ActiveLinkColor = System.Drawing.Color.Black;
            this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Description.DataPropertyName = "Description";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Description.DefaultCellStyle = dataGridViewCellStyle4;
            this.Description.FillWeight = 30F;
            this.Description.HeaderText = "检测";
            this.Description.Image = null;
            this.Description.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.Description.LinkColor = System.Drawing.Color.Black;
            this.Description.MinimumWidth = 100;
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Description.VisitedLinkColor = System.Drawing.Color.Black;
            // 
            // EndButton
            // 
            this.EndButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.EndButton.BackColor = System.Drawing.Color.Transparent;
            this.EndButton.BorderColor = System.Drawing.Color.Transparent;
            this.EndButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EndButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.EndButton.ForeColor = System.Drawing.Color.White;
            this.EndButton.Image = null;
            this.EndButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.EndButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.EndButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.EndButton.Location = new System.Drawing.Point(376, 614);
            this.EndButton.Name = "EndButton";
            this.EndButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.EndButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.EndButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.EndButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.EndButton.Size = new System.Drawing.Size(116, 32);
            this.EndButton.TabIndex = 46;
            this.EndButton.Text = "完 成";
            this.EndButton.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // StartButton
            // 
            this.StartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StartButton.BackColor = System.Drawing.Color.Transparent;
            this.StartButton.BorderColor = System.Drawing.Color.Transparent;
            this.StartButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StartButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StartButton.ForeColor = System.Drawing.Color.White;
            this.StartButton.Image = null;
            this.StartButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StartButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.StartButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.StartButton.Location = new System.Drawing.Point(540, 53);
            this.StartButton.Name = "StartButton";
            this.StartButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.StartButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.StartButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.StartButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.StartButton.Size = new System.Drawing.Size(116, 32);
            this.StartButton.TabIndex = 42;
            this.StartButton.Text = "开 始";
            this.StartButton.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // FailButton
            // 
            this.FailButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FailButton.BackColor = System.Drawing.Color.Transparent;
            this.FailButton.BorderColor = System.Drawing.Color.Transparent;
            this.FailButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FailButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FailButton.ForeColor = System.Drawing.Color.White;
            this.FailButton.Image = null;
            this.FailButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.FailButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.FailButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.FailButton.Location = new System.Drawing.Point(540, 148);
            this.FailButton.Name = "FailButton";
            this.FailButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.FailButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.FailButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.FailButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.FailButton.Size = new System.Drawing.Size(116, 32);
            this.FailButton.TabIndex = 43;
            this.FailButton.Text = "失 败";
            this.FailButton.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // PassButton
            // 
            this.PassButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PassButton.BackColor = System.Drawing.Color.Transparent;
            this.PassButton.BorderColor = System.Drawing.Color.Transparent;
            this.PassButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PassButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PassButton.ForeColor = System.Drawing.Color.White;
            this.PassButton.Image = null;
            this.PassButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PassButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.PassButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(171)))));
            this.PassButton.Location = new System.Drawing.Point(541, 100);
            this.PassButton.Name = "PassButton";
            this.PassButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.PassButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.PassButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.PassButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(214)))), ((int)(((byte)(187)))));
            this.PassButton.Size = new System.Drawing.Size(116, 32);
            this.PassButton.TabIndex = 44;
            this.PassButton.Text = "通 过";
            this.PassButton.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.BackColor = System.Drawing.Color.Transparent;
            this.CancelButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.CancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CancelButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CancelButton.Image = null;
            this.CancelButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CancelButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.CancelButton.InactiveColorB = System.Drawing.Color.White;
            this.CancelButton.Location = new System.Drawing.Point(541, 614);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.PressedColorA = System.Drawing.Color.White;
            this.CancelButton.PressedColorB = System.Drawing.Color.White;
            this.CancelButton.PressedContourColorA = System.Drawing.Color.White;
            this.CancelButton.PressedContourColorB = System.Drawing.Color.White;
            this.CancelButton.Size = new System.Drawing.Size(115, 32);
            this.CancelButton.TabIndex = 47;
            this.CancelButton.Text = "取 消";
            this.CancelButton.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // SkipButton
            // 
            this.SkipButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SkipButton.BackColor = System.Drawing.Color.Transparent;
            this.SkipButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.SkipButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SkipButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SkipButton.Image = null;
            this.SkipButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SkipButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.SkipButton.InactiveColorB = System.Drawing.Color.White;
            this.SkipButton.Location = new System.Drawing.Point(540, 305);
            this.SkipButton.Name = "SkipButton";
            this.SkipButton.PressedColorA = System.Drawing.Color.White;
            this.SkipButton.PressedColorB = System.Drawing.Color.White;
            this.SkipButton.PressedContourColorA = System.Drawing.Color.White;
            this.SkipButton.PressedContourColorB = System.Drawing.Color.White;
            this.SkipButton.Size = new System.Drawing.Size(115, 32);
            this.SkipButton.TabIndex = 49;
            this.SkipButton.Text = "跳 过";
            this.SkipButton.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // Calibration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CanResize = false;
            this.ClientSize = new System.Drawing.Size(675, 660);
            this.ControlBox = false;
            this.Controls.Add(this.SkipButton);
            this.Controls.Add(this.EndButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.PassButton);
            this.Controls.Add(this.FailButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Calibration";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "病人定标";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.abSelect)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewEx abSelect;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft EndButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft StartButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft FailButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft PassButton;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft CancelButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn index;
        private System.Windows.Forms.DataGridViewLinkAndImageColumn Description;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft SkipButton;
    }
}