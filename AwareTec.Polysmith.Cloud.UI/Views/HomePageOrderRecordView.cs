using AwareTec.Polysmith.Cloud.UI.DataModels;
using AwareTec.Polysmith.Cloud.UI.DataModels.EnumModels;
using AwareTec.Polysmith.Cloud.UI.DataModels.ViewModel.FromDb;
using AwareTec.Polysmith.Cloud.UI.Forms.Tips;
using AwareTec.Polysmith.Cloud.UI.Views.ViewsEventArgs;
using Newtonsoft.Json;
using RestfulWebRequest.ApiCall;
using RestfulWebRequest.EnumModels.EnumModels4Table;
using RestfulWebRequest.RestfulModel;
using RestfulWebRequest.RestfulTable.RestfulRequestTable;
using RestfulWebRequest.RestfulTable.RestfulResponseTable;
using RestfulWebRequest.RestfulTable.RestfulResponseTable.BuiltInType;
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
    public partial class HomePageOrderRecordView : UserControl
    {
        #region 事件相关
        private event EventHandler<SwitchPageEventArgs> _startMonitoringBeClicked;
        private event EventHandler<SwitchPageEventArgs> _enterPlayBeClicked;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("自定义事件选项")]
        [Description("开始监测的快捷菜单被点击")]
        public event EventHandler<SwitchPageEventArgs> StartMonitoringBeClicked
        {
            add
            {
                if (value == null)
                    return;

                if (_startMonitoringBeClicked == null ||
                   _startMonitoringBeClicked.GetInvocationList().Length == 0)
                {
                    _startMonitoringBeClicked = value;
                }
                else
                {
                    foreach (var item in _startMonitoringBeClicked.GetInvocationList())
                    {
                        if (value == item)
                            return;
                    }
                    _startMonitoringBeClicked += value;
                }
            }
            remove
            {
                if (value == null)
                    return;

                if (_startMonitoringBeClicked == null ||
                   _startMonitoringBeClicked.GetInvocationList().Length == 0)
                    return;

                foreach (var item in _startMonitoringBeClicked.GetInvocationList())
                {
                    if (value == item)
                        _startMonitoringBeClicked -= value;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("自定义事件选项")]
        [Description("进入回放的快捷菜单被点击")]
        public event EventHandler<SwitchPageEventArgs> EnterPlayBeClicked
        {
            add
            {
                if (value == null)
                    return;

                if (_enterPlayBeClicked == null ||
                   _enterPlayBeClicked.GetInvocationList().Length == 0)
                {
                    _enterPlayBeClicked = value;
                }
                else
                {
                    foreach (var item in _enterPlayBeClicked.GetInvocationList())
                    {
                        if (value == item)
                            return;
                    }
                    _enterPlayBeClicked += value;
                }
            }
            remove
            {
                if (value == null)
                    return;

                if (_enterPlayBeClicked == null ||
                   _enterPlayBeClicked.GetInvocationList().Length == 0)
                    return;

                foreach (var item in _enterPlayBeClicked.GetInvocationList())
                {
                    if (value == item)
                        _enterPlayBeClicked -= value;
                }
            }
        }
        #endregion

        #region 构造函数
        public HomePageOrderRecordView()
        {
            InitializeComponent(); 
        }
        #endregion

        /// <summary>
        /// 页面过滤器触发绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderRecordSearchView_QueryOrderByFilter(object sender, QueryOrderByFilterEventArgs e)
        {
            var requestModel = e.OrderRecordSearchViewModel.ToDb();
            OrderRecordView.RequestModel = requestModel as GetMyOrderRequestModel;
        }

        #region 事件向上传递
        private void OrderRecordView_EnterPlayBeClicked(object sender, ViewsEventArgs.SwitchPageEventArgs e)
        {
            if(_enterPlayBeClicked != null)
                _enterPlayBeClicked(sender, e);
        }

        private void OrderRecordView_StartMonitoringBeClicked(object sender, ViewsEventArgs.SwitchPageEventArgs e)
        {
            if(_startMonitoringBeClicked != null)
                _startMonitoringBeClicked(sender, e);
        }
        #endregion

        #region 按键触发
        /// <summary>
        /// 按键触发实现
        /// </summary>
        /// <param name="keyData"></param>
        public void mainKeyDown(Keys keyData)
        {
            
        }
        /// <summary>
        /// 刷新数据
        /// </summary>
        public void RefeshViewData()
        {
            OrderRecordView.RefreshViewData();
        }
        #endregion
    }
}
