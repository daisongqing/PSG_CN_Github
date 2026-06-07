using RestfulWebRequest.EnumModels;
using RestfulWebRequest.RestfulAttribute;
using RestfulWebRequest.RestfulModel;
using RestfulWebRequest.RestfulTable.RestfulRequestTable;
using RestfulWebRequest.RestfulTable.RestfulResponseTable;
using System;
using System.Net;
using System.Reflection;

namespace RestfulWebRequest.ApiCall
{
    /// <summary>
    /// 服务端接口调用：
    /// <see cref="Login(UserLoginRequestModel, out ResponseModel)" />登录
    /// <see cref="QueryOrderListUsingCustomerService(QueryOrderListUsingCustomerServiceRequestModel, out ResponseModel)" />检测管理列表
    /// <see cref="StartMonitoring(StartMonitoringRequestModel, out ResponseModel)(UserLoginRequestModel)" />开始监测
    /// <see cref="UploadEDFMarkup(UploadEDFMarkupRequestModel, out ResponseModel)" />上传edf标记
    /// <see cref="QueryOrderListUsingCustomerService(UserLoginRequestModel)" />
    /// 
    /// 
    /// 
    /// <!--须知: 
    ///     所有接口均返回布尔值, 为true即响应成功, 为false即响应失败
    ///     out出去的responseModel可查看响应成功返回的数据或者响应失败返回的错误信息
    /// 
    ///     接口没有返回值,ApiRequest<T,R>, R就为:ResponseModel;
    ///     接口有返回值,则R为接口所指定的响应模型
    ///     
    ///     所有的模型,涉及到非可空类型（如int、枚举、浮点等内置类型）
    ///     都需要改为可空类型
    ///     
    ///     涉及文件上传的接口, 请求模型必须看一下文件上传特性, 指定文件名和忽略属性
    ///     
    ///     200+的都是成功
    ///     300+的都是路由指向性的
    ///     400+的都是验证失败类的
    ///     500+的都是服务器内部错误
    /// -->
    /// </summary>
    public class ApiRequest : BaseApiRequest
    {
        #region 单例模式
        private static readonly ApiRequest _instance = new ApiRequest();
        public static ApiRequest Instance => _instance;
        private ApiRequest() { }
        #endregion

        #region 只读字段
        private readonly Type THIS_TYPE = typeof(ApiRequest);
        #endregion

        #region 接口

        #region 登录
        [RestfulApi("/api/user/login",
                    Method = RequestMethod.POST,
                    RequireToken = false,
                    HeaderString = "connect-origin:appclient")]
        public bool Login(UserLoginRequestModel requestModel, out ResponseModel responseModel)
        {
            bool result = ApiRequest<UserLoginRequestModel, UserLoginResponseModel>(THIS_TYPE, MethodBase.GetCurrentMethod().Name, requestModel, out ResponseModel innerResponseModel);
            responseModel = innerResponseModel;
            return result;
        }

                
        #endregion

        #region 客服
        #region 检测管理列表（查询订单）
        [RestfulApi("/api/order/examine",
                    Method = RequestMethod.GET,
                    RequireToken = true)]
        public bool QueryOrderListUsingCustomerService(QueryOrderListUsingCustomerServiceRequestModel requestModel, out ResponseModel responseModel)
        {
            bool result = ApiRequest<QueryOrderListUsingCustomerServiceRequestModel, QueryOrderListResponseModel>(THIS_TYPE, MethodBase.GetCurrentMethod().Name, requestModel, out ResponseModel innerResponseModel);
            responseModel = innerResponseModel;
            return result;
        }
        #endregion

        #region 开始监测
        [RestfulApi("/api/order/examine/begin/:id",
                    Method = RequestMethod.PUT,
                    RequireToken = true)]
        public bool StartMonitoring(StartMonitoringRequestModel requestModel, out ResponseModel responseModel)
        {
            bool result = ApiRequest<StartMonitoringRequestModel, ResponseModel>(THIS_TYPE, MethodBase.GetCurrentMethod().Name, requestModel, out ResponseModel innerResponseModel);
            responseModel = innerResponseModel;
            return result;
        }

        #endregion

