using pSystem.Interface.Util;
using System;

namespace AwareTec.Polysmith.DataBaseCom
{
    /// <summary>
    /// 呼吸事件统计
    /// </summary>
    public class Doc_BreathEventResult : ITable
    {
        public Doc_BreathEventResult()
        {
            strHypopneaDegreeLevel = "未见■ 轻度□ 中度□ 重度□";
            strKidHypopneaDegreeLevel = "未见■ 轻度□ 中度□ 重度□";
            strChenBreathEventHapened = "是□  否■";
        }
        #region 私有成员
        private string m_GUID = "";
        private bool m_GUID_ini = false;
        private float m_ApneaAndHypopneaCount = 0;
        private bool m_ApneaAndHypopneaCount_ini = false;
        private float m_ApneaAndHypopneaIndex = 0;
        private bool m_ApneaAndHypopneaIndex_ini = false;
        private float m_OAHIndex = 0;
        private bool m_OAHIndex_ini = false;
        private float m_RERAIndex = 0;
        private bool m_RERAIndex_ini = false;
        private float m_RespiratoryEffortRelatedAwakens = 0;
        private bool m_RespiratoryEffortRelatedAwakens_ini = false;
        private float m_HypopneaIndex = 0;
        private bool m_HypopneaIndex_ini = false;
        private float m_ApneaTurbidIndex = 0;
        private bool m_ApneaTurbidIndex_ini = false;
        private float m_ApneaIndex = 0;
        private bool m_ApneaIndex_ini = false;
        private float m_ApneaDurationSleepPecent = 0;
        private bool m_ApneaDurationSleepPecent_ini = false;
        private float m_ApneaTotalTimes = 0;
        private bool m_ApneaTotalTimes_ini = false;
        private float m_ApneaTotalCount = 0;
        private bool m_ApneaTotalCount_ini = false;
        private float m_ApneaREMTotalCount = 0;
        private bool m_ApneaREMTotalCount_ini = false;
        private float m_ApneaNREMTotalCount = 0;
        private bool m_ApneaNREMTotalCount_ini = false;
        private float m_MaxApneaDuration = 0;
        private bool m_MaxApneaDuration_ini = false;
        private float m_ApneaAverageTimes = 0;
        private bool m_ApneaAverageTimes_ini = false;
        private float m_CentralApneaTotalTimes = 0;
        private bool m_CentralApneaTotalTimes_ini = false;
        private float m_CentralApneaTotalCount = 0;
        private bool m_CentralApneaTotalCount_ini = false;
        private float m_CentralApneaREMTotalCount = 0;
        private bool m_CentralApneaREMTotalCount_ini = false;
        private float m_CentralApneaNREMTotalCount = 0;
        private bool m_CentralApneaNREMTotalCount_ini = false;
        private float m_MaxCentralApneaDuration = 0;
        private bool m_MaxCentralApneaDuration_ini = false;
        private float m_CentralApneaAverageTimes = 0;
        private bool m_CentralApneaAverageTimes_ini = false;
        private float m_ObstructiveApneaTotalTimes = 0;
        private bool m_ObstructiveApneaTotalTimes_ini = false;
        private float m_ObstructiveApneaTotalCount = 0;
        private bool m_ObstructiveApneaTotalCount_ini = false;
        private float m_ObstructiveApneaREMTotalCount = 0;
        private bool m_ObstructiveApneaREMTotalCount_ini = false;
        private float m_ObstructiveApneaNREMTotalCount = 0;
        private bool m_ObstructiveApneaNREMTotalCount_ini = false;
        private float m_MaxObstructiveApneaDuration = 0;
        private bool m_MaxObstructiveApneaDuration_ini = false;
        private float m_ObstructiveApneaAverageTimes = 0;
        private bool m_ObstructiveApneaAverageTimes_ini = false;
        private float m_MixedApneaTotalTimes = 0;
        private bool m_MixedApneaTotalTimes_ini = false;
        private float m_MixedApneaTotalCount = 0;
        private bool m_MixedApneaTotalCount_ini = false;
        private float m_MixedApneaREMTotalCount = 0;
        private bool m_MixedApneaREMTotalCount_ini = false;
        private float m_MixedApneaNREMTotalCount = 0;
        private bool m_MixedApneaNREMTotalCount_ini = false;
        private float m_MaxMixedApneaDuration = 0;
        private bool m_MaxMixedApneaDuration_ini = false;
        private float m_MixedApneaAverageTimes = 0;
        private bool m_MixedApneaAverageTimes_ini = false;
        private float m_HypopneaTotalTimes = 0;
        private bool m_HypopneaTotalTimes_ini = false;
        private float m_HypopneaTotalCount = 0;
        private bool m_HypopneaTotalCount_ini = false;
        private float m_HypopneaREMTotalCount = 0;
        private bool m_HypopneaREMTotalCount_ini = false;
        private float m_HypopneaNREMTotalCount = 0;
        private bool m_HypopneaNREMTotalCount_ini = false;
        private float m_MaxHypopneaDuration = 0;
        private bool m_MaxHypopneaDuration_ini = false;
        private float m_HypopneaAverageTimes = 0;
        private bool m_HypopneaAverageTimes_ini = false;
        private bool m_ChenBreathEventHapened = false;
        private bool m_ChenBreathEventHapened_ini = false;
        private int m_MainApneaType = 0;
        private bool m_MainApneaType_ini = false;
        private int m_ApneaHypopneaDegreeLevel = 0;
        private bool m_ApneaHypopneaDegreeLevel_ini = false;
        private float m_ApneaWakeTotalCount = 0;
        private bool m_ApneaWakeTotalCount_ini = false;
        private float m_ObstructiveApneaWakeTotalCount = 0;
        private bool m_ObstructiveApneaWakeTotalCount_ini = false;
        private float m_CentralApneaWakeTotalCount = 0;
        private bool m_CentralApneaWakeTotalCount_ini = false;
        private float m_MixedApneaWakeTotalCount = 0;
        private bool m_MixedApneaWakeTotalCount_ini = false;
        private float m_HypopneaWakeTotalCount = 0;
        private bool m_HypopneaWakeTotalCount_ini = false;
        private string m_strDescriptionOfBreathEvent = "";
        private float m_ApneaTotalIndex = 0;
        private bool m_ApneaTotalIndex_ini = false;
        private float m_ObstructiveApneaTotalIndex = 0;
        private bool m_ObstructiveApneaTotalIndex_ini = false;
        private float m_MixApneaTotalIndex = 0;
        private bool m_MixApneaTotalIndex_ini = false;
        private float m_CenterApneaTotalIndex = 0;
        private bool m_CenterApneaTotalIndex_ini = false;
        private float m_HypopneaTotalIndex = 0;
        private bool m_HypopneaTotalIndex_ini = false;
        #endregion
        #region 公有成员
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string GUID
        {
            set
            {
                m_GUID = value;
                m_GUID_ini = true;
            }
            get
            {
                return m_GUID;
            }
        }
        /// <summary>
        /// 呼吸暂停+低通气次数（A/H）
        /// </summary>
        public float ApneaAndHypopneaCount
        {
            set
            {
                m_ApneaAndHypopneaCount = value;
                m_ApneaAndHypopneaCount_ini = true;
            }
            get
            {
                if (m_ApneaAndHypopneaCount == 0)
                    return (m_MixedApneaTotalCount + m_ObstructiveApneaTotalCount + m_CentralApneaTotalCount + m_HypopneaTotalCount);
                return m_ApneaAndHypopneaCount;
            }
        }
        /// <summary>
        /// 呼吸暂停及低通气指数即AHI
        /// </summary>
        public float ApneaAndHypopneaIndex
        {
            set
            {
                m_ApneaAndHypopneaIndex = value;
                m_ApneaAndHypopneaIndex_ini = true;

                //成人版
                if (ModeType == 0)
                {
                    if (value < 5)
                    {
                        m_ApneaHypopneaDegreeLevel = 0;
                        strHypopneaDegreeLevel = "未见■ 轻度□ 中度□ 重度□";
                    }
                    else if (value >= 5 && value < 15)
                    {
                        m_ApneaHypopneaDegreeLevel = 1;
                        strHypopneaDegreeLevel = "未见□ 轻度■ 中度□ 重度□";
                    }
                    else if (value >= 15 && value < 30)
                    {
                        m_ApneaHypopneaDegreeLevel = 2;
                        strHypopneaDegreeLevel = "未见□ 轻度□ 中度■ 重度□";
                    }
                    else if (value >= 30)
                    {
                        m_ApneaHypopneaDegreeLevel = 3;
                        strHypopneaDegreeLevel = "未见□ 轻度□ 中度□ 重度■";
                    }
                }
                //儿童版
                /*else
                {
                    if (value <= 5)
                    {
                        m_ApneaHypopneaDegreeLevel = 0;
                        strKidHypopneaDegreeLevel = "未见■ 轻度□ 中度□ 重度□";
                    }
                    else if (value > 5 && value <= 10)
                    {
                        m_ApneaHypopneaDegreeLevel = 1;
                        strKidHypopneaDegreeLevel = "未见□ 轻度■ 中度□ 重度□";
                    }
                    else if (value > 10 && value <= 20)
                    {
                        m_ApneaHypopneaDegreeLevel = 2;
                        strKidHypopneaDegreeLevel = "未见□ 轻度□ 中度■ 重度□";
                    }
                    else if (value > 20)
                    {
                        m_ApneaHypopneaDegreeLevel = 3;
                        strKidHypopneaDegreeLevel = "未见□ 轻度□ 中度□ 重度■";
                    }
                }*/              
            }
            get
            {
                return m_ApneaAndHypopneaIndex;
            }
        }
        /// <summary>
        /// 呼吸努力相关觉醒指数RERAI
        /// </summary>
        public float RERAIndex
        {
            set
            {
                m_RERAIndex = value;
                m_RERAIndex_ini = true;
            }
            get
            {
                return m_RERAIndex;
            }
        }
        /// <summary>
        /// 呼吸努力相关觉醒次数
        /// </summary>
        public float RespiratoryEffortRelatedAwakens
        {
            set
            {
                m_RespiratoryEffortRelatedAwakens = value;
                m_RespiratoryEffortRelatedAwakens_ini = true;
            }
            get
            {
                return m_RespiratoryEffortRelatedAwakens;
            }
        }
        /// <summary>
        /// 低通气指数
        /// </summary>
        public float HypopneaIndex
        {
            set
            {
                m_HypopneaIndex = value;
                m_HypopneaIndex_ini = true;
            }
            get
            {
                return m_HypopneaIndex;
            }
        }
        /// <summary>
        /// 呼吸絮乱指数 RDI
        /// </summary>
        public float ApneaTurbidIndex
        {
            set
            {
                m_ApneaTurbidIndex = value;
                m_ApneaTurbidIndex_ini = true;
            }
            get
            {
                return m_ApneaTurbidIndex;
            }
        }
        /// <summary>
        /// 呼吸暂停指数
        /// </summary>
        public float ApneaIndex
        {
            set
            {
                m_ApneaIndex = value;
                m_ApneaIndex_ini = true;
            }
            get
            {
                return m_ApneaIndex;
            }
        }

