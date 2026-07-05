using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using zentoraHRMS.Helpers;

namespace zentoraHRMS.Filters
{
    public class PermissionActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.Equals("Auth", StringComparison.OrdinalIgnoreCase))
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            var session = filterContext.HttpContext.Session;
            if (session != null && session["RoleId"] != null)
            {
                var roleId = Convert.ToInt32(session["RoleId"]);
                
                // Cache permissions in session to avoid DB hit on every request
                if (session["UserPermissions"] == null)
                {
                    session["UserPermissions"] = PermissionManager.GetRolePermissions(roleId);
                }

                var perms = (Dictionary<string, List<string>>)session["UserPermissions"];
                var currentPath = filterContext.HttpContext.Request.Path;
                
                List<string> currentPerms = new List<string>();
                
                foreach (var kvp in perms)
                {
                    if (currentPath.StartsWith(kvp.Key, StringComparison.OrdinalIgnoreCase) || 
                        kvp.Key.StartsWith(currentPath, StringComparison.OrdinalIgnoreCase))
                    {
                        currentPerms.AddRange(kvp.Value);
                    }
                }
                
                filterContext.Controller.ViewBag.CurrentPermissions = currentPerms.Distinct().ToList();
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
