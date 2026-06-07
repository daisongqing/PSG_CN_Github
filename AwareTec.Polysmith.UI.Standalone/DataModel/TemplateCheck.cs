using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwareTec.Polysmith.Util;
using System.Configuration;
using pSystem.DataBaseHepler;
using System.Data;
namespace AwareTec.Polysmith.UI.DataModel
{
    /// <summary>
    /// 模板数据检查类
    /// </summary>
    public class TemplateCheck
    {
        private string m_TemplatezipPath = "";
        private string m_zipPath = "";
        private string m_dbTemplatePath = "";
        private string m_channelPath = "";
        private string m_reportPath = "";
        private string basePath = AppDomain.CurrentDomain.BaseDirectory;
        private static TemplateCheck m_Default = null;
        Configuration m_appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        private string m_dbName = "AwareTecSmith.db";
        private string m_dbPsw = "123456";
        private string m_logTmpPath = "";
        /// <summary>
        /// 单例模式
        /// </summary>
        public static TemplateCheck Default
        {
            get
            {
                return m_Default ?? (m_Default = new TemplateCheck());
            }
        }
       private bool m_ini = false;
        /// <summary>
        /// 构造函数
        /// </summary>
        public TemplateCheck()
        {
            m_zipPath = basePath + "Template";
            m_TemplatezipPath = string.Format("{0}\\Template.zip", m_zipPath);
            m_dbTemplatePath = string.Format("{0}AwareTecSmith.db", basePath);
            m_channelPath = string.Format("{0}Channel", basePath);
            m_reportPath = string.Format("{0}ReportTemplate", basePath);
            m_logTmpPath = string.Format("{0}Feature\\Log\\Resource", basePath);
            m_ini = bool.Parse(ConfigurationManager.AppSettings["FirstUpdate"]);
            if (m_ini)
            {
                m_appConfig.AppSettings.Settings["FirstUpdate"].Value = "false";
                m_appConfig.Save();
                ConfigurationManager.RefreshSection("appSettings");
            }
        }
        /// <summary>
        /// 检查必备文件无缺失
        /// </summary>
        /// <returns></returns>
        public bool Check()
        {
            if (!File.Exists(this.m_TemplatezipPath))
            {
                return false;
            }
            bool flag = File.Exists(this.m_dbTemplatePath);
            bool flag2 = Directory.Exists(this.m_channelPath);
            if (!flag2)
            {
                Directory.CreateDirectory(this.m_channelPath);
            }
            bool flag3 = Directory.Exists(this.m_reportPath);
            if (!flag3)
            {
                Directory.CreateDirectory(this.m_reportPath);
            }
            bool hasLogTmp= Directory.Exists(this.m_logTmpPath);
            if (!hasLogTmp)
            {
                Directory.CreateDirectory(this.m_logTmpPath);
            }
            bool result = false;
            if (!flag || !flag2 || !flag3 || m_ini)
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
                            if (!(extension == ".db"))
                            {
                                if (!(extension == ".cfg"))
                                {
                                    if (extension == ".doc" || extension == ".docx")
                                    {
                                        if (!flag3)
                                        {
                                            File.Copy(files[i], string.Format("{0}\\{1}", this.m_reportPath, fileName));
                                        }
                                    }
                                    else if(extension == ".html")
                                    {
                                        string logTplFilePath = string.Format("{0}\\{1}", this.m_logTmpPath, fileName);
                                        if (File.Exists(logTplFilePath))
                                            File.Delete(logTplFilePath);

                                        File.Copy(files[i], string.Format("{0}\\{1}", this.m_logTmpPath, fileName));
                                    }
                                }
                                else if (!flag2)
                                {
                                    File.Copy(files[i], string.Format("{0}\\{1}", this.m_channelPath, fileName));
                                }
                            }
                            else
                            {
                                if (!flag)
                                {
                                    if (!(fileName == "AwareTecSmith.db"))
                                    {
                                        return false;
                                    }
                                    File.Copy(files[i], this.m_dbTemplatePath);
                                }
                                else if (m_ini)
                                {
                                    AddCoulum(files[i],m_dbTemplatePath);
                                }
                            }
                        }
                    }
                    result = true;
                }
                if (!flag3)
                {
                    string[] directories = Directory.GetDirectories(text);
                    for (int j = 0; j < directories.Length; j++)
                    {
                        string fileName2 = Path.GetFileName(directories[j]);
                        Directory.Move(directories[j], string.Format("{0}\\{1}", this.m_reportPath, fileName2));
                    }
                }
                else if (m_ini)
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
            if (m_ini)
                m_ini = false;
            return true;
        }

        private void AddCoulum(string newdbPath, string olddbPath)
        {
            SQLiteHepler new_sqlCommand = new SQLiteHepler(string.Format("Data Source={0};Password={1};", newdbPath, m_dbPsw));
            SQLiteHepler old_sqlCommand = new SQLiteHepler(string.Format("Data Source={0};Password={1};", olddbPath, m_dbPsw));
            List<string> allTable = new_sqlCommand.GetAllTableName();
            List<string> allTable2 = old_sqlCommand.GetAllTableName();
            for (int i = 0; i < allTable.Count; i++)
            {
                if (!allTable2.Contains(allTable[i]))
                {
                    string key = "sourcedb";
                    //连接新数据库的字符串
                    string attchPath = string.Format("attach database '{0}' AS {2} key '{1}';", newdbPath, m_dbPsw, key);
                    ///在老的数据库中创建缺失的表，同时从新数据库复制数据到新表
                    string createTableSql = string.Format("{0} create table {1} as select * from {2}.{1};", attchPath, allTable[i], key);
                    old_sqlCommand.Command(createTableSql);
                    //old_sqlCommand.Command(string.Format("{0} create table {1} as select * from {2}.{1}; insert into {1}  select * from {2}.{1};", attchPath, allTable[i], key));
                }
                else
                {
                    DataTable dt = new_sqlCommand.Select(string.Format("PRAGMA  table_info([{0}])", allTable[i]));
                    DataTable dt2 = old_sqlCommand.Select(string.Format("PRAGMA  table_info([{0}])", allTable[i]));
                    ///对老表缺失的列进行补充
                    if (dt.Rows.Count != dt2.Rows.Count)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            DataRow[] dr = dt2.Select(string.Format("name ='{0}'", item[1]));
                            if (dr.Length == 0)
                            {
                                //新增列
                                var defaultVal = item[4];
                                if(defaultVal.ToString() == string.Empty)
                                    old_sqlCommand.Command(string.Format("alter table {0}  ADD COLUMN {1} {2}", allTable[i], item[1], item[2]));
                                else
                                    old_sqlCommand.Command(string.Format("alter table {0}  ADD COLUMN {1} {2} default {3}", allTable[i], item[1], item[2], item[4]));

                                #region 拷贝新列的列值
                                string selectSql = string.Format("select ID, {1} from {0}", allTable[i], item[1]);
                                DataTable data = new_sqlCommand.Select(selectSql);
                                if (data.Rows.Count > 0)
                                {
                                    foreach (DataRow r in data.Rows)
                                    {
                                        StringBuilder sb = new StringBuilder();
                                        string key = "sourcedb";
                                        //连接新数据库的字符串
                                        string attchPath = string.Format("attach database '{0}' AS {2} key '{1}';", newdbPath, m_dbPsw, key);
                                        sb.Append(attchPath);

                                        string id = r[0].ToString();
                                        string colValue = r[1].ToString();
                                        string updateSql = string.Format("update {0} set {1} = {2} where ID = {3}",
                                                                        allTable[i],
                                                                        item[1],
                                                                        colValue,
                                                                        id);
                                        sb.Append(updateSql);
                                        old_sqlCommand.Command(sb.ToString());
                                    }
                                }

                                #endregion
                            }
                        }
                    }
                    else
                    {
                        ///表单有新增的给老的数据库进行补充
                        DataTable data = new_sqlCommand.Select(string.Format("select * from {0}", allTable[i]));
                        if (data.Rows.Count > 0)
                        {
                            StringBuilder sb = new StringBuilder();
                            string key = "sourcedb";
                            //连接新数据库的字符串
                            string attchPath = string.Format("attach database '{0}' AS {2} key '{1}';", newdbPath, m_dbPsw, key);
                            sb.Append(attchPath);
                            sb.AppendFormat("insert into {0} select * from {1}.{0} where {1}.{0}.ID not in (select {0}.ID from {0});", allTable[i], key);
                            old_sqlCommand.Command(sb.ToString());
                        }
                    }
                }
            }
            old_sqlCommand.Close();
            new_sqlCommand.Close();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}
