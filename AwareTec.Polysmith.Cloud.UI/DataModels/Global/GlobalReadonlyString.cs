using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.Global
{
    public class GlobalReadonlyString
    {
        public class Common
        {
            public readonly static string REFRESH_SECTION = "appSettings";
            public readonly static string FALSE = "false";
            public readonly static string TRUE = "true";
        }

        public class Software
        {
            public readonly static string SOFTWARE_SITE = "huimian.com";
            public readonly static string ACTIVATION_KEY = "CloudTenlet";
            public readonly static string VERIFY_STRING = "20HL";
            public readonly static string FIRST_UPDATE = "FirstUpdate";
        }
        
        public class CommonException
        {
            public readonly static string EnumOutOfExpectedRange = "枚举超出预期范围";
        }

        public class PredefinedPath
        {
            public readonly static string ROOT_PATH = "\\云平台菲诗奥存储路径";
            public readonly static string REPORT_PATH = "\\导出的报告";
            public readonly static string ORDER_PATH = "\\生成的表单数据";
            public readonly static string FULL_USER_CONFIG_FILE = "FullUser.config";
            public readonly static string FILE_OR_DIR_REQUIRED = "FileOrDir.required";
            public readonly static string USER_DOMAIN = "UserDomain";
            public readonly static string USER_CHANNEL_DIR = "\\Channel";
            public readonly static string USER_CHANNEL_FILE = "\\DefaultChannel.cfg";
            public readonly static string USER_ORDER_DIR = "\\Order";
            public readonly static string USER_CONFIG_DIR = "\\UserConfig";
            public readonly static string USER_ORDER_FILE = "\\UserOrder.Path";
            public readonly static string USER_DEFAULT_CONFIG_FILE = "\\UserOperation.Config";
        }
    }
}
