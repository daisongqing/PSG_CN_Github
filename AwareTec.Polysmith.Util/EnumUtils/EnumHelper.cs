using System;
using System.ComponentModel;
using System.Reflection;

namespace AwareTec.Polysmith.Util.EnumUtils
{
    public class EnumHelper
    {
        /// <summary>  
        /// 获取枚举描述
        /// </summary>  
        /// <param name="en">枚举</param>  
        /// <returns>返回枚举的描述 </returns>  
        public static string GetDescription(Object obj)
        {
            Type type = obj.GetType();   //获取类型  
            MemberInfo[] memberInfos = type.GetMember(obj.ToString());   //获取成员  
            if (memberInfos != null && memberInfos.Length > 0)
            {
                DescriptionAttribute[] attrs = memberInfos[0].GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];   //获取描述特性  
                if (attrs != null && attrs.Length > 0)
                {
                    return attrs[0].Description;    //返回当前描述
                }
            }
            return obj.ToString();
        }

        public static string GetDefaultValue(object obj, string propertyName)
        {
            AttributeCollection attributes = TypeDescriptor.GetProperties(obj)[propertyName].Attributes;
            if(attributes == null ||
               attributes.Count == 0)
                return null;

            DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)attributes[typeof(DefaultValueAttribute)];
            if(defaultValueAttribute == null)   return null;
            return defaultValueAttribute.Value.ToString();
        }
    }
}
