namespace AwareTec.Polysmith.DataCenter
{
    /// <summary>
    /// 分析文件的信息类
    /// </summary>
    internal class FileDataInfo
    {
        /// <summary>
        /// 当前文件信息ID（与数据存储ID一致）
        /// </summary>
        public string ID { set; get; }
        /// <summary>
        /// 通讯ID
        /// </summary>
        public string CommunicationID { set; get; }
        /// <summary>
        /// 源文件路径
        /// </summary>
        public string FileName { set; get; }

        /// <summary>
        /// 有效的结束帧索引
        /// </summary>
        public int ValidEndIndex { set; get; }
        /// <summary>
        /// 是否为自动分析
        /// </summary>
        public bool IsAutoAnalysis { set; get; }
        /// <summary>
        /// 是否需要重新编辑文件
        /// </summary>
        public bool IsReEditFile { set; get; }
        /// <summary>
        /// 是否为首次加载
        /// </summary>
        public bool IsFirstLoad { set; get; }
        public FileDataInfo()
        {
            IsReEditFile = false;
            IsAutoAnalysis = false;
            ValidStartIndex = -1;
            ValidEndIndex = -1;
        }
        /// <summary>
        /// 有效的开始帧索引
        /// </summary>
        public int ValidStartIndex { get; set; }
    }
}
