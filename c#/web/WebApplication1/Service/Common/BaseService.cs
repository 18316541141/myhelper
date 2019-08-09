
using log4net;
using Snowflake.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Service.Common
{
    /// <summary>
    /// 基础业务类
    /// </summary>
    public abstract class BaseService
    {

        protected ILog log;

        public BaseService()
        {
            log = LogManager.GetLogger("Log4net.config");
        }

        static IdWorker idWorker;

        static BaseService()
        {
            idWorker = new IdWorker(1, 1);
        }

        /// <summary>
        /// 使用分布式雪花算法生成唯一id
        /// </summary>
        /// <returns></returns>
        public long NextId()
        {
            return idWorker.NextId();
        }

        /// <summary>
        /// 异常信息
        /// </summary>
        /// <param name="msg"></param>
        protected void Ex(string msg)
        {
            throw new Exception(msg + "(Error:-1)");
        }
    }
}