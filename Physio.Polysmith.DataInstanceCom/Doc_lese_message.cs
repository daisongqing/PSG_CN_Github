using pSystem.Interface.Util;
using System;

namespace Physio.Polysmith.DataInstanceCom
{
    /// <summary>
    /// 消息表
    /// </summary>
    public class Doc_lese_message : ITable
    {
        #region 私有成员
        private int m_Id = 0;
        private bool m_Id_ini = false;
        private string m_Title = "";
        private bool m_Title_ini = false;
        private string m_Comment = "";
        private bool m_Comment_ini = false;
        private int m_MsgType = 0;
        private bool m_MsgType_ini = false;
        private int m_Status = 0;
        private bool m_Status_ini = false;
        private int m_FromId = 0;
        private bool m_FromId_ini = false;
        private int m_ReceiverId = 0;
        private bool m_ReceiverId_ini = false;
        private DateTime m_CreateTime = default(DateTime);
        private bool m_CreateTime_ini = false;
        private DateTime m_UpdateTime = default(DateTime);
        private bool m_UpdateTime_ini = false;
        private DateTime m_DeleteTime = default(DateTime);
        private bool m_DeleteTime_ini = false;
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
        public string Title
        {
            set
            {
                m_Title = value;
                m_Title_ini = true;
            }
            get
            {
                return m_Title;
            }
        }
        public string Comment
        {
            set
            {
                m_Comment = value;
                m_Comment_ini = true;
            }
            get
            {
                return m_Comment;
            }
        }
        public int MsgType
        {
            set
            {
                m_MsgType = value;
                m_MsgType_ini = true;
            }
            get
            {
                return m_MsgType;
            }
        }
        public int Status
        {
            set
            {
                m_Status = value;
                m_Status_ini = true;
            }
            get
            {
                return m_Status;
            }
        }
        public int FromId
        {
            set
            {
                m_FromId = value;
                m_FromId_ini = true;
            }
            get
            {
                return m_FromId;
            }
        }
        public int ReceiverId
        {
            set
            {
                m_ReceiverId = value;
                m_ReceiverId_ini = true;
            }
            get
            {
                return m_ReceiverId;
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
        public DateTime UpdateTime
        {
            set
            {
                m_UpdateTime = value;
                m_UpdateTime_ini = true;
            }
            get
            {
                return m_UpdateTime;
            }
        }
        public DateTime DeleteTime
        {
            set
            {
                m_DeleteTime = value;
                m_DeleteTime_ini = true;
            }
            get
            {
                return m_DeleteTime;
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
            if (this.m_Title_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Title", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Title, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Comment_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Comment", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Comment, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MsgType_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MsgType", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_MsgType, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Status_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Status", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Status, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_FromId_ini)
            {
                def = string.Format("{0}{2}{1}", def, "FromId", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_FromId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ReceiverId_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ReceiverId", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ReceiverId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_CreateTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "CreateTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_CreateTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_UpdateTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "UpdateTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_UpdateTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_DeleteTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "DeleteTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_DeleteTime, Mulitcase ? "," : "");
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
            if (m_Title_ini)
            {
                returnstr = string.Format("{0}Title='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Title);
                Mulitcase = true;
            }
            if (m_Comment_ini)
            {
                returnstr = string.Format("{0}Comment='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Comment);
                Mulitcase = true;
            }
            if (m_MsgType_ini)
            {
                returnstr = string.Format("{0}MsgType={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MsgType);
                Mulitcase = true;
            }
            if (m_Status_ini)
            {
                returnstr = string.Format("{0}Status={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Status);
                Mulitcase = true;
            }
            if (m_FromId_ini)
            {
                returnstr = string.Format("{0}FromId={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_FromId);
                Mulitcase = true;
            }
            if (m_ReceiverId_ini)
            {
                returnstr = string.Format("{0}ReceiverId={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ReceiverId);
                Mulitcase = true;
            }
            if (m_CreateTime_ini)
            {
                returnstr = string.Format("{0}CreateTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_CreateTime);
                Mulitcase = true;
            }
            if (m_UpdateTime_ini)
            {
                returnstr = string.Format("{0}UpdateTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_UpdateTime);
                Mulitcase = true;
            }
            if (m_DeleteTime_ini)
            {
                returnstr = string.Format("{0}DeleteTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_DeleteTime);
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
            if (m_Title_ini)
            {
                def = string.Format("{0}{2}Title='{1}'", def, m_Title, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Comment_ini)
            {
                def = string.Format("{0}{2}Comment='{1}'", def, m_Comment, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MsgType_ini)
            {
                def = string.Format("{0}{2}MsgType={1}", def, m_MsgType, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Status_ini)
            {
                def = string.Format("{0}{2}Status={1}", def, m_Status, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_FromId_ini)
            {
                def = string.Format("{0}{2}FromId={1}", def, m_FromId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ReceiverId_ini)
            {
                def = string.Format("{0}{2}ReceiverId={1}", def, m_ReceiverId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_CreateTime_ini)
            {
                def = string.Format("{0}{2}CreateTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_CreateTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_UpdateTime_ini)
            {
                def = string.Format("{0}{2}UpdateTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_UpdateTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_DeleteTime_ini)
            {
                def = string.Format("{0}{2}DeleteTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_DeleteTime, Mulitcase ? "," : "");
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
            return ("lese_message");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "Id":
                    Id = Convert.ToInt32(Value);
                    break;
                case "Title":
                    Title = Value.ToString(); ;
                    break;
                case "Comment":
                    Comment = Value.ToString(); ;
                    break;
                case "MsgType":
                    MsgType = Convert.ToInt32(Value);
                    break;
                case "Status":
                    Status = Convert.ToInt32(Value);
                    break;
                case "FromId":
                    FromId = Convert.ToInt32(Value);
                    break;
                case "ReceiverId":
                    ReceiverId = Convert.ToInt32(Value);
                    break;
                case "CreateTime":
                    CreateTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "UpdateTime":
                    UpdateTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "DeleteTime":
                    DeleteTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_lese_message());
        }
        #endregion
    }
}
