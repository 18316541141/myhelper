using CommonHelper.Helper;
using CommonHelper.Helper.CommonEntity;
using CommonWeb.Filter.Common;
using CommonWeb.Intf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Mvc;
namespace WebApplication1.Controllers.Common
{
    /// <summary>
    /// 通用控制器
    /// </summary>
    public sealed partial class IndexController : FastController
    {
        /// <summary>
        /// 实时刷新的业务处理类
        /// </summary>
        public IRealTimeInitService RealTimeInitService { set; get; }

        /// <summary>
        /// 用户专用的接口类，提供用户操作的业务逻辑
        /// </summary>
        public IUserService UserService { set; get; }

        /// <summary>
        /// 系统业务接口类，提供系统的信息
        /// </summary>
        public ISystemService SystemService { set; get; }

        /// <summary>
        /// 用户和等待池表，确保每个用户只能在同一个等待池上等待。
        /// </summary>
        public static HashSet<string> UsernameAndPoolSet { set; get; }

        static IndexController()
        {
            UsernameAndPoolSet = new HashSet<string>();
        }

        /// <summary>
        /// 加载登陆后数据
        /// </summary>
        /// <returns></returns>
        [Compress]
        public JsonResult LoadLoginData()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("leftMenus", UserService.LoadLeftMenus(User.Identity.Name));
            data.Add("username", User.Identity.Name);
            return MyJson(new Result { code = 0, data = data });
        }

        /// <summary>
        /// 匿名的实时检测最新版本号，如果发现版本号发生变化，则马上
        /// 返回，如果没变化，则等待30秒或30秒内版本号发生变化后返回
        /// （需要传入认证参数：createDate（yyyyMMddHHmmss）、r（随机字符串）、）
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [Sign(new string[] { "realTimePool", "realTimeVersion", "createDate", "r" })]
        public JsonResult AnonymousRealTime(string realTimePool, string realTimeVersion,string signKey)
        {
            return RealTime(realTimePool, realTimeVersion,UserService.LoadUsernameBySignKey(signKey));
        }

        /// <summary>
        /// 实时刷新
        /// </summary>
        /// <param name="realTimePool"></param>
        /// <param name="realTimeVersion"></param>
        /// <param name="username"></param>
        /// <returns></returns>
		private JsonResult RealTime(string realTimePool,string realTimeVersion,string username)
		{
			if (RealTimeInitService.GetWaitPoolSet().Contains(realTimePool))
            {
                string newestVersion;
                bool initRet = ThreadHelper.CompareControllerVersion(realTimePool, realTimeVersion, out newestVersion);
                if (string.IsNullOrEmpty(realTimeVersion))
                {
                    initRet = RealTimeInitService.Init(realTimePool, username);
                }
                if (initRet)
                {
                    string usernameAndPoolKey = username + realTimePool;
                    if (UsernameAndPoolSet.Contains(usernameAndPoolKey))
                    {
                        return MyJson(new Result { code = -1, msg = "当前用户已有线程在等待池中，无法向等待池放入新线程。" });
                    }
                    else
                    {
                        lock (UsernameAndPoolSet)
                        {
                            if (UsernameAndPoolSet.Contains(usernameAndPoolKey))
                            {
                                return MyJson(new Result { code = -1, msg = "当前用户已有线程在等待池中，无法向等待池放入新线程。" });
                            }
                            else
                            {
                                UsernameAndPoolSet.Add(usernameAndPoolKey);
                            }
                        }
                    }
                    ThreadHelper.BatchWait(realTimePool, 60000);
                    lock (UsernameAndPoolSet)
                    {
                        UsernameAndPoolSet.Remove(usernameAndPoolKey);
                    }
					initRet = ThreadHelper.CompareControllerVersion(realTimePool, realTimeVersion, out newestVersion);
                    return MyJson(new Result { code = (short)(initRet?1:0),data=new Dictionary<string, string>
                        {
                            ["realTimePool"]= realTimePool,
                            ["realTimeVersion"]= newestVersion
                        }
                    });
                }
                else
                {
                    return MyJson(new Result { code = 0 ,data = new Dictionary<string, string>
                        {
                            ["realTimePool"] = realTimePool,
                            ["realTimeVersion"] = newestVersion
                        }
                    });
                }
            }
            else
            {
                return MyJson(new Result { code = -1,msg = "该等待池未开放。" });
            }
		}

        /// <summary>
        /// 实时检测最新版本号，如果发现版本号发生变化，则马上
        /// 返回，如果没变化，则等待30秒或30秒内版本号发生变化后返回
        /// </summary>
        /// <param name="realTimePool">等待池名称</param>
        /// <param name="realTimeVersion">版本号</param>
        /// <returns>返回结果</returns>
        public JsonResult RealTime(string realTimePool,string realTimeVersion)
        {
			return RealTime(realTimePool,realTimeVersion,User.Identity.Name);   
        }

