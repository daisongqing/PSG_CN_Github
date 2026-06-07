using AwareTec.Polysmith.UI.DataModel;
using AwareTec.Polysmith.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using AwareTec.Polysmith.UI.FunctionControls.tools;
using pSystem.LogManagement.TotalLog;

namespace AwareTec.Polysmith.UI
{

    static class Program
    {
        public static bool IsLogin = false;
        public static bool newUser = false;
        public static string userName = "";
        public static bool LocationScan = true;

        public static string Language = "CN";
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            try
            {
                System.Threading.ThreadPool.SetMinThreads(50, 50);
                //处理未捕获的异常   
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                //处理UI线程异常   
                Application.ThreadException += Application_ThreadException;
                //处理非UI线程异常   
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

                ML.LoadLanguages("*.lng", "ENG");

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //获得当前登录的Windows用户标示
                System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
                System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);

                if (!TotalLogManager.Start())
                    throw new Exception("日志总记录管理器开启失败");
                //判断当前登录用户是否为管理员
                if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
                {
                    LogInstance.Default.AddLog("系统启动运行...", pSystem.LogManagement.LogLevel.DEBUG);
                IsLogin:
                    IsLogin = false;
#if true
                    RegistForm reg = new RegistForm();
                    if (!reg.isOk)
                    {
                        reg.ShowDialog();
                       // reg.isOk = true;
                    }
                    if (reg.isOk)
#endif
                    {
                        Login login = new Login();
                        if (login.ShowDialog() == DialogResult.OK)
                        {
                            if (LocationScan)
                            {
                                DataModel.DeviceOnLine.Default.Start = true;
                                DataModel.DeviceOnLine.Default.LoadReady = true;
                            }
                            else
                            {
                                DataModel.DeviceOnWLan.Default.Start = true;
                            }
                            Microsoft.VisualBasic.Interaction.Beep();
                            Application.Run(new MainForm());
                            Microsoft.VisualBasic.Interaction.Beep();
                            DataModel.DeviceOnLine.Default.LoadReady = false;
                            if (LocationScan)
                            {
                                DataModel.DeviceOnLine.Default.Start = false;
                                DataModel.DeviceOnLine.Default.LoadReady = false;
                            }
                            else
                            {
                                DataModel.DeviceOnWLan.Default.Start = false;
                            }
                        }
                        else
                        {
                            Application.Exit();
                        }
                        //当关闭主程序的时候会执行这个代码,在关闭主程序的时候需要给IsLogin 设置成true,那么就goto 到IsLogin,然后又重新回到登录窗口.
                        if (IsLogin)
                        {
                            goto IsLogin;
                        }
                        DataModel.DeviceOnLine.Default.Dispose();
                        LogInstance.Default.AddLog("退出系统...", pSystem.LogManagement.LogLevel.DEBUG);
                        LogInstance.Default.Dispose();
                        if (!TotalLogManager.End())
                            throw new Exception("日志总记录管理器终止失败");
                        Application.Exit();
                    }
                    //else
                    //{
                    //    MessageForm.Show("注册码失效，请联系厂家客服！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}
                }
                else
                {
                    //创建启动对象
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    //设置运行文件
                    startInfo.FileName = System.Windows.Forms.Application.ExecutablePath;
                    //设置启动动作,确保以管理员身份运行
                    startInfo.Verb = "runas";
                    //如果不是管理员，则启动UAC
                    System.Diagnostics.Process.Start(startInfo);
                    //退出
                    System.Windows.Forms.Application.Exit();
                }
            }
            catch (Exception ex)
            {
                var strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now + "\r\n";

                var str = string.Format(strDateInfo + "异常类型：{0}\r\n异常消息：{1}\r\n异常信息：{2}\r\n",
                                           ex.GetType().Name, ex.Message, ex.StackTrace);
                LogInstance.Default.AddLog(str,pSystem.LogManagement.LogLevel.FATAL);
                MessageForm.Show("发生错误，请查看程序日志！", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            LogInstance.Default.AddLog(str, pSystem.LogManagement.LogLevel.FATAL);
            MessageForm.Show("发生错误，请查看程序日志！", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var error = e.ExceptionObject as Exception;
            var strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now + "\r\n";
            var str = error != null ? string.Format(strDateInfo + "Application UnhandledException:{0}; 堆栈信息:{1}", error.Message, error.StackTrace) : string.Format("Application UnhandledError:{0}", e);

            LogInstance.Default.AddLog(str, pSystem.LogManagement.LogLevel.FATAL);
            MessageForm.Show("发生错误，请查看程序日志！", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="str"></param>
        static void WriteLog(string str)
        {
            if (!Directory.Exists("ErrLog"))
            {
                Directory.CreateDirectory("ErrLog");
            }

            using (var sw = new StreamWriter(@"ErrLog\ErrLog.txt", true))
            {
                sw.WriteLine(str);
                sw.WriteLine("---------------------------------------------------------");
                sw.Close();
            }
        }
    }
}
