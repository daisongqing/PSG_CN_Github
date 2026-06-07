using AwareTec.Polysmith.Util.EnumUtils;
using System;
using System.Collections.Generic;

namespace AwareTec.Polysmith.Util.BaseException.ExTypeDefine
{
    public abstract class BaseExTypeException : PhysioAppBaseException
    {
        #region 私有字段
        private string _originalExType = string.Empty;
        private string _originalExMsg = string.Empty;
        private string _originalStackTrace = string.Empty;
        private string _context = string.Empty;
        private string _reason = string.Empty;
        private string _keyValueName = string.Empty;
        private string _keyVal = string.Empty;
        #endregion

        #region 公有属性
        public string OriginalExType => _originalExType;
        public string OriginalExMsg => _originalExMsg;
        public string OriginalStackTrace => _originalStackTrace;
        public string Context => _context;
        public string Reason => _reason;
        public string KeyValueName => _keyValueName;
        public string KeyVal => _keyVal;
        #endregion

        protected BaseExTypeException(Type occur,
                                      Dictionary<BaseExceptionInfoType, string> infos,
                                      Exception originalE,
                                      string typeMsg) : base(occur,
                                                            typeMsg,
                                                            GenerateMsg(originalE, infos))
        {
            DecodeE(originalE);
            DecodeDict(infos);
        }

        /// <summary>
        /// 静态异常消息模板
        /// </summary>
        protected static string _msgTemplate = @"
                                {
                                    -+-+-+-+-+-+-+-+-+-+-+-+
                                    原异常信息：
                                 }";

        /// <summary>
        /// 生成异常消息
        /// </summary>
        /// <param name="infos"></param>
        /// <returns></returns>
        private static string GenerateMsg(Exception e, Dictionary<BaseExceptionInfoType, string> infos)
        {
            string preStr = string.Empty;
            if (infos != null)
            {
                foreach (var item in infos)
                {
                    string str = EnumHelper.GetDescription(item.Key) + ":" + item.Value + "\n";
                    preStr += str;
                }
            }

            if (e == null)
                return preStr;

            string result = preStr + _msgTemplate;

            result += "\n" + EnumHelper.GetDescription(OriginalExceptionInfoType.ExType) + e.GetType().ToString();
            result += "\n" + EnumHelper.GetDescription(OriginalExceptionInfoType.ExMsg) + e.Message;
            result += "\n" + EnumHelper.GetDescription(OriginalExceptionInfoType.StackTrace) + e.StackTrace;

            return result;
        }

        /// <summary>
        /// 解析原异常
        /// </summary>
        /// <param name="infos"></param>
        private void DecodeE(Exception e)
        {
            if (e == null)
                return;

            _originalExMsg = e.Message;
            _originalExType = e.GetType().ToString();
            _originalStackTrace = e.StackTrace;
        }

        /// <summary>
        /// 解析信息
        /// </summary>
        /// <param name="infos"></param>
        private void DecodeDict(Dictionary<BaseExceptionInfoType, string> infos)
        {
            if (infos == null)
                return;

            foreach (var item in infos)
            {
                switch (item.Key)
                {
                    case BaseExceptionInfoType.Context:
                        _context = item.Value;
                        break;
                    case BaseExceptionInfoType.Reason:
                        _reason = item.Value;
                        break;
                    case BaseExceptionInfoType.KeyValueName:
                        _keyValueName = item.Value;
                        break;
                    case BaseExceptionInfoType.KeyVal:
                        _keyVal = item.Value;
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