        /// <summary>
        /// 呼吸暂停总指数
        /// </summary>
        public float ApneaTotalIndex
        {
            set
            {
                m_ApneaTotalIndex = value;
                m_ApneaTotalIndex_ini = true;
            }
            get
            {
                return m_ApneaTotalIndex;
            }
        }
        /// <summary>
        /// 阻塞性呼吸暂停总指数
        /// </summary>
        public float ObstructiveApneaTotalIndex
        {
            set
            {
                m_ObstructiveApneaTotalIndex = value;
                m_ObstructiveApneaTotalIndex_ini = true;
            }
            get
            {
                return m_ObstructiveApneaTotalIndex;
            }
        }
        /// <summary>
        /// 混合性呼吸暂停总指数
        /// </summary>
        public float MixApneaTotalIndex
        {
            set
            {
                m_MixApneaTotalIndex = value;
                m_MixApneaTotalIndex_ini = true;
            }
            get
            {
                return m_MixApneaTotalIndex;
            }
        }
        /// <summary>
        /// 中枢性呼吸暂停总指数
        /// </summary>
        public float CenterApneaTotalIndex
        {
            set
            {
                m_CenterApneaTotalIndex = value;
                m_CenterApneaTotalIndex_ini = true;
            }
            get
            {
                return m_CenterApneaTotalIndex;
            }
        }
        /// <summary>
        /// 低通气呼吸暂停总指数
        /// </summary>
        public float HypopneaTotalIndex
        {
            set
            {
                m_HypopneaTotalIndex = value;
                m_HypopneaTotalIndex_ini = true;
            }
            get
            {
                return m_HypopneaTotalIndex;
            }
        }

        /// <summary>
        /// 总呼吸暂停时间与总睡眠时间占比
        /// </summary>
        public float ApneaDurationSleepPecent
        {
            set
            {
                m_ApneaDurationSleepPecent = value;
                m_ApneaDurationSleepPecent_ini = true;
            }
            get
            {
                return m_ApneaDurationSleepPecent;
            }
        }
        /// <summary>
        /// 呼吸暂停总时间
        /// </summary>
        public float ApneaTotalTimes
        {
            set
            {
                m_ApneaTotalTimes = value;
                m_ApneaTotalTimes_ini = true;
            }
            get
            {
                return m_ApneaTotalTimes;
            }
        }
        /// <summary>
        /// 呼吸暂停总次数
        /// </summary>
        public float ApneaTotalCount
        {
            set
            {
                m_ApneaTotalCount = value;
                m_ApneaTotalCount_ini = true;
            }
            get
            {
                return m_ApneaTotalCount;
            }
        }
        /// <summary>
        /// REM期间呼吸暂停总次数
        /// </summary>
        public float ApneaREMTotalCount
        {
            set
            {
                m_ApneaREMTotalCount = value;
                m_ApneaREMTotalCount_ini = true;
            }
            get
            {
                return m_ApneaREMTotalCount;
            }
        }
        /// <summary>
        /// NREM期间呼吸暂停总次数
        /// </summary>
        public float ApneaNREMTotalCount
        {
            set
            {
                m_ApneaNREMTotalCount = value;
                m_ApneaNREMTotalCount_ini = true;
            }
            get
            {
                return m_ApneaNREMTotalCount;
            }
        }
        /// <summary>
        /// 呼吸暂停最长持续时间
        /// </summary>
        public float MaxApneaDuration
        {
            set
            {
                m_MaxApneaDuration = value;
                m_MaxApneaDuration_ini = true;
            }
            get
            {
                return m_MaxApneaDuration;
            }
        }
        /// <summary>
        /// 呼吸暂停平均时间
        /// </summary>
        public float ApneaAverageTimes
        {
            set
            {
                m_ApneaAverageTimes = value;
                m_ApneaAverageTimes_ini = true;
            }
            get
            {
                return m_ApneaAverageTimes;
            }
        }
        /// <summary>
        /// 中枢性呼吸暂停总时间
        /// </summary>
        public float CentralApneaTotalTimes
        {
            set
            {
                m_CentralApneaTotalTimes = value;
                m_CentralApneaTotalTimes_ini = true;
            }
            get
            {
                return m_CentralApneaTotalTimes;
            }
        }

