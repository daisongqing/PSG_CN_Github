using AwareTec.Polysmith.DataBaseCom;
using AwareTec.Polysmith.UI.FunctionControls.tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.UI.Block
{
    public partial class MarkerManagement: SkinForm
    {
        public MarkerManagement()
        {
            InitializeComponent();
            this.Load += MarkerManagement_Load;
        }

        private void MarkerManagement_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < DataModel.MarkerManage.Default.DefineMarkers.Count; i++)
            {
                pChart.IMarker def = DataModel.MarkerManage.Default.DefineMarkers[i];
                bool isStrMark = def is pChart.StringMarkers;
                abSelect.Rows.Add(def.Name, def.Description, def.BackColor, isStrMark ? (Program.Language == "EN" ? "Point Label" : "点位标签") : (Program.Language == "EN" ? "Area Label" : "区域标签"), def.HotKey, isStrMark ? "- -" : (def as pChart.RectangleMarkers).MinLimitValue.ToString());
                abSelect.Rows[i].Tag = def.Comments;
                abSelect.Rows[i].Cells[0].Tag = def.Tag;
                abSelect.Rows[i].Cells[4].Tag = isStrMark ? false : (def as pChart.RectangleMarkers).ReadOnly;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Delete:
                    DeleteButton_Click(null, null);
                    return true;//不继续处理
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            DataModel.LogInstance.Default.AddLog("用户点击 事件管理-新增 按钮");
            var edit = new MarkerEditCopy();
            edit.Text = Program.Language=="EN"?"New Event":"事件新增";
            edit.SaveMarkerEventHandle += add_SaveMarkerEventHandle;
            edit.ShowDialog();
        }

        private void add_SaveMarkerEventHandle(pChart.IMarker marker)
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                abSelect.Rows.Add(marker.Name, marker.Description, marker.BackColor, marker is pChart.StringMarkers ? (Program.Language == "EN" ? "Point Label" : "点位标签") : (Program.Language == "EN" ? "Area Label" : "区域标签"), marker.HotKey, marker is pChart.StringMarkers ? "- -" : (marker as pChart.RectangleMarkers).MinLimitValue.ToString());
                abSelect.Rows[abSelect.Rows.Count - 1].Tag = marker.Comments;
                abSelect.Rows[abSelect.Rows.Count - 1].Cells[0].Tag = marker.Tag = true;
                DataModel.MarkerManage.Default.DefineMarkers.Add(marker);
                DataModel.DataBaseHelper.Default.Insert(new Doc_EventsDefine()
                                                        {
                                                            Name = marker.Name,
                                                            Description = marker.Description,
                                                            BackColor = ColorToString(marker.BackColor),
                                                            Flag = marker is pChart.StringMarkers ? 0 : 1,
                                                            Comments = marker.Comments,
                                                            AllowDelete = true,
                                                            MarkTyp = (int)pChart.IMarker.MarkType.None,
                                                            ModeType = Channel.Default.SystemSetting.ModeType,
                                                            Reserve1 = string.Format("{0}{1}", 
                                                            marker.HotKey, 
                                                            marker is pChart.StringMarkers ? "" : string.Format("/{0}", (marker as pChart.RectangleMarkers).MinLimitValue))
                                                        });
                Channel.Default.DefineMarksChange();
                DataModel.LogInstance.Default.AddLog(string.Format("用户新增事件成功 新增的事件名称为{0}", marker.Name), pSystem.LogManagement.LogLevel.WARN);
                AhDung.MessageTip.ShowOk("新增成功!");

            }));
        }
        private string ColorToString(Color color)
        {
            string strvalue = color.ToString();
            string[] ss = strvalue.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
            if (ss.Length == 2)
            {
                if (ss[1].Contains("="))
                {
                    string[] ss2 = ss[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    strvalue = string.Format("{3:X2}{0:X2}{1:X2}{2:X2}", byte.Parse(ss2[1].Replace("R=", "")), byte.Parse(ss2[2].Replace("G=", "")), byte.Parse(ss2[3].Replace("B=", "")), byte.Parse(ss2[0].Replace("A=", "")));
                }
                else
                    strvalue = ss[1];
            }
            return strvalue;
        }
        private void edit_SaveMarkerEventHandle(pChart.IMarker marker)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                abSelect.CurrentRow.Cells[1].Value = marker.Description;
                abSelect.CurrentRow.Cells[2].Value = marker.BackColor;
                abSelect.CurrentRow.Cells[3].Value = marker is pChart.StringMarkers ? (Program.Language == "EN" ? "Point Label" : "点位标签") : (Program.Language == "EN" ? "Area Label" : "区域标签");
                abSelect.CurrentRow.Cells[4].Value = marker.HotKey;
                abSelect.CurrentRow.Cells[5].Value = marker is pChart.StringMarkers ? "- -" : (marker as pChart.RectangleMarkers).MinLimitValue.ToString();
                abSelect.CurrentRow.Tag = marker.Comments;
            }));
            pChart.IMarker find = DataModel.MarkerManage.Default.DefineMarkers.Find(t => t.Name == marker.Name);
            if (find != null)
            {
                find.Name = marker.Name;
                find.Description = marker.Description;
                find.BackColor = marker.BackColor;
                find.Comments = marker.Comments;
                find.HotKey = marker.HotKey;
                if (marker is pChart.RectangleMarkers)
                {
                    (find as pChart.RectangleMarkers).MinLimitValue = (marker as pChart.RectangleMarkers).MinLimitValue;
                }
                Channel.Default.MarkColorChange(find);
            }
            DataModel.DataBaseHelper.Default.Update(new Doc_EventsDefine() 
                                                    { 
                                                        Name = marker.Name,
                                                        ModeType = Channel.Default.SystemSetting.ModeType
                                                    }, 
                                                    new Doc_EventsDefine() 
                                                    { 
                                                        Description = marker.Description, 
                                                        BackColor = ColorToString(marker.BackColor), 
                                                        Flag = marker is pChart.StringMarkers ? 0 : 1, 
                                                        Comments = marker.Comments, Reserve1 = string.Format("{0}{1}", 
                                                        marker.HotKey, marker is pChart.StringMarkers ? "" : string.Format("/{0}", (marker as pChart.RectangleMarkers).MinLimitValue)) 
                                                    });
            Channel.Default.DefineMarksChange();
            DataModel.LogInstance.Default.AddLog(string.Format("用户编辑事件成功 编辑的事件名称为{0}",marker.Name), pSystem.LogManagement.LogLevel.WARN);
            AhDung.MessageTip.ShowOk("编辑成功!");
        }
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (abSelect.SelectedRows.Count > 0)
            {
                string[] id = new string[abSelect.SelectedRows.Count];
                int tmpidx = 0;
                foreach (DataGridViewRow item in abSelect.SelectedRows)
                {
                    if (Convert.ToBoolean(item.Cells[0].Tag))
                    {
                        id[tmpidx++] = item.Cells[0].Value.ToString();
                    }
                }
                if (tmpidx > 0)
                {
                    if (MessageForm.Show("是否删除所选择的项？", "信息确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        foreach (DataGridViewRow item in abSelect.SelectedRows)
                        {
                            if (Convert.ToBoolean(item.Cells[0].Tag))
                            {
                                abSelect.Rows.Remove(item);
                            }
                        }
                        for (int s = 0; s < tmpidx; s++)
                        {
                            for (int i = 0; i < DataModel.MarkerManage.Default.DefineMarkers.Count; i++)
                            {
                                if (DataModel.MarkerManage.Default.DefineMarkers[i].Name == id[s])
                                {
                                    DataModel.MarkerManage.Default.DefineMarkers.RemoveAt(i);
                                    DataModel.LogInstance.Default.AddLog(string.Format("用户删除事件成功 删除事件为{0}", id[s]), pSystem.LogManagement.LogLevel.WARN);
                                    DataModel.DataBaseHelper.Default.Delete(new Doc_EventsDefine() 
                                                                            { 
                                                                                Name = id[s],
                                                                                ModeType = Channel.Default.SystemSetting.ModeType
                                                                            });
                                    break;
                                }
                            }
                        }
                        Channel.Default.DefineMarksChange();
                        AhDung.MessageTip.ShowOk("删除成功!");
                    }
                }
                else
                {
                    AhDung.MessageTip.ShowWarning("选择项为不可删除项!");
                }
            }
            else
            {
                AhDung.MessageTip.ShowWarning("请选择需要删除的项!");
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (abSelect.SelectedRows.Count > 0)
            {
                if (abSelect.SelectedRows.Count == 1)
                {
                    var edit = new MarkerEditCopy();
                    bool isstringmark = abSelect.CurrentRow.Cells[3].Value.ToString() == (Program.Language == "EN" ? "Point Label" : "点位标签");
                    pChart.IMarker marker;
                    if (isstringmark)
                    {
                        marker = new pChart.StringMarkers();
                    }
                    else
                        marker = new pChart.RectangleMarkers(Convert.ToBoolean(abSelect.CurrentRow.Cells[4].Tag)) { MinLimitValue = Convert.ToSingle(abSelect.CurrentRow.Cells[5].Value) };
                    marker.BackColor = (Color)abSelect.CurrentRow.Cells[2].Value;
                    marker.Name = abSelect.CurrentRow.Cells[0].Value.ToString();
                    marker.Description = abSelect.CurrentRow.Cells[1].Value.ToString();
                    marker.Comments = abSelect.CurrentRow.Tag.ToString();
                    marker.HotKey = abSelect.CurrentRow.Cells[4].Value.ToString();
                    DataModel.LogInstance.Default.AddLog(string.Format("用户点击 编辑按钮 编辑的事件为{0}", marker.Name), pSystem.LogManagement.LogLevel.INFO);
                    edit.Text = Program.Language == "EN" ? "Event Edit" : "事件编辑";
                    edit.CurrentMarker = marker;
                    edit.SaveMarkerEventHandle += edit_SaveMarkerEventHandle;
                    edit.ShowDialog();
                }
                else
                {
                    AhDung.MessageTip.ShowWarning("只能选择一项进行编辑!");
                }
            }
            else
            {
                AhDung.MessageTip.ShowWarning("请选择需要编辑的项!");
            }
        }

        private void abSelect_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            var edit = new MarkerEditCopy();
            bool isstringmark = abSelect.Rows[e.RowIndex].Cells[3].Value.ToString() == (Program.Language == "EN" ? "Point Label" : "点位标签");
            pChart.IMarker marker;
            if (isstringmark)
            {
                marker = new pChart.StringMarkers();
            }
            else
            {
                marker = new pChart.RectangleMarkers(Convert.ToBoolean(abSelect.CurrentRow.Cells[4].Tag)) { MinLimitValue = Convert.ToSingle(abSelect.CurrentRow.Cells[5].Value) };

            }
            marker.BackColor = (Color)abSelect.Rows[e.RowIndex].Cells[2].Value;
            marker.Name = abSelect.Rows[e.RowIndex].Cells[0].Value.ToString();
            marker.Description = abSelect.Rows[e.RowIndex].Cells[1].Value.ToString();
            marker.HotKey = abSelect.Rows[e.RowIndex].Cells[4].Value.ToString();
            marker.Comments = abSelect.Rows[e.RowIndex].Tag.ToString();
            DataModel.LogInstance.Default.AddLog(string.Format("用户双击事件列表 编辑事件 编辑的事件为{0}", marker.Name), pSystem.LogManagement.LogLevel.INFO);
            edit.Text = Program.Language == "EN" ? "Event Edit" :"事件编辑";
            edit.CurrentMarker = marker;
            edit.SaveMarkerEventHandle += edit_SaveMarkerEventHandle;
            edit.ShowDialog();
        }
    }
}
