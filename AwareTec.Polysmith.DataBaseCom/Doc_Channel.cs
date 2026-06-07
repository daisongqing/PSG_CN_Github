using pSystem.Interface.Util;
using System;
using System.Drawing;

namespace AwareTec.Polysmith.DataBaseCom
{
    /// <summary>
    /// 通道基础信息表单
    /// </summary>
    public class Doc_Channel : ITable
    {
        #region 私有成员
        private int m_ID = 0;
        private bool m_ID_ini = false;
        private string m_Name = "";
        private bool m_Name_ini = false;
        private string m_Description = "";
        private bool m_Description_ini = false;
        private string m_SignName = "";
        private bool m_SignName_ini = false;
        private string m_SensorType = "";
        private bool m_SensorType_ini = false;
        private float m_ViewMinValue = 0;
        private bool m_ViewMinValue_ini = false;
        private float m_ViewMaxValue = 0;
        private bool m_ViewMaxValue_ini = false;
        private float m_ADMinValue = 0;
        private bool m_ADMinValue_ini = false;
        private float m_ADMaxValue = 0;
        private bool m_ADMaxValue_ini = false;
        private string m_Unit = "";
        private bool m_Unit_ini = false;
        private bool m_NeedConvert = false;
        private bool m_NeedConvert_ini = false;
        private bool m_Enable = false;
        private bool m_Enable_ini = false;
        private int m_SpanTime = 0;
        private bool m_SpanTime_ini = false;
        private int m_ValueZoomRate = 0;
        private bool m_ValueZoomRate_ini = false;
        private int m_GroupID = 0;
        private bool m_GroupID_ini = false;
        private int m_IndexInGroup = 0;
        private bool m_IndexInGroup_ini = false;
        private int m_LenghtInGroup = 0;
        private bool m_LenghtInGroup_ini = false;
        private int m_ByteLenghtOfValue = 0;
        private bool m_ByteLenghtOfValue_ini = false;
        private bool m_UnSignData = false;
        private bool m_UnSignData_ini = false;
        private int m_pViewMinValue = 0;
        private bool m_pViewMinValue_ini = false;
        private int m_pViewMaxValue = 0;
        private bool m_pViewMaxValue_ini = false;
        private int m_pADMinValue = 0;
        private bool m_pADMinValue_ini = false;
        private int m_pADMaxValue = 0;
        private bool m_pADMaxValue_ini = false;
        private bool m_CalibrationsVisible = false;
        private bool m_CalibrationsVisible_ini = false;
        private bool m_IsShowValue = false;
        private bool m_IsShowValue_ini = false;
        private bool m_AntipoleEnable = false;
        private bool m_AntipoleEnable_ini = false;
        private string m_PenColor = "";
        private bool m_PenColor_ini = false;
        private bool m_PixelEnable = false;
        private bool m_PixelEnable_ini = false;
        private string m_strPixelRange = "";
        private bool m_strPixelRange_ini = false;
        private string m_Reserve1 = "";
        private bool m_Reserve1_ini = false;
        private string m_Reserve2 = "";
        private bool m_Reserve2_ini = false;
        private string m_Reserve3 = "";
        private bool m_Reserve3_ini = false;
        private string m_Reserve4 = "";
        private bool m_Reserve4_ini = false;
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
        public string SignName
        {
            set
            {
                m_SignName = value;
                m_SignName_ini = true;
            }
            get
            {
                return m_SignName;
            }
        }
        public string SensorType
        {
            set
            {
                m_SensorType = value;
                m_SensorType_ini = true;
            }
            get
            {
                return m_SensorType;
            }
        }
        public float ViewMinValue
        {
            set
            {
                m_ViewMinValue = value;
                m_ViewMinValue_ini = true;
            }
            get
            {
                return m_ViewMinValue;
            }
        }
        public float ViewMaxValue
        {
            set
            {
                m_ViewMaxValue = value;
                m_ViewMaxValue_ini = true;
            }
            get
            {
                return m_ViewMaxValue;
            }
        }
        public float ADMinValue
        {
            set
            {
                m_ADMinValue = value;
                m_ADMinValue_ini = true;
            }
            get
            {
                return m_ADMinValue;
            }
        }
        public float ADMaxValue
        {
            set
            {
                m_ADMaxValue = value;
                m_ADMaxValue_ini = true;
            }
            get
            {
                return m_ADMaxValue;
            }
        }
        public string Unit
        {
            set
            {
                m_Unit = value;
                m_Unit_ini = true;
            }
            get
            {
                return m_Unit;
            }
        }
        public bool NeedConvert
        {
            set
            {
                m_NeedConvert = value;
                m_NeedConvert_ini = true;
            }
            get
            {
                return m_NeedConvert;
            }
        }
        public bool Enable
        {
            set
            {
                m_Enable = value;
                m_Enable_ini = true;
            }
            get
            {
                return m_Enable;
            }
        }
        public int SpanTime
        {
            set
            {
                m_SpanTime = value;
                m_SpanTime_ini = true;
            }
            get
            {
                return m_SpanTime;
            }
        }
        public int ValueZoomRate
        {
            set
            {
                m_ValueZoomRate = value;
                m_ValueZoomRate_ini = true;
            }
            get
            {
                return m_ValueZoomRate;
            }
        }
        public int GroupID
        {
            set
            {
                m_GroupID = value;
                m_GroupID_ini = true;
            }
            get
            {
                return m_GroupID;
            }
        }
        public int IndexInGroup
        {
            set
            {
                m_IndexInGroup = value;
                m_IndexInGroup_ini = true;
            }
            get
            {
                return m_IndexInGroup;
            }
        }
        public int LenghtInGroup
        {
            set
            {
                m_LenghtInGroup = value;
                m_LenghtInGroup_ini = true;
            }
            get
            {
                return m_LenghtInGroup;
            }
        }
        public int ByteLenghtOfValue
        {
            set
            {
                m_ByteLenghtOfValue = value;
                m_ByteLenghtOfValue_ini = true;
            }
            get
            {
                return m_ByteLenghtOfValue;
            }
        }
        public bool UnSignData
        {
            set
            {
                m_UnSignData = value;
                m_UnSignData_ini = true;
            }
            get
            {
                return m_UnSignData;
            }
        }
        public int pViewMinValue
        {
            set
            {
                m_pViewMinValue = value;
                m_pViewMinValue_ini = true;
            }
            get
            {
                return m_pViewMinValue;
            }
        }
        public int pViewMaxValue
        {
            set
            {
                m_pViewMaxValue = value;
                m_pViewMaxValue_ini = true;
            }
            get
            {
                return m_pViewMaxValue;
            }
        }
        public int pADMinValue
        {
            set
            {
                m_pADMinValue = value;
                m_pADMinValue_ini = true;
            }
            get
            {
                return m_pADMinValue;
            }
        }
        public int pADMaxValue
        {
            set
            {
                m_pADMaxValue = value;
                m_pADMaxValue_ini = true;
            }
            get
            {
                return m_pADMaxValue;
            }
        }
        public bool CalibrationsVisible
        {
            set
            {
                m_CalibrationsVisible = value;
                m_CalibrationsVisible_ini = true;
            }
            get
            {
                return m_CalibrationsVisible;
            }
        }
        public bool IsShowValue
        {
            set
            {
                m_IsShowValue = value;
                m_IsShowValue_ini = true;
            }
            get
            {
                return m_IsShowValue;
            }
        }
        public bool AntipoleEnable
        {
            set
            {
                m_AntipoleEnable = value;
                m_AntipoleEnable_ini = true;
            }
            get
            {
                return m_AntipoleEnable;
            }
        }
        public string PenColor
        {
            set
            {
                m_PenColor = value;
                m_PenColor_ini = true;
            }
            get
            {
                return m_PenColor;
            }
        }
        public bool PixelEnable
        {
            set
            {
                m_PixelEnable = value;
                m_PixelEnable_ini = true;
            }
            get
            {
                return m_PixelEnable;
            }
        }
        public string strPixelRange
        {
            set
            {
                m_strPixelRange = value;
                m_strPixelRange_ini = true;
            }
            get
            {
                return m_strPixelRange;
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
            if (this.m_SignName_ini)
            {
                def = string.Format("{0}{2}{1}", def, "SignName", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_SignName, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_SensorType_ini)
            {
                def = string.Format("{0}{2}{1}", def, "SensorType", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_SensorType, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ViewMinValue_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ViewMinValue", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ViewMinValue, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ViewMaxValue_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ViewMaxValue", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ViewMaxValue, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ADMinValue_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ADMinValue", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ADMinValue, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ADMaxValue_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ADMaxValue", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ADMaxValue, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Unit_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Unit", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Unit, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_NeedConvert_ini)
            {
                def = string.Format("{0}{2}{1}", def, "NeedConvert", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_NeedConvert ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Enable_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Enable", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Enable ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_SpanTime_ini)
            {
                def = string.Format("{0}{2}{1}", def, "SpanTime", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_SpanTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ValueZoomRate_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ValueZoomRate", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ValueZoomRate, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_GroupID_ini)
            {
                def = string.Format("{0}{2}{1}", def, "GroupID", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_GroupID, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_IndexInGroup_ini)
            {
                def = string.Format("{0}{2}{1}", def, "IndexInGroup", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_IndexInGroup, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_LenghtInGroup_ini)
            {
                def = string.Format("{0}{2}{1}", def, "LenghtInGroup", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_LenghtInGroup, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ByteLenghtOfValue_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ByteLenghtOfValue", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ByteLenghtOfValue, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_UnSignData_ini)
            {
                def = string.Format("{0}{2}{1}", def, "UnSignData", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_UnSignData ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_pViewMinValue_ini)
            {
                def = string.Format("{0}{2}{1}", def, "pViewMinValue", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_pViewMinValue, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_pViewMaxValue_ini)
            {
                def = string.Format("{0}{2}{1}", def, "pViewMaxValue", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_pViewMaxValue, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_pADMinValue_ini)
            {
                def = string.Format("{0}{2}{1}", def, "pADMinValue", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_pADMinValue, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_pADMaxValue_ini)
            {
                def = string.Format("{0}{2}{1}", def, "pADMaxValue", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_pADMaxValue, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_CalibrationsVisible_ini)
            {
                def = string.Format("{0}{2}{1}", def, "CalibrationsVisible", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_CalibrationsVisible ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_IsShowValue_ini)
            {
                def = string.Format("{0}{2}{1}", def, "IsShowValue", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_IsShowValue ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_AntipoleEnable_ini)
            {
                def = string.Format("{0}{2}{1}", def, "AntipoleEnable", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_AntipoleEnable ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_PenColor_ini)
            {
                def = string.Format("{0}{2}{1}", def, "PenColor", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_PenColor, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_PixelEnable_ini)
            {
                def = string.Format("{0}{2}{1}", def, "PixelEnable", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_PixelEnable ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_strPixelRange_ini)
            {
                def = string.Format("{0}{2}{1}", def, "strPixelRange", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_strPixelRange, Mulitcase ? "," : "");
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
            if (m_SignName_ini)
            {
                returnstr = string.Format("{0}SignName='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_SignName);
                Mulitcase = true;
            }
            if (m_SensorType_ini)
            {
                returnstr = string.Format("{0}SensorType='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_SensorType);
                Mulitcase = true;
            }
            if (m_ViewMinValue_ini)
            {
                returnstr = string.Format("{0}ViewMinValue={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ViewMinValue);
                Mulitcase = true;
            }
            if (m_ViewMaxValue_ini)
            {
                returnstr = string.Format("{0}ViewMaxValue={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ViewMaxValue);
                Mulitcase = true;
            }
            if (m_ADMinValue_ini)
            {
                returnstr = string.Format("{0}ADMinValue={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ADMinValue);
                Mulitcase = true;
            }
            if (m_ADMaxValue_ini)
            {
                returnstr = string.Format("{0}ADMaxValue={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ADMaxValue);
                Mulitcase = true;
            }
            if (m_Unit_ini)
            {
                returnstr = string.Format("{0}Unit='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Unit);
                Mulitcase = true;
            }
            if (m_NeedConvert_ini)
            {
                returnstr = string.Format("{0}NeedConvert={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_NeedConvert ? 1 : 0);
                Mulitcase = true;
            }
            if (m_Enable_ini)
            {
                returnstr = string.Format("{0}Enable={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Enable ? 1 : 0);
                Mulitcase = true;
            }
            if (m_SpanTime_ini)
            {
                returnstr = string.Format("{0}SpanTime={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_SpanTime);
                Mulitcase = true;
            }
            if (m_ValueZoomRate_ini)
            {
                returnstr = string.Format("{0}ValueZoomRate={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ValueZoomRate);
                Mulitcase = true;
            }
            if (m_GroupID_ini)
            {
                returnstr = string.Format("{0}GroupID={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_GroupID);
                Mulitcase = true;
            }
            if (m_IndexInGroup_ini)
            {
                returnstr = string.Format("{0}IndexInGroup={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_IndexInGroup);
                Mulitcase = true;
            }
            if (m_LenghtInGroup_ini)
            {
                returnstr = string.Format("{0}LenghtInGroup={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_LenghtInGroup);
                Mulitcase = true;
            }
            if (m_ByteLenghtOfValue_ini)
            {
                returnstr = string.Format("{0}ByteLenghtOfValue={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ByteLenghtOfValue);
                Mulitcase = true;
            }
            if (m_UnSignData_ini)
            {
                returnstr = string.Format("{0}UnSignData={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_UnSignData ? 1 : 0);
                Mulitcase = true;
            }
            if (m_pViewMinValue_ini)
            {
                returnstr = string.Format("{0}pViewMinValue={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_pViewMinValue);
                Mulitcase = true;
            }
            if (m_pViewMaxValue_ini)
            {
                returnstr = string.Format("{0}pViewMaxValue={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_pViewMaxValue);
                Mulitcase = true;
            }
            if (m_pADMinValue_ini)
            {
                returnstr = string.Format("{0}pADMinValue={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_pADMinValue);
                Mulitcase = true;
            }
            if (m_pADMaxValue_ini)
            {
                returnstr = string.Format("{0}pADMaxValue={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_pADMaxValue);
                Mulitcase = true;
            }
            if (m_CalibrationsVisible_ini)
            {
                returnstr = string.Format("{0}CalibrationsVisible={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_CalibrationsVisible ? 1 : 0);
                Mulitcase = true;
            }
            if (m_IsShowValue_ini)
            {
                returnstr = string.Format("{0}IsShowValue={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_IsShowValue ? 1 : 0);
                Mulitcase = true;
            }
            if (m_AntipoleEnable_ini)
            {
                returnstr = string.Format("{0}AntipoleEnable={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_AntipoleEnable ? 1 : 0);
                Mulitcase = true;
            }
            if (m_PenColor_ini)
            {
                returnstr = string.Format("{0}PenColor='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_PenColor);
                Mulitcase = true;
            }
            if (m_PixelEnable_ini)
            {
                returnstr = string.Format("{0}PixelEnable={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_PixelEnable ? 1 : 0);
                Mulitcase = true;
            }
            if (m_strPixelRange_ini)
            {
                returnstr = string.Format("{0}strPixelRange='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_strPixelRange);
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
            if (m_SignName_ini)
            {
                def = string.Format("{0}{2}SignName='{1}'", def, m_SignName, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_SensorType_ini)
            {
                def = string.Format("{0}{2}SensorType='{1}'", def, m_SensorType, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ViewMinValue_ini)
            {
                def = string.Format("{0}{2}ViewMinValue={1}", def, m_ViewMinValue, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ViewMaxValue_ini)
            {
                def = string.Format("{0}{2}ViewMaxValue={1}", def, m_ViewMaxValue, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ADMinValue_ini)
            {
                def = string.Format("{0}{2}ADMinValue={1}", def, m_ADMinValue, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ADMaxValue_ini)
            {
                def = string.Format("{0}{2}ADMaxValue={1}", def, m_ADMaxValue, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Unit_ini)
            {
                def = string.Format("{0}{2}Unit='{1}'", def, m_Unit, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_NeedConvert_ini)
            {
                def = string.Format("{0}{2}NeedConvert={1}", def, m_NeedConvert ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Enable_ini)
            {
                def = string.Format("{0}{2}Enable={1}", def, m_Enable ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_SpanTime_ini)
            {
                def = string.Format("{0}{2}SpanTime={1}", def, m_SpanTime, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ValueZoomRate_ini)
            {
                def = string.Format("{0}{2}ValueZoomRate={1}", def, m_ValueZoomRate, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_GroupID_ini)
            {
                def = string.Format("{0}{2}GroupID={1}", def, m_GroupID, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_IndexInGroup_ini)
            {
                def = string.Format("{0}{2}IndexInGroup={1}", def, m_IndexInGroup, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_LenghtInGroup_ini)
            {
                def = string.Format("{0}{2}LenghtInGroup={1}", def, m_LenghtInGroup, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ByteLenghtOfValue_ini)
            {
                def = string.Format("{0}{2}ByteLenghtOfValue={1}", def, m_ByteLenghtOfValue, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_UnSignData_ini)
            {
                def = string.Format("{0}{2}UnSignData={1}", def, m_UnSignData ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_pViewMinValue_ini)
            {
                def = string.Format("{0}{2}pViewMinValue={1}", def, m_pViewMinValue, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_pViewMaxValue_ini)
            {
                def = string.Format("{0}{2}pViewMaxValue={1}", def, m_pViewMaxValue, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_pADMinValue_ini)
            {
                def = string.Format("{0}{2}pADMinValue={1}", def, m_pADMinValue, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_pADMaxValue_ini)
            {
                def = string.Format("{0}{2}pADMaxValue={1}", def, m_pADMaxValue, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_CalibrationsVisible_ini)
            {
                def = string.Format("{0}{2}CalibrationsVisible={1}", def, m_CalibrationsVisible ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_IsShowValue_ini)
            {
                def = string.Format("{0}{2}IsShowValue={1}", def, m_IsShowValue ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_AntipoleEnable_ini)
            {
                def = string.Format("{0}{2}AntipoleEnable={1}", def, m_AntipoleEnable ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_PenColor_ini)
            {
                def = string.Format("{0}{2}PenColor='{1}'", def, m_PenColor, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_PixelEnable_ini)
            {
                def = string.Format("{0}{2}PixelEnable={1}", def, m_PixelEnable ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_strPixelRange_ini)
            {
                def = string.Format("{0}{2}strPixelRange='{1}'", def, m_strPixelRange, Mulitcase ? "," : "");
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
            if (!Mulitcase)
            {
                return ("");
            }
            return (def);
        }
        public string GetThisTableName()
        {
            return ("Channel");
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
                case "SignName":
                    SignName = Value.ToString(); ;
                    break;
                case "SensorType":
                    SensorType = Value.ToString(); ;
                    break;
                case "ViewMinValue":
                    ViewMinValue = Convert.ToSingle(Value);
                    break;
                case "ViewMaxValue":
                    ViewMaxValue = Convert.ToSingle(Value);
                    break;
                case "ADMinValue":
                    ADMinValue = Convert.ToSingle(Value);
                    break;
                case "ADMaxValue":
                    ADMaxValue = Convert.ToSingle(Value);
                    break;
                case "Unit":
                    Unit = Value.ToString(); ;
                    break;
                case "NeedConvert":
                    NeedConvert = Convert.ToBoolean(Value);
                    break;
                case "Enable":
                    Enable = Convert.ToBoolean(Value);
                    break;
                case "SpanTime":
                    SpanTime = Convert.ToInt32(Value);
                    break;
                case "ValueZoomRate":
                    ValueZoomRate = Convert.ToInt32(Value);
                    break;
                case "GroupID":
                    GroupID = Convert.ToInt32(Value);
                    break;
                case "IndexInGroup":
                    IndexInGroup = Convert.ToInt32(Value);
                    break;
                case "LenghtInGroup":
                    LenghtInGroup = Convert.ToInt32(Value);
                    break;
                case "ByteLenghtOfValue":
                    ByteLenghtOfValue = Convert.ToInt32(Value);
                    break;
                case "UnSignData":
                    UnSignData = Convert.ToBoolean(Value);
                    break;
                case "pViewMinValue":
                    pViewMinValue = Convert.ToInt32(Value);
                    break;
                case "pViewMaxValue":
                    pViewMaxValue = Convert.ToInt32(Value);
                    break;
                case "pADMinValue":
                    pADMinValue = Convert.ToInt32(Value);
                    break;
                case "pADMaxValue":
                    pADMaxValue = Convert.ToInt32(Value);
                    break;
                case "CalibrationsVisible":
                    CalibrationsVisible = Convert.ToBoolean(Value);
                    break;
                case "IsShowValue":
                    IsShowValue = Convert.ToBoolean(Value);
                    break;
                case "AntipoleEnable":
                    AntipoleEnable = Convert.ToBoolean(Value);
                    break;
                case "PenColor":
                    PenColor = Value.ToString(); ;
                    break;
                case "PixelEnable":
                    PixelEnable = Convert.ToBoolean(Value);
                    break;
                case "strPixelRange":
                    strPixelRange = Value.ToString(); ;
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
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_Channel());
        }
        #endregion
        private string[] m_strPixelRangArray = null;
        /// <summary>
        /// 获取灵敏度范围取值表
        /// </summary>
        public string[] strPixelRangArray
        {
            get
            {
                if (m_strPixelRangArray == null)
                    m_strPixelRangArray = m_strPixelRange.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                return m_strPixelRangArray;
            }
        }
        /// <summary>
        /// c存储灵敏度范围取值表
        /// </summary>
        public int[] intPixelRangArray
        {
            set;
            get;
        }
        /// <summary>
        /// 获取默认颜色
        /// </summary>
        public Color Color
        {
            get
            {
                int result = 0;
                if (m_PenColor == "")
                {
                    m_PenColor = "Black";
                }
                Color pencolor = Color.Black;
                if (int.TryParse(m_PenColor, System.Globalization.NumberStyles.HexNumber, null, out result))
                    pencolor = Color.FromArgb(result);
                else
                    pencolor = Color.FromName(m_PenColor);
                return pencolor;
            }
        }
    }
}
