using pSystem.Interface.Util;
using System;

namespace Physio.Polysmith.DataInstanceCom
{
    public class Doc_lese_servicescenter : ITable
    {
        #region 私有成员
        private int m_Id = 0;
        private bool m_Id_ini = false;
        private int m_OrgId = 0;
        private bool m_OrgId_ini = false;
        private string m_CenterName = "";
        private bool m_CenterName_ini = false;
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
        private string m_Pic = "";
        private bool m_Pic_ini = false;
        private string m_Remark = "";
        private bool m_Remark_ini = false;
        private string m_DocIds = "";
        private bool m_DocIds_ini = false;
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
        public string CenterName
        {
            set
            {
                m_CenterName = value;
                m_CenterName_ini = true;
            }
            get
            {
                return m_CenterName;
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
        public string Pic
        {
            set
            {
                m_Pic = value;
                m_Pic_ini = true;
            }
            get
            {
                return m_Pic;
            }
        }
        public string Remark
        {
            set
            {
                m_Remark = value;
                m_Remark_ini = true;
            }
            get
            {
                return m_Remark;
            }
        }
        public string DocIds
        {
            set
            {
                m_DocIds = value;
                m_DocIds_ini = true;
            }
            get
            {
                return m_DocIds;
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
            if (this.m_OrgId_ini)
            {
                def = string.Format("{0}{2}{1}", def, "OrgId", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_OrgId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_CenterName_ini)
            {
                def = string.Format("{0}{2}{1}", def, "CenterName", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_CenterName, Mulitcase ? "," : "");
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
            if (this.m_Pic_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Pic", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Pic, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Remark_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Remark", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Remark, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_DocIds_ini)
            {
                def = string.Format("{0}{2}{1}", def, "DocIds", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_DocIds, Mulitcase ? "," : "");
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
            if (m_OrgId_ini)
            {
                returnstr = string.Format("{0}OrgId={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_OrgId);
                Mulitcase = true;
            }
            if (m_CenterName_ini)
            {
                returnstr = string.Format("{0}CenterName='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_CenterName);
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
            if (m_Pic_ini)
            {
                returnstr = string.Format("{0}Pic='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Pic);
                Mulitcase = true;
            }
            if (m_Remark_ini)
            {
                returnstr = string.Format("{0}Remark='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Remark);
                Mulitcase = true;
            }
            if (m_DocIds_ini)
            {
                returnstr = string.Format("{0}DocIds='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_DocIds);
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
            if (m_OrgId_ini)
            {
                def = string.Format("{0}{2}OrgId={1}", def, m_OrgId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_CenterName_ini)
            {
                def = string.Format("{0}{2}CenterName='{1}'", def, m_CenterName, Mulitcase ? "," : "");
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
            if (m_Pic_ini)
            {
                def = string.Format("{0}{2}Pic='{1}'", def, m_Pic, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Remark_ini)
            {
                def = string.Format("{0}{2}Remark='{1}'", def, m_Remark, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_DocIds_ini)
            {
                def = string.Format("{0}{2}DocIds='{1}'", def, m_DocIds, Mulitcase ? "," : "");
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
            return ("lese_servicescenter");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "Id":
                    Id = Convert.ToInt32(Value);
                    break;
                case "OrgId":
                    OrgId = Convert.ToInt32(Value);
                    break;
                case "CenterName":
                    CenterName = Value.ToString(); ;
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
                case "Pic":
                    Pic = Value.ToString(); ;
                    break;
                case "Remark":
                    Remark = Value.ToString(); ;
                    break;
                case "DocIds":
                    DocIds = Value.ToString(); ;
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_lese_servicescenter());
        }
        #endregion
    }
}
