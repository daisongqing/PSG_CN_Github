using AwareTec.Polysmith.DataBaseCom;
using System;
using System.Collections.Generic;
using System.Linq;
namespace AwareTec.Polysmith.DataCenter
{
    /// <summary>
    /// 基础公共数据信息体
    /// </summary>
    public class IDataBase
    {       
        /// <summary>
        /// edf的原始路径
        /// </summary>
        public string m_EdfPath = "";

        private DateTime m_StartRecordTime = default(DateTime);
        /// <summary>
        /// 记录的开始时间
        /// </summary>
        public DateTime StartRecordTime
        {
            set
            {
                m_StartRecordTime = value;
            }
            get
            {
                return m_StartRecordTime;
            }
        }
        private DateTime m_ValidStartTime = default(DateTime);
        /// <summary>
        /// 分析有效的开始时间
        /// </summary>
        public DateTime ValidStartTime
        {
            set
            {
                m_ValidStartTime = value;
                if (m_StartRecordTime.Year != 1 && value.Year != 1)
                    m_StartFrameNo = (int)(value - m_StartRecordTime).TotalSeconds / 30 + 1;

            }
            get
            {
                return m_ValidStartTime;
            }
        }
        private DateTime m_ValidEndTime = default(DateTime);
        /// <summary>
        /// 分析有效的结束时间
        /// </summary>
        public DateTime ValidEndTime
        {
            set
            {
                m_ValidEndTime = value;
                if (m_StartRecordTime.Year != 1 && value.Year != 1)
                {
                    int sec = (int)(value - m_StartRecordTime).TotalSeconds;
                    m_EndFrameNo = sec % 30 == 0 ? sec / 30 : sec / 30 + 1;
                }
            }
            get
            {
                return m_ValidEndTime;
            }
        }
        public bool IsBreathOnly = false;
        /// <summary>
        /// TST的开始时间
        /// </summary>
        public DateTime TSTStartTime { private set; get; }
        /// <summary>
        /// TST的结束时间
        /// </summary>
        public DateTime TSTEndTime { private set; get; }
        private List<DataEpoch> m_TIBepochs = new List<DataEpoch>();
        /// <summary>
        /// 获取开关灯期间的帧集合
        /// </summary>
        internal List<DataEpoch> TIBEpochs
        {
            get
            {
                return m_TIBepochs;
            }
        }
        private List<DataEpoch> m_TSTEpochs = null;
        /// <summary>
        /// 获取TST期间的帧集合
        /// </summary>
        internal List<DataEpoch> TSTEpochs
        {
            get
            {
                return m_TSTEpochs;
            }
        }
        private List<DataEvent> m_events = new List<DataEvent>();
        /// <summary>
        /// 获取或设置事件标记
        /// </summary>
        internal List<DataEvent> AllEvents
        {
            get
            {
                return this.m_events;
            }
            set
            {
                this.m_events = value;
                this.m_BreathEvents.Clear();
                this.m_SpO2Events.Clear();
                this.m_ArousalEvents.Clear();
                this.m_LegsEvents.Clear();
                m_SnoreEvents.Clear();
                m_MolarEvents.Clear();
                IEnumerable<IGrouping<EventTypeEnum, DataEvent>> source = Enumerable.GroupBy<DataEvent, EventTypeEnum>((IEnumerable<DataEvent>)value, (Func<DataEvent, EventTypeEnum>)(t => t.EventTyp));
                if (Enumerable.Count<IGrouping<EventTypeEnum, DataEvent>>(source) <= 0)
                    return;
                foreach (IGrouping<EventTypeEnum, DataEvent> grouping in source)
                {
                    List<DataEvent> list = Enumerable.ToList<DataEvent>((IEnumerable<DataEvent>)grouping);
                    if (grouping.Key == EventTypeEnum.Resp_CA || grouping.Key == EventTypeEnum.Resp_OA || (grouping.Key == EventTypeEnum.Resp_MA || grouping.Key == EventTypeEnum.Resp_Hypnea))
                        this.m_BreathEvents.AddRange((IEnumerable<DataEvent>)list);
                    else if (grouping.Key == EventTypeEnum.SpO2 || grouping.Key == EventTypeEnum.Spo2Artifact)
                        this.m_SpO2Events.AddRange((IEnumerable<DataEvent>)list);
                    else if (grouping.Key == EventTypeEnum.Arousal || grouping.Key == EventTypeEnum.PlmArousal || grouping.Key == EventTypeEnum.RespArousal || grouping.Key == EventTypeEnum.LmArousal)
                        this.m_ArousalEvents.AddRange((IEnumerable<DataEvent>)list);
                    else if (grouping.Key == EventTypeEnum.PeriodicalBodyMove || grouping.Key == EventTypeEnum.Leg)
                        this.m_LegsEvents.AddRange((IEnumerable<DataEvent>)list);
                    else if (grouping.Key == EventTypeEnum.Snore)
                    {
                        m_SnoreEvents.AddRange(list);
                    }
                    else if (grouping.Key == EventTypeEnum.MultipleSleep)
                    {
                        m_MulSleepEvents.AddRange(list);
                    }
                    else if(grouping.Key==EventTypeEnum.Molar)
                    {
                        m_MolarEvents.AddRange(list);
                    }
                }
            }
        }
        private List<DataEvent> m_MolarEvents = new List<DataEvent>();
        /// <summary>
        /// 获取所有的磨牙事件
        /// </summary>
        internal List<DataEvent> MolarEvents
        {
            get
            {
                return m_MolarEvents;
            }
        }
        private List<DataEvent> m_SnoreEvents = new List<DataEvent>();
        /// <summary>
        /// 获取所有的鼾声事件
        /// </summary>
        internal List<DataEvent> SnoreEvents
        {
            get
            {
                return m_SnoreEvents;
            }
        }
        private List<DataEvent> m_LegsEvents = new List<DataEvent>();
        /// <summary>
        /// 获取所有的腿动事件
        /// </summary>
        internal List<DataEvent> LegsEvents
        {
            get
            {
                return m_LegsEvents;
            }
        }
        private List<DataEvent> m_BreathEvents = new List<DataEvent>();
        /// <summary>
        /// 获取所有的呼吸事件
        /// </summary>
        internal List<DataEvent> BreathEvents
        {
            get
            {
                return m_BreathEvents;
            }
        }
        private List<DataEvent> m_SpO2Events = new List<DataEvent>();
        /// <summary>
        /// 获取所有的血氧事件
        /// </summary>
        internal List<DataEvent> SpO2Events
        {
            get
            {
                return m_SpO2Events;
            }
        }
        private List<DataEvent> m_ArousalEvents = new List<DataEvent>();
        /// <summary>
        /// 获取所有的微觉醒事件事件
        /// </summary>
        internal List<DataEvent> ArousalEvents
        {
            get
            {
                return m_ArousalEvents;
            }
        }
        private List<DataEvent> m_MulSleepEvents = new List<DataEvent>();
        /// <summary>
        /// 获取所有的小睡事件
        /// </summary>
        internal List<DataEvent> MulSleepEvents
        {
            get
            {
                return m_MulSleepEvents;
            }
        }
        private float m_SPTime = 0;
        /// <summary>
        /// 获取SPT
        /// </summary>
        internal float SPTime
        {
            get
            {
                return m_SPTime;
            }
        }
        private float m_TotalSleepTime = 0;
        /// <summary>
        /// 获取TST
        /// </summary>
        internal float TotalSleepTime
        {
            get
            {
                return m_TotalSleepTime;
            }
        }

