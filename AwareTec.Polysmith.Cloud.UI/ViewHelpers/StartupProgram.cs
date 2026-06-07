using AwareTec.Polysmith.Cloud.UI.DataModels.EnumModels;
using AwareTec.Polysmith.Cloud.UI.DataModels.Global;
using AwareTec.Polysmith.Cloud.UI.Forms;
using AwareTec.Polysmith.Cloud.UI.Forms.Tips;
using AwareTec.Polysmith.Util.NetworkUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.Cloud.UI.ViewHelpers
{
    public class StartupProgram
    {
        public static MainForm MainForm = null;

        public static void Start()
        {
            System.Threading.ThreadPool.SetMinThreads(300, 200);
            var bgThread = new CheckNetworkIsAvailableBackgroundThread();
            GlobalSingleton.Instance.Start = true;
#if !DEBUG
            #region 软件加载前检查
            CheckResults checkResults = CheckResults.Empty;
            while (checkResults != CheckResults.Ok)
            {
                checkResults = CheckBeforeLoadSoftware.Check();
                switch (checkResults)
                {
                    case CheckResults.NotCurrentlyTheLatestVersion:
                        {
                            var form = new ForceUpdatePromptForm();
                            if(form.ShowDialog() == DialogResult.Cancel)
                                Environment.Exit(1);
                        }
                        break;
                    case CheckResults.SerialNumberNotBeActivated:
                        {
                            var form = new LicenseActivationForm();
                            if(form.ShowDialog() == DialogResult.Cancel)
                                Environment.Exit(1);
                        }
                        break;
                    case CheckResults.ReadyDataError:
                        {
                            MessageForm.Show("路径数据准备失败");
                            Environment.Exit(1);
                        }
                        break;
                    case CheckResults.Ok:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(GlobalReadonlyString.CommonException.EnumOutOfExpectedRange);
                }
            }
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("软件加载前检查必要文件成功", pSystem.LogManagement.LogLevel.WARN);

            #endregion
#endif
            MainFormCloseMethod closeMethod = MainFormCloseMethod.Empty;
            while (closeMethod != MainFormCloseMethod.Exit)
            {
                RestfulWebRequest.ApiCall.ApiRequest.RemoteUrl = GlobalSingleton.Instance.FullUserConfig.RemoteUrl;
                var loginForm = new LoginForm();
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    MainForm = new MainForm();
                    DataModels.DeviceOnLine.Default.Start = true;
                    if (DataModels.Channel.Default != null)
                        Application.Run(MainForm);
                    DataModels.DeviceOnLine.Default.Start = false;
                    closeMethod = MainForm.MainFormCloseMethod;
                }
                else
                    Environment.Exit(1);
            }
            bgThread.End();
        }
    }
}
