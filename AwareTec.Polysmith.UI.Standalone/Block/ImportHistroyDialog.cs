using AwareTec.Polysmith.UI.DataModel;
using AwareTec.Polysmith.UI.FunctionControls.tools;
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
    public partial class ImportHistroyDialog : SkinForm
    {
        private string m_basePath = Application.StartupPath + "\\Data";
        public ImportHistroyDialog()
        {
            InitializeComponent();
            this.FormClosing += ImportHistroyDialog_FormClosing;
            AutoSearchRadioButton.CheckedChanged += AutoSearchRadioButton_CheckedChanged;
        }

        private void AutoSearchRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(AutoSearchRadioButton.Checked)
            {
                HandSearchTextBox.Text = null;
            }
            else
            {

            }
        }

        bool autoClose = false;
        private void ImportHistroyDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (autoClose)
            {
                e.Cancel = true;
                autoClose = false;
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            //DialogResult = DialogResult.Cancel;
            if (AutoSearchRadioButton.Checked)
            {
                if (!string.IsNullOrEmpty(Channel.Default.MatchKey))
                {///找到文件路径下日期最新的用户数据
                    FunctionControls.tools.ProgressTipForm.Defalut.Text = Program.Language=="EN"? "Automatic Search" : "自动搜索";
                    FunctionControls.tools.ProgressTipForm.Defalut.DoWork += Defalut_DoWork;
                    if (FunctionControls.tools.ProgressTipForm.Defalut.ShowDialog() == DialogResult.OK)
                    {
                        if (MessageForm.Show(string.Format(Program.Language == "EN" ? "Match to file: {0}, do you want to continue?" : "匹配到文件：{0},是否继续？", EdfPath), Program.Language == "EN" ? "Message" : "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                }
                else
                {
                    MessageForm.Show("无法自动搜索病人数据，请手动选择！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    autoClose = true;
                }
            }
            else
            {
                if (HandSearchTextBox.Text == "")
                {
                    AhDung.MessageTip.ShowError("当前未选中任何EDF文件，请重新选择！");
                    return;
                }
                EDF edf = EDF.Default.ConvertToEDF(EDF.Default.ReadFile(HandSearchTextBox.Text, false));
                if (edf.getMatchKey().Contains(Channel.Default.MatchKey) || edf.getMatchKey().Contains(Channel.Default.MatchKey.Substring(0, Channel.Default.MatchKey.Length - 1)))
                {
                    EdfPath = HandSearchTextBox.Text;
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    AhDung.MessageTip.ShowError("文件不匹配，请重新选择！");
                }
                return;
            }
        }

        private void Defalut_DoWork(ProgressTipForm sender, DoWorkEventArgs e)
        {
            sender.SetProgress(2, "开始搜索可移动存储盘符 ...");
            string[] dirs = Environment.GetLogicalDrives(); //取得所有的盘符 
            int cnt = 0;
            string matchKey = Channel.Default.MatchKey;
            if (sender.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            int process2 = 5;
            foreach (string dir in dirs)
            {
                DriveInfo Tdriver = new DriveInfo(dir);
                if (Tdriver.DriveType == DriveType.Removable)
                {
                    sender.SetProgress(process2, string.Format("检索可移动存储盘符{0} ...", Tdriver.Name));
                    if (sender.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                    string[] files = Directory.GetFiles(dir, "*.edf");
                    for (int i = 0; i < files.Length; i++)
                    {
                        string filename = Path.GetFileName(files[i]);
                        sender.SetProgress(process2++, string.Format("检索文件 {0} ", filename));
                        System.Threading.Thread.Sleep(50);
                        if (sender.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                        EDF edf = EDF.Default.ConvertToEDF(EDF.Default.ReadFile(files[i], false));
                        // if (m_DataSource.Select(string.Format("PatientNO='{0}' and Progress={1}", edf.PatientNO, (int)ProgressState.Ready)).Length > 0)
                        if (edf.getMatchKey().Contains(matchKey) || edf.getMatchKey().Contains(matchKey.Substring(0, matchKey.Length - 1)))
                        {
                            sender.SetProgress(80, "数据校验...");
                            System.Threading.Thread.Sleep(1000);
                            //record.EdfPath = path.Replace(Application.StartupPath, "..");
                            //DataBaseHelper.Default.Update(new Doc_MainViewRecord() { MatchKey = record.MatchKey }, new Doc_MainViewRecord() { EdfPath = record.EdfPath });
                            EdfPath = files[i];
                            cnt++;
                            break;
                        }
                    }
                    process2 += 10;
                }
            }
            if (cnt == 0)
            {
                if (sender.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                sender.SetProgress(process2, "开始搜索睡眠分析系统数据存储库 ...");
                string searPath= EDF.Default.DefaultEdfSavePath;
                DirectoryInfo[] sss = new DirectoryInfo(searPath).GetDirectories();
                try
                {
                    sss = sss.OrderByDescending(t => DateTime.Parse(t.Name)).ToArray();
                }
                catch { }
                if (sss.Length != 0)
                {
                    float offset = (float)((100.0 - process2) / sss.Length);
                    process2 += 2;
                    for (int s = 0; s < sss.Length; s++)
                    {
                        string[] files = Directory.GetFiles(sss[s].FullName, "*.edf");
                        sender.SetProgress(process2, string.Format("检索文件夹 {0} ", sss[s].FullName));
                        if (files.Length > 0)
                        {
                            float offset2 = offset / files.Length;
                            for (int i = 0; i < files.Length; i++)
                            {
                                EDF edf = EDF.Default.ConvertToEDF(EDF.Default.ReadFile(files[i], false));
                                System.Threading.Thread.Sleep(50);
                                if (sender.CancellationPending)
                                {
                                    e.Cancel = true;
                                    return;
                                }
                                string name = Path.GetFileName(files[i]);
                                sender.SetProgress(process2, string.Format("检索文件 {0} ", name));
                                if (edf.getMatchKey().Contains(matchKey) || edf.getMatchKey().Contains(matchKey.Substring(0, matchKey.Length - 1)))
                                {
                                    cnt++;
                                    sender.SetProgress(80, "数据校验...");
                                    System.Threading.Thread.Sleep(1000);
                                    EdfPath = files[i];
                                    break;
                                }
                                process2 = (int)(process2 + offset2);
                            }
                        }
                        else
                        {
                            process2 = (int)(process2 + offset);
                        }
                    }
                }
                if (cnt == 0)
                {
                    sender.SetProgress(100);
                    sender.SetError(Program.Language=="EN"? "No matching EDF files were found for the current case, please manually select" : "未发现与当前病例匹配的edf文件,请手动选择");
                    autoClose = true;
                    return;
                }
            }
            sender.SetProgress(100, "完成");
        }
        /// <summary>
        /// 选择的edf路径
        /// </summary>
        public string EdfPath { set; get; }
        /// <summary>
        /// 当前病人ID
        /// </summary>
        public string ID { set; get; }
        /// <summary>
        /// 监测日期
        /// </summary>
        public DateTime RecordTime { set; get; }


        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void AutoSearchRadioButton_CheckedChanged(object sender, EventArgs e)
        //{
        //    HandSearchTextBox.Visible = BrowseButton.Visible = !AutoSearchRadioButton.Checked;
        //}

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "EDF数据文件|*.edf";
            open.RestoreDirectory = true;
            open.FilterIndex = 1;
            if (open.ShowDialog() == DialogResult.OK)
            {
                HandSearchTextBox.Text = open.FileName;
                HandSearchRadioButton.Checked = true;
                AutoSearchRadioButton.Checked = false;
            }
        }
    }
}
