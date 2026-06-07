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
    public partial class ProgressTipForm : SkinForm
    {
        private static ProgressTipForm m_default = null;

        public static ProgressTipForm Defalut
        {
            get
            {
                return m_default ?? (m_default = new ProgressTipForm());
            }
        }
        public ProgressTipForm()
        {
            InitializeComponent();
            DefaultStatusText = Program.Language=="EN"? "Please wait a moment..." : "请稍后...";
            CancellingText = Program.Language == "EN" ? "Cancelling in progress..." : "取消中...";
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new System.ComponentModel.DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            this.Load += ProgressTipForm_Load;
            this.FormClosed += ProgressTipForm_FormClosed;
        }

        private void ProgressTipForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            CancelButton.Text = Program.Language == "EN" ? "Cancel": "取消";
            this.Dispose();
            m_default = null;
        }

        private void ProgressTipForm_Load(object sender, EventArgs e)
        {
            DataModel.EDF.Default.Interrupt = false;
            //reset to defaults just in case the user wants to reuse the form
            Result = null;
            ActionProgressBar.Value = ActionProgressBar.Minimum;
            lastStatus = DefaultStatusText;
            TipMessageLabel.Text = DefaultStatusText;
            lastPercent = ActionProgressBar.Minimum;
            //start the background worker as soon as the form is loaded
            worker.RunWorkerAsync(Argument);
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var flag = AddTask(sender, e);
            DataModel.LogInstance.Default.AddLog("[进度条]是否能够执行进度条DoWork:" + (flag ? "是" : "否"));
            if (flag)
            {
                //the background worker started
                //let's call the user's event handler
                DataModel.LogInstance.Default.AddLog("[进度条]是否绑定了进度条DoWork事件:" + ((m_dowork != null) ? "是" : "否"));
                if (m_dowork != null)
                {
                    m_dowork(this, e);
                }
            }
            AddTaskHandler = null;//清空附加任务
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //make sure the new value is valid for the progress bar and update it
            if (e.ProgressPercentage >= ActionProgressBar.Minimum && e.ProgressPercentage <= ActionProgressBar.Maximum)
                ActionProgressBar.Value = e.ProgressPercentage;
            //do not update the text if a cancellation request is pending
            if (e.UserState != null && !worker.CancellationPending && !haserror)
            {
                TipMessageLabel.Text = e.UserState.ToString();
                TipMessageLabel.ForeColor = Color.Black;
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //the background worker completed
            //keep the resul and close the form
            Result = e;
            if (!haserror)
            {
                if (e.Error != null)
                {
                    DataModel.LogInstance.Default.AddLog("进度条错误信息:\n" + e.Error.Message + "堆栈信息:" + e.Error.StackTrace, pSystem.LogManagement.LogLevel.ERROR);
                    DialogResult = DialogResult.Abort;
                }
                else if (e.Cancelled)
                    DialogResult = DialogResult.Cancel;
                else
                    DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                CancelButton.Text = Program.Language == "EN" ? "OK" : "确定";
                haserror = false;
            }
        }
        int lastPercent;
        string lastStatus;
        /// <summary>
        /// Changes the status text only.
        /// </summary>
        /// <param name="status">New status text.</param>
        public void SetProgress(string status)
        {
            //do not update the text if it didn't change
            //or if a cancellation request is pending
            if (status != lastStatus && !worker.CancellationPending && !haserror)
            {
                lastStatus = status;
                worker.ReportProgress(ActionProgressBar.Minimum - 1, status);
            }
        }
        /// <summary>
        /// Changes the progress bar value only.
        /// </summary>
        /// <param name="percent">New value for the progress bar.</param>
        public void SetProgress(int percent)
        {
            //do not update the progress bar if the value didn't change
            if (percent != lastPercent && !haserror)
            {
                lastPercent = percent;
                worker.ReportProgress(percent);
            }
        }
        /// <summary>
        /// Changes both progress bar value and status text.
        /// </summary>
        /// <param name="percent">New value for the progress bar.</param>
        /// <param name="status">New status text.</param>
        public void SetProgress(int percent, string status)
        {
            //update the form is at least one of the values need to be updated
            if (percent != lastPercent || (status != lastStatus && !worker.CancellationPending) && !haserror)
            {
                lastPercent = percent;
                lastStatus = status;
                worker.ReportProgress(percent, status);
                DataModel.LogInstance.Default.AddLog(string.Format("执行进度:{0}->{1}", percent, status), pSystem.LogManagement.LogLevel.DEBUG);
            }
        }
        bool haserror = false;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errmsg"></param>
        public void SetError(string errmsg)
        {
            if (this.InvokeRequired)
            {
                while (!this.IsHandleCreated)
                {
                    if (this.Disposing || this.IsDisposed)
                    {
                        return;
                    }
                }
                this.Invoke(new MethodInvoker(() =>
                {
                    SetError(errmsg);
                }));
            }
            else
            {
                TipMessageLabel.Text = errmsg;
                TipMessageLabel.ForeColor = Color.Red;
                ActionLabel.Visible = false;
                haserror = true;
                DataModel.LogInstance.Default.AddLog(errmsg, pSystem.LogManagement.LogLevel.ERROR);
            }
        }
        /// <summary>
        /// Gets the progress bar so it is possible to customize it
        /// before displaying the form.
        /// Do not use it directly from the background worker function!
        /// </summary>
        public pSystem.UI.ReaLTaiizor.Controls.DungeonProgressBar ProgressBar { get { return ActionProgressBar; } }
        /// <summary>
        /// Will be passed to the background worker.
        /// </summary>
        public object Argument { get; set; }
        /// <summary>
        /// Tag
        /// </summary>
        public static object ResultTag { get; set; }
        /// <summary>
        /// Background worker's result.
        /// You may also check ShowDialog return value
        /// to know how the background worker finished.
        /// </summary>
        public RunWorkerCompletedEventArgs Result { get; private set; }
        /// <summary>
        /// True if the user clicked the Cancel button
        /// and the background worker is still running.
        /// </summary>
        public bool CancellationPending
        {
            get { return worker.CancellationPending; }
        }
        /// <summary>
        /// Text displayed once the Cancel button is clicked.
        /// </summary>
        public string CancellingText { get; set; }
        /// <summary>
        /// Default status text.
        /// </summary>
        public string DefaultStatusText { get; set; }

        /// <summary>
        /// Delegate for the DoWork event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Contains the event data.</param>
        public delegate void DoWorkEventHandler(ProgressTipForm sender, DoWorkEventArgs e);
        private event DoWorkEventHandler m_dowork;
        /// <summary>
        /// Occurs when the background worker starts.
        /// </summary>
        public event DoWorkEventHandler DoWork
        {
            add
            {
                if (m_dowork != null)
                    m_dowork = null;
                m_dowork = value;
            }
            remove
            {
                m_dowork = null;
            }
        }
        /// <summary>
        /// 取消时发生
        /// </summary>
        public delegate void CanceledDelegate();
        private event CanceledDelegate m_CanceledHandle;
        /// <summary>
        /// 取消时发生
        /// </summary>
        public event CanceledDelegate CanceledHandle
        {
            add
            {
                if (this.m_CanceledHandle != null)
                    this.m_CanceledHandle = null;
                this.m_CanceledHandle = value;
            }
            remove
            {
                if (this.m_CanceledHandle == null)
                    return;
                this.m_CanceledHandle = null;
            }
        }
        public delegate bool AddTaskDelegate(object sender, DoWorkEventArgs e);
        /// <summary>
        /// 附加任务
        /// </summary>
        public event AddTaskDelegate AddTaskHandler;

        private bool AddTask(object sender, DoWorkEventArgs e)
        {
            if (AddTaskHandler != null)
                return AddTaskHandler.Invoke(sender, e);
            return true;
        }
        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (CancelButton.Text == (Program.Language == "EN" ? "OK" : "确定"))
            {
                DataModel.LogInstance.Default.AddLog("用户在进度条等待时间点击确定按钮");
                lastStatus = "";
                lastPercent = 0;
                TipMessageLabel.Text = CancellingText;
                this.Close();
                CancelButton.Text = Program.Language == "EN" ? "Cancel" : "取消";
                ActionLabel.Visible = true;
            }
            else
            {
                DataModel.LogInstance.Default.AddLog("用户在进度条等待时间点击取消按钮");
                //notify the background worker we want to cancel
                DataModel.EDF.Default.Interrupt = true;
                if (m_CanceledHandle != null)
                    m_CanceledHandle.BeginInvoke(null, null);
                worker.CancelAsync();
                TipMessageLabel.Text = CancellingText;
                worker.Dispose();
                this.Close();
            }
            haserror = false;
        }
    }
}
