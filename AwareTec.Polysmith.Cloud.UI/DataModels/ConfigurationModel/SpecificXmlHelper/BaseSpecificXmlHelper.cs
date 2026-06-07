using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.SpecificXmlHelper
{
    public abstract class BaseSpecificXmlHelper
    {
        protected readonly string _xmlPath;

        protected BaseSpecificXmlHelper(string xmlPath)
        {
            _xmlPath = xmlPath;
        }
    }
}
