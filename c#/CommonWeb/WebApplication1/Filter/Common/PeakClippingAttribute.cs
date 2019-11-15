using CommonHelper.Helper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace CommonWeb.Filter.Common
{
    /// <summary>
    /// 削峰过滤器，错峰限行.
    /// </summary>
    public class PeakClippingAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 最大同时处理的线程数
        /// </summary>
        static int MaxThreadCount { set; get; }

        /// <summary>
        /// 当前处理的线程数
        /// </summary>
        static int ExecuteCount;

        /// <summary>
        /// 等待池名称
        /// </summary>
        static string PoolName { set; get; }

        static PeakClippingAttribute()
        {
            MaxThreadCount = 4;
            ExecuteCount = 0;
            PoolName = Guid.NewGuid().ToString();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Interlocked.Increment(ref ExecuteCount);
            while (ExecuteCount > MaxThreadCount)
            {
                ThreadHelper.BatchWait(PoolName,int.MaxValue);
            }
            base.OnActionExecuting(filterContext);
            Interlocked.Decrement(ref ExecuteCount);
            ThreadHelper.BatchSet(PoolName);
        }
    }
}