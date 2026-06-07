using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Util
{
    /// <summary>
    /// 睡眠过程分析
    /// </summary>
    public class SleepStudy
    {
        #region 私有成员
        #region 导入接口
#if !DEBUG
        private IntPtr m_Instance = IntPtr.Zero;
        /// <summary>
        /// 输入环境光数据信号
        /// </summary>
        /// <param name="light"></param>
        [DllImport("psg_wrapper.dll", EntryPoint = "push_light", CallingConvention = CallingConvention.StdCall)]
        public static extern void push_light(float light);
        /// <summary>
        /// 输入心电数据信号
        /// </summary>
        /// <param name="ecg_la_ra"></param>
        [DllImport("psg_wrapper.dll", EntryPoint = "push_ecg", CallingConvention = CallingConvention.StdCall)]
        public static extern void push_ecg(float ecg_la_ra);
        /// <summary>
        /// 输入6个脑电数据信号
        /// </summary>
        /// <param name="eeg_f1_m2"></param>
        /// <param name="eeg_f2_m1"></param>
        /// <param name="eeg_c3_m2"></param>
        /// <param name="eeg_c4_m1"></param>
        /// <param name="eeg_o1_m2"></param>
        /// <param name="eeg_o2_m1"></param>
        [DllImport("psg_wrapper.dll", EntryPoint = "push_eeg", CallingConvention = CallingConvention.StdCall)]
        public static extern void push_eeg(float eeg_f1_m2, float eeg_f2_m1,  float eeg_c3_m2, float eeg_c4_m1, float eeg_o1_m2, float eeg_o2_m1);
       /// <summary>
       /// 数据下颚肌电数据信号
       /// </summary>
       /// <param name="emg_pos_neg"></param>
        [DllImport("psg_wrapper.dll", EntryPoint = "push_emg", CallingConvention = CallingConvention.StdCall)]
        public static extern void push_emg(float emg_pos_neg);
        /// <summary>
        /// 输入眼动1、眼动2的数据信号
        /// </summary>
        /// <param name="eog_e1_m2"></param>
        /// <param name="eog_e2_m2"></param>
        [DllImport("psg_wrapper.dll", EntryPoint = "push_eog", CallingConvention = CallingConvention.StdCall)]
        public static extern void push_eog(float eog_e1_m2, float eog_e2_m2);
        /// <summary>
        /// 输入左腿动、右腿动数据信号
        /// </summary>
        /// <param name="leg_l"></param>
        /// <param name="leg_r"></param>
        [DllImport("psg_wrapper.dll", EntryPoint = "push_leg", CallingConvention = CallingConvention.StdCall)]
        public static extern void push_leg(float leg_l, float leg_r);
        /// <summary>
        /// 输入腹部呼吸、胸部呼吸、压力测量、热敏测量的数据信号
        /// </summary>
        /// <param name="resp_abdomen"></param>
        /// <param name="resp_chest"></param>
        /// <param name="resp_pressure"></param>
        /// <param name="resp_thermal"></param>
        [DllImport("psg_wrapper.dll", EntryPoint = "push_resp", CallingConvention = CallingConvention.StdCall)]
        public static extern void push_resp(float resp_abdomen, float resp_chest, float resp_pressure, float resp_thermal);
        /// <summary>
        /// 输入血氧数据信号
        /// </summary>
        /// <param name="sao2_finger"></param>
        [DllImport("psg_wrapper.dll", EntryPoint = "push_sao2", CallingConvention = CallingConvention.StdCall)]
        public static extern void push_sao2(float sao2_finger);
        /// <summary>
        /// 输入鼾声数据信号
        /// </summary>
        /// <param name="trachea_sound"></param>
        [DllImport("psg_wrapper.dll", EntryPoint = "push_trachea", CallingConvention = CallingConvention.StdCall)]
        public static extern void push_trachea(float trachea_sound);
        /// <summary>
        /// 信号输入完成，告知处理中心可以开始进行运算分析
        /// </summary>
        [DllImport("psg_wrapper.dll", EntryPoint = "set_end_of_signals", CallingConvention = CallingConvention.StdCall)]
        public static extern void set_end_of_signals();
        /// <summary>
        /// 是否处于分析结束状态
        /// </summary>
        /// <returns>0-未结束 1-结束</returns>
        [DllImport("psg_wrapper.dll", EntryPoint = "is_end_of_stage", CallingConvention = CallingConvention.StdCall)]
        public static extern int is_end_of_stage();
        /// <summary>
        /// 获取总帧序数
        /// </summary>
        /// <returns></returns>
        [DllImport("psg_wrapper.dll", EntryPoint = "fetch_stages_size", CallingConvention = CallingConvention.StdCall)]
        public static extern int fetch_stages_size();
        /// <summary>
        /// 返回当前输入帧序号属于哪个睡眠分期阶段
        /// </summary>
        /// <param name="idx"></param>
        /// <returns>0-觉醒 1-N1 2-N2 3-N3 4-REM</returns>
        [DllImport("psg_wrapper.dll", EntryPoint = "fetch_stages", CallingConvention = CallingConvention.StdCall)]
        public static extern int fetch_stages(int idx);
        /// <summary>
        /// 初始化
        /// </summary>
        [DllImport("psg_wrapper.dll", EntryPoint = "PSG_init", CallingConvention = CallingConvention.StdCall)]
        public static extern void PSG_init();
        /// <summary>
        /// 清空所有缓存区域
        /// </summary>
        [DllImport("psg_wrapper.dll", EntryPoint = "PSG_clear", CallingConvention = CallingConvention.StdCall)]
        public static extern void PSG_clear(); // TODO clear all buffers after processing an edf file
        /// <summary>
        /// 销毁所有占用资源，让系统进行内存回收
        /// </summary>
        [DllImport("psg_wrapper.dll", EntryPoint = "PSG_destroy", CallingConvention = CallingConvention.StdCall)]
        public static extern void PSG_destroy();
#endif
        #endregion

        /// <summary>
        /// 睡眠过程分析是否已经准备好
        /// </summary>
        private bool m_ready = false;
        private bool m_compelet = false;
        private static SleepStudy m_Default = null;
        /// <summary>
        /// 30s为一帧
        /// </summary>
        private int m_OneFrameTimeSpan = 30;
        /// <summary>
        /// 构造函数
        /// </summary>
        private SleepStudy()
        {
            Task.Factory.StartNew(() =>
            {
#if !DEBUG
                PSG_init();
#endif
                m_ready = true;
            });
        }
        #endregion
        #region 公有成员
        /// <summary>
        /// 通道数据
        /// </summary>
        public class ChannelData
        {
            /// <summary>
            /// 通道ID
            /// </summary>
            public ChannelID ID = ChannelID.All;
            /// <summary>
            /// 采样频率
            /// </summary>
            public int SampleFreq = 0;
            /// <summary>
            /// 数据
            /// </summary>
            public float[] Values=new float[0];

            public void Add()
            {
                for (int i = 0; i < SampleFreq; i++)
                {

                }
            }
        }
        /// <summary>
        /// 获取睡眠过程分析的单例
        /// </summary>
        public static SleepStudy Default
        {
            get
            {
                return m_Default ?? (m_Default = new SleepStudy());
            }
        }
        /// <summary>
        /// 睡眠数据分析完成触发事件
        /// </summary>
        /// <param name="isOk">处理结果有效还是无效</param>
        /// <param name="sleepState">存储睡眠分期的状态数组</param>
        public delegate void SleepStudyCompeletDelegate(bool isOk,int[] sleepState);
        /// <summary>
        /// 睡眠数据分析完成触发事件
        /// </summary>
        public event SleepStudyCompeletDelegate SleepStudyCompeletHandle;
        /// <summary>
        /// 开始分析
        /// </summary>
        public void Start(int totalFrameCnt,List<ChannelData> data)
        {
            m_compelet = false;
            Task th = Task.Factory.StartNew(() =>
               {
                   int[] sleepState = new int[0];
#if !DEBUG
                   ChannelData sao2 = data.Find(t=>t.ID==ChannelID.BloodOxygen);
                   for (int i = 0; i < totalFrameCnt; i++)
                   {

                       for (int s = 0; s < m_OneFrameTimeSpan; s++)
                       {
                           int cnt=s * sao2.SampleFreq * i;
                           for (int k = 0; k < sao2.SampleFreq; k++)
                           {
                               push_sao2(sao2.Values[k + cnt]);
                           }
                       }
                   }
                   set_end_of_signals();
                   while (is_end_of_stage() == 0)
                   {
                       System.Threading.Thread.Sleep(5);
                   }
                   int stage_size = fetch_stages_size();
                   if (stage_size == totalFrameCnt)
                   {
                       m_compelet = true;
                   }
                   else
                   {
                       sleepState = new int[stage_size];
                       for (int i = 0; i < stage_size; i++)
                       {
                           sleepState[i] = fetch_stages(i);
                       }
                   }
                   PSG_clear();
#endif
                   if (SleepStudyCompeletHandle != null)
                   {
                       SleepStudyCompeletHandle.BeginInvoke(m_compelet, sleepState, null, null);
                   }
               });
        }
        /// <summary>
        /// 睡眠过程分析是否已经准备好
        /// </summary>
        public bool Ready
        {
            get
            {
                return m_ready;
            }
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
#if !DEBUG
            PSG_destroy();
#endif
            m_ready = false;
            m_Default = null;
            GC.Collect();
        }
        #endregion
    }
}
