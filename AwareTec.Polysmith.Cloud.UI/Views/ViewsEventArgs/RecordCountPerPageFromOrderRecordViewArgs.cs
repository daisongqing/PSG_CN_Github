using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.Views.ViewsEventArgs
{
    public class RecordCountPerPageFromOrderRecordViewArgs : EventArgs
    {
        private int _recordCountPerPage;
        public int RecordCountPerPage => _recordCountPerPage;

        public RecordCountPerPageFromOrderRecordViewArgs(int recordCountPerPage)
        {
            _recordCountPerPage = recordCountPerPage;
        }
    }
}
