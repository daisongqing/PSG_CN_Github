using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.ViewModel.CustomEventArgs
{
    public class RecordCountPerPageChangedEventArgs
    {
        private int _recordCountPerPage;
        public int RecordCountPerPage => _recordCountPerPage;

        public RecordCountPerPageChangedEventArgs(int recordCountPerPage)
        {
            _recordCountPerPage = recordCountPerPage;
        }
    }
}
