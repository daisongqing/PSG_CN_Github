using pSystem.Interface.Util;
using System;

namespace AwareTec.Polysmith.DataBaseCom
{
    /// <summary>
    /// 体位统计结果
    /// </summary>
    public class Doc_BodyStateResult : ITable
    {
        public Doc_BodyStateResult()
        {
        }
        #region 私有成员
        private string m_GUID = "";
        private bool m_GUID_ini = false;
        private float m_SitSleepPeriodDuration = 0;
        private bool m_SitSleepPeriodDuration_ini = false;
        private float m_SitSleepCA = 0;
        private bool m_SitSleepCA_ini = false;
        private float m_SitSleepMA = 0;
        private bool m_SitSleepMA_ini = false;
        private float m_SitSleepOA = 0;
        private bool m_SitSleepOA_ini = false;
        private float m_SitSleepHYP = 0;
        private bool m_SitSleepHYP_ini = false;
        private float m_SitSleepAHI = 0;
        private bool m_SitSleepAHI_ini = false;
        private float m_SitSleepRERA = 0;
        private bool m_SitSleepRERA_ini = false;
        private float m_SitSleepRDI = 0;
        private bool m_SitSleepRDI_ini = false;
        private float m_SitSleepDesat = 0;
        private bool m_SitSleepDesat_ini = false;
        private float m_LeftSleepPeriodDuration = 0;
        private bool m_LeftSleepPeriodDuration_ini = false;
        private float m_LeftSleepCA = 0;
        private bool m_LeftSleepCA_ini = false;
        private float m_LeftSleepMA = 0;
        private bool m_LeftSleepMA_ini = false;
        private float m_LeftSleepOA = 0;
        private bool m_LeftSleepOA_ini = false;
        private float m_LeftSleepHYP = 0;
        private bool m_LeftSleepHYP_ini = false;
        private float m_LeftSleepAHI = 0;
        private bool m_LeftSleepAHI_ini = false;
        private float m_LeftSleepRERA = 0;
        private bool m_LeftSleepRERA_ini = false;
        private float m_LeftSleepRDI = 0;
        private bool m_LeftSleepRDI_ini = false;
        private float m_LeftSleepDesat = 0;
        private bool m_LeftSleepDesat_ini = false;
        private float m_RightSleepPeriodDuration = 0;
        private bool m_RightSleepPeriodDuration_ini = false;
        private float m_RightSleepCA = 0;
        private bool m_RightSleepCA_ini = false;
        private float m_RightSleepMA = 0;
        private bool m_RightSleepMA_ini = false;
        private float m_RightSleepOA = 0;
        private bool m_RightSleepOA_ini = false;
        private float m_RightSleepHYP = 0;
        private bool m_RightSleepHYP_ini = false;
        private float m_RightSleepAHI = 0;
        private bool m_RightSleepAHI_ini = false;
        private float m_RightSleepRERA = 0;
        private bool m_RightSleepRERA_ini = false;
        private float m_RightSleepRDI = 0;
        private bool m_RightSleepRDI_ini = false;
        private float m_RightSleepDesat = 0;
        private bool m_RightSleepDesat_ini = false;
        private float m_ProstrateSleepPeriodDuration = 0;
        private bool m_ProstrateSleepPeriodDuration_ini = false;
        private float m_ProstrateSleepCA = 0;
        private bool m_ProstrateSleepCA_ini = false;
        private float m_ProstrateSleepMA = 0;
        private bool m_ProstrateSleepMA_ini = false;
        private float m_ProstrateSleepOA = 0;
        private bool m_ProstrateSleepOA_ini = false;
        private float m_ProstrateSleepHYP = 0;
        private bool m_ProstrateSleepHYP_ini = false;
        private float m_ProstrateSleepAHI = 0;
        private bool m_ProstrateSleepAHI_ini = false;
        private float m_ProstrateSleepRERA = 0;
        private bool m_ProstrateSleepRERA_ini = false;
        private float m_ProstrateSleepRDI = 0;
        private bool m_ProstrateSleepRDI_ini = false;
        private float m_ProstrateSleepDesat = 0;
        private bool m_ProstrateSleepDesat_ini = false;
        private float m_UpSleepPeriodDuration = 0;
        private bool m_UpSleepPeriodDuration_ini = false;
        private float m_UpSleepCA = 0;
        private bool m_UpSleepCA_ini = false;
        private float m_UpSleepMA = 0;
        private bool m_UpSleepMA_ini = false;
        private float m_UpSleepOA = 0;
        private bool m_UpSleepOA_ini = false;
        private float m_UpSleepHYP = 0;
        private bool m_UpSleepHYP_ini = false;
        private float m_UpSleepAHI = 0;
        private bool m_UpSleepAHI_ini = false;
        private float m_UpSleepRERA = 0;
        private bool m_UpSleepRERA_ini = false;
        private float m_UpSleepRDI = 0;
        private bool m_UpSleepRDI_ini = false;
        private float m_UpSleepDesat = 0;
        private bool m_UpSleepDesat_ini = false;
        private float m_SitTotalSleepTimeDuration = 0;
        private bool m_SitTotalSleepTimeDuration_ini = false;
        private float m_LeftTotalSleepTimeDuration = 0;
        private bool m_LeftTotalSleepTimeDuration_ini = false;
        private float m_RightTotalSleepTimeDuration = 0;
        private bool m_RightTotalSleepTimeDuration_ini = false;
        private float m_UpTotalSleepTimeDuration = 0;
        private bool m_UpTotalSleepTimeDuration_ini = false;
        private float m_ProstrateTotalSleepTimeDuration = 0;
        private bool m_ProstrateTotalSleepTimeDuration_ini = false;
        #endregion
        #region 公有成员
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
        public float SitSleepPeriodDuration
        {
            set
            {
                m_SitSleepPeriodDuration = value;
                m_SitSleepPeriodDuration_ini = true;
            }
            get
            {
                return m_SitSleepPeriodDuration;
            }
        }
        public float SitSleepCA
        {
            set
            {
                m_SitSleepCA = value;
                m_SitSleepCA_ini = true;
            }
            get
            {
                return m_SitSleepCA;
            }
        }
        public float SitSleepMA
        {
            set
            {
                m_SitSleepMA = value;
                m_SitSleepMA_ini = true;
            }
            get
            {
                return m_SitSleepMA;
            }
        }
        public float SitSleepOA
        {
            set
            {
                m_SitSleepOA = value;
                m_SitSleepOA_ini = true;
            }
            get
            {
                return m_SitSleepOA;
            }
        }
        public float SitSleepHYP
        {
            set
            {
                m_SitSleepHYP = value;
                m_SitSleepHYP_ini = true;
            }
            get
            {
                return m_SitSleepHYP;
            }
        }
        public float SitSleepAHI
        {
            set
            {
                m_SitSleepAHI = value;
                m_SitSleepAHI_ini = true;
            }
            get
            {
                return m_SitSleepAHI;
            }
        }
        public float SitSleepRERA
        {
            set
            {
                m_SitSleepRERA = value;
                m_SitSleepRERA_ini = true;
            }
            get
            {
                return m_SitSleepRERA;
            }
        }
        public float SitSleepRDI
        {
            set
            {
                m_SitSleepRDI = value;
                m_SitSleepRDI_ini = true;
            }
            get
            {
                return m_SitSleepRDI;
            }
        }
        public float SitSleepDesat
        {
            set
            {
                m_SitSleepDesat = value;
                m_SitSleepDesat_ini = true;
            }
            get
            {
                return m_SitSleepDesat;
            }
        }

        public float LeftSleepPeriodDuration
        {
            set
            {
                m_LeftSleepPeriodDuration = value;
                m_LeftSleepPeriodDuration_ini = true;
            }
            get
            {
                return m_LeftSleepPeriodDuration;
            }
        }
        public float LeftSleepCA
        {
            set
            {
                m_LeftSleepCA = value;
                m_LeftSleepCA_ini = true;
            }
            get
            {
                return m_LeftSleepCA;
            }
        }
        public float LeftSleepMA
        {
            set
            {
                m_LeftSleepMA = value;
                m_LeftSleepMA_ini = true;
            }
            get
            {
                return m_LeftSleepMA;
            }
        }
        public float LeftSleepOA
        {
            set
            {
                m_LeftSleepOA = value;
                m_LeftSleepOA_ini = true;
            }
            get
            {
                return m_LeftSleepOA;
            }
        }
        public float LeftSleepHYP
        {
            set
            {
                m_LeftSleepHYP = value;
                m_LeftSleepHYP_ini = true;
            }
            get
            {
                return m_LeftSleepHYP;
            }
        }
        public float LeftSleepAHI
        {
            set
            {
                m_LeftSleepAHI = value;
                m_LeftSleepAHI_ini = true;
            }
            get
            {
                return m_LeftSleepAHI;
            }
        }
        public float LeftSleepRERA
        {
            set
            {
                m_LeftSleepRERA = value;
                m_LeftSleepRERA_ini = true;
            }
            get
            {
                return m_LeftSleepRERA;
            }
        }
        public float LeftSleepRDI
        {
            set
            {
                m_LeftSleepRDI = value;
                m_LeftSleepRDI_ini = true;
            }
            get
            {
                return m_LeftSleepRDI;
            }
        }
        public float LeftSleepDesat
        {
            set
            {
                m_LeftSleepDesat = value;
                m_LeftSleepDesat_ini = true;
            }
            get
            {
                return m_LeftSleepDesat;
            }
        }

