using AwareTec.Polysmith.Cloud.UI.Forms.Tips;
using AwareTec.Polysmith.Util.EnumUtils;
using pSystem.LogManagement;
using pSystem.LogManagement.TotalLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace AwareTec.Polysmith.Cloud.UI.Views
{
    public partial class ObverseLogCtrl : UserControl
    {
        #region 私有字段
        private readonly DateTime _startTime;
        private readonly DateTime _endTime;
        private readonly int _crossDays;
        private readonly List<string> _logFiles;

        private readonly List<pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox> _checkBoxList;

        /// <summary>
        /// 当前查看日志的页码
        /// </summary>
        private int _currentPageNo = 0;

        private List<LogTyp> _checkedLogtyp = new List<LogTyp>();

        private const string CHECKBOX = "CheckBox";

        private readonly bool _isCurrent = true;
        #endregion

        #region 枚举
        private enum LogTyp
        {
            [Description("严重错误(fatal)")]
            FATAL,
            [Description("错误(error)")]
            ERROR,
            [Description("警告(warn)")]
            WARN,
            [Description("信息(info)")]
            INFO,
            [Description("调试(debug)")]
            DEBUG
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 用于当前日志的构造函数
        /// </summary>
        /// <param name="log"></param>
        public ObverseLogCtrl(LogPerRun log)
        {
            InitializeComponent();
            _isCurrent = true;
            this.Resize += ObverseLogCtrl_Resize;
            _checkBoxList = GetAllCheckBox();

            //初始化数据
            _startTime = log.StartTime;
            _endTime = log.EndTime;
            _crossDays = log.CrossDays;
            _logFiles = log.PathList;

            OpenBrowser();
            InitCheckBox();
        }

        public ObverseLogCtrl(LogInfo logInfo)
        {
            InitializeComponent();
            _isCurrent = false;
            this.Resize += ObverseLogCtrl_Resize;
            _checkBoxList = GetAllCheckBox();

            //初始化数据
            _startTime = logInfo.StartTime;
            _endTime = logInfo.EndDate;
            _crossDays = logInfo.CrossDays;
            _logFiles = logInfo.LogFiles;

            OpenBrowser();
            InitCheckBox();
        }


        private void AdjustButtonLocation()
        {
            const int BUTTON_DIFF_STATUS_TOP = 10;

            var barY = LogBasicLogStatusBar.Location.Y;
            NextPageButton.Location = new System.Drawing.Point(NextPageButton.Location.X, barY + BUTTON_DIFF_STATUS_TOP);
            PrePageButton.Location = new System.Drawing.Point(PrePageButton.Location.X, barY + BUTTON_DIFF_STATUS_TOP);
        }
        #endregion

        #region 浏览器
        /// <summary>
        /// 初始化浏览器
        /// </summary>
        /// <param name="pathArr"></param>
        private void OpenBrowser()
        {
            if (_logFiles == null ||
                _logFiles.Count == 0)
            {
                MessageForm.Show("当前无任何操作日志");
                return;
            }

            if(_isCurrent)
                Logger.Instance.LogFileJustBeAppended += this.Instance_LogFileJustBeAppended;
            ShowPage(_logFiles.Count);//默认展示最后一页
        }

        private void Instance_LogFileJustBeAppended(object sender, LogFileJustBeAppendedEventArgs e)
        {
            ShowPage(_logFiles.Count);//默认展示最后一页
        }

        /// <summary>
        /// 根据页码显示浏览器的日志信息
        /// </summary>
        /// <param name="pageNo"></param>
        public void ShowPage(int pageNo)
        {
            var pathStr = _logFiles[pageNo - 1];
            var path = Application.StartupPath + "\\" + pathStr;

            _currentPageNo = pageNo;

            LogWebBrowser.Navigate(path);
            ShowStatusBar();
        }

        /// <summary>
        /// 执行脚本
        /// </summary>
        private void InvokeScript()
        {
            Object[] paramArray = new Object[1];
            string[] str = new string[_checkedLogtyp.Count];
            for(int index = 0; index < _checkedLogtyp.Count; index++)
            {
                string logtyp = Enum.GetName(typeof(LogTyp), _checkedLogtyp[index]);
                str[index] = logtyp.ToUpper();
            }
            string formatArray = String.Join(",", str);
            paramArray[0] = (Object)formatArray;
            LogWebBrowser.Document.InvokeScript("notify", paramArray);
        }
        #endregion

        #region 状态栏
        /// <summary>
        /// 显示状态栏信息
        /// </summary>
        private void ShowStatusBar()
        {
            //页码
            CurrentPageNoLabel.Text = string.Format("当前:第{0}页/总{1}页", _currentPageNo, _logFiles.Count);

            //上一页 下一页按钮可见性
            NextPageButton.Enabled = true;
            PrePageButton.Enabled = true;
            if (_currentPageNo == _logFiles.Count)
                NextPageButton.Enabled = false;
            if (_currentPageNo == 1)
                PrePageButton.Enabled = false;

            //当前日志信息:
            bool isCurrentProgram = DateTime.Compare(_startTime, TotalLogManager.Current.StartTime) == 0;
            string logPerRunInfoTemplate = isCurrentProgram ?
                                           "属于: 运行时日志 | 日志写入始于{0}" :
                                           "属于: 历史日志 | 日志写入始于{0},止于{1}";
            string logPerRunInfoText = isCurrentProgram ?
                                        string.Format(logPerRunInfoTemplate, _startTime) :
                                        string.Format(logPerRunInfoTemplate, _startTime, _endTime);
            LogPerRunInfo.Text = logPerRunInfoText;

            //跨天运行信息:
            string crossDaysTemplate = _crossDays == 0 ?
                                       "该日志仅在一天内运行" :
                                       "该日志运行了{0}天";
            string crossDaysText = _crossDays == 0 ?
                                   crossDaysTemplate : string.Format(crossDaysTemplate, _isCurrent ? _crossDays + 1 : _crossDays);
            CrossDaysLabel.Text = crossDaysText;
        }

        /// <summary>
        /// 处理上一页按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrePageButton_Click(object sender, EventArgs e)
        {
            int page = _currentPageNo - 1;
            ShowPage(page);
        }

        /// <summary>
        /// 处理下一页按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextPageButton_Click(object sender, EventArgs e)
        {
            int page = _currentPageNo + 1;
            ShowPage(page);
        }
        #endregion
        
        #region 复选框

        /// <summary>
        /// 获取当前组件下的所有复选框对象
        /// </summary>
        /// <returns></returns>
        private List<pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox> GetAllCheckBox()
        {
            List<pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox> list = new List<pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox>();
            foreach (var ctrl in RadioButtonPanel.Controls)
            {
                if (!(ctrl is pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox))
                    continue;
                var checkbox = ctrl as pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox;
                list.Add(checkbox);
            }
            return list;
        }
        
        /// <summary>
        /// 初始化复选框
        /// </summary>
        private void InitCheckBox()
        {
            _checkBoxList.ForEach(x =>
            {
                var logTypStr = x.Name.Replace(CHECKBOX, string.Empty);
                Type enumTyp = typeof(LogTyp);
                foreach (var item in Enum.GetValues(enumTyp))
                {
                    if (logTypStr.Equals(Enum.GetName(enumTyp, item), StringComparison.OrdinalIgnoreCase))
                    {
                        x.Tag = item;
                        x.Text = EnumHelper.GetDescription(item);
                        break;
                    }    
                }
            });
            foreach (LogTyp item in Enum.GetValues(typeof(LogTyp)))
                _checkedLogtyp.Add(item);   //初始化时默认全选

            _checkBoxList.ForEach(x => x.CheckedChanged += Checkbox_CheckedChanged);//绑定所有复选框的Check状态值改变事件
        }

        /// <summary>
        /// 复选框选中状态改变事件的处理
        /// </summary>
        /// <param name="sender"></param>
        private void Checkbox_CheckedChanged(object sender)
        {
            const string CHECKBOX = "CheckBox";
            var checkBox = sender as pSystem.UI.ReaLTaiizor.Controls.RibbonCheckBox;
            var logTyp = (LogTyp)(checkBox.Tag);
            if (checkBox.Checked)
            {
                if (!_checkedLogtyp.Exists(x => x == logTyp))
                    _checkedLogtyp.Add(logTyp);
            }
            else
            {
                if (_checkedLogtyp.Exists(x => x == logTyp))
                    _checkedLogtyp.Remove(logTyp);
            }
            InvokeScript();
        }
        #endregion

        private void ObverseLogCtrl_Resize(object sender, EventArgs e)
        {
            AdjustButtonLocation();
        }

        /// <summary>
        /// 解绑事件
        /// </summary>
        public void UnbindEvents()
        {
            if (!_isCurrent)
                return;
            Logger.Instance.LogFileJustBeAppended -= this.Instance_LogFileJustBeAppended;
        }
    }
}
