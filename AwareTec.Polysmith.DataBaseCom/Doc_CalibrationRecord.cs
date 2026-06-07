using pSystem.Interface.Util;
using System;

namespace AwareTec.Polysmith.DataBaseCom
{
    /// <summary>
    /// 定标记录
    /// </summary>
    public class Doc_CalibrationRecord : ITable
    {
        #region 私有成员
        private int m_ID = 0;
        private bool m_ID_ini = false;
        private string m_MatchKey = "";
        private bool m_MatchKey_ini = false;
        private DateTime m_StartTime = default(DateTime);
        private bool m_StartTime_ini = false;
        private string m_Comments = "";
        private bool m_Comments_ini = false;
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
        public string MatchKey
        {
            set
            {
                m_MatchKey = value;
                m_MatchKey_ini = true;
            }
            get
            {
                return m_MatchKey;
            }
        }
        public DateTime StartTime
        {
            set
            {
                m_StartTime = value;
                m_StartTime_ini = true;
            }
            get
            {
                return m_StartTime;
            }
        }
        public string Comments
        {
            set
            {
                m_Comments = value;
                m_Comments_ini = true;
            }
            get
            {
                return m_Comments;
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
            if (this.m_MatchKey_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MatchKey", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_MatchKey, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_StartTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "StartTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_StartTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Comments_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Comments", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Comments, Mulitcase ? "," : "");
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
            if (m_MatchKey_ini)
            {
                returnstr = string.Format("{0}MatchKey='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MatchKey);
                Mulitcase = true;
            }
            if (m_StartTime_ini)
            {
                returnstr = string.Format("{0}StartTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_StartTime);
                Mulitcase = true;
            }
            if (m_Comments_ini)
            {
                returnstr = string.Format("{0}Comments='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Comments);
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
            if (m_MatchKey_ini)
            {
                def = string.Format("{0}{2}MatchKey='{1}'", def, m_MatchKey, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_StartTime_ini)
            {
                def = string.Format("{0}{2}StartTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_StartTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Comments_ini)
            {
                def = string.Format("{0}{2}Comments='{1}'", def, m_Comments, Mulitcase ? "," : "");
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
            return ("CalibrationRecord");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "ID":
                    ID = Convert.ToInt32(Value);
                    break;
                case "MatchKey":
                    MatchKey = Value.ToString(); ;
                    break;
                case "StartTime":
                    StartTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "Comments":
                    Comments = Value.ToString(); ;
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_CalibrationRecord());
        }
        #endregion
    }
}
