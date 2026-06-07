using AwareTec.Polysmith.Cloud.UI.ViewHelpers.ViewHelperEventArgs;
using RestfulWebRequest.ApiCall;
using RestfulWebRequest.RestfulModel;
using RestfulWebRequest.RestfulTable.RestfulRequestTable;
using RestfulWebRequest.RestfulTable.RestfulResponseTable;
using RestfulWebRequest.RestfulTable.RestfulResponseTable.BuiltInType;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.ViewHelpers
{
    /// <summary>
    /// EDF文件断点续传帮助类
    /// </summary>
    public class FileResumableUploadsHelper
    {
        #region 单例模式
        private FileResumableUploadsHelper()
        {}

        private static readonly FileResumableUploadsHelper _instance = new FileResumableUploadsHelper();
        public static FileResumableUploadsHelper Instance => _instance;
        #endregion

        private event EventHandler<FileSliceUploadResultEventArgs> _fileSliceUploadResult;

        public event EventHandler<FileSliceUploadResultEventArgs> FileSliceUploadResult
        {
            add
            {
                if (value == null)
                    return;

                if (_fileSliceUploadResult == null ||
                   _fileSliceUploadResult.GetInvocationList().Length == 0)
                {
                    _fileSliceUploadResult = value;
                }
                else
                {
                    foreach (var item in _fileSliceUploadResult.GetInvocationList())
                    {
                        if (value == item)
                            return;
                    }
                    _fileSliceUploadResult += value;
                }
            }
            remove
            {
                _fileSliceUploadResult = null;
            }
        }

        /// <summary>
        /// 每个切分文件的字节数
        /// </summary>
        private const int SIZE_OF_EACH_FILE = 1024 * 500;

        /// <summary>
        /// 将EDF文件上传至该订单id
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public void UploadFile(string filePath, string edfPath, TaskUion order)
        {
            try
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException("请求上传的文件不存在");

                string fileName = Path.GetFileNameWithoutExtension(filePath) + Path.GetExtension(filePath); ;
                bool isSuccess = ApiRequest.Instance.UploadEDFMarkup(new UploadEDFMarkupRequestModel()
                {
                    id = order.OrderID
                }, out ResponseModel responseModel);
                if (!isSuccess)
                {
                    var model = responseModel as ResponseFailModel<UploadEDFMarkupRequestModel>;
                    throw new Exception(string.IsNullOrWhiteSpace(model.ErrorMessage) ?
                                        string.Format("无法获取当前上传的切片信息, 状态码为{0}", model.HttpStatusCode) :
                                        model.ErrorMessage);
                }

                int currentPieces = (responseModel as ResponseSuccessModel<UploadEDFMarkupResponseModel>).RestfulTable.currentPieces;
                using (var fileStreamReader = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    //分割的文件个数
                    int splitFileCount = fileStreamReader.Length % SIZE_OF_EACH_FILE == 0 ?
                                         (int)(fileStreamReader.Length / SIZE_OF_EACH_FILE) :
                                         (int)(fileStreamReader.Length / SIZE_OF_EACH_FILE + 1);
                    for (int index = currentPieces + 1; index <= splitFileCount; index++)
                    {
                        //切割的每份文件大小
                        int splitFileSize = index == splitFileCount ? (int)(fileStreamReader.Length - ((index - 1) * SIZE_OF_EACH_FILE)) : SIZE_OF_EACH_FILE;
                        //切割的文件数据
                        byte[] data = new byte[splitFileSize];
                        fileStreamReader.Seek(SIZE_OF_EACH_FILE * (index - 1), SeekOrigin.Begin);
                        if (fileStreamReader.Read(data, 0, splitFileSize) > 0)
                        {
                            using (var streamWriter = new MemoryStream())
                            {
                                streamWriter.Write(data, 0, data.Length);
                                streamWriter.Position = 0;
                                if (ApiRequest.Instance.UploadEDF(new UploadEDFRequestModel()
                                {
                                    id = order.OrderID,
                                    file = streamWriter,
                                    totalPieces = splitFileCount,
                                    currentPieces = index,
                                    fileName = fileName,
                                }, out ResponseModel uploadResponseModel) == false)
                                    throw new Exception("分片上传失败, 原因是:" + (uploadResponseModel as ResponseFailModel<UploadEDFRequestModel>).ErrorMessage);
                            }
                        }
                        else
                        {
                            throw new Exception("上传前切割文件失败");
                        }

                        var args = new FileSliceUploadResultEventArgs(true, null, index, splitFileCount, edfPath, order);
                        if (_fileSliceUploadResult != null)
                            _fileSliceUploadResult.Invoke(this, args);
                    }
                }
            }catch (Exception ex)
            {
                var args = new FileSliceUploadResultEventArgs(false, ex.Message, null, null, edfPath, order);
                if (_fileSliceUploadResult != null)
                    _fileSliceUploadResult.Invoke(this, args);
            }
        }
    }
}
