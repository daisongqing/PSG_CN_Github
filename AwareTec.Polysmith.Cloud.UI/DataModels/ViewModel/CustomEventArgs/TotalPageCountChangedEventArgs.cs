using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.ViewModel.CustomEventArgs
{
    public class TotalPageCountChangedEventArgs : EventArgs
    {
        private int _totalPageCount;
        public int TotalPageCount => _totalPageCount;

        public TotalPageCountChangedEventArgs(int totalPageCount)
        {
            _totalPageCount = totalPageCount;
        }
    }
}
