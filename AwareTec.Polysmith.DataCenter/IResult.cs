using pSystem.Interface.Util;
namespace AwareTec.Polysmith.DataCenter
{
    /// <summary>
    /// 结果分析类接口
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// 获取结果
        /// </summary>
        /// <returns></returns>
        ITable getResult();
    }
}
