using pSystem.Interface.Util;
using System;

namespace AwareTec.Polysmith.DataBaseCom
{
    /// <summary>
    /// 评分者
    /// </summary>
    public class Doc_Doctor : ITable
    {
        #region 私有成员
        private int m_ID = 0;
        private bool m_ID_ini = false;
        private string m_UserID = "";
        private bool m_UserID_ini = false;
        private string m_Name = "";
        private bool m_Name_ini = false;
        private string m_LockPsw = "";
        private bool m_LockPsw_ini = false;
        private string m_Age = "";
        private bool m_Age_ini = false;
        private string m_Gender = "";
        private bool m_Gender_ini = false;
        private string m_Post = "";
        private bool m_Post_ini = false;
        private string m_Phone = "";
        private bool m_Phone_ini = false;
        private string m_Department = "";
        private bool m_Department_ini = false;
        private string m_Photo = "";
        private bool m_Photo_ini = false;
        private string m_Comments = "";
        private bool m_Comments_ini = false;
        #endregion
        #region 公有成员
        public int ID
        {
            set
            {
                m_ID = value;
                m_ID_ini = true;
            }
            get
            {
                return m_ID;
            }
        }
        public string UserID
        {
            set
            {
                m_UserID = value;
                m_UserID_ini = true;
            }
            get
            {
                return m_UserID;
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
        public string LockPsw
        {
            set
            {
                m_LockPsw = value;
                m_LockPsw_ini = true;
            }
            get
            {
                return m_LockPsw;
            }
        }
        public string Age
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
        public string Gender
        {
            set
            {
                m_Gender = value;
                m_Gender_ini = true;
            }
            get
            {
                return m_Gender;
            }
        }
        public string Post
        {
            set
            {
                m_Post = value;
                m_Post_ini = true;
            }
            get
            {
                return m_Post;
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
        public string Department
        {
            set
            {
                m_Department = value;
                m_Department_ini = true;
            }
            get
            {
                return m_Department;
            }
        }
        public string Photo
        {
            set
            {
                m_Photo = value;
                m_Photo_ini = true;
            }
            get
            {
                return m_Photo;
            }
        }
        public string Comments
        {
            set
            {
                m_Comments = value;
                m_Comments_ini = true;
            }
            get
            {
                return m_Comments;
            }
        }
        #endregion
        #region 继承成员
        public string GetInsertString()
        {
            string def = " (";
            string val = " VALUES(";
            bool Mulitcase = false;
            //if (this.m_ID_ini)
            //{
            //    def = string.Format("{0}{2}{1}", def, "ID", Mulitcase ? "," : "");
            //    val = string.Format("{0}{2}{1}", val, m_ID, Mulitcase ? "," : "");
            //    Mulitcase = true;
            //}
            if (this.m_UserID_ini)
            {
                def = string.Format("{0}{2}{1}", def, "UserID", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_UserID, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Name_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Name", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Name, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_LockPsw_ini)
            {
                def = string.Format("{0}{2}{1}", def, "LockPsw", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_LockPsw, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Age_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Age", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Age, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Gender_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Gender", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Gender, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Post_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Post", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Post, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Phone_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Phone", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Phone, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Department_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Department", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Department, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Photo_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Photo", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Photo, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Comments_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Comments", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Comments, Mulitcase ? "," : "");
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
            if (m_ID_ini)
            {
                returnstr = string.Format("{0}ID={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ID);
                Mulitcase = true;
            }
            if (m_UserID_ini)
            {
                returnstr = string.Format("{0}UserID='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_UserID);
                Mulitcase = true;
            }
            if (m_Name_ini)
            {
                returnstr = string.Format("{0}Name='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Name);
                Mulitcase = true;
            }
            if (m_LockPsw_ini)
            {
                returnstr = string.Format("{0}LockPsw='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_LockPsw);
                Mulitcase = true;
            }
            if (m_Age_ini)
            {
                returnstr = string.Format("{0}Age='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Age);
                Mulitcase = true;
            }
            if (m_Gender_ini)
            {
                returnstr = string.Format("{0}Gender='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Gender);
                Mulitcase = true;
            }
            if (m_Post_ini)
            {
                returnstr = string.Format("{0}Post='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Post);
                Mulitcase = true;
            }
            if (m_Phone_ini)
            {
                returnstr = string.Format("{0}Phone='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Phone);
                Mulitcase = true;
            }
            if (m_Department_ini)
            {
                returnstr = string.Format("{0}Department='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Department);
                Mulitcase = true;
            }
            if (m_Photo_ini)
            {
                returnstr = string.Format("{0}Photo='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Photo);
                Mulitcase = true;
            }
            if (m_Comments_ini)
            {
                returnstr = string.Format("{0}Comments='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Comments);
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
            if (m_ID_ini)
            {
                def = string.Format("{0}{2}ID={1}", def, m_ID, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_UserID_ini)
            {
                def = string.Format("{0}{2}UserID='{1}'", def, m_UserID, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Name_ini)
            {
                def = string.Format("{0}{2}Name='{1}'", def, m_Name, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_LockPsw_ini)
            {
                def = string.Format("{0}{2}LockPsw='{1}'", def, m_LockPsw, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Age_ini)
            {
                def = string.Format("{0}{2}Age='{1}'", def, m_Age, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Gender_ini)
            {
                def = string.Format("{0}{2}Gender='{1}'", def, m_Gender, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Post_ini)
            {
                def = string.Format("{0}{2}Post='{1}'", def, m_Post, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Phone_ini)
            {
                def = string.Format("{0}{2}Phone='{1}'", def, m_Phone, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Department_ini)
            {
                def = string.Format("{0}{2}Department='{1}'", def, m_Department, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Photo_ini)
            {
                def = string.Format("{0}{2}Photo='{1}'", def, m_Photo, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Comments_ini)
            {
                def = string.Format("{0}{2}Comments='{1}'", def, m_Comments, Mulitcase ? "," : "");
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
            return ("Doctor");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "ID":
                    ID = Convert.ToInt32(Value);
                    break;
                case "UserID":
                    UserID = Value.ToString(); ;
                    break;
                case "Name":
                    Name = Value.ToString(); ;
                    break;
                case "LockPsw":
                    LockPsw = Value.ToString(); ;
                    break;
                case "Age":
                    Age = Value.ToString(); ;
                    break;
                case "Gender":
                    Gender = Value.ToString(); ;
                    break;
                case "Post":
                    Post = Value.ToString(); ;
                    break;
                case "Phone":
                    Phone = Value.ToString(); ;
                    break;
                case "Department":
                    Department = Value.ToString(); ;
                    break;
                case "Photo":
                    Photo = Value.ToString(); ;
                    break;
                case "Comments":
                    Comments = Value.ToString(); ;
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_Doctor());
        }
        #endregion

    }
}