        public float RightSleepPeriodDuration
        {
            set
            {
                m_RightSleepPeriodDuration = value;
                m_RightSleepPeriodDuration_ini = true;
            }
            get
            {
                return m_RightSleepPeriodDuration;
            }
        }
        public float RightSleepCA
        {
            set
            {
                m_RightSleepCA = value;
                m_RightSleepCA_ini = true;
            }
            get
            {
                return m_RightSleepCA;
            }
        }
        public float RightSleepMA
        {
            set
            {
                m_RightSleepMA = value;
                m_RightSleepMA_ini = true;
            }
            get
            {
                return m_RightSleepMA;
            }
        }
        public float RightSleepOA
        {
            set
            {
                m_RightSleepOA = value;
                m_RightSleepOA_ini = true;
            }
            get
            {
                return m_RightSleepOA;
            }
        }
        public float RightSleepHYP
        {
            set
            {
                m_RightSleepHYP = value;
                m_RightSleepHYP_ini = true;
            }
            get
            {
                return m_RightSleepHYP;
            }
        }
        public float RightSleepAHI
        {
            set
            {
                m_RightSleepAHI = value;
                m_RightSleepAHI_ini = true;
            }
            get
            {
                return m_RightSleepAHI;
            }
        }
        public float RightSleepRERA
        {
            set
            {
                m_RightSleepRERA = value;
                m_RightSleepRERA_ini = true;
            }
            get
            {
                return m_RightSleepRERA;
            }
        }
        public float RightSleepRDI
        {
            set
            {
                m_RightSleepRDI = value;
                m_RightSleepRDI_ini = true;
            }
            get
            {
                return m_RightSleepRDI;
            }
        }
        public float RightSleepDesat
        {
            set
            {
                m_RightSleepDesat = value;
                m_RightSleepDesat_ini = true;
            }
            get
            {
                return m_RightSleepDesat;
            }
        }

        public float ProstrateSleepPeriodDuration
        {
            set
            {
                m_ProstrateSleepPeriodDuration = value;
                m_ProstrateSleepPeriodDuration_ini = true;
            }
            get
            {
                return m_ProstrateSleepPeriodDuration;
            }
        }
        public float ProstrateSleepCA
        {
            set
            {
                m_ProstrateSleepCA = value;
                m_ProstrateSleepCA_ini = true;
            }
            get
            {
                return m_ProstrateSleepCA;
            }
        }
        public float ProstrateSleepMA
        {
            set
            {
                m_ProstrateSleepMA = value;
                m_ProstrateSleepMA_ini = true;
            }
            get
            {
                return m_ProstrateSleepMA;
            }
        }
        public float ProstrateSleepOA
        {
            set
            {
                m_ProstrateSleepOA = value;
                m_ProstrateSleepOA_ini = true;
            }
            get
            {
                return m_ProstrateSleepOA;
            }
        }
        public float ProstrateSleepHYP
        {
            set
            {
                m_ProstrateSleepHYP = value;
                m_ProstrateSleepHYP_ini = true;
            }
            get
            {
                return m_ProstrateSleepHYP;
            }
        }
        public float ProstrateSleepAHI
        {
            set
            {
                m_ProstrateSleepAHI = value;
                m_ProstrateSleepAHI_ini = true;
            }
            get
            {
                return m_ProstrateSleepAHI;
            }
        }
        public float ProstrateSleepRERA
        {
            set
            {
                m_ProstrateSleepRERA = value;
                m_ProstrateSleepRERA_ini = true;
            }
            get
            {
                return m_ProstrateSleepRERA;
            }
        }
        public float ProstrateSleepRDI
        {
            set
            {
                m_ProstrateSleepRDI = value;
                m_ProstrateSleepRDI_ini = true;
            }
            get
            {
                return m_ProstrateSleepRDI;
            }
        }
        public float ProstrateSleepDesat
        {
            set
            {
                m_ProstrateSleepDesat = value;
                m_ProstrateSleepDesat_ini = true;
            }
            get
            {
                return m_ProstrateSleepDesat;
            }
        }

        public float UpSleepPeriodDuration
        {
            set
            {
                m_UpSleepPeriodDuration = value;
                m_UpSleepPeriodDuration_ini = true;
            }
            get
            {
                return m_UpSleepPeriodDuration;
            }
        }
        public float UpSleepCA
        {
            set
            {
                m_UpSleepCA = value;
                m_UpSleepCA_ini = true;
            }
            get
            {
                return m_UpSleepCA;
            }
        }
        public float UpSleepMA
        {
            set
            {
                m_UpSleepMA = value;
                m_UpSleepMA_ini = true;
            }
            get
            {
                return m_UpSleepMA;
            }
        }
        public float UpSleepOA
        {
            set
            {
                m_UpSleepOA = value;
                m_UpSleepOA_ini = true;
            }
            get
            {
                return m_UpSleepOA;
            }
        }
        public float UpSleepHYP
        {
            set
            {
                m_UpSleepHYP = value;
                m_UpSleepHYP_ini = true;
            }
            get
            {
                return m_UpSleepHYP;
            }
        }
        public float UpSleepAHI
        {
            set
            {
                m_UpSleepAHI = value;
                m_UpSleepAHI_ini = true;
            }
            get
            {
                return m_UpSleepAHI;
            }
        }
        public float UpSleepRERA
        {
            set
            {
                m_UpSleepRERA = value;
                m_UpSleepRERA_ini = true;
            }
            get
            {
                return m_UpSleepRERA;
            }
        }
        public float UpSleepRDI
        {
            set
            {
                m_UpSleepRDI = value;
                m_UpSleepRDI_ini = true;
            }
            get
            {
                return m_UpSleepRDI;
            }
        }
        public float UpSleepDesat
        {
            set
            {
                m_UpSleepDesat = value;
                m_UpSleepDesat_ini = true;
            }
            get
            {
                return m_UpSleepDesat;
            }
        }

        /// <summary>
        /// 侧卧时CA事件发生次数
        /// </summary>
        public float LateralSleepCA { set; get; }
        /// <summary>
        /// 侧卧时OA事件发生次数
        /// </summary>
        public float LateralSleepOA { set; get; }
        /// <summary>
        /// 侧卧时MA事件发生次数
        /// </summary>
        public float LateralSleepMA { set; get; }
        /// <summary>
        /// 侧卧时低通气事件发生次数
        /// </summary>
        public float LateralSleepHYP { set; get; }
        public float LateralSleepAHI { set; get; }
        public float LateralSleepRERA { set; get; }
        public float LateralSleepRDI { set; get; }
        public float LateralSleepDesat { set; get; }

        /// <summary>
        /// 非仰卧位OA次数
        /// </summary>
        public float UnSitSleepOA
        {
            set;
            get;
        }
        /// <summary>
        /// 非仰卧位MA次数
        /// </summary>
        public float UnSitSleepMA
        {
            set;
            get;
        }
        /// <summary>
        /// 非仰卧位CA次数
        /// </summary>
        public float UnSitSleepCA
        {
            set;
            get;
        }
        /// <summary>
        /// 非仰卧位低通气次数
        /// </summary>
        public float UnSitSleepHYP
        {
            set;
            get;
        }
        /// <summary>
        /// 非仰卧位AHI
        /// </summary>
        public float UnSitSleepAHI
        {
            set;
            get;
        }
        public float UnSitSleepRERA { set; get; }
        public float UnSitSleepRDI { set; get; }
        public float UnSitSleepDesat { set; get; }


        public float SitTotalSleepTimeDuration
        {
            set
            {
                m_SitTotalSleepTimeDuration = value;
                m_SitTotalSleepTimeDuration_ini = true;
            }
            get
            {
                return m_SitTotalSleepTimeDuration;
            }
        }
        public float LeftTotalSleepTimeDuration
        {
            set
            {
                m_LeftTotalSleepTimeDuration = value;
                m_LeftTotalSleepTimeDuration_ini = true;
            }
            get
            {
                return m_LeftTotalSleepTimeDuration;
            }
        }
        public float RightTotalSleepTimeDuration
        {
            set
            {
                m_RightTotalSleepTimeDuration = value;
                m_RightTotalSleepTimeDuration_ini = true;
            }
            get
            {
                return m_RightTotalSleepTimeDuration;
            }
        }
        public float UpTotalSleepTimeDuration
        {
            set
            {
                m_UpTotalSleepTimeDuration = value;
                m_UpTotalSleepTimeDuration_ini = true;
            }
            get
            {
                return m_UpTotalSleepTimeDuration;
            }
        }
        public float ProstrateTotalSleepTimeDuration
        {
            set
            {
                m_ProstrateTotalSleepTimeDuration = value;
                m_ProstrateTotalSleepTimeDuration_ini = true;
            }
            get
            {
                return m_ProstrateTotalSleepTimeDuration;
            }
        }
        public float LateralTotalSleepTimeDuration { set; get; }
        public float UnSitTotalSleepTimeDuration { set; get; }

        public float SitTotalSleepTimeOfTST { set; get; }
        public float LeftTotalSleepTimeOfTST { set; get; }
        public float RightTotalSleepTimeOfTST { set; get; }
        public float UpTotalSleepTimeOfTST { set; get; }
        public float ProstrateTotalSleepTimeOfTST { set; get; }
        public float LateralTotalSleepTimeOfTST { set; get; }
        public float UnSitTotalSleepTimeOfTST { set; get; }

        /// <summary>
        /// 仰卧氧减指数
        /// </summary>
        public float SitSleepDesatIndex { set; get; }
        /// <summary>
        /// 左侧卧氧减指数
        /// </summary>
        public float LeftSleepDesatIndex { set; get; }
        /// <summary>
        /// 右侧卧氧减指数
        /// </summary>
        public float RightSleepDesatIndex { set; get; }
        /// <summary>
        /// 侧卧位（左侧+右侧）氧减指数
        /// </summary>
        public float LateralSleepDesatIndex { set; get; }
        /// <summary>
        /// 俯卧氧减指数
        /// </summary>
        public float ProstrateSleepDesatIndex { set; get; }
        /// <summary>
        /// 坐立氧减指数
        /// </summary>
        public float UpSleepDesatIndex { set; get; }
        /// <summary>
        /// 非仰卧位氧减指数
        /// </summary>
        public float UnSitSleepDesatIndex { set; get; }

