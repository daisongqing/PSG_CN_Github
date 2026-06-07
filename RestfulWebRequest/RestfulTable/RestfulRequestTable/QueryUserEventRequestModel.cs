using RestfulWebRequest.EnumModels.EnumModels4Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.RestfulTable.RestfulRequestTable
{
    /// <summary>
    /// 用户事件数据查询 请求模型
    /// </summary>
    public class QueryUserEventRequestModel : IRestfulTable
    {
        public string UserId;
        public ModeType? Mode;
    }
}