        #region 修改是否是定时任务
        [RestfulApi("/api/order/examine/istimedtask/:id",
                    Method = RequestMethod.PUT,
                    RequireToken = true)]
        public bool UpdateScheduledTaskFlag(UpdateScheduledTaskFlagRequestModel requestModel, out ResponseModel responseModel)
        {
            bool result = ApiRequest<UpdateScheduledTaskFlagRequestModel, ResponseModel>(THIS_TYPE, MethodBase.GetCurrentMethod().Name, requestModel, out ResponseModel innerResponseModel);
            responseModel = innerResponseModel;
            return result;
        }

        #endregion

        #region 上传edf标记
        [RestfulApi("/api/order/upload/begin/:id",
                    Method = RequestMethod.GET,
                    RequireToken = true)]
        public bool UploadEDFMarkup(UploadEDFMarkupRequestModel requestModel, out ResponseModel responseModel)
        {
            bool result = ApiRequest<UploadEDFMarkupRequestModel, UploadEDFMarkupResponseModel>(THIS_TYPE, MethodBase.GetCurrentMethod().Name, requestModel, out ResponseModel innerResponseModel);
            responseModel = innerResponseModel;
            return result;
        }
        #endregion

        #region 上传edf
        [RestfulApi("/api/order/upload",
                    Method = RequestMethod.POST,
                    RequireToken = true,
                    ContentType = "multipart/form-data")]
        public bool UploadEDF(UploadEDFRequestModel requestModel, out ResponseModel responseModel)
        {
            bool result = ApiRequest<UploadEDFRequestModel, ResponseModel>(THIS_TYPE, MethodBase.GetCurrentMethod().Name, requestModel, out ResponseModel innerResponseModel);
            responseModel = innerResponseModel;
            return result;
        }
        #endregion

        #region 下载edf
        [RestfulApi("/api/order/download/:id",
                    Method = RequestMethod.GET,
                    RequireToken = true,
                    ResponseContentType = ResponseContentType.Stream)]
        public bool DownloadEDF(DownloadEDFRequestModel requestModel, out ResponseModel responseModel,StreamReceivedBack callback)
        {
            bool result = ApiRequest<DownloadEDFRequestModel, DownloadEDFResponseModel>(THIS_TYPE, MethodBase.GetCurrentMethod().Name, requestModel, out ResponseModel innerResponseModel, callback);
            responseModel = innerResponseModel;
            return result;
        }
        #endregion
        #endregion

        #region 判图医师
        #region 我的审核检测单（查询订单）
        [RestfulApi("/api/order/judgmentaudit/my",
                    Method = RequestMethod.GET,
                    RequireToken = true)]
        public bool QueryOrderListUsingJudgment(QueryOrderListUsingMapInterpnetationDoctoRequestModel requestModel, out ResponseModel responseModel)
        {
            bool result = ApiRequest<QueryOrderListUsingMapInterpnetationDoctoRequestModel, QueryOrderListResponseModel>(THIS_TYPE, MethodBase.GetCurrentMethod().Name, requestModel, out ResponseModel innerResponseModel);
            responseModel = innerResponseModel;
            return result;
        }
        #endregion

        #region 血氧等监测数据（保存用户数据）

        #region 添加监测数据
        [RestfulApi("/api/clientdata",
                    Method = RequestMethod.POST,
                    RequireToken = true)]
        public bool AddMonitoringData(AddOrEditMonitoringDataRequestModel requestModel, out ResponseModel responseModel)
        {
            bool result = ApiRequest<AddOrEditMonitoringDataRequestModel, ResponseModel>(THIS_TYPE, MethodBase.GetCurrentMethod().Name, requestModel, out ResponseModel innerResponseModel);
            responseModel = innerResponseModel;
            return result;
        }
        #endregion

        #region 修改
        [RestfulApi("/api/clientdata",
                    Method = RequestMethod.PUT,
                    RequireToken = true)]
        public bool EditMonitoringData(AddOrEditMonitoringDataRequestModel requestModel, out ResponseModel responseModel)
        {
            bool result = ApiRequest<AddOrEditMonitoringDataRequestModel, ResponseModel>(THIS_TYPE, MethodBase.GetCurrentMethod().Name, requestModel, out ResponseModel innerResponseModel);
            responseModel = innerResponseModel;
            return result;
        }
        #endregion

