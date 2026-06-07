using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalistUI.EnumModels
{
    public enum ControlStatus
    {
        /// <summary>
        /// 空
        /// </summary>
        Empty = -1,
        /// <summary>
        /// 悬浮
        /// </summary>
        Hover,
        /// <summary>
        /// 获取焦点
        /// </summary>
        Focus,
        /// <summary>
        /// 失去焦点且有错误
        /// </summary>
        LoseFocusAndError,
        /// <summary>
        /// 失去焦点但无错误
        /// </summary>
        LoseFocusAndSuccess,
    }
}
