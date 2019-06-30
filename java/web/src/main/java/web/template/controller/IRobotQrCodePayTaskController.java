package web.template.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import web.template.entity.Result;
import web.template.entity.param.IRobotQrCodePayTaskParam;
import web.template.service.IRobotQrCodePayTaskService;

@RestController
@RequestMapping("/IRobotQrCodePayTaskController")
public class IRobotQrCodePayTaskController {
	
	@Autowired
	private IRobotQrCodePayTaskService irobotQrCodePayTaskService;
	
	@RequestMapping("/page")	
	public Result page(IRobotQrCodePayTaskParam param,Integer currentPageIndex,Integer pageSize){
		return new Result(0, null, irobotQrCodePayTaskService.page(param, currentPageIndex, pageSize));
	}
}