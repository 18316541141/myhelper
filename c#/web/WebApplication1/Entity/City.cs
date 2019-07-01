using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Entity
{
    /// <summary>
    /// 市
    /// </summary>
    public class City
    {
        /// <summary>
        /// 地区名称
        /// </summary>
        public string CityName { set; get; }

        /// <summary>
        /// 地区值
        /// </summary>
        public int CityID { set; get; }

        /// <summary>
        /// 所在省值
        /// </summary>
        public int ProvinceID { set; get; }
    }
}