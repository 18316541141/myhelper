using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Filter.Common
{
    /// <summary>
    /// 压缩过滤器
    /// </summary>
    public sealed class CompressAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var content = filterContext.Result;
            var response = filterContext.HttpContext.Response;
            var acceptEncoding = filterContext.HttpContext.Request.Headers["Accept-Encoding"]?.ToUpper();
            if (acceptEncoding != null)
            {
                if (acceptEncoding.Contains("GZIP"))
                {
                    response.AppendHeader("Content-Encoding", "gzip");
                    response.Filter= new GZipStream(response.Filter, CompressionMode.Compress);
                }
                else if (acceptEncoding.Contains("DEFLATE"))
                {
                    response.AppendHeader("Content-Encoding", "deflate");
                    response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
                }
            }
            base.OnActionExecuted(filterContext);
        }
    }
}