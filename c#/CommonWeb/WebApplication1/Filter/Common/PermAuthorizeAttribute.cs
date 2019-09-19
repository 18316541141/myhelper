using CommonHelper.Helper.CommonEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace CommonWeb.Filter.Common
{
    /// <summary>
    /// 权限校验模块
    /// </summary>
    public sealed class PermAuthorizeAttribute : AuthorizeAttribute
    {

        /// <summary>
        /// 每次发生请求前先检查action是否允许匿名访问，如果不允许匿名访问，
        /// 并且没有权限时，设置后续处理为EmptyResult
        /// </summary>
        /// <param name="filterContext">过滤器上下文</param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
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
            if (OnlyLogin && !AuthorizeCore(filterContext.HttpContext))
                filterContext.Result = new EmptyResult();
            base.OnAuthorization(filterContext);
        }

        /// <summary>
        /// 返回用户是否有权限，如果有权限返回true，否则返回false
        /// </summary>
        /// <param name="httpContext">过滤器上下文</param>
        /// <returns>如果已登录返回true，否则返回false</returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return true;
        }

        /// <summary>
        /// 如果用户无权限，则会调用这个方法，告诉用户没有权限。
        /// </summary>
        /// <param name="filterContext">过滤器上下文</param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            HttpResponseBase Response = filterContext.HttpContext.Response;
            Response.ContentType = "application/json;charset=UTF-8";
            Response.StatusCode = 200;
            string jsonStr = JsonConvert.SerializeObject(new Result
            {
                code = -9
            });
            Response.Write(jsonStr);
        }
    }
}