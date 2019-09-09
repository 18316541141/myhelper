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
    /// 飞蚁代理
    /// </summary>
    public sealed class FeiYiProxyIp : UseProxyIp
    {
        protected override string ProxyIpCacheFilePath
        {
            get
            {
                return "FeiYiProxyIpCache.txt";
            }
        }

        protected sealed override Queue<ProxyUserInfo> ProxyUserInfoQueue
        {
            get
            {
                Queue<ProxyUserInfo> _proxyUserInfoQueue = new Queue<ProxyUserInfo>();
                foreach (JObject jobject in JArray.Parse(ConfigurationManager.AppSettings[$"{AllStatic.EnvironmentType}.FeiYiProxyRefresh"]))
                {
                    _proxyUserInfoQueue.Enqueue(new ProxyUserInfo
                    {
                        User = Convert.ToString(jobject["user"]),
                        GetProxyIpUrl = Convert.ToString(jobject["getProxyIpUrl"])
                    });
                }
                return _proxyUserInfoQueue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public sealed override string AddLimitDate(string text)
        {
            string[] rows=text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder textBuilder = new StringBuilder();
            foreach (string row in rows)
            {
                if (row.Contains(":"))
                {
                    textBuilder.Append(row).Append("\r\n");
                }
            }
            return new JObject
            {
                ["ProxyIp"] = textBuilder.ToString(),
                ["LimitDate"] = DateTime.Now.AddMinutes(2).ToString("yyyy-MM-dd HH:mm:ss")
            }.ToString();
        }

        public override bool CheckNeedAddWhite(string text) => false;

        public sealed override bool Explain(out List<string> ipProxyCacheList, out List<string> portProxyCacheList, out List<DateTime?> limitDateCacheList, string text)
        {
            ipProxyCacheList = new List<string>();
            portProxyCacheList = new List<string>();
            limitDateCacheList = new List<DateTime?>();
            if (text.Contains("您的该套餐已过期") || text.Contains("请更换条件再试") || text.Contains("您的余额可能不足"))
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
