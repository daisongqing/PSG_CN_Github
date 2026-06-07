using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.RestfulModel
{
    public class ResponseErrorModel
    {
        public ResponseError error;
    }

    public class ResponseError
    {
        public string message;
    }
}
