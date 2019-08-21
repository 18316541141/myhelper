using CommonHelper.Helper;
using CX_Task_Center.Code.Message;
using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using WebApplication1.Entity;
using WebApplication1.Entity.Common;

namespace WebApplication1.Controllers.Common
{
    /// <summary>
    /// 基础控制器
    /// </summary>
    public abstract class BaseController
    {
        /// <summary>
        /// 最近一次发送心跳的时间
        /// </summary>
        static DateTime _lastHeartbeatDate { set; get; }

        static BaseController()
        {
            _lastHeartbeatDate = DateTime.Now;
        }

        /// <summary>
        /// 日志输出类
        /// </summary>
        public ILog Log { set; get; }

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

        /// <summary>
        /// 办事易微信报警类
        /// </summary>
        public BsyWarningHelper BsyWarningHelper { set; get; }

        public BaseController()
        {
            MonitorServer = ConfigurationManager.AppSettings["MonitorServer"];
            SignKey = ConfigurationManager.AppSettings["SignKey"];
            SignSecret = ConfigurationManager.AppSettings["SignSecret"];
            RobotId = ConfigurationManager.AppSettings["RobotId"];
            OpenIds = new List<string>();
            foreach (JValue val in JArray.Parse(ConfigurationManager.AppSettings["OpenIds"]))
            {
                OpenIds.Add(Convert.ToString(val));
            }
        }

        /// <summary>
        /// 发送心跳报文，如果发不了说明服务器挂了，则马上报警;
        /// 如果长时间未发送心跳，服务器会认为机器人挂了，则马上报警。
        /// </summary>
        public void Heartbeat()
        {
            if((DateTime.Now- _lastHeartbeatDate).TotalMinutes >= 1)
            {
                var param = MySignHelper.New(SignKey, SignSecret)
                    .Add("robotId", RobotId)
                    .Params();
                try
                {
                    JObject jobject = HttpWebRequestHelper.HttpGet($"{MonitorServer}index/heartbeat", param).GetJsonObj();
                    if(Convert.ToInt32(jobject["code"]) != 0)
                    {
                        foreach (string openId in OpenIds)
                        {
                            BsyWarningHelper.SendWarning($@"
                            <warning type='01' openId='{openId}'>
                                <title>监测心跳的服务器报错</title>
                                <source title='报警来源'>{RobotId}</source>
                                <datetime title='发生时间'>{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}</datetime>
                                <content title='报警内容'>发送心跳报文时，返回错误</content>
                                <remark>代码逻辑错误</remark>
                                <url><![CDATA[{MonitorServer}]]></url>
                            </warning>
                            ");
                        }
                    }
                }
                catch (Exception ex)
                {
                    foreach (string openId in OpenIds)
                    {
                        BsyWarningHelper.SendWarning($@"
                            <warning type='01' openId='{openId}'>
                                <title>监测心跳的服务器无法连接</title>
                                <source title='报警来源'>{RobotId}</source>
                                <datetime title='发生时间'>{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}</datetime>
                                <content title='报警内容'>发送心跳报文时，服务器连不上。</content>
                                <remark>错误详情：{ex.Message}</remark>
                                <url><![CDATA[{MonitorServer}]]></url>
                            </warning>
                            ");
                    }
                }
                finally
                {
                    _lastHeartbeatDate = DateTime.Now;
                }
            }
        }
    }
}