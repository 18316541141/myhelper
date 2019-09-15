using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.CommonEntity
{
    public partial class HeartbeatEntity : IEquatable<HeartbeatEntity>
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public virtual long? Id { set; get; }

        /// <summary>
        /// 用户名
        /// </summary>
        public virtual string Username { set; get; }

        /// <summary>
        /// 最近一次心跳时间
        /// </summary>
        public virtual DateTime? LastHeartbeatTime { set; get; }

        public bool Equals(HeartbeatEntity other)
        {
            return other!=null && Id == other.Id;
        }
    }
}
