using AwareTec.Polysmith.Cloud.UI.DataModels;
using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.Predefined;
using AwareTec.Polysmith.Cloud.UI.DataModels.Global;
using AwareTec.Polysmith.Cloud.UI.Forms.MainFormBlock;
using AwareTec.Polysmith.Cloud.UI.Forms.Tips;
using AwareTec.Polysmith.DataBaseCom;
using RestfulWebRequest.ApiCall;
using RestfulWebRequest.RestfulModel;
using RestfulWebRequest.RestfulTable.RestfulRequestTable;
using RestfulWebRequest.RestfulTable.RestfulResponseTable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.Cloud.UI.Forms.RealDataViewBlock
{
    public partial class Calibration : CloudSkinForm
    {
        #region 私有变量

        private string m_matchKey;
        private Dictionary<int, string> m_map = new Dictionary<int, string>();
        private DateTime m_startTime;
        private List<PredefinedCalibration> list = null;
        private double m_run = 0;
        private int m_currentIndex = 0;
        private DateTime m_startbuttonclicktime = new DateTime();
        private bool m_autoSelect = false;
        private Doc_CalibrationRecord m_currentCalibration = null;
        private bool m_HasCalibration = false;
        #endregion

        #region 公有变量

        public Protocol.ProtocolServer Protocol = null;

        #endregion

        #region 初始化 load/dispose/init

        /// <summary>
        /// 初始化
        /// </summary>
        public Calibration()
        {
            InitializeComponent();
            this.Load += Calibration_Load;
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Calibration_Load(object sender, EventArgs e)
        {
            this.CancelButton.Click += CancelButton_Click;
            this.EndButton.Click += EndButton_Click;
            this.StartButton.Click += StartButton_Click;
            this.PassButton.Click += PassButton_Click;
            this.FailButton.Click += FailButton_Click;
            this.SkipButton.Click += SkipButton_Click;

            this.PassButton.Enabled = false;
            this.FailButton.Enabled = false;
            m_matchKey = (Tag as string);
            m_startTime = Channel.Default.StartTime;
            list = GlobalSingleton.Instance.PredefinedData.PredefinedCalibrations;
            List<int> keys = new List<int>();
            /// 获取已定标的项
            bool isSuccess = ApiRequest.Instance.QueryMonitoringData(new RestfulWebRequest.RestfulTable.RestfulRequestTable.QueryMonitoringDataRequestModel
            {
                orderId = m_matchKey
            }, out ResponseModel responseModel);
            if (isSuccess)
            {
                var data2 = responseModel as ResponseSuccessModel<QueryMonitoringDataResponseModel>;
                if (data2 != null)
                {
                    m_HasCalibration = true;
                    if (data2.RestfulTable.calibrationInfo != "")
                    {
                        string[] items = data2.RestfulTable.calibrationInfo.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        if (items.Length > 0)
                        {
                            for (int i = 0; i < items.Length; i++)
                            {
                                string[] ss = items[i].Split(':');
                                if (ss.Length == 2)
                                {
                                    keys.Add(int.Parse(ss[0]));
                                    m_map.Add(int.Parse(ss[0]), ss[1]);
                                }
                            }
                        }
                    }
                }
            }
            list = list.OrderBy(t => t.XH).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                abSelect.Rows.Add(list[i].XH, list[i].Description);
                abSelect.Rows[i].Tag = list[i].Id;
                if (keys.Contains(list[i].Id))
                    (abSelect.Rows[i].Cells[1] as LinkAndImageCell).Image = Properties.Resources.complete2;
            }
            abSelect.Rows[m_currentIndex].Selected = true;
            abSelect.RowStateChanged += abSelect_RowStateChanged;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 数据表 行状态改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void abSelect_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (m_autoSelect)
            {
                m_autoSelect = false;
                return;
            }
            if (e.Row.Index != m_currentIndex)
            {
                m_autoSelect = true;
                abSelect.Rows[m_currentIndex].Selected = true;
            }
        }

        #endregion

        #region 按钮方法

        /// <summary>
        /// 定标跳过按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SkipButton_Click(object sender, EventArgs e)
        {
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("当前定标的：{0}  跳过", abSelect.Rows[m_currentIndex].Cells[1].Value.ToString()));
            m_currentIndex++;
            if (m_currentIndex >= abSelect.Rows.Count)
            {
                m_currentIndex = 0;
            }
            m_autoSelect = true;
            abSelect.Rows[m_currentIndex].Selected = true;
        }

        /// <summary>
        /// 定标失败按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FailButton_Click(object sender, EventArgs e)
        {
            Channel.Default.calibrationChangedView(m_currentCalibration, 2);
            m_currentCalibration = null;
            StartButton.Enabled = true;
            SkipButton.Enabled = true;
            PassButton.Enabled = FailButton.Enabled = false;
            LinkAndImageCell cell = abSelect.Rows[m_currentIndex].Cells[1] as LinkAndImageCell;
            cell.Image = Properties.Resources.failed;
            m_run = 0;
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("当前定标的：{0}  失败", abSelect.Rows[m_currentIndex].Cells[1].Value.ToString()));
            if (!Protocol.SetCalibrationFlag(0))
            {
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("定标写入edf失败 FailButton_Click定标的是：{0}", abSelect.Rows[m_currentIndex].Cells[1].Value.ToString()), pSystem.LogManagement.LogLevel.ERROR);
            }
        }

        /// <summary>
        /// 定标通过按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PassButton_Click(object sender, EventArgs e)
        {
            double runtime = (DateTime.Now - m_startbuttonclicktime).TotalSeconds;
            if (runtime < 3)
            {
                AhDung.MessageTip.ShowError("当前定标的时间太短");
                return;
            }
            double ss = (DateTime.Now - m_startTime).TotalMilliseconds;
            int id = Convert.ToInt32(abSelect.Rows[m_currentIndex].Tag);
            string value = string.Format("{0}-{1}", m_run, ss);
            if (m_map.Keys.Contains(id))
            {
                m_map[id] = value;
            }
            else
            {
                m_map.Add(id, value);
            }
            StartButton.Enabled = true;
            SkipButton.Enabled = true;
            PassButton.Enabled = FailButton.Enabled = false;
            m_run = 0;
            LinkAndImageCell cell = abSelect.Rows[m_currentIndex].Cells[1] as LinkAndImageCell;
            cell.Image = Properties.Resources.complete2;
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("当前定标的：{0}  通过", abSelect.Rows[m_currentIndex].Cells[1].Value.ToString()));
            m_currentIndex++;
            if (m_currentIndex < abSelect.Rows.Count)
            {
                m_autoSelect = true;
                abSelect.Rows[m_currentIndex].Selected = true;
            }
            else
            {
                m_currentIndex--;
            }
            Channel.Default.calibrationChangedView(m_currentCalibration, 1);
            m_currentCalibration = null;
            if (!Protocol.SetCalibrationFlag(0))
            {
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("定标写入edf失败 PassButton_Click定标的是：{0}", abSelect.Rows[m_currentIndex].Cells[1].Value.ToString()), pSystem.LogManagement.LogLevel.ERROR);
            }
        }

        /// <summary>
        /// 定标开始按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartButton_Click(object sender, EventArgs e)
        {
            m_startbuttonclicktime = DateTime.Now;
            m_run = (DateTime.Now - m_startTime).TotalMilliseconds;
            StartButton.Enabled = false;
            SkipButton.Enabled = false;
            m_currentCalibration = new Doc_CalibrationRecord();
            m_currentCalibration.StartTime = m_startTime;
            m_currentCalibration.Comments = string.Format("{0}:{1}-0", Convert.ToInt32(abSelect.Rows[m_currentIndex].Tag), m_run);
            Channel.Default.calibrationChangedView(m_currentCalibration, 0);
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("点击 病人定标-开始 按钮 当前定标的是：{0}", abSelect.Rows[m_currentIndex].Cells[1].Value.ToString()), pSystem.LogManagement.LogLevel.INFO);
            if (!Protocol.SetCalibrationFlag(Convert.ToInt32(abSelect.Rows[m_currentIndex].Cells[0].Value) + 1))
            {
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("定标写入edf失败 StartButton_Click定标的是：{0}", abSelect.Rows[m_currentIndex].Cells[1].Value.ToString()), pSystem.LogManagement.LogLevel.ERROR);
            }
            //为了定标的字可以完整的显示出来，所以做了2秒的限制
            //System.Threading.Thread.Sleep(5);
            PassButton.Enabled = FailButton.Enabled = true;
        }

        /// <summary>
        /// 完成按钮  点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EndButton_Click(object sender, EventArgs e)
        {
            Channel.Default.calibrationChangedView(m_currentCalibration, 2);///清除最后一个没通过的标记
            m_currentCalibration = null;
            if (m_map.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (KeyValuePair<int, string> pp in m_map)
                {
                    sb.AppendFormat("{0}:{1};", pp.Key, pp.Value);
                }
                AddOrEditMonitoringDataRequestModel model = new AddOrEditMonitoringDataRequestModel()
                {
                    orderId = m_matchKey,
                    spO2Info = "",
                    analysisResults = "",
                    calibrationInfo = sb.ToString(),
                    eventMarkers = "",
                    frameInfo = "",
                    version = 0
                };
                /// 保存定标信息到云服务平台
                bool isSuccess = m_HasCalibration? ApiRequest.Instance.EditMonitoringData(model, out ResponseModel responseModel)
                    : ApiRequest.Instance.AddMonitoringData(model, out ResponseModel responseModel2);
                if (isSuccess)
                {

                }
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("点击 病人定标-结束 按钮");
                if (!Protocol.SetCalibrationFlag(0))
                {
                    DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("定标写入edf失败 EndButton_Click", pSystem.LogManagement.LogLevel.ERROR);
                }
                this.Close();
            }
        }

        /// <summary>
        /// 取消按钮  点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (MessageForm.Show("定标信息未完成，确定退出？", "信息确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                AddOrEditMonitoringDataRequestModel model = new AddOrEditMonitoringDataRequestModel()
                {
                    orderId = m_matchKey,
                    spO2Info = "",
                    analysisResults = "",
                    calibrationInfo = "",
                    eventMarkers = "",
                    frameInfo = "",
                    version = 0
                };
                /// 保存定标信息到云服务平台
                bool isSuccess = m_HasCalibration ? ApiRequest.Instance.EditMonitoringData(model, out ResponseModel responseModel)
                    : ApiRequest.Instance.AddMonitoringData(model, out ResponseModel responseModel2);
                if (isSuccess)
                {
                    Channel.Default.calibrationChangedView(m_currentCalibration, 3);///清除所有定标
                    m_currentCalibration = null;
                    DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("点击 病人定标-取消 按钮");
                    if (!Protocol.SetCalibrationFlag(0))
                    {
                        DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("定标写入edf失败 CancelButton_Click", pSystem.LogManagement.LogLevel.ERROR);
                    }
                    this.Close();
                }
                else
                {
                    AhDung.MessageTip.ShowWarning("取消操作失败，请重试！");
                }
            }
        }

        #endregion

        #region 公有方法

        #endregion


    }
}
