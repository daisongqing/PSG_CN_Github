using AwareTec.Polysmith.Cloud.UI.DataModels.ViewModel.Base;
using AwareTec.Polysmith.Util.EnumUtils;
using RestfulWebRequest.EnumModels.EnumModels4Table;
using RestfulWebRequest.RestfulTable;
using RestfulWebRequest.RestfulTable.RestfulRequestTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.ViewModel.ToDb
{
    public class OrderRecordSearchViewModel : IToDb
    {
        private bool? _searchDateTime;
        private string _orderNumber;
        private string _patientName;
        private DateTime? _startDateTime;
        private DateTime? _endDateTime;
        private string _progressStatus;
        private OrderStatus _orderStatus;

        public string ProgressStatus
        {
            get => _progressStatus;
            set
            {
                if (value == null)
                    return;

                _progressStatus = value;

                bool isFind = false;
                foreach (OrderStatus item in Enum.GetValues(typeof(OrderStatus)))
                {
                    if (EnumHelper.GetDescription(item).Equals(_progressStatus))
                    {
                        isFind = true;
                        _orderStatus = item;
                    }  
                }
                if (!isFind) _orderStatus = OrderStatus.Empty;
            }
        }

        public bool? SearchDateTime
        {
            get => _searchDateTime;
            set
            {
                if (value == null)
                    return;

                _searchDateTime = value;
            }
            
        }

        public string OrderNumber
        {
            get => _orderNumber;
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                    return;
                _orderNumber = value;
            }
        }

        public string PatientName
        {
            get => _patientName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    return;
                _patientName = value;
            }
            
        }

        public DateTime? StartDateTime
        {
            get => _startDateTime;
            set
            {
                if (value == null)
                    return;

                _startDateTime = value;
            }
            
        }

        public DateTime? EndDateTime
        {
            get => _endDateTime;
            set
            {
                if (value == null)
                    return;
                _endDateTime = value;
            }
        }

        public IRestfulTable ToDb()
        {
            int? status = null;
            if (_orderStatus != OrderStatus.Empty)
                status = (int)_orderStatus;
            return new GetMyOrderRequestModel()
            {
                Filter = _orderNumber,
                StartTime = _searchDateTime == null ||
                            _searchDateTime == false ?
                            null : _startDateTime,
                EndTime = _searchDateTime == null ||
                            _searchDateTime == false ?
                            null : _endDateTime,
                PatientName = _patientName,
                Status = status
            };
        }
    }
}
