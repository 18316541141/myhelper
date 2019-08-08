using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Service.Common
{
    /// <summary>
    /// 基础业务类
    /// </summary>
    public abstract class BaseService
    {
        /// <summary>
        /// 异常信息
        /// </summary>
        /// <param name="msg"></param>
        protected void Ex(string msg)
        {
            throw new Exception(msg + "(Error:-1)");
        }
    }
}