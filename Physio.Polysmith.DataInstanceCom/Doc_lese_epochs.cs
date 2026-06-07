using pSystem.Interface.Util;
using System;

namespace Physio.Polysmith.DataInstanceCom
{
    public class Doc_lese_epochs : ITable
    {
        #region 私有成员
        private int m_Id = 0;
        private bool m_Id_ini = false;
        private int m_EpIndex = 0;
        private bool m_EpIndex_ini = false;
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
        private string m_PaintOrderNum = "";
        private bool m_PaintOrderNum_ini = false;
        #endregion
        #region 公有成员
        public int Id
        {
            set
            {
                m_Id = value;
                m_Id_ini = true;
            }
            get
            {
                return m_Id;
            }
        }
        public int EpIndex
        {
            set
            {
                m_EpIndex = value;
                m_EpIndex_ini = true;
            }
            get
            {
                return m_EpIndex;
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
        public string PaintOrderNum
        {
            set
            {
                m_PaintOrderNum = value;
                m_PaintOrderNum_ini = true;
            }
            get
            {
                return m_PaintOrderNum;
            }
        }
        #endregion
        #region 继承成员
        public string GetInsertString()
        {
            string def = " (";
            string val = " VALUES(";
            bool Mulitcase = false;
            if (this.m_Id_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Id", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Id, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_EpIndex_ini)
            {
                def = string.Format("{0}{2}{1}", def, "EpIndex", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_EpIndex, Mulitcase ? "," : "");
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
            if (this.m_PaintOrderNum_ini)
            {
                def = string.Format("{0}{2}{1}", def, "PaintOrderNum", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_PaintOrderNum, Mulitcase ? "," : "");
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
            if (m_Id_ini)
            {
                returnstr = string.Format("{0}Id={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Id);
                Mulitcase = true;
            }
            if (m_EpIndex_ini)
            {
                returnstr = string.Format("{0}EpIndex={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_EpIndex);
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
            if (m_PaintOrderNum_ini)
            {
                returnstr = string.Format("{0}PaintOrderNum='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_PaintOrderNum);
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
            if (m_Id_ini)
            {
                def = string.Format("{0}{2}Id={1}", def, m_Id, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_EpIndex_ini)
            {
                def = string.Format("{0}{2}EpIndex={1}", def, m_EpIndex, Mulitcase ? "," : "");
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
            if (m_PaintOrderNum_ini)
            {
                def = string.Format("{0}{2}PaintOrderNum='{1}'", def, m_PaintOrderNum, Mulitcase ? "," : "");
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
            return ("lese_epochs");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "Id":
                    Id = Convert.ToInt32(Value);
                    break;
                case "EpIndex":
                    EpIndex = Convert.ToInt32(Value);
                    break;
                case "Stage":
                    Stage = Convert.ToInt32(Value);
                    break;
                case "Pos":
                    Pos = Convert.ToInt32(Value);
                    break;
                case "SpO2":
                    SpO2 = Value.ToString(); ;
                    break;
                case "HeartRate":
                    HeartRate = Value.ToString(); ;
                    break;
                case "CA":
                    CA = Convert.ToSingle(Value);
                    break;
                case "OA":
                    OA = Convert.ToSingle(Value);
                    break;
                case "MA":
                    MA = Convert.ToSingle(Value);
                    break;
                case "Hypopnea":
                    Hypopnea = Convert.ToSingle(Value);
                    break;
                case "MT":
                    MT = Convert.ToSingle(Value);
                    break;
                case "PaintOrderNum":
                    PaintOrderNum = Value.ToString(); ;
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_lese_epochs());
        }
        #endregion
    }
}
