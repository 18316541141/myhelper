using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonHelper.CommonEntity
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class LogEntityParams
    {


        /// <summary>
        /// 主键id，由分布式雪花id生成
        /// </summary>
        public long? id { set; get; }


        /// <summary>
        /// 日志日期
        /// </summary>
        public DateTime? createDate { set; get; }


        /// <summary>
        /// 日志日期
        /// </summary>
        public DateTime? createDateStart { set; get; }


        /// <summary>
        /// 日志日期
        /// </summary>
        public DateTime? createDateEnd { set; get; }


        /// <summary>
        /// 日志分级
        /// </summary>
        public string level { set; get; }


        /// <summary>
        /// 日志分级
        /// </summary>
        public string levelLike { set; get; }


        /// <summary>
        /// 线程号
        /// </summary>
        public string threadNo { set; get; }


        /// <summary>
        /// 线程号
        /// </summary>
        public string threadNoLike { set; get; }


        /// <summary>
        /// 日志内容
        /// </summary>
        public string message { set; get; }


        /// <summary>
        /// 日志内容
        /// </summary>
        public string messageLike { set; get; }


        /// <summary>
        /// 日志发生的命名空间
        /// </summary>
        public string projectName { set; get; }


        /// <summary>
        /// 日志发生的命名空间
        /// </summary>
        public string projectNameLike { set; get; }


        /// <summary>
        /// 日志发生的类型
        /// </summary>
        public string typeName { set; get; }


        /// <summary>
        /// 日志发生的类型
        /// </summary>
        public string typeNameLike { set; get; }


        /// <summary>
        /// 日志发生的方法名称
        /// </summary>
        public string funcName { set; get; }


        /// <summary>
        /// 日志发生的方法名称
        /// </summary>
        public string funcNameLike { set; get; }


        /// <summary>
        /// 日志的异常堆栈信息
        /// </summary>
        public string exception { set; get; }


        /// <summary>
        /// 日志的异常堆栈信息
        /// </summary>
        public string exceptionLike { set; get; }

        /// <summary>
        /// 操作的用户名
        /// </summary>
        public string username { set; get; }

        /// <summary>
        /// 操作的用户名模糊查询条件
        /// </summary>
        public string usernameLike { set; get; }

        /// <summary>
        /// 升序排序
        /// </summary>
        public LogEntityOrderBy orderByAsc { set; get; }

        /// <summary>
        /// 降序排序
        /// </summary>
        public LogEntityOrderBy orderByDesc { set; get; }
    }
}