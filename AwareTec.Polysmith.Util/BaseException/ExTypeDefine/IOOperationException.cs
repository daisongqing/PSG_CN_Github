using System;
using System.Collections.Generic;

namespace AwareTec.Polysmith.Util.BaseException.ExTypeDefine
{
    /// <summary>
    /// IO操作异常
    /// </summary>
    public sealed class IOOperationException : BaseExTypeException
    {
        private const string TYPE_MSG = "IO操作异常";

        public IOOperationException(Type occur,
                                     Dictionary<BaseExceptionInfoType, string> infos,
                                     Exception e) : base(occur, infos, e, TYPE_MSG) { }
    }
}
