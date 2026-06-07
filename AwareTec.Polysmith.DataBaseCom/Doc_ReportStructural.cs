using pSystem.Interface.Util;
using System;

namespace AwareTec.Polysmith.DataBaseCom
{
    /// <summary>
    ///  结构图元素定义类
    /// </summary>
    public class Doc_ReportStructural : ITable
    {
        #region 私有成员
        private int m_ID = 0;
        private bool m_ID_ini = false;
        private string m_Name = "";
        private bool m_Name_ini = false;
        private string m_Description = "";
        private bool m_Description_ini = false;
        private int m_Interval = 0;
        private bool m_Interval_ini = false;
        private int m_MaxValue = 0;
        private bool m_MaxValue_ini = false;
        private int m_MinValue = 0;
        private bool m_MinValue_ini = false;
        private int m_Height = 0;
        private bool m_Height_ini = false;
        private int m_Width = 0;
        private bool m_Width_ini = false;
        private string m_PenColor = "";
        private bool m_PenColor_ini = false;
        private string m_LegendLables = "";
        private bool m_LegendLables_ini = false;
        private string m_CalibrationsColors = "";
        private bool m_CalibrationsColors_ini = false;
        private bool m_RangeCanChanged = false;
        private bool m_RangeCanChanged_ini = false;
        private bool m_ShowTimeLables = false;
        private bool m_ShowTimeLables_ini = false;
        private int m_ChartStyle = 0;
        private bool m_ChartStyle_ini = false;
        private string m_SavePath = "";
        private bool m_SavePath_ini = false;
        private string m_DataSourceName = "";
        private bool m_DataSourceName_ini = false;
        private bool m_Visible = false;
        private bool m_Visible_ini = false;
        private int m_Index = 0;
        private bool m_Index_ini = false;
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
        public int Interval
        {
            set
            {
                m_Interval = value;
                m_Interval_ini = true;
            }
            get
            {
                return m_Interval;
            }
        }
        public int MaxValue
        {
            set
            {
                m_MaxValue = value;
                m_MaxValue_ini = true;
            }
            get
            {
                return m_MaxValue;
            }
        }
        public int MinValue
        {
            set
            {
                m_MinValue = value;
                m_MinValue_ini = true;
            }
            get
            {
                return m_MinValue;
            }
        }
        public int Height
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
        public int Width
        {
            set
            {
                m_Width = value;
                m_Width_ini = true;
            }
            get
            {
                return m_Width;
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
        public string LegendLables
        {
            set
            {
                m_LegendLables = value;
                m_LegendLables_ini = true;
            }
            get
            {
                return m_LegendLables;
            }
        }
        public string CalibrationsColors
        {
            set
            {
                m_CalibrationsColors = value;
                m_CalibrationsColors_ini = true;
            }
            get
            {
                return m_CalibrationsColors;
            }
        }
        public bool RangeCanChanged
        {
            set
            {
                m_RangeCanChanged = value;
                m_RangeCanChanged_ini = true;
            }
            get
            {
                return m_RangeCanChanged;
            }
        }
        public bool ShowTimeLables
        {
            set
            {
                m_ShowTimeLables = value;
                m_ShowTimeLables_ini = true;
            }
            get
            {
                return m_ShowTimeLables;
            }
        }
        public int ChartStyle
        {
            set
            {
                m_ChartStyle = value;
                m_ChartStyle_ini = true;
            }
            get
            {
                return m_ChartStyle;
            }
        }
        public string SavePath
        {
            set
            {
                m_SavePath = value;
                m_SavePath_ini = true;
            }
            get
            {
                return m_SavePath;
            }
        }
        public string DataSourceName
        {
            set
            {
                m_DataSourceName = value;
                m_DataSourceName_ini = true;
            }
            get
            {
                return m_DataSourceName;
            }
        }
        public bool Visible
        {
            set
            {
                m_Visible = value;
                m_Visible_ini = true;
            }
            get
            {
                return m_Visible;
            }
        }
        public int Index
        {
            set
            {
                m_Index = value;
                m_Index_ini = true;
            }
            get
            {
                return m_Index;
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
            if (this.m_Interval_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Interval", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Interval, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MaxValue_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MaxValue", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_MaxValue, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MinValue_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MinValue", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_MinValue, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Height_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Height", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Height, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Width_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Width", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Width, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_PenColor_ini)
            {
                def = string.Format("{0}{2}{1}", def, "PenColor", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_PenColor, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_LegendLables_ini)
            {
                def = string.Format("{0}{2}{1}", def, "LegendLables", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_LegendLables, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_CalibrationsColors_ini)
            {
                def = string.Format("{0}{2}{1}", def, "CalibrationsColors", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_CalibrationsColors, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_RangeCanChanged_ini)
            {
                def = string.Format("{0}{2}{1}", def, "RangeCanChanged", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_RangeCanChanged ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ShowTimeLables_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ShowTimeLables", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ShowTimeLables ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_ChartStyle_ini)
            {
                def = string.Format("{0}{2}{1}", def, "ChartStyle", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_ChartStyle, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_SavePath_ini)
            {
                def = string.Format("{0}{2}{1}", def, "SavePath", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_SavePath, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_DataSourceName_ini)
            {
                def = string.Format("{0}{2}{1}", def, "DataSourceName", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_DataSourceName, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Visible_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Visible", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Visible ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Index_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Index", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Index, Mulitcase ? "," : "");
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
            if (m_Interval_ini)
            {
                returnstr = string.Format("{0}Interval={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Interval);
                Mulitcase = true;
            }
            if (m_MaxValue_ini)
            {
                returnstr = string.Format("{0}MaxValue={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MaxValue);
                Mulitcase = true;
            }
            if (m_MinValue_ini)
            {
                returnstr = string.Format("{0}MinValue={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MinValue);
                Mulitcase = true;
            }
            if (m_Height_ini)
            {
                returnstr = string.Format("{0}Height={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Height);
                Mulitcase = true;
            }
            if (m_Width_ini)
            {
                returnstr = string.Format("{0}Width={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Width);
                Mulitcase = true;
            }
            if (m_PenColor_ini)
            {
                returnstr = string.Format("{0}PenColor='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_PenColor);
                Mulitcase = true;
            }
            if (m_LegendLables_ini)
            {
                returnstr = string.Format("{0}LegendLables='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_LegendLables);
                Mulitcase = true;
            }
            if (m_CalibrationsColors_ini)
            {
                returnstr = string.Format("{0}CalibrationsColors='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_CalibrationsColors);
                Mulitcase = true;
            }
            if (m_RangeCanChanged_ini)
            {
                returnstr = string.Format("{0}RangeCanChanged={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_RangeCanChanged ? 1 : 0);
                Mulitcase = true;
            }
            if (m_ShowTimeLables_ini)
            {
                returnstr = string.Format("{0}ShowTimeLables={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ShowTimeLables ? 1 : 0);
                Mulitcase = true;
            }
            if (m_ChartStyle_ini)
            {
                returnstr = string.Format("{0}ChartStyle={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_ChartStyle);
                Mulitcase = true;
            }
            if (m_SavePath_ini)
            {
                returnstr = string.Format("{0}SavePath='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_SavePath);
                Mulitcase = true;
            }
            if (m_DataSourceName_ini)
            {
                returnstr = string.Format("{0}DataSourceName='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_DataSourceName);
                Mulitcase = true;
            }
            if (m_Visible_ini)
            {
                returnstr = string.Format("{0}Visible={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Visible ? 1 : 0);
                Mulitcase = true;
            }
            if (m_Index_ini)
            {
                returnstr = string.Format("{0}Index={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Index);
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
            if (m_Interval_ini)
            {
                def = string.Format("{0}{2}Interval={1}", def, m_Interval, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MaxValue_ini)
            {
                def = string.Format("{0}{2}MaxValue={1}", def, m_MaxValue, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MinValue_ini)
            {
                def = string.Format("{0}{2}MinValue={1}", def, m_MinValue, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Height_ini)
            {
                def = string.Format("{0}{2}Height={1}", def, m_Height, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Width_ini)
            {
                def = string.Format("{0}{2}Width={1}", def, m_Width, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_PenColor_ini)
            {
                def = string.Format("{0}{2}PenColor='{1}'", def, m_PenColor, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_LegendLables_ini)
            {
                def = string.Format("{0}{2}LegendLables='{1}'", def, m_LegendLables, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_CalibrationsColors_ini)
            {
                def = string.Format("{0}{2}CalibrationsColors='{1}'", def, m_CalibrationsColors, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_RangeCanChanged_ini)
            {
                def = string.Format("{0}{2}RangeCanChanged={1}", def, m_RangeCanChanged ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ShowTimeLables_ini)
            {
                def = string.Format("{0}{2}ShowTimeLables={1}", def, m_ShowTimeLables ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_ChartStyle_ini)
            {
                def = string.Format("{0}{2}ChartStyle={1}", def, m_ChartStyle, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_SavePath_ini)
            {
                def = string.Format("{0}{2}SavePath='{1}'", def, m_SavePath, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_DataSourceName_ini)
            {
                def = string.Format("{0}{2}DataSourceName='{1}'", def, m_DataSourceName, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Visible_ini)
            {
                def = string.Format("{0}{2}Visible={1}", def, m_Visible ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Index_ini)
            {
                def = string.Format("{0}{2}Index={1}", def, m_Index, Mulitcase ? "," : "");
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
            return ("newReportStructural2");
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
                case "Interval":
                    Interval = Convert.ToInt32(Value);
                    break;
                case "MaxValue":
                    MaxValue = Convert.ToInt32(Value);
                    break;
                case "MinValue":
                    MinValue = Convert.ToInt32(Value);
                    break;
                case "Height":
                    Height = Convert.ToInt32(Value);
                    break;
                case "Width":
                    Width = Convert.ToInt32(Value);
                    break;
                case "PenColor":
                    PenColor = Value.ToString(); ;
                    break;
                case "LegendLables":
                    LegendLables = Value.ToString(); ;
                    break;
                case "CalibrationsColors":
                    CalibrationsColors = Value.ToString(); ;
                    break;
                case "RangeCanChanged":
                    RangeCanChanged = Convert.ToBoolean(Value);
                    break;
                case "ShowTimeLables":
                    ShowTimeLables = Convert.ToBoolean(Value);
                    break;
                case "ChartStyle":
                    ChartStyle = Convert.ToInt32(Value);
                    break;
                case "SavePath":
                    SavePath = Value.ToString(); ;
                    break;
                case "DataSourceName":
                    DataSourceName = Value.ToString(); ;
                    break;
                case "Visible":
                    Visible = Convert.ToBoolean(Value);
                    break;
                case "Index":
                    Index = Convert.ToInt32(Value);
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_ReportStructural());
        }
        #endregion
    }
}
