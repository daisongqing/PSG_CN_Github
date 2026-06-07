using AwareTec.Polysmith.Cloud.UI.Forms;
using AwareTec.Polysmith.Util.PathUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.ViewHelpers
{
    public class ShowFileByExtensionHelper
    {
        public static void Show(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) ||
                (!StringPath.PathExists(filePath)))
                throw new ArgumentException("文件路径有误");

            var fileInfo = new FileInfo(filePath);
            switch (fileInfo.Extension)
            {
                case ".xls":
                case ".xlsx":
                case ".csv":
                    System.Diagnostics.Process.Start(filePath);
                    break;
                case ".pdf":
                    var dialog = new PdfViewDialog
                    {
                        Text = "报告预览",
                        Encryption = false,
                        PdfPath = filePath
                    };
                    dialog.ShowDialog();
                    break;
                case ".doc":
                case ".docx":
                    System.Diagnostics.Process.Start(filePath);
                    break;
                default:
                    throw  new NotSupportedException("该文件格式暂不支持预览");
            }
        }
    }
}
