using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonHelper.CommonEntity
{
    public sealed partial class LogEntityOrderBy
    {
        /// <summary>
        /// 主键id，由分布式雪花id生成
        /// </summary>
        public bool Id { set; get; }

        /// <summary>
        /// 日志日期
        /// </summary>
        public bool CreateDate { set; get; }

        /// <summary>
        /// 日志分级
        /// </summary>
        public bool Level { set; get; }

        /// <summary>
        /// 线程号
        /// </summary>
        public bool ThreadNo { set; get; }

        /// <summary>
        /// 日志内容
        /// </summary>
        public bool Message { set; get; }

        /// <summary>
        /// 日志发生的命名空间
        /// </summary>
        public bool ProjectName { set; get; }

        /// <summary>
        /// 日志发生的类型
        /// </summary>
        public bool TypeName { set; get; }

        /// <summary>
        /// 日志发生的方法名称
        /// </summary>
        public bool FuncName { set; get; }

        /// <summary>
        /// 日志的异常堆栈信息
        /// </summary>
        public bool Exception { set; get; }
    }
}