using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.OrderBy
{
    public sealed partial class GlobalVariableOrderBy
    {


			/// <summary>
			/// 主键id
			/// </summary>
			public bool Id { set; get; }

			/// <summary>
			/// 变量排序号
			/// </summary>
			public bool VarSortIndex { set; get; }

			/// <summary>
			/// 变量备注
			/// </summary>
			public bool VarRemark { set; get; }

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