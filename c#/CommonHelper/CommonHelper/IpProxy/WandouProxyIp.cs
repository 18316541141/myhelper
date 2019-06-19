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
    /// 豌豆代理，十分慢
    /// </summary>
    public class WandouProxyIp:UseProxyIp
    {
        /// <summary>
        /// 代理ip缓存的保存的文件位置，保存格式化后的报文
        /// </summary>
        protected override string ProxyIpCacheFilePath
        {
            get { return "WandouProxyIpCache.txt"; }
        }

        /// <summary>
        /// 代理ip刷新地址，默认是：http://dynamic.goubanjia.com/dynamic/get/c8667ab533b7a454cf9e157539947632.html?sep=5 可继承后重写
        /// </summary>
        protected override Queue<ProxyUserInfo> ProxyUserInfoQueue
        {
            get
            {
                
                return null;
            }
        }

        public override bool CheckNeedAddWhite(string text) => text.Contains("白名单");

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
                ["LimitDate"] = DateTime.Now.AddMinutes(5).ToString("yyyy-MM-dd HH:mm:ss")
            }.ToString();
        }

        /// <summary>
        /// 解析格式化后的报文
        /// </summary>
        /// <param name="ipProxyCacheList"></param>
        /// <param name="portProxyCacheList"></param>
        /// <param name="limitDateCacheList"></param>
        /// <param name="text"></param>
        public override bool Explain(out List<string> ipProxyCacheList, out List<string> portProxyCacheList, out List<DateTime?> limitDateCacheList, string text)
        {
            string[] ipAndPortArray;
            JObject jObject = JObject.Parse(text);
            ipProxyCacheList = new List<string>();
            portProxyCacheList = new List<string>();
            limitDateCacheList = new List<DateTime?>();
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
