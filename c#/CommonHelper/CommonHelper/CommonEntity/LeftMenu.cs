using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommonHelper.Helper.CommonEntity
{
    /// <summary>
    /// 后台菜单树实体类
    /// </summary>
    public class LeftMenu
    {
        /// <summary>
        /// 菜单序号
        /// </summary>
        [JsonProperty("sortIndex")]
        public int SortIndex { set; get; }

        /// <summary>
        /// 菜单标题
        /// </summary>
        [JsonProperty("title")]
        public string Title { set; get; }

        /// <summary>
        /// 菜单id
        /// </summary>
        [JsonProperty("id")]
        public string Id { set; get; }

        /// <summary>
        /// 菜单点击时，打开的页面所在的url
        /// </summary>
        [JsonProperty("url")]
        public string Url { set; get; }

        /// <summary>
        /// 子菜单
        /// </summary>
        [JsonProperty("leftMenus")]
        public List<LeftMenu> LeftMenus { set; get; }

        public LeftMenu()
        {
            LeftMenus = new List<LeftMenu>();
        }
    }
}