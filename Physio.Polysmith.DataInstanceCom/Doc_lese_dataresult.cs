using pSystem.Interface.Util;
using System;

namespace Physio.Polysmith.DataInstanceCom
{
    public class Doc_lese_dataresult : ITable
    {
        #region 私有成员
        private int m_ID = 0;
        private bool m_ID_ini = false;
        private string m_SleepResult = "";
        private bool m_SleepResult_ini = false;
        private string m_BloodOxygenResult = "";
        private bool m_BloodOxygenResult_ini = false;
        private string m_BodyMovementResult = "";
        private bool m_BodyMovementResult_ini = false;
        private string m_BodyStateResult = "";
        private bool m_BodyStateResult_ini = false;
        private string m_BreathEventResult = "";
        private bool m_BreathEventResult_ini = false;
        private string m_HeartRateResult = "";
        private bool m_HeartRateResult_ini = false;
        private string m_SnoreResult = "";
        private bool m_SnoreResult_ini = false;
        private string m_GUID = "";
        private bool m_GUID_ini = false;
        private string m_Reserve1 = "";
        private bool m_Reserve1_ini = false;
        private string m_Reserve2 = "";
        private bool m_Reserve2_ini = false;
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
        public string SleepResult
        {
            set
            {
                m_SleepResult = value;
                m_SleepResult_ini = true;
            }
            get
            {
                return m_SleepResult;
            }
        }
        public string BloodOxygenResult
        {
            set
            {
                m_BloodOxygenResult = value;
                m_BloodOxygenResult_ini = true;
            }
            get
            {
                return m_BloodOxygenResult;
            }
        }
        public string BodyMovementResult
        {
            set
            {
                m_BodyMovementResult = value;
                m_BodyMovementResult_ini = true;
            }
            get
            {
                return m_BodyMovementResult;
            }
        }
        public string BodyStateResult
        {
            set
            {
                m_BodyStateResult = value;
                m_BodyStateResult_ini = true;
            }
            get
            {
                return m_BodyStateResult;
            }
        }
        public string BreathEventResult
        {
            set
            {
                m_BreathEventResult = value;
                m_BreathEventResult_ini = true;
            }
            get
            {
                return m_BreathEventResult;
            }
        }
        public string HeartRateResult
        {
            set
            {
                m_HeartRateResult = value;
                m_HeartRateResult_ini = true;
            }
            get
            {
                return m_HeartRateResult;
            }
        }
        public string SnoreResult
        {
            set
            {
                m_SnoreResult = value;
                m_SnoreResult_ini = true;
            }
            get
            {
                return m_SnoreResult;
            }
        }
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
            if (this.m_SleepResult_ini)
            {
                def = string.Format("{0}{2}{1}", def, "SleepResult", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_SleepResult, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_BloodOxygenResult_ini)
            {
                def = string.Format("{0}{2}{1}", def, "BloodOxygenResult", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_BloodOxygenResult, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_BodyMovementResult_ini)
            {
                def = string.Format("{0}{2}{1}", def, "BodyMovementResult", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_BodyMovementResult, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_BodyStateResult_ini)
            {
                def = string.Format("{0}{2}{1}", def, "BodyStateResult", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_BodyStateResult, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_BreathEventResult_ini)
            {
                def = string.Format("{0}{2}{1}", def, "BreathEventResult", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_BreathEventResult, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_HeartRateResult_ini)
            {
                def = string.Format("{0}{2}{1}", def, "HeartRateResult", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_HeartRateResult, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_SnoreResult_ini)
            {
                def = string.Format("{0}{2}{1}", def, "SnoreResult", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_SnoreResult, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_GUID_ini)
            {
                def = string.Format("{0}{2}{1}", def, "GUID", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_GUID, Mulitcase ? "," : "");
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
            if (m_SleepResult_ini)
            {
                returnstr = string.Format("{0}SleepResult='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_SleepResult);
                Mulitcase = true;
            }
            if (m_BloodOxygenResult_ini)
            {
                returnstr = string.Format("{0}BloodOxygenResult='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_BloodOxygenResult);
                Mulitcase = true;
            }
            if (m_BodyMovementResult_ini)
            {
                returnstr = string.Format("{0}BodyMovementResult='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_BodyMovementResult);
                Mulitcase = true;
            }
            if (m_BodyStateResult_ini)
            {
                returnstr = string.Format("{0}BodyStateResult='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_BodyStateResult);
                Mulitcase = true;
            }
            if (m_BreathEventResult_ini)
            {
                returnstr = string.Format("{0}BreathEventResult='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_BreathEventResult);
                Mulitcase = true;
            }
            if (m_HeartRateResult_ini)
            {
                returnstr = string.Format("{0}HeartRateResult='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_HeartRateResult);
                Mulitcase = true;
            }
            if (m_SnoreResult_ini)
            {
                returnstr = string.Format("{0}SnoreResult='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_SnoreResult);
                Mulitcase = true;
            }
            if (m_GUID_ini)
            {
                returnstr = string.Format("{0}GUID='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_GUID);
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
            if (m_SleepResult_ini)
            {
                def = string.Format("{0}{2}SleepResult='{1}'", def, m_SleepResult, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_BloodOxygenResult_ini)
            {
                def = string.Format("{0}{2}BloodOxygenResult='{1}'", def, m_BloodOxygenResult, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_BodyMovementResult_ini)
            {
                def = string.Format("{0}{2}BodyMovementResult='{1}'", def, m_BodyMovementResult, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_BodyStateResult_ini)
            {
                def = string.Format("{0}{2}BodyStateResult='{1}'", def, m_BodyStateResult, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_BreathEventResult_ini)
            {
                def = string.Format("{0}{2}BreathEventResult='{1}'", def, m_BreathEventResult, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_HeartRateResult_ini)
            {
                def = string.Format("{0}{2}HeartRateResult='{1}'", def, m_HeartRateResult, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_SnoreResult_ini)
            {
                def = string.Format("{0}{2}SnoreResult='{1}'", def, m_SnoreResult, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_GUID_ini)
            {
                def = string.Format("{0}{2}GUID='{1}'", def, m_GUID, Mulitcase ? "," : "");
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
            if (!Mulitcase)
            {
                return ("");
            }
            return (def);
        }
        public string GetThisTableName()
        {
            return ("lese_dataresult");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "ID":
                    ID = Convert.ToInt32(Value);
                    break;
                case "SleepResult":
                    SleepResult = Value.ToString(); ;
                    break;
                case "BloodOxygenResult":
                    BloodOxygenResult = Value.ToString(); ;
                    break;
                case "BodyMovementResult":
                    BodyMovementResult = Value.ToString(); ;
                    break;
                case "BodyStateResult":
                    BodyStateResult = Value.ToString(); ;
                    break;
                case "BreathEventResult":
                    BreathEventResult = Value.ToString(); ;
                    break;
                case "HeartRateResult":
                    HeartRateResult = Value.ToString(); ;
                    break;
                case "SnoreResult":
                    SnoreResult = Value.ToString(); ;
                    break;
                case "GUID":
                    GUID = Value.ToString(); ;
                    break;
                case "Reserve1":
                    Reserve1 = Value.ToString(); ;
                    break;
                case "Reserve2":
                    Reserve2 = Value.ToString(); ;
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_lese_dataresult());
        }
        #endregion
    }
}
