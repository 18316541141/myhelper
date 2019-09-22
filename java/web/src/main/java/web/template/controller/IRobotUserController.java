package web.template.controller;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import web.template.controller.common.BaseController;
import web.template.service.IRobotUserService;
/**
 * “用户管理”模块的控制器类
 * @author admin
 */
@RestController
@RequestMapping("/IRobotUser")
public class IRobotUserController extends BaseController{
	
	@Autowired
	private IRobotUserService service;
	
	/*抄考代码
	----------------------------------------------------------------------------------------------------------------------------
	IRobotUser data = new IRobotUser();

					data.setIuId(nextId());
		
					data.setIuUsername(param.getIuUsername());
		
		
					data.setIuPassword(param.getIuPassword());
		
		
					data.setIuSignKey(param.getIuSignKey());
		
		
					data.setIuSignSecret(param.getIuSignSecret());
		
		
					data.setIuCreateDate(param.getIuCreateDate());
		
		
		
	----------------------------------------------------------------------------------------------------------------------------
	IRobotUserParams param = new IRobotUserParams();

		param.setIuId(param.getIuId());
		
		param.setIuUsername(param.getIuUsername());
		
		param.setIuUsernameLike(param.getIuUsernameLike());
		
		param.setIuPassword(param.getIuPassword());
		
		param.setIuPasswordLike(param.getIuPasswordLike());
		
		param.setIuSignKey(param.getIuSignKey());
		
		param.setIuSignKeyLike(param.getIuSignKeyLike());
		
		param.setIuSignSecret(param.getIuSignSecret());
		
		param.setIuSignSecretLike(param.getIuSignSecretLike());
		
		param.setIuCreateDate(param.getIuCreateDate());
		
		param.setIuCreateDateStart(param.getIuCreateDateStart());
		
		param.setIuCreateDateEnd(param.getIuCreateDateEnd());
		
	----------------------------------------------------------------------------------------------------------------------------
	* 分页查询***模块，并返回查询结果
	* @param param	查询参数
    * @param currentPageIndex	当前页码
 	* @param pageSize	每页显示的数据量
    * @return	返回***模块的查询结果
    @RequestMapping("/page")
	public Result page(IRobotUserParams param,int currentPageIndex,int pageSize){
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
	public ModelAndView export(IRobotUserParams param, String excelType, int currentPageIndex, int pageSize){
		Map<String,Object> model=new HashMap<String,Object>();
		model.put(MyExcelView.EXCEL_DATA, service.export(param, currentPageIndex, pageSize).getPageDataList());
		model.put(MyExcelView.EXCEL_NAME, "excel文件名称."+excelType);
		model.put(MyExcelView.GROUP_NAME, "组名称");
		return new ModelAndView(myExcelView, model);
	}
	----------------------------------------------------------------------------------------------------------------------------
	@RequestMapping("/import")
	public Result importExcel(@RequestParam("fileUpload")MultipartFile fileUpload, 其他参数...){
		List<IRobotUser> IRobotUserList;
        if (fileUpload.getOriginalFilename().endsWith("xlsx"))
        {
            IRobotUserList = ExcelHelper.excelXlsxToList<IRobotUser>(fileUpload.InputStream);
        }
        else if (fileUpload.getOriginalFilename().endsWith("xls"))
        {
            IRobotUserList = ExcelHelper.excelXlsToList<IRobotUser>(fileUpload.InputStream);
        }
		service.addBatch(IRobotUserList);
		return new Result(0, "导入成功。",null);
	}
	----------------------------------------------------------------------------------------------------------------------------
	@RequestMapping("/del")
	public Result del(long iuId){
        service.del(iuId);
		return new Result(0, "数据已删除。",null);
	}
	----------------------------------------------------------------------------------------------------------------------------
	* 新增一条数据
	* @param data	新增的数据
	@RequestMapping("/add")
	public Result add(IRobotUser data){
		service.add(data);
		return new Result(0, "保存成功。",null);
	}
	----------------------------------------------------------------------------------------------------------------------------
	* 批量修改状态
	* @param datas	  修改状态的数据
	@RequestMapping("/changeStatus")
	public Result changeStatus(IRobotUser datas){
		return new Result(0,"修改成功，共"+service.changeStatus(datas)+"条。",null);
	}
	----------------------------------------------------------------------------------------------------------------------------
	* 根据主键id查询***模块的数据实体
	* @param iuId	主键id
	* @param return 返回***模块的查询结果
	@RequestMapping("/load")
	public Result load(Long iuId){
		return new Result(0,null ,service.Load(iuId) );
	}
	----------------------------------------------------------------------------------------------------------------------------
	* 批量删除***模块的数据实体
	* @param datas	删除的数据主键列表
	@RequestMapping("/delBatch")
	public Result delBatch(List<IRobotUser> datas){
		return new Result(0 ,null,"删除成功，共"+Service.DelBatch(datas)+"条。");
	}
	 */
}