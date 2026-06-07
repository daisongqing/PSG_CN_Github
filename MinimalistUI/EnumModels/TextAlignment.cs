using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalistUI.EnumModels
{
    /// <summary>
    /// 文字排列方式，须水平与垂直组合，因此相加值须在[1, 5]之间
    /// </summary>
    public enum TextAlignment
    {
        /// <summary>
        /// 垂直居中
        /// </summary>
        VerticalCenter = -3,
        /// <summary>
        /// 垂直居上
        /// </summary>
        VerticalTop = -2,
        /// <summary>
        /// 垂直居下
        /// </summary>
        VerticalBottom = -1,
        /// <summary>
        /// 水平居中
        /// </summary>
        HorizontalCenter = 4,
        /// <summary>
        /// 水平居左
        /// </summary>
        HorizontalLeft = 5,
        /// <summary>
        /// 水平居右
        /// </summary>
        HorizontalRight = 6, 
    }
}
