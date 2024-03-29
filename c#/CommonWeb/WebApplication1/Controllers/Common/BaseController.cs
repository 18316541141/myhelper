﻿using CommonWeb.Entity.Common;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using WebApplication1.Entity;
using WebApplication1.Entity.Common;

namespace CommonWeb.Controllers.Common
{
    /// <summary>
    /// 基础控制器
    /// </summary>
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// 日志
        /// </summary>
        public ILog log { set; get; }

        /// <summary>
        /// 跨平台的文件路径分隔符
        /// </summary>
        public readonly static char s;

        static BaseController()
        {
            s = Path.DirectorySeparatorChar;
        }

        public BaseController(){}

        /// <summary>
        /// 返回JsonResult
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="format">json中dateTime类型的格式</param>
        /// <returns>Json</returns>
        protected JsonResult MyJson(object data, string format= "yyyy-MM-dd HH:mm:ss")
        {
            return new MyJsonResult
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                FormateStr = format
            };
        }

        /// <summary>
        /// 返回JsonResult
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="callback">回调函数，当客户端发送的是jsonp请求时，需要用到回调函数</param>
        /// <param name="format">json中dateTime类型的格式</param>
        /// <returns>Json</returns>
        protected JsonResult MyJson(string callback, object data, string format = "yyyy-MM-dd HH:mm:ss")
        {
            return new MyJsonResult
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                FormateStr = format,
                Callback= callback
            };
        }
    }
}