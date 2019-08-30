using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.AopInterceptor
{
    /// <summary>
    /// 启用分布式事务的标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class DistributedTransactionScope:Attribute
    {
    }
}