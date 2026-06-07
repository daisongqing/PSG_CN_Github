using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.ApiCall
{

    /// <summary>
    /// 自定义流接收委托
    /// </summary>
    /// <param name="data"></param>
    /// <param name="streamPath"></param>
    [ComVisible(true)]
    public delegate bool StreamReceivedBack(byte[] data, string streamPath,long datalength);
}
