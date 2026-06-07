using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.RestfulTable.RestfulRequestTable
{
    /// <summary>
    /// 上传edf标记 请求模型
    /// 此为API文档接口所在位置:<see cref="https://docs.apipost.cn/preview/b47318c2a9a0cdb4/39e5d5699afa56a1?target_id=9ec9863e-38b4-44e8-a62d-f27fd350985f#d0e000c5-bbbf-4374-970e-c76c15839d7a" />
    /// </summary>
    public class UploadEDFMarkupRequestModel:IRestfulTable
    {
        /// <summary>
        /// 断点重置
        /// </summary>
        public bool? reset;
        /// <summary>
        /// 订单号唯一标识符
        /// </summary>
        public string id;
    }
}
