using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace WebApplication1.Entity
{
	/// <summary>
	/// 心跳监测表
	/// </summary>
    public sealed partial class HeartbeatEntitySetNullParams
    {
        public HeartbeatEntitySetNullParams() { }

	        /// <summary>
			/// 主键id
			/// </summary>
	        public long? Id { set; get; }
			/// <summary>
			/// 最近一次的心跳时间
			/// </summary>
			public bool LastHeartbeatTime { set; get; }
			/// <summary>
			/// 机器人id
			/// </summary>
			public bool RobotId { set; get; }
    }
}