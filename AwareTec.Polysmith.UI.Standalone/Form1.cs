using AwareTec.Polysmith.pChart;
using AwareTec.Polysmith.Protocol;
using pSystem.Communication.Com;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using AwareTec.Polysmith.Util;
using AwareTec.Polysmith.DataBaseCom;
namespace AwareTec.Polysmith.UI
{
    public partial class Form1 : SkinForm
    {
        private string _path = AppDomain.CurrentDomain.BaseDirectory + "channels.cfg";
        IOClient m_client = null;
        private bool m_fnstop = false;
        private string m_portname = "";
        Configuration m_appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        Protocol.ProtocolServer m_Protocol;
        DataTable m_Indextable ;
        private ChannelFiliter m_ChannelFiliter = null;
        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer
| ControlStyles.ResizeRedraw
| ControlStyles.Selectable
| ControlStyles.AllPaintingInWmPaint
| ControlStyles.UserPaint
| ControlStyles.SupportsTransparentBackColor,
true);
            CheckForIllegalCrossThreadCalls = false;
            m_KillTask = false;
            TaskStart();
            m_ChannelFiliter = new ChannelFiliter();
            m_ChannelFiliter.InitRate();
            this.Load += Form1_Load;
            this.FormClosing += Form1_FormClosing;
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            smithChart1.Disposed();
            m_fnstop = true;
            this.BeginInvoke(new MethodInvoker(() =>
            {
                m_client.Close();
            }));
            m_KillTask = true;
            th.Abort();
        }
        #region 内存回收
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        private static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        /// <summary>
        /// 释放内存
        /// </summary>
        private void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
        #endregion
        /// <summary>
        /// 定时清理系统垃圾的线程
        /// </summary>
        private System.Threading.Thread th = null;
        /// <summary>
        /// 中断任务标志
        /// </summary>
        private bool m_KillTask = true;
        /// <summary>
        /// 任务执行器
        /// </summary>
        private void TaskStart()
        {
            th = new System.Threading.Thread(() =>
            {
                while (!m_KillTask)
                {
                    ClearMemory();
                    System.Threading.Thread.Sleep(10000);
                }
            });
            th.IsBackground = true;
            th.SetApartmentState(System.Threading.ApartmentState.MTA);
            th.Start();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            m_fnstop = !m_fnstop;
        }
        private int[] m_strTimeSpan = null;
        private DataTable m_DefineData = null;
        void Form1_Load(object sender, EventArgs e)
        {
            string port = ConfigurationManager.AppSettings["Port"];
            string[] portList = SerialPort.GetPortNames();
            if (portList.Contains(port))
            {
                m_portname = port;
                port=string.Format("BlueTooch({0})", port);
                comboBoxEx1.Items.Add(port);
                comboBoxEx1.Text =port;
            }
            m_strTimeSpan = new int[comboBoxEx2.Items.Count];
            for (int i = 0; i < comboBoxEx2.Items.Count; i++)
            {
                string[] ss = comboBoxEx2.Items[i].ToString().Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                string str = ss[1].ToUpper();
                if (str == "S")
                    m_strTimeSpan[i] = int.Parse(ss[0]) * 1000;
                else if (str == "MIN")
                {
                    m_strTimeSpan[i] = int.Parse(ss[0]) * 60000;
                }
                else if (str == "H")
                {
                    m_strTimeSpan[i] = int.Parse(ss[0]) * 3600000;
                }
            }
            comboBoxEx2.SelectedIndex = 1;
            smithChart1.SerilArea.xAxis.MaxValue = m_strTimeSpan[comboBoxEx2.SelectedIndex];
            m_client = new MySerialPort("Com1");
            m_client.Delay = 10;
            m_Protocol = new Protocol.ProtocolServer(m_client, ConvertToChannel(Channel.Default.ChannelProperties));
            m_Protocol.AddLog += ser_AddLog;
           // m_Protocol.RecDataEventHandle += ser_RecDataEventHandle;
            initDataItem();
            CreatChannel();
            smithChart1.SerilArea.DrawImageBeforeHandle += SerilArea_DrawImageBeforeHandle;
            smithChart1.ChannelViewPopupHandler += smithChart1_ChannelViewPopupHandler;
            smithChart1.ChannelHeadPopupHandler += smithChart1_ChannelHeadPopupHandler;
            comboBoxEx4.SelectedIndex = 0;
            comboBoxEx3.SelectedIndex = 3;
            comboBoxEx5.SelectedIndex = 2;
        }

        void smithChart1_ChannelHeadPopupHandler(CurveItem channel, MouseButtons buttons)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                contextMenuStrip3.Items.Clear();
                if (buttons == MouseButtons.Right)
                {
                    ToolStripMenuItem ts = new ToolStripMenuItem();
                    ts.Name = "copy";
                    ts.Text = "复制";
                    ts.Click += ts_Click;
                    var yy = channel.ClientRectanglePoints.Select(t => t.Y);
                    if (yy.Count() > 0)
                    {
                        ts.Tag = (yy.Max() - yy.Min());
                        contextMenuStrip3.Items.Add(ts);
                        contextMenuStrip3.Show(Cursor.Position);
                    }
                }
            }));
        }

        void ts_Click(object sender, EventArgs e)
        {
            Clipboard.SetText((sender as ToolStripMenuItem).Tag.ToString());
        }
        private void smithChart1_ChannelViewPopupHandler(CurveItem channel, MouseButtons buttons)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                if (buttons == MouseButtons.Left)
                {
                    Info inf = new Info();
                    inf.PixelRate = m_PRate;
                    if (channel.ClientRectanglePoints.Count > 0)
                    {
                        List<float> values = channel.ClientRectanglePoints.Select(t => t.Y).ToList();
                        float max = values.Max();
                        int idxs = values.IndexOf(max);
                        float fx = (float)(max * 0.37);
                        float fx1 = (float)(fx + fx * 0.01), fx2 = (float)(fx - fx * 0.01);
                        float time = 0;
                        for (int i = idxs; i < values.Count; i++)
                        {
                            if (values[i] >= fx2 && values[i] <= fx1)
                            {
                                time = channel.TimeSpan * (i - idxs);
                                break;
                            }
                        }
                        { inf.MaxValue = max; inf.MinValue = values.Min(); inf.ValueCount = values.Count; inf.TimeValue = time; }
                    }
                    inf.Text += string.Format("({0})", channel.Name);
                    inf.TopMost = true;
                    inf.ShowDialog();
                }
                else
                {
                    contextMenuStrip2.Show(this.PointToScreen(Cursor.Position));
                }

            }));
        }
        private bool m_NeedRefresh = false;
        private float m_PRate = 0;
        private float m_ZoomRate = 1;
       private void SerilArea_DrawImageBeforeHandle()
        {
            if (m_NeedRefresh)
            {
                smithChart1.SerilArea.xAxis.MaxValue = m_strTimeSpan[comboBoxEx2.SelectedIndex];
                for (int i = 0; i < m_DefineData.Rows.Count; i++)
                {
                    CurveItem find = smithChart1.SerilArea.Find(m_DefineData.Rows[i]["ID"].ToString());
                    find.Name = m_DefineData.Rows[i]["strName"].ToString();
                    find.Visible = Convert.ToBoolean(m_DefineData.Rows[i]["State"]);
                    find.ChannelNo = Convert.ToByte(m_DefineData.Rows[i]["Index"]);
                    find.yAxis.MaxValue = Convert.ToInt32(m_DefineData.Rows[i]["MaxValue"]);
                    find.yAxis.MinValue = Convert.ToInt32(m_DefineData.Rows[i]["MinValue"]);
                    find.PixelRate = m_PRate;
                    find.ValueZoomRate = m_ZoomRate;
                    find.xAxis.MaxValue = m_strTimeSpan[comboBoxEx2.SelectedIndex];
                    find.TimeSpan = Convert.ToInt32(m_DefineData.Rows[i]["TimeSpan"]);
                }
                m_NeedRefresh = false;
            }
        }
        private void initDataItem()
        {
            m_Indextable = new DataTable();
            m_Indextable.Columns.Add("strName");
            m_Indextable.Columns.Add("FrameCount");
            m_Indextable.Columns.Add("MissCount");
            m_Indextable.Columns.Add("ID");
            m_Indextable.Columns.Add("FrameIndex");
            abSelect.DataSource = m_Indextable;
            m_DefineData = new DataTable();
            m_DefineData.Columns.Add("State", typeof(bool));
            m_DefineData.Columns.Add("strName");
            m_DefineData.Columns.Add("TimeSpan");
            m_DefineData.Columns.Add("MaxValue");
            m_DefineData.Columns.Add("MinValue");
            m_DefineData.Columns.Add("Index", typeof(int));
            m_DefineData.Columns.Add("ID");
            string strvalue = ReadIds();
            if (strvalue != "")
            {
                string[] strValues = strvalue.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < strValues.Length; i++)
                {
                    string[] ss = strValues[i].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    m_DefineData.Rows.Add(ss);
                }
            }
        }
        private int m_channelCnt = 0;
        private string[] strIndex = new string[] { "HeadElectricDiagram_1", "HeadElectricDiagram_2", "HeadElectricDiagram_3", "HeadElectricDiagram_4", "HeadElectricDiagram_5", "HeadElectricDiagram_6",
                                                  "EyesElectricDiagram_1","EyesElectricDiagram_2","ElectrocarDiogram","Snoring","MandibleElectricDiagram","ChestBreathing","AbdominalBreathing","RightLegMove"
                                                  ,"LeftLegMove","PressureMuzzleFlow","ThermalMuzzleFlow","BloodOxygen","BodyState","AmbientLight","PulseRate","BodyMovement"};
        private void CreatChannel()
        {
            foreach (string Key in strIndex)
            {
               DataRow[] dr= m_DefineData.Select(string.Format( "ID='{0}'",Key));
               int ymaxValue = 5;
               int yminValue = 0;
               bool Visible = true;
               int TimeSpan = 10;
               int channelNo = 0;
               if (dr.Length > 0)
               {
                   Visible = Convert.ToBoolean(dr[0]["State"]);
                   ymaxValue = Convert.ToInt32(dr[0]["MaxValue"]);
                   yminValue = Convert.ToInt32(dr[0]["MinValue"]);
                   TimeSpan = Convert.ToInt32(dr[0]["TimeSpan"]);
                   channelNo = Convert.ToByte(dr[0]["Index"]);
               }
                CurveItem item = new CurveItem();
                item.AutoCalibrationsEnable = false;
                try
                {
                    item.yAxis.MaxValue = ymaxValue;
                    item.yAxis.MinValue = yminValue;
                    item.Visible = Visible;
                }
                catch { }
                item.xAxis.MaxValue = smithChart1.SerilArea.xAxis.MaxValue;
                item.ID = Key;
                switch (Key)
                {
                    case "HeadElectricDiagram_1":
                        item.Name = "脑电-1";
                        item.TimeSpan = 4;
                        item.PenColor = Color.Red;
                        break;
                    case "HeadElectricDiagram_2":
                        item.Name = "脑电-2";
                        item.TimeSpan = 4;
                        item.PenColor = Color.Red;
                        break;
                    case "HeadElectricDiagram_3":
                        item.Name = "脑电-3";
                        item.TimeSpan = 4;
                        item.PenColor = Color.Red;
                        break;
                    case "HeadElectricDiagram_4":
                        item.Name = "脑电-4";
                        item.TimeSpan = 4;
                        item.PenColor = Color.Red;
                        break;
                    case "HeadElectricDiagram_5":
                        item.Name = "脑电-5";
                        item.TimeSpan = 4;
                        item.PenColor = Color.Red;
                        break;
                    case "HeadElectricDiagram_6":
                        item.Name = "脑电-6";
                        item.TimeSpan = 4;
                        item.PenColor = Color.Red;
                        break;
                    case "HeadElectricDiagram_7":
                        item.Name = "脑电-7";
                        item.TimeSpan = 4;
                        item.PenColor = Color.Red;
                        break;
                    case "HeadElectricDiagram_8":
                        item.Name = "脑电-8";
                        item.TimeSpan = 4;
                        item.PenColor = Color.Red;
                        break;
                    case "EyesElectricDiagram_1":
                        item.Name = "眼电-1";
                        item.TimeSpan = 4;
                        item.PenColor = Color.Blue;
                        break;
                    case "EyesElectricDiagram_2":
                        item.Name = "眼电-2";
                        item.TimeSpan = 4;
                        item.PenColor = Color.Blue;
                        break;
                    case "ElectrocarDiogram":
                        item.Name = "心电";
                        item.TimeSpan = 4;
                        item.PenColor = Color.Green;
                        break;
                    case "MandibleElectricDiagram":
                        item.Name = "下颌肌电";
                        item.TimeSpan = 4;
                        item.PenColor = Color.Cyan;
                        break;
                    case "Snoring":
                        item.Name = "鼾声";
                        item.TimeSpan = 4;
                        item.PenColor = Color.DarkBlue;
                        item.ChannelNo = 12;
                        break;
                    ///100个胸部呼吸数据+100个腹部呼吸数据+1个右腿动数据+1个左腿动数据+ 1个电量数据+1个环境光数据,周期1s
                    case "ChestBreathing":
                        item.Name = "胸部呼吸";
                        item.TimeSpan = 10;
                        item.PenColor = Color.DeepSkyBlue;
                       // item.IsShowValue = true;
                        break;
                    case "AbdominalBreathing":
                        item.Name = "腹部呼吸";
                        item.TimeSpan = 10;
                        item.PenColor = Color.YellowGreen;
                        break;
                    case "RightLegMove":
                        item.Name = "右腿动";
                        item.TimeSpan = 1000;
                        item.PenColor = Color.DarkMagenta;
                        break;
                    case "LeftLegMove":
                        item.Name = "左腿动";
                        item.TimeSpan = 1000;
                        item.PenColor = Color.DarkGoldenrod;
                        break;
                    case "CurrentEnergy":
                        item.Name = "电量";
                        item.TimeSpan = 1000;
                        item.PenColor = Color.Gray;
                        break;
                    case "AmbientLight":
                        item.Name = "环境光";
                        item.TimeSpan = 1000;
                        item.PenColor = Color.Gray;
                        break;
                    case "ThermalMuzzleFlow":
                        item.Name = "热敏";
                        item.TimeSpan = 125;
                        item.PenColor = Color.Gray;
                        item.AutoCalibrationsEnable = true;
                        break;
                    case "PressureMuzzleFlow":
                        item.Name = "压力";
                        item.TimeSpan = 125;
                        item.PenColor = Color.Gray;
                        item.AutoCalibrationsEnable = true;
                        break;
                    case "BodyState":
                        item.Name = "体位";
                        item.TimeSpan = 1000;
                        item.PenColor = Color.DarkViolet;
                        break;
                    case "BloodOxygen":
                        item.Name = "血氧";
                        item.TimeSpan = 1000;
                        item.PenColor = Color.DarkViolet;
                        item.IsShowValue = true;
                        item.yAxis.CalibrationsVisible = true;
                        break;
                    case "PulseRate":
                        item.Name = "脉率";
                        item.TimeSpan = 1000;
                        item.PenColor = Color.DarkViolet;
                        break;
                    case "BodyMovement":
                        item.Name = "体动";
                        item.TimeSpan = 1000;
                        item.PenColor = Color.DarkViolet;
                        break;
                    default:
                        item.Name = Key;
                        item.TimeSpan = 20;
                        item.PenColor = Color.Gray;
                        break;
                }
                if (dr.Length > 0)
                {
                    item.TimeSpan = TimeSpan;
                    item.ChannelNo = channelNo;
                }
                else
                {
                    item.ChannelNo=m_channelCnt;
                    TimeSpan = item.TimeSpan;
                    m_DefineData.Rows.Add(Visible, item.Name, TimeSpan, ymaxValue, yminValue, m_channelCnt++, Key);
                }
                item.Tag = new ChannelFiliter() {HighPass_1S=m_ChannelFiliter.HighPass_1S.Clone(),LowPass_30Hz=m_ChannelFiliter.LowPass_30Hz.Clone(),LowPass_60Hz=m_ChannelFiliter.LowPass_60Hz.Clone(),SingleNotch=m_ChannelFiliter.SingleNotch.Clone(),HighPass_03S=m_ChannelFiliter.HighPass_03S.Clone() };
                item.FiliterDataEvent += item_FiliterDataEvent;
                smithChart1.SerilArea.AddCurve(item);
            }
        }
        private bool _hasHighPass = false;
        private bool _hasLowPass = false;
        private bool _hasSinglePass = false;
        private int bak_filiterBit = 0;
        /// <summary>
        /// 滤波实现
        /// </summary>
        /// <param name="datasource"></param>
        /// <returns></returns>
        private float[] item_FiliterDataEvent(CurveItem item, float[] datasource,int firstDataIndex,object filiter)
        {
            ChannelFiliter tag = (ChannelFiliter)filiter;
            if (bak_filiterBit != m_filiterBit)
            {
                tag.HighPass_1S.StartIdx = 0;
                tag.LowPass_30Hz.StartIdx = 0;
                tag.LowPass_60Hz.StartIdx = 0;
                tag.SingleNotch.StartIdx = 0;
                tag.HighPass_03S.StartIdx = 0;
                bak_filiterBit = m_filiterBit;
            }
            if (_hasHighPass)
            {
                if (sToolStripMenuItem1.Checked)
                    datasource = m_ChannelFiliter.Filiter(datasource, firstDataIndex, tag.HighPass_03S);
                else
                    datasource = m_ChannelFiliter.Filiter(datasource, firstDataIndex, tag.HighPass_1S);
            }
            if (_hasLowPass)
            {
                if (stoolStripMenuItem_30.Checked)
                    datasource = m_ChannelFiliter.Filiter(datasource, firstDataIndex, tag.LowPass_30Hz);
                else
                    datasource = m_ChannelFiliter.Filiter(datasource, firstDataIndex, tag.LowPass_60Hz);

            }
            if (_hasSinglePass)
                datasource = m_ChannelFiliter.Filiter(datasource, firstDataIndex, tag.SingleNotch);
            return datasource;
        }
        private bool m_stop = false;
        private ChannelFiliter xfChannel1 = null;
        private ChannelFiliter xfChannel2 = null;
        private ChannelFiliter xfChannel3 = null;
        private ChannelFiliter xfChannel4 = null;
        /// <summary>
        /// 接收到一个通道
        /// </summary>
        /// <param name="Channel"></param>
        private void m_Protocol_RecChannelEventHandle(DataDefine Channel)
        {
            pChart.CurveItem find = smithChart1.SerilArea.Find(Channel.Name);
            if (find != null)
            {
                Channel.RecOneDataEventHandle += find.AddDatavalue;
                if (Channel.Name == "BloodOxygen")
                {
                    Channel.RecOneDataEventHandle += BloodOxygen_RecOneDataEventHandle;
                }
                else if (Channel.Name == "BodyState")
                {
                    Channel.RecOneDataEventHandle += BodyState_RecOneDataEventHandle;
                }
                else if (Channel.Name == "AmbientLight")
                {
                    Channel.RecOneDataEventHandle += AmbientLight_RecOneDataEventHandle;
                }
                else if (Channel.Name == "PulseRate")
                {
                    Channel.RecOneDataEventHandle += PulseRate_RecOneDataEventHandle;
                }
                else if (Channel.Name == "ChestBreathing")
                {
                    Channel.RecOneDataEventHandle += ChestBreathing_RecOneDataEventHandle;
                    xfChannel1 = new ChannelFiliter(100);
                }
                else if (Channel.Name == "AbdominalBreathing")
                {
                    Channel.RecOneDataEventHandle += AbdominalBreathing_RecOneDataEventHandle;
                    xfChannel2 = new ChannelFiliter(100);
                }
                else if (Channel.Name == "PressureMuzzleFlow")
                {
                    Channel.RecOneDataEventHandle += PressureMuzzleFlow_RecOneDataEventHandle;
                    xfChannel3 = new ChannelFiliter(100);
                }
                else if (Channel.Name == "ThermalMuzzleFlow")
                {
                    Channel.RecOneDataEventHandle += ThermalMuzzleFlow_RecOneDataEventHandle;
                    xfChannel4 = new ChannelFiliter(100);
                }
            }
        }
        void ThermalMuzzleFlow_RecOneDataEventHandle(float data, bool Invaild, string msg)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                toolStripStatusLabel6.Text = string.Format("热敏呼吸率：{0}", xfChannel4.getHeartRate((int)data));
            }
            ));
        }
        void PressureMuzzleFlow_RecOneDataEventHandle(float data, bool Invaild, string msg)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                toolStripStatusLabel7.Text = string.Format("压力呼吸率：{0}", xfChannel3.getHeartRate((int)data));
            }
            ));
        }
        void AbdominalBreathing_RecOneDataEventHandle(float data, bool Invaild, string msg)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                toolStripStatusLabel9.Text = string.Format("腹部呼吸率：{0}", xfChannel2.getHeartRate((int)data));
            }
            ));
        }
        void ChestBreathing_RecOneDataEventHandle(float data, bool Invaild, string msg)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                toolStripStatusLabel8.Text = string.Format("胸部呼吸率：{0}", xfChannel1.getHeartRate((int)data));
            }
            ));
        }

        void PulseRate_RecOneDataEventHandle(float data, bool Invaild, string msg)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                toolStripStatusLabel5.Text = string.Format("脉率:{0}", data);
            }
            ));
        }

        private void AmbientLight_RecOneDataEventHandle(float data, bool Invaild, string msg)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                toolStripStatusLabel3.Text = string.Format("{0}", data==0x01?"开灯":"关灯");
            }
            ));
        }

        void BodyState_RecOneDataEventHandle(float data, bool Invaild, string msg)
        {
            /// 仰：0x01  左：0x02  右：0x03  趴：0x04  坐：0x05    
            this.Invoke(new MethodInvoker(() =>
            {
                toolStripStatusLabel1.Text = string.Format("体位：{0}", data == 0x01 ? "仰卧" : data == 0x02 ? "左侧" : data == 0x03 ? "右侧" : data == 0x04 ? "俯卧" : "坐立");
            }
        ));
        }

        private void BloodOxygen_RecOneDataEventHandle(float data, bool Invaild, string msg)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                toolStripStatusLabel4.Text = string.Format("血氧：{0}", data);
            }
            ));
        }
       private void ser_AddLog(pSystem.LogManagement.LogItem obj)
        {
            try
            {
                if (m_fnstop)
                    return;
                textBox1.AppendText(string.Format("{0}{1}\r\n", DateTime.Now.ToString(), obj.Description));
                if (textBox1.TextLength > textBox1.MaxLength)
                    textBox1.Text.Remove(0, 3000);
            }
            catch { }
        }

        void chart1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void 停止ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_fnstop)
            {
                m_fnstop = false;
                停止ToolStripMenuItem.Text = "开始";
            }
            else
            {
                m_fnstop = true;
                停止ToolStripMenuItem.Text = "停止";
            }
        }

        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void comboBoxEx1_DropDown(object sender, EventArgs e)
        {
            comboBoxEx1.Items.Clear();
            comboBoxEx1.Items.AddRange(GetPort());
        }

        private string[] GetPort()
        {
            List<string> ret = new List<string>();
            Microsoft.Win32.RegistryKey hklm = Microsoft.Win32.Registry.LocalMachine;
            Microsoft.Win32.RegistryKey software11 = hklm.OpenSubKey("HARDWARE");
            //打开"HARDWARE"子健
            Microsoft.Win32.RegistryKey software = software11.OpenSubKey("DEVICEMAP");
            Microsoft.Win32.RegistryKey sitekey = software.OpenSubKey("SERIALCOMM");
            //获取当前子健
            String[] Str2 = sitekey.GetValueNames();
            //Str2 = System.IO.Ports.SerialPort.GetPortNames();//第二中方法，直接取得串口值
            //获得当前子健下面所有健组成的字符串数组
            int ValueCount = sitekey.ValueCount;
            //获得当前子健存在的健值
            int i;
            for (i = 0; i < ValueCount; i++)
            {
                if (Str2[i].ToUpper().Contains("BTH"))
                {
                    ret.Add(string.Format("BlueTooch({0})", sitekey.GetValue(Str2[i])));
                }
            }
            return ret.ToArray();
        }

        private void buttonEx1_Click(object sender, EventArgs e)
        {
            if (m_portname == "")
                return;
            if (!m_client.MatchKey.Contains(m_portname))
                (m_client as MySerialPort).Port = m_portname;
            if (buttonEx1.Text == "启动")
            {
                smithChart1.SerilArea.xAxis.MaxValue = m_strTimeSpan[comboBoxEx2.SelectedIndex];
                for (int i = 0; i < strIndex.Length; i++)
                {
                    CurveItem item = smithChart1.SerilArea.Find(strIndex[i]);
                    {
                        try
                        {
                            item.xAxis.MaxValue = smithChart1.SerilArea.xAxis.MaxValue;
                        }
                        catch { }
                    }
                }
                m_ChannelFiliter.HighPass_1S.StartIdx = 0;
                m_ChannelFiliter.LowPass_30Hz.StartIdx = 0;
                m_ChannelFiliter.LowPass_60Hz.StartIdx = 0;
                m_ChannelFiliter.SingleNotch.StartIdx = 0;
                smithChart1.SerilArea.Clear();
                m_Protocol.FnStop = false;
                m_client.Open();
                //new Action(() =>
                //{
                    //if (m_Protocol.StartSampleRequest())
                    {
                        buttonEx1.Text = "断开";
                        comboBoxEx1.Enabled = false;
                        comboBoxEx5.Enabled = false;
                    }
                    m_appConfig.AppSettings.Settings["Port"].Value = m_portname;
                    m_appConfig.Save();
                //}).BeginInvoke(null,null);
            }
            else
            {
                Application.DoEvents();
               // if (m_Protocol.StopSampleRequest())
                {
                    m_client.Close();
                    m_Protocol.FnStop = true;
                    buttonEx1.Text = "启动";
                    comboBoxEx1.Enabled = true;
                    comboBoxEx5.Enabled = true;
                }
            }
        }

        private void comboBoxEx1_SelectedValueChanged(object sender, EventArgs e)
        {
            m_portname = comboBoxEx1.SelectedItem.ToString();
            if (m_portname != "")
            {
                m_portname = m_portname.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries)[1];
            }
        }

        private void buttonEx2_Click(object sender, EventArgs e)
        {
            ChannelConfig channel = new ChannelConfig();
            channel.RecSaveEventHandle += channel_RecSaveEventHandle;
            channel.Init(m_DefineData);
            channel.Owner = this;
            channel.ShowDialog();
        }

        private bool channel_RecSaveEventHandle(DataTable ChannelConfigTable)
        {
            m_DefineData = ChannelConfigTable;
            m_NeedRefresh = true;
            return true;
        }
        /// <summary>
        /// 读取通道信息
        /// </summary>
        /// <returns></returns>
        private string ReadIds()
        {
            string retValue = "";
            string ID = "Cap00000000";
            if (File.Exists(_path))
            {
                XmlDocument doc = new XmlDocument();
                StringBuilder sb = new StringBuilder();
                using (StreamReader sr = new StreamReader(_path, Encoding.UTF8))
                {
                    string sOneLine = "";
                    while (sr.Peek() > 0)
                    {
                        sOneLine = sr.ReadLine();
                        sb.AppendFormat("{0}\r\n", sOneLine);
                        System.Threading.Thread.Sleep(5);
                    }
                    sr.Close();
                }
                doc.LoadXml(sb.ToString());
                XmlNode xmls = doc.SelectSingleNode("Captures");
                foreach (XmlNode node in xmls.ChildNodes)
                {
                    if (node.Name == ID)
                    {
                        retValue = node.Attributes["Value"].InnerText;
                        break;
                    }
                }
            }
            return retValue;
        }

        private void comboBoxEx2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEx cb=(ComboBoxEx)sender;
            switch (cb.Name)
            {
                case "comboBoxEx2":
                    break;
                case "comboBoxEx4":
                    string[] strval = cb.SelectedItem.ToString().Split(' ');
                    if (strval.Length == 2)
                    {
                        int rr = Convert.ToInt32(strval[0]);
                        if (strval[1] == "mV/mm")
                            rr = rr * 1000;
                        m_PRate = rr;
                    }
                    break;
                case "comboBoxEx3":
                    m_ZoomRate = Convert.ToInt32(cb.SelectedItem.ToString());
                    break;
            }
            if (m_stop)
                smithChart1.Invalidate();
            m_NeedRefresh = true;
        }
        private string m_Path = AppDomain.CurrentDomain.BaseDirectory + "Data";
        private void buttonEx3_Click(object sender, EventArgs e)
        {
            m_Indextable.Rows.Clear();
            string m_Name="FrameN0.";
            String filePath =  string.Format("{0}\\{1}", m_Path, string.Format("{0}{1}.dat", m_Name, DateTime.Now.ToString("yyyyMMdd")));
            if (File.Exists(filePath))
            {
                StringBuilder sb = new StringBuilder();
                int cnt=0;
                using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
                {
                    string strLine = sr.ReadLine();
                    while (!string.IsNullOrEmpty(strLine))
                    {
                        string[] val = strLine.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        if (val.Length == 2)
                        {
                            DataRow[] dr = m_Indextable.Select(string.Format("ID='{0}'", val[0]));
                            if (dr.Length > 0)
                            {
                                dr[0]["FrameCount"] = Convert.ToUInt32(dr[0]["FrameCount"]) + 1;
                                if (val[0] == "2")
                                    cnt++;
                                int idx = Convert.ToByte(dr[0]["FrameIndex"]);
                                if (idx == 255)
                                    idx = 0;
                                else
                                {
                                    idx++;
                                }
                                int ss = int.Parse(val[1]) - idx;
                                if (ss != 0)
                                {
                                    dr[0]["MissCount"] = Convert.ToUInt32(dr[0]["MissCount"]) + (ss > 0 ? ss : ss + 255);
                                }
                                dr[0]["FrameIndex"] = val[1];
                            }
                            else
                            {
                                string des = "";
                                switch (int.Parse(val[0]))
                                {
                                    case 4:
                                        des = "脑电心电(数据类型0x04)";
                                        break;
                                    case 1:
                                        des = "血氧体动(数据类型0x01)";
                                        break;
                                    case 2:
                                        des = "呼吸腿动(数据类型0x02)";
                                        break;
                                    case 3:
                                        des = "热敏压力(数据类型0x03)";
                                        break;
                                    default:
                                        des = "未定义";
                                        break;
                                }
                                m_Indextable.Rows.Add(des, 1, 0, val[0], val[1]);
                            }
                        }
                        strLine = sr.ReadLine();
                    }
                }
                cnt = cnt+1;
            }
        }

        private void buttonEx4_Click(object sender, EventArgs e)
        {
            if (!m_stop)
            {
                m_stop = true;
                smithChart1.IsStop = true;
                buttonEx4.Text = "继续";
                m_Protocol.FnStop = true;
            }
            else
            {
                m_stop = false;
                smithChart1.IsStop = false;
                buttonEx4.Text = "暂停";
                m_Protocol.FnStop = false;
            }
        }
        private int m_filiterBit = 0;
        private object m_lockFobj = new object();
        private void sToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lock (m_lockFobj)
            {
                ToolStripMenuItem tsm = (ToolStripMenuItem)sender;
                switch (tsm.Name)
                {
                    case "sToolStripMenuItem":
                        if (sToolStripMenuItem1.Checked)
                        {
                            sToolStripMenuItem1.Checked = false;
                            m_filiterBit = m_filiterBit & 0xEF;
                        }
                        _hasHighPass = !_hasHighPass;
                        tsm.Checked = _hasHighPass;
                        m_filiterBit = (m_filiterBit & 0xFE) + (_hasHighPass ? 0x01 : 0x00);
                        break;
                    case "sToolStripMenuItem1":
                        if (sToolStripMenuItem.Checked)
                        {
                            sToolStripMenuItem.Checked = false;
                            m_filiterBit = m_filiterBit & 0xFE;
                        }
                        tsm.Checked = !tsm.Checked;
                        _hasHighPass = tsm.Checked;
                        m_filiterBit = (m_filiterBit & 0xEF) + (_hasHighPass ? 0x01 : 0x00);
                        break;
                    case "hzToolStripMenuItem":
                        if (stoolStripMenuItem_30.Checked)
                        {
                            stoolStripMenuItem_30.Checked = false;
                            m_filiterBit = m_filiterBit & 0xFB;
                        }
                        tsm.Checked = !tsm.Checked;
                        _hasLowPass = tsm.Checked;
                        m_filiterBit = (m_filiterBit & 0xFD) + (_hasLowPass ? 0x01 : 0x00);
                        break;
                    case "stoolStripMenuItem_30":
                        if (hzToolStripMenuItem.Checked)
                        {
                            hzToolStripMenuItem.Checked = false;
                            m_filiterBit = m_filiterBit & 0xFD;
                        }
                        tsm.Checked = !tsm.Checked;
                        _hasLowPass = tsm.Checked;
                        m_filiterBit = (m_filiterBit & 0xFB) + (_hasLowPass ? 0x01 : 0x00);
                        break;
                    case "hzToolStripMenuItem1":
                        _hasSinglePass = !_hasSinglePass;
                        tsm.Checked = _hasSinglePass;
                        m_filiterBit = (m_filiterBit & 0xF7) + (_hasSinglePass ? 0x01 : 0x00);
                        break;
                }
                (tsm.OwnerItem as ToolStripMenuItem).Checked = tsm.Checked;
                if (m_stop)
                    smithChart1.Invalidate();
            }
        }

        private void buttonEx5_Click(object sender, EventArgs e)
        {
            new Action(() =>
            {
                m_Protocol.StartSampleRequest();
            }).BeginInvoke(null, null);
        }

        private void comboBoxEx5_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_Protocol.DataViewTyp = comboBoxEx5.SelectedIndex;
        }
        /// <summary>
        /// 通道装换成数据通讯对象
        /// </summary>
        /// <param name="channels"></param>
        /// <returns></returns>
        private List<DataDefine> ConvertToChannel(List<Doc_Channel> channels)
        {
            List<DataDefine> define = new List<DataDefine>();
            for (int i = 0; i < channels.Count; i++)
            {
                Doc_Channel channel = channels[i];
                if (!channel.Enable)
                    continue;
                DataDefine data = new DataDefine(channel.Name);
                data.ByteCountInData = channel.ByteLenghtOfValue;
                data.DataLength = channel.LenghtInGroup;
                data.GroupID = channel.GroupID;
                data.GroupIndex = channel.IndexInGroup;
                data.MaxADValue = channel.pADMaxValue;
                data.MaxViewValue = channel.pViewMaxValue;
                data.MinADValue = channel.pADMinValue;
                data.MinViewValue = channel.pViewMinValue;
                data.ShouldConverted = channel.NeedConvert;
                data.unSignData = channel.UnSignData;
                //data.RecOneDataEventHandle += ChartArea.FindCurve(channel.Name).AddDatavalue;
                m_Protocol_RecChannelEventHandle(data);
                define.Add(data);
            }
            return define;
        }
    }
}
