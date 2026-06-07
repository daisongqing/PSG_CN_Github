using RestfulWebRequest.ApiCall;
using RestfulWebRequest.RestfulModel;
using RestfulWebRequest.RestfulTable.RestfulRequestTable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.ViewHelpers
{
    /// <summary>
    /// 上传报告帮助类
    /// </summary>
    public class UploadReportHelper
    {
        public static bool Upload(string reportPath, string orderId)
        {
            if (!File.Exists(reportPath))
                throw new FileNotFoundException("请求上传的报告不存在");

            string fileName = Path.GetFileNameWithoutExtension(reportPath) +
                              Path.GetExtension(reportPath);
            using (var stream = new FileStream(reportPath, FileMode.Open, FileAccess.Read))
            {
                bool isSuccess = ApiRequest.Instance.UploadReport(new UploadReportRequestModel()
                {
                    id = orderId,
                    File = stream,
                    fileName = fileName
                }, out ResponseModel responseModel);
                return isSuccess;
            }
            return false;
        }
    }
}
