using AwareTec.Polysmith.Cloud.UI.DataModels;
using AwareTec.Polysmith.Cloud.UI.DataModels.Global;
using AwareTec.Polysmith.Cloud.UI.Forms.Tips;
using AwareTec.Polysmith.Cloud.UI.ViewHelpers;
using AwareTec.Polysmith.Util;
using pSystem.UI.ReaLTaiizor.CustomCtrl;
using pSystem.UI.ReaLTaiizor.CustomCtrl.Picker;
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

namespace AwareTec.Polysmith.Cloud.UI.Forms.MainFormBlock
{
    public partial class Montage : CloudSkinForm
    {
        #region 私有变量

        private DataTable m_DefineData = null;
        private DataTable Bak_DefineData = null;
        private bool m_cellinit = false;
        private int m_cellinitCnt = 0;
        private string m_SelectPath = "";
        private int m_SensitivityColumnIndex = 10;

        #endregion

        #region 公有变量

        public string m_strAlert = "信息提示";
        public string m_strResetAlert = "是否需要恢复到初始设置?";
        public string m_strSaveAlert = "是否需要保存当前配置方案?";
        public string m_strSelectNone = "全不选";
        public string m_strSave = "保存";
        public string m_strInvertSelect = "反选";
        public string m_strSelectAll = "全选";
        public string m_strCaptureFilter = "Capture Filter";
        public string m_strUndefined = "未定义";
        public string m_strReset = "重置";

        #endregion

        #region 事件委托
        /// <summary>
        /// 滤波器动态加载委托
        /// </summary>
        /// <param name="CaptureResult"></param>
        /// <returns></returns>
        public delegate string[] FilterDropDownDelegate(int ID, string typ);
        /// <summary>
        /// 滤波器动态加载事件
        /// </summary>
        public event FilterDropDownDelegate FilterDropDownHandle;

        /// <summary>
        /// 滤波器动态加载委托
        /// </summary>
        /// <param name="CaptureResult"></param>
        /// <returns></returns>
        public delegate string[] FilterColumnLoadDelegate(string typ);
        /// <summary>
        /// 滤波器动态加载事件
        /// </summary>
        public event FilterColumnLoadDelegate FilterColumnLoadHandle;

        #endregion


        #region 初始化 load/dispose/init

        /// <summary>
        /// 初始化
        /// </summary>
        public Montage()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer
| ControlStyles.ResizeRedraw
| ControlStyles.Selectable
| ControlStyles.AllPaintingInWmPaint
| ControlStyles.UserPaint
| ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            InitializeComponent();
            this.Load += Montage_Load;
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Montage_Load(object sender, EventArgs e)
        {
            //每次更改这个页面的designer时，需要把SelectChannelComBox_SelectedIndexChanged 事件重新绑定一次
            this.ResetButton.Click += ResetButton_Click;
            this.DeriveButton.Click += DeriveButton_Click;
            this.SaveButton.Click += SaveButton_Click;
            this.KeyDown += Montage_KeyDown;

            abSelect.CellMouseDown += abSelect_CellMouseDown;
            abSelect.CellClick += abSelect_CellClick;
            abSelect.DataError += abSelect_DataError;
            abSelect.CellFormatting += abSelect_CellFormatting;
            abSelect.EditingControlShowing += abSelect_EditingControlShowing;
            abSelect.CellParsing += abSelect_CellParsing;

            ResetButton.Text = m_strReset;
            SaveButton.Text = m_strSave;
            for (int i = 1; i < abSelect.Columns.Count; i++)
            {
                abSelect.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                abSelect.Columns[i].ReadOnly = false;
            }
            m_SensitivityColumnIndex = abSelect.Columns["Sensitivity"].Index;
            this.SelectChannelComBox.MouseWheel += SelectChannelComBox_MouseWheel;
            this.AllCheckToolStripMenuItem.Click += AllCheckToolStripMenuItem_Click;
            this.NoCheckToolStripMenuItem.Click += AllCheckToolStripMenuItem_Click;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 防止拖拽的时候出现卡顿
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = 0x02000000;////用双缓冲绘制窗口的所有子控件
                return cp;
            }
        }

