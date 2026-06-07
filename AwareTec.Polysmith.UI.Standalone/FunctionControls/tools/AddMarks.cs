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
    public partial class AddMarks : UserControl
    {
        public AddMarks()
        {
            InitializeComponent();
            this.Load += AddMarks_Load;
        }
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
        public bool isRectangle = true;
        private void AddMarks_Load(object sender, EventArgs e)
        {
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
                        if ( rectangleMarkers.Name != "")
                            source.RemoveAll(t => t.MarkTyp != IMarker.MarkType.None);
                        this.EventTypeCombBox.Items.AddRange((from t in source
                                                              select t.Name).ToArray<string>());
                    }
                }
                SaveButton.Enabled = !rectangleMarkers.ReadOnly;
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
            if (m_MaxDate.Year != 1 && m_MinDate.Year != 1)
            {
                startdateTimePicker1.MaxDate = m_MaxDate;
                startdateTimePicker1.MinDate = m_MinDate;
                stopdateTimePicker1.MaxDate = m_MaxDate;
                stopdateTimePicker1.MinDate = m_MinDate;
            }
            if (!this.isRectangle)
            {
                this.label1.Text = Program.Language=="EN"? "Occurrence" : "发生时间";
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
        public delegate bool CreatingMarksDelegate(string ChannelID, IMarker mark,bool israngeTime);
        /// <summary>
        /// 创建一个事件标签事件
        /// </summary>
        public event CreatingMarksDelegate CreatingMarksHandler;
        private void CancelButton_Click(object sender, EventArgs e)
        {
            DataModel.LogInstance.Default.AddLog("用户点击 新增事件-取消按钮",pSystem.LogManagement.LogLevel.INFO);
            this.ParentForm.Close();
            this.Dispose();
        }
        public bool ReTimeRange { get; private set; }
        private bool m_isEdit = false;
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
        private string strName = "";
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            IMarker marker = MarkerManage.Default.DefineMarkers.Find((IMarker t) => t.Name == this.strName);
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
                    AhDung.MessageTip.ShowError(Program.Language == "EN" ? "The start time of the event should be less than the end time" : "事件的开始时间要小于结束时间");
                    return;
                }
                //判断事件的开始和结束时间 要在记录开始和结束时间之内
                if (this.StartTime < this.m_MinDate || this.EndTime > this.m_MaxDate)
                {
                    AhDung.MessageTip.ShowError(Program.Language == "EN" ? "The start and end times of the event should be within the recorded start and end times" : "事件的开始和结束时间 要在记录开始和结束时间之内");
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
                //点位标签修改时间
                this.m_perMarker.StartFrameNo = (int)(this.StartTime - this.m_MinDate).TotalSeconds / 30;
            }
            this.m_perMarker.Name = this.strName;
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
        private IMarker m_perMarker = null;
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
        /// 标记开始时间
        /// </summary>
        public DateTime StartTime { set; get; }
        /// <summary>
        /// 标记结束时间
        /// </summary>
        public DateTime EndTime { set; get; }
        private DateTime m_MaxDate = default(DateTime);

        private DateTime m_MinDate = default(DateTime);
        /// <summary>
        /// 设置最大/小日期限制
        /// </summary>
        /// <param name="maxTime"></param>
        /// <param name="minTime"></param>
        public void SetMaxMinDate(DateTime maxTime,DateTime minTime)
        {
            m_MaxDate = maxTime.AddSeconds(1);
            m_MinDate = minTime;
        }
        private void EventTypeCombBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            strName = EventTypeCombBox.SelectedItem.ToString();
            IMarker find = DataModel.MarkerManage.Default.DefineMarkers.Find(t => t.Name == strName);
            if (find != null)
                RemarksRichTextBox.Text = find.Description;
        }
        public bool Delete = false;

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DataModel.LogInstance.Default.AddLog("用户点击 新增事件-删除按钮", pSystem.LogManagement.LogLevel.INFO);
            Delete = true;
            this.ParentForm.Close();
            this.Dispose();
        }
        public delegate void DeletingMarksDelegate(string ChannelID, IMarker mark);
        /// <summary>
        /// 删除一个事件标签事件
        /// </summary>
        public event DeletingMarksDelegate DeletingMarksHandler;
    }
}
