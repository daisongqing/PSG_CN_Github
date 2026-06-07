using RestfulWebRequest.RestfulTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.ViewModel.Base
{
    public interface IToDb
    {
        /// <summary>
        /// 组装相应模型
        /// </summary>
        /// <returns></returns>
        IRestfulTable ToDb();
    }
}
