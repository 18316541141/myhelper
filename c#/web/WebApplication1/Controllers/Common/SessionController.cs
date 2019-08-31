﻿using CommonHelper.Helper;
using CommonHelper.Helper.CommonEntity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Entity;
using WebApplication1.Entity.Common;
using WebApplication1.Filter.Common;
using WebApplication1.Service;

namespace WebApplication1.Controllers.Common
{
    /// <summary>
    /// 所有操作session的请求都在这里处理
    /// </summary>
    public class SessionController : BaseController
    {
        public SystemService SystemService { set; get; }

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
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public JsonResult Logout()
        {
            FormsAuthentication.SignOut();
            return Json(new Result { code = 0 }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 跨域登录，用于实现单点登录
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Sign(new string[] { "callback", "username", "signKey", "createDate", "r" })]
        public JsonResult JsonpLogin(string callback, string username,string signKey)
        {
            FormsAuthentication.SetAuthCookie(username, false);
            string guid = Guid.NewGuid().ToString();
            Session.Add("loginGuid", guid);
            lock (SingleUserAttribute.UserMap)
            {
                if (SingleUserAttribute.UserMap.ContainsKey(username))
                {
                    SingleUserAttribute.UserMap[username] = guid;
                }
                else
                {
                    SingleUserAttribute.UserMap.Add(username, guid);
                }
            }
            return MyJson(callback, new Result { code = 0,msg="登录成功" });
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="Username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="vercode">验证码</param>
        /// <returns>返回登录结果的json</returns>
        [AllowAnonymous]
        public JsonResult Login(string username, string password, string vercode)
        {
            try
            {
#if debugger
#else
                if (!Convert.ToString(Session["vercode"]).Equals(vercode, StringComparison.OrdinalIgnoreCase))
                {
                    return Json(new Result { code = -1, msg = "验证码错误。" }, JsonRequestBehavior.AllowGet);
                }
#endif
                Random random = new Random();
                //zhang
                string u = "zhang";
                //123
                string p = "40bd001563085fc35165329ea1ff5c5ecbdbbeef";
                if (u == username && p == password)
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                    string guid=Guid.NewGuid().ToString();
                    Session.Add("loginGuid", guid);
                    lock (SingleUserAttribute.UserMap)
                    {
                        if (SingleUserAttribute.UserMap.ContainsKey(username))
                        {
                            SingleUserAttribute.UserMap[username] = guid;
                        }
                        else
                        {
                            SingleUserAttribute.UserMap.Add(username, guid);
                        }
                    }
                    return Json(new Result { code = 0, data = new { leftMenus = SystemService.LoadLeftMenus() } }, JsonRequestBehavior.AllowGet);
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