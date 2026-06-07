#region 导包
using AwareTec.Polysmith.Util.BaseException;
using AwareTec.Polysmith.Util.BaseException.ExTypeDefine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
#endregion

namespace AwareTec.Polysmith.Util.PathUtils
{
    /// <summary>
    /// 管理字符串形式的路径
    /// </summary>
    public class StringPath
    {
        /// <summary>
        /// 将C#源代码中获取的路径转为日志输出的路径
        /// </summary>
        /// <param name="cSharpPath"></param>
        /// <returns></returns>
        public static string ConvertLogPath(string cSharpPath)
        {
            if (string.IsNullOrWhiteSpace(cSharpPath))
                return "默认路径";

            return cSharpPath.Replace("\\", "/");
        }

        /// <summary>
        /// 判断路径是否存在
        /// </summary>
        /// <returns>存在返回true, 不存在返回false</returns>
        public static bool PathExists(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return false;

            if ((!File.Exists(path)) && (!Directory.Exists(path)))
                return false;

            return true;
        }

        /// <summary>
        /// 获取已存在路径的类型
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static PathType GetTypeOfPath(string path)
        {
            if (!PathExists(path))
                return PathType.Invalid;

            if (File.Exists(path))
                return PathType.File;

            if (Directory.Exists(path))
                return PathType.Directory;

            return PathType.Error;
        }

        /// <summary>
        /// 判断路径是否正确
        /// </summary>
        /// <param name="path"></param>
        /// <remarks>
        /// 正确指即便该路径不存在，也能够被创建出来
        /// 不存在的路径也可以是正确的，只要能够被创建出来
        /// </remarks>
        public static bool PathIsCorrect(string path)
        {
            return true;
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns>创建成功或已存在均返回true,创建失败返回false</returns>
        public static bool CreateDir(string dirPath)
        {
            if (string.IsNullOrWhiteSpace(dirPath))
                return false;

            //存在返回true
            if (Directory.Exists(dirPath))
                return true;

            try
            {
                Directory.CreateDirectory(dirPath);
                return true;
            }
            catch (Exception e)
            {
                if (e is UnauthorizedAccessException ||
                   e is IOException)
                {
                    Dictionary<BaseExceptionInfoType, string> infos = new Dictionary<BaseExceptionInfoType, string>();
                    infos.Add(BaseExceptionInfoType.Context, "创建目录");
                    infos.Add(BaseExceptionInfoType.Reason, "调用者无操作权限或IO异常");

                    throw new IOOperationException(typeof(StringPath),
                                                   infos,
                                                   e);
                }
                return false;
            }
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool CreateFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return false;

            //存在返回true
            if (File.Exists(filePath))
                return true;

            try
            {
                File.Create(filePath);
                return true;
            }
            catch (Exception e)
            {
                if (e is UnauthorizedAccessException ||
                   e is IOException)
                {
                    Dictionary<BaseExceptionInfoType, string> infos = new Dictionary<BaseExceptionInfoType, string>();
                    infos.Add(BaseExceptionInfoType.Context, "创建文件");
                    infos.Add(BaseExceptionInfoType.Reason, "调用者无操作权限或IO异常");

                    throw new IOOperationException(typeof(StringPath),
                                                   infos,
                                                   e);
                }
                return false;
            }
        }

        /// <summary>
        /// 获取文件路径的后缀名
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>
        /// 路径非法或不是文件或无后缀名，则返回空串
        /// </returns>
        public static string GetFileExtension(string filePath)
        {
            if (GetTypeOfPath(filePath) == PathType.File)
            {
                var arr = filePath.Split('.');
                if (arr.Length < 1)
                    return string.Empty;
                else
                    return arr.LastOrDefault();
            }
            else
                return string.Empty;
        }

        /// <summary>
        /// 非递归式查找目录下的所有文件或目录
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="isFile"></param>
        /// <returns></returns>
        public static FileSystemInfo[] GetAllFilesOrDirsInDir(string dir, bool isFile)
        {
            if (!Directory.Exists(dir))
                return null;

            DirectoryInfo dInfo = new DirectoryInfo(dir);
            var infos = dInfo.GetFileSystemInfos();
            IEnumerable<FileSystemInfo> fInfos;
            if (isFile)
                fInfos = infos.Where(x => x.Attributes != FileAttributes.Directory);
            else
                fInfos = infos.Where(x => x.Attributes == FileAttributes.Directory);

            return fInfos.ToArray();
        }
    
        /// <summary>
        /// 获取当前电脑的全部盘符
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllDiskId()
        {
            List<string> deviceIDs = new List<string>();
            ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT  *  From  Win32_LogicalDisk ");
            ManagementObjectCollection queryCollection = query.Get();
            foreach (ManagementObject mo in queryCollection)
            {
                switch (int.Parse(mo["DriveType"].ToString()))
                {
                    case (int)DriveType.Removable: //可移动磁盘       
                        break;
                    case (int)DriveType.Fixed://本地磁盘       
                    {
                        deviceIDs.Add(mo["DeviceID"].ToString());
                        break;
                    }
                    case (int)DriveType.CDRom: //CD rom drives       
                        break;
                    case (int)DriveType.Network:   //网络驱动     
                        break;
                    case (int)DriveType.Ram:
                        break;
                    case (int)DriveType.NoRootDirectory:
                        break;
                    default:   //defalut   to   folder       
                        break;
                }
            }
            return deviceIDs;
        }

        /// <summary>
        /// 根据盘符名称获取盘符的总容量
        /// </summary>
        /// <param name="diskId"></param>
        /// <returns></returns>
        public static long GetHardDiskTotalSpace(string diskId)
        {
            long totalSize = new long();
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            diskId += "\\";
            foreach (System.IO.DriveInfo drive in drives)
            {
                if (drive.Name == diskId)
                {
                    totalSize = drive.TotalSize;
                }
            }
            return totalSize;
        }

        /// <summary>
        /// 根据盘符名称获取盘符的剩余容量
        /// </summary>
        /// <param name="diskId"></param>
        /// <returns></returns>
        public static long GetHardDiskFreeSpace(string diskId)
        {
            long freeSpace = new long();
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            diskId += "\\";
            foreach (System.IO.DriveInfo drive in drives)
            {
                if (drive.Name == diskId)
                {
                    freeSpace = drive.TotalFreeSpace;
                }
            }
            return freeSpace;
        }

        /// <summary>
        /// 获取当前电脑剩余容量最大的盘符名称
        /// </summary>
        /// <returns></returns>
        public static string GetMaxFreeSpaceDiskId()
        {
            var diskList = StringPath.GetAllDiskId();
            string maxFreeSpaceHardDiskId = "";
            long maxFreeSpaceHardDiskSize = 0;
            diskList.ForEach(x =>
            {
                long freeSpace = StringPath.GetHardDiskFreeSpace(x);
                if (freeSpace > maxFreeSpaceHardDiskSize)
                {
                    maxFreeSpaceHardDiskSize = freeSpace;
                    maxFreeSpaceHardDiskId = x;
                }
            });
            if (maxFreeSpaceHardDiskSize == 0)
                throw new InsufficientMemoryException("当前没有任何的可用盘符, 请清理磁盘空间后再进行操作");
            return maxFreeSpaceHardDiskId;
        }
    }

    public class StringPathConvertLogPathException : Exception
    {
        private static string _defaultMsg = @"
                                    {
                                        路径转为日志路径失败，原因是：{1}
                                    }";

        public StringPathConvertLogPathException(string reason
                                            ) :
                                            base(string.Format(_defaultMsg,
                                                                reason))
        { }
    }


}
