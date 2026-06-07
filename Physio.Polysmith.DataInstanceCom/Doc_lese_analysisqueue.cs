using pSystem.Interface.Util;
using System;

namespace Physio.Polysmith.DataInstanceCom
{
    public class Doc_lese_analysisqueue : ITable
    {
        #region 私有成员
        private int m_Id = 0;
        private bool m_Id_ini = false;
        private int m_Num = 0;
        private bool m_Num_ini = false;
        private string m_OrderNum = "";
        private bool m_OrderNum_ini = false;
        private string m_ClientMac = "";
        private bool m_ClientMac_ini = false;
        private int m_AnalysisStatus = 0;
        private bool m_AnalysisStatus_ini = false;
        private DateTime m_CreateTime = default(DateTime);
        private bool m_CreateTime_ini = false;
        private string m_Conditions = "";
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
        public int Num
        {
            set
            {
                m_Num = value;
                m_Num_ini = true;
            }
            get
            {
                return m_Num;
            }
        }
        public string OrderNum
        {
            set
            {
                m_OrderNum = value;
                m_OrderNum_ini = true;
            }
            get
            {
                return m_OrderNum;
            }
        }
        public string ClientMac
        {
            set
            {
                m_ClientMac = value;
                m_ClientMac_ini = true;
            }
            get
            {
                return m_ClientMac;
            }
        }
        public int AnalysisStatus
        {
            set
            {
                m_AnalysisStatus = value;
                m_AnalysisStatus_ini = true;
            }
            get
            {
                return m_AnalysisStatus;
            }
        }
        public DateTime CreateTime
        {
            set
            {
                m_CreateTime = value;
                m_CreateTime_ini = true;
            }
            get
            {
                return m_CreateTime;
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
            if (this.m_Num_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Num", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Num, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_OrderNum_ini)
            {
                def = string.Format("{0}{2}{1}", def, "OrderNum", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_OrderNum, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ClientMac_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ClientMac", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_ClientMac, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_AnalysisStatus_ini)
            {
                def = string.Format("{0}{2}{1}", def, "AnalysisStatus", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_AnalysisStatus, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_CreateTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "CreateTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_CreateTime, Mulitcase ? "," : "");
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
            if (m_Num_ini)
            {
                returnstr = string.Format("{0}Num={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Num);
                Mulitcase = true;
            }
            if (m_OrderNum_ini)
            {
                returnstr = string.Format("{0}OrderNum='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_OrderNum);
                Mulitcase = true;
            }
            if (m_ClientMac_ini)
            {
                returnstr = string.Format("{0}ClientMac='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ClientMac);
                Mulitcase = true;
            }
            if (m_AnalysisStatus_ini)
            {
                returnstr = string.Format("{0}AnalysisStatus={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_AnalysisStatus);
                Mulitcase = true;
            }
            if (m_CreateTime_ini)
            {
                returnstr = string.Format("{0}CreateTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_CreateTime);
                Mulitcase = true;
            }
            if (m_Conditions != "")
            {
                returnstr = string.Format("{0} {2} {1}", returnstr, m_Conditions, Mulitcase ? "AND" : "");
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
            if (m_Num_ini)
            {
                def = string.Format("{0}{2}Num={1}", def, m_Num, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_OrderNum_ini)
            {
                def = string.Format("{0}{2}OrderNum='{1}'", def, m_OrderNum, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ClientMac_ini)
            {
                def = string.Format("{0}{2}ClientMac='{1}'", def, m_ClientMac, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_AnalysisStatus_ini)
            {
                def = string.Format("{0}{2}AnalysisStatus={1}", def, m_AnalysisStatus, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_CreateTime_ini)
            {
                def = string.Format("{0}{2}CreateTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_CreateTime, Mulitcase ? "," : "");
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
            return ("lese_analysisqueue");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "Id":
                    Id = Convert.ToInt32(Value);
                    break;
                case "Num":
                    Num = Convert.ToInt32(Value);
                    break;
                case "OrderNum":
                    OrderNum = Value.ToString(); ;
                    break;
                case "ClientMac":
                    ClientMac = Value.ToString(); ;
                    break;
                case "AnalysisStatus":
                    AnalysisStatus = Convert.ToInt32(Value);
                    break;
                case "CreateTime":
                    CreateTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_lese_analysisqueue());
        }
        #endregion
        /// <summary>
        /// 加入时间判断条件
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="CompareSign"></param>
        public void AddTimeConditions(DateTime dt, string CompareSign)
        {
            m_Conditions = string.Format(" CreateTime{0}'{1}'", CompareSign, dt);
        }
    }
}
