using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;

namespace AwareTec.Polysmith.Util
{
    /// <summary>
    /// 解压缩帮助类
    /// </summary>
    public class SharpZipLibHelper
    {
        private static byte[] buffer = new byte[2048];

        /// <summary>
        /// 解压缩目录
        /// </summary>
        /// <param name="zipDirectoryPath">  压缩目录路径 </param>
        /// <param name="unZipDirecotyPath"> 解压缩目录路径 </param>
        public static void UnZip(string zipfile, string unzip_dir, string Password)
        {
            while (unzip_dir.LastIndexOf("\\") + 1 == unzip_dir.Length)//检查路径是否以"\"结尾
            {
                unzip_dir = unzip_dir.Substring(0, unzip_dir.Length - 1);//如果是则去掉末尾的"\"
            }

            using (ZipInputStream zipStream = new ZipInputStream(File.OpenRead(zipfile)))
            {
                //判断Password
                if (Password != null && Password.Length > 0)
                {
                    zipStream.Password = Password;
                }

                ZipEntry zipEntry = null;
                while ((zipEntry = zipStream.GetNextEntry()) != null)
                {
                    string directoryName = Path.GetDirectoryName(zipEntry.Name);
                    string fileName = Path.GetFileName(zipEntry.Name);

                    if (!string.IsNullOrEmpty(directoryName))
                    {
                        Directory.CreateDirectory(unzip_dir + @"\" + directoryName);
                    }

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        if (zipEntry.CompressedSize == 0)
                            break;
                        if (zipEntry.IsDirectory)//如果压缩格式为文件夹方式压缩
                        {
                            directoryName = Path.GetDirectoryName(unzip_dir + @"\" + zipEntry.Name);
                            Directory.CreateDirectory(directoryName);
                        }
                        else//2012-5-28修改，支持单个文件压缩时自己创建目标文件夹
                        {
                            if (!Directory.Exists(unzip_dir))
                            {
                                Directory.CreateDirectory(unzip_dir);
                            }
                        }

                        using (FileStream stream = File.Create(unzip_dir + @"\" + zipEntry.Name))
                        {
                            while (true)
                            {
                                int size = zipStream.Read(buffer, 0, buffer.Length);
                                if (size > 0)
                                {
                                    stream.Write(buffer, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        ///　压缩文件夹
        /// </summary>
        /// <param name="dir">待压缩的文件夹</param>
        /// <param name="targetFileName">压缩后文件路径（包括文件名）</param>
        /// <returns></returns>
        public static bool Zip(string zip_dir, string targetfile, string password)
        {
            bool recursive = true; //是否递归压缩

            //如果已经存在目标文件，询问用户是否覆盖
            if (File.Exists(targetfile))
            {
                return false;
            }
            string[] ars = new string[2];
            if (recursive == false)
            {
                ars[0] = zip_dir;
                ars[1] = targetfile;
                return ZipFileDictory(ars, password);
            }
            FileStream ZipFile;
            ZipOutputStream ZipStream;

            //open
            ZipFile = File.Create(targetfile);
            ZipStream = new ZipOutputStream(ZipFile);
            ZipStream.SetLevel(9);
            ZipStream.Password = password;

            if (zip_dir != String.Empty)
            {
                _CompressFolder(zip_dir, ZipStream, zip_dir.Substring(3));
            }

            //close
            ZipStream.Finish();
            ZipStream.Close();

            if (File.Exists(targetfile))
                return true;
            else
                return false;
        }

        public static bool ZipFile(string zipfile, string targetfile, string password)
        {
            //如果已经存在目标文件，询问用户是否覆盖
            if (File.Exists(targetfile))
            {
                return false;
            }
            string[] ars = new string[2];

            FileStream ZipFile;
            ZipOutputStream ZipStream;

            //open
            ZipFile = File.Create(targetfile);
            ZipStream = new ZipOutputStream(ZipFile);
            ZipStream.SetLevel(6);
            ZipStream.Password = password;

            if (zipfile != String.Empty)
            {
                _AddFile(zipfile, ZipStream, zipfile.Substring(3));
            }

            //close
            ZipStream.Finish();
            ZipStream.Close();

            if (File.Exists(targetfile))
                return true;
            else
                return false;
        }

        /// <summary>
        ///　压缩某个子文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="zips"></param>
        /// <param name="zipfolername"></param>
        private static void _AddFile(string fileName, ZipOutputStream zips, string zipfolername)
        {
            if (File.Exists(fileName))
            {
                _CreateZipFile(fileName, zips, zipfolername);
            }
        }

        /// <summary>
        /// 压缩某个子文件夹
        /// </summary>
        /// <param name="basePath">      </param>
        /// <param name="zips">          </param>
        /// <param name="zipfolername">  </param>
        private static void _CompressFolder(string basePath, ZipOutputStream zips, string zipfolername)
        {
            if (File.Exists(basePath))
            {
                _AddFile(basePath, zips, zipfolername);
                return;
            }
            string[] names = Directory.GetFiles(basePath);
            foreach (string fileName in names)
            {
                _AddFile(fileName, zips, zipfolername);
            }

            names = Directory.GetDirectories(basePath);
            foreach (string folderName in names)
            {
                _CompressFolder(folderName, zips, zipfolername);
            }
        }

        /// <summary>
        /// 压缩单独文件
        /// </summary>
        /// <param name="FileToZip">     </param>
        /// <param name="zips">          </param>
        /// <param name="zipfolername">  </param>
        private static void _CreateZipFile(string FileToZip, ZipOutputStream zips, string zipfolername)
        {
            try
            {
                FileStream StreamToZip = new FileStream(FileToZip, FileMode.Open, FileAccess.Read);
                string temp = FileToZip;
                string temp1 = zipfolername;
                if (temp1.Length > 0)
                {
                    int i = temp1.LastIndexOf("\\") + 1;//这个地方原来是个bug用的是"//"，导致压缩路径过长路径2012-7-2
                    int j = temp.Length - i;
                    temp = temp.Substring(i, j);
                }
                ZipEntry ZipEn = new ZipEntry(temp.Substring(3));

                zips.PutNextEntry(ZipEn);
                byte[] buffer = new byte[16384];
                System.Int32 size = StreamToZip.Read(buffer, 0, buffer.Length);
                zips.Write(buffer, 0, size);
                try
                {
                    while (size < StreamToZip.Length)
                    {
                        int sizeRead = StreamToZip.Read(buffer, 0, buffer.Length);
                        zips.Write(buffer, 0, sizeRead);
                        size += sizeRead;
                    }
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }

                StreamToZip.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 压缩目录
        /// </summary>
        /// <param name="args"> 数组(数组[0]: 要压缩的目录; 数组[1]: 压缩的文件名) </param>
        private static bool ZipFileDictory(string[] args, string password)
        {
            ZipOutputStream s = null;
            try
            {
                string[] filenames = Directory.GetFiles(args[0]);

                Crc32 crc = new Crc32();
                s = new ZipOutputStream(File.Create(args[1]));
                s.SetLevel(6);
                s.Password = password;

                foreach (string file in filenames)
                {
                    //打开压缩文件
                    FileStream fs = File.OpenRead(file);

                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    ZipEntry entry = new ZipEntry(file);

                    entry.DateTime = DateTime.Now;

                    entry.Size = fs.Length;
                    fs.Close();

                    crc.Reset();
                    crc.Update(buffer);

                    entry.Crc = crc.Value;

                    s.PutNextEntry(entry);

                    s.Write(buffer, 0, buffer.Length);
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                s.Finish();
                s.Close();
            }
            return true;
        }

        /// <summary>
        /// 压缩单文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="savePath">压缩包路径</param>
        /// <param name="isReserved">是否保存原文件</param>
        public static void ZipFile(string filePath, string savePath, bool isReserved = true)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(string.Format("{0}不存在.", filePath));
            }
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
            }
            FileInfo file = new FileInfo(filePath);
            string filename = Path.GetFileNameWithoutExtension(filePath);
            string tempDir = Path.Combine(file.DirectoryName, "temp_Zip");
            //string tempDir = file.DirectoryName;
            string zipfilepath = Path.Combine(tempDir, file.Name);
            if (!Directory.Exists(tempDir))
            {
                Directory.CreateDirectory(tempDir);
            }
            if (!File.Exists(zipfilepath))
            {
                //默认保留原文件
                if (isReserved)
                {
                    file.CopyTo(zipfilepath);
                }
                else
                {
                    file.MoveTo(zipfilepath);
                }

            }
            System.IO.Compression.ZipFile.CreateFromDirectory(tempDir, savePath);
            Directory.Delete(tempDir, true);
        }

        /// <summary>
        /// 文件解压
        /// </summary>
        /// <param name="zipPath">压缩包路径</param>
        /// <param name="saveDir">解压保存文件夹</param>
        /// <param name="isReserved">是否保存压缩包</param>
        public static void UnZipFile(string zipPath, string saveDir, bool isReserved = true)
        {
            System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, saveDir);
            if (!isReserved)
            {
                File.Delete(zipPath);
            }
        }
    }
}
