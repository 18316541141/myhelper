using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.OrderBy
{
    public partial class IRobotErrorMsgOrderBy
    {


			/// <summary>
			/// 错误编号，错误信息的主键
			/// </summary>
			public bool IEErrNo { set; get; }

			/// <summary>
			/// 错误记录创建日期
			/// </summary>
			public bool IECreateDate { set; get; }

			/// <summary>
			/// 发生错误的订单
			/// </summary>
			public bool IEErrOrderNo { set; get; }

			/// <summary>
			/// 发生错误的机器人ID
			/// </summary>
			public bool IEErrRobotId { set; get; }

			/// <summary>
			/// 错误截图路径的json字符串
			/// </summary>
			public bool IEErrPic { set; get; }

			/// <summary>
			/// 错误信息的具体内容
			/// </summary>
			public bool IEErrContext { set; get; }

			/// <summary>
			/// 处理状态：0 未处理、1 已处理
			/// </summary>
			public bool IEHandleStatus { set; get; }
    }
}