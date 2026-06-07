using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;

namespace AwareTec.Polysmith.Util
{
    public class ZipHelper
    {
        #region 压缩
        /// <summary>   
        /// 递归压缩文件夹的内部方法   
        /// </summary>   
        /// <param name="folderToZip">要压缩的文件夹路径</param>   
        /// <param name="zipStream">压缩输出流</param>   
        /// <param name="parentFolderName">此文件夹的上级文件夹</param>   
        /// <returns></returns>   
        private static bool ZipDirectory(string folderToZip, ZipOutputStream zipStream, string parentFolderName, bool NoParentFolder = true)
        {
            if (!getRunState())
                return false;
            bool result = true;
            string[] folders, files;
            ZipEntry ent = null;
            FileStream fs = null;
            Crc32 crc = new Crc32();
            ZipOutputStream zipOutputStream = zipStream;
            string parentName = "";
            try
            {
                if (!NoParentFolder)
                {
                    parentName = Path.Combine(parentFolderName, Path.GetFileName(folderToZip) + "/");
                    ent = new ZipEntry(parentName);
                    zipOutputStream.PutNextEntry(ent);
                    zipOutputStream.Flush();
                }
                files = Directory.GetFiles(folderToZip);
                foreach (string file in files)
                {
                    if (!getRunState())
                        break;
                    fs = File.OpenRead(file);
                    int readlen = 4096000;
                    //每次4M
                    byte[] buffer = new byte[readlen];

                    ent = new ZipEntry(Path.Combine(parentFolderName, string.Format("{0}{1}", NoParentFolder ? "" : Path.GetFileName(folderToZip) + "/", Path.GetFileName(file))));
                    ent.Size = fs.Length;

                    long remaindSize = fs.Length;
                    //计算CRC
                    crc.Reset();
                    while (remaindSize > 0)
                    {
                        if (remaindSize < readlen)
                        {
                            readlen = (int)remaindSize;
                            buffer = new byte[readlen];
                        }
                        fs.Read(buffer, 0, readlen);
                        remaindSize -= readlen;
                        crc.Update(buffer);
                        if (!getRunState())
                            return false;
                    }
                    ent.Crc = crc.Value;
                    zipOutputStream.PutNextEntry(ent);

                    //压缩数据
                    readlen = 4096000;
                    buffer = new byte[readlen];
                    remaindSize = fs.Length;
                    fs.Seek(0, SeekOrigin.Begin);
                    while (remaindSize > 0)
                    {
                        if (remaindSize < readlen)
                            readlen = (int)remaindSize;
                        fs.Read(buffer, 0, readlen);
                        remaindSize -= readlen;
                        zipOutputStream.Write(buffer, 0, readlen);
                        if (!getRunState())
                            return false;
                    }
                    fs.Close();
                }
            }
            catch (Exception ee)
            {
                result = false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                if (ent != null)
                {
                    ent = null;
                }
                GC.Collect();
                GC.Collect(1);
            }
            if (getRunState())
            {
                folders = Directory.GetDirectories(folderToZip);
                foreach (string folder in folders)
                    if (!ZipDirectory(folder, zipStream, parentName, false))
                        return false;
            }
            return result;
        }

        /// <summary>   
        /// 压缩文件夹    
        /// </summary>   
        /// <param name="folderToZip">要压缩的文件夹路径</param>   
        /// <param name="zipedFile">压缩文件完整路径</param>   
        /// <param name="password">密码</param>   
        /// <returns>是否压缩成功</returns>   
        public static bool ZipDirectory(string folderToZip, string zipedFile, string password, bool NoParentFolder = true)
        {
            bool result = false;
            if (!Directory.Exists(folderToZip))
                return result;
            try
            {
                using (ZipOutputStream zipStream = new ZipOutputStream(File.Create(zipedFile)))
                {
                    zipStream.SetLevel(0);
                    if (!string.IsNullOrEmpty(password)) zipStream.Password = password;
                    if (getRunState())
                        result = ZipDirectory(folderToZip, zipStream, "", NoParentFolder);

                    zipStream.Finish();
                    zipStream.Close();
                }
            }
            catch (Exception ee)
            {

            }
            if (!getRunState())
            {
                if (File.Exists(zipedFile))
                    File.Delete(zipedFile);
            }
            return result;
        }

        /// <summary>   
        /// 压缩文件夹   
        /// </summary>   
        /// <param name="folderToZip">要压缩的文件夹路径</param>   
        /// <param name="zipedFile">压缩文件完整路径</param>   
        /// <returns>是否压缩成功</returns>   
        public static bool ZipDirectory(string folderToZip, string zipedFile)
        {
            bool result = ZipDirectory(folderToZip, zipedFile, null);
            return result;
        }

