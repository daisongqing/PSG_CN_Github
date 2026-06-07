using pSystem.Interface.Util;
using System;
using System.IO;

namespace AwareTec.Polysmith.DataBaseCom
{
    /// <summary>
    /// 病人信息
    /// </summary>
    public class Doc_PatientInfo : ITable
    {
        public Doc_PatientInfo()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            ResultPhoto = Path.Combine(basePath, "Photo\\报告图.jpg");
            StagePhotoPath = Path.Combine(basePath, "Photo\\Stage.jpg");
            PosPhotoPath = Path.Combine(basePath, "Photo\\Pos.jpg");
            Spo2PhotoPath = Path.Combine(basePath, "Photo\\Spo2.jpg");
            CAPhotoPath = Path.Combine(basePath, "Photo\\CA.jpg");
            OAPhotoPath = Path.Combine(basePath, "Photo\\OA.jpg");
            MAPhotoPath = Path.Combine(basePath, "Photo\\MA.jpg");
            LegsPhotoPath = Path.Combine(basePath, "Photo\\PLM.jpg");
            PLMsPhotoPath = Path.Combine(basePath, "Photo\\PLMs.jpg");
            MicArousalPhotoPath = Path.Combine(basePath, "Photo\\Arousal.jpg");
            HypopneaPhotoPath = Path.Combine(basePath, "Photo\\Hypopnea.jpg");
            HeartPhotoPath = Path.Combine(basePath, "Photo\\Heart.jpg");
            BodyMovementPhotoPath = Path.Combine(basePath, "Photo\\BodyMovement.jpg");
            FirstSleepPhotoPath = Path.Combine(basePath, "Photo\\FirstSleep.jpg");
            SecondSleepPhotoPath = Path.Combine(basePath, "Photo\\SecondSleep.jpg");
            ThirdSleepPhotoPath = Path.Combine(basePath, "Photo\\ThirdSleep.jpg");
            FourthSleepPhotoPath = Path.Combine(basePath, "Photo\\FourthSleep.jpg");
            FifthSleepPhotoPath = Path.Combine(basePath, "Photo\\FifthSleep.jpg");

