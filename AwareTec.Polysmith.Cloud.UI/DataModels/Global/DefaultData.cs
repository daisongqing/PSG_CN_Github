using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.Default;
using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.RequiredFileOrDir;
using AwareTec.Polysmith.Util.XmlUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.Global
{
    public class DefaultData
    {
        private List<DefaultEvent> _defaultEvents = null;
        public List<DefaultEvent> DefaultEvents => _defaultEvents;

        public DefaultData(List<RequiredSubPath> requiredSubPaths)
        {
            var defaultEventPath = requiredSubPaths.Find(x => x.PathType == EnumModels.RequiredFileType.DefaultEvent).Path;
            var xmlHelper = new XmlHelper<DefaultEvents>(XmlReader.Create(defaultEventPath));
            _defaultEvents = xmlHelper.ToObject().DefaultEvent.Cast<DefaultEvent>().ToList();
        }
    }
}
