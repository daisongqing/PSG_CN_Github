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

namespace AwareTec.Polysmith.UI
{
    public partial class ChannelConfig :SkinForm
    {
        private DataTable m_DefineData = null;
        private DataTable Bak_DefineData = null;
        private string _path = AppDomain.CurrentDomain.BaseDirectory + "channels.cfg";
        public ChannelConfig()
        {
            InitializeComponent();
            this.abSelect.CellDoubleClick += abSelect_CellDoubleClick;
            abSelect.SelectionChanged += abSelect_SelectionChanged;
            abSelect.CellMouseDown += abSelect_CellMouseDown;
            abSelect.CellMouseMove += abSelect_CellMouseMove;
            abSelect.DragDrop += abSelect_DragDrop;
            abSelect.DragEnter += abSelect_DragEnter;
            this.FormClosed += Capture_FormClosed;
            this.Load += Capture_Load;
        }
        private void Capture_Load(object sender, EventArgs e)
        {
            toolStripButton1.Text = m_strSelectAll;
            toolStripButton2.Text = m_strInvertSelect;
            toolStripButton4.Text = m_strReset;
            toolStripButton3.Text = m_strSave;
            for (int i = 1; i < abSelect.Columns.Count; i++)
            {
                abSelect.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                abSelect.Columns[i].ReadOnly = false;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dt"></param>
        public void Init(DataTable dt)
        {
            m_DefineData = dt;
            abSelect.DataSource = m_DefineData;
            Bak_DefineData = m_DefineData.Copy();
            ChanggeSelectAllState();
        }
        private void Capture_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Bak_DefineData != null)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < Bak_DefineData.Rows.Count; i++)
                {
                    for (int j = 0; j < Bak_DefineData.Columns.Count; j++)
                    {
                        Bak_DefineData.Rows[i][j] = m_DefineData.Rows[i][j];
                        sb.AppendFormat("{0}|", Bak_DefineData.Rows[i][j]);
                    }
                    sb.Append(";");
                }
                string value = sb.ToString().Trim();
                SaveIds("00000000", value);
            }
        }
        private void SaveIds(string ID, string value)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                ID = string.Format("Cap{0}", ID);
                if (!File.Exists(_path))
                {
                    using (StreamWriter sw = new StreamWriter(_path))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n  <{0}>\r\n<{1} Value=\"{2}\" />\r\n</{0}>", "Captures", ID, value);
                        sw.WriteLine(sb);
                        sw.Close();
                    }
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    using (StreamReader sr = new StreamReader(_path, Encoding.UTF8))
                    {
                        string sOneLine = "";
                        while (sr.Peek() > 0)
                        {
                            sOneLine = sr.ReadLine();
                            sb.AppendFormat("{0}\r\n", sOneLine);
                            System.Threading.Thread.Sleep(5);
                        }
                        sr.Close();
                    }
                    doc.LoadXml(sb.ToString());
                    XmlNode xmls = doc.SelectSingleNode("Captures");
                    bool find = false;
                    foreach (XmlNode node in xmls.ChildNodes)
                    {
                        if (node.Name == ID)
                        {
                            node.Attributes["Value"].InnerText = value;
                            find = true;
                            break;
                        }
                    }
                    if (!find)
                    {
                        XmlElement addNode = doc.CreateElement(ID);
                        addNode.SetAttribute("Value", null, value);
                        xmls.AppendChild(addNode);
                    }
                    using (TextWriter tw = new StreamWriter(_path, false, Encoding.UTF8))
                    {
                        doc.Save(tw);
                        tw.Close();
                    }
                }
            }
            catch (Exception ee) { }
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
        public string m_strSave = "保存";
        public string m_strInvertSelect = "反选";
        public string m_strSelectAll = "全选";
        public string m_strCaptureFilter = "Capture Filter";
        public string m_strUndefined = "未定义";
        public string m_strReset = Program.Language=="EN"?"Reset":"重置";

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
            if (toolStripButton1.Text == m_strSelectAll)
            {
                toolStripButton1.Text = m_strSelectNone;
                toolStripButton1.BackColor = Color.Gold;
                for (int i = 0; i < m_DefineData.Rows.Count; i++)
                {
                    m_DefineData.Rows[i]["State"] = true;
                }
            }
            else
            {
                toolStripButton1.Text = m_strSelectAll;
                toolStripButton1.BackColor = Color.FromArgb(215, 223, 231);
                for (int i = 0; i < m_DefineData.Rows.Count; i++)
                {
                    m_DefineData.Rows[i]["State"] = false;
                }
            }
            ChanggeSelectAllState();
            abSelect.Invalidate();
        }
        /// <summary>
        /// 接收用户决策委托
        /// </summary>
        /// <param name="CaptureResult"></param>
        /// <returns></returns>
        public delegate bool RecSaveDelegate(DataTable ChannelConfigTable);
        /// <summary>
        /// 接收用户决策事件
        /// </summary>
        public event RecSaveDelegate RecSaveEventHandle;
        /// <summary>
        ///重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (MessageForm.Show(m_strResetAlert, m_strAlert, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                abSelect.EndEdit();
                // selectIdxbyprocess = true;
                m_DefineData = Bak_DefineData.Copy();
                if (toolStripButton1.Text != m_strSelectAll)
                {
                    toolStripButton1.Text = m_strSelectAll;
                    toolStripButton1.BackColor = Color.FromArgb(215, 223, 231);
                }
                abSelect.DataSource = m_DefineData;
                ChanggeSelectAllState();
            }
        }
        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            abSelect.EndEdit();
            if (MessageForm.Show(m_strSaveAlert, m_strAlert, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                ///当前表格中的勾选情况，保留到备份表格中，防止用户决策丢失
                if (RecSaveEventHandle != null)
                {
                    bool ret = RecSaveEventHandle.Invoke(m_DefineData);
                    // if(MessageForm.Show(string.Format("Save {0}",ret?"OK":"Faild"),"Message Alert",MessageBoxButtons.OK)==DialogResult.OK)
                    {
                        this.Close();
                        this.Dispose();
                    }
                }
            }
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
            abSelect.Invalidate();
        }

        private void ChanggeSelectAllState()
        {
            DataRow[] all = m_DefineData.Select("State=false");
            if (all.Length == 0)
            {
                toolStripButton1.Text = m_strSelectNone;
                toolStripButton1.BackColor = Color.Gold;
            }
            else if (all.Length >= m_DefineData.Rows.Count - 1)
            {
                toolStripButton1.Text = m_strSelectAll;
                toolStripButton1.BackColor = Color.FromArgb(215, 223, 231);
            }
        }
    }
}
