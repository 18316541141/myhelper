using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Entity;

namespace WebApplication1.Controllers
{
    /// <summary>
    /// 基础控制器
    /// </summary>
    public class BaseController : Controller
    {

        /// <summary>
        /// 返回JsonResult.24     /// </summary>
        /// <param name="data">数据</param>
        /// <param name="behavior">行为</param>
        /// <param name="format">json中dateTime类型的格式</param>
        /// <returns>Json</returns>
        protected JsonResult MyJson(object data, JsonRequestBehavior behavior, string format= "yyyy-MM-dd HH:mm:ss")
        {
            return new MyJsonResult
            {
                Data = data,
                JsonRequestBehavior = behavior,
                FormateStr = format
            };
        }

        /// <summary>
        /// 返回JsonResult42     /// </summary>
        /// <param name="data">数据</param>
        /// <param name="format">数据格式</param>
        /// <returns>Json</returns>
        protected JsonResult MyJson(object data, string format= "yyyy-MM-dd HH:mm:ss")
        {
            return new MyJsonResult
            {
                Data = data,
                FormateStr = format
            };
        }
    }
}