        /// <summary>   
        /// 压缩文件   
        /// </summary>   
        /// <param name="fileToZip">要压缩的文件全名</param>   
        /// <param name="zipedFile">压缩后的文件名</param>   
        /// <param name="password">密码</param>   
        /// <returns>压缩结果</returns>   
        public static bool ZipFile(string fileToZip, string zipedFile, string password)
        {
            bool result = true;
            ZipOutputStream zipStream = null;
            FileStream fs = null;
            ZipEntry ent = null;

            if (!File.Exists(fileToZip))
                return false;

            try
            {
                fs = File.OpenRead(fileToZip);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();

                fs = File.Create(zipedFile);
                zipStream = new ZipOutputStream(fs);
                if (!string.IsNullOrEmpty(password)) zipStream.Password = password;
                ent = new ZipEntry(Path.GetFileName(fileToZip));
                zipStream.PutNextEntry(ent);
                zipStream.SetLevel(0);

                zipStream.Write(buffer, 0, buffer.Length);

            }
            catch
            {
                result = false;
            }
            finally
            {
                if (zipStream != null)
                {
                    zipStream.Finish();
                    zipStream.Close();
                }
                if (ent != null)
                {
                    ent = null;
                }
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
            GC.Collect();
            GC.Collect(1);

            return result;
        }

        /// <summary>   
        /// 压缩文件   
        /// </summary>   
        /// <param name="fileToZip">要压缩的文件全名</param>   
        /// <param name="zipedFile">压缩后的文件名</param>   
        /// <returns>压缩结果</returns>   
        public static bool ZipFile(string fileToZip, string zipedFile)
        {
            bool result = ZipFile(fileToZip, zipedFile, null);
            return result;
        }

        /// <summary>   
        /// 压缩文件或文件夹   
        /// </summary>   
        /// <param name="fileToZip">要压缩的路径</param>   
        /// <param name="zipedFile">压缩后的文件名</param>   
        /// <param name="password">密码</param>   
        /// <returns>压缩结果</returns>   
        public static bool Zip(string fileToZip, string zipedFile, string password)
        {
            bool result = false;
            if (Directory.Exists(fileToZip))
                result = ZipDirectory(fileToZip, zipedFile, password);
            else if (File.Exists(fileToZip))
                result = ZipFile(fileToZip, zipedFile, password);

            return result;
        }

        /// <summary>   
        /// 压缩文件或文件夹   
        /// </summary>   
        /// <param name="fileToZip">要压缩的路径</param>   
        /// <param name="zipedFile">压缩后的文件名</param>   
        /// <returns>压缩结果</returns>   
        public static bool Zip(string fileToZip, string zipedFile)
        {
            bool result = Zip(fileToZip, zipedFile, null);
            return result;

        }

        #endregion

        #region 解压

        /// <summary>   
        /// 解压功能(解压压缩文件到指定目录)   
        /// </summary>   
        /// <param name="fileToUnZip">待解压的文件</param>   
        /// <param name="zipedFolder">指定解压目标目录</param>   
        /// <param name="password">密码</param>   
        /// <returns>解压结果</returns>   
        public static bool UnZip(string fileToUnZip, string zipedFolder, string password)
        {
            bool result = true;
            ZipEntry ent = null;
            string fileName;

            if (!File.Exists(fileToUnZip))
                return false;

            if (!Directory.Exists(zipedFolder))
                Directory.CreateDirectory(zipedFolder);

            try
            {
                using (ZipInputStream zipStream = new ZipInputStream(File.OpenRead(fileToUnZip)))
                {
                    if (!string.IsNullOrEmpty(password)) zipStream.Password = password;
                    while ((ent = zipStream.GetNextEntry()) != null && getRunState())
                    {
                        if (!string.IsNullOrEmpty(ent.Name))
                        {
                            fileName = Path.Combine(zipedFolder, ent.Name);
                            fileName = fileName.Replace('/', '\\');//change by Mr.HopeGi   

                            if (fileName.EndsWith("\\"))
                            {
                                Directory.CreateDirectory(fileName);
                                continue;
                            }

                            using (FileStream fs = File.Create(fileName))
                            {
                                int size = 2048;
                                byte[] data = new byte[size];
                                while (getRunState())
                                {
                                    size = zipStream.Read(data, 0, data.Length);
                                    if (size > 0)
                                        fs.Write(data, 0, size);
                                    else
                                        break;
                                }
                                fs.Flush();
                                fs.Close();
                            }
                        }
                    }
                }
            }
            catch
            {
                result = false;
            }
            finally
            {
                if (ent != null)
                {
                    ent = null;
                }
                GC.Collect();
                GC.Collect(1);
            }
            return result;
        }

        /// <summary>   
        /// 解压功能(解压压缩文件到指定目录)   
        /// </summary>   
        /// <param name="fileToUnZip">待解压的文件</param>   
        /// <param name="zipedFolder">指定解压目标目录</param>   
        /// <returns>解压结果</returns>   
        public static bool UnZip(string fileToUnZip, string zipedFolder)
        {
            bool result = UnZip(fileToUnZip, zipedFolder, null);
            return result;
        }
        #endregion

        public delegate bool GetRunStateDelegate();
        /// <summary>
        /// 获取运行状态时发生
        /// </summary>
        public static event GetRunStateDelegate GetRunStateHandler;

        private static bool getRunState()
        {
            if (GetRunStateHandler != null)
                return GetRunStateHandler.Invoke();
            return true;
        }
    }
}
