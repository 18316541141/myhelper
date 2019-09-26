
using CommonHelper.staticVar;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Snowflake.Net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using WebApplication1.Entity.Common;

namespace CommonCrawler.Service.Common
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
        protected readonly static char s;

        /// <summary>
        /// 机器人id
        /// </summary>
        protected string RobotId { set; get; }

        /// <summary>
        /// 开发者的openId列表
        /// </summary>
        protected List<string> OpenIds { set; get; }

        /// <summary>
        /// 监视服务器的域名地址
        /// </summary>
        protected string MonitorServer { set; get; }

        public BaseService()
        {
            MonitorServer = ConfigurationManager.AppSettings[$"{AllStatic.EnvironmentType}.MonitorServer"];
            RobotId = ConfigurationManager.AppSettings[$"{AllStatic.EnvironmentType}.RobotId"];
            OpenIds = new List<string>();
            foreach (JValue val in JArray.Parse(ConfigurationManager.AppSettings[$"{AllStatic.EnvironmentType}.OpenIds"]))
            {
                OpenIds.Add(Convert.ToString(val));
            }
        }


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