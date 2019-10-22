using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace WebApplication1.Entity
{
	/// <summary>
	/// 全局变量表
	/// </summary>
    public sealed partial class GlobalVariableSetNullParams
    {
        public GlobalVariableSetNullParams() { }

	        /// <summary>
			/// 主键id
			/// </summary>
	        public long? Id { set; get; }
			/// <summary>
			/// 变量排序号
			/// </summary>
			public bool VarSortIndex { set; get; }
			/// <summary>
			/// 变量名称
			/// </summary>
			public bool VarName { set; get; }
			/// <summary>
			/// 变量值
			/// </summary>
			public bool VarValue { set; get; }
    }
}