using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Entity
{
    /// <summary>
    /// 镇值
    /// </summary>
    public class Town
    {
        /// <summary>
        /// 地区名称
        /// </summary>
        public string TownName { set; get; }

        /// <summary>
        /// 地区值
        /// </summary>
        public string TownID { set; get; }

        /// <summary>
        /// 所在区值
        /// </summary>
        public string DistrictID { set; get; }
    }
}