using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pSystem.LogManagement;
namespace AwareTec.Polysmith.Cloud.UI.DataModels
{
    /// <summary>
    /// 日志操作类
    /// </summary>
    public class LogInstance
    {
        public LogInstance()
        {
        }
        public void AddLog(string msg,LogLevel level)
        {
            Logger.Instance.Log(msg,level);
        }
        /// <summary>
        /// 记录日志信息
        /// </summary>
        /// <param name="msg"></param>
        public void AddLog(string msg)
        {
            AddLog(msg, LogLevel.INFO);
        }

        public void Dispose()
        {
            Logger.Instance.Dispose();
        }
    }
}
