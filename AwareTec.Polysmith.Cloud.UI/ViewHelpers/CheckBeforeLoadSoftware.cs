using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.FullUser;
using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.RequiredFileOrDir;
using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.SpecificXmlHelper;
using AwareTec.Polysmith.Cloud.UI.DataModels.EnumModels;
using AwareTec.Polysmith.Cloud.UI.DataModels.Global;
using AwareTec.Polysmith.Util;
using AwareTec.Polysmith.Util.PathUtils;
using AwareTec.Polysmith.Util.XmlUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AwareTec.Polysmith.Cloud.UI.ViewHelpers
{
    /// <summary>
    /// 软件加载前的必要性检查
    /// </summary>
    public class CheckBeforeLoadSoftware
    {
        private string m_TemplatezipPath = "";
        private string m_reportPath = "";
        private string m_logTmpPath = "";
        private string basePath = AppDomain.CurrentDomain.BaseDirectory;
        private string m_zipPath = "";
        public static CheckResults Check()
        {
            //检测是否缺失必要文件
            if (!CheckFilesAreMissing())
                return CheckResults.RequiredFilesAreMissing;

            //TO DO: 检测当前软件是否为最新版本
            var currentSoftwareVersion = Assembly.GetExecutingAssembly().GetName().Version;

            //检测是否已注册
            if (GlobalSingleton.Instance.Reg.VerifyReg().ToUpper() != GlobalReadonlyString.Software.VERIFY_STRING)
                return CheckResults.SerialNumberNotBeActivated;

            //检测路径数据是否已准备
            if (!ReadyData() || !new CheckBeforeLoadSoftware().CheckOther())
                return CheckResults.ReadyDataError;

            return CheckResults.Ok;
        }

        private static bool CheckFilesAreMissing()
        {
            var requiredPaths = GlobalSingleton.Instance.RequiredPaths;
            if (requiredPaths == null) return false;

            foreach (var item in requiredPaths)
            {
                if (!StringPath.PathExists(item.Path))
                    return false;
            }
            return true;
        }

        private static bool ReadyData()
        {
            try
            {
                if (GlobalSingleton.Instance.RequiredPaths == null)
                    return false;

                var fullUserConfigPath = GlobalReadonlyString.PredefinedPath.FULL_USER_CONFIG_FILE;

                //不存在则从Default中复制一份
                if (!StringPath.PathExists(fullUserConfigPath))
                {
                    var defaultPath = GlobalSingleton.Instance.RequiredPaths.Find(x => x.PathType == RequiredFileType.DefaultFullUserConfig).Path;
                    File.Copy(defaultPath, fullUserConfigPath);
                }

                var fullUserConfigurationXmlHelper = new FullUserConfigurationXmlHelper(fullUserConfigPath);

                var model = fullUserConfigurationXmlHelper.Read();
                if (string.IsNullOrWhiteSpace(model.RootPath))
                {
                    var hassetpath = GetSuggestionAboutPathHelper.HasSetGeneratePath();
                    if (hassetpath != null)
                    {
                        fullUserConfigurationXmlHelper.Modify(hassetpath);
                    }
                    else
                    {
                        var suggestModel = GetSuggestionAboutPathHelper.GetAndGeneratePath();
                        fullUserConfigurationXmlHelper.Modify(suggestModel);
                    }
                }
                else
                {
                    if (!StringPath.PathExists(model.RootPath))
                        StringPath.CreateDir(model.RootPath);
                    if (!StringPath.PathExists(model.OrderPath))
                        StringPath.CreateDir(model.OrderPath);
                    if (!StringPath.PathExists(model.ReportPath))
                        StringPath.CreateDir(model.ReportPath);
                }
                return true;
            }
            catch (Exception ex)
            {
                if (ex is InsufficientMemoryException)
                    throw ex;
                return false;
            }
        }

        /// <summary>
        /// 检查必备文件无缺失
        /// </summary>
        /// <returns></returns>
        private bool CheckOther()
        {
            m_zipPath = basePath + "Template";
            m_TemplatezipPath = string.Format("{0}\\Template.zip", m_zipPath);
            m_reportPath = string.Format("{0}ReportTemplate", basePath);
            m_logTmpPath = string.Format("{0}Feature\\Log\\Resource", basePath);
            if (!File.Exists(this.m_TemplatezipPath))
            {
                return false;
            }
            bool reportReady = Directory.Exists(this.m_reportPath);
            if (!reportReady)
            {
                Directory.CreateDirectory(this.m_reportPath);
            }
            bool hasLogTmp = Directory.Exists(this.m_logTmpPath);
            if (!hasLogTmp)
            {
                Directory.CreateDirectory(this.m_logTmpPath);
            }
            bool result = false;
            if (!reportReady|| !hasLogTmp)
            {
                string text = string.Format("{0}\\Template", this.m_zipPath);
                SharpZipLibHelper.UnZip(this.m_TemplatezipPath, text, "pps");
                string[] files = Directory.GetFiles(text);
                if (files.Length > 0)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        string fileName = Path.GetFileName(files[i]);
                        string extension;
                        if ((extension = Path.GetExtension(files[i])) != null)
                        {
                            if (!(extension == ".cfg") && !(extension == ".db"))
                            {
                                if (extension == ".doc" || extension == ".docx")
                                {
                                    if (!reportReady)
                                    {
                                        File.Copy(files[i], string.Format("{0}\\{1}", this.m_reportPath, fileName));
                                    }
                                }
                                else if (extension == ".html")
                                {
                                    string logTplFilePath = string.Format("{0}\\{1}", this.m_logTmpPath, fileName);
                                    if (File.Exists(logTplFilePath))
                                        File.Delete(logTplFilePath);

                                    File.Copy(files[i], string.Format("{0}\\{1}", this.m_logTmpPath, fileName));
                                }
                            }
                        }
                    }
                    result = true;
                }
                if (!reportReady)
                {
                    string[] directories = Directory.GetDirectories(text);
                    for (int j = 0; j < directories.Length; j++)
                    {
                        string fileName2 = Path.GetFileName(directories[j]);
                        Directory.Move(directories[j], string.Format("{0}\\{1}", this.m_reportPath, fileName2));
                    }
                }
                else
                {
                    string[] directories = Directory.GetDirectories(text);
                    for (int j = 0; j < directories.Length; j++)
                    {
                        string name = Path.GetFileName(directories[j]);
                        string[] olds = Directory.GetFiles(string.Format("{0}\\{1}", this.m_reportPath, name)).Select(t => Path.GetFileName(t)).ToArray();
                        string[] fileslist = Directory.GetFiles(directories[j]);
                        for (int s = 0; s < fileslist.Length; s++)
                        {
                            string filename = Path.GetFileName(fileslist[s]);
                            if (!olds.Contains(filename))
                            {
                                File.Copy(fileslist[s], string.Format("{0}\\{1}\\{2}", this.m_reportPath, name, filename));
                            }
                        }

                    }
                }
                Directory.Delete(text, true);
                return result;
            }
            return true;
        }
    }
}
