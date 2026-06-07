namespace AwareTec.Polysmith.DataCenter
{
    /// <summary>
    /// 睡眠分期枚举
    /// </summary>
    public enum SleepStageEnum
    {
        /// <summary>
        /// W
        /// </summary>
        W = 5,
        /// <summary>
        /// 1
        /// </summary>
        N1 = 3,
        /// <summary>
        /// 2
        /// </summary>
        N2 = 2,
        /// <summary>
        /// 3
        /// </summary>
        N3 = 1,
        /// <summary>
        /// R
        /// </summary>
        R = 4,
        /// <summary>
        /// 
        /// </summary>
        None = 0
    }
    /// <summary>
    /// 体位信息枚举
    /// </summary>
    public enum BodyPosEnum
    {
        /// <summary>
        /// 坐立
        /// </summary>
        UP = 5,
        /// <summary>
        /// 左侧
        /// </summary>
        L = 2,
        /// <summary>
        /// 右侧
        /// </summary>
        R = 3,
        /// <summary>
        /// 俯卧
        /// </summary>
        P = 4,
        /// <summary>
        /// 仰卧
        /// </summary>
        S = 1,
        /// <summary>
        /// 
        /// </summary>
        None = -1
    }
    /// <summary>
    /// 事件类型定义
    /// </summary>
    public enum EventTypeEnum
    {
        /// <summary>
        /// 中枢型呼吸暂停事件
        /// </summary>
        Resp_CA = 0,
        /// <summary>
        /// 阻塞型呼吸暂停事件
        /// </summary>
        Resp_OA = 1,
        /// <summary>
        /// 混合型呼吸暂停事件
        /// </summary>
        Resp_MA = 2,
        /// <summary>
        /// 低通气呼吸事件
        /// </summary>
        Resp_Hypnea = 3,
        /// <summary>
        /// MT
        /// </summary>
        Pose = 4,
        /// <summary>
        ///睡眠事件
        /// </summary>
        Stage = 5,
        /// <summary>
        /// 氧减事件
        /// </summary>
        SpO2 = 6,
        /// <summary>
        /// 微觉醒事件
        /// </summary>
        Arousal = 7,
        /// <summary>
        /// 腿动事件
        /// </summary>
        Leg = 8,
        /// <summary>
        /// 鼾声
        /// </summary>
        Snore = 9,
        /// <summary>
        /// 腿动事件微觉醒
        /// </summary>
        LmArousal = 12,
        /// <summary>
        /// 周期性腿动相关觉醒
        /// </summary>
        PlmArousal = 13,
        /// <summary>
        /// 呼吸事件相关觉醒
        /// </summary>
        RespArousal = 14,
        /// <summary>
        /// 周期性循环腿动
        /// </summary>
        PeriodicalBodyMove = 11,
        /// <summary>
        /// 陈施氏呼吸事件
        /// </summary>
        CheyneStokes = 20,
        /// <summary>
        /// 血氧伪迹
        /// </summary>
        Spo2Artifact = 21,
        /// <summary>
        /// 多次小睡
        /// </summary>
        MultipleSleep = 22,
        /// <summary>
        /// 磨牙
        /// </summary>
        Molar = 83,
        /// <summary>
        /// 无定义
        /// </summary>
        None = 255
    }
}
