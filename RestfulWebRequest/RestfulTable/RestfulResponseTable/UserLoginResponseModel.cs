using RestfulWebRequest.EnumModels.EnumModels4Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.RestfulTable.RestfulResponseTable
{
    public class UserLoginResponseModel : IRestfulTable
    {
        public string token;
        public string id;
        public string account;
        public UserType type;
        public bool? isAdmin;
    }
}
