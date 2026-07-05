using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using zentoraHRMS.Models;

namespace zentoraHRMS.Controllers
{
    public class LeaveController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Zentora"].ConnectionString;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            base.OnActionExecuting(filterContext);
        }

        #region Leave Applications
        public ActionResult LeaveApplications()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null) return RedirectToAction("Login", "Auth");

            List<LeaveApplicationModel> list = new List<LeaveApplicationModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT LA.LeaveApplicationId, LA.EmployeeId, E.FirstName + ' ' + E.LastName AS EmployeeName, 
                                 LA.LeaveCategoryId, LC.CategoryName AS LeaveCategoryName, 
                                 LA.StartDate, LA.EndDate, LA.Reason, LA.Status, LA.AppliedDate 
                                 FROM LeaveApplications LA 
                                 INNER JOIN EmployeeDetails E ON LA.EmployeeId = E.Id 
                                 INNER JOIN LeaveCategory LC ON LA.LeaveCategoryId = LC.LeaveCategoryId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new LeaveApplicationModel
                            {
                                LeaveApplicationId = Convert.ToInt32(reader["LeaveApplicationId"]),
                                EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                                EmployeeName = reader["EmployeeName"].ToString(),
                                LeaveCategoryId = Convert.ToInt32(reader["LeaveCategoryId"]),
                                LeaveCategoryName = reader["LeaveCategoryName"].ToString(),
                                StartDate = Convert.ToDateTime(reader["StartDate"]),
                                EndDate = Convert.ToDateTime(reader["EndDate"]),
                                Reason = reader["Reason"].ToString(),
                                Status = reader["Status"].ToString(),
                                AppliedDate = Convert.ToDateTime(reader["AppliedDate"])
                            });
                        }
                    }
                }
            }
            return View(list);
        }

        [HttpPost]
        public JsonResult SaveLeaveApplication(LeaveApplicationModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO LeaveApplications (EmployeeId, LeaveCategoryId, StartDate, EndDate, Reason, Status, AppliedDate) 
                                     VALUES (@EmployeeId, @LeaveCategoryId, @StartDate, @EndDate, @Reason, @Status, GETDATE())";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeId", model.EmployeeId);
                        cmd.Parameters.AddWithValue("@LeaveCategoryId", model.LeaveCategoryId);
                        cmd.Parameters.AddWithValue("@StartDate", model.StartDate);
                        cmd.Parameters.AddWithValue("@EndDate", model.EndDate);
                        cmd.Parameters.AddWithValue("@Reason", model.Reason ?? "");
                        cmd.Parameters.AddWithValue("@Status", model.Status ?? "Pending");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Leave application submitted successfully!" });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        public JsonResult GetLeaveApplicationById(int id)
        {
            LeaveApplicationModel model = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT LeaveApplicationId, EmployeeId, LeaveCategoryId, StartDate, EndDate, Reason, Status FROM LeaveApplications WHERE LeaveApplicationId = @LeaveApplicationId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@LeaveApplicationId", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model = new LeaveApplicationModel
                            {
                                LeaveApplicationId = Convert.ToInt32(reader["LeaveApplicationId"]),
                                EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                                LeaveCategoryId = Convert.ToInt32(reader["LeaveCategoryId"]),
                                StartDate = Convert.ToDateTime(reader["StartDate"]),
                                EndDate = Convert.ToDateTime(reader["EndDate"]),
                                Reason = reader["Reason"].ToString(),
                                Status = reader["Status"].ToString()
                            };
                        }
                    }
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateLeaveApplication(LeaveApplicationModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE LeaveApplications SET EmployeeId = @EmployeeId, LeaveCategoryId = @LeaveCategoryId, 
                                     StartDate = @StartDate, EndDate = @EndDate, Reason = @Reason, Status = @Status WHERE LeaveApplicationId = @LeaveApplicationId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@LeaveApplicationId", model.LeaveApplicationId);
                        cmd.Parameters.AddWithValue("@EmployeeId", model.EmployeeId);
                        cmd.Parameters.AddWithValue("@LeaveCategoryId", model.LeaveCategoryId);
                        cmd.Parameters.AddWithValue("@StartDate", model.StartDate);
                        cmd.Parameters.AddWithValue("@EndDate", model.EndDate);
                        cmd.Parameters.AddWithValue("@Reason", model.Reason ?? "");
                        cmd.Parameters.AddWithValue("@Status", model.Status ?? "Pending");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        [HttpPost]
        public JsonResult DeleteLeaveApplication(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM LeaveApplications WHERE LeaveApplicationId = @LeaveApplicationId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@LeaveApplicationId", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }
        #endregion

        #region Leave Balances
        public ActionResult LeaveBalances()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null) return RedirectToAction("Login", "Auth");

            List<LeaveApplicationModel> list = new List<LeaveApplicationModel>();
            // Just returning leave records as an audit overview
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT LA.LeaveApplicationId, LA.EmployeeId, E.FirstName + ' ' + E.LastName AS EmployeeName, 
                                 LA.LeaveCategoryId, LC.CategoryName AS LeaveCategoryName, 
                                 LA.StartDate, LA.EndDate, LA.Reason, LA.Status, LA.AppliedDate 
                                 FROM LeaveApplications LA 
                                 INNER JOIN EmployeeDetails E ON LA.EmployeeId = E.Id 
                                 INNER JOIN LeaveCategory LC ON LA.LeaveCategoryId = LC.LeaveCategoryId 
                                 WHERE LA.Status = 'Approved'";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new LeaveApplicationModel
                            {
                                LeaveApplicationId = Convert.ToInt32(reader["LeaveApplicationId"]),
                                EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                                EmployeeName = reader["EmployeeName"].ToString(),
                                LeaveCategoryId = Convert.ToInt32(reader["LeaveCategoryId"]),
                                LeaveCategoryName = reader["LeaveCategoryName"].ToString(),
                                StartDate = Convert.ToDateTime(reader["StartDate"]),
                                EndDate = Convert.ToDateTime(reader["EndDate"]),
                                Reason = reader["Reason"].ToString(),
                                Status = reader["Status"].ToString(),
                                AppliedDate = Convert.ToDateTime(reader["AppliedDate"])
                            });
                        }
                    }
                }
            }
            return View(list);
        }
        #endregion

        #region Helpers
        [HttpGet]
        public JsonResult GetLeaveCategoriesList()
        {
            List<LeaveCategoryModel> list = new List<LeaveCategoryModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT LeaveCategoryId, CategoryName FROM LeaveCategory WHERE IsActive = 1";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new LeaveCategoryModel
                            {
                                LeaveCategoryId = Convert.ToInt32(reader["LeaveCategoryId"]),
                                CategoryName = reader["CategoryName"].ToString()
                            });
                        }
                    }
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion
            public ActionResult LeavePolicies() { return View(); }
}
}
