using CommonHelper.Helper;
using Newtonsoft.Json;
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
        [JsonProperty("id")]
        [ExcelCol(ColName = "主键id" , ColIndex = 0)]
        public virtual long? Id { set; get; }

        /// <summary>
        /// 日志日期
        /// </summary>
        [JsonProperty("createDate")]
        [ExcelCol(ColName = "创建日期", ColIndex = 1)]
        public virtual DateTime? CreateDate { set; get; }

        /// <summary>
        /// 日志等级
        /// </summary>
        [JsonProperty("level")]
        [ExcelCol(ColName = "日志分级", ColIndex = 2)]
        public virtual string Level { set; get; }

        /// <summary>
        /// 线程号
        /// </summary>
        [JsonProperty("threadNo")]
        [ExcelCol(ColName = "线程号", ColIndex = 3)]
        public virtual string ThreadNo { set; get; }

        /// <summary>
        /// 日志内容
        /// </summary>
        [JsonProperty("message")]
        [ExcelCol(ColName = "日志内容", ColIndex = 4)]
        public virtual string Message { set; get; }

        /// <summary>
        /// 异常发生的项目名
        /// </summary>
        [JsonProperty("projectName")]
        [ExcelCol(ColName = "项目名称", ColIndex = 5)]
        public virtual string ProjectName { set; get; }

        /// <summary>
        /// 异常发生的类名称
        /// </summary>
        [JsonProperty("typeName")]
        [ExcelCol(ColName = "异常发生类", ColIndex = 6)]
        public virtual string TypeName { set; get; }

        /// <summary>
        /// 异常发生的方法名称
        /// </summary>
        [JsonProperty("funcName")]
        [ExcelCol(ColName = "异常发生方法", ColIndex = 7)]
        public virtual string FuncName { set; get; }

        /// <summary>
        /// 异常的堆栈信息
        /// </summary>
        [JsonProperty("exception")]
        [ExcelCol(ColName = "异常堆栈信息", ColIndex = 8)]
        public virtual string Exception { set; get; }

        /// <summary>
        /// 操作的用户名
        /// </summary>
        [JsonProperty("username")]
        [ExcelCol(ColName = "用户名", ColIndex = 9)]
        public virtual string Username { set; get; }
    }
}
