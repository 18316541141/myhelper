package web.template.controller;
import java.awt.image.BufferedImage;
import java.io.IOException;

import javax.imageio.ImageIO;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.shiro.SecurityUtils;
import org.apache.shiro.session.Session;
import org.apache.shiro.subject.Subject;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.context.support.ServletContextResource;
import org.springframework.web.multipart.MultipartFile;

import com.google.code.kaptcha.Producer;

import web.template.entity.Result;
import web.template.service.SystemService;
@RestController
@RequestMapping("/index")
public class IndexController {
	
	@Autowired
	private Producer defaultKaptcha;
	
	@Autowired
	private SystemService systemService;
	
	/**
	 * 登出功能
	 * @return
	 */
	@RequestMapping("/logout")
	public Result logout(){
		Subject subject = SecurityUtils.getSubject();
		subject.logout();
		return new Result(0,null,null);
	}
	
	/**
	 * 获取左侧菜单
	 * @return
	 */
	@RequestMapping("/loadLeftMenus")
	public Result loadLeftMenus(){
		return new Result(0,"",systemService.loadLeftMenus((String)SecurityUtils.getSubject().getPrincipal()));
	}
	
	/**
	 * 获取验证码
	 * @param response	验证码响应报文
	 */
	@RequestMapping("/verificationCode")
	public void verificationCode(HttpServletResponse response){
		response.setDateHeader("Expires", 0);
		response.setHeader("Cache-Control", "no-store, no-cache, must-revalidate");
		response.addHeader("Cache-Control", "post-check=0, pre-check=0");
		response.setHeader("Pragma", "no-cache");
		response.setContentType("image/jpeg");
		String text=defaultKaptcha.createText();
		Subject subject=SecurityUtils.getSubject();
		Session session=subject.getSession();
		session.setAttribute("vercode", text);
		BufferedImage bufferedImage=defaultKaptcha.createImage(text);
		try {
			ImageIO.write(bufferedImage, "jpg", response.getOutputStream());
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
	
	@RequestMapping("/uploadImage")
	public Result uploadImage(@RequestParam("file")MultipartFile file,HttpServletRequest request,String pathName){
		try {
			file.transferTo(new ServletContextResource(request.getServletContext(), "/WEB-INF/uploadFiles/"+pathName+"/").getFile());
			return null;
		} catch (IllegalStateException e) {
			e.printStackTrace();
			throw new RuntimeException(e);
		} catch (IOException e) {
			e.printStackTrace();
			throw new RuntimeException(e);
		}
	}
}