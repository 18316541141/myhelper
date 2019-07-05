using CommonHelper.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Entity;

namespace WebApplication1.filter
{
    /// <summary>
    /// 实时加载拦截器
    /// </summary>
    public class RealTimeAttribute : ActionFilterAttribute
    {

        /// <summary>
        /// 检查客户端的版本号，如果和服务端的版本号一致，则表示客户端显示的内容已经是最新的，
        /// 把线程挂在等待池，30秒后返回。
        /// 如果不是最新的，则进入Action获取最新数据
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            HttpRequestBase request = filterContext.HttpContext.Request;
            HttpResponseBase response = filterContext.HttpContext.Response;
            string realTimeVersion = Convert.ToString(request.Headers["Real-Time-Version"]);
            string realTimePool = Convert.ToString(request.Headers["Real-Time-Pool"]);
            response.AddHeader("Real-Time-Pool", realTimePool);
            string newestVersion;
            if (ThreadHelper.CompareControllerVersion(realTimePool, realTimeVersion, out newestVersion))
            {
                ThreadHelper.BatchWait(realTimePool, 30000);
                response.AddHeader("Real-Time-Version", newestVersion);
                response.ContentType = "application/json";
                response.Write(JsonConvert.SerializeObject(new Result { code = 1}));
            }
            else
            {
                response.AddHeader("Real-Time-Version", newestVersion);
                base.OnActionExecuted(filterContext);
            }
        }
    }
}