using AwareTec.Polysmith.Cloud.UI.DataModels;
using AwareTec.Polysmith.Cloud.UI.DataModels.ConfigurationModel.Predefined;
using AwareTec.Polysmith.Cloud.UI.ViewHelpers;
using AwareTec.Polysmith.DataBaseCom;
using AwareTec.Polysmith.pChart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.Cloud.UI.Views
{
    public partial class MulReportChart : UserControl
    {
        #region 私有变量

        /// <summary>
        /// 绘图区域高
        /// </summary>
        private int m_Height = 800;
        /// <summary>
        /// 绘图区域宽
        /// </summary>
        private int m_Width = 600;
        /// <summary>
        /// 边距间隔
        /// </summary>
        private int m_docksize = 30;
        /// <summary>
        /// 存放源数据的队列
        /// </summary>
        private SuperPointF[] m_DataSource = new SuperPointF[0];
        /// <summary>
        /// 滚动条滚动小格占位移量
        /// </summary>
        private int m_OneFrameDistance = 2;
        /// <summary>
        /// 绘制好的原图
        /// </summary>
        private Image m_SourceImage = null;
        private bool MouseIsDown = false;
        private SuperPointF m_StartSubPiont = null;
        private int bak_FrameCnt = 1;
        private Rectangle rect = Rectangle.Empty;
        /// <summary>
        /// 当前帧序号
        /// </summary>
        private int m_CurrentFrameNo = 1;
        private int m_xoffSet = 0;
        /// <summary>
        /// 总帧数
        /// </summary>
        private int m_TotalFrameCnt = 0;
        /// <summary>
        /// x轴一个刻度所占像素个数（30s一帧，一帧一个像素点，那么一个小时就是120个像素点）
        /// </summary>
        public int xDistanceProportion = 120;
        /// <summary>
        /// y轴一个刻度所占像素个数
        /// </summary>
        public int yDistanceProportion = 50;
        /// <summary>
        /// 采样周期
        /// </summary>
        public int TimeSpan = 30;
        private ChartMap m_CharMap = new ChartMap();
        private ResultDomain m_Result = null;
        private Area m_selectArea = null;

        #endregion

        #region 公有变量

        /// <summary>
        /// 当前帧序号
        /// </summary>
        public int CurrentFrameNo
        {
            set
            {
                m_CurrentFrameNo = value;
            }
        }

        #endregion

        #region 事件委托

        /// <summary>
        /// 当前帧序号发生改变事件
        /// </summary>
        public delegate void CurrentFrameChangedDelegate(int frameCnt);
        /// <summary>
        /// 当前帧序号发生改变事件
        /// </summary>
        public event CurrentFrameChangedDelegate CurrentFrameChangedHandler;

        #endregion

        #region 初始化 load/dispose/init

        /// <summary>
        /// 初始化
        /// </summary>
        public MulReportChart()
        {
            InitializeComponent();
            this.Load += MulReportChart_Load;
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MulReportChart_Load(object sender, EventArgs e)
        {
            this.panel1.Paint += this.panel1_Paint;
            this.panel1.Resize += this.panel1_Resize;
            this.panel1.MouseDown += this.panel1_MouseDown;
            this.panel1.MouseMove += this.panel1_MouseMove;
            this.panel1.MouseUp += this.panel1_MouseUp;

            this.m_Width = base.Width;
            this.m_Height = this.panel1.Height;
            for (int i = 0; i < ShowContantComBox.CheckBoxItems.Count; i++)
            {
                ShowContantComBox.CheckBoxItems[i].Checked = true;
            }

            this.ShowContantComBox.CheckBoxCheckedChanged += ShowContantComBox_CheckBoxCheckedChanged;
            this.SelectedComboBox.SelectedIndexChanged += SelectedComboBox_SelectedIndexChanged;
            this.maxValueText.KeyPress += MaxValueText_KeyPress;
            this.minValueText.KeyPress += MaxValueText_KeyPress;
            this.maxValueText.KeyDown += MaxValueText_KeyDown;
            this.minValueText.KeyDown += MaxValueText_KeyDown;
            this.PrintButton.Click += PrintButton_Click;


        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 多趋势图重绘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //焦点如果集中panel1，panel2中显示项下拉框会消失
            //panel1.Focus();
            using (Graphics graphics = e.Graphics)
            {
                if (this.m_SourceImage == null)
                {
                    this.m_SourceImage = this.CreatMap();
                }
                int num = this.m_xoffSet * this.m_OneFrameDistance;
                graphics.DrawImage(this.m_SourceImage, new RectangleF(0f, 0f, (float)this.m_Width, (float)this.m_Height), new RectangleF((float)num, 0f, (float)this.m_Width, (float)this.m_Height), GraphicsUnit.Pixel);
                using (Pen splitpen = new Pen(Color.FromArgb(221, 221, 221)))
                    graphics.DrawLine(splitpen, new Point(0, 0), new Point(Width, 0));
                if (this.m_StartSubPiont == null)
                {
                    this.m_StartSubPiont = new SuperPointF();
                    int num2 = this.m_CurrentFrameNo - this.m_Result.OffSetFrameCnt;
                    if (num2 <= 0)
                    {
                        num2 = 1;
                    }
                    this.m_StartSubPiont.X = (float)(num2 - 1) * this.m_CharMap.xAxis.ValueRate + (float)this.m_docksize;
                    this.m_StartSubPiont.XValue = (float)num2;
                }
                if (this.panel1.Width != 0)
                {
                    this.m_StartSubPiont.X = (this.m_StartSubPiont.XValue - 1f) * this.m_CharMap.xAxis.ValueRate + (float)this.m_docksize - (float)num;
                    if (this.m_StartSubPiont.X < (float)this.m_Width && this.m_StartSubPiont.X > 0f)
                    {
                        using (Pen pen = new Pen(Color.Blue, 1f))
                        {
                            pen.DashStyle = DashStyle.Dash;
                            pen.DashPattern = new float[]
                            {
                                5f,
                                5f
                            };
                            graphics.DrawLine(pen, new Point((int)this.m_StartSubPiont.X + 2, this.m_Height - this.m_docksize), new Point((int)this.m_StartSubPiont.X + 2, this.m_docksize));
                        }
                        int num3 = this.m_CurrentFrameNo - this.m_Result.OffSetFrameCnt;
                        string[] array = new string[this.m_CharMap.ItemAreas.Count];
                        using (Font font = new Font("宋体", 9f))
                        {
                            for (int i = 0; i < this.m_CharMap.ItemAreas.Count; i++)
                            {
                                if (!this.m_CharMap.ItemAreas[i].Checked)
                                    continue;
                                if (num3 >= this.m_CharMap.ItemAreas[i].Points.Count)
                                {
                                    num3 = this.m_CharMap.ItemAreas[i].Points.Count - 1;
                                }
                                if (num3 < 0)
                                {
                                    num3 = 0;
                                }
                                if (this.m_CharMap.ItemAreas[i].Points[num3] == null)
                                {
                                    break;
                                }
                                int num4 = (int)this.m_CharMap.ItemAreas[i].Points[num3].YMaxValue - 1;
                                array[i] = string.Format("({0}){1}", this.m_CharMap.ItemAreas[i].Name, (this.m_CharMap.ItemAreas[i].yAxis.MaxValue < 6f) ? ((num4 > 0) ? this.m_CharMap.ItemAreas[i].yAxis.LegendLables[(int)this.m_CharMap.ItemAreas[i].Points[num3].YMaxValue - 1] : "") : this.m_CharMap.ItemAreas[i].Points[num3].YMaxValue.ToString());
                                float num5 = graphics.MeasureString(array[i], font).Width + 2f;
                                float num6 = num5 + this.m_StartSubPiont.X;
                                if (num6 < (float)(this.m_Width - this.m_docksize))
                                {
                                    graphics.DrawString(array[i], font, Brushes.Gray, new PointF(this.m_StartSubPiont.X + 2f, (float)(this.m_CharMap.ItemAreas[i].Location.Y + font.Height + 3)));
                                }
                                else
                                {
                                    graphics.DrawString(array[i], font, Brushes.Gray, new PointF(this.m_StartSubPiont.X - num5, (float)(this.m_CharMap.ItemAreas[i].Location.Y + font.Height + 3)));
                                }
                            }
                            string text = string.Format("帧序号-{0}", this.m_CurrentFrameNo);
                            float num7 = graphics.MeasureString(text, font).Width + 2f;
                            float num8 = num7 + this.m_StartSubPiont.X;
                            if (num8 < (float)(this.m_Width - this.m_docksize))
                            {
                                graphics.DrawString(text, font, Brushes.Gray, new PointF(this.m_StartSubPiont.X + 2f, (float)(this.m_docksize - font.Height)));
                            }
                            else
                            {
                                graphics.DrawString(text, font, Brushes.Gray, new PointF(this.m_StartSubPiont.X - num7, (float)(this.m_docksize - font.Height)));
                            }
                        }
                    }
                }
                Console.WriteLine("***更新一次报告图表2***");
            }
        }

        /// <summary>
        /// 多趋势图改变大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_Resize(object sender, EventArgs e)
        {
            if (this.panel1.Width != 0 && this.panel1.Height != 0)
            {
                this.m_Height = this.panel1.Height;
                this.m_Width = this.panel1.Width;
                this.rect = new Rectangle(40, this.m_Height - 15, 200, 20);
                this.m_SourceImage = this.CreatMap();
            }
        }

        /// <summary>
        /// 生成谱图
        /// </summary>
        /// <returns></returns>
        private Image CreatMap()
        {
            this.m_CharMap.Width = this.m_Width;
            this.m_CharMap.StartTime = this.m_Result.StartTime;
            TimeSpan timeSpan = this.m_Result.EndTime - this.m_Result.StartTime;
            int num = (int)timeSpan.TotalHours;
            num = (((double)num < timeSpan.TotalHours) ? (num + 1) : num);
            this.m_CharMap.xAxis.MaxValue = (float)((int)(timeSpan.TotalHours * 60.0 * 2.0));
            this.m_CharMap.xAxis.Interval = num;
            this.m_CharMap.xAxis.Calibrations.Clear();
            for (int i = 0; i < num; i++)
            {
                this.m_CharMap.xAxis.Calibrations.Add((float)(120 * i));
            }
            this.m_CharMap.xAxis.Calibrations.Add(this.m_CharMap.xAxis.MaxValue);
            int num2 = this.m_CharMap.Docksize;
            this.m_CharMap.Hight = this.m_Height;
            int count = this.m_CharMap.ItemAreas.Count;
            var shows = this.m_CharMap.ItemAreas.Where(t => t.Checked);
            if (shows.Count() > 0)
                count = shows.Count();
            int num3 = (this.m_Height - 2 * this.m_CharMap.Docksize) / count;
            for (int j = 0; j < this.m_CharMap.ItemAreas.Count; j++)
            {
                Area area = this.m_CharMap.ItemAreas[j];
                if (area.Checked)
                {
                    area.Location = new Point(this.m_CharMap.Docksize, num2);
                    area.Hight = num3;
                    num2 += (int)area.Hight;
                }
            }
            return this.m_CharMap.GetMap();
        }

        /// <summary>
        /// 展示项  下拉框 勾选状态改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowContantComBox_CheckBoxCheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < ShowContantComBox.CheckBoxItems.Count; i++)
            {
                Area one = m_CharMap.ItemAreas.Find(t => t.ID == ShowContantComBox.CheckBoxItems[i].Text.ToString());
                if (one != null)
                {
                    one.Checked = ShowContantComBox.CheckBoxItems[i].Checked;
                }
            }
            this.m_SourceImage = this.CreatMap();
            panel1.Invalidate();
        }

        /// <summary>
        /// 选择项 下拉框 选择的内容改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectedComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Area one = m_CharMap.ItemAreas.Find(t => t.ID == SelectedComboBox.SelectedItem.ToString());
            if (one != null)
            {
                maxValueText.Text = one.yAxis.MaxValue.ToString();
                minValueText.Text = one.yAxis.MinValue.ToString();
                m_selectArea = one;
            }
        }

        /// <summary>
        /// 文本框输入值限制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaxValueText_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputTextCheck.floatvalue_KeyPress(sender, e);
        }

        /// <summary>
        /// 文本框 键盘快捷键处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaxValueText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (m_selectArea == null)
                    return;
                string strmax = maxValueText.Text;
                string strmin = minValueText.Text;
                if (strmax == "")
                {
                    maxValueText.Text = m_selectArea.yAxis.MaxValue.ToString();
                    minValueText.Text = m_selectArea.yAxis.MinValue.ToString();
                    AhDung.MessageTip.ShowWarning("数据不能设置为空！");
                    return;
                }
                if (!CheckTxtBoxContant(maxValueText.Text))
                {
                    maxValueText.Text = m_selectArea.yAxis.MaxValue.ToString();
                    AhDung.MessageTip.ShowWarning("最大值的输入非法！");
                    return;
                }
                if (!CheckTxtBoxContant(minValueText.Text))
                {
                    minValueText.Text = m_selectArea.yAxis.MinValue.ToString();
                    AhDung.MessageTip.ShowWarning("最小值的输入非法！");
                    return;
                }
                if (maxValueText.Text == m_selectArea.yAxis.MaxValue.ToString() && minValueText.Text == m_selectArea.yAxis.MinValue.ToString())
                    return;
                m_selectArea.yAxis.SetMaxMinValue(float.Parse(strmax), float.Parse(strmin));
                m_selectArea.yAxis.LegendLables = m_selectArea.yAxis.Calibrations.Select(t => t.ToString()).ToList();
                this.m_SourceImage = this.CreatMap();
                panel1.Invalidate();
            }
        }

        /// <summary>
        /// 检查文本框内容
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool CheckTxtBoxContant(string data)
        {
            char[] dat = data.ToCharArray();
            for (int i = 0; i < dat.Length; i++)
            {
                //限制只能输入数字
                if (!char.IsDigit(dat[i]))
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region 鼠标动作效果

        /// <summary>
        /// 鼠标松开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            this.panel1.Capture = false;
            Cursor.Clip = Rectangle.Empty;
            if (this.MouseIsDown)
            {
                if (this.bak_FrameCnt != this.m_CurrentFrameNo)
                {
                    this.bak_FrameCnt = this.m_CurrentFrameNo;
                    if (this.CurrentFrameChangedHandler != null)
                    {
                        this.CurrentFrameChangedHandler.BeginInvoke(this.m_CurrentFrameNo, null, null);
                    }
                }
                this.MouseIsDown = false;
            }
        }
        
        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.MouseIsDown)
            {
                this.panel1.Capture = true;
                if (this.m_StartSubPiont != null)
                {
                    this.m_StartSubPiont = this.CheckPoint(e.Location);
                    this.panel1.Invalidate();
                }
            }
        }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            panel1.Focus();
            if (e.Button == MouseButtons.Left)
            {
                this.MouseIsDown = true;
                this.panel1.Capture = true;
                this.m_StartSubPiont = this.CheckPoint(e.Location);
                this.panel1.Invalidate();
            }
        }

        /// <summary>
        /// 检查轴线点
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private SuperPointF CheckPoint(Point p)
        {
            SuperPointF superPointF = new SuperPointF();
            int num = this.m_xoffSet * this.m_OneFrameDistance;
            int num2 = this.m_docksize - num;
            num2 = ((num2 > 0) ? num2 : 0);
            float num3 = (float)this.m_TotalFrameCnt * this.m_CharMap.xAxis.ValueRate + (float)this.m_docksize;
            if (p.X < num2)
            {
                superPointF.X = (float)num2;
            }
            else if ((float)(p.X + num) > num3)
            {
                superPointF.X = num3 - (float)num;
            }
            else
            {
                superPointF.X = (float)p.X;
            }
            superPointF.XValue = (float)((int)((superPointF.X + (float)num - (float)this.m_docksize) / this.m_CharMap.xAxis.ValueRate));
            this.m_CurrentFrameNo = (int)(superPointF.XValue + (float)this.m_Result.OffSetFrameCnt);
            int num4 = this.m_TotalFrameCnt + this.m_Result.OffSetFrameCnt - 1;
            if (this.m_CurrentFrameNo > num4)
            {
                this.m_CurrentFrameNo = num4;
            }
            return superPointF;
        }

        #endregion

        #region 按钮方法

        /// <summary>
        /// 打印 按钮 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "png|*.png|jpeg|*.jpeg|bmp|*.bmp";
            sf.RestoreDirectory = true;
            sf.FilterIndex = 1;
            sf.FileName = string.Format("MulMap{0:yyMMddhhmmss}", DateTime.Now);
            if (sf.ShowDialog() == DialogResult.OK)
            {
                Task.Factory.StartNew(() =>
                {
                    System.Threading.Thread.Sleep(500);
                    Bitmap bit = new Bitmap(this.panel1.Width, this.panel1.Height);//实例化一个和窗体一样大的bitmap
                    Graphics g = Graphics.FromImage(bit);
                    g.CompositingQuality = CompositingQuality.HighQuality;//质量设为最高
                    g.CopyFromScreen(this.Parent.Left, this.Parent.Top + panel2.Height + (this.Parent as SkinForm).CaptionHeight, 0, 0, new Size(panel1.Width, panel1.Height));//保存整个窗体为图片
                    bit.Save(sf.FileName);//默认保存格式为PNG，保存成jpg格式质量不是很好
                    AhDung.MessageTip.ShowOk("打印成功");
                    DataModels.Global.GlobalSingleton.Instance.LogInstance.AddLog("用户打印 多谱图，打印成功", pSystem.LogManagement.LogLevel.WARN);
                });
            }
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="result"></param>
        /// <param name="isbreathOnly"></param>
        public void LoadData(ResultDomain result = null, bool isbreathOnly = false)
        {
            if (result == null)
            {
                result = Channel.Default.AnalysisReult.ConvertResultDomain(0, false);
            }
            this.m_Result = result;
            this.m_CurrentFrameNo = this.m_Result.OffSetFrameCnt;
            this.m_TotalFrameCnt = this.m_Result.SleepStagPoints.Length;
            #region 新方式
            var reportmaps = Channel.Default.ReportStructurals.Where(t => t.Visible);
            if (reportmaps.Count() > 0)
            {
                ShowContantComBox.Items.Clear();
                SelectedComboBox.Items.Clear();
                int index = 0;
                reportmaps = reportmaps.OrderBy(t => t.Index);
                PropertyInfo[] ms = result.GetType().GetProperties();
                foreach (PredefinedReportStructure report in reportmaps)
                {
                    if (report.Id == 9 && isbreathOnly)///ID为9是睡眠分期结构图
                    {
                        continue;
                    }
                    ShowContantComBox.Items.Add(report.Description);
                    var dd = ShowContantComBox.Items[index];
                    Area area = new Area();
                    area.ID = report.Description;
                    area.Name = report.Name;
                    area.Hight = (int)report.Height;
                    area.PenColor = Color.FromName(report.PenColor);
                    PropertyInfo ss = ms.First(t => t.Name == report.DataSourceName);
                    if (ss != null)
                    {
                        try
                        {
                            if (ss.PropertyType == typeof(SuperPointF[]))
                            {
                                area.Points = ((SuperPointF[])Convert.ChangeType(ss.GetValue(result), ss.PropertyType)).ToList();
                            }
                        }
                        catch
                        {

                        }
                    }
                    area.yAxis.Interval = report.Interval;
                    area.yAxis.SetMaxMinValue(report.MaxValue, report.MinValue);
                    if (report.LegendLables != "")
                    {
                        area.yAxis.LegendLables = report.LegendLables.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToList();
                        area.yAxis.CalibrationsColors = report.CalibrationsColors.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(t => Color.FromName(t.Trim())).ToList();
                    }
                    else
                    {
                        area.Edit = true;
                        area.yAxis.LegendLables = area.yAxis.Calibrations.Select(t => t.ToString()).ToList();
                        SelectedComboBox.Items.Add(report.Description);
                    }
                    area.SavePath = report.SavePath;
                    m_CharMap.ItemAreas.Add(area);
                }
                ShowContantComBox.Height = ShowContantComBox.Font.Height * (ShowContantComBox.Items.Count + 1);
            }
            #endregion
        }

        #endregion

        /// <summary>
        /// 防止拖拽的时候出现卡顿
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = 0x02000000;////用双缓冲绘制窗口的所有子控件
                return cp;
            }
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Left || keyData == Keys.Right)
            {
                if (keyData == Keys.Up)
                {
                    this.m_CurrentFrameNo = this.m_Result.OffSetFrameCnt;
                    this.m_StartSubPiont.X = (float)this.m_docksize;
                }
                else if (keyData == Keys.Down)
                {
                    this.m_CurrentFrameNo = this.m_TotalFrameCnt + this.m_Result.OffSetFrameCnt - 1;
                    this.m_StartSubPiont.X = (float)this.m_TotalFrameCnt * this.m_CharMap.xAxis.ValueRate + (float)this.m_docksize;
                }
                else if (keyData == Keys.Left)
                {
                    if (this.m_CurrentFrameNo > this.m_Result.OffSetFrameCnt)
                    {
                        this.m_CurrentFrameNo--;
                        this.m_StartSubPiont.X -= this.m_CharMap.xAxis.ValueRate;
                    }
                }
                else if (this.m_CurrentFrameNo < this.m_TotalFrameCnt + this.m_Result.OffSetFrameCnt - 1)
                {
                    this.m_CurrentFrameNo++;
                    this.m_StartSubPiont.X += this.m_CharMap.xAxis.ValueRate;
                }
                if (this.bak_FrameCnt != this.m_CurrentFrameNo)
                {
                    this.bak_FrameCnt = this.m_CurrentFrameNo;
                    if (this.CurrentFrameChangedHandler != null)
                    {
                        this.CurrentFrameChangedHandler.BeginInvoke(this.m_CurrentFrameNo, null, null);
                    }
                    this.m_StartSubPiont.XValue = (float)(this.m_CurrentFrameNo - this.m_Result.OffSetFrameCnt);
                    this.panel1.Invalidate();
                }
                return false;
            }
            return base.ProcessDialogKey(keyData);
        }


    }
}
