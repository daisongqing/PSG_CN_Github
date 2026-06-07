using pSystem.Interface.Util;
using System;

namespace AwareTec.Polysmith.DataBaseCom
{
    public class Doc_EventType : ITable
    {
        #region 私有成员
        private int m_ID = 0;
        private bool m_ID_ini = false;
        private string m_Name = "";
        private bool m_Name_ini = false;
        private string m_Description = "";
        private bool m_Description_ini = false;
        private int m_Flag = 0;
        private bool m_Flag_ini = false;
        private string m_BackColor = "";
        private bool m_BackColor_ini = false;
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
        public int Flag
        {
            set
            {
                m_Flag = value;
                m_Flag_ini = true;
            }
            get
            {
                return m_Flag;
            }
        }
        public string BackColor
        {
            set
            {
                m_BackColor = value;
                m_BackColor_ini = true;
            }
            get
            {
                return m_BackColor;
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
            if (this.m_Flag_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Flag", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Flag, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_BackColor_ini)
            {
                def = string.Format("{0}{2}{1}", def, "BackColor", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_BackColor, Mulitcase ? "," : "");
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
            if (m_Flag_ini)
            {
                returnstr = string.Format("{0}Flag={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Flag);
                Mulitcase = true;
            }
            if (m_BackColor_ini)
            {
                returnstr = string.Format("{0}BackColor='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_BackColor);
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
            if (m_Flag_ini)
            {
                def = string.Format("{0}{2}Flag={1}", def, m_Flag, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_BackColor_ini)
            {
                def = string.Format("{0}{2}BackColor='{1}'", def, m_BackColor, Mulitcase ? "," : "");
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
            return ("EventType");
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
                case "Flag":
                    Flag = Convert.ToInt32(Value);
                    break;
                case "BackColor":
                    BackColor = Value.ToString(); ;
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_EventType());
        }
        #endregion

    }
}
