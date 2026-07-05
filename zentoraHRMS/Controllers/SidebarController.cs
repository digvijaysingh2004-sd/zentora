using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using zentoraHRMS.Models;

namespace zentoraHRMS.Controllers
{
    public class SidebarController : Controller
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Zentora"].ConnectionString;

        [ChildActionOnly]
        public ActionResult RenderSidebar()
        {
            var model = new SidebarViewModel();
            var allParents = new List<ParentModuleVM>();
            var allChildren = new List<ChildModuleVM>();
            var allSubChildren = new List<SubChildModuleVM>();

            // IMPORTANT: Grab the actual Role ID of the logged-in user!
            // Adjust "RoleId" to match whatever Session/Identity key you use for login
            int currentRoleId = 0;
            if (Session["RoleId"] != null)
            {
                currentRoleId = Convert.ToInt32(Session["RoleId"]);
            }
            else
            {
                // If they aren't logged in or RoleId is missing, return an empty sidebar
                return PartialView("_Sidebar", model);
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Use the new Role-based Stored Procedure
                using (SqlCommand cmd = new SqlCommand("sp_GetSidebarModulesByRole", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoleId", currentRoleId);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // 1. Read Permitted Parent Modules
                        while (reader.Read())
                        {
                            allParents.Add(new ParentModuleVM
                            {
                                ParentModuleId = Convert.ToInt32(reader["ParentModuleId"]),
                                ModuleName = reader["ModuleName"].ToString(),
                                IconClass = reader["IconClass"] != DBNull.Value ? reader["IconClass"].ToString() : "",
                                RouteUrl = reader["RouteUrl"].ToString(),
                                SortOrder = Convert.ToInt32(reader["SortOrder"])
                            });
                        }

                        // 2. Read Permitted Child Modules
                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                allChildren.Add(new ChildModuleVM
                                {
                                    ChildModuleId = Convert.ToInt32(reader["ChildModuleId"]),
                                    ParentModuleId = Convert.ToInt32(reader["ParentModuleId"]),
                                    ModuleName = reader["ModuleName"].ToString(),
                                    RouteUrl = reader["RouteUrl"].ToString(),
                                    SortOrder = Convert.ToInt32(reader["SortOrder"])
                                });
                            }
                        }

                        // 3. Read Permitted Sub-Child Modules
                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                allSubChildren.Add(new SubChildModuleVM
                                {
                                    SubChildModuleId = Convert.ToInt32(reader["SubChildModuleId"]),
                                    ChildModuleId = Convert.ToInt32(reader["ChildModuleId"]),
                                    ModuleName = reader["ModuleName"].ToString(),
                                    RouteUrl = reader["RouteUrl"].ToString(),
                                    SortOrder = Convert.ToInt32(reader["SortOrder"])
                                });
                            }
                        }
                    }
                }
            }

            // Build the hierarchy in memory
            foreach (var child in allChildren)
            {
                child.SubChildren = allSubChildren.Where(s => s.ChildModuleId == child.ChildModuleId).ToList();
            }

            foreach (var parent in allParents)
            {
                parent.Children = allChildren.Where(c => c.ParentModuleId == parent.ParentModuleId).ToList();
            }

            model.ParentModules = allParents;

            return PartialView("_Sidebar", model);
        }
    }
}