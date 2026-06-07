using AwareTec.Polysmith.Cloud.UI.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.Cloud.UI.Forms.MainFormBlock
{
    public partial class ChannelCloneDialog : CloudSkinForm
    {
        #region 私有变量

        private List<string> strIds = new List<string>();
        /// <summary>
        /// 当前通道列表
        /// </summary>
        private List<ChannelTable> m_Channels { set; get; }
        private ChannelTable m_CurrentTable = null;
        private string m_DatasourceID = "";
        private int m_ChannelNo = -1;

        #endregion

        #region 公有变量

        #endregion

        #region 事件委托

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

        #endregion



        #region 初始化 load/dispose/init

        /// <summary>
        /// 初始化
        /// </summary>
        public ChannelCloneDialog()
        {
            InitializeComponent();
            this.Load += ChannelCloneDialog_Load;
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChannelCloneDialog_Load(object sender, EventArgs e)
        {
            this.SubmitButton.Click += SubmitButton_Click;
            this.CancelButton.Click += CancelButton_Click;
            this.ChannelDataSourceCombBox.SelectedIndexChanged += ChannelDataSourceCombBox_SelectedIndexChanged;
            this.InsertStationComBox.SelectedIndexChanged += InsertStationComBox_SelectedIndexChanged;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 通道数据源 下拉框选择项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChannelDataSourceCombBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_Channels != null)
            {
                string name = ChannelDataSourceCombBox.SelectedItem.ToString();
                m_CurrentTable = m_Channels.Find(t => t.strName == name && !t.IsClone);
                if (m_CurrentTable != null)
                    m_DatasourceID = m_CurrentTable.ID.ToString();
                InsertStationComBox.SelectedIndex = ChannelDataSourceCombBox.SelectedIndex;
            }
        }

        /// <summary>
        /// 插入位置 下拉框选择项 改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InsertStationComBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_Channels != null)
            {
                m_ChannelNo = m_Channels[InsertStationComBox.SelectedIndex].Index;
            }
        }

        #endregion

        #region 按钮方法

        /// <summary>
        /// 取消按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("用户点击 派生-取消按钮");
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        /// <summary>
        /// 提交按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ChannelNameTxtBox.Text))
            {
                AhDung.MessageTip.ShowError("派生的通道名称不能为空!");
                return;
            }
            else
            {
                if (m_Channels.Find(t => t.strName == ChannelNameTxtBox.Text) != null)
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
                table.strName = ChannelNameTxtBox.Text;
                table.IsClone = true;
                table.State = true;
                table.Index = m_ChannelNo + (InsertFrontRadioBut.Checked ? 0 : 1);
                this.Invoke(new MethodInvoker(() =>
                {
                    this.Dispose();
                    this.DialogResult = DialogResult.Yes;
                }));
                if (CloneChannelHandle != null)
                {
                    bool vaild = true;
                    string ID = string.Format("Clone_({0})", table.ID);
                    while (vaild)
                    {//检查ID是否重复，重复的话反复新建ID验证，直到唯一
                        if (strIds.Find(t => t == ID) == null)
                        {
                            vaild = false;
                            table.ChannelID = ID;
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
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("通道派生成功 派生的通道名称为 {0}", table.strName), pSystem.LogManagement.LogLevel.WARN);
            });
        }

        #endregion

        #region 公有方法

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
                strIds.Add(table.ChannelID.ToString());
                if (table.ChannelID.Contains("Clone") || table.ChannelID.Contains("Append"))
                    continue;
                strLabs2[idx++] = table.strName;
                m_Channels.Add(table);
            }
            string[] strLabs = new string[idx];
            Array.Copy(strLabs2, 0, strLabs, 0, idx);
            ChannelDataSourceCombBox.Items.Clear();
            ChannelDataSourceCombBox.Items.AddRange(strLabs);
            InsertStationComBox.Items.Clear();
            InsertStationComBox.Items.AddRange(strLabs);
        }

        #endregion

    }
}
