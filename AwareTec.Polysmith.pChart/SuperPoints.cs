using System.Drawing;

namespace AwareTec.Polysmith.pChart
{
    /// <summary>
    /// 超级坐标定义
    /// </summary>
    public class SuperPoints
    {
        /// <summary>
        /// 起始索引
        /// </summary>
        public int StartIndex = 0;
        /// <summary>
        /// 结束索引
        /// </summary>
        public int EndIndex = 0;
        /// <summary>
        /// 绘制用坐标集合
        /// </summary>
        public PointF[] Points { set; get; }
        /// <summary>
        /// 源值坐标集合
        /// </summary>
        public PointF[] SourcePoints { set; get; }
        /// <summary>
        /// Y值
        /// </summary>
        public float[] yDataValues { set; get; }
        public SuperPoints()
        {
            Points = new PointF[0];
            SourcePoints = new PointF[0];
            yDataValues = new float[0];
        }
    }
}
