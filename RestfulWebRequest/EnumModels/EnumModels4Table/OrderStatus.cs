using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.EnumModels.EnumModels4Table
{
    public enum OrderStatus
    {
        [Description("空")]
        Empty = -1,
        [Description("待监测")]
        ToBeMonitored = 0,
        [Description("待上传")]
        ToBeUpload = 1,
        [Description("待分析")]
        ToBeAnalysis = 2,
        [Description("待审核")]
        ToBeAudit = 3,
        [Description("预约取消")]
        AppointmentCancellation = 4,
        [Description("已完成")]
        Completed = 5
    }
}
