
using CommonHelper.Helper;
using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using WebApplication1.Entity;
using WebApplication1.filter;
using WebApplication1.Params;
using WebApplication1.Repository;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    public class IndexController : BaseController
    {
        SystemService _systemService;

        /// <summary>
        /// 上传文件所允许的路径
        /// </summary>
        HashSet<string> _allowPath { set; get; }

        public IndexController()
        {
            _systemService = new SystemService();
            _allowPath = new HashSet<string>();
            _allowPath.Add("test");
        }

        /// <summary>
        /// 验证码获取
        /// </summary>
        [AllowAnonymous]
        public void VerificationCode()
        {
            string vercode;
            using (Bitmap vercodeImg = VerifyCodeHelper.CreateVerifyCodeBmp(out vercode))
            {
                Session["vercode"] = vercode;
                vercodeImg.Save(Response.OutputStream, ImageFormat.Jpeg);
            }
        }

        /// <summary>
        /// 加载左侧菜单
        /// </summary>
        /// <returns></returns>
        public JsonResult LoadLeftMenus()
        {
            return MyJson(new Result {code=0,data= _systemService.LoadLeftMenus() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public JsonResult Logout()
        {
            FormsAuthentication.SignOut();
            return MyJson(new Result { code = 0}, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 实时加载最新消息
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        [RealTime]
        public JsonResult LoadNewsAlarm()
        {
            List<NewsAlarm> newsAlarmList = new List<NewsAlarm>();
            return MyJson(new Result { code=0,data= newsAlarmList }, JsonRequestBehavior.AllowGet);
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
                id="01",
                name = "广东"
            });
            treeNodeList.Add(new TreeFormNode
            {
                id = "02",
                name = "佛山"
            });
            TreeFormNode treeNode;
            treeNodeList.Add(treeNode = new TreeFormNode
            {
                id = "03",
                name = "顺德"
            });
            treeNode.children.Add(new TreeFormNode
            {
                id = "31",
                name = "陈村"
            });
            treeNode.children.Add(new TreeFormNode
            {
                id = "32",
                name = "陳村"
            });
            treeNode.children.Add(new TreeFormNode
            {
                id = "33",
                name = "陳邨"
            });
            return MyJson(new Result { code = 0 ,data= treeNodeList }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 上传单张图片
        /// </summary>
        /// <returns></returns>
        [SizeFilter(Size= 3145728, Msg = "上传文件大小不能超过3M，可通过压缩减少文件大小。")]
        public JsonResult UploadSingleImage(HttpPostedFileBase fileUpload, string pathName)
        {
            if (_allowPath.Contains(pathName))
            {
                Image image=null;
                try
                {
                    image =Image.FromStream(fileUpload.InputStream);
                }
                catch
                {
                    return MyJson(new Result { code = -1, msg = "上传的文件可能不是图片文件，无法处理。" }, JsonRequestBehavior.AllowGet);
                }
                try
                {
                    string imgName=FileHelper.SaveImageBySha1(image, $"{Server.MapPath("~/uploadFiles/")}{pathName}");
                    using (Image img = Image.FromFile($"{Server.MapPath("~/uploadFiles/")}{pathName}{Path.DirectorySeparatorChar}{imgName}"))
                    {
                        using (Image thumbnailImg = img.GetThumbnailImage(150, img.Height * 150 / img.Width, () => false, IntPtr.Zero))
                        {
                            string thumbnailName= FileHelper.SaveImageBySha1(thumbnailImg, $"{Server.MapPath("~/uploadFiles/")}{pathName}");
                            return MyJson(new Result { code = 0, data = new { thumbnailName = thumbnailName, imgName = imgName, imgWidth = img.Width, imgHeight = img.Height } }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch(Exception ex)
                {
                    return MyJson(new Result { code = -1, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return MyJson(new Result { code = -1, msg = "该路径不允许上传文件。" }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 多个文件上传
        /// </summary>
        /// <param name="fileUploads"></param>
        /// <param name="pathName"></param>
        /// <returns></returns>
        [SizeFilter(Size = 52428800, Msg = "上传文件大小不能超过50M，可通过压缩减少文件大小。")]
        public JsonResult UploadFiles(HttpPostedFileBase fileUploads, string pathName)
        {
            if (_allowPath.Contains(pathName))
            {
                return MyJson(new Result { code = 0, data = FileHelper.SaveFileNameBySha1(fileUploads.InputStream, $"{Server.MapPath("~/uploadFiles/")}{pathName}") }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return MyJson(new Result { code = -1, msg = "该路径不允许上传文件。" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult SingleImageCrop(string pathName, string imgName,int imgWidth,int imgHeight,int x,int y,int w,int h)
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
                    return MyJson(new Result { code = -1, msg = "该文件可能不是图片文件，切割失败！" }, JsonRequestBehavior.AllowGet);
                }
                try
                {
                    using (bitmap)
                    {
                        using (Image cutImg = ImageHandleHelper.CutPicByRect(bitmap.GetThumbnailImage(imgWidth, imgHeight, () => false, IntPtr.Zero), x, y, w, h))
                        {

                            using (Image thumbnailImg = cutImg.GetThumbnailImage(150, cutImg.Height * 150 / cutImg.Width, () => false, IntPtr.Zero))
                            {
                                return MyJson(new Result { code = 0, data = new {
                                    thumbnailName = FileHelper.SaveImageBySha1(thumbnailImg, $"{Server.MapPath("~/uploadFiles/")}{pathName}"),
                                    imgName = FileHelper.SaveImageBySha1(cutImg, $"{Server.MapPath("~/uploadFiles/")}{pathName}")
                                } }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    return MyJson(new Result { code = -1, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return MyJson(new Result { code = -1, msg = "图片不存在，切割失败！" }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 显示上传的图片
        /// <param name="pathName">图片路径名称</param>
        /// <param name="imgName">图片名称</param>
        /// </summary>
        [OutputCache(Duration = int.MaxValue)]
        public void ShowImage(string pathName,string imgName)
        {
            Response.ContentType = "image/jpeg";
            string imgPath =$"{Server.MapPath("~/uploadFiles/")}{pathName}{Path.DirectorySeparatorChar}{imgName}";
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
            SystemService systemService = new SystemService();
            return MyJson(new Result { code = 0, data = systemService.LoadAreaTree() }, JsonRequestBehavior.AllowGet);
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

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="Username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="vercode">验证码</param>
        /// <returns>返回登录结果的json</returns>
        [AllowAnonymous]
        public JsonResult Login(string username, string password,string vercode)
        {
            try
            {
                if (!Convert.ToString(Session["vercode"]).Equals(vercode, StringComparison.OrdinalIgnoreCase))
                {
                    return Json(new Result { code = -1, msg = "验证码错误。" }, JsonRequestBehavior.AllowGet);
                }
                Random random = new Random();
                //zhang
                string u="60b69332fb3d57a5c6537a57d45d6e790768b6b6";
                //123
                string p = "40bd001563085fc35165329ea1ff5c5ecbdbbeef";
                if (u == username && p == password)
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                    return MyJson(new Result { code = 0,data=new {leftMenus= _systemService.LoadLeftMenus()}}, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return MyJson(new Result { code = -1, msg = "登录失败，账号或密码错误！" }, JsonRequestBehavior.AllowGet);
                }
            }
            finally
            {
                Session["vercode"] = Guid.NewGuid().ToString();
            }
        }
    }
}