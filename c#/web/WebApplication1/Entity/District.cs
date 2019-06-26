using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Entity
{
    /// <summary>
    /// 区表
    /// </summary>
    public class District
    {
        /// <summary>
        /// 地区名称
        /// </summary>
        public string DistrictName { set; get; }

        /// <summary>
        /// 地区值
        /// </summary>
        public string DistrictID { set; get; }

        /// <summary>
        /// 所在市值
        /// </summary>
        public string CityID { set; get; }
    }
}