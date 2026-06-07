using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using AwareTec.Polysmith.pChart;

namespace AwareTec.Polysmith.UI.FunctionControls.tools
{
    public partial class HScrollBarEx : UserControl
    {
        private DateTime m_EndTime = DateTime.Now;
        private Rectangle m_RectPanel = Rectangle.Empty;
        private bool m_mouseIsDown;
        private int rectX = 27;
        private int rectY = 20;
        public HScrollBarEx()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer
     | ControlStyles.ResizeRedraw
     | ControlStyles.Selectable
     | ControlStyles.AllPaintingInWmPaint
     | ControlStyles.UserPaint
     | ControlStyles.SupportsTransparentBackColor,
true);
            this.UpdateStyles();
            this.Load += HScrollBarEx_Load;
        }
        /// <summary>
        /// 防止拖拽的时候出现卡顿
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = 0x02000000;////用双缓冲绘制窗口的所有子控件
                return cp;
            }
        }
        private void HScrollBarEx_Load(object sender, EventArgs e)
        {
            this.panel1.Paint += this.panel1_Paint;
            this.panel1.SizeChanged += this.panel1_SizeChanged;
            this.hScrollBar1.ValueChanged += this.hScrollBar1_ValueChanged;
            this.panel1.MouseDown += this.Panel1_MouseDown;
            this.panel1.MouseMove += this.panel1_MouseMove;
            this.panel1.MouseUp += this.panel1_MouseUp;
            this.m_RectPanel = new Rectangle(this.rectX, this.rectY, this.panel1.Width - 2 * this.rectX, this.panel1.Height - 2 * this.rectY);
        }
        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            int currentLocationx = e.X;
            //不允许点击到进度条之外的地方 点击直接return
            if (currentLocationx < this.rectX)
            {
                currentLocationx = this.rectX;
            }
            else if (currentLocationx > this.panel1.Width - this.rectX)
            {
                currentLocationx = this.panel1.Width - this.rectX;
            }
            this.m_mouseIsDown = true;
            this.panel1.Capture = true;
            this.currentValue = (int)Math.Round((float)(currentLocationx - this.rectX) / this.m_ValueRate, 0) + 1;
            this.panel1.Invalidate();
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.m_mouseIsDown)
            {
                this.m_mouseIsDown = false;
                this.panel1.Capture = false;
                if (this.ValueChanged != null)
                {
                    this.ValueChanged.BeginInvoke(this.currentValue, false, new AsyncCallback((t) =>
                    {
                        base.Invoke(new MethodInvoker(delegate ()
                        {
                            changeReady = true;
                            if (currentValue > hScrollBar1.Maximum)
                                currentValue = hScrollBar1.Maximum;
                            else if (currentValue < hScrollBar1.Minimum)
                                currentValue = hScrollBar1.Minimum;
                            this.hScrollBar1.Value = currentValue;
                        }));
                    }), null);
                }

            }
        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.m_mouseIsDown)
            {
                float num = (float)e.X;
                if (num < (float)this.rectX)
                {
                    num = (float)this.rectX;
                }
                else if (num > (float)this.m_RectPanel.Right)
                {
                    num = (float)this.m_RectPanel.Right;
                }
                this.currentValue = (int)Math.Round((num - (float)this.rectX) / this.m_ValueRate, 0) + 1;
                this.panel1.Invalidate();
            }
        }
        private bool m_valuechangeEnable = true;
        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            if (!this.m_valuechangeEnable)
            {
                this.m_valuechangeEnable = true;
                return;
            }
            if (!this.changeReady)
            {
                if (this.ValueChanged != null && this.m_valueChangeEanble)
                {
                    bool auto = m_AutoJump;
                    this.ValueChanged.BeginInvoke(this.currentValue, auto, null, null);
                    m_AutoJump = true;
                }
                this.panel1.Invalidate();
                Console.WriteLine("滚动条发生值变化");
            }
            this.changeReady = false;
            this.m_valueChangeEanble = true;
        }

        private void panel1_SizeChanged(object sender, EventArgs e)
        {
            this.m_RectPanel = new Rectangle(this.rectX, this.rectY, this.panel1.Width - 2 * this.rectX, this.panel1.Height - 2 * this.rectY);
            this.panel1.Invalidate();
        }
        private bool finish = false;
        private int currentValue = 1;
        private bool changeReady = false;
        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (this.finish && e.Type != ScrollEventType.EndScroll)
            {
                this.finish = false;
                if (e.Type == ScrollEventType.SmallIncrement)
                {
                    this.m_addValue += this.m_moveOffValue;
                    if (this.m_addValue >= this.m_MaxFrameOffSetValue)
                    {
                        this.m_addValue -= this.m_MaxFrameOffSetValue;
                        this.CurrentFrameCnt = this.currentValue + 1;
                        return;
                    }
                    if (this.ValueChanged != null)
                    {
                        this.ValueChanged.BeginInvoke(this.currentValue, false, null, null);
                    }
                    this.finish = true;
                    return;
                }
                else if (e.Type == ScrollEventType.SmallDecrement)
                {
                    this.m_addValue -= this.m_moveOffValue;
                    if (this.m_addValue < 0)
                    {
                        this.m_addValue = this.m_MaxFrameOffSetValue + this.m_addValue;
                        this.CurrentFrameCnt = this.currentValue - 1;
                        return;
                    }
                    if (this.ValueChanged != null)
                    {
                        this.ValueChanged.BeginInvoke(this.currentValue, false, null, null);
                    }
                    this.finish = true;
                    return;
                }
                else if (this.currentValue != e.NewValue)
                {
                    this.m_addValue = 0;
                    this.currentValue = e.NewValue;
                    if (this.ValueChanged != null)
                    {
                        this.ValueChanged.BeginInvoke(this.currentValue, false, null, null);
                    }
                    this.panel1.Invalidate();
                    this.changeReady = true;
                }
            }
        }
        private bool m_valueChangeEanble = true;
        /// <summary>
        /// 重画边框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            this.Displacement = (float)this.m_RectPanel.Width;
            float num = (float)(this.currentValue - 1) * this.m_ValueRate + (float)this.rectX;
            using (Pen pen = new Pen(Color.Black, 1f))
            {
                graphics.DrawRectangle(pen, this.m_RectPanel);
            }
            using (Pen pen2 = new Pen(Color.Blue, 1f))
            {
                pen2.DashStyle = DashStyle.Dash;
                pen2.DashPattern = new float[]
                {
                    3f,
                    3f
                };
                graphics.DrawLine(pen2, new Point((int)num, this.m_RectPanel.Top), new Point((int)num, this.m_RectPanel.Bottom));
            }
            Font font = new Font("宋体", 9f, FontStyle.Regular);
            float num2 = 0f;
            float num3 = 0f;
            for (int i = 0; i < this.m_strTime.Count; i++)
            {
                SizeF sizeF = graphics.MeasureString(this.m_strTime[i], font);
                SizeF sizeF2 = graphics.MeasureString(this.Calibrations[i].ToString(), font);
                if (num2 < sizeF.Width)
                {
                    num2 = sizeF.Width;
                }
                if (num3 < sizeF2.Width)
                {
                    num3 = sizeF2.Width;
                }
            }
            List<int> list = new List<int>(2);
            float num4 = num2 / 2f;
            float num5 = num3 / 2f;
            string text = this.m_StartTime.AddSeconds((double)((this.currentValue - 1) * 30)).ToString("HH:mm:ss");
            float width = graphics.MeasureString(text, font).Width;
            int j = 0;
            while (j < this.Calibrations.Count)
            {
                if ((float)this.currentValue <= this.Calibrations[j])
                {
                    if (num + width / 2f >= (this.Calibrations[j] - 1f) * this.m_ValueRate + (float)this.m_RectPanel.X - num4)
                    {
                        list.Add(j);
                    }
                    if (j - 1 >= 0 && num - width / 2f <= (this.Calibrations[j - 1] - 1f) * this.m_ValueRate + (float)this.m_RectPanel.X + num4)
                    {
                        list.Add(j - 1);
                        break;
                    }
                    break;
                }
                else
                {
                    j++;
                }
            }
            bool flag = list.Count > 0;
            for (int k = 0; k < this.m_strTime.Count; k++)
            {
                if ((!flag || !list.Contains(k)) && (float)this.currentValue != this.Calibrations[k])
                {
                    graphics.DrawString(this.m_strTime[k], font, Brushes.Black, new PointF((this.Calibrations[k] - 1f) * this.m_ValueRate + (float)this.m_RectPanel.X - num4, (float)(this.m_RectPanel.Y - font.Height - 2)));
                    graphics.DrawString(this.Calibrations[k].ToString(), font, Brushes.Black, new PointF((this.Calibrations[k] - 1f) * this.m_ValueRate + (float)this.m_RectPanel.X - num5, (float)(this.m_RectPanel.Bottom + 2)));
                }
            }
            graphics.DrawString(text, font, Brushes.Blue, new PointF(num - num4, (float)(this.m_RectPanel.Y - font.Height - 2)));
            graphics.DrawString(this.currentValue.ToString(), font, Brushes.Blue, new PointF(num - num5, (float)(this.m_RectPanel.Bottom + 2)));
            //标记多次小睡的联动
            int markCnt = MultipleSleepMarks.Count;
            if (markCnt > 0)
            {
                for (int s = 0; s < markCnt; s++)
                {
                    RectangleMarkers rectanglemarks = MultipleSleepMarks[s] as RectangleMarkers;
                    outsleeprectangle = new Rectangle((int)(rectanglemarks.StartIndex * this.m_ValueRate + this.rectX), this.rectY + 1, (int)(this.MultipleSleepMarks[s].HeadRectangle.Width * this.ValueRate), this.m_RectPanel.Height - 2);
                    graphics.DrawRectangle(new Pen(Color.SteelBlue, 1), outsleeprectangle);
                    insleeprectangle = new Rectangle((int)(rectanglemarks.StartIndex * this.m_ValueRate + this.rectX + 1), this.rectY + 2, (int)(this.MultipleSleepMarks[s].HeadRectangle.Width * this.ValueRate - 1), this.m_RectPanel.Height - 3);
                    graphics.FillRectangle(new SolidBrush(Color.FromArgb(20, 173, 213, 162)), insleeprectangle);
                }
            }
            this.finish = true;
        }
        /// <summary>
        /// 画小睡事件外框的矩形
        /// </summary>
        private Rectangle outsleeprectangle;
        /// <summary>
        /// 画小睡事件内部的矩形
        /// </summary>
        private Rectangle insleeprectangle;
        private List<string> m_strTime = new List<string>();
        /// <summary>
        /// 获取或者设置当前所在帧序号
        /// </summary>
        public int CurrentFrameCnt
        {
            get
            {
                return this.currentValue;
            }
            set
            {
                setHsValue(value);
            }
        }
        private void setHsValue(int value)
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
                    setHsValue(value);
                }));
            }
            else
            {
                if (value != this.currentValue)
                {
                    if (value <= 0)
                    {
                        value = 1;
                        this.m_addValue = 0;
                    }
                    else if (value > this.hScrollBar1.Maximum - 9)
                    {
                        value = this.hScrollBar1.Maximum - 9;
                        this.m_addValue = 0;
                    }
                    this.currentValue = value;
                    this.hScrollBar1.Value = value;
                }
            }
        }
        /// <summary>
        /// 下一页
        /// </summary>
        public void NextPage()
        {
            m_AutoJump = false;
            CurrentFrameCnt++;
        }
        private bool m_AutoJump = true;
        /// <summary>
        ///  按键执行
        /// </summary>
        /// <param name="typ"></param>
        public void UserKeyDown(int typ, bool pageAll = false)
        {
            int addframeCnt = m_baseTimeLine / 30;
            int addvalueset = m_baseTimeLine % 30;
            m_AutoJump = false;
            switch (typ)
            {
                case 0:
                    if (this.currentValue == 1)
                    {
                        return;
                    }
                    this.m_addValue = 0;
                    this.CurrentFrameCnt = 1;
                    return;
                case 1:
                    if (this.currentValue == 1 && this.m_addValue == 0)
                    {
                        return;
                    }
                    if (pageAll)
                    {
                        int last = addframeCnt;
                        if (currentValue - addframeCnt < 1)
                            last = currentValue - 1;
                        if (addframeCnt != 0 && addvalueset == 0)
                        {
                            this.m_addValue = 0;
                            this.CurrentFrameCnt = this.currentValue - last;
                            return;
                        }
                        this.m_addValue -= addvalueset;
                        if (this.m_addValue < 0)
                        {
                            this.m_addValue += this.m_MaxFrameOffSetValue;
                            this.CurrentFrameCnt = this.currentValue - 1;
                            return;
                        }
                    }
                    else
                    {
                        this.m_addValue -= this.m_moveOffValue;
                        if (this.m_addValue < 0)
                        {
                            this.m_addValue += this.m_MaxFrameOffSetValue;
                            this.CurrentFrameCnt = this.currentValue - 1;
                            return;
                        }
                    }
                    if (this.ValueChanged != null)
                    {
                        this.ValueChanged.BeginInvoke(this.currentValue, m_AutoJump, null, null);
                        return;
                    }
                    break;
                case 2:
                    if ((float)this.currentValue == this.m_MaxValue && this.m_addValue == 0)
                    {
                        return;
                    }
                    if (pageAll)
                    {
                        int last = addframeCnt;
                        if (currentValue + addframeCnt > m_MaxValue)
                            last = (int)m_MaxValue - currentValue;
                        if (addframeCnt != 0 && addvalueset == 0)
                        {
                            this.m_addValue = 0;
                            this.CurrentFrameCnt = this.currentValue + last;
                            return;
                        }
                        this.m_addValue += addvalueset;
                        if (this.m_addValue >= this.m_MaxFrameOffSetValue)
                        {
                            this.m_addValue -= this.m_MaxFrameOffSetValue;
                            this.CurrentFrameCnt = this.currentValue + 1;
                            return;
                        }
                    }
                    else
                    {
                        this.m_addValue += this.m_moveOffValue;
                        if (this.m_addValue >= this.m_MaxFrameOffSetValue)
                        {
                            this.m_addValue -= this.m_MaxFrameOffSetValue;
                            this.CurrentFrameCnt = this.currentValue + 1;
                            return;
                        }
                    }
                    if (this.ValueChanged != null)
                    {
                        this.ValueChanged.BeginInvoke(this.currentValue, m_AutoJump, null, null);
                        return;
                    }
                    break;
                case 3:
                    if ((float)this.currentValue == this.m_MaxValue)
                    {
                        return;
                    }
                    this.m_addValue = 0;
                    this.CurrentFrameCnt = (int)this.m_MaxValue;
                    break;
                default:
                    return;
            }
        }
        private DateTime m_StartTime;
        private int m_moveOffValue = 0;
        /// <summary>
        /// 偏移量
        /// </summary>
        public int MoveOffValue
        {
            get
            {
                return this.m_moveOffValue;
            }
            set
            {
                this.m_moveOffValue = value;
                base.Invoke(new MethodInvoker(delegate ()
                {
                    this.hScrollBar1.SmallChange = this.m_moveOffValue / this.m_MaxFrameOffSetValue;
                }));
            }
        }
        private int m_baseTimeLine = 1;
        /// <summary>
        /// 设置时基
        /// </summary>
        public int BaseTimeLine
        {
            set
            {
                m_baseTimeLine = value;
            }
        }
        private int m_MaxFrameOffSetValue = 30;
        private int m_addValue = 0;
        /// <summary>
        /// 偏移量累加值
        /// </summary>
        public int FrameOffsetValue
        {
            get
            {
                if (m_moveOffValue > m_MaxFrameOffSetValue)
                {
                    return 0;
                }
                else
                {
                    return m_addValue;
                }
            }
            set
            {
                m_addValue = value;
            }
        }
        /// <summary>
        /// 设置开始结束时间
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void SetStartEndTime(DateTime start, DateTime end)
        {
            int num = (int)(end - start).TotalSeconds;
            this.m_StartTime = start;
            this.m_EndTime = end;
            int num2 = num / 30;
            if (num % 30 != 0)
            {
                num2++;
                this.m_EndTime = this.m_StartTime.AddSeconds((double)(num2 * 30));
            }
            this.m_MaxValue = (float)num2;
            this.hScrollBar1.Maximum = num2 + 9;
            this.m_MinValue = 1f;
            float off = (m_MaxValue - m_MinValue);
            if (off <= 0)
                off = 1;
            this.m_ValueRate = (float)((double)this.m_Displacement * 1.0) / off;
            this.m_strTime.Clear();
            this.Calibrations.Clear();
            float num3 = (num2 < this.m_Interval) ? 0f : ((float)((double)num2 * 1.0 / (double)this.m_Interval));
            int num4 = this.m_Interval;
            if (num3 == 0f)
            {
                num4 = num2 - 1;
                num3 = 1f;
            }
            for (int i = 0; i < num4; i++)
            {
                this.Calibrations.Add((float)((int)(this.m_MinValue + num3 * (float)i)));
                this.m_strTime.Add(this.m_StartTime.AddSeconds((double)(num3 * 30f * (float)i)).ToString("HH:mm:ss"));
            }
            this.Calibrations.Add((float)num2);
            this.m_strTime.Add(this.m_EndTime.ToString("HH:mm:ss"));
        }
        private float m_Displacement = 400;
        /// <summary>
        /// 实际位移值
        /// </summary>
        public float Displacement
        {
            set
            {
                if (m_Displacement != value)
                {
                    m_Displacement = value;
                    float off = (m_MaxValue - m_MinValue);
                    if (off <= 0)
                        off = 1;
                    m_ValueRate = (float)(m_Displacement * 1.00) / off;
                }
            }
            get
            {
                return m_Displacement;
            }
        }
        /// <summary>
        /// 位移偏移量
        /// </summary>
        public float offSetDistance = 0;
        /// <summary>
        /// 值转换系数
        /// </summary>
        private float m_ValueRate = 0.01f;
        /// <summary>
        /// 值转换系数
        /// </summary>
        public float ValueRate
        {
            get
            {
                return m_ValueRate;
            }
        }
        /// <summary>
        /// 帧刻度
        /// </summary>
        public List<float> Calibrations = new List<float>();
        private int m_Interval = 10;
        /// <summary>
        /// 刻度个数
        /// </summary>
        public int Interval
        {
            set
            {
                if (this.m_Interval == value)
                {
                    return;
                }
                this.m_Interval = value;
                this.Calibrations.Clear();
                this.m_strTime.Clear();
                float num = (float)((int)((this.m_MaxValue - this.m_MinValue) / (float)this.m_Interval));
                for (int i = 0; i < this.m_Interval; i++)
                {
                    this.Calibrations.Add(this.m_MinValue + num * (float)i);
                    this.m_strTime.Add(this.m_StartTime.AddSeconds((double)(num * 30f * (float)i)).ToString("HH:mm:ss"));
                }
                this.Calibrations.Add(this.m_MaxValue);
                this.m_strTime.Add(this.m_EndTime.ToString("HH:mm:ss"));
            }
        }
        private float m_MaxValue = 1;
        /// <summary>
        /// 最大帧序号
        /// </summary>
        public float MaxValue
        {
            get
            {
                return m_MaxValue;
            }
        }
        private float m_MinValue = 1;
        /// <summary>
        /// 最小帧序号
        /// </summary>
        public float MinValue
        {
            get
            {
                return m_MinValue;
            }
        }
        public delegate void ValueChangeDelegate(int frameCnt, bool autoJump = false);
        /// <summary>
        /// 当前帧发生变化时产生
        /// </summary>
        public event ValueChangeDelegate ValueChanged;
        /// <summary>
        /// 存放多次小睡事件
        /// </summary>
        private List<pChart.IMarker> MultipleSleepMarks = new List<pChart.IMarker>();
        /// <summary>
        /// 将存放多次小睡事件的list传值过来
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="startLine"></param>
        /// <param name="endLine"></param>
        public void DrawMultipleSleepMarks(List<pChart.IMarker> MultipleSleepMarks)
        {
            this.MultipleSleepMarks = MultipleSleepMarks;
            panel1.Invalidate();
        }
    }
}
