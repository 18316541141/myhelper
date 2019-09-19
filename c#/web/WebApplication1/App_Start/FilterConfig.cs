using CommonWeb.Filter.Common;
using log4net.Repository.Hierarchy;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            #region 这两个拦截器是常规的web应用登陆或鉴权使用的拦截器，当在微信平台开发时，需要注释掉。
            filters.Add(new LoginAuthorizeAttribute());
            filters.Add(new PermAuthorizeAttribute());
            SingleUserAttribute singleUserAttribute = new SingleUserAttribute();
            singleUserAttribute.IgnoreRequests.Add("/session/verificationCode".ToLower());
            singleUserAttribute.IgnoreRequests.Add("/session/login".ToLower());
            singleUserAttribute.IgnoreRequests.Add("/session/jsonpLogin".ToLower());
            singleUserAttribute.IgnoreRequests.Add("/Index/AnonymousRealTime".ToLower());
            singleUserAttribute.IgnoreRequests.Add("/Index/AnonymousShowImage".ToLower());
            filters.Add(singleUserAttribute);
            #endregion
            filters.Add(new MyErrorAttribute());
            //filters.Add(new HandleErrorAttribute());
        }
    }
}
