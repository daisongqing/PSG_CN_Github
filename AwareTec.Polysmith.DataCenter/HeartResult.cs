using AwareTec.Polysmith.DataBaseCom;
using System;

namespace AwareTec.Polysmith.DataCenter
{
    /// <summary>
    /// 心脏事件统计指标类
    /// </summary>
    internal class HeartResult
    {
        private IDataBase m_DataSource = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="datasource"></param>
        public HeartResult(IDataBase datasource)
        {
            m_DataSource = datasource;
        }
        /// <summary>
        /// 获取心脏统计指标值
        /// </summary>
        /// <returns></returns>
        public Doc_HeartRateReuslt getResult()
        {
            Doc_HeartRateReuslt docHeartRateReuslt = new Doc_HeartRateReuslt();
            docHeartRateReuslt.ApneaAndHypopneaAverageHeartRate = 0;
            int ApneaAndHypopneaCnt = 0;
            int nCnt = 0;
            int num2 = -1;
            int num3 = -1;
            if (this.m_DataSource.TSTEpochs.Count > 0)
            {
                num2 = this.m_DataSource.TSTEpochs[0].Index;
                num3 = this.m_DataSource.TSTEpochs[this.m_DataSource.TSTEpochs.Count - 1].Index;
            }
            int sCnt = 0;
            int TIBCnt = 0;
            int WTIBCnt = 0;
            int RTIBCnt = 0;
            docHeartRateReuslt.MinSleepHeartRate = 999f;
            docHeartRateReuslt.MinSleepHeartRateTIB = 999f;
            docHeartRateReuslt.WeakAverageSleepHeartRateTIB = 0;
            docHeartRateReuslt.WeakMinSleepHeartRateTIB = 999f;
            foreach (DataEpoch dataEpoch in this.m_DataSource.TIBEpochs)
            {
                //呼吸事件相关平均心率
                if (dataEpoch.Hypopnea > 0 || dataEpoch.MA > 0 || dataEpoch.CA > 0 || dataEpoch.OA > 0)
                {
                    docHeartRateReuslt.ApneaAndHypopneaAverageHeartRate += dataEpoch.AveHeartRate;
                    ApneaAndHypopneaCnt++;
                }
                //NREM期间的平均心率
                if (dataEpoch.Index >= num2 && dataEpoch.Index <= num3 && (dataEpoch.EnumStage == SleepStageEnum.N1 || dataEpoch.EnumStage == SleepStageEnum.N2 || dataEpoch.EnumStage == SleepStageEnum.N3) && (double)dataEpoch.AveHeartRate > 0.0)
                {
                    docHeartRateReuslt.NREMAverageHeartRate += dataEpoch.AveHeartRate;
                    ++nCnt;
                }
                
                if (dataEpoch.AveHeartRate > 0)
                {
                    if (dataEpoch.EnumStage == SleepStageEnum.R)
                    {
                        docHeartRateReuslt.REMAverageHeartRate += dataEpoch.AveHeartRate;
                        RTIBCnt++;
                    }

                    if (dataEpoch.EnumStage != SleepStageEnum.W)
                    {
                        docHeartRateReuslt.AverageSleepHeartRate += dataEpoch.AveHeartRate;
                        sCnt++;
                    }
                    //清醒期的相关心率
                    else
                    {
                        docHeartRateReuslt.WeakAverageSleepHeartRateTIB += dataEpoch.AveHeartRate;
                        WTIBCnt++;
                    }
                    docHeartRateReuslt.AverageSleepHeartRateTIB += dataEpoch.AveHeartRate;
                    TIBCnt++;
                }
                if ((double)docHeartRateReuslt.MaxSleepHeartRateTIB < (double)dataEpoch.MaxHeartRate && dataEpoch.MaxHeartRate < 600)
                {
                    docHeartRateReuslt.MaxSleepHeartRateTIB = dataEpoch.MaxHeartRate;
                    docHeartRateReuslt.MaxSleepHeartRateTIBStartTime = m_DataSource.StartRecordTime.AddSeconds(30 * dataEpoch.Index + dataEpoch.MaxHeartRateIndex);
                }
                if ((double)docHeartRateReuslt.WeakMaxSleepHeartRateTIB < (double)dataEpoch.MaxHeartRate && dataEpoch.MaxHeartRate < 600 && dataEpoch.EnumStage == SleepStageEnum.W)
                {
                    docHeartRateReuslt.WeakMaxSleepHeartRateTIB = dataEpoch.MaxHeartRate;
                    docHeartRateReuslt.WeakMaxSleepHeartRateTIBStartTime = m_DataSource.StartRecordTime.AddSeconds(30 * dataEpoch.Index + dataEpoch.MaxHeartRateIndex);
                }
                if ((double)docHeartRateReuslt.MaxSleepHeartRate < (double)dataEpoch.MaxHeartRate && dataEpoch.MaxHeartRate < 600 && dataEpoch.EnumStage != SleepStageEnum.W)
                {
                    docHeartRateReuslt.MaxSleepHeartRate = dataEpoch.MaxHeartRate;
                    docHeartRateReuslt.MaxSleepHeartRateStartTime = m_DataSource.StartRecordTime.AddSeconds(30 * dataEpoch.Index + dataEpoch.MaxHeartRateIndex);
                }
                if ((double)docHeartRateReuslt.MinSleepHeartRate > (double)dataEpoch.MinHeartRate && (double)dataEpoch.MinHeartRate > 0.0)
                {
                    docHeartRateReuslt.MinSleepHeartRate = dataEpoch.MinHeartRate;
                    docHeartRateReuslt.MinSleepHeartRateStartTime = m_DataSource.StartRecordTime.AddSeconds(30 * dataEpoch.Index + dataEpoch.MinHeartRateIndex);
                }
                if ((double)docHeartRateReuslt.WeakMinSleepHeartRateTIB > (double)dataEpoch.MinHeartRate && (double)dataEpoch.MinHeartRate > 0.0 && dataEpoch.EnumStage == SleepStageEnum.W)
                {
                    docHeartRateReuslt.WeakMinSleepHeartRateTIB = dataEpoch.MinHeartRate;
                    docHeartRateReuslt.WeakMinSleepHeartRateTIBStartTime = m_DataSource.StartRecordTime.AddSeconds(30 * dataEpoch.Index + dataEpoch.MinHeartRateIndex);
                }
                if ((double)docHeartRateReuslt.MinSleepHeartRateTIB > (double)dataEpoch.MinHeartRate && (double)dataEpoch.MinHeartRate > 0.0 && dataEpoch.EnumStage != SleepStageEnum.W)
                {
                    docHeartRateReuslt.MinSleepHeartRateTIB = dataEpoch.MinHeartRate;
                    docHeartRateReuslt.MinSleepHeartRateTIBStartTime = m_DataSource.StartRecordTime.AddSeconds(30 * dataEpoch.Index + dataEpoch.MaxHeartRateIndex);
                }
            }
            if ((double)docHeartRateReuslt.MinSleepHeartRate == 999.0)
                docHeartRateReuslt.MinSleepHeartRate = 0.0f;
            if ((double)docHeartRateReuslt.MinSleepHeartRateTIB == 999.0)
                docHeartRateReuslt.MinSleepHeartRateTIB = 0.0f;
            if ((double)docHeartRateReuslt.WeakMinSleepHeartRateTIB == 999.0)
                docHeartRateReuslt.WeakMinSleepHeartRateTIB = 0.0f;
            if (ApneaAndHypopneaCnt > 0)
                docHeartRateReuslt.ApneaAndHypopneaAverageHeartRate = (float)Math.Round((double)docHeartRateReuslt.ApneaAndHypopneaAverageHeartRate / (double)ApneaAndHypopneaCnt, 2);
            if (nCnt > 0)
                docHeartRateReuslt.NREMAverageHeartRate = (float)Math.Round((double)docHeartRateReuslt.NREMAverageHeartRate / (double)nCnt, 2);
            if (RTIBCnt > 0)
                docHeartRateReuslt.REMAverageHeartRate = (float)Math.Round((double)docHeartRateReuslt.REMAverageHeartRate / (double)RTIBCnt, 2);
            if (sCnt > 0)
                docHeartRateReuslt.AverageSleepHeartRate = (float)Math.Round((double)docHeartRateReuslt.AverageSleepHeartRate / sCnt, 2);
            if (TIBCnt > 0)
                docHeartRateReuslt.AverageSleepHeartRateTIB = (float)Math.Round((double)docHeartRateReuslt.AverageSleepHeartRateTIB / TIBCnt, 2);
            if (WTIBCnt > 0)
                docHeartRateReuslt.WeakAverageSleepHeartRateTIB = (float)Math.Round((double)docHeartRateReuslt.WeakAverageSleepHeartRateTIB / WTIBCnt, 2);
            return docHeartRateReuslt;
        }
    }
}
