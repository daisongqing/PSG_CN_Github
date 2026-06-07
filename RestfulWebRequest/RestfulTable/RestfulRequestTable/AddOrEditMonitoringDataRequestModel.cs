using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.RestfulTable.RestfulRequestTable
{
    /// <summary>
    /// 血氧等监测数据添加及修改 请求模型
    /// 此为API文档接口所在位置:添加<see cref="https://docs.apipost.cn/preview/b47318c2a9a0cdb4/39e5d5699afa56a1?target_id=9ec9863e-38b4-44e8-a62d-f27fd350985f#6399d407-a6a7-42fe-dbb0-af094309c663" />
    /// 此为API文档接口所在位置:修改<see cref="https://docs.apipost.cn/preview/b47318c2a9a0cdb4/39e5d5699afa56a1?target_id=9ec9863e-38b4-44e8-a62d-f27fd350985f#508f629d-136d-4eb6-8074-0c3c9fb40997" />
    /// </summary>
    public class AddOrEditMonitoringDataRequestModel : IRestfulTable
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
        public int? version;
    }
}
