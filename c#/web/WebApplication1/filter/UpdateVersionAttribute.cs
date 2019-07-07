using CommonHelper.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.filter
{
    /// <summary>
    /// 版本更新拦截器，会使得实时加载的版本号更新，并唤醒等待池。
    /// </summary>
    public class UpdateVersionAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 当对某一实时刷新的数据进行操作后，使用该拦截器唤醒等待池，
        /// 事项实时刷新数据的功能
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string realTimePools = Convert.ToString(filterContext.HttpContext.Request.Headers["Real-Time-Pool"]);
            base.OnActionExecuted(filterContext);
            foreach(string realTimePool in realTimePools.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                ThreadHelper.EditControllerVersion(realTimePool);
                ThreadHelper.BatchSet(realTimePool);
            }
        }
    }
}