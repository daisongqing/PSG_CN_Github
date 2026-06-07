using AwareTec.Polysmith.Cloud.UI.DataModels;
using AwareTec.Polysmith.Cloud.UI.DataModels.Global;
using AwareTec.Polysmith.Cloud.UI.Forms.Tips;
using AwareTec.Polysmith.pChart;
using RestfulWebRequest.ApiCall;
using RestfulWebRequest.RestfulModel;
using RestfulWebRequest.RestfulTable.RestfulRequestTable;
using RestfulWebRequest.RestfulTable.RestfulResponseTable.BuiltInType;
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
    public partial class MarkerManagement : CloudSkinForm
    {
        #region 私有变量
        private List<ChannelTable> m_tables = new List<ChannelTable>();
        #endregion

        #region 公有变量

        #endregion

        #region 初始化 load/dispose/init

        /// <summary>
        /// 初始化
        /// </summary>
        public MarkerManagement()
        {
            InitializeComponent();
            this.Load += MarkerManagement_Load;
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MarkerManagement_Load(object sender, EventArgs e)
        {
            this.AddButton.Click += AddButton_Click;
            this.DeleteButton.Click += DeleteButton_Click;
            this.EditButton.Click += EditButton_Click;
            this.abSelect.CellDoubleClick += abSelect_CellDoubleClick;

            for (int i = 0; i < MarkerManage.Default.DefineMarkers.Count; i++)
            {
                pChart.IMarker def = MarkerManage.Default.DefineMarkers[i];
                bool isStrMark = def is pChart.StringMarkers;
                abSelect.Rows.Add(def.Name, def.Description, def.BackColor, isStrMark ? "点位标签" : "区域标签", def.HotKey, isStrMark ? "- -" : (def as pChart.RectangleMarkers).MinLimitValue.ToString());
                abSelect.Rows[i].Tag = def.Comments;
                abSelect.Rows[i].Cells[0].Tag = def.Tag;
                abSelect.Rows[i].Cells[4].Tag = isStrMark ? false : (def as pChart.RectangleMarkers).ReadOnly;
            }
            if (GlobalSingleton.Instance.User.CurrentChannelConfig != null)
                m_tables = GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelTable.Count() > 0 ? GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelTable : GlobalSingleton.Instance.User.DefaultChannelConfig.CurrentChannelTable;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 新增事件 eventhandle
        /// </summary>
        /// <param name="marker"></param>
        private void add_SaveMarkerEventHandle(pChart.IMarker marker)
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                abSelect.Rows.Add(marker.Name, marker.Description, marker.BackColor, marker is pChart.StringMarkers ? "点位标签" : "区域标签", marker.HotKey, marker is pChart.StringMarkers ? "- -" : (marker as pChart.RectangleMarkers).MinLimitValue.ToString());
                abSelect.Rows[abSelect.Rows.Count - 1].Tag = marker.Comments;
                abSelect.Rows[abSelect.Rows.Count - 1].Cells[0].Tag = marker.Tag = true;
                MarkerManage.Default.DefineMarkers.Add(marker);
                m_tables = m_tables.FindAll(t => t.Enable);
                int[] allchannels = m_tables.Select(t => t.ID).ToArray();
                bool isSuccess = ApiRequest.Instance.AddUserEvent(new AddUserEventRequestModel()
                {
                    userId = marker.ClouduserId,
                    mode = (RestfulWebRequest.EnumModels.EnumModels4Table.ModeType?)marker.Cloudmode,
                    name = marker.Name,
                    description = marker.Description,
                    isAreaLabel = (marker is pChart.RectangleMarkers),
                    markerColor = ColorToString(marker.BackColor),
                    selectedChannel = marker.Comments,
                    eventType = RestfulWebRequest.EnumModels.EnumModels4Table.EventType.None,
                    optionalChannel = IntsToString(allchannels),
                    predefinedId = Convert.ToInt32(marker.ID),
                    hotkey = marker.HotKey,
                    minTimeDomain = (marker is pChart.RectangleMarkers) ? (marker as pChart.RectangleMarkers).MinLimitValue : 0,
                    isReadOnly = marker.CloudisReadOnly,
                }, out ResponseModel responseModel);
                if (isSuccess)
                {
                    //MarkerManage.Default.DefineMarkers需要由 GlobalSingleton.Instance.User.UserEvent 进行更新
                    UserEvent def = GlobalSingleton.Instance.User.UserEvent.First(t => t.name == marker.Name);
                    pChart.IMarker mark = MarkerManage.Default.UserEventConvertToImarker(def);
                    int changeindex = MarkerManage.Default.DefineMarkers.FindIndex(t => t.Name == marker.Name);
                    MarkerManage.Default.DefineMarkers.RemoveAt(changeindex);
                    MarkerManage.Default.DefineMarkers.Insert(changeindex, mark);
                    Channel.Default.DefineMarksChange();
                    DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("用户新增事件成功 新增的事件名称为{0}", marker.Name), pSystem.LogManagement.LogLevel.WARN);
                    AhDung.MessageTip.ShowOk("新增成功!");
                }
                else
                {
                    AhDung.MessageTip.ShowError("新增失败!");
                }

            }));
        }
        /// <summary>
        /// 编辑事件 eventhandle
        /// </summary>
        /// <param name="marker"></param>
        private void edit_SaveMarkerEventHandle(pChart.IMarker marker)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                abSelect.CurrentRow.Cells[1].Value = marker.Description;
                abSelect.CurrentRow.Cells[2].Value = marker.BackColor;
                abSelect.CurrentRow.Cells[3].Value = marker is pChart.StringMarkers ? "点位标签" : "区域标签";
                abSelect.CurrentRow.Cells[4].Value = marker.HotKey;
                abSelect.CurrentRow.Cells[5].Value = marker is pChart.StringMarkers ? "- -" : (marker as pChart.RectangleMarkers).MinLimitValue.ToString();
                abSelect.CurrentRow.Tag = marker.Comments;
            }));
            pChart.IMarker find = MarkerManage.Default.DefineMarkers.Find(t => t.Name == marker.Name);
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
            bool isSuccess = ApiRequest.Instance.EditUserEvent(new EditUserEventRequest()
            {
                id = marker.Cloudid,
                userId =marker.ClouduserId,
                mode = (RestfulWebRequest.EnumModels.EnumModels4Table.ModeType?)marker.Cloudmode,
                name = marker.Name,
                description = marker.Description,
                isAreaLabel = marker is pChart.RectangleMarkers,
                markerColor = ColorToString(marker.BackColor),
                selectedChannel = marker.Comments,
                eventType = (RestfulWebRequest.EnumModels.EnumModels4Table.EventType?)marker.MarkTyp,
                optionalChannel = IntsToString(marker.AllowChannels),
                predefinedId =Convert.ToInt32(marker.ID),
                hotkey = marker.HotKey,
                minTimeDomain = (marker is pChart.RectangleMarkers) ? (marker as pChart.RectangleMarkers).MinLimitValue : 0,
                isReadOnly = marker.CloudisReadOnly,
            }, out ResponseModel responseModel);;
            if (isSuccess)
            {
                int changeindex = MarkerManage.Default.DefineMarkers.FindIndex(t => t.Name == marker.Name);
                MarkerManage.Default.DefineMarkers.RemoveAt(changeindex);
                MarkerManage.Default.DefineMarkers.Insert(changeindex, marker);
                Channel.Default.DefineMarksChange();
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("用户编辑事件成功 编辑的事件名称为{0}", marker.Name), pSystem.LogManagement.LogLevel.WARN);
                AhDung.MessageTip.ShowOk("编辑成功!");
            }
            else
            {
                AhDung.MessageTip.ShowError("编辑失败!");
            }
        }

        /// <summary>
        /// 颜色变成字符串类型
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 将allowchannel int数组类型变成string类型
        /// </summary>
        /// <param name="allowchannels"></param>
        /// <returns></returns>
        private string IntsToString(int[] allowchannels)
        {
            if (allowchannels == null || allowchannels.Length == 0)
                return "";
            StringBuilder stringBuilder = new StringBuilder();
            for(int i=0;i<allowchannels.Length;i++)
            {
                if(i>0)
                    stringBuilder.Append("/");
                stringBuilder.Append(i.ToString());
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 双击数据表的单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void abSelect_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            var edit = new MarkerEditCopy();
            bool isstringmark = abSelect.Rows[e.RowIndex].Cells[3].Value.ToString() == "点位标签";
            pChart.IMarker marker;
            if (isstringmark)
            {
                marker = new pChart.StringMarkers();
            }
            else
            {
                marker = new pChart.RectangleMarkers(Convert.ToBoolean(abSelect.CurrentRow.Cells[4].Tag)) { MinLimitValue = Convert.ToSingle(abSelect.CurrentRow.Cells[5].Value) };

            }
            marker.Name = abSelect.Rows[e.RowIndex].Cells[0].Value.ToString();
            marker = MarkerManage.Default.DefineMarkers.Find(t => t.Name == marker.Name);
            marker.BackColor = (Color)abSelect.Rows[e.RowIndex].Cells[2].Value;
            marker.Description = abSelect.Rows[e.RowIndex].Cells[1].Value.ToString();
            marker.HotKey = abSelect.Rows[e.RowIndex].Cells[4].Value.ToString();
            marker.Comments = abSelect.Rows[e.RowIndex].Tag.ToString();
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("用户双击事件列表 编辑事件 编辑的事件为{0}", marker.Name), pSystem.LogManagement.LogLevel.INFO);
            edit.Text = "事件编辑";
            edit.CurrentMarker = marker;
            edit.SaveMarkerEventHandle += edit_SaveMarkerEventHandle;
            edit.ShowDialog();
        }

        #endregion

        #region 按钮方法

        /// <summary>
        /// 事件编辑按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButton_Click(object sender, EventArgs e)
        {
            if (abSelect.SelectedRows.Count > 0)
            {
                if (abSelect.SelectedRows.Count == 1)
                {
                    var edit = new MarkerEditCopy();
                    bool isstringmark = abSelect.CurrentRow.Cells[3].Value.ToString() == "点位标签";
                    pChart.IMarker marker;
                    if (isstringmark)
                    {
                        marker = new pChart.StringMarkers();
                    }
                    else
                        marker = new pChart.RectangleMarkers(Convert.ToBoolean(abSelect.CurrentRow.Cells[4].Tag)) { MinLimitValue = Convert.ToSingle(abSelect.CurrentRow.Cells[5].Value) };
                    marker.Name = abSelect.CurrentRow.Cells[0].Value.ToString();
                    MarkerManage.Default.DefineMarkers.Find(t => t.Name == marker.Name);
                    marker.BackColor = (Color)abSelect.CurrentRow.Cells[2].Value;
                    marker.Description = abSelect.CurrentRow.Cells[1].Value.ToString();
                    marker.Comments = abSelect.CurrentRow.Tag.ToString();
                    marker.HotKey = abSelect.CurrentRow.Cells[4].Value.ToString();
                    DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("用户点击 编辑按钮 编辑的事件为{0}", marker.Name), pSystem.LogManagement.LogLevel.INFO);
                    edit.Text = "事件编辑";
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

        /// <summary>
        /// 事件删除按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        int scnt = 0;
                        for (int s = 0; s < tmpidx; s++)
                        {
                            for (int i = 0; i < MarkerManage.Default.DefineMarkers.Count; i++)
                            {
                                if (MarkerManage.Default.DefineMarkers[i].Name == id[s])
                                {
                                    bool isSuccess = ApiRequest.Instance.DeleteUserEvent(new DeleteUserEventRequestModel()
                                    {
                                        id = MarkerManage.Default.DefineMarkers[i].EventID,
                                    }, out ResponseModel responseModel);
                                    if (isSuccess)
                                    {
                                        scnt++;
                                        MarkerManage.Default.DefineMarkers.RemoveAt(i);
                                        DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("用户删除事件成功 删除的事件名称为{0}", id[s]), pSystem.LogManagement.LogLevel.WARN);
                                    }
                                    break;
                                }
                            }
                        }
                        if (scnt > 0)
                        {
                            Channel.Default.DefineMarksChange();
                            if (scnt < tmpidx)
                            {
                                AhDung.MessageTip.ShowWarning(string.Format("删除成功{0}条，未删除{1}条!", scnt, tmpidx - scnt));
                            }
                            else
                                AhDung.MessageTip.ShowOk("删除成功!");
                        }
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

        /// <summary>
        /// 事件新增按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, EventArgs e)
        {
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("用户点击 事件管理-新增 按钮");
            var edit = new MarkerEditCopy();
            edit.Text = "事件新增";
            edit.SaveMarkerEventHandle += add_SaveMarkerEventHandle;
            edit.ShowDialog();
        }

        #endregion

        #region 键盘快捷键

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

        #endregion

        #region 公有方法

        #endregion
    }
}
