using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Entity.Common
{
    /// <summary>
    /// 后台菜单树实体类
    /// </summary>
    public class LeftMenu
    {
        /// <summary>
        /// 菜单标题
        /// </summary>
        public string title { set; get; }

        /// <summary>
        /// 菜单id
        /// </summary>
        public string id { set; get; }

        /// <summary>
        /// 菜单点击时，打开的页面所在的url
        /// </summary>
        public string url { set; get; }

        /// <summary>
        /// 子菜单
        /// </summary>
        public List<LeftMenu> leftMenus { set; get; }

        public LeftMenu()
        {
            leftMenus = new List<LeftMenu>();
        }
    }
}