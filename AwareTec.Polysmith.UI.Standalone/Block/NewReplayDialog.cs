using AwareTec.Polysmith.DataBaseCom;
using AwareTec.Polysmith.UI.DataModel;
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
using AwareTec.Polysmith.UI.FunctionControls.tools;
using AwareTec.Polysmith.Util.PathUtils;
using AwareTec.Polysmith.Util;

namespace AwareTec.Polysmith.UI.Block
{
    public partial class NewReplayDialog : SkinForm
    {
        #region 全局变量
        private string m_guid = "";
        private List<DataBaseCom.Doc_Doctor> _doctor = null;
        private DataBaseCom.Doc_PatientInfo _patient = null;
        public bool IsImportEDF = false;
        /// <summary>
        /// 是否是直接自动分析
        /// </summary>
        public bool AutoAnalysis = false;
        private string m_edfPath = "";

        #endregion

        #region 对外属性
        public string GUID
        {
            get
            {
                return m_guid;
            }
        }


        /// <summary>
        /// edf路径
        /// </summary>
        public string EdfPath
        {
            set
            {
                m_edfPath = value;
                BrowseButton.Visible = false;
                AutoSearchButton.Visible = false;
                DataSourceTextBox.Text = value;
            }
            get
            {
                return m_edfPath;
            }
        }
        #endregion

        #region 初始化相关
        public NewReplayDialog()
        {
            InitializeComponent();
            this.Load += NewReplayDialog_Load;
        }

        private void NewReplayDialog_Load(object sender, EventArgs e)
        {
            string[] strValue = new string[_doctor.Count];
            string[] strID = new string[_doctor.Count];
            for (int i = 0; i < _doctor.Count; i++)
            {
                strValue[i] = _doctor[i].Name;
                strID[i] = _doctor[i].UserID;
            }
            RaterComboBox.Items.AddRange(strValue);
            RaterComboBox.Tag = strID;
            RaterComboBox.SelectedIndex = 0;
            if (IsImportEDF)
                AutoSearchButton.Visible = false;
        }
        #endregion

