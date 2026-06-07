using pSystem.Interface.Util;
using System;

namespace AwareTec.Polysmith.DataBaseCom
{
    /// <summary>
    /// 分析记录总表
    /// </summary>
    public class Doc_MainViewRecord : ITable
    {
        #region 私有成员
        private int m_ID = 0;
        private bool m_ID_ini = false;
        private string m_PatientID = "";
        private bool m_PatientID_ini = false;
        private string m_DoctorID = "";
        private bool m_DoctorID_ini = false;
        private DateTime m_RecordTime = default(DateTime);
        private bool m_RecordTime_ini = false;
        private int m_Progress = 0;
        private bool m_Progress_ini = false;
        private bool m_ReportReady = false;
        private bool m_ReportReady_ini = false;
        private string m_GUID = "";
        private bool m_GUID_ini = false;
        private string m_EdfPath = "";
        private bool m_EdfPath_ini = false;
        private int m_FrameCount = 0;
        private bool m_FrameCount_ini = false;
        private string m_MatchKey = "";
        private bool m_MatchKey_ini = false;
        private int m_LoginID = 0;
        private bool m_LoginID_ini = false;
        private string m_ReviewMontageName = "";
        private bool m_ReviewMontageName_ini = false;
        private DateTime m_CreatTime = default(DateTime);
        private bool m_CreatTime_ini = false;
        private DateTime m_StartRecordTime = default(DateTime);
        private bool m_StartRecordTime_ini = false;
        private DateTime m_EndRecordTime = default(DateTime);
        private bool m_EndRecordTime_ini = false;
        private DateTime m_LightOffTime = default(DateTime);
        private bool m_LightOffTime_ini = false;
        private DateTime m_LightOnTime = default(DateTime);
        private bool m_LightOnTime_ini = false;
        private int m_LastAnalysisType = 0;
        private bool m_LastAnalysisType_ini = false;
        private string m_LastOutputReportTemplate = "";
        private bool m_LastOutputReportTemplate_ini = false;
        private string m_Reserve1 = "";
        private bool m_Reserve1_ini = false;
        private string m_Reserve2 = "";
        private bool m_Reserve2_ini = false;
        private string m_Reserve3 = "";
        private bool m_Reserve3_ini = false;
        private string m_Reserve4 = "";
        private bool m_Reserve4_ini = false;
        private string m_Reserve5 = "";
        private bool m_Reserve5_ini = false;
        private int m_StatusWord = 0;
        private bool m_StatusWord_ini = false;
        private int m_ModeType = 0;
        private bool m_ModeType_ini = false;
        private bool m_VideoHave = false;
        private bool m_VideoHave_ini = false;
        private int m_DifferentVersion = 0;
        private bool m_DifferentVersion_ini = false;
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
        public string PatientID
        {
            set
            {
                m_PatientID = value;
                m_PatientID_ini = true;
            }
            get
            {
                return m_PatientID;
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
        public DateTime RecordTime
        {
            set
            {
                m_RecordTime = value;
                m_RecordTime_ini = true;
            }
            get
            {
                return m_RecordTime;
            }
        }
        public int Progress
        {
            set
            {
                m_Progress = value;
                m_Progress_ini = true;
            }
            get
            {
                return m_Progress;
            }
        }
        public bool ReportReady
        {
            set
            {
                m_ReportReady = value;
                m_ReportReady_ini = true;
            }
            get
            {
                return m_ReportReady;
            }
        }
        public string GUID
        {
            set
            {
                m_GUID = value;
                m_GUID_ini = true;
            }
            get
            {
                return m_GUID;
            }
        }
        public string EdfPath
        {
            set
            {
                m_EdfPath = value.Replace(AppDomain.CurrentDomain.BaseDirectory, "...\\");
                m_EdfPath_ini = true;
            }
            get
            {
                return m_EdfPath.Replace("...\\", AppDomain.CurrentDomain.BaseDirectory);
            }
        }
        public int FrameCount
        {
            set
            {
                m_FrameCount = value;
                m_FrameCount_ini = true;
            }
            get
            {
                return m_FrameCount;
            }
        }
        public string MatchKey
        {
            set
            {
                m_MatchKey = value;
                m_MatchKey_ini = true;
            }
            get
            {
                return m_MatchKey;
            }
        }
        public int LoginID
        {
            set
            {
                m_LoginID = value;
                m_LoginID_ini = true;
            }
            get
            {
                return m_LoginID;
            }
        }
        public string ReviewMontageName
        {
            set
            {
                m_ReviewMontageName = value;
                m_ReviewMontageName_ini = true;
            }
            get
            {
                return m_ReviewMontageName;
            }
        }
        public DateTime CreatTime
        {
            set
            {
                m_CreatTime = value;
                m_CreatTime_ini = true;
            }
            get
            {
                return m_CreatTime;
            }
        }
        public DateTime StartRecordTime
        {
            set
            {
                m_StartRecordTime = value;
                m_StartRecordTime_ini = true;
            }
            get
            {
                return m_StartRecordTime;
            }
        }
        public DateTime EndRecordTime
        {
            set
            {
                m_EndRecordTime = value;
                m_EndRecordTime_ini = true;
            }
            get
            {
                return m_EndRecordTime;
            }
        }
        public DateTime LightOffTime
        {
            set
            {
                m_LightOffTime = value;
                m_LightOffTime_ini = true;
            }
            get
            {
                return m_LightOffTime;
            }
        }
        public DateTime LightOnTime
        {
            set
            {
                m_LightOnTime = value;
                m_LightOnTime_ini = true;
            }
            get
            {
                return m_LightOnTime;
            }
        }
        public int LastAnalysisType
        {
            set
            {
                m_LastAnalysisType = value;
                m_LastAnalysisType_ini = true;
            }
            get
            {
                return m_LastAnalysisType;
            }
        }
        public string LastOutputReportTemplate
        {
            set
            {
                m_LastOutputReportTemplate = value;
                m_LastOutputReportTemplate_ini = true;
            }
            get
            {
                return m_LastOutputReportTemplate;
            }
        }
        /// <summary>
        /// 用作了存储分析的选项字符
        /// </summary>
        public string Reserve1
        {
            set
            {
                m_Reserve1 = value;
                m_Reserve1_ini = true;
            }
            get
            {
                return m_Reserve1;
            }
        }
        /// <summary>
        /// 用作存储设备的SN号
        /// </summary>
        public string Reserve2
        {
            set
            {
                m_Reserve2 = value;
                m_Reserve2_ini = true;
            }
            get
            {
                return m_Reserve2;
            }
        }
        public string Reserve3
        {
            set
            {
                m_Reserve3 = value;
                m_Reserve3_ini = true;
            }
            get
            {
                return m_Reserve3;
            }
        }
        public string Reserve4
        {
            set
            {
                m_Reserve4 = value;
                m_Reserve4_ini = true;
            }
            get
            {
                return m_Reserve4;
            }
        }
        public string Reserve5
        {
            set
            {
                m_Reserve5 = value;
                m_Reserve5_ini = true;
            }
            get
            {
                return m_Reserve5;
            }
        }
        public int StatusWord
        {
            set
            {
                m_StatusWord = value;
                m_StatusWord_ini = true;
            }
            get
            {
                return m_StatusWord;
            }
        }
        public int ModeType
        {
            set
            {
                m_ModeType = value;
                m_ModeType_ini = true;
            }
            get
            {
                return m_ModeType;
            }
        }
        public bool VideoHave
        {
            set
            {
                m_VideoHave = value;
                m_VideoHave_ini = true;
            }
            get
            {
                return m_VideoHave;
            }
        }
        public int DifferentVersion
        {
            set
            {
                m_DifferentVersion = value;
                m_DifferentVersion_ini = true;
            }
            get
            {
                return m_DifferentVersion;
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
            if (this.m_PatientID_ini)
            {
                def = string.Format("{0}{2}{1}", def, "PatientID", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_PatientID, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_DoctorID_ini)
            {
                def = string.Format("{0}{2}{1}", def, "DoctorID", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_DoctorID, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_RecordTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "RecordTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_RecordTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Progress_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Progress", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Progress, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ReportReady_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ReportReady", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ReportReady ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_GUID_ini)
            {
                def = string.Format("{0}{2}{1}", def, "GUID", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_GUID, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_EdfPath_ini)
            {
                def = string.Format("{0}{2}{1}", def, "EdfPath", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_EdfPath, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_FrameCount_ini)
            {
                def = string.Format("{0}{2}{1}", def, "FrameCount", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_FrameCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MatchKey_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MatchKey", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_MatchKey, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_LoginID_ini)
            {
                def = string.Format("{0}{2}{1}", def, "LoginID", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_LoginID, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ReviewMontageName_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ReviewMontageName", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_ReviewMontageName, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_CreatTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "CreatTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_CreatTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_StartRecordTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "StartRecordTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_StartRecordTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_EndRecordTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "EndRecordTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_EndRecordTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_LightOffTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "LightOffTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_LightOffTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_LightOnTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "LightOnTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_LightOnTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_LastAnalysisType_ini)
            {
                def = string.Format("{0}{2}{1}", def, "LastAnalysisType", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_LastAnalysisType, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_LastOutputReportTemplate_ini)
            {
                def = string.Format("{0}{2}{1}", def, "LastOutputReportTemplate", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_LastOutputReportTemplate, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Reserve1_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Reserve1", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Reserve1, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Reserve2_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Reserve2", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Reserve2, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Reserve3_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Reserve3", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Reserve3, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Reserve4_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Reserve4", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Reserve4, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Reserve5_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Reserve5", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Reserve5, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_StatusWord_ini)
            {
                def = string.Format("{0}{2}{1}", def, "StatusWord", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_StatusWord, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ModeType_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ModeType", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ModeType, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_VideoHave_ini)
            {
                def = string.Format("{0}{2}{1}", def, "VideoHave", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_VideoHave ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_DifferentVersion_ini)
            {
                def = string.Format("{0}{2}{1}", def, "DifferentVersion", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_DifferentVersion, Mulitcase ? "," : "");
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
            if (m_PatientID_ini)
            {
                returnstr = string.Format("{0}PatientID='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_PatientID);
                Mulitcase = true;
            }
            if (m_DoctorID_ini)
            {
                returnstr = string.Format("{0}DoctorID='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_DoctorID);
                Mulitcase = true;
            }
            if (m_RecordTime_ini)
            {
                returnstr = string.Format("{0}RecordTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_RecordTime);
                Mulitcase = true;
            }
            if (m_Progress_ini)
            {
                returnstr = string.Format("{0}Progress={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Progress);
                Mulitcase = true;
            }
            if (m_ReportReady_ini)
            {
                returnstr = string.Format("{0}ReportReady={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ReportReady ? 1 : 0);
                Mulitcase = true;
            }
            if (m_GUID_ini)
            {
                returnstr = string.Format("{0}GUID='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_GUID);
                Mulitcase = true;
            }
            if (m_EdfPath_ini)
            {
                returnstr = string.Format("{0}EdfPath='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_EdfPath);
                Mulitcase = true;
            }
            if (m_FrameCount_ini)
            {
                returnstr = string.Format("{0}FrameCount={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_FrameCount);
                Mulitcase = true;
            }
            if (m_MatchKey_ini)
            {
                returnstr = string.Format("{0}MatchKey='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MatchKey);
                Mulitcase = true;
            }
            if (m_LoginID_ini)
            {
                returnstr = string.Format("{0}LoginID={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_LoginID);
                Mulitcase = true;
            }
            if (m_ReviewMontageName_ini)
            {
                returnstr = string.Format("{0}ReviewMontageName='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ReviewMontageName);
                Mulitcase = true;
            }
            if (m_CreatTime_ini)
            {
                returnstr = string.Format("{0}CreatTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_CreatTime);
                Mulitcase = true;
            }
            if (m_StartRecordTime_ini)
            {
                returnstr = string.Format("{0}StartRecordTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_StartRecordTime);
                Mulitcase = true;
            }
            if (m_EndRecordTime_ini)
            {
                returnstr = string.Format("{0}EndRecordTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_EndRecordTime);
                Mulitcase = true;
            }
            if (m_LightOffTime_ini)
            {
                returnstr = string.Format("{0}LightOffTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_LightOffTime);
                Mulitcase = true;
            }
            if (m_LightOnTime_ini)
            {
                returnstr = string.Format("{0}LightOnTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_LightOnTime);
                Mulitcase = true;
            }
            if (m_LastAnalysisType_ini)
            {
                returnstr = string.Format("{0}LastAnalysisType={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_LastAnalysisType);
                Mulitcase = true;
            }
            if (m_LastOutputReportTemplate_ini)
            {
                returnstr = string.Format("{0}LastOutputReportTemplate='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_LastOutputReportTemplate);
                Mulitcase = true;
            }
            if (m_Reserve1_ini)
            {
                returnstr = string.Format("{0}Reserve1='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Reserve1);
                Mulitcase = true;
            }
            if (m_Reserve2_ini)
            {
                returnstr = string.Format("{0}Reserve2='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Reserve2);
                Mulitcase = true;
            }
            if (m_Reserve3_ini)
            {
                returnstr = string.Format("{0}Reserve3='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Reserve3);
                Mulitcase = true;
            }
            if (m_Reserve4_ini)
            {
                returnstr = string.Format("{0}Reserve4='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Reserve4);
                Mulitcase = true;
            }
            if (m_Reserve5_ini)
            {
                returnstr = string.Format("{0}Reserve5='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Reserve5);
                Mulitcase = true;
            }
            if (m_StatusWord_ini)
            {
                returnstr = string.Format("{0}StatusWord={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_StatusWord);
                Mulitcase = true;
            }
            if (m_ModeType_ini)
            {
                returnstr = string.Format("{0}ModeType={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ModeType);
                Mulitcase = true;
            }
            if (m_VideoHave_ini)
            {
                returnstr = string.Format("{0}VideoHave={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_VideoHave ? 1 : 0);
                Mulitcase = true;
            }
            if (m_DifferentVersion_ini)
            {
                returnstr = string.Format("{0}DifferentVersion={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_DifferentVersion);
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
            if (m_PatientID_ini)
            {
                def = string.Format("{0}{2}PatientID='{1}'", def, m_PatientID, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_DoctorID_ini)
            {
                def = string.Format("{0}{2}DoctorID='{1}'", def, m_DoctorID, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_RecordTime_ini)
            {
                def = string.Format("{0}{2}RecordTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_RecordTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Progress_ini)
            {
                def = string.Format("{0}{2}Progress={1}", def, m_Progress, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ReportReady_ini)
            {
                def = string.Format("{0}{2}ReportReady={1}", def, m_ReportReady ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_GUID_ini)
            {
                def = string.Format("{0}{2}GUID='{1}'", def, m_GUID, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_EdfPath_ini)
            {
                def = string.Format("{0}{2}EdfPath='{1}'", def, m_EdfPath, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_FrameCount_ini)
            {
                def = string.Format("{0}{2}FrameCount={1}", def, m_FrameCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MatchKey_ini)
            {
                def = string.Format("{0}{2}MatchKey='{1}'", def, m_MatchKey, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_LoginID_ini)
            {
                def = string.Format("{0}{2}LoginID={1}", def, m_LoginID, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ReviewMontageName_ini)
            {
                def = string.Format("{0}{2}ReviewMontageName='{1}'", def, m_ReviewMontageName, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_CreatTime_ini)
            {
                def = string.Format("{0}{2}CreatTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_CreatTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_StartRecordTime_ini)
            {
                def = string.Format("{0}{2}StartRecordTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_StartRecordTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_EndRecordTime_ini)
            {
                def = string.Format("{0}{2}EndRecordTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_EndRecordTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_LightOffTime_ini)
            {
                def = string.Format("{0}{2}LightOffTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_LightOffTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_LightOnTime_ini)
            {
                def = string.Format("{0}{2}LightOnTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_LightOnTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_LastAnalysisType_ini)
            {
                def = string.Format("{0}{2}LastAnalysisType={1}", def, m_LastAnalysisType, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_LastOutputReportTemplate_ini)
            {
                def = string.Format("{0}{2}LastOutputReportTemplate='{1}'", def, m_LastOutputReportTemplate, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Reserve1_ini)
            {
                def = string.Format("{0}{2}Reserve1='{1}'", def, m_Reserve1, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Reserve2_ini)
            {
                def = string.Format("{0}{2}Reserve2='{1}'", def, m_Reserve2, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Reserve3_ini)
            {
                def = string.Format("{0}{2}Reserve3='{1}'", def, m_Reserve3, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Reserve4_ini)
            {
                def = string.Format("{0}{2}Reserve4='{1}'", def, m_Reserve4, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Reserve5_ini)
            {
                def = string.Format("{0}{2}Reserve5='{1}'", def, m_Reserve5, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_StatusWord_ini)
            {
                def = string.Format("{0}{2}StatusWord={1}", def, m_StatusWord, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ModeType_ini)
            {
                def = string.Format("{0}{2}ModeType={1}", def, m_ModeType, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_VideoHave_ini)
            {
                def = string.Format("{0}{2}VideoHave={1}", def, m_VideoHave ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_DifferentVersion_ini)
            {
                def = string.Format("{0}{2}DifferentVersion={1}", def, m_DifferentVersion, Mulitcase ? "," : "");
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
            return ("MainViewRecord");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "ID":
                    ID = Convert.ToInt32(Value);
                    break;
                case "PatientID":
                    PatientID = Value.ToString(); ;
                    break;
                case "DoctorID":
                    DoctorID = Value.ToString(); ;
                    break;
                case "RecordTime":
                    RecordTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "Progress":
                    Progress = Convert.ToInt32(Value);
                    break;
                case "ReportReady":
                    ReportReady = Convert.ToBoolean(Value);
                    break;
                case "GUID":
                    GUID = Value.ToString(); ;
                    break;
                case "EdfPath":
                    EdfPath = Value.ToString(); ;
                    break;
                case "FrameCount":
                    FrameCount = Convert.ToInt32(Value);
                    break;
                case "MatchKey":
                    MatchKey = Value.ToString(); ;
                    break;
                case "LoginID":
                    LoginID = Convert.ToInt32(Value);
                    break;
                case "ReviewMontageName":
                    ReviewMontageName = Value.ToString(); ;
                    break;
                case "CreatTime":
                    CreatTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "StartRecordTime":
                    StartRecordTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "EndRecordTime":
                    EndRecordTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "LightOffTime":
                    LightOffTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "LightOnTime":
                    LightOnTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "LastAnalysisType":
                    LastAnalysisType = Convert.ToInt32(Value);
                    break;
                case "LastOutputReportTemplate":
                    LastOutputReportTemplate = Value.ToString(); ;
                    break;
                case "Reserve1":
                    Reserve1 = Value.ToString(); ;
                    break;
                case "Reserve2":
                    Reserve2 = Value.ToString(); ;
                    break;
                case "Reserve3":
                    Reserve3 = Value.ToString(); ;
                    break;
                case "Reserve4":
                    Reserve4 = Value.ToString(); ;
                    break;
                case "Reserve5":
                    Reserve5 = Value.ToString(); ;
                    break;
                case "StatusWord":
                    StatusWord = Convert.ToInt32(Value);
                    break;
                case "ModeType":
                    ModeType = Convert.ToInt32(Value);
                    break;
                case "VideoHave":
                    VideoHave = Convert.ToBoolean(Value);
                    break;
                case "DifferentVersion":
                    DifferentVersion = Convert.ToInt32(Value);
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_MainViewRecord());
        }
        #endregion 
    }
}
