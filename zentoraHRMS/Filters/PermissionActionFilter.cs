using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using zentoraHRMS.Helpers;

namespace zentoraHRMS.Filters
{
    public class PermissionActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Bypass child actions to avoid breaking page sub-rendering (e.g. RenderSidebar)
            if (filterContext.IsChildAction)
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            // Bypass authentication controller
            string controller = filterContext.RouteData.Values["controller"]?.ToString();
            string action = filterContext.RouteData.Values["action"]?.ToString();

            if (string.Equals(controller, "Auth", StringComparison.OrdinalIgnoreCase))
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            var session = filterContext.HttpContext.Session;
            if (session != null)
            {
                // Bypass Superadmin from all checks
                var roleName = session["RoleName"]?.ToString();
                if (!string.IsNullOrEmpty(roleName) && string.Equals(roleName, "Superadmin", StringComparison.OrdinalIgnoreCase))
                {
                    PopulateCurrentPermissions(filterContext);
                    base.OnActionExecuting(filterContext);
                    return;
                }

                if (session["RoleId"] != null)
                {
                    var roleId = Convert.ToInt32(session["RoleId"]);
                    
                    // Cache permissions in session to avoid DB hit on every request
                    if (session["UserPermissions"] == null)
                    {
                        session["UserPermissions"] = PermissionManager.GetRolePermissions(roleId);
                    }

                    var perms = (Dictionary<string, List<string>>)session["UserPermissions"];
                    var allModuleRoutes = PermissionManager.GetProtectedRoutes();
                    
                    var matchingRoute = GetMatchingModuleRoute(controller, action, allModuleRoutes);
                    if (matchingRoute != null)
                    {
                        var requiredAction = GetRequiredAction(action);
                        bool isAuthorized = false;

                        if (perms.TryGetValue(matchingRoute, out var allowedActions))
                        {
                            if (allowedActions.Contains(requiredAction, StringComparer.OrdinalIgnoreCase))
                            {
                                isAuthorized = true;
                            }
                        }

                        if (!isAuthorized)
                        {
                            if (filterContext.HttpContext.Request.IsAjaxRequest())
                            {
                                filterContext.Result = new JsonResult
                                {
                                    Data = new { success = false, message = "Access Denied: You do not have permission to perform this action." },
                                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                                };
                            }
                            else
                            {
                                session["ErrorMessage"] = "Access Denied: You do not have permission to access this page.";
                                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index" }));
                            }
                            return;
                        }
                    }
                }
            }

            PopulateCurrentPermissions(filterContext);
            base.OnActionExecuting(filterContext);
        }

        private void PopulateCurrentPermissions(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;
            if (session != null && session["RoleId"] != null)
            {
                var roleId = Convert.ToInt32(session["RoleId"]);
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
        }

        private string GetMatchingModuleRoute(string controller, string action, IEnumerable<string> allModuleRoutes)
        {
            string exactPath = $"/{controller}/{action}";
            var match = allModuleRoutes.FirstOrDefault(r => string.Equals(r, exactPath, StringComparison.OrdinalIgnoreCase));
            if (match != null) return match;

            var controllerRoutes = allModuleRoutes.Where(r => r.StartsWith($"/{controller}/", StringComparison.OrdinalIgnoreCase)).ToList();
            if (controllerRoutes.Count == 1)
            {
                return controllerRoutes[0];
            }
            else if (controllerRoutes.Count > 1)
            {
                foreach (var route in controllerRoutes)
                {
                    string lastSegment = route.Split('/').Last();
                    string singular = lastSegment.EndsWith("s", StringComparison.OrdinalIgnoreCase) && lastSegment.Length > 1
                        ? lastSegment.Substring(0, lastSegment.Length - 1)
                        : lastSegment;

                    if (action.Contains(lastSegment, StringComparison.OrdinalIgnoreCase) || 
                        action.Contains(singular, StringComparison.OrdinalIgnoreCase))
                    {
                        return route;
                    }
                }
                return controllerRoutes.FirstOrDefault(r => action.StartsWith(r.Split('/').Last(), StringComparison.OrdinalIgnoreCase));
            }
            return null;
        }

        private string GetRequiredAction(string action)
        {
            string actLower = action.ToLower();
            if (actLower.Contains("save") || actLower.Contains("create") || actLower.Contains("add") || actLower.Contains("insert"))
            {
                return "add";
            }
            if (actLower.Contains("update") || actLower.Contains("edit") || actLower.Contains("modify"))
            {
                return "edit";
            }
            if (actLower.Contains("delete") || actLower.Contains("remove") || actLower.Contains("destroy"))
            {
                return "delete";
            }
            return "view";
        }
    }
}
