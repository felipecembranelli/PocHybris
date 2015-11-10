using System.Web;
using System.Web.Mvc;

namespace PocHybris.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute
            {
                View = "Error"
            }, 1);
        }
    }
}
