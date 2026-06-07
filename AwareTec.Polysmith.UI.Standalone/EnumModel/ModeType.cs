using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.UI.EnumModel
{
    /// <summary>
    /// 模式
    /// </summary>
    public enum ModeType
    {
        [Description("成人版")]
        Adult = 0,
        [Description("儿童版")]
        Child = 1
    }
}
