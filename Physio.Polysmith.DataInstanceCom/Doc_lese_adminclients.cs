using pSystem.Interface.Util;
using System;

namespace Physio.Polysmith.DataInstanceCom
{
    /// <summary>
    /// 客户端路径配置信息
    /// </summary>
    public class Doc_lese_adminclients : ITable
    {
        #region 私有成员
        private int m_Id = 0;
        private bool m_Id_ini = false;
        private int m_AccId = 0;
        private bool m_AccId_ini = false;
        private string m_ClientMac = "";
        private bool m_ClientMac_ini = false;
        private string m_EdfPath = "";
        private bool m_EdfPath_ini = false;
        private string m_ReportPath = "";
        private bool m_ReportPath_ini = false;
        private string m_VideoPath = "";
        private bool m_VideoPath_ini = false;
        private string m_EdfUPPath = "";
        private bool m_EdfUPPath_ini = false;
        private string m_EdfLoadPath = "";
        private bool m_EdfLoadPath_ini = false;
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
        public int AccId
        {
            set
            {
                m_AccId = value;
                m_AccId_ini = true;
            }
            get
            {
                return m_AccId;
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
                if (value != null)
                    m_EdfPath = value.Replace(AppDomain.CurrentDomain.BaseDirectory, "...\\");
                m_EdfPath_ini = true;
            }
            get
            {
                return m_EdfPath.Replace("...\\", AppDomain.CurrentDomain.BaseDirectory);
            }
        }
        public string ReportPath
        {
            set
            {
                if (value != null)
                    m_ReportPath = value.Replace(AppDomain.CurrentDomain.BaseDirectory, "...\\");
                m_ReportPath_ini = true;
            }
            get
            {
                return m_ReportPath.Replace("...\\", AppDomain.CurrentDomain.BaseDirectory);
            }
        }
        public string VideoPath
        {
            set
            {
                if (value != null)
                    m_VideoPath = value.Replace(AppDomain.CurrentDomain.BaseDirectory, "...\\");
                m_VideoPath_ini = true;
            }
            get
            {
                return m_VideoPath.Replace("...\\", AppDomain.CurrentDomain.BaseDirectory);
            }
        }
        public string EdfUPPath
        {
            set
            {
                if (value != null)
                    m_EdfUPPath = value.Replace(AppDomain.CurrentDomain.BaseDirectory, "...\\");
                m_EdfUPPath_ini = true;
            }
            get
            {
                return m_EdfUPPath.Replace("...\\", AppDomain.CurrentDomain.BaseDirectory);
            }
        }
        public string EdfLoadPath
        {
            set
            {
                if (value != null)
                    m_EdfLoadPath = value.Replace(AppDomain.CurrentDomain.BaseDirectory, "...\\");
                m_EdfLoadPath_ini = true;
            }
            get
            {
                return m_EdfLoadPath.Replace("...\\", AppDomain.CurrentDomain.BaseDirectory);
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
            if (this.m_AccId_ini)
            {
                def = string.Format("{0}{2}{1}", def, "AccId", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_AccId, Mulitcase ? "," : "");
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
            if (this.m_ReportPath_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ReportPath", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_ReportPath, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_VideoPath_ini)
            {
                def = string.Format("{0}{2}{1}", def, "VideoPath", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_VideoPath, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_EdfUPPath_ini)
            {
                def = string.Format("{0}{2}{1}", def, "EdfUPPath", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_EdfUPPath, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_EdfLoadPath_ini)
            {
                def = string.Format("{0}{2}{1}", def, "EdfLoadPath", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_EdfLoadPath, Mulitcase ? "," : "");
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
            if (m_AccId_ini)
            {
                returnstr = string.Format("{0}AccId={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_AccId);
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
            if (m_ReportPath_ini)
            {
                returnstr = string.Format("{0}ReportPath='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ReportPath);
                Mulitcase = true;
            }
            if (m_VideoPath_ini)
            {
                returnstr = string.Format("{0}VideoPath='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_VideoPath);
                Mulitcase = true;
            }
            if (m_EdfUPPath_ini)
            {
                returnstr = string.Format("{0}EdfUPPath='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_EdfUPPath);
                Mulitcase = true;
            }
            if (m_EdfLoadPath_ini)
            {
                returnstr = string.Format("{0}EdfLoadPath='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_EdfLoadPath);
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
            if (m_AccId_ini)
            {
                def = string.Format("{0}{2}AccId={1}", def, m_AccId, Mulitcase ? "," : "");
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
            if (m_ReportPath_ini)
            {
                def = string.Format("{0}{2}ReportPath='{1}'", def, m_ReportPath, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_VideoPath_ini)
            {
                def = string.Format("{0}{2}VideoPath='{1}'", def, m_VideoPath, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_EdfUPPath_ini)
            {
                def = string.Format("{0}{2}EdfUPPath='{1}'", def, m_EdfUPPath, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_EdfLoadPath_ini)
            {
                def = string.Format("{0}{2}EdfLoadPath='{1}'", def, m_EdfLoadPath, Mulitcase ? "," : "");
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
            return ("lese_adminclients");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "Id":
                    Id = Convert.ToInt32(Value);
                    break;
                case "AccId":
                    AccId = Convert.ToInt32(Value);
                    break;
                case "ClientMac":
                    ClientMac = Value.ToString(); ;
                    break;
                case "EdfPath":
                    EdfPath = Value.ToString(); ;
                    break;
                case "ReportPath":
                    ReportPath = Value.ToString(); ;
                    break;
                case "VideoPath":
                    VideoPath = Value.ToString(); ;
                    break;
                case "EdfUPPath":
                    EdfUPPath = Value.ToString(); ;
                    break;
                case "EdfLoadPath":
                    EdfLoadPath = Value.ToString(); ;
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_lese_adminclients());
        }
        #endregion
    }
}
