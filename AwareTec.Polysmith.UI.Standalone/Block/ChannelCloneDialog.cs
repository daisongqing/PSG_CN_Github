using AwareTec.Polysmith.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.UI.Block
{
    public partial class ChannelCloneDialog : SkinForm
    {
        public ChannelCloneDialog()
        {
            InitializeComponent();
        }
        private List<string> strIds = new List<string>();
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="tables"></param>
        public void Init(List<ChannelTable> tables)
        {
            m_Channels = new List<ChannelTable>();
            string[] strLabs2 = new string[tables.Count];
            int idx = 0;
            foreach (ChannelTable table in tables)
            {
                strIds.Add(table.ID);
                if (table.ID.Contains("Clone") || table.ID.Contains("Append"))
                    continue;
                strLabs2[idx++] = table.Name;
                m_Channels.Add(table);
            }
            string[] strLabs = new string[idx];
            Array.Copy(strLabs2, 0, strLabs, 0, idx);
            CombBoxDataSource.Items.Clear();
            CombBoxDataSource.Items.AddRange(strLabs);
            CoBtInsert.Items.Clear();
            CoBtInsert.Items.AddRange(strLabs);

        }
        /// <summary>
        /// 当前通道列表
        /// </summary>
        private List<ChannelTable> m_Channels { set; get; }
        private ChannelTable m_CurrentTable = null;
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ChannelName.Text))
            {
                AhDung.MessageTip.ShowError("派生的通道名称不能为空!");
                return;
            }
            else
            {
                if (m_Channels.Find(t => t.Name == ChannelName.Text) != null)
                {
                    AhDung.MessageTip.ShowError("派生的通道名称已存在!");
                    return;
                }
            }
            if (string.IsNullOrWhiteSpace(m_DatasourceID))
            {
                AhDung.MessageTip.ShowError("未选择通道数据源!");
                return;
            }
            Task.Factory.StartNew(() =>
            {
                ChannelTable table = m_CurrentTable.Clone();
                table.Name = ChannelName.Text;
                table.IsClone = true;
                table.Visible = true;
                table.ChannelNo = m_ChannelNo + (RaBtFront.Checked ? 0 : 1);
                this.Invoke(new MethodInvoker(() =>
                {
                    this.Dispose();
                    this.DialogResult = DialogResult.Yes;
                }));
                if (CloneChannelHandle != null)
                {
                    bool vaild = true;
                    string ID = string.Format("Clone_{0}", table.ID);
                    while (vaild)
                    {//检查ID是否重复，重复的话反复新建ID验证，直到唯一
                        if (strIds.Find(t => t == ID) == null)
                        {
                            vaild = false;
                            table.ID = ID;
                            strIds.Add(ID);
                        }
                        else
                        {
                            ID = string.Format("Clone_{0}", ID);
                            System.Threading.Thread.Sleep(5);
                        }
                    }
                    CloneChannelHandle.Invoke(table);
                }
                AhDung.MessageTip.ShowOk("通道派生成功，请保存!");
                DataModel.LogInstance.Default.AddLog(string.Format("通道派生成功 派生的通道名称为 {0}", table.Name), pSystem.LogManagement.LogLevel.WARN);
            });
        }

        /// <summary>
        /// 克隆通道时触发
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public delegate void CloneChannelDelegate(ChannelTable table);
        /// <summary>
        /// 克隆通道时触发
        /// </summary>
        public event CloneChannelDelegate CloneChannelHandle;
        private void CancelButton_Click(object sender, EventArgs e)
        {
            DataModel.LogInstance.Default.AddLog("用户点击 派生-取消按钮");
            this.DialogResult = DialogResult.No;
            this.Close();
        }
        private string m_DatasourceID = "";
        private int m_ChannelNo = -1;
        private void CombBoxDataSource_SelectedValueChanged(object sender, EventArgs e)
        {
            if (m_Channels != null)
            {
                string name = CombBoxDataSource.SelectedItem.ToString();
                m_CurrentTable = m_Channels.Find(t => t.Name == name && !t.IsClone);
                if (m_CurrentTable != null)
                    m_DatasourceID = m_CurrentTable.ID;
                CoBtInsert.SelectedIndex = CombBoxDataSource.SelectedIndex;
            }
        }

        private void CoBtInsert_SelectedValueChanged(object sender, EventArgs e)
        {
            if (m_Channels != null)
            {
                m_ChannelNo = m_Channels[CoBtInsert.SelectedIndex].ChannelNo;
            }
        }

    }
}
