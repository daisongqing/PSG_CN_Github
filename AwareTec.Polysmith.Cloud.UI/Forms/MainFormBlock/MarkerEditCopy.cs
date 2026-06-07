using AwareTec.Polysmith.Cloud.UI.DataModels;
using AwareTec.Polysmith.Cloud.UI.DataModels.Global;
using AwareTec.Polysmith.Cloud.UI.ViewHelpers;
using AwareTec.Polysmith.pChart;
using AwareTec.Polysmith.Util;
using pSystem.UI.ReaLTaiizor.CustomCtrl;
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
    public partial class MarkerEditCopy : CloudSkinForm
    {
        #region 私有变量

        private List<ChannelTable> m_tables = new List<ChannelTable>();
        private bool m_noHot = false;

        #endregion

        #region 公有变量

        /// <summary>
        /// 当前应用标记
        /// </summary>
        public IMarker CurrentMarker { set; get; }

        #endregion

        #region 事件委托

        public delegate void SaveMarkerDelegate(IMarker marker);
        /// <summary>
        /// 保存标记事件
        /// </summary>
        public event SaveMarkerDelegate SaveMarkerEventHandle;

        #endregion

        #region 初始化 load/dispose/init

        /// <summary>
        /// 初始化
        /// </summary>
        public MarkerEditCopy()
        {
            InitializeComponent();
            this.Load += MarkerEditCopy_Load;
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MarkerEditCopy_Load(object sender, EventArgs e)
        {
            this.CancelButton.Click += CancelButton_Click;
            this.SubmitButton.Click += SubmitButton_Click;
            this.colorSelectComboBox.DropDownOnClick += ColorSelectComboBox_DropDownOnClick;
            this.RbPointLabel.CheckedChanged += RbPointLabel_CheckedChanged;
            this.RbAreaLabel.CheckedChanged += RbAreaLabel_CheckedChanged;
            this.TbMinTime.KeyPress += TbMinTime_KeyPress;
            this.TbHotKey.KeyDown += HotKey_KeyDown;
            if (GlobalSingleton.Instance.User.CurrentChannelConfig != null)
                m_tables = GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelTable.Count() > 0 ? GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelTable : GlobalSingleton.Instance.User.DefaultChannelConfig.CurrentChannelTable;
            m_tables.RemoveAll(t => t.IsClone == true);
            m_tables.Sort((x, y) => x.Index.CompareTo(y.Index));
            //DataTable m_DefineData = Channel.Default.CurrentChannelTable.Rows.Count > 0 ? Channel.Default.CurrentChannelTable : Channel.Default.DefultChannelTable;
            //for (int i = 0; i < m_DefineData.Rows.Count; i++)
            //{
            //    ChannelTable table = ChannelTable.ConvertToChannel(m_DefineData.Rows[i]);
            //    if (!table.IsClone)
            //    {
            //        m_tables.Add(table);

            //    }
            //}
            checkBoxComboBox1.Items.Clear();
            if (CurrentMarker != null)
            {
                IMarker find = MarkerManage.Default.DefineMarkers.Find(t => t.Name == CurrentMarker.Name);
                if (find != null)
                {
                    if (find.AllowChannels != null)
                    {
                        List<ChannelTable> tables = m_tables.FindAll(t => find.AllowChannels.Contains(t.ID) && !t.IsClone);
                        if (tables.Count > 0)
                        {
                            checkBoxComboBox1.Items.AddRange(tables.Select(t => t.strName).ToArray());
                        }
                    }
                    else
                    {
                        checkBoxComboBox1.Items.AddRange(m_tables.Select(t => t.strName).ToArray());
                    }
                }
                else
                {
                    checkBoxComboBox1.Items.AddRange(m_tables.Select(t => t.strName).ToArray());
                }
                TbMarkName.Text = CurrentMarker.Name;
                TbMarkName.ReadOnly = true;
                TbMarkName.Enabled = false;
                TbMarkDetail.Text = CurrentMarker.Description;
                TbHotKey.Text = CurrentMarker.HotKey;
                colorSelectComboBox.Color = CurrentMarker.BackColor;
                string[] ss = CurrentMarker.Comments.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                int len = ss.Length;
                for (int i = 0; i < len; i++)
                {
                    for (int s = 0; s < checkBoxComboBox1.CheckBoxItems.Count; s++)
                    {
                        if (checkBoxComboBox1.CheckBoxItems[s].Text == m_tables.Find(t => t.ID == int.Parse(ss[i])).strName)
                        {
                            checkBoxComboBox1.CheckBoxItems[s].Checked = true;
                            break;
                        }
                    }
                }
                RbAreaLabel.Checked = CurrentMarker is pChart.RectangleMarkers;
                RbPointLabel.Checked = !RbAreaLabel.Checked;
                if (RbPointLabel.Checked)
                {
                    object test1 = new object();
                    EventArgs test2 = new EventArgs();
                    RbPointLabel_CheckedChanged(test1, test2);
                    RbPointLabel.Location = RbAreaLabel.Location;
                    RbAreaLabel.Visible = false;
                    checkBoxComboBox1.Enabled = false;
                }
                else
                {
                    RbPointLabel.Visible = false;
                    TbMinTime.Text = (CurrentMarker as pChart.RectangleMarkers).MinLimitValue.ToString();
                }
            }
            else
            {
                //新增事件在已存在已有的默认色后，需要给出提示更换一个颜色事件
                List<Color> colors = PhysioColor.Instance.AllColors;
                colors.Distinct();
                foreach (IMarker marker in MarkerManage.Default.DefineMarkers)
                {
                    if (colors.Contains(marker.BackColor))
                    {
                        colors.Remove(marker.BackColor);
                    }
                }
                this.colorSelectComboBox.Color = colors.Count > 0 ? colors[0] : System.Drawing.Color.Pink;
                checkBoxComboBox1.Items.AddRange(m_tables.Select(t => t.strName).ToArray());
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 单选框点击后 位置改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbPointLabel_CheckedChanged(object sender, EventArgs e)
        {
            if (RbPointLabel.Checked)
            {
                label10.Visible = label7.Visible = label8.Visible = label11.Visible = label9.Visible = TbMinTime.Visible = checkBoxComboBox1.Visible = false;
                int pianyi = label2.Location.Y + (label3.Location.Y - label2.Location.Y) / 2;
                TbHotKey.Location = new Point(TbHotKey.Location.X, pianyi - 5);
                label5.Location = new Point(label5.Location.X, pianyi);
                label4.Location = new Point(label4.Location.X, pianyi);
                colorSelectComboBox.Location = new Point(colorSelectComboBox.Location.X, pianyi - 5);
                m_noHot = true;
                TbHotKey.Text = "无";
            }
            else
            {
                label10.Visible = label7.Visible = checkBoxComboBox1.Visible = label8.Visible = label11.Visible = label9.Visible = TbMinTime.Visible = true;
                TbHotKey.Location = new Point(TbHotKey.Location.X, label2.Location.Y + 90);
                label5.Location = new Point(label5.Location.X, label2.Location.Y + 95);
                label4.Location = new Point(label4.Location.X, label2.Location.Y + 95);
                label8.Location = new Point(label8.Location.X, label2.Location.Y + 95);
                label11.Location = new Point(label11.Location.X, label2.Location.Y + 95);
                label9.Location = new Point(label9.Location.X, label2.Location.Y + 95);
                TbMinTime.Location = new Point(TbMinTime.Location.X, label2.Location.Y + 90);
                colorSelectComboBox.Location = new Point(colorSelectComboBox.Location.X, label2.Location.Y + 90);
                m_noHot = false;
                TbHotKey.Text = "";
            }
        }

        /// <summary>
        /// 单选框点击后 位置改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbAreaLabel_CheckedChanged(object sender, EventArgs e)
        {
            if (RbAreaLabel.Checked == false)
            {
                label10.Visible = label7.Visible = label8.Visible = label11.Visible = label9.Visible = TbMinTime.Visible = checkBoxComboBox1.Visible = false;
                int pianyi = label2.Location.Y + (label3.Location.Y - label2.Location.Y) / 2;
                TbHotKey.Location = new Point(TbHotKey.Location.X, pianyi - 5);
                label5.Location = new Point(label5.Location.X, pianyi);
                label4.Location = new Point(label4.Location.X, pianyi);
                colorSelectComboBox.Location = new Point(colorSelectComboBox.Location.X, pianyi - 5);
                m_noHot = true;
                TbHotKey.Text = "无";
            }
            else
            {
                label10.Visible = label7.Visible = checkBoxComboBox1.Visible = label8.Visible = label11.Visible = label9.Visible = TbMinTime.Visible = true;
                TbHotKey.Location = new Point(TbHotKey.Location.X, label2.Location.Y + 90);
                label5.Location = new Point(label5.Location.X, label2.Location.Y + 95);
                label4.Location = new Point(label4.Location.X, label2.Location.Y + 95);
                label8.Location = new Point(label8.Location.X, label2.Location.Y + 95);
                label11.Location = new Point(label11.Location.X, label2.Location.Y + 95);
                label9.Location = new Point(label9.Location.X, label2.Location.Y + 95);
                TbMinTime.Location = new Point(TbMinTime.Location.X, label2.Location.Y + 90);
                colorSelectComboBox.Location = new Point(colorSelectComboBox.Location.X, label2.Location.Y + 90);
                m_noHot = false;
                TbHotKey.Text = "";
            }
        }

        /// <summary>
        /// 颜色下拉框点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorSelectComboBox_DropDownOnClick(object sender, pSystem.UI.ReaLTaiizor.CustomCtrl.CustomCtrlEventArgs.DropDownOnClickEventArgs e)
        {
            ColorSettings colorSettings = new ColorSettings((ColorComboBox)sender, e.Color);
            colorSettings.StartPosition = FormStartPosition.Manual;
            colorSettings.Location = Cursor.Position;
            colorSettings.Location = EnsureLocationHelper.CalculateSituableLocation(colorSettings);
            colorSettings.ColorChanged += ColorSettings_ColorChanged;
            colorSettings.ShowDialog();
        }

        /// <summary>
        /// 颜色改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorSettings_ColorChanged(object sender, pSystem.UI.ReaLTaiizor.CustomCtrl.CustomCtrlEventArgs.ColorChangedEventArgs e)
        {
            if (!(sender is ColorSettings))
                return;

            ColorSettings colorSettings = sender as ColorSettings;

            colorSettings.Sender.Color = e.Color;
            colorSettings.ColorChanged -= ColorSettings_ColorChanged;
            colorSettings.Close();
        }

        /// <summary>
        /// 键盘快捷键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HotKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (m_noHot)
                return;
            if (CurrentMarker is pChart.RectangleMarkers)
            {
                if ((CurrentMarker as pChart.RectangleMarkers).ReadOnly)
                {
                    AhDung.MessageTip.ShowWarning("当前事件不能使用热键！");
                    return;
                }
            }
            if (!CheckNumber(e.KeyValue))
                return;
            string text = e.KeyCode.ToString();
            if (text.Contains(Keys.Control.ToString()) || text.Contains(Keys.Alt.ToString()) || text.Contains(Keys.Shift.ToString()))
                return;
            if (e.Control)
                text = string.Format("Ctrl + {0}", text);
            if (e.Alt)
                text = string.Format("Alt + {0}", text);
            if (e.Shift)
                text = string.Format("Shift + {0}", text);
            IMarker find = (CurrentMarker == null) ? MarkerManage.Default.DefineMarkers.Find(t => t.HotKey == text) : MarkerManage.Default.DefineMarkers.Find(t => t.HotKey == text && (t.Name != CurrentMarker.Name));
            if (find == null)
                TbHotKey.Text = text;
            else
            {
                AhDung.MessageTip.ShowError("热键组合已被使用！");
            }
        }

        /// <summary>
        /// 仅能输入数字和字母
        /// </summary>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        private bool CheckNumber(int keyCode)
        {
            // 数字
            if (keyCode >= 48 && keyCode <= 57) return true;
            // 小数字键盘
            else if (keyCode >= 96 && keyCode <= 105) return true;
            // 字母键
            else if (keyCode >= (int)Keys.A && keyCode <= (int)Keys.Z) return true;

            return false;
        }

        /// <summary>
        /// 文本框输入限制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbMinTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputTextCheck.floatvalue_KeyPress(sender, e);
        }

        #endregion

        #region 按钮方法

        /// <summary>
        /// 事件编辑 提交按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (SaveMarkerEventHandle != null)
            {
                StringBuilder sb = new StringBuilder();
                List<int> timespans = new List<int>();
                for (int i = 0; i < checkBoxComboBox1.CheckBoxItems.Count; i++)
                {
                    if (checkBoxComboBox1.CheckBoxItems[i].Checked)
                    {
                        ChannelTable chanTable = m_tables.Find(t => t.strName == checkBoxComboBox1.CheckBoxItems[i].Text);
                        sb.AppendFormat("{0}/", chanTable.ID);
                        timespans.Add(chanTable.SpanTime);
                    }
                }
                if (timespans.Distinct().Count() > 1)
                {
                    AhDung.MessageTip.ShowWarning("请确保事件的使用通道采样率一致");
                    return;
                }
                if (RbAreaLabel.Checked)
                {
                    string strMin = TbMinTime.Text.Trim();
                    if (strMin == "")
                    {
                        AhDung.MessageTip.ShowWarning("最小时域无效");
                        return;
                    }
                    CurrentMarker = new RectangleMarkers() { MinLimitValue = float.Parse(strMin) };
                }
                else
                    CurrentMarker = new StringMarkers();
                CurrentMarker.Name = TbMarkName.Text.Trim();
                if (CurrentMarker.Name == "")
                {
                    AhDung.MessageTip.ShowError("带*号的地方为必填项!");
                    return;
                }
                IMarker marker = MarkerManage.Default.DefineMarkers.Find(t => t.Name == CurrentMarker.Name);
                //对已有事件的修改不触发该判断
                if (marker != null && this.Text != "事件编辑")
                {
                    AhDung.MessageTip.ShowError("新增失败：事件名称重复!");
                    return;
                }
                if (marker != null)
                    CurrentMarker = marker;
                else
                {
                    //新增用户自定义事件所需要取到的一些属性
                    CurrentMarker.ClouduserId = MarkerManage.Default.DefineMarkers[0].ClouduserId;
                    CurrentMarker.Cloudmode = MarkerManage.Default.DefineMarkers[0].Cloudmode;
                    CurrentMarker.MarkTyp = IMarker.MarkType.None;
                    CurrentMarker.ID = "-1";
                    CurrentMarker.CloudisReadOnly = false;
                }
                CurrentMarker.BackColor = colorSelectComboBox.Color;
                CurrentMarker.Description = TbMarkDetail.Text;
                CurrentMarker.HotKey = TbHotKey.Text == "无" ? "" : TbHotKey.Text;
                CurrentMarker.Comments = sb.ToString().TrimEnd('/');
                if (RbAreaLabel.Checked)
                    (CurrentMarker as RectangleMarkers).MinLimitValue = Convert.ToSingle(TbMinTime.Text.Trim());
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("用户点击 事件编辑-提交按钮 编辑的事件名称为 {0}，分类为 {1}，最小时域为 {2}", CurrentMarker.Name, RbAreaLabel.Checked ? "区域标签" : "点位标签", TbMinTime.Text.Trim()));
                SaveMarkerEventHandle.BeginInvoke(CurrentMarker, null, null);
            }
            this.Close();
        }

        /// <summary>
        /// 事件编辑 取消按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("用户点击 事件编辑-取消按钮");
        }

        #endregion

        #region 公有方法

        #endregion

    }
}
