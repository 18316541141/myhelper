package web.template.controller.common;
import java.awt.image.BufferedImage;
import java.io.IOException;

import javax.imageio.ImageIO;
import javax.servlet.http.HttpServletResponse;

import org.apache.shiro.SecurityUtils;
import org.apache.shiro.session.Session;
import org.apache.shiro.subject.Subject;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.google.code.kaptcha.Producer;

import web.template.entity.common.Result;

@RestController
@RequestMapping("/session")
public class SessionController extends BaseController {
	/**
	 * 谷歌提供的验证码生成器
	 */
	@Autowired
	private Producer defaultKaptcha;
	
	/**
	 * 登出功能
	 * 
	 * @return
	 */
	@RequestMapping("/logout")
	public Result logout() {
		Subject subject = SecurityUtils.getSubject();
		subject.logout();
		return new Result(0, null, null);
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
	
	
}
