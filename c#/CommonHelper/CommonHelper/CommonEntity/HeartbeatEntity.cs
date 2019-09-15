using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.CommonEntity
{
    /// <summary>
    /// 心跳实体类
    /// </summary>
    public class HeartbeatEntity:IEquatable<HeartbeatEntity>
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public long? Id { set; get; }

        /// <summary>
        /// 心跳用户名
        /// </summary>
        public string Username { set; get; }

        /// <summary>
        /// 最近一次心跳时间
        /// </summary>
        public DateTime LastHeartbeatTime { set; get; }

        /// <summary>
        /// 状态：0（死亡）、1（存活）
        /// </summary>
        public sbyte? Status
        {
            get
            {
                return (sbyte?)((DateTime.Now - LastHeartbeatTime).TotalMinutes < 10?1:0);
            }
        }

        /// <summary>
        /// 等值依据
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(HeartbeatEntity other)
        {
            return other.Id == Id;
        }
    }
}
