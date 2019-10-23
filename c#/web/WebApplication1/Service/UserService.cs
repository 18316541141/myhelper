using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonHelper.Helper.CommonEntity;
using CommonWeb.Intf;
namespace WebApplication1.Service
{
    public class UserService : IUserService
    {
        public bool CheckLogin(string username, string password)
        {
            return username == "superAdmin";
        }

        public string FindSecretByKey(string signKey)
        {
            throw new NotImplementedException();
        }

        public List<LeftMenu> LoadLeftMenus(string username)
        {
            List<LeftMenu> leftMenuList = new List<LeftMenu>();
            LeftMenu sysMenu = new LeftMenu
            {
                Name = "system",
                Title = "系统管理",
                SortIndex = 0,
            };
            sysMenu.LeftMenus.Add(new LeftMenu
            {
                Name = "logEntity",
                Title = "系统日志",
                SortIndex = 0,
                //Url = "menus/system/logEntity.html"
                Url= "/static/common/system/logEntity.html"
            });
            sysMenu.LeftMenus.Add(new LeftMenu
            {
                Name = "heartbeatEntity",
                Title = "心跳监测",
                SortIndex = 1,
                //Url = "menus/system/heartbeatEntity.html"
                Url = "/static/common/system/heartbeatEntity.html"
            });
            sysMenu.LeftMenus.Add(new LeftMenu
            {
                Name = "globalVariable",
                Title = "全局变量",
                SortIndex = 2,
                //Url = "menus/system/heartbeatEntity.html"
                Url = "/static/common/system/globalVariable.html"
            });
            sysMenu.LeftMenus.Add(new LeftMenu
            {
                Name = "test",
                Title = "测试节点",
                SortIndex = 3,
                //Url = "menus/system/heartbeatEntity.html"
                Url = "/static/menus/test.html"
            });
            leftMenuList.Add(sysMenu);
            return leftMenuList;
        }

        public string LoadUsernameBySignKey(string signKey)
        {
            throw new NotImplementedException();
        }
    }
}