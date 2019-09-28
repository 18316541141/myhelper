using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CommonHelper.Entity
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
        [JsonProperty("id")]
        public virtual long? Id { set; get; }

        /// <summary>
        /// 最近一次的心跳时间
        /// </summary>
        [JsonProperty("lastHeartbeatTime")]
        public virtual DateTime? LastHeartbeatTime { set; get; }

        /// <summary>
        /// 机器人的ip地址
        /// </summary>
        [JsonProperty("robotMac")]
        public virtual string RobotMac { set; get; }

        /// <summary>
        /// 机器人备注
        /// </summary>
        [JsonProperty("remark")]
        public virtual string Remark { set; get; }

        /// <summary>
        /// 扩展字段
        /// </summary>
        [JsonProperty("extendField")]
        public virtual string ExtendField { set; get; }

        /// <summary>
        /// 监视服务器的url
        /// </summary>
        [JsonProperty("monitorServer")]
        public virtual string MonitorServer { set; get; }

        /// <summary>
        /// 机器人状态
        /// </summary>
        [NotMapped]
        [JsonProperty("status")]
        public sbyte? Status
        {
            get
            {
                if (LastHeartbeatTime == null)
                {
                    return null;
                }
                else
                {
                    return (sbyte)((DateTime.Now - LastHeartbeatTime).Value.TotalMinutes > 10 ? 0 : 1);
                }
            }
        }

        /// <summary>
        /// 状态中文描述
        /// </summary>
        [NotMapped]
        [JsonProperty("statusDesc")]
        public string StatusDesc
        {
            get
            {
                if (Status == 0)
                {
                    return "已停止";
                }
                else if (Status == 1)
                {
                    return "运行中";
                }
                return "";
            }
        }

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