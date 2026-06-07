using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.RestfulTable.RestfulRequestTable
{
    /// <summary>
    /// 开始监测 请求模型
    /// 此为API文档接口所在位置:<see cref="https://docs.apipost.cn/preview/b47318c2a9a0cdb4/39e5d5699afa56a1?target_id=9ec9863e-38b4-44e8-a62d-f27fd350985f#8d80139d-2762-4f29-a22f-6bfb81517f04" />
    /// </summary>
    public class StartMonitoringRequestModel : IRestfulTable
    {
        public string id;
        public string sn;
    }
}
