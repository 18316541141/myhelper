package web.template.controller;

import org.apache.shiro.SecurityUtils;

/**
 * 基础控制器，所有控制器必须基础该控制器。
 * @author admin
 */
public abstract class BaseController {
	/**
	 * 返回当前登陆的用户名
	 * @return
	 */
	public String username() {
		return (String)SecurityUtils.getSubject().getPrincipal();
	}
}
