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
    public class HeartbeatEntity
    {
        /// <summary>
        /// 心跳用户名
        /// </summary>
        public string Username { set; get; }

        /// <summary>
        /// 最近一次心跳时间
        /// </summary>
        public DateTime LastHeartbeatTime { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string OpenId { set; get; }

        /// <summary>
        /// 状态：0（死亡）、1（存活）
        /// </summary>
        public sbyte Status
        {
            get
            {
                return (sbyte)((DateTime.Now - LastHeartbeatTime).TotalMinutes < 10?1:0);
            }
        }
    }
}
