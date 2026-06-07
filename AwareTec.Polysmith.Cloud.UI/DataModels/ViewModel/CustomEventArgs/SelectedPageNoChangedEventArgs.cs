using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.ViewModel.CustomEventArgs
{
    public class SelectedPageNoChangedEventArgs : EventArgs
    {
        private int _selectedPageNo;
        public int SelectedPageNo => _selectedPageNo;

        public SelectedPageNoChangedEventArgs(int selectedPageNo)
        {
            _selectedPageNo = selectedPageNo;
        }
    }
}
