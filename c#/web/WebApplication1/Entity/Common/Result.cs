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
        /// 
        /// </summary>
        public short code { set; get; }
        /// <summary>
        /// 返回的数据
        /// </summary>
        public dynamic data { set; get; }
    }
}