using AwareTec.Polysmith.Cloud.UI.ViewHelpers;
using AwareTec.Polysmith.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.DataModels
{
    /// <summary>
    /// 与通道配置表匹配的类
    /// </summary>
    public class ChannelTable : PredefinedChannel
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        public string ChannelID { set; get; }
        /// <summary>
        /// 显示通道名
        /// </summary>
        public string strName { set; get; }
        /// <summary>
        /// 灵敏度
        /// </summary>
        public float Sensitivity { set; get; }
        /// <summary>
        /// 最大值
        /// </summary>
        public float MaxValue { set; get; }
        /// <summary>
        /// 最大值
        /// </summary>
        public float MinValue { set; get; }
        /// <summary>
        /// 通道颜色
        /// </summary>
        public Color ColorSelect { set; get; }
        /// <summary>
        /// 高通滤波器类型
        /// </summary>
        public string HighPass { set; get; }
        /// <summary>
        /// 低通滤波器类型
        /// </summary>
        public string LowPass { set; get; }
        /// <summary>
        /// 陷波器类型
        /// </summary>
        public string SingleNotch { set; get; }
        /// <summary>
        /// 反极性
        /// </summary>
        public bool Antipole { set; get; }
        /// <summary>
        /// 通道可见性
        /// </summary>
        public bool State { set; get; }
        /// <summary>
        /// 通道排序序号
        /// </summary>
        public int Index { set; get; }
        /// <summary>
        /// 备注信息 0:30 （0表示是主屏 1表示副屏；30表示时基）
        /// </summary>
        public string Reserve { set; get; }

        /// <summary>
        /// 75微伏基线可见性
        /// </summary>
        public bool DBaseLineVisible { set; get; }

        /// <summary>
        /// 是否是克隆通道
        /// </summary>
        public bool IsClone { set; get; }

        /// <summary>
        /// c存储灵敏度范围取值表
        /// </summary>
        public int[] intPixelRangArray
        {
            set;
            get;
        }

        private string[] m_strPixelRangArray = new string[0];
        /// <summary>
        /// 获取灵敏度范围取值表
        /// </summary>
        public string[] strPixelRangArray
        {
            get
            {
                if (m_strPixelRangArray.Length == 0)
                    m_strPixelRangArray = strPixelRange.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                return m_strPixelRangArray;
            }
        }

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public ChannelTable Clone()
        {
            ChannelTable table = creatTable(this);
            //用户可设置属性
            table.strName = this.strName;
            table.Sensitivity = this.Sensitivity;
            table.MaxValue = this.MaxValue;
            table.MinValue = this.MinValue;
            table.ColorSelect = this.ColorSelect;
            table.HighPass = this.HighPass;
            table.LowPass = this.LowPass;
            table.SingleNotch = this.SingleNotch;
            table.Antipole = this.Antipole;
            table.State = this.State;
            table.Index = this.Index;
            table.Reserve = this.Reserve;
            table.DBaseLineVisible = this.DBaseLineVisible;
            table.IsClone = this.IsClone;
            table.ChannelID = this.ChannelID;
            return table;
        }
        /// <summary>
        /// 一行通道数据转成通道配置类
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static ChannelTable ConvertToChannel(DataRow dr)
        {
            //获取ID
            int ID = Convert.ToInt32(dr["ID"]);
            ChannelTable table = creatTable(Global.GlobalSingleton.Instance.PredefinedData.PredefinedChannels.Find(t => t.ID == ID));
            //用户可设置属性
            table.strName = dr["strName"].ToString();
            table.Sensitivity = getSensitivity(dr["Sensitivity"]);
            table.MaxValue = Convert.ToInt32(dr["MaxValue"]);
            table.MinValue = Convert.ToInt32(dr["MinValue"]);
            table.ColorSelect = (Color)dr["ColorSelect"];
            table.HighPass = dr["HighPass"].ToString();
            table.LowPass = dr["LowPass"].ToString();
            table.SingleNotch = dr["SingleNotch"].ToString();
            table.Antipole = Convert.ToBoolean(dr["Antipole"]);
            table.State = Convert.ToBoolean(dr["State"]);
            table.Index = Convert.ToByte(dr["Index"]);
            table.Reserve = dr["Reserve"].ToString();
            table.DBaseLineVisible = Convert.ToBoolean(dr["DBaseLineVisible"]);
            table.IsClone = Convert.ToBoolean(dr["IsClone"]);
            table.ChannelID = dr["ChannelID"].ToString();
            return table;
        }
        /// <summary>
        /// 通道配置类转成一行表单数据
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static DataRow ConvertToDataRow(ChannelTable table, DataTable materTable = null)
        {

            DataRow dr = materTable == null ? ChannelManageCloud.Default.CreatEmptyTable().NewRow() : materTable.NewRow();
            //用户可设置属性
            dr["strName"] = table.strName;
            dr["Sensitivity"] = setSensitivity(table.Sensitivity, table.Unit == "Pa");
            dr["MaxValue"] = table.MaxValue;
            dr["MinValue"] = table.MinValue;
            dr["ColorSelect"]= table.ColorSelect;
            dr["HighPass"]= table.HighPass;
            dr["LowPass"] = table.LowPass;
            dr["SingleNotch"] = table.SingleNotch;
            dr["Antipole"] = table.Antipole;
            dr["State"] = table.State;
            dr["Index"] = table.Index;
            dr["Reserve"] = table.Reserve;
            dr["DBaseLineVisible"] = table.DBaseLineVisible;
            dr["IsClone"] = table.IsClone;
            dr["ID"] = table.ID;
            dr["ChannelID"] = table.ChannelID;
            return dr;
        }
        /// <summary>
        /// 获取灵敏度值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int getSensitivity(object obj)
        {
            int ret = 0;
            if (obj != null)
            {
                string ss = obj.ToString();
                if (!string.IsNullOrEmpty(ss))
                {
                    string[] sss = ss.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (sss.Length == 1)
                    {
                        if (sss[0] == "Off")
                            return 0;
                    }
                    else if (sss.Length == 2)
                    {
                        if (sss[0] == "Off")
                            return 0;
                        ret = Convert.ToInt32(sss[0]);
                        int rate = 1;
                        if (sss[1] == "mV/mm")
                            rate = 1000;
                        else if (sss[1] == "V/mm")
                            rate = 1000000;
                        else if (sss[1] == "hPa/mm")
                            rate = 100;
                        else if (sss[1] == "KPa/mm")
                            rate = 1000;
                        ret *= rate;
                        return ret;
                    }
                }
            }
            return ret;
        }
        /// <summary>
        /// 设置灵敏度字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string setSensitivity(float value, bool ispressure = false)
        {
            string strValue = "";
            string unit = "μV/mm";
            if (value == 0)
                return "Off";
            if (!ispressure)
            {
                if (value < 1000)
                {
                    strValue = value.ToString();
                }
                else if (value >= 1000 && value < 1000000)
                {
                    strValue = (value / 1000).ToString();
                    unit = "mV/mm";
                }
                else
                {
                    strValue = (value / 1000000).ToString();
                    unit = "V/mm";
                }
            }
            else
            {
                if (value < 100)
                {
                    unit = "Pa/mm";
                    strValue = value.ToString();
                }
                else if (value < 10000)
                {
                    unit = "hPa/mm";
                    strValue = (value / 100).ToString();
                }
                else
                {
                    unit = "KPa/mm";
                    strValue = (value / 1000).ToString();
                }
            }
            return string.Format("{0} {1}", strValue, unit);
        }
        /// <summary>
        /// 父类衍生一个子类table
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        private static ChannelTable creatTable(PredefinedChannel channel)
        {
            ChannelTable ret = new ChannelTable();
            if (channel != null)
            {
                //系统属性
                ret.ID = channel.ID;
                ret.Enable = channel.Enable;
                ret.SignName = channel.SignName;
                ret.SensorType = channel.SensorType;
                ret.IsShowValue = channel.IsShowValue;
                ret.ViewMinValue = channel.ViewMinValue;
                ret.ViewMaxValue = channel.ViewMaxValue;
                ret.NeedConvert = channel.NeedConvert;
                ret.ADMaxValue = channel.ADMaxValue;
                ret.ADMinValue = channel.ADMinValue;
                ret.SpanTime = channel.SpanTime;
                ret.GroupID = channel.GroupID;
                ret.Unit = channel.Unit;
                ret.IndexInGroup = channel.IndexInGroup;
                ret.LenghtInGroup = channel.LenghtInGroup;
                ret.ByteLenghtOfValue = channel.ByteLenghtOfValue;
                ret.UnSignData = channel.UnSignData;
                ret.IsShowValue = channel.IsShowValue;
                ret.CalibrationsVisible = channel.CalibrationsVisible;
                ret.PixelEnable = channel.PixelEnable;
                ret.strPixelRange = channel.strPixelRange;
                int len = ret.strPixelRangArray.Length;
                ret.intPixelRangArray = new int[len];
                for (int j = 0; j < len; j++)
                {
                    ret.intPixelRangArray[j] = getSensitivity(ret.strPixelRangArray[j]);
                }
            }
            return ret;
        }

    }

}
