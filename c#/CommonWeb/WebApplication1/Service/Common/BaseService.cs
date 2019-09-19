using CommonHelper.Helper.CommonEntity;
using CommonWeb.Filter.Common;
using log4net;
using Newtonsoft.Json;
using Snowflake.Net;
using System;
using System.Collections.Generic;
using System.IO;
namespace CommonWeb.Service.Common
{
    /// <summary>
    /// 基础业务类
    /// </summary>
    public abstract class BaseService
    {
        /// <summary>
        /// 日志输出类
        /// </summary>
        public ILog Log { set; get; }

        /// <summary>
        /// 分布式雪花id生成器
        /// </summary>
        public IdWorker IdWorker { set; get; }

        /// <summary>
        /// 跨平台的斜杠
        /// </summary>
        protected static char s;

        static BaseService()
        {
            s = Path.DirectorySeparatorChar;
        }

        /// <summary>
        /// 使用分布式雪花算法生成唯一id
        /// </summary>
        /// <returns></returns>
        public long NextId()
        {
            return IdWorker.NextId();
        }

        /// <summary>
        /// 直接返回json结果，终止后面的代码处理
        /// </summary>
        /// <param name="msg">异常信息</param>
        /// <param name="modularEnum">需指明这是由哪一个模块抛出的异常</param>
        /// <param name="code">错误码</param>
        protected void Ret(string msg,short code = -1)
        {
            throw new Exception(MyErrorAttribute.ErrorPrefix + JsonConvert.SerializeObject(new Result
            {
                msg = msg,
                code = code,
            }));
        }
    }
}