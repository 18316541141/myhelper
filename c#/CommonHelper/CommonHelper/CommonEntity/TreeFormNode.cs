using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonHelper.Helper.CommonEntity
{
    /// <summary>
    /// 树表单节点
    /// </summary>
    public sealed class TreeFormNode
    {
        /// <summary>
        /// 节点id
        /// </summary>
        [JsonProperty("id")]
        public string Id { set; get; }

        /// <summary>
        /// 节点标题
        /// </summary>
        [JsonProperty("name")]
        public string Name { set; get; }

        /// <summary>
        /// 树节点的子节点
        /// </summary>
        [JsonProperty("children")]
        public List<TreeFormNode> Children { set; get; }

        public TreeFormNode()
        {
            Children = new List<TreeFormNode>();
        }
    }
}