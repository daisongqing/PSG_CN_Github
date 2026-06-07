using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.RestfulTable.RestfulResponseTable
{
    public class UploadEDFMarkupResponseModel : IRestfulTable
    {
        /// <summary>
        /// 目前此订单对应的EDF已上传了几片
        /// </summary>
        public int currentPieces;
    }
}
