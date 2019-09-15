using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.CommonEntity
{
    public sealed partial class HeartbeatEntityParams
    {
        public HeartbeatEntityParams()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public long? Id { set; get; }


        /// <summary>
        /// 
        /// </summary>
        public long? IdStart { set; get; }


        /// <summary>
        /// 
        /// </summary>
        public long? IdEnd { set; get; }


        /// <summary>
        /// 
        /// </summary>
        public string Username { set; get; }


        /// <summary>
        /// 
        /// </summary>
        public string UsernameLike { set; get; }


        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastHeartbeatTime { set; get; }


        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastHeartbeatTimeStart { set; get; }


        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastHeartbeatTimeEnd { set; get; }

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
