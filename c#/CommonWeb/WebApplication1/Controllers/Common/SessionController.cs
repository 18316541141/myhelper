using CommonHelper.Helper;
using CommonHelper.Helper.CommonEntity;
using CommonWeb.Controllers.Common;
using CommonWeb.Filter.Common;
using CommonWeb.Intf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebApplication1.Controllers.Common
{
    /// <summary>
    /// 所有操作session的请求都在这里处理
    /// </summary>
    public sealed partial class SessionController : BaseController
    {
        /// <summary>
        /// 用户专用的接口类，提供用户操作的业务逻辑
        /// </summary>
        public IUserService UserService { set; get; }

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
            return MyJson(new Result { code = 0 });
        }

        /// <summary>
        /// 跨域登录，用于实现单点登录
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Sign(new string[] { "callback", "signKey", "createDate", "r" })]
        public JsonResult JsonpLogin(string callback,string signKey)
        {
            string username = UserService.LoadUsernameBySignKey(signKey);
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
#if DEBUG
#else
                if (!Convert.ToString(Session["vercode"]).Equals(vercode, StringComparison.OrdinalIgnoreCase))
                {
                    return MyJson(new Result { code = -1, msg = "验证码错误。" });
                }
#endif
                if (UserService.CheckLogin(username, password))
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
                    return MyJson(new Result { code = 0, data = new { leftMenus = UserService.LoadLeftMenus(User.Identity.Name) } });
                }
                else
                {
                    return MyJson(new Result { code = -1, msg = "登录失败，账号或密码错误！" });
                }
            }
            finally
            {
                Session["vercode"] = Guid.NewGuid().ToString();
            }
        }
    }
}