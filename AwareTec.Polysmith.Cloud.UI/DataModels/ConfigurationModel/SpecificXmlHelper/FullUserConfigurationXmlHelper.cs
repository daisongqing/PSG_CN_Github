using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.FullUser;
using AwareTec.Polysmith.Util.XmlUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.SpecificXmlHelper
{
    public class FullUserConfigurationXmlHelper : BaseSpecificXmlHelper
    {
        public FullUserConfigurationXmlHelper(string xmlPath) : base(xmlPath){}

        public FullUserConfigurationModel Read()
        {
            var xmlHelper = new XmlHelper<FullUserConfigurationModel>(XmlReader.Create(_xmlPath));
            return xmlHelper.ToObject();
        }

        /// <summary>
        /// 修改并保存
        /// </summary>
        /// <param name="newConfig"></param>
        /// <returns>不需修改或修改成功均返回true, 需要修改但修改失败返回false</returns>
        public bool Modify(FullUserConfigurationModel newConfig)
        {
            var xmlHelper = new XmlHelper<FullUserConfigurationModel>(XmlReader.Create(_xmlPath));
            var originalConfig = xmlHelper.ToObject();
            Type type = newConfig.GetType();

            bool isEdited = false;
            foreach (var propertyInfo in type.GetProperties())
            {
                var oldValue = propertyInfo.GetValue(originalConfig);
                var newValue = propertyInfo.GetValue(newConfig);
                if (newValue == null)
                    continue;
                string strvalue = newValue.ToString();
                if (newValue.GetType() == typeof(bool))
                {
                    strvalue = strvalue.ToLower();
                }
                if (oldValue == null)
                {
                    isEdited = true;

                    if (newValue != null)
                    {
                        if (!xmlHelper.ModifyElement(propertyInfo.Name, strvalue))
                            return false;
                    }
                    else
                    {
                        if (!xmlHelper.ModifyElement(propertyInfo.Name, string.Empty))
                            return false;
                    }
                }
                else
                {
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
