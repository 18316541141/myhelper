using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonHelper.CommonEntity
{
    public sealed partial class HeartbeatEntityOrderBy
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public bool Id { set; get; }

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