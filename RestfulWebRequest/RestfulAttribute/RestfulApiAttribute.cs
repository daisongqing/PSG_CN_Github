using RestfulWebRequest.EnumModels;
using System;
using System.Collections.Generic;
using System.Linq;


namespace RestfulWebRequest.RestfulAttribute
{
    /// <summary>
    /// Restful通讯特性,
    /// </summary>
    /// <remarks>
    /// RestfulApiSuffix属性必选
    /// Method属性可选, 默认Get方法
    /// RequireToken, 默认True
    /// ContentType属性可选, 默认application/json
    /// OnlyResponseByStream属性, 默认False
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class RestfulApiAttribute : Attribute
    {
        private string _restfulApiSuffix = null;
        private RequestMethod _method = RequestMethod.GET;
        private string _contentType = "application/json";
        private bool _requireToken = true;
        private string _headerString = null;
        private ResponseContentType _responseContentType = ResponseContentType.Json;

        public string RestfulApiSuffix => _restfulApiSuffix;

        public RestfulApiAttribute(string suffix)
        {
            _restfulApiSuffix = suffix;
        }

        public string ContentType
        {
            get => _contentType;
            set => _contentType = value;
        }

        public RequestMethod Method
        {
            get => _method;
            set => _method = value;
        }

        public bool RequireToken
        {
            get => _requireToken;
            set => _requireToken = value;
        }

        public Dictionary<string, string> Header
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_headerString))
                    return null;
                else
                {
                    if(!_headerString.Contains(':'))
                        return null;

                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    if (_headerString.Contains(','))
                    {
                        foreach (var keyValue in _headerString.Split(','))
                        {
                            var arr = keyValue.Split(':');
                            dict.Add(arr[0], arr[1]);
                        }
                    }
                    else
                    {
                        var arr = _headerString.Split(':');
                        dict.Add(arr[0], arr[1]);
                    }
                    
                    return dict;
                }
            }
        }

        public string HeaderString
        {
            get => _headerString;
            set => _headerString = value;
        }

        public ResponseContentType ResponseContentType
        {
            get => _responseContentType;
            set => _responseContentType = value;
        }
    }
}
