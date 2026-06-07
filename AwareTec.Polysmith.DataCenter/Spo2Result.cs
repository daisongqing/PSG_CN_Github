using AwareTec.Polysmith.DataBaseCom;
using System;
using System.Collections.Generic;
using System.Linq;
namespace AwareTec.Polysmith.DataCenter
{
    /// <summary>
    /// 血氧指标计算类
    /// </summary>
    internal class Spo2Result
    {

        private IDataBase m_dataSource = null;
        /// <summary>
        /// 采样率
        /// </summary>
        private float m_TimeSpan = 1;
        private bool m_TimeSpan_ini = false;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="values"></param>
        /// <param name="BaseDataSource"></param>
        public Spo2Result(IDataBase BaseDataSource)
        {
            m_dataSource = BaseDataSource;
        }
        /// <summary>
        /// 获取指标结果
        /// </summary>
        /// <returns></returns>
        public Doc_BloodOxygenResult getResult(Doc_SystemSetting setting)
        {
            Doc_BloodOxygenResult result = new Doc_BloodOxygenResult();
            try
            {
                result.ModeType = setting.ModeType;
                int spo2baselinecount = 0;
                float sumspo2basetime = 0f;
                bool isfindspo2basetime = false;
                if (!m_dataSource.IsBreathOnly)
                {
                    foreach (DataEpoch one in m_dataSource.TIBEpochs)
                    {
                        if (one.EnumStage == SleepStageEnum.W)
                        {
                            spo2baselinecount++;
                            sumspo2basetime += one.AveSpO2;
                        }
                    }
                    result.Spo2BaseLine = spo2baselinecount > 0 ? (float)Math.Round(sumspo2basetime / spo2baselinecount, 2) : 0;
                    isfindspo2basetime = spo2baselinecount > 0;
                }
                //血氧基线 初筛 第一个血氧事件之前 血氧的平均值      多导 是TIB期间W期血氧平均值
                if (!isfindspo2basetime)
                {
                    if (m_dataSource.SpO2Events.Count > 0)
                    {
                        DataEvent firstspo2event = m_dataSource.SpO2Events[0];
                        foreach (DataEpoch one in m_dataSource.TIBEpochs)
                        {
                            if (one.Index < firstspo2event.StartIndex / 30)
                            {
                                spo2baselinecount++;
                                sumspo2basetime += one.AveSpO2;
                            }
                            else
                                break;
                        }
                        result.Spo2BaseLine = spo2baselinecount > 0 ? (float)Math.Round(sumspo2basetime / spo2baselinecount, 2) : m_dataSource.TIBEpochs[0].AveSpO2;
                    }
                    else
                        result.Spo2BaseLine = 0;
                }
                IEnumerable<float> source1 = Enumerable.Where<float>(Enumerable.Select<DataEpoch, float>((IEnumerable<DataEpoch>)this.m_dataSource.TIBEpochs, (Func<DataEpoch, float>)(t => t.MinSpO2)), (Func<float, bool>)(t => (double)t > 0.0));
                result.AveBloodOxygenTIB = result.MinBloodOxygenTST = result.MinBloodOxygenTIB = result.MinLimitBloodOxygenDuration = 0.0f;
                result.strMinBloodOxygenREM = result.strMinBloodOxygenNREM = "--";
                result.AveBloodOxygenWake = 100;
                if (Enumerable.Count<float>(source1) > 0)
                {
                    result.MinBloodOxygenTIB = Enumerable.Min(source1);
                    result.MaxBloodOxygenTIB = this.m_dataSource.TIBEpochs.Select(t => t.MaxSpO2).Max();
                    IEnumerable<float> source2 = Enumerable.Where<float>(Enumerable.Select<DataEpoch, float>((IEnumerable<DataEpoch>)this.m_dataSource.TSTEpochs, (Func<DataEpoch, float>)(t => t.MinSpO2)), (Func<float, bool>)(t => (double)t > 0.0));
                    if (Enumerable.Count<float>(source2) > 0)
                    {
                        result.MinBloodOxygenTST = Enumerable.Min(source2);
                        result.MinLimitBloodOxygenDuration = (float)Enumerable.Count<float>(Enumerable.Where<float>(source2, (Func<float, bool>)(t => (double)t == (double)result.MinBloodOxygenTST)));
                    }
                    else
                    {
                        result.MinBloodOxygenTST = result.MinBloodOxygenTIB;
                        result.MinLimitBloodOxygenDuration = (float)Enumerable.Count<float>(Enumerable.Where<float>(source1, (Func<float, bool>)(t => (double)t == (double)result.MinBloodOxygenTST)));
                    }
                    IEnumerable<float> spo2MaxTST = this.m_dataSource.TSTEpochs.Select(t => t.MaxSpO2);
                    if (spo2MaxTST.Count() > 0)
                        result.MaxBloodOxygenTST = spo2MaxTST.Max();
                    IEnumerable<DataEpoch> WakeEpochs = this.m_dataSource.TIBEpochs.Where(t => t.EnumStage == SleepStageEnum.W);
                    if (WakeEpochs.Count() > 0)
                    {
                        //排除0的干扰
                        result.AveBloodOxygenWake = (float)Math.Round(WakeEpochs.Select(t => t.AveSpO2).ToList().FindAll(k => k > 0).Average(), 2);
                        result.MinBloodOxygenWake = (float)Math.Round(WakeEpochs.Select(t => t.MinSpO2).ToList().FindAll(k => k > 0).Min(), 2);
                    }
                    result.AveBloodOxygenTIB = (float)Math.Round((double)Enumerable.Average(Enumerable.Where<float>(Enumerable.Select<DataEpoch, float>((IEnumerable<DataEpoch>)this.m_dataSource.TIBEpochs, (Func<DataEpoch, float>)(t => t.AveSpO2)), (Func<float, bool>)(t => (double)t > 0.0))), 2);
                    if (this.m_dataSource.TSTEpochs.Count > 0)
                        result.AveBloodOxygenTST = (float)Math.Round((double)Enumerable.Average(Enumerable.Where<float>(Enumerable.Select<DataEpoch, float>((IEnumerable<DataEpoch>)this.m_dataSource.TSTEpochs, (Func<DataEpoch, float>)(t => t.AveSpO2)), (Func<float, bool>)(t => (double)t > 0.0))), 2);
                    else
                        result.AveBloodOxygenTST = result.AveBloodOxygenTIB;
                }
                IEnumerable<DataEpoch> source3 = Enumerable.Where<DataEpoch>((IEnumerable<DataEpoch>)this.m_dataSource.TSTEpochs, (Func<DataEpoch, bool>)(t => t.EnumStage == SleepStageEnum.R));
                result.AveBloodOxygenREM = -1f;
                if (Enumerable.Count<DataEpoch>(source3) > 0)
                {
                    IEnumerable<float> source2 = Enumerable.Where<float>(Enumerable.Select<DataEpoch, float>(source3, (Func<DataEpoch, float>)(t => t.AveSpO2)), (Func<float, bool>)(t => (double)t > 0.0));
                    if (Enumerable.Count<float>(source2) > 0)
                    {
                        result.AveBloodOxygenREM = (float)Math.Round((double)Enumerable.Average(source2), 2);
                        result.strMinBloodOxygenREM = string.Format("{0}", Math.Round((double)Enumerable.Min(source2), 2));
                    }
                }
                IEnumerable<DataEpoch> source4 = Enumerable.Where<DataEpoch>((IEnumerable<DataEpoch>)this.m_dataSource.TSTEpochs, (Func<DataEpoch, bool>)(t =>
                {
                    if (t.EnumStage != SleepStageEnum.N1 && t.EnumStage != SleepStageEnum.N2)
                        return t.EnumStage == SleepStageEnum.N3;
                    return true;
                }));
                try
                {
                    result.AveBloodOxygenNREM = -1f;
                    var spo2List = Enumerable.Where<float>(Enumerable.Select<DataEpoch, float>(source4, (Func<DataEpoch, float>)(t => t.AveSpO2)), (Func<float, bool>)(t => (double)t > 0.0));
                    if (spo2List.Count() > 0)
                    {
                        result.AveBloodOxygenNREM = (float)Math.Round((double)Enumerable.Average(spo2List), 2);
                        result.strMinBloodOxygenNREM = string.Format("{0}", Math.Round((double)Enumerable.Min(spo2List), 2));
                    }
                }
                catch
                {
                    result.AveBloodOxygenNREM = -1f;
                }
                float minSpO2ofBreath = 100f;
                float minSpO2Value = 100f;
                int num3 = 0;
                result.MinBloodOxygenTSTBreathEventTyp = 0;
                result.TotalBloodOxygenLowThan98 = result.TotalBloodOxygenLowThan90 = result.TotalBloodOxygenLowThan85 = result.TotalBloodOxygenLowThan80 = result.TotalBloodOxygenLowThan60 = result.TotalOxygenReduceIndex = result.TotalOxygenReduceMaxDuration = 0.0f;
                result.WakeBloodOxygenLowThan60 = result.WakeBloodOxygenLowThan80 = result.WakeBloodOxygenLowThan85 = result.WakeBloodOxygenLowThan90 = result.WakeBloodOxygenLowThan98 = result.WakeOxygenReduceIndex = result.WakeOxygenReduceMaxDuration = 0.0f;
                result.NREMBloodOxygenLowThan60 = result.NREMBloodOxygenLowThan80 = result.NREMBloodOxygenLowThan85 = result.NREMBloodOxygenLowThan90 = result.NREMBloodOxygenLowThan98 = result.NREMOxygenReduceIndex = result.NREMOxygenReduceMaxDuration = 0.0f;
                result.REMBloodOxygenLowThan60 = result.REMBloodOxygenLowThan80 = result.REMBloodOxygenLowThan85 = result.REMBloodOxygenLowThan90 = result.REMBloodOxygenLowThan98 = result.REMOxygenReduceIndex = result.REMOxygenReduceMaxDuration = 0.0f;
                int readyCnt = 0;
                foreach (DataEpoch one in m_dataSource.TIBEpochs)
                {
                    if (one.Spo2LevelTimes.Length > 0)
                    {
                        readyCnt++;
                        result.TotalBloodOxygenLowThan98Times += one.Spo2LevelTimes[0];
                        result.TotalBloodOxygenLowThan90Times += one.Spo2LevelTimes[1];
                        result.TotalBloodOxygenLowThan85Times += one.Spo2LevelTimes[2];
                        result.TotalBloodOxygenLowThan80Times += one.Spo2LevelTimes[3];
                        result.TotalBloodOxygenLowThan70Times += one.Spo2LevelTimes[4];
                        result.TotalBloodOxygenLowThan60Times += one.Spo2LevelTimes[5];
                        result.TotalBloodOxygenLowThan50Times += one.Spo2LevelTimes[6];
                        result.TotalBloodOxygenLowThan40Times += one.Spo2LevelTimes[7];
                    }
                }
                bool ready = readyCnt > 0;
                result.OxygenReduceCount = 0;
                result.Spo2ArtifactCount = 0;
                float maxlongspo2 = -1f;
                float maxspo2reducevalue = 0f;
                float sumspo2time = 0f;
                float sumspo2reduce = 0f;
                double sumspo2delaytime = 0f;
                result.Spo2Reduce3Index = 0;
                result.Spo2Reduce4Index = 0;
                int spo2reduce3count = 0;
                int spo2reduce4count = 0;
                int spo2dalaycount = 0;
                result.LongBloodOxygenDuringTime = 0;
                foreach (DataEvent dataEvent1 in this.m_dataSource.SpO2Events)
                {
                    DataEvent item = dataEvent1;
                    if (item.EventTyp == EventTypeEnum.Spo2Artifact)
                    {
                        result.Spo2ArtifactCount++;
                        result.Spo2ArtifactTotalTime += item.Duration;
                        continue;
                    }
                    result.OxygenReduceCount++;
                    if (Convert.ToDouble(item.Comments) >= 3)
                    {
                        spo2reduce3count++;
                        if (Convert.ToDouble(item.Comments) >= 4)
                            spo2reduce4count++;
                    }
                    bool flag1 = item.StartTime >= this.m_dataSource.TSTStartTime && item.StartTime < this.m_dataSource.TSTEndTime || item.EndTime > this.m_dataSource.TSTStartTime && item.EndTime <= this.m_dataSource.TSTEndTime;
                    if (flag1)
                    {
                        if (Enumerable.Count<int>(Enumerable.Where<int>((IEnumerable<int>)item.OnStages, (Func<int, bool>)(t =>
                        {
                            if (t != 4 && t != 3 && t != 2)
                                return t == 1;
                            return true;
                        }))) > 0)
                            ++num3;
                        else
                            flag1 = false;
                    }
                    //最低血氧发生的时间
                    if (Convert.ToSingle(item.Tag) == result.MinBloodOxygenTST)
                    {
                        result.MinBloodOxygenTSTStartTime = item.StartTime;
                    }
                    //最大氧减幅度发生的时间，数值，持续时间
                    float spo2reducevalue = Convert.ToSingle(item.Comments);
                    if (spo2reducevalue >= maxspo2reducevalue)
                    {
                        maxspo2reducevalue = spo2reducevalue;
                        result.MaxBloodOxygenReduceTSTStartTime = item.StartTime;
                        result.MaxBloodOxygenReduceTST = maxspo2reducevalue;
                        result.MaxBloodOxygenReduceDuringTime = item.Duration;
                    }
                    //最长氧减发生的时间、数值、持续时间
                    if (item.Duration > maxlongspo2)
                    {
                        maxlongspo2 = item.Duration;
                        result.LongBloodOxygenTSTStartTime = item.StartTime;
                        result.LongBloodOxygenTST = Convert.ToSingle(item.Tag);
                        result.LongBloodOxygenDuringTime = maxlongspo2;
                    }
                    //算总氧减时间,总氧减幅度
                    sumspo2time += item.Duration;
                    sumspo2reduce += Convert.ToSingle(item.Comments);
                    DateTime dt = item.StartTime.AddSeconds(0.0 - (double)this.m_dataSource.TimeThresholdValue_spo2);
                    float _minSpo2Value = Convert.ToSingle(item.Tag);
                    if (flag1 && (double)minSpO2Value > (double)_minSpo2Value)
                    {
                        minSpO2Value = _minSpo2Value;
                        result.MinLimitBloodOxygenDuration = item.Duration;
                        result.MinBloodOxygenTST = _minSpo2Value;
                        try
                        {
                            //判断是否是与呼吸事件相关的氧减事件，如果是则取出最低的血氧值
                            //（计算规则：当前氧减事件往前m_TimeThresholdValue内是否有发生呼吸事件）
                            DataEvent exist = m_dataSource.BreathEvents.First(t => (t.StartTime <= dt || (t.StartTime > dt && t.StartTime < item.StartTime)) && t.EndTime >= dt);
                            if (exist != null)
                            {
                                if ((double)minSpO2ofBreath > (double)_minSpo2Value)
                                {
                                    minSpO2ofBreath = _minSpo2Value;
                                    result.MinBloodOxygenTSTBreathEventTyp = exist.EventTyp == EventTypeEnum.Resp_CA ? 2 : (exist.EventTyp == EventTypeEnum.Resp_OA ? 1 : (exist.EventTyp == EventTypeEnum.Resp_MA ? 3 : 4));
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                    if (flag1)
                    {
                        try
                        {
                            //判断是否是与呼吸事件相关的氧减事件，
                            //（计算规则：当前氧减事件往前m_TimeThresholdValue内是否有发生呼吸事件）
                            DataEvent exist = m_dataSource.BreathEvents.First(t => (t.StartTime <= dt || (t.StartTime > dt && t.StartTime < item.StartTime)) && t.EndTime >= dt);
                            if (exist != null)
                            {
                                sumspo2delaytime += (item.StartTime - exist.StartTime).TotalSeconds;
                                spo2dalaycount++;
                            }
                        }
                        catch
                        {
                        }
                    }
                    int[] onStages = item.OnStages;
                    bool flag2 = false, flag3 = false, flag4 = false, flag5 = false, flag6 = false, flag7 = false, flag8 = false, flag9 = false, flag10 = false;
                    if ((double)_minSpo2Value < 98.0 && (double)_minSpo2Value >= 90.0)
                    {
                        flag5 = true;
                        ++result.TotalBloodOxygenLowThan98;
                        if (!ready)
                            result.TotalBloodOxygenLowThan98Times += item.Duration;
                    }
                    else if ((double)_minSpo2Value < 90.0 && (double)_minSpo2Value >= 85.0)
                    {
                        flag2 = true;
                        ++result.TotalBloodOxygenLowThan90;
                        if (!ready)
                            result.TotalBloodOxygenLowThan90Times += item.Duration;
                    }
                    else if ((double)_minSpo2Value < 85.0 && (double)_minSpo2Value >= 80.0)
                    {
                        flag3 = true;
                        ++result.TotalBloodOxygenLowThan85;
                        if (!ready)
                            result.TotalBloodOxygenLowThan85Times += item.Duration;
                    }
                    else if ((double)_minSpo2Value < 80.0 && (double)_minSpo2Value >= 70.0)
                    {
                        flag4 = true;
                        ++result.TotalBloodOxygenLowThan80;
                        if (!ready)
                            result.TotalBloodOxygenLowThan80Times += item.Duration;
                    }
                    else if ((double)_minSpo2Value < 70.0 && (double)_minSpo2Value >= 60.0)
                    {
                        flag7 = true;
                        ++result.TotalBloodOxygenLowThan70;
                        if (!ready)
                            result.TotalBloodOxygenLowThan70Times += item.Duration;
                    }
                    else if ((double)_minSpo2Value < 60.0 && (double)_minSpo2Value >= 50.0)
                    {
                        flag6 = true;
                        ++result.TotalBloodOxygenLowThan60;
                        if (!ready)
                            result.TotalBloodOxygenLowThan60Times += item.Duration;
                    }
                    else if ((double)_minSpo2Value < 50.0 && (double)_minSpo2Value >= 40.0)
                    {
                        flag8 = true;
                        ++result.TotalBloodOxygenLowThan50;
                        if (!ready)
                            result.TotalBloodOxygenLowThan50Times += item.Duration;
                    }
                    else
                    {
                        flag9 = true;
                        ++result.TotalBloodOxygenLowThan40;
                        if (!ready)
                            result.TotalBloodOxygenLowThan40Times += item.Duration;
                    }
                    if (Enumerable.Contains<int>((IEnumerable<int>)onStages, 5))
                    {
                        if (flag5)
                            ++result.WakeBloodOxygenLowThan98;
                        else if (flag2)
                            ++result.WakeBloodOxygenLowThan90;
                        else if (flag3)
                            ++result.WakeBloodOxygenLowThan85;
                        else if (flag4)
                            ++result.WakeBloodOxygenLowThan80;
                        else if (flag7)
                            ++result.WakeBloodOxygenLowThan70;
                        else if (flag6)
                            ++result.WakeBloodOxygenLowThan60;
                        else if (flag8)
                            ++result.WakeBloodOxygenLowThan50;
                        else if (flag9)
                            ++result.WakeBloodOxygenLowThan40;
                        if ((double)result.WakeOxygenReduceMaxDuration < (double)item.Duration)
                            result.WakeOxygenReduceMaxDuration = item.Duration;
                    }
                    if (Enumerable.Contains<int>((IEnumerable<int>)onStages, 4))
                    {
                        if (flag2)
                            ++result.REMBloodOxygenLowThan90;
                        else if (flag3)
                            ++result.REMBloodOxygenLowThan85;
                        else if (flag4)
                            ++result.REMBloodOxygenLowThan80;
                        else if (flag5)
                            ++result.REMBloodOxygenLowThan98;
                        else if (flag6)
                            ++result.REMBloodOxygenLowThan60;
                        else if (flag7)
                            ++result.REMBloodOxygenLowThan70;
                        else if (flag8)
                            ++result.REMBloodOxygenLowThan50;
                        else if (flag9)
                            ++result.REMBloodOxygenLowThan40;
                        if ((double)result.REMOxygenReduceMaxDuration < (double)item.Duration)
                            result.REMOxygenReduceMaxDuration = item.Duration;
                    }
                    if (Enumerable.Contains<int>((IEnumerable<int>)onStages, 3) || Enumerable.Contains<int>((IEnumerable<int>)onStages, 2) || Enumerable.Contains<int>((IEnumerable<int>)onStages, 1))
                    {
                        if (flag2)
                            ++result.NREMBloodOxygenLowThan90;
                        else if (flag3)
                            ++result.NREMBloodOxygenLowThan85;
                        else if (flag4)
                            ++result.NREMBloodOxygenLowThan80;
                        else if (flag5)
                            ++result.NREMBloodOxygenLowThan98;
                        else if (flag6)
                            ++result.NREMBloodOxygenLowThan60;
                        else if (flag7)
                            ++result.NREMBloodOxygenLowThan70;
                        else if (flag8)
                            ++result.NREMBloodOxygenLowThan50;
                        else if (flag9)
                            ++result.NREMBloodOxygenLowThan40;
                        if ((double)result.NREMOxygenReduceMaxDuration < (double)item.Duration)
                            result.NREMOxygenReduceMaxDuration = item.Duration;
                    }
                }
                if (spo2dalaycount > 0)
                {
                    result.AverageSpo2DelayTime = (float)Math.Round(sumspo2delaytime / spo2dalaycount / 60, 2);
                }
                else
                {
                    result.AverageSpo2DelayTime = 0;
                }
                if (result.Spo2ArtifactCount > 0)
                {
                    result.Spo2ArtifactTotalTimeOfTST = (float)Math.Round(result.Spo2ArtifactTotalTime / this.m_dataSource.TotalSleepTime, 4) * 100;
                    result.Spo2ArtifactTotalTime = (float)Math.Round(result.Spo2ArtifactTotalTime / 60, 2);
                }
                else
                {
                    result.Spo2ArtifactTotalTimeOfTST = 0;
                    result.Spo2ArtifactTotalTime = 0;
                }
                if (this.m_dataSource.SpO2Events.Count > 0)
                {
                    result.BloodOxygenTotalTimeOfTST = this.m_dataSource.TotalSleepTime > 0 ? (float)Math.Round(sumspo2time / (double)this.m_dataSource.TotalSleepTime / 60, 4) * 100 : 0;
                    result.TotalBloodOxygenTSTTime = (float)Math.Round(sumspo2time / 60, 2);
                    result.AverageBloodOxygenTSTTime = (float)Math.Round(sumspo2time / this.m_dataSource.SpO2Events.Count, 2);
                    result.AverageBloodOxygenTSTReduceValue= (float)Math.Round(sumspo2reduce / this.m_dataSource.SpO2Events.Count, 2);
                    result.AverageBloodOxygenTSTTimeMin = (float)Math.Round(result.AverageBloodOxygenTSTTime / 60, 2);
                }
                else
                {
                    result.BloodOxygenTotalTimeOfTST = 0;
                    result.TotalBloodOxygenTSTTime = 0;
                    result.AverageBloodOxygenTSTTime = 0;
                    result.AverageBloodOxygenTSTReduceValue = 0;
                    result.AverageBloodOxygenTSTTimeMin = 0;
                }
                result.TotalBloodOxygenLowThan98TimesMinutes = (float)Math.Round(result.TotalBloodOxygenLowThan98Times > 0 ? result.TotalBloodOxygenLowThan98Times / 60 : 0, 2);
                result.TotalBloodOxygenLowThan90TimesMinutes = (float)Math.Round(result.TotalBloodOxygenLowThan90Times > 0 ? result.TotalBloodOxygenLowThan90Times / 60 : 0, 2);
                result.TotalBloodOxygenLowThan85TimesMinutes = (float)Math.Round(result.TotalBloodOxygenLowThan85Times > 0 ? result.TotalBloodOxygenLowThan85Times / 60 : 0, 2);
                result.TotalBloodOxygenLowThan80TimesMinutes = (float)Math.Round(result.TotalBloodOxygenLowThan80Times > 0 ? result.TotalBloodOxygenLowThan80Times / 60 : 0, 2);
                result.TotalBloodOxygenLowThan70TimesMinutes = (float)Math.Round(result.TotalBloodOxygenLowThan70Times > 0 ? result.TotalBloodOxygenLowThan70Times / 60 : 0, 2);
                result.TotalBloodOxygenLowThan60TimesMinutes = (float)Math.Round(result.TotalBloodOxygenLowThan60Times > 0 ? result.TotalBloodOxygenLowThan60Times / 60 : 0, 2);
                result.TotalBloodOxygenLowThan50TimesMinutes = (float)Math.Round(result.TotalBloodOxygenLowThan50Times > 0 ? result.TotalBloodOxygenLowThan50Times / 60 : 0, 2);
                result.TotalBloodOxygenLowThan40TimesMinutes = (float)Math.Round(result.TotalBloodOxygenLowThan40Times > 0 ? result.TotalBloodOxygenLowThan40Times / 60 : 0, 2);

                result.Spo2Reduce3Index = (float)Math.Round(spo2reduce3count * 60 / (double)this.m_dataSource.TotalSleepTime, 2);
                result.Spo2Reduce4Index = (float)Math.Round(spo2reduce4count * 60 / (double)this.m_dataSource.TotalSleepTime, 2);
                result.OxygenReduceIndex = 0.0f;
                if ((double)this.m_dataSource.TotalSleepTime > 0.0)
                    result.OxygenReduceIndex = (float)Math.Round((double)(num3 * 60) / (double)this.m_dataSource.TotalSleepTime, 2);
                result.MinBreathBloodOxygen = minSpO2ofBreath;
                foreach (DataEpoch dataEpoch in this.m_dataSource.TIBEpochs)
                {
                    if ((double)dataEpoch.MinSpO2 != 0.0)
                    {
                        float num4 = 100f - dataEpoch.MinSpO2;
                        if (dataEpoch.EnumStage == SleepStageEnum.W)
                        {
                            if ((double)result.WakeOxygenReduceIndex < (double)num4)
                                result.WakeOxygenReduceIndex = num4;
                        }
                        else if (dataEpoch.EnumStage == SleepStageEnum.R)
                        {
                            if ((double)result.REMOxygenReduceIndex < (double)num4)
                                result.REMOxygenReduceIndex = num4;
                        }
                        else if (dataEpoch.EnumStage != SleepStageEnum.None && (double)result.NREMOxygenReduceIndex < (double)num4)
                            result.NREMOxygenReduceIndex = num4;
                    }
                }
                //result.TotalOxygenReduceIndex = (double)result.WakeOxygenReduceIndex < (double)result.NREMOxygenReduceIndex || (double)result.WakeOxygenReduceIndex < (double)result.REMOxygenReduceIndex ? ((double)result.WakeOxygenReduceIndex > (double)result.NREMOxygenReduceIndex || (double)result.NREMOxygenReduceIndex < (double)result.REMOxygenReduceIndex ? result.REMOxygenReduceIndex : result.NREMOxygenReduceIndex) : result.WakeOxygenReduceIndex;
                //result.TotalOxygenReduceMaxDuration = (double)result.WakeOxygenReduceMaxDuration < (double)result.NREMOxygenReduceMaxDuration || (double)result.WakeOxygenReduceMaxDuration < (double)result.REMOxygenReduceMaxDuration ? ((double)result.WakeOxygenReduceMaxDuration > (double)result.NREMOxygenReduceMaxDuration || (double)result.NREMOxygenReduceMaxDuration < (double)result.REMOxygenReduceMaxDuration ? result.REMOxygenReduceMaxDuration : result.NREMOxygenReduceMaxDuration) : result.WakeOxygenReduceMaxDuration;
                result.TotalOxygenReduceIndex = result.WakeOxygenReduceIndex + result.NREMOxygenReduceIndex + result.REMOxygenReduceIndex;
                result.TotalOxygenReduceMaxDuration = result.WakeOxygenReduceMaxDuration + result.NREMOxygenReduceMaxDuration + result.REMOxygenReduceMaxDuration;
                IEnumerable <DataEvent> source5 = this.m_dataSource.SpO2Events.Where(t => Convert.ToSingle(t.Tag) <= this.m_dataSource.SpO2ThresholdValue && t.EventTyp != EventTypeEnum.Spo2Artifact);
                if (source5 != null)
                {
                    var valuesss = source5.Select(t => t.Duration);
                    result.BloodOxygenLowThan90Duration = (float)Math.Round(valuesss.Sum() / 60f, 2);
                    if ((double)this.m_dataSource.TotalSleepTime > 0.0)
                        result.BloodOxygenLowThan90DurationPercent = (float)Math.Round((double)result.BloodOxygenLowThan90Duration * 100.0 / (double)this.m_dataSource.TotalSleepTime, 2);
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
            return result;
        }
    }
}
