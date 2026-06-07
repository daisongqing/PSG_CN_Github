using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;

namespace AwareTec.Polysmith.pChart
{
    /// <summary>
    /// 曲线单元类
    /// </summary>
    public class CurveItem
    {
        #region 私有成员
        //private System.Timers.Timer m_Timer1 = null;
        private List<float> m_DataInQueues = new List<float>();
        /// <summary>
        /// 源y坐标数据
        /// </summary>
        private List<float> bak_yDataValues = new List<float>();
        /// <summary>
        /// 换算之后的坐标数据
        /// </summary>
        //private PointF[] m_xyDataValues = new PointF[0];
        /// <summary>
        /// 源值转换成坐标值
        /// </summary>
        /// <returns></returns>
        private float ConvertYValue(float value, float basevalue)
        {
            float y = value;
            if (PixelRate != 0)
            {
                if (Antipole)
                {
                    y = yBaseDistance + (value - basevalue) * yAxis.ValueRate;
                }
                else
                    y = yBaseDistance - (value - basevalue) * yAxis.ValueRate;
            }
            else
            {
                if (Antipole)
                    y = yTop + (value - yAxis.MinValue) * yAxis.ValueRate;
                else
                    y = yBottom - (value - yAxis.MinValue) * yAxis.ValueRate;
            }
            if (y < m_yLimitMinDistance)
                y = m_yLimitMinDistance;
            if (y > m_yLimitMaxDistance)
                y = m_yLimitMaxDistance;
            return y;
        }
        /// <summary>
        /// 过滤波形
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private float[] Filiter(List<float> data, int length, ref int startIndex)
        {
            float[] ydatas = new float[0];
            int startIdx = 0;
            int dataIdx = 0;
            lock (m_lockaddData)
            {
                int len = data.Count;
                try
                {
                    if (len < length)
                    {
                        length = len;
                        startIndex = 0;
                    }
                    else if (len < (length + startIndex))
                    {
                        startIndex = len - length;
                        length = len;
                    }
                    else
                    {
                        length = length + startIndex;
                    }
                    ydatas = new float[length];
                    dataIdx = len - length;
                    startIdx = m_CurrentStartIndex + dataIdx;
                    int TmpIdx = 0;
                    for (int i = dataIdx; i < len; i++)
                        ydatas[TmpIdx++] = data[i];
                }
                catch (Exception ee)
                {
                    Console.WriteLine(string.Format("len:{0}--startIdx:{1}", len, startIdx));
                }
            }
            if (FiliterDataEvent != null)
            {
                ydatas = FiliterDataEvent.Invoke(this, ydatas, startIdx, Tag);
            }
            ///取1000个点中的均值作基准值
            //if (len <= 10000 && len > 0)
            //    BaseValue = ydatas.Average();
            return ydatas;
        }
        /// <summary>
        /// 过滤波形
        /// </summary>
        /// <param name="data"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private float[] Filiter(List<float> data, int startIndex, int length)
        {
            float[] ydatas = new float[length];
            //如果是初筛的数据 查看回放时 选择全呼吸 在脑电通道操作会出错 这里做限制
            if (data.Count == 0)
            {
                return ydatas;
            }
            data.CopyTo(startIndex, ydatas, 0, length);
            if (FiliterDataEvent != null)
            {
                ydatas = FiliterDataEvent.Invoke(this, ydatas, startIndex, Tag);
            }
            return ydatas;
        }

        /// <summary>
        /// 数据捕获周期(ms)
        /// </summary>
        private int m_TimeSpan = 20;
        /// <summary>
        /// 最大点数
        /// </summary>
        private int m_MaxCnt = 0;
        /// <summary>
        /// 采样频率
        /// </summary>
        private int m_sampleFreq = 0;
        /// <summary>
        /// 一帧的数据点长度
        /// </summary>
        private int m_OneFrameDataLen = 0;
        /// <summary>
        /// 任务终止
        /// </summary>
        private bool m_KillTask = true;
        private bool m_TaskEnable = false;
        /// <summary>
        /// 线程th
        /// </summary>
        private System.Threading.Thread th = null;
        private int m_sleepSpan = 90;
        private object m_obj = new object();
        /// <summary>
        /// 任务执行器
        /// </summary>
        private void TaskStart()
        {
            th = new System.Threading.Thread(() =>
            {
                while (!m_KillTask)
                {
                    int sleepspan = m_sleepSpan;
                    if (m_TaskEnable)
                    {
                        DateTime dt = DateTime.Now;
                        int cnt = m_DataInQueues.Count;
                        int n = cnt > m_getDataCountSencons ? m_getDataCountSencons : cnt;
                        if (n > 0)
                        {
                            float[] datasource = new float[n];
                            for (int i = 0; i < n; i++)
                            {
                                datasource[i] = m_DataInQueues[i];
                            }
                            lock (m_obj)
                                m_DataInQueues.RemoveRange(0, n);
                            int offset = (bak_yDataValues.Count + n) - m_MaxDataLength;
                            lock (m_lockaddData)
                            {
                                if (offset > 0)
                                {
                                    bak_yDataValues.RemoveRange(0, offset);
                                    m_CurrentStartIndex += offset;
                                }
                                bak_yDataValues.AddRange(datasource);
                            }
                            if (m_PushDataToCloneItemHandle != null)
                                m_PushDataToCloneItemHandle.Invoke(datasource);///用begininvoke的话，在有多个委托待执行时，会提示异常错误
                        }
                        sleepspan -= (int)(DateTime.Now - dt).TotalMilliseconds;
                    }
                    System.Threading.Thread.Sleep(sleepspan > 0 ? sleepspan : 1);
                }
            });
            th.IsBackground = true;
            th.SetApartmentState(System.Threading.ApartmentState.MTA);
            th.Start();
        }
        #endregion
        #region 构造函数
        /// <summary>
        /// 曲线单元构造函数
        /// </summary>
        public CurveItem()
        {
            xAxis = new XAxis();
            yAxis = new YAxis();
            //m_Timer1 = new System.Timers.Timer();
            //m_Timer1.Interval = 40;
            //m_Timer1.Elapsed += m_Timer1_Elapsed;
            PixelRate = 0;
            ValueZoomRate = 1;
            IsShowValue = false;
            ShowPeakValue = true;
            OneFrameTimeSpan = 30;
            IsLastViewChannel = false;
            IsCloneItem = false;
            ChannelNum = -1;
        }

