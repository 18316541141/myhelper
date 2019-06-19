using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CommonHelper.IpProxy
{
    /// <summary>
    /// 蜻蜓代理，十分慢
    /// </summary>
    public class QingtingProxyIp:UseProxyIp
    {
        /// <summary>
        /// 代理ip缓存的保存的文件位置，保存格式化后的报文
        /// </summary>
        protected override string ProxyIpCacheFilePath
        {
            get { return "QingtingProxyIpCache.txt"; }
        }

        /// <summary>
        /// 代理ip刷新地址，默认是：http://dynamic.goubanjia.com/dynamic/get/c8667ab533b7a454cf9e157539947632.html?sep=5 可继承后重写
        /// </summary>
        protected override Queue<ProxyUserInfo> ProxyUserInfoQueue
        {
            get
            {
                Queue<ProxyUserInfo> proxyUserInfoQueue = new Queue<ProxyUserInfo>();
                return proxyUserInfoQueue;
            }
        }

        public override bool CheckNeedAddWhite(string text) => text.Contains("白名单");

        /// <summary>
        /// 部分代理的返回报文是没有使用期限的，需要使用该方法添加使用期限
        /// </summary>
        /// <param name="text">源报文</param>
        /// <returns>返回含有期限的ip代理信息</returns>
        public override string AddLimitDate(string text)=>text;

        /// <summary>
        /// 把代理ip信息解析成缓存
        /// </summary>
        /// <param name="ipProxyCacheList">ip地址缓存列表</param>
        /// <param name="portProxyCacheList">端口缓存列表</param>
        /// <param name="limitDateCacheList">到期时间缓存列表</param>
        /// <param name="text"></param>
        public override bool Explain(out List<string> ipProxyCacheList, out List<string> portProxyCacheList, out List<DateTime?> limitDateCacheList, string text)
        {
            ipProxyCacheList=new List<string>();
            portProxyCacheList = new List<string>();
            limitDateCacheList=new List<DateTime?>();
            foreach (JObject jObject in JObject.Parse(text)["data"])
            {
                ipProxyCacheList.Add(Convert.ToString(jObject["ip"]));
                portProxyCacheList.Add(Convert.ToString(jObject["port"]));
                limitDateCacheList.Add(DateTime.ParseExact(Convert.ToString(jObject["expire_time"]),"yyyy-MM-dd HH:mm:ss",CultureInfo.CurrentCulture));
            }
            return true;
        }
    }
}
