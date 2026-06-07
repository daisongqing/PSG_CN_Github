using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AwareTec.Polysmith.UI.DataModel
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// 将对象转换为string字符串
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string ToJsonString(this object @this)
        {
            return JsonConvert.SerializeObject(@this);
        }

        /// <summary>
        /// 将xml对象转换xml字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string ToXmlString<T>(this T @this)
        {
            using (var stream = new MemoryStream())
            {
                var xml = new XmlSerializer(typeof(T));
                var ns = new XmlSerializerNamespaces();
                ns.Add("", "");//把命名空间设置为空，这样就没有命名空间了
                xml.Serialize(stream, @this, ns);
                stream.Position = 0;
                var sr = new StreamReader(stream);
                return sr.ReadToEnd();
            }
        }

        /// <summary>
        /// 将xml字符串转换成xml对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T ToXmlObject<T>(this string @this)
        {
            using (var stream = new StringReader(@this))
            {
                var xml = new XmlSerializer(typeof(T));
                return (T)xml.Deserialize(stream);
            }
        }        
    }
}
