using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Util.NetworkUtils
{
    public class NetworkHelper
    {
        public static bool CheckForInternetConnection(int timeoutMs = 10000, string url = null)
        {
            try
            {
                //Ping myPing = new Ping();
                //String host = "http://www.baidu.com";
                //byte[] buffer = new byte[32];
                //int timeout = 1000;
                //PingOptions pingOptions = new PingOptions();
                //PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                //return (reply.Status == IPStatus.Success);
                return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
