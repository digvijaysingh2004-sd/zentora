using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

namespace zentoraHRMS.Helpers
{
    public class PermissionManager
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["Zentora"].ConnectionString;

        public static Dictionary<string, List<string>> GetRolePermissions(int roleId)
        {
            var permissions = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        COALESCE(s.RouteUrl, c.RouteUrl, p.RouteUrl) AS RouteUrl,
                        rp.ActionId
                    FROM RolePermissions rp
                    LEFT JOIN ParentModules p ON rp.ParentModuleId = p.ParentModuleId
                    LEFT JOIN ChildModules c ON rp.ChildModuleId = c.ChildModuleId
                    LEFT JOIN SubChildModules s ON rp.SubChildModuleId = s.SubChildModuleId
                    WHERE rp.RoleId = @RoleId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RoleId", roleId);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string routeUrl = reader["RouteUrl"] != DBNull.Value ? reader["RouteUrl"].ToString() : "";
                            int actionId = Convert.ToInt32(reader["ActionId"]);
                            string actionStr = MapActionId(actionId);

                            if (!string.IsNullOrEmpty(routeUrl) && routeUrl != "#")
                            {
                                if (!permissions.ContainsKey(routeUrl))
                                {
                                    permissions[routeUrl] = new List<string>();
                                }
                                if (!permissions[routeUrl].Contains(actionStr))
                                {
                                    permissions[routeUrl].Add(actionStr);
                                }
                            }
                        }
                    }
                }
            }

            return permissions;
        }

        private static string MapActionId(int id)
        {
            switch (id)
            {
                case 1: return "view";
                case 2: return "add";
                case 3: return "edit";
                case 4: return "delete";
                default: return "view";
            }
        }
    }
}
