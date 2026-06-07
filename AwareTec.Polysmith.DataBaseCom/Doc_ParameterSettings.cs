using pSystem.Interface.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.DataBaseCom
{
    public class Doc_ParameterSettings : ITable
    {
        #region 私有成员
        private int m_ID = 0;
        private bool m_ID_ini = false;
        private int m_UserID = 0;
        private bool m_UserID_ini = false;
        private float m_Spo2BufferTime = 0;
        private bool m_Spo2BufferTime_ini = false;
        private float m_Spo2LastTime = 0;
        private bool m_Spo2LastTime_ini = false;
        private int m_Spo2Reduce = 0;
        private bool m_Spo2Reduce_ini = false;
        private int m_BreathAnalysisChannel = 0;
        private bool m_BreathAnalysisChannel_ini = false;
        private string m_EventThresholdValueByPress = "";
        private bool m_EventThresholdValueByPress_ini = false;
        private string m_EventThresholdValueByFlow = "";
        private bool m_EventThresholdValueByFlow_ini = false;
        private string m_EventThresholdValueByFlowPress = "";
        private bool m_EventThresholdValueByFlowPress_ini = false;
        private float m_MinBreathLastTime = 0;
        private bool m_MinBreathLastTime_ini = false;
        private float m_MaxBreathLastTime = 0;
        private bool m_MaxBreathLastTime_ini = false;
        private float m_MinLMThresholdValue = 0;
        private bool m_MinLMThresholdValue_ini = false;
        private float m_MaxLMThresholdValue = 0;
        private bool m_MaxLMThresholdValue_ini = false;
        private float m_MinPLMsThresholdValue = 0;
        private bool m_MinPLMsThresholdValue_ini = false;
        private float m_MaxPLMsThresholdValue = 0;
        private bool m_MaxPLMsThresholdValue_ini = false;
        private int m_SleepStageModeChoose = 0;
        private bool m_SleepStageModeChoose_ini = false;
        private int m_SpindlesAlphaWavesAnalysisChannel = 0;
        private bool m_SpindlesAlphaWavesAnalysisChannel_ini = false;
        private float m_MTLastTime = 0;
        private bool m_MTLastTime_ini = false;
        private int m_ArousalAnalysisChannel = 0;
        private bool m_ArousalAnalysisChannel_ini = false;
        private string m_Reserve1 = "";
        private bool m_Reserve1_ini = false;
        private string m_Reserve2 = "";
        private bool m_Reserve2_ini = false;
        private string m_Reserve3 = "";
        private bool m_Reserve3_ini = false;
        private string m_Reserve4 = "";
        private bool m_Reserve4_ini = false;
        private string m_Reserve5 = "";
        private bool m_Reserve5_ini = false;
        private string m_Reserve6 = "";
        private bool m_Reserve6_ini = false;
        private int m_ModeType = 0;
        private bool m_ModeType_ini = false;
        #endregion
        #region 公有成员
        public int ID
        {
            set
            {
                m_ID = value;
                m_ID_ini = true;
            }
            get
            {
                return m_ID;
            }
        }
        public int UserID
        {
            set
            {
                m_UserID = value;
                m_UserID_ini = true;
            }
            get
            {
                return m_UserID;
            }
        }
        public float Spo2BufferTime
        {
            set
            {
                m_Spo2BufferTime = value;
                m_Spo2BufferTime_ini = true;
            }
            get
            {
                return m_Spo2BufferTime;
            }
        }
        public float Spo2LastTime
        {
            set
            {
                m_Spo2LastTime = value;
                m_Spo2LastTime_ini = true;
            }
            get
            {
                return m_Spo2LastTime;
            }
        }
        public int Spo2Reduce
        {
            set
            {
                m_Spo2Reduce = value;
                m_Spo2Reduce_ini = true;
            }
            get
            {
                return m_Spo2Reduce;
            }
        }
        public int BreathAnalysisChannel
        {
            set
            {
                m_BreathAnalysisChannel = value;
                m_BreathAnalysisChannel_ini = true;
            }
            get
            {
                return m_BreathAnalysisChannel;
            }
        }
        public string EventThresholdValueByPress
        {
            set
            {
                m_EventThresholdValueByPress = value;
                m_EventThresholdValueByPress_ini = true;
            }
            get
            {
                return m_EventThresholdValueByPress;
            }
        }
        public string EventThresholdValueByFlow
        {
            set
            {
                m_EventThresholdValueByFlow = value;
                m_EventThresholdValueByFlow_ini = true;
            }
            get
            {
                return m_EventThresholdValueByFlow;
            }
        }
        public string EventThresholdValueByFlowPress
        {
            set
            {
                m_EventThresholdValueByFlowPress = value;
                m_EventThresholdValueByFlowPress_ini = true;
            }
            get
            {
                return m_EventThresholdValueByFlowPress;
            }
        }
        public float MinBreathLastTime
        {
            set
            {
                m_MinBreathLastTime = value;
                m_MinBreathLastTime_ini = true;
            }
            get
            {
                return m_MinBreathLastTime;
            }
        }
        public float MaxBreathLastTime
        {
            set
            {
                m_MaxBreathLastTime = value;
                m_MaxBreathLastTime_ini = true;
            }
            get
            {
                return m_MaxBreathLastTime;
            }
        }
        public float MinLMThresholdValue
        {
            set
            {
                m_MinLMThresholdValue = value;
                m_MinLMThresholdValue_ini = true;
            }
            get
            {
                return m_MinLMThresholdValue;
            }
        }
        public float MaxLMThresholdValue
        {
            set
            {
                m_MaxLMThresholdValue = value;
                m_MaxLMThresholdValue_ini = true;
            }
            get
            {
                return m_MaxLMThresholdValue;
            }
        }
        public float MinPLMsThresholdValue
        {
            set
            {
                m_MinPLMsThresholdValue = value;
                m_MinPLMsThresholdValue_ini = true;
            }
            get
            {
                return m_MinPLMsThresholdValue;
            }
        }
        public float MaxPLMsThresholdValue
        {
            set
            {
                m_MaxPLMsThresholdValue = value;
                m_MaxPLMsThresholdValue_ini = true;
            }
            get
            {
                return m_MaxPLMsThresholdValue;
            }
        }
        public int SleepStageModeChoose
        {
            set
            {
                m_SleepStageModeChoose = value;
                m_SleepStageModeChoose_ini = true;
            }
            get
            {
                return m_SleepStageModeChoose;
            }
        }
        public int SpindlesAlphaWavesAnalysisChannel
        {
            set
            {
                m_SpindlesAlphaWavesAnalysisChannel = value;
                m_SpindlesAlphaWavesAnalysisChannel_ini = true;
            }
            get
            {
                return m_SpindlesAlphaWavesAnalysisChannel;
            }
        }
        public float MTLastTime
        {
            set
            {
                m_MTLastTime = value;
                m_MTLastTime_ini = true;
            }
            get
            {
                return m_MTLastTime;
            }
        }
        public int ArousalAnalysisChannel
        {
            set
            {
                m_ArousalAnalysisChannel = value;
                m_ArousalAnalysisChannel_ini = true;
            }
            get
            {
                return m_ArousalAnalysisChannel;
            }
        }
        public string Reserve1
        {
            set
            {
                m_Reserve1 = value;
                m_Reserve1_ini = true;
            }
            get
            {
                return m_Reserve1;
            }
        }
        public string Reserve2
        {
            set
            {
                m_Reserve2 = value;
                m_Reserve2_ini = true;
            }
            get
            {
                return m_Reserve2;
            }
        }
        public string Reserve3
        {
            set
            {
                m_Reserve3 = value;
                m_Reserve3_ini = true;
            }
            get
            {
                return m_Reserve3;
            }
        }
        public string Reserve4
        {
            set
            {
                m_Reserve4 = value;
                m_Reserve4_ini = true;
            }
            get
            {
                return m_Reserve4;
            }
        }
        public string Reserve5
        {
            set
            {
                m_Reserve5 = value;
                m_Reserve5_ini = true;
            }
            get
            {
                return m_Reserve5;
            }
        }
        public string Reserve6
        {
            set
            {
                m_Reserve6 = value;
                m_Reserve6_ini = true;
            }
            get
            {
                return m_Reserve6;
            }
        }
        public int ModeType
        {
            set
            {
                m_ModeType = value;
                m_ModeType_ini = true;
            }
            get
            {
                return m_ModeType;
            }
        }
        #endregion
        #region 继承成员
        public string GetInsertString()
        {
            string def = " (";
            string val = " VALUES(";
            bool Mulitcase = false;
            if (this.m_ID_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ID", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ID, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_UserID_ini)
            {
                def = string.Format("{0}{2}{1}", def, "UserID", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_UserID, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Spo2BufferTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Spo2BufferTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Spo2BufferTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Spo2LastTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Spo2LastTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Spo2LastTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Spo2Reduce_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Spo2Reduce", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Spo2Reduce, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_BreathAnalysisChannel_ini)
            {
                def = string.Format("{0}{2}{1}", def, "BreathAnalysisChannel", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_BreathAnalysisChannel, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_EventThresholdValueByPress_ini)
            {
                def = string.Format("{0}{2}{1}", def, "EventThresholdValueByPress", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_EventThresholdValueByPress, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_EventThresholdValueByFlow_ini)
            {
                def = string.Format("{0}{2}{1}", def, "EventThresholdValueByFlow", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_EventThresholdValueByFlow, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_EventThresholdValueByFlowPress_ini)
            {
                def = string.Format("{0}{2}{1}", def, "EventThresholdValueByFlowPress", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_EventThresholdValueByFlowPress, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MinBreathLastTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MinBreathLastTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_MinBreathLastTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MaxBreathLastTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MaxBreathLastTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_MaxBreathLastTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MinLMThresholdValue_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MinLMThresholdValue", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_MinLMThresholdValue, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MaxLMThresholdValue_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MaxLMThresholdValue", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_MaxLMThresholdValue, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MinPLMsThresholdValue_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MinPLMsThresholdValue", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_MinPLMsThresholdValue, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MaxPLMsThresholdValue_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MaxPLMsThresholdValue", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_MaxPLMsThresholdValue, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_SleepStageModeChoose_ini)
            {
                def = string.Format("{0}{2}{1}", def, "SleepStageModeChoose", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_SleepStageModeChoose, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_SpindlesAlphaWavesAnalysisChannel_ini)
            {
                def = string.Format("{0}{2}{1}", def, "SpindlesAlphaWavesAnalysisChannel", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_SpindlesAlphaWavesAnalysisChannel, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MTLastTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MTLastTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_MTLastTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ArousalAnalysisChannel_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ArousalAnalysisChannel", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ArousalAnalysisChannel, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Reserve1_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Reserve1", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Reserve1, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Reserve2_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Reserve2", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Reserve2, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Reserve3_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Reserve3", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Reserve3, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Reserve4_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Reserve4", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Reserve4, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Reserve5_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Reserve5", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Reserve5, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Reserve6_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Reserve6", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Reserve6, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ModeType_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ModeType", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ModeType, Mulitcase ? "," : "");
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
            if (m_ID_ini)
            {
                returnstr = string.Format("{0}ID={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ID);
                Mulitcase = true;
            }
            if (m_UserID_ini)
            {
                returnstr = string.Format("{0}UserID={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_UserID);
                Mulitcase = true;
            }
            if (m_Spo2BufferTime_ini)
            {
                returnstr = string.Format("{0}Spo2BufferTime={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Spo2BufferTime);
                Mulitcase = true;
            }
            if (m_Spo2LastTime_ini)
            {
                returnstr = string.Format("{0}Spo2LastTime={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Spo2LastTime);
                Mulitcase = true;
            }
            if (m_Spo2Reduce_ini)
            {
                returnstr = string.Format("{0}Spo2Reduce={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Spo2Reduce);
                Mulitcase = true;
            }
            if (m_BreathAnalysisChannel_ini)
            {
                returnstr = string.Format("{0}BreathAnalysisChannel={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_BreathAnalysisChannel);
                Mulitcase = true;
            }
            if (m_EventThresholdValueByPress_ini)
            {
                returnstr = string.Format("{0}EventThresholdValueByPress='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_EventThresholdValueByPress);
                Mulitcase = true;
            }
            if (m_EventThresholdValueByFlow_ini)
            {
                returnstr = string.Format("{0}EventThresholdValueByFlow='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_EventThresholdValueByFlow);
                Mulitcase = true;
            }
            if (m_EventThresholdValueByFlowPress_ini)
            {
                returnstr = string.Format("{0}EventThresholdValueByFlowPress='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_EventThresholdValueByFlowPress);
                Mulitcase = true;
            }
            if (m_MinBreathLastTime_ini)
            {
                returnstr = string.Format("{0}MinBreathLastTime={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MinBreathLastTime);
                Mulitcase = true;
            }
            if (m_MaxBreathLastTime_ini)
            {
                returnstr = string.Format("{0}MaxBreathLastTime={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MaxBreathLastTime);
                Mulitcase = true;
            }
            if (m_MinLMThresholdValue_ini)
            {
                returnstr = string.Format("{0}MinLMThresholdValue={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MinLMThresholdValue);
                Mulitcase = true;
            }
            if (m_MaxLMThresholdValue_ini)
            {
                returnstr = string.Format("{0}MaxLMThresholdValue={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MaxLMThresholdValue);
                Mulitcase = true;
            }
            if (m_MinPLMsThresholdValue_ini)
            {
                returnstr = string.Format("{0}MinPLMsThresholdValue={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MinPLMsThresholdValue);
                Mulitcase = true;
            }
            if (m_MaxPLMsThresholdValue_ini)
            {
                returnstr = string.Format("{0}MaxPLMsThresholdValue={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MaxPLMsThresholdValue);
                Mulitcase = true;
            }
            if (m_SleepStageModeChoose_ini)
            {
                returnstr = string.Format("{0}SleepStageModeChoose={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_SleepStageModeChoose);
                Mulitcase = true;
            }
            if (m_SpindlesAlphaWavesAnalysisChannel_ini)
            {
                returnstr = string.Format("{0}SpindlesAlphaWavesAnalysisChannel={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_SpindlesAlphaWavesAnalysisChannel);
                Mulitcase = true;
            }
            if (m_MTLastTime_ini)
            {
                returnstr = string.Format("{0}MTLastTime={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MTLastTime);
                Mulitcase = true;
            }
            if (m_ArousalAnalysisChannel_ini)
            {
                returnstr = string.Format("{0}ArousalAnalysisChannel={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ArousalAnalysisChannel);
                Mulitcase = true;
            }
            if (m_Reserve1_ini)
            {
                returnstr = string.Format("{0}Reserve1='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Reserve1);
                Mulitcase = true;
            }
            if (m_Reserve2_ini)
            {
                returnstr = string.Format("{0}Reserve2='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Reserve2);
                Mulitcase = true;
            }
            if (m_Reserve3_ini)
            {
                returnstr = string.Format("{0}Reserve3='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Reserve3);
                Mulitcase = true;
            }
            if (m_Reserve4_ini)
            {
                returnstr = string.Format("{0}Reserve4='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Reserve4);
                Mulitcase = true;
            }
            if (m_Reserve5_ini)
            {
                returnstr = string.Format("{0}Reserve5='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Reserve5);
                Mulitcase = true;
            }
            if (m_Reserve6_ini)
            {
                returnstr = string.Format("{0}Reserve6='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Reserve6);
                Mulitcase = true;
            }
            if (m_ModeType_ini)
            {
                returnstr = string.Format("{0}ModeType={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ModeType);
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
            if (m_ID_ini)
            {
                def = string.Format("{0}{2}ID={1}", def, m_ID, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_UserID_ini)
            {
                def = string.Format("{0}{2}UserID={1}", def, m_UserID, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Spo2BufferTime_ini)
            {
                def = string.Format("{0}{2}Spo2BufferTime={1}", def, m_Spo2BufferTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Spo2LastTime_ini)
            {
                def = string.Format("{0}{2}Spo2LastTime={1}", def, m_Spo2LastTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Spo2Reduce_ini)
            {
                def = string.Format("{0}{2}Spo2Reduce={1}", def, m_Spo2Reduce, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_BreathAnalysisChannel_ini)
            {
                def = string.Format("{0}{2}BreathAnalysisChannel={1}", def, m_BreathAnalysisChannel, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_EventThresholdValueByPress_ini)
            {
                def = string.Format("{0}{2}EventThresholdValueByPress='{1}'", def, m_EventThresholdValueByPress, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_EventThresholdValueByFlow_ini)
            {
                def = string.Format("{0}{2}EventThresholdValueByFlow='{1}'", def, m_EventThresholdValueByFlow, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_EventThresholdValueByFlowPress_ini)
            {
                def = string.Format("{0}{2}EventThresholdValueByFlowPress='{1}'", def, m_EventThresholdValueByFlowPress, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MinBreathLastTime_ini)
            {
                def = string.Format("{0}{2}MinBreathLastTime={1}", def, m_MinBreathLastTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MaxBreathLastTime_ini)
            {
                def = string.Format("{0}{2}MaxBreathLastTime={1}", def, m_MaxBreathLastTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MinLMThresholdValue_ini)
            {
                def = string.Format("{0}{2}MinLMThresholdValue={1}", def, m_MinLMThresholdValue, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MaxLMThresholdValue_ini)
            {
                def = string.Format("{0}{2}MaxLMThresholdValue={1}", def, m_MaxLMThresholdValue, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MinPLMsThresholdValue_ini)
            {
                def = string.Format("{0}{2}MinPLMsThresholdValue={1}", def, m_MinPLMsThresholdValue, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MaxPLMsThresholdValue_ini)
            {
                def = string.Format("{0}{2}MaxPLMsThresholdValue={1}", def, m_MaxPLMsThresholdValue, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_SleepStageModeChoose_ini)
            {
                def = string.Format("{0}{2}SleepStageModeChoose={1}", def, m_SleepStageModeChoose, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_SpindlesAlphaWavesAnalysisChannel_ini)
            {
                def = string.Format("{0}{2}SpindlesAlphaWavesAnalysisChannel={1}", def, m_SpindlesAlphaWavesAnalysisChannel, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MTLastTime_ini)
            {
                def = string.Format("{0}{2}MTLastTime={1}", def, m_MTLastTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ArousalAnalysisChannel_ini)
            {
                def = string.Format("{0}{2}ArousalAnalysisChannel={1}", def, m_ArousalAnalysisChannel, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Reserve1_ini)
            {
                def = string.Format("{0}{2}Reserve1='{1}'", def, m_Reserve1, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Reserve2_ini)
            {
                def = string.Format("{0}{2}Reserve2='{1}'", def, m_Reserve2, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Reserve3_ini)
            {
                def = string.Format("{0}{2}Reserve3='{1}'", def, m_Reserve3, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Reserve4_ini)
            {
                def = string.Format("{0}{2}Reserve4='{1}'", def, m_Reserve4, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Reserve5_ini)
            {
                def = string.Format("{0}{2}Reserve5='{1}'", def, m_Reserve5, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Reserve6_ini)
            {
                def = string.Format("{0}{2}Reserve6='{1}'", def, m_Reserve6, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ModeType_ini)
            {
                def = string.Format("{0}{2}ModeType={1}", def, m_ModeType, Mulitcase ? "," : "");
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
            return ("SystemSetting");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "ID":
                    ID = Convert.ToInt32(Value);
                    break;
                case "UserID":
                    UserID = Convert.ToInt32(Value);
                    break;
                case "Spo2BufferTime":
                    Spo2BufferTime = Convert.ToSingle(Value);
                    break;
                case "Spo2LastTime":
                    Spo2LastTime = Convert.ToSingle(Value);
                    break;
                case "Spo2Reduce":
                    Spo2Reduce = Convert.ToInt32(Value);
                    break;
                case "BreathAnalysisChannel":
                    BreathAnalysisChannel = Convert.ToInt32(Value);
                    break;
                case "EventThresholdValueByPress":
                    EventThresholdValueByPress = Value.ToString(); ;
                    break;
                case "EventThresholdValueByFlow":
                    EventThresholdValueByFlow = Value.ToString(); ;
                    break;
                case "EventThresholdValueByFlowPress":
                    EventThresholdValueByFlowPress = Value.ToString(); ;
                    break;
                case "MinBreathLastTime":
                    MinBreathLastTime = Convert.ToSingle(Value);
                    break;
                case "MaxBreathLastTime":
                    MaxBreathLastTime = Convert.ToSingle(Value);
                    break;
                case "MinLMThresholdValue":
                    MinLMThresholdValue = Convert.ToSingle(Value);
                    break;
                case "MaxLMThresholdValue":
                    MaxLMThresholdValue = Convert.ToSingle(Value);
                    break;
                case "MinPLMsThresholdValue":
                    MinPLMsThresholdValue = Convert.ToSingle(Value);
                    break;
                case "MaxPLMsThresholdValue":
                    MaxPLMsThresholdValue = Convert.ToSingle(Value);
                    break;
                case "SleepStageModeChoose":
                    SleepStageModeChoose = Convert.ToInt32(Value);
                    break;
                case "SpindlesAlphaWavesAnalysisChannel":
                    SpindlesAlphaWavesAnalysisChannel = Convert.ToInt32(Value);
                    break;
                case "MTLastTime":
                    MTLastTime = Convert.ToSingle(Value);
                    break;
                case "ArousalAnalysisChannel":
                    ArousalAnalysisChannel = Convert.ToInt32(Value);
                    break;
                case "Reserve1":
                    Reserve1 = Value.ToString(); ;
                    break;
                case "Reserve2":
                    Reserve2 = Value.ToString(); ;
                    break;
                case "Reserve3":
                    Reserve3 = Value.ToString(); ;
                    break;
                case "Reserve4":
                    Reserve4 = Value.ToString(); ;
                    break;
                case "Reserve5":
                    Reserve5 = Value.ToString(); ;
                    break;
                case "Reserve6":
                    Reserve6 = Value.ToString(); ;
                    break;
                case "ModeType":
                    ModeType = Convert.ToInt32(Value);
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_ParameterSettings());
        }
        #endregion
    }
}
