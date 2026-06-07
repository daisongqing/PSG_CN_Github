using RestfulWebRequest.RestfulTable.RestfulResponseTable.BuiltInType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.RestfulTable.RestfulResponseTable
{
    public class GetPermissionListResponseModel : IRestfulTable
    {
        public Permission[] Array;
    }
}
