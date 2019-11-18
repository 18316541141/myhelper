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
        static SameTimeOperLock _SameTimeOperLock { set; get; }

        static PeakClippingAttribute()
        {

            _SameTimeOperLock=new SameTimeOperLock(4);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _SameTimeOperLock.LockOnMaxThread();
            base.OnActionExecuting(filterContext);
            _SameTimeOperLock.UnLock();
        }
    }
}