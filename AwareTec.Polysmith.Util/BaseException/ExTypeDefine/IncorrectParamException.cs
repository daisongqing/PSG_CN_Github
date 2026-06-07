using System;
using System.Collections.Generic;

namespace AwareTec.Polysmith.Util.BaseException.ExTypeDefine
{
    /// <summary>
    /// 错误传参异常
    /// </summary>
    public sealed class IncorrectParamException : BaseExTypeException
    {
        private const string TYPE_MSG = "错误传参异常";

        public IncorrectParamException(Type occur,
                                     Dictionary<BaseExceptionInfoType, string> infos,
                                     Exception e) : base(occur, infos, e, TYPE_MSG) { }
    }
}
