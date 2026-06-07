using AwareTec.Polysmith.DataBaseCom;
using System;
using System.Collections.Generic;
namespace AwareTec.Polysmith.DataCenter
{
    internal class DataAnalysis
    {
        /// <summary>
        /// 当前分析的edf数据文件
        /// </summary>
        private BaseFile m_CurrentEDFDataSource = null;
        private List<Doc_Channel> m_channelProperties = null;
        public void Init(BaseFile source, List<Doc_Channel> channelProperties)
        {
            m_CurrentEDFDataSource = source;
            m_channelProperties = channelProperties;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Doc_EventRecords> getEvents(List<Doc_EventRecords> subEventsReocrds)
        {
            if (m_CurrentEDFDataSource != null)
            {
                List<Doc_EventRecords> records = new List<Doc_EventRecords>();
                for (int i = 0; i < m_CurrentEDFDataSource.Channels.Count; i++)
                {
                    ChannelItem channel = m_CurrentEDFDataSource.Channels[i];
                    Doc_Channel find = m_channelProperties.Find(t => t.SignName == channel.DataSignName);
                    if (find.ID == 17)
                    {
                        //auto_spo2( channel.Data.ToArray(),
                    }
                }

                return records;
            }
            else
                return subEventsReocrds;
        }

        #region 氧减事件分析算法
        /// <summary>
        /// 自动分析出氧减事件
        /// </summary>
        /// <param name="spo2">血氧值</param>
        /// <param name="dec">氧减事件判断的下降阈值</param>
        /// <param name="miss_threshold">限制血氧最低值</param>
        /// <param name="buffer_threshold1"></param>
        /// <param name="buffer_threshold2"></param>
        /// <param name="buffer_threshold3"></param>
        /// <returns></returns>
        private string auto_spo2(float[] spo2, float dec, float miss_threshold, float buffer_threshold1 = 10, float buffer_threshold2 = 25, float buffer_threshold3 = 20)
        {
            DateTime dt = DateTime.Now;
            string res = "";
            int i = 1,
            st = 0,
            ed = 0;
            int Len = spo2.Length;
            float max_line = spo2[0],
             buffer_time_1 = 1,
             buffer_time_2 = 1,
             buffer_time_3 = 0;

            while (i < Len)
            {
                if (max_line < spo2[i])
                    max_line = spo2[i];
                //根据趋势的变化更换基线
                if (spo2[i] != max_line && spo2[i] == spo2[i - 1])
                {
                    buffer_time_1 += 1;
                    if (buffer_time_1 >= buffer_threshold1)
                    {
                        max_line = spo2[i];
                        buffer_time_1 = 1;
                    }
                }
                else
                {
                    buffer_time_1 = 1;
                }
                //当前数值小于基线考虑氧降事件开始
                if (spo2[i] < max_line)
                {
                    st = i - 1;
                    float min_line = spo2[i];
                    i += 1;

                    float edOfNotGetMaxline = 0;
                    int ed_index = 0;

                    while (i < Len && spo2[i] <= max_line)
                    {
                        // 考虑事件中的最小值
                        if (min_line > spo2[i])
                            min_line = spo2[i];
                        // 下降中值长时间保持稳定，考虑更换基线
                        if (max_line - min_line < dec && spo2[i] != max_line && spo2[i] == spo2[i - 1])
                        {
                            buffer_time_2 += 1;
                            if (buffer_time_2 >= buffer_threshold2)
                            {
                                max_line = spo2[i];
                                buffer_time_2 = 1;
                                break;
                            }
                        }
                        else
                        {
                            buffer_time_2 = 1;
                        }
                        // 更换事件起始点
                        if (spo2[i] == max_line && spo2[i] - min_line < dec)
                            st = i;
                        // 抬升大于等于规定的下降率，事件结束
                        if (spo2[i] - min_line >= dec)
                        {

                            ed = i;
                            while (i + 1 < Len && spo2[i] < spo2[i + 1])
                            {
                                ed = i + 1;
                                i += 1;
                            }
                            max_line = spo2[i];
                            if (min_line > miss_threshold)
                            {
                                res = string.Format("{2}{0},{1};", st, ed, res);
                            }
                            break;
                        }
                        // 下降大于等于下降率且长时间抬升不大于下降率，事件结束
                        if (max_line - min_line >= dec && spo2[i] > min_line)
                        {
                            buffer_time_3 += 1;
                            //确定此过程中的终点及其值，把此值作为新的基线
                            if (edOfNotGetMaxline < spo2[i])
                            {
                                edOfNotGetMaxline = spo2[i];
                                ed_index = i;
                            }

                            if (buffer_time_3 >= buffer_threshold3)
                            {
                                ed = ed_index;
                                max_line = edOfNotGetMaxline;
                                if (min_line > miss_threshold)
                                {
                                    res = string.Format("{2}{0},{1};", st, ed, res);
                                }
                                i = ed;
                                buffer_time_3 = 0;
                                break;
                            }
                            //上升大于下降率但此时数值大于基线，事件结束
                            if (i + 1 < Len && spo2[i + 1] - min_line >= dec && spo2[i + 1] > max_line)
                            {
                                i += 1;
                                ed = i;
                                max_line = spo2[i];
                                if (min_line > miss_threshold)
                                {
                                    res = string.Format("{2}{0},{1};", st, ed, res);
                                }
                                buffer_time_3 = 0;
                                break;
                            }
                        }
                        else
                            buffer_time_3 = 0;

                        i += 1;
                    }
                }
                i += 1;
            }
            Console.WriteLine(string.Format("氧减事件分析总耗时：{0}ms", (DateTime.Now - dt).TotalMilliseconds));
            return res;
        }
        #endregion
        #region 呼吸事件算法
        //获取窗口内的最大振幅
        private static float[] catch_swing(List<float> signal, int w_l)
        {
            List<float> swings = new List<float>();
            int len = signal.Count;
            float max_v = signal[0];
            float min_v = signal[0];

            for (int i = 0; i < len; i++)
            {
                if (max_v < signal[i])
                    max_v = signal[i];
                if (min_v > signal[i])
                    min_v = signal[i];
                if ((i + 1) % w_l == 0)
                {
                    swings.Add(max_v - min_v);

                    if (i + 1 < len)
                    {
                        max_v = signal[i + 1];
                        min_v = signal[i + 1];
                    }
                }
            }
            if (len % w_l != 0)
            {
                swings.Add(max_v - min_v);
            }
            return swings.ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="signal"></param>
        /// <param name="window_size"></param>
        /// <param name="fz"></param>
        /// <param name="apnea_threshold"></param>
        /// <param name="hypopnea_threshold"></param>
        /// <param name="stop_threshold"></param>
        /// <param name="change_standline_threshold"></param>
        /// <param name="event_minlen"></param>
        /// <param name="max_lim"></param>
        /// <param name="min_lim"></param>
        /// <returns></returns>
        private string auto_resp(List<float> signal, int window_size = 5, int fz = 100, float apnea_threshold = 0.15f, float hypopnea_threshold = 0.55f, int stop_threshold = 3, int change_standline_threshold = 3,
                    int event_minlen = 2, int event_maxlen = 90, float max_lim = 150, float min_lim = 5)///window_size-4.5 fz-100 apnea_threshold-0.15 hypopnea_threshold-0.55 stop_threshold-3 change_standline_threshold-3  event_len-2 max_lim-150  min_lim-5
        {
            string Apena = "A"; //# 呼吸暂停
            string Hypopnea = "H";// # 低通气

            int w_l = window_size * fz;
            float[] swing = catch_swing(signal, w_l);// # 从信号中获取的振幅信息

            int change_standline = 0;

            float standard_line = swing[0];//#初始基线去第一个值，且控制在一个范围之内
            if (standard_line < min_lim)
                standard_line = min_lim;
            if (standard_line > max_lim)
                standard_line = max_lim;

            List<string> res = new List<string>();

            int len = swing.Length;
            int i = 1;

            while (i < len)
            {
                //根据实际情况变换基线
                if (standard_line < swing[i])
                {
                    change_standline += 1;
                    if (change_standline >= change_standline_threshold)
                    {
                        standard_line = (swing[i - 2] + swing[i - 1] + swing[i]) / 3;
                        if (standard_line < min_lim)
                            standard_line = min_lim;
                        if (standard_line > max_lim)
                            standard_line = max_lim;
                        change_standline = 0;
                    }
                }
                else if (standard_line >= swing[i] && swing[i] > hypopnea_threshold * standard_line)
                    change_standline = 0;

                // 振幅小于基线乘以低通气阈值，考虑事件开始    
                else if (swing[i] <= hypopnea_threshold * standard_line)
                {
                    int risetime = 0;//振幅有明显上升的持续时间
                    int st = i * w_l;
                    int n_A = 0;
                    int n_H = 0;
                    float baseLine = hypopnea_threshold * standard_line;
                    int ed = 0;
                    //当振幅接近基线时事件结束  
                    while (i + 1 < len && (swing[i] < baseLine || swing[i + 1] < baseLine))
                    {
                        // 振幅有连续明显的上升且振幅值大于下限值，考虑事件结束
                        if (swing[i] > swing[i - 1] && swing[i] >= min_lim)
                            risetime += 1;
                        if (risetime >= stop_threshold)
                        {
                            ed = i * w_l;
                            if (n_A + n_H < event_maxlen)
                            {
                                if (n_A >= n_H)
                                    res.Add(string.Format("{0},{1},{2}", st, ed, Apena));
                                else
                                    res.Add(string.Format("{0},{1},{2}", st, ed, Hypopnea));
                            }
                            break;
                        }
                        else
                            risetime = 0;

                        if (swing[i] > apnea_threshold * standard_line)
                            n_H += 1;

                        if (swing[i] <= apnea_threshold * standard_line)
                            n_A += 1;

                        i += 1;
                    }
                    ed = i * w_l;
                    if (ed > signal.Count)
                        ed = signal.Count;

                    if (i < len)
                    {
                        standard_line = (swing[i] + swing[i - 1] + swing[i - 2]) / 3;
                        if (standard_line < min_lim)
                            standard_line = min_lim;
                        if (standard_line > max_lim)
                            standard_line = max_lim;
                    }

                    if (n_A + n_H >= event_minlen && n_A + n_H <= event_maxlen && risetime < stop_threshold)
                    {
                        if (n_A >= n_H)
                            res.Add(string.Format("{0},{1},{2}", st, ed, Apena));
                        else
                            res.Add(string.Format("{0},{1},{2}", st, ed, Hypopnea));
                    }
                    if (i == len - 1)
                        break;
                    else
                        continue;
                }
                i += 1;
            }
            return string.Join(";", res);
        }
        #endregion
    }
}
