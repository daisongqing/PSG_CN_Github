using AwareTec.Polysmith.Cloud.UI.DataModels.Global;
using AwareTec.Polysmith.DataCenter;
using AwareTec.Polysmith.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.DataModels
{
    /// <summary>
    /// EDF文件操作类
    /// </summary>
    public class EDF:DataCenter.BaseFile
    {
        private string m_Path = AppDomain.CurrentDomain.BaseDirectory + "Data";
        static EDF m_Default = null;
        /// <summary>
        /// 单例模式
        /// </summary>
        public static EDF Default
        {
            get
            {
                return m_Default ?? (m_Default = new EDF());
            }
        }

        public EDF()
        {
            Channels = new List<ChannelItem>();
        }

        public EDF(byte[] m_headBytes,long dataLength)
        {
            int TmpIdx = 0;
            int lastindex = m_headBytes.Length - 1;
            int endIndex = TmpIdx + 8;
            if (endIndex > lastindex)
                return;
            this.StartFlag = System.Text.Encoding.ASCII.GetString(m_headBytes, TmpIdx, 8);
            TmpIdx = endIndex;
            endIndex = TmpIdx + 80;
            if (endIndex > lastindex)
                return;
            this.PatientNO = System.Text.Encoding.ASCII.GetString(m_headBytes, TmpIdx, 80);
            TmpIdx = endIndex;
            endIndex = TmpIdx + 80;
            if (endIndex > lastindex)
                return;
            this.DeviceID = System.Text.Encoding.ASCII.GetString(m_headBytes, TmpIdx, 80);
            TmpIdx = endIndex;
            endIndex = TmpIdx + 8;
            if (endIndex > lastindex)
                return;
            this.RecordDate = System.Text.Encoding.ASCII.GetString(m_headBytes, TmpIdx, 8);
            TmpIdx = endIndex;
            endIndex = TmpIdx + 8;
            if (endIndex > lastindex)
                return;
            this.RecordTime = System.Text.Encoding.ASCII.GetString(m_headBytes, TmpIdx, 8);
            TmpIdx = endIndex;
            endIndex = TmpIdx + 8;
            if (endIndex > lastindex)
                return;
            string[] date = this.RecordDate.Split('.');
            int[] rts = this.RecordTime.Split(new char[] { '.', ':' }).Select(t => int.Parse(t == "00" ? "0" : t)).ToArray();
            this.StartTime = DateTime.Parse(string.Format("{0} {1}:{2}:{3}", string.Format("{0}/{1}/{2}", date[2], date[1], date[0]), rts[0] > 59 ? 59 : rts[0], rts[1] > 59 ? 59 : rts[1], rts[2] > 59 ? 59 : rts[2]));
            this.CaptainLength = int.Parse(System.Text.Encoding.ASCII.GetString(m_headBytes, TmpIdx, 8).Trim(' '));
            TmpIdx = endIndex;
            endIndex = TmpIdx + 44;
            if (endIndex > lastindex)
                return;
            this.Reserve = System.Text.Encoding.ASCII.GetString(m_headBytes, TmpIdx, 44);
            TmpIdx = endIndex;
            endIndex = TmpIdx + 8;
            if (endIndex > lastindex)
                return;
            string ss = System.Text.Encoding.ASCII.GetString(m_headBytes, TmpIdx, 8);
            this.RecordCnt = int.Parse(ss.Trim(' '));
            TmpIdx = endIndex;
            endIndex = TmpIdx + 8;
            if (endIndex > lastindex)
                return;
            this.RecordTimeSpan = int.Parse(System.Text.Encoding.ASCII.GetString(m_headBytes, TmpIdx, 8).Trim(' '));
            TmpIdx = endIndex;
            endIndex = TmpIdx + 4;
            if (endIndex > lastindex)
                return;
            this.DataSignCnt = int.Parse(System.Text.Encoding.ASCII.GetString(m_headBytes, TmpIdx, 4).Trim(' '));
            TmpIdx = endIndex;
            this.Channels = new List<ChannelItem>(this.DataSignCnt);
            int DataStartIndx = 0;
            Channels = new List<ChannelItem>(DataSignCnt);
            for (int i = 0; i < this.DataSignCnt; i++)
            {
                if (Interrupt)
                {
                    return;
                }
                int offset = 0;
                ChannelItem channel = new ChannelItem();
                channel.DataSignName = System.Text.Encoding.ASCII.GetString(m_headBytes, TmpIdx + 16 * i, 16).Trim(' ').ToLower();
                offset += 16 * this.DataSignCnt;
                channel.SensorType = System.Text.Encoding.ASCII.GetString(m_headBytes, TmpIdx + offset + i * 80, 80).Trim(' ');
                offset += 80 * this.DataSignCnt;
                channel.Unit = System.Text.Encoding.ASCII.GetString(m_headBytes, TmpIdx + offset + i * 8, 8).Trim(' ');
                offset += 8 * this.DataSignCnt;
                ss = System.Text.Encoding.ASCII.GetString(m_headBytes, TmpIdx + offset + i * 8, 8);
                channel.ViewMinValue = float.Parse(ss.Trim(' '));
                channel.ViewMinValue = (channel.Unit == "V" || channel.Unit == "degF") ? channel.ViewMinValue * 1000000 : channel.ViewMinValue;
                offset += 8 * this.DataSignCnt;
                ss = System.Text.Encoding.ASCII.GetString(m_headBytes, TmpIdx + offset + i * 8, 8);
                channel.ViewMaxValue = float.Parse(ss.Trim(' '));
                channel.ViewMaxValue = (channel.Unit == "V" || channel.Unit == "degF") ? channel.ViewMaxValue * 1000000 : channel.ViewMaxValue;
                offset += 8 * this.DataSignCnt;
                ss = System.Text.Encoding.ASCII.GetString(m_headBytes, TmpIdx + offset + i * 8, 8);
                channel.ADMinValue = float.Parse(ss.Trim(' '));
                offset += 8 * this.DataSignCnt;
                ss = System.Text.Encoding.ASCII.GetString(m_headBytes, TmpIdx + offset + i * 8, 8);
                channel.ADMaxValue = float.Parse(ss.Trim(' '));
                offset += 8 * this.DataSignCnt;
                channel.FilterInfo = System.Text.Encoding.ASCII.GetString(m_headBytes, TmpIdx + offset + i * 80, 80).Trim(' ');
                offset += 80 * this.DataSignCnt;
                channel.DataLength = int.Parse(System.Text.Encoding.ASCII.GetString(m_headBytes, TmpIdx + offset + i * 8, 8).Trim(' '));
                offset += 8 * this.DataSignCnt;
                channel.Reserve = System.Text.Encoding.ASCII.GetString(m_headBytes, TmpIdx + offset + i * 32, 32).Trim(' ');
                offset += 32 * this.DataSignCnt;
                if (i == DataSignCnt - 1)
                    DataStartIndx = TmpIdx + offset;
                channel.ConstRateA = (channel.ViewMaxValue - channel.ViewMinValue) / (channel.ADMaxValue - channel.ADMinValue);
                channel.ChannelIndex = i;
                channel.ID = -1;
                ChannelTable find = GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelTable.Find(t => t.SignName.ToLower() == channel.DataSignName);
                if (find != null)
                    channel.ID = find.ID;
                this.Channels.Add(channel);
            }
            IsCorrect = DataStartIndx == CaptainLength;
            if (IsCorrect)
            {
                ///算出一秒钟的数据总量
                for (int j = 0; j < this.DataSignCnt; j++)
                {
                    this.OneSecondDataCounts += (this.Channels[j].OneFrameDataCount);
                }
                this.EndTime = this.StartTime.AddSeconds((dataLength - CaptainLength) / this.OneSecondDataCounts);
            }
        }
        /// <summary>
        /// edf基础数据存储目录
        /// </summary>
        public string DefaultEdfSavePath
        {
            set
            {
                m_Path = value;
            }
            get
            {
                if (m_Path != Channel.Default.SystemSetting.SaveEdfPath)
                {
                    if (Directory.Exists(Channel.Default.SystemSetting.SaveEdfPath))
                    {
                        m_Path = Channel.Default.SystemSetting.SaveEdfPath;
                    }
                }
                return m_Path;
            }
        }
        /// <summary>
        /// 是否中断任务
        /// </summary>
        public bool Interrupt = false;
        /// <summary>
        /// 获取关键字
        /// </summary>
        /// <returns></returns>
        public string getMatchKey()
        {
            string ret = "";
            if (!string.IsNullOrEmpty(PatientNO))
            {
                string[] ss = PatientNO.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (ss.Length > 4)
                {
                    ret = ss[4];
                }
            }
            return ret;
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="fileName"></param>
        public byte[] ReadFile(string fileName,bool All=true)
        {
            byte[] rangeBytes;
            if (File.Exists(fileName))
            {
                int offset = 0;
                try
                {
                    //构造读取文件流对象
                    using (FileStream fsRead = new FileStream(fileName, FileMode.Open)) //打开文件，不能创建新的
                    {
                        rangeBytes = new byte[All ? fsRead.Length : 200];
                        //开辟临时缓存内存
                        byte[] byteArrayRead = new byte[All ? 1024 * 1024 : 200]; //  1字节*1024 = 1k 1k*1024 = 1M内存
                        using (BufferedStream bufferedStream = new BufferedStream(fsRead))
                        {
                            DateTime dt = DateTime.Now;
                            //通过死缓存去读文本中的内容
                            while (!Interrupt)
                            {
                                //readCount  这个是保存真正读取到的字节数
                                int readCount = bufferedStream.Read(byteArrayRead, 0, byteArrayRead.Length);
                                if (readCount > 0)
                                {
                                    Buffer.BlockCopy(byteArrayRead, 0, rangeBytes, offset, readCount);
                                    //Array.Copy(byteArrayRead, 0, rangeBytes, offset, readCount);
                                    offset += readCount;
                                    if (offset == rangeBytes.Length)
                                        break;
                                    bufferedStream.Seek(offset, SeekOrigin.Begin);
                                }
                                else
                                    break;
                                if (!All)
                                {
                                    break;
                                }
                            }
                            Console.WriteLine(string.Format("*****读取({1}M)文件耗时{0}ms*******", (DateTime.Now - dt).TotalMilliseconds, fsRead.Length / 1024 / 1024));
                            bufferedStream.Close();
                        }
                        fsRead.Close();
                    }
                    return rangeBytes;
                }
                catch (Exception ee)
                {
                    AhDung.MessageTip.ShowError(ee.Message, 1500);
                    return new byte[0];
                }
            }
            return new byte[0];
        }
        public byte[] ReadFile(string fileName, int dataLenght)
        {
            byte[] rangeBytes;
            if (File.Exists(fileName))
            {
                int offset = 0;
                try
                {
                    //构造读取文件流对象
                    using (FileStream fsRead = new FileStream(fileName, FileMode.Open)) //打开文件，不能创建新的
                    {
                        rangeBytes = new byte[dataLenght];
                        using (BufferedStream bufferedStream = new BufferedStream(fsRead))
                        {
                            //通过死缓存去读文本中的内容
                            while (!Interrupt)
                            {
                                //开辟临时缓存内存
                                byte[] byteArrayRead = new byte[dataLenght>1024?1024:dataLenght]; //  1字节*1024 = 1k 
                                //readCount  这个是保存真正读取到的字节数
                                int readCount = bufferedStream.Read(byteArrayRead, 0, byteArrayRead.Length);
                                if (readCount > 0)
                                {
                                    Array.Copy(byteArrayRead, 0, rangeBytes, offset, readCount);
                                    offset += readCount;
                                    dataLenght -= readCount;
                                }
                                else
                                    break;
                                if (dataLenght==0)
                                {
                                    break;
                                }
                            }
                            bufferedStream.Close();
                        }
                        fsRead.Close();
                    }
                    return rangeBytes;
                }
                catch (Exception ee)
                {
                    AhDung.MessageTip.ShowError(ee.Message, 1500);
                    return new byte[0];
                }
            }
            return new byte[0];
        }
        /// <summary>
        /// 转换成edf对象
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public EDF ConvertToEDF(byte[] datas)
        {
            DateTime dt = DateTime.Now;
            int lastindex = datas.Length - 1;
            EDF ret = new EDF(datas,datas.Length);
            if (!ret.IsCorrect)
                return ret;
            try
            {
                DateTime dt2 = DateTime.Now;
                ///算出总秒数
                int no = (lastindex - ret.CaptainLength + 1) / ret.OneSecondDataCounts;
                ///算出每个通道的总数据量
                for (int j = 0; j < ret.DataSignCnt; j++)
                {
                    ret.Channels[j].DataBytes = new byte[no * ret.Channels[j].OneFrameDataCount];
                }
                for (int i = ret.CaptainLength; i <= lastindex; )
                {
                    for (int j = 0; j < ret.DataSignCnt; j++)
                    {
                        if (Interrupt)
                        {
                            return ret;
                        }
                        if (ret.Channels[j].TmpIndex < ret.Channels[j].DataBytes.Length)
                        {
                            Buffer.BlockCopy(datas, i, ret.Channels[j].DataBytes, ret.Channels[j].TmpIndex, ret.Channels[j].OneFrameDataCount);
                            ret.Channels[j].TmpIndex += ret.Channels[j].OneFrameDataCount;
                        }
                        i += ret.Channels[j].OneFrameDataCount;
                    }
                }
                datas = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                Console.WriteLine(string.Format("取得所有通道bytes数据：{0}ms", (DateTime.Now - dt).TotalMilliseconds));
                Task[] tt = new Task[ret.Channels.Count];
                for (int i = 0; i < ret.Channels.Count; i++)
                {
                    tt[i] = Task.Factory.StartNew((obj) =>
                    {
                        int idx = (int)obj;
                        int datalen = ret.Channels[idx].DataBytes.Length / 2;
                        ret.Channels[idx].Data = new List<float>(datalen);
                        for (int s = 0; s < datalen; s++)
                        {
                            float value =  BitConverter.ToInt16(ret.Channels[idx].DataBytes, 2 * s);
                            value = (float)(ret.Channels[idx].ViewMinValue + (value - ret.Channels[idx].ADMinValue) * ret.Channels[idx].ConstRateA);
                            ret.Channels[idx].Data.Add(value);
                        }
                        ret.Channels[idx].DataBytes=new byte[0];
                    }, i);
                }
                Task.WaitAll(tt);
                for (int i = 0; i < tt.Length; i++)
                {
                    tt[i].Dispose();
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                int offset = ret.RecordTimeSpan * ret.RecordCnt;
                if (offset == 0)
                {
                    offset = ret.Channels.Select(t => t.RecordSeconds).Max();
                }
                else
                {
                    int max = ret.Channels.Select(t => t.RecordSeconds).Max();
                    if (max < offset)
                        offset = max;
                }
                ret.EndTime = ret.StartTime.AddSeconds(offset);
                TimeSpan ts = (DateTime.Now - dt2);
                Console.WriteLine(string.Format("edf数据部分转换耗时：{0}ms", ts.TotalMilliseconds));
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
            Console.WriteLine(string.Format("edf转换总耗时：{0}ms", (DateTime.Now - dt).TotalMilliseconds));
            return ret;
        }
        /// <summary>
        /// 生成新数组
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public byte[] CreatNewSource(byte[] datas, DateTime startTime, DateTime endTime)
        {
            int TmpIdx = 0;
            int lastindex = datas.Length - 1;
            EDF ret = new EDF();
            int endIndex = TmpIdx + 8;
            if (endIndex > lastindex)
                return datas;
            TmpIdx = endIndex;
            endIndex = TmpIdx + 80;
            if (endIndex > lastindex)
                return datas;
            TmpIdx = endIndex;
            endIndex = TmpIdx + 80;
            if (endIndex > lastindex)
                return datas;
            TmpIdx = endIndex;
            endIndex = TmpIdx + 8;
            if (endIndex > lastindex)
                return datas;
            int idx_t=TmpIdx;
            ret.RecordDate = System.Text.Encoding.ASCII.GetString(datas, TmpIdx, 8);
            TmpIdx = endIndex;
            endIndex = TmpIdx + 8;
            if (endIndex > lastindex)
                return datas;
            ret.RecordTime = System.Text.Encoding.ASCII.GetString(datas, TmpIdx, 8);
            TmpIdx = endIndex;
            endIndex = TmpIdx + 8;
            if (endIndex > lastindex)
                return datas;
            string[] date = ret.RecordDate.Split('.');
            int[] rts = ret.RecordTime.Split(new char[] { '.', ':' }).Select(t => int.Parse(t == "00" ? "0" : t)).ToArray();
            ret.StartTime = DateTime.Parse(string.Format("{0} {1}:{2}:{3}", string.Format("{0}/{1}/{2}", date[2], date[1], date[0]), rts[0] > 59 ? 59 : rts[0], rts[1] > 59 ? 59 : rts[1], rts[2] > 59 ? 59 : rts[2]));
            ret.CaptainLength = int.Parse(System.Text.Encoding.ASCII.GetString(datas, TmpIdx, 8).Trim(' '));
            TmpIdx = endIndex;
            endIndex = TmpIdx + 44;
            if (endIndex > lastindex)
                return datas;
            TmpIdx = endIndex;
            endIndex = TmpIdx + 8;
            if (endIndex > lastindex)
                return datas;
            int idx_cc = TmpIdx;
            string ss = System.Text.Encoding.ASCII.GetString(datas, TmpIdx, 8);
            ret.RecordCnt = int.Parse(ss.Trim(' '));
            TmpIdx = endIndex;
            endIndex = TmpIdx + 8;
            if (endIndex > lastindex)
                return datas;
            ret.RecordTimeSpan = int.Parse(System.Text.Encoding.ASCII.GetString(datas, TmpIdx, 8).Trim(' '));
            TmpIdx = endIndex;
            endIndex = TmpIdx + 4;
            if (endIndex > lastindex)
                return datas;
            ret.DataSignCnt = int.Parse(System.Text.Encoding.ASCII.GetString(datas, TmpIdx, 4).Trim(' '));
            TmpIdx = endIndex;
            int oneSecondDataLen = 0;
            for (int i = 0; i < ret.DataSignCnt; i++)
            {
                int offset = 0;
                offset += 16 * ret.DataSignCnt;
                offset += 80 * ret.DataSignCnt;
                offset += 8 * ret.DataSignCnt;
                offset += 8 * ret.DataSignCnt;
                offset += 8 * ret.DataSignCnt;
                offset += 8 * ret.DataSignCnt;
                offset += 8 * ret.DataSignCnt;
                offset += 80 * ret.DataSignCnt;
                oneSecondDataLen += int.Parse(System.Text.Encoding.ASCII.GetString(datas, TmpIdx + offset + i * 8, 8).Trim(' '));
                offset += 8 * ret.DataSignCnt;
                offset += 32 * ret.DataSignCnt;
            }
            ///重新写入开始时间
            string time = string.Format("{0}{1}", startTime.ToString("dd.MM.yy"), startTime.ToString("HH:mm:ss")); //string.Format("{0}.{1}.{2}{3}.{4}.{5}",);
            byte[] tt = System.Text.Encoding.ASCII.GetBytes(time);
            Array.Copy(tt, 0, datas, idx_t, 16);
            ///重新写入记录次数
            int offset0 = (int)(startTime - ret.StartTime).TotalSeconds;
            int offset2 = (int)(endTime - startTime).TotalSeconds;
            int recordCnt = offset2 / ret.RecordTimeSpan;
            byte[] cc = System.Text.Encoding.ASCII.GetBytes(recordCnt.ToString().PadRight(8,' '));
            Array.Copy(cc, 0, datas, idx_cc, 8);
            ///截取时间段的数据内容
            byte[] dataArray=new byte[oneSecondDataLen*2*offset2];
            Array.Copy(datas,ret.CaptainLength+ offset0 * oneSecondDataLen * 2,dataArray,0,dataArray.Length);
            ///定义返回数组
            byte[] rdata=new byte[ret.CaptainLength+dataArray.Length];
            ///取出头文件内容存放到返回数组中
            Array.Copy(datas,0,rdata,0,ret.CaptainLength);
            ///取出数据段存放到返回数组中
            Array.Copy(dataArray,0,rdata,ret.CaptainLength,dataArray.Length);
            using (BinaryWriter bw = new BinaryWriter(new FileStream("E:\\PengCreat.edf", FileMode.Create)))
            {
                bw.Write(rdata);
                bw.Flush();
            }
            return rdata;
        }
        /// <summary>
        /// 获取记录时间
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public DateTime getRecordTime(byte[] datas)
        {
            if (datas.Length > 176)
            {
                string RecordDate = System.Text.Encoding.ASCII.GetString(datas, 168, 8);
                string RecordTime = datas.Length > 184 ? System.Text.Encoding.ASCII.GetString(datas, 176, 8) : "00:00:00";
                string[] date = RecordDate.Split(new char[] { '.', ':' });
                int[] rts = RecordTime.Split('.').Select(t => int.Parse(t == "00" ? "0" : t)).ToArray();
                return DateTime.Parse(string.Format("{0} {1}:{2}:{3}", string.Format("{0}/{1}/{2}", date[2], date[1], date[0]), rts[0] > 59 ? 59 : rts[0], rts[1] > 59 ? 59 : rts[1], rts[2] > 59 ? 59 : rts[2]));
            }
            return DateTime.Now;
        }
        private int m_TotalCnt = 200;
        /// <summary>
        /// 累计写入最大数量，到达最大数量后即写入文件
        /// </summary>
        public int TotalCnt
        {
            set
            {
                m_TotalCnt = value;
            }
        }
        /// <summary>
        /// 写edf
        /// </summary>
        private StreamWriter sw = null;
        /// <summary>
        /// 任务终止
        /// </summary>
        private bool m_KillTask = true;
        /// <summary>
        /// 线程th
        /// </summary>
        private System.Threading.Thread th = null;
        private object m_obj = new object();
        /// <summary>
        /// 记录次数
        /// </summary>
        private int m_TotalWriteCnt = 0;
        private byte[] dataSaver = new byte[0];
        private bool m_init_ready = false;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="TempeletPath"></param>
        public void Init(string MatchKey)
        {
            string PatientID=string.Format("{0} {1} {2:dd-MM-yyyy} {3} {4}", Channel.Default.Patient.PatientNo, Channel.Default.Patient.Gender, Channel.Default.Patient.BirthDate, Channel.Default.Patient.PatientName, MatchKey);
            EDF ret = new EDF();
            ret.Channels = Channels;
            int len = 0;
            for (int i = 0; i < Channels.Count; i++)
            {
                len += Channels[i].DataLength * 2;
            }
            dataSaver = new byte[len];
            string _Path = string.Format("{0}\\{1:yyyy-MM-dd}",DefaultEdfSavePath, DateTime.Now);
            if (!Directory.Exists(_Path))
            {
                Directory.CreateDirectory(_Path);
            }
            _Path = string.Format("{0}\\{1}", _Path, string.Format("{1} {2} {3} {4}_{0:HHmmss}.edf", DateTime.Now, Channel.Default.Patient.PatientNo,Channel.Default.Patient.PatientName,Channel.Default.Patient.Gender,Channel.Default.Patient.Age));
            using (sw = new StreamWriter(_Path, false, Encoding.ASCII))
            {
                sw.AutoFlush = true;
                sw.Write(EdfWriteHead(ret, PatientID));
                sw.Flush();
                sw.Close();
                EdfPath = _Path;
            }
            bw = new BinaryWriter(new FileStream(_Path, FileMode.Append));
            Task.Factory.StartNew(() =>
            {
                System.Threading.Thread.Sleep(2000);
                m_KillTask = false;
                TaskStart();
            });
            m_init_ready = true;
        }
        private BinaryWriter bw = null;
        /// <summary>
        /// 获取edf头字符串
        /// </summary>
        /// <param name="edfChannel"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        private string EdfWriteHead(EDF edfChannel, string ID)
        {
            edfChannel.StartFlag = "0".PadRight(8, ' ');
            edfChannel.PatientNO = ID.PadRight(80, ' ');
            edfChannel.DeviceID = "Dev0001".PadRight(80, ' ');
            DateTime dt = DateTime.Now;
            edfChannel.RecordDate = string.Format("{0}.{1}.{2}", dt.Day.ToString().PadLeft(2, '0'), dt.Month.ToString().PadLeft(2, '0'), (dt.Year % 100).ToString().PadLeft(2, '0'));
            edfChannel.RecordTime = string.Format("{0}.{1}.{2}", dt.Hour.ToString().PadLeft(2, '0'), dt.Minute.ToString().PadLeft(2, '0'), dt.Second.ToString().PadLeft(2, '0'));
            edfChannel.RecordCnt = 0;
            edfChannel.CaptainLength = 256 + 256 * edfChannel.Channels.Count;
            edfChannel.Reserve = string.Format("EDF+C {0}", DeviceSNCode).PadRight(44, ' ');
            edfChannel.RecordTimeSpan = 1;
            edfChannel.DataSignCnt = edfChannel.Channels.Count;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}{1}{2}", edfChannel.StartFlag, edfChannel.PatientNO, edfChannel.DeviceID);
            sb.AppendFormat("{0}{1}{2}", edfChannel.RecordDate, edfChannel.RecordTime, edfChannel.CaptainLength.ToString().PadRight(8, ' '));
            sb.AppendFormat("{0}{1}{2}{3}", edfChannel.Reserve, edfChannel.RecordCnt.ToString().PadRight(8, ' '), edfChannel.RecordTimeSpan.ToString().PadRight(8, ' '), edfChannel.DataSignCnt.ToString().PadRight(4, ' '));
            //for (int i = 0; i < edfChannel.Channels.Count; i++)
            //{
            //    sb.AppendFormat("{0}{1}{2}", edfChannel.Channels[i].DataSignName, edfChannel.Channels[i].SensorType, edfChannel.Channels[i].Unit);
            //    sb.AppendFormat("{0}{1}{2}{3}", edfChannel.Channels[i].ViewMinValue.ToString().PadRight(8, ' '), edfChannel.Channels[i].ViewMaxValue.ToString().PadRight(8, ' '), edfChannel.Channels[i].ADMinValue.ToString().PadRight(8, ' '), edfChannel.Channels[i].ADMaxValue.ToString().PadRight(8, ' '));
            //    sb.AppendFormat("{0}{1}{2}", edfChannel.Channels[i].FilterInfo, edfChannel.Channels[i].DataLength.ToString().PadRight(8, ' '), edfChannel.Channels[i].Reserve);
            //}
            for (int i = 0; i < edfChannel.Channels.Count; i++)
            {
                sb.Append(edfChannel.Channels[i].DataSignName);
            }
            for (int i = 0; i < edfChannel.Channels.Count; i++)
            {
                sb.Append(edfChannel.Channels[i].SensorType);
            }
            for (int i = 0; i < edfChannel.Channels.Count; i++)
            {
                sb.Append(edfChannel.Channels[i].Unit);
            }
            for (int i = 0; i < edfChannel.Channels.Count; i++)
            {
                sb.Append(edfChannel.Channels[i].ViewMinValue.ToString().PadRight(8, ' '));
            }
            for (int i = 0; i < edfChannel.Channels.Count; i++)
            {
                sb.Append(edfChannel.Channels[i].ViewMaxValue.ToString().PadRight(8, ' '));
            }
            for (int i = 0; i < edfChannel.Channels.Count; i++)
            {
                sb.Append(edfChannel.Channels[i].ADMinValue.ToString().PadRight(8, ' '));
            }
            for (int i = 0; i < edfChannel.Channels.Count; i++)
            {
                sb.Append(edfChannel.Channels[i].ADMaxValue.ToString().PadRight(8, ' '));
            }
            for (int i = 0; i < edfChannel.Channels.Count; i++)
            {
                sb.Append(edfChannel.Channels[i].FilterInfo);
            }
            for (int i = 0; i < edfChannel.Channels.Count; i++)
            {
                sb.Append(edfChannel.Channels[i].DataLength.ToString().PadRight(8, ' '));
            }
            for (int i = 0; i < edfChannel.Channels.Count; i++)
            {
                sb.Append(edfChannel.Channels[i].Reserve);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 任务执行器
        /// </summary>
        private void TaskStart()
        {
            th = new System.Threading.Thread(() =>
            {
                while (!m_KillTask)
                {
                    bool ready = true;
                    for (int i = 0; i < Channels.Count; i++)
                    {
                        if (!Channels[i].IsReady)
                        {
                            ready = false;
                            break;
                        }
                    }
                    if (ready)
                    {
                        int offset = 0;
                        bool stop = true;
                        for (int i = 0; i < Channels.Count; i++)
                        {
                            stop = stop & Channels[i].DataNull;
                            int len = Channels[i].DataLength * 2;
                            byte[] charData = Channels[i].getData();
                            Array.Copy(charData, 0, dataSaver, offset, len);
                            offset += len;
                        }
                        if (!stop)
                        {
                            bw.Write(dataSaver);
                            m_TotalWriteCnt++;
                        }
                    }
                    System.Threading.Thread.Sleep(200);
                }
            });
            th.IsBackground = true;
            th.SetApartmentState(System.Threading.ApartmentState.MTA);
            th.Start();
        }
        /// <summary>
        /// 获取通道ID
        /// </summary>
        /// <param name="ChannelName"></param>
        /// <returns></returns>
        public int ConvertToChannelNumEx(string ChannelID)
        {
            ChannelTable find = GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelTable.Find(t => t.ChannelID == ChannelID);
            if (find != null)
            {
                return find.ID;
            }
            return -1;
        }
        /// <summary>
        /// 获取通道ID
        /// </summary>
        /// <param name="ChannelName"></param>
        /// <returns></returns>
        public string ConvertToChannelIDEx(string ChannelName)
        {
            ChannelTable find = GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelTable.Find(t => t.SignName.ToLower() == ChannelName.ToLower());
            //ChannelTable find =  Channel.Default.ChannelProperties.Find(t=>t.SignName.ToLower()==ChannelName.ToLower());
            if (find != null)
            {
                return find.ID.ToString();
            }
            return "";
        }
        /// <summary>
        /// 获取通道ID
        /// </summary>
        /// <param name="ChannelName"></param>
        /// <returns></returns>
        public string ConvertToChannelIDEx(int ChannelNum)
        {
            ChannelTable find = GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelTable.Find(t => t.ID == ChannelNum && !t.IsClone);
            if (find != null)
            {
                return find.ChannelID;
            }
            return "";
        }
        /// <summary>
        /// 获取通道对应名字
        /// </summary>
        /// <param name="ChannelName"></param>
        /// <returns></returns>
        public string ConvertToChannelNameEx(int ChannelID)
        {
            ChannelTable find = GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelTable.Find(t => t.ID == ChannelID);
            //ChannelTable find = Channel.Default.ChannelProperties.Find(t => t.ID == ChannelID);
            if (find != null)
            {
                return find.SignName;
            }
            return "";
        }

        /// <summary>
        /// 创建通道
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="TimeSpan"></param>
        /// <returns></returns>
        public ChannelItem ConvertToChannelItemEx(string ID, int TimeSpan)
        {
            ChannelItem item = new ChannelItem();
            ChannelTable find = GlobalSingleton.Instance.User.CurrentChannelConfig.CurrentChannelTable.Find(t => t.ChannelID == ID);
            if (find != null)
            {
                item.DataSignName = find.SignName;
                item.SensorType = find.SensorType;
                item.Unit = find.Unit;
                item.ViewMinValue = find.ViewMinValue;
                item.ViewMaxValue = find.ViewMaxValue;
                item.ADMinValue = find.ADMinValue;
                item.ADMaxValue = find.ADMaxValue;
                item.DataConvet = find.NeedConvert;
                item.ID = find.ID;
                item.UnSignData = find.UnSignData;
                if (find.ID == 9)
                    item.DataConvet = false;
            }
            else
            {
                item.Unit = item.SensorType = item.DataSignName = "";
                return item;
            }
            item.DataSignName = item.DataSignName.ToString().PadRight(16, ' ');
            item.FilterInfo = "Baseline filtering".PadRight(80, ' ');
            item.SensorType = item.SensorType.PadRight(80, ' ');
            item.Unit = item.Unit.PadRight(8, ' ');
            item.DataLength = TimeSpan;
            item.Reserve = "".PadRight(32, ' ');
            ChannelItem find2 = Channels.Find(t => t.DataSignName == item.DataSignName);
            if (find2 == null)
                Channels.Add(item);
            return item;
        }
        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            if (m_init_ready)
            {
                if (bw != null)
                {
                    try
                    {
                        m_KillTask = true;
                        th.Abort();
                        System.Threading.Thread.Sleep(1200);
                        bw.Flush();
                        bw.Close();
                        bw.Dispose();
                    }
                    catch (Exception ee)
                    {
                        Console.WriteLine(ee.Message);
                    }
                    using (sw = new StreamWriter(File.OpenWrite(EdfPath)))
                    {
                        sw.BaseStream.Seek(236, SeekOrigin.Begin);
                        sw.Write((m_TotalWriteCnt).ToString().PadRight(8, ' '));
                        sw.Flush();
                        sw.Close();
                        m_TotalWriteCnt = 0;
                    }
                }
                m_init_ready = false;
            }
        }
    }
}
