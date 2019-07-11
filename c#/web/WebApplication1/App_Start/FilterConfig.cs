using System.Web;
using System.Web.Mvc;
using WebApplication1.Filter;
using WebApplication1.Filter.Common;

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
            singleUserAttribute.IgnoreRequests.Add("/session/jsonpLogin".ToLower());
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
