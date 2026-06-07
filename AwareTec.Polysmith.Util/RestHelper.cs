using System;
using System.IO;
using System.Net;
using System.Text;

namespace AwareTec.Polysmith.Util
{
    /// <summary>
    /// HTTP访问method  常用的几样
    /// </summary>
    public enum HttpVerb
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

    /// <summary>
    /// HTTP访问格式类型，根据Postman整理
    /// </summary>
    public enum HttpContentType
    {
        /// <summary>
        /// text/plain
        /// </summary>
        Text,
        /// <summary>
        /// application/json
        /// </summary>
        JSON,
        /// <summary>
        /// application/xml
        /// </summary>
        XML,
        /// <summary>
        /// text/xml
        /// </summary>
        TextXML,
        /// <summary>
        /// text/html
        /// </summary>
        HTML,
        /// <summary>
        /// application/javascript
        /// </summary>
        Javascript,
        /// <summary>
        /// application/octet-stream
        /// </summary>
        OCTET
    }

    /// <summary>
    /// Rest方式访问方法，ContentType为application/json
    /// </summary>
    public class RestHelper
    {
        /// <summary>
        /// 请求的url地址  eg：192.168.3.27:80000
        /// </summary>
        public string EndPoint { get; set; }

        /// <summary>
        /// 请求的方法,默认 GET
        /// </summary>
        public HttpVerb Method { get; set; }

        /// <summary>
        /// 格式类型：默认application/json 
        /// </summary>
        public HttpContentType ContentType { get; set; }

        /// <summary>
        /// 传送的数据，如json字符串
        /// </summary>
        public string PostData { get; set; }

        /// <summary>
        /// 编码规范，默认 UTF8
        /// </summary>
        public Encoding HttpEncoding { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public RestHelper(HttpContentType contentType = HttpContentType.JSON)
        {
            EndPoint = "";
            Method = HttpVerb.GET;
            ContentType = contentType;
            PostData = "";
            HttpEncoding = Encoding.UTF8;
            System.Net.ServicePointManager.DefaultConnectionLimit = 200;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public RestHelper(string endpoint, HttpContentType contentType = HttpContentType.JSON)
        {
            EndPoint = endpoint;
            Method = HttpVerb.GET;
            ContentType = contentType;
            PostData = "";
            HttpEncoding = Encoding.UTF8;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public RestHelper(string endpoint, HttpVerb method, HttpContentType contentType = HttpContentType.JSON)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = contentType;
            PostData = "";
            HttpEncoding = Encoding.UTF8;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public RestHelper(string endpoint, HttpVerb method, string postData, HttpContentType contentType = HttpContentType.JSON)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = contentType;
            PostData = postData;
            HttpEncoding = Encoding.UTF8;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public RestHelper(string endpoint, Encoding encoding, HttpVerb method = HttpVerb.GET, string postData = "", HttpContentType contentType = HttpContentType.JSON)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = contentType;
            PostData = postData;
            HttpEncoding = encoding;
        }

        /// <summary>
        /// 直接访问
        /// </summary>
        /// <returns></returns>
        public bool MakeRequest(out string responseValue)
        {
            return MakeRequest("", out responseValue);
        }
        private bool m_isconnect = true;
        /// <summary>
        /// 连接是否正常
        /// </summary>
        public bool IsConected { private set { m_isconnect = value; } get { return m_isconnect; } }
        /// <summary>
        /// 带参数访问，如 MakeRequest("?param=0")
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public bool MakeRequest(string parameters, out string responseValue)
        {
            bool ret = false;
            responseValue = string.Empty;
            try
            {
                parameters = string.Format("/{0}", parameters);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);
                request.Method = Method.ToString();
                request.ContentLength = 0;
                request.ContentType = GetContentType(ContentType);
                //如果传送的数据不为空，并且方法是post或put
                if (!string.IsNullOrEmpty(PostData) && (Method == HttpVerb.POST || Method == HttpVerb.PUT))
                {
                    request.Timeout = 1000 * 60 * 10;
                    var bytes = HttpEncoding.GetBytes(PostData);//编码方式，默认UTF-8  
                    request.ContentLength = bytes.Length;
                    using (var writeStream = request.GetRequestStream())
                    {
                        writeStream.Write(bytes, 0, bytes.Length);
                    }
                }
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                        throw new ApplicationException(message);
                    }

                    // grab the response  
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                            }
                        }
                    }
                    IsConected = true;
                    ret = true;
                }
                request.Abort();
            }
            catch (Exception e)
            {
                IsConected = false;
                new Exception("Request failed.>>" + e.Message);
            }
            return ret;
        }
        /// <summary>
        /// 带参数访问，如 MakeRequest("?param=0")
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public bool MakeRequest(byte[] data, string parameters, out string responseValue)
        {
            bool ret = false;
            responseValue = string.Empty;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);

                request.Method = Method.ToString();
                request.ContentLength = 0;
                request.ContentType = GetContentType(ContentType);
                request.Timeout = 500000;
                //如果传送的数据不为空，并且方法是post或put
                if (data != null && (Method == HttpVerb.POST || Method == HttpVerb.PUT))
                {
                    var bytes = data;//编码方式，默认UTF-8  
                    int len = bytes.Length;
                    request.ContentLength = len;
                    using (var writeStream = request.GetRequestStream())
                    {
                        writeStream.Write(bytes, 0, len);
                    }
                }
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                        throw new ApplicationException(message);
                    }

                    // grab the response  
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                            }
                        }
                    }
                    IsConected = true;
                    ret = true;
                }

            }
            catch (Exception e)
            {
                IsConected = false;
                throw new Exception("Request failed.>>" + e.Message);
            }
            return ret;
        }
        /// <summary>
        /// 将content type转成字符串描述
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        private string GetContentType(HttpContentType contentType)
        {
            switch (contentType)
            {
                case HttpContentType.Text:
                    return "text/plain";
                case HttpContentType.JSON:
                    return "application/json";
                case HttpContentType.XML:
                    return "application/xml";
                case HttpContentType.TextXML:
                    return "text/xml";
                case HttpContentType.HTML:
                    return "text/html";
                case HttpContentType.Javascript:
                    return "application/javascript";
                case HttpContentType.OCTET:
                    return "application/octet-stream";
                default:
                    return "";
            }
        }

    }
}
