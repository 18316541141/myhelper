using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CommonHelper.EFMap
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
        /// 机器人id
        /// </summary>
        [JsonProperty("robotId")]
        public virtual string RobotId { set; get; }

        /// <summary>
        /// 状态：0：已停止、1：运行中
        /// </summary>
        [NotMapped]
        [JsonProperty("status")]
        public sbyte Status
        {
            get
            {
                return (sbyte)((DateTime.Now - LastHeartbeatTime).Value.TotalMinutes > 10?0:1);
            }
        }

        /// <summary>
        /// 状态的中文描述
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
                return null;
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