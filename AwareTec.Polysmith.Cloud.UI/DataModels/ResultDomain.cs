using AwareTec.Polysmith.pChart;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.DataModels
{
    /// <summary>
    /// 分析结果数据类
    /// </summary>
    public class ResultDomain
    {
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
