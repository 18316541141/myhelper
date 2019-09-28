using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonHelper.OrderBy
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
        /// 机器人的ip地址
        /// </summary>
        public bool RobotMac { set; get; }

        /// <summary>
        /// 机器人备注
        /// </summary>
        public bool Remark { set; get; }

        /// <summary>
        /// 扩展字段
        /// </summary>
        public bool ExtendField { set; get; }

        /// <summary>
        /// 监视服务器的url
        /// </summary>
        public bool MonitorServer { set; get; }
    }
}