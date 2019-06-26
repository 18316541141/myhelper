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
            CompressAttribute compressAttribute = new CompressAttribute();
            compressAttribute.IgnoreRequests.Add("/index/showImage".ToLower());
            compressAttribute.IgnoreRequests.Add("/index/verificationCode".ToLower());
            compressAttribute.IgnoreRequests.Add("/index/downFile".ToLower());
            filters.Add(compressAttribute);
            filters.Add(new HandleErrorAttribute());
        }
    }
}
