﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Entity;

namespace WebApplication1.filter
{
    /// <summary>
    /// 单用户拦截器，检测多个用户名相同的用户登录时产生的guid是不一样的，
    /// 利用这一点使得用户只能在同一地区登陆。
    /// </summary>
    public class SingleUserAttribute: ActionFilterAttribute
    {
        public static Dictionary<string, string> UserMap;

        public SingleUserAttribute()
        {
            IgnoreRequests = new HashSet<string>();
        }

        /// <summary>
        /// 不需要压缩过滤器的请求url
        /// </summary>
        public HashSet<string> IgnoreRequests { set; get; }
        static SingleUserAttribute()
        {
            UserMap = new Dictionary<string, string>();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            HttpContextBase httpContextBase = filterContext.HttpContext;
            if (!IgnoreRequests.Contains(httpContextBase.Request.Url.AbsolutePath.ToLower()))
            {
                string username=httpContextBase.User.Identity.Name;
                if (UserMap.ContainsKey(username) && UserMap[username] == (string)httpContextBase.Session["loginGuid"]){
                    base.OnActionExecuted(filterContext);
                }
                else
                {
                    FormsAuthentication.SignOut();
                    HttpResponseBase response = httpContextBase.Response;
                    response.ContentType = "application/json;charset=UTF-8";
                    response.StatusCode = 200;
                    response.Write(JsonConvert.SerializeObject(new Result {
                        code = -10
                    }));
                }
            }
            else
            {
                base.OnActionExecuted(filterContext);
            }
        }
    }
}