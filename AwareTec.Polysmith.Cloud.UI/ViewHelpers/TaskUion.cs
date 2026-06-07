using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareTec.Polysmith.Cloud.UI.ViewHelpers
{
    /// <summary>
    /// 下载/上传任务单元
    /// </summary>
    public class TaskUion
    {
        public string sourceFilePath = "";

        public string OrderID = "";

        public float PercentValue = 0;

        public int rowIndex = -1;

        public int PageIndex = 0;

        public bool upLoad = true;

        public int compelet = 0;///0-等待 1-执行中 2-完成

        public string showValue = "";

        public DateTime RecordTime = default(DateTime);

        public bool cancel = false;

        public string LocalZipPath = "";

        public string FileName = "";

        public bool ProgressEnable = false;

        public bool pasue = false;

        public object Tag = new object();
    }
}
