using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.RestfulTable.RestfulRequestTable
{
    /// <summary>
    /// 修改是否是定时任务
    /// 此为API文档接口所在位置:<see cref="https://docs.apipost.cn/preview/b47318c2a9a0cdb4/39e5d5699afa56a1?target_id=9ec9863e-38b4-44e8-a62d-f27fd350985f#ce54f373-323a-41a8-f317-5e15f99d111c" />
    /// </summary>
    public class UpdateScheduledTaskFlagRequestModel : IRestfulTable
    {
        /// <summary>
        /// 订单号id
        /// </summary>
        public string id;
        /// <summary>
        /// 是否是定时任务
        /// </summary>
        public bool? isTimedTask;
    }
}
