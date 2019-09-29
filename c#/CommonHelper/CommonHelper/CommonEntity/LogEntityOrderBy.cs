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
        public bool id { set; get; }

        /// <summary>
        /// 日志日期
        /// </summary>
        public bool createDate { set; get; }

        /// <summary>
        /// 日志分级
        /// </summary>
        public bool level { set; get; }

        /// <summary>
        /// 线程号
        /// </summary>
        public bool threadNo { set; get; }

        /// <summary>
        /// 日志内容
        /// </summary>
        public bool message { set; get; }

        /// <summary>
        /// 日志发生的命名空间
        /// </summary>
        public bool projectName { set; get; }

        /// <summary>
        /// 日志发生的类型
        /// </summary>
        public bool typeName { set; get; }

        /// <summary>
        /// 日志发生的方法名称
        /// </summary>
        public bool funcName { set; get; }

        /// <summary>
        /// 日志的异常堆栈信息
        /// </summary>
        public bool exception { set; get; }

        /// <summary>
        /// 操作的用户名
        /// </summary>
        public bool username { set; get; }
    }
}