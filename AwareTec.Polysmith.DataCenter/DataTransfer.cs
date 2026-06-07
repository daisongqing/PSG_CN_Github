using AwareTec.Polysmith.DataBaseCom;
using System;
using System.Collections.Generic;

namespace AwareTec.Polysmith.DataCenter
{
    /// <summary>
    /// 分析结果集合
    /// </summary>
    public class OutResult
    {
        /// <summary>
        /// Tag
        /// </summary>
        public object Tag { set; get; }
        /// <summary>
        /// 分析结果的唯一标识
        /// </summary>
        public string GUID { set; get; }
        /// <summary>
        /// edf的加载路径
        /// </summary>
        public string EdfPath { set; get; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { set; get; }
        /// <summary>
        /// 关灯时间
        /// </summary>
        public DateTime LightOffTime { set; get; }
        /// <summary>
        /// 开灯时间
        /// </summary>
        public DateTime LightOnTime { set; get; }
        /// <summary>
        /// 心率指标
        /// </summary>
        public Doc_HeartRateReuslt HeartRate { set; get; }
        /// <summary>
        /// 磨牙指标
        /// </summary>
        public Doc_MolarReuslt Molar { set; get; }
        /// <summary>
        /// 呼吸指标
        /// </summary>
        public Doc_BreathEventResult BreathEvent { set; get; }
        /// <summary>
        /// 体位指标
        /// </summary>
        public Doc_BodyStateResult BodyState { set; get; }
        /// <summary>
        /// 体动指标
        /// </summary>
        public Doc_BodyMovementResult BodyMovement { set; get; }
        /// <summary>
        /// 血氧指标
        /// </summary>
        public Doc_BloodOxygenResult BloodOxygen { set; get; }
        /// <summary>
        /// 总帧结果
        /// </summary>
        public List<Doc_Epochs> Epochs { set; get; }
        /// <summary>
        /// 事件标注
        /// </summary>
        public List<Doc_EventRecords> EventRecords { set; get; }
        /// <summary>
        /// 睡眠相关指标
        /// </summary>
        public Doc_SleepResult Sleep { set; get; }
        /// <summary>
        /// 中断
        /// </summary>
        public bool Interrupt = false;
        /// <summary>
        ///  构造函数
        /// </summary>
        public OutResult()
        {
            EventRecords = new List<Doc_EventRecords>();
            Epochs = new List<Doc_Epochs>();
            HeartRate = new Doc_HeartRateReuslt();
            Sleep = new Doc_SleepResult();
            BloodOxygen = new Doc_BloodOxygenResult();
            BodyState = new Doc_BodyStateResult();
            BreathEvent = new Doc_BreathEventResult();
            BodyMovement = new Doc_BodyMovementResult();
            Molar = new Doc_MolarReuslt();
        }
        /// <summary>
        /// 清除数据
        /// </summary>
        public void Clear()
        {
            EventRecords.Clear();
            Epochs.Clear();
            EdfPath = "";
            GUID = "";
            Tag = null;
            HeartRate = new Doc_HeartRateReuslt();
            Sleep = new Doc_SleepResult();
            BloodOxygen = new Doc_BloodOxygenResult();
            BodyState = new Doc_BodyStateResult();
            BreathEvent = new Doc_BreathEventResult();
            BodyMovement = new Doc_BodyMovementResult();
            Molar = new Doc_MolarReuslt();
        }
    }
    /// <summary>
    /// 输入条件参数
    /// </summary>
    public class InConditions
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { set; get; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { set; get; }
        /// <summary>
        /// 关灯时间
        /// </summary>
        public DateTime LightOffTime { set; get; }
        /// <summary>
        /// 开灯时间
        /// </summary>
        public DateTime LightOnTime { set; get; }
        /// <summary>
        /// 总帧结果
        /// </summary>
        public List<Doc_Epochs> Epochs { set; get; }
        /// <summary>
        /// 事件标注
        /// </summary>
        public List<Doc_EventRecords> EventRecords { set; get; }
        /// <summary>
        /// EDF路径
        /// </summary>
        public string EdfPath { set; get; }
        /// <summary>
        /// 唯一标号
        /// </summary>
        public string GUID { set; get; }
        /// <summary>
        /// 总睡眠帧
        /// </summary>
        public int TotalFrameCnt { set; get; }

        /// <summary>
        /// 初筛
        /// </summary>
        public bool IsBreathOnly { set; get; }
        /// <summary>
        /// 分析字
        /// </summary>
        public int AnalysisStateWord { set; get; }
        /// <summary>
        /// 血氧值
        /// </summary>
        public List<float> SpO2Values { set; get; }
        /// <summary>
        /// 心率值
        /// </summary>
        public List<float> HeartRateValues { set; get; }

        /// <summary>
        /// 压力值
        /// </summary>
        public List<float> PressureValues { set; get; }
        public InConditions()
        {
            Epochs = new List<Doc_Epochs>();
            EventRecords = new List<Doc_EventRecords>();
            LightOffTime = default(DateTime);
            LightOnTime = default(DateTime);
            AnalysisStateWord = 0;
            IsBreathOnly = false;
            SpO2Values = new List<float>();
            HeartRateValues = new List<float>();
        }
    }
}
