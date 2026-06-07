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
    public partial class SetTime : CloudSkinForm
    {
        #region 私有变量

        private DateTime m_start = default(DateTime);
        private DateTime m_end = default(DateTime);

        #endregion

        #region 公有变量

        /// <summary>
        /// 自动开始时间
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

        #endregion

        #region 事件委托

        public delegate bool SetTimeDelegate(DateTime start, DateTime end);
        /// <summary>
        /// 设置定时开关机时触发
        /// </summary>
        public event SetTimeDelegate SetTimeHandler;

        #endregion

        #region 初始化 load/dispose/init

        /// <summary>
        /// 初始化
        /// </summary>
        public SetTime()
        {
            InitializeComponent();
            this.Load += SetTime_Load;
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetTime_Load(object sender, EventArgs e)
        {
            this.CancelButton.Click += CancelButton_Click;
            this.ConformButton.Click += ConformButton_Click;
            this.KeyDown += SetTime_KeyDown;

            this.KeyPreview = true;
            if (m_start.Year != 1 && m_end.Year != 1)
            {
                startdateTimePicker1.Value = m_start;
                this.stopdateTimePicker1.ValueChanged += new System.EventHandler(this.startdateTimePicker1_ValueChanged);
                this.startdateTimePicker1.ValueChanged += new System.EventHandler(this.startdateTimePicker1_ValueChanged);
                stopdateTimePicker1.Value = m_end;
                AhDung.MessageTip.ShowOk("设备已处于定时模式，已获取开始/结束时间！", 1800);
            }
            else
            {
                startdateTimePicker1.Value = DateTime.Parse(string.Format("{0:yyyy-MM-dd} 20:00:00", DateTime.Now));
                this.stopdateTimePicker1.ValueChanged += new System.EventHandler(this.startdateTimePicker1_ValueChanged);
                this.startdateTimePicker1.ValueChanged += new System.EventHandler(this.startdateTimePicker1_ValueChanged);
                stopdateTimePicker1.Value = DateTime.Parse(string.Format("{0:yyyy-MM-dd} 08:00:00", DateTime.Now.AddDays(1)));
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 键盘快捷键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConformButton_Click(null, null);
            }

        }

        /// <summary>
        /// 开始时间值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startdateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dp = (DateTimePicker)sender;
            if (startdateTimePicker1.Value < DateTime.Now)
            {
                startdateTimePicker1.Value = m_start.Year == 1 ? DateTime.Now : m_start;
                AhDung.MessageTip.ShowWarning("开始时间不能早于当前时间");
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
                AhDung.MessageTip.ShowWarning("记录时间不少于30min");
                return;
            }
            TimeSpan span = TimeSpan.FromSeconds(sec);
            label4.Text = string.Format("总记录时间：{0}", span);
            m_start = startdateTimePicker1.Value;
            m_end = stopdateTimePicker1.Value;
        }

        #endregion

        #region 按钮方法

        /// <summary>
        /// 定时 确定按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConformButton_Click(object sender, EventArgs e)
        {
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("用户点击 定时设置-确定按钮");
            ConformButton.Enabled = CancelButton.Enabled = false;
            if (SetTimeHandler != null)
            {
                IAsyncResult ret = SetTimeHandler.BeginInvoke(startdateTimePicker1.Value, stopdateTimePicker1.Value, null, null);

                bool result = SetTimeHandler.EndInvoke(ret);
                if (result)
                    this.DialogResult = DialogResult.OK;
                else
                {
                    ConformButton.Enabled = CancelButton.Enabled = true;
                }
            }
        }

        /// <summary>
        /// 定时 取消按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("用户点击 定时设置-取消按钮");
            this.Close();
        }

        #endregion

        #region 公有方法

        #endregion

    }
}
