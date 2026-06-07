using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.Base
{
    public enum RequestMethod
    {
        /// <summary>
        /// get：获取
        /// </summary>
        GET,
        /// <summary>
        /// post：修改
        /// </summary>
        POST,
        /// <summary>
        /// put：写入
        /// </summary>
        PUT,
        /// <summary>
        /// delete：删除
        /// </summary>
        DELETE
    }
}
