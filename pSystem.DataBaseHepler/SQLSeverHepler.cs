using Dapper;
using pSystem.Interface.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace pSystem.DataBaseHepler
{
    /// <summary>
    /// SQL数据库操作对象
    /// </summary>
    public class SQLServerHepler : IDataBaseCommand
    {
        private string sqlconstr = "Data Source=.;Initial Catalog=AnalysisResultTemplet;User ID=sa;Password=sa;Persist Security Info=True;";
        //private string sqlconstr = string.Format("Data Source={0}AwareTecSmith.db;Password=123456;", AppDomain.CurrentDomain.BaseDirectory);
        private DataTable DataRead(string cmd)
        {
            try
            {
                DataTable dt = new System.Data.DataTable();
                using (var session = CreateSession())
                {
                    try
                    {
                        IDataReader dreader = session.Connection.ExecuteReader(cmd, null, session.Transaction);
                        DataTable schemaTable = dreader.GetSchemaTable();
                        //动态构建表，添加列
                        foreach (DataRow dr in schemaTable.Rows)
                        {
                            DataColumn dc = new DataColumn();
                            //设置列的数据类型
                            dc.DataType = Type.GetType(dr["DataType"].ToString());
                            //设置列的名称
                            dc.ColumnName = dr[0].ToString();
                            //将该列添加进构造的表中
                            dt.Columns.Add(dc);
                        }
                        while (dreader.Read())
                        {
                            DataRow dr = dt.NewRow();
                            for (int i = 0; i < dreader.FieldCount; i++)
                            {
                                dr[i] = dreader[i];
                            }
                            dt.Rows.Add(dr);
                        }
                        dreader.Close();
                        dreader.Dispose();
                    }
                    catch (Exception ex)
                    {
                        session.Rollback();
                    }
                }
                return (dt);
            }
            catch
            {
                return (new DataTable());
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public SQLServerHepler(string SqlConnection)
        {
            if (SqlConnection != "")
                this.sqlconstr = SqlConnection;
        }

        #region IDataBaseCommand接口实现
        /// <summary>
        /// Select语句
        /// </summary>
        /// <param name="Condition">条件</param>
        /// <returns></returns>
        public DataTable Select(ITable Condition)
        {
            string sqlcommandstring = "SELECT * FROM " + Condition.GetThisTableName();
            string afterstring = Condition.GetSelectString();
            if (afterstring != "")
            {
                sqlcommandstring += " WHERE " + afterstring;
            }
            return (DataRead(sqlcommandstring));
        }
        /// <summary>
        /// Select语句
        /// </summary>
        /// <param name="Condition">条件</param>
        /// <param name="OrderBy">根据条件进行排序</param>
        /// <returns></returns>
        public DataTable Select(ITable Condition, string OrderBy)
        {
            string sqlcommandstring = "SELECT * FROM " + Condition.GetThisTableName();
            string afterstring = Condition.GetSelectString();
            if (afterstring != "")
            {
                sqlcommandstring += " WHERE " + afterstring;
            }
            if (OrderBy != "")
            {
                sqlcommandstring += " ORDER BY " + OrderBy + " ASC";
            }
            return (DataRead(sqlcommandstring));

        }
        /// <summary>
        /// Select语句 
        /// </summary>
        /// <param name="Condition">条件</param>
        /// <param name="OrderBy">根据条件进行排序</param>
        /// <param name="TopCount">选择前多少条数据</param>
        /// <returns></returns>
        public DataTable Select(ITable Condition, string OrderBy, int TopCount)
        {
            string sqlcommandstring = "SELECT * FROM " + Condition.GetThisTableName();
            string afterstring = Condition.GetSelectString();
            if (afterstring != "")
            {
                sqlcommandstring += " WHERE " + afterstring;
            }
            if (OrderBy != "")
            {
                sqlcommandstring += " ORDER BY " + OrderBy + " DESC LIMIT " + TopCount;
            }
            return (DataRead(sqlcommandstring));
        }
        /// <summary>
        /// 根据自定义字段进行选择
        /// </summary>
        /// <param name="Command"></param>
        /// <returns></returns>
        public DataTable Select(string Command)
        {
            return (DataRead(Command));
        }
        /// <summary>
        /// 查询是否存在此行
        /// </summary>
        /// <param name="Condition">条件</param>
        /// <returns></returns>
        public bool Exist(ITable Condition)
        {
            string sqlcommandstring = "SELECT * FROM " + Condition.GetThisTableName();
            string afterstring = Condition.GetSelectString();
            if (afterstring != "")
            {
                sqlcommandstring += " WHERE " + afterstring;
            }
            DataTable dt = DataRead(sqlcommandstring);
            if (dt.Rows.Count > 0)
                return (true);
            else
                return (false);
        }
        /// <summary>
        /// Delete语句
        /// </summary>
        /// <param name="Condition">条件</param>
        /// <returns></returns>
        public bool Delete(ITable Condition)
        {
            string sqlcommandstring = "DELETE FROM " + Condition.GetThisTableName();
            string afterstring = Condition.GetSelectString();
            if (afterstring != "")
            {
                sqlcommandstring += "\r\n" + "WHERE " + afterstring;
            }
            using (var session = CreateSession())
            {
                try
                {
                    int cnt = session.Connection.Execute(sqlcommandstring, null, session.Transaction);
                    if (cnt == 0)
                        return false;
                }
                catch (Exception ex)
                {
                    session.Rollback();
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 删除语句
        /// </summary>
        /// <param name="Value">值</param>
        /// <returns></returns>
        public bool Delete<T>(List<T> Value) where T : ITable
        {
            using (SqlConnection m_sqlcon = CreateConnection() as SqlConnection)
            {
                using (SqlTransaction trans = m_sqlcon.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand("", m_sqlcon))
                    {
                        cmd.Transaction = trans;
                        try
                        {
                            int cnt = 0;
                            for (int i = 0; i < Value.Count; i++)
                            {
                                string sqlcommandstring = string.Format("DELETE FROM {0} ", Value[i].GetThisTableName());
                                string afterstring = Value[i].GetSelectString();
                                if (afterstring == "")
                                    return (false);
                                sqlcommandstring = string.Format("{0}WHERE {1}", sqlcommandstring, afterstring);
                                cmd.CommandText = sqlcommandstring;
                                cmd.ExecuteNonQuery();
                                cnt++;
                            }
                            if (cnt == 0)
                                return false;
                            trans.Commit();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            return false;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Insert语句
        /// </summary>
        /// <param name="Value">值</param>
        /// <returns></returns>
        public bool Insert(ITable Value)
        {
            string sqlcommandstring = string.Format("INSERT INTO {0} ", Value.GetThisTableName());
            string afterstring = Value.GetInsertString();
            if (afterstring == "")
                return (false);
            sqlcommandstring += afterstring;
            using (var session = CreateSession())
            {
                try
                {

                    int cnt = session.Connection.Execute(sqlcommandstring.Replace("\\", "\\\\"), null, session.Transaction);
                    if (cnt == 0)
                        return false;
                }
                catch (Exception ex)
                {
                    session.Rollback();
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Insert语句
        /// </summary>
        /// <param name="Value">值</param>
        /// <returns></returns>
        public bool Insert<T>(List<T> Value) where T : ITable
        {
            using (var session = CreateSession())
            {
                int cnt = 0;
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(string.Format("INSERT INTO {0} ", Value[0].GetThisTableName()));
                    for (int i = 0; i < Value.Count; i++)
                    {
                        string afterstring = Value[i].GetInsertString();
                        if (afterstring == "")
                            return (false);
                        if (i > 0)
                        {
                            afterstring = afterstring.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)[1].Replace("VALUES", "");
                        }
                        afterstring = afterstring.Replace("\\", "\\\\");
                        sb.AppendFormat("{0}{1}", afterstring, i == Value.Count - 1 ? "" : ",");
                    }
                    int n = session.Connection.Execute(sb.ToString(), null, session.Transaction);
                    cnt += n;
                    if (cnt == 0)
                        return false;
                    return true;
                }
                catch (Exception ex)
                {
                    session.Rollback();
                    return false;
                }
            }
        }
        /// <summary>
        /// Update语句
        /// </summary>
        /// <param name="OldValue">原值</param>
        /// <param name="NewValue">新值</param>
        /// <returns></returns>
        public bool Update(ITable OldValue, ITable NewValue)
        {
            string sqlcommandstring = "UPDATE " + OldValue.GetThisTableName();
            string afterstring = OldValue.GetSelectString();
            string afterstring2 = NewValue.GetUpdateString();
            if (afterstring == "" || afterstring2 == "")
                return (false);
            sqlcommandstring += "\r\n" + afterstring2 + "\r\nWHERE " + afterstring;
            using (var session = CreateSession())
            {
                try
                {

                    int cnt = session.Connection.Execute(sqlcommandstring.Replace("\\", "\\\\"), null, session.Transaction);
                    if (cnt == 0)
                        return false;
                }
                catch (Exception ex)
                {
                    session.Rollback();
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Update语句
        /// </summary>
        /// <returns></returns>
        public bool Update(Dictionary<ITable, ITable> tables)
        {
            using (SqlConnection m_sqlcon = CreateConnection() as SqlConnection)
            {
                using (SqlTransaction trans = m_sqlcon.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand("", m_sqlcon))
                    {
                        cmd.Transaction = trans;
                        try
                        {
                            int cnt = 0;
                            foreach (KeyValuePair<ITable, ITable> p in tables)
                            {
                                string sqlcommandstring = "UPDATE " + p.Value.GetThisTableName();
                                string afterstring = p.Key.GetSelectString();
                                string afterstring2 = p.Value.GetUpdateString();
                                if (afterstring == "" || afterstring2 == "")
                                    return (false);
                                sqlcommandstring = string.Format("{0}\r\n{1}\r\nWHERE {2}", sqlcommandstring, afterstring2, afterstring);
                                cmd.CommandText = sqlcommandstring.Replace("\\", "\\\\");
                                cmd.ExecuteNonQuery();
                                cnt++;
                            }
                            if (cnt == 0)
                                return false;
                            trans.Commit();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            return false;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 删除并复位自增字段
        /// </summary>
        /// <param name="Condition">条件</param>
        /// <returns></returns>
        public bool Truncate(ITable Condition)
        {
            string sqlcommandstring = "TRUNCATE TABLE " + Condition.GetThisTableName();
            using (var session = CreateSession())
            {
                try
                {
                    int cnt = session.Connection.Execute(sqlcommandstring, null, session.Transaction);
                    if (cnt == 0)
                        return false;
                }
                catch (Exception ex)
                {
                    session.Rollback();
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 自定义语句
        /// </summary>
        /// <param name="CommandString">SQL语句</param>
        /// <returns></returns>
        public bool Command(string CommandString)
        {
            using (var session = CreateSession())
            {
                try
                {
                    int cnt = session.Connection.Execute(CommandString, null, session.Transaction);
                    if (cnt == 0)
                        return false;
                }
                catch (Exception ex)
                {
                    session.Rollback();
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 关闭SQL连接
        /// </summary>
        public void Close()
        {

        }

        /// <summary>
        /// 获取所有表名
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllTableName()
        {
            List<string> retNameList = new List<string>();
            DataTable tbName = (CreateConnection() as SqlConnection).GetSchema("Tables");
            if (tbName.Columns.Contains("TABLE_NAME"))
            {
                foreach (DataRow dr in tbName.Rows)
                {
                    retNameList.Add((string)dr["TABLE_NAME"]);
                }
            }
            return retNameList;
        }
        #endregion
        #region ISessionFactory接口实现
        /// <summary>
        /// 创建数据库连接
        /// </summary>
        public IDbConnection CreateConnection()
        {
            IDbConnection m_sqlcon = new SqlConnection(sqlconstr);
            if (m_sqlcon.State == ConnectionState.Closed)
            {
                m_sqlcon.Open();
            }

            return m_sqlcon;
        }

        /// <summary>
        /// 创建数据库连接会话
        /// </summary>
        public IDbSession CreateSession()
        {
            IDbConnection conn = CreateConnection();
            IDbSession session = new DbSession(conn);

            return session;
        }

        /// <summary>
        /// 创建数据库事务会话
        /// </summary>
        public IDbSession CreateSession(IDbConnection conn, IDbTransaction trans)
        {
            IDbSession session = new DbSession(conn, trans);
            return session;
        }
        #endregion
    }
}
