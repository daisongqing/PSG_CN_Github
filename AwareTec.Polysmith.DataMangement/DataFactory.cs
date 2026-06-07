namespace AwareTec.Polysmith.DataMangement
{
    /// <summary>
    /// 更改状态枚举
    /// </summary>
    public enum UpdateMode
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        /// <summary>
        /// 新增
        /// </summary>
        Insert = 1,
        /// <summary>
        /// 编辑
        /// </summary>
        Update = 2,
        /// <summary>
        /// 删除
        /// </summary>
        Delete = 3
    }
    /// <summary>
    /// 数据工厂类
    /// </summary>
    public class DataFactory
    {
        /// <summary>
        /// 结果操作对象
        /// </summary>
        private AnalysisResultUnit m_reusltInstance = null;
        /// <summary>
        /// 帧信息操作对象
        /// </summary>
        private EpochsUnit m_EpochInstance = null;
        /// <summary>
        /// 事件信息操作对象
        /// </summary>
        private EventRecordsUit m_EventsInstance = null;
        /// <summary>
        /// 配置信息操作对象
        /// </summary>
        private AnalysisConfigUnit m_ConfigInstacne = null;
        /// <summary>
        /// 血氧信息操作对象
        /// </summary>
        private Spo2Unit m_Spo2Unit = null;
        /// <summary>
        /// 压力信息操作对象
        /// </summary>
        private PressureUnit m_PressureUnit = null;
        /// <summary>
        /// 生物定标操作对象
        /// </summary>
        private CalibrationUnit m_CalobrationUnit = null;
        /// <summary>
        /// 初始化标志
        /// </summary>
        private bool m_init = false;
        private string m_basePath = "";
        private string m_FilePath = "";
        private string m_keyname = "Location";
        /// <summary>
        /// 构造函数
        /// </summary>
        public DataFactory()
        {
            m_reusltInstance = new AnalysisResultUnit();
            m_EventsInstance = new EventRecordsUit();
            m_EpochInstance = new EpochsUnit();
            m_ConfigInstacne = new AnalysisConfigUnit();
            m_Spo2Unit = new Spo2Unit();
            m_CalobrationUnit = new CalibrationUnit();
            m_PressureUnit = new PressureUnit();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize(string basePath, string guid, int TotalFrameCnt,bool beinData=false)
        {
            m_basePath = basePath;
            m_FilePath = string.Format("{0}\\{1}\\{2}", basePath, m_keyname, guid);
            m_reusltInstance.DataAnResult = m_EventsInstance.DataAnResult = m_EpochInstance.DataAnResult = m_ConfigInstacne.DataAnResult = m_Spo2Unit.DataAnResult = m_CalobrationUnit.DataAnResult = m_PressureUnit.DataAnResult = beinData;
            m_reusltInstance.BasePath = m_EventsInstance.BasePath = m_EpochInstance.BasePath = m_ConfigInstacne.BasePath = m_Spo2Unit.BasePath = m_CalobrationUnit.BasePath = m_PressureUnit.BasePath= basePath;

            m_reusltInstance.GUID = m_EventsInstance.GUID = m_EpochInstance.GUID = m_ConfigInstacne.GUID = m_Spo2Unit.GUID = m_CalobrationUnit.GUID = m_PressureUnit.GUID= guid;

            m_EpochInstance.Initialize(TotalFrameCnt);
            m_init = true;
        }

        public string FilePath
        {
            get => m_FilePath;
            set => m_FilePath = value;
        }
        public string BasePath
        {
            get
            {
                return m_basePath;
            }
        }
        /// <summary>
        /// 获取帧操作对象
        /// </summary>
        public EpochsUnit EpochsInstance
        {
            get
            {
                return m_EpochInstance;
            }
        }
        /// <summary>
        /// 获取事件操作对象
        /// </summary>
        public EventRecordsUit EventsInstance
        {
            get
            {
                return m_EventsInstance;
            }
        }
        /// <summary>
        /// 获取血氧信息操作对象
        /// </summary>
        public Spo2Unit Spo2Instance
        {
            get
            {
                return m_Spo2Unit;
            }
        }
        /// <summary>
        /// 获取血氧信息操作对象
        /// </summary>
        public PressureUnit PressureInstance
        {
            get
            {
                return m_PressureUnit;
            }
        }
        /// <summary>
        /// 获取生物定标操作对象
        /// </summary>
        public CalibrationUnit CalibrationInstance
        {
            get
            {
                return m_CalobrationUnit;
            }
        }
        /// <summary>
        /// 获取结果操作对象
        /// </summary>
        public AnalysisResultUnit ResultInstance
        {
            get
            {
                return m_reusltInstance;
            }
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            m_reusltInstance.Dispose();
            m_EventsInstance.Dispose();
            m_EpochInstance.Dispose();
            m_ConfigInstacne.Dispose();
            m_CalobrationUnit.Dispose();
            m_Spo2Unit.Dispose();
            m_PressureUnit.Dispose();
        }
    }
}
