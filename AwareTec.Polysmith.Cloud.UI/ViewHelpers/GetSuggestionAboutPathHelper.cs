using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.FullUser;
using AwareTec.Polysmith.Cloud.UI.DataModels.Global;
using AwareTec.Polysmith.Util.PathUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.ViewHelpers
{
    public class GetSuggestionAboutPathHelper
    {
        /// <summary>
        /// 得到推荐配置路径并生成
        /// </summary>
        /// <returns></returns>
        public static FullUserConfigurationModel GetAndGeneratePath()
        {
            string diskId = StringPath.GetMaxFreeSpaceDiskId();
            string rootDir = diskId + GlobalReadonlyString.PredefinedPath.ROOT_PATH;
            if (!StringPath.PathExists(rootDir))
                StringPath.CreateDir(rootDir);

            string orderDir = rootDir + GlobalReadonlyString.PredefinedPath.ORDER_PATH;
            string reportDir = rootDir + GlobalReadonlyString.PredefinedPath.REPORT_PATH;
            if(!StringPath.PathExists(orderDir))
                StringPath.CreateDir(orderDir);
            if(!StringPath.PathExists(reportDir))
                StringPath.CreateDir(reportDir);

            return new FullUserConfigurationModel()
            {
                RootPath = rootDir,
                OrderPath = orderDir,
                ReportPath = reportDir
            };
        }
        /// <summary>
        /// 以前安装过，推荐配置路径不需要重新生成
        /// </summary>
        /// <returns></returns>
        public static FullUserConfigurationModel HasSetGeneratePath()
        {
            List<string> alldisks = StringPath.GetAllDiskId();

            bool hassetpath = false;
            string rootDir = "";
            string orderDir = "";
            string reportDir = "";

            for (int i = 0; i < alldisks.Count; i++)
            {
                rootDir = string.Format("{0}{1}", alldisks[i], GlobalReadonlyString.PredefinedPath.ROOT_PATH);
                if (StringPath.PathExists(rootDir))
                {
                    orderDir = string.Format("{0}{1}", rootDir, GlobalReadonlyString.PredefinedPath.ORDER_PATH);
                    reportDir = string.Format("{0}{1}", rootDir, GlobalReadonlyString.PredefinedPath.REPORT_PATH);
                    if (!StringPath.PathExists(orderDir))
                        StringPath.CreateDir(orderDir);
                    if (!StringPath.PathExists(reportDir))
                        StringPath.CreateDir(reportDir);
                    if (StringPath.PathExists(orderDir) && StringPath.PathExists(reportDir))
                    {
                        hassetpath = true;
                        break;
                    }
                }
            }
            if (hassetpath)
            {
                return new FullUserConfigurationModel()
                {
                    RootPath = rootDir,
                    OrderPath = orderDir,
                    ReportPath = reportDir
                };
            }
            else
            {
                return null;
            }
        }
    }
}
