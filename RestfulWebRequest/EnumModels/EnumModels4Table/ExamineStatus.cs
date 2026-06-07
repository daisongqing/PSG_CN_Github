using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.EnumModels.EnumModels4Table
{
    public enum ExamineStatus
    {
        [Description("空")]
        Empty = -1,
        [Description("未审核")]
        NotReviewed = 0,
        [Description("通过")]
        Passed = 1,
        [Description("未通过")]
        NoPassed = 2
    }
}
