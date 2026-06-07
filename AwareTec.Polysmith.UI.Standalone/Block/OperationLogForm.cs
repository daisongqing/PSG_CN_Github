using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AwareTec.Polysmith.UI.FunctionControls.tools;
using pSystem.LogManagement.TotalLog;

namespace AwareTec.Polysmith.UI.Block
{
    public partial class OperationLogForm : SkinForm
    {
        #region 私有字段

        #region 用于选择日期选项卡
        /// <summary>
        /// 当前选择日期选项卡中的选中单选按钮
        /// </summary>
        private static pSystem.UI.ReaLTaiizor.Controls.AirRadioButton _checkedRadioButton = null;
        #endregion

        private const int ANCHOR_BOTTOM_DIFF = 25;
        #endregion

        #region 视觉参数
        const int LEFT_ANCHOR_DIFF = 5;
        const int TOP_ANCHOR_DIFF = 5;
        const int DIFF_PER_BUTTON = 10;
        const int PANEL_DIFF = 6;
        #endregion

        #region 全局控制

        /// <summary>
        /// 构造函数
        /// </summary>
        public OperationLogForm()
        {
            InitializeComponent();
            ChooseDateTabPage.Resize += ChooseDateTabPage_Resize;
            FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OperationLogForm_FormClosing);
            if (!TotalLogManager.DeleteHistories())
                throw new Exception("自动删除历史30天失败");
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            InitBrowser(TotalLogManager.Current);

            InitDateSelect();
        }

