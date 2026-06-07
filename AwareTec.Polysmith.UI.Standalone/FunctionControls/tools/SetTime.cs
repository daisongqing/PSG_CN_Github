using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.UI.FunctionControls.tools
{
    public partial class SetTime : SkinForm
    {
        DotNetCtl.ErrorProviderHelper m_errorProvider;
        private bool m_ready = true;
        
        public SetTime()
        {
            InitializeComponent();
            m_errorProvider = new DotNetCtl.ErrorProviderHelper(this);
            this.KeyPreview = true;
            this.KeyDown += LockDialog_KeyDown;
            this.Load += SetTime_Load;

        }

        private void SetTime_Load(object sender, EventArgs e)
        {
            if (m_start.Year != 1 && m_end.Year != 1)
            {
                startdateTimePicker1.Value = m_start;
                this.stopdateTimePicker1.ValueChanged += new System.EventHandler(this.startdateTimePicker1_ValueChanged);
                this.startdateTimePicker1.ValueChanged += new System.EventHandler(this.startdateTimePicker1_ValueChanged);
                stopdateTimePicker1.Value = m_end;
                AhDung.MessageTip.ShowOk(Program.Language == "EN" ? "The device is in timed mode and the start/end time has been obtained!" : "设备已处于定时模式，已获取开始/结束时间！", 1800);
            }
            else
            {
                startdateTimePicker1.Value = DateTime.Parse(string.Format("{0:yyyy-MM-dd} 20:00:00", DateTime.Now));
                this.stopdateTimePicker1.ValueChanged += new System.EventHandler(this.startdateTimePicker1_ValueChanged);
                this.startdateTimePicker1.ValueChanged += new System.EventHandler(this.startdateTimePicker1_ValueChanged);
                stopdateTimePicker1.Value = DateTime.Parse(string.Format("{0:yyyy-MM-dd} 08:00:00", DateTime.Now.AddDays(1)));
            }
        }

        private void LockDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveButton_Click(null,null);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AutoRunTime
        {
            set
            {
                if (value != "")
                {
                    string[] ss = value.Split(',');
                    if (ss.Length == 2)
                    {
                        m_start = DateTime.Parse(ss[0]);
                        m_end = DateTime.Parse(ss[1]);
                    }
                }
            }
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            {
                SaveButton.Enabled = CancelButton.Enabled = false;
                if (SetTimeHandler != null)
                {
                    IAsyncResult ret = SetTimeHandler.BeginInvoke(startdateTimePicker1.Value, stopdateTimePicker1.Value, null, null);

                    bool result = SetTimeHandler.EndInvoke(ret);
                    if (result)
                        this.DialogResult = DialogResult.OK;
                    else
                    {
                        SaveButton.Enabled = CancelButton.Enabled = true;
                    }
                }
            }
        }
        private DateTime m_start=default(DateTime);
        private DateTime m_end = default(DateTime);
        private void startdateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dp=(DateTimePicker)sender;
            if (startdateTimePicker1.Value < DateTime.Now)
            {
                startdateTimePicker1.Value = m_start.Year == 1 ? DateTime.Now : m_start;
                AhDung.MessageTip.ShowWarning(Program.Language=="EN"? "The start time cannot be earlier than the current time" : "开始时间不能早于当前时间");
                return;
            }
            int sec = (int)(stopdateTimePicker1.Value - startdateTimePicker1.Value).TotalSeconds;
            if (sec < 1800)
            {
                if (dp.Name == "startdateTimePicker1")
                {
                    startdateTimePicker1.Value = m_start.Year == 1 ? DateTime.Now : m_start;
                }
                else
                    stopdateTimePicker1.Value = m_end.Year == 1 ? DateTime.Now : m_end;
                AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "Record time not less than 30 minutes" : "记录时间不少于30min");
                return;
            }
            TimeSpan span=TimeSpan.FromSeconds(sec);
            label4.Text = string.Format("Total recording time：{0}", span);
            m_start = startdateTimePicker1.Value;
            m_end = stopdateTimePicker1.Value;
        }

        public delegate bool SetTimeDelegate(DateTime start,DateTime end);
        /// <summary>
        /// 设置定时开关机时触发
        /// </summary>
        public event SetTimeDelegate SetTimeHandler;

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DataModel.LogInstance.Default.AddLog("用户点击 定时设置-取消按钮");
            this.Close();
        }
    }
}