        private float m_RemTotalTimes = 0;
        /// <summary>
        /// 获取RT
        /// </summary>
        internal float RemTotalTimes
        {
            get
            {
                return m_RemTotalTimes;
            }
        }
        private float m_NRemTotalTimes = 0;
        /// <summary>
        /// 获取NT
        /// </summary>
        internal float NRemTotalTimes
        {
            get
            {
                return m_NRemTotalTimes;
            }
        }
        private int m_StartFrameNo = 0;
        /// <summary>
        /// 获取分析数据的开始帧序号
        /// </summary>
        internal int StartFrameNo
        {
            get
            {
                return m_StartFrameNo;
            }
        }
        private int m_EndFrameNo = 0;
        /// <summary>
        /// 获取分析数据的结束帧序号
        /// </summary>
        internal int EndFrameNo
        {
            get
            {
                return m_EndFrameNo;
            }
        }
        /// <summary>
        /// 血氧值队列
        /// </summary>
        private List<float> m_SpO2Values = new List<float>();
        /// <summary>
        /// 获取血氧值队列
        /// </summary>
        internal List<float> SpO2Values
        {
            get
            {
                return m_SpO2Values;
            }
        }
        /// <summary>
        /// 数据执行初始化
        /// </summary>
        internal bool Init(List<Doc_Epochs> epochs, List<Doc_EventRecords> eventRecords)
        {
            int startIdx = -1;
            int endIdx = -1;
            this.m_StartFrameNo = (int)(this.m_ValidStartTime - this.m_StartRecordTime).TotalSeconds / 30 + 1;
            double totalSeconds = (this.m_ValidEndTime - this.m_StartRecordTime).TotalSeconds;
            this.m_EndFrameNo = totalSeconds % 30.0 == 0.0 ? (int)(totalSeconds / 30.0) : (int)(totalSeconds / 30.0 + 1.0);
            if (epochs.Count < this.m_EndFrameNo)
                return false;
            this.m_TIBepochs.Clear();
            for (int index = this.m_StartFrameNo - 1; index < this.m_EndFrameNo; ++index)
            {
                Doc_Epochs docEpochs = epochs[index];
                string[] strArray1 = docEpochs.SpO2.Split(',');
                string[] strArray2 = docEpochs.HeartRate.Split(',');
                if (strArray1.Length < 3 || strArray2.Length < 3)
                    continue;
                float m_minSpO2 = float.Parse(strArray1[0]);
                float m_maxSpO2 = float.Parse(strArray1[1]);
                float m_minHeartRate = float.Parse(strArray2[0]);
                float m_maxHeartRate = float.Parse(strArray2[1]);
                int m_minHeartRateIndex = 0;
                int m_MaxHeartRateIndex = 0;
                if (strArray2.Length >= 5)
                {
                    m_minHeartRateIndex = Convert.ToInt32(strArray2[3]);
                    m_MaxHeartRateIndex = Convert.ToInt32(strArray2[4]);
                }
                DataEpoch one = new DataEpoch()
                {
                    Stage = this.IsBreathOnly ? 4 : docEpochs.Stage,
                    Pos = docEpochs.Pos,
                    OA = docEpochs.OA,
                    MT = docEpochs.MT,
                    Index = index,
                    CA = docEpochs.CA,
                    MA = docEpochs.MA,
                    MaxSpO2 = m_maxSpO2,
                    MinSpO2 = m_minSpO2,
                    AveSpO2 = strArray1.Length == 2 ? m_minSpO2 : float.Parse(strArray1[2]),
                    MinHeartRate = m_minHeartRate,
                    MaxHeartRate = m_maxHeartRate,
                    MinHeartRateIndex= m_minHeartRateIndex,
                    MaxHeartRateIndex= m_MaxHeartRateIndex,
                    AveHeartRate = strArray2.Length == 2 ? m_minHeartRate : float.Parse(strArray2[2]),
                    Hypopnea = docEpochs.Hypopnea,
                    MicArousal=docEpochs.MicArousal,
                    PLMs =docEpochs.PLMs,
                    PLM=docEpochs.PLM
                };
                if (strArray1.Length > 10)
                {
                    one.Spo2LevelTimes = new int[strArray1.Length - 3];
                    int cnt = 0;
                    for (int i = 3; i < strArray1.Length; i++)
                        one.Spo2LevelTimes[cnt++] = int.Parse(strArray1[i]);
                }
                this.m_TIBepochs.Add(one);
            }
            for (int index = 0; index < this.m_TIBepochs.Count; ++index)
            {
                if (this.m_TIBepochs[index].Stage != (int)SleepStageEnum.W&& this.m_TIBepochs[index].Stage != (int)SleepStageEnum.None)
                {
                    startIdx = this.m_TIBepochs[index].Index;
                    this.TSTStartTime = this.m_StartRecordTime.AddSeconds((double)(startIdx * 30));
                    break;
                }
            }
            for (int index = this.m_TIBepochs.Count - 1; index >= 0; --index)
            {
                if (this.m_TIBepochs[index].Stage != (int)SleepStageEnum.W && this.m_TIBepochs[index].Stage != (int)SleepStageEnum.None)
                {
                    endIdx = this.m_TIBepochs[index].Index;
                    this.TSTEndTime = this.m_StartRecordTime.AddSeconds((double)((endIdx + 1) * 30));
                    break;
                }
            }
            this.m_TSTEpochs = Enumerable.ToList<DataEpoch>(Enumerable.Where<DataEpoch>((IEnumerable<DataEpoch>)this.m_TIBepochs, (Func<DataEpoch, bool>)(t =>
            {
                if (t.Index >= startIdx)
                    return t.Index <= endIdx;
                return false;
            })));
            for (int i = 0; i < m_TSTEpochs.Count; i++)
            {
                if (m_TSTEpochs[i].EnumStage != SleepStageEnum.W && m_TSTEpochs[i].EnumStage != SleepStageEnum.None)
                {
                    if (m_TSTEpochs[i].EnumStage == SleepStageEnum.R)
                    {
                        m_RemTotalTimes += 0.5f;
                    }
                    m_TotalSleepTime += 0.5f;
                }
                m_SPTime += 0.5f;
            }
            float validTime = (float)(this.m_ValidEndTime - this.m_ValidStartTime).TotalMinutes;
            if ((double)this.m_TotalSleepTime - (double)validTime > 1.0)
                this.m_TotalSleepTime = (float)Math.Round((double)validTime, 1);
            this.m_TotalSleepTime = (float)Math.Round((double)this.m_TotalSleepTime, 2);
            m_NRemTotalTimes = m_TotalSleepTime - m_RemTotalTimes;
            List<DataEvent> list = new List<DataEvent>();
            foreach (Doc_EventRecords docEventRecords in eventRecords)
            {
                DataEvent dataEvent = new DataEvent();
                if (Enum.TryParse<EventTypeEnum>(docEventRecords.EventType.ToString(), out dataEvent.EventTyp))
                {
                    int startframeNo;
                    int endframeNo;
                    if (dataEvent.EventTyp == EventTypeEnum.MultipleSleep)
                    {
                        startframeNo = docEventRecords.StartIndex;
                        endframeNo = docEventRecords.EndIndex - 1;//小睡结束索引要减1
                        dataEvent.StartTime = m_StartRecordTime.AddMinutes(docEventRecords.StartIndex * 0.5f);
                        dataEvent.EndTime = m_StartRecordTime.AddMinutes(docEventRecords.EndIndex * 0.5f);
                    }
                    else
                    {
                        dataEvent.StartTime = docEventRecords.StartTime;
                        dataEvent.EndTime = docEventRecords.EndTime;
                        if (docEventRecords.StartTime > this.m_ValidStartTime && docEventRecords.EndTime < this.m_ValidEndTime)
                        {
                            startframeNo = (int)((docEventRecords.StartTime - this.m_StartRecordTime).TotalSeconds / 30.0);
                            endframeNo = (int)((docEventRecords.EndTime - this.m_StartRecordTime).TotalSeconds / 30.0);
                        }
                        else if (docEventRecords.StartTime < this.m_ValidStartTime && docEventRecords.EndTime > this.m_ValidStartTime)
                        {
                            dataEvent.StartTime = this.m_ValidStartTime;
                            startframeNo = this.StartFrameNo - 1;
                            if (docEventRecords.EndTime > this.m_ValidEndTime)
                            {
                                dataEvent.EndTime = this.m_ValidEndTime;
                                endframeNo = this.m_EndFrameNo - 1;
                            }
                            else
                                endframeNo = (int)((docEventRecords.EndTime - this.m_StartRecordTime).TotalSeconds / 30.0);
                        }
                        else if (docEventRecords.StartTime < this.m_ValidEndTime && docEventRecords.StartTime > this.m_ValidStartTime && docEventRecords.EndTime > this.m_ValidEndTime)
                        {
                            dataEvent.EndTime = this.m_ValidEndTime;
                            startframeNo = (int)((docEventRecords.StartTime - this.m_StartRecordTime).TotalSeconds / 30.0);
                            endframeNo = this.m_EndFrameNo - 1;
                        }
                        else
                            continue;
                    }
                    dataEvent.OnStages = new int[endframeNo - startframeNo + 1];
                    dataEvent.OnPos = new int[dataEvent.OnStages.Length];
                    int index1 = 0;
                    for (int index2 = startframeNo; index2 <= endframeNo; ++index2)
                    {
                        dataEvent.OnStages[index1] = this.IsBreathOnly ? 4 : epochs[index2].Stage;
                        dataEvent.OnPos[index1++] = epochs[index2].Pos;
                    }

                    if (dataEvent.OnStages.ToList().Distinct().Count() > 1 && docEventRecords.EventName == "氧减")
                    {
                        var w = 0.0;
                        var r = 0.0;
                        var n = 0.0;
                        var max = 0.0; var result = 0;
                        for (int i = 0; i <= endframeNo - startframeNo; i++)
                        {
                            var time = 0.0;

                            if (i == 0)
                            {
                                time = (double)(this.m_StartRecordTime.AddSeconds(30 * (startframeNo + 1)) - docEventRecords.StartTime).TotalSeconds;
                            }
                            else if (i == endframeNo - startframeNo)
                            {
                                time = (double)(docEventRecords.EndTime - this.m_StartRecordTime.AddSeconds(30 * endframeNo)).TotalSeconds;
                            }
                            else
                            {
                                time = 30.0;
                            }

                            if (dataEvent.OnStages[i] == 5)
                            {
                                w += time;
                            }
                            else if (dataEvent.OnStages[i] == 4)
                            {
                                r += time;
                            }
                            else if (dataEvent.OnStages[i] == 3 || dataEvent.OnStages[i] == 2 || dataEvent.OnStages[i] == 1)
                            {
                                n += time;
                            }
                        }

                        max = w >= r ? (w >= n ? w : n) : (r >= n ? r : n);
                        foreach (int item in dataEvent.OnStages.ToList().Distinct())
                        {
                            if (item == 5)
                            {
                                result = result == 0 ? (max == w ? item : 0) : result;
                            }
                            if (item == 4)
                            {
                                result = result == 0 ? (max == r ? item : 0) : result;
                            }
                            if (item == 3 || item == 2 || item == 1)
                            {
                                result = result == 0 ? (max == n ? item : 0) : result;
                            }
                        }

                        if (result == 4 || result == 5)
                        {
                            for (int i = 0; i < dataEvent.OnStages.Length; i++)
                            {
                                dataEvent.OnStages[i] = result;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < dataEvent.OnStages.Length; i++)
                            {
                                if (dataEvent.OnStages[i] == 1 || dataEvent.OnStages[i] == 2 || dataEvent.OnStages[i] == 3)
                                {
                                    continue;
                                }
                                if (i == 0)
                                {
                                    dataEvent.OnStages[i] = dataEvent.OnStages.Where(x => x != 4 && x != 5).FirstOrDefault();
                                }
                                else
                                {
                                    dataEvent.OnStages[i] = dataEvent.OnStages[i - 1];
                                }
                            }
                        }
                    }

                    if (dataEvent.OnStages.ToList().Distinct().Count() > 1 && (docEventRecords.EventType == (int)EventTypeEnum.Resp_CA || docEventRecords.EventType == (int)EventTypeEnum.Resp_OA || docEventRecords.EventType == (int)EventTypeEnum.Resp_MA || docEventRecords.EventType == (int)EventTypeEnum.Resp_Hypnea))
                    {
                        var r = 0.0;
                        var n = 0.0;
                        var max = 0.0; var result = 0;
                        for (int i = 0; i <= endframeNo - startframeNo; i++)
                        {
                            var time = 0.0;

                            if (i == 0)
                            {
                                time = (double)(this.m_StartRecordTime.AddSeconds(30 * (startframeNo + 1)) - docEventRecords.StartTime).TotalSeconds;
                            }
                            else if (i == endframeNo - startframeNo)
                            {
                                time = (double)(docEventRecords.EndTime - this.m_StartRecordTime.AddSeconds(30 * endframeNo)).TotalSeconds;
                            }
                            else
                            {
                                time = 30.0;
                            }

                            if (dataEvent.OnStages[i] == 4)
                            {
                                r += time;
                            }
                            else if (dataEvent.OnStages[i] == 3 || dataEvent.OnStages[i] == 2 || dataEvent.OnStages[i] == 1)
                            {
                                n += time;
                            }
                        }

                        max = r >= n ? r : n;
                        foreach (int item in dataEvent.OnStages.ToList().Distinct())
                        {
                            if (item == 4)
                            {
                                result = result == 0 ? (max == r ? item : 0) : result;
                            }
                            if (item == 3 || item == 2 || item == 1)
                            {
                                result = result == 0 ? (max == n ? item : 0) : result;
                            }
                        }

                        if (result == 4)
                        {
                            for (int i = 0; i < dataEvent.OnStages.Length; i++)
                            {
                                dataEvent.OnStages[i] = result;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < dataEvent.OnStages.Length; i++)
                            {
                                if (dataEvent.OnStages[i] == 4)
                                {
                                    dataEvent.OnStages[i]= dataEvent.OnStages.Where(x => x != 4).FirstOrDefault();
                                }
                            }
                        }
                    }

                    //dataEvent.OnStages = Enumerable.ToArray<int>(Enumerable.Distinct<int>((IEnumerable<int>)dataEvent.OnStages));
                    dataEvent.Duration = (float)Math.Round((dataEvent.EndTime - dataEvent.StartTime).TotalSeconds, 2);
                    dataEvent.ID = docEventRecords.EventID;
                    dataEvent.StartIndex = docEventRecords.StartIndex;
                    dataEvent.EndIndex = docEventRecords.EndIndex;
                    dataEvent.Tag = (object)docEventRecords.Tag;
                    dataEvent.Comments = docEventRecords.Comments;
                    list.Add(dataEvent);
                }
            }            
            this.AllEvents = list;          
            return true;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        internal void Dispose()
        {
            this.m_events.Clear();
            this.m_BreathEvents.Clear();
            this.m_SpO2Events.Clear();
            this.m_ArousalEvents.Clear();
            this.m_LegsEvents.Clear();
            m_SnoreEvents.Clear();
            m_MolarEvents.Clear();
            m_TSTEpochs.Clear();
            m_TIBepochs.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
        /// <summary>
        /// 判断RERA事件的时间阈值
        /// </summary>
        private float m_TimeThresholdValue_arousal = 30;
        /// <summary>
        /// 判断RERA事件的时间阈值
        /// </summary>
        public float TimeThresholdValue_arousal
        {
            set
            {
                m_TimeThresholdValue_arousal = value;
            }
            get
            {
                return m_TimeThresholdValue_arousal;
            }
        }
        /// <summary>
        /// 判断伴随最长低通气事件发生的最低血氧事件的时间阈值
        /// </summary>
        private float m_TimeThresholdValue_hyp = 30;
        /// <summary>
        /// 判断伴随最长低通气事件发生的最低血氧事件的时间阈值
        /// </summary>
        public float TimeThresholdValue_hyp
        {
            set
            {
                m_TimeThresholdValue_hyp = value;
            }
            get
            {
                return m_TimeThresholdValue_hyp;
            }
        }
        /// <summary>
        /// 判断伴随最长阻塞型呼吸暂停事件发生的最低血氧事件的时间阈值
        /// </summary>
        private float m_TimeThresholdValue_apn = 30;
        /// <summary>
        /// 判断伴随最长阻塞型呼吸暂停事件发生的最低血氧事件的时间阈值
        /// </summary>
        public float TimeThresholdValue_apn
        {
            set
            {
                m_TimeThresholdValue_apn = value;
            }
            get
            {
                return m_TimeThresholdValue_apn;
            }
        }

        /// <summary>
        /// 判断呼吸事件相关的氧减事件时间阈值
        /// </summary>
        private float m_TimeThresholdValue_spo2 = 30;
        /// <summary>
        /// 判断呼吸事件相关的氧减事件时间阈值
        /// </summary>
        public float TimeThresholdValue_spo2
        {
            set
            {
                m_TimeThresholdValue_spo2 = value;
            }
            get
            {
                return m_TimeThresholdValue_spo2;
            }
        }
        /// <summary>
        /// 为统计血氧低于某界线值的时间而设定的
        /// </summary>
        private float m_SpO2ThresholdValue = 88;
        /// <summary>
        /// 为统计血氧低于某界线值的时间而设定的
        /// </summary>
        public float SpO2ThresholdValue
        {
            set
            {
                m_SpO2ThresholdValue = value;
            }
            get
            {
                return m_SpO2ThresholdValue;
            }
        }
        private double m_TimeThresholdValue_leg = 0.5;
        /// <summary>
        /// 判断腿动事件是否伴随微觉醒事件时间阈值
        /// </summary>
        public double TimeThresholdValue_leg
        {
            set
            {
                m_TimeThresholdValue_leg = value;
            }
            get
            {
                return m_TimeThresholdValue_leg;
            }
        }
        private int m_OxygenReduceRange = 3;
        /// <summary>
        /// 氧减事件判断血氧下降百分比阈值设定
        /// 默认为 3
        /// </summary>
        public int OxygenReduceRange
        {
            set
            {
                m_OxygenReduceRange = value;
            }
            get
            {
                return m_OxygenReduceRange;
            }
        }
    }
    /// <summary>
    ///  按帧划分的基础信息类
    /// </summary>
    public class DataEpoch
    {
        private int m_Index = 0;
        /// <summary>
        /// 索引序号 从0开始
        /// </summary>
        public int Index
        {
            set
            {
                m_Index = value;
            }
            get
            {
                return m_Index;
            }
        }
        private int m_Stage = 0;
        /// <summary>
        /// 睡眠分期状态值
        /// </summary>
        public int Stage
        {
            set
            {
                m_Stage = value;
                m_enumStage = value == 5 ? SleepStageEnum.W : value == 3 ? SleepStageEnum.N1 : value == 2 ? SleepStageEnum.N2 : value == 1 ? SleepStageEnum.N3 : value == 4 ? SleepStageEnum.R : SleepStageEnum.None;
            }
            get
            {
                return m_Stage;
            }
        }
        private SleepStageEnum m_enumStage = SleepStageEnum.None;
        /// <summary>
        /// 获取睡眠分期枚举状态
        /// </summary>
        public SleepStageEnum EnumStage
        {
            get
            {
                return m_enumStage;
            }
        }
        private int m_Pos = 0;
        /// <summary>
        /// 体位状态值
        /// </summary>
        public int Pos
        {
            set
            {
                m_Pos = value;
                m_enumPos = value == 1 ? BodyPosEnum.S : value == 2 ? BodyPosEnum.L : value == 3 ? BodyPosEnum.R : value == 4 ? BodyPosEnum.P : value == 5 ? BodyPosEnum.UP : BodyPosEnum.None;
            }
            get
            {
                return m_Pos;
            }
        }
        private BodyPosEnum m_enumPos = BodyPosEnum.None;
        /// <summary>
        /// 获取体位枚举状态
        /// </summary>
        public BodyPosEnum EnumPos
        {
            get
            {
                return m_enumPos;
            }
        }
        private int[] m_Spo2LevelTimes = new int[0];
        /// <summary>
        /// 存放阶段血氧时间数组
        /// </summary>
        public int[] Spo2LevelTimes
        {
            set
            {
                m_Spo2LevelTimes = value;
            }
            get
            {
                return m_Spo2LevelTimes;
            }
        }
        private float m_MaxSpO2 = 0;
        /// <summary>
        /// 最大血氧值
        /// </summary>
        public float MaxSpO2
        {
            set
            {
                m_MaxSpO2 = value;
            }
            get
            {
                return m_MaxSpO2;
            }
        }
        private float m_MinSpO2 = 0;
        /// <summary>
        /// 最小血氧值
        /// </summary>
        public float MinSpO2
        {
            set
            {
                m_MinSpO2 = value;
            }
            get
            {
                return m_MinSpO2;
            }
        }
        private float m_AveSpO2 = 0;
        /// <summary>
        /// 平均血氧值
        /// </summary>
        public float AveSpO2
        {
            set
            {
                m_AveSpO2 = value;
            }
            get
            {
                return m_AveSpO2;
            }
        }
        private float m_MaxHeartRate = 0;
        /// <summary>
        /// 最大脉率值
        /// </summary>
        public float MaxHeartRate
        {
            set
            {
                m_MaxHeartRate = value;
            }
            get
            {
                return m_MaxHeartRate;
            }
        }
        private float m_MinHeartRate = 0;
        /// <summary>
        /// 最小脉率值
        /// </summary>
        public float MinHeartRate
        {
            set
            {
                m_MinHeartRate = value;
            }
            get
            {
                return m_MinHeartRate;
            }
        }
        private int m_MaxHeartRateIndex = 0;
        /// <summary>
        /// 最大脉率值在帧内的序号
        /// </summary>
        public int MaxHeartRateIndex
        {
            set
            {
                m_MaxHeartRateIndex = value;
            }
            get
            {
                return m_MaxHeartRateIndex;
            }
        }
        private int m_MinHeartRateIndex = 0;
        /// <summary>
        /// 最小脉率值在帧内的序号
        /// </summary>
        public int MinHeartRateIndex
        {
            set
            {
                m_MinHeartRateIndex = value;
            }
            get
            {
                return m_MinHeartRateIndex;
            }
        }
        private float m_AveHeartRate = 0;
        /// <summary>
        /// 平均脉率值
        /// </summary>
        public float AveHeartRate
        {
            set
            {
                m_AveHeartRate = value;
            }
            get
            {
                return m_AveHeartRate;
            }
        }
        private float m_CA = 0;
        /// <summary>
        /// 中枢型呼吸暂停时间指标 s
        /// </summary>
        public float CA
        {
            set
            {
                m_CA = value;
            }
            get
            {
                return m_CA;
            }
        }
        private float m_OA = 0;
        /// <summary>
        /// 阻塞型呼吸暂停时间指标 s
        /// </summary>
        public float OA
        {
            set
            {
                m_OA = value;
            }
            get
            {
                return m_OA;
            }
        }
        private float m_MA = 0;
        /// <summary>
        /// 混合型呼吸暂停时间指标 s
        /// </summary>
        public float MA
        {
            set
            {
                m_MA = value;
            }
            get
            {
                return m_MA;
            }
        }

        private float m_MT = 0;
        /// <summary>
        /// 周期性循环腿动指标
        /// </summary>
        public float MT
        {
            set
            {
                m_MT = value;
            }
            get
            {
                return m_MT;
            }
        }
        private float m_Hypopnea = 0;
        /// <summary>
        /// 低通气时间指标 s
        /// </summary>
        public float Hypopnea
        {
            set
            {
                m_Hypopnea = value;
            }
            get
            {
                return m_Hypopnea;
            }
        }
        private float m_PLM = 0;
        /// <summary>
        /// 腿动指标
        /// </summary>
        public float PLM
        {
            set
            {
                m_PLM = value;
            }
            get
            {
                return m_PLM;
            }
        }
        private float m_PLMs = 0;
        /// <summary>
        /// 周期性循环腿动指标
        /// </summary>
        public float PLMs
        {
            set
            {
                m_PLMs = value;
            }
            get
            {
                return m_PLMs;
            }
        }
        private float m_MicArousal = 0;
        /// <summary>
        /// 周期性循环腿动指标
        /// </summary>
        public float MicArousal
        {
            set
            {
                m_MicArousal = value;
            }
            get
            {
                return m_MicArousal;
            }
        }       
    }
    /// <summary>
    /// 事件标记单元
    /// </summary>
    public class DataEvent
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { set; get; }
        /// <summary>
        /// 事件类型
        /// </summary>
        public EventTypeEnum EventTyp = EventTypeEnum.None;
        /// <summary>
        /// 持续时间
        /// </summary>
        public float Duration { set; get; }
        /// <summary>
        /// 开始索引号
        /// </summary>
        public int StartIndex { set; get; }
        /// <summary>
        /// 结束索引号
        /// </summary>
        public int EndIndex { set; get; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { set; get; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { set; get; }
        /// <summary>
        /// 所处在分期
        /// </summary>
        public int[] OnStages { set; get; }
        /// <summary>
        /// 所在体位状态
        /// </summary>
        public int[] OnPos { set; get; }
        /// <summary>
        /// Tag
        /// </summary>
        public object Tag { set; get; }
        /// <summary>
        /// Comments
        /// </summary>
        public object Comments { set; get; }
    }

}
