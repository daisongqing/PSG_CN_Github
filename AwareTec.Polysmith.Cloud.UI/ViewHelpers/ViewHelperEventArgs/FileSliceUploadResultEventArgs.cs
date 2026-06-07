using RestfulWebRequest.RestfulTable.RestfulResponseTable.BuiltInType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.ViewHelpers.ViewHelperEventArgs
{
    public class FileSliceUploadResultEventArgs
    {
        private bool _isSuccessful;
        private string _errorMessage;
        private int? _currentPieces;
        private int? _totalPieces;
        private string _edfPath;
        private TaskUion _order;

        public bool IsSuccessful => _isSuccessful;
        public string ErrorMessage => _errorMessage;
        public int? CurrentPieces => _currentPieces;
        public int? TotalPieces => _totalPieces;
        public TaskUion Order => _order;
        public string EdfPath => _edfPath;

        public FileSliceUploadResultEventArgs(bool isSuccessful, 
                                              string errorMessage, 
                                              int? currentPieces, 
                                              int? totalPieces,
                                              string edfPath,
                                              TaskUion order)
        {
            _isSuccessful = isSuccessful;
            _errorMessage = errorMessage;
            _currentPieces = currentPieces;
            _totalPieces = totalPieces;
            _edfPath = edfPath;
            _order = order;
        }
    }
}
