namespace AwareTec.Polysmith.DataCenter
{
    /// <summary>
    /// 通讯类接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICommunication
    {
        event ExtraTaskRunningDelegate ExtraTaskRunningHandler;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        bool Init();
        /// <summary>
        /// 准备分析前的必要数据
        /// </summary>
        /// <returns></returns>
        bool SubmitAnalysisConditions(InConditions Conditions);
        /// <summary>
        /// 获取最终的分析结果
        /// </summary>
        /// <param name="ID">结果保存的唯一标识符</param>
        /// <returns></returns>
        OutResult ReadAnalysiResult(string ResultID);
        /// <summary>
        /// 判断数据源是否存在
        /// </summary>
        /// <returns></returns>
        bool ExistDataSource();
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        bool UpLoadDataSouce(byte[] data);
        /// <summary>
        /// 分析终止
        /// </summary>
        /// <returns></returns>
        bool AnalysisStop();
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <returns></returns>
        bool DeleteDataSource();
        /// <summary>
        /// 释放资源
        /// </summary>
        /// <returns></returns>
        bool Dispose();
        /// <summary>
        /// 执行附加任务
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        object GoExtraTask(object[] args);
        /// <summary>
        /// 通讯ID
        /// </summary>
        string ID { set; get; }
        /// <summary>
        /// 服务通讯状态 正常/断开
        /// </summary>
        bool IsConnected { get; }
    }
    /// <summary>
    /// 
    /// </summary>
    public interface ICommunicationEx
    {
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="recData"></param>
        /// <returns></returns>
        bool Get<T>(out T recData);
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Post<T>(T data);
        /// <summary>
        /// 装换
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        T Convert<T>(object data);
    }
}
