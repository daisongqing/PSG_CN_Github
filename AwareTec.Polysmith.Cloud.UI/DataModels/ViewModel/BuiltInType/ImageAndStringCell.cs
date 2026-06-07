using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.DataModels.ViewModel.BuiltInType
{
    public class ImageAndStringCell
    {
        private string _displayName = string.Empty;
        private Image _image = null;

        public string DisplayName
        {
            get => _displayName;
            set => _displayName = value;
        }
        public Image Image
        {
            get => _image;
            set => _image = value;
        }
    }
}
