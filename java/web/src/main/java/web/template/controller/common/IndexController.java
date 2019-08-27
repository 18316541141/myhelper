package web.template.controller.common;
import java.awt.Image;
import java.awt.Rectangle;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;
import javax.imageio.ImageIO;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import org.apache.commons.io.IOUtils;
import org.apache.shiro.SecurityUtils;
import org.apache.shiro.subject.Subject;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.util.FileCopyUtils;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.multipart.MultipartFile;

import com.txj.common.FileHelper;
import com.txj.common.ImageHandleHelper;
import com.txj.common.ThreadHelper;

import web.template.entity.common.Result;
import web.template.entity.common.TreeFormNode;
import web.template.service.common.RealTimeInitService;
import web.template.service.common.SystemService;
@RestController
@RequestMapping("/index")
public class IndexController extends BaseController {
	
	@Autowired
	private SystemService systemService;
	
	@Autowired
	private RealTimeInitService realTimeInitService;
	
	private Set<String> allowPath;
	
	/**
	 * 等待池表
	 */
	public Set<String> WaitPoolSet;
	
	/**
	 * 用户和等待池表，确保每个用户只能在同一个等待池上等待。
	 */
	public HashSet<String> UsernameAndPoolSet;
	
	public IndexController(){
		WaitPoolSet=new HashSet<String>();
		WaitPoolSet.add("newsAlarm");
		
		allowPath=new HashSet<String>();
		allowPath.add("test");
		
		UsernameAndPoolSet = new HashSet<String>();
	}
	
	/**
	 * 
	 * @param realTimePool
	 * @param realTimeVersion
	 * @param username
	 * @return
	 */
	private Result realTime(String realTimePool,String realTimeVersion,String username){
		if(WaitPoolSet.contains(realTimePool)){
			String[] newestVersion=new String[1];
			boolean initRet = ThreadHelper.compareControllerVersion(realTimePool, realTimeVersion, newestVersion);
			if (realTimeVersion == null){
				initRet = realTimeInitService.init(realTimePool,username);
			}
			Map<String,String> data=new HashMap<String, String>();
			if (initRet){
				ThreadHelper.batchWait(realTimePool, 60000);
				initRet = ThreadHelper.compareControllerVersion(realTimePool, realTimeVersion, newestVersion);
				data.put("realTimePool", realTimePool);
				data.put("realTimeVersion", newestVersion[0]);
				return new Result(1,null,data);
			}else{
				data.put("realTimePool", realTimePool);
				data.put("realTimeVersion", newestVersion[0]);
				return new Result(0,null,data);
			}
		}else{
			return new Result(-1,"该等待池未开放。",null);
		}
	}
	
	/**
	 * 
	 * @param realTimePool
	 * @param realTimeVersion
	 * @param signKey
	 * @return
	 */
	public Result anonymousRealTime(String realTimePool, String realTimeVersion, String signKey)
    {
        return realTime(realTimePool, realTimeVersion,/*systemService.LoadUsernameBySignKey(signKey)*/"zhang");
    }
	
	/**
	 * 
	 * @param realTimePool
	 * @param realTimeVersion
	 * @return
	 */
	@RequestMapping("/realTime")
	public Result realTime(String realTimePool,String realTimeVersion){
		return realTime(realTimePool,realTimeVersion,username());
	}
	
	/**
	 * 加载地区选择json
	 * @return
	 */
	public Result areaSelect(){
		return null;
	}
	
	/**
	 * 登陆后加载基础数据
	 * @return
	 */
	@RequestMapping("/loadLoginData")
	public Result loadLoginData(){
		Map<String, Object> data = new HashMap<String, Object>();
        data.put("leftMenus", systemService.loadLeftMenus(username()));
		return new Result(0,null,data);
	}
	
	/**
	 * 加载最新消息提醒
	 * @return
	 */
	@RequestMapping("/loadNewsAlarm")
	public Result loadNewsAlarm()
    {
        return new Result(0,null,systemService.loadNewsAlarm());
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
		return new Result(0,"",systemService.loadLeftMenus(username()));
	}
	
	/**
	 * 显示图片
	 * @param pathName
	 * @param imgName
	 */
	@RequestMapping("/showImage")
	public Result showImage(String pathName,String imgName,HttpServletRequest request,HttpServletResponse response){
		if(allowPath.contains(pathName)){
			InputStream inputStream;
			try {
				File imgFile=realFile("/WEB-INF/uploadFiles/"+pathName+"/"+imgName);
				if(imgFile.exists()){
					inputStream = new FileInputStream(imgFile);
					response.addHeader("Cache-control","max-age="+Integer.MAX_VALUE);
					response.setContentType("image/jpeg");
					FileCopyUtils.copy(inputStream, response.getOutputStream());					
				}else{
					response.setStatus(404);
				}
			} catch (IOException e) {
				e.printStackTrace();
			}
			return null;
		}else {
			return new Result(-1, "该目录不允许查看", null);
		}
	}
	
