using AwareTec.Polysmith.Cloud.UI.DataModels.ViewModel.ToDb;
using System;

namespace AwareTec.Polysmith.Cloud.UI.Views.ViewsEventArgs
{
    public class QueryOrderByFilterEventArgs : EventArgs
    {
        private OrderRecordSearchViewModel _orderRecordSearchViewModel;
        public OrderRecordSearchViewModel OrderRecordSearchViewModel => _orderRecordSearchViewModel;

        public QueryOrderByFilterEventArgs(OrderRecordSearchViewModel orderRecordSearchViewModel)
        {
            _orderRecordSearchViewModel = orderRecordSearchViewModel;
        }
    }
}
