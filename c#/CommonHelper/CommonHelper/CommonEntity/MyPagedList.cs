using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Entity.Common
{
    /// <summary>
    /// 自定义的分页列表
    /// </summary>
    public sealed class MyPagedList<T>
    {
        /// <summary>
        /// 分页查询的数据
        /// </summary>
        [JsonProperty("pageDataList")]
        public List<T> PageDataList { set; get; }

        /// <summary>
        /// 当前页码
        /// </summary>
        [JsonProperty("currentPageIndex")]
        public int CurrentPageIndex { get; set; }

        /// <summary>
        /// 当前分页数据中，最后一条数据在所有数据中的索引
        /// </summary>
        [JsonProperty("endItemIndex")]
        public int EndItemIndex { get; set; }

        /// <summary>
        /// 每页显示的数据量
        /// </summary>
        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        /// <summary>
        /// 当前分页数据中，第一条数据在所有数据中的索引
        /// </summary>
        [JsonProperty("startItemIndex")]
        public int StartItemIndex { get; set; }

        /// <summary>
        /// 数据总量
        /// </summary>
        [JsonProperty("totalItemCount")]
        public int TotalItemCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        [JsonProperty("totalPageCount")]
        public int TotalPageCount { get; set; }

        /// <summary>
        /// 扩展数据，假如使用分页返回值时需要顺带其他附加数据，则
        /// 该成员用于保存其他附加数据
        /// </summary>
        [JsonProperty("extendData")]
        public Dictionary<string,object> ExtendData { get; set; }
    }
}