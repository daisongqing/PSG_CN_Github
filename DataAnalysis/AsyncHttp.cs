using AwareTec.Polysmith.pChart.HelpClass;
using AwareTec.Polysmith.pChart.HelpClass.Extensions;
using DataAnalysis.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAnalysis
{
    public class AsyncHttp
    {
        private TcpListener _listener;

        private List<float> PressureMuzzleFlowData = new List<float>();
        private List<float> ThermalMuzzleFlowData = new List<float>();

        public async Task<string> HttpStartTest()
        {
            return await Task.Run(() =>
            {
                try
                {
                    TcpClient client = null;
                    NetworkStream stream = null;
                    byte[] buffer = null;
                    string responseJson = null;
                    IPAddress localIP = IPAddress.Parse("127.0.0.1");
                    int localPort = 8087;
                    TcpListener listener = new TcpListener(localIP, localPort);//用本地IP和端口实例化Listener
                    _listener = listener;
                    listener.Start();//开始监听
                    while (true)
                    {
                        client = listener.AcceptTcpClient();//接受一个Client
                        buffer = new byte[client.ReceiveBufferSize];
                        stream = client.GetStream();//获取网络流
                        stream.Read(buffer, 0, buffer.Length);//读取网络流中的数据
                        stream.Close();//关闭流
                        client.Close();//关闭Client
                        responseJson = Encoding.Default.GetString(buffer).Trim('\0');//转换成字符串                      
                        Logger.Net.Info($"[{"数据分析"}] 读取网络流中数据:{responseJson}");
                        if (responseJson != "" && responseJson != null && responseJson.Length > 0)
                        {
                            try
                            {
                                var resXml = responseJson.ToXmlObject<PubReturnMsg>();
                                if (resXml.ServiceName != "数据分析")
                                {
                                    listener.Stop();
                                    return "0|" + "分析异常，请重新发起数据分析";
                                }
                                else
                                {
                                    listener.Stop();
                                    if (resXml.rtnCode.IsNullOrWhiteSpace())
                                    {
                                        return "0|" + "分析异常，请重新发起数据分析";
                                    }
                                    else
                                    {
                                        PressureMuzzleFlowData = resXml.PressureMuzzleFlowData;
                                        ThermalMuzzleFlowData = resXml.ThermalMuzzleFlowData;
                                        return "1|";
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.Net.Info($"[{"错误日志记录"}]  发送内容:{ex.ToString()}");
                                listener.Stop();
                                return "0|" + ex.ToString();
                            }
                        }
                        else
                        {
                            listener.Stop();
                            return "0|发起分析失败";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Net.Info($"[{"错误日志记录"}]  发送内容:{ex.Message}");
                    //KillAlipayFacePay();
                    return ex.ToString();
                }
            });
        }

        public string resCode;

        public void HttpResStart()
        {
            try
            {
                byte[] sendData = null;//要发送的字节数组
                TcpClient client = null;//TcpClient实例
                NetworkStream stream = null;//网络流
                IPAddress remoteIP = IPAddress.Parse("127.0.0.1");//远程主机IP
                int remotePort = 8087;//远程主机端口
                sendData = Encoding.Default.GetBytes(resCode);//获取要发送的字节数组
                Logger.Net.Info($"[数据分析]  发送字节： {resCode}");
                client = new TcpClient();//实例化TcpClient
                try
                {
                    client.Connect(remoteIP, remotePort);//连接远程主机
                    stream = client.GetStream();//获取网络流
                    stream.Write(sendData, 0, sendData.Length);//将数据写入网络流
                    stream.Close();//关闭网络流
                    client.Close();//关闭客户端
                }
                catch (System.Exception ex)
                {

                }

            }
            catch (Exception ex)
            {

            }
        }

        public async Task<string> WaitAsync(Task<string> task, int timeout)
        {
            using (var timeoutCancellationTokenSource = new CancellationTokenSource())
            {
                var delayTask = Task.Delay(timeout, timeoutCancellationTokenSource.Token);
                if (await Task.WhenAny(task, delayTask) == task)
                {
                    timeoutCancellationTokenSource.Cancel();
                    return await task;
                }

                Logger.Net.Error($"[数据分析]  获取数据服务调用超时");
                throw new TimeoutException("调用超时");
            }
        }
    }
}
