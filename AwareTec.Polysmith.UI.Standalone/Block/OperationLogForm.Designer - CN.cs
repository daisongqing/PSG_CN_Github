
namespace AwareTec.Polysmith.UI.Block
{
    partial class OperationLogForm
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
            this.OperationLogTab = new pSystem.UI.ReaLTaiizor.Controls.AirTabPage();
            this.ObverseLogTabPage = new System.Windows.Forms.TabPage();
            this.ChooseDateTabPage = new System.Windows.Forms.TabPage();
            this.HistoryLogPanel = new System.Windows.Forms.Panel();
            this.ButtonPanel = new System.Windows.Forms.Panel();
            this.ReturnSearchButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.ObserveLogButton = new pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft();
            this.TipsPanel = new System.Windows.Forms.Panel();
            this.TipsNotification = new pSystem.UI.ReaLTaiizor.Controls.FoxNotification();
            this.SelectDatePanel = new System.Windows.Forms.Panel();
            this.SelectDate = new pSystem.UI.ReaLTaiizor.Controls.HopeDatePicker();
            this.SearchResultPanel = new System.Windows.Forms.Panel();
            this.LogIsNothing = new pSystem.UI.ReaLTaiizor.Controls.FoxNotification();
            this.OperationLogTab.SuspendLayout();
            this.ChooseDateTabPage.SuspendLayout();
            this.ButtonPanel.SuspendLayout();
            this.TipsPanel.SuspendLayout();
            this.SelectDatePanel.SuspendLayout();
            this.SearchResultPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // OperationLogTab
            // 
            this.OperationLogTab.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.OperationLogTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OperationLogTab.Controls.Add(this.ObverseLogTabPage);
            this.OperationLogTab.Controls.Add(this.ChooseDateTabPage);
            this.OperationLogTab.ItemSize = new System.Drawing.Size(30, 115);
            this.OperationLogTab.Location = new System.Drawing.Point(6, 54);
            this.OperationLogTab.Multiline = true;
            this.OperationLogTab.Name = "OperationLogTab";
            this.OperationLogTab.Padding = new System.Drawing.Point(6, 7);
            this.OperationLogTab.SelectedIndex = 0;
            this.OperationLogTab.ShowOuterBorders = false;
            this.OperationLogTab.Size = new System.Drawing.Size(1333, 743);
            this.OperationLogTab.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.OperationLogTab.SquareColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(235)))), ((int)(((byte)(255)))));
            this.OperationLogTab.TabIndex = 0;
            // 
            // ObverseLogTabPage
            // 
            this.ObverseLogTabPage.BackColor = System.Drawing.Color.White;
            this.ObverseLogTabPage.Location = new System.Drawing.Point(119, 4);
            this.ObverseLogTabPage.Name = "ObverseLogTabPage";
            this.ObverseLogTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ObverseLogTabPage.Size = new System.Drawing.Size(1210, 735);
            this.ObverseLogTabPage.TabIndex = 1;
            this.ObverseLogTabPage.Text = "当前日志";
            // 
            // ChooseDateTabPage
            // 
            this.ChooseDateTabPage.BackColor = System.Drawing.Color.White;
            this.ChooseDateTabPage.Controls.Add(this.HistoryLogPanel);
            this.ChooseDateTabPage.Controls.Add(this.ButtonPanel);
            this.ChooseDateTabPage.Controls.Add(this.TipsPanel);
            this.ChooseDateTabPage.Controls.Add(this.SelectDatePanel);
            this.ChooseDateTabPage.Controls.Add(this.SearchResultPanel);
            this.ChooseDateTabPage.Location = new System.Drawing.Point(119, 4);
            this.ChooseDateTabPage.Name = "ChooseDateTabPage";
            this.ChooseDateTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ChooseDateTabPage.Size = new System.Drawing.Size(1210, 735);
            this.ChooseDateTabPage.TabIndex = 0;
            this.ChooseDateTabPage.Text = "历史日志";
            // 
            // HistoryLogPanel
            // 
            this.HistoryLogPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HistoryLogPanel.Location = new System.Drawing.Point(0, 0);
            this.HistoryLogPanel.Name = "HistoryLogPanel";
            this.HistoryLogPanel.Size = new System.Drawing.Size(1210, 646);
            this.HistoryLogPanel.TabIndex = 6;
            this.HistoryLogPanel.Visible = false;
            // 
            // ButtonPanel
            // 
            this.ButtonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonPanel.Controls.Add(this.ReturnSearchButton);
            this.ButtonPanel.Controls.Add(this.ObserveLogButton);
            this.ButtonPanel.Location = new System.Drawing.Point(3, 652);
            this.ButtonPanel.Name = "ButtonPanel";
            this.ButtonPanel.Size = new System.Drawing.Size(1207, 83);
            this.ButtonPanel.TabIndex = 62;
            // 
            // ReturnSearchButton
            // 
            this.ReturnSearchButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.ReturnSearchButton.BackColor = System.Drawing.Color.Transparent;
            this.ReturnSearchButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.ReturnSearchButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ReturnSearchButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ReturnSearchButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.ReturnSearchButton.Image = null;
            this.ReturnSearchButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ReturnSearchButton.InactiveColorA = System.Drawing.Color.White;
            this.ReturnSearchButton.InactiveColorB = System.Drawing.Color.White;
            this.ReturnSearchButton.Location = new System.Drawing.Point(1028, 20);
            this.ReturnSearchButton.Name = "ReturnSearchButton";
            this.ReturnSearchButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.ReturnSearchButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.ReturnSearchButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.ReturnSearchButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.ReturnSearchButton.Size = new System.Drawing.Size(126, 41);
            this.ReturnSearchButton.TabIndex = 59;
            this.ReturnSearchButton.Text = "返回查询";
            this.ReturnSearchButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.ReturnSearchButton.Visible = false;
            this.ReturnSearchButton.Click += new System.EventHandler(this.ReturnSearchButton_Click);
            // 
            // ObserveLogButton
            // 
            this.ObserveLogButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ObserveLogButton.BackColor = System.Drawing.Color.Transparent;
            this.ObserveLogButton.BorderColor = System.Drawing.Color.Transparent;
            this.ObserveLogButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ObserveLogButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ObserveLogButton.ForeColor = System.Drawing.Color.White;
            this.ObserveLogButton.Image = null;
            this.ObserveLogButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ObserveLogButton.InactiveColorA = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.ObserveLogButton.InactiveColorB = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(130)))), ((int)(((byte)(239)))));
            this.ObserveLogButton.Location = new System.Drawing.Point(1012, 20);
            this.ObserveLogButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ObserveLogButton.Name = "ObserveLogButton";
            this.ObserveLogButton.PressedColorA = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.ObserveLogButton.PressedColorB = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.ObserveLogButton.PressedContourColorA = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.ObserveLogButton.PressedContourColorB = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.ObserveLogButton.Size = new System.Drawing.Size(151, 41);
            this.ObserveLogButton.TabIndex = 57;
            this.ObserveLogButton.Text = "选定并查看日志";
            this.ObserveLogButton.TextAlignment = System.Drawing.StringAlignment.Center;
            this.ObserveLogButton.Click += new System.EventHandler(this.ObserveLogButton_Click);
            // 
            // TipsPanel
            // 
            this.TipsPanel.Controls.Add(this.TipsNotification);
            this.TipsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TipsPanel.Location = new System.Drawing.Point(3, 3);
            this.TipsPanel.Name = "TipsPanel";
            this.TipsPanel.Size = new System.Drawing.Size(1204, 38);
            this.TipsPanel.TabIndex = 60;
            // 
            // TipsNotification
            // 
            this.TipsNotification.BlueBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(237)))), ((int)(((byte)(248)))));
            this.TipsNotification.BlueBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(217)))), ((int)(((byte)(240)))));
            this.TipsNotification.BlueTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(143)))), ((int)(((byte)(184)))));
            this.TipsNotification.Cursor = System.Windows.Forms.Cursors.Default;
            this.TipsNotification.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TipsNotification.EnabledCalc = true;
            this.TipsNotification.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.TipsNotification.GreenBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(240)))), ((int)(((byte)(214)))));
            this.TipsNotification.GreenBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(229)))), ((int)(((byte)(182)))));
            this.TipsNotification.GreenTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(140)))), ((int)(((byte)(69)))));
            this.TipsNotification.Location = new System.Drawing.Point(0, 0);
            this.TipsNotification.Name = "TipsNotification";
            this.TipsNotification.RedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(222)))), ((int)(((byte)(222)))));
            this.TipsNotification.RedBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(204)))), ((int)(((byte)(209)))));
            this.TipsNotification.RedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(99)))), ((int)(((byte)(94)))));
            this.TipsNotification.Size = new System.Drawing.Size(1204, 40);
            this.TipsNotification.Style = pSystem.UI.ReaLTaiizor.Controls.FoxNotification.Styles.Blue;
            this.TipsNotification.TabIndex = 4;
            this.TipsNotification.Text = "未选择日期时默认按照当前程序运行时的操作日志查看";
            this.TipsNotification.YellowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(248)))), ((int)(((byte)(225)))));
            this.TipsNotification.YellowBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(235)))), ((int)(((byte)(200)))));
            this.TipsNotification.YellowTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(131)))), ((int)(((byte)(88)))));
            // 
            // SelectDatePanel
            // 
            this.SelectDatePanel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.SelectDatePanel.Controls.Add(this.SelectDate);
            this.SelectDatePanel.Location = new System.Drawing.Point(3, 47);
            this.SelectDatePanel.Name = "SelectDatePanel";
            this.SelectDatePanel.Size = new System.Drawing.Size(443, 599);
            this.SelectDatePanel.TabIndex = 61;
            // 
            // SelectDate
            // 
            this.SelectDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectDate.BackColor = System.Drawing.Color.White;
            this.SelectDate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(223)))), ((int)(((byte)(230)))));
            this.SelectDate.Date = new System.DateTime(2021, 7, 25, 0, 0, 0, 0);
            this.SelectDate.DayNames = "MTWTFSS";
            this.SelectDate.DaysTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.SelectDate.DayTextColorA = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(49)))), ((int)(((byte)(51)))));
            this.SelectDate.DayTextColorB = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(147)))), ((int)(((byte)(153)))));
            this.SelectDate.HeaderFormat = "{0} Y - {1} M";
            this.SelectDate.HeaderTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(49)))), ((int)(((byte)(51)))));
            this.SelectDate.HeadLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(231)))), ((int)(((byte)(237)))));
            this.SelectDate.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.SelectDate.Location = new System.Drawing.Point(38, 101);
            this.SelectDate.Name = "SelectDate";
            this.SelectDate.NMColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(196)))), ((int)(((byte)(204)))));
            this.SelectDate.NMHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.SelectDate.NYColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(196)))), ((int)(((byte)(204)))));
            this.SelectDate.NYHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.SelectDate.PMColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(196)))), ((int)(((byte)(204)))));
            this.SelectDate.PMHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.SelectDate.PYColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(196)))), ((int)(((byte)(204)))));
            this.SelectDate.PYHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.SelectDate.SelectedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.SelectDate.SelectedTextColor = System.Drawing.Color.White;
            this.SelectDate.Size = new System.Drawing.Size(250, 270);
            this.SelectDate.TabIndex = 5;
            this.SelectDate.Text = "hopeDatePicker1";
            this.SelectDate.ValueTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(133)))), ((int)(((byte)(228)))));
            this.SelectDate.onDateChanged += new pSystem.UI.ReaLTaiizor.Controls.HopeDatePicker.DateChanged(this.SelectDate_onDateChanged);
            // 
            // SearchResultPanel
            // 
            this.SearchResultPanel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.SearchResultPanel.AutoScroll = true;
            this.SearchResultPanel.Controls.Add(this.LogIsNothing);
            this.SearchResultPanel.Location = new System.Drawing.Point(452, 47);
            this.SearchResultPanel.Name = "SearchResultPanel";
            this.SearchResultPanel.Size = new System.Drawing.Size(752, 599);
            this.SearchResultPanel.TabIndex = 58;
            // 
            // LogIsNothing
            // 
            this.LogIsNothing.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LogIsNothing.BlueBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(237)))), ((int)(((byte)(248)))));
            this.LogIsNothing.BlueBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(217)))), ((int)(((byte)(240)))));
            this.LogIsNothing.BlueTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(143)))), ((int)(((byte)(184)))));
            this.LogIsNothing.Cursor = System.Windows.Forms.Cursors.Default;
            this.LogIsNothing.EnabledCalc = true;
            this.LogIsNothing.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LogIsNothing.GreenBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(240)))), ((int)(((byte)(214)))));
            this.LogIsNothing.GreenBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(229)))), ((int)(((byte)(182)))));
            this.LogIsNothing.GreenTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(140)))), ((int)(((byte)(69)))));
            this.LogIsNothing.Location = new System.Drawing.Point(264, 254);
            this.LogIsNothing.Name = "LogIsNothing";
            this.LogIsNothing.RedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(222)))), ((int)(((byte)(222)))));
            this.LogIsNothing.RedBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(204)))), ((int)(((byte)(209)))));
            this.LogIsNothing.RedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(99)))), ((int)(((byte)(94)))));
            this.LogIsNothing.Size = new System.Drawing.Size(246, 40);
            this.LogIsNothing.Style = pSystem.UI.ReaLTaiizor.Controls.FoxNotification.Styles.Red;
            this.LogIsNothing.TabIndex = 59;
            this.LogIsNothing.Text = "当前所选日没有任何日志";
            this.LogIsNothing.Visible = false;
            this.LogIsNothing.YellowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(248)))), ((int)(((byte)(225)))));
            this.LogIsNothing.YellowBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(235)))), ((int)(((byte)(200)))));
            this.LogIsNothing.YellowTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(131)))), ((int)(((byte)(88)))));
            // 
            // OperationLogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(41)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(1345, 803);
            this.Controls.Add(this.OperationLogTab);
            this.MinimumSize = new System.Drawing.Size(1278, 784);
            this.Name = "OperationLogForm";
            this.ShowIcon = false;
            this.Text = "操作日志";
            this.OperationLogTab.ResumeLayout(false);
            this.ChooseDateTabPage.ResumeLayout(false);
            this.ButtonPanel.ResumeLayout(false);
            this.TipsPanel.ResumeLayout(false);
            this.SelectDatePanel.ResumeLayout(false);
            this.SearchResultPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private pSystem.UI.ReaLTaiizor.Controls.AirTabPage OperationLogTab;
        private System.Windows.Forms.TabPage ChooseDateTabPage;
        private System.Windows.Forms.TabPage ObverseLogTabPage;
        private pSystem.UI.ReaLTaiizor.Controls.FoxNotification TipsNotification;
        private pSystem.UI.ReaLTaiizor.Controls.HopeDatePicker SelectDate;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft ObserveLogButton;
        private System.Windows.Forms.Panel SearchResultPanel;
        private pSystem.UI.ReaLTaiizor.Controls.FoxNotification LogIsNothing;
        private pSystem.UI.ReaLTaiizor.Controls.DungeonButtonLeft ReturnSearchButton;
        private System.Windows.Forms.Panel TipsPanel;
        private System.Windows.Forms.Panel SelectDatePanel;
        private System.Windows.Forms.Panel ButtonPanel;
        private System.Windows.Forms.Panel HistoryLogPanel;
    }
}