using Newtonsoft.Json;
using RestfulWebRequest.EnumModels;
using RestfulWebRequest.RestfulAttribute;
using RestfulWebRequest.RestfulTable.RestfulResponseTable.BuiltInType;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RestfulWebRequest.Base
{
    public class HttpHelper
    {
        private const string COLON = ":";


        public static MultipartFormDataContent GetFormData(object obj)
        {
            var fieldInfos = obj.GetType().GetFields();
            string fileName = null;
            //获取待上传的文件名
            foreach (var fieldInfo in fieldInfos)
            {
                var value = fieldInfo.GetValue(obj);
                FileUploadAttribute attr = AttributeReader.Read(fieldInfo);
                if (attr == null)
                    continue;
                if (attr.IsFileName)
                    fileName = (string)value;
            }
            if (string.IsNullOrWhiteSpace(fileName))
                throw new Exception("上传文件必须带文件名");

            var formData = new MultipartFormDataContent();
            
            foreach (var fieldInfo in fieldInfos)
            {
                var type = fieldInfo.FieldType;
                var defaultValue = GetDefaultValue(type);
                var value = fieldInfo.GetValue(obj);
                var fieldName = fieldInfo.Name;
                FileUploadAttribute attr = AttributeReader.Read(fieldInfo);

                if (attr != null && attr.MultipartFormDataIgnore)
                    continue;

                if (value == null ? value == default :
                    value.Equals(defaultValue))
                    continue;

                switch (type.Name)
                {
                    case "Stream":
                        Stream stream = value as Stream;
                        formData.Add(new StreamContent(stream, (int)stream.Length), fieldName, fileName);
                        break;
                    default:
                        formData.Add(new StringContent(value.ToString()), fieldName);
                        break;
                }
            }
            return formData;
        }

        public static bool FillOnlyStreamModel(Type type, DownLoadFileModel file, out object obj)
        {
            obj = null;
            if (type.GetFields().Length != 1)
                return false;

            FieldInfo fieldInfo = type.GetFields().First();
            if (!fieldInfo.FieldType.Name.Equals("DownLoadFileModel"))
                return false;

            obj = type.Assembly.CreateInstance(type.FullName);
            fieldInfo.SetValue(obj, file);
            return true;
        }

        public static bool FillOnlyArrayModel(Type type, object[] array, out object obj)
        {
            obj = null;
            if (type.GetFields().Length != 1)
                return false;

            FieldInfo fieldInfo = type.GetFields().First();
            if (!fieldInfo.FieldType.BaseType.Name.Equals("Array"))
                return false;

            obj = type.Assembly.CreateInstance(type.FullName);
            fieldInfo.SetValue(obj, array);
            return true;
        }

        public static string SerializeObject(object obj, 
                                             string url,
                                             RequestMethod method,
                                             out bool noField)
        {
            noField = true;
            if (obj is null)
                return url;

            FieldInfo[] fieldInfos = null;
            if ((fieldInfos = obj.GetType().GetFields()).Length == 0)
                return url;

            StringBuilder sb = new StringBuilder();
            var pathParamFieldName = GetPathParamUrl(url, obj, fieldInfos, ref sb);
            if(method != RequestMethod.POST &&
               method != RequestMethod.PUT)
                GetQueryParamUrl(url, pathParamFieldName, obj, fieldInfos, ref sb);
            else
                noField = (!string.IsNullOrWhiteSpace(pathParamFieldName) &&
                            fieldInfos.Length == 1) ? true : false;
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
                if(type.AssemblyQualifiedName.Contains("RestfulWebRequest.EnumModels.EnumModels4Table"))
                    sb.Append(fieldName + "=" + (int)value + "&");
                else
                    sb.Append(fieldName + "=" + value + "&");
            }
            if (haveAppend)
                sb = sb.Remove(sb.Length - 1, 1);
        }

        /// <summary>
        /// 获取类型对应的默认值
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static object GetDefaultValue(Type t)
        {
            if (t.IsValueType)
                return Activator.CreateInstance(t);

            return null;
        }
    }
} 
