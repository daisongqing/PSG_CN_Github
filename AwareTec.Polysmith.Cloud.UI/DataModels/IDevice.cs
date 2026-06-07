using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pSystem.Communication.Com;

namespace AwareTec.Polysmith.Cloud.UI.DataModels
{
    /// <summary>
    /// 设备
    /// </summary>
    public interface IDevice
    {
        /// <summary>
        /// 设备名称
        /// </summary>
        string DeviceAddr { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        string MatchKey { get; }
        /// <summary>
        /// 设备名称
        /// </summary>
        string DeviceName { get; set; }
        /// <summary>
        /// 设备状态
        /// </summary>
        DeviceState Status { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        string Description
        {
            get;
        }
        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        IDevice Clone();

        IOClient PortClient { set; get; }
    }

    /// <summary>
    /// 设备状态字
    /// </summary>
    public enum DeviceState
    {
        /// <summary>
        /// 在线
        /// </summary>
        OnLine = 0,
        /// <summary>
        /// 脱机
        /// </summary>
        OffLine = 1,
        /// <summary>
        /// 运行
        /// </summary>
        Running = 2,
        /// <summary>
        /// 设备可用
        /// </summary>
        Enable = 3,
        /// <summary>
        /// 无
        /// </summary>
        None = 99
    }
}
