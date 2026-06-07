using AwareTec.Polysmith.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.UI
{
    /// <summary>
    /// 与通道配置表匹配的类
    /// </summary>
    public class ChannelTable
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { set; get; }
        /// <summary>
        /// 通道编码
        /// </summary>
        public int ChannelNum { set; get; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 可见性
        /// </summary>
        public bool Visible { set; get; }
        /// <summary>
        /// 最大值
        /// </summary>
        public float MaxValue { set; get; }
        /// <summary>
        /// 最大值
        /// </summary>
        public float MinValue { set; get; }
        /// <summary>
        /// 采样周期
        /// </summary>
        public int TimeSpan { set; get; }
        /// <summary>
        /// 放大倍数
        /// </summary>
        public float ValueZoomRate { set; get; }
        /// <summary>
        /// 画笔颜色
        /// </summary>
        public Color PenColor { set; get; }
        /// <summary>
        /// 通道序号
        /// </summary>
        public int ChannelNo { set; get; }
        /// <summary>
        /// 陷波器类型
        /// </summary>
        public string SingleNotch { set; get; }
        /// <summary>
        /// 高通滤波器类型
        /// </summary>
        public string HighPass { set; get; }
        /// <summary>
        /// 低通滤波器类型
        /// </summary>
        public string LowPass { set; get; }
        /// <summary>
        /// 灵敏度使能标志
        /// </summary>
        public bool PixelEnable { set; get; }
        /// <summary>
        /// 显示值标志
        /// </summary>
        public bool IsShowValue { set; get; }
        /// <summary>
        /// 通道刻度显示标志
        /// </summary>
        public bool CalibrationsVisible { set; get; }
        private bool m_DBaseLineVisible = false;
        /// <summary>
        /// 75μv基线显示
        /// </summary>
        public bool DBaseLineVisible { set { m_DBaseLineVisible = value; } get { return m_DBaseLineVisible; } }
        private bool m_Antipole = false;
        /// <summary>
        /// 反极性
        /// </summary>
        public bool Antipole { set { m_Antipole = value; } get { return m_Antipole; } }
        /// <summary>
        /// 灵敏度
        /// </summary>
        public float PixelRate { set; get; }
        /// <summary>
        /// 是否是派生通道
        /// </summary>
        private bool m_isClone = false;
        /// <summary>
        /// 是否是派生通道
        /// </summary>
        public bool IsClone { set { m_isClone = value; } get { return m_isClone; } }
        /// <summary>
        /// 滤波器载体
        /// </summary>
        public object Tag { set; get; }
        /// <summary>
        /// d单位
        /// </summary>
        public string Unit { set; get; }
        /// <summary>
        /// 是否最大最小值生效
        /// </summary>
        public bool MaxMinValueEnable = false;
        private string m_Reserve = "0:30";
        /// <summary>
        /// 备用字段
        /// </summary>
        public string Reserve
        {
            set
            {
                if (value != "")
                {
                    m_Reserve = value;
                    string[] ss = value.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    Belong = int.Parse(ss[0]);
                }
                else
                {
                    Belong = 0;
                }
            }
            get { return m_Reserve; }
        }
        private int m_belong = 0;
        /// <summary>
        /// 
        /// </summary>
        public int Belong
        {
            private set { m_belong = value; }
            get { return m_belong; }
        }
        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public ChannelTable Clone()
        {
            ChannelTable table = new ChannelTable();
            table.ID = this.ID;
            table.ChannelNum = this.ChannelNum;
            table.ChannelNo = this.ChannelNo;
            table.HighPass = this.HighPass;
            table.IsClone = this.IsClone;
            table.LowPass = this.LowPass;
            table.MaxValue = this.MaxValue;
            table.MinValue = this.MinValue;
            table.Name = this.Name;
            table.PenColor = this.PenColor;
            table.PixelEnable = this.PixelEnable;
            table.PixelRate = this.PixelRate;
            table.SingleNotch = this.SingleNotch;
            table.TimeSpan = this.TimeSpan;
            table.ValueZoomRate = this.ValueZoomRate;
            table.Visible = this.Visible;
            table.Reserve = this.Reserve;
            table.Antipole = this.Antipole;
            table.IsShowValue = this.IsShowValue;
            table.CalibrationsVisible = this.CalibrationsVisible;
            table.Unit = this.Unit;
            table.MaxMinValueEnable = this.MaxMinValueEnable;
            table.DBaseLineVisible = this.DBaseLineVisible;
            return table;
        }
        /// <summary>
        /// 一行通道数据转成通道配置类
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static ChannelTable ConvertToChannel(DataRow dr)
        {
            ChannelTable table = new ChannelTable();
            table.Visible = Convert.ToBoolean(dr["State"]);
            table.MaxValue = Convert.ToInt32(dr["MaxValue"]);
            table.MinValue = Convert.ToInt32(dr["MinValue"]);
            table.TimeSpan = Convert.ToInt32(dr["TimeSpan"]);
            table.ChannelNo = Convert.ToByte(dr["Index"]);
            table.ValueZoomRate = Convert.ToByte(dr["ZoomRate"]);
            table.PenColor = (Color)dr["ColorSelect"];
            table.Name = dr["strName"].ToString();
            table.SingleNotch = dr["SingleNotch"].ToString();
            table.HighPass = dr["HighPass"].ToString();
            table.LowPass = dr["LowPass"].ToString();
            table.Reserve = dr["Reserve"].ToString();
            table.IsShowValue = Convert.ToBoolean(dr["IsShowValue"]);
            table.CalibrationsVisible = Convert.ToBoolean(dr["CalibrationsVisible"]);
            table.Antipole = Convert.ToBoolean(dr["Antipole"]);
            table.ID = dr["ID"].ToString();
            table.PixelRate = getSensitivity(dr["Sensitivity"]);
            table.PixelEnable = bool.Parse(dr["PixelEnable"].ToString());
            DataBaseCom.Doc_Channel find= Channel.Default.ChannelProperties.Find(t => t.Name == table.ID);
            if (find != null)
            {
                table.ChannelNum = find.ID;
                table.Unit = find.Unit;
                table.IsShowValue = find.IsShowValue;
                table.CalibrationsVisible = find.CalibrationsVisible;
                table.ValueZoomRate = find.ValueZoomRate;
            }
            else
            {
                table.IsClone = true;
            }
            return table;
        }
        /// <summary>
        /// 通道配置类转成一行表单数据
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static DataRow ConvertToDataRow(ChannelTable table,DataTable materTable=null)
        {
            DataRow dr = materTable == null ? ChannelManage.Default.CreatEmptyTable().NewRow() : materTable.NewRow();
            dr["ID"] = table.ID;
            dr["State"] = table.Visible;
            dr["MaxValue"] = table.MaxValue;
            dr["MinValue"] = table.MinValue;
            dr["TimeSpan"] = table.TimeSpan;
            dr["Index"] = table.ChannelNo;
            dr["ZoomRate"] = table.ValueZoomRate;
            dr["ColorSelect"] = table.PenColor;
            dr["strName"] = table.Name;
            dr["SingleNotch"] = table.SingleNotch;
            dr["HighPass"] = table.HighPass;
            dr["LowPass"] = table.LowPass;
            dr["Sensitivity"] = setSensitivity(table.PixelRate, table.Unit == "Pa");
            dr["PixelEnable"] = table.PixelEnable;
            dr["Reserve"] = table.Reserve;
            dr["CalibrationsVisible"] = table.CalibrationsVisible;
            dr["IsShowValue"] = table.IsShowValue;
            dr["Antipole"] = table.Antipole;
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
        #region 滤波器相关
        /// <summary>
        /// 获取陷波器类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static byte getSingleNotch(object obj)
        {
            byte ret = 0;
            if (obj != null)
            {
                string ss = obj.ToString();
                switch (ss)
                {
                    case "1s":
                        ret = 1;
                        break;

                }
            }
            return ret;
        }
        /// <summary>
        /// 获取陷波器描述
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static string setSingleNotch(byte typ)
        {
            string ret = "Off";
            switch (typ)
            {
                case 1:
                    ret = "1s";
                    break;

            }
            return ret;
        }
        /// <summary>
        /// 获取高通滤波器类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static byte getHighPass(object obj)
        {
            byte ret = 0;
            if (obj != null)
            {
                string ss = obj.ToString();
                switch (ss)
                {
                    case "0.1592":
                        ret = 1;
                        break;
                    case "0.5305":
                        ret = 2;
                        break;
                }
            }
            return ret;
        }
        /// <summary>
        /// 获取高通滤波器描述
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static string setHighPass(byte typ)
        {
            string ret = "Off";
            switch (typ)
            {
                case 1:
                    ret = "0.1592";
                    break;
                case 2:
                    ret = "0.5305";
                    break;
            }
            return ret;
        }
        /// <summary>
        /// 获取低通滤波器类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static byte getLowPass(object obj)
        {
            byte ret = 0;
            if (obj != null)
            {
                string ss = obj.ToString();
                switch (ss)
                {
                    case "30s":
                        ret = 1;
                        break;
                    case "60s":
                        ret = 2;
                        break;
                }
            }
            return ret;
        }
        /// <summary>
        /// 获取低通滤波器描述
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static string setLowPass(byte typ)
        {
            string ret = "Off";
            switch (typ)
            {
                case 1:
                    ret = "30s";
                    break;
                case 2:
                    ret = "60s";
                    break;
            }
            return ret;
        }
        #endregion
    }
}
