using AwareTec.Polysmith.Cloud.UI.DataModels;
using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.FullUser;
using AwareTec.Polysmith.Cloud.UI.DataModels.Global;
using AwareTec.Polysmith.Util;
using AwareTec.Polysmith.Util.PathUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.Cloud.UI.Forms.MainFormBlock
{
    public partial class AutoSearchEDFDialog : CloudSkinForm
    {
        #region 私有变量
        private FullUserConfigurationModel fullUserConfig = null;
        private string m_matchKey = "";
        private string _edfPath = string.Empty;
        #endregion

        #region 公有变量
        public string EdfPath => _edfPath;
        #endregion

        #region 事件委托

        public delegate void SearchRecordsDelegate(string edfPath);
        /// <summary>
        /// 自动搜索记录触发事件
        /// </summary>
        public event SearchRecordsDelegate SearchRecordsEventHandle;

        #endregion

        #region 初始化 load/dispose/init

        /// <summary>
        /// 初始化
        /// </summary>
        public AutoSearchEDFDialog()
        {
            InitializeComponent();
            this.Load += AutoSearchEDFDialog_Load;
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoSearchEDFDialog_Load(object sender, EventArgs e)
        {
            this.SubmitButton.Click += SubmitButton_Click;
            this.CancelButton.Click += CancelButton_Click;
            this.BrowseButton.Click += BrowseButton_Click;
            this.autoSearchLink.Click += AutoSearchLink_Click;
            fullUserConfig = GlobalSingleton.Instance.getSystemSetting();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 设置进度
        /// </summary>
        /// <param name="value"></param>
        /// <param name="contentTxt"></param>
        private void SetProcessMsg(int value, string contentTxt)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                SearchProgressBar.Value = value;
            }));
        }

        /// <summary>
        /// 自动搜索匹配数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoSearchLink_Click(object sender, EventArgs e)
        {
            Channel.Default.FnStop = false;
            SearchProgressBar.Visible = true;
            autoSearchLink.Enabled = false;
            Task.Factory.StartNew(() =>
            {
                int processValue = 1;
                float cnt = 0;
                float offset = 0;
                List<string> allFiles = new List<string>();
                string m_edfpath = "";
                {
                    DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("用户选择从 U盘查找");
                    SetProcessMsg(processValue, "搜索移动盘符...");
                    string[] dirs = Environment.GetLogicalDrives(); //取得所有的盘符 
                    foreach (string dir in dirs)
                    {
                        DriveInfo Tdriver = new DriveInfo(dir);
                        if (Tdriver.DriveType == DriveType.Removable && Tdriver.IsReady)
                        {
                            string[] files = Directory.GetFiles(dir, "*.edf");
                            cnt += files.Length;
                            allFiles.AddRange(files);
                        }
                    }
                    if (cnt > 0)
                    {
                        processValue += 10;
                        SetProcessMsg(processValue, "搜索病例记录...");
                        processValue += 10;
                        offset = (100 - processValue) / cnt;
                        for (int i = 0; i < cnt; i++)
                        {
                            if (Channel.Default.FnStop)
                                break;
                            SetProcessMsg(processValue, string.Format("正在匹配({0}/{1})...", i, cnt));
                            EDF edf = EDF.Default.ConvertToEDF(EDF.Default.ReadFile(allFiles[i], false));
                            if (m_matchKey == edf.getMatchKey())
                            {
                                string path = System.IO.Path.Combine(edf.DefaultEdfSavePath, string.Format("{0:yyyy-MM-dd}\\{1}", edf.StartTime, Path.GetFileName(allFiles[i])));
                                string dirPath = Path.GetDirectoryName(path);
                                if (!Directory.Exists(dirPath))
                                {
                                    Directory.CreateDirectory(dirPath);
                                }
                                if (Channel.Default.FMove(allFiles[i], path))
                                {
                                    m_edfpath = path;
                                    processValue = 100;
                                    break;
                                }
                            }
                            processValue = (int)(processValue + offset);
                        }
                    }
                }
                if (m_edfpath == "")
                {
                    DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("移动盘符未找到 本地路径查找",pSystem.LogManagement.LogLevel.ERROR);
                    processValue = 0;
                    SetProcessMsg(processValue, "搜索存储路径下匹配文件夹...");
                    cnt = 0;
                    List<string> files = new List<string>();
                    string searchPath = string.Format("{0}", fullUserConfig.RootPath);
                    //string searchPath = string.Format("{0}\\{1}", fullUserConfig.RootPath, Tag.ToString());
                    FolderHelper.GetFile(searchPath, ".edf", ref allFiles);
                    files.AddRange(allFiles);
                    cnt = files.Count;
                    if (cnt > 0)
                    {
                        processValue += 10;
                        SetProcessMsg(processValue, "搜索病例记录...");
                        processValue += 10;
                        offset = (100 - processValue) / cnt;
                        for (int i = 0; i < cnt; i++)
                        {
                            SetProcessMsg(processValue, string.Format("正在匹配({0}/{1})...", i, cnt));
                            EDF edf = EDF.Default.ConvertToEDF(EDF.Default.ReadFile(files[i], false));
                            if (m_matchKey == edf.getMatchKey())
                            {
                                //DriveInfo会自动将详细地址只取第一位的盘地址
                                DriveInfo Tdriver = new DriveInfo(files[i]);
                                //只有u盘之类的才会复制文件
                                if (Tdriver.DriveType == DriveType.Removable && Tdriver.IsReady)
                                {
                                    string path = System.IO.Path.Combine(edf.DefaultEdfSavePath, string.Format("{0:yyyy-MM-dd}\\{1}", edf.StartTime, Path.GetFileName(files[i])));
                                    string dirPath = Path.GetDirectoryName(path);
                                    if (!Directory.Exists(dirPath))
                                    {
                                        Directory.CreateDirectory(dirPath);
                                    }
                                    if (Channel.Default.FMove(files[i], path))
                                    {
                                        m_edfpath = path;
                                        processValue = 100;
                                        break;
                                    }
                                }
                                //本地文件需要给edfpath属性赋值，才可以直接双击进入回放
                                else
                                {
                                    m_edfpath = files[i];
                                    break;
                                }
                            }
                            processValue = (int)(processValue + offset);
                        }
                    }
                }
                this.Invoke(new MethodInvoker(() =>
                {
                    autoSearchLink.Enabled = true;
                    SearchProgressBar.Visible = false;
                    if (m_edfpath != "")
                    {
                        AhDung.MessageTip.ShowOk("匹配成功！");
                        FindFromLocalPathTextBox.Text = m_edfpath;
                        DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("匹配到符合的edf文件：{0}", m_edfpath),pSystem.LogManagement.LogLevel.WARN);
                    }
                    else
                    {
                        FindFromLocalPathTextBox.Text = "";
                        AhDung.MessageTip.ShowError("未匹配到符合的edf文件！");
                        DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("未匹配到符合的edf文件！", pSystem.LogManagement.LogLevel.ERROR);
                    }
                }
                ));
            });
        }

        #endregion

        #region 按钮方法

        /// <summary>
        /// 开始上传 浏览按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "EDF数据文件|*.edf";
            open.RestoreDirectory = true;
            open.FilterIndex = 1;
            if (open.ShowDialog() == DialogResult.OK)
            {
                FindFromLocalPathTextBox.Text = open.FileName;
                DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog(string.Format("点击 上传文件-浏览 按钮，浏览路径为 {0}", StringPath.ConvertLogPath(open.FileName)));
            }
        }

        /// <summary>
        /// 开始上传 取消按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("点击 自动搜索-取消 按钮");
            Channel.Default.FnStop = true;
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        /// <summary>
        /// 开始上传 上传按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            string path = FindFromLocalPathTextBox.Text.Trim();
            if (path != "")
            {
                EDF edf = EDF.Default.ConvertToEDF(EDF.Default.ReadFile(path, false));
                if (m_matchKey == edf.getMatchKey())
                {
                    _edfPath = path;
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    AhDung.MessageTip.ShowWarning("请选择匹配的edf文件！");
                }
            }
            else
            {
                AhDung.MessageTip.ShowWarning("请选择符合的edf文件！");
            }
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 初始化信息
        /// </summary>
        /// <param name="records"></param>
        public void Init(string matchKey)
        {
            m_matchKey = matchKey;
        }

        #endregion
    }
}
