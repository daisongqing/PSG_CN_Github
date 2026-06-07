using System.Drawing;

namespace AwareTec.Polysmith.pChart
{
    public class DrawingText
    {
        #region 私有
        /// <summary>
        /// 绘制文字所使用的字体
        /// </summary>
        private Font m_font;

        /// <summary>
        /// 绘制文字所使用的画笔
        /// </summary>
        private Brush m_brush;

        /// <summary>
        /// 绘制文字所绘的左上角点的坐标
        /// </summary>
        private PointF m_point;

        /// <summary>
        /// 绘制的文字字符串
        /// </summary>
        private string m_str;

        /// <summary>
        /// 所绘制的图形区域对象
        /// </summary>
        private Graphics m_graphics;
        #endregion

        #region 公有
        public TextRegion Size { get; private set; }

        #endregion

        public DrawingText(Graphics graphics,
                           Font font,
                           Brush brush,
                           PointF point,
                           string str)
        {
            m_graphics = graphics;
            m_font = font;
            m_brush = brush;
            m_point = point;
            m_str = str;

            Size = new TextRegion(m_graphics.MeasureString(m_str, m_font),
                                m_point);
        }

        public void Draw()
        {
            m_graphics.DrawString(m_str,
                                  m_font,
                                  m_brush,
                                  m_point);
        }

        #region 内部类
        public class TextRegion
        {
            public TextRegion(SizeF size,
                            PointF point)
            {
                Size = size;
                Width = Size.Width;
                Height = Size.Height;

                LeftX = point.X;
                RightX = point.X + Width;
                TopY = point.Y;
                BottomY = point.Y - Height;

                LeftTopPoint = point;
                LeftBottomPoint = new PointF(LeftX, BottomY);
                RightTopPoint = new PointF(RightX, TopY);
                RightBottomPoint = new PointF(RightX, BottomY);
            }

            /// <summary>
            /// 文字区域相加
            /// </summary>
            /// <param name="tr1"></param>
            /// <param name="tr2"></param>
            /// <returns></returns>
            public static TextRegion operator +(TextRegion tr1, TextRegion tr2)
            {
                if (tr1 == null || tr2 == null)
                {
                    if (tr1 == null && tr2 == null)
                        return null;
                    return tr1 ?? tr2;
                }
                float widthDiff = tr1.LeftX - tr2.LeftX;
                float heightDiff = tr1.TopY - tr2.TopY;
                TextRegion leftTextRegion = widthDiff > 0 ? tr2 : tr1;
                TextRegion rightTextRegion = widthDiff > 0 ? tr1 : tr2;
                TextRegion topTextRegion = heightDiff > 0 ? tr1 : tr2;
                TextRegion bottomTextRegion = heightDiff > 0 ? tr2 : tr1;
                SizeF size = new SizeF(rightTextRegion.RightX - leftTextRegion.LeftX,
                                        topTextRegion.TopY - bottomTextRegion.BottomY);
                return new TextRegion(size, new PointF(leftTextRegion.LeftX, topTextRegion.TopY));
            }

            /// <summary>
            /// 判断该对象是否与文字区域块所在位置重合
            /// </summary>
            /// <param name=""></param>
            /// <returns></returns>
            public bool isCoincidedWith(TextRegion tr)
            {
                float widthDiff = this.LeftX - tr.LeftX;
                float heightDiff = this.TopY - tr.TopY;
                TextRegion leftTextRegion = widthDiff > 0 ? tr : this;
                TextRegion rightTextRegion = widthDiff > 0 ? this : tr;
                TextRegion topTextRegion = heightDiff > 0 ? this : tr;
                TextRegion bottomTextRegion = heightDiff > 0 ? tr : this;

                if (rightTextRegion.LeftX <= leftTextRegion.RightX)
                {
                    return true;
                }
                return false;
            }

            #region 属性
            /// <summary>
            /// 宽度
            /// </summary>
            public float Width { get; private set; }

            /// <summary>
            /// 高度
            /// </summary>
            public float Height { get; private set; }

            /// <summary>
            /// 尺寸
            /// </summary>
            public SizeF Size { get; private set; }

            /// <summary>
            /// 左上角的坐标
            /// </summary>
            public PointF LeftTopPoint { get; private set; }

            /// <summary>
            /// 左下角的坐标
            /// </summary>
            public PointF LeftBottomPoint { get; private set; }

            /// <summary>
            /// 右上角的坐标
            /// </summary>
            public PointF RightTopPoint { get; private set; }

            /// <summary>
            /// 右下角的坐标
            /// </summary>
            public PointF RightBottomPoint { get; private set; }

            public float LeftX { get; private set; }

            public float RightX { get; private set; }

            public float TopY { get; private set; }

            public float BottomY { get; private set; }
            #endregion
        }
        #endregion
    }
}
