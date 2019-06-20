using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Entity;

namespace WebApplication1.Service
{
    /// <summary>
    /// 绝大部分系统都用到的业务逻辑类
    /// </summary>
    public class SystemService
    {
        /// <summary>
        /// 加载左侧菜单
        /// </summary>
        /// <returns></returns>
        public List<LeftMenu> LoadLeftMenus()
        {
            List<LeftMenu> leftMenuList = new List<LeftMenu>();
            LeftMenu leftMenu1 = new LeftMenu
            {
                id = "m1",
                title = "测试1"
            };
            List<LeftMenu> leftMenuList11 = new List<LeftMenu>();
            LeftMenu leftMenu111 = new LeftMenu
            {
                id="m11",
                title= "测试1-1",
                url= "menus/testMenus1/test.html"
            };
            leftMenuList11.Add(leftMenu111);

            LeftMenu leftMenu112 = new LeftMenu
            {
                id= "m12",
                title= "测试1-2",
                url= "menus/testMenus2/test1.html"
            };
            leftMenuList11.Add(leftMenu112);
            leftMenu1.leftMenus=leftMenuList11;
            leftMenuList.Add(leftMenu1);
            LeftMenu leftMenu2 = new LeftMenu
            {
                id="m2",
                title= "测试2"
            };

            leftMenuList.Add(leftMenu2);
            LeftMenu leftMenu3 = new LeftMenu
            {
                id= "m3",
                title= "测试3"
            };

            leftMenuList.Add(leftMenu3);
            return leftMenuList;
        }
    }
}