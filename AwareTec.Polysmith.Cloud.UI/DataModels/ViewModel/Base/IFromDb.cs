using RestfulWebRequest.RestfulTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.ViewModel.Base
{
    public interface IFromDb
    {
        void FromDb(IRestfulTable restfulTable, object otherInitParams);
    }
}
