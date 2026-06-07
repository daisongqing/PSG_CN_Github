using pSystem.Interface.Util;
using System;

namespace AwareTec.Polysmith.DataBaseCom
{
    /// <summary>
    /// 体动
    /// </summary>
    public class Doc_BodyMovementResult : ITable
    {
        public Doc_BodyMovementResult()
        {
            //m_PeriodicalBodyMovementCount = 100;
            //m_PeriodicalBodyMovementIndex = 10;
            //m_AwakenPeriodicalBodyMovementCount = 30;
            //m_AwakenPeriodicalBodyMovementIndex = 3;
            strLegsMoveDegreeLevel = "未见■ 轻度□ 中度□ 重度□";
        }
        #region 私有成员
        private string m_GUID = "";
        private bool m_GUID_ini = false;
        private float m_PeriodicalBodyMovementCount = 0;
        private bool m_PeriodicalBodyMovementCount_ini = false;
        private float m_PeriodicalBodyMovementIndex = 0;
        private bool m_PeriodicalBodyMovementIndex_ini = false;
        private float m_AwakenPeriodicalBodyMovementCount = 0;
        private bool m_AwakenPeriodicalBodyMovementCount_ini = false;
        private float m_AwakenPeriodicalBodyMovementIndex = 0;
        private bool m_AwakenPeriodicalBodyMovementIndex_ini = false;
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
        /// <summary>
        /// 周期性肢体运动次数（腿动）
        /// </summary>
        public float PeriodicalBodyMovementCount
        {
            set
            {
                m_PeriodicalBodyMovementCount = value;
                m_PeriodicalBodyMovementCount_ini = true;
            }
            get
            {
                return m_PeriodicalBodyMovementCount;
            }
        }
        /// <summary>
        /// 周期性肢体运动指数（腿动）
        /// </summary>
        public float PeriodicalBodyMovementIndex
        {
            set
            {
                m_PeriodicalBodyMovementIndex = value;
                m_PeriodicalBodyMovementIndex_ini = true;
            }
            get
            {
                return m_PeriodicalBodyMovementIndex;
            }
        }
        /// <summary>
        /// 伴随觉醒的周期性肢体运动次数（序列）
        /// </summary>
        public float AwakenPeriodicalBodyMovementCount
        {
            set
            {
                m_AwakenPeriodicalBodyMovementCount = value;
                m_AwakenPeriodicalBodyMovementCount_ini = true;
            }
            get
            {
                return m_AwakenPeriodicalBodyMovementCount;
            }
        }
        /// <summary>
        /// 伴随觉醒的周期性肢体运动指数（序列）
        /// </summary>
        public float AwakenPeriodicalBodyMovementIndex
        {
            set
            {
                m_AwakenPeriodicalBodyMovementIndex = value;
                m_AwakenPeriodicalBodyMovementIndex_ini = true;
            }
            get
            {
                return m_AwakenPeriodicalBodyMovementIndex;
            }
        }
        /// <summary>
        /// 伴随觉醒的周期性肢体运动次数（腿动）
        /// </summary>
        public float LegsAwakenPeriodicalBodyMovementCount
        {
            set;
            get;
        }
        /// <summary>
        /// 伴随觉醒的周期性肢体运动指数（腿动）
        /// </summary>
        public float LegsAwakenPeriodicalBodyMovementIndex
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
            if (this.m_PeriodicalBodyMovementCount_ini)
            {
                def = string.Format("{0}{2}{1}", def, "PeriodicalBodyMovementCount", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_PeriodicalBodyMovementCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_PeriodicalBodyMovementIndex_ini)
            {
                def = string.Format("{0}{2}{1}", def, "PeriodicalBodyMovementIndex", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_PeriodicalBodyMovementIndex, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_AwakenPeriodicalBodyMovementCount_ini)
            {
                def = string.Format("{0}{2}{1}", def, "AwakenPeriodicalBodyMovementCount", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_AwakenPeriodicalBodyMovementCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_AwakenPeriodicalBodyMovementIndex_ini)
            {
                def = string.Format("{0}{2}{1}", def, "AwakenPeriodicalBodyMovementIndex", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_AwakenPeriodicalBodyMovementIndex, Mulitcase ? "," : "");
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
            if (m_PeriodicalBodyMovementCount_ini)
            {
                returnstr = string.Format("{0}PeriodicalBodyMovementCount={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", PeriodicalBodyMovementCount);
                Mulitcase = true;
            }
            if (m_PeriodicalBodyMovementIndex_ini)
            {
                returnstr = string.Format("{0}PeriodicalBodyMovementIndex={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", PeriodicalBodyMovementIndex);
                Mulitcase = true;
            }
            if (m_AwakenPeriodicalBodyMovementCount_ini)
            {
                returnstr = string.Format("{0}AwakenPeriodicalBodyMovementCount={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", AwakenPeriodicalBodyMovementCount);
                Mulitcase = true;
            }
            if (m_AwakenPeriodicalBodyMovementIndex_ini)
            {
                returnstr = string.Format("{0}AwakenPeriodicalBodyMovementIndex={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", AwakenPeriodicalBodyMovementIndex);
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
            if (m_PeriodicalBodyMovementCount_ini)
            {
                def = string.Format("{0}{2}PeriodicalBodyMovementCount={1}", def, m_PeriodicalBodyMovementCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_PeriodicalBodyMovementIndex_ini)
            {
                def = string.Format("{0}{2}PeriodicalBodyMovementIndex={1}", def, m_PeriodicalBodyMovementIndex, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_AwakenPeriodicalBodyMovementCount_ini)
            {
                def = string.Format("{0}{2}AwakenPeriodicalBodyMovementCount={1}", def, m_AwakenPeriodicalBodyMovementCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_AwakenPeriodicalBodyMovementIndex_ini)
            {
                def = string.Format("{0}{2}AwakenPeriodicalBodyMovementIndex={1}", def, m_AwakenPeriodicalBodyMovementIndex, Mulitcase ? "," : "");
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
            return ("BodyMovementResult");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "GUID":
                    GUID = Value.ToString(); ;
                    break;
                case "PeriodicalBodyMovementCount":
                    PeriodicalBodyMovementCount = Convert.ToSingle(Value);
                    break;
                case "PeriodicalBodyMovementIndex":
                    PeriodicalBodyMovementIndex = Convert.ToSingle(Value);
                    break;
                case "AwakenPeriodicalBodyMovementCount":
                    AwakenPeriodicalBodyMovementCount = Convert.ToSingle(Value);
                    break;
                case "AwakenPeriodicalBodyMovementIndex":
                    AwakenPeriodicalBodyMovementIndex = Convert.ToSingle(Value);
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_BodyMovementResult());
        }
        #endregion
        /// <summary>
        /// 序列
        /// </summary>
        public float NREMPeriodicalBodyMovementCount { set; get; }
        /// <summary>
        /// 序列
        /// </summary>
        public float REMPeriodicalBodyMovementCount { set; get; }
        /// <summary>
        /// 序列
        /// </summary>
        public float NREMPeriodicalBodyMovementIndex { set; get; }
        /// <summary>
        /// 序列
        /// </summary>
        public float REMPeriodicalBodyMovementIndex { set; get; }
        /// <summary>
        /// 序列
        /// </summary>
        public float NREMAwakenPeriodicalBodyMovementCount { set; get; }
        /// <summary>
        /// 序列
        /// </summary>
        public float REMAwakenPeriodicalBodyMovementCount { set; get; }
        /// <summary>
        /// 序列
        /// </summary>
        public float NREMAwakenPeriodicalBodyMovementIndex { set; get; }
        /// <summary>
        /// 序列
        /// </summary>
        public float REMAwakenPeriodicalBodyMovementIndex { set; get; }
        /// <summary>
        /// 序列
        /// </summary>
        public float WeekAwakenPeriodicalBodyMovementCount { set; get; }
        /// <summary>
        /// 序列
        /// </summary>
        public float WeekAwakenPeriodicalBodyMovementIndex { set; get; }
        /// <summary>
        /// 序列
        /// </summary>
        public float WeekPeriodicalBodyMovementCount { set; get; }
        /// <summary>
        /// 序列
        /// </summary>
        public float WeekPeriodicalBodyMovementIndex { set; get; }
        /// <summary>
        /// 序列
        /// </summary>
        public float SleepAwakenPeriodicalBodyMovementCount { set; get; }
        /// <summary>
        /// 序列
        /// </summary>
        public float SleepAwakenPeriodicalBodyMovementIndex { set; get; }
        /// <summary>
        /// 序列
        /// </summary>
        public float SleepPeriodicalBodyMovementCount { set; get; }
        /// <summary>
        /// 序列
        /// </summary>
        public float SleepPeriodicalBodyMovementIndex { set; get; }
        /// <summary>
        /// 腿动
        /// </summary>
        public float LegsNREMPeriodicalBodyMovementCount { set; get; }
        /// <summary>
        /// 腿动
        /// </summary>
        public float LegsREMPeriodicalBodyMovementCount { set; get; }
        /// <summary>
        /// 腿动
        /// </summary>
        public float LegsNREMPeriodicalBodyMovementIndex { set; get; }
        /// <summary>
        /// 腿动
        /// </summary>
        public float LegsREMPeriodicalBodyMovementIndex { set; get; }
        /// <summary>
        /// 腿动
        /// </summary>
        public float LegsNREMAwakenPeriodicalBodyMovementCount { set; get; }
        /// <summary>
        /// 腿动
        /// </summary>
        public float LegsREMAwakenPeriodicalBodyMovementCount { set; get; }
        /// <summary>
        /// 腿动
        /// </summary>
        public float LegsNREMAwakenPeriodicalBodyMovementIndex { set; get; }
        /// <summary>
        /// 腿动
        /// </summary>
        public float LegsREMAwakenPeriodicalBodyMovementIndex { set; get; }
        /// <summary>
        /// 腿动
        /// </summary>
        public float LegsWeekAwakenPeriodicalBodyMovementCount { set; get; }
        /// <summary>
        /// 腿动
        /// </summary>
        public float LegsWeekAwakenPeriodicalBodyMovementIndex { set; get; }
        /// <summary>
        /// 腿动
        /// </summary>
        public float LegsWeekPeriodicalBodyMovementCount { set; get; }
        /// <summary>
        /// 腿动
        /// </summary>
        public float LegsWeekPeriodicalBodyMovementIndex { set; get; }
        /// <summary>
        /// 腿动
        /// </summary>
        public float LegsSleepAwakenPeriodicalBodyMovementCount { set; get; }
        /// <summary>
        /// 腿动
        /// </summary>
        public float LegsSleepAwakenPeriodicalBodyMovementIndex { set; get; }
        /// <summary>
        /// 腿动
        /// </summary>
        public float LegsSleepPeriodicalBodyMovementCount { set; get; }
        /// <summary>
        /// 腿动
        /// </summary>
        public float LegsSleepPeriodicalBodyMovementIndex { set; get; }
        /// <summary>
        /// 腿动总次数
        /// </summary>
        public float LegsMoveTotalCount { set; get; }
        private float m_LegsMoveIndex = 0f;
        /// <summary>
        /// 腿动总指数
        /// </summary>
        public float LegsMoveIndex 
        { 
            set
            {
                m_LegsMoveIndex = value;
                if (value < 5)
                {
                    strLegsMoveDegreeLevel = "未见■ 轻度□ 中度□ 重度□";
                }
                else if (value >= 5 && value < 25)
                {
                    strLegsMoveDegreeLevel = "未见□ 轻度■ 中度□ 重度□";
                }
                else if (value >= 25 && value < 50)
                {
                    strLegsMoveDegreeLevel = "未见□ 轻度□ 中度■ 重度□";
                }
                else if (value >= 50)
                {
                    strLegsMoveDegreeLevel = "未见□ 轻度□ 中度□ 重度■";
                }
            }
            get => m_LegsMoveIndex;
        }
        /// <summary>
        /// 腿动指数情况描述（0-5未见 5-25轻度 25-50中度 大于50重度）
        /// </summary>
        public string strLegsMoveDegreeLevel { private set; get; }
        /// <summary>
        /// PLM序列
        /// </summary>
        public float PeriodicalLegsCount { set; get; }
        /// <summary>
        /// 周期性肢体运动指数（序列）
        /// </summary>
        public float PLMPeriodicalBodyMovementIndex { set; get; }
        /// <summary>
        /// R期腿动次数
        /// </summary>
        public float REMLegsMoveCount { set; get; }
        /// <summary>
        /// W期腿动次数
        /// </summary>
        public float WeekLegsMoveCount { set; get; }
        /// <summary>
        /// N期腿动次数
        /// </summary>
        public float NREMLegsMoveCount { set; get; }
        /// <summary>
        /// 睡眠期腿动次数
        /// </summary>
        public float SleepLegsMoveCount { set; get; }

        /// <summary>
        /// PLMS相关微觉醒次数
        /// </summary>
        public float PlmArousalCount { set; get; }
    }
}
