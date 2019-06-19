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
            filters.Add(new HandleErrorAttribute());
        }
    }
}
