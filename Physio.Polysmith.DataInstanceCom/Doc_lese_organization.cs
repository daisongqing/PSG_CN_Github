using pSystem.Interface.Util;
using System;

namespace Physio.Polysmith.DataInstanceCom
{
    public class Doc_lese_organization : ITable
    {
        #region 私有成员
        private int m_Id = 0;
        private bool m_Id_ini = false;
        private string m_Name = "";
        private bool m_Name_ini = false;
        private string m_Num = "";
        private bool m_Num_ini = false;
        private string m_Contacts = "";
        private bool m_Contacts_ini = false;
        private string m_Phone = "";
        private bool m_Phone_ini = false;
        private string m_Address = "";
        private bool m_Address_ini = false;
        private int m_IsUse = 0;
        private bool m_IsUse_ini = false;
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
        public string Name
        {
            set
            {
                m_Name = value;
                m_Name_ini = true;
            }
            get
            {
                return m_Name;
            }
        }
        public string Num
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
        public string Contacts
        {
            set
            {
                m_Contacts = value;
                m_Contacts_ini = true;
            }
            get
            {
                return m_Contacts;
            }
        }
        public string Phone
        {
            set
            {
                m_Phone = value;
                m_Phone_ini = true;
            }
            get
            {
                return m_Phone;
            }
        }
        public string Address
        {
            set
            {
                m_Address = value;
                m_Address_ini = true;
            }
            get
            {
                return m_Address;
            }
        }
        public int IsUse
        {
            set
            {
                m_IsUse = value;
                m_IsUse_ini = true;
            }
            get
            {
                return m_IsUse;
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
            if (this.m_Name_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Name", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Name, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Num_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Num", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Num, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Contacts_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Contacts", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Contacts, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Phone_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Phone", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Phone, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Address_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Address", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Address, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_IsUse_ini)
            {
                def = string.Format("{0}{2}{1}", def, "IsUse", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_IsUse, Mulitcase ? "," : "");
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
            if (m_Name_ini)
            {
                returnstr = string.Format("{0}Name='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Name);
                Mulitcase = true;
            }
            if (m_Num_ini)
            {
                returnstr = string.Format("{0}Num='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Num);
                Mulitcase = true;
            }
            if (m_Contacts_ini)
            {
                returnstr = string.Format("{0}Contacts='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Contacts);
                Mulitcase = true;
            }
            if (m_Phone_ini)
            {
                returnstr = string.Format("{0}Phone='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Phone);
                Mulitcase = true;
            }
            if (m_Address_ini)
            {
                returnstr = string.Format("{0}Address='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Address);
                Mulitcase = true;
            }
            if (m_IsUse_ini)
            {
                returnstr = string.Format("{0}IsUse={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_IsUse);
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
            if (m_Name_ini)
            {
                def = string.Format("{0}{2}Name='{1}'", def, m_Name, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Num_ini)
            {
                def = string.Format("{0}{2}Num='{1}'", def, m_Num, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Contacts_ini)
            {
                def = string.Format("{0}{2}Contacts='{1}'", def, m_Contacts, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Phone_ini)
            {
                def = string.Format("{0}{2}Phone='{1}'", def, m_Phone, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Address_ini)
            {
                def = string.Format("{0}{2}Address='{1}'", def, m_Address, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_IsUse_ini)
            {
                def = string.Format("{0}{2}IsUse={1}", def, m_IsUse, Mulitcase ? "," : "");
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
            return ("lese_organization");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "Id":
                    Id = Convert.ToInt32(Value);
                    break;
                case "Name":
                    Name = Value.ToString(); ;
                    break;
                case "Num":
                    Num = Value.ToString(); ;
                    break;
                case "Contacts":
                    Contacts = Value.ToString(); ;
                    break;
                case "Phone":
                    Phone = Value.ToString(); ;
                    break;
                case "Address":
                    Address = Value.ToString(); ;
                    break;
                case "IsUse":
                    IsUse = Convert.ToInt32(Value);
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_lese_organization());
        }
        #endregion
    }
}
