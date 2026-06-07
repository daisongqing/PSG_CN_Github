using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.EnumModels
{
    public enum CheckResults
    {
        Empty,
        Ok,
        [Description("数据准备错误")]
        ReadyDataError,
        [Description("缺失必要的启动文件")]
        RequiredFilesAreMissing,
        [Description("当前不是最新版本")]
        NotCurrentlyTheLatestVersion,
        [Description("序列号未激活")]
        SerialNumberNotBeActivated,
    }
}
