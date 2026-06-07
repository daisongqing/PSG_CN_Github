using pSystem.Interface.Util;
using System;

namespace AwareTec.Polysmith.DataBaseCom
{
    /// <summary>
    /// 事件标记自定义
    /// </summary>
    public class Doc_EventsDefine : ITable
    {
        #region 私有成员
        private int m_ID = 0;
        private bool m_ID_ini = false;
        private string m_Name = "";
        private bool m_Name_ini = false;
        private string m_Description = "";
        private bool m_Description_ini = false;
        private int m_Flag = 0;
        private bool m_Flag_ini = false;
        private string m_BackColor = "";
        private bool m_BackColor_ini = false;
        private string m_Comments = "";
        private bool m_Comments_ini = false;
        private int m_MarkTyp = 0;
        private bool m_MarkTyp_ini = false;
        private bool m_AllowDelete = false;
        private bool m_AllowDelete_ini = false;
        private string m_AllowChannel = "";
        private bool m_AllowChannel_ini = false;
        private string m_Reserve1 = "";
        private bool m_Reserve1_ini = false;
        private string m_Reserve2 = "";
        private bool m_Reserve2_ini = false;
        private string m_Reserve3 = "";
        private bool m_Reserve3_ini = false;
        private string m_Reserve4 = "";
        private bool m_Reserve4_ini = false;
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
        public int Flag
        {
            set
            {
                m_Flag = value;
                m_Flag_ini = true;
            }
            get
            {
                return m_Flag;
            }
        }
        public string BackColor
        {
            set
            {
                m_BackColor = value;
                m_BackColor_ini = true;
            }
            get
            {
                return m_BackColor;
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
        public int MarkTyp
        {
            set
            {
                m_MarkTyp = value;
                m_MarkTyp_ini = true;
            }
            get
            {
                return m_MarkTyp;
            }
        }
        public bool AllowDelete
        {
            set
            {
                m_AllowDelete = value;
                m_AllowDelete_ini = true;
            }
            get
            {
                return m_AllowDelete;
            }
        }
        public string AllowChannel
        {
            set
            {
                m_AllowChannel = value;
                m_AllowChannel_ini = true;
            }
            get
            {
                return m_AllowChannel;
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
            if (this.m_Flag_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Flag", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_Flag, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_BackColor_ini)
            {
                def = string.Format("{0}{2}{1}", def, "BackColor", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_BackColor, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_Comments_ini)
            {
                def = string.Format("{0}{2}{1}", def, "Comments", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_Comments, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_MarkTyp_ini)
            {
                def = string.Format("{0}{2}{1}", def, "MarkTyp", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_MarkTyp, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_AllowDelete_ini)
            {
                def = string.Format("{0}{2}{1}", def, "AllowDelete", Mulitcase ? "," : "");
                val = string.Format("{0}{2}{1}", val, m_AllowDelete ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (this.m_AllowChannel_ini)
            {
                def = string.Format("{0}{2}{1}", def, "AllowChannel", Mulitcase ? "," : "");
                val = string.Format("{0}{2}'{1}'", val, m_AllowChannel, Mulitcase ? "," : "");
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
            if (m_Flag_ini)
            {
                returnstr = string.Format("{0}Flag={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Flag);
                Mulitcase = true;
            }
            if (m_BackColor_ini)
            {
                returnstr = string.Format("{0}BackColor='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_BackColor);
                Mulitcase = true;
            }
            if (m_Comments_ini)
            {
                returnstr = string.Format("{0}Comments='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_Comments);
                Mulitcase = true;
            }
            if (m_MarkTyp_ini)
            {
                returnstr = string.Format("{0}MarkTyp={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_MarkTyp);
                Mulitcase = true;
            }
            if (m_AllowDelete_ini)
            {
                returnstr = string.Format("{0}AllowDelete={1}", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_AllowDelete ? 1 : 0);
                Mulitcase = true;
            }
            if (m_AllowChannel_ini)
            {
                returnstr = string.Format("{0}AllowChannel='{1}'", Mulitcase ? string.Format("{0} AND ", returnstr) : "", m_AllowChannel);
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
            if (m_Flag_ini)
            {
                def = string.Format("{0}{2}Flag={1}", def, m_Flag, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_BackColor_ini)
            {
                def = string.Format("{0}{2}BackColor='{1}'", def, m_BackColor, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_Comments_ini)
            {
                def = string.Format("{0}{2}Comments='{1}'", def, m_Comments, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_MarkTyp_ini)
            {
                def = string.Format("{0}{2}MarkTyp={1}", def, m_MarkTyp, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_AllowDelete_ini)
            {
                def = string.Format("{0}{2}AllowDelete={1}", def, m_AllowDelete ? 1 : 0, Mulitcase ? "," : "");
                Mulitcase = true;
            }
            if (m_AllowChannel_ini)
            {
                def = string.Format("{0}{2}AllowChannel='{1}'", def, m_AllowChannel, Mulitcase ? "," : "");
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
            return ("EventsDefine");
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
                case "Flag":
                    Flag = Convert.ToInt32(Value);
                    break;
                case "BackColor":
                    BackColor = Value.ToString(); ;
                    break;
                case "Comments":
                    Comments = Value.ToString(); ;
                    break;
                case "MarkTyp":
                    MarkTyp = Convert.ToInt32(Value);
                    break;
                case "AllowDelete":
                    AllowDelete = Convert.ToBoolean(Value);
                    break;
                case "AllowChannel":
                    AllowChannel = Value.ToString(); ;
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
                case "ModeType":
                    ModeType = Convert.ToInt32(Value);
                    break;
            }
        }
        public ITable GetNewInstance()
        {
            return (new Doc_EventsDefine());
        }
        #endregion

        public object Clone()
        {
            return new Doc_EventsDefine()
            {
                Name = this.Name,
                Description = this.Description,
                Flag = this.Flag,
                BackColor = this.BackColor,
                Comments = this.Comments,
                MarkTyp = this.MarkTyp,
                AllowDelete = this.AllowDelete,
                AllowChannel = this.AllowChannel,
                Reserve1 = this.Reserve1,
                Reserve2 = this.Reserve2,
                Reserve3 = this.Reserve3,
                Reserve4 = this.Reserve4,
            };
        }
    }
}
