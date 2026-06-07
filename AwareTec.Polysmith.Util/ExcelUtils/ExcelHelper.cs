using System;
using System.Collections.Generic;
using System.Linq;
#region 导包
using AwareTec.Polysmith.Util.PathUtils;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.Util;
using System.Data;
#endregion

namespace AwareTec.Polysmith.Util.ExcelUtils
{
    public class ExcelHelper
    {
        #region 字段
        const string XLS_STR = "xls";
        const string XLSX_STR = "xlsx";
        #endregion

        #region 枚举
        /// <summary>
        /// EXCEL的类型
        /// </summary>
        enum ExcelType
        {
            Empty,
            Xls,
            Xlsx,
            Invalid
        }
        #endregion

        public static IWorkbook SetValues(string filePath,
                                          List<string> names,
                                          List<string> values)
        {
            IWorkbook workbook;
            if (!ReadFile(filePath, out workbook))
                throw new Exception("读取EXCEL文件失败");

            var dict = GetAllCellsByName(workbook);
            foreach (var item in dict)
            {
                int findIndex = -2;
                if ((findIndex = names.FindIndex(n => n.Equals(item.Key, StringComparison.OrdinalIgnoreCase))) == -1)
                    continue;

                item.Value.SetValue(values[findIndex]);
            }
            ReEvaluateFormula(workbook);
            return workbook;
        }

        public static void SaveExcel(string path, IWorkbook workbook)
        {
            if (StringPath.PathExists(path))
                File.Delete(path);

            string directoryName = Path.GetDirectoryName(path.ToString());
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            using (FileStream fs = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                workbook.Write(fs);
            }
        }

        /// <summary>
        /// 重新评估公式
        /// </summary>
        private static void ReEvaluateFormula(IWorkbook workbook)
        {
            IFormulaEvaluator evaluator = workbook.GetCreationHelper().CreateFormulaEvaluator();
            evaluator.EvaluateAll();
        }

        /// <summary>
        /// 获取工作簿中名称框引用
        /// </summary>
        /// <param name="workbook"></param>
        private static Dictionary<string, ExcelCell> GetAllCellsByName(IWorkbook workbook)
        {
            workbook.MissingCellPolicy = MissingCellPolicy.CREATE_NULL_AS_BLANK;

            Dictionary<string, ExcelCell> dict = new Dictionary<string, ExcelCell>();
            for (int nameIndex = 0; nameIndex < workbook.NumberOfNames; nameIndex++)
            {
                try
                {
                    IName name = workbook.GetNameAt(nameIndex);
                    AreaReference area = new AreaReference(name.RefersToFormula);
                    var cellRefList = area.GetAllReferencedCells();
                    var cellRefFirst = cellRefList.FirstOrDefault();
                    var cellFirst = workbook.GetSheet(name.SheetName).GetRow(cellRefFirst.Row).GetCell(cellRefFirst.Col);
                    ExcelCell cell = new ExcelCell()
                    {
                        WorkBook = workbook,
                        RefName = name.NameName,
                        SheetName = name.SheetName,
                        RowIndex = cellFirst.RowIndex,
                        ColIndex = cellFirst.ColumnIndex,
                        CellRefs = cellRefList.ToList()
                    };
                    dict.Add(name.NameName, cell);
                }
                catch { }
            }
            return dict;
        }

        /// <summary>
        ///  打开EXCEL文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="workBook"></param>
        /// <returns></returns>
        private static bool ReadFile(string filePath, out IWorkbook workBook)
        {
            workBook = null;
            if (!StringPath.PathExists(filePath))
                return false;

            ExcelType type;
            if (!CheckFileIsExcel(filePath, out type))
                return false;

            switch (type)
            {
                case ExcelType.Xls:
                    workBook = ReadXls(filePath);
                    break;
                case ExcelType.Xlsx:
                    workBook = ReadXlsx(filePath);
                    break;
            }
            return true;
        }

        /// <summary>
        /// 打开文件后缀名为xls的EXCEL文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static HSSFWorkbook ReadXls(string filePath)
        {
            try
            {
                HSSFWorkbook workbook = null;
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    workbook = new HSSFWorkbook(fs);
                return workbook;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 打开文件后缀名为xlsx的EXCEL文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static XSSFWorkbook ReadXlsx(string filePath)
        {
            try
            {
                XSSFWorkbook workbook = null;
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    workbook = new XSSFWorkbook(fs);
                return workbook;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 判断路径是否为合法的excel文件路径
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="type">判断文件为xls或为xlsx</param>
        /// <returns>
        /// 为excel文件则返回true，否则返回false
        /// </returns>
        /// <remarks>
        /// 合法即后缀名须为.xls 或 .xlsx
        /// </remarks>
        private static bool CheckFileIsExcel(string filePath, out ExcelType type)
        {
            var pathArr = filePath.Split('.');
            type = ExcelType.Empty;

            if (pathArr.Length < 1)
                return false;

            var fileExtension = pathArr.LastOrDefault();

            type = fileExtension.Equals(XLS_STR, StringComparison.OrdinalIgnoreCase) ?
                   ExcelType.Xls :
                   fileExtension.Equals(XLSX_STR, StringComparison.OrdinalIgnoreCase) ?
                   ExcelType.Xlsx :
                   ExcelType.Invalid;

            return type == ExcelType.Xls || type == ExcelType.Xlsx;
        }

        public static IWorkbook RecordSetValues(List<string> titlename, List<string> values)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            //创建工作表
            var sheet = workbook.CreateSheet("信息表");
            //创建标题行 从0行开始写入
            var titlerow = sheet.CreateRow(0);
            //创建标题行的单元格
            for (int i = 0; i < titlename.Count; i++)
            {
                var titlecell = titlerow.CreateCell(i);
                titlecell.SetCellType(CellType.String);
                titlecell.CellStyle.ShrinkToFit = true;
                titlecell.SetCellValue(titlename[i]);
                //自动适应列宽
                sheet.AutoSizeColumn(i);
            }
            //生成内容行
            int index = 1;//从第一行开始写
            for (int i = 0; i < values.Count; i++)
            {
                int rownumber = index + i / titlename.Count;
                var valusrowindex = sheet.CreateRow(rownumber);
                for (int j = 0; j < titlename.Count; j++)
                {
                    var valusrow = valusrowindex.CreateCell(j);
                    ExcelCell excelCell = new ExcelCell()
                    {
                        WorkBook = workbook,
                        SheetName = sheet.SheetName,
                        RowIndex = valusrow.RowIndex,
                        ColIndex = valusrow.ColumnIndex,
                    };
                    excelCell.SetValue(values[i]);
                    //自动适应列宽,只选择内容行的其中一行来自动适应
                    if (i < titlename.Count)
                    {
                        sheet.AutoSizeColumn(i);
                    }
                    i++;
                }
                i--;
            }
            return workbook;
        }

        /// <summary>
        /// Excel导入成DataTble
        /// </summary>
        /// <param name="file">导入路径(包含文件名与扩展名)</param>
        /// <returns></returns>
        public static DataTable ExcelToTable(string file)
        {
            DataTable dt = new DataTable();
            IWorkbook workbook;
            string fileExt = Path.GetExtension(file).ToLower();
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(fs); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(fs); } else { workbook = null; }
                if (workbook == null) { return null; }
                ISheet sheet = workbook.GetSheetAt(0);
                //表头  
                IRow header = sheet.GetRow(sheet.FirstRowNum);
                List<int> columns = new List<int>();
                for (int i = 0; i < header.LastCellNum; i++)
                {
                    object obj = GetValueType(header.GetCell(i));
                    if (obj == null || obj.ToString() == string.Empty)
                    {
                        dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                    }
                    else
                        dt.Columns.Add(new DataColumn(obj.ToString()));
                    columns.Add(i);
                }
                //数据  
                for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                {
                    DataRow dr = dt.NewRow();
                    bool hasValue = false;
                    foreach (int j in columns)
                    {
                        dr[j] = GetValueType(sheet.GetRow(i).GetCell(j));
                        if (dr[j] != null && dr[j].ToString() != string.Empty)
                        {
                            hasValue = true;
                        }
                    }
                    if (hasValue)
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }
        /// <summary>
        /// 获取单元格类型
        /// </summary>
        /// <param name="cell">目标单元格</param>
        /// <returns></returns>
        private static object GetValueType(ICell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.Blank:
                    return null;
                case CellType.Boolean:
                    return cell.BooleanCellValue;
                case CellType.Numeric:
                    return cell.NumericCellValue;
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Error:
                    return cell.ErrorCellValue;
                case CellType.Formula:
                default:
                    return "=" + cell.CellFormula;
            }
        }
    }
}
