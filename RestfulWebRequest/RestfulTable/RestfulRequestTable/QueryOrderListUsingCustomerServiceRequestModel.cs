using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.RestfulTable.RestfulRequestTable
{
    /// <summary>
    /// 检测管理列表 请求模型
    /// 此为API文档接口所在位置:<see cref="https://docs.apipost.cn/preview/b47318c2a9a0cdb4/39e5d5699afa56a1?target_id=9ec9863e-38b4-44e8-a62d-f27fd350985f#c6754497-2a3e-428e-a694-4fe730fe9868" />
    /// </summary>
    public class QueryOrderListUsingCustomerServiceRequestModel : IRestfulTable
    {
        /// <summary>
        /// 订单号（非订单id）
        /// </summary>
        public string Filter;
        /// <summary>
        /// 订单状态
        /// </summary>
        public int? Status;
        /// <summary>
        /// 医疗机构id
        /// </summary>
        public string HospitalId;
        /// <summary>
        /// 开始的预约检测时间
        /// </summary>
        public DateTime? StartTime;
        /// <summary>
        /// 结束的预约检测时间
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
