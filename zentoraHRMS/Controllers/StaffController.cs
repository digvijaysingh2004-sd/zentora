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
                    string query = @"INSERT INTO LoginDetails (EmpId, FullName, Username, Password, RoleType, IsActive, IsDeleted, CreatedDate, CreatedBy, Phone, Email, LastLogin, SystemAddedOn) 
                                     SELECT @EmpId, @FullName, @Username, @Password, @RoleType, 1, 0, GETDATE(), 1, 
                                            COALESCE(Phone, ''), COALESCE(Email, ''), GETDATE(), GETDATE() 
                                     FROM EmployeeDetails WHERE Id = @EmpId";
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
                                     Password = @Password, RoleType = @RoleType,
                                     Phone = (SELECT COALESCE(Phone, '') FROM EmployeeDetails WHERE Id = @EmpId),
                                     Email = (SELECT COALESCE(Email, '') FROM EmployeeDetails WHERE Id = @EmpId)
                                     WHERE Id = @Id";
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
                string query = @"SELECT Id, EmpCode, FirstName, MiddleName, LastName, Username, PhoneCode, Phone, Email, Password, Gender, MaritalStatus, Designation, Company, Branch, Department, SubDepartment, OfficeShift, OfficeLocation, DOJ, DOL, DOB, ProfileImage, Address, State, City, Country, ZipCode, RoleType, LeaveCategory, HolidayCategory, ProjectRole, ReportingManager, AssociateReportingManager, IsActive FROM EmployeeDetails WHERE IsDeleted = 0";
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
                                EmpCode = reader["EmpCode"] != DBNull.Value ? reader["EmpCode"].ToString() : "",
                                FirstName = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : "",
                                MiddleName = reader["MiddleName"] != DBNull.Value ? reader["MiddleName"].ToString() : "",
                                LastName = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : "",
                                Username = reader["Username"] != DBNull.Value ? reader["Username"].ToString() : "",
                                PhoneCode = reader["PhoneCode"] != DBNull.Value ? reader["PhoneCode"].ToString() : "",
                                Phone = reader["Phone"] != DBNull.Value ? reader["Phone"].ToString() : "",
                                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "",
                                Password = reader["Password"] != DBNull.Value ? reader["Password"].ToString() : "",
                                Gender = reader["Gender"] != DBNull.Value ? reader["Gender"].ToString() : "",
                                MaritalStatus = reader["MaritalStatus"] != DBNull.Value ? reader["MaritalStatus"].ToString() : "",
                                Designation = reader["Designation"] != DBNull.Value ? reader["Designation"].ToString() : "",
                                Company = reader["Company"] != DBNull.Value ? reader["Company"].ToString() : "",
                                Branch = reader["Branch"] != DBNull.Value ? reader["Branch"].ToString() : "",
                                Department = reader["Department"] != DBNull.Value ? reader["Department"].ToString() : "",
                                SubDepartment = reader["SubDepartment"] != DBNull.Value ? reader["SubDepartment"].ToString() : "",
                                OfficeShift = reader["OfficeShift"] != DBNull.Value ? reader["OfficeShift"].ToString() : "",
                                OfficeLocation = reader["OfficeLocation"] != DBNull.Value ? reader["OfficeLocation"].ToString() : "",
                                DOJ = reader["DOJ"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["DOJ"]) : null,
                                DOL = reader["DOL"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["DOL"]) : null,
                                DOB = reader["DOB"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["DOB"]) : null,
                                ProfileImage = reader["ProfileImage"] != DBNull.Value ? reader["ProfileImage"].ToString() : "",
                                Address = reader["Address"] != DBNull.Value ? reader["Address"].ToString() : "",
                                State = reader["State"] != DBNull.Value ? reader["State"].ToString() : "",
                                City = reader["City"] != DBNull.Value ? reader["City"].ToString() : "",
                                Country = reader["Country"] != DBNull.Value ? reader["Country"].ToString() : "",
                                ZipCode = reader["ZipCode"] != DBNull.Value ? reader["ZipCode"].ToString() : "",
                                RoleType = reader["RoleType"] != DBNull.Value ? (int?)Convert.ToInt32(reader["RoleType"]) : null,
                                LeaveCategory = reader["LeaveCategory"] != DBNull.Value ? reader["LeaveCategory"].ToString() : "",
                                HolidayCategory = reader["HolidayCategory"] != DBNull.Value ? reader["HolidayCategory"].ToString() : "",
                                ProjectRole = reader["ProjectRole"] != DBNull.Value ? reader["ProjectRole"].ToString() : "",
                                ReportingManager = reader["ReportingManager"] != DBNull.Value ? (int?)Convert.ToInt32(reader["ReportingManager"]) : null,
                                AssociateReportingManager = reader["AssociateReportingManager"] != DBNull.Value ? (int?)Convert.ToInt32(reader["AssociateReportingManager"]) : null,
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
                    string query = @"INSERT INTO EmployeeDetails 
                                     (EmpCode, FirstName, MiddleName, LastName, Username, PhoneCode, Phone, Email, Password, Gender, MaritalStatus, Designation, Company, Branch, Department, SubDepartment, OfficeShift, OfficeLocation, DOJ, DOL, DOB, ProfileImage, Address, State, City, Country, ZipCode, RoleType, LeaveCategory, HolidayCategory, ProjectRole, ReportingManager, AssociateReportingManager, CreateBy, IsActive, IsDeleted, CreatedDate, SystemAddedOn) 
                                     VALUES 
                                     (@EmpCode, @FirstName, @MiddleName, @LastName, @Username, @PhoneCode, @Phone, @Email, @Password, @Gender, @MaritalStatus, @Designation, @Company, @Branch, @Department, @SubDepartment, @OfficeShift, @OfficeLocation, @DOJ, @DOL, @DOB, @ProfileImage, @Address, @State, @City, @Country, @ZipCode, @RoleType, @LeaveCategory, @HolidayCategory, @ProjectRole, @ReportingManager, @AssociateReportingManager, 1, 1, 0, GETDATE(), GETDATE())";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@EmpCode", model.EmpCode ?? "");
                        cmd.Parameters.AddWithValue("@FirstName", model.FirstName ?? "");
                        cmd.Parameters.AddWithValue("@MiddleName", model.MiddleName ?? "");
                        cmd.Parameters.AddWithValue("@LastName", model.LastName ?? "");
                        cmd.Parameters.AddWithValue("@Username", model.Username ?? "");
                        cmd.Parameters.AddWithValue("@PhoneCode", model.PhoneCode ?? "+91");
                        cmd.Parameters.AddWithValue("@Phone", model.Phone ?? "");
                        cmd.Parameters.AddWithValue("@Email", model.Email ?? "");
                        cmd.Parameters.AddWithValue("@Password", model.Password ?? "123456");
                        cmd.Parameters.AddWithValue("@Gender", model.Gender ?? "");
                        cmd.Parameters.AddWithValue("@MaritalStatus", model.MaritalStatus ?? "");
                        cmd.Parameters.AddWithValue("@Designation", model.Designation ?? "");
                        cmd.Parameters.AddWithValue("@Company", model.Company ?? "");
                        cmd.Parameters.AddWithValue("@Branch", model.Branch ?? "");
                        cmd.Parameters.AddWithValue("@Department", model.Department ?? "");
                        cmd.Parameters.AddWithValue("@SubDepartment", model.SubDepartment ?? "");
                        cmd.Parameters.AddWithValue("@OfficeShift", model.OfficeShift ?? "General");
                        cmd.Parameters.AddWithValue("@OfficeLocation", model.OfficeLocation ?? "");
                        cmd.Parameters.AddWithValue("@DOJ", (object)model.DOJ ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@DOL", (object)model.DOL ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@DOB", (object)model.DOB ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ProfileImage", model.ProfileImage ?? "");
                        cmd.Parameters.AddWithValue("@Address", model.Address ?? "");
                        cmd.Parameters.AddWithValue("@State", model.State ?? "");
                        cmd.Parameters.AddWithValue("@City", model.City ?? "");
                        cmd.Parameters.AddWithValue("@Country", model.Country ?? "");
                        cmd.Parameters.AddWithValue("@ZipCode", model.ZipCode ?? "");
                        cmd.Parameters.AddWithValue("@RoleType", (object)model.RoleType ?? 1);
                        cmd.Parameters.AddWithValue("@LeaveCategory", model.LeaveCategory ?? "");
                        cmd.Parameters.AddWithValue("@HolidayCategory", model.HolidayCategory ?? "");
                        cmd.Parameters.AddWithValue("@ProjectRole", model.ProjectRole ?? "");
                        cmd.Parameters.AddWithValue("@ReportingManager", (object)model.ReportingManager ?? 0);
                        cmd.Parameters.AddWithValue("@AssociateReportingManager", (object)model.AssociateReportingManager ?? 0);
                        
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
                string query = "SELECT Id, EmpCode, FirstName, MiddleName, LastName, Username, PhoneCode, Phone, Email, Password, Gender, MaritalStatus, Designation, Company, Branch, Department, SubDepartment, OfficeShift, OfficeLocation, DOJ, DOL, DOB, ProfileImage, Address, State, City, Country, ZipCode, RoleType, LeaveCategory, HolidayCategory, ProjectRole, ReportingManager, AssociateReportingManager, IsActive FROM EmployeeDetails WHERE Id = @Id";
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
                                EmpCode = reader["EmpCode"] != DBNull.Value ? reader["EmpCode"].ToString() : "",
                                FirstName = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : "",
                                MiddleName = reader["MiddleName"] != DBNull.Value ? reader["MiddleName"].ToString() : "",
                                LastName = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : "",
                                Username = reader["Username"] != DBNull.Value ? reader["Username"].ToString() : "",
                                PhoneCode = reader["PhoneCode"] != DBNull.Value ? reader["PhoneCode"].ToString() : "",
                                Phone = reader["Phone"] != DBNull.Value ? reader["Phone"].ToString() : "",
                                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "",
                                Password = reader["Password"] != DBNull.Value ? reader["Password"].ToString() : "",
                                Gender = reader["Gender"] != DBNull.Value ? reader["Gender"].ToString() : "",
                                MaritalStatus = reader["MaritalStatus"] != DBNull.Value ? reader["MaritalStatus"].ToString() : "",
                                Designation = reader["Designation"] != DBNull.Value ? reader["Designation"].ToString() : "",
                                Company = reader["Company"] != DBNull.Value ? reader["Company"].ToString() : "",
                                Branch = reader["Branch"] != DBNull.Value ? reader["Branch"].ToString() : "",
                                Department = reader["Department"] != DBNull.Value ? reader["Department"].ToString() : "",
                                SubDepartment = reader["SubDepartment"] != DBNull.Value ? reader["SubDepartment"].ToString() : "",
                                OfficeShift = reader["OfficeShift"] != DBNull.Value ? reader["OfficeShift"].ToString() : "",
                                OfficeLocation = reader["OfficeLocation"] != DBNull.Value ? reader["OfficeLocation"].ToString() : "",
                                DOJ = reader["DOJ"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["DOJ"]) : null,
                                DOL = reader["DOL"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["DOL"]) : null,
                                DOB = reader["DOB"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["DOB"]) : null,
                                ProfileImage = reader["ProfileImage"] != DBNull.Value ? reader["ProfileImage"].ToString() : "",
                                Address = reader["Address"] != DBNull.Value ? reader["Address"].ToString() : "",
                                State = reader["State"] != DBNull.Value ? reader["State"].ToString() : "",
                                City = reader["City"] != DBNull.Value ? reader["City"].ToString() : "",
                                Country = reader["Country"] != DBNull.Value ? reader["Country"].ToString() : "",
                                ZipCode = reader["ZipCode"] != DBNull.Value ? reader["ZipCode"].ToString() : "",
                                RoleType = reader["RoleType"] != DBNull.Value ? (int?)Convert.ToInt32(reader["RoleType"]) : null,
                                LeaveCategory = reader["LeaveCategory"] != DBNull.Value ? reader["LeaveCategory"].ToString() : "",
                                HolidayCategory = reader["HolidayCategory"] != DBNull.Value ? reader["HolidayCategory"].ToString() : "",
                                ProjectRole = reader["ProjectRole"] != DBNull.Value ? reader["ProjectRole"].ToString() : "",
                                ReportingManager = reader["ReportingManager"] != DBNull.Value ? (int?)Convert.ToInt32(reader["ReportingManager"]) : null,
                                AssociateReportingManager = reader["AssociateReportingManager"] != DBNull.Value ? (int?)Convert.ToInt32(reader["AssociateReportingManager"]) : null,
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
                    string query = @"UPDATE EmployeeDetails SET 
                                     EmpCode = @EmpCode, FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, 
                                     Username = @Username, PhoneCode = @PhoneCode, Phone = @Phone, Email = @Email, Password = @Password, 
                                     Gender = @Gender, MaritalStatus = @MaritalStatus, Designation = @Designation, Company = @Company, 
                                     Branch = @Branch, Department = @Department, SubDepartment = @SubDepartment, OfficeShift = @OfficeShift, 
                                     OfficeLocation = @OfficeLocation, DOJ = @DOJ, DOL = @DOL, DOB = @DOB, ProfileImage = @ProfileImage, 
                                     Address = @Address, State = @State, City = @City, Country = @Country, ZipCode = @ZipCode, 
                                     RoleType = @RoleType, LeaveCategory = @LeaveCategory, HolidayCategory = @HolidayCategory, 
                                     ProjectRole = @ProjectRole, ReportingManager = @ReportingManager, 
                                     AssociateReportingManager = @AssociateReportingManager, IsActive = @IsActive,
                                     UpdatedDate = GETDATE(), UpdatedBy = 1
                                     WHERE Id = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", model.Id);
                        cmd.Parameters.AddWithValue("@EmpCode", model.EmpCode ?? "");
                        cmd.Parameters.AddWithValue("@FirstName", model.FirstName ?? "");
                        cmd.Parameters.AddWithValue("@MiddleName", model.MiddleName ?? "");
                        cmd.Parameters.AddWithValue("@LastName", model.LastName ?? "");
                        cmd.Parameters.AddWithValue("@Username", model.Username ?? "");
                        cmd.Parameters.AddWithValue("@PhoneCode", model.PhoneCode ?? "+91");
                        cmd.Parameters.AddWithValue("@Phone", model.Phone ?? "");
                        cmd.Parameters.AddWithValue("@Email", model.Email ?? "");
                        cmd.Parameters.AddWithValue("@Password", model.Password ?? "123456");
                        cmd.Parameters.AddWithValue("@Gender", model.Gender ?? "");
                        cmd.Parameters.AddWithValue("@MaritalStatus", model.MaritalStatus ?? "");
                        cmd.Parameters.AddWithValue("@Designation", model.Designation ?? "");
                        cmd.Parameters.AddWithValue("@Company", model.Company ?? "");
                        cmd.Parameters.AddWithValue("@Branch", model.Branch ?? "");
                        cmd.Parameters.AddWithValue("@Department", model.Department ?? "");
                        cmd.Parameters.AddWithValue("@SubDepartment", model.SubDepartment ?? "");
                        cmd.Parameters.AddWithValue("@OfficeShift", model.OfficeShift ?? "General");
                        cmd.Parameters.AddWithValue("@OfficeLocation", model.OfficeLocation ?? "");
                        cmd.Parameters.AddWithValue("@DOJ", (object)model.DOJ ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@DOL", (object)model.DOL ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@DOB", (object)model.DOB ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ProfileImage", model.ProfileImage ?? "");
                        cmd.Parameters.AddWithValue("@Address", model.Address ?? "");
                        cmd.Parameters.AddWithValue("@State", model.State ?? "");
                        cmd.Parameters.AddWithValue("@City", model.City ?? "");
                        cmd.Parameters.AddWithValue("@Country", model.Country ?? "");
                        cmd.Parameters.AddWithValue("@ZipCode", model.ZipCode ?? "");
                        cmd.Parameters.AddWithValue("@RoleType", (object)model.RoleType ?? 1);
                        cmd.Parameters.AddWithValue("@LeaveCategory", model.LeaveCategory ?? "");
                        cmd.Parameters.AddWithValue("@HolidayCategory", model.HolidayCategory ?? "");
                        cmd.Parameters.AddWithValue("@ProjectRole", model.ProjectRole ?? "");
                        cmd.Parameters.AddWithValue("@ReportingManager", (object)model.ReportingManager ?? 0);
                        cmd.Parameters.AddWithValue("@AssociateReportingManager", (object)model.AssociateReportingManager ?? 0);
                        cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                        
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
                string query = "SELECT Id, FirstName, LastName, Username FROM EmployeeDetails WHERE IsDeleted = 0 AND IsActive = 1";
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
                                LastName = reader["LastName"].ToString(),
                                Username = reader["Username"] != DBNull.Value ? reader["Username"].ToString() : ""
                            });
                        }
                    }
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDesignationsList()
        {
            List<string> list = new List<string>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT DesignationName FROM Designations WHERE IsActive = 1 ORDER BY DesignationName";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(reader["DesignationName"].ToString());
                        }
                    }
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetBranchesList()
        {
            List<string> list = new List<string>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT BranchName FROM Branches ORDER BY BranchName";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(reader["BranchName"].ToString());
                        }
                    }
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDepartmentsList()
        {
            List<string> list = new List<string>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT DepartmentName FROM Departments ORDER BY DepartmentName";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(reader["DepartmentName"].ToString());
                        }
                    }
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSubDepartmentsList()
        {
            List<string> list = new List<string>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT SubDepartmentName FROM SubDepartments ORDER BY SubDepartmentName";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(reader["SubDepartmentName"].ToString());
                        }
                    }
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDepartmentsByBranch(string branchName)
        {
            List<string> list = new List<string>();
            if (string.IsNullOrEmpty(branchName))
            {
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT DepartmentName FROM Departments 
                                 WHERE BranchID = (SELECT TOP 1 BranchId FROM Branches WHERE BranchName = @BranchName) 
                                 ORDER BY DepartmentName";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@BranchName", branchName);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(reader["DepartmentName"].ToString());
                        }
                    }
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSubDepartmentsByDepartment(string departmentName)
        {
            List<string> list = new List<string>();
            if (string.IsNullOrEmpty(departmentName))
            {
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT SubDepartmentName FROM SubDepartments 
                                 WHERE DepartmentID = (SELECT TOP 1 DepartmentID FROM Departments WHERE DepartmentName = @DepartmentName) 
                                 ORDER BY SubDepartmentName";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@DepartmentName", departmentName);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(reader["SubDepartmentName"].ToString());
                        }
                    }
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetShiftsList()
        {
            List<string> list = new List<string>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ShiftName FROM Shifts WHERE IsActive = 1 ORDER BY ShiftName";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(reader["ShiftName"].ToString());
                        }
                    }
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetHolidayCategoriesList()
        {
            List<string> list = new List<string>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT CategoryName FROM HolidayCategory WHERE IsActive = 1 ORDER BY CategoryName";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(reader["CategoryName"].ToString());
                        }
                    }
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetLeaveCategoriesList()
        {
            List<string> list = new List<string>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT CategoryName FROM LeaveCategory WHERE IsActive = 1 ORDER BY CategoryName";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(reader["CategoryName"].ToString());
                        }
                    }
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
