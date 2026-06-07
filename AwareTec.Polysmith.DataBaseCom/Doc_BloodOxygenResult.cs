using pSystem.Interface.Util;
using System;
using System.Collections.Generic;

namespace AwareTec.Polysmith.DataBaseCom
{
    /// <summary>
    /// 血氧统计结果
    /// </summary>
    public class Doc_BloodOxygenResult : ITable
    {
        public Doc_BloodOxygenResult()
        {
            strBloodOxygenLowDegreeLevel = "未见■ 轻度□ 中度□ 重度□";
            strMinBloodOxygenTSTBreathEventTyp = "无呼吸异常事件发生";
        }
        #region 私有成员
        private string m_GUID = "";
        private bool m_GUID_ini = false;
        private int m_BloodOxygenLowDegreeLevel = 0;
        private bool m_BloodOxygenLowDegreeLevel_ini = false;
        private float m_MinLimitBloodOxygenDuration = 0;
        private bool m_MinLimitBloodOxygenDuration_ini = false;
        private float m_MinBloodOxygenTIB = 0;
        private bool m_MinBloodOxygenTIB_ini = false;
        private float m_MinBloodOxygenTST = 0;
        private bool m_MinBloodOxygenTST_ini = false;
        private DateTime m_MinBloodOxygenTSTStartTime = default(DateTime);
        private bool m_MinBloodOxygenTSTStartTime_ini = false;
        private float m_LongBloodOxygenTST = 0;
        private bool m_LongBloodOxygenTST_ini = false;
        private DateTime m_LongBloodOxygenTSTStartTime = default(DateTime);
        private bool m_LongBloodOxygenTSTStartTime_ini = false;
        private float m_AverageBloodOxygenTSTTime = 0;
        private bool m_AverageBloodOxygenTSTTime_ini = false;
        private float m_MinBreathBloodOxygen = 0;
        private bool m_MinBreathBloodOxygen_ini = false;
        private float m_AveBloodOxygenTIB = 0;
        private bool m_AveBloodOxygenTIB_ini = false;
        private float m_AveBloodOxygenTST = 0;
        private bool m_AveBloodOxygenTST_ini = false;
        private float m_AveBloodOxygenREM = 0;
        private bool m_AveBloodOxygenREM_ini = false;
        private float m_AveBloodOxygenNREM = 0;
        private bool m_AveBloodOxygenNREM_ini = false;
        private float m_OxygenReduceIndex = 0;
        private bool m_OxygenReduceIndex_ini = false;
        private float m_OxygenReduceCount = 0;
        private bool m_OxygenReduceCount_ini = false;
        private float m_WakeBloodOxygenLowThan98 = 0;
        private bool m_WakeBloodOxygenLowThan98_ini = false;
        private float m_WakeBloodOxygenLowThan90 = 0;
        private bool m_WakeBloodOxygenLowThan90_ini = false;
        private float m_WakeBloodOxygenLowThan85 = 0;
        private bool m_WakeBloodOxygenLowThan85_ini = false;
        private float m_WakeBloodOxygenLowThan80 = 0;
        private bool m_WakeBloodOxygenLowThan80_ini = false;
        private float m_WakeBloodOxygenLowThan60 = 0;
        private bool m_WakeBloodOxygenLowThan60_ini = false;
        private float m_WakeOxygenReduceIndex = 0;
        private bool m_WakeOxygenReduceIndex_ini = false;
        private float m_WakeOxygenReduceMaxDuration = 0;
        private bool m_WakeOxygenReduceMaxDuration_ini = false;
        private float m_REMBloodOxygenLowThan98 = 0;
        private bool m_REMBloodOxygenLowThan98_ini = false;
        private float m_REMBloodOxygenLowThan90 = 0;
        private bool m_REMBloodOxygenLowThan90_ini = false;
        private float m_REMBloodOxygenLowThan85 = 0;
        private bool m_REMBloodOxygenLowThan85_ini = false;
        private float m_REMBloodOxygenLowThan80 = 0;
        private bool m_REMBloodOxygenLowThan80_ini = false;
        private float m_REMBloodOxygenLowThan60 = 0;
        private bool m_REMBloodOxygenLowThan60_ini = false;
        private float m_REMOxygenReduceIndex = 0;
        private bool m_REMOxygenReduceIndex_ini = false;
        private float m_REMOxygenReduceMaxDuration = 0;
        private bool m_REMOxygenReduceMaxDuration_ini = false;
        private float m_NREMBloodOxygenLowThan98 = 0;
        private bool m_NREMBloodOxygenLowThan98_ini = false;
        private float m_NREMBloodOxygenLowThan90 = 0;
        private bool m_NREMBloodOxygenLowThan90_ini = false;
        private float m_NREMBloodOxygenLowThan85 = 0;
        private bool m_NREMBloodOxygenLowThan85_ini = false;
        private float m_NREMBloodOxygenLowThan80 = 0;
        private bool m_NREMBloodOxygenLowThan80_ini = false;
        private float m_NREMBloodOxygenLowThan60 = 0;
        private bool m_NREMBloodOxygenLowThan60_ini = false;
        private float m_NREMOxygenReduceIndex = 0;
        private bool m_NREMOxygenReduceIndex_ini = false;
        private float m_NREMOxygenReduceMaxDuration = 0;
        private bool m_NREMOxygenReduceMaxDuration_ini = false;
        private float m_TotalBloodOxygenLowThan98 = 0;
        private bool m_TotalBloodOxygenLowThan98_ini = false;
        private float m_TotalBloodOxygenLowThan90 = 0;
        private bool m_TotalBloodOxygenLowThan90_ini = false;
        private float m_TotalBloodOxygenLowThan85 = 0;
        private bool m_TotalBloodOxygenLowThan85_ini = false;
        private float m_TotalBloodOxygenLowThan80 = 0;
        private bool m_TotalBloodOxygenLowThan80_ini = false;
        private float m_TotalBloodOxygenLowThan60 = 0;
        private bool m_TotalBloodOxygenLowThan60_ini = false;
        private float m_TotalOxygenReduceIndex = 0;
        private bool m_TotalOxygenReduceIndex_ini = false;
        private float m_TotalOxygenReduceMaxDuration = 0;
        private bool m_TotalOxygenReduceMaxDuration_ini = false;
        private float m_BloodOxygenLowThan90DurationPercent = 0;
        private bool m_BloodOxygenLowThan90DurationPercent_ini = false;
        private float m_BloodOxygenLowThan90Duration = 0;
        private bool m_BloodOxygenLowThan90Duration_ini = false;
        private float m_MinHypopneaBloodOxygen = 0;
        private bool m_MinHypopneaBloodOxygen_ini = false;
        private float m_MinApneaBloodOxygen = 0;
        private bool m_MinApneaBloodOxygen_ini = false;
        private int m_MinBloodOxygenTSTBreathEventTyp = 0;
        private bool m_MinBloodOxygenTSTBreathEventTyp_ini = false;
        #endregion
        #region 继承成员
        public string GetInsertString()
        {
            string arg = " (";
            string text = " VALUES(";
            bool flag = false;
            if (this.m_GUID_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "GUID", flag ? "," : "");
                text = string.Format("{0}{2}'{1}'", text, this.m_GUID, flag ? "," : "");
                flag = true;
            }
            if (this.m_BloodOxygenLowDegreeLevel_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "BloodOxygenLowDegreeLevel", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_BloodOxygenLowDegreeLevel, flag ? "," : "");
                flag = true;
            }
            if (this.m_MinLimitBloodOxygenDuration_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "MinLimitBloodOxygenDuration", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_MinLimitBloodOxygenDuration, flag ? "," : "");
                flag = true;
            }
            if (this.m_MinBloodOxygenTIB_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "MinBloodOxygenTIB", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_MinBloodOxygenTIB, flag ? "," : "");
                flag = true;
            }
            if (this.m_MinBloodOxygenTST_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "MinBloodOxygenTST", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_MinBloodOxygenTST, flag ? "," : "");
                flag = true;
            }
            if (this.m_MinBloodOxygenTSTStartTime_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "MinBloodOxygenTSTStartTime", flag ? "," : "");
                text = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", text, m_MinBloodOxygenTSTStartTime, flag ? "," : "");
                flag = true;
            }
            if (this.m_LongBloodOxygenTST_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "LongBloodOxygenTST", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_LongBloodOxygenTST, flag ? "," : "");
                flag = true;
            }
            if (this.m_LongBloodOxygenTSTStartTime_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "LongBloodOxygenTSTStartTime", flag ? "," : "");
                text = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", text, m_LongBloodOxygenTSTStartTime, flag ? "," : "");
                flag = true;
            }
            if (this.m_AverageBloodOxygenTSTTime_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "AverageBloodOxygenTSTTime", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_AverageBloodOxygenTSTTime, flag ? "," : "");
                flag = true;
            }
            if (this.m_MinBreathBloodOxygen_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "MinBreathBloodOxygen", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_MinBreathBloodOxygen, flag ? "," : "");
                flag = true;
            }
            if (this.m_AveBloodOxygenTIB_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "AveBloodOxygenTIB", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_AveBloodOxygenTIB, flag ? "," : "");
                flag = true;
            }
            if (this.m_AveBloodOxygenTST_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "AveBloodOxygenTST", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_AveBloodOxygenTST, flag ? "," : "");
                flag = true;
            }
            if (this.m_AveBloodOxygenREM_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "AveBloodOxygenREM", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_AveBloodOxygenREM, flag ? "," : "");
                flag = true;
            }
            if (this.m_AveBloodOxygenNREM_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "AveBloodOxygenNREM", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_AveBloodOxygenNREM, flag ? "," : "");
                flag = true;
            }
            if (this.m_OxygenReduceIndex_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "OxygenReduceIndex", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_OxygenReduceIndex, flag ? "," : "");
                flag = true;
            }
            if (this.m_OxygenReduceCount_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "OxygenReduceCount", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_OxygenReduceCount, flag ? "," : "");
                flag = true;
            }
            if (this.m_WakeBloodOxygenLowThan98_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "WakeBloodOxygenLowThan98", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_WakeBloodOxygenLowThan98, flag ? "," : "");
                flag = true;
            }
            if (this.m_WakeBloodOxygenLowThan90_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "WakeBloodOxygenLowThan90", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_WakeBloodOxygenLowThan90, flag ? "," : "");
                flag = true;
            }
            if (this.m_WakeBloodOxygenLowThan85_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "WakeBloodOxygenLowThan85", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_WakeBloodOxygenLowThan85, flag ? "," : "");
                flag = true;
            }
            if (this.m_WakeBloodOxygenLowThan80_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "WakeBloodOxygenLowThan80", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_WakeBloodOxygenLowThan80, flag ? "," : "");
                flag = true;
            }
            if (this.m_WakeBloodOxygenLowThan60_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "WakeBloodOxygenLowThan60", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_WakeBloodOxygenLowThan60, flag ? "," : "");
                flag = true;
            }
            if (this.m_WakeOxygenReduceIndex_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "WakeOxygenReduceIndex", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_WakeOxygenReduceIndex, flag ? "," : "");
                flag = true;
            }
            if (this.m_WakeOxygenReduceMaxDuration_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "WakeOxygenReduceMaxDuration", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_WakeOxygenReduceMaxDuration, flag ? "," : "");
                flag = true;
            }
            if (this.m_REMBloodOxygenLowThan98_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "REMBloodOxygenLowThan98", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_REMBloodOxygenLowThan98, flag ? "," : "");
                flag = true;
            }
            if (this.m_REMBloodOxygenLowThan90_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "REMBloodOxygenLowThan90", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_REMBloodOxygenLowThan90, flag ? "," : "");
                flag = true;
            }
            if (this.m_REMBloodOxygenLowThan85_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "REMBloodOxygenLowThan85", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_REMBloodOxygenLowThan85, flag ? "," : "");
                flag = true;
            }
            if (this.m_REMBloodOxygenLowThan80_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "REMBloodOxygenLowThan80", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_REMBloodOxygenLowThan80, flag ? "," : "");
                flag = true;
            }
            if (this.m_REMBloodOxygenLowThan60_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "REMBloodOxygenLowThan60", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_REMBloodOxygenLowThan60, flag ? "," : "");
                flag = true;
            }
            if (this.m_REMOxygenReduceIndex_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "REMOxygenReduceIndex", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_REMOxygenReduceIndex, flag ? "," : "");
                flag = true;
            }
            if (this.m_REMOxygenReduceMaxDuration_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "REMOxygenReduceMaxDuration", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_REMOxygenReduceMaxDuration, flag ? "," : "");
                flag = true;
            }
            if (this.m_NREMBloodOxygenLowThan98_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "NREMBloodOxygenLowThan98", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_NREMBloodOxygenLowThan98, flag ? "," : "");
                flag = true;
            }
            if (this.m_NREMBloodOxygenLowThan90_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "NREMBloodOxygenLowThan90", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_NREMBloodOxygenLowThan90, flag ? "," : "");
                flag = true;
            }
            if (this.m_NREMBloodOxygenLowThan85_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "NREMBloodOxygenLowThan85", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_NREMBloodOxygenLowThan85, flag ? "," : "");
                flag = true;
            }
            if (this.m_NREMBloodOxygenLowThan80_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "NREMBloodOxygenLowThan80", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_NREMBloodOxygenLowThan80, flag ? "," : "");
                flag = true;
            }
            if (this.m_NREMBloodOxygenLowThan60_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "NREMBloodOxygenLowThan60", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_NREMBloodOxygenLowThan60, flag ? "," : "");
                flag = true;
            }
            if (this.m_NREMOxygenReduceIndex_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "NREMOxygenReduceIndex", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_NREMOxygenReduceIndex, flag ? "," : "");
                flag = true;
            }
            if (this.m_NREMOxygenReduceMaxDuration_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "NREMOxygenReduceMaxDuration", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_NREMOxygenReduceMaxDuration, flag ? "," : "");
                flag = true;
            }
            if (this.m_TotalBloodOxygenLowThan98_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "TotalBloodOxygenLowThan98", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_TotalBloodOxygenLowThan98, flag ? "," : "");
                flag = true;
            }
            if (this.m_TotalBloodOxygenLowThan90_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "TotalBloodOxygenLowThan90", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_TotalBloodOxygenLowThan90, flag ? "," : "");
                flag = true;
            }
            if (this.m_TotalBloodOxygenLowThan85_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "TotalBloodOxygenLowThan85", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_TotalBloodOxygenLowThan85, flag ? "," : "");
                flag = true;
            }
            if (this.m_TotalBloodOxygenLowThan80_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "TotalBloodOxygenLowThan80", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_TotalBloodOxygenLowThan80, flag ? "," : "");
                flag = true;
            }
            if (this.m_TotalBloodOxygenLowThan60_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "TotalBloodOxygenLowThan60", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_TotalBloodOxygenLowThan60, flag ? "," : "");
                flag = true;
            }
            if (this.m_TotalOxygenReduceIndex_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "TotalOxygenReduceIndex", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_TotalOxygenReduceIndex, flag ? "," : "");
                flag = true;
            }
            if (this.m_TotalOxygenReduceMaxDuration_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "TotalOxygenReduceMaxDuration", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_TotalOxygenReduceMaxDuration, flag ? "," : "");
                flag = true;
            }
            if (this.m_BloodOxygenLowThan90DurationPercent_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "BloodOxygenLowThan90DurationPercent", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_BloodOxygenLowThan90DurationPercent, flag ? "," : "");
                flag = true;
            }
            if (this.m_BloodOxygenLowThan90Duration_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "BloodOxygenLowThan90Duration", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_BloodOxygenLowThan90Duration, flag ? "," : "");
                flag = true;
            }
            if (this.m_MinHypopneaBloodOxygen_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "MinHypopneaBloodOxygen", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_MinHypopneaBloodOxygen, flag ? "," : "");
                flag = true;
            }
            if (this.m_MinApneaBloodOxygen_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "MinApneaBloodOxygen", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_MinApneaBloodOxygen, flag ? "," : "");
                flag = true;
            }
            if (this.m_MinBloodOxygenTSTBreathEventTyp_ini)
            {
                arg = string.Format("{0}{2}{1}", arg, "MinBloodOxygenTSTBreathEventTyp", flag ? "," : "");
                text = string.Format("{0}{2}{1}", text, this.m_MinBloodOxygenTSTBreathEventTyp, flag ? "," : "");
                flag = true;
            }
            if (!flag)
            {
                return "";
            }
            return string.Format("{0})\r\n{1})", arg, text);
        }
        public string GetSelectString()
        {
            bool flag = false;
            string text = " ";
            if (this.m_GUID_ini)
            {
                text = string.Format("{0}GUID='{1}'", flag ? string.Format("{0} AND ", text) : "", this.m_GUID);
                flag = true;
            }
            if (this.m_BloodOxygenLowDegreeLevel_ini)
            {
                text = string.Format("{0}BloodOxygenLowDegreeLevel={1}", flag ? string.Format("{0} AND ", text) : "", this.m_BloodOxygenLowDegreeLevel);
                flag = true;
            }
            if (this.m_MinLimitBloodOxygenDuration_ini)
            {
                text = string.Format("{0}MinLimitBloodOxygenDuration={1}", flag ? string.Format("{0} AND ", text) : "", this.m_MinLimitBloodOxygenDuration);
                flag = true;
            }
            if (this.m_MinBloodOxygenTIB_ini)
            {
                text = string.Format("{0}MinBloodOxygenTIB={1}", flag ? string.Format("{0} AND ", text) : "", this.m_MinBloodOxygenTIB);
                flag = true;
            }
            if (this.m_MinBloodOxygenTST_ini)
            {
                text = string.Format("{0}MinBloodOxygenTST={1}", flag ? string.Format("{0} AND ", text) : "", this.m_MinBloodOxygenTST);
                flag = true;
            }
            if (m_MinBloodOxygenTSTStartTime_ini)
            {
                text = string.Format("{0}MinBloodOxygenTSTStartTime='{1:yyyy-MM-dd HH:mm:ss}'", flag ? string.Format("{0} AND ", text) : "", m_MinBloodOxygenTSTStartTime);
                flag = true;
            }
            if (this.m_LongBloodOxygenTST_ini)
            {
                text = string.Format("{0}LongBloodOxygenTST={1}", flag ? string.Format("{0} AND ", text) : "", this.m_LongBloodOxygenTST);
                flag = true;
            }
            if (m_LongBloodOxygenTSTStartTime_ini)
            {
                text = string.Format("{0}LongBloodOxygenTSTStartTime='{1:yyyy-MM-dd HH:mm:ss}'", flag ? string.Format("{0} AND ", text) : "", m_LongBloodOxygenTSTStartTime);
                flag = true;
            }
            if (this.m_AverageBloodOxygenTSTTime_ini)
            {
                text = string.Format("{0}AverageBloodOxygenTSTTime={1}", flag ? string.Format("{0} AND ", text) : "", this.m_AverageBloodOxygenTSTTime);
                flag = true;
            }
            if (this.m_MinBreathBloodOxygen_ini)
            {
                text = string.Format("{0}MinBreathBloodOxygen={1}", flag ? string.Format("{0} AND ", text) : "", this.m_MinBreathBloodOxygen);
                flag = true;
            }
            if (this.m_AveBloodOxygenTIB_ini)
            {
                text = string.Format("{0}AveBloodOxygenTIB={1}", flag ? string.Format("{0} AND ", text) : "", this.m_AveBloodOxygenTIB);
                flag = true;
            }
            if (this.m_AveBloodOxygenTST_ini)
            {
                text = string.Format("{0}AveBloodOxygenTST={1}", flag ? string.Format("{0} AND ", text) : "", this.m_AveBloodOxygenTST);
                flag = true;
            }
            if (this.m_AveBloodOxygenREM_ini)
            {
                text = string.Format("{0}AveBloodOxygenREM={1}", flag ? string.Format("{0} AND ", text) : "", this.m_AveBloodOxygenREM);
                flag = true;
            }
            if (this.m_AveBloodOxygenNREM_ini)
            {
                text = string.Format("{0}AveBloodOxygenNREM={1}", flag ? string.Format("{0} AND ", text) : "", this.m_AveBloodOxygenNREM);
                flag = true;
            }
            if (this.m_OxygenReduceIndex_ini)
            {
                text = string.Format("{0}OxygenReduceIndex={1}", flag ? string.Format("{0} AND ", text) : "", this.m_OxygenReduceIndex);
                flag = true;
            }
            if (this.m_OxygenReduceCount_ini)
            {
                text = string.Format("{0}OxygenReduceCount={1}", flag ? string.Format("{0} AND ", text) : "", this.m_OxygenReduceCount);
                flag = true;
            }
            if (this.m_WakeBloodOxygenLowThan98_ini)
            {
                text = string.Format("{0}WakeBloodOxygenLowThan98={1}", flag ? string.Format("{0} AND ", text) : "", this.m_WakeBloodOxygenLowThan98);
                flag = true;
            }
            if (this.m_WakeBloodOxygenLowThan90_ini)
            {
                text = string.Format("{0}WakeBloodOxygenLowThan90={1}", flag ? string.Format("{0} AND ", text) : "", this.m_WakeBloodOxygenLowThan90);
                flag = true;
            }
            if (this.m_WakeBloodOxygenLowThan85_ini)
            {
                text = string.Format("{0}WakeBloodOxygenLowThan85={1}", flag ? string.Format("{0} AND ", text) : "", this.m_WakeBloodOxygenLowThan85);
                flag = true;
            }
            if (this.m_WakeBloodOxygenLowThan80_ini)
            {
                text = string.Format("{0}WakeBloodOxygenLowThan80={1}", flag ? string.Format("{0} AND ", text) : "", this.m_WakeBloodOxygenLowThan80);
                flag = true;
            }
            if (this.m_WakeBloodOxygenLowThan60_ini)
            {
                text = string.Format("{0}WakeBloodOxygenLowThan60={1}", flag ? string.Format("{0} AND ", text) : "", this.m_WakeBloodOxygenLowThan60);
                flag = true;
            }
            if (this.m_WakeOxygenReduceIndex_ini)
            {
                text = string.Format("{0}WakeOxygenReduceIndex={1}", flag ? string.Format("{0} AND ", text) : "", this.m_WakeOxygenReduceIndex);
                flag = true;
            }
            if (this.m_WakeOxygenReduceMaxDuration_ini)
            {
                text = string.Format("{0}WakeOxygenReduceMaxDuration={1}", flag ? string.Format("{0} AND ", text) : "", this.m_WakeOxygenReduceMaxDuration);
                flag = true;
            }
            if (this.m_REMBloodOxygenLowThan98_ini)
            {
                text = string.Format("{0}REMBloodOxygenLowThan98={1}", flag ? string.Format("{0} AND ", text) : "", this.m_REMBloodOxygenLowThan98);
                flag = true;
            }
            if (this.m_REMBloodOxygenLowThan90_ini)
            {
                text = string.Format("{0}REMBloodOxygenLowThan90={1}", flag ? string.Format("{0} AND ", text) : "", this.m_REMBloodOxygenLowThan90);
                flag = true;
            }
            if (this.m_REMBloodOxygenLowThan85_ini)
            {
                text = string.Format("{0}REMBloodOxygenLowThan85={1}", flag ? string.Format("{0} AND ", text) : "", this.m_REMBloodOxygenLowThan85);
                flag = true;
            }
            if (this.m_REMBloodOxygenLowThan80_ini)
            {
                text = string.Format("{0}REMBloodOxygenLowThan80={1}", flag ? string.Format("{0} AND ", text) : "", this.m_REMBloodOxygenLowThan80);
                flag = true;
            }
            if (this.m_REMBloodOxygenLowThan60_ini)
            {
                text = string.Format("{0}REMBloodOxygenLowThan60={1}", flag ? string.Format("{0} AND ", text) : "", this.m_REMBloodOxygenLowThan60);
                flag = true;
            }
            if (this.m_REMOxygenReduceIndex_ini)
            {
                text = string.Format("{0}REMOxygenReduceIndex={1}", flag ? string.Format("{0} AND ", text) : "", this.m_REMOxygenReduceIndex);
                flag = true;
            }
            if (this.m_REMOxygenReduceMaxDuration_ini)
            {
                text = string.Format("{0}REMOxygenReduceMaxDuration={1}", flag ? string.Format("{0} AND ", text) : "", this.m_REMOxygenReduceMaxDuration);
                flag = true;
            }
            if (this.m_NREMBloodOxygenLowThan98_ini)
            {
                text = string.Format("{0}NREMBloodOxygenLowThan98={1}", flag ? string.Format("{0} AND ", text) : "", this.m_NREMBloodOxygenLowThan98);
                flag = true;
            }
            if (this.m_NREMBloodOxygenLowThan90_ini)
            {
                text = string.Format("{0}NREMBloodOxygenLowThan90={1}", flag ? string.Format("{0} AND ", text) : "", this.m_NREMBloodOxygenLowThan90);
                flag = true;
            }
            if (this.m_NREMBloodOxygenLowThan85_ini)
            {
                text = string.Format("{0}NREMBloodOxygenLowThan85={1}", flag ? string.Format("{0} AND ", text) : "", this.m_NREMBloodOxygenLowThan85);
                flag = true;
            }
            if (this.m_NREMBloodOxygenLowThan80_ini)
            {
                text = string.Format("{0}NREMBloodOxygenLowThan80={1}", flag ? string.Format("{0} AND ", text) : "", this.m_NREMBloodOxygenLowThan80);
                flag = true;
            }
            if (this.m_NREMBloodOxygenLowThan60_ini)
            {
                text = string.Format("{0}NREMBloodOxygenLowThan60={1}", flag ? string.Format("{0} AND ", text) : "", this.m_NREMBloodOxygenLowThan60);
                flag = true;
            }
            if (this.m_NREMOxygenReduceIndex_ini)
            {
                text = string.Format("{0}NREMOxygenReduceIndex={1}", flag ? string.Format("{0} AND ", text) : "", this.m_NREMOxygenReduceIndex);
                flag = true;
            }
            if (this.m_NREMOxygenReduceMaxDuration_ini)
            {
                text = string.Format("{0}NREMOxygenReduceMaxDuration={1}", flag ? string.Format("{0} AND ", text) : "", this.m_NREMOxygenReduceMaxDuration);
                flag = true;
            }
            if (this.m_TotalBloodOxygenLowThan98_ini)
            {
                text = string.Format("{0}TotalBloodOxygenLowThan98={1}", flag ? string.Format("{0} AND ", text) : "", this.m_TotalBloodOxygenLowThan98);
                flag = true;
            }
            if (this.m_TotalBloodOxygenLowThan90_ini)
            {
                text = string.Format("{0}TotalBloodOxygenLowThan90={1}", flag ? string.Format("{0} AND ", text) : "", this.m_TotalBloodOxygenLowThan90);
                flag = true;
            }
            if (this.m_TotalBloodOxygenLowThan85_ini)
            {
                text = string.Format("{0}TotalBloodOxygenLowThan85={1}", flag ? string.Format("{0} AND ", text) : "", this.m_TotalBloodOxygenLowThan85);
                flag = true;
            }
            if (this.m_TotalBloodOxygenLowThan80_ini)
            {
                text = string.Format("{0}TotalBloodOxygenLowThan80={1}", flag ? string.Format("{0} AND ", text) : "", this.m_TotalBloodOxygenLowThan80);
                flag = true;
            }
            if (this.m_TotalBloodOxygenLowThan60_ini)
            {
                text = string.Format("{0}TotalBloodOxygenLowThan60={1}", flag ? string.Format("{0} AND ", text) : "", this.m_TotalBloodOxygenLowThan60);
                flag = true;
            }
            if (this.m_TotalOxygenReduceIndex_ini)
            {
                text = string.Format("{0}TotalOxygenReduceIndex={1}", flag ? string.Format("{0} AND ", text) : "", this.m_TotalOxygenReduceIndex);
                flag = true;
            }
            if (this.m_TotalOxygenReduceMaxDuration_ini)
            {
                text = string.Format("{0}TotalOxygenReduceMaxDuration={1}", flag ? string.Format("{0} AND ", text) : "", this.m_TotalOxygenReduceMaxDuration);
                flag = true;
            }
            if (this.m_BloodOxygenLowThan90DurationPercent_ini)
            {
                text = string.Format("{0}BloodOxygenLowThan90DurationPercent={1}", flag ? string.Format("{0} AND ", text) : "", this.m_BloodOxygenLowThan90DurationPercent);
                flag = true;
            }
            if (this.m_BloodOxygenLowThan90Duration_ini)
            {
                text = string.Format("{0}BloodOxygenLowThan90Duration={1}", flag ? string.Format("{0} AND ", text) : "", this.m_BloodOxygenLowThan90Duration);
                flag = true;
            }
            if (this.m_MinHypopneaBloodOxygen_ini)
            {
                text = string.Format("{0}MinHypopneaBloodOxygen={1}", flag ? string.Format("{0} AND ", text) : "", this.m_MinHypopneaBloodOxygen);
                flag = true;
            }
            if (this.m_MinApneaBloodOxygen_ini)
            {
                text = string.Format("{0}MinApneaBloodOxygen={1}", flag ? string.Format("{0} AND ", text) : "", this.m_MinApneaBloodOxygen);
                flag = true;
            }
            if (this.m_MinBloodOxygenTSTBreathEventTyp_ini)
            {
                text = string.Format("{0}MinBloodOxygenTSTBreathEventTyp={1}", flag ? string.Format("{0} AND ", text) : "", this.m_MinBloodOxygenTSTBreathEventTyp);
                flag = true;
            }
            if (!flag)
            {
                return "";
            }
            return text;
        }
        public string GetUpdateString()
        {
            string text = "SET ";
            bool flag = false;
            if (this.m_GUID_ini)
            {
                text = string.Format("{0}{2}GUID='{1}'", text, this.m_GUID, flag ? "," : "");
                flag = true;
            }
            if (this.m_BloodOxygenLowDegreeLevel_ini)
            {
                text = string.Format("{0}{2}BloodOxygenLowDegreeLevel={1}", text, this.m_BloodOxygenLowDegreeLevel, flag ? "," : "");
                flag = true;
            }
            if (this.m_MinLimitBloodOxygenDuration_ini)
            {
                text = string.Format("{0}{2}MinLimitBloodOxygenDuration={1}", text, this.m_MinLimitBloodOxygenDuration, flag ? "," : "");
                flag = true;
            }
            if (this.m_MinBloodOxygenTIB_ini)
            {
                text = string.Format("{0}{2}MinBloodOxygenTIB={1}", text, this.m_MinBloodOxygenTIB, flag ? "," : "");
                flag = true;
            }
            if (this.m_MinBloodOxygenTST_ini)
            {
                text = string.Format("{0}{2}MinBloodOxygenTST={1}", text, this.m_MinBloodOxygenTST, flag ? "," : "");
                flag = true;
            }
            if (m_MinBloodOxygenTSTStartTime_ini)
            {
                text = string.Format("{0}{2}MinBloodOxygenTSTStartTime='{1:yyyy-MM-dd HH:mm:ss}'", text, m_MinBloodOxygenTSTStartTime, flag ? "," : "");
                flag = true;
            }
            if (this.m_LongBloodOxygenTST_ini)
            {
                text = string.Format("{0}{2}LongBloodOxygenTST={1}", text, this.m_LongBloodOxygenTST, flag ? "," : "");
                flag = true;
            }
            if (m_LongBloodOxygenTSTStartTime_ini)
            {
                text = string.Format("{0}{2}LongBloodOxygenTSTStartTime='{1:yyyy-MM-dd HH:mm:ss}'", text, m_LongBloodOxygenTSTStartTime, flag ? "," : "");
                flag = true;
            }
            if (this.m_AverageBloodOxygenTSTTime_ini)
            {
                text = string.Format("{0}{2}AverageBloodOxygenTSTTime={1}", text, this.m_AverageBloodOxygenTSTTime, flag ? "," : "");
                flag = true;
            }
            if (this.m_MinBreathBloodOxygen_ini)
            {
                text = string.Format("{0}{2}MinBreathBloodOxygen={1}", text, this.m_MinBreathBloodOxygen, flag ? "," : "");
                flag = true;
            }
            if (this.m_AveBloodOxygenTIB_ini)
            {
                text = string.Format("{0}{2}AveBloodOxygenTIB={1}", text, this.m_AveBloodOxygenTIB, flag ? "," : "");
                flag = true;
            }
            if (this.m_AveBloodOxygenTST_ini)
            {
                text = string.Format("{0}{2}AveBloodOxygenTST={1}", text, this.m_AveBloodOxygenTST, flag ? "," : "");
                flag = true;
            }
            if (this.m_AveBloodOxygenREM_ini)
            {
                text = string.Format("{0}{2}AveBloodOxygenREM={1}", text, this.m_AveBloodOxygenREM, flag ? "," : "");
                flag = true;
            }
            if (this.m_AveBloodOxygenNREM_ini)
            {
                text = string.Format("{0}{2}AveBloodOxygenNREM={1}", text, this.m_AveBloodOxygenNREM, flag ? "," : "");
                flag = true;
            }
            if (this.m_OxygenReduceIndex_ini)
            {
                text = string.Format("{0}{2}OxygenReduceIndex={1}", text, this.m_OxygenReduceIndex, flag ? "," : "");
                flag = true;
            }
            if (this.m_OxygenReduceCount_ini)
            {
                text = string.Format("{0}{2}OxygenReduceCount={1}", text, this.m_OxygenReduceCount, flag ? "," : "");
                flag = true;
            }
            if (this.m_WakeBloodOxygenLowThan98_ini)
            {
                text = string.Format("{0}{2}WakeBloodOxygenLowThan98={1}", text, this.m_WakeBloodOxygenLowThan98, flag ? "," : "");
                flag = true;
            }
            if (this.m_WakeBloodOxygenLowThan90_ini)
            {
                text = string.Format("{0}{2}WakeBloodOxygenLowThan90={1}", text, this.m_WakeBloodOxygenLowThan90, flag ? "," : "");
                flag = true;
            }
            if (this.m_WakeBloodOxygenLowThan85_ini)
            {
                text = string.Format("{0}{2}WakeBloodOxygenLowThan85={1}", text, this.m_WakeBloodOxygenLowThan85, flag ? "," : "");
                flag = true;
            }
            if (this.m_WakeBloodOxygenLowThan80_ini)
            {
                text = string.Format("{0}{2}WakeBloodOxygenLowThan80={1}", text, this.m_WakeBloodOxygenLowThan80, flag ? "," : "");
                flag = true;
            }
            if (this.m_WakeBloodOxygenLowThan60_ini)
            {
                text = string.Format("{0}{2}WakeBloodOxygenLowThan60={1}", text, this.m_WakeBloodOxygenLowThan60, flag ? "," : "");
                flag = true;
            }
            if (this.m_WakeOxygenReduceIndex_ini)
            {
                text = string.Format("{0}{2}WakeOxygenReduceIndex={1}", text, this.m_WakeOxygenReduceIndex, flag ? "," : "");
                flag = true;
            }
            if (this.m_WakeOxygenReduceMaxDuration_ini)
            {
                text = string.Format("{0}{2}WakeOxygenReduceMaxDuration={1}", text, this.m_WakeOxygenReduceMaxDuration, flag ? "," : "");
                flag = true;
            }
            if (this.m_REMBloodOxygenLowThan98_ini)
            {
                text = string.Format("{0}{2}REMBloodOxygenLowThan98={1}", text, this.m_REMBloodOxygenLowThan98, flag ? "," : "");
                flag = true;
            }
            if (this.m_REMBloodOxygenLowThan90_ini)
            {
                text = string.Format("{0}{2}REMBloodOxygenLowThan90={1}", text, this.m_REMBloodOxygenLowThan90, flag ? "," : "");
                flag = true;
            }
            if (this.m_REMBloodOxygenLowThan85_ini)
            {
                text = string.Format("{0}{2}REMBloodOxygenLowThan85={1}", text, this.m_REMBloodOxygenLowThan85, flag ? "," : "");
                flag = true;
            }
            if (this.m_REMBloodOxygenLowThan80_ini)
            {
                text = string.Format("{0}{2}REMBloodOxygenLowThan80={1}", text, this.m_REMBloodOxygenLowThan80, flag ? "," : "");
                flag = true;
            }
            if (this.m_REMBloodOxygenLowThan60_ini)
            {
                text = string.Format("{0}{2}REMBloodOxygenLowThan60={1}", text, this.m_REMBloodOxygenLowThan60, flag ? "," : "");
                flag = true;
            }
            if (this.m_REMOxygenReduceIndex_ini)
            {
                text = string.Format("{0}{2}REMOxygenReduceIndex={1}", text, this.m_REMOxygenReduceIndex, flag ? "," : "");
                flag = true;
            }
            if (this.m_REMOxygenReduceMaxDuration_ini)
            {
                text = string.Format("{0}{2}REMOxygenReduceMaxDuration={1}", text, this.m_REMOxygenReduceMaxDuration, flag ? "," : "");
                flag = true;
            }
            if (this.m_NREMBloodOxygenLowThan98_ini)
            {
                text = string.Format("{0}{2}NREMBloodOxygenLowThan98={1}", text, this.m_NREMBloodOxygenLowThan98, flag ? "," : "");
                flag = true;
            }
            if (this.m_NREMBloodOxygenLowThan90_ini)
            {
                text = string.Format("{0}{2}NREMBloodOxygenLowThan90={1}", text, this.m_NREMBloodOxygenLowThan90, flag ? "," : "");
                flag = true;
            }
            if (this.m_NREMBloodOxygenLowThan85_ini)
            {
                text = string.Format("{0}{2}NREMBloodOxygenLowThan85={1}", text, this.m_NREMBloodOxygenLowThan85, flag ? "," : "");
                flag = true;
            }
            if (this.m_NREMBloodOxygenLowThan80_ini)
            {
                text = string.Format("{0}{2}NREMBloodOxygenLowThan80={1}", text, this.m_NREMBloodOxygenLowThan80, flag ? "," : "");
                flag = true;
            }
            if (this.m_NREMBloodOxygenLowThan60_ini)
            {
                text = string.Format("{0}{2}NREMBloodOxygenLowThan60={1}", text, this.m_NREMBloodOxygenLowThan60, flag ? "," : "");
                flag = true;
            }
            if (this.m_NREMOxygenReduceIndex_ini)
            {
                text = string.Format("{0}{2}NREMOxygenReduceIndex={1}", text, this.m_NREMOxygenReduceIndex, flag ? "," : "");
                flag = true;
            }
            if (this.m_NREMOxygenReduceMaxDuration_ini)
            {
                text = string.Format("{0}{2}NREMOxygenReduceMaxDuration={1}", text, this.m_NREMOxygenReduceMaxDuration, flag ? "," : "");
                flag = true;
            }
            if (this.m_TotalBloodOxygenLowThan98_ini)
            {
                text = string.Format("{0}{2}TotalBloodOxygenLowThan98={1}", text, this.m_TotalBloodOxygenLowThan98, flag ? "," : "");
                flag = true;
            }
            if (this.m_TotalBloodOxygenLowThan90_ini)
            {
                text = string.Format("{0}{2}TotalBloodOxygenLowThan90={1}", text, this.m_TotalBloodOxygenLowThan90, flag ? "," : "");
                flag = true;
            }
            if (this.m_TotalBloodOxygenLowThan85_ini)
            {
                text = string.Format("{0}{2}TotalBloodOxygenLowThan85={1}", text, this.m_TotalBloodOxygenLowThan85, flag ? "," : "");
                flag = true;
            }
            if (this.m_TotalBloodOxygenLowThan80_ini)
            {
                text = string.Format("{0}{2}TotalBloodOxygenLowThan80={1}", text, this.m_TotalBloodOxygenLowThan80, flag ? "," : "");
                flag = true;
            }
            if (this.m_TotalBloodOxygenLowThan60_ini)
            {
                text = string.Format("{0}{2}TotalBloodOxygenLowThan60={1}", text, this.m_TotalBloodOxygenLowThan60, flag ? "," : "");
                flag = true;
            }
            if (this.m_TotalOxygenReduceIndex_ini)
            {
                text = string.Format("{0}{2}TotalOxygenReduceIndex={1}", text, this.m_TotalOxygenReduceIndex, flag ? "," : "");
                flag = true;
            }
            if (this.m_TotalOxygenReduceMaxDuration_ini)
            {
                text = string.Format("{0}{2}TotalOxygenReduceMaxDuration={1}", text, this.m_TotalOxygenReduceMaxDuration, flag ? "," : "");
                flag = true;
            }
            if (this.m_BloodOxygenLowThan90DurationPercent_ini)
            {
                text = string.Format("{0}{2}BloodOxygenLowThan90DurationPercent={1}", text, this.m_BloodOxygenLowThan90DurationPercent, flag ? "," : "");
                flag = true;
            }
            if (this.m_BloodOxygenLowThan90Duration_ini)
            {
                text = string.Format("{0}{2}BloodOxygenLowThan90Duration={1}", text, this.m_BloodOxygenLowThan90Duration, flag ? "," : "");
                flag = true;
            }
            if (this.m_MinHypopneaBloodOxygen_ini)
            {
                text = string.Format("{0}{2}MinHypopneaBloodOxygen={1}", text, this.m_MinHypopneaBloodOxygen, flag ? "," : "");
                flag = true;
            }
            if (this.m_MinApneaBloodOxygen_ini)
            {
                text = string.Format("{0}{2}MinApneaBloodOxygen={1}", text, this.m_MinApneaBloodOxygen, flag ? "," : "");
                flag = true;
            }
            if (this.m_MinBloodOxygenTSTBreathEventTyp_ini)
            {
                text = string.Format("{0}{2}MinBloodOxygenTSTBreathEventTyp={1}", text, this.m_MinBloodOxygenTSTBreathEventTyp, flag ? "," : "");
                flag = true;
            }
            if (!flag)
            {
                return "";
            }
            return text;
        }
        public string GetThisTableName()
        {
            return ("BloodOxygenResult");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "GUID":
                    this.GUID = Value.ToString();
                    return;
                case "BloodOxygenLowDegreeLevel":
                    this.BloodOxygenLowDegreeLevel = Convert.ToInt32(Value);
                    return;
                case "MinLimitBloodOxygenDuration":
                    this.MinLimitBloodOxygenDuration = Convert.ToSingle(Value);
                    return;
                case "MinBloodOxygenTIB":
                    this.MinBloodOxygenTIB = Convert.ToSingle(Value);
                    return;
                case "MinBloodOxygenTST":
                    this.MinBloodOxygenTST = Convert.ToSingle(Value);
                    return;
                case "MinBloodOxygenTSTStartTime":
                    MinBloodOxygenTSTStartTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "LongBloodOxygenTST":
                    this.LongBloodOxygenTST = Convert.ToSingle(Value);
                    return;
                case "LongBloodOxygenTSTStartTime":
                    LongBloodOxygenTSTStartTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "AverageBloodOxygenTSTTime":
                    this.AverageBloodOxygenTSTTime = Convert.ToSingle(Value);
                    return;
                case "MinBreathBloodOxygen":
                    this.MinBreathBloodOxygen = Convert.ToSingle(Value);
                    return;
                case "AveBloodOxygenTIB":
                    this.AveBloodOxygenTIB = Convert.ToSingle(Value);
                    return;
                case "AveBloodOxygenTST":
                    this.AveBloodOxygenTST = Convert.ToSingle(Value);
                    return;
                case "AveBloodOxygenREM":
                    this.AveBloodOxygenREM = Convert.ToSingle(Value);
                    return;
                case "AveBloodOxygenNREM":
                    this.AveBloodOxygenNREM = Convert.ToSingle(Value);
                    return;
                case "OxygenReduceIndex":
                    this.OxygenReduceIndex = Convert.ToSingle(Value);
                    return;
                case "OxygenReduceCount":
                    this.OxygenReduceCount = Convert.ToSingle(Value);
                    return;
                case "WakeBloodOxygenLowThan98":
                    this.WakeBloodOxygenLowThan98 = Convert.ToSingle(Value);
                    return;
                case "WakeBloodOxygenLowThan90":
                    this.WakeBloodOxygenLowThan90 = Convert.ToSingle(Value);
                    return;
                case "WakeBloodOxygenLowThan85":
                    this.WakeBloodOxygenLowThan85 = Convert.ToSingle(Value);
                    return;
                case "WakeBloodOxygenLowThan80":
                    this.WakeBloodOxygenLowThan80 = Convert.ToSingle(Value);
                    return;
                case "WakeBloodOxygenLowThan60":
                    this.WakeBloodOxygenLowThan60 = Convert.ToSingle(Value);
                    return;
                case "WakeOxygenReduceIndex":
                    this.WakeOxygenReduceIndex = Convert.ToSingle(Value);
                    return;
                case "WakeOxygenReduceMaxDuration":
                    this.WakeOxygenReduceMaxDuration = Convert.ToSingle(Value);
                    return;
                case "REMBloodOxygenLowThan98":
                    this.REMBloodOxygenLowThan98 = Convert.ToSingle(Value);
                    return;
                case "REMBloodOxygenLowThan90":
                    this.REMBloodOxygenLowThan90 = Convert.ToSingle(Value);
                    return;
                case "REMBloodOxygenLowThan85":
                    this.REMBloodOxygenLowThan85 = Convert.ToSingle(Value);
                    return;
                case "REMBloodOxygenLowThan80":
                    this.REMBloodOxygenLowThan80 = Convert.ToSingle(Value);
                    return;
                case "REMBloodOxygenLowThan60":
                    this.REMBloodOxygenLowThan60 = Convert.ToSingle(Value);
                    return;
                case "REMOxygenReduceIndex":
                    this.REMOxygenReduceIndex = Convert.ToSingle(Value);
                    return;
                case "REMOxygenReduceMaxDuration":
                    this.REMOxygenReduceMaxDuration = Convert.ToSingle(Value);
                    return;
                case "NREMBloodOxygenLowThan98":
                    this.NREMBloodOxygenLowThan98 = Convert.ToSingle(Value);
                    return;
                case "NREMBloodOxygenLowThan90":
                    this.NREMBloodOxygenLowThan90 = Convert.ToSingle(Value);
                    return;
                case "NREMBloodOxygenLowThan85":
                    this.NREMBloodOxygenLowThan85 = Convert.ToSingle(Value);
                    return;
                case "NREMBloodOxygenLowThan80":
                    this.NREMBloodOxygenLowThan80 = Convert.ToSingle(Value);
                    return;
                case "NREMBloodOxygenLowThan60":
                    this.NREMBloodOxygenLowThan60 = Convert.ToSingle(Value);
                    return;
                case "NREMOxygenReduceIndex":
                    this.NREMOxygenReduceIndex = Convert.ToSingle(Value);
                    return;
                case "NREMOxygenReduceMaxDuration":
                    this.NREMOxygenReduceMaxDuration = Convert.ToSingle(Value);
                    return;
                case "TotalBloodOxygenLowThan98":
                    this.TotalBloodOxygenLowThan98 = Convert.ToSingle(Value);
                    return;
                case "TotalBloodOxygenLowThan90":
                    this.TotalBloodOxygenLowThan90 = Convert.ToSingle(Value);
                    return;
                case "TotalBloodOxygenLowThan85":
                    this.TotalBloodOxygenLowThan85 = Convert.ToSingle(Value);
                    return;
                case "TotalBloodOxygenLowThan80":
                    this.TotalBloodOxygenLowThan80 = Convert.ToSingle(Value);
                    return;
                case "TotalBloodOxygenLowThan60":
                    this.TotalBloodOxygenLowThan60 = Convert.ToSingle(Value);
                    return;
                case "TotalOxygenReduceIndex":
                    this.TotalOxygenReduceIndex = Convert.ToSingle(Value);
                    return;
                case "TotalOxygenReduceMaxDuration":
                    this.TotalOxygenReduceMaxDuration = Convert.ToSingle(Value);
                    return;
                case "BloodOxygenLowThan90DurationPercent":
                    this.BloodOxygenLowThan90DurationPercent = Convert.ToSingle(Value);
                    return;
                case "BloodOxygenLowThan90Duration":
                    this.BloodOxygenLowThan90Duration = Convert.ToSingle(Value);
                    return;
                case "MinHypopneaBloodOxygen":
                    this.MinHypopneaBloodOxygen = Convert.ToSingle(Value);
                    return;
                case "MinApneaBloodOxygen":
                    this.MinApneaBloodOxygen = Convert.ToSingle(Value);
                    return;
                case "MinBloodOxygenTSTBreathEventTyp":
                    this.MinBloodOxygenTSTBreathEventTyp = Convert.ToInt32(Value);
                    break;

                    return;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_BloodOxygenResult());
        }
        #endregion
        #region 公有属性
        /// <summary>
        /// 最低血氧期间发生的呼吸事件类型
        /// </summary>
        public string strMinBloodOxygenTSTBreathEventTyp { private set; get; }
        /// <summary>
        /// 记录的唯一标识
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
        /// 最低血氧期间发生的呼吸事件类型
        /// </summary>
        public int MinBloodOxygenTSTBreathEventTyp
        {
            set
            {
                m_MinBloodOxygenTSTBreathEventTyp = value;
                m_MinBloodOxygenTSTBreathEventTyp_ini = true;
                switch (value)
                {
                    case 1:
                        strMinBloodOxygenTSTBreathEventTyp = "为阻塞性睡眠呼吸暂停";
                        break;
                    case 2:
                        strMinBloodOxygenTSTBreathEventTyp = "为中枢型睡眠呼吸暂停";
                        break;
                    case 3:
                        strMinBloodOxygenTSTBreathEventTyp = "为混合型睡眠呼吸暂停";
                        break;
                    case 4:
                        strMinBloodOxygenTSTBreathEventTyp = "为低通气睡眠事件";
                        break;
                }
            }
            get
            {
                return m_MinBloodOxygenTSTBreathEventTyp;
            }
        }
        /// <summary>
        /// 间接性低氧血症 0-正常 1-轻度 2-中度 3-重度
        /// </summary>
        public int BloodOxygenLowDegreeLevel
        {
            set
            {
                m_BloodOxygenLowDegreeLevel = value;
                m_BloodOxygenLowDegreeLevel_ini = true;
            }
            get
            {
                return m_BloodOxygenLowDegreeLevel;
            }
        }
        /// <summary>
        /// 最低血氧持续时间
        /// </summary>
        public float MinLimitBloodOxygenDuration
        {
            set
            {
                m_MinLimitBloodOxygenDuration = value;
                m_MinLimitBloodOxygenDuration_ini = true;
            }
            get
            {
                return m_MinLimitBloodOxygenDuration;
            }
        }
        /// <summary>
        /// 最低血氧值
        /// </summary>
        public float MinBloodOxygenTIB
        {
            set
            {
                m_MinBloodOxygenTIB = value;
                m_MinBloodOxygenTIB_ini = true;
            }
            get
            {
                return m_MinBloodOxygenTIB;
            }
        }
        public float AveBloodOxygenTST
        {
            set
            {
                m_AveBloodOxygenTST = value;
                m_AveBloodOxygenTST_ini = true;
            }
            get
            {
                return m_AveBloodOxygenTST;
            }
        }
        /// <summary>
        /// 总睡眠期的最低血氧值
        /// </summary>
        public float MinBloodOxygenTST
        {
            set
            {
                m_MinBloodOxygenTST = value;
                m_MinBloodOxygenTST_ini = true;

                //成人版
                if (ModeType == 0)
                {
                    if (value >= 90)
                    {
                        m_BloodOxygenLowDegreeLevel = 0;
                    }
                    else if (value >= 85 && value < 90)
                    {
                        m_BloodOxygenLowDegreeLevel = 1;
                    }
                    else if (value >= 80 && value < 85)
                    {
                        m_BloodOxygenLowDegreeLevel = 2;
                    }
                    else
                    {
                        m_BloodOxygenLowDegreeLevel = 3;
                    }
                }
                //儿童版
                else
                {
                    if (value >= 92)
                    {
                        m_BloodOxygenLowDegreeLevel = 0;
                    }
                    else if (value > 85 && value < 92)
                    {
                        m_BloodOxygenLowDegreeLevel = 1;
                    }
                    else if (value > 75 && value <= 85)
                    {
                        m_BloodOxygenLowDegreeLevel = 2;
                    }
                    else
                    {
                        m_BloodOxygenLowDegreeLevel = 3;
                    }
                }
                
                strBloodOxygenLowDegreeLevel = string.Format("未见{0} 轻度{1} 中度{2} 重度{3}", m_BloodOxygenLowDegreeLevel == 0 ? "■" : "□", m_BloodOxygenLowDegreeLevel == 1 ? "■" : "□", m_BloodOxygenLowDegreeLevel == 2 ? "■" : "□", m_BloodOxygenLowDegreeLevel == 3 ? "■" : "□");
            }
            get
            {
                return m_MinBloodOxygenTST;
            }
        }
        /// <summary>
        /// 总睡眠期的最低血氧值发生时间
        /// </summary>
        public DateTime MinBloodOxygenTSTStartTime
        {
            set
            {
                m_MinBloodOxygenTSTStartTime = value;
                m_MinBloodOxygenTSTStartTime_ini = true;
            }
            get
            {
                return m_MinBloodOxygenTSTStartTime;
            }
        }
        /// <summary>
        /// 总睡眠期的最长氧减数值
        /// </summary>
        public float LongBloodOxygenTST
        {
            set
            {
                m_LongBloodOxygenTST = value;
                m_LongBloodOxygenTST_ini = true;
            }
            get
            {
                return m_LongBloodOxygenTST;
            }
        }
        /// <summary>
        /// 总睡眠期的最大氧减幅度数值
        /// </summary>
        public float MaxBloodOxygenReduceTST { set; get; }
        /// <summary>
        /// 总睡眠期的最长氧减持续时间
        /// </summary>
        public float LongBloodOxygenDuringTime { set; get; }
        /// <summary>
        /// 总睡眠期的最大氧减幅度持续时间
        /// </summary>
        public float MaxBloodOxygenReduceDuringTime { set; get; }
        /// <summary>
        /// 总睡眠期的全部氧减时间(单位是分钟)
        /// </summary>
        public float TotalBloodOxygenTSTTime { set; get; }
        /// <summary>
        /// 全部氧减时间在总睡眠期间的占比(TST)
        /// </summary>
        public float BloodOxygenTotalTimeOfTST { set; get; }
        /// <summary>
        /// 总睡眠期的平均氧减时间
        /// </summary>
        public float AverageBloodOxygenTSTTime
        {
            set
            {
                m_AverageBloodOxygenTSTTime = value;
                m_AverageBloodOxygenTSTTime_ini = true;
            }
            get
            {
                return m_AverageBloodOxygenTSTTime;
            }
        }
        /// <summary>
        /// 总睡眠期的平均氧减幅度
        /// </summary>
        public float AverageBloodOxygenTSTReduceValue
        {
            set; get;
        }
        /// <summary>
        /// 总睡眠期的平均氧减时间(单位：分钟)
        /// </summary>
        public float AverageBloodOxygenTSTTimeMin
        {
            set;get;
        }
        /// <summary>
        /// 总睡眠期的最长氧减发生时间
        /// </summary>
        public DateTime LongBloodOxygenTSTStartTime
        {
            set
            {
                m_LongBloodOxygenTSTStartTime = value;
                m_LongBloodOxygenTSTStartTime_ini = true;
            }
            get
            {
                return m_LongBloodOxygenTSTStartTime;
            }
        }
        /// <summary>
        /// 总睡眠期的最大氧减幅度发生时间
        /// </summary>
        public DateTime MaxBloodOxygenReduceTSTStartTime { set; get; }
        /// <summary>
        ///  最长低通气持续期间的最低血氧值
        /// </summary>
        public float MinHypopneaBloodOxygen
        {
            set
            {
                m_MinHypopneaBloodOxygen = value; m_MinHypopneaBloodOxygen_ini = true;
                strMinHypopneaBloodOxygen = value <= 0 ? "" : string.Format("，伴随最低血氧饱和度为{0} %", value);
            }
            get
            {
                return m_MinHypopneaBloodOxygen;
            }
        }
        /// <summary>
        ///  最长阻塞型呼吸暂停持续期间的最低血氧值
        /// </summary>
        public float MinApneaBloodOxygen
        {
            set
            {
                m_MinApneaBloodOxygen = value; m_MinApneaBloodOxygen_ini = true;
                strMinApneaBloodOxygen = value <= 0 ? "" : string.Format("，伴随最低血氧饱和度为{0} %", value);
            }
            get
            {
                return m_MinBloodOxygenTIB; ;
            }
        }
        /// <summary>
        /// 与呼吸事件相关的最低血氧值
        /// </summary>
        public float MinBreathBloodOxygen
        {
            set
            {
                m_MinBreathBloodOxygen = value;
                if (value == 100)
                    strMinBloodOxygenInRespiratory = "--";
                else
                    strMinBloodOxygenInRespiratory = string.Format("{0} %", value);
                m_MinBreathBloodOxygen_ini = true;
            }
            get
            {
                return m_MinBreathBloodOxygen;
            }
        }
        /// <summary>
        /// 平均血氧（nrem）
        /// </summary>
        public float AveBloodOxygenNREM
        {
            set
            {
                m_AveBloodOxygenNREM = value;
                m_AveBloodOxygenNREM_ini = true;
                if (value <= 0)
                    strAveBloodOxygenNREM = "--";
                else
                    strAveBloodOxygenNREM = string.Format("{0}", value);
            }
            get
            {
                return m_AveBloodOxygenNREM;
            }
        }
        /// <summary>
        /// 平均血氧（rem）
        /// </summary>
        public float AveBloodOxygenREM
        {
            set
            {
                m_AveBloodOxygenREM = value;
                m_AveBloodOxygenREM_ini = true;
                if (value <= 0)
                    strAveBloodOxygenREM = "--";
                else
                    strAveBloodOxygenREM = string.Format("{0}", value);
            }
            get
            {
                return m_AveBloodOxygenREM;
            }
        }
        /// <summary>
        /// 平均血氧（TIB）
        /// </summary>
        public float AveBloodOxygenTIB
        {
            set
            {
                m_AveBloodOxygenTIB = value;
                m_AveBloodOxygenTIB_ini = true;
            }
            get
            {
                return m_AveBloodOxygenTIB;
            }
        }
        /// <summary>
        /// 氧减指数
        /// </summary>
        public float OxygenReduceIndex
        {
            set
            {
                m_OxygenReduceIndex = value;
                m_OxygenReduceIndex_ini = true;
            }
            get
            {
                return m_OxygenReduceIndex;
            }
        }

        /// <summary>
        /// 血氧指数低于90%总时间
        /// </summary>
        public float BloodOxygenLowThan90Duration
        {
            set
            {
                m_BloodOxygenLowThan90Duration = value;
                m_BloodOxygenLowThan90Duration_ini = true;
            }
            get
            {
                return m_BloodOxygenLowThan90Duration;
            }
        }
        /// <summary>
        /// 血氧指数低于90%总时间与睡眠总时间占比
        /// </summary>
        public float BloodOxygenLowThan90DurationPercent
        {
            set
            {
                m_BloodOxygenLowThan90DurationPercent = value;
                m_BloodOxygenLowThan90DurationPercent_ini = true;
            }
            get
            {
                return m_BloodOxygenLowThan90DurationPercent;
            }
        }
        /// <summary>
        /// 氧减次数
        /// </summary>
        public float OxygenReduceCount
        {
            set
            {
                m_OxygenReduceCount = value;
                m_OxygenReduceCount_ini = true;
            }
            get
            {
                return m_OxygenReduceCount;
            }
        }
        public float WakeBloodOxygenLowThan98
        {
            set
            {
                m_WakeBloodOxygenLowThan98 = value;
                m_WakeBloodOxygenLowThan98_ini = true;
            }
            get
            {
                return m_WakeBloodOxygenLowThan98;
            }
        }
        /// <summary>
        /// 清醒期血氧指数低于90%占比
        /// </summary>
        public float WakeBloodOxygenLowThan90
        {
            set
            {
                m_WakeBloodOxygenLowThan90 = value;
                m_WakeBloodOxygenLowThan90_ini = true;
            }
            get
            {
                return m_WakeBloodOxygenLowThan90;
            }
        }
        /// <summary>
        /// 清醒期血氧指数低于85%占比
        /// </summary>
        public float WakeBloodOxygenLowThan85
        {
            set
            {
                m_WakeBloodOxygenLowThan85 = value;
                m_WakeBloodOxygenLowThan85_ini = true;
            }
            get
            {
                return m_WakeBloodOxygenLowThan85;
            }
        }
        /// <summary>
        /// 清醒期血氧指数低于80%占比
        /// </summary>
        public float WakeBloodOxygenLowThan80
        {
            set
            {
                m_WakeBloodOxygenLowThan80 = value;
                m_WakeBloodOxygenLowThan80_ini = true;
            }
            get
            {
                return m_WakeBloodOxygenLowThan80;
            }
        }
        public float WakeBloodOxygenLowThan60
        {
            set
            {
                m_WakeBloodOxygenLowThan60 = value;
                m_WakeBloodOxygenLowThan60_ini = true;
            }
            get
            {
                return m_WakeBloodOxygenLowThan60;
            }
        }
        /// <summary>
        /// 清醒期氧减指数
        /// </summary>
        public float WakeOxygenReduceIndex
        {
            set
            {
                m_WakeOxygenReduceIndex = value;
                m_WakeOxygenReduceIndex_ini = true;
            }
            get
            {
                return m_WakeOxygenReduceIndex;
            }
        }
        /// <summary>
        /// 清醒期氧减最长时间
        /// </summary>
        public float WakeOxygenReduceMaxDuration
        {
            set
            {
                m_WakeOxygenReduceMaxDuration = value;
                m_WakeOxygenReduceMaxDuration_ini = true;
            }
            get
            {
                return m_WakeOxygenReduceMaxDuration;
            }
        }
        public float REMBloodOxygenLowThan98
        {
            set
            {
                m_REMBloodOxygenLowThan98 = value;
                m_REMBloodOxygenLowThan98_ini = true;
            }
            get
            {
                return m_REMBloodOxygenLowThan98;
            }
        }
        /// <summary>
        /// 快速眼动期血氧指数低于90%占比
        /// </summary>
        public float REMBloodOxygenLowThan90
        {
            set
            {
                m_REMBloodOxygenLowThan90 = value;
                m_REMBloodOxygenLowThan90_ini = true;
            }
            get
            {
                return m_REMBloodOxygenLowThan90;
            }
        }
        /// <summary>
        /// 快速眼动期血氧指数低于85%占比
        /// </summary>
        public float REMBloodOxygenLowThan85
        {
            set
            {
                m_REMBloodOxygenLowThan85 = value;
                m_REMBloodOxygenLowThan85_ini = true;
            }
            get
            {
                return m_REMBloodOxygenLowThan85;
            }
        }
        /// <summary>
        /// 快速眼动期血氧指数低于80%占比
        /// </summary>
        public float REMBloodOxygenLowThan80
        {
            set
            {
                m_REMBloodOxygenLowThan80 = value;
                m_REMBloodOxygenLowThan80_ini = true;
            }
            get
            {
                return m_REMBloodOxygenLowThan80;
            }
        }
        public float REMBloodOxygenLowThan60
        {
            set
            {
                m_REMBloodOxygenLowThan60 = value;
                m_REMBloodOxygenLowThan60_ini = true;
            }
            get
            {
                return m_REMBloodOxygenLowThan60;
            }
        }
        /// <summary>
        /// 快速眼动期氧减指数
        /// </summary>
        public float REMOxygenReduceIndex
        {
            set
            {
                m_REMOxygenReduceIndex = value;
                m_REMOxygenReduceIndex_ini = true;
            }
            get
            {
                return m_REMOxygenReduceIndex;
            }
        }
        /// <summary>
        /// 快速眼动期氧减最长时间
        /// </summary>
        public float REMOxygenReduceMaxDuration
        {
            set
            {
                m_REMOxygenReduceMaxDuration = value;
                m_REMOxygenReduceMaxDuration_ini = true;
            }
            get
            {
                return m_REMOxygenReduceMaxDuration;
            }
        }
        public float NREMBloodOxygenLowThan98
        {
            set
            {
                m_NREMBloodOxygenLowThan98 = value;
                m_NREMBloodOxygenLowThan98_ini = true;
            }
            get
            {
                return m_NREMBloodOxygenLowThan98;
            }
        }
        /// <summary>
        /// 非快速眼动期血氧指数低于90%占比
        /// </summary>
        public float NREMBloodOxygenLowThan90
        {
            set
            {
                m_NREMBloodOxygenLowThan90 = value;
                m_NREMBloodOxygenLowThan90_ini = true;
            }
            get
            {
                return m_NREMBloodOxygenLowThan90;
            }
        }
        /// <summary>
        /// 非快速眼动期血氧指数低于85%占比
        /// </summary>
        public float NREMBloodOxygenLowThan85
        {
            set
            {
                m_NREMBloodOxygenLowThan85 = value;
                m_NREMBloodOxygenLowThan85_ini = true;
            }
            get
            {
                return m_NREMBloodOxygenLowThan85;
            }
        }
        /// <summary>
        /// 非快速眼动期血氧指数低于80%占比
        /// </summary>
        public float NREMBloodOxygenLowThan80
        {
            set
            {
                m_NREMBloodOxygenLowThan80 = value;
                m_NREMBloodOxygenLowThan80_ini = true;
            }
            get
            {
                return m_NREMBloodOxygenLowThan80;
            }
        }
        public float NREMBloodOxygenLowThan60
        {
            set
            {
                m_NREMBloodOxygenLowThan60 = value;
                m_NREMBloodOxygenLowThan60_ini = true;
            }
            get
            {
                return m_NREMBloodOxygenLowThan60;
            }
        }
        /// <summary>
        /// 非快速眼动期氧减指数
        /// </summary>
        public float NREMOxygenReduceIndex
        {
            set
            {
                m_NREMOxygenReduceIndex = value;
                m_NREMOxygenReduceIndex_ini = true;
            }
            get
            {
                return m_NREMOxygenReduceIndex;
            }
        }
        /// <summary>
        /// 非快速眼动期氧减最长时间
        /// </summary>
        public float NREMOxygenReduceMaxDuration
        {
            set
            {
                m_NREMOxygenReduceMaxDuration = value;
                m_NREMOxygenReduceMaxDuration_ini = true;
            }
            get
            {
                return m_NREMOxygenReduceMaxDuration;
            }
        }
        public float TotalBloodOxygenLowThan98
        {
            set
            {
                m_TotalBloodOxygenLowThan98 = value;
                m_TotalBloodOxygenLowThan98_ini = true;
            }
            get
            {
                return m_TotalBloodOxygenLowThan98;
            }
        }
        /// <summary>
        /// 总监测期血氧指数低于90%占比
        /// </summary>
        public float TotalBloodOxygenLowThan90
        {
            set
            {
                m_TotalBloodOxygenLowThan90 = value;
                m_TotalBloodOxygenLowThan90_ini = true;
            }
            get
            {
                return m_TotalBloodOxygenLowThan90;
            }
        }
        /// <summary>
        /// 总监测期血氧指数低于85%占比
        /// </summary>
        public float TotalBloodOxygenLowThan85
        {
            set
            {
                m_TotalBloodOxygenLowThan85 = value;
                m_TotalBloodOxygenLowThan85_ini = true;
            }
            get
            {
                return m_TotalBloodOxygenLowThan85;
            }
        }
        /// <summary>
        /// 总监测期血氧指数低于80%占比
        /// </summary>
        public float TotalBloodOxygenLowThan80
        {
            set
            {
                m_TotalBloodOxygenLowThan80 = value;
                m_TotalBloodOxygenLowThan80_ini = true;
            }
            get
            {
                return m_TotalBloodOxygenLowThan80;
            }
        }
        public float TotalBloodOxygenLowThan60
        {
            set
            {
                m_TotalBloodOxygenLowThan60 = value;
                m_TotalBloodOxygenLowThan60_ini = true;
            }
            get
            {
                return m_TotalBloodOxygenLowThan60;
            }
        }
        /// <summary>
        /// 总监测期间氧减指数
        /// </summary>
        public float TotalOxygenReduceIndex
        {
            set
            {
                m_TotalOxygenReduceIndex = value;
                m_TotalOxygenReduceIndex_ini = true;
            }
            get
            {
                return m_TotalOxygenReduceIndex;
            }
        }
        /// <summary>
        /// 总监测期间氧减最长时间
        /// </summary>
        public float TotalOxygenReduceMaxDuration
        {
            set
            {
                m_TotalOxygenReduceMaxDuration = value;
                m_TotalOxygenReduceMaxDuration_ini = true;
            }
            get
            {
                return m_TotalOxygenReduceMaxDuration;
            }
        }
        #endregion

        /// <summary>
        /// 间接性低氧血症描述
        /// </summary>
        public string strBloodOxygenLowDegreeLevel { private set; get; }

        /// <summary>
        /// 低通气事件产生的血氧相关描述
        /// </summary>
        public string strMinHypopneaBloodOxygen { private set; get; }
        /// <summary>
        /// 阻塞型呼吸事件产生的血氧相关描述
        /// </summary>
        public string strMinApneaBloodOxygen { private set; get; }
        /// <summary>
        /// 与呼吸事件相关最低血氧值
        /// </summary>
        public string strMinBloodOxygenInRespiratory { private set; get; }
        /// <summary>
        /// r期间的平均血氧
        /// </summary>
        public string strAveBloodOxygenREM { private set; get; }
        /// <summary>
        /// Nrem期间的平均血氧
        /// </summary>
        public string strAveBloodOxygenNREM { private set; get; }

        /// <summary>
        /// r期间的最低血氧
        /// </summary>
        public string strMinBloodOxygenREM { set; get; }
        /// <summary>
        /// Nrem期间的最低血氧
        /// </summary>
        public string strMinBloodOxygenNREM { set; get; }
        /// <summary>
        /// 清醒期低于70的统计
        /// </summary>
        public float WakeBloodOxygenLowThan70 { set; get; }
        /// <summary>
        /// 非快速眼动睡眠期低于70的统计
        /// </summary>
        public float NREMBloodOxygenLowThan70 { set; get; }
        /// <summary>
        /// 快速眼动睡眠期低于70的统计
        /// </summary>
        public float REMBloodOxygenLowThan70 { set; get; }
        /// <summary>
        /// 低于70的总和统计
        /// </summary>
        public float TotalBloodOxygenLowThan70 { set; get; }
        /// <summary>
        /// 清醒期低于50的统计
        /// </summary>
        public float WakeBloodOxygenLowThan50 { set; get; }
        /// <summary>
        /// 非快速眼动睡眠期低于50的统计
        /// </summary>
        public float NREMBloodOxygenLowThan50 { set; get; }
        /// <summary>
        /// 快速眼动睡眠期低于50的统计
        /// </summary>
        public float REMBloodOxygenLowThan50 { set; get; }
        /// <summary>
        /// 低于50的总和统计
        /// </summary>
        public float TotalBloodOxygenLowThan50 { set; get; }
        /// <summary>
        /// 清醒期低于40的统计
        /// </summary>
        public float WakeBloodOxygenLowThan40 { set; get; }
        /// <summary>
        /// 非快速眼动睡眠期低于40的统计
        /// </summary>
        public float NREMBloodOxygenLowThan40 { set; get; }
        /// <summary>
        /// 快速眼动睡眠期低于40的统计
        /// </summary>
        public float REMBloodOxygenLowThan40 { set; get; }
        /// <summary>
        /// 低于40的总和统计
        /// </summary>
        public float TotalBloodOxygenLowThan40 { set; get; }
        ///// <summary>
        ///// 睡眠期间低于98的统计
        ///// </summary>
        //public float TSTBloodOxygenLowThan98 { set; get; }
        ///// <summary>
        ///// 睡眠期间低于80的统计
        ///// </summary>
        //public float TSTBloodOxygenLowThan80 { set; get; }
        ///// <summary>
        ///// 睡眠期间低于85的统计
        ///// </summary>
        //public float TSTBloodOxygenLowThan85 { set; get; }
        ///// <summary>
        ///// 睡眠期间低于70的统计
        ///// </summary>
        //public float TSTBloodOxygenLowThan70 { set; get; }
        ///// <summary>
        ///// 睡眠期间低于60的统计
        ///// </summary>
        //public float TSTBloodOxygenLowThan60 { set; get; }
        ///// <summary>
        ///// 睡眠期间低于50的统计
        ///// </summary>
        //public float TSTBloodOxygenLowThan50 { set; get; }
        ///// <summary>
        ///// 睡眠期间低于40的统计
        ///// </summary>
        //public float TSTBloodOxygenLowThan40 { set; get; }
        /// <summary>
        /// 最大血氧值(TIB)
        /// </summary>
        public float MaxBloodOxygenTIB { set; get; }
        /// <summary>
        /// 最大血氧值(TST)
        /// </summary>
        public float MaxBloodOxygenTST { set; get; }

        /// <summary>
        /// 平均血氧值(W)
        /// </summary>
        public float AveBloodOxygenWake { set; get; }
        /// <summary>
        /// 最小血氧值(W)
        /// </summary>
        public float MinBloodOxygenWake { set; get; }
        /// <summary>
        /// 低于98的时间总和统计(分钟为单位)
        /// </summary>
        public float TotalBloodOxygenLowThan98TimesMinutes { set; get; }
        /// <summary>
        /// 低于90的时间总和统计(分钟为单位)
        /// </summary>
        public float TotalBloodOxygenLowThan90TimesMinutes { set; get; }
        /// <summary>
        /// 低于85的时间总和统计(分钟为单位)
        /// </summary>
        public float TotalBloodOxygenLowThan85TimesMinutes { set; get; }
        /// <summary>
        /// 低于80的时间总和统计(分钟为单位)
        /// </summary>
        public float TotalBloodOxygenLowThan80TimesMinutes { set; get; }
        /// <summary>
        /// 低于70的时间总和统计(分钟为单位)
        /// </summary>
        public float TotalBloodOxygenLowThan70TimesMinutes { set; get; }
        /// <summary>
        /// 低于60的时间总和统计(分钟为单位)
        /// </summary>
        public float TotalBloodOxygenLowThan60TimesMinutes { set; get; }
        /// <summary>
        /// 低于50的时间总和统计(分钟为单位)
        /// </summary>
        public float TotalBloodOxygenLowThan50TimesMinutes { set; get; }
        /// <summary>
        /// 低于40的时间总和统计(分钟为单位)
        /// </summary>
        public float TotalBloodOxygenLowThan40TimesMinutes { set; get; }
        /// <summary>
        /// 低于98的时间总和统计(秒为单位)
        /// </summary>
        public float TotalBloodOxygenLowThan98Times { set; get; }
        /// <summary>
        /// 低于90的时间总和统计(秒为单位)
        /// </summary>
        public float TotalBloodOxygenLowThan90Times { set; get; }
        /// <summary>
        /// 低于85的时间总和统计(秒为单位)
        /// </summary>
        public float TotalBloodOxygenLowThan85Times { set; get; }
        /// <summary>
        /// 低于80的时间总和统计(秒为单位)
        /// </summary>
        public float TotalBloodOxygenLowThan80Times { set; get; }
        /// <summary>
        /// 低于70的时间总和统计(秒为单位)
        /// </summary>
        public float TotalBloodOxygenLowThan70Times { set; get; }
        /// <summary>
        /// 低于60的时间总和统计(秒为单位)
        /// </summary>
        public float TotalBloodOxygenLowThan60Times { set; get; }
        /// <summary>
        /// 低于50的时间总和统计(秒为单位)
        /// </summary>
        public float TotalBloodOxygenLowThan50Times { set; get; }
        /// <summary>
        /// 低于40的时间总和统计(秒为单位)
        /// </summary>
        public float TotalBloodOxygenLowThan40Times { set; get; }
        /// <summary>
        /// 最长呼吸暂停持续期间的最低血氧值
        /// </summary>
        public float MinAllApneaBloodOxygen { set; get; }
        /// <summary>
        /// 血氧伪迹次数
        /// </summary>
        public float Spo2ArtifactCount { set; get; }
        /// <summary>
        /// 血氧伪迹总时间
        /// </summary>
        public float Spo2ArtifactTotalTime { set; get; }
        /// <summary>
        /// 血氧伪迹总时间在睡眠期间的占比
        /// </summary>
        public float Spo2ArtifactTotalTimeOfTST { set; get; }
        /// <summary>
        /// 氧饱和度下降≥3%指数 
        /// </summary>
        public float Spo2Reduce3Index { set; get; }
        /// <summary>
        /// 氧饱和度下降≥4%指数 
        /// </summary>
        public float Spo2Reduce4Index { set; get; }
        /// <summary>
        /// 平均血氧延迟时间(单位:分)
        /// </summary>
        public float AverageSpo2DelayTime { set; get; }
        /// <summary>
        /// 血氧基线
        /// </summary>
        public float Spo2BaseLine { set; get; }
        /// <summary>
        /// 模式
        /// </summary>
        public int ModeType { set; get; }
    }
}