            LessSpO2PhotoPath = Path.Combine(basePath, "Photo\\LessSpO2.jpg");
            RangeSpO2PhotoPath = Path.Combine(basePath, "Photo\\RangeSpO2.jpg");
            RangeHeartRatePhotoPath = Path.Combine(basePath, "Photo\\RangeHeartRate.jpg");
            BreathBarPhotoPath = Path.Combine(basePath, "Photo\\BreathBar.jpg");
            SymptomDegreePhotoPath = Path.Combine(basePath, "Photo\\SymptomDegree.jpg");
            if (!Directory.Exists(ResultPhoto))
                Directory.CreateDirectory(Path.GetDirectoryName(ResultPhoto));
            ObesityDegreeL1 = "□";
            ObesityDegreeL2 = "■";
            ObesityDegreeL3 = "□";
            ObesityDegreeL4 = "□";
            CurrentDateTime = RecordTime = DateTime.Now;
            strCurrentDateTime = DateTime.Now.ToString("yyyy年MM月dd日");
        }
        #region 私有成员
        private int m_ID = 0;
        private bool m_ID_ini = false;
        private string m_MedicalNo = "";
        private bool m_MedicalNo_ini = false;
        private string m_OrderID = "";
        private bool m_OrderID_ini = false;
        private string m_PatientName = "";
        private bool m_PatientName_ini = false;
        private int m_Age = 0;
        private bool m_Age_ini = false;
        private string m_Gender = "";
        private bool m_Gender_ini = false;
        private DateTime m_BirthDate = DateTime.Now;
        private bool m_BirthDate_ini = false;
        private float m_Height = 0;
        private bool m_Height_ini = false;
        private float m_Weight = 0;
        private bool m_Weight_ini = false;
        private string m_Telephone = "";
        private bool m_Telephone_ini = false;
        private string m_MobilePhone = "";
        private bool m_MobilePhone_ini = false;
        private string m_EmergencyPeople = "";
        private bool m_EmergencyPeople_ini = false;
        private string m_EmergencyPhone = "";
        private bool m_EmergencyPhone_ini = false;
        private string m_Address = "";
        private bool m_Address_ini = false;
        private string m_Obstacles = "";
        private bool m_Obstacles_ini = false;
        private string m_ESS = "";
        private bool m_ESS_ini = false;
        private string m_O2DelveryRange = "";
        private bool m_O2DelveryRange_ini = false;
        private float m_Snoring = 0;
        private bool m_Snoring_ini = false;
        private float m_BAI = 0;
        private bool m_BAI_ini = false;
        private float m_RespiratoryRate = 0;
        private bool m_RespiratoryRate_ini = false;
        private string m_ScoreType = "";
        private bool m_ScoreType_ini = false;
        private float m_BDI = 0;
        private bool m_BDI_ini = false;
        private float m_NeckSize = 0;
        private bool m_NeckSize_ini = false;
        private string m_MedicalHistory = "";
        private bool m_MedicalHistory_ini = false;
        private string m_Medication = "";
        private bool m_Medication_ini = false;
        private string m_SleepDisorder = "";
        private bool m_SleepDisorder_ini = false;
        private string m_EKGAnalysis = "";
        private bool m_EKGAnalysis_ini = false;
        private string m_Comments = "";
        private bool m_Comments_ini = false;
        private string m_RecordType = "";
        private bool m_RecordType_ini = false;
        private string m_RoomNo = "";
        private bool m_RoomNo_ini = false;
        private string m_BedNo = "";
        private bool m_BedNo_ini = false;
        private DateTime m_ScheduingStartTime = DateTime.Now;
        private bool m_ScheduingStartTime_ini = false;
        private DateTime m_ScheduingEndTime = DateTime.Now;
        private bool m_ScheduingEndTime_ini = false;
        private string m_EEGNo = "";
        private bool m_EEGNo_ini = false;
        private string m_ConsultationMode = "";
        private bool m_ConsultationMode_ini = false;
        private int m_ModeType = 0;
        private bool m_ModeType_ini = false;
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
        /// <summary>
        /// 病例号，业务上实际的唯一标识符
        /// </summary>
        /// <remarks>
        /// 默认同个医院，
        /// 因此虽然儿童版成人版两套病人数据不共享，
        /// 但是病例号仍然都得是唯一标识符，
        /// 即儿童版的病例号与成人版的病例号必须无一对是相同的
        /// </remarks>
        public string PatientNo
        {
            set
            {
                m_MedicalNo = value;
                m_MedicalNo_ini = true;
            }
            get
            {
                return m_MedicalNo;
            }
        }
        public string OrderID
        {
            set
            {
                m_OrderID = value;
                m_OrderID_ini = true;
            }
            get
            {
                return m_OrderID;
            }
        }
        public string PatientName
        {
            set
            {
                m_PatientName = value;
                m_PatientName_ini = true;
            }
            get
            {
                return m_PatientName;
            }
        }
        public int Age
        {
            set
            {
                m_Age = value;
                m_Age_ini = true;
            }
            get
            {
                return m_Age;
            }
        }
        public string Gender
        {
            set
            {
                m_Gender = value;
                m_Gender_ini = true;
            }
            get
            {
                return m_Gender;
            }
        }
        public DateTime BirthDate
        {
            set
            {
                m_BirthDate = value;
                Birthday = BirthDate.ToString("yyyy-MM-dd");
                m_BirthDate_ini = true;
            }
            get
            {
                return m_BirthDate;
            }
        }
        public float Height
        {
            set
            {
                m_Height = value;
                m_Height_ini = true;
            }
            get
            {
                return m_Height;
            }
        }
        public float Weight
        {
            set
            {
                m_Weight = value;
                m_Weight_ini = true;
            }
            get
            {
                return m_Weight;
            }
        }
        public string Telephone
        {
            set
            {
                m_Telephone = value;
                m_Telephone_ini = true;
            }
            get
            {
                return m_Telephone;
            }
        }
        public string MobilePhone
        {
            set
            {
                m_MobilePhone = value;
                m_MobilePhone_ini = true;
            }
            get
            {
                return m_MobilePhone;
            }
        }
        public string EmergencyPeople
        {
            set
            {
                m_EmergencyPeople = value;
                m_EmergencyPeople_ini = true;
            }
            get
            {
                return m_EmergencyPeople;
            }
        }
        public string EmergencyPhone
        {
            set
            {
                m_EmergencyPhone = value;
                m_EmergencyPhone_ini = true;
            }
            get
            {
                return m_EmergencyPhone;
            }
        }
        public string Address
        {
            set
            {
                m_Address = value;
                m_Address_ini = true;
            }
            get
            {
                return m_Address;
            }
        }
        public string Obstacles
        {
            set
            {
                m_Obstacles = value;
                m_Obstacles_ini = true;
            }
            get
            {
                return m_Obstacles;
            }
        }
        public string ESS
        {
            set
            {
                m_ESS = value;
                m_ESS_ini = true;
            }
            get
            {
                return m_ESS;
            }
        }
        public string O2DelveryRange
        {
            set
            {
                m_O2DelveryRange = value;
                m_O2DelveryRange_ini = true;
            }
            get
            {
                return m_O2DelveryRange;
            }
        }
        public float Snoring
        {
            set
            {
                m_Snoring = value;
                m_Snoring_ini = true;
            }
            get
            {
                return m_Snoring;
            }
        }
        public float BAI
        {
            set
            {
                m_BAI = value;
                m_BAI_ini = true;
            }
            get
            {
                return m_BAI;
            }
        }
        public float RespiratoryRate
        {
            set
            {
                m_RespiratoryRate = value;
                m_RespiratoryRate_ini = true;
            }
            get
            {
                return m_RespiratoryRate;
            }
        }
        public string ScoreType
        {
            set
            {
                m_ScoreType = value;
                m_ScoreType_ini = true;
            }
            get
            {
                return m_ScoreType;
            }
        }
        public float BDI
        {
            set
            {
                m_BDI = value;
                m_BDI_ini = true;
            }
            get
            {
                return m_BDI;
            }
        }
        public float NeckSize
        {
            set
            {
                m_NeckSize = value;
                m_NeckSize_ini = true;
            }
            get
            {
                return m_NeckSize;
            }
        }
        public string MedicalHistory
        {
            set
            {
                m_MedicalHistory = value;
                m_MedicalHistory_ini = true;
            }
            get
            {
                return m_MedicalHistory;
            }
        }
        public string Medication
        {
            set
            {
                m_Medication = value;
                m_Medication_ini = true;
            }
            get
            {
                return m_Medication;
            }
        }
        public string SleepDisorder
        {
            set
            {
                m_SleepDisorder = value;
                m_SleepDisorder_ini = true;
            }
            get
            {
                return m_SleepDisorder;
            }
        }
        public string EKGAnalysis
        {
            set
            {
                m_EKGAnalysis = value;
                m_EKGAnalysis_ini = true;
            }
            get
            {
                return m_EKGAnalysis;
            }
        }
        public string Comments
        {
            set
            {
                m_Comments = value;
                m_Comments_ini = true;
            }
            get
            {
                return m_Comments;
            }
        }
        public string RecordType
        {
            set
            {
                m_RecordType = value;
                m_RecordType_ini = true;
            }
            get
            {
                return m_RecordType;
            }
        }
        public string RoomNo
        {
            set
            {
                m_RoomNo = value;
                m_RoomNo_ini = true;
            }
            get
            {
                return m_RoomNo;
            }
        }
        public string BedNo
        {
            set
            {
                m_BedNo = value;
                m_BedNo_ini = true;
            }
            get
            {
                return m_BedNo;
            }
        }
        public DateTime ScheduingStartTime
        {
            set
            {
                m_ScheduingStartTime = value;
                m_ScheduingStartTime_ini = true;
            }
            get
            {
                return m_ScheduingStartTime;
            }
        }
        public DateTime ScheduingEndTime
        {
            set
            {
                m_ScheduingEndTime = value;
                m_ScheduingEndTime_ini = true;
            }
            get
            {
                return m_ScheduingEndTime;
            }
        }
        public string EEGNo
        {
            set
            {
                m_EEGNo = value;
                m_EEGNo_ini = true;
            }
            get
            {
                return m_EEGNo;
            }
        }
        public string ConsultationMode
        {
            set
            {
                m_ConsultationMode = value;
                m_ConsultationMode_ini = true;
            }
            get
            {
                return m_ConsultationMode;
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
        #endregion
        #region 继承成员
        public string GetInsertString()
        {
            string def = " (";
            string val = " VALUES(";
            bool Mulitcase = false;
            ////自增不需要设置
            //if (this.m_ID_ini)
            //{
            //    def = string.Format("{0}{2}{1}", def, "ID", Mulitcase ? "," : "");
            //    val = string.Format("{0}{2}{1}", val, m_ID, Mulitcase ? "," : "");
            //    Mulitcase = true;
            //}
            if (this.m_MedicalNo_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MedicalNo", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_MedicalNo, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_OrderID_ini)
            {
                def = string.Format("{0}{2}{1}", def, "OrderID", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_OrderID, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_PatientName_ini)
            {
                def = string.Format("{0}{2}{1}", def, "PatientName", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_PatientName, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Age_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Age", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Age, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Gender_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Gender", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Gender, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_BirthDate_ini)
            {
                def = string.Format("{0}{2}{1}", def, "BirthDate", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_BirthDate, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Height_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Height", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Height, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Weight_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Weight", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Weight, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Telephone_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Telephone", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Telephone, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MobilePhone_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MobilePhone", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_MobilePhone, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_EmergencyPeople_ini)
            {
                def = string.Format("{0}{2}{1}", def, "EmergencyPeople", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_EmergencyPeople, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_EmergencyPhone_ini)
            {
                def = string.Format("{0}{2}{1}", def, "EmergencyPhone", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_EmergencyPhone, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Address_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Address", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Address, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Obstacles_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Obstacles", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Obstacles, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ESS_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ESS", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_ESS, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_O2DelveryRange_ini)
            {
                def = string.Format("{0}{2}{1}", def, "O2DelveryRange", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_O2DelveryRange, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Snoring_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Snoring", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Snoring, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_BAI_ini)
            {
                def = string.Format("{0}{2}{1}", def, "BAI", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_BAI, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_RespiratoryRate_ini)
            {
                def = string.Format("{0}{2}{1}", def, "RespiratoryRate", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_RespiratoryRate, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ScoreType_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ScoreType", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_ScoreType, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_BDI_ini)
            {
                def = string.Format("{0}{2}{1}", def, "BDI", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_BDI, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_NeckSize_ini)
            {
                def = string.Format("{0}{2}{1}", def, "NeckSize", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_NeckSize, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MedicalHistory_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MedicalHistory", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_MedicalHistory, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Medication_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Medication", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Medication, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_SleepDisorder_ini)
            {
                def = string.Format("{0}{2}{1}", def, "SleepDisorder", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_SleepDisorder, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_EKGAnalysis_ini)
            {
                def = string.Format("{0}{2}{1}", def, "EKGAnalysis", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_EKGAnalysis, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Comments_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Comments", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Comments, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_RecordType_ini)
            {
                def = string.Format("{0}{2}{1}", def, "RecordType", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_RecordType, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_RoomNo_ini)
            {
                def = string.Format("{0}{2}{1}", def, "RoomNo", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_RoomNo, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_BedNo_ini)
            {
                def = string.Format("{0}{2}{1}", def, "BedNo", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_BedNo, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ScheduingStartTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ScheduingStartTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_ScheduingStartTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ScheduingEndTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ScheduingEndTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1:yyyy-MM-dd HH:mm:ss}'", val, m_ScheduingEndTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_EEGNo_ini)
            {
                def = string.Format("{0}{2}{1}", def, "EEGNo", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_EEGNo, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ConsultationMode_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ConsultationMode", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_ConsultationMode, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ModeType_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ModeType", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ModeType, Mulitcase ? "," : "");
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
            if (m_MedicalNo_ini)
            {
                returnstr = string.Format("{0}MedicalNo='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MedicalNo);
                Mulitcase = true;
            }
            if (m_OrderID_ini)
            {
                returnstr = string.Format("{0}OrderID='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_OrderID);
                Mulitcase = true;
            }
            if (m_PatientName_ini)
            {
                returnstr = string.Format("{0}PatientName='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_PatientName);
                Mulitcase = true;
            }
            if (m_Age_ini)
            {
                returnstr = string.Format("{0}Age={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Age);
                Mulitcase = true;
            }
            if (m_Gender_ini)
            {
                returnstr = string.Format("{0}Gender='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Gender);
                Mulitcase = true;
            }
            if (m_BirthDate_ini)
            {
                returnstr = string.Format("{0}BirthDate='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_BirthDate);
                Mulitcase = true;
            }
            if (m_Height_ini)
            {
                returnstr = string.Format("{0}Height={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Height);
                Mulitcase = true;
            }
            if (m_Weight_ini)
            {
                returnstr = string.Format("{0}Weight={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Weight);
                Mulitcase = true;
            }
            if (m_Telephone_ini)
            {
                returnstr = string.Format("{0}Telephone='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Telephone);
                Mulitcase = true;
            }
            if (m_MobilePhone_ini)
            {
                returnstr = string.Format("{0}MobilePhone='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MobilePhone);
                Mulitcase = true;
            }
            if (m_EmergencyPeople_ini)
            {
                returnstr = string.Format("{0}EmergencyPeople='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_EmergencyPeople);
                Mulitcase = true;
            }
            if (m_EmergencyPhone_ini)
            {
                returnstr = string.Format("{0}EmergencyPhone='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_EmergencyPhone);
                Mulitcase = true;
            }
            if (m_Address_ini)
            {
                returnstr = string.Format("{0}Address='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Address);
                Mulitcase = true;
            }
            if (m_Obstacles_ini)
            {
                returnstr = string.Format("{0}Obstacles='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Obstacles);
                Mulitcase = true;
            }
            if (m_ESS_ini)
            {
                returnstr = string.Format("{0}ESS='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ESS);
                Mulitcase = true;
            }
            if (m_O2DelveryRange_ini)
            {
                returnstr = string.Format("{0}O2DelveryRange='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_O2DelveryRange);
                Mulitcase = true;
            }
            if (m_Snoring_ini)
            {
                returnstr = string.Format("{0}Snoring={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Snoring);
                Mulitcase = true;
            }
            if (m_BAI_ini)
            {
                returnstr = string.Format("{0}BAI={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_BAI);
                Mulitcase = true;
            }
            if (m_RespiratoryRate_ini)
            {
                returnstr = string.Format("{0}RespiratoryRate={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_RespiratoryRate);
                Mulitcase = true;
            }
            if (m_ScoreType_ini)
            {
                returnstr = string.Format("{0}ScoreType='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ScoreType);
                Mulitcase = true;
            }
            if (m_BDI_ini)
            {
                returnstr = string.Format("{0}BDI={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_BDI);
                Mulitcase = true;
            }
            if (m_NeckSize_ini)
            {
                returnstr = string.Format("{0}NeckSize={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_NeckSize);
                Mulitcase = true;
            }
            if (m_MedicalHistory_ini)
            {
                returnstr = string.Format("{0}MedicalHistory='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MedicalHistory);
                Mulitcase = true;
            }
            if (m_Medication_ini)
            {
                returnstr = string.Format("{0}Medication='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Medication);
                Mulitcase = true;
            }
            if (m_SleepDisorder_ini)
            {
                returnstr = string.Format("{0}SleepDisorder='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_SleepDisorder);
                Mulitcase = true;
            }
            if (m_EKGAnalysis_ini)
            {
                returnstr = string.Format("{0}EKGAnalysis='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_EKGAnalysis);
                Mulitcase = true;
            }
            if (m_Comments_ini)
            {
                returnstr = string.Format("{0}Comments='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Comments);
                Mulitcase = true;
            }
            if (m_RecordType_ini)
            {
                returnstr = string.Format("{0}RecordType='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_RecordType);
                Mulitcase = true;
            }
            if (m_RoomNo_ini)
            {
                returnstr = string.Format("{0}RoomNo='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_RoomNo);
                Mulitcase = true;
            }
            if (m_BedNo_ini)
            {
                returnstr = string.Format("{0}BedNo='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_BedNo);
                Mulitcase = true;
            }
            if (m_ScheduingStartTime_ini)
            {
                returnstr = string.Format("{0}ScheduingStartTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ScheduingStartTime);
                Mulitcase = true;
            }
            if (m_ScheduingEndTime_ini)
            {
                returnstr = string.Format("{0}ScheduingEndTime='{1:yyyy-MM-dd HH:mm:ss}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ScheduingEndTime);
                Mulitcase = true;
            }
            if (m_EEGNo_ini)
            {
                returnstr = string.Format("{0}EEGNo='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_EEGNo);
                Mulitcase = true;
            }
            if (m_ConsultationMode_ini)
            {
                returnstr = string.Format("{0}ConsultationMode='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ConsultationMode);
                Mulitcase = true;
            }
            if (m_ModeType_ini)
            {
                returnstr = string.Format("{0}ModeType={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ModeType);
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
            if (m_MedicalNo_ini)
            {
                def = string.Format("{0}{2}MedicalNo='{1}'", def, m_MedicalNo, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_OrderID_ini)
            {
                def = string.Format("{0}{2}OrderID='{1}'", def, m_OrderID, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_PatientName_ini)
            {
                def = string.Format("{0}{2}PatientName='{1}'", def, m_PatientName, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Age_ini)
            {
                def = string.Format("{0}{2}Age={1}", def, m_Age, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Gender_ini)
            {
                def = string.Format("{0}{2}Gender='{1}'", def, m_Gender, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_BirthDate_ini)
            {
                def = string.Format("{0}{2}BirthDate='{1:yyyy-MM-dd HH:mm:ss}'", def, m_BirthDate, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Height_ini)
            {
                def = string.Format("{0}{2}Height={1}", def, m_Height, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Weight_ini)
            {
                def = string.Format("{0}{2}Weight={1}", def, m_Weight, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Telephone_ini)
            {
                def = string.Format("{0}{2}Telephone='{1}'", def, m_Telephone, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MobilePhone_ini)
            {
                def = string.Format("{0}{2}MobilePhone='{1}'", def, m_MobilePhone, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_EmergencyPeople_ini)
            {
                def = string.Format("{0}{2}EmergencyPeople='{1}'", def, m_EmergencyPeople, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_EmergencyPhone_ini)
            {
                def = string.Format("{0}{2}EmergencyPhone='{1}'", def, m_EmergencyPhone, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Address_ini)
            {
                def = string.Format("{0}{2}Address='{1}'", def, m_Address, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Obstacles_ini)
            {
                def = string.Format("{0}{2}Obstacles='{1}'", def, m_Obstacles, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ESS_ini)
            {
                def = string.Format("{0}{2}ESS='{1}'", def, m_ESS, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_O2DelveryRange_ini)
            {
                def = string.Format("{0}{2}O2DelveryRange='{1}'", def, m_O2DelveryRange, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Snoring_ini)
            {
                def = string.Format("{0}{2}Snoring={1}", def, m_Snoring, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_BAI_ini)
            {
                def = string.Format("{0}{2}BAI={1}", def, m_BAI, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_RespiratoryRate_ini)
            {
                def = string.Format("{0}{2}RespiratoryRate={1}", def, m_RespiratoryRate, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ScoreType_ini)
            {
                def = string.Format("{0}{2}ScoreType='{1}'", def, m_ScoreType, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_BDI_ini)
            {
                def = string.Format("{0}{2}BDI={1}", def, m_BDI, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_NeckSize_ini)
            {
                def = string.Format("{0}{2}NeckSize={1}", def, m_NeckSize, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MedicalHistory_ini)
            {
                def = string.Format("{0}{2}MedicalHistory='{1}'", def, m_MedicalHistory, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Medication_ini)
            {
                def = string.Format("{0}{2}Medication='{1}'", def, m_Medication, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_SleepDisorder_ini)
            {
                def = string.Format("{0}{2}SleepDisorder='{1}'", def, m_SleepDisorder, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_EKGAnalysis_ini)
            {
                def = string.Format("{0}{2}EKGAnalysis='{1}'", def, m_EKGAnalysis, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Comments_ini)
            {
                def = string.Format("{0}{2}Comments='{1}'", def, m_Comments, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_RecordType_ini)
            {
                def = string.Format("{0}{2}RecordType='{1}'", def, m_RecordType, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_RoomNo_ini)
            {
                def = string.Format("{0}{2}RoomNo='{1}'", def, m_RoomNo, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_BedNo_ini)
            {
                def = string.Format("{0}{2}BedNo='{1}'", def, m_BedNo, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ScheduingStartTime_ini)
            {
                def = string.Format("{0}{2}ScheduingStartTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_ScheduingStartTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ScheduingEndTime_ini)
            {
                def = string.Format("{0}{2}ScheduingEndTime='{1:yyyy-MM-dd HH:mm:ss}'", def, m_ScheduingEndTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_EEGNo_ini)
            {
                def = string.Format("{0}{2}EEGNo='{1}'", def, m_EEGNo, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ConsultationMode_ini)
            {
                def = string.Format("{0}{2}ConsultationMode='{1}'", def, m_ConsultationMode, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ModeType_ini)
            {
                def = string.Format("{0}{2}ModeType={1}", def, m_ModeType, Mulitcase ? "," : "");
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
            return ("Patient");
        }
        public void SetValue(string strName, object Value)
        {
            string value = Value.ToString();
            switch (strName)
            {
                case "ID":
                    ID = Convert.ToInt32(Value);
                    break;
                case "MedicalNo":
                    PatientNo = Value.ToString(); ;
                    break;
                case "OrderID":
                    OrderID = Value.ToString(); ;
                    break;
                case "PatientName":
                    PatientName = Value.ToString(); ;
                    break;
                case "Age":
                    Age = Convert.ToInt32(Value);
                    break;
                case "Gender":
                    Gender = Value.ToString(); ;
                    break;
                case "BirthDate":
                    BirthDate = value == "" ? DateTime.Parse("1970-01-01 00:00:00") : Convert.ToDateTime(Value);
                    break;
                case "Height":
                    Height = value == "" ? 0 : Convert.ToSingle(Value);
                    break;
                case "Weight":
                    Weight = value == "" ? 0 : Convert.ToSingle(Value);
                    break;
                case "Telephone":
                    Telephone = Value.ToString(); ;
                    break;
                case "MobilePhone":
                    MobilePhone = Value.ToString(); ;
                    break;
                case "EmergencyPeople":
                    EmergencyPeople = Value.ToString(); ;
                    break;
                case "EmergencyPhone":
                    EmergencyPhone = Value.ToString(); ;
                    break;
                case "Address":
                    Address = Value.ToString(); ;
                    break;
                case "Obstacles":
                    Obstacles = Value.ToString(); ;
                    break;
                case "ESS":
                    ESS = Value.ToString(); ;
                    break;
                case "O2DelveryRange":
                    O2DelveryRange = Value.ToString(); ;
                    break;
                case "Snoring":
                    Snoring = value == "" ? 0 : Convert.ToSingle(Value);
                    break;
                case "BAI":
                    BAI = value == "" ? 0 : Convert.ToSingle(Value);
                    break;
                case "RespiratoryRate":
                    RespiratoryRate = value == "" ? 0 : Convert.ToSingle(Value);
                    break;
                case "ScoreType":
                    ScoreType = Value.ToString(); ;
                    break;
                case "BDI":
                    BDI = value == "" ? 0 : Convert.ToSingle(Value);
                    break;
                case "NeckSize":
                    NeckSize = value == "" ? 0 : Convert.ToSingle(Value);
                    break;
                case "MedicalHistory":
                    MedicalHistory = Value.ToString(); ;
                    break;
                case "Medication":
                    Medication = Value.ToString(); ;
                    break;
                case "SleepDisorder":
                    SleepDisorder = Value.ToString(); ;
                    break;
                case "EKGAnalysis":
                    EKGAnalysis = Value.ToString(); ;
                    break;
                case "Comments":
                    Comments = Value.ToString(); ;
                    break;
                case "RecordType":
                    RecordType = Value.ToString(); ;
                    break;
                case "RoomNo":
                    RoomNo = Value.ToString(); ;
                    break;
                case "BedNo":
                    BedNo = Value.ToString(); ;
                    break;
                case "ScheduingStartTime":
                    ScheduingStartTime = value == "" ? DateTime.Parse("1970-01-01 00:00:00") : Convert.ToDateTime(Value);
                    break;
                case "ScheduingEndTime":
                    ScheduingEndTime = value == "" ? DateTime.Parse("1970-01-01 00:00:00") : Convert.ToDateTime(Value);
                    break;
                case "EEGNo":
                    EEGNo = Value.ToString(); ;
                    break;
                case "ConsultationMode":
                    ConsultationMode = Value.ToString(); ;
                    break;
                case "ModeType":
                    ModeType = Convert.ToInt32(Value);
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_PatientInfo());
        }
        #endregion

        /// <summary>
        /// 当前日期
        /// </summary>
        public DateTime CurrentDateTime { set; get; }
        /// <summary>
        /// 当前日期(string类型）
        /// </summary>
        public string strCurrentDateTime { set; get; }
        /// <summary>
        /// 监测开始日期
        /// </summary>
        public DateTime RecordTime { set; get; }
        /// <summary>
        /// 病人生日
        /// </summary>
        public string Birthday { set; get; }
        /// <summary>
        /// (BMI = 体重/（身高2）；体重以KG为单位，身高以M为单位。)
        /// </summary>
        public float BMI
        {
            set
            {


            }
            get
            {
                return (float)Math.Round(Weight / Math.Pow(Height / 100, 2), 2);
            }
        }
        /// <summary>
        /// 正常：18.5-23.9；超重：≥24；偏胖：24-27.9；肥胖：≥28；重度肥胖：≥30；
        /// </summary>
        public string BMIRange
        {
            set
            {


            }
            get
            {
                return BMI < 18.5 ? "偏瘦" : (BMI >= 18.5 && BMI < 24) ? "正常" : (BMI >= 24 && BMI < 25) ? "超重" : (BMI >= 25 && BMI < 28) ? "偏胖" : (BMI >= 28 && BMI < 30) ? "肥胖" : "重度肥胖";
            }
        }
        /// <summary>
        /// 肥胖等级描述
        /// </summary>
        public string strBMIRange
        {
            get
            {
                //return string.Format("偏瘦{4} 正常{0} 轻度{1} 中度{2} 重度{3}", BMI >= 18.5 && BMI < 24 ? "■" : "□", (BMI >= 24 && BMI < 25) ? "■" : "□", (BMI >= 25 && BMI < 28) ? "■" : "□", ((BMI >= 28 && BMI < 30) ? "■" : "□"), BMI < 18.5 ? "■" : "□");
                return string.Format("偏瘦{4} 正常{0} 轻度{1} 中度{2} 重度{3}", BMI >= 18.5 && BMI < 24 ? "■" : "□", (BMI >= 24 && BMI < 25) ? "■" : "□", (BMI >= 25 && BMI < 28) ? "■" : "□", ((BMI >= 28) ? "■" : "□"), BMI < 18.5 ? "■" : "□");
            }
        }
        /// <summary>
        /// 肥胖指数
        /// </summary>
        public float ObesityIndex { set; get; }

        /// <summary>
        /// 病人名字
        /// </summary>
        public string DoctorName { set; get; }
        /// <summary>
        /// 结果图路径
        /// </summary>
        public string ResultPhoto { set; get; }
        /// <summary>
        /// 肥胖等级 正常
        /// </summary>
        public string ObesityDegreeL1 { set; get; }
        /// <summary>
        /// 肥胖等级 轻度
        /// </summary>
        public string ObesityDegreeL2 { set; get; }
        /// <summary>
        /// 肥胖等级 中度
        /// </summary>
        public string ObesityDegreeL3 { set; get; }
        /// <summary>
        /// 肥胖等级 重度
        /// </summary>
        public string ObesityDegreeL4 { set; get; }
        /// <summary>
        /// 睡眠分期图谱路径
        /// </summary>
        public string StagePhotoPath { set; get; }
        /// <summary>
        /// 体位图谱路径
        /// </summary>
        public string PosPhotoPath { set; get; }
        /// <summary>
        /// 血氧图谱
        /// </summary>
        public string Spo2PhotoPath { set; get; }
        /// <summary>
        /// 心率图谱
        /// </summary>
        public string HeartPhotoPath { set; get; }
        /// <summary>
        /// 体动图谱
        /// </summary>
        public string BodyMovementPhotoPath { set; get; }
        /// <summary>
        /// CA图谱路径
        /// </summary>
        public string CAPhotoPath { set; get; }
        /// <summary>
        /// OA图谱路径
        /// </summary>
        public string OAPhotoPath { set; get; }
        /// <summary>
        /// MA图谱路径
        /// </summary>
        public string MAPhotoPath { set; get; }
        /// <summary>
        /// 腿动路径
        /// </summary>
        public string LegsPhotoPath { set; get; }
        /// <summary>
        /// PLMS图谱路径
        /// </summary>
        public string PLMsPhotoPath { set; get; }
        /// <summary>
        /// 微觉醒图谱路径
        /// </summary>
        public string MicArousalPhotoPath { set; get; }
        /// <summary>
        /// Hyp图谱路径
        /// </summary>
        public string HypopneaPhotoPath { set; get; }

        /// <summary>
        /// 小睡片段一图片
        /// </summary>
        public string FirstSleepPhotoPath { set; get; }
        /// <summary>
        /// 小睡片段二图片
        /// </summary>
        public string SecondSleepPhotoPath { set; get; }
        /// <summary>
        /// 小睡片段三图片
        /// </summary>
        public string ThirdSleepPhotoPath { set; get; }
        /// <summary>
        /// 小睡片段四图片
        /// </summary>
        public string FourthSleepPhotoPath { set; get; }
        /// <summary>
        /// 小睡片段五图片
        /// </summary>
        public string FifthSleepPhotoPath { set; get; }

        /// <summary>
        /// 血氧趋势图路径
        /// </summary>
        public string LessSpO2PhotoPath { set; get; }

        /// <summary>
        /// 血氧范围趋势图路径
        /// </summary>
        public string RangeSpO2PhotoPath { set; get; }

        /// <summary>
        /// 心率范围趋势图路径
        /// </summary>
        public string RangeHeartRatePhotoPath { set; get; }

        /// <summary>
        /// 呼吸事件柱状趋势图路径
        /// </summary>
        public string BreathBarPhotoPath { set; get; }

        /// <summary>
        /// 症状程度趋势图路径
        /// </summary>
        public string SymptomDegreePhotoPath { set; get; }
    }
}
