using AwareTec.Polysmith.Cloud.UI.ViewHelpers.ViewHelperEventArgs;
using AwareTec.Polysmith.Util.PathUtils;
using RestfulWebRequest.RestfulTable.RestfulResponseTable.BuiltInType;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.ViewHelpers
{
    public class FileCopyHelper
    {
        #region 单例模式
        private FileCopyHelper() { }

        private static readonly FileCopyHelper _instance = new FileCopyHelper();
        public static FileCopyHelper Instance => _instance;
        #endregion

        private event EventHandler<CopyFileEventArgs> _copyFileProgressIntChanged;

        public event EventHandler<CopyFileEventArgs> CopyFileProgressIntChanged
        {
            add
            {
                if (value == null)
                    return;

                if (_copyFileProgressIntChanged == null ||
                   _copyFileProgressIntChanged.GetInvocationList().Length == 0)
                {
                    _copyFileProgressIntChanged = value;
                }
                else
                {
                    foreach (var item in _copyFileProgressIntChanged.GetInvocationList())
                    {
                        if (value == item)
                            return;
                    }
                    _copyFileProgressIntChanged += value;
                }
            }
            remove
            {
                _copyFileProgressIntChanged = null;
            }
        }

        private const int SIZE_OF_SPLIT = 500;

        public void Copy(string originalPath,
                         string destinationPath,
                         TaskUion order)
        {
            double percentage = 0;
            try
            {
                if (string.IsNullOrWhiteSpace(originalPath) ||
                    (!StringPath.PathExists(originalPath)))
                    throw new ArgumentException("复制过程：传入的原文件路径为空或不存在");

                if (string.IsNullOrWhiteSpace(destinationPath))
                    throw new ArgumentException("复制过程：传入的目标文件路径为空");

                if (StringPath.PathExists(destinationPath))
                    File.Delete(destinationPath);

                var fileInfo = new FileInfo(originalPath);
                long totalBytes = fileInfo.Length;
                int splitCount = totalBytes % SIZE_OF_SPLIT == 0 ?
                                 (int)(totalBytes / SIZE_OF_SPLIT) :
                                 (int)(totalBytes / SIZE_OF_SPLIT + 1);

                using (var writer = new FileStream(destinationPath, FileMode.Create))
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
                                    var inProgressArgs = new CopyFileEventArgs(false, string.Empty, percentage, destinationPath, order);
                                    if (_copyFileProgressIntChanged != null)
                                        _copyFileProgressIntChanged.Invoke(this, inProgressArgs);
                                }
                            }
                            else
                            {
                                throw new Exception("切分流时单次读取失败");
                            }
                        }
                    }
                }
                var args = new CopyFileEventArgs(true, string.Empty, percentage, destinationPath, order);
                if (_copyFileProgressIntChanged != null)
                    _copyFileProgressIntChanged.Invoke(this, args);
            }
            catch (Exception ex)
            {
                var args = new CopyFileEventArgs(false, ex.Message, percentage, destinationPath, order);
                if (_copyFileProgressIntChanged != null)
                    _copyFileProgressIntChanged.Invoke(this, args);
            }
        }
    }
}
