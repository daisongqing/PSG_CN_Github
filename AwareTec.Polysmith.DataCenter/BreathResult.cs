using AwareTec.Polysmith.DataBaseCom;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AwareTec.Polysmith.DataCenter
{
    internal class BreathResult
    {
        private IDataBase m_DataSource = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="datasource"></param>
        public BreathResult(IDataBase datasource)
        {
            m_DataSource = datasource;
            m_eventRecords = m_DataSource.BreathEvents;
        }
        private List<DataEvent> m_eventRecords = null;
        /// <summary>
        /// 获取指标结果
        /// </summary>
        /// <returns></returns>
        public Doc_BreathEventResult getResult(Doc_SystemSetting setting)
        {
            Doc_BreathEventResult breathEventResult = new Doc_BreathEventResult();
            breathEventResult.ModeType = setting.ModeType;
            breathEventResult.ApneaAndHypopneaCount = (float)this.m_eventRecords.Count;
            DataEvent maxLastHypopnea = (DataEvent)null;
            DataEvent maxLastObstructiveApnea = (DataEvent)null;
            DataEvent maxLastApnea = (DataEvent)null;
            int BrethTotalCnt = 0;
            int HypTotalCnt = 0;
            breathEventResult.CentralApneaAverageTimes = breathEventResult.CentralApneaNREMTotalCount = breathEventResult.CentralApneaREMTotalCount = breathEventResult.CentralApneaTotalCount = breathEventResult.CentralApneaTotalTimes = breathEventResult.CentralApneaWakeTotalCount = 0.0f;
            breathEventResult.MaxApneaDuration = 0.0f;
            breathEventResult.MaxCentralApneaDuration = breathEventResult.MaxHypopneaDuration = breathEventResult.MaxMixedApneaDuration = breathEventResult.MaxObstructiveApneaDuration = 0.0f;
            breathEventResult.MixedApneaAverageTimes = breathEventResult.MixedApneaNREMTotalCount = breathEventResult.MixedApneaREMTotalCount = breathEventResult.MixedApneaTotalCount = breathEventResult.MixedApneaTotalTimes = breathEventResult.MixedApneaWakeTotalCount = 0.0f;
            breathEventResult.ObstructiveApneaAverageTimes = breathEventResult.ObstructiveApneaNREMTotalCount = breathEventResult.ObstructiveApneaREMTotalCount = breathEventResult.ObstructiveApneaTotalCount = breathEventResult.ObstructiveApneaTotalTimes = breathEventResult.ObstructiveApneaWakeTotalCount = 0.0f;
            breathEventResult.HypopneaAverageTimes = breathEventResult.HypopneaIndex = breathEventResult.HypopneaNREMTotalCount = breathEventResult.HypopneaREMTotalCount = breathEventResult.HypopneaTotalCount = breathEventResult.HypopneaTotalTimes = breathEventResult.HypopneaWakeTotalCount = 0.0f;
            breathEventResult.ApneaAndHypopneaCount = breathEventResult.ApneaAndHypopneaIndex = breathEventResult.ApneaAverageTimes = breathEventResult.ApneaDurationSleepPecent = breathEventResult.ApneaIndex = breathEventResult.ApneaNREMTotalCount = breathEventResult.ApneaREMTotalCount = breathEventResult.ApneaTotalCount = breathEventResult.ApneaTotalTimes = breathEventResult.ApneaTurbidIndex = breathEventResult.ApneaWakeTotalCount = 0.0f;
            breathEventResult.ApneaHypopneaDegreeLevel = 0;
            breathEventResult.strMaxHYPDruationHappendTime = breathEventResult.strMaxApneaDruationHappendTime = "--:--:--";
            float m_maxApneaDrution = 0;
            for (int index = 0; index < this.m_eventRecords.Count; ++index)
            {
                DataEvent dataEvent = this.m_eventRecords[index];
                float duration = dataEvent.Duration;
                bool flag = dataEvent.StartTime >= this.m_DataSource.TSTStartTime && dataEvent.StartTime < this.m_DataSource.TSTEndTime || dataEvent.EndTime > this.m_DataSource.TSTStartTime && dataEvent.EndTime <= this.m_DataSource.TSTEndTime;
                if (flag)
                {
                    if (Enumerable.Count<int>(Enumerable.Where<int>((IEnumerable<int>)dataEvent.OnStages, (Func<int, bool>)(t =>
                    {
                        if (t != 4 && t != 3 && t != 2)
                            return t == 1;
                        return true;
                    }))) > 0)
                        ++BrethTotalCnt;
                    else
                        flag = false;
                }
                if (dataEvent.EventTyp == EventTypeEnum.Resp_CA)
                {
                    if (dataEvent.OnStages.Where(x => x == 5).Count() != dataEvent.OnStages.Count())
                    {
                        ++breathEventResult.CentralApneaTotalCount;
                        breathEventResult.CentralApneaTotalTimes += duration;
                        if ((double)breathEventResult.MaxCentralApneaDuration < (double)duration)
                            breathEventResult.MaxCentralApneaDuration = duration;
                        if (m_maxApneaDrution < duration)
                        {
                            maxLastApnea = dataEvent;
                            m_maxApneaDrution = duration;
                            breathEventResult.strMaxApneaDruationHappendTime = dataEvent.StartTime.ToString("HH:mm:ss");
                        }

                        if (Enumerable.Contains<int>((IEnumerable<int>)dataEvent.OnStages, 4))
                            ++breathEventResult.CentralApneaREMTotalCount;
                        if (Enumerable.Contains<int>((IEnumerable<int>)dataEvent.OnStages, 3) || Enumerable.Contains<int>((IEnumerable<int>)dataEvent.OnStages, 2) || Enumerable.Contains<int>((IEnumerable<int>)dataEvent.OnStages, 1))
                            ++breathEventResult.CentralApneaNREMTotalCount;
                    }
                    else
                    {
                        ++breathEventResult.CentralApneaWakeTotalCount;
                    }                                               
                }
                else if (dataEvent.EventTyp == EventTypeEnum.Resp_MA)
                {
                    if (dataEvent.OnStages.Where(x => x == 5).Count() != dataEvent.OnStages.Count())
                    {
                        ++breathEventResult.MixedApneaTotalCount;
                        breathEventResult.MixedApneaTotalTimes += duration;
                        if ((double)breathEventResult.MaxMixedApneaDuration < (double)duration)
                            breathEventResult.MaxMixedApneaDuration = duration;
                        if (m_maxApneaDrution < duration)
                        {
                            maxLastApnea = dataEvent;
                            m_maxApneaDrution = duration;
                            breathEventResult.strMaxApneaDruationHappendTime = dataEvent.StartTime.ToString("HH:mm:ss");
                        }

                        if (Enumerable.Contains<int>((IEnumerable<int>)dataEvent.OnStages, 4))
                            ++breathEventResult.MixedApneaREMTotalCount;
                        if (Enumerable.Contains<int>((IEnumerable<int>)dataEvent.OnStages, 3) || Enumerable.Contains<int>((IEnumerable<int>)dataEvent.OnStages, 2) || Enumerable.Contains<int>((IEnumerable<int>)dataEvent.OnStages, 1))
                            ++breathEventResult.MixedApneaNREMTotalCount;
                    }
                    else
                    {
                        ++breathEventResult.MixedApneaWakeTotalCount;
                    }                  
                }
                else if (dataEvent.EventTyp != EventTypeEnum.Resp_Hypnea)
                {
                    if (dataEvent.OnStages.Where(x => x == 5).Count() != dataEvent.OnStages.Count())
                    {
                        ++breathEventResult.ObstructiveApneaTotalCount;
                        breathEventResult.ObstructiveApneaTotalTimes += duration;
                        if ((double)breathEventResult.MaxObstructiveApneaDuration < (double)duration)
                        {
                            breathEventResult.MaxObstructiveApneaDuration = duration;
                            maxLastObstructiveApnea = dataEvent;
                        }
                        if (m_maxApneaDrution < duration)
                        {
                            maxLastApnea = dataEvent;
                            m_maxApneaDrution = duration;
                            breathEventResult.strMaxApneaDruationHappendTime = dataEvent.StartTime.ToString("HH:mm:ss");
                        }

                        if (Enumerable.Contains<int>((IEnumerable<int>)dataEvent.OnStages, 4))
                            ++breathEventResult.ObstructiveApneaREMTotalCount;
                        if (Enumerable.Contains<int>((IEnumerable<int>)dataEvent.OnStages, 3) || Enumerable.Contains<int>((IEnumerable<int>)dataEvent.OnStages, 2) || Enumerable.Contains<int>((IEnumerable<int>)dataEvent.OnStages, 1))
                            ++breathEventResult.ObstructiveApneaNREMTotalCount;
                    }
                    else
                    {
                        ++breathEventResult.ObstructiveApneaWakeTotalCount;
                    }
                }
                else
                {
                    if (dataEvent.OnStages.Where(x => x == 5).Count() != dataEvent.OnStages.Count())
                    {
                        if (flag)
                            ++HypTotalCnt;
                        ++breathEventResult.HypopneaTotalCount;
                        breathEventResult.HypopneaTotalTimes += duration;
                        if ((double)breathEventResult.MaxHypopneaDuration < (double)duration)
                        {
                            breathEventResult.MaxHypopneaDuration = duration;
                            breathEventResult.strMaxHYPDruationHappendTime = dataEvent.StartTime.ToString("HH:mm:ss");
                            maxLastHypopnea = dataEvent;
                        }

                        if (Enumerable.Contains<int>((IEnumerable<int>)dataEvent.OnStages, 4))
                            ++breathEventResult.HypopneaREMTotalCount;
                        if (Enumerable.Contains<int>((IEnumerable<int>)dataEvent.OnStages, 3) || Enumerable.Contains<int>((IEnumerable<int>)dataEvent.OnStages, 2) || Enumerable.Contains<int>((IEnumerable<int>)dataEvent.OnStages, 1))
                            ++breathEventResult.HypopneaNREMTotalCount;
                    }
                    else
                    {
                        ++breathEventResult.HypopneaWakeTotalCount;
                    }
                }
            }
            if ((double)breathEventResult.HypopneaTotalCount > 0.0)
            {
                breathEventResult.HypopneaAverageTimes = (float)Math.Round((double)breathEventResult.HypopneaTotalTimes / (double)breathEventResult.HypopneaTotalCount, 2);
                breathEventResult.HypopneaIndex = this.m_DataSource.TotalSleepTime > 0 ? (float)Math.Round((double)(HypTotalCnt * 60) / (double)this.m_DataSource.TotalSleepTime, 2) : 0;
            }
            if ((double)breathEventResult.CentralApneaTotalCount > 0.0)
                breathEventResult.CentralApneaAverageTimes = (float)Math.Round((double)breathEventResult.CentralApneaTotalTimes / (double)breathEventResult.CentralApneaTotalCount, 2);
            if ((double)breathEventResult.ObstructiveApneaTotalCount > 0.0)
                breathEventResult.ObstructiveApneaAverageTimes = (float)Math.Round((double)breathEventResult.ObstructiveApneaTotalTimes / (double)breathEventResult.ObstructiveApneaTotalCount, 2);
            if ((double)breathEventResult.MixedApneaTotalCount > 0.0)
                breathEventResult.MixedApneaAverageTimes = (float)Math.Round((double)breathEventResult.MixedApneaTotalTimes / (double)breathEventResult.MixedApneaTotalCount, 2);
            breathEventResult.ApneaWakeTotalCount = breathEventResult.CentralApneaWakeTotalCount + breathEventResult.MixedApneaWakeTotalCount + breathEventResult.ObstructiveApneaWakeTotalCount;
            breathEventResult.ApneaREMTotalCount = breathEventResult.CentralApneaREMTotalCount + breathEventResult.MixedApneaREMTotalCount + breathEventResult.ObstructiveApneaREMTotalCount;
            breathEventResult.ApneaNREMTotalCount = breathEventResult.CentralApneaNREMTotalCount + breathEventResult.MixedApneaNREMTotalCount + breathEventResult.ObstructiveApneaNREMTotalCount;
            breathEventResult.ApneaTotalTimes = breathEventResult.MixedApneaTotalTimes + breathEventResult.ObstructiveApneaTotalTimes + breathEventResult.MixedApneaTotalTimes;
            breathEventResult.MaxApneaDuration = m_maxApneaDrution;
            breathEventResult.ApneaDurationSleepPecent = (float)Math.Round((float)(breathEventResult.MixedApneaTotalTimes + breathEventResult.ObstructiveApneaTotalTimes + breathEventResult.MixedApneaTotalTimes) / this.m_DataSource.TotalSleepTime,2);
            breathEventResult.ApneaTotalCount = (float)this.m_eventRecords.Count - breathEventResult.HypopneaTotalCount;
            breathEventResult.ApneaTSTTotalCount = breathEventResult.ApneaTotalCount - breathEventResult.ApneaWakeTotalCount;
            breathEventResult.MixedApneaTSTTotalTimes = breathEventResult.MixedApneaTotalCount - breathEventResult.MixedApneaWakeTotalCount;
            breathEventResult.CentralApneaTSTTotalCount = breathEventResult.CentralApneaTotalCount - breathEventResult.CentralApneaWakeTotalCount;
            breathEventResult.ObstructiveApneaTSTTotalCount = breathEventResult.ObstructiveApneaTotalCount - breathEventResult.ObstructiveApneaWakeTotalCount;
            breathEventResult.HypopneaTSTTotalCount = breathEventResult.HypopneaTotalCount - breathEventResult.HypopneaWakeTotalCount;
            breathEventResult.ApneaAndHypopneaTSTCount = breathEventResult.ApneaTSTTotalCount + breathEventResult.HypopneaTSTTotalCount;
            breathEventResult.ObstructiveApneaTotalIndex = this.m_DataSource.TotalSleepTime > 0 ? (breathEventResult.ObstructiveApneaTSTTotalCount * 60) / this.m_DataSource.TotalSleepTime : 0;
            breathEventResult.CenterApneaTotalIndex = this.m_DataSource.TotalSleepTime > 0 ? (breathEventResult.CentralApneaTSTTotalCount * 60) / this.m_DataSource.TotalSleepTime : 0;
            breathEventResult.MixApneaTotalIndex = this.m_DataSource.TotalSleepTime > 0 ? (breathEventResult.MixedApneaTSTTotalTimes * 60) / this.m_DataSource.TotalSleepTime : 0;
            breathEventResult.HypopneaTotalIndex = this.m_DataSource.TotalSleepTime > 0 ? (breathEventResult.HypopneaTSTTotalCount * 60) / this.m_DataSource.TotalSleepTime : 0;
            breathEventResult.ApneaTotalIndex = this.m_DataSource.TotalSleepTime > 0 ? (breathEventResult.ApneaAndHypopneaTSTCount * 60) / this.m_DataSource.TotalSleepTime : 0;

            if ((double)this.m_DataSource.TotalSleepTime > 0.0)
                breathEventResult.ApneaIndex = (float)Math.Round((double)((BrethTotalCnt - HypTotalCnt) * 60) / (double)this.m_DataSource.TotalSleepTime, 2);
            if ((double)breathEventResult.ApneaTotalCount > 0.0)
            {
                breathEventResult.ApneaAverageTimes = (float)Math.Round((double)breathEventResult.ApneaTotalTimes / (double)breathEventResult.ApneaTotalCount, 2);
            }
            if ((double)this.m_DataSource.TotalSleepTime > 0.0)
                breathEventResult.ApneaAndHypopneaIndex = (float)Math.Round((double)(BrethTotalCnt * 60) / (double)this.m_DataSource.TotalSleepTime, 2);
            List<DataEvent> arousalEvents = this.m_DataSource.ArousalEvents;
            breathEventResult.RERAAverageTimes = 0.0f;
            breathEventResult.RERAIndex = breathEventResult.RERATotalTimes = 0.0f;
            breathEventResult.RERATotalCount = 0;
            breathEventResult.ApneaAndHypopneaTotalTimes = breathEventResult.ApneaTotalTimes + breathEventResult.HypopneaTotalTimes;
            breathEventResult.MaxRERADuration = 0;
            foreach (DataEvent dataEvent in arousalEvents)
            {
                DataEvent item = dataEvent;
                DateTime dt = item.StartTime.AddSeconds(0.0 - (double)this.m_DataSource.TimeThresholdValue_arousal);
                try
                {
                    //判断是否是呼吸努力相关的微觉醒 
                    //（计算规则：当前微觉醒事件往前m_TimeThresholdValue内是否有发生呼吸事件）
                    DataEvent exist = m_eventRecords.Find(t => (t.StartTime <= dt || (t.StartTime > dt && t.StartTime < item.StartTime)) && t.EndTime >= dt);
                    if (exist != null)
                    {
                        if (breathEventResult.MaxRERADuration < exist.Duration)
                            breathEventResult.MaxRERADuration = exist.Duration;
                        if (exist.OnStages.Contains((int)SleepStageEnum.R))
                        {
                            breathEventResult.RERAInREMTotalCount++;
                            breathEventResult.RERAInREMTotalTimes += exist.Duration;
                        }
                        if (exist.OnStages.Contains((int)SleepStageEnum.N1) || exist.OnStages.Contains((int)SleepStageEnum.N2) || exist.OnStages.Contains((int)SleepStageEnum.N3))
                        {
                            breathEventResult.RERAInNREMTotalCount++;
                            breathEventResult.RERAInNREMTotalTimes += exist.Duration;
                        }
                        ++breathEventResult.RERATotalCount;
                        breathEventResult.RERATotalTimes += item.Duration;
                    }
                }
                catch
                {
                }
            }
            if ((double)this.m_DataSource.TotalSleepTime > 0.0)
            {
                breathEventResult.ObstructiveApneaIndex = (float)Math.Round((double)(breathEventResult.ObstructiveApneaTotalCount * 60) / (double)this.m_DataSource.TotalSleepTime, 2);
                breathEventResult.OAHCount = breathEventResult.ObstructiveApneaTotalCount + breathEventResult.HypopneaTotalCount;
                breathEventResult.OAHIndex = (float)Math.Round((double)(breathEventResult.OAHCount * 60) / (double)this.m_DataSource.TotalSleepTime, 2);
                breathEventResult.CenterApneaIndex = (float)Math.Round((double)(breathEventResult.CentralApneaTotalCount * 60) / (double)this.m_DataSource.TotalSleepTime, 2);
                breathEventResult.MixApneaIndex = (float)Math.Round((double)(breathEventResult.MixedApneaTotalCount * 60) / (double)this.m_DataSource.TotalSleepTime, 2);
                if (m_DataSource.NRemTotalTimes > 0)
                {
                    breathEventResult.NREMApneaIndex = (float)Math.Round((double)((breathEventResult.CentralApneaNREMTotalCount + breathEventResult.MixedApneaNREMTotalCount + breathEventResult.ObstructiveApneaNREMTotalCount) * 60) / (double)this.m_DataSource.NRemTotalTimes, 2);
                    breathEventResult.NREMObstructiveApneaIndex = (float)Math.Round((double)(breathEventResult.ObstructiveApneaNREMTotalCount * 60) / (double)this.m_DataSource.NRemTotalTimes, 2);
                    breathEventResult.NREMCenterApneaIndex = (float)Math.Round((double)(breathEventResult.CentralApneaNREMTotalCount * 60) / (double)this.m_DataSource.NRemTotalTimes, 2);
                    breathEventResult.NREMMixApneaIndex = (float)Math.Round((double)(breathEventResult.MixedApneaNREMTotalCount * 60) / (double)this.m_DataSource.NRemTotalTimes, 2);
                    breathEventResult.NREMHypopneaIndex = (float)Math.Round((double)(breathEventResult.HypopneaNREMTotalCount * 60) / (double)this.m_DataSource.NRemTotalTimes, 2);
                    breathEventResult.NremRERAIndex = (float)Math.Round((double)(breathEventResult.RERAInNREMTotalCount * 60) / (double)this.m_DataSource.NRemTotalTimes, 2);
                }
                if (m_DataSource.RemTotalTimes > 0)
                {
                    breathEventResult.REMApneaIndex = (float)Math.Round((double)((breathEventResult.CentralApneaREMTotalCount + breathEventResult.MixedApneaREMTotalCount + breathEventResult.ObstructiveApneaREMTotalCount) * 60) / (double)this.m_DataSource.RemTotalTimes, 2);
                    breathEventResult.REMObstructiveApneaIndex = (float)Math.Round((double)(breathEventResult.ObstructiveApneaREMTotalCount * 60) / (double)this.m_DataSource.RemTotalTimes, 2);
                    breathEventResult.REMCenterApneaIndex = (float)Math.Round((double)(breathEventResult.CentralApneaREMTotalCount * 60) / (double)this.m_DataSource.RemTotalTimes, 2);
                    breathEventResult.REMMixApneaIndex = (float)Math.Round((double)(breathEventResult.MixedApneaREMTotalCount * 60) / (double)this.m_DataSource.RemTotalTimes, 2);
                    breathEventResult.REMHypopneaIndex = (float)Math.Round((double)(breathEventResult.HypopneaREMTotalCount * 60) / (double)this.m_DataSource.RemTotalTimes, 2);
                    breathEventResult.RemRERAIndex = (float)Math.Round((double)(breathEventResult.RERAInREMTotalCount * 60) / (double)this.m_DataSource.RemTotalTimes, 2);
                }
                breathEventResult.RERAIndex = (float)Math.Round((double)(breathEventResult.RERATotalCount * 60) / (double)this.m_DataSource.TotalSleepTime, 2);
                breathEventResult.NREMApneaAndHypopneaIndex = breathEventResult.NREMObstructiveApneaIndex + breathEventResult.NREMCenterApneaIndex + breathEventResult.NREMMixApneaIndex + breathEventResult.NREMHypopneaIndex;
                breathEventResult.REMApneaAndHypopneaIndex = breathEventResult.REMObstructiveApneaIndex + breathEventResult.REMCenterApneaIndex + breathEventResult.REMMixApneaIndex + breathEventResult.REMHypopneaIndex;

                breathEventResult.NREMApneaTurbidIndex = breathEventResult.NREMApneaAndHypopneaIndex + breathEventResult.NremRERAIndex;
                breathEventResult.REMApneaTurbidIndex = breathEventResult.REMApneaAndHypopneaIndex + breathEventResult.RemRERAIndex;
            }
            if (breathEventResult.RERATotalCount > 0)
                breathEventResult.RERAAverageTimes = (float)Math.Round((double)breathEventResult.RERATotalTimes / (double)breathEventResult.RERATotalCount, 2);
            if ((double)this.m_DataSource.TotalSleepTime > 0.0)                ///计算RDI 呼吸事件次数+微觉醒次数之和除TST  TST单位是分钟，需要换算成小时
                breathEventResult.ApneaTurbidIndex = (float)Math.Round((double)((BrethTotalCnt + breathEventResult.RERATotalCount) * 60) / (double)this.m_DataSource.TotalSleepTime, 2);
            List<DataEvent> spO2Events = this.m_DataSource.SpO2Events.Where(t => t.EventTyp != EventTypeEnum.Spo2Artifact).ToList();
            if (maxLastHypopnea != null)
            {
                DateTime dt2 = maxLastHypopnea.EndTime.AddSeconds((double)this.m_DataSource.TimeThresholdValue_hyp);
                try
                {
                    try
                    {
                        //判断是否是最长低通气发生时的最低氧减事件
                        //（计算规则：当前低通气事件从开始时间以及往前后m_TimeThresholdValue_hyp内是否有发生氧减）
                        var exist = spO2Events.Where(t => t.StartTime > maxLastHypopnea.StartTime && t.StartTime < dt2);
                        if (exist != null)
                        {
                            m_minSpO2ValueByHypopnea = exist.Select(t => Convert.ToSingle(t.Tag)).Min();
                        }
                    }
                    catch { }
                }
                catch
                {
                }
            }
            if (maxLastObstructiveApnea != null)
            {
                DateTime dt2 = maxLastObstructiveApnea.EndTime.AddSeconds((double)this.m_DataSource.TimeThresholdValue_apn);
                try
                {
                    //判断是否是最长阻塞型呼吸暂停发生时的最低氧减事件
                    //（计算规则：当前阻塞型呼吸暂停事件从开始时间以及往前后m_TimeThresholdValue_apn内是否有发生氧减）
                    var exist = spO2Events.Where(t => t.StartTime > maxLastObstructiveApnea.StartTime && t.StartTime < dt2);
                    if (exist != null)
                    {
                        m_minSpO2ValueByOA = exist.Select(t => Convert.ToSingle(t.Tag)).Min();
                    }
                }
                catch
                {
                }
            }
            if (maxLastApnea != null)
            {
                DateTime dt2 = maxLastApnea.EndTime.AddSeconds((double)this.m_DataSource.TimeThresholdValue_apn);
                try
                {
                    var exist = spO2Events.Where(t => t.StartTime > maxLastApnea.StartTime && t.StartTime < dt2);
                    if (exist != null)
                        this.m_minSpO2ValueByApnea = exist.Select(t => Convert.ToSingle(t.Tag)).Min();
                }
                catch
                {
                }
            }

            if (breathEventResult.MaxApneaDuration == 0 && breathEventResult.MaxHypopneaDuration == 0)
            {
                breathEventResult.strDescriptionOfBreathEvent = "";
            }
            else
            {
                if (breathEventResult.MaxApneaDuration > breathEventResult.MaxHypopneaDuration)
                {
                    if (breathEventResult.MaxCentralApneaDuration >= breathEventResult.MaxMixedApneaDuration && breathEventResult.MaxCentralApneaDuration >= breathEventResult.MaxObstructiveApneaDuration)
                        breathEventResult.strDescriptionOfBreathEvent = string.Format("睡眠期间最长的呼吸异常事件是中枢性呼吸暂停,中枢性呼吸暂停事件最长持续时间为{0}秒,伴随最低血氧饱和度为{1} %。", breathEventResult.MaxCentralApneaDuration, minSpO2ValueByApnea);
                    else if (breathEventResult.MaxObstructiveApneaDuration >= breathEventResult.MaxMixedApneaDuration && breathEventResult.MaxObstructiveApneaDuration >= breathEventResult.MaxCentralApneaDuration)
                        breathEventResult.strDescriptionOfBreathEvent = string.Format("睡眠期间最长的呼吸异常事件是阻塞性呼吸暂停,阻塞性呼吸暂停事件最长持续时间为{0}秒,伴随最低血氧饱和度为{1} %。", breathEventResult.MaxObstructiveApneaDuration, minSpO2ValueByApnea);
                    else
                        breathEventResult.strDescriptionOfBreathEvent = string.Format("睡眠期间最长的呼吸异常事件是混合性呼吸暂停,混合性呼吸暂停事件最长持续时间为{0}秒,伴随最低血氧饱和度为{1} %。", breathEventResult.MaxMixedApneaDuration, minSpO2ValueByApnea);
                }
                else if(breathEventResult.MaxApneaDuration == breathEventResult.MaxHypopneaDuration)
                {
                    if (breathEventResult.MaxCentralApneaDuration >= breathEventResult.MaxMixedApneaDuration && breathEventResult.MaxCentralApneaDuration >= breathEventResult.MaxObstructiveApneaDuration)
                        breathEventResult.strDescriptionOfBreathEvent = string.Format("睡眠期间最长的呼吸异常事件是中枢性呼吸暂停和低通气,中枢性呼吸暂停事件最长持续时间为{0}秒,伴随最低血氧饱和度为{1} %。低通气事件最长持续时间为{2}秒,伴随最低血氧饱和度为{3} %。", breathEventResult.MaxCentralApneaDuration, minSpO2ValueByApnea, breathEventResult.MaxHypopneaDuration, m_minSpO2ValueByHypopnea);
                    else if (breathEventResult.MaxObstructiveApneaDuration >= breathEventResult.MaxMixedApneaDuration && breathEventResult.MaxObstructiveApneaDuration >= breathEventResult.MaxCentralApneaDuration)
                        breathEventResult.strDescriptionOfBreathEvent = string.Format("睡眠期间最长的呼吸异常事件是阻塞性呼吸暂停和低通气,阻塞性呼吸暂停事件最长持续时间为{0}秒,伴随最低血氧饱和度为{1} %。低通气事件最长持续时间为{2}秒,伴随最低血氧饱和度为{3} %。", breathEventResult.MaxObstructiveApneaDuration, minSpO2ValueByApnea, breathEventResult.MaxHypopneaDuration, m_minSpO2ValueByHypopnea);
                    else
                        breathEventResult.strDescriptionOfBreathEvent = string.Format("睡眠期间最长的呼吸异常事件是混合性呼吸暂停和低通气,混合性呼吸暂停事件最长持续时间为{0}秒,伴随最低血氧饱和度为{1} %。低通气事件最长持续时间为{2}秒,伴随最低血氧饱和度为{3} %。", breathEventResult.MaxMixedApneaDuration, minSpO2ValueByApnea, breathEventResult.MaxHypopneaDuration, m_minSpO2ValueByHypopnea);
                }
                else
                {
                    breathEventResult.strDescriptionOfBreathEvent = string.Format("睡眠期间最长的呼吸异常事件是低通气,低通气事件最长持续时间为{0}秒,伴随最低血氧饱和度为{1} %。", breathEventResult.MaxHypopneaDuration, m_minSpO2ValueByHypopnea);
                }
            }
            return breathEventResult;
        }

        private float m_minSpO2ValueByHypopnea = 0;
        /// <summary>
        /// 最长低通气发生时伴随的血氧值 
        /// </summary>
        public float minSpO2ValueByHypopnea
        {
            get
            {
                return m_minSpO2ValueByHypopnea;
            }
        }
        private float m_minSpO2ValueByOA = 0;
        /// <summary>
        /// 最长阻塞型呼吸暂停发生时伴随的血氧值 
        /// </summary>
        public float minSpO2ValueByOA
        {
            get
            {
                return m_minSpO2ValueByOA;
            }
        }
        private float m_minSpO2ValueByApnea = 0;
        /// <summary>
        /// 最长呼吸暂停发生时伴随的血氧值 
        /// </summary>
        public float minSpO2ValueByApnea
        {
            get
            {
                return m_minSpO2ValueByApnea;
            }
        }
    }
}
