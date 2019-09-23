package web.template.controller.common;
import java.awt.Image;
import java.awt.Rectangle;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;
import javax.imageio.ImageIO;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import org.apache.commons.io.IOUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.util.FileCopyUtils;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.multipart.MultipartFile;
import com.txj.common.FileHelper;
import com.txj.common.ImageHandleHelper;
import com.txj.common.ThreadHelper;
import com.txj.common.entity.Result;

import web.template.intf.IRealTimeInitService;
import web.template.intf.IUserService;
@RestController
@RequestMapping("/index")
public class IndexController extends BaseController {

	@Autowired
	private IUserService userService;
	
	@Autowired
	private IRealTimeInitService realTimeInitService;
	
	/**
	 * 等待池表
	 */
	public Set<String> waitPoolSet;
	
	/**
	 * 用户和等待池表，确保每个用户只能在同一个等待池上等待。
	 */
	public final static HashSet<String> USERNAME_AND_POOL_SET;

	static {
		USERNAME_AND_POOL_SET = new HashSet<String>();
	}

	public IndexController() {
		waitPoolSet = new HashSet<String>();
		waitPoolSet.add("newsAlarm");
	}

	/**
	 * 实时挂载线程到等待池
	 * @param realTimePool	
	 * @param realTimeVersion
	 * @param username
	 * @return
	 */
	private final Result realTime(final String realTimePool, final String realTimeVersion, final String username) {
		if (waitPoolSet.contains(realTimePool)) {
			final String[] newestVersion = new String[1];
			boolean initRet = ThreadHelper.compareControllerVersion(realTimePool, realTimeVersion, newestVersion);
			if (realTimeVersion == null) {
				initRet = realTimeInitService.init(realTimePool, username);
			}
			final Map<String, String> data = new HashMap<String, String>();
			if (initRet) {
				final String usernameAndPoolKey = username + realTimePool;
				if (USERNAME_AND_POOL_SET.contains(usernameAndPoolKey)) {
					return new Result(-1, "当前用户已有线程在等待池中，无法向等待池放入新线程。", null);
				} else {
					synchronized (USERNAME_AND_POOL_SET) {
						if (USERNAME_AND_POOL_SET.contains(usernameAndPoolKey)) {
							return new Result(-1, "当前用户已有线程在等待池中，无法向等待池放入新线程。", null);
						} else {
							USERNAME_AND_POOL_SET.add(usernameAndPoolKey);
						}
					}
				}
				ThreadHelper.batchWait(realTimePool, 60000);
				synchronized (USERNAME_AND_POOL_SET) {
					USERNAME_AND_POOL_SET.remove(usernameAndPoolKey);
				}
				initRet = ThreadHelper.compareControllerVersion(realTimePool, realTimeVersion, newestVersion);
				data.put("realTimePool", realTimePool);
				data.put("realTimeVersion", newestVersion[0]);
				return new Result(1, null, data);
			} else {
				data.put("realTimePool", realTimePool);
				data.put("realTimeVersion", newestVersion[0]);
				return new Result(0, null, data);
			}
		} else {
			return new Result(-1, "该等待池未开放。", null);
		}
	}

	/**
	 * 
	 * @param realTimePool
	 * @param realTimeVersion
	 * @param signKey
	 * @return
	 */
	@RequestMapping("/anonymousRealTime")
	public final Result anonymousRealTime(final String realTimePool, final String realTimeVersion, final String signKey) {
		return realTime(realTimePool, realTimeVersion,
				/* systemService.LoadUsernameBySignKey(signKey) */"zhang");
	}

	/**
	 * 
	 * @param realTimePool
	 * @param realTimeVersion
	 * @return
	 */
	@RequestMapping("/realTime")
	public final Result realTime(String realTimePool, String realTimeVersion) {
		return realTime(realTimePool, realTimeVersion, username());
	}

	/**
	 * 加载地区选择json
	 * 
	 * @return
	 */
	public Result areaSelect() {
		return null;
	}

	/**
	 * 登陆后加载基础数据
	 * 
	 * @return
	 */
	@RequestMapping("/loadLoginData")
	public Result loadLoginData() {
		Map<String, Object> data = new HashMap<String, Object>();
		data.put("leftMenus", userService.loadLeftMenus(username()));
		return new Result(0, null, data);
	}

	/**
	 * 获取左侧菜单
	 * 
	 * @return
	 */
	@RequestMapping("/loadLeftMenus")
	public Result loadLeftMenus() {
		return new Result(0, "", userService.loadLeftMenus(username()));
	}

