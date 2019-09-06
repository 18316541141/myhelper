using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace WebApplication1.Entity
{
	/// <summary>
	/// 
	/// </summary>
    public sealed partial class IRobotUserSetNullParams
    {
        public IRobotUserSetNullParams() { }

	        /// <summary>
			/// 主键id
			/// </summary>
	        public long? IUId { set; get; }
			/// <summary>
			/// 用户名
			/// </summary>
			public bool IUUsername { set; get; }
			/// <summary>
			/// 密码
			/// </summary>
			public bool IUPassword { set; get; }
			/// <summary>
			/// 签名的唯一标识
			/// </summary>
			public bool IUSignKey { set; get; }
			/// <summary>
			/// 签名密钥
			/// </summary>
			public bool IUSignSecret { set; get; }
			/// <summary>
			/// 用户创建日期
			/// </summary>
			public bool IUCreateDate { set; get; }
    }
}