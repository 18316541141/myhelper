using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Entity.Common
{
    /// <summary>
    /// 这是整个网站的后台和前端交互的返回值类
    /// </summary>
    public class Result
    {
        /// <summary>
        /// 返回的消息
        /// </summary>
        public string msg { set; get; }
        /// <summary>
        /// 返回的消息码
        /// 1：实时更新时，如果没有变不需要刷新时返回
        /// 0：成功
        /// -1：常规错误
        /// -8：用户未授权
        /// -9：用户未授权，但有回调
        /// -10：登录超时
        /// -11：同一账户登录两次
        /// </summary>
        public short code { set; get; }
        /// <summary>
        /// 返回的数据
        /// </summary>
        public dynamic data { set; get; }
    }
}