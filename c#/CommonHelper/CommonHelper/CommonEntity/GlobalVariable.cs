using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WebApplication1.Entity
{
    /// <summary>
    /// 全局变量表
    /// </summary>
    public partial class GlobalVariable //: IEntity
    {
        public GlobalVariable() { }

        /// <summary>
        /// 主键id
        /// </summary>
        [JsonProperty("id")]
        public virtual long? Id { set; get; }

        /// <summary>
        /// 变量排序号
        /// </summary>
        [JsonProperty("varSortIndex")]
        public virtual int? VarSortIndex { set; get; }

        /// <summary>
        /// 变量名称
        /// </summary>
        [JsonProperty("varName")]
        public virtual string VarName { set; get; }

        /// <summary>
        /// 变量值
        /// </summary>
        [JsonProperty("varValue")]
        public virtual string VarValue { set; get; }
        /*
            [NotMapped]
            public long? Key
            {
                set { Id = value; }
                get { return Id; }
            }

            public string TableName()
            {
                return "global_variable";
            }
        */
    }
}