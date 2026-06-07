using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.EnumModels.EnumModels4Table
{
    public enum ExamType
    {
        [Description("空")]
        Empty = -1,
        [Description("多导")]
        MultiLead = 0,
        [Description("初筛")]
        PrimaryScreeningTest = 1
    }
}
