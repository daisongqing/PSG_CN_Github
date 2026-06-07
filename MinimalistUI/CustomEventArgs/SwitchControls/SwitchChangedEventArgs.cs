using MinimalistUI.EnumModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalistUI.CustomEventArgs.SwitchControls
{
    public class SwitchChangedEventArgs : MinimalistUIEventArgs
    {
        private SwitchResults _switchResults;
        private string _switchText;

        public SwitchResults SwitchResults => _switchResults;
        public string SwitchText => _switchText;

        private SwitchChangedEventArgs() { }
        internal SwitchChangedEventArgs(SwitchResults switchResults,
                                        string switchText) : base()
        {
            _switchResults = switchResults;
            _switchText = switchText;
        }
    }
}
