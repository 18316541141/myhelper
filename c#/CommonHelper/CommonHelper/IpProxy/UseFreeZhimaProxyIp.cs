using CommonHelper.Helper;
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
    /// 使用免费版芝麻代理
    /// </summary>
    public class UseFreeZhimaProxyIp : UseProxyIp
    {
        /// <summary>
        /// 代理ip缓存的保存的文件位置，保存格式化后的报文
        /// </summary>
        protected override string ProxyIpCacheFilePath
        {
            get { return "FreeZhimaProxyIpCache.txt"; }
        }

        /// <summary>
        /// 检查是否添加白名单，如果需要则添加白名单，
        /// 部分第三方代理ip提供商需要对本机的外网ip进行加白才能调用。
        /// </summary>
        /// <returns>返回false，进行往下走，返回true，已进行加白操作，需要再试一次获取代理ip</returns>
        public override bool CheckNeedAddWhite(string text) => text.Contains("白名单");

        /// <summary>
        /// 芝麻代理获取ip地址，可以有多个。当其中一个获取不了则从下一个开始获取。
        /// </summary>
        protected override Queue<ProxyUserInfo> ProxyUserInfoQueue
        {
            get
            {
                Queue<ProxyUserInfo> proxyUserInfoQueue = new Queue<ProxyUserInfo>();
                foreach (JObject jobject in JArray.Parse(ConfigurationManager.AppSettings[$"{AllStatic.EnvironmentType}.FreeZhimaProxyRefresh"]))
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
        /// 部分代理的返回报文是没有使用期限的，需要使用该方法添加使用期限
        /// </summary>
        /// <param name="text">源报文</param>
        /// <returns>返回含有期限的ip代理信息</returns>
        public override string AddLimitDate(string text)=>
            new JObject
            {
                ["ProxyIp"] = text,
                ["LimitDate"] = DateTime.Now.AddMinutes(5).ToString("yyyy-MM-dd HH:mm:ss")
            }.ToString();

        /// <summary>
        /// 解析格式化后的报文
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
