using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplication1.Entity
{
	/// <summary>
	/// 
	/// </summary>
    public partial class IRobotUser //: IEntity
    {
        public IRobotUser() { }

			/// <summary>
			/// 主键id
			/// </summary>
			public virtual long? IUId { set; get; }
			/// <summary>
			/// 用户名
			/// </summary>
			public virtual string IUUsername { set; get; }
			/// <summary>
			/// 密码
			/// </summary>
			public virtual string IUPassword { set; get; }
			/// <summary>
			/// 签名的唯一标识
			/// </summary>
			public virtual string IUSignKey { set; get; }
			/// <summary>
			/// 签名密钥
			/// </summary>
			public virtual string IUSignSecret { set; get; }
			/// <summary>
			/// 用户创建日期
			/// </summary>
			public virtual DateTime? IUCreateDate { set; get; }
	/*
		[NotMapped]
		public long? Key
		{
			set { IUId = value; }
			get { return IUId; }
		}

		public string TableName()
		{
			return "IRobot_User";
		}
	*/
    }
}