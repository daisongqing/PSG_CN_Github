using RestfulWebRequest.EnumModels;
using RestfulWebRequest.RestfulAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.Base
{
    public class UrlParameterConvert
    {
        private const string COLON = ":";

        public static string SerializeObject(object obj, 
                                             string url,
                                             RequestMethod method,
                                             out bool noField)
        {
            noField = false;
            if (obj is null)
                return string.Empty;

            FieldInfo[] fieldInfos = null;
            if ((fieldInfos = obj.GetType().GetFields()).Length == 0)
                return string.Empty;

            StringBuilder sb = new StringBuilder();
            var pathParamFieldName = GetPathParamUrl(url, obj, fieldInfos, ref sb);
            if ((!string.IsNullOrWhiteSpace(pathParamFieldName)) &&
                fieldInfos.Length == 1)
                noField = true;
            if(method != RequestMethod.POST)
                GetQueryParamUrl(url, pathParamFieldName, obj, fieldInfos, ref sb);
            return sb.ToString();
        }

        /// <summary>
        /// 判断url是否存在路径参数
        /// </summary>
        /// <returns></returns>
        static bool UrlExistPathParam(string url, out string pathParamFieldName, out int colonIndex)
        {
            pathParamFieldName = string.Empty;
            colonIndex = -1;
            if (string.IsNullOrWhiteSpace(url))
                return false;

            colonIndex = url.IndexOf(COLON);
            if (colonIndex == -1)
                return false;

            int lastIndex = url.Length - 1;
            pathParamFieldName = url.Substring(colonIndex + 1, lastIndex - colonIndex);
            return true;
        }

        /// <summary>
        /// 获取带路径参数的url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <param name="fieldInfos"></param>
        /// <param name="sb"></param>
        /// <returns>路径参数对应的类字段名称</returns>
        /// <exception cref="Exception"></exception>
        static string GetPathParamUrl(string url,
                                    object obj,
                                    FieldInfo[] fieldInfos,
                                    ref StringBuilder sb)
        {
            bool result = UrlExistPathParam(url, out string pathParamFieldName, out int colonIndex);
            if (!result)
            {
                sb.Append(url);
                return pathParamFieldName;
            }
            
            sb.Append(url.Substring(0, colonIndex));
            bool isFind = false;
            foreach (var fieldInfo in fieldInfos)
            {
                var type = fieldInfo.FieldType;
                var defaultValue = GetDefaultValue(type);
                var value = fieldInfo.GetValue(obj);
                
                if (fieldInfo.Name.Equals(pathParamFieldName))
                {
                    if (value == null ? value == default :
                        value.Equals(defaultValue))
                        throw new Exception("路径参数为必填项, 却未被赋值");

                    sb.Append(value);
                    isFind = true;
                }
            }
            if (!isFind)
                throw new Exception("url中检测到路径参数, 但类中字段名称与该路径参数不相符");
            return pathParamFieldName;
        }

        /// <summary>
        /// 获取带查询参数的url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="pathParamFieldName"></param>
        /// <param name="obj"></param>
        /// <param name="fieldInfos"></param>
        /// <param name="sb"></param>
        static void GetQueryParamUrl(string url,
                                     string pathParamFieldName,  
                                     object obj,
                                     FieldInfo[] fieldInfos,
                                     ref StringBuilder sb)
        {
            bool haveAppend = false;
            foreach (var fieldInfo in fieldInfos)
            {
                var type = fieldInfo.FieldType;
                var fieldName = fieldInfo.Name;
                var defaultValue = GetDefaultValue(type);
                var value = fieldInfo.GetValue(obj);
                if ((!string.IsNullOrWhiteSpace(pathParamFieldName)) && 
                    fieldName.Equals(pathParamFieldName))
                    continue;
                if (value == null ? value == default :
                    value.Equals(defaultValue))
                    continue;

                if (!haveAppend)
                {
                    sb.Append("?");
                    haveAppend = true;
                } 
                sb.Append(fieldName + "=" + value + "&");
            }
            if (haveAppend)
                sb = sb.Remove(sb.Length - 1, 1);
        }

        static bool GetNoField(string pathParamFieldName,
                                object obj,
                                FieldInfo[] fieldInfos)
        {
            if(string.IsNullOrWhiteSpace(pathParamFieldName))
                return false;

            if (fieldInfos.Length == 1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获取类型对应的默认值
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        static object GetDefaultValue(Type t)
        {
            if (t.IsValueType)
                return Activator.CreateInstance(t);

            return null;
        }
    }
} 
