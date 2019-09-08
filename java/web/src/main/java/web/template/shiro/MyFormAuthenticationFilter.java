package web.template.shiro;

import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.Collection;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.UUID;

import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;

import org.apache.shiro.SecurityUtils;
import org.apache.shiro.authc.AuthenticationException;
import org.apache.shiro.authc.AuthenticationToken;
import org.apache.shiro.mgt.DefaultSecurityManager;
import org.apache.shiro.session.Session;
import org.apache.shiro.session.mgt.DefaultSessionManager;
import org.apache.shiro.session.mgt.eis.SessionDAO;
import org.apache.shiro.subject.Subject;
import org.apache.shiro.web.filter.authc.FormAuthenticationFilter;
import org.apache.shiro.web.mgt.DefaultWebSecurityManager;
import org.apache.shiro.web.session.mgt.DefaultWebSessionManager;
import org.apache.shiro.web.session.mgt.ServletContainerSessionManager;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.txj.common.entity.Result;

import web.template.entity.common.LeftMenu;
import web.template.service.common.SystemService;

/**
 * 重写登陆表单拦截器
 * 
 * @author admin
 *
 */
public final class MyFormAuthenticationFilter extends FormAuthenticationFilter {

	private ObjectMapper objectMapper;

	private SystemService systemService;

	/**
	 * 当用户没权限或未登陆时触发该功能
	 * 
	 * @param request
	 *            请求报文
	 * @param response
	 *            相应报文
	 */
	@Override
	protected boolean onAccessDenied(ServletRequest request, ServletResponse response) throws Exception {
		// 判断是否为登陆请求
		if (isLoginRequest(request, response)) {
			return executeLogin(request, response);
		} else {
			response.setCharacterEncoding("UTF-8");
			response.setContentType("application/json; charset=utf-8");
			response.getWriter().write(objectMapper.writeValueAsString(new Result(-10, "", null)));
			return false;
		}
	}

	/**
	 * 执行登陆操作
	 * 
	 * @param request
	 *            请求报文
	 * @param response
	 *            相应报文
	 */
	protected boolean executeLogin(ServletRequest request, ServletResponse response) throws Exception {
		AuthenticationToken token = createToken(request, response);
		if (token == null) {
			String msg = "createToken method implementation returned null. A valid non-null AuthenticationToken "
					+ "must be created in order to execute a login attempt.";
			throw new IllegalStateException(msg);
		}
		response.setCharacterEncoding("UTF-8");
		response.setContentType("application/json; charset=utf-8");
		PrintWriter printWriter = response.getWriter();
		Subject subject = getSubject(request, response);
		Session session = subject.getSession();
		try {
			String[] vercodes = request.getParameterValues("vercode");
			if (vercodes.length == 0) {
				printWriter.write(objectMapper.writeValueAsString(new Result(-1, "验证码错误。", null)));
				return false;
			}
			String temp = (String) session.getAttribute("vercode");
			for (String vercode : vercodes) {
				if (!vercode.equalsIgnoreCase(temp)) {
					printWriter.write(objectMapper.writeValueAsString(new Result(-1, "验证码错误。", null)));
					return false;
				}
			}
			subject.login(token);
			Map<String, Object> dataMap = new HashMap<String, Object>();
			dataMap.put("leftMenus", systemService.loadLeftMenus((String) token.getPrincipal()));
			printWriter.write(objectMapper.writeValueAsString(new Result(0, "", dataMap)));
		} catch (AuthenticationException e) {
			printWriter.write(objectMapper.writeValueAsString(new Result(-1, "账号或密码错误，登陆失败！", null)));
		}
		// 主动修改验证码，只要用户不刷新验证码，则永远也不可能验证通过，强制用户刷新验证码
		session.setAttribute("vercode", UUID.randomUUID().toString());
		return false;
	}

	public ObjectMapper getObjectMapper() {
		return objectMapper;
	}

	public void setObjectMapper(ObjectMapper objectMapper) {
		this.objectMapper = objectMapper;
	}

	public SystemService getSystemService() {
		return systemService;
	}

	public void setSystemService(SystemService systemService) {
		this.systemService = systemService;
	}
}