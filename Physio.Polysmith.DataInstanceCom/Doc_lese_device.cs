using pSystem.Interface.Util;
using System;

namespace Physio.Polysmith.DataInstanceCom
{
    public class Doc_lese_device : ITable
    {
        #region 私有成员
        private int m_Id = 0;
        private bool m_Id_ini = false;
        private string m_Sn = "";
        private bool m_Sn_ini = false;
        private string m_Model = "";
        private bool m_Model_ini = false;
        private int m_Status = 0;
        private bool m_Status_ini = false;
        private int m_OrgId = 0;
        private bool m_OrgId_ini = false;
        private int m_IsAuthorize = 0;
        private bool m_IsAuthorize_ini = false;
        private DateTime m_OperationTime = default(DateTime);
        private bool m_OperationTime_ini = false;
        private int m_OpertatorId = 0;
        private bool m_OpertatorId_ini = false;
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
        public string Sn
        {
            set
            {
                m_Sn = value;
                m_Sn_ini = true;
            }
            get
            {
                return m_Sn;
            }
        }
        public string Model
        {
            set
            {
                m_Model = value;
                m_Model_ini = true;
            }
            get
            {
                return m_Model;
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
        public int OrgId
        {
            set
            {
                m_OrgId = value;
                m_OrgId_ini = true;
            }
            get
            {
                return m_OrgId;
            }
        }
        public int IsAuthorize
        {
            set
            {
                m_IsAuthorize = value;
                m_IsAuthorize_ini = true;
            }
            get
            {
                return m_IsAuthorize;
            }
        }
        public DateTime OperationTime
        {
            set
            {
                m_OperationTime = value;
                m_OperationTime_ini = true;
            }
            get
            {
                return m_OperationTime;
            }
        }
        public int OpertatorId
        {
            set
            {
                m_OpertatorId = value;
                m_OpertatorId_ini = true;
            }
            get
            {
                return m_OpertatorId;
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
            if (this.m_Sn_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Sn", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Sn, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Model_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Model", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Model, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Status_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Status", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Status, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_OrgId_ini)
            {
                def = string.Format("{0}{2}{1}", def, "OrgId", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_OrgId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_IsAuthorize_ini)
            {
                def = string.Format("{0}{2}{1}", def, "IsAuthorize", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_IsAuthorize, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_OperationTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "OperationTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_OperationTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_OpertatorId_ini)
            {
                def = string.Format("{0}{2}{1}", def, "OpertatorId", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_OpertatorId, Mulitcase ? "," : "");
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
            if (m_Sn_ini)
            {
                returnstr = string.Format("{0}Sn='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Sn);
                Mulitcase = true;
            }
            if (m_Model_ini)
            {
                returnstr = string.Format("{0}Model='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Model);
                Mulitcase = true;
            }
            if (m_Status_ini)
            {
                returnstr = string.Format("{0}Status={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Status);
                Mulitcase = true;
            }
            if (m_OrgId_ini)
            {
                returnstr = string.Format("{0}OrgId={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_OrgId);
                Mulitcase = true;
            }
            if (m_IsAuthorize_ini)
            {
                returnstr = string.Format("{0}IsAuthorize={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_IsAuthorize);
                Mulitcase = true;
            }
            if (m_OperationTime_ini)
            {
                returnstr = string.Format("{0}OperationTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_OperationTime);
                Mulitcase = true;
            }
            if (m_OpertatorId_ini)
            {
                returnstr = string.Format("{0}OpertatorId={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_OpertatorId);
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
            if (m_Sn_ini)
            {
                def = string.Format("{0}{2}Sn='{1}'", def, m_Sn, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Model_ini)
            {
                def = string.Format("{0}{2}Model='{1}'", def, m_Model, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Status_ini)
            {
                def = string.Format("{0}{2}Status={1}", def, m_Status, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_OrgId_ini)
            {
                def = string.Format("{0}{2}OrgId={1}", def, m_OrgId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_IsAuthorize_ini)
            {
                def = string.Format("{0}{2}IsAuthorize={1}", def, m_IsAuthorize, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_OperationTime_ini)
            {
                def = string.Format("{0}{2}OperationTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_OperationTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_OpertatorId_ini)
            {
                def = string.Format("{0}{2}OpertatorId={1}", def, m_OpertatorId, Mulitcase ? "," : "");
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
            return ("lese_device");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "Id":
                    Id = Convert.ToInt32(Value);
                    break;
                case "Sn":
                    Sn = Value.ToString(); ;
                    break;
                case "Model":
                    Model = Value.ToString(); ;
                    break;
                case "Status":
                    Status = Convert.ToInt32(Value);
                    break;
                case "OrgId":
                    OrgId = Convert.ToInt32(Value);
                    break;
                case "IsAuthorize":
                    IsAuthorize = Convert.ToInt32(Value);
                    break;
                case "OperationTime":
                    OperationTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "OpertatorId":
                    OpertatorId = Convert.ToInt32(Value);
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_lese_device());
        }
        #endregion
    }
}