        /// <summary>
        /// 重绘
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            base.OnPaint(e);
        }
        #endregion

        #region 查看日志选项卡
        /// <summary>
        /// 初始化[查看日志]选项卡
        /// </summary>
        /// <param name="pathArr"></param>
        private void InitBrowser(LogPerRun log)
        {
            var logWebBrowser = new ObverseLogCtrl(log);
            logWebBrowser.Dock = DockStyle.Fill;
            ObverseLogTabPage.Controls.Add(logWebBrowser);
        }

        #endregion

        #region 选择日期选项卡

        /// <summary>
        /// 初始化[查看日期]选项卡
        /// </summary>
        /// <remarks>
        /// 默认打开时选择的是当天
        /// </remarks>
        private void InitDateSelect()
        {
            DateTime time = DateTime.Now;
            SelectDate.Date = time;
            SelectDate_onDateChanged(SelectDate.Date);
        }

        /// <summary>
        /// 处理所选日期转变事件
        /// </summary>
        /// <param name="newDateTime"></param>
        private void SelectDate_onDateChanged(DateTime newDateTime)
        {
            SearchResultPanel.Controls.Clear();
            SearchResultPanel.Controls.Add(LogIsNothing);
            //if (!TotalLogManager.SearchHistoryRecordsByDate(newDateTime,
            //                                                out List<LogPerRun> histories))
            //{
            //    this.LogIsNothing.Visible = true;
            //    this.ObserveLogButton.Enabled = false;
            //}
            //else
            //{
            //    this.LogIsNothing.Visible = false;
            //    this.ObserveLogButton.Enabled = true;
            //}
            List<LogInfo> histories = null;
            if ((histories = LogReader.GetLogFilesByStartTime(newDateTime)).Count == 0)
            {
                this.LogIsNothing.Visible = true;
                this.ObserveLogButton.Enabled = false;
            }
            else
            {
                this.LogIsNothing.Visible = false;
                this.ObserveLogButton.Enabled = true;
            }

            for (int index = 0; index < histories.Count; index++)
            {
                var item = histories[index];
                var radioButton = GenerateRadioButton(item);
                if (index == 0)
                    radioButton.Checked = true;
                SearchResultPanel.Controls.Add(radioButton);
            }
            CalculateSearchResultPanelPosAndSize();
        }

        /// <summary>
        /// 生成单选按钮
        /// </summary>
        /// <param name="perRun"></param>
        /// <returns></returns>
        private pSystem.UI.ReaLTaiizor.Controls.AirRadioButton GenerateRadioButton(LogInfo logInfo)
        {
            var g = this.CreateGraphics();
            pSystem.UI.ReaLTaiizor.Controls.AirRadioButton radioButton = new pSystem.UI.ReaLTaiizor.Controls.AirRadioButton();
            string radioTextTemplate = "日志开始写入时间:{0}; 日志结束写入日期:{1}";
            string text = string.Format(radioTextTemplate, logInfo.StartTime, logInfo.EndDate);
            Font font = new Font("Segoe UI", 9F);
            var fontSize = g.MeasureString(text, font);
            const int margin = 20;

            //radioButton视觉设置
            radioButton.Text = text;
            radioButton.Field = 16;
            radioButton.Customization = "PDw8/+3t7f/m5ub/p6en/2RkZP8=";
            radioButton.ForeColor = Color.Black;
            radioButton.Tag = logInfo;
            radioButton.Checked = false;
            radioButton.Cursor = Cursors.Hand;
            radioButton.Transparent = false;
            radioButton.Font = font;
            radioButton.NoRounding = false;
            radioButton.Size = new Size((int)fontSize.Width + margin, (int)fontSize.Height + margin);
            radioButton.CheckedChanged += RadioButton_CheckedChanged;

            return radioButton;
        }

        /// <summary>
        /// 生成单选按钮
        /// </summary>
        /// <param name="perRun"></param>
        /// <returns></returns>
        private pSystem.UI.ReaLTaiizor.Controls.AirRadioButton GenerateRadioButton(LogPerRun perRun)
        {
            var g = this.CreateGraphics();
            pSystem.UI.ReaLTaiizor.Controls.AirRadioButton radioButton = new pSystem.UI.ReaLTaiizor.Controls.AirRadioButton();
            string radioTextTemplate = "日志开始写入时间:{0}; 日志结束写入时间:{1}";
            string text = string.Format(radioTextTemplate, perRun.StartTime, perRun.EndTime);
            Font font = new Font("Segoe UI", 9F);
            var fontSize = g.MeasureString(text, font);
            const int margin = 3;

            //radioButton视觉设置
            radioButton.Text = text;
            radioButton.Field = 16;
            radioButton.Customization = "PDw8/+3t7f/m5ub/p6en/2RkZP8=";
            radioButton.ForeColor = Color.Black;
            radioButton.Tag = perRun;
            radioButton.Checked = false;
            radioButton.Cursor = Cursors.Hand;
            radioButton.Transparent = false;
            radioButton.Font = font;
            radioButton.NoRounding = false;
            radioButton.Size = new Size((int)fontSize.Width + margin, (int)fontSize.Height + margin);
            radioButton.CheckedChanged += RadioButton_CheckedChanged;

            return radioButton;
        }

        /// <summary>
        /// 处理单选按钮选中状态改变事件
        /// </summary>
        /// <param name="sender"></param>
        private static void RadioButton_CheckedChanged(object sender)
        {
            pSystem.UI.ReaLTaiizor.Controls.AirRadioButton radioButton = sender as pSystem.UI.ReaLTaiizor.Controls.AirRadioButton;
            if (radioButton.Checked)
                _checkedRadioButton = radioButton;
        }

        /// <summary>
        /// 处理选定并查看日志按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ObserveLogButton_Click(object sender, EventArgs e)
        {
            if (_checkedRadioButton == null)
                MessageForm.Show("当前未选定任一运行日志");

            //var logPerRun = _checkedRadioButton.Tag as LogPerRun;
            //OpenBrowser(logPerRun);
            var logInfo = _checkedRadioButton.Tag as LogInfo;
            OpenBrowser(logInfo);
        }

        /// <summary>
        /// 打开历史日志浏览器
        /// </summary>
        /// <param name="log"></param>
        private void OpenBrowser(LogInfo log)
        {
            ChangeVisibleForAnyCtrls(ChooseDateTabPage);
            DisposeAndRemoveLogCtrl(HistoryLogPanel);
            LogIsNothing.Visible = false;

            //CalculateHistoryLogPanelPosAndSize();
            var logWebBrowser = new ObverseLogCtrl(log);
            logWebBrowser.Dock = DockStyle.Fill;
            HistoryLogPanel.Controls.Add(logWebBrowser);
        }

        /// <summary>
        /// 打开历史日志浏览器
        /// </summary>
        /// <param name="log"></param>
        private void OpenBrowser(LogPerRun log)
        {
            ChangeVisibleForAnyCtrls(ChooseDateTabPage);
            DisposeAndRemoveLogCtrl(HistoryLogPanel);
            LogIsNothing.Visible = false;
            
            //CalculateHistoryLogPanelPosAndSize();
            var logWebBrowser = new ObverseLogCtrl(log);
            logWebBrowser.Dock = DockStyle.Fill;
            HistoryLogPanel.Controls.Add(logWebBrowser);
        }
        #endregion

        /// <summary>
        /// 对Control内部的任一Control(递归查找)进行可见性的改变
        /// </summary>
        /// <returns>
        /// 若该控件下的子控件至少有一个可见性为true，则返回true;
        /// 全部不可见则返回false
        /// </returns>
        /// <remarks>
        /// 若其任一子控件下的visible为true，则其为true
        /// </remarks>
        private bool ChangeVisibleForAnyCtrls(Control ctrl)
        {
            bool atLeastItemIsVisible = false;
            foreach (Control item in ctrl.Controls)
            {
                bool childAtLeastItemIsVisible = false;
                if (item.Controls.Count > 0)
                {
                    if (ChangeVisibleForAnyCtrls(item))
                        childAtLeastItemIsVisible = true;
                }

                if (item.Visible && childAtLeastItemIsVisible)
                {
                    item.Visible = true;
                    continue;
                }
                    
                item.Visible = !item.Visible;

                if(item.Visible)
                    atLeastItemIsVisible = true;
            }
            return atLeastItemIsVisible;
        }

        private void ReturnSearchButton_Click(object sender, EventArgs e)
        {
            ChangeVisibleForAnyCtrls(ChooseDateTabPage);
            
            InitDateSelect();
            CalculateSearchResultPanelPosAndSize();
        }

        private void ChooseDateTabPage_Resize(object sender, EventArgs e)
        {
            //CalculateHistoryLogPanelPosAndSize();

            CalculateSearchResultPanelPosAndSize();
        }

        private void CalculateHistoryLogPanelPosAndSize()
        {
            if (HistoryLogPanel.Visible)
                HistoryLogPanel.Size = new Size(HistoryLogPanel.Size.Width, ButtonPanel.Location.Y - PANEL_DIFF);
        }

        private void CalculateSearchResultPanelPosAndSize()
        {
            var panelPoint = new Point(SelectDatePanel.Location.X + SelectDatePanel.Width + PANEL_DIFF,
                                        TipsPanel.Location.Y + TipsPanel.Height + PANEL_DIFF);
            SearchResultPanel.Location = panelPoint;
            int width = ChooseDateTabPage.Width - SearchResultPanel.Location.X;
            int height = ButtonPanel.Location.Y - PANEL_DIFF - (TipsPanel.Location.Y + TipsPanel.Height + PANEL_DIFF);
            SearchResultPanel.Size = new Size(width, height);

            if (LogIsNothing.Visible)
            {
                Point center = new Point(SearchResultPanel.Width / 2, SearchResultPanel.Height / 2);
                int x = center.X - LogIsNothing.Width / 2;
                int y = center.Y - LogIsNothing.Height / 2;

                LogIsNothing.Location = new Point(x, y);
                return;
            }

            int index = 0;
            foreach (Control item in SearchResultPanel.Controls)
            {
                if (!(item is pSystem.UI.ReaLTaiizor.Controls.AirRadioButton))
                    continue;

                var radioButton = item as pSystem.UI.ReaLTaiizor.Controls.AirRadioButton;

                radioButton.Location = new Point(LEFT_ANCHOR_DIFF,
                                                 TOP_ANCHOR_DIFF + (radioButton.Size.Height + DIFF_PER_BUTTON) * index);
                index++;
            }
        }

        private void OperationLogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisposeAndRemoveLogCtrl(ObverseLogTabPage);
            DisposeAndRemoveLogCtrl(HistoryLogPanel);
        }

        /// <summary>
        /// 销毁和清除查看日志的控件
        /// </summary>
        /// <param name="parent"></param>
        private void DisposeAndRemoveLogCtrl(Control parent)
        {
            for(int index = 0; index < parent.Controls.Count; index++)
            {
                var ctrl = parent.Controls[index];
                if (!(ctrl is ObverseLogCtrl))
                    continue;
                var obverseLogCtrl = ctrl as ObverseLogCtrl;
                parent.Controls.RemoveAt(index);
                obverseLogCtrl.UnbindEvents();
                obverseLogCtrl.Dispose();
            }
        }
    }
}
