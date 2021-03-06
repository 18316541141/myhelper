package web.template.controller.common;

import java.io.File;
import java.io.IOException;

import javax.servlet.http.HttpServletRequest;

import org.apache.logging.log4j.Logger;
import org.apache.shiro.SecurityUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.context.request.RequestContextHolder;
import org.springframework.web.context.request.ServletRequestAttributes;
import org.springframework.web.context.support.ServletContextResource;

import com.txj.common.SnowFlakeHelper;
import com.txj.common.entity.Result;
import com.txj.common.exception.ResultException;

import web.template.entity.common.MyLog;
import web.template.view.MyExcelView;

/**
 * 基础控制器，所有控制器必须基础该控制器。
 * 
 * @author admin
 */
@RestController
public abstract class BaseController {

	protected SnowFlakeHelper snowFlakeHelper;

	@Autowired
	public void setSnowFlakeHelper(SnowFlakeHelper snowFlakeHelper) {
		this.snowFlakeHelper = snowFlakeHelper;
		log.setSnowFlakeHelper(snowFlakeHelper);
	}

	@Autowired
	protected MyExcelView myExcelView;

	@Value("${projectName}")
	public void setProjectName(String projectName) {
		log.setProjectName(projectName);
	}

	/**
	 * 跨平台的斜杠
	 */
	protected final static char s;

	static {
		s = File.separatorChar;
	}

	/**
	 * 日志输出类
	 */
	protected MyLog log;

	public BaseController() {
		log = MyLog.getLogger(this.getClass());
	}

	/**
	 * 
	 * @param throwable
	 * @return
	 */
	@ExceptionHandler(Throwable.class)
	public final Result handleException(final Throwable throwable) {
		if (throwable instanceof ResultException) {
			return ((ResultException) throwable).getResult();
		} else {
			log.error(throwable.getMessage(), throwable);
			return new Result(-1, "系统繁忙，请稍后重试...", null);
		}
	}

	/**
	 * 返回当前登陆的用户名
	 * 
	 * @return
	 */
	public final String username() {
		return (String) SecurityUtils.getSubject().getPrincipal();
	}

	/**
	 * 把web路径的文件对象返回
	 * 
	 * @param webPath
	 *            web路径
	 * @return
	 */
	public final File realFile(final String webPath) {
		try {
			final HttpServletRequest request = ((ServletRequestAttributes) (RequestContextHolder
					.currentRequestAttributes())).getRequest();
			return new ServletContextResource(request.getServletContext(), webPath).getFile();
		} catch (IOException e) {
			return null;
		}
	}

	/**
	 * 把web路径转为物理路径
	 * 
	 * @param webPath
	 *            web路径
	 * @return
	 */
	public final String realPath(final String webPath) {
		return realFile(webPath).getAbsolutePath();
	}
}
