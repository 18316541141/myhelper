using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.CommonEntity
{
    /// <summary>
    /// 日志实体类
    /// </summary>
    public class LogEntity
    {
        /// <summary>
        /// 日志的主键id
        /// </summary>
        public virtual long? Id { set; get; }

        /// <summary>
        /// 日志日期
        /// </summary>
        public virtual DateTime CreateDate { set; get; }

        /// <summary>
        /// 日志等级
        /// </summary>
        public virtual string Level { set; get; }

        /// <summary>
        /// 线程号
        /// </summary>
        public virtual string ThreadNo { set; get; }

        /// <summary>
        /// 日志内容
        /// </summary>
        public virtual string Message { set; get; }

        /// <summary>
        /// 异常发生的命名空间
        /// </summary>
        public virtual string Namespace { set; get; }

        /// <summary>
        /// 异常发生的类名称
        /// </summary>
        public virtual string TypeName { set; get; }

        /// <summary>
        /// 异常发生的方法名称
        /// </summary>
        public virtual string MethodName { set; get; }

        /// <summary>
        /// 用户名
        /// </summary>
        public virtual string Username { set; get; }
    }
}
