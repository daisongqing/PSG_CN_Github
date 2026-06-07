using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

namespace AwareTec.Polysmith.Util
{
    /// <summary>
    /// 通道管理
    /// </summary>
    public class ChannelManage
    {
        private static ChannelManage m_Default = null;
        /// <summary>
        /// 通道管理的实例
        /// </summary>
        public static ChannelManage Default
        {
            get
            {
                return m_Default ?? (m_Default = new ChannelManage());
            }
        }
        private string m_BasePath = AppDomain.CurrentDomain.BaseDirectory + "Channel";
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
                int len = filepath.Length + 1;
                string[] names = Directory.GetFiles(filepath, "*.cfg");
                for (int i = 0; i < names.Length; i++)
                    ret.Add(Path.GetFileName(names[i]));
            }
            return ret;
        }
        /// <summary>
        /// 读取通道信息
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
        /// 获取配置表
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public DataTable oldReadChannelConfig(string path)
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
        /// 创建一个空通道配置表格
        /// </summary>
        /// <returns></returns>
        public DataTable oldCreatEmptyTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("State", typeof(bool));
            dt.Columns.Add("strName");
            dt.Columns.Add("TimeSpan");
            dt.Columns.Add("MaxValue");
            dt.Columns.Add("MinValue");
            dt.Columns.Add("Index", typeof(int));
            dt.Columns.Add("ID");
            dt.Columns.Add("Sensitivity");
            dt.Columns.Add("ZoomRate");
            dt.Columns.Add("SingleNotch");
            dt.Columns.Add("HighPass");
            dt.Columns.Add("LowPass");
            dt.Columns.Add("PixelEnable", typeof(bool));
            dt.Columns.Add("ColorSelect", typeof(Color));
            dt.Columns.Add("Reserve");
            return dt;
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
            dt.Columns.Add("TimeSpan");
            dt.Columns.Add("MaxValue");
            dt.Columns.Add("MinValue");
            dt.Columns.Add("Index", typeof(int));
            dt.Columns.Add("ID");
            dt.Columns.Add("Sensitivity");
            dt.Columns.Add("ZoomRate");
            dt.Columns.Add("SingleNotch");
            dt.Columns.Add("HighPass");
            dt.Columns.Add("LowPass");
            dt.Columns.Add("PixelEnable", typeof(bool));
            dt.Columns.Add("IsShowValue", typeof(bool));
            dt.Columns.Add("CalibrationsVisible", typeof(bool));
            dt.Columns.Add("Antipole", typeof(bool));
            dt.Columns.Add("ColorSelect", typeof(Color));
            dt.Columns.Add("Reserve");
            return dt;
        }
        /// <summary>
        /// 读取通道信息
        /// </summary>
        /// <returns></returns>
        public DataTable ReadChannelConfig(string path)
        {
            DataTable dt = CreatEmptyTable();
            if (!string.IsNullOrEmpty(path))
            {
                if (File.Exists(path))
                {
                    XmlDocument doc = new XmlDocument();
                    StringBuilder sb = new StringBuilder();
                    doc.Load(path);
                    XmlNode rootNode = doc.SelectSingleNode("Data");
                    if (rootNode != null)
                    {
                        XmlNodeList nodes = rootNode.SelectNodes("Channel");
                        foreach (XmlNode node in nodes)
                        {
                            DataRow dr = dt.NewRow();
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                XmlNode n = node.SelectSingleNode(dt.Columns[i].ColumnName);
                                if (n == null)
                                {
                                    dr[i] = Activator.CreateInstance(dt.Columns[i].DataType);
                                }
                                else
                                {
                                    if (dt.Columns[i].DataType == typeof(Color))
                                    {
                                        int result;
                                        string value = n.InnerText;
                                        if (int.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out result))
                                            dr[i] = Color.FromArgb(result);
                                        else
                                            dr[i] = Color.FromName(value);
                                    }
                                    else
                                        dr[i] = n.InnerText;
                                }
                            }
                            dt.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        return oldReadChannelConfig(path);
                    }
                }
            }
            return dt;
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
        /// <summary>
        /// 保存配置信息
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="path"></param>
        public void SaveChannelConfig(DataTable dt, string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            //if (!File.Exists(path))
            //{
            //    //创建类型声明节点  
            //    XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            //    xmlDoc.AppendChild(node);
            //}
            //else
            {
                xmlDoc.RemoveAll();
                //创建类型声明节点  
                XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
                xmlDoc.AppendChild(node);
            }
            //创建根节点  
            XmlNode root = xmlDoc.CreateElement("Data");
            xmlDoc.AppendChild(root);
            int colCnt = dt.Columns.Count;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, "Channel", null);
                for (int j = 0; j < colCnt; j++)
                {
                    string strvalue = dt.Rows[i][j].ToString();
                    if (dt.Columns[j].DataType == typeof(Color))
                    {
                        string[] ss = strvalue.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                        if (ss.Length == 2)
                        {
                            if (ss[1].Contains("="))
                            {
                                string[] ss2 = ss[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                strvalue = string.Format("{3:X2}{0:X2}{1:X2}{2:X2}", byte.Parse(ss2[1].Replace("R=", "")), byte.Parse(ss2[2].Replace("G=", "")), byte.Parse(ss2[3].Replace("B=", "")), byte.Parse(ss2[0].Replace("A=", "")));
                            }
                            else
                                strvalue = ss[1];
                        }
                    }
                    CreateNode(xmlDoc, node, dt.Columns[j].ColumnName, strvalue);
                }
                root.AppendChild(node);
            }
            xmlDoc.Save(path);
        }
    }
}
