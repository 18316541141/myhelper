package web.template.filter;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import org.apache.commons.lang3.StringUtils;
import org.apache.shiro.SecurityUtils;
import org.springframework.lang.Nullable;
import org.springframework.web.servlet.HandlerInterceptor;
import org.springframework.web.servlet.ModelAndView;
import com.txj.common.ThreadHelper;
import web.template.controller.common.IndexController;

/**
 * 版本更新拦截器，会使得实时加载的版本号更新，并唤醒等待池。
 * 
 * @author admin
 */
public class UpdateVersionInterceptor implements HandlerInterceptor {

	/**
	 * 当对某一实时刷新的数据进行操作后，使用该拦截器唤醒等待池， 事项实时刷新数据的功能
	 */
	public void postHandle(HttpServletRequest request, HttpServletResponse response, Object handler,
			@Nullable ModelAndView modelAndView) throws Exception {
		String realTimePools = request.getParameter("realTimePools");
		if (StringUtils.isNotEmpty(realTimePools)) {
			for (String realTimePool : realTimePools.split(",")) {
				ThreadHelper.editControllerVersion(realTimePool);
				String username=(String) SecurityUtils.getSubject().getPrincipal();
				String usernameAndPoolKey = username==null?"zhang":username  + realTimePool;
				if (IndexController.USERNAME_AND_POOL_SET.contains(usernameAndPoolKey)) {
					synchronized (IndexController.USERNAME_AND_POOL_SET) {
						IndexController.USERNAME_AND_POOL_SET.remove(usernameAndPoolKey);
					}
				}
				ThreadHelper.batchSet(realTimePool);
			}
		}
	}
}