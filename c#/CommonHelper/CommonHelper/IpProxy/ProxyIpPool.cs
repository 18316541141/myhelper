using CommonHelper.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonHelper.IpProxy
{
    /// <summary>
    /// 代理ip池
    /// </summary>
    public class ProxyIpPool
    {
        List<UseProxyIp> _useProxyIpList;
        ushort _randomIndex;
        int _usableIpCount;
        public ProxyIpPool(params UseProxyIp[] proxyIps)
        {
            _useProxyIpList = new List<UseProxyIp>(proxyIps);
            _randomIndex =(ushort) new Random().Next();
            _usableIpCount = -1;
        }
        public ProxyIpPool(List<UseProxyIp> useProxyIpList)
        {
            _useProxyIpList = useProxyIpList;
            _randomIndex = (ushort)new Random().Next();
            _usableIpCount = -1;
        }

        /// <summary>
        /// 轮流从各个第三方ip代理接口中获取可用的代理ip。
        /// </summary>
        public IpPort IpPort
        {
            get
            {
                lock (typeof(UseProxyIp))
                {
                    UseProxyIp useProxyIp = _useProxyIpList[_randomIndex % _useProxyIpList.Count];
                    for (; _usableIpCount <1; Thread.Sleep(1000))
                    {
                        _randomIndex++;
                        useProxyIp = _useProxyIpList[_randomIndex % _useProxyIpList.Count];
                        if(useProxyIp.LoadOrRefreshProxyIp())
                            _usableIpCount = useProxyIp.IpProxyCacheList.Count;
                    }
                    _usableIpCount--;
                    return useProxyIp.IpPort;
                }
            }
            private set { }
        }

        /// <summary>
        /// 告诉代理池，该ip已超时。
        /// </summary>
        public void VpnTimeout(IpPort ipPort)
        {
            lock (typeof(UseProxyIp))
                foreach (UseProxyIp useProxyIp in _useProxyIpList)
                    if (useProxyIp.VpnTimeout(ipPort))
                        break;
        }
    }
}
