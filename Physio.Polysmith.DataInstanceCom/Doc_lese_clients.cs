using pSystem.Interface.Util;
using System;

namespace Physio.Polysmith.DataInstanceCom
{
    /// <summary>
    /// 客户端表单类
    /// </summary>
    public class Doc_lese_clients : ITable
    {
        #region 私有成员
        private int m_Id = 0;
        private bool m_Id_ini = false;
        private string m_ClientName = "";
        private bool m_ClientName_ini = false;
        private string m_ClientMac = "";
        private bool m_ClientMac_ini = false;
        private string m_EdfPath = "";
        private bool m_EdfPath_ini = false;
        private string m_CopyPath = "";
        private bool m_CopyPath_ini = false;
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
        public string ClientName
        {
            set
            {
                m_ClientName = value;
                m_ClientName_ini = true;
            }
            get
            {
                return m_ClientName;
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
        public string EdfPath
        {
            set
            {
                m_EdfPath = value.Replace(AppDomain.CurrentDomain.BaseDirectory, "...\\");
                m_EdfPath_ini = true;
            }
            get
            {
                return m_EdfPath.Replace("...\\", AppDomain.CurrentDomain.BaseDirectory);
            }
        }
        public string CopyPath
        {
            set
            {
                m_CopyPath = value.Replace(AppDomain.CurrentDomain.BaseDirectory, "...\\");
                m_CopyPath_ini = true;
            }
            get
            {
                return m_CopyPath.Replace("...\\", AppDomain.CurrentDomain.BaseDirectory);
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
            if (this.m_ClientName_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ClientName", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_ClientName, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ClientMac_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ClientMac", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_ClientMac, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_EdfPath_ini)
            {
                def = string.Format("{0}{2}{1}", def, "EdfPath", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_EdfPath, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_CopyPath_ini)
            {
                def = string.Format("{0}{2}{1}", def, "CopyPath", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_CopyPath, Mulitcase ? "," : "");
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
            if (m_ClientName_ini)
            {
                returnstr = string.Format("{0}ClientName='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ClientName);
                Mulitcase = true;
            }
            if (m_ClientMac_ini)
            {
                returnstr = string.Format("{0}ClientMac='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ClientMac);
                Mulitcase = true;
            }
            if (m_EdfPath_ini)
            {
                returnstr = string.Format("{0}EdfPath='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_EdfPath);
                Mulitcase = true;
            }
            if (m_CopyPath_ini)
            {
                returnstr = string.Format("{0}CopyPath='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_CopyPath);
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
            if (m_ClientName_ini)
            {
                def = string.Format("{0}{2}ClientName='{1}'", def, m_ClientName, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ClientMac_ini)
            {
                def = string.Format("{0}{2}ClientMac='{1}'", def, m_ClientMac, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_EdfPath_ini)
            {
                def = string.Format("{0}{2}EdfPath='{1}'", def, m_EdfPath, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_CopyPath_ini)
            {
                def = string.Format("{0}{2}CopyPath='{1}'", def, m_CopyPath, Mulitcase ? "," : "");
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
            return ("lese_clients");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "Id":
                    Id = Convert.ToInt32(Value);
                    break;
                case "ClientName":
                    ClientName = Value.ToString(); ;
                    break;
                case "ClientMac":
                    ClientMac = Value.ToString(); ;
                    break;
                case "EdfPath":
                    EdfPath = Value.ToString(); ;
                    break;
                case "CopyPath":
                    CopyPath = Value.ToString(); ;
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_lese_clients());
        }
        #endregion
    }
}
