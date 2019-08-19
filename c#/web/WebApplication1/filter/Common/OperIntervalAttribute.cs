using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Entity.Common;

namespace WebApplication1.Filter.Common
{
    /// <summary>
    /// 操作间隔拦截器，以ip为标识，对每次操作的间隔时间
    /// 进行限制
    /// </summary>
    public class OperIntervalAttribute: ActionFilterAttribute
    {
        /// <summary>
        /// 间隔的毫秒数
        /// </summary>
        public double IntervalMillisecond { set; get; }

        /// <summary>
        /// 记录近期访客的访问时间
        /// </summary>
        public static Dictionary<string,DateTime> RecordMap { set; get; }

        /// <summary>
        /// 最近一次清理日期
        /// </summary>
        public static DateTime LastClearDate { set; get; }

        static OperIntervalAttribute()
        {
            RecordMap = new Dictionary<string, DateTime>();
            LastClearDate = DateTime.Now;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContextBase httpContextBase = filterContext.HttpContext;
            HttpRequestBase request = httpContextBase.Request;
            string key = request.UserHostAddress + request.Path;
            if (RecordMap.ContainsKey(key))
            {
                bool temp;
                lock (RecordMap)
                {
                    if (RecordMap.ContainsKey(key))
                    {
                        temp = (DateTime.Now - RecordMap[key]).TotalMilliseconds > IntervalMillisecond;
                        RecordMap[key] = DateTime.Now;
                    }
                    else
                    {
                        RecordMap.Add(key, DateTime.Now);
                        temp = false;
                    }
                }
                if(temp)
                {
                    base.OnActionExecuting(filterContext);
                }
                else
                {
                    HttpResponseBase response = httpContextBase.Response;
                    response.ContentEncoding = Encoding.UTF8;
                    response.ContentType = "application/json;charset=UTF-8";
                    response.StatusCode = 200;
                    response.Write(JsonConvert.SerializeObject(new Result
                    {
                        code = -1,
                        msg = "操作过于频繁、请稍后重试！",
                    }));
                    filterContext.Result = new EmptyResult();
                }
            }
            else
            {
                lock (RecordMap)
                {
                    RecordMap.Add(key, DateTime.Now);
                }
                base.OnActionExecuting(filterContext);
            }
            if ((DateTime.Now - LastClearDate).TotalSeconds > 60)
            {
                lock (RecordMap)
                {
                    foreach (string tkey in new HashSet<string>(RecordMap.Keys))
                    {
                        if (tkey.EndsWith(request.Path) && RecordMap.ContainsKey(tkey) && (DateTime.Now - RecordMap[tkey]).TotalMilliseconds > IntervalMillisecond)
                        {
                            RecordMap.Remove(tkey);
                        }
                    }
                }
                LastClearDate = DateTime.Now;
            }
        }
    }
}