	/**
	 * 显示图片
	 * 
	 * @param imgName
	 */
	@RequestMapping("/showImage")
	public Result showImage(String imgName, HttpServletRequest request, HttpServletResponse response) {
		InputStream inputStream;
		try {
			File imgFile = realFile("/WEB-INF/uploadFiles/" + imgName);
			if (imgFile.exists()) {
				inputStream = new FileInputStream(imgFile);
				response.addHeader("Cache-control", "max-age=" + Integer.MAX_VALUE);
				response.setContentType("image/jpeg");
				FileCopyUtils.copy(inputStream, response.getOutputStream());
			} else {
				response.setStatus(404);
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
		return null;
	}

	/**
	 * 下载文件
	 * 
	 * @param pathName
	 * @param fileName
	 * @param fileDesc
	 */
	@RequestMapping("/downFile")
	public void downFile(String fileName, String fileDesc, HttpServletResponse response) {
		File file = realFile("/WEB-INF/uploadFiles/" + fileName);
		try (FileInputStream fileInputStream = new FileInputStream(file)) {
			response.setContentType("application/octet-stream");
			response.setHeader("Content-disposition",
					"attachment; filename=" + new String(fileDesc.getBytes("utf-8"), "ISO8859-1"));
			IOUtils.copy(fileInputStream, response.getOutputStream());
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}

	/**
	 * 切割单张图片
	 * 
	 * @param imgName
	 *            图片名称
	 * @param imgWidth
	 *            图片宽度
	 * @param imgHeight
	 *            图片高度
	 * @param x
	 *            左上角切点x坐标
	 * @param y
	 *            左上角切点y坐标
	 * @param w
	 *            切割宽度
	 * @param h
	 *            切割高度
	 * @return 返回切割结果，thumbnailName（切割图）、imgName（缩略图）、
	 */
	@RequestMapping("/singleImageCrop")
	public Result singleImageCrop(String imgName, int imgWidth, int imgHeight, int x, int y, int w,
			int h, HttpServletRequest request) {
		try {
			File file = realFile("/WEB-INF/uploadFiles/" + imgName);
			if (file.exists()) {
				Image scaleImage = ImageHandleHelper.scale(ImageIO.read(file), imgWidth, imgHeight);
				Image cutImage = ImageHandleHelper.cutPicByRect(scaleImage, new Rectangle(x, y, w, h));
				Image thumbnailImg = ImageHandleHelper.scale(cutImage, 150,
						cutImage.getHeight(null) * 150 / cutImage.getWidth(null));
				Map<String, String> data = new HashMap<String, String>();
				data.put("imgName", FileHelper.SaveImageBySha1(cutImage, realPath("/WEB-INF/uploadFiles/")));
				data.put("thumbnailName",
						FileHelper.SaveImageBySha1(thumbnailImg, realPath("/WEB-INF/uploadFiles/")));
				return new Result(0, null, data);
			} else {
				return new Result(-1, "图片不存在，切割失败！", null);
			}
		} catch (IOException e) {
			e.printStackTrace();
			return new Result(-1, e.getMessage(), null);
		}
	}

	/**
	 * 上次单张图片
	 * 
	 * @param fileUpload
	 *            上传图片
	 * @param request
	 *            请求对象
	 * @param pathName
	 *            路径名称
	 * @return
	 */
	@RequestMapping("/uploadSingleImage")
	public Result uploadSingleImage(@RequestParam("fileUpload") MultipartFile fileUpload, HttpServletRequest request) {
		try {
			File target = realFile("/WEB-INF/uploadFiles/");
			String sha1 = FileHelper.SaveInputStreamBySha1(fileUpload.getInputStream(), target.getAbsolutePath());
			Image image = ImageIO.read(new File(target.getAbsoluteFile() + File.separator + sha1));
			Image scaleImg = ImageHandleHelper.scale(image, 150,
					image.getHeight(null) * 150 / image.getWidth(null));
			String thumbnailName = FileHelper.SaveImageBySha1(scaleImg, target.getAbsolutePath());
			Map<String, Object> dataMap = new HashMap<>();
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
	}

	/**
	 * 上传多个文件
	 * 
	 * @param fileUploads
	 *            上传的文件
	 * @param pathName
	 *            路径名称
	 * @return
	 */
	@RequestMapping("/uploadFiles")
	public Result uploadFiles(@RequestParam("fileUploads") MultipartFile fileUploads, HttpServletRequest request) {
		try {
			File target = realFile("/WEB-INF/uploadFiles/");
			return new Result(0, null,
					FileHelper.SaveInputStreamBySha1(fileUploads.getInputStream(), target.getAbsolutePath()));
		} catch (IOException e) {
			e.printStackTrace();
			return new Result(-1, e.getMessage(), null);
		}
	}
}