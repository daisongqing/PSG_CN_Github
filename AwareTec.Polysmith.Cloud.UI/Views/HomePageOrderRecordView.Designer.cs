namespace AwareTec.Polysmith.Cloud.UI.Views
{
    partial class HomePageOrderRecordView
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
            this.OrderRecordSearchView = new AwareTec.Polysmith.Cloud.UI.Views.OrderRecordSearchView();
            this.OrderRecordView = new AwareTec.Polysmith.Cloud.UI.Views.OrderRecordView();
            this.SuspendLayout();
            // 
            // OrderRecordSearchView
            // 
            this.OrderRecordSearchView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(255)))), ((int)(((byte)(253)))));
            this.OrderRecordSearchView.Dock = System.Windows.Forms.DockStyle.Top;
            this.OrderRecordSearchView.Location = new System.Drawing.Point(0, 0);
            this.OrderRecordSearchView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OrderRecordSearchView.Name = "OrderRecordSearchView";
            this.OrderRecordSearchView.Size = new System.Drawing.Size(1800, 77);
            this.OrderRecordSearchView.TabIndex = 0;
            this.OrderRecordSearchView.QueryOrderByFilter += new System.EventHandler<AwareTec.Polysmith.Cloud.UI.Views.ViewsEventArgs.QueryOrderByFilterEventArgs>(this.OrderRecordSearchView_QueryOrderByFilter);
            // 
            // OrderRecordView
            // 
            this.OrderRecordView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OrderRecordView.Location = new System.Drawing.Point(0, 77);
            this.OrderRecordView.Name = "OrderRecordView";
            this.OrderRecordView.Size = new System.Drawing.Size(1800, 540);
            this.OrderRecordView.TabIndex = 2;
            this.OrderRecordView.StartMonitoringBeClicked += new System.EventHandler<AwareTec.Polysmith.Cloud.UI.Views.ViewsEventArgs.SwitchPageEventArgs>(this.OrderRecordView_StartMonitoringBeClicked);
            this.OrderRecordView.EnterPlayBeClicked += new System.EventHandler<AwareTec.Polysmith.Cloud.UI.Views.ViewsEventArgs.SwitchPageEventArgs>(this.OrderRecordView_EnterPlayBeClicked);
            // 
            // HomePageOrderRecordView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.OrderRecordView);
            this.Controls.Add(this.OrderRecordSearchView);
            this.Name = "HomePageOrderRecordView";
            this.Size = new System.Drawing.Size(1800, 617);
            this.ResumeLayout(false);

        }

        #endregion

        private OrderRecordSearchView OrderRecordSearchView;
        private OrderRecordView OrderRecordView;
    }
}
