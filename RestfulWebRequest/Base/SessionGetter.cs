using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.Base
{
    public class SessionGetter
    {
        private string _username = null;

        private string _password = null;
        private string _sessionKey = null;

        public string Username => _username;
        public string Password => _password;
        public string SessionKey => _sessionKey;

        public SessionGetter(string username, string password, string sessionKey)
        {
            _username = username;
            _password = password;
            _sessionKey = sessionKey;
        }
    }
}
