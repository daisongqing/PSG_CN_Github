using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.RestfulTable.RestfulRequestTable
{
    /// <summary>
    /// 血氧等监测数据查询 请求模型
    /// 此为API文档接口所在位置:<see cref="https://docs.apipost.cn/preview/b47318c2a9a0cdb4/39e5d5699afa56a1?target_id=9ec9863e-38b4-44e8-a62d-f27fd350985f#2e474a6d-d0f1-43c4-da48-eb1c3c6e601d" />
    /// </summary>
    public class QueryMonitoringDataRequestModel : IRestfulTable
    {
        /// <summary>
        /// 订单id
        /// </summary>
        public string orderId;
    }
}
