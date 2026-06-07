using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.EnumModels.EnumModels4Table
{
    public enum ModeType
    {
        [Description("空")]
        Empty = -1,
        [Description("儿童版")]
        Child = 0,
        [Description("成人版")]
        Adult = 1
    }
}
