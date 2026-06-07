using Newtonsoft.Json;
using RestfulWebRequest.Base;
using RestfulWebRequest.EnumModels;
using RestfulWebRequest.RestfulAttribute;
using RestfulWebRequest.RestfulTable;
using RestfulWebRequest.RestfulTable.RestfulRequestTable;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web;

namespace RestfulWebRequest.RestfulModel
{
    public class RequestModel<T> : IRestfulTable 
        where T : IRestfulTable
    {
        private static string DOMAIN = "";

        private static string defaultUrl = "http://sleep.physiomedtec.com";
        private static readonly JsonSerializerSettings JSONS_ERIALIZER_SETTINGS = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore
        };

        #region 私有字段

        private HttpContent _httpContent;

        private string _contentType;
        /// <summary>
        /// 传输的实体类
        /// </summary>
        private readonly T _restfulTable;
        /// <summary>
        /// 请求方法
        /// </summary>
        private RequestMethod _method;
        /// <summary>
        /// API后缀
        /// </summary>
        private string _restfulSuffix;
        /// <summary>
        /// 请求头
        /// </summary>
        private Dictionary<string, string> _requestHeader = null;

        private string _data = null;

        private string _requestUrl = null;

        private ResponseContentType _responseContentType = ResponseContentType.Json;
        #endregion

        #region 只读属性
        public ResponseContentType ResponseContentType => _responseContentType;
        public HttpContent HttpContent => _httpContent;
        public string ContentType => _contentType;

        public string RequestUrl => _requestUrl;
        public string Data => _data;

        public T RestfulTable => _restfulTable;

        public RequestMethod Method => _method;

        public string RestfulSuffix => _restfulSuffix;

        public Dictionary<string, string> RequestHeader => _requestHeader;
        #endregion

        #region 构造函数

        public RequestModel(T restfulTable,
                            RestfulApiAttribute attr)
        {
            _restfulTable = restfulTable;
            _method = attr.Method;
            _contentType = attr.ContentType;
            _restfulSuffix = attr.RestfulApiSuffix;
            _responseContentType = attr.ResponseContentType;
            RemoteUrl = RequestModel<IRestfulTable>.DOMAIN;
            if (restfulTable != null)
                Load();
            else
            {
                _requestUrl = DOMAIN + _restfulSuffix;
                _httpContent = null;
            }
            
            

            if (attr.RequireToken || attr.Header != null)
            {
                if (attr.RequireToken &&
                    !UserTokenInfoControl.Instance.TokenExist)
                    throw new Exception("Token不存在");

                _requestHeader = new Dictionary<string, string>();

                if (attr.Header != null)
                {
                    foreach (var item in attr.Header)
                        _requestHeader.Add(item.Key, item.Value);
                }

                if (attr.RequireToken)
                {
                    foreach (var item in UserTokenInfoControl.Instance.TokenEntry)
                        _requestHeader.Add(item.Key, item.Value);
                }
            }
            else
                _requestHeader = null;
        }

        private void Load()
        {
            //拼接路径参数与查询参数
            _requestUrl = DOMAIN + HttpHelper.SerializeObject(_restfulTable, _restfulSuffix, _method, out bool noField);
            //如果参数拼接后有剩余的其他可用属性
            if (!noField)
            {
                switch (_contentType)
                {
                    case "application/json":
                        _httpContent = new StringContent(JsonConvert.SerializeObject(_restfulTable, Formatting.Indented, JSONS_ERIALIZER_SETTINGS), System.Text.Encoding.UTF8, _contentType);
                        break;
                    case "multipart/form-data": //文件传输
                        _httpContent = HttpHelper.GetFormData(_restfulTable);
                        break;
                }
            }else
                _httpContent = null;
        }
        #endregion

        #region 只写属性
        /// <summary>
        /// 设置链接地址
        /// </summary>
        internal static string RemoteUrl
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                    DOMAIN = defaultUrl;
                else
                    DOMAIN = value;
            }
        }
        #endregion
    }
}
