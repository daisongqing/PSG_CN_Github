using System.Net;

namespace RestfulWebRequest.Base
{
    public class SessionRequest
    {
        private const string SESSION_KEY = "SessionKey";

        #region 私有字段
        private WebHeaderCollection _sessionKeyHeader = new WebHeaderCollection();

        private SessionGetter _sessionGetter = null;
        #endregion

        #region 公有属性
        public SessionGetter SessionGetter
        {
            get => _sessionGetter;
            set
            {
                _sessionGetter = value;
                _sessionKeyHeader.Clear();

                _sessionKeyHeader.Add(SESSION_KEY, _sessionGetter.SessionKey);
            }
        }

        public WebHeaderCollection SessionKeyHeader => _sessionKeyHeader;

        public bool SessionExist
        {
            get
            {
                if(_sessionKeyHeader != null)
                {
                    foreach(var keyStr in _sessionKeyHeader.AllKeys)
                    {
                        if (keyStr.Equals(SESSION_KEY))
                            return true;
                    }       
                }
                return false;
            }
        }
        #endregion

        #region 单例模式
        private static readonly SessionRequest _instance = new SessionRequest();
        public static SessionRequest Instance => _instance;
        #endregion

    }
}
