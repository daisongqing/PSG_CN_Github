using AwareTec.Polysmith.pChart;
using AwareTec.Polysmith.UI.DataModel;
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

namespace AwareTec.Polysmith.UI.Block
{
    public partial class AutoSearchDialog : SkinForm
    {
        #region 全局变量
        private bool init = false;
        private List<AnalysisRecordViewData> m_records = null;
        /// <summary>
        /// 当前应用标记
        /// </summary>
        public IMarker CurrentMarker { set; get; }
        #endregion

        #region 事件与委托的定义
        public delegate void SearchRecordsDelegate(List<AnalysisRecordViewData> records);
        /// <summary>
        /// 自动搜索记录触发事件
        /// </summary>
        public event SearchRecordsDelegate SearchRecordsEventHandle;
        #endregion

        #region 内部方法

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
        /// 搜索路径下所有当前扩展名的文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="Extension">扩展名</param>
        /// <param name="smallDir"></param>
        public static void GetFile(string path, string Extension, ref List<string> files)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] fil = dir.GetFiles();
            DirectoryInfo[] dii = dir.GetDirectories();
            foreach (FileInfo f in fil)
            {
                string name = Path.GetExtension(f.FullName);
                if (name.Equals(Extension))
                {
                    files.Add(f.FullName);
                }
            }
            //获取子文件夹内的文件列表，递归遍历
            foreach (DirectoryInfo d in dii)
            {
                GetFile(d.FullName, Extension, ref files);
            }
        }
        #endregion


        #region 初始化相关

        /// <summary>
        /// 构造函数
        /// </summary>
        public AutoSearchDialog()
        {
            InitializeComponent();

            #region 为防止designer随意更改 解绑事件，因此事件绑定一律放置于逻辑代码的构造函数中
            this.CancelButton.Click += this.CancelButton_Click;
            this.SearchButton.Click += this.SearchButton_Click;
            this.BrowseButton.Click += this.BrowseButton_Click;
            this.FindFromUDiskRadioButton.CheckedChanged += FindFromUDiskRadioButton_CheckedChanged;
            #endregion
        }

        private void FindFromUDiskRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(FindFromUDiskRadioButton.Checked)
            {
                FindFromLocalPathTextBox.Text = null;
            }
        }

        /// <summary>
        /// 初始化信息
        /// </summary>
        /// <param name="records"></param>
        public void Init(List<AnalysisRecordViewData> records)
        {
            m_records = records;
        }

        #endregion

        #region 所有控件的绑定事件

        /// <summary>
        /// 取消按钮点击时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            DataModel.LogInstance.Default.AddLog("点击 自动搜索-取消 按钮");
            Channel.Default.FnStop = true;
            this.Close();
            this.Dispose();
        }

        /// <summary>
        /// 搜索按钮点击时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            Channel.Default.FnStop = false;
            SearchButton.Enabled = false;
            SearchProgressBar.Visible = true;
            Task.Factory.StartNew(() =>
            {
                List<AnalysisRecordViewData> records = new List<AnalysisRecordViewData>();
                int processValue = 1;
                float cnt = 0;
                float offset = 0;
                List<string> allFiles = new List<string>();
                if (FindFromUDiskRadioButton.Checked)
                {
                    SetProcessMsg(processValue, "搜索移动盘符...");
                    string[] dirs = Environment.GetLogicalDrives(); //取得所有的盘符 
                    foreach (string dir in dirs)
                    {
                        DriveInfo Tdriver = new DriveInfo(dir);
                        if (Tdriver.DriveType == DriveType.Removable&&Tdriver.IsReady)
                        {
                            string[] files = Directory.GetFiles(dir, "*.edf");
                            cnt += files.Length;
                            allFiles.AddRange(files);
                        }
                    }
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
                        AnalysisRecordViewData record = m_records.Find(t => t.MatchKey == edf.getMatchKey());
                        if (record != null)
                        {
                            string path = System.IO.Path.Combine(edf.DefaultEdfSavePath, string.Format("{0:yyyy-MM-dd}\\{1}", edf.StartTime, Path.GetFileName(allFiles[i])));
                            string dirPath = Path.GetDirectoryName(path);
                            if (!Directory.Exists(dirPath))
                            {
                                Directory.CreateDirectory(dirPath);
                            }
                            if (Channel.Default.FMove(allFiles[i], path))
                            {
                                record.EdfPath = path;
                                records.Add(record);
                            }
                        }
                        processValue = (int)(processValue + offset);
                    }
                    DataModel.LogInstance.Default.AddLog("用户选择从 U盘查找 点击 自动搜索-搜索 按钮");
                }
                else
                {
                    if (FindFromLocalPathTextBox.Text.Trim() == "")
                    {
                        AhDung.MessageTip.ShowWarning("路径不能为空");
                        this.Invoke(new MethodInvoker(() =>
                        {
                            SearchProgressBar.Visible = false;
                            SearchButton.Enabled = true;
                        }));
                        return;
                    }
                    SetProcessMsg(processValue, "搜索路径下所有edf文件...");
                    GetFile(FindFromLocalPathTextBox.Text, ".edf", ref allFiles);
                    cnt = allFiles.Count;
                    processValue += 10;
                    SetProcessMsg(processValue, "搜索病例记录...");
                    processValue += 10;
                    offset = (100 - processValue) / cnt;
                    for (int i = 0; i < cnt; i++)
                    {
                        SetProcessMsg(processValue, string.Format("正在匹配({0}/{1})...", i, cnt));
                        EDF edf = EDF.Default.ConvertToEDF(EDF.Default.ReadFile(allFiles[i], false));
                        AnalysisRecordViewData record = m_records.Find(t => t.MatchKey == edf.getMatchKey());
                        if (record != null)
                        {
                            string s = FindFromLocalPathTextBox.Text.Trim();
                            //DriveInfo会自动将详细地址只取第一位的盘地址
                            DriveInfo Tdriver = new DriveInfo(s);
                            //只有u盘之类的才会复制文件
                            if (Tdriver.DriveType == DriveType.Removable && Tdriver.IsReady)
                            {
                                string path = System.IO.Path.Combine(edf.DefaultEdfSavePath, string.Format("{0:yyyy-MM-dd}\\{1}", edf.StartTime, Path.GetFileName(allFiles[i])));
                                string dirPath = Path.GetDirectoryName(path);
                                if (!Directory.Exists(dirPath))
                                {
                                    Directory.CreateDirectory(dirPath);
                                }
                                if (Channel.Default.FMove(allFiles[i], path))
                                {
                                    record.EdfPath = path;
                                }
                            }
                            //本地文件需要给edfpath属性赋值，才可以直接双击进入回放
                            else
                            {
                                record.EdfPath = allFiles[i];
                            }
                            records.Add(record);
                        }
                        processValue = (int)(processValue + offset);
                    }
                    DataModel.LogInstance.Default.AddLog("用户选择从 本地路径查找 点击 自动搜索-搜索 按钮");
                }
                this.Invoke(new MethodInvoker(() =>
                {
                    SearchProgressBar.Visible = false;
                    SearchButton.Enabled = true;
                    this.Close();
                }));
                if (SearchRecordsEventHandle != null)
                {
                    SearchRecordsEventHandle.BeginInvoke(records, null, null);
                }
            });
        }

        /// <summary>
        /// 浏览按钮点击时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new System.Windows.Forms.FolderBrowserDialog();
            folder.Description = "选择搜索路径";
            if (folder.ShowDialog() == DialogResult.OK)
            {
                FindFromLocalPathTextBox.Text = folder.SelectedPath;
                FindFromLocalPathRadioButton.Checked = true;
                FindFromUDiskRadioButton.Checked = false;
                DataModel.LogInstance.Default.AddLog(string.Format("点击 自动搜索-浏览 按钮，浏览路径为 {0}", StringPath.ConvertLogPath(folder.SelectedPath)));
            }
        }

        #endregion
    }
}
