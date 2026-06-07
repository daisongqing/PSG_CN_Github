using AwareTec.Polysmith.Cloud.UI.DataModels.ViewModel.ToDb;
using AwareTec.Polysmith.Cloud.UI.Views.ViewsEventArgs;
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
    public partial class OrderRecordSearchView : UserControl
    {
        private const int CONTROL_SPACE = 30;

        private event EventHandler<QueryOrderByFilterEventArgs> _queryOrderByFilter;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("自定义事件选项")]
        [Description("通过过滤器查询订单事件被触发")]
        public event EventHandler<QueryOrderByFilterEventArgs> QueryOrderByFilter
        {
            add
            {
                if (value == null)
                    return;

                if (_queryOrderByFilter == null ||
                   _queryOrderByFilter.GetInvocationList().Length == 0)
                {
                    _queryOrderByFilter = value;
                }
                else
                {
                    foreach (var item in _queryOrderByFilter.GetInvocationList())
                    {
                        if (value == item)
                            return;
                    }
                    _queryOrderByFilter += value;
                }
            }
            remove
            {
                if (value == null)
                    return;

                if (_queryOrderByFilter == null ||
                   _queryOrderByFilter.GetInvocationList().Length == 0)
                    return;

                foreach (var item in _queryOrderByFilter.GetInvocationList())
                {
                    if (value == item)
                        _queryOrderByFilter -= value;
                }
            }
        }

        public OrderRecordSearchView()
        {
            InitializeComponent();
            SearchByTimeCheckBox.Checked = false;
            ProgressStatusComboBox.Items.Clear();
            ProgressStatusComboBox.Items.Add(" ");
            if (DataModels.Global.GlobalSingleton.Instance.User.UserTokenInfo.UserType == RestfulWebRequest.EnumModels.EnumModels4Table.UserType.JudgementCenterUser)
            {
                ProgressStatusComboBox.Items.Add("待分析");
                ProgressStatusComboBox.Items.Add("待审核");
            }
            else
            {
                ProgressStatusComboBox.Items.Add("待监测");
                ProgressStatusComboBox.Items.Add("待上传");
            }
            ProgressStatusComboBox.Items.Add("已完成");
        }

        private void SearchButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            var args = new QueryOrderByFilterEventArgs(new OrderRecordSearchViewModel()
                                                        {
                                                            SearchDateTime = SearchByTimeCheckBox.Checked,
                                                            OrderNumber = OrderNumberTextBox.Text,
                                                            PatientName = PatientNameTextBox.Text,
                                                            ProgressStatus = ProgressStatusComboBox.Text,
                                                            StartDateTime = SearchByTimeCheckBox.Checked ? StartDateTimePicker.Value : default(DateTime),
                                                            EndDateTime = SearchByTimeCheckBox.Checked ? EndDateTimePicker.Value : default(DateTime)
                                                        });
            if (_queryOrderByFilter != null)
                _queryOrderByFilter(this, args);

        }

        private void SearchByTimeCheckBox_CheckedChanged(object sender)
        {
            VisiblePanel.Visible = SearchByTimeCheckBox.Checked;
        }

        private void panel1_VisibleChanged(object sender, EventArgs e)
        {
            if (VisiblePanel.Visible)
                SearchButton.Location = new Point(VisiblePanel.Location.X + VisiblePanel.Width + CONTROL_SPACE,
                                                  SearchByTimeCheckBox.Location.Y);
            else
                SearchButton.Location = new Point(SearchByTimeCheckBox.Location.X + SearchByTimeCheckBox.Width + CONTROL_SPACE,
                                                  SearchByTimeCheckBox.Location.Y);

            ResetButton.Location = new Point(SearchButton.Location.X + SearchButton.Width + CONTROL_SPACE,
                                             SearchButton.Location.Y);
        }

        private void Return_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Return)
                return;

            SearchButton_MouseClick(SearchButton, 
                                    new MouseEventArgs
                                    (MouseButtons.Left,
                                     0,0,0,0));
        }

        private void ResetButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            OrderNumberTextBox.Text = string.Empty;
            PatientNameTextBox.Text = string.Empty;
            ProgressStatusComboBox.SelectedIndex = 0;
            SearchByTimeCheckBox.Checked = false;
        }
    }
}
