using RestfulWebRequest.RestfulTable.RestfulResponseTable.BuiltInType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.ViewHelpers.ViewHelperEventArgs
{
    public class ZipFileEventArgs : EventArgs
    {
        private bool _isSuccessful = false;
        private string _errorMessage = string.Empty;
        private double _percentage = 0;
        private string _zipPath = string.Empty;
        private string _edfPath = string.Empty;
        private TaskUion _order = null;

        public bool IsSuccessful => _isSuccessful;
        public string ErrorMessage => _errorMessage;
        public double Percentage => _percentage;
        public string ZipPath => _zipPath;
        public string EdfPath => _edfPath;
        public TaskUion Order => _order;

        public ZipFileEventArgs(bool isSuccessful, 
                                string errorMessage, 
                                double percentage,
                                string zipPath,
                                TaskUion order,
                                string edfPath)
        {
            _isSuccessful = isSuccessful;
            _errorMessage = errorMessage;
            _percentage = percentage;
            _zipPath = zipPath;
            _order = order;
            _edfPath = edfPath;
        }
    }
}