        #region 查询
        [RestfulApi("/api/clientdata/:orderId",
                    Method = RequestMethod.GET,
                    RequireToken = true)]
        public bool QueryMonitoringData(QueryMonitoringDataRequestModel requestModel, out ResponseModel responseModel)
        {
            bool result = ApiRequest<QueryMonitoringDataRequestModel, QueryMonitoringDataResponseModel>(THIS_TYPE, MethodBase.GetCurrentMethod().Name, requestModel, out ResponseModel innerResponseModel);
            responseModel = innerResponseModel;
            return result;
        }
        #endregion

        #region 查询版本号
        [RestfulApi("/api/clientdata/version/:orderId",
                    Method = RequestMethod.GET,
                    RequireToken = true)]
        public bool QueryMonitoringVersion(QueryMonitoringDataRequestModel requestModel, out ResponseModel responseModel)
        {
            bool result = ApiRequest<QueryMonitoringDataRequestModel, QueryMonitoringVersionResponseModel>(THIS_TYPE, MethodBase.GetCurrentMethod().Name, requestModel, out ResponseModel innerResponseModel);
            responseModel = innerResponseModel;
            return result;
        }
        #endregion

        #endregion

        #region 保存分析（将"待分析"转为"待审核",保存完用户数据再调用此接口, 将状态修改）
        [RestfulApi("/api/order/judgmentaudit/analysis/:id",
                    Method = RequestMethod.PUT,
                    RequireToken = true)]
        public bool SaveAnalysis(SaveAnalysisRequestModel requestModel, out ResponseModel responseModel)
        {
            bool result = ApiRequest<SaveAnalysisRequestModel, ResponseModel>(THIS_TYPE, MethodBase.GetCurrentMethod().Name, requestModel, out ResponseModel innerResponseModel);
            responseModel = innerResponseModel;
            return result;
        }
        #endregion
        #endregion

        #region 审核员 
        #region 报告审核列表
        [RestfulApi("/api/order/report",
                    Method = RequestMethod.GET,
                    RequireToken = true)]
        public bool QueryOrderListUsingAuditor(QueryOrderListUsingAuditorRequestModel requestModel, out ResponseModel responseModel)
        {
            bool result = ApiRequest<QueryOrderListUsingAuditorRequestModel, QueryOrderListResponseModel>(THIS_TYPE, MethodBase.GetCurrentMethod().Name, requestModel, out ResponseModel innerResponseModel);
            responseModel = innerResponseModel;
            return result;
        }
        #endregion

        #region 审核
        [RestfulApi("/api/order/report/examine",
                    Method = RequestMethod.PUT,
                    RequireToken = true)]
        public bool Audit(AuditRequestModel requestModel, out ResponseModel responseModel)
        {
            bool result = ApiRequest<AuditRequestModel, ResponseModel>(THIS_TYPE, MethodBase.GetCurrentMethod().Name, requestModel, out ResponseModel innerResponseModel);
            responseModel = innerResponseModel;
            return result;
        }
        #endregion

        #region 上传报告
        [RestfulApi("/api/order/report/upload",
                    Method = RequestMethod.POST,
                    RequireToken = true,
                    ContentType = "multipart/form-data")]
        public bool UploadReport(UploadReportRequestModel requestModel, out ResponseModel responseModel)
        {
            bool result = ApiRequest<UploadReportRequestModel, ResponseModel>(THIS_TYPE, MethodBase.GetCurrentMethod().Name, requestModel, out ResponseModel innerResponseModel);
            responseModel = innerResponseModel;
            return result;
        }
        #endregion

        #endregion

        #region 全角色
        #region 下载报告
        [RestfulApi("/api/order/report/download/:id",
                    Method = RequestMethod.GET,
                    RequireToken = true,
                    ResponseContentType = ResponseContentType.Stream)]
        public bool DownloadReport(DownloadReportRequestModel requestModel, out ResponseModel responseModel)
        {
            bool result = ApiRequest<DownloadReportRequestModel, DownloadReportResponseModel>(THIS_TYPE, MethodBase.GetCurrentMethod().Name, requestModel, out ResponseModel innerResponseModel);
            responseModel = innerResponseModel;
            return result;
        }
        #endregion

