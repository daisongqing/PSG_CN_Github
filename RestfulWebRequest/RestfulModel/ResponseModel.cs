using RestfulWebRequest.RestfulTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.RestfulModel
{
    public abstract class ResponseModel: IRestfulTable
    {
        protected HttpStatusCode _httpStatusCode;
        public HttpStatusCode HttpStatusCode => _httpStatusCode;
    }
}
