using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.SpecificXmlHelper;
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
    public class OrderPathXmlHelper : BaseSpecificXmlHelper
    {

        public OrderPathXmlHelper(string xmlPath) : base(xmlPath){}

        /// <summary>
        /// 读取订单数据
        /// </summary>
        /// <returns></returns>
        public List<OrderPath> Read()
        {
            var xmlHelper = new XmlHelper<OrderPaths>(XmlReader.Create(_xmlPath));
            return xmlHelper.ToObject().OrderPath.Cast<OrderPath>().ToList();
        }

        /// <summary>
        /// 修改并保存
        /// </summary>
        /// <param name="orderPaths">修改或新增的对象列表</param>
        /// <returns></returns>
        public bool Modify(List<OrderPath> orderPaths)
        { 
            foreach(var item in orderPaths)
            {
                var xmlHelper = new XmlHelper<OrderPaths>(XmlReader.Create(_xmlPath));
                var originalOrderPaths = xmlHelper.ToObject().OrderPath.Cast<OrderPath>().ToList();

                var findNewInOld = originalOrderPaths.Find(x => x.OrderId.Equals(item.OrderId));

                //新增
                if (findNewInOld == null)
                {
                    xmlHelper.AddNestedElement("OrderPath", item, null);
                }
                else {
                    var type = item.GetType();
                    foreach(var property in type.GetProperties())
                    {
                        var newValue = property.GetValue(item);
                        var oldValue = property.GetValue(findNewInOld);
                        if(newValue == null &&
                           oldValue != null)
                        {
                            xmlHelper.ModifyNestedElement(property.Name,
                                                          string.Empty,
                                                          "OrderId",
                                                          item.OrderId,
                                                          "OrderPath");
                            continue;
                        }
                            
                        if (newValue != null && (!newValue.Equals(oldValue)))
                        {
                            xmlHelper.ModifyNestedElement(property.Name,
                                                          newValue.ToString(),
                                                          "OrderId",
                                                          item.OrderId,
                                                          "OrderPath");
                        }
                    }
                }

                if (xmlHelper.WriteToFile(_xmlPath) == 1)
                    return false;
            }
            GC.Collect();
            return true;
        }
    }
}
