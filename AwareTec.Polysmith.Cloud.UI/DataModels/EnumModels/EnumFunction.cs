using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.EnumModels
{
    /// <summary>
    /// 功能类型
    /// </summary>
    public enum EnumFunction
    {

        开始监测=0,
        继续监听=1,
        进入回放=2,
        上传edf = 3,
        下载edf = 4,
        查看数据 = 5,
        None=99
    }
}
