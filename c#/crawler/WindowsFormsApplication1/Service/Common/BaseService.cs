
using log4net;
using Newtonsoft.Json;
using Snowflake.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebApplication1.Entity.Common;

namespace WebApplication1.Service.Common
{
    /// <summary>
    /// 基础业务类
    /// </summary>
    public abstract class BaseService
    {
        /// <summary>
        /// 日志输出类
        /// </summary>
        public ILog Log { set; get; }

        /// <summary>
        /// 分布式雪花id生成器
        /// </summary>
        public IdWorker IdWorker { set; get; }

        /// <summary>
        /// 跨平台的斜杠
        /// </summary>
        protected static char s;

        static BaseService()
        {
            s = Path.DirectorySeparatorChar;
        }

        /// <summary>
        /// 使用分布式雪花算法生成唯一id
        /// </summary>
        /// <returns></returns>
        public long NextId()
        {
            return IdWorker.NextId();
        }
        
    }
}