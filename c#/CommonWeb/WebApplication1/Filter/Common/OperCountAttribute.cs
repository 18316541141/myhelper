using CommonHelper.Helper.CommonEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
namespace CommonWeb.Filter.Common
{
    /// <summary>
    /// 操作记录
    /// </summary>
    public sealed class OperRecord
    {
        /// <summary>
        /// 操作标识，用于区分操作者和操作目标
        /// </summary>
        public string OperKey { set; get; }

        /// <summary>
        /// 操作次数
        /// </summary>
        public int Count { set; get; }

        /// <summary>
        /// 最近一次的操作日期
        /// </summary>
        public DateTime LastOperDate { set; get; }
    }

    /// <summary>
    /// 操作次数限制的拦截器，超过一定的次数后，需要隔一段时间才能
    /// 再次操作。
    /// </summary>
    public sealed class OperCountAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 清除的毫秒数，每隔ClearMillisecond毫秒清理一次。
        /// </summary>
        public long ClearMillisecond { set; get; }

        /// <summary>
        /// 次数限制，当次数达到CountLimit次时，无法访问。
        /// </summary>
        public int CountLimit { set; get; }

        /// <summary>
        /// 记录近期访客的操作记录
        /// </summary>
        static Dictionary<string, OperRecord> RecordMap { set; get; }

        /// <summary>
        /// 最近一次清理日期
        /// </summary>
        static DateTime LastClearDate { set; get; }

        static OperCountAttribute()
        {
            RecordMap = new Dictionary<string, OperRecord>();
            LastClearDate = DateTime.Now;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContextBase httpContextBase = filterContext.HttpContext;
            HttpRequestBase request = httpContextBase.Request;
            HttpResponseBase response = httpContextBase.Response;
            string operKey = request.UserHostAddress + request.Path.ToLower();
            OperRecord operRecord=null;
            lock (RecordMap)
            {
                if (RecordMap.ContainsKey(operKey))
                {
                    operRecord = RecordMap[operKey];
                    long totalMilliseconds = Convert.ToInt64((DateTime.Now - operRecord.LastOperDate).TotalMilliseconds);
                    int count = Convert.ToInt32((totalMilliseconds - totalMilliseconds % ClearMillisecond) / ClearMillisecond);
                    operRecord.Count = operRecord.Count > count ? operRecord.Count - count : 0;
                }
                else
                {
                    RecordMap.Add(operKey, operRecord = new OperRecord
                    {
                        Count = 1,
                        OperKey = operKey,
                        LastOperDate = DateTime.Now
                    });
                }
            }
            if (operRecord.Count >= CountLimit)
            {
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
            else
            {
                lock (RecordMap)
                {
                    operRecord.Count++;
                    operRecord.LastOperDate = DateTime.Now;
                }
                base.OnActionExecuting(filterContext);
            }
            if ((DateTime.Now - LastClearDate).TotalSeconds > 60)
            {
                lock (RecordMap)
                {
                    long sumMillisecond = ClearMillisecond * CountLimit;
                    foreach (string tkey in new HashSet<string>(RecordMap.Keys))
                    {
                        if (tkey.EndsWith(request.Path))
                        {
                            operRecord = RecordMap[tkey];
                            long totalMilliseconds = Convert.ToInt64((DateTime.Now - operRecord.LastOperDate).TotalMilliseconds);
                            if(totalMilliseconds >= sumMillisecond){
                                RecordMap.Remove(tkey);
                            }
                        }
                    }
                }
                LastClearDate = DateTime.Now;
            }
        }
    }
}