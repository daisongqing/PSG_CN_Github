using AwareTec.Polysmith.DataCenter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareTec.Polysmith.Cloud.UI.DataModels
{
    public class EDFInstance:IDisposable
    {
        private FileStream fsRead;
        private string m_filename = "";
        private BufferedStream bufferedStream;
        private int m_captionStartIndex = 184;
        private int m_captionLenght = 0;
        private byte[] m_headBytes = new byte[0];
        private EDF m_edfData = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="FileName"></param>
        public EDFInstance(string FileName)
        {
            DateTime dt = DateTime.Now;
            m_filename = FileName;
            if (fsRead == null)
            {
                fsRead = new FileStream(m_filename, FileMode.Open);
                bufferedStream = new BufferedStream(fsRead);
            }
            bufferedStream.Seek(m_captionStartIndex, SeekOrigin.Begin);
            byte[] byteArrayRead = new byte[8];
            int readCount = bufferedStream.Read(byteArrayRead, 0, byteArrayRead.Length);
            if (readCount > 0)
            {
                m_captionLenght = int.Parse(System.Text.Encoding.ASCII.GetString(byteArrayRead, 0, 8).Trim(' '));
                m_headBytes = new byte[m_captionLenght];
                bufferedStream.Seek(0, SeekOrigin.Begin);
                readCount = bufferedStream.Read(m_headBytes, 0, m_headBytes.Length);
                if (readCount == m_captionLenght)
                {
                    m_edfData = new EDF(m_headBytes, fsRead.Length);
                    try
                    {
                        bufferedStream.Seek(0, SeekOrigin.Begin);
                        byte[] hashbyte = new byte[1024 * 10];
                        readCount = bufferedStream.Read(hashbyte, 0, hashbyte.Length);
                        m_edfData.HashCode =Global.GlobalSingleton.Instance.ComputeSHA256(hashbyte);
                    }
                    catch { }
                    m_edfData.EdfPath = m_filename;
                    //ReadBaseInfo();
                }
            }
            Console.WriteLine(string.Format("读取EDF基础数据耗时【{0}ms】", (DateTime.Now - dt).TotalMilliseconds));
        }
        /// <summary>
        /// (17 血氧 18体位 20 心率 21 体动) 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="totalSeconds"></param>
        /// <param name="ids"></param>
        private void readData(int startIndex, int totalSeconds, List<int> ids)
        {
            DateTime dt = DateTime.Now;
            int tmpindex = 0;
            int channelstartindex = m_captionLenght + startIndex;
            //获取定标通道的数据
            ChannelItem[] channels = new ChannelItem[ids.Count+1];
            int cnt = 0;
            for (int m = 0; m < m_edfData.Channels.Count; m++)
            {
                ChannelItem item = m_edfData.Channels[m];
                if (m > 0)
                    tmpindex += m_edfData.Channels[m - 1].OneFrameDataCount;
                if (ids.Contains(item.ID)|| m_edfData.Channels[m].DataSignName== "biological")
                {
                    item.DataBytes = new byte[(int)(totalSeconds * item.OneFrameDataCount)];
                    item.TmpIndex = tmpindex;
                    channels[cnt++] = item;
                }
            }
            ///先把文件位置定位一遍，加快读取速度
            int position = channelstartindex + m_edfData.OneSecondDataCounts;
            for (int i = 1; i < totalSeconds-1; i++)
            {
                position += +m_edfData.OneSecondDataCounts;
                bufferedStream.Seek(position, SeekOrigin.Begin);
            }
            Task[] tt = new Task[cnt];
            for (int m = 0; m < channels.Length; m++)
            {
                if (channels[m] != null)
                {
                    ChannelItem item = channels[m];
                    int index = 0;
                    DateTime dt2 = DateTime.Now;
                    byte[] byteArrayRead2 = new byte[item.OneFrameDataCount];
                    for (int i = 0; i < totalSeconds; i++)
                    {
                        bufferedStream.Seek(channelstartindex + item.TmpIndex, SeekOrigin.Begin);
                        int len = bufferedStream.Read(byteArrayRead2, 0, byteArrayRead2.Length);
                        Array.Copy(byteArrayRead2, 0, item.DataBytes, index, len);
                        item.TmpIndex += m_edfData.OneSecondDataCounts;
                        index += len;
                    }
                    item.DataBytesLenght = index;
                    Console.WriteLine(string.Format("读取{1}数据耗时【{0}ms】", (DateTime.Now - dt2).TotalMilliseconds, item.DataSignName));
                    tt[m] = Task.Factory.StartNew((obj) =>
                    {
                        ChannelItem channel = (ChannelItem)obj;
                        int datalen = channel.DataBytesLenght / 2;
                        channel.Data = new List<float>(datalen);
                        for (int s = 0; s < datalen; s++)
                        {
                            float value = 0;
                            if (channel.UnSignData)
                                value = BitConverter.ToUInt16(channel.DataBytes, 2 * s);
                            else
                                value = BitConverter.ToInt16(channel.DataBytes, 2 * s);
                            if (item.DataLength != 1)
                                value = (float)(channel.ViewMinValue + (value - channel.ADMinValue) * channel.ConstRateA);
                            channel.Data.Add(value);
                        }
                        channel.DataBytes = new byte[0];
                    }, item);
                }
            }
            Task.WaitAll(tt);
            for (int i = 0; i < tt.Length; i++)
            {
                tt[i].Dispose();
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            Console.WriteLine(string.Format("读取指定通道[{1}]-{2}秒数据耗时【{0}ms】", (DateTime.Now - dt).TotalMilliseconds, string.Join(",", ids), totalSeconds));
        }
        /// <summary>
        /// 获取源数据
        /// </summary>
        public EDF DataSource
        {
            get
            {
                return m_edfData;
            }
        }
        private List<float> m_spo2Values = new List<float>();
        /// <summary>
        /// 获取血氧数据
        /// </summary>
        public List<float> Spo2Values
        {
            get
            {
                return m_spo2Values;
            }
        }
        private List<float> m_heartValues = new List<float>();
        /// <summary>
        /// 获取心率数据
        /// </summary>
        public List<float> HeartValues
        {
            get
            {
                return m_heartValues;
            }
        }
        private List<float> m_posValues = new List<float>();
        /// <summary>
        /// 获取体位数据
        /// </summary>
        public List<float> PosValues
        {
            get
            {
                return m_posValues;
            }
        }
        private List<float> m_pressurePeekValues = new List<float>();
        /// <summary>
        /// 获取压力的峰值队列
        /// </summary>
        public List<float> PressurePeekValues
        {
            get
            {
                return m_pressurePeekValues;
            }
        }
        private List<float> m_mtValues = new List<float>();
        /// <summary>
        /// 获取定标数据
        /// </summary>
        public List<float> CalibrationValues
        {
            get
            {
                return m_CalibrationValues;
            }
        }
        private List<float> m_CalibrationValues = new List<float>();
        /// <summary>
        /// 获取体动数据
        /// </summary>
        public List<float> MTValues
        {
            get
            {
                return m_mtValues;
            }
        }
        /// <summary>
        /// 读取选取时间段的原数据
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="channelIDs"></param>
        /// <returns></returns>
        public bool readData(DateTime start,DateTime end,List<int> channelIDs)
        {
            if (m_edfData.IsCorrect)
            {
                int startoff = (int)(start - m_edfData.StartTime).TotalSeconds;
                int totals = (int)(end - start).TotalSeconds;
                int startindex = startoff * m_edfData.OneSecondDataCounts;
                readData(startindex, totals, channelIDs);
                return true;
            }
            return false;
        }
        public delegate void SearchProgressBarValueDelegate(long BarValue);
        public event SearchProgressBarValueDelegate SearchProgressBarValueEventHandle;
        /// <summary>
        /// 裁剪EDF文件通过小睡事件
        /// </summary>
        /// <param name="starttime">经过了排序</param>
        /// <param name="stoptime"></param>
        /// <param name="sourceEdf"></param>
        public bool readEdfDataByMultSleep(List<DateTime> StartTimes,List<DateTime> StopTimes, EDF sourceEdf)
        {
            EDF newEdf = new EDF();

            #region 头部信息 8+80+80+(8+8)+8+44+(8)+8+4
            newEdf.StartFlag = sourceEdf.StartFlag;
            newEdf.PatientNO = sourceEdf.PatientNO;
            newEdf.DeviceID = sourceEdf.DeviceID;
            //重新计算开始时间
            string time = string.Format("{0}{1}", StartTimes[0].ToString("dd.MM.yy"), StartTimes[0].ToString("HH:mm:ss"));
            byte[] newtime = System.Text.Encoding.ASCII.GetBytes(time);
            newEdf.RecordDate = StartTimes[0].ToString("dd.MM.yy");
            newEdf.RecordTime = StartTimes[0].ToString("HH:mm:ss");
            newEdf.CaptainLength = sourceEdf.CaptainLength;
            newEdf.Reserve = sourceEdf.Reserve;
            //重新计算文件记录次数
            int StartOffTime = (int)(StartTimes[0] - sourceEdf.StartTime).TotalSeconds;
            int NewTotalTime = 0;
            int recordCnt = NewTotalTime / sourceEdf.RecordTimeSpan;
            byte[] newrecordCnt = System.Text.Encoding.ASCII.GetBytes(recordCnt.ToString().PadRight(8, ' '));
            newEdf.RecordCnt = recordCnt;
            newEdf.RecordTimeSpan = sourceEdf.RecordTimeSpan;
            newEdf.DataSignCnt = sourceEdf.DataSignCnt;
            #endregion

            //回传给进度条的值
            long barvalue = 0;
            //一秒数据的长度
            int oneSecondDataLen = sourceEdf.OneSecondDataCounts;
            bool SaveHandData = true;
            //开辟临时缓存内存（存放读取的头部信息）
            byte[] byteArrayRead = new byte[1024 * 1024];
            //开辟临时缓存内存（存放写入的头部信息）
            byte[] byteArrayWrite = new byte[1024 * 1024];
            //do
            //{
            //}
            //while (!fsRead.CanRead);
            if (!fsRead.CanRead)
            {
                //fsRead.Unlock(0,fsRead.Length);
                return false;
                //string name = fsRead.Name;
                //fsRead.Close();
                //fsRead.Dispose();
                //fsRead = new FileStream(m_filename, FileMode.Open,FileAccess.Read);
            }
            barvalue = 5;
            if (SearchProgressBarValueEventHandle != null)
                SearchProgressBarValueEventHandle.Invoke(barvalue);
            BufferedStream bufferedStream = new BufferedStream(fsRead, 1048576);
            long oldPosition = fsRead.Position;
            //进入回放之后，文件流读取的位置会改变，所以这里需要重置为0
            fsRead.Position = 0;
            //需要保存数据的总个数（用作比较）
            long saveDataCnt = 0;
            //需要保存数据的总个数
            long recordDataCnt = 0;
            //文件另存为
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //设置文件类型
            saveFileDialog.Filter = "EDF数据文件|*.edf";
            //保存对话框是否记忆上次打开的目录
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.FileName = string.Format("{0}_{1}_{2}_{3}_{4}_{5}_{6:HHmmss}", "New", Channel.Default.Patient.PatientNo, Channel.Default.Patient.PatientName, Channel.Default.Patient.Gender, Channel.Default.Patient.Age, StartTimes[0].ToString("yyyy-MM-dd"), StartTimes[0]);
            //文件保存路径
            string localFilePath = "";
            //点了保存按钮进入
            if (DialogResult.OK == saveFileDialog.ShowDialog())
            {
               //获得文件路径
               localFilePath = saveFileDialog.FileName.ToString();
            }
            else
            {
                return false;
            }
            long readfilesize = fsRead.Length;
            long barvalueneedchangecount = readfilesize / 1048576;
            int barvaluehavechangecount = 1;
            BinaryWriter bw = new BinaryWriter(new FileStream(localFilePath, FileMode.Create));
            for (int i = 0; i < StartTimes.Count; i++)
            {
                saveDataCnt = 0;
                recordDataCnt = (int)(StopTimes[i] - StartTimes[i]).TotalSeconds * oneSecondDataLen;
                //定位下一个片段的读取流起始位置
                if (i > 0)
                {
                    StartOffTime = (int)(StartTimes[i] - sourceEdf.StartTime).TotalSeconds;
                    bufferedStream.Seek(newEdf.CaptainLength + StartOffTime * oneSecondDataLen , SeekOrigin.Begin);
                }
                while (true)
                {
                    //readCount  每次实际读取到的个数
                    int readCount = bufferedStream.Read(byteArrayRead, 0, byteArrayRead.Length);
                    if (readCount > 0)
                    {
                        //将读出来的头部信息写入数据
                        if (SaveHandData)
                        {
                            Array.Copy(byteArrayRead, 0, byteArrayWrite, 0, newEdf.CaptainLength);
                            //更改新的起始时间(格式中的位置为168)和记录次数（格式中的位置为236）
                            Array.Copy(newtime, 0, byteArrayWrite, 168, 16);
                            Array.Copy(newrecordCnt, 0, byteArrayWrite, 236, 8);
                            //将读取流的起始位置变成新开始时间的位置
                            bufferedStream.Seek(newEdf.CaptainLength + StartOffTime * oneSecondDataLen, SeekOrigin.Begin);
                            bw.Write(byteArrayWrite, 0, newEdf.CaptainLength);
                            bw.Flush();
                            SaveHandData = false;
                            barvalue = 10;
                            if (SearchProgressBarValueEventHandle != null)
                                SearchProgressBarValueEventHandle.Invoke(barvalue);
                        }
                        else
                        {
                            saveDataCnt += readCount;
                            if (saveDataCnt > recordDataCnt)
                            {
                                bw.Write(byteArrayRead, 0, (int)(recordDataCnt + readCount - saveDataCnt));
                                bw.Flush();
                                break;
                            }
                            else
                            {
                                bw.Write(byteArrayRead);
                            }
                        }
                        if (SearchProgressBarValueEventHandle != null && barvalueneedchangecount > 0)
                        {
                            SearchProgressBarValueEventHandle.Invoke(barvalue + 90 * barvaluehavechangecount / barvalueneedchangecount);
                            barvaluehavechangecount++;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            fsRead.Position = oldPosition;
            barvalue = 100;
            if (SearchProgressBarValueEventHandle != null)
                SearchProgressBarValueEventHandle.Invoke(barvalue);
            return true;
        }

        /// <summary>
        /// 裁剪EDF文件通过系统开关灯事件
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="stoptime"></param>
        /// <returns></returns>
        //public void readEdfDataByOnOffTime(DateTime starttime, DateTime stoptime, EDF sourceEdf)
        //{
        //    EDF newEdf = new EDF();

        //    #region 头部信息 8+80+80+(8+8)+8+44+(8)+8+4
        //    newEdf.StartFlag = sourceEdf.StartFlag;
        //    newEdf.PatientNO = sourceEdf.PatientNO;
        //    newEdf.DeviceID = sourceEdf.DeviceID;
        //    //重新计算开始时间
        //    string time = string.Format("{0}{1}", starttime.ToString("dd.MM.yy"), starttime.ToString("HH:mm:ss"));
        //    byte[] newtime = System.Text.Encoding.ASCII.GetBytes(time);
        //    newEdf.RecordDate = starttime.ToString("dd.MM.yy");
        //    newEdf.RecordTime = starttime.ToString("HH:mm:ss");
        //    newEdf.CaptainLength = sourceEdf.CaptainLength;
        //    newEdf.Reserve = sourceEdf.Reserve;
        //    //重新计算文件记录次数
        //    int StartOffTime = (int)(starttime - sourceEdf.StartTime).TotalSeconds;
        //    int NewTotalTime = (int)(stoptime - starttime).TotalSeconds;
        //    int recordCnt = NewTotalTime / sourceEdf.RecordTimeSpan;
        //    byte[] newrecordCnt = System.Text.Encoding.ASCII.GetBytes(recordCnt.ToString().PadRight(8, ' '));
        //    newEdf.RecordCnt = recordCnt;
        //    newEdf.RecordTimeSpan = sourceEdf.RecordTimeSpan;
        //    newEdf.DataSignCnt = sourceEdf.DataSignCnt;
        //    #endregion
            
        //    //一秒数据的长度
        //    int oneSecondDataLen = sourceEdf.OneSecondDataCounts;
        //    bool SaveHandData = true;
        //    //开辟临时缓存内存（存放读取的头部信息）
        //    byte[] byteArrayRead = new byte[10 * 1024];                               
        //    //开辟临时缓存内存（存放写入的头部信息）
        //    byte[] byteArrayWrite = new byte[10 * 1024];

        //    BufferedStream bufferedStream = new BufferedStream(fsRead, 10240);
        //    long oldPosition = fsRead.Position;
        //    //进入回放之后，文件流读取的位置会改变，所以这里需要重置为0
        //    fsRead.Position = 0;
        //    //需要保存数据的总个数（用作比较）
        //    long saveDataCnt = 0;
        //    //需要保存数据的总个数
        //    long recordDataCnt = recordCnt * oneSecondDataLen ;
        //    BinaryWriter bw = new BinaryWriter(new FileStream("E:\\PengCreat.edf", FileMode.Create));
        //    while (true)
        //    {
        //        //readCount  每次实际读取到的个数
        //        int readCount = bufferedStream.Read(byteArrayRead, 0, byteArrayRead.Length);
        //        if (readCount > 0)
        //        {
        //            //将读出来的头部信息写入数据
        //            if (SaveHandData)
        //            {
        //                Array.Copy(byteArrayRead, 0, byteArrayWrite, 0, newEdf.CaptainLength);
        //                //更改新的起始时间(格式中的位置为168)和记录次数（格式中的位置为236）
        //                Array.Copy(newtime, 0, byteArrayWrite, 168, 16);
        //                Array.Copy(newrecordCnt, 0, byteArrayWrite, 236, 8);
        //                //将读取流的起始位置变成新开始时间的位置
        //                bufferedStream.Seek(newEdf.CaptainLength + StartOffTime * oneSecondDataLen, SeekOrigin.Begin);
        //                bw.Write(byteArrayWrite, 0, newEdf.CaptainLength);
        //                bw.Flush();
        //                SaveHandData = false;
        //            }
        //            else
        //            {
        //                saveDataCnt += readCount;
        //                if (saveDataCnt > recordDataCnt)
        //                {
        //                    bw.Write(byteArrayRead, 0, (int)(recordDataCnt + readCount - saveDataCnt));
        //                    bw.Flush();
        //                    break;
        //                }
        //                else
        //                {
        //                    bw.Write(byteArrayRead);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            break;
        //        }
        //    }
        //    fsRead.Position = oldPosition;
        //    bufferedStream.Close();
        //    bufferedStream.Dispose();
        //    bw.Close();
        //    bw.Dispose();
        //}
        /// <summary>
        /// 读取选取时间段的原数据
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="channelIDs"></param>
        /// <returns></returns>
        public bool readData(DateTime start, DateTime end, List<pChart.CurveItem> curveItems)
        {
            try
            {
                if (m_edfData.IsCorrect)
                {
                    DateTime dt = DateTime.Now;
                    List<pChart.CurveItem> all = curveItems.Where(t => t.Visible).ToList();
                    int vaildlen = all.Count();
                    if (vaildlen < 1)
                        return true;
                    List<int> channelIDs = all.Select(t => t.ChannelNum).ToList();
                    int ts = (int)(start - m_edfData.StartTime).TotalSeconds;
                    int ts2 = (int)(end - start).TotalSeconds;
                    int startindex = ts * m_edfData.OneSecondDataCounts;
                    int tmpindex = 0;
                    ChannelItem[] channels = new ChannelItem[vaildlen];
                    int cnt = 0;///本次要转换的通道数量
                    for (int m = 0; m < m_edfData.Channels.Count; m++)
                    {
                        ChannelItem item = m_edfData.Channels[m];
                        if (m > 0)
                            tmpindex += m_edfData.Channels[m - 1].OneFrameDataCount;
                        if (channelIDs.Contains(item.ID))
                        {
                            item.DataBytes = new byte[(int)(ts2 * item.OneFrameDataCount)];
                            item.TmpIndex = tmpindex;
                            channels[cnt++] = item;
                        }
                    }
                    ///cnt有可能小于vaildlen，因为曲线通道存在克隆或者合成通道
                    Task[] tt = new Task[cnt];
                    int taskindex = 0;
                    int channelstartindex = m_captionLenght + startindex;
                    for (int m = 0; m < cnt; m++)
                    {
                        ChannelItem item = channels[m];
                        for (int n = 0; n < vaildlen; n++)
                        {
                            if (all[n].ChannelNum == item.ID && !all[n].IsCloneItem)
                            {
                                int index = 0;
                                byte[] byteArrayRead = new byte[item.OneFrameDataCount];
                                for (int i = 0; i < ts2; i++)
                                {
                                    bufferedStream.Seek(channelstartindex + item.TmpIndex, SeekOrigin.Begin);
                                    int len = bufferedStream.Read(byteArrayRead, 0, byteArrayRead.Length);
                                    Array.Copy(byteArrayRead, 0, item.DataBytes, index, len);
                                    item.TmpIndex += m_edfData.OneSecondDataCounts;
                                    index += len;
                                }
                                item.DataBytesLenght = item.DataBytes.Length;
                                tt[taskindex++] = Task.Factory.StartNew((obj) =>
                                {
                                    try
                                    {
                                        object[] args = (object[])obj;
                                        ChannelItem channel = (ChannelItem)args[0];
                                        pChart.CurveItem curve = (pChart.CurveItem)args[1];
                                        int datalen = channel.DataBytesLenght / 2;
                                        channel.Data = new List<float>(datalen);
                                        for (int s = 0; s < datalen; s++)
                                        {
                                            float value = 0;
                                            if (channel.UnSignData)
                                                value = BitConverter.ToUInt16(channel.DataBytes, 2 * s);
                                            else
                                                value = BitConverter.ToInt16(channel.DataBytes, 2 * s);
                                            if (channel.DataLength != 1)
                                                value = (float)(channel.ViewMinValue + (value - channel.ADMinValue) * channel.ConstRateA);
                                            channel.Data.Add(value);
                                        }
                                        curve.BindYDataValues(channel.Data);
                                        channel.Data.Clear();
                                        channel.DataBytes = new byte[0];
                                    }
                                    catch (Exception ee)
                                    {
                                        Console.WriteLine(ee.Message);
                                    }
                                }, new object[] { item, all[n] });
                                break;
                            }
                        }
                    }
                    Task.WaitAll(tt);
                    for (int i = 0; i < tt.Length; i++)
                    {
                        tt[i].Dispose();
                    }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    Console.WriteLine(string.Format("当前显示区域的数据从读取到绘制总耗时：{0}ms", (DateTime.Now - dt).TotalMilliseconds));
                    return true;
                }
                return false;
            }
            catch(Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
            return false;
        }
        /// <summary>
        /// 读取单通道数据
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="totalSeconds"></param>
        /// <param name="channelID"></param>
        public List<float> readChanelData(int startIndex, int endIndex, int channelID)
        {
            List<float> ret = null;
            int tmpindex = 0;
            ChannelItem channel = null;
            int lenght = endIndex - startIndex;
            for (int m = 0; m < m_edfData.Channels.Count; m++)
            {
                ChannelItem item = m_edfData.Channels[m];
                if (m > 0)
                    tmpindex += m_edfData.Channels[m - 1].OneFrameDataCount;
                if (channelID == item.ID)
                {

                    item.DataBytes = new byte[lenght * 2];
                    item.TmpIndex = tmpindex;
                    channel = item;
                    break;
                }
            }
            //如果是初筛的数据 查看回放时 选择全呼吸 在脑电通道操作会出错 这里做限制
            if (channel==null)
            {
                return new List<float>();
            }
            int endts = endIndex / channel.DataLength;
            int endms = endIndex % channel.DataLength;
            int startts = (startIndex / channel.DataLength);
            int startms = (startIndex % channel.DataLength);
            int totalSeconds = endts - startts  + (endms != 0 ? 1 : 0);
            int channelstartindex = m_captionLenght + startts * m_edfData.OneSecondDataCounts;
            int index = 0;
            byte[] byteArrayRead2;
            int last = totalSeconds - 1;
            for (int i = 0; i <= last; i++)
            {
                if (i == 0 && startms != 0)
                {
                    byteArrayRead2 = new byte[channel.OneFrameDataCount - startms * 2];
                    bufferedStream.Seek(channelstartindex + channel.TmpIndex + startms * 2, SeekOrigin.Begin);
                }
                else if (i == last && endms != 0)
                {
                    byteArrayRead2 = new byte[endms * 2];
                    bufferedStream.Seek(channelstartindex + channel.TmpIndex, SeekOrigin.Begin);
                }
                else
                {
                    byteArrayRead2 = new byte[channel.OneFrameDataCount];
                    bufferedStream.Seek(channelstartindex + channel.TmpIndex, SeekOrigin.Begin);
                }
                int len = bufferedStream.Read(byteArrayRead2, 0, byteArrayRead2.Length);
                Array.Copy(byteArrayRead2, 0, channel.DataBytes, index, len);
                channel.TmpIndex += m_edfData.OneSecondDataCounts;
                index += len;
            }
            channel.DataBytesLenght = index;
            ret = new List<float>(lenght);
            for (int s = 0; s < lenght; s++)
            {
                float value = 0;
                if (channel.UnSignData)
                    value = BitConverter.ToUInt16(channel.DataBytes, 2 * s);
                else
                    value = BitConverter.ToInt16(channel.DataBytes, 2 * s);
                if (channel.DataLength != 1)
                    value = (float)(channel.ViewMinValue + (value - channel.ADMinValue) * channel.ConstRateA);
                ret.Add(value);
            }
            channel.DataBytes = new byte[0];
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            return ret;
        }
        /// <summary>
        /// 读取基础数据
        /// </summary>
        /// <returns></returns>
        public bool ReadBaseInfo()
        {
            bufferedStream.Seek(m_captionLenght, SeekOrigin.Begin);
            ///获取基础信息通道数据(15 压力 17 血氧 18体位 20 心率 21 体动)
            readData(0, (int)(m_edfData.EndTime - m_edfData.StartTime).TotalSeconds, new List<int> {15, 17, 18, 20, 21 });
            for (int i = 0; i < m_edfData.Channels.Count; i++)
            {
                ChannelItem item = m_edfData.Channels[i];
                if (item.ID == 17)
                {
                    m_spo2Values = new List<float>(item.Data);
                }
                else if (item.ID == 18)
                {
                    m_posValues = new List<float>(item.Data);
                }
                else if (item.ID == 20)
                {
                    m_heartValues = new List<float>(item.Data);
                }
                else if (item.ID == 21)
                {
                    m_mtValues = new List<float>(item.Data);
                }
                else if (item.DataSignName == "biological")
                {
                    m_CalibrationValues = new List<float>(item.Data);
                }
                else if (item.ID == 15)
                {
                    for (int s = 1; s < item.Data.Count - 1; s++)
                    {
                        if (item.Data[s] > item.Data[s - 1] && item.Data[s] > item.Data[s + 1])
                        {
                            float value = item.Data[s] / 98.067f;
                            if (value >= 1)
                                m_pressurePeekValues.Add(value);
                        }
                    }
                }
                item.Data.Clear();
            }
            return true;
        }
        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务。
        /// </summary>
        public void Dispose()
        {
            try
            {
                bufferedStream.Close();
            }
            catch { }
            try
            {
                bufferedStream.Dispose();
            }
            catch { }
            try
            {
                fsRead.Close();
            }
            catch { }
            try
            {
                fsRead.Dispose();
            }
            catch { }
        }
    }
}
