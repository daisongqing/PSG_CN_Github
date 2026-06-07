using pSystem.Interface.Util;
using System;

namespace AwareTec.Polysmith.DataBaseCom
{
    /// <summary>
    /// 睡眠统计结果
    /// </summary>
    public class Doc_SleepResult : ITable
    {
        public Doc_SleepResult()
        {
            m_N1SleepLatencyFromLightOn = -1;
            m_N2SleepLatencyFromLightOn = -1;
            m_N3SleepLatencyFromLightOn = -1;
            m_REMSleepLatencyFromLightOn = -1;
            StartRecordTime = EndRecordTime = DateTime.Now;
            strAwakeningTimes = "正常";
            strSleepEfficiency = "正常";
            strSleepLatency = "正常";
            strArousalCount = "正常";
            strREMSleepLatency = "正常";
            strN1Ratio = "正常";
            strN2Ratio = "正常";
            strN3Ratio = "正常";
            strRRatio = "正常";
            strWakeAndSleepOK = "符合□ 不符合■";
            MultipleSleepSpanRange = "1.5-2";
            AlphaWaveFreqRange = "8.5-9.5";
            AlphaPeakValue = 50;
        }
        #region 私有成员
        private string m_GUID = "";
        private bool m_GUID_ini = false;
        private DateTime m_LightOffTime = default(DateTime);
        private bool m_LightOffTime_ini = false;
        private DateTime m_LightOnTime = default(DateTime);
        private bool m_LightOnTime_ini = false;
        private float m_TotalSleepTime = 0;
        private bool m_TotalSleepTime_ini = false;
        private float m_SleepEfficiency = 0;
        private bool m_SleepEfficiency_ini = false;
        private float m_WakeAfterSleepTimes = 0;
        private bool m_WakeAfterSleepTimes_ini = false;
        private float m_AwakeningTimes = 0;
        private bool m_AwakeningTimes_ini = false;
        private float m_SleepPeriodTime = 0;
        private bool m_SleepPeriodTime_ini = false;
        private float m_ArousalCount = 0;
        private bool m_ArousalCount_ini = false;
        private float m_TimeInBed = 0;
        private bool m_TimeInBed_ini = false;
        private float m_MicroArousalCount = 0;
        private bool m_MicroArousalCount_ini = false;
        private float m_MicroArousalIndex = 0;
        private bool m_MicroArousalIndex_ini = false;
        private float m_WKOfSleepDuration = 0;
        private bool m_WKOfSleepDuration_ini = false;
        private float m_WKOfSleepTimeInBedPencent = 0;
        private bool m_WKOfSleepTimeInBedPencent_ini = false;
        private float m_WKOfSleepTotalTimePencent = 0;
        private bool m_WKOfSleepTotalTimePencent_ini = false;
        private float m_WKOfSleepPeriodTimePencent = 0;
        private bool m_WKOfSleepPeriodTimePencent_ini = false;
        private float m_REMOfSleepDuration = 0;
        private bool m_REMOfSleepDuration_ini = false;
        private float m_REMOfSleepTimeInBedPencent = 0;
        private bool m_REMOfSleepTimeInBedPencent_ini = false;
        private float m_REMOfSleepTotalTimePencent = 0;
        private bool m_REMOfSleepTotalTimePencent_ini = false;
        private float m_REMOfSleepPeriodTimePencent = 0;
        private bool m_REMOfSleepPeriodTimePencent_ini = false;
        private float m_N1OfSleepDuration = 0;
        private bool m_N1OfSleepDuration_ini = false;
        private float m_N1OfSleepTimeInBedPencent = 0;
        private bool m_N1OfSleepTimeInBedPencent_ini = false;
        private float m_N1OfSleepTotalTimePencent = 0;
        private bool m_N1OfSleepTotalTimePencent_ini = false;
        private float m_N1OfSleepPeriodTimePencent = 0;
        private bool m_N1OfSleepPeriodTimePencent_ini = false;
        private float m_N2OfSleepDuration = 0;
        private bool m_N2OfSleepDuration_ini = false;
        private float m_N2OfSleepTimeInBedPencent = 0;
        private bool m_N2OfSleepTimeInBedPencent_ini = false;
        private float m_N2OfSleepTotalTimePencent = 0;
        private bool m_N2OfSleepTotalTimePencent_ini = false;
        private float m_N2OfSleepPeriodTimePencent = 0;
        private bool m_N2OfSleepPeriodTimePencent_ini = false;
        private float m_N3OfSleepDuration = 0;
        private bool m_N3OfSleepDuration_ini = false;
        private float m_N3OfSleepTimeInBedPencent = 0;
        private bool m_N3OfSleepTimeInBedPencent_ini = false;
        private float m_N3OfSleepTotalTimePencent = 0;
        private bool m_N3OfSleepTotalTimePencent_ini = false;
        private float m_N3OfSleepPeriodTimePencent = 0;
        private bool m_N3OfSleepPeriodTimePencent_ini = false;
        private float m_SleepLatencyTimesFromLightOn = 0;
        private bool m_SleepLatencyTimesFromLightOn_ini = false;
        private float m_REMSleepLatencyFromLightOn = 0;
        private bool m_REMSleepLatencyFromLightOn_ini = false;
        private float m_N1SleepLatencyFromLightOn = 0;
        private bool m_N1SleepLatencyFromLightOn_ini = false;
        private float m_N2SleepLatencyFromLightOn = 0;
        private bool m_N2SleepLatencyFromLightOn_ini = false;
        private float m_N3SleepLatencyFromLightOn = 0;
        private bool m_N3SleepLatencyFromLightOn_ini = false;
        private float m_REMSleepLatencyFromSleepOnset = 0;
        private bool m_REMSleepLatencyFromSleepOnset_ini = false;
        private float m_N1SleepLatencyFromSleepOnset = 0;
        private bool m_N1SleepLatencyFromSleepOnset_ini = false;
        private float m_N2SleepLatencyFromSleepOnset = 0;
        private bool m_N2SleepLatencyFromSleepOnset_ini = false;
        private float m_N3SleepLatencyFromSleepOnset = 0;
        private bool m_N3SleepLatencyFromSleepOnset_ini = false;
        private bool m_WakeAndSleepPercentOk = false;
        private bool m_WakeAndSleepPercentOk_ini = false;
        private float m_LatencyOfPersistentSleep = 0;
        private bool m_LatencyOfPersistentSleep_ini = false;
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
        public DateTime LightOffTime
        {
            set
            {
                m_LightOffTime = value;
                m_LightOffTime_ini = true;
            }
            get
            {
                return m_LightOffTime;
            }
        }
        public DateTime LightOnTime
        {
            set
            {
                m_LightOnTime = value;
                m_LightOnTime_ini = true;
            }
            get
            {
                return m_LightOnTime;
            }
        }
        /// <summary>
        /// TST
        /// </summary>
        public float TotalSleepTime
        {
            set
            {
                m_TotalSleepTime = value;
                m_TotalSleepTime_ini = true;
            }
            get
            {
                return m_TotalSleepTime;
            }
        }
        public float SleepEfficiency
        {
            set
            {
                m_SleepEfficiency = value;
                m_SleepEfficiency_ini = true;
                if (value > 100)
                {
                    value = 100;
                }
                if (m_SleepEfficiency > 85)
                    strSleepEfficiency = "正常";
                else
                    strSleepEfficiency = "降低";
            }
            get
            {
                return m_SleepEfficiency;
            }
        }
        public float WakeAfterSleepTimes
        {
            set
            {
                m_WakeAfterSleepTimes = value;
                m_WakeAfterSleepTimes_ini = true;
            }
            get
            {
                return m_WakeAfterSleepTimes;
            }
        }
        /// <summary>
        /// 觉醒时间
        /// </summary>
        public float AwakeningTimes
        {
            set
            {
                m_AwakeningTimes = value;
                m_AwakeningTimes_ini = true;
                if (m_AwakeningTimes < 30)
                    strAwakeningTimes = "正常";
                else
                    strAwakeningTimes = "增多";
            }
            get
            {
                return m_AwakeningTimes;
            }
        }
        /// <summary>
        /// SPT(TST期间所有非W期的时间总和)
        /// </summary>
        public float SleepPeriodTime
        {
            set
            {
                m_SleepPeriodTime = value;
                m_SleepPeriodTime_ini = true;
            }
            get
            {
                return m_SleepPeriodTime;
            }
        }
        /// <summary>
        /// 觉醒次数
        /// </summary>
        public float ArousalCount
        {
            set
            {
                m_ArousalCount = value;
                m_ArousalCount_ini = true;
                if (m_ArousalCount < 4)
                    strArousalCount = "正常";
                else
                    strArousalCount = "增多";
            }
            get
            {
                return m_ArousalCount;
            }
        }
        /// <summary>
        /// 开关灯间的时间即为卧床时间（TIB）
        /// </summary>
        public float TimeInBed
        {
            set
            {
                m_TimeInBed = value;
                m_TimeInBed_ini = true;
            }
            get
            {
                return m_TimeInBed;
            }
        }
        public float MicroArousalCount
        {
            set
            {
                m_MicroArousalCount = value;
                m_MicroArousalCount_ini = true;
            }
            get
            {
                return m_MicroArousalCount;
            }
        }
        public float MicroArousalIndex
        {
            set
            {
                m_MicroArousalIndex = value;
                m_MicroArousalIndex_ini = true;
            }
            get
            {
                return m_MicroArousalIndex;
            }
        }
        public float WKOfSleepDuration
        {
            set
            {
                m_WKOfSleepDuration = value;
                m_WKOfSleepDuration_ini = true;
            }
            get
            {
                return m_WKOfSleepDuration;
            }
        }
        public float WKOfSleepTimeInBedPencent
        {
            set
            {
                m_WKOfSleepTimeInBedPencent = value;
                m_WKOfSleepTimeInBedPencent_ini = true;
            }
            get
            {
                return m_WKOfSleepTimeInBedPencent;
            }
        }
        public float WKOfSleepTotalTimePencent
        {
            set
            {
                m_WKOfSleepTotalTimePencent = value;
                m_WKOfSleepTotalTimePencent_ini = true;
            }
            get
            {
                return m_WKOfSleepTotalTimePencent;
            }
        }
        public float WKOfSleepPeriodTimePencent
        {
            set
            {
                m_WKOfSleepPeriodTimePencent = value;
                m_WKOfSleepPeriodTimePencent_ini = true;
            }
            get
            {
                return m_WKOfSleepPeriodTimePencent;
            }
        }
        public float REMOfSleepDuration
        {
            set
            {
                m_REMOfSleepDuration = value;
                m_REMOfSleepDuration_ini = true;
            }
            get
            {
                return m_REMOfSleepDuration;
            }
        }
        public float REMOfSleepTimeInBedPencent
        {
            set
            {
                m_REMOfSleepTimeInBedPencent = value;
                m_REMOfSleepTimeInBedPencent_ini = true;
            }
            get
            {
                return m_REMOfSleepTimeInBedPencent;
            }
        }
        public float REMOfSleepTotalTimePencent
        {
            set
            {
                m_REMOfSleepTotalTimePencent = value;
                m_REMOfSleepTotalTimePencent_ini = true;
                if (m_REMOfSleepTotalTimePencent < 20)
                    strRRatio = "降低";
                else if (m_REMOfSleepTotalTimePencent <= 25)
                    strRRatio = "正常";
                else
                    strRRatio = "增高";
            }
            get
            {
                return m_REMOfSleepTotalTimePencent;
            }
        }
        public float REMOfSleepPeriodTimePencent
        {
            set
            {
                m_REMOfSleepPeriodTimePencent = value;
                m_REMOfSleepPeriodTimePencent_ini = true;
            }
            get
            {
                return m_REMOfSleepPeriodTimePencent;
            }
        }
        public float N1OfSleepDuration
        {
            set
            {
                m_N1OfSleepDuration = value;
                m_N1OfSleepDuration_ini = true;
            }
            get
            {
                return m_N1OfSleepDuration;
            }
        }
        public float N1OfSleepTimeInBedPencent
        {
            set
            {
                m_N1OfSleepTimeInBedPencent = value;
                m_N1OfSleepTimeInBedPencent_ini = true;
            }
            get
            {
                return m_N1OfSleepTimeInBedPencent;
            }
        }
        public float N1OfSleepTotalTimePencent
        {
            set
            {
                m_N1OfSleepTotalTimePencent = value;
                m_N1OfSleepTotalTimePencent_ini = true;
                if (m_N1OfSleepTotalTimePencent < 2)
                    strN1Ratio = "降低";
                else if (m_N1OfSleepTotalTimePencent <= 5)
                    strN1Ratio = "正常";
                else
                    strN1Ratio = "增高";
            }
            get
            {
                return m_N1OfSleepTotalTimePencent;
            }
        }
        public float N1OfSleepPeriodTimePencent
        {
            set
            {
                m_N1OfSleepPeriodTimePencent = value;
                m_N1OfSleepPeriodTimePencent_ini = true;
            }
            get
            {
                return m_N1OfSleepPeriodTimePencent;
            }
        }
        public float N2OfSleepDuration
        {
            set
            {
                m_N2OfSleepDuration = value;
                m_N2OfSleepDuration_ini = true;
            }
            get
            {
                return m_N2OfSleepDuration;
            }
        }
        public float N2OfSleepTimeInBedPencent
        {
            set
            {
                m_N2OfSleepTimeInBedPencent = value;
                m_N2OfSleepTimeInBedPencent_ini = true;
            }
            get
            {
                return m_N2OfSleepTimeInBedPencent;
            }
        }
        public float N2OfSleepTotalTimePencent
        {
            set
            {
                m_N2OfSleepTotalTimePencent = value;
                m_N2OfSleepTotalTimePencent_ini = true;
                if (m_N2OfSleepTotalTimePencent < 45)
                    strN2Ratio = "降低";
                else if (m_N2OfSleepTotalTimePencent <= 55)
                    strN2Ratio = "正常";
                else
                    strN2Ratio = "增高";

            }
            get
            {
                return m_N2OfSleepTotalTimePencent;
            }
        }
        public float N2OfSleepPeriodTimePencent
        {
            set
            {
                m_N2OfSleepPeriodTimePencent = value;
                m_N2OfSleepPeriodTimePencent_ini = true;
            }
            get
            {
                return m_N2OfSleepPeriodTimePencent;
            }
        }
        public float N3OfSleepDuration
        {
            set
            {
                m_N3OfSleepDuration = value;
                m_N3OfSleepDuration_ini = true;
            }
            get
            {
                return m_N3OfSleepDuration;
            }
        }
        public float N3OfSleepTimeInBedPencent
        {
            set
            {
                m_N3OfSleepTimeInBedPencent = value;
                m_N3OfSleepTimeInBedPencent_ini = true;
            }
            get
            {
                return m_N3OfSleepTimeInBedPencent;
            }
        }
        public float N3OfSleepTotalTimePencent
        {
            set
            {
                m_N3OfSleepTotalTimePencent = value;
                m_N3OfSleepTotalTimePencent_ini = true;
                if (m_N3OfSleepTotalTimePencent < 13)
                    strN3Ratio = "降低";
                else if (m_N3OfSleepTotalTimePencent <= 23)
                    strN3Ratio = "正常";
                else
                    strN3Ratio = "增高";
            }
            get
            {
                return m_N3OfSleepTotalTimePencent;
            }
        }
        public float N3OfSleepPeriodTimePencent
        {
            set
            {
                m_N3OfSleepPeriodTimePencent = value;
                m_N3OfSleepPeriodTimePencent_ini = true;
            }
            get
            {
                return m_N3OfSleepPeriodTimePencent;
            }
        }
        public float SleepLatencyTimesFromLightOn
        {
            set
            {
                m_SleepLatencyTimesFromLightOn = value;
                m_SleepLatencyTimesFromLightOn_ini = true;
            }
            get
            {
                return m_SleepLatencyTimesFromLightOn;
            }
        }
        public float REMSleepLatencyFromLightOn
        {
            set
            {
                m_REMSleepLatencyFromLightOn = value;
                m_REMSleepLatencyFromLightOn_ini = true;
                if (m_REMSleepLatencyFromLightOn < 70)
                    strREMSleepLatency = "缩短";
                else if (m_REMSleepLatencyFromLightOn <= 90)
                    strREMSleepLatency = "正常";
                else
                    strREMSleepLatency = "延长";
            }
            get
            {
                return m_REMSleepLatencyFromLightOn;
            }
        }
        public float N1SleepLatencyFromLightOn
        {
            set
            {
                m_N1SleepLatencyFromLightOn = value;
                m_N1SleepLatencyFromLightOn_ini = true;
            }
            get
            {
                return m_N1SleepLatencyFromLightOn;
            }
        }
        public float N2SleepLatencyFromLightOn
        {
            set
            {
                m_N2SleepLatencyFromLightOn = value;
                m_N2SleepLatencyFromLightOn_ini = true;
            }
            get
            {
                return m_N2SleepLatencyFromLightOn;
            }
        }
        public float N3SleepLatencyFromLightOn
        {
            set
            {
                m_N3SleepLatencyFromLightOn = value;
                m_N3SleepLatencyFromLightOn_ini = true;
            }
            get
            {
                return m_N3SleepLatencyFromLightOn;
            }
        }
        public float REMSleepLatencyFromSleepOnset
        {
            set
            {
                m_REMSleepLatencyFromSleepOnset = value;
                m_REMSleepLatencyFromSleepOnset_ini = true;
            }
            get
            {
                return m_REMSleepLatencyFromSleepOnset;
            }
        }
        public float N1SleepLatencyFromSleepOnset
        {
            set
            {
                m_N1SleepLatencyFromSleepOnset = value;
                m_N1SleepLatencyFromSleepOnset_ini = true;
            }
            get
            {
                return m_N1SleepLatencyFromSleepOnset;
            }
        }
        public float N2SleepLatencyFromSleepOnset
        {
            set
            {
                m_N2SleepLatencyFromSleepOnset = value;
                m_N2SleepLatencyFromSleepOnset_ini = true;
            }
            get
            {
                return m_N2SleepLatencyFromSleepOnset;
            }
        }
        public float N3SleepLatencyFromSleepOnset
        {
            set
            {
                m_N3SleepLatencyFromSleepOnset = value;
                m_N3SleepLatencyFromSleepOnset_ini = true;
            }
            get
            {
                return m_N3SleepLatencyFromSleepOnset;
            }
        }
        public bool WakeAndSleepPercentOk
        {
            set
            {
                m_WakeAndSleepPercentOk = value;
                m_WakeAndSleepPercentOk_ini = true;
            }
            get
            {
                if ((m_REMOfSleepTotalTimePencent >= 0.1 && m_REMOfSleepTotalTimePencent <= 0.3) && (m_N1OfSleepTotalTimePencent>=0.05&& m_N1OfSleepTotalTimePencent<=0.1)&&(m_N2OfSleepTotalTimePencent >= 0.5&& m_N2OfSleepTotalTimePencent<=0.6) && (m_N3OfSleepTotalTimePencent >= 0.15 && m_N3OfSleepTotalTimePencent <= 0.2))
                {
                    m_WakeAndSleepPercentOk = true;
                    strWakeAndSleepOK = "符合□ 不符合■";
                }
                else
                {
                    m_WakeAndSleepPercentOk = false;
                    strWakeAndSleepOK = "符合■ 不符合□";
                }
                return m_WakeAndSleepPercentOk;
            }
        }
        public float LatencyOfPersistentSleep
        {
            get
            {
                return this.m_LatencyOfPersistentSleep;
            }
            set
            {
                this.m_LatencyOfPersistentSleep = value;
                this.m_LatencyOfPersistentSleep_ini = true;
                if (m_LatencyOfPersistentSleep < 10)
                    strSleepLatency = "缩短";
                else if (m_LatencyOfPersistentSleep <= 30)
                    strSleepLatency = "正常";
                else
                    strSleepLatency = "延长";
            }
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
            if (this.m_LightOffTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "LightOffTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_LightOffTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_LightOnTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "LightOnTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_LightOnTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_TotalSleepTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "TotalSleepTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_TotalSleepTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_SleepEfficiency_ini)
            {
                def = string.Format("{0}{2}{1}", def, "SleepEfficiency", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_SleepEfficiency, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_WakeAfterSleepTimes_ini)
            {
                def = string.Format("{0}{2}{1}", def, "WakeAfterSleepTimes", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_WakeAfterSleepTimes, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_AwakeningTimes_ini)
            {
                def = string.Format("{0}{2}{1}", def, "AwakeningTimes", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_AwakeningTimes, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_SleepPeriodTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "SleepPeriodTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_SleepPeriodTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ArousalCount_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ArousalCount", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ArousalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_TimeInBed_ini)
            {
                def = string.Format("{0}{2}{1}", def, "TimeInBed", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_TimeInBed, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MicroArousalCount_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MicroArousalCount", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_MicroArousalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MicroArousalIndex_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MicroArousalIndex", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_MicroArousalIndex, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_WKOfSleepDuration_ini)
            {
                def = string.Format("{0}{2}{1}", def, "WKOfSleepDuration", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_WKOfSleepDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_WKOfSleepTimeInBedPencent_ini)
            {
                def = string.Format("{0}{2}{1}", def, "WKOfSleepTimeInBedPencent", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_WKOfSleepTimeInBedPencent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_WKOfSleepTotalTimePencent_ini)
            {
                def = string.Format("{0}{2}{1}", def, "WKOfSleepTotalTimePencent", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_WKOfSleepTotalTimePencent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_WKOfSleepPeriodTimePencent_ini)
            {
                def = string.Format("{0}{2}{1}", def, "WKOfSleepPeriodTimePencent", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_WKOfSleepPeriodTimePencent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_REMOfSleepDuration_ini)
            {
                def = string.Format("{0}{2}{1}", def, "REMOfSleepDuration", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_REMOfSleepDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_REMOfSleepTimeInBedPencent_ini)
            {
                def = string.Format("{0}{2}{1}", def, "REMOfSleepTimeInBedPencent", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_REMOfSleepTimeInBedPencent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_REMOfSleepTotalTimePencent_ini)
            {
                def = string.Format("{0}{2}{1}", def, "REMOfSleepTotalTimePencent", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_REMOfSleepTotalTimePencent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_REMOfSleepPeriodTimePencent_ini)
            {
                def = string.Format("{0}{2}{1}", def, "REMOfSleepPeriodTimePencent", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_REMOfSleepPeriodTimePencent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_N1OfSleepDuration_ini)
            {
                def = string.Format("{0}{2}{1}", def, "N1OfSleepDuration", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_N1OfSleepDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_N1OfSleepTimeInBedPencent_ini)
            {
                def = string.Format("{0}{2}{1}", def, "N1OfSleepTimeInBedPencent", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_N1OfSleepTimeInBedPencent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_N1OfSleepTotalTimePencent_ini)
            {
                def = string.Format("{0}{2}{1}", def, "N1OfSleepTotalTimePencent", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_N1OfSleepTotalTimePencent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_N1OfSleepPeriodTimePencent_ini)
            {
                def = string.Format("{0}{2}{1}", def, "N1OfSleepPeriodTimePencent", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_N1OfSleepPeriodTimePencent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_N2OfSleepDuration_ini)
            {
                def = string.Format("{0}{2}{1}", def, "N2OfSleepDuration", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_N2OfSleepDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_N2OfSleepTimeInBedPencent_ini)
            {
                def = string.Format("{0}{2}{1}", def, "N2OfSleepTimeInBedPencent", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_N2OfSleepTimeInBedPencent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_N2OfSleepTotalTimePencent_ini)
            {
                def = string.Format("{0}{2}{1}", def, "N2OfSleepTotalTimePencent", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_N2OfSleepTotalTimePencent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_N2OfSleepPeriodTimePencent_ini)
            {
                def = string.Format("{0}{2}{1}", def, "N2OfSleepPeriodTimePencent", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_N2OfSleepPeriodTimePencent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_N3OfSleepDuration_ini)
            {
                def = string.Format("{0}{2}{1}", def, "N3OfSleepDuration", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_N3OfSleepDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_N3OfSleepTimeInBedPencent_ini)
            {
                def = string.Format("{0}{2}{1}", def, "N3OfSleepTimeInBedPencent", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_N3OfSleepTimeInBedPencent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_N3OfSleepTotalTimePencent_ini)
            {
                def = string.Format("{0}{2}{1}", def, "N3OfSleepTotalTimePencent", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_N3OfSleepTotalTimePencent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_N3OfSleepPeriodTimePencent_ini)
            {
                def = string.Format("{0}{2}{1}", def, "N3OfSleepPeriodTimePencent", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_N3OfSleepPeriodTimePencent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_SleepLatencyTimesFromLightOn_ini)
            {
                def = string.Format("{0}{2}{1}", def, "SleepLatencyTimesFromLightOn", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_SleepLatencyTimesFromLightOn, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_REMSleepLatencyFromLightOn_ini)
            {
                def = string.Format("{0}{2}{1}", def, "REMSleepLatencyFromLightOn", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_REMSleepLatencyFromLightOn, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_N1SleepLatencyFromLightOn_ini)
            {
                def = string.Format("{0}{2}{1}", def, "N1SleepLatencyFromLightOn", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_N1SleepLatencyFromLightOn, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_N2SleepLatencyFromLightOn_ini)
            {
                def = string.Format("{0}{2}{1}", def, "N2SleepLatencyFromLightOn", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_N2SleepLatencyFromLightOn, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_N3SleepLatencyFromLightOn_ini)
            {
                def = string.Format("{0}{2}{1}", def, "N3SleepLatencyFromLightOn", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_N3SleepLatencyFromLightOn, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_REMSleepLatencyFromSleepOnset_ini)
            {
                def = string.Format("{0}{2}{1}", def, "REMSleepLatencyFromSleepOnset", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_REMSleepLatencyFromSleepOnset, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_N1SleepLatencyFromSleepOnset_ini)
            {
                def = string.Format("{0}{2}{1}", def, "N1SleepLatencyFromSleepOnset", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_N1SleepLatencyFromSleepOnset, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_N2SleepLatencyFromSleepOnset_ini)
            {
                def = string.Format("{0}{2}{1}", def, "N2SleepLatencyFromSleepOnset", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_N2SleepLatencyFromSleepOnset, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_N3SleepLatencyFromSleepOnset_ini)
            {
                def = string.Format("{0}{2}{1}", def, "N3SleepLatencyFromSleepOnset", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_N3SleepLatencyFromSleepOnset, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_WakeAndSleepPercentOk_ini)
            {
                def = string.Format("{0}{2}{1}", def, "WakeAndSleepPercentOk", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_WakeAndSleepPercentOk ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_LatencyOfPersistentSleep_ini)
            {
                def = string.Format("{0}{2}{1}", def, "LatencyOfPersistentSleep", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_LatencyOfPersistentSleep, Mulitcase ? "," : "");
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
            if (m_LightOffTime_ini)
            {
                returnstr = string.Format("{0}LightOffTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_LightOffTime);
                Mulitcase = true;
            }
            if (m_LightOnTime_ini)
            {
                returnstr = string.Format("{0}LightOnTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_LightOnTime);
                Mulitcase = true;
            }
            if (m_TotalSleepTime_ini)
            {
                returnstr = string.Format("{0}TotalSleepTime={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_TotalSleepTime);
                Mulitcase = true;
            }
            if (m_SleepEfficiency_ini)
            {
                returnstr = string.Format("{0}SleepEfficiency={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_SleepEfficiency);
                Mulitcase = true;
            }
            if (m_WakeAfterSleepTimes_ini)
            {
                returnstr = string.Format("{0}WakeAfterSleepTimes={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_WakeAfterSleepTimes);
                Mulitcase = true;
            }
            if (m_AwakeningTimes_ini)
            {
                returnstr = string.Format("{0}AwakeningTimes={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_AwakeningTimes);
                Mulitcase = true;
            }
            if (m_SleepPeriodTime_ini)
            {
                returnstr = string.Format("{0}SleepPeriodTime={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_SleepPeriodTime);
                Mulitcase = true;
            }
            if (m_ArousalCount_ini)
            {
                returnstr = string.Format("{0}ArousalCount={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ArousalCount);
                Mulitcase = true;
            }
            if (m_TimeInBed_ini)
            {
                returnstr = string.Format("{0}TimeInBed={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_TimeInBed);
                Mulitcase = true;
            }
            if (m_MicroArousalCount_ini)
            {
                returnstr = string.Format("{0}MicroArousalCount={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MicroArousalCount);
                Mulitcase = true;
            }
            if (m_MicroArousalIndex_ini)
            {
                returnstr = string.Format("{0}MicroArousalIndex={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MicroArousalIndex);
                Mulitcase = true;
            }
            if (m_WKOfSleepDuration_ini)
            {
                returnstr = string.Format("{0}WKOfSleepDuration={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_WKOfSleepDuration);
                Mulitcase = true;
            }
            if (m_WKOfSleepTimeInBedPencent_ini)
            {
                returnstr = string.Format("{0}WKOfSleepTimeInBedPencent={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_WKOfSleepTimeInBedPencent);
                Mulitcase = true;
            }
            if (m_WKOfSleepTotalTimePencent_ini)
            {
                returnstr = string.Format("{0}WKOfSleepTotalTimePencent={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_WKOfSleepTotalTimePencent);
                Mulitcase = true;
            }
            if (m_WKOfSleepPeriodTimePencent_ini)
            {
                returnstr = string.Format("{0}WKOfSleepPeriodTimePencent={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_WKOfSleepPeriodTimePencent);
                Mulitcase = true;
            }
            if (m_REMOfSleepDuration_ini)
            {
                returnstr = string.Format("{0}REMOfSleepDuration={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_REMOfSleepDuration);
                Mulitcase = true;
            }
            if (m_REMOfSleepTimeInBedPencent_ini)
            {
                returnstr = string.Format("{0}REMOfSleepTimeInBedPencent={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_REMOfSleepTimeInBedPencent);
                Mulitcase = true;
            }
            if (m_REMOfSleepTotalTimePencent_ini)
            {
                returnstr = string.Format("{0}REMOfSleepTotalTimePencent={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_REMOfSleepTotalTimePencent);
                Mulitcase = true;
            }
            if (m_REMOfSleepPeriodTimePencent_ini)
            {
                returnstr = string.Format("{0}REMOfSleepPeriodTimePencent={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_REMOfSleepPeriodTimePencent);
                Mulitcase = true;
            }
            if (m_N1OfSleepDuration_ini)
            {
                returnstr = string.Format("{0}N1OfSleepDuration={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_N1OfSleepDuration);
                Mulitcase = true;
            }
            if (m_N1OfSleepTimeInBedPencent_ini)
            {
                returnstr = string.Format("{0}N1OfSleepTimeInBedPencent={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_N1OfSleepTimeInBedPencent);
                Mulitcase = true;
            }
            if (m_N1OfSleepTotalTimePencent_ini)
            {
                returnstr = string.Format("{0}N1OfSleepTotalTimePencent={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_N1OfSleepTotalTimePencent);
                Mulitcase = true;
            }
            if (m_N1OfSleepPeriodTimePencent_ini)
            {
                returnstr = string.Format("{0}N1OfSleepPeriodTimePencent={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_N1OfSleepPeriodTimePencent);
                Mulitcase = true;
            }
            if (m_N2OfSleepDuration_ini)
            {
                returnstr = string.Format("{0}N2OfSleepDuration={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_N2OfSleepDuration);
                Mulitcase = true;
            }
            if (m_N2OfSleepTimeInBedPencent_ini)
            {
                returnstr = string.Format("{0}N2OfSleepTimeInBedPencent={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_N2OfSleepTimeInBedPencent);
                Mulitcase = true;
            }
            if (m_N2OfSleepTotalTimePencent_ini)
            {
                returnstr = string.Format("{0}N2OfSleepTotalTimePencent={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_N2OfSleepTotalTimePencent);
                Mulitcase = true;
            }
            if (m_N2OfSleepPeriodTimePencent_ini)
            {
                returnstr = string.Format("{0}N2OfSleepPeriodTimePencent={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_N2OfSleepPeriodTimePencent);
                Mulitcase = true;
            }
            if (m_N3OfSleepDuration_ini)
            {
                returnstr = string.Format("{0}N3OfSleepDuration={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_N3OfSleepDuration);
                Mulitcase = true;
            }
            if (m_N3OfSleepTimeInBedPencent_ini)
            {
                returnstr = string.Format("{0}N3OfSleepTimeInBedPencent={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_N3OfSleepTimeInBedPencent);
                Mulitcase = true;
            }
            if (m_N3OfSleepTotalTimePencent_ini)
            {
                returnstr = string.Format("{0}N3OfSleepTotalTimePencent={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_N3OfSleepTotalTimePencent);
                Mulitcase = true;
            }
            if (m_N3OfSleepPeriodTimePencent_ini)
            {
                returnstr = string.Format("{0}N3OfSleepPeriodTimePencent={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_N3OfSleepPeriodTimePencent);
                Mulitcase = true;
            }
            if (m_SleepLatencyTimesFromLightOn_ini)
            {
                returnstr = string.Format("{0}SleepLatencyTimesFromLightOn={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_SleepLatencyTimesFromLightOn);
                Mulitcase = true;
            }
            if (m_REMSleepLatencyFromLightOn_ini)
            {
                returnstr = string.Format("{0}REMSleepLatencyFromLightOn={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_REMSleepLatencyFromLightOn);
                Mulitcase = true;
            }
            if (m_N1SleepLatencyFromLightOn_ini)
            {
                returnstr = string.Format("{0}N1SleepLatencyFromLightOn={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_N1SleepLatencyFromLightOn);
                Mulitcase = true;
            }
            if (m_N2SleepLatencyFromLightOn_ini)
            {
                returnstr = string.Format("{0}N2SleepLatencyFromLightOn={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_N2SleepLatencyFromLightOn);
                Mulitcase = true;
            }
            if (m_N3SleepLatencyFromLightOn_ini)
            {
                returnstr = string.Format("{0}N3SleepLatencyFromLightOn={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_N3SleepLatencyFromLightOn);
                Mulitcase = true;
            }
            if (m_REMSleepLatencyFromSleepOnset_ini)
            {
                returnstr = string.Format("{0}REMSleepLatencyFromSleepOnset={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_REMSleepLatencyFromSleepOnset);
                Mulitcase = true;
            }
            if (m_N1SleepLatencyFromSleepOnset_ini)
            {
                returnstr = string.Format("{0}N1SleepLatencyFromSleepOnset={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_N1SleepLatencyFromSleepOnset);
                Mulitcase = true;
            }
            if (m_N2SleepLatencyFromSleepOnset_ini)
            {
                returnstr = string.Format("{0}N2SleepLatencyFromSleepOnset={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_N2SleepLatencyFromSleepOnset);
                Mulitcase = true;
            }
            if (m_N3SleepLatencyFromSleepOnset_ini)
            {
                returnstr = string.Format("{0}N3SleepLatencyFromSleepOnset={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_N3SleepLatencyFromSleepOnset);
                Mulitcase = true;
            }
            if (m_WakeAndSleepPercentOk_ini)
            {
                returnstr = string.Format("{0}WakeAndSleepPercentOk={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_WakeAndSleepPercentOk ? 1 : 0);
                Mulitcase = true;
            }
            if (m_LatencyOfPersistentSleep_ini)
            {
                returnstr = string.Format("{0}LatencyOfPersistentSleep={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_LatencyOfPersistentSleep);
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
            if (m_LightOffTime_ini)
            {
                def = string.Format("{0}{2}LightOffTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_LightOffTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_LightOnTime_ini)
            {
                def = string.Format("{0}{2}LightOnTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_LightOnTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_TotalSleepTime_ini)
            {
                def = string.Format("{0}{2}TotalSleepTime={1}", def, m_TotalSleepTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_SleepEfficiency_ini)
            {
                def = string.Format("{0}{2}SleepEfficiency={1}", def, m_SleepEfficiency, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_WakeAfterSleepTimes_ini)
            {
                def = string.Format("{0}{2}WakeAfterSleepTimes={1}", def, m_WakeAfterSleepTimes, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_AwakeningTimes_ini)
            {
                def = string.Format("{0}{2}AwakeningTimes={1}", def, m_AwakeningTimes, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_SleepPeriodTime_ini)
            {
                def = string.Format("{0}{2}SleepPeriodTime={1}", def, m_SleepPeriodTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ArousalCount_ini)
            {
                def = string.Format("{0}{2}ArousalCount={1}", def, m_ArousalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_TimeInBed_ini)
            {
                def = string.Format("{0}{2}TimeInBed={1}", def, m_TimeInBed, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MicroArousalCount_ini)
            {
                def = string.Format("{0}{2}MicroArousalCount={1}", def, m_MicroArousalCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MicroArousalIndex_ini)
            {
                def = string.Format("{0}{2}MicroArousalIndex={1}", def, m_MicroArousalIndex, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_WKOfSleepDuration_ini)
            {
                def = string.Format("{0}{2}WKOfSleepDuration={1}", def, m_WKOfSleepDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_WKOfSleepTimeInBedPencent_ini)
            {
                def = string.Format("{0}{2}WKOfSleepTimeInBedPencent={1}", def, m_WKOfSleepTimeInBedPencent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_WKOfSleepTotalTimePencent_ini)
            {
                def = string.Format("{0}{2}WKOfSleepTotalTimePencent={1}", def, m_WKOfSleepTotalTimePencent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_WKOfSleepPeriodTimePencent_ini)
            {
                def = string.Format("{0}{2}WKOfSleepPeriodTimePencent={1}", def, m_WKOfSleepPeriodTimePencent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_REMOfSleepDuration_ini)
            {
                def = string.Format("{0}{2}REMOfSleepDuration={1}", def, m_REMOfSleepDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_REMOfSleepTimeInBedPencent_ini)
            {
                def = string.Format("{0}{2}REMOfSleepTimeInBedPencent={1}", def, m_REMOfSleepTimeInBedPencent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_REMOfSleepTotalTimePencent_ini)
            {
                def = string.Format("{0}{2}REMOfSleepTotalTimePencent={1}", def, m_REMOfSleepTotalTimePencent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_REMOfSleepPeriodTimePencent_ini)
            {
                def = string.Format("{0}{2}REMOfSleepPeriodTimePencent={1}", def, m_REMOfSleepPeriodTimePencent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_N1OfSleepDuration_ini)
            {
                def = string.Format("{0}{2}N1OfSleepDuration={1}", def, m_N1OfSleepDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_N1OfSleepTimeInBedPencent_ini)
            {
                def = string.Format("{0}{2}N1OfSleepTimeInBedPencent={1}", def, m_N1OfSleepTimeInBedPencent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_N1OfSleepTotalTimePencent_ini)
            {
                def = string.Format("{0}{2}N1OfSleepTotalTimePencent={1}", def, m_N1OfSleepTotalTimePencent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_N1OfSleepPeriodTimePencent_ini)
            {
                def = string.Format("{0}{2}N1OfSleepPeriodTimePencent={1}", def, m_N1OfSleepPeriodTimePencent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_N2OfSleepDuration_ini)
            {
                def = string.Format("{0}{2}N2OfSleepDuration={1}", def, m_N2OfSleepDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_N2OfSleepTimeInBedPencent_ini)
            {
                def = string.Format("{0}{2}N2OfSleepTimeInBedPencent={1}", def, m_N2OfSleepTimeInBedPencent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_N2OfSleepTotalTimePencent_ini)
            {
                def = string.Format("{0}{2}N2OfSleepTotalTimePencent={1}", def, m_N2OfSleepTotalTimePencent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_N2OfSleepPeriodTimePencent_ini)
            {
                def = string.Format("{0}{2}N2OfSleepPeriodTimePencent={1}", def, m_N2OfSleepPeriodTimePencent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_N3OfSleepDuration_ini)
            {
                def = string.Format("{0}{2}N3OfSleepDuration={1}", def, m_N3OfSleepDuration, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_N3OfSleepTimeInBedPencent_ini)
            {
                def = string.Format("{0}{2}N3OfSleepTimeInBedPencent={1}", def, m_N3OfSleepTimeInBedPencent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_N3OfSleepTotalTimePencent_ini)
            {
                def = string.Format("{0}{2}N3OfSleepTotalTimePencent={1}", def, m_N3OfSleepTotalTimePencent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_N3OfSleepPeriodTimePencent_ini)
            {
                def = string.Format("{0}{2}N3OfSleepPeriodTimePencent={1}", def, m_N3OfSleepPeriodTimePencent, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_SleepLatencyTimesFromLightOn_ini)
            {
                def = string.Format("{0}{2}SleepLatencyTimesFromLightOn={1}", def, m_SleepLatencyTimesFromLightOn, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_REMSleepLatencyFromLightOn_ini)
            {
                def = string.Format("{0}{2}REMSleepLatencyFromLightOn={1}", def, m_REMSleepLatencyFromLightOn, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_N1SleepLatencyFromLightOn_ini)
            {
                def = string.Format("{0}{2}N1SleepLatencyFromLightOn={1}", def, m_N1SleepLatencyFromLightOn, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_N2SleepLatencyFromLightOn_ini)
            {
                def = string.Format("{0}{2}N2SleepLatencyFromLightOn={1}", def, m_N2SleepLatencyFromLightOn, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_N3SleepLatencyFromLightOn_ini)
            {
                def = string.Format("{0}{2}N3SleepLatencyFromLightOn={1}", def, m_N3SleepLatencyFromLightOn, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_REMSleepLatencyFromSleepOnset_ini)
            {
                def = string.Format("{0}{2}REMSleepLatencyFromSleepOnset={1}", def, m_REMSleepLatencyFromSleepOnset, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_N1SleepLatencyFromSleepOnset_ini)
            {
                def = string.Format("{0}{2}N1SleepLatencyFromSleepOnset={1}", def, m_N1SleepLatencyFromSleepOnset, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_N2SleepLatencyFromSleepOnset_ini)
            {
                def = string.Format("{0}{2}N2SleepLatencyFromSleepOnset={1}", def, m_N2SleepLatencyFromSleepOnset, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_N3SleepLatencyFromSleepOnset_ini)
            {
                def = string.Format("{0}{2}N3SleepLatencyFromSleepOnset={1}", def, m_N3SleepLatencyFromSleepOnset, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_WakeAndSleepPercentOk_ini)
            {
                def = string.Format("{0}{2}WakeAndSleepPercentOk={1}", def, m_WakeAndSleepPercentOk ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_LatencyOfPersistentSleep_ini)
            {
                def = string.Format("{0}{2}LatencyOfPersistentSleep={1}", def, m_LatencyOfPersistentSleep, Mulitcase ? "," : "");
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
            return ("SleepResult");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "GUID":
                    GUID = Value.ToString(); ;
                    break;
                case "LightOffTime":
                    LightOffTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "LightOnTime":
                    LightOnTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "TotalSleepTime":
                    TotalSleepTime = Convert.ToSingle(Value);
                    break;
                case "SleepEfficiency":
                    SleepEfficiency = Convert.ToSingle(Value);
                    break;
                case "WakeAfterSleepTimes":
                    WakeAfterSleepTimes = Convert.ToSingle(Value);
                    break;
                case "AwakeningTimes":
                    AwakeningTimes = Convert.ToSingle(Value);
                    break;
                case "SleepPeriodTime":
                    SleepPeriodTime = Convert.ToSingle(Value);
                    break;
                case "ArousalCount":
                    ArousalCount = Convert.ToSingle(Value);
                    break;
                case "TimeInBed":
                    TimeInBed = Convert.ToSingle(Value);
                    break;
                case "MicroArousalCount":
                    MicroArousalCount = Convert.ToSingle(Value);
                    break;
                case "MicroArousalIndex":
                    MicroArousalIndex = Convert.ToSingle(Value);
                    break;
                case "WKOfSleepDuration":
                    WKOfSleepDuration = Convert.ToSingle(Value);
                    break;
                case "WKOfSleepTimeInBedPencent":
                    WKOfSleepTimeInBedPencent = Convert.ToSingle(Value);
                    break;
                case "WKOfSleepTotalTimePencent":
                    WKOfSleepTotalTimePencent = Convert.ToSingle(Value);
                    break;
                case "WKOfSleepPeriodTimePencent":
                    WKOfSleepPeriodTimePencent = Convert.ToSingle(Value);
                    break;
                case "REMOfSleepDuration":
                    REMOfSleepDuration = Convert.ToSingle(Value);
                    break;
                case "REMOfSleepTimeInBedPencent":
                    REMOfSleepTimeInBedPencent = Convert.ToSingle(Value);
                    break;
                case "REMOfSleepTotalTimePencent":
                    REMOfSleepTotalTimePencent = Convert.ToSingle(Value);
                    break;
                case "REMOfSleepPeriodTimePencent":
                    REMOfSleepPeriodTimePencent = Convert.ToSingle(Value);
                    break;
                case "N1OfSleepDuration":
                    N1OfSleepDuration = Convert.ToSingle(Value);
                    break;
                case "N1OfSleepTimeInBedPencent":
                    N1OfSleepTimeInBedPencent = Convert.ToSingle(Value);
                    break;
                case "N1OfSleepTotalTimePencent":
                    N1OfSleepTotalTimePencent = Convert.ToSingle(Value);
                    break;
                case "N1OfSleepPeriodTimePencent":
                    N1OfSleepPeriodTimePencent = Convert.ToSingle(Value);
                    break;
                case "N2OfSleepDuration":
                    N2OfSleepDuration = Convert.ToSingle(Value);
                    break;
                case "N2OfSleepTimeInBedPencent":
                    N2OfSleepTimeInBedPencent = Convert.ToSingle(Value);
                    break;
                case "N2OfSleepTotalTimePencent":
                    N2OfSleepTotalTimePencent = Convert.ToSingle(Value);
                    break;
                case "N2OfSleepPeriodTimePencent":
                    N2OfSleepPeriodTimePencent = Convert.ToSingle(Value);
                    break;
                case "N3OfSleepDuration":
                    N3OfSleepDuration = Convert.ToSingle(Value);
                    break;
                case "N3OfSleepTimeInBedPencent":
                    N3OfSleepTimeInBedPencent = Convert.ToSingle(Value);
                    break;
                case "N3OfSleepTotalTimePencent":
                    N3OfSleepTotalTimePencent = Convert.ToSingle(Value);
                    break;
                case "N3OfSleepPeriodTimePencent":
                    N3OfSleepPeriodTimePencent = Convert.ToSingle(Value);
                    break;
                case "SleepLatencyTimesFromLightOn":
                    SleepLatencyTimesFromLightOn = Convert.ToSingle(Value);
                    break;
                case "REMSleepLatencyFromLightOn":
                    REMSleepLatencyFromLightOn = Convert.ToSingle(Value);
                    break;
                case "N1SleepLatencyFromLightOn":
                    N1SleepLatencyFromLightOn = Convert.ToSingle(Value);
                    break;
                case "N2SleepLatencyFromLightOn":
                    N2SleepLatencyFromLightOn = Convert.ToSingle(Value);
                    break;
                case "N3SleepLatencyFromLightOn":
                    N3SleepLatencyFromLightOn = Convert.ToSingle(Value);
                    break;
                case "REMSleepLatencyFromSleepOnset":
                    REMSleepLatencyFromSleepOnset = Convert.ToSingle(Value);
                    break;
                case "N1SleepLatencyFromSleepOnset":
                    N1SleepLatencyFromSleepOnset = Convert.ToSingle(Value);
                    break;
                case "N2SleepLatencyFromSleepOnset":
                    N2SleepLatencyFromSleepOnset = Convert.ToSingle(Value);
                    break;
                case "N3SleepLatencyFromSleepOnset":
                    N3SleepLatencyFromSleepOnset = Convert.ToSingle(Value);
                    break;
                case "WakeAndSleepPercentOk":
                    WakeAndSleepPercentOk = Convert.ToBoolean(Value);
                    break;
                case "LatencyOfPersistentSleep":
                    LatencyOfPersistentSleep = Convert.ToSingle(Value);
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_SleepResult());
        }
        #endregion
        /// <summary>
        /// 记录开始时间
        /// </summary>
        public DateTime StartRecordTime { set; get; }
        /// <summary>
        /// 记录开始时间(string 类型 只有年月日)
        /// </summary>
        public string strStartRecordTime { set; get; }
        /// <summary>
        /// 关灯时间（string类型）
        /// </summary>
        public string strLightOffTime { set; get; }
        /// <summary>
        /// 开灯时间（string类型）
        /// </summary>
        public string strLightOnTime { set; get; }
        /// <summary>
        /// 记录结束时间
        /// </summary>
        public DateTime EndRecordTime { set; get; }
        /// <summary>
        /// 深浅睡眠比例失调 符合?不符合
        /// </summary>
        public string strWakeAndSleepOK { private set; get; }
        /// <summary>
        /// 睡眠效率 正常?降低
        /// </summary>
        public string strSleepEfficiency { private set; get; }
        /// <summary>
        /// 睡眠潜伏期 正常?缩短？延长
        /// </summary>
        public string strSleepLatency { private set; get; }
        /// <summary>
        /// 觉醒时间  正常？增多
        /// </summary>
        public string strAwakeningTimes { private set; get; }
        /// <summary>
        /// 睡眠觉醒次数 正常?增多
        /// </summary>
        public string strArousalCount { private set; get; }
        /// <summary>
        /// R期潜伏期 正常?缩短？延长
        /// </summary>
        public string strREMSleepLatency { private set; get; }
        /// <summary>
        /// N1期比例 正常?增高？降低
        /// </summary>
        public string strN1Ratio { private set; get; }
        /// <summary>
        /// N2期比例 正常?增高？降低
        /// </summary>
        public string strN2Ratio { private set; get; }
        /// <summary>
        /// N3期比例 正常?增高？降低
        /// </summary>
        public string strN3Ratio { private set; get; }
        /// <summary>
        /// R期比例 正常?增高？降低
        /// </summary>
        public string strRRatio { private set; get; }
        public string strN1SleepLatencyFromLightOn
        {
            get
            {
                return N1SleepLatencyFromLightOn == -1 ? "- -" : N1SleepLatencyFromLightOn.ToString();
            }
        }
        public string strN2SleepLatencyFromLightOn
        {
            get
            {
                return N2SleepLatencyFromLightOn == -1 ? "- -" : N2SleepLatencyFromLightOn.ToString();
            }
        }
        public string strN3SleepLatencyFromLightOn
        {
            get
            {
                return N3SleepLatencyFromLightOn == -1 ? "- -" : N3SleepLatencyFromLightOn.ToString();
            }
        }
        public string strREMSleepLatencyFromLightOn
        {
            get
            {
                return REMSleepLatencyFromLightOn == -1 ? "- -" : REMSleepLatencyFromLightOn.ToString();
            }
        }

        public string strSleepLatencyFromSleepOnset
        {
            get
            {
                if ((double)this.m_LatencyOfPersistentSleep != -1.0)
                    return this.m_LatencyOfPersistentSleep.ToString();
                return "- -";
            }
        }
        /// <summary>
        /// 睡眠期间W期次数
        /// </summary>
        public int WeekCountInSleepTime { set; get; }
        /// <summary>
        /// 睡眠期间N1期次数
        /// </summary>
        public int N1CountInSleepTime { set; get; }
        /// <summary>
        /// 睡眠期间N2期次数
        /// </summary>
        public int N2CountInSleepTime { set; get; }
        /// <summary>
        /// 睡眠期间N3期次数
        /// </summary>
        public int N3CountInSleepTime { set; get; }
        /// <summary>
        /// 睡眠期间R期次数
        /// </summary>
        public int REMCountInSleepTime { set; get; }
        /// <summary>
        /// 总记录时间
        /// </summary>
        public float TotalRecordTime { set; get; }
        /// <summary>
        /// 关灯帧序号
        /// </summary>
        public float LightOffFrameNo { set; get; }

        /// <summary>
        /// 开灯帧序号
        /// </summary>
        public float LightOnFrameNo { set; get; }
        /// <summary>
        /// 睡眠期间N期次数
        /// </summary>
        public int NREMCountInSleepTime { set; get; }
        /// <summary>
        /// 非快速眼动期总时长
        /// </summary>
        public float NREMOfSleepDuration { set; get; }
        /// <summary>
        /// 非快速眼动期在总卧床时间的占比
        /// </summary>
        public float NREMOfSleepTimeInBedPencent { set; get; }
        /// <summary>
        /// 非快速眼动期在总睡眠时间的占比
        /// </summary>
        public float NREMOfSleepTotalTimePencent { set; get; }
        /// <summary>
        /// 非快速眼动期在睡眠间期时间的占比
        /// </summary>
        public float NREMOfSleepPeriodTimePencent { set; get; }

        #region 微觉醒相关指标定义
        /// <summary>
        /// N期时的微觉醒次数
        /// </summary>
        public float NREMMicroArousalCount { set; get; }

        /// <summary>
        /// N1期时的微觉醒次数
        /// </summary>
        public float N1MicroArousalCount { set; get; }
        /// <summary>
        /// N2期时的微觉醒次数
        /// </summary>
        public float N2MicroArousalCount { set; get; }
        /// <summary>
        /// N3期时的微觉醒次数
        /// </summary>
        public float N3MicroArousalCount { set; get; }

        /// <summary>
        /// N期时的微觉醒指数
        /// </summary>
        public float NREMMicroArousalIndex { set; get; }
        /// <summary>
        /// R期时的微觉醒次数
        /// </summary>
        public float REMMicroArousalCount { set; get; }
        /// <summary>
        /// R期时的微觉醒指数
        /// </summary>
        public float REMMicroArousalIndex { set; get; }

        /// <summary>
        /// N1期时的腿动相关微觉醒次数
        /// </summary>
        public float N1LmMicroArousalCount { set; get; }
        /// <summary>
        /// N1期时的腿动相关微觉醒指数
        /// </summary>
        public float N1LmMicroArousalIndex { set; get; }
        /// <summary>
        /// N2期时的腿动相关微觉醒次数
        /// </summary>
        public float N2LmMicroArousalCount { set; get; }
        /// <summary>
        /// N2期时的腿动相关微觉醒指数
        /// </summary>
        public float N2LmMicroArousalIndex { set; get; }
        /// <summary>
        /// N3期时的腿动相关微觉醒次数
        /// </summary>
        public float N3LmMicroArousalCount { set; get; }
        /// <summary>
        /// N3期时的腿动相关微觉醒指数
        /// </summary>
        public float N3LmMicroArousalIndex { set; get; }
        /// <summary>
        /// N期时的腿动相关微觉醒次数
        /// </summary>
        public float NREMLmMicroArousalCount { set; get; }
        /// <summary>
        /// N期时的腿动相关微觉醒指数
        /// </summary>
        public float NREMLmMicroArousalIndex { set; get; }
        /// <summary>
        /// R期时的腿动相关微觉醒次数
        /// </summary>
        public float REMLmMicroArousalCount { set; get; }
        /// <summary>
        /// R期时的腿动相关微觉醒指数
        /// </summary>
        public float REMLmMicroArousalIndex { set; get; }
        /// <summary>
        /// 睡眠期时的腿动相关微觉醒次数
        /// </summary>
        public float LmMicroArousalCount { set; get; }
        /// <summary>
        /// 睡眠期时的腿动相关微觉醒指数
        /// </summary>
        public float LmMicroArousalIndex { set; get; }

        /// <summary>
        /// N1期时的循环腿动相关微觉醒次数
        /// </summary>
        public float N1PlmMicroArousalCount { set; get; }
        /// <summary>
        /// N1期时的循环腿动相关微觉醒指数
        /// </summary>
        public float N1PlmMicroArousalIndex { set; get; }
        /// <summary>
        /// N2期时的循环腿动相关微觉醒次数
        /// </summary>
        public float N2PlmMicroArousalCount { set; get; }
        /// <summary>
        /// N2期时的循环腿动相关微觉醒指数
        /// </summary>
        public float N2PlmMicroArousalIndex { set; get; }
        /// <summary>
        /// N3期时的循环腿动相关微觉醒次数
        /// </summary>
        public float N3PlmMicroArousalCount { set; get; }
        /// <summary>
        /// N3期时的循环腿动相关微觉醒指数
        /// </summary>
        public float N3PlmMicroArousalIndex { set; get; }
        /// <summary>
        /// N期时的循环腿动相关微觉醒次数
        /// </summary>
        public float NREMPlmMicroArousalCount { set; get; }
        /// <summary>
        /// N期时的循环腿动相关微觉醒指数
        /// </summary>
        public float NREMPlmMicroArousalIndex { set; get; }
        /// <summary>
        /// R期时的循环腿动相关微觉醒次数
        /// </summary>
        public float REMPlmMicroArousalCount { set; get; }
        /// <summary>
        /// R期时的循环腿动相关微觉醒指数
        /// </summary>
        public float REMPlmMicroArousalIndex { set; get; }
        /// <summary>
        /// 睡眠期时的循环腿动相关微觉醒次数
        /// </summary>
        public float PlmMicroArousalCount { set; get; }
        /// <summary>
        /// 睡眠期时的循环腿动相关微觉醒指数
        /// </summary>
        public float PlmMicroArousalIndex { set; get; }

        /// <summary>
        /// N1期时的呼吸相关微觉醒次数
        /// </summary>
        public float N1RespMicroArousalCount { set; get; }
        /// <summary>
        /// N1期时的呼吸相关微觉醒指数
        /// </summary>
        public float N1RespMicroArousalIndex { set; get; }

        /// <summary>
        /// N2期时的呼吸相关微觉醒次数
        /// </summary>
        public float N2RespMicroArousalCount { set; get; }
        /// <summary>
        /// N2期时的呼吸相关微觉醒指数
        /// </summary>
        public float N2RespMicroArousalIndex { set; get; }

        /// <summary>
        /// N3期时的呼吸相关微觉醒次数
        /// </summary>
        public float N3RespMicroArousalCount { set; get; }
        /// <summary>
        /// N3期时的呼吸相关微觉醒指数
        /// </summary>
        public float N3RespMicroArousalIndex { set; get; }

        /// <summary>
        /// N期时的呼吸相关微觉醒次数
        /// </summary>
        public float NREMRespMicroArousalCount { set; get; }
        /// <summary>
        /// N期时的呼吸相关微觉醒指数
        /// </summary>
        public float NREMRespMicroArousalIndex { set; get; }
        /// <summary>
        /// R期时的呼吸相关微觉醒次数
        /// </summary>
        public float REMRespMicroArousalCount { set; get; }
        /// <summary>
        /// R期时的呼吸相关微觉醒指数
        /// </summary>
        public float REMRespMicroArousalIndex { set; get; }
        /// <summary>
        /// 睡眠期时的呼吸相关微觉醒次数
        /// </summary>
        public float RespMicroArousalCount { set; get; }
        /// <summary>
        /// 睡眠期时的呼吸相关微觉醒指数
        /// </summary>
        public float RespMicroArousalIndex { set; get; }

        /// <summary>
        /// N1期时的自发性微觉醒次数
        /// </summary>
        public float N1NorMicroArousalCount { set; get; }
        /// <summary>
        /// N1期时的自发性微觉醒指数
        /// </summary>
        public float N1NorMicroArousalIndex { set; get; }
        /// <summary>
        /// N2期时的自发性微觉醒次数
        /// </summary>
        public float N2NorMicroArousalCount { set; get; }
        /// <summary>
        /// N2期时的自发性微觉醒指数
        /// </summary>
        public float N2NorMicroArousalIndex { set; get; }
        /// <summary>
        /// N3期时的自发性微觉醒次数
        /// </summary>
        public float N3NorMicroArousalCount { set; get; }
        /// <summary>
        /// N3期时的自发性微觉醒指数
        /// </summary>
        public float N3NorMicroArousalIndex { set; get; }
        /// <summary>
        /// N期时的自发性微觉醒次数
        /// </summary>
        public float NREMNorMicroArousalCount { set; get; }
        /// <summary>
        /// N期时的自发性微觉醒指数
        /// </summary>
        public float NREMNorMicroArousalIndex { set; get; }
        /// <summary>
        /// R期时的自发性微觉醒次数
        /// </summary>
        public float REMNorMicroArousalCount { set; get; }
        /// <summary>
        /// R期时的自发性微觉醒指数
        /// </summary>
        public float REMNorMicroArousalIndex { set; get; }
        /// <summary>
        /// 睡眠期时的自发性微觉醒次数
        /// </summary>
        public float NorMicroArousalCount { set; get; }
        /// <summary>
        /// 睡眠期时的自发性微觉醒指数
        /// </summary>
        public float NorMicroArousalIndex { set; get; }
        #endregion
        #region 小睡模式指标
        /// <summary>
        /// 多次小睡总的次数
        /// </summary>
        public float TotalMultipleSleepCount { set; get; }

        /// <summary>
        /// 多次小睡总的时间
        /// </summary>
        public float TotalMultipleSleepTimes { set; get; }
        /// <summary>
        /// 多次小睡监测开始时间
        /// </summary>
        public string MultipleSleepStartTime { set; get; }
        /// <summary>
        /// 平均睡眠潜伏期
        /// </summary>
        public float AverageMultipleSleepLatencyTimes { set; get; }
        /// <summary>
        /// SOREMPs次数(进入入睡期REM睡眠的小睡次数)
        /// </summary>
        public float SOREMPsCount { set; get; }
        /// <summary>
        /// 单次小睡时间持续时间
        /// </summary>
        public float SingleSleepDuration { set; get; }
        /// <summary>
        /// 小睡间隔时间范围
        /// </summary>
        public string MultipleSleepSpanRange { set; get; }
        /// <summary>
        /// α节律频率范围
        /// </summary>
        public string AlphaWaveFreqRange { set; get; }
        /// <summary>
        /// α节律平均振幅
        /// </summary>
        public float AlphaPeakValue { set; get; }
        /// <summary>
        /// 第一次小睡时间范围
        /// </summary>
        public string FirstSleepTimeRange { set; get; }
        /// <summary>
        /// 第二次小睡时间范围
        /// </summary>
        public string SecondSleepTimeRange { set; get; }
        /// <summary>
        /// 第三次小睡时间范围
        /// </summary>
        public string ThirdSleepTimeRange { set; get; }
        /// <summary>
        /// 第四次小睡时间范围
        /// </summary>
        public string FourthSleepTimeRange { set; get; }
        /// <summary>
        /// 第五次小睡时间范围
        /// </summary>
        public string FifthSleepTimeRange { set; get; }

        /// <summary>
        /// 第一次小睡时间
        /// </summary>
        public float FirstTotalSleepTimes { set; get; }
        /// <summary>
        /// 第二次小睡时间
        /// </summary>
        public float SecondTotalSleepTimes { set; get; }
        /// <summary>
        /// 第三次小睡时间
        /// </summary>
        public float ThirdTotalSleepTimes { set; get; }
        /// <summary>
        /// 第四次小睡时间
        /// </summary>
        public float FourthTotalSleepTimes { set; get; }
        /// <summary>
        /// 第五次小睡时间
        /// </summary>
        public float FifthTotalSleepTimes { set; get; }

        /// <summary>
        /// 第一次小睡睡眠时间
        /// </summary>
        public float FirstDeepSleepTimes { set; get; }
        /// <summary>
        /// 第二次小睡睡眠时间
        /// </summary>
        public float SecondDeepSleepTimes { set; get; }
        /// <summary>
        /// 第三次小睡睡眠时间
        /// </summary>
        public float ThirdDeepSleepTimes { set; get; }
        /// <summary>
        /// 第四次小睡睡眠时间
        /// </summary>
        public float FourthDeepSleepTimes { set; get; }
        /// <summary>
        /// 第五次小睡睡眠时间
        /// </summary>
        public float FifthDeepSleepTimes { set; get; }

        /// <summary>
        /// 第一次小睡清醒时间
        /// </summary>
        public float FirstWakeTimes { set; get; }
        /// <summary>
        /// 第二次小睡清醒时间
        /// </summary>
        public float SecondWakeTimes { set; get; }
        /// <summary>
        /// 第三次小睡清醒时间
        /// </summary>
        public float ThirdWakeTimes { set; get; }
        /// <summary>
        /// 第四次小睡清醒时间
        /// </summary>
        public float FourthWakeTimes { set; get; }
        /// <summary>
        /// 第五次小睡清醒时间
        /// </summary>
        public float FifthWakeTimes { set; get; }

        /// <summary>
        /// 第一次小睡Rem次数
        /// </summary>
        public int FirstSleepRemCounts { set; get; }
        /// <summary>
        /// 第二次小睡Rem次数
        /// </summary>
        public int SecondSleepRemCounts { set; get; }
        /// <summary>
        /// 第三次小睡Rem次数
        /// </summary>
        public int ThirdSleepRemCounts { set; get; }
        /// <summary>
        /// 第四次小睡Rem次数
        /// </summary>
        public int FourthSleepRemCounts { set; get; }
        /// <summary>
        /// 第五次小睡Rem次数
        /// </summary>
        public int FifthSleepRemCounts { set; get; }

        private float m_FirstSleepLatencyTimesFromLightOff = -1;
        /// <summary>
        /// 第一次小睡睡眠潜伏期
        /// </summary>
        public float FirstSleepLatencyTimesFromLightOff
        {
            set { m_FirstSleepLatencyTimesFromLightOff = value; }
            get { return m_FirstSleepLatencyTimesFromLightOff; }
        }
        private float m_SecondSleepLatencyTimesFromLightOff = -1;
        /// <summary>
        /// 第二次小睡睡眠潜伏期
        /// </summary>
        public float SecondSleepLatencyTimesFromLightOff
        {
            set { m_SecondSleepLatencyTimesFromLightOff = value; }
            get { return m_SecondSleepLatencyTimesFromLightOff; }
        }
        private float m_ThirdSleepLatencyTimesFromLightOff = -1;
        /// <summary>
        /// 第三次小睡睡眠潜伏期
        /// </summary>
        public float ThirdSleepLatencyTimesFromLightOff
        {
            set { m_ThirdSleepLatencyTimesFromLightOff = value; }
            get { return m_ThirdSleepLatencyTimesFromLightOff; }
        }
        private float m_FourthSleepLatencyTimesFromLightOff = -1;
        /// <summary>
        /// 第四次小睡睡眠潜伏期
        /// </summary>
        public float FourthSleepLatencyTimesFromLightOff
        {
            set { m_FourthSleepLatencyTimesFromLightOff = value; }
            get { return m_FourthSleepLatencyTimesFromLightOff; }
        }
        private float m_FifthSleepLatencyTimesFromLightOff = -1;
        /// <summary>
        /// 第五次小睡睡眠潜伏期
        /// </summary>
        public float FifthSleepLatencyTimesFromLightOff
        {
            set { m_FifthSleepLatencyTimesFromLightOff = value; }
            get { return m_FifthSleepLatencyTimesFromLightOff; }
        }
        private float m_FirstRemSleepLatencyTimesFromLightOff = -1;
        /// <summary>
        /// 第一次小睡Rem睡眠潜伏期
        /// </summary>
        public float FirstRemSleepLatencyTimesFromLightOff
        {
            set { m_FirstRemSleepLatencyTimesFromLightOff = value; }
            get { return m_FirstRemSleepLatencyTimesFromLightOff; }
        }
        private float m_SecondRemSleepLatencyTimesFromLightOff = -1;
        /// <summary>
        /// 第二次小睡Rem睡眠潜伏期
        /// </summary>
        public float SecondRemSleepLatencyTimesFromLightOff
        {
            set { m_SecondRemSleepLatencyTimesFromLightOff = value; }
            get { return m_SecondRemSleepLatencyTimesFromLightOff; }
        }
        private float m_ThirdRemSleepLatencyTimesFromLightOff = -1;
        /// <summary>
        /// 第三次小睡Rem睡眠潜伏期
        /// </summary>
        public float ThirdRemSleepLatencyTimesFromLightOff
        {
            set { m_ThirdRemSleepLatencyTimesFromLightOff = value; }
            get { return m_ThirdRemSleepLatencyTimesFromLightOff; }
        }
        private float m_FourthRemSleepLatencyTimesFromLightOff = -1;
        /// <summary>
        /// 第四次小睡Rem睡眠潜伏期
        /// </summary>
        public float FourthRemSleepLatencyTimesFromLightOff
        {
            set { m_FourthRemSleepLatencyTimesFromLightOff = value; }
            get { return m_FourthRemSleepLatencyTimesFromLightOff; }
        }
        private float m_FifthRemSleepLatencyTimesFromLightOff = -1;
        /// <summary>
        /// 第五次小睡Rem睡眠潜伏期
        /// </summary>
        public float FifthRemSleepLatencyTimesFromLightOff
        {
            set { m_FifthRemSleepLatencyTimesFromLightOff = value; }
            get { return m_FifthRemSleepLatencyTimesFromLightOff; }
        }

        /// <summary>
        /// 平均REM睡眠潜伏期
        /// </summary>
        public float AverageMultipleSleepRemLatencyTimes { set; get; }

        /// <summary>
        /// REM出现次数
        /// </summary>
        public float MultipleSleepTotalRemCounts{ set; get; }

        #endregion
    }
}
