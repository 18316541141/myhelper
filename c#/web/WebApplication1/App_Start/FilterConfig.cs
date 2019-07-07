using System.Web;
using System.Web.Mvc;
using WebApplication1.filter;

namespace WebApplication1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LoginAuthorizeAttribute());
            filters.Add(new PermAuthorizeAttribute());
            SingleUserAttribute singleUserAttribute = new SingleUserAttribute();
            singleUserAttribute.IgnoreRequests.Add("/session/verificationCode".ToLower());
            singleUserAttribute.IgnoreRequests.Add("/session/login".ToLower());
            filters.Add(singleUserAttribute);
            CompressAttribute compressAttribute = new CompressAttribute();
            compressAttribute.IgnoreRequests.Add("/index/showImage".ToLower());
            compressAttribute.IgnoreRequests.Add("/session/verificationCode".ToLower());
            compressAttribute.IgnoreRequests.Add("/index/downFile".ToLower());
            filters.Add(compressAttribute);
            filters.Add(new HandleErrorAttribute());
        }
    }
}
