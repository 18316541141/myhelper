using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Entity.Common
{
    /// <summary>
    /// 自定义的分页列表
    /// </summary>
    public class MyPagedList<T>
    {
        /// <summary>
        /// 分页查询的数据
        /// </summary>
        public List<T> pageDataList { set; get; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int currentPageIndex { get; set; }

        /// <summary>
        /// 当前分页数据中，最后一条数据在所有数据中的索引
        /// </summary>
        public int endItemIndex { get; set; }

        /// <summary>
        /// 每页显示的数据量
        /// </summary>
        public int pageSize { get; set; }

        /// <summary>
        /// 当前分页数据中，第一条数据在所有数据中的索引
        /// </summary>
        public int startItemIndex { get; set; }
        
        /// <summary>
        /// 数据总量
        /// </summary>
        public int totalItemCount { get; set; }
        
        /// <summary>
        /// 总页数
        /// </summary>
        public int totalPageCount { get; set; }

        /// <summary>
        /// 扩展数据，假如使用分页返回值时需要顺带其他附加数据，则
        /// 该成员用于保存其他附加数据
        /// </summary>
        public Dictionary<string,object> ExtendData { get; set; }
    }
}