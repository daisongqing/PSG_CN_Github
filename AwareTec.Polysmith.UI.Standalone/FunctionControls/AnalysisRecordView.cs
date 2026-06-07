using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AwareTec.Polysmith.DataBaseCom;
using AwareTec.Polysmith.UI.DataModel;
using System.IO;
using AwareTec.Polysmith.Util;
using System.Reflection;
using AwareTec.Polysmith.pChart;
using System.Runtime.InteropServices;
using System.Threading;
using pSystem.Interface.Util;
using Newtonsoft.Json;
using AwareTec.Polysmith.UI.Block;
using AwareTec.Polysmith.UI.FunctionControls.tools;
using Microsoft.VisualBasic.Devices;
using AwareTec.Polysmith.DataMangement;
using System.Text.RegularExpressions;
using AwareTec.Polysmith.Vedio;
using System.Diagnostics;
using ApiCopyFile = AwareTec.Polysmith.Util.ApiCopyFile;

namespace AwareTec.Polysmith.UI.FunctionControls
{
    public partial class AnalysisRecordView : UserControl
    {
        private ReportHelper aph = null;
        private bool m_init = false;
        private string m_strDoctorColumnName = "DoctorName";
        private string m_strProcessColumnName = "Progress";
        private string m_strPatientNoColumnName = "PatientNo";
        private string m_strMapColumnName = "Atlas";
        private string m_strReportColumnName = "Report";
        private string m_strGuidColumnName = "guid";
        private DataTable detailInfor = new DataTable();

        public AnalysisRecordView()
        {
            InitializeComponent();
            this.Load += AnalysisRecordView_Load;
            abSelect.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            StartDateTimePicker.Value = DateTime.Now.AddDays(-14);
            this.SizeChanged += AnalysisRecordView_SizeChanged;
            abSelect.CellClick += abSelect_CellClick;
            abSelect.CellEndEdit += abSelect_CellEndEdit;
            abSelect.CellBeginEdit += abSelect_CellBeginEdit;
            abSelect.CellDoubleClick += abSelect_CellDoubleClick;
            abSelect.CellMouseClick += abSelect_CellMouseClick;
            abSelect.EditingControlShowing += abSelect_EditingControlShowing;
            abSelect.MouseClick += AbSelect_MouseClick;
            this.MouseWheel += AnalysisRecordView_MouseWheel;
            aph = new ReportHelper();
            bak_height = abSelect.Height;
        }

