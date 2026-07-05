using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using zentoraHRMS.Models;

namespace zentoraHRMS.Controllers
{
    public class StaffController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Zentora"].ConnectionString;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            base.OnActionExecuting(filterContext);
        }

        #region Users
        public ActionResult Users()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            List<UserModel> list = new List<UserModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, EmpId, FullName, Username, Password, RoleType, IsActive FROM LoginDetails WHERE IsDeleted = 0";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new UserModel
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                EmpId = Convert.ToInt32(reader["EmpId"]),
                                FullName = reader["FullName"].ToString(),
                                Username = reader["Username"].ToString(),
                                Password = reader["Password"].ToString(),
                                RoleType = reader["RoleType"].ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"])
                            });
                        }
                    }
                }
            }
            return View(list);
        }

        [HttpPost]
        public JsonResult SaveUser(UserModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO LoginDetails (EmpId, FullName, Username, Password, RoleType, IsActive, IsDeleted, CreatedDate, CreatedBy) 
                                     VALUES (@EmpId, @FullName, @Username, @Password, @RoleType, 1, 0, GETDATE(), 1)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@EmpId", model.EmpId);
                        cmd.Parameters.AddWithValue("@FullName", model.FullName ?? "");
                        cmd.Parameters.AddWithValue("@Username", model.Username ?? "");
                        cmd.Parameters.AddWithValue("@Password", model.Password ?? "");
                        cmd.Parameters.AddWithValue("@RoleType", model.RoleType ?? "");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "User saved successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult GetUserById(int id)
        {
            UserModel model = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, EmpId, FullName, Username, Password, RoleType, IsActive FROM LoginDetails WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model = new UserModel
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                EmpId = Convert.ToInt32(reader["EmpId"]),
                                FullName = reader["FullName"].ToString(),
                                Username = reader["Username"].ToString(),
                                Password = reader["Password"].ToString(),
                                RoleType = reader["RoleType"].ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"])
                            };
                        }
                    }
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateUser(UserModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE LoginDetails SET EmpId = @EmpId, FullName = @FullName, Username = @Username, 
                                     Password = @Password, RoleType = @RoleType WHERE Id = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", model.Id);
                        cmd.Parameters.AddWithValue("@EmpId", model.EmpId);
                        cmd.Parameters.AddWithValue("@FullName", model.FullName ?? "");
                        cmd.Parameters.AddWithValue("@Username", model.Username ?? "");
                        cmd.Parameters.AddWithValue("@Password", model.Password ?? "");
                        cmd.Parameters.AddWithValue("@RoleType", model.RoleType ?? "");
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
        public JsonResult DeleteUser(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE LoginDetails SET IsDeleted = 1 WHERE Id = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
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

        #region Employees
        public ActionResult Employees()
        {
            if (Session["RoleType"] == null || Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            List<EmployeeModel> list = new List<EmployeeModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, EmpCode, FirstName, LastName, Username, Phone, Email, Designation, Company, Branch, Department, IsActive FROM EmployeeDetails WHERE IsDeleted = 0";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new EmployeeModel
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                EmpCode = reader["EmpCode"].ToString(),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Username = reader["Username"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                Email = reader["Email"].ToString(),
                                Designation = reader["Designation"].ToString(),
                                Company = reader["Company"].ToString(),
                                Branch = reader["Branch"].ToString(),
                                Department = reader["Department"].ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"])
                            });
                        }
                    }
                }
            }
            return View(list);
        }

        [HttpPost]
        public JsonResult SaveEmployee(EmployeeModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO EmployeeDetails (EmpCode, FirstName, LastName, Username, Phone, Email, Designation, Company, Branch, Department, IsActive, IsDeleted, CreatedDate, SystemAddedOn) 
                                     VALUES (@EmpCode, @FirstName, @LastName, @Username, @Phone, @Email, @Designation, @Company, @Branch, @Department, 1, 0, GETDATE(), GETDATE())";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@EmpCode", model.EmpCode ?? "");
                        cmd.Parameters.AddWithValue("@FirstName", model.FirstName ?? "");
                        cmd.Parameters.AddWithValue("@LastName", model.LastName ?? "");
                        cmd.Parameters.AddWithValue("@Username", model.Username ?? "");
                        cmd.Parameters.AddWithValue("@Phone", model.Phone ?? "");
                        cmd.Parameters.AddWithValue("@Email", model.Email ?? "");
                        cmd.Parameters.AddWithValue("@Designation", model.Designation ?? "");
                        cmd.Parameters.AddWithValue("@Company", model.Company ?? "");
                        cmd.Parameters.AddWithValue("@Branch", model.Branch ?? "");
                        cmd.Parameters.AddWithValue("@Department", model.Department ?? "");
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return Json(new { success = true, message = "Employee saved successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult GetEmployeeById(int id)
        {
            EmployeeModel model = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, EmpCode, FirstName, LastName, Username, Phone, Email, Designation, Company, Branch, Department, IsActive FROM EmployeeDetails WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model = new EmployeeModel
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                EmpCode = reader["EmpCode"].ToString(),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Username = reader["Username"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                Email = reader["Email"].ToString(),
                                Designation = reader["Designation"].ToString(),
                                Company = reader["Company"].ToString(),
                                Branch = reader["Branch"].ToString(),
                                Department = reader["Department"].ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"])
                            };
                        }
                    }
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateEmployee(EmployeeModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE EmployeeDetails SET EmpCode = @EmpCode, FirstName = @FirstName, LastName = @LastName, 
                                     Username = @Username, Phone = @Phone, Email = @Email, Designation = @Designation, 
                                     Company = @Company, Branch = @Branch, Department = @Department WHERE Id = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", model.Id);
                        cmd.Parameters.AddWithValue("@EmpCode", model.EmpCode ?? "");
                        cmd.Parameters.AddWithValue("@FirstName", model.FirstName ?? "");
                        cmd.Parameters.AddWithValue("@LastName", model.LastName ?? "");
                        cmd.Parameters.AddWithValue("@Username", model.Username ?? "");
                        cmd.Parameters.AddWithValue("@Phone", model.Phone ?? "");
                        cmd.Parameters.AddWithValue("@Email", model.Email ?? "");
                        cmd.Parameters.AddWithValue("@Designation", model.Designation ?? "");
                        cmd.Parameters.AddWithValue("@Company", model.Company ?? "");
                        cmd.Parameters.AddWithValue("@Branch", model.Branch ?? "");
                        cmd.Parameters.AddWithValue("@Department", model.Department ?? "");
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
        public JsonResult DeleteEmployee(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE EmployeeDetails SET IsDeleted = 1 WHERE Id = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
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
        public JsonResult GetEmployeeList()
        {
            List<EmployeeModel> list = new List<EmployeeModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, FirstName, LastName FROM EmployeeDetails WHERE IsDeleted = 0 AND IsActive = 1";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new EmployeeModel
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString()
                            });
                        }
                    }
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
