using System;
using System.Collections.Generic;
using System.IO;

namespace AwareTec.Polysmith.Util
{
    /// <summary>
    /// 文件夹帮助类
    /// </summary>
    public class FolderHelper
    {
        /// <summary>
        /// 文件夹拷贝
        /// </summary>
        /// <param name="srcFolderPath"></param>
        /// <param name="destFolderPath"></param>
        public static void FolderCopy(string srcFolderPath, string destFolderPath, bool addParentFolder = true)
        {
            //检查目标目录是否以目标分隔符结束，如果不是则添加之
            if (destFolderPath[destFolderPath.Length - 1] != Path.DirectorySeparatorChar)
                destFolderPath += Path.DirectorySeparatorChar;
            if (addParentFolder)
            {
                destFolderPath = string.Format("{0}\\{1}\\", destFolderPath, Path.GetFileName(srcFolderPath));
            }
            //判断目标目录是否存在，如果不在则创建之
            if (!Directory.Exists(destFolderPath))
                Directory.CreateDirectory(destFolderPath);
            string[] fileList = Directory.GetFileSystemEntries(srcFolderPath);
            foreach (string file in fileList)
            {
                if (Directory.Exists(file))
                    FolderCopy(file, destFolderPath + Path.GetFileName(file), false);
                else
                {
                    FileInfo fi = new FileInfo(file);
                    if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)//改变只读文件属性，否则删不掉
                        fi.Attributes = FileAttributes.Normal;
                    try
                    { File.Copy(file, destFolderPath + Path.GetFileName(file), true); }
                    catch (Exception e)
                    {

                    }
                }

            }
        }
        /// <summary>
        /// 删除文件夹及文件夹中的内容
        /// </summary>
        /// <param name="delFolderPath"></param>
        public static void FolderDelete(string delFolderPath)
        {
            if (delFolderPath[delFolderPath.Length - 1] != Path.DirectorySeparatorChar)
                delFolderPath += Path.DirectorySeparatorChar;
            string[] fileList = new string[0];
            if (Directory.Exists(delFolderPath))
            {
                fileList = Directory.GetFileSystemEntries(delFolderPath);
                foreach (string item in fileList)
                {
                    if (File.Exists(item))
                    {
                        FileInfo fi = new FileInfo(item);
                        if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)//改变只读文件属性，否则删不掉
                            fi.Attributes = FileAttributes.Normal;
                        File.Delete(item);
                    }//删除其中的文件
                    else
                        FolderDelete(item);//递归删除子文件夹
                }
                Directory.Delete(delFolderPath);//删除已空文件夹
            }
        }

        /// <summary>
        /// 文件夹移动
        /// </summary>
        /// <param name="srcFolderPath"></param>
        /// <param name="destFolderPath"></param>
        public static void FolderMove(string srcFolderPath, string destFolderPath, bool addParent = true)
        {
            //检查目标目录是否以目标分隔符结束，如果不是则添加之
            if (destFolderPath[destFolderPath.Length - 1] != Path.DirectorySeparatorChar)
                destFolderPath += Path.DirectorySeparatorChar;
            if (addParent)
            {
                destFolderPath = string.Format("{0}{1}\\", destFolderPath, Path.GetFileName(srcFolderPath));
            }
            //判断目标目录是否存在，如果不在则创建之
            if (!Directory.Exists(destFolderPath))
                Directory.CreateDirectory(destFolderPath);
            string[] fileList = Directory.GetFileSystemEntries(srcFolderPath);
            foreach (string file in fileList)
            {
                if (Directory.Exists(file))
                {
                    FolderMove(file, destFolderPath + Path.GetFileName(file), false);
                }
                else
                {
                    string dest = destFolderPath + Path.GetFileName(file);
                    if (!File.Exists(dest))
                    {
                        File.Move(file, dest);
                    }
                    else
                    {
                        File.Delete(file);
                    }
                }
            }
            Directory.Delete(srcFolderPath);
        }

        /// <summary>
        /// 搜索路径下所有当前扩展名的文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="Extension">扩展名</param>
        /// <param name="smallDir"></param>
        public static void GetFile(string path, string Extension, ref List<string> files)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo dir = new DirectoryInfo(path);

                FileInfo[] fil = dir.GetFiles();
                DirectoryInfo[] dii = dir.GetDirectories();
                foreach (FileInfo f in fil)
                {
                    string name = Path.GetExtension(f.FullName);
                    if (name.Equals(Extension))
                    {
                        files.Add(f.FullName);
                    }
                }
                //获取子文件夹内的文件列表，递归遍历
                foreach (DirectoryInfo d in dii)
                {
                    GetFile(d.FullName, Extension, ref files);
                }
            }
        }
    }
}
