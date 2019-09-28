using CommonCrawler.Intf;
using CommonHelper.Entity;
using CommonHelper.Helper;
using CommonHelper.staticVar;
using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using WebApplication1.Entity;

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
            LastHeartbeatDate = DateTime.MinValue;
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

        public BaseController()
        {
            MonitorServer = ConfigurationManager.AppSettings[$"{AllStatic.EnvironmentType}.MonitorServer"];
            SignKey = ConfigurationManager.AppSettings[$"{AllStatic.EnvironmentType}.SignKey"];
            SignSecret = ConfigurationManager.AppSettings[$"{AllStatic.EnvironmentType}.SignSecret"];
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
        /// 发送心跳报文，冷却时间为5分钟一次。
        /// </summary>
        public void SendHeartbeat()
        {
            if ((DateTime.Now - LastHeartbeatDate).TotalMinutes > 5)
            {
                try
                {
                    HeartbeatEntity heartbeatEntity = CrawlerService.GetHeartbeatEntityInfo();
                    var param = MySignHelper.New(SignKey, SignSecret).Params();
                    param.Add("RobotMac", NetworkHelper.GetMacAddress());
                    param.Add("Remark", heartbeatEntity.Remark);
                    param.Add("ExtendField", heartbeatEntity.ExtendField);
                    param.Add("MonitorServer", heartbeatEntity.MonitorServer);
                    string text = HttpWebRequestHelper.HttpGet($"{MonitorServer}HeartbeatEntity/Send", param).GetText();
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