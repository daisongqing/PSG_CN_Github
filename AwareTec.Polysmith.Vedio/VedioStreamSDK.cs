using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Vedio
{
    public class VedioStreamSDK
    {
        private const int BUFFER_SIZE = 32 * 1024;                 //320k
        private const int HEADER_SIZE = 40;                       //40字节头
        private const int MAX_FILE_NAME_LEN = 500;                        //待解析的视频文件路径名最大值
        private const int PSTSPACKETTYPE = 2;                          //ps ts 封装文件
        private const int RTPPACKETTYPE = 4;                        //rtp 封装文件
        public enum INTELLIGENT_DATA_TYPE
        {
            ANALYZE_NONE = 0x00,     // none intelligent data
            ANALYZE_ITS_AID_INFO_V2 = 0x10,     // its aid information
            ANALYZE_ITS_TPS_INFO_V2 = 0x11,     // its tps information
            ANALYZE_ITS_TARGET_LIST = 0x12,     // its target list
            ANALYZE_ITS_TPS_RULE_LIST = 0x13,     // rule list
            ANALYZE_VCA_TARGET_LIST = 0x20,     // vca target list
            ANALYZE_VCA_ALERT = 0x21,     // vca alert
            ANALYZE_VCA_RULE_LIST = 0x22,     // vca rule list
            ANALYZE_VCA_EVT_INFO_LIST = 0x23,     // vca event information
            ANALYZE_FACE_IDENTIFICATION = 0x30,     // face identification
            ANALYZE_FACE_DETECT_RULE = 0x31,     // face detect rule
            INTELLIGENT_IVS_INDEX = 0x40,     // ivs index data
            ANALYZE_INTEL_UNKNOWN = 0x99      // 未知数据类型
        }

        public enum STREAM_ERROR_TYPE
        {
            ANALYZE_RTP_PACKET_NUM = 0x10,   // RTP视频包序不连续
            ANALYZE_RTP_HEADER = 0x11,   // RTP头有误，包括扩展头
            ANALYZE_MPEG_PACKET_PSH = 0x20,   // PSH有误
            ANALYZE_MPEG_PACKET_PSM = 0x21,   // PSM有误
            ANALYZE_MPEG_PACKET_PES = 0x22,   // PES有误
            ANALYZE_TS_HEADER = 0x30,   // TS头有误
            ANALYZE_MPEG_PACKET_PAT = 0x31,   // PAT有误
            ANALYZE_MPEG_PACKET_PMT = 0x32,   // PMT有误
            ANALYZE_REDUNDANT = 0x40,   // 码流里有冗余数据，或者包长度不对
            ANALYZE_STREAM_HEADER = 0x50,   // 创建句柄时的码流头有误
            ANALYZE_ERROR_UNKNOWN = 0x99,   // 码流有未知错误
        }
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct ANA_ERROR_INFOR
        {
            public uint nErrorType;  // 错误类型
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 40, ArraySubType = UnmanagedType.U1)]
            public byte[] pHeaderData; // 若头有误，送出实际码流头，默认40字节
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.U4)]
            public uint[] Reserved; // reserved 6

        }
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct PACKET_INFO
        {
            public uint dwTimeStamp;  //time stamp
            public uint nYear;        //year
            public uint nMonth;       //month
            public uint nDay;         //day
            public uint nHour;        //hour
            public uint nMinute;      //minute
            public uint nSecond;	     //second
            public uint nPacketType;  //packet type
            public uint dwPacketSize; //packet size
            public IntPtr pPacketBuffer;//packet buffer
        }
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct PACKET_INFO_EX
        {
            public ushort uWidth;         //width
            public ushort uHeight;        //height
            public uint dwTimeStamp;    //lower time stamp
            public uint dwTimeStampHigh;//higher time stamp 
            public uint nYear;	        //year
            public uint nMonth;         //month
            public uint nDay;           //day
            public uint nHour;          //hour
            public uint nMinute;        //minute
            public uint nSecond;        //second
            public uint nMillisecond;   //millisecond
            public uint dwFrameNum;     //frame num
            public uint dwFrameRate;    //frame rate,当帧率小于0时，0x80000002:表示1/2帧率，同理可推0x80000010为1/16帧率
            public uint dwFlag;         //flag E帧标记
            public uint dwFilePos;      //file pos//////////////// 1：表示3gp文件解析完成标记
            public uint nPacketType;    //packet type
            public uint dwPacketSize;   //packet size
            public IntPtr pPacketBuffer;  //packet buffer
            public uint dwEncrypted;    //if Encrypted. !0 for yes, 0 for no
            public uint dwPacketType;   // 打包方式
            public uint dwEncryptArith; // 加密算法
            public uint dwEncryptRound; // 加密轮数
            public uint dwKeyLen;       // 密钥长度
            public uint dwEncryptType;  // 加密类型
            /// <summary>
            /// reserved[0] 当为私有帧时，表示私有数据类型；当为音频帧时，表示音频的通道号；  
            /// 当为视频帧时，对于非RTP码流，表示当前视频通道号，比如E0到EF;对于RTP码流，表示对应SSRC值。
            /// reserved[1] 当为私有帧时，表示私有裸数据地址高位。
            /// reserved[2] 当为私有帧时，表示私有裸数据地址低位。
            /// reserved[3] 当为私有帧时，表示私有裸数据长度。
            /// reserved[4] 当为私有帧时，表示私有帧的时间间隔/时间戳；
            /// 当为视频帧时，如果是I帧，    Reserved[4] |= 0x00000001，标记为svc码流，              Reserved[4] &= 0xFFFFFFFE，标记非svc码流；
            /// reserved[5] 当为视频帧时，如果是I帧，    Reserved[5] |= 0x00000001，标记为smart264/smart265码流，Reserved[5] &= 0xFFFFFFFE，标记非smart264/smart265码流；
            /// 如果是P帧，    Reserved[5] |= 0x00000001，标记为深P帧，                Reserved[5] &= 0xFFFFFFFE，标记非深P帧; 
            /// 如果是I/P/B帧，Reserved[5] |= 0x00000002，标记为小鹰眼码流，           Reserved[5] &= 0xFFFFFFFD，标记非小鹰眼码流;   
            /// 当为私有帧时，表示当前私有帧所属视频的通道号，比如E0到EF
            /// </summary>
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.U4)]
            public uint[] Reserved;
        }
        #region 动态库引用

        /// <summary>
        /// 以流或者文件流形式创建分析句柄
        /// </summary>
        /// <param name="dwSize"> [IN] 内部解析缓存大小（默认2M）</param>
        /// <param name="pHeader">[IN] hik40字节hik头，如果为null表示为无头模式</param>
        /// <returns>生成的解析句柄：非空指针，其他值表示错误</returns>
        [DllImport("pvedio\\AnalyzeData.dll")]
        public static extern IntPtr HIKANA_CreateStreamEx(uint dwSize, byte[] pHeader);

        /// <summary>
        /// 释放解析句柄资源
        /// </summary>
        /// <param name="hHandle">[IN] 解析句柄</param>
        [DllImport("pvedio\\AnalyzeData.dll")]
        public static extern void HIKANA_Destroy(IntPtr hHandle);


        /// <summary>
        /// 输入需要分析的数据，3gp文件不支持调用
        /// </summary>
        /// <param name="hHandle">分析句柄</param>
        /// <param name="pBuffer">送入数据地址</param>
        /// <param name="dwSize">送入数据大小</param>
        /// <returns>成功返回1，其他返回值表示错误</returns>
        [DllImport("pvedio\\AnalyzeData.dll")]
        public static extern int HIKANA_InputData(IntPtr hHandle, byte[] pBuffer, uint dwSize);

        /// <summary>
        /// 获取分析得到的一帧数据
        /// </summary>
        /// <param name="hHandle">分析句柄</param>
        /// <param name="pstPacket">帧信息结构体</param>
        /// <returns>成功得到一帧数据返回0，其他返回值表示错误，如果为数据不够需要继续input数据，可以调用HIKANA_GetLastErrorH获取错误类型</returns>
        [DllImport("pvedio\\AnalyzeData.dll")]
        public static extern int HIKANA_GetOnePacketEx(IntPtr hHandle, ref PACKET_INFO_EX pstPacket);


        /// <summary>
        /// 清除解析缓存里面的数据
        /// </summary>
        /// <param name="hHandle">解析句柄</param>
        /// <returns>成功返回0，其他返回值表示错误</returns>
        [DllImport("pvedio\\AnalyzeData.dll")]
        public static extern int HIKANA_ClearBuffer(IntPtr hHandle);

        /// <summary>
        /// 获取解析缓存里面的剩余数据
        /// </summary>
        /// <param name="hHandle">解析句柄</param>
        /// <param name="pData">外部缓存指针</param>
        /// <param name="pdwSize">外部缓存大小送入，输出值为获取得到的解析缓存里面的剩余数据大小</param>
        /// <returns>成功返回0，其他返回值表示错误，可以调用HIKANA_GetLastErrorH获取错误类型；最多能获取外部缓存大小的数据</returns>
        [DllImport("pvedio\\AnalyzeData.dll")]
        public static extern int HIKANA_GetRemainData(IntPtr hHandle, byte[] pData, ref uint pdwSize);

        /// <summary>
        /// 设置分析帧类型
        /// </summary>
        /// <param name="hHandle">分析句柄</param>
        /// <param name="nType">只对svc码流有效 (在头文件AnalyzeDataDefine.h定义的0~3解析类型)</param>
        /// <returns>成功返回0，其他返回值表示错误，可以调用HIKANA_GetLastErrorH获取错误类型</returns>
        [DllImport("pvedio\\AnalyzeData.dll")]
        public static extern int HIKANA_SetAnalyzeFrameType(IntPtr hHandle, uint nType);

        /// <summary>
        /// 设置输出帧是否为带封装或者裸数据
        /// </summary>
        /// <param name="hHandle">分析句柄</param>
        /// <param name="nType">输出帧类型(ANALYZE_ENCAPSULATED_DATA: 带封装，ANALYZE_RAW_DATA：裸数据)</param>
        /// <returns>成功返回0，其他返回值表示错误可以调用HIKANA_GetLastErrorH获取错误类型；对于rtp、3gp封装只能得到裸数据</returns>
        [DllImport("pvedio\\AnalyzeData.dll")]
        public static extern int HIKANA_SetOutputPacketType(IntPtr hHandle, uint nType);
        /// <summary>
        /// 回调函数
        /// </summary>
        /// <param name="pErrorInfor">回调错误类型</param>
        /// <param name="pUser">用户信息</param>
        public delegate void pErroeInforCBDelegate(ref ANA_ERROR_INFOR pErrorInfor, IntPtr pUser);
        /// <summary>
        /// 码流错误信息回调
        /// </summary>
        /// <param name="hHandle">分析句柄</param>
        /// <param name="pErroeInforCB">回调函数</param>
        /// <param name="pUser">用户信息</param>
        /// <returns>成功返回0，其他返回值表示错误可以调用HIKANA_GetLastErrorH获取错误类型</returns>
        [DllImport("pvedio\\AnalyzeData.dll")]
        public static extern int HIKANA_RegistStreamInforCB(IntPtr hHandle, pErroeInforCBDelegate pErroeInforCB, int pUser);

        /// <summary>
        /// 获取上一个错误值
        /// </summary>
        /// <param name="hHandle">分析句柄</param>
        /// <returns>上一个错误的错误类型</returns>
        [DllImport("pvedio\\AnalyzeData.dll")]
        public static extern uint HIKANA_GetLastErrorH(IntPtr hHandle);

        /// <summary>
        /// 获取当前版本
        /// </summary>
        /// <returns> 返回int型值，不同2进制位代表是否基线、编译日期、版本等信息(|baseline(2)|build  year(5:0~31)|month(4:1~12)|date(5:1~31) |major(4)|minor(4)|modify(4)|test(4)|)</returns>
        [DllImport("pvedio\\AnalyzeData.dll")]
        public static extern int HIKANA_GetVersion();

        /// <summary>
        /// 3gp文件通过文件路径创建句柄
        /// </summary>
        /// <param name="dwSize">设定内部解析缓存大小（默认2M）</param>
        /// <param name="szFilePath">文件路径</param>
        /// <returns>生成的解析句柄：非空指针，其他值表示错误；该接口只支持3gp封装视频文件解析，其他封装暂不支持调用该接口创建句柄</returns>
        [DllImport("pvedio\\AnalyzeData.dll")]
        public static extern IntPtr HIKANA_CreateHandleByPath(uint dwSize, string szFilePath);
        #endregion
    }
}
