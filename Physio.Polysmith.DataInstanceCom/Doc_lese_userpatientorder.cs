using pSystem.Interface.Util;
using System;

namespace Physio.Polysmith.DataInstanceCom
{
    public class Doc_lese_userpatientorder : ITable
    {
        #region 私有成员
        private int m_Id = 0;
        private bool m_Id_ini = false;
        private int m_UserpatientId = 0;
        private bool m_UserpatientId_ini = false;
        private DateTime m_AppointmentTime = default(DateTime);
        private bool m_AppointmentTime_ini = false;
        private int m_ServicesCenterId = 0;
        private bool m_ServicesCenterId_ini = false;
        private int m_DoctorId = 0;
        private bool m_DoctorId_ini = false;
        private DateTime m_CreateTime = default(DateTime);
        private bool m_CreateTime_ini = false;
        private int m_OrderState = 0;
        private bool m_OrderState_ini = false;
        private int m_SyncFlag = 0;
        private bool m_SyncFlag_ini = false;
        private DateTime m_SyncTime = default(DateTime);
        private bool m_SyncTime_ini = false;
        private string m_OrgNum = "";
        private bool m_OrgNum_ini = false;
        private string m_ReportPic = "";
        private bool m_ReportPic_ini = false;
        private string m_Edfpath = "";
        private bool m_Edfpath_ini = false;
        private string m_ReviewMontageName = "";
        private bool m_ReviewMontageName_ini = false;
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
        private int m_FrameCount = 0;
        private bool m_FrameCount_ini = false;
        private string m_Remark = "";
        private bool m_Remark_ini = false;
        private byte m_GenerateReport = 0;
        private bool m_GenerateReport_ini = false;
        private int m_IsSendWx = 0;
        private bool m_IsSendWx_ini = false;
        private DateTime m_ProcessTime = default(DateTime);
        private bool m_ProcessTime_ini = false;
        private DateTime m_DataUpTime = default(DateTime);
        private bool m_DataUpTime_ini = false;
        private DateTime m_DataAnalyTime = default(DateTime);
        private bool m_DataAnalyTime_ini = false;
        private DateTime m_ComplateTime = default(DateTime);
        private bool m_ComplateTime_ini = false;
        private DateTime m_DeleteTime = default(DateTime);
        private bool m_DeleteTime_ini = false;
        private DateTime m_UpdateTime = default(DateTime);
        private bool m_UpdateTime_ini = false;
        private int m_CenterpersonId = 0;
        private bool m_CenterpersonId_ini = false;
        private int m_DecisionType = 0;
        private bool m_DecisionType_ini = false;
        private int m_DecisionCenterId = 0;
        private bool m_DecisionCenterId_ini = false;
        private int m_IsPay = 0;
        private bool m_IsPay_ini = false;
        private int m_AuditCenter = 0;
        private bool m_AuditCenter_ini = false;
        private int m_IsPass = 0;
        private bool m_IsPass_ini = false;
        private int m_IsPush = 0;
        private bool m_IsPush_ini = false;
        private int m_LoginId = 0;
        private bool m_LoginId_ini = false;
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
        public int UserpatientId
        {
            set
            {
                m_UserpatientId = value;
                m_UserpatientId_ini = true;
            }
            get
            {
                return m_UserpatientId;
            }
        }
        public DateTime AppointmentTime
        {
            set
            {
                m_AppointmentTime = value;
                m_AppointmentTime_ini = true;
            }
            get
            {
                return m_AppointmentTime;
            }
        }
        public int ServicesCenterId
        {
            set
            {
                m_ServicesCenterId = value;
                m_ServicesCenterId_ini = true;
            }
            get
            {
                return m_ServicesCenterId;
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
        public int OrderState
        {
            set
            {
                m_OrderState = value;
                m_OrderState_ini = true;
            }
            get
            {
                return m_OrderState;
            }
        }
        public int SyncFlag
        {
            set
            {
                m_SyncFlag = value;
                m_SyncFlag_ini = true;
            }
            get
            {
                return m_SyncFlag;
            }
        }
        public DateTime SyncTime
        {
            set
            {
                m_SyncTime = value;
                m_SyncTime_ini = true;
            }
            get
            {
                return m_SyncTime;
            }
        }
        public string OrgNum
        {
            set
            {
                m_OrgNum = value;
                m_OrgNum_ini = true;
            }
            get
            {
                return m_OrgNum;
            }
        }
        public string ReportPic
        {
            set
            {
                m_ReportPic = value;
                m_ReportPic_ini = true;
            }
            get
            {
                return m_ReportPic;
            }
        }
        public string Edfpath
        {
            set
            {
                m_Edfpath = value;
                m_Edfpath_ini = true;
            }
            get
            {
                return m_Edfpath;
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
        public string Remark
        {
            set
            {
                m_Remark = value;
                m_Remark_ini = true;
            }
            get
            {
                return m_Remark;
            }
        }
        public byte GenerateReport
        {
            set
            {
                m_GenerateReport = value;
                m_GenerateReport_ini = true;
            }
            get
            {
                return m_GenerateReport;
            }
        }
        public int IsSendWx
        {
            set
            {
                m_IsSendWx = value;
                m_IsSendWx_ini = true;
            }
            get
            {
                return m_IsSendWx;
            }
        }
        public DateTime ProcessTime
        {
            set
            {
                m_ProcessTime = value;
                m_ProcessTime_ini = true;
            }
            get
            {
                return m_ProcessTime;
            }
        }
        public DateTime DataUpTime
        {
            set
            {
                m_DataUpTime = value;
                m_DataUpTime_ini = true;
            }
            get
            {
                return m_DataUpTime;
            }
        }
        public DateTime DataAnalyTime
        {
            set
            {
                m_DataAnalyTime = value;
                m_DataAnalyTime_ini = true;
            }
            get
            {
                return m_DataAnalyTime;
            }
        }
        public DateTime ComplateTime
        {
            set
            {
                m_ComplateTime = value;
                m_ComplateTime_ini = true;
            }
            get
            {
                return m_ComplateTime;
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
        public int CenterpersonId
        {
            set
            {
                m_CenterpersonId = value;
                m_CenterpersonId_ini = true;
            }
            get
            {
                return m_CenterpersonId;
            }
        }
        public int DecisionType
        {
            set
            {
                m_DecisionType = value;
                m_DecisionType_ini = true;
            }
            get
            {
                return m_DecisionType;
            }
        }
        public int DecisionCenterId
        {
            set
            {
                m_DecisionCenterId = value;
                m_DecisionCenterId_ini = true;
            }
            get
            {
                return m_DecisionCenterId;
            }
        }
        public int IsPay
        {
            set
            {
                m_IsPay = value;
                m_IsPay_ini = true;
            }
            get
            {
                return m_IsPay;
            }
        }
        public int AuditCenter
        {
            set
            {
                m_AuditCenter = value;
                m_AuditCenter_ini = true;
            }
            get
            {
                return m_AuditCenter;
            }
        }
        public int IsPass
        {
            set
            {
                m_IsPass = value;
                m_IsPass_ini = true;
            }
            get
            {
                return m_IsPass;
            }
        }
        public int IsPush
        {
            set
            {
                m_IsPush = value;
                m_IsPush_ini = true;
            }
            get
            {
                return m_IsPush;
            }
        }
        public int LoginId
        {
            set
            {
                m_LoginId = value;
                m_LoginId_ini = true;
            }
            get
            {
                return m_LoginId;
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
            if (this.m_UserpatientId_ini)
            {
                def = string.Format("{0}{2}{1}", def, "UserpatientId", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_UserpatientId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_AppointmentTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "AppointmentTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_AppointmentTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ServicesCenterId_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ServicesCenterId", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ServicesCenterId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_DoctorId_ini)
            {
                def = string.Format("{0}{2}{1}", def, "DoctorId", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_DoctorId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_CreateTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "CreateTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_CreateTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_OrderState_ini)
            {
                def = string.Format("{0}{2}{1}", def, "OrderState", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_OrderState, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_SyncFlag_ini)
            {
                def = string.Format("{0}{2}{1}", def, "SyncFlag", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_SyncFlag, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_SyncTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "SyncTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_SyncTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_OrgNum_ini)
            {
                def = string.Format("{0}{2}{1}", def, "OrgNum", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_OrgNum, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ReportPic_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ReportPic", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_ReportPic, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Edfpath_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Edfpath", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Edfpath, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ReviewMontageName_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ReviewMontageName", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_ReviewMontageName, Mulitcase ? "," : "");
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
            if (this.m_FrameCount_ini)
            {
                def = string.Format("{0}{2}{1}", def, "FrameCount", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_FrameCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Remark_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Remark", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Remark, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_GenerateReport_ini)
            {
                def = string.Format("{0}{2}{1}", def, "GenerateReport", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_GenerateReport, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_IsSendWx_ini)
            {
                def = string.Format("{0}{2}{1}", def, "IsSendWx", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_IsSendWx, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ProcessTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ProcessTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_ProcessTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_DataUpTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "DataUpTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_DataUpTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_DataAnalyTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "DataAnalyTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_DataAnalyTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ComplateTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ComplateTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_ComplateTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_DeleteTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "DeleteTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_DeleteTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_UpdateTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "UpdateTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_UpdateTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_CenterpersonId_ini)
            {
                def = string.Format("{0}{2}{1}", def, "CenterpersonId", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_CenterpersonId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_DecisionType_ini)
            {
                def = string.Format("{0}{2}{1}", def, "DecisionType", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_DecisionType, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_DecisionCenterId_ini)
            {
                def = string.Format("{0}{2}{1}", def, "DecisionCenterId", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_DecisionCenterId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_IsPay_ini)
            {
                def = string.Format("{0}{2}{1}", def, "IsPay", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_IsPay, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_AuditCenter_ini)
            {
                def = string.Format("{0}{2}{1}", def, "AuditCenter", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_AuditCenter, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_IsPass_ini)
            {
                def = string.Format("{0}{2}{1}", def, "IsPass", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_IsPass, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_IsPush_ini)
            {
                def = string.Format("{0}{2}{1}", def, "IsPush", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_IsPush, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_LoginId_ini)
            {
                def = string.Format("{0}{2}{1}", def, "LoginId", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_LoginId, Mulitcase ? "," : "");
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
            if (m_UserpatientId_ini)
            {
                returnstr = string.Format("{0}UserpatientId={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_UserpatientId);
                Mulitcase = true;
            }
            if (m_AppointmentTime_ini)
            {
                returnstr = string.Format("{0}AppointmentTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_AppointmentTime);
                Mulitcase = true;
            }
            if (m_ServicesCenterId_ini)
            {
                returnstr = string.Format("{0}ServicesCenterId={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ServicesCenterId);
                Mulitcase = true;
            }
            if (m_DoctorId_ini)
            {
                returnstr = string.Format("{0}DoctorId={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_DoctorId);
                Mulitcase = true;
            }
            if (m_CreateTime_ini)
            {
                returnstr = string.Format("{0}CreateTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_CreateTime);
                Mulitcase = true;
            }
            if (m_OrderState_ini)
            {
                returnstr = string.Format("{0}OrderState={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_OrderState);
                Mulitcase = true;
            }
            if (m_SyncFlag_ini)
            {
                returnstr = string.Format("{0}SyncFlag={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_SyncFlag);
                Mulitcase = true;
            }
            if (m_SyncTime_ini)
            {
                returnstr = string.Format("{0}SyncTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_SyncTime);
                Mulitcase = true;
            }
            if (m_OrgNum_ini)
            {
                returnstr = string.Format("{0}OrgNum='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_OrgNum);
                Mulitcase = true;
            }
            if (m_ReportPic_ini)
            {
                returnstr = string.Format("{0}ReportPic='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ReportPic);
                Mulitcase = true;
            }
            if (m_Edfpath_ini)
            {
                returnstr = string.Format("{0}Edfpath='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Edfpath);
                Mulitcase = true;
            }
            if (m_ReviewMontageName_ini)
            {
                returnstr = string.Format("{0}ReviewMontageName='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ReviewMontageName);
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
            if (m_FrameCount_ini)
            {
                returnstr = string.Format("{0}FrameCount={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_FrameCount);
                Mulitcase = true;
            }
            if (m_Remark_ini)
            {
                returnstr = string.Format("{0}Remark='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Remark);
                Mulitcase = true;
            }
            if (m_GenerateReport_ini)
            {
                returnstr = string.Format("{0}GenerateReport={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_GenerateReport);
                Mulitcase = true;
            }
            if (m_IsSendWx_ini)
            {
                returnstr = string.Format("{0}IsSendWx={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_IsSendWx);
                Mulitcase = true;
            }
            if (m_ProcessTime_ini)
            {
                returnstr = string.Format("{0}ProcessTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ProcessTime);
                Mulitcase = true;
            }
            if (m_DataUpTime_ini)
            {
                returnstr = string.Format("{0}DataUpTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_DataUpTime);
                Mulitcase = true;
            }
            if (m_DataAnalyTime_ini)
            {
                returnstr = string.Format("{0}DataAnalyTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_DataAnalyTime);
                Mulitcase = true;
            }
            if (m_ComplateTime_ini)
            {
                returnstr = string.Format("{0}ComplateTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ComplateTime);
                Mulitcase = true;
            }
            if (m_DeleteTime_ini)
            {
                returnstr = string.Format("{0}DeleteTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_DeleteTime);
                Mulitcase = true;
            }
            if (m_UpdateTime_ini)
            {
                returnstr = string.Format("{0}UpdateTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_UpdateTime);
                Mulitcase = true;
            }
            if (m_CenterpersonId_ini)
            {
                returnstr = string.Format("{0}CenterpersonId={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_CenterpersonId);
                Mulitcase = true;
            }
            if (m_DecisionType_ini)
            {
                returnstr = string.Format("{0}DecisionType={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_DecisionType);
                Mulitcase = true;
            }
            if (m_DecisionCenterId_ini)
            {
                returnstr = string.Format("{0}DecisionCenterId={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_DecisionCenterId);
                Mulitcase = true;
            }
            if (m_IsPay_ini)
            {
                returnstr = string.Format("{0}IsPay={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_IsPay);
                Mulitcase = true;
            }
            if (m_AuditCenter_ini)
            {
                returnstr = string.Format("{0}AuditCenter={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_AuditCenter);
                Mulitcase = true;
            }
            if (m_IsPass_ini)
            {
                returnstr = string.Format("{0}IsPass={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_IsPass);
                Mulitcase = true;
            }
            if (m_IsPush_ini)
            {
                returnstr = string.Format("{0}IsPush={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_IsPush);
                Mulitcase = true;
            }
            if (m_LoginId_ini)
            {
                returnstr = string.Format("{0}LoginId={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_LoginId);
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
            if (m_UserpatientId_ini)
            {
                def = string.Format("{0}{2}UserpatientId={1}", def, m_UserpatientId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_AppointmentTime_ini)
            {
                def = string.Format("{0}{2}AppointmentTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_AppointmentTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ServicesCenterId_ini)
            {
                def = string.Format("{0}{2}ServicesCenterId={1}", def, m_ServicesCenterId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_DoctorId_ini)
            {
                def = string.Format("{0}{2}DoctorId={1}", def, m_DoctorId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_CreateTime_ini)
            {
                def = string.Format("{0}{2}CreateTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_CreateTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_OrderState_ini)
            {
                def = string.Format("{0}{2}OrderState={1}", def, m_OrderState, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_SyncFlag_ini)
            {
                def = string.Format("{0}{2}SyncFlag={1}", def, m_SyncFlag, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_SyncTime_ini)
            {
                def = string.Format("{0}{2}SyncTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_SyncTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_OrgNum_ini)
            {
                def = string.Format("{0}{2}OrgNum='{1}'", def, m_OrgNum, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ReportPic_ini)
            {
                def = string.Format("{0}{2}ReportPic='{1}'", def, m_ReportPic, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Edfpath_ini)
            {
                def = string.Format("{0}{2}Edfpath='{1}'", def, m_Edfpath, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ReviewMontageName_ini)
            {
                def = string.Format("{0}{2}ReviewMontageName='{1}'", def, m_ReviewMontageName, Mulitcase ? "," : "");
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
            if (m_FrameCount_ini)
            {
                def = string.Format("{0}{2}FrameCount={1}", def, m_FrameCount, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Remark_ini)
            {
                def = string.Format("{0}{2}Remark='{1}'", def, m_Remark, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_GenerateReport_ini)
            {
                def = string.Format("{0}{2}GenerateReport={1}", def, m_GenerateReport, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_IsSendWx_ini)
            {
                def = string.Format("{0}{2}IsSendWx={1}", def, m_IsSendWx, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ProcessTime_ini)
            {
                def = string.Format("{0}{2}ProcessTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_ProcessTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_DataUpTime_ini)
            {
                def = string.Format("{0}{2}DataUpTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_DataUpTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_DataAnalyTime_ini)
            {
                def = string.Format("{0}{2}DataAnalyTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_DataAnalyTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ComplateTime_ini)
            {
                def = string.Format("{0}{2}ComplateTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_ComplateTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_DeleteTime_ini)
            {
                def = string.Format("{0}{2}DeleteTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_DeleteTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_UpdateTime_ini)
            {
                def = string.Format("{0}{2}UpdateTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_UpdateTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_CenterpersonId_ini)
            {
                def = string.Format("{0}{2}CenterpersonId={1}", def, m_CenterpersonId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_DecisionType_ini)
            {
                def = string.Format("{0}{2}DecisionType={1}", def, m_DecisionType, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_DecisionCenterId_ini)
            {
                def = string.Format("{0}{2}DecisionCenterId={1}", def, m_DecisionCenterId, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_IsPay_ini)
            {
                def = string.Format("{0}{2}IsPay={1}", def, m_IsPay, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_AuditCenter_ini)
            {
                def = string.Format("{0}{2}AuditCenter={1}", def, m_AuditCenter, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_IsPass_ini)
            {
                def = string.Format("{0}{2}IsPass={1}", def, m_IsPass, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_IsPush_ini)
            {
                def = string.Format("{0}{2}IsPush={1}", def, m_IsPush, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_LoginId_ini)
            {
                def = string.Format("{0}{2}LoginId={1}", def, m_LoginId, Mulitcase ? "," : "");
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
            return ("lese_userpatientorder");
        }
        public void SetValue(string strName, object Value)
        {
            switch (strName)
            {
                case "Id":
                    Id = Convert.ToInt32(Value);
                    break;
                case "UserpatientId":
                    UserpatientId = Convert.ToInt32(Value);
                    break;
                case "AppointmentTime":
                    AppointmentTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "ServicesCenterId":
                    ServicesCenterId = Convert.ToInt32(Value);
                    break;
                case "DoctorId":
                    DoctorId = Convert.ToInt32(Value);
                    break;
                case "CreateTime":
                    CreateTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "OrderState":
                    OrderState = Convert.ToInt32(Value);
                    break;
                case "SyncFlag":
                    SyncFlag = Convert.ToInt32(Value);
                    break;
                case "SyncTime":
                    SyncTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "OrgNum":
                    OrgNum = Value.ToString(); ;
                    break;
                case "ReportPic":
                    ReportPic = Value.ToString(); ;
                    break;
                case "Edfpath":
                    Edfpath = Value.ToString(); ;
                    break;
                case "ReviewMontageName":
                    ReviewMontageName = Value.ToString(); ;
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
                case "FrameCount":
                    FrameCount = Convert.ToInt32(Value);
                    break;
                case "Remark":
                    Remark = Value.ToString(); ;
                    break;
                case "GenerateReport":
                    GenerateReport = Convert.ToByte(Value);
                    break;
                case "IsSendWx":
                    IsSendWx = Convert.ToInt32(Value);
                    break;
                case "ProcessTime":
                    ProcessTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "DataUpTime":
                    DataUpTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "DataAnalyTime":
                    DataAnalyTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "ComplateTime":
                    ComplateTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "DeleteTime":
                    DeleteTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "UpdateTime":
                    UpdateTime = string.IsNullOrEmpty(Value.ToString()) ? default(DateTime) : Convert.ToDateTime(Value);
                    break;
                case "CenterpersonId":
                    CenterpersonId = Convert.ToInt32(Value);
                    break;
                case "DecisionType":
                    DecisionType = Convert.ToInt32(Value);
                    break;
                case "DecisionCenterId":
                    DecisionCenterId = Convert.ToInt32(Value);
                    break;
                case "IsPay":
                    IsPay = Convert.ToInt32(Value);
                    break;
                case "AuditCenter":
                    AuditCenter = Convert.ToInt32(Value);
                    break;
                case "IsPass":
                    IsPass = Convert.ToInt32(Value);
                    break;
                case "IsPush":
                    IsPush = Convert.ToInt32(Value);
                    break;
                case "LoginId":
                    LoginId = Convert.ToInt32(Value);
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_lese_userpatientorder());
        }
        #endregion
    }
}
