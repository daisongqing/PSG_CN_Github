using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace AwareTec.Polysmith.Util.XmlUtils
{
    public class XmlHelper<T>
    {
        public class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }

        private XElement XDoc { get; set; }

        /// <summary>
        /// 使用后会关闭reader解除占用。具体查看<see cref="TextReader"/>
        /// </summary>
        /// <param name="reader"></param>
        public XmlHelper(TextReader reader)
        {
            XDoc = XElement.Parse(reader.ReadToEnd());
            reader.Close();
        }
        /// <summary>
        /// 使用后会关闭reader解除占用。具体查看<see cref="XmlReader"/>
        /// </summary>
        /// <param name="reader"></param>
        public XmlHelper(XmlReader reader)
        {
            XDoc = XElement.Load(reader);
            reader.Close();
        }

        public XmlHelper(T xmlEntity)
        {
            XmlSerializer xs = new XmlSerializer(xmlEntity.GetType());
            var writer = new Utf8StringWriter();
            xs.Serialize(writer, xmlEntity);
            string xmlStr = writer.ToString();
            XDoc = XElement.Parse(xmlStr);
        }
        /// <summary>
        /// 修改元素的值
        /// </summary>
        /// <param name="element">元素名</param>
        /// <param name="value">值</param>
        /// <param name="xmlNamespace">命名空间</param>
        public bool ModifyElement(string element, string value, string xmlNamespace = null)
        {
            try
            {
                if (string.IsNullOrEmpty(xmlNamespace))
                {
                    var xElement = XDoc.Element(element);
                    xElement.Value = value;
                }
                else
                {
                    XNamespace ns = xmlNamespace;
                    var xElement = XDoc.Element(ns + element);
                    xElement.Value = value;
                }
                return true;
            }catch(Exception e)
            {
                return false;
            }
            
        }

        /// <summary>
        /// 修改多个嵌套元素特定检索的值
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="elementList"></param>
        public bool ModifyNestedElement(string element,
                                        string value, 
                                        string filterName, 
                                        string filterValue, 
                                        params string[] elementList)
        {
            if (elementList == null || elementList.Length == 0)
                return false;

            if (string.IsNullOrWhiteSpace(element) ||
                value == null ||
               string.IsNullOrWhiteSpace(filterName) ||
               string.IsNullOrWhiteSpace(filterValue))
                return false;
            
            IEnumerable<XElement> find = null;
            for(int index = 0; index < elementList.Length; index++)
            {
                var findElement = elementList[index];
                if(index == 0)
                    find = XDoc.Elements(findElement);
                else
                {
                    if (find == null)
                        return false;
                    find = find.Elements(findElement);
                }
            }
            if(find == null) return false;
            var filter = find.Elements(filterName); 

            if(filter == null) return false;

            var filterResult = filter.Single(x => x.Value.Equals(filterValue));
            if(filterResult == null) return false;

            var specificElement = filterResult.Parent; 
            if (specificElement == null) return false;

            var finalElement = specificElement.Element(element);
            if (finalElement == null)
                specificElement.Add(new XElement(element, value));
            else
                finalElement.Value = value;

            return true;
        }

        public bool AddNestedElement(string element,
                                     object newValue,
                                     params string[] elementList)
        {
            if (string.IsNullOrWhiteSpace(element))
                return false;

            XElement findElement = XDoc;
            if (elementList != null && elementList.Length > 0)
            {
                foreach(var item in elementList)
                {
                    findElement = findElement.Element(item);
                    if(findElement == null) return false;
                }
            }
            IEnumerable<XElement>  addRootElement = findElement.Elements(element);

            Type type = newValue.GetType();
            if (addRootElement != null && addRootElement.Count() != 0)
            {
                addRootElement.Last().AddAfterSelf(new XElement(type.Name));
                findElement = findElement.Elements(type.Name).Last();
            }
            else
            {
                findElement.Add(new XElement(type.Name));
                findElement = findElement.Element(type.Name);
            }

            foreach (var property in type.GetProperties())
                findElement.Add(new XElement(property.Name, property.GetValue(newValue) ?? string.Empty));
            return true;
        }

        public override string ToString() => XDoc.ToString();

        public virtual T ToObject()
        {

            TextReader reader = new StringReader(XDoc.ToString());
            XmlSerializer ser = new XmlSerializer(typeof(T));
            T obj = (T)ser.Deserialize(reader);
            return obj;
        }

        public static T ConvertXmlToObject(XmlReader reader)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            T obj = (T)ser.Deserialize(reader);
            return obj;
        }

        public static string ConvertObjectToXmlString(T obj)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            var writer = new Utf8StringWriter();
            xs.Serialize(writer, obj);
            return writer.ToString();
        }
        /// <summary>
        /// 修改元素
        /// </summary>
        /// <param name="reader">xml</param>
        /// <param name="element">元素</param>
        /// <param name="value">值</param>
        /// <param name="xmlNamespace">命名空间</param>
        /// <returns>xml字符串</returns>
        public static string ModifyElement(XmlReader reader, string element, string value, string xmlNamespace = null)
        {
            var doc = XElement.Load(reader);
            if (string.IsNullOrEmpty(xmlNamespace))
            {
                var xElement = doc.Element(element);
                xElement.Value = value;
            }
            else
            {
                XNamespace ns = xmlNamespace;
                var xElement = doc.Element(ns + element);
                xElement.Value = value;
            }
            return doc.ToString();
        }

        public int WriteToFile(string path)
        {
            try
            {
                XDoc.Save(path);
                return 0;
            }
            catch (Exception e)
            {
                return -1;
            }
        }
    }
}