        #region 重写
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                ConfirmButton_Click(null, null);
                return true;
            }
            else
                return base.ProcessDialogKey(keyData);
        }
        #endregion

        #region 所有控件的绑定
        /// <summary>
        /// 确认按钮点击时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            EDF.Default.Interrupt = false;
            string fpath = DataSourceTextBox.Text;
            if (RaterComboBox.SelectedItem == null)
            {
                AhDung.MessageTip.ShowWarning("评分人不能为空!");
                return;
            }
            if (fpath == "")
            {
                AhDung.MessageTip.ShowWarning("未选择有效的数据源!");
                return;
            }

            string id = (RaterComboBox.Tag as string[])[RaterComboBox.SelectedIndex];
            DataBaseCom.Doc_Doctor doctor = _doctor.Find(t => t.UserID == id);
            string guid = "";
            Channel.Default.Doctor = doctor;
            Channel.Default.Patient = _patient;
            this.Close();
            Channel.Default.RefreshHomeMenu();
            m_edfPath = fpath;
            DataModel.LogInstance.Default.AddLog(string.Format("用户点击 新建诊断-确定按钮，选择的评分人为 {0}，选择的数据源为 {1}", doctor.Name, m_edfPath));
            DataModel.AnalysisRecordViewData record = Tag as DataModel.AnalysisRecordViewData;
            if (!AutoAnalysis)
            {
                guid = Channel.Default.CreatGUID(_patient.ID, doctor.ID);
                if (IsImportEDF)
                {
                    if (record.Progress == ProgressState.None && string.IsNullOrEmpty(record.MatchKey))
                    {
                        DataModel.DataBaseHelper.Default.Update(new DataBaseCom.Doc_MainViewRecord() { ID = record.ID },
                                                                new DataBaseCom.Doc_MainViewRecord()
                                                                {
                                                                    PatientID = _patient.PatientNo,
                                                                    Progress = (int)DataModel.ProgressState.Temporary,
                                                                    DoctorID = doctor.ID.ToString(),
                                                                    EdfPath = fpath,
                                                                    GUID = guid,
                                                                    CreatTime = DateTime.Now,
                                                                    VideoHave = false,
                                                                    DifferentVersion=1,
                                                                    RecordTime = record.RecordTime,
                                                                    Reserve3=record.Reserve3,
                                                                    MatchKey = DataModel.DataBaseHelper.Default.ComputeSHA256(fpath),
                                                                });
                        Channel.Default.AnalysisReult.Tag = record.ID;
                    }
                    else
                    {
                        EDF edf = EDF.Default.ConvertToEDF(EDF.Default.ReadFile(fpath, false));
                        if (edf.getMatchKey().Contains(record.MatchKey) || edf.getMatchKey().Contains(record.MatchKey.Substring(0, record.MatchKey.Length - 1)))
                        {
                            DataBaseCom.Doc_MainViewRecord mvr = new DataBaseCom.Doc_MainViewRecord()
                            {
                                PatientID = _patient.PatientNo,
                                Progress = (int)DataModel.ProgressState.Temporary,
                                DoctorID = doctor.ID.ToString(),
                                EdfPath = fpath,
                                GUID = guid,
                                ReportReady = false,
                                RecordTime = DateTime.Now,
                                CreatTime = DateTime.Now,
                                //导入edf 出现新纪录
                                MatchKey = record.MatchKey,
                                Reserve3 = record.Reserve3,
                                Reserve5=record.ResultPath,
                                VideoHave = record.VideoHave,
                                DifferentVersion = 1,
                                LoginID = Channel.Default.Loginer.ID,
                                ModeType = Channel.Default.SystemSetting.ModeType
                            };
                            DataModel.DataBaseHelper.Default.Insert(mvr);
                            Channel.Default.AnalysisReult.Tag = DataModel.DataBaseHelper.Default.Select(new DataBaseCom.Doc_MainViewRecord() { GUID = mvr.GUID }).ID;
                        }
                        else
                        {
                            DataBaseCom.Doc_MainViewRecord mvr = new DataBaseCom.Doc_MainViewRecord()
                            {
                                PatientID = _patient.PatientNo,
                                Progress = (int)DataModel.ProgressState.Temporary,
                                DoctorID = doctor.ID.ToString(),
                                EdfPath = fpath,
                                GUID = guid,
                                ReportReady = false,
                                RecordTime = DateTime.Now,
                                CreatTime = DateTime.Now,
                                //导入edf 出现新纪录
                                MatchKey = edf.getMatchKey() == "" ? DataModel.DataBaseHelper.Default.ComputeSHA256(fpath) : edf.getMatchKey(),
                                Reserve3 = "",
                                VideoHave = false,
                                DifferentVersion = 1,
                                LoginID = Channel.Default.Loginer.ID,
                                ModeType = Channel.Default.SystemSetting.ModeType
                            };
                            DataModel.DataBaseHelper.Default.Insert(mvr);
                            Channel.Default.AnalysisReult.Tag = DataModel.DataBaseHelper.Default.Select(new DataBaseCom.Doc_MainViewRecord() { GUID = mvr.GUID }).ID;
                        }
                    }
                }
                else
                {
                    DataBaseCom.Doc_MainViewRecord mvr = new DataBaseCom.Doc_MainViewRecord()
                    {
                        PatientID = _patient.PatientNo,
                        Progress = (int)DataModel.ProgressState.Temporary,
                        DoctorID = doctor.ID.ToString(),
                        EdfPath = fpath,
                        GUID = guid,
                        ReportReady = false,
                        LoginID = Channel.Default.Loginer.ID,
                        ReviewMontageName = record.ReviewMontageName,
                        CreatTime = record.CreatTime,//新诊断进行赋值 videohave 跟着原有记录的值变化
                        VideoHave =record.VideoHave,
                        Reserve3=record.Reserve3,
                        DifferentVersion = 1,
                        MatchKey = record.MatchKey == "" ? DataModel.DataBaseHelper.Default.ComputeSHA256(fpath) : record.MatchKey,
                        ModeType = Channel.Default.SystemSetting.ModeType
                    };
                    if (record.GUID == "")///自动搜索导入分析
                    {
                        DataModel.DataBaseHelper.Default.Update(new Doc_MainViewRecord() { ID = record.ID }, mvr);
                    }
                    else///新诊断时调用
                    {
                        mvr.CreatTime = DateTime.Now;
                        mvr.VideoHave = record.VideoHave;
                        mvr.Reserve3 = record.Reserve3;
                        mvr.RecordTime = record.RecordTime ;
                        DataModel.DataBaseHelper.Default.Insert(mvr);
                    }
                    Channel.Default.AnalysisReult.Tag = DataModel.DataBaseHelper.Default.Select(new DataBaseCom.Doc_MainViewRecord() { GUID = mvr.GUID }).ID;
                }
            }
            else
            {
                Channel.Default.AnalysisReult.Tag = record.ID;
            }
            m_guid = guid;
            DialogResult = DialogResult.OK;
        }
        /// <summary>
        /// 自动搜索按钮点击时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoSearchButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new System.Windows.Forms.FolderBrowserDialog();
            string[] dirs = Environment.GetLogicalDrives(); //取得所有的盘符 
            int cnt = 0;
            AnalysisRecordViewData record = this.Tag as AnalysisRecordViewData;
            foreach (string dir in dirs)
            {
                DriveInfo Tdriver = new DriveInfo(dir);
                if (Tdriver.DriveType == DriveType.Removable)
                {
                    string[] files = Directory.GetFiles(dir, "*.edf");
                    for (int i = 0; i < files.Length; i++)
                    {
                        EDF edf = EDF.Default.ConvertToEDF(EDF.Default.ReadFile(files[i], false));
                        string savePath = EDF.Default.DefaultEdfSavePath;
                        // if (m_DataSource.Select(string.Format("PatientNO='{0}' and Progress={1}", edf.PatientNO, (int)ProgressState.Ready)).Length > 0)
                        if (edf.getMatchKey().Contains(record.MatchKey) || edf.getMatchKey().Contains(record.MatchKey.Substring(0, record.MatchKey.Length - 1)))
                        {
                            string path = System.IO.Path.Combine(savePath, string.Format("{0:yyyy-MM-dd}\\{1}", edf.StartTime, Path.GetFileName(files[i])));
                            ApiCopyFile.DoCopy(files[i], path, IntPtr.Zero);
                            DataSourceTextBox.Text = path;
                            AhDung.MessageTip.ShowOk("匹配成功");
                            DataModel.LogInstance.Default.AddLog(string.Format("用户点击 新建诊断-自动搜索 匹配成功，匹配路径为 {0}", StringPath.ConvertLogPath(path)), pSystem.LogManagement.LogLevel.WARN);
                            cnt++;
                            break;
                        }
                    }
                }
            }
            if (cnt == 0)
            {
                if (MessageForm.Show(Program.Language=="EN"? "No matching EDF files were found for the current case. Do you want to specify a path for searching" : "未发现与当前病例匹配的edf文件，是否指定路径搜索？", "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (folder.ShowDialog() == DialogResult.OK)
                    {
                        string[] files = Directory.GetFiles(folder.SelectedPath, "*.edf");
                        for (int i = 0; i < files.Length; i++)
                        {
                            EDF edf = EDF.Default.ConvertToEDF(EDF.Default.ReadFile(files[i], false));
                            string savePath = EDF.Default.DefaultEdfSavePath;
                            // if (m_DataSource.Select(string.Format("PatientNO='{0}' and Progress={1}", edf.PatientNO, (int)ProgressState.Ready)).Length > 0)
                            if (edf.getMatchKey().Contains(record.MatchKey) || edf.getMatchKey().Contains(record.MatchKey.Substring(0, record.MatchKey.Length - 1)))
                            {
                                string path = System.IO.Path.Combine(savePath, string.Format("{0:yyyy-MM-dd}\\{1}", DateTime.Now, Path.GetFileName(files[i])));
                                DataSourceTextBox.Text = path;
                                AhDung.MessageTip.ShowOk("匹配成功");
                                DataModel.LogInstance.Default.AddLog(string.Format("用户点击 新建诊断-自动搜索 按指定路径搜索匹配成功，匹配路径为 {0}", StringPath.ConvertLogPath(path)), pSystem.LogManagement.LogLevel.WARN);
                                cnt++;
                                break;
                            }
                        }
                        if (cnt == 0)
                        {
                            MessageForm.Show(Program.Language == "EN" ? "No matching EDF files were found for the current case, please manually select!" : "未发现与当前病例匹配的edf文件,请手动选择！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 浏览按钮点击时触发
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
                DataSourceTextBox.Text = open.FileName;
            }
        }

        /// <summary>
        /// 取消按钮点击时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            DataModel.LogInstance.Default.AddLog("用户点击 新建诊断-取消按钮");
            this.Close();
        }
        #endregion

        public void SetPatientAndDoctor(List<DataBaseCom.Doc_Doctor> doctor,DataBaseCom.Doc_PatientInfo patient)
        {
            _doctor = doctor;
            _patient = patient;
        }
        
    }
}
