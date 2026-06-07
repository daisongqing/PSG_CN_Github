using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.RestfulAttribute
{
    public class AttributeReader
    {

        public static RestfulApiAttribute Read(Type type, string methodName)
        {
            MethodInfo methodInfo = type.GetMethod(methodName);
            if (methodInfo == null)
                return null;

            Attribute[] attrs = Attribute.GetCustomAttributes(methodInfo);
            if (attrs == null || attrs.Length != 1)
                return null;

            Attribute attr = attrs.First();

            if (!(attr is RestfulApiAttribute))
                return null;

            RestfulApiAttribute restfulApiAttribute = (RestfulApiAttribute)attr;

            return restfulApiAttribute;
        }

        public static FileUploadAttribute Read(FieldInfo fieldInfo)
        {
            var attrs = (FileUploadAttribute[])fieldInfo.GetCustomAttributes(typeof(FileUploadAttribute), false);

            if (attrs == null || attrs.Length != 1)
                return null;

            Attribute attr = attrs.First();

            if (!(attr is FileUploadAttribute))
                return null;

            FileUploadAttribute restfulApiAttribute = (FileUploadAttribute)attr;

            return restfulApiAttribute;
        }
    }
}
