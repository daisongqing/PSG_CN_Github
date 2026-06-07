using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.Base
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class ViewModelAttribute : Attribute
    {
        private bool _alwaysVisible = true;
        private string _dataGridViewColumnName = string.Empty;

        public ViewModelAttribute(string dataGridViewColumnName)
        {
            _dataGridViewColumnName = dataGridViewColumnName;
        }

        public string DataGridViewColumnName => _dataGridViewColumnName;

        public bool AlwaysVisible
        {
            get => _alwaysVisible;
            set => _alwaysVisible = value;
        }
    }
}
