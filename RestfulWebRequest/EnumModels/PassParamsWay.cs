using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.EnumModels
{
    public enum PassParamsWay
    {
        /// <summary>
        /// 查询参数
        /// </summary>
        QueryParams,
        /// <summary>
        /// 路径参数
        /// </summary>
        PathVariables
    }
}
