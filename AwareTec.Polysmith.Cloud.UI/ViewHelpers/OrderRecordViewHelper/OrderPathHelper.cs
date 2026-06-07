using AwareTec.Polysmith.Cloud.UI.DataModels;
using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.SpecificXmlHelper;
using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.User;
using AwareTec.Polysmith.Cloud.UI.DataModels.Global;
using AwareTec.Polysmith.Util.PathUtils;
using RestfulWebRequest.RestfulTable.RestfulResponseTable.BuiltInType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.ViewHelpers.OrderRecordViewHelper
{
    public class OrderPathHelper
    {
        /// <summary>
        /// 尽可能去寻找Edf
        /// </summary>
        /// <returns></returns>
        public static bool LookForEdfAsMuchAsPossible(OrderItem item, out string edfPath)
        {
            edfPath = null;

            //获取订单记录中OrderPath
            if (GetOrderPath(item, out string orderPath)
                == OrderPathSearchResult.Ok)
            {
                if (EdfPathExists(orderPath,
                                  item,
                                  out string existEdfPath))
                {
                    edfPath = existEdfPath;
                    return true;
                }
            }

            //未找到则去FullUser指定的云平台存储路径找
            string thisOrderCloudDir = GetAndGenerateOrderSavePath(item);
            if (thisOrderCloudDir == null)
                throw new Exception("未找到云平台菲诗奥存储路径");
            if(EdfPathExists(thisOrderCloudDir, item, out string existEdfPathInCloud))
            {
                bool isWritten = WriteNewOrderPath(item, existEdfPathInCloud);
                if(!isWritten)
                    throw new Exception("修改路径记录时失败");
                return true;
            }
            else
            {
                //仍未找到就清空本地记录数据
               bool isWritten = WriteNewOrderPath(item, string.Empty);
               if (!isWritten)
                    throw new Exception("修改路径记录时失败");

               return false;
            }
        }


        /// <summary>
        /// 获取用户订单路径记录下存储的path路径
        /// </summary>
        /// <param name="item"></param>
        /// <param name="orderPath"></param>
        /// <returns>
        /// 获取成功返回null,
        /// 获取失败返回错误信息
        /// </returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="Exception"></exception>
        public static OrderPathSearchResult GetOrderPath(OrderItem item, 
                                                         out string orderPath)
        {
            orderPath = string.Empty;
            try
            {
                if (item == null)
                    return OrderPathSearchResult.ArgumentNull;

                var orderPaths = GlobalSingleton.Instance.User.OrderPath;
                if (orderPaths == null ||
                    orderPaths.Count == 0)
                    return OrderPathSearchResult.OrderPathOfUserMissing;

                var find = orderPaths.Find(x => x.OrderId.Equals(item.id));
                if (find == null)
                    return OrderPathSearchResult.OrderPathOfOrderMissing;

                if (string.IsNullOrWhiteSpace(find.Path))
                    return OrderPathSearchResult.DirPathOfOrderMissing;

                if (!StringPath.PathExists(find.Path))
                    return OrderPathSearchResult.DirPathIsNotFound;

                orderPath = find.Path; 
                return OrderPathSearchResult.Ok;
            }
            catch (Exception ex)
            {
                return OrderPathSearchResult.UnknownError;
            }
        }

        /// <summary>
        /// 判断该目录下是否存在匹配的edf文件
        /// </summary>
        /// <param name="dir">查找的目录</param>
        /// <param name="edfPath">返回Edf路径</param>
        /// <returns></returns>
        public static bool EdfPathExists(string dir,
                                         OrderItem item,
                                         out string edfPath)
        {
            edfPath = null;

            if (string.IsNullOrWhiteSpace(dir) ||
               (!StringPath.PathExists(dir)))
                return false;

            if (item == null)
                return false;
            if (Path.GetExtension(dir).ToLower() == ".edf")
            {
                edfPath = dir;
                return true;
            }
            var edfFiles = Directory.GetFiles(dir).ToList().FindAll(x => x.Contains(".edf") || x.Contains(".EDF"));
            foreach (var edfFile in edfFiles)
            {
                var edf = EDF.Default.ConvertToEDF(EDF.Default.ReadFile(edfFile, false));
                if (edf.getMatchKey().Equals(item.id))
                {
                    edfPath = edfFile;
                    return true;
                }
                    
            }
            return false;
        }

        /// <summary>
        /// 获取并生成表单数据存储路径对应的订单文件夹
        /// </summary>
        /// <param name="item"></param>
        /// <returns>成功返回订单文件夹, 失败返回null</returns>
        public static string GetAndGenerateOrderSavePath(OrderItem item)
        {
            if (item == null)
                return null;

            return GetAndGenerateOrderSavePath(item.id);
        }
        /// <summary>
        /// 获取并生成表单数据存储路径对应的订单文件夹
        /// </summary>
        /// <param name="item"></param>
        /// <returns>成功返回订单文件夹, 失败返回null</returns>
        public static string GetAndGenerateOrderSavePath(string OrderID)
        {
            try
            {
                if (string.IsNullOrEmpty( OrderID ))
                    return null;
                var fullUserConfig = GlobalSingleton.Instance.FullUserConfig;

                if (fullUserConfig == null)
                    return null;

                if (string.IsNullOrWhiteSpace(fullUserConfig.OrderPath) ||
                   (!StringPath.PathExists(fullUserConfig.OrderPath)))
                    return null;

                string thisOrderPath = fullUserConfig.OrderPath + "\\" + OrderID;

                if (!StringPath.PathExists(thisOrderPath))
                    StringPath.CreateDir(thisOrderPath);

                return thisOrderPath;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// 将新的订单文件夹路径写入用户订单路径记录
        /// </summary>
        /// <param name="item"></param>
        /// <param name="orderPath"></param>
        /// <returns></returns>
        public static bool WriteNewOrderPath(OrderItem item,
                                             string orderPath)
        {
            if(item == null ||
               orderPath == null)
                return false;

            var orderPaths = GlobalSingleton.Instance.User.OrderPath;
            var find = orderPaths.Find(x => x.OrderId.Equals(item.id));
            if (find == null)
            {
                orderPaths.Add(new OrderPath() 
                {
                    OrderId = item.id,
                    Path = orderPath,
                    Version = 1
                });
                GlobalSingleton.Instance.User.OrderPathXmlHelper.Modify(orderPaths);
                if (GetOrderPath(item, out string result) != OrderPathSearchResult.Ok)
                    return false;
            }
            else
            {
                if (find.Path.Equals(orderPath)) return true;
                find.Path = orderPath;
                bool isModified = GlobalSingleton.Instance.User.OrderPathXmlHelper.Modify(orderPaths);
                return isModified;
            }
            return true;
        }
    }

    public enum OrderPathSearchResult
    {
        [Description("查询成功")]
        Ok,
        [Description("参数为空")]
        ArgumentNull,
        [Description("用户的订单本地数据缺失")]
        OrderPathOfUserMissing,
        [Description("当前订单的对应数据缺失")]
        OrderPathOfOrderMissing,
        [Description("当前订单无对应的dir路径")]
        DirPathOfOrderMissing,
        [Description("当前订单对应的edf路径不存在")]
        DirPathIsNotFound,
        [Description("未知错误")]
        UnknownError
    }
}
