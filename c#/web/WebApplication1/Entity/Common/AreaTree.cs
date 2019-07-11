using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Entity.Common
{
    /// <summary>
    /// 地区树
    /// </summary>
    public class AreaTree
    {
        /// <summary>
        /// 地区名称
        /// </summary>
        public string name { set; get; }

        /// <summary>
        /// 地区值
        /// </summary>
        public string value { set; get; }

        /// <summary>
        /// 上级地区值
        /// </summary>
        public string parentValue { set; get; }
    }
}