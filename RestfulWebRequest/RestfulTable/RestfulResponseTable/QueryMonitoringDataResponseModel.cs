using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.RestfulTable.RestfulResponseTable
{
    public class QueryMonitoringDataResponseModel : IRestfulTable
    {
        /// <summary>
        /// 订单id
        /// </summary>
        public string orderId;
        /// <summary>
        /// 帧信息
        /// </summary>
        public string frameInfo;
        /// <summary>
        /// 血氧信息
        /// </summary>
        public string spO2Info;
        /// <summary>
        /// 事件标记信息
        /// </summary>
        public string eventMarkers;
        /// <summary>
        /// 定标信息
        /// </summary>
        public string calibrationInfo;
        /// <summary>
        /// 分析结果信息
        /// </summary>
        public string analysisResults;
        /// <summary>
        /// 版本号
        /// </summary>
        public int version;
    }
}
