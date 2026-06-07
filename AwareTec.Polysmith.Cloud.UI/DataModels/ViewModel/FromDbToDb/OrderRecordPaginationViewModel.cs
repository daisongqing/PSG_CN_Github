using AwareTec.Polysmith.Cloud.UI.DataModels.ViewModel.Base;
using AwareTec.Polysmith.Cloud.UI.DataModels.ViewModel.CustomEventArgs;
using RestfulWebRequest.RestfulTable;
using RestfulWebRequest.RestfulTable.RestfulRequestTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.ViewModel.FromDbToDb
{
    public class OrderRecordPaginationViewModel : IToDb, IFromDb
    {
        private int _recordCountPerPage;
        private int _totalPageCount;
        private int _selectedPageNo = 1;
        private int _totalRecordCount;
        private int _skipCount;

        #region Events
        private event EventHandler<SelectedPageNoChangedEventArgs> _selectedPageNoChanged;
        private event EventHandler<TotalPageCountChangedEventArgs> _totalPageCountChanged;
        private event EventHandler<TotalRecordCountChangedEventArgs> _totalRecordCountChanged;
        private event EventHandler<RecordCountPerPageChangedEventArgs> _recordPerPageChanged;


        public event EventHandler<SelectedPageNoChangedEventArgs> SelectedPageNoChanged
        {
            add
            {
                if (value == null)
                    return;

                if (_selectedPageNoChanged == null ||
                   _selectedPageNoChanged.GetInvocationList().Length == 0)
                {
                    _selectedPageNoChanged = value;
                }
                else
                {
                    foreach (var item in _selectedPageNoChanged.GetInvocationList())
                    {
                        if (value == item)
                            return;
                    }
                    _selectedPageNoChanged += value;
                }
            }
            remove
            {
                if (value == null)
                    return;

                if (_selectedPageNoChanged == null ||
                   _selectedPageNoChanged.GetInvocationList().Length == 0)
                    return;

                foreach (var item in _selectedPageNoChanged.GetInvocationList())
                {
                    if (value == item)
                        _selectedPageNoChanged -= value;
                }
            }
        }

        public event EventHandler<TotalPageCountChangedEventArgs> TotalPageCountChanged
        {
            add
            {
                if (value == null)
                    return;

                if (_totalPageCountChanged == null ||
                   _totalPageCountChanged.GetInvocationList().Length == 0)
                {
                    _totalPageCountChanged = value;
                }
                else
                {
                    foreach (var item in _totalPageCountChanged.GetInvocationList())
                    {
                        if (value == item)
                            return;
                    }
                    _totalPageCountChanged += value;
                }
            }
            remove
            {
                if (value == null)
                    return;

                if (_totalPageCountChanged == null ||
                   _totalPageCountChanged.GetInvocationList().Length == 0)
                    return;

                foreach (var item in _totalPageCountChanged.GetInvocationList())
                {
                    if (value == item)
                        _totalPageCountChanged -= value;
                }
            }
        }

        public event EventHandler<TotalRecordCountChangedEventArgs> TotalRecordCountChanged
        {
            add
            {
                if (value == null)
                    return;

                if (_totalRecordCountChanged == null ||
                   _totalRecordCountChanged.GetInvocationList().Length == 0)
                {
                    _totalRecordCountChanged = value;
                }
                else
                {
                    foreach (var item in _totalRecordCountChanged.GetInvocationList())
                    {
                        if (value == item)
                            return;
                    }
                    _totalRecordCountChanged += value;
                }
            }
            remove
            {
                if (value == null)
                    return;

                if (_totalRecordCountChanged == null ||
                   _totalRecordCountChanged.GetInvocationList().Length == 0)
                    return;

                foreach (var item in _totalRecordCountChanged.GetInvocationList())
                {
                    if (value == item)
                        _totalRecordCountChanged -= value;
                }
            }
        }

        public event EventHandler<RecordCountPerPageChangedEventArgs> RecordPerPageChanged
        {
            add
            {
                if (value == null)
                    return;

                if (_recordPerPageChanged == null ||
                   _recordPerPageChanged.GetInvocationList().Length == 0)
                {
                    _recordPerPageChanged = value;
                }
                else
                {
                    foreach (var item in _recordPerPageChanged.GetInvocationList())
                    {
                        if (value == item)
                            return;
                    }
                    _recordPerPageChanged += value;
                }
            }
            remove
            {
                if (value == null)
                    return;

                if (_recordPerPageChanged == null ||
                   _recordPerPageChanged.GetInvocationList().Length == 0)
                    return;

                foreach (var item in _recordPerPageChanged.GetInvocationList())
                {
                    if (value == item)
                        _recordPerPageChanged -= value;
                }
            }
        }
        #endregion

        public int RecordCountPerPage
        {
            get => _recordCountPerPage;
            set
            {
                if (value == _recordCountPerPage)
                    return;

                _recordCountPerPage = value;
                if (_recordCountPerPage != 0)
                {
                    TotalPageCount = _totalRecordCount % _recordCountPerPage == 0 ?
                  _totalRecordCount / _recordCountPerPage :
                  _totalRecordCount / _recordCountPerPage + 1;
                    _skipCount =  _recordCountPerPage * (_selectedPageNo - 1);
                }
                if (_recordPerPageChanged != null)
                    _recordPerPageChanged(this, new RecordCountPerPageChangedEventArgs(_recordCountPerPage));
            }
            
        }

        public int TotalPageCount
        {
            get => _totalPageCount;
            private set
            {
                if (value == _totalPageCount)
                    return;

                _totalPageCount = value;
                if (_totalPageCountChanged != null)
                    _totalPageCountChanged(this, new TotalPageCountChangedEventArgs(_totalPageCount));
            }
        }

        public int SelectedPageNo
        {
            get => _selectedPageNo;
            set
            {
                if (_selectedPageNo == value)
                    return;

                _selectedPageNo = value;
                _skipCount =  _recordCountPerPage * (_selectedPageNo - 1);
                if (_selectedPageNoChanged != null)
                    _selectedPageNoChanged(this, new SelectedPageNoChangedEventArgs(_selectedPageNo));
            }
        }

        public int TotalRecordCount
        {
            get => _totalRecordCount;
            set
            {
                if (_totalRecordCount == value)
                    return;

                _totalRecordCount = value;
                TotalPageCount = _totalRecordCount % _recordCountPerPage == 0 ?
                                 _totalRecordCount / _recordCountPerPage :
                                 _totalRecordCount / _recordCountPerPage + 1;
                if (_totalRecordCountChanged != null)
                    _totalRecordCountChanged(this, new TotalRecordCountChangedEventArgs(_totalRecordCount));
            }
            
        }

        public int SkipCount => _skipCount;

        public void FromDb(IRestfulTable restfulTable, object otherInitParams)
        {
            throw new NotImplementedException();
        }

        public IRestfulTable ToDb()
        {
            return new GetMyOrderRequestModel()
            {
                MaxResultCount = _recordCountPerPage,
                SkipCount = _skipCount
            };
        }
    }
}
