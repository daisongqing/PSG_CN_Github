using pSystem.Interface.Util;
using System;

namespace Physio.Polysmith.DataInstanceCom
{
    public class Doc_lese_clientdown : ITable
    {
        #region 私有成员
        private int m_Id = 0;
        private bool m_Id_ini = false;
        private int m_ClientId = 0;
        private bool m_ClientId_ini = false;
        private string m_OrderId = "";
        private bool m_OrderId_ini = false;
        private int m_Downflag = 0;
        private bool m_Downflag_ini = false;
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
        public int ClientId
        {
            set
            {
                m_ClientId = value;
                m_ClientId_ini = true;
            }
            get
            {
                return m_ClientId;
            }
        }
        public string OrderId
        {
            set
            {
                m_OrderId = value;
                m_OrderId_ini = true;
            }
            get
            {
                return m_OrderId;
            }
        }
        public int Downflag
        {
            set
            {
                m_Downflag = value;
                m_Downflag_ini = true;
            }
            get
            {
                return m_Downflag;
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
            if (this.m_ClientId_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ClientId", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ClientId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_OrderId_ini)
            {
                def = string.Format("{0}{2}{1}", def, "OrderId", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_OrderId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Downflag_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Downflag", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Downflag, Mulitcase ? "," : "");
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
            if (m_ClientId_ini)
            {
                returnstr = string.Format("{0}ClientId={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ClientId);
                Mulitcase = true;
            }
            if (m_OrderId_ini)
            {
                returnstr = string.Format("{0}OrderId='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_OrderId);
                Mulitcase = true;
            }
            if (m_Downflag_ini)
            {
                returnstr = string.Format("{0}Downflag={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Downflag);
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
            if (m_ClientId_ini)
            {
                def = string.Format("{0}{2}ClientId={1}", def, m_ClientId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_OrderId_ini)
            {
                def = string.Format("{0}{2}OrderId='{1}'", def, m_OrderId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Downflag_ini)
            {
                def = string.Format("{0}{2}Downflag={1}", def, m_Downflag, Mulitcase ? "," : "");
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
            return ("lese_clientdown");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "Id":
                    Id = Convert.ToInt32(Value);
                    break;
                case "ClientId":
                    ClientId = Convert.ToInt32(Value);
                    break;
                case "OrderId":
                    OrderId = Value.ToString(); ;
                    break;
                case "Downflag":
                    Downflag = Convert.ToInt32(Value);
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_lese_clientdown());
        }
        #endregion
    }
}
