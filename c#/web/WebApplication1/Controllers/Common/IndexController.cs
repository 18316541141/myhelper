using CommonHelper.Helper;
using CommonHelper.Helper.CommonEntity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Entity.Common;
using WebApplication1.Filter.Common;
using WebApplication1.Service;
namespace WebApplication1.Controllers.Common
{
    /// <summary>
    /// 通用控制器
    /// </summary>
    public class IndexController : FastController
    {
        public SystemService SystemService { set; get; }

        public RealTimeInitService RealTimeInitService { set; get; }

        /// <summary>
        /// 上传文件所允许的路径
        /// </summary>
        public static HashSet<string> _allowPath { set; get; }

        /// <summary>
        /// 等待池表
        /// </summary>
        public static HashSet<string> WaitPoolSet { set; get; }

        /// <summary>
        /// 用户和等待池表，确保每个用户只能在同一个等待池上等待。
        /// </summary>
        public static HashSet<string> UsernameAndPoolSet { set; get; }

        static IndexController()
        {
            _allowPath = new HashSet<string>();
            _allowPath.Add("test");

            WaitPoolSet = new HashSet<string>();
            WaitPoolSet.Add("newsAlarm");

            UsernameAndPoolSet = new HashSet<string>();
        }

        /// <summary>
        /// 加载登陆后数据
        /// </summary>
        /// <returns></returns>
        public JsonResult LoadLoginData()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("leftMenus", SystemService.LoadLeftMenus());
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
            return RealTime(realTimePool, realTimeVersion,/*SystemService.LoadUsernameBySignKey(signKey)*/"zhang");
        }


