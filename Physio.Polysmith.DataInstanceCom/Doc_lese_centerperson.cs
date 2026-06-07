using pSystem.Interface.Util;
using System;

namespace Physio.Polysmith.DataInstanceCom
{
    public class Doc_lese_centerperson : ITable
    {
        #region 私有成员
        private int m_Id = 0;
        private bool m_Id_ini = false;
        private int m_ServicesCenterId = 0;
        private bool m_ServicesCenterId_ini = false;
        private string m_Name = "";
        private bool m_Name_ini = false;
        private int m_Sex = 0;
        private bool m_Sex_ini = false;
        private string m_Phone = "";
        private bool m_Phone_ini = false;
        private int m_AccountId = 0;
        private bool m_AccountId_ini = false;
        private DateTime m_CreateTime = default(DateTime);
        private bool m_CreateTime_ini = false;
        private string m_Createor = "";
        private bool m_Createor_ini = false;
        private string m_Pic = "";
        private bool m_Pic_ini = false;
        private string m_Remark = "";
        private bool m_Remark_ini = false;
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
        public int ServicesCenterId
        {
            set
            {
                m_ServicesCenterId = value;
                m_ServicesCenterId_ini = true;
            }
            get
            {
                return m_ServicesCenterId;
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
        public int Sex
        {
            set
            {
                m_Sex = value;
                m_Sex_ini = true;
            }
            get
            {
                return m_Sex;
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
        public int AccountId
        {
            set
            {
                m_AccountId = value;
                m_AccountId_ini = true;
            }
            get
            {
                return m_AccountId;
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
        public string Createor
        {
            set
            {
                m_Createor = value;
                m_Createor_ini = true;
            }
            get
            {
                return m_Createor;
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
            if (this.m_ServicesCenterId_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ServicesCenterId", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ServicesCenterId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Name_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Name", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Name, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Sex_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Sex", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Sex, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Phone_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Phone", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Phone, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_AccountId_ini)
            {
                def = string.Format("{0}{2}{1}", def, "AccountId", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_AccountId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_CreateTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "CreateTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_CreateTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Createor_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Createor", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Createor, Mulitcase ? "," : "");
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
            if (m_ServicesCenterId_ini)
            {
                returnstr = string.Format("{0}ServicesCenterId={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ServicesCenterId);
                Mulitcase = true;
            }
            if (m_Name_ini)
            {
                returnstr = string.Format("{0}Name='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Name);
                Mulitcase = true;
            }
            if (m_Sex_ini)
            {
                returnstr = string.Format("{0}Sex={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Sex);
                Mulitcase = true;
            }
            if (m_Phone_ini)
            {
                returnstr = string.Format("{0}Phone='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Phone);
                Mulitcase = true;
            }
            if (m_AccountId_ini)
            {
                returnstr = string.Format("{0}AccountId={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_AccountId);
                Mulitcase = true;
            }
            if (m_CreateTime_ini)
            {
                returnstr = string.Format("{0}CreateTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_CreateTime);
                Mulitcase = true;
            }
            if (m_Createor_ini)
            {
                returnstr = string.Format("{0}Createor='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Createor);
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
            if (m_ServicesCenterId_ini)
            {
                def = string.Format("{0}{2}ServicesCenterId={1}", def, m_ServicesCenterId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Name_ini)
            {
                def = string.Format("{0}{2}Name='{1}'", def, m_Name, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Sex_ini)
            {
                def = string.Format("{0}{2}Sex={1}", def, m_Sex, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Phone_ini)
            {
                def = string.Format("{0}{2}Phone='{1}'", def, m_Phone, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_AccountId_ini)
            {
                def = string.Format("{0}{2}AccountId={1}", def, m_AccountId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_CreateTime_ini)
            {
                def = string.Format("{0}{2}CreateTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_CreateTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Createor_ini)
            {
                def = string.Format("{0}{2}Createor='{1}'", def, m_Createor, Mulitcase ? "," : "");
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
            if (!Mulitcase)
            {
                return ("");
            }
            return (def);
        }
        public string GetThisTableName()
        {
            return ("lese_centerperson");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "Id":
                    Id = Convert.ToInt32(Value);
                    break;
                case "ServicesCenterId":
                    ServicesCenterId = Convert.ToInt32(Value);
                    break;
                case "Name":
                    Name = Value.ToString(); ;
                    break;
                case "Sex":
                    Sex = Convert.ToInt32(Value);
                    break;
                case "Phone":
                    Phone = Value.ToString(); ;
                    break;
                case "AccountId":
                    AccountId = Convert.ToInt32(Value);
                    break;
                case "CreateTime":
                    CreateTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "Createor":
                    Createor = Value.ToString(); ;
                    break;
                case "Pic":
                    Pic = Value.ToString(); ;
                    break;
                case "Remark":
                    Remark = Value.ToString(); ;
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_lese_centerperson());
        }
        #endregion
    }
}