	/**
	 * 下载文件
	 * @param pathName
	 * @param fileName
	 * @param fileDesc
	 */
	@RequestMapping("/downFile")
	public void downFile(String pathName, String fileName, String fileDesc,HttpServletResponse response){
		File file=realFile("/WEB-INF/uploadFiles/"+pathName+"/"+fileName);
		if(allowPath.contains(pathName) && file.exists()){
			try (FileInputStream fileInputStream=new FileInputStream(file)){
				response.setContentType("application/octet-stream");
				response.setHeader("Content-disposition", "attachment; filename="+ new String(fileDesc.getBytes("utf-8"), "ISO8859-1"));
				IOUtils.copy(fileInputStream, response.getOutputStream());
			} catch (FileNotFoundException e) {
				e.printStackTrace();
			} catch (IOException e) {
				e.printStackTrace();
			}
		}else{
			response.setStatus(404);
		}
	}
	
	/**
	 * 切割单张图片
	 * @param pathName	图片路径名称
	 * @param imgName	图片名称
	 * @param imgWidth	图片宽度
	 * @param imgHeight	图片高度
	 * @param x	左上角切点x坐标
	 * @param y	左上角切点y坐标
	 * @param w	切割宽度
	 * @param h	切割高度
	 * @return	返回切割结果，thumbnailName（切割图）、imgName（缩略图）、
	 */
	@RequestMapping("/singleImageCrop")
	public Result singleImageCrop(String pathName, String imgName,int imgWidth,int imgHeight,int x,int y,int w,int h,HttpServletRequest request){
		try {
			File file=realFile("/WEB-INF/uploadFiles/"+pathName+"/"+imgName);
			if(file.exists()){
				Image scaleImage=ImageHandleHelper.scale(ImageIO.read(file), imgWidth, imgHeight);
				Image cutImage=ImageHandleHelper.cutPicByRect(scaleImage, new Rectangle(x, y, w, h));
				Image thumbnailImg=ImageHandleHelper.scale(cutImage, 150, cutImage.getHeight(null) * 150 / cutImage.getWidth(null));
				Map<String,String> data=new HashMap<String, String>();
				data.put("imgName", FileHelper.SaveImageBySha1(cutImage, realPath("/WEB-INF/uploadFiles/"+pathName)));
				data.put("thumbnailName", FileHelper.SaveImageBySha1(thumbnailImg, realPath("/WEB-INF/uploadFiles/"+pathName)));
				return new Result(0, null, data);
			}else{
				return new Result(-1,"图片不存在，切割失败！",null);
			}
		} catch (IOException e) {
			e.printStackTrace();
			return new Result(-1,e.getMessage(),null);
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
				File target=realFile("/WEB-INF/uploadFiles/"+pathName+"/");
				String sha1=FileHelper.SaveInputStreamBySha1(fileUpload.getInputStream(), target.getAbsolutePath());
				Image image=ImageIO.read(new File(target.getAbsoluteFile()+File.separator+sha1));
				Image scaleImg=ImageHandleHelper.scale(image, 150, image.getHeight(null) * 150 / image.getWidth(null));
				String thumbnailName=FileHelper.SaveImageBySha1(scaleImg, target.getAbsolutePath());
				Map<String, Object> dataMap=new HashMap<>();
				dataMap.put("thumbnailName", thumbnailName);
				dataMap.put("imgName", sha1);
				dataMap.put("imgWidth", image.getWidth(null));
				dataMap.put("imgHeight", image.getHeight(null));
				return new Result(0, null, dataMap);
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
	@RequestMapping("/uploadFiles")
	public Result uploadFiles(@RequestParam("fileUploads")MultipartFile fileUploads, HttpServletRequest request,String pathName){
		if (allowPath.contains(pathName))
        {
			try {
				File target = realFile("/WEB-INF/uploadFiles/"+pathName+"/");
				return new Result(0, null, FileHelper.SaveInputStreamBySha1(fileUploads.getInputStream(), target.getAbsolutePath()));
			} catch (IOException e) {
				e.printStackTrace();
				return new Result(-1,e.getMessage(),null);
			}
		}else{			
			return new Result(-1, "该路径不允许上传文件。", null);
		}
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