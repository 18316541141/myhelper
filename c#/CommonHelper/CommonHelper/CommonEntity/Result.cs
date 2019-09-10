using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace CommonHelper.Helper.CommonEntity
{
    /// <summary>
    /// 这是整个网站的后台和前端交互的返回值类
    /// </summary>
    public sealed class Result
    {
        /// <summary>
        /// 返回的消息
        /// </summary>
        public string msg { set; get; }
        /// <summary>
        /// 返回的消息码
        /// 1：成功，并表示版本号没有变化
        /// 0：成功，并表示版本号变化
        /// -1：常规错误
        /// -8：用户未授权
        /// -9：用户未授权，但有回调
        /// -10：登录超时
        /// -11：同一账户登录两次
        /// -12：接口签名认证错误
        /// </summary>
        public short code { set; get; }
        /// <summary>
        /// 返回的数据
        /// </summary>
        public dynamic data { set; get; }
    }
}