package web.template.controller.common;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.txj.common.entity.Result;

import web.template.params.LogEntityParams;
import web.template.service.common.LogEntityService;

/**
 * “日志管理”模块的控制器类
 * 
 * @author admin
 */
@RestController
@RequestMapping("/LogEntity")
public class LogEntityController extends BaseController {

	@Autowired
	private LogEntityService service;

	/**
	 * 分页查询“日志管理”模块，并返回查询结果
	 * 
	 * @param param
	 *            查询参数
	 * @param currentPageIndex
	 *            当前页码
	 * @param pageSize
	 *            每页显示的数据量
	 * @return 返回“日志管理”模块的查询结果
	 */
	@RequestMapping("/page")
	public Result page(LogEntityParams param, int currentPageIndex, int pageSize) {
		log.error("测试日志", new RuntimeException("致命错误"));
		return new Result(0, null, service.page(param, currentPageIndex, pageSize));
	}

	/**
	 * 根据主键id查询“日志管理”模块的数据实体
	 * 
	 * @param id
	 *            主键id
	 * @param return
	 *            返回“日志管理”模块的查询结果
	 */
	@RequestMapping("/load")
	public Result load(Long id) {
		return new Result(0, null, service.load(id));
	}

	/*
	 * 抄考代码
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- LogEntity data = new
	 * LogEntity();
	 * 
	 * data.setId(nextId());
	 * 
	 * data.setCreateDate(param.getCreateDate());
	 * 
	 * 
	 * 
	 * data.setLevel(param.getLevel());
	 * 
	 * 
	 * data.setThreadNo(param.getThreadNo());
	 * 
	 * 
	 * data.setMessage(param.getMessage());
	 * 
	 * 
	 * data.setProjectName(param.getProjectName());
	 * 
	 * 
	 * data.setTypeName(param.getTypeName());
	 * 
	 * 
	 * data.setFuncName(param.getFuncName());
	 * 
	 * 
	 * data.setException(param.getException());
	 * 
	 * 
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- LogEntityParams param
	 * = new LogEntityParams();
	 * 
	 * param.setId(param.getId());
	 * 
	 * param.setCreateDate(param.getCreateDate());
	 * 
	 * param.setCreateDateStart(param.getCreateDateStart());
	 * 
	 * param.setCreateDateEnd(param.getCreateDateEnd());
	 * 
	 * param.setLevel(param.getLevel());
	 * 
	 * param.setLevelLike(param.getLevelLike());
	 * 
	 * param.setThreadNo(param.getThreadNo());
	 * 
	 * param.setThreadNoLike(param.getThreadNoLike());
	 * 
	 * param.setMessage(param.getMessage());
	 * 
	 * param.setMessageLike(param.getMessageLike());
	 * 
	 * param.setProjectName(param.getProjectName());
	 * 
	 * param.setProjectNameLike(param.getProjectNameLike());
	 * 
	 * param.setTypeName(param.getTypeName());
	 * 
	 * param.setTypeNameLike(param.getTypeNameLike());
	 * 
	 * param.setFuncName(param.getFuncName());
	 * 
	 * param.setFuncNameLike(param.getFuncNameLike());
	 * 
	 * param.setException(param.getException());
	 * 
	 * param.setExceptionLike(param.getExceptionLike());
	 * 
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 分页查询***模块，并返回查询结果
	 * 
	 * @param param 查询参数
	 * 
	 * @param currentPageIndex 当前页码
	 * 
	 * @param pageSize 每页显示的数据量
	 * 
	 * @return 返回***模块的查询结果
	 * 
	 * @RequestMapping("/page") public Result page(LogEntityParams param,int
	 * currentPageIndex,int pageSize){ return new
	 * Result(0,null,service.page(param, currentPageIndex, pageSize)); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 导出***模块，使用excel表保存
	 * 
	 * @param param 筛选参数
	 * 
	 * @param excelType excel的后缀名、类型
	 * 
	 * @param currentPageIndex 当前页码
	 * 
	 * @param pageSize 每页显示的数据量
	 * 
	 * @return 返回excel
	 * 
	 * @RequestMapping("/export") public ModelAndView export(LogEntityParams
	 * param, String excelType, int currentPageIndex, int pageSize){
	 * Map<String,Object> model=new HashMap<String,Object>();
	 * model.put(MyExcelView.EXCEL_DATA, service.export(param, currentPageIndex,
	 * pageSize).getPageDataList()); model.put(MyExcelView.EXCEL_NAME,
	 * "excel文件名称."+excelType); model.put(MyExcelView.GROUP_NAME, "组名称"); return
	 * new ModelAndView(myExcelView, model); }
	 * -------------------------------------------------------------------------
	 * ---------------------------------------------------
	 * 
	 * @RequestMapping("/import") public Result
	 * importExcel(@RequestParam("fileUpload")MultipartFile fileUpload,
	 * 其他参数...){ List<LogEntity> LogEntityList; if
	 * (fileUpload.getOriginalFilename().endsWith("xlsx")) { LogEntityList =
	 * ExcelHelper.excelXlsxToList<LogEntity>(fileUpload.InputStream); } else if
	 * (fileUpload.getOriginalFilename().endsWith("xls")) { LogEntityList =
	 * ExcelHelper.excelXlsToList<LogEntity>(fileUpload.InputStream); }
	 * service.addBatch(LogEntityList); return new Result(0, "导入成功。",null); }
	 * -------------------------------------------------------------------------
	 * ---------------------------------------------------
	 * 
	 * @RequestMapping("/del") public Result del(long id){ service.del(id);
	 * return new Result(0, "数据已删除。",null); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 新增一条数据
	 * 
	 * @param data 新增的数据
	 * 
	 * @RequestMapping("/add") public Result add(LogEntity data){
	 * service.add(data); return new Result(0, "保存成功。",null); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 批量修改状态
	 * 
	 * @param datas 修改状态的数据
	 * 
	 * @RequestMapping("/changeStatus") public Result changeStatus(LogEntity
	 * datas){ return new
	 * Result(0,"修改成功，共"+service.changeStatus(datas)+"条。",null); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 根据主键id查询***模块的数据实体
	 * 
	 * @param id 主键id
	 * 
	 * @param return 返回***模块的查询结果
	 * 
	 * @RequestMapping("/load") public Result load(Long id){ return new
	 * Result(0,null ,service.Load(id) ); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 批量删除***模块的数据实体
	 * 
	 * @param datas 删除的数据主键列表
	 * 
	 * @RequestMapping("/delBatch") public Result delBatch(List<LogEntity>
	 * datas){ return new Result(0 ,null,"删除成功，共"+Service.DelBatch(datas)+"条。");
	 * }
	 */
}