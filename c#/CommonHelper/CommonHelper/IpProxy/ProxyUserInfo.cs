using Antlr3.ST;
using CommonHelper.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.IpProxy
{
    /// <summary>
    /// 代理用户信息类
    /// </summary>
    public class ProxyUserInfo
    {
        /// <summary>
        /// 代理的注册用户
        /// </summary>
        public string User { set; get; }

        /// <summary>
        /// 获取代理ip的url
        /// </summary>
        public string GetProxyIpUrl { set; get; }

        /// <summary>
        /// 设置白名单的模板
        /// </summary>
        public string WhiteIpTemplate{set; get;}

        /// <summary>
        /// 获取设置白名单的url
        /// </summary>
        /// <param name="whiteIp"></param>
        /// <returns></returns>
        public string GetWhiteIpUrl(string whiteIp)
        {
            StringTemplate st = new StringTemplate(WhiteIpTemplate);
            st.SetAttribute("whiteIp", whiteIp);
            return st.ToString();
        }
    }
}
