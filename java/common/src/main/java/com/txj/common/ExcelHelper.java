package com.txj.common;
import java.io.IOException;
import java.io.OutputStream;
import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import org.apache.commons.lang3.StringUtils;
import org.apache.poi.hssf.usermodel.HSSFCell;
import org.apache.poi.hssf.usermodel.HSSFRow;
import org.apache.poi.hssf.usermodel.HSSFSheet;
import org.apache.poi.hssf.usermodel.HSSFWorkbook;
import org.apache.poi.ss.usermodel.CellBase;

import com.txj.annotation.ExcelCol;
import com.txj.annotation.ExcelSheet;
public class ExcelHelper {
	
	
	/**
	 * 导出excel到输出流上
	 * @param dataListArrays 数据集合，可以有多个，每个放在不同的sheet中
	 * @param outputStream 组名称
	 */
	public static <T> void listToExcelXls(List<T>[] dataListArrays, OutputStream outputStream){
		listToExcelXls(dataListArrays, outputStream,null);
	}
	
	/**
	 * 导出excel到输出流上
	 * @param dataListArrays 数据集合，可以有多个，每个放在不同的sheet中
	 * @param outputStream 输出流
	 * @param groupName 组名称
	 */
	public static <T> void listToExcelXls(List<T>[] dataListArrays, OutputStream outputStream,String groupName){
		HSSFWorkbook hssfWorkbook=new HSSFWorkbook();
		for (List<T> dataList : dataListArrays){
			if(dataList==null || dataList.size() == 0){
                throw new RuntimeException("数据列表为空，未能导出数据！");
            }
			List<ExcelColInfo> excelColInfoList=colFilter(dataList.get(0).getClass(), groupName);
			if (excelColInfoList.size() == 0){
                throw new RuntimeException("字段列表为空，不能导出数据！");
            }
			String sheetName=getSheetName(dataList.get(0).getClass(), groupName);
			HSSFSheet hssfSheet=hssfWorkbook.createSheet(sheetName);
			HSSFRow hssfRow = (HSSFRow)hssfSheet.createRow(0);
			for(ExcelColInfo excelColInfo : excelColInfoList){
                hssfRow.createCell(excelColInfo.getColIndex()).setCellValue(excelColInfo.getColName());
			}
			T temp;
			for (int i = 1, len = dataList.size(); i <= len; i++){
				temp = dataList.get(i);
				hssfRow = (HSSFRow)hssfSheet.createRow(i);
				for(ExcelColInfo excelColInfo : excelColInfoList){
					try {
						setCellValue(hssfRow.createCell(i), excelColInfo.getField().get(temp));
					} catch (IllegalArgumentException e) {
						e.printStackTrace();
					} catch (IllegalAccessException e) {
						e.printStackTrace();
					}
				}
            }
			for (int i = 1,width, len = dataList.size(); i <= len; i++){
				hssfSheet.autoSizeColumn(i);
			}
			try {
				hssfWorkbook.write(outputStream);
			} catch (IOException e) {
				e.printStackTrace();
			}finally {
				try {
					hssfWorkbook.close();
				} catch (IOException e) {
					e.printStackTrace();
				}
			}
		}
	}
	
	/**
	 * 设置单元格的值
	 * @param cell 单元格对象
	 * @param val 单元格值
	 * @param width 单元格宽度
	 */
	static void setCellValue(CellBase cell, Object val){
		if (val != null)
        {
			if (val instanceof Double || val instanceof Integer || val instanceof Long){
				cell.setCellValue((double)val);
			} else if(val instanceof  Date){
				cell.setCellValue((Date)val);
			} else if (val instanceof Boolean){
				cell.setCellValue((Boolean)val);
			}else{
				cell.setCellValue((String)val);
			}
        }
	}
	
	/**
	 * 获取表单名称
	 * @param clazz
	 * @param groupName
	 * @return
	 */
	static String getSheetName(Class<?> clazz, String groupName)
    {
		ExcelSheet excelSheet=(ExcelSheet)clazz.getAnnotation(ExcelSheet.class);
		String[] groupNames=excelSheet.groupNames();
		for(int i=0,len=groupName.length();i<len;i++){
			if(StringUtils.isEmpty(groupName) || groupNames[i].equals(groupName)){
				return groupName;
			}
		}
		return "sheet"+PasswordHelper.RandomPassword(6,1);
    }
	
	static List<ExcelColInfo> colFilter(Class clazz, String groupName){
		List<ExcelColInfo> excelColInfoList = new ArrayList<ExcelColInfo>();
		Field field;
		ExcelColInfo excelColInfo;
		for(int i=0,len_i=clazz.getFields().length;i<len_i;i++){
			field=clazz.getFields()[i];
			ExcelCol excelCol=field.getAnnotation(ExcelCol.class);
			String[] groupNames=excelCol.groupNames();
			for(int j=0,len_j=groupNames.length;j<len_j;j++){				
				if(StringUtils.isEmpty(groupName) || groupNames[j].equals(groupName)){				
					excelColInfo=new ExcelColInfo();
					excelColInfo.setField(field);
					excelColInfo.setColName(excelCol.colNames()[j]);
					excelColInfo.setColIndex(excelCol.colIndexs()[j]);
					excelColInfoList.add(excelColInfo);
				}
			}
		}
		return excelColInfoList;
	}
	
	private static class ExcelColInfo{
		private Field field;
		private Integer colIndex;
		private String colName;

		public Field getField() {
			return field;
		}

		public void setField(Field field) {
			this.field = field;
		}

		public int getColIndex() {
			return colIndex;
		}

		public void setColIndex(int colIndex) {
			this.colIndex = colIndex;
		}

		public String getColName() {
			return colName;
		}

		public void setColName(String colName) {
			this.colName = colName;
		}

		public void setColIndex(Integer colIndex) {
			this.colIndex = colIndex;
		}
	}
}