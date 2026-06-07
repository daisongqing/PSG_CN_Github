using pSystem.Interface.Util;
using System;

namespace Physio.Polysmith.DataInstanceCom
{
    public class Doc_lese_doctorclients : ITable
    {
        #region 私有成员
        private int m_Id = 0;
        private bool m_Id_ini = false;
        private int m_DoctorId = 0;
        private bool m_DoctorId_ini = false;
        private string m_ClientMac = "";
        private bool m_ClientMac_ini = false;
        private int m_IsOnline = 0;
        private bool m_IsOnline_ini = false;
        private int m_IsActive = 0;
        private bool m_IsActive_ini = false;
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
        public int DoctorId
        {
            set
            {
                m_DoctorId = value;
                m_DoctorId_ini = true;
            }
            get
            {
                return m_DoctorId;
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
        public int IsOnline
        {
            set
            {
                m_IsOnline = value;
                m_IsOnline_ini = true;
            }
            get
            {
                return m_IsOnline;
            }
        }
        public int IsActive
        {
            set
            {
                m_IsActive = value;
                m_IsActive_ini = true;
            }
            get
            {
                return m_IsActive;
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
            if (this.m_DoctorId_ini)
            {
                def = string.Format("{0}{2}{1}", def, "DoctorId", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_DoctorId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ClientMac_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ClientMac", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_ClientMac, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_IsOnline_ini)
            {
                def = string.Format("{0}{2}{1}", def, "IsOnline", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_IsOnline, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_IsActive_ini)
            {
                def = string.Format("{0}{2}{1}", def, "IsActive", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_IsActive, Mulitcase ? "," : "");
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
            if (m_DoctorId_ini)
            {
                returnstr = string.Format("{0}DoctorId={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_DoctorId);
                Mulitcase = true;
            }
            if (m_ClientMac_ini)
            {
                returnstr = string.Format("{0}ClientMac='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ClientMac);
                Mulitcase = true;
            }
            if (m_IsOnline_ini)
            {
                returnstr = string.Format("{0}IsOnline={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_IsOnline);
                Mulitcase = true;
            }
            if (m_IsActive_ini)
            {
                returnstr = string.Format("{0}IsActive={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_IsActive);
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
            if (m_DoctorId_ini)
            {
                def = string.Format("{0}{2}DoctorId={1}", def, m_DoctorId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ClientMac_ini)
            {
                def = string.Format("{0}{2}ClientMac='{1}'", def, m_ClientMac, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_IsOnline_ini)
            {
                def = string.Format("{0}{2}IsOnline={1}", def, m_IsOnline, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_IsActive_ini)
            {
                def = string.Format("{0}{2}IsActive={1}", def, m_IsActive, Mulitcase ? "," : "");
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
            return ("lese_doctorclients");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "Id":
                    Id = Convert.ToInt32(Value);
                    break;
                case "DoctorId":
                    DoctorId = Convert.ToInt32(Value);
                    break;
                case "ClientMac":
                    ClientMac = Value.ToString(); ;
                    break;
                case "IsOnline":
                    IsOnline = Convert.ToInt32(Value);
                    break;
                case "IsActive":
                    IsActive = Convert.ToInt32(Value);
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_lese_doctorclients());
        }
        #endregion
    }
}
