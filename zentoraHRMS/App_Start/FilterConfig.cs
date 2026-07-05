using System.Web;
using System.Web.Mvc;
using zentoraHRMS.Filters;

namespace zentoraHRMS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new PermissionActionFilter());
        }
    }
}

