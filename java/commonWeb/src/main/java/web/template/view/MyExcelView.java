package web.template.view;
import java.util.List;
import java.util.Map;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import org.apache.poi.ss.usermodel.Workbook;
import org.springframework.stereotype.Component;
import org.springframework.web.servlet.view.document.AbstractXlsxView;
import com.txj.common.ExcelHelper;

/**
 * 通用的excel视图处理器
 * @author admin
 */
@Component
public class MyExcelView extends AbstractXlsxView{
	
	/**
	 * 视图名称
	 */
	public final static String View;
	
	/**
	 * excel的名称key值
	 */
	public final static String EXCEL_NAME;
	
	/**
	 * excel导出时的组名称
	 */
	public final static String GROUP_NAME;
	
	/**
	 * excel的数据key值
	 */
	public final static String EXCEL_DATA;
	
	static{
		View="myExcelView";
		EXCEL_NAME="EXCEL_NAME";
		EXCEL_DATA="EXCEL_DATA";
		GROUP_NAME = "GROUP_NAME";
	}
	
	@Override
	@SuppressWarnings("unchecked")
	protected void buildExcelDocument(Map<String, Object> model, Workbook workbook, HttpServletRequest request,
			HttpServletResponse response) throws Exception {
		String excelName=String.valueOf(model.get(MyExcelView.EXCEL_NAME));
		String groupName=String.valueOf(model.get(MyExcelView.GROUP_NAME));
		List<?> excelData = (List<?>)model.get(MyExcelView.EXCEL_DATA);
		response.setContentType("application/octet-stream");
		response.setHeader("Content-Disposition", "filename="+new String(excelName.getBytes("utf-8"),"iso8859-1"));
		if(excelName.endsWith(".xls")){
			ExcelHelper.listToExcelXls(new List[]{excelData}, response.getOutputStream(), groupName);
		}else if(excelName.endsWith(".xlsx")){
			ExcelHelper.listToExcelXlsx(new List[]{excelData}, response.getOutputStream(), groupName);
		}
	}

}
