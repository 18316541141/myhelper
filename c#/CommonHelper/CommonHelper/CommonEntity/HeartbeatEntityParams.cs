using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public long? id { set; get; }


        /// <summary>
        /// 最近一次的心跳时间
        /// </summary>
        public DateTime? lastHeartbeatTime { set; get; }


        /// <summary>
        /// 最近一次的心跳时间
        /// </summary>
        public DateTime? lastHeartbeatTimeStart { set; get; }


        /// <summary>
        /// 最近一次的心跳时间
        /// </summary>
        public DateTime? lastHeartbeatTimeEnd { set; get; }


        /// <summary>
        /// 机器人id
        /// </summary>
        public string robotId { set; get; }


        /// <summary>
        /// 机器人id
        /// </summary>
        public string robotIdLike { set; get; }

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