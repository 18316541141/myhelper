﻿using System;
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
        /// 0：正确无误
        /// -1：无种类错误，只用于区分正确和错误。
        /// -2：用户已退出登录，登录失败！
        /// </summary>
        public short code { set; get; }
        /// <summary>
        /// 返回的数据
        /// </summary>
        public dynamic data { set; get; }
    }
}