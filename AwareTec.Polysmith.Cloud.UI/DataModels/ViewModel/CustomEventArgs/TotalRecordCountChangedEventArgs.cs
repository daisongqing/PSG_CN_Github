using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.ViewModel.CustomEventArgs
{
    public class TotalRecordCountChangedEventArgs : EventArgs
    {
        private int _totalRecordCount;
        public int TotalRecordCount => _totalRecordCount;

        public TotalRecordCountChangedEventArgs(int totalRecordCount)
        {
            _totalRecordCount = totalRecordCount;
        }
    }
}
