﻿using CommonHelper.EFRepository;
using Newtonsoft.Json.Converters;
using Snowflake.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.staticVar
{
    /// <summary>
    /// 所有静态资源的管理器
    /// </summary>
    public static class AllStatic
    {
        /// <summary>
        /// 数据源、数据表、数据库操作
        /// </summary>
        public static Dictionary<string, Dictionary<string, dynamic>> InverseRepositoryMap { set; get; }

        /// <summary>
        /// 分布式雪花id生成器
        /// </summary>
        public static IdWorker IdWorker { set; get; }

        /// <summary>
        /// 默认的时间转换器
        /// </summary>
        public static IsoDateTimeConverter TimeConverter { set; get; }
        
        static AllStatic()
        {
            TimeConverter = new IsoDateTimeConverter();
            TimeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            InverseRepositoryMap = new Dictionary<string, Dictionary<string, dynamic>>();
        }
    }
}
