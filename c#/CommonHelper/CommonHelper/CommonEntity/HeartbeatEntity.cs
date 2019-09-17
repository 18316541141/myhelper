using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
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
        public virtual long? Id { set; get; }

        /// <summary>
        /// 最近一次的心跳时间
        /// </summary>
        public virtual DateTime? LastHeartbeatTime { set; get; }

        /// <summary>
        /// 机器人id
        /// </summary>
        public virtual string RobotId { set; get; }

        /// <summary>
        /// 状态：0：已停止、1：运行中
        /// </summary>
        [NotMapped]
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