using AwareTec.Polysmith.UI.DataModel;
using AwareTec.Polysmith.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using AwareTec.Polysmith.UI.FunctionControls.tools;

namespace AwareTec.Polysmith.UI.Block
{
    public partial class Montage : SkinForm
    {
        private DataTable m_DefineData = null;
        private DataTable Bak_DefineData = null;
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
            //this.abSelect.CellDoubleClick += abSelect_CellDoubleClick;
            //abSelect.SelectionChanged += abSelect_SelectionChanged;
            abSelect.CellMouseDown += abSelect_CellMouseDown;
            //abSelect.CellMouseMove += abSelect_CellMouseMove;
            //abSelect.DragDrop += abSelect_DragDrop;
            //abSelect.DragEnter += abSelect_DragEnter;
            abSelect.CellClick += abSelect_CellClick;
            abSelect.DataError += abSelect_DataError;
            abSelect.CellBeginEdit += abSelect_CellBeginEdit;
            abSelect.CellFormatting += abSelect_CellFormatting;
            abSelect.EditingControlShowing += abSelect_EditingControlShowing;
            abSelect.CellParsing += abSelect_CellParsing;
            this.Load += Capture_Load;
            this.KeyDown += Montage_KeyDown;
        }
        private void Text_KeyPress(object sender, KeyPressEventArgs e)//处理方法
        {
            if (abSelect.CurrentCell != null)
            {
                if (abSelect.CurrentCell.ColumnIndex > 1 && abSelect.CurrentCell.ColumnIndex < 5)
                {
                    //通道名称 可以输入任何值
                    if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8 && abSelect.CurrentCell.ColumnIndex != 2)
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void abSelect_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)//显示编辑控件的事件
        {
            if (abSelect.EditingControl != null)
                this.abSelect.EditingControl.KeyPress += Text_KeyPress;//为当前编辑控件添加事件
        }

        private void abSelect_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if (abSelect.EditingControl != null)
                this.abSelect.EditingControl.KeyPress -= Text_KeyPress;//为当前编辑控件删除事件
        }

        private void Montage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (abSelect.CurrentRow != null)
                {
                    string strid = abSelect.CurrentRow.Cells["ID"].Value.ToString();
                    if (strid.Contains("Clone_") || strid.Contains("Append_"))
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

        private bool m_cellinit = false;
        private int m_cellinitCnt = 0;
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
                object tag = dgv.Rows[m_cellinitCnt].Cells["PixelEnable"].Value;
                DataGridViewComboBoxCell cell = abSelect.Rows[m_cellinitCnt].Cells[e.ColumnIndex] as DataGridViewComboBoxCell;
                if (cell.Items.Count == 0)
                {
                    string channelid = abSelect.Rows[m_cellinitCnt].Cells["ID"].Value.ToString().Replace("Clone_", "");
                    if (channelid.Contains("Append_"))
                        channelid = channelid.Replace("Append_", "").Split(':')[0];
                    string[] rangs = new string[] { "Off" };
                    DataBaseCom.Doc_Channel find = Channel.Default.ChannelProperties.Find(t => t.Name == channelid);
                    if (find != null)
                        rangs = find.strPixelRangArray;
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
                e.Value = !Convert.ToBoolean(m_DefineData.Rows[e.RowIndex]["Antipole"]) ? Properties.Resources.copyno : Properties.Resources.copytrue;
            }
        }