        private void AbSelect_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && !loading&&abSelect.Rows.Count==0)
            {
               foreach(ToolStripItem item in  PatientCaseContextMenuStrip.Items)
                {
                    item.Visible = false;
                }
                ImportZipToolStripMenuItem.Visible = true;
                PatientCaseContextMenuStrip.Show(abSelect, abSelect.PointToClient(Cursor.Position));
            }
        }

        private void AnalysisRecordView_MouseWheel(object sender, MouseEventArgs e)
        {
            mainKeyDown(e.Delta > 0 ? Keys.Left : Keys.Right);
        }
        /// <summary>
        /// 按键按下时发生
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        public bool mainKeyDown(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                    RoundAngleButton1_Click(null, null);
                    return true;//不继续处理
                case Keys.Down:
                    RoundAngleButton4_Click(null, null);
                    return true;
                case Keys.Left:
                    RoundAngleButton2_Click(null, null);
                    return true;
                case Keys.Right:
                    RoundAngleButton3_Click(null, null);
                    return true;
                case Keys.Delete:
                    DeleteToolStripMenuItem_Click(null, null);
                    return true;
                case Keys.Enter:
                    SearchButton_Click(null, null);
                    break;
            }
            return false;
        }
        private void abSelect_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && !loading)
            {
                if (e.RowIndex < 0)
                    return;
                abSelect.EndEdit();
                foreach (ToolStripItem item in PatientCaseContextMenuStrip.Items)
                {
                    item.Visible = true;
                }
                ManualAnalysisToolStripMenuItem.Tag = null;
                AutoAnalysisToolStripMenuItem.Tag = null;
                ExportReportToolStripMenuItem.Tag = null;
                UnlockToolStripMenuItem.Tag = null;
                AutoSearchToolStripMenuItem.Tag = null;
                NewDiagnosisToolStripMenuItem.Tag = null;
                AutoAnalysisToolStripMenuItem.Visible = false;
                string state = abSelect.Rows[e.RowIndex].Cells[m_strProcessColumnName].Value.ToString();
                bool score = abSelect.Rows[e.RowIndex].Cells[m_strDoctorColumnName].Value == null ? false : abSelect.Rows[e.RowIndex].Cells[m_strDoctorColumnName].Value.ToString() != "--";
                UnlockToolStripMenuItem.Visible = false;
                AutoSearchToolStripMenuItem.Visible = false;
                ExportZipToolStripMenuItem.Visible = false;
                ExportZipToolStripMenuItem.Tag = null;
                LookedfToolStripMenuItem.Visible = false;
                LookedfToolStripMenuItem.Tag = null;
                LookVideoToolStripMenuItem.Visible = false;
                LoadDownVedioToolStripMenuItem.Visible = false;
                if (state == (Program.Language=="EN"? "To be Analysis" : "待分析"))
                {
                    AutoAnalysisToolStripMenuItem.Enabled = true;
                    ManualAnalysisToolStripMenuItem.Enabled = score;
                    ExportReportToolStripMenuItem.Enabled = false;
                    NewDiagnosisToolStripMenuItem.Enabled = false;
                    AutoSearchToolStripMenuItem.Visible = true;
                    AnalysisRecordViewData arv = abSelect.Rows[e.RowIndex].Tag as AnalysisRecordViewData;
                    AutoAnalysisToolStripMenuItem.Tag = arv;
                    AutoSearchToolStripMenuItem.Tag = arv;
                    if (!Directory.Exists(string.Format("{0}\\{1}", Channel.Default.SystemSetting.VedioSavePath, arv.MatchKey)) && arv.MatchKey != "")
                    {
                        LoadDownVedioToolStripMenuItem.Visible = true;
                        LoadDownVedioToolStripMenuItem.Tag = arv;
                    }
                }
                else if (state == (Program.Language == "EN" ? "Finished" : "已完成"))
                {
                    AutoAnalysisToolStripMenuItem.Enabled = false;
                    ManualAnalysisToolStripMenuItem.Enabled = false;
                    ExportReportToolStripMenuItem.Enabled = true;
                    NewDiagnosisToolStripMenuItem.Enabled = true;
                    NewDiagnosisToolStripMenuItem.Tag = abSelect.Rows[e.RowIndex].Tag;
                    UnlockToolStripMenuItem.Visible = true;
                    ExportReportToolStripMenuItem.Tag = abSelect.Rows[e.RowIndex].Tag;
                    UnlockToolStripMenuItem.Tag = e.RowIndex;
                    ExportZipToolStripMenuItem.Visible = true;
                    LookedfToolStripMenuItem.Visible = true;
                    LookedfToolStripMenuItem.Tag = abSelect.Rows[e.RowIndex].Tag;
                    ExportZipToolStripMenuItem.Tag= abSelect.Rows[e.RowIndex].Tag;
                }
                else if (state == (Program.Language == "EN" ? "To be Finish" : "待完成"))
                {
                    AutoAnalysisToolStripMenuItem.Enabled = false;
                    ManualAnalysisToolStripMenuItem.Enabled = score;
                    ExportReportToolStripMenuItem.Enabled = false;
                    NewDiagnosisToolStripMenuItem.Enabled = true;
                    NewDiagnosisToolStripMenuItem.Tag = abSelect.Rows[e.RowIndex].Tag;
                    ExportZipToolStripMenuItem.Visible = true;
                    LookedfToolStripMenuItem.Visible = true;
                    LookedfToolStripMenuItem.Tag = abSelect.Rows[e.RowIndex].Tag;
                    ExportZipToolStripMenuItem.Tag = abSelect.Rows[e.RowIndex].Tag;
                }
                else
                {
                    ManualAnalysisToolStripMenuItem.Enabled = false;//新建的病例 进度为none时，不允许手动分析
                    AutoAnalysisToolStripMenuItem.Enabled = false;
                    ExportReportToolStripMenuItem.Enabled = false;
                    NewDiagnosisToolStripMenuItem.Enabled = false;
                }
                if(LookedfToolStripMenuItem.Tag!=null)
                {
                    string matchkey = (LookedfToolStripMenuItem.Tag as AnalysisRecordViewData).MatchKey;
                    if (Directory.Exists(string.Format("{0}\\{1}", Channel.Default.SystemSetting.VedioSavePath, matchkey)))
                        LookVideoToolStripMenuItem.Visible = true;
                    else if(state!= (Program.Language == "EN" ? "Finished" : "已完成"))
                    {
                        LoadDownVedioToolStripMenuItem.Visible = true;
                        LoadDownVedioToolStripMenuItem.Tag= abSelect.Rows[e.RowIndex].Tag;
                    }
                    //string matchkey= (LookedfToolStripMenuItem.Tag as AnalysisRecordViewData).MatchKey;
                    //if (Directory.Exists(string.Format("{0}\\{1}", Channel.Default.SystemSetting.VedioSavePath, matchkey))|| Directory.Exists(string.Format("{0}\\{1}", Channel.Default.SystemSetting.VedioSavePath, matchkey.Substring(0, matchkey.Length - 1))))
                    //    LookVideoToolStripMenuItem.Visible = true;
                }

                //AutoAnalysis.Visible = false;///送检版本去掉自动分析
                abSelect.Rows[e.RowIndex].Selected = true;
                ManualAnalysisToolStripMenuItem.Tag = abSelect.Rows[e.RowIndex].Tag;
                PatientCaseContextMenuStrip.Tag = abSelect.Rows[e.RowIndex].Cells[m_strPatientNoColumnName].Value;
                PatientCaseContextMenuStrip.Show(abSelect, abSelect.PointToClient(Cursor.Position));
            }
        }
        public delegate void LoadRecordDelegate(AnalysisResult result, DataModel.ProgressState Complete);
        /// <summary>
        /// 数据加载
        /// </summary>
        public event LoadRecordDelegate LoadRecordHandle;
        private bool loading = false;
        /// <summary>
        /// 初始化montage
        /// </summary>
        /// <param name="reviewMontageName"></param>
        private void initMontage(string reviewMontageName)
        {
            string path = string.IsNullOrEmpty(reviewMontageName) ? Channel.Default.DefaultChannelPath : string.Format("{0}\\{1}", ChannelManage.Default.ConfigruationBasePath, reviewMontageName);
            Channel.Default.CurrentChannelPath = path;
            Channel.Default.CurrentChannelTable = ChannelManage.Default.ReadChannelConfig(Channel.Default.CurrentChannelPath);
        }
        private void abSelect_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (loading)
                {
                    AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "Loading not completed, please wait..." : "加载未完成，请等待...");
                    return;
                }
                object strvalue = abSelect.Rows[e.RowIndex].Cells[m_strDoctorColumnName].Value;
                if (strvalue == null)
                    return;
                AnalysisRecordViewData record = (abSelect.Rows[e.RowIndex].Tag as AnalysisRecordViewData);
                if (record.Progress == ProgressState.None)
                {
                    AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "The cases to be monitored cannot start scoring!" : "待监测的病例无法开始评分！");
                    return;
                }
                if (strvalue.ToString() != "--")
                {
                    loading = true;
                    Channel.Default.Patient = DataModel.DataBaseHelper.Default.Select<Doc_PatientInfo>(new Doc_PatientInfo() { PatientNo = record.PatientNo });
                    bool hasdata = Convert.ToBoolean(abSelect.Rows[e.RowIndex].Cells[m_strProcessColumnName].Tag);
                    if (!hasdata)
                    {
                        if (!string.IsNullOrEmpty(record.EdfPath))
                        {
                            hasdata = true;
                            //如果通过右上角自动搜索后，在首页记录选择医生进入回放，则删除 waitlist对应的记录，保持一致
                            if(m_waitforRecords.Exists(t=>t.MatchKey==record.MatchKey))
                            {
                                m_waitforRecords.Remove(m_waitforRecords.Find(t => t.MatchKey == record.MatchKey));
                            }
                        }
                    }

                    if (hasdata)
                    {
                        Task.Factory.StartNew(() =>
                        {
                            AnalysisResult result = new AnalysisResult() { ReadFromdb = true, GUID = record.GUID };
                            result.EdfPath = record.EdfPath;
                            result.Tag = record.ID;
                            if (result.EdfPath.Contains(".."))
                            {
                                result.EdfPath = result.EdfPath.Replace("..", Application.StartupPath);
                            }
                            ////数据源加载入口
                            if (LoadRecordHandle != null)
                            {
                                if (File.Exists(result.EdfPath))
                                {
                                    Channel.Default.Doctor = m_DoctorList.Find(t => t.UserID == record.DoctorID);
                                    initMontage(record.ReviewMontageName);
                                    LoadRecordHandle.Invoke(result, record.Progress);
                                    Channel.Default.MatchKey = record.MatchKey;
                                    if (record.Progress == ProgressState.Ready)
                                    {
                                        m_waitforRecords.RemoveAll(t => t.ID == record.ID);
                                        bak_records.RemoveAll(t => t.ID == record.ID);
                                        m_recordList = AnalysisRecordViewData.ConvertEntity(DataModel.DataBaseHelper.Default.LoadMainView());
                                        m_TotalCount = bak_records.Count;
                                        if (m_TotalCount == 0)
                                        {
                                            RoundAngleButton6_Click(null, null);
                                        }
                                    }
                                }
                                else
                                {
                                    AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "Unable to find data source (EDF file)" : "找不到数据源(edf文件)");
                                    DataModel.LogInstance.Default.AddLog(string.Format("用户双击状态为 {3} 病例，病例号 {0},病例记录时间为 {1}，评分人为 {2}，找不到数据源(edf文件)", record.PatientNo, record.RecordTime.ToString("G"), record.DoctorName, abSelect.Rows[e.RowIndex].Cells[m_strProcessColumnName].Value.ToString()), pSystem.LogManagement.LogLevel.ERROR);
                                }
                            }
                        });
                        DataModel.LogInstance.Default.AddLog(string.Format("用户双击状态为 {3} 病例，病例号 {0},病例记录时间为 {1}，评分人为 {2}，进入历史回放页面", record.PatientNo, record.RecordTime.ToString("G"),record.DoctorName,abSelect.Rows[e.RowIndex].Cells[m_strProcessColumnName].Value.ToString()));
                    }
                    else
                    {
                        ImportHistroyDialog importHistroyDialog = new ImportHistroyDialog();
                        importHistroyDialog.ID = record.PatientNo;
                        importHistroyDialog.RecordTime = record.RecordTime;
                        Channel.Default.MatchKey = record.MatchKey;
                        importHistroyDialog.ShowDialog();
                        {
                            if (importHistroyDialog.DialogResult == DialogResult.OK)
                            {
                                record.EdfPath = importHistroyDialog.EdfPath;
                                if (LoadRecordHandle != null)
                                {
                                    LoadRecordHandle.Invoke(new AnalysisResult() { Tag = record.ID, EdfPath = record.EdfPath }, ProgressState.Ready);
                                }
                            }
                        }
                    }
                    loading = false;
                }
                else
                {
                    AhDung.MessageTip.ShowWarning(Program.Language=="EN"? "Please choose a doctor" : "请选择评分人");
                }
            }
        }

        private void abSelect_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (abSelect.Columns[e.ColumnIndex].Name == m_strDoctorColumnName && abSelect.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewTextBoxCell)
            {
                e.Cancel = Convert.ToBoolean(abSelect.Rows[e.RowIndex].Cells[m_strProcessColumnName].Tag);
            }
        }

        //保留对编辑comtrol的引用：
        ComboBox combo = null;

        //填充引用，一旦它有效：
        private void abSelect_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            combo = e.Control as ComboBox;
        }
        private void abSelect_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (abSelect.Columns[e.ColumnIndex].Name == m_strDoctorColumnName && abSelect.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewComboBoxCell)
            {
                DataGridViewTextBoxCell cb = new DataGridViewTextBoxCell();
                cb.Value = abSelect.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null ? "--" : abSelect.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (combo != null)
                {
                    DataGridViewComboBoxCell comboBoxEditingControl = abSelect.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell;
                    if (combo.SelectedIndex >= 0)
                    {
                        string ss = (comboBoxEditingControl.Tag as string[])[combo.SelectedIndex];
                        Channel.Default.Doctor = m_DoctorList.Find(t => t.UserID == ss);
                        AnalysisRecordViewData record = (abSelect.Rows[e.RowIndex].Tag as AnalysisRecordViewData);
                        record.DoctorID = Channel.Default.Doctor.UserID;
                        record.DoctorName = Channel.Default.Doctor.Name;
                    }
                }
                try
                {
                    abSelect.Rows[e.RowIndex].Cells[e.ColumnIndex] = cb;
                }
                catch { }
            }
        }

        private void abSelect_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            if (abSelect.Columns[e.ColumnIndex].Name == m_strDoctorColumnName && this.abSelect.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewTextBoxCell)
            {
                if (!Convert.ToBoolean(this.abSelect.Rows[e.RowIndex].Cells[m_strProcessColumnName].Tag))
                {
                    DataGridViewComboBoxCell dataGridViewComboBoxCell = new DataGridViewComboBoxCell();
                    string[] array = new string[this.m_DoctorList.Count];
                    string[] array2 = new string[this.m_DoctorList.Count];
                    for (int i = 0; i < this.m_DoctorList.Count; i++)
                    {
                        array[i] = this.m_DoctorList[i].Name;
                        array2[i] = this.m_DoctorList[i].UserID;
                    }
                    dataGridViewComboBoxCell.Items.AddRange(array);
                    dataGridViewComboBoxCell.Tag = array2;
                    dataGridViewComboBoxCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                    this.abSelect.Rows[e.RowIndex].Cells[e.ColumnIndex] = dataGridViewComboBoxCell;
                    DataGridViewComboBoxEditingControl dataGridViewComboBoxEditingControl = this.abSelect.EditingControl as DataGridViewComboBoxEditingControl;
                    if (dataGridViewComboBoxEditingControl != null)
                    {
                        dataGridViewComboBoxEditingControl.DroppedDown = true;
                        return;
                    }
                }
            }
            else if (abSelect.Columns[e.ColumnIndex].Name == m_strMapColumnName)
            {
                if (Convert.ToBoolean(this.abSelect.Rows[e.RowIndex].Cells[m_strProcessColumnName].Tag) && this.abSelect.Rows[e.RowIndex].Cells[m_strMapColumnName].Value.ToString() != "--")
                {
                    DataModel.LogInstance.Default.AddLog(string.Format("用户点击 病例列表中的图谱列的查看按钮，当前的病例号为{0}", this.abSelect.Rows[e.RowIndex].Cells[0].Value));
                    Moudle moudle = new Moudle();
                    moudle.ControlBox = true;
                    moudle.MaximizeBox = true;
                    moudle.CanResize = true;
                    moudle.Text =Program.Language=="EN"? "Trend Chart Preview" : "图谱预览";
                    moudle.Size = new Size(900, 1000);
                    tools.MulReportChart mulReportChart = new tools.MulReportChart();
                    mulReportChart.Dock = DockStyle.Fill;
                    AnalysisRecordViewData analysisRecordViewData = this.abSelect.Rows[e.RowIndex].Tag as AnalysisRecordViewData;
                    AnalysisResult analysisResult = new DataModel.DataManger().ReadResult(analysisRecordViewData.ResultPath, analysisRecordViewData.GUID);
                    analysisResult.StartTime = analysisRecordViewData.RecordTime;
                    analysisResult.EndTime = analysisRecordViewData.RecordTime.AddSeconds((double)(analysisRecordViewData.FrameCount * 30));
                    ResultDomain result = analysisResult.ConvertResultDomain(analysisRecordViewData.FrameCount, false);
                    string reserve = DataBaseHelper.Default.Select<Doc_MainViewRecord>(new Doc_MainViewRecord
                    {
                        ID = analysisRecordViewData.ID
                    }).Reserve1;
                    bool isbreathOnly = false;
                    if (!string.IsNullOrEmpty(reserve))
                    {
                        string[] array3 = reserve.Split(new char[]
                        {
                            ';'
                        });
                        isbreathOnly = bool.Parse(array3[1]);
                    }
                    mulReportChart.LoadData(result, isbreathOnly);
                    mulReportChart.CurrentFrameNo = 1;
                    moudle.Controls.Add(mulReportChart);
                    moudle.ShowDialog();
                    return;
                }
            }
            else if (abSelect.Columns[e.ColumnIndex].Name == m_strReportColumnName)
            {
                DataModel.LogInstance.Default.AddLog(string.Format("用户点击 病例列表中的报告列的查看按钮，当前的病例号为{0}", this.abSelect.Rows[e.RowIndex].Cells[0].Value));
                if (Convert.ToBoolean(this.abSelect.Rows[e.RowIndex].Cells[m_strProcessColumnName].Tag) && this.abSelect.Rows[e.RowIndex].Cells[m_strReportColumnName].Value.ToString() != "--")
                {
                    Task.Factory.StartNew(() =>
                    {
                        AnalysisRecordViewData analysisRecordViewData2 = this.abSelect.Rows[e.RowIndex].Tag as AnalysisRecordViewData;
                        string reserve2 = DataBaseHelper.Default.Select<Doc_MainViewRecord>(new Doc_MainViewRecord
                        {
                            ID = analysisRecordViewData2.ID
                        }).Reserve1;
                        bool flag = false;
                        string a = "pdf";
                        string text2;
                        if (!string.IsNullOrEmpty(reserve2))
                        {
                            string[] array4 = reserve2.Split(new char[]
                            {
                                ';'
                            });
                            flag = bool.Parse(array4[1]);
                            a = array4[2].ToLower();
                            string[] array5 = array4[3].Split(new char[]
                            {
                                '/'
                            });
                            text2 = string.Format("{0}ReportTemplate\\{1}\\{2}", AppDomain.CurrentDomain.BaseDirectory, flag ? "Breath" : "Sleep", flag ? array5[1] : array5[0]);
                        }
                        else
                        {
                            if (Channel.Default.SystemSetting.ReportType != "")
                            {
                                a = Channel.Default.SystemSetting.ReportType.ToLower();
                            }
                            text2 = Channel.Default.ReportTemplatePath;
                        }
                        AnalysisResult analysisResult2 = new DataModel.DataManger().ReadResult(analysisRecordViewData2.ResultPath, analysisRecordViewData2.GUID);
                        analysisResult2.StartTime = analysisRecordViewData2.RecordTime;
                        analysisResult2.EndTime = analysisRecordViewData2.RecordTime.AddSeconds((double)(analysisRecordViewData2.FrameCount * 30));
                        analysisResult2.Sleep.TotalRecordTime = (float)Math.Round((analysisResult2.EndTime - analysisResult2.StartTime).TotalMinutes, 2);
                        if (analysisResult2.Sleep.TotalRecordTime < analysisResult2.Sleep.TimeInBed)
                            analysisResult2.Sleep.TotalRecordTime = analysisResult2.Sleep.TimeInBed;
                        //保存分析直接返回首页查看报告 开灯关灯时间没有赋值
                        if (analysisResult2.Sleep.LightOnTime.Year==1)
                        {
                            analysisResult2.Sleep.LightOnTime = analysisResult2.EndTime;
                            analysisResult2.Sleep.LightOffTime = analysisResult2.StartTime;
                        }
                        List<string> list = new List<string>();
                        List<string> list2 = new List<string>();
                        Doc_PatientInfo doc_PatientInfo = DataBaseHelper.Default.Select<Doc_PatientInfo>(new Doc_PatientInfo
                        {
                            PatientNo = analysisRecordViewData2.PatientNo
                        });
                        doc_PatientInfo.RecordTime = analysisRecordViewData2.RecordTime;
                        doc_PatientInfo.DoctorName = analysisRecordViewData2.DoctorName;
                        ITable[] array6 = new ITable[]
                        {
                            doc_PatientInfo,
                            analysisResult2.BloodOxygen,
                            analysisResult2.BodyMovement,
                            analysisResult2.BodyState,
                            analysisResult2.BreathEvent,
                            analysisResult2.HeartRate,
                            analysisResult2.Sleep
                        };
                        for (int j = 0; j < array6.Length; j++)
                        {
                            PropertyInfo[] properties = array6[j].GetType().GetProperties();
                            for (int k = 0; k < properties.Length; k++)
                            {
                                list.Add(properties[k].Name);
                                object value = properties[k].GetValue(array6[j], null);
                                list2.Add((value != null) ? value.ToString() : "");
                            }
                        }
                        ResultDomain result2 = analysisResult2.ConvertResultDomain(analysisRecordViewData2.FrameCount, false);
                        Channel.Default.Patient = doc_PatientInfo;
                        Image image = Channel.Default.CreatMap(result2, flag);
                        image.Save(doc_PatientInfo.ResultPhoto);
                        string savePath = Application.StartupPath + "\\TemPlate\\bak_report.pdf";
                        PdfViewDialog m_PdfViewDialog = null;
                        if (a != "excel")
                        {
                            this.aph.CreateNewWordDocument(text2);
                            this.aph.SaveAs(list, list2, savePath, ReportHelper.DocumentFormat.Pdf, delegate (bool t)
                            {
                                m_PdfViewDialog = new PdfViewDialog
                                {
                                    Text = Program.Language==""? "Report Preview" : "报告预览",
                                    Encryption = false,
                                    PdfPath = savePath
                                };
                                this.Invoke(new MethodInvoker(() =>
                                {
                                    m_PdfViewDialog.ShowDialog();
                                }));
                            });
                            return;
                        }
                        else
                        {
                            savePath = Application.StartupPath + "\\TemPlate\\预览报告.xls";
                            NPOI.SS.UserModel.IWorkbook workbook = Util.ExcelUtils.ExcelHelper.SetValues(text2, list, list2);
                            Util.ExcelUtils.ExcelHelper.SaveExcel(savePath, workbook);
                            System.Diagnostics.Process.Start(savePath);
                        }
                        //ExcelTemplate excelTemplate = new ExcelTemplate();
                        //excelTemplate.Open(text2);
                        //excelTemplate.SetValues(list, list2);
                        //excelTemplate.SaveAs(savePath, delegate (bool t)
                        //{
                        //    m_PdfViewDialog = new PdfViewDialog
                        //    {
                        //        Text = "报告预览",
                        //        Encryption = false,
                        //        PdfPath = savePath
                        //    };
                        //    this.Invoke(new MethodInvoker(() =>
                        //    {
                        //        m_PdfViewDialog.ShowDialog();
                        //    }));
                        //});
                    });
                }
            }
        }
        private int bak_height = 0;
        private List<AnalysisRecordViewData> m_waitforRecords = new List<AnalysisRecordViewData>();
        private void AnalysisRecordView_SizeChanged(object sender, EventArgs e)
        {
            this.m_OnePageCount = (this.abSelect.Height - this.abSelect.ColumnHeadersHeight) / this.abSelect.RowTemplate.Height;
            if (this.bak_height != this.abSelect.Height && this.abSelect.Height > 0)
            {
                this.bak_height = this.abSelect.Height;
                if (!this.m_init)
                {
                    this.m_recordList = AnalysisRecordViewData.ConvertEntity(DataBaseHelper.Default.LoadMainView());
                    this.m_TotalCount = this.m_recordList.Count;
                    this.m_init = true;
                    this.bak_records = this.m_recordList;
                    this.m_waitforRecords = AnalysisRecordViewData.ConvertEntity(DataBaseHelper.Default.LoadMainView(ProgressState.Ready));
                    this.showTaskTip();
                }
                this.m_TotalPage = ((this.m_TotalCount % this.m_OnePageCount == 0) ? (this.m_TotalCount / this.m_OnePageCount) : (this.m_TotalCount / this.m_OnePageCount + 1));
                this.m_PageIndex = 1;
                this.LoadData(this.bak_records, this.m_PageIndex);
            }
            this.reFreshPagelable();
        }
        private List<AnalysisRecordViewData> bak_records = null;
        /// <summary>
        /// 当前页索引编号
        /// </summary>
        private int m_PageIndex = 0;
        /// <summary>
        /// 总页数
        /// </summary>
        private int m_TotalPage = 0;
        /// <summary>
        /// 当前页的最大数据
        /// </summary>
        private int m_OnePageCount = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        private int m_TotalCount = 0;
        private List<Doc_Doctor> m_DoctorList = null;
        private void AnalysisRecordView_Load(object sender, EventArgs e)
        {
            LoadDoctors();
            StartDateTimePicker.Value = DateTime.Now.AddDays(-7);
            EndDateTime.Value = DateTime.Now;
            this.VisibleChanged += AnalysisRecordView_VisibleChanged;
            Channel.Default.UpdateRecordHandle += Default_UpdateRecordHandle;
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="guid"></param>
        private void Default_UpdateRecordHandle(int id)
        {
            if (this.InvokeRequired)
            {
                while (!this.IsHandleCreated)
                {
                    if (this.Disposing || this.IsDisposed)
                    {
                        return;
                    }
                }
                this.Invoke(new MethodInvoker(() =>
                {
                    Default_UpdateRecordHandle(id);
                }));
            }
            else
            {
                if (id == -1)
                {
                    RefreshData();///id等于-1时刷新列表
                }
                Doc_MainViewRecord record = DataBaseHelper.Default.Select<Doc_MainViewRecord>(new Doc_MainViewRecord
                {
                    ID = id
                });
                if (record == null)
                    return;
                AnalysisRecordViewData find = this.m_recordList.Find((AnalysisRecordViewData t) => t.ID == id);
                if (find != null)
                {
                    find.Progress = (ProgressState)Enum.Parse(typeof(ProgressState), record.Progress.ToString());
                    string doctorName = "";
                    if (!string.IsNullOrEmpty(record.DoctorID))
                    {
                        int docterid = int.Parse(record.DoctorID);
                        Doc_Doctor docter = this.m_DoctorList.Find((Doc_Doctor t) => t.ID == docterid);
                        if (docter != null)
                        {
                            find.DoctorID = docter.UserID;
                            doctorName = docter.Name;
                        }
                    }
                    find.EdfPath = record.EdfPath;
                    find.CreatTime = record.CreatTime;
                    find.VideoHave = record.VideoHave;
                    find.DifferentVersion = record.DifferentVersion;
                    find.MatchKey = record.MatchKey;
                    find.Reserve3 = record.Reserve3;
                    find.ResultPath = record.Reserve5;
                    find.FrameCount = record.FrameCount;
                    for (int i = 0; i < this.abSelect.Rows.Count; i++)
                    {
                        if ((this.abSelect.Rows[i].Tag as AnalysisRecordViewData).ID == id)
                        {
                            string text = "--";
                            string value = "--";
                            Bitmap bitmap = new Bitmap(16, 16);
                            bitmap.MakeTransparent();
                            bool flag = true;
                            ProgressState progress = find.Progress;
                            switch (progress)
                            {
                                case ProgressState.None:
                                    find.DoctorName = "--";
                                    bitmap = Properties.Resources.pCreat;
                                    break;
                                case ProgressState.Ready:
                                    bitmap = Properties.Resources.pReady;
                                    value = (Program.Language == "EN" ? "To be Analysis" : "待分析");
                                    break;
                                case ProgressState.Running:
                                    bitmap = Properties.Resources.running;
                                    value = Program.Language == "EN" ? "Analysising" : "分析中";
                                    find.DoctorName = doctorName;
                                    flag = false;
                                    break;
                                default:
                                    if (progress != ProgressState.Temporary)
                                    {
                                        if (progress == ProgressState.Compelet)
                                        {
                                            bitmap = Properties.Resources.complete;
                                            value = Program.Language == "EN" ? "Finished" : "已完成";
                                            find.DoctorName = doctorName;
                                            //保存分析之后直接预览，可能会存在ResultPath为空
                                            find.ResultPath = record.Reserve5;
                                            flag = false;
                                            text = Program.Language=="EN"?"View":"查看";
                                        }
                                    }
                                    else
                                    {
                                        bitmap = Properties.Resources.temporary;
                                        find.DoctorName = doctorName;
                                        value = Program.Language == "EN" ? "To be Finish" : "待完成";
                                        flag = false;
                                    }
                                    break;
                            }
                            this.abSelect.Rows[i].Cells[m_strProcessColumnName].Value = value;
                            this.abSelect.Rows[i].Cells[m_strGuidColumnName].Value = (find.GUID = record.GUID);
                            LinkAndImageCell linkAndImageCell = this.abSelect.Rows[i].Cells[m_strProcessColumnName] as LinkAndImageCell;
                            linkAndImageCell.Image = bitmap;
                            linkAndImageCell.ImageAlignmentMode = LinkAndImageCell.ImageAlignment.Center;
                            this.abSelect.Rows[i].Cells[m_strDoctorColumnName].Value = find.DoctorName;
                            this.abSelect.Rows[i].Cells[m_strProcessColumnName].Tag = !flag;
                            this.abSelect.Rows[i].Cells["VideoHave"].Value = find.VideoHave ? (Program.Language == "EN" ? "Y" : "有") : "--";
                            this.abSelect.Rows[i].Tag = find;
                            if (text != "--")
                            {
                                this.abSelect.Rows[i].Cells[m_strMapColumnName] = new DataGridViewLinkCell
                                {
                                    Value = text
                                };
                                this.abSelect.Rows[i].Cells[m_strReportColumnName] = new DataGridViewLinkCell
                                {
                                    Value = text
                                };
                            }
                            this.abSelect.InvalidateRow(i);
                            return;
                        }
                    }
                    return;
                }
                List<AnalysisRecordViewData> newdata = AnalysisRecordViewData.ConvertEntity(DataBaseHelper.Default.LoadMainView(record.ID));
                this.m_recordList.InsertRange(0, newdata);
                for (int s = 0; s < newdata.Count; s++)
                {
                    if (newdata[s].Progress == ProgressState.Ready)
                    {
                        m_waitforRecords.Add(newdata[s]);
                    }
                }
                this.m_TotalCount = this.m_recordList.Count;
                this.m_TotalPage = ((this.m_TotalCount % this.m_OnePageCount == 0) ? (this.m_TotalCount / this.m_OnePageCount) : (this.m_TotalCount / this.m_OnePageCount + 1));
                this.m_PageIndex = 1;
                this.LoadData(this.m_recordList, this.m_PageIndex);
                this.reFreshPagelable();
                this.bak_records = this.m_recordList;
            }
        }

        private void AnalysisRecordView_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible && Channel.Default.HomePageRefresh)
            {
                m_recordList = AnalysisRecordViewData.ConvertEntity(DataModel.DataBaseHelper.Default.LoadMainView());
                m_TotalCount = m_recordList.Count;
                m_TotalPage = m_TotalCount % m_OnePageCount == 0 ? m_TotalCount / m_OnePageCount : m_TotalCount / m_OnePageCount + 1;
                LoadData(m_recordList, m_PageIndex);
                reFreshPagelable();
                bak_records = m_recordList;
                Channel.Default.HomePageRefresh = false;
            }
        }
        /// <summary>
        /// 加载评分人信息相关
        /// </summary>
        private void LoadDoctors()
        {
            m_DoctorList = DataModel.DataBaseHelper.Default.Select<Doc_Doctor>();
            m_DoctorList.RemoveAll(t => t.ID == 1);
            string[] docs = new string[m_DoctorList.Count + 1];
            DocNameCombBox.Items.Clear();
            docs[0] = "";
            for (int i = 0; i < m_DoctorList.Count; i++)
                docs[i + 1] = string.Format("{0}({1})", m_DoctorList[i].Name, m_DoctorList[i].UserID);
            DocNameCombBox.Items.AddRange(docs);
        }
        /// <summary>
        /// 数据刷新
        /// </summary>
        public void RefreshData()
        {
            LoadDoctors();
            if (!panel4.Visible)
            {
                panel4.Visible = true;
                showTaskTip();
            }
            m_recordList = AnalysisRecordViewData.ConvertEntity(DataModel.DataBaseHelper.Default.LoadMainView());
            m_TotalCount = m_recordList.Count;
            m_TotalPage = m_TotalCount % m_OnePageCount == 0 ? m_TotalCount / m_OnePageCount : m_TotalCount / m_OnePageCount + 1;
            m_PageIndex = 1;
            LoadData(m_recordList, m_PageIndex);
            reFreshPagelable();
            bak_records = m_recordList;
        }
        /// <summary>
        /// 修改病例信息
        /// </summary>
        /// <param name="info"></param>
        public void ChangePatient(Doc_PatientInfo info)
        {
            int startIndex = (m_PageIndex - 1) * m_OnePageCount;
            int len = m_TotalCount - startIndex;
            len = m_OnePageCount > len ? m_recordList.Count : startIndex + m_OnePageCount;
            for (int i = 0; i < m_recordList.Count; i++)
            {
                AnalysisRecordViewData record = m_recordList[i];
                if (record.PatientNo == info.PatientNo)
                {
                    record.PatientName = info.PatientName;
                    record.Height = info.Height;
                    record.Weight = info.Weight;
                    record.Sex = info.Gender;
                    record.Age = info.Age;
                    if (i >= startIndex && i < len)
                    {
                        int rowidx = i - startIndex;
                        abSelect.Rows[rowidx].Cells["PatientName"].Value = info.PatientName;
                        abSelect.Rows[rowidx].Cells["Sex"].Value = info.Gender;
                        abSelect.Rows[rowidx].Cells["Age"].Value = info.Age;
                        abSelect.Rows[rowidx].Cells["strBMI"].Value = info.BMI;
                        abSelect.InvalidateRow(rowidx);
                    }
                }
            }
        }

        /// <summary>
        /// 更新标签信息
        /// </summary>
        private void reFreshPagelable()
        {
            label2.Text = m_TotalCount.ToString();
            label4.Text = m_OnePageCount.ToString();
            label6.Text = m_TotalPage.ToString();
            PageTextBox.Text = m_PageIndex.ToString();
        }
        private List<AnalysisRecordViewData> m_recordList = new List<AnalysisRecordViewData>();
        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="dt"></param>
        private void LoadData(List<AnalysisRecordViewData> recorddatas, int pageidx)
        {
            abSelect.Rows.Clear();
            int startIndex = (pageidx - 1) * m_OnePageCount;
            int len = m_TotalCount - startIndex;
            recorddatas.Sort((record, r) => r.CreatTime.CompareTo(record.CreatTime));//按创建时间降序
            len = m_OnePageCount > len ? recorddatas.Count : startIndex + m_OnePageCount;
            int rowidx = 0;
            abSelect.RowIndexOffSet = startIndex;
            for (int i = startIndex; i < len; i++)
            {
                AnalysisRecordViewData arv = recorddatas[i];
                string des = "--";
                string gp = "--";
                string strDoctor = "--";
                Bitmap img = new Bitmap(16, 16);
                img.MakeTransparent();
                bool doctorchange = true;
                switch (arv.Progress)
                {
                    case ProgressState.None:
                        arv.DoctorName = "--";
                        img = Properties.Resources.pCreat;
                        break;
                    case ProgressState.Ready:
                        img = Properties.Resources.pReady;
                        gp = Program.Language == "EN" ? "To be Analysis" : "待分析";
                        break;
                    case ProgressState.Compelet:
                        img = Properties.Resources.complete;
                        gp = Program.Language == "EN" ? "Finished" : "已完成";
                        strDoctor = arv.DoctorName;
                        doctorchange = false;
                        des = Program.Language == "EN" ? "View" : "查看";
                        break;
                    case ProgressState.Running:
                        img = Properties.Resources.running;
                        gp = Program.Language == "EN" ? "Analysising" : "分析中";
                        strDoctor = arv.DoctorName;
                        doctorchange = false;
                        break;
                    case ProgressState.Temporary:
                        img = Properties.Resources.temporary;
                        strDoctor = arv.DoctorName;
                        gp = Program.Language == "EN" ? "To be Finish" : "待完成";
                        doctorchange = false;
                        break;
                }
                abSelect.Rows.Add(arv.PatientNo, arv.PatientName, arv.Sex, arv.Age, arv.BMI, arv.RecordTime, gp, strDoctor, des, des, arv.GUID, arv.CreatTime, arv.VideoHave ?(Program.Language=="EN"?"Y":"有") : "--");
                LinkAndImageCell lc = abSelect.Rows[rowidx].Cells[m_strProcessColumnName] as LinkAndImageCell;
                lc.Image = img;
                lc.ImageAlignmentMode = LinkAndImageCell.ImageAlignment.Center;

                lc.Style.Padding = new Padding(50, 0, 0, 0);

                if (arv.Progress == ProgressState.Compelet)
                {
                    lc.Style.Padding = new Padding(13, 0, 0, 0);
                } else
                    if (arv.Progress == ProgressState.Temporary)
                {
                    lc.Style.Padding = new Padding(40, 0, 0, 0);
                }

                Bitmap bitmap2 = new Bitmap(10, 10);
                bitmap2.MakeTransparent();
                if (arv.Reserve4 == "True")
                {
                    bitmap2 = Properties.Resources.SetTimeOK;
                }
                LinkAndImageCell linkAndImageCell2 = this.abSelect.Rows[rowidx].Cells[m_strPatientNoColumnName] as LinkAndImageCell;
                linkAndImageCell2.Image = bitmap2;
                linkAndImageCell2.ImageAlignmentMode = LinkAndImageCell.ImageAlignment.AlignLeft;
                abSelect.Rows[rowidx].Cells[m_strProcessColumnName].Tag = !doctorchange;
                abSelect.Rows[rowidx].Tag = arv;
                if (des != "--")
                {
                    abSelect.Rows[rowidx].Cells[m_strMapColumnName] = new DataGridViewLinkCell() { Value = des };
                    abSelect.Rows[rowidx].Cells[m_strReportColumnName] = new DataGridViewLinkCell() { Value = des };
                }
                rowidx++;
            }
        }

        private void RoundAngleButton1_Click(object sender, EventArgs e)
        {
            if (m_PageIndex != 1)
            {
                m_PageIndex = 1;
                LoadData(bak_records, m_PageIndex);
                reFreshPagelable();
            }
        }

        private void RoundAngleButton2_Click(object sender, EventArgs e)
        {
            if (m_PageIndex > 1)
            {
                m_PageIndex -= 1;
                LoadData(bak_records, m_PageIndex);
                reFreshPagelable();
            }
        }

        private void RoundAngleButton3_Click(object sender, EventArgs e)
        {
            if (m_PageIndex < m_TotalPage)
            {
                m_PageIndex += 1;
                LoadData(bak_records, m_PageIndex);
                reFreshPagelable();
            }
        }

        private void RoundAngleButton4_Click(object sender, EventArgs e)
        {
            if (m_PageIndex != m_TotalPage)
            {
                m_PageIndex = m_TotalPage;
                LoadData(bak_records, m_PageIndex);
                reFreshPagelable();
            }
        }

        private void PatientNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataModel.InputTextCheck.name_KeyPress(sender, e);
        }

        private void PageTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataModel.InputTextCheck.phone_KeyPress(sender, e);
        }

        private bool ShowMessage(string msg)
        {
            object ss = this.Invoke(new MessageBoxShow(MessageBoxShow_F), new object[] { msg });
            return Convert.ToBoolean(ss);
        }
        delegate bool MessageBoxShow(string msg);
        bool MessageBoxShow_F(string msg)
        {
            return MessageForm.Show(msg, Program.Language == "EN" ? "Message" : "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes;
        }
        private void search_SearchRecordsEventHandle(List<AnalysisRecordViewData> records)
        {
            if (records.Count > 0)
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    panel4.Visible = false;
                    planWork.Visible = false;
                    bak_records = records;
                    m_TotalCount = bak_records.Count;
                    m_TotalPage = m_TotalCount % m_OnePageCount == 0 ? m_TotalCount / m_OnePageCount : m_TotalCount / m_OnePageCount + 1;
                    ////数据更新到表单中
                    LoadData(records, 1);
                    reFreshPagelable();
                }));
                //自动搜索到了记录之后不管是否返回，在首页记录里面该记录应该只需要选择医生，就可以查看历史回放。这里更新m_recordlist，达到效果
                Dictionary<ITable, ITable> list = new Dictionary<ITable, ITable>();
                for (int i = 0; i < records.Count; i++)
                    list.Add(new Doc_MainViewRecord() { ID = records[i].ID }, new Doc_MainViewRecord() { EdfPath = records[i].EdfPath });
                DataModel.DataBaseHelper.Default.Update(list);
                m_recordList = AnalysisRecordViewData.ConvertEntity(DataModel.DataBaseHelper.Default.LoadMainView());
                ///用户确认
                if (ShowMessage(string.Format(Program.Language == "EN" ? "Currently retrieved {0} matching records (see form), (Yes) waiting for user action, (No) returning to homepage" : "当前检索到{0}条相匹配的记录（见表单）,(是)等待用户操作，(否)返回首页", records.Count)))
                {
                    //Dictionary<ITable, ITable> list = new Dictionary<ITable, ITable>();
                    //for (int i = 0; i < records.Count; i++)
                    //    list.Add(new Doc_MainViewRecord() { ID = records[i].ID }, new Doc_MainViewRecord() { EdfPath = records[i].EdfPath });
                    //DataModel.DataBaseHelper.Default.Update(list);
                    ////自动搜索到了记录之后点击返回，在首页记录里面该记录应该只需要选择医生，就可以查看历史回放。这里更新m_recordlist，达到效果
                    //m_recordList = AnalysisRecordViewData.ConvertEntity(DataModel.DataBaseHelper.Default.LoadMainView());
                }
                else
                {
                    RoundAngleButton6_Click(null, null);
                }
            }
            else
            {
                AhDung.MessageTip.ShowWarning(Program.Language=="EN"? "No relevant case records were found at the moment!" : "当前未搜索到相关的病例记录！");
            }

        }
        /// <summary>
        /// 返回首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RoundAngleButton6_Click(object sender, EventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                panel4.Visible = true;
                m_TotalCount = m_recordList.Count;
                m_TotalPage = m_TotalCount % m_OnePageCount == 0 ? m_TotalCount / m_OnePageCount : m_TotalCount / m_OnePageCount + 1;
                ////数据更新到表单中
                LoadData(m_recordList, m_PageIndex);
                reFreshPagelable();
                bak_records = m_recordList;
                showTaskTip();
            }));

        }

        private void planWork_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            planWork.Visible = false;
            bak_records = m_waitforRecords.FindAll(t => t.EdfPath != "");
            m_TotalCount = bak_records.Count;
            m_TotalPage = m_TotalCount % m_OnePageCount == 0 ? m_TotalCount / m_OnePageCount : m_TotalCount / m_OnePageCount + 1;
            ////数据更新到表单中
            LoadData(bak_records, 1);
            reFreshPagelable();
        }
        private void showTaskTip()
        {
            int len = m_waitforRecords.FindAll(t => t.EdfPath != "").Count;
            if (len > 0)
            {
                planWork.Visible = true;
                planWork.Text = len.ToString();
                this.toolTip1.SetToolTip(this.planWork, string.Format(Program.Language=="EN"? "Unprocessed task: {0}" : "未处理任务：{0}", len));
            }
            else
            {
                planWork.Visible = false;
            }
        }

        #region 快捷菜单子项的绑定事件

        /// <summary>
        /// 病例编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientInfoEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string id = PatientCaseContextMenuStrip.Tag.ToString();
            DataModel.LogInstance.Default.AddLog(string.Format("用户在右键菜单栏中点击 病例编辑 病例号为 {0}", id));
            Doc_PatientInfo patientinfo = DataBaseHelper.Default.Select(new Doc_PatientInfo() { PatientNo = id });
            PatientEdit patient = new PatientEdit();
            patient.Text = Program.Language=="EN"? "Case Edit" : "病例编辑";
            patient.Initialize(patientinfo, true);
            patient.Owner = this.ParentForm;
            patient.StartPosition = FormStartPosition.CenterParent;
            patient.SaveInfoHandle += (this.ParentForm as MainForm).patient_SaveInfoHandle;
            patient.ShowDialog();
        }

        /// <summary>
        /// 新监测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewMonitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string id = PatientCaseContextMenuStrip.Tag.ToString();
            DataModel.LogInstance.Default.AddLog(string.Format("用户在右键菜单栏中点击 新监测 病例号为 {0}", id));
            Doc_PatientInfo patientinfo = DataBaseHelper.Default.Select(new Doc_PatientInfo() { PatientNo = id });
            Channel.Default.NextStep(patientinfo);
        }

        /// <summary>
        /// 新诊断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewDiagnosisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string id = PatientCaseContextMenuStrip.Tag.ToString();
            DataModel.LogInstance.Default.AddLog(string.Format("用户在右键菜单栏中点击 新诊断 病例号为 {0}", id));
            AnalysisRecordViewData record = NewDiagnosisToolStripMenuItem.Tag as AnalysisRecordViewData;
            string path = "";
            if (record.EdfPath.Contains(".."))
            {
                path = record.EdfPath.Replace("..", Application.StartupPath);
            }
            else
                path = record.EdfPath;
            if (!File.Exists(path))
            {
                AhDung.MessageTip.ShowWarning(Program.Language=="EN"? "Cannot find the specified file path!" : "找不到指定的文件路径！");
                return;
            }
            List<Doc_MainViewRecord> views = DataBaseHelper.Default.SelectAll(new Doc_MainViewRecord()
                                                                            {
                                                                                MatchKey = record.MatchKey,
                                                                                PatientID = record.PatientNo
                                                                            });
            IEnumerable<string> doctorids = views.Select(t => t.DoctorID).Distinct();
            var sss = m_DoctorList.Where(t => !doctorids.Contains(t.ID.ToString()));
            if (sss.Count() > 0)
            {
                Doc_PatientInfo patientinfo = DataBaseHelper.Default.Select(new Doc_PatientInfo() { PatientNo = id });
                Block.NewReplayDialog replay = new Block.NewReplayDialog();
                replay.EdfPath = path;
                replay.Tag = record;
                replay.SetPatientAndDoctor(sss.ToList(), patientinfo);
                if (replay.ShowDialog() == DialogResult.OK)
                {
                    initMontage(record.ReviewMontageName);
                    Channel.Default.StartHistroyAnalysisByEDF(replay.EdfPath, replay.AutoAnalysis, replay.GUID);
                }
            }
            else
            {
                AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "Cannot rate: All raters have rated the current record!" : "不能评分：所有评分人都对当前记录有过评分！");
            }
        }

        /// <summary>
        /// 手动分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManualAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loading)
            {
                AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "Loading not completed, please wait..." : "加载未完成，请等待...");
                return;
            }
            //DataModel.LogInstance.Default.AddLog(string.Format("用户在右键菜单栏中点击 手动分析 病例号为 {0}", PatientCaseContextMenuStrip.Tag.ToString()));
            AnalysisRecordViewData record = ManualAnalysisToolStripMenuItem.Tag as AnalysisRecordViewData;
            DataModel.LogInstance.Default.AddLog(string.Format("用户在右键菜单栏中点击 手动分析 病例号为 {0} 评分人为 {1}", record.PatientNo, record.DoctorName));
            if (!string.IsNullOrEmpty(record.GUID) && record.Progress != ProgressState.Ready)
            {
                AnalysisResult result = new AnalysisResult() { ReadFromdb = true, GUID = record.GUID };
                result.EdfPath = record.EdfPath;
                result.Tag = record.ID;
                if (result.EdfPath.Contains(".."))
                {
                    result.EdfPath = result.EdfPath.Replace("..", Application.StartupPath);
                }
                ////数据源加载入口
                if (LoadRecordHandle != null)
                {
                    if (File.Exists(result.EdfPath))
                    {
                        Channel.Default.Patient = DataModel.DataBaseHelper.Default.Select<Doc_PatientInfo>(new Doc_PatientInfo() { PatientNo = record.PatientNo });
                        Channel.Default.Doctor = m_DoctorList.Find(t => t.UserID == record.DoctorID);
                        initMontage(record.ReviewMontageName);
                        LoadRecordHandle.BeginInvoke(result, record.Progress, null, null);
                    }
                    else
                    {
                        AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "Unable to find data source (EDF file)" : "找不到数据源(edf文件)");
                    }
                }
            }
            else
            {
                ImportHistroyDialog importHistroyDialog = new ImportHistroyDialog();
                importHistroyDialog.ID = record.PatientNo;
                importHistroyDialog.RecordTime = record.RecordTime;
                Channel.Default.MatchKey = record.MatchKey;
                if (importHistroyDialog.ShowDialog() == DialogResult.OK)
                {
                    Channel.Default.Patient = DataModel.DataBaseHelper.Default.Select<Doc_PatientInfo>(new Doc_PatientInfo() { PatientNo = record.PatientNo });
                    record.EdfPath = importHistroyDialog.EdfPath;
                    if (LoadRecordHandle != null)
                    {
                        this.Dispose();
                        LoadRecordHandle.Invoke(new AnalysisResult() { Tag = record.ID, EdfPath = record.EdfPath }, ProgressState.Ready);
                    }
                }

            }
        }

        /// <summary>
        /// 自动分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loading)
            {
                AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "Loading not completed, please wait...":"加载未完成，请等待...");
                return;
            }
            string id = PatientCaseContextMenuStrip.Tag.ToString();
            DataModel.LogInstance.Default.AddLog(string.Format("用户在右键菜单栏中点击 自动分析 病例号为 {0}", id));
            Doc_PatientInfo patientinfo = DataBaseHelper.Default.Select(new Doc_PatientInfo() { PatientNo = id });
            Block.NewReplayDialog replay = new Block.NewReplayDialog();
            replay.Text = Program.Language == "EN" ? "Automatic Analysis" : "自动分析";
            replay.SetPatientAndDoctor(m_DoctorList, patientinfo);
            replay.AutoAnalysis = true;
            replay.Tag = AutoAnalysisToolStripMenuItem.Tag;
            if (replay.ShowDialog() == DialogResult.OK)
            {
                Channel.Default.StartHistroyAnalysisByEDF(replay.EdfPath, replay.AutoAnalysis);
            }
        }

        /// <summary>
        /// 导出报告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataModel.LogInstance.Default.AddLog(string.Format("用户在右键菜单栏中点击 导出报告 病例号为 {0}", PatientCaseContextMenuStrip.Tag.ToString()));
            AnalysisRecordViewData record = this.ExportReportToolStripMenuItem.Tag as AnalysisRecordViewData;
            string reserve = DataBaseHelper.Default.Select<Doc_MainViewRecord>(new Doc_MainViewRecord
            {
                ID = record.ID
            }).Reserve1;
            string reportTyp = "pdf";
            string templatePath = "";
            if (!string.IsNullOrEmpty(reserve))
            {
                string[] array = reserve.Split(new char[]
                {
                    ';'
                });
                bool flag = bool.Parse(array[1]);
                reportTyp = array[2].ToLower();
                string[] array2 = array[3].Split(new char[]
                {
                    '/'
                });
                templatePath = string.Format("{0}ReportTemplate\\{1}\\{2}", AppDomain.CurrentDomain.BaseDirectory, flag ? "Breath" : "Sleep", flag ? array2[1] : array2[0]);
            }
            else
            {
                if (Channel.Default.SystemSetting.ReportType != "")
                {
                    reportTyp = Channel.Default.SystemSetting.ReportType.ToLower();
                }
                templatePath = Channel.Default.ReportTemplatePath;
            }
            string fileName = string.Format(Program.Language=="EN"? "Sleep analysis report for {1}({2}) {0}--{3}" : "{1}({2})的睡眠分析报告{0}--{3}", new object[]
            {
                record.RecordTime.ToString("yyyyMMddHHmmss"),
                record.PatientName,
                record.PatientNo,
                record.DoctorName
            });
            SaveFileDialog report = new SaveFileDialog();
            report.Filter = ((reportTyp == "xps") ? "Xps|*.xps" : ((reportTyp == "word") ? "Word|*.doc" : ((reportTyp == "pdf") ? "Pdf|*.pdf" : "Excel|*.xlsx")));
            report.RestoreDirectory = true;
            report.FileName = fileName;
            if (report.ShowDialog() == DialogResult.OK)
            {
                base.BeginInvoke(new MethodInvoker(delegate ()
                {
                    AnalysisResult analysisResult = new DataModel.DataManger().ReadResult(record.ResultPath, record.GUID);
                    analysisResult.StartTime = record.RecordTime;
                    analysisResult.EndTime = record.RecordTime.AddSeconds((double)(record.FrameCount * 30));
                    analysisResult.Sleep.TotalRecordTime = (float)Math.Round((analysisResult.EndTime - analysisResult.StartTime).TotalMinutes, 2);
                    if (analysisResult.Sleep.TotalRecordTime < analysisResult.Sleep.TimeInBed)
                        analysisResult.Sleep.TotalRecordTime = analysisResult.Sleep.TimeInBed;
                    List<string> list = new List<string>();
                    List<string> list2 = new List<string>();
                    Doc_PatientInfo doc_PatientInfo = DataBaseHelper.Default.Select<Doc_PatientInfo>(new Doc_PatientInfo
                    {
                        PatientNo = record.PatientNo
                    });
                    doc_PatientInfo.RecordTime = record.RecordTime;
                    doc_PatientInfo.DoctorName = record.DoctorName;
                    ResultDomain result = analysisResult.ConvertResultDomain(record.FrameCount, false);
                    Image image = Channel.Default.CreatMap(result, false);
                    image.Save(doc_PatientInfo.ResultPhoto);
                    ITable[] array3 = new ITable[]
                    {
                        doc_PatientInfo,
                        analysisResult.BloodOxygen,
                        analysisResult.BodyMovement,
                        analysisResult.BodyState,
                        analysisResult.BreathEvent,
                        analysisResult.HeartRate,
                        analysisResult.Sleep
                    };
                    for (int i = 0; i < array3.Length; i++)
                    {
                        PropertyInfo[] properties = array3[i].GetType().GetProperties();
                        for (int j = 0; j < properties.Length; j++)
                        {
                            list.Add(properties[j].Name);
                            object value = properties[j].GetValue(array3[i], null);
                            list2.Add((value != null) ? value.ToString() : j.ToString());
                        }
                    }
                    if (reportTyp != "excel")
                    {
                        ReportHelper.DocumentFormat sformat = ReportHelper.DocumentFormat.Pdf;
                        if (!(reportTyp == "xps"))
                        {
                            if (!(reportTyp == "pdf"))
                            {
                                if (reportTyp == "word")
                                {
                                    sformat = ReportHelper.DocumentFormat.Doc;
                                }
                            }
                            else
                            {
                                sformat = ReportHelper.DocumentFormat.Pdf;
                            }
                        }
                        else
                        {
                            sformat = ReportHelper.DocumentFormat.Xps;
                        }
                        this.aph.CreateNewWordDocument(templatePath);
                        this.aph.SaveAs(list, list2, report.FileName, sformat, delegate (bool t)
                        {
                            if (t)
                            {
                                AhDung.MessageTip.ShowOk(Program.Language=="EN"? "Saved Successfully!" : "保存成功！", -1, null, null, false);
                                return;
                            }
                            AhDung.MessageTip.ShowError(Program.Language == "EN" ? "Save failed!" : "保存失败！", -1, null, null, false);
                        });
                        return;
                    }
                    ExcelTemplate excelTemplate = new ExcelTemplate();
                    excelTemplate.Open(templatePath);
                    excelTemplate.SetValues(list, list2);
                    excelTemplate.SaveAs(report.FileName, delegate (bool t)
                    {
                        if (t)
                        {
                            AhDung.MessageTip.ShowOk(Program.Language == "EN" ? "Saved Successfully!" : "保存成功！", -1, null, null, false);
                            return;
                        }
                        AhDung.MessageTip.ShowError(Program.Language == "EN" ? "Save failed!" : "保存失败！", -1, null, null, false);
                    });
                    excelTemplate.Close();
                }));
            }
        }

        /// <summary>
        /// 导入EDF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportEDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loading)
            {
                AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "Loading not completed, please wait..." : "加载未完成，请等待...");
                return;
            }
            DataModel.LogInstance.Default.AddLog(string.Format("用户在右键菜单栏中点击 导入EDF 病例号为 {0}", PatientCaseContextMenuStrip.Tag.ToString()));
            int rowIdx = 0;
            if (abSelect.SelectedRows.Count > 0)
                rowIdx = abSelect.SelectedRows[0].Index;
            else
                rowIdx = abSelect.CurrentRow.Index;
            AnalysisRecordViewData record = abSelect.Rows[rowIdx].Tag as AnalysisRecordViewData;
            Doc_PatientInfo patientinfo = DataBaseHelper.Default.Select(new Doc_PatientInfo() { PatientNo = record.PatientNo });
            Block.NewReplayDialog import = new Block.NewReplayDialog();
            import.IsImportEDF = true;
            import.Tag = record;
            import.SetPatientAndDoctor(m_DoctorList, patientinfo);
            if (import.ShowDialog() == DialogResult.OK)
            {
                Channel.Default.StartHistroyAnalysisByEDF(import.EdfPath, import.AutoAnalysis, import.GUID);
            }
        }

        /// <summary>
        /// 解锁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnlockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowIdx = Convert.ToInt16(UnlockToolStripMenuItem.Tag);
            AnalysisRecordViewData record = abSelect.Rows[rowIdx].Tag as AnalysisRecordViewData;
            tools.LockDialog lockdialog = new tools.LockDialog();
            lockdialog.StartPosition = FormStartPosition.Manual;
            lockdialog.Location = Cursor.Position;
            lockdialog.Location = EnsureLocationHelper.CalculateSituableLocation(lockdialog);
            lockdialog.PSW = m_DoctorList.Find(t => t.UserID == record.DoctorID).LockPsw;
            if (lockdialog.ShowDialog() == DialogResult.OK)
            {
                DataBaseCom.Doc_MainViewRecord oldrec = new Doc_MainViewRecord() { GUID = record.GUID };
                DataBaseCom.Doc_MainViewRecord newrec = new Doc_MainViewRecord()
                {
                    Progress = (int)ProgressState.Temporary,
                };
                DataBaseHelper.Default.Update(oldrec, newrec);//把当前记录的状态变更成临时存储
                //DataBaseHelper.Default.UpdateToTemporaryResultEx(record.GUID);
                record.Progress = ProgressState.Temporary;
                abSelect.Rows[rowIdx].Tag = record;
                abSelect.Rows[rowIdx].Cells[m_strProcessColumnName].Value = Program.Language == "EN" ? "To be Finish" : "待完成";
                abSelect.Rows[rowIdx].Cells[m_strMapColumnName] = new DataGridViewTextBoxCell() { Value = "--" };
                abSelect.Rows[rowIdx].Cells[m_strReportColumnName] = new DataGridViewTextBoxCell() { Value = "--" };
                LinkAndImageCell lc = abSelect.Rows[rowIdx].Cells[m_strProcessColumnName] as LinkAndImageCell;
                lc.Image = Properties.Resources.temporary;
                lc.ImageAlignmentMode = LinkAndImageCell.ImageAlignment.Center;
                if (Program.Language == "EN")
                {
                    lc.Style.Padding = new Padding(40, 0, 0, 0);
                }
  
                abSelect.InvalidateRow(rowIdx);
            }
        }

        /// <summary>
        /// 自动搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //更新m_waitforrecords里面的数据，新建病例再监测不会更新数据，导致自动搜索找不到，所以在点击前手动更新
            m_waitforRecords = AnalysisRecordViewData.ConvertEntity(DataModel.DataBaseHelper.Default.LoadMainView(ProgressState.Ready));
            DataModel.LogInstance.Default.AddLog(string.Format("用户在右键菜单栏中点击 自动搜索 病例号为 {0}", PatientCaseContextMenuStrip.Tag.ToString()));
            string[] dirs = Environment.GetLogicalDrives(); //取得所有的盘符 
            int cnt = 0;
            AnalysisRecordViewData record = AutoSearchToolStripMenuItem.Tag as AnalysisRecordViewData;
            AnalysisRecordViewData find = m_waitforRecords.Find(t => t.MatchKey == record.MatchKey);
            if(find!=null)
            {
                if (find.EdfPath != "")
                {
                    AhDung.MessageTip.ShowWarning(Program.Language=="EN"? "The record file has been matched. Please select the evaluator to enter the scoring mode" : "该记录文件已匹配，请选择评分人，进入评分模式");
                    DataModel.LogInstance.Default.AddLog(string.Format("记录文件已匹配，未选择评分人"), pSystem.LogManagement.LogLevel.WARN);
                    return;
                }
            }
            if (record.MatchKey == "")
            {
                AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "This record cannot be automatically searched, please search manually" : "该记录无法自动搜索，请手动搜索");
                DataModel.LogInstance.Default.AddLog(string.Format("匹配失败 数据库中记录的key为{0}", record.MatchKey), pSystem.LogManagement.LogLevel.ERROR);
                return;
            }
            EDF.Default.Interrupt = false;
            foreach (string dir in dirs)
            {
                try
                {
                    DriveInfo Tdriver = new DriveInfo(dir);
                    if (Tdriver.DriveType == DriveType.Removable)
                    {
                        string[] files = Directory.GetFiles(dir, "*.edf");
                        for (int i = 0; i < files.Length; i++)
                        {
                            EDF edf = EDF.Default.ConvertToEDF(EDF.Default.ReadFile(files[i], false));
                            if (edf.getMatchKey().Contains(record.MatchKey) || edf.getMatchKey().Contains(record.MatchKey.Substring(0, record.MatchKey.Length - 1)))
                            {
                                if (MessageForm.Show(Program.Language == "EN" ? "Matching successful, do you need to rate?" : "已匹配成功，是否需要进行评分？", Program.Language == "EN" ? "Message":"信息确认", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                {
                                    DataModel.LogInstance.Default.AddLog(string.Format("匹配成功 数据库中记录的key为{0}  edf文件中记录的key为{1}", record.MatchKey, edf.getMatchKey()), pSystem.LogManagement.LogLevel.WARN);
                                    string path = System.IO.Path.Combine(edf.DefaultEdfSavePath, string.Format("{0:yyyy-MM-dd}\\{1}", edf.StartTime, Path.GetFileName(files[i])));
                                    string dirPath = Path.GetDirectoryName(path);
                                    if (!Directory.Exists(dirPath))
                                    {
                                        Directory.CreateDirectory(dirPath);
                                    }
                                    ApiCopyFile.DoCopy(files[i], path, IntPtr.Zero);
                                    Doc_PatientInfo patientinfo = DataBaseHelper.Default.Select(new Doc_PatientInfo() { PatientNo = record.PatientNo });
                                    Block.NewReplayDialog replay = new Block.NewReplayDialog();
                                    replay.SetPatientAndDoctor(m_DoctorList, patientinfo);
                                    replay.EdfPath = path;
                                    replay.Tag = record;
                                    if (replay.ShowDialog() == DialogResult.OK)
                                    {
                                        m_waitforRecords.RemoveAll(t => t.ID == record.ID);
                                        //Channel.Default.StartHistroyAnalysisByEDF(replay.EdfPath, replay.AutoAnalysis);
                                        if (LoadRecordHandle != null)
                                        {
                                            record.EdfPath = path;
                                            LoadRecordHandle.Invoke(new AnalysisResult() { Tag = record.ID, EdfPath = record.EdfPath }, ProgressState.Ready);
                                        }
                                    }
                                }
                                cnt++;
                                break;
                            }
                        }
                    }
                }
                catch(Exception ee)
                {
                    DataModel.LogInstance.Default.AddLog(string.Format("检索磁盘出错 {0}", ee.ToString()), pSystem.LogManagement.LogLevel.ERROR);
                }
            }
            if (cnt == 0)
            {
                if (MessageForm.Show(Program.Language=="EN"? "No matching EDF files were found for the current case. Do you want to specify a path for searching?" : "未发现与当前病例匹配的edf文件，是否指定路径搜索？", Program.Language=="EN"? "Message" : "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    FolderBrowserDialog folder = new System.Windows.Forms.FolderBrowserDialog();
                    if (folder.ShowDialog() == DialogResult.OK)
                    {
                        string[] files = Directory.GetFiles(folder.SelectedPath, "*.edf");
                        for (int i = 0; i < files.Length; i++)
                        {
                            EDF edf = EDF.Default.ConvertToEDF(EDF.Default.ReadFile(files[i], false));
                            string path = files[i];
                            // if (m_DataSource.Select(string.Format("PatientNO='{0}' and Progress={1}", edf.PatientNO, (int)ProgressState.Ready)).Length > 0)
                            if (edf.getMatchKey().Contains(record.MatchKey) || edf.getMatchKey().Contains(record.MatchKey.Substring(0, record.MatchKey.Length - 1)))
                            {
                                if (MessageForm.Show(Program.Language=="EN"? "Matching successful, do you need to rate?" : "已匹配成功，是否需要进行评分？",Program.Language=="EN"? "CONFIRM" : "信息确认", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                {
                                    Doc_PatientInfo patientinfo = DataBaseHelper.Default.Select(new Doc_PatientInfo() { PatientNo = record.PatientNo });
                                    Block.NewReplayDialog replay = new Block.NewReplayDialog();
                                    replay.SetPatientAndDoctor(m_DoctorList, patientinfo);
                                    replay.EdfPath = path;
                                    replay.Tag = record;
                                    if (replay.ShowDialog() == DialogResult.OK)
                                    {
                                        m_waitforRecords.RemoveAll(t => t.ID == record.ID);
                                        if (LoadRecordHandle != null)
                                        {
                                            record.EdfPath = path;
                                            LoadRecordHandle.Invoke(new AnalysisResult() { Tag = record.ID, EdfPath = record.EdfPath }, ProgressState.Ready);
                                        }
                                    }
                                }
                                cnt++;
                                break;
                            }
                        }
                        if (cnt == 0)
                        {
                            MessageForm.Show(Program.Language == "EN" ? "No matching EDF files were found for the current case?" : "未发现与当前病例匹配的edf文件", Program.Language == "EN" ? "Message" : "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowIdx = 0;
            if (abSelect.SelectedRows.Count > 0)
                rowIdx = abSelect.SelectedRows[0].Index;
            else
                rowIdx = abSelect.CurrentRow.Index;
            AnalysisRecordViewData record = abSelect.Rows[rowIdx].Tag as AnalysisRecordViewData;
            DataModel.LogInstance.Default.AddLog(string.Format("用户在右键菜单栏中点击 删除 病例号为 {0}", record.PatientNo));
            tools.LockDialog lockdialog = new tools.LockDialog();
            lockdialog.StartPosition = FormStartPosition.Manual;
            lockdialog.Location = Cursor.Position;
            lockdialog.Location = EnsureLocationHelper.CalculateSituableLocation(lockdialog);
            lockdialog.Text = Program.Language=="EN"?"Delete":"删除";
            if (record.Progress == ProgressState.Compelet || record.Progress == ProgressState.Temporary)
            {
                lockdialog.PSW = m_DoctorList.Find(t => t.UserID == record.DoctorID).LockPsw;
            }
            if (lockdialog.ShowDialog() == DialogResult.OK)
            {
                if (DataBaseHelper.Default.DeletaResultEx(record.ID, record.PatientNo, record.GUID, record.MatchKey))
                {
                    if (record.ResultPath == Channel.Default.SystemSetting.SaveEdfPath)
                    {
                        record.ResultPath = string.Format("{0}\\Location\\{1}", record.ResultPath, record.GUID);
                        try
                        {
                            System.IO.Directory.Delete(record.ResultPath, true);///删除存储文件
                        }
                        catch (Exception ee)
                        {
                            DataModel.LogInstance.Default.AddLog(string.Format("删除存储文件出错 文件地址{0}", record.ResultPath), pSystem.LogManagement.LogLevel.ERROR);
                        }
                    }
                    DataModel.LogInstance.Default.AddLog(string.Format("删除存储文件 文件地址{0}", record.ResultPath), pSystem.LogManagement.LogLevel.DEBUG);
                    bak_records.Remove(record);
                    m_TotalCount--;
                    m_TotalPage = m_TotalCount % m_OnePageCount == 0 ? m_TotalCount / m_OnePageCount : m_TotalCount / m_OnePageCount + 1;
                    if (m_PageIndex > m_TotalPage)
                    {
                        m_PageIndex = m_TotalPage;
                        if (m_PageIndex == 0)
                            m_PageIndex = 1;
                    }
                    LoadData(bak_records, m_PageIndex);
                    reFreshPagelable();
                    m_waitforRecords.Remove(record);
                    showTaskTip();
                    AhDung.MessageTip.ShowOk(Program.Language == "EN" ? "Record deletion successful！" : "记录删除成功!");
                }
                else
                    AhDung.MessageTip.ShowError(Program.Language == "EN" ? "Record deletion failed!" : "记录删除失败!");
            }
        }

        #endregion
        /// <summary>
        /// 导出统计信息按钮点击时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportInformationButton_Click(object sender, EventArgs e)
        {
            string templatePath = Application.StartupPath + "\\StatisticsReport\\统计信息模版.xls";
            string savepath = string.Format(Program.Language == "EN" ? "{0}\\Statistics Information.xls" : "{ 0}\\统计信息.xls", Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            if (isOpenFile(templatePath) || isOpenFile(savepath))
            {
                AhDung.MessageTip.ShowError(Program.Language == "EN" ? "Failed to open statistics file, file already open" : "打开统计信息文件失败,文件已打开");
            }
            else
            {
                FunctionControls.tools.ProgressTipForm.Defalut.Text = Program.Language == "EN" ? "Export statistical information" : "导出统计信息";
                FunctionControls.tools.ProgressTipForm.Defalut.DoWork += ExportInfDefalut_DoWork;
                if (FunctionControls.tools.ProgressTipForm.Defalut.ShowDialog() == DialogResult.OK)
                {
                    AhDung.MessageTip.ShowOk(Program.Language == "EN" ? "Successfully exported statistical information！" : "导出统计信息成功");
                    System.Diagnostics.Process.Start(savepath);
                }
            }
        }
        private void ExportInfDefalut_DoWork(ProgressTipForm sender, DoWorkEventArgs e)
        {
            string templatePath = Application.StartupPath + "\\StatisticsReport\\统计信息模版.xls";
            string savepath = string.Format(Program.Language == "EN" ? "{0}\\Statistics Information.xls" : "{0}\\统计信息.xls", Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            sender.SetProgress(10, Program.Language == "EN" ? "Open the statistical information template " : "打开统计信息模版");
            detailInfor = Util.ExcelUtils.ExcelHelper.ExcelToTable(templatePath);
            Dictionary<string, int> columnindex = new Dictionary<string, int>();
            List<string> titleName = new List<string>();
            int index = 0;
            //标题行
            if (detailInfor.Rows.Count != 1 || detailInfor.Columns.Count != detailInfor.Rows[0].ItemArray.Length)
            {
                sender.SetError(Program.Language == "EN" ? "Exporting statistical information failed, please check the template！" : "导出统计信息失败,请检查模版");
                return;
            }
            for (int i = 0; i < detailInfor.Columns.Count; i++)
            {
                columnindex.Add(detailInfor.Rows[0].ItemArray[i].ToString(), i);
                titleName.Add(detailInfor.Columns[i].ToString());
            }
            string[] valuesindex = new string[detailInfor.Columns.Count];
            List<Doc_MainViewRecord> needRecords = DataBaseHelper.Default.SelectAll<Doc_MainViewRecord>(new Doc_MainViewRecord
            {
                Progress = (int)DataModel.ProgressState.Compelet,
                ModeType = Channel.Default.SystemSetting.ModeType,
                LoginID = Channel.Default.Loginer.ID
            });
            if (needRecords.Count > 0)
            {
                List<string> allValues = new List<string>();
                try
                {
                    sender.SetProgress(50, Program.Language == "EN" ? "Obtain statistical information" : "获取统计信息");
                    if (sender.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                    for (int i = 0; i < needRecords.Count; i++)
                    {
                        AnalysisResult oneAnalysisResult = new DataModel.DataManger().ReadResult(needRecords[i].Reserve5, needRecords[i].GUID);
                        Doc_PatientInfo doc_PatientInfo = DataBaseHelper.Default.Select<Doc_PatientInfo>(new Doc_PatientInfo
                        {
                            PatientNo = needRecords[i].PatientID
                        });
                        ITable[] array6 = new ITable[]
                        {
                            doc_PatientInfo,
                            oneAnalysisResult.BloodOxygen,
                            oneAnalysisResult.BodyMovement,
                            oneAnalysisResult.BodyState,
                            oneAnalysisResult.BreathEvent,
                            oneAnalysisResult.HeartRate,
                            oneAnalysisResult.Sleep
                        };
                        for (int j = 0; j < array6.Length; j++)
                        {
                            PropertyInfo[] properties = array6[j].GetType().GetProperties();
                            for (int k = 0; k < properties.Length; k++)
                            {
                                if (columnindex.Keys.Contains(properties[k].Name))
                                {
                                    object value = properties[k].GetValue(array6[j], null);
                                    if (columnindex.TryGetValue(properties[k].Name, out index))
                                        valuesindex[index] = (value != null) ? value.ToString() : "";
                                    else
                                        continue;
                                }
                            }
                        }
                        allValues.AddRange(valuesindex);
                    }
                    sender.SetProgress(80, Program.Language == "EN" ? "Save statistical information" : "保存统计信息");
                    if (sender.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                    NPOI.SS.UserModel.IWorkbook workbook = Util.ExcelUtils.ExcelHelper.RecordSetValues(titleName, allValues);
                    Util.ExcelUtils.ExcelHelper.SaveExcel(savepath, workbook);
                    sender.SetProgress(100, "完成");
                }
                catch (Exception ee)
                {
                    DataModel.LogInstance.Default.AddLog(string.Format("导出统计信息出错 错误信息{0}", ee.Message),pSystem.LogManagement.LogLevel.ERROR);
                    sender.SetError(Program.Language == "EN" ? "Exporting statistical information failed" : "导出统计信息失败");
                }
            }
            else
            {
                sender.SetError(Program.Language == "EN" ? "There are no completed case records" : "没有已完成的病例记录");
                return;
            }
        }


        /// <summary>
        /// 查询按钮点击时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (DateCheckBox.Checked)
            {
                if (StartDateTimePicker.Value > EndDateTime.Value)
                {
                    AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "The start time of the query cannot be less than the end time!" : "查询的起始时间不能小于结束时间!");
                    return;
                }
            }
            string userid = "";
            if (DocNameCombBox.Text != "")
            {
                userid = DocNameCombBox.Text.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries)[1];
            }
            m_recordList = DateCheckBox.Checked ? AnalysisRecordViewData.ConvertEntity(DataModel.DataBaseHelper.Default.LoadMainView
                                                                                        (TbPatientNo.Text,
                                                                                         userid,
                                                                                         PatientNameTextBox.Text,
                                                                                         DateTime.Parse(string.Format("{0:yyyy-MM-dd 00:00:00}",
                                                                                         StartDateTimePicker.Value)),
                                                                                         DateTime.Parse(string.Format("{0:yyyy-MM-dd 23:59:59}",
                                                                                         EndDateTime.Value
                                                                                         ))))
                           : AnalysisRecordViewData.ConvertEntity(DataModel.DataBaseHelper.Default.LoadMainView
                                                                   (TbPatientNo.Text,
                                                                   userid,
                                                                   PatientNameTextBox.Text));
            m_TotalCount = m_recordList.Count;
            m_TotalPage = m_TotalCount % m_OnePageCount == 0 ? m_TotalCount / m_OnePageCount : m_TotalCount / m_OnePageCount + 1;
            m_PageIndex = 1;
            DataModel.LogInstance.Default.AddLog(string.Format("点击 查询 按钮,依据病例号 {0} 病人姓名 {1} 评分人 {2} 日期从 {3} 至 {4} 查询", TbPatientNo.Text, PatientNameTextBox.Text, userid, StartDateTimePicker.Value.ToString("G"), EndDateTime.Value.ToString("G")));
            LoadData(m_recordList, m_PageIndex);
            m_waitforRecords = AnalysisRecordViewData.ConvertEntity(DataModel.DataBaseHelper.Default.LoadMainView(ProgressState.Ready));
            bak_records = m_recordList;
            reFreshPagelable();
        }

        /// <summary>
        /// 自动搜索按钮点击时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoSearchButton_Click(object sender, EventArgs e)
        {
            //更新m_waitforrecords里面的数据，新建病例再检测不会更新数据，导致自动搜索找不到，所以在点击前手动更新
            m_waitforRecords = AnalysisRecordViewData.ConvertEntity(DataModel.DataBaseHelper.Default.LoadMainView(ProgressState.Ready));
            DataModel.LogInstance.Default.AddLog("点击 自动搜索 按钮");
            Block.AutoSearchDialog search = new Block.AutoSearchDialog();
            search.SearchRecordsEventHandle += search_SearchRecordsEventHandle;
            search.Init(m_waitforRecords);
            EDF.Default.Interrupt = false;
            search.ShowDialog();
        }
        //判断文件是否已经打开
        private bool isOpenFile(string filepath)
        {
            bool isopen = false;
            try
            {
                FileStream fileStream = File.OpenWrite(filepath);
                fileStream.Close();
            }
            catch
            {
                isopen = true;
            }
            return isopen;
        }
        #region 数据导入导出
        private bool m_fnStop = false;
        /// <summary>
        /// 执行导出记录数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportZip_Defalut_DoWork(ProgressTipForm sender, DoWorkEventArgs e)
        {
            object[] array = sender.Argument as object[];
            string edfpath = array[0] as string;
            bool doctordetailneedexport = (bool)array[2];
            bool patientresultneedexport = (bool)array[3];
            bool videoneedexport = (bool)array[4];
            AnalysisRecordViewData record = array[1] as AnalysisRecordViewData;
            ///临时压缩文件目录
            string parentPath = string.Format("{0}\\psz_ziptemplate", Path.GetDirectoryName(edfpath));
            ///edf所在目录
            string edfdirectory = string.Format("{0}\\{1:yyyy-MM-dd}", parentPath, DateTime.Now);
            ///评分结果所在次上级目录
            string sourcePath = string.Format("{0}\\Location", Channel.Default.SystemSetting.SaveEdfPath);
            ///评分结果所在上级目录
            string resultpath = string.Format("{0}\\{1}", sourcePath, record.GUID);
            ///视频文件所在目录
            string videopath=string .Format("{0}\\{1}", record.Reserve3, record.MatchKey);
            string desdirectory = "";
            bool edfcopyready = false;
            try
            {
                ///移动edf文件，不用copy的原因是在同级磁盘中移动消耗时间更短
                Directory.CreateDirectory(edfdirectory);
                sender.SetProgress(5, Program.Language=="EN"? "Copy Edf file" : "复制Edf文件");
                if (sender.CancellationPending)
                {
                    goto last;
                }
                File.Move(edfpath, string.Format("{0}\\{1}", edfdirectory, Path.GetFileName(edfpath)));
                edfcopyready = true;
                ///写入病例
                string baseInfodirectory = string.Format("{0}\\{1}", parentPath, record.GUID);
                sender.SetProgress(10, Program.Language == "EN" ? "Write case information" : "写入病例信息");
                if (sender.CancellationPending)
                {
                    goto last;
                }
                //判断目标目录是否存在，如果不在则创建之
                if (!Directory.Exists(baseInfodirectory))
                    Directory.CreateDirectory(baseInfodirectory);
                PatientUnit.Default.Write(baseInfodirectory, DataBaseHelper.Default.Select(new Doc_PatientInfo() { PatientNo = record.PatientNo }));
                if (patientresultneedexport&&Directory.Exists(resultpath))
                {
                    sender.SetProgress(20, Program.Language == "EN" ? "Copy Edf related analysis files" : "复制Edf相关分析文件");
                    if (sender.CancellationPending)
                    {
                        goto last;
                    }
                    ///把评分结果复制到临时打包文件夹
                    FolderHelper.FolderCopy(resultpath, parentPath);
                }
                if (doctordetailneedexport)
                {
                    sender.SetProgress(30, Program.Language == "EN" ? "Write rating physician information" : "写入评分医师信息");
                    if (sender.CancellationPending)
                    {
                        goto last;
                    }
                    DoctorUnit.Default.Write(baseInfodirectory, m_DoctorList.Find(t => t.UserID == record.DoctorID));
                }
                if(videoneedexport)
                {
                    sender.SetProgress(40, Program.Language == "EN" ? "Copy video files" : "复制视频文件");
                    if (sender.CancellationPending)
                    {
                        goto last;
                    }
                    ///把评分结果复制到临时打包文件夹
                    FolderHelper.FolderCopy(videopath, parentPath);
                }
                sender.SetProgress(45, Program.Language == "EN" ? "In data compression..." : "数据压缩中...");
                if (sender.CancellationPending)
                {
                    goto last;
                }
                desdirectory = string.Format("{0}\\{1}-{2}.zip", Environment.GetFolderPath(Environment.SpecialFolder.Desktop), record.GUID,record.PatientName);
                ZipHelper.GetRunStateHandler += ZipHelper_GetRunStateHandler;
                ///压缩信息
                ZipHelper.ZipDirectory(parentPath, desdirectory, "pps");
                ZipHelper.GetRunStateHandler -= ZipHelper_GetRunStateHandler;
            last:;
            }
            catch (Exception ee)
            {
                DataModel.LogInstance.Default.AddLog(string.Format("执行导出记录数据操作出错 错误信息为 {0}", ee.ToString()), pSystem.LogManagement.LogLevel.ERROR);
            }
            finally
            {
                if (edfcopyready)
                {
                    sender.SetProgress(75, Program.Language == "EN" ? "Data compression completed, clean the data..." : "数据压缩完毕,清理数据中...");
                    ///edf文件移回到原路径下
                    File.Move(string.Format("{0}\\{1}", edfdirectory, Path.GetFileName(edfpath)), edfpath);
                }
                if (Directory.Exists(parentPath))
                {
                    sender.SetProgress(85, Program.Language == "EN" ? "Cleaning completed" : "清理完成");
                    ///删除临时文件夹
                    FolderHelper.FolderDelete(parentPath);
                }
                if (sender.CancellationPending)
                {
                    e.Cancel = true;
                }
                else
                {
                    sender.SetProgress(95, Program.Language == "EN" ? "Browsing exported files..." : "正在浏览导出文件...");
                    if (desdirectory != "")
                    {
                        System.Diagnostics.Process.Start("explorer", string.Format("/select,{0}", desdirectory));
                    }
                }
                m_fnStop = false;
            }
        }
        
        private void Defalut_CanceledHandle()
        {
            m_fnStop = true;
        }
        private bool ZipHelper_GetRunStateHandler()
        {
            return !m_fnStop;
        }
        /// <summary>
        /// 导出记录数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportZipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataModel.LogInstance.Default.AddLog("用户点击 导出记录数据 按钮");
            AnalysisRecordViewData record = ExportZipToolStripMenuItem.Tag as AnalysisRecordViewData;
            bool hasvideo = record.Reserve3.ToString().Length>0;
            tools.ExportSelection selectexport = new tools.ExportSelection(hasvideo);
            if (selectexport.ShowDialog() == DialogResult.OK)
            {
                string edfpath = record.EdfPath;
                if (edfpath.Contains(".."))
                {
                    edfpath = edfpath.Replace("..", Application.StartupPath);
                }
                if (!File.Exists(edfpath))
                {
                    AhDung.MessageTip.ShowWarning(Program.Language=="EN"? "Unable to find data source (EDF file)" : "找不到数据源(edf文件)");
                    return;
                }
                m_fnStop = false;
                FunctionControls.tools.ProgressTipForm.Defalut.Text = ExportZipToolStripMenuItem.Text;
                FunctionControls.tools.ProgressTipForm.Defalut.Argument = new object[] { edfpath, record, selectexport.DoctorDetailischeck, selectexport.PatientResultischeck, selectexport.Videoischeck};
                FunctionControls.tools.ProgressTipForm.Defalut.DoWork += ExportZip_Defalut_DoWork;
                ProgressTipForm.Defalut.CanceledHandle += Defalut_CanceledHandle;
                if (FunctionControls.tools.ProgressTipForm.Defalut.ShowDialog() != DialogResult.OK)
                {

                }
                ProgressTipForm.Defalut.CanceledHandle -= Defalut_CanceledHandle;
            }
            else
            {
                return;
            }
        }
        /// <summary>
        /// 附加记录数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportZipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in PatientCaseContextMenuStrip.Items)
            {
                item.Visible = true;
            }
            OpenFileDialog folder = new System.Windows.Forms.OpenFileDialog();
            folder.Title =Program.Language=="EN"? "Additional record data" : "附加记录数据";
            folder.Filter = "zip|*.zip";
            folder.RestoreDirectory = true;
            if (folder.ShowDialog() == DialogResult.OK)
            {
                FunctionControls.tools.ProgressTipForm.Defalut.AddTaskHandler += Defalut_AddTaskHandler;
                FunctionControls.tools.ProgressTipForm.Defalut.CanceledHandle += Defalut_CanceledHandle;
                Channel.Default.StartHistroyAnalysisByEDF(folder.FileName, true, "");
            }
            m_fnStop = false;
        }
        /// <summary>
        /// 执行附加记录数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool Defalut_AddTaskHandler(object sender, DoWorkEventArgs e)
        {
            FunctionControls.tools.ProgressTipForm workplan = FunctionControls.tools.ProgressTipForm.Defalut;
            workplan.SetProgress(2, Program.Language == "EN" ? "Additional record data..." : "附加记录数据中...");
            string sourceZipPath = (e.Argument as object[])[0].ToString();
            string unzipPath = string.Format("{0}\\{1}", Path.GetDirectoryName(sourceZipPath), Path.GetFileNameWithoutExtension(sourceZipPath));
            Directory.CreateDirectory(unzipPath);///创建文件夹
            ZipHelper.GetRunStateHandler += ZipHelper_GetRunStateHandler;
            if (ZipHelper.UnZip(sourceZipPath, unzipPath, "pps"))//解压zip文件
            {
                if (workplan.CancellationPending)
                {
                    goto last;
                }
                string[] files = Directory.GetDirectories(unzipPath);
                //判断解压出来文件个数对不对
                if (files.Length < 2)
                {
                    workplan.SetError(Program.Language == "EN" ? "he content of the compressed file is incorrect!" : "压缩包文件内容错误！");
                    DataModel.LogInstance.Default.AddLog(string.Format("附加记录数据，解压文件出错，解压出来文件个数{0}",files.Length),pSystem.LogManagement.LogLevel.ERROR);
                    return false;
                }
                Doc_MainViewRecord doc_MainViewRecord = new Doc_MainViewRecord();
                foreach (string f in files)
                {
                    string parentName = Path.GetFileName(f);
                    bool haszimu=Regex.Matches(parentName, "[a-zA-Z]").Count > 0;
                    if (parentName.Contains("-") && !parentName.Contains("_")&& haszimu==false)
                    {
                        string[] edfs = Directory.GetFiles(f, "*.edf");
                        if (edfs.Length < 0)
                        {
                            AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "EDF file missing, import failed!" : "edf文件缺失，导入失败！");
                            return false;
                        }
                        FolderHelper.FolderMove(f, Channel.Default.SystemSetting.SaveEdfPath);
                        if (workplan.CancellationPending)
                        {
                            goto last;
                        }
                        doc_MainViewRecord.EdfPath = string.Format("{0}\\{1}\\{2}", Channel.Default.SystemSetting.SaveEdfPath, parentName, Path.GetFileName(edfs[0]));
                        doc_MainViewRecord.Reserve5 = Channel.Default.SystemSetting.SaveEdfPath;
                        EDF m_edf = EDF.Default.ConvertToEDF(EDF.Default.ReadFile(doc_MainViewRecord.EdfPath, false));
                        string matchkey = m_edf.getMatchKey();
                        doc_MainViewRecord.MatchKey = matchkey != "" ? matchkey : DataModel.DataBaseHelper.Default.ComputeSHA256(doc_MainViewRecord.EdfPath);
                        workplan.SetProgress(2, Program.Language == "EN" ? "Data preprocessing..." : "数据预处理...");
                    }
                    else if (parentName.Contains("-") && !parentName.Contains("_")&& haszimu)
                    {
                        //视频
                        string[] videos = Directory.GetFiles(f, "*.mp4");
                        if (videos.Length < 0)
                        {
                            AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "Video file missing, import failed!" : "视频文件缺失，导入失败！");
                            return false;
                        }
                        FolderHelper.FolderMove(f, Channel.Default.SystemSetting.VedioSavePath);
                        if (workplan.CancellationPending)
                        {
                            goto last;
                        }
                        workplan.SetProgress(4, Program.Language == "EN" ? "Video Preprocessing..." : "视频预处理...");
                    }
                    else
                    {
                        doc_MainViewRecord.GUID = parentName;
                        if (DataBaseHelper.Default.Select(new Doc_MainViewRecord() { GUID = parentName }) != null)
                        {
                            workplan.SetProgress(8, Program.Language == "EN" ? "Data preprocessing..." : "数据预处理...");
                            workplan.SetError(Program.Language == "EN" ? "The recorded data already exists and cannot be imported repeatedly!" : "记录数据已经存在，不允许重复导入！");
                            goto last;
                        }
                        workplan.SetProgress(5, Program.Language == "EN" ? "Data preprocessing..." : "数据预处理...");
                        string resultpath = string.Format("{0}\\Location\\{1}", Channel.Default.SystemSetting.SaveEdfPath, parentName);
                        FolderHelper.FolderMove(f, string.Format("{0}\\Location", Channel.Default.SystemSetting.SaveEdfPath));
                        ///读取文件中的医生信息
                        Doc_Doctor doc_Doctor = DoctorUnit.Default.Read(resultpath);
                        if (doc_Doctor != null)
                        {
                            Doc_Doctor find = m_DoctorList.Find(t => t.UserID == doc_Doctor.UserID);
                            if (find == null)
                            {
                                DataBaseHelper.Default.Insert(doc_Doctor);
                                doc_Doctor = DataBaseHelper.Default.Select(new Doc_Doctor() { UserID = doc_Doctor.UserID });
                                doc_MainViewRecord.DoctorID = doc_Doctor.ID.ToString();
                            }
                            else
                            {
                                doc_MainViewRecord.DoctorID = find.ID.ToString();
                            }
                            Channel.Default.Doctor = doc_Doctor;
                        }
                        else
                        {
                            //如果没有医生记录，默认取医生列表的第一个人
                            doc_MainViewRecord.DoctorID = m_DoctorList[0].ID.ToString();
                        }
                        if (workplan.CancellationPending)
                        {
                            goto last;
                        }
                        ///读取病人信息
                        Doc_PatientInfo doc_PatientInfo = PatientUnit.Default.Read(resultpath);
                        if (doc_PatientInfo != null)
                        {
                            DataModel.LogInstance.Default.AddLog(string.Format("用户在右键菜单栏中点击 附加记录数据 病例号为 {0}", doc_PatientInfo.PatientNo));
                            Doc_PatientInfo find = DataBaseHelper.Default.Select(new Doc_PatientInfo() { PatientNo = doc_PatientInfo.PatientNo });
                            if (find == null)
                            {
                                DataBaseHelper.Default.Insert(doc_PatientInfo);
                            }
                            doc_MainViewRecord.PatientID = doc_PatientInfo.PatientNo;
                            Channel.Default.Patient = doc_PatientInfo;
                        }
                        if (workplan.CancellationPending)
                        {
                            goto last;
                        }
                        doc_MainViewRecord.Progress = (int)ProgressState.Temporary;
                        doc_MainViewRecord.LoginID = Channel.Default.Loginer.ID;
                        doc_MainViewRecord.RecordTime = doc_MainViewRecord.CreatTime = DateTime.Now;
                        doc_MainViewRecord.DifferentVersion = 1;
                        doc_MainViewRecord.ReportReady = false;
                    }
                }
                if (workplan.CancellationPending)
                {
                    goto last;
                }
                workplan.SetProgress(9, Program.Language == "EN" ? "Data preprocessing..." : "数据预处理...");
                if (File.Exists(new Vedio.MyConfiguration() { SaveDirectory = Channel.Default.SystemSetting.VedioSavePath, MatchKey = doc_MainViewRecord.MatchKey }.FileName))
                {
                    doc_MainViewRecord.Reserve3 = Channel.Default.SystemSetting.VedioSavePath;
                    doc_MainViewRecord.VideoHave = true;//做处理
                }
                doc_MainViewRecord.ModeType = Channel.Default.SystemSetting.ModeType;
                DataBaseHelper.Default.Insert(doc_MainViewRecord);
                Channel.Default.AnalysisReult.Tag = DataModel.DataBaseHelper.Default.Select(new DataBaseCom.Doc_MainViewRecord() { GUID = doc_MainViewRecord.GUID }).ID;
                workplan.Argument = new object[] { doc_MainViewRecord.EdfPath, true, doc_MainViewRecord.GUID };
            }
            else
            {
                workplan.SetProgress(10, Program.Language == "EN" ? "Error extracting file" : "解压文件出错");
                DataModel.LogInstance.Default.AddLog("附加记录数据，解压文件出错",pSystem.LogManagement.LogLevel.ERROR);
                return false;
            }
            FolderHelper.FolderDelete(unzipPath);
            ZipHelper.GetRunStateHandler -= ZipHelper_GetRunStateHandler;
            workplan.SetProgress(10, Program.Language == "EN" ? "Data preprocessing completed" : "数据预处理完成");
            return true;
        last:
            FolderHelper.FolderDelete(unzipPath);
            ZipHelper.GetRunStateHandler -= ZipHelper_GetRunStateHandler;
            e.Cancel = true;
            return false;
        }
        //打开edf所在位置
        private void LookedfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalysisRecordViewData record = LookedfToolStripMenuItem.Tag as AnalysisRecordViewData;
            if (File.Exists(record.EdfPath))
                OpenFolder("explorer", string.Format("/select,{0}", record.EdfPath));
            else
                AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "Edf is missing and cannot be located!" : "Edf缺失无法定位！");
        }
        //打开视频所在位置
        private void LookVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalysisRecordViewData record = LookedfToolStripMenuItem.Tag as AnalysisRecordViewData;
            string vediopath = string.Format("{0}\\{1}", Channel.Default.SystemSetting.VedioSavePath, record.MatchKey);
            OpenFolder("explorer", vediopath);
        }
        private void OpenFolder(string filename,string args)
        {
            //创建启动对象
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            //设置运行文件
            startInfo.FileName = filename;
            startInfo.Arguments = args;
            //设置启动动作,确保以管理员身份运行
            startInfo.Verb = "runas";
            //如果不是管理员，则启动UAC
            System.Diagnostics.Process.Start(startInfo);
        }

        #endregion
        #region 视频网络下载
        private Int32 m_lFindHandle = -1;
        private Int32 m_lDownHandle = -1;
        private void 载入网络视频ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Vedio.MyConfiguration vedioConf = new Vedio.MyConfiguration();
            vedioConf.UrlPath = Channel.Default.SystemSetting.VedioSourceUrl;
            vedioConf.Tag = LoadDownVedioToolStripMenuItem.Tag as AnalysisRecordViewData;
            FunctionControls.tools.ProgressTipForm.Defalut.Text = Program.Language == "EN" ? "Video Download" : "视频下载";
            FunctionControls.tools.ProgressTipForm.Defalut.DoWork += VideoLoadDown_DoWork;
            FunctionControls.tools.ProgressTipForm.Defalut.Argument = vedioConf;
            if (FunctionControls.tools.ProgressTipForm.Defalut.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void VideoLoadDown_DoWork(ProgressTipForm sender, DoWorkEventArgs e)
        {
            Vedio.MyConfiguration myConfiguration = e.Argument as Vedio.MyConfiguration;
            string[] ss = myConfiguration.UrlPath.Split(':');
            if (ss.Length == 2)
            {
                string ip = ss[0];
                try
                {
                    int port = int.Parse(ss[1]);
                    ReturnResult ret = new ReturnResult();
                    string savePath = "";
                    uint iLastErr = 0;
                    int m_lUserID = -1;
                    bool compelet = false;
                    bool m_bInitSDK = CHCNetSDK.NET_DVR_Init();
                    sender.SetProgress(1, Program.Language == "EN" ? "Log in to the video network..." : "登录视频网络...");
                    if (sender.CancellationPending)
                    {
                        goto end;
                    }
                    CHCNetSDK.NET_DVR_DEVICEINFO_V30 DeviceInfo = new CHCNetSDK.NET_DVR_DEVICEINFO_V30();
                    //登录设备 Login the device
                    m_lUserID = CHCNetSDK.NET_DVR_Login_V30(ip, port, myConfiguration.UserName, myConfiguration.PassWord, ref DeviceInfo);
                    if (m_lUserID < 0)
                    {
                        iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                        ret.IsOK = false;
                        //ret.OutMsg = "NET_DVR_Login_V30 failed, error code= " + iLastErr; //登录失败，输出错误号
                        //sender.SetError(ret.OutMsg);
                        sender.SetError(Program.Language == "EN" ? "Login device failed " : "登录设备失败");
                        goto end;
                    }
                    else
                    {
                        AnalysisRecordViewData record = myConfiguration.Tag as AnalysisRecordViewData;
                        DateTime dateTimeEnd = record.RecordTime.AddSeconds(record.FrameCount * 30);
                        DateTime dateTimeStart = record.RecordTime;
                        if (dateTimeStart == dateTimeEnd)
                            dateTimeEnd = DateTime.Now;
                        CHCNetSDK.NET_DVR_FILECOND_V40 fileinfo = new CHCNetSDK.NET_DVR_FILECOND_V40();
                        fileinfo.lChannel = 1;
                        fileinfo.dwFileType = 0xff; //0xff-全部，0-定时录像，1-移动侦测，2-报警触发，...
                        fileinfo.dwIsLocked = 0xff; //0-未锁定文件，1-锁定文件，0xff表示所有文件（包括锁定和未锁定）
                                                    //设置录像查找的开始时间 Set the starting time to search video files
                        fileinfo.struStartTime.dwYear = (uint)dateTimeStart.Year;
                        fileinfo.struStartTime.dwMonth = (uint)dateTimeStart.Month;
                        fileinfo.struStartTime.dwDay = (uint)dateTimeStart.Day;
                        fileinfo.struStartTime.dwHour = (uint)dateTimeStart.Hour;
                        fileinfo.struStartTime.dwMinute = (uint)dateTimeStart.Minute;
                        fileinfo.struStartTime.dwSecond = (uint)dateTimeStart.Second;

                        //设置录像查找的结束时间 Set the stopping time to search video files
                        fileinfo.struStopTime.dwYear = (uint)dateTimeEnd.Year;
                        fileinfo.struStopTime.dwMonth = (uint)dateTimeEnd.Month;
                        fileinfo.struStopTime.dwDay = (uint)dateTimeEnd.Day;
                        fileinfo.struStopTime.dwHour = (uint)dateTimeEnd.Hour;
                        fileinfo.struStopTime.dwMinute = (uint)dateTimeEnd.Minute;
                        fileinfo.struStopTime.dwSecond = (uint)dateTimeEnd.Second;

                        sender.SetProgress(20, Program.Language == "EN" ? "Search for video records... " : "查找视频记录...");
                        if (sender.CancellationPending)
                        {
                            goto end;
                        }
                        //开始录像文件查找 Start to search video files 
                        m_lFindHandle = CHCNetSDK.NET_DVR_FindFile_V40(m_lUserID, ref fileinfo);
                        if (m_lFindHandle < 0)
                        {
                            iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                            ret.IsOK = false;
                            //ret.OutMsg = "NET_DVR_FindFile_V40 failed, error code= " + iLastErr; //预览失败，输出错误号
                            //sender.SetError(ret.OutMsg);
                            sender.SetError(Program.Language == "EN" ? "Video file search failed" : "录像文件查找失败");
                        }
                        else
                        {
                            sender.SetProgress(30, Program.Language == "EN" ? "Search for video records..." : "查找视频记录...");
                            if (sender.CancellationPending)
                            {
                                goto end;
                            }
                            CHCNetSDK.NET_DVR_FINDDATA_V30 struFileData = new CHCNetSDK.NET_DVR_FINDDATA_V30();
                            List<string> vedionames = new List<string>();
                            while (true)
                            {
                                //逐个获取查找到的文件信息 Get file information one by one.
                                int result = CHCNetSDK.NET_DVR_FindNextFile_V30(m_lFindHandle, ref struFileData);

                                if (result == CHCNetSDK.NET_DVR_ISFINDING)  //正在查找请等待 Searching, please wait
                                {
                                    continue;
                                }
                                else if (result == CHCNetSDK.NET_DVR_FILE_SUCCESS) //获取文件信息成功 Get the file information successfully
                                {
                                    vedionames.Add(struFileData.sFileName);

                                }
                                else if (result == CHCNetSDK.NET_DVR_FILE_NOFIND || result == CHCNetSDK.NET_DVR_NOMOREFILE)
                                {
                                    break; //未查找到文件或者查找结束，退出   No file found or no more file found, search is finished 
                                }
                                else
                                {
                                    break;
                                }
                            }
                            sender.SetProgress(99, string.Format(Program.Language == "EN" ? "Found {0} video records" : "查找到视频记录{0}个", vedionames.Count));
                            if (sender.CancellationPending)
                            {
                                goto end;
                            }
                            CHCNetSDK.NET_DVR_FindClose_V30(m_lFindHandle);
                            System.Threading.Thread.Sleep(1000);
                            sender.SetProgress(0, Program.Language == "EN" ? "Start downloading..." : "开始下载...");
                            if (sender.CancellationPending)
                            {
                                goto end;
                            }
                            savePath = string.Format("{0}{1}", Directory.GetDirectoryRoot(Channel.Default.SystemSetting.VedioSavePath), record.MatchKey);
                            if (!Directory.Exists(savePath))
                            {
                                Directory.CreateDirectory(savePath);
                            }
                            string vname = Path.GetFileName(savePath);
                            for (int i = 0; i < vedionames.Count; i++)
                            {
                                if (m_lDownHandle >= 0)
                                {
                                    //ret.OutMsg = "Downloading, please stop firstly!";//正在下载，请先停止下载
                                    e.Cancel = true;
                                    //sender.SetError(ret.OutMsg);
                                    sender.SetError(Program.Language == "EN" ? "Downloading video file, please stop downloading first" : "正在下载视频文件，请先停止下载");
                                    return;
                                }
                                string sVideoFileName;  //录像文件保存路径和文件名 the path and file name to save      
                                ///路径必须是英文名称
                                sVideoFileName = string.Format("{0}\\{1}{2}.mp4", savePath, vname, i == 0 ? "" : string.Format("_{0}", i));

                                //按文件名下载 Download by file name
                                m_lDownHandle = CHCNetSDK.NET_DVR_GetFileByName(m_lUserID, vedionames[i], sVideoFileName);
                                if (m_lDownHandle < 0)
                                {
                                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                                    ret.OutMsg = "NET_DVR_GetFileByName failed, error code= \r\n" + iLastErr;
                                    continue;
                                }

                                uint iOutValue = 0;
                                if (!CHCNetSDK.NET_DVR_PlayBackControl_V40(m_lDownHandle, CHCNetSDK.NET_DVR_PLAYSTART, IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue))
                                {
                                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                                    ret.OutMsg = "NET_DVR_PLAYSTART failed, error code= " + iLastErr; //下载控制失败，输出错误号
                                    continue;
                                }
                                int iPos = 0;
                                while (true)
                                {
                                    //获取下载进度
                                    iPos = CHCNetSDK.NET_DVR_GetDownloadPos(m_lDownHandle);
                                    if (iPos >= 100)
                                    {
                                        m_lDownHandle = -1;
                                        break;
                                    }
                                    sender.SetProgress(iPos, string.Format(Program.Language == "EN" ? "Video recording download ({0}/{1})... " : "视频记录下载({0}/{1})...", i + 1, vedionames.Count));
                                    if (sender.CancellationPending)
                                    {
                                        goto last;
                                    }
                                    Trace.WriteLine(string.Format("视频文件{1}下载进度{0}%", iPos, vedionames[i]));
                                    System.Threading.Thread.Sleep(500);
                                }
                            }
                            ///把视频记录从临时文件夹复制到视频存储路径下
                            FolderHelper.FolderMove(savePath, Channel.Default.SystemSetting.VedioSavePath);
                            DataBaseHelper.Default.Update(new Doc_MainViewRecord() { ID = record.ID }, new Doc_MainViewRecord() { VideoHave = true, Reserve3 = Channel.Default.SystemSetting.VedioSavePath });
                            compelet = true;
                        }
                        goto end;
                    }
                last:
                    CHCNetSDK.NET_DVR_StopGetFile(m_lFindHandle);
                end:
                    if (m_lUserID >= 0)
                    {
                        if (!CHCNetSDK.NET_DVR_Logout(m_lUserID))
                        {
                            iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                            //ret.OutMsg = "NET_DVR_Logout failed, error code= " + iLastErr;
                            //sender.SetError(ret.OutMsg);
                            sender.SetError(Program.Language == "EN" ? "File download failed" : "文件下载失败");
                        }
                        else
                        {
                            m_lUserID = -1;
                            m_lDownHandle = -1;
                            m_lFindHandle = -1;
                        }
                    }
                    if (m_bInitSDK)
                    {
                        CHCNetSDK.NET_DVR_Cleanup();
                        m_bInitSDK = false;
                    }
                    e.Cancel = true;
                    if (Directory.Exists(savePath))
                    {
                      FolderHelper.FolderDelete(savePath);
                    }
                    if (compelet)
                        sender.SetProgress(100, Program.Language == "EN" ? "Download complete" : "完成下载");
                }
                catch (Exception ee)
                {
                    Trace.WriteLine(ee.Message);
                }
            }
            else
            {
                AhDung.MessageTip.ShowWarning(Program.Language == "EN" ? "Please confirm that the video network address is correct!" : "请确认视频网络地址正确！");
            }
        }
        #endregion

    }
}
