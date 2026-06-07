using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.RestfulTable.RestfulRequestTable
{
    /// <summary>
    /// 用户登录 请求模型
    /// 此为API文档接口所在位置:<see cref="https://docs.apipost.cn/preview/b47318c2a9a0cdb4/39e5d5699afa56a1?target_id=9ec9863e-38b4-44e8-a62d-f27fd350985f#233f5455-b006-4bcb-add8-d7bd7d2b2526" />
    /// </summary>
    public class UserLoginRequestModel : IRestfulTable
    {
        public string account;
        public string password;
    }
}