        /// <summary>
        /// 上传单张图片
        /// </summary>
        /// <returns></returns>
        [SizeFilter(3145728, "上传文件大小不能超过3M，可通过压缩减少文件大小。")]
        [OperCount(CountLimit = 10,ClearMillisecond = 60000)]
        public JsonResult UploadSingleImage(HttpPostedFileBase fileUpload)
        {
            Image image = null;
            try
            {
                image = Image.FromStream(fileUpload.InputStream);
            }
            catch
            {
                return MyJson(new Result { code = -1, msg = "上传的文件可能不是图片文件，无法处理。" });
            }
            try
            {
                string imgName = FileHelper.SaveImageBySha1(image, SystemService.UploadFilePath());
                using (Image img = Image.FromFile($"{SystemService.UploadFilePath()}{s}{imgName}"))
                {
                    using (Image thumbnailImg = img.GetThumbnailImage(150, img.Height * 150 / img.Width, () => false, IntPtr.Zero))
                    {
                        string thumbnailName = FileHelper.SaveImageBySha1(thumbnailImg, SystemService.UploadFilePath());
                        return MyJson(new Result { code = 0, data = new { thumbnailName = thumbnailName, imgName = imgName, imgWidth = img.Width, imgHeight = img.Height } });
                    }
                }
            }
            catch (Exception ex)
            {
                return MyJson(new Result { code = -1, msg = ex.Message });
            }
        }

        /// <summary>
        /// 多个文件上传
        /// </summary>
        /// <param name="fileUploads"></param>
        /// <param name="pathName"></param>
        /// <returns></returns>
        [SizeFilter(52428800, "上传文件大小不能超过50M，可通过压缩减少文件大小。")]
        [OperCount(CountLimit = 100, ClearMillisecond = 60000)]
        public JsonResult UploadFiles(HttpPostedFileBase fileUploads)
        {
            return MyJson(new Result { code = 0, data = FileHelper.SaveFileNameBySha1(fileUploads.InputStream, SystemService.UploadFilePath()) });
        }

        /// <summary>
        /// 切割单张图片
        /// </summary>
        /// <param name="imgName">图片名称</param>
        /// <param name="imgWidth">图片宽度</param>
        /// <param name="imgHeight">图片高度</param>
        /// <param name="x">左上角切点x坐标</param>
        /// <param name="y">左上角切点y坐标</param>
        /// <param name="w">切割宽度</param>
        /// <param name="h">切割高度</param>
        public JsonResult SingleImageCrop(string imgName, int imgWidth, int imgHeight, int x, int y, int w, int h)
        {
            string imgPath = $"{SystemService.UploadFilePath()}{s}{imgName}";
            if (System.IO.File.Exists(imgPath))
            {
                Bitmap bitmap = null;
                try
                {
                    bitmap = new Bitmap(imgPath);
                }
                catch
                {
                    return MyJson(new Result { code = -1, msg = "该文件可能不是图片文件，切割失败！" });
                }
                try
                {
                    using (bitmap)
                    {
                        using (Image cutImg = ImageHandleHelper.CutPicByRect(bitmap.GetThumbnailImage(imgWidth, imgHeight, () => false, IntPtr.Zero), x, y, w, h))
                        {

                            using (Image thumbnailImg = cutImg.GetThumbnailImage(150, cutImg.Height * 150 / cutImg.Width, () => false, IntPtr.Zero))
                            {
                                return MyJson(new Result
                                {
                                    code = 0,
                                    data = new
                                    {
                                        thumbnailName = FileHelper.SaveImageBySha1(thumbnailImg, SystemService.UploadFilePath()),
                                        imgName = FileHelper.SaveImageBySha1(cutImg, SystemService.UploadFilePath())
                                    }
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return MyJson(new Result { code = -1, msg = ex.Message });
                }
            }
            else
            {
                return MyJson(new Result { code = -1, msg = "图片不存在，切割失败！" });
            }
        }

		/// <summary>
        /// 允许匿名访问的显示上传的图片
        /// </summary>
        /// <param name="pathName">图片路径名称</param>
        /// <param name="imgName">图片名称</param>
        [AllowAnonymous]
        [Sign(new string[] { "createDate", "r" ,"imgName" })]
        [OutputCache(Duration = int.MaxValue)]
        public void AnonymousShowImage(string imgName)
        {
            ShowImage(imgName);
        }

        /// <summary>
        /// 显示上传的图片
        /// <param name="pathName">图片路径名称</param>
        /// <param name="imgName">图片名称</param>
        /// </summary>
        [OutputCache(Duration = int.MaxValue)]
        public void ShowImage(string imgName)
        {
            Response.ContentType = "image/jpeg";
            string imgPath = $"{SystemService.UploadFilePath()}{s}{imgName}";
            if (System.IO.File.Exists(imgPath))
            {
                using (Stream stream = System.IO.File.OpenRead(imgPath))
                {
                    StreamHelper.CopyStream(stream, Response.OutputStream, false);
                }
            }
            else
            {
                Response.StatusCode = 404;
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="pathName">文件路径名称</param>
        /// <param name="fileName">文件sha1名称</param>
        /// <param name="fileDesc">文件的中文描述</param>
        /// <returns></returns>
        [OutputCache(Duration = int.MaxValue)]
        public FilePathResult DownFile(string fileName, string fileDesc)
        {
            string filePath = $"{SystemService.UploadFilePath()}{s}{fileName}";
            if (System.IO.File.Exists(filePath))
            {
                return File(filePath, MimeMapping.GetMimeMapping(filePath), fileDesc);
            }
            else
            {
                Response.StatusCode = 404;
                return null;
            }
        }
    }
}