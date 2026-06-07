using AwareTec.Polysmith.Cloud.UI.DataModels.EnumModels;
using AwareTec.Polysmith.Cloud.UI.DataModels.Global;
using AwareTec.Polysmith.Cloud.UI.Forms.MainFormBlock;
using AwareTec.Polysmith.DataBaseCom;
using AwareTec.Polysmith.pChart;
using AwareTec.Polysmith.Util;
using RestfulWebRequest.EnumModels.EnumModels4Table;
using RestfulWebRequest.RestfulTable.RestfulResponseTable.BuiltInType;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.DataModels
{
    /// <summary>
    /// 标签表单数据
    /// </summary>
    public class MarkerTable
    {
        /// <summary>
        /// 标签编号
        /// </summary>
        public string ID { set; get; }
        /// <summary>
        /// 标签名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Description { set; get; }
        /// <summary>
        /// 背景色
        /// </summary>
        public Color BackColor { set; get; }
    }

    public class MarkerManage
    {
        private static MarkerManage m_Default = null;
        /// <summary>
        /// 单例模式
        /// </summary>
        public static MarkerManage Default
        {
            get
            {
                return m_Default ?? (m_Default = new MarkerManage());
            }
        }

        public void ChangeData()
        {
            DefineMarkers.Clear();
            List<UserEvent> events = new List<UserEvent>(GlobalSingleton.Instance.User.UserEvent);
            events = events.OrderByDescending(t => (int)t.eventType).ToList();///降序排列
            for (int i = 0; i < events.Count; i++)
            {
                UserEvent def = events[i];
                IMarker mark = UserEventConvertToImarker(def);
                DefineMarkers.Add(mark);
            }
        }

        private MarkerManage(){ }

        /// <summary>
        /// 初始化
        /// </summary>
        public bool Start = false;
        public List<IMarker> DefineMarkers = new List<IMarker>(); 

        /// <summary>
        /// 根据通道ID查找相应的事件标记
        /// </summary>
        /// <param name="channelID"></param>
        /// <returns></returns>
        public List<IMarker> SearchMarkers(string channelID)
        {
            if (channelID.Contains("Clone_") || channelID.Contains("Append_"))
                return new List<IMarker>();
            int id = EDF.Default.ConvertToChannelNumEx(channelID);
            return SearchMarkers(id);
        }
        /// <summary>
        /// 根据通道ID查找相应的事件标记
        /// </summary>
        /// <param name="channelID"></param>
        /// <returns></returns>
        public List<IMarker> SearchMarkers(int channelID)
        {
            List<IMarker> ret = new List<IMarker>();
            for (int i = 0; i < DefineMarkers.Count; i++)
            {
                if (DefineMarkers[i] is RectangleMarkers)
                {
                    RectangleMarkers rect = DefineMarkers[i] as RectangleMarkers;
                    if (rect.OnChannelIndexs.Contains(channelID)&&!rect.ReadOnly)
                    {
                        if (rect.OnChannelIndexs[0] == channelID)
                        {
                            ret.Insert(0, rect);
                        }
                        else
                            ret.Add(rect);
                    }
                }
            }
            return ret;
        }
        /// <summary>
        /// 获取热键集合
        /// </summary>
        /// <returns></returns>
        public List<HotKey> getHotKeys()
        {
            List<HotKey> ret = new List<HotKey>(DefineMarkers.Count);
            for (int i = 0; i < DefineMarkers.Count; i++)
            {
                HotKey key = new HotKey();
                key.MarkTyp = DefineMarkers[i].MarkTyp;
                key.NewMarkName = DefineMarkers[i].Name;
                if (DefineMarkers[i] is RectangleMarkers)
                {
                    RectangleMarkers mark = (DefineMarkers[i] as RectangleMarkers);
                    key.MinLimitRange = mark.MinLimitValue;
                    if (mark.MarkTyp == pChart.IMarker.MarkType.Hypopnea || mark.MarkTyp == pChart.IMarker.MarkType.CA || mark.MarkTyp == pChart.IMarker.MarkType.OA || mark.MarkTyp == pChart.IMarker.MarkType.MA)
                    {
                        if (mark.MinLimitValue == 0)
                            key.MinLimitRange = 5;
                    }
                    else if (mark.MarkTyp == pChart.IMarker.MarkType.OxygenReduce)
                    {
                        if (mark.MinLimitValue == 0)
                            key.MinLimitRange = 2;
                    }
                    else if (mark.MarkTyp == pChart.IMarker.MarkType.LegMove)
                    {
                        if (mark.MinLimitValue == 0)
                            key.MinLimitRange = 0.5f;
                    }
                    else if (mark.MarkTyp == pChart.IMarker.MarkType.PeriodicalBodyMove)
                    {
                        if (mark.MinLimitValue == 0)
                            key.MinLimitRange = 17f;
                    }
                    else
                    {
                        if (mark.MinLimitValue == 0)
                            key.MinLimitRange = 1;
                    }
                }
                string[] ss = DefineMarkers[i].HotKey.Split('+');
                for (int s = 0; s < ss.Length; s++)
                {
                    string value = ss[s].Trim();
                    if (value == "")
                        continue;
                    if (value == "Shift")
                        key.ShiftEnable = true;
                    else if (value == "Control" || value == "Ctrl")
                        key.ControlEnable = true;
                    else if (value == "Alt")
                        key.AltEnable = true;
                    else
                        key.KeyCode = value;
                }
                ret.Add(key);
            }
            return ret;
        }
        /// <summary>
        /// 将UserEvent 转换成Imarker
        /// </summary>
        /// <param name="def"></param>
        /// <returns></returns>
        public IMarker UserEventConvertToImarker(UserEvent def)
        {
            int result;
            Color backcolor;
            if (int.TryParse(def.markerColor, System.Globalization.NumberStyles.HexNumber, null, out result))
                backcolor = Color.FromArgb(result);
            else
                backcolor = Color.FromName(def.markerColor);
            pChart.IMarker mark;
            if (!def.isAreaLabel)
            {
                mark = new pChart.StringMarkers();
            }
            else
            {
                mark = new pChart.RectangleMarkers(def.isReadOnly) { MinLimitValue = (float)def.minTimeDomain };
                string[] ac = def.optionalChannel.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                int[] ac2 = new int[ac.Length];
                if (ac.Length > 0)
                {
                    for (int k = 0; k < ac.Length; k++)
                    {
                        try
                        {
                            int kk = Convert.ToInt32(ac[k]);
                            ac2[k] = kk;
                        }
                        catch (Exception ee)
                        {
                            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("事件定义表转换出错，错误信息{0}", ee.Message), pSystem.LogManagement.LogLevel.ERROR);
                        }

                    }
                    mark.AllowChannels = ac2;
                }
            }
            mark.Description = def.description;
            mark.Name = def.name;
            mark.BackColor = backcolor;
            mark.Comments = def.selectedChannel;
            mark.EventID = def.id;
            mark.Tag = def.predefinedId == -1;
            mark.MarkTyp = (IMarker.MarkType)def.eventType;
            mark.HotKey = def.hotkey;

            mark.ID = def.predefinedId.ToString();
            mark.ClouduserId = def.userId;
            mark.Cloudid = def.id;
            mark.Cloudmode = def.mode;
            mark.CloudisReadOnly = def.isReadOnly;

            return mark;
        }
    }
}
