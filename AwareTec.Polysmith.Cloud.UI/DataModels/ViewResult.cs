using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.DataModels
{
    /// <summary>
    /// 实时显示指标
    /// </summary>
    public class ViewResult
    {
        /// <summary>
        /// 胸部呼吸率
        /// </summary>
        public string ChestBreathingRate = "";
        /// <summary>
        /// 血氧浓度
        /// </summary>
        public string BloodOxygen = "";
        /// <summary>
        /// 压力值
        /// </summary>
        public string Pressure = "";
        /// <summary>
        /// 体位信息
        /// </summary>
        public string BodySate = "";
        /// <summary>
        /// 开关灯状态
        /// </summary>
        public string AmbientLight = "";
        /// <summary>
        /// 脉率
        /// </summary>
        public string PulseRate = "";
        /// <summary>
        /// 腹部呼吸率
        /// </summary>
        public string AbdominalBreathingRate = "";
        /// <summary>
        /// 压力呼吸率
        /// </summary>
        public string PressureBreathingRate = "";
        /// <summary>
        /// 热敏呼吸率
        /// </summary>
        public string ThermalBreathingRate = "";
        /// <summary>
        /// 电池剩余容量
        /// </summary>
        public float BatteryCapacity = 100;

    }
}
