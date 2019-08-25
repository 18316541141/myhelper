package web.template.controller.common;

import java.util.Date;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import web.template.entity.common.NewsAlarm;
import web.template.entity.common.Result;
import web.template.service.common.SystemService;

/**
 * 最新消息提醒的控制器
 * 
 * @author admin
 */
@RestController
@RequestMapping("/newAlarm")
public class NewAlarmController extends BaseController {

	@Autowired
	private SystemService systemService;

	/**
	 * 新增一条最新消息
	 * @return
	 */
	@RequestMapping("/add")
	public Result add() {
		NewsAlarm newsAlarm = new NewsAlarm();
		newsAlarm.setCreateDate(new Date());
		newsAlarm.setMenuId("m11");
		newsAlarm.setReadState(0);
		newsAlarm.setReceive("rfrefsdfsd");
		newsAlarm.setTitle("测试001");
		systemService.addNewsAlarm(newsAlarm);
		return new Result(0, null, null);
	}
}