using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Entity
{
    /// <summary>
    /// 新消息提醒实体类
    /// </summary>
    public class NewsAlarm
    {
        /// <summary>
        /// 消息创建时间
        /// </summary>
        public DateTime createDate { set; get; }

        /// <summary>
        /// 消息摘要
        /// </summary>
        public string title { set; get; }

        /// <summary>
        /// 查看新消息的菜单页。
        /// </summary>
        public string menuUrl { set; get; }
    }
}