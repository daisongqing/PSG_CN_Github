using AwareTec.Polysmith.Cloud.UI.ViewHelpers.ViewHelperEventArgs;
using AwareTec.Polysmith.Util.PathUtils;
using RestfulWebRequest.RestfulTable.RestfulResponseTable.BuiltInType;
using System;
using System.IO;
using System.IO.Compression;

namespace AwareTec.Polysmith.Cloud.UI.ViewHelpers
{
    public class ZipFileHelper
    {
        #region 单例模式
        private ZipFileHelper(){ }

        private static readonly ZipFileHelper _instance = new ZipFileHelper();
        public static ZipFileHelper Instance => _instance;
        #endregion

        private event EventHandler<ZipFileEventArgs> _zipFileProgressIntChanged;

        public event EventHandler<ZipFileEventArgs> ZipFileProgressIntChanged
        {
            add
            {
                if (value == null)
                    return;

                if (_zipFileProgressIntChanged == null ||
                   _zipFileProgressIntChanged.GetInvocationList().Length == 0)
                {
                    _zipFileProgressIntChanged = value;
                }
                else
                {
                    foreach (var item in _zipFileProgressIntChanged.GetInvocationList())
                    {
                        if (value == item)
                            return;
                    }
                    _zipFileProgressIntChanged += value;
                }
            }
            remove
            {
                _zipFileProgressIntChanged = null;
            }
        }

        /// <summary>
        /// 每个切分文件的字节数
        /// </summary>
        private const int SIZE_OF_SPLIT = 1024 * 2048;
        public void ZipFileWithProgress(string filePath,
                                        string zipPath,
                                        TaskUion order)
        {
            double percentage = 0;
            try
            {
                if (string.IsNullOrWhiteSpace(filePath) ||
                (!StringPath.PathExists(filePath)))
                    throw new ArgumentException("传入的文件路径为空或不存在");

                if (string.IsNullOrWhiteSpace(zipPath))
                    throw new ArgumentException("传入的压缩文件路径为空");

                if(StringPath.PathExists(zipPath))
                    File.Delete(zipPath);

                var fileInfo = new FileInfo(filePath);
                long totalBytes = fileInfo.Length;
                int splitCount = totalBytes % SIZE_OF_SPLIT == 0 ?
                                 (int)(totalBytes / SIZE_OF_SPLIT) :
                                 (int)(totalBytes / SIZE_OF_SPLIT + 1);

                using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Create));

                using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Update))
                {
                    string entryName = fileInfo.Name;
                    ZipArchiveEntry entry = archive.CreateEntry(entryName);
                    entry.LastWriteTime = fileInfo.LastWriteTime;

                    using (Stream writer = entry.Open())
                    {
                        using (Stream reader = File.OpenRead(fileInfo.FullName))
                        {
                            for (int index = 1; index <= splitCount; index++)
                            {
                                int splitBytes = index == splitCount ?
                                                 (int)(totalBytes - ((index - 1) * SIZE_OF_SPLIT)) :
                                                 SIZE_OF_SPLIT;
                                byte[] readData = new byte[splitBytes];
                                reader.Seek(SIZE_OF_SPLIT * (index - 1), SeekOrigin.Begin);
                                if (reader.Read(readData, 0, readData.Length) > 0)
                                {
                                    writer.Write(readData, 0, readData.Length);
                                    percentage = index == splitCount ?
                                                 100 : percentage += (1.0 / splitCount);

                                    if(index != splitCount)
                                    {
                                        var inProgressArgs = new ZipFileEventArgs(false, string.Empty, percentage, zipPath, order, filePath);
                                        if (_zipFileProgressIntChanged != null)
                                            _zipFileProgressIntChanged.Invoke(this, inProgressArgs);
                                    }
                                }
                                else
                                {
                                    throw new Exception("切分流时单次读取失败");
                                }
                            }
                        }
                    }
                }
                var args = new ZipFileEventArgs(true, string.Empty, percentage, zipPath, order, filePath);
                if (_zipFileProgressIntChanged != null)
                    _zipFileProgressIntChanged.Invoke(this, args);
            }
            catch(Exception ex)
            {
                var args = new ZipFileEventArgs(false, ex.Message, percentage, zipPath, order, filePath);
                if(_zipFileProgressIntChanged != null)
                    _zipFileProgressIntChanged.Invoke(this, args);
            }
        }
    }
}
