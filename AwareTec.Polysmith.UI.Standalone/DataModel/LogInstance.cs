using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pSystem.LogManagement;
namespace AwareTec.Polysmith.UI.DataModel
{
    /// <summary>
    /// 日志操作类
    /// </summary>
    public class LogInstance
    {
        private static LogInstance m_default = null;
        public static LogInstance Default
        {
            get
            {
                return m_default ?? (m_default = new LogInstance());
            }

        }
        private LogInstance()
        {
            if (!TemplateCheck.Default.Check())
            {
                AhDung.MessageTip.ShowError("缺少必要的启动文件，系统无法正常运行！");
                m_default = null;
                return;
            }
        }
        public void AddLog(string msg, LogLevel level)
        {
            if (Channel.Default.Loginer != null)
                Logger.Instance.UserName = Channel.Default.Loginer.Name;
            Logger.Instance.Log(msg, level);
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
