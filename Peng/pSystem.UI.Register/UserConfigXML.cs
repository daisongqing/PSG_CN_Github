using System.Xml.Linq;

namespace pSystem.UI.Register
{
    /// <summary>
    /// xml文件读写类
    /// </summary>
    internal class UserConfigXML
    {
        private static object lockobj = new object();
        private string m_filename = "";
        public UserConfigXML(string fileName)
        {
            m_filename = @fileName;
        }
        /// <summary>
        /// 项目根节点
        /// </summary>
        public XElement Root { get; set; }

        /// <summary>
        /// 获取UserConfig数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string Get(string name)
        {
            if (Root == null)
            {
                try
                {
                    Root = XElement.Load(m_filename);
                }
                catch { return ""; }
            }
            return Root.Element(name).Attribute("Value").Value;
        }

        /// <summary>
        /// 设置UserConfig数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void Set(string name, object value)
        {
            lock (lockobj)
            {
                if (Root == null)
                {
                    try
                    {
                        Root = XElement.Load(m_filename);
                    }
                    catch { return; }
                }
                var xElement = Root.Element(name);
                xElement.Attribute("Value").SetValue(value.ToString());
                Root.Save(m_filename);
            }
        }
    }
}
