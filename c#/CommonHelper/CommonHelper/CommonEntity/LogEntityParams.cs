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
        public long? Id { set; get; }


        /// <summary>
        /// 日志日期
        /// </summary>
        public DateTime? CreateDate { set; get; }


        /// <summary>
        /// 日志日期
        /// </summary>
        public DateTime? CreateDateStart { set; get; }


        /// <summary>
        /// 日志日期
        /// </summary>
        public DateTime? CreateDateEnd { set; get; }


        /// <summary>
        /// 日志分级
        /// </summary>
        public string Level { set; get; }


        /// <summary>
        /// 日志分级
        /// </summary>
        public string LevelLike { set; get; }


        /// <summary>
        /// 线程号
        /// </summary>
        public string ThreadNo { set; get; }


        /// <summary>
        /// 线程号
        /// </summary>
        public string ThreadNoLike { set; get; }


        /// <summary>
        /// 日志内容
        /// </summary>
        public string Message { set; get; }


        /// <summary>
        /// 日志内容
        /// </summary>
        public string MessageLike { set; get; }


        /// <summary>
        /// 日志发生的命名空间
        /// </summary>
        public string ProjectName { set; get; }


        /// <summary>
        /// 日志发生的命名空间
        /// </summary>
        public string ProjectNameLike { set; get; }


        /// <summary>
        /// 日志发生的类型
        /// </summary>
        public string TypeName { set; get; }


        /// <summary>
        /// 日志发生的类型
        /// </summary>
        public string TypeNameLike { set; get; }


        /// <summary>
        /// 日志发生的方法名称
        /// </summary>
        public string FuncName { set; get; }


        /// <summary>
        /// 日志发生的方法名称
        /// </summary>
        public string FuncNameLike { set; get; }


        /// <summary>
        /// 日志的异常堆栈信息
        /// </summary>
        public string Exception { set; get; }


        /// <summary>
        /// 日志的异常堆栈信息
        /// </summary>
        public string ExceptionLike { set; get; }

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