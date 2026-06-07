using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.EnumModels
{
    public enum MainFormCloseMethod
    {
        Empty,
        [Description("注销")]
        SignOut,
        [Description("退出")]
        Exit
    }
}
