using RestfulWebRequest.RestfulTable.RestfulResponseTable.BuiltInType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.RestfulTable.RestfulResponseTable
{
    public class QueryOrderListResponseModel : IRestfulTable
    {
        /// <summary>
        /// 分页之前的总条数
        /// </summary>
        public int totalCount;
        /// <summary>
        /// 分页之后实际拿到的数据列表
        /// 如果分页参数都不传，默认是第一页，返回10条
        /// </summary>
        public OrderItem[] items; 
    }
}
