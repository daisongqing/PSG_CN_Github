using pSystem.Interface.Util;
using System;

namespace Physio.Polysmith.DataInstanceCom
{
    /// <summary>
    /// Edf的进度
    /// </summary>
    public class Doc_lese_edfproccess : ITable
    {
        #region 私有成员
        private int m_Id = 0;
        private bool m_Id_ini = false;
        private string m_OrderNum = "";
        private bool m_OrderNum_ini = false;
        private string m_SourceFile = "";
        private bool m_SourceFile_ini = false;
        private string m_ZipFile = "";
        private bool m_ZipFile_ini = false;
        private string m_EdfPath = "";
        private bool m_EdfPath_ini = false;
        private int m_FileState = 0;
        private bool m_FileState_ini = false;
        private DateTime m_CreateTime = default(DateTime);
        private bool m_CreateTime_ini = false;
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
        public string OrderNum
        {
            set
            {
                m_OrderNum = value;
                m_OrderNum_ini = true;
            }
            get
            {
                return m_OrderNum;
            }
        }
        public string SourceFile
        {
            set
            {
                m_SourceFile = value;
                m_SourceFile_ini = true;
            }
            get
            {
                return m_SourceFile;
            }
        }
        public string ZipFile
        {
            set
            {
                m_ZipFile = value;
                m_ZipFile_ini = true;
            }
            get
            {
                return m_ZipFile;
            }
        }
        public string EdfPath
        {
            set
            {
                m_EdfPath = value;
                m_EdfPath_ini = true;
            }
            get
            {
                return m_EdfPath;
            }
        }
        public int FileState
        {
            set
            {
                m_FileState = value;
                m_FileState_ini = true;
            }
            get
            {
                return m_FileState;
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
            if (this.m_OrderNum_ini)
            {
                def = string.Format("{0}{2}{1}", def, "OrderNum", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_OrderNum, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_SourceFile_ini)
            {
                def = string.Format("{0}{2}{1}", def, "SourceFile", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_SourceFile, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ZipFile_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ZipFile", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_ZipFile, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_EdfPath_ini)
            {
                def = string.Format("{0}{2}{1}", def, "EdfPath", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_EdfPath, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_FileState_ini)
            {
                def = string.Format("{0}{2}{1}", def, "FileState", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_FileState, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_CreateTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "CreateTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_CreateTime, Mulitcase ? "," : "");
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
            if (m_OrderNum_ini)
            {
                returnstr = string.Format("{0}OrderNum='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_OrderNum);
                Mulitcase = true;
            }
            if (m_SourceFile_ini)
            {
                returnstr = string.Format("{0}SourceFile='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_SourceFile);
                Mulitcase = true;
            }
            if (m_ZipFile_ini)
            {
                returnstr = string.Format("{0}ZipFile='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ZipFile);
                Mulitcase = true;
            }
            if (m_EdfPath_ini)
            {
                returnstr = string.Format("{0}EdfPath='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_EdfPath);
                Mulitcase = true;
            }
            if (m_FileState_ini)
            {
                returnstr = string.Format("{0}FileState={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_FileState);
                Mulitcase = true;
            }
            if (m_CreateTime_ini)
            {
                returnstr = string.Format("{0}CreateTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_CreateTime);
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
            if (m_OrderNum_ini)
            {
                def = string.Format("{0}{2}OrderNum='{1}'", def, m_OrderNum, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_SourceFile_ini)
            {
                def = string.Format("{0}{2}SourceFile='{1}'", def, m_SourceFile, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ZipFile_ini)
            {
                def = string.Format("{0}{2}ZipFile='{1}'", def, m_ZipFile, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_EdfPath_ini)
            {
                def = string.Format("{0}{2}EdfPath='{1}'", def, m_EdfPath, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_FileState_ini)
            {
                def = string.Format("{0}{2}FileState={1}", def, m_FileState, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_CreateTime_ini)
            {
                def = string.Format("{0}{2}CreateTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_CreateTime, Mulitcase ? "," : "");
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
            return ("lese_edfproccess");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "Id":
                    Id = Convert.ToInt32(Value);
                    break;
                case "OrderNum":
                    OrderNum = Value.ToString(); ;
                    break;
                case "SourceFile":
                    SourceFile = Value.ToString(); ;
                    break;
                case "ZipFile":
                    ZipFile = Value.ToString(); ;
                    break;
                case "EdfPath":
                    EdfPath = Value.ToString(); ;
                    break;
                case "FileState":
                    FileState = Convert.ToInt32(Value);
                    break;
                case "CreateTime":
                    CreateTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_lese_edfproccess());
        }
        #endregion
    }
}
