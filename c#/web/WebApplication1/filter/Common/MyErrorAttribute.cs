using log4net;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Entity.Common;
namespace WebApplication1.Filter.Common
{
    /// <summary>
    /// 统一的异常处理器，对于任何未处理，或意料之外的异常都会
    /// 由该处理器处理，并统一对用户声称是网络异常。
    /// </summary>
    public class MyErrorAttribute: HandleErrorAttribute
    {
        ILog log;
        public MyErrorAttribute()
        {
            log = LogManager.GetLogger("Log4net.config");
        }

        public override void OnException(ExceptionContext filterContext)
        {
            Exception exception = filterContext.Exception;
            HttpContextBase httpContextBase = filterContext.HttpContext;
            HttpResponseBase response = httpContextBase.Response;
            response.ContentEncoding = Encoding.UTF8;
            response.ContentType = "application/json;charset=UTF-8";
            response.StatusCode = 200;
            response.Write(JsonConvert.SerializeObject(new Result
            {
                code = -1,
                msg = exception.Message.Contains("(Error:-1)") ? exception.Message.Replace("(Error:-1)","") : "系统繁忙，请稍后重试..."
            }));
            log.Error(exception.Message, exception);
            filterContext.Result = new EmptyResult();
            filterContext.ExceptionHandled = true;
        }
    }
}