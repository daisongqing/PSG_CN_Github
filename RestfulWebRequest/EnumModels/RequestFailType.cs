using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.EnumModels
{
    public enum RequestFailType
    {
        /// <summary>
        /// Http请求失败
        /// </summary>
        HttpRequestFail,
        /// <summary>
        /// 软件操作失败
        /// </summary>
        SoftwareOperationFail
    }
}
