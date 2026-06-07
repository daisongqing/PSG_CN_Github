using AwareTec.Polysmith.Cloud.UI.ViewHelpers.ViewHelperEventArgs;
using AwareTec.Polysmith.Util.PathUtils;
using RestfulWebRequest.ApiCall;
using RestfulWebRequest.RestfulModel;
using RestfulWebRequest.RestfulTable.RestfulRequestTable;
using RestfulWebRequest.RestfulTable.RestfulResponseTable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.ViewHelpers
{
    /// <summary>
    /// 从远端下载文件的帮助类
    /// </summary>
    public class DownloadFileHelper
    {
        #region 单例模式
        private DownloadFileHelper() { }

        private static readonly DownloadFileHelper _instance = new DownloadFileHelper();
        public static DownloadFileHelper Instance => _instance;
        #endregion
        private static string m_NewFilePath = "";
        private static FileStream m_streamWriter = null;
        private static TaskUion _order = null;
        /// <summary>
        /// 中断标识
        /// </summary>
        public static bool Interrupt = false;

        private static double m_hasdowndataindex = 0;
        public static double HasDownDataIndex
        {
            set => m_hasdowndataindex = value;
        }

        private static event EventHandler<FileSliceDownloadResultEventArgs> _downFileProgressIntChanged;

        public static event EventHandler<FileSliceDownloadResultEventArgs> DownFileProgressIntChanged
        {
            add
            {
                if (value == null)
                    return;

                if (_downFileProgressIntChanged == null ||
                   _downFileProgressIntChanged.GetInvocationList().Length == 0)
                {
                    _downFileProgressIntChanged = value;
                }
                else
                {
                    foreach (var item in _downFileProgressIntChanged.GetInvocationList())
                    {
                        if (value == item)
                            return;
                    }
                    _downFileProgressIntChanged += value;
                }
            }
            remove
            {
                _downFileProgressIntChanged = null;
            }
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="directoryPath">保存的目录位置</param>
        /// <param name="newFileNameWithoutExtension">新文件名称</param>
        /// <param name="orderId">订单id</param>
        /// <param name="fileType">文件类型</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string Download(string directoryPath,
                                    string newFileNameWithoutExtension,
                                    string orderId,
                                    FileType fileType,
                                    TaskUion order=null)
        {
            if (string.IsNullOrWhiteSpace(directoryPath) ||
               string.IsNullOrWhiteSpace(newFileNameWithoutExtension))
                throw new ArgumentNullException("文件路径不得为空");

            if (File.Exists(directoryPath))
                throw new ArgumentException("所选的保存位置不存在");

            if (fileType == FileType.Edf)
            {
                _order = order;
            }
            if (fileType == FileType.Report && _order != null)
            {
                return string.Empty;
            }
            m_NewFilePath = string.Format("{0}\\{1}", directoryPath, newFileNameWithoutExtension);
            string fullFileName = string.Empty;
            Stream stream = DownloadViaRestful(orderId, fileType, out string extensionName);
            {
                fullFileName = string.Format("{0}.{1}", m_NewFilePath, extensionName);
                if (stream != null)
                {
                    byte[] data = new byte[stream.Length];
                    stream.Read(data, 0, data.Length);
                    stream.Close();
                    stream.Dispose();
                    if (StringPath.PathExists(fullFileName))
                        File.Delete(fullFileName);
                    using (var streamWriter = new FileStream(fullFileName, FileMode.CreateNew))
                    {
                        streamWriter.Write(data, 0, data.Length);
                    }
                }
                if (m_streamWriter != null)
                {
                    m_streamWriter.Flush();
                    m_streamWriter.Close();
                    m_streamWriter.Dispose();
                    m_streamWriter = null;
                }
            }
            _order = null;
            return fullFileName;
        }

        private static Stream DownloadViaRestful(string orderId, FileType fileType, out string extension)
        {
            extension = string.Empty;
            Stream stream = null;
            switch (fileType)
            {
                case FileType.Edf:
                    {
                        bool isSuccess = ApiRequest.Instance.DownloadEDF(new DownloadEDFRequestModel()
                        {
                            id = orderId
                        }, out ResponseModel responseModel,new StreamReceivedBack(SaveEdfCallBack));
                        if (isSuccess)
                        {
                            var model = responseModel as ResponseSuccessModel<DownloadEDFResponseModel>;
                            var fileModel = model.RestfulTable.File;
                            if (!fileModel.FileExtension.Equals("zip"))
                                throw new Exception("所下载的文件后缀名与指定的不一致");
                            else
                            {
                                stream = fileModel.Stream;
                                extension = fileModel.FileExtension;
                            } 
                        }
                        else
                            throw new Exception("请求EDF文件失败");
                    }
                    break;
                case FileType.Report:
                    {
                        bool isSuccess = ApiRequest.Instance.DownloadReport(new DownloadReportRequestModel()
                        {
                            id = orderId
                        }, out ResponseModel responseModel);
                        if (isSuccess)
                        {
                            var model = responseModel as ResponseSuccessModel<DownloadReportResponseModel>;
                            var fileModel = model.RestfulTable.File;
                            stream = fileModel.Stream;
                            extension = fileModel.FileExtension;
                        }
                        else
                            throw new Exception("请求报告文件失败");
                    }
                    break;
                default:
                    throw new NotSupportedException("不受支持的文件下载类型");
            }
            return stream;
        }
        /// <summary>
        /// 下载时数据流接收委托
        /// </summary>
        /// <param name="data"></param>
        /// <param name="streamPath"></param>
        private static bool SaveEdfCallBack(byte[] data, string streamPath, long datalength)
        {
            if (Interrupt)
                return false;
            if (m_streamWriter == null)
            {
                string fullFileName = string.Format("{0}.{1}", m_NewFilePath, streamPath);
                if (StringPath.PathExists(fullFileName))
                    File.Delete(fullFileName);
                m_streamWriter = new FileStream(fullFileName, FileMode.CreateNew);
            }
            m_streamWriter.Write(data, 0, data.Length);

            //计算比例
            m_hasdowndataindex += data.Length;
            double percentage = Math.Round((double)(m_hasdowndataindex / datalength), 4);
            bool hasdownsuccess = m_hasdowndataindex >= datalength;
            if (_order != null)
            {
                var inProgressArgs = new FileSliceDownloadResultEventArgs(hasdownsuccess, string.Empty, percentage, streamPath, _order);
                if (_downFileProgressIntChanged != null)
                    _downFileProgressIntChanged.Invoke(null, inProgressArgs);
            }
            return true;
        }
    }

    public enum FileType
    {
        Edf,
        Report
    }
}
