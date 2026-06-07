using Newtonsoft.Json;
using RestfulWebRequest.Base;
using RestfulWebRequest.EnumModels;
using RestfulWebRequest.RestfulTable;
using RestfulWebRequest.RestfulTable.RestfulResponseTable.BuiltInType;
using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace RestfulWebRequest.RestfulModel
{
    public class ResponseSuccessModel<R> : ResponseModel
        where R : IRestfulTable
    {
        private ResponseContentType _responseContentType;

        private string _data = null;

        private object[] _array = null;

        private DownLoadFileModel _file = null;

        #region 只读属性
        public ResponseContentType ResponseContentType => _responseContentType;
        public string Data => _data;
        public object[] Array => _array;
        public DownLoadFileModel File => _file;
        #endregion

        public ResponseSuccessModel(object obj, 
                                    HttpStatusCode httpStatusCode,
                                    ResponseContentType responseContentType)
        {
            _httpStatusCode = httpStatusCode;
            _responseContentType = responseContentType;
            switch (_responseContentType)
            {
                case ResponseContentType.Array:
                    _array = (object[])obj;
                    break;
                case ResponseContentType.Stream:
                    _file = (DownLoadFileModel)obj;
                    break;
                case ResponseContentType.Json:
                    _data = (string)obj;
                    break;
            }
        }

        public R RestfulTable
        {
            get
            {
                //无返回值的情况
                if (typeof(R) == typeof(ResponseModel))
                    return null;

                //按返回值种类区分
                switch (_responseContentType)
                {
                    case ResponseContentType.Stream:
                        {
                            bool convert = HttpHelper.FillOnlyStreamModel(typeof(R), _file, out object obj);
                            if (!convert) throw new Exception("返回值转换为Stream失败");
                            return (R)obj;
                        }
                        break;
                    case ResponseContentType.Json:
                        {
                            var model = JsonConvert.DeserializeObject<R>(_data);
                            if(model == null) throw new Exception("返回值转换为Json失败");
                            return model;
                        }
                        break;
                    case ResponseContentType.Array:
                        {
                            bool convert = HttpHelper.FillOnlyArrayModel(typeof(R), _array, out object obj);
                            if (!convert) throw new Exception("返回值转换为Array失败");
                            return (R)obj;
                        }
                        break;
                    default:
                        throw new NotSupportedException("不受支持的枚举类型");
                }
            }
        }
    }
}