        /// <summary>
        /// 中枢性呼吸暂停总次数
        /// </summary>
        public float CentralApneaTotalCount
        {
            set
            {
                m_CentralApneaTotalCount = value;
                m_CentralApneaTotalCount_ini = true;
            }
            get
            {
                return m_CentralApneaTotalCount;
            }
        }
        /// <summary>
        /// REM期间中枢性呼吸暂停总次数
        /// </summary>
        public float CentralApneaREMTotalCount
        {
            set
            {
                m_CentralApneaREMTotalCount = value;
                m_CentralApneaREMTotalCount_ini = true;
            }
            get
            {
                return m_CentralApneaREMTotalCount;
            }
        }
        /// <summary>
        /// NREM期间中枢性呼吸暂停总次数
        /// </summary>
        public float CentralApneaNREMTotalCount
        {
            set
            {
                m_CentralApneaNREMTotalCount = value;
                m_CentralApneaNREMTotalCount_ini = true;
            }
            get
            {
                return m_CentralApneaNREMTotalCount;
            }
        }
        /// <summary>
        /// 中枢性呼吸暂停最长持续时间
        /// </summary>
        public float MaxCentralApneaDuration
        {
            set
            {
                m_MaxCentralApneaDuration = value;
                m_MaxCentralApneaDuration_ini = true;
            }
            get
            {
                return m_MaxCentralApneaDuration;
            }
        }
        /// <summary>
        /// 中枢性呼吸暂停平均时间
        /// </summary>
        public float CentralApneaAverageTimes
        {
            set
            {
                m_CentralApneaAverageTimes = value;
                m_CentralApneaAverageTimes_ini = true;
            }
            get
            {
                return m_CentralApneaAverageTimes;
            }
        }
        /// <summary>
        /// 阻塞性呼吸暂停总时间
        /// </summary>
        public float ObstructiveApneaTotalTimes
        {
            set
            {
                m_ObstructiveApneaTotalTimes = value;
                m_ObstructiveApneaTotalTimes_ini = true;
            }
            get
            {
                return m_ObstructiveApneaTotalTimes;
            }
        }
        /// <summary>
        /// 阻塞性呼吸暂停总次数
        /// </summary>
        public float ObstructiveApneaTotalCount
        {
            set
            {
                m_ObstructiveApneaTotalCount = value;
                m_ObstructiveApneaTotalCount_ini = true;
            }
            get
            {
                return m_ObstructiveApneaTotalCount;
            }
        }
        /// <summary>
        /// REM期间阻塞性呼吸暂停总次数
        /// </summary>
        public float ObstructiveApneaREMTotalCount
        {
            set
            {
                m_ObstructiveApneaREMTotalCount = value;
                m_ObstructiveApneaREMTotalCount_ini = true;
            }
            get
            {
                return m_ObstructiveApneaREMTotalCount;
            }
        }
        /// <summary>
        /// NREM期间阻塞性呼吸暂停总次数
        /// </summary>
        public float ObstructiveApneaNREMTotalCount
        {
            set
            {
                m_ObstructiveApneaNREMTotalCount = value;
                m_ObstructiveApneaNREMTotalCount_ini = true;
            }
            get
            {
                return m_ObstructiveApneaNREMTotalCount;
            }
        }
        /// <summary>
        /// 阻塞性呼吸暂停时最大持续时间
        /// </summary>
        public float MaxObstructiveApneaDuration
        {
            set
            {
                m_MaxObstructiveApneaDuration = value;
                m_MaxObstructiveApneaDuration_ini = true;
            }
            get
            {
                return m_MaxObstructiveApneaDuration;
            }
        }
        /// <summary>
        /// 阻塞性呼吸暂停平均时间
        /// </summary>
        public float ObstructiveApneaAverageTimes
        {
            set
            {
                m_ObstructiveApneaAverageTimes = value;
                m_ObstructiveApneaAverageTimes_ini = true;
            }
            get
            {
                return m_ObstructiveApneaAverageTimes;
            }
        }
        /// <summary>
        /// 混合型呼吸暂停总时间
        /// </summary>
        public float MixedApneaTotalTimes
        {
            set
            {
                m_MixedApneaTotalTimes = value;
                m_MixedApneaTotalTimes_ini = true;
            }
            get
            {
                return m_MixedApneaTotalTimes;
            }
        }
        /// <summary>
        /// 混合型呼吸暂停总次数
        /// </summary>
        public float MixedApneaTotalCount
        {
            set
            {
                m_MixedApneaTotalCount = value;
                m_MixedApneaTotalCount_ini = true;
            }
            get
            {
                return m_MixedApneaTotalCount;
            }
        }
        /// <summary>
        /// REM期间混合型呼吸暂停总次数
        /// </summary>
        public float MixedApneaREMTotalCount
        {
            set
            {
                m_MixedApneaREMTotalCount = value;
                m_MixedApneaREMTotalCount_ini = true;
            }
            get
            {
                return m_MixedApneaREMTotalCount;
            }
        }
        /// <summary>
        /// NREM期间混合型呼吸暂停总次数
        /// </summary>
        public float MixedApneaNREMTotalCount
        {
            set
            {
                m_MixedApneaNREMTotalCount = value;
                m_MixedApneaNREMTotalCount_ini = true;
            }
            get
            {
                return m_MixedApneaNREMTotalCount;
            }
        }
        /// <summary>
        /// 混合型呼吸暂停最大持续时间
        /// </summary>
        public float MaxMixedApneaDuration
        {
            set
            {
                m_MaxMixedApneaDuration = value;
                m_MaxMixedApneaDuration_ini = true;
            }
            get
            {
                return m_MaxMixedApneaDuration;
            }
        }
        /// <summary>
        /// 混合型呼吸暂停平均时间
        /// </summary>
        public float MixedApneaAverageTimes
        {
            set
            {
                m_MixedApneaAverageTimes = value;
                m_MixedApneaAverageTimes_ini = true;
            }
            get
            {
                return m_MixedApneaAverageTimes;
            }
        }
        /// <summary>
        /// 低通气总时间
        /// </summary>
        public float HypopneaTotalTimes
        {
            set
            {
                m_HypopneaTotalTimes = value;
                m_HypopneaTotalTimes_ini = true;
            }
            get
            {
                return m_HypopneaTotalTimes;
            }
        }
        /// <summary>
        /// 低通气总次数
        /// </summary>
        public float HypopneaTotalCount
        {
            set
            {
                m_HypopneaTotalCount = value;
                m_HypopneaTotalCount_ini = true;
            }
            get
            {
                return m_HypopneaTotalCount;
            }
        }
        /// <summary>
        /// REM期间低通气总次数
        /// </summary>
        public float HypopneaREMTotalCount
        {
            set
            {
                m_HypopneaREMTotalCount = value;
                m_HypopneaREMTotalCount_ini = true;
            }
            get
            {
                return m_HypopneaREMTotalCount;
            }
        }
        /// <summary>
        /// NREM期间低通气总次数
        /// </summary>
        public float HypopneaNREMTotalCount
        {
            set
            {
                m_HypopneaNREMTotalCount = value;
                m_HypopneaNREMTotalCount_ini = true;
            }
            get
            {
                return m_HypopneaNREMTotalCount;
            }
        }
        /// <summary>
        /// 低通气最长持续时间
        /// </summary>
        public float MaxHypopneaDuration
        {
            set
            {
                m_MaxHypopneaDuration = value;
                m_MaxHypopneaDuration_ini = true;
            }
            get
            {
                return m_MaxHypopneaDuration;
            }
        }
        /// <summary>
        /// 低通气平均时间
        /// </summary>
        public float HypopneaAverageTimes
        {
            set
            {
                m_HypopneaAverageTimes = value;
                m_HypopneaAverageTimes_ini = true;
            }
            get
            {
                return m_HypopneaAverageTimes;
            }
        }
        /// <summary>
        /// 是否有陈施氏呼吸事件发生
        /// </summary>
        public bool ChenBreathEventHapened
        {
            set
            {
                m_ChenBreathEventHapened = value;
                strChenBreathEventHapened = value ? "是■  否□" : "是□  否■";
                m_ChenBreathEventHapened_ini = true;
            }
            get
            {
                return m_ChenBreathEventHapened;
            }
        }
        /// <summary>
        /// 主要的呼吸暂停事件类型 0-阻塞型 1-中枢型  2-混合型
        /// </summary>
        public int MainApneaType
        {
            set
            {
                m_MainApneaType = value;
                //switch (value)
                //{
                //    case 0:
                //        strMainApneaType = "阻塞型■ 中枢型□ 混合型□";
                //        break;
                //    case 1:
                //        strMainApneaType = "阻塞型□ 中枢型■ 混合型□";
                //        break;
                //    case 2:
                //        strMainApneaType = "阻塞型□ 中枢型□ 混合型■";
                //        break;
                //}
                m_MainApneaType_ini = true;
            }
            get
            {
                return m_MainApneaType;
            }
        }
        /// <summary>
        /// 低通气综合症级别判定 0-正常 1-轻度 2-中度  3-重度
        /// </summary>
        public int ApneaHypopneaDegreeLevel
        {
            set
            {
                m_ApneaHypopneaDegreeLevel = value;
                m_ApneaHypopneaDegreeLevel_ini = true;
            }
            get
            {
                return m_ApneaHypopneaDegreeLevel;
            }
        }
        public float ApneaWakeTotalCount
        {
            set
            {
                m_ApneaWakeTotalCount = value;
                m_ApneaWakeTotalCount_ini = true;
            }
            get
            {
                return m_ApneaWakeTotalCount;
            }
        }
        public float ObstructiveApneaWakeTotalCount
        {
            set
            {
                m_ObstructiveApneaWakeTotalCount = value;
                m_ObstructiveApneaWakeTotalCount_ini = true;
            }
            get
            {
                return m_ObstructiveApneaWakeTotalCount;
            }
        }
        public float CentralApneaWakeTotalCount
        {
            set
            {
                m_CentralApneaWakeTotalCount = value;
                m_CentralApneaWakeTotalCount_ini = true;
            }
            get
            {
                return m_CentralApneaWakeTotalCount;
            }
        }
        public float MixedApneaWakeTotalCount
        {
            set
            {
                m_MixedApneaWakeTotalCount = value;
                m_MixedApneaWakeTotalCount_ini = true;
            }
            get
            {
                return m_MixedApneaWakeTotalCount;
            }
        }
        public float HypopneaWakeTotalCount
        {
            set
            {
                m_HypopneaWakeTotalCount = value;
                m_HypopneaWakeTotalCount_ini = true;
            }
            get
            {
                return m_HypopneaWakeTotalCount;
            }
        }
        /// <summary>
        /// 睡眠期间总体呼吸事件的描述，类似是什么，持续多长时间，伴随多少氧减
        /// </summary>
        public string strDescriptionOfBreathEvent
        {
            set { m_strDescriptionOfBreathEvent = value; }
            get { return m_strDescriptionOfBreathEvent; }
        }
        #endregion
        #region 继承成员
        public string GetInsertString()
        {
            string def = " (";
            string val = " VALUES(";
            bool Mulitcase = false;
            if (this.m_GUID_ini)
            {
                def = string.Format("{0}{2}{1}", def, "GUID", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_GUID, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ApneaAndHypopneaCount_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ApneaAndHypopneaCount", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ApneaAndHypopneaCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ApneaAndHypopneaIndex_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ApneaAndHypopneaIndex", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ApneaAndHypopneaIndex, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_RERAIndex_ini)
            {
                def = string.Format("{0}{2}{1}", def, "RERAIndex", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_RERAIndex, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_RespiratoryEffortRelatedAwakens_ini)
            {
                def = string.Format("{0}{2}{1}", def, "RespiratoryEffortRelatedAwakens", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_RespiratoryEffortRelatedAwakens, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_HypopneaIndex_ini)
            {
                def = string.Format("{0}{2}{1}", def, "HypopneaIndex", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_HypopneaIndex, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ApneaTurbidIndex_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ApneaTurbidIndex", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ApneaTurbidIndex, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ApneaIndex_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ApneaIndex", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ApneaIndex, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ApneaDurationSleepPecent_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ApneaDurationSleepPecent", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ApneaDurationSleepPecent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ApneaTotalTimes_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ApneaTotalTimes", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ApneaTotalTimes, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ApneaTotalCount_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ApneaTotalCount", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ApneaTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ApneaREMTotalCount_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ApneaREMTotalCount", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ApneaREMTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ApneaNREMTotalCount_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ApneaNREMTotalCount", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ApneaNREMTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MaxApneaDuration_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MaxApneaDuration", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_MaxApneaDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ApneaAverageTimes_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ApneaAverageTimes", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ApneaAverageTimes, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_CentralApneaTotalTimes_ini)
            {
                def = string.Format("{0}{2}{1}", def, "CentralApneaTotalTimes", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_CentralApneaTotalTimes, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_CentralApneaTotalCount_ini)
            {
                def = string.Format("{0}{2}{1}", def, "CentralApneaTotalCount", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_CentralApneaTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_CentralApneaREMTotalCount_ini)
            {
                def = string.Format("{0}{2}{1}", def, "CentralApneaREMTotalCount", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_CentralApneaREMTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_CentralApneaNREMTotalCount_ini)
            {
                def = string.Format("{0}{2}{1}", def, "CentralApneaNREMTotalCount", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_CentralApneaNREMTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MaxCentralApneaDuration_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MaxCentralApneaDuration", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_MaxCentralApneaDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_CentralApneaAverageTimes_ini)
            {
                def = string.Format("{0}{2}{1}", def, "CentralApneaAverageTimes", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_CentralApneaAverageTimes, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ObstructiveApneaTotalTimes_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ObstructiveApneaTotalTimes", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ObstructiveApneaTotalTimes, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ObstructiveApneaTotalCount_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ObstructiveApneaTotalCount", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ObstructiveApneaTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ObstructiveApneaREMTotalCount_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ObstructiveApneaREMTotalCount", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ObstructiveApneaREMTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ObstructiveApneaNREMTotalCount_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ObstructiveApneaNREMTotalCount", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ObstructiveApneaNREMTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MaxObstructiveApneaDuration_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MaxObstructiveApneaDuration", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_MaxObstructiveApneaDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ObstructiveApneaAverageTimes_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ObstructiveApneaAverageTimes", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ObstructiveApneaAverageTimes, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MixedApneaTotalTimes_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MixedApneaTotalTimes", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_MixedApneaTotalTimes, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MixedApneaTotalCount_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MixedApneaTotalCount", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_MixedApneaTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MixedApneaREMTotalCount_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MixedApneaREMTotalCount", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_MixedApneaREMTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MixedApneaNREMTotalCount_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MixedApneaNREMTotalCount", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_MixedApneaNREMTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MaxMixedApneaDuration_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MaxMixedApneaDuration", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_MaxMixedApneaDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MixedApneaAverageTimes_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MixedApneaAverageTimes", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_MixedApneaAverageTimes, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_HypopneaTotalTimes_ini)
            {
                def = string.Format("{0}{2}{1}", def, "HypopneaTotalTimes", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_HypopneaTotalTimes, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_HypopneaTotalCount_ini)
            {
                def = string.Format("{0}{2}{1}", def, "HypopneaTotalCount", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_HypopneaTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_HypopneaREMTotalCount_ini)
            {
                def = string.Format("{0}{2}{1}", def, "HypopneaREMTotalCount", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_HypopneaREMTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_HypopneaNREMTotalCount_ini)
            {
                def = string.Format("{0}{2}{1}", def, "HypopneaNREMTotalCount", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_HypopneaNREMTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MaxHypopneaDuration_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MaxHypopneaDuration", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_MaxHypopneaDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_HypopneaAverageTimes_ini)
            {
                def = string.Format("{0}{2}{1}", def, "HypopneaAverageTimes", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_HypopneaAverageTimes, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ChenBreathEventHapened_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ChenBreathEventHapened", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ChenBreathEventHapened ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MainApneaType_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MainApneaType", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_MainApneaType, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ApneaHypopneaDegreeLevel_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ApneaHypopneaDegreeLevel", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ApneaHypopneaDegreeLevel, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ApneaWakeTotalCount_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ApneaWakeTotalCount", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ApneaWakeTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ObstructiveApneaWakeTotalCount_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ObstructiveApneaWakeTotalCount", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ObstructiveApneaWakeTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_CentralApneaWakeTotalCount_ini)
            {
                def = string.Format("{0}{2}{1}", def, "CentralApneaWakeTotalCount", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_CentralApneaWakeTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MixedApneaWakeTotalCount_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MixedApneaWakeTotalCount", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_MixedApneaWakeTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_HypopneaWakeTotalCount_ini)
            {
                def = string.Format("{0}{2}{1}", def, "HypopneaWakeTotalCount", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_HypopneaWakeTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }

            if (!Mulitcase)
            {
                return ("");
            }
            return string.Format("{0})\r\n{1})", def, val);
        }
        public string GetSelectString()
        {
            bool Mulitcase = false;
            string returnstr = " ";
            if (m_GUID_ini)
            {
                returnstr = string.Format("{0}GUID='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_GUID);
                Mulitcase = true;
            }
            if (m_ApneaAndHypopneaCount_ini)
            {
                returnstr = string.Format("{0}ApneaAndHypopneaCount={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ApneaAndHypopneaCount);
                Mulitcase = true;
            }
            if (m_ApneaAndHypopneaIndex_ini)
            {
                returnstr = string.Format("{0}ApneaAndHypopneaIndex={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ApneaAndHypopneaIndex);
                Mulitcase = true;
            }
            if (m_RERAIndex_ini)
            {
                returnstr = string.Format("{0}RERAIndex={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_RERAIndex);
                Mulitcase = true;
            }
            if (m_RespiratoryEffortRelatedAwakens_ini)
            {
                returnstr = string.Format("{0}RespiratoryEffortRelatedAwakens={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_RespiratoryEffortRelatedAwakens);
                Mulitcase = true;
            }
            if (m_HypopneaIndex_ini)
            {
                returnstr = string.Format("{0}HypopneaIndex={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_HypopneaIndex);
                Mulitcase = true;
            }
            if (m_ApneaTurbidIndex_ini)
            {
                returnstr = string.Format("{0}ApneaTurbidIndex={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ApneaTurbidIndex);
                Mulitcase = true;
            }
            if (m_ApneaIndex_ini)
            {
                returnstr = string.Format("{0}ApneaIndex={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ApneaIndex);
                Mulitcase = true;
            }
            if (m_ApneaDurationSleepPecent_ini)
            {
                returnstr = string.Format("{0}ApneaDurationSleepPecent={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ApneaDurationSleepPecent);
                Mulitcase = true;
            }
            if (m_ApneaTotalTimes_ini)
            {
                returnstr = string.Format("{0}ApneaTotalTimes={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ApneaTotalTimes);
                Mulitcase = true;
            }
            if (m_ApneaTotalCount_ini)
            {
                returnstr = string.Format("{0}ApneaTotalCount={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ApneaTotalCount);
                Mulitcase = true;
            }
            if (m_ApneaREMTotalCount_ini)
            {
                returnstr = string.Format("{0}ApneaREMTotalCount={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ApneaREMTotalCount);
                Mulitcase = true;
            }
            if (m_ApneaNREMTotalCount_ini)
            {
                returnstr = string.Format("{0}ApneaNREMTotalCount={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ApneaNREMTotalCount);
                Mulitcase = true;
            }
            if (m_MaxApneaDuration_ini)
            {
                returnstr = string.Format("{0}MaxApneaDuration={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MaxApneaDuration);
                Mulitcase = true;
            }
            if (m_ApneaAverageTimes_ini)
            {
                returnstr = string.Format("{0}ApneaAverageTimes={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ApneaAverageTimes);
                Mulitcase = true;
            }
            if (m_CentralApneaTotalTimes_ini)
            {
                returnstr = string.Format("{0}CentralApneaTotalTimes={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_CentralApneaTotalTimes);
                Mulitcase = true;
            }
            if (m_CentralApneaTotalCount_ini)
            {
                returnstr = string.Format("{0}CentralApneaTotalCount={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_CentralApneaTotalCount);
                Mulitcase = true;
            }
            if (m_CentralApneaREMTotalCount_ini)
            {
                returnstr = string.Format("{0}CentralApneaREMTotalCount={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_CentralApneaREMTotalCount);
                Mulitcase = true;
            }
            if (m_CentralApneaNREMTotalCount_ini)
            {
                returnstr = string.Format("{0}CentralApneaNREMTotalCount={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_CentralApneaNREMTotalCount);
                Mulitcase = true;
            }
            if (m_MaxCentralApneaDuration_ini)
            {
                returnstr = string.Format("{0}MaxCentralApneaDuration={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MaxCentralApneaDuration);
                Mulitcase = true;
            }
            if (m_CentralApneaAverageTimes_ini)
            {
                returnstr = string.Format("{0}CentralApneaAverageTimes={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_CentralApneaAverageTimes);
                Mulitcase = true;
            }
            if (m_ObstructiveApneaTotalTimes_ini)
            {
                returnstr = string.Format("{0}ObstructiveApneaTotalTimes={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ObstructiveApneaTotalTimes);
                Mulitcase = true;
            }
            if (m_ObstructiveApneaTotalCount_ini)
            {
                returnstr = string.Format("{0}ObstructiveApneaTotalCount={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ObstructiveApneaTotalCount);
                Mulitcase = true;
            }
            if (m_ObstructiveApneaREMTotalCount_ini)
            {
                returnstr = string.Format("{0}ObstructiveApneaREMTotalCount={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ObstructiveApneaREMTotalCount);
                Mulitcase = true;
            }
            if (m_ObstructiveApneaNREMTotalCount_ini)
            {
                returnstr = string.Format("{0}ObstructiveApneaNREMTotalCount={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ObstructiveApneaNREMTotalCount);
                Mulitcase = true;
            }
            if (m_MaxObstructiveApneaDuration_ini)
            {
                returnstr = string.Format("{0}MaxObstructiveApneaDuration={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MaxObstructiveApneaDuration);
                Mulitcase = true;
            }
            if (m_ObstructiveApneaAverageTimes_ini)
            {
                returnstr = string.Format("{0}ObstructiveApneaAverageTimes={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ObstructiveApneaAverageTimes);
                Mulitcase = true;
            }
            if (m_MixedApneaTotalTimes_ini)
            {
                returnstr = string.Format("{0}MixedApneaTotalTimes={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MixedApneaTotalTimes);
                Mulitcase = true;
            }
            if (m_MixedApneaTotalCount_ini)
            {
                returnstr = string.Format("{0}MixedApneaTotalCount={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MixedApneaTotalCount);
                Mulitcase = true;
            }
            if (m_MixedApneaREMTotalCount_ini)
            {
                returnstr = string.Format("{0}MixedApneaREMTotalCount={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MixedApneaREMTotalCount);
                Mulitcase = true;
            }
            if (m_MixedApneaNREMTotalCount_ini)
            {
                returnstr = string.Format("{0}MixedApneaNREMTotalCount={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MixedApneaNREMTotalCount);
                Mulitcase = true;
            }
            if (m_MaxMixedApneaDuration_ini)
            {
                returnstr = string.Format("{0}MaxMixedApneaDuration={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MaxMixedApneaDuration);
                Mulitcase = true;
            }
            if (m_MixedApneaAverageTimes_ini)
            {
                returnstr = string.Format("{0}MixedApneaAverageTimes={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MixedApneaAverageTimes);
                Mulitcase = true;
            }
            if (m_HypopneaTotalTimes_ini)
            {
                returnstr = string.Format("{0}HypopneaTotalTimes={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_HypopneaTotalTimes);
                Mulitcase = true;
            }
            if (m_HypopneaTotalCount_ini)
            {
                returnstr = string.Format("{0}HypopneaTotalCount={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_HypopneaTotalCount);
                Mulitcase = true;
            }
            if (m_HypopneaREMTotalCount_ini)
            {
                returnstr = string.Format("{0}HypopneaREMTotalCount={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_HypopneaREMTotalCount);
                Mulitcase = true;
            }
            if (m_HypopneaNREMTotalCount_ini)
            {
                returnstr = string.Format("{0}HypopneaNREMTotalCount={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_HypopneaNREMTotalCount);
                Mulitcase = true;
            }
            if (m_MaxHypopneaDuration_ini)
            {
                returnstr = string.Format("{0}MaxHypopneaDuration={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MaxHypopneaDuration);
                Mulitcase = true;
            }
            if (m_HypopneaAverageTimes_ini)
            {
                returnstr = string.Format("{0}HypopneaAverageTimes={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_HypopneaAverageTimes);
                Mulitcase = true;
            }
            if (m_ChenBreathEventHapened_ini)
            {
                returnstr = string.Format("{0}ChenBreathEventHapened={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ChenBreathEventHapened ? 1 : 0);
                Mulitcase = true;
            }
            if (m_MainApneaType_ini)
            {
                returnstr = string.Format("{0}MainApneaType={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MainApneaType);
                Mulitcase = true;
            }
            if (m_ApneaHypopneaDegreeLevel_ini)
            {
                returnstr = string.Format("{0}ApneaHypopneaDegreeLevel={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ApneaHypopneaDegreeLevel);
                Mulitcase = true;
            }
            if (m_ApneaWakeTotalCount_ini)
            {
                returnstr = string.Format("{0}ApneaWakeTotalCount={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ApneaWakeTotalCount);
                Mulitcase = true;
            }
            if (m_ObstructiveApneaWakeTotalCount_ini)
            {
                returnstr = string.Format("{0}ObstructiveApneaWakeTotalCount={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ObstructiveApneaWakeTotalCount);
                Mulitcase = true;
            }
            if (m_CentralApneaWakeTotalCount_ini)
            {
                returnstr = string.Format("{0}CentralApneaWakeTotalCount={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_CentralApneaWakeTotalCount);
                Mulitcase = true;
            }
            if (m_MixedApneaWakeTotalCount_ini)
            {
                returnstr = string.Format("{0}MixedApneaWakeTotalCount={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MixedApneaWakeTotalCount);
                Mulitcase = true;
            }
            if (m_HypopneaWakeTotalCount_ini)
            {
                returnstr = string.Format("{0}HypopneaWakeTotalCount={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_HypopneaWakeTotalCount);
                Mulitcase = true;
            }
            if (!Mulitcase)
            {
                return ("");
            }
            return (returnstr);
        }
        public string GetUpdateString()
        {
            string def = "SET ";
            bool Mulitcase = false;
            if (m_GUID_ini)
            {
                def = string.Format("{0}{2}GUID='{1}'", def, m_GUID, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ApneaAndHypopneaCount_ini)
            {
                def = string.Format("{0}{2}ApneaAndHypopneaCount={1}", def, m_ApneaAndHypopneaCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ApneaAndHypopneaIndex_ini)
            {
                def = string.Format("{0}{2}ApneaAndHypopneaIndex={1}", def, m_ApneaAndHypopneaIndex, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_RERAIndex_ini)
            {
                def = string.Format("{0}{2}RERAIndex={1}", def, m_RERAIndex, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_RespiratoryEffortRelatedAwakens_ini)
            {
                def = string.Format("{0}{2}RespiratoryEffortRelatedAwakens={1}", def, m_RespiratoryEffortRelatedAwakens, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_HypopneaIndex_ini)
            {
                def = string.Format("{0}{2}HypopneaIndex={1}", def, m_HypopneaIndex, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ApneaTurbidIndex_ini)
            {
                def = string.Format("{0}{2}ApneaTurbidIndex={1}", def, m_ApneaTurbidIndex, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ApneaIndex_ini)
            {
                def = string.Format("{0}{2}ApneaIndex={1}", def, m_ApneaIndex, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ApneaDurationSleepPecent_ini)
            {
                def = string.Format("{0}{2}ApneaDurationSleepPecent={1}", def, m_ApneaDurationSleepPecent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ApneaTotalTimes_ini)
            {
                def = string.Format("{0}{2}ApneaTotalTimes={1}", def, m_ApneaTotalTimes, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ApneaTotalCount_ini)
            {
                def = string.Format("{0}{2}ApneaTotalCount={1}", def, m_ApneaTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ApneaREMTotalCount_ini)
            {
                def = string.Format("{0}{2}ApneaREMTotalCount={1}", def, m_ApneaREMTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ApneaNREMTotalCount_ini)
            {
                def = string.Format("{0}{2}ApneaNREMTotalCount={1}", def, m_ApneaNREMTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MaxApneaDuration_ini)
            {
                def = string.Format("{0}{2}MaxApneaDuration={1}", def, m_MaxApneaDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ApneaAverageTimes_ini)
            {
                def = string.Format("{0}{2}ApneaAverageTimes={1}", def, m_ApneaAverageTimes, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_CentralApneaTotalTimes_ini)
            {
                def = string.Format("{0}{2}CentralApneaTotalTimes={1}", def, m_CentralApneaTotalTimes, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_CentralApneaTotalCount_ini)
            {
                def = string.Format("{0}{2}CentralApneaTotalCount={1}", def, m_CentralApneaTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_CentralApneaREMTotalCount_ini)
            {
                def = string.Format("{0}{2}CentralApneaREMTotalCount={1}", def, m_CentralApneaREMTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_CentralApneaNREMTotalCount_ini)
            {
                def = string.Format("{0}{2}CentralApneaNREMTotalCount={1}", def, m_CentralApneaNREMTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MaxCentralApneaDuration_ini)
            {
                def = string.Format("{0}{2}MaxCentralApneaDuration={1}", def, m_MaxCentralApneaDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_CentralApneaAverageTimes_ini)
            {
                def = string.Format("{0}{2}CentralApneaAverageTimes={1}", def, m_CentralApneaAverageTimes, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ObstructiveApneaTotalTimes_ini)
            {
                def = string.Format("{0}{2}ObstructiveApneaTotalTimes={1}", def, m_ObstructiveApneaTotalTimes, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ObstructiveApneaTotalCount_ini)
            {
                def = string.Format("{0}{2}ObstructiveApneaTotalCount={1}", def, m_ObstructiveApneaTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ObstructiveApneaREMTotalCount_ini)
            {
                def = string.Format("{0}{2}ObstructiveApneaREMTotalCount={1}", def, m_ObstructiveApneaREMTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ObstructiveApneaNREMTotalCount_ini)
            {
                def = string.Format("{0}{2}ObstructiveApneaNREMTotalCount={1}", def, m_ObstructiveApneaNREMTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MaxObstructiveApneaDuration_ini)
            {
                def = string.Format("{0}{2}MaxObstructiveApneaDuration={1}", def, m_MaxObstructiveApneaDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ObstructiveApneaAverageTimes_ini)
            {
                def = string.Format("{0}{2}ObstructiveApneaAverageTimes={1}", def, m_ObstructiveApneaAverageTimes, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MixedApneaTotalTimes_ini)
            {
                def = string.Format("{0}{2}MixedApneaTotalTimes={1}", def, m_MixedApneaTotalTimes, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MixedApneaTotalCount_ini)
            {
                def = string.Format("{0}{2}MixedApneaTotalCount={1}", def, m_MixedApneaTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MixedApneaREMTotalCount_ini)
            {
                def = string.Format("{0}{2}MixedApneaREMTotalCount={1}", def, m_MixedApneaREMTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MixedApneaNREMTotalCount_ini)
            {
                def = string.Format("{0}{2}MixedApneaNREMTotalCount={1}", def, m_MixedApneaNREMTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MaxMixedApneaDuration_ini)
            {
                def = string.Format("{0}{2}MaxMixedApneaDuration={1}", def, m_MaxMixedApneaDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MixedApneaAverageTimes_ini)
            {
                def = string.Format("{0}{2}MixedApneaAverageTimes={1}", def, m_MixedApneaAverageTimes, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_HypopneaTotalTimes_ini)
            {
                def = string.Format("{0}{2}HypopneaTotalTimes={1}", def, m_HypopneaTotalTimes, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_HypopneaTotalCount_ini)
            {
                def = string.Format("{0}{2}HypopneaTotalCount={1}", def, m_HypopneaTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_HypopneaREMTotalCount_ini)
            {
                def = string.Format("{0}{2}HypopneaREMTotalCount={1}", def, m_HypopneaREMTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_HypopneaNREMTotalCount_ini)
            {
                def = string.Format("{0}{2}HypopneaNREMTotalCount={1}", def, m_HypopneaNREMTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MaxHypopneaDuration_ini)
            {
                def = string.Format("{0}{2}MaxHypopneaDuration={1}", def, m_MaxHypopneaDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_HypopneaAverageTimes_ini)
            {
                def = string.Format("{0}{2}HypopneaAverageTimes={1}", def, m_HypopneaAverageTimes, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ChenBreathEventHapened_ini)
            {
                def = string.Format("{0}{2}ChenBreathEventHapened={1}", def, m_ChenBreathEventHapened ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MainApneaType_ini)
            {
                def = string.Format("{0}{2}MainApneaType={1}", def, m_MainApneaType, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ApneaHypopneaDegreeLevel_ini)
            {
                def = string.Format("{0}{2}ApneaHypopneaDegreeLevel={1}", def, m_ApneaHypopneaDegreeLevel, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ApneaWakeTotalCount_ini)
            {
                def = string.Format("{0}{2}ApneaWakeTotalCount={1}", def, m_ApneaWakeTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ObstructiveApneaWakeTotalCount_ini)
            {
                def = string.Format("{0}{2}ObstructiveApneaWakeTotalCount={1}", def, m_ObstructiveApneaWakeTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_CentralApneaWakeTotalCount_ini)
            {
                def = string.Format("{0}{2}CentralApneaWakeTotalCount={1}", def, m_CentralApneaWakeTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MixedApneaWakeTotalCount_ini)
            {
                def = string.Format("{0}{2}MixedApneaWakeTotalCount={1}", def, m_MixedApneaWakeTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_HypopneaWakeTotalCount_ini)
            {
                def = string.Format("{0}{2}HypopneaWakeTotalCount={1}", def, m_HypopneaWakeTotalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (!Mulitcase)
            {
                return ("");
            }
            return (def);
        }
        public string GetThisTableName()
        {
            return ("BreathEventResult");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "GUID":
                    GUID = Value.ToString(); ;
                    break;
                case "ApneaAndHypopneaCount":
                    ApneaAndHypopneaCount = Convert.ToSingle(Value);
                    break;
                case "ApneaAndHypopneaIndex":
                    ApneaAndHypopneaIndex = Convert.ToSingle(Value);
                    break;
                case "RERAIndex":
                    RERAIndex = Convert.ToSingle(Value);
                    break;
                case "RespiratoryEffortRelatedAwakens":
                    RespiratoryEffortRelatedAwakens = Convert.ToSingle(Value);
                    break;
                case "HypopneaIndex":
                    HypopneaIndex = Convert.ToSingle(Value);
                    break;
                case "ApneaTurbidIndex":
                    ApneaTurbidIndex = Convert.ToSingle(Value);
                    break;
                case "ApneaIndex":
                    ApneaIndex = Convert.ToSingle(Value);
                    break;
                case "ApneaTotalIndex":
                    ApneaTotalIndex = Convert.ToSingle(Value);
                    break;
                case "ObstructiveApneaTotalIndex":
                    ObstructiveApneaTotalIndex = Convert.ToSingle(Value);
                    break;
                case "MixApneaTotalIndex":
                    MixApneaTotalIndex = Convert.ToSingle(Value);
                    break;
                case "CenterApneaTotalIndex":
                    CenterApneaTotalIndex = Convert.ToSingle(Value);
                    break;
                case "HypopneaTotalIndex":
                    HypopneaTotalIndex = Convert.ToSingle(Value);
                    break;
                case "ApneaDurationSleepPecent":
                    ApneaDurationSleepPecent = Convert.ToSingle(Value);
                    break;
                case "ApneaTotalTimes":
                    ApneaTotalTimes = Convert.ToSingle(Value);
                    break;
                case "ApneaTotalCount":
                    ApneaTotalCount = Convert.ToSingle(Value);
                    break;
                case "ApneaREMTotalCount":
                    ApneaREMTotalCount = Convert.ToSingle(Value);
                    break;
                case "ApneaNREMTotalCount":
                    ApneaNREMTotalCount = Convert.ToSingle(Value);
                    break;
                case "MaxApneaDuration":
                    MaxApneaDuration = Convert.ToSingle(Value);
                    break;
                case "ApneaAverageTimes":
                    ApneaAverageTimes = Convert.ToSingle(Value);
                    break;
                case "CentralApneaTotalTimes":
                    CentralApneaTotalTimes = Convert.ToSingle(Value);
                    break;
                case "CentralApneaTotalCount":
                    CentralApneaTotalCount = Convert.ToSingle(Value);
                    break;
                case "CentralApneaREMTotalCount":
                    CentralApneaREMTotalCount = Convert.ToSingle(Value);
                    break;
                case "CentralApneaNREMTotalCount":
                    CentralApneaNREMTotalCount = Convert.ToSingle(Value);
                    break;
                case "MaxCentralApneaDuration":
                    MaxCentralApneaDuration = Convert.ToSingle(Value);
                    break;
                case "CentralApneaAverageTimes":
                    CentralApneaAverageTimes = Convert.ToSingle(Value);
                    break;
                case "ObstructiveApneaTotalTimes":
                    ObstructiveApneaTotalTimes = Convert.ToSingle(Value);
                    break;
                case "ObstructiveApneaTotalCount":
                    ObstructiveApneaTotalCount = Convert.ToSingle(Value);
                    break;
                case "ObstructiveApneaREMTotalCount":
                    ObstructiveApneaREMTotalCount = Convert.ToSingle(Value);
                    break;
                case "ObstructiveApneaNREMTotalCount":
                    ObstructiveApneaNREMTotalCount = Convert.ToSingle(Value);
                    break;
                case "MaxObstructiveApneaDuration":
                    MaxObstructiveApneaDuration = Convert.ToSingle(Value);
                    break;
                case "ObstructiveApneaAverageTimes":
                    ObstructiveApneaAverageTimes = Convert.ToSingle(Value);
                    break;
                case "MixedApneaTotalTimes":
                    MixedApneaTotalTimes = Convert.ToSingle(Value);
                    break;
                case "MixedApneaTotalCount":
                    MixedApneaTotalCount = Convert.ToSingle(Value);
                    break;
                case "MixedApneaREMTotalCount":
                    MixedApneaREMTotalCount = Convert.ToSingle(Value);
                    break;
                case "MixedApneaNREMTotalCount":
                    MixedApneaNREMTotalCount = Convert.ToSingle(Value);
                    break;
                case "MaxMixedApneaDuration":
                    MaxMixedApneaDuration = Convert.ToSingle(Value);
                    break;
                case "MixedApneaAverageTimes":
                    MixedApneaAverageTimes = Convert.ToSingle(Value);
                    break;
                case "HypopneaTotalTimes":
                    HypopneaTotalTimes = Convert.ToSingle(Value);
                    break;
                case "HypopneaTotalCount":
                    HypopneaTotalCount = Convert.ToSingle(Value);
                    break;
                case "HypopneaREMTotalCount":
                    HypopneaREMTotalCount = Convert.ToSingle(Value);
                    break;
                case "HypopneaNREMTotalCount":
                    HypopneaNREMTotalCount = Convert.ToSingle(Value);
                    break;
                case "MaxHypopneaDuration":
                    MaxHypopneaDuration = Convert.ToSingle(Value);
                    break;
                case "HypopneaAverageTimes":
                    HypopneaAverageTimes = Convert.ToSingle(Value);
                    break;
                case "ChenBreathEventHapened":
                    ChenBreathEventHapened = Convert.ToBoolean(Value);
                    break;
                case "MainApneaType":
                    MainApneaType = Convert.ToInt32(Value);
                    break;
                case "ApneaHypopneaDegreeLevel":
                    ApneaHypopneaDegreeLevel = Convert.ToInt32(Value);
                    break;
                case "ApneaWakeTotalCount":
                    ApneaWakeTotalCount = Convert.ToSingle(Value);
                    break;
                case "ObstructiveApneaWakeTotalCount":
                    ObstructiveApneaWakeTotalCount = Convert.ToSingle(Value);
                    break;
                case "CentralApneaWakeTotalCount":
                    CentralApneaWakeTotalCount = Convert.ToSingle(Value);
                    break;
                case "MixedApneaWakeTotalCount":
                    MixedApneaWakeTotalCount = Convert.ToSingle(Value);
                    break;
                case "HypopneaWakeTotalCount":
                    HypopneaWakeTotalCount = Convert.ToSingle(Value);
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_BreathEventResult());
        }
        #endregion
        /// <summary>
        /// 发生陈施氏呼吸事件情况描述
        /// </summary>
        public string strChenBreathEventHapened { private set; get; }
        /// <summary>
        /// 低通气综合症情况描述
        /// </summary>
        public string strHypopneaDegreeLevel { private set; get; }
        /// <summary>
        /// 儿童低通气综合症情况描述
        /// </summary>
        public string strKidHypopneaDegreeLevel { private set; get; }

        /// <summary>
        /// 鼻压力通道数据展示
        /// </summary>
        public string strPressureMuzzleFlow { set; get; }

        /// <summary>
        /// 低通气综合症情况描述(string 类型，没有□)
        /// </summary>
        private string m_strMainApneaType = "阻塞型□ 中枢型□ 混合型□";
        /// <summary>
        /// 主要的呼吸暂停事件类型描述
        /// </summary>
        public string strMainApneaType
        {
            private set
            {
                m_strMainApneaType = value;
            }
            get
            {
                if (m_ApneaHypopneaDegreeLevel == 0)
                    return "阻塞型□ 中枢型□ 混合型□";

                //成人版
                if (ModeType == 0)
                {
                    if (m_MixedApneaTotalCount > m_ObstructiveApneaTotalCount && MixedApneaTotalCount > m_CentralApneaTotalCount)
                    {
                        m_strMainApneaType = "阻塞型□ 中枢型□ 混合型■";
                    }
                    else if (m_ObstructiveApneaTotalCount > MixedApneaTotalCount && m_ObstructiveApneaTotalCount > m_CentralApneaTotalCount)
                    {
                        m_strMainApneaType = "阻塞型■ 中枢型□ 混合型□";
                    }
                    else if (m_CentralApneaTotalCount > MixedApneaTotalCount && m_ObstructiveApneaTotalCount < m_CentralApneaTotalCount)
                    {
                        m_strMainApneaType = "阻塞型□ 中枢型■ 混合型□";
                    }
                }
                //儿童版
                else
                {
                    m_strMainApneaType = "阻塞型■ 中枢型□ 混合型□";
                }
                
                return m_strMainApneaType;
            }
        }
        /// <summary>
        /// 呼吸努力相关觉醒总次数
        /// </summary>
        public int RERATotalCount { set; get; }
        /// <summary>
        /// Nrem期间呼吸努力相关觉醒总次数
        /// </summary>
        public int RERAInNREMTotalCount { set; get; }
        /// <summary>
        /// Nrem期间呼吸努力相关觉醒总次数
        /// </summary>
        public float RERAInNREMTotalTimes { set; get; }
        /// <summary>
        /// Nrem期间呼吸努力相关觉醒指数
        /// </summary>
        public float NremRERAIndex { set; get; }
        /// <summary>
        /// REM期间的呼吸努力相关觉醒总次数
        /// </summary>
        public int RERAInREMTotalCount { set; get; }
        /// <summary>
        /// REM期间的呼吸努力相关觉醒总次数
        /// </summary>
        public float RERAInREMTotalTimes { set; get; }
        /// <summary>
        /// Rem期间呼吸努力相关觉醒指数
        /// </summary>
        public float RemRERAIndex { set; get; }
        /// <summary>
        /// 呼吸努力相关觉醒总时间
        /// </summary>
        public float RERATotalTimes { set; get; }
        /// <summary>
        /// 呼吸努力相关觉醒平均时间
        /// </summary>
        public float RERAAverageTimes { set; get; }
        /// <summary>
        /// 睡眠期间呼吸事件总次数（去除清醒期）
        /// </summary>
        public float ApneaAndHypopneaTSTCount { set; get; }
        /// <summary>
        /// 睡眠期间呼吸暂停总次数（去除清醒期）
        /// </summary>
        public float ApneaTSTTotalCount { set; get; }
        /// <summary>
        /// 阻塞性呼吸暂停总次数（去除清醒期）
        /// </summary>
        public float ObstructiveApneaTSTTotalCount { set; get; }
        /// <summary>
        /// 中枢性呼吸暂停总次数（去除清醒期）
        /// </summary>
        public float CentralApneaTSTTotalCount { set; get; }

        /// <summary>
        /// 混合型呼吸暂停总时间（去除清醒期）
        /// </summary>
        public float MixedApneaTSTTotalTimes { set; get; }

        /// <summary>
        /// 低通气总次数(去除清醒期)
        /// </summary>
        public float HypopneaTSTTotalCount { set; get; }
        /// <summary>
        /// 最长呼吸暂停的发生时间
        /// </summary>
        public string strMaxApneaDruationHappendTime { set; get; }
        /// <summary>
        /// 最长呼吸暂停的发生时间
        /// </summary>
        public string strMaxHYPDruationHappendTime { set; get; }
        /// <summary>
        /// 呼吸暂停+低通气总时间
        /// </summary>
        public float ApneaAndHypopneaTotalTimes { set; get; }
        /// <summary>
        /// 最长呼吸努力相关觉醒时间
        /// </summary>
        public float MaxRERADuration { set; get; }
        /// <summary>
        /// 阻塞型呼吸暂停指数
        /// </summary>
        public float ObstructiveApneaIndex { set; get; }
        /// <summary>
        /// OAHI
        /// </summary>
        public float OAHIndex
        {
            set
            {
                m_OAHIndex = value;
                m_OAHIndex_ini = true;

                //儿童版
                if (ModeType != 0)
                {
                    if (value <= 1)
                    {
                        m_ApneaHypopneaDegreeLevel = 0;
                        strKidHypopneaDegreeLevel = "未见■ 轻度□ 中度□ 重度□";
                    }
                    else if (value > 1 && value <= 5)
                    {
                        m_ApneaHypopneaDegreeLevel = 1;
                        strKidHypopneaDegreeLevel = "未见□ 轻度■ 中度□ 重度□";
                    }
                    else if (value > 5 && value <= 10)
                    {
                        m_ApneaHypopneaDegreeLevel = 2;
                        strKidHypopneaDegreeLevel = "未见□ 轻度□ 中度■ 重度□";
                    }
                    else if (value > 10)
                    {
                        m_ApneaHypopneaDegreeLevel = 3;
                        strKidHypopneaDegreeLevel = "未见□ 轻度□ 中度□ 重度■";
                    }
                }
            }
            get
            {
                return m_OAHIndex;
            }
        }
        /// <summary>
        /// OAH总次数
        /// </summary>
        public float OAHCount { set; get; }
        /// <summary>
        /// 中枢型呼吸暂停指数
        /// </summary>
        public float CenterApneaIndex { set; get; }
        /// <summary>
        /// 混合型呼吸暂停指数
        /// </summary>
        public float MixApneaIndex { set; get; }
        /// <summary>
        /// NREM呼吸暂停指数
        /// </summary>
        public float NREMApneaIndex { set; get; }
        /// <summary>
        /// NREM阻塞型呼吸暂停指数
        /// </summary>
        public float NREMObstructiveApneaIndex { set; get; }
        /// <summary>
        /// NREM中枢型呼吸暂停指数
        /// </summary>
        public float NREMCenterApneaIndex { set; get; }
        /// <summary>
        /// NREM混合型呼吸暂停指数
        /// </summary>
        public float NREMMixApneaIndex { set; get; }
        /// <summary>
        /// Nrem期间的低通气指数
        /// </summary>
        public float NREMHypopneaIndex { set; get; }
        /// <summary>
        /// REM呼吸暂停指数
        /// </summary>
        public float REMApneaIndex { set; get; }
        /// <summary>
        /// REM阻塞型呼吸暂停指数
        /// </summary>
        public float REMObstructiveApneaIndex { set; get; }
        /// <summary>
        /// REM中枢型呼吸暂停指数
        /// </summary>
        public float REMCenterApneaIndex { set; get; }
        /// <summary>
        /// REM混合型呼吸暂停指数
        /// </summary>
        public float REMMixApneaIndex { set; get; }
        /// <summary>
        /// Rem期间的低通气指数
        /// </summary>
        public float REMHypopneaIndex { set; get; }
        /// <summary>
        /// NREM呼吸暂停及低通气指数即NAHI
        /// </summary>
        public float NREMApneaAndHypopneaIndex { set; get; }
        /// <summary>
        /// REM呼吸暂停及低通气指数即RAHI
        /// </summary>
        public float REMApneaAndHypopneaIndex { set; get; }

        /// <summary>
        /// NREM期间呼吸絮乱指数 RDI
        /// </summary>
        public float NREMApneaTurbidIndex { set; get; }
        /// <summary>
        /// REM期间呼吸絮乱指数 RDI
        /// </summary>
        public float REMApneaTurbidIndex { set; get; }

        #region 压力滴定相关
        /// <summary>
        /// 平均压力值
        /// </summary>
        public float AveragePressureInTIB { set; get; }
        #endregion

        /// <summary>
        /// 模式
        /// </summary>
        public int ModeType { set; get; }

        /// <summary>
        /// 鼻压力平均值
        /// </summary>
        public float AvgPressureMuzzleFlow { set; get; }
        /// <summary>
        /// 用口呼吸占比
        /// </summary>
        public float MouthBreathRatio { set; get; }
        /// <summary>
        /// 用口呼吸时间（s）
        /// </summary>
        public float MouthBreathTimes { set; get; }
        /// <summary>
        /// 用口呼吸时间(min)
        /// </summary>
        public float MouthBreathMinutes { set; get; }

        /// <summary>
        /// 鼻压力呼吸率（次/min）
        /// </summary>
        public float PressureMuzzleFlowRespiratoryRate { set; get; }

        /// <summary>
        /// 口呼吸判断鼻压力阈值设定
        /// 默认为 3
        /// </summary>
        private int m_MouthBreathValue = 3;
        /// <summary>
        /// 口呼吸判断鼻压力阈值设定
        /// 默认为 3
        /// </summary>
        public int MouthBreathValue
        {
            set
            {
                m_MouthBreathValue = value;
            }
            get
            {
                return m_MouthBreathValue;
            }
        }

        /// <summary>
        /// 口呼吸判断鼻压力异常范围阈值设定
        /// 默认为 50
        /// </summary>
        private int m_UnMouthBreathValue = 50;
        /// <summary>
        /// 口呼吸判断鼻压力阈值设定
        /// 默认为 50
        /// </summary>
        public int UnMouthBreathValue
        {
            set
            {
                m_UnMouthBreathValue = value;
            }
            get
            {
                return m_UnMouthBreathValue;
            }
        }
    }
}
