using pSystem.Interface.Util;
using System;

namespace AwareTec.Polysmith.DataBaseCom
{
    /// <summary>
    /// 定标
    /// </summary>
    public class Doc_CalibrationDefine : ITable
    {
        #region 私有成员
        private int m_ID = 0;
        private bool m_ID_ini = false;
        private string m_Name = "";
        private bool m_Name_ini = false;
        private string m_Description = "";
        private bool m_Description_ini = false;
        private int m_XH = 0;
        private bool m_XH_ini = false;
        private string m_ChannelID = "";
        private bool m_ChannelID_ini = false;
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
        public string Description
        {
            set
            {
                m_Description = value;
                m_Description_ini = true;
            }
            get
            {
                return m_Description;
            }
        }
        public int XH
        {
            set
            {
                m_XH = value;
                m_XH_ini = true;
            }
            get
            {
                return m_XH;
            }
        }
        public string ChannelID
        {
            set
            {
                m_ChannelID = value;
                m_ChannelID_ini = true;
            }
            get
            {
                return m_ChannelID;
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
            if (this.m_Description_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Description", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Description, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_XH_ini)
            {
                def = string.Format("{0}{2}{1}", def, "XH", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_XH, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ChannelID_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ChannelID", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_ChannelID, Mulitcase ? "," : "");
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
            if (m_Description_ini)
            {
                returnstr = string.Format("{0}Description='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Description);
                Mulitcase = true;
            }
            if (m_XH_ini)
            {
                returnstr = string.Format("{0}XH={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_XH);
                Mulitcase = true;
            }
            if (m_ChannelID_ini)
            {
                returnstr = string.Format("{0}ChannelID='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ChannelID);
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
            if (m_Description_ini)
            {
                def = string.Format("{0}{2}Description='{1}'", def, m_Description, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_XH_ini)
            {
                def = string.Format("{0}{2}XH={1}", def, m_XH, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ChannelID_ini)
            {
                def = string.Format("{0}{2}ChannelID='{1}'", def, m_ChannelID, Mulitcase ? "," : "");
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
            return ("CalibrationDefine");
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
                case "Description":
                    Description = Value.ToString(); ;
                    break;
                case "XH":
                    XH = Convert.ToInt32(Value);
                    break;
                case "ChannelID":
                    ChannelID = Value.ToString(); ;
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_CalibrationDefine());
        }
        #endregion
    }
}
