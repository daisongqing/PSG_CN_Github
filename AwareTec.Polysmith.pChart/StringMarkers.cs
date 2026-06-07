using System;
using System.Collections.Generic;
using System.Drawing;

namespace AwareTec.Polysmith.pChart
{
    /// <summary>
    /// 文字标记单元
    /// </summary>
    public class StringMarkers : IMarker
    {
        /// <summary>
        /// 内容显示布局
        /// </summary>
        public Layout strLayout = Layout.Bottom;

        public override object Clone()
        {
            StringMarkers mark = new StringMarkers();
            mark.ID = this.ID;
            mark.MarkCreatTime = this.MarkCreatTime;
            mark.MarkTyp = this.MarkTyp;
            mark.Name = this.Name;
            mark.Tag = this.Tag;
            mark.Description = this.Description;
            mark.Comments = this.Comments;
            mark.BackColor = this.BackColor;
            mark.ForeColor = this.ForeColor;
            mark.EndFrameNo = this.EndFrameNo;
            mark.StartFrameNo = this.StartFrameNo;
            mark.HotKey = this.HotKey;
            return mark;
        }
    }
    /// <summary>
    /// 范围标记单元
    /// </summary>
    public class RectangleMarkers : IMarker
    {
        /// <summary>
        /// 起点坐标
        /// </summary>
        public Point StartPoint { set; get; }
        /// <summary>
        /// 结束坐标
        /// </summary>
        public Point EndPoint { set; get; }
        /// <summary>
        /// 开始时刻
        /// </summary>
        public DateTime StartTime { set; get; }
        /// <summary>
        /// 结束时刻
        /// </summary>
        public DateTime EndTime { set; get; }
        /// <summary>
        /// 开始索引号
        /// </summary>
        public int StartIndex { set; get; }
        /// <summary>
        /// 结束索引号
        /// </summary>
        public int EndIndex { set; get; }
        /// <summary>
        /// 最小时域值
        /// </summary>
        public float MinLimitValue { set; get; }
        /// <summary>
        /// 选中区域的点
        /// </summary>
        public List<float> RectanglePoints { set; get; }
        /// <summary>
        /// 只读
        /// </summary>
        public bool ReadOnly { private set; get; }
        /// <summary>
        /// 是否是手动主观删除
        /// </summary>
        public bool DeleteByHand { set; get; }
        private string m_Comments = "";
        public string Comments2 { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public override string Comments
        {
            get
            {
                return m_Comments;
            }
            set
            {
                m_Comments = value;
                string[] ss = m_Comments.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                int len = ss.Length;
                OnChannelIndexs = new int[len];
                for (int i = 0; i < len; i++)
                {
                    OnChannelIndexs[i] = int.Parse(ss[i]);
                }
            }
        }
        /// <summary>
        /// 该事件的作用范围：描述的通道ID
        /// </summary>
        public int[] OnChannelIndexs { get; private set; }

        public RectangleMarkers(bool flag)
        {
            ReadOnly = flag;
        }

        public RectangleMarkers()
        {
            ReadOnly = false;
            DeleteByHand = false;
        }
        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            RectangleMarkers mark = new RectangleMarkers();
            mark.ID = this.ID;
            mark.MarkCreatTime = this.MarkCreatTime;
            mark.MarkTyp = this.MarkTyp;
            mark.Name = this.Name;
            mark.StartIndex = this.StartIndex;
            mark.StartTime = this.StartTime;
            mark.Tag = this.Tag;
            mark.EndIndex = this.EndIndex;
            mark.EndTime = this.EndTime;
            mark.Description = this.Description;
            mark.Comments = this.Comments;
            mark.BackColor = this.BackColor;
            mark.ForeColor = this.ForeColor;
            mark.EndFrameNo = this.EndFrameNo;
            mark.StartFrameNo = this.StartFrameNo;
            mark.HotKey = this.HotKey;
            mark.MinLimitValue = this.MinLimitValue;
            mark.ReadOnly = this.ReadOnly;
            mark.DeleteByHand = this.DeleteByHand;
            return mark;
        }
    }
}
