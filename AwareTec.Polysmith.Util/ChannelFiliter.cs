using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Util
{
    /// <summary>
    /// 通道滤波器
    /// </summary>
    public class ChannelFiliter
    {
#if !DEBUG
        private IntPtr m_Instance = IntPtr.Zero;
        /// <summary>
        /// 创建对象
        /// </summary>
        /// <returns></returns>
        [DllImport("respiratory_rate.dll", EntryPoint = "respiratorydetector_construct", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr respiratorydetector_construct(int frequency);
        /// <summary>
        /// 计算心率
        /// </summary>
        /// <param name="resp"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [DllImport("respiratory_rate.dll", EntryPoint = "respiratorydetector_run", CallingConvention = CallingConvention.StdCall)]
        public static extern int respiratorydetector_run(IntPtr resp, int value);
        /// <summary>
        /// 释放对象
        /// </summary>
        /// <param name="resp"></param>
        [DllImport("respiratory_rate.dll", EntryPoint = "respiratorydetector_destruct", CallingConvention = CallingConvention.StdCall)]
        public static extern void respiratorydetector_destruct(IntPtr resp);
        /// <summary>
        /// 创建热敏呼吸指针
        /// </summary>
        /// <param name="resp"></param>
        [DllImport("onuse.dll", EntryPoint = "TemperatureClassifierInit", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr TemperatureClassifierInit();
        /// <summary>
        /// 创建压力指针
        /// </summary>
        /// <param name="resp"></param>
        [DllImport("onuse.dll", EntryPoint = "PressureClassifierInit", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr PressureClassifierInit();
        /// <summary>
        /// 创建腹部呼吸指针
        /// </summary>
        /// <param name="resp"></param>
        [DllImport("onuse.dll", EntryPoint = "AbdomenClassifierInit", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr AbdomenClassifierInit();
        /// <summary>
        /// 创建胸部呼吸指针
        /// </summary>
        /// <param name="resp"></param>
        [DllImport("onuse.dll", EntryPoint = "ChestClassifierInit", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr ChestClassifierInit();
        /// <summary>
        /// 判断呼吸通道是否正常 1-正常 0-不正常
        /// </summary>
        /// <param name="resp"></param>
        [DllImport("onuse.dll", EntryPoint = "classifiy", CallingConvention = CallingConvention.StdCall)]
        public static extern float classifiy(IntPtr classifier, float value);
        /// <summary>
        /// 释放指定呼吸对象资源
        /// </summary>
        /// <param name="resp"></param>
        [DllImport("onuse.dll", EntryPoint = "ClassifierDestruct", CallingConvention = CallingConvention.StdCall)]
        public static extern void ClassifierDestruct(IntPtr classifier);
        private IntPtr m_breathInstance1 = IntPtr.Zero;
        private IntPtr m_breathInstance2 = IntPtr.Zero;
        private IntPtr m_breathInstance3 = IntPtr.Zero;
        private IntPtr m_breathInstance4 = IntPtr.Zero;
#endif
        public ChannelFiliter()
        {

        }
        public ChannelFiliter(int frequency)
        {
#if !DEBUG
            m_Instance = respiratorydetector_construct(frequency);
#endif
        }

        ~ChannelFiliter()
        {
#if !DEBUG
            if (m_Instance != IntPtr.Zero)
                respiratorydetector_destruct(m_Instance);
            if (m_breathChannelType != 0)
            {
                ClassifierDestruct(m_breathChannelType == 1 ? m_breathInstance1 : m_breathChannelType == 2 ? m_breathInstance2 : m_breathChannelType == 3 ? m_breathInstance3 : m_breathInstance4);
            }
#endif
        }
        private int m_breathChannelType = 0;
        /// <summary>
        /// 呼吸类型 1-热敏  2-压力  3-腹部 4-胸部
        /// </summary>
        public int breathChannelType
        {
            set
            {
#if !DEBUG
                m_breathChannelType = value;
                switch (value)
                {
                    case 1:
                        m_breathInstance1 = TemperatureClassifierInit();
                        break;
                    case 2:
                        m_breathInstance2 = PressureClassifierInit();
                        break;
                    case 3:
                        m_breathInstance3 = AbdomenClassifierInit();
                        break;
                    case 4:
                        m_breathInstance4 = ChestClassifierInit();
                        break;
                }
#else
                m_breathChannelType = 0;
#endif
            }
        }
        /// <summary>
        /// 判断呼吸机的工作状态
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool getBreathChannelState(float value)
        {
#if !DEBUG
            switch (m_breathChannelType)
            {
                case 1:
                    return classifiy(m_breathInstance1, value) == 1;
                case 2:
                    return classifiy(m_breathInstance2, value) == 1;
                case 3:
                    return classifiy(m_breathInstance3, value) == 1;
                case 4:
                    return classifiy(m_breathInstance4, value) == 1;
                default:
                    return false;
            }
#else
            return true;
#endif
        }
        public int getHeartRate(int data)
        {
#if !DEBUG
            return respiratorydetector_run(m_Instance, data);
#else
            return 1;
#endif
        }
        /// <summary>
        /// 设置采样频率
        /// </summary>
        public int SampleFrequency
        {
            set
            {
#if !DEBUG
                m_Instance = respiratorydetector_construct(value);
#endif
                int a = value;
            }
        }
        /// <summary>
        /// 滤波器列表
        /// </summary>
        public List<RateValue> ChannelFiltersList = new List<RateValue>();
        /// <summary>
        /// 单一
        /// </summary>
        public RateValue SingleNotch { get; set; }
        /// <summary>
        /// 低通滤波 60hz
        /// </summary>
        public RateValue LowPass_60Hz { get; set; }
        /// <summary>
        /// 低通滤波 30hz
        /// </summary>
        public RateValue LowPass_30Hz { get; set; }
        /// <summary>
        /// 高通滤波 1s
        /// </summary>
        public RateValue HighPass_1S { set; get; }
        /// <summary>
        ///  高通滤波 0.3s
        /// </summary>
        public RateValue HighPass_03S { set; get; }
        /// <summary>
        /// 滤波算法
        /// </summary>
        /// <param name="y">存放数据转换后的值</param>
        /// <param name="x">源数据值</param>
        /// <param name="StartIndex"></param>
        /// <param name="RateB">滤波系数B</param>
        /// <param name="RateA">滤波系数A</param>
        /// <returns>滤波转换后的值</returns>
        public float Filiter(float[] y, float[] x, int StartIndex, double[] RateB, double[] RateA)
        {
            double upper, lower;
            upper = lower = 0;
            for (int i = 0; StartIndex - i >= 0 && i < RateB.Length; i++)
            {
                upper += RateB[i] * x[StartIndex - i];
            }
            for (int i = 1; StartIndex - i >= 0 && i < RateA.Length; i++)
            {
                lower += RateA[i] * y[StartIndex - i];
            }
            float ret = (float)(upper - lower);
            y[StartIndex] = ret;
            return ret;
        }
        /// <summary>
        /// 改进后滤波算法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="RateB"></param>
        /// <param name="RateA"></param>
        /// <returns></returns>
        public float[] Filiter(float[] x, int firstDataIndex, RateValue rv)
        {
            float[] y = new float[x.Length];
            if (rv.StartIdx != 0)
            {
                if (rv.StartIdx < firstDataIndex)
                {
                    rv.StartIdx = 0;
                }
                else
                {
                    try
                    {
                        int offset = rv.StartIdx - firstDataIndex;
                        int ss = (rv.bak_Y.Length - offset);
                        if (ss < 0)
                        {
                            rv.StartIdx = 0;
                        }
                        else
                        {
                            Array.Copy(rv.bak_Y, ss, y, 0, offset);
                            rv.StartIdx = offset;
                        }
                    }
                    catch { }
                }
            }
            for (int i = rv.StartIdx; i < y.Length; i++)
            {
                y[i] = x[i];
                y[i] = Filiter(y, x, i, rv.RateB, rv.RateA);
            }
            rv.bak_Y = new float[y.Length];
            Array.Copy(y, 0, rv.bak_Y, 0, y.Length);
            rv.StartIdx = y.Length + firstDataIndex;
            return y;
        }
        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            if (HighPass_1S != null)
                HighPass_1S.StartIdx = 0;
            if (HighPass_03S != null)
                HighPass_03S.StartIdx = 0;
            if (LowPass_30Hz != null)
                LowPass_30Hz.StartIdx = 0;
            if (LowPass_60Hz != null)
                LowPass_60Hz.StartIdx = 0;
            if (SingleNotch != null)
                SingleNotch.StartIdx = 0;
            for (int i = 0; i < ChannelFiltersList.Count; i++)
            {
                ChannelFiltersList[i].StartIdx = 0;
            }
        }
        /// <summary>
        /// 初始化系数表
        /// </summary>
        public void InitRate()
        {
            Task[] tt = new Task[5];
            tt[0] = new Task(() =>
            {
                RateValue rv = new RateValue();
                rv.RateA = ReadCSV(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Filiter\\HighPass_500_1S_a.csv"));
                rv.RateB = ReadCSV(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Filiter\\HighPass_500_1S_b.csv"));
                HighPass_1S = rv;
            });
            tt[0].Start();
            tt[1] = new Task(() =>
            {
                RateValue rv = new RateValue();
                rv.RateA = ReadCSV(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Filiter\\LowPass_500_60Hz_a.csv"));
                rv.RateB = ReadCSV(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Filiter\\LowPass_500_60Hz_b.csv"));
                LowPass_60Hz = rv;
            });
            tt[1].Start();
            tt[2] = new Task(() =>
            {
                RateValue rv = new RateValue();
                rv.RateA = ReadCSV(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Filiter\\SingleNotch_500_50Hz_a.csv"));
                rv.RateB = ReadCSV(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Filiter\\SingleNotch_500_50Hz_b.csv"));
                SingleNotch = rv;
            });
            tt[2].Start();
            tt[3] = new Task(() =>
            {
                RateValue rv = new RateValue();
                rv.RateA = ReadCSV(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Filiter\\LowPass_500_30Hz_a.csv"));
                rv.RateB = ReadCSV(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Filiter\\LowPass_500_30Hz_b.csv"));
                LowPass_30Hz = rv;
            });
            tt[3].Start();
            tt[4] = new Task(() =>
            {
                RateValue rv = new RateValue();
                rv.RateA = ReadCSV(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Filiter\\HighPass_500_0.3S_a.csv"));
                rv.RateB = ReadCSV(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Filiter\\HighPass_500_0.3S_b.csv"));
                HighPass_03S = rv;
            });
            tt[4].Start();
            Task.WaitAll(tt);
        }
        /// <summary>
        /// 自动初始化滤波器
        /// </summary>
        /// <param name="auto"></param>
        public void InitRate(bool auto)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Filiter");
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path, "*.csv");
                for (int i = 0; i < files.Length; i++)
                {
                    string[] vals = files[i].Replace(string.Format("{0}\\", path), "").Replace(".csv", "").Split('_');
                    if (vals.Length > 3)
                    {
                        string strTyp = vals[0].ToLower();
                        FilterType typ = strTyp == "lowpass" ? FilterType.LowPass : strTyp == "highpass" ? FilterType.HighPass : strTyp == "singlenotch" ? FilterType.SingleNotch : FilterType.None;
                        int span = int.Parse(vals[1]);
                        string name = vals[2];
                        RateValue find = ChannelFiltersList.Find(t => t.Name == name && t.FilterTyp == typ && t.SampleSpan == span);
                        double[] data = ReadCSV(files[i]);
                        File.Delete(files[i]);
                        if (find == null)
                        {
                            RateValue rv = new RateValue();
                            rv.Name = name;
                            rv.FilterTyp = typ;
                            rv.Checked = false;
                            rv.SampleSpan = span;
                            if (vals[3].ToLower() == "a")
                                rv.RateA = data;
                            else if (vals[3].ToLower() == "b")
                                rv.RateB = data;
                            ChannelFiltersList.Add(rv);
                        }
                        else
                        {
                            if (vals[3].ToLower() == "a")
                                find.RateA = data;
                            else if (vals[3].ToLower() == "b")
                                find.RateB = data;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 创建一个新的实例
        /// </summary>
        /// <returns></returns>
        public ChannelFiliter CreatInstance()
        {
            ChannelFiliter ret = new ChannelFiliter();
            for (int i = 0; i < this.ChannelFiltersList.Count; i++)
            {
                ret.ChannelFiltersList.Add(this.ChannelFiltersList[i].Clone());
            }
            return ret;
        }
        /// <summary>
        /// 根据匹配条件，创建一个新的实例
        /// </summary>
        /// <returns></returns>
        public ChannelFiliter CreatInstance(int span)
        {
            ChannelFiliter ret = new ChannelFiliter();
            for (int i = 0; i < this.ChannelFiltersList.Count; i++)
            {
                if (ChannelFiltersList[i].SampleSpan == span)
                    ret.ChannelFiltersList.Add(this.ChannelFiltersList[i].Clone());
            }
            return ret;
        }
        /// <summary>
        /// 读取系数文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private double[] ReadCSV(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.ASCII))
                {
                    //记录每次读取的一行记录
                    string strLine = sr.ReadLine();
                    if (!string.IsNullOrEmpty(strLine))
                    {
                        string[] aryLine = strLine.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        return aryLine.Select(t => double.Parse(t)).ToArray();
                    }
                    return new double[0];
                }
            }
        }
    }
}
