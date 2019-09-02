package com.txj.common;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.lang.reflect.Field;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import org.apache.commons.lang3.StringUtils;
import org.apache.poi.hssf.usermodel.HSSFCell;
import org.apache.poi.hssf.usermodel.HSSFRow;
import org.apache.poi.hssf.usermodel.HSSFSheet;
import org.apache.poi.hssf.usermodel.HSSFWorkbook;
import org.apache.poi.ss.usermodel.CellBase;
import org.apache.poi.ss.usermodel.CellType;
import org.apache.poi.xssf.usermodel.XSSFRow;
import org.apache.poi.xssf.usermodel.XSSFSheet;
import org.apache.poi.xssf.usermodel.XSSFWorkbook;
import com.txj.annotation.ExcelCol;
import com.txj.annotation.ExcelSheet;

public final class ExcelHelper {
	/**
	 * 导出excel到输出流上
	 * 
	 * @param dataListArrays
	 *            数据集合，可以有多个，每个放在不同的sheet中
	 * @param outputStream
	 *            输出流
	 */
	public static <T> void listToExcelXlsx(final List<T>[] dataListArrays,final OutputStream outputStream) {
		listToExcelXlsx(dataListArrays, outputStream, null);
	}

	/**
	 *  把输入流的内容转化为List
	 * @param clazz
	 * @param inputStream
	 * @param groupName
	 * @return
	 */
	public static <T> List<T> ExcelXlsxToList(final Class<T> clazz, final InputStream inputStream, final String groupName){
		final List<T> ret = new ArrayList<T>();
		final String sheetName=getSheetName(clazz, groupName);
		final List<ExcelColInfo> excelColInfoList = colFilter(clazz, groupName);
        if (excelColInfoList.size() == 0)
        {
            throw new RuntimeException("字段列表为空，不能导入数据！");
        }
        XSSFWorkbook xssfWorkbook=null;
		try {
			xssfWorkbook = new XSSFWorkbook(inputStream);
		} catch (IOException e1) {
			e1.printStackTrace();
		}
		final XSSFSheet xssfSheet=(XSSFSheet)xssfWorkbook.getSheet(sheetName);
        XSSFRow xssfRow;
        T obj;
        for (int i=1,len= xssfSheet.getLastRowNum();i<=len ;i++)
        {
            xssfRow = (XSSFRow)xssfSheet.getRow(i);
            try {
				obj=(T)clazz.newInstance();
				for (final ExcelColInfo excelColInfo : excelColInfoList)
				{
					setPropValue(obj, xssfRow.getCell(excelColInfo.getColIndex()), excelColInfo.getField());
				}
				ret.add(obj);
			} catch (InstantiationException | IllegalAccessException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
        }
        return ret;
    }
	
	/**
	 * 把输入流的内容转化为List
	 * @param clazz	
	 * @param inputStream	
	 * @param groupName	
	 * @return
	 */
	public static <T> List<T> ExcelXlsToList(final Class<T> clazz, final InputStream inputStream, final String groupName) {
		final List<T> ret = new ArrayList<T>();
		final String sheetName = getSheetName(clazz, groupName);
		final List<ExcelColInfo> excelColInfoList = colFilter(clazz, groupName);
		if (excelColInfoList.size() == 0) {
			throw new RuntimeException("字段列表为空，不能导入数据！");
		}
		HSSFWorkbook hssfWorkbook = null;
		try {
			hssfWorkbook = new HSSFWorkbook(inputStream);
		} catch (IOException e) {
			e.printStackTrace();
		}
		final HSSFSheet hssfSheet = (HSSFSheet) hssfWorkbook.getSheet(sheetName);
		HSSFRow hssfRow;
		T obj;
		for (int i = 1, len = hssfSheet.getLastRowNum(); i <= len; i++) {
			hssfRow = (HSSFRow) hssfSheet.getRow(i);
			try {
				obj = (T) clazz.newInstance();
				for (final ExcelColInfo excelColInfo : excelColInfoList) {
					setPropValue(obj, (HSSFCell) hssfRow.getCell(excelColInfo.getColIndex()), excelColInfo.getField());
				}
				ret.add(obj);
			} catch (InstantiationException | IllegalAccessException e) {
				e.printStackTrace();
			}
		}
		return ret;
	}

	static void setPropValue(final Object obj, final CellBase cell, final Field field) {
		final CellType cellType = cell.getCellType();
		try {
			if (cellType == CellType.BOOLEAN) {
				field.set(obj, cell.getBooleanCellValue());
			} else if (cellType == CellType.NUMERIC) {
				final Class<?> clazz = field.getClass();
				if (clazz == Double.class || clazz == double.class) {
					field.set(obj, cell.getNumericCellValue());
				} else if (clazz == Float.class || clazz == float.class) {
					Double val=cell.getNumericCellValue();
					field.set(obj, val.floatValue());
				} else if (clazz == Integer.class || clazz == int.class) {
					Double val=cell.getNumericCellValue();
					field.set(obj, val.intValue());
				} else if (clazz == Long.class || clazz == long.class) {
					Double val=cell.getNumericCellValue();
					field.set(obj, val.longValue());
				} else if (clazz == Short.class || clazz == short.class) {
					Double val=cell.getNumericCellValue();
					field.set(obj, val.shortValue());
				}
			} else if (cellType == CellType.STRING) {
				field.set(obj, cell.getStringCellValue());
			} else {
				field.set(obj, null);
			}
		} catch (IllegalArgumentException | IllegalAccessException e) {
			e.printStackTrace();
		}
	}

	/**
	 * 导出excel到输出流上
	 * 
	 * @param dataListArrays
	 *            数据集合，可以有多个，每个放在不同的sheet中
	 * @param outputStream
	 *            输出流
	 * @param groupName
	 *            组名称
	 */
	public static <T> void listToExcelXlsx(final List<T>[] dataListArrays, final OutputStream outputStream, final String groupName) {
		final XSSFWorkbook xssfWorkbook = new XSSFWorkbook();
		for (final List<T> dataList : dataListArrays) {
			if (dataList == null || dataList.size() == 0) {
				throw new RuntimeException("数据列表为空，未能导出数据！");
			}
			final List<ExcelColInfo> excelColInfoList = colFilter(dataList.get(0).getClass(), groupName);
			if (excelColInfoList.size() == 0) {
				throw new RuntimeException("字段列表为空，不能导出数据！");
			}
			final String sheetName = getSheetName(dataList.get(0).getClass(), groupName);
			final XSSFSheet xssfSheet = xssfWorkbook.createSheet(sheetName);
			XSSFRow xssfRow = (XSSFRow) xssfSheet.createRow(0);
			for (final ExcelColInfo excelColInfo : excelColInfoList) {
				xssfRow.createCell(excelColInfo.getColIndex()).setCellValue(excelColInfo.getColName());
			}
			T temp;
			for (int i = 1, len = dataList.size(); i <= len; i++) {
				temp = dataList.get(i - 1);
				xssfRow = (XSSFRow) xssfSheet.createRow(i);
				for (final ExcelColInfo excelColInfo : excelColInfoList) {
					try {
						setCellValue(xssfRow.createCell(excelColInfo.getColIndex()), excelColInfo.getField().get(temp));
					} catch (IllegalArgumentException e) {
						e.printStackTrace();
					} catch (IllegalAccessException e) {
						e.printStackTrace();
					}
				}
			}
			for (int i = 0, len = excelColInfoList.size(); i < len; i++) {
				xssfSheet.autoSizeColumn(i);
			}
			try {
				xssfWorkbook.write(outputStream);
			} catch (IOException e) {
				e.printStackTrace();
			} finally {
				try {
					xssfWorkbook.close();
				} catch (IOException e) {
					e.printStackTrace();
				}
			}
		}
	}

	/**
	 * 导出excel到输出流上
	 * 
	 * @param dataListArrays
	 *            数据集合，可以有多个，每个放在不同的sheet中
	 * @param outputStream
	 *            组名称
	 */
	public static <T> void listToExcelXls(final List<T>[] dataListArrays, final OutputStream outputStream) {
		listToExcelXls(dataListArrays, outputStream, null);
	}

	/**
	 * 导出excel到输出流上
	 * 
	 * @param dataListArrays
	 *            数据集合，可以有多个，每个放在不同的sheet中
	 * @param outputStream
	 *            输出流
	 * @param groupName
	 *            组名称
	 */
	public static <T> void listToExcelXls(final List<T>[] dataListArrays, final OutputStream outputStream, final String groupName) {
		final HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
		for (final List<T> dataList : dataListArrays) {
			if (dataList == null || dataList.size() == 0) {
				throw new RuntimeException("数据列表为空，未能导出数据！");
			}
			final List<ExcelColInfo> excelColInfoList = colFilter(dataList.get(0).getClass(), groupName);
			if (excelColInfoList.size() == 0) {
				throw new RuntimeException("字段列表为空，不能导出数据！");
			}
			final String sheetName = getSheetName(dataList.get(0).getClass(), groupName);
			final HSSFSheet hssfSheet = hssfWorkbook.createSheet(sheetName);
			HSSFRow hssfRow = (HSSFRow) hssfSheet.createRow(0);
			for (final ExcelColInfo excelColInfo : excelColInfoList) {
				hssfRow.createCell(excelColInfo.getColIndex()).setCellValue(excelColInfo.getColName());
			}
			T temp;
			for (int i = 1, len = dataList.size(); i <= len; i++) {
				temp = dataList.get(i - 1);
				hssfRow = (HSSFRow) hssfSheet.createRow(i);
				for (final ExcelColInfo excelColInfo : excelColInfoList) {
					try {
						setCellValue(hssfRow.createCell(excelColInfo.getColIndex()), excelColInfo.getField().get(temp));
					} catch (IllegalArgumentException e) {
						e.printStackTrace();
					} catch (IllegalAccessException e) {
						e.printStackTrace();
					}
				}
			}
			for (int i = 0, len = excelColInfoList.size(); i < len; i++) {
				hssfSheet.autoSizeColumn(i);
			}
			try {
				hssfWorkbook.write(outputStream);
			} catch (IOException e) {
				e.printStackTrace();
			} finally {
				try {
					hssfWorkbook.close();
				} catch (IOException e) {
					e.printStackTrace();
				}
			}
		}
	}

	public static final SimpleDateFormat simpleDateFormat;

	static {
		simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
	}

	/**
	 * 设置单元格的值
	 * 
	 * @param cell
	 *            单元格对象
	 * @param val
	 *            单元格值
	 * @param width
	 *            单元格宽度
	 */
	static void setCellValue(CellBase cell, Object val) {
		if (val != null) {
			if (val instanceof Double) {
				cell.setCellValue((double) val);
			} else if (val instanceof Date) {
				synchronized (simpleDateFormat) {
					cell.setCellValue(simpleDateFormat.format((Date) val));
				}
			} else if (val instanceof Boolean) {
				cell.setCellValue((Boolean) val);
			} else {
				cell.setCellValue(String.valueOf(val));
			}
		}
	}

	/**
	 * 获取表单名称
	 * 
	 * @param clazz
	 * @param groupName
	 * @return
	 */
	static String getSheetName(final Class<?> clazz, final String groupName) {
		final ExcelSheet excelSheet = (ExcelSheet) clazz.getAnnotation(ExcelSheet.class);
		final String[] groupNames = excelSheet.groupNames();
		for (int i = 0, len = groupName.length(); i < len; i++) {
			if (StringUtils.isEmpty(groupName) || groupNames[i].equals(groupName)) {
				return groupName;
			}
		}
		return "sheet" + PasswordHelper.RandomPassword(6, 1);
	}

	static List<ExcelColInfo> colFilter(final Class<?> clazz, final String groupName) {
		final List<ExcelColInfo> excelColInfoList = new ArrayList<ExcelColInfo>();
		Field field;
		ExcelColInfo excelColInfo;
		ExcelCol excelCol;
		String[] groupNames;
		for (int i = 0, len_i = clazz.getDeclaredFields().length; i < len_i; i++) {
			field = clazz.getDeclaredFields()[i];
			excelCol = field.getAnnotation(ExcelCol.class);
			groupNames = excelCol.groupNames();
			for (int j = 0, len_j = groupNames.length; j < len_j; j++) {
				if (StringUtils.isEmpty(groupName) || groupNames[j].equals(groupName)) {
					excelColInfo = new ExcelColInfo();
					field.setAccessible(true);
					excelColInfo.setField(field);
					excelColInfo.setColName(excelCol.colNames()[j]);
					excelColInfo.setColIndex(excelCol.colIndexs()[j]);
					excelColInfoList.add(excelColInfo);
				}
			}
		}
		return excelColInfoList;
	}

	private static final class ExcelColInfo {
		private Field field;
		private Integer colIndex;
		private String colName;

		public final Field getField() {
			return field;
		}

		public final void setField(final Field field) {
			this.field = field;
		}

		public final int getColIndex() {
			return colIndex;
		}

		public final void setColIndex(final int colIndex) {
			this.colIndex = colIndex;
		}

		public final String getColName() {
			return colName;
		}

		public final void setColName(final String colName) {
			this.colName = colName;
		}
	}
}