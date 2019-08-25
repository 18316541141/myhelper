package web.template.controller.common;

import java.io.File;
import java.io.IOException;

import javax.servlet.http.HttpServletRequest;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.apache.shiro.SecurityUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.context.request.RequestContextHolder;
import org.springframework.web.context.request.ServletRequestAttributes;
import org.springframework.web.context.support.ServletContextResource;

import com.txj.common.SnowFlakeHelper;

import web.template.view.MyExcelView;

/**
 * 基础控制器，所有控制器必须基础该控制器。
 * @author admin
 */
public abstract class BaseController {
	
	@Autowired
	protected SnowFlakeHelper snowFlakeHelper;
	
	@Autowired
	protected MyExcelView myExcelView;
	
	/**
	 * 跨平台的斜杠
	 */
	protected static char s;
	
	static{
		s=File.separatorChar;
	}
	
	/**
	 * 日志输出类
	 */
	protected Logger logger;
	
	public BaseController(){
		logger=LogManager.getLogger(this.getClass());
	}
	
	/**
	 * 返回当前登陆的用户名
	 * @return
	 */
	public String username() {
		return (String)SecurityUtils.getSubject().getPrincipal();
	}
	
	/**
	 * 把web路径的文件对象返回
	 * @param webPath web路径
	 * @return
	 */
	public File realFile(String webPath){
		try {
			HttpServletRequest request = ((ServletRequestAttributes) (RequestContextHolder.currentRequestAttributes())).getRequest();
			return new ServletContextResource(request.getServletContext(), webPath).getFile();
		} catch (IOException e) {
			return null;
		}
	}
	
	/**
	 * 把web路径转为物理路径
	 * @param webPath web路径
	 * @return
	 */
	public String realPath(String webPath){
		return realFile(webPath).getAbsolutePath();
	}
}
