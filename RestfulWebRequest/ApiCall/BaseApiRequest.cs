using RestfulWebRequest.Base;
using RestfulWebRequest.RestfulAttribute;
using RestfulWebRequest.RestfulModel;
using RestfulWebRequest.RestfulTable;
using RestfulWebRequest.RestfulTable.RestfulRequestTable;
using RestfulWebRequest.RestfulTable.RestfulResponseTable;
using System;
using System.Net;

namespace RestfulWebRequest.ApiCall
{
    public abstract class BaseApiRequest
    {
        public bool ApiRequest<T,R>(Type type, 
                                    string methodName,
                                    T requestTable,
                                    out ResponseModel responseModel,StreamReceivedBack callback=null) 
                                 where T : IRestfulTable
                                 where R : IRestfulTable
        {
            RequestModel<T> requestModel = null;
            try
            {
                //读取特性
                var attr = AttributeReader.Read(type, methodName);

                //请求模型的生成
                requestModel = new RequestModel<T>(requestTable, attr);

                //连接
                responseModel = RestfulRequest.Connect2Async<T, R>(requestModel,callback).Result;

                if (responseModel is ResponseSuccessModel<R>)
                {
                    ResponseSuccessModel<R> responseSuccessModel = responseModel as ResponseSuccessModel<R>;
                    //登录成功后 更新全局单例的Token
                    if (responseSuccessModel.RestfulTable is UserLoginResponseModel &&
                        requestModel.RestfulTable is UserLoginRequestModel &&
                        !string.IsNullOrWhiteSpace((responseSuccessModel.RestfulTable as UserLoginResponseModel).token))
                    {
                        var loginUser = responseSuccessModel.RestfulTable as UserLoginResponseModel;
                        UserTokenInfoControl.Instance.UserTokenInfo = new UserTokenInfo(loginUser.account,
                                                                                        loginUser.id,
                                                                                        loginUser.type,
                                                                                        loginUser.token,
                                                                                        loginUser.isAdmin);
                    }
                    return true;
                }

                return false;
            }
            catch(Exception e)
            {
                responseModel = new ResponseFailModel<T>(e, requestModel);
                return false;
            } 
        }
    }
}
