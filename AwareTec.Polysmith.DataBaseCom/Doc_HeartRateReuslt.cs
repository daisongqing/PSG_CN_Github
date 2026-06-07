using pSystem.Interface.Util;
using System;

namespace AwareTec.Polysmith.DataBaseCom
{
    /// <summary>
    /// 心脏数据统计
    /// </summary>
    public class Doc_HeartRateReuslt : ITable
    {
        public Doc_HeartRateReuslt()
        {
            //m_NREMAverageHeartRate = 59.9f;
            //m_MaxSleepHeartRate = 112;
            //m_MinSleepHeartRate = 17;
            //m_AverageSleepHeartRate = 59.9f;
        }
        #region 私有成员
        private string m_GUID = "";
        private bool m_GUID_ini = false;
        private float m_NREMAverageHeartRate = 0;
        private bool m_NREMAverageHeartRate_ini = false;
        private float m_MaxSleepHeartRate = 0;
        private bool m_MaxSleepHeartRate_ini = false;
        private float m_MinSleepHeartRate = 0;
        private bool m_MinSleepHeartRate_ini = false;
        private float m_AverageSleepHeartRate = 0;
        private bool m_AverageSleepHeartRate_ini = false;
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
        /// NREM期间的平均心率
        /// </summary>
        public float NREMAverageHeartRate
        {
            set
            {
                m_NREMAverageHeartRate = value;
                m_NREMAverageHeartRate_ini = true;
            }
            get
            {
                return m_NREMAverageHeartRate;
            }
        }

        /// <summary>
        /// 呼吸事件相关的平均心率
        /// </summary>
        public float ApneaAndHypopneaAverageHeartRate
        {
            set;

            get;

        }

        /// <summary>
        /// 睡眠期间的最大心率(TIB)
        /// </summary>
        public float MaxSleepHeartRateTIB
        {
            set;
            get;
        }
        /// <summary>
        /// 睡眠期间的最大心率(TIB)发生时间
        /// </summary>
        public DateTime MaxSleepHeartRateTIBStartTime
        {
            set;
            get;
        }
        /// <summary>
        /// 睡眠期间最小心率(TIB)
        /// </summary>
        public float MinSleepHeartRateTIB
        {
            set;
            get;
        }
        /// <summary>
        /// 睡眠期间最小心率(TIB)发生时间
        /// </summary>
        public DateTime MinSleepHeartRateTIBStartTime
        {
            set;
            get;
        }
        /// <summary>
        /// 睡眠期间的平均心率(TIB)
        /// </summary>
        public float AverageSleepHeartRateTIB
        {
            set;
            get;
        }

