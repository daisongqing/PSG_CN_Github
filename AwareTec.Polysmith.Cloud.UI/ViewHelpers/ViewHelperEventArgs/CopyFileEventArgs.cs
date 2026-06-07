using RestfulWebRequest.RestfulTable.RestfulResponseTable.BuiltInType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.ViewHelpers.ViewHelperEventArgs
{
    public class CopyFileEventArgs : EventArgs
    {
        private bool _isSuccessful = false;
        private string _errorMessage = string.Empty;
        private double _percentage = 0;
        private string _filePath = string.Empty;
        private TaskUion _order = null;

        public bool IsSuccessful => _isSuccessful;
        public string ErrorMessage => _errorMessage;
        public double Percentage => _percentage;
        public string FilePath => _filePath;

        public TaskUion Order => _order;

        public CopyFileEventArgs(bool isSuccessful, 
                                 string errorMessage, 
                                 double percentage,
                                 string filePath,
                                 TaskUion order)
        {
            _isSuccessful = isSuccessful;
            _errorMessage = errorMessage;
            _percentage = percentage;
            _filePath = filePath;
            _order = order;
        }
    }
}
