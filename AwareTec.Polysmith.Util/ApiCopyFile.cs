using System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Util
{
    /// <summary>
    /// 文件拷贝
    /// </summary>
    public class ApiCopyFile
    {
        private const int FO_COPY = 0x0002;
        private const int FOF_ALLOWUNDO = 0x00044;
        //显示进度条  0x00044 // 不显示一个进度对话框 0x0100 显示进度对话框单不显示进度条  0x0002显示进度条和对话框  
        private const int FOF_SILENT = 0x0002;//0x0100;  
        //  
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 0)]
        public struct SHFILEOPSTRUCT
        {
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.U4)]
            public int wFunc;
            public string pFrom;
            public string pTo;
            public short fFlags;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fAnyOperationsAborted;
            public IntPtr hNameMappings;
            public string lpszProgressTitle;
        }
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        static extern int SHFileOperation(ref SHFILEOPSTRUCT FileOp);
        public static bool DoCopy(string strSource, string strTarget, IntPtr hwnd)
        {
            if (!File.Exists(strTarget))
            {
                SHFILEOPSTRUCT fileop = new SHFILEOPSTRUCT();
                fileop.wFunc = FO_COPY;
                if (hwnd != IntPtr.Zero)
                    fileop.hwnd = hwnd;
                fileop.pFrom = string.Format("{0}\0", strSource);
                fileop.lpszProgressTitle = "EDF文件迁移";
                fileop.pTo = string.Format("{0}\0", strTarget);
                //fileop.fFlags = FOF_ALLOWUNDO;  
                fileop.fFlags = FOF_SILENT;
                int retcode = SHFileOperation(ref fileop);
                return retcode == 0;///1223是不执行的返回代码
            }
            return true;
        }
    }
}
