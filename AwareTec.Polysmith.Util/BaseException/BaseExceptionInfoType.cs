using System.ComponentModel;

namespace AwareTec.Polysmith.Util.BaseException
{
    /// <summary>
    /// 系统基类异常的信息类型
    /// </summary>
    public enum BaseExceptionInfoType
    {
        [Description("模块")]
        Module,
        [Description("子模块")]
        ItemModule,
        [Description("异常类型")]
        ExceptionType,
        [Description("上下文")]
        Context,
        [Description("原因")]
        Reason,
        [Description("关键变量名")]
        KeyValueName,
        [Description("关键变量值")]
        KeyVal
    }

    /// <summary>
    /// 原异常信息类型
    /// </summary>
    public enum OriginalExceptionInfoType
    {
        [Description("异常类型")]
        ExType,
        [Description("异常信息")]
        ExMsg,
        [Description("调用堆栈")]
        StackTrace
    }
}
