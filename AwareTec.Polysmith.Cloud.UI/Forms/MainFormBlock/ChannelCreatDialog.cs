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
    public partial class ChannelCreatDialog : CloudSkinForm
    {
        #region 私有变量

        private List<string> strIds = new List<string>();
        /// <summary>
        /// 当前通道列表
        /// </summary>
        private List<ChannelTable> m_Channels { set; get; }
        private ChannelTable m_CurrentTable = null;
        private string m_DatasourceID = "";
        private string bak_DatasourceID = "";
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
        public delegate void AppendChannelDelegate(ChannelTable table);
        /// <summary>
        /// 克隆通道时触发
        /// </summary>
        public event AppendChannelDelegate AppendChannelHandle;

        #endregion

        #region 初始化 load/dispose/init

        /// <summary>
        /// 初始化
        /// </summary>
        public ChannelCreatDialog()
        {
            InitializeComponent();
            this.Load += ChannelCreatDialog_Load;
        }
        
        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChannelCreatDialog_Load(object sender, EventArgs e)
        {
            this.SubmitButton.Click += SubmitButton_Click;
            this.CancelButton.Click += CancelButton_Click;
            this.PositiveDataSourceComBox.SelectedIndexChanged += PositiveDataSourceComBox_SelectedIndexChanged;
            this.NegativeDataSourceComBox.SelectedIndexChanged += NegativeDataSourceComBox_SelectedIndexChanged;
            this.InsertStationComBox.SelectedIndexChanged += InsertStationComBox_SelectedIndexChanged;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 正向数据源 下拉框选择项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PositiveDataSourceComBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_Channels != null)
            {
                string name = PositiveDataSourceComBox.SelectedItem.ToString();
                m_CurrentTable = m_Channels.Find(t => t.strName == name && !t.IsClone);
                if (m_CurrentTable != null)
                    m_DatasourceID = m_CurrentTable.ID.ToString();
                InsertStationComBox.SelectedIndex = PositiveDataSourceComBox.SelectedIndex;
            }
        }

        /// <summary>
        /// 负向数据源 下拉框选择项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NegativeDataSourceComBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_Channels != null)
            {
                string name = NegativeDataSourceComBox.SelectedItem.ToString();
                ChannelTable find = m_Channels.Find(t => t.strName == name && !t.IsClone);
                if (find != null)
                    bak_DatasourceID = find.ID.ToString();
            }
        }

        /// <summary>
        /// 插入位置 下拉框选择项改变
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
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("用户点击 合成-取消按钮");
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
            if (string.IsNullOrWhiteSpace(m_DatasourceID) || string.IsNullOrWhiteSpace(bak_DatasourceID))
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
                table.Index = m_ChannelNo + (InsertFrontRationBut.Checked ? 0 : 1);
                this.Invoke(new MethodInvoker(() =>
                {
                    this.Dispose();
                    this.DialogResult = DialogResult.Yes;
                }));
                if (AppendChannelHandle != null)
                {
                    bool vaild = true;
                    string ID = string.Format("Append_{0}:{1}", table.ID, bak_DatasourceID);
                    while (vaild)
                    {//检查ID是否重复，重复的话反复新建ID验证，直到唯一
                        if (strIds.Find(t => t == ID) == null)
                        {
                            vaild = false;
                            table.ID = Convert.ToInt32(ID);
                            strIds.Add(ID);
                        }
                        else
                        {
                            ID = string.Format("Append_{0}", ID);
                            System.Threading.Thread.Sleep(5);
                        }
                    }
                    AppendChannelHandle.Invoke(table);
                }
                AhDung.MessageTip.ShowOk("附加通道创建成功，请保存!");
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("合成通道创建成功 合成通道名称为 {0}", table.strName), pSystem.LogManagement.LogLevel.WARN);
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
                strIds.Add(table.ID.ToString());
                if (table.ID.ToString().Contains("Clone") || table.ID.ToString().Contains("Append"))
                    continue;
                strLabs2[idx++] = table.strName;
                m_Channels.Add(table);
            }
            string[] strLabs = new string[idx];
            Array.Copy(strLabs2, 0, strLabs, 0, idx);
            PositiveDataSourceComBox.Items.Clear();
            PositiveDataSourceComBox.Items.AddRange(strLabs);
            InsertStationComBox.Items.Clear();
            InsertStationComBox.Items.AddRange(strLabs);
            NegativeDataSourceComBox.Items.Clear();
            NegativeDataSourceComBox.Items.AddRange(strLabs);
        }

        #endregion

    }
}
