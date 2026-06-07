using pSystem.Interface.Util;
using System;

namespace AwareTec.Polysmith.DataBaseCom
{
    public class Doc_Epochs : ITable
    {
        #region 私有成员
        private int m_ID = 0;
        private bool m_ID_ini = false;
        private int m_EpochIndex = 0;
        private bool m_EpochIndex_ini = false;
        private int m_Stage = 0;
        private bool m_Stage_ini = false;
        private int m_Pos = 0;
        private bool m_Pos_ini = false;
        private string m_SpO2 = "";
        private bool m_SpO2_ini = false;
        private string m_HeartRate = "";
        private bool m_HeartRate_ini = false;
        private float m_CA = 0;
        private bool m_CA_ini = false;
        private float m_OA = 0;
        private bool m_OA_ini = false;
        private float m_MA = 0;
        private bool m_MA_ini = false;
        private float m_Hypopnea = 0;
        private bool m_Hypopnea_ini = false;
        private float m_MT = 0;
        private bool m_MT_ini = false;
        private float m_PLMs = 0;
        private bool m_PLMs_ini = false;
        private float m_PLM = 0;
        private bool m_PLM_ini = false;
        private float m_MicArousal = 0;
        private bool m_MicArousal_ini = false;
        private string m_GUID = "";
        private bool m_GUID_ini = false;
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
        public int EpochIndex
        {
            set
            {
                m_EpochIndex = value;
                m_EpochIndex_ini = true;
            }
            get
            {
                return m_EpochIndex;
            }
        }
        public int Stage
        {
            set
            {
                m_Stage = value;
                m_Stage_ini = true;
            }
            get
            {
                return m_Stage;
            }
        }
        public int Pos
        {
            set
            {
                m_Pos = value;
                m_Pos_ini = true;
            }
            get
            {
                return m_Pos;
            }
        }
        public string SpO2
        {
            set
            {
                m_SpO2 = value;
                m_SpO2_ini = true;
            }
            get
            {
                return m_SpO2;
            }
        }
        public string HeartRate
        {
            set
            {
                m_HeartRate = value;
                m_HeartRate_ini = true;
            }
            get
            {
                return m_HeartRate;
            }
        }
        public float CA
        {
            set
            {
                m_CA = value;
                m_CA_ini = true;
            }
            get
            {
                return m_CA;
            }
        }
        public float OA
        {
            set
            {
                m_OA = value;
                m_OA_ini = true;
            }
            get
            {
                return m_OA;
            }
        }
        public float MA
        {
            set
            {
                m_MA = value;
                m_MA_ini = true;
            }
            get
            {
                return m_MA;
            }
        }
        public float Hypopnea
        {
            set
            {
                m_Hypopnea = value;
                m_Hypopnea_ini = true;
            }
            get
            {
                return m_Hypopnea;
            }
        }
        public float MicArousal
        {
            set
            {
                m_MicArousal = value;
                m_MicArousal_ini = true;
            }
            get
            {
                return m_MicArousal;
            }
        }
        public float PLMs
        {
            set
            {
                m_PLMs = value;
                m_PLMs_ini = true;
            }
            get
            {
                return m_PLMs;
            }
        }
        public float PLM
        {
            set
            {
                m_PLM = value;
                m_PLM_ini = true;
            }
            get
            {
                return m_PLM;
            }
        }
        public float MT
        {
            set
            {
                m_MT = value;
                m_MT_ini = true;
            }
            get
            {
                return m_MT;
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
            if (this.m_EpochIndex_ini)
            {
                def = string.Format("{0}{2}{1}", def, "EpochIndex", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_EpochIndex, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Stage_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Stage", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Stage, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Pos_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Pos", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Pos, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_SpO2_ini)
            {
                def = string.Format("{0}{2}{1}", def, "SpO2", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_SpO2, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_HeartRate_ini)
            {
                def = string.Format("{0}{2}{1}", def, "HeartRate", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_HeartRate, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_CA_ini)
            {
                def = string.Format("{0}{2}{1}", def, "CA", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_CA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_OA_ini)
            {
                def = string.Format("{0}{2}{1}", def, "OA", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_OA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MA_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MA", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_MA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Hypopnea_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Hypopnea", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Hypopnea, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MT_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MT", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_MT, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_PLMs_ini)
            {
                def = string.Format("{0}{2}{1}", def, "PLMs", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_PLMs, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MicArousal_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MicArousal", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_MicArousal, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_GUID_ini)
            {
                def = string.Format("{0}{2}{1}", def, "GUID", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_GUID, Mulitcase ? "," : "");
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
            if (m_EpochIndex_ini)
            {
                returnstr = string.Format("{0}EpochIndex={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_EpochIndex);
                Mulitcase = true;
            }
            if (m_Stage_ini)
            {
                returnstr = string.Format("{0}Stage={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Stage);
                Mulitcase = true;
            }
            if (m_Pos_ini)
            {
                returnstr = string.Format("{0}Pos={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Pos);
                Mulitcase = true;
            }
            if (m_SpO2_ini)
            {
                returnstr = string.Format("{0}SpO2='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_SpO2);
                Mulitcase = true;
            }
            if (m_HeartRate_ini)
            {
                returnstr = string.Format("{0}HeartRate='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_HeartRate);
                Mulitcase = true;
            }
            if (m_CA_ini)
            {
                returnstr = string.Format("{0}CA={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_CA);
                Mulitcase = true;
            }
            if (m_OA_ini)
            {
                returnstr = string.Format("{0}OA={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_OA);
                Mulitcase = true;
            }
            if (m_MA_ini)
            {
                returnstr = string.Format("{0}MA={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MA);
                Mulitcase = true;
            }
            if (m_Hypopnea_ini)
            {
                returnstr = string.Format("{0}Hypopnea={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Hypopnea);
                Mulitcase = true;
            }
            if (m_MT_ini)
            {
                returnstr = string.Format("{0}MT={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MT);
                Mulitcase = true;
            }
            if (m_PLMs_ini)
            {
                returnstr = string.Format("{0}PLMs={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_PLMs);
                Mulitcase = true;
            }
            if (m_MicArousal_ini)
            {
                returnstr = string.Format("{0}MicArousal={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MicArousal);
                Mulitcase = true;
            }
            if (m_GUID_ini)
            {
                returnstr = string.Format("{0}GUID='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_GUID);
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
            if (m_EpochIndex_ini)
            {
                def = string.Format("{0}{2}EpochIndex={1}", def, m_EpochIndex, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Stage_ini)
            {
                def = string.Format("{0}{2}Stage={1}", def, m_Stage, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Pos_ini)
            {
                def = string.Format("{0}{2}Pos={1}", def, m_Pos, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_SpO2_ini)
            {
                def = string.Format("{0}{2}SpO2='{1}'", def, m_SpO2, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_HeartRate_ini)
            {
                def = string.Format("{0}{2}HeartRate='{1}'", def, m_HeartRate, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_CA_ini)
            {
                def = string.Format("{0}{2}CA={1}", def, m_CA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_OA_ini)
            {
                def = string.Format("{0}{2}OA={1}", def, m_OA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MA_ini)
            {
                def = string.Format("{0}{2}MA={1}", def, m_MA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Hypopnea_ini)
            {
                def = string.Format("{0}{2}Hypopnea={1}", def, m_Hypopnea, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MT_ini)
            {
                def = string.Format("{0}{2}MT={1}", def, m_MT, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_PLMs_ini)
            {
                def = string.Format("{0}{2}PLMs={1}", def, m_PLMs, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MicArousal_ini)
            {
                def = string.Format("{0}{2}MicArousal={1}", def, m_MicArousal, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_GUID_ini)
            {
                def = string.Format("{0}{2}GUID='{1}'", def, m_GUID, Mulitcase ? "," : "");
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
            return ("Epochs");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "ID":
                    ID = Convert.ToInt32(Value);
                    break;
                case "EpochIndex":
                    EpochIndex = Convert.ToInt32(Value);
                    break;
                case "Stage":
                    Stage = string.IsNullOrEmpty(Value.ToString()) ? 0 : Convert.ToInt32(Value);
                    break;
                case "Pos":
                    Pos = string.IsNullOrEmpty(Value.ToString()) ? 0 : Convert.ToInt32(Value);
                    break;
                case "SpO2":
                    SpO2 = Value.ToString();
                    break;
                case "HeartRate":
                    HeartRate = Value.ToString();
                    break;
                case "CA":
                    CA = string.IsNullOrEmpty(Value.ToString()) ? 0 : Convert.ToSingle(Value);
                    break;
                case "OA":
                    OA = string.IsNullOrEmpty(Value.ToString()) ? 0 : Convert.ToSingle(Value);
                    break;
                case "MA":
                    MA = string.IsNullOrEmpty(Value.ToString()) ? 0 : Convert.ToSingle(Value);
                    break;
                case "Hypopnea":
                    Hypopnea = string.IsNullOrEmpty(Value.ToString()) ? 0 : Convert.ToSingle(Value);
                    break;
                case "MT":
                    MT = string.IsNullOrEmpty(Value.ToString()) ? 0 : Convert.ToSingle(Value);
                    break;
                case "PLM":
                    PLM = string.IsNullOrEmpty(Value.ToString()) ? 0 : Convert.ToSingle(Value);
                    break;
                case "PLMs":
                    PLMs=string.IsNullOrEmpty(Value.ToString()) ? 0 : Convert.ToSingle(Value);
                    break;
                case "MicArousal":
                    MicArousal = string.IsNullOrEmpty(Value.ToString()) ? 0 : Convert.ToSingle(Value);
                    break;
                case "GUID":
                    GUID = Value.ToString(); ;
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_Epochs());
        }
        #endregion
        /// <summary>
        /// 当前epoch是否存在与当前数据库中
        /// </summary>
        public bool EpochExist = false;
        /// <summary>
        /// 开关灯状态
        /// </summary>
        public int LightState = 0;
        /// <summary>
        /// 打鼾次数
        /// </summary>
        public float SornCount = 0;
        /// <summary>
        /// 此帧陈施式呼吸事件所占时间（秒）
        /// </summary>
        public float Chen = 0;
        /// <summary>
        /// 是否为手动标记
        /// </summary>
        public bool ByHand { set; get; }
    }
}
