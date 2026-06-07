using RestfulWebRequest.RestfulAttribute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.RestfulTable.RestfulRequestTable
{
    /// <summary>
    /// 上传报告 请求模型
    /// 此为API文档接口所在位置:<see cref="https://docs.apipost.cn/preview/b47318c2a9a0cdb4/39e5d5699afa56a1?target_id=9ec9863e-38b4-44e8-a62d-f27fd350985f#69772e0c-8886-403e-86a2-c3e4b04eeff1" />
    /// </summary>
    public class UploadReportRequestModel : IRestfulTable
    {
        /// <summary>
        /// 订单id
        /// </summary>
        public string id;
        /// <summary>
        /// 待上传的报告文件流
        /// </summary>
        public Stream File;

        [FileUpload(true,
                    MultipartFormDataIgnore = true)]
        public string fileName;
    }
}
