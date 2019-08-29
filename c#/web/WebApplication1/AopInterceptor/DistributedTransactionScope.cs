using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.AopInterceptor
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class DistributedTransactionScope:Attribute
    {
    }
}