using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.Base
{
    public class CustomAttributeReader
    {
        public static ViewModelAttribute Read(PropertyInfo propertyInfo)
        {
            var attrs = (ViewModelAttribute[])propertyInfo.GetCustomAttributes(typeof(ViewModelAttribute), false);

            if (attrs == null || attrs.Length != 1)
                return null;

            Attribute attr = attrs.First();

            if (!(attr is ViewModelAttribute))
                return null;

            ViewModelAttribute restfulApiAttribute = (ViewModelAttribute)attr;

            return restfulApiAttribute;
        }
    }
}
