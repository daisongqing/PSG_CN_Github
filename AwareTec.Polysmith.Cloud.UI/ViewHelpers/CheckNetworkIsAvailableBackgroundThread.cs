using AwareTec.Polysmith.Cloud.UI.Forms.Tips;
using AwareTec.Polysmith.Util.NetworkUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.ViewHelpers
{
    public class CheckNetworkIsAvailableBackgroundThread
    {
        private readonly Thread _thread = null;
        private bool _fieldNetworkIsAvailable = true;
        private bool _threadIsRunning = true;
        private bool _networkIsAvailable
        {
            get => _fieldNetworkIsAvailable;
            set
            {
                if (_fieldNetworkIsAvailable == value)
                    return;

                _fieldNetworkIsAvailable = value;

                if (StartupProgram.MainForm != null)
                    StartupProgram.MainForm.NetworkIsAvailable = _fieldNetworkIsAvailable;

                if (!_fieldNetworkIsAvailable)
                {
                    var form = new NetworkIsUnavailableForm();
                    form.ShowDialog();
                }
            }
        }

        public bool NetworkIsAvailable => _networkIsAvailable;

        public CheckNetworkIsAvailableBackgroundThread()
        {
            _thread = new Thread(() =>
            {
                while (_threadIsRunning)
                {
                    _networkIsAvailable = NetworkHelper.CheckForInternetConnection();
                    Thread.Sleep(3000);
                }
            });
            _thread.IsBackground = true;
            _thread.SetApartmentState(ApartmentState.MTA);
            _thread.Start();
        }

        public void End()
        {
            _threadIsRunning = false;
            _thread.Abort();
            _thread.DisableComObjectEagerCleanup();
        }
    }
}
