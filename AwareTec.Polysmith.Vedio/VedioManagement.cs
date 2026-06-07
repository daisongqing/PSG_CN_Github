using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Vedio
{
    /// <summary>
    /// 视频监控管理类
    /// </summary>
    public class VedioManagement
    {
        private uint iLastErr = 0;
        private Int32 m_lUserID = -1;
        private Int32 m_lFindHandle = -1;
        private Int32 m_lDownHandle = -1;
        private bool m_bInitSDK = false;
        private bool m_bTalk = false;
        private Int32 m_lRealHandle = -1;
        private int lVoiceComHandle = -1;
        private bool m_bRecord = false;
        /// <summary>
        /// 播放倍率
        /// </summary>
        private int m_FastRate = 1;
        private int m_fastCnt = 0;
        CHCNetSDK.REALDATACALLBACK RealData = null;
        public CHCNetSDK.NET_DVR_PTZPOS m_struPtzCfg;
        #region 事件定义
        public delegate void RealDataCallBackDelegate(Int32 lRealHandle, UInt32 dwDataType, IntPtr pBuffer, UInt32 dwBufSize, IntPtr pUser);
        /// <summary>
        /// 取得实时视频数据时触发
        /// </summary>
        public event RealDataCallBackDelegate RealDataCallBackHandler;
        public delegate void VoiceDataCallBackDelegate(int lVoiceComHandle, IntPtr pRecvDataBuffer, uint dwBufSize, byte byAudioFlag, System.IntPtr pUser);
        /// <summary>
        /// 开启语音通话时触发
        /// </summary>
        public event VoiceDataCallBackDelegate VoiceDataCallBackHandler;
        #endregion
        private static VedioManagement m_Default = null;

        public static VedioManagement Default
        {
            get
            {
                return m_Default ?? (m_Default = new VedioManagement());
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public VedioManagement()
        {
            m_bInitSDK = CHCNetSDK.NET_DVR_Init();
        }
        /// <summary>
        /// 取得实时视频数据时回调
        /// </summary>
        private void RealDataCallBack(Int32 lRealHandle, UInt32 dwDataType, IntPtr pBuffer, UInt32 dwBufSize, IntPtr pUser)
        {
            if (RealDataCallBackHandler != null)
                RealDataCallBackHandler.Invoke(lRealHandle, dwDataType, pBuffer, dwBufSize, pUser);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lVoiceComHandle"></param>
        /// <param name="pRecvDataBuffer"></param>
        /// <param name="dwBufSize"></param>
        /// <param name="byAudioFlag">0-PC采集音频 1-设备音频</param>
        /// <param name="pUser"></param>
        private void VoiceDataCallBack(int lVoiceComHandle, IntPtr pRecvDataBuffer, uint dwBufSize, byte byAudioFlag, System.IntPtr pUser)
        {
            if (VoiceDataCallBackHandler != null)
                VoiceDataCallBackHandler.Invoke(lVoiceComHandle, pRecvDataBuffer, dwBufSize, byAudioFlag, pUser);
        }
        /// <summary>
        /// 开始实施预览
        /// </summary>
        /// <param name="wHandle"></param>
        /// <returns></returns>
        public ReturnResult StartRealViewPlay(IntPtr wHandle)
        {
            ReturnResult ret = new ReturnResult();
            if (m_lUserID < 0)
            {
                ret.OutMsg = "Please login the device firstly";
                return ret;
            }
            if (m_lRealHandle < 0)
            {
                CHCNetSDK.NET_DVR_PREVIEWINFO lpPreviewInfo = new CHCNetSDK.NET_DVR_PREVIEWINFO();
                lpPreviewInfo.hPlayWnd = wHandle;//预览窗口
                lpPreviewInfo.lChannel = 1;//预te览的设备通道
                lpPreviewInfo.dwStreamType = 1;//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
                lpPreviewInfo.dwLinkMode = 0;//连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
                lpPreviewInfo.bBlocked = true; //0- 非阻塞取流，1- 阻塞取流
                lpPreviewInfo.dwDisplayBufNum = 1; //播放库播放缓冲区最大缓冲帧数
                lpPreviewInfo.byProtoType = 0;
                lpPreviewInfo.byPreviewMode = 0;

                if (RealData == null)
                {
                    RealData = new CHCNetSDK.REALDATACALLBACK(RealDataCallBack);//预览实时流回调函数
                }

                IntPtr pUser = new IntPtr();//用户数据
                //NET_DVR_SetDVRConfig
                //打开预览 Start live view 
                m_lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V40(m_lUserID, ref lpPreviewInfo, null/*RealData*/, pUser);
                if (m_lRealHandle < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    ret.OutMsg = "NET_DVR_RealPlay_V40 failed, error code= " + iLastErr; //预览失败，输出错误号
                    return ret;
                }
                CHCNetSDK.NET_DVR_PTZPreset(m_lRealHandle, CHCNetSDK.GOTO_PRESET, 2);
            }
            else
            {
                //停止预览 Stop live view 
                if (!CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    ret.OutMsg = "NET_DVR_StopRealPlay failed, error code= " + iLastErr;
                    return ret;
                }
                m_lRealHandle = -1;

            }
            ret.IsOK = true;
            return ret;
        }
        #region 回放
        public void RefreshProgress()
        {
            if (m_recordStart && reviewPort > -1)
            {
                int nStamp = CHCNetSDK.PlayM4_GetPlayedTimeEx(reviewPort);
                if (ReviewRunningHandler != null)
                {
                    ReviewRunningHandler.Invoke(nStamp, m_totalTimes);
                }
            }
        }
        public delegate void ReviewRunningDelegate(int currentTimes, uint TotalsTime);
        /// <summary>
        /// 每播放一帧触发
        /// </summary>
        public event ReviewRunningDelegate ReviewRunningHandler;
        bool g_bRefDone = false;
        int reviewPort = -1;
        private bool m_recordStart = false;
        private void play(uint nport, uint userid)
        {
            g_bRefDone = true;
        }
        private void displayCBFun(int nPort, byte[] pBuf, int nSize, int nWidth, int nHeight, int nStamp, int nType, int nReceved)
        {
            if (ReviewRunningHandler != null)
            {
                ReviewRunningHandler.Invoke(nStamp, m_totalTimes);
            }
        }
        private uint m_totalTimes = 0;
        /// <summary>
        /// 防止被GC回收，定义成全局静态变量
        /// </summary>
        private static CHCNetSDK.DecCBFunDelegate decCBFun = null;
        private static CHCNetSDK.FileRefDoneCB recallcb = null;
        private static CHCNetSDK.DisplayCBFun display = null;
        private string m_currentPath = "";
        private IntPtr m_wHand = IntPtr.Zero;
        /// <summary>
        /// 开始回放
        /// </summary>
        /// <returns></returns>
        public ReturnResult StartReviewRecord(string recordPath, IntPtr wHand, int StartmillSeconds = 0)
        {
            ReturnResult ret = new ReturnResult();
            if (m_currentPath == recordPath)
            {
                ret.IsOK = false;
                return ret;
            }
            Trace.WriteLine(string.Format("m_recordStart={0}", m_recordStart));
            if (m_recordStart)
            {
                Trace.WriteLine("关闭当前正在播放的视频:停止播放");
                CHCNetSDK.PlayM4_Stop(reviewPort);
                Trace.WriteLine("关闭当前正在播放的视频:关闭声音");
                CHCNetSDK.PlayM4_StopSound();
                Trace.WriteLine("关闭当前正在播放的视频:关闭文件");
                CHCNetSDK.PlayM4_CloseFile(reviewPort);
                Trace.WriteLine("关闭当前正在播放的视频:释放播放资源");
                CHCNetSDK.PlayM4_FreePort(reviewPort);
                reviewPort = -1;
                m_recordStart = false;
                g_bRefDone = false;
                m_currentPath = "";
                Trace.WriteLine("关闭当前正在播放的视频：完成");
            }
            DateTime dt = DateTime.Now;
            int nPort = -1;
            bool bFlag = false;
            // 获取播放库端口号
            CHCNetSDK.PlayM4_GetPort(ref nPort);
            System.Diagnostics.Trace.WriteLine(string.Format("PlayM4_GetPort耗时：{0}ms", (DateTime.Now - dt).TotalMilliseconds));
            string refIndex = "";
            bool needref = true;
            if (refIndex != "")
            {
                byte[] refidx = refIndex.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(t => byte.Parse(t)).ToArray();
                if (CHCNetSDK.PlayM4_SetRefValue(nPort, Marshal.AllocHGlobal(refidx.Length), (ushort)refidx.Length))
                {
                    uint err = CHCNetSDK.PlayM4_GetLastError(nPort);
                    if (err == 0)
                    {
                        g_bRefDone = true;
                        needref = false;
                    }
                }
            }
            if (needref)
            {
                recallcb = new CHCNetSDK.FileRefDoneCB(play);
                // 创建文件索引
                CHCNetSDK.PlayM4_SetFileRefCallBack(nPort, recallcb, 0);
                GC.KeepAlive(recallcb);
            }
            System.Diagnostics.Trace.WriteLine(string.Format("写文件索引回调耗时：{0}ms", (DateTime.Now - dt).TotalMilliseconds));
            //打开待播放的文件
            bFlag = CHCNetSDK.PlayM4_OpenFile(nPort, recordPath);
            if (!bFlag)
            {
                ret.OutMsg = "文件被损坏";
                return ret;
            }
            System.Diagnostics.Trace.WriteLine(string.Format("PlayM4_OpenFile调耗时：{0}ms", (DateTime.Now - dt).TotalMilliseconds));
            ///解析帧回调
            //decCBFun = new CHCNetSDK.DecCBFunDelegate(DecCBFunction);
            //CHCNetSDK.PlayM4_SetDecCallBackExMend(nPort, decCBFun, null, 0, 0);
            //GC.KeepAlive(decCBFun);
            ///获取总的视频时长
            m_totalTimes = CHCNetSDK.PlayM4_GetFileTime(nPort) * 1000;
            System.Diagnostics.Trace.WriteLine(string.Format("PlayM4_GetFileTime调耗时：{0}ms", (DateTime.Now - dt).TotalMilliseconds));
            //开始播放文件
            CHCNetSDK.PlayM4_Play(nPort, wHand);
            System.Diagnostics.Trace.WriteLine(string.Format("PlayM4_Play调耗时：{0}ms", (DateTime.Now - dt).TotalMilliseconds));
            m_wHand = wHand;
            if (m_SoundEnable)
            {
                CHCNetSDK.PlayM4_PlaySound(nPort);
            }
            if (m_volume >= 0)
            {
                CHCNetSDK.PlayM4_SetVolume(nPort, (ushort)m_volume);
            }
            m_currentPath = recordPath;
            m_recordStart = true;
            System.Diagnostics.Trace.WriteLine(string.Format("PlayM4_PlaySound调耗时：{0}ms", (DateTime.Now - dt).TotalMilliseconds));
            if (needref)
            {
                DateTime dt2 = DateTime.Now;
                reviewPort = nPort;
                if (m_FastRate != 0 || m_FastRate != 1)
                {
                    bool on = m_FastRate > 0;
                    int cnt = (int)Math.Log(Math.Abs(m_FastRate), 2);
                    m_FastRate = 0;
                    m_fastCnt = 0;
                    for (int i = 0; i < cnt; i++)
                        ReviewPlayFast(on);
                }
                while (true)
                {
                    if (g_bRefDone == false)
                    {
                        System.Threading.Thread.Sleep(2);
                        continue;
                    }
                    else//索引创建成功
                    {

                        //把这个也重置为0，否则调整倍率后，关闭视频再打开，视频倍率不会默认为1倍
                        //System.Threading.Thread.Sleep(200);
                        //IntPtr buf = IntPtr.Zero;
                        //ushort size = 0;
                        //if (!CHCNetSDK.PlayM4_GetRefValue(nPort, buf, ref size))
                        //{
                        //    uint err = CHCNetSDK.PlayM4_GetLastError(nPort);
                        //    if (err > 0)
                        //    {
                        //        buf = Marshal.AllocHGlobal(size);
                        //        if (!CHCNetSDK.PlayM4_GetRefValue(nPort, buf, ref size))
                        //        {
                        //            err = CHCNetSDK.PlayM4_GetLastError(nPort);
                        //            if (err > 0)
                        //            {

                        //            }
                        //        }
                        //    }

                        //}
                        //byte[] indexbuf = new byte[size];
                        //Marshal.Copy(buf, indexbuf, 0, size);
                        //string des = string.Join(" ", indexbuf);
                        //Console.WriteLine(des);
                        //Marshal.FreeHGlobal(buf);
                        break;
                    }
                }
                System.Diagnostics.Trace.WriteLine(string.Format("建文件索引耗时：{0}ms", (DateTime.Now - dt2).TotalMilliseconds));
            }
            if (StartmillSeconds > 0)
            {
                DateTime dt3 = DateTime.Now;
                setReViewCurrentTimes(StartmillSeconds);
                System.Diagnostics.Trace.WriteLine(string.Format("指定到时间播放耗时：{0}ms", (DateTime.Now - dt3).TotalMilliseconds));
            }
            ret.IsOK = true;
            return ret;
        }

        private void DecCBFunction(int nPort, IntPtr pBuf, int nSize, ref CHCNetSDK.FRAME_INFO pFrameInfo, int nReserved1, int nReserved2)
        {
            if (ReviewRunningHandler != null)
            {
                ReviewRunningHandler.Invoke(pFrameInfo.nStamp, m_totalTimes);
            }
        }
        private void endFileCallBack(int nPort, IntPtr pUser)
        {
            if (ReviewRunningHandler != null)
            {
                ReviewRunningHandler.Invoke((int)m_totalTimes, m_totalTimes);
            }
            try
            {
                //停止播放
                CHCNetSDK.PlayM4_Stop(nPort);

                //停止播放声音
                CHCNetSDK.PlayM4_StopSound();

                //关闭文件
                CHCNetSDK.PlayM4_CloseFile(nPort);

                CHCNetSDK.PlayM4_FreePort(nPort);

            }
            catch { }
            finally
            {
                m_recordStart = false;
                g_bRefDone = false;
                reviewPort = -1;
            }
        }
        /// <summary>
        /// 指定到当前时间播放
        /// </summary>
        /// <param name="Millsecends">ms</param>
        /// <returns></returns>
        public ReturnResult setReViewCurrentTimes(int Millsecends)
        {
            ReturnResult ret = new ReturnResult();
            if (m_recordStart)
            {
                if (Millsecends < 0)
                    Millsecends = 0;
                ret.IsOK = CHCNetSDK.PlayM4_SetPlayedTimeEx(reviewPort, (uint)Millsecends);
                uint nError = CHCNetSDK.PlayM4_GetLastError(0);
                if (nError > 0)
                {

                }
            }
            return ret;
        }
        /// <summary>
        /// 暂停播放
        /// </summary>
        /// <param name="Pause"></param>
        /// <returns></returns>
        public ReturnResult ReviewPause(bool Pause)
        {
            ReturnResult ret = new ReturnResult();
            if (reviewPort >= 0)
            {
                ret.IsOK = CHCNetSDK.PlayM4_Pause(reviewPort, Pause ? 1 : 0);
                if (ret.IsOK)
                {
                    return ret;
                }
                uint nError = CHCNetSDK.PlayM4_GetLastError(0);
                if (nError > 0)
                {
                    if (!Pause)
                    {
                        ret.IsOK = CHCNetSDK.PlayM4_Play(reviewPort, m_wHand);
                        if (m_FastRate != 0 || m_FastRate != 1)
                        {
                            bool on = m_FastRate > 0;
                            int cnt = (int)Math.Log(Math.Abs(m_FastRate), 2);
                            m_FastRate = 0;
                            m_fastCnt = 0;
                            for (int i = 0; i < cnt; i++)
                                ReviewPlayFast(on);
                        }
                    }
                    else
                    {
                        nError = CHCNetSDK.PlayM4_GetLastError(0);
                        if (nError == 2)
                        {
                            ret.IsOK = CHCNetSDK.PlayM4_Pause(reviewPort, Pause ? 1 : 0);
                        }
                    }
                }
            }
            return ret;
        }
        /// <summary>
        /// 结束播放
        /// </summary>
        /// <returns></returns>
        public ReturnResult ReviewStop()
        {
            ReturnResult ret = new ReturnResult();
            if (reviewPort >= 0)
            {
                ret.IsOK = CHCNetSDK.PlayM4_Stop(reviewPort);
                if (ret.IsOK)
                {
                    try
                    {
                        CHCNetSDK.PlayM4_StopSound();
                        ///关闭文件
                        CHCNetSDK.PlayM4_CloseFile(reviewPort);
                        //释放播放库端口号
                        CHCNetSDK.PlayM4_FreePort(reviewPort);
                    }
                    catch { }
                    reviewPort = -1;
                    m_recordStart = false;
                    g_bRefDone = false;
                    m_currentPath = "";
                    ret.IsOK = true;
                }
            }
            return ret;
        }
        private int m_volume = -1;
        /// <summary>
        /// 播放的声音设置
        /// </summary>
        /// <param name="volume">声音大小 0-0xffff</param>
        /// <returns></returns>
        public ReturnResult ReviewSetVolume(ushort volume)
        {
            ReturnResult ret = new ReturnResult();
            if (reviewPort >= 0)
            {
                ret.IsOK = CHCNetSDK.PlayM4_SetVolume(reviewPort, volume);
                if (ret.IsOK)
                    m_volume = volume;
            }
            return ret;
        }
        private bool m_SoundEnable = true;
        /// <summary>
        /// 放音/禁音
        /// </summary>
        /// <param name="on">true-放音 false-禁音</param>
        /// <returns></returns>
        public ReturnResult ReviewPlaySound(bool on)
        {
            ReturnResult ret = new ReturnResult();
            if (reviewPort >= 0)
            {
                ret.IsOK = on ? CHCNetSDK.PlayM4_PlaySound(reviewPort) : CHCNetSDK.PlayM4_StopSound();
                if (ret.IsOK)
                {
                    m_SoundEnable = on;
                }
            }
            return ret;
        }

        /// <summary>
        /// 加速播放
        /// </summary>
        /// <param name="on">true-加速 false-减速</param>
        /// <returns></returns>
        public ReturnResult ReviewPlayFast(bool on)
        {
            ReturnResult ret = new ReturnResult();
            if (reviewPort >= 0)
            {
                if ((m_fastCnt == 4 && on) || (m_fastCnt == -4 && !on))
                    return ret;
                ret.IsOK = on ? CHCNetSDK.PlayM4_Fast(reviewPort) : CHCNetSDK.PlayM4_Slow(reviewPort);
                if (ret.IsOK)
                {
                    if ((m_fastCnt == 4 && !on))
                        m_fastCnt = 3;
                    else if (m_fastCnt == -4 && on)
                        m_fastCnt = -3;
                    if (m_fastCnt == 0 && m_FastRate == 0)
                        m_FastRate += (on ? 2 : -2);
                    else if (m_fastCnt == 0 && m_FastRate != 0)
                    {
                        m_FastRate = 0;
                        return ret;
                    }
                    else
                    {
                        //调整视频播放速率
                        if (m_FastRate > 0)
                        {
                            m_FastRate = (on ? m_FastRate * 2 : m_FastRate / 2);
                            if (m_FastRate == 1)
                                m_FastRate = 0;
                        }
                        else
                        {
                            m_FastRate = (on ? m_FastRate / 2 : m_FastRate * 2);
                            if (m_FastRate == -1)
                                m_FastRate = 0;
                        }
                    }
                    m_fastCnt += on ? 1 : -1;
                }
            }
            return ret;
        }

        /// <summary>
        /// 单帧播放
        /// </summary>
        /// <param name="on">true-前进 false-后退</param>
        /// <returns></returns>
        public ReturnResult ReviewPlayOneByOne(bool on)
        {
            ReturnResult ret = new ReturnResult();
            if (reviewPort >= 0)
            {
                ret.IsOK = on ? CHCNetSDK.PlayM4_OneByOne(reviewPort) : CHCNetSDK.PlayM4_OneByOneBack(reviewPort);

            }
            return ret;
        }
        /// <summary>
        /// 放大指定区域
        /// </summary>
        /// <param name="on">true-加速 false-减速</param>
        /// <returns></returns>
        public ReturnResult ReviewZoom(ref Rectangle rect, IntPtr wHand)
        {
            ReturnResult ret = new ReturnResult();
            if (reviewPort >= 0)
            {
                ret.IsOK = CHCNetSDK.PlayM4_SetDisplayRegion(reviewPort, 0, ref rect, wHand, true);
                CHCNetSDK.PlayM4_RefreshPlayEx(reviewPort, 0);

                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                if (iLastErr > 0)
                {
                    Console.WriteLine(iLastErr);
                }
            }
            return ret;
        }
        public ReturnResult ReviewZoom(bool on)
        {
            ReturnResult ret = new ReturnResult();
            if (m_lUserID >= 0)
            {
                ret.IsOK = CHCNetSDK.NET_DVR_PTZControl(m_lRealHandle, (uint)(on ? 11 : 12), 0);
            }
            return ret;
        }
        /// <summary>
        /// 获取像素大小
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public ReturnResult getPictrueSize(ref int width, ref int height)
        {
            ReturnResult ret = new ReturnResult();
            if (reviewPort >= 0)
            {
                ret.IsOK = CHCNetSDK.PlayM4_GetPictureSize(reviewPort, ref width, ref height);
            }
            return ret;
        }
        #endregion
        /// <summary>
        /// 默认300M
        /// </summary>
        private ulong m_MaxRecodFileLenght = 300 * 1024 * 1024;
        /// <summary>
        /// 最大切片文件大小（M）
        /// </summary>
        public int MaxRecodFileLenght
        {
            set
            {
                m_MaxRecodFileLenght = (ulong)value * 1024 * 1024;
            }
        }
        private bool m_NotSplitRecordFile = false;
        /// <summary>
        /// 文件不切片标志  true-不切片  false-切片
        /// </summary>
        public bool NotSplitRecordFile
        {
            set
            {
                m_NotSplitRecordFile = value;
            }
            get
            {
                return m_NotSplitRecordFile;
            }
        }
        /// <summary>
        /// 开始记录视频文件
        /// </summary>
        /// <param name="recordPath"></param>
        /// <param name="guid"></param>
        public ReturnResult StartRecord(string recordPath, bool all = false)
        {
            ReturnResult ret = new ReturnResult();
            CHCNetSDK.NET_DVR_LOCAL_GENERAL_CFG info = new CHCNetSDK.NET_DVR_LOCAL_GENERAL_CFG();
            bool success = CHCNetSDK.NET_DVR_GetSDKLocalCfg(CHCNetSDK.NET_SDK_LOCAL_CFG_TYPE.NET_DVR_LOCAL_CFG_TYPE_GENERAL, ref info);
            info.byNotSplitRecordFile = (byte)(m_NotSplitRecordFile ? 1 : 0);///0-启用切片 1-不启用
            info.i64FileSize = m_MaxRecodFileLenght;///切片大小300M  这里的单位是byte
            success = CHCNetSDK.NET_DVR_SetSDKLocalCfg(CHCNetSDK.NET_SDK_LOCAL_CFG_TYPE.NET_DVR_LOCAL_CFG_TYPE_GENERAL, ref info);
            if (success)
            {
                ///设备校时
                CHCNetSDK.NET_DVR_TIME time = new CHCNetSDK.NET_DVR_TIME();
                DateTime dt = DateTime.Now;
                time.dwYear = (uint)dt.Year;
                time.dwMonth = (uint)dt.Month;
                time.dwDay = (uint)dt.Day;
                time.dwHour = (uint)dt.Hour;
                time.dwMinute = (uint)dt.Minute;
                time.dwSecond = (uint)dt.Second;
                try
                {
                    int nSizeOfTime = Marshal.SizeOf(time);
                    IntPtr intPtr = Marshal.AllocHGlobal(nSizeOfTime);
                    Marshal.StructureToPtr(time, intPtr, true);
                    CHCNetSDK.NET_DVR_SetDVRConfig(m_lUserID, CHCNetSDK.NET_DVR_SET_TIMECFG, 1, intPtr, (uint)nSizeOfTime);
                    Marshal.FreeHGlobal(intPtr);
                }
                catch { }
            }
            string strextend = Path.GetExtension(recordPath);
            string directorypath = "";
            //录像保存路径和文件名 the path and file name to save
            string sVideoFileName;
            if (string.IsNullOrEmpty(strextend))
            {
                directorypath = recordPath;
                sVideoFileName = Path.Combine(recordPath, string.Format("{0}.mp4", Path.GetFileName(directorypath)));
            }
            else
            {
                sVideoFileName = recordPath;
                directorypath = Path.GetDirectoryName(recordPath);
            }

            if (!Directory.Exists(directorypath))
            {
                Directory.CreateDirectory(directorypath);
            }
            if (all)
            {
                //强制I帧 Make a I frame
                CHCNetSDK.NET_DVR_MakeKeyFrame(m_lUserID, 1);
                if (File.Exists(sVideoFileName))
                {
                    sVideoFileName = Path.Combine(directorypath, string.Format("{1:ddhhmmss}_{0}.mp4", Path.GetFileName(directorypath), DateTime.Now));
                }
                //开始录像 Start recording
                if (!CHCNetSDK.NET_DVR_SaveRealData(m_lRealHandle, sVideoFileName))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    ret.OutMsg = "NET_DVR_SaveRealData failed, error code= " + iLastErr;
                }
            }
            else
            {
                Task.Factory.StartNew(() =>
                {
                    int totalCnt = 120;
                    int frameCnt = 1;
                record:
                    sVideoFileName = Path.Combine(directorypath, string.Format("{0}.mp4", frameCnt));
                    int cnt = -1;
                    //强制I帧 Make a I frame
                    CHCNetSDK.NET_DVR_MakeKeyFrame(m_lUserID, 1);
                    //开始录像 Start recording
                    if (!CHCNetSDK.NET_DVR_SaveRealData(m_lRealHandle, sVideoFileName))
                    {
                        iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                        ret.OutMsg = "NET_DVR_SaveRealData failed, error code= " + iLastErr;
                    }
                    m_bRecord = true;
                    while (cnt <= totalCnt && m_lRealHandle >= 0)
                    {
                        if (m_lRealHandle < 0)
                        {
                            return;
                        }
                        System.Threading.Thread.Sleep(1000);
                        cnt++;
                    }
                    CHCNetSDK.NET_DVR_StopSaveRealData(m_lRealHandle);
                    m_bRecord = false;
                    frameCnt += 4;
                    goto record;
                });
            }
            m_bRecord = true;
            ret.IsOK = true;
            return ret;
        }
        /// <summary>
        /// 云台运动控制 0-上 1-下 2-左 3-右
        /// </summary>
        /// <param name="directionTyp"></param>
        /// <returns></returns>
        public ReturnResult DeviceOperate(int directionTyp, bool on = true)
        {
            ReturnResult ret = new ReturnResult();
            switch (directionTyp)
            {

                case 0:
                    ret.IsOK = CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.TILT_UP, (uint)(on ? 0 : 1), 4);
                    break;
                case 1:
                    ret.IsOK = CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.TILT_DOWN, (uint)(on ? 0 : 1), 4);
                    break;
                case 2:
                    ret.IsOK = CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.PAN_LEFT, (uint)(on ? 0 : 1), 4);
                    break;
                case 3:
                    ret.IsOK = CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.PAN_RIGHT, (uint)(on ? 0 : 1), 4);
                    break;
            }
            return ret;
        }
        private static CHCNetSDK.VOICEDATACALLBACKV30 VoiceData = null;

        /// <summary>
        /// 语音对讲控制
        /// </summary>
        /// <returns></returns>
        public ReturnResult VioceTalk()
        {
            ReturnResult ret = new ReturnResult();
            if (m_bTalk == false)
            {
                //开始语音对讲 Start two-way talk
                VoiceData = new CHCNetSDK.VOICEDATACALLBACKV30(VoiceDataCallBack);//预览实时流回调函数

                lVoiceComHandle = CHCNetSDK.NET_DVR_StartVoiceCom_V30(m_lUserID, 1, true, VoiceData, IntPtr.Zero);
                //bNeedCBNoEncData [in]需要回调的语音数据类型：0- 编码后的语音数据，1- 编码前的PCM原始数据
                GC.KeepAlive(VoiceData);
                if (lVoiceComHandle < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    ret.OutMsg = "NET_DVR_StartVoiceCom_V30 failed, error code= " + iLastErr;
                    return ret;
                }
                else
                {
                    m_bTalk = true;
                }
            }
            else
            {
                //停止语音对讲 Stop two-way talk
                if (!CHCNetSDK.NET_DVR_StopVoiceCom(lVoiceComHandle))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    ret.OutMsg = "NET_DVR_StopVoiceCom failed, error code= " + iLastErr;
                    return ret;
                }
                else
                {
                    m_bTalk = false;
                }
            }
            ret.IsOK = true;
            return ret;
        }
        /// <summary>
        /// 设备登录
        /// </summary>
        /// <param name="IP">待连接的视频设备的IP地址</param>
        /// <param name="Port">待连接的视频设备的端口号</param>
        /// <param name="UserID">用户名</param>
        /// <param name="Password">密码</param>
        /// <returns></returns>
        public ReturnResult Login(string IP, int Port, string UserID, string Password)
        {
            ReturnResult ret = new ReturnResult();
            if (m_lUserID < 0)
            {
                if (!m_bInitSDK)
                    m_bInitSDK = CHCNetSDK.NET_DVR_Init();
                string DVRIPAddress = IP; //设备IP地址或者域名
                int DVRPortNumber = Port;//设备服务端口号
                string DVRUserName = UserID;//设备登录用户名
                string DVRPassword = Password;//设备登录密码

                CHCNetSDK.NET_DVR_DEVICEINFO_V30 DeviceInfo = new CHCNetSDK.NET_DVR_DEVICEINFO_V30();

                //登录设备 Login the device
                m_lUserID = CHCNetSDK.NET_DVR_Login_V30(DVRIPAddress, DVRPortNumber, DVRUserName, DVRPassword, ref DeviceInfo);
                if (m_lUserID < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    ret.IsOK = false;
                    ret.OutMsg = "NET_DVR_Login_V30 failed, error code= " + iLastErr; //登录失败，输出错误号
                    return ret;
                }
                else
                {
                    //登录成功
                    ret.IsOK = true;
                }
            }
            else
                ret.IsOK = true;
            return ret;
        }
        /// <summary>
        /// 设备注销
        /// </summary>
        /// <returns></returns>
        public ReturnResult Dispose()
        {
            ReturnResult ret = new ReturnResult();
            if (m_lUserID >= 0)
            {
                if (m_lRealHandle >= 0)
                {
                    if (m_bRecord)
                    {
                        CHCNetSDK.NET_DVR_StopSaveRealData(m_lRealHandle);
                        m_bRecord = false;
                    }
                    CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle);
                    m_lRealHandle = -1;
                }
                if (!CHCNetSDK.NET_DVR_Logout(m_lUserID))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    ret.OutMsg = "NET_DVR_Logout failed, error code= " + iLastErr;
                    return ret;
                }
                if (m_bInitSDK)
                {
                    CHCNetSDK.NET_DVR_Cleanup();
                    m_bInitSDK = false;
                }
                m_lUserID = -1;
                ret.IsOK = true;
            }
            else if (m_recordStart)
            {
                CHCNetSDK.PlayM4_Stop(reviewPort);
                CHCNetSDK.PlayM4_StopSound();
                CHCNetSDK.PlayM4_CloseFile(reviewPort);
                CHCNetSDK.PlayM4_FreePort(reviewPort);
                reviewPort = -1;
                m_recordStart = false;
                g_bRefDone = false;
                ret.IsOK = true;
                m_currentPath = "";
                m_FastRate = 1;
                m_fastCnt = 0;
                m_SoundEnable = true;
                m_volume = -1;
            }
            return ret;
        }
        /// <summary>
        /// 获取文件的开始播放/结束播放的时间
        /// </summary>
        /// <param name="vedioPath"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public bool getStartAndEndTime(string vedioPath, ref DateTime start, ref DateTime end)
        {
            uint buffLength = 20 * 1024;
            byte[] pReadBuf = new byte[buffLength];
            try
            {
                long offset = 0;
                bool startini = false;
                bool complete = false;
                long len = 0;
                int framecnt = 0;
                using (FileStream fsRead = new FileStream(vedioPath, FileMode.Open))
                {
                    byte[] head = new byte[40];
                    fsRead.Read(head, 0, 40);
                    IntPtr m_hNewHandle = Vedio.VedioStreamSDK.HIKANA_CreateStreamEx(buffLength * buffLength, head);///创建分析句柄
                    while (!complete)
                    {
                        int readCount = fsRead.Read(pReadBuf, 0, pReadBuf.Length);///读取原加密视频数据
                        if (readCount == 0)
                        {
                            break;
                        }
                        else
                        {
                            offset += readCount;
                            fsRead.Seek(offset, SeekOrigin.Begin);
                        }
                        Vedio.VedioStreamSDK.HIKANA_InputData(m_hNewHandle, pReadBuf, (uint)readCount);///原数据推送到分析模块
                        Vedio.VedioStreamSDK.HIKANA_SetOutputPacketType(m_hNewHandle, 0);// 设置输出帧为裸数据(1:不带封装；0:为默认,带封装)
                        int rettt = 0;
                        Vedio.VedioStreamSDK.PACKET_INFO_EX stPacket = new VedioStreamSDK.PACKET_INFO_EX();///创建帧信息体用于接收解析的数据
                        while (rettt == 0)
                        {
                            rettt = Vedio.VedioStreamSDK.HIKANA_GetOnePacketEx(m_hNewHandle, ref stPacket);///获取解析数据
                            if (rettt == 0)
                            {
                                len += stPacket.dwPacketSize;
                                if (stPacket.nYear > 1 && stPacket.nYear != 0xFFFFFFFF && stPacket.dwFrameNum != 0xFFFFFFFF)
                                {
                                    if (!startini)
                                    {
                                        framecnt++;
                                        if (framecnt == 1)
                                        {
                                            start = new DateTime((int)stPacket.nYear, (int)stPacket.nMonth, (int)stPacket.nDay, (int)stPacket.nHour, (int)stPacket.nMinute, (int)stPacket.nSecond, (int)stPacket.nMillisecond);
                                        }
                                        else if (framecnt == stPacket.dwFrameRate*2)
                                        {
                                            startini = true;
                                            offset = fsRead.Length - len + 40;
                                            uint last = (uint)pReadBuf.Length;
                                            Vedio.VedioStreamSDK.HIKANA_GetRemainData(m_hNewHandle, pReadBuf, ref last);
                                            fsRead.Seek(offset, SeekOrigin.Begin);
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        try
                                        {
                                            end = new DateTime((int)stPacket.nYear, (int)stPacket.nMonth, (int)stPacket.nDay, (int)stPacket.nHour, (int)stPacket.nMinute, (int)stPacket.nSecond, (int)stPacket.nMillisecond);
                                        }
                                        catch { }
                                    }
                                }
                            }
                        }
                    }
                    Vedio.VedioStreamSDK.HIKANA_ClearBuffer(m_hNewHandle);
                    Vedio.VedioStreamSDK.HIKANA_Destroy(m_hNewHandle);
                }
            }
            catch (Exception ee)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="Port"></param>
        /// <param name="UserID"></param>
        /// <param name="Password"></param>
        /// <param name="savePath"></param>
        /// <returns></returns>
        public ReturnResult LoadDownVideos(string IP, int Port, string UserID, string Password,string savePath)
        {
            ReturnResult ret = Login(IP, Port, UserID, Password);
            if (ret.IsOK)
            {
                DateTime dateTimeEnd = DateTime.Now;
                DateTime dateTimeStart = dateTimeEnd.AddDays(-2);
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
                //开始录像文件查找 Start to search video files 
                m_lFindHandle = CHCNetSDK.NET_DVR_FindFile_V40(m_lUserID, ref fileinfo);
                if (m_lFindHandle < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    ret.IsOK = false;
                    ret.OutMsg = "NET_DVR_FindFile_V40 failed, error code= " + iLastErr; //预览失败，输出错误号
                    return ret;
                }
                else
                {
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
                    CHCNetSDK.NET_DVR_FindClose_V30(m_lFindHandle);
                    string vname = Path.GetFileName(savePath);
                    for(int i = 0; i < vedionames.Count; i++)
                    {
                        if (m_lDownHandle >= 0)
                        {
                            ret.OutMsg="Downloading, please stop firstly!";//正在下载，请先停止下载
                            return ret;
                        }
                        string sVideoFileName;  //录像文件保存路径和文件名 the path and file name to save      
                        ///路径必须是英文名称
                        sVideoFileName = string.Format("D:\\{1}{2}.mp4",savePath, vname, i == 0 ? "" : string.Format("_{0}", i));

                        //按文件名下载 Download by file name
                        m_lDownHandle = CHCNetSDK.NET_DVR_GetFileByName(m_lUserID, vedionames[i], sVideoFileName);
                        if (m_lDownHandle < 0)
                        {
                            iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                            ret.OutMsg+= "NET_DVR_GetFileByName failed, error code= \r\n" + iLastErr;
                            continue;
                        }

                        uint iOutValue = 0;
                        if (!CHCNetSDK.NET_DVR_PlayBackControl_V40(m_lDownHandle, CHCNetSDK.NET_DVR_PLAYSTART, IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue))
                        {
                            iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                            ret.OutMsg += "NET_DVR_PLAYSTART failed, error code= " + iLastErr; //下载控制失败，输出错误号
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
                            Trace.WriteLine(string.Format("视频文件{1}下载进度{0}%",iPos, vedionames[i]));
                            System.Threading.Thread.Sleep(500);
                        }
                    }
                }
            }
            return ret;
        }
        /// <summary>
        /// 获取下载状态
        /// </summary>
        public bool LoadDownStatus { private set; get; }
        /// <summary>
        /// 获取播放速率
        /// </summary>
        public int FastRate
        {
            get
            {
                return m_FastRate;
            }
        }
    }
    /// <summary>
    /// 视频回放
    /// </summary>
    public class VedioRePlayFactory
    {
        private uint iLastErr = 0;
        private bool g_bRefDone = false;
        private int reviewPort = -1;
        private bool m_recordStart = false;
        private uint m_totalTimes = 0;
        private static CHCNetSDK.FileRefDoneCB recallcb = null;
        private string m_currentPath = "";
        private IntPtr m_wHand = IntPtr.Zero;
        /// <summary>
        /// 播放倍率
        /// </summary>
        private int m_FastRate = 1;
        private int m_fastCnt = 0;
        /// <summary>
        /// 更新实时播放进度
        /// </summary>
        public void RefreshProgress()
        {
            if (m_recordStart && reviewPort > -1)
            {
                int nStamp = CHCNetSDK.PlayM4_GetPlayedTimeEx(reviewPort);
                if (ReviewRunningHandler != null)
                {
                    ReviewRunningHandler.Invoke(nStamp, m_totalTimes);
                }
            }
        }
        public delegate void ReviewRunningDelegate(int currentTimes, uint TotalsTime);
        /// <summary>
        /// 每播放一帧触发
        /// </summary>
        public event ReviewRunningDelegate ReviewRunningHandler;

        private void play(uint nport, uint userid)
        {
            g_bRefDone = true;
        }
        /// <summary>
        /// 开始回放
        /// </summary>
        /// <returns></returns>
        public ReturnResult StartReviewRecord(string recordPath, IntPtr wHand, int StartmillSeconds = 0)
        {
            ReturnResult ret = new ReturnResult();
            if (m_currentPath == recordPath)
            {
                ret.IsOK = false;
                return ret;
            }
            Trace.WriteLine(string.Format("m_recordStart={0}", m_recordStart));
            if (m_recordStart)
            {
                Trace.WriteLine("关闭当前正在播放的视频:停止播放");
                CHCNetSDK.PlayM4_Stop(reviewPort);
                Trace.WriteLine("关闭当前正在播放的视频:关闭声音");
                CHCNetSDK.PlayM4_StopSound();
                Trace.WriteLine("关闭当前正在播放的视频:关闭文件");
                CHCNetSDK.PlayM4_CloseFile(reviewPort);
                Trace.WriteLine("关闭当前正在播放的视频:释放播放资源");
                CHCNetSDK.PlayM4_FreePort(reviewPort);
                reviewPort = -1;
                m_recordStart = false;
                g_bRefDone = false;
                m_currentPath = "";
                Trace.WriteLine("关闭当前正在播放的视频：完成");
            }
            int nPort = -1;
            bool bFlag = false;
            // 获取播放库端口号
            CHCNetSDK.PlayM4_GetPort(ref nPort);
            string refIndex = "";
            bool needref = true;
            if (refIndex != "")
            {
                byte[] refidx = refIndex.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(t => byte.Parse(t)).ToArray();
                if (CHCNetSDK.PlayM4_SetRefValue(nPort, Marshal.AllocHGlobal(refidx.Length), (ushort)refidx.Length))
                {
                    uint err = CHCNetSDK.PlayM4_GetLastError(nPort);
                    if (err == 0)
                    {
                        g_bRefDone = true;
                        needref = false;
                    }
                }
            }
            if (needref)
            {
                recallcb = new CHCNetSDK.FileRefDoneCB(play);
                // 创建文件索引
                CHCNetSDK.PlayM4_SetFileRefCallBack(nPort, recallcb, 0);
                GC.KeepAlive(recallcb);
            }
            //打开待播放的文件
            bFlag = CHCNetSDK.PlayM4_OpenFile(nPort, recordPath);
            if (!bFlag)
            {
                ret.OutMsg = "文件被损坏";
                return ret;
            }
            ///获取总的视频时长
            m_totalTimes = CHCNetSDK.PlayM4_GetFileTime(nPort) * 1000;
            //开始播放文件
            CHCNetSDK.PlayM4_Play(nPort, wHand);
            Trace.WriteLine("播放一个视频");
            m_wHand = wHand;
            CHCNetSDK.PlayM4_PlaySound(nPort);
            m_currentPath = recordPath;
            m_recordStart = true;
            if (needref)
            {
                while (true)
                {
                    if (g_bRefDone == false)
                    {
                        System.Threading.Thread.Sleep(2);
                        continue;
                    }
                    else//索引创建成功
                    {
                        reviewPort = nPort;
                        m_FastRate = 0;
                        //把这个也重置为0，否则调整倍率后，关闭视频再打开，视频倍率不会默认为1倍
                        m_fastCnt = 0;
                        System.Threading.Thread.Sleep(200);
                        break;
                    }
                }
            }
            if (StartmillSeconds > 0)
            {
                setReViewCurrentTimes(StartmillSeconds);
            }
            ret.IsOK = true;
            return ret;
        }
        /// <summary>
        /// 停止播放
        /// </summary>
        /// <returns></returns>
        public ReturnResult StopReviewRecord()
        {
            ReturnResult ret = new ReturnResult();
            if (m_recordStart)
            {
                CHCNetSDK.PlayM4_Stop(reviewPort);
                CHCNetSDK.PlayM4_StopSound();
                CHCNetSDK.PlayM4_CloseFile(reviewPort);
                CHCNetSDK.PlayM4_FreePort(reviewPort);
                reviewPort = -1;
                m_recordStart = false;
                g_bRefDone = false;
                m_currentPath = "";
                ret.IsOK = true;
            }
            return ret;
        }
        /// <summary>
        /// 指定到当前时间播放
        /// </summary>
        /// <param name="Millsecends">ms</param>
        /// <returns></returns>
        public ReturnResult setReViewCurrentTimes(int Millsecends)
        {
            ReturnResult ret = new ReturnResult();
            if (m_recordStart)
            {
                if (Millsecends < 0)
                    Millsecends = 0;
                ret.IsOK = CHCNetSDK.PlayM4_SetPlayedTimeEx(reviewPort, (uint)Millsecends);
                uint nError = CHCNetSDK.PlayM4_GetLastError(0);
                if (nError > 0)
                {

                }
            }
            return ret;
        }
        /// <summary>
        /// 暂停播放
        /// </summary>
        /// <param name="Pause"></param>
        /// <returns></returns>
        public ReturnResult ReviewPause(bool Pause)
        {
            ReturnResult ret = new ReturnResult();
            if (reviewPort >= 0)
            {
                ret.IsOK = CHCNetSDK.PlayM4_Pause(reviewPort, Pause ? 1 : 0);
                if (ret.IsOK)
                {
                    return ret;
                }
                uint nError = CHCNetSDK.PlayM4_GetLastError(0);
                if (nError > 0)
                {
                    if (!Pause)
                    {
                        ret.IsOK = CHCNetSDK.PlayM4_Play(reviewPort, m_wHand);
                        m_FastRate = 0;
                    }
                    else
                    {
                        nError = CHCNetSDK.PlayM4_GetLastError(0);
                        if (nError == 2)
                        {
                            ret.IsOK = CHCNetSDK.PlayM4_Pause(reviewPort, Pause ? 1 : 0);
                        }
                    }
                }
            }
            return ret;
        }
        /// <summary>
        /// 结束播放
        /// </summary>
        /// <returns></returns>
        public ReturnResult ReviewStop()
        {
            ReturnResult ret = new ReturnResult();
            if (reviewPort >= 0)
            {
                ret.IsOK = CHCNetSDK.PlayM4_Stop(reviewPort);
                if (ret.IsOK)
                {
                    try
                    {
                        CHCNetSDK.PlayM4_StopSound();
                        ///关闭文件
                        CHCNetSDK.PlayM4_CloseFile(reviewPort);
                        //释放播放库端口号
                        CHCNetSDK.PlayM4_FreePort(reviewPort);
                    }
                    catch { }
                    m_recordStart = false;
                    g_bRefDone = false;
                    reviewPort = -1;
                }
            }
            return ret;
        }
        /// <summary>
        /// 播放的声音设置
        /// </summary>
        /// <param name="volume">声音大小 0-0xffff</param>
        /// <returns></returns>
        public ReturnResult ReviewSetVolume(ushort volume)
        {
            ReturnResult ret = new ReturnResult();
            if (reviewPort >= 0)
            {
                ret.IsOK = CHCNetSDK.PlayM4_SetVolume(reviewPort, volume);
            }
            return ret;
        }
        /// <summary>
        /// 放音/禁音
        /// </summary>
        /// <param name="on">true-放音 false-禁音</param>
        /// <returns></returns>
        public ReturnResult ReviewPlaySound(bool on)
        {
            ReturnResult ret = new ReturnResult();
            if (reviewPort >= 0)
            {
                ret.IsOK = on ? CHCNetSDK.PlayM4_PlaySound(reviewPort) : CHCNetSDK.PlayM4_StopSound();
            }
            return ret;
        }

        /// <summary>
        /// 加速播放
        /// </summary>
        /// <param name="on">true-加速 false-减速</param>
        /// <returns></returns>
        public ReturnResult ReviewPlayFast(bool on)
        {
            ReturnResult ret = new ReturnResult();
            if (reviewPort >= 0)
            {
                if ((m_fastCnt == 4 && on) || (m_fastCnt == -4 && !on))
                    return ret;
                ret.IsOK = on ? CHCNetSDK.PlayM4_Fast(reviewPort) : CHCNetSDK.PlayM4_Slow(reviewPort);
                if (ret.IsOK)
                {
                    if ((m_fastCnt == 4 && !on))
                        m_fastCnt = 3;
                    else if (m_fastCnt == -4 && on)
                        m_fastCnt = -3;
                    if (m_fastCnt == 0 && m_FastRate == 0)
                        m_FastRate += (on ? 2 : -2);
                    else if (m_fastCnt == 0 && m_FastRate != 0)
                    {
                        m_FastRate = 0;
                        return ret;
                    }
                    else
                    {
                        //调整视频播放速率
                        if (m_FastRate > 0)
                        {
                            m_FastRate = (on ? m_FastRate * 2 : m_FastRate / 2);
                            if (m_FastRate == 1)
                                m_FastRate = 0;
                        }
                        else
                        {
                            m_FastRate = (on ? m_FastRate / 2 : m_FastRate * 2);
                            if (m_FastRate == -1)
                                m_FastRate = 0;
                        }
                    }
                    m_fastCnt += on ? 1 : -1;
                }
            }
            return ret;
        }

        /// <summary>
        /// 单帧播放
        /// </summary>
        /// <param name="on">true-前进 false-后退</param>
        /// <returns></returns>
        public ReturnResult ReviewPlayOneByOne(bool on)
        {
            ReturnResult ret = new ReturnResult();
            if (reviewPort >= 0)
            {
                ret.IsOK = on ? CHCNetSDK.PlayM4_OneByOne(reviewPort) : CHCNetSDK.PlayM4_OneByOneBack(reviewPort);

            }
            return ret;
        }
        /// <summary>
        /// 放大指定区域
        /// </summary>
        /// <param name="on">true-加速 false-减速</param>
        /// <returns></returns>
        public ReturnResult ReviewZoom(ref Rectangle rect, IntPtr wHand)
        {
            ReturnResult ret = new ReturnResult();
            if (reviewPort >= 0)
            {
                ret.IsOK = CHCNetSDK.PlayM4_SetDisplayRegion(reviewPort, 0, ref rect, wHand, true);
                CHCNetSDK.PlayM4_RefreshPlayEx(reviewPort, 0);

                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                if (iLastErr > 0)
                {
                    Console.WriteLine(iLastErr);
                }
            }
            return ret;
        }
        /// <summary>
        /// 获取像素大小
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public ReturnResult getPictrueSize(ref int width, ref int height)
        {
            ReturnResult ret = new ReturnResult();
            if (reviewPort >= 0)
            {
                ret.IsOK = CHCNetSDK.PlayM4_GetPictureSize(reviewPort, ref width, ref height);
            }
            return ret;
        }
        /// <summary>
        /// 设备注销
        /// </summary>
        /// <returns></returns>
        public ReturnResult Dispose()
        {
            ReturnResult ret = new ReturnResult();
            if (m_recordStart)
            {
                CHCNetSDK.PlayM4_Stop(reviewPort);
                CHCNetSDK.PlayM4_StopSound();
                CHCNetSDK.PlayM4_CloseFile(reviewPort);
                CHCNetSDK.PlayM4_FreePort(reviewPort);
                reviewPort = -1;
                m_recordStart = false;
                g_bRefDone = false;
                ret.IsOK = true;
                m_currentPath = "";
            }
            return ret;
        }
    }
}
