using AwareTec.Polysmith.Util.PathUtils;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace AwareTec.Polysmith.Util.ExcelUtils
{
    internal class ExcelCell
    {
        #region 私有字段
        private IWorkbook _workbook = null;
        private string _refName = string.Empty;
        private string _sheetName = string.Empty;
        private int _rowIndex = -1;
        private int _colIndex = -1;
        private List<CellReference> _cellrefs = null;
        #endregion

        #region 内部属性
        private int ColRefsCount
        {
            get => _cellrefs.LastOrDefault().Col - _cellrefs.FirstOrDefault().Col + 1;
        }

        private int RowRefsCount
        {
            get => _cellrefs.LastOrDefault().Row - _cellrefs.FirstOrDefault().Row + 1;
        }
        #endregion

        #region 外部属性
        internal IWorkbook WorkBook
        {
            get => _workbook;
            set
            {
                if (value != null)
                    _workbook = value;
                else
                    throw new Exception("ExcelCell的WorkBook属性赋值有误");
            }
        }
        internal string RefName
        {
            get => _refName;
            set
            {
                if (value != null)
                    _refName = value;
                else
                    throw new Exception("ExcelCell的RefName属性赋值有误");
            }
        }
        internal string SheetName
        {
            get => _sheetName;
            set
            {
                if (value != null)
                    _sheetName = value;
                else
                    throw new Exception("ExcelCell的SheetName属性赋值有误");
            }
        }
        internal int RowIndex
        {
            get => _rowIndex;
            set => _rowIndex = value;
        }
        internal int ColIndex
        {
            get => _colIndex;
            set => _colIndex = value;
        }
        internal List<CellReference> CellRefs
        {
            get => _cellrefs;
            set
            {
                if (value != null)
                    _cellrefs = value;
                else
                    throw new Exception("ExcelCell的CellRefs属性赋值有误");
            }
        }
        #endregion

        #region 默认写入格式
        private const string DOUBLE_FORMAT = "0.00";
        private const string INT_FORMAT = "0";
        private const string DATE_FORMAT = "yyyy/mm/dd;@";
        private const string GENERAL = "General";
        private const string PHOTO = "Photo";
        #endregion

        #region 默认能够转换的日期格式
        private static List<string> _dateFormats = new List<string>();

        private static string[] DateFormats
        {
            get
            {
                if (_dateFormats.Count != 0)
                    return _dateFormats.ToArray();

                _dateFormats.Add("yyyy/MM/dd hh:mm:ss");
                _dateFormats.Add("yyyy/MM/dd h:m:s");
                _dateFormats.Add("yyyy/MM/dd hh:mm:s");
                _dateFormats.Add("yyyy/MM/dd h:mm:s");
                _dateFormats.Add("yyyy/MM/dd hh:m:s");
                _dateFormats.Add("yyyy/MM/dd hh:m:ss");
                _dateFormats.Add("yyyy/MM/dd h:mm:ss");
                _dateFormats.Add("yyyy/MM/dd h:m:ss");

                _dateFormats.Add("yyyy/M/d hh:mm:ss");
                _dateFormats.Add("yyyy/M/d h:m:s");
                _dateFormats.Add("yyyy/M/d hh:mm:s");
                _dateFormats.Add("yyyy/M/d h:mm:s");
                _dateFormats.Add("yyyy/M/d hh:m:s");
                _dateFormats.Add("yyyy/M/d hh:m:ss");
                _dateFormats.Add("yyyy/M/d h:mm:ss");
                _dateFormats.Add("yyyy/M/d h:m:ss");

                _dateFormats.Add("yyyy/MM/d hh:mm:ss");
                _dateFormats.Add("yyyy/MM/d h:m:s");
                _dateFormats.Add("yyyy/MM/d hh:mm:s");
                _dateFormats.Add("yyyy/MM/d h:mm:s");
                _dateFormats.Add("yyyy/MM/d hh:m:s");
                _dateFormats.Add("yyyy/MM/d hh:m:ss");
                _dateFormats.Add("yyyy/MM/d h:mm:ss");
                _dateFormats.Add("yyyy/MM/d h:m:ss");

                _dateFormats.Add("yyyy/M/dd hh:mm:ss");
                _dateFormats.Add("yyyy/M/dd h:m:s");
                _dateFormats.Add("yyyy/M/dd hh:mm:s");
                _dateFormats.Add("yyyy/M/dd h:mm:s");
                _dateFormats.Add("yyyy/M/dd hh:m:s");
                _dateFormats.Add("yyyy/M/dd hh:m:ss");
                _dateFormats.Add("yyyy/M/dd h:mm:ss");
                _dateFormats.Add("yyyy/M/dd h:m:ss");
                return _dateFormats.ToArray();
            }
        }
        #endregion

        /// <summary>
        /// 对该单元格设置对应类型的值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        internal void SetValue(string value)
        {
            if (_workbook == null ||
               string.IsNullOrWhiteSpace(_sheetName) ||
               _rowIndex == -1 ||
               _colIndex == -1
                )
                throw new Exception("单元格转换有问题");

            if (value == null)
                throw new Exception("EXCEL导出时的赋值单元格参数有误");

            try
            {
                var cell = _workbook.GetSheet(_sheetName).GetRow(_rowIndex).GetCell(_colIndex);
                object data = null;

                //初步判断: 根据传入值初步判断类型
                CellStrType type = CellStrType.Unknown;
                if ((int)(type = GetCellTypeByValue(value, out data)) < 1)
                    return;

                //二次判断: 根据Cell的名称框引用名称最终判断类型
                type = GetTypeByRefName(type);

                SetValuesByType(type, data);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 根据类型为Cell赋值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        private void SetValuesByType(CellStrType type, object value)
        {
            if ((int)type < 1)
                return;

            var cell = _workbook.GetSheet(_sheetName).GetRow(_rowIndex).GetCell(_colIndex);
            string originalFormat = cell.CellStyle.GetDataFormatString();
            short originalFormatShort = cell.CellStyle.DataFormat;
            switch (type)
            {
                case CellStrType.Blank:
                    cell.SetCellType(CellType.String);
                    cell.SetCellValue(string.Empty);
                    break;
                case CellStrType.Int:
                    cell.SetCellType(CellType.Numeric);
                    if (originalFormat.Equals(GENERAL, StringComparison.OrdinalIgnoreCase))
                    {
                        var newFormat = _workbook.CreateDataFormat();
                        var shortNewFormat = newFormat.GetFormat(INT_FORMAT);
                        cell.CellStyle.DataFormat = shortNewFormat;
                    }
                    cell.SetCellValue((int)value);
                    break;
                case CellStrType.Double:
                    cell.SetCellType(CellType.Numeric);

                    if (originalFormat.Equals(GENERAL, StringComparison.OrdinalIgnoreCase))
                    {
                        var newFormat = _workbook.CreateDataFormat();
                        var shortNewFormat = newFormat.GetFormat(DOUBLE_FORMAT);
                        cell.CellStyle.DataFormat = shortNewFormat;
                    }
                    if (double.IsNaN((double)value))
                    {
                        cell.SetCellValue(0);
                        return;
                    }
                    cell.SetCellValue((double)value);
                    break;
                case CellStrType.Date:
                    //if (originalFormat.Equals(GENERAL, StringComparison.OrdinalIgnoreCase))
                    //{
                    //    var newFormat = _workbook.CreateDataFormat();
                    //    var shortNewFormat = newFormat.GetFormat(DATE_FORMAT);
                    //    cell.CellStyle.DataFormat = shortNewFormat;
                    //}
                    //else
                    //    cell.CellStyle.DataFormat = _workbook.GetCreationHelper().CreateDataFormat().GetFormat(originalFormat);
                    cell.SetCellType(CellType.String);
                    if (originalFormat == "m/d/yy")
                        originalFormat = "yyyy-MM-dd";
                    else if (originalFormat.Contains(":") || originalFormat.Contains("分"))
                    {
                        originalFormat = "yyyy-MM-dd HH:mm:ss";
                    }
                    else if (originalFormat == "yyyy'年'月'd'日';@")
                    {
                        originalFormat = originalFormat.Replace(";", "").Replace("@", "").Replace("m", "M");
                    }
                    else if (originalFormat.Contains("yyyy-MM-dd"))
                    {
                        originalFormat = "yyyy-MM-dd";
                    }
                    else if (originalFormat.Contains("yyyy-mm-dd")||originalFormat== "yyyy\\-mm\\-dd;@")
                    {
                        originalFormat = "yyyy-mm-dd";
                    }
                    else
                    {
                        originalFormat = "yyyy-MM-dd HH:mm:ss";
                    }
                    cell.SetCellValue(Convert.ToDateTime(value).ToString(originalFormat));
                    break;
                case CellStrType.String:
                    cell.SetCellType(CellType.String);
                    cell.SetCellValue((string)value);
                    break;
                case CellStrType.Picture:
                    PictureType picType = PictureType.None;
                    if ((picType = GetPicTypeByFilePath((string)value)) == PictureType.None)
                        return;
                    byte[] bytes = System.IO.File.ReadAllBytes((string)value);
                    var helper = _workbook.GetCreationHelper();
                    var drawing = _workbook.GetSheet(_sheetName).CreateDrawingPatriarch();
                    var anchor = helper.CreateClientAnchor();
                    anchor.AnchorType = AnchorType.MoveAndResize;
                    anchor.Col1 = _colIndex;
                    anchor.Col2 = _colIndex + ColRefsCount;
                    anchor.Row1 = _rowIndex;
                    anchor.Row2 = _rowIndex + RowRefsCount;
                    var picIndex = _workbook.AddPicture(bytes, picType);
                    var pic = drawing.CreatePicture(anchor, picIndex);//创建图片实例
                    pic.Resize(1, 1);///把实例绘制到单元格上，图片会自动按照单元格大小进行缩放，scalex，scaley均要设置成1
                    break;
                case CellStrType.Boolean:
                    cell.SetCellType(CellType.Boolean);
                    cell.SetCellValue((bool)value);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// 根据名称框引用名称获取Cell类型
        /// </summary>
        /// <returns></returns>
        private CellStrType GetTypeByRefName(CellStrType type)
        {
            //只要引用名称存在"Photo"即为 图片类型
            if (_refName.Contains(PHOTO))
                return CellStrType.Picture;

            //将误判断的类型重置
            if (type == CellStrType.Picture)
                return CellStrType.String;

            return type;
        }

        /// <summary>
        /// 根据传入字符串判断字符串的类型
        /// </summary>
        /// <remarks>
        /// 浮点型、整型、日期： Numeric
        /// 图片: 若图片路径不存在，则自动为字符串类型
        /// 布尔型： Boolean
        /// 空串、伪空串：Blank
        /// 字符串：String
        /// 其他：Unknown
        /// 如果传入字符串为null，则为Error类型
        /// 不考虑公式情况
        /// </remarks>
        /// <param name="str"></param>
        /// <returns></returns>
        private CellStrType GetCellTypeByValue(string str, out object resultData)
        {
            resultData = null;
            if (str == null)
                return CellStrType.Error;

            if (string.IsNullOrWhiteSpace(str))
                return CellStrType.Blank;

            //是否为整型
            int intVal;
            if (Int32.TryParse(str, out intVal))
            {
                resultData = intVal;
                return CellStrType.Int;
            }

            //是否浮点型
            double doubleVal;
            if (double.TryParse(str, out doubleVal))
            {
                resultData = doubleVal;
                return CellStrType.Double;
            }

            //是否为日期
            DateTime datetimeVal;
            //DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
            //dtFormat.ShortDatePattern = "yyyy-MM-dd hh:mm:ss";
            if (DateTime.TryParse(str, out datetimeVal) && str.Contains("/"))
            //if (str.Contains("/") && DateTime.TryParse(str, dtFormat,DateTimeStyles.None, out datetimeVal))
            {
                resultData = datetimeVal;
                return CellStrType.Date;
            }

            //是否为图片路径
            if (GetPicTypeByFilePath(str) != PictureType.None)
            {
                resultData = str;
                return CellStrType.Picture;
            }

            //是否为布尔
            bool boolVal;
            if (bool.TryParse(str, out boolVal))
            {
                resultData = boolVal;
                return CellStrType.Boolean;
            }

            //否则最终即为字符串
            resultData = str;
            return CellStrType.String;
        }

        /// <summary>
        /// 根据文件路径获取图片类型
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>
        /// 如果不是图片或路径有问题，则返回PictureType.None
        /// </returns>
        private PictureType GetPicTypeByFilePath(string filePath)
        {
            string extension = StringPath.GetFileExtension(filePath);
            if (extension == string.Empty)
                return PictureType.None;

            //补充后缀名为.jpg
            if (extension.Equals("jpg", StringComparison.OrdinalIgnoreCase))
                return PictureType.JPEG;

            foreach (PictureType item in Enum.GetValues(typeof(PictureType)))
            {
                if ((int)item < 1)
                    continue;

                if (extension.Equals(item.ToString(), StringComparison.OrdinalIgnoreCase))
                    return item;
            }
            return PictureType.None;
        }

        #region Win32 API
        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr ptr);
        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(
       IntPtr hdc, // handle to DC  
       int nIndex // index of capability  
       );
        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);
        #endregion
        #region DeviceCaps常量
        const int HORZRES = 8;
        const int VERTRES = 10;
        const int LOGPIXELSX = 88;
        const int LOGPIXELSY = 90;
        const int DESKTOPVERTRES = 117;
        const int DESKTOPHORZRES = 118;
        #endregion  
        /// <summary>  
        /// 获取宽度缩放百分比  
        /// </summary>  
        private float ScaleX
        {
            get
            {
                IntPtr hdc = GetDC(IntPtr.Zero);
                float ScaleX = (float)GetDeviceCaps(hdc, DESKTOPHORZRES) / (float)GetDeviceCaps(hdc, HORZRES);
                ReleaseDC(IntPtr.Zero, hdc);
                return ScaleX;
            }
        }
        /// <summary>  
        /// 获取高度缩放百分比  
        /// </summary>  
        private static float ScaleY
        {
            get
            {
                IntPtr hdc = GetDC(IntPtr.Zero);
                float ScaleY = (float)(float)GetDeviceCaps(hdc, DESKTOPVERTRES) / (float)GetDeviceCaps(hdc, VERTRES);
                ReleaseDC(IntPtr.Zero, hdc);
                return ScaleY;
            }
        }
    }
}
