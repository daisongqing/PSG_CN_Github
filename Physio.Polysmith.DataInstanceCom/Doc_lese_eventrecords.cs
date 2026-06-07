using pSystem.Interface.Util;
using System;

namespace Physio.Polysmith.DataInstanceCom
{
    public class Doc_lese_eventrecords : ITable
    {
        #region 私有成员
        private int m_Id = 0;
        private bool m_Id_ini = false;
        private string m_EventID = "";
        private bool m_EventID_ini = false;
        private int m_EventType = 0;
        private bool m_EventType_ini = false;
        private string m_EventName = "";
        private bool m_EventName_ini = false;
        private int m_ChannelID = 0;
        private bool m_ChannelID_ini = false;
        private DateTime m_StartTime = default(DateTime);
        private bool m_StartTime_ini = false;
        private DateTime m_EndTime = default(DateTime);
        private bool m_EndTime_ini = false;
        private int m_StartIndex = 0;
        private bool m_StartIndex_ini = false;
        private int m_EndIndex = 0;
        private bool m_EndIndex_ini = false;
        private string m_Description = "";
        private bool m_Description_ini = false;
        private string m_PaintOrderNum = "";
        private bool m_PaintOrderNum_ini = false;
        private string m_Tag = "";
        private bool m_Tag_ini = false;
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
        public string EventID
        {
            set
            {
                m_EventID = value;
                m_EventID_ini = true;
            }
            get
            {
                return m_EventID;
            }
        }
        public int EventType
        {
            set
            {
                m_EventType = value;
                m_EventType_ini = true;
            }
            get
            {
                return m_EventType;
            }
        }
        public string EventName
        {
            set
            {
                m_EventName = value;
                m_EventName_ini = true;
            }
            get
            {
                return m_EventName;
            }
        }
        public int ChannelID
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
        public DateTime StartTime
        {
            set
            {
                m_StartTime = value;
                m_StartTime_ini = true;
            }
            get
            {
                return m_StartTime;
            }
        }
        public DateTime EndTime
        {
            set
            {
                m_EndTime = value;
                m_EndTime_ini = true;
            }
            get
            {
                return m_EndTime;
            }
        }
        public int StartIndex
        {
            set
            {
                m_StartIndex = value;
                m_StartIndex_ini = true;
            }
            get
            {
                return m_StartIndex;
            }
        }
        public int EndIndex
        {
            set
            {
                m_EndIndex = value;
                m_EndIndex_ini = true;
            }
            get
            {
                return m_EndIndex;
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
        public string PaintOrderNum
        {
            set
            {
                m_PaintOrderNum = value;
                m_PaintOrderNum_ini = true;
            }
            get
            {
                return m_PaintOrderNum;
            }
        }
        public string Tag
        {
            set
            {
                m_Tag = value;
                m_Tag_ini = true;
            }
            get
            {
                return m_Tag;
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
            if (this.m_EventID_ini)
            {
                def = string.Format("{0}{2}{1}", def, "EventID", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_EventID, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_EventType_ini)
            {
                def = string.Format("{0}{2}{1}", def, "EventType", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_EventType, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_EventName_ini)
            {
                def = string.Format("{0}{2}{1}", def, "EventName", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_EventName, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ChannelID_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ChannelID", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ChannelID, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_StartTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "StartTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_StartTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_EndTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "EndTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_EndTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_StartIndex_ini)
            {
                def = string.Format("{0}{2}{1}", def, "StartIndex", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_StartIndex, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_EndIndex_ini)
            {
                def = string.Format("{0}{2}{1}", def, "EndIndex", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_EndIndex, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Description_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Description", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Description, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_PaintOrderNum_ini)
            {
                def = string.Format("{0}{2}{1}", def, "PaintOrderNum", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_PaintOrderNum, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Tag_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Tag", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Tag, Mulitcase ? "," : "");
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
            if (m_EventID_ini)
            {
                returnstr = string.Format("{0}EventID='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_EventID);
                Mulitcase = true;
            }
            if (m_EventType_ini)
            {
                returnstr = string.Format("{0}EventType={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_EventType);
                Mulitcase = true;
            }
            if (m_EventName_ini)
            {
                returnstr = string.Format("{0}EventName='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_EventName);
                Mulitcase = true;
            }
            if (m_ChannelID_ini)
            {
                returnstr = string.Format("{0}ChannelID={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ChannelID);
                Mulitcase = true;
            }
            if (m_StartTime_ini)
            {
                returnstr = string.Format("{0}StartTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_StartTime);
                Mulitcase = true;
            }
            if (m_EndTime_ini)
            {
                returnstr = string.Format("{0}EndTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_EndTime);
                Mulitcase = true;
            }
            if (m_StartIndex_ini)
            {
                returnstr = string.Format("{0}StartIndex={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_StartIndex);
                Mulitcase = true;
            }
            if (m_EndIndex_ini)
            {
                returnstr = string.Format("{0}EndIndex={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_EndIndex);
                Mulitcase = true;
            }
            if (m_Description_ini)
            {
                returnstr = string.Format("{0}Description='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Description);
                Mulitcase = true;
            }
            if (m_PaintOrderNum_ini)
            {
                returnstr = string.Format("{0}PaintOrderNum='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_PaintOrderNum);
                Mulitcase = true;
            }
            if (m_Tag_ini)
            {
                returnstr = string.Format("{0}Tag='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Tag);
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
            if (m_EventID_ini)
            {
                def = string.Format("{0}{2}EventID='{1}'", def, m_EventID, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_EventType_ini)
            {
                def = string.Format("{0}{2}EventType={1}", def, m_EventType, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_EventName_ini)
            {
                def = string.Format("{0}{2}EventName='{1}'", def, m_EventName, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ChannelID_ini)
            {
                def = string.Format("{0}{2}ChannelID={1}", def, m_ChannelID, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_StartTime_ini)
            {
                def = string.Format("{0}{2}StartTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_StartTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_EndTime_ini)
            {
                def = string.Format("{0}{2}EndTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_EndTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_StartIndex_ini)
            {
                def = string.Format("{0}{2}StartIndex={1}", def, m_StartIndex, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_EndIndex_ini)
            {
                def = string.Format("{0}{2}EndIndex={1}", def, m_EndIndex, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Description_ini)
            {
                def = string.Format("{0}{2}Description='{1}'", def, m_Description, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_PaintOrderNum_ini)
            {
                def = string.Format("{0}{2}PaintOrderNum='{1}'", def, m_PaintOrderNum, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Tag_ini)
            {
                def = string.Format("{0}{2}Tag='{1}'", def, m_Tag, Mulitcase ? "," : "");
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
            return ("lese_eventrecords");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "Id":
                    Id = Convert.ToInt32(Value);
                    break;
                case "EventID":
                    EventID = Value.ToString(); ;
                    break;
                case "EventType":
                    EventType = Convert.ToInt32(Value);
                    break;
                case "EventName":
                    EventName = Value.ToString(); ;
                    break;
                case "ChannelID":
                    ChannelID = Convert.ToInt32(Value);
                    break;
                case "StartTime":
                    StartTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "EndTime":
                    EndTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "StartIndex":
                    StartIndex = Convert.ToInt32(Value);
                    break;
                case "EndIndex":
                    EndIndex = Convert.ToInt32(Value);
                    break;
                case "Description":
                    Description = Value.ToString(); ;
                    break;
                case "PaintOrderNum":
                    PaintOrderNum = Value.ToString(); ;
                    break;
                case "Tag":
                    Tag = Value.ToString(); ;
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_lese_eventrecords());
        }
        #endregion
    }
}
