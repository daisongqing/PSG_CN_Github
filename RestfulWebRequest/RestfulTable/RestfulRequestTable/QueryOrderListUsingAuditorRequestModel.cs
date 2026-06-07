using RestfulWebRequest.EnumModels.EnumModels4Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.RestfulTable.RestfulRequestTable
{
    /// <summary>
    /// 报告审核列表 请求模型
    /// 此为API文档接口所在位置:<see cref="https://docs.apipost.cn/preview/b47318c2a9a0cdb4/39e5d5699afa56a1?target_id=9ec9863e-38b4-44e8-a62d-f27fd350985f#ad6c35d4-0306-4aca-9f6a-57a183e6ad81" />
    /// </summary>
    public class QueryOrderListUsingAuditorRequestModel : IRestfulTable
    {
        /// <summary>
        /// 订单号（非订单id）
        /// </summary>
        public string Filter;
        /// <summary>
        /// 医疗机构id
        /// </summary>
        public string HospitalId;
        /// <summary>
        /// 审核状态
        /// </summary>
        public ExamineStatus? ExamineStatus;
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
