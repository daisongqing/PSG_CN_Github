using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace pSystem.UI.Register
{
    /// <summary>
    /// 加密狗操作类
    /// </summary>
    public class USBDogOperation
    {
        #region API
        // ctlCode definition for S4Control
        static public uint S4_LED_UP = 0x00000004;  // LED up
        static public uint S4_LED_DOWN = 0x00000008;  // LED down
        static public uint S4_LED_WINK = 0x00000028;  // LED wink
        static public uint S4_GET_DEVICE_TYPE = 0x00000025;	//get device type
        static public uint S4_GET_SERIAL_NUMBER = 0x00000026;	//get device serial
        static public uint S4_GET_VM_TYPE = 0x00000027;  // get VM type
        static public uint S4_GET_DEVICE_USABLE_SPACE = 0x00000029;  // get total space
        static public uint S4_SET_DEVICE_ID = 0x0000002a;  // set device ID
        static public uint S4_GET_CURRENT_TIME = 0x0000002d;  //获取当前时间
        // device type definition 
        static public uint S4_LOCAL_DEVICE = 0x00;		// local device 
        static public uint S4_MASTER_DEVICE = 0x80;		// net master device
        static public uint S4_SLAVE_DEVICE = 0xc0;		// net slave device
        // vm type definiton 
        static public uint S4_VM_51 = 0x00;		// VM51
        static public uint S4_VM_251_BINARY = 0x01;		// VM251 binary mode
        static public uint S4_VM_251_SOURCE = 0x02;		// VM251 source mode
        // PIN type definition 
        static public uint S4_USER_PIN = 0x000000a1;		// user PIN
        static public uint S4_DEV_PIN = 0x000000a2;		// dev PIN
        static public uint S4_AUTHEN_PIN = 0x000000a3;		// autheticate Key
        // file type definition 
        static public uint S4_RSA_PUBLIC_FILE = 0x00000006;		// RSA public file
        static public uint S4_RSA_PRIVATE_FILE = 0x00000007;		// RSA private file 
        static public uint S4_EXE_FILE = 0x00000008;		// VM file
        static public uint S4_DATA_FILE = 0x00000009;		// data file
        // dwFlag definition for S4WriteFile
        static public uint S4_CREATE_NEW = 0x000000a5;		// create new file
        static public uint S4_UPDATE_FILE = 0x000000a6;		// update file
        static public uint S4_KEY_GEN_RSA_FILE = 0x000000a7;		// produce RSA key pair
        static public uint S4_SET_LICENCES = 0x000000a8;		// set the license number for modle,available for net device only
        static public uint S4_CREATE_ROOT_DIR = 0x000000ab;		// create root directory, available for empty device only
        static public uint S4_CREATE_SUB_DIR = 0x000000ac;		// create child directory
        static public uint S4_CREATE_MODULE = 0x000000ad;		// create modle, available for net device only
        // the three parameters below must be bitwise-inclusive-or with S4_CREATE_NEW, only for executive file
        static public uint S4_FILE_READ_WRITE = 0x00000000;      // can be read and written in executive file,default
        static public uint S4_FILE_EXECUTE_ONLY = 0x00000100;      // can NOT be read or written in executive file
        static public uint S4_CREATE_PEDDING_FILE = 0x00002000;		// create padding file
        /* return value*/
        static public uint S4_SUCCESS = 0x00000000;		// succeed
        static public uint S4_UNPOWERED = 0x00000001;
        static public uint S4_INVALID_PARAMETER = 0x00000002;
        static public uint S4_COMM_ERROR = 0x00000003;
        static public uint S4_PROTOCOL_ERROR = 0x00000004;
        static public uint S4_DEVICE_BUSY = 0x00000005;
        static public uint S4_KEY_REMOVED = 0x00000006;
        static public uint S4_INSUFFICIENT_BUFFER = 0x00000011;
        static public uint S4_NO_LIST = 0x00000012;
        static public uint S4_GENERAL_ERROR = 0x00000013;
        static public uint S4_UNSUPPORTED = 0x00000014;
        static public uint S4_DEVICE_TYPE_MISMATCH = 0x00000020;
        static public uint S4_FILE_SIZE_CROSS_7FFF = 0x00000021;
        static public uint S4_DEVICE_UNSUPPORTED = 0x00006a81;
        static public uint S4_FILE_NOT_FOUND = 0x00006a82;
        static public uint S4_INSUFFICIENT_SECU_STATE = 0x00006982;
        static public uint S4_DIRECTORY_EXIST = 0x00006901;
        static public uint S4_FILE_EXIST = 0x00006a80;
        static public uint S4_INSUFFICIENT_SPACE = 0x00006a84;
        static public uint S4_OFFSET_BEYOND = 0x00006B00;
        static public uint S4_PIN_BLOCK = 0x00006983;
        static public uint S4_FILE_TYPE_MISMATCH = 0x00006981;
        static public uint S4_CRYPTO_KEY_NOT_FOUND = 0x00009403;
        static public uint S4_APPLICATION_TEMP_BLOCK = 0x00006985;
        static public uint S4_APPLICATION_PERM_BLOCK = 0x00009303;
        static public int S4_DATA_BUFFER_LENGTH_ERROR = 0x00006700;
        static public uint S4_CODE_RANGE = 0x00010000;
        static public uint S4_CODE_RESERVED_INST = 0x00020000;
        static public uint S4_CODE_RAM_RANGE = 0x00040000;
        static public uint S4_CODE_BIT_RANGE = 0x00080000;
        static public uint S4_CODE_SFR_RANGE = 0x00100000;
        static public uint S4_CODE_XRAM_RANGE = 0x00200000;
        static public uint S4_ERROR_UNKNOWN = 0xffffffff;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SENSE4_CONTEXT
        {
            public int dwIndex;		//device index
            public int dwVersion;		//version		
            public int hLock;			//device handle
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public byte[] reserve;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 56)]
            public byte[] bAtr;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] bID;
            public uint dwAtrLen;
        }

        //Assume that Sense4user.dll in , if not, modify the lines below
        [DllImport(@"Sense4.dll")]
        private static extern uint S4Enum([MarshalAs(UnmanagedType.LPArray), Out] SENSE4_CONTEXT[] s4_context, ref uint size);
        [DllImport(@"Sense4.dll")]
        private static extern uint S4Open(ref SENSE4_CONTEXT s4_context);
        [DllImport(@"Sense4.dll")]
        private static extern uint S4Close(ref SENSE4_CONTEXT s4_context);
        [DllImport(@"Sense4.dll")]
        private static extern uint S4Control(ref SENSE4_CONTEXT s4Ctx, uint ctlCode, byte[] inBuff,
            uint inBuffLen, byte[] outBuff, uint outBuffLen, ref uint BytesReturned);
        [DllImport(@"Sense4.dll")]
        private static extern uint S4CreateDir(ref SENSE4_CONTEXT s4Ctx, string DirID, uint DirSize, uint Flags);
        [DllImport(@"Sense4.dll")]
        private static extern uint S4ChangeDir(ref SENSE4_CONTEXT s4Ctx, string Path);
        [DllImport(@"Sense4.dll")]
        private static extern uint S4EraseDir(ref SENSE4_CONTEXT s4Ctx, string DirID);
        [DllImport(@"Sense4.dll")]
        private static extern uint S4VerifyPin(ref SENSE4_CONTEXT s4Ctx, byte[] Pin, uint PinLen, uint PinType);
        [DllImport(@"Sense4.dll")]
        private static extern uint S4ChangePin(ref SENSE4_CONTEXT s4Ctx, byte[] OldPin, uint OldPinLen,
            byte[] NewPin, uint NewPinLen, uint PinType);
        [DllImport(@"Sense4.dll")]
        private static extern uint S4WriteFile(ref SENSE4_CONTEXT s4Ctx, string FileID, uint Offset,
            byte[] Buffer, uint BufferSize, uint FileSize, ref uint BytesWritten, uint Flags,
            uint FileType);
        [DllImport(@"Sense4.dll")]
        private static extern uint S4Execute(ref SENSE4_CONTEXT s4Ctx, string FileID, byte[] InBuffer,
            uint InbufferSize, byte[] OutBuffer, uint OutBufferSize, ref uint BytesReturned);
        #endregion
        #region 私有成员
        private struct rinfo
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] rflag;

            //  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public uint rlen;
        }
        private struct winfo
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] wflag;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
            public byte[] wbuff;
        }
        /// <summary>
        /// 检查相关
        /// </summary>
        /// <param name="psw"></param>
        /// <returns></returns>
        private string Check(string psw, ref SENSE4_CONTEXT[] si)
        {
            uint size = 0;
            uint ret = S4Enum(null, ref size);
            if (ret != 0x11)
            {
                return "Dongle not found";
            }
            si = new SENSE4_CONTEXT[size / Marshal.SizeOf(typeof(SENSE4_CONTEXT))];
            ret = S4Enum(si, ref size);
            if (ret != 0x00)
            {
                return "Get dongle failure:" + ret.ToString();
            }

            //打开第一个设备
            ret = S4Open(ref si[0]);
            if (ret != 0x00)
            {
                return "Open dongle failure:" + ret.ToString();
            }

            //切换到根目录
            ret = S4ChangeDir(ref si[0], "\\");
            if (ret != 0x00)
            {
                S4Close(ref si[0]);
                return "Switch to the root directory failed:" + ret.ToString();
            }
            byte[] Pin = Encoding.Default.GetBytes(psw);
            ret = S4VerifyPin(ref si[0], Pin, (uint)Pin.Length, S4_USER_PIN);
            if (ret != 0x00)
            {
                S4Close(ref si[0]);
                return "Check user password failed:" + ret.ToString();
            }
            return "";
        }
        #endregion
        #region 共有成员
        private string _UserPwd = "91532186";
        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPwd { set { _UserPwd = value; } get { return _UserPwd; } }
        public string _DevicePwd = "951753237538658985461458";
        /// <summary>
        /// 设备序列号
        /// </summary>
        public string DevicePwd { set { _DevicePwd = value; } get { return _DevicePwd; } }
        /// <summary>
        /// 检查加密狗是否存在
        /// </summary>
        /// <returns></returns>
        public bool checkDeviceExist()
        {
            bool IsExist = false;
            uint size = 0;
            uint ret = S4Enum(null, ref size);
            if (ret != 0x11)
            {
                return false;
            }
            if (size != 0)
            {
                IsExist = true;
            }
            else
            {
                IsExist = false;
            }
            return IsExist;

        }
        /// <summary>
        /// 获取加密狗到期时间
        /// </summary>
        /// <returns></returns>
        public DateTime getDateTime()
        {
            uint size = 0;
            uint ret = S4Enum(null, ref size);
            DateTime dt = DateTime.Now;
            SENSE4_CONTEXT[] si = new SENSE4_CONTEXT[size / Marshal.SizeOf(typeof(SENSE4_CONTEXT))];
            ret = S4Enum(si, ref size);
            ret = S4Open(ref si[0]);
            byte[] inBuffer = new byte[512];
            inBuffer[0] = 0x04;
            byte[] outBuffer = new byte[256];
            outBuffer[0] = 0xff;
            outBuffer[1] = 0x01;
            outBuffer[2] = 0x02;
            uint BytesReturned = 0;
            ret = S4Control(ref si[0], S4_GET_CURRENT_TIME, inBuffer, 1, outBuffer, 256, ref BytesReturned);
            string ls = "";
            byte[] byts = new byte[4];
            List<int> dtlist = new List<int>();
            for (int j = 0; j < 8; j++)
            {
                Array.Copy(outBuffer, 4 * j, byts, 0, 4);
                dtlist.Add(BitConverter.ToInt32(byts, 0));
            }
            ls = string.Format("{0}-{1}-{2} {3}:{4}:{5}", dtlist[5] + 1900, dtlist[4] + 1, dtlist[3], dtlist[2], dtlist[1], dtlist[0]);
            dt = Convert.ToDateTime(ls);
            ret = S4Close(ref si[0]);
            return dt;
        }
        /// <summary>
        /// 返回空值表示成功
        /// </summary>
        /// <param name="readLenght"></param>
        /// <param name="txt"></param>
        /// <returns></returns>
        public string ReadDog(uint readLenght, ref string txt)
        {
            uint ret;
            byte[] inBuffer = new byte[247];
            byte[] outBuffer = new byte[200];
            byte[] outBuffer1 = new byte[247];
            uint BytesReturned = 0;
            String buff = "";
            String outbuff = "";
            int k = 0, i = 0;
            SENSE4_CONTEXT[] si = null;
            if (Check(UserPwd, ref si) == "")
            {
                rinfo readinfo = new rinfo();
                buff = "1";
                readinfo.rflag = System.Text.Encoding.Default.GetBytes(buff);
                buff = readLenght.ToString();
                readinfo.rlen = (uint)Convert.ToInt16(buff);
                //要读取的数据长度
                for (k = 0; k < (readinfo.rlen / 200); k++)
                {
                    //读标志
                    inBuffer[0] = readinfo.rflag[0];
                    inBuffer[1] = (byte)k;
                    inBuffer[2] = 200;
                    ret = S4Execute(ref si[0], "0001", inBuffer, 3, outBuffer, 256, ref BytesReturned);
                    if (ret != 0x00)
                    {
                        S4Close(ref si[0]);
                        return "Lock the file failed execution:" + ret;
                    }
                    else
                    {
                        outbuff = outbuff + System.Text.Encoding.Default.GetString(outBuffer);
                    }
                }
                inBuffer[0] = readinfo.rflag[0];
                inBuffer[1] = (byte)k;
                inBuffer[2] = (byte)(readinfo.rlen % 200);
                ret = S4Execute(ref si[0], "0001", inBuffer, 3, outBuffer, 256, ref BytesReturned);
                if (ret != 0x00)
                {
                    S4Close(ref si[0]);
                    return "Lock the file failed execution:" + ret;
                }
                for (i = 0; i < BytesReturned; i++)
                {
                    outBuffer1[i] = outBuffer[i];
                }

                buff = System.Text.Encoding.Default.GetString(outBuffer1);
                outbuff = outbuff + buff;
                txt = outbuff;
                S4Close(ref si[0]);
            }
            return "";
        }
        /// <summary>
        /// 返回值等于空字符串表示成功
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public string WriteDog(string txt)
        {

            uint ret;
            byte[] inBuffer = new byte[247];
            uint BytesReturned = 0;
            String buff;
            int k = 0, i = 0;
            SENSE4_CONTEXT[] si = null;
            if (Check(UserPwd, ref si) == "")
            {
                winfo writeinfo = new winfo();
                buff = "0";
                writeinfo.wflag = System.Text.Encoding.Default.GetBytes(buff);
                buff = txt;
                writeinfo.wbuff = System.Text.Encoding.Default.GetBytes(buff);
                //要写入的内容
                for (k = 0; k < (buff.Length / 200); k++)
                {
                    //写数据标志
                    inBuffer[0] = writeinfo.wflag[0];
                    inBuffer[1] = (byte)k;
                    for (i = 0; i < 200; i++)
                    {
                        inBuffer[i + 2] = writeinfo.wbuff[k * 200 + i];
                    }
                    ret = S4Execute(ref si[0], "0001", inBuffer, 202, inBuffer, 256, ref BytesReturned);
                    if (ret != 0x00)
                    {
                        S4Close(ref si[0]);
                        return "Lock the file failed execution:" + ret;
                    }
                }
                inBuffer[0] = writeinfo.wflag[0];
                inBuffer[1] = (byte)k;
                for (i = 0; i < (buff.Length % 200); i++)
                {
                    inBuffer[i + 2] = writeinfo.wbuff[k * 200 + i];
                }
                ret = S4Execute(ref si[0], "0001", inBuffer, (uint)((buff.Length % 200) + 2), inBuffer, 256, ref BytesReturned);
                if (ret != 0x00)
                {
                    S4Close(ref si[0]);
                    return "Lock the file failed execution:" + ret;
                }
                S4Close(ref si[0]);
            }
            return "";
        }
        /// <summary>
        /// 修改密码 
        /// </summary>
        /// <param name="type">type-1 修改用户密码 type-2 修改设备密码 </param>
        /// <returns></returns>
        public string ChangePwd(int type)
        {
            string rtn = "";
            uint ret;
            SENSE4_CONTEXT[] si = null;
            if (Check(UserPwd, ref si) == "")
            {
                if (type == 1)
                {
                    byte[] OldPin = { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38 };
                    byte[] NewPin = new byte[8];
                    Encoding.ASCII.GetBytes(UserPwd, 0, 8, NewPin, 0);
                    ret = S4ChangePin(ref si[0], OldPin, 8, NewPin, 8, S4_USER_PIN);
                    if (ret == 0)
                    {
                        rtn = "user pin modify success";
                    }
                    else
                    {
                        rtn = "user pin modify failed";
                        return rtn;
                    }
                }
                else if (type == 2)
                {
                    byte[] OldPin = { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38 };
                    byte[] NewPin = new byte[24];
                    Encoding.ASCII.GetBytes(DevicePwd, 0, 24, NewPin, 0);
                    ret = S4ChangePin(ref si[0], OldPin, 24, NewPin, 24, S4_DEV_PIN);
                    if (ret == 0)
                    {
                        rtn += ";device pin modify success";

                    }
                    else
                    {
                        rtn += ";device pin modify failed";
                    }
                }

                ret = S4Close(ref si[0]);
            }
            return rtn;
        }
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="fileLenght"></param>
        /// <returns></returns>
        public string CreateFile(string fileLenght)
        {
            String buff = "";
            String name = "";
            uint len = 0;
            uint ret;
            SENSE4_CONTEXT[] si = null;
            if (Check(DevicePwd, ref si) == "")
            {
                //创建数据文件
                uint BytesWritten = 0;
                name = "0002";
                buff = fileLenght;
                len = (uint)Convert.ToInt16(buff);
                byte[] indata = new byte[50];
                ret = S4WriteFile(ref si[0], name, 0, null, 0, len, ref BytesWritten, S4_CREATE_NEW, S4_DATA_FILE);
                if (ret == 0x00006a80)
                {
                    // uint BytesWritten1 = 50;
                    ret = S4WriteFile(ref si[0], name, 0, indata, 50, 0, ref BytesWritten, S4_UPDATE_FILE, S4_DATA_FILE);
                    if (ret != S4_SUCCESS)
                    {
                        S4Close(ref si[0]);
                        return "Lock the file failed execution:" + ret;
                    }

                }
                else
                {
                    if (ret != S4_SUCCESS && ret != 0x00006a80)
                    {
                        return "Creat the file failed：" + ret;
                    }
                }

                S4Close(ref si[0]);
            }
            return "";
        }
        #endregion
    }
}
