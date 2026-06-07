using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.FullUser;
using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.Predefined;
using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.Predefined.total;
using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.RequiredFileOrDir;
using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.SpecificXmlHelper;
using AwareTec.Polysmith.DataBaseCom;
using AwareTec.Polysmith.Util.PathUtils;
using AwareTec.Polysmith.Util.XmlUtils;
using pSystem.UI.Register;
using RestfulWebRequest.ApiCall;
using RestfulWebRequest.Base;
using RestfulWebRequest.RestfulModel;
using RestfulWebRequest.RestfulTable.RestfulRequestTable;
using RestfulWebRequest.RestfulTable.RestfulResponseTable;
using RestfulWebRequest.RestfulTable.RestfulResponseTable.BuiltInType;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.Global
{
    /// <summary>
    /// 全局单例模式管理器
    /// </summary>
    public class GlobalSingleton
    {
        #region 单例模式
        private GlobalSingleton()
        {
            //新建注册机对象
            _reg = new Reg(GlobalReadonlyString.Software.SOFTWARE_SITE, true) { VerifyMode = emVerifyMode.Both };
            _reg.SetRegistKey(GlobalReadonlyString.Software.ACTIVATION_KEY);
            //新建日志写入对象
            _logInstance = new LogInstance();
            //新建必需路径
            var requiredPath = GlobalReadonlyString.PredefinedPath.FILE_OR_DIR_REQUIRED;
            if (StringPath.PathExists(requiredPath))
            {
                var xmlHelper4RequiredPath = new XmlHelper<RequiredPath>(XmlReader.Create(requiredPath));
                _requiredPaths = xmlHelper4RequiredPath.ToObject().RequiredSubPath.Cast<RequiredSubPath>().ToList();
            }

            _predefinedData = new PredefinedData(_requiredPaths);
            _defaultData = new DefaultData(_requiredPaths);
        }

        private static readonly GlobalSingleton _instance = new GlobalSingleton();
        public static GlobalSingleton Instance => _instance;
        #endregion

        #region 私有字段
        private Reg _reg = null;
        private LogInstance _logInstance = null;
        private Configuration _appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        private List<RequiredSubPath> _requiredPaths = null;
        private PredefinedData _predefinedData = null;
        private DefaultData _defaultData = null;
        private UserDomain _user = null;
        #endregion

        #region 属性

        #region 只读属性
        /// <summary>
        /// 注册机对象
        /// </summary>
        public Reg Reg => _reg;
        /// <summary>
        /// 日志操作对象
        /// </summary>
        public LogInstance LogInstance => _logInstance;

        public PredefinedData PredefinedData => _predefinedData;

        public DefaultData DefaultData => _defaultData;

        public Configuration AppConfig => _appConfig;

        public FullUserConfigurationModel FullUserConfig
        {
            get
            {
                var fullUserConfigPath = GlobalReadonlyString.PredefinedPath.FULL_USER_CONFIG_FILE;
                var xmlHelper = new FullUserConfigurationXmlHelper(fullUserConfigPath);
                var fullUserConfig = xmlHelper.Read();
                return fullUserConfig;
            }
        }

        /// <summary>
        /// 软件所需启动文件的路径
        /// </summary>
        public List<RequiredSubPath> RequiredPaths => _requiredPaths;

        #endregion

        #region  公开属性
        private bool m_SocreLock = false;
        private bool m_SleepLock = true;
        /// <summary>
        /// 评分锁定标识
        /// </summary>
        public bool ScoreLock
        {
            set
            {
                m_SocreLock = value;
            }
            get { return m_SocreLock; }

        }
        /// <summary>
        /// 睡眠分期锁
        /// </summary>
        public bool SleepLock
        {
            set => m_SleepLock = value;
            get => m_SleepLock;
        }

        public UserDomain User 
        {
            get => _user;
            set
            {
                _user = value;

                //切换账户时, 通道配置须转变
                string defaultChannePath = string.Format("{0}{1}",
                                           string.Format("{0}{1}", _user.UserFolder, GlobalReadonlyString.PredefinedPath.USER_CHANNEL_DIR),
                                           GlobalReadonlyString.PredefinedPath.USER_CHANNEL_FILE);
                ChannelConfig channelConfig = new ChannelConfig(defaultChannePath);
                _user.CurrentChannelConfig = channelConfig;
                _user.DefaultChannelConfig = channelConfig;
                //切换账户时, 用户事件须转变
                MarkerManage.Default.ChangeData();
            }
        }

         /// <summary>
         /// 开启数据加载
         /// </summary>
        public bool Start { set; get; }

        #endregion
        #endregion

        #region 公开方法
        /// <summary>
        ///  计算指定文件的SHA256值
        /// </summary>
        /// <param name="fileName">指定文件的完全限定名称</param>
        /// <returns>返回值的字符串形式</returns>
        public string ComputeSHA256(String fileName)
        {
            String hashMD5 = String.Empty;
            //检查文件是否存在，如果文件存在则进行计算，否则返回空值
            if (System.IO.File.Exists(fileName))
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    //计算文件的MD5值
                    System.Security.Cryptography.SHA256 calculator = System.Security.Cryptography.SHA256.Create();
                    byte[] buff = new byte[1024 * 10];///只取前10k的数据计算哈希值
                    int len = fs.Read(buff, 0, buff.Length);
                    byte[] readbytes = new byte[len];
                    Array.Copy(buff, 0, readbytes, 0, len);
                    Byte[] buffer = calculator.ComputeHash(readbytes);
                    calculator.Clear();
                    hashMD5 = string.Join("", buffer.Select(t => t.ToString("x2")));
                }//关闭文件流
            }//结束计算
            return hashMD5;
        }

        /// <summary>
        ///  计算指定文件的SHA256值
        /// </summary>
        /// <param name="fileName">指定文件的完全限定名称</param>
        /// <returns>返回值的字符串形式</returns>
        public string ComputeSHA256(byte[] data)
        {
            String hashMD5 = String.Empty;
            //计算文件的MD5值
            System.Security.Cryptography.SHA256 calculator = System.Security.Cryptography.SHA256.Create();
            byte[] buff = new byte[1024 * 10];///只取前10k的数据计算哈希值
            Array.Copy(data, 0, buff, 0, buff.Length);
            Byte[] buffer = calculator.ComputeHash(buff);
            calculator.Clear();
            hashMD5 = string.Join("", buffer.Select(t => t.ToString("x2")));
            return hashMD5;
        }
        /// <summary>
        /// 转换病人信息
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public Doc_PatientInfo convetToPatient(Patient patient)
        {
            Doc_PatientInfo info = new Doc_PatientInfo();
            info.PatientName = patient.name;
            info.Age = patient.age;
            info.Gender = patient.gender ? "男" : "女";
            info.PatientNo = patient.medicalNo;
            info.Height = (float)patient.height;
            info.Weight = (float)patient.weight;
            info.Telephone = patient.phone;
            info.BirthDate = patient.birthday;
            info.Address = patient.address;
            return info;
        }

        /// <summary>
        /// 获取全局参数配置
        /// </summary>
        public FullUserConfigurationModel getSystemSetting()
        {
            var fullUserConfigPath = GlobalReadonlyString.PredefinedPath.FULL_USER_CONFIG_FILE;
            var xmlHelper = new FullUserConfigurationXmlHelper(fullUserConfigPath);
            var fullUserConfig = xmlHelper.Read();
            return fullUserConfig;
        }
        #endregion
    }
}
