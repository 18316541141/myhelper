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
        public string menuId { set; get; }

        /// <summary>
        /// 新消息的接受者
        /// </summary>
        public string receive { set; get; }

        /// <summary>
        /// 消息的读取状态：0：未读，1：已读
        /// </summary>
        public int readState { set; get; }
    }
}