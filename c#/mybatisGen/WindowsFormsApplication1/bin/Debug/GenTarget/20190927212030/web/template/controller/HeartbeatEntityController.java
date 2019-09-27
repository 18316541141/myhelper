package web.template.controller;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.multipart.MultipartFile;

import web.template.controller.common.BaseController;
import web.template.service.IRobotQrCodePayTaskService;
/**
 * ***模块的控制器类
 * @author admin
 */
@RestController
@RequestMapping("/HeartbeatEntity")
public class HeartbeatEntityController extends BaseController{
	
	@Autowired
	private HeartbeatEntityService service;
	
	/*抄考代码
	----------------------------------------------------------------------------------------------------------------------------
	HeartbeatEntity data = new HeartbeatEntity();

					data.setId(nextId());
		
					data.setLastHeartbeat(param.getLastHeartbeat());
		
		
		
					data.setRobotIp(param.getRobotIp());
		
		
					data.setRemark(param.getRemark());
		
		
					data.setExtendField(param.getExtendField());
		
		
					data.setMonitorServer(param.getMonitorServer());
		
		
	----------------------------------------------------------------------------------------------------------------------------
	HeartbeatEntityParams param = new HeartbeatEntityParams();

		param.setId(param.getId());
		
		param.setLastHeartbeat(param.getLastHeartbeat());
		
		param.setLastHeartbeatStart(param.getLastHeartbeatStart());
		
		param.setLastHeartbeatEnd(param.getLastHeartbeatEnd());
		
		param.setRobotIp(param.getRobotIp());
		
		param.setRobotIpLike(param.getRobotIpLike());
		
		param.setRemark(param.getRemark());
		
		param.setRemarkLike(param.getRemarkLike());
		
		param.setExtendField(param.getExtendField());
		
		param.setExtendFieldLike(param.getExtendFieldLike());
		
		param.setMonitorServer(param.getMonitorServer());
		
		param.setMonitorServerLike(param.getMonitorServerLike());
		
	----------------------------------------------------------------------------------------------------------------------------
	* 分页查询***模块，并返回查询结果
	* @param param	查询参数
    * @param currentPageIndex	当前页码
 	* @param pageSize	每页显示的数据量
    * @return	返回***模块的查询结果
    @RequestMapping("/page")
	public Result page(HeartbeatEntityParams param,int currentPageIndex,int pageSize){
		return new Result(0,null,service.page(param, currentPageIndex, pageSize));
	}
	----------------------------------------------------------------------------------------------------------------------------
	* 导出***模块，使用excel表保存
	* @param param 筛选参数
	* @param excelType excel的后缀名、类型
	* @param currentPageIndex 当前页码
	* @param pageSize 每页显示的数据量
	* @return 返回excel
	@RequestMapping("/export")
	public ModelAndView export(HeartbeatEntityParams param, String excelType, int currentPageIndex, int pageSize){
		Map<String,Object> model=new HashMap<String,Object>();
		model.put(MyExcelView.EXCEL_DATA, service.export(param, currentPageIndex, pageSize).getPageDataList());
		model.put(MyExcelView.EXCEL_NAME, "excel文件名称."+excelType);
		model.put(MyExcelView.GROUP_NAME, "组名称");
		return new ModelAndView(myExcelView, model);
	}
	----------------------------------------------------------------------------------------------------------------------------
	@RequestMapping("/import")
	public Result importExcel(@RequestParam("fileUpload")MultipartFile fileUpload, 其他参数...){
		List<HeartbeatEntity> HeartbeatEntityList;
        if (fileUpload.getOriginalFilename().endsWith("xlsx"))
        {
            HeartbeatEntityList = ExcelHelper.excelXlsxToList<HeartbeatEntity>(fileUpload.InputStream);
        }
        else if (fileUpload.getOriginalFilename().endsWith("xls"))
        {
            HeartbeatEntityList = ExcelHelper.excelXlsToList<HeartbeatEntity>(fileUpload.InputStream);
        }
		service.addBatch(HeartbeatEntityList);
		return new Result(0, "导入成功。",null);
	}
	----------------------------------------------------------------------------------------------------------------------------
	@RequestMapping("/del")
	public Result del(long id){
        service.del(id);
		return new Result(0, "数据已删除。",null);
	}
	----------------------------------------------------------------------------------------------------------------------------
	* 新增一条数据
	* @param data	新增的数据
	@RequestMapping("/add")
	public Result add(HeartbeatEntity data){
		service.add(data);
		return new Result(0, "保存成功。",null);
	}
	----------------------------------------------------------------------------------------------------------------------------
	* 批量修改状态
	* @param datas	  修改状态的数据
	@RequestMapping("/changeStatus")
	public Result changeStatus(HeartbeatEntity datas){
		return new Result(0,"修改成功，共"+service.changeStatus(datas)+"条。",null);
	}
	----------------------------------------------------------------------------------------------------------------------------
	* 根据主键id查询***模块的数据实体
	* @param id	主键id
	* @param return 返回***模块的查询结果
	@RequestMapping("/load")
	public Result load(Long id){
		return new Result(0,null ,service.Load(id) );
	}
	----------------------------------------------------------------------------------------------------------------------------
	* 批量删除***模块的数据实体
	* @param datas	删除的数据主键列表
	@RequestMapping("/delBatch")
	public Result delBatch(List<HeartbeatEntity> datas){
		return new Result(0 ,null,"删除成功，共"+Service.DelBatch(datas)+"条。");
	}
	 */
}