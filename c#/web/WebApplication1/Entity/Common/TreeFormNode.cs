using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Entity.Common
{
    /// <summary>
    /// 树表单节点
    /// </summary>
    public class TreeFormNode
    {
        /// <summary>
        /// 节点id
        /// </summary>
        public string id { set; get; }

        /// <summary>
        /// 节点标题
        /// </summary>
        public string name { set; get; }

        /// <summary>
        /// 树节点的子节点
        /// </summary>
        public List<TreeFormNode> children { set; get; }

        public TreeFormNode()
        {
            children = new List<TreeFormNode>();
        }
    }
}