        /// <summary>
        /// 键盘快捷键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Montage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (abSelect.CurrentRow != null)
                {
                    bool isclonechannel = (bool)abSelect.CurrentRow.Cells["IsClone"].Value;
                    if(isclonechannel)
                    {
                        if (MessageForm.Show(string.Format("是否确定要删除\"{0}\"通道?", abSelect.CurrentRow.Cells["strName"].Value), "删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            m_DefineData.Rows.RemoveAt(int.Parse(abSelect.CurrentRow.Cells["Index"].Value.ToString()));
                            for (int i = 0; i < m_DefineData.Rows.Count; i++)
                            {
                                m_DefineData.Rows[i]["Index"] = i;
                            }
                            m_DefineData.AcceptChanges();
                            m_cellinit = false;
                            abSelect.DataSource = m_DefineData;
                            abSelect.Invalidate();
                            AhDung.MessageTip.ShowOk("删除成功!");
                        }
                    }
                    else
                    {
                        string ss = m_DefineData.Rows[abSelect.CurrentCell.RowIndex][abSelect.CurrentCell.ColumnIndex].ToString();
                        AhDung.MessageTip.ShowWarning("当前选中通道不属于派生或者合成通道，不能被删除!");
                        abSelect.CurrentCell.Value = ss;
                        abSelect.InvalidateCell(abSelect.CurrentCell);
                    }
                }
                else
                {
                    AhDung.MessageTip.ShowWarning("未选中任何通道!");
                }
            }
        }

        /// <summary>
        /// 判断单元格输入值的格式（通道名称 可以输入任何值）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (abSelect.CurrentCell != null)
            {
                if (abSelect.CurrentCell.ColumnIndex >= abSelect.CurrentRow.Cells["strName"].ColumnIndex && abSelect.CurrentCell.ColumnIndex < abSelect.CurrentRow.Cells["MinValue"].ColumnIndex)
                {
                    //通道名称 可以输入任何值
                    if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8 && abSelect.CurrentCell.ColumnIndex != abSelect.CurrentRow.Cells["strName"].ColumnIndex)
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        /// <summary>
        /// 通道显示下拉框 鼠标滚轮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectChannelComBox_MouseWheel(object sender, MouseEventArgs e)
        {
            m_cellinit = false;
        }

        /// <summary>
        /// 通道显示下拉框 内容改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectChannelComBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_SelectPath = string.Format("{0}\\{1}", ChannelManageCloud.Default.ConfigruationBasePath, SelectChannelComBox.SelectedItem.ToString());
            ChangeTable(m_SelectPath);
            m_cellinit = false;
            abSelect.Focus();
            SelectChannelComBox.Capture = false;
        }

        /// <summary>
        /// 下拉框选择改动导联方案
        /// </summary>
        /// <param name="path"></param>
        private void ChangeTable(string path)
        {
            ChannelConfig channelConfig = new ChannelConfig(path);
            DataTable dt = path == GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelPath ? GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentDataTable : channelConfig.CurrentDataTable;
            if (dt.Rows.Count == 0)
            {
                dt = GlobalSingleton.Instance.User.DefaultChannelConfig.CurrentDataTable;
            }
            LoadData(dt, path);
        }

        /// <summary>
        /// 加载改动后的导联方案内容
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="path"></param>
        private void LoadData(DataTable dt, string path)
        {
            m_SelectPath = path;
            dt.AcceptChanges();
            m_DefineData = dt.Copy();
            if (FilterColumnLoadHandle != null)
            {
                abSelect.SuspendLayout();
                HighPass.Items.Clear();
                HighPass.Items.Add("Off");
                HighPass.Items.AddRange(FilterColumnLoadHandle.Invoke(HighPass.DataPropertyName));
                LowPass.Items.Clear();
                LowPass.Items.Add("Off");
                LowPass.Items.AddRange(FilterColumnLoadHandle.Invoke(LowPass.DataPropertyName));
                SingleNotch.Items.Clear();
                SingleNotch.Items.Add("Off");
                SingleNotch.Items.AddRange(FilterColumnLoadHandle.Invoke(SingleNotch.DataPropertyName));
                abSelect.ResumeLayout();
            }
            abSelect.DataSource = m_DefineData;
            Bak_DefineData = m_DefineData.Copy();
        }

        /// <summary>
        /// 通道管理 保存按钮 handle
        /// </summary>
        /// <param name="args"></param>
        private void mmd_SaveMontageHandle(params object[] args)
        {
            if (args.Length == 0)
            {
                GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentDataTable = m_DefineData;
                Channel.Default.ChannelChanged();
                AhDung.MessageTip.ShowOk("本次方案已生效!");
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("本次方案已生效!", pSystem.LogManagement.LogLevel.WARN);
            }
            else if (args.Length == 1)
            {
                ChannelManageCloud.Default.SaveChannelConfig(m_DefineData, m_SelectPath);

                ChannelConfig channelConfig = new ChannelConfig(m_SelectPath);
                GlobalSingleton.Instance.User.CurrentChannelConfig = channelConfig;
                Channel.Default.ChannelChanged();
                AhDung.MessageTip.ShowOk("保存成功!");
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("保存成功!", pSystem.LogManagement.LogLevel.WARN);
            }
            else if (args.Length == 2)
            {
                if (Convert.ToBoolean(args[1]))
                {
                    string path = string.Format("{0}\\{1}", ChannelManageCloud.Default.ConfigruationBasePath, args[0]);
                    ChannelManageCloud.Default.SaveChannelConfig(m_DefineData, path);

                    ChannelConfig channelConfig = new ChannelConfig(path);
                    GlobalSingleton.Instance.User.CurrentChannelConfig = channelConfig;
                    Channel.Default.ChannelChanged();
                    AhDung.MessageTip.ShowOk("方案已更改，并保存成功!");
                    DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("方案已更改，并保存成功!", pSystem.LogManagement.LogLevel.WARN);
                }
                else
                {
                    string path = string.Format("{0}\\{1}", ChannelManageCloud.Default.ConfigruationBasePath, args[0]);
                    ChannelManageCloud.Default.SaveChannelConfig(m_DefineData, path);
                    AhDung.MessageTip.ShowOk("新方案保存成功!");
                    DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("新方案保存成功!", pSystem.LogManagement.LogLevel.WARN);
                }
            }
            this.Close();
            this.Dispose();
        }

        /// <summary>
        /// 通道管理 克隆handle
        /// </summary>
        /// <param name="table"></param>
        private void channelclone_CloneChannelHandle(ChannelTable table)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                DataRow dr = ChannelTable.ConvertToDataRow(table, m_DefineData);
                m_DefineData.Rows.InsertAt(dr, table.Index);
                for (int i = table.Index + 1; i < m_DefineData.Rows.Count; i++)
                {
                    m_DefineData.Rows[i]["Index"] = i;
                }
                m_cellinit = false;
                GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentDataTable = m_DefineData;
                abSelect.Update();///防止滚动条刷新失败出现黑色线块

            }));
        }

        /// <summary>
        /// 颜色控件  点击后弹出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Control_DropDownOnClick(object sender, pSystem.UI.ReaLTaiizor.CustomCtrl.CustomCtrlEventArgs.DropDownOnClickEventArgs e)
        {
            ColorSettings colorSettings = new ColorSettings((ColorComboBox)sender, e.Color);
            colorSettings.StartPosition = FormStartPosition.Manual;
            colorSettings.Location = Cursor.Position;
            colorSettings.Location = EnsureLocationHelper.CalculateSituableLocation(colorSettings);
            colorSettings.ColorChanged += ColorSettings_ColorChanged;
            colorSettings.ShowDialog();
        }
        /// <summary>
        /// 颜色控件  改变颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorSettings_ColorChanged(object sender, pSystem.UI.ReaLTaiizor.CustomCtrl.CustomCtrlEventArgs.ColorChangedEventArgs e)
        {
            if (!(sender is ColorSettings))
                return;

            ColorSettings colorSettings = sender as ColorSettings;

            abSelect.CurrentCell.Value = colorSettings.Sender.Color = e.Color;
            colorSettings.ColorChanged -= ColorSettings_ColorChanged;
            colorSettings.Close();
        }


        #endregion

        #region 数据表方法

        /// <summary>
        /// 鼠标点击按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void abSelect_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //点击序号会报错
            if (e.ColumnIndex == -1)
            {
                return;
            }
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex > -1 && abSelect.Columns[e.ColumnIndex].Name == "AntipolePic")
                {
                    contextMenuStrip1.Tag = e.ColumnIndex;
                    contextMenuStrip1.Show(abSelect, abSelect.PointToClient(Cursor.Position));
                }
                return;
            }
        }

        /// <summary>
        /// 鼠标点击数据表单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void abSelect_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //点击序号会报错
            if (e.ColumnIndex == -1)
            {
                return;
            }
            ///防止下拉框点击两次
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && abSelect[e.ColumnIndex, e.RowIndex] != null && !abSelect[e.ColumnIndex, e.RowIndex].ReadOnly)
            {
                if (abSelect.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn)
                {
                    abSelect.BeginEdit(true);
                    DataGridViewComboBoxEditingControl comboBoxEditingControl = abSelect.EditingControl as DataGridViewComboBoxEditingControl;
                    if (comboBoxEditingControl != null)
                    {
                        if (FilterDropDownHandle != null && e.ColumnIndex > m_SensitivityColumnIndex)
                        {
                            DataGridViewComboBoxCell cell = abSelect.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell;
                            string[] dd = FilterDropDownHandle.Invoke(Convert.ToInt32( abSelect.Rows[e.RowIndex].Cells["ID"].Value), abSelect.Columns[e.ColumnIndex].DataPropertyName);
                            cell.Items.Clear();
                            comboBoxEditingControl.Items.Clear();
                            cell.Items.Add("Off");
                            comboBoxEditingControl.Items.Add("Off");
                            if (dd.Length > 0)
                            {
                                cell.Items.AddRange(dd);
                                comboBoxEditingControl.Items.AddRange(dd);
                            }

                        }
                        else if (abSelect.Columns[e.ColumnIndex].Name == "Sensitivity" && comboBoxEditingControl.Items.Count == 0)///防止重置时灵敏度下拉选项空的问题
                        {
                            string channelid = abSelect.Rows[m_cellinitCnt].Cells["ChannelID"].Value.ToString();

                            string[] rangs = new string[] { "Off" };
                            ChannelTable find = GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelTable.Find(t => t.ChannelID == channelid);
                            if (find != null)
                                rangs = find.strPixelRange.Split('\\');
                            DataGridViewComboBoxCell cell = abSelect.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell;
                            cell.Items.AddRange(rangs);
                            comboBoxEditingControl.Items.AddRange(rangs);
                        }
                        comboBoxEditingControl.DroppedDown = true;
                    }
                }
                else
                {
                    if (abSelect.Columns[e.ColumnIndex] is ColorSelectPickerColumn)
                    {
                        ColorSelectPickerControl colorSelectControl = abSelect.EditingControl as ColorSelectPickerControl;
                        if (colorSelectControl != null)
                            colorSelectControl.DropDownOnClick += Control_DropDownOnClick;
                    }
                }
            }
            //点击之后未选中和选中是两个不同的图片
            if (abSelect.Columns[e.ColumnIndex].Name == "AntipolePic" && e.RowIndex >= 0)
            {

                m_DefineData.Rows[e.RowIndex]["Antipole"] = !Convert.ToBoolean(m_DefineData.Rows[e.RowIndex]["Antipole"]);
                abSelect.InvalidateCell(e.ColumnIndex, e.RowIndex);
            }
        }

        /// <summary>
        /// 数据表单元格重绘，新增列会自动重绘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void abSelect_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (abSelect.Columns[e.ColumnIndex].Name == "Sensitivity" && !m_cellinit)///通过监测发现该事件在第一次完成后会反复被执行，添加m_cellinit标识就是为了 防止一直被执行
            {///m_cellinitCnt代替了e.rowIndex,因为隐藏的行触发时这个值有问题，不知啥原因。
                DataGridView dgv = (DataGridView)sender;
                DataGridViewComboBoxCell cell = abSelect.Rows[m_cellinitCnt].Cells[e.ColumnIndex] as DataGridViewComboBoxCell;
                if (cell.Items.Count == 0)
                {
                    string channelid = abSelect.Rows[m_cellinitCnt].Cells["ChannelID"].Value.ToString();
                    string[] rangs = new string[] { "Off" };
                    ChannelTable find = GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelTable.Find(t => t.ChannelID == channelid);
                    if (find != null)
                        rangs = find.strPixelRange.Split('\\');
                    cell.Items.Clear();
                    cell.Items.AddRange(rangs);
                }
                m_cellinitCnt++;
                if (m_cellinitCnt == abSelect.Rows.Count)
                {
                    m_cellinitCnt = 0;
                    m_cellinit = true;
                }
            }
            else if (abSelect.Columns[e.ColumnIndex].Name == "AntipolePic")
            {
                e.Value = !Convert.ToBoolean(m_DefineData.Rows[e.RowIndex]["Antipole"]) ? Properties.Resources.Antipole2 : Properties.Resources.Antipole1;
            }
        }

        /// <summary>
        /// 显示编辑控件的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void abSelect_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (abSelect.EditingControl != null)
                this.abSelect.EditingControl.KeyPress += Text_KeyPress;//为当前编辑控件添加事件
        }

        /// <summary>
        /// 单元格内容修改后，退出编辑模式时候 发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void abSelect_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if (abSelect.EditingControl != null)
                this.abSelect.EditingControl.KeyPress -= Text_KeyPress;//为当前编辑控件删除事件
        }

        /// <summary>
        /// 数据表错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void abSelect_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        #endregion

        #region 按钮方法

        /// <summary>
        /// 通道管理 保存按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (m_DefineData == null)
            {
                AhDung.MessageTip.ShowWarning("当前无待保存方案");
                return;
            }
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("用户点击 通道管理-保存按钮");
            DataGridViewCell cell = abSelect.CurrentCell;
            abSelect.EndEdit();
            abSelect.CurrentCell = null;
            bool ready = true;
            string[] channelNames = new string[m_DefineData.Rows.Count];
            for (int i = 0; i < m_DefineData.Rows.Count; i++)
            {
                float max = 0, min = 0;
                channelNames[i] = m_DefineData.Rows[i][1].ToString();
                try
                {
                    Convert.ToBoolean(m_DefineData.Rows[i]["State"]);
                }
                catch
                {
                    ready = false;
                    break;
                }
                try
                {
                    max = Convert.ToSingle(m_DefineData.Rows[i]["MaxValue"]);
                }
                catch
                {
                    ready = false;
                    break;
                }
                try
                {
                    min = Convert.ToSingle(m_DefineData.Rows[i]["MinValue"]);
                }
                catch
                {
                    ready = false;
                    break;
                }
                if (max <= min)
                {
                    ready = false;
                    break;
                }
            }
            var last = channelNames.Distinct();
            if (last.Count() != channelNames.Length)
            {
                AhDung.MessageTip.ShowWarning("通道名称不能出现重复项！");
                return;
            }
            if (ready)
            {
                MontageDialog mmd = new MontageDialog();
                mmd.CurrentName = m_SelectPath.Remove(0, ChannelManageCloud.Default.ConfigruationBasePath.Length + 1);
                mmd.SaveMontageHandle += mmd_SaveMontageHandle;
                mmd.ShowDialog();
            }
            else
            {
                AhDung.MessageTip.ShowWarning("表格中数值输入格式不正确或最大值最小值设定不合规！");
            }
        }

        /// <summary>
        /// 通道管理 派生按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeriveButton_Click(object sender, EventArgs e)
        {
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("用户点击 通道管理-派生按钮");
            ChannelCloneDialog channelclone = new ChannelCloneDialog();
            List<ChannelTable> tables = new List<ChannelTable>(m_DefineData.Rows.Count);
            for (int i = 0; i < m_DefineData.Rows.Count; i++)
            {
                ChannelTable table = ChannelTable.ConvertToChannel(m_DefineData.Rows[i]);
                tables.Add(table);
            }
            channelclone.Init(tables);
            channelclone.CloneChannelHandle += channelclone_CloneChannelHandle;
            channelclone.ShowDialog();
        }

        /// <summary>
        /// 通道管理 重置按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetButton_Click(object sender, EventArgs e)
        {
            if (MessageForm.Show(m_strResetAlert, m_strAlert, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("用户点击 通道管理-重置按钮 确认重置");
                abSelect.EndEdit();
                m_DefineData = Bak_DefineData.Copy();
                m_cellinit = false;
                abSelect.DataSource = m_DefineData;
            }
        }

        /// <summary>
        /// 全选/全不选 勾选点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllCheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool checkall = Convert.ToInt16((sender as ToolStripMenuItem).Tag) == 1;
            int columIndex = Convert.ToInt16(contextMenuStrip1.Tag);
            for (int i = 0; i < m_DefineData.Rows.Count; i++)
            {
                m_DefineData.Rows[i]["Antipole"] = checkall;
            }
            abSelect.InvalidateColumn(columIndex);
        }

        #endregion

        #region 公有方法
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="path"></param>
        public void Init(string path)
        {
            SelectChannelComBox.SelectedIndexChanged += SelectChannelComBox_SelectedIndexChanged;

            string filepath = ChannelManageCloud.Default.ConfigruationBasePath;
            string name = path.Replace(string.Format("{0}\\", ChannelManageCloud.Default.ConfigruationBasePath), "");
            if (Directory.Exists(filepath))
            {
                SelectChannelComBox.Items.Clear();
                int len = filepath.Length + 1;
                string[] names = Directory.GetFiles(filepath, "*.cfg");
                if (names.Length > 0)
                {
                    SelectChannelComBox.Items.AddRange(names.Select(t => t.Remove(0, len)).ToArray());
                }
                else
                {
                    SelectChannelComBox.Items.Add(name);
                }
            }
            SelectChannelComBox.Visible = true;
            if (SelectChannelComBox.Items.Contains(name))
                SelectChannelComBox.SelectedItem = name;
            else
            {
                if (SelectChannelComBox.Items.Count > 0)
                    SelectChannelComBox.SelectedIndex = 0;
            }
        }
        #endregion

    }
}
