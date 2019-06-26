using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.filter
{
    /// <summary>
    /// 压缩过滤器
    /// </summary>
    public class CompressAttribute: ActionFilterAttribute
    {
        public CompressAttribute()
        {
            IgnoreRequests = new HashSet<string>();
        }

        /// <summary>
        /// 不需要压缩过滤器的请求url
        /// </summary>
        public HashSet<string> IgnoreRequests { set; get; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!IgnoreRequests.Contains(filterContext.HttpContext.Request.Url.AbsolutePath.ToLower()))
            {
                var content = filterContext.Result;
                var response = filterContext.HttpContext.Response;
                var acceptEncoding = filterContext.HttpContext.Request.Headers["Accept-Encoding"].ToUpper();
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