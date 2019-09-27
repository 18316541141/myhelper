using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplication1.Entity
{
	/// <summary>
	/// 心跳监测表
	/// </summary>
    public partial class HeartbeatEntity //: IEntity
    {
        public HeartbeatEntity() { }

			/// <summary>
			/// 主键id
			/// </summary>
			public virtual long? Id { set; get; }
			/// <summary>
			/// 最近一次的心跳时间
			/// </summary>
			public virtual DateTime? LastHeartbeatTime { set; get; }
			/// <summary>
			/// 机器人的ip地址
			/// </summary>
			public virtual string RobotIp { set; get; }
			/// <summary>
			/// 机器人备注
			/// </summary>
			public virtual string Remark { set; get; }
			/// <summary>
			/// 扩展字段
			/// </summary>
			public virtual string ExtendField { set; get; }
	/*
		[NotMapped]
		public long? Key
		{
			set { Id = value; }
			get { return Id; }
		}

		public string TableName()
		{
			return "Heartbeat_Entity";
		}
	*/
    }
}