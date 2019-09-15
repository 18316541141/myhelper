using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.entity;

namespace CommonHelper.CommonEntity
{
    /// <summary>
    /// 心跳实体类
    /// </summary>
    public partial class HeartbeatEntity
    {
        /// <summary>
        /// 主键id
        /// </summary>
        [KeyAttr]
        public long? Id { set; get; }

        /// <summary>
        /// 心跳用户名
        /// </summary>
        public string Username { set; get; }

        /// <summary>
        /// 最近一次心跳时间
        /// </summary>
        public DateTime LastHeartbeatTime { set; get; }
    }
}
