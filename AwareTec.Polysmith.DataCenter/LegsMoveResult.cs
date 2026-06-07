using AwareTec.Polysmith.DataBaseCom;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AwareTec.Polysmith.DataCenter
{
    /// <summary>
    /// 肢体运动指标基础类
    /// </summary>
    internal class LegsMoveResult
    {
        private IDataBase m_DataSource = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="datasource"></param>
        public LegsMoveResult(IDataBase datasource)
        {
            m_DataSource = datasource;
        }
        /// <summary>
        /// 获取心脏统计指标值
        /// </summary>
        /// <returns></returns>
        public Doc_BodyMovementResult getResult()
        {
            Doc_BodyMovementResult result = new Doc_BodyMovementResult();
            List<DataEvent> legsEvents = new List<DataEvent>();
            List<DataEvent> PLevents = new List<DataEvent>();
            for (int i = 0; i < this.m_DataSource.LegsEvents.Count; i++)
            {
                if (this.m_DataSource.LegsEvents[i].EventTyp == EventTypeEnum.PeriodicalBodyMove)
                {
                    PLevents.Add(m_DataSource.LegsEvents[i]);
                }
                else
                {
                    legsEvents.Add(m_DataSource.LegsEvents[i]);
                }
            }
            legsEvents.Sort((t1, t2) => t1.StartIndex.CompareTo(t2.StartIndex));
            result.PeriodicalLegsCount = PLevents.Count;
            result.LegsMoveTotalCount = legsEvents.Count;
            result.PlmArousalCount = this.m_DataSource.ArousalEvents.Where(x => x.EventTyp == EventTypeEnum.PlmArousal).Count();
            result.LegsMoveIndex= (float)Math.Round((double)(result.LegsMoveTotalCount * 60f / this.m_DataSource.TotalSleepTime), 2);
            result.AwakenPeriodicalBodyMovementCount = (result.AwakenPeriodicalBodyMovementIndex = 0f);
            int lastCount = 0;
            for (int i = 0; i < PLevents.Count; i++)
            {
                DataEvent item = PLevents[i];
                bool findone = false;
                int OnePLegsMoveCount = 0;

                DataEvent dataEvent = this.m_DataSource.ArousalEvents.Find((DataEvent t) => (/*(t.EventTyp == EventTypeEnum.LmArousal || t.EventTyp == EventTypeEnum.PlmArousal) &&*/ (t.StartTime <= item.EndTime.AddSeconds(this.m_DataSource.TimeThresholdValue_leg) && t.EndTime.AddSeconds(this.m_DataSource.TimeThresholdValue_leg) >= item.StartTime)));
                
                for (int s = lastCount; s < legsEvents.Count; s++)
                {
                    if (item.StartIndex <= legsEvents[s].EndIndex && item.EndIndex >= legsEvents[s].StartIndex)
                    {
                        OnePLegsMoveCount++;
                        if (legsEvents[s].OnStages.ToList().Distinct().Count()>1)
                        {
                            var startIndex = (int)((legsEvents[s].StartTime - m_DataSource.StartRecordTime).TotalSeconds / 30) +1;
                            var endIndex = (int)((legsEvents[s].EndTime - m_DataSource.StartRecordTime).TotalSeconds / 30) + 1;
                            Dictionary<string, double> dic = new Dictionary<string, double>();
                            for (int j = 0; j <= endIndex - startIndex; j++)
                            {
                                var time = 0.0;

                                if (j == 0)
                                {
                                    time = (double)(m_DataSource.StartRecordTime.AddSeconds(30 * startIndex) - legsEvents[s].StartTime).TotalSeconds;
                                }
                                else if (j == endIndex - startIndex)
                                {
                                    time = (double)(legsEvents[s].EndTime - m_DataSource.StartRecordTime.AddSeconds(30 * startIndex)).TotalSeconds;
                                }
                                else
                                {
                                    time = 30.0;
                                }

                                if (legsEvents[s].OnStages[j] == 5)
                                {
                                    GetDic(dic, time,"w");
                                }
                                else if (legsEvents[s].OnStages[j] == 4)
                                {
                                    GetDic(dic, time, "r");
                                }
                                else if (legsEvents[s].OnStages[j] == 3 || legsEvents[s].OnStages[j] == 2 || legsEvents[s].OnStages[j] == 1)
                                {
                                    GetDic(dic, time, "n");
                                }
                            }
                            if (dic.ToList().OrderByDescending(x => x.Value).First().Key == "r")
                            {
                                result.LegsREMPeriodicalBodyMovementCount++;
                                if (dataEvent != null)
                                {
                                    result.LegsREMAwakenPeriodicalBodyMovementCount++;
                                }
                            }
                            else if (dic.ToList().OrderByDescending(x => x.Value).First().Key == "w")
                            {
                                result.LegsWeekPeriodicalBodyMovementCount++;
                                if (dataEvent != null)
                                {
                                    result.LegsWeekAwakenPeriodicalBodyMovementCount++;
                                }
                            }
                            else if (dic.ToList().OrderByDescending(x => x.Value).First().Key == "n")
                            {
                                result.LegsNREMPeriodicalBodyMovementCount++;
                                if (dataEvent != null)
                                {
                                    result.LegsNREMAwakenPeriodicalBodyMovementCount++;
                                }
                            }                          
                        }
                        else
                        {
                            if (legsEvents[s].OnStages[0] == (int)SleepStageEnum.R)
                            {
                                result.LegsREMPeriodicalBodyMovementCount++;
                                if (dataEvent != null)
                                {
                                    result.LegsREMAwakenPeriodicalBodyMovementCount ++;
                                }
                            }
                            else if(legsEvents[s].OnStages[0] == (int)SleepStageEnum.W)
                            {
                                result.LegsWeekPeriodicalBodyMovementCount ++;
                                if (dataEvent != null)
                                {
                                    result.LegsWeekAwakenPeriodicalBodyMovementCount ++;
                                }
                            }
                            else if (legsEvents[s].OnStages[0] != (int)SleepStageEnum.None)
                            {
                                result.LegsNREMPeriodicalBodyMovementCount++;
                                if (dataEvent != null)
                                {
                                    result.LegsNREMAwakenPeriodicalBodyMovementCount++;
                                }
                            }
                        }
                        findone = true;
                    }
                    else if (findone)
                    {
                        lastCount = s;
                        break;
                    }
                }
                result.PeriodicalBodyMovementCount += OnePLegsMoveCount;                
                if (dataEvent != null)
                {
                    result.AwakenPeriodicalBodyMovementCount += 1f;
                    result.LegsAwakenPeriodicalBodyMovementCount += OnePLegsMoveCount;
                }
                bool R1 = false, R2 = false, R3 = false;
                if (item.OnStages.ToList().Distinct().Count() > 1)
                {
                    var startIndex = (int)((item.StartTime - m_DataSource.StartRecordTime).TotalSeconds / 30) + 1;
                    var endIndex = (int)((item.EndTime - m_DataSource.StartRecordTime).TotalSeconds / 30) + 1;
                    Dictionary<string, double> dic = new Dictionary<string, double>();
                    for (int j = 0; j <= endIndex - startIndex; j++)
                    {
                        var time = 0.0;

                        if (j == 0)
                        {
                            time = (double)(m_DataSource.StartRecordTime.AddSeconds(30 * startIndex) - item.StartTime).TotalSeconds;
                        }
                        else if (j == endIndex - startIndex)
                        {
                            time = (double)(item.EndTime - m_DataSource.StartRecordTime.AddSeconds(30 * startIndex)).TotalSeconds;
                        }
                        else
                        {
                            time = 30.0;
                        }

                        if (item.OnStages[j] == 5)
                        {
                            GetDic(dic, time, "w");
                        }
                        else if (item.OnStages[j] == 4)
                        {
                            GetDic(dic, time, "r");
                        }
                        else if (item.OnStages[j] == 3 || item.OnStages[j] == 2 || item.OnStages[j] == 1)
                        {
                            GetDic(dic, time, "n");
                        }
                    }
                    if (dic.ToList().OrderBy(x => x.Value).First().Key == "r")
                    {
                        item.OnStages[0] = (int)SleepStageEnum.R;
                    }
                    else if (dic.ToList().OrderBy(x => x.Value).First().Key == "w")
                    {
                        item.OnStages[0] = (int)SleepStageEnum.W;
                    }
                    else if (dic.ToList().OrderBy(x => x.Value).First().Key == "n")
                    {
                        item.OnStages[0] = (int)SleepStageEnum.N1;
                    }
                }

                if (item.OnStages[0] == (int)SleepStageEnum.R)
                {
                    if (!R1)
                    {
                        result.REMPeriodicalBodyMovementCount++;
                        //result.LegsREMPeriodicalBodyMovementCount += OnePLegsMoveCount;
                        R1 = true;
                        if (dataEvent != null)
                        {
                            result.REMAwakenPeriodicalBodyMovementCount++;
                            //result.LegsREMAwakenPeriodicalBodyMovementCount += OnePLegsMoveCount;
                        }
                    }
                }
                else if (item.OnStages[0] == (int)SleepStageEnum.W)
                {
                    if (!R2)
                    {
                        result.WeekPeriodicalBodyMovementCount++;
                        //result.LegsWeekPeriodicalBodyMovementCount += OnePLegsMoveCount;
                        R2 = true;
                        if (dataEvent != null)
                        {
                            result.WeekAwakenPeriodicalBodyMovementCount++;
                            //result.LegsWeekAwakenPeriodicalBodyMovementCount += OnePLegsMoveCount;
                        }
                    }
                }
                else if (item.OnStages[0] != (int)SleepStageEnum.None)
                {
                    if (!R3)
                    {
                        result.NREMPeriodicalBodyMovementCount++;
                        //result.LegsNREMPeriodicalBodyMovementCount += OnePLegsMoveCount;
                        R3 = true;
                        if (dataEvent != null)
                        {
                            result.NREMAwakenPeriodicalBodyMovementCount++;
                            //result.LegsNREMAwakenPeriodicalBodyMovementCount += OnePLegsMoveCount;
                        }
                    }
                }
                if (R1 || R3)
                {
                    result.SleepPeriodicalBodyMovementCount++;
                    //result.LegsSleepPeriodicalBodyMovementCount += OnePLegsMoveCount;
                    if (dataEvent != null)
                    {
                        result.SleepAwakenPeriodicalBodyMovementCount++;
                        //result.LegsSleepAwakenPeriodicalBodyMovementCount += OnePLegsMoveCount;
                    }
                }
            }
            for (int i = 0; i < legsEvents.Count; i++)
            {
                DataEvent item = legsEvents[i];
                bool R1 = false, R2 = false, R3 = false;
                if (item.OnStages.ToList().Distinct().Count() > 1)
                {
                    var startIndex = (int)((item.StartTime - m_DataSource.StartRecordTime).TotalSeconds / 30) + 1;
                    var endIndex = (int)((item.EndTime - m_DataSource.StartRecordTime).TotalSeconds / 30) + 1;
                    Dictionary<string, double> dic = new Dictionary<string, double>();
                    for (int j = 0; j <= endIndex - startIndex; j++)
                    {
                        var time = 0.0;

                        if (j == 0)
                        {
                            time = (double)(m_DataSource.StartRecordTime.AddSeconds(30 * startIndex) - item.StartTime).TotalSeconds;
                        }
                        else if (j == endIndex - startIndex)
                        {
                            time = (double)(item.EndTime - m_DataSource.StartRecordTime.AddSeconds(30 * startIndex)).TotalSeconds;
                        }
                        else
                        {
                            time = 30.0;
                        }

                        if (item.OnStages[j] == 5)
                        {
                            GetDic(dic, time, "w");
                        }
                        else if (item.OnStages[j] == 4)
                        {
                            GetDic(dic, time, "r");
                        }
                        else if (item.OnStages[j] == 3 || item.OnStages[j] == 2 || item.OnStages[j] == 1)
                        {
                            GetDic(dic, time, "n");
                        }
                    }
                    if (dic.ToList().OrderBy(x => x.Value).First().Key == "r")
                    {
                        item.OnStages[0] = (int)SleepStageEnum.R;
                    }
                    else if (dic.ToList().OrderBy(x => x.Value).First().Key == "w")
                    {
                        item.OnStages[0] = (int)SleepStageEnum.W;
                    }
                    else if (dic.ToList().OrderBy(x => x.Value).First().Key == "n")
                    {
                        item.OnStages[0] = (int)SleepStageEnum.N1;
                    }
                }
                if (item.OnStages[0] == (int)SleepStageEnum.R)
                {
                    if (!R1)
                    {
                        result.REMLegsMoveCount++;
                        R1 = true;
                    }
                }
                else if (item.OnStages[0] == (int)SleepStageEnum.W)
                {
                    if (!R2)
                    {
                        result.WeekLegsMoveCount++;
                        R2 = true;
                    }
                }
                else if (item.OnStages[0] != (int)SleepStageEnum.None)
                {
                    if (!R3)
                    {
                        result.NREMLegsMoveCount++;
                        R3 = true;
                    }
                }
                if (R1 || R3)
                {
                    result.SleepLegsMoveCount++;
                }
            }
            result.LegsSleepPeriodicalBodyMovementCount = result.LegsREMPeriodicalBodyMovementCount + result.LegsNREMPeriodicalBodyMovementCount;
            result.LegsSleepAwakenPeriodicalBodyMovementCount = result.LegsREMAwakenPeriodicalBodyMovementCount + result.LegsNREMAwakenPeriodicalBodyMovementCount;
            if (m_DataSource.RemTotalTimes > 0)
            {
                result.REMAwakenPeriodicalBodyMovementIndex = (float)Math.Round((double)(result.REMAwakenPeriodicalBodyMovementCount * 60f / this.m_DataSource.RemTotalTimes), 2);
                result.REMPeriodicalBodyMovementIndex = (float)Math.Round((double)(result.REMPeriodicalBodyMovementCount * 60f / this.m_DataSource.RemTotalTimes), 2);
                result.LegsREMAwakenPeriodicalBodyMovementIndex = (float)Math.Round((double)(result.LegsREMAwakenPeriodicalBodyMovementCount * 60f / this.m_DataSource.RemTotalTimes), 2);
                result.LegsREMPeriodicalBodyMovementIndex = (float)Math.Round((double)(result.LegsREMPeriodicalBodyMovementCount * 60f / this.m_DataSource.RemTotalTimes), 2);
            }
            if (m_DataSource.NRemTotalTimes > 0)
            {
                result.NREMAwakenPeriodicalBodyMovementIndex = (float)Math.Round((double)(result.NREMAwakenPeriodicalBodyMovementCount * 60f / this.m_DataSource.NRemTotalTimes), 2);
                result.NREMPeriodicalBodyMovementIndex = (float)Math.Round((double)(result.NREMPeriodicalBodyMovementCount * 60f / this.m_DataSource.NRemTotalTimes), 2);
                result.LegsNREMAwakenPeriodicalBodyMovementIndex = (float)Math.Round((double)(result.LegsNREMAwakenPeriodicalBodyMovementCount * 60f / this.m_DataSource.NRemTotalTimes), 2);
                result.LegsNREMPeriodicalBodyMovementIndex = (float)Math.Round((double)(result.LegsNREMPeriodicalBodyMovementCount * 60f / this.m_DataSource.NRemTotalTimes), 2);
            }
            if (this.m_DataSource.TotalSleepTime > 0f)
            {
                result.SleepAwakenPeriodicalBodyMovementIndex = (float)Math.Round((double)(result.SleepAwakenPeriodicalBodyMovementCount * 60f / this.m_DataSource.TotalSleepTime), 2);
                result.SleepPeriodicalBodyMovementIndex = (float)Math.Round((double)(result.SleepPeriodicalBodyMovementCount * 60f / this.m_DataSource.TotalSleepTime), 2);
                result.LegsSleepAwakenPeriodicalBodyMovementIndex = (float)Math.Round((double)(result.LegsSleepAwakenPeriodicalBodyMovementCount * 60f / this.m_DataSource.TotalSleepTime), 2);
                result.LegsSleepPeriodicalBodyMovementIndex = (float)Math.Round((double)(result.LegsSleepPeriodicalBodyMovementCount * 60f / this.m_DataSource.TotalSleepTime), 2);
            }
            float weekTimes = m_DataSource.SPTime - m_DataSource.TotalSleepTime;
            if (weekTimes > 0)
            {
                result.WeekAwakenPeriodicalBodyMovementIndex = (float)Math.Round((double)(result.WeekAwakenPeriodicalBodyMovementCount * 60f / weekTimes), 2);
                result.WeekPeriodicalBodyMovementIndex = (float)Math.Round((double)(result.WeekPeriodicalBodyMovementCount * 60f / weekTimes), 2);
                result.LegsWeekAwakenPeriodicalBodyMovementIndex = (float)Math.Round((double)(result.LegsWeekAwakenPeriodicalBodyMovementCount * 60f / weekTimes), 2);
                result.LegsWeekPeriodicalBodyMovementIndex = (float)Math.Round((double)(result.LegsWeekPeriodicalBodyMovementCount * 60f / weekTimes), 2);
            }
            if (m_DataSource.SPTime > 0)
            {
                if (result.PeriodicalBodyMovementCount < result.PeriodicalLegsCount * 4)
                    result.PeriodicalBodyMovementCount = result.PeriodicalLegsCount * 4;
                result.PeriodicalBodyMovementIndex = (float)Math.Round((double)(result.PeriodicalBodyMovementCount * 60f / this.m_DataSource.TotalSleepTime), 2);
                result.PLMPeriodicalBodyMovementIndex = (float)Math.Round((double)(result.PeriodicalLegsCount * 60f / this.m_DataSource.TotalSleepTime), 2);
                result.AwakenPeriodicalBodyMovementIndex = (float)Math.Round((double)(result.AwakenPeriodicalBodyMovementCount * 60f / this.m_DataSource.TotalSleepTime), 2);
                result.LegsAwakenPeriodicalBodyMovementIndex = (float)Math.Round((double)(result.LegsAwakenPeriodicalBodyMovementCount * 60f / this.m_DataSource.TotalSleepTime), 2);
            }
            return result;
        }

        private Dictionary<string, double> GetDic(Dictionary<string, double> dic, double time,string str)
        {
            double val;
            if (dic.TryGetValue(str, out val))
            {
                //如果指定的字典的键存在
                dic[str] = val + time;
            }
            else
            {
                //不存在，则添加
                dic.Add(str, time);
            }
            return dic;
        }
    }
}
