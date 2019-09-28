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
            throw new NotImplementedException();
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
                Url = "menus/system/logEntity.html"
            });
            sysMenu.LeftMenus.Add(new LeftMenu
            {
                Name = "heartbeatEntity",
                Title = "心跳监测",
                SortIndex = 1,
                Url = "menus/system/heartbeatEntity.html"
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