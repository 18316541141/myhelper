using CommonHelper.Helper.CommonEntity;
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
    /// 报文大小过滤器
    /// </summary>
    public sealed class SizeFilterAttribute: AuthorizeAttribute
    {

        /// <summary>
        /// 附件最大限制，单位：字节
        /// </summary>
        public readonly int Size;

        /// <summary>
        /// 提示信息
        /// </summary>
        public readonly string Msg;

        public SizeFilterAttribute(int size, string msg)
        {
            Size = size;
            Msg = msg;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if(!AuthorizeCore(filterContext.HttpContext))
                filterContext.Result = new EmptyResult();
            base.OnAuthorization(filterContext);
        }


        protected override bool AuthorizeCore(HttpContextBase httpContext)=>
            Size >= httpContext.Request.ContentLength;

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