		private JsonResult RealTime(string realTimePool,string realTimeVersion,string username)
		{
			if (WaitPoolSet.Contains(realTimePool))
            {
                string newestVersion;
                bool initRet = ThreadHelper.CompareControllerVersion(realTimePool, realTimeVersion, out newestVersion);
                if (realTimeVersion == null)
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
                    return MyJson(new Result { code = initRet?1:0,data=new Dictionary<string, string>
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
        /// 加载最新提醒消息
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public JsonResult LoadNewsAlarm()
        {
            return MyJson(new Result { code = 0, data = SystemService.LoadNewsAlarm() });
        }

        /// <summary>
        /// 加载树节点测试方法，正式上线时删除
        /// </summary>
        /// <returns></returns>
        public JsonResult LoadTreeNode()
        {
            List<TreeFormNode> treeNodeList = new List<TreeFormNode>();
            treeNodeList.Add(new TreeFormNode
            {
                Id = "01",
                Name = "广东"
            });
            treeNodeList.Add(new TreeFormNode
            {
                Id = "02",
                Name = "佛山"
            });
            TreeFormNode treeNode;
            treeNodeList.Add(treeNode = new TreeFormNode
            {
                Id = "03",
                Name = "顺德"
            });
            treeNode.Children.Add(new TreeFormNode
            {
                Id = "31",
                Name = "陈村"
            });
            treeNode.Children.Add(new TreeFormNode
            {
                Id = "32",
                Name = "陳村"
            });
            treeNode.Children.Add(new TreeFormNode
            {
                Id = "33",
                Name = "陳邨"
            });
            treeNode.Children.Add(new TreeFormNode
            {
                Id = "34",
                Name = "陈村1"
            });
            treeNode.Children.Add(new TreeFormNode
            {
                Id = "35",
                Name = "陳村1"
            });
            treeNode.Children.Add(new TreeFormNode
            {
                Id = "36",
                Name = "陳邨1"
            });
            treeNode.Children.Add(new TreeFormNode
            {
                Id = "37",
                Name = "陈村2"
            });
            treeNode.Children.Add(new TreeFormNode
            {
                Id = "38",
                Name = "陳村2"
            });
            treeNode.Children.Add(new TreeFormNode
            {
                Id = "39",
                Name = "陳邨2"
            });
            treeNode.Children.Add(new TreeFormNode
            {
                Id = "40",
                Name = "陈村3"
            });
            treeNode.Children.Add(new TreeFormNode
            {
                Id = "41",
                Name = "陳村3"
            });
            treeNode.Children.Add(new TreeFormNode
            {
                Id = "42",
                Name = "陳邨3"
            });
            treeNode.Children.Add(new TreeFormNode
            {
                Id = "43",
                Name = "陈村4"
            });
            treeNode.Children.Add(new TreeFormNode
            {
                Id = "44",
                Name = "陳村4"
            });
            treeNode.Children.Add(new TreeFormNode
            {
                Id = "45",
                Name = "陳邨4"
            });
            treeNode.Children.Add(new TreeFormNode
            {
                Id = "46",
                Name = "陈村5"
            });
            treeNode.Children.Add(new TreeFormNode
            {
                Id = "47",
                Name = "陳村5"
            });
            treeNode.Children.Add(new TreeFormNode
            {
                Id = "48",
                Name = "陳邨5"
            });
            return MyJson(new Result { code = 0, data = treeNodeList });
        }

        /// <summary>
        /// 上传单张图片
        /// </summary>
        /// <returns></returns>
        [SizeFilter(Size = 3145728, Msg = "上传文件大小不能超过3M，可通过压缩减少文件大小。")]
        [OperCount(CountLimit = 10,ClearMillisecond = 60000)]
        public JsonResult UploadSingleImage(HttpPostedFileBase fileUpload, string pathName)
        {
            if (_allowPath.Contains(pathName))
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
                    string imgName = FileHelper.SaveImageBySha1(image, $"{Server.MapPath("~/uploadFiles/")}{pathName}");
                    using (Image img = Image.FromFile($"{Server.MapPath("~/uploadFiles/")}{pathName}{Path.DirectorySeparatorChar}{imgName}"))
                    {
                        using (Image thumbnailImg = img.GetThumbnailImage(150, img.Height * 150 / img.Width, () => false, IntPtr.Zero))
                        {
                            string thumbnailName = FileHelper.SaveImageBySha1(thumbnailImg, $"{Server.MapPath("~/uploadFiles/")}{pathName}");
                            return MyJson(new Result { code = 0, data = new { thumbnailName = thumbnailName, imgName = imgName, imgWidth = img.Width, imgHeight = img.Height } });
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
                return MyJson(new Result { code = -1, msg = "该路径不允许上传文件。" });
            }
        }

        /// <summary>
        /// 多个文件上传
        /// </summary>
        /// <param name="fileUploads"></param>
        /// <param name="pathName"></param>
        /// <returns></returns>
        [SizeFilter(Size = 52428800, Msg = "上传文件大小不能超过50M，可通过压缩减少文件大小。")]
        [OperCount(CountLimit = 100, ClearMillisecond = 60000)]
        public JsonResult UploadFiles(HttpPostedFileBase fileUploads, string pathName)
        {
            if (_allowPath.Contains(pathName))
            {
                return MyJson(new Result { code = 0, data = FileHelper.SaveFileNameBySha1(fileUploads.InputStream, $"{Server.MapPath("~/uploadFiles/")}{pathName}") });
            }
            else
            {
                return MyJson(new Result { code = -1, msg = "该路径不允许上传文件。" });
            }
        }

        /// <summary>
        /// 切割单张图片
        /// </summary>
        /// <param name="pathName">图片路径名称</param>
        /// <param name="imgName">图片名称</param>
        /// <param name="imgWidth">图片宽度</param>
        /// <param name="imgHeight">图片高度</param>
        /// <param name="x">左上角切点x坐标</param>
        /// <param name="y">左上角切点y坐标</param>
        /// <param name="w">切割宽度</param>
        /// <param name="h">切割高度</param>
        public JsonResult SingleImageCrop(string pathName, string imgName, int imgWidth, int imgHeight, int x, int y, int w, int h)
        {
            string imgPath = $"{Server.MapPath("~/uploadFiles/")}{pathName}{Path.DirectorySeparatorChar}{imgName}";
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
                                        thumbnailName = FileHelper.SaveImageBySha1(thumbnailImg, $"{Server.MapPath("~/uploadFiles/")}{pathName}"),
                                        imgName = FileHelper.SaveImageBySha1(cutImg, $"{Server.MapPath("~/uploadFiles/")}{pathName}")
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
        [Sign(new string[] { "createDate", "r" , "pathName", "imgName" })]
        [OutputCache(Duration = int.MaxValue)]
        public void AnonymousShowImage(string pathName, string imgName)
        {
            ShowImage(pathName, imgName);
        }

        /// <summary>
        /// 显示上传的图片
        /// <param name="pathName">图片路径名称</param>
        /// <param name="imgName">图片名称</param>
        /// </summary>
        [OutputCache(Duration = int.MaxValue)]
        public void ShowImage(string pathName, string imgName)
        {
            Response.ContentType = "image/jpeg";
            string imgPath = $"{Server.MapPath("~/uploadFiles/")}{pathName}{Path.DirectorySeparatorChar}{imgName}";
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
        /// 加载地区选择json
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = int.MaxValue)]
        public JsonResult AreaSelect()
        {
            return MyJson(new Result { code = 0, data = SystemService.LoadAreaTree() });
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="pathName">文件路径名称</param>
        /// <param name="fileName">文件sha1名称</param>
        /// <param name="fileDesc">文件的中文描述</param>
        /// <returns></returns>
        [OutputCache(Duration = int.MaxValue)]
        public FilePathResult DownFile(string pathName, string fileName, string fileDesc)
        {
            string filePath = $"{Server.MapPath("~/uploadFiles/")}{pathName}{Path.DirectorySeparatorChar}{fileName}";
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