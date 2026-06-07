using System.Collections.Generic;
using System.Data;

namespace pSystem.Interface.Util
{
    /// <summary>
    /// 表类基础对象
    /// </summary>
    public interface ITable
    {
        /// <summary>
        /// 获取Select语句子句
        /// </summary>
        /// <returns></returns>
        string GetSelectString();
        /// <summary>
        /// 获取Update语句子句
        /// </summary>
        /// <returns></returns>
        string GetUpdateString();
        /// <summary>
        /// 获取Insert语句子句
        /// </summary>
        /// <returns></returns>
        string GetInsertString();
        /// <summary>
        /// 获取本对象解释名
        /// </summary>
        /// <returns></returns>
        string GetThisTableName();
        /// <summary>
        /// 设置数值
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        void SetValue(string strName, object Value);
        /// <summary>
        /// 获取本对象新的实例
        /// </summary>
        /// <returns></returns>
        ITable GetNewInstance();
    }

    /// <summary>
    /// 针对ITable类进行DataTable转换成实例
    /// </summary>
    /// <typeparam name="MyTable"></typeparam>
    public class TableConvert<MyTable> where MyTable : ITable, new()
    {
        /// <summary>
        /// 转换DataTable为实例对象
        /// </summary>
        /// <param name="table">表格</param>
        /// <returns></returns>
        public List<MyTable> ConvertFromTable(DataTable table)
        {
            List<MyTable> returnvalue = new List<MyTable>();
            int col = table.Columns.Count;
            foreach (DataRow dr in table.Rows)
            {
                MyTable item = new MyTable();
                for (int colindex = 0; colindex < col; colindex++)
                {
                    try
                    {
                        item.SetValue(table.Columns[colindex].ColumnName, dr[colindex]);
                    }
                    catch { }
                }
                returnvalue.Add(item);
            }
            return ((List<MyTable>)returnvalue);
        }
    }
}
