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
@RequestMapping("/IRobotQrCodePayTask")
public class IRobotQrCodePayTaskController extends BaseController{
	
	@Autowired
	private IRobotQrCodePayTaskService service;
	
	/*抄考代码
	----------------------------------------------------------------------------------------------------------------------------
	IRobotQrCodePayTask data = new IRobotQrCodePayTask();

					data.setIrTaskID(nextId());
		
		
		
					data.setIrOrderNo(param.getIrOrderNo());
		
		
					data.setIrWeiXinNickName(param.getIrWeiXinNickName());
		
		
					data.setIrWeiXinHeaderImage(param.getIrWeiXinHeaderImage());
		
		
					data.setIrQrCodeImagePath(param.getIrQrCodeImagePath());
		
		
					data.setIrHandleState(param.getIrHandleState());
		
		
		
					data.setIrHandleMessage(param.getIrHandleMessage());
		
		
					data.setIrHandleTime(param.getIrHandleTime());
		
		
		
					data.setIrCreateTime(param.getIrCreateTime());
		
		
		
					data.setIrReportPicPathJson(param.getIrReportPicPathJson());
		
		
					data.setIrTakeMoney(param.getIrTakeMoney());
		
		
		
					data.setIrRobotId(param.getIrRobotId());
		
		
					data.setIrRemark(param.getIrRemark());
		
		
					data.setIrPushState(param.getIrPushState());
		
		
		
					data.setIrPushTime(param.getIrPushTime());
		
		
		
					data.setIrScanPayNotifyRet(param.getIrScanPayNotifyRet());
		
		
					data.setIrScanPayNotifyUrl(param.getIrScanPayNotifyUrl());
		
		
	----------------------------------------------------------------------------------------------------------------------------
	IRobotQrCodePayTaskParams param = new IRobotQrCodePayTaskParams();

		param.setIrTaskID(param.getIrTaskID());
		
		param.setIrTaskIDStart(param.getIrTaskIDStart());
		
		param.setIrTaskIDEnd(param.getIrTaskIDEnd());
		
		param.setIrOrderNo(param.getIrOrderNo());
		
		param.setIrOrderNoLike(param.getIrOrderNoLike());
		
		param.setIrWeiXinNickName(param.getIrWeiXinNickName());
		
		param.setIrWeiXinNickNameLike(param.getIrWeiXinNickNameLike());
		
		param.setIrWeiXinHeaderImage(param.getIrWeiXinHeaderImage());
		
		param.setIrWeiXinHeaderImageLike(param.getIrWeiXinHeaderImageLike());
		
		param.setIrQrCodeImagePath(param.getIrQrCodeImagePath());
		
		param.setIrQrCodeImagePathLike(param.getIrQrCodeImagePathLike());
		
		param.setIrHandleState(param.getIrHandleState());
		
		param.setIrHandleStateStart(param.getIrHandleStateStart());
		
		param.setIrHandleStateEnd(param.getIrHandleStateEnd());
		
		param.setIrHandleMessage(param.getIrHandleMessage());
		
		param.setIrHandleMessageLike(param.getIrHandleMessageLike());
		
		param.setIrHandleTime(param.getIrHandleTime());
		
		param.setIrHandleTimeStart(param.getIrHandleTimeStart());
		
		param.setIrHandleTimeEnd(param.getIrHandleTimeEnd());
		
		param.setIrCreateTime(param.getIrCreateTime());
		
		param.setIrCreateTimeStart(param.getIrCreateTimeStart());
		
		param.setIrCreateTimeEnd(param.getIrCreateTimeEnd());
		
		param.setIrReportPicPathJson(param.getIrReportPicPathJson());
		
		param.setIrReportPicPathJsonLike(param.getIrReportPicPathJsonLike());
		
		param.setIrTakeMoney(param.getIrTakeMoney());
		
		param.setIrTakeMoneyStart(param.getIrTakeMoneyStart());
		
		param.setIrTakeMoneyEnd(param.getIrTakeMoneyEnd());
		
		param.setIrRobotId(param.getIrRobotId());
		
		param.setIrRobotIdLike(param.getIrRobotIdLike());
		
		param.setIrRemark(param.getIrRemark());
		
		param.setIrRemarkLike(param.getIrRemarkLike());
		
		param.setIrPushState(param.getIrPushState());
		
		param.setIrPushStateStart(param.getIrPushStateStart());
		
		param.setIrPushStateEnd(param.getIrPushStateEnd());
		
		param.setIrPushTime(param.getIrPushTime());
		
		param.setIrPushTimeStart(param.getIrPushTimeStart());
		
		param.setIrPushTimeEnd(param.getIrPushTimeEnd());
		
		param.setIrScanPayNotifyRet(param.getIrScanPayNotifyRet());
		
		param.setIrScanPayNotifyRetLike(param.getIrScanPayNotifyRetLike());
		
		param.setIrScanPayNotifyUrl(param.getIrScanPayNotifyUrl());
		
		param.setIrScanPayNotifyUrlLike(param.getIrScanPayNotifyUrlLike());
		
	----------------------------------------------------------------------------------------------------------------------------
	* 分页查询***模块，并返回查询结果
	* @param param	查询参数
    * @param currentPageIndex	当前页码
 	* @param pageSize	每页显示的数据量
    * @return	返回***模块的查询结果
    @RequestMapping("/page")
	public Result page(IRobotQrCodePayTaskParams param,int currentPageIndex,int pageSize){
		return new Result(0,null,service.page(param, currentPageIndex, pageSize));
	}
	----------------------------------------------------------------------------------------------------------------------------
	导出excel，待定...
	----------------------------------------------------------------------------------------------------------------------------
	@RequestMapping("/import")
	public Result importExcel(@RequestParam("fileUpload")MultipartFile fileUpload, 其他参数...){
		List<IRobotQrCodePayTask> IRobotQrCodePayTaskList;
        if (fileUpload.getOriginalFilename().endsWith("xlsx"))
        {
            IRobotQrCodePayTaskList = ExcelHelper.excelXlsxToList<IRobotQrCodePayTask>(fileUpload.InputStream);
        }
        else if (fileUpload.getOriginalFilename().endsWith("xls"))
        {
            IRobotQrCodePayTaskList = ExcelHelper.excelXlsToList<IRobotQrCodePayTask>(fileUpload.InputStream);
        }
		service.addBatch(IRobotQrCodePayTaskList);
		return new Result(0, "导入成功。",null);
	}
	----------------------------------------------------------------------------------------------------------------------------
	@RequestMapping("/del")
	public Result del(long irTaskID){
        service.del(irTaskID);
		return new Result(0, "数据已删除。",null);
	}
	----------------------------------------------------------------------------------------------------------------------------
	* 新增一条数据
	* @param data	新增的数据
	@RequestMapping("/add")
	public Result add(IRobotQrCodePayTask data){
		service.add(data);
		return new Result(0, "保存成功。",null);
	}
	----------------------------------------------------------------------------------------------------------------------------
	* 批量修改状态
	* @param datas	  修改状态的数据
	@RequestMapping("/changeStatus")
	public Result changeStatus(IRobotQrCodePayTask datas){
		return new Result(0,"修改成功，共"+service.changeStatus(datas)+"条。",null);
	}
	----------------------------------------------------------------------------------------------------------------------------
	* 根据主键id查询***模块的数据实体
	* @param irTaskID	主键id
	* @param return 返回***模块的查询结果
	@RequestMapping("/load")
	public Result load(Long irTaskID){
		return new Result(0,null ,service.Load(irTaskID) );
	}
	----------------------------------------------------------------------------------------------------------------------------
	* 批量删除***模块的数据实体
	* @param datas	删除的数据主键列表
	@RequestMapping("/delBatch")
	public Result delBatch(List<IRobotQrCodePayTask> datas){
		return new Result(0 ,null,"删除成功，共"+Service.DelBatch(datas)+"条。");
	}
	 */
}