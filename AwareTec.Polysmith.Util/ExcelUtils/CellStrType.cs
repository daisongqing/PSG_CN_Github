namespace AwareTec.Polysmith.Util.ExcelUtils
{
    public enum CellStrType
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = -1,
        /// <summary>
        /// 错误
        /// </summary>
        Error = 0,
        /// <summary>
        /// 空单元格
        /// </summary>
        Blank = 1,
        /// <summary>
        /// 整型
        /// </summary>
        Int = 2,
        /// <summary>
        /// 浮点型
        /// </summary>
        Double = 3,
        /// <summary>
        /// 字符串
        /// </summary>
        String = 4,
        /// <summary>
        /// 布尔
        /// </summary>
        Boolean = 5,
        /// <summary>
        /// 图片
        /// </summary>
        Picture = 6,
        /// <summary>
        /// 日期
        /// </summary>
        Date = 7,
    }
}
