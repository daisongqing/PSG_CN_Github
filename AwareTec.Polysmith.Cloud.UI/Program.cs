using AwareTec.Polysmith.Cloud.UI.Forms;
using AwareTec.Polysmith.Cloud.UI.Forms.Tips;
using AwareTec.Polysmith.Cloud.UI.ViewHelpers;
using pSystem.LogManagement.TotalLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.Cloud.UI
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //处理未捕获的异常   
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常   
            Application.ThreadException += Application_ThreadException; ;
            //处理非UI线程异常   
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //获得当前登录的Windows用户标示
            System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
            if (!TotalLogManager.Start())
                throw new Exception("日志总记录管理器开启失败");
            if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            {
                try
                {
                    StartupProgram.Start();
                }
                catch (Exception ee) 
                {
                    MessageForm.Show(ee.Message);
                }
            }
            else
            {
                //创建启动对象
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                //设置运行文件
                startInfo.FileName = Application.ExecutablePath;
                //设置启动动作,确保以管理员身份运行
                startInfo.Verb = "runas";
                //如果不是管理员，则启动UAC
                System.Diagnostics.Process.Start(startInfo);
                //退出
                Application.Exit();
            }
        }


        /// <summary>
        ///错误弹窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string str;
            var strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now + "\r\n";
            var error = e.Exception;
            if (error != null)
            {
                str = string.Format(strDateInfo + "异常类型：{0}\r\n异常消息：{1}\r\n异常信息：{2}\r\n",
                     error.GetType().Name, error.Message, error.StackTrace);
            }
            else
            {
                str = string.Format("应用程序线程错误:{0}", e);
            }

            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(str, pSystem.LogManagement.LogLevel.FATAL);
            MessageForm.Show("发生错误，请查看程序日志！", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var error = e.ExceptionObject as Exception;
            var strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now + "\r\n";
            var str = error != null ? string.Format(strDateInfo + "Application UnhandledException:{0}; 堆栈信息:{1}", error.Message, error.StackTrace) : string.Format("Application UnhandledError:{0}", e);

            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(str, pSystem.LogManagement.LogLevel.FATAL);
            MessageForm.Show("发生错误，请查看程序日志！", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
