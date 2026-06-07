using AwareTec.Polysmith.DataBaseCom;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AwareTec.Polysmith.DataCenter
{
    /// <summary>
    /// 睡眠指标
    /// </summary>
    internal class SleepResult
    {
        private IDataBase m_DataSource = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="datasource"></param>
        public SleepResult(IDataBase datasource)
        {
            m_DataSource = datasource;
        }
        /// <summary>
        /// 获取结果
        /// </summary>
        /// <returns></returns>
        public Doc_SleepResult getReuslt()
        {
            Doc_SleepResult result = new Doc_SleepResult();
            float OneEpochMins = 0.5f;
            result.StartRecordTime = this.m_DataSource.StartRecordTime;
            result.strStartRecordTime = this.m_DataSource.StartRecordTime.ToString("yyyy年MM月dd日");
            result.TotalSleepTime = this.m_DataSource.TotalSleepTime;
            result.LightOffTime = this.m_DataSource.StartRecordTime.AddSeconds((double)((this.m_DataSource.StartFrameNo - 1) * 30));
            result.LightOnTime = this.m_DataSource.StartRecordTime.AddSeconds((double)(this.m_DataSource.EndFrameNo * 30));
            result.strLightOffTime = result.LightOffTime.ToString("F");
            result.strLightOnTime = result.LightOnTime.ToString("F");
            if (result.LightOnTime > m_DataSource.ValidEndTime)
                result.LightOnTime = m_DataSource.ValidEndTime;
            result.TimeInBed = (float)Math.Round((double)this.m_DataSource.TIBEpochs.Count * 0.5, 2);
            result.SleepEfficiency = (float)Math.Round((double)(result.TotalSleepTime / result.TimeInBed * 100f), 2);
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            int num2 = 0;
            float[] values = new float[]
            {
                -1f,
                -1f,
                -1f,
                -1f
            };
            for (int i = 0; i < this.m_DataSource.TIBEpochs.Count; i++)
            {
                SleepStageEnum enumStage = this.m_DataSource.TIBEpochs[i].EnumStage;
                if (enumStage == SleepStageEnum.N1 && !flag)
                {
                    result.N1SleepLatencyFromLightOn = (float)i * OneEpochMins;
                    values[0] = result.N1SleepLatencyFromLightOn;
                    flag = true;
                    num2++;
                }
                else if (enumStage == SleepStageEnum.N2 && !flag2)
                {
                    result.N2SleepLatencyFromLightOn = (float)i * OneEpochMins;
                    values[1] = result.N2SleepLatencyFromLightOn;
                    flag2 = true;
                    num2++;
                }
                else if (enumStage == SleepStageEnum.N3 && !flag3)
                {
                    result.N3SleepLatencyFromLightOn = (float)i * OneEpochMins;
                    values[2] = result.N3SleepLatencyFromLightOn;
                    flag3 = true;
                    num2++;
                }
                else if (enumStage == SleepStageEnum.R && !flag4)
                {
                    result.REMSleepLatencyFromLightOn = (float)i * OneEpochMins;
                    values[3] = result.REMSleepLatencyFromLightOn;
                    flag4 = true;
                    num2++;
                }
                if (num2 == 4)
                {
                    break;
                }
            }
            bool flag6 = false;
            ///连续的清醒片段数量
            int weekContinueCnt = 0;
            float TotalWeekTimes = 0f;
            int num5 = 0;
            int TsT = 0;
            result.LatencyOfPersistentSleep = -1f;
            SleepStageEnum m_lastStage = SleepStageEnum.None;
            foreach (DataEpoch dataEpoch in this.m_DataSource.TSTEpochs)
            {
                TsT++;
                if (dataEpoch.EnumStage == SleepStageEnum.W)
                {
                    TotalWeekTimes += OneEpochMins;
                    if (m_lastStage != dataEpoch.EnumStage)
                    {
                        weekContinueCnt++;
                        result.WeekCountInSleepTime++;
                        num5 = 0;
                    }
                }
                else if (dataEpoch.EnumStage == SleepStageEnum.N1)
                {
                    if (m_lastStage != dataEpoch.EnumStage)
                    {
                        result.N1CountInSleepTime++;
                    }
                    result.N1OfSleepDuration += OneEpochMins;
                }
                else if (dataEpoch.EnumStage == SleepStageEnum.N2)
                {
                    if (m_lastStage != dataEpoch.EnumStage)
                    {
                        result.N2CountInSleepTime++;
                    }
                    result.N2OfSleepDuration += OneEpochMins;
                }
                else if (dataEpoch.EnumStage == SleepStageEnum.N3)
                {
                    if (m_lastStage != dataEpoch.EnumStage)
                    {
                        result.N3CountInSleepTime++;
                    }
                    result.N3OfSleepDuration += OneEpochMins;
                }
                else if (dataEpoch.EnumStage == SleepStageEnum.R)
                {
                    if (m_lastStage != dataEpoch.EnumStage)
                    {
                        result.REMCountInSleepTime++;
                    }
                    result.REMOfSleepDuration += OneEpochMins;
                }
                if (dataEpoch.EnumStage != SleepStageEnum.W && !flag6)
                {
                    num5++;
                    if (num5 >= 20)
                    {
                        result.LatencyOfPersistentSleep = (float)(TsT - 20) * 0.5f;
                        flag6 = true;
                    }
                }
                m_lastStage = dataEpoch.EnumStage;
            }
            result.SleepPeriodTime = result.TotalSleepTime + TotalWeekTimes;
            try
            {
                result.SleepLatencyTimesFromLightOn = values.Where(t => t != -1).Min();
            }
            catch
            {
            }
            if (result.REMSleepLatencyFromLightOn > 0)
                result.REMSleepLatencyFromLightOn -= result.SleepLatencyTimesFromLightOn;///r期的睡眠潜伏期是从入睡帧到第一个r期帧
            result.LatencyOfPersistentSleep += result.SleepLatencyTimesFromLightOn;
            result.WakeAfterSleepTimes = result.TimeInBed - result.SleepLatencyTimesFromLightOn - result.TotalSleepTime;
            result.AwakeningTimes = result.TimeInBed - result.TotalSleepTime;
            result.WKOfSleepDuration = TotalWeekTimes;
            //result.WeekCountInSleepTime = (int)(result.WKOfSleepDuration / 0.5f);
            //result.N1CountInSleepTime = (int)(result.N1OfSleepDuration / 0.5f);
            //result.N2CountInSleepTime = (int)(result.N2OfSleepDuration / 0.5f);
            //result.N3CountInSleepTime = (int)(result.N3OfSleepDuration / 0.5f);
            //result.REMCountInSleepTime = (int)(result.REMOfSleepDuration / 0.5f);

            result.NREMCountInSleepTime = result.N1CountInSleepTime + result.N2CountInSleepTime + result.N3CountInSleepTime;
            result.NREMOfSleepDuration = result.N1OfSleepDuration + result.N2OfSleepDuration + result.N3OfSleepDuration;

            result.WKOfSleepTimeInBedPencent = (float)Math.Round((double)(result.WKOfSleepDuration / result.TimeInBed * 100f), 2);
            result.N1OfSleepTimeInBedPencent = (float)Math.Round((double)(result.N1OfSleepDuration / result.TimeInBed * 100f), 2);
            result.N2OfSleepTimeInBedPencent = (float)Math.Round((double)(result.N2OfSleepDuration / result.TimeInBed * 100f), 2);
            result.N3OfSleepTimeInBedPencent = (float)Math.Round((double)(result.N3OfSleepDuration / result.TimeInBed * 100f), 2);
            result.NREMOfSleepTimeInBedPencent = (float)Math.Round((double)(result.NREMOfSleepDuration / result.TimeInBed * 100f), 2);
            result.REMOfSleepTimeInBedPencent = (float)Math.Round((double)(result.REMOfSleepDuration / result.TimeInBed * 100f), 2);
            if (this.m_DataSource.TotalSleepTime > 0f)
            {
                result.N1OfSleepTotalTimePencent = (float)Math.Round((double)(result.N1OfSleepDuration / result.TotalSleepTime * 100f), 2);
                result.N2OfSleepTotalTimePencent = (float)Math.Round((double)(result.N2OfSleepDuration / result.TotalSleepTime * 100f), 2);
                result.N3OfSleepTotalTimePencent = (float)Math.Round((double)(result.N3OfSleepDuration / result.TotalSleepTime * 100f), 2);
                result.REMOfSleepTotalTimePencent = (float)Math.Round((double)(result.REMOfSleepDuration / result.TotalSleepTime * 100f), 2);
                result.NREMOfSleepTotalTimePencent = (float)Math.Round((double)(result.NREMOfSleepDuration / result.TotalSleepTime * 100f), 2);
            }
            result.WKOfSleepPeriodTimePencent = (float)Math.Round((double)(result.WKOfSleepDuration / result.SleepPeriodTime * 100f), 2);
            result.N1OfSleepPeriodTimePencent = (float)Math.Round((double)(result.N1OfSleepDuration / result.SleepPeriodTime * 100f), 2);
            result.N2OfSleepPeriodTimePencent = (float)Math.Round((double)(result.N2OfSleepDuration / result.SleepPeriodTime * 100f), 2);
            result.N3OfSleepPeriodTimePencent = (float)Math.Round((double)(result.N3OfSleepDuration / result.SleepPeriodTime * 100f), 2);
            result.REMOfSleepPeriodTimePencent = (float)Math.Round((double)(result.REMOfSleepDuration / result.SleepPeriodTime * 100f), 2);
            result.NREMOfSleepPeriodTimePencent = (float)Math.Round((double)(result.NREMOfSleepDuration / result.SleepPeriodTime * 100f), 2);
            result.ArousalCount = (float)weekContinueCnt;
            result.MicroArousalCount = (float)this.m_DataSource.ArousalEvents.Count;
            foreach (DataEvent item in m_DataSource.ArousalEvents)
            {
                bool NRemReady = false;
                switch (item.EventTyp)
                {
                    case EventTypeEnum.Arousal:
                        result.NorMicroArousalCount++;
                        if (item.OnStages.Contains((int)SleepStageEnum.R))
                        {
                            result.REMMicroArousalCount++;
                            result.REMNorMicroArousalCount++;
                        }
                        if(item.OnStages.Contains((int)SleepStageEnum.N1))
                        {
                            NRemReady = true;
                            result.N1MicroArousalCount++;
                            result.N1NorMicroArousalCount++;
                        }
                        if (item.OnStages.Contains((int)SleepStageEnum.N2))
                        {
                            NRemReady = true;
                            result.N2MicroArousalCount++;
                            result.N2NorMicroArousalCount++;
                        }
                        if (item.OnStages.Contains((int)SleepStageEnum.N3))
                        {
                            NRemReady = true;
                            result.N3MicroArousalCount++;
                            result.N3NorMicroArousalCount++;
                        }
                        if (NRemReady)
                        {
                            result.NREMMicroArousalCount++;
                            result.NREMNorMicroArousalCount++;
                        }
                        break;
                    case EventTypeEnum.LmArousal:
                        result.LmMicroArousalCount++;
                        if (item.OnStages.Contains((int)SleepStageEnum.R))
                        {
                            result.REMMicroArousalCount++;
                            result.REMLmMicroArousalCount++;
                        }
                        if (item.OnStages.Contains((int)SleepStageEnum.N1))
                        {
                            NRemReady = true;
                            result.N1MicroArousalCount++;
                            result.N1LmMicroArousalCount++;
                        }
                        if (item.OnStages.Contains((int)SleepStageEnum.N2))
                        {
                            NRemReady = true;
                            result.N2MicroArousalCount++;
                            result.N2LmMicroArousalCount++;
                        }
                        if (item.OnStages.Contains((int)SleepStageEnum.N3))
                        {
                            NRemReady = true;
                            result.N3MicroArousalCount++;
                            result.N3LmMicroArousalCount++;
                        }
                        if (NRemReady)
                        {
                            result.NREMMicroArousalCount++;
                            result.NREMLmMicroArousalCount++;
                        }
                        break;
                    case EventTypeEnum.PlmArousal:
                        result.PlmMicroArousalCount++;
                        if (item.OnStages.Contains((int)SleepStageEnum.R))
                        {
                            result.REMMicroArousalCount++;
                            result.REMPlmMicroArousalCount++;
                        }
                        if (item.OnStages.Contains((int)SleepStageEnum.N1))
                        {
                            NRemReady = true;
                            result.N1MicroArousalCount++;
                            result.N1PlmMicroArousalCount++;
                        }
                        if (item.OnStages.Contains((int)SleepStageEnum.N2))
                        {
                            NRemReady = true;
                            result.N2MicroArousalCount++;
                            result.N2PlmMicroArousalCount++;
                        }
                        if (item.OnStages.Contains((int)SleepStageEnum.N3))
                        {
                            NRemReady = true;
                            result.N3MicroArousalCount++;
                            result.N3PlmMicroArousalCount++;
                        }
                        if (NRemReady)
                        {
                            result.NREMMicroArousalCount++;
                            result.NREMPlmMicroArousalCount++;
                        }
                        break;
                    case EventTypeEnum.RespArousal:
                        result.RespMicroArousalCount++;
                        if (item.OnStages.Contains((int)SleepStageEnum.R))
                        {
                            result.REMMicroArousalCount++;
                            result.REMRespMicroArousalCount++;
                        }
                        if (item.OnStages.Contains((int)SleepStageEnum.N1))
                        {
                            NRemReady = true;
                            result.N1MicroArousalCount++;
                            result.N1RespMicroArousalCount++;
                        }
                        if (item.OnStages.Contains((int)SleepStageEnum.N2))
                        {
                            NRemReady = true;
                            result.N2MicroArousalCount++;
                            result.N2RespMicroArousalCount++;
                        }
                        if (item.OnStages.Contains((int)SleepStageEnum.N3))
                        {
                            NRemReady = true;
                            result.N3MicroArousalCount++;
                            result.N3RespMicroArousalCount++;
                        }
                        if (NRemReady)
                        {
                            result.NREMMicroArousalCount++;
                            result.NREMRespMicroArousalCount++;
                        }
                        break;
                }
            }
            if (this.m_DataSource.TotalSleepTime > 0f)
            {
                result.MicroArousalIndex = (float)Math.Round((double)(result.MicroArousalCount * 60f / result.TotalSleepTime), 2);

                result.NorMicroArousalIndex = (float)Math.Round((double)(result.NorMicroArousalCount * 60f / result.TotalSleepTime), 2);
                result.LmMicroArousalIndex = (float)Math.Round((double)(result.LmMicroArousalCount * 60f / result.TotalSleepTime), 2);
                result.PlmMicroArousalIndex = (float)Math.Round((double)(result.PlmMicroArousalCount * 60f / result.TotalSleepTime), 2);
                result.RespMicroArousalIndex = (float)Math.Round((double)(result.RespMicroArousalCount * 60f / result.TotalSleepTime), 2);
            }
            if (m_DataSource.RemTotalTimes > 0)
            {
                result.REMMicroArousalIndex = (float)Math.Round((double)(result.REMMicroArousalCount * 60f / m_DataSource.RemTotalTimes), 2);

                result.REMNorMicroArousalIndex = (float)Math.Round((double)(result.REMNorMicroArousalCount * 60f / m_DataSource.RemTotalTimes), 2);
                result.REMLmMicroArousalIndex = (float)Math.Round((double)(result.REMLmMicroArousalCount * 60f / m_DataSource.RemTotalTimes), 2);
                result.REMPlmMicroArousalIndex = (float)Math.Round((double)(result.REMPlmMicroArousalCount * 60f / m_DataSource.RemTotalTimes), 2);
                result.REMRespMicroArousalIndex = (float)Math.Round((double)(result.REMRespMicroArousalCount * 60f / m_DataSource.RemTotalTimes), 2);
            }
            if (m_DataSource.NRemTotalTimes > 0)
            {
                result.NREMMicroArousalIndex = (float)Math.Round((double)(result.NREMMicroArousalCount * 60f / m_DataSource.NRemTotalTimes), 2);

                result.NREMNorMicroArousalIndex = (float)Math.Round((double)(result.NREMNorMicroArousalCount * 60f / m_DataSource.NRemTotalTimes), 2);
                result.NREMLmMicroArousalIndex = (float)Math.Round((double)(result.NREMLmMicroArousalCount * 60f / m_DataSource.NRemTotalTimes), 2);
                result.NREMPlmMicroArousalIndex = (float)Math.Round((double)(result.NREMPlmMicroArousalCount * 60f / m_DataSource.NRemTotalTimes), 2);
                result.NREMRespMicroArousalIndex = (float)Math.Round((double)(result.NREMRespMicroArousalCount * 60f / m_DataSource.NRemTotalTimes), 2);
            }
            result.TotalRecordTime = (float)Math.Round((result.EndRecordTime - result.StartRecordTime).TotalMinutes, 2);
            #region 多次小睡的指标计算
            result.TotalMultipleSleepCount = m_DataSource.MulSleepEvents.Count;
            if (result.TotalMultipleSleepCount > 0)
            {
                List<DataEvent> mults = m_DataSource.MulSleepEvents.OrderBy(t => t.StartTime).ToList();
                result.MultipleSleepStartTime = mults[0].StartTime.ToString("HH:mm");
                result.SingleSleepDuration = mults[0].Duration;
                float minspan = 0, maxspan = 0;
                int remCnt = 0;
                for (int i = 0; i < result.TotalMultipleSleepCount; i++)
                {
                    string range = string.Format("{0}-{1}", mults[i].StartTime.ToString("HH:mm"), mults[i].EndTime.ToString("HH:mm"));
                    float SL = mults[i].OnStages.Length * 0.5f, RSL = -1;
                    bool SL_ini = false;
                    int RemCnt = 0;
                    int rIndex = 0, bak_rIndex = 0;
                    for (int s = 0; s < mults[i].OnStages.Length; s++)
                    {

                        if (mults[i].OnStages[s] != (int)SleepStageEnum.W)
                        {
                            if (!SL_ini)
                            {
                                SL = s * 0.5f;
                                SL_ini = true;
                            }
                            if (mults[i].OnStages[s] == (int)SleepStageEnum.R)
                            {
                                if(RemCnt==0)
                                {
                                    //第一次计数无法通过差值计数，所以这里主动记一次，并把bak_rIndex置成当前r帧序号
                                    RemCnt = 1;
                                    bak_rIndex = s;
                                }
                                rIndex = s;///记录当前r期的帧索引
                                if (rIndex - bak_rIndex > 1)///如果当前r期索引跟最后一次r期帧索引不连续就结束一次r期计数
                                {
                                    RemCnt++;
                                }
                                if (RSL < 0)
                                {
                                    RSL = s * 0.5f;
                                    if (RSL <= 15)///SOREMPsCount的统计规则是入睡后15min内进入REM
                                        result.SOREMPsCount++;
                                }
                                bak_rIndex = rIndex;
                            }
                        }
                    }
                    result.AverageMultipleSleepLatencyTimes += SL;
                    if (i == 0)
                    {
                        result.FirstSleepTimeRange = range;
                        result.FirstSleepLatencyTimesFromLightOff = SL;
                        result.FirstRemSleepLatencyTimesFromLightOff = RSL;
                        if (RSL > -1)
                        {
                            result.FirstRemSleepLatencyTimesFromLightOff -= SL;
                            result.AverageMultipleSleepRemLatencyTimes += RSL;
                            remCnt++;
                            result.FirstSleepRemCounts = RemCnt;
                        }
                        result.FirstTotalSleepTimes = (float)Math.Round(mults[i].Duration / 60, 1);
                        result.FirstWakeTimes = mults[i].OnStages.Where(t => t == (int)SleepStageEnum.W || t == (int)SleepStageEnum.None).Count()*0.5f;
                        result.FirstDeepSleepTimes = result.FirstTotalSleepTimes - result.FirstWakeTimes;
                    }
                    else
                    {
                        if (result.SingleSleepDuration > mults[i].Duration)
                            result.SingleSleepDuration = mults[i].Duration;
                        float value = (float)Math.Round((mults[i].StartTime - mults[i - 1].EndTime).TotalMinutes, 1);
                        if (i == 1)
                        {
                            maxspan = minspan = value;
                            result.SecondSleepTimeRange = range;
                            result.SecondSleepLatencyTimesFromLightOff = SL;
                            result.SecondRemSleepLatencyTimesFromLightOff = RSL;
                            if (RSL > -1)
                            {
                                result.SecondRemSleepLatencyTimesFromLightOff -= SL;
                                result.AverageMultipleSleepRemLatencyTimes += RSL;
                                remCnt++;
                                result.SecondSleepRemCounts = RemCnt;
                            }
                            result.SecondTotalSleepTimes = (float)Math.Round(mults[i].Duration / 60, 1);
                            result.SecondWakeTimes = mults[i].OnStages.Where(t => t == (int)SleepStageEnum.W || t == (int)SleepStageEnum.None).Count() * 0.5f;
                            result.SecondDeepSleepTimes = result.SecondTotalSleepTimes - result.SecondWakeTimes;
                        }
                        else
                        {
                            if (minspan > value)
                                minspan = value;
                            if (maxspan < value)
                            {
                                maxspan = value;
                            }
                            if (i == 2)
                            {
                                result.ThirdSleepTimeRange = range;
                                result.ThirdSleepLatencyTimesFromLightOff = SL;
                                result.ThirdRemSleepLatencyTimesFromLightOff = RSL;
                                if (RSL > -1)
                                {
                                    result.ThirdRemSleepLatencyTimesFromLightOff -= SL;
                                    result.AverageMultipleSleepRemLatencyTimes += RSL;
                                    remCnt++;
                                    result.ThirdSleepRemCounts = RemCnt;
                                }
                                result.ThirdTotalSleepTimes = (float)Math.Round(mults[i].Duration / 60, 1);
                                result.ThirdWakeTimes = mults[i].OnStages.Where(t => t == (int)SleepStageEnum.W || t == (int)SleepStageEnum.None).Count() * 0.5f;
                                result.ThirdDeepSleepTimes = result.ThirdTotalSleepTimes - result.ThirdWakeTimes;
                            }
                            else if (i == 3)
                            {
                                result.FourthSleepTimeRange = range;
                                result.FourthSleepLatencyTimesFromLightOff = SL;
                                result.FourthRemSleepLatencyTimesFromLightOff = RSL;
                                if (RSL > -1)
                                {
                                    result.FourthRemSleepLatencyTimesFromLightOff -= SL;
                                    result.AverageMultipleSleepRemLatencyTimes += RSL;
                                    remCnt++;
                                    result.FourthSleepRemCounts = RemCnt;
                                }
                                result.FourthTotalSleepTimes = (float)Math.Round(mults[i].Duration / 60, 1);
                                result.FourthWakeTimes = mults[i].OnStages.Where(t => t == (int)SleepStageEnum.W || t == (int)SleepStageEnum.None).Count() * 0.5f;
                                result.FourthDeepSleepTimes = result.FourthTotalSleepTimes - result.FourthWakeTimes;
                            }
                            else
                            {
                                result.FifthSleepTimeRange = range;
                                result.FifthSleepLatencyTimesFromLightOff = SL;
                                result.FifthRemSleepLatencyTimesFromLightOff = RSL;
                                if (RSL > -1)
                                {
                                    result.FifthRemSleepLatencyTimesFromLightOff -= SL;
                                    result.AverageMultipleSleepRemLatencyTimes += RSL;
                                    remCnt++;
                                    result.FifthSleepRemCounts = RemCnt;
                                }
                                result.FifthTotalSleepTimes = (float)Math.Round(mults[i].Duration / 60, 1);
                                result.FifthWakeTimes = mults[i].OnStages.Where(t => t == (int)SleepStageEnum.W || t == (int)SleepStageEnum.None).Count() * 0.5f;
                                result.FifthDeepSleepTimes = result.FifthTotalSleepTimes - result.FifthWakeTimes;
                            }
                        }
                    }

                }
                if (remCnt > 0)
                {
                    result.AverageMultipleSleepRemLatencyTimes= (float)Math.Round(result.AverageMultipleSleepRemLatencyTimes / remCnt, 2);
                }
                result.TotalMultipleSleepTimes = result.FirstTotalSleepTimes + result.SecondTotalSleepTimes + result.ThirdTotalSleepTimes + result.FourthTotalSleepTimes + result.FifthTotalSleepTimes;
                result.MultipleSleepTotalRemCounts = result.FifthSleepRemCounts + result.FourthSleepRemCounts + result.ThirdSleepRemCounts + result.SecondSleepRemCounts + result.FirstSleepRemCounts;
                result.SingleSleepDuration = (float)Math.Round(result.SingleSleepDuration / 60, 1);
                result.AverageMultipleSleepLatencyTimes = result.AverageMultipleSleepLatencyTimes / result.TotalMultipleSleepCount;
                result.MultipleSleepSpanRange = minspan == maxspan ? minspan.ToString() : string.Format("{0}-{1}", minspan, maxspan);
            }
            #endregion
            return result;
        }
    }
}
