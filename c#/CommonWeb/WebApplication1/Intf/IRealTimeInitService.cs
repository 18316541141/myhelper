using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonWeb.Intf
{
    /// <summary>
    /// 实时刷新的首次刷新业务处理
    /// </summary>
    public interface IRealTimeInitService
    {
        /// <summary>
        /// 首次刷新时调用该方法
        /// </summary>
        /// <param name="realTimePool">实时等待池名称</param>
        /// <param name="username">用户名</param>
        /// <returns>返回true，需进入等待池。返回false则马上刷新</returns>
        bool Init(string realTimePool, string username);
    }
}