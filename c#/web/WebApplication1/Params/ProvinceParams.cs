using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Params
{
    /// <summary>
    /// 省表查询条件
    /// </summary>
    public class ProvinceParams
    {
        /// <summary>
        /// 地区名称
        /// </summary>
        public string provinceName { set; get; }

        /// <summary>
        /// 地区值
        /// </summary>
        public int provinceID { set; get; }
    }
}