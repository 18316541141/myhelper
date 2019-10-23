using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace CommonHelper.Helper
{
    /// <summary>
    /// ip地址帮助类
    /// </summary>
    public static class NetworkHelper
    {
        /// <summary>
        /// 获取本机在外网的ip地址
        /// </summary>
        /// <returns></returns>
        public static string GetOuterNetIP()
        {
            using (var webClient = new WebClient())
            {
                while (true)
                {
                    try
                    {
                        string text = HttpWebRequestHelper.HttpGet("http://pv.sohu.com/cityjson?ie=utf-8").GetText();
                        Match rebool = Regex.Match(text, @"\d{2,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}");
                        return rebool.Value;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("获取本机的外网ip失败，5秒后重试...");
                        Console.WriteLine(ex.StackTrace);
                        Thread.Sleep(5000);
                    }
                }
            }
        }
        /// <summary>  
        /// 获取本机MAC地址  
        /// </summary>  
        /// <returns>本机MAC地址</returns>  
        public static string GetMacAddress()
        {
            try
            {
                string strMac = string.Empty;
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        strMac = mo["MacAddress"].ToString();
                    }
                }
                moc = null;
                mc = null;
                return strMac;
            }
            catch
            {
                return "unknown";
            }
        }

    }
}
