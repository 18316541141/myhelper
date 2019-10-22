using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.OrderBy;
using WebApplication1.Params;

namespace WebApplication1.Params
{
    /// <summary>
    /// 全局变量表
    /// </summary>
    public sealed partial class GlobalVariableParams
    {


        /// <summary>
        /// 主键id
        /// </summary>
        public long? Id { set; get; }


        /// <summary>
        /// 变量排序号
        /// </summary>
        public int? VarSortIndex { set; get; }


        /// <summary>
        /// 变量排序号
        /// </summary>
        public int? VarSortIndexStart { set; get; }


        /// <summary>
        /// 变量排序号
        /// </summary>
        public int? VarSortIndexEnd { set; get; }


        /// <summary>
        /// 变量排序号
        /// </summary>
        public int? VarSortIndexChange { set; get; }


        /// <summary>
        /// 变量名称
        /// </summary>
        public string VarName { set; get; }


        /// <summary>
        /// 变量名称
        /// </summary>
        public string VarNameLike { set; get; }


        /// <summary>
        /// 变量值
        /// </summary>
        public string VarValue { set; get; }


        /// <summary>
        /// 变量值
        /// </summary>
        public string VarValueLike { set; get; }

        /// <summary>
        /// 升序排序
        /// </summary>
        public GlobalVariableOrderBy orderByAsc { set; get; }

        /// <summary>
        /// 降序排序
        /// </summary>
        public GlobalVariableOrderBy orderByDesc { set; get; }
    }
}