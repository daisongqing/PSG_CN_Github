using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace AwareTec.Polysmith.Cloud.UI.ViewHelpers
{
    /// <summary>
    /// 通道管理
    /// </summary>
    public class ChannelManageCloud
    {
        #region 私有变量

        private static ChannelManageCloud m_Default = null;
        private string m_BasePath = string.Format("{0}{1}", DataModels.Global.GlobalSingleton.Instance.User.UserFolder, DataModels.Global.GlobalReadonlyString.PredefinedPath.USER_CHANNEL_DIR);

        #endregion

        #region 公有变量

        /// <summary>
        /// 通道管理的实例
        /// </summary>
        public static ChannelManageCloud Default
        {
            get
            {
                return m_Default ?? (m_Default = new ChannelManageCloud());
            }
        }

        /// <summary>
        /// 存储通道管理配置文件的基础路径
        /// </summary>
        public string ConfigruationBasePath
        {
            get
            {
                return m_BasePath;
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 老方法获取配置表
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private DataTable oldReadChannelConfig(string path)
        {
            DataTable dt = CreatEmptyTable();
            string strvalue = ReadIds(path);
            if (strvalue != "")
            {
                string[] strValues = strvalue.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                Color lclColor;
                int result;
                for (int i = 0; i < strValues.Length; i++)
                {
                    DataRow dr = dt.NewRow();
                    string[] ss = strValues[i].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    int len = ss.Length;
                    for (int s = 0; s < len; s++)
                    {
                        if (s == 13)///找到颜色栏
                        {
                            string value = ss[s];
                            if (int.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out result))
                                lclColor = Color.FromArgb(result);
                            else
                                lclColor = Color.FromName(value);
                            dr[s] = lclColor;
                        }
                        else
                            dr[s] = ss[s];
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt.Copy();
        }

        /// <summary>
        /// 老方法读取通道信息
        /// </summary>
        /// <returns></returns>
        private string ReadIds(string path)
        {
            string retValue = "";
            string ID = "Cap00000000";
            if (!string.IsNullOrEmpty(path))
            {
                if (File.Exists(path))
                {
                    XmlDocument doc = new XmlDocument();
                    StringBuilder sb = new StringBuilder();
                    using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
                    {
                        string sOneLine = "";
                        while (sr.Peek() > 0)
                        {
                            sOneLine = sr.ReadLine();
                            sb.AppendFormat("{0}\r\n", sOneLine);
                            System.Threading.Thread.Sleep(5);
                        }
                        sr.Close();
                    }
                    doc.LoadXml(sb.ToString());
                    XmlNode xmls = doc.SelectSingleNode("Captures");
                    foreach (XmlNode node in xmls.ChildNodes)
                    {
                        if (node.Name == ID)
                        {
                            retValue = node.Attributes["Value"].InnerText;
                            break;
                        }
                    }
                }
            }
            return retValue;
        }

        /// <summary>    
        /// 创建节点    
        /// </summary>    
        /// <param name="xmldoc"></param>  xml文档  
        /// <param name="parentnode"></param>父节点    
        /// <param name="name"></param>  节点名  
        /// <param name="value"></param>  节点值  
        ///   
        private void CreateNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
        {
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            parentNode.AppendChild(node);
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 获取配置方案名称
        /// </summary>
        /// <returns></returns>
        public List<string> GetCfgNames()
        {
            List<string> ret = new List<string>();
            string filepath = m_BasePath;
            if (Directory.Exists(filepath))
            {
                string[] names = Directory.GetFiles(filepath, "*.cfg");
                for (int i = 0; i < names.Length; i++)
                    ret.Add(Path.GetFileName(names[i]));
            }
            return ret;
        }

        /// <summary>
        /// 创建一个空通道配置表格
        /// </summary>
        /// <returns></returns>
        public DataTable CreatEmptyTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("State", typeof(bool));
            dt.Columns.Add("strName");
            dt.Columns.Add("MaxValue");
            dt.Columns.Add("MinValue");
            dt.Columns.Add("Index", typeof(int));
            dt.Columns.Add("ID");
            dt.Columns.Add("Sensitivity");
            dt.Columns.Add("SingleNotch");
            dt.Columns.Add("HighPass");
            dt.Columns.Add("LowPass");
            dt.Columns.Add("Antipole", typeof(bool));
            dt.Columns.Add("ColorSelect", typeof(Color));
            dt.Columns.Add("Reserve");
            dt.Columns.Add("DBaseLineVisible", typeof(bool));
            dt.Columns.Add("IsClone", typeof(bool));
            dt.Columns.Add("ChannelID");
            return dt;
        }

        /// <summary>
        /// 读取通道信息
        /// </summary>
        /// <returns></returns>
        public DataTable ReadChannelConfig(string path)
        {
            DataTable dataTable = CreatEmptyTable();
            if (!string.IsNullOrEmpty(path))
            {
                if (File.Exists(path))
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(path);
                    XmlNode rootNode = xmlDocument.SelectSingleNode("Data");
                    if (rootNode != null)
                    {
                        XmlNodeList xmlNodeList = rootNode.SelectNodes("Channel");
                        foreach (XmlNode xmlNode in xmlNodeList)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            for (int i = 0; i < dataTable.Columns.Count; i++)
                            {
                                XmlNode node = xmlNode.SelectSingleNode(dataTable.Columns[i].ColumnName);
                                if (node != null)
                                {
                                    if (dataTable.Columns[i].DataType == typeof(Color))
                                    {
                                        int result;
                                        string value = node.InnerText;
                                        if (int.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out result))
                                            dataRow[i] = Color.FromArgb(result);
                                        else
                                            dataRow[i] = Color.FromName(value);
                                    }
                                    else
                                    {
                                        dataRow[i] = node.InnerText;
                                    }
                                }
                                else
                                {
                                    dataRow[i] = Activator.CreateInstance(dataTable.Columns[i].DataType);
                                }
                            }
                            dataTable.Rows.Add(dataRow);
                        }
                    }
                    else
                    {
                        return oldReadChannelConfig(path);
                    }
                }
            }
            return dataTable;
        }

        /// <summary>
        /// 保存配置信息
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="path"></param>
        public void SaveChannelConfig(DataTable dt, string path)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.RemoveAll();

            //创建类型的声明节点
            XmlNode xmlNode = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", "");
            xmlDocument.AppendChild(xmlNode);

            //创建根节点
            XmlNode rootNode = xmlDocument.CreateElement("Data");
            xmlDocument.AppendChild(rootNode);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                XmlNode node = xmlDocument.CreateNode(XmlNodeType.Element, "Channel", null);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string nodeValue = dt.Rows[i][j].ToString();
                    if (dt.Columns[j].DataType == typeof(Color))
                    {
                        string[] ss = nodeValue.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                        if (ss.Length == 2)
                        {
                            if (ss[1].Contains("="))
                            {
                                string[] ss2 = ss[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                nodeValue = string.Format("{3:X2}{0:X2}{1:X2}{2:X2}", byte.Parse(ss2[1].Replace("R=", "")), byte.Parse(ss2[2].Replace("G=", "")), byte.Parse(ss2[3].Replace("B=", "")), byte.Parse(ss2[0].Replace("A=", "")));
                            }
                            else
                                nodeValue = ss[1];
                        }
                    }
                    CreateNode(xmlDocument, node, dt.Columns[j].ColumnName, nodeValue);
                }
                rootNode.AppendChild(node);
            }
            xmlDocument.Save(path);
        }

        #endregion




        /// <summary>
        /// 创建任何配置表格
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable CreatAnyEmptyTable(Type type)
        {
            DataTable dt = new DataTable();
            PropertyInfo[] propertyInfo = type.GetProperties();
            for (int i = 0; i < propertyInfo.Length; i++)
            {
                dt.Columns.Add(propertyInfo[i].Name, propertyInfo[i].PropertyType);
            }
            return dt;
        }

        /// <summary>
        /// 读取通道信息
        /// </summary>
        /// <returns></returns>
        public DataTable ReadAnyChannelConfig(string path,Type type)
        {
            DataTable dataTable = CreatAnyEmptyTable(type);
            if (!string.IsNullOrEmpty(path))
            {
                if (File.Exists(path))
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(path);
                    XmlNode rootNode = xmlDocument.SelectSingleNode("Data");
                    if (rootNode != null)
                    {
                        XmlNodeList xmlNodeList = rootNode.SelectNodes("Channel");
                        foreach (XmlNode xmlNode in xmlNodeList)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            for (int i = 0; i < dataTable.Columns.Count; i++)
                            {
                                XmlNode node = xmlNode.SelectSingleNode(dataTable.Columns[i].ColumnName);
                                if (node != null)
                                {
                                    if (dataTable.Columns[i].DataType == typeof(Color))
                                    {
                                        int result;
                                        string value = node.InnerText;
                                        if (int.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out result))
                                            dataRow[i] = Color.FromArgb(result);
                                        else
                                            dataRow[i] = Color.FromName(value);
                                    }
                                    else
                                    {
                                        dataRow[i] = node.InnerText;
                                    }
                                }
                                else
                                {
                                    dataRow[i] = Activator.CreateInstance(dataTable.Columns[i].DataType);
                                }
                            }
                        }
                    }
                    else
                    {
                        return oldReadChannelConfig(path);
                    }
                }
            }
            return dataTable;
        }
    }
}
