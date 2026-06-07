using System.Drawing;

namespace AwareTec.Polysmith.pChart
{
    /// <summary>
    /// 曲线区域
    /// </summary>
    public class ChartArea
    {
        #region 私有属性
        /// <summary>
        /// X轴坐标队列
        /// </summary>
        public Axis xAxis = null;
        /// <summary>
        /// Y轴坐标队列
        /// </summary>
        public Axis yAxis = null;
        /// <summary>
        /// 高
        /// </summary>
        private int m_Height = 800;
        /// <summary>
        /// 宽
        /// </summary>
        private int m_Width = 600;
        /// <summary>
        /// 左边距
        /// </summary>
        private int m_Left = 0;
        /// <summary>
        /// 右边距
        /// </summary>
        private int m_Right = 0;
        /// <summary>
        /// 上边距
        /// </summary>
        private int m_Top = 0;
        /// <summary>
        /// 下边距
        /// </summary>
        private int m_Bottom = 0;
        #endregion
        #region 公有
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
        #endregion
    }
}
