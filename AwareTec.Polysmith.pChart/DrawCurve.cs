using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
namespace AwareTec.Polysmith.pChart
{
    /// <summary>
    /// 曲线绘制
    /// </summary>
    public class DrawCurve
    {
        /// <summary>
        /// X轴坐标队列
        /// </summary>
        public XAxis xAxis = null;
        /// <summary>
        /// Y轴坐标队列
        /// </summary>
        public YAxis yAxis = null;
        /// <summary>
        /// 高
        /// </summary>
        private int m_Hight = 800;
        /// <summary>
        /// 宽
        /// </summary>
        private int m_Width = 600;
        /// <summary>
        /// 边距间隔
        /// </summary>
        private int m_docksize = 100;
        /// <summary>
        /// 左右边距
        /// </summary>
        public int RightLeftDistance
        {
            get
            {
                return m_docksize;
            }
        }
        /// <summary>
        /// 上下边距
        /// </summary>
        public int TopBottomDistance
        {
            get
            {
                return m_docksize;
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public DrawCurve()
        {
            xAxis = new XAxis() { BackColor = Color.DarkGray, ForeColor = Color.LimeGreen, AxisVisible = true, CalibrationsVisible = true, Displacement = m_Width - 2 * m_docksize };
            yAxis = new YAxis() { BackColor = Color.DarkGray, ForeColor = Color.LimeGreen, AxisVisible = true, Displacement = m_Hight - 2 * m_docksize };
        }
        /// <summary>
        /// 曲线集合
        /// </summary>
        private List<CurveItem> m_curveItems = new List<CurveItem>();
        /// <summary>
        /// 设置高度
        /// </summary>
        public int Height
        {
            set
            {
                m_Hight = value;
                yAxis.Displacement = m_Hight - 2 * m_docksize;
                m_rectangle = new Rectangle(m_docksize, m_docksize, m_Width - 2 * m_docksize, (int)yAxis.Displacement);
                m_Headrectangle = new Rectangle(0, m_docksize, m_docksize, m_Hight - 2 * m_docksize);
                int cnt = m_curveItems.Count;
                for (int i = 0; i < cnt; i++)
                {
                    if (m_curveItems[i].Visible)
                    {
                        m_curveItems[i].yAxis.Displacement = yAxis.Displacement;
                        m_curveItems[i].yLimitMaxDistance = m_Hight - m_docksize;
                        m_curveItems[i].yLimitMinDistance = m_docksize;
                    }
                }
            }
            get { return m_Hight; }
        }
        /// <summary>
        /// 设置高度
        /// </summary>
        public int Width
        {
            set
            {
                m_Width = value;
                xAxis.Displacement = m_Width - 2 * m_docksize;
                int cnt = m_curveItems.Count;
                m_rectangle = new Rectangle(m_docksize, m_docksize, (int)xAxis.Displacement, m_Hight - 2 * m_docksize);
                m_Headrectangle = new Rectangle(0, m_docksize, m_docksize, m_Hight - 2 * m_docksize);
                for (int i = 0; i < cnt; i++)
                {
                    if (m_curveItems[i].Visible)
                    {
                        m_curveItems[i].xAxis.Displacement = xAxis.Displacement;

                    }
                }
            }
            get { return m_Width; }
        }
        private Rectangle m_rectangle = new Rectangle();
        /// <summary>
        /// 工作区域
        /// </summary>
        public Rectangle ClientRectangle
        {
            get
            {
                return m_rectangle;
            }
        }
        private RectangleF m_Headrectangle = new RectangleF();
        /// <summary>
        /// 头部区域
        /// </summary>
        public RectangleF HeadRectangle
        {
            internal set
            {
                m_Headrectangle = value;
            }
            get
            {
                return m_Headrectangle;
            }
        }
        /// <summary>
        /// 判断结束点所在通道位置，y坐标超限是需要进行二次校准，保证其跟起始点在一个通道内
        /// </summary>
        /// <returns></returns>
        public Point CheckEndPoint(ref Point StartPoint, Point EndPoint)
        {
            Point p1 = StartPoint;
            CurveItem find = m_curveItems.Find(t => t.Visible && t.ClientRectangle.Contains(p1));
            if (find != null)
            {
                StartPoint = new Point(StartPoint.X, (int)find.ClientRectangle.Top);
                //if (find.ClientRectangle.Contains(EndPoint))
                //    return EndPoint;
                //else
                return new Point(EndPoint.X > find.ClientRectangle.Right ? (int)find.ClientRectangle.Right : EndPoint.X < find.ClientRectangle.Left ? (int)find.ClientRectangle.Left : EndPoint.X, (int)find.ClientRectangle.Bottom);
            }
            return EndPoint;
        }
        /// <summary>
        /// 根据点坐标查找当前通道副本
        /// </summary>
        /// <param name="p"></param>
        /// <returns>当前通道的副本</returns>
        public CurveItem Find(Point p)
        {
            for (int i = 0; i < m_curveItems.Count; i++)
            {
                if (m_curveItems[i].ClientRectangle.Contains(p) && m_curveItems[i].Visible)
                {
                    CurveItem ret = m_curveItems[i].Clone();
                    ret.ClientRectangleChecked = true;
                    return ret;
                }
                else if (m_curveItems[i].HeadRectangle.Contains(p))
                {
                    CurveItem ret = m_curveItems[i].Clone();
                    ret.HeadRectangleChecked = true;
                    return ret;
                }
            }
            return null;
        }
        /// <summary>
        /// 加入曲线
        /// </summary>
        /// <param name="item"></param>
        public CurveItem AddCurve(CurveItem item)
        {
            CurveItem find = m_curveItems.Find(t => t.ID == item.ID);
            if (find == null)
            {
                item.xAxis.MaxValue = this.xAxis.MaxValue;
                m_curveItems.Add(item);
                return item;
            }
            return find;
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public CurveItem Find(string key)
        {
            CurveItem find = m_curveItems.Find(t => t.ID == key);
            return find;
        }
        /// <summary>
        /// 清空所有数据
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < m_curveItems.Count; i++)
                m_curveItems[i].Clear();
        }
        /// <summary>
        /// 绘制趋势图前发生
        /// </summary>
        public delegate void DrawImageBefore();
        /// <summary>
        /// 绘制趋势图前发生
        /// </summary>
        public event DrawImageBefore DrawImageBeforeHandle;
        /// <summary>
        /// 灵敏度
        /// </summary>
        public float PixelRate = 0;
        /// <summary>
        /// 获取趋势图
        /// </summary>
        /// <returns></returns>
        public Image GetMap()
        {
            // DateTime dt = DateTime.Now;
            Bitmap srcImg = new Bitmap(m_Width, m_Hight);
            using (Graphics g = Graphics.FromImage(srcImg))
            {
                int w = this.m_Width;
                int h = this.m_Hight;
                g.Clip = new Region(new Rectangle(0, 0, w, h));
                g.Clear(this.m_backcolor);
                //g.SmoothingMode = SmoothingMode.AntiAlias;
                g.SmoothingMode = SmoothingMode.None;
                ///绘制x主轴坐标
                xAxis.DrawXAxis(g, m_docksize, w, h);
                ///绘制y主轴坐标
                yAxis.DrawYAxis(g, m_docksize, w, h);
                g.FillRectangle(Brushes.CadetBlue, new Rectangle(0, m_docksize, m_docksize - 4, h - 2 * m_docksize));
                float sss = xAxis.MaxValue / 1000;
                string strWater = "";
                if (sss / 60 > 1 && sss / 60 <= 60)
                    strWater = string.Format("{0}min", sss / 60);
                else if (sss / 60 > 60)
                    strWater = string.Format("{0}h", sss / 3600);
                else
                    strWater = string.Format("{0}s", sss);
                Font strFont = new Font("幼圆", 40, FontStyle.Bold);
                SizeF waterSize = g.MeasureString(strWater, strFont);
                g.DrawString(strWater, strFont, Brushes.Gray, new PointF(m_docksize - 4 - waterSize.Height, h / 2 - waterSize.Height / 2), new StringFormat(StringFormatFlags.DirectionVertical) { Alignment = StringAlignment.Center });
                if (DrawImageBeforeHandle != null)
                {
                    DrawImageBeforeHandle.Invoke();
                }
                m_curveItems.Sort((student1, student2) => (student1.ChannelNo - student2.ChannelNo));
                int cnt = m_curveItems.Where(t => t.Visible == true).Count();
                if (cnt != 0)
                {
                    ///根据通道个数，计算平均通道高度
                    float m = (m_Hight - 2 * m_docksize) / cnt;
                    float rr = (float)(m * 25.4 / g.DpiY);
                    int ChannelNo = 0;
                    for (int i = 0; i < m_curveItems.Count; i++)
                    {
                        if (!m_curveItems[i].Visible)
                            continue;
                        m_curveItems[i].yBaseDistance = m * ChannelNo + m / 2 + m_docksize;
                        if (m_curveItems[i].PixelRate != 0)
                            m_curveItems[i].yAxis.MaxValue = rr * m_curveItems[i].PixelRate;
                        m_curveItems[i].yAxis.Displacement = m;
                        m_curveItems[i].yAxis.offSetDistance = m * ChannelNo;
                        m_curveItems[i].xAxis.offSetDistance = m_docksize;
                        m_curveItems[i].ClientRectangle = new RectangleF(m_rectangle.X, m_rectangle.Y + m_curveItems[i].yAxis.offSetDistance, m_rectangle.Width, m);
                        m_curveItems[i].HeadRectangle = new RectangleF(m_rectangle.X - m_docksize, m_rectangle.Y + m_curveItems[i].yAxis.offSetDistance, m_docksize - 4, m);
                        ChannelNo++;
                        g.DrawString(m_curveItems[i].Name, m_curveItems[i].Font, new SolidBrush(m_curveItems[i].PenColor), new PointF(0, m_curveItems[i].yBaseDistance - m_curveItems[i].Font.Height / 2));
                        m_curveItems[i].yAxis.drawyAxis(g, m_docksize, w, h);
                        using (Pen pointpen = new Pen(m_curveItems[i].PenColor, m_curveItems[i].PenWidth))
                        {
                            // DateTime dtt = DateTime.Now;        
                            //Console.WriteLine(string.Format("{1}取数据耗时：---{0}s", (DateTime.Now - dtt).TotalSeconds, m_curveItems[i].Name));
                            if (m_curveItems[i].IsShowValue)
                            {
                                SuperPoints sp = m_curveItems[i].GetxyDataValues();
                                PointF[] source = sp.SourcePoints;
                                ///绘制曲线点
                                PointF[] datapoints = sp.Points;
                                if (datapoints.Length > 1)
                                    using (Font font = new Font("宋体", 9))
                                    {
                                        pointpen.EndCap = LineCap.RoundAnchor;
                                        float yvalue = 0;
                                        for (int a = 0; a < datapoints.Length - 1; a++)
                                        {
                                            yvalue = source[a].Y;
                                            g.DrawString(yvalue.ToString(), font, new SolidBrush(Color.Black), new PointF(datapoints[a].X - 2, datapoints[a].Y - font.Height - 2));
                                            g.DrawLine(pointpen, datapoints[a], datapoints[a + 1]);
                                        }
                                        yvalue = source[source.Length - 1].Y;
                                        g.DrawString(yvalue.ToString(), font, new SolidBrush(Color.Black), new PointF(datapoints[source.Length - 1].X - 2, datapoints[source.Length - 1].Y - font.Height - 2));
                                    }
                            }
                            else
                            {
                                ///绘制曲线点
                                PointF[] datapoints = m_curveItems[i].GetxyDataValues().Points;
                                if (datapoints.Length > 1)
                                {
                                    var yy = m_curveItems[i].ClientRectanglePoints.Select(t => t.Y);
                                    using (Font font = new Font("宋体", 9))
                                    {
                                        g.DrawString((yy.Max() - yy.Min()).ToString(), font, new SolidBrush(m_curveItems[i].PenColor), new PointF(2, m_curveItems[i].yBaseDistance + m_curveItems[i].Font.Height / 2 + 2));

                                    }
                                    g.DrawLines(pointpen, datapoints);
                                }
                            }
                            // Console.WriteLine(string.Format("{1}曲线绘制耗时：---{0}s", (DateTime.Now - dtt).TotalSeconds,m_curveItems[i].Name));
                        }
                    }
                    // System.Threading.Tasks.Task[] tt = new System.Threading.Tasks.Task[cnt];
                    // int tidx = 0;
                    // for (int i = 0; i < m_curveItems.Count; )
                    // {
                    //     if (m_curveItems[i].Visible)
                    //     {
                    //         int idx = i;
                    //         Graphics gl = Graphics.FromImage(srcImg);
                    //         g.SmoothingMode = SmoothingMode.HighQuality;
                    //         tt[tidx] = new System.Threading.Tasks.Task(() =>
                    //         {
                    //             using (Pen pointpen = new Pen(m_curveItems[idx].PenColor, m_curveItems[idx].PenWidth))
                    //             {
                    //                 ///绘制曲线点
                    //                 PointF[] datapoints = m_curveItems[idx].GetxyDataValues();
                    //                 if (datapoints.Length > 1)
                    //                     gl.DrawLines(pointpen, datapoints);
                    //             }
                    //             gl.Dispose();
                    //         });
                    //         System.Threading.Thread.Sleep(10);
                    //         tt[tidx].Start();
                    //         tidx++;
                    //     }
                    //     i++;
                    // }
                    //System.Threading.Tasks.Task.WaitAll(tt);
                    // for (int s = 0; s < tt.Length; s++)
                    // {
                    //     tt[s].Dispose();
                    // }
                    // GC.Collect();
                }
                g.Clip.Dispose();
            }
            // Console.WriteLine(string.Format("曲线绘制总耗时：---{0}s",(DateTime.Now-dt).TotalSeconds));
            return (srcImg);
        }
        /// <summary>
        /// 默认背景色为黑
        /// </summary>
        private Color m_backcolor = System.Drawing.SystemColors.Window;
        /// <summary>
        /// 获取或设置背景色
        /// </summary>
        public Color BackColor { set { m_backcolor = value; } get { return m_backcolor; } }

    }
}
