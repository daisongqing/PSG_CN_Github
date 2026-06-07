using pSystem.Interface.Util;
using System;

namespace Physio.Polysmith.DataInstanceCom
{
    public class Doc_lese_clientanalysis : ITable
    {
        #region 私有成员
        private int m_Id = 0;
        private bool m_Id_ini = false;
        private string m_DeviceSn = "";
        private bool m_DeviceSn_ini = false;
        private string m_OrderNum = "";
        private bool m_OrderNum_ini = false;
        private string m_ClientId = "";
        private bool m_ClientId_ini = false;
        private int m_DoctorId = 0;
        private bool m_DoctorId_ini = false;
        private DateTime m_OperationTime = default(DateTime);
        private bool m_OperationTime_ini = false;
        private int m_IsOpen = 0;
        private bool m_IsOpen_ini = false;
        private int m_IsAuthorized = 0;
        private bool m_IsAuthorized_ini = false;
        private int m_IsActive = 0;
        private bool m_IsActive_ini = false;
        private int m_FileLoading = 0;
        private bool m_FileLoading_ini = false;
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
        public string DeviceSn
        {
            set
            {
                m_DeviceSn = value;
                m_DeviceSn_ini = true;
            }
            get
            {
                return m_DeviceSn;
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
        public string ClientId
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
        public int DoctorId
        {
            set
            {
                m_DoctorId = value;
                m_DoctorId_ini = true;
            }
            get
            {
                return m_DoctorId;
            }
        }
        public DateTime OperationTime
        {
            set
            {
                m_OperationTime = value;
                m_OperationTime_ini = true;
            }
            get
            {
                return m_OperationTime;
            }
        }
        public int IsOpen
        {
            set
            {
                m_IsOpen = value;
                m_IsOpen_ini = true;
            }
            get
            {
                return m_IsOpen;
            }
        }
        public int IsAuthorized
        {
            set
            {
                m_IsAuthorized = value;
                m_IsAuthorized_ini = true;
            }
            get
            {
                return m_IsAuthorized;
            }
        }
        public int IsActive
        {
            set
            {
                m_IsActive = value;
                m_IsActive_ini = true;
            }
            get
            {
                return m_IsActive;
            }
        }
        public int FileLoading
        {
            set
            {
                m_FileLoading = value;
                m_FileLoading_ini = true;
            }
            get
            {
                return m_FileLoading;
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
            if (this.m_DeviceSn_ini)
            {
                def = string.Format("{0}{2}{1}", def, "DeviceSn", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_DeviceSn, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_OrderNum_ini)
            {
                def = string.Format("{0}{2}{1}", def, "OrderNum", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_OrderNum, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ClientId_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ClientId", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_ClientId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_DoctorId_ini)
            {
                def = string.Format("{0}{2}{1}", def, "DoctorId", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_DoctorId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_OperationTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "OperationTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_OperationTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_IsOpen_ini)
            {
                def = string.Format("{0}{2}{1}", def, "IsOpen", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_IsOpen, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_IsAuthorized_ini)
            {
                def = string.Format("{0}{2}{1}", def, "IsAuthorized", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_IsAuthorized, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_IsActive_ini)
            {
                def = string.Format("{0}{2}{1}", def, "IsActive", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_IsActive, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_FileLoading_ini)
            {
                def = string.Format("{0}{2}{1}", def, "FileLoading", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_FileLoading, Mulitcase ? "," : "");
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
            if (m_DeviceSn_ini)
            {
                returnstr = string.Format("{0}DeviceSn='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_DeviceSn);
                Mulitcase = true;
            }
            if (m_OrderNum_ini)
            {
                returnstr = string.Format("{0}OrderNum='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_OrderNum);
                Mulitcase = true;
            }
            if (m_ClientId_ini)
            {
                returnstr = string.Format("{0}ClientId='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ClientId);
                Mulitcase = true;
            }
            if (m_DoctorId_ini)
            {
                returnstr = string.Format("{0}DoctorId={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_DoctorId);
                Mulitcase = true;
            }
            if (m_OperationTime_ini)
            {
                returnstr = string.Format("{0}OperationTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_OperationTime);
                Mulitcase = true;
            }
            if (m_IsOpen_ini)
            {
                returnstr = string.Format("{0}IsOpen={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_IsOpen);
                Mulitcase = true;
            }
            if (m_IsAuthorized_ini)
            {
                returnstr = string.Format("{0}IsAuthorized={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_IsAuthorized);
                Mulitcase = true;
            }
            if (m_IsActive_ini)
            {
                returnstr = string.Format("{0}IsActive={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_IsActive);
                Mulitcase = true;
            }
            if (m_FileLoading_ini)
            {
                returnstr = string.Format("{0}FileLoading={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_FileLoading);
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
            if (m_DeviceSn_ini)
            {
                def = string.Format("{0}{2}DeviceSn='{1}'", def, m_DeviceSn, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_OrderNum_ini)
            {
                def = string.Format("{0}{2}OrderNum='{1}'", def, m_OrderNum, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ClientId_ini)
            {
                def = string.Format("{0}{2}ClientId='{1}'", def, m_ClientId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_DoctorId_ini)
            {
                def = string.Format("{0}{2}DoctorId={1}", def, m_DoctorId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_OperationTime_ini)
            {
                def = string.Format("{0}{2}OperationTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_OperationTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_IsOpen_ini)
            {
                def = string.Format("{0}{2}IsOpen={1}", def, m_IsOpen, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_IsAuthorized_ini)
            {
                def = string.Format("{0}{2}IsAuthorized={1}", def, m_IsAuthorized, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_IsActive_ini)
            {
                def = string.Format("{0}{2}IsActive={1}", def, m_IsActive, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_FileLoading_ini)
            {
                def = string.Format("{0}{2}FileLoading={1}", def, m_FileLoading, Mulitcase ? "," : "");
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
            return ("lese_clientanalysis");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "Id":
                    Id = Convert.ToInt32(Value);
                    break;
                case "DeviceSn":
                    DeviceSn = Value.ToString(); ;
                    break;
                case "OrderNum":
                    OrderNum = Value.ToString(); ;
                    break;
                case "ClientId":
                    ClientId = Value.ToString(); ;
                    break;
                case "DoctorId":
                    DoctorId = Convert.ToInt32(Value);
                    break;
                case "OperationTime":
                    OperationTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "IsOpen":
                    IsOpen = Convert.ToInt32(Value);
                    break;
                case "IsAuthorized":
                    IsAuthorized = Convert.ToInt32(Value);
                    break;
                case "IsActive":
                    IsActive = Convert.ToInt32(Value);
                    break;
                case "FileLoading":
                    FileLoading = Convert.ToInt32(Value);
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_lese_clientanalysis());
        }
        #endregion
    }
}
