﻿using System;
using System.Collections.Generic;
using CommonWeb.Intf;

namespace WebApplication1.Service
{
    /// <summary>
    /// 实时刷新的首次调用
    /// </summary>
    public class RealTimeInitService : IRealTimeInitService
    {
        HashSet<string> WaitPoolSet;

        public RealTimeInitService()
        {
            WaitPoolSet = new HashSet<string>();
        }

        public HashSet<string> GetWaitPoolSet()
        {
            return WaitPoolSet;
        }

        public bool Init(string realTimePool, string username)
        {
            return false;
        }
    }
}