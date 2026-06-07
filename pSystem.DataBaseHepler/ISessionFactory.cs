using System.Data;

namespace pSystem.DataBaseHepler
{
    /// <summary>
    /// 会话工厂
    /// </summary>
    public interface ISessionFactory
    {
        /// <summary>
        /// 创建数据库连接
        /// </summary>
        IDbConnection CreateConnection();
        /// <summary>
        /// 创建数据库连接会话
        /// </summary>
        IDbSession CreateSession();
        /// <summary>
        /// 创建数据库事务会话
        /// </summary>
        IDbSession CreateSession(IDbConnection conn, IDbTransaction trans);
    }
}
