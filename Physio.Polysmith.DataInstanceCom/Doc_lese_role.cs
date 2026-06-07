using pSystem.Interface.Util;
using System;

namespace Physio.Polysmith.DataInstanceCom
{
    public class Doc_lese_role : ITable
    {
        #region 私有成员
        private int m_Id = 0;
        private bool m_Id_ini = false;
        private string m_RoleName = "";
        private bool m_RoleName_ini = false;
        private string m_Remrk = "";
        private bool m_Remrk_ini = false;
        private string m_RoleAuthIds = "";
        private bool m_RoleAuthIds_ini = false;
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
        public string RoleName
        {
            set
            {
                m_RoleName = value;
                m_RoleName_ini = true;
            }
            get
            {
                return m_RoleName;
            }
        }
        public string Remrk
        {
            set
            {
                m_Remrk = value;
                m_Remrk_ini = true;
            }
            get
            {
                return m_Remrk;
            }
        }
        public string RoleAuthIds
        {
            set
            {
                m_RoleAuthIds = value;
                m_RoleAuthIds_ini = true;
            }
            get
            {
                return m_RoleAuthIds;
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
            if (this.m_RoleName_ini)
            {
                def = string.Format("{0}{2}{1}", def, "RoleName", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_RoleName, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Remrk_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Remrk", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Remrk, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_RoleAuthIds_ini)
            {
                def = string.Format("{0}{2}{1}", def, "RoleAuthIds", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_RoleAuthIds, Mulitcase ? "," : "");
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
            if (m_RoleName_ini)
            {
                returnstr = string.Format("{0}RoleName='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_RoleName);
                Mulitcase = true;
            }
            if (m_Remrk_ini)
            {
                returnstr = string.Format("{0}Remrk='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Remrk);
                Mulitcase = true;
            }
            if (m_RoleAuthIds_ini)
            {
                returnstr = string.Format("{0}RoleAuthIds='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_RoleAuthIds);
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
            if (m_RoleName_ini)
            {
                def = string.Format("{0}{2}RoleName='{1}'", def, m_RoleName, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Remrk_ini)
            {
                def = string.Format("{0}{2}Remrk='{1}'", def, m_Remrk, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_RoleAuthIds_ini)
            {
                def = string.Format("{0}{2}RoleAuthIds='{1}'", def, m_RoleAuthIds, Mulitcase ? "," : "");
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
            return ("lese_role");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "Id":
                    Id = Convert.ToInt32(Value);
                    break;
                case "RoleName":
                    RoleName = Value.ToString(); ;
                    break;
                case "Remrk":
                    Remrk = Value.ToString(); ;
                    break;
                case "RoleAuthIds":
                    RoleAuthIds = Value.ToString(); ;
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
            return (new Doc_lese_role());
        }
        #endregion
    }
}
