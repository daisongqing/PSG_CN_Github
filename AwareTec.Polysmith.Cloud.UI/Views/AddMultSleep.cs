using AwareTec.Polysmith.pChart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.Cloud.UI.Views
{
    public partial class AddMultSleep : UserControl
    {
        #region 私有变量

        DotNetCtl.ErrorProviderHelper m_errorProvider;
        private bool m_needShowWarning = true;
        private bool m_isEdit = false;
        private bool m_needAdd = true;
        private DateTime m_maxDate = default(DateTime);
        private DateTime m_minDate = default(DateTime);

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

        /// <summary>
        /// 存放小睡事件列表
        /// </summary>
        public List<IMarker> MultipleSleepMarks { set; get; }

        /// <summary>
        /// 当前小睡事件
        /// </summary>
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

        #region 事件委托

        public delegate void CreatingSleepDelegate(List<IMarker> MultipleSleepMarks);
        /// <summary>
        /// 创建或修改小睡事件
        /// </summary>
        public event CreatingSleepDelegate CreatingSleepHandler;

        #endregion

        #region 初始化 load/dispose/init

        /// <summary>
        /// 初始化
        /// </summary>
        public AddMultSleep()
        {
            InitializeComponent();
            this.Load += AddMultSleep_Load;
        }

        /// <summary>
        /// 初始化加载 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddMultSleep_Load(object sender, EventArgs e)
        {
            this.ConformButton.Click += ConformButton_Click;
            this.CancelButton.Click += CancelButton_Click;
            this.startdateTimePicker1.ValueChanged += StartdateTimePicker1_ValueChanged;
            this.stopdateTimePicker1.ValueChanged += StopdateTimePicker1_ValueChanged;

            m_errorProvider = new DotNetCtl.ErrorProviderHelper(this);
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

        #region 私有方法

        /// <summary>
        /// 结束时间 值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopdateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (stopdateTimePicker1.Value < m_minDate || stopdateTimePicker1.Value > m_maxDate)
            {
                AhDung.MessageTip.ShowWarning("事件的结束时间要在记录时间范围内,请确认日期选择正确");
                m_errorProvider.ShowError(stopdateTimePicker1, "时间错误");
                m_needShowWarning = false;
            }
            else
            {
                m_needShowWarning = true;
                m_errorProvider.Clear();
            }
        }

        /// <summary>
        /// 开始时间 值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartdateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (startdateTimePicker1.Value < m_minDate || startdateTimePicker1.Value > m_maxDate)
            {
                AhDung.MessageTip.ShowWarning("事件的开始时间要在记录时间范围内,请确认日期选择正确");
                m_errorProvider.ShowError(startdateTimePicker1, "时间错误");
                m_needShowWarning = false;
            }
            else
            {
                m_needShowWarning = true;
                m_errorProvider.Clear();
            }
        }

        #endregion

        #region 按钮方法

        /// <summary>
        /// 新增小睡 取消按钮 点击
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
        /// 新增小睡 确定按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConformButton_Click(object sender, EventArgs e)
        {
            this.StartTime = this.startdateTimePicker1.Value;
            this.EndTime = this.stopdateTimePicker1.Value;
            if (m_needShowWarning == false)
            {
                m_needShowWarning = true;
                return;
            }
            if (this.StartTime > this.EndTime)
            {
                AhDung.MessageTip.ShowWarning("事件的开始时间要小于结束时间,请确认日期选择正确");
                return;
            }
            //判断事件的开始和结束时间 要在记录开始和结束时间之内
            if (this.StartTime < this.m_minDate || this.EndTime > this.m_maxDate)
            {
                AhDung.MessageTip.ShowWarning("事件的开始和结束时间 要在记录开始和结束时间之内");
                return;
            }
            if ((this.EndTime - this.StartTime).TotalSeconds < 600)
            {
                AhDung.MessageTip.ShowWarning("小睡时间最少10分钟");
                return;
            }
            SleepMark.StartTime = this.StartTime;
            SleepMark.EndTime = this.EndTime;
            SleepMark.StartIndex = (int)(this.StartTime - this.m_minDate).TotalSeconds / 30;
            SleepMark.EndIndex = (int)(this.EndTime - this.m_minDate).TotalSeconds / 30;
            //做预防
            if (Math.Abs((this.StartTime - this.m_minDate).TotalSeconds - SleepMark.StartIndex * 30) > 300 || Math.Abs((this.EndTime - this.m_minDate).TotalSeconds - SleepMark.EndIndex * 30) > 300)
                SleepMark.Comments2 = "0-0";
            else
                SleepMark.Comments2 = string.Format("{0}-{1}", (this.StartTime - this.m_minDate).TotalSeconds - SleepMark.StartIndex * 30, (this.EndTime - this.m_minDate).TotalSeconds - SleepMark.EndIndex * 30);
            if (this.IsEdit)
            {
                MultipleSleepMarks.RemoveAll(t =>
                {
                    RectangleMarkers rec = t as RectangleMarkers;
                    return SleepMark.StartIndex <= rec.EndIndex && SleepMark.EndIndex >= rec.StartIndex && t != SleepMark;
                });
                m_needAdd = false;
            }
            else
                MultipleSleepMarks.RemoveAll(t => (t as RectangleMarkers).EndTime >= SleepMark.StartTime && (t as RectangleMarkers).StartTime <= SleepMark.EndTime);
            for (int i = 0; i < MultipleSleepMarks.Count; i++)
            {
                RectangleMarkers rec = MultipleSleepMarks[i] as RectangleMarkers;
                if (SleepMark.StartIndex < rec.StartIndex && m_needAdd)
                {
                    MultipleSleepMarks.Insert(i, SleepMark);
                    m_needAdd = false;
                    break;
                }
            }
            if (m_needAdd)
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

        #endregion

        #region 公有方法

        /// <summary>
        /// 设置最大/小日期限制
        /// </summary>
        /// <param name="maxTime"></param>
        /// <param name="minTime"></param>
        public void SetMaxMinDate(DateTime maxTime, DateTime minTime)
        {
            m_maxDate = maxTime.AddSeconds(1);
            m_minDate = minTime;
        }

        #endregion

    }
}
