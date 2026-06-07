using pSystem.Interface.Util;
using System;

namespace Physio.Polysmith.DataInstanceCom
{
    public class Doc_lese_userpatient : ITable
    {
        #region 私有成员
        private int m_Id = 0;
        private bool m_Id_ini = false;
        private string m_CaseNumber = "";
        private bool m_CaseNumber_ini = false;
        private string m_Name = "";
        private bool m_Name_ini = false;
        private string m_Sex = "";
        private bool m_Sex_ini = false;
        private int m_Age = 0;
        private bool m_Age_ini = false;
        private float m_Height = 0;
        private bool m_Height_ini = false;
        private float m_Weight = 0;
        private bool m_Weight_ini = false;
        private DateTime m_Birthday = default(DateTime);
        private bool m_Birthday_ini = false;
        private string m_Address = "";
        private bool m_Address_ini = false;
        private string m_Mobile = "";
        private bool m_Mobile_ini = false;
        private DateTime m_CreateTime = default(DateTime);
        private bool m_CreateTime_ini = false;
        private int m_SyncFlag = 0;
        private bool m_SyncFlag_ini = false;
        private DateTime m_SyncTime = default(DateTime);
        private bool m_SyncTime_ini = false;
        private string m_SyncOrg = "";
        private bool m_SyncOrg_ini = false;
        private string m_OpenId = "";
        private bool m_OpenId_ini = false;
        private string m_NickName = "";
        private bool m_NickName_ini = false;
        private string m_HeadImgUrl = "";
        private bool m_HeadImgUrl_ini = false;
        private float m_BMI = 0;
        private bool m_BMI_ini = false;
        private string m_CardId = "";
        private bool m_CardId_ini = false;
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
        public string CaseNumber
        {
            set
            {
                m_CaseNumber = value;
                m_CaseNumber_ini = true;
            }
            get
            {
                return m_CaseNumber;
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
        public string Sex
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
        public int Age
        {
            set
            {
                m_Age = value;
                m_Age_ini = true;
            }
            get
            {
                return m_Age;
            }
        }
        public float Height
        {
            set
            {
                m_Height = value;
                m_Height_ini = true;
            }
            get
            {
                return m_Height;
            }
        }
        public float Weight
        {
            set
            {
                m_Weight = value;
                m_Weight_ini = true;
            }
            get
            {
                return m_Weight;
            }
        }
        public DateTime Birthday
        {
            set
            {
                m_Birthday = value;
                m_Birthday_ini = true;
            }
            get
            {
                return m_Birthday;
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
        public string Mobile
        {
            set
            {
                m_Mobile = value;
                m_Mobile_ini = true;
            }
            get
            {
                return m_Mobile;
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
        public int SyncFlag
        {
            set
            {
                m_SyncFlag = value;
                m_SyncFlag_ini = true;
            }
            get
            {
                return m_SyncFlag;
            }
        }
        public DateTime SyncTime
        {
            set
            {
                m_SyncTime = value;
                m_SyncTime_ini = true;
            }
            get
            {
                return m_SyncTime;
            }
        }
        public string SyncOrg
        {
            set
            {
                m_SyncOrg = value;
                m_SyncOrg_ini = true;
            }
            get
            {
                return m_SyncOrg;
            }
        }
        public string OpenId
        {
            set
            {
                m_OpenId = value;
                m_OpenId_ini = true;
            }
            get
            {
                return m_OpenId;
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
        public string HeadImgUrl
        {
            set
            {
                m_HeadImgUrl = value;
                m_HeadImgUrl_ini = true;
            }
            get
            {
                return m_HeadImgUrl;
            }
        }
        public float BMI
        {
            set
            {
                m_BMI = value;
                m_BMI_ini = true;
            }
            get
            {
                return m_BMI;
            }
        }
        public string CardId
        {
            set
            {
                m_CardId = value;
                m_CardId_ini = true;
            }
            get
            {
                return m_CardId;
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
            if (this.m_CaseNumber_ini)
            {
                def = string.Format("{0}{2}{1}", def, "CaseNumber", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_CaseNumber, Mulitcase ? "," : "");
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
                val = string.Format("{0}{2}'{1}'", val, m_Sex, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Age_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Age", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Age, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Height_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Height", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Height, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Weight_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Weight", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Weight, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Birthday_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Birthday", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_Birthday, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Address_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Address", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Address, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Mobile_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Mobile", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Mobile, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_CreateTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "CreateTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_CreateTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_SyncFlag_ini)
            {
                def = string.Format("{0}{2}{1}", def, "SyncFlag", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_SyncFlag, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_SyncTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "SyncTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_SyncTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_SyncOrg_ini)
            {
                def = string.Format("{0}{2}{1}", def, "SyncOrg", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_SyncOrg, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_OpenId_ini)
            {
                def = string.Format("{0}{2}{1}", def, "OpenId", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_OpenId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_NickName_ini)
            {
                def = string.Format("{0}{2}{1}", def, "NickName", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_NickName, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_HeadImgUrl_ini)
            {
                def = string.Format("{0}{2}{1}", def, "HeadImgUrl", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_HeadImgUrl, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_BMI_ini)
            {
                def = string.Format("{0}{2}{1}", def, "BMI", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_BMI, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_CardId_ini)
            {
                def = string.Format("{0}{2}{1}", def, "CardId", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_CardId, Mulitcase ? "," : "");
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
            if (m_CaseNumber_ini)
            {
                returnstr = string.Format("{0}CaseNumber='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_CaseNumber);
                Mulitcase = true;
            }
            if (m_Name_ini)
            {
                returnstr = string.Format("{0}Name='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Name);
                Mulitcase = true;
            }
            if (m_Sex_ini)
            {
                returnstr = string.Format("{0}Sex='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Sex);
                Mulitcase = true;
            }
            if (m_Age_ini)
            {
                returnstr = string.Format("{0}Age={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Age);
                Mulitcase = true;
            }
            if (m_Height_ini)
            {
                returnstr = string.Format("{0}Height={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Height);
                Mulitcase = true;
            }
            if (m_Weight_ini)
            {
                returnstr = string.Format("{0}Weight={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Weight);
                Mulitcase = true;
            }
            if (m_Birthday_ini)
            {
                returnstr = string.Format("{0}Birthday='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Birthday);
                Mulitcase = true;
            }
            if (m_Address_ini)
            {
                returnstr = string.Format("{0}Address='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Address);
                Mulitcase = true;
            }
            if (m_Mobile_ini)
            {
                returnstr = string.Format("{0}Mobile='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Mobile);
                Mulitcase = true;
            }
            if (m_CreateTime_ini)
            {
                returnstr = string.Format("{0}CreateTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_CreateTime);
                Mulitcase = true;
            }
            if (m_SyncFlag_ini)
            {
                returnstr = string.Format("{0}SyncFlag={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_SyncFlag);
                Mulitcase = true;
            }
            if (m_SyncTime_ini)
            {
                returnstr = string.Format("{0}SyncTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_SyncTime);
                Mulitcase = true;
            }
            if (m_SyncOrg_ini)
            {
                returnstr = string.Format("{0}SyncOrg='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_SyncOrg);
                Mulitcase = true;
            }
            if (m_OpenId_ini)
            {
                returnstr = string.Format("{0}OpenId='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_OpenId);
                Mulitcase = true;
            }
            if (m_NickName_ini)
            {
                returnstr = string.Format("{0}NickName='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_NickName);
                Mulitcase = true;
            }
            if (m_HeadImgUrl_ini)
            {
                returnstr = string.Format("{0}HeadImgUrl='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_HeadImgUrl);
                Mulitcase = true;
            }
            if (m_BMI_ini)
            {
                returnstr = string.Format("{0}BMI={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_BMI);
                Mulitcase = true;
            }
            if (m_CardId_ini)
            {
                returnstr = string.Format("{0}CardId='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_CardId);
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
            if (m_CaseNumber_ini)
            {
                def = string.Format("{0}{2}CaseNumber='{1}'", def, m_CaseNumber, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Name_ini)
            {
                def = string.Format("{0}{2}Name='{1}'", def, m_Name, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Sex_ini)
            {
                def = string.Format("{0}{2}Sex='{1}'", def, m_Sex, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Age_ini)
            {
                def = string.Format("{0}{2}Age={1}", def, m_Age, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Height_ini)
            {
                def = string.Format("{0}{2}Height={1}", def, m_Height, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Weight_ini)
            {
                def = string.Format("{0}{2}Weight={1}", def, m_Weight, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Birthday_ini)
            {
                def = string.Format("{0}{2}Birthday='{1:yyyy-MM-dd HH:mm:ss}'", def, m_Birthday, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Address_ini)
            {
                def = string.Format("{0}{2}Address='{1}'", def, m_Address, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Mobile_ini)
            {
                def = string.Format("{0}{2}Mobile='{1}'", def, m_Mobile, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_CreateTime_ini)
            {
                def = string.Format("{0}{2}CreateTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_CreateTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_SyncFlag_ini)
            {
                def = string.Format("{0}{2}SyncFlag={1}", def, m_SyncFlag, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_SyncTime_ini)
            {
                def = string.Format("{0}{2}SyncTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_SyncTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_SyncOrg_ini)
            {
                def = string.Format("{0}{2}SyncOrg='{1}'", def, m_SyncOrg, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_OpenId_ini)
            {
                def = string.Format("{0}{2}OpenId='{1}'", def, m_OpenId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_NickName_ini)
            {
                def = string.Format("{0}{2}NickName='{1}'", def, m_NickName, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_HeadImgUrl_ini)
            {
                def = string.Format("{0}{2}HeadImgUrl='{1}'", def, m_HeadImgUrl, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_BMI_ini)
            {
                def = string.Format("{0}{2}BMI={1}", def, m_BMI, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_CardId_ini)
            {
                def = string.Format("{0}{2}CardId='{1}'", def, m_CardId, Mulitcase ? "," : "");
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
            return ("lese_userpatient");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "Id":
                    Id = Convert.ToInt32(Value);
                    break;
                case "CaseNumber":
                    CaseNumber = Value.ToString(); ;
                    break;
                case "Name":
                    Name = Value.ToString(); ;
                    break;
                case "Sex":
                    Sex = Value.ToString(); ;
                    break;
                case "Age":
                    Age = Convert.ToInt32(Value);
                    break;
                case "Height":
                    Height = Convert.ToSingle(Value);
                    break;
                case "Weight":
                    Weight = Convert.ToSingle(Value);
                    break;
                case "Birthday":
                    Birthday = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "Address":
                    Address = Value.ToString(); ;
                    break;
                case "Mobile":
                    Mobile = Value.ToString(); ;
                    break;
                case "CreateTime":
                    CreateTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "SyncFlag":
                    SyncFlag = Convert.ToInt32(Value);
                    break;
                case "SyncTime":
                    SyncTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "SyncOrg":
                    SyncOrg = Value.ToString(); ;
                    break;
                case "OpenId":
                    OpenId = Value.ToString(); ;
                    break;
                case "NickName":
                    NickName = Value.ToString(); ;
                    break;
                case "HeadImgUrl":
                    HeadImgUrl = Value.ToString(); ;
                    break;
                case "BMI":
                    BMI = Convert.ToSingle(Value);
                    break;
                case "CardId":
                    CardId = Value.ToString(); ;
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_lese_userpatient());
        }
        #endregion
    }
}
