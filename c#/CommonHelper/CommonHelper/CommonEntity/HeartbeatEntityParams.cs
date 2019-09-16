using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.OrderBy;

namespace CommonHelper.CommonEntity
{
    /// <summary>
    /// 心跳监测表
    /// </summary>
    public sealed partial class HeartbeatEntityParams
    {


        /// <summary>
        /// 主键id
        /// </summary>
        public long? Id { set; get; }


        /// <summary>
        /// 最近一次的心跳时间
        /// </summary>
        public DateTime? LastHeartbeatTime { set; get; }


        /// <summary>
        /// 最近一次的心跳时间
        /// </summary>
        public DateTime? LastHeartbeatTimeStart { set; get; }


        /// <summary>
        /// 最近一次的心跳时间
        /// </summary>
        public DateTime? LastHeartbeatTimeEnd { set; get; }


        /// <summary>
        /// 机器人id
        /// </summary>
        public string RobotId { set; get; }


        /// <summary>
        /// 机器人id
        /// </summary>
        public string RobotIdLike { set; get; }

        /// <summary>
        /// 升序排序
        /// </summary>
        public HeartbeatEntityOrderBy orderByAsc { set; get; }

        /// <summary>
        /// 降序排序
        /// </summary>
        public HeartbeatEntityOrderBy orderByDesc { set; get; }
    }
}