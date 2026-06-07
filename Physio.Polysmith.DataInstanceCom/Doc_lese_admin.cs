using pSystem.Interface.Util;
using System;

namespace Physio.Polysmith.DataInstanceCom
{
    /// <summary>
    /// 账户管理
    /// </summary>
    public class Doc_lese_admin : ITable
    {
        #region 私有成员
        private int m_Id = 0;
        private bool m_Id_ini = false;
        private string m_UserName = "";
        private bool m_UserName_ini = false;
        private string m_Password = "";
        private bool m_Password_ini = false;
        private string m_Email = "";
        private bool m_Email_ini = false;
        private string m_NickName = "";
        private bool m_NickName_ini = false;
        private DateTime m_LastLoginLtime = default(DateTime);
        private bool m_LastLoginLtime_ini = false;
        private int m_Status = 0;
        private bool m_Status_ini = false;
        private int m_RoleId = 0;
        private bool m_RoleId_ini = false;
        private DateTime m_CreateTime = default(DateTime);
        private bool m_CreateTime_ini = false;
        private DateTime m_UpdateTime = default(DateTime);
        private bool m_UpdateTime_ini = false;
        private DateTime m_DeleteTime = default(DateTime);
        private bool m_DeleteTime_ini = false;
        private DateTime m_PsgLastLoginTime = default(DateTime);
        private bool m_PsgLastLoginTime_ini = false;
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
        public string UserName
        {
            set
            {
                m_UserName = value;
                m_UserName_ini = true;
            }
            get
            {
                return m_UserName;
            }
        }
        public string Password
        {
            set
            {
                m_Password = value;
                m_Password_ini = true;
            }
            get
            {
                return m_Password;
            }
        }
        public string Email
        {
            set
            {
                m_Email = value;
                m_Email_ini = true;
            }
            get
            {
                return m_Email;
            }
        }
        public string NickName
        {
            set
            {
                m_NickName = value;
                m_NickName_ini = true;
            }
            get
            {
                return m_NickName;
            }
        }
        public DateTime LastLoginLtime
        {
            set
            {
                m_LastLoginLtime = value;
                m_LastLoginLtime_ini = true;
            }
            get
            {
                return m_LastLoginLtime;
            }
        }
        public int Status
        {
            set
            {
                m_Status = value;
                m_Status_ini = true;
            }
            get
            {
                return m_Status;
            }
        }
        public int RoleId
        {
            set
            {
                m_RoleId = value;
                m_RoleId_ini = true;
            }
            get
            {
                return m_RoleId;
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
        public DateTime PsgLastLoginTime
        {
            set
            {
                m_PsgLastLoginTime = value;
                m_PsgLastLoginTime_ini = true;
            }
            get
            {
                return m_PsgLastLoginTime;
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
            if (this.m_UserName_ini)
            {
                def = string.Format("{0}{2}{1}", def, "UserName", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_UserName, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Password_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Password", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Password, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Email_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Email", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Email, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_NickName_ini)
            {
                def = string.Format("{0}{2}{1}", def, "NickName", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_NickName, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_LastLoginLtime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "LastLoginLtime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_LastLoginLtime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Status_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Status", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Status, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_RoleId_ini)
            {
                def = string.Format("{0}{2}{1}", def, "RoleId", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_RoleId, Mulitcase ? "," : "");
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
            if (this.m_PsgLastLoginTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "PsgLastLoginTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_PsgLastLoginTime, Mulitcase ? "," : "");
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
            if (m_UserName_ini)
            {
                returnstr = string.Format("{0}UserName='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_UserName);
                Mulitcase = true;
            }
            if (m_Password_ini)
            {
                returnstr = string.Format("{0}Password='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Password);
                Mulitcase = true;
            }
            if (m_Email_ini)
            {
                returnstr = string.Format("{0}Email='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Email);
                Mulitcase = true;
            }
            if (m_NickName_ini)
            {
                returnstr = string.Format("{0}NickName='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_NickName);
                Mulitcase = true;
            }
            if (m_LastLoginLtime_ini)
            {
                returnstr = string.Format("{0}LastLoginLtime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_LastLoginLtime);
                Mulitcase = true;
            }
            if (m_Status_ini)
            {
                returnstr = string.Format("{0}Status={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Status);
                Mulitcase = true;
            }
            if (m_RoleId_ini)
            {
                returnstr = string.Format("{0}RoleId={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_RoleId);
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
            if (m_PsgLastLoginTime_ini)
            {
                returnstr = string.Format("{0}PsgLastLoginTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_PsgLastLoginTime);
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
            if (m_UserName_ini)
            {
                def = string.Format("{0}{2}UserName='{1}'", def, m_UserName, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Password_ini)
            {
                def = string.Format("{0}{2}Password='{1}'", def, m_Password, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Email_ini)
            {
                def = string.Format("{0}{2}Email='{1}'", def, m_Email, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_NickName_ini)
            {
                def = string.Format("{0}{2}NickName='{1}'", def, m_NickName, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_LastLoginLtime_ini)
            {
                def = string.Format("{0}{2}LastLoginLtime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_LastLoginLtime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Status_ini)
            {
                def = string.Format("{0}{2}Status={1}", def, m_Status, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_RoleId_ini)
            {
                def = string.Format("{0}{2}RoleId={1}", def, m_RoleId, Mulitcase ? "," : "");
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
            if (m_PsgLastLoginTime_ini)
            {
                def = string.Format("{0}{2}PsgLastLoginTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_PsgLastLoginTime, Mulitcase ? "," : "");
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
            return ("lese_admin");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "Id":
                    Id = Convert.ToInt32(Value);
                    break;
                case "UserName":
                    UserName = Value.ToString(); ;
                    break;
                case "Password":
                    Password = Value.ToString(); ;
                    break;
                case "Email":
                    Email = Value.ToString(); ;
                    break;
                case "NickName":
                    NickName = Value.ToString(); ;
                    break;
                case "LastLoginLtime":
                    LastLoginLtime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "Status":
                    Status = Convert.ToInt32(Value);
                    break;
                case "RoleId":
                    RoleId = Convert.ToInt32(Value);
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
                case "PsgLastLoginTime":
                    PsgLastLoginTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_lese_admin());
        }
        #endregion
    }
}
