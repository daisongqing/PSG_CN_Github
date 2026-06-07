using Newtonsoft.Json;
using RestfulWebRequest.EnumModels;
using RestfulWebRequest.RestfulTable;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace RestfulWebRequest.RestfulModel
{
    public class ResponseFailModel<T> : ResponseModel
        where T : IRestfulTable
    {
        #region const varibles
        private const string FAIL_EXCEPTION = "引发异常, 异常类型为{0}\n";

        private const string HTTP_FAIL_EXCEPTION = "状态码为:{0}\n详细错误信息:{1}\n请求的对象类型:{2}\n请求的url:{3}\n请求的参数:{4}\n请求的Http内容类型:{5}";

        private const string SOFTWARE_FAIL_EXCEPTION = "异常消息为:{0}\n 调用堆栈为{1}";

        private const string START_FIND_STR = "\"message\":\"";
        private const string END_FIND_STR = "\",\"details\":";
        #endregion

        #region Fields
        private RequestFailType _requestFailType;

        private string _errorMessage;

        private RequestModel<T> _requestModel;

        private Exception _fieldException;

        private string _stackTrace;
        #endregion

        #region Properties
        public RequestFailType RequestFailType => _requestFailType;
        public RequestModel<T> RequestModel => _requestModel;
        public string ErrorMessage => _errorMessage;
        public string StackTrace => _stackTrace;

        private Exception _exception
        {
            get => _fieldException;
            set => _fieldException = value;
        }
        #endregion

        public ResponseFailModel(Exception failException, 
                                RequestModel<T> requestModel)
        {
            _requestFailType = RequestFailType.SoftwareOperationFail;
            _errorMessage = failException.Message;
            _requestModel = requestModel;
            _fieldException = failException;
            _stackTrace = failException.StackTrace;
        }

        public ResponseFailModel(string responseBody,
                                HttpStatusCode statusCode,
                                RequestModel<T> requestModel)
        {
            _requestFailType = RequestFailType.HttpRequestFail;
            _httpStatusCode = statusCode;
            var model = JsonConvert.DeserializeObject<ResponseErrorModel>(responseBody);
            _errorMessage = model == null ? "无服务端自定义的错误消息" : model.error.message;
            _requestModel = requestModel;
            _fieldException = null;
            _stackTrace = null;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format(FAIL_EXCEPTION, _requestFailType.ToString()));
            sb.Append(_requestFailType == RequestFailType.HttpRequestFail ?
                     string.Format(HTTP_FAIL_EXCEPTION, ((int)_httpStatusCode).ToString(),
                                                        _errorMessage,
                                                            typeof(T),
                                                        _requestModel.RequestUrl,
                                                        _requestModel.Data,
                                                        _requestModel.ContentType) :
                                                        
                    string.Format(SOFTWARE_FAIL_EXCEPTION, _errorMessage, _stackTrace));
            return sb.ToString();
        }
    }
}
