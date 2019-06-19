using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.IpProxy
{
    /// <summary>
    /// 公司默认使用的ip代理类，可以继承后重写，接入其他第三方ip代理
    /// </summary>
    public class UseDefaultProxyIp: UseProxyIp
    {
        /// <summary>
        /// 代理ip缓存的保存的文件位置，保存格式化后的报文
        /// </summary>
        protected override string ProxyIpCacheFilePath
        {
            get { return "DefaultProxyIpCache.txt"; }
        }

        /// <summary>
        /// 代理ip刷新地址，默认是：http://dynamic.goubanjia.com/dynamic/get/c8667ab533b7a454cf9e157539947632.html?sep=5 可继承后重写，
        /// 可以有多个
        /// </summary>
        protected override Queue<ProxyUserInfo> ProxyUserInfoQueue
        {
            get
            {
                Queue<ProxyUserInfo> proxyUserInfoQueue = new Queue<ProxyUserInfo>();
                proxyUserInfoQueue.Enqueue(new ProxyUserInfo
                {
                    User="default",
                    GetProxyIpUrl= "http://dynamic.goubanjia.com/dynamic/get/c8667ab533b7a454cf9e157539947632.html?sep=5"
                });
                return proxyUserInfoQueue;
            }
        }

        /// <summary>
        /// 检查是否添加白名单，如果需要则添加白名单，
        /// 部分第三方代理ip提供商需要对本机的外网ip进行加白才能调用。
        /// </summary>
        /// <returns>返回false，进行往下走，返回true，已进行加白操作，需要再试一次获取代理ip</returns>
        public override bool CheckNeedAddWhite(string text) => false;

        /// <summary>
        /// 部分代理的返回报文是没有使用期限的，需要使用该方法添加使用期限
        /// </summary>
        /// <param name="text">源报文</param>
        /// <returns>返回含有期限的ip代理信息</returns>
        public override string AddLimitDate(string text)
        {
            return new JObject
            {
                ["ProxyIp"] = text,
                ["LimitDate"] = DateTime.Now.AddMinutes(1).ToString("yyyy-MM-dd HH:mm:ss")
            }.ToString();
        }

        /// <summary>
        /// 把代理ip信息解析成缓存
        /// </summary>
        /// <param name="ipProxyCacheList">ip地址缓存列表</param>
        /// <param name="portProxyCacheList">端口缓存列表</param>
        /// <param name="limitDateCacheList">到期时间缓存列表</param>
        /// <param name="text"></param>
        public override bool Explain(out List<string> ipProxyCacheList, out List<string> portProxyCacheList, out List<DateTime?> limitDateCacheList, string text)
        {
            string[] ipAndPortArray;
            JObject jObject = JObject.Parse(text);
            ipProxyCacheList = new List<string>();
            portProxyCacheList = new List<string>();
            limitDateCacheList = new List<DateTime?>();
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
