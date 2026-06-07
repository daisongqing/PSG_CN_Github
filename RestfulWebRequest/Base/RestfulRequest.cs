using Newtonsoft.Json;
using RestfulWebRequest.ApiCall;
using RestfulWebRequest.EnumModels;
using RestfulWebRequest.RestfulModel;
using RestfulWebRequest.RestfulTable;
using RestfulWebRequest.RestfulTable.RestfulResponseTable.BuiltInType;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.Base
{
    public class RestfulRequest
    {
        public static async Task<ResponseModel> Connect2Async<T, R>(RequestModel<T> requestModel, StreamReceivedBack callback =null)
                                                        where T : IRestfulTable
                                                        where R : IRestfulTable
        {
            try
            {
                int maxLengt = 1024 * 20 ;///20k
                long lastIndex = maxLengt-1;
                long startIndex = 0;
                byte[] datasource = null;
                int dataIndex = 0;
            readdata:
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = null;
                    //添加请求头
                    if(requestModel.RequestHeader != null)
                    {
                        foreach (var item in requestModel.RequestHeader)
                            client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                    if (requestModel.ResponseContentType == ResponseContentType.Stream)
                    {
                        client.DefaultRequestHeaders.Add("Range", string.Format("bytes={0}-{1}", startIndex, lastIndex));
                    }
                    switch (requestModel.Method)
                    {
                        case RequestMethod.GET:
                            response = await client.GetAsync(requestModel.RequestUrl).ConfigureAwait(false);
                            break;
                        case RequestMethod.POST:
                            response = await client.PostAsync(requestModel.RequestUrl, requestModel.HttpContent).ConfigureAwait(false);
                            break;
                        case RequestMethod.DELETE:
                            response = await client.DeleteAsync(requestModel.RequestUrl).ConfigureAwait(false);
                            break;
                        case RequestMethod.PUT:
                            response = await client.PutAsync(requestModel.RequestUrl, requestModel.HttpContent).ConfigureAwait(false);
                            break;
                        default:
                            break;
                    }

                    var status = response.StatusCode;
                    
                    if (response.IsSuccessStatusCode)
                    {
                        switch (requestModel.ResponseContentType)
                        {
                            case ResponseContentType.Stream:
                                {
                                    ContentRangeHeaderValue head = response.Content.Headers.ContentRange;
                                    Stream responseStream = await response.Content.ReadAsStreamAsync();
                                    if (response.Content.Headers.ContentDisposition == null)
                                        throw new NotSupportedException("未获取到文件的后缀信息");
                                    var fileName = response.Content.Headers.ContentDisposition.FileNameStar;
                                    if (datasource == null)
                                    {
                                        if (head != null)
                                            datasource = new byte[(long)head.Length];
                                        else
                                        {
                                            return new ResponseSuccessModel<R>(new DownLoadFileModel(responseStream, fileName), status, ResponseContentType.Stream);
                                        }
                                    }
                                    byte[] data = new byte[responseStream.Length];
                                    responseStream.Read(data, 0, data.Length);
                                    Array.Copy(data, 0, datasource, dataIndex, data.Length);
                                    dataIndex += data.Length;
                                    if (callback != null)
                                    {
                                        if (!callback.Invoke(data, fileName.Split('.').Last(), (long)head.Length))
                                            return new ResponseSuccessModel<R>(new DownLoadFileModel(null, fileName), status, ResponseContentType.Stream);
                                    }
                                    if (head.To + 1 < head.Length)
                                    {
                                        startIndex = lastIndex + 1;
                                        lastIndex += maxLengt;
                                        if (lastIndex + 1 > head.Length)
                                        {
                                            lastIndex = (long)(head.Length - 1);
                                        }
                                        client.Dispose();
                                        goto readdata;
                                    }
                                    else
                                    {
                                        if (callback == null)
                                        {
                                            responseStream.SetLength(0);
                                            responseStream.Write(datasource, 0, datasource.Length);
                                        }
                                        else
                                        {
                                            responseStream = null;
                                        }
                                        return new ResponseSuccessModel<R>(new DownLoadFileModel(responseStream, fileName), status, ResponseContentType.Stream);
                                    }
                                }
                                break;
                            case ResponseContentType.Array:
                                {
                                    string responseBody = await response.Content.ReadAsStringAsync();
                                    Type type = typeof(R);
                                    Type arrayType = type.GetField("Array").FieldType;
                                    var arrayValue = JsonConvert.DeserializeObject(responseBody, arrayType);
                                    
                                    return new ResponseSuccessModel<R>(arrayValue, status, ResponseContentType.Array);
                                }
                                break;
                            case ResponseContentType.Json:
                                {
                                    string responseBody = await response.Content.ReadAsStringAsync();
                                    return new ResponseSuccessModel<R>(responseBody, status, ResponseContentType.Json);
                                }
                                break;
                            default:
                                throw new NotSupportedException("不受支持的枚举类型");
                        }
                    }
                    else
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        return new ResponseFailModel<T>(responseBody, status, requestModel);
                    }
                }
            }
            catch (HttpRequestException e)
            {
                return new ResponseFailModel<T>(e, requestModel);
            }
            catch(Exception e)
            {
                return new ResponseFailModel<T>(e, requestModel);
            }
        }
    }
}
