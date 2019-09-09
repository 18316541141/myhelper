using CommonHelper.Helper;
using CommonHelper.IpProxy;
using CommonHelper.staticVar;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CommonHelper.IpProxy
{
    /// <summary>
    /// 迅联http代理
    /// </summary>
    public class FreeXunlianProxyIp : UseProxyIp
    {
        protected override string ProxyIpCacheFilePath
        {
            get
            {
                return "FreeXunlianProxyCache.txt";
            }
        }

        protected override Queue<ProxyUserInfo> ProxyUserInfoQueue
        {
            get
            {
                Queue<ProxyUserInfo> proxyUserInfoQueue = new Queue<ProxyUserInfo>();
                foreach (JObject jobject in JArray.Parse(ConfigurationManager.AppSettings[$"{AllStatic.EnvironmentType}.FreeXunlianProxyRefresh"]))
                    proxyUserInfoQueue.Enqueue(new ProxyUserInfo
                    {
                        User = Convert.ToString(jobject["user"]),
                        GetProxyIpUrl = Convert.ToString(jobject["getProxyIpUrl"]),
                        WhiteIpTemplate = Convert.ToString(jobject["whiteIpTemplate"]),
                    });
                return proxyUserInfoQueue;
            }
        }

        public override string AddLimitDate(string text)=>
            new JObject
            {
                ["ProxyIp"] = text,
                ["LimitDate"] = DateTime.Now.AddMinutes(5).ToString("yyyy-MM-dd HH:mm:ss")
            }.ToString();

        public override bool CheckNeedAddWhite(string text) => text.Contains("白名单");

        public override bool Explain(out List<string> ipProxyCacheList, out List<string> portProxyCacheList, out List<DateTime?> limitDateCacheList, string text)
        {
            ipProxyCacheList = new List<string>();
            portProxyCacheList = new List<string>();
            limitDateCacheList = new List<DateTime?>();
            if (text.Contains("提取过快") || text.Contains("传参有误") || text.Contains("接返回数据为空") || text.Contains("有效IP数量不够") || text.Contains("不允许获取") || text.Contains("太频繁"))
                return false;
            string[] ipAndPortArray;
            JObject jObject = JObject.Parse(text);
            foreach (string ipAndPort in Convert.ToString(jObject["ProxyIp"]).Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
            {
                ipAndPortArray = ipAndPort.Split(':');
                ipProxyCacheList.Add(ipAndPortArray[0].Trim());
                portProxyCacheList.Add(ipAndPortArray[1].Trim());
                limitDateCacheList.Add(DateTime.ParseExact(Convert.ToString(jObject["LimitDate"]), "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture));
            }
            return true;
        }
    }
}
