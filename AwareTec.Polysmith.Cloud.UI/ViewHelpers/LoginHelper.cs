using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.Predefined;
using AwareTec.Polysmith.Cloud.UI.DataModels.Global;
using AwareTec.Polysmith.Cloud.UI.Forms.Tips;
using AwareTec.Polysmith.Util.PathUtils;
using RestfulWebRequest.ApiCall;
using RestfulWebRequest.EnumModels.EnumModels4Table;
using RestfulWebRequest.RestfulModel;
using RestfulWebRequest.RestfulTable.RestfulRequestTable;
using RestfulWebRequest.RestfulTable.RestfulResponseTable;
using RestfulWebRequest.RestfulTable.RestfulResponseTable.BuiltInType;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.ViewHelpers
{
    public class LoginHelper
    {
        /// <summary>
        /// 读取用户
        /// </summary>
        public static UserDomain ReadUser(string username)
        {
            //查询用户根目录
            string userRootDir = GlobalReadonlyString.PredefinedPath.USER_DOMAIN;
            if (!StringPath.PathExists(userRootDir))
                return null;

            var dirs = Directory.GetDirectories(userRootDir).ToList();
            var findUser = dirs.Find(x => x.Split('\\').Last().Split('_').First().Equals(username));
            if (findUser == null) return null;

            //填充用户名、Md5密码和记住密码标识
            var userdomain = new UserDomain(username,
                                            findUser.Contains("_") ?
                                            findUser.Split('_').Last() :
                                            null,
                                            findUser.Contains("_"));
            return userdomain;
        }

        /// <summary>
        /// 新建用户
        /// </summary>
        /// <returns></returns>
        public static bool NewUser(UserDomain userDomain, out string errMessage)
        {
            errMessage = null;
            try
            {
                //新建用户根目录
                string userRootDir = GlobalReadonlyString.PredefinedPath.USER_DOMAIN;
                if (!StringPath.PathExists(userRootDir))
                    StringPath.CreateDir(userRootDir);

                //判断用户目录是否存在
                string username = userDomain.Username;
                var dirs = Directory.GetDirectories(userRootDir).ToList();
                var findUser = dirs.Find(x => x.Contains(username));
                if(findUser == null)
                    StringPath.CreateDir(userDomain.UserFolder);
                else if(!(new DirectoryInfo(findUser)).Name.Equals(userDomain.UserFolder.Split('\\').Last()))
                    Directory.Move(findUser, userDomain.UserFolder);

                //更新用户事件  
                UpdateEventData(userDomain);
                //新建通道
                NewUserChannelFolder(userDomain);
                //新建OrderPath
                NewOrderPathRecorder(userDomain);
                //新建用户操作配置
                NewUserOperationConfig(userDomain);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message + ":" + ex.StackTrace;
                return false;
            }
        }

        /// <summary>
        /// 根据预定义事件更新远端的事件数据
        /// </summary>
        /// <returns></returns>
        public static void UpdateEventData(UserDomain userDomain)
        {
            bool isSuccess = ApiRequest.Instance.QueryUserEvent(new QueryUserEventRequestModel()
            {
                UserId = userDomain.UserTokenInfo.UserId,
                Mode = userDomain.ModeType,
            }, out ResponseModel responseModel);
            if (!isSuccess) throw new Exception(responseModel.ToString());

            var model = responseModel as ResponseSuccessModel<QueryUserEventResponseModel>;
            var remoteEvents = model.RestfulTable.items.Where(x => x.predefinedId != -1).ToList();

            //比对预定义事件与远端数据中的事件, 不一致则删除
             var predefinedEvents = GlobalSingleton.Instance.PredefinedData.PredefinedEvents;

            #region 根据预定义事件新增专属的用户事件
            if (remoteEvents == null || remoteEvents.Count() != predefinedEvents.Count)
            {
                var defaultEvents = GlobalSingleton.Instance.DefaultData.DefaultEvents;
                foreach (var item in predefinedEvents)
                {
                    var findPredefinedInRemote = remoteEvents.Find(x => x.predefinedId.Equals(item.Id));
                    if (findPredefinedInRemote != null) continue;
                    var defaultEvent = defaultEvents.Find(x => x.Id.Equals(item.Id));
                    bool addSuccess = ApiRequest.Instance.AddUserEvent(new AddUserEventRequestModel()
                    {
                        userId = userDomain.UserTokenInfo.UserId,
                        mode = userDomain.ModeType,
                        name = item.Name,
                        description = defaultEvent.Description,
                        isAreaLabel = item.isAreaLabel,
                        markerColor = defaultEvent.MarkerColor,
                        selectedChannel = item.AllowChannel,//默认参照optionalChannel
                        eventType = item.eventType, 
                        optionalChannel = item.AllowChannel,
                        predefinedId = item.Id,
                        hotkey = defaultEvent.HotKey,
                        minTimeDomain = string.IsNullOrWhiteSpace(defaultEvent.MinTimeDomain) ? 
                                        0 : Double.Parse(defaultEvent.MinTimeDomain),
                        isReadOnly = item.isReadOnly
                    }, out ResponseModel addResponseModel);
                    if (!addSuccess) throw new Exception(addResponseModel.ToString());
                    Console.WriteLine("新增成功一条事件, 事件名称:" + item.Name);
                }
            }
            #endregion

            #region 用户登录过, 比对预定义事件
            else
            {
                foreach (var item in remoteEvents)
                {
                    //从预定义中找 是否存在对应的远端
                    var findRemoteInPredefined = predefinedEvents.Find(x => x.Id.Equals(item.predefinedId));
                    
                    //预定义事件发生删除, 须删除远端中的事件
                    if (findRemoteInPredefined == null)
                    {
                        bool deleteSuccess = ApiRequest.Instance.DeleteUserEvent(new DeleteUserEventRequestModel()
                        {
                            id = item.id
                        }, out ResponseModel deleteRresponseModel);
                        if (!deleteSuccess) throw new Exception(deleteRresponseModel.ToString());
                        Console.WriteLine("预定义事件发生删除, 正在删除远端");
                    }
                    //修改预定义事件与远端事件不一致的数据
                    bool haveEdited = EditEvents(findRemoteInPredefined, item);
                    if(haveEdited)
                        Console.WriteLine("预定义事件发生属性变动, 正在矫正远端");
                }
            }
            #endregion
        }

        /// <summary>
        /// 新建用户通道配置文件夹
        /// </summary>
        /// <returns></returns>
        public static bool NewUserChannelFolder(UserDomain userDomain)
        {
            string channelRootDir = userDomain.UserFolder +
                                    GlobalReadonlyString.PredefinedPath.USER_CHANNEL_DIR;
            if (!StringPath.PathExists(channelRootDir))
                StringPath.CreateDir(channelRootDir);
            else
                return true;
            //复制用户通道默认配置文件至用户域中
            
            string ChannelFilePath = channelRootDir + GlobalReadonlyString.PredefinedPath.USER_CHANNEL_FILE;
            if (StringPath.PathExists(ChannelFilePath))
                return true;

            var channelDefaultPath = GlobalSingleton.Instance.RequiredPaths.Find(x => x.PathType == DataModels.EnumModels.RequiredFileType.DefaultChannel).Path;

            File.Copy(channelDefaultPath, ChannelFilePath);

            return true;
        }

        /// <summary>
        /// 新建订单路径记录文件
        /// </summary>
        /// <param name="userDomain"></param>
        /// <returns></returns>
        public static bool NewOrderPathRecorder(UserDomain userDomain)
        {
            string orderRootDir = userDomain.UserFolder +
                                  GlobalReadonlyString.PredefinedPath.USER_ORDER_DIR;
            if (!StringPath.PathExists(orderRootDir))
                StringPath.CreateDir(orderRootDir);

            string orderFile = orderRootDir + GlobalReadonlyString.PredefinedPath.USER_ORDER_FILE;
            if (StringPath.PathExists(orderFile))
                return true;

            var orderDefaultPath = GlobalSingleton.Instance.RequiredPaths.Find(x => x.PathType == DataModels.EnumModels.RequiredFileType.DefaultOrderPath).Path;

            File.Copy(orderDefaultPath, orderFile);
            return true;
        }

        /// <summary>
        /// 新建用户配置文件
        /// </summary>
        /// <param name="userDomain"></param>
        /// <returns></returns>
        public static bool NewUserOperationConfig(UserDomain userDomain)
        {
            string userConfigRootDir = userDomain.UserFolder +
                                  GlobalReadonlyString.PredefinedPath.USER_CONFIG_DIR;
            if (!StringPath.PathExists(userConfigRootDir))
                StringPath.CreateDir(userConfigRootDir);

            string userConfigFile = userConfigRootDir + GlobalReadonlyString.PredefinedPath.USER_DEFAULT_CONFIG_FILE;
            if (StringPath.PathExists(userConfigFile))
                return true;

            var userConfigDefaultPath = GlobalSingleton.Instance.RequiredPaths.Find(x => x.PathType == DataModels.EnumModels.RequiredFileType.DefaultUserConfig).Path;

            File.Copy(userConfigDefaultPath, userConfigFile);
            return true;
        }


        private static bool EditEvents(PredefinedEvent predefinedEvent,
                                       UserEvent remoteEvent)
        {
            if (predefinedEvent.eventType.Equals(remoteEvent.eventType) &&
               predefinedEvent.Name.Equals(remoteEvent.name) &&
               predefinedEvent.isAreaLabel.Equals(remoteEvent.isAreaLabel) &&
               predefinedEvent.AllowChannel.Equals(remoteEvent.optionalChannel) &&
               predefinedEvent.isReadOnly.Equals(remoteEvent.isReadOnly))
                return false;

            #region 判断可选通道和已选通道
            string allow = string.Empty; string selected = string.Empty;
            if (!predefinedEvent.AllowChannel.Equals(remoteEvent.optionalChannel))
            {
                if (string.IsNullOrWhiteSpace(remoteEvent.optionalChannel) ||
                    string.IsNullOrWhiteSpace(remoteEvent.selectedChannel))
                {
                    allow = predefinedEvent.AllowChannel;
                    selected = predefinedEvent.AllowChannel;
                }
                else
                {
                    var predefinedAllowChannels = predefinedEvent.AllowChannel.Contains("/") ?
                                    predefinedEvent.AllowChannel.Split('/').ToList() :
                                    new string[] { predefinedEvent.AllowChannel }.ToList();

                    var remoteAllowChannels = remoteEvent.optionalChannel.Contains("/") ?
                                            remoteEvent.optionalChannel.Split('/').ToList() :
                                            new string[] { remoteEvent.optionalChannel }.ToList();

                    var remoteSelectedChannels = remoteEvent.selectedChannel.Contains("/") ?
                                                remoteEvent.selectedChannel.Split('/').ToList() :
                                                new string[] { remoteEvent.selectedChannel }.ToList();

                    for (int i = 0; i < remoteAllowChannels.Count; i++)
                    {
                        var item = remoteAllowChannels[i];
                        var findRemoteAllow = predefinedAllowChannels.Find(x => x.Equals(item));
                        if (findRemoteAllow != null) continue;
                        remoteAllowChannels.RemoveAt(i);
                        i--; 
                    }

                    for (int i = 0; i < remoteSelectedChannels.Count; i++)
                    {
                        var item = remoteSelectedChannels[i];
                        var findRemoteSelected = predefinedAllowChannels.Find(x => x.Equals(item));
                        if (findRemoteSelected != null) continue;
                        remoteSelectedChannels.RemoveAt(i);
                        i--;
                    }
                    remoteAllowChannels.ForEach(x => allow += (x + "/"));
                    allow = string.IsNullOrWhiteSpace(allow) ? 
                            predefinedEvent.AllowChannel: 
                            allow.Remove(allow.Length - 1, 1);
                    remoteSelectedChannels.ForEach(x => selected += (x + "/"));
                    selected = string.IsNullOrWhiteSpace(selected) ?
                               predefinedEvent.AllowChannel :
                               selected.Remove(selected.Length - 1, 1);
                }
            }
            #endregion

            if (predefinedEvent.eventType.Equals(remoteEvent.eventType) &&
               predefinedEvent.Name.Equals(remoteEvent.name) &&
               predefinedEvent.isAreaLabel.Equals(remoteEvent.isAreaLabel) &&
               predefinedEvent.isReadOnly.Equals(remoteEvent.isReadOnly) &&
               (string.IsNullOrWhiteSpace(allow) || allow.Equals(remoteEvent.optionalChannel) )&&
               (string.IsNullOrWhiteSpace(selected) || selected.Equals(remoteEvent.selectedChannel)))
                return false;

            bool isSuccess = ApiRequest.Instance.EditUserEvent(new EditUserEventRequest()
            {
                id = remoteEvent.id,
                userId = remoteEvent.userId,
                mode = (ModeType)remoteEvent.mode,
                name = predefinedEvent.Name,
                description = remoteEvent.description,
                isAreaLabel = predefinedEvent.isAreaLabel,
                markerColor = remoteEvent.markerColor,
                selectedChannel = selected,
                eventType = predefinedEvent.eventType,
                optionalChannel = allow,
                predefinedId = predefinedEvent.Id,
                hotkey = remoteEvent.hotkey,
                minTimeDomain = remoteEvent.minTimeDomain,
                isReadOnly = predefinedEvent.isReadOnly
            }, out ResponseModel responseModel);
            if(!isSuccess) throw new Exception(responseModel.ToString());
            return true;
        }
    }
}
