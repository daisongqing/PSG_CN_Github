using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.EnumModels.EnumModels4Table
{
    public enum UserType
    {
        [Description("空")]
        Empty = -1,
        [Description("平台用户")]
        PlatformUser = 0,
        [Description("渠道商用户")]
        ChannelBusinessUser = 1,
        [Description("判图中心用户")]
        JudgementCenterUser = 2,
        [Description("医疗机构用户")]
        MedicalInstitutionUser = 3
    }
}
