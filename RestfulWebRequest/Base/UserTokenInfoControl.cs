using System.Collections.Generic;
using System.Net;

namespace RestfulWebRequest.Base
{
    public class UserTokenInfoControl
    {
        private const string TOKEN_KEY_STRING = "Authorization";
        private const string TOKEN_VALUE_STRING = "Bearer {0}";

        #region 私有字段
        private Dictionary<string, string> _tokenEntry = null;

        private UserTokenInfo _userTokenInfo = null;
        #endregion

        #region 公有属性
        public UserTokenInfo UserTokenInfo
        {
            get => _userTokenInfo;
            internal set
            {
                _userTokenInfo = value;
                _tokenEntry = new Dictionary<string, string>();
                _tokenEntry.Add(TOKEN_KEY_STRING, string.Format(TOKEN_VALUE_STRING, _userTokenInfo.TokenString));
            }
        }

        internal Dictionary<string, string> TokenEntry => _tokenEntry;

        internal bool TokenExist => _tokenEntry.ContainsKey(TOKEN_KEY_STRING);
        #endregion

        #region 单例模式
        private static readonly UserTokenInfoControl _instance = new UserTokenInfoControl();
        public static UserTokenInfoControl Instance => _instance;
        #endregion

    }
}
