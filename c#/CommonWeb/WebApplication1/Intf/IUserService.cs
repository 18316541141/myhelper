using CommonHelper.Helper.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonWeb.Intf
{
    /// <summary>
    /// 用户操作的业务逻辑接口类，
    /// 所有用户操作的业务逻辑必须实现这些方法。
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 根据签名的signKey获取签名的signSecret
        /// </summary>
        /// <param name="signKey"></param>
        /// <returns></returns>
        string FindSecretByKey(string signKey);

        /// <summary>
        /// 根据signKey加载用户名
        /// </summary>
        /// <param name="signKey"></param>
        /// <returns></returns>
        string LoadUsernameBySignKey(string signKey);

        /// <summary>
        /// 根据用户名，加载用户的树节点
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        List<LeftMenu> LoadLeftMenus(string username);

        /// <summary>
        /// 登陆用户名、密码校验
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        bool CheckLogin(string username,string password);
    }
}