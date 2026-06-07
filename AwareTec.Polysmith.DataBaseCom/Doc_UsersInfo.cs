using pSystem.Interface.Util;
using System;

namespace AwareTec.Polysmith.DataBaseCom
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class Doc_UsersInfo : ITable
    {
        #region 私有成员
        private int m_ID = 0;
        private bool m_ID_ini = false;
        private string m_Name = "";
        private bool m_Name_ini = false;
        private string m_PassWord = "";
        private bool m_PassWord_ini = false;
        private int m_Level = 0;
        private bool m_Level_ini = false;
        private string m_DoctorID = "";
        private bool m_DoctorID_ini = false;
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
        public string PassWord
        {
            set
            {
                m_PassWord = value;
                m_PassWord_ini = true;
            }
            get
            {
                return m_PassWord;
            }
        }
        public int Level
        {
            set
            {
                m_Level = value;
                m_Level_ini = true;
            }
            get
            {
                return m_Level;
            }
        }
        public string DoctorID
        {
            set
            {
                m_DoctorID = value;
                m_DoctorID_ini = true;
            }
            get
            {
                return m_DoctorID;
            }
        }
        #endregion
        #region 继承成员
        public string GetInsertString()
        {
            string def = " (";
            string val = " VALUES(";
            bool Mulitcase = false;
            if (this.m_ID_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ID", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ID, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Name_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Name", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Name, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_PassWord_ini)
            {
                def = string.Format("{0}{2}{1}", def, "PassWord", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_PassWord, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Level_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Level", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Level, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_DoctorID_ini)
            {
                def = string.Format("{0}{2}{1}", def, "DoctorID", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_DoctorID, Mulitcase ? "," : "");
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
            if (m_Name_ini)
            {
                returnstr = string.Format("{0}Name='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Name);
                Mulitcase = true;
            }
            if (m_PassWord_ini)
            {
                returnstr = string.Format("{0}PassWord='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_PassWord);
                Mulitcase = true;
            }
            if (m_Level_ini)
            {
                returnstr = string.Format("{0}Level={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Level);
                Mulitcase = true;
            }
            if (m_DoctorID_ini)
            {
                returnstr = string.Format("{0}DoctorID='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_DoctorID);
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
            if (m_Name_ini)
            {
                def = string.Format("{0}{2}Name='{1}'", def, m_Name, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_PassWord_ini)
            {
                def = string.Format("{0}{2}PassWord='{1}'", def, m_PassWord, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Level_ini)
            {
                def = string.Format("{0}{2}Level={1}", def, m_Level, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_DoctorID_ini)
            {
                def = string.Format("{0}{2}DoctorID='{1}'", def, m_DoctorID, Mulitcase ? "," : "");
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
            return ("UsersInfo");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "ID":
                    ID = Convert.ToInt32(Value);
                    break;
                case "Name":
                    Name = Value.ToString(); ;
                    break;
                case "PassWord":
                    PassWord = Value.ToString(); ;
                    break;
                case "Level":
                    Level = Convert.ToInt32(Value);
                    break;
                case "DoctorID":
                    DoctorID = Value.ToString(); ;
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_UsersInfo());
        }
        #endregion
    }
}
