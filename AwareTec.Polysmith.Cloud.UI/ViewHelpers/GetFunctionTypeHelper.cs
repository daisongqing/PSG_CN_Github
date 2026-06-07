using AwareTec.Polysmith.Cloud.UI.DataModels.EnumModels;
using AwareTec.Polysmith.Cloud.UI.DataModels.Global;
using RestfulWebRequest.EnumModels.EnumModels4Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.ViewHelpers
{
    public class GetFunctionTypeHelper
    {
        /// <summary>
        /// 根据订单状态获取双击事件所指引的右键菜单选项
        /// </summary>
        /// <param name="orderStatus"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static EnumFunction Get(OrderStatus orderStatus)
        {
            switch (orderStatus)
            {
                case OrderStatus.ToBeMonitored:
                    return EnumFunction.开始监测;
                case OrderStatus.ToBeUpload:
                    return EnumFunction.上传edf;
                case OrderStatus.ToBeAnalysis:
                case OrderStatus.ToBeAudit:
                case OrderStatus.Completed:
                    return EnumFunction.进入回放;
                default:
                    return EnumFunction.None;
            }
        }
            
        public static EnumFunction[] GetVisibleFunctions(OrderStatus orderStatus)
        {
            switch (orderStatus)
            {
                case OrderStatus.ToBeMonitored:
                    return new EnumFunction[] {EnumFunction.开始监测};
                case OrderStatus.ToBeUpload:
                    return new EnumFunction[] { EnumFunction.继续监听, EnumFunction.上传edf };
                case OrderStatus.ToBeAnalysis:
                    return new EnumFunction[] { EnumFunction.进入回放, EnumFunction.查看数据, EnumFunction.下载edf };
                case OrderStatus.ToBeAudit:
                    return new EnumFunction[] { EnumFunction.进入回放, EnumFunction.查看数据, EnumFunction.下载edf };
                case OrderStatus.Completed:
                    return new EnumFunction[] { EnumFunction.进入回放, EnumFunction.查看数据, EnumFunction.下载edf};
                default:
                    return new EnumFunction[0];
            }
        }
    }
}
