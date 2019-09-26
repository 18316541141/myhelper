using CommonCrawler.Intf;
using CommonHelper.Helper;
using CommonHelper.staticVar;
using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace WebApplication1.Controllers.Common
{
    /// <summary>
    /// 基础控制器
    /// </summary>
    public abstract class BaseController
    {

		/// <summary>
        /// 跨平台的斜杠
        /// </summary>
        protected static readonly char s;

        static BaseController()
        {
            LastHeartbeatDate = DateTime.Now;
			s = Path.DirectorySeparatorChar;
        }

        /// <summary>
        /// 日志输出类
        /// </summary>
        public ILog Log { set; get; }

        /// <summary>
        /// 爬虫定制化功能
        /// </summary>
        public ICrawlerService CrawlerService { set; get; }

        /// <summary>
        /// 监视服务器的域名地址
        /// </summary>
        protected string MonitorServer { set; get; }

        /// <summary>
        /// 签名用的key
        /// </summary>
        protected string SignKey { set; get; }

        /// <summary>
        /// 签名用的密钥
        /// </summary>
        protected string SignSecret { set; get; }

        /// <summary>
        /// 开发者的openId列表
        /// </summary>
        protected List<string> OpenIds { set; get; }

        /// <summary>
        /// 机器人id
        /// </summary>
        protected string RobotId { set; get; }

        public BaseController()
        {
            MonitorServer = ConfigurationManager.AppSettings[$"{AllStatic.EnvironmentType}.MonitorServer"];
            SignKey = ConfigurationManager.AppSettings[$"{AllStatic.EnvironmentType}.SignKey"];
            SignSecret = ConfigurationManager.AppSettings[$"{AllStatic.EnvironmentType}.SignSecret"];
            RobotId = ConfigurationManager.AppSettings[$"{AllStatic.EnvironmentType}.RobotId"];
            OpenIds = new List<string>();
            foreach (JValue val in JArray.Parse(ConfigurationManager.AppSettings[$"{AllStatic.EnvironmentType}.OpenIds"]))
            {
                OpenIds.Add(Convert.ToString(val));
            }
        }

        /// <summary>
        /// 最近一次心跳时间，避免频繁发送心跳
        /// </summary>
        private static DateTime LastHeartbeatDate { set; get; }

        /// <summary>
        /// 发送心跳报文
        /// </summary>
        public void SendHeartbeat()
        {
            if ((DateTime.Now - LastHeartbeatDate).TotalSeconds > 60)
            {
                try
                {
                    HttpWebRequestHelper.HttpGet($"{MonitorServer}HeartbeatEntity/Send", MySignHelper.New(SignKey, SignSecret)
                        .Add("robotId", RobotId)
                        .Params());
                    LastHeartbeatDate = DateTime.Now;
                }
                catch (Exception ex)
                {
                    Log.Error($"发送心跳报文出错，错误原因：{ex.Message}",ex);
                    CrawlerService.HeartbeatException(ex);
                }
            }
        }
    }
}