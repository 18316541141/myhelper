using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Entity;
using WebApplication1.Entity.Common;

namespace WebApplication1.Filter.Common
{
    /// <summary>
    /// 附件大小过滤器
    /// </summary>
    public class SizeFilterAttribute: AuthorizeAttribute
    {

        /// <summary>
        /// 附件最大限制，单位：字节
        /// </summary>
        public int Size { set; get; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Msg { set; get; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if(!AuthorizeCore(filterContext.HttpContext))
                filterContext.Result = new EmptyResult();
            base.OnAuthorization(filterContext);
        }


        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.Request.ContentLength <= Size;
        }

        /// <summary>
        /// 超出大小时返回
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            HttpResponseBase Response = filterContext.HttpContext.Response;
            Response.ContentEncoding = Encoding.UTF8;
            Response.ContentType = "application/json;charset=UTF-8";
            Response.StatusCode = 200;
            string jsonStr = JsonConvert.SerializeObject(new Result
            {
                code = -1,
                msg = Msg
            });
            Response.Write(jsonStr);
        }
    }
}