using CommonHelper.Helper;
using CommonHelper.IpProxy;
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
    /// 使用收费芝麻代理
    /// </summary>
    public class UseChargeZhimaProxyIp : UseProxyIp
    {
        /// <summary>
        /// ip代理缓存文件
        /// </summary>
        protected override string ProxyIpCacheFilePath
        {
            get
            {
                return "ChargeZhimaProxyIpCache.txt";
            }
        }

        /// <summary>
        /// 代理用户信息列表
        /// </summary>
        protected override Queue<ProxyUserInfo> ProxyUserInfoQueue
        {
            get
            {
                Queue<ProxyUserInfo> proxyUserInfoQueue = new Queue<ProxyUserInfo>();
                foreach (JObject jobject in JArray.Parse(ConfigurationManager.AppSettings["ChargeZhimaProxyRefresh"]))
                    proxyUserInfoQueue.Enqueue(new ProxyUserInfo
                    {
                        User = Convert.ToString(jobject["user"]),
                        GetProxyIpUrl = Convert.ToString(jobject["getProxyIpUrl"]),
                        WhiteIpTemplate = Convert.ToString(jobject["whiteIpTemplate"]),
                    });
                return ProxyUserInfoQueue;
            }
        }

        /// <summary>
        /// 添加使用期限
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public override string AddLimitDate(string text)=>
            new JObject
            {
                ["ProxyIp"] = text,
                ["LimitDate"] = DateTime.Now.AddMinutes(5).ToString("yyyy-MM-dd HH:mm:ss")
            }.ToString();

        /// <summary>
        /// 检查是否需要加白名单，如果需要加白名单则进行加白操作并返回true，
        /// 如果不需要则返回false
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public override bool CheckNeedAddWhite(string text) => text.Contains("白名单");

        /// <summary>
        /// 解析报文。
        /// </summary>
        /// <param name="ipProxyCacheList"></param>
        /// <param name="portProxyCacheList"></param>
        /// <param name="limitDateCacheList"></param>
        /// <param name="text"></param>
        public override bool Explain(out List<string> ipProxyCacheList, out List<string> portProxyCacheList, out List<DateTime?> limitDateCacheList, string text)
        {
            ipProxyCacheList = new List<string>();
            portProxyCacheList = new List<string>();
            limitDateCacheList = new List<DateTime?>();
            if (text.Contains("套餐已过期") || text.Contains("请更换条件再试") || text.Contains("已到达上限"))
                return false;
            string[] ipAndPortArray;
            JObject jObject = JObject.Parse(text);
            foreach (string ipAndPort in Convert.ToString(jObject["ProxyIp"]).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
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
