using CommonHelper.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Controllers.Common;

namespace WebApplication1.Filter.Common
{
    /// <summary>
    /// 版本更新拦截器，会使得实时加载的版本号更新，并唤醒等待池。
    /// </summary>
    public sealed class UpdateVersionAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 当对某一实时刷新的数据进行操作后，使用该拦截器唤醒等待池，
        /// 事项实时刷新数据的功能
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            HttpContextBase httpContext = filterContext.HttpContext;
            string realTimePools = httpContext.Request.QueryString["realTimePools"];
            base.OnActionExecuted(filterContext);
            foreach(string realTimePool in realTimePools.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                ThreadHelper.EditControllerVersion(realTimePool);
                string usernameAndPoolKey = httpContext.User.Identity.Name + realTimePool;
                if (IndexController.UsernameAndPoolSet.Contains(usernameAndPoolKey))
                {
                    lock (IndexController.UsernameAndPoolSet)
                    {
                        IndexController.UsernameAndPoolSet.Remove(usernameAndPoolKey);
                    }
                }
                ThreadHelper.BatchSet(realTimePool);
            }
        }
    }
}