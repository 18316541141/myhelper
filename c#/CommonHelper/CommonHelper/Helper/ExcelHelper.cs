using CommonHelper.Helper;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace CommonHelper.Helper
{
    /// <summary>
    /// excel表格的sheet属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ExcelSheet : Attribute
    {
        /// <summary>
        /// Sheet名称
        /// </summary>
        public string SheetName { set; get; }

        /// <summary>
        /// 组名称
        /// </summary>
        public string GroupName { set; get; }
    }

    /// <summary>
    /// excel表格的列名称属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ExcelCol : Attribute
    {
        /// <summary>
        /// 列名称
        /// </summary>
        public string ColName { set; get; }

        /// <summary>
        /// 列索引，从0开始
        /// </summary>
        public int ColIndex { set; get; }

        /// <summary>
        /// 组名称
        /// </summary>
        public string GroupName { set; get; }
    }

    /// <summary>
    /// excel列信息
    /// </summary>
    public class ExcelColInfo
    {
        /// <summary>
        /// 列特性
        /// </summary>
        public ExcelCol ExcelCol { set; get; }

        /// <summary>
        /// 列属性信息
        /// </summary>
        public PropertyInfo PropertyInfo { set; get; }
    }

    /// <summary>
    /// Excel帮助类
    /// </summary>
    public class ExcelHelper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataList"></param>
        /// <param name="outputStream"></param>
        /// <param name="groupName"></param>
        public static void ListToExcelXls<T>(List<T> dataList, Stream outputStream, string groupName = null)
        {
            ListToExcelXls(new List<T>[]{ dataList }, outputStream, groupName);
        }

        /// <summary>
        /// 导出xls结尾的Excel数据，每个实体必须含有ExcelColNameAttr
        /// </summary>
        /// <param name="dataList"></param>
        /// <param name="outputStream"></param>
        public static void ListToExcelXls<T>(List<T>[] dataListArrays, Stream outputStream,string groupName=null)
        {
            HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
            foreach (List<T> dataList in dataListArrays)
            {
                if(dataList==null || dataList.Count == 0)
                {
                    throw new Exception("数据列表为空，未能导出数据！");
                }
                List<ExcelColInfo> excelColInfoList= ColFilter(dataList[0].GetType(), groupName);
                if (excelColInfoList.Count == 0)
                {
                    throw new Exception("字段列表为空，不能导出数据！");
                }
                HSSFSheet hssfSheet = (HSSFSheet)hssfWorkbook.CreateSheet(GetSheetName(dataList[0].GetType(), groupName));
                HSSFRow hssfRow = (HSSFRow)hssfSheet.CreateRow(0);
                ExcelCol excelCol;
                foreach (ExcelColInfo excelColInfo in excelColInfoList)
                {
                    excelCol = excelColInfo.ExcelCol;
                    hssfRow.CreateCell(excelCol.ColIndex).SetCellValue(excelCol.ColName);
                }
                T temp;
                int[] colWidths=new int[excelColInfoList.Count];
                for (int i = 1,width, len = dataList.Count; i <= len; i++)
                {
                    temp = dataList[i];
                    hssfRow = (HSSFRow)hssfSheet.CreateRow(i);
                    foreach (ExcelColInfo excelColInfo in excelColInfoList)
                    {
                        excelCol = excelColInfo.ExcelCol;
                        SetCellValue(
                            (HSSFCell)hssfRow.CreateCell(excelCol.ColIndex), 
                            excelColInfo.PropertyInfo.GetValue(temp),
                            out width
                        );
                        if(width> colWidths[excelCol.ColIndex])
                        {
                            colWidths[excelCol.ColIndex] = width;
                        }
                    }
                }
                UpdateColWidth(hssfSheet, colWidths);
            }
            hssfWorkbook.Write(outputStream);
        }

        /// <summary>
        /// 设置列宽
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="colWidths"></param>
        static void UpdateColWidth(ISheet sheet, int[] colWidths)
        {
            for (var i = 0; i < colWidths.Length; i++)
            {
                sheet.SetColumnWidth(i, ((colWidths[i] > 254 ? 254 : colWidths[i]) + 1) * 256);
            }
        }

        /// <summary>
        /// 获取sheet名称
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        static string GetSheetName(Type type, string groupName=null)
        {
            foreach (ExcelSheet excelSheet in type.GetCustomAttributes(typeof(ExcelSheet)))
            {
                if (groupName==null)
                {
                    return excelSheet.SheetName;
                }
                else
                {
                    if (excelSheet.GroupName == groupName)
                    {
                        return excelSheet.SheetName;
                    }
                }
            }
            return "sheet"+PasswordHelper.RandomPassword(6,1);
        }

        static void SetPropValue(object obj, ICell cell, PropertyInfo prop)
        {
            CellType cellType = cell.CellType;
            if (cellType == CellType.Boolean)
            {
                prop.SetValue(obj, cell.BooleanCellValue);
            }
            else if (cellType == CellType.Numeric)
            {
                Type type = prop.PropertyType;
                if(type == typeof(double) || type == typeof(double?))
                {
                    prop.SetValue(obj, cell.NumericCellValue);
                }
                else if (type == typeof(float) || type == typeof(float?))
                {
                    prop.SetValue(obj, Convert.ToSingle(cell.NumericCellValue));
                }
                else if (type == typeof(string))
                {
                    prop.SetValue(obj, Convert.ToString(cell.NumericCellValue));
                }
                else if (type == typeof(int) || type == typeof(int?))
                {
                    prop.SetValue(obj, Convert.ToInt32(cell.NumericCellValue));
                }
                else if (type == typeof(long) || type == typeof(long?))
                {
                    prop.SetValue(obj, Convert.ToInt64(cell.NumericCellValue));
                }
                else if (type == typeof(short) || type == typeof(short?))
                {
                    prop.SetValue(obj, Convert.ToInt16(cell.NumericCellValue));
                }
            }
            else if (cellType == CellType.String)
            {
                prop.SetValue(obj, cell.StringCellValue);
            }
            else
            {
                prop.SetValue(obj, null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="val"></param>
        /// <param name="width"></param>
        static void SetCellValue(ICell cell, object val,out int width)
        {
            width = 0;
            if (val != null)
            {
                Type type=val.GetType();
                string cellString;
                if (type == typeof(double))
                {
                    cell.SetCellValue(Convert.ToDouble(val));
                    cellString = Convert.ToString(cell.NumericCellValue);
                }
                else if (type == typeof(int))
                {
                    cell.SetCellValue(Convert.ToInt32(val));
                    cellString = Convert.ToString(cell.NumericCellValue);
                }
                else if (val.GetType() == typeof(DateTime))
                {
                    cell.SetCellValue(Convert.ToDateTime(val));
                    cellString = Convert.ToString(cell.DateCellValue);
                }
                else
                {
                    cell.SetCellValue(Convert.ToString(val));
                    cellString = Convert.ToString(cell.StringCellValue);
                }
                width = Encoding.GetEncoding(936).GetBytes(cellString).Length;
            }
        }

        /// <summary>
        /// 列过滤，只返回有效列
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        static List<ExcelColInfo> ColFilter(Type type, string groupName=null)
        {
            List<ExcelColInfo> excelColInfoList = new List<ExcelColInfo>();
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                foreach (ExcelCol excelCol in propertyInfo.GetCustomAttributes(typeof(ExcelCol), true))
                {
                    if (groupName == null)
                    {
                        excelColInfoList.Add(new ExcelColInfo
                        {
                            PropertyInfo= propertyInfo,
                            ExcelCol= excelCol
                        });
                        break;
                    }
                    else
                    {
                        if (groupName == excelCol.GroupName)
                        {
                            excelColInfoList.Add(new ExcelColInfo
                            {
                                PropertyInfo = propertyInfo,
                                ExcelCol = excelCol
                            });
                            break;
                        }
                    }
                }
            }
            excelColInfoList.Sort((x,y)=> x.ExcelCol.ColIndex - y.ExcelCol.ColIndex);
            return excelColInfoList;
        }


        public static void ListToExcelXlsx<T>(List<T> dataList, Stream outputStream, string groupName = null)
        {
            ListToExcelXlsx(new List<T>[]{ dataList }, outputStream, groupName);
        }

        public static List<T> ExcelXlsToList<T>(Stream inputStream, string groupName = null)
        {
            List<T> ret = new List<T>();
            Type type = typeof(T);
            string sheetName = GetSheetName(type, groupName);
            List<ExcelColInfo> excelColInfoList = ColFilter(type, groupName);
            if (excelColInfoList.Count == 0)
            {
                throw new Exception("字段列表为空，不能导入数据！");
            }
            HSSFWorkbook hssfWorkbook = new HSSFWorkbook(inputStream);
            HSSFSheet hssfSheet = (HSSFSheet)hssfWorkbook.GetSheet(sheetName);
            HSSFRow hssfRow;
            T obj;
            for (int i = 1, len = hssfSheet.LastRowNum; i <= len; i++)
            {
                hssfRow = (HSSFRow)hssfSheet.GetRow(i);
                obj = (T)Activator.CreateInstance(type);
                foreach (ExcelColInfo excelColInfo in excelColInfoList)
                {
                    SetPropValue(obj, (HSSFCell)hssfRow.GetCell(excelColInfo.ExcelCol.ColIndex), excelColInfo.PropertyInfo);
                }
                ret.Add(obj);
            }
            return ret;
        }

        public static List<T> ExcelXlsxToList<T>(Stream inputStream, string groupName = null)
        {
            List<T> ret = new List<T>();
            Type type=typeof(T);
            string sheetName=GetSheetName(type, groupName);
            List<ExcelColInfo> excelColInfoList = ColFilter(type, groupName);
            if (excelColInfoList.Count == 0)
            {
                throw new Exception("字段列表为空，不能导入数据！");
            }
            XSSFWorkbook xssfWorkbook = new XSSFWorkbook(inputStream);
            XSSFSheet xssfSheet=(XSSFSheet)xssfWorkbook.GetSheet(sheetName);
            XSSFRow xssfRow;
            T obj;
            for (int i=1,len= xssfSheet.LastRowNum;i<=len ;i++)
            {
                xssfRow = (XSSFRow)xssfSheet.GetRow(i);
                obj=(T)Activator.CreateInstance(type);
                foreach (ExcelColInfo excelColInfo in excelColInfoList)
                {
                    SetPropValue(obj, (XSSFCell)xssfRow.GetCell(excelColInfo.ExcelCol.ColIndex), excelColInfo.PropertyInfo);
                }
                ret.Add(obj);
            }
            return ret;
        }

        /// <summary>
        /// 导出xls结尾的Excel数据，每个实体必须含有ExcelColNameAttr
        /// </summary>
        /// <param name="dataList"></param>
        /// <param name="outputStream"></param>
        public static void ListToExcelXlsx<T>(List<T>[] dataListArrays, Stream outputStream, string groupName = null)
        {
            XSSFWorkbook xssfWorkbook = new XSSFWorkbook();
            foreach (List<T> dataList in dataListArrays)
            {
                if (dataList == null || dataList.Count == 0)
                {
                    throw new Exception("数据列表为空，未能导出数据！");
                }
                List<ExcelColInfo> excelColInfoList = ColFilter(dataList[0].GetType(), groupName);
                if (excelColInfoList.Count == 0)
                {
                    throw new Exception("字段列表为空，不能导出数据！");
                }
                XSSFSheet xssfSheet = (XSSFSheet)xssfWorkbook.CreateSheet(GetSheetName(dataList[0].GetType(),groupName));
                XSSFRow xssfRow = (XSSFRow)xssfSheet.CreateRow(0);
                ExcelCol excelCol;
                foreach (ExcelColInfo excelColInfo in excelColInfoList)
                {
                    excelCol = excelColInfo.ExcelCol;
                    xssfRow.CreateCell(excelCol.ColIndex).SetCellValue(excelCol.ColName);
                }
                object temp;
                int[] colWidths = new int[excelColInfoList.Count];
                for (int i = 0, width, len = dataList.Count; i < len; i++)
                {
                    temp = dataList[i];
                    xssfRow = (XSSFRow)xssfSheet.CreateRow(i + 1);
                    foreach (ExcelColInfo excelColInfo in excelColInfoList)
                    {
                        excelCol=excelColInfo.ExcelCol;
                        SetCellValue(
                            (XSSFCell)xssfRow.CreateCell(excelCol.ColIndex),
                            excelColInfo.PropertyInfo.GetValue(temp),
                            out width
                        );
                        if (width > colWidths[excelCol.ColIndex])
                        {
                            colWidths[excelCol.ColIndex] = width;
                        }
                    }
                }
                UpdateColWidth(xssfSheet, colWidths);
            }
            xssfWorkbook.Write(outputStream);
        }
    }
}