using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Intf
{
    /// <summary>
    /// 实时刷新的首次刷新业务处理
    /// </summary>
    public interface IRealTimeInitService
    {
        /// <summary>
        /// 首次刷新时调用该方法
        /// </summary>
        /// <param name="realTimePool"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        bool Init(string realTimePool, string username);
    }
}