using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Service
{
    /// <summary>
    /// 实时消息初始化，因为实时消息是通过版本号机制减少数据库的查询，
    /// 但首次访问时，浏览器并不会存有任何版本号，这种情况下，默认是认为浏览器的
    /// 信息不是最新的，但实际上需要先从数据库查询决定是否获取最新消息
    /// </summary>
    public class RealTimeInitService
    {
        /// <summary>
        /// 实时信息的初始化器，返回true：不需要刷新，返回false：需要刷新
        /// </summary>
        /// <param name="poolName"></param>
        /// <returns></returns>
        public bool Init(string poolName,string username)
        {
            if (poolName == "newsAlarm")
            {
                return InitNewsAlarm(username);
            }
            return false;
        } 

        /// <summary>
        /// 最新消息框的初始化器
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        bool InitNewsAlarm(string username)
        {
            return false;
        }
    }
}