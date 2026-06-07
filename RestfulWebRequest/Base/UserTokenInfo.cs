using RestfulWebRequest.EnumModels.EnumModels4Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.Base
{
    public class UserTokenInfo
    {
        #region Fields
        private string _username = null;
        private string _userId = null;
        private UserType _userType = UserType.Empty;
        private string _tokenString = null;
        private bool? _isAdmin = null;
        #endregion

        #region Properties
        public string Username => _username;
        public string UserId => _userId;
        public UserType UserType => _userType;
        public string TokenString => _tokenString;

        public bool? IsAdmin => _isAdmin;

        #endregion

        #region Constructor
        public UserTokenInfo(string username, 
                             string userId, 
                             UserType userType, 
                             string tokenString,
                             bool? isAdmin)
        {
            _username = username;
            _userId = userId;
            _userType = userType;
            _tokenString = tokenString;
            _isAdmin = isAdmin;
        }

        #endregion
    }
}
