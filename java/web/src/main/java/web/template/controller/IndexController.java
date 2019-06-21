package web.template.controller;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

import javax.imageio.ImageIO;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.commons.io.FileUtils;
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
import com.txj.common.FileHelper;

import web.template.entity.Result;
import web.template.entity.TreeFormNode;
import web.template.entity.UploadFilesResult;
import web.template.service.SystemService;
@RestController
@RequestMapping("/index")
public class IndexController {
	
	@Autowired
	private Producer defaultKaptcha;
	
	@Autowired
	private SystemService systemService;
	
	Set<String> allowPath;
	
	public IndexController(){
		allowPath=new HashSet<String>();
		allowPath.add("test");
	}
	
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
	
	/**
	 * 上次单张图片
	 * @param fileUpload	上传图片
	 * @param request	请求对象
	 * @param pathName	路径名称
	 * @return
	 */
	@RequestMapping("/uploadSingleImage")
	public Result uploadSingleImage(@RequestParam("fileUpload")MultipartFile fileUpload,HttpServletRequest request,String pathName){
		if(allowPath.contains(pathName)){			
			try {
				File target=new ServletContextResource(request.getServletContext(), "/WEB-INF/uploadFiles/"+pathName+"/").getFile();
				FileHelper.SaveInputStreamBySha1(fileUpload.getInputStream(), target.getAbsolutePath());
				return null;
			} catch (IllegalStateException e) {
				e.printStackTrace();
				throw new RuntimeException(e);
			} catch (IOException e) {
				e.printStackTrace();
				throw new RuntimeException(e);
			}
		}else{
			return new Result(-1, "该目录不允许上传！", null);
		}
	}
	
	/**
	 * 上传多个文件
	 * @param fileUploads	上传的文件
	 * @param pathName	路径名称
	 * @return
	 */
	public Result uploadFiles(@RequestParam("fileUpload")MultipartFile[] fileUploads, HttpServletRequest request,String pathName){
		if (allowPath.contains(pathName))
        {
			List<UploadFilesResult> uploadFilesResultList=new ArrayList<>();
			UploadFilesResult uploadFilesResult;
			for (MultipartFile multipartFile : fileUploads) {
				uploadFilesResult=new UploadFilesResult();
				File target;
				try {
					target = new ServletContextResource(request.getServletContext(), "/WEB-INF/uploadFiles/"+pathName+"/").getFile();
					uploadFilesResult.setSha1(FileHelper.SaveInputStreamBySha1(multipartFile.getInputStream(), target.getAbsolutePath()));
					multipartFile.getOriginalFilename();
					uploadFilesResult.setExtension("");
					uploadFilesResultList.add(uploadFilesResult);
				} catch (IOException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
			}
        }
		return null;
	}
	
	
	@RequestMapping("/loadTreeNode")
	public Result loadTreeNode()
    {
        List<TreeFormNode> treeNodeList = new ArrayList<TreeFormNode>();
        TreeFormNode t01=new TreeFormNode();
        {
        	t01.setId("01");
        	t01.setName("广东"); 
        }
        treeNodeList.add(t01);
        TreeFormNode t02=new TreeFormNode();
        {
        	t02.setId("02");
        	t02.setName("佛山");
        }
        treeNodeList.add(t02);
        TreeFormNode treeNode = new TreeFormNode();
        {
        	treeNode.setId("03");
        	treeNode.setName("顺德");
        }
        treeNodeList.add(treeNode);
        TreeFormNode t31=new TreeFormNode();
        {
        	t31.setId("31");
        	t31.setName("陈村");
        }
        treeNode.getChildren().add(t31);
        TreeFormNode t32=new TreeFormNode();
        {
        	t32.setId("32");
        	t32.setName("陳村");
        }
        treeNode.getChildren().add(t32);
        TreeFormNode t33=new TreeFormNode();
        {
        	t33.setId("33");
        	t33.setName("陳邨");
        }
        treeNode.getChildren().add(t33);
        return new Result (0,null ,treeNodeList );
    }
}