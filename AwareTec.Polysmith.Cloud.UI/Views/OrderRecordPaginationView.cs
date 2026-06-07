using AwareTec.Polysmith.Cloud.UI.DataModels.ViewModel.FromDbToDb;
using AwareTec.Polysmith.Cloud.UI.DataModels.ViewModel.ToDb;
using AwareTec.Polysmith.Cloud.UI.ViewHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.Cloud.UI.Views
{
    public partial class OrderRecordPaginationView : UserControl
    {
        private OrderRecordPaginationViewModel _orderRecordPaginationViewModel = null;

        public OrderRecordPaginationViewModel OrderRecordPaginationViewModel
        {
            get => _orderRecordPaginationViewModel;
            set
            {
                if (value == null)
                    return;
                _orderRecordPaginationViewModel = value;

                _orderRecordPaginationViewModel.TotalPageCountChanged -= OrderRecordPaginationViewModel_TotalPageCountChanged;
                _orderRecordPaginationViewModel.SelectedPageNoChanged -= OrderRecordPaginationViewModel_SelectedPageNoChanged;
                _orderRecordPaginationViewModel.RecordPerPageChanged -= OrderRecordPaginationViewModel_RecordPerPageChanged;
                _orderRecordPaginationViewModel.TotalRecordCountChanged -= OrderRecordPaginationViewModel_TotalRecordCountChanged;

                
                Init();

                _orderRecordPaginationViewModel.TotalPageCountChanged += OrderRecordPaginationViewModel_TotalPageCountChanged;
                _orderRecordPaginationViewModel.SelectedPageNoChanged += OrderRecordPaginationViewModel_SelectedPageNoChanged;
                _orderRecordPaginationViewModel.RecordPerPageChanged += OrderRecordPaginationViewModel_RecordPerPageChanged;
                _orderRecordPaginationViewModel.TotalRecordCountChanged += OrderRecordPaginationViewModel_TotalRecordCountChanged;
            }
        }

        private void Init()
        {
            TotalRecordCountLabel.Text = _orderRecordPaginationViewModel.TotalRecordCount.ToString();
            RecordCountPerPageLabel.Text = _orderRecordPaginationViewModel.RecordCountPerPage.ToString();
            TotalPageCountLabel.Text = _orderRecordPaginationViewModel.TotalPageCount.ToString();
            SelectPageNoTextBox.Text = _orderRecordPaginationViewModel.SelectedPageNo.ToString();
            PreviousPageBtn.Enabled = _orderRecordPaginationViewModel.SelectedPageNo != 1;
            if (_orderRecordPaginationViewModel.TotalPageCount != 0)
                NextPageBtn.Enabled = _orderRecordPaginationViewModel.SelectedPageNo != _orderRecordPaginationViewModel.TotalPageCount;
            else
                NextPageBtn.Enabled = false;
            LastPageBtn.Enabled = FirstPageBtn.Enabled = _orderRecordPaginationViewModel.TotalPageCount != 0;
        }

        private void OrderRecordPaginationViewModel_TotalRecordCountChanged(object sender, DataModels.ViewModel.CustomEventArgs.TotalRecordCountChangedEventArgs e)
        {
            TotalRecordCountLabel.Text = e.TotalRecordCount.ToString();
        }

        private void OrderRecordPaginationViewModel_RecordPerPageChanged(object sender, DataModels.ViewModel.CustomEventArgs.RecordCountPerPageChangedEventArgs e)
        {
            RecordCountPerPageLabel.Text = e.RecordCountPerPage.ToString();
        }

        private void OrderRecordPaginationViewModel_SelectedPageNoChanged(object sender, DataModels.ViewModel.CustomEventArgs.SelectedPageNoChangedEventArgs e)
        {
            if (!e.SelectedPageNo.ToString().Equals(SelectPageNoTextBox.Text))
                SelectPageNoTextBox.Text = e.SelectedPageNo.ToString();

            PreviousPageBtn.Enabled = e.SelectedPageNo != 1;
            NextPageBtn.Enabled = e.SelectedPageNo != _orderRecordPaginationViewModel.TotalPageCount;
            LastPageBtn.Enabled = FirstPageBtn.Enabled = _orderRecordPaginationViewModel.TotalPageCount != 0;
        }

        private void OrderRecordPaginationViewModel_TotalPageCountChanged(object sender, DataModels.ViewModel.CustomEventArgs.TotalPageCountChangedEventArgs e)
        {
            TotalPageCountLabel.Text = e.TotalPageCount.ToString();
        }

        public OrderRecordPaginationView()
        {
            InitializeComponent();
        }

        private void SelectPageNoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
                e.Handled = true;

            //if (char.IsDigit(e.KeyChar))
            //{
            //    string currentText = SelectPageNoTextBox.Text + e.KeyChar;
            //    int currentInt = Convert.ToInt32(currentText.ToString());
            //    if (currentInt > _orderRecordPaginationViewModel.TotalPageCount)
            //    {
            //        SelectPageNoTextBox.Text = _orderRecordPaginationViewModel.TotalPageCount.ToString();
            //    }
            //}
        }

        private void FirstPageBtn_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            SelectPageNoTextBox.Text = 1.ToString();
        }

        private void PreviousPageBtn_MouseClick(object sender, MouseEventArgs e)
        {
            //if (e.Button != MouseButtons.Left)
            //    return;
            if (_orderRecordPaginationViewModel.SelectedPageNo <= 1)
                return;

            if (_orderRecordPaginationViewModel != null)
                SelectPageNoTextBox.Text = (_orderRecordPaginationViewModel.SelectedPageNo - 1).ToString();
        }

        private void NextPageBtn_MouseClick(object sender, MouseEventArgs e)
        {
            //if (e.Button != MouseButtons.Left)
            //    return;
            if (_orderRecordPaginationViewModel.SelectedPageNo == _orderRecordPaginationViewModel.TotalPageCount || _orderRecordPaginationViewModel.TotalPageCount == 0)
                return;
            if (_orderRecordPaginationViewModel != null)
                SelectPageNoTextBox.Text = (_orderRecordPaginationViewModel.SelectedPageNo + 1).ToString();
        }

        private void LastPageBtn_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            if (_orderRecordPaginationViewModel != null)
                SelectPageNoTextBox.Text = _orderRecordPaginationViewModel.TotalPageCount.ToString();
        }

        private void SelectPageNoTextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SelectPageNoTextBox.Text))
                return;

            int selectPageNo = int.Parse(SelectPageNoTextBox.Text);
            if (_orderRecordPaginationViewModel != null && _orderRecordPaginationViewModel.TotalPageCount > 0)
            {
                _orderRecordPaginationViewModel.SelectedPageNo = selectPageNo > _orderRecordPaginationViewModel.TotalPageCount ? _orderRecordPaginationViewModel.TotalPageCount : selectPageNo;
            }
        }

        private void SelectPageNoTextBox_Leave(object sender, EventArgs e)
        {
            int selectPageNo = int.Parse(SelectPageNoTextBox.Text);
            SelectPageNoTextBox.Text = selectPageNo > _orderRecordPaginationViewModel.TotalPageCount ? _orderRecordPaginationViewModel.TotalPageCount.ToString() : selectPageNo.ToString();
        }

        public void KeyPress(Keys keys)
        {
            switch (keys)
            {
                case Keys.Right:
                    NextPageBtn_MouseClick(null, null);
                    break;
                case Keys.Left:
                    PreviousPageBtn_MouseClick(null, null);
                    break;
            }
        }
    }
}
