using CommonHelper.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Entity;

namespace CommonCrawler.Intf
{
    /// <summary>
    /// 爬虫接口类，提供常用的定制化功能
    /// </summary>
    public interface ICrawlerService
    {
        /// <summary>
        /// 获取自定义的心跳信息
        /// </summary>
        /// <returns></returns>
        HeartbeatEntity GetHeartbeatEntityInfo();

        /// <summary>
        /// 发送心跳报文时异常处理方法
        /// </summary>
        /// <param name="ex">异常对象</param>
        void HeartbeatException(Exception ex);
    }
}
