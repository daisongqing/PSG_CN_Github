using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.DataCenter
{
    public class BreathEventsCreat
    {
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
        public static string auto_resp(List<float> signal, int window_size = 5, int fz = 100, float apnea_threshold = 0.15f, float hypopnea_threshold = 0.55f, int stop_threshold = 3, int change_standline_threshold = 3,
                    int event_minlen = 2, int event_maxlen=90, float max_lim = 150, float min_lim = 5)///window_size-4.5 fz-100 apnea_threshold-0.15 hypopnea_threshold-0.55 stop_threshold-3 change_standline_threshold-3  event_len-2 max_lim-150  min_lim-5
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
    }
}
