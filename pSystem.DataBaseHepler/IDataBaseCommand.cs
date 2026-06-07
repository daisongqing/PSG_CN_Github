using pSystem.Interface.Util;
using System.Collections.Generic;
using System.Data;

namespace pSystem.DataBaseHepler
{
    /// <summary>
    /// 数据库操作接口
    /// </summary>
    public interface IDataBaseCommand
    {
        /// <summary>
        /// Select语句
        /// </summary>
        /// <param name="Condition">条件</param>
        /// <returns></returns>
        DataTable Select(ITable Condition);
        /// <summary>
        /// Select语句
        /// </summary>
        /// <param name="Condition">条件</param>
        /// <param name="OrderBy">根据条件进行排序</param>
        /// <returns></returns>
        DataTable Select(ITable Condition, string OrderBy);
        /// <summary>
        /// Select语句 
        /// </summary>
        /// <param name="Condition">条件</param>
        /// <param name="OrderBy">根据条件进行排序</param>
        /// <param name="TopCount">选择前多少条数据</param>
        /// <returns></returns>
        DataTable Select(ITable Condition, string OrderBy, int TopCount);
        /// <summary>
        /// 根据自定义字段进行选择
        /// </summary>
        /// <param name="Command"></param>
        /// <returns></returns>
        DataTable Select(string Command);
        /// <summary>
        /// 查询是否存在此行
        /// </summary>
        /// <param name="Condition">条件</param>
        /// <returns></returns>
        bool Exist(ITable Condition);
        /// <summary>
        /// Delete语句
        /// </summary>
        /// <param name="Condition">条件</param>
        /// <returns></returns>
        bool Delete(ITable Condition);
        /// <summary>
        /// 删除语句
        /// </summary>
        /// <param name="Value">值</param>
        /// <returns></returns>
        bool Delete<T>(List<T> Value) where T : ITable;
        /// <summary>
        /// Insert语句
        /// </summary>
        /// <param name="Value">值</param>
        /// <returns></returns>
        bool Insert(ITable Value);
        /// <summary>
        /// Insert语句
        /// </summary>
        /// <param name="Value">值</param>
        /// <returns></returns>
        bool Insert<T>(List<T> Value) where T : ITable;
        /// <summary>
        /// Update语句
        /// </summary>
        /// <param name="OldValue">原值</param>
        /// <param name="NewValue">新值</param>
        /// <returns></returns>
        bool Update(ITable OldValue, ITable NewValue);
        /// <summary>
        /// Update语句
        /// </summary>
        /// <returns></returns>
        bool Update(Dictionary<ITable, ITable> tables);
        /// <summary>
        /// 删除并复位自增字段
        /// </summary>
        /// <param name="Condition">条件</param>
        /// <returns></returns>
        bool Truncate(ITable Condition);
        /// <summary>
        /// 自定义语句
        /// </summary>
        /// <param name="CommandString">SQL语句</param>
        /// <returns></returns>
        bool Command(string CommandString);
        /// <summary>
        /// 关闭SQL连接
        /// </summary>
        void Close();
        /// <summary>
        /// 获取数据库表名称
        /// </summary>
        /// <returns></returns>
        List<string> GetAllTableName();
    }
}
