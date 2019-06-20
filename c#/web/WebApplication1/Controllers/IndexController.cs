
using CommonHelper.Helper;
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
using WebApplication1.Entity;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    public class IndexController : Controller
    {
        SystemService _systemService;

        public IndexController()
        {
            _systemService = new SystemService();
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
            return Json(new Result {code=0,data= _systemService.LoadLeftMenus() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 上传图片通用方法
        /// </summary>
        /// <returns></returns>
        public JsonResult UploadImage()
        {
            return null;
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public JsonResult Logout()
        {
            FormsAuthentication.SignOut();
            return Json(new Result { code = 0}, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="Username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="vercode">验证码</param>
        /// <returns>返回登录结果的json</returns>
        [AllowAnonymous]
        public JsonResult Login(string Username, string password,string vercode)
        {
            try
            {
                if (!Convert.ToString(Session["vercode"]).Equals(vercode, StringComparison.OrdinalIgnoreCase))
                {
                    return Json(new Result { code = -1, msg = "验证码错误。" }, JsonRequestBehavior.AllowGet);
                }
                Random random = new Random();
                if (random.NextDouble() > .5)
                {
                    FormsAuthentication.SetAuthCookie(Username, false);
                    return Json(new Result { code = 0,data=new
                    {
                        leftMenus= _systemService.LoadLeftMenus()
                    } }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new Result { code = -1, msg = "登录失败，账号或密码错误！" }, JsonRequestBehavior.AllowGet);
                }
            }
            finally
            {
                Session["vercode"] = Guid.NewGuid().ToString();
            }
        }
    }
}