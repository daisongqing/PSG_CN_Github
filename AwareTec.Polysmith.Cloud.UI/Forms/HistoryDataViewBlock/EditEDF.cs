using AwareTec.Polysmith.Cloud.UI.DataModels;
using AwareTec.Polysmith.Cloud.UI.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.Cloud.UI.Forms.HistoryDataViewBlock
{
    public partial class EditEDF : CloudSkinForm
    {
        #region 私有变量

        private DateTime m_starttime = new DateTime();
        private DateTime m_stoptime = new DateTime();

        #endregion

        #region 公有变量

        #endregion

        #region 事件委托

        /// <summary>
        /// 传递开关灯时间和剪切edf的方式
        /// </summary>
        public event OnOffTimesDelegate OnOffTimesEventHandler;
        public delegate bool OnOffTimesDelegate(DateTime starttime, DateTime stoptime, bool byLightOnOff);

        #endregion


        #region 初始化 load/dispose/init

        /// <summary>
        /// 初始化
        /// </summary>
        public EditEDF(HistoryDataView historyDataView)
        {
            InitializeComponent();
            this.Load += EditEDF_Load;

            historyDataView.SearchProgressBarValueEvenetHandle += HistoryDataView_SearchProgressBarValueEvenetHandle;

        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditEDF_Load(object sender, EventArgs e)
        {
            this.CancelButton.Click += CancelButton_Click;
            this.ConfirmButton.Click += ConfirmButton_Click;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 设置进度
        /// </summary>
        /// <param name="value"></param>
        /// <param name="contentTxt"></param>
        private void SetProcessMsg(int value, string contentTxt)
        {
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

        #region 按钮方法

        /// <summary>
        /// 编辑edf 确定按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            ConfirmButton.Enabled = false;
            SearchProgressBar.Visible = true;
            m_starttime = Channel.Default.AnalysisReult.Sleep.LightOffTime;
            m_stoptime = Channel.Default.AnalysisReult.Sleep.LightOnTime;
            //DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("用户在剪切EDF页面 点击确定按钮 关灯时间为{0} 开灯时间为{1} 用户选择通过{2}裁剪", m_starttime, m_stoptime, ByLightOnOffRadioButton.Checked ? "开关灯" : "小睡时间"));
            if (OnOffTimesEventHandler != null)
            {
                if (OnOffTimesEventHandler.Invoke(m_starttime, m_stoptime, ByLightOnOffRadioButton.Checked))
                {
                    AhDung.MessageTip.ShowOk("剪切文件成功");
                    //DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("剪切文件成功");
                }
                this.Close();
                this.Dispose();
            }
        }

        /// <summary>
        /// 编辑edf 取消按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            //DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("用户在剪切EDF页面 点击取消按钮  ");
            this.Close();
            this.Dispose();
        }

        #endregion

        #region 公有方法

        #endregion

    }
}
