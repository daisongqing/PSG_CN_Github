using pSystem.Interface.Util;
using System;

namespace Physio.Polysmith.DataInstanceCom
{
    public class Doc_lese_systemsetting : ITable
    {
        #region 私有成员
        private int m_ID = 0;
        private bool m_ID_ini = false;
        private string m_SaveEdfPath = "";
        private bool m_SaveEdfPath_ini = false;
        private string m_ReportPath = "";
        private bool m_ReportPath_ini = false;
        private string m_ReportTemplate = "";
        private bool m_ReportTemplate_ini = false;
        private string m_ReportTemplate2 = "";
        private bool m_ReportTemplate2_ini = false;
        private string m_ReportType = "";
        private bool m_ReportType_ini = false;
        private bool m_AutoRunReport = false;
        private bool m_AutoRunReport_ini = false;
        private int m_PlaySpanTime = 0;
        private bool m_PlaySpanTime_ini = false;
        private bool m_AllowRemoteAnalysis = false;
        private bool m_AllowRemoteAnalysis_ini = false;
        private string m_RemoteServerAddr = "";
        private bool m_RemoteServerAddr_ini = false;
        private string m_Password = "";
        private bool m_Password_ini = false;
        private string m_VedioSourceUrl = "";
        private bool m_VedioSourceUrl_ini = false;
        private string m_VedioSavePath = "";
        private bool m_VedioSavePath_ini = false;
        private bool m_UseBirthdayEnable = false;
        private bool m_UseBirthdayEnable_ini = false;
        private bool m_AnalysisBeforeReport = false;
        private bool m_AnalysisBeforeReport_ini = false;
        private bool m_ReportChartCanSelected = false;
        private bool m_ReportChartCanSelected_ini = false;
        private string m_OxygenReduceRange = "";
        private bool m_OxygenReduceRange_ini = false;
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
        private string m_Reserve6 = "";
        private bool m_Reserve6_ini = false;
        private int m_StatusWord = 0;
        private bool m_StatusWord_ini = false;
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
        public string SaveEdfPath
        {
            set
            {
                m_SaveEdfPath = value;
                m_SaveEdfPath_ini = true;
            }
            get
            {
                return m_SaveEdfPath;
            }
        }
        public string ReportPath
        {
            set
            {
                m_ReportPath = value;
                m_ReportPath_ini = true;
            }
            get
            {
                return m_ReportPath;
            }
        }
        public string ReportTemplate
        {
            set
            {
                m_ReportTemplate = value;
                m_ReportTemplate_ini = true;
            }
            get
            {
                return m_ReportTemplate;
            }
        }
        public string ReportTemplate2
        {
            set
            {
                m_ReportTemplate2 = value;
                m_ReportTemplate2_ini = true;
            }
            get
            {
                return m_ReportTemplate2;
            }
        }
        public string ReportType
        {
            set
            {
                m_ReportType = value;
                m_ReportType_ini = true;
            }
            get
            {
                return m_ReportType;
            }
        }
        public bool AutoRunReport
        {
            set
            {
                m_AutoRunReport = value;
                m_AutoRunReport_ini = true;
            }
            get
            {
                return m_AutoRunReport;
            }
        }
        public int PlaySpanTime
        {
            set
            {
                m_PlaySpanTime = value;
                m_PlaySpanTime_ini = true;
            }
            get
            {
                return m_PlaySpanTime;
            }
        }
        public bool AllowRemoteAnalysis
        {
            set
            {
                m_AllowRemoteAnalysis = value;
                m_AllowRemoteAnalysis_ini = true;
            }
            get
            {
                return m_AllowRemoteAnalysis;
            }
        }
        public string RemoteServerAddr
        {
            set
            {
                m_RemoteServerAddr = value;
                m_RemoteServerAddr_ini = true;
            }
            get
            {
                return m_RemoteServerAddr;
            }
        }
        public string Password
        {
            set
            {
                m_Password = value;
                m_Password_ini = true;
            }
            get
            {
                return m_Password;
            }
        }
        public string VedioSourceUrl
        {
            set
            {
                m_VedioSourceUrl = value;
                m_VedioSourceUrl_ini = true;
            }
            get
            {
                return m_VedioSourceUrl;
            }
        }
        public string VedioSavePath
        {
            set
            {
                m_VedioSavePath = value;
                m_VedioSavePath_ini = true;
            }
            get
            {
                return m_VedioSavePath;
            }
        }
        public bool UseBirthdayEnable
        {
            set
            {
                m_UseBirthdayEnable = value;
                m_UseBirthdayEnable_ini = true;
            }
            get
            {
                return m_UseBirthdayEnable;
            }
        }
        public bool AnalysisBeforeReport
        {
            set
            {
                m_AnalysisBeforeReport = value;
                m_AnalysisBeforeReport_ini = true;
            }
            get
            {
                return m_AnalysisBeforeReport;
            }
        }
        public bool ReportChartCanSelected
        {
            set
            {
                m_ReportChartCanSelected = value;
                m_ReportChartCanSelected_ini = true;
            }
            get
            {
                return m_ReportChartCanSelected;
            }
        }
        public string OxygenReduceRange
        {
            set
            {
                m_OxygenReduceRange = value;
                m_OxygenReduceRange_ini = true;
            }
            get
            {
                return m_OxygenReduceRange;
            }
        }
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
        public string Reserve6
        {
            set
            {
                m_Reserve6 = value;
                m_Reserve6_ini = true;
            }
            get
            {
                return m_Reserve6;
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
            if (this.m_SaveEdfPath_ini)
            {
                def = string.Format("{0}{2}{1}", def, "SaveEdfPath", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_SaveEdfPath, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ReportPath_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ReportPath", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_ReportPath, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ReportTemplate_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ReportTemplate", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_ReportTemplate, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ReportTemplate2_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ReportTemplate2", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_ReportTemplate2, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ReportType_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ReportType", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_ReportType, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_AutoRunReport_ini)
            {
                def = string.Format("{0}{2}{1}", def, "AutoRunReport", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_AutoRunReport ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_PlaySpanTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "PlaySpanTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_PlaySpanTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_AllowRemoteAnalysis_ini)
            {
                def = string.Format("{0}{2}{1}", def, "AllowRemoteAnalysis", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_AllowRemoteAnalysis ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_RemoteServerAddr_ini)
            {
                def = string.Format("{0}{2}{1}", def, "RemoteServerAddr", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_RemoteServerAddr, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Password_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Password", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Password, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_VedioSourceUrl_ini)
            {
                def = string.Format("{0}{2}{1}", def, "VedioSourceUrl", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_VedioSourceUrl, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_VedioSavePath_ini)
            {
                def = string.Format("{0}{2}{1}", def, "VedioSavePath", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_VedioSavePath, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_UseBirthdayEnable_ini)
            {
                def = string.Format("{0}{2}{1}", def, "UseBirthdayEnable", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_UseBirthdayEnable ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_AnalysisBeforeReport_ini)
            {
                def = string.Format("{0}{2}{1}", def, "AnalysisBeforeReport", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_AnalysisBeforeReport ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ReportChartCanSelected_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ReportChartCanSelected", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ReportChartCanSelected ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_OxygenReduceRange_ini)
            {
                def = string.Format("{0}{2}{1}", def, "OxygenReduceRange", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_OxygenReduceRange, Mulitcase ? "," : "");
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
            if (this.m_Reserve6_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Reserve6", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Reserve6, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_StatusWord_ini)
            {
                def = string.Format("{0}{2}{1}", def, "StatusWord", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_StatusWord, Mulitcase ? "," : "");
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
            if (m_SaveEdfPath_ini)
            {
                returnstr = string.Format("{0}SaveEdfPath='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_SaveEdfPath);
                Mulitcase = true;
            }
            if (m_ReportPath_ini)
            {
                returnstr = string.Format("{0}ReportPath='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ReportPath);
                Mulitcase = true;
            }
            if (m_ReportTemplate_ini)
            {
                returnstr = string.Format("{0}ReportTemplate='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ReportTemplate);
                Mulitcase = true;
            }
            if (m_ReportTemplate2_ini)
            {
                returnstr = string.Format("{0}ReportTemplate2='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ReportTemplate2);
                Mulitcase = true;
            }
            if (m_ReportType_ini)
            {
                returnstr = string.Format("{0}ReportType='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ReportType);
                Mulitcase = true;
            }
            if (m_AutoRunReport_ini)
            {
                returnstr = string.Format("{0}AutoRunReport={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_AutoRunReport ? 1 : 0);
                Mulitcase = true;
            }
            if (m_PlaySpanTime_ini)
            {
                returnstr = string.Format("{0}PlaySpanTime={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_PlaySpanTime);
                Mulitcase = true;
            }
            if (m_AllowRemoteAnalysis_ini)
            {
                returnstr = string.Format("{0}AllowRemoteAnalysis={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_AllowRemoteAnalysis ? 1 : 0);
                Mulitcase = true;
            }
            if (m_RemoteServerAddr_ini)
            {
                returnstr = string.Format("{0}RemoteServerAddr='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_RemoteServerAddr);
                Mulitcase = true;
            }
            if (m_Password_ini)
            {
                returnstr = string.Format("{0}Password='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Password);
                Mulitcase = true;
            }
            if (m_VedioSourceUrl_ini)
            {
                returnstr = string.Format("{0}VedioSourceUrl='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_VedioSourceUrl);
                Mulitcase = true;
            }
            if (m_VedioSavePath_ini)
            {
                returnstr = string.Format("{0}VedioSavePath='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_VedioSavePath);
                Mulitcase = true;
            }
            if (m_UseBirthdayEnable_ini)
            {
                returnstr = string.Format("{0}UseBirthdayEnable={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_UseBirthdayEnable ? 1 : 0);
                Mulitcase = true;
            }
            if (m_AnalysisBeforeReport_ini)
            {
                returnstr = string.Format("{0}AnalysisBeforeReport={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_AnalysisBeforeReport ? 1 : 0);
                Mulitcase = true;
            }
            if (m_ReportChartCanSelected_ini)
            {
                returnstr = string.Format("{0}ReportChartCanSelected={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ReportChartCanSelected ? 1 : 0);
                Mulitcase = true;
            }
            if (m_OxygenReduceRange_ini)
            {
                returnstr = string.Format("{0}OxygenReduceRange='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_OxygenReduceRange);
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
            if (m_Reserve6_ini)
            {
                returnstr = string.Format("{0}Reserve6='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Reserve6);
                Mulitcase = true;
            }
            if (m_StatusWord_ini)
            {
                returnstr = string.Format("{0}StatusWord={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_StatusWord);
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
            if (m_SaveEdfPath_ini)
            {
                def = string.Format("{0}{2}SaveEdfPath='{1}'", def, m_SaveEdfPath, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ReportPath_ini)
            {
                def = string.Format("{0}{2}ReportPath='{1}'", def, m_ReportPath, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ReportTemplate_ini)
            {
                def = string.Format("{0}{2}ReportTemplate='{1}'", def, m_ReportTemplate, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ReportTemplate2_ini)
            {
                def = string.Format("{0}{2}ReportTemplate2='{1}'", def, m_ReportTemplate2, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ReportType_ini)
            {
                def = string.Format("{0}{2}ReportType='{1}'", def, m_ReportType, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_AutoRunReport_ini)
            {
                def = string.Format("{0}{2}AutoRunReport={1}", def, m_AutoRunReport ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_PlaySpanTime_ini)
            {
                def = string.Format("{0}{2}PlaySpanTime={1}", def, m_PlaySpanTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_AllowRemoteAnalysis_ini)
            {
                def = string.Format("{0}{2}AllowRemoteAnalysis={1}", def, m_AllowRemoteAnalysis ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_RemoteServerAddr_ini)
            {
                def = string.Format("{0}{2}RemoteServerAddr='{1}'", def, m_RemoteServerAddr, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Password_ini)
            {
                def = string.Format("{0}{2}Password='{1}'", def, m_Password, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_VedioSourceUrl_ini)
            {
                def = string.Format("{0}{2}VedioSourceUrl='{1}'", def, m_VedioSourceUrl, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_VedioSavePath_ini)
            {
                def = string.Format("{0}{2}VedioSavePath='{1}'", def, m_VedioSavePath, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_UseBirthdayEnable_ini)
            {
                def = string.Format("{0}{2}UseBirthdayEnable={1}", def, m_UseBirthdayEnable ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_AnalysisBeforeReport_ini)
            {
                def = string.Format("{0}{2}AnalysisBeforeReport={1}", def, m_AnalysisBeforeReport ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ReportChartCanSelected_ini)
            {
                def = string.Format("{0}{2}ReportChartCanSelected={1}", def, m_ReportChartCanSelected ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_OxygenReduceRange_ini)
            {
                def = string.Format("{0}{2}OxygenReduceRange='{1}'", def, m_OxygenReduceRange, Mulitcase ? "," : "");
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
            if (m_Reserve6_ini)
            {
                def = string.Format("{0}{2}Reserve6='{1}'", def, m_Reserve6, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_StatusWord_ini)
            {
                def = string.Format("{0}{2}StatusWord={1}", def, m_StatusWord, Mulitcase ? "," : "");
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
            return ("lese_systemsetting");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "ID":
                    ID = Convert.ToInt32(Value);
                    break;
                case "SaveEdfPath":
                    SaveEdfPath = Value.ToString(); ;
                    break;
                case "ReportPath":
                    ReportPath = Value.ToString(); ;
                    break;
                case "ReportTemplate":
                    ReportTemplate = Value.ToString(); ;
                    break;
                case "ReportTemplate2":
                    ReportTemplate2 = Value.ToString(); ;
                    break;
                case "ReportType":
                    ReportType = Value.ToString(); ;
                    break;
                case "AutoRunReport":
                    AutoRunReport = Convert.ToBoolean(Value);
                    break;
                case "PlaySpanTime":
                    PlaySpanTime = Convert.ToInt32(Value);
                    break;
                case "AllowRemoteAnalysis":
                    AllowRemoteAnalysis = Convert.ToBoolean(Value);
                    break;
                case "RemoteServerAddr":
                    RemoteServerAddr = Value.ToString(); ;
                    break;
                case "Password":
                    Password = Value.ToString(); ;
                    break;
                case "VedioSourceUrl":
                    VedioSourceUrl = Value.ToString(); ;
                    break;
                case "VedioSavePath":
                    VedioSavePath = Value.ToString(); ;
                    break;
                case "UseBirthdayEnable":
                    UseBirthdayEnable = Convert.ToBoolean(Value);
                    break;
                case "AnalysisBeforeReport":
                    AnalysisBeforeReport = Convert.ToBoolean(Value);
                    break;
                case "ReportChartCanSelected":
                    ReportChartCanSelected = Convert.ToBoolean(Value);
                    break;
                case "OxygenReduceRange":
                    OxygenReduceRange = Value.ToString(); ;
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
                case "Reserve6":
                    Reserve6 = Value.ToString(); ;
                    break;
                case "StatusWord":
                    StatusWord = Convert.ToInt32(Value);
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_lese_systemsetting());
        }
        #endregion
    }
}
