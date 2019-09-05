using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.App_Start;

namespace CommonHelper.IpProxy
{
    public class UseTaiYangProxyIp : UseProxyIp
    {
        protected override string ProxyIpCacheFilePath
        {
            get
            {
                return "TaiYangProxyIpCache.txt";
            }
        }

        protected override Queue<ProxyUserInfo> ProxyUserInfoQueue
        {
            get
            {
                Queue<ProxyUserInfo> _proxyUserInfoQueue = new Queue<ProxyUserInfo>();
                foreach (JObject jobject in JArray.Parse(ConfigurationManager.AppSettings[$"{EnvironmentConfig.EnvironmentType}.TaiYangProxyRefresh"]))
                {
                    _proxyUserInfoQueue.Enqueue(new ProxyUserInfo
                    {
                        User = Convert.ToString(jobject["user"]),
                        GetProxyIpUrl = Convert.ToString(jobject["getProxyIpUrl"]),
                        WhiteIpTemplate = Convert.ToString(jobject["whiteIpTemplate"]),
                    });
                }
                return _proxyUserInfoQueue;
            }
        }

        public override string AddLimitDate(string text)
        {
            return new JObject
            {
                ["ProxyIp"] = text,
                ["LimitDate"] = DateTime.Now.AddMinutes(5).ToString("yyyy-MM-dd HH:mm:ss")
            }.ToString();
        }

        public override bool CheckNeedAddWhite(string text)
        {
            return text.Contains("白名单");
        }

        public override bool Explain(out List<string> ipProxyCacheList, out List<string> portProxyCacheList, out List<DateTime?> limitDateCacheList, string text)
        {
            ipProxyCacheList = new List<string>();
            portProxyCacheList = new List<string>();
            limitDateCacheList = new List<DateTime?>();
            if (text.Contains("您的该套餐已过期") || text.Contains("请更换条件再试") || text.Contains("您的余额可能不足"))
                return false;
            string[] ipAndPortArray;
            JObject jObject = JObject.Parse(text);
            foreach (string ipAndPort in Convert.ToString(jObject["ProxyIp"]).Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
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
