using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulWebRequest.EnumModels.EnumModels4Table
{
    public enum EventType
    {
        /// <summary>
        /// 中枢型呼吸暂停
        /// </summary>
        CA = 0,
        /// <summary>
        /// 阻塞型呼吸暂停
        /// </summary>
        OA = 1,
        /// <summary>
        /// 混合型呼吸暂停
        /// </summary>
        MA = 2,
        /// <summary>
        /// 低通气
        /// </summary>
        Hypopnea = 3,
        /// <summary>
        /// 体动
        /// </summary>
        MT = 4,
        /// <summary>
        /// 睡眠
        /// </summary>
        Stage = 5,
        /// <summary>
        /// 氧减
        /// </summary>
        OxygenReduce = 6,
        /// <summary>
        /// 自发性微觉醒
        /// </summary>
        Arousal = 7,
        /// <summary>
        /// 腿动
        /// </summary>
        LegMove = 8,
        /// <summary>
        /// 打鼾
        /// </summary>
        Snore = 9,
        /// <summary>
        /// 不能需要分析
        /// </summary>
        AnlysisEnable = 80,
        /// <summary>
        /// 无
        /// </summary>
        None = 99,
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
        /// 眨眼
        /// </summary>
        Wink = 70,
        /// <summary>
        /// α节律
        /// </summary>
        AlphaRhythm = 71,
        /// <summary>
        /// 纺锤波
        /// </summary>
        Spindles = 72,
        /// <summary>
        /// K复合波
        /// </summary>
        Kcomplex = 73,
        /// <summary>
        /// 开灯事件
        /// </summary>
        LightOn = 100,
        /// <summary>
        /// 关灯事假
        /// </summary>
        LightOff = 101
    }
}
