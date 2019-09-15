using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.CommonEntity
{
    public sealed partial class HeartbeatEntityOrderBy
    {
        /// <summary>
        /// 
        /// </summary>
        public bool Id { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public bool Username { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public bool LastHeartbeatTime { set; get; }
    }
}
