using CommonHelper.Helper.CommonEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Entity;
using WebApplication1.Entity.Common;

namespace CommonWeb.Filter.Common
{
    /// <summary>
    /// 登录校验模块
    /// </summary>
    public class LoginAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 每次发生请求前先检查action是否允许匿名访问，如果不允许匿名访问，
        /// 并且当前请求并没有登录时，设置后续处理为EmptyResult
        /// </summary>
        /// <param name="filterContext">过滤器上下文</param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var s = filterContext.HttpContext.Request.Url;
            object[] objs = filterContext.ActionDescriptor.GetCustomAttributes(false);
            bool OnlyLogin = true;
            foreach (object obj in objs)
            {
                if (obj.GetType() == typeof(AllowAnonymousAttribute))
                {
                    OnlyLogin = false;
                    break;
                }
            }
            if (OnlyLogin && !filterContext.HttpContext.User.Identity.IsAuthenticated)
                filterContext.Result = new EmptyResult();
            base.OnAuthorization(filterContext);
        }

        /// <summary>
        /// 返回用户是否已登录，如果已登录返回true，否则返回false
        /// </summary>
        /// <param name="httpContext">过滤器上下文</param>
        /// <returns>如果已登录返回true，否则返回false</returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
#if debugger
            string username = WebConfigurationManager.AppSettings["testUser"];
            HttpSessionStateBase Session = httpContext.Session;
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
            return true;
#else
            return httpContext.User.Identity.IsAuthenticated;
#endif
        }

        /// <summary>
        /// 如果用户未登录，则会调用这个方法，告诉用户没有登录。
        /// </summary>
        /// <param name="filterContext">过滤器上下文</param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            HttpResponseBase Response = filterContext.HttpContext.Response;
            Response.ContentType = "application/json;charset=UTF-8";
            Response.StatusCode = 200;
            string jsonStr = JsonConvert.SerializeObject(new Result
            {
                code = -10
            });
            Response.Write(jsonStr);
        }
    }
}