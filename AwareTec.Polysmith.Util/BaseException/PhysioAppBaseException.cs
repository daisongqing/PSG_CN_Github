using System;

namespace AwareTec.Polysmith.Util.BaseException
{
    /// <summary>
    /// 菲诗奥睡眠系统基类异常
    /// </summary>
    public abstract class PhysioAppBaseException : Exception
    {
        #region 私有字段
        private string _module = string.Empty;
        private string _itemModule = string.Empty;
        private string _exceptionType = string.Empty;
        #endregion

        #region 公有属性
        public string Module => _module;
        public string ItemModule => _itemModule;

        public string ExceptionType => _exceptionType;

        #endregion

        protected PhysioAppBaseException(Type occur,
                                         string typeMsg,
                                         string afterMsg) : base(GenerateMsg(occur, typeMsg, afterMsg))
        {
            DecodeInfos(occur, typeMsg);
        }

        protected static string _msgTemplate = @"
                                {
                                    发生在:[{0}]模块中的[{1}]子模块
                                    异常类型：{2}
                                 }";

        /// <summary>
        /// 生成异常消息
        /// </summary>
        /// <param name="infos"></param>
        /// <returns></returns>
        private static string GenerateMsg(Type occur,
                                         string typeMsg,
                                         string afterMsg)
        {
            string str = string.Format(_msgTemplate,
                          occur.Namespace, occur.Name);
            return str + "\n" + afterMsg;
        }

        private void DecodeInfos(Type occur,
                                 string typeMsg)
        {
            _module = occur.Namespace;
            _itemModule = occur.Name;
            _exceptionType = typeMsg;
        }
    }
}
