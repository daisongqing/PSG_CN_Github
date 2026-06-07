using AwareTec.Polysmith.Cloud.UI.DataModels;
using AwareTec.Polysmith.Cloud.UI.DataModels.Global;
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
    public partial class ArousalSelection : UserControl
    {
        #region 私有变量

        private string m_strValue = "1";

        #endregion

        #region 公有变量

        /// <summary>
        /// 脑电通道ID
        /// </summary>
        public string Value
        {
            set
            {
                m_strValue = value;
            }
            get
            {
                return m_strValue;
            }
        }

        #endregion

        #region 初始化 load/dispose/init

        /// <summary>
        /// 初始化
        /// </summary>
        public ArousalSelection()
        {
            InitializeComponent();
            this.Load += ArousalSelection_Load;
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ArousalSelection_Load(object sender, EventArgs e)
        {
            CbBrain.SelectedIndexChanged += CbBrain_SelectedIndexChanged;
            string[] value = m_strValue.Split(';');
            foreach (Control c in this.Controls)
            {
                if (c is pSystem.UI.ReaLTaiizor.Controls.AloneComboBox)
                {
                    string[] val = c.Tag.ToString().Split('\\');
                    pSystem.UI.ReaLTaiizor.Controls.AloneComboBox cb = c as pSystem.UI.ReaLTaiizor.Controls.AloneComboBox;
                    foreach (string s in val)
                    {
                        ChannelTable find = GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelTable.Find(t => t.ID.ToString() == s);

                        //ChannelTable find = Channel.Default.CurrentSaveTable.Find(t => t.ID.ToString() == s);
                        if (find == null)
                            continue;
                        else
                        {
                            cb.Items.Add(find.strName);
                        }
                        if (value.Contains(s))
                        {
                            cb.Text = find.strName;
                        }
                    }
                }
            }

        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 脑电 下拉框 选择项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbBrain_SelectedIndexChanged(object sender, EventArgs e)
        {
            pSystem.UI.ReaLTaiizor.Controls.AloneComboBox bb = sender as pSystem.UI.ReaLTaiizor.Controls.AloneComboBox;
            string[] ss = m_strValue.Split(';');
            ss[0] = GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelTable.Find(t => t.strName == bb.Text).ID.ToString();

            //ss[0] = Channel.Default.CurrentSaveTable.Find(t => t.strName == bb.Text).ID.ToString();
            m_strValue = string.Join(";", ss);
        }

        #endregion

        #region 按钮方法

        #endregion

        #region 公有方法

        #endregion
    }
}
