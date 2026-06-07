using AwareTec.Polysmith.Cloud.UI.DataModels;
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
    public partial class AddMarks : UserControl
    {
        #region 私有变量

        private bool m_isEdit = false;
        private string m_eventTypeName = "";
        private IMarker m_perMarker = null;
        private DateTime m_maxDate = default(DateTime);
        private DateTime m_minDate = default(DateTime);

        #endregion

        #region 公有变量

        public bool isRectangle = true;
        public bool ReTimeRange { get; private set; }

        /// <summary>
        /// 传入一个Imark
        /// </summary>
        public IMarker perMarker
        {
            set
            {
                m_perMarker = value;
            }
        }

        /// <summary>
        /// 设置是否为编辑模式
        /// </summary>
        public bool IsEdit
        {
            set
            {
                m_isEdit = value;
            }
        }

        /// <summary>
        /// 通道ID
        /// </summary>
        public string ChannelID = "";

        /// <summary>
        /// 标记开始时间
        /// </summary>
        public DateTime StartTime { set; get; }

        /// <summary>
        /// 标记结束时间
        /// </summary>
        public DateTime EndTime { set; get; }

        public bool Delete = false;

        #endregion

        #region 事件委托

        public delegate bool CreatingMarksDelegate(string ChannelID, IMarker mark, bool israngeTime);
        /// <summary>
        /// 创建一个事件标签事件
        /// </summary>
        public event CreatingMarksDelegate CreatingMarksHandler;

        #endregion

        #region 初始化 load/dispose/init

        /// <summary>
        /// 初始化
        /// </summary>
        public AddMarks()
        {
            InitializeComponent();
            this.Load += AddMarks_Load;
        }
        
        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddMarks_Load(object sender, EventArgs e)
        {
            this.ConformButton.Click += ConformButton_Click;
            this.CancelButton.Click += CancelButton_Click;
            this.DeleteButton.Click += DeleteButton_Click;
            this.EventTypeCombBox.SelectedIndexChanged += EventTypeCombBox_SelectedIndexChanged;

            if (this.isRectangle)
            {
                RectangleMarkers rectangleMarkers = m_perMarker as RectangleMarkers;
                if (rectangleMarkers.ReadOnly)
                {
                    this.EventTypeCombBox.Items.Clear();
                    this.EventTypeCombBox.Items.Add(rectangleMarkers.Name);
                }
                else
                {
                    //searchmark 会自动忽略 只可读属性的事件 
                    List<IMarker> source = MarkerManage.Default.SearchMarkers(this.ChannelID);
                    if (m_perMarker.MarkTyp != IMarker.MarkType.None)
                    {
                        source.RemoveAll(t => t.MarkTyp == IMarker.MarkType.None);
                        Array.Sort(rectangleMarkers.OnChannelIndexs);
                        foreach (IMarker k in source)
                        {
                            Array.Sort((k as RectangleMarkers).OnChannelIndexs);
                            if (string.Join("/", (k as RectangleMarkers).OnChannelIndexs) == string.Join("/", rectangleMarkers.OnChannelIndexs))
                            {
                                this.EventTypeCombBox.Items.Add(k.Name);
                            }
                        }
                    }
                    else
                    {
                        if (rectangleMarkers.Name != "")
                            source.RemoveAll(t => t.MarkTyp != IMarker.MarkType.None);
                        this.EventTypeCombBox.Items.AddRange((from t in source
                                                              select t.Name).ToArray<string>());
                    }
                }
                ConformButton.Enabled = !rectangleMarkers.ReadOnly;
            }
            else
            {
                List<IMarker> stringmarklist = new List<IMarker>();
                for (int i = 0; i < MarkerManage.Default.DefineMarkers.Count; i++)
                {
                    if (MarkerManage.Default.DefineMarkers[i] is StringMarkers)
                    {
                        stringmarklist.Add(MarkerManage.Default.DefineMarkers[i]);
                    }
                }
                //右键直接添加事件
                if (!string.IsNullOrEmpty(m_perMarker.Name))
                {
                    //自定义点位标签 和 系统点位标签不允许互相转换
                    if (m_perMarker.MarkTyp == IMarker.MarkType.None)
                        stringmarklist.RemoveAll(t => t.MarkTyp != IMarker.MarkType.None);
                    else
                        stringmarklist.RemoveAll(t => t.MarkTyp == IMarker.MarkType.None);
                }
                this.EventTypeCombBox.Items.AddRange((from t in stringmarklist
                                                      select t.Name).ToArray<string>());

            }
            if (this.StartTime.Year == 1)
            {
                this.StartTime = DateTime.Now;
            }
            if (this.EndTime.Year == 1)
            {
                this.EndTime = DateTime.Now;
            }
            if (m_maxDate.Year != 1 && m_minDate.Year != 1)
            {
                startdateTimePicker1.MaxDate = m_maxDate;
                startdateTimePicker1.MinDate = m_minDate;
                stopdateTimePicker1.MaxDate = m_maxDate;
                stopdateTimePicker1.MinDate = m_minDate;
            }
            if (!this.isRectangle)
            {
                this.label1.Text = "发生时间";
                this.label2.Visible = false;
                this.stopdateTimePicker1.Visible = false;
                this.startdateTimePicker1.Value = this.StartTime;
            }
            else
            {
                this.startdateTimePicker1.Value = this.StartTime;
                this.stopdateTimePicker1.Value = this.EndTime;
            }
            if (this.m_perMarker.Name != "")
            {
                this.EventTypeCombBox.SelectedItem = this.m_perMarker.Name;
                this.RemarksRichTextBox.Text = this.m_perMarker.Description;
                this.DeleteButton.Visible = true;
            }
            else
            {
                this.EventTypeCombBox.SelectedIndex = 0;
            }
            Rectangle rect = Screen.GetWorkingArea(this);
            int startx = this.ParentForm.Location.X;
            int starty = this.ParentForm.Location.Y;
            if (startx + this.Width > rect.Width)
            {
                startx = rect.Width - this.Width;
            }
            if (starty + this.Height > rect.Bottom)
            {
                starty = rect.Bottom - this.Height;
            }
            this.ParentForm.Location = new Point(startx, starty);

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
                this.ConformButton_Click(null, null);
                return true;
            }
            if (keyData == Keys.Escape)
            {
                this.CancelButton_Click(null, null);
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 事件类型 下拉框 内容改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventTypeCombBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_eventTypeName = EventTypeCombBox.SelectedItem.ToString();
            IMarker find = MarkerManage.Default.DefineMarkers.Find(t => t.Name == m_eventTypeName);
            if (find != null)
                RemarksRichTextBox.Text = find.Description;
        }

        #endregion

        #region 按钮方法

        /// <summary>
        /// 新增事件 删除 按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            Delete = true;
            this.ParentForm.Close();
            this.Dispose();
        }

        /// <summary>
        /// 新增事件 取消 按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
            this.Dispose();
        }

        /// <summary>
        /// 新增事件 确定 按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConformButton_Click(object sender, EventArgs e)
        {
            IMarker marker = MarkerManage.Default.DefineMarkers.Find((IMarker t) => t.Name == this.m_eventTypeName);
            if (this.isRectangle)
            {
                ///如果没有任何修改直接退出
                if (this.StartTime == this.startdateTimePicker1.Value && this.EndTime == this.stopdateTimePicker1.Value && m_perMarker.MarkTyp == marker.MarkTyp && m_isEdit)
                {
                    goto last;
                }
                if (this.StartTime != this.startdateTimePicker1.Value || this.EndTime != this.stopdateTimePicker1.Value)
                {
                    this.ReTimeRange = true;
                }
                //避免了毫秒的误差
                this.StartTime = this.startdateTimePicker1.Value.Millisecond != 0 ? this.startdateTimePicker1.Value.AddMilliseconds((-1) * this.startdateTimePicker1.Value.Millisecond) : this.startdateTimePicker1.Value;
                this.EndTime = this.stopdateTimePicker1.Value.Millisecond != 0 ? this.stopdateTimePicker1.Value.AddMilliseconds((-1) * this.stopdateTimePicker1.Value.Millisecond) : this.stopdateTimePicker1.Value;
                if (this.StartTime > this.EndTime)
                {
                    AhDung.MessageTip.ShowError("事件的开始时间要小于结束时间");
                    return;
                }
                //判断事件的开始和结束时间 要在记录开始和结束时间之内
                if (this.StartTime < this.m_minDate || this.EndTime > this.m_maxDate)
                {
                    AhDung.MessageTip.ShowError("事件的开始和结束时间 要在记录开始和结束时间之内");
                    return;
                }
                RectangleMarkers rectangleMarkers = this.m_perMarker as RectangleMarkers;
                rectangleMarkers.StartTime = this.StartTime;
                rectangleMarkers.EndTime = this.EndTime;
                rectangleMarkers.MarkCreatTime = rectangleMarkers.StartTime;
                rectangleMarkers.MinLimitValue = (marker as RectangleMarkers).MinLimitValue;
            }
            else
            {
                ///如果没有任何修改直接退出
                if (this.StartTime == this.startdateTimePicker1.Value && m_perMarker.MarkTyp == marker.MarkTyp && m_isEdit)
                {
                    goto last;
                }
                if (this.StartTime != this.startdateTimePicker1.Value)
                {
                    this.ReTimeRange = true;
                }
                this.StartTime = this.startdateTimePicker1.Value;
                this.m_perMarker.MarkCreatTime = this.StartTime;
            }
            this.m_perMarker.Name = this.m_eventTypeName;
            this.m_perMarker.Description = marker.Description;
            this.m_perMarker.ForeColor = marker.BackColor;
            this.m_perMarker.MarkTyp = marker.MarkTyp;
            this.m_perMarker.Comments = marker.Comments;
            if (this.CreatingMarksHandler != null)
            {
                this.CreatingMarksHandler(this.ChannelID, this.m_perMarker, this.ReTimeRange);
            }
            last:
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
