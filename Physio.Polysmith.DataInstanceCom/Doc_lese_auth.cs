using pSystem.Interface.Util;
using System;

namespace Physio.Polysmith.DataInstanceCom
{
    public class Doc_lese_auth : ITable
    {
        #region 私有成员
        private int m_Id = 0;
        private bool m_Id_ini = false;
        private string m_AuthName = "";
        private bool m_AuthName_ini = false;
        private int m_Pid = 0;
        private bool m_Pid_ini = false;
        private string m_PidPath = "";
        private bool m_PidPath_ini = false;
        private int m_Level = 0;
        private bool m_Level_ini = false;
        private string m_AuthM = "";
        private bool m_AuthM_ini = false;
        private string m_AuthC = "";
        private bool m_AuthC_ini = false;
        private string m_AuthA = "";
        private bool m_AuthA_ini = false;
        private int m_IsNav = 0;
        private bool m_IsNav_ini = false;
        private int m_Sort = 0;
        private bool m_Sort_ini = false;
        private DateTime m_CreateTime = default(DateTime);
        private bool m_CreateTime_ini = false;
        private DateTime m_UpdateTime = default(DateTime);
        private bool m_UpdateTime_ini = false;
        private DateTime m_DeleteTime = default(DateTime);
        private bool m_DeleteTime_ini = false;
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
        public string AuthName
        {
            set
            {
                m_AuthName = value;
                m_AuthName_ini = true;
            }
            get
            {
                return m_AuthName;
            }
        }
        public int Pid
        {
            set
            {
                m_Pid = value;
                m_Pid_ini = true;
            }
            get
            {
                return m_Pid;
            }
        }
        public string PidPath
        {
            set
            {
                m_PidPath = value;
                m_PidPath_ini = true;
            }
            get
            {
                return m_PidPath;
            }
        }
        public int Level
        {
            set
            {
                m_Level = value;
                m_Level_ini = true;
            }
            get
            {
                return m_Level;
            }
        }
        public string AuthM
        {
            set
            {
                m_AuthM = value;
                m_AuthM_ini = true;
            }
            get
            {
                return m_AuthM;
            }
        }
        public string AuthC
        {
            set
            {
                m_AuthC = value;
                m_AuthC_ini = true;
            }
            get
            {
                return m_AuthC;
            }
        }
        public string AuthA
        {
            set
            {
                m_AuthA = value;
                m_AuthA_ini = true;
            }
            get
            {
                return m_AuthA;
            }
        }
        public int IsNav
        {
            set
            {
                m_IsNav = value;
                m_IsNav_ini = true;
            }
            get
            {
                return m_IsNav;
            }
        }
        public int Sort
        {
            set
            {
                m_Sort = value;
                m_Sort_ini = true;
            }
            get
            {
                return m_Sort;
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
        public DateTime UpdateTime
        {
            set
            {
                m_UpdateTime = value;
                m_UpdateTime_ini = true;
            }
            get
            {
                return m_UpdateTime;
            }
        }
        public DateTime DeleteTime
        {
            set
            {
                m_DeleteTime = value;
                m_DeleteTime_ini = true;
            }
            get
            {
                return m_DeleteTime;
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
            if (this.m_AuthName_ini)
            {
                def = string.Format("{0}{2}{1}", def, "AuthName", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_AuthName, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Pid_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Pid", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Pid, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_PidPath_ini)
            {
                def = string.Format("{0}{2}{1}", def, "PidPath", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_PidPath, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Level_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Level", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Level, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_AuthM_ini)
            {
                def = string.Format("{0}{2}{1}", def, "AuthM", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_AuthM, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_AuthC_ini)
            {
                def = string.Format("{0}{2}{1}", def, "AuthC", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_AuthC, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_AuthA_ini)
            {
                def = string.Format("{0}{2}{1}", def, "AuthA", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_AuthA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_IsNav_ini)
            {
                def = string.Format("{0}{2}{1}", def, "IsNav", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_IsNav, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Sort_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Sort", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Sort, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_CreateTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "CreateTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_CreateTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_UpdateTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "UpdateTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_UpdateTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_DeleteTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "DeleteTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_DeleteTime, Mulitcase ? "," : "");
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
            if (m_AuthName_ini)
            {
                returnstr = string.Format("{0}AuthName='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_AuthName);
                Mulitcase = true;
            }
            if (m_Pid_ini)
            {
                returnstr = string.Format("{0}Pid={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Pid);
                Mulitcase = true;
            }
            if (m_PidPath_ini)
            {
                returnstr = string.Format("{0}PidPath='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_PidPath);
                Mulitcase = true;
            }
            if (m_Level_ini)
            {
                returnstr = string.Format("{0}Level={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Level);
                Mulitcase = true;
            }
            if (m_AuthM_ini)
            {
                returnstr = string.Format("{0}AuthM='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_AuthM);
                Mulitcase = true;
            }
            if (m_AuthC_ini)
            {
                returnstr = string.Format("{0}AuthC='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_AuthC);
                Mulitcase = true;
            }
            if (m_AuthA_ini)
            {
                returnstr = string.Format("{0}AuthA='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_AuthA);
                Mulitcase = true;
            }
            if (m_IsNav_ini)
            {
                returnstr = string.Format("{0}IsNav={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_IsNav);
                Mulitcase = true;
            }
            if (m_Sort_ini)
            {
                returnstr = string.Format("{0}Sort={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Sort);
                Mulitcase = true;
            }
            if (m_CreateTime_ini)
            {
                returnstr = string.Format("{0}CreateTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_CreateTime);
                Mulitcase = true;
            }
            if (m_UpdateTime_ini)
            {
                returnstr = string.Format("{0}UpdateTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_UpdateTime);
                Mulitcase = true;
            }
            if (m_DeleteTime_ini)
            {
                returnstr = string.Format("{0}DeleteTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_DeleteTime);
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
            if (m_AuthName_ini)
            {
                def = string.Format("{0}{2}AuthName='{1}'", def, m_AuthName, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Pid_ini)
            {
                def = string.Format("{0}{2}Pid={1}", def, m_Pid, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_PidPath_ini)
            {
                def = string.Format("{0}{2}PidPath='{1}'", def, m_PidPath, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Level_ini)
            {
                def = string.Format("{0}{2}Level={1}", def, m_Level, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_AuthM_ini)
            {
                def = string.Format("{0}{2}AuthM='{1}'", def, m_AuthM, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_AuthC_ini)
            {
                def = string.Format("{0}{2}AuthC='{1}'", def, m_AuthC, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_AuthA_ini)
            {
                def = string.Format("{0}{2}AuthA='{1}'", def, m_AuthA, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_IsNav_ini)
            {
                def = string.Format("{0}{2}IsNav={1}", def, m_IsNav, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Sort_ini)
            {
                def = string.Format("{0}{2}Sort={1}", def, m_Sort, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_CreateTime_ini)
            {
                def = string.Format("{0}{2}CreateTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_CreateTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_UpdateTime_ini)
            {
                def = string.Format("{0}{2}UpdateTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_UpdateTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_DeleteTime_ini)
            {
                def = string.Format("{0}{2}DeleteTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_DeleteTime, Mulitcase ? "," : "");
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
            return ("lese_auth");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "Id":
                    Id = Convert.ToInt32(Value);
                    break;
                case "AuthName":
                    AuthName = Value.ToString(); ;
                    break;
                case "Pid":
                    Pid = Convert.ToInt32(Value);
                    break;
                case "PidPath":
                    PidPath = Value.ToString(); ;
                    break;
                case "Level":
                    Level = Convert.ToInt32(Value);
                    break;
                case "AuthM":
                    AuthM = Value.ToString(); ;
                    break;
                case "AuthC":
                    AuthC = Value.ToString(); ;
                    break;
                case "AuthA":
                    AuthA = Value.ToString(); ;
                    break;
                case "IsNav":
                    IsNav = Convert.ToInt32(Value);
                    break;
                case "Sort":
                    Sort = Convert.ToInt32(Value);
                    break;
                case "CreateTime":
                    CreateTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "UpdateTime":
                    UpdateTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "DeleteTime":
                    DeleteTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_lese_auth());
        }
        #endregion
    }
}
