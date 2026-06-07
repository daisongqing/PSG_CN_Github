using RestfulWebRequest.EnumModels.EnumModels4Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.RestfulTable.RestfulRequestTable
{
    /// <summary>
    /// 用户事件数据新增 请求模型
    /// 此为API文档接口所在位置<see cref="https://docs.apipost.cn/preview/b47318c2a9a0cdb4/39e5d5699afa56a1?target_id=9ec9863e-38b4-44e8-a62d-f27fd350985f#207dcba5-49bf-40f6-aa15-c855f0e33bf2" />
    /// </summary>
    public class AddUserEventRequestModel : IRestfulTable
    {
        public string userId;
        public ModeType? mode;
        public string name;
        public string description;
        public bool? isAreaLabel;
        public string markerColor;
        public string selectedChannel;
        public EventType? eventType;
        public string optionalChannel;
        public int? predefinedId;
        public string hotkey;
        public double? minTimeDomain;
        public bool? isReadOnly;
    }
}
