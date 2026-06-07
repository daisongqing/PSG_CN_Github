using Newtonsoft.Json;

namespace AwareTec.Polysmith.DataMangement
{
    /// <summary>
    /// 分析配置
    /// </summary>
    public class AnalysisConfigUnit : IUnit
    {
        public class AnalysisCondition
        {
            private int m_AnalysisStateWord = 0;
            /// <summary>
            /// 分析状态字
            /// </summary>
            public int AnalysisStateWord
            {
                set
                {
                    m_AnalysisStateWord = value;
                }
                get
                {
                    return m_AnalysisStateWord;
                }
            }

            private string m_ArousalDataSourcePlan = "1";
            /// <summary>
            /// 微觉醒数据源选择字符串：其中是对应通道编码,通道之间用分号分割。
            /// </summary>
            public string ArousalDataSourcePlan
            {
                set
                {
                    m_ArousalDataSourcePlan = value;
                }
                get
                {
                    return m_ArousalDataSourcePlan;
                }
            }

            private string m_SleepStageSourcePlan = "4;23;6";
            /// <summary>
            /// 睡眠分期数据源选择字符串：其中是对应通道编码,通道之间用分号分割。
            /// </summary>
            public string SleepStageSourcePlan
            {
                set
                {
                    m_SleepStageSourcePlan = value;
                }
                get
                {
                    return m_SleepStageSourcePlan;
                }
            }
            /// <summary>
            /// 默认值 3
            /// </summary>
            private string m_OxygenReduceRange = "3";
            /// <summary>
            /// 氧减范围 2-5
            /// </summary>
            public string OxygenReduceRange
            {
                set
                {
                    m_OxygenReduceRange = value;
                }
                get
                {
                    return m_OxygenReduceRange;
                }
            }
        }
        #region 私有成员
        /// <summary>
        /// 定时任务时间间隔
        /// </summary>
        private int m_sleepTime = 3000;
        /// <summary>
        /// 自动保存的任务线程
        /// </summary>
        private System.Threading.Thread th = null;
        /// <summary>
        /// 终结任务标志位
        /// </summary>
        private bool m_KillTask = false;
        /// <summary>
        /// 是否需要更新
        /// </summary>
        private bool m_fresh = false;
        /// <summary>
        /// 队列锁
        /// </summary>
        private object m_lockObj = new object();
        /// <summary>
        /// 初始化完成标志
        /// </summary>
        private bool m_init = false;
        /// <summary>
        /// 开启任务
        /// </summary>
        private void TaskRun()
        {
            th = new System.Threading.Thread(() =>
            {
                while (!m_KillTask)
                {
                    System.Threading.Thread.Sleep(m_sleepTime);
                    if (m_fresh)
                    {
                        if (m_writer == null)
                            m_writer = CreatWriter(m_savePath);
                        m_writer.Write(System.Text.Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(this)));
                        m_fresh = false;
                    }
                }
            });
            th.IsBackground = true;
            th.SetApartmentState(System.Threading.ApartmentState.MTA);
            th.Start();
        }
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        public AnalysisConfigUnit()
        {
            FileName = "conditions.cfgs";
        }


    }
}