        #region 获取权限列表
        [RestfulApi("/api/role/allpermission",
                    Method = RequestMethod.GET,
                    RequireToken = true,
                    ResponseContentType = ResponseContentType.Array)]
        public bool GetPermissionList(GetPermissionListRequestModel requestModel, out ResponseModel responseModel)
        {
            bool result = ApiRequest<GetPermissionListRequestModel, GetPermissionListResponseModel>(THIS_TYPE, MethodBase.GetCurrentMethod().Name, requestModel, out ResponseModel innerResponseModel);
            responseModel = innerResponseModel;
            return result;
        }

        #endregion

        #region 我的订单
        [RestfulApi("/api/myorder",
                    Method = RequestMethod.GET,
                    RequireToken = true)]
        public bool GetMyOrder(GetMyOrderRequestModel requestModel, out ResponseModel responseModel)
        {
            bool result = ApiRequest<GetMyOrderRequestModel, GetMyOrderResponseModel>(THIS_TYPE, MethodBase.GetCurrentMethod().Name, requestModel, out ResponseModel innerResponseModel);
            responseModel = innerResponseModel;
            return result;
        }
        #endregion


        #region 获取设备是否存在
        [RestfulApi("/api/equipment/hasequipment/:sn",
                    Method = RequestMethod.GET,
                    RequireToken = true)]
        public bool HasEquipment(HasEquipmentRequestModel requestModel, out ResponseModel responseModel)
        {
            bool result = ApiRequest<HasEquipmentRequestModel, HasEquipmentResponseModel>(THIS_TYPE, MethodBase.GetCurrentMethod().Name, requestModel, out ResponseModel innerResponseModel);
            responseModel = innerResponseModel;
            return result;
        }
        #endregion

        #endregion

        #region 预定义接口

        #region 用户事件

        #region 新增数据
        [RestfulApi("/api/userevent",
                    Method = RequestMethod.POST,
                    RequireToken = true)]
        public bool AddUserEvent(AddUserEventRequestModel requestModel, out ResponseModel responseModel)
        {
            bool result = ApiRequest<AddUserEventRequestModel, ResponseModel>(THIS_TYPE, MethodBase.GetCurrentMethod().Name, requestModel, out ResponseModel innerResponseModel);
            responseModel = innerResponseModel;
            return result;
        }
        #endregion

        #region 根据用户id和模式查询
        [RestfulApi("/api/userevent",
                    Method = RequestMethod.GET,
                    RequireToken = true)]
        public bool QueryUserEvent(QueryUserEventRequestModel requestModel, out ResponseModel responseModel)
        {
            bool result = ApiRequest<QueryUserEventRequestModel, QueryUserEventResponseModel>(THIS_TYPE, MethodBase.GetCurrentMethod().Name, requestModel, out ResponseModel innerResponseModel);
            responseModel = innerResponseModel;
            return result;
        }
        #endregion

        #region 修改
        [RestfulApi("/api/userevent/:id",
                    Method = RequestMethod.PUT,
                    RequireToken = true)]
        public bool EditUserEvent(EditUserEventRequest requestModel, out ResponseModel responseModel)
        {
            bool result = ApiRequest<EditUserEventRequest, ResponseModel>(THIS_TYPE, MethodBase.GetCurrentMethod().Name, requestModel, out ResponseModel innerResponseModel);
            responseModel = innerResponseModel;
            return result;
        }
        #endregion

        #region 删除
        [RestfulApi("/api/userevent/:id",
                    Method = RequestMethod.DELETE,
                    RequireToken = true)]
        public bool DeleteUserEvent(DeleteUserEventRequestModel requestModel, out ResponseModel responseModel)
        {
            bool result = ApiRequest<DeleteUserEventRequestModel, ResponseModel>(THIS_TYPE, MethodBase.GetCurrentMethod().Name, requestModel, out ResponseModel innerResponseModel);
            responseModel = innerResponseModel;
            return result;
        }
        #endregion
        #endregion

        #endregion

        #endregion

        /// <summary>
        /// 设置链接地址
        /// </summary>
        public static string RemoteUrl
        {
            set
            {
                RequestModel<RestfulTable.IRestfulTable>.RemoteUrl = value;
            }
        }
    }
}
