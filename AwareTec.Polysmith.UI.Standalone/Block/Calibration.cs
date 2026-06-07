using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AwareTec.Polysmith.DataBaseCom;
using AwareTec.Polysmith.UI.FunctionControls.tools;
namespace AwareTec.Polysmith.UI.Block
{
    public partial class Calibration : SkinForm
    {
        string m_matchKey;
        public Protocol.ProtocolServer Protocol = null;
        public Calibration()
        {
            InitializeComponent();
            this.Load += Calibration_Load;
        }
        private Dictionary<int, string> m_map = new Dictionary<int, string>();
        private DateTime m_startTime;
        private List<Doc_CalibrationDefine> list = null;
        private double m_run = 0;
        private int m_currentIndex = 0;
        private DateTime m_startbuttonclicktime=new DateTime();
        private void Calibration_Load(object sender, EventArgs e)
        {
            this.PassButton.Enabled = false;
            this.FailButton.Enabled = false;
            m_matchKey = (Tag as Doc_MainViewRecord).MatchKey;
            m_startTime = Channel.Default.StartTime;
            list = DataModel.DataBaseHelper.Default.SelectAll(new Doc_CalibrationDefine());
            List<int> keys = new List<int>();
            Doc_CalibrationRecord record = DataModel.DataBaseHelper.Default.Select(new Doc_CalibrationRecord() { MatchKey = m_matchKey });
            if (record != null)
            {
                if (record.Comments != "")
                {
                    string[] items = record.Comments.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (items.Length > 0)
                    {
                        for (int i = 0; i < items.Length; i++)
                        {
                            string[] ss = items[i].Split(':');
                            if (ss.Length == 2)
                            {
                                keys.Add(int.Parse(ss[0]));
                            }
                        }
                    }
                }
            }
            list = list.OrderBy(t => t.XH).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                abSelect.Rows.Add(list[i].XH,list[i].Description);
                abSelect.Rows[i].Tag = list[i].ID;
                if (keys.Contains(list[i].ID))
                    (abSelect.Rows[i].Cells[1] as LinkAndImageCell).Image = Properties.Resources.complete2;
            }
            abSelect.Rows[m_currentIndex].Selected = true;
            abSelect.RowStateChanged += abSelect_RowStateChanged;
        }
        private bool m_autoSelect = false;
        private void abSelect_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (m_autoSelect)
            {
                m_autoSelect = false;
                return;
            }
            if (e.Row.Index != m_currentIndex)
            {
                //e.Row.Selected = false;
                m_autoSelect = true;
                abSelect.Rows[m_currentIndex].Selected = true;
            }
        }
        private Doc_CalibrationRecord m_currentCalibration = null;
        private void StartButton_Click(object sender, EventArgs e)
        {
            m_startbuttonclicktime = DateTime.Now;
            m_run = (DateTime.Now - m_startTime).TotalMilliseconds;
            StartButton.Enabled = false;
            SkipButton.Enabled = false;
            m_currentCalibration=new Doc_CalibrationRecord ();
            m_currentCalibration.StartTime = m_startTime;
            m_currentCalibration.Comments = string.Format("{0}:{1}-0", Convert.ToInt32(abSelect.Rows[m_currentIndex].Tag), m_run);
            Channel.Default.calibrationChangedView(m_currentCalibration, 0);
            DataModel.LogInstance.Default.AddLog(string.Format("点击 病人定标-开始 按钮 当前定标的是：{0}", abSelect.Rows[m_currentIndex].Cells[1].Value.ToString()),pSystem.LogManagement.LogLevel.INFO);
            if (!Protocol.SetCalibrationFlag(Convert.ToInt32(abSelect.Rows[m_currentIndex].Cells[0].Value)+1))
            {
                DataModel.LogInstance.Default.AddLog(string.Format("定标写入edf失败 StartButton_Click定标的是：{0}", abSelect.Rows[m_currentIndex].Cells[1].Value.ToString()), pSystem.LogManagement.LogLevel.ERROR);
            }
            //为了定标的字可以完整的显示出来，所以做了2秒的限制
            //System.Threading.Thread.Sleep(5);
            PassButton.Enabled = FailButton.Enabled = true;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (MessageForm.Show(Program.Language=="EN"? "Calibration information not completed, are you sure to exit?" : "定标信息未完成，确定退出？", Program.Language == "EN" ? "CONFIRM" : "信息确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
                Channel.Default.calibrationChangedView(m_currentCalibration, 3);///清除所有定标
                m_currentCalibration = null;
                DataModel.LogInstance.Default.AddLog("点击 病人定标-取消 按钮");
                if (!Protocol.SetCalibrationFlag(0))
                {
                    DataModel.LogInstance.Default.AddLog("定标写入edf失败 CancelButton_Click", pSystem.LogManagement.LogLevel.ERROR);
                }
            }
        }

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
            DataModel.LogInstance.Default.AddLog(string.Format("当前定标的：{0}  通过", abSelect.Rows[m_currentIndex].Cells[1].Value.ToString()), pSystem.LogManagement.LogLevel.DEBUG);
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
                DataModel.LogInstance.Default.AddLog(string.Format("定标写入edf失败 PassButton_Click定标的是：{0}", abSelect.Rows[m_currentIndex].Cells[1].Value.ToString()), pSystem.LogManagement.LogLevel.ERROR);
            }
        }

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
            DataModel.LogInstance.Default.AddLog(string.Format("当前定标的：{0}  失败", abSelect.Rows[m_currentIndex].Cells[1].Value.ToString()), pSystem.LogManagement.LogLevel.DEBUG);
            if (!Protocol.SetCalibrationFlag(0))
            {
                DataModel.LogInstance.Default.AddLog(string.Format("定标写入edf失败 FailButton_Click定标的是：{0}", abSelect.Rows[m_currentIndex].Cells[1].Value.ToString()), pSystem.LogManagement.LogLevel.ERROR);
            }
        }

        private void SkipButton_Click(object sender, EventArgs e)
        {
            DataModel.LogInstance.Default.AddLog(string.Format("当前定标的：{0}  跳过", abSelect.Rows[m_currentIndex].Cells[1].Value.ToString()), pSystem.LogManagement.LogLevel.DEBUG);
            m_currentIndex++;
            if (m_currentIndex >= abSelect.Rows.Count)
            {
                m_currentIndex=0;
            }
            m_autoSelect = true;
            abSelect.Rows[m_currentIndex].Selected = true;          
        }

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
                Doc_CalibrationRecord record = new Doc_CalibrationRecord() { MatchKey = m_matchKey };
                if (DataModel.DataBaseHelper.Default.Exsit(record))
                {
                    DataModel.DataBaseHelper.Default.Update(record, new Doc_CalibrationRecord() { Comments = sb.ToString() });
                }
                else
                {
                    record.StartTime = m_startTime;
                    record.Comments = sb.ToString();
                    DataModel.DataBaseHelper.Default.Insert(record);
                }
            }
            DataModel.LogInstance.Default.AddLog("点击 病人定标-结束 按钮");
            if (!Protocol.SetCalibrationFlag(0))
            {
                DataModel.LogInstance.Default.AddLog("定标写入edf失败 EndButton_Click", pSystem.LogManagement.LogLevel.ERROR);
            }
            this.Close();
        }

    }
}
