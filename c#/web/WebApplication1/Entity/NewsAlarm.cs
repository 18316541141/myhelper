using Newtonsoft.Json;
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
        [JsonProperty("createDate")]
        public DateTime CreateDate { set; get; }

        /// <summary>
        /// 消息摘要
        /// </summary>
        [JsonProperty("title")]
        public string Title { set; get; }

        /// <summary>
        /// 查看新消息的菜单页。
        /// </summary>
        [JsonProperty("menuId")]
        public string MenuId { set; get; }

        /// <summary>
        /// 新消息的接受者
        /// </summary>
        [JsonProperty("receive")]
        public string Receive { set; get; }

        /// <summary>
        /// 消息的读取状态：0：未读，1：已读
        /// </summary>
        [JsonProperty("readState")]
        public int ReadState { set; get; }
    }
}