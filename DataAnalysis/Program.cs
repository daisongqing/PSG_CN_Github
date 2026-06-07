using AwareTec.Polysmith.pChart.HelpClass;
using AwareTec.Polysmith.pChart.HelpClass.Extensions;
using DataAnalysis.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAnalysis
{
    public class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        //[STAThread]
        public void DataAnalysis()
        {
            try
            {
                Logger.Net.Info($"[数据分析] 启动数据分析程序");
                //接收数据
                var asyncHttp = new AsyncHttp();
                var httpResult = asyncHttp.WaitAsync(asyncHttp.HttpStartTest(), 90 * 1000);
                httpResult.Wait();

                Logger.Net.Info($"[数据分析]  获取结果为：{httpResult.Result}");
                var rtnMsg = new PubReturnMsg();
                if (!httpResult.Result.IsNullOrWhiteSpace())
                {
                    if (httpResult.Result.Substring(0, 1) == "1")
                    {
                        Logger.Net.Info($"[{"数据分析"}] 成功");

                        #region 数据分析


                        rtnMsg.rtnCode = "0";
                        rtnMsg.ServiceName = "获取分析数据";
                        rtnMsg.rtnMsg = "获取分析数据成功";
                        rtnMsg.result = null;
                        #endregion
                    }
                    else if (httpResult.Result.Substring(0, 1) == "0")
                    {
                        Logger.Net.Info($"[{"数据分析"}] 失败");

                        rtnMsg.rtnCode = "-1";
                        rtnMsg.ServiceName = "获取分析数据";
                        rtnMsg.rtnMsg = "获取分析数据失败";
                    }
                    else
                    {
                        Logger.Net.Error($"[数据分析]  获取刷脸支付程序数据异常 {httpResult.Result}");

                        rtnMsg.rtnCode = "-1";
                        rtnMsg.ServiceName = "获取分析数据";
                        rtnMsg.rtnMsg = "获取分析数据异常";
                    }
                }
                else
                {
                    Logger.Net.Error($"[数据分析]  获取数据分析数据超时");

                    rtnMsg.rtnCode = "-1";
                    rtnMsg.ServiceName = "获取分析数据";
                    rtnMsg.rtnMsg = "获取分析数据超时";
                }
              
                //发送数据
                var resData = rtnMsg.ToXmlString();
                asyncHttp.resCode = resData;
                asyncHttp.HttpResStart();
                return;
            }
            catch (Exception e)
            {
                Logger.Net.Error($"[数据分析]  数据分析异常 原因：" + e.Message);
                throw;
            }
        }
    
    }
}
