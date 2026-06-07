using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AwareTec.Polysmith.pChart
{
    /// <summary>
    /// 趋势区域单元
    /// </summary>
    public class Area
    {
        /// <summary>
        /// 左上角坐标位置
        /// </summary>
        public Point Location { set; get; }
        /// <summary>
        /// 工作区域
        /// </summary>
        public Rectangle ClientRectangle
        {
            get
            {
                return new Rectangle(Location.X, Location.Y, m_width, m_Hight);
            }
        }
        /// <summary>
        /// X轴坐标队列
        /// </summary>
        public XAxis xAxis = null;
        /// <summary>
        /// Y轴坐标队列
        /// </summary>
        public YAxis yAxis = null;
        /// <summary>
        /// 曲线名称（缩写）
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 曲线中文名称
        /// </summary>
        public string Description{set; get;}
        /// <summary>
        /// 曲线ID 唯一标识
        /// </summary>
        public string ID { set; get; }
        /// <summary>
        /// 曲线单元构造函数
        /// </summary>
        public Area()
        {
            xAxis = new XAxis();
            yAxis = new YAxis();
            Location = new Point(30, 0);
            Edit = false;
            Checked = true;
        }
        private float m_top = 30;
        private float m_bottom = 10;
        private int m_Hight = 100;
        private int m_width = 800;
        /// <summary>
        /// 高度
        /// </summary>
        public int Hight
        {
            set
            {
                m_Hight = value;
                yAxis.Displacement = value - m_top - m_bottom;
            }
            get
            {
                return m_Hight;
            }
        }
        public int Width
        {
            set
            {
                m_width = value;

            }
            get
            {
                return m_width;
            }
        }
        /// <summary>
        /// 顶部空间
        /// </summary>
        public float Top
        {
            set
            {
                m_top = value;
            }
        }
        /// <summary>
        /// 顶部空间
        /// </summary>
        public float Bottom
        {
            set
            {
                m_bottom = value;
            }
        }
        /// <summary>
        /// 是否为编辑模式
        /// </summary>
        public bool Edit { set; get; }

        /// <summary>
        /// 是否选中显示
        /// </summary>
        public bool Checked { set; get; }
        /// <summary>
        /// 保存路径
        /// </summary>
        public string SavePath { set; get; }
        /// <summary>
        /// 是否显示时间标签
        /// </summary>
        public bool ShowTimeLables = false;
        /// <summary>
        /// 默认背景色为黑
        /// </summary>
        private Color m_BackColor = System.Drawing.SystemColors.Window;
        /// <summary>
        /// 获取或设置背景色
        /// </summary>
        public Color BackColor { set { m_BackColor = value; } get { return m_BackColor; } }
        private float m_PenWidth = 0.5f;
        /// <summary>
        /// 画笔的宽度
        /// </summary>
        public float PenWidth
        {
            set { m_PenWidth = value; }
            get { return m_PenWidth; }
        }
        private Font m_Font = new Font("宋体", 10.0f);
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
        private Color m_PenColor = Color.Black;
        public bool ChangeVLineWith = true;
        /// <summary>
        /// 获取或设置画笔的颜色
        /// </summary>
        public Color PenColor { set { m_PenColor = value; } get { return m_PenColor; } }
        /// <summary>
        /// 存放点位信息
        /// </summary>
        public List<SuperPointF> Points = new List<SuperPointF>();
        /// <summary>
        /// 绘制曲线
        /// </summary>
        public void DrawCurve(Graphics g)
        {
            try
            {
                Point location = Location;
                Pen[] pens = new Pen[yAxis.CalibrationsColors.Count];
                for (int i = 0; i < pens.Length; i++)
                {
                    pens[i] = new Pen(yAxis.CalibrationsColors[i], 3f);
                }
                bool userCalibrationsColor = pens.Length > 0;
                using (Pen p = new Pen(PenColor, 1f))
                {
                    if (Points != null)
                    {
                        float baseZero = location.Y + m_top;
                        float baseDisplacement = yAxis.Displacement + location.Y + m_top;
                        int cnt = Points.Count;
                        for (int i = 0; i < cnt; i++)
                        {
                            if (Points[i] == null)
                                continue;
                            if (Points[i].YMinValue != -1)
                            {
                                Points[i].X = (Points[i].XValue - xAxis.MinValue) * xAxis.ValueRate + location.X;
                                Points[i].YMin = baseDisplacement - (Points[i].YMinValue - yAxis.MinValue) * yAxis.ValueRate;
                                Points[i].YMax = baseDisplacement - (Points[i].YMaxValue - yAxis.MinValue) * yAxis.ValueRate;
                                Points[i].YMin = Points[i].YMin > baseDisplacement ? baseDisplacement : Points[i].YMin < baseZero ? baseZero : Points[i].YMin;
                                Points[i].YMax = Points[i].YMax > baseDisplacement ? baseDisplacement : Points[i].YMax < baseZero ? baseZero : Points[i].YMax;
                                if (Points[i].YMinValue == Points[i].YMaxValue && Points[i].YMaxValue > 0)
                                    Points[i].YMax += 0.1f;
                                g.DrawLine(p, new PointF(Points[i].X, Points[i].YMin), new PointF(Points[i].X, Points[i].YMax));
                            }
                            else if (Points[i].YMinValue == -1 && Points[i].YMaxValue >= 0)
                            {
                                SuperPointF next = Points[i + 1 == cnt ? i : i + 1];
                                if (next == null)
                                    next = Points[i];
                                float frontx = (next.XValue - xAxis.MinValue) * xAxis.ValueRate + +location.X;
                                Points[i].X = (Points[i].XValue - xAxis.MinValue) * xAxis.ValueRate + +location.X;
                                Points[i].YMin = baseDisplacement - (next.YMaxValue - yAxis.MinValue) * yAxis.ValueRate;
                                Points[i].YMax = baseDisplacement - (Points[i].YMaxValue - yAxis.MinValue) * yAxis.ValueRate;
                                Points[i].YMin = Points[i].YMin > baseDisplacement ? baseDisplacement : Points[i].YMin < baseZero ? baseZero : Points[i].YMin;
                                Points[i].YMax = Points[i].YMax > baseDisplacement ? baseDisplacement : Points[i].YMax < baseZero ? baseZero : Points[i].YMax;
                                if (Points[i].YMin != Points[i].YMax)
                                {
                                    p.Width = 1;
                                    p.Color = Color.LightGray;
                                    g.DrawLine(p, new PointF(Points[i].X, Points[i].YMin), new PointF(Points[i].X, Points[i].YMax));
                                    p.Color = PenColor;
                                }
                                if (ChangeVLineWith)
                                    p.Width = 3;
                                int index = (int)(Points[i].YMaxValue - yAxis.MinValue);
                                Pen pp = p;
                                //todo
                                if (index >= 0 && userCalibrationsColor)
                                    pp = pens[index];
                                g.DrawLine(pp, new PointF(frontx, Points[i].YMin), new PointF(Points[i].X, Points[i].YMin));
                                Points[i].YMin = -1;
                            }
                        }
                    }
                }
            }
            catch(Exception ee)
            {
                //todo 有时候血氧 脉率 体位会有明显错误值
                Console.WriteLine(ee.Message);
            }
        }
        private float topmargin=15;
        /// <summary>
        /// 绘制刻度
        /// </summary>
        /// <param name="g"></param>
        public void DrawYCalibrations(Graphics g,bool isSmallPic)
        {
            Point location = Location;
            using (Pen p = new Pen(PenColor, 1f))
            {
                bool drawName = true;
                if (isSmallPic)
                {
                    //小图
                    drawName = Description != "";
                    if (drawName)
                    {
                        //左上角的名字（中文）
                        g.DrawString(Description, m_Font, new SolidBrush(PenColor), new Point(location.X + 5, location.Y + 2));
                    }
                }
                else
                {
                    //一整张报告图
                    drawName = Name != "";
                    if (drawName)
                    {
                        //左上角的名字(缩写)
                        g.DrawString(Name, m_Font, new SolidBrush(PenColor), new Point(location.X + 5, location.Y + 2));
                    }
                }
                using (Pen cp = new Pen(yAxis.BackColor, 1f))
                {
                    //if (drawName)
                    //{
                    //    ///左边顶部分界线
                    //    g.DrawLine(cp, new PointF(location.X, location.Y), new PointF(location.X + 15, location.Y));
                    //    ///右边顶部分界线
                    //    g.DrawLine(cp, new PointF(location.X + xAxis.Displacement - 15, location.Y), new PointF(location.X + xAxis.Displacement, location.Y));
                    //}
                    //是否显示左右两边的边界
                    if (isSmallPic)
                    {
                        g.DrawLine(cp, new PointF(location.X, topmargin), new PointF(location.X, m_Hight));
                        if (drawName)
                        {
                            g.DrawLine(cp, new PointF(location.X + xAxis.Displacement + 1, topmargin), new PointF(location.X + xAxis.Displacement + 1, m_Hight));
                        }
                    }
                    if (m_Font.Height * yAxis.Calibrations.Count < m_Hight)
                    {
                        for (int i = 0; i < yAxis.Calibrations.Count; i++)
                        {
                            float y = yAxis.Displacement - (yAxis.Calibrations[i] - yAxis.MinValue) * yAxis.ValueRate + location.Y + m_top;
                            ///左边刻度
                            g.DrawLine(cp, new PointF(location.X - 3, y), new PointF(location.X, y));
                            if (drawName && isSmallPic)
                            {
                                //右边刻度
                                g.DrawLine(cp, new PointF(location.X + xAxis.Displacement - 3, y), new PointF(location.X + xAxis.Displacement, y));
                                //绘制横向虚线
                                using (Pen HorizontalDottedLinePen = new Pen(Color.FromArgb(240, 201, 207), 1f))
                                {
                                    HorizontalDottedLinePen.DashStyle = DashStyle.Dot;
                                    HorizontalDottedLinePen.DashPattern = new float[] { 4, 6 };
                                    g.DrawLine(HorizontalDottedLinePen, new PointF(location.X, y), new PointF(location.X + xAxis.Displacement, y));
                                }

                            }
                            ///刻度描述
                            g.DrawString(yAxis.LegendLables[i], m_Font, new SolidBrush(PenColor), new Point(0, (int)(y - m_Font.Height / 2)));
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 每个小图的绘画
        /// </summary>
        /// <returns></returns>
        public Image GetMap()
        {
            Bitmap srcImg = new Bitmap(m_width, m_Hight + (ShowTimeLables ? xAxis.Font.Height : 0));
            using (Graphics g = Graphics.FromImage(srcImg))
            {
                int w = srcImg.Width;
                int h = srcImg.Height;
                g.Clip = new Region(new Rectangle(0, 0, w, h));
                g.Clear(this.m_BackColor);
                g.SmoothingMode = SmoothingMode.None;
                Point bak_location = new Point(Location.X, Location.Y);
                try
                {
                    //绘画刻度和边框
                    DrawYCalibrations(g,true);
                    DrawCurve(g);
                    if (ShowTimeLables)
                    {
                        using (Pen Custompen = new Pen(Color.Gray, 1))
                        {
                            Custompen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                            Custompen.DashPattern = new float[] { 4, 6 };

                            for (int i = 0; i < m_Times.Length; i++)
                            {
                                float x = m_Locations[i].X;
                                float timeWidth = g.MeasureString(m_Times[i], xAxis.Font).Width;
                                if (x + timeWidth > w)
                                    x = w - timeWidth;
                                //描述时间文字
                                g.DrawString(m_Times[i], xAxis.Font, new SolidBrush(xAxis.BackColor), new PointF(x, h - xAxis.Font.Height));
                                if (i > 0||i < m_Times.Length-1)
                                {
                                    float newxpoint = x + timeWidth / 2;
                                    //描述时间刻度
                                    g.DrawLine(Custompen, new PointF(newxpoint, h - xAxis.Font.Height), new PointF(newxpoint, h - xAxis.Font.Height-4));
                                    //描述时间虚线
                                    g.DrawLine(Custompen, new PointF(newxpoint, topmargin), new PointF(newxpoint, h - xAxis.Font.Height));
                                }
                            }
                        }
                        //最下面的时间轴
                        using (Pen cp = new Pen(yAxis.BackColor, 1f))
                            g.DrawLine(cp, new PointF(Location.X, h - xAxis.Font.Height), new PointF(Location.X + xAxis.Displacement, h - xAxis.Font.Height));
                    }
                }
                catch (Exception ee) { }
                Location = bak_location;
            }
            return srcImg;
        }
        private string[] m_Times = null;
        private PointF[] m_Locations = null;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="times"></param>
        /// <param name="locations"></param>
        public void SetTimeLabels(string[] times, PointF[] locations)
        {
            m_Times = times;
            m_Locations = locations;
        }
    }
}
