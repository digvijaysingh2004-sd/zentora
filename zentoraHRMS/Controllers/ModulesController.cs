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
    public class ModulesController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Zentora"].ConnectionString;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            base.OnActionExecuting(filterContext);
        }

        #region Parent Modules
        public ActionResult Parent()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            List<ParentModuleVM> list = new List<ParentModuleVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ParentModuleId, ModuleName, IconClass, RouteUrl, SortOrder FROM ParentModules ORDER BY SortOrder";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new ParentModuleVM
                            {
                                ParentModuleId = Convert.ToInt32(reader["ParentModuleId"]),
                                ModuleName = reader["ModuleName"].ToString(),
                                IconClass = reader["IconClass"] != DBNull.Value ? reader["IconClass"].ToString() : "",
                                RouteUrl = reader["RouteUrl"].ToString(),
                                SortOrder = Convert.ToInt32(reader["SortOrder"])
                            });
                        }
                    }
                }
            }
            return View(list);
        }

        [HttpPost]
        public JsonResult SaveParentModule(ParentModuleVM model)
        {
            try
            {
                if (!IsSuperAdmin())
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string maxQuery = "SELECT ISNULL(MAX(SortOrder), 0) + 1 FROM ParentModules";
                        using (SqlCommand maxCmd = new SqlCommand(maxQuery, con))
                        {
                            con.Open();
                            model.SortOrder = Convert.ToInt32(maxCmd.ExecuteScalar());
                        }
                    }
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO ParentModules (ModuleName, IconClass, RouteUrl, SortOrder, IsActive) VALUES (@ModuleName, @IconClass, @RouteUrl, @SortOrder, 1)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ModuleName", model.ModuleName ?? "");
                        cmd.Parameters.AddWithValue("@IconClass", model.IconClass ?? "");
                        cmd.Parameters.AddWithValue("@RouteUrl", model.RouteUrl ?? "");
                        cmd.Parameters.AddWithValue("@SortOrder", model.SortOrder);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Parent Module saved successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult GetParentModuleById(int id)
        {
            ParentModuleVM model = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ParentModuleId, ModuleName, IconClass, RouteUrl, SortOrder FROM ParentModules WHERE ParentModuleId = @ParentModuleId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ParentModuleId", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model = new ParentModuleVM
                            {
                                ParentModuleId = Convert.ToInt32(reader["ParentModuleId"]),
                                ModuleName = reader["ModuleName"].ToString(),
                                IconClass = reader["IconClass"] != DBNull.Value ? reader["IconClass"].ToString() : "",
                                RouteUrl = reader["RouteUrl"].ToString(),
                                SortOrder = Convert.ToInt32(reader["SortOrder"])
                            };
                        }
                    }
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateParentModule(ParentModuleVM model)
        {
            try
            {
                if (!IsSuperAdmin())
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string origQuery = "SELECT SortOrder FROM ParentModules WHERE ParentModuleId = @ParentModuleId";
                        using (SqlCommand origCmd = new SqlCommand(origQuery, con))
                        {
                            origCmd.Parameters.AddWithValue("@ParentModuleId", model.ParentModuleId);
                            con.Open();
                            var val = origCmd.ExecuteScalar();
                            if (val != null && val != DBNull.Value)
                            {
                                model.SortOrder = Convert.ToInt32(val);
                            }
                        }
                    }
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE ParentModules SET ModuleName = @ModuleName, IconClass = @IconClass, RouteUrl = @RouteUrl, SortOrder = @SortOrder WHERE ParentModuleId = @ParentModuleId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ParentModuleId", model.ParentModuleId);
                        cmd.Parameters.AddWithValue("@ModuleName", model.ModuleName ?? "");
                        cmd.Parameters.AddWithValue("@IconClass", model.IconClass ?? "");
                        cmd.Parameters.AddWithValue("@RouteUrl", model.RouteUrl ?? "");
                        cmd.Parameters.AddWithValue("@SortOrder", model.SortOrder);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteParentModule(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM ParentModules WHERE ParentModuleId = @ParentModuleId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ParentModuleId", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region Child Modules
        public ActionResult Child()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            List<ChildModuleVM> list = new List<ChildModuleVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ChildModuleId, ParentModuleId, ModuleName, RouteUrl, SortOrder FROM ChildModules ORDER BY SortOrder";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new ChildModuleVM
                            {
                                ChildModuleId = Convert.ToInt32(reader["ChildModuleId"]),
                                ParentModuleId = Convert.ToInt32(reader["ParentModuleId"]),
                                ModuleName = reader["ModuleName"].ToString(),
                                RouteUrl = reader["RouteUrl"].ToString(),
                                SortOrder = Convert.ToInt32(reader["SortOrder"])
                            });
                        }
                    }
                }
            }
            return View(list);
        }

        [HttpPost]
        public JsonResult SaveChildModule(ChildModuleVM model)
        {
            try
            {
                if (!IsSuperAdmin())
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string maxQuery = "SELECT ISNULL(MAX(SortOrder), 0) + 1 FROM ChildModules";
                        using (SqlCommand maxCmd = new SqlCommand(maxQuery, con))
                        {
                            con.Open();
                            model.SortOrder = Convert.ToInt32(maxCmd.ExecuteScalar());
                        }
                    }
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO ChildModules (ParentModuleId, ModuleName, RouteUrl, SortOrder, IsActive) VALUES (@ParentModuleId, @ModuleName, @RouteUrl, @SortOrder, 1)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ParentModuleId", model.ParentModuleId);
                        cmd.Parameters.AddWithValue("@ModuleName", model.ModuleName ?? "");
                        cmd.Parameters.AddWithValue("@RouteUrl", model.RouteUrl ?? "");
                        cmd.Parameters.AddWithValue("@SortOrder", model.SortOrder);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Child Module saved successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult GetChildModuleById(int id)
        {
            ChildModuleVM model = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ChildModuleId, ParentModuleId, ModuleName, RouteUrl, SortOrder FROM ChildModules WHERE ChildModuleId = @ChildModuleId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ChildModuleId", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model = new ChildModuleVM
                            {
                                ChildModuleId = Convert.ToInt32(reader["ChildModuleId"]),
                                ParentModuleId = Convert.ToInt32(reader["ParentModuleId"]),
                                ModuleName = reader["ModuleName"].ToString(),
                                RouteUrl = reader["RouteUrl"].ToString(),
                                SortOrder = Convert.ToInt32(reader["SortOrder"])
                            };
                        }
                    }
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateChildModule(ChildModuleVM model)
        {
            try
            {
                if (!IsSuperAdmin())
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string origQuery = "SELECT SortOrder FROM ChildModules WHERE ChildModuleId = @ChildModuleId";
                        using (SqlCommand origCmd = new SqlCommand(origQuery, con))
                        {
                            origCmd.Parameters.AddWithValue("@ChildModuleId", model.ChildModuleId);
                            con.Open();
                            var val = origCmd.ExecuteScalar();
                            if (val != null && val != DBNull.Value)
                            {
                                model.SortOrder = Convert.ToInt32(val);
                            }
                        }
                    }
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE ChildModules SET ParentModuleId = @ParentModuleId, ModuleName = @ModuleName, RouteUrl = @RouteUrl, SortOrder = @SortOrder WHERE ChildModuleId = @ChildModuleId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ChildModuleId", model.ChildModuleId);
                        cmd.Parameters.AddWithValue("@ParentModuleId", model.ParentModuleId);
                        cmd.Parameters.AddWithValue("@ModuleName", model.ModuleName ?? "");
                        cmd.Parameters.AddWithValue("@RouteUrl", model.RouteUrl ?? "");
                        cmd.Parameters.AddWithValue("@SortOrder", model.SortOrder);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteChildModule(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM ChildModules WHERE ChildModuleId = @ChildModuleId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ChildModuleId", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region SubChild Modules
        public ActionResult SubChild()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            List<SubChildModuleVM> list = new List<SubChildModuleVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT SubChildModuleId, ChildModuleId, ModuleName, RouteUrl, SortOrder FROM SubChildModules ORDER BY SortOrder";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new SubChildModuleVM
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
            return View(list);
        }

        [HttpPost]
        public JsonResult SaveSubChildModule(SubChildModuleVM model)
        {
            try
            {
                if (!IsSuperAdmin())
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string maxQuery = "SELECT ISNULL(MAX(SortOrder), 0) + 1 FROM SubChildModules";
                        using (SqlCommand maxCmd = new SqlCommand(maxQuery, con))
                        {
                            con.Open();
                            model.SortOrder = Convert.ToInt32(maxCmd.ExecuteScalar());
                        }
                    }
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO SubChildModules (ChildModuleId, ModuleName, RouteUrl, SortOrder, IsActive) VALUES (@ChildModuleId, @ModuleName, @RouteUrl, @SortOrder, 1)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ChildModuleId", model.ChildModuleId);
                        cmd.Parameters.AddWithValue("@ModuleName", model.ModuleName ?? "");
                        cmd.Parameters.AddWithValue("@RouteUrl", model.RouteUrl ?? "");
                        cmd.Parameters.AddWithValue("@SortOrder", model.SortOrder);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Sub-Child Module saved successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult GetSubChildModuleById(int id)
        {
            SubChildModuleVM model = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT SubChildModuleId, ChildModuleId, ModuleName, RouteUrl, SortOrder FROM SubChildModules WHERE SubChildModuleId = @SubChildModuleId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@SubChildModuleId", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model = new SubChildModuleVM
                            {
                                SubChildModuleId = Convert.ToInt32(reader["SubChildModuleId"]),
                                ChildModuleId = Convert.ToInt32(reader["ChildModuleId"]),
                                ModuleName = reader["ModuleName"].ToString(),
                                RouteUrl = reader["RouteUrl"].ToString(),
                                SortOrder = Convert.ToInt32(reader["SortOrder"])
                            };
                        }
                    }
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateSubChildModule(SubChildModuleVM model)
        {
            try
            {
                if (!IsSuperAdmin())
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string origQuery = "SELECT SortOrder FROM SubChildModules WHERE SubChildModuleId = @SubChildModuleId";
                        using (SqlCommand origCmd = new SqlCommand(origQuery, con))
                        {
                            origCmd.Parameters.AddWithValue("@SubChildModuleId", model.SubChildModuleId);
                            con.Open();
                            var val = origCmd.ExecuteScalar();
                            if (val != null && val != DBNull.Value)
                            {
                                model.SortOrder = Convert.ToInt32(val);
                            }
                        }
                    }
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE SubChildModules SET ChildModuleId = @ChildModuleId, ModuleName = @ModuleName, RouteUrl = @RouteUrl, SortOrder = @SortOrder WHERE SubChildModuleId = @SubChildModuleId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@SubChildModuleId", model.SubChildModuleId);
                        cmd.Parameters.AddWithValue("@ChildModuleId", model.ChildModuleId);
                        cmd.Parameters.AddWithValue("@ModuleName", model.ModuleName ?? "");
                        cmd.Parameters.AddWithValue("@RouteUrl", model.RouteUrl ?? "");
                        cmd.Parameters.AddWithValue("@SortOrder", model.SortOrder);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteSubChildModule(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM SubChildModules WHERE SubChildModuleId = @SubChildModuleId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@SubChildModuleId", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region Module Actions
        public ActionResult Actions()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            List<ModuleActionModel> list = new List<ModuleActionModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ActionId, ActionName, ActionCode FROM ModuleActions";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new ModuleActionModel
                            {
                                ActionId = Convert.ToInt32(reader["ActionId"]),
                                ActionName = reader["ActionName"].ToString(),
                                ActionCode = reader["ActionCode"].ToString()
                            });
                        }
                    }
                }
            }
            return View(list);
        }

        [HttpPost]
        public JsonResult SaveModuleAction(ModuleActionModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO ModuleActions (ActionName, ActionCode) VALUES (@ActionName, @ActionCode)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ActionName", model.ActionName ?? "");
                        cmd.Parameters.AddWithValue("@ActionCode", model.ActionCode ?? "");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Module Action saved successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult GetModuleActionById(int id)
        {
            ModuleActionModel model = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ActionId, ActionName, ActionCode FROM ModuleActions WHERE ActionId = @ActionId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ActionId", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model = new ModuleActionModel
                            {
                                ActionId = Convert.ToInt32(reader["ActionId"]),
                                ActionName = reader["ActionName"].ToString(),
                                ActionCode = reader["ActionCode"].ToString()
                            };
                        }
                    }
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateModuleAction(ModuleActionModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE ModuleActions SET ActionName = @ActionName, ActionCode = @ActionCode WHERE ActionId = @ActionId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ActionId", model.ActionId);
                        cmd.Parameters.AddWithValue("@ActionName", model.ActionName ?? "");
                        cmd.Parameters.AddWithValue("@ActionCode", model.ActionCode ?? "");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteModuleAction(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM ModuleActions WHERE ActionId = @ActionId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ActionId", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region Helpers
        [HttpGet]
        public JsonResult GetParentModules()
        {
            List<ParentModuleVM> list = new List<ParentModuleVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ParentModuleId, ModuleName FROM ParentModules ORDER BY SortOrder";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new ParentModuleVM
                            {
                                ParentModuleId = Convert.ToInt32(reader["ParentModuleId"]),
                                ModuleName = reader["ModuleName"].ToString()
                            });
                        }
                    }
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetChildModules()
        {
            List<ChildModuleVM> list = new List<ChildModuleVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ChildModuleId, ModuleName FROM ChildModules ORDER BY SortOrder";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new ChildModuleVM
                            {
                                ChildModuleId = Convert.ToInt32(reader["ChildModuleId"]),
                                ModuleName = reader["ModuleName"].ToString()
                            });
                        }
                    }
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        private bool IsSuperAdmin()
        {
            return Session["RoleName"] != null && Session["RoleName"].ToString().Equals("Superadmin", StringComparison.OrdinalIgnoreCase);
        }

        [HttpGet]
        public JsonResult GetNextSortOrder(string type)
        {
            int nextOrder = 1;
            string table = "";
            if (type == "parent") table = "ParentModules";
            else if (type == "child") table = "ChildModules";
            else if (type == "subchild") table = "SubChildModules";

            if (!string.IsNullOrEmpty(table))
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = $"SELECT ISNULL(MAX(SortOrder), 0) + 1 FROM {table}";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        nextOrder = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            return Json(new { nextOrder = nextOrder }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
