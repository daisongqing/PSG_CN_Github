using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.User;
using AwareTec.Polysmith.Util.XmlUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.SpecificXmlHelper
{
    public class UserOperationConfigXmlHelper : BaseSpecificXmlHelper
    {
        public UserOperationConfigXmlHelper(string xmlPath) : base(xmlPath){}

        public UserOperationConfig Read()
        {
            var xmlHelper = new XmlHelper<UserOperationConfig>(XmlReader.Create(_xmlPath));
            return xmlHelper.ToObject();
        }

        /// <summary>
        /// 修改并保存
        /// </summary>
        /// <param name="newUserOperationConfig"></param>
        /// <returns>不需修改或修改成功均返回true, 需要修改但修改失败返回false</returns>
        public bool Modify(UserOperationConfig newUserOperationConfig)
        {
            var xmlHelper = new XmlHelper<UserOperationConfig>(XmlReader.Create(_xmlPath));
            var originalUserOperationConfig = xmlHelper.ToObject();
            Type type = newUserOperationConfig.GetType();

            bool isEdited = false;
            foreach(var propertyInfo in type.GetProperties())
            {
                var oldValue = propertyInfo.GetValue(originalUserOperationConfig);
                var newValue = propertyInfo.GetValue(newUserOperationConfig);
                string strvalue = newValue.ToString();
                if(newValue.GetType() == typeof(bool))
                {
                    strvalue = strvalue.ToLower();
                }
                if(oldValue == null)
                {
                    if (oldValue.Equals(newValue))
                        continue;

                    isEdited = true;

                    if(newValue != null)
                    {
                        if (!xmlHelper.ModifyElement(propertyInfo.Name, strvalue))
                            return false;
                    }
                    else
                    {
                        if(!xmlHelper.ModifyElement(propertyInfo.Name, string.Empty))
                            return false;
                    }
                }
                else
                {
                    if (newValue == null)
                        continue;

                    isEdited = true;
                    if (!xmlHelper.ModifyElement(propertyInfo.Name, strvalue))
                        return false;
                }
            }
            if (isEdited)
                return xmlHelper.WriteToFile(_xmlPath) == 0;
            return true;
        }
    }
}
