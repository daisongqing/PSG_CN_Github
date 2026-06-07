using pSystem.Interface.Util;
using System;

namespace AwareTec.Polysmith.DataBaseCom
{
    /// <summary>
    /// 磨牙数据统计
    /// </summary>
    public class Doc_MolarReuslt 
    {
        public Doc_MolarReuslt()
        {

        }
        #region 私有成员
        #endregion
        #region 公有成员
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string GUID { set; get; }
        /// <summary>
        /// 磨牙次数tst
        /// </summary>
        public float MolarCount { set; get; }
        /// <summary>
        /// 磨牙指数
        /// </summary>
        public float MolarIndex { set; get; }
        /// <summary>
        /// NREM磨牙次数
        /// </summary>
        public float NREMMolarCount { set; get; }
        /// <summary>
        /// NREM磨牙指数
        /// </summary>
        public float NREMMolarIndex { set; get; }
        /// <summary>
        /// REM磨牙次数
        /// </summary>
        public float REMMolarCount { set; get; }
        /// <summary>
        /// REM磨牙指数
        /// </summary>
        public float REMMolarIndex { set; get; }
        /// <summary>
        /// 仰卧磨牙次数
        /// </summary>
        public float SitMolarCount { set; get; }
        /// <summary>
        /// 仰卧磨牙指数
        /// </summary>
        public float SitMolarIndex { set; get; }
        /// <summary>
        /// 侧卧磨牙次数
        /// </summary>
        public float LateralMolarCount { set; get; }
        /// <summary>
        /// 侧卧磨牙指数
        /// </summary>
        public float LateralMolarIndex { set; get; }
        /// <summary>
        /// 俯卧磨牙次数
        /// </summary>
        public float ProstrateMolarCount { set; get; }
        /// <summary>
        /// 俯卧磨牙指数
        /// </summary>
        public float ProstrateMolarIndex { set; get; }
        /// <summary>
        /// 坐立磨牙次数
        /// </summary>
        public float UpMolarCount { set; get; }
        /// <summary>
        /// 坐立磨牙指数
        /// </summary>
        public float UpMolarIndex { set; get; }
        #endregion

    }
}