        private void abSelect_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            //判断是否可以编辑  
            if (abSelect.Columns[e.ColumnIndex].Name == "Sensitivity")
            {
                object tag = dgv.Rows[e.RowIndex].Cells["PixelEnable"].Value;
                if (!bool.Parse(tag.ToString()))
                {
                    //编辑不能  
                    e.Cancel = true;
                }
            }
        }
        private void abSelect_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //点击序号会报错
            if(e.ColumnIndex==-1)
            {
                return;
            }
            ///防止下拉框点击两次
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && abSelect[e.ColumnIndex, e.RowIndex] != null && !abSelect[e.ColumnIndex, e.RowIndex].ReadOnly)
            {
                DataGridViewComboBoxColumn comboBoxColumn = abSelect.Columns[e.ColumnIndex] as DataGridViewComboBoxColumn;
                if (comboBoxColumn != null)
                {
                    abSelect.BeginEdit(true);
                    DataGridViewComboBoxEditingControl comboBoxEditingControl = abSelect.EditingControl as DataGridViewComboBoxEditingControl;
                    if (comboBoxEditingControl != null)
                    {
                        if (FilterDropDownHandle != null && e.ColumnIndex > 8)
                        {
                            DataGridViewComboBoxCell cell = abSelect.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell;
                            string[] dd = FilterDropDownHandle.Invoke(abSelect.Rows[e.RowIndex].Cells["ID"].Value.ToString(), abSelect.Columns[e.ColumnIndex].DataPropertyName);
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
                        else if (e.ColumnIndex == 8 && comboBoxEditingControl.Items.Count == 0)///防止重置时灵敏度下拉选项空的问题
                        {
                            string channelid = abSelect.Rows[m_cellinitCnt].Cells["ID"].Value.ToString().Replace("Clone_", "");
                            if (channelid.Contains("Append_"))
                                channelid = channelid.Replace("Append_", "").Split(':')[0];
                            string[] rangs = new string[] { "Off" };
                            DataBaseCom.Doc_Channel find = Channel.Default.ChannelProperties.Find(t => t.Name == channelid);
                            if (find != null)
                                rangs = find.strPixelRangArray;
                            DataGridViewComboBoxCell cell = abSelect.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell;
                            cell.Items.AddRange(rangs);
                            comboBoxEditingControl.Items.AddRange(rangs);
                        }
                        comboBoxEditingControl.DroppedDown = true;
                    }
                }
            }
            //点击之后未选中和选中是两个不同的图片
            if (abSelect.Columns[e.ColumnIndex].Name == "AntipolePic" && e.RowIndex >= 0)
            {

                m_DefineData.Rows[e.RowIndex]["Antipole"] = !Convert.ToBoolean(m_DefineData.Rows[e.RowIndex]["Antipole"]);
                abSelect.InvalidateCell(e.ColumnIndex,e.RowIndex);
            }
        }

        private void abSelect_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
        private void Capture_Load(object sender, EventArgs e)
        {
            //toolStripButton1.Text = m_strSelectAll;
            //toolStripButton2.Text = m_strInvertSelect;
            ResetButton.Text = m_strReset;
            SaveButton.Text = m_strSave;
            for (int i = 1; i < abSelect.Columns.Count; i++)
            {
                abSelect.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                if (i == 3)
                    abSelect.Columns[i].ReadOnly = true;
                else
                    abSelect.Columns[i].ReadOnly = false;
              
            }
            cbselectchannel.MouseWheel += ComboBox_MouseWheel;
            //if (Channel.Default.IsRealTimeView)
            //{
            //    DeriveButton.Visible = false;
            //    toolStripSeparator3.Visible = false;
            //}
        }

        void ComboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            m_cellinit = false;
        }
        private string m_SelectPath = "";
        private void init(bool visible)
        {
            cbselectchannel.Visible = visible;
        }
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
            ChanggeSelectAllState();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dt"></param>
        public void Init(DataTable dt, string path)
        {
            init(false);
            LoadData(dt, path);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="path"></param>
        public void Init(string path)
        {
            string filepath = ChannelManage.Default.ConfigruationBasePath;
            string name = path.Replace(string.Format("{0}\\", ChannelManage.Default.ConfigruationBasePath), "");
            if (Directory.Exists(filepath))
            {
                cbselectchannel.Items.Clear();
                int len = filepath.Length + 1;
                string[] names = Directory.GetFiles(filepath, "*.cfg");
                if (names.Length > 0)
                {
                    cbselectchannel.Items.AddRange(names.Select(t => t.Remove(0, len)).ToArray());
                }
                else
                {
                    cbselectchannel.Items.Add(name);
                }
            }
            init(true);
            if (cbselectchannel.Items.Contains(name))
                cbselectchannel.SelectedItem = name;
            else
            {
                if (cbselectchannel.Items.Count > 0)
                    cbselectchannel.SelectedIndex = 0;
            }
        }

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
        #region 拖动效果
        private int selectionIdx = 0;
        private void abSelect_SelectionChanged(object sender, EventArgs e)
        {
            if (abSelect.Rows.Count > 0 && selectionIdx > -1 && selectionIdx < abSelect.Rows.Count - 1)// (dgv.SelectedRows.Count > 0))
            {

                if (abSelect.Rows.Count <= selectionIdx)
                    selectionIdx = abSelect.Rows.Count - 1;
                abSelect.Rows[selectionIdx].Selected = true;
                abSelect.CurrentCell = abSelect.Rows[selectionIdx].Cells[0];
            }
        }

        private void abSelect_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex > -1 && abSelect.Columns[e.ColumnIndex].Name == "AntipolePic")
                {
                    contextMenuStrip1.Tag = e.ColumnIndex;
                    contextMenuStrip1.Show(abSelect, abSelect.PointToClient(Cursor.Position));
                }
                return;
            }
            if (e.RowIndex >= 0)
                selectionIdx = e.RowIndex;
        }

        private void abSelect_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.Clicks < 2) && (e.Button == MouseButtons.Left))
            {
                if ((e.RowIndex > -1))
                    abSelect.DoDragDrop(abSelect.Rows[e.RowIndex], DragDropEffects.Move);
            }
        }

        private void abSelect_DragDrop(object sender, DragEventArgs e)
        {
            int idx = GetRowFromPoint(e.X, e.Y);
            if (idx < 0) return;

            if (e.Data.GetDataPresent(typeof(DataGridViewRow)))
            {
                DataGridViewRow row = (DataGridViewRow)e.Data.GetData(typeof(DataGridViewRow));
                int oldIdx = row.Index;
                if (oldIdx == idx)
                    return;
                DataRow[] ss = m_DefineData.Select(string.Format("ID='{0}'", row.Cells["ID"].Value.ToString()));
                object[] tt = ss[0].ItemArray;
                int iix = Convert.ToInt32(ss[0]["Index"]);
                DataRow dsour = Bak_DefineData.Rows[iix];
                string DesId = m_DefineData.Rows[idx]["ID"].ToString();
                m_DefineData.Rows.Remove(ss[0]);
                Bak_DefineData.Rows.RemoveAt(iix);
                int desidx = 0;
                for (int i = 0; i < Bak_DefineData.Rows.Count; i++)
                {
                    if (Bak_DefineData.Rows[i]["ID"].ToString() == DesId)
                    {
                        desidx = idx > oldIdx ? i + 1 : i;
                        break;
                    }
                }
                m_DefineData.Rows.InsertAt(ss[0], idx);
                abSelect.CurrentCell = abSelect.Rows[idx].Cells[0];
                for (int i = 0; i < m_DefineData.Columns.Count; i++)
                {
                    m_DefineData.Rows[idx][i] = tt[i];
                }
                Bak_DefineData.Rows.InsertAt(dsour, desidx);
                for (int i = 0; i < Bak_DefineData.Columns.Count; i++)
                {
                    Bak_DefineData.Rows[desidx][i] = tt[i];
                }
                for (int i = 0; i < Bak_DefineData.Rows.Count; i++)
                {
                    Bak_DefineData.Rows[i]["Index"] = i;
                    DataRow[] sr = m_DefineData.Select(string.Format("ID='{0}'", Bak_DefineData.Rows[i]["ID"].ToString()));
                    if (sr.Length == 1)
                    {
                        sr[0]["Index"] = i;
                    }
                }
                selectionIdx = idx;
            }
        }

        private void abSelect_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private int GetRowFromPoint(int x, int y)
        {
            for (int i = 0; i < abSelect.RowCount; i++)
            {
                Rectangle rec = abSelect.GetRowDisplayRectangle(i, false);

                if (abSelect.RectangleToScreen(rec).Contains(x, y))
                    return i;
            }

            return -1;
        }
        #endregion
        /// <summary>
        /// 清空
        /// </summary>
        public void clear()
        {
            m_DefineData.Rows.Clear();
        }
        public string m_strAlert = "信息提示";
        public string m_strResetAlert = "是否需要恢复到初始设置?";
        public string m_strSaveAlert = "是否需要保存当前配置方案?";
        public string m_strSelectNone = "全不选";
        public string m_strSave = Program.Language == "EN" ? "Save" : "保存";
        public string m_strInvertSelect = "反选";
        public string m_strSelectAll = "全选";
        public string m_strCaptureFilter = "Capture Filter";
        public string m_strUndefined = "未定义";
        public string m_strReset =Program.Language=="EN"?"Reset":"重置";

        private void abSelect_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 0 && e.RowIndex > 0)
            {
                string ID = abSelect.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                DataRow[] dr = m_DefineData.Select(string.Format("ID='{0}'", ID));
                if (dr.Length > 0)
                {
                    bool ret = Convert.ToBoolean(dr[0]["State"]);
                    dr[0]["State"] = !ret;
                    ChanggeSelectAllState();
                }
            }
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            abSelect.EndEdit();
            //if (toolStripButton1.Text == m_strSelectAll)
            //{
            //    toolStripButton1.Text = m_strSelectNone;
            //    toolStripButton1.BackColor = Color.Gold;
            //    for (int i = 0; i < m_DefineData.Rows.Count; i++)
            //    {
            //        m_DefineData.Rows[i]["State"] = true;
            //    }
            //}
            //else
            //{
            //    toolStripButton1.Text = m_strSelectAll;
            //    toolStripButton1.BackColor = Color.FromArgb(215, 223, 231);
            //    for (int i = 0; i < m_DefineData.Rows.Count; i++)
            //    {
            //        m_DefineData.Rows[i]["State"] = false;
            //    }
            //}
            ChanggeSelectAllState();
            if (abSelect.CurrentRow != null)
            {
                abSelect.InvalidateCell(abSelect.CurrentRow.Cells[0]);
            }
            // abSelect.Invalidate();
        }
        /// <summary>
        /// 滤波器动态加载委托
        /// </summary>
        /// <param name="CaptureResult"></param>
        /// <returns></returns>
        public delegate string[] FilterDropDownDelegate(string ID, string typ);
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
        /// <summary>
        ///重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetButton_Click(object sender, EventArgs e)
        {
            if (MessageForm.Show(m_strResetAlert, m_strAlert, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataModel.LogInstance.Default.AddLog("用户点击 通道管理-重置按钮 确认重置");
                abSelect.EndEdit();
                // selectIdxbyprocess = true;
                m_DefineData = Bak_DefineData.Copy();
                m_cellinit = false;
                abSelect.DataSource = m_DefineData;
                ChanggeSelectAllState();
                //abSelect.Invalidate();//不需要
            }
        }
        /// <summary>
        /// 保存事件
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
            DataModel.LogInstance.Default.AddLog("用户点击 通道管理-保存按钮");
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
                    Convert.ToInt16(m_DefineData.Rows[i][2]);
                }
                catch
                {
                    ready = false;
                    break;
                }
                try
                {
                    max = Convert.ToSingle(m_DefineData.Rows[i][3]);
                }
                catch
                {
                    ready = false;
                    break;
                }
                try
                {
                    min = Convert.ToSingle(m_DefineData.Rows[i][4]);
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
                mmd.CurrentName = m_SelectPath.Remove(0, ChannelManage.Default.ConfigruationBasePath.Length + 1);
                mmd.SaveMontageHandle += mmd_SaveMontageHandle;
                mmd.ShowDialog();
            }
            else
            {
                AhDung.MessageTip.ShowWarning("表格中数值输入格式不正确或最大值最小值设定不合规！");
            }
        }

        private void mmd_SaveMontageHandle(params object[] args)
        {
            if (args.Length == 0)
            {
                Channel.Default.CurrentChannelTable = m_DefineData;
                Channel.Default.CurrentChannelPath = m_SelectPath;
                Channel.Default.ChannelChanged();
                AhDung.MessageTip.ShowOk("本次方案已生效!");
                DataModel.LogInstance.Default.AddLog("本次方案已生效!", pSystem.LogManagement.LogLevel.WARN);
            }
            else if (args.Length == 1)
            {
                ChannelManage.Default.SaveChannelConfig(m_DefineData, m_SelectPath);
                Channel.Default.CurrentChannelTable = m_DefineData;
                Channel.Default.CurrentChannelPath = m_SelectPath;
                Channel.Default.ChannelChanged();
                AhDung.MessageTip.ShowOk("保存成功!");
                DataModel.LogInstance.Default.AddLog("保存成功!", pSystem.LogManagement.LogLevel.WARN);
            }
            else if (args.Length == 2)
            {
                if (Convert.ToBoolean(args[1]))
                {
                    string path = string.Format("{0}\\{1}", ChannelManage.Default.ConfigruationBasePath, args[0]);
                    ChannelManage.Default.SaveChannelConfig(m_DefineData, path);
                    Channel.Default.CurrentChannelTable = m_DefineData;
                    Channel.Default.CurrentChannelPath = path;
                    Channel.Default.ChannelChanged();
                    AhDung.MessageTip.ShowOk("方案已更改，并保存成功!");
                    DataModel.LogInstance.Default.AddLog("方案已更改，并保存成功!", pSystem.LogManagement.LogLevel.WARN);
                }
                else
                {
                    string path = string.Format("{0}\\{1}", ChannelManage.Default.ConfigruationBasePath, args[0]);
                    ChannelManage.Default.SaveChannelConfig(m_DefineData, path);
                    AhDung.MessageTip.ShowOk("新方案保存成功!");
                    DataModel.LogInstance.Default.AddLog("新方案保存成功!", pSystem.LogManagement.LogLevel.WARN);
                }
            }
            this.Close();
            this.Dispose();
        }
        /// <summary>
        /// 反选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            abSelect.EndEdit();
            for (int i = 0; i < m_DefineData.Rows.Count; i++)
            {
                if (Convert.ToBoolean(m_DefineData.Rows[i]["State"]))
                {
                    m_DefineData.Rows[i]["State"] = false;
                }
                else
                {
                    m_DefineData.Rows[i]["State"] = true;
                }
            }
            ChanggeSelectAllState();
            if (abSelect.CurrentRow != null)
            {
                abSelect.InvalidateCell(abSelect.CurrentRow.Cells[0]);
            }
            abSelect.Invalidate();
        }

        private void ChanggeSelectAllState()
        {
            //DataRow[] all = m_DefineData.Select("State=false");
            //if (all.Length == 0)
            //{
            //    toolStripButton1.Text = m_strSelectNone;
            //    toolStripButton1.BackColor = Color.Gold;
            //}
            //else if (all.Length >= m_DefineData.Rows.Count - 1)
            //{
            //    toolStripButton1.Text = m_strSelectAll;
            //    toolStripButton1.BackColor = Color.FromArgb(215, 223, 231);
            //}
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            abSelect.EndEdit();
            DataRow[] dr = m_DefineData.Select("State=true");
            if (dr.Length > 0)
            {
                BatchUnit unit = new BatchUnit();
                unit.PixelRateItems = new List<string>(Sensitivity.Items.Count);
                for (int i = 0; i < Sensitivity.Items.Count; i++)
                    unit.PixelRateItems.Add(Sensitivity.Items[i].ToString());
                unit.SingleNotchItems = new List<string>(SingleNotch.Items.Count);
                for (int i = 0; i < SingleNotch.Items.Count; i++)
                    unit.SingleNotchItems.Add(SingleNotch.Items[i].ToString());
                unit.HighPassItems = new List<string>(HighPass.Items.Count);
                for (int i = 0; i < HighPass.Items.Count; i++)
                    unit.HighPassItems.Add(HighPass.Items[i].ToString());
                unit.LowPassItems = new List<string>(LowPass.Items.Count);
                for (int i = 0; i < LowPass.Items.Count; i++)
                    unit.LowPassItems.Add(LowPass.Items[i].ToString());
                ChannelMulEdit edit = new ChannelMulEdit();
                edit.Owner = this;
                edit.Init(unit);
                edit.CommitBatchUnitHandle += edit_CommitBatchUnitHandle;
                edit.ShowDialog();
            }
            else
            {
                AhDung.MessageTip.ShowError("当前未选择通道");
            }
        }

        private void edit_CommitBatchUnitHandle(BatchUnit unit)
        {
            for (int i = 0; i < m_DefineData.Rows.Count; i++)
            {
                if (Convert.ToBoolean(m_DefineData.Rows[i]["State"]))
                {
                    if (unit.PixelRateEnable)
                    {
                        abSelect.Rows[i].Cells["Sensitivity"].Value = unit.PixelRate;
                    }
                    if (unit.PenColorEnable)
                    {
                        abSelect.Rows[i].Cells["ColorSelect"].Value = unit.PenColor;
                    }
                    if (unit.MaxValueEnable)
                    {
                        abSelect.Rows[i].Cells["MaxValue"].Value = unit.MaxValue;
                    }
                    if (unit.MinValueEnable)
                    {
                        abSelect.Rows[i].Cells["MinValue"].Value = unit.MinValue;
                    }
                    if (unit.SampleSpanEnable)
                    {
                        abSelect.Rows[i].Cells["TimeSpan"].Value = unit.SampleSpan;
                    }
                    if (unit.SingleNotchEnable)
                    {
                        abSelect.Rows[i].Cells["SingleNotch"].Value = unit.SingleNotch;
                    }
                    if (unit.HighPassEnable)
                    {
                        abSelect.Rows[i].Cells["HighPass"].Value = unit.HighPass;
                    }
                    if (unit.LowPassEnable)
                    {
                        abSelect.Rows[i].Cells["LowPass"].Value = unit.LowPass;
                    }
                }
            }
            abSelect.EndEdit();
            AhDung.MessageTip.ShowOk("批量修改成功!");
        }
        private void ChangeTable(string path)
        {
            DataTable dt = path == Channel.Default.CurrentChannelPath ? Channel.Default.CurrentChannelTable : ChannelManage.Default.ReadChannelConfig(path);
            if (dt.Rows.Count == 0)
            {
                dt = Channel.Default.DefultChannelTable;
            }
            LoadData(dt, path);
        }
        private void cbselectchannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_SelectPath = string.Format("{0}\\{1}", ChannelManage.Default.ConfigruationBasePath, cbselectchannel.SelectedItem.ToString());
            ChangeTable(m_SelectPath);
            m_cellinit = false;
            abSelect.Focus();
            cbselectchannel.Capture = false;
        }
        /// <summary>
        /// 派生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeriveButton_Click(object sender, EventArgs e)
        {
            DataModel.LogInstance.Default.AddLog("用户点击 通道管理-派生按钮");
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

        private void channelclone_CloneChannelHandle(ChannelTable table)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                DataRow dr = ChannelTable.ConvertToDataRow(table, m_DefineData);
                m_DefineData.Rows.InsertAt(dr, table.ChannelNo);
                for (int i = table.ChannelNo + 1; i < m_DefineData.Rows.Count; i++)
                {
                    m_DefineData.Rows[i]["Index"] = i;
                }
                m_cellinit = false;
                abSelect.Update();///防止滚动条刷新失败出现黑色线块

            }));
        }
        /// <summary>
        /// 合成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SyntheticButton_Click(object sender, EventArgs e)
        {
            DataModel.LogInstance.Default.AddLog("用户点击 通道管理-合成按钮");
            ChannelCreatDialog creat = new ChannelCreatDialog();
            List<ChannelTable> tables = new List<ChannelTable>(m_DefineData.Rows.Count);
            for (int i = 0; i < m_DefineData.Rows.Count; i++)
            {
                ChannelTable table = ChannelTable.ConvertToChannel(m_DefineData.Rows[i]);
                tables.Add(table);
            }
            creat.Init(tables);
            creat.AppendChannelHandle += channelclone_CloneChannelHandle;
            creat.ShowDialog();
        }

        private void 全勾ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool checkall = Convert.ToInt16((sender as ToolStripMenuItem).Tag) == 1;
            int columIndex = Convert.ToInt16(contextMenuStrip1.Tag);
            for (int i = 0; i < m_DefineData.Rows.Count; i++)
            {
                m_DefineData.Rows[i]["Antipole"] = checkall;
            }
            abSelect.InvalidateColumn(columIndex);
        }

    }
}
