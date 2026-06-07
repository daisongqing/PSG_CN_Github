using System;
using System.Drawing;

namespace AwareTec.Polysmith.pChart
{
    /// <summary>
    /// 标记基类
    /// </summary>
    public abstract class IMarker : ICloneable
    {
        public enum MarkType
        {
            /// <summary>
            /// 中枢型呼吸暂停
            /// </summary>
            CA = 0,
            /// <summary>
            /// 阻塞型呼吸暂停
            /// </summary>
            OA = 1,
            /// <summary>
            /// 混合型呼吸暂停
            /// </summary>
            MA = 2,
            /// <summary>
            /// 低通气
            /// </summary>
            Hypopnea = 3,
            /// <summary>
            /// 体动
            /// </summary>
            MT = 4,
            /// <summary>
            /// 睡眠
            /// </summary>
            Stage = 5,
            /// <summary>
            /// 氧减
            /// </summary>
            OxygenReduce = 6,
            /// <summary>
            /// 自发性微觉醒
            /// </summary>
            Arousal = 7,
            /// <summary>
            /// 腿动
            /// </summary>
            LegMove = 8,
            /// <summary>
            /// 打鼾
            /// </summary>
            Snore = 9,
            /// <summary>
            /// 不能需要分析
            /// </summary>
            AnlysisEnable=80,
            /// <summary>
            /// 无
            /// </summary>
            None = 99,
            /// <summary>
            /// 腿动事件微觉醒
            /// </summary>
            LmArousal = 12,
            /// <summary>
            /// 周期性腿动相关觉醒
            /// </summary>
            PlmArousal = 13,
            /// <summary>
            /// 呼吸事件相关觉醒
            /// </summary>
            RespArousal = 14,
            /// <summary>
            /// 周期性循环腿动
            /// </summary>
            PeriodicalBodyMove = 11,
            /// <summary>
            /// 陈施氏呼吸事件
            /// </summary>
            CheyneStokes = 20,
            /// <summary>
            /// 血氧伪迹
            /// </summary>
            Spo2Artifact = 21,
            /// <summary>
            /// 多次小睡
            /// </summary>
            MultipleSleep = 22,
            /// <summary>
            /// 磨牙
            /// </summary>
            Molar=83,
            /// <summary>
            /// 眨眼
            /// </summary>
            Wink = 70,
            /// <summary>
            /// α节律
            /// </summary>
            AlphaRhythm = 71,
            /// <summary>
            /// 纺锤波
            /// </summary>
            Spindles = 72,
            /// <summary>
            /// K复合波
            /// </summary>
            Kcomplex = 73,
            /// <summary>
            /// 开灯事件
            /// </summary>
            LightOn = 100,
            /// <summary>
            /// 关灯事假
            /// </summary>
            LightOff = 101
        }
        /// <summary>
        /// 标记的内容显示布局
        /// </summary>
        public enum Layout
        {
            /// <summary>
            /// 从上到下
            /// </summary>
            Top = 0,
            /// <summary>
            /// 从左到右
            /// </summary>
            Left = 1,
            /// <summary>
            /// 从右到左
            /// </summary>
            Right = 2,
            /// <summary>
            /// 从下到上
            /// </summary>
            Bottom
        }
        private string m_ID = "";
        /// <summary>
        /// 标记的唯一ID
        /// </summary>
        public string ID
        {
            set
            {
                m_ID = value;
            }
            get
            {
                return m_ID;
            }
        }
        /// <summary>
        /// 标记发生时间
        /// </summary>
        public DateTime MarkCreatTime { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public MarkType MarkTyp = MarkType.None;
        /// <summary>
        /// 事件编号
        /// </summary>
        public string EventID { set; get; }
        private string m_Name = "";
        /// <summary>
        /// 标记的名称
        /// </summary>
        public string Name
        {
            set
            {
                m_Name = value;
            }
            get
            {
                return m_Name;
            }
        }
        private string m_Description = "";
        /// <summary>
        /// 标记的描述
        /// </summary>
        public string Description
        {
            set
            {
                m_Description = value;
            }
            get
            {
                if (string.IsNullOrEmpty(m_Description))
                    m_Description = m_Name;
                return m_Description;
            }
        }

        private Font m_Font = new Font("宋体", 9.0f);
        /// <summary>
        /// 字体样式
        /// </summary>
        public Font Font
        {
            set
            {
                m_Font = value;
            }
            get
            {
                return m_Font;
            }
        }
        public bool isSelected = false;
        private Point m_Location = new Point(0, 0);
        /// <summary>
        /// 坐标位置
        /// </summary>
        public Point Location
        {
            set
            {
                m_Location = value;
            }
            get
            {
                return m_Location;
            }
        }
        /// <summary>
        /// 文字显示区域
        /// </summary>
        public RectangleF HeadRectangle
        {
            set;
            get;
        }
        /// <summary>
        /// 是否要被删除
        /// </summary>
        public bool Delete = false;
        /// <summary>
        /// 默认背景色为透明色
        /// </summary>
        private Color m_BackColor = Color.DarkCyan;
        /// <summary>
        /// 获取或设置背景色
        /// </summary>
        public Color BackColor { set { m_BackColor = value; } get { return m_BackColor; } }
        /// <summary>
        /// 默认前景色为黑
        /// </summary>
        private Color m_ForeColor = Color.Black;
        /// <summary>
        /// 获取或设置前景色
        /// </summary>
        public Color ForeColor { set { m_ForeColor = value; } get { return m_ForeColor; } }
        /// <summary>
        /// 是否显示边框
        /// </summary>
        public bool BodyLine = false;
        /// <summary>
        /// Tag
        /// </summary>
        public object Tag { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Comments { set; get; }
        /// <summary>
        /// 当前区域开始绘制的索引号
        /// </summary>
        public int CurrentStartIndex { set; get; }
        /// <summary>
        /// 当前区域终止绘制的索引号
        /// </summary>
        public int CurrentEndIndex { set; get; }
        /// <summary>
        /// 发生时所在的帧序号
        /// </summary>
        public int StartFrameNo { set; get; }
        /// <summary>
        /// 结束时所在的帧序号
        /// </summary>
        public int EndFrameNo { set; get; }
        /// <summary>
        /// 当前区域是否需要绘制标记
        /// </summary>
        public bool CurrentHasMark = false;
        /// <summary>
        /// 热键
        /// </summary>
        public string HotKey { set; get; }
        /// <summary>
        /// 可允许显示的通道集合
        /// </summary>
        public int[] AllowChannels { set; get; }
        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public virtual object Clone()
        {
            return new object();
        }
        #region 云平台事件管理所需要额外增加的属性
        /// <summary>
        /// 云平台事件管理所需要额外增加的属性 id
        /// </summary>
        public string Cloudid { set; get; }
        /// <summary>
        /// 云平台事件管理所需要额外增加的属性 userid
        /// </summary>
        public string ClouduserId { set; get; }
        /// <summary>
        /// 云平台事件管理所需要额外增加的属性 用户的版本 儿童版还是成人版
        /// </summary>
        public int Cloudmode { set; get; }
        /// <summary>
        /// 云平台事件管理所需要额外增加的属性 事件的flag如果等于2 为true 类似眨眼
        /// </summary>
        public bool CloudisReadOnly { set; get; }

        #endregion
    }
}
