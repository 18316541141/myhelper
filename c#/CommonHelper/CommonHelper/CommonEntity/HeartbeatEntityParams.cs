using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonHelper.OrderBy;
using CommonHelper.Params;

namespace CommonHelper.Params
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
        /// 机器人的ip地址
        /// </summary>
        public string RobotIp { set; get; }


        /// <summary>
        /// 机器人的ip地址
        /// </summary>
        public string RobotIpLike { set; get; }


        /// <summary>
        /// 机器人备注
        /// </summary>
        public string Remark { set; get; }


        /// <summary>
        /// 机器人备注
        /// </summary>
        public string RemarkLike { set; get; }


        /// <summary>
        /// 扩展字段
        /// </summary>
        public string ExtendField { set; get; }


        /// <summary>
        /// 扩展字段
        /// </summary>
        public string ExtendFieldLike { set; get; }

        /// <summary>
        /// 监视服务器的url
        /// </summary>
        public string MonitorServer { set; get; }

        /// <summary>
        /// 监视服务器的url
        /// </summary>
        public string MonitorServerLike { set; get; }

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