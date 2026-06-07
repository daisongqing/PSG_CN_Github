using AwareTec.Polysmith.DataBaseCom;
using System;
using System.Collections.Generic;

namespace AwareTec.Polysmith.DataCenter
{
    /// <summary>
    /// 磨牙事件统计指标类
    /// </summary>
    internal class MolarResult
    {
        private IDataBase m_DataSource = null;
        private List<DataEvent> m_eventRecords = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="datasource"></param>
        public MolarResult(IDataBase datasource)
        {
            m_DataSource = datasource;
            m_eventRecords = m_DataSource.MolarEvents;
        }
        /// <summary>
        /// 获取磨牙统计指标值
        /// </summary>
        /// <returns></returns>
        public Doc_MolarReuslt getResult()
        {
            Doc_MolarReuslt docMolarReuslt = new Doc_MolarReuslt();
            docMolarReuslt.MolarCount = docMolarReuslt.NREMMolarCount = docMolarReuslt.REMMolarCount = docMolarReuslt.SitMolarCount = docMolarReuslt.LateralMolarCount = docMolarReuslt.ProstrateMolarCount = docMolarReuslt.UpMolarCount = 0;
            docMolarReuslt.MolarIndex = docMolarReuslt.NREMMolarIndex = docMolarReuslt.REMMolarIndex = docMolarReuslt.SitMolarIndex = docMolarReuslt.LateralMolarIndex = docMolarReuslt.ProstrateMolarIndex = docMolarReuslt.UpMolarIndex = 0;
            double SitTotalTime = 0;
            double LateralTotalTime = 0;
            double ProstrateTime = 0;
            double UpTotalTime = 0;

            foreach (DataEpoch epoch in m_DataSource.TSTEpochs)
            {
                if (epoch.EnumPos == BodyPosEnum.S)
                    SitTotalTime += 0.5;
                else if (epoch.EnumPos == BodyPosEnum.L || epoch.EnumPos == BodyPosEnum.R)
                    LateralTotalTime += 0.5;
                else if (epoch.EnumPos == BodyPosEnum.P)
                    ProstrateTime += 0.5;
                else if (epoch.EnumPos == BodyPosEnum.UP)
                    UpTotalTime += 0.5;
            }
            bool isRem, isNRem, isWeak, isSit, isLateral, isUp;
            for (int eventindex = 0; eventindex < this.m_eventRecords.Count; ++eventindex)
            {
                DataEvent dataEvent = this.m_eventRecords[eventindex];
                if (dataEvent.StartTime >= this.m_DataSource.TSTStartTime && dataEvent.StartTime < this.m_DataSource.TSTEndTime || dataEvent.EndTime > this.m_DataSource.TSTStartTime && dataEvent.EndTime <= this.m_DataSource.TSTEndTime)
                {
                    docMolarReuslt.MolarCount++;
                    isNRem = isRem = isWeak = false;
                    for (int stageindex = 0; stageindex < dataEvent.OnStages.Length; stageindex++)
                    {
                        switch (dataEvent.OnStages[stageindex])
                        {
                            case (int)SleepStageEnum.R:
                                isRem = true;
                                break;
                            case (int)SleepStageEnum.N1:
                            case (int)SleepStageEnum.N2:
                            case (int)SleepStageEnum.N3:
                                isNRem = true;
                                break;
                            case (int)SleepStageEnum.W:
                                isWeak = true;
                                break;
                        }
                    }
                    if (isRem)
                    {
                        docMolarReuslt.REMMolarCount++;
                    }
                    if(isNRem)
                    {
                        docMolarReuslt.NREMMolarCount++;
                    }
                    if(isWeak)
                    {
                        docMolarReuslt.MolarCount--;
                    }
                }
                isSit = isLateral = isUp = false;
                for (int posindex = 0; posindex < dataEvent.OnPos.Length; posindex++)
                {
                    switch (dataEvent.OnPos[posindex])
                    {
                        case (int)BodyPosEnum.S:
                            isSit = true;
                            break;
                        case (int)BodyPosEnum.L:
                        case (int)BodyPosEnum.R:
                            isLateral = true;
                            break;
                        case (int)BodyPosEnum.UP:
                            isUp = true;
                            break;
                    }
                }
                if (isSit)
                {
                    docMolarReuslt.SitMolarCount++;
                }
                if (isLateral)
                {
                    docMolarReuslt.LateralMolarCount++;
                }
                if (isUp)
                {
                    docMolarReuslt.UpMolarCount++;
                }
            }
            
            if (docMolarReuslt.MolarCount > 0)
                docMolarReuslt.MolarIndex = (float)Math.Round((double)(docMolarReuslt.MolarCount * 60) / (double)this.m_DataSource.TotalSleepTime, 2);
            if (docMolarReuslt.NREMMolarCount > 0)
                docMolarReuslt.NREMMolarIndex = (float)Math.Round((double)(docMolarReuslt.NREMMolarCount * 60) / (double)this.m_DataSource.NRemTotalTimes, 2);
            if (docMolarReuslt.REMMolarCount > 0)
                docMolarReuslt.REMMolarIndex = (float)Math.Round((double)(docMolarReuslt.REMMolarCount * 60) / (double)this.m_DataSource.RemTotalTimes, 2);
            if (docMolarReuslt.SitMolarCount > 0 && SitTotalTime > 0)
                docMolarReuslt.SitMolarIndex = (float)Math.Round((double)(docMolarReuslt.SitMolarCount * 60) / (double)SitTotalTime, 2);
            if (docMolarReuslt.LateralMolarCount > 0 && LateralTotalTime > 0)
                docMolarReuslt.LateralMolarIndex = (float)Math.Round((double)(docMolarReuslt.LateralMolarCount * 60) / (double)LateralTotalTime, 2);
            if (docMolarReuslt.ProstrateMolarCount > 0 && ProstrateTime > 0)
                docMolarReuslt.ProstrateMolarIndex = (float)Math.Round((double)(docMolarReuslt.ProstrateMolarCount * 60) / (double)ProstrateTime, 2);
            if (docMolarReuslt.UpMolarCount > 0 && UpTotalTime > 0)
                docMolarReuslt.UpMolarIndex = (float)Math.Round((double)(docMolarReuslt.UpMolarCount * 60) / (double)UpTotalTime, 2);

            return docMolarReuslt;
        }
    }
}
