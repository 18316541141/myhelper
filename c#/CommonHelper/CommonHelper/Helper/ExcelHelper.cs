using CommonHelper.Helper;
using Google.Protobuf.Collections;
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
    /// excel表格数据格式化
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="inputData">输入指定内容</param>
    /// <returns>返回格式化后的字符串数据</returns>
    public delegate string ExcelDataFormat<T>(T inputData);

    /// <summary>
    /// excel表格的数据转化为实体类的数据
    /// </summary>
    /// <typeparam name="In">excel表格数据</typeparam>
    /// <typeparam name="Out">返回实体类数据</typeparam>
    /// <param name="input"></param>
    /// <returns></returns>
    public delegate Out DataFormatToEntity<Out,In>(In input) where In:ICell ;

    /// <summary>
    /// excel表格的列名称属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ExcelColAttr : Attribute
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
        public ExcelColAttr ExcelColAttr { set; get; }

        public PropertyInfo PropertyInfo { set; get; }
    }

    /// <summary>
    /// Excel帮助类
    /// </summary>
    public class ExcelHelper
    {


        /// <summary>
        /// 读取excel的数据到数据表对象
        /// </summary>
        /// <param name="filePath">excel的路径</param>
        /// <param name="sheetName">工作簿</param>
        /// <returns>返回数据表</returns>
        public static DataTable DoReadExcelDataTable(string filePath, string sheetName = null)
        {
            IWorkbook workbook = null;
            string fileExt = Path.GetExtension(filePath);

            #region 初始化信息

            try
            {

                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    //将文件流中模板加载到工作簿对象中
                    if (fileExt == ".xls")
                    {
                        workbook = new HSSFWorkbook(file);
                    }
                    else if (fileExt == ".xlsx")
                    {
                        workbook = new XSSFWorkbook(file);
                    }
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            #endregion

            ISheet sheet = null;

            if (!string.IsNullOrEmpty(sheetName)) sheet = workbook.GetSheet(sheetName);
            else sheet = workbook.GetSheetAt(0);

            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            DataTable dt = new DataTable();

            var firstRow = sheet.GetRow(0);
            //一行最后一个方格的编号 即总的列数  
            for (int j = 0; j < firstRow.LastCellNum; j++)
            {
                dt.Columns.Add(Convert.ToChar(((int)'A') + j).ToString());
            }

            bool isFirst = true;
            while (rows.MoveNext())
            {
                if (isFirst)
                {
                    isFirst = false;
                    continue;
                }
                IRow row = null;

                if (fileExt == ".xls")
                {
                    row = (HSSFRow)rows.Current;
                }
                else if (fileExt == ".xlsx")
                {
                    row = (XSSFRow)rows.Current;
                }

                DataRow dr = dt.NewRow();

                for (int i = 0; i < row.LastCellNum; i++)
                {
                    ICell cell = row.GetCell(i);

                    if (cell == null)
                    {
                        dr[i] = null;
                    }
                    else
                    {
                        if (fileExt == ".xls")
                        {
                            HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(workbook);
                            cell = e.EvaluateInCell(cell);
                            if (cell.CellType == CellType.Numeric)
                            {
                                //NPOI中数字和日期都是NUMERIC类型的，这里对其进行判断是否是日期类型
                                if (HSSFDateUtil.IsCellDateFormatted(cell)) //日期类型
                                {
                                    dr[i] = cell.DateCellValue;
                                }
                                else //其他数字类型
                                {
                                    dr[i] = cell.NumericCellValue;
                                }
                            }
                            else
                            {
                                dr[i] = cell.ToString();
                            }
                        }
                        else if (fileExt == ".xlsx")
                        {
                            XSSFFormulaEvaluator e = new XSSFFormulaEvaluator(workbook);
                            cell = e.EvaluateInCell(cell);
                            if (cell.CellType == CellType.Numeric)
                            {
                                //NPOI中数字和日期都是NUMERIC类型的，这里对其进行判断是否是日期类型
                                if (DateUtil.IsCellDateFormatted(cell)) //日期类型
                                {
                                    dr[i] = cell.DateCellValue;
                                }
                                else //其他数字类型
                                {
                                    dr[i] = cell.NumericCellValue;
                                }
                            }
                            else
                            {
                                dr[i] = cell.ToString();
                            }
                        }

                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// 获取需要导出的数据列属性
        /// </summary>
        /// <param name="columnInfo">列信息，属性名、列名</param>
        /// <returns>返回数据列属性</returns>
        static List<PropertyInfo> PropertyInfoList<T>(Dictionary<string, string> columnInfo)
        {
            var myType = typeof(T);
            var myPro = new List<PropertyInfo>();
            foreach (string key in columnInfo.Keys)
            {
                var p = myType.GetProperty(key);
                if (p == null) continue;
                myPro.Add(p);
            }
            return myPro;
        }

        /// <summary>
        /// 按照顺序返回导出的数据列属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnInfo">列信息，属性名、顺序</param>
        /// <returns></returns>
        static List<PropertyInfo> PropertyInfoList<T>(Dictionary<string, int> columnInfo)
        {
            var myType = typeof(T);
            var myPro = new List<PropertyInfo>(65535);
            foreach (KeyValuePair<string,int> keyVal in columnInfo)
            {
                var p = myType.GetProperty(keyVal.Key);
                if (p == null) continue;
                myPro[keyVal.Value] = p;
            }
            myPro.TrimExcess();
            return myPro;
        }

        

        /// <summary>
        /// 将数据列表生成csv文本
        /// </summary>
        /// <typeparam name="T">导出的数据类型</typeparam>
        /// <param name="objList">要导出的数据</param>
        /// <param name="columnInfo">导出的列属性名、列中文名</param>
        /// <param name="streamWriter">导出的内容输出的输出流</param>
        /// <returns></returns>
        public static void ListToCsv<T>(List<T> objList, Dictionary<string, string> columnInfo,StreamWriter streamWriter,Dictionary<string, ExcelDataFormat<dynamic>> dataFormatMap=null)
        {
            StringBuilder stringBuilder = new StringBuilder();
            //列分隔符
            string splitCol = "";
            var myPro = PropertyInfoList<T>(columnInfo);
            foreach (string key in columnInfo.Keys)
            {
                //列名
                var cName = columnInfo[key];
                stringBuilder.Append(splitCol).Append("\"").Append(cName.Replace(",", "\",\"").Replace("\"", "\"\"")).Append("\"");
                splitCol = ",";
            }
            stringBuilder.Append("\r\n");
            //如果没有找到可用的属性则结束 
            if (myPro.Count == 0)
            {
                return;
            }
            splitCol = "";
            object myProValue;
            string cellValue;
            foreach (var obj in objList)
            {
                foreach (var pro in myPro)
                {
                    myProValue = pro.GetValue(obj, null);
                    //取单元格的值
                    cellValue = (myProValue ?? "").ToString().Trim();
                    stringBuilder.Append(splitCol).Append("\"").Append("'").Append(
                        (dataFormatMap==null || !dataFormatMap.ContainsKey(pro.Name) ? cellValue:dataFormatMap[pro.Name](cellValue))
                        .Replace(",", "\",\"").Replace("\"", "\"\"")).Append("\"");
                    splitCol = ",";
                }
                stringBuilder.Append("\r\n");
                splitCol = "";
            }
            streamWriter.Write(stringBuilder.ToString());
        }

        /// <summary>
        /// 根据xls模板导出以xls结尾的Excel数据，
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objList">导出数据列表</param>
        /// <param name="columnInfo">数据列信息字典：属性名、属性摆放位置</param>
        /// <param name="templatePath">模板xls文件的本地路径</param>
        /// <param name="outputPath">导出数据输出到该本地路径</param>
        /// <param name="dataFormatMap">数据列格式化</param>
        public static void ListToExcelXlsWithTemplate<T>(List<T> objList, Dictionary<string, int> columnInfo, string templatePath, string outputPath, Dictionary<string, ExcelDataFormat<dynamic>> dataFormatMap = null)
        {
            using (Stream templateStream = File.OpenRead(templatePath))
            using (Stream outputStream = File.OpenWrite(outputPath))
                ListToExcelXlsWithTemplate<T>(objList, columnInfo, templateStream, outputStream, dataFormatMap);
        }

        /// <summary>
        /// 根据xls模板导出以xls结尾的Excel数据，
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objList">导出数据列表</param>
        /// <param name="columnInfo">数据列信息字典：属性名、属性摆放位置</param>
        /// <param name="templateStream">模板xls文件的输入流</param>
        /// <param name="outputStream">导出数据输出到该输出流</param>
        /// <param name="dataFormatMap">数据列格式化</param>
        public static void ListToExcelXlsWithTemplate<T>(List<T> objList, Dictionary<string, int> columnInfo,Stream templateStream, Stream outputStream, Dictionary<string, ExcelDataFormat<dynamic>> dataFormatMap = null)
        {
            if (columnInfo.Count == 0)
                throw new Exception("字段列表为空，不能导出数据！");
            if (objList.Count == 0)
                throw new Exception("数据列表为空，未能导出数据！");
            var book = new HSSFWorkbook(templateStream);
            //添加一个sheet  
            var sheet1 = book.GetSheetAt(0);
            //列宽暂存变量
            var columnWidths = new int[columnInfo.Keys.Count];
            var myPro = PropertyInfoList<T>(columnInfo);
            //如果没有找到可用的属性则结束 
            if (myPro.Count == 0)
                return;
            Encoding e936 = Encoding.GetEncoding(936);


            //将列表数据写入Excel文件对象
            for (var i = 0; i < objList.Count; i++)
            {
                //每一行创建一次Excel的行对象
                var tempRow = (HSSFRow)sheet1.CreateRow(i + 1);
                for (var j = 0; j < myPro.Count; j++)
                    if (myPro[j] != null)
                    {
                        var myProValue = myPro[j].GetValue(objList[i], null);
                        if (myProValue != null)
                        {
                            //取单元格的值
                            var cellValue = myProValue.ToString().Trim();

                            //当前内容单元格的宽度
                            var valueLength = e936.GetBytes(cellValue).Length;
                            //取列宽
                            if (columnWidths[j] < valueLength) columnWidths[j] = valueLength;

                            //写单元格的内容
                            tempRow.CreateCell(j).SetCellValue(dataFormatMap == null || !dataFormatMap.ContainsKey(myPro[j].Name) ? cellValue : dataFormatMap[myPro[j].Name](cellValue));
                        }
                    }
            }
            //调整自动列宽
            for (var i = 0; i < columnInfo.Keys.Count; i++)
                if(columnWidths[i]>0)
                    //设置列宽
                    sheet1.SetColumnWidth(i, ((columnWidths[i] > 254 ? 254 : columnWidths[i]) + 1) * 256);
            //取出文件内容
            book.Write(outputStream);
        }

        /// <summary>
        /// 导出以xls结尾的Excel数据，
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objList">导出数据列表</param>
        /// <param name="columnInfo">数据列信息字典</param>
        /// <param name="outputPath">导出xls输出的本地路径</param>
        /// <param name="dataFormatMap">列格式化</param>
        public static void ListToExcelXls<T>(List<T> objList, Dictionary<string, string> columnInfo, string outputPath, Dictionary<string, ExcelDataFormat<dynamic>> dataFormatMap = null)
        {
            using (Stream outputStream = File.OpenWrite(outputPath))
                ListToExcelXls<T>(objList, columnInfo, outputStream, dataFormatMap);
        }

        /// <summary>
        /// 把excel转化为特定实体类型的集合
        /// </summary>
        /// <typeparam name="T">特定实体类型</typeparam>
        /// <param name="filePath">excel的保存路径</param>
        /// <param name="sheetName">工作簿名称</param>
        /// <param name="columnInfo">提取的属性名称和对应的列</param>
        /// <param name="dataFormatToEntityMap">excel单元格的数据转换器</param>
        /// <returns>返回实体类型集合</returns>
        public static List<T> ExcelXlsToList<T>(string filePath, string sheetName, Dictionary<string, int> columnInfo, Dictionary<string, DataFormatToEntity<dynamic, HSSFCell>> dataFormatToEntityMap = null)
        {
            using (Stream stream = File.OpenRead(filePath))
               return ExcelXlsToList<T>(stream, sheetName, columnInfo, dataFormatToEntityMap);
        }

        /// <summary>
        /// 把excel转化为特定实体类型的集合
        /// </summary>
        /// <typeparam name="T">特定实体类型</typeparam>
        /// <param name="stream">excel的流</param>
        /// <param name="sheetName">工作簿名称</param>
        /// <param name="columnInfo">提取的属性名称和对应的列</param>
        /// <param name="dataFormatToEntityMap">excel单元格的数据转换器</param>
        /// <returns>返回实体类型集合</returns>
        public static List<T> ExcelXlsToList<T>(Stream stream, string sheetName, Dictionary<string, int> columnInfo, Dictionary<string, DataFormatToEntity<dynamic,HSSFCell>> dataFormatToEntityMap=null)
        {
            List<T> retList = new List<T>();
            Type type = typeof(T);
            ConstructorInfo constructorInfo = type.GetConstructor(Type.EmptyTypes);
            var book = new HSSFWorkbook(stream);
            var sheet1 = book.GetSheet(sheetName);
            var myPro = PropertyInfoList<T>(columnInfo);
            IEnumerator it = sheet1.GetRowEnumerator();
            it.MoveNext();
            while(it.MoveNext())
            {
                object obj = constructorInfo.Invoke(null);
                HSSFRow ros=(HSSFRow)it.Current;
                for (int i=0,len= myPro.Count;i<len;i++)
                    if (dataFormatToEntityMap.ContainsKey(myPro[i].Name))
                        type.GetField(myPro[i].Name).SetValue(obj, dataFormatToEntityMap[myPro[i].Name]((HSSFCell)ros.GetCell(i)));
                    else
                        type.GetField(myPro[i].Name).SetValue(obj, ((HSSFCell)ros.GetCell(i)).StringCellValue);
                retList.Add((T)obj);
            }
            return retList;
        }

        /// <summary>
        /// 把excel转化为特定实体类型的集合
        /// </summary>
        /// <typeparam name="T">特定实体类型</typeparam>
        /// <param name="filePath">excel的保存路径</param>
        /// <param name="sheetName">工作簿名称</param>
        /// <param name="columnInfo">提取的属性名称和对应的列</param>
        /// <param name="dataFormatToEntityMap">excel单元格的数据转换器</param>
        /// <returns>返回实体类型集合</returns>
        public static List<T> ExcelXlsxToList<T>(string filePath, string sheetName, Dictionary<string, int> columnInfo, Dictionary<string, DataFormatToEntity<dynamic, XSSFCell>> dataFormatToEntityMap = null)
        {
            using(Stream stream=File.OpenRead(filePath))
                return ExcelXlsxToList<T>(stream, sheetName, columnInfo, dataFormatToEntityMap);
        }

        /// <summary>
        /// 把excel转化为特定实体类型的集合
        /// </summary>
        /// <typeparam name="T">特定实体类型</typeparam>
        /// <param name="stream">excel的流</param>
        /// <param name="sheetName">工作簿名称</param>
        /// <param name="columnInfo">提取的属性名称和对应的列</param>
        /// <param name="dataFormatToEntityMap">excel单元格的数据转换器</param>
        /// <returns>返回实体类型集合</returns>
        public static List<T> ExcelXlsxToList<T>(Stream stream, string sheetName, Dictionary<string, int> columnInfo, Dictionary<string, DataFormatToEntity<dynamic, XSSFCell>> dataFormatToEntityMap = null)
        {
            List<T> retList = new List<T>();
            Type type = typeof(T);
            ConstructorInfo constructorInfo = type.GetConstructor(Type.EmptyTypes);
            var book = new XSSFWorkbook(stream);
            var sheet1 = book.GetSheet(sheetName);
            var myPro = PropertyInfoList<T>(columnInfo);
            IEnumerator it = sheet1.GetRowEnumerator();
            it.MoveNext();
            while (it.MoveNext())
            {
                object obj = constructorInfo.Invoke(null);
                XSSFRow ros = (XSSFRow)it.Current;
                for (int i = 0, len = myPro.Count; i < len; i++)
                    if (dataFormatToEntityMap.ContainsKey(myPro[i].Name))
                        type.GetField(myPro[i].Name).SetValue(obj, dataFormatToEntityMap[myPro[i].Name]((XSSFCell)ros.GetCell(i)));
                    else
                        type.GetField(myPro[i].Name).SetValue(obj, ((XSSFCell)ros.GetCell(i)).StringCellValue);
                retList.Add((T)obj);
            }
            return retList;
        }

        /// <summary>
        /// 导出xls结尾的Excel数据，每个实体必须含有ExcelColNameAttr
        /// </summary>
        /// <param name="dataList"></param>
        /// <param name="outputStream"></param>
        public static void ListToExcelXls(List<object> dataList, Stream outputStream,string groupName=null)
        {
            if(dataList==null || dataList.Count == 0)
            {
                throw new Exception("数据列表为空，未能导出数据！");
            }
            List<ExcelColInfo> excelColInfoList= ColFilter(dataList[0], groupName);
            HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
            HSSFSheet hssfSheet = (HSSFSheet)hssfWorkbook.CreateSheet("Sheet1");
            HSSFRow hssfRow = (HSSFRow)hssfSheet.CreateRow(0);
            ExcelColAttr excelColAttr;
            foreach (ExcelColInfo excelColInfo in excelColInfoList)
            {
                excelColAttr = excelColInfo.ExcelColAttr;
                hssfRow.CreateCell(excelColAttr.ColIndex).SetCellValue(excelColAttr.ColName);
            }
            object temp;
            object val;
            PropertyInfo propertyInfo;
            HSSFCell hssfCell;
            for (int i = 1, len = dataList.Count; i <= len; i++)
            {
                temp = dataList[i];
                hssfRow = (HSSFRow)hssfSheet.CreateRow(i);
                foreach (ExcelColInfo excelColInfo in excelColInfoList)
                {
                    excelColAttr = excelColInfo.ExcelColAttr;
                    propertyInfo = excelColInfo.PropertyInfo;
                    val = propertyInfo.GetValue(temp);
                    hssfCell =(HSSFCell) hssfRow.CreateCell(excelColAttr.ColIndex);
                    if (val.GetType() == typeof(double))
                    {
                        hssfCell.SetCellValue(Convert.ToDouble(val));
                    }
                    else if (val.GetType() == typeof(int))
                    {
                        hssfCell.SetCellValue(Convert.ToInt32(val));
                    }
                    else if (val.GetType() == typeof(string))
                    {
                        hssfCell.SetCellValue(Convert.ToString(val));
                    }
                    else if (val.GetType() == typeof(DateTime))
                    {
                        hssfCell.SetCellValue(Convert.ToDateTime(val));
                    }
                }
            }
        }

        /// <summary>
        /// 列过滤，只返回有效列
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        static List<ExcelColInfo> ColFilter(object obj,string groupName=null)
        {
            List<ExcelColInfo> excelColInfoList = new List<ExcelColInfo>();
            Type type = obj.GetType();
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                foreach (Attribute attr in propertyInfo.GetCustomAttributes(false))
                {
                    if (attr.GetType() == typeof(ExcelColNameAttr))
                    {
                        ExcelColNameAttr excelColNameAttr = (ExcelColNameAttr)attr;
                        if (groupName == null)
                        {
                            excelColInfoList.Add(new ExcelColInfo
                            {
                                PropertyInfo= propertyInfo,
                                ExcelColNameAttr=excelColNameAttr
                            });
                            break;
                        }
                        else
                        {
                            if (groupName == excelColNameAttr.GroupName)
                            {
                                excelColInfoList.Add(new ExcelColInfo
                                {
                                    PropertyInfo = propertyInfo,
                                    ExcelColNameAttr = excelColNameAttr
                                });
                                break;
                            }
                        }
                    }
                }
            }
            excelColInfoList.Sort((x,y)=> x.ExcelColNameAttr.ColIndex - y.ExcelColNameAttr.ColIndex);
            return excelColInfoList;
        }

        /// <summary>
        /// 导出以xls结尾的Excel数据，
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objList">导出数据列表</param>
        /// <param name="columnInfo">数据列信息字典</param>
        /// <param name="outputStream">导出xls的输出流</param>
        /// <param name="dataFormatMap">列格式化</param>
        public static void ListToExcelXls<T>(List<T> objList, Dictionary<string, string> columnInfo, Stream outputStream, Dictionary<string, ExcelDataFormat<dynamic>> dataFormatMap = null)
        {
            if (columnInfo.Count == 0)
                throw new Exception("字段列表为空，不能导出数据！");
            if (objList.Count == 0)
                throw new Exception("数据列表为空，未能导出数据！");
            var book = new HSSFWorkbook();
            //添加一个sheet  
            var sheet1 = book.CreateSheet("Sheet1");
            //给sheet1添加第一行的头部标题  
            var row1 = (HSSFRow)sheet1.CreateRow(0);
            //列宽暂存变量
            var columnWidths = new int[columnInfo.Keys.Count];
            var myPro = PropertyInfoList<T>(columnInfo);
            //如果没有找到可用的属性则结束 
            if (myPro.Count == 0)
                return;
            Encoding e936 = Encoding.GetEncoding(936);
            for (var i = 0; i < columnInfo.Keys.Count; i++)
            {
                var keyName = columnInfo.Keys.ElementAt(i);
                //列名
                var cName = columnInfo[keyName];
                //列宽
                columnWidths[i] = e936.GetBytes(cName).Length;
                row1.CreateCell(i).SetCellValue(columnInfo[keyName]);
            }


            //将列表数据写入Excel文件对象
            for (var i = 0; i < objList.Count; i++)
            {
                //每一行创建一次Excel的行对象
                var tempRow = (HSSFRow)sheet1.CreateRow(i + 1);
                for (var j = 0; j < myPro.Count; j++)
                {
                    var myProValue = myPro[j].GetValue(objList[i], null);
                    if (myProValue != null)
                    {
                        //取单元格的值
                        var cellValue = myProValue.ToString().Trim();

                        //当前内容单元格的宽度
                        var valueLength = e936.GetBytes(cellValue).Length;
                        //取列宽
                        if (columnWidths[j] < valueLength) columnWidths[j] = valueLength;

                        //写单元格的内容
                        tempRow.CreateCell(j).SetCellValue(dataFormatMap == null || !dataFormatMap.ContainsKey(myPro[j].Name) ? cellValue : dataFormatMap[myPro[j].Name](cellValue));
                    }
                }
            }
            //调整自动列宽
            for (var i = 0; i < columnInfo.Keys.Count; i++)
                //设置列宽
                sheet1.SetColumnWidth(i, ((columnWidths[i] > 254 ? 254 : columnWidths[i]) + 1) * 256);
            //取出文件内容
            book.Write(outputStream);
        }

        /// <summary>
        /// 根据xlsx模板导出以xlsx结尾的Excel数据，
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objList">导出数据列表</param>
        /// <param name="columnInfo">数据列信息字典：属性名、属性摆放位置</param>
        /// <param name="templatePath">模板xlsx文件在本地的路径</param>
        /// <param name="outputPath">导出数据输出到本地的路径</param>
        /// <param name="dataFormatMap">数据列格式化</param>
        public static void ListToExcelXlsxWithTemplate<T>(List<T> objList, Dictionary<string, int> columnInfo, string templatePath, string outputPath, Dictionary<string, ExcelDataFormat<dynamic>> dataFormatMap = null)
        {
            using (Stream templateStream = File.OpenRead(templatePath))
            using (Stream outputStream = File.OpenWrite(outputPath))
                ListToExcelXlsxWithTemplate<T>(objList, columnInfo, templateStream, outputStream, dataFormatMap);
        }

        /// <summary>
        /// 根据xlsx模板导出以xlsx结尾的Excel数据，
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objList">导出数据列表</param>
        /// <param name="columnInfo">数据列信息字典：属性名、属性摆放位置</param>
        /// <param name="templateStream">模板xlsx文件的输入流</param>
        /// <param name="outputStream">导出数据输出到该输出流</param>
        /// <param name="dataFormatMap">数据列格式化</param>
        public static void ListToExcelXlsxWithTemplate<T>(List<T> objList, Dictionary<string, int> columnInfo, Stream templateStream, Stream outputStream, Dictionary<string, ExcelDataFormat<dynamic>> dataFormatMap = null)
        {
            if (columnInfo.Count == 0)
                throw new Exception("字段列表为空，不能导出数据！");
            if (objList.Count == 0)
                throw new Exception("数据列表为空，未能导出数据！");

            //创建Excel文件的对象  
            var book = new XSSFWorkbook();
            //添加一个sheet  
            var sheet1 = book.CreateSheet("Sheet1");

            //列宽暂存变量
            var columnWidths = new int[columnInfo.Keys.Count];

            var myPro = PropertyInfoList<T>(columnInfo);
            //如果没有找到可用的属性则结束 
            if (myPro.Count == 0)
                return;
            Encoding e936 = Encoding.GetEncoding(936);
            //将列表数据写入Excel文件对象
            for (var i = 0; i < objList.Count; i++)
            {
                //每一行创建一次Excel的行对象
                var tempRow = (XSSFRow)sheet1.CreateRow(i + 1);
                for (var j = 0; j < myPro.Count; j++)
                    if (myPro[j] != null)
                    {
                        var myProValue = myPro[j].GetValue(objList[i], null);
                        if (myProValue != null)
                        {
                            //取单元格的值
                            var cellValue = myProValue.ToString().Trim();

                            //当前内容单元格的宽度
                            var valueLength = e936.GetBytes(cellValue).Length;
                            //取列宽
                            if (columnWidths[j] < valueLength) columnWidths[j] = valueLength;

                            //写单元格的内容
                            tempRow.CreateCell(j).SetCellValue(dataFormatMap == null || !dataFormatMap.ContainsKey(myPro[j].Name) ? cellValue : dataFormatMap[myPro[j].Name](cellValue));
                        }
                    }
            }
            //调整自动列宽
            for (var i = 0; i < columnInfo.Keys.Count; i++)
                //设置列宽
                sheet1.SetColumnWidth(i, ((columnWidths[i] > 254 ? 254 : columnWidths[i]) + 1) * 256);
            //取出文件内容
            book.Write(outputStream);
        }

        /// <summary>
        /// 导出以xlsx结尾的Excel数据，
        /// </summary>
        /// <typeparam name="T">导出的数据类型</typeparam>
        /// <param name="objList">导出数据列表</param>
        /// <param name="columnInfo">数据列信息字典</param>
        /// <param name="outputPath">导出数据输出到该输出流</param>
        /// <param name="dataFormatMap"></param>
        public static void ListToExcelXlsx<T>(List<T> objList, Dictionary<string, string> columnInfo, string outputPath, Dictionary<string, ExcelDataFormat<dynamic>> dataFormatMap = null)
        {
            using (Stream outputStream = File.OpenWrite(outputPath))
                ListToExcelXlsx<T>(objList, columnInfo, outputStream, dataFormatMap);
        }

        /// <summary>
        /// 导出以xlsx结尾的Excel数据，
        /// </summary>
        /// <typeparam name="T">导出的数据类型</typeparam>
        /// <param name="objList">导出数据列表</param>
        /// <param name="columnInfo">数据列信息字典</param>
        /// <param name="stream">导出数据输出到该输出流</param>
        /// <param name="dataFormatMap"></param>
        public static void ListToExcelXlsx<T>(List<T> objList, Dictionary<string, string> columnInfo, Stream outputStream, Dictionary<string, ExcelDataFormat<dynamic>> dataFormatMap = null)
        {
            if (columnInfo.Count == 0)
                throw new Exception("字段列表为空，不能导出数据！");
            if (objList.Count == 0)
                throw new Exception("数据列表为空，未能导出数据！");

            //创建Excel文件的对象  
            var book = new XSSFWorkbook(); //new HSSFWorkbook();
            //添加一个sheet  
            var sheet1 = book.CreateSheet("Sheet1");
            //给sheet1添加第一行的头部标题  
            var row1 = (XSSFRow)sheet1.CreateRow(0);

            //列宽暂存变量
            var columnWidths = new int[columnInfo.Keys.Count];

            var myPro = PropertyInfoList<T>(columnInfo);
            //如果没有找到可用的属性则结束 
            if (myPro.Count == 0)
                return;
            Encoding e936= Encoding.GetEncoding(936);
            for (var i = 0; i < columnInfo.Keys.Count; i++)
            {
                var keyName = columnInfo.Keys.ElementAt(i);
                //列名
                var cName = columnInfo[keyName];
                //列宽
                columnWidths[i] = e936.GetBytes(cName).Length;
                row1.CreateCell(i).SetCellValue(columnInfo[keyName]);
            }


            //将列表数据写入Excel文件对象
            for (var i = 0; i < objList.Count; i++)
            {
                //每一行创建一次Excel的行对象
                var tempRow = (XSSFRow)sheet1.CreateRow(i + 1);
                for (var j = 0; j < myPro.Count; j++)
                {
                    var myProValue = myPro[j].GetValue(objList[i], null);
                    if (myProValue != null)
                    {
                        //取单元格的值
                        var cellValue = myProValue.ToString().Trim();

                        //当前内容单元格的宽度
                        var valueLength = e936.GetBytes(cellValue).Length;
                        //取列宽
                        if (columnWidths[j] < valueLength) columnWidths[j] = valueLength;

                        //写单元格的内容
                        tempRow.CreateCell(j).SetCellValue(dataFormatMap == null || !dataFormatMap.ContainsKey(myPro[j].Name) ? cellValue : dataFormatMap[myPro[j].Name](cellValue));
                    }
                }
            }
            //调整自动列宽
            for (var i = 0; i < columnInfo.Keys.Count; i++)
                //设置列宽
                sheet1.SetColumnWidth(i, ((columnWidths[i]>254?254: columnWidths[i]) + 1) * 256);
            //取出文件内容
            book.Write(outputStream);
        }
    }
}
