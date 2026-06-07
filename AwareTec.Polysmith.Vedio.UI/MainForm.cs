using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.Vedio.UI
{
    public partial class MainForm : SkinForm
    {
        private List<VedioPlayer> m_Plays = new List<VedioPlayer>();
        private bool m_AutoKill = false;
        public MainForm()
        {
            InitializeComponent();
            this.TopMost = true;
            this.Load += MainForm_Load;
            this.FormClosed += MainForm_FormClosed;
            this.FormClosing += MainForm_FormClosing;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!m_AutoKill)
            {
                if (MessageBoxMidle.Show(this, "是否关闭视频监控？", "信息确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            for (int i = 0; i < m_Plays.Count; i++)
            {
                m_Plays[i].myDispose();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            for(int i = 0; i < m_Plays.Count; i++)
            {
                this.panel1.Controls.Add(m_Plays[i]);
            }
            this.Left = Screen.AllScreens[0].WorkingArea.Right - this.Width;
            this.Top = Screen.AllScreens[0].WorkingArea.Bottom - this.Height;
            this.StartPosition = FormStartPosition.Manual;
            this.BringToFront();
        }

        public void CreatVedioPlayer(string urlPath, string saveDirectory, string matchKey,bool NotSplitRecordFile,int MaxRecodFileLenght)
        {
            VedioPlayer player = new VedioPlayer(new MyConfiguration() { UrlPath = urlPath, MatchKey = matchKey, SaveDirectory = saveDirectory });
            VedioManagement.Default.NotSplitRecordFile = NotSplitRecordFile;
            VedioManagement.Default.MaxRecodFileLenght = MaxRecodFileLenght;
            player.IsRealView = true;
            player.Dock = DockStyle.Fill;
            m_Plays.Add(player);
        }

        protected override void DefWndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case WinMessageUtil.WM_COPYDATA:
                    string str = WinMessageUtil.ReceiveMessage(ref m);
                    if (str == "SampleStop")
                    {
                        m_AutoKill = true;
                        this.Close();
                    }
                    break;
                default:
                    break;
            }
            base.DefWndProc(ref m);
        }
    }
}
