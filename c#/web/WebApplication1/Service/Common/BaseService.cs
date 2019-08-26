﻿using CommonHelper.Helper.CommonEntity;
using log4net;
using Newtonsoft.Json;
using Snowflake.Net;
using System;
using System.Collections.Generic;
using System.IO;
using WebApplication1.Filter.Common;

namespace WebApplication1.Service.Common
{
    /// <summary>
    /// 基础业务类
    /// </summary>
    public abstract class BaseService
    {
        /// <summary>
        /// 日志输出类
        /// </summary>
        public ILog log { set; get; }

        /// <summary>
        /// 分布式雪花id生成器
        /// </summary>
        static IdWorker idWorker;

        /// <summary>
        /// 跨平台的斜杠
        /// </summary>
        protected static char s;

        static BaseService()
        {
            idWorker = new IdWorker(1, 1);
            s = Path.DirectorySeparatorChar;
        }

        /// <summary>
        /// 使用分布式雪花算法生成唯一id
        /// </summary>
        /// <returns></returns>
        public long NextId()
        {
            return idWorker.NextId();
        }

        /// <summary>
        /// 手动抛出异常信息，并交由异常处理器统一处理
        /// </summary>
        /// <param name="msg">异常信息</param>
        /// <param name="code">错误码</param>
        protected void Ex(string msg, short code = -1)
        {
            throw new Exception(MyErrorAttribute.ErrorPrefix + JsonConvert.SerializeObject(new Result
            {
                msg = msg,
                code = code,
            }));
        }
    }
}