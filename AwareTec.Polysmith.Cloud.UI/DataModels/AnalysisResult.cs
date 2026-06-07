using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwareTec.Polysmith.DataBaseCom;
using AwareTec.Polysmith.pChart;
using AwareTec.Polysmith.DataCenter;
namespace AwareTec.Polysmith.Cloud.UI.DataModels
{
    /// <summary>
    /// 分析结果集合
    /// </summary>
    public class AnalysisResult:OutResult
    {
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { set; get; }
        /// <summary>
        /// 是否有数据被更新
        /// </summary>
        public bool HasDataChange { set; get; }
        /// <summary>
        /// 从数据库中读取
        /// </summary>
        public bool ReadFromdb { set; get; }
        /// <summary>
        /// 是否为老版本
        /// </summary>
        public bool OldVision { set; get; }

        public AnalysisResult()
            : base()
        {
            HasDataChange = false;
            ReadFromdb = false;
            OldVision = false;
        }
        /// <summary>
        /// 转换成业务数据结果类
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static AnalysisResult Convert(OutResult parent)
        {
            return new AnalysisResult
            {
                BloodOxygen = parent.BloodOxygen,
                BodyMovement = parent.BodyMovement,
                BodyState = parent.BodyState,
                BreathEvent = parent.BreathEvent,
                EdfPath = parent.EdfPath,
                GUID = parent.GUID,
                HeartRate = parent.HeartRate,
                LightOffTime = parent.LightOffTime,
                LightOnTime = parent.LightOnTime,
                Sleep = parent.Sleep,
                StartTime = parent.StartTime,
                Tag = parent.Tag,
                Epochs = parent.Epochs,
                Molar = parent.Molar,
                EventRecords = parent.EventRecords
            };
        }
        /// <summary>
        ///  转换成分析图表数据
        /// </summary>
        /// <returns></returns>
        public ResultDomain ConvertResultDomain(int EpochsCount, bool all = false)
        {
            ResultDomain resultDomain = new ResultDomain();
            resultDomain.StartTime = base.StartTime;
            resultDomain.EndTime = this.EndTime;
            try
            {
                if (base.Epochs.Count > EpochsCount)
                {
                    EpochsCount = base.Epochs.Count;
                }
                if (Epochs.Count == 0)
                {
                    for (int i = 0; i < EpochsCount; i++)
                    {
                        Epochs.Add(new Doc_Epochs() { EpochIndex = i });
                    }
                }
                else
                {
                    base.Epochs.Sort((Doc_Epochs t1, Doc_Epochs t2) => t1.EpochIndex.CompareTo(t2.EpochIndex));
                    if (Epochs.Count <= EpochsCount)
                    {
                        int start = Epochs.Count;
                        List<Doc_Epochs> bak_epochs = new List<Doc_Epochs>();
                        int epochIndex = 0;
                        for (int i = 0; i < start; i++)
                        {
                            int index = Epochs[i].EpochIndex;
                            if (index < EpochsCount)
                            {
                                for (int j = epochIndex; j < index; j++)
                                {
                                    bak_epochs.Add(new Doc_Epochs() { EpochIndex = j });
                                }
                                Epochs[i].EpochExist = true;
                                bak_epochs.Add(Epochs[i]);
                                epochIndex = Epochs[i].EpochIndex + 1;
                            }
                        }
                        if (epochIndex < EpochsCount)
                        {
                            for (int i = epochIndex; i < EpochsCount; i++)
                            {
                                bak_epochs.Add(new Doc_Epochs() { EpochIndex = i });
                            }
                        }
                        Epochs = bak_epochs;
                    }
                }
            }
            catch
            {
                EpochsCount = base.Epochs.Count;
            }
            resultDomain.MultSleepRecords= EventRecords.Where(t => t.EventType == (int)IMarker.MarkType.MultipleSleep).ToList();
            int LightOffIndex = 0;
            int LightOnIndex = EpochsCount-1;
            if (!all && base.Sleep != null)
            {
                if (base.Sleep.LightOffTime <= this.EndTime && base.Sleep.LightOffTime >= base.StartTime)
                {
                    double totalSeconds = (base.Sleep.LightOffTime - base.StartTime).TotalSeconds;
                    LightOffIndex = (int)(totalSeconds / 30.0) - 1;
                    if (totalSeconds % 30.0 != 0.0 || totalSeconds == 0.0)
                    {
                        LightOffIndex++;
                    }
                    resultDomain.StartTime = base.StartTime.AddSeconds((double)(LightOffIndex * 30));
                }
                if (base.Sleep.LightOnTime <= this.EndTime && base.Sleep.LightOnTime >= base.StartTime)
                {
                    double totalSeconds2 = (base.Sleep.LightOnTime - base.StartTime).TotalSeconds;
                    LightOnIndex = (int)(totalSeconds2 / 30.0)-1;
                    if (totalSeconds2 % 30.0 != 0.0 || totalSeconds2 == 0.0)
                    {
                        LightOnIndex++;
                    }
                    resultDomain.EndTime = base.StartTime.AddSeconds((double)(LightOnIndex * 30));
                }
                resultDomain.OffSetFrameCnt = LightOffIndex + 1;
            }
            EpochsCount = LightOnIndex - LightOffIndex + 1;
            resultDomain.BloodOxygenPoints = new SuperPointF[EpochsCount];
            resultDomain.BodyStatePoints = new SuperPointF[EpochsCount];
            resultDomain.CAPoints = new SuperPointF[EpochsCount];
            resultDomain.HypopneaPoints = new SuperPointF[EpochsCount];
            resultDomain.HeartRatePoints = new SuperPointF[EpochsCount];
            resultDomain.MAPoints = new SuperPointF[EpochsCount];
            resultDomain.OAPoints = new SuperPointF[EpochsCount];
            resultDomain.PLMsPoints = new SuperPointF[EpochsCount];
            resultDomain.MicArousalPoints = new SuperPointF[EpochsCount];
            resultDomain.PLMPoints = new SuperPointF[EpochsCount];
            resultDomain.SleepStagPoints = new SuperPointF[EpochsCount];
            resultDomain.MTPoints = new SuperPointF[EpochsCount];
            int num3 = (Epochs.Count > LightOnIndex) ? LightOnIndex + 1 : Epochs.Count;
            for (int i = LightOffIndex; i < num3; i++)
            {
                int num4 = base.Epochs[i].EpochIndex - LightOffIndex;
                if (num4 >= 0 && num4 < EpochsCount)
                {
                    string[] array = base.Epochs[i].SpO2.Split(new char[]
					{
						','
					});
                    if (array.Length >= 2)
                    {
                        resultDomain.BloodOxygenPoints[num4] = new SuperPointF
                        {
                            XValue = (float)num4,
                            YMaxValue = float.Parse(array[1]),
                            YMinValue = float.Parse(array[0])
                        };
                    }
                    resultDomain.BodyStatePoints[num4] = new SuperPointF
                    {
                        XValue = (float)num4,
                        YMaxValue = (float)base.Epochs[i].Pos
                    };
                    resultDomain.SleepStagPoints[num4] = new SuperPointF
                    {
                        XValue = (float)num4,
                        YMaxValue = (float)base.Epochs[i].Stage
                    };
                    array = base.Epochs[i].HeartRate.Split(new char[]
					{
						','
					});
                    if (array.Length >= 2)
                    {
                        resultDomain.HeartRatePoints[num4] = new SuperPointF
                        {
                            XValue = (float)num4,
                            YMaxValue = float.Parse(array[1]),
                            YMinValue = float.Parse(array[0])
                        };
                    }
                    resultDomain.OAPoints[num4] = new SuperPointF
                    {
                        XValue = (float)num4,
                        YMaxValue = base.Epochs[i].OA,
                        YMinValue = 0f
                    };
                    resultDomain.MAPoints[num4] = new SuperPointF
                    {
                        XValue = (float)num4,
                        YMaxValue = base.Epochs[i].MA,
                        YMinValue = 0f
                    };
                    resultDomain.CAPoints[num4] = new SuperPointF
                    {
                        XValue = (float)num4,
                        YMaxValue = base.Epochs[i].CA,
                        YMinValue = 0f
                    };
                    resultDomain.HypopneaPoints[num4] = new SuperPointF
                    {
                        XValue = (float)num4,
                        YMaxValue = base.Epochs[i].Hypopnea,
                        YMinValue = 0f
                    };
                    resultDomain.MTPoints[num4] = new SuperPointF
                    {
                        XValue = (float)num4,
                        YMaxValue = base.Epochs[i].MT,
                        YMinValue = 0f
                    };
                    resultDomain.MicArousalPoints[num4] = new SuperPointF
                    {
                        XValue = (float)num4,
                        YMaxValue = base.Epochs[i].MicArousal,
                        YMinValue = 0f
                    };
                    resultDomain.PLMsPoints[num4] = new SuperPointF
                    {
                        XValue = (float)num4,
                        YMaxValue = base.Epochs[i].PLMs,
                        YMinValue = 0f
                    };
                    resultDomain.PLMPoints[num4] = new SuperPointF
                    {
                        XValue = (float)num4,
                        YMaxValue = base.Epochs[i].PLM,
                        YMinValue = 0f
                    };
                }
            }
            return resultDomain;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public List<Doc_Epochs> ConvertEpochs(ResultDomain domain)
        {
            List<Doc_Epochs> list = new List<Doc_Epochs>();
            int num = domain.SleepStagPoints.Length;
            for (int i = 0; i < num; i++)
            {
                list.Add(new Doc_Epochs
                {
                    EpochIndex = i,
                    GUID = base.GUID,
                    CA = domain.CAPoints[i].YMaxValue,
                    OA = domain.OAPoints[i].YMaxValue,
                    MA = domain.MAPoints[i].YMaxValue,
                    Pos = (int)domain.BodyStatePoints[i].YMaxValue,
                    Stage = (int)domain.SleepStagPoints[i].YMaxValue,
                    Hypopnea = domain.HypopneaPoints[i].YMaxValue,
                    MicArousal=domain.MicArousalPoints[i].YMaxValue,
                    PLMs = domain.PLMsPoints[i].YMaxValue,
                    PLM=domain.PLMPoints[i].YMaxValue,
                    HeartRate = string.Format("{0},{1}", domain.HeartRatePoints[i].YMinValue, domain.HeartRatePoints[i].YMaxValue),
                    SpO2 = string.Format("{0},{1}", domain.BloodOxygenPoints[i].YMinValue, domain.BloodOxygenPoints[i].YMaxValue)
                });
            }
            return list;
        }
    }

    /// <summary>
    /// 进度状态枚举
    /// </summary>
    public enum ProgressState
    {
        /// <summary>
        /// 无
        /// </summary>
        None=0,
        /// <summary>
        /// 准备
        /// </summary>
        Ready=1,
        /// <summary>
        /// 分析中
        /// </summary>
        Running=2,
        /// <summary>
        /// 监测中
        /// </summary>
        Monitoring=3,
        /// <summary>
        /// 监测完毕
        /// </summary>
        MonitorFinish=4,
        /// <summary>
        /// 回放
        /// </summary>
        Replay=5,
        /// <summary>
        /// 临时
        /// </summary>
        Temporary=88,
        /// <summary>
        /// 已完成
        /// </summary>
        Compelet=99
    }
}
