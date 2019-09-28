
using Newtonsoft.Json.Linq;
using CommonHelper.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace CommonHelper.IpProxy
{
    /// <summary>
    /// 抽象的接入其他第三方ip代理类
    /// </summary>
    public abstract class UseProxyIp
    {
        /// <summary>
        /// 代理ip缓存的保存的文件位置，保存格式化后的报文
        /// </summary>
        protected abstract string ProxyIpCacheFilePath { get; }

        /// <summary>
        /// 代理ip的用户信息列表
        /// </summary>
        protected abstract Queue<ProxyUserInfo> ProxyUserInfoQueue {  get; }

        /// <summary>
        /// 第三方ip代理厂商有些是一次性返回多个
        /// 代理ip，通常每次轮流使用这些ip，轮流的规则是ip数%_indexCach 得到的余数
        /// 每次_indexCache都会增加
        /// </summary>
        ushort _indexCache;

        /// <summary>
        /// ip代理缓存
        /// </summary>
        public List<string> IpProxyCacheList;

        /// <summary>
        /// ip代理的端口缓存
        /// </summary>
        List<string> _portProxyCacheList;

        /// <summary>
        /// ip代理超时时间
        /// </summary>
        List<DateTime?> _limitDateCacheList { set; get; }

        public UseProxyIp()
        {
            IpProxyCacheList = new List<string>();
            _portProxyCacheList = new List<string>();
            _limitDateCacheList = new List<DateTime?>();
            _indexCache = (ushort)new Random().Next();
        }

        /// <summary>
        /// 检查是否需要添加白名单，如果需要则添加白名单，
        /// 部分第三方代理ip提供商需要对本机的外网ip进行加白才能调用。
        /// </summary>
        /// <param name="text">需要检查的报文</param>
        /// <returns>如果不需要加白名单，则返回false，如果需要加白名单返回true</returns>
        public abstract bool CheckNeedAddWhite(string text);

        /// <summary>
        /// 返回当前或下一个用户。
        /// </summary>
        /// <param name="next">
        ///     如果next=false，则返回同一个用户、如果next=true则返回下一个用户。
        /// </param>
        /// <returns></returns>
        public ProxyUserInfo CurrentProxyUserInfo(bool next=false)
        {
            if (next && ProxyUserInfoQueue.Count > 1)
                lock (this)
                    if (next && ProxyUserInfoQueue.Count > 1)
                        ProxyUserInfoQueue.Dequeue();
            return ProxyUserInfoQueue.Peek();
        }

        /// <summary>
        /// 部分代理的返回报文是没有使用期限的，需要使用该方法添加使用期限
        /// </summary>
        /// <param name="text">源报文</param>
        /// <returns>返回含有期限的ip代理信息</returns>
        public abstract string AddLimitDate(string text);

        /// <summary>
        /// 把代理ip信息解析成缓存
        /// </summary>
        /// <param name="ipProxyCacheList">ip地址缓存列表</param>
        /// <param name="portProxyCacheList">端口缓存列表</param>
        /// <param name="limitDateCacheList">到期时间缓存列表</param>
        /// <param name="text">数据</param>
        /// <returns>是否正确解析，如果正确解析返回true</returns>
        public abstract bool Explain(out List<string> ipProxyCacheList, out List<string> portProxyCacheList, out List<DateTime?> limitDateCacheList, string text);

        /// <summary>
        /// 加载或刷新代理ip，当内存没有缓存代理ip时，读磁盘缓存，如果磁盘没缓存
        /// 则获取新的代理ip；当缓存的代理ip过时，则再次去获取新的代理ip
        /// </summary>
        /// <returns>如果加载或刷新成功返回true</returns>
        public virtual bool LoadOrRefreshProxyIp()
        {
            ProxyUserInfo proxyUserInfo = CurrentProxyUserInfo();
            lock (typeof(UseProxyIp))
            {
                if (IpProxyCacheList.Count == 0 && _portProxyCacheList.Count == 0 && _limitDateCacheList.Count == 0)
                {
                    try
                    {
                        Console.WriteLine($"发现没有缓存的代理ip，从磁盘读取“{ProxyIpCacheFilePath}”缓存信息....");
                        List<DateTime?> tempLimitDateCacheList=null;
                        if (!Explain(out IpProxyCacheList, out _portProxyCacheList, out tempLimitDateCacheList, File.ReadAllText(ProxyIpCacheFilePath)))
                            Console.WriteLine("解析缓存信息失败！");
                        _limitDateCacheList= tempLimitDateCacheList;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"读取缓存信息出错",ex);
                    }
                }
                Console.WriteLine("检查代理ip是否有效。。。");
                if (IpProxyCacheList.Count == 0 || DateTime.Now > _limitDateCacheList[_indexCache % IpProxyCacheList.Count])
                {
                    Console.WriteLine($"发送：{proxyUserInfo.GetProxyIpUrl} 获取代理ip");
                    string retText = HttpWebRequestHelper.HttpGet(proxyUserInfo.GetProxyIpUrl).GetText();
                    while (CheckNeedAddWhite(retText))
                    {
                        Console.WriteLine($"获取代理ip时返回：{retText}");
                        string whiteIp = NetworkHelper.GetOuterNetIP();
                        Console.WriteLine($"需要添加 {whiteIp} 到白名单。");
                        string ret = HttpWebRequestHelper.HttpGet(CurrentProxyUserInfo().GetWhiteIpUrl(NetworkHelper.GetOuterNetIP())).GetText();
                        Console.WriteLine($"添加白名单返回结果：{ret}");
                        retText = HttpWebRequestHelper.HttpGet(proxyUserInfo.GetProxyIpUrl).GetText();
                    }
                    retText = AddLimitDate(retText);
                    Console.WriteLine($"获取代理ip结果：{retText}...");
                    Console.WriteLine("正在解析代理ip....");
                    List<DateTime?> tempLimitDateCacheList = null;
                    if (Explain(out IpProxyCacheList, out _portProxyCacheList, out tempLimitDateCacheList, retText))
                    {
                        File.WriteAllText(ProxyIpCacheFilePath, retText);
                        _limitDateCacheList = tempLimitDateCacheList;
                    }
                    else
                    {
                        Console.WriteLine($"解析代理ip出错");
                        CurrentProxyUserInfo(true);
                        Console.WriteLine($"当前获取代理ip的url已失效，切换到下一个url：'{proxyUserInfo.GetProxyIpUrl}'。");
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 将代理设置为超时
        /// </summary>
        /// <param name="ipPort"></param>
        public bool VpnTimeout(IpPort ipPort)
        {
            lock (typeof(UseProxyIp))
                for (int i = 0, len = IpProxyCacheList.Count; i<len;i++)
                    if(IpProxyCacheList[i]==ipPort.Ip && _portProxyCacheList[i] == ipPort.Port)
                    {
                        _limitDateCacheList[i] = _limitDateCacheList[i].Value.AddDays(-1);
                        return true;
                    }
            return false;
        }

        /// <summary>
        /// ip代理地址，如果有缓存则直接使用缓存，没有则刷新；当缓存超时也会刷新
        /// </summary>
        public virtual IpPort IpPort
        {
            get
            {
                IpPort ipPort;
                if (LoadOrRefreshProxyIp())
                {
                    ipPort.Ip = IpProxyCacheList[_indexCache % IpProxyCacheList.Count];
                    ipPort.Port = _portProxyCacheList[_indexCache % IpProxyCacheList.Count];
                    _indexCache++;
                }else
                {
                    ipPort.Ip = "";
                    ipPort.Port = "";
                }
                return ipPort;
            }
        }
    }
}
