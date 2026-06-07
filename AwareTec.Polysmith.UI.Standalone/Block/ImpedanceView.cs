using pSystem.Communication.Com;
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
    public partial class ImpedanceView : SkinForm
    {
        Protocol.ProtocolServer m_protocol = null;
        private int m_LimitValue = 5000;
        private string m_ChannelPath = AppDomain.CurrentDomain.BaseDirectory + "Impedance.txt";
        public ImpedanceView()
        {
            InitializeComponent();
            label1.Text = Program.Language=="EN"? "Note: Different colors indicate the impedance range between the electrode and the skin. Green indicates good contact, yellow indicates average contact, and red indicates poor contact (if necessary, please reattach the electrode)" : "注：不同颜色指示电极与皮肤之间的阻抗范围,绿色表示接触良好，黄色表示接触一般，红色表示接触差(如有必要请重新粘贴电极)";
            this.Load += ImpedanceView_Load;
            this.FormClosed += ImpedanceView_FormClosed;
        }

       private void ImpedanceView_FormClosed(object sender, FormClosedEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                m_protocol.GetImpedanceDataRequest(false);
                m_protocol.RecImpedanceDataHandle -= protocol_RecImpedanceDataHandle;
            });
        }

        private void ImpedanceView_Load(object sender, EventArgs e)
        {
            string[] IDList = new string[] { "EEG 1", "EEG 2", "EEG 3", "EEG 4", "EEG 5", "EEG 6", "EEG 7", "EEG 8", "EEG 9", "EEG 10", "EEG 11", "EEG 12", "EEG 13", "EEG 14", "EEG 15", "EEG 16" };
            string[] NameList = new string[] { "E2", "F3", "E1", "M2", "C3", "C4", "O1", "F4", "LAT+", "RAT+", "M1", "O2", "RAT-", "LAT-", "Chin2", "ChinZ" };
            if (File.Exists(m_ChannelPath))
            {
                using (FileStream fs = new FileStream(m_ChannelPath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string strValue = sr.ReadLine();
                        Dictionary<string, string> readValues = new Dictionary<string, string>();
                        while (!string.IsNullOrEmpty(strValue))
                        {
                            string[] ss = strValue.Split('=');
                            if (ss.Length == 2)
                            {
                                readValues.Add(ss[0].Trim(), ss[1].Trim());
                            }
                            strValue = sr.ReadLine();
                        }
                        foreach (KeyValuePair<string, string> p in readValues)
                        {
                            abSelect.Rows.Add(p.Key, p.Value, "");
                        }
                    }
                }
            }
            if (abSelect.Rows.Count == 0)
            {
                for (int i = 0; i < NameList.Length; i++)
                {
                    abSelect.Rows.Add(IDList[i], NameList[i], "");
                }
            }
            for (int i = 0; i < abSelect.Rows.Count; i++)
            {
                LinkAndImageCell df = abSelect.Rows[i].Cells[2] as LinkAndImageCell;
                {
                    Bitmap bitmap = new Bitmap(16, 16);
                    bitmap.MakeTransparent();
                    df.Image = bitmap;
                    df.Image.Tag = 0;
                }
                df.ReadOnly = true;
                df.TrackVisitedState = false;
                df.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
                df.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        public void Init(Protocol.ProtocolServer protocol)
        {
            m_protocol = protocol;
            protocol.RecImpedanceDataHandle += protocol_RecImpedanceDataHandle;
            Task.Factory.StartNew(() =>
            {
                protocol.GetImpedanceDataRequest(true);
            });

        }

        private void protocol_RecImpedanceDataHandle(uint[] UserData)
        {
            for (int i = 0; i < abSelect.Rows.Count; i++)
            {
                uint value = UserData[i];
                abSelect.Rows[i].Cells[2].Value = string.Format("{0:f2} KOhm", value/1000.0);
                LinkAndImageCell df = abSelect.Rows[i].Cells[2] as LinkAndImageCell;
                df.Image = value <= m_LimitValue ? Properties.Resources.LGreen : value > m_LimitValue && value <= (uint)(m_LimitValue * 1.4) ? Properties.Resources.LYellow : Properties.Resources.LRed;
            }
            abSelect.Invalidate();
        }

        private void Dose5kRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Dose5kRadioButton.Checked)
            {
                m_LimitValue = 5000;
                DataModel.LogInstance.Default.AddLog("用户选择阻抗值为 5k",pSystem.LogManagement.LogLevel.DEBUG);
            }
            else if (Dose10kRadioButton.Checked)
            {
                m_LimitValue = 10000;
                DataModel.LogInstance.Default.AddLog("用户选择阻抗值为 10k",pSystem.LogManagement.LogLevel.DEBUG);
            }
            else
            {
                m_LimitValue = 15000;
                DataModel.LogInstance.Default.AddLog("用户选择阻抗值为 15k",pSystem.LogManagement.LogLevel.DEBUG);
            }
            for (int i = 0; i < abSelect.Rows.Count; i++)
            {
                string strValue = abSelect.Rows[i].Cells[2].Value.ToString();
                if (!string.IsNullOrEmpty(strValue))
                {
                    uint value = (uint)(float.Parse(strValue.Split(' ')[0])*1000);
                    LinkAndImageCell df = abSelect.Rows[i].Cells[2] as LinkAndImageCell;
                    df.Image = value <= m_LimitValue ? Properties.Resources.LGreen : value > m_LimitValue && value <= (uint)(m_LimitValue * 1.4) ? Properties.Resources.LYellow : Properties.Resources.LRed;
                }
            }
            abSelect.Invalidate();
        }
    }
}
