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
    public partial class SleepStageSelection : UserControl
    {
        #region 私有变量

        private string m_strValue = "1;23;7";

        #endregion

        #region 公有变量

        /// <summary>
        /// 睡眠分析选择 脑电/肌电/眼电的通道ID
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
        public SleepStageSelection()
        {
            InitializeComponent();
            this.Load += SleepStageSelection_Load;
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SleepStageSelection_Load(object sender, EventArgs e)
        {
            string[] value = m_strValue.Split(';');
            foreach (Control c in this.Controls)
            {
                if (c is pSystem.UI.ReaLTaiizor.Controls.AloneComboBox)
                {
                    string[] Tags = c.Tag.ToString().Split('\\');
                    pSystem.UI.ReaLTaiizor.Controls.AloneComboBox cb = c as pSystem.UI.ReaLTaiizor.Controls.AloneComboBox;
                    foreach (string ChannelID in Tags)
                    {
                        ChannelTable find = GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelTable.Find(t => t.ID.ToString() == ChannelID);

                        //ChannelTable find = Channel.Default.CurrentSaveTable.Find(t => t.ID.ToString() == ChannelID);
                        if (find == null)
                        {
                            continue;
                        }
                        else
                        {
                            cb.Items.Add(find.strName);
                        }
                        if (value.Contains(ChannelID))
                        {
                            cb.Text = find.strName;
                        }
                    }
                }
            }
            MuscleComboBox.SelectedIndexChanged += MuscleComboBox_SelectedIndexChanged;
            BrainCombBox.SelectedIndexChanged += BrainCombBox_SelectedIndexChanged;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 脑电通道选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrainCombBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            pSystem.UI.ReaLTaiizor.Controls.AloneComboBox bb = sender as pSystem.UI.ReaLTaiizor.Controls.AloneComboBox;
            string[] ss = m_strValue.Split(';');
            ss[0] = GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelTable.Find(t => t.strName == bb.Text).ID.ToString();

            //ss[0] = Channel.Default.CurrentSaveTable.Find(t => t.strName == bb.Text).ID.ToString();
            m_strValue = string.Join(";", ss);
        }

        /// <summary>
        /// 肌电通道选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MuscleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            pSystem.UI.ReaLTaiizor.Controls.AloneComboBox bb = sender as pSystem.UI.ReaLTaiizor.Controls.AloneComboBox;
            string[] ss = m_strValue.Split(';');
            ss[1] = GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelTable.Find(t => t.strName == bb.Text).ID.ToString();

            //ss[1] = Channel.Default.CurrentSaveTable.Find(t => t.strName == bb.Text).ID.ToString();
            m_strValue = string.Join(";", ss);
        }

        /// <summary>
        /// 眼电通道选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EyeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            pSystem.UI.ReaLTaiizor.Controls.AloneComboBox bb = sender as pSystem.UI.ReaLTaiizor.Controls.AloneComboBox;
            string[] ss = m_strValue.Split(';');
            ss[2] = GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelTable.Find(t => t.strName == bb.Text).ID.ToString();

            //ss[2] = Channel.Default.CurrentSaveTable.Find(t => t.strName == bb.Text).ID.ToString();
            m_strValue = string.Join(";", ss);
        }

        #endregion

        #region 按钮方法

        #endregion

        #region 公有方法

        #endregion

    }
}
