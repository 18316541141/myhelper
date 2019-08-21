package com.txj.common;
import java.io.OutputStream;
import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.List;

import org.apache.commons.lang3.StringUtils;
import org.apache.poi.hssf.usermodel.HSSFWorkbook;

import com.txj.annotation.ExcelCol;
import com.txj.annotation.ExcelSheet;
public class ExcelHelper {
	public static <T> void listToExcelXls(List<T>[] dataListArrays, OutputStream outputStream,String groupName){
		HSSFWorkbook hssfWorkbook=new HSSFWorkbook();
		for (List<T> dataList : dataListArrays){
			if(dataList==null || dataList.size() == 0)
            {
                throw new RuntimeException("数据列表为空，未能导出数据！");
            }
		}
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
					excelColInfo.setExcelCol(excelCol);
					excelColInfoList.add(excelColInfo);
				}
			}
		}
		return excelColInfoList;
	}
	
	private static class ExcelColInfo{
		private Field field;
		private ExcelCol excelCol;

		public ExcelCol getExcelCol() {
			return excelCol;
		}

		public void setExcelCol(ExcelCol excelCol) {
			this.excelCol = excelCol;
		}

		public Field getField() {
			return field;
		}

		public void setField(Field field) {
			this.field = field;
		}
	}
}