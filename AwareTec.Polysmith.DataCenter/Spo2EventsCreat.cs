using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.DataCenter
{
    public class Spo2EventsCreat
    {
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
        public static string auto_spo2(float[] spo2, float dec, float miss_threshold, float buffer_threshold1 = 10, float buffer_threshold2 = 25, float buffer_threshold3 = 20)
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
    }
}
