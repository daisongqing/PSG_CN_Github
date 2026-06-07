using AwareTec.Polysmith.DataBaseCom;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AwareTec.Polysmith.DataCenter
{
    /// <summary>
    /// 体位统计信息
    /// </summary>
    internal class BodyPosResult
    {
        private IDataBase m_DataSource = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="datasource"></param>
        public BodyPosResult(IDataBase datasource)
        {
            m_DataSource = datasource;
        }
        /// <summary>
        /// 获取指标结果
        /// </summary>
        /// <returns></returns>
        public Doc_BodyStateResult getResult()
        {
            Doc_BodyStateResult result = new Doc_BodyStateResult();
            result.MinSpo2_Sit_R = result.MinSpo2_Sit_UnR = result.MinSpo2_UnSit_R = result.MinSpo2_UnSit_UnR = 100;
            float totalSpo2_Sit_R = 0, totalSpo2_Sit_UnR = 0, totalSpo2_UnSit_R = 0, totalSpo2_UnSit_UnR = 0;
            int countSpo2_Sit_R = 0, countSpo2_Sit_UnR = 0, countSpo2_UnSit_R = 0, countSpo2_UnSit_UnR = 0;
            result.AveSpo2_Sit_R = result.AveSpo2_Sit_UnR = result.AveSpo2_UnSit_R = result.AveSpo2_UnSit_UnR = 0;
            foreach (DataEpoch dataEpoch in this.m_DataSource.TSTEpochs)
            {
                ///以前判断了非w期，现在去掉
                if (true) //(dataEpoch.EnumStage != SleepStageEnum.W)
                {
                    switch (dataEpoch.EnumPos)
                    {
                        case BodyPosEnum.S:
                            result.SitTotalSleepTimeDuration += 30f;
                            ///Up与S定义反了
                            switch (dataEpoch.EnumStage)
                            {
                                case SleepStageEnum.N1:
                                    result.UpTimesInN1SleepTime += 0.5f;
                                    break;
                                case SleepStageEnum.N2:
                                    result.UpTimesInN2SleepTime += 0.5f;
                                    break;
                                case SleepStageEnum.N3:
                                    result.UpTimesInN3SleepTime += 0.5f;
                                    break;
                                case SleepStageEnum.R:
                                    result.UpTimesInREMSleepTime += 0.5f;
                                    break;
                            }
                            break;
                        case BodyPosEnum.L:
                        case BodyPosEnum.R:
                            if (dataEpoch.EnumPos == BodyPosEnum.L)
                                result.LeftTotalSleepTimeDuration += 30f;
                            else
                                result.RightTotalSleepTimeDuration += 30f;
                            switch (dataEpoch.EnumStage)
                            {
                                case SleepStageEnum.N1:
                                    result.LateralTimesInN1SleepTime += 0.5f;
                                    break;
                                case SleepStageEnum.N2:
                                    result.LateralTimesInN2SleepTime += 0.5f;
                                    break;
                                case SleepStageEnum.N3:
                                    result.LateralTimesInN3SleepTime += 0.5f;
                                    break;
                                case SleepStageEnum.R:
                                    result.LateralTimesInREMSleepTime += 0.5f;
                                    break;
                            }
                            break;
                        case BodyPosEnum.P:
                            result.ProstrateTotalSleepTimeDuration += 30f;
                            switch (dataEpoch.EnumStage)
                            {
                                case SleepStageEnum.N1:
                                    result.ProstrateTimesInN1SleepTime += 0.5f;
                                    break;
                                case SleepStageEnum.N2:
                                    result.ProstrateTimesInN2SleepTime += 0.5f;
                                    break;
                                case SleepStageEnum.N3:
                                    result.ProstrateTimesInN3SleepTime += 0.5f;
                                    break;
                                case SleepStageEnum.R:
                                    result.ProstrateTimesInREMSleepTime += 0.5f;
                                    break;
                            }
                            break;
                        case BodyPosEnum.UP:
                            result.UpTotalSleepTimeDuration += 30f;
                            switch (dataEpoch.EnumStage)
                            {
                                case SleepStageEnum.N1:
                                    result.SitTimesInN1SleepTime += 0.5f;
                                    break;
                                case SleepStageEnum.N2:
                                    result.SitTimesInN2SleepTime += 0.5f;
                                    break;
                                case SleepStageEnum.N3:
                                    result.SitTimesInN3SleepTime += 0.5f;
                                    break;
                                case SleepStageEnum.R:
                                    result.SitTimesInREMSleepTime += 0.5f;
                                    break;
                            }
                            break;
                    }
                }
                if (dataEpoch.EnumStage != SleepStageEnum.W)
                {
                    switch (dataEpoch.EnumPos)
                    {
                        case BodyPosEnum.S:
                            if (dataEpoch.EnumStage == SleepStageEnum.R)
                            {
                                if (dataEpoch.AveSpO2 > 0)
                                {
                                    totalSpo2_Sit_R += dataEpoch.AveSpO2;
                                    countSpo2_Sit_R++;
                                }
                                result.MinSpo2_Sit_R = dataEpoch.MinSpO2 > 0 && dataEpoch.MinSpO2 < result.MinSpo2_Sit_R ? dataEpoch.MinSpO2 : result.MinSpo2_Sit_R;
                            }
                            else
                            {
                                if (dataEpoch.AveSpO2 > 0)
                                {
                                    totalSpo2_Sit_UnR += dataEpoch.AveSpO2;
                                    countSpo2_Sit_UnR++;
                                }
                                result.MinSpo2_Sit_UnR = dataEpoch.MinSpO2 > 0 && dataEpoch.MinSpO2 < result.MinSpo2_Sit_UnR ? dataEpoch.MinSpO2 : result.MinSpo2_Sit_UnR;
                            }
                            break;
                        case BodyPosEnum.L:
                        case BodyPosEnum.R:
                        case BodyPosEnum.P:
                        case BodyPosEnum.UP:
                            if (dataEpoch.EnumStage == SleepStageEnum.R)
                            {
                                if (dataEpoch.AveSpO2 > 0)
                                {
                                    totalSpo2_UnSit_R += dataEpoch.AveSpO2;
                                    countSpo2_UnSit_R++;
                                }
                                result.MinSpo2_UnSit_R = dataEpoch.MinSpO2 > 0 && dataEpoch.MinSpO2 < result.MinSpo2_UnSit_R ? dataEpoch.MinSpO2 : result.MinSpo2_UnSit_R;

                            }
                            else
                            {
                                if (dataEpoch.AveSpO2 > 0)
                                {
                                    totalSpo2_UnSit_UnR += dataEpoch.AveSpO2;
                                    countSpo2_UnSit_UnR++;
                                }
                                result.MinSpo2_UnSit_UnR = dataEpoch.MinSpO2 > 0 && dataEpoch.MinSpO2 < result.MinSpo2_UnSit_UnR ? dataEpoch.MinSpO2 : result.MinSpo2_UnSit_UnR;
                            }
                            break;

                    }
                }
            }
            result.MinSpo2_Sit_R = (float)Math.Round(result.MinSpo2_Sit_R == 100 ? 0 : result.MinSpo2_Sit_R, 2);
            result.MinSpo2_Sit_UnR = (float)Math.Round(result.MinSpo2_Sit_UnR == 100 ? 0 : result.MinSpo2_Sit_UnR, 2);
            result.MinSpo2_UnSit_R = (float)Math.Round(result.MinSpo2_UnSit_R == 100 ? 0 : result.MinSpo2_UnSit_R, 2);
            result.MinSpo2_UnSit_UnR = (float)Math.Round(result.MinSpo2_UnSit_UnR == 100 ? 0 : result.MinSpo2_UnSit_UnR, 2);
            result.AveSpo2_Sit_R = (float)Math.Round(countSpo2_Sit_R > 0 ? totalSpo2_Sit_R / countSpo2_Sit_R : 0, 2);
            result.AveSpo2_Sit_UnR = (float)Math.Round(countSpo2_Sit_UnR > 0 ? totalSpo2_Sit_UnR / countSpo2_Sit_UnR : 0, 2);
            result.AveSpo2_UnSit_R = (float)Math.Round(countSpo2_UnSit_R > 0 ? totalSpo2_UnSit_R / countSpo2_UnSit_R : 0, 2);
            result.AveSpo2_UnSit_UnR = (float)Math.Round(countSpo2_UnSit_UnR > 0 ? totalSpo2_UnSit_UnR / countSpo2_UnSit_UnR : 0, 2);

            result.UpTimesInTotalSleepTime = result.UpTimesInN1SleepTime + result.UpTimesInN2SleepTime + result.UpTimesInN3SleepTime + result.UpTimesInREMSleepTime;
            result.ProstrateTimesInTotalSleepTime = result.ProstrateTimesInN1SleepTime + result.ProstrateTimesInN2SleepTime + result.ProstrateTimesInN3SleepTime + result.ProstrateTimesInREMSleepTime;
            result.LateralTimesInTotalSleepTime = result.LateralTimesInN1SleepTime + result.LateralTimesInN2SleepTime + result.LateralTimesInN3SleepTime + result.LateralTimesInREMSleepTime;
            result.SitTimesInTotalSleepTime = result.SitTimesInN1SleepTime += result.SitTimesInN2SleepTime += result.SitTimesInN3SleepTime += result.SitTimesInREMSleepTime;
            result.UnSupineTimesInTotalSleepTime = result.ProstrateTimesInTotalSleepTime + result.LateralTimesInTotalSleepTime + result.SitTimesInTotalSleepTime;
            result.SitSleepCA = result.RightSleepCA = (result.LeftSleepCA = (result.UpSleepCA = (result.ProstrateSleepCA = 0f)));
            result.ProstrateSleepMA = (result.RightSleepMA = (result.LeftSleepMA = (result.UpSleepMA = (result.SitSleepMA = 0f))));
            result.LeftSleepOA = (result.ProstrateSleepOA = (result.RightSleepOA = (result.SitSleepOA = (result.UpSleepOA = 0f))));
            result.LeftSleepHYP = (result.ProstrateSleepHYP = (result.RightSleepHYP = (result.SitSleepHYP = (result.UpSleepHYP = 0f))));
            List<DataEvent> breathEvents = this.m_DataSource.BreathEvents;

            for (int i = 0; i < breathEvents.Count; i++)
            {
                DataEvent dataEvent = breathEvents[i];
                if (dataEvent.EventTyp == EventTypeEnum.Resp_CA)
                {
                    bool C1 = false, C2 = false, C3 = false, C4 = false, C5 = false, C6 = false, C7 = false, C8 = false, C9 = false, C10 = false, C11 = false;
                    for (int s = 0; s < dataEvent.OnPos.Length; s++)
                    {
                        switch (dataEvent.OnPos[s])
                        {
                            case (int)BodyPosEnum.S:
                                if (!C1)
                                {
                                    result.SitSleepCA += 1f;
                                    result.SitSleepCATotalTimes += dataEvent.Duration;
                                    C1 = true;
                                }
                                if (dataEvent.OnStages[s] == (int)SleepStageEnum.R)
                                {
                                    if (!C10)
                                    {
                                        result.REMUpSleepCA += 1f;
                                        C10 = true;
                                    }
                                }
                                else if (dataEvent.OnStages[s] != (int)SleepStageEnum.W && dataEvent.OnStages[s] != (int)SleepStageEnum.None)
                                {
                                    if (!C11)
                                    {
                                        result.NREMUpSleepCA += 1f;
                                        C11 = true;
                                    }
                                }
                                break;
                            case (int)BodyPosEnum.L:
                            case (int)BodyPosEnum.R:
                                if (dataEvent.OnPos[s] == (int)BodyPosEnum.L)
                                {
                                    if (!C2)
                                    {
                                        result.LeftSleepCA += 1f;
                                        result.LeftSleepCATotalTimes += dataEvent.Duration;
                                        C2 = true;
                                    }
                                }
                                else
                                {
                                    if (!C3)
                                    {
                                        result.RightSleepCA += 1f;
                                        result.RightSleepCATotalTimes += dataEvent.Duration;
                                        C3 = true;
                                    }
                                }
                                if (dataEvent.OnStages[s] == (int)SleepStageEnum.R)
                                {
                                    if (!C6)
                                    {
                                        result.REMLateralSleepCA += 1f;
                                        C6 = true;
                                    }
                                }
                                else if (dataEvent.OnStages[s] != (int)SleepStageEnum.W && dataEvent.OnStages[s] != (int)SleepStageEnum.None)
                                {
                                    if (!C7)
                                    {
                                        result.NREMLateralSleepCA += 1f;
                                        C7 = true;
                                    }
                                }
                                break;
                            case (int)BodyPosEnum.P:
                                if (!C4)
                                {
                                    result.ProstrateSleepCA += 1f;
                                    result.ProstrateSleepCATotalTimes += dataEvent.Duration;
                                    C4 = true;
                                }
                                if (dataEvent.OnStages[s] == (int)SleepStageEnum.R)
                                {
                                    if (!C8)
                                    {
                                        result.REMProstrateSleepCA += 1f;
                                        C8 = true;
                                    }
                                }
                                else if (dataEvent.OnStages[s] != (int)SleepStageEnum.W && dataEvent.OnStages[s] != (int)SleepStageEnum.None)
                                {
                                    if (!C9)
                                    {
                                        result.NREMProstrateSleepCA += 1f;
                                        C9 = true;
                                    }
                                }
                                break;
                            case (int)BodyPosEnum.UP:
                                if (!C5)
                                {
                                    result.UpSleepCA += 1f;
                                    result.UpSleepCATotalTimes += dataEvent.Duration;
                                    C5 = true;
                                }

                                break;
                        }
                    }
                    if (C2 || C3)
                    {
                        result.LateralSleepCATotalTimes += dataEvent.Duration;
                        result.LateralSleepCA += 1;
                    }
                }
                else if (dataEvent.EventTyp == EventTypeEnum.Resp_MA)
                {
                    bool C1 = false, C2 = false, C3 = false, C4 = false, C5 = false, C6 = false, C7 = false, C8 = false, C9 = false, C10 = false, C11 = false;
                    for (int s = 0; s < dataEvent.OnPos.Length; s++)
                    {
                        switch (dataEvent.OnPos[s])
                        {
                            case (int)BodyPosEnum.S:
                                if (!C1)
                                {
                                    result.SitSleepMA += 1f;
                                    result.SitSleepMATotalTimes += dataEvent.Duration;
                                    C1 = true;
                                }
                                if (dataEvent.OnStages[s] == (int)SleepStageEnum.R)
                                {
                                    if (!C10)
                                    {
                                        result.REMUpSleepMA += 1f;
                                        C10 = true;
                                    }
                                }
                                else if (dataEvent.OnStages[s] != (int)SleepStageEnum.W && dataEvent.OnStages[s] != (int)SleepStageEnum.None)
                                {
                                    if (!C11)
                                    {
                                        result.NREMUpSleepMA += 1f;
                                        C11 = true;
                                    }
                                }
                                break;
                            case (int)BodyPosEnum.L:
                            case (int)BodyPosEnum.R:
                                if (dataEvent.OnPos[s] == (int)BodyPosEnum.L)
                                {
                                    if (!C2)
                                    {
                                        result.LeftSleepMA += 1f;
                                        result.LeftSleepMATotalTimes += dataEvent.Duration;
                                        C2 = true;
                                    }
                                }
                                else
                                {
                                    if (!C3)
                                    {
                                        result.RightSleepMA += 1f;
                                        result.RightSleepMATotalTimes += dataEvent.Duration;
                                        C3 = true;
                                    }
                                }
                                if (dataEvent.OnStages[s] == (int)SleepStageEnum.R)
                                {
                                    if (!C6)
                                    {
                                        result.REMLateralSleepMA += 1f;
                                        C6 = true;
                                    }
                                }
                                else if (dataEvent.OnStages[s] != (int)SleepStageEnum.W && dataEvent.OnStages[s] != (int)SleepStageEnum.None)
                                {
                                    if (!C7)
                                    {
                                        result.NREMLateralSleepMA += 1f;
                                        C7 = true;
                                    }
                                }
                                break;
                            case (int)BodyPosEnum.P:
                                if (!C4)
                                {
                                    result.ProstrateSleepMA += 1f;
                                    result.ProstrateSleepMATotalTimes += dataEvent.Duration;
                                    C4 = true;
                                }
                                if (dataEvent.OnStages[s] == (int)SleepStageEnum.R)
                                {
                                    if (!C8)
                                    {
                                        result.REMProstrateSleepMA += 1f;
                                        C8 = true;
                                    }
                                }
                                else if (dataEvent.OnStages[s] != (int)SleepStageEnum.W && dataEvent.OnStages[s] != (int)SleepStageEnum.None)
                                {
                                    if (!C9)
                                    {
                                        result.NREMProstrateSleepMA += 1f;
                                        C9 = true;
                                    }
                                }
                                break;
                            case (int)BodyPosEnum.UP:
                                if (!C5)
                                {
                                    result.UpSleepMA += 1f;
                                    result.UpSleepMATotalTimes += dataEvent.Duration;
                                    C5 = true;
                                }

                                break;
                        }
                    }
                    if (C2 || C3)
                    {
                        result.LateralSleepMATotalTimes += dataEvent.Duration;
                        result.LateralSleepMA += 1;
                    }
                }
                else if (dataEvent.EventTyp != EventTypeEnum.Resp_Hypnea)
                {
                    bool C1 = false, C2 = false, C3 = false, C4 = false, C5 = false, C6 = false, C7 = false, C8 = false, C9 = false, C10 = false, C11 = false;
                    for (int s = 0; s < dataEvent.OnPos.Length; s++)
                    {
                        switch (dataEvent.OnPos[s])
                        {
                            case (int)BodyPosEnum.S:
                                if (!C1)
                                {
                                    result.SitSleepOA += 1f;
                                    result.SitSleepOATotalTimes += dataEvent.Duration;
                                    C1 = true;
                                }
                                if (dataEvent.OnStages[s] == (int)SleepStageEnum.R)
                                {
                                    if (!C10)
                                    {
                                        result.REMUpSleepOA += 1f;
                                        C10 = true;
                                    }
                                }
                                else if (dataEvent.OnStages[s] != (int)SleepStageEnum.W && dataEvent.OnStages[s] != (int)SleepStageEnum.None)
                                {
                                    if (!C11)
                                    {
                                        result.NREMUpSleepOA += 1f;
                                        C11 = true;
                                    }
                                }
                                break;
                            case (int)BodyPosEnum.L:
                            case (int)BodyPosEnum.R:
                                if (dataEvent.OnPos[s] == (int)BodyPosEnum.L)
                                {
                                    if (!C2)
                                    {
                                        result.LeftSleepOA += 1f;
                                        result.LeftSleepOATotalTimes += dataEvent.Duration;
                                        C2 = true;
                                    }
                                }
                                else
                                {
                                    if (!C3)
                                    {
                                        result.RightSleepOA += 1f;
                                        result.RightSleepOATotalTimes += dataEvent.Duration;
                                        C3 = true;
                                    }
                                }
                                if (dataEvent.OnStages[s] == (int)SleepStageEnum.R)
                                {
                                    if (!C6)
                                    {
                                        result.REMLateralSleepOA += 1f;
                                        C6 = true;
                                    }
                                }
                                else if (dataEvent.OnStages[s] != (int)SleepStageEnum.W && dataEvent.OnStages[s] != (int)SleepStageEnum.None)
                                {
                                    if (!C7)
                                    {
                                        result.NREMLateralSleepOA += 1f;
                                        C7 = true;
                                    }
                                }
                                break;
                            case (int)BodyPosEnum.P:
                                if (!C4)
                                {
                                    result.ProstrateSleepOA += 1f;
                                    result.ProstrateSleepOATotalTimes += dataEvent.Duration;
                                    C4 = true;
                                }
                                if (dataEvent.OnStages[s] == (int)SleepStageEnum.R)
                                {
                                    if (!C8)
                                    {
                                        result.REMProstrateSleepOA += 1f;
                                        C8 = true;
                                    }
                                }
                                else if (dataEvent.OnStages[s] != (int)SleepStageEnum.W && dataEvent.OnStages[s] != (int)SleepStageEnum.None)
                                {
                                    if (!C9)
                                    {
                                        result.NREMProstrateSleepOA += 1f;
                                        C9 = true;
                                    }
                                }
                                break;
                            case (int)BodyPosEnum.UP:
                                if (!C5)
                                {
                                    result.UpSleepOA += 1f;
                                    result.UpSleepOATotalTimes += dataEvent.Duration;
                                    C5 = true;
                                }
                                break;
                        }
                    }
                    if (C2 || C3)
                    {
                        result.LateralSleepOATotalTimes += dataEvent.Duration;
                        result.LateralSleepOA += 1;
                    }
                }
                else
                {
                    bool C1 = false, C2 = false, C3 = false, C4 = false, C5 = false, C6 = false, C7 = false, C8 = false, C9 = false, C10 = false, C11 = false;
                    for (int s = 0; s < dataEvent.OnPos.Length; s++)
                    {
                        switch (dataEvent.OnPos[s])
                        {
                            case (int)BodyPosEnum.S:
                                if (!C1)
                                {
                                    result.SitSleepHYP += 1f;
                                    result.SitSleepHYPTotalTimes += dataEvent.Duration;
                                    C1 = true;
                                }
                                ///睡眠分期这里的指标UP与S弄反了
                                if (dataEvent.OnStages[s] == (int)SleepStageEnum.R)
                                {
                                    if (!C10)
                                    {
                                        result.REMUpSleepHYP += 1f;
                                        C10 = true;
                                    }
                                }
                                else if (dataEvent.OnStages[s] != (int)SleepStageEnum.W && dataEvent.OnStages[s] != (int)SleepStageEnum.None)
                                {
                                    if (!C11)
                                    {
                                        result.NREMUpSleepHYP += 1f;
                                        C11 = true;
                                    }
                                }
                                break;
                            case (int)BodyPosEnum.L:
                            case (int)BodyPosEnum.R:
                                if (dataEvent.OnPos[s] == (int)BodyPosEnum.L)
                                {
                                    if (!C2)
                                    {
                                        result.LeftSleepHYP += 1f;
                                        result.LeftSleepHYPTotalTimes += dataEvent.Duration;
                                        C2 = true;
                                    }
                                }
                                else
                                {
                                    if (!C3)
                                    {
                                        result.RightSleepHYP += 1f;
                                        result.RightSleepHYPTotalTimes += dataEvent.Duration;
                                        C3 = true;
                                    }
                                }
                                if (dataEvent.OnStages[s] == (int)SleepStageEnum.R)
                                {
                                    if (!C6)
                                    {
                                        result.REMLateralSleepHYP += 1f;
                                        C6 = true;
                                    }
                                }
                                else if (dataEvent.OnStages[s] != (int)SleepStageEnum.W && dataEvent.OnStages[s] != (int)SleepStageEnum.None)
                                {
                                    if (!C7)
                                    {
                                        result.NREMLateralSleepHYP += 1f;
                                        C7 = true;
                                    }
                                }
                                break;
                            case (int)BodyPosEnum.P:
                                if (!C4)
                                {
                                    result.ProstrateSleepHYP += 1f;
                                    result.ProstrateSleepHYPTotalTimes += dataEvent.Duration;
                                    C4 = true;
                                }
                                if (dataEvent.OnStages[s] == (int)SleepStageEnum.R)
                                {
                                    if (!C8)
                                    {
                                        result.REMProstrateSleepHYP += 1f;
                                        C8 = true;
                                    }
                                }
                                else if (dataEvent.OnStages[s] != (int)SleepStageEnum.W && dataEvent.OnStages[s] != (int)SleepStageEnum.None)
                                {
                                    if (!C9)
                                    {
                                        result.NREMProstrateSleepHYP += 1f;
                                        C9 = true;
                                    }
                                }
                                break;
                            case (int)BodyPosEnum.UP:
                                if (!C5)
                                {
                                    result.UpSleepHYP += 1f;
                                    result.UpSleepHYPTotalTimes += dataEvent.Duration;
                                    C5 = true;
                                }
                                break;
                        }
                    }
                    if (C2 || C3)
                    {
                        result.LateralSleepHYPTotalTimes += dataEvent.Duration;
                        result.LateralSleepHYP += 1;
                    }
                }
            }
            result.SitTotalSleepTimeDuration /= 60f;
            result.LeftTotalSleepTimeDuration /= 60f;
            result.RightTotalSleepTimeDuration /= 60f;
            result.ProstrateTotalSleepTimeDuration /= 60f;
            result.UpTotalSleepTimeDuration /= 60f;
            result.LeftSleepRERA = (result.ProstrateSleepRERA = (result.RightSleepRERA = (result.SitSleepRERA = (result.UpSleepRERA = 0f))));
            foreach (DataEvent dataEvent2 in this.m_DataSource.ArousalEvents)
            {
                DateTime dt = dataEvent2.StartTime.AddSeconds((double)(0f - this.m_DataSource.TimeThresholdValue_arousal));
                try
                {
                    DataEvent dataEvent3 = breathEvents.Find((DataEvent t) => t.StartTime <= dt && t.EndTime >= dt);
                    if (dataEvent3 != null)
                    {
                        if (dataEvent2.OnPos.Contains(1))
                        {
                            result.SitSleepRERA += 1f;
                        }
                        if (dataEvent2.OnPos.Contains(2))
                        {
                            result.LeftSleepRERA += 1f;
                        }
                        if (dataEvent2.OnPos.Contains(3))
                        {
                            result.RightSleepRERA += 1f;
                        }
                        if (dataEvent2.OnPos.Contains(4))
                        {
                            result.ProstrateSleepRERA += 1f;
                        }
                        if (dataEvent2.OnPos.Contains(5))
                        {
                            result.UpSleepRERA += 1f;
                        }
                    }
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee.Message);
                }
            }
            result.LeftSleepDesat = (result.ProstrateSleepDesat = (result.RightSleepDesat = (result.SitSleepDesat = (result.UpSleepDesat = 0f))));
            result.LeftSleepDesatIndex = result.ProstrateSleepDesatIndex = result.RightSleepDesatIndex = result.SitSleepDesatIndex = result.UpSleepDesatIndex = 0f;

            result.SitSleepATotalCount = result.SitSleepOA + result.SitSleepCA + result.SitSleepMA;
            result.SitSleepATotalTimes = result.SitSleepCATotalTimes + result.SitSleepMATotalTimes + result.SitSleepOATotalTimes;
            result.SitSleepAHTotalCount = result.SitSleepOA + result.SitSleepCA + result.SitSleepMA + result.SitSleepHYP;
            result.SitSleepAHTotalTimes = result.SitSleepCATotalTimes + result.SitSleepMATotalTimes + result.SitSleepOATotalTimes + result.SitSleepHYPTotalTimes;
            result.SitSleepAHTotalTimesMinutes = (float)Math.Round(result.SitSleepAHTotalTimes / 60 ,2);

            result.LeftSleepATotalCount = result.LeftSleepOA + result.LeftSleepCA + result.LeftSleepMA;
            result.LeftSleepATotalTimes = result.LeftSleepCATotalTimes + result.LeftSleepOATotalTimes + result.LeftSleepMATotalTimes;
            result.LeftSleepAHTotalCount = result.LeftSleepOA + result.LeftSleepCA + result.LeftSleepMA + result.LeftSleepHYP;
            result.LeftSleepAHTotalTimes = result.LeftSleepCATotalTimes + result.LeftSleepHYPTotalTimes + result.LeftSleepMATotalTimes + result.LeftSleepOATotalTimes;
            result.LeftSleepAHTotalTimesMinutes= (float)Math.Round(result.LeftSleepAHTotalTimes / 60, 2);

            result.RightSleepATotalCount = result.RightSleepOA + result.RightSleepCA + result.RightSleepMA;
            result.RightSleepATotalTimes = result.RightSleepCATotalTimes + result.RightSleepMATotalTimes + result.RightSleepOATotalTimes;
            result.RightSleepAHTotalCount = result.RightSleepOA + result.RightSleepCA + result.RightSleepMA + result.RightSleepHYP;
            result.RightSleepAHTotalTimes = result.RightSleepCATotalTimes + result.RightSleepHYPTotalTimes + result.RightSleepMATotalTimes + result.RightSleepOATotalTimes;
            result.RightSleepAHTotalTimesMinutes= (float)Math.Round(result.RightSleepAHTotalTimes / 60, 2);

            result.LateralSleepATotalCount = result.LateralSleepCA + result.LateralSleepOA + result.LateralSleepMA;
            result.LateralSleepATotalTimes = result.LateralSleepCATotalTimes + result.LateralSleepMATotalTimes + result.LateralSleepOATotalTimes;
            result.LateralSleepAHTotalCount = result.LateralSleepCA + result.LateralSleepOA + result.LateralSleepMA + result.LateralSleepHYP;
            result.LateralSleepAHTotalTimes = result.LateralSleepCATotalTimes + result.LateralSleepMATotalTimes + result.LateralSleepOATotalTimes + result.LateralSleepHYPTotalTimes;

            result.UpSleepATotalCount = result.UpSleepOA + result.UpSleepCA + result.UpSleepMA;
            result.UpSleepATotalTimes = result.UpSleepOATotalTimes + result.UpSleepCATotalTimes + result.UpSleepMATotalTimes;
            result.UpSleepAHTotalCount = result.UpSleepOA + result.UpSleepCA + result.UpSleepMA + result.UpSleepHYP;
            result.UpSleepAHTotalTimes = result.UpSleepOATotalTimes + result.UpSleepCATotalTimes + result.UpSleepMATotalTimes + result.UpSleepHYPTotalTimes;
            result.UpSleepAHTotalTimesMinutes = (float)Math.Round(result.UpSleepAHTotalTimes / 60, 2);

            result.ProstrateSleepATotalCount = result.ProstrateSleepOA + result.ProstrateSleepCA + result.ProstrateSleepMA;
            result.ProstrateSleepATotalTimes = result.ProstrateSleepCATotalTimes + result.ProstrateSleepMATotalTimes + result.ProstrateSleepOATotalTimes;
            result.ProstrateSleepAHTotalCount = result.ProstrateSleepOA + result.ProstrateSleepCA + result.ProstrateSleepMA + result.ProstrateSleepHYP;
            result.ProstrateSleepAHTotalTimes = result.ProstrateSleepCATotalTimes + result.ProstrateSleepMATotalTimes + result.ProstrateSleepOATotalTimes + result.ProstrateSleepHYPTotalTimes;
            result.ProstrateSleepAHTotalTimesMinutes= (float)Math.Round(result.ProstrateSleepAHTotalTimes / 60, 2);

            result.UnSitSleepOA = result.LateralSleepOA + result.ProstrateSleepOA + result.UpSleepOA;
            result.UnSitSleepCA = result.LateralSleepCA + result.ProstrateSleepCA + result.UpSleepCA;
            result.UnSitSleepMA = result.LateralSleepMA + result.ProstrateSleepMA + result.UpSleepMA;
            result.UnSitSleepHYP = result.LateralSleepHYP + result.ProstrateSleepHYP + result.UpSleepHYP;
            result.UnSitSleepATotalCount = result.UnSitSleepOA + result.UnSitSleepCA + result.UnSitSleepMA;
            result.UnSitSleepOATotalTimes = result.LateralSleepOATotalTimes + result.ProstrateSleepOATotalTimes + result.UpSleepOATotalTimes;
            result.UnSitSleepCATotalTimes = result.LateralSleepCATotalTimes + result.ProstrateSleepCATotalTimes + result.UpSleepCATotalTimes;
            result.UnSitSleepMATotalTimes = result.LateralSleepMATotalTimes + result.ProstrateSleepMATotalTimes + result.UpSleepMATotalTimes;
            result.UnSitSleepHYPTotalTimes = result.LateralSleepHYPTotalTimes + result.ProstrateSleepHYPTotalTimes + result.UpSleepHYPTotalTimes;
            result.UnSitSleepATotalTimes = result.LateralSleepATotalTimes + result.ProstrateSleepATotalTimes + result.UpSleepATotalTimes;
            result.UnSitSleepAHTotalCount = result.LateralSleepAHTotalCount + result.ProstrateSleepAHTotalCount + result.UpSleepAHTotalCount;
            result.UnSitSleepAHTotalTimes = result.LateralSleepAHTotalTimes + result.UpSleepAHTotalTimes + result.ProstrateSleepAHTotalTimes;

            foreach (DataEpoch one in m_DataSource.TIBEpochs)
            {
                if (one.Spo2LevelTimes.Length > 0)
                {
                    if (one.EnumPos == BodyPosEnum.L || one.EnumPos == BodyPosEnum.R)
                    {
                        result.LateralSleepSpo2Than100TotalTimes += one.Spo2LevelTimes[0];
                        result.LateralSleepSpo2Than90TotalTimes += one.Spo2LevelTimes[1];
                        //result.TotalBloodOxygenLowThan85Times += one.Spo2LevelTimes[2];
                        result.LateralSleepSpo2Than80TotalTimes += one.Spo2LevelTimes[3];
                        result.LateralSleepSpo2Than70TotalTimes += one.Spo2LevelTimes[4];
                        result.LateralSleepSpo2Than60TotalTimes += one.Spo2LevelTimes[5];
                        result.LateralSleepSpo2Than50TotalTimes += one.Spo2LevelTimes[6];
                        result.LateralSleepSpo2Than40TotalTimes += one.Spo2LevelTimes[7];
                    }
                    else if (one.EnumPos == BodyPosEnum.P)
                    {
                        result.ProstrateSleepSpo2Than100TotalTimes += one.Spo2LevelTimes[0];
                        result.ProstrateSleepSpo2Than90TotalTimes += one.Spo2LevelTimes[1];
                        //result.TotalBloodOxygenLowThan85Times += one.Spo2LevelTimes[2];
                        result.ProstrateSleepSpo2Than80TotalTimes += one.Spo2LevelTimes[3];
                        result.ProstrateSleepSpo2Than70TotalTimes += one.Spo2LevelTimes[4];
                        result.ProstrateSleepSpo2Than60TotalTimes += one.Spo2LevelTimes[5];
                        result.ProstrateSleepSpo2Than50TotalTimes += one.Spo2LevelTimes[6];
                        result.ProstrateSleepSpo2Than40TotalTimes += one.Spo2LevelTimes[7];
                    }
                    else if (one.EnumPos == BodyPosEnum.S)
                    {
                        result.SitSleepSpo2Than100TotalTimes += one.Spo2LevelTimes[0];
                        result.SitSleepSpo2Than90TotalTimes += one.Spo2LevelTimes[1];
                        //result.TotalBloodOxygenLowThan85Times += one.Spo2LevelTimes[2];
                        result.SitSleepSpo2Than80TotalTimes += one.Spo2LevelTimes[3];
                        result.SitSleepSpo2Than70TotalTimes += one.Spo2LevelTimes[4];
                        result.SitSleepSpo2Than60TotalTimes += one.Spo2LevelTimes[5];
                        result.SitSleepSpo2Than50TotalTimes += one.Spo2LevelTimes[6];
                        if (one.Spo2LevelTimes[7] > 0)
                        {
                            Console.WriteLine(one.Index);
                        }
                        result.SitSleepSpo2Than40TotalTimes += one.Spo2LevelTimes[7];
                    }
                }
            }
            float minspo2_1 = 100, minspo2_2 = 100, minspo2_3 = 100;
            float totalSpo2Reduce_Sit_R = 0, totalSpo2Reduce_Sit_UnR = 0, totalSpo2Reduce_UnSit_R = 0, totalSpo2Reduce_UnSit_UnR = 0, totalSpo2Reduce_TST = 0;
            int countSpo2Reduce_Sit_R = 0, countSpo2Reduce_Sit_UnR = 0, countSpo2Reduce_UnSit_R = 0, countSpo2Reduce_UnSit_UnR = 0, countSpo2Reduce_TST = 0;
            result.AveSpo2Reduce_Sit_R = result.AveSpo2Reduce_Sit_UnR = result.AveSpo2Reduce_UnSit_R = result.AveSpo2Reduce_UnSit_UnR = result.AveSpo2Reduce_TST = 0;
            foreach (DataEvent item in this.m_DataSource.SpO2Events)
            {
                if (item.EventTyp == EventTypeEnum.Spo2Artifact)
                    continue;
                float Spo2ReduceValue = Convert.ToSingle(item.Comments);
                totalSpo2Reduce_TST += Spo2ReduceValue;
                countSpo2Reduce_TST++;
                if (item.OnPos.Contains((int)BodyPosEnum.S))
                {
                    if (!item.OnStages.Contains((int)SleepStageEnum.R))
                    {
                        totalSpo2Reduce_Sit_UnR += Spo2ReduceValue;
                        countSpo2Reduce_Sit_UnR++;
                    }
                    if (item.OnStages.Contains((int)SleepStageEnum.R))
                    {
                        totalSpo2Reduce_Sit_R += Spo2ReduceValue;
                        countSpo2Reduce_Sit_R++;
                    }
                }
                if (!item.OnPos.Contains((int)BodyPosEnum.S))
                {
                    if (!item.OnStages.Contains((int)SleepStageEnum.R))
                    {
                        totalSpo2Reduce_UnSit_UnR += Spo2ReduceValue;
                        countSpo2Reduce_UnSit_UnR++;
                    }
                    if (item.OnStages.Contains((int)SleepStageEnum.R))
                    {
                        totalSpo2Reduce_UnSit_R += Spo2ReduceValue;
                        countSpo2Reduce_UnSit_R++;
                    }
                }
                float minSpo2Value = Convert.ToSingle(item.Tag);
                bool D1 = false, D2 = false, D3 = false, D4 = false, D5 = false, D6 = false, D7 = false;
                if (minSpo2Value >= 90 && minSpo2Value < 98)
                    D1 = true;
                else if (minSpo2Value >= 80 && minSpo2Value < 90)
                    D2 = true;
                else if (minSpo2Value >= 70 && minSpo2Value < 80)
                    D3 = true;
                else if (minSpo2Value >= 60 && minSpo2Value < 70)
                    D4 = true;
                else if (minSpo2Value >= 50 && minSpo2Value < 60)
                    D5 = true;
                else if (minSpo2Value >= 40 && minSpo2Value < 50)
                    D6 = true;
                else
                {
                    D7 = true;
                }
                if (item.OnPos.Contains((int)BodyPosEnum.UP))
                {
                    result.UpSleepDesat += 1f;
                }
                bool IsLateral = false;
                if (item.OnPos.Contains((int)BodyPosEnum.L))
                {
                    IsLateral = true;
                    result.LeftSleepDesat += 1f;
                }
                if (item.OnPos.Contains((int)BodyPosEnum.R))
                {
                    IsLateral = true;
                    result.RightSleepDesat += 1f;
                }
                if (IsLateral)
                {
                    if (minspo2_1 > minSpo2Value)
                        minspo2_1 = minSpo2Value;
                    if (D1)
                    {
                        result.LateralSleepDesatThan100 += 1f;
                        result.LateralSleepDesatThan100TotalTimes += item.Duration;
                    }
                    else if (D2)
                    {
                        result.LateralSleepDesatThan90 += 1f;
                        result.LateralSleepDesatThan90TotalTimes += item.Duration;
                    }
                    else if (D3)
                    {
                        result.LateralSleepDesatThan80 += 1f;
                        result.LateralSleepDesatThan80TotalTimes += item.Duration;
                    }
                    else if (D4)
                    {
                        result.LateralSleepDesatThan70 += 1f;
                        result.LateralSleepDesatThan70TotalTimes += item.Duration;
                    }
                    else if (D5)
                    {
                        result.LateralSleepDesatThan60 += 1f;
                        result.LateralSleepDesatThan60TotalTimes += item.Duration;
                    }
                    else if (D6)
                    {
                        result.LateralSleepDesatThan50 += 1f;
                        result.LateralSleepDesatThan50TotalTimes += item.Duration;
                    }
                    else if (D7)
                    {
                        result.LateralSleepDesatThan40 += 1f;
                        result.LateralSleepDesatThan40TotalTimes += item.Duration;
                    }
                }
                if (item.OnPos.Contains((int)BodyPosEnum.P))
                {
                    if (minspo2_2 > minSpo2Value)
                        minspo2_2 = minSpo2Value;
                    result.ProstrateSleepDesat += 1f;
                    if (D1)
                    {
                        result.ProstrateSleepDesatThan100 += 1f;
                        result.ProstrateSleepDesatThan100TotalTimes += item.Duration;
                    }
                    else if (D2)
                    {
                        result.ProstrateSleepDesatThan90 += 1f;
                        result.ProstrateSleepDesatThan90TotalTimes += item.Duration;
                    }
                    else if (D3)
                    {
                        result.ProstrateSleepDesatThan80 += 1f;
                        result.ProstrateSleepDesatThan80TotalTimes += item.Duration;
                    }
                    else if (D4)
                    {
                        result.ProstrateSleepDesatThan70 += 1f;
                        result.ProstrateSleepDesatThan70TotalTimes += item.Duration;
                    }
                    else if (D5)
                    {
                        result.ProstrateSleepDesatThan60 += 1f;
                        result.ProstrateSleepDesatThan60TotalTimes += item.Duration;
                    }
                    else if (D6)
                    {
                        result.ProstrateSleepDesatThan50 += 1f;
                        result.ProstrateSleepDesatThan50TotalTimes += item.Duration;
                    }
                    else if (D7)
                    {
                        result.ProstrateSleepDesatThan40 += 1f;
                        result.ProstrateSleepDesatThan40TotalTimes += item.Duration;
                    }
                }
                if (item.OnPos.Contains((int)BodyPosEnum.S))
                {
                    if (minspo2_3 > minSpo2Value)
                        minspo2_3 = minSpo2Value;
                    result.SitSleepDesat += 1f;
                    if (D1)
                    {
                        result.SitSleepDesatThan100 += 1f;
                        result.SitSleepDesatThan100TotalTimes += item.Duration;
                    }
                    else if (D2)
                    {
                        result.SitSleepDesatThan90 += 1f;
                        result.SitSleepDesatThan90TotalTimes += item.Duration;
                    }
                    else if (D3)
                    {
                        result.SitSleepDesatThan80 += 1f;
                        result.SitSleepDesatThan80TotalTimes += item.Duration;
                    }
                    else if (D4)
                    {
                        result.SitSleepDesatThan70 += 1f;
                        result.SitSleepDesatThan70TotalTimes += item.Duration;
                    }
                    else if (D5)
                    {
                        result.SitSleepDesatThan60 += 1f;
                        result.SitSleepDesatThan60TotalTimes += item.Duration;
                    }
                    else if (D6)
                    {
                        result.SitSleepDesatThan50 += 1f;
                        result.SitSleepDesatThan50TotalTimes += item.Duration;
                    }
                    else if (D7)
                    {
                        result.SitSleepDesatThan40 += 1f;
                        result.SitSleepDesatThan40TotalTimes += item.Duration;
                    }
                }
            }
            ///定义出错 UP定义成了Sit，为兼容之前的模板主动幅值
            result.UpSleepDesatThan100 = result.SitSleepDesatThan100;
            result.UpSleepDesatThan90 = result.SitSleepDesatThan90;
            result.UpSleepDesatThan80 = result.SitSleepDesatThan80;
            result.UpSleepDesatThan70 = result.SitSleepDesatThan70;
            result.UpSleepDesatThan60 = result.SitSleepDesatThan60;
            result.UpSleepDesatThan50 = result.SitSleepDesatThan50;
            result.UpSleepDesatThan40 = result.SitSleepDesatThan40;

            result.AveSpo2Reduce_Sit_R = (float)Math.Round(countSpo2Reduce_Sit_R > 0 ? totalSpo2Reduce_Sit_R / countSpo2Reduce_Sit_R : 0, 2);
            result.AveSpo2Reduce_Sit_UnR = (float)Math.Round(countSpo2Reduce_Sit_UnR > 0 ? totalSpo2Reduce_Sit_UnR / countSpo2Reduce_Sit_UnR : 0, 2);
            result.AveSpo2Reduce_UnSit_R = (float)Math.Round(countSpo2Reduce_UnSit_R > 0 ? totalSpo2Reduce_UnSit_R / countSpo2Reduce_UnSit_R : 0, 2);
            result.AveSpo2Reduce_UnSit_UnR = (float)Math.Round(countSpo2Reduce_UnSit_UnR > 0 ? totalSpo2Reduce_UnSit_UnR / countSpo2Reduce_UnSit_UnR : 0, 2);
            result.AveSpo2Reduce_TST = (float)Math.Round(countSpo2Reduce_TST > 0 ? totalSpo2Reduce_TST / countSpo2Reduce_TST : 0, 2);

            #region snore 鼾声相关

            result.SnoreTotalCount = m_DataSource.SnoreEvents.Count;
            result.SnoreIndex = m_DataSource.SnoreEvents.Count > 0 ? (float)Math.Round(result.SnoreTotalCount * 60 / m_DataSource.TotalSleepTime, 2) : 0;
            result.SitSleepSnoreCount = result.LeftSleepSnoreCount = result.RightSleepSnoreCount = result.ProstrateSleepSnoreCount = result.UpSleepSnoreCount = result.UnSitSleepSnoreCount = result.LateralSleepSnoreCount = 0;
            result.SitSleepSnoreIndex = result.LeftSleepSnoreIndex = result.RightSleepSnoreIndex = result.ProstrateSleepSnoreIndex = result.UpSleepSnoreIndex = result.UnSitSleepSnoreIndex = result.LateralSleepSnoreIndex = 0;
            foreach (DataEvent dataEvent in m_DataSource.SnoreEvents)
            {
                DataEvent item = dataEvent;
                for (int i = 0; i < item.OnPos.Length; i++)
                {
                    switch (item.OnPos[i])
                    {
                        case (int)BodyPosEnum.S:
                            result.SitSleepSnoreCount++;
                            break;
                        case (int)BodyPosEnum.L:
                            result.LeftSleepSnoreCount++;
                            result.LateralSleepSnoreCount++;
                            result.UnSitSleepSnoreCount++;
                            break;
                        case (int)BodyPosEnum.R:
                            result.RightSleepSnoreIndex++;
                            result.LateralSleepSnoreCount++;
                            result.UnSitSleepSnoreCount++;
                            break;
                        case (int)BodyPosEnum.P:
                            result.ProstrateSleepSnoreCount++;
                            result.UnSitSleepSnoreCount++;
                            break;
                        case (int)BodyPosEnum.UP:
                            result.UpSleepSnoreCount++;
                            result.UnSitSleepSnoreCount++;
                            break;
                        default:
                            break;
                    }
                }
            }

            #endregion


            if (result.SitTotalSleepTimeDuration > 0f)
            {
                result.SitTotalSleepTimeOfTST = (float)Math.Round((double)(result.SitTotalSleepTimeDuration / m_DataSource.TotalSleepTime), 4) * 100;
                result.SitSleepSnoreIndex = (float)Math.Round((double)(result.SitSleepSnoreCount * 60f / result.SitTotalSleepTimeDuration), 2);
                result.SitSleepOAIndex = (float)Math.Round((double)(result.SitSleepOA * 60f / result.SitTotalSleepTimeDuration), 2);
                result.SitSleepMAIndex = (float)Math.Round((double)(result.SitSleepMA * 60f / result.SitTotalSleepTimeDuration), 2);
                result.SitSleepCAIndex = (float)Math.Round((double)(result.SitSleepCA * 60f / result.SitTotalSleepTimeDuration), 2);
                result.SitSleepAIndex = (float)Math.Round((double)(result.SitSleepATotalCount * 60f / result.SitTotalSleepTimeDuration), 2);
                result.SitSleepHYPIndex = (float)Math.Round((double)(result.SitSleepHYP * 60f / result.SitTotalSleepTimeDuration), 2);
                result.SitSleepAHI = (float)Math.Round((double)(result.SitSleepAHTotalCount * 60f / result.SitTotalSleepTimeDuration), 2);
                result.SitSleepRDI = (float)Math.Round((double)((result.SitSleepAHTotalCount + result.SitSleepRERA) * 60f / result.SitTotalSleepTimeDuration), 2);
                result.SitSleepDesatIndex = (float)Math.Round((double)(result.SitSleepDesat * 60f / result.SitTotalSleepTimeDuration), 2);
            }
            if (result.LeftTotalSleepTimeDuration > 0f)
            {
                result.LeftTotalSleepTimeOfTST = (float)Math.Round((double)(result.LeftTotalSleepTimeDuration / m_DataSource.TotalSleepTime), 4) * 100;
                result.LeftSleepSnoreIndex = (float)Math.Round((double)(result.LeftSleepSnoreCount * 60f / result.LeftTotalSleepTimeDuration), 2);
                result.LeftSleepOAIndex = (float)Math.Round((double)(result.LeftSleepOA * 60f / result.LeftTotalSleepTimeDuration), 2);
                result.LeftSleepMAIndex = (float)Math.Round((double)(result.LeftSleepMA * 60f / result.LeftTotalSleepTimeDuration), 2);
                result.LeftSleepCAIndex = (float)Math.Round((double)(result.LeftSleepCA * 60f / result.LeftTotalSleepTimeDuration), 2);
                result.LeftSleepAIndex = (float)Math.Round((double)(result.LeftSleepATotalCount * 60f / result.SitTotalSleepTimeDuration), 2);
                result.LeftSleepHYPIndex = (float)Math.Round((double)(result.LeftSleepHYP * 60f / result.LeftTotalSleepTimeDuration), 2);
                result.LeftSleepAHI = (float)Math.Round((double)(result.LeftSleepAHTotalCount * 60f / result.LeftTotalSleepTimeDuration), 2);
                result.LeftSleepRDI = (float)Math.Round((double)((result.LeftSleepAHTotalCount + result.LeftSleepRERA) * 60f / result.LeftTotalSleepTimeDuration), 2);
                result.LeftSleepDesatIndex = (float)Math.Round((double)(result.LeftSleepDesat * 60f / result.LeftTotalSleepTimeDuration), 2);
            }
            if (result.RightTotalSleepTimeDuration > 0f)
            {
                result.RightTotalSleepTimeOfTST = (float)Math.Round((double)(result.RightTotalSleepTimeDuration / m_DataSource.TotalSleepTime), 4) * 100;
                result.RightSleepSnoreIndex = (float)Math.Round((double)(result.RightSleepSnoreCount * 60f / result.RightTotalSleepTimeDuration), 2);
                result.RightSleepOAIndex = (float)Math.Round((double)(result.RightSleepOA * 60f / result.RightTotalSleepTimeDuration), 2);
                result.RightSleepMAIndex = (float)Math.Round((double)(result.RightSleepMA * 60f / result.RightTotalSleepTimeDuration), 2);
                result.RightSleepCAIndex = (float)Math.Round((double)(result.RightSleepCA * 60f / result.RightTotalSleepTimeDuration), 2);
                result.RightSleepAIndex = (float)Math.Round((double)(result.RightSleepATotalCount * 60f / result.RightTotalSleepTimeDuration), 2);
                result.RightSleepHYPIndex = (float)Math.Round((double)(result.RightSleepHYP * 60f / result.RightTotalSleepTimeDuration), 2);
                result.RightSleepAHI = (float)Math.Round((double)(result.RightSleepAHTotalCount * 60f / result.RightTotalSleepTimeDuration), 2);
                result.RightSleepRDI = (float)Math.Round((double)((result.RightSleepAHTotalCount + result.RightSleepRERA) * 60f / result.RightTotalSleepTimeDuration), 2);
                result.RightSleepDesatIndex = (float)Math.Round((double)(result.RightSleepDesat * 60f / result.RightTotalSleepTimeDuration), 2);
            }
            if (result.ProstrateTotalSleepTimeDuration > 0f)
            {
                result.ProstrateTotalSleepTimeOfTST = (float)Math.Round((double)(result.ProstrateTotalSleepTimeDuration / m_DataSource.TotalSleepTime), 4) * 100;
                result.ProstrateSleepSnoreIndex = (float)Math.Round((double)(result.ProstrateSleepSnoreCount * 60f / result.ProstrateTotalSleepTimeDuration), 2);
                result.ProstrateSleepOAIndex = (float)Math.Round((double)(result.ProstrateSleepOA * 60f / result.ProstrateTotalSleepTimeDuration), 2);
                result.ProstrateSleepMAIndex = (float)Math.Round((double)(result.ProstrateSleepMA * 60f / result.ProstrateTotalSleepTimeDuration), 2);
                result.ProstrateSleepCAIndex = (float)Math.Round((double)(result.ProstrateSleepCA * 60f / result.ProstrateTotalSleepTimeDuration), 2);
                result.ProstrateSleepAIndex = (float)Math.Round((double)(result.ProstrateSleepATotalCount * 60f / result.ProstrateTotalSleepTimeDuration), 2);
                result.ProstrateSleepHYPIndex = (float)Math.Round((double)(result.ProstrateSleepHYP * 60f / result.ProstrateTotalSleepTimeDuration), 2);
                result.ProstrateSleepAHI = (float)Math.Round((double)(result.ProstrateSleepAHTotalCount * 60f / result.ProstrateTotalSleepTimeDuration), 2);
                result.ProstrateSleepRDI = (float)Math.Round((double)((result.ProstrateSleepAHTotalCount + result.ProstrateSleepRERA) * 60f / result.ProstrateTotalSleepTimeDuration), 2);
                result.ProstrateSleepDesatIndex = (float)Math.Round((double)(result.ProstrateSleepDesat * 60f / result.ProstrateTotalSleepTimeDuration), 2);
            }
            if (result.UpTotalSleepTimeDuration > 0f)
            {
                result.UpTotalSleepTimeOfTST = (float)Math.Round((double)(result.UpTotalSleepTimeDuration / m_DataSource.TotalSleepTime), 4) * 100;
                result.UpSleepSnoreIndex = (float)Math.Round((double)(result.UpSleepSnoreCount * 60f / result.UpTotalSleepTimeDuration), 2);
                result.UpSleepOAIndex = (float)Math.Round((double)(result.UpSleepOA * 60f / result.UpTotalSleepTimeDuration), 2);
                result.UpSleepMAIndex = (float)Math.Round((double)(result.UpSleepMA * 60f / result.UpTotalSleepTimeDuration), 2);
                result.UpSleepCAIndex = (float)Math.Round((double)(result.UpSleepCA * 60f / result.UpTotalSleepTimeDuration), 2);
                result.UpSleepAIndex = (float)Math.Round((double)(result.UpSleepATotalCount * 60f / result.UpTotalSleepTimeDuration), 2);
                result.UpSleepHYPIndex = (float)Math.Round((double)(result.UpSleepHYP * 60f / result.UpTotalSleepTimeDuration), 2);
                result.UpSleepAHI = (float)Math.Round((double)(result.UpSleepAHTotalCount * 60f / result.UpTotalSleepTimeDuration), 2);
                result.UpSleepRDI = (float)Math.Round((double)((result.UpSleepAHTotalCount + result.UpSleepRERA) * 60f / result.UpTotalSleepTimeDuration), 2);
                result.UpSleepDesatIndex = (float)Math.Round((double)(result.UpSleepDesat * 60f / result.UpTotalSleepTimeDuration), 2);
            }
            if (result.LeftTotalSleepTimeDuration > 0f || result.RightTotalSleepTimeDuration > 0f)
            {
                result.LateralTotalSleepTimeOfTST = (float)Math.Round((double)((result.LeftTotalSleepTimeDuration + result.RightTotalSleepTimeDuration) / m_DataSource.TotalSleepTime), 4) * 100;
                result.LateralTotalSleepTimeDuration = result.LeftTotalSleepTimeDuration + result.RightTotalSleepTimeDuration;
                result.LateralSleepRERA = result.LeftSleepRERA + result.RightSleepRERA;
                result.LateralSleepDesat = result.LeftSleepDesat + result.RightSleepDesat;
                result.LateralSleepSnoreIndex = (float)Math.Round((double)(result.LateralSleepSnoreCount * 60f / result.LateralTotalSleepTimeDuration), 2);
                result.LateralSleepOAIndex = (float)Math.Round((double)(result.LateralSleepOA * 60f / result.LateralTotalSleepTimeDuration), 2);
                result.LateralSleepMAIndex = (float)Math.Round((double)(result.LateralSleepMA * 60f / result.LateralTotalSleepTimeDuration), 2);
                result.LateralSleepCAIndex = (float)Math.Round((double)(result.LateralSleepCA * 60f / result.LateralTotalSleepTimeDuration), 2);
                result.LateralSleepAIndex = (float)Math.Round((double)(result.LateralSleepATotalCount * 60f / result.LateralTotalSleepTimeDuration), 2);
                result.LateralSleepHYPIndex = (float)Math.Round((double)(result.LateralSleepHYP * 60f / result.LateralTotalSleepTimeDuration), 2);
                result.LateralSleepAHI = result.LeftSleepAHI + result.RightSleepAHI;
                result.LateralSleepRDI = result.LeftSleepRDI + result.RightSleepRDI;
                result.LateralSleepDesatIndex = result.LeftSleepDesatIndex + result.RightSleepDesatIndex;
            }
            if (result.LeftTotalSleepTimeDuration > 0f || result.RightTotalSleepTimeDuration > 0f || result.ProstrateTotalSleepTimeDuration > 0f || result.UpTotalSleepTimeDuration > 0f)
            {
                result.UnSitTotalSleepTimeOfTST = (float)Math.Round((double)((result.LeftTotalSleepTimeDuration + result.RightTotalSleepTimeDuration + result.ProstrateTotalSleepTimeDuration + result.UpTotalSleepTimeDuration) / m_DataSource.TotalSleepTime), 4) * 100; 
                result.UnSitTotalSleepTimeDuration = result.LeftTotalSleepTimeDuration + result.RightTotalSleepTimeDuration + result.ProstrateTotalSleepTimeDuration + result.UpTotalSleepTimeDuration;
                result.UnSitSleepSnoreIndex = (float)Math.Round((double)(result.UnSitSleepSnoreCount * 60f / result.UnSitTotalSleepTimeDuration), 2);
                result.UnSitSleepRERA = result.LeftSleepRERA + result.RightSleepRERA + result.ProstrateSleepRERA + result.UpSleepRERA;
                result.UnSitSleepOAIndex = result.LeftSleepOAIndex + result.RightSleepOAIndex + result.ProstrateSleepOAIndex + result.UpSleepOAIndex;
                result.UnSitSleepMAIndex = result.LeftSleepMAIndex + result.RightSleepMAIndex + result.ProstrateSleepMAIndex + result.UpSleepMAIndex;
                result.UnSitSleepCAIndex = result.LeftSleepCAIndex + result.RightSleepCAIndex + result.ProstrateSleepCAIndex + result.UpSleepCAIndex;
                result.UnSitSleepAIndex = result.LeftSleepAIndex + result.RightSleepAIndex + result.ProstrateSleepAIndex + result.UpSleepAIndex;
                result.UnSitSleepHYPIndex = result.LeftSleepHYPIndex + result.RightSleepHYPIndex + result.ProstrateSleepHYPIndex + result.UpSleepHYPIndex;
                result.UnSitSleepAHI = result.LeftSleepAHI + result.RightSleepAHI + result.ProstrateSleepAHI + result.UpSleepAHI;
                result.UnSitSleepRDI = result.LeftSleepRDI + result.RightSleepRDI + result.ProstrateSleepRDI + result.UpSleepRDI;
                result.UnSitSleepDesat = result.LeftSleepDesat + result.RightSleepDesat + result.ProstrateSleepDesat + result.UpSleepDesat;
                result.UnSitSleepDesatIndex = result.LeftSleepDesatIndex + result.RightSleepDesatIndex + result.ProstrateSleepDesatIndex + result.UpSleepDesatIndex;
            }
            if (minspo2_1 < 100 && minspo2_1 > 0)
                result.MinSpo2ValueOnLateral = minspo2_1;
            if (minspo2_2 < 100 && minspo2_2 > 0)
                result.MinSpo2ValueOnProstrate = minspo2_2;
            if (minspo2_3 < 100 && minspo2_3 > 0)
                result.MinSpo2ValueOnUp = minspo2_3;

            return result;
        }
    }
}
