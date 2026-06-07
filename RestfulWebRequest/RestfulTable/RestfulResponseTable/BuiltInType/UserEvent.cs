using RestfulWebRequest.EnumModels.EnumModels4Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.RestfulTable.RestfulResponseTable.BuiltInType
{
    public class UserEvent
    {
        public string id;
        public string userId;
        public int mode;
        public string name;
        public string description;
        public bool isAreaLabel;
        public string markerColor;
        public string selectedChannel;
        public EventType eventType;
        public string optionalChannel;
        public int predefinedId;
        public string hotkey;
        public double minTimeDomain;
        public bool isReadOnly;
    }
}
