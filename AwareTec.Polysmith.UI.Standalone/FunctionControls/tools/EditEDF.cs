using AwareTec.Polysmith.pChart;
using AwareTec.Polysmith.UI.DataModel;
using AwareTec.Polysmith.UI.FunctionControls;
using AwareTec.Polysmith.Util;
using AwareTec.Polysmith.Util.PathUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.UI.Block
{
    public partial class EditEDF : SkinForm
    {
        #region 公有变量
        public HistoryDataView historyDataView = null;
        #endregion

        #region 私有变量
        private DateTime m_starttime = new DateTime();
        private DateTime m_stoptime = new DateTime();
        #endregion

        #region 事件与委托的定义
        public event OnOffTimesDelegate OnOffTimesEventHandler;
        public delegate bool OnOffTimesDelegate(DateTime starttime, DateTime stoptime,bool byLightOnOff);
        #endregion

        #region 内部方法
        /// <summary>
        /// 设置进度
        /// </summary>
        /// <param name="value"></param>
        /// <param name="contentTxt"></param>
        private void SetProcessMsg(int value, string contentTxt)
        {
            //this.Invoke(new MethodInvoker(() =>
            //{
            //    SearchProgressBar.Value = value;
            //}));
            value = value > 100 ? 100 : value;
            value = value < 0 ? 0 : value;
            SearchProgressBar.Value = value;

        }
        /// <summary>
        /// 进度条传值
        /// </summary>
        /// <param name="barvalue"></param>
        private void HistoryDataView_SearchProgressBarValueEvenetHandle(long barvalue)
        {
            SetProcessMsg((int)barvalue, "");
        }
        #endregion


        #region 初始化相关

        public EditEDF(HistoryDataView historyDataView)
        {
            InitializeComponent();
            historyDataView.SearchProgressBarValueEvenetHandle += HistoryDataView_SearchProgressBarValueEvenetHandle;
            #region 为防止designer随意更改 解绑事件，因此事件绑定一律放置于逻辑代码的构造函数中
            this.CancelButton.Click += this.CancelButton_Click;
            this.ConfirmButton.Click += this.ConfirmButton_Click;
            #endregion
        }

        #endregion

        #region 所有控件的绑定事件
        /// <summary>
        /// 取消按钮点击时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            DataModel.LogInstance.Default.AddLog("用户在剪切EDF页面 点击取消按钮");
            this.Close();
            this.Dispose();
        }

        /// <summary>
        /// 确定按钮点击时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            ConfirmButton.Enabled = false;
            SearchProgressBar.Visible = true;
            m_starttime = Channel.Default.AnalysisReult.Sleep.LightOffTime;
            m_stoptime = Channel.Default.AnalysisReult.Sleep.LightOnTime;
            DataModel.LogInstance.Default.AddLog(string.Format("用户在剪切EDF页面 点击确定按钮 关灯时间为{0} 开灯时间为{1} 用户选择通过{2}裁剪", m_starttime, m_stoptime, ByLightOnOffRadioButton.Checked ? "开关灯" : "小睡时间"), pSystem.LogManagement.LogLevel.INFO);
            if (OnOffTimesEventHandler != null)
            {
                if (OnOffTimesEventHandler.Invoke(m_starttime, m_stoptime, ByLightOnOffRadioButton.Checked))
                {
                    AhDung.MessageTip.ShowOk(Program.Language=="EN"? "Cut file successfully" : "剪切文件成功");
                    DataModel.LogInstance.Default.AddLog("剪切文件成功", pSystem.LogManagement.LogLevel.WARN);
                }
                this.Close();
                this.Dispose();
            }
        }

        #endregion
    }
}