        private void m_Timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            int cnt = m_DataInQueues.Count;
            int n = cnt > m_getDataCountSencons ? m_getDataCountSencons : cnt;
            if (n > 0)
            {
                float[] datasource = new float[n];
                for (int i = 0; i < n; i++)
                {
                    datasource[i] = m_DataInQueues[i];
                }
                lock (m_obj)
                    m_DataInQueues.RemoveRange(0, n);
                int offset = (bak_yDataValues.Count + n) - m_MaxDataLength;
                lock (m_lockaddData)
                {
                    if (offset > 0)
                    {
                        bak_yDataValues.RemoveRange(0, offset);
                        m_CurrentStartIndex += offset;
                    }
                }
                bak_yDataValues.AddRange(datasource);
            }
        }
        #endregion
        #region 事件定义
        public delegate void reChangedDelegate();
        /// <summary>
        /// 界面区域更新
        /// </summary>
        public event reChangedDelegate reChangedHandle;
        public delegate float[] FiliterDataDelegate(CurveItem item, float[] datasource, int firstDataIndex, object filiter);
        /// <summary>
        /// 触发滤波算法事件
        /// </summary>
        public event FiliterDataDelegate FiliterDataEvent;
        public delegate float[] FiliterAllDataDelegate(float[] datasource);
        /// <summary>
        /// 触发滤波算法事件
        /// </summary>
        public event FiliterAllDataDelegate FiliterAllDataEvent;
        #endregion
        #region 公有属性
        /// <summary>
        /// X轴坐标队列
        /// </summary>
        public XAxis xAxis = null;
        /// <summary>
        /// Y轴坐标队列
        /// </summary>
        public YAxis yAxis = null;
        /// <summary>
        /// 曲线名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 默认背景色为黑
        /// </summary>
        private Color m_BackColor = Color.Gray;
        /// <summary>
        /// 获取或设置背景色
        /// </summary>
        public Color BackColor { set { m_BackColor = value; } get { return m_BackColor; } }
        private float m_PenWidth = 0.5f;
        /// <summary>
        /// 画笔的宽度
        /// </summary>
        public float PenWidth
        {
            set { m_PenWidth = value; }
            get { return m_PenWidth; }
        }
        /// <summary>
        /// 判断容器归属类型值
        /// </summary>
        public int belong = 0;
        private Color m_PenColor = Color.Black;
        /// <summary>
        /// 灵敏度系数与通道的大小相关。
        /// </summary>
        public float PixelRateCoefficient = 1.0f;
        /// <summary>
        /// 获取或设置画笔的颜色
        /// </summary>
        public Color PenColor { set { m_PenColor = value; } get { return m_PenColor; } }
        /// <summary>
        /// 是否采用临时通道配置参数的标志
        /// </summary>
        public bool TemporaryControl = false;
        /// <summary>
        /// 标签集合
        /// </summary>
        public List<IMarker> CurrentMarks = new List<IMarker>();
        /// <summary>
        /// 待消除的标记
        /// </summary>
        public List<IMarker> removeMarks = new List<IMarker>();
        /// <summary>
        /// 自动刻度
        /// </summary>
        private bool m_AutoCalibrationsEnable = false;
        /// <summary>
        /// 自动刻度
        /// </summary>
        public bool AutoCalibrationsEnable
        {
            set
            {
                m_AutoCalibrationsEnable = value;
            }
            get
            {
                return m_AutoCalibrationsEnable;
            }
        }
        /// <summary>
        /// 基线是否显示
        /// </summary>
        public bool DBaseLineVisible = false;
        /// <summary>
        /// 反极使能
        /// </summary>
        public bool Antipole = false;
        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool isSelected = false;
        /// <summary>
        /// 曲线ID 唯一标识
        /// </summary>
        public string ID { set; get; }
        /// <summary>
        /// 通道编码与ID对应
        /// </summary>
        public int ChannelNum { set; get; }
        /// <summary>
        /// 信号名称
        /// </summary>
        public string SignName { set; get; }
        /// <summary>
        /// 基准值
        /// </summary>
        public float BaseValue { set; get; }
        /// <summary>
        /// 基准y坐标
        /// </summary>
        public float yBaseDistance { set; get; }
        /// <summary>
        /// 定义一帧的时间区间(s)
        /// </summary>
        public int OneFrameTimeSpan { set; get; }
        /// <summary>
        /// 基准坐标
        /// </summary>
        public float headBaseDistance { set; get; }
        /// <summary>
        /// 显示区域顶部坐标
        /// </summary>
        public float yTop { set; get; }
        /// <summary>
        /// 显示区域底部坐标
        /// </summary>
        public float yBottom { set; get; }
        /// <summary>
        /// mm系数常量
        /// </summary>
        public float PixelConstants { set; get; }
        /// <summary>
        /// 灵敏度
        /// </summary>
        public float PixelRate { set; get; }
        /// <summary>
        /// 值放大倍数
        /// </summary>
        public float ValueZoomRate { set; get; }
        /// <summary>
        /// 是否显示点值
        /// </summary>
        public bool IsShowValue { set; get; }
        /// <summary>
        /// 是否显示峰谷值
        /// </summary>
        public bool ShowPeakValue { set; get; }
        /// <summary>
        /// 从上往下数，是否是最后一个通道
        /// </summary>
        public bool IsLastViewChannel { set; get; }
        /// <summary>
        /// 内存中存储一小时的数据长度
        /// </summary>
        private int m_MaxDataLength = 0;
        private int m_getDataCountSencons = 0;
        /// <summary>
        /// 获取或设置数据的捕获周期(ms)
        /// </summary>
        public int TimeSpan
        {
            set
            {
                m_TimeSpan = value;
                m_sampleFreq = 1000 / value;
                m_OneFrameDataLen = OneFrameTimeSpan * m_sampleFreq;
                m_MaxDataLength = 3600 * m_sampleFreq;
                double sn = (m_sampleFreq * (m_sleepSpan + 10) / 1000);
                m_getDataCountSencons = sn < 1 ? 1 : (int)sn;
                Console.WriteLine("{0}----{1}-----{2}", ID, m_sampleFreq, m_getDataCountSencons);
            }
            get
            {
                return m_TimeSpan;
            }
        }
        /// <summary>
        /// 是否分段滤波
        /// </summary>
        public bool SubsectionzFlite = false;
        /// <summary>
        /// 曲线开始记录时间
        /// </summary>
        public DateTime StartTime = DateTime.Now;
        /// <summary>
        /// 通道排列序号 由下到上排序
        /// </summary>
        public int ChannelNo { set; get; }
        /// <summary>
        /// AD最高限值
        /// </summary>
        public int MaxADValue = 0;
        /// <summary>
        /// AD最低限值
        /// </summary>
        public int MinADValue = 0;
        /// <summary>
        /// 与AD对应的最高限值
        /// </summary>
        public int MaxViewValue = 0;
        /// <summary>
        /// 与AD对应的最低限值
        /// </summary>
        public int MinViewValue = 0;
        /// <summary>
        /// 是否为克隆出来的通道
        /// </summary>
        public bool IsCloneItem { set; get; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Visible = true;

        /// <summary>
        /// 是否画波形
        /// </summary>
        public bool IsDraw = true;
        private Font m_Font = new Font("微软雅黑", 16.0f);
        private string m_HighPass = "Off";
        /// <summary>
        /// 高通滤波器 为Off表示不启用
        /// </summary>
        public string HighPass { set { m_HighPass = value; } get { return m_HighPass; } }
        private string m_LowPass = "Off";
        /// <summary>
        /// 低通滤波器 为Off表示不启用
        /// </summary>
        public string LowPass { set { m_LowPass = value; } get { return m_LowPass; } }
        private string m_SingleNotch = "Off";
        /// <summary>
        /// 陷波器 为Off表示不启用
        /// </summary>
        public string SingleNotch { set { m_SingleNotch = value; } get { return m_SingleNotch; } }
        private int m_endViewX = 0;
        /// <summary>
        /// 获取当前可见数据的终点坐标
        /// </summary>
        public int EndViewX
        {
            get
            {
                return m_endViewX;
            }
        }
        /// <summary>
        /// 字体样式
        /// </summary>
        public Font Font
        {
            set
            {
                m_Font = value;
            }
            get
            {
                return m_Font;
            }
        }
        private float m_yLimitMaxDistance = 0;
        /// <summary>
        /// y值最大有效区域
        /// </summary>
        public float yLimitMaxDistance
        {
            set
            {
                if (m_yLimitMaxDistance != value)
                    m_yLimitMaxDistance = value;
            }
        }
        private float m_yLimitMinDistance = 0;
        /// <summary>
        /// y值最小有效区域
        /// </summary>
        public float yLimitMinDistance
        {
            set
            {
                if (m_yLimitMinDistance != value)
                    m_yLimitMinDistance = value;
            }
        }
        /// <summary>
        /// 工作区域被选中
        /// </summary>
        public bool ClientRectangleChecked = false;
        private RectangleF m_rectangle = new RectangleF();
        /// <summary>
        /// 工作区域
        /// </summary>
        public RectangleF ClientRectangle
        {
            internal set
            {
                m_rectangle = value;
            }
            get
            {
                return m_rectangle;
            }
        }
        /// <summary>
        /// 头部区域被选中
        /// </summary>
        public bool HeadRectangleChecked = false;
        private RectangleF m_Headrectangle = new RectangleF();
        /// <summary>
        /// 头部区域
        /// </summary>
        public RectangleF HeadRectangle
        {
            internal set
            {
                m_Headrectangle = value;
            }
            get
            {
                return m_Headrectangle;
            }
        }
        private List<PointF> m_ClientRectangleData = new List<PointF>();
        /// <summary>
        /// 获取当前显示区域的y数据队列
        /// </summary>
        public List<PointF> ClientRectanglePoints
        {
            set
            {
                m_ClientRectangleData = value;
            }
            get
            {
                return m_ClientRectangleData;
            }
        }
        /// <summary>
        /// 是否为拖拽实现事件重新标记动作
        /// </summary>
        public bool IsMarkerChanged = false;
        /// <summary>
        /// Tag(暂时存放滤波器)
        /// </summary>
        public object Tag { set; get; }
        #endregion
        #region 公有方法
        private object m_lockaddData = new object();
        /// <summary>
        /// 队列中第一个数据在总接收数据的索引号
        /// </summary>
        private int m_CurrentStartIndex = 0;
        private int m_Addindex = 0;
        /// <summary>
        /// 添加数据
        /// </summary>
        public void AddDatavalue(float yValue, bool Invaild, string msg)
        {
            if (Invaild)
            {
                ///无效值按照0显示，而改变以前不显示的方式
                yValue = 0;
            }
            if (AutoCalibrationsEnable)
            {
                yAxis.MaxValue = yValue > yAxis.MaxValue ? yValue : yAxis.MaxValue;
            }
            lock (m_obj)
                m_DataInQueues.Add(yValue);
            if (!m_TaskEnable)
            {
                m_Addindex++;
                if (m_Addindex > m_sampleFreq * 2)
                {
                    m_TaskEnable = true;
                    m_KillTask = false;
                    TaskStart();
                    m_Addindex = 0;
                }
            }
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="yValues"></param>
        public void AddDatavalue(IEnumerable<float> yValues)
        {
            foreach (float value in yValues)
            {
                AddDatavalue(value, false, "");
            }
        }
        /// <summary>
        /// 绑定静态数据
        /// </summary>
        /// <param name="yValues"></param>
        public void BindYDataValues(IEnumerable<float> yValues)
        {
            bak_yDataValues = yValues.ToList();
            if (AutoCalibrationsEnable)
            {
                int len = bak_yDataValues.Count;
                if (len > 0)
                {
                    yAxis.MaxValue = bak_yDataValues.Max();
                    yAxis.MinValue = bak_yDataValues.Min();
                }
            }
        }
        /// <summary>
        /// 获取局部点
        /// </summary>
        /// <param name="startIdx"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public SuperPoints GetxyDataValues(int startIdx, ref int length)
        {
            SuperPoints ret = new SuperPoints();
            //int len = bak_yDataValues.Count;
            //float[] data = new float[length];
            //if (length + startIdx > len)
            //    length = len - startIdx;
            //m_ClientRectangleData.Clear();
            //float[] ydatas = Filiter(bak_yDataValues, length + m_sampleFreq * 15);
            //ret.Points = new PointF[length];
            //ret.SourcePoints = new PointF[length];
            //for (int i = 0; i < length; i++)
            //{
            //    int idx = i + startIdx;
            //    data[i] = ydatas[idx] / ValueZoomRate;
            //    float x = xAxis.ValueRate * (i) * m_TimeSpan + xAxis.offSetDistance;
            //    ret.Points[i] = new PointF(x, ConvertYValue(data[i], BaseValue));
            //    ret.SourcePoints[i] = new PointF(x, data[i]);
            //}
            //ret.yDataValues = data;
            return ret;
        }
        /// <summary>
        /// 获取将要绘制的坐标数据
        /// 引用输出的是源坐标值
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        public SuperPoints GetxyDataValues()
        {
            try
            {
                m_ClientRectangleData.Clear();
                SuperPoints ret = new SuperPoints();
                m_MaxCnt = (int)(xAxis.MaxValue - xAxis.MinValue) / m_TimeSpan + 1;
                int StartIdx = m_sampleFreq * 30;
                float[] ydatas = Filiter(bak_yDataValues, m_MaxCnt, ref StartIdx);
                int len = ydatas.Length;
                int tmpIdx = 0;
                int bak_len = len - StartIdx;
                int sampleCnt = bak_len <= 4000 ? 1000 : bak_len <= 8000 ? 1500 : bak_len <= 20000 ? 2000 : bak_len <= 30000 ? 2200 : 2500;
                int cnt = bak_len / sampleCnt + ((bak_len % sampleCnt == 0) ? 0 : 1);
                ret.yDataValues = new float[bak_len];
                //int offset = StartIdx > 500 ? 500 : StartIdx;
                //SubsectionzFlite = true;
                //float[] ydatas = Filiter(bak_yDataValues, StartIdx - offset, bak_len + offset);
                //SubsectionzFlite = false;
                for (int s = StartIdx; s < len; s++)
                {
                    ret.yDataValues[tmpIdx++] = ydatas[s] / ValueZoomRate;
                }
                if (tmpIdx > 0 && !IsShowValue)
                {
                    BaseValue = ret.yDataValues.Average();
                }
                tmpIdx = 0;
                cnt = 0;//暂时去掉抽点绘图
                if (cnt > 1)
                {
                    int addcnt = 1;
                    List<float> vv = new List<float>();
                    int xIdx = 0;
                    int xlen = sampleCnt * 2;
                    ret.Points = new PointF[xlen];
                    ret.SourcePoints = new PointF[xlen];
                    for (int s = 0; s < bak_len; s++)
                    {
                        vv.Add(ret.yDataValues[s]);
                        m_ClientRectangleData.Add(new PointF(xAxis.ValueRate * s * m_TimeSpan + xAxis.offSetDistance, ret.yDataValues[s]));
                        if (addcnt == cnt || s == bak_len - 1)
                        {
                            addcnt = 1;
                            float min = vv[0], max = vv[0];
                            int minIdx = 0, maxIdx = 0;
                            for (int k = 1; k < vv.Count; k++)
                            {
                                if (min > vv[k])
                                {
                                    min = vv[k];
                                    minIdx = k;
                                }
                                if (max < vv[k])
                                {
                                    max = vv[k];
                                    maxIdx = k;
                                }
                            }
                            if (minIdx < maxIdx)
                            {
                                float x = xAxis.ValueRate * (tmpIdx + minIdx) * m_TimeSpan + xAxis.offSetDistance;
                                ret.Points[xIdx] = new PointF(x, ConvertYValue(min, BaseValue));
                                ret.SourcePoints[xIdx++] = new PointF(x, min);
                                x = xAxis.ValueRate * (tmpIdx + maxIdx) * m_TimeSpan + xAxis.offSetDistance;
                                ret.Points[xIdx] = new PointF(x, ConvertYValue(max, BaseValue));
                                tmpIdx += cnt;
                                ret.SourcePoints[xIdx++] = new PointF(x, max);
                            }
                            else if (minIdx == maxIdx)
                            {
                                float x = xAxis.ValueRate * (tmpIdx + minIdx) * m_TimeSpan + xAxis.offSetDistance;
                                ret.Points[xIdx] = new PointF(x, ConvertYValue(min, BaseValue));
                                ret.SourcePoints[xIdx++] = new PointF(x, min);
                                x = xAxis.ValueRate * (tmpIdx + maxIdx + 1) * m_TimeSpan + xAxis.offSetDistance;
                                ret.Points[xIdx] = new PointF(x, ConvertYValue(max, BaseValue));
                                ret.SourcePoints[xIdx++] = new PointF(x, max);
                                tmpIdx += cnt;
                            }
                            else
                            {
                                float x = xAxis.ValueRate * (tmpIdx + maxIdx) * m_TimeSpan + xAxis.offSetDistance;
                                ret.Points[xIdx] = new PointF(x, ConvertYValue(max, BaseValue));
                                ret.SourcePoints[xIdx++] = new PointF(x, max);
                                x = xAxis.ValueRate * (tmpIdx + minIdx) * m_TimeSpan + xAxis.offSetDistance;
                                ret.Points[xIdx] = new PointF(x, ConvertYValue(min, BaseValue));
                                ret.SourcePoints[xIdx++] = new PointF(x, min);
                                tmpIdx += cnt;
                            }
                            vv.Clear();
                        }
                        else
                            addcnt++;
                    }
                    return ret;
                }
                else
                {

                    ret.SourcePoints = new PointF[bak_len];
                    ret.Points = new PointF[bak_len];
                    float basex = xAxis.ValueRate * m_TimeSpan;
                    float x = xAxis.offSetDistance;
                    try
                    {
                        for (int s = 0; s < bak_len; s++)
                        {
                            ret.Points[s] = new PointF(x, ConvertYValue(ret.yDataValues[s], BaseValue));
                            float y = Antipole ? 0 - ret.yDataValues[s] : ret.yDataValues[s];
                            m_ClientRectangleData.Add(new PointF(x, y));
                            ret.SourcePoints[s] = new PointF(x, y);
                            x += basex;
                        }
                    }
                    catch (Exception ee)
                    {

                    }
                    return ret;
                }
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }
        /// <summary>
        /// 返回一帧源坐标数据
        /// </summary>
        /// <param name="FrameCnt">当前帧序号</param>
        /// <param name="timeLine">当前时基（s）</param>
        /// <param name="offset">偏移数（s）</param>
        /// <returns></returns>
        public SuperPoints GetOneFramePoints(int FrameCnt, int timeLine, int offset)
        {
            try
            {
                m_ClientRectangleData.Clear();
                int clen = bak_yDataValues.Count;
                int cnt = timeLine / OneFrameTimeSpan;
                int offset1 = 0, offset2 = 0;
                offset1 = OneFrameTimeSpan - offset;
                if (offset1 < timeLine)
                {
                    cnt += 1;
                    offset2 = timeLine - offset1;
                }
                int dots = timeLine * m_sampleFreq;
                int startidx = (FrameCnt - 1) * m_OneFrameDataLen + offset * m_sampleFreq;
                int endidx = startidx + dots - 1;
                if (endidx >= clen && startidx < clen)
                {
                    endidx = clen - 1;
                    dots = endidx - startidx;
                }
                if (startidx >= 0 && endidx < clen)
                {
                    SubsectionzFlite = true;
                    int dotscnt = dots + 1;
                    if (startidx + dotscnt <= clen)
                        dots = dotscnt;///显示区域要多加一个点，保证曲线的连贯性
                    int offset3 = startidx > m_sampleFreq * 5 ? m_sampleFreq * 5 : startidx;
                    float[] ydatas = Filiter(bak_yDataValues, startidx - offset3, dots + offset3);
                    SubsectionzFlite = false;
                    SuperPoints sp = new SuperPoints();
                    sp.StartIndex = startidx;
                    sp.EndIndex = endidx;
                    sp.yDataValues = new float[dots];
                    Array.Copy(ydatas, offset3, sp.yDataValues, 0, dots);
                    float _baseValue = sp.yDataValues.Average() / ValueZoomRate;
                    //float _baseValue = sp.yDataValues.Average();///临时更改
                    sp.Points = new PointF[dots];
                    sp.SourcePoints = new PointF[dots];

                    for (int i = 0; i < dots; i++)
                    {
                        float x = xAxis.ValueRate * i * m_TimeSpan;
                        float y = sp.yDataValues[i] / ValueZoomRate;
                        //float y = sp.yDataValues[i];///临时更改
                        sp.Points[i] = new PointF(x, ConvertYValue(y, _baseValue));
                        y = Antipole ? 0 - y : y;
                        sp.SourcePoints[i] = new PointF(x, y);
                        sp.yDataValues[i] = y;
                        m_ClientRectangleData.Add(new PointF(x, y));
                    }
                    return sp;
                }
                return null;
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
                return null;
            }
        }
        /// <summary>
        /// 返回一帧源坐标数据
        /// </summary>
        /// <param name="FrameCnt">当前帧序号</param>
        /// <param name="timeLine">当前时基（s）</param>
        /// <param name="offset">偏移数（s）</param>
        /// <returns></returns>
        public SuperPoints GetOneFramePointsEx(int FrameCnt, int timeLine, int offset)
        {
            try
            {
                m_ClientRectangleData.Clear();
                int clen = bak_yDataValues.Count;
                int dots = timeLine * m_sampleFreq;
                if (clen > 0)
                {
                    if (dots != clen)
                    {
                        int head = ((FrameCnt - 1) * 30 + offset);
                        if (head >= 5)
                            head = 5;
                        dots = clen - head * m_sampleFreq;
                    }
                    SubsectionzFlite = true;
                    float[] ydatas = Filiter(bak_yDataValues, 0, bak_yDataValues.Count);
                    SubsectionzFlite = false;
                    SuperPoints sp = new SuperPoints();
                    sp.StartIndex = clen - dots;
                    sp.EndIndex = clen - 1;
                    sp.yDataValues = new float[dots];
                    Array.Copy(ydatas, sp.StartIndex, sp.yDataValues, 0, dots);
                    float _baseValue = sp.yDataValues.Average() / ValueZoomRate;
                    #region 注释掉降采样处理代码段
                    //if (m_sampleFreq > 1&&timeLine>120)///降采样处理，暂时去掉
                    //{
                    //    int ff = timeLine / 30;
                    //    if (ff < 1)
                    //        ff = 1;
                    //    int zoom = ff * 5;
                    //    float[] desdata = new float[sp.yDataValues.Length / zoom];
                    //    //IntPtr sptr = Marshal.AllocHGlobal(sp.yDataValues.Length);
                    //    //Marshal.Copy(sp.yDataValues, 0, sptr, sp.yDataValues.Length);
                    //    CvArr input = new CvMat(1, sp.yDataValues.Length, MatrixType.F32C1, sp.yDataValues);
                    //    CvArr ouput = new CvMat(1, desdata.Length, MatrixType.F32C1);
                    //    Cv.Resize(input, ouput);
                    //    IntPtr datptr = ouput.GetRow(0).Data;

                    //    Marshal.Copy(datptr, desdata, 0, desdata.Length);
                    //    input.Dispose();
                    //    ouput.Dispose();
                    //    sp.Points = new PointF[desdata.Length];
                    //    for (int i = 0; i < desdata.Length; i++)
                    //    {
                    //        if (float.IsNaN(desdata[i]))
                    //            desdata[i] = 0;
                    //        float x = xAxis.ValueRate * i * m_TimeSpan * zoom;
                    //        float y = desdata[i] / ValueZoomRate;
                    //        sp.Points[i] = new PointF(x, ConvertYValue(y, _baseValue));
                    //    }
                    //    sp.SourcePoints = new PointF[dots];
                    //    for (int i = 0; i < dots; i++)
                    //    {
                    //        float x = xAxis.ValueRate * i * m_TimeSpan;
                    //        float y = sp.yDataValues[i] / ValueZoomRate;
                    //        y = Antipole ? 0 - y : y;
                    //        sp.SourcePoints[i] = new PointF(x, y);
                    //        sp.yDataValues[i] = y;
                    //        m_ClientRectangleData.Add(new PointF(x, y));
                    //    }
                    //}
                    //else
                    #endregion
                    {
                        sp.Points = new PointF[dots];
                        sp.SourcePoints = new PointF[dots];
                        for (int i = 0; i < dots; i++)
                        {
                            float x = xAxis.ValueRate * i * m_TimeSpan;
                            float y = sp.yDataValues[i] / ValueZoomRate;
                            sp.Points[i] = new PointF(x, ConvertYValue(y, _baseValue));
                            y = Antipole ? 0 - y : y;
                            sp.SourcePoints[i] = new PointF(x, y);
                            sp.yDataValues[i] = y;
                            m_ClientRectangleData.Add(new PointF(x, y));
                        }
                    }
                    sp.StartIndex = ((FrameCnt - 1) * 30 + offset) * m_sampleFreq;///一帧30s
                    sp.EndIndex = sp.StartIndex + timeLine * m_sampleFreq;

                    m_endViewX = (int)Math.Round((m_ClientRectangleData.Count - 1) * TimeSpan * xAxis.ValueRate, 0);
                    return sp;
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
            m_endViewX = (int)xAxis.Displacement;
            return null;
        }
        /// <summary>
        /// 获取区域数据
        /// </summary>
        /// <param name="startIndex">开始索引</param>
        /// <param name="endIndex">结束索引</param>
        /// <returns></returns>
        public SuperPoints GetRectangeleValues(int startIndex, int endIndex)
        {
            SuperPoints sp = new SuperPoints();
            sp.StartIndex = startIndex;
            sp.EndIndex = endIndex;
            if (endIndex == 0 && startIndex == 0)
                return sp;
            int dots = endIndex - startIndex + 1;
            sp.yDataValues = new float[dots];
            sp.Points = new PointF[dots];
            sp.SourcePoints = new PointF[dots];
            SubsectionzFlite = true;
            int offset = startIndex > m_sampleFreq * 5 ? m_sampleFreq * 5 : startIndex;
            //操作派生通道会报错
            if (ReadDataHandler != null)
            {
                List<float> dats = ReadDataHandler.Invoke(startIndex - offset, endIndex + 1, ChannelNum);
                float[] ydatas = Filiter(dats, 0, dots + offset);
                SubsectionzFlite = false;
                float _basevalue = ydatas.Average();
                for (int i = 0; i < dots; i++)
                {
                    float x = xAxis.ValueRate * i * m_TimeSpan;
                    float y = ydatas[i + offset] / ValueZoomRate;
                    sp.Points[i] = new PointF(x, ConvertYValue(y, _basevalue));
                    if (Antipole)
                        y = 0 - y;
                    sp.SourcePoints[i] = new PointF(x, y);
                    sp.yDataValues[i] = y;
                }
            }
            return sp;
        }

        /// <summary>
        /// 对象深复制
        /// </summary>
        public CurveItem Clone(bool IsCloneDatasource = true)
        {
            CurveItem item = new CurveItem();
            item.ClientRectangle = this.ClientRectangle;
            item.ChannelNo = this.ChannelNo;
            item.BaseValue = this.BaseValue;
            item.BackColor = this.BackColor;
            item.Font = this.Font;
            item.ID = this.ID;
            item.ChannelNum = this.ChannelNum;
            item.Name = this.Name;
            item.PenColor = this.PenColor;
            item.PenWidth = this.PenWidth;
            item.Visible = this.Visible;
            item.yAxis.Displacement = this.yAxis.Displacement;
            item.xAxis.Displacement = this.xAxis.Displacement;
            item.xAxis.MaxValue = this.xAxis.MaxValue;
            item.yAxis.MaxValue = this.yAxis.MaxValue;
            item.yAxis.MinValue = this.yAxis.MinValue;
            item.TimeSpan = this.TimeSpan;
            item.PixelRate = this.PixelRate;
            item.ValueZoomRate = this.ValueZoomRate;
            item.MaxADValue = this.MaxADValue;
            item.MaxViewValue = this.MaxViewValue;
            item.MinADValue = this.MinADValue;
            item.MinViewValue = this.MinViewValue;
            item.SingleNotch = this.SingleNotch;
            item.HighPass = this.HighPass;
            item.LowPass = this.LowPass;
            item.PixelConstants = this.PixelConstants;
            item.ClientRectanglePoints = new List<PointF>(this.m_ClientRectangleData);
            item.FiliterDataEvent = this.FiliterDataEvent;
            item.IsCloneItem = this.IsCloneItem;
            item.Tag = this.Tag;
            if (IsCloneDatasource)
            {
                int len = this.bak_yDataValues.Count;
                float[] data = new float[len];
                this.bak_yDataValues.CopyTo(0, data, 0, len);
                item.bak_yDataValues = data.ToList();
                //item.m_xyDataValues = new PointF[this.m_xyDataValues.Length];
                //Array.Copy(m_xyDataValues, 0, item.m_xyDataValues, 0, m_xyDataValues.Length);
            }
            return item;
        }
        /// <summary>
        /// 复制通道数据并绑定
        /// </summary>
        public void CloneDataSource(CurveItem item)
        {
            m_CurrentStartIndex = item.m_CurrentStartIndex;
            this.BindYDataValues(item.bak_yDataValues);
        }
        /// <summary>
        /// 复制通道数据并绑定
        /// </summary>
        public void CloneDataSource(CurveItem item1, CurveItem item2)
        {
            int len = item1.bak_yDataValues.Count;
            int len2 = item2.bak_yDataValues.Count;
            len = len < len2 ? len : len2;
            float[] values = new float[len];
            for (int i = 0; i < len; i++)
            {
                values[i] = item1.bak_yDataValues[i] - item2.bak_yDataValues[i];
            }
            this.BindYDataValues(values);
        }
        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            try
            {
                CurrentMarks.Clear();
                bak_yDataValues.Clear();
                m_CurrentStartIndex = 0;
                m_Addindex = 0;
                if (!m_KillTask)
                {
                    m_KillTask = true;
                    th.Abort();
                }
                //m_xyDataValues=new PointF[0];
                m_registList.Clear();
            }
            catch { }
        }
        /// <summary>
        /// 获取当前数据的最大值
        /// </summary>
        /// <returns></returns>
        public int getMaxValue()
        {
            int len = m_ClientRectangleData.Count;
            float maxvalue = 0, minvalue = 0;
            if (len > 0)
                minvalue = maxvalue = m_ClientRectangleData[0].Y;
            else
                return 0;
            for (int i = 1; i < len; i++)
            {
                if (maxvalue < m_ClientRectangleData[i].Y)
                {
                    maxvalue = m_ClientRectangleData[i].Y;
                }
                if (minvalue > m_ClientRectangleData[i].Y)
                {
                    minvalue = m_ClientRectangleData[i].Y;
                }
                // minvalue = (minvalue + bak_yDataValues[i]) / 2;
            }
            return (int)((maxvalue - minvalue));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        internal void CheckPushDataDelegateList(string id)
        {
            foreach (KeyValuePair<string, PushDataToCloneItem> p in m_registList)
            {
                if (p.Key == id)
                {
                    m_PushDataToCloneItemHandle -= p.Value;
                    m_registList.Remove(p.Key);
                    break;
                }
            }
        }
        public delegate void PushDataToCloneItem(float[] datasource);
        private Dictionary<string, PushDataToCloneItem> m_registList = new Dictionary<string, PushDataToCloneItem>();
        private event PushDataToCloneItem m_PushDataToCloneItemHandle;
        /// <summary>
        /// 推送数据到其克隆的通道（实时曲线时用到）
        /// </summary>
        public event PushDataToCloneItem PushDataToCloneItemHanlde
        {
            add
            {
                CurveItem t = (value.Target as CurveItem);
                if (m_PushDataToCloneItemHandle != null)
                {
                    foreach (KeyValuePair<string, PushDataToCloneItem> p in m_registList)
                    {
                        if (p.Key == t.ID)
                        {
                            m_PushDataToCloneItemHandle -= p.Value;
                            m_registList.Remove(t.ID);
                            break;
                        }
                    }
                }
                m_PushDataToCloneItemHandle += value;
                m_registList.Add(t.ID, value);
            }
            remove
            {
                m_PushDataToCloneItemHandle -= value;
            }
        }
        public void RecivePushData(float[] data)
        {
            lock (m_lockaddData)
                this.bak_yDataValues.AddRange(data);
            int offset = bak_yDataValues.Count - m_MaxDataLength;
            lock (m_lockaddData)
            {
                if (offset > 0)
                {
                    bak_yDataValues.RemoveRange(0, offset);
                    m_CurrentStartIndex += offset;
                }
            }
        }
        public delegate List<float> ReadDataDelegate(int startIndex, int endIndex, int ID);
        /// <summary>
        /// 获取当前可见时间段数据时触发
        /// </summary>
        public event ReadDataDelegate ReadDataHandler;

        /// <summary>
        /// 读取通道数据
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        private List<float> ReadChannelData(int startIndex, int endIndex, int ID)
        {
            if (ReadDataHandler != null)
            {
                return ReadDataHandler.Invoke(startIndex, endIndex, ID);
            }
            return new List<float>();
        }
        #endregion

        public override string ToString()
        {
            return Name;
        }
    }
}
