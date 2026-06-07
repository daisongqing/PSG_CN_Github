using AwareTec.Polysmith.DataBaseCom;
using AwareTec.Polysmith.DataMangement;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.UI.DataModel
{
    public class DataManger
    {
        private DataFactory m_DataFactory = null;
        private int m_TotalFrameCnt = 0;
        /// <summary>
        /// 初始化完成标志
        /// </summary>
        private bool init = false;
        private bool isFirst = false;
        public DataManger()
        {
            m_DataFactory = new DataFactory();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="guid"></param>
        /// <param name="TotalFrameCnt"></param>
        public void Initialize(string basePath, string guid, int TotalFrameCnt)
        {
            m_TotalFrameCnt = TotalFrameCnt;
            m_DataFactory.Initialize(basePath, guid, TotalFrameCnt);
        }
        /// <summary>
        /// 开启服务
        /// </summary>
        /// <returns></returns>
        public bool StartServer()
        {
            m_DataFactory.EpochsInstance.Start();
            m_DataFactory.EventsInstance.Start();
            return true;
        }
        /// <summary>
        /// 另存一份文件
        /// </summary>
        /// <param name="SaveAsPath"></param>
        public bool SaveCopyAs()
        {
            DataModel.LogInstance.Default.AddLog(string.Format("文件备份的地址为 {0}\\Location\\{1}", m_DataFactory.BasePath, m_DataFactory.EventsInstance.GUID), pSystem.LogManagement.LogLevel.DEBUG);
            if (m_DataFactory.EpochsInstance.SaveCopyAs() && m_DataFactory.EventsInstance.SaveCopyAs())
            {
                DataModel.LogInstance.Default.AddLog("文件备份 成功", pSystem.LogManagement.LogLevel.WARN);
                return true;
            }
            else
            {
                DataModel.LogInstance.Default.AddLog("文件备份 失败",pSystem.LogManagement.LogLevel.ERROR);
                return false;
            }
        }
        /// <summary>
        /// 恢复另存为的文件
        /// </summary>
        /// <param name="SaveAsPath"></param>
        public bool RecoverCopyAs()
        {
            int EpochsRevocer = m_DataFactory.EpochsInstance.RecoverCopyAs();
            int EvenetRecover = m_DataFactory.EventsInstance.RecoverCopyAs();
            if (EpochsRevocer == 1 || EvenetRecover == 1)
            {
                DataModel.LogInstance.Default.AddLog("恢复另存为的文件失败 备份文件缺失", pSystem.LogManagement.LogLevel.ERROR);
                return false;
            }
            else if (EpochsRevocer == 2 || EvenetRecover == 2)
            {
                DataModel.LogInstance.Default.AddLog("恢复另存为的文件失败 文件操作失败", pSystem.LogManagement.LogLevel.ERROR);
                return false;
            }
            DataModel.LogInstance.Default.AddLog("恢复另存为的文件成功", pSystem.LogManagement.LogLevel.WARN);
            return true;
        }
        /// <summary>
        /// 获取存储结果
        /// </summary>
        public AnalysisResult Result
        {
            private set;
            get;
        }
        public bool IsFirst
        {
            get
            {
                return isFirst;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public DataFactory dataFactory
        {
            get
            {
                return m_DataFactory;
            }
        }
        /// <summary>
        /// 查询目录路径下是否有备份的文件
        /// </summary>
        /// <returns></returns>
        public bool HasSaveAsFile()
        {
            string[] filenames = Directory.GetFiles(m_DataFactory.FilePath, ".", SearchOption.TopDirectoryOnly);
            foreach (string onefilename in filenames)
            {
                if (onefilename.Contains("Back"))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="result"></param>
        public void BindData(AnalysisResult result,bool isanalysis=false)
        {
            if (result.Epochs.Count > 0)
                m_DataFactory.EpochsInstance.BindData(result.Epochs.Select(t => new EpochsUnit.EpochInfo() { Epoch = t, Index = t.EpochIndex, ByHand = t.ByHand }));
            if (result.EventRecords.Count > 0)
            {
                List<EventRecordsUit.EventRecord> events = new List<EventRecordsUit.EventRecord>();
                for (int i = 0; i < result.EventRecords.Count; i++)
                {
                    Doc_EventRecords t = result.EventRecords[i];
                    //if (t.EventType != (int)pChart.IMarker.MarkType.MultipleSleep&&t.EventType!=(int)pChart.IMarker.MarkType.None)
                    {
                        EventRecordsUit.EventRecord record = new EventRecordsUit.EventRecord()
                        {
                            MarkType = t.EventType,
                            StartIndex = t.StartIndex,
                            EndIndex = t.EndIndex,
                            ByHand = t.ByHand,
                            Comments = (t.EventType == (int)pChart.IMarker.MarkType.None|| t.EventType == (int)pChart.IMarker.MarkType.MultipleSleep) ? t.Tag : t.Comments
                        };
                        events.Add(record);
                    }
                }
                m_DataFactory.EventsInstance.BindData(events, (int)pChart.IMarker.MarkType.MultipleSleep,isanalysis);
            }
        }
        /// <summary>
        /// 保存血氧值
        /// </summary>
        /// <param name="values"></param>
        public void SaveSpo2Values(List<float> values)
        {
            m_DataFactory.Spo2Instance.WriteResult(values);
        }

        /// <summary>
        /// 保存压力峰值
        /// </summary>
        /// <param name="values"></param>
        public void SavePressureValues(List<float> values)
        {
            m_DataFactory.PressureInstance.WriteResult(values);
        }
        /// <summary>
        /// 保存定标值
        /// </summary>
        /// <param name="values"></param>
        public void SaveCalobrationValues(List<float> values)
        {
            m_DataFactory.CalibrationInstance.WriteResult(values);
        }
        /// <summary>
        /// 保存分析结果
        /// </summary>
        /// <param name="result"></param>
        public void SaveDataResult(AnalysisResult result)
        {
            if (result != null)
            {
                Doc_DataResult doc_DataResult = new Doc_DataResult();
                if (result.Sleep != null)
                {
                    doc_DataResult.SleepResult = JsonConvert.SerializeObject(result.Sleep);
                }
                if (result.BloodOxygen != null)
                {
                    doc_DataResult.BloodOxygenResult = JsonConvert.SerializeObject(result.BloodOxygen);
                }
                if (result.BreathEvent != null)
                {
                    doc_DataResult.BreathEventResult = JsonConvert.SerializeObject(result.BreathEvent);
                }
                if (result.BodyState != null)
                {
                    doc_DataResult.BodyStateResult = JsonConvert.SerializeObject(result.BodyState);
                }
                if (result.BodyMovement != null)
                {
                    doc_DataResult.BodyMovementResult = JsonConvert.SerializeObject(result.BodyMovement);
                }
                if (result.HeartRate != null)
                {
                    doc_DataResult.HeartRateResult = JsonConvert.SerializeObject(result.HeartRate);
                }
                doc_DataResult.GUID = result.GUID;
                m_DataFactory.ResultInstance.WriteResult(doc_DataResult);
            }
        }
        /// <summary>
        /// 读取结果
        /// </summary>
        /// <returns></returns>
        public AnalysisResult ReadResult()
        {
            AnalysisResult ret = new AnalysisResult();
            ret.Epochs = m_DataFactory.EpochsInstance.ReadResult();
            ret.EventRecords = m_DataFactory.EventsInstance.ReadResult();
            DataBaseCom.Doc_DataResult result = m_DataFactory.ResultInstance.ReadResult();
            if (result != null)
            {
                ret.Sleep = JsonConvert.DeserializeObject<Doc_SleepResult>(result.SleepResult);
                ret.BloodOxygen = JsonConvert.DeserializeObject<Doc_BloodOxygenResult>(result.BloodOxygenResult);
                ret.BodyMovement = JsonConvert.DeserializeObject<Doc_BodyMovementResult>(result.BodyMovementResult);
                ret.BodyState = JsonConvert.DeserializeObject<Doc_BodyStateResult>(result.BodyStateResult);
                ret.BreathEvent = JsonConvert.DeserializeObject<Doc_BreathEventResult>(result.BreathEventResult);
                ret.HeartRate = JsonConvert.DeserializeObject<Doc_HeartRateReuslt>(result.HeartRateResult);
            }
            return ret;
        }
        /// <summary>
        /// 读取结果
        /// </summary>
        /// <returns></returns>
        public AnalysisResult ReadResult(string path, string guid)
        {
            AnalysisResult ret = new AnalysisResult();
            DataFactory df = new DataFactory();
            df.Initialize(path, guid, 0);
            ret.Epochs = df.EpochsInstance.ReadResult();
            ret.EventRecords = df.EventsInstance.ReadResult();
            DataBaseCom.Doc_DataResult result = df.ResultInstance.ReadResult();
            if (result != null)
            {
                ret.Sleep = JsonConvert.DeserializeObject<Doc_SleepResult>(result.SleepResult);
                ret.BloodOxygen = JsonConvert.DeserializeObject<Doc_BloodOxygenResult>(result.BloodOxygenResult);
                ret.BodyMovement = JsonConvert.DeserializeObject<Doc_BodyMovementResult>(result.BodyMovementResult);
                ret.BodyState = JsonConvert.DeserializeObject<Doc_BodyStateResult>(result.BodyStateResult);
                ret.BreathEvent = JsonConvert.DeserializeObject<Doc_BreathEventResult>(result.BreathEventResult);
                ret.HeartRate = JsonConvert.DeserializeObject<Doc_HeartRateReuslt>(result.HeartRateResult);
            }
            return ret;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="path"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        public bool Delete(string path)
        {
            bool ret = false;
            try
            {
                System.IO.Directory.Delete(path, true);
                ret = true;
            }
            catch { }
            return ret;
        }
        /// <summary>
        /// 执行条件
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="OldID"></param>
        /// <returns></returns>
        public string ConvertEventCondition(DataMangement.UpdateMode mode, string OldID = "")
        {
            return string.Format("{0}|{1}", (int)mode, OldID);
        }

        /// <summary>
        /// 执行条件
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="OldID"></param>
        /// <returns></returns>
        public string ConvertEventCondition(DataMangement.UpdateMode mode, pChart.IMarker.MarkType makeTyp, int oldstartIndex, int oldendIndex)
        {
            return ConvertEventCondition(mode, makeTyp, string.Format(":{0}-{1}", oldstartIndex, oldendIndex));
        }
        /// <summary>
        /// 执行条件
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="OldID"></param>
        /// <returns></returns>
        public string ConvertEventCondition(DataMangement.UpdateMode mode, pChart.IMarker.MarkType makeTyp, string containID)
        {
            return ConvertEventCondition(mode, string.Format("{0}{1}", (int)makeTyp, containID));
        }

    }
}
