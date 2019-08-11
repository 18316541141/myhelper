using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Entity.Common
{
    /// <summary>
    /// 地区树
    /// </summary>
    public class AreaTree
    {
        /// <summary>
        /// 地区名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { set; get; }

        /// <summary>
        /// 地区值
        /// </summary>
        [JsonProperty("value")]
        public string Value { set; get; }

        /// <summary>
        /// 上级地区值
        /// </summary>
        [JsonProperty("parentValue")]
        public string ParentValue { set; get; }
    }
}