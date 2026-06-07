using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AwareTec.Polysmith.pChart;
using AwareTec.Polysmith.UI.DataModel;

namespace AwareTec.Polysmith.UI.FunctionControls.tools
{
    public partial class AddMultSleep : UserControl
    {
        #region 私有变量
        DotNetCtl.ErrorProviderHelper m_errorProvider;
        private bool needshowwarning = true;
        private bool m_isEdit = false;
        private bool add = true;
        private DateTime m_MaxDate = default(DateTime);
        private DateTime m_MinDate = default(DateTime);
        public delegate void CreatingSleepDelegate(List<IMarker> MultipleSleepMarks);
        public event CreatingSleepDelegate CreatingSleepHandler;
        #endregion
        #region 公有变量
        /// <summary>
        /// 设置是否为编辑模式
        /// </summary>
        public bool IsEdit
        {
            set
            {
                m_isEdit = value;
            }
            get
            {
                return m_isEdit;
            }
        }
        //存放小睡事件列表
        public List<IMarker> MultipleSleepMarks { set; get; }
        //当前小睡事件
        public RectangleMarkers SleepMark { set; get; }
        /// <summary>
        /// 标记开始时间
        /// </summary>
        public DateTime StartTime { set; get; }
        /// <summary>
        /// 标记结束时间
        /// </summary>
        public DateTime EndTime { set; get; }
        #endregion
        #region 初始化
        public AddMultSleep()
        {
            InitializeComponent();
            m_errorProvider = new DotNetCtl.ErrorProviderHelper(this);
            this.Load += AddMultSleep_Load;
            this.startdateTimePicker1.ValueChanged += StartdateTimePicker1_ValueChanged;
            this.stopdateTimePicker1.ValueChanged += StopdateTimePicker1_ValueChanged;
        }
        private void AddMultSleep_Load(object sender, EventArgs e)
        {
            if (SleepMark == null)
            {
                return;
            }
            if (m_isEdit)
            {
                CancelButton.Visible = false;
            }
            if (SleepMark.StartTime > SleepMark.EndTime)
            {
                startdateTimePicker1.Value = SleepMark.EndTime;
                stopdateTimePicker1.Value = SleepMark.StartTime;
            }
            else
            {
                startdateTimePicker1.Value = SleepMark.StartTime;
                stopdateTimePicker1.Value = SleepMark.EndTime;
            }
            Rectangle rect = Screen.GetWorkingArea(this);
            int startx = this.ParentForm.Location.X;
            int starty = this.ParentForm.Location.Y;
            if (startx + this.Width > rect.Width)
            {
                startx = rect.Width - this.Width;
            }
            if (starty + this.Height > rect.Height)
            {
                starty = rect.Bottom - this.Height;
            }
            this.ParentForm.Location = new Point(startx, starty);
        }
        #endregion
        #region 日期判断
        /// <summary>
        /// 设置最大/小日期限制
        /// </summary>
        /// <param name="maxTime"></param>
        /// <param name="minTime"></param>
        public void SetMaxMinDate(DateTime maxTime, DateTime minTime)
        {
            m_MaxDate = maxTime.AddSeconds(1);
            m_MinDate = minTime;
        }
        private void StopdateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (stopdateTimePicker1.Value < m_MinDate || stopdateTimePicker1.Value > m_MaxDate)
            {
                AhDung.MessageTip.ShowWarning("事件的结束时间要在记录时间范围内,请确认日期选择正确");
                m_errorProvider.ShowError(stopdateTimePicker1, "时间错误");
                needshowwarning = false;
            }
            else
            {
                needshowwarning = true;
                m_errorProvider.Clear();
            }
        }

        private void StartdateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (startdateTimePicker1.Value < m_MinDate || startdateTimePicker1.Value > m_MaxDate)
            {
                AhDung.MessageTip.ShowWarning("事件的开始时间要在记录时间范围内,请确认日期选择正确");
                m_errorProvider.ShowError(startdateTimePicker1, "时间错误");
                needshowwarning = false;
            }
            else
            {
                needshowwarning = true;
                m_errorProvider.Clear();
            }
        }
        #endregion
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (this.IsEdit)
            {

            }
            else
            {
                MultipleSleepMarks.Remove(SleepMark);
                if (this.CreatingSleepHandler != null)
                {
                    this.CreatingSleepHandler.Invoke(MultipleSleepMarks);
                }
            }
            this.ParentForm.Close();
            this.Dispose();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            this.StartTime = this.startdateTimePicker1.Value;
            this.EndTime = this.stopdateTimePicker1.Value;
            if (needshowwarning == false)
            {
                needshowwarning = true;
                return;
            }
            if (this.StartTime > this.EndTime)
            {
                AhDung.MessageTip.ShowWarning("事件的开始时间要小于结束时间,请确认日期选择正确");
                return;
            }
            //判断事件的开始和结束时间 要在记录开始和结束时间之内
            if (this.StartTime < this.m_MinDate || this.EndTime > this.m_MaxDate)
            {
                AhDung.MessageTip.ShowWarning("事件的开始和结束时间 要在记录开始和结束时间之内");
                return;
            }
            if ((this.EndTime-this.StartTime).TotalSeconds < 600)
            {
                AhDung.MessageTip.ShowWarning("小睡时间最少10分钟");
                return;
            }
            SleepMark.StartTime = this.StartTime;
            SleepMark.EndTime = this.EndTime;
            SleepMark.StartIndex = (int)(this.StartTime - this.m_MinDate).TotalSeconds / 30;
            SleepMark.EndIndex = (int)(this.EndTime - this.m_MinDate).TotalSeconds / 30;
            //做预防
            if (Math.Abs((this.StartTime - this.m_MinDate).TotalSeconds - SleepMark.StartIndex * 30)>300 || Math.Abs((this.EndTime - this.m_MinDate).TotalSeconds-SleepMark.EndIndex * 30)>300)
                SleepMark.Comments2 = "0-0";
            else
                SleepMark.Comments2 = string.Format("{0}-{1}", (this.StartTime - this.m_MinDate).TotalSeconds - SleepMark.StartIndex * 30, (this.EndTime - this.m_MinDate).TotalSeconds - SleepMark.EndIndex * 30);
            if (this.IsEdit)
            {
                MultipleSleepMarks.RemoveAll(t =>
                {
                    RectangleMarkers rec = t as RectangleMarkers;
                    return SleepMark.StartIndex <= rec.EndIndex && SleepMark.EndIndex >= rec.StartIndex && t != SleepMark;
                });
                add = false;
            }
            else
                MultipleSleepMarks.RemoveAll(t => (t as RectangleMarkers).EndTime >= SleepMark.StartTime && (t as RectangleMarkers).StartTime <= SleepMark.EndTime);
            for (int i = 0; i < MultipleSleepMarks.Count; i++)
            {
                RectangleMarkers rec = MultipleSleepMarks[i] as RectangleMarkers;
                if (SleepMark.StartIndex < rec.StartIndex&& add)
                {
                    MultipleSleepMarks.Insert(i, SleepMark);
                    add = false;
                    break;
                }
            }
            if (add)
            {
                MultipleSleepMarks.Add(SleepMark);
            }
            if (this.CreatingSleepHandler != null)
            {
                this.CreatingSleepHandler.Invoke(MultipleSleepMarks);
            }
            base.ParentForm.DialogResult = DialogResult.OK;
            base.ParentForm.Close();
            base.Dispose();
        }

        /// <summary>
        /// 键盘快捷键
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Return)
            {
                this.SaveButton_Click(null, null);
                return true;
            }
            if (keyData == Keys.Escape)
            {
                this.CancelButton_Click(null, null);
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}
