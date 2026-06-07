using AwareTec.Polysmith.Cloud.UI.ViewHelpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AwareTec.Polysmith.Cloud.UI.DataModels
{
    public  class ChannelConfig
    {
        private List<ChannelTable> m_channelTables = new List<ChannelTable>();
        private string _fieldCurrentChannelPath = string.Empty;
        private DataTable _dataTable = null;
        private string _currentChannelPath
        {
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    return;
                _fieldCurrentChannelPath = value;
            }
            get => _fieldCurrentChannelPath;
        }

        public delegate void ChannelChangedDelegate(bool TemporaryInvalidate = false);
        private event ChannelChangedDelegate m_ChannelChangedHandle;
        /// <summary>
        /// 通道配置改变时发生
        /// </summary>
        public event ChannelChangedDelegate ChannelChangedHandle
        {
            add
            {
                if (m_ChannelChangedHandle != null)
                {
                    Delegate[] find = m_ChannelChangedHandle.GetInvocationList();
                    foreach (Delegate gate in find)
                    {
                        if (gate.Target == value.Target)
                        {
                            return;
                        }
                    }
                    m_ChannelChangedHandle += value;
                }
                else
                {
                    m_ChannelChangedHandle += value;
                }
            }
            remove
            {
                if (m_ChannelChangedHandle != null)
                {
                    Delegate[] find = m_ChannelChangedHandle.GetInvocationList();
                    foreach (Delegate gate in find)
                    {
                        if (gate.Target == value.Target)
                        {
                            m_ChannelChangedHandle -= value;
                        }
                        break;
                    }
                }
            }
        }

        public ChannelConfig(string CurrentChannelPath)
        {
            _currentChannelPath = CurrentChannelPath;
            CurrentDataTable = ChannelManageCloud.Default.ReadChannelConfig(CurrentChannelPath);
        }
        
        /// <summary>
        /// 获取当时使用通道配置的全部属性
        /// </summary>
        public List<ChannelTable> CurrentChannelTable
        {
            get => m_channelTables;
        }

        /// <summary>
        /// 获取当时使用通道配置的用户属性
        /// </summary>
        public DataTable CurrentDataTable
        {
            get => _dataTable;
            set
            {
                _dataTable = value;
                m_channelTables = DataTableAddToChannelTable(_dataTable);
            }
        }

        /// <summary>
        /// 获取当前配置文件路径
        /// </summary>
        public string CurrentChannelPath
        {
            get => _currentChannelPath;
        }

        /// <summary>
        /// 将用户cfg文件 和系统预定义的xml 文件 合并
        /// </summary>
        /// <param name="dataTable">用户cfg文件 </param>
        /// <param name="channelTables"> 系统预定义的xml </param>
        /// <returns></returns>
        private List<ChannelTable> DataTableAddToChannelTable(DataTable dataTable)
        {
            List<ChannelTable> finaltables = new List<ChannelTable>();
            try
            {
                for(int i = 0; i < dataTable.Rows.Count; i++)
                {
                    finaltables.Add(ChannelTable.ConvertToChannel(dataTable.Rows[i]));
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
            return finaltables;
        }
    }
}
