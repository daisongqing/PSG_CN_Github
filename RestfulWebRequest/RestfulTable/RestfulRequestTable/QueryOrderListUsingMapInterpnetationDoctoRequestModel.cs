using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.RestfulTable.RestfulRequestTable
{
    /// <summary>
    /// 我的审核检测单（查询订单） 请求模型
    /// 此为API文档接口所在位置:<see cref="https://docs.apipost.cn/preview/b47318c2a9a0cdb4/39e5d5699afa56a1?target_id=9ec9863e-38b4-44e8-a62d-f27fd350985f#e794760e-6144-4828-9d7e-f3103dd90e92" />
    /// </summary>
    public class QueryOrderListUsingMapInterpnetationDoctoRequestModel : IRestfulTable
    {
        /// <summary>
        /// 订单号（非订单id）
        /// </summary>
        public string No;
        /// <summary>
        /// 开始的预约监测时间
        /// </summary>
        public DateTime? StartTime;
        /// <summary>
        /// 结束的预约监测时间
        /// </summary>
        public DateTime? EndTime;
        /// <summary>
        /// 过滤数据条数 LIMIT <N-M> OFFSET <M>,这个skipcount指的是OFFSET后面的值
        /// 如果分页参数都不传，默认是第一页，返回10条
        /// </summary>
        public int? SkipCount;
        /// <summary>
        /// 每页显示条数 LIMIT <N-M> OFFSET <M>,这个MaxResultCount指的是LIMIT后面的值
        /// 如果分页参数都不传，默认是第一页，返回10条
        /// </summary>
        public int? MaxResultCount;
    }
}
