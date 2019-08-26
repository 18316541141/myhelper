using CommonHelper.Helper;
using CommonHelper.Helper.CommonEntity;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Entity.Common;
namespace WebApplication1.Filter.Common
{
    /// <summary>
    /// 统一的异常处理器，对于任何未处理，或意料之外的异常都会
    /// 由该处理器处理，并统一对用户声称是系统繁忙。
    /// </summary>
    public class MyErrorAttribute: HandleErrorAttribute
    {
        ILog log;

        /// <summary>
        /// 错误前缀
        /// </summary>
        public static string ErrorPrefix { set; get; }

        static MyErrorAttribute()
        {
            ErrorPrefix = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 默认的json字符串
        /// </summary>
        public string DefaultExJsonStr { set; get; }

        public MyErrorAttribute()
        {
            DefaultExJsonStr = JsonConvert.SerializeObject(new Result
            {
                code = -1,
                msg = "系统繁忙，请稍后重试..."
            });
            log = LogManager.GetLogger("Log4net.config");
        }

        public override void OnException(ExceptionContext filterContext)
        {
            //捕获到系统内部往外抛出的异常
            Exception exception = filterContext.Exception;
            /**
             * 检查异常信息是否带有ErrorPrefix前缀，
             * 如果有，则表示是开发人员自发的捕获出来，并写上具体错误原因的异常消息。
             * 如果没有，则表示这是意料之外的异常，这时统一口径就是："系统繁忙，请稍后重试..."
             */
            string resultJson = exception.Message.StartsWith(ErrorPrefix) ? FindDataHelper.FindDataByPrefix(exception.Message, ErrorPrefix): DefaultExJsonStr;
            HttpContextBase httpContextBase = filterContext.HttpContext;
            HttpResponseBase response = httpContextBase.Response;
            response.ContentEncoding = Encoding.UTF8;
            response.ContentType = "application/json;charset=UTF-8";
            response.StatusCode = 200;
            response.Write(resultJson);
            log.Error(exception.Message, exception);
            filterContext.Result = new EmptyResult();
            filterContext.ExceptionHandled = true;
        }
    }
}