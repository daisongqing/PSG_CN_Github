using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.Predefined;
using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.Predefined.total;
using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.RequiredFileOrDir;
using AwareTec.Polysmith.Cloud.UI.DataModels.EnumModels;
using AwareTec.Polysmith.Util.XmlUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.Global
{
    public class PredefinedData
    {
        private List<PredefinedCalibration> _predefinedCalibrations = null;
        private List<PredefinedReportStructure> _predefinedReportStructure = null;
        private List<PredefinedEvent> _predefinedEvents = null;
        private List<PredefinedChannel> _predefinedChannels = null;
        public List<PredefinedCalibration> PredefinedCalibrations => _predefinedCalibrations;
        public List<PredefinedReportStructure> PredefinedReportStructure => _predefinedReportStructure;
        public List<PredefinedEvent> PredefinedEvents => _predefinedEvents;
        public List<PredefinedChannel> PredefinedChannels => _predefinedChannels; 
        public PredefinedData(List<RequiredSubPath> requiredSubPaths)
        {
            foreach(var path in requiredSubPaths)
            {
                string filePath = path.Path;
                switch (path.PathType)
                {
                    case RequiredFileType.PredefinedEvent:
                        {
                            var xmlHelper = new XmlHelper<PredefinedEvents>(XmlReader.Create(filePath));
                            _predefinedEvents = xmlHelper.ToObject().PredefinedEvent.Cast<PredefinedEvent>().ToList();
                        }
                        break;
                    case RequiredFileType.PredefinedReportStructure:
                        {
                            var xmlHelper = new XmlHelper<PredefinedReportStructures>(XmlReader.Create(filePath));
                            _predefinedReportStructure = xmlHelper.ToObject().ReportStructure.Cast<PredefinedReportStructure>().ToList();
                        }
                        break;
                    case RequiredFileType.PredefinedCalibration:
                        {
                            var xmlHelper = new XmlHelper<PredefinedCalibrations>(XmlReader.Create(filePath));
                            _predefinedCalibrations = xmlHelper.ToObject().PredefinedCalibration.Cast<PredefinedCalibration>().ToList();
                        }
                        break;
                    case RequiredFileType.PredefinedChannel:
                        {
                            var xmlHelper = new XmlHelper<PredefinedChannels>(XmlReader.Create(filePath));
                            _predefinedChannels = xmlHelper.ToObject().PredefinedChannel.Cast<PredefinedChannel>().ToList();
                        }
                        break;
                }
            }
        }
    }
}
