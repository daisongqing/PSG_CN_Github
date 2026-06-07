using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.SpecificXmlHelper;
using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.User;
using AwareTec.Polysmith.Cloud.UI.DataModels.EnumModels;
using RestfulWebRequest.ApiCall;
using RestfulWebRequest.Base;
using RestfulWebRequest.EnumModels.EnumModels4Table;
using RestfulWebRequest.RestfulModel;
using RestfulWebRequest.RestfulTable.RestfulRequestTable;
using RestfulWebRequest.RestfulTable.RestfulResponseTable;
using RestfulWebRequest.RestfulTable.RestfulResponseTable.BuiltInType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.Global
{
    public class UserDomain
    {
        private string _username;
        private string _passwordMd5;
        private ModeType _modeType;
        private bool _rememberPassword;
        private ChannelConfig _currentchannelConfig;   //To Do:替换成可用的对象
        private ChannelConfig _defaultchannelConfig;   //To Do:替换成可用的对象

        public UserDomain(string username,
                          string passwordMd5,
                          bool rememberPassword)
        {
            _username = username;
            _passwordMd5 = passwordMd5;
            _rememberPassword = rememberPassword;
        }

        #region 只读属性
        public UserTokenInfo UserTokenInfo => UserTokenInfoControl.Instance.UserTokenInfo;

        #endregion
        public ModeType ModeType
        {
            get => _modeType;
            set => _modeType = value;
        }
        public string Username
        {
            get => _username;
            set => _username = value;
        }
        public string PasswordMd5
        {
            get => _passwordMd5;
            set => _passwordMd5 = value;
        }
        public bool RememberPassword
        {
            get => _rememberPassword;
            set => _rememberPassword = value;
        }

        #region 内部直接获取的属性
        public string UserFolder
        {
            get
            {
                return  GlobalReadonlyString.PredefinedPath.USER_DOMAIN + "\\" +
                       (_rememberPassword ? _username + "_" + _passwordMd5 : _username);
            }
        }
        /// <summary>
        /// 用户配置文件路径
        /// </summary>
        public string ConfigPath
        {
            get
            {
                return UserFolder + GlobalReadonlyString.PredefinedPath.USER_CONFIG_DIR + GlobalReadonlyString.PredefinedPath.USER_DEFAULT_CONFIG_FILE;
            }
        }

        public UserEvent[] UserEvent
        {
            get
            {
                bool querySuccess = ApiRequest.Instance.QueryUserEvent(new QueryUserEventRequestModel()
                {
                    UserId = UserTokenInfo.UserId,
                    Mode = _modeType,
                }, out ResponseModel queryResponseModel);
                var model = queryResponseModel as ResponseSuccessModel<QueryUserEventResponseModel>;
                return model.RestfulTable.items;
            }
        }
        public List<OrderPath> OrderPath
        {
            get
            {
                var xmlHelper = new OrderPathXmlHelper(UserFolder + GlobalReadonlyString.PredefinedPath.USER_ORDER_DIR + GlobalReadonlyString.PredefinedPath.USER_ORDER_FILE);
                return xmlHelper.Read();
            }
        }
        public UserOperationConfig UserConfig
        {
            get
            {
                var xmlHelper = new UserOperationConfigXmlHelper(UserFolder + GlobalReadonlyString.PredefinedPath.USER_CONFIG_DIR + GlobalReadonlyString.PredefinedPath.USER_DEFAULT_CONFIG_FILE);
                return xmlHelper.Read();
            }
        }

        public ChannelConfig CurrentChannelConfig
        {
            get
            {
                return _currentchannelConfig;
            }

            set
            {
                if (value == null ||
                    value.Equals(_currentchannelConfig))
                    return;
                _currentchannelConfig = value;
            }
        }

        public ChannelConfig DefaultChannelConfig
        {
            get => _defaultchannelConfig;
            set
            {
                if (value == null ||
                    value.Equals(_defaultchannelConfig))
                    return;
                _defaultchannelConfig = value;
            }
        }
        #endregion


        #region Xml帮助类
        public OrderPathXmlHelper OrderPathXmlHelper => new OrderPathXmlHelper(UserFolder + GlobalReadonlyString.PredefinedPath.USER_ORDER_DIR + GlobalReadonlyString.PredefinedPath.USER_ORDER_FILE);
        #endregion
    }
}
