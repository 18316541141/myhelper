﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.CommonEntity
{
    public class HeartbeatEntitySetNullParams
    {
        public HeartbeatEntitySetNullParams() { }

        /// <summary>
        /// 
        /// </summary>
        public long? Id { set; get; }
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