        /// <summary>
        /// 睡眠期间的最大心率(TST)
        /// </summary>
        public float MaxSleepHeartRate
        {
            set
            {
                m_MaxSleepHeartRate = value;
                m_MaxSleepHeartRate_ini = true;
            }
            get
            {
                return m_MaxSleepHeartRate;
            }
        }
        /// <summary>
        /// 睡眠期间的最大心率(TST)发生时间
        /// </summary>
        public DateTime MaxSleepHeartRateStartTime
        {
            set;get;
        }
        /// <summary>
        /// 睡眠期间最小心率(TST)
        /// </summary>
        public float MinSleepHeartRate
        {
            set
            {
                m_MinSleepHeartRate = value;
                m_MinSleepHeartRate_ini = true;
            }
            get
            {
                return m_MinSleepHeartRate;
            }
        }
        /// <summary>
        /// 睡眠期间最小心率(TST)发生时间
        /// </summary>
        public DateTime MinSleepHeartRateStartTime
        {
            set;get;
        }
        /// <summary>
        /// 睡眠期间的平均心率(TST)
        /// </summary>
        public float AverageSleepHeartRate
        {
            set
            {
                m_AverageSleepHeartRate = value;
                m_AverageSleepHeartRate_ini = true;
            }
            get
            {
                return m_AverageSleepHeartRate;
            }
        }
        private float m_REMAverageHeartRate = 0f;
        /// <summary>
        /// 睡眠期间快速眼动时的平均心率(TST)
        /// </summary>
        public float REMAverageHeartRate
        {
            set
            {
                m_REMAverageHeartRate = value;
            }
            get
            {
                return m_REMAverageHeartRate;
            }
        }
        /// <summary>
        /// 睡眠期间W期的最大心率(TIB)
        /// </summary>
        public float WeakMaxSleepHeartRateTIB
        {
            set;
            get;
        }
        /// <summary>
        /// 睡眠期间的W期最大心率(TIB)发生时间
        /// </summary>
        public DateTime WeakMaxSleepHeartRateTIBStartTime
        {
            set;
            get;
        }
        /// <summary>
        /// 睡眠期间W期最小心率(TIB)
        /// </summary>
        public float WeakMinSleepHeartRateTIB
        {
            set;
            get;
        }
        /// <summary>
        /// 睡眠期间W期最小心率(TIB)发生时间
        /// </summary>
        public DateTime WeakMinSleepHeartRateTIBStartTime
        {
            set;
            get;
        }
        /// <summary>
        /// 睡眠期间W期的平均心率(TIB)
        /// </summary>
        public float WeakAverageSleepHeartRateTIB
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
            if (this.m_NREMAverageHeartRate_ini)
            {
                def = string.Format("{0}{2}{1}", def, "NREMAverageHeartRate", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_NREMAverageHeartRate, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MaxSleepHeartRate_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MaxSleepHeartRate", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_MaxSleepHeartRate, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MinSleepHeartRate_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MinSleepHeartRate", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_MinSleepHeartRate, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_AverageSleepHeartRate_ini)
            {
                def = string.Format("{0}{2}{1}", def, "AverageSleepHeartRate", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_AverageSleepHeartRate, Mulitcase ? "," : "");
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
                returnstr = string.Format("{0}GUID='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", GUID);
                Mulitcase = true;
            }
            if (m_NREMAverageHeartRate_ini)
            {
                returnstr = string.Format("{0}NREMAverageHeartRate={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", NREMAverageHeartRate);
                Mulitcase = true;
            }
            if (m_MaxSleepHeartRate_ini)
            {
                returnstr = string.Format("{0}MaxSleepHeartRate={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", MaxSleepHeartRate);
                Mulitcase = true;
            }
            if (m_MinSleepHeartRate_ini)
            {
                returnstr = string.Format("{0}MinSleepHeartRate={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", MinSleepHeartRate);
                Mulitcase = true;
            }
            if (m_AverageSleepHeartRate_ini)
            {
                returnstr = string.Format("{0}AverageSleepHeartRate={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", AverageSleepHeartRate);
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
            if (m_NREMAverageHeartRate_ini)
            {
                def = string.Format("{0}{2}NREMAverageHeartRate={1}", def, m_NREMAverageHeartRate, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MaxSleepHeartRate_ini)
            {
                def = string.Format("{0}{2}MaxSleepHeartRate={1}", def, m_MaxSleepHeartRate, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MinSleepHeartRate_ini)
            {
                def = string.Format("{0}{2}MinSleepHeartRate={1}", def, m_MinSleepHeartRate, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_AverageSleepHeartRate_ini)
            {
                def = string.Format("{0}{2}AverageSleepHeartRate={1}", def, m_AverageSleepHeartRate, Mulitcase ? "," : "");
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
            return ("HeartRateReuslt");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "GUID":
                    GUID = Value.ToString(); ;
                    break;
                case "NREMAverageHeartRate":
                    NREMAverageHeartRate = Convert.ToSingle(Value);
                    break;
                case "MaxSleepHeartRate":
                    MaxSleepHeartRate = Convert.ToSingle(Value);
                    break;
                case "MinSleepHeartRate":
                    MinSleepHeartRate = Convert.ToSingle(Value);
                    break;
                case "AverageSleepHeartRate":
                    AverageSleepHeartRate = Convert.ToSingle(Value);
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_HeartRateReuslt());
        }
        #endregion

    }
}