        /// <summary>
        /// 仰卧位OA指数
        /// </summary>
        public float SitSleepOAIndex
        {
            set;
            get;
        }
        /// <summary>
        /// 仰卧位MA指数
        /// </summary>
        public float SitSleepMAIndex
        {
            set;
            get;
        }
        /// <summary>
        /// 仰卧位CA指数
        /// </summary>
        public float SitSleepCAIndex
        {
            set;
            get;
        }
        /// <summary>
        /// 仰卧位呼吸暂停指数
        /// </summary>
        public float SitSleepAIndex
        {
            set;
            get;
        }
        /// <summary>
        /// 仰卧位低通气指数
        /// </summary>
        public float SitSleepHYPIndex
        {
            set;
            get;
        }

        /// <summary>
        /// 左侧位OA指数
        /// </summary>
        public float LeftSleepOAIndex
        {
            set;
            get;
        }
        /// <summary>
        /// 左侧位MA指数
        /// </summary>
        public float LeftSleepMAIndex
        {
            set;
            get;
        }
        /// <summary>
        /// 左侧位CA指数
        /// </summary>
        public float LeftSleepCAIndex
        {
            set;
            get;
        }
        /// <summary>
        /// 左侧位呼吸暂停指数
        /// </summary>
        public float LeftSleepAIndex
        {
            set;
            get;
        }
        /// <summary>
        /// 左侧位低通气指数
        /// </summary>
        public float LeftSleepHYPIndex
        {
            set;
            get;
        }

        /// <summary>
        /// 右侧位OA指数
        /// </summary>
        public float RightSleepOAIndex
        {
            set;
            get;
        }
        /// <summary>
        /// 右侧位MA指数
        /// </summary>
        public float RightSleepMAIndex
        {
            set;
            get;
        }
        /// <summary>
        /// 右侧位CA指数
        /// </summary>
        public float RightSleepCAIndex
        {
            set;
            get;
        }
        /// <summary>
        /// 右侧位呼吸暂停指数
        /// </summary>
        public float RightSleepAIndex
        {
            set;
            get;
        }
        /// <summary>
        /// 右侧位低通气指数
        /// </summary>
        public float RightSleepHYPIndex
        {
            set;
            get;
        }

        /// <summary>
        /// 侧卧位（左侧+右侧）OA指数
        /// </summary>
        public float LateralSleepOAIndex
        {
            set;
            get;
        }
        /// <summary>
        /// 侧卧位（左侧+右侧）MA指数
        /// </summary>
        public float LateralSleepMAIndex
        {
            set;
            get;
        }
        /// <summary>
        /// 侧卧位（左侧+右侧）CA指数
        /// </summary>
        public float LateralSleepCAIndex
        {
            set;
            get;
        }
        /// <summary>
        /// 侧卧位（左侧+右侧）呼吸暂停指数
        /// </summary>
        public float LateralSleepAIndex
        {
            set;
            get;
        }
        /// <summary>
        /// 侧卧位（左侧+右侧）低通气指数
        /// </summary>
        public float LateralSleepHYPIndex
        {
            set;
            get;
        }

        /// <summary>
        /// 俯卧位OA指数
        /// </summary>
        public float ProstrateSleepOAIndex
        {
            set;
            get;
        }
        /// <summary>
        /// 俯卧位MA指数
        /// </summary>
        public float ProstrateSleepMAIndex
        {
            set;
            get;
        }
        /// <summary>
        /// 俯卧位CA指数
        /// </summary>
        public float ProstrateSleepCAIndex
        {
            set;
            get;
        }
        /// <summary>
        /// 俯卧位呼吸暂停指数
        /// </summary>
        public float ProstrateSleepAIndex
        {
            set;
            get;
        }
        /// <summary>
        /// 俯卧位低通气指数
        /// </summary>
        public float ProstrateSleepHYPIndex
        {
            set;
            get;
        }

        /// <summary>
        /// 坐立位OA指数
        /// </summary>
        public float UpSleepOAIndex
        {
            set;
            get;
        }
        /// <summary>
        /// 坐立位MA指数
        /// </summary>
        public float UpSleepMAIndex
        {
            set;
            get;
        }
        /// <summary>
        /// 坐立位CA指数
        /// </summary>
        public float UpSleepCAIndex
        {
            set;
            get;
        }
        /// <summary>
        /// 坐立位呼吸暂停指数
        /// </summary>
        public float UpSleepAIndex
        {
            set;
            get;
        }
        /// <summary>
        /// 坐立位低通气指数
        /// </summary>
        public float UpSleepHYPIndex
        {
            set;
            get;
        }

        /// <summary>
        /// 非仰卧位OA指数
        /// </summary>
        public float UnSitSleepOAIndex
        {
            set;
            get;
        }
        /// <summary>
        /// 非仰卧位MA指数
        /// </summary>
        public float UnSitSleepMAIndex
        {
            set;
            get;
        }
        /// <summary>
        /// 非仰卧位CA指数
        /// </summary>
        public float UnSitSleepCAIndex
        {
            set;
            get;
        }
        /// <summary>
        /// 非仰卧位呼吸暂停指数
        /// </summary>
        public float UnSitSleepAIndex
        {
            set;
            get;
        }
        /// <summary>
        /// 非仰卧位低通气指数
        /// </summary>
        public float UnSitSleepHYPIndex
        {
            set;
            get;
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
            if (this.m_SitSleepPeriodDuration_ini)
            {
                def = string.Format("{0}{2}{1}", def, "SitSleepPeriodDuration", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_SitSleepPeriodDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_SitSleepCA_ini)
            {
                def = string.Format("{0}{2}{1}", def, "SitSleepCA", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_SitSleepCA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_SitSleepMA_ini)
            {
                def = string.Format("{0}{2}{1}", def, "SitSleepMA", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_SitSleepMA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_SitSleepOA_ini)
            {
                def = string.Format("{0}{2}{1}", def, "SitSleepOA", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_SitSleepOA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_SitSleepHYP_ini)
            {
                def = string.Format("{0}{2}{1}", def, "SitSleepHYP", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_SitSleepHYP, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_SitSleepAHI_ini)
            {
                def = string.Format("{0}{2}{1}", def, "SitSleepAHI", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_SitSleepAHI, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_SitSleepRERA_ini)
            {
                def = string.Format("{0}{2}{1}", def, "SitSleepRERA", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_SitSleepRERA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_SitSleepRDI_ini)
            {
                def = string.Format("{0}{2}{1}", def, "SitSleepRDI", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_SitSleepRDI, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_SitSleepDesat_ini)
            {
                def = string.Format("{0}{2}{1}", def, "SitSleepDesat", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_SitSleepDesat, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_LeftSleepPeriodDuration_ini)
            {
                def = string.Format("{0}{2}{1}", def, "LeftSleepPeriodDuration", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_LeftSleepPeriodDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_LeftSleepCA_ini)
            {
                def = string.Format("{0}{2}{1}", def, "LeftSleepCA", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_LeftSleepCA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_LeftSleepMA_ini)
            {
                def = string.Format("{0}{2}{1}", def, "LeftSleepMA", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_LeftSleepMA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_LeftSleepOA_ini)
            {
                def = string.Format("{0}{2}{1}", def, "LeftSleepOA", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_LeftSleepOA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_LeftSleepHYP_ini)
            {
                def = string.Format("{0}{2}{1}", def, "LeftSleepHYP", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_LeftSleepHYP, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_LeftSleepAHI_ini)
            {
                def = string.Format("{0}{2}{1}", def, "LeftSleepAHI", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_LeftSleepAHI, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_LeftSleepRERA_ini)
            {
                def = string.Format("{0}{2}{1}", def, "LeftSleepRERA", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_LeftSleepRERA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_LeftSleepRDI_ini)
            {
                def = string.Format("{0}{2}{1}", def, "LeftSleepRDI", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_LeftSleepRDI, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_LeftSleepDesat_ini)
            {
                def = string.Format("{0}{2}{1}", def, "LeftSleepDesat", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_LeftSleepDesat, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_RightSleepPeriodDuration_ini)
            {
                def = string.Format("{0}{2}{1}", def, "RightSleepPeriodDuration", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_RightSleepPeriodDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_RightSleepCA_ini)
            {
                def = string.Format("{0}{2}{1}", def, "RightSleepCA", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_RightSleepCA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_RightSleepMA_ini)
            {
                def = string.Format("{0}{2}{1}", def, "RightSleepMA", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_RightSleepMA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_RightSleepOA_ini)
            {
                def = string.Format("{0}{2}{1}", def, "RightSleepOA", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_RightSleepOA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_RightSleepHYP_ini)
            {
                def = string.Format("{0}{2}{1}", def, "RightSleepHYP", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_RightSleepHYP, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_RightSleepAHI_ini)
            {
                def = string.Format("{0}{2}{1}", def, "RightSleepAHI", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_RightSleepAHI, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_RightSleepRERA_ini)
            {
                def = string.Format("{0}{2}{1}", def, "RightSleepRERA", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_RightSleepRERA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_RightSleepRDI_ini)
            {
                def = string.Format("{0}{2}{1}", def, "RightSleepRDI", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_RightSleepRDI, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_RightSleepDesat_ini)
            {
                def = string.Format("{0}{2}{1}", def, "RightSleepDesat", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_RightSleepDesat, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ProstrateSleepPeriodDuration_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ProstrateSleepPeriodDuration", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ProstrateSleepPeriodDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ProstrateSleepCA_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ProstrateSleepCA", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ProstrateSleepCA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ProstrateSleepMA_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ProstrateSleepMA", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ProstrateSleepMA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ProstrateSleepOA_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ProstrateSleepOA", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ProstrateSleepOA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ProstrateSleepHYP_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ProstrateSleepHYP", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ProstrateSleepHYP, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ProstrateSleepAHI_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ProstrateSleepAHI", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ProstrateSleepAHI, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ProstrateSleepRERA_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ProstrateSleepRERA", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ProstrateSleepRERA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ProstrateSleepRDI_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ProstrateSleepRDI", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ProstrateSleepRDI, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ProstrateSleepDesat_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ProstrateSleepDesat", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ProstrateSleepDesat, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_UpSleepPeriodDuration_ini)
            {
                def = string.Format("{0}{2}{1}", def, "UpSleepPeriodDuration", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_UpSleepPeriodDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_UpSleepCA_ini)
            {
                def = string.Format("{0}{2}{1}", def, "UpSleepCA", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_UpSleepCA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_UpSleepMA_ini)
            {
                def = string.Format("{0}{2}{1}", def, "UpSleepMA", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_UpSleepMA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_UpSleepOA_ini)
            {
                def = string.Format("{0}{2}{1}", def, "UpSleepOA", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_UpSleepOA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_UpSleepHYP_ini)
            {
                def = string.Format("{0}{2}{1}", def, "UpSleepHYP", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_UpSleepHYP, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_UpSleepAHI_ini)
            {
                def = string.Format("{0}{2}{1}", def, "UpSleepAHI", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_UpSleepAHI, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_UpSleepRERA_ini)
            {
                def = string.Format("{0}{2}{1}", def, "UpSleepRERA", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_UpSleepRERA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_UpSleepRDI_ini)
            {
                def = string.Format("{0}{2}{1}", def, "UpSleepRDI", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_UpSleepRDI, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_UpSleepDesat_ini)
            {
                def = string.Format("{0}{2}{1}", def, "UpSleepDesat", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_UpSleepDesat, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_SitTotalSleepTimeDuration_ini)
            {
                def = string.Format("{0}{2}{1}", def, "SitTotalSleepTimeDuration", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_SitTotalSleepTimeDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_LeftTotalSleepTimeDuration_ini)
            {
                def = string.Format("{0}{2}{1}", def, "LeftTotalSleepTimeDuration", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_LeftTotalSleepTimeDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_RightTotalSleepTimeDuration_ini)
            {
                def = string.Format("{0}{2}{1}", def, "RightTotalSleepTimeDuration", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_RightTotalSleepTimeDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_UpTotalSleepTimeDuration_ini)
            {
                def = string.Format("{0}{2}{1}", def, "UpTotalSleepTimeDuration", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_UpTotalSleepTimeDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ProstrateTotalSleepTimeDuration_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ProstrateTotalSleepTimeDuration", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ProstrateTotalSleepTimeDuration, Mulitcase ? "," : "");
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
            if (m_SitSleepPeriodDuration_ini)
            {
                returnstr = string.Format("{0}SitSleepPeriodDuration={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_SitSleepPeriodDuration);
                Mulitcase = true;
            }
            if (m_SitSleepCA_ini)
            {
                returnstr = string.Format("{0}SitSleepCA={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_SitSleepCA);
                Mulitcase = true;
            }
            if (m_SitSleepMA_ini)
            {
                returnstr = string.Format("{0}SitSleepMA={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_SitSleepMA);
                Mulitcase = true;
            }
            if (m_SitSleepOA_ini)
            {
                returnstr = string.Format("{0}SitSleepOA={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_SitSleepOA);
                Mulitcase = true;
            }
            if (m_SitSleepHYP_ini)
            {
                returnstr = string.Format("{0}SitSleepHYP={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_SitSleepHYP);
                Mulitcase = true;
            }
            if (m_SitSleepAHI_ini)
            {
                returnstr = string.Format("{0}SitSleepAHI={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_SitSleepAHI);
                Mulitcase = true;
            }
            if (m_SitSleepRERA_ini)
            {
                returnstr = string.Format("{0}SitSleepRERA={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_SitSleepRERA);
                Mulitcase = true;
            }
            if (m_SitSleepRDI_ini)
            {
                returnstr = string.Format("{0}SitSleepRDI={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_SitSleepRDI);
                Mulitcase = true;
            }
            if (m_SitSleepDesat_ini)
            {
                returnstr = string.Format("{0}SitSleepDesat={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_SitSleepDesat);
                Mulitcase = true;
            }
            if (m_LeftSleepPeriodDuration_ini)
            {
                returnstr = string.Format("{0}LeftSleepPeriodDuration={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_LeftSleepPeriodDuration);
                Mulitcase = true;
            }
            if (m_LeftSleepCA_ini)
            {
                returnstr = string.Format("{0}LeftSleepCA={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_LeftSleepCA);
                Mulitcase = true;
            }
            if (m_LeftSleepMA_ini)
            {
                returnstr = string.Format("{0}LeftSleepMA={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_LeftSleepMA);
                Mulitcase = true;
            }
            if (m_LeftSleepOA_ini)
            {
                returnstr = string.Format("{0}LeftSleepOA={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_LeftSleepOA);
                Mulitcase = true;
            }
            if (m_LeftSleepHYP_ini)
            {
                returnstr = string.Format("{0}LeftSleepHYP={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_LeftSleepHYP);
                Mulitcase = true;
            }
            if (m_LeftSleepAHI_ini)
            {
                returnstr = string.Format("{0}LeftSleepAHI={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_LeftSleepAHI);
                Mulitcase = true;
            }
            if (m_LeftSleepRERA_ini)
            {
                returnstr = string.Format("{0}LeftSleepRERA={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_LeftSleepRERA);
                Mulitcase = true;
            }
            if (m_LeftSleepRDI_ini)
            {
                returnstr = string.Format("{0}LeftSleepRDI={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_LeftSleepRDI);
                Mulitcase = true;
            }
            if (m_LeftSleepDesat_ini)
            {
                returnstr = string.Format("{0}LeftSleepDesat={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_LeftSleepDesat);
                Mulitcase = true;
            }
            if (m_RightSleepPeriodDuration_ini)
            {
                returnstr = string.Format("{0}RightSleepPeriodDuration={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_RightSleepPeriodDuration);
                Mulitcase = true;
            }
            if (m_RightSleepCA_ini)
            {
                returnstr = string.Format("{0}RightSleepCA={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_RightSleepCA);
                Mulitcase = true;
            }
            if (m_RightSleepMA_ini)
            {
                returnstr = string.Format("{0}RightSleepMA={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_RightSleepMA);
                Mulitcase = true;
            }
            if (m_RightSleepOA_ini)
            {
                returnstr = string.Format("{0}RightSleepOA={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_RightSleepOA);
                Mulitcase = true;
            }
            if (m_RightSleepHYP_ini)
            {
                returnstr = string.Format("{0}RightSleepHYP={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_RightSleepHYP);
                Mulitcase = true;
            }
            if (m_RightSleepAHI_ini)
            {
                returnstr = string.Format("{0}RightSleepAHI={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_RightSleepAHI);
                Mulitcase = true;
            }
            if (m_RightSleepRERA_ini)
            {
                returnstr = string.Format("{0}RightSleepRERA={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_RightSleepRERA);
                Mulitcase = true;
            }
            if (m_RightSleepRDI_ini)
            {
                returnstr = string.Format("{0}RightSleepRDI={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_RightSleepRDI);
                Mulitcase = true;
            }
            if (m_RightSleepDesat_ini)
            {
                returnstr = string.Format("{0}RightSleepDesat={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_RightSleepDesat);
                Mulitcase = true;
            }
            if (m_ProstrateSleepPeriodDuration_ini)
            {
                returnstr = string.Format("{0}ProstrateSleepPeriodDuration={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ProstrateSleepPeriodDuration);
                Mulitcase = true;
            }
            if (m_ProstrateSleepCA_ini)
            {
                returnstr = string.Format("{0}ProstrateSleepCA={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ProstrateSleepCA);
                Mulitcase = true;
            }
            if (m_ProstrateSleepMA_ini)
            {
                returnstr = string.Format("{0}ProstrateSleepMA={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ProstrateSleepMA);
                Mulitcase = true;
            }
            if (m_ProstrateSleepOA_ini)
            {
                returnstr = string.Format("{0}ProstrateSleepOA={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ProstrateSleepOA);
                Mulitcase = true;
            }
            if (m_ProstrateSleepHYP_ini)
            {
                returnstr = string.Format("{0}ProstrateSleepHYP={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ProstrateSleepHYP);
                Mulitcase = true;
            }
            if (m_ProstrateSleepAHI_ini)
            {
                returnstr = string.Format("{0}ProstrateSleepAHI={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ProstrateSleepAHI);
                Mulitcase = true;
            }
            if (m_ProstrateSleepRERA_ini)
            {
                returnstr = string.Format("{0}ProstrateSleepRERA={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ProstrateSleepRERA);
                Mulitcase = true;
            }
            if (m_ProstrateSleepRDI_ini)
            {
                returnstr = string.Format("{0}ProstrateSleepRDI={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ProstrateSleepRDI);
                Mulitcase = true;
            }
            if (m_ProstrateSleepDesat_ini)
            {
                returnstr = string.Format("{0}ProstrateSleepDesat={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ProstrateSleepDesat);
                Mulitcase = true;
            }
            if (m_UpSleepPeriodDuration_ini)
            {
                returnstr = string.Format("{0}UpSleepPeriodDuration={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_UpSleepPeriodDuration);
                Mulitcase = true;
            }
            if (m_UpSleepCA_ini)
            {
                returnstr = string.Format("{0}UpSleepCA={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_UpSleepCA);
                Mulitcase = true;
            }
            if (m_UpSleepMA_ini)
            {
                returnstr = string.Format("{0}UpSleepMA={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_UpSleepMA);
                Mulitcase = true;
            }
            if (m_UpSleepOA_ini)
            {
                returnstr = string.Format("{0}UpSleepOA={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_UpSleepOA);
                Mulitcase = true;
            }
            if (m_UpSleepHYP_ini)
            {
                returnstr = string.Format("{0}UpSleepHYP={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_UpSleepHYP);
                Mulitcase = true;
            }
            if (m_UpSleepAHI_ini)
            {
                returnstr = string.Format("{0}UpSleepAHI={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_UpSleepAHI);
                Mulitcase = true;
            }
            if (m_UpSleepRERA_ini)
            {
                returnstr = string.Format("{0}UpSleepRERA={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_UpSleepRERA);
                Mulitcase = true;
            }
            if (m_UpSleepRDI_ini)
            {
                returnstr = string.Format("{0}UpSleepRDI={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_UpSleepRDI);
                Mulitcase = true;
            }
            if (m_UpSleepDesat_ini)
            {
                returnstr = string.Format("{0}UpSleepDesat={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_UpSleepDesat);
                Mulitcase = true;
            }
            if (m_SitTotalSleepTimeDuration_ini)
            {
                returnstr = string.Format("{0}SitTotalSleepTimeDuration={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_SitTotalSleepTimeDuration);
                Mulitcase = true;
            }
            if (m_LeftTotalSleepTimeDuration_ini)
            {
                returnstr = string.Format("{0}LeftTotalSleepTimeDuration={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_LeftTotalSleepTimeDuration);
                Mulitcase = true;
            }
            if (m_RightTotalSleepTimeDuration_ini)
            {
                returnstr = string.Format("{0}RightTotalSleepTimeDuration={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_RightTotalSleepTimeDuration);
                Mulitcase = true;
            }
            if (m_UpTotalSleepTimeDuration_ini)
            {
                returnstr = string.Format("{0}UpTotalSleepTimeDuration={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_UpTotalSleepTimeDuration);
                Mulitcase = true;
            }
            if (m_ProstrateTotalSleepTimeDuration_ini)
            {
                returnstr = string.Format("{0}ProstrateTotalSleepTimeDuration={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ProstrateTotalSleepTimeDuration);
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
            if (m_SitSleepPeriodDuration_ini)
            {
                def = string.Format("{0}{2}SitSleepPeriodDuration={1}", def, m_SitSleepPeriodDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_SitSleepCA_ini)
            {
                def = string.Format("{0}{2}SitSleepCA={1}", def, m_SitSleepCA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_SitSleepMA_ini)
            {
                def = string.Format("{0}{2}SitSleepMA={1}", def, m_SitSleepMA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_SitSleepOA_ini)
            {
                def = string.Format("{0}{2}SitSleepOA={1}", def, m_SitSleepOA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_SitSleepHYP_ini)
            {
                def = string.Format("{0}{2}SitSleepHYP={1}", def, m_SitSleepHYP, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_SitSleepAHI_ini)
            {
                def = string.Format("{0}{2}SitSleepAHI={1}", def, m_SitSleepAHI, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_SitSleepRERA_ini)
            {
                def = string.Format("{0}{2}SitSleepRERA={1}", def, m_SitSleepRERA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_SitSleepRDI_ini)
            {
                def = string.Format("{0}{2}SitSleepRDI={1}", def, m_SitSleepRDI, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_SitSleepDesat_ini)
            {
                def = string.Format("{0}{2}SitSleepDesat={1}", def, m_SitSleepDesat, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_LeftSleepPeriodDuration_ini)
            {
                def = string.Format("{0}{2}LeftSleepPeriodDuration={1}", def, m_LeftSleepPeriodDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_LeftSleepCA_ini)
            {
                def = string.Format("{0}{2}LeftSleepCA={1}", def, m_LeftSleepCA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_LeftSleepMA_ini)
            {
                def = string.Format("{0}{2}LeftSleepMA={1}", def, m_LeftSleepMA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_LeftSleepOA_ini)
            {
                def = string.Format("{0}{2}LeftSleepOA={1}", def, m_LeftSleepOA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_LeftSleepHYP_ini)
            {
                def = string.Format("{0}{2}LeftSleepHYP={1}", def, m_LeftSleepHYP, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_LeftSleepAHI_ini)
            {
                def = string.Format("{0}{2}LeftSleepAHI={1}", def, m_LeftSleepAHI, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_LeftSleepRERA_ini)
            {
                def = string.Format("{0}{2}LeftSleepRERA={1}", def, m_LeftSleepRERA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_LeftSleepRDI_ini)
            {
                def = string.Format("{0}{2}LeftSleepRDI={1}", def, m_LeftSleepRDI, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_LeftSleepDesat_ini)
            {
                def = string.Format("{0}{2}LeftSleepDesat={1}", def, m_LeftSleepDesat, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_RightSleepPeriodDuration_ini)
            {
                def = string.Format("{0}{2}RightSleepPeriodDuration={1}", def, m_RightSleepPeriodDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_RightSleepCA_ini)
            {
                def = string.Format("{0}{2}RightSleepCA={1}", def, m_RightSleepCA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_RightSleepMA_ini)
            {
                def = string.Format("{0}{2}RightSleepMA={1}", def, m_RightSleepMA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_RightSleepOA_ini)
            {
                def = string.Format("{0}{2}RightSleepOA={1}", def, m_RightSleepOA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_RightSleepHYP_ini)
            {
                def = string.Format("{0}{2}RightSleepHYP={1}", def, m_RightSleepHYP, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_RightSleepAHI_ini)
            {
                def = string.Format("{0}{2}RightSleepAHI={1}", def, m_RightSleepAHI, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_RightSleepRERA_ini)
            {
                def = string.Format("{0}{2}RightSleepRERA={1}", def, m_RightSleepRERA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_RightSleepRDI_ini)
            {
                def = string.Format("{0}{2}RightSleepRDI={1}", def, m_RightSleepRDI, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_RightSleepDesat_ini)
            {
                def = string.Format("{0}{2}RightSleepDesat={1}", def, m_RightSleepDesat, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ProstrateSleepPeriodDuration_ini)
            {
                def = string.Format("{0}{2}ProstrateSleepPeriodDuration={1}", def, m_ProstrateSleepPeriodDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ProstrateSleepCA_ini)
            {
                def = string.Format("{0}{2}ProstrateSleepCA={1}", def, m_ProstrateSleepCA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ProstrateSleepMA_ini)
            {
                def = string.Format("{0}{2}ProstrateSleepMA={1}", def, m_ProstrateSleepMA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ProstrateSleepOA_ini)
            {
                def = string.Format("{0}{2}ProstrateSleepOA={1}", def, m_ProstrateSleepOA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ProstrateSleepHYP_ini)
            {
                def = string.Format("{0}{2}ProstrateSleepHYP={1}", def, m_ProstrateSleepHYP, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ProstrateSleepAHI_ini)
            {
                def = string.Format("{0}{2}ProstrateSleepAHI={1}", def, m_ProstrateSleepAHI, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ProstrateSleepRERA_ini)
            {
                def = string.Format("{0}{2}ProstrateSleepRERA={1}", def, m_ProstrateSleepRERA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ProstrateSleepRDI_ini)
            {
                def = string.Format("{0}{2}ProstrateSleepRDI={1}", def, m_ProstrateSleepRDI, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ProstrateSleepDesat_ini)
            {
                def = string.Format("{0}{2}ProstrateSleepDesat={1}", def, m_ProstrateSleepDesat, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_UpSleepPeriodDuration_ini)
            {
                def = string.Format("{0}{2}UpSleepPeriodDuration={1}", def, m_UpSleepPeriodDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_UpSleepCA_ini)
            {
                def = string.Format("{0}{2}UpSleepCA={1}", def, m_UpSleepCA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_UpSleepMA_ini)
            {
                def = string.Format("{0}{2}UpSleepMA={1}", def, m_UpSleepMA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_UpSleepOA_ini)
            {
                def = string.Format("{0}{2}UpSleepOA={1}", def, m_UpSleepOA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_UpSleepHYP_ini)
            {
                def = string.Format("{0}{2}UpSleepHYP={1}", def, m_UpSleepHYP, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_UpSleepAHI_ini)
            {
                def = string.Format("{0}{2}UpSleepAHI={1}", def, m_UpSleepAHI, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_UpSleepRERA_ini)
            {
                def = string.Format("{0}{2}UpSleepRERA={1}", def, m_UpSleepRERA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_UpSleepRDI_ini)
            {
                def = string.Format("{0}{2}UpSleepRDI={1}", def, m_UpSleepRDI, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_UpSleepDesat_ini)
            {
                def = string.Format("{0}{2}UpSleepDesat={1}", def, m_UpSleepDesat, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_SitTotalSleepTimeDuration_ini)
            {
                def = string.Format("{0}{2}SitTotalSleepTimeDuration={1}", def, m_SitTotalSleepTimeDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_LeftTotalSleepTimeDuration_ini)
            {
                def = string.Format("{0}{2}LeftTotalSleepTimeDuration={1}", def, m_LeftTotalSleepTimeDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_RightTotalSleepTimeDuration_ini)
            {
                def = string.Format("{0}{2}RightTotalSleepTimeDuration={1}", def, m_RightTotalSleepTimeDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_UpTotalSleepTimeDuration_ini)
            {
                def = string.Format("{0}{2}UpTotalSleepTimeDuration={1}", def, m_UpTotalSleepTimeDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ProstrateTotalSleepTimeDuration_ini)
            {
                def = string.Format("{0}{2}ProstrateTotalSleepTimeDuration={1}", def, m_ProstrateTotalSleepTimeDuration, Mulitcase ? "," : "");
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
            return ("BodyStateResult");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "GUID":
                    GUID = Value.ToString(); ;
                    break;
                case "SitSleepPeriodDuration":
                    SitSleepPeriodDuration = Convert.ToSingle(Value);
                    break;
                case "SitSleepCA":
                    SitSleepCA = Convert.ToSingle(Value);
                    break;
                case "SitSleepMA":
                    SitSleepMA = Convert.ToSingle(Value);
                    break;
                case "SitSleepOA":
                    SitSleepOA = Convert.ToSingle(Value);
                    break;
                case "SitSleepHYP":
                    SitSleepHYP = Convert.ToSingle(Value);
                    break;
                case "SitSleepAHI":
                    SitSleepAHI = Convert.ToSingle(Value);
                    break;
                case "SitSleepRERA":
                    SitSleepRERA = Convert.ToSingle(Value);
                    break;
                case "SitSleepRDI":
                    SitSleepRDI = Convert.ToSingle(Value);
                    break;
                case "SitSleepDesat":
                    SitSleepDesat = Convert.ToSingle(Value);
                    break;
                case "LeftSleepPeriodDuration":
                    LeftSleepPeriodDuration = Convert.ToSingle(Value);
                    break;
                case "LeftSleepCA":
                    LeftSleepCA = Convert.ToSingle(Value);
                    break;
                case "LeftSleepMA":
                    LeftSleepMA = Convert.ToSingle(Value);
                    break;
                case "LeftSleepOA":
                    LeftSleepOA = Convert.ToSingle(Value);
                    break;
                case "LeftSleepHYP":
                    LeftSleepHYP = Convert.ToSingle(Value);
                    break;
                case "LeftSleepAHI":
                    LeftSleepAHI = Convert.ToSingle(Value);
                    break;
                case "LeftSleepRERA":
                    LeftSleepRERA = Convert.ToSingle(Value);
                    break;
                case "LeftSleepRDI":
                    LeftSleepRDI = Convert.ToSingle(Value);
                    break;
                case "LeftSleepDesat":
                    LeftSleepDesat = Convert.ToSingle(Value);
                    break;
                case "RightSleepPeriodDuration":
                    RightSleepPeriodDuration = Convert.ToSingle(Value);
                    break;
                case "RightSleepCA":
                    RightSleepCA = Convert.ToSingle(Value);
                    break;
                case "RightSleepMA":
                    RightSleepMA = Convert.ToSingle(Value);
                    break;
                case "RightSleepOA":
                    RightSleepOA = Convert.ToSingle(Value);
                    break;
                case "RightSleepHYP":
                    RightSleepHYP = Convert.ToSingle(Value);
                    break;
                case "RightSleepAHI":
                    RightSleepAHI = Convert.ToSingle(Value);
                    break;
                case "RightSleepRERA":
                    RightSleepRERA = Convert.ToSingle(Value);
                    break;
                case "RightSleepRDI":
                    RightSleepRDI = Convert.ToSingle(Value);
                    break;
                case "RightSleepDesat":
                    RightSleepDesat = Convert.ToSingle(Value);
                    break;
                case "ProstrateSleepPeriodDuration":
                    ProstrateSleepPeriodDuration = Convert.ToSingle(Value);
                    break;
                case "ProstrateSleepCA":
                    ProstrateSleepCA = Convert.ToSingle(Value);
                    break;
                case "ProstrateSleepMA":
                    ProstrateSleepMA = Convert.ToSingle(Value);
                    break;
                case "ProstrateSleepOA":
                    ProstrateSleepOA = Convert.ToSingle(Value);
                    break;
                case "ProstrateSleepHYP":
                    ProstrateSleepHYP = Convert.ToSingle(Value);
                    break;
                case "ProstrateSleepAHI":
                    ProstrateSleepAHI = Convert.ToSingle(Value);
                    break;
                case "ProstrateSleepRERA":
                    ProstrateSleepRERA = Convert.ToSingle(Value);
                    break;
                case "ProstrateSleepRDI":
                    ProstrateSleepRDI = Convert.ToSingle(Value);
                    break;
                case "ProstrateSleepDesat":
                    ProstrateSleepDesat = Convert.ToSingle(Value);
                    break;
                case "UpSleepPeriodDuration":
                    UpSleepPeriodDuration = Convert.ToSingle(Value);
                    break;
                case "UpSleepCA":
                    UpSleepCA = Convert.ToSingle(Value);
                    break;
                case "UpSleepMA":
                    UpSleepMA = Convert.ToSingle(Value);
                    break;
                case "UpSleepOA":
                    UpSleepOA = Convert.ToSingle(Value);
                    break;
                case "UpSleepHYP":
                    UpSleepHYP = Convert.ToSingle(Value);
                    break;
                case "UpSleepAHI":
                    UpSleepAHI = Convert.ToSingle(Value);
                    break;
                case "UpSleepRERA":
                    UpSleepRERA = Convert.ToSingle(Value);
                    break;
                case "UpSleepRDI":
                    UpSleepRDI = Convert.ToSingle(Value);
                    break;
                case "UpSleepDesat":
                    UpSleepDesat = Convert.ToSingle(Value);
                    break;
                case "SitTotalSleepTimeDuration":
                    SitTotalSleepTimeDuration = Convert.ToSingle(Value);
                    break;
                case "LeftTotalSleepTimeDuration":
                    LeftTotalSleepTimeDuration = Convert.ToSingle(Value);
                    break;
                case "RightTotalSleepTimeDuration":
                    RightTotalSleepTimeDuration = Convert.ToSingle(Value);
                    break;
                case "UpTotalSleepTimeDuration":
                    UpTotalSleepTimeDuration = Convert.ToSingle(Value);
                    break;
                case "ProstrateTotalSleepTimeDuration":
                    ProstrateTotalSleepTimeDuration = Convert.ToSingle(Value);
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_BodyStateResult());
        }
        #endregion
        /// <summary>
        /// 仰卧位时所有呼吸暂停事件的总次数
        /// </summary>
        public float SitSleepATotalCount { set; get; }
        /// <summary>
        /// 左侧卧位时所有呼吸暂停事件的总次数
        /// </summary>
        public float LeftSleepATotalCount { set; get; }
        /// <summary>
        /// 右侧卧位时所有呼吸暂停事件的总次数
        /// </summary>
        public float RightSleepATotalCount { set; get; }
        /// <summary>
        /// 坐立卧位时所有呼吸暂停事件的总次数
        /// </summary>
        public float UpSleepATotalCount { set; get; }
        /// <summary>
        /// 俯卧位时所有呼吸暂停事件的总次数
        /// </summary>
        public float ProstrateSleepATotalCount { set; get; }
        /// <summary>
        /// 侧卧位时（左侧 右侧）所有呼吸暂停事件的总次数
        /// </summary>
        public float LateralSleepATotalCount { set; get; }
        /// <summary>
        /// 非仰卧位时所有呼吸暂停事件的总次数
        /// </summary>
        public float UnSitSleepATotalCount { set; get; }

        /// <summary>
        /// 仰卧位时所有呼吸事件的总次数
        /// </summary>
        public float SitSleepAHTotalCount { set; get; }
        /// <summary>
        /// 左侧卧位时所有呼吸事件的总次数
        /// </summary>
        public float LeftSleepAHTotalCount { set; get; }
        /// <summary>
        /// 右侧卧位时所有呼吸事件的总次数
        /// </summary>
        public float RightSleepAHTotalCount { set; get; }
        /// <summary>
        /// 坐立卧位时所有呼吸事件的总次数
        /// </summary>
        public float UpSleepAHTotalCount { set; get; }
        /// <summary>
        /// 附卧位时所有呼吸事件的总次数
        /// </summary>
        public float ProstrateSleepAHTotalCount { set; get; }
        /// <summary>
        /// 侧卧位时（左侧 右侧）所有呼吸事件的总次数
        /// </summary>
        public float LateralSleepAHTotalCount { set; get; }
        /// <summary>
        /// 非仰卧位时所有呼吸事件的总次数
        /// </summary>
        public float UnSitSleepAHTotalCount { set; get; }

        /// <summary>
        /// TST期间非仰卧位的总时间
        /// </summary>
        public float UnSupineTimesInTotalSleepTime { set; get; }
        /// <summary>
        /// TST期间仰卧位的总时间
        /// </summary>
        public float UpTimesInTotalSleepTime { set; get; }
        /// <summary>
        /// N1期间仰卧位的总时间
        /// </summary>
        public float UpTimesInN1SleepTime { set; get; }
        /// <summary>
        /// N2期间仰卧位的总时间
        /// </summary>
        public float UpTimesInN2SleepTime { set; get; }
        /// <summary>
        /// N3期间仰卧位的总时间
        /// </summary>
        public float UpTimesInN3SleepTime { set; get; }
        /// <summary>
        /// R期间仰卧位的总时间
        /// </summary>
        public float UpTimesInREMSleepTime { set; get; }

        /// <summary>
        /// TST期间侧卧位的总时间
        /// </summary>
        public float LateralTimesInTotalSleepTime { set; get; }
        /// <summary>
        /// N1期间侧卧位的总时间
        /// </summary>
        public float LateralTimesInN1SleepTime { set; get; }
        /// <summary>
        /// N2期间侧卧位的总时间
        /// </summary>
        public float LateralTimesInN2SleepTime { set; get; }
        /// <summary>
        /// N3期间侧卧位的总时间
        /// </summary>
        public float LateralTimesInN3SleepTime { set; get; }
        /// <summary>
        /// R期间侧卧位的总时间
        /// </summary>
        public float LateralTimesInREMSleepTime { set; get; }

        /// <summary>
        /// TST期间俯卧位的总时间
        /// </summary>
        public float ProstrateTimesInTotalSleepTime { set; get; }
        /// <summary>
        /// N1期间俯卧位的总时间
        /// </summary>
        public float ProstrateTimesInN1SleepTime { set; get; }
        /// <summary>
        /// N2期间俯卧位的总时间
        /// </summary>
        public float ProstrateTimesInN2SleepTime { set; get; }
        /// <summary>
        /// N3期间俯卧位的总时间
        /// </summary>
        public float ProstrateTimesInN3SleepTime { set; get; }
        /// <summary>
        /// R期间俯卧位的总时间
        /// </summary>
        public float ProstrateTimesInREMSleepTime { set; get; }
        /// <summary>
        /// TST期间坐立的总时间
        /// </summary>
        public float SitTimesInTotalSleepTime { set; get; }
        /// <summary>
        /// N1期间坐立位的总时间
        /// </summary>
        public float SitTimesInN1SleepTime { set; get; }
        /// <summary>
        /// N2期间坐立位的总时间
        /// </summary>
        public float SitTimesInN2SleepTime { set; get; }
        /// <summary>
        /// N3期间坐立位的总时间
        /// </summary>
        public float SitTimesInN3SleepTime { set; get; }
        /// <summary>
        /// R期间坐立位的总时间
        /// </summary>
        public float SitTimesInREMSleepTime { set; get; }

        public float NREMProstrateSleepCA { set; get; }

        public float REMProstrateSleepCA { set; get; }

        public float NREMProstrateSleepMA { set; get; }

        public float REMProstrateSleepMA { set; get; }

        public float NREMProstrateSleepOA { set; get; }

        public float REMProstrateSleepOA { set; get; }

        public float NREMProstrateSleepHYP { set; get; }

        public float REMProstrateSleepHYP { set; get; }

        public float NREMUpSleepCA { set; get; }

        public float REMUpSleepCA { set; get; }

        public float NREMUpSleepMA { set; get; }

        public float REMUpSleepMA { set; get; }

        public float NREMUpSleepOA { set; get; }

        public float REMUpSleepOA { set; get; }

        public float NREMUpSleepHYP { set; get; }

        public float REMUpSleepHYP { set; get; }

        public float NREMLateralSleepCA { set; get; }

        public float REMLateralSleepCA { set; get; }

        public float NREMLateralSleepMA { set; get; }

        public float REMLateralSleepMA { set; get; }

        public float NREMLateralSleepOA { set; get; }

        public float REMLateralSleepOA { set; get; }

        public float NREMLateralSleepHYP { set; get; }

        public float REMLateralSleepHYP { set; get; }
        /// <summary>
        /// 仰卧（坐立定义混了，为了以前版本保留UP的定义）
        /// </summary>
        public float UpSleepDesatThan100 { set; get; }
        public float UpSleepDesatThan90 { set; get; }
        public float UpSleepDesatThan80 { set; get; }
        public float UpSleepDesatThan70 { set; get; }
        public float UpSleepDesatThan60 { set; get; }
        public float UpSleepDesatThan50 { set; get; }
        public float UpSleepDesatThan40 { set; get; }
        public float SitSleepDesatThan100 { set; get; }
        public float SitSleepDesatThan90 { set; get; }
        public float SitSleepDesatThan80 { set; get; }
        public float SitSleepDesatThan70 { set; get; }
        public float SitSleepDesatThan60 { set; get; }
        public float SitSleepDesatThan50 { set; get; }
        public float SitSleepDesatThan40 { set; get; }

        public float ProstrateSleepDesatThan100 { set; get; }
        public float ProstrateSleepDesatThan90 { set; get; }
        public float ProstrateSleepDesatThan80 { set; get; }
        public float ProstrateSleepDesatThan70 { set; get; }
        public float ProstrateSleepDesatThan60 { set; get; }
        public float ProstrateSleepDesatThan50 { set; get; }
        public float ProstrateSleepDesatThan40 { set; get; }
        /// <summary>
        /// 侧卧
        /// </summary>
        public float LateralSleepDesatThan100 { set; get; }
        public float LateralSleepDesatThan90 { set; get; }
        public float LateralSleepDesatThan80 { set; get; }
        public float LateralSleepDesatThan70 { set; get; }
        public float LateralSleepDesatThan60 { set; get; }
        public float LateralSleepDesatThan50 { set; get; }
        public float LateralSleepDesatThan40 { set; get; }
        /// <summary>
        /// 俯卧位最低血氧值
        /// </summary>
        public float MinSpo2ValueOnProstrate { set; get; }
        /// <summary>
        /// 坐立位最低血氧值
        /// </summary>
        public float MinSpo2ValueOnUp { set; get; }
        /// <summary>
        /// 侧卧位最低血氧值
        /// </summary>
        public float MinSpo2ValueOnLateral { set; get; }

        /// <summary>
        /// CA事件在仰卧姿势发生的总时间(s)
        /// </summary>
        public float SitSleepCATotalTimes { set; get; }
        /// <summary>
        /// OA事件在仰卧姿势发生的总时间(s)
        /// </summary>
        public float SitSleepOATotalTimes { set; get; }
        /// <summary>
        /// MA事件在仰卧姿势发生的总时间(s)
        /// </summary>
        public float SitSleepMATotalTimes { set; get; }
        /// <summary>
        /// 低通气事件在仰卧姿势发生的总时间(s)
        /// </summary>
        public float SitSleepHYPTotalTimes { set; get; }
        /// <summary>
        /// 呼吸暂停事件在仰卧姿势发生的总时间(s)
        /// </summary>
        public float SitSleepATotalTimes { set; get; }

        /// <summary>
        /// CA事件在侧卧位（包括左侧右侧）发生的总时间（s）
        /// </summary>
        public float LateralSleepCATotalTimes { set; get; }
        /// <summary>
        /// OA事件在侧卧位（包括左侧右侧）发生的总时间（s）
        /// </summary>
        public float LateralSleepOATotalTimes { set; get; }
        /// <summary>
        /// MA事件在侧卧位（包括左侧右侧）发生的总时间（s）
        /// </summary>
        public float LateralSleepMATotalTimes { set; get; }
        /// <summary>
        /// 低通气事件在侧卧位（包括左侧右侧）发生的总时间（s）
        /// </summary>
        public float LateralSleepHYPTotalTimes { set; get; }
        /// <summary>
        /// 呼吸暂停事件在侧卧位（包括左侧右侧）发生的总时间（s）
        /// </summary>
        public float LateralSleepATotalTimes { set; get; }

        /// <summary>
        /// CA事件在左侧位发生的总时间（s）
        /// </summary>
        public float LeftSleepCATotalTimes { set; get; }
        /// <summary>
        /// OA事件在左侧位发生的总时间（s）
        /// </summary>
        public float LeftSleepOATotalTimes { set; get; }
        /// <summary>
        /// MA事件在左侧位发生的总时间（s）
        /// </summary>
        public float LeftSleepMATotalTimes { set; get; }
        /// <summary>
        /// 低通气事件在左侧位发生的总时间（s）
        /// </summary>
        public float LeftSleepHYPTotalTimes { set; get; }
        /// <summary>
        /// 呼吸暂停事件在左侧卧姿势发生的总时间(s)
        /// </summary>
        public float LeftSleepATotalTimes { set; get; }

        /// <summary>
        /// CA事件在右侧位发生的总时间（s）
        /// </summary>
        public float RightSleepCATotalTimes { set; get; }
        /// <summary>
        /// OA事件在右侧位发生的总时间（s）
        /// </summary>
        public float RightSleepOATotalTimes { set; get; }
        /// <summary>
        /// MA事件在右侧位发生的总时间（s）
        /// </summary>
        public float RightSleepMATotalTimes { set; get; }
        /// <summary>
        /// 低通气事件在右侧位发生的总时间（s）
        /// </summary>
        public float RightSleepHYPTotalTimes { set; get; }
        /// <summary>
        /// 呼吸暂停事件在右侧卧姿势发生的总时间(s)
        /// </summary>
        public float RightSleepATotalTimes { set; get; }

        /// <summary>
        /// CA事件在俯卧位发生的总时间（s）
        /// </summary>
        public float ProstrateSleepCATotalTimes { set; get; }
        /// <summary>
        /// OA事件在俯卧位发生的总时间（s）
        /// </summary>
        public float ProstrateSleepOATotalTimes { set; get; }
        /// <summary>
        /// MA事件在俯卧位发生的总时间（s）
        /// </summary>
        public float ProstrateSleepMATotalTimes { set; get; }
        /// <summary>
        /// 低通气事件在俯卧位发生的总时间（s）
        /// </summary>
        public float ProstrateSleepHYPTotalTimes { set; get; }
        /// <summary>
        /// 呼吸暂停事件在俯卧姿势发生的总时间(s)
        /// </summary>
        public float ProstrateSleepATotalTimes { set; get; }

        /// <summary>
        /// CA事件在坐立姿势发生的总时间(s)
        /// </summary>
        public float UpSleepCATotalTimes { set; get; }
        /// <summary>
        /// OA事件在坐立姿势发生的总时间(s)
        /// </summary>
        public float UpSleepOATotalTimes { set; get; }
        /// <summary>
        /// MA事件在坐立姿势发生的总时间(s)
        /// </summary>
        public float UpSleepMATotalTimes { set; get; }
        /// <summary>
        /// 低通气事件在坐立姿势发生的总时间(s)
        /// </summary>
        public float UpSleepHYPTotalTimes { set; get; }
        /// <summary>
        /// 呼吸暂停事件在坐立姿势发生的总时间(s)
        /// </summary>
        public float UpSleepATotalTimes { set; get; }

        /// <summary>
        /// CA事件在非仰卧姿势发生的总时间(s)
        /// </summary>
        public float UnSitSleepCATotalTimes { set; get; }
        /// <summary>
        /// OA事件在非仰卧姿势发生的总时间(s)
        /// </summary>
        public float UnSitSleepOATotalTimes { set; get; }
        /// <summary>
        /// MA事件在非仰卧姿势发生的总时间(s)
        /// </summary>
        public float UnSitSleepMATotalTimes { set; get; }
        /// <summary>
        /// 低通气事件在非仰卧姿势发生的总时间(s)
        /// </summary>
        public float UnSitSleepHYPTotalTimes { set; get; }
        /// <summary>
        /// 呼吸暂停事件在非仰卧姿势发生的总时间(s)
        /// </summary>
        public float UnSitSleepATotalTimes { set; get; }

        /// <summary>
        /// 仰卧位时所有呼吸事件的总时间(s)
        /// </summary>
        public float SitSleepAHTotalTimes { set; get; }
        /// <summary>
        /// 仰卧位时所有呼吸事件的总时间(min)
        /// </summary>
        public float SitSleepAHTotalTimesMinutes { set; get; }
        /// <summary>
        /// 侧卧位（包括左侧 右侧）时所有呼吸事件的总时间(s)
        /// </summary>
        public float LateralSleepAHTotalTimes { set; get; }
        /// <summary>
        /// 左侧卧位时所有呼吸事件的总时间(s)
        /// </summary>
        public float LeftSleepAHTotalTimes { set; get; }
        /// <summary>
        /// 左侧卧位时所有呼吸事件的总时间(min)
        /// </summary>
        public float LeftSleepAHTotalTimesMinutes { set; get; }
        /// <summary>
        /// 右侧卧位时所有呼吸事件的总时间(s)
        /// </summary>
        public float RightSleepAHTotalTimes { set; get; }
        /// <summary>
        /// 右侧卧位时所有呼吸事件的总时间(min)
        /// </summary>
        public float RightSleepAHTotalTimesMinutes { set; get; }
        /// <summary>
        /// 俯卧位时所有呼吸事件的总时间(s)
        /// </summary>
        public float ProstrateSleepAHTotalTimes { set; get; }
        /// <summary>
        /// 俯卧位时所有呼吸事件的总时间(min)
        /// </summary>
        public float ProstrateSleepAHTotalTimesMinutes { set; get; }
        /// <summary>
        /// 坐立位时所有呼吸事件的总时间(s)
        /// </summary>
        public float UpSleepAHTotalTimes { set; get; }
        /// <summary>
        /// 坐立位时所有呼吸事件的总时间(min)
        /// </summary>
        public float UpSleepAHTotalTimesMinutes { set; get; }
        /// <summary>
        /// 非仰卧位时所有呼吸事件的总时间(s)
        /// </summary>
        public float UnSitSleepAHTotalTimes { set; get; }

        /// <summary>
        /// 仰卧(s)
        /// </summary>
        public float SitSleepDesatThan100TotalTimes { set; get; }
        public float SitSleepDesatThan90TotalTimes { set; get; }
        public float SitSleepDesatThan80TotalTimes { set; get; }
        public float SitSleepDesatThan70TotalTimes { set; get; }
        public float SitSleepDesatThan60TotalTimes { set; get; }
        public float SitSleepDesatThan50TotalTimes { set; get; }
        public float SitSleepDesatThan40TotalTimes { set; get; }
        /// <summary>
        /// 俯卧(s)
        /// </summary>
        public float ProstrateSleepDesatThan100TotalTimes { set; get; }
        public float ProstrateSleepDesatThan90TotalTimes { set; get; }
        public float ProstrateSleepDesatThan80TotalTimes { set; get; }
        public float ProstrateSleepDesatThan70TotalTimes { set; get; }
        public float ProstrateSleepDesatThan60TotalTimes { set; get; }
        public float ProstrateSleepDesatThan50TotalTimes { set; get; }
        public float ProstrateSleepDesatThan40TotalTimes { set; get; }
        /// <summary>
        /// 侧卧(s)
        /// </summary>
        public float LateralSleepDesatThan100TotalTimes { set; get; }
        public float LateralSleepDesatThan90TotalTimes { set; get; }
        public float LateralSleepDesatThan80TotalTimes { set; get; }
        public float LateralSleepDesatThan70TotalTimes { set; get; }
        public float LateralSleepDesatThan60TotalTimes { set; get; }
        public float LateralSleepDesatThan50TotalTimes { set; get; }
        public float LateralSleepDesatThan40TotalTimes { set; get; }

        /// <summary>
        /// 仰卧(s)
        /// </summary>
        public float SitSleepSpo2Than100TotalTimes { set; get; }
        public float SitSleepSpo2Than90TotalTimes { set; get; }
        public float SitSleepSpo2Than80TotalTimes { set; get; }
        public float SitSleepSpo2Than70TotalTimes { set; get; }
        public float SitSleepSpo2Than60TotalTimes { set; get; }
        public float SitSleepSpo2Than50TotalTimes { set; get; }
        public float SitSleepSpo2Than40TotalTimes { set; get; }
        /// <summary>
        /// 俯卧(s)
        /// </summary>
        public float ProstrateSleepSpo2Than100TotalTimes { set; get; }
        public float ProstrateSleepSpo2Than90TotalTimes { set; get; }
        public float ProstrateSleepSpo2Than80TotalTimes { set; get; }
        public float ProstrateSleepSpo2Than70TotalTimes { set; get; }
        public float ProstrateSleepSpo2Than60TotalTimes { set; get; }
        public float ProstrateSleepSpo2Than50TotalTimes { set; get; }
        public float ProstrateSleepSpo2Than40TotalTimes { set; get; }
        /// <summary>
        /// 侧卧(s)
        /// </summary>
        public float LateralSleepSpo2Than100TotalTimes { set; get; }
        public float LateralSleepSpo2Than90TotalTimes { set; get; }
        public float LateralSleepSpo2Than80TotalTimes { set; get; }
        public float LateralSleepSpo2Than70TotalTimes { set; get; }
        public float LateralSleepSpo2Than60TotalTimes { set; get; }
        public float LateralSleepSpo2Than50TotalTimes { set; get; }
        public float LateralSleepSpo2Than40TotalTimes { set; get; }

        /// <summary>
        /// （非）仰卧位 （非）快速眼动期 最小血氧
        /// </summary>
        public float MinSpo2_Sit_R { set; get; }
        public float MinSpo2_Sit_UnR { set; get; }
        public float MinSpo2_UnSit_R { set; get; }
        public float MinSpo2_UnSit_UnR { set; get; }
        /// <summary>
        /// （非）仰卧位 （非）快速眼动期 平均血氧
        /// </summary>
        public float AveSpo2_Sit_R { set; get; }
        public float AveSpo2_Sit_UnR { set; get; }
        public float AveSpo2_UnSit_R { set; get; }
        public float AveSpo2_UnSit_UnR { set; get; }
        /// <summary>
        /// （非）仰卧位 （非）快速眼动期 平均氧减
        /// </summary>
        public float AveSpo2Reduce_Sit_R { set; get; }
        public float AveSpo2Reduce_Sit_UnR { set; get; }
        public float AveSpo2Reduce_UnSit_R { set; get; }
        public float AveSpo2Reduce_UnSit_UnR { set; get; }
        /// <summary>
        /// 睡眠期间 平均氧减
        /// </summary>
        public float AveSpo2Reduce_TST { set; get; }

        #region 鼾声相关
        /// <summary>
        /// 鼾声总次数
        /// </summary>
        public float SnoreTotalCount { set; get; }
        /// <summary>
        /// 鼾声指数
        /// </summary>
        public float SnoreIndex { set; get; }
        /// <summary>
        /// 仰卧位鼾声次数
        /// </summary>
        public float SitSleepSnoreCount { set; get; }
        /// <summary>
        /// 仰卧位鼾声指数
        /// </summary>
        public float SitSleepSnoreIndex { set; get; }
        /// <summary>
        /// 左侧位鼾声次数
        /// </summary>
        public float LeftSleepSnoreCount { set; get; }
        /// <summary>
        /// 左侧位鼾声指数
        /// </summary>
        public float LeftSleepSnoreIndex { set; get; }
        /// <summary>
        /// 右侧位鼾声次数
        /// </summary>
        public float RightSleepSnoreCount { set; get; }
        /// <summary>
        /// 右侧位鼾声指数
        /// </summary>
        public float RightSleepSnoreIndex { set; get; }
        /// <summary>
        /// 俯卧位鼾声次数
        /// </summary>
        public float ProstrateSleepSnoreCount { set; get; }
        /// <summary>
        /// 俯卧位鼾声指数
        /// </summary>
        public float ProstrateSleepSnoreIndex { set; get; }
        /// <summary>
        /// 坐立位鼾声次数
        /// </summary>
        public float UpSleepSnoreCount { set; get; }
        /// <summary>
        /// 坐立位鼾声指数
        /// </summary>
        public float UpSleepSnoreIndex { set; get; }
        /// <summary>
        /// 非仰卧位鼾声次数
        /// </summary>
        public float UnSitSleepSnoreCount { set; get; }
        /// <summary>
        /// 非仰卧位鼾声指数
        /// </summary>
        public float UnSitSleepSnoreIndex { set; get; }
        /// <summary>
        /// 侧卧位（左侧+右侧）鼾声次数
        /// </summary>
        public float LateralSleepSnoreCount { set; get; }
        /// <summary>
        /// 侧卧位（左侧+右侧）鼾声指数
        /// </summary>
        public float LateralSleepSnoreIndex { set; get; }
        #endregion

    }
}
