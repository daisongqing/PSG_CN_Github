using AwareTec.Polysmith.DataBaseCom;
using AwareTec.Polysmith.pChart;
using AwareTec.Polysmith.UI.CompatibleDbManager;
using AwareTec.Polysmith.UI.CompatibleDbManager.EventDefineTable;
using AwareTec.Polysmith.UI.EnumModel;
using AwareTec.Polysmith.UI.FunctionControls.tools;
using AwareTec.Polysmith.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.UI.DataModel
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

        public MarkerManage()
        {
            Channel.Default.ModeTypeChangedHandler += ModeTypeChangedHandler;
            ModeType mode = (ModeType)Channel.Default.SystemSetting.ModeType;   //当前模式
            BatchUpdateData(mode);

            List<Doc_EventsDefine> events = DataBaseHelper.Default.SelectAll(new Doc_EventsDefine(){ModeType = (int)mode});
            events = events.OrderByDescending(t => t.MarkTyp).ToList();///降序排列
            for (int i = 0; i < events.Count; i++)
            {
                Doc_EventsDefine def = events[i];
                int result;
                Color backcolor;
                if (int.TryParse(def.BackColor, System.Globalization.NumberStyles.HexNumber, null, out result))
                    backcolor = Color.FromArgb(result);
                else
                    backcolor = Color.FromName(def.BackColor);
                pChart.IMarker mark;
                string[] sss = def.Reserve1.Split(new char[]{ '/'});
                if (def.Flag == 0)
                {
                    mark = new pChart.StringMarkers();
                }
                else
                {
                    mark = new pChart.RectangleMarkers(def.Flag == 2) { MinLimitValue = sss.Length > 1 ? float.Parse(sss[1]) : 0 };
                    string[] ac = def.AllowChannel.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    int[] ac2 = new int[ac.Length];
                    if (ac.Length > 0)
                    {
                        for(int k=0;k<ac.Length;k++)
                        {
                            try
                            {
                                int kk = Convert.ToInt32(ac[k]);
                                ac2[k] = kk;
                            }
                            catch(Exception ee)
                            {
                                DataModel.LogInstance.Default.AddLog(string.Format("事件定义表转换出错，错误信息{0}", ee.Message), pSystem.LogManagement.LogLevel.ERROR);
                            }

                        }
                        mark.AllowChannels = ac2;
                    }
                    //mark.AllowChannels = ac.Select(delegate (string t)
                    //{
                    //    int rr = 0;
                    //    try { rr = int.Parse(t); }
                    //    catch { }
                    //    return rr;
                    //}).ToArray();         
                }
                mark.Description = def.Description;
                mark.Name = def.Name;
                mark.BackColor = backcolor;
                mark.Comments = def.Comments;
                mark.EventID = def.ID.ToString();
                mark.Tag = def.AllowDelete;
                mark.MarkTyp = (IMarker.MarkType)def.MarkTyp;
                mark.HotKey = sss.Length > 0 ? sss[0] : "";
                DefineMarkers.Add(mark);
            }
        }

        private void ModeTypeChangedHandler()
        {
            Channel.Default.ModeTypeChangedHandler -= ModeTypeChangedHandler;
            m_Default = new MarkerManage();
        }

        /// <summary>
        /// 批量矫正数据库数据
        /// </summary>
        /// <remarks>
        /// 第一次加载儿童版，需要将事件定义的非自定义事件进行复制迁移至儿童版
        /// </remarks>
        /// <returns></returns>
        private void BatchUpdateData(ModeType mode)
        {
            if (mode == ModeType.Adult)
                return;

            bool childModeIsExist = DataBaseHelper.Default.Exsit(new Doc_EventsDefine()
            {
                ModeType = (int)mode,
                AllowDelete = false
            });
            if (childModeIsExist)
                return;

            ProgressTipForm.Defalut.Text = Program.Language=="EN"? "Initialize the data for the children's version" : "初始化儿童版的数据";
            ProgressTipForm.Defalut.Argument = mode;
            ProgressTipForm.Defalut.DoWork += Defalut_DoWork;
            ProgressTipForm.Defalut.ShowDialog();
        }

        private void Defalut_DoWork(ProgressTipForm sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int progressInt = 20;

            sender.SetProgress(progressInt, Program.Language == "EN" ? "The first time loading the children's version requires data initialization, which takes approximately 20-30 seconds" : "第一次加载儿童版需要进行数据的初始化，大约耗时20-30秒...");
            var mode = (ModeType)(sender.Argument);

            //需要被复制的原数据 
            List<Doc_EventsDefine> events = DataBaseHelper.Default.SelectAll(new Doc_EventsDefine()
            {
                ModeType = (int)ModeType.Adult,
                AllowDelete = false
            });
            sender.SetProgress(progressInt += 10, Program.Language == "EN" ? "The first time loading the children's version requires data initialization, which takes approximately 20-30 seconds" : "第一次加载儿童版需要进行数据的初始化，大约耗时20-30秒...");

            var progressCount = 70 / events.Count;
            for(int i = 0; i < events.Count; i++)
            {
                var item = events[i];
                if (!ModeTypeCompatibleDbManager.UpdateModeTypeInDb(mode, item))
                {
                    e.Cancel = true;
                    sender.SetError(Program.Language == "EN" ? "Children's version data initialization failed" : "儿童版数据初始化失败");
                    Channel.Default.ProgressStauts = ProgressState.Replay;
                    throw new CompatibleDbManagerException(typeof(ModeTypeCompatibleDbManager),
                                                        typeof(Doc_EventsDefine),
                                                        mode.GetType());
                }
                sender.SetProgress(progressInt += progressCount, string.Format(Program.Language == "EN" ? "There are a total of {0} pieces of data, of which {1} have already been initialized" : "共{0}条数据，已初始化{1}条数据", events.Count,i+1));
            }

            sender.SetProgress(100, Program.Language == "EN" ? "Children's version data initialization completed" : "儿童版数据初始化完毕");
            //批量复制
            //if (!ModeTypeCompatibleDbManager.BatchUpdateModeTypeInDb(mode, events))
            //    throw new CompatibleDbManagerException(typeof(ModeTypeCompatibleDbManager),
            //                                            typeof(Doc_EventsDefine),
            //                                            mode.GetType());
        }

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
    }
}
