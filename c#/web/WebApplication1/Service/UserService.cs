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
            throw new NotImplementedException();
        }

        public string LoadUsernameBySignKey(string signKey)
        {
            throw new NotImplementedException();
        }
    }
}