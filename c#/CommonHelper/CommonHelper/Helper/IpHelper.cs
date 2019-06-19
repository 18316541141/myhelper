using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommonHelper.Helper
{
    /// <summary>
    /// ip地址帮助类
    /// </summary>
    public class IpHelper
    {
        /// <summary>
        /// 获取本机在外网的ip地址
        /// </summary>
        /// <returns></returns>
        public static string GetOuterNetIP()
        {
            using (var webClient = new WebClient())
                try
                {
                    webClient.Credentials = CredentialCache.DefaultCredentials;
                    byte[] pageDate = webClient.DownloadData("http://pv.sohu.com/cityjson?ie=utf-8");
                    String ip = Encoding.UTF8.GetString(pageDate);
                    webClient.Dispose();
                    Match rebool = Regex.Match(ip, @"\d{2,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}");
                    return rebool.Value;
                }
                catch (Exception e)
                {
                    return "";
                }
        }


    }
}
