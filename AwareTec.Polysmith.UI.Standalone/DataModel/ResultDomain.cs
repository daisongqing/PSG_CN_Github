using AwareTec.Polysmith.pChart;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.UI.DataModel
{
    /// <summary>
    /// 分析结果数据类
    /// </summary>
    public class ResultDomain
    {
        private string m_BasePath = AppDomain.CurrentDomain.BaseDirectory + "\\AnalysisData\\分析数据集合.xls";
        private static ResultDomain m_Default = null;
        /// <summary>
        /// 单例
        /// </summary>
        public static ResultDomain Default
        {
            get
            {
                return m_Default ?? (m_Default = new ResultDomain());
            }
        }
        public bool Start = false;
        /// <summary>
        /// 单例模式的构造函数
        /// </summary>
        private ResultDomain(bool one)
        {
            Ready = false;
            new Task(() =>
            {
                //ReadExcelDataBySql();
                LoadData();
                //CreatData();
                Ready = true;
            }).Start();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public ResultDomain()
        {

        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="maxLength"></param>
        public void InitData(int maxLength)
        {
            Type keyTyp=typeof(ResultDomain);
            PropertyInfo[] info= keyTyp.GetProperties();
            Type tt=typeof(SuperPointF[]);
            foreach (PropertyInfo pi in info)
            {
                if (pi.PropertyType == tt)
                {
                    pi.SetValue(this, new SuperPointF[maxLength]);
                }
            }
        }
        /// <summary>
        /// 加载本地数据
        /// </summary>
        private void LoadData()
        {
            try
            {
                object missing = System.Reflection.Missing.Value;
                Application excel = new ApplicationClass();
                if (excel == null)
                {
                    return;
                }
                else
                {

                    excel.Visible = false;
                    excel.UserControl = true;
                    // 以只读的形式打开EXCEL文件
                    Workbook wb = excel.Application.Workbooks.Open(m_BasePath, missing, true, missing, missing, missing,
                     missing, missing, missing, true, missing, missing, missing, missing, missing);
                    //取得第一个工作薄
                    Worksheet ws = (Worksheet)wb.Worksheets.get_Item(1);
                    string[] cols = new string[] { "B", "C", "D", "E", "F", "G", "H", "I", "J", "K" };
                    Range rng3 = ws.Columns.get_Range(cols[0] + 1, cols[cols.Length - 1] + 1);
                    //取得总记录行数   (包括标题列)
                    int rowsint = ws.UsedRange.Cells.Rows.Count; //得到行数
                    object[,] arrayColums = (object[,])rng3.Value2;
                    for (int i = 0; i < arrayColums.Length; i++)
                    {
                        string head = arrayColums[1, i + 1].ToString();
                        //取得数据范围区域 (不包括标题列) 
                        Range rng1 = ws.Cells.get_Range(string.Format("{0}2", cols[i]), string.Format("{0}{1}", cols[i], rowsint));
                        object[,] arryItem = (object[,])rng1.Value2;   //get range's value
                        int len = arryItem.Length;
                        SuperPointF[] data = new SuperPointF[(len == 0 ? 960 : len)];
                        switch (head)
                        {
                            case "睡眠分期":
                                if (len == 0)
                                {
                                    len = 960;
                                    Random r = new Random(4);
                                    for (int s = 0; s < len; s++)
                                    {
                                        SuperPointF sf = new SuperPointF();
                                        sf.XValue = s;
                                        sf.YMaxValue = r.Next(1, 5);
                                        data[s] = (sf);
                                    }
                                }
                                else
                                {
                                    for (int s = 1; s <= len; s++)
                                    {
                                        SuperPointF sf = new SuperPointF();
                                        sf.XValue = s;
                                        sf.YMaxValue = Convert.ToSingle(arryItem[s, 1]);
                                        data[s - 1] = (sf);
                                    }
                                }
                                SleepStagPoints = data;
                                break;
                            case "体位":
                                if (len == 0)
                                {
                                    len = 960;
                                    Random r = new Random(2);
                                    for (int s = 0; s < len; s++)
                                    {
                                        SuperPointF sf = new SuperPointF();
                                        sf.XValue = s;
                                        sf.YMaxValue = r.Next(1, 4);
                                        data[s] = (sf);
                                    }
                                }
                                else
                                {
                                    for (int s = 1; s <= len; s++)
                                    {
                                        SuperPointF sf = new SuperPointF();
                                        sf.XValue = s;
                                        sf.YMaxValue = Convert.ToSingle(arryItem[s, 1]);
                                        data[s - 1] = (sf);
                                    }
                                }
                                BodyStatePoints = data;
                                break;
                            case "血氧饱和度":
                                if (len == 0)
                                {
                                    len = 960;
                                    Random r = new Random(97);
                                    for (int s = 0; s < len; s++)
                                    {
                                        SuperPointF sf = new SuperPointF();
                                        sf.XValue = s;
                                        sf.YMaxValue = r.Next(97, 100);
                                        sf.YMinValue = r.Next(90, 97);
                                        data[s] = (sf);
                                    }
                                }
                                else
                                {
                                    for (int s = 1; s <= len; s++)
                                    {
                                        SuperPointF sf = new SuperPointF();
                                        string[] ss = arryItem[s, 1].ToString().Split(',');
                                        sf.XValue = s;
                                        sf.YMaxValue = Convert.ToSingle(ss[1]);
                                        sf.YMinValue = Convert.ToSingle(ss[0]);
                                        data[s - 1] = (sf);
                                    }
                                }
                                BloodOxygenPoints = data;
                                break;
                            case "心率":
                                if (len == 0)
                                {
                                    len = 960;
                                    Random r = new Random(150);
                                    for (int s = 0; s < len; s++)
                                    {
                                        SuperPointF sf = new SuperPointF();
                                        sf.XValue = s;
                                        sf.YMaxValue = r.Next(100, 110);
                                        sf.YMinValue = r.Next(50, 70);
                                        data[s] = (sf);
                                    }
                                }
                                else
                                {
                                    for (int s = 1; s <= len; s++)
                                    {
                                        SuperPointF sf = new SuperPointF();
                                        string[] ss = arryItem[s, 1].ToString().Split(',');
                                        sf.XValue = s;
                                        sf.YMaxValue = Convert.ToSingle(ss[1]);
                                        sf.YMinValue = Convert.ToSingle(ss[0]);
                                        data[s - 1] = (sf);
                                    }
                                }
                                HeartRatePoints = data;
                                break;
                            case "OA":
                                if (len == 0)
                                {
                                    len = 960;
                                    Random r = new Random(15);
                                    for (int s = 0; s < len; s++)
                                    {
                                        SuperPointF sf = new SuperPointF();
                                        sf.XValue = s;
                                        sf.YMaxValue = r.Next(10, 20);
                                        sf.YMinValue = 0;
                                        data[s] = (sf);
                                    }
                                }
                                else
                                {
                                    for (int s = 1; s <= len; s++)
                                    {
                                        SuperPointF sf = new SuperPointF();
                                        sf.XValue = s;
                                        sf.YMaxValue = Convert.ToSingle(arryItem[s, 1]) * 120;
                                        sf.YMinValue = 0;
                                        data[s - 1] = (sf);
                                    }
                                }
                                OAPoints = data;
                                break;
                            case "CA":
                                if (len == 0)
                                {
                                    len = 960;
                                    Random r = new Random(18);
                                    for (int s = 0; s < len; s++)
                                    {
                                        SuperPointF sf = new SuperPointF();
                                        sf.XValue = s;
                                        if (s > 80 && s < 300 || s > 400 && s < 600 || s > 800)
                                        {
                                            sf.YMaxValue = r.Next(8, 20);
                                            sf.YMinValue = 0;
                                        }
                                        data[s] = (sf);
                                    }
                                }
                                else
                                {
                                    for (int s = 1; s <= len; s++)
                                    {
                                        SuperPointF sf = new SuperPointF();
                                        sf.XValue = s;
                                        sf.YMaxValue = Convert.ToSingle(arryItem[s, 1]) * 120;
                                        sf.YMinValue = 0;
                                        data[s - 1] = (sf);
                                    }
                                }
                                CAPoints = data;
                                break;
                            case "MA":
                                if (len == 0)
                                {
                                    len = 960;
                                    Random r = new Random(16);
                                    for (int s = 0; s < len; s++)
                                    {
                                        SuperPointF sf = new SuperPointF();
                                        sf.XValue = s;
                                        if (s < 300 || s > 500)
                                        {
                                            sf.YMaxValue = r.Next(12, 20);
                                            sf.YMinValue = 0;
                                        }
                                        data[s] = (sf);
                                    }
                                }
                                else
                                {
                                    for (int s = 1; s <= len; s++)
                                    {
                                        SuperPointF sf = new SuperPointF();
                                        sf.XValue = s;
                                        sf.YMaxValue = Convert.ToSingle(arryItem[s, 1]) * 120;
                                        sf.YMinValue = 0;
                                        data[s - 1] = (sf);
                                    }
                                }
                                MAPoints = data;
                                break;
                            case "OH":
                                if (len == 0)
                                {
                                    len = 960;
                                    Random r = new Random(15);
                                    for (int s = 0; s < len; s++)
                                    {
                                        SuperPointF sf = new SuperPointF();
                                        sf.XValue = s;
                                        sf.YMaxValue = r.Next(10, 20);
                                        sf.YMinValue = 0;
                                        data[s] = (sf);
                                    }
                                }
                                else
                                {
                                    for (int s = 1; s <= len; s++)
                                    {
                                        SuperPointF sf = new SuperPointF();
                                        sf.XValue = s;
                                        sf.YMaxValue = Convert.ToSingle(arryItem[s, 1]) * 120;
                                        sf.YMinValue = 0;
                                        data[s - 1] = (sf);
                                    }
                                }
                                OHPoints = data;
                                break;
                            case "CH":
                                if (len == 0)
                                {
                                    len = 960;
                                    Random r = new Random(18);
                                    for (int s = 0; s < len; s++)
                                    {
                                        SuperPointF sf = new SuperPointF();
                                        sf.XValue = s;
                                        if (s > 80 && s < 300 || s > 400 && s < 600 || s > 800)
                                        {
                                            sf.YMaxValue = r.Next(8, 20);
                                            sf.YMinValue = 0;
                                        }
                                        data[s] = (sf);
                                    }
                                }
                                else
                                {
                                    for (int s = 1; s <= len; s++)
                                    {
                                        SuperPointF sf = new SuperPointF();
                                        sf.XValue = s;
                                        sf.YMaxValue = Convert.ToSingle(arryItem[s, 1]) * 120;
                                        sf.YMinValue = 0;
                                        data[s - 1] = (sf);
                                    }
                                }
                                HypopneaPoints = data;
                                break;
                            case "MH":
                                if (len == 0)
                                {
                                    len = 960;
                                    Random r = new Random(16);
                                    for (int s = 0; s < len; s++)
                                    {
                                        SuperPointF sf = new SuperPointF();
                                        sf.XValue = s;
                                        if (s < 300 || s > 500)
                                        {
                                            sf.YMaxValue = r.Next(12, 20);
                                            sf.YMinValue = 0;
                                        }
                                        data[s] = (sf);
                                    }
                                }
                                else
                                {
                                    for (int s = 1; s <= len; s++)
                                    {
                                        SuperPointF sf = new SuperPointF();
                                        sf.XValue = s;
                                        sf.YMaxValue = Convert.ToSingle(arryItem[s, 1]) * 120;
                                        sf.YMinValue = 0;
                                        data[s - 1] = (sf);
                                    }
                                }
                                MHPoints = data;
                                break;
                        }
                    }
                }
                excel.Quit(); excel = null;
                Process[] procs = Process.GetProcessesByName("excel");
                foreach (Process pro in procs)
                {
                    pro.Kill();//没有更好的方法,只有杀掉进程
                }
                GC.Collect();
            }
            catch { }
        }
        private void CreatData()
        {
            string[] head=new  string[]{"睡眠分期","体位","血氧饱和度","心率","OA","CA","MA"};
            for (int i = 0; i < head.Length; i++)
            {
                int len = 0;
                SuperPointF[] data = new SuperPointF[len == 0 ? 960 : len];
                switch (head[i])
                {
                    case "睡眠分期":
                        if (len == 0)
                        {
                            len = 960;
                            Random r = new Random(4);
                            for (int s = 0; s < len; s++)
                            {
                                SuperPointF sf = new SuperPointF();
                                sf.XValue = s;
                                sf.YMaxValue = r.Next(1, 5);
                                data[s] = sf;
                            }
                        }
                        SleepStagPoints = data;
                        break;
                    case "体位":
                        if (len == 0)
                        {
                            len = 960;
                            Random r = new Random(2);
                            for (int s = 0; s < len; s++)
                            {
                                SuperPointF sf = new SuperPointF();
                                sf.XValue = s;
                                sf.YMaxValue = r.Next(1, 4);
                                data[s] = sf;
                            }
                        }
                        BodyStatePoints = data;
                        break;
                    case "血氧饱和度":
                        if (len == 0)
                        {
                            len = 960;
                            Random r = new Random(97);
                            for (int s = 0; s < len; s++)
                            {
                                SuperPointF sf = new SuperPointF();
                                sf.XValue = s;
                                sf.YMaxValue = r.Next(97, 100);
                                sf.YMinValue = r.Next(90, 97);
                                data[s] = sf;
                            }
                        }
                        BloodOxygenPoints = data;
                        break;
                    case "心率":
                        if (len == 0)
                        {
                            len = 960;
                            Random r = new Random(150);
                            for (int s = 0; s < len; s++)
                            {
                                SuperPointF sf = new SuperPointF();
                                sf.XValue = s;
                                sf.YMaxValue = r.Next(100, 110);
                                sf.YMinValue = r.Next(50, 70);
                                data[s] = sf;
                            }
                        }
                        HeartRatePoints = data;
                        break;
                    case "OA":
                        if (len == 0)
                        {
                            len = 960;
                            Random r = new Random(15);
                            for (int s = 0; s < len; s++)
                            {
                                SuperPointF sf = new SuperPointF();
                                sf.XValue = s;
                                sf.YMaxValue = r.Next(10, 20);
                                sf.YMinValue = 0;
                                data[s] = sf;
                            }
                        }
                        OAPoints = data;
                        break;
                    case "CA":
                        if (len == 0)
                        {
                            len = 960;
                            Random r = new Random(18);
                            for (int s = 0; s < len; s++)
                            {
                                if (s > 80 && s < 300 || s > 400 && s < 600 || s > 800)
                                {
                                    SuperPointF sf = new SuperPointF();
                                    sf.XValue = s;
                                    sf.YMaxValue = r.Next(8, 20);
                                    sf.YMinValue = 0;
                                    data[s] = sf;
                                }
                            }
                        }
                        CAPoints = data;
                        break;
                    case "MA":
                        if (len == 0)
                        {
                            len = 960;
                            Random r = new Random(16);
                            for (int s = 0; s < len; s++)
                            {
                                if (s < 300 || s > 500)
                                {
                                    SuperPointF sf = new SuperPointF();
                                    sf.XValue = s;
                                    sf.YMaxValue = r.Next(12, 20);
                                    sf.YMinValue = 0;
                                    data[s] = sf;
                                }
                            }
                        }
                        MAPoints = data;
                        break;
                }
            }
        }
        /// <summary>
        /// 多次小睡事件
        /// </summary>
        public List<DataBaseCom.Doc_EventRecords> MultSleepRecords = new List<DataBaseCom.Doc_EventRecords>();
        /// <summary>
        /// 准备完毕
        /// </summary>
        public bool Ready { set; get; }
        /// <summary>
        /// 睡眠分期数据
        /// </summary>
        public SuperPointF[] SleepStagPoints { set; get; }
        /// <summary>
        /// 血氧数据
        /// </summary>
        public SuperPointF[] BloodOxygenPoints { set; get; }
        /// <summary>
        /// 心率数据
        /// </summary>
        public SuperPointF[] HeartRatePoints { set; get; }
        /// <summary>
        /// 微觉醒
        /// </summary>
        public SuperPointF[] MicArousalPoints { set; get; }
        /// <summary>
        /// PLMs(周期性循环腿动)
        /// </summary>
        public SuperPointF[] PLMsPoints { set; get; }
        /// <summary>
        /// PLM(腿动)
        /// </summary>
        public SuperPointF[] PLMPoints { set; get; }
        /// <summary>
        /// OA (阻塞型呼吸暂停事件)
        /// </summary>
        public SuperPointF[] OAPoints { set; get; }
        /// <summary>
        /// CA (中枢型呼吸暂停事件)
        /// </summary>
        public SuperPointF[] CAPoints { set; get; }
        /// <summary>
        /// MA (混合型呼吸暂停事件)
        /// </summary>
        public SuperPointF[] MAPoints { set; get; }
        /// <summary>
        /// 体位
        /// </summary>
        public SuperPointF[] BodyStatePoints { set; get; }
        /// <summary>
        /// OH
        /// </summary>
        public SuperPointF[] OHPoints { set; get; }
        /// <summary>
        /// 低通气事件
        /// </summary>
        public SuperPointF[] HypopneaPoints { set; get; }
        /// <summary>
        /// MH
        /// </summary>
        public SuperPointF[] MHPoints { set; get; }
        /// <summary>
        /// 体动数据
        /// </summary>
        public SuperPointF[] MTPoints { set; get; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { set; get; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { set; get; }
        /// <summary>
        /// 偏移帧数
        /// </summary>
        public int OffSetFrameCnt = 1;
    }
}
