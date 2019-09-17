using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Intf
{
    /// <summary>
    /// 签名拦截器专用的接口类
    /// </summary>
    public interface ISignFilterService
    {
        /// <summary>
        /// 根据签名的signKey获取签名的signSecret
        /// </summary>
        /// <param name="signKey"></param>
        /// <returns></returns>
        string FindSecretByKey(string signKey);
    }
}