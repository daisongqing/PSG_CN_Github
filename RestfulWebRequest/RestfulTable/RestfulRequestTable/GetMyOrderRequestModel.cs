using RestfulWebRequest.EnumModels.EnumModels4Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.RestfulTable.RestfulRequestTable
{
    public class GetMyOrderRequestModel : IRestfulTable
    {
        /// <summary>
        /// 订单号（非订单id）
        /// </summary>
        public string Filter;
        /// <summary>
        /// 患者姓名过滤
        /// </summary>
        public string PatientName;
        /// <summary>
        /// 开始的预约检测时间
        /// </summary>
        public DateTime? StartTime;
        /// <summary>
        /// 结束的预约检测时间
        /// </summary>
        public DateTime? EndTime;
        /// <summary>
        /// 订单状态
        /// </summary>
        public int? Status;
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
        /// <summary>
        /// 模式
        /// </summary>
        public ModeType? Mode;
    